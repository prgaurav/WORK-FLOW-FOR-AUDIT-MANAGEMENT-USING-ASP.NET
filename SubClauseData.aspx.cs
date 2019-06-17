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
            SqlCommand cmd = new SqlCommand("Select * from SubClause", con);
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
            //LoadClauseDropDownList();

            ListItem selectCompany = new ListItem("Select Company", "-1");
            CompanyDropDown.Items.Insert(0,selectCompany);
            ListItem selectStandard = new ListItem("Select Standard", "-1");
            StandardDropDown.Items.Insert(0, selectStandard);
            ListItem selectClause = new ListItem("Select Clause", "-1");
            ClauseDropDown.Items.Insert(0, selectClause);

            StandardDropDown.Enabled = false;
            ClauseDropDown.Enabled = false;
            //LoadClauseLabel();
        }
    }

    protected void standardButton_Click(object sender, EventArgs e)
    {
        if (inputSubClauseID.Text != "" && inputSubClauseText.Text != "" && CompanyDropDown.SelectedIndex!=0 && StandardDropDown.SelectedIndex!=0 && ClauseDropDown.SelectedIndex!=0)
        {
            try
            {

                string connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
                using (SqlConnection con = new SqlConnection(connectionString))
                {

                    SqlCommand check_SubClause = new SqlCommand("select count(*) from SubClause where ([CompanyName]=@CompanyName) AND ([StandardName]=@StandardName) AND ([ClauseID]=@ClauseID) AND ([SubClauseID]=@SubClauseID)", con);
                    con.Open();
                    check_SubClause.Parameters.AddWithValue("@CompanyName", CompanyDropDown.SelectedItem.Value);
                    check_SubClause.Parameters.AddWithValue("@StandardName", StandardDropDown.SelectedItem.Value);
                    check_SubClause.Parameters.AddWithValue("@ClauseID", ClauseDropDown.SelectedItem.Value);
                    check_SubClause.Parameters.AddWithValue("@SubClauseID", inputSubClauseID.Text);
                    int UserExist = (int)check_SubClause.ExecuteScalar();

                    if (UserExist > 0)
                    {
                        Label3.Text = "This SubClause already exists for selected Company, Standard and Clause";
                        //Username exist
                    }
                    else
                    {
                        //Label1.Text = "NOT Exists";
                        string qry = "INSERT INTO [dbo].[SubClause] ([CompanyName], [StandardName], [ClauseID], [ClauseName], [SubClauseID], [SubClauseName]) VALUES (@CompanyName,@StandardName,@ClauseID,@ClauseName,@SubClauseID,@SubClauseName)";
                        SqlCommand cmd = new SqlCommand(qry, con);
                        cmd.Parameters.AddWithValue("@CompanyName", CompanyDropDown.SelectedItem.Value);
                        cmd.Parameters.AddWithValue("@StandardName", StandardDropDown.SelectedItem.Value);
                        cmd.Parameters.AddWithValue("@ClauseID", ClauseDropDown.SelectedItem.Value);
                        cmd.Parameters.AddWithValue("@ClauseName", ClauseNameLabel.Text);
                        cmd.Parameters.AddWithValue("@SubClauseID", inputSubClauseID.Text);
                        cmd.Parameters.AddWithValue("@SubClauseName", inputSubClauseText.Text);
                        int i = cmd.ExecuteNonQuery();
                        if (i != 0)
                        {
                            Label3.Text = " Your data has been saved in the database";
                            Label3.ForeColor = System.Drawing.Color.ForestGreen;

                            using (SqlConnection con1 = new SqlConnection(connectionString))
                            {
                                SqlCommand cmd1 = new SqlCommand("Select * from SubClause", con1);
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
            Label3.Text = "Some Input is missing";
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
                SqlCommand cmd = new SqlCommand("SELECT * from Standard where ([CompanyName]=@CompanyName)", con);
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
    public void LoadClauseDropDownList()
    {
        try
        {
            string connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * from Clause where ([CompanyName]=@CompanyName) AND ([StandardName]=@StandardName)", con);
                con.Open();
                cmd.Parameters.AddWithValue("@CompanyName", CompanyDropDown.SelectedItem.Value);
                cmd.Parameters.AddWithValue("@StandardName", StandardDropDown.SelectedItem.Value);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);  // fill dataset
                ClauseDropDown.DataTextField = ds.Tables[0].Columns["ClauseID"].ToString(); // text field name of table dispalyed in dropdown
                ClauseDropDown.DataValueField = ds.Tables[0].Columns["ClauseID"].ToString();             // to retrive specific  textfield name 
                ClauseDropDown.DataSource = ds.Tables[0];      //assigning datasource to the dropdownlist
                ClauseDropDown.DataBind();  //binding dropdownlist
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message+" Clause Drop Down");
        }
    }

    public void LoadClauseLabel()
    {
        try
        {
            string connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * from Clause where ([CompanyName]=@CompanyName) AND ([StandardName]=@StandardName) AND ([ClauseID]=@ClauseID)", con);
                con.Open();
                cmd.Parameters.AddWithValue("@CompanyName", CompanyDropDown.SelectedItem.Value);
                cmd.Parameters.AddWithValue("@StandardName", StandardDropDown.SelectedItem.Value);
                cmd.Parameters.AddWithValue("@ClauseID", ClauseDropDown.SelectedItem.Value);
                SqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                ClauseNameLabel.Text = dr["ClauseName"].ToString();
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }

    protected void StandardDropDown_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (StandardDropDown.SelectedIndex == 0)
        {
            ListItem selectClause = new ListItem("Select Clause", "-1");
            ClauseDropDown.Items.Clear();
            ClauseDropDown.Items.Insert(0, selectClause);
            ClauseDropDown.Enabled = false;
            Label3.Text = "Select Standard";
        }
        else
        {
            LoadClauseDropDownList();
            ListItem selectClause = new ListItem("Select Clause", "-1");
            ClauseDropDown.Items.Insert(0, selectClause);
            ClauseDropDown.Enabled = true;
        }
    }

    protected void ClauseDropDown_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ClauseDropDown.SelectedIndex == 0)
        {
            ClauseNameLabel.Text = "Select Clause ID";
        }
        else
        {
            LoadClauseLabel();
        }
    }


    
}