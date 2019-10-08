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
    public partial class ClassList : System.Web.UI.Page
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
            com.CommandText = "ClassCRUD";
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add(new SqlParameter("@action", SqlDbType.VarChar, 50));
            com.Parameters["@action"].Value = "Read";
            sqlda = new SqlDataAdapter(com);
            ds = new DataSet();
            sqlda.Fill(ds);
            ClassGridView.DataSource = ds;
            ClassGridView.DataBind();
            con.Close();
        }

        protected void ClassGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView drvClass = e.Row.DataItem as DataRowView;

                DropDownList ddlLecturer = e.Row.FindControl("ddlLecturer") as DropDownList;
                if (ddlLecturer != null)
                {
                    SqlConnection con = new SqlConnection(connectionString);
                    com = new SqlCommand();
                    con.Open();
                    com.Connection = con;
                    com.CommandText = "DDLLecturer";
                    com.CommandType = CommandType.StoredProcedure;
                    sqlda = new SqlDataAdapter(com);
                    ds = new DataSet();
                    sqlda.Fill(ds);
                    ddlLecturer.DataSource = ds;
                    ddlLecturer.DataTextField = "Name";
                    ddlLecturer.DataValueField = "LecturerID";
                    ddlLecturer.DataBind();
                    con.Close();
                    ddlLecturer.SelectedValue = drvClass["LecturerID"].ToString();
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
                    ddlBatch.SelectedValue = drvClass["BatchID"].ToString();
                }
            }
        }

        protected void ClassGridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            ClassGridView.EditIndex = e.NewEditIndex;
            LoadData();
        }

        protected void ClassGridView_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int ClassID = int.Parse(ClassGridView.Rows[e.RowIndex].Cells[0].Text);
            TextBox txtName = ClassGridView.Rows[e.RowIndex].FindControl("txtName") as TextBox;
            TextBox txtDescription = ClassGridView.Rows[e.RowIndex].FindControl("txtDescription") as TextBox;
            DropDownList ddlBatch = ClassGridView.Rows[e.RowIndex].FindControl("ddlBatch") as DropDownList;
            DropDownList ddlLecturer = ClassGridView.Rows[e.RowIndex].FindControl("ddlLecturer") as DropDownList;
            UpdateClass(ClassID, txtName.Text, txtDescription.Text, int.Parse(ddlBatch.SelectedValue), int.Parse(ddlLecturer.SelectedValue));
            ClassGridView.EditIndex = -1;
            LoadData();
        }

        protected void UpdateClass(int ClassID, string Name, string Description, int BatchID, int LecturerID)
        {
            SqlConnection con = new SqlConnection(connectionString);
            com = new SqlCommand();
            con.Open();
            com.Connection = con;
            com.CommandText = "ClassCRUD";
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add(new SqlParameter("@action", SqlDbType.VarChar, 50));
            com.Parameters.Add(new SqlParameter("@ClassID", SqlDbType.Int));
            com.Parameters.Add(new SqlParameter("@Name", SqlDbType.VarChar, 100));
            com.Parameters.Add(new SqlParameter("@Description", SqlDbType.VarChar, 100));
            com.Parameters.Add(new SqlParameter("@BatchID", SqlDbType.Int));
            com.Parameters.Add(new SqlParameter("@LecturerID", SqlDbType.Int));
            com.Parameters["@action"].Value = "Update";
            com.Parameters["@ClassID"].Value = ClassID;
            com.Parameters["@Name"].Value = Name;
            com.Parameters["@Description"].Value = Description;
            com.Parameters["@LecturerID"].Value = LecturerID;
            com.Parameters["@BatchID"].Value = BatchID;
            sqlda = new SqlDataAdapter(com);
            ds = new DataSet();
            sqlda.Fill(ds);
            con.Close();
        }

        protected void ClassGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int ClassID = int.Parse(ClassGridView.Rows[e.RowIndex].Cells[0].Text);
            DeleteClass(ClassID);
            ClassGridView.EditIndex = -1;
            LoadData();
        }

        protected void DeleteClass(int ClassID)
        {
            SqlConnection con = new SqlConnection(connectionString);
            com = new SqlCommand();
            con.Open();
            com.Connection = con;
            com.CommandText = "ClassCRUD";
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add(new SqlParameter("@action", SqlDbType.VarChar, 50));
            com.Parameters.Add(new SqlParameter("@ClassID", SqlDbType.Int));
            com.Parameters["@action"].Value = "Delete";
            com.Parameters["@ClassID"].Value = ClassID;
            sqlda = new SqlDataAdapter(com);
            ds = new DataSet();
            sqlda.Fill(ds);
            con.Close();
        }

        protected void ClassGridView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            ClassGridView.EditIndex = -1;
            LoadData();
        }

        protected void ClassGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }
    }
}