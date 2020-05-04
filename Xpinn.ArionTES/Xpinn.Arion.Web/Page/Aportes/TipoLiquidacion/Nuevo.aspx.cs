using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using Xpinn.Aportes.Services;
using Xpinn.Aportes.Entities;

partial class Nuevo : GlobalWeb
{
    private TipoLiqAporteServices TipoLiquidacionServicio = new TipoLiqAporteServices();
    String operacion = "";
    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[TipoLiquidacionServicio.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(TipoLiquidacionServicio.CodigoPrograma, "E");
            else
                VisualizarOpciones(TipoLiquidacionServicio.CodigoPrograma, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(TipoLiquidacionServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            operacion = (String)Session["operacion"];
            if (idObjeto == "")
            {
                if (operacion == "N")
                {
                    ConsultarMaxLiquidacion();
                   
                 
                }
            }
            if (operacion == "")
            {

              
            }
            
            if (!IsPostBack)
            {
                // Llena el DDL de la periodicidad
                List<Xpinn.FabricaCreditos.Entities.Persona1> lstDatosSolicitud = new List<Xpinn.FabricaCreditos.Entities.Persona1>();
                Xpinn.FabricaCreditos.Services.Persona1Service DatosClienteServicio = new Xpinn.FabricaCreditos.Services.Persona1Service();
                String ListaSolicitada = "Periodicidad";
                lstDatosSolicitud = DatosClienteServicio.ListasDesplegables(ListaSolicitada, (Usuario)Session["Usuario"]);
                ddlPeriodicidad.DataSource = lstDatosSolicitud;
                ddlPeriodicidad.DataTextField = "ListaDescripcion";
                ddlPeriodicidad.DataValueField = "ListaIdStr";
                ddlPeriodicidad.DataBind();
                if (Session["Periodicidad"] != null)
                    ddlPeriodicidad.SelectedValue = Session["Periodicidad"].ToString();
                Session.Remove("Periodicidad");

                LlenarComboLineaAporteBase(ddlLineaBase);
                LlenarComboLineaAporteAfecta(ddlLineaAfecta);

                if (Session[TipoLiquidacionServicio.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[TipoLiquidacionServicio.CodigoPrograma + ".id"].ToString();
                    Session.Remove(TipoLiquidacionServicio.CodigoPrograma + ".id");
                    ObtenerDatos(idObjeto);
                }
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(TipoLiquidacionServicio.CodigoPrograma, "Page_Load", ex);
        }
    }


    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            TipoLiqAporte vTipoLiquidacion = new TipoLiqAporte();

            if (idObjeto != "")
                vTipoLiquidacion = TipoLiquidacionServicio.ConsultarTipoLiqAporte(Convert.ToInt64(idObjeto), (Usuario)Session["usuario"]);

            vTipoLiquidacion.tipo_liquidacion = Convert.ToInt32(txtTipoLiquidacion.Text.Trim());
            vTipoLiquidacion.nombre = Convert.ToString(txtDescripcion.Text.Trim());
            vTipoLiquidacion.cod_periodicidad = Convert.ToInt32(ddlPeriodicidad.SelectedValue);
            vTipoLiquidacion.tipo_saldo_base = Convert.ToInt32(ddlTipoSaldoBase.SelectedValue);
            vTipoLiquidacion.lineaaporte_base = Convert.ToInt32(ddlLineaBase.SelectedValue);
            vTipoLiquidacion.lineaaporte_afecta = Convert.ToInt32(ddlLineaAfecta.SelectedValue);
           
            
            if (idObjeto != "")
            {
                vTipoLiquidacion.tipo_liquidacion = Convert.ToInt32(idObjeto);
                TipoLiquidacionServicio.ModificarTipoLiqAporte(vTipoLiquidacion, (Usuario)Session["usuario"]);
            }
            else
            {
                vTipoLiquidacion = TipoLiquidacionServicio.CrearTipoLiqAporte(vTipoLiquidacion, (Usuario)Session["usuario"]);
                idObjeto = vTipoLiquidacion.tipo_liquidacion.ToString();
            }

            Session[TipoLiquidacionServicio.CodigoPrograma + ".id"] = idObjeto;
            Navegar(Pagina.Lista);
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(TipoLiquidacionServicio.CodigoPrograma, "btnGuardar_Click", ex);
        }
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }
    protected void LlenarComboLineaAporteBase(DropDownList ddlOficina)
    {
        ddlLineaBase.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));
        AporteServices aporteService = new AporteServices();
        Usuario usuap = (Usuario)Session["usuario"];
        Aporte aporte = new Aporte();
        ddlLineaBase.DataSource = aporteService.ListarLineaAporte(aporte, (Usuario)Session["usuario"]);
        ddlLineaBase.DataTextField = "nom_linea_aporte";
        ddlLineaBase.DataValueField = "cod_linea_aporte";
        ddlLineaBase.DataBind();
  

    }
    protected void LlenarComboLineaAporteAfecta(DropDownList ddlOficina)
    {
        ddlLineaAfecta.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));
        AporteServices aporteService = new AporteServices();
        Usuario usuap = (Usuario)Session["usuario"];
        Aporte aporte = new Aporte();
        ddlLineaAfecta.DataSource = aporteService.ListarLineaAporte(aporte, (Usuario)Session["usuario"]);
        ddlLineaAfecta.DataTextField = "nom_linea_aporte";
        ddlLineaAfecta.DataValueField = "cod_linea_aporte";
        ddlLineaAfecta.DataBind();
   

    }
    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            TipoLiqAporte vTipoLiquidacion = new TipoLiqAporte();
            vTipoLiquidacion = TipoLiquidacionServicio.ConsultarTipoLiqAporte(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);

            if (!string.IsNullOrEmpty(vTipoLiquidacion.tipo_liquidacion.ToString()))
                txtTipoLiquidacion.Text = HttpUtility.HtmlDecode(vTipoLiquidacion.tipo_liquidacion.ToString().Trim());

            if (!string.IsNullOrEmpty(vTipoLiquidacion.nombre))
                txtDescripcion.Text = HttpUtility.HtmlDecode(vTipoLiquidacion.nombre.ToString().Trim());

            if (!string.IsNullOrEmpty(vTipoLiquidacion.cod_periodicidad.ToString()))
                ddlPeriodicidad.SelectedValue = HttpUtility.HtmlDecode(vTipoLiquidacion.cod_periodicidad.ToString().Trim());

            if (!string.IsNullOrEmpty(vTipoLiquidacion.tipo_saldo_base.ToString()))
                ddlTipoSaldoBase.SelectedValue = HttpUtility.HtmlDecode(vTipoLiquidacion.tipo_saldo_base.ToString().Trim());            
            
            
            if (!string.IsNullOrEmpty(vTipoLiquidacion.lineaaporte_base.ToString()))
                ddlLineaBase.SelectedValue = HttpUtility.HtmlDecode(vTipoLiquidacion.lineaaporte_base.ToString().Trim());
           
            if (!string.IsNullOrEmpty(vTipoLiquidacion.lineaaporte_afecta.ToString()))
                ddlLineaAfecta.SelectedValue = HttpUtility.HtmlDecode(vTipoLiquidacion.lineaaporte_afecta.ToString().Trim());
           
          
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(TipoLiquidacionServicio.CodigoPrograma, "ObtenerDatos", ex);
        }
    }
    private void ConsultarMaxLiquidacion()
    {
        Int64 maxliquid = 0;
        Int64 numeroliquidacion = 1;
        TipoLiqAporteServices TipoLiquidacionServicio = new TipoLiqAporteServices();
        TipoLiqAporte tipoliquidacion = new TipoLiqAporte();
        tipoliquidacion = TipoLiquidacionServicio.ConsultarMax((Usuario)Session["usuario"]);

        if (!string.IsNullOrEmpty(tipoliquidacion.tipo_liquidacion.ToString()))
            maxliquid = tipoliquidacion.tipo_liquidacion + numeroliquidacion;
        this.txtTipoLiquidacion.Text = Convert.ToInt64(maxliquid).ToString();

    }

    

    
}