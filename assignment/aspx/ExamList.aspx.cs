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
    public partial class ExamList : System.Web.UI.Page
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
            com.CommandText = "ExamCRUD";
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add(new SqlParameter("@action", SqlDbType.VarChar, 50));
            com.Parameters["@action"].Value = "Read";
            sqlda = new SqlDataAdapter(com);
            ds = new DataSet();
            sqlda.Fill(ds);
            ExamGridView.DataSource = ds;
            ExamGridView.DataBind();
            con.Close();
        }

        protected void ExamGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView drvExam = e.Row.DataItem as DataRowView;

                DropDownList ddlSubject = e.Row.FindControl("ddlSubject") as DropDownList;
                if (ddlSubject != null)
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
                    ddlSubject.SelectedValue = drvExam["SubjectID"].ToString();
                }
            }
        }

        protected void ExamGridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            ExamGridView.EditIndex = e.NewEditIndex;
            LoadData();
        }

        protected void ExamGridView_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int ExamID = int.Parse(ExamGridView.Rows[e.RowIndex].Cells[0].Text);
            TextBox txtDescription = ExamGridView.Rows[e.RowIndex].FindControl("txtDescription") as TextBox;
            DropDownList ddlSubject = ExamGridView.Rows[e.RowIndex].FindControl("ddlSubject") as DropDownList;
            TextBox txtDate = ExamGridView.Rows[e.RowIndex].FindControl("txtDate") as TextBox;
            TextBox txtTime = ExamGridView.Rows[e.RowIndex].FindControl("txtTime") as TextBox;
            UpdateExam(ExamID, txtDescription.Text, int.Parse(ddlSubject.SelectedValue), Convert.ToDateTime(txtDate.Text), Convert.ToDateTime(txtTime.Text));
            ExamGridView.EditIndex = -1;
            LoadData();
        }

        protected void UpdateExam(int ExamID, string Description, int SubjectID, DateTime Date, DateTime Time)
        {
            SqlConnection con = new SqlConnection(connectionString);
            com = new SqlCommand();
            con.Open();
            com.Connection = con;
            com.CommandText = "ExamCRUD";
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add(new SqlParameter("@action", SqlDbType.VarChar, 50));
            com.Parameters.Add(new SqlParameter("@ExamID", SqlDbType.Int));
            com.Parameters.Add(new SqlParameter("@Description", SqlDbType.VarChar, 100));
            com.Parameters.Add(new SqlParameter("@SubjectID", SqlDbType.Int));
            com.Parameters.Add(new SqlParameter("@Date", SqlDbType.DateTime));
            com.Parameters.Add(new SqlParameter("@Time", SqlDbType.DateTime));
            com.Parameters["@action"].Value = "Update";
            com.Parameters["@ExamID"].Value = ExamID;
            com.Parameters["@Description"].Value = Description;
            com.Parameters["@SubjectID"].Value = SubjectID;
            com.Parameters["@Date"].Value = Date;
            com.Parameters["@Time"].Value = Time;
            com.ExecuteNonQuery();
            con.Close();
        }

        protected void ExamGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int ExamID = int.Parse(ExamGridView.Rows[e.RowIndex].Cells[0].Text);
            DeleteExam(ExamID);
            ExamGridView.EditIndex = -1;
            LoadData();
        }

        protected void DeleteExam(int ExamID)
        {
            SqlConnection con = new SqlConnection(connectionString);
            com = new SqlCommand();
            con.Open();
            com.Connection = con;
            com.CommandText = "ExamCRUD";
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add(new SqlParameter("@action", SqlDbType.VarChar, 50));
            com.Parameters.Add(new SqlParameter("@ExamID", SqlDbType.Int));
            com.Parameters["@action"].Value = "Delete";
            com.Parameters["@ExamID"].Value = ExamID;
            sqlda = new SqlDataAdapter(com);
            ds = new DataSet();
            sqlda.Fill(ds);
            con.Close();
        }

        protected void ExamGridView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            ExamGridView.EditIndex = -1;
            LoadData();
        }

        protected void ExamGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }
    }
}