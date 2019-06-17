<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Transaction.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContentPlaceHolder" runat="Server">
    <style>
        body {
            background-size: cover;
            background-position: right;
            background-color: rgb(16, 28, 53);
        }

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
    <form id="form1" runat="server" style="text-align: left; font-size: 1.5em; color: white; margin-top: 150px; margin-right: 300px; margin-left: 250px;">
        <div class="padding">
            <br />
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div>
                        <asp:Label ID="Label1" runat="server" Text="Select Company : "></asp:Label>
                        <asp:DropDownList ID="CompanyDropDown" runat="server" AutoPostBack="True" OnSelectedIndexChanged="CompanyDropDown_SelectedIndexChanged" Style="margin-left: 40px;"></asp:DropDownList>
                    </div>
                    <br />
                    <div>
                        <asp:Label ID="Label8" runat="server" Text="Select Department : "></asp:Label>
                        <asp:DropDownList ID="DepartmentDropDown" runat="server" AutoPostBack="True" Style="margin-left: 10px;"></asp:DropDownList>
                    </div>
                    <br />
                    <div>
                        <asp:Label ID="Label2" runat="server" Text="Select Standard : "></asp:Label>
                        <asp:DropDownList ID="StandardDropDown" runat="server" AutoPostBack="True" OnSelectedIndexChanged="StandardDropDown_SelectedIndexChanged" Style="margin-left: 40px;"></asp:DropDownList>
                    </div>
                    <br />
                    <div>
                        <asp:Label ID="Label4" runat="server" Text="Select Clause ID : "></asp:Label>
                        <asp:DropDownList ID="ClauseDropDown" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ClauseDropDown_SelectedIndexChanged" Style="margin-left: 40px;"></asp:DropDownList>
                    </div>

                    <br />
                    <div>
                        <asp:Label ID="Label5" runat="server" Text="Select SubClause ID : "></asp:Label>
                        <asp:DropDownList ID="SubClauseDropDown" runat="server" AutoPostBack="True"></asp:DropDownList>
                    </div>
                 </ContentTemplate>
            </asp:UpdatePanel>
            <br />
            <div>
                <asp:Label ID="Label7" runat="server" Text="Enter PersonalNo. : "></asp:Label>
                <asp:TextBox ID="inputPersonalNo" runat="server" Style="margin-left: 15px;"></asp:TextBox>
            </div>
            <br />
            <div>
                <asp:Label ID="Label6" runat="server" Text="Enter Starting Date : "></asp:Label>
                <asp:TextBox ID="inputFrom" runat="server" TextMode="Date"></asp:TextBox>
            </div>
            <br />
            <div>
                <asp:Label ID="Label9" runat="server" Text="Enter Ending Date : "></asp:Label>
                <asp:TextBox ID="inputTo" runat="server" TextMode="Date" Style="margin-left: 10px;"></asp:TextBox>&nbsp
                    <asp:Button padding-left="120px" CssClass="btn btn-primary" ID="standardButton" runat="server" Text="Add" OnClick="standardButton_Click" />
            </div>
                    
            <br />
            <div>
                <asp:Label ID="Label3" runat="server" Text="" Style="font-size: 0.7em; color: red;"></asp:Label>
            </div>
       
        <br />
        <div >
            <asp:Label ID="existingLabel" runat="server" Text="Existing Audit Programs : "></asp:Label>
        </div>
            </div>
        <br />
        <div style="margin-left: 100px;">
            <asp:GridView ID="GridView1" runat="server"
                CssClass="mydatagrid" PagerStyle-CssClass="pager"
                HeaderStyle-CssClass="header" RowStyle-CssClass="rows"
                Font-Size="11pt" Font-Names="Arial">
            </asp:GridView>
        </div>

    </form>
</asp:Content>

