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
                if (Response.Cookies["UserName"] != null && Response.Cookies["Password"]!=null)
                {
                    Login1.UserName = Response.Cookies["UserName"].ToString();
                    System.Web.UI.WebControls.TextBox tb = (System.Web.UI.WebControls.TextBox)Login1.FindControl("Password");
                    tb.Attributes["Value"] = Response.Cookies["Password"].ToString();

                }
            }
        }

        protected void LoginButton_Click(object sender, EventArgs e)
        {

        }

        protected void Login1_Authenticate(object sender, AuthenticateEventArgs e)
        {
            
            MessageBox.Show(System.Configuration.ConfigurationManager.ConnectionStrings["MySQLDBConnection"].ConnectionString);
            string sCon = System.Configuration.ConfigurationManager.ConnectionStrings["MySQLDBConnection"].ConnectionString;
            string sQuery = "SELECT ID,acPass FROM uacc WHERE ID=" + Login1.UserName;
            
            MySqlConnection sqlCon = new MySqlConnection(sCon);
            sqlCon.Open();
            MySqlCommand cmd = sqlCon.CreateCommand();
            cmd.CommandText = sQuery;
            MySqlDataReader reader = cmd.ExecuteReader();
            string acPass = "", iID = "";
            while (reader.Read())
            {
              acPass = Cipher.StringCipher.Decrypt(reader["acPass"].ToString(), reader["ID"].ToString());
                iID = reader["ID"].ToString();
            }
            if (Login1.UserName == iID && Login1.Password == acPass)
            {
                e.Authenticated = true;
            }
            else
            {
                e.Authenticated = false;
            }
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
    }
}