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

        int StudentID;
        int ClassStudentID;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DdlInit();
            }
            if (Request.QueryString["action"] == "add")
            {
                h1Title.InnerText = "Add a Student";
            }
            else if (Request.QueryString["action"] == "edit" && Request.QueryString["StudentID"] != null)
            {
                h1Title.InnerText = "Edit Student";
                StudentID = int.Parse(Request.QueryString["StudentID"]);
                LoadStudentData(StudentID);
                LoadClassStudentData(StudentID);
                LoadStudentResultData(StudentID);
                edit.Visible = true;
                addBtns.Visible = false;
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
            txtName.Text = ds.Tables[0].Rows[0]["Name"].ToString();
            txtEmail.Text = ds.Tables[0].Rows[0]["Email"].ToString();
            txtContactNo.Text = ds.Tables[0].Rows[0]["ContactNo"].ToString();
            ddlBatch.SelectedValue = ds.Tables[0].Rows[0]["BatchID"].ToString();
            ddlStatus.SelectedValue = ds.Tables[0].Rows[0]["StatusID"].ToString();
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

        protected void LoadClassSubjectData(int ClassID)
        {
            SqlConnection con = new SqlConnection(connectionString);
            com = new SqlCommand();
            con.Open();
            com.Connection = con;
            com.CommandText = "ReadClassSubject";
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add(new SqlParameter("@ClassID", SqlDbType.Int));
            com.Parameters["@ClassID"].Value = ClassID;
            sqlda = new SqlDataAdapter(com);
            ds = new DataSet();
            sqlda.Fill(ds);
            ClassSubjectGridView.DataSource = ds;
            ClassSubjectGridView.DataBind();
            con.Close();
        }

        protected void LoadClassStudentData(int StudentID)
        {
            SqlConnection con = new SqlConnection(connectionString);
            com = new SqlCommand();
            con.Open();
            com.Connection = con;
            com.CommandText = "ReadClassStudent";
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add(new SqlParameter("@StudentID", SqlDbType.Int));
            com.Parameters["@StudentID"].Value = StudentID;
            sqlda = new SqlDataAdapter(com);
            ds = new DataSet();
            sqlda.Fill(ds);
            ClassStudentID = int.Parse(ds.Tables[0].Rows[0]["ClassStudentID"].ToString());
            txtClassName.Text = ds.Tables[0].Rows[0]["Name"].ToString();
            txtClassDescription.Text = ds.Tables[0].Rows[0]["Description"].ToString();
            txtLecturerInCharge.Text = ds.Tables[0].Rows[0]["LecturerInCharge"].ToString();
            txtClassBatch.Text = ds.Tables[0].Rows[0]["Batch"].ToString();
            ddlClass.SelectedValue = ds.Tables[0].Rows[0]["ClassID"].ToString();
            con.Close();
            int ClassID = int.Parse(ds.Tables[0].Rows[0]["ClassID"].ToString());
            LoadClassSubjectData(ClassID);
        }

        protected void UpdateClassStudentData(int ClassStudentID, int ClassID)
        {
            SqlConnection con = new SqlConnection(connectionString);
            com = new SqlCommand();
            con.Open();
            com.Connection = con;
            com.CommandText = "UpdateClassStudent";
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add(new SqlParameter("@ClassStudentID", SqlDbType.Int));
            com.Parameters.Add(new SqlParameter("@ClassID", SqlDbType.Int));
            com.Parameters["@ClassStudentID"].Value = ClassStudentID;
            com.Parameters["@ClassID"].Value = ClassID;
            sqlda = new SqlDataAdapter(com);
            ds = new DataSet();
            sqlda.Fill(ds);
            con.Close();
        }

        protected void UpdateStudent(int StudentID, string Name, string Email, string ContactNo, int BatchID, int StatusID)
        {
            SqlConnection con = new SqlConnection(connectionString);
            com = new SqlCommand();
            con.Open();
            com.Connection = con;
            com.CommandText = "StudentCRUD";
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add(new SqlParameter("@action", SqlDbType.VarChar, 50));
            com.Parameters.Add(new SqlParameter("@StudentID", SqlDbType.Int));
            com.Parameters.Add(new SqlParameter("@Name", SqlDbType.VarChar, 100));
            com.Parameters.Add(new SqlParameter("@Email", SqlDbType.VarChar, 100));
            com.Parameters.Add(new SqlParameter("@ContactNo", SqlDbType.VarChar, 15));
            com.Parameters.Add(new SqlParameter("@BatchID", SqlDbType.Int));
            com.Parameters.Add(new SqlParameter("@StatusID", SqlDbType.Int));
            com.Parameters["@action"].Value = "Update";
            com.Parameters["@StudentID"].Value = StudentID;
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

        protected void ClassSubjectGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void ClassSubjectGridView_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }

        protected void ClassSubjectGridView_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

        }

        protected void ClassSubjectGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void ClassSubjectGridView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {

        }

        protected void ClassSubjectGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("StudentList.aspx");
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            UpdateStudent(StudentID, txtName.Text, txtEmail.Text, txtContactNo.Text, int.Parse(ddlBatch.SelectedValue), int.Parse(ddlStatus.SelectedValue));
            UpdateClassStudentData(ClassStudentID, int.Parse(ddlClass.SelectedValue));
            Response.Redirect(Request.Url.ToString());
        }
    }
}