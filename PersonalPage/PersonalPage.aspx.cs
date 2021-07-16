using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WeExchange.PersonalPage
{
    public partial class PersonalPage : System.Web.UI.Page
    {
        public const String connstr = "server=localhost;Database=weexchange;uid=root;pwd=1234;charset=utf8;pooling=True;";
        public string user_id;
        public string[] genders = new string[] { "male", "female", "secret" };
        private string user_name, user_gender, user_contact_info;
        protected void Page_Load(object sender, EventArgs e)
        {
            user_id = Session["UserName"].ToString();
            if(!IsPostBack)
            {
                using (MySqlConnection conn = new MySqlConnection(connstr))
                {
                    MySqlCommand command = conn.CreateCommand();
                    command.CommandText =
                        "SELECT user_name, gender, contact_info from users where id=" + user_id;
                    conn.Open();
                    MySqlDataReader reader = command.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(reader);
                    user_name = dt.Rows[0]["user_name"].ToString();
                    user_gender = dt.Rows[0]["gender"].ToString();
                    user_contact_info = dt.Rows[0]["contact_info"].ToString();
                    conn.Close();
                }

                name.Text = user_name;
                for (int i = 0; i < 3; ++i)
                {
                    if (genders[i] == user_gender)
                        gender.SelectedIndex = i;
                }
                contact_info.Value = user_contact_info;
            }
            profile.ImageUrl = "/DisplayImage/ProfileImage.aspx?user_id=" + user_id;
            Cancel(sender, e);
        }

        protected void Modify(object sender, EventArgs e)
        {
            upload_button.Visible = true;
            name.Enabled = true;
            gender.Enabled = true;
            contact_info.Disabled = false;
            modify_button.Visible = false;
            save.Visible = true;
            cancel.Visible = true;
        }
        protected void Cancel(object sender, EventArgs e)
        {
            upload_button.Visible = false;
            name.Enabled = false;
            gender.Enabled = false;
            contact_info.Disabled = true;
            modify_button.Visible = true;
            save.Visible = false;
            cancel.Visible = false;
        }

        
        protected void Update(object sender, EventArgs e)
        {
            if (upload_button.HasFile)
            {
                string fileExtension = Path.GetExtension(upload_button.FileName).ToLower();
                if (IsImage(fileExtension))
                {
                    profile.ImageUrl = Path.GetFullPath(upload_button.FileName);
                    using (MySqlConnection conn = new MySqlConnection(connstr))
                    {
                        MySqlCommand command = conn.CreateCommand();
                        command.CommandText =
                            "UPDATE users SET profile=NULL where id=" + user_id;
                        conn.Open();
                        command.ExecuteNonQuery();

                        command.CommandText = "update users set profile=@profile where id=@id";
                        command.Parameters.Clear();
                        command.Parameters.AddWithValue("id", user_id);
                        command.Parameters.AddWithValue("profile", upload_button.FileBytes);
                        command.ExecuteNonQuery();
                        conn.Close();
                    }
                }
                else
                {
                    Response.Write("<script>alert('Please Upload Image File')</script>");
                    return;
                }
            }

            using (MySqlConnection conn = new MySqlConnection(connstr))
            {
                MySqlCommand command = conn.CreateCommand();
                command.CommandText = "update users set " +
                    "user_name=@user_name, gender=@gender, contact_info=@contact_info " +
                    "where id=@id";
                conn.Open();
                command.Parameters.Clear();
                command.Parameters.AddWithValue("id", user_id);
                command.Parameters.AddWithValue("user_name", name.Text);
                command.Parameters.AddWithValue("gender", genders[gender.SelectedIndex]);
                command.Parameters.AddWithValue("contact_info", contact_info.Value);
                command.ExecuteNonQuery();
                conn.Close();
            }
            Response.Redirect(Request.Url.ToString());
        }

        private bool IsImage(string file)
        {
            bool isimage = false;
            string tmp = file.ToLower();
            
            string[] allowExtension = { ".jpg", ".gif", ".bmp", ".png" };
            
            for (int i = 0; i < allowExtension.Length; i++)
            {
                if (tmp == allowExtension[i])
                {
                    isimage = true;
                    break;
                }
            }
            return isimage;
        }
    }
}