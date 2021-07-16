using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql;
using MySql.Data.MySqlClient;

namespace WeExchange
{
    public partial class CategoryPage : System.Web.UI.Page
    {
        public const String connstr = "server=localhost;Database=weexchange;uid=root;pwd=1234;charset=utf8";
        public string category_id;
        protected void Page_Load(object sender, EventArgs e)
        {
            category_id = Request["category_id"];
            using (MySqlConnection conn = new MySqlConnection(connstr))
            {
                MySqlCommand command = conn.CreateCommand();
                command.CommandText = 
                    "SELECT name, total from categories where id=" + category_id;
                conn.Open();
                MySqlDataReader reader = command.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(reader);
                string name = dt.Rows[0]["name"].ToString();
                category_title.InnerText = name;
            }
            AddItems(sender, e);
        }
        public void AddItems(object sender, EventArgs e)
        {
            grid_table.InnerHtml = "";
            string sql = "";
            int degree_id = degree_list.SelectedIndex;

            if (degree_id == 0)
            {
                sql = "SELECT id, name, description, price, post_date, " +
                    "owner_id, hot, tag, image_id from items where category_id="+category_id;
            }
            else
            {
                sql = "SELECT id, name, description, price, post_date, " +
                    "owner_id, hot, tag, image_id from items where category_id="+category_id+
                    " and tag="+degree_id;
            }

            string order = " ORDER BY ";
            switch (select.SelectedIndex)
            {
                case 0:
                    order += "hot ASC";
                    break;
                case 1:
                    order += "hot DESC";
                    break;
                case 2:
                    order += "price ASC";
                    break;
                case 3:
                    order += "price DESC";
                    break;
                case 4:
                    order += "post_date ASC";
                    break;
                case 5:
                    order += "post_date DESC";
                    break;
                default:
                    order = "";
                    break;
            }
            sql = sql + order;

            using (MySqlConnection conn = new MySqlConnection(connstr))
            {
                MySqlCommand command = conn.CreateCommand();
                command.CommandText = sql;
                conn.Open();
                MySqlDataReader reader = command.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(reader);

                foreach(DataRow row in dt.Rows)
                {
                    string img_url = "/ItemImage.aspx?image_id="+row["image_id"].ToString().Split(',')[0].Trim();
                    
                    
                    string item =
                        "<div class=\"grid_item\">" +
                            "<a id=\"link\" style=\"text-decoration: none\" href=\"/ItemPage/itemPage.aspx?item_id=" + row["id"].ToString() + "\">" +
                                "<img id=\"img\" runat=\"server\" style=\"width: 100%;\" src=/DisplayImage/" + img_url + ">" +
                                "<div class=\"title\">" + row["name"].ToString() + "</div>" +
                                "<div class=\"price\">&yen " + row["price"].ToString() + "</div>" +
                                "<div class=\"hot\">HOT: " + row["hot"].ToString() + "</div>" +
                                "<p class=\"description\">" + row["description"].ToString() + "</p>" +
                                "<div class=\"post_date\">Post On<br/>" + row["post_date"].ToString() + "</div>" +
                            "</a>" +
                        "</div>";

                    grid_table.InnerHtml += item;
                }
            }
        }
    }    
}