using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using System.Net.Mail;


namespace WEBO
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                if (Request.Cookies["uID"] != null && Request.Cookies["uPWD"]!=null)
                {
                    uIDBox.Text = Request.Cookies["uID"].Value;
                    uPWD.Text = Cipher.StringCipher.Decrypt(Request.Cookies["uPWD"].Value, Request.Cookies["uID"].Value);

                }
            }
        }
        private bool bCanLogin = false;
        protected void LoginButton_Click(object sender, EventArgs e)
        {

        }

        protected void Login1_Authenticate(object sender, AuthenticateEventArgs e)
        {
            
            
            
        }

        protected void Login1_LoggedIn(object sender, EventArgs e)
        {
            Response.Redirect("done.html");
        }

        protected void Login1_LoginError(object sender, EventArgs e)
        {
           
            Response.Redirect("register.aspx");
        }

        protected void LoginButton_Click1(object sender, EventArgs e)
        {

        }
        private void SetCookies(string[] CookiesToSet, string[] namesToSet, DateTime expires)
        {

            for (int i = 0; i < CookiesToSet.Length; i++)
            {
                Response.Cookies[namesToSet[i]].Value = CookiesToSet[i];
                Response.Cookies[namesToSet[i]].Expires = expires;
            }

        }
        private void SetCookies(string[] CookiesToSet, string[] namesToSet)
        {

            for (int i = 0; i < CookiesToSet.Length; i++)
            {
                Response.Cookies[namesToSet[i]].Value = CookiesToSet[i];
                
            }

        }
        private string getIP()
        {
            HttpRequest ipRequest = HttpContext.Current.Request;
            string ipAddr = ipRequest.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (ipAddr == null || ipAddr.ToLower() == "unknown")
                ipAddr = ipRequest.ServerVariables["REMOTE_ADDR"];
            return ipAddr;

        }
        protected void loginButton_Click2(object sender, EventArgs e)
        {
            try
            {
                string sCon = System.Configuration.ConfigurationManager.ConnectionStrings["MySQLDBConnection"].ConnectionString;
                string sQuery = "SELECT ID,acPass,loginState,fullName FROM uacc WHERE ID=" + uIDBox.Text;

                MySqlConnection sqlCon = new MySqlConnection(sCon);
                sqlCon.Open();
                MySqlCommand cmd = sqlCon.CreateCommand();
                cmd.CommandText = sQuery;
                MySqlDataReader reader = cmd.ExecuteReader();
                string acPass = "", iID = "",fullName  = "";
                while (reader.Read())
                {
                    if (reader["loginState"].ToString() == "O")
                    {
                        bCanLogin = true;
                        acPass = Cipher.StringCipher.Decrypt(reader["acPass"].ToString(), reader["ID"].ToString());
                        iID = reader["ID"].ToString();
                        fullName = reader["fullName"].ToString();
                    }
                    else
                        bCanLogin = false;
                }
                
                if (uIDBox.Text == iID && uPWD.Text == acPass && bCanLogin == true)
                {
                    if (chkBx.Checked == true) //The remember me funciton
                     SetCookies(new string[] { "uID", "uPWD" }, new string[] { uIDBox.Text, Cipher.StringCipher.Encrypt(uPWD.Text, uIDBox.Text) }, DateTime.Now.AddDays(30));
                    else
                        SetCookies(new string[] { "uID", "uPWD" }, new string[] { uIDBox.Text, Cipher.StringCipher.Encrypt(uPWD.Text, uIDBox.Text) }); //if No remmer me , just store em for the rest of session

                    //store the current cookie for the forums and the IRC [ full name ]
                    SetCookies(new string[] {"idFullname" }, new string[] {fullName });
                    //Disposing
                    cmd.Dispose();
                    reader.Dispose();
                    sqlCon.Close();
                    sqlCon.Dispose();

                    sqlCon = new MySqlConnection(sCon);
                    sqlCon.Open();
                    sQuery = "UPDATE uacc SET loginState=\"I\" WHERE ID=" + iID + ';';
                    cmd = new MySqlCommand(sQuery, sqlCon);
                    cmd.ExecuteNonQuery();

                    GC.Collect();
                    Response.Redirect("~/done.html");

                }
                else
                {
                    if(bCanLogin == true)
                        errorLabel.Text = "Error While logging in, Invalid ID/Password.";
                    if (bCanLogin == false)
                    {
                        errorLabel.Text = "Error while logging in, already logged in.";
                    }
                }
            }
            catch (Exception ex)
            {
                errorLabel.Text = "An Error Occurred While logging in, " + ex.Message;
            }
        }
    }
}