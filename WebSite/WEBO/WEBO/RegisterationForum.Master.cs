using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Web;
using MySql.Data;
using MySql.Data.MySqlClient;
using Cipher;
/*
 * Email Validation >> Done
 * Set Connection >> 
 * Compare Data>>
 * Read Data >>
 * Insert Data >>
 */
namespace WEBO
{
    public partial class RegisterationForum : System.Web.UI.MasterPage
    {
        #region Declarations

        #region Connection Setting

        const string uID = "root;";
        const string uPWD = ";";
        const string dpName = "accounts;";
        const string dpServer = "localhost;";

        #endregion
        #region DataHandling
        string sFullName;
        string sEmail;
        string sPass;
        int iID;
        int iNum;
        string acmLevel;
        string sActivity;
        string sNickName;
        string sCodeforceHandle;
        bool bRecieveInfo = false;
        bool bAgreeOnTerms = false;
        #endregion
        #region Data Validation
        bool bIDExist = false;
        bool bIDFree = false;
        bool bIDNumMatch = false;
        bool bValidAccount = false;
        #endregion
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            onLoadData();
        }
        /// <summary>
        /// Fetches the data from the controls for further usage.
        /// </summar    y>
        protected  void fetchData()
        {
            sFullName = fullName.Text;
            sEmail = Email.Text;
            sPass = Password.Text;
            iID = Convert.ToInt32(idText.Text);
            iNum = Convert.ToInt32(numForum.Text);
            acmLevel = acmLevlText.Text;
            sCodeforceHandle = TextBox1.Text;
            sNickName = nickName.Text;
            int iTempCounter = 0;//for getting rid of ',' issue
            foreach (ListItem item in activitiesCheckBox.Items) // fetching selected activities
            {
                if (item.Selected == true)
                {
                    if (iTempCounter != activitiesCheckBox.Items.Count-1)
                    {
                        sActivity += item.Text + ',';
                    }
                    else
                        sActivity += item.Text;
                }
            }
            if (sActivity == "")
                sActivity = "NoActivity";
        }
        #region Data Validation
        /// <summary>
        /// Loads Data on Controls
        /// </summary>
        protected void onLoadData()
        {
            if (!IsPostBack)
            {
                string loadStr = "SELECT acName FROM student_activities;";
                string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["MySQLDBConnection"].ConnectionString.ToString();//"server=" + dpServer + "uid=" + uID + "pwd=" + uPWD + "database=" + dpName;
                MySqlConnection tempCon = new MySqlConnection(conStr);
                tempCon.Open();
                MySqlCommand cmd = new MySqlCommand(loadStr, tempCon);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    activitiesCheckBox.Items.Add(reader["acName"].ToString()); // Push items  while reading
                }
            }
        }
        protected void validateExistID(object sender, ServerValidateEventArgs args)
        {
            string sQuery = "SELECT SeatID,Reserved FROM student_id WHERE SeatID=" + iID.ToString() + ";";
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
            bIDExist = args.IsValid;
        }
        protected void validateFreeID(object sender, ServerValidateEventArgs args)
        {
            string sQuery = "SELECT SeatID,Reserved FROM student_id WHERE SeatID=" + iID.ToString() + ";";
            string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["MySQLDBConnection"].ConnectionString.ToString();//"server=" + dpServer + "uid=" + uID + "pwd=" + uPWD + "database=" + dpName;
            MySqlConnection tempCon = new MySqlConnection(conStr);
            tempCon.Open();
            MySqlCommand cmd = new MySqlCommand(sQuery, tempCon);
            MySqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read() && (int)reader["Reserved"] != 0)
            {
                args.IsValid = false;//Return that it's not valid
            }
            else
            {
                args.IsValid = true; // Return the ID is Free
            }
            bIDFree =  args.IsValid;
        }
        protected void validateRightNum(object sender, ServerValidateEventArgs args)
        {
            string sQuery = "SELECT SeatID,forumNum FROM student_id WHERE SeatID=" + iID.ToString() + ";";
            string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["MySQLDBConnection"].ConnectionString.ToString();//"server=" + dpServer + "uid=" + uID + "pwd=" + uPWD + "database=" + dpName;
            MySqlConnection tempCon = new MySqlConnection(conStr);
            tempCon.Open();
            MySqlCommand cmd = new MySqlCommand(sQuery, tempCon);
            MySqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read() && Convert.ToInt32(reader["forumNum"]) == iNum)
            {
                args.IsValid = true;//Return that it's valid
            }
            else
            {
                args.IsValid = false; // Return that it's not valid
            }
            bIDNumMatch = args.IsValid;
        }
        protected void validateID()
        {
            validateIDExist.Validate();
            if (bIDExist == true)
            {
                validateIDFree.Validate();
                if (bIDFree == true)
                {
                    validateIDNumMatch.Validate();
                    if (bIDNumMatch == true)
                    {
                        
                        Response.Write("Every thing is Right");
                        bValidAccount = true;
                        
                        //Execute Injection Process
                    }
                    else
                    {
                        //Error is already expressed through 
                        Response.Write("Every thing is Right Expect that the id and num don't match");
                    }
                }
                else
                {
                    Response.Write("ID is not FREE");
                }
            }
            else
            {
                Response.Write("Wrong ID");
                //ERROR ID DOESN'T Exist
            }
        }
        #endregion

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (bAgreeOnTerms == true)
            {
                fetchData();
                validateID();
                if (bValidAccount == true)
                    registerAccount();
            }
            else
            {
                Response.Write("You must agree on the terms and conditions if you want to join");
            }

        }
        /// <summary>
        /// Insert the Data inside the DB
        /// </summary>
        protected void registerAccount()
        {
            try
             {
           //Ciphering the password with itself+name
            string sEncryptedPass = StringCipher.Encrypt(sPass,iID.ToString());
           //The Activation KeyCode
            string sAcKey = StringCipher.Encrypt(sPass + iID.ToString() + sFullName, iID.ToString() + acmLevel);
            string sQuery = "INSERT INTO uacc (ID,fullName,acPass,pNum,Email,nickName,acmLevel,stActivity,acKey,codeforcesHandle,regDate,isActive) VALUES(@ID,@fullName,@acPass,@pNum,@Email,@nickName,@acmLevel,@stActivity,@acKey,@codeforcesHandle,@regDate,@isActive);";
            string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["MySQLDBConnection"].ConnectionString.ToString();//"server=" + dpServer + "uid=" + uID + "pwd=" + uPWD + "database=" + dpName;
            MySqlConnection tempCon = new MySqlConnection(conStr);
            tempCon.Open();
           //Add PARAMS 
            MySqlCommand cmd = new MySqlCommand(sQuery, tempCon);
            cmd.Parameters.AddWithValue("@ID", iID);
            cmd.Parameters.AddWithValue("@fullName", sFullName);
            cmd.Parameters.AddWithValue("@acPass", sEncryptedPass);
            cmd.Parameters.AddWithValue("@pNum", iNum);
            cmd.Parameters.AddWithValue("@Email", sEmail);
            cmd.Parameters.AddWithValue("@nickName", sNickName);
            cmd.Parameters.AddWithValue("@acKey", sAcKey);
            cmd.Parameters.AddWithValue("@acmLevel", acmLevel);
            cmd.Parameters.AddWithValue("@stActivity", sActivity);
            cmd.Parameters.AddWithValue("@codeforcesHandle",sCodeforceHandle);
            cmd.Parameters.AddWithValue("@regDate", DateTime.Now.Date.ToString());
            cmd.Parameters.AddWithValue("@isActive", "N");
            cmd.ExecuteNonQuery(); // then Execute

            tempCon.Close();
           /**********************************************************************************************/
           //then we set the ID to reserved
            tempCon.Open();

            sQuery = "UPDATE student_id SET Reserved=1 WHERE SeatID=" + iID.ToString() + ";";
            cmd = new MySqlCommand(sQuery, tempCon);
            cmd.ExecuteNonQuery();
            tempCon.Close();
           /**********************************************************************************************/
            if (bRecieveInfo == true)
                ApplyNotifcations();
            
            SetCookies(new string[] { iID.ToString(), sAcKey,sFullName},new string[]{"UserName","acKey","fullName"});

            Response.Redirect("activate.aspx");
               
            }
       catch(Exception Exc){
                Response.Write("    " + Exc.ToString());
            }

        }

        private void SetCookies(string[] CookiesToSet,string[] namesToSet)
        {

           for(int i=0;i<CookiesToSet.Length;i++)
           {
               Response.Cookies[namesToSet[i]].Value = CookiesToSet[i];
           }

        }
        protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
        {
        
          
        }
     
        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            bRecieveInfo = chkRecieveInfo.Checked;
        }

      
        protected void ApplyNotifcations()
        {
            try
            {
            string sQuery = "INSERT INTO emailNotifcations (SeatID,Email) VALUES(@SeatID,@Email);";
            string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["MySQLDBConnection"].ConnectionString.ToString();//"server=" + dpServer + "uid=" + uID + "pwd=" + uPWD + "database=" + dpName;
            MySqlConnection tempCon = new MySqlConnection(conStr);
            tempCon.Open();
            MySqlCommand cmd = new MySqlCommand(sQuery,tempCon);
            cmd.Parameters.AddWithValue("@SeatID",iID.ToString());
            cmd.Parameters.AddWithValue("@Email",sEmail);
            cmd.ExecuteNonQuery();
            tempCon.Close();
            tempCon.Dispose();
            }
            catch  (MySqlException exc)
            {  
                Response.Write(exc.Message);
            }
            finally
            {
                GC.Collect();
            }

        }

        protected void chkAgreeOnTerms_CheckedChanged(object sender, EventArgs e)
        {
            bAgreeOnTerms = true;
        }
    }
}