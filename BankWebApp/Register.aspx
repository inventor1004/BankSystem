<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="BankSystemSQ.Registration" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Sign Up Page</title>
    <link href="./css/login-page.css" rel="stylesheet" />
    <script type="text/javascript">
        function togglePasswordVisibility() {
            var passwordTextBox = document.getElementById('<%= Password.ClientID %>');
            passwordTextBox.type = (passwordTextBox.type === "password") ? "text" : "password";
        }
    </script>
</head>
<body>
    <form id="signUpForm" runat="server">
        <div>
            <img src="./css/bank-logo.png" class="logo-image" />
            <br />       
        </div>

        <div class="sign-up-inputs">
            <asp:TextBox  ID="ID"              runat="server" placeholder="ID"/>
            <br /> 
            <asp:TextBox  ID="Password"        runat="server" placeholder="Password" TextMode="Password"/>
            <br /> 
            <asp:TextBox  ID="reenterPassword" runat="server" TextMode="Password" placeholder="Re-enter Password"/>           
            <br /> 
            <asp:TextBox  ID="FirstName"       runat="server" placeholder="First Name"/>
            <br /> 
            <asp:TextBox  ID="LasName"         runat="server" placeholder="Last Name"/>
            <br /> 
            <asp:Calendar ID="DateOfBirth"     runat="server" placeholder="Date Of Birth" />
            <br /> 
            <asp:TextBox  ID="Address"         runat="server" placeholder="Address"/>
            <br /> 
            <asp:TextBox  ID="PhoneNumber"     runat="server" placeholder="Phone Number" type="number"/>
        </div>


        <div>
            <input type="checkbox" id="showPassword" onclick="togglePasswordVisibility()" />
            <label>Show Password</label>
        </div>
        <div>
            <asp:RequiredFieldValidator ID="usernameValidator" runat="server" ControlToValidate="ID"
            ErrorMessage="Username is required." ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
             
        </div>
        <div>
            <asp:RequiredFieldValidator ID="passwordValidator" runat="server" ControlToValidate="Password"
            ErrorMessage="Password is required." ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="passwordFormatValidator" runat="server" ControlToValidate="password"
            ErrorMessage="Password must contain 8 characters: at least 1 number, 1 uppercase letter, and 1 special character."
            ValidationExpression="^(?=.*\d)(?=.*[A-Z])(?=.*[^a-zA-Z0-9]).{8,}$" ForeColor="Red" Display="Dynamic">
            </asp:RegularExpressionValidator>
        </div>
        <div>
            <asp:RequiredFieldValidator ID="reenterPasswordValidator" runat="server" ControlToValidate="reenterPassword"
            ErrorMessage="Re-entering the password is required." ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
            <asp:CompareValidator ID="passwordMatchValidator" runat="server" ControlToCompare="password" ControlToValidate="reenterPassword"
            ErrorMessage="Passwords do not match." ForeColor="Red" Display="Dynamic"></asp:CompareValidator>
        </div>
        <div>
            <asp:Button ID="signUpButton" runat="server" Text="Sign Up" OnClick="signUpButton_Click" />
        </div>
    </form>
</body>
</html>
