<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AuditPlan2.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContentPlaceHolder" runat="Server">
    <style>
        body {
         background-size: cover;
            background-position: right;
            background-color: rgb(16, 28, 53);

        .mydatagrid {
            width: 80%;
            border: solid 2px black;
            min-width: 80%;
        }

        .header {
            background-color: #000;
            font-family: Arial;
            color: White;
            height: 25px;
            text-align: center;
            font-size: 16px;
        }

        .rows {
            background-color: #fff;
            font-family: Arial;
            font-size: 14px;
            color: #000;
            min-height: 25px;
            text-align: left;
        }

            .rows:hover {
                background-color: #5badff;
                color: #fff;
            }

        .mydatagrid a /** FOR THE PAGING ICONS  **/ {
            background-color: Transparent;
            padding: 5px 5px 5px 5px;
            color: #fff;
            text-decoration: none;
            font-weight: bold;
        }

            .mydatagrid a:hover /** FOR THE PAGING ICONS  HOVER STYLES**/ {
                background-color: #000;
                color: #fff;
            }

        .mydatagrid span /** FOR THE PAGING ICONS CURRENT PAGE INDICATOR **/ {
            background-color: #fff;
            color: #000;
            padding: 5px 5px 5px 5px;
        }

        .pager {
            background-color: #5badff;
            font-family: Arial;
            color: White;
            height: 30px;
            text-align: left;
        }

        .mydatagrid td {
            padding: 5px;
        }

        .mydatagrid th {
            padding: 5px;
        }
    </style>
    <form id="form1" runat="server" style="text-align: left; font-size: 1.5em; color: white; margin-top: 150px; margin-right: 300px;margin-left:250px;">
        <div>
            <br />
            <div class="padding">
                <asp:Label ID="Label1" runat="server" Text="Program ID :  "></asp:Label>
                <asp:Label ID="ProgramIDLabel" runat="server" Text=""></asp:Label>
            </div>
            <br />
            <div class="padding">
                <asp:Label ID="Label2" runat="server" Text="Personal No :  "></asp:Label>
                <asp:Label ID="PersonalNoLabel" runat="server" Text=""></asp:Label>
            </div>
            <br />
            <div class="padding">
                <asp:Label ID="Label4" runat="server" Text="Select Section :  "></asp:Label>
                <asp:DropDownList ID="SectionDropDown" runat="server"></asp:DropDownList>
            </div>
            <br />
            <div class="padding">
                <asp:Label ID="Label6" runat="server" Text="Enter Starting Date :  "></asp:Label>
                <asp:TextBox ID="inputFrom" runat="server" TextMode="Date"></asp:TextBox>
            </div>
            <br />
            <div class="padding">
                <asp:Label ID="Label9" runat="server" Text="Enter Ending Date :  "></asp:Label>
                <asp:TextBox ID="inputTo" runat="server" TextMode="Date"></asp:TextBox>
                <asp:Button padding-left="120px" CssClass="btn btn-primary" ID="standardButton" runat="server" Text="Add" OnClick="standardButton_Click" />
            </div>
            <br />
            
            <div class="padding">
                <asp:Label ID="Label3" runat="server" Text=""></asp:Label>
            </div>

            <div class="padding">
                <asp:Label ID="existingLabel" runat="server" Text="Existing Audit Plans : "></asp:Label>
            </div>
            <div style="margin-left: 100px;margin-top:20px;">
                <asp:GridView ID="GridView1" runat="server"
                    CssClass="mydatagrid" PagerStyle-CssClass="pager"
                    HeaderStyle-CssClass="header" RowStyle-CssClass="rows"
                    Font-Names="Arial"
                    Font-Size="11pt">
                </asp:GridView>
            </div>
        </div>
    </form>
</asp:Content>

