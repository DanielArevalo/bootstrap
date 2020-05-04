using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Globalization;
using System.Configuration;
using System.Collections;
using System.Text;
using System.Web.UI.HtmlControls;
public partial class Lista : GlobalWeb
{
    private Xpinn.Caja.Services.TransaccionCajaService transaccionService = new Xpinn.Caja.Services.TransaccionCajaService();
    private Xpinn.Caja.Entities.TransaccionCaja transacCaja = new Xpinn.Caja.Entities.TransaccionCaja();

    private Xpinn.Caja.Services.ReintegroService reintegroService = new Xpinn.Caja.Services.ReintegroService();
    private Xpinn.Caja.Entities.Reintegro reintegro = new Xpinn.Caja.Entities.Reintegro();

    private Xpinn.Caja.Services.HorarioOficinaService HorarioService = new Xpinn.Caja.Services.HorarioOficinaService();
    private Xpinn.Caja.Entities.HorarioOficina horario = new Xpinn.Caja.Entities.HorarioOficina();

    private Xpinn.Caja.Services.CajeroService cajeroService = new Xpinn.Caja.Services.CajeroService();
    private Xpinn.Caja.Entities.Cajero cajero = new Xpinn.Caja.Entities.Cajero();


    Xpinn.Caja.Services.MovimientoCajaService movCajaServicio = new Xpinn.Caja.Services.MovimientoCajaService();
    Xpinn.Caja.Entities.MovimientoCaja movCaja = new Xpinn.Caja.Entities.MovimientoCaja();
    List<Xpinn.Caja.Entities.MovimientoCaja> lstConsultaCheque = new List<Xpinn.Caja.Entities.MovimientoCaja>();

    Usuario user = new Usuario();
    List<Xpinn.Caja.Entities.TransaccionCaja> lstConsulta = new List<Xpinn.Caja.Entities.TransaccionCaja>();

    protected void Page_PreInit(object sender, System.EventArgs e)    
    {
        try
        {
            VisualizarOpciones(transaccionService.CodigoProgramaRepReversiones, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoCancelar += btnCancelar_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoExportar += btnExportar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(transaccionService.GetType().Name + "A", "Page_PreInit", ex);
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        
    
        try
        {
            if (!IsPostBack)
            {
                LlenarComboOficinas(ddlOficinas);

                txtFecha.Text = DateTime.Today.ToShortDateString();
                 txtFechaFinal.Text = DateTime.Today.ToShortDateString();
                mvReversion.ActiveViewIndex = 0;

                // ObtenerDatos();


             //  Actualizar(); 
            }
            Actualizar();
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(transaccionService.GetType().Name + "A", "Page_Load", ex);
        }
    }

    protected void LlenarComboMonedas(DropDownList ddlMonedas)
    {

        Xpinn.Caja.Services.TipoMonedaService monedaService = new Xpinn.Caja.Services.TipoMonedaService();
        Xpinn.Caja.Entities.TipoMoneda moneda = new Xpinn.Caja.Entities.TipoMoneda();
        ddlMonedas.DataSource = monedaService.ListarTipoMoneda(moneda, (Usuario)Session["Usuario"]);
        ddlMonedas.DataTextField = "descripcion";
        ddlMonedas.DataValueField = "cod_moneda";
        ddlMonedas.DataBind();
    }

    protected void LlenarComboOficinas(DropDownList ddlOficinas)
    {

        Xpinn.Caja.Services.OficinaService oficinaService = new Xpinn.Caja.Services.OficinaService();
        Xpinn.Caja.Entities.Oficina oficina = new Xpinn.Caja.Entities.Oficina();
        ddlOficinas.DataSource = oficinaService.ListarOficina(oficina, (Usuario)Session["usuario"]);
        ddlOficinas.DataTextField = "nombre";
        ddlOficinas.DataValueField = "cod_oficina";
        ddlOficinas.DataBind();
        consultar_cajeros();
        ddlOficinas_SelectedIndexChanged(ddlOficinas, null);
    }

    protected void btnCancelar_Click(object sender, ImageClickEventArgs e)
    {
        Navegar("../../../General/Global/inicio.aspx");
    }

   
    protected void ObtenerDatos()
    {
        try
        {
            reintegro = reintegroService.ConsultarCajero((Usuario)Session["usuario"]);
            if (!string.IsNullOrEmpty(reintegro.fechareintegro.ToString()))
                txtFecha.Text = reintegro.fechareintegro.ToShortDateString();

            if (!string.IsNullOrEmpty(reintegro.nomcajero))
               this.ddlCajero.SelectedItem.Text= reintegro.nomcajero.ToString().Trim();

            if (!string.IsNullOrEmpty(reintegro.cod_oficina.ToString()))
                Session["Oficina"] = reintegro.cod_oficina.ToString().Trim();
            if (!string.IsNullOrEmpty(reintegro.cod_caja.ToString()))
                Session["Caja"] = reintegro.cod_caja.ToString().Trim();
            if (!string.IsNullOrEmpty(reintegro.cod_cajero.ToString()))
                Session["Cajero"] = reintegro.cod_cajero.ToString().Trim();
           

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(reintegroService.GetType().Name + "A", "ObtenerDatos", ex);
        }
    }

    private void consultar_cajeros()
    {
        Usuario usuap = (Usuario)Session["usuario"];
        ddlCajero.Visible = true;
        Labelcajeros.Visible = true;
        Xpinn.Caja.Entities.Cajero ejec = new Xpinn.Caja.Entities.Cajero();
        long iOficina = (usuap.cod_oficina);
        ejec.cod_oficina = Convert.ToInt64(ddlOficinas.SelectedValue);
        ddlCajero.DataSource = cajeroService.ListarCajeroPorOficina(ejec, (Usuario)Session["usuario"]);
        ddlCajero.DataTextField = "nom_cajero";
        ddlCajero.DataValueField = "cod_cajero";
        ddlCajero.DataBind();
        ddlCajero.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));

    
    }


    public void Actualizar()
    {
        try
        {
            transacCaja.cod_cajero = Convert.ToInt64(ddlCajero.SelectedValue);
            transacCaja.cod_oficina = Convert.ToInt64(ddlOficinas.SelectedValue);
            transacCaja.fecha_movimiento = Convert.ToDateTime(txtFecha.Text);
            transacCaja.fecha_movimientofinal = Convert.ToDateTime(this.txtFechaFinal.Text);
            if (transacCaja.cod_cajero > 0)
            {
                lstConsulta = transaccionService.ListarOperacionesAnuladas(transacCaja, (Usuario)Session["usuario"]);//, (UserSession)Session["user"]);

                gvOperacion.DataSource = lstConsulta;

                if (lstConsulta.Count > 0)
                {
                    gvOperacion.Visible = true;
                    gvOperacion.DataBind();
                }
                else
                {
                    gvOperacion.Visible = false;
                }
            }
            else
            {
                lstConsulta = transaccionService.ListarOperacionesAnuladasSincajero(transacCaja, (Usuario)Session["usuario"]);//, (UserSession)Session["user"]);

                gvOperacion.DataSource = lstConsulta;

                if (lstConsulta.Count > 0)
                {
                    gvOperacion.Visible = true;
                    gvOperacion.DataBind();
                }
                else
                {
                    gvOperacion.Visible = false;
                }
            }

            Session.Add(transaccionService.GetType().Name + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(transaccionService.GetType().Name + "L", "Actualizar", ex);
        }
    }



    protected void ddlOficinas_SelectedIndexChanged(object sender, EventArgs e)
    {

        consultar_cajeros();
    }


    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        Actualizar();
    }

    protected void btnExportar_Click(object sender, EventArgs e)
    {
        ExportToExcel(gvOperacion);
    }

    protected void ExportToExcel(GridView GridView1)
    {
                if (gvOperacion.Rows.Count > 0)
                {
                    StringBuilder sb = new StringBuilder();
                    StringWriter sw = new StringWriter(sb);
                    HtmlTextWriter htw = new HtmlTextWriter(sw);
                    Page pagina = new Page();
                    dynamic form = new HtmlForm();
                    gvOperacion.EnableViewState = false;
                    pagina.EnableEventValidation = false;
                    pagina.DesignerInitialize();
                    pagina.Controls.Add(form);
                    form.Controls.Add(gvOperacion);
                    pagina.RenderControl(htw);
                    Response.Clear();
                    Response.Buffer = true;
                    Response.ContentType = "application/vnd.ms-excel";
                    Response.AddHeader("Content-Disposition", "attachment;filename=data.xls");
                    Response.Charset = "UTF-8";
                    Response.ContentEncoding = Encoding.Default;
                    Response.Write(sb.ToString());
                    Response.End();
                }
                else
                    VerError("Se debe generar el reporte primero");

          
        
    }

}