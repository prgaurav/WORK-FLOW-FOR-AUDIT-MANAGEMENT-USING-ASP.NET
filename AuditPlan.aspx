<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AuditPlan.aspx.cs" Inherits="_Default" %>

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
    <form id="form1" runat="server" style="text-align: center; font-size: 1.5em; color: white; margin-top: 150px; margin-left: 280px;">
        <div>
            <asp:GridView ID="GridView1" runat="server"
                CssClass="mydatagrid" PagerStyle-CssClass="pager"
                HeaderStyle-CssClass="header" RowStyle-CssClass="rows"
                AutoGenerateColumns="false" Font-Names="Arial"
                OnPageIndexChanging="OnPaging"
                Font-Size="11pt" 
                AllowPaging="true">
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:RadioButton ID="RadioButton1" runat="server"
                                onclick="RadioCheck(this);" />
                            <asp:HiddenField ID="HiddenField1" runat="server"
                                Value='<%#Eval("PID")%>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField ItemStyle-Width="150px" DataField="PID"
                        HeaderText="Program ID" />
                    <asp:BoundField ItemStyle-Width="150px" DataField="PersonalNo"
                        HeaderText="Personal No" />
                    <asp:BoundField ItemStyle-Width="150px" DataField="CompanyName"
                        HeaderText="Company Name" />
                    <asp:BoundField ItemStyle-Width="150px" DataField="DepartmentName"
                        HeaderText="Department Name" />
                    <asp:BoundField ItemStyle-Width="150px" DataField="ClauseID"
                        HeaderText="Clause ID" />
                    <asp:BoundField ItemStyle-Width="150px" DataField="SubClauseID"
                        HeaderText="SubClause ID" />
                    <asp:BoundField ItemStyle-Width="150px" DataField="From"
                        HeaderText="From" />
                    <asp:BoundField ItemStyle-Width="150px" DataField="To"
                        HeaderText="To" />

                </Columns>
            </asp:GridView>
        </div>
        <div style="margin-top:50px;margin-left:450px;text-align:left;">
            <asp:Button CssClass="btn btn-primary" ID="Button1" runat="server" Text="Plan Audit" OnClick="standardButton_Click" />
        </div>
        <div style="margin-top:200px">
            <asp:Label ID="Label3" runat="server" Text=""></asp:Label>
        </div>

    </form>

    <script type="text/javascript">

        function RadioCheck(rb) {

            var gv = document.getElementById("<%=GridView1.ClientID%>");

            var rbs = gv.getElementsByTagName("input");



            var row = rb.parentNode.parentNode;

            for (var i = 0; i < rbs.length; i++) {

                if (rbs[i].type == "radio") {

                    if (rbs[i].checked && rbs[i] != rb) {

                        rbs[i].checked = false;

                        break;

                    }

                }

            }

        }

    </script>
</asp:Content>

