using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //string username = Request.QueryString["EID"];
        string username = Session["UserName"].ToString();
        userLabel.InnerText = "Welcome "+username;
        username1.InnerHtml = "Welcome " + username;
    }
}
