<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ChangePassword.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContentPlaceHolder" runat="Server">
    <style>
        body{
            background-size: cover;
            background-position: right;
            background-color: rgb(16, 28, 53);
        }
    </style>
    <form id="form1" runat="server" style="text-align: left; font-size: 1.5em; color: white; color: white; margin-top: 150px; margin-right: 300px; margin-left: 200px;">
        <br />
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
        <div class="padding">
            <asp:Label ID="Label2" runat="server" Text="Enter Old Password : "></asp:Label>
            <asp:TextBox ID="inputOldPassword" runat="server" TextMode="Password" Style="margin-left: 40px;"></asp:TextBox>
        </div>
        <br />
        <div class="padding">
            <asp:Label ID="Label3" runat="server" Text="Enter New Password : "></asp:Label>
            <asp:TextBox ID="inputNewPassword" runat="server" TextMode="Password" Style="margin-left: 30px;"></asp:TextBox>
        </div>
        <br />
        <div class="padding">
            <asp:Label ID="Label4" runat="server" Text="Reenter New Password : "></asp:Label>
            <asp:TextBox ID="inputReNewPassword" runat="server" TextMode="Password" Style="margin-left: 5px;"></asp:TextBox>

        </div>
        <br />
        <div class="padding" style="margin-top: 20px;">
            <asp:Button CssClass="btn btn-primary" ID="companyButton" runat="server" Text="Change Password" OnClick="companyButton_Click" />
        </div>
        <br />
        <div class="padding" style="font-size: 0.7em; color: red;">
            <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
        </div>
        <br />
        <br />
                    </ContentTemplate>
            </asp:UpdatePanel>
    </form>
</asp:Content>

