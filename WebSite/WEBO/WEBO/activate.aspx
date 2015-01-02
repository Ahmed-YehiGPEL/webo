<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="activate.aspx.cs" Inherits="WEBO.activate" %>
<!--NEED TO :
    Make page not available directly only by referrer

     --->
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
        }
        #form2 {
            text-align: center;
        }
        .style2 {
	text-align: center;
}
.style3 {
	text-align: center;
	font-size: medium;
}
.style4 {
	text-align: center;
}
.style5 {
	font-size: 16pt;
}
        .auto-style1 {
            text-align: justify;
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
        <p>
            <asp:Label ID="lblAddress" runat="server" Text="An E-Mail have been sent your account with your activation information, Please Copy the activation code from the E-Mail message in order to activate your account and proceed" style="text-align: left"></asp:Label>
        </p>
        <p>
            <asp:Label ID="lblActCode" runat="server" ForeColor="Green" Text="Activation Code : " Font-Size="16pt"></asp:Label>
        </p>
        <p>
            <asp:TextBox ID="actCodeBox" runat="server" BorderStyle="Dotted" Font-Italic="True" Font-Size="9pt" style="text-align: center" Width="557px">Activation Code</asp:TextBox>
        </p>
        <p>
            <asp:Button ID="btnActivate" runat="server" Font-Bold="True" ForeColor="Blue" OnClick="Button1_Click" Text="Proceed" Width="257px" />
        </p>
        <p class="style4">
            <asp:Label ID="StatusLabel" runat="server" ForeColor="ForestGreen" Font-Size="Small" Font-Italic="True"/>
        </p>
        
        <p>
            <asp:Button ID="Button3" runat="server" OnClick="Button3_Click" Text="Request new code / Enter Activation Code" Width="297px" />
        </p>
            <div id="re">
			<div class="style3">
				<asp:Label ID="lblNote" runat="server" Text="This action will overwrite your current E-mail address" Visible="False" style="text-align: center"></asp:Label>
                </div>
			<table align="center" style="width: 63%">
				<tr>
					<td style="width: 220px" class="auto-style1">

               <asp:Label ID="lblUID" runat="server" Text="User ID :" Visible="False" Font-Bold="True" Font-Size="14pt" ForeColor="Blue"></asp:Label>

           		 </td>
					<td class="auto-style1">
               <asp:TextBox ID="uIDBox" runat="server" TextMode="Number" Width="167px" BorderStyle="Dotted" Enabled="False" Visible="False" ></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ControlToValidate="uIDBox" Enabled="False" Visible="False"></asp:RequiredFieldValidator>
           		        <asp:CustomValidator ID="CustomValidator1" runat="server" Enabled="False" ErrorMessage="CustomValidator" ForeColor="Red" OnServerValidate="validateExistID" Visible="False">Invalid ID</asp:CustomValidator>
           		 </td>
				</tr>
				<tr>
					<td style="width: 220px" class="auto-style1">

               <asp:Label ID="lblSecQuestion" runat="server" Text="Security Question :" Visible="False" Font-Bold="True" Font-Size="14pt" ForeColor="Blue"></asp:Label>

           		 </td>
					<td class="auto-style1">
            <asp:DropDownList ID="secQuestions" runat="server" Height="25px" Enabled="False" Visible="False"></asp:DropDownList>                    
               <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="RequiredFieldValidator" ControlToValidate="secQuestions" Enabled="False">*</asp:RequiredFieldValidator>
            		</td>
				</tr>
				<tr>
					<td style="width: 220px" class="auto-style1">

               <asp:Label ID="lblSecAnswer" runat="server" Text="Answer :" Visible="False" Font-Bold="True" Font-Size="14pt" ForeColor="Blue"></asp:Label>

           		 </td>
					<td class="auto-style1">
                       <asp:TextBox ID="secAnswer" runat="server" Width="194px" BorderStyle="Dotted" Enabled="False" Visible="False"></asp:TextBox>
                       <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="RequiredFieldValidator" ControlToValidate="secAnswer" Enabled="False" Visible="False">*</asp:RequiredFieldValidator>
            		</td>
				</tr>
				<tr>
					<td style="width: 220px" class="auto-style1">

               <asp:Label ID="lblEmail" runat="server" Text="E-mail address:" Visible="False" Font-Bold="True" Font-Size="14pt" ForeColor="Blue"></asp:Label>

           		 </td>
					<td class="auto-style1">
            <asp:TextBox ID="emailTextBox" runat="server" BorderStyle="Dotted" Font-Italic="True" Font-Size="9pt" style="text-align: center" TextMode="Email" Width="192px" Enabled="False" Visible="False">eee@jjjj.com</asp:TextBox>

               <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="RequiredFieldValidator" ControlToValidate="emailTextBox" Enabled="False" Visible="False">*</asp:RequiredFieldValidator>

           		 </td>
				</tr>
			</table>
            </div>
		<p style="font-size: small; color: #FF0000; " class="style2">
            <asp:Button ID="Button2" runat="server" Font-Bold="True" ForeColor="Blue" OnClick="Button2_Click" Text="Request new ticket" Width="257px" Visible="False" Enabled="False" />
        </p><br />
		<p style="font-size: small; color: #FF0000; " class="style4">
            you may not activate your account after 3 days of registration as it&#39;s 
			automatically deleted and you&#39;ll have to register again.</p>
    </form>
    
     <p>
        
         Notes :<br />
         When the user registers the ID and the Activation Key will be sent to the Server, in the acKey Coloumn
         <br />
         Once a user verifies himself,he will be marked as Active in isActive Column<br />
         if a user tries to activate himself after 3 days of no activation , no activation will be enabled<br />
         because we will delete this activations every 3 days<br />
         and with the deletion of the activation the account itself will be deleted
         <br />
    </body>
</html>
