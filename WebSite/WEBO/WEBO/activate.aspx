<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="activate.aspx.cs" Inherits="WEBO.activate" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        #Text1 {
            width: 321px;
        }
        #Text2 {
            width: 320px;
        }
        #Button3 {
            width: 139px;
        }
    </style>
    <script>
        function postUserID() {
            document.getElementById("usrID").textContent = get_cookie("UserName");
        }
        function get_cookie(cookie_name) {

            var cookie_string = document.cookie;
            if (cookie_string.length != 0) {
                var cookie_value = cookie_string.match('(^|;)[\s]*' + cookie_name + '=([^;]*)');
                return decodeURIComponent(cookie_value[2]);
            }
            return '';
        }
        function postUserName() {
            document.getElementById("usrFullName").textContent = get_cookie("fullName");
        }
        function setVal() {
            postUserID();
            postUserName();
        }
    </script>
</head>
<body style="text-align: center">
    <form id="form1" runat="server" style="font-family: 'Courier New', Courier, monospace; font-size: large; font-weight: bold; font-style: normal; font-variant: normal; text-transform: none; color: #000080; text-decoration: blink; border: thin double #000000">
        An E-Mail have been sent your account with your activation information, Please Copy the activation code from the E-Mail message in order to acitvate your account and proceed<p>
            <asp:Label ID="Label1" runat="server" ForeColor="Green" Text="Activation Code : "></asp:Label>
        </p>
        <p>
            <asp:TextBox ID="actCodeBox" runat="server" BorderStyle="Dotted" Font-Italic="True" Font-Size="9pt" style="text-align: center" Width="557px">Activation Code</asp:TextBox>
        </p>
        <p>
            <asp:Button ID="Button1" runat="server" Font-Bold="True" ForeColor="Blue" OnClick="Button1_Click" Text="Proceed" Width="257px" />
        </p>
        <p>
            Wrong E-mail address, you may request a ticket with a new Email address</p>
        <p>
            <asp:TextBox ID="emailTextBox" runat="server" BorderStyle="Dotted" Font-Italic="True" Font-Size="9pt" style="text-align: center" TextMode="Email" Width="557px">Email Address</asp:TextBox>
        </p>
        <p>
            <asp:Button ID="Button2" runat="server" Font-Bold="True" ForeColor="Blue" OnClick="Button2_Click" Text="Request new ticket" Width="257px" />
        </p>
    <p>
        Test Of Cookies :
    <asp:Button Text="Click" runat="server" OnClick="Unnamed_Click" ID="btn1" />
        <asp:Button ID="Button4" runat="server" OnClick="setCook" Text="Button" />
    </p>
    </form>
    <p>
        <input id="usrID" type="text" aria-readonly="True" /></p>
    <p>
        <input id="usrFullName" type="text" aria-readonly="True" /></p>
    <p>
        <input id="Button3" onload="setVal()" type="button" value="Check" /></p>
    </body>
</html>
