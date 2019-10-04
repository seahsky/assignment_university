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
    public partial class ClassForm : System.Web.UI.Page
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

        protected void AddClass(string Name, string Description, int BatchID, int LecturerID)
        {
            SqlConnection con = new SqlConnection(connectionString);
            com = new SqlCommand();
            con.Open();
            com.Connection = con;
            com.CommandText = "ClassCRUD";
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add(new SqlParameter("@action", SqlDbType.VarChar, 50));
            com.Parameters.Add(new SqlParameter("@Name", SqlDbType.VarChar, 100));
            com.Parameters.Add(new SqlParameter("@Description", SqlDbType.VarChar, 100));
            com.Parameters.Add(new SqlParameter("@BatchID", SqlDbType.Int));
            com.Parameters.Add(new SqlParameter("@LecturerID", SqlDbType.Int));
            com.Parameters["@action"].Value = "Create";
            com.Parameters["@Name"].Value = Name;
            com.Parameters["@Description"].Value = Description;
            com.Parameters["@LecturerID"].Value = LecturerID;
            com.Parameters["@BatchID"].Value = BatchID;
            sqlda = new SqlDataAdapter(com);
            ds = new DataSet();
            sqlda.Fill(ds);
            con.Close();
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            Response.Redirect("ClassForm.aspx");
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            AddClass(txtName.Text, txtDescription.Text, int.Parse(ddlBatch.SelectedValue), int.Parse(ddlLecturer.SelectedValue));
            Response.Redirect("ClassForm.aspx?add=success");
        }
    }
}