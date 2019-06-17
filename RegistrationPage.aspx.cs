using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;


public partial class RegistrationPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void registerButton_Click(object sender, EventArgs e)
    {
        //Label4.Text = "Button CLicked";
        try
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            string eid = inputEID.Value;
            string pass = inputPassword.Value;
            string name = inputName.Value;
            string confirmpass = inputConfirmPassword.Value;
            if (pass.Equals(confirmpass))
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {

                    SqlCommand check_EID = new SqlCommand("select count(*) from loginTable where ([PersonalNo]=@PersonalNo)", con);
                    con.Open();
                    check_EID.Parameters.AddWithValue("@PersonalNo", eid);
                    int UserExist = (int)check_EID.ExecuteScalar();

                    if (UserExist > 0)
                    {
                        Label4.Text = "This Employee ID has already been registered. Please Sign in.";
                        inputEID.Value = "";
                        inputName.Value = "";
                        //Username exist
                    }
                    else
                    {
                        //Label1.Text = "NOT Exists";
                        string qry = "INSERT INTO [dbo].[loginTable] ([PersonalNo], [Name], [Password]) VALUES (@PersonalNo,@Name,@Password)";
                        SqlCommand cmd = new SqlCommand(qry, con);
                        cmd.Parameters.AddWithValue("@PersonalNo", eid);
                        cmd.Parameters.AddWithValue("@Name", name);
                        cmd.Parameters.AddWithValue("@Password", pass);
                        int i = cmd.ExecuteNonQuery();
                        if (i != 0)
                        {
                            Label4.Text = " Registration Successful";
                            inputEID.Value = "";
                            inputName.Value = "";

                            //Response.Redirect("LoginPage.aspx");
                        }
                        else
                        {
                            Label4.Text = "Error occured in database";

                        }
                        //Username doesn't exist.
                    }
                }
            }

            else
            {
                Label4.Text = "Password doesnot match in both fields!";
            }

        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }
}