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
    public partial class SubjectForm : System.Web.UI.Page
    {
        string connectionString = ConfigurationManager.ConnectionStrings["assignmentConnectionString"].ConnectionString;
        SqlCommand com;
        SqlDataAdapter sqlda;
        DataSet ds;
        Boolean success = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["add"] == "success")
            {
                success = true;
                cardSuccess.Visible = success;
                success = false;
            }
        }

        protected void AddSubject(string Name, string Description)
        {
            SqlConnection con = new SqlConnection(connectionString);
            com = new SqlCommand();
            con.Open();
            com.Connection = con;
            com.CommandText = "SubjectCRUD";
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add(new SqlParameter("@action", SqlDbType.VarChar, 50));
            com.Parameters.Add(new SqlParameter("@Name", SqlDbType.VarChar, 100));
            com.Parameters.Add(new SqlParameter("@Description", SqlDbType.VarChar, 100));
            com.Parameters["@action"].Value = "Create";
            com.Parameters["@Name"].Value = Name;
            com.Parameters["@Description"].Value = Description;
            sqlda = new SqlDataAdapter(com);
            ds = new DataSet();
            sqlda.Fill(ds);
            con.Close();
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            Response.Redirect("SubjectForm.aspx");
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            AddSubject(txtName.Text, txtDescription.Text);
            Response.Redirect("SubjectForm.aspx?add=success");
        }
    }
}