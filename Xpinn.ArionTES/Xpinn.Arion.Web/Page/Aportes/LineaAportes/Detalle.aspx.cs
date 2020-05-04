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
using Xpinn.FabricaCreditos.Services;
using Xpinn.FabricaCreditos.Entities;
using Xpinn.Comun.Services;
using Xpinn.Comun.Entities;
using Xpinn.Aportes.Services;
using Xpinn.Aportes.Entities;
using System.Data.Common;

public partial class Nuevo : GlobalWeb
{
    Xpinn.FabricaCreditos.Services.Persona1Service DatosClienteServicio = new Xpinn.FabricaCreditos.Services.Persona1Service();
    private Xpinn.Aportes.Services.GrupoLineaAporteServices LineaAporteServicio = new Xpinn.Aportes.Services.GrupoLineaAporteServices();
    private Xpinn.Aportes.Services.TipoProductoServices TipoProductoServicio = new Xpinn.Aportes.Services.TipoProductoServices();
 
    List<Xpinn.FabricaCreditos.Entities.Persona1> lstDatosSolicitud = new List<Xpinn.FabricaCreditos.Entities.Persona1>();  //Lista de los menus desplegables
    String ListaSolicitada = null;  // Cadena en la que se indica la lista a solicitar

    String lineaaporte = "";
 

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(LineaAporteServicio.CodigoProgramaLineas, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoEditar += btnEditar_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
            //toolBar.eventoLimpiar += btnLimpiar_Click;
           
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(LineaAporteServicio.CodigoProgramaLineas, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                LlenarComboTipoLiquidacion(Ddltipoliquidacion);
                LlenarComboTipoProducto(DdlTipoProducto);
                CargarListas();
                Usuario usuap = (Usuario)Session["usuario"];
                Int64 oficina = Convert.ToInt64(usuap.cod_oficina);

                if (Session[LineaAporteServicio.CodigoProgramaLineas + ".id"] != null)
                {
                    idObjeto = Session[LineaAporteServicio.CodigoProgramaLineas.ToString() + ".id"].ToString();
                    Session.Remove(LineaAporteServicio.CodigoProgramaLineas.ToString() + ".id");
                    ObtenerDatos(idObjeto);
                }

                lineaaporte = (String)Session["lineaaporte"];
                if (lineaaporte == "N")
                {
                    ConsultarMaxAporte();
                    lineaaporte = "0";
                }

            }
        }

        catch (Exception ex)
        {
            BOexcepcion.Throw(LineaAporteServicio.CodigoProgramaLineas, "Page_Load", ex);
        }
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
         this.grabar();
        
 
    }
    protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    {
        GuardarValoresConsulta(pConsulta, LineaAporteServicio.CodigoProgramaLineas);
        Navegar(Pagina.Nuevo);
    }
    protected void btnEditar_Click(object sender, ImageClickEventArgs e)
    {
        Session[LineaAporteServicio.CodigoProgramaLineas + ".id"] = idObjeto;

        Navegar(Pagina.Nuevo);

    }
    public void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        GuardarValoresConsulta(pConsulta, LineaAporteServicio.CodigoProgramaLineas);
        Navegar(Pagina.Lista);
    }

   
    private void ConsultarMaxAporte()
    {
        Int64 maxaporte = 0;
        Int64 numeroaporte=1;
        Xpinn.Aportes.Services.AporteServices AportesServicio = new Xpinn.Aportes.Services.AporteServices();
        Xpinn.Aportes.Entities.Aporte aporte = new Xpinn.Aportes.Entities.Aporte();        
        aporte = AportesServicio.ConsultarMaxAporte((Usuario)Session["usuario"]);

        if (!string.IsNullOrEmpty(aporte.numero_aporte.ToString()))
         maxaporte = aporte.numero_aporte + numeroaporte;
        //this.Linea.Text = Convert.ToInt64(maxaporte).ToString();
       
    }
       

    protected void LlenarComboPeriodicidad(DropDownList DdlPeriodicidad)
    {
        PeriodicidadService periodicidadService = new PeriodicidadService();
        Usuario usuap = (Usuario)Session["usuario"];
        Periodicidad periodicidad = new Periodicidad();
        DdlPeriodicidad.DataSource = periodicidadService.ListarPeriodicidad(periodicidad, (Usuario)Session["usuario"]);
        DdlPeriodicidad.DataTextField = "Descripcion";
        DdlPeriodicidad.DataValueField = "Codigo";
        DdlPeriodicidad.DataBind();
        DdlPeriodicidad.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));

    }
    protected void LlenarComboFormaPago(DropDownList DdlFormaPago)
    {
        FormaPagoService formadepagoService = new FormaPagoService();
        Usuario usuap = (Usuario)Session["usuario"];
        FormaPago formapago = new FormaPago();
        DdlFormaPago.DataSource = formadepagoService.ListarFormaPago(formapago, (Usuario)Session["usuario"]);
        DdlFormaPago.DataTextField = "Descripcion";
        DdlFormaPago.DataValueField = "Codigo";
        DdlFormaPago.DataBind();
        DdlFormaPago.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));

    }

    protected void LlenarComboTipoProducto(DropDownList  DdlTipoProducto)
    {
       
        TipoProductoServices TipoProductoService = new TipoProductoServices();
        Usuario usuap = (Usuario)Session["usuario"];
        TipoProducto tipoproducto = new TipoProducto();
        DdlTipoProducto.DataSource = TipoProductoService.ListarTipoProducto(tipoproducto, (Usuario)Session["usuario"]);
        DdlTipoProducto.DataTextField = "nombre";
        DdlTipoProducto.DataValueField = "cod_tipo_prod";
        DdlTipoProducto.DataBind();
       // DdlTipoProducto.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));

    }

    protected void LlenarComboTipoLiquidacion(DropDownList DdlFormaPago)
    {
        TipoLiqAporteServices tipoliquidacionService= new TipoLiqAporteServices();
        Usuario usuap = (Usuario)Session["usuario"];
        TipoLiqAporte tipoliquid= new TipoLiqAporte();
        Ddltipoliquidacion.DataSource = tipoliquidacionService.ListarTipoLiqAporte(tipoliquid, (Usuario)Session["usuario"]);
        Ddltipoliquidacion.DataTextField = "nombre";
        Ddltipoliquidacion.DataValueField = "tipo_liquidacion";
        Ddltipoliquidacion.DataBind();
       //Ddltipoliquidacion.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));

    }

    protected void LlenarComboTipoTasa(DropDownList DdlTipoTasa)
    {
        TipoTasaService tipotasaService = new TipoTasaService();
        Usuario usuap = (Usuario)Session["usuario"];
        TipoTasa tipotasa = new TipoTasa();
        DdlTipoTasa.DataSource = tipotasaService.ListarTipoTasa(tipotasa, (Usuario)Session["usuario"]);
        DdlTipoTasa.DataTextField = "nombre";
        DdlTipoTasa.DataValueField = "cod_tipo_tasa";
        DdlTipoTasa.DataBind();
        //DdlTipoTasa.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));

    }


    protected void LlenarComboTasaHistorica(DropDownList DdlTipoHistorico)
    {
        TipoTasaHistService tipotasahistService = new TipoTasaHistService();
        Usuario usuap = (Usuario)Session["usuario"];
        TipoTasaHist tipotasa = new TipoTasaHist();
        DdlTipoHistorico.DataSource = tipotasahistService.ListarTipoTasaHist(tipotasa, (Usuario)Session["usuario"]);
        DdlTipoHistorico.DataTextField = "Descripcion";
        DdlTipoHistorico.DataValueField = "tipo_historico";
        DdlTipoHistorico.DataBind();
        DdlTipoHistorico.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));

    }
    private void TraerResultadosLista()
    {

        lstDatosSolicitud.Clear();
        lstDatosSolicitud = DatosClienteServicio.ListasDesplegables(ListaSolicitada, (Usuario)Session["Usuario"]);

    }
    private void CargarListas()
    {
        try
        {

            ListaSolicitada = "TipoIdentificacion";
            TraerResultadosLista();
      

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(DatosClienteServicio.GetType().Name + "L", "CargarListas", ex);
        }
    }


    protected void ObtenerDatos(String pIdObjeto)
    {

        try
        {
            GrupoLineaAporte lineaaporte = new GrupoLineaAporte();
            if (pIdObjeto != null)
            {
                lineaaporte.cod_linea_aporte = Int32.Parse(pIdObjeto);
                lineaaporte = LineaAporteServicio.ConsultarLineaAporte(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);


                if (!string.IsNullOrEmpty(lineaaporte.cod_linea_aporte.ToString()))
                {
                    this.TxtCodLinea.Text = lineaaporte.cod_linea_aporte.ToString();
                    this.TxtLinea.Text = lineaaporte.nombre.ToString();
                    txtDistribucion.Text = lineaaporte.porcentaje.ToString();
                    this.DdlTipoProducto.SelectedValue = lineaaporte.tipo_aporte.ToString();
                    DdlTipoCuota.SelectedValue= lineaaporte.tipo_cuota.ToString();
                    this.Ddltipoliquidacion.SelectedValue = lineaaporte.tipo_liquidacion.ToString();
                    Txtcuotaminima.Text = lineaaporte.valor_cuota_minima.ToString();
                    Txtcuotamaxima.Text = lineaaporte.valor_cuota_maximo.ToString();
                    Txtretirominimo.Text = lineaaporte.min_valor_retiro.ToString();
                    Txtretiromaximo.Text = lineaaporte.max_valor_retiro.ToString();
                    TxtSaldoMinimo.Text = lineaaporte.saldo_minimo.ToString();
                    Txtsaldominliquid.Text = lineaaporte.saldo_minimo_Liqui.ToString();
                    txtCruce.Text = lineaaporte.porcentaje_cruce.ToString();
                    Txtretiromaximo.Text = lineaaporte.max_valor_retiro.ToString();
                    if (lineaaporte.distribuye == 1)
                    {
                        chkDistribuye.Checked = true;

                    }
                    if (lineaaporte.cruzar == 0)
                    {
                        ChkcruceNO.Checked = true;
                    }
                    if (lineaaporte.cruzar == 1)
                    {
                        ChkcruceSI.Checked = true;
                    }

                    if (lineaaporte.estado== 1)
                    {
                        Ckhactiva.Checked = true;
                    }
                    if (lineaaporte.estado == 2)
                    {
                        Ckhinactiva.Checked = true;
                   
                    }
                    if (lineaaporte.estado == 3)
                    {
                        Ckhcerrada.Checked = true;

                    }
                }

            }

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(LineaAporteServicio.GetType().Name + "A", "ObtenerDatos", ex);
        }
    }
    private void grabar()
    {
        Usuario usuap = new Usuario();
        try
        {
            GrupoLineaAporte lineaaporte = new GrupoLineaAporte();
            lineaaporte.cod_linea_aporte = Convert.ToInt64(TxtCodLinea.Text);
            lineaaporte.nombre = Convert.ToString(TxtLinea.Text);
            if (chkDistribuye.Checked == true)
            {
                lineaaporte.distribuye = 1;
            }
            else
            {
                lineaaporte.distribuye = 0;
            }
            if (txtDistribucion.Text == "")
            {
                 lineaaporte.porcentaje_distrib=0;
            }
            else
            {
                lineaaporte.porcentaje_distrib = Int64.Parse(txtDistribucion.Text);
            }
            lineaaporte.tipo_aporte = Int64.Parse(DdlTipoProducto.SelectedValue);
            lineaaporte.tipo_cuota = Int64.Parse(DdlTipoCuota.SelectedValue);
            lineaaporte.valor_cuota_minima = Int64.Parse(Txtcuotaminima.Text);
            lineaaporte.valor_cuota_maximo = Int64.Parse(Txtcuotamaxima.Text);
            lineaaporte.obligatorio = 0;//confirmar
            lineaaporte.tipo_liquidacion = Int64.Parse(Ddltipoliquidacion.SelectedValue); ;
            lineaaporte.min_valor_pago = 0;//confirmar
            lineaaporte.min_valor_retiro = Int64.Parse(Txtretirominimo.Text);
            lineaaporte.saldo_minimo = Int64.Parse(TxtSaldoMinimo.Text);
            lineaaporte.saldo_minimo_Liqui = Int64.Parse(Txtsaldominliquid.Text);
            lineaaporte.valor_cierre = 0;//confirmar
            lineaaporte.dias_cierre = 0;//confirmar
            if (ChkcruceSI.Checked == true)
            {
                lineaaporte.cruzar = 1;

            }
            if (ChkcruceNO.Checked == true)
            {
                lineaaporte.cruzar = 0;
            }
            if (txtCruce.Text == "")
            {
                lineaaporte.porcentaje_cruce = 0;
            }
            else
            {
                lineaaporte.porcentaje_cruce = Int64.Parse(txtCruce.Text);
            }
            lineaaporte.cobra_mora = 0;//confirmar
            lineaaporte.provisionar = 0;//confirmar
            lineaaporte.permite_retiros = 0;//confirmar
            lineaaporte.permite_traslados = 0;//confirmar
            lineaaporte.porcentaje_minimo = 0;//confirmar
            lineaaporte.porcentaje_maximo = 0;//confirmar

            lineaaporte.max_valor_retiro = Int64.Parse(Txtretiromaximo.Text);         
      
          
            if (Ckhactiva.Checked == true)
            {
                lineaaporte.estado = 1;
            }
            if (Ckhinactiva.Checked == true)
            {
                lineaaporte.estado = 2;
            }

            if (Ckhcerrada.Checked == true)
            {
                lineaaporte.estado = 3;
            }
            if (idObjeto != "")
            {
                lineaaporte.cod_linea_aporte = Convert.ToInt64(idObjeto);
                LineaAporteServicio.ModificarLineaAporte(lineaaporte, (Usuario)Session["usuario"]);
            }
            else
            {
                lineaaporte = LineaAporteServicio.CrearLineaAporte(lineaaporte, (Usuario)Session["usuario"]);

            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(LineaAporteServicio.GetType().Name, "btnGuardar_Click", ex);
        }
        Navegar(Pagina.Lista);
    }

   

    protected void Ckhactiva_CheckedChanged(object sender, EventArgs e)
    {
        Ckhinactiva.Checked = false;
        Ckhcerrada.Checked = false;
        if (Ckhactiva.Checked == true)
        { 
            Ckhinactiva.Checked = false;
            Ckhcerrada.Checked = false;
        }
       
    }
    protected void ChkcruceSI_CheckedChanged(object sender, EventArgs e)
    {
        ChkcruceNO.Checked = false;
        if(ChkcruceSI.Checked == true)
        {
            ChkcruceNO.Checked = false;
        }
       
    }
    protected void ChkcruceNO_CheckedChanged(object sender, EventArgs e)
    {
        ChkcruceSI.Checked = false;
        if (ChkcruceNO.Checked == true)
        {
            ChkcruceSI.Checked = false;
        }
    }
    protected void Ckhinactiva_CheckedChanged(object sender, EventArgs e)
    {
        Ckhactiva.Checked = false;
        Ckhcerrada.Checked = false;
        if (Ckhinactiva.Checked == true)
        {
            Ckhactiva.Checked = false;
            Ckhcerrada.Checked = false;
        }
        
    }
    protected void Ckhcerrada_CheckedChanged(object sender, EventArgs e)
    {
        Ckhactiva.Checked = false;
        Ckhinactiva.Checked = false;
        if (Ckhcerrada.Checked == true)
        {
            Ckhactiva.Checked = false;
            Ckhinactiva.Checked = false;
        }      
        
        
    }
}