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
    public partial class ExamSelectList : System.Web.UI.Page
    {
        string connectionString = ConfigurationManager.ConnectionStrings["assignmentConnectionString"].ConnectionString;
        SqlCommand com;
        SqlDataAdapter sqlda;
        DataSet ds;

        Boolean hasExam = true;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["ClassSubjectID"] != null)
                {
                    int ClassSubjectID = int.Parse(Request.QueryString["ClassSubjectID"]);
                    int SubjectID = LoadOneClassSubject(ClassSubjectID, "SubjectID");
                    LoadExamBySubjectID(SubjectID);

                    if (SubjectID != 0)
                    {
                        hasExam = true;
                    }

                    if (hasExam == false)
                    {
                        noExam.Visible = true;
                    }
                    else
                    {
                        noExam.Visible = false;
                    }
                }
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

        protected void LoadExamBySubjectID(int SubjectID)
        {
            SqlConnection con = new SqlConnection(connectionString);
            com = new SqlCommand();
            con.Open();
            com.Connection = con;
            com.CommandText = "GetExamBySubjectID";
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add(new SqlParameter("@SubjectID", SqlDbType.Int));
            com.Parameters["@SubjectID"].Value = SubjectID;
            sqlda = new SqlDataAdapter(com);
            ds = new DataSet();
            sqlda.Fill(ds);
            ExamSelectGridView.DataSource = ds;
            ExamSelectGridView.DataBind();
            con.Close();
        }

        protected void ExamSelectGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void ExamSelectGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }
    }
}