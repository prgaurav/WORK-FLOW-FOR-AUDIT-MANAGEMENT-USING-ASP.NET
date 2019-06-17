using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
        existingLabel.Visible = true;

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            SqlCommand cmd = new SqlCommand("Select * from Company", con);
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

    protected void companyButton_Click(object sender, EventArgs e)
    {
        if (inputCompany.Text != "")
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
                using (SqlConnection con = new SqlConnection(connectionString))
                {

                    SqlCommand check_User_Name = new SqlCommand("SELECT COUNT(*) FROM [Company] WHERE ([CompanyName] = @CompanyName)", con);
                    con.Open();
                    check_User_Name.Parameters.AddWithValue("@CompanyName", inputCompany.Text);
                    int UserExist = (int)check_User_Name.ExecuteScalar();

                    if (UserExist > 0)
                    {
                        Label1.Text = "Error! Company already exists in the database";
                        //Username exist
                    }
                    else
                    {
                        //Label1.Text = "NOT Exists";
                        string qry = "INSERT INTO [dbo].[Company] ([CompanyName]) VALUES (@company)";
                        SqlCommand cmd = new SqlCommand(qry, con);
                        cmd.Parameters.AddWithValue("company", inputCompany.Text);
                        int i = cmd.ExecuteNonQuery();
                        if (i != 0)
                        {
                            Label1.Text = " Your data has been saved in the database";
                            Label1.ForeColor = System.Drawing.Color.ForestGreen;

                            using (SqlConnection con1 = new SqlConnection(connectionString))
                            {
                                SqlCommand cmd1 = new SqlCommand("Select * from Company", con1);
                                con1.Open();
                                SqlDataReader rdr = cmd1.ExecuteReader();
                                GridView1.DataSource = rdr;
                                GridView1.DataBind();
                            }

                        }
                        else
                        {
                            Label1.Text = "Error occured in database";
                            Label1.ForeColor = System.Drawing.Color.Red;

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
            Label1.Text = "Enter Company Name";
        }
    }
}