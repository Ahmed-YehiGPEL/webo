<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="mangement.aspx.cs" Inherits="WEBO.mangement" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Account Management</title>
    <style type="text/css">
        .auto-style1 {
            width: 128px;
        }
        .auto-style2 {
            width: 157px;
        }
        .auto-style3 {
            font-size: xx-large;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <p align="center">
            <span class="auto-style3">Account Mangement Area</span>
        </p>
        <table style="height: 257px; width: 483px">
            <tr>
                <td colspan="2">
                    <asp:Label Text="Please choose an action" runat="server" Height="23px" style="text-align: center" Width="447px" />
                </td>
            </tr>
             <tr>
                <td class="auto-style2">
                    <asp:Label  Text="New password:" runat="server" />
                </td>
                 <td class="auto-style1">
                     <asp:TextBox TextMode="Password" runat="server" ID="newPWDBox" Width="203px" />
                 </td>
             </tr>
            <tr>
                <td class="auto-style2">
                    <asp:Label ID="Label1" Text="Repeat password:" runat="server" />
                </td>
                 <td class="auto-style1">
                     <asp:TextBox ID="newRePWDBox" TextMode="Password" runat="server" Width="203px" />
                 </td>
            </tr>
            <tr>
                <td class="auto-style2">
                    <asp:Label Text="Change E-mail :" runat="server" />
                </td>
                <td class="auto-style1">
                    <asp:TextBox TextMode="Email" ID="newEmailBox" runat="server" Width="203px" />
                </td>
            </tr>
            <tr>
                <td class="auto-style2">
                    <asp:Label Text="Change nick name:" runat="server" />
                </td>
                <td class="auto-style1">
                    <asp:TextBox runat="server" ID="nickNameBox" Width="203px" />
                    
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:CheckBoxList runat="server" Width="476px">
                        <asp:ListItem Text="Commit password change" />
                        <asp:ListItem Text="Commit E-mail change" />
                        <asp:ListItem>Commit Nickname Change</asp:ListItem>
                    </asp:CheckBoxList>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: center">
                    Note that any change made without checking it&#39;s check box won&#39;t be commited to your account data.<br />
                    Enter your security question and answer for confirmation :n :</td>
            </tr>
            <tr>
                <td>
                    <asp:Label Text="Security Queestion :" runat="server" />
                </td>
                <td>
                    <asp:DropDownList runat="server" ID="secQuestions">
                        
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label Text="Answer :" runat="server" />
                </td>
                <td>
                    <asp:TextBox runat="server" ID="secAnswer" Width="203px"/>
                </td>
            </tr>
            <tr>
                <td colspan="2">

                    <asp:Button Text="Commit changes" runat="server" ID="btnCommit" Width="473px" />

                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
