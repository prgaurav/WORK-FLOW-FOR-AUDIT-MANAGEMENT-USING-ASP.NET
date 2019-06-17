<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RegistrationPage.aspx.cs" Inherits="RegistrationPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="RegistrationPage.css" rel="stylesheet" />
    <link href="bootstrap.min.css" rel="stylesheet" id="bootstrap_css" />
    <script src="bootstrap.min.js"></script>
    <script src="jquery.min.js"></script>
    <title></title>
</head>
<body>

    <div class="container-fluid">
        <div class="row no-gutter">
            <div class="d-none d-md-flex col-md-4 col-lg-6 bg-image"></div>
            <div class="col-md-8 col-lg-6">
                <div class="login d-flex align-items-center py-5">
                    <div class="container">
                        <div class="row">
                            <div class="col-md-9 col-lg-8 mx-auto">
                                <h3 class="login-heading mb-4">Sign Up</h3>
                                <form id="form1" runat="server">
                                    <div class="form-label-group">
                                        <input type="text" id="inputEID" class="form-control" placeholder="Employee ID" runat="server" required autofocus />
                                        <label for="inputEID">Employee ID</label>
                                    </div>

                                    <div class="form-label-group">
                                        <input type="text" id="inputName" class="form-control" placeholder="Name" runat="server" required autofocus />
                                        <label for="inputName">Name</label>
                                        
                                    </div>

                                    <div class="form-label-group">
                                        <input type="password" id="inputPassword" class="form-control" placeholder="Password" runat="server" required />
                                        <label for="inputPassword">Password</label>
                                    </div>

                                    <div class="form-label-group">
                                        <input type="password" id="inputConfirmPassword" class="form-control" placeholder="Reenter Password" runat="server" required />
                                        <label for="inputConfirmPassword">Reenter Password</label>
                                    </div>

                                    

                                    <button id="registerButton" onserverclick="registerButton_Click" class="btn btn-lg btn-primary btn-block btn-login text-uppercase font-weight-bold mb-2" type="submit" runat="server">Sign Up</button>
                                    <br />
                                    <div class="text-center">
                                        <a class="medium" href="LoginPage.aspx">Sign In</a>
                                    </div>
                                    <br />
                                    <asp:Label class="custom-control" ID="Label4" runat="server" Text=""></asp:Label>
                                </form>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>



</body>
</html>
