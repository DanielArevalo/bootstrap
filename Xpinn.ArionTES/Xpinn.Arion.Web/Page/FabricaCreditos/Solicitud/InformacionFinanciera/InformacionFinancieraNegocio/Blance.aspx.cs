using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;

public partial class Page_FabricaCreditos_Solicitud_InformacionFinanciera_InformacionFinancieraNegocio_Default : System.Web.UI.Page
{
    private Xpinn.FabricaCreditos.Services.EstadosFinancierosService EstadosFinancierosServicio = new Xpinn.FabricaCreditos.Services.EstadosFinancierosService();
    Xpinn.FabricaCreditos.Entities.EstadosFinancieros InformacionFinanciera = new Xpinn.FabricaCreditos.Entities.EstadosFinancieros();
    protected void Page_PreInit(object sender, EventArgs e)
    {
       
        Site1 toolBar = (Site1)this.Master;
        toolBar.eventoAdelante += btnAdelante_Click;
        toolBar.eventoAtras += btnAtras_Click;  
        ((Label)Master.FindControl("lblNombresApellidos")).Text = Session["Nombres"].ToString();
        ((Label)Master.FindControl("lblIdCliente")).Text = Session["Identificacion"].ToString();

        ImageButton btnAdelante = Master.FindControl("btnAdelante") as ImageButton;
        btnAdelante.ValidationGroup = "";
        
        btnAdelante.ImageUrl = "~/Images/btnCrearSolicitud.jpg";

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["TipoCredito"].ToString() == "M")
            {            
                Xpinn.FabricaCreditos.Entities.EstadosFinancieros InformacionFinanciera = new Xpinn.FabricaCreditos.Entities.EstadosFinancieros();
                InformacionFinanciera = EstadosFinancierosServicio.listarperosnainfofin(Convert.ToInt64(Session["Cod_persona"].ToString()), (Usuario)Session["Usuario"]);
            }
        }

    }

    protected void btnAtras_Click(object sender, ImageClickEventArgs e)
    {
        if (Session["TipoCredito"].ToString() == "C")
        {
            Response.Redirect("~/Page/FabricaCreditos/Solicitud/InformacionFamiliar/Conyuge/Nuevo.aspx");
        }
        else
            Response.Redirect("~/Page/FabricaCreditos/Solicitud/InformacionFinanciera/ComposicionPasivo/Lista.aspx");
 
    }

    protected void btnAdelante_Click(object sender, ImageClickEventArgs e)
    {
        guardar();

        Response.Redirect("~/Page/FabricaCreditos/Solicitud/DatosSolicitud/SolicitudCredito.aspx");
    }

    public void guardar()
    {

         Xpinn.FabricaCreditos.Entities.EstadosFinancieros InformacionFinanciera = new Xpinn.FabricaCreditos.Entities.EstadosFinancieros();
       
        InformacionFinanciera.cod_persona = Convert.ToInt64(Session["Cod_persona"].ToString());
        if (Session["Cod_persona_conyuge"] != null)
            InformacionFinanciera.cod_personaconyuge = Convert.ToInt64(Session["Cod_persona_conyuge"].ToString());

        EstadosFinancierosServicio.guardarIngreEgre(InformacionFinanciera, (Usuario)Session["Usuario"]);
 
    }


}