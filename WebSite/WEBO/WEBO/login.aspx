<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="WEBO.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            height: 21px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
        <asp:Login ID="Login1" runat="server" BorderStyle="Double" CreateUserUrl="~/WebForm1.aspx" FailureAction="RedirectToLoginPage" Font-Names="Lucida Console" ForeColor="Green" InstructionText="Please Input your Seat ID and Password To Proceed" OnAuthenticate="Login1_Authenticate" OnLoggedIn="Login1_LoggedIn" RememberMeSet="True" TitleText="Sample Log in Test" UserNameLabelText="Seat ID" UserNameRequiredErrorMessage="Seat ID is Required" Width="351px" OnLoginError="Login1_LoginError">
            <CheckBoxStyle BorderColor="#CC6699" BorderStyle="Dotted" />
            <InstructionTextStyle Font-Bold="True" Font-Italic="False" Font-Names="Courier New" Font-Size="9pt" ForeColor="Blue" />
            <LabelStyle BorderStyle="Ridge" />
            <LayoutTemplate>
                <table cellpadding="1" cellspacing="0" style="border-collapse:collapse;">
                    <tr>
                        <td>
                            <table cellpadding="0" style="width:351px;">
                                <tr>
                                    <td align="center" colspan="2">Sample Log in Test</td>
                                </tr>
                                <tr>
                                    <td align="center" colspan="2" style="color:Blue;font-family:Courier New;font-size:9pt;font-weight:bold;font-style:normal;">Please Input your Seat ID and Password To Proceed</td>
                                </tr>
                                <tr>
                                    <td align="right" style="border-style:Ridge;">
                                        <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">Seat ID</asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="UserName" runat="server" BorderStyle="Double" MaxLength="10" TextMode="Number"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName" ErrorMessage="Seat ID is Required" ToolTip="Seat ID is Required" ValidationGroup="Login1">*</asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="border-style:Ridge;">
                                        <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">Password:</asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="Password" runat="server" BorderStyle="Double" TextMode="Password"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password" ErrorMessage="Password is required." ToolTip="Password is required." ValidationGroup="Login1">*</asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="border-color:#CC6699;border-style:Dotted;">
                                        <asp:CheckBox ID="RememberMe" runat="server" Checked="True" Text="Remember me next time." />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" colspan="2" style="color:Red;">
                                        <asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" colspan="2">
                                        <asp:Button ID="LoginButton" runat="server" CommandName="Login" ForeColor="Red" OnClick="LoginButton_Click1" Text="Log In" ValidationGroup="Login1" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </LayoutTemplate>
            <LoginButtonStyle ForeColor="Red" />
            <TextBoxStyle BorderStyle="Double" />
        </asp:Login>
    </form>
</body>
</html>
