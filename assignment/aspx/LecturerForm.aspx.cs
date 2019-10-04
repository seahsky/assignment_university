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
    public partial class LecturerForm : System.Web.UI.Page
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

        protected void AddLecturer(string Name, string Email, string ContactNo, int StatusID)
        {
            SqlConnection con = new SqlConnection(connectionString);
            com = new SqlCommand();
            con.Open();
            com.Connection = con;
            com.CommandText = "LecturerCRUD";
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add(new SqlParameter("@action", SqlDbType.VarChar, 50));
            com.Parameters.Add(new SqlParameter("@Name", SqlDbType.VarChar, 100));
            com.Parameters.Add(new SqlParameter("@Email", SqlDbType.VarChar, 100));
            com.Parameters.Add(new SqlParameter("@ContactNo", SqlDbType.VarChar, 15));
            com.Parameters.Add(new SqlParameter("@StatusID", SqlDbType.Int));
            com.Parameters["@action"].Value = "Create";
            com.Parameters["@Name"].Value = Name;
            com.Parameters["@Email"].Value = Email;
            com.Parameters["@ContactNo"].Value = ContactNo;
            com.Parameters["@StatusID"].Value = StatusID;
            sqlda = new SqlDataAdapter(com);
            ds = new DataSet();
            sqlda.Fill(ds);
            con.Close();
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            Response.Redirect("LecturerForm.aspx");
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            AddLecturer(txtName.Text, txtEmail.Text, txtContactNo.Text, int.Parse(ddlStatus.SelectedValue));
            Response.Redirect("LecturerForm.aspx?add=success");
        }
    }
}