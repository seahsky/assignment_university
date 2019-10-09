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

        protected void ExamSubjectGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void ExamSubjectGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string ClassSubjectID = DataBinder.Eval(e.Row.DataItem, "ClassSubjectID").ToString();
                string Location = ResolveUrl("ExamSelectList.aspx") + "?ClassSubjectID=" + ClassSubjectID;
                e.Row.Attributes["onclick"] = string.Format("javascript:window.location='{0}';", Location);
                e.Row.Style["cursor"] = "pointer";
            }
        }
    }
}