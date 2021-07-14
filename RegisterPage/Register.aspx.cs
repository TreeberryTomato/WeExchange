using System;
using MySql.Data.MySqlClient;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace WeExchange.RegisterPage
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void CheckRegister(object sender, EventArgs e)
        {

            ///MySql数据库连接语句
            string connection = ConfigurationManager.ConnectionStrings["test1ConnectionString"].ConnectionString;
            MySqlConnection conn = new MySqlConnection(connection);
            conn.Open();

            string phonenumber = phone.Text;
            string user_name = username.Text;
            string true_name = name.Text;
            string uage = age.Text;
            string psd = password.Text;
            string uemail = email.Text;
            string ugender = gender.SelectedValue;

            //MySql查询语句
            string sqlQuery = "select * from users";

            //执行查询
            MySqlCommand cmd = new MySqlCommand(sqlQuery, conn);
            MySqlDataAdapter data = new MySqlDataAdapter();
            data.SelectCommand = cmd;

            //将查询结果注入到dataset Ds中
            DataSet Ds = new DataSet();
            data.Fill(Ds, "user");
            DataTable myTable = Ds.Tables["user"];

            // 添加一行
            DataRow myRow = myTable.NewRow();
            myRow["user_name"] = user_name;
            myRow["phone_number"] = phonenumber;
            myRow["real_name"] = true_name;
            myRow["age"] = uage;
            myRow["password"] = psd;
            myRow["email"] = uemail;
            myRow["contact_info"] = null;
            myRow["profile"] = null;
            myRow["gender"] = ugender;
            myRow["id"] = 64; //id若为“自动增长”，此处可以不设置，即便设置也无效
            myTable.Rows.Add(myRow);

            // 将DataSet的修改提交至“数据库”
            MySqlCommandBuilder mySqlCommandBuilder = new MySqlCommandBuilder(data);
            data.Update(Ds, "user");
            conn.Close();
        }
    }
}