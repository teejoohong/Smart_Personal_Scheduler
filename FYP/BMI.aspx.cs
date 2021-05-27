using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FYP
{
    public partial class BMI : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string absoluteurl = HttpContext.Current.Request.Url.AbsoluteUri;
                Label1.Text = absoluteurl;
                HyperLink1.Text = absoluteurl;
                HyperLink1.NavigateUrl = absoluteurl;
            }
        }
    }
}