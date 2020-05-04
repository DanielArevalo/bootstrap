using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Web.UI;
using xpinnWSEstadoCuenta;

public partial class Aportes : GlobalWeb
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

            //Realizando Consulta de Datos
            xpinnWSEstadoCuenta.Persona1 pEntidad = new xpinnWSEstadoCuenta.Persona1();
            pEntidad.cod_persona = pPersona.cod_persona;
            pEntidad.identificacion = pPersona.identificacion;
            pEntidad.nombre = pPersona.nombre;
            pEntidad.direccion = pPersona.direccion;
            pEntidad.telefono = pPersona.telefono;
            pEntidad.descripcion = pProducto;

            List<xpinnWSEstadoCuenta.Persona1> lstInfo = new List<xpinnWSEstadoCuenta.Persona1>();
            DateTime pFecIniProduct = DateTime.MinValue;

                    try
                    {
                        pEntidad.tipocontrato = "Aportes";
                        bool ManejaClubAhorrador = ConfigurationManager.AppSettings["IncluirClubAhorradores"] == null ? true :
                            (ConfigurationManager.AppSettings["IncluirClubAhorradores"].ToString() == "1") ? false : true;

                        string pFiltro = string.Empty;
                        pFiltro = " And v_aportes.estado in (1) and v_aportes.numero_aporte =" + ViewState["pProducto"].ToString() + " ";
                        List<xpinnWSEstadoCuenta.Aporte> lstConsulta = new List<xpinnWSEstadoCuenta.Aporte>();
                        lstConsulta = service.ListarAportesEstadoCuenta(Convert.ToInt64(pPersona.cod_persona), ManejaClubAhorrador, pFiltro, DateTime.Now);
                        if (lstConsulta != null)
                        {
                            List<xpinnWSEstadoCuenta.Aporte> lstAporte = new List<xpinnWSEstadoCuenta.Aporte>();
                            Aporte apo = lstConsulta.ElementAt(0);
                            lstAporte.Add(apo);
                            frmAportes.DataSource = lstAporte;
                            frmAportes.DataBind();
                            frvData.Visible = false;
                            frmAportes.Visible = true;
                        }
                        Actualizar(ViewState["pProducto"].ToString());
                    }
                    catch (Exception e)
                    {
                        VerError("Se genero un error al Consultar el producto");
                        return;
                    }
                

            lstInfo.Add(pEntidad);
            frvData.DataSource = lstInfo;
            frvData.DataBind();            

        }
        catch (Exception ex) { VerError(ex.Message); }
    }

    private void VerificarGridView(int pTipoProd)
    {
        bool esCredito = pTipoProd == 2 ? true : false;
        gvMovimiento.Columns[3].Visible = esCredito;
        gvMovimiento.Columns[10].Visible = esCredito;
        gvMovimiento.Columns[11].Visible = esCredito;
        gvMovimiento.Columns[12].Visible = esCredito;
        gvMovimiento.Columns[13].Visible = esCredito;
        gvMovimiento.Columns[14].Visible = esCredito;
        gvMovimiento.Columns[15].Visible = esCredito;
        gvMovimiento.Columns[16].Visible = esCredito;
    }

    private void Actualizar(string pProducto)
    {
        try
        {
            DateTime pFec = DateTime.Now;
            pPersona = (xpinnWSLogin.Persona1)Session["persona"];
            List<Object> lstMov = new List<Object>();

            DateTime pFecIni = DateTime.ParseExact(txtFecIni.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime pFecFin = DateTime.ParseExact(txtFecFin.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            List<xpinnWSCredito.MovimientoAporte> lstAportes = BOCreditos.ListarMovAporte(Convert.ToInt64(pProducto), pFecIni, pFecFin, pPersona.identificacion, pPersona.clavesinecriptar, Session["sec"].ToString());
            if (lstAportes != null)
            {
                var movProd = from mp in lstAportes
                              orderby mp.FechaPago, mp.FechaCuota, mp.CodOperacion
                              select new
                              {
                                  num_producto = mp.numero_aporte,
                                  mp.num_comp,
                                  tipo_comp = mp.tipo_comp,
                                  FechaPago = mp.FechaPago.ToShortDateString(),
                                  FechaCuota = mp.FechaCuota.ToShortDateString(),
                                  TipoOperacion = mp.CodOperacion,
                                  Transaccion = mp.TipoOperacion,
                                  tipomovimiento = mp.TipoMovimiento,
                                  dias_mora = 0, //Solo credito
                                  num_cuota = 0, //Solo credito
                                  saldo = mp.Saldo,
                                  capital = mp.Capital,
                                  IntCte = 0, //Solo credito
                                  IntMora = 0, //Solo credito
                                  LeyMiPyme = 0, //Solo credito
                                  ivaMiPyme = 0, //Solo credito
                                  Poliza = 0, //Solo credito
                                  otros = 0, //Solo credito
                                  prejuridico = 0, //Solo credito
                                  totalpago = ((mp.TipoOperacion == "Pagos Caja") ? 0 : mp.Capital),
                                  calificacion = 0, //Solo credito
                                  seguros = 0 //Solo credito
                              };

                foreach (var node in movProd)
                {
                    lstMov.Add(node);
                }
            }            
            

            pGrilla.Visible = false;
            lblInfo.Visible = true;
            lblTotalReg.Visible = false;
            if (lstMov.Count > 0)
            {
                pGrilla.Visible = true;
                lblInfo.Visible = false;
                lblTotalReg.Visible = true;
                lblTotalReg.Text = "<br/> Movimientos encontrados :" + lstMov.Count;
                gvMovimiento.DataSource = lstMov;
                gvMovimiento.DataBind();
                VerificarGridView(1);
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

