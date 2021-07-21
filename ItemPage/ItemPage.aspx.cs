using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace WeExchange.ItemPage
{
    public partial class ItemPage : System.Web.UI.Page
    {
        public const String connstr = "server=localhost;Database=weexchange;uid=root;pwd=1234;charset=utf8";
        public string item_id;
        public string item_description;
        public string price;
        public string post_date;
        public string hot;
        public string owner_id;
        public string content_owner_id;
        public string userid;
        public string content;
        public string comment_post_date;
        public string content_owner;
        public string comment_id;
        //private bool canModify = true;

        public string owner_name;

        public string name;

        protected void Page_Load(object sender, EventArgs e)
        {
            
                //Get item info
                item_id = Request["item_id"];
            using (MySqlConnection conn = new MySqlConnection(connstr))
            {
                MySqlCommand command = conn.CreateCommand();
                command.CommandText =
                    "SELECT * from weexchange.items where id='" + item_id + "';";
                conn.Open();
                MySqlDataReader reader = command.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(reader);
                name = dt.Rows[0]["name"].ToString();
                item_description = dt.Rows[0]["description"].ToString();
                price = dt.Rows[0]["price"].ToString();
                post_date = dt.Rows[0]["post_date"].ToString();
                hot = dt.Rows[0]["hot"].ToString();
                owner_id = dt.Rows[0]["owner_id"].ToString();
            }

            //Get owner info
            using (MySqlConnection conn1 = new MySqlConnection(connstr))
            {
                MySqlCommand command = conn1.CreateCommand();
                command.CommandText =
                    "SELECT * from weexchange.users where id='" + owner_id + "';";
                conn1.Open();
                MySqlDataReader reader = command.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(reader);
                owner_name = dt.Rows[0]["user_name"].ToString();
            }

            ShowItem(sender, e);
            ShowImage(sender, e);
            ShowComments(sender, e);
            CommentList(sender, e);
        }

        protected void ShowItem(object sender, EventArgs e)
        {
            description.InnerHtml = "";
            string html = "<a href=\"/PersonalPage/PersonalPage.aspx?user_id="+ owner_id + "\"style=\"color:#3d3c3a\">"+ 
                              "<div class=\"owner\">" +
                            //"<img src = \"DisplayProfile.jsp?user_id=<%=owner_id%>\" width=\"40px\" style=\"border-radius: 20px;\">" + 
                              "<div class=\"owner_name\">" + owner_name + "</div>" + 
                              "</div>" + 
                          "</a>" + 
                          "<div class=\"item_name\">" + name + "</div>" + 
                          "<p class=\"item_description\">" + item_description + "</p>" + 
                          "<div class=\"item_price\">&yen" + price + "</div>" + 
                          "<div class=\"hot\">HOT: " + hot + "</div>" + 
                          "<div class=\"post_date\">Post On<br>" + post_date + "</div>";

            description.InnerHtml += html;
           
        }

        protected void ShowImage(object sender, EventArgs e)
        {
            //< img src = "DisplayItemImage.jsp?image_id=<%=image_ids[i]%>" alt = "1" >

            int alt = 1;
            wrap.InnerHtml = "";
            string sql = "select * from images where item_id='" + item_id + "';";

            using (MySqlConnection conn = new MySqlConnection(connstr))
            {
                MySqlCommand command = conn.CreateCommand();
                command.CommandText = sql;
                conn.Open();
                MySqlDataReader reader = command.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(reader);

                string item = "";

                foreach (DataRow row in dt.Rows)
                {
                    string img_url = "/DisplayImage/ItemImage.aspx?image_id=" + row["id"].ToString();

                    item = "<img id=\"img\" runat=\"server\" src=\"" + img_url + "\" alt=\"" + alt + "\">";

                    wrap.InnerHtml += item;
                    alt++;
                }
                wrap.InnerHtml += item;
            }
        }

        protected void ShowComments(object sender, EventArgs e)
        {

            if (Session["UserName"] == null)
            {
                Response.Redirect("../LoginPage/Login.aspx");

            }
            else
            {
                userid = Session["UserName"].ToString();
                string sql = "select * from users where id='" + userid + "';";

                using (MySqlConnection con = new MySqlConnection(connstr))
                {
                    MySqlCommand command = con.CreateCommand();
                    command.CommandText = sql;
                    con.Open();
                    MySqlDataReader reader = command.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(reader);
                    string user_name = dt.Rows[0]["user_name"].ToString();
                    con.Close();

                }
                profile.ImageUrl = "/DisplayImage/ProfileImage.aspx?user_id=" + userid;
            }

        }
        protected void SubmitComments(object sender, EventArgs e)
        {
            ///MySql数据库连接语句
            string connection = ConfigurationManager.ConnectionStrings["test1ConnectionString"].ConnectionString;
            MySqlConnection conn = new MySqlConnection(connection);
            conn.Open();

            string comment = comment_input.Text;

            //MySql查询语句
            string sqlQuery = "select * from comments";

            //执行查询
            MySqlCommand cmd = new MySqlCommand(sqlQuery, conn);
            MySqlDataAdapter data = new MySqlDataAdapter();
            data.SelectCommand = cmd;

            //将查询结果注入到dataset Ds中
            DataSet Ds = new DataSet();
            data.Fill(Ds, "comments");
            DataTable myTable = Ds.Tables["comments"];

            // 添加一行
            DataRow myRow = myTable.NewRow();
            myRow["content"] = comment;
            myRow["post_date"] = DateTime.Now.ToString();
            myRow["owner_id"] = Session["UserName"].ToString();
            myRow["item_id"] = item_id;
            myTable.Rows.Add(myRow);

            // 将DataSet的修改提交至“数据库”
            MySqlCommandBuilder mySqlCommandBuilder = new MySqlCommandBuilder(data);
            data.Update(Ds, "comments");
            conn.Close();
            //Response.Redirect("ItemPage.aspx");
            Response.Redirect(Request.Url.ToString());

        }
        protected void CommentList(object sender, EventArgs e)
        {
            commentList.InnerHtml = "";
            string span = "";
            string comment = "";
            string footer = "";
            string first = "";
            string last = "";
            string text = "";
            int comments_count = 1;

            string sql = "select content,user_name,comments.post_date as com_postdate,comments.id as comment_id,comments.owner_id as com_ownerid from comments,users,items where item_id='" + item_id + "' and users.id = comments.owner_id and items.id = comments.item_id;";

            using (MySqlConnection con = new MySqlConnection(connstr))
            {
                MySqlCommand command = con.CreateCommand();
                command.CommandText = sql;
                con.Open();
                MySqlDataReader reader = command.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(reader);
                //判断此item是否有评论
                if (dt != null)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        content = row["content"].ToString();
                        comment_post_date = row["com_postdate"].ToString();
                        content_owner = row["user_name"].ToString();
                        content_owner_id = row["com_ownerid"].ToString();
                        comment_id = row["comment_id"].ToString();


                        first = "<div class=\"comment\">";
                        Control control1 = ParseControl(first);
                        commentList.Controls.Add(control1);
                        
                        span = "<span class=\"comment_avatar\"> <asp:Image runat=\"server\" CommandName=\"" + content_owner_id + "\" ImageUrl =\" /DisplayImage/ProfileImage.aspx?user_id=" + content_owner_id +"\" /></span> ";
                        Control control2 = ParseControl(span);
                        commentList.Controls.Add(control2);
                        ImageButton open = control2.Controls[0] as ImageButton;
                        if (open != null)
                        {
                            open.Command += new CommandEventHandler(OpenProfile);
                        }
                        

                        comment = "<div class=\"comment_content\"><p class=\"content_name\">" + content_owner + "</p><p class=\"content_article\">" + content;
                        Control control3 = ParseControl(comment);
                        commentList.Controls.Add(control3);


                        if (Session["UserName"].ToString().Equals(content_owner_id))
                        {
                            last = "<asp:Button runat =\"server\" ID=\"comment_debut" + comments_count + "\" class=\"delete_button\" CommandName=\"" + comment_id + "\" text=\"Delete\"/>";
                            Control control4 = ParseControl(last);
                            commentList.Controls.Add(control4);
                            Button delete = control4.Controls[0] as Button;
                            Console.WriteLine(delete);
                            delete.Command += new CommandEventHandler(DeletComment);
                        }

                        footer = "</p><p class=\"content_footer\"><span class=\"footer_id\">#" + comments_count + "</span><span class=\"footer_timestamp\">" + comment_post_date + "</span></p></div><div class=\"cls\"></div></div>";

                        Control control5 = ParseControl(footer);
                        commentList.Controls.Add(control5);

                        text = "<input type=\"text\" style=\"display: none\" name=\"comment_id\" value=\"" + comment_id + "\"/><input type = \"text\" style = \"display: none\" name = \"owner_id\" value = \"" + content_owner_id + "\"/><input type = \"text\" style = \"display: none\" name = \"item_id\" value = \"" + item_id + "\"/>";
                        comments_count++;
                    }

                }

                con.Close();
            }
        }


        protected void OpenProfile(object sender, CommandEventArgs e)
        {
            string id = e.CommandName;
            Response.Redirect("/PersonalPage/PersonalPage.aspx?user_id=" + id);
        }
        protected void DeletComment(Object sender, CommandEventArgs e)
        {

            string id = e.CommandName;
            string sql = "delete from comments where id='" + id + "';";
            using (MySqlConnection con = new MySqlConnection(connstr))
            {
                MySqlCommand command = con.CreateCommand();
                command.CommandText = sql;
                con.Open();
                System.Diagnostics.Debug.WriteLine(command.CommandText);
                command.ExecuteNonQuery();
                con.Close();
            }
            Response.Redirect(Request.Url.ToString());
        }
    }
}