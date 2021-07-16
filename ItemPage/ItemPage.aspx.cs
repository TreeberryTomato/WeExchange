using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MySql.Data.MySqlClient;

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
        }

        protected void ShowItem(object sender, EventArgs e)
        {
            description.InnerHtml = "";
            string html = "<a href=\"\"style=\"color:#3d3c3a\">"+ 
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
                    string img_url = "/ItemImage.aspx?image_id=" + row["id"].ToString();

                    item = "<img id=\"img\" runat=\"server\" src=\"" + img_url + "\" alt=\"" + alt + "\">";

                    wrap.InnerHtml += item;
                    alt++;
                }
                wrap.InnerHtml += item;
            }
        }
    }
}