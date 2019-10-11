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
    public partial class SubjectList : System.Web.UI.Page
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
            com.CommandText = "SubjectCRUD";
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add(new SqlParameter("@action", SqlDbType.VarChar, 50));
            com.Parameters["@action"].Value = "Read";
            sqlda = new SqlDataAdapter(com);
            ds = new DataSet();
            sqlda.Fill(ds);
            SubjectGridView.DataSource = ds;
            SubjectGridView.DataBind();
            con.Close();
        }

        protected void SubjectGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView drvSubject = e.Row.DataItem as DataRowView;

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
                    ddlLecturer.SelectedValue = drvSubject["LecturerID"].ToString();
                }
            }
        }

        protected void SubjectGridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            SubjectGridView.EditIndex = e.NewEditIndex;
            LoadData();
        }

        protected void SubjectGridView_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int SubjectID = int.Parse(SubjectGridView.Rows[e.RowIndex].Cells[0].Text);
            TextBox txtName = SubjectGridView.Rows[e.RowIndex].FindControl("txtName") as TextBox;
            TextBox txtDescription = SubjectGridView.Rows[e.RowIndex].FindControl("txtDescription") as TextBox;
            DropDownList ddlLecturer = SubjectGridView.Rows[e.RowIndex].FindControl("ddlLecturer") as DropDownList;
            UpdateSubject(SubjectID, txtName.Text, txtDescription.Text);
            InsertOrUpdateLecturerSubject(int.Parse(ddlLecturer.SelectedValue), SubjectID);
            SubjectGridView.EditIndex = -1;
            LoadData();
        }

        protected void UpdateSubject(int SubjectID, string Name, string Description)
        {
            SqlConnection con = new SqlConnection(connectionString);
            com = new SqlCommand();
            con.Open();
            com.Connection = con;
            com.CommandText = "SubjectCRUD";
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add(new SqlParameter("@action", SqlDbType.VarChar, 50));
            com.Parameters.Add(new SqlParameter("@SubjectID", SqlDbType.Int));
            com.Parameters.Add(new SqlParameter("@Name", SqlDbType.VarChar, 100));
            com.Parameters.Add(new SqlParameter("@Description", SqlDbType.VarChar, 100));
            com.Parameters["@action"].Value = "Update";
            com.Parameters["@SubjectID"].Value = SubjectID;
            com.Parameters["@Name"].Value = Name;
            com.Parameters["@Description"].Value = Description;
            com.ExecuteNonQuery();
            con.Close();
        }

        protected void InsertOrUpdateLecturerSubject(int LecturerID, int SubjectID)
        {
            SqlConnection con = new SqlConnection(connectionString);
            com = new SqlCommand();
            con.Open();
            com.Connection = con;
            com.CommandText = "InsertOrUpdateLecturerSubject";
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add(new SqlParameter("@SubjectID", SqlDbType.Int));
            com.Parameters.Add(new SqlParameter("@LecturerID", SqlDbType.Int));
            com.Parameters["@SubjectID"].Value = SubjectID;
            com.Parameters["@LecturerID"].Value = LecturerID;
            com.ExecuteNonQuery();
            con.Close();
        }

        protected void SubjectGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int SubjectID = int.Parse(SubjectGridView.Rows[e.RowIndex].Cells[0].Text);
            DeleteSubject(SubjectID);
            SubjectGridView.EditIndex = -1;
            LoadData();
        }

        protected void DeleteSubject(int SubjectID)
        {
            SqlConnection con = new SqlConnection(connectionString);
            com = new SqlCommand();
            con.Open();
            com.Connection = con;
            com.CommandText = "SubjectCRUD";
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add(new SqlParameter("@action", SqlDbType.VarChar, 50));
            com.Parameters.Add(new SqlParameter("@SubjectID", SqlDbType.Int));
            com.Parameters["@action"].Value = "Delete";
            com.Parameters["@SubjectID"].Value = SubjectID;
            sqlda = new SqlDataAdapter(com);
            ds = new DataSet();
            sqlda.Fill(ds);
            con.Close();
        }

        protected void SubjectGridView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            SubjectGridView.EditIndex = -1;
            LoadData();
        }

        protected void SubjectGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }
    }
}