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
        private bool canModify = true;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] != null)
            {
                user_id = Session["UserName"].ToString();
            }
            if (Request["user_id"]!=null)
            {
                if(Request["user_id"]!=user_id)
                {
                    user_id = Request["user_id"];
                    canModify = false;
                }
            }
            
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

            if (!canModify)
            {
                modify_button.Visible = false;
                posts_container_title.InnerHtml = "His/Her&nbspPosts";
                add_post.Visible = false;
            }

            SetItems();
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

        public void AddPost(object sender, EventArgs e)
        {
            Response.Redirect("/PostPage/PostPage.aspx");
        }

        public void SetItems()
        {
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(connstr))
            {
                MySqlCommand command = conn.CreateCommand();
                command.CommandText =
                    "SELECT id, image_id, name, description, price " +
                    "from items where owner_id=" + user_id;
                conn.Open();
                MySqlDataReader reader = command.ExecuteReader();  
                dt.Load(reader);
                conn.Close();
            }

            posted_list.InnerHtml = "";

            foreach (DataRow row in dt.Rows)
            {
                string html =
                    "<div class=\"grid-item\">" +
                        "<a href=\"/ItemPage/ItemPage.aspx?item_id=" + row["id"].ToString() + "\" style=\"color: #3d3c3a\">" +
                            "<img src=\"/DisplayImage/ItemImage.aspx?image_id=" + row["image_id"].ToString().Split(',')[0].Trim() + "\" style=\"width: 100%;\">" +
                            "<div class=\"item_title\">" + row["name"].ToString() + "</div>" +
                            "<div class=\"item_price\">&yen" + row["price"].ToString() + "</div>" +
                            "<p class=\"item_description\">" + row["description"].ToString() + "</p>" +
                        "</a>";

                if(canModify)
                    html+=
                        "<asp:ImageButton runat=\"server\" " +
                        "class=\"delete_buttons\" ImageUrl=\"/images/delete.png\" " +
                        "CommandName=\""+row["id"].ToString()+"\"/>";

                html += "</div>";
                Control control = ParseControl(html);

                if (canModify)
                {
                    ImageButton delete = control.Controls[1] as ImageButton;
                    delete.Command += new CommandEventHandler(DeleteItem);
                }
                
                posted_list.Controls.Add(control);
            }
        }

        public void DeleteItem(Object sender, CommandEventArgs e)
        {
            string item_id = e.CommandName;
            using (MySqlConnection conn = new MySqlConnection(connstr))
            {
                MySqlCommand command = conn.CreateCommand();
                command.CommandText =
                    "DELETE from items " +
                    "where id=\"" + item_id + "\" and owner_id=\"" + user_id+"\"";
                conn.Open();
                System.Diagnostics.Debug.WriteLine(command.CommandText);
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