using System;
using MySql.Data.MySqlClient;
using System.Data;
using System.Configuration;

namespace WeExchange
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {


        }
        protected void Checklogin(object sender, EventArgs e)
        {

            ///<summary>
            ///MySql数据库连接语句
            ///</summary>
            string connection = ConfigurationManager.ConnectionStrings["test1ConnectionString"].ConnectionString;
            MySqlConnection conn = new MySqlConnection(connection);
            conn.Open();

            string account = user_name.Text;
            string pwd = psd.Text;

            //MySql查询语句
            string sqlQuery = "select * from users where user_name='" + account + "'";

            //执行查询
            MySqlCommand cmd = new MySqlCommand(sqlQuery, conn);
            MySqlDataAdapter data = new MySqlDataAdapter();
            data.SelectCommand = cmd;

            //将查询结果注入到dataset Ds中
            DataSet Ds = new DataSet();
            data.Fill(Ds, "user");
            if (Ds == null || Ds.Tables.Count == 0 || (Ds.Tables.Count == 1 && Ds.Tables[0].Rows.Count == 0))
            {

                Response.Write("<script>alert('Account does not exit, please register.')</script>");
            }
            else
            {
                // 访问DataSet
                DataTable myTable = Ds.Tables["user"];
                foreach (DataRow myRow in myTable.Rows)
                {
                    if (Equals(myRow["password"], pwd))
                    {

                        Session["UserName"] = myRow["id"];
                        Response.Write("<script>alert('登录成功')</script>");

                    }
                    else
                    {
                        Response.Write("<script>alert('密码错误')</script>");
                    }
                }
            }


            conn.Close();
        }
    }
}