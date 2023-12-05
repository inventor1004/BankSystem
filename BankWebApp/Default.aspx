<%@ Page Title="Home Page" Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="BankSystemSQ._Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login-page</title>
    <link href="./css/login-page.css" rel="stylesheet" />
</head>
    <body>
        <form id="identityCheckForm" runat="server">
            <div>
                <img src="./css/bank-logo.png" class="logo-image" />
                <h1>Welcome To Simple Bank</h1>
            </div>
            <div>
                <asp:TextBox ID="userID" runat="server" CssClass="id-textbox" placeholder="ID"></asp:TextBox>
                
            </div>
            <div>
                <asp:TextBox ID="userPW" runat="server" TextMode="Password" CssClass="pw-textbox" placeholder="Password"></asp:TextBox>
            </div>
            <div>
                <asp:Button ID="loginButton" runat="server" CssClass="login-button" Text="Login" OnClick ="loginButton_Click" />
                <asp:Button ID="signupButton" runat="server" CssClass="login-button" Text="Signup" OnClick ="signupButton_Click" CausesValidation ="false"/>
            </div>
            <div style="color: red"><asp:RequiredFieldValidator ID="UserNameValidator" runat="server" ErrorMessage="User name <b>cannot</b> be BLANK." ControlToValidate ="userID"></asp:RequiredFieldValidator></div>
            <div style="color: red"><asp:RequiredFieldValidator ID="PasswordValidator" runat="server" ErrorMessage="Password <b>cannot</b> be BLANK." ControlToValidate ="userPW"></asp:RequiredFieldValidator></div>

       </form>
    </body>
</html>

