using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Page_Aportes_Biometria_Biometria : System.Web.UI.Page
{
    private Xpinn.FabricaCreditos.Services.Persona1Service TerceroServicio = new Xpinn.FabricaCreditos.Services.Persona1Service();
    private Xpinn.Aportes.Services.AfiliacionServices AfiliacionServicio = new Xpinn.Aportes.Services.AfiliacionServices();

    protected void Page_Load(object sender, EventArgs e)
    {
      
    }
    

}