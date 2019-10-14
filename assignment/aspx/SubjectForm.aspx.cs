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
            if (!IsPostBack)
            {
                DdlInit();
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
        }

        protected int AddSubject(string Name, string Description)
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
            com.Parameters.Add("@NewSubjectID", SqlDbType.Int).Direction = ParameterDirection.Output;
            com.Parameters["@action"].Value = "Create";
            com.Parameters["@Name"].Value = Name;
            com.Parameters["@Description"].Value = Description;
            com.ExecuteNonQuery();
            int NewSubjectID = int.Parse(com.Parameters["@NewSubjectID"].Value.ToString());
            con.Close();
            return NewSubjectID;
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

        protected void btnReset_Click(object sender, EventArgs e)
        {
            Response.Redirect("SubjectForm.aspx");
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            int SubjectID = AddSubject(txtName.Text, txtDescription.Text);
            InsertOrUpdateLecturerSubject(int.Parse(ddlLecturer.SelectedValue), SubjectID);
            Response.Redirect("SubjectList.aspx");
        }
    }
}