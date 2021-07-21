using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WeExchange
{
    public partial class Base : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Session["UserName"]!=null)
            {
                register_login.Visible = false;
                online_logout.Visible = true;
            }
            else
            {
                register_login.Visible = true;
                online_logout.Visible = false;
            }
        }
        protected void Logout(object sender, EventArgs e)
        {
            Session["UserName"] = null;
            Response.Redirect("/HomePage/HomePage.aspx");
        }
    }
}