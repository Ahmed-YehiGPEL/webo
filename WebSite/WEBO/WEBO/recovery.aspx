<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="recovery.aspx.cs" Inherits="WEBO.recovery" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Recover your account</title>
    <style type="text/css">
        .auto-style1 {
            height: 26px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table style="width: 384px">
        <tr>
            <td colspan="2">
                <asp:Label Text="Please fill in the current forum in order to recover your account" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label Text="User ID:" runat="server" />
            </td>
            <td>
                <asp:TextBox runat="server" ID="uIDBox" TextMode="Number"/>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="uIDBox" ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label runat="server" Text="Registered E-mail:" />
            </td>
            <td>
                <asp:TextBox runat="server" ID="uEmail" TextMode="Email"/> 
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="uEmail" ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label Text="Security Question:" runat="server" />
            </td>
            <td>
                <asp:DropDownList runat="server" ID="secQuestions" />
            </td>
        </tr>
        <tr>
            <td class="auto-style1">
                <asp:Label Text="Answer :" runat="server" />
            </td>
            <td class="auto-style1">
                <asp:TextBox runat="server" ID="secAnswer"/>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="secAnswer" ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Button Text="Recover Data" runat="server" ID="btnRecover" Height="31px" OnClick="btnRecover_Click" Width="370px"/>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Label runat="server" ID="stateLabel" ForeColor="Green"/>
            </td>
        </tr>
    </table>
    </div>
    </form>
</body>
</html>
