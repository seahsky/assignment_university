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
    public partial class ExamResultList : System.Web.UI.Page
    {
        string connectionString = ConfigurationManager.ConnectionStrings["assignmentConnectionString"].ConnectionString;
        SqlCommand com;
        SqlDataAdapter sqlda;
        DataSet ds;

        Boolean hasStudent = true;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["ExamID"] != null)
                {
                    int ExamID = int.Parse(Request.QueryString["ExamID"]);
                    //hasSubject = LoadClassSubject(ClassID);
                    LoadExamResult(ExamID);

                    if (hasStudent == false)
                    {
                        noStudent.Visible = true;
                    }
                    else
                    {
                        noStudent.Visible = false;
                    }
                }
            }
        }

        protected void LoadExamResult(int ExamID)
        {
            SqlConnection con = new SqlConnection(connectionString);
            com = new SqlCommand();
            con.Open();
            com.Connection = con;
            com.CommandText = "ReadExamResult";
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add(new SqlParameter("@ExamID", SqlDbType.Int));
            com.Parameters["@ExamID"].Value = ExamID;
            sqlda = new SqlDataAdapter(com);
            ds = new DataSet();
            sqlda.Fill(ds);
            ExamResultGridView.DataSource = ds;
            ExamResultGridView.DataBind();
            con.Close();
        }

        protected void ExamResultGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            DataRowView drvExamResultSubject = e.Row.DataItem as DataRowView;

            DropDownList ddlGrade = e.Row.FindControl("ddlGrade") as DropDownList;
            if (ddlGrade != null)
            {
                SqlConnection con = new SqlConnection(connectionString);
                com = new SqlCommand();
                con.Open();
                com.Connection = con;
                com.CommandText = "DDLGrade";
                com.CommandType = CommandType.StoredProcedure;
                sqlda = new SqlDataAdapter(com);
                ds = new DataSet();
                sqlda.Fill(ds);
                ddlGrade.DataSource = ds;
                ddlGrade.DataTextField = "Grade";
                ddlGrade.DataValueField = "GradeID";
                ddlGrade.DataBind();
                con.Close();
                ddlGrade.SelectedValue = drvExamResultSubject["GradeID"].ToString();
            }

        }

        protected void InsertOrUpdateExamResult(int StudentID, int ExamID, int Mark, int GradeID)
        {
            SqlConnection con = new SqlConnection(connectionString);
            com = new SqlCommand();
            con.Open();
            com.Connection = con;
            com.CommandText = "InsertOrUpdateExamResult";
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add(new SqlParameter("@StudentID", SqlDbType.Int));
            com.Parameters.Add(new SqlParameter("@ExamID", SqlDbType.Int));
            com.Parameters.Add(new SqlParameter("@Mark", SqlDbType.Int));
            com.Parameters.Add(new SqlParameter("@GradeID", SqlDbType.Int));
            com.Parameters["@StudentID"].Value = StudentID;
            com.Parameters["@ExamID"].Value = ExamID;
            com.Parameters["@Mark"].Value = Mark;
            com.Parameters["@GradeID"].Value = GradeID;
            com.ExecuteNonQuery();
            con.Close();
        }

        protected void ExamResultGridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            int ExamID = int.Parse(Request.QueryString["ExamID"]);
            ExamResultGridView.EditIndex = e.NewEditIndex;
            LoadExamResult(ExamID);
        }

        protected void ExamResultGridView_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int ExamID = int.Parse(Request.QueryString["ExamID"]);
            int StudentID = int.Parse(ExamResultGridView.Rows[e.RowIndex].Cells[0].Text);
            TextBox txtMark = ExamResultGridView.Rows[e.RowIndex].FindControl("txtMark") as TextBox;
            DropDownList ddlGrade = ExamResultGridView.Rows[e.RowIndex].FindControl("ddlGrade") as DropDownList;
            InsertOrUpdateExamResult(StudentID, ExamID, int.Parse(txtMark.Text), int.Parse(ddlGrade.SelectedValue));
            ExamResultGridView.EditIndex = -1;
            LoadExamResult(ExamID);
        }

        protected void ExamResultGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void ExamResultGridView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            int ExamID = int.Parse(Request.QueryString["ExamID"]);
            ExamResultGridView.EditIndex = -1;
            LoadExamResult(ExamID);
        }

        protected void ExamResultGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }
    }
}