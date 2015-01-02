<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="WEBO.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style2 {
            width: 420px;
        }
        .auto-style5 {
            height: 26px;
            width: 254px;
        }
        .auto-style6 {
            width: 254px;
        }
        .auto-style7 {
            height: 26px;
            width: 264px;
        }
        .auto-style8 {
            width: 264px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        Sample Login System<br />
        <table style="height: 138px; width: 392px">
            <tr>
                <td class="auto-style7">
                    <asp:Label Text="User-ID :" runat="server" ID="uIDLabel"/>
                </td>
                <td class="auto-style5">
                    <asp:TextBox runat="server" ID="uIDBox" TextMode="Number" MaxLength="10" Width="167px"/>
                </td>
            </tr>
            <tr>
                <td class="auto-style8">
                    <asp:Label Text="Password :" runat="server" ID="uPWDLabel"/>
                </td>
                <td class="auto-style6">
                    <asp:TextBox runat="server" ID="uPWD" TextMode="Password" Width="165px"/>
                </td>
            </tr>
            <tr>
             <td class="auto-style2" colspan="2">
                 <asp:CheckBox Text="Remember me next time for next 30 days" runat="server" ID="chkBx" BorderStyle="Dotted"/>
             </td>
            </tr>
            <tr>
                <td class="auto-style2" colspan="2">
                    <asp:Label  Text="" runat="server" ID="errorLabel" ForeColor="Red"/>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Button Text="Login" ID="loginButton" runat="server" align="Center" ForeColor="Green" Height="29px" Width="424px" OnClick="loginButton_Click2"/>
                </td>
            </tr>
            <tr>
                <td colspan="1" class="auto-style8">
                    <asp:LinkButton Text="Forgot your password !" runat="server" />
                </td>
                <td class="auto-style6">
                    <asp:LinkButton Text="Register an account" runat="server" PostBackUrl="~/register.aspx" />
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
