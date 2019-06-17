using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;

public partial class LoginPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //string connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
    }

    protected void loginButton_Click(object sender, EventArgs e)
    {
        //Label4.Text = "Button CLicked";
        try
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            string uid = inputEID.Value;
            string pass = inputPassword.Value;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string qry = "select * from loginTable where PersonalNo='" + uid + "' and Password='" + pass + "'";
                SqlCommand cmd = new SqlCommand(qry, con);
                con.Open();
                SqlDataReader sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    Label4.Text = "Login Sucessful!";

                    Session["UID"] = uid;
                    Session["UserName"] = sdr["Name"];

                    Response.Redirect("Home.aspx?EID="+sdr["Name"].ToString());
                    
                    //Session.RemoveAll();
                }
                else
                {
                    Label4.Text = "Employee ID & Password Is not correct Try again..!!";

                }
                con.Close();
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }
    }