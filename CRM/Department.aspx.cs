using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
namespace CRM
{
    public partial class Department : System.Web.UI.Page
    {
        string Connection = "Data Source=TECH; initial catalog=CRM; User Id=sa; password=tech333;";
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                BindDdlDepartment();
                BindReport();
            }

        }
        public void BindReport()
        {
            SqlConnection con = new SqlConnection(Connection);
            con.Open();
            string query = "select *from department order by D_Id desc";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            adp.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                gvReport.DataSource = dt;
                gvReport.DataBind();
            }
        }
        public void BindDdlDepartment()
        {
            SqlConnection con = new SqlConnection(Connection);
            con.Open();
            string query = "select *from department order by D_Id desc";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            adp.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                ddlDepartment.DataSource = dt;
                ddlDepartment.DataValueField = "D_Id";
                ddlDepartment.DataTextField = "D_Name";
                ddlDepartment.DataBind();
                //gvReport.DataSource = dt;
                //gvReport.DataBind();
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string D_Id = ddlDepartment.SelectedValue;
            //SqlConnection con = new SqlConnection(Connection);
            //con.Open();
            //string query = "Insert into department values('" + txtDepartmentName.Text + "','" + txtDescription.Text + "')";
            //SqlCommand cmd = new SqlCommand(query, con);
            //cmd.CommandType = CommandType.Text;
            ////SqlCommand cmd = new SqlCommand("Insert_Department", con);
            ////cmd.CommandType = CommandType.StoredProcedure;
            //cmd.ExecuteNonQuery();
            //BindReport();

        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (ViewState["D_Id"].ToString() != "")
            {
                SqlConnection con = new SqlConnection(Connection);
                con.Open();
                string query = "update department set D_Name='" + txtDepartmentName.Text + "',D_Description='" + txtDescription.Text + "' where D_Id='" + ViewState["D_Id"].ToString() + "'";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                BindReport();
            }
        }
        
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            ViewState["D_Id"] = row.Cells[0].Text;
            txtDepartmentName.Text = row.Cells[1].Text;
            txtDescription.Text = row.Cells[2].Text;

        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            if (ViewState["D_Id"].ToString() != "")
            {
                SqlConnection con = new SqlConnection(Connection);
                con.Open();
                string query = "delete from department where D_Id='" + ViewState["D_Id"].ToString() + "'";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                BindReport();
            }
        }

        protected void gvReport_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvReport.PageIndex = e.NewPageIndex;
            BindReport();
        }

      
    }
}