using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WeExchange
{
    public partial class ItemImage : System.Web.UI.Page
    {
        public const String connstr = "server=localhost;Database=weexchange;uid=root;pwd=1234;charset=utf8;pooling=True";
        MySqlConnection conn = new MySqlConnection(connstr);
        protected void Page_Load(object sender, EventArgs e)
        {
            conn.Open();
            Byte[] img = null;
            string image_id = Request["image_id"];
            MySqlCommand command = conn.CreateCommand();
            command.CommandText =
                "SELECT image_content from images where id=" + image_id;
            MySqlDataReader imageReader = command.ExecuteReader();
            DataTable imageTable = new DataTable();
            imageTable.Load(imageReader);
            img = (Byte[])imageTable.Rows[0]["image_content"];

            Response.BinaryWrite(img);
            conn.Close();
        }
    }
}