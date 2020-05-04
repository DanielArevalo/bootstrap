using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public delegate void txtcolor_ActionsDelegate(object sender, EventArgs e);

public partial class ctlColorPicker : System.Web.UI.UserControl
{
    public event txtcolor_ActionsDelegate eventoCambiar;
   
    protected void Page_Load(object sender, EventArgs e){
        
    }

    
    public string Text
    {
        set { txtcolor.Text = value; }
        get { return txtcolor.Text; }

    }

    protected void txtcolor_TextChanged(object sender, EventArgs e)
    {
        if (eventoCambiar != null)
            eventoCambiar(sender, e);
    }

    public Unit Width_
    {
        get { return txtcolor.Width; }
        set { txtcolor.Width = value; }
    }
    
}