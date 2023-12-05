<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Mainpage.aspx.cs" Inherits="BankSystemSQ.Mainpage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Banking System</title>
    <link href="./css/mainPage.css" rel="stylesheet" />
</head>
<body>
    <form id="mainPageForm" runat="server">
        <div id="main-page">
            <div id="welcome-section">
                <h1>Welcome, User!</h1>
            </div>

            <div id="accounts">
                <div>
                    <h2>Savings Account</h2>
                    <p class="display-value" id="savings-amount"><asp:Literal ID="savingAmountLiteral" runat="server"></asp:Literal></p>
                </div>
                <div>
                    <h2>Checking Account</h2>
                    <p class="display-value" id="checking-amount"><asp:Literal ID="checkingAmountLiteral" runat="server"></asp:Literal></p>
                </div>
            </div>
            <br/>

            <div id="TransactionSection" style="text-align: center; border: 2px solid #ffffff; border-radius: 10px; background-color:#006703; padding: 20px;">
                <h2>Transaction</h2>
                <asp:DropDownList runat="server" ID="TCaccountList">
                    <asp:ListItem Text="Select an account" Value="" />
                    <asp:ListItem Text="Chequing Account" Value="chequing" />
                    <asp:ListItem Text="Saving Account"   Value="saving" />
                </asp:DropDownList>              
                <asp:TextBox ID="TransactionInput"  runat="server" placeholder="Enter deposit amount" type="number" step="any"></asp:TextBox>
                <asp:Button ID="TCDepositBtn"  runat="server" Text="Deposit" OnClick="TCDepositButton_Click" />
                <asp:Button ID="TCWithdrawButton" runat="server" Text="Withdrawal" OnClick="TCWithdrawButton_Click" />
            </div>
            <br/>

            <div id="TransferSection" style="text-align: center; border: 2px solid #ffffff; background-color:#006703;  border-radius: 10px; padding: 20px;">
                <h2 style="margin-bottom: 0px";>Transfer</h2>
                <div style="display: flex; align-items: center;justify-content: center";>
                    <h3 style="margin-right: 10px; margin-left: 10px";>FROM </h3>
                    <asp:DropDownList runat="server" ID="TSFromAccountList" >
                        <asp:ListItem Text="Select an account" Value="" />
                        <asp:ListItem Text="Chequing Account" Value="chequing" />
                        <asp:ListItem Text="Saving Account"   Value="saving" />
                    </asp:DropDownList>
                    <h3 style="margin-right: 10px; margin-left: 10px";> To </h3>
                     <asp:DropDownList runat="server" ID="TSToAccountList">
                         <asp:ListItem Text="Select an account" Value="" />
                         <asp:ListItem Text="Chequing Account" Value="chequing" />
                         <asp:ListItem Text="Saving Account"   Value="saving" />
                     </asp:DropDownList>
                </div>
                <asp:TextBox ID="TransferInput"  runat="server" placeholder="Enter deposit amount" type="number" step="any"></asp:TextBox>
                <asp:Button ID="TFDepositBtn"  runat="server" Text="Deposit" OnClick="TFDepositButton_Click" />
            </div>
            <br/>

            <div id="SendMoneySection" style="text-align: center; border: 2px solid #ffffff; background-color:#006703; border-radius: 10px; padding: 20px;">
                <h2 style="margin-bottom: 0px";>Send Money</h2>
                <div style="display: flex; align-items: center;justify-content: center";>
                    <h3 style="margin-right: 10px; margin-left: 10px";>FROM</h3>
                    <asp:DropDownList runat="server" ID="SMFromAccountList" >
                        <asp:ListItem Text="Select an account" Value="" />
                        <asp:ListItem Text="Chequing Account" Value="chequing" />
                        <asp:ListItem Text="Saving Account"   Value="saving" />
                    </asp:DropDownList>
                    <h3 style="margin-right: 10px; margin-left: 10px";> To </h3>
                    <asp:TextBox ID="ReceiverAccountNum" runat="server" placeholder="Receiver Account Number" type="number"/>
                </div>
                <asp:TextBox ID="SendMoneyInput"  runat="server" placeholder="Enter deposit amount" type="number"></asp:TextBox>
                <asp:Button ID="Button1"  runat="server" Text="Deposit" OnClick="SMDepositButton_Click" />
            </div>


            <div id="message-area">
                <p id="transactionMessage" runat="server"></p>
            </div>

        </div>
    </form>
</body>
</html>
