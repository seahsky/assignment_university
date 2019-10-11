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
    public partial class ExamSubjectList : System.Web.UI.Page
    {
        string connectionString = ConfigurationManager.ConnectionStrings["assignmentConnectionString"].ConnectionString;
        SqlCommand com;
        SqlDataAdapter sqlda;
        DataSet ds;

        Boolean hasSubject = true;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["ClassID"] != null)
                {
                    int ClassID = int.Parse(Request.QueryString["ClassID"]);
                    hasSubject = LoadClassSubject(ClassID);

                    if (hasSubject == false)
                    {
                        noSubject.Visible = true;
                    }
                    else
                    {
                        noSubject.Visible = false;
                    }
                }
            }
        }

        protected Boolean LoadClassSubject(int ClassID)
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
            ExamSubjectGridView.DataSource = ds;
            ExamSubjectGridView.DataBind();
            con.Close();
            if (ds.Tables[0].Rows.Count < 1)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        protected int LoadOneClassSubject(int ClassSubjectID, string option)
        {
            int[] cs = new int[2];
            SqlConnection con = new SqlConnection(connectionString);
            com = new SqlCommand();
            con.Open();
            com.Connection = con;
            com.CommandText = "ReadOneClassSubject";
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add(new SqlParameter("@ClassSubjectID", SqlDbType.Int));
            com.Parameters["@ClassSubjectID"].Value = ClassSubjectID;
            sqlda = new SqlDataAdapter(com);
            ds = new DataSet();
            sqlda.Fill(ds);
            con.Close();
            cs[0] = int.Parse(ds.Tables[0].Rows[0]["ClassID"].ToString());
            cs[1] = int.Parse(ds.Tables[0].Rows[0]["SubjectID"].ToString());
            if (option == "ClassID")
            {
                return cs[0];
            }
            else if (option == "SubjectID")
            {
                return cs[1];
            }
            else
            {
                return 0;
            }
        }

        protected void UpdateExamSubject(int ClassSubjectID, int ExamID)
        {
            SqlConnection con = new SqlConnection(connectionString);
            com = new SqlCommand();
            con.Open();
            com.Connection = con;
            com.CommandText = "UpdateClassSubjectExam";
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add(new SqlParameter("@ClassSubjectID", SqlDbType.Int));
            com.Parameters.Add(new SqlParameter("@ExamID", SqlDbType.Int));
            com.Parameters["@ClassSubjectID"].Value = ClassSubjectID;
            com.Parameters["@ExamID"].Value = ExamID;
            com.ExecuteNonQuery();
            con.Close();
        }

        protected void ExamSubjectGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
        }

        protected void ExamSubjectGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView drvExamSubject = e.Row.DataItem as DataRowView;

                DropDownList ddlDateTime = e.Row.FindControl("ddlDateTime") as DropDownList;
                if (ddlDateTime != null)
                {
                    int SubjectID = LoadOneClassSubject(int.Parse(drvExamSubject["ClassSubjectID"].ToString()), "SubjectID");
                    SqlConnection con = new SqlConnection(connectionString);
                    com = new SqlCommand();
                    con.Open();
                    com.Connection = con;
                    com.CommandText = "DDLExamSubject";
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Add(new SqlParameter("@SubjectID", SqlDbType.Int));
                    com.Parameters["@SubjectID"].Value = SubjectID;
                    sqlda = new SqlDataAdapter(com);
                    ds = new DataSet();
                    sqlda.Fill(ds);
                    ddlDateTime.DataSource = ds;
                    ddlDateTime.DataTextField = "DateTime";
                    ddlDateTime.DataValueField = "ExamID";
                    ddlDateTime.DataBind();
                    con.Close();
                    ddlDateTime.SelectedValue = drvExamSubject["ExamID"].ToString();
                }
            }
        }

        protected void ExamSubjectGridView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            int ClassID = int.Parse(Request.QueryString["ClassID"]);
            ExamSubjectGridView.EditIndex = -1;
            hasSubject = LoadClassSubject(ClassID);
        }

        protected void ExamSubjectGridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            int ClassID = int.Parse(Request.QueryString["ClassID"]);
            ExamSubjectGridView.EditIndex = e.NewEditIndex;
            hasSubject = LoadClassSubject(ClassID);
        }

        protected void ExamSubjectGridView_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int ClassID = int.Parse(Request.QueryString["ClassID"]);
            int ClassSubjectID = int.Parse(ExamSubjectGridView.Rows[e.RowIndex].Cells[0].Text);
            DropDownList ddlDateTime = ExamSubjectGridView.Rows[e.RowIndex].FindControl("ddlDateTime") as DropDownList;
            UpdateExamSubject(ClassSubjectID, int.Parse(ddlDateTime.SelectedValue));
            ExamSubjectGridView.EditIndex = -1;
            hasSubject = LoadClassSubject(ClassID);
        }
    }
}