using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;

public partial class Lista : GlobalWeb
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                //ClientScript.RegisterStartupScript(this.GetType(), "AjustarMenu", "AjustarMenu();", true);
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}