using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Data;
using System.Configuration;
using System.IO;
using System.Windows;

namespace WeExchange.PostPage
{
    public partial class PostPage : System.Web.UI.Page
    {
        public const String connstr = "server=localhost;Database=weexchange;uid=root;pwd=1234;charset=utf8";

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Post_Item(object sender, EventArgs e)
        {
            //Compute item id
            string itemId = "";

            using (MySqlConnection con = new MySqlConnection(connstr))
            {
                MySqlCommand command = con.CreateCommand();
                command.CommandText =
                    "SELECT * from weexchange.items;";
                con.Open();
                MySqlDataReader reader = command.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(reader);
                string name = dt.Rows[dt.Rows.Count-1]["id"].ToString();
                //itemId = (int.Parse(name)+1).ToString();
                con.Close();
            }

            itemId = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.DayOfWeek.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();

            //Compute all images' id and save them into a string
            int[] image_ids = new int[4] { -1, -1, -1, -1 };
            using (MySqlConnection con1 = new MySqlConnection(connstr))
            {
                MySqlCommand command = con1.CreateCommand();
                command.CommandText =
                    "SELECT * from weexchange.images;";
                con1.Open();
                MySqlDataReader reader = command.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(reader);
                string imageId = dt.Rows[dt.Rows.Count - 1]["id"].ToString();
                image_ids[0] = int.Parse(imageId) + 1;
                image_ids[1] = image_ids[0] + 1;
                image_ids[2] = image_ids[1] + 1;
                image_ids[3] = image_ids[2] + 1;

                //MessageBox.Show(image_ids[0] + " " + image_ids[1]);

                con1.Close();
            }

            //Get category id and total
            string category = itemcategory.Value;
            int category_id = 0;
            int total = 1;
            using (MySqlConnection con2 = new MySqlConnection(connstr))
            {
                MySqlCommand command = con2.CreateCommand();
                command.CommandText =
                    "SELECT * from weexchange.categories where name='" + category + "';";
                con2.Open();
                MySqlDataReader reader = command.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(reader);
                category_id = int.Parse(dt.Rows[0]["id"].ToString());
                total = int.Parse(dt.Rows[0]["total"].ToString());

                con2.Close();
            }

            //Get owner id (已登录状态取user's id）
            int owner_id = int.Parse(Session["UserName"].ToString());
            //int owner_id = 2;

            //Update total in categories
            total = total + 1;
            using (MySqlConnection con3 = new MySqlConnection(connstr))
            {
                MySqlCommand command = con3.CreateCommand();
                command.CommandText =
                    "update weexchange.categories set total = " + total + " where (id=" + category_id + ");";
                con3.Open();
                command.ExecuteNonQuery();
                con3.Close();
            }

            //Get name, tag, description, price
            string itemName = itemname.Text;
            string itemTag = itemtag.Value;
            string itemDescription = itemdescription.Text;
            string itemPrice = itemprice.Text;

            //Get post's datetime
            string datetime = DateTime.Now.ToLocalTime().ToString();

            //Compute tag id
            int tag;
            string t1 = "Totally new";
            string t2 = "80% or 90% new";
            if (itemTag.Equals(t1) == true) tag = 1;
            else if (itemTag.Equals(t2) == true) tag = 2;
            else tag = 3;

            //Insert basic information into database except images
            MySqlConnection con4 = new MySqlConnection(connstr);
            MySqlCommand cmd;
            try
            {   string item_pt = "INSERT INTO weexchange.items(id, category_id, name, description, price, post_date, owner_id, hot, tag, image_id) VALUES (@id, @category_id, @name, @description, @price, @post_date, @owner_id, @hot, @tag, @image_id)";

                cmd = new MySqlCommand(item_pt, con4);

                cmd.Parameters.Add("@id", MySqlDbType.VarChar, 45);
                cmd.Parameters.Add("@category_id", MySqlDbType.Int32);
                cmd.Parameters.Add("@name", MySqlDbType.VarChar, 50);
                cmd.Parameters.Add("@description", MySqlDbType.TinyText);
                cmd.Parameters.Add("@price", MySqlDbType.Float);
                cmd.Parameters.Add("@post_date", MySqlDbType.DateTime);
                cmd.Parameters.Add("@owner_id", MySqlDbType.Int32);
                cmd.Parameters.Add("@hot", MySqlDbType.Int32);
                cmd.Parameters.Add("@tag", MySqlDbType.Int32);
                cmd.Parameters.Add("@image_id", MySqlDbType.VarChar, 255);

                cmd.Parameters["@id"].Value = itemId;
                cmd.Parameters["@category_id"].Value = category_id;
                cmd.Parameters["@name"].Value = itemName;
                cmd.Parameters["@description"].Value = itemDescription;
                cmd.Parameters["@price"].Value = itemPrice;
                cmd.Parameters["@post_date"].Value = datetime;
                cmd.Parameters["@owner_id"].Value = owner_id;
                cmd.Parameters["@hot"].Value = 0;
                cmd.Parameters["@tag"].Value = tag;
                cmd.Parameters["@image_id"].Value = image_ids[0].ToString() + ',' + image_ids[1].ToString() + ',' + image_ids[2].ToString() + ',' + image_ids[3].ToString();

                con4.Open();

                int RowsAffected1 = cmd.ExecuteNonQuery();

                if (RowsAffected1 > 0)
                {
                    MessageBox.Show("Image1 saved sucessfully!");
                }
                con4.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (con4.State == ConnectionState.Open)
                {
                    con4.Close();
                }
            }

            // 将四张图片存储进数据库
            Insert_Image(sender,e,image_ids,itemId);
        }

        protected void Insert_Image(object sender, EventArgs e, int[] image_ids, string item_id)
        {
            MySqlConnection con = new MySqlConnection(connstr);
            MySqlConnection con2 = new MySqlConnection(connstr);
            MySqlConnection con3 = new MySqlConnection(connstr);
            MySqlConnection con4 = new MySqlConnection(connstr);
            BinaryReader br1, br2, br3, br4;

            try
            {
                string type1 = itemimage.PostedFile.ContentType;
                string type2 = itemimage2.PostedFile.ContentType;
                string type3 = itemimage3.PostedFile.ContentType;
                string type4 = itemimage4.PostedFile.ContentType;

                
                Stream sm1 = itemimage.PostedFile.InputStream;
                Stream sm2 = itemimage2.PostedFile.InputStream;
                Stream sm3 = itemimage3.PostedFile.InputStream;
                Stream sm4 = itemimage4.PostedFile.InputStream;

                MessageBox.Show(Server.MapPath(itemimage.FileName));

                byte[] ImageData1;
                byte[] ImageData2;
                byte[] ImageData3;
                byte[] ImageData4;

                br1 = new BinaryReader(sm1);
                br2 = new BinaryReader(sm2);
                br3 = new BinaryReader(sm3);
                br4 = new BinaryReader(sm4);
                
                ImageData1 = br1.ReadBytes((int)sm1.Length);
                ImageData2 = br2.ReadBytes((int)sm2.Length);
                ImageData3 = br3.ReadBytes((int)sm3.Length);
                ImageData4 = br4.ReadBytes((int)sm4.Length);
                
                br1.Close();
                br2.Close();
                br3.Close();
                br4.Close();

                sm1.Close();
                sm2.Close();
                sm3.Close();
                sm4.Close();

                con.Open();
                con2.Open();
                con3.Open();
                con4.Open();

                //MySql查询语句
                string sqlQuery = "select * from weexchange.images";

                //执行查询
                MySqlCommand cmd = new MySqlCommand(sqlQuery, con);
                MySqlDataAdapter data = new MySqlDataAdapter();
                data.SelectCommand = cmd;

                //将查询结果注入到dataset Ds中
                DataSet Ds = new DataSet();
                data.Fill(Ds, "image");
                DataTable myTable = Ds.Tables["image"];

                // 添加四行
                DataRow myRow = myTable.NewRow();
                myRow["id"] = image_ids[0];
                myRow["item_id"] = item_id;
                myRow["image_content"] = ImageData1;
                myTable.Rows.Add(myRow);

                DataRow myRow2 = myTable.NewRow();
                myRow2["id"] = image_ids[1];
                myRow2["item_id"] = item_id;
                myRow2["image_content"] = ImageData2;
                myTable.Rows.Add(myRow2);

                DataRow myRow3 = myTable.NewRow();
                myRow3["id"] = image_ids[2];
                myRow3["item_id"] = item_id;
                myRow3["image_content"] = ImageData3;
                myTable.Rows.Add(myRow3);

                DataRow myRow4 = myTable.NewRow();
                myRow4["id"] = image_ids[3];
                myRow4["item_id"] = item_id;
                myRow4["image_content"] = ImageData4;
                myTable.Rows.Add(myRow4);

                // 将DataSet的修改提交至“数据库”
                MySqlCommandBuilder mySqlCommandBuilder = new MySqlCommandBuilder(data);
                data.Update(Ds, "image");

                con.Close();
                con2.Close();
                con3.Close();
                con4.Close();
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                if (con2.State == ConnectionState.Open)
                {
                    con2.Close();
                }
                if (con3.State == ConnectionState.Open)
                {
                    con3.Close();
                }
                if (con4.State == ConnectionState.Open)
                {
                    con4.Close();
                }
            }
        }
    }
}