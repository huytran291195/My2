using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Connect
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            testconnect();
        }
        public void testconnect()
        {
            Data data = new Data();
            List<string> vs = new List<string>{ "đâ","đâ","dấddasd"};
           
            DataTable dt = data.GetData_DataTable("testx", vs);
            DataTable dt2 = data.GetData_DataTable("testx2", vs);


            int k;
        }
    }
}