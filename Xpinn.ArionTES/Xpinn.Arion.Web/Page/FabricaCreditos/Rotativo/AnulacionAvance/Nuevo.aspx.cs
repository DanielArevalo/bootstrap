using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using System.IO;
using System.Configuration;
using System.Globalization;
using System.Text;
using System.Web.UI.HtmlControls;
using Xpinn.FabricaCreditos.Services;
using Xpinn.FabricaCreditos.Entities;


public partial class Nuevo : GlobalWeb
{

    AvanceService AvancServices = new AvanceService();
    CreditoService CreditoServicio = new CreditoService();
    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(AvancServices.CodigoProgramaAnulAvances, "E");

            Site toolBar = (Site)this.Master;
            toolBar.eventoCancelar += btnCancelar_Click;
          
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AvancServices.GetType().Name + "L", "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
             //   txtFechaDesem.Text = Convert.ToString(DateTime.Now);
                CargarDropdown();
                mvAplicar.ActiveViewIndex = 0;
                txtCodigo.Enabled = false;
                txtFechaSoli.Enabled = false;
                txtValorSoli.Enabled = false;
                panel1.Enabled = false;

                if (Session[AvancServices.CodigoProgramaAnulAvances + ".id"] != null)
                {
                    idObjeto = Session[AvancServices.CodigoProgramaAnulAvances + ".id"].ToString();
                    Session.Remove(AvancServices.CodigoProgramaAnulAvances + ".id");
                    ObtenerDatos(idObjeto);
                }
               
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AvancServices.GetType().Name + "L", "Page_Load", ex);
        }

    }

 
    void CargarDropdown()
    {
        ctlTasaInteres.Inicializar();
        MotivoService motivoServicio = new MotivoService();
        Motivo motivo = new Motivo();
        ddlNegar.DataSource = motivoServicio.ListarMotivosFiltro(motivo, (Usuario)Session["usuario"], 2);
        ddlNegar.DataTextField = "Descripcion";
        ddlNegar.DataValueField = "Codigo";
        ddlNegar.DataBind();
        ddlNegar.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));
    }  
    


    public void PoblarLista(string pTabla, DropDownList ddlControl)
    {
        PoblarLista(pTabla, "", "", "", ddlControl);
    }

    public void PoblarLista(string pTabla, string pColumnas, string pCondicion, string pOrden, DropDownList ddlControl)
    {
        List<Xpinn.Comun.Entities.ListaDesplegable> plista = new List<Xpinn.Comun.Entities.ListaDesplegable>();
        Xpinn.Comun.Entities.ListaDesplegable pentidad = new Xpinn.Comun.Entities.ListaDesplegable();
        Xpinn.Comun.Services.ListaDeplegableService pservicio = new Xpinn.Comun.Services.ListaDeplegableService();

        ddlControl.Items.Clear();
        plista = pservicio.ListarListaDesplegable(pentidad, pTabla, pColumnas, pCondicion, pOrden, (Usuario)Session["usuario"]);
        pentidad.idconsecutivo = null;
        pentidad.descripcion = "Seleccione un item";
        plista.Insert(0, pentidad);
        ddlControl.DataTextField = "descripcion";
        ddlControl.DataValueField = "idconsecutivo";
        ddlControl.DataSource = plista;
        ddlControl.DataBind();
    }



    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            Avance vDetalle = new Avance();
            String pIdObjeto2 = Convert.ToString(Session["numavanace"]);
            String pIdCredito = Convert.ToString(Session["numcredito"]);
            Avance vDetalleTasa = new Avance();
            Avance vDetallePlazo = new Avance();
            Avance vDetallePlazoMax = new Avance();

            vDetalle = AvancServices.ConsultarCredRotativoXaprobar(Convert.ToInt64(pIdObjeto2), (Usuario)Session["usuario"]);

            if (vDetalle.idavance != 0)
                txtCodigo.Text = vDetalle.idavance.ToString();
            txtNumavance.Text = txtCodigo.Text;
            if (vDetalle.cod_linea_credito != "")
                txtcodLineacredito.Text = vDetalle.cod_linea_credito;
            if (vDetalle.nomlinea != "")
                txtNomLinea.Text = vDetalle.nomlinea;
            if (vDetalle.numero_radicacion != 0)
                txtNumCredito.Text = vDetalle.numero_radicacion.ToString().Trim();
            if (vDetalle.nomoficina != "")
                txtOficina.Text = vDetalle.nomoficina;

            if (vDetalle.identificacion != "")
                txtIdentificacion.Text = vDetalle.identificacion;
            if (vDetalle.nombre != "")
                txtNombre.Text = vDetalle.nombre;

            if (vDetalle.fecha_solicitud != DateTime.MinValue)
                txtFechaSoli.Text = vDetalle.fecha_solicitud.ToShortDateString();
            if (vDetalle.valor_solicitado != 0)
                txtValorSoli.Text = vDetalle.valor_solicitado.ToString();

            if (vDetalle.fecha_aprobacion != DateTime.MinValue)
                txtFechaApro.Text = vDetalle.fecha_aprobacion.ToShortDateString();
            if (vDetalle.valor_aprobado != 0)
                txtValorApro.Text = vDetalle.valor_aprobado.ToString();
            else
                txtValorApro.Text = txtValorSoli.Text;


            if (vDetalle.descforma_pago != "")
                txtFormaPago.Text = vDetalle.descforma_pago;
            if (vDetalle.observacion != "")
                txtObservacion.Text = vDetalle.observacion;

            if (vDetalle.cod_deudor != 0)

                Session["codigocliente"] = vDetalle.cod_deudor;

            
            vDetalleTasa = AvancServices.ConsultarTasaCreditoTotativo(Convert.ToInt64(pIdCredito), (Usuario)Session["usuario"]);
            vDetallePlazo = AvancServices.ConsultarPlazoCreditoTotativo(Convert.ToString(txtNomLinea.Text), (Usuario)Session["usuario"]);
            vDetallePlazoMax = AvancServices.ConsultarPlazoMaximoCredito(Convert.ToInt64(pIdCredito), (Usuario)Session["usuario"]);
            

            if (vDetallePlazo.diferir == 1)
            {
                txtPlazo.Enabled = true;
                txtPlazo.Text = Convert.ToString(vDetalle.plazo_diferir);

            }

            if (vDetallePlazo.diferir == 0 && vDetalle.plazo_diferir == 0)
            {
                txtPlazo.Enabled = false;
                txtPlazo.Text = vDetallePlazoMax.plazo_maximo.ToString().Trim();
            }
            if (vDetallePlazo.diferir == 0 || vDetalle.plazo_diferir > 0)
            {
                txtPlazo.Text = Convert.ToString(vDetalle.plazo_diferir);

            }

            // para la tasa


            if (vDetalleTasa.calculo_atr != null)
            {
                if (!string.IsNullOrEmpty(vDetalleTasa.calculo_atr.ToString()))
                    ctlTasaInteres.FormaTasa = HttpUtility.HtmlDecode(vDetalleTasa.calculo_atr.ToString().Trim());
                if (!string.IsNullOrEmpty(vDetalleTasa.tipo_historico.ToString()))
                    ctlTasaInteres.TipoHistorico = Convert.ToInt32(HttpUtility.HtmlDecode(vDetalleTasa.tipo_historico.ToString().Trim()));
                if (!string.IsNullOrEmpty(vDetalleTasa.desviacion.ToString()))
                    ctlTasaInteres.Desviacion = Convert.ToDecimal(HttpUtility.HtmlDecode(vDetalleTasa.desviacion.ToString().Trim()));
                if (!string.IsNullOrEmpty(vDetalleTasa.tipo_tasa.ToString()))
                    ctlTasaInteres.TipoTasa = Convert.ToInt32(HttpUtility.HtmlDecode(vDetalleTasa.tipo_tasa.ToString().Trim()));
                if (!string.IsNullOrEmpty(vDetalleTasa.tasa.ToString()))
                    ctlTasaInteres.Tasa = Convert.ToDecimal(HttpUtility.HtmlDecode(vDetalleTasa.tasa.ToString().Trim()));
            }


        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AvancServices.CodigoProgramaAnulAvances, "ObtenerDatos", ex);
        }
    }


    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    } 
    
    public void MensajeFinal(string pmensaje)
    {
        mvAplicar.ActiveViewIndex = 1;
        lblmsj.Text = pmensaje;
    }

    protected void btnCncNegar_Click(object sender, EventArgs e)
    {
        txtObs.Text = "";
        ddlNegar.SelectedIndex = 0;
    }

    protected void btnContinuar_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }

    protected void btnAcpNegar_Click(object sender, EventArgs e)
    {

        VerError("");
        Usuario _usuarios;
        _usuarios = (Usuario)Session["usuario"];
        Avance avances = new Avance();
        Motivo motivo = new Motivo();
        Label lblUsuario = (Label)Master.FindControl("header1").FindControl("lblUser");

        motivo.Codigo = Int16.Parse(ddlNegar.SelectedValue);
        avances.observacion = txtObs.Text;
        avances.idavance = Int32.Parse(txtNumavance.Text);
        AvancServices.NegarAvances(avances, motivo, _usuarios);
        hiddenOperacionAvance.Value = ((int)TipoDocumentoCorreo.CreditoNegado).ToString();
        MensajeFinal("Avance " + txtNumavance.Text + " fue Negado");
        btnContinuar.Visible = false;

    }
}
