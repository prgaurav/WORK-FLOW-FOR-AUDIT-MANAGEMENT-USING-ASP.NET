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

    }

    protected void companyButton_Click(object sender, EventArgs e)
    {
        try
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            string oldPassword = inputOldPassword.Text;
            string newPassword = inputNewPassword.Text;
            string newRePassword = inputReNewPassword.Text;
            string password;

            if (newPassword.Equals(newRePassword))
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("SELECT * from loginTable where ([PersonalNo]=@PersonalNo)", con);
                    con.Open();
                    cmd.Parameters.AddWithValue("@PersonalNo", Session["UID"]);
                    SqlDataReader dr = cmd.ExecuteReader();
                    dr.Read();
                    password = dr["Password"].ToString();
                }

                if (oldPassword.Equals(password))
                {
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        con.Open();
                        string qry = "UPDATE loginTable SET [Password]=@Password WHERE [PersonalNo]=@PersonalNo";
                        SqlCommand cmd = new SqlCommand(qry, con);
                        cmd.Parameters.AddWithValue("@PersonalNo", Session["UID"]);
                        cmd.Parameters.AddWithValue("@Password", newPassword);
                        int i = cmd.ExecuteNonQuery();
                        if (i != 0)
                        {
                            Label1.Text = "Password Changed Successfully";
                            //Response.Redirect("LoginPage.aspx");
                        }
                        else
                        {
                            Label1.Text = "Error occured in database";

                        }
                        //Username doesn't exist.
                    }
                }
                else
                {
                    Label1.Text = "Incorrect Old Password";
                }
            }


            else
            {
                Label1.Text = "Password doesnot match in both fields!";
            }

        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }
}