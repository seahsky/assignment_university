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
    public partial class LecturerList : System.Web.UI.Page
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
            com.CommandText = "LecturerCRUD";
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add(new SqlParameter("@action", SqlDbType.VarChar, 50));
            com.Parameters["@action"].Value = "Read";
            sqlda = new SqlDataAdapter(com);
            ds = new DataSet();
            sqlda.Fill(ds);
            LecturerGridView.DataSource = ds;
            LecturerGridView.DataBind();
            con.Close();
        }

        protected void LecturerGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList ddlStatus = e.Row.FindControl("ddlStatus") as DropDownList;
                if (ddlStatus != null)
                {
                    SqlConnection con = new SqlConnection(connectionString);
                    com = new SqlCommand();
                    con.Open();
                    com.Connection = con;
                    com.CommandText = "DDLLecturerStatus";
                    com.CommandType = CommandType.StoredProcedure;
                    sqlda = new SqlDataAdapter(com);
                    ds = new DataSet();
                    sqlda.Fill(ds);
                    ddlStatus.DataSource = ds;
                    ddlStatus.DataTextField = "Status";
                    ddlStatus.DataValueField = "LecturerStatusID";
                    ddlStatus.DataBind();
                    con.Close();
                }
            }
        }

        protected void LecturerGridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            LecturerGridView.EditIndex = e.NewEditIndex;
            LoadData();
        }

        protected void LecturerGridView_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int LecturerID = int.Parse(LecturerGridView.Rows[e.RowIndex].Cells[0].Text);
            TextBox txtName = LecturerGridView.Rows[e.RowIndex].FindControl("txtName") as TextBox;
            TextBox txtEmail = LecturerGridView.Rows[e.RowIndex].FindControl("txtEmail") as TextBox;
            TextBox txtContactNo = LecturerGridView.Rows[e.RowIndex].FindControl("txtContactNo") as TextBox;
            DropDownList ddlStatus = LecturerGridView.Rows[e.RowIndex].FindControl("ddlStatus") as DropDownList;
            UpdateLecturer(LecturerID, txtName.Text, txtEmail.Text, txtContactNo.Text, int.Parse(ddlStatus.SelectedValue));
            LecturerGridView.EditIndex = -1;
            LoadData();
        }

        protected void UpdateLecturer(int LecturerID, string Name, string Email, string ContactNo, int StatusID)
        {
            SqlConnection con = new SqlConnection(connectionString);
            com = new SqlCommand();
            con.Open();
            com.Connection = con;
            com.CommandText = "LecturerCRUD";
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add(new SqlParameter("@action", SqlDbType.VarChar, 50));
            com.Parameters.Add(new SqlParameter("@LecturerID", SqlDbType.Int));
            com.Parameters.Add(new SqlParameter("@Name", SqlDbType.VarChar, 100));
            com.Parameters.Add(new SqlParameter("@Email", SqlDbType.VarChar, 100));
            com.Parameters.Add(new SqlParameter("@ContactNo", SqlDbType.VarChar, 15));
            com.Parameters.Add(new SqlParameter("@StatusID", SqlDbType.Int));
            com.Parameters["@action"].Value = "Update";
            com.Parameters["@LecturerID"].Value = LecturerID;
            com.Parameters["@Name"].Value = Name;
            com.Parameters["@Email"].Value = Email;
            com.Parameters["@ContactNo"].Value = ContactNo;
            com.Parameters["@StatusID"].Value = StatusID;
            sqlda = new SqlDataAdapter(com);
            ds = new DataSet();
            sqlda.Fill(ds);
            con.Close();
        }

        protected void LecturerGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int LecturerID = int.Parse(LecturerGridView.Rows[e.RowIndex].Cells[0].Text);
            DeleteLecturer(LecturerID);
            LecturerGridView.EditIndex = -1;
            LoadData();
        }

        protected void DeleteLecturer(int LecturerID)
        {
            SqlConnection con = new SqlConnection(connectionString);
            com = new SqlCommand();
            con.Open();
            com.Connection = con;
            com.CommandText = "LecturerCRUD";
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add(new SqlParameter("@action", SqlDbType.VarChar, 50));
            com.Parameters.Add(new SqlParameter("@LecturerID", SqlDbType.Int));
            com.Parameters["@action"].Value = "Delete";
            com.Parameters["@LecturerID"].Value = LecturerID;
            sqlda = new SqlDataAdapter(com);
            ds = new DataSet();
            sqlda.Fill(ds);
            con.Close();
        }

        protected void LecturerGridView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            LecturerGridView.EditIndex = -1;
            LoadData();
        }

        protected void LecturerGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }
    }
}