using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace assignment.aspx
{
    public partial class StudentForm : System.Web.UI.Page
    {
        string connectionString = ConfigurationManager.ConnectionStrings["assignmentConnectionString"].ConnectionString;
        SqlCommand com;
        SqlDataAdapter sqlda;
        DataSet ds;
        Boolean success = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            DdlInit();
            if (Request.QueryString["action"] == "add")
            {
                h1Title.InnerText = "Add a Student";
            }
            else if (Request.QueryString["action"] == "edit" && Request.QueryString["StudentID"] != null)
            {
                h1Title.InnerText = "Edit Student";
                LoadStudentResultData(int.Parse(Request.QueryString["StudentID"]));
                edit.Visible = true;
            }
            else
            {
                Response.Redirect("");
            }


            if (Request.QueryString["add"] == "success")
            {
                success = true;
                cardSuccess.Visible = success;
                success = false;
            }
        }

        protected void DdlInit()
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
        }

        protected void AddStudent(string Name, string Email, string ContactNo, int BatchID, int StatusID)
        {
            SqlConnection con = new SqlConnection(connectionString);
            com = new SqlCommand();
            con.Open();
            com.Connection = con;
            com.CommandText = "StudentCRUD";
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add(new SqlParameter("@action", SqlDbType.VarChar, 50));
            com.Parameters.Add(new SqlParameter("@Name", SqlDbType.VarChar, 100));
            com.Parameters.Add(new SqlParameter("@Email", SqlDbType.VarChar, 100));
            com.Parameters.Add(new SqlParameter("@ContactNo", SqlDbType.VarChar, 15));
            com.Parameters.Add(new SqlParameter("@BatchID", SqlDbType.Int));
            com.Parameters.Add(new SqlParameter("@StatusID", SqlDbType.Int));
            com.Parameters["@action"].Value = "Create";
            com.Parameters["@Name"].Value = Name;
            com.Parameters["@Email"].Value = Email;
            com.Parameters["@ContactNo"].Value = ContactNo;
            com.Parameters["@BatchID"].Value = BatchID;
            com.Parameters["@StatusID"].Value = StatusID;
            sqlda = new SqlDataAdapter(com);
            ds = new DataSet();
            sqlda.Fill(ds);
            con.Close();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            AddStudent(txtName.Text, txtEmail.Text, txtContactNo.Text, int.Parse(ddlBatch.SelectedValue), int.Parse(ddlStatus.SelectedValue));
            Response.Redirect("StudentForm.aspx?action=add");
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            Response.Redirect("StudentForm.aspx?action=add");
        }

        protected void LoadStudentData(int StudentID)
        {
            SqlConnection con = new SqlConnection(connectionString);
            com = new SqlCommand();
            con.Open();
            com.Connection = con;
            com.CommandText = "StudentCRUD";
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add(new SqlParameter("@action", SqlDbType.VarChar, 50));
            com.Parameters.Add(new SqlParameter("@StudentID", SqlDbType.Int));
            com.Parameters["@action"].Value = "ReadOne";
            com.Parameters["@StudentID"].Value = StudentID;
            sqlda = new SqlDataAdapter(com);
            ds = new DataSet();
            sqlda.Fill(ds);
            StudentResultGridView.DataSource = ds;
            StudentResultGridView.DataBind();
            con.Close();
        }

        protected void LoadStudentResultData(int StudentID)
        {
            SqlConnection con = new SqlConnection(connectionString);
            com = new SqlCommand();
            con.Open();
            com.Connection = con;
            com.CommandText = "ReadStudentResultInfo";
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add(new SqlParameter("@StudentID", SqlDbType.Int));
            com.Parameters["@StudentID"].Value = StudentID;
            sqlda = new SqlDataAdapter(com);
            ds = new DataSet();
            sqlda.Fill(ds);
            StudentResultGridView.DataSource = ds;
            StudentResultGridView.DataBind();
            con.Close();
        }

        protected void LoadStudentClassData(int StudentID)
        {
            SqlConnection con = new SqlConnection(connectionString);
            com = new SqlCommand();
            con.Open();
            com.Connection = con;
            com.CommandText = "ReadStudentClass";
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add(new SqlParameter("@StudentID", SqlDbType.Int));
            com.Parameters["@StudentID"].Value = StudentID;
            sqlda = new SqlDataAdapter(com);
            ds = new DataSet();
            sqlda.Fill(ds);
            StudentResultGridView.DataSource = ds;
            StudentResultGridView.DataBind();
            con.Close();
        }

        protected void StudentResultGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void StudentResultGridView_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }

        protected void StudentResultGridView_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

        }

        protected void StudentResultGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void StudentResultGridView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {

        }

        protected void StudentResultGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }
    }
}