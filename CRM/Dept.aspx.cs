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
    public partial class Dept : System.Web.UI.Page
    {
        string Connection = "Data Source=PREMPDHAKAL\\MSSQLSERVER2022; initial catalog=CoreMvcDB; User Id=sa; password=12345;";
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMsg.Text = "";
            if (!IsPostBack)
            {
                BindReport();
            }
        }
        public void BindReport()
        {
            try
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
            catch(Exception ex)
            {

            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection(Connection);
                con.Open();
                string query = "Insert into department values('" + txtDepartmentName.Text + "','" + txtDescription.Text + "')";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.CommandType = CommandType.Text;
                //SqlCommand cmd = new SqlCommand("Insert_Department", con);
                //cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
                ClearText();
                BindReport();
                lblMsg.Text = "Save Successfully";
            }
            catch(Exception ex)
            {

            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtId.Text == "")
                {
                    lblMsg.Text = "Please enter the dept Id";
                    return;
                }
                SqlConnection con = new SqlConnection(Connection);
                con.Open();
                string query = "update department set D_Name='" + txtDepartmentName.Text + "',D_Description='" + txtDescription.Text + "' where D_Id='" + txtId.Text + "'";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.CommandType = CommandType.Text;
                //SqlCommand cmd = new SqlCommand("Insert_Department", con);
                //cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
                BindReport();
                ClearText();
                lblMsg.Text = "Update Successfully";
            }
            catch(Exception ex)
            {

            }
        }

        protected void btnEdits_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtId.Text == "")
                {
                    lblMsg.Text = "Please enter the dept Id";
                    return;
                }
                SqlConnection con = new SqlConnection(Connection);
                con.Open();
                string query = "select *from department where D_Id='" + txtId.Text + "'";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    txtDepartmentName.Text = dt.Rows[0]["D_Name"].ToString();
                    txtDescription.Text = dt.Rows[0]["D_Description"].ToString();
                }
            }
            catch(Exception ex)
            {

            }
        }
        protected void gvReport_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvReport.PageIndex = e.NewPageIndex;
            BindReport();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection(Connection);
                con.Open();
                string query = "delete from department where D_Id='" + txtId.Text + "'";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                BindReport();
                ClearText();
                lblMsg.Text = "Delete Successfully";
            }
            catch(Exception ex)
            {

            }
        }
        public void ClearText()
        {
            txtId.Text = string.Empty;
            txtDepartmentName.Text = "";
            txtDescription.Text = "";
        }
    }
}