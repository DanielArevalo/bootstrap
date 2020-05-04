using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class TextBoxValor : System.Web.UI.UserControl
{
   
    protected void Page_Load(object sender, EventArgs e)
    {
        TxtValor.Attributes["onblur"] = "blur(this.value)";
    }

  
}