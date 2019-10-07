using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace assignment.aspx
{
    public partial class StudentList : System.Web.UI.Page
    {
        string connectionString = ConfigurationManager.ConnectionStrings["assignmentConnectionString"].ConnectionString;
        SqlCommand com;
        SqlDataAdapter sqlda;
        DataSet ds;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
            }
        }

        protected void LoadData()
        {
            SqlConnection con = new SqlConnection(connectionString);
            com = new SqlCommand();
            con.Open();
            com.Connection = con;
            com.CommandText = "StudentCRUD";
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add(new SqlParameter("@action", SqlDbType.VarChar, 50));
            com.Parameters["@action"].Value = "Read";
            sqlda = new SqlDataAdapter(com);
            ds = new DataSet();
            sqlda.Fill(ds);
            StudentGridView.DataSource = ds;
            StudentGridView.DataBind();
            con.Close();
        }

        protected void StudentGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
        }

        protected void StudentGridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            StudentGridView.EditIndex = e.NewEditIndex;
            LoadData();
        }

        protected void StudentGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //string StudentID = DataBinder.Eval(e.Row.DataItem, "StudentID").ToString();
                //string Location = ResolveUrl("StudentForm.aspx") + "?action=view" + "&StudentID=" + StudentID;
                //e.Row.Attributes["onclick"] = string.Format("javascript:window.location='{0}';", Location);
                //e.Row.Style["cursor"] = "pointer";

                DataRowView drvStudent = e.Row.DataItem as DataRowView;

                DropDownList ddlStatus = e.Row.FindControl("ddlStatus") as DropDownList;
                if (ddlStatus != null)
                {
                    SqlConnection con = new SqlConnection(connectionString);
                    com = new SqlCommand();
                    con.Open();
                    com.Connection = con;
                    com.CommandText = "DDLStudentStatus";
                    com.CommandType = CommandType.StoredProcedure;
                    sqlda = new SqlDataAdapter(com);
                    ds = new DataSet();
                    sqlda.Fill(ds);
                    ddlStatus.DataSource = ds;
                    ddlStatus.DataTextField = "Status";
                    ddlStatus.DataValueField = "StudentStatusID";
                    ddlStatus.DataBind();
                    con.Close();
                    ddlStatus.SelectedValue = drvStudent["StatusID"].ToString();
                }
                DropDownList ddlBatch = e.Row.FindControl("ddlBatch") as DropDownList;
                if (ddlBatch != null)
                {
                    SqlConnection con = new SqlConnection(connectionString);
                    com = new SqlCommand();
                    con.Open();
                    com.Connection = con;
                    com.CommandText = "DDLBatch";
                    com.CommandType = CommandType.StoredProcedure;
                    sqlda = new SqlDataAdapter(com);
                    ds = new DataSet();
                    sqlda.Fill(ds);
                    ddlBatch.DataSource = ds;
                    ddlBatch.DataTextField = "Batch";
                    ddlBatch.DataValueField = "BatchID";
                    ddlBatch.DataBind();
                    con.Close();
                    ddlBatch.SelectedValue = drvStudent["BatchID"].ToString();
                }
                DropDownList ddlClass = e.Row.FindControl("ddlClass") as DropDownList;
                if (ddlBatch != null)
                {
                    SqlConnection con = new SqlConnection(connectionString);
                    com = new SqlCommand();
                    con.Open();
                    com.Connection = con;
                    com.CommandText = "DDLClass";
                    com.CommandType = CommandType.StoredProcedure;
                    sqlda = new SqlDataAdapter(com);
                    ds = new DataSet();
                    sqlda.Fill(ds);
                    ddlClass.DataSource = ds;
                    ddlClass.DataTextField = "Class";
                    ddlClass.DataValueField = "ClassID";
                    ddlClass.DataBind();
                    con.Close();
                    ddlClass.SelectedValue = drvStudent["ClassID"].ToString();
                }
            }
        }

        protected void StudentGridView_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int StudentID = int.Parse(StudentGridView.Rows[e.RowIndex].Cells[0].Text);
            TextBox txtName = StudentGridView.Rows[e.RowIndex].FindControl("txtName") as TextBox;
            TextBox txtEmail = StudentGridView.Rows[e.RowIndex].FindControl("txtEmail") as TextBox;
            TextBox txtContactNo = StudentGridView.Rows[e.RowIndex].FindControl("txtContactNo") as TextBox;
            DropDownList ddlBatch = StudentGridView.Rows[e.RowIndex].FindControl("ddlBatch") as DropDownList;
            DropDownList ddlStatus = StudentGridView.Rows[e.RowIndex].FindControl("ddlStatus") as DropDownList;
            DropDownList ddlClass = StudentGridView.Rows[e.RowIndex].FindControl("ddlClass") as DropDownList;
            if (ddlClass.SelectedValue == GetStudentCurrentClass(StudentID))
            {
                UpdateStudent(StudentID, txtName.Text, txtEmail.Text, txtContactNo.Text, int.Parse(ddlBatch.SelectedValue), int.Parse(ddlStatus.SelectedValue), int.Parse(ddlClass.SelectedValue), "false");
            }
            else
            {
                UpdateStudent(StudentID, txtName.Text, txtEmail.Text, txtContactNo.Text, int.Parse(ddlBatch.SelectedValue), int.Parse(ddlStatus.SelectedValue), int.Parse(ddlClass.SelectedValue), "true");
            }
            StudentGridView.EditIndex = -1;
            LoadData();
        }

        protected void UpdateStudent(int StudentID, string Name, string Email, string ContactNo, int BatchID, int StatusID, int ClassID, string IsClassChanged)
        {
            SqlConnection con = new SqlConnection(connectionString);
            com = new SqlCommand();
            con.Open();
            com.Connection = con;
            com.CommandText = "StudentCRUD";
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add(new SqlParameter("@action", SqlDbType.VarChar, 50));
            com.Parameters.Add(new SqlParameter("@IsClassChanged", SqlDbType.VarChar, 50));
            com.Parameters.Add(new SqlParameter("@StudentID", SqlDbType.Int));
            com.Parameters.Add(new SqlParameter("@Name", SqlDbType.VarChar, 100));
            com.Parameters.Add(new SqlParameter("@Email", SqlDbType.VarChar, 100));
            com.Parameters.Add(new SqlParameter("@ContactNo", SqlDbType.VarChar, 15));
            com.Parameters.Add(new SqlParameter("@BatchID", SqlDbType.Int));
            com.Parameters.Add(new SqlParameter("@StatusID", SqlDbType.Int));
            com.Parameters.Add(new SqlParameter("@ClassID", SqlDbType.Int));
            com.Parameters["@action"].Value = "Update";
            com.Parameters["@IsClassChanged"].Value = IsClassChanged;
            com.Parameters["@StudentID"].Value = StudentID;
            com.Parameters["@Name"].Value = Name;
            com.Parameters["@Email"].Value = Email;
            com.Parameters["@ContactNo"].Value = ContactNo;
            com.Parameters["@BatchID"].Value = BatchID;
            com.Parameters["@StatusID"].Value = StatusID;
            com.Parameters["@ClassID"].Value = ClassID;
            sqlda = new SqlDataAdapter(com);
            ds = new DataSet();
            sqlda.Fill(ds);
            con.Close();
        }

        protected string GetStudentCurrentClass(int StudentID)
        {
            SqlConnection con = new SqlConnection(connectionString);
            com = new SqlCommand();
            con.Open();
            com.Connection = con;
            com.CommandText = "GetStudentCurrentClass";
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add(new SqlParameter("@StudentID", SqlDbType.Int));
            com.Parameters["@StudentID"].Value = StudentID;
            sqlda = new SqlDataAdapter(com);
            ds = new DataSet();
            sqlda.Fill(ds);
            con.Close();
            return ds.Tables[0].Rows[0]["ClassID"].ToString();
        }

        protected void StudentGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int StudentID = int.Parse(StudentGridView.Rows[e.RowIndex].Cells[0].Text);
            DeleteStudent(StudentID);
            StudentGridView.EditIndex = -1;
            LoadData();
        }

        protected void DeleteStudent(int StudentID)
        {
            SqlConnection con = new SqlConnection(connectionString);
            com = new SqlCommand();
            con.Open();
            com.Connection = con;
            com.CommandText = "StudentCRUD";
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add(new SqlParameter("@action", SqlDbType.VarChar, 50));
            com.Parameters.Add(new SqlParameter("@StudentID", SqlDbType.Int));
            com.Parameters["@action"].Value = "Delete";
            com.Parameters["@StudentID"].Value = StudentID;
            sqlda = new SqlDataAdapter(com);
            ds = new DataSet();
            sqlda.Fill(ds);
            con.Close();
        }

        protected void StudentGridView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            StudentGridView.EditIndex = -1;
            LoadData();
        }
    }
}