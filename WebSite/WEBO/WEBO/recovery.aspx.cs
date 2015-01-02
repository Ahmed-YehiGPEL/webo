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
    public partial class recovery : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        private string sCon = System.Configuration.ConfigurationManager.ConnectionStrings["MySQLDBConnection"].ConnectionString.ToString();
        private const string mailSubject = "ASU FCIS 2014 - Account Recovery Mail [ no reply E-mail ]";
        private const string mailBody = "This is a recovery E-mail sent upon your request.\nIf you think this E-mail was sent to you by false, Please don't reply to it.\nRemember to change your password onve you log in to your account,Your new temporary password is :\n\n";
        private const string smtpPassword = "webo2014fcisproject_smtpServer";
        private const string smtpUser = "fcis.admin@fcis.tk";

        protected void btnRecover_Click(object sender, EventArgs e)
        {
            try
            {
                MySqlConnection con = new MySqlConnection(sCon);
                con.Open();
                MySqlCommand cmd = con.CreateCommand();
                string sQuery = "SELECT secQuestion,secAnswer,ID,Email FROM uacc WHERE ID=" + uIDBox.Text + ';';
                cmd.CommandText = sQuery;
                MySqlDataReader reader = cmd.ExecuteReader();
                string dEmail = "", dQuestion = "", dAnswer = "",dID="";
                while (reader.Read())
                {
                    dID = reader["ID"].ToString();
                    dEmail = reader["Email"].ToString();
                    dQuestion = reader["secQuestion"].ToString();
                    dAnswer = reader["secAnswer"].ToString();
                }
                if (dID == uIDBox.Text)
                {
                    if (dQuestion == secQuestions.SelectedItem.Text)
                    {
                        if (dAnswer == secAnswer.Text)
                        {
                            if (dEmail == uEmail.Text)
                            {
                                //send recovery email
                            }
                            else
                                stateLabel.Text = "Wrong E-mail address";
                        }
                        else
                            stateLabel.Text = "Wrong Answer";
                    }
                    else
                        stateLabel.Text = "Wrong security question";
                }
                else
                    stateLabel.Text = "Invalid ID";
                
            }
            catch (Exception ex)
            {
                stateLabel.Text = ex.Message;
            }

        }
        private string sendRecoveryEmail(string email,string newPass)
        { 
           try
            {
                MailMessage mail = new MailMessage("fcis.admin@fcis.tk", email);
                mail.Subject = mailSubject;
                mail.Body = mailBody + newPass;
                string smtpServer = System.Configuration.ConfigurationManager.ConnectionStrings["EmailSMTPServer"].ConnectionString;
                SmtpClient smtpClient = new SmtpClient(smtpServer);
                System.Net.NetworkCredential accecCredit = new System.Net.NetworkCredential(smtpUser, smtpPassword);
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = accecCredit;
                
                smtpClient.Send(mail);
                return "Done";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }
        private void recoverAccount()
        {
            try
            {

                MySqlConnection con = new MySqlConnection(sCon);
                Random rnd = new Random();
                int rndNum = Convert.ToInt32(rnd.Next(1, secAnswer.Text.Length));
                string sQuery = "UPDATE uacc SET acPass=" + Cipher.StringCipher.Encrypt(rndNum.ToString(), uIDBox.Text) + "WHERE ID="+uIDBox.Text+';';
                MySqlCommand cmd = new MySqlCommand(sQuery, con);
                cmd.ExecuteNonQuery();
                string res = sendRecoveryEmail(uEmail.Text,rndNum.ToString());
                if(res=="Done")
                    stateLabel.Text = "An E-mail has been sent succesfully with your new password.";
                else
                    stateLabel.Text = "An error occurred " + res;
            }
            catch (Exception e)
            {
                stateLabel.Text = e.Message;
            }
        }
    }
}