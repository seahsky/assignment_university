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
            if (!IsPostBack)
            {
                DdlInit();
            }
            if (Request.QueryString["action"] == "add")
            {
                h1Title.InnerText = "Add a Student";
            }
            else if (Request.QueryString["action"] == "view" && Request.QueryString["StudentID"] != null)
            {
                h1Title.InnerText = "View Student Details";
                int StudentID = int.Parse(Request.QueryString["StudentID"]);
                int ClassID = GetStudentCurrentClass(StudentID);
                LoadStudent(StudentID);
                LoadClassStudent(StudentID);
                LoadClassSubject(ClassID);
                LoadStudentResult(StudentID);
                view.Visible = true;
                addBtns.Visible = false;
                txtName.Enabled = false;
                txtEmail.Enabled = false;
                txtContactNo.Enabled = false;
                ddlBatch.Enabled = false;
                ddlStatus.Enabled = false;
                ddlClass.Enabled = false;
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

        protected void AddStudent(string Name, string Email, string ContactNo, int BatchID, int StatusID, int ClassID)
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
            com.Parameters.Add(new SqlParameter("@ClassID", SqlDbType.Int));
            com.Parameters["@action"].Value = "Create";
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

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            AddStudent(txtName.Text, txtEmail.Text, txtContactNo.Text, int.Parse(ddlBatch.SelectedValue), int.Parse(ddlStatus.SelectedValue), int.Parse(ddlClass.SelectedValue));
            Response.Redirect("StudentForm.aspx?action=add");
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            Response.Redirect("StudentForm.aspx?action=add");
        }

        protected void LoadStudent(int StudentID)
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
            ddlClass.SelectedValue = ds.Tables[0].Rows[0]["ClassID"].ToString();
            con.Close();
        }

        protected void LoadClassStudent(int StudentID)
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
            ClassStudentGridView.DataSource = ds;
            ClassStudentGridView.DataBind();
            con.Close();
        }

        protected void LoadClassSubject(int ClassID)
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

        protected void LoadStudentResult(int StudentID)
        {
            SqlConnection con = new SqlConnection(connectionString);
            com = new SqlCommand();
            con.Open();
            com.Connection = con;
            com.CommandText = "ReadStudentResult";
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

        protected int GetStudentCurrentClass(int StudentID)
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
            return int.Parse(ds.Tables[0].Rows[0]["ClassID"].ToString());
        }
    }
}