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
using Xpinn.FabricaCreditos.Entities;

partial class Nuevo : GlobalWeb
{
    private Xpinn.FabricaCreditos.Services.LineasCreditoService LineasCreditoServicio = new Xpinn.FabricaCreditos.Services.LineasCreditoService();
    private List<Xpinn.FabricaCreditos.Entities.Persona1> lstDatosSolicitud = new List<Xpinn.FabricaCreditos.Entities.Persona1>();  //Lista de los menus desplegables
    private String ListaSolicitada = null;  // Cadena en la que se indica la lista a solicitar
    private String operacion="";

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[LineasCreditoServicio.CodigoPrograma + ".CodAtr"] != null)
                VisualizarOpciones(LineasCreditoServicio.CodigoPrograma, "E");
            else
                VisualizarOpciones(LineasCreditoServicio.CodigoPrograma, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(LineasCreditoServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                lblCodLineaCredito.Text = Session[LineasCreditoServicio.CodigoPrograma + ".LineaCredito"].ToString();
                CargarListas();
                ddldescuentostipo_SelectedIndexChanged(null, null);
                if (Session[LineasCreditoServicio.CodigoPrograma + ".CodAtr"] != null)
                {
                    idObjeto = Session[LineasCreditoServicio.CodigoPrograma + ".CodAtr"].ToString();
                    ddlatributo.Enabled = false;
                    ObtenerDatos(idObjeto);
                }
                else
                {                    
                    ddlatributo.Enabled = true; 
                }
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(LineasCreditoServicio.CodigoPrograma, "Page_Load", ex);
        }
    }

    private void Limpiar()
    {
        txtvalor.Text = "";
        txtcuotas.Text = "";
        CheckBox.Checked = false;
    }

    private void CargarListas()
    {
        try
        {            
            ddlatributo.DataSource = LineasCreditoServicio.ddlatributo((Usuario)Session["Usuario"]);
            ddlatributo.DataTextField = "nombre";
            ddlatributo.DataValueField = "cod_atr";
            ddlatributo.DataBind();

            ddlimpuestos.DataSource = LineasCreditoServicio.ddlimpuestos((Usuario)Session["Usuario"]);
            ddlimpuestos.DataTextField = "nombre";
            ddlimpuestos.DataValueField = "cod_atr";
            ddlimpuestos.Items.Insert(0, new ListItem("<Seleccione un Item>", ""));
            ddlimpuestos.DataBind();

            GlobalWeb glob = new GlobalWeb();
            ddldescuentostipo.DataSource = glob.ListaCreditoTipoDeDescuento();
            ddldescuentostipo.DataTextField = "descripcion";
            ddldescuentostipo.DataValueField = "codigo";
            ddldescuentostipo.DataBind();

            ddlliquidacion.DataSource = glob.ListaCreditoTipoDeLiquidacion();
            ddlliquidacion.DataTextField = "descripcion";
            ddlliquidacion.DataValueField = "codigo";
            ddlliquidacion.DataBind();

            ddldescuentosforma.DataSource = glob.ListaCreditoFormadeDescuento();
            ddldescuentosforma.DataTextField = "descripcion";
            ddldescuentosforma.DataValueField = "codigo";
            ddldescuentosforma.DataBind();

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(LineasCreditoServicio.GetType().Name + "L", "CargarListas", ex);
        }
    }


    private Boolean ValidarDatos()
    {
        VerError("");
        LineasCredito vLineasCredito = new LineasCredito();
        vLineasCredito = LineasCreditoServicio.ConsultarDeducciones(lblCodLineaCredito.Text, Convert.ToInt32(ddlatributo.SelectedValue), (Usuario)Session["usuario"]);
        if (idObjeto == "")
        {
            if (Convert.ToInt64(ddlatributo.SelectedValue) == vLineasCredito.cod_atr)
            {
                VerError("Ya existe un registro con el mismo atributo");
                return false;
            }
        }
        if (txtvalor.Text == "")
        {
            VerError("Ingrese el Valor");
            return false;
        }
            
        return true;
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            if (ValidarDatos())
            {
                Xpinn.FabricaCreditos.Entities.LineasCredito vLineasCredito = new Xpinn.FabricaCreditos.Entities.LineasCredito();
                vLineasCredito = LineasCreditoServicio.ConsultarDeducciones(lblCodLineaCredito.Text, Convert.ToInt32(ddlatributo.SelectedValue) ,(Usuario)Session["usuario"]);
                String ValidacionLinea = vLineasCredito.cod_linea_credito;

                vLineasCredito.cod_linea_credito = lblCodLineaCredito.Text;
                vLineasCredito.cod_atr = Convert.ToInt64(ddlatributo.SelectedValue);
                vLineasCredito.tipoliquidacion = ddlliquidacion.SelectedValue;
                vLineasCredito.valor1 = Convert.ToDecimal(txtvalor.Text);
                if (CheckBox.Checked)
                    vLineasCredito.cobra_mora = 1;
                else
                    vLineasCredito.cobra_mora = 0;

                if (ChkModifica.Checked)
                    vLineasCredito.modifica = 1;
                else
                    vLineasCredito.modifica = 0;


                vLineasCredito.numero_cuotas = txtcuotas.Text;
                vLineasCredito.Formadescuento = ddldescuentosforma.SelectedValue;
                vLineasCredito.tipoimpuesto = ddlimpuestos.SelectedValue;
                vLineasCredito.tiposdescuento = ddldescuentostipo.SelectedValue;

                if (ValidacionLinea != "" && ValidacionLinea != null)
                {                    
                    LineasCreditoServicio.modificardeducciones(vLineasCredito, (Usuario)Session["Usuario"]);
                }
                else
                {
                    LineasCreditoServicio.Creardeducciones(vLineasCredito, (Usuario)Session["Usuario"]);
                }

                Session[LineasCreditoServicio.CodigoPrograma + ".id"] = lblCodLineaCredito.Text;
                Navegar(Pagina.Nuevo);
            }           

        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(LineasCreditoServicio.CodigoPrograma, "btnGuardar_Click", ex);
        }
    }

    private void TraerResultadosLista()
    {
        lstDatosSolicitud.Clear();
        lstDatosSolicitud = LineasCreditoServicio.ListasDesplegables(ListaSolicitada, (Usuario)Session["Usuario"]);
    }

    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Session[LineasCreditoServicio.CodigoPrograma + ".id"] = lblCodLineaCredito.Text;
        Navegar(Pagina.Nuevo);
    }

    protected void ObtenerDatos(string IdObjeto)
    {
        try
        {
            LineasCredito vLineasCredito = new LineasCredito();
            int pAtributo = 0;
            if (IdObjeto != null)
            {
                if (IdObjeto.ToString() != "")
                    pAtributo = Convert.ToInt32(IdObjeto.ToString());
            }            

            vLineasCredito = LineasCreditoServicio.ConsultarDeducciones(lblCodLineaCredito.Text, pAtributo, (Usuario)Session["Usuario"]);

            if (vLineasCredito.cod_linea_credito != "" && vLineasCredito.cod_linea_credito != null)
                lblCodLineaCredito.Text = vLineasCredito.cod_linea_credito;

            if (vLineasCredito.cod_atr != Int64.MinValue)
                ddlatributo.SelectedValue = HttpUtility.HtmlDecode(vLineasCredito.cod_atr.ToString().Trim());

            if (vLineasCredito.tipo_liquidacion != Int64.MinValue)
                ddlliquidacion.SelectedValue = HttpUtility.HtmlDecode(vLineasCredito.tipo_liquidacion.ToString().Trim());

            if (vLineasCredito.tipo_descuento != Int64.MinValue)
                ddldescuentostipo.SelectedValue = HttpUtility.HtmlDecode(vLineasCredito.tipo_descuento.ToString().Trim());

            if (vLineasCredito.Forma_descuento != Int64.MinValue)
                ddldescuentosforma.SelectedValue = HttpUtility.HtmlDecode(vLineasCredito.Forma_descuento.ToString().Trim());

            if (vLineasCredito.valor1 != 0)
                txtvalor.Text = HttpUtility.HtmlDecode(vLineasCredito.valor1.ToString().Trim());

            if (vLineasCredito.numero_cuotas1 != 0)
                txtcuotas.Text = HttpUtility.HtmlDecode(vLineasCredito.numero_cuotas1.ToString().Trim());

            if (vLineasCredito.cobra_mora != 0)
                CheckBox.Checked = true;
            else
                CheckBox.Checked = false;


            if (vLineasCredito.modifica>0)
                ChkModifica.Checked = true;
            else
                ChkModifica.Checked = false;

                
            if (vLineasCredito.cod_tipo_impuesto != Int64.MinValue)
                ddlimpuestos.SelectedValue = HttpUtility.HtmlDecode(vLineasCredito.cod_tipo_impuesto.ToString().Trim());

            ddldescuentostipo_SelectedIndexChanged(null, null);
                        
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(LineasCreditoServicio.CodigoPrograma, "ObtenerDatos", ex);
        }
    }

    protected void ddldescuentostipo_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddldescuentostipo.SelectedValue == "0" || ddldescuentostipo.SelectedValue == "1")
        {
            lblTipoLiquidacion.Visible = false;
            ddlliquidacion.Visible = false;
        }
        else
        {
            lblTipoLiquidacion.Visible = true;
            ddlliquidacion.Visible = true;
        }
    }

    protected void ddlatributo_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddldescuentostipo.SelectedValue == "0" || ddldescuentostipo.SelectedValue == "1")
        {
            lblTipoLiquidacion.Visible = false;
            ddlliquidacion.Visible = false;
        }
    }


}