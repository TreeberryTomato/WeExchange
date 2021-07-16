﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WeExchange
{
    public partial class ProfileImage : System.Web.UI.Page
    {
        public const String connstr = "server=localhost;Database=weexchange;uid=root;pwd=1234;charset=utf8;pooling=True";
        MySqlConnection conn = new MySqlConnection(connstr);
        protected void Page_Load(object sender, EventArgs e)
        {
            conn.Open();
            Byte[] img = null;
            string user_id = Request["user_id"];
            MySqlCommand command = conn.CreateCommand();
            command.CommandText =
                "SELECT profile from users where id=" + user_id;
            MySqlDataReader imageReader = command.ExecuteReader();
            DataTable imageTable = new DataTable();
            imageTable.Load(imageReader);
            try
            {
                img = (Byte[])imageTable.Rows[0]["profile"];
            }
            catch(Exception exception)
            {
                System.Diagnostics.Debug.WriteLine(exception.StackTrace);
                img = new byte[0];
            }
            

            Response.BinaryWrite(img);
            conn.Close();
        }
    }
}