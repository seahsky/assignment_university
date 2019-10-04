using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace assignment.aspx
{
    public partial class ExamForm : System.Web.UI.Page
    {
        string connectionString = ConfigurationManager.ConnectionStrings["assignmentConnectionString"].ConnectionString;
        SqlCommand com;
        SqlDataAdapter sqlda;
        DataSet ds;
        Boolean success = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            DdlInit();
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
            com.CommandText = "DDLSubject";
            com.CommandType = CommandType.StoredProcedure;
            sqlda = new SqlDataAdapter(com);
            ds = new DataSet();
            sqlda.Fill(ds);
            ddlSubject.DataSource = ds;
            ddlSubject.DataTextField = "Subject";
            ddlSubject.DataValueField = "SubjectID";
            ddlSubject.DataBind();
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
        }

        protected void AddExam(string Description, int SubjectID, int BatchID)
        {
            SqlConnection con = new SqlConnection(connectionString);
            com = new SqlCommand();
            con.Open();
            com.Connection = con;
            com.CommandText = "ExamCRUD";
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add(new SqlParameter("@action", SqlDbType.VarChar, 50));
            com.Parameters.Add(new SqlParameter("@Description", SqlDbType.VarChar, 100));
            com.Parameters.Add(new SqlParameter("@SubjectID", SqlDbType.Int));
            com.Parameters.Add(new SqlParameter("@BatchID", SqlDbType.Int));
            com.Parameters["@action"].Value = "Create";
            com.Parameters["@Description"].Value = Description;
            com.Parameters["@SubjectID"].Value = SubjectID;
            com.Parameters["@BatchID"].Value = BatchID;
            sqlda = new SqlDataAdapter(com);
            ds = new DataSet();
            sqlda.Fill(ds);
            con.Close();
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            Response.Redirect("ExamForm.aspx");
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            AddExam(txtDescription.Text, int.Parse(ddlSubject.SelectedValue), int.Parse(ddlBatch.SelectedValue));
            Response.Redirect("ExamForm.aspx?add=success");
        }
    }
}