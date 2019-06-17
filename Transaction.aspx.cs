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
            SqlCommand cmd = new SqlCommand("Select * from AuditProgram", con);
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
            CompanyDropDown.Items.Insert(0, selectCompany);
            ListItem selectDepartment = new ListItem("Select Department", "-1");
            DepartmentDropDown.Items.Insert(0, selectDepartment);
            ListItem selectStandard = new ListItem("Select Standard", "-1");
            StandardDropDown.Items.Insert(0, selectStandard);
            ListItem selectClause = new ListItem("Select Clause", "-1");
            ClauseDropDown.Items.Insert(0, selectClause);
            ListItem selectSubClause = new ListItem("Select SubClause", "-1");
            SubClauseDropDown.Items.Insert(0, selectSubClause);
            StandardDropDown.Enabled = false;
            ClauseDropDown.Enabled = false;
            SubClauseDropDown.Enabled = false;
            DepartmentDropDown.Enabled = false;
            //LoadClauseLabel();
        }
    }

    protected void standardButton_Click(object sender, EventArgs e)
    {
        if (inputFrom.Text != "" && inputTo.Text != "" && inputPersonalNo.Text != "" && CompanyDropDown.SelectedIndex != 0 && StandardDropDown.SelectedIndex != 0 && ClauseDropDown.SelectedIndex != 0 && DepartmentDropDown.SelectedIndex != 0 && SubClauseDropDown.SelectedIndex != 0)
        {
            int result = DateTime.Compare(Convert.ToDateTime(inputFrom.Text), Convert.ToDateTime(inputTo.Text));
            if (result < 0)
            {

                try
                {

                    string connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {

                        SqlCommand check_AuditProgram = new SqlCommand("select count(*) from AuditProgram where ([PersonalNo]=@PersonalNo) AND ([CompanyName]=@CompanyName) AND ([StandardName]=@StandardName) AND ([ClauseID]=@ClauseID) AND ([SubClauseID]=@SubClauseID) AND ([DepartmentName]=@DepartmentName) ", con);
                        con.Open();
                        check_AuditProgram.Parameters.AddWithValue("@CompanyName", CompanyDropDown.SelectedItem.Value);
                        check_AuditProgram.Parameters.AddWithValue("@StandardName", StandardDropDown.SelectedItem.Value);
                        check_AuditProgram.Parameters.AddWithValue("@ClauseID", ClauseDropDown.SelectedItem.Value);
                        check_AuditProgram.Parameters.AddWithValue("@SubClauseID", SubClauseDropDown.SelectedItem.Value);
                        check_AuditProgram.Parameters.AddWithValue("@DepartmentName", DepartmentDropDown.SelectedItem.Value);
                        check_AuditProgram.Parameters.AddWithValue("@PersonalNo", inputPersonalNo.Text);
                        int UserExist = (int)check_AuditProgram.ExecuteScalar();

                        if (UserExist > 0)
                        {
                            Label3.Text = "This Audit Program already exists for selected Company, Standard, Clause, SubClause and Department";
                            //Username exist
                        }
                        else
                        {
                            //Label1.Text = "NOT Exists";
                            string qry = "INSERT INTO [dbo].[AuditProgram] ([PersonalNo], [CompanyName], [StandardName], [ClauseID], [SubClauseID], [From], [To], [DepartmentName]) VALUES (@PersonalNo, @CompanyName, @StandardName, @ClauseID, @SubClauseID, @From, @To, @DepartmentName)";
                            SqlCommand cmd = new SqlCommand(qry, con);
                            cmd.Parameters.AddWithValue("@PersonalNo", inputPersonalNo.Text);
                            cmd.Parameters.AddWithValue("@CompanyName", CompanyDropDown.SelectedItem.Value);
                            cmd.Parameters.AddWithValue("@StandardName", StandardDropDown.SelectedItem.Value);
                            cmd.Parameters.AddWithValue("@ClauseID", ClauseDropDown.SelectedItem.Value);
                            cmd.Parameters.AddWithValue("@SubClauseID", SubClauseDropDown.SelectedItem.Value);
                            cmd.Parameters.AddWithValue("@From", inputFrom.Text);
                            cmd.Parameters.AddWithValue("@To", inputTo.Text);
                            cmd.Parameters.AddWithValue("@DepartmentName", DepartmentDropDown.SelectedItem.Value);
                            int i = cmd.ExecuteNonQuery();
                            if (i != 0)
                            {
                                Label3.Text = " Your data has been saved in the database";
                                Label3.ForeColor = System.Drawing.Color.ForestGreen;

                                using (SqlConnection con1 = new SqlConnection(connectionString))
                                {
                                    SqlCommand cmd1 = new SqlCommand("Select * from AuditProgram", con1);
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
                Label3.Text = "Starting Date cannot be less than or equal to Ending Date";
            }
        }
        else
        {
            Label3.Text = "Some Input is missing";
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
            ListItem selectDepartment = new ListItem("Select Department", "-1");
            DepartmentDropDown.Items.Clear();
            DepartmentDropDown.Items.Insert(0, selectStandard);
            DepartmentDropDown.Enabled = false;
            ListItem selectClause = new ListItem("Select Clause", "-1");
            ClauseDropDown.Items.Clear();
            ClauseDropDown.Items.Insert(0, selectClause);
            ClauseDropDown.Enabled = false;
            ListItem selectSubClause = new ListItem("Select SubClause", "-1");
            SubClauseDropDown.Items.Clear();
            SubClauseDropDown.Items.Insert(0, selectSubClause);
            SubClauseDropDown.Enabled = false;
            //Label3.Text = "Select Company";

        }
        else
        {
            LoadStandardDropDownList();
            LoadDepartmentDropDownList();
            ListItem selectStandard = new ListItem("Select Standard", "-1");
            StandardDropDown.Items.Insert(0, selectStandard);
            StandardDropDown.Enabled = true;
            ListItem selectDepartment = new ListItem("Select Department", "-1");
            DepartmentDropDown.Items.Insert(0, selectDepartment);
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
    public void LoadDepartmentDropDownList()
    {
        try
        {
            string connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * from Department where ([CompanyName]=@CompanyName)", con);
                con.Open();
                cmd.Parameters.AddWithValue("@CompanyName", CompanyDropDown.SelectedItem.Value);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);  // fill dataset
                DepartmentDropDown.DataTextField = ds.Tables[0].Columns["DepartmentName"].ToString(); // text field name of table dispalyed in dropdown
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
            Response.Write(ex.Message + " Clause Drop Down");
        }
    }

    public void LoadSubClauseDropDownList()
    {
        try
        {
            string connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * from SubClause where ([CompanyName]=@CompanyName) AND ([StandardName]=@StandardName) AND ([ClauseID]=@ClauseID)", con);
                con.Open();
                cmd.Parameters.AddWithValue("@CompanyName", CompanyDropDown.SelectedItem.Value);
                cmd.Parameters.AddWithValue("@StandardName", StandardDropDown.SelectedItem.Value);
                cmd.Parameters.AddWithValue("@ClauseID", ClauseDropDown.SelectedItem.Value);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);  // fill dataset
                SubClauseDropDown.DataTextField = ds.Tables[0].Columns["SubClauseID"].ToString(); // text field name of table dispalyed in dropdown
                SubClauseDropDown.DataValueField = ds.Tables[0].Columns["SubClauseID"].ToString();             // to retrive specific  textfield name 
                SubClauseDropDown.DataSource = ds.Tables[0];      //assigning datasource to the dropdownlist
                SubClauseDropDown.DataBind();  //binding dropdownlist
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message + " SubClause Drop Down");
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
            ListItem selectSubClause = new ListItem("Select SubClause", "-1");
            SubClauseDropDown.Items.Clear();
            SubClauseDropDown.Items.Insert(0, selectSubClause);
            SubClauseDropDown.Enabled = false;
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
            ListItem selectSubClauseID = new ListItem("Select SubClause", "-1");
            SubClauseDropDown.Items.Clear();
            SubClauseDropDown.Items.Insert(0, selectSubClauseID);
            SubClauseDropDown.Enabled = false;
            Label3.Text = "Select Clause";
        }
        else
        {
            LoadSubClauseDropDownList();
            ListItem selectSubClauseID = new ListItem("Select SubClause", "-1");
            SubClauseDropDown.Items.Insert(0, selectSubClauseID);
            SubClauseDropDown.Enabled = true;
        }
    }

   



}