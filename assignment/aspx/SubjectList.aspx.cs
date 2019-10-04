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
            UpdateSubject(SubjectID, txtName.Text, txtDescription.Text);
            SubjectGridView.EditIndex = -1;
            LoadData();
        }

        protected void UpdateSubject(int SubjectID, string Name, string Description)
        {
            SqlConnection con = new SqlConnection(connectionString);
            com = new SqlCommand();
            con.Open();
            com.Connection = con;
            com.CommandText = "StudentCRUD";
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add(new SqlParameter("@action", SqlDbType.VarChar, 50));
            com.Parameters.Add(new SqlParameter("@SubjectID", SqlDbType.Int));
            com.Parameters.Add(new SqlParameter("@Name", SqlDbType.VarChar, 100));
            com.Parameters.Add(new SqlParameter("@Description", SqlDbType.VarChar, 100));
            com.Parameters["@action"].Value = "Update";
            com.Parameters["@SubjectID"].Value = SubjectID;
            com.Parameters["@Name"].Value = Name;
            com.Parameters["@Description"].Value = Description;
            sqlda = new SqlDataAdapter(com);
            ds = new DataSet();
            sqlda.Fill(ds);
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