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
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            SqlCommand cmd = new SqlCommand("Select * from Clause", con);
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
        if (!IsPostBack)
        {
            LoadCompanyDropDownList();
            //LoadStandardDropDownList();
            ListItem selectCompany = new ListItem("Select Company", "-1");
            CompanyDropDown.Items.Insert(0, selectCompany);
            ListItem selectStandard = new ListItem("Select Standard", "-1");
            StandardDropDown.Items.Insert(0, selectStandard);

            StandardDropDown.Enabled = false;
        }
    }

    protected void standardButton_Click(object sender, EventArgs e)
    {
        if (inputClauseID.Text != "" && inputClauseText.Text != "" && CompanyDropDown.SelectedIndex != 0 && StandardDropDown.SelectedIndex != 0)
        {
            try
            {

                string connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
                using (SqlConnection con = new SqlConnection(connectionString))
                {

                    SqlCommand check_Clause = new SqlCommand("select count(*) from Clause where ([CompanyName]=@CompanyName) AND ([StandardName]=@StandardName) AND ([ClauseID]=@ClauseID)", con);
                    con.Open();
                    check_Clause.Parameters.AddWithValue("@CompanyName", CompanyDropDown.SelectedItem.Value);
                    check_Clause.Parameters.AddWithValue("@StandardName", StandardDropDown.SelectedItem.Value);
                    check_Clause.Parameters.AddWithValue("@ClauseID", inputClauseID.Text);
                    int UserExist = (int)check_Clause.ExecuteScalar();

                    if (UserExist > 0)
                    {
                        Label3.Text = "This Clause already exists for selected company and standard";
                        //Username exist
                    }
                    else
                    {
                        //Label1.Text = "NOT Exists";
                        string qry = "INSERT INTO [dbo].[Clause] ([CompanyName], [StandardName], [ClauseID], [ClauseName]) VALUES (@CompanyName,@StandardName,@ClauseID,@ClauseName)";
                        SqlCommand cmd = new SqlCommand(qry, con);
                        cmd.Parameters.AddWithValue("@CompanyName", CompanyDropDown.SelectedItem.Value);
                        cmd.Parameters.AddWithValue("@StandardName", StandardDropDown.SelectedItem.Value);
                        cmd.Parameters.AddWithValue("@ClauseID", inputClauseID.Text);
                        cmd.Parameters.AddWithValue("@ClauseName", inputClauseText.Text);
                        int i = cmd.ExecuteNonQuery();
                        if (i != 0)
                        {
                            Label3.Text = " Your data has been saved in the database";
                            Label3.ForeColor = System.Drawing.Color.ForestGreen;

                            using (SqlConnection con1 = new SqlConnection(connectionString))
                            {
                                SqlCommand cmd1 = new SqlCommand("Select * from Clause", con1);
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
            Label3.Text = "Error! Some input is missing";
            Label3.ForeColor = System.Drawing.Color.Red;
        }

    }

    protected void CompanyDropDown_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (CompanyDropDown.SelectedIndex == 0)
        {
            ListItem selectStandard = new ListItem("Select Standard", "-1");
            StandardDropDown.Items.Clear();
            StandardDropDown.Items.Insert(0, selectStandard);
            StandardDropDown.Enabled = false;
            Label3.Text = "Select Company";
        }
        else
        {
            LoadStandardDropDownList();
            ListItem selectStandard = new ListItem("Select Standard", "-1");
            StandardDropDown.Items.Insert(0, selectStandard);
            StandardDropDown.Enabled = true;
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

    public void LoadStandardDropDownList()
    {
        try
        {
            string connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT StandardName from Standard where ([CompanyName]=@CompanyName)", con);
                con.Open();
                cmd.Parameters.AddWithValue("@CompanyName", CompanyDropDown.SelectedItem.Value);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);  // fill dataset
                StandardDropDown.DataTextField = ds.Tables[0].Columns["StandardName"].ToString(); // text field name of table dispalyed in dropdown
                StandardDropDown.DataValueField = ds.Tables[0].Columns["StandardName"].ToString();             // to retrive specific  textfield name 
                StandardDropDown.DataSource = ds.Tables[0];      //assigning datasource to the dropdownlist
                StandardDropDown.DataBind();  //binding dropdownlist
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }
}