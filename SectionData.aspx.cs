using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
        existingLabel.Visible = true;
        if (!IsPostBack)
        {
            LoadCompanyDropDownList();
            //LoadDepartmentDropDownList();
            ListItem selectCompany = new ListItem("Select Company", "-1");
            CompanyDropDown.Items.Insert(0, selectCompany);
            ListItem selectDepartment = new ListItem("Select Department", "-1");
            DepartmentDropDown.Items.Insert(0, selectDepartment);

            DepartmentDropDown.Enabled = false;
        }

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            SqlCommand cmd = new SqlCommand("Select * from Section", con);
            con.Open();
            SqlDataReader rdr = cmd.ExecuteReader();
            if (rdr.HasRows)
            {
                GridView1.DataSource = rdr;
                GridView1.DataBind();
            }
            else
            {
                existingLabel.Visible = false;

            }
        }
    }

    protected void standardButton_Click(object sender, EventArgs e)
    {
        if (inputSection.Text != "" && CompanyDropDown.SelectedIndex != 0 && DepartmentDropDown.SelectedIndex != 0)
        {
            try
            {

                string connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
                using (SqlConnection con = new SqlConnection(connectionString))
                {

                    SqlCommand check_Clause = new SqlCommand("select count(*) from Section where ([CompanyName]=@CompanyName) AND ([DepartmentName]=@DepartmentName) AND ([SectionName]=@SectionName)", con);
                    con.Open();
                    check_Clause.Parameters.AddWithValue("@CompanyName", CompanyDropDown.SelectedItem.Value);
                    check_Clause.Parameters.AddWithValue("@DepartmentName", DepartmentDropDown.SelectedItem.Value);
                    check_Clause.Parameters.AddWithValue("@SectionName", inputSection.Text);
                    int UserExist = (int)check_Clause.ExecuteScalar();

                    if (UserExist > 0)
                    {
                        Label3.Text = "Error! This Section already exists for selected Company and Department";
                        //Username exist
                    }
                    else
                    {
                        //Label1.Text = "NOT Exists";
                        string qry = "INSERT INTO [dbo].[Section] ([CompanyName], [DepartmentName], [SectionName]) VALUES (@CompanyName,@DepartmentName,@SectionName)";
                        SqlCommand cmd = new SqlCommand(qry, con);
                        cmd.Parameters.AddWithValue("@CompanyName", CompanyDropDown.SelectedItem.Value);
                        cmd.Parameters.AddWithValue("@DepartmentName", DepartmentDropDown.SelectedItem.Value);
                        cmd.Parameters.AddWithValue("@SectionName", inputSection.Text);
                        
                        int i = cmd.ExecuteNonQuery();
                        if (i != 0)
                        {
                            Label3.Text = " Your data has been saved in the database";
                            Label3.ForeColor = System.Drawing.Color.ForestGreen;

                            using (SqlConnection con1 = new SqlConnection(connectionString))
                            {
                                SqlCommand cmd1 = new SqlCommand("Select * from Section", con1);
                                con1.Open();
                                SqlDataReader rdr = cmd1.ExecuteReader();
                                GridView1.DataSource = rdr;
                                GridView1.DataBind();
                            }

                        }
                        else
                        {
                            Label3.Text = "Error occured in database";
                            Label3.ForeColor = System.Drawing.Color.Red;

                        }
                        //Username doesn't exist.
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }
        else
        {
            Label3.Text = "Some input is missing";
        }

    }

    protected void CompanyDropDown_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (CompanyDropDown.SelectedIndex == 0)
        {
            ListItem selectDepartment = new ListItem("Select Department", "-1");
            
            DepartmentDropDown.Items.Clear();
            DepartmentDropDown.Items.Insert(0, selectDepartment);
            DepartmentDropDown.Enabled = false;
            Label3.Text = "Select Company";
        }
        else
        {
            LoadDepartmentDropDownList();
            ListItem selectStandard = new ListItem("Select Standard", "-1");
            DepartmentDropDown.Items.Insert(0, selectStandard);
            DepartmentDropDown.Enabled = true;
        }
    }


    public void LoadCompanyDropDownList()
    {
        try
        {
            string connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * from Company", con);
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);  // fill dataset
                CompanyDropDown.DataTextField = ds.Tables[0].Columns["CompanyName"].ToString(); // text field name of table dispalyed in dropdown
                CompanyDropDown.DataValueField = ds.Tables[0].Columns["CompanyName"].ToString();             // to retrive specific  textfield name 
                CompanyDropDown.DataSource = ds.Tables[0];      //assigning datasource to the dropdownlist
                CompanyDropDown.DataBind();  //binding dropdownlist
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }

    public void LoadDepartmentDropDownList()
    {
        try
        {
            string connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT DepartmentName from Department where ([CompanyName]=@CompanyName)", con);
                con.Open();
                cmd.Parameters.AddWithValue("@CompanyName", CompanyDropDown.SelectedItem.Value);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);  // fill dataset
                DepartmentDropDown.DataValueField = ds.Tables[0].Columns["DepartmentName"].ToString();             // to retrive specific  textfield name 
                DepartmentDropDown.DataSource = ds.Tables[0];      //assigning datasource to the dropdownlist
                DepartmentDropDown.DataBind();  //binding dropdownlist
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }
}