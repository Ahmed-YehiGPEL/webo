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
            if (!IsPostBack)
            {

                string sQuery = "SELECT scQuestion FROM secquestions;";
                string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["MySQLDBConnection"].ConnectionString.ToString();//"server=" + dpServer + "uid=" + uID + "pwd=" + uPWD + "database=" + dpName;
                MySqlConnection tempCon = new MySqlConnection(conStr);
                tempCon.Open();
                MySqlCommand cmd = new MySqlCommand(sQuery, tempCon);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                    secQuestions.Items.Add(reader["scQuestion"].ToString());

            }
        }
        private const string mailSubject = "Welcom to ASU FCIS 2014 - Activation Mail [ no reply E-mail ]";
        private const string mailBody = "Welcome to ASU FCIS 2014\nPlease don't reply on this E-mail\nCopy this activation code to the activation area:\n\n";
        private const string smtpPassword = "webo2014fcisproject_smtpServer";
        private const string smtpUser = "fcis.admin@fcis.tk";
        bool bExistID = false;
        private bool bReversed = false;
        protected void validateExistID(object sender, ServerValidateEventArgs args)
        {
         
                string sQuery = "SELECT SeatID,Reserved FROM student_id WHERE SeatID=" + uIDBox.Text + ";";
                string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["MySQLDBConnection"].ConnectionString.ToString();//"server=" + dpServer + "uid=" + uID + "pwd=" + uPWD + "database=" + dpName;
                MySqlConnection tempCon = new MySqlConnection(conStr);
                tempCon.Open();
                MySqlCommand cmd = new MySqlCommand(sQuery, tempCon);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (!reader.HasRows)
                {
                    args.IsValid = false;
                }
                else
                {
                    args.IsValid = true;
                }
                bExistID = args.IsValid;
         
        }
        private string getACKey(string uID)
        {
             string acKey = "";
             string sCon = System.Configuration.ConfigurationManager.ConnectionStrings["MySQLDBConnection"].ConnectionString;
             MySqlConnection con = new MySqlConnection(sCon);
             con.Open();
             MySqlCommand cmd = con.CreateCommand();
             cmd.CommandText = "SELECT acKey FROM uacc WHERE ID=" + uID + ';';
             MySqlDataReader reader = cmd.ExecuteReader();
             while (reader.Read())
                 acKey = reader["acKey"].ToString();
            
            return acKey;
        }
        
        protected void Button1_Click(object sender, EventArgs e)//Activate
        {
            /*
             *Get user id from cookies
             *Get user ac from table
             *compare them the ac and the ac written in
             *if yes , activate 
             *if no , show error message
             */
                string userName = Request.Cookies["uID"].Value;
                string acKey = getACKey(userName);
                if (acKey == actCodeBox.Text)
                {
                    activateAccount(userName);
                    Response.Redirect("~/index.html");
                }
                else
                {
                    StatusLabel.Text = "Error, Wrong Activation Code ";
                }
            
        }
        private void activateAccount(string user)   
        {
            string sCon = System.Configuration.ConfigurationManager.ConnectionStrings["MySQLDBConnection"].ConnectionString;
            MySqlConnection con = new MySqlConnection(sCon);
            string sQuery = "UPDATE uacc SET `isActive`=\"Y\" WHERE `ID`=" + user + ";";
            con.Open();
            MySqlCommand cmd = new MySqlCommand(sQuery, con);
            cmd.ExecuteNonQuery();
            con.Close();
            
            GC.Collect();
        }
        protected void Button2_Click(object sender, EventArgs e) // Check and activate
        {
            try
            {
               
                string uID = uIDBox.Text;
                string sAnswer = secAnswer.Text;
                string sQuestion = secQuestions.SelectedItem.Text;

                if (bExistID == true)
                {

                    string sCon = System.Configuration.ConfigurationManager.ConnectionStrings["MySQLDBConnection"].ConnectionString;
                    MySqlConnection con = new MySqlConnection(sCon);
                    string sQuery = "SELECT secQuestion,secAnswer FROM uacc WHERE ID=" + uID + ";";
                    con.Open();
                    MySqlCommand cmd = new MySqlCommand(sQuery, con);
                    MySqlDataReader reader = cmd.ExecuteReader();
                    string temAC = "", temQU = "";
                    while (reader.Read())
                    {
                        temAC = reader["secAnswer"].ToString();
                        temQU = reader["secQuestion"].ToString();
                    }
                    if (temAC == sAnswer && sQuestion == temQU)
                    {
                        string acKey = getACKey(uID);
                        string res = sendActEmail(acKey, emailTextBox.Text);
                        if (res == "Done")
                        {
                            StatusLabel.Text = "Email Sent Succesfully Please Refer Back To Your Inbox, and check the Spam/Junk mail folder";
                            

                        }
                        else
                        {
                            StatusLabel.Text = res;
                            
                        }
                    }
                    else
                        StatusLabel.Text = "Error While Matching Question And Answer Please Try Again";
                }
                
              }
            catch (Exception exc)
            {
                StatusLabel.Text = "An error occurred,error message:\n" + exc.Message;
                StatusLabel.Visible = true;

            }


        }
        protected string sendActEmail(string actKey, string email)
        {
            try
            {
                MailMessage mail = new MailMessage("fcis.admin@fcis.tk", email);
                mail.Subject = mailSubject;
                mail.Body = mailBody + actKey;
                string smtpServer = System.Configuration.ConfigurationManager.ConnectionStrings["EmailSMTPServer"].ConnectionString;
                Response.Write(smtpServer);
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

        private string getCookie(string p)
        {
            return Request.Cookies[p].Value.ToString();
        }

        protected void Unnamed_Click(object sender, EventArgs e)
        {
         
        }
        protected void setCook(object sendet, EventArgs e)
        {

        }

        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            //deactivating controls
            reverseControls();
            Response.Write("Welcome");
            Response.Redirect("~/activate.aspx");
        }
        private void reverseControls()
        {
            actCodeBox.Enabled = !actCodeBox.Enabled;
            btnActivate.Enabled = !btnActivate.Enabled;
            lblActCode.Visible = !lblActCode.Visible;
            lblAddress.Visible = !lblAddress.Visible;

            actCodeBox.Visible = !actCodeBox.Visible;
            btnActivate.Visible = !btnActivate.Visible;


            secQuestions.Visible = !(secQuestions.Visible);
            secAnswer.Visible = !secAnswer.Visible;
            uIDBox.Visible = !(uIDBox.Visible);
            emailTextBox.Visible = !(emailTextBox.Visible);
            lblSecAnswer.Visible = !lblSecAnswer.Visible;
            lblSecQuestion.Visible = !lblSecQuestion.Visible;
            lblUID.Visible = !lblUID.Visible;
            lblEmail.Visible = !lblEmail.Visible;
            lblNote.Visible = !lblNote.Visible;
            Button2.Visible = !Button2.Visible;


            RequiredFieldValidator1.Visible = !(RequiredFieldValidator1.Visible);
            RequiredFieldValidator2.Visible = !(RequiredFieldValidator2.Visible);
            RequiredFieldValidator3.Visible = !(RequiredFieldValidator3.Visible);
            RequiredFieldValidator4.Visible = !(RequiredFieldValidator4.Visible);
            CustomValidator1.Visible = !CustomValidator1.Visible;

            secQuestions.Enabled = !(secQuestions.Enabled);
            secAnswer.Enabled = !(secAnswer.Enabled);
            uIDBox.Enabled = !(uIDBox.Enabled);
            emailTextBox.Enabled = !(emailTextBox.Enabled);
            Button2.Enabled = !(Button2.Enabled);


            RequiredFieldValidator1.Enabled = !(RequiredFieldValidator1.Enabled);
            RequiredFieldValidator2.Enabled = !(RequiredFieldValidator2.Enabled);
            RequiredFieldValidator3.Enabled = !(RequiredFieldValidator3.Enabled);
            RequiredFieldValidator4.Enabled = !(RequiredFieldValidator4.Enabled);
            CustomValidator1.Enabled = !CustomValidator1.Enabled;
            bReversed = !bReversed;
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
               reverseControls();
        }
    }
}