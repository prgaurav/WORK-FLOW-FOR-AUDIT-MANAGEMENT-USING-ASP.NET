using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        GetSelectedRecord();
        BindGrid();
    }

    private void BindGrid()
    {
        string strQuery = "select * from AuditProgram where ([PersonalNo]=@PersonalNo)";
        DataTable dt = new DataTable();
        string connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
        SqlConnection con = new SqlConnection(connectionString);
        SqlDataAdapter sda = new SqlDataAdapter();
        SqlCommand cmd = new SqlCommand(strQuery);
        cmd.Parameters.AddWithValue("@PersonalNo", Session["UID"]);
        cmd.CommandType = CommandType.Text;
        cmd.Connection = con;
        try
        {
            con.Open();
            sda.SelectCommand = cmd;
            sda.Fill(dt);
            if (dt.Rows.Count == 0)             //-------------Check whether gridview is empty-------------------
            {
                Button1.Enabled = false;
                Button1.Visible = false;
                Label3.Text = "No Audit Program exists for logged in user";
            }
            else
            {
                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            con.Close();
            sda.Dispose();
            con.Dispose();
        }
    }

    private void GetSelectedRecord()
    {
        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            RadioButton rb = (RadioButton)GridView1.Rows[i]
                            .Cells[0].FindControl("RadioButton1");
            if (rb != null)
            {
                if (rb.Checked)
                {
                    HiddenField hf = (HiddenField)GridView1.Rows[i]
                                    .Cells[0].FindControl("HiddenField1");
                    if (hf != null)
                    {
                        Session["ProgramID"] = hf.Value;
                        Session["RadioChecked"] = "true";
                    }

                    break;
                }
            }
        }
    }
    private void SetSelectedRecord()
    {
        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            RadioButton rb = (RadioButton)GridView1.Rows[i].Cells[0]
                                            .FindControl("RadioButton1");
            if (rb != null)
            {
                HiddenField hf = (HiddenField)GridView1.Rows[i]
                                    .Cells[0].FindControl("HiddenField1");
                if (hf != null && ViewState["SelectedContact"] != null)
                {
                    if (hf.Value.Equals(ViewState["SelectedContact"].ToString()))
                    {
                        rb.Checked = true;
                        break;
                    }
                }
            }
        }
    }
    protected void OnPaging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        GridView1.DataBind();
        SetSelectedRecord();
    }

    protected void standardButton_Click(object sender, EventArgs e)
    {
        if (Session["RadioChecked"] != null && Session["RadioChecked"].ToString() == "true")
        {
            Response.Redirect("AuditPlan2.aspx");
        }
        else
        {
            Label3.Text = "Select an Audit Program !";
        }

    }
}