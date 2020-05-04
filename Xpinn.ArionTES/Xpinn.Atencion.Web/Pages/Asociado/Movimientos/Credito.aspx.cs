using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Web.UI;
using xpinnWSEstadoCuenta;

public partial class Credito : GlobalWeb
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
            xpinnWSEstadoCuenta.Persona1 pEntidad = new xpinnWSEstadoCuenta.Persona1();
            pEntidad.cod_persona = pPersona.cod_persona;
            pEntidad.identificacion = pPersona.identificacion;
            pEntidad.nombre = pPersona.nombre;
            pEntidad.direccion = pPersona.direccion;
            pEntidad.telefono = pPersona.telefono;
            pEntidad.descripcion = pProducto;
            long avances = 0;

            List<xpinnWSEstadoCuenta.Persona1> lstInfo = new List<xpinnWSEstadoCuenta.Persona1>();
            //Realizando Consulta de Datos
            DateTime pFec = DateTime.Now;
            txtFecIni.Text = DateTime.ParseExact(pFec.ToString("dd/MM/yyyy"), "dd/MM/yyyy", null).AddMonths(-6).ToString("dd/MM/yyyy");
            txtFecFin.Text = DateTime.ParseExact(pFec.ToString("dd/MM/yyyy"), "dd/MM/yyyy", null).ToString("dd/MM/yyyy");
            DateTime pFecIniProduct = DateTime.MinValue;

            try
            {
                xpinnWSCredito.CreditoPlan pEntidadCred = BOCreditos.ConsultarInformacionCreditos(Convert.ToInt64(pProducto), false, Session["sec"].ToString());
                pEntidad.tipocontrato = "Crédito";
                //pEntidad.estado = pEntidadCred.nom_estado;
                pEntidad.fechacreacion = pEntidadCred.FechaDesembolso;
                pEntidad.medio = pEntidadCred.Linea;
                if (pEntidadCred.FechaDesembolso != DateTime.MinValue)
                    pFecIniProduct = pEntidadCred.FechaDesembolso;
                if (pEntidadCred.tipo_linea == 2)
                {
                    avances = pEntidadCred.Numero_radicacion;
                }
                else
                {
                    btnPlanPagos.Visible = true;
                }
            }
            catch (Exception ex)
            {
                VerError("Se genero un error al Consultar el producto" + ex.Message);
                return;
            }
            
            lstInfo.Add(pEntidad);
            frvData.DataSource = lstInfo;
            frvData.DataBind();
            if (avances > 0)
            {
                string empresa = ConfigurationManager.AppSettings["Empresa"].ToString();
                if (empresa == "COOTREGUA")
                {
                    Actualizar(pProducto);
                }
                else
                {
                    cargarAvances(avances);
                    pnlFiltros.Visible = false;
                }
            }
            else
            {
                Actualizar(pProducto);
            }

        }
        catch (Exception ex) { VerError(ex.Message); }
    }

    private void cargarAvances(long radicado)
    {
        List<xpinnWSCredito.ConsultaAvance> lstAvance = BOCreditos.ListarAvancesCredito(radicado, "", Session["sec"].ToString());
        if (lstAvance != null && lstAvance.Count > 0)
        {
            gvAvances.DataSource = lstAvance;
            gvAvances.DataBind();
            lblTotalReg.Visible = false;
            pGrilla.Visible = false;
            pnlFiltros.Visible = false;
        }
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



            List<xpinnWSCredito.MovimientoProducto> lstCredito = BOCreditos.ListarMovCreditos(Convert.ToInt64(pProducto), 1, pPersona.identificacion, pPersona.clavesinecriptar, Session["sec"].ToString());
            if (lstCredito != null)
            {
                var movProd = from mp in lstCredito
                              where Convert.ToDateTime(mp.FechaPago) >= pFecIni
                              && Convert.ToDateTime(mp.FechaPago) <= pFecFin
                              orderby mp.FechaPago, mp.FechaCuota, mp.CodOperacion
                              select new
                              {
                                  num_producto = mp.NumeroRadicacion,
                                  mp.num_comp,
                                  tipo_comp = mp.TIPO_COMP,
                                  FechaPago = mp.FechaPago.ToShortDateString(),
                                  FechaCuota = mp.FechaCuota.ToShortDateString(),
                                  TipoOperacion = mp.CodOperacion,
                                  Transaccion = mp.TipoOperacion,
                                  tipomovimiento = mp.TipoMovimiento,
                                  dias_mora = mp.DiasMora,
                                  num_cuota = mp.NumCuota,
                                  saldo = mp.Saldo,
                                  capital = mp.Capital,
                                  IntCte = mp.IntCte,
                                  IntMora = mp.IntMora,
                                  LeyMiPyme = mp.LeyMiPyme,
                                  ivaMiPyme = mp.ivaMiPyme,
                                  Poliza = mp.Poliza,
                                  otros = mp.Otros,
                                  prejuridico = mp.Prejuridico,
                                  totalpago = ((mp.TipoOperacion == "Desembolsos") ? 0 : mp.Capital + mp.IntCte + mp.IntMora + mp.LeyMiPyme + mp.ivaMiPyme + mp.Poliza + mp.Otros + mp.Seguros + mp.Prejuridico),
                                  calificacion = mp.Calificacion,
                                  seguros = mp.Seguros
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
                VerificarGridView(2);
            }

        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }


    protected void btnConsultarPlanPagos_Click(object sender, EventArgs e)
    {
        string id = ViewState["pProducto"].ToString();
        Navegar("~/Pages/Credito/PlanPagos/Detalle.aspx?num_radic=" + id);
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

