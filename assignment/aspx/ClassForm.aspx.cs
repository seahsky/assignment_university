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
        Boolean hasSubject = true;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DdlInit();
                if (Request.QueryString["action"] == "add")
                {
                    h1Title.InnerText = "Add a Class";
                }
                else if (Request.QueryString["action"] == "view" && Request.QueryString["ClassID"] != null)
                {
                    h1Title.InnerText = "View Class Details";
                    int ClassID = int.Parse(Request.QueryString["ClassID"]);
                    LoadClass(ClassID);
                    hasSubject = LoadClassSubject(ClassID);

                    if (hasSubject == false)
                    {
                        noSubject.Visible = true;
                    }
                    else
                    {
                        noSubject.Visible = false;
                    }

                    view.Visible = true;
                    addBtns.Visible = false;
                    txtName.Enabled = false;
                    txtDescription.Enabled = false;
                    ddlBatch.Enabled = false;
                    ddlLecturer.Enabled = false;
                }
                else
                {
                    Response.Redirect("");
                }

                if (Request.QueryString["add"] == "success")
                {
                    success = true;
                    cardSuccess.Visible = success;
                    success = false;
                }
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
        }

        protected string AddClass(string Name, string Description, int BatchID, int LecturerID)
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
            com.Parameters.Add("@NewClassID", SqlDbType.Int).Direction = ParameterDirection.Output;
            com.Parameters["@action"].Value = "Create";
            com.Parameters["@Name"].Value = Name;
            com.Parameters["@Description"].Value = Description;
            com.Parameters["@LecturerID"].Value = LecturerID;
            com.Parameters["@BatchID"].Value = BatchID;
            com.ExecuteNonQuery();
            string NewClassID = com.Parameters["@NewClassID"].Value.ToString();
            con.Close();
            return NewClassID;
        }

        protected void LoadClass(int ClassID)
        {
            SqlConnection con = new SqlConnection(connectionString);
            com = new SqlCommand();
            con.Open();
            com.Connection = con;
            com.CommandText = "ClassCRUD";
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add(new SqlParameter("@action", SqlDbType.VarChar, 50));
            com.Parameters.Add(new SqlParameter("@ClassID", SqlDbType.Int));
            com.Parameters["@action"].Value = "ReadOne";
            com.Parameters["@ClassID"].Value = ClassID;
            sqlda = new SqlDataAdapter(com);
            ds = new DataSet();
            sqlda.Fill(ds);
            txtName.Text = ds.Tables[0].Rows[0]["Name"].ToString();
            txtDescription.Text = ds.Tables[0].Rows[0]["Description"].ToString();
            ddlBatch.SelectedValue = ds.Tables[0].Rows[0]["BatchID"].ToString();
            ddlLecturer.SelectedValue = ds.Tables[0].Rows[0]["LecturerID"].ToString();
            con.Close();
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
            ClassSubjectGridView.DataSource = ds;
            ClassSubjectGridView.DataBind();
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

        protected void AddClassSubject(int SubjectID)
        {
            int ClassID = int.Parse(Request.QueryString["ClassID"]);

            SqlConnection con = new SqlConnection(connectionString);
            com = new SqlCommand();
            con.Open();
            com.Connection = con;
            com.CommandText = "CreateClassSubject";
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add(new SqlParameter("@ClassID", SqlDbType.Int));
            com.Parameters.Add(new SqlParameter("@SubjectID", SqlDbType.Int));
            com.Parameters["@ClassID"].Value = ClassID;
            com.Parameters["@SubjectID"].Value = SubjectID;
            sqlda = new SqlDataAdapter(com);
            ds = new DataSet();
            sqlda.Fill(ds);
            con.Close();
        }

        protected void UpdateClassSubject(int ClassSubjectID, int SubjectID)
        {
            SqlConnection con = new SqlConnection(connectionString);
            com = new SqlCommand();
            con.Open();
            com.Connection = con;
            com.CommandText = "UpdateClassSubject";
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add(new SqlParameter("@ClassSubjectID", SqlDbType.Int));
            com.Parameters.Add(new SqlParameter("@SubjectID", SqlDbType.Int));
            com.Parameters["@ClassSubjectID"].Value = ClassSubjectID;
            com.Parameters["@SubjectID"].Value = SubjectID;
            sqlda = new SqlDataAdapter(com);
            ds = new DataSet();
            sqlda.Fill(ds);
            con.Close();
        }

        protected void DeleteClassSubject(int ClassSubjectID)
        {
            SqlConnection con = new SqlConnection(connectionString);
            com = new SqlCommand();
            con.Open();
            com.Connection = con;
            com.CommandText = "DeleteClassSubject";
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add(new SqlParameter("@ClassSubjectID", SqlDbType.Int));
            com.Parameters["@ClassSubjectID"].Value = ClassSubjectID;
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
            string NewClassID = AddClass(txtName.Text, txtDescription.Text, int.Parse(ddlBatch.SelectedValue), int.Parse(ddlLecturer.SelectedValue));
            Response.Redirect("ClassForm.aspx?action=view&ClassID=" + NewClassID);
        }

        protected void addSubjectBtn_Click(object sender, EventArgs e)
        {
            AddClassSubject(int.Parse(ddlSubject.SelectedValue));
            Response.Redirect(Request.Url.ToString());
        }

        protected void ClassSubjectGridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            int ClassID = int.Parse(Request.QueryString["ClassID"]);
            ClassSubjectGridView.EditIndex = e.NewEditIndex;
            LoadClassSubject(ClassID);
        }

        protected void ClassSubjectGridView_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int ClassID = int.Parse(Request.QueryString["ClassID"]);
            int ClassSubjectID = int.Parse(ClassSubjectGridView.Rows[e.RowIndex].Cells[0].Text);

            DropDownList ddlSubject = ClassSubjectGridView.Rows[e.RowIndex].FindControl("ddlSubject") as DropDownList;

            UpdateClassSubject(ClassSubjectID, int.Parse(ddlSubject.SelectedValue));
            ClassSubjectGridView.EditIndex = -1;
            LoadClassSubject(ClassID);
        }

        protected void ClassSubjectGridView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            int ClassID = int.Parse(Request.QueryString["ClassID"]);
            ClassSubjectGridView.EditIndex = -1;
            LoadClassSubject(ClassID);
        }

        protected void ClassSubjectGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int ClassID = int.Parse(Request.QueryString["ClassID"]);
            int ClassSubjectID = int.Parse(ClassSubjectGridView.Rows[e.RowIndex].Cells[0].Text);
            DeleteClassSubject(ClassSubjectID);
            ClassSubjectGridView.EditIndex = -1;
            LoadClassSubject(ClassID);
        }

        protected void ClassSubjectGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
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
                }
            }
        }

        protected void ClassSubjectGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void manageExamBtn_Click(object sender, EventArgs e)
        {
            string ClassID = Request.QueryString["ClassID"];
            Response.Redirect("ExamSubjectList.aspx?ClassID=" + ClassID);
        }
    }
}