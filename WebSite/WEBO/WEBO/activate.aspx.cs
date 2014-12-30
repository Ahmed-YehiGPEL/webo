using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Net.Mail;
namespace WEBO
{
    public partial class activate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        private const string mailSubject = "Welcom to ASU FCIS 2014 - Activation Mail";
        private const string mailBody = "Welcome to ASU FCIS 2014\nPlease copy this activation code to the activation area:\n";
        protected void Button1_Click(object sender, EventArgs e)
        {
            string acKey = Response.Cookies["acKey"].ToString();
            string userName = Response.Cookies["tempUser"].ToString();
            string sCon = System.Configuration.ConfigurationManager.ConnectionStrings["MySQLDBConnection"].ConnectionString;
            string sQuery = "SELECT ID,acKey FROM uacc WHERE ID=" + userName + ";";
            MySqlConnection con = new MySqlConnection(sCon);
            con.Open();
            MySqlCommand cmd = con.CreateCommand();
            cmd.CommandText = sQuery;
            MySqlDataReader reader = cmd.ExecuteReader();
            string temUsr = "", temAC = "";
            while (reader.Read())
            {
                temUsr = reader["ID"].ToString();
                temAC = reader["acKey"].ToString();
            }
            if (acKey == temAC && temUsr == Response.Cookies["UserName"].ToString())
            {
                Response.Redirect("index.html");
                Response.Cookies["acKey"].Value = "-1";

            }
            else
            {
                Response.Write("WRONG WRONG");
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            string userName = getCookie("UserName");
            string sCon = System.Configuration.ConfigurationManager.ConnectionStrings["MySQLDBConnection"].ConnectionString;
            string sQuery = "SELECT acKey FROM uacc WHERE ID=" + userName + ";";
            MySqlConnection con = new MySqlConnection(sCon);
            con.Open();
            MySqlCommand cmd = new MySqlCommand(sQuery, con);
            MySqlDataReader reader = cmd.ExecuteReader();
            string tempKey = "";
            while (reader.Read())
            {
                tempKey = reader["acKey"].ToString();
            }
            string res = sendActEmail(tempKey,emailTextBox.Text);
            if (res != "Done")
            {
                Response.Write("Email address done and sent " + tempKey + "  to "+ emailTextBox.Text);

            }
            else
                Response.Write(res);

        }
        protected string sendActEmail(string actKey, string email)
        {
            try
            {
                MailMessage mail = new MailMessage("fcis.admin@fcis.tk", email);
                mail.Subject = mailSubject;
                mail.Body = mailBody + actKey;
                SmtpClient smtpClient = new SmtpClient("mail.fics.tk", 25);
                //SmtpClient smtpClient = new SmtpClient(System.Configuration.ConfigurationManager.ConnectionStrings["EmailSMTP"].ConnectionString);
                smtpClient.EnableSsl = true;
                System.Net.NetworkCredential accecCredit = new System.Net.NetworkCredential("fcis.admin@fcis.tk", "fcis2014weboproject");
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = accecCredit;
                smtpClient.ServicePoint.MaxIdleTime = 1;
                smtpClient.Send(mail);
                return "Done";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        private string getCookie(string p)
        {
            return Request.Cookies[p].Value.ToString();
        }

        protected void Unnamed_Click(object sender, EventArgs e)
        {
            Response.Write(Request.Cookies["UserName"].Value.ToString());
         
        }
        protected void setCook(object sendet, EventArgs e)
        {
            HttpCookie COOK = new HttpCookie("UserName", "2014170069");
            COOK.Expires = new DateTime(2015, 12, 25);
            Response.Cookies.Add(COOK);
            
        }
    }
}