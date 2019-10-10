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
    public partial class BatchList : System.Web.UI.Page
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
            com.CommandText = "BatchCRUD";
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add(new SqlParameter("@action", SqlDbType.VarChar, 50));
            com.Parameters["@action"].Value = "Read";
            sqlda = new SqlDataAdapter(com);
            ds = new DataSet();
            sqlda.Fill(ds);
            BatchGridView.DataSource = ds;
            BatchGridView.DataBind();
            con.Close();
        }

        protected void BatchGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }


        protected void BatchGridView_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int BatchID = int.Parse(BatchGridView.Rows[e.RowIndex].Cells[0].Text);
            TextBox txtYear = BatchGridView.Rows[e.RowIndex].FindControl("txtYear") as TextBox;
            UpdateBatch(BatchID, int.Parse(txtYear.Text));
            BatchGridView.EditIndex = -1;
            LoadData();
        }

        protected void UpdateBatch(int BatchID, int Year)
        {
            SqlConnection con = new SqlConnection(connectionString);
            com = new SqlCommand();
            con.Open();
            com.Connection = con;
            com.CommandText = "BatchCRUD";
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add(new SqlParameter("@action", SqlDbType.VarChar, 50));
            com.Parameters.Add(new SqlParameter("@BatchID", SqlDbType.Int));
            com.Parameters.Add(new SqlParameter("@Year", SqlDbType.Int));
            com.Parameters["@action"].Value = "Update";
            com.Parameters["@BatchID"].Value = BatchID;
            com.Parameters["@Year"].Value = Year;
            sqlda = new SqlDataAdapter(com);
            ds = new DataSet();
            sqlda.Fill(ds);
            con.Close();
        }

        protected void BatchGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int BatchID = int.Parse(BatchGridView.Rows[e.RowIndex].Cells[0].Text);
            DeleteBatch(BatchID);
            BatchGridView.EditIndex = -1;
            LoadData();
        }

        protected void DeleteBatch(int BatchID)
        {
            SqlConnection con = new SqlConnection(connectionString);
            com = new SqlCommand();
            con.Open();
            com.Connection = con;
            com.CommandText = "BatchCRUD";
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add(new SqlParameter("@action", SqlDbType.VarChar, 50));
            com.Parameters.Add(new SqlParameter("@BatchID", SqlDbType.Int));
            com.Parameters["@action"].Value = "Delete";
            com.Parameters["@BatchID"].Value = BatchID;
            sqlda = new SqlDataAdapter(com);
            ds = new DataSet();
            sqlda.Fill(ds);
            con.Close();
        }

        protected void BatchGridView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            BatchGridView.EditIndex = -1;
            LoadData();
        }

        protected void BatchGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void BatchGridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            BatchGridView.EditIndex = e.NewEditIndex;
            LoadData();
        }
    }
}