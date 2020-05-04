using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Web.UI;
using xpinnWSEstadoCuenta;

public partial class Servicios : GlobalWeb
{
    xpinnWSLogin.Persona1 pPersona;
    xpinnWSCredito.WSCreditoSoapClient BOCreditos = new xpinnWSCredito.WSCreditoSoapClient();
    xpinnWSEstadoCuenta.WSEstadoCuentaSoapClient service = new xpinnWSEstadoCuenta.WSEstadoCuentaSoapClient();
    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            Site toolBar = (Site)Master;
            VisualizarTitulo(OptionsUrl.EstadoCuenta, "Inf");
        }
        catch (Exception ex)
        {
            //BOexcepcion.Throw(Anulacionservicio.CodigoPrograma, "Page_PreInit", ex);
            VerError(ex.Message);
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            pPersona = (xpinnWSLogin.Persona1)Session["persona"];
            if (Session[pPersona.cod_persona + "NroProducto"] != null)
            {
                ViewState["pProducto"] = Session[pPersona.cod_persona + "NroProducto"].ToString();
                Session.Remove(pPersona.cod_persona + "NroProducto");
                ObtenerDatos(ViewState["pProducto"].ToString());
            }
            else
            {
                if (ViewState["pProducto"] != null)
                    ObtenerDatos(ViewState["pProducto"].ToString());
                else
                    Navegar("~/Pages/Asociado/EstadoCuenta/Detalle.aspx");
            }
        }
    }

    private void btnImprimir_Click(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }

    private void btnRegresar_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Detalle);
    }

    private void ObtenerDatos(string pProducto)
    {
        try
        {
            pPersona = (xpinnWSLogin.Persona1)Session["persona"];
            if (string.IsNullOrEmpty(pProducto.Trim()))
            {
                VerError("Se generó un error al realizar la consulta.");
                return;
            }
            //Realizando Consulta de Datos
            DateTime pFec = DateTime.Now;
            txtFecIni.Text = DateTime.ParseExact(pFec.ToString("dd/MM/yyyy"), "dd/MM/yyyy", null).AddMonths(-6).ToString("dd/MM/yyyy");
            txtFecFin.Text = DateTime.ParseExact(pFec.ToString("dd/MM/yyyy"), "dd/MM/yyyy", null).ToString("dd/MM/yyyy");

            List<xpinnWSEstadoCuenta.Persona1> lstInfo = new List<xpinnWSEstadoCuenta.Persona1>();
            DateTime pFecIniProduct = DateTime.MinValue;

                    try
                    {
                        bool ManejaClubAhorrador = ConfigurationManager.AppSettings["IncluirClubAhorradores"] == null ? true :
                            (ConfigurationManager.AppSettings["IncluirClubAhorradores"].ToString() == "1") ? false : true;
                                                
                        List<xpinnWSEstadoCuenta.Servicio> lstServicio = new List<xpinnWSEstadoCuenta.Servicio>();
                        string pFiltroServicio = " and SERVICIOS.ESTADO not in ('T') and Numero_Servicio = " + pProducto;
                        lstServicio = service.ListarServiciosClubAhorrador(pPersona.cod_persona, pFiltroServicio, ManejaClubAhorrador, pPersona.identificacion, pPersona.clavesinecriptar, Session["sec"].ToString());

                        if (lstServicio != null)
                        {
                            List<xpinnWSEstadoCuenta.Servicio> lstSrv = new List<xpinnWSEstadoCuenta.Servicio>();
                            Servicio apo = lstServicio.ElementAt(0);
                            lstSrv.Add(apo);
                            frmServicios.DataSource = lstSrv;
                            frmServicios.DataBind();
                            frmServicios.Visible = true;
                        }
                        Actualizar(ViewState["pProducto"].ToString());
                    }
                    catch (Exception e)
                    {
                        VerError("Se genero un error al Consultar el producto");
                        return;
                    }                        
        }
        catch (Exception ex) { VerError(ex.Message); }
    }

    private void Actualizar(string pProducto)
    {
        try
        {
            DateTime pFec = DateTime.Now;
            pPersona = (xpinnWSLogin.Persona1)Session["persona"];

            DateTime pFecIni = DateTime.ParseExact(txtFecIni.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime pFecFin = DateTime.ParseExact(txtFecFin.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);


            xpinnWSCredito.Servicio servicio = new xpinnWSCredito.Servicio();
            servicio.numero_servicio = Convert.ToInt32(pProducto);
            servicio.Fec_ini = pFecIni;
            servicio.Fec_fin = pFecFin;
            List<xpinnWSCredito.Servicio> lstServicio = BOCreditos.ReporteMovimientoServicio(servicio, Session["sec"].ToString());
            pGrilla.Visible = false;
            lblInfo.Visible = true;
            lblTotalReg.Visible = false;
            if (lstServicio != null && lstServicio.Count > 0)
            {
                pGrilla.Visible = true;
                lblInfo.Visible = false;
                lblTotalReg.Visible = true;
                lblTotalReg.Text = "<br/> Movimientos encontrados :" + lstServicio.Count;
                gvLista.DataSource = lstServicio;
                gvLista.DataBind();
            }            
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }


    protected void btnConsultar_Click1(object sender, EventArgs e)
    {
        try
        {
            VerError("");
            if (Page.IsValid)
            {
                Actualizar(ViewState["pProducto"].ToString());
            }
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }
}
                            
                           