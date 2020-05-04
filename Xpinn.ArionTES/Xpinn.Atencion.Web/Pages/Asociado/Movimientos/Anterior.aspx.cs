using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.UI;
using xpinnWSEstadoCuenta;

public partial class Movimientos : GlobalWeb
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
            if (Session[pPersona.cod_persona + "NroProducto"] != null && Session[pPersona.cod_persona + "CodProducto"] != null)
            {
                ViewState["pProducto"] = Session[pPersona.cod_persona + "NroProducto"].ToString();
                Session.Remove(pPersona.cod_persona + "NroProducto");
                ViewState["pTipoProduct"] = Session[pPersona.cod_persona + "CodProducto"].ToString();
                Session.Remove(pPersona.cod_persona + "CodProducto");
                ObtenerDatos(ViewState["pProducto"].ToString(), ViewState["pTipoProduct"].ToString());
            }
            else
            {
                if (ViewState["pProducto"] != null && ViewState["pTipoProduct"] != null)
                    ObtenerDatos(ViewState["pProducto"].ToString(), ViewState["pTipoProduct"].ToString());
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
        
    private void ObtenerDatos(string pProducto, string pTipoProduct)
    {
        try
        {
            pPersona = (xpinnWSLogin.Persona1)Session["persona"];
            if (string.IsNullOrEmpty(pProducto.Trim()) || string.IsNullOrEmpty(pTipoProduct.Trim()))
            {
                VerError("Se generó un error al realizar la consulta.");
                return;
            }
            //Realizando Consulta de Datos
            DateTime pFec = DateTime.Now;
            txtFecIni.Text = DateTime.ParseExact(pFec.ToString(gFormatoFecha), gFormatoFecha, null).AddMonths(-6).ToString();
            txtFecFin.Text = DateTime.ParseExact(pFec.ToString(gFormatoFecha), gFormatoFecha, null).ToString();

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
            DateTime pFecIniProduct = DateTime.MinValue;

            switch (pTipoProduct)
            {
                case "1"://Aportes
                    try
                    {
                        pEntidad.tipocontrato = "Aportes";
                        ActualizarAportes(ViewState["pProducto"].ToString());
                        //Actualizar(pProducto, pTipoProduct);
                    }
                    catch (Exception e)
                    {
                        VerError("Se genero un error al Consultar el producto");
                        return;
                    }
                    break;

                case "2"://Creditos
                    try
                    {
                        xpinnWSCredito.CreditoPlan pEntidadCred = BOCreditos.ConsultarInformacionCreditos(Convert.ToInt64(pProducto), false, "");
                        pEntidad.tipocontrato = "Crédito";
                        pEntidad.fechacreacion = pEntidadCred.FechaDesembolso;
                        pEntidad.medio = pEntidadCred.Linea;

                        if (pEntidadCred.FechaDesembolso != DateTime.MinValue)
                            pFecIniProduct = pEntidadCred.FechaDesembolso;
                        if (pEntidadCred.tipo_linea == 2)
                        {
                            avances = pEntidadCred.Numero_radicacion;
                            if (avances > 0)
                            {
                                cargarAvances(avances);
                            }
                            else
                            {
                                ActualizarCredito(pProducto);
                            }
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
                    break;

                case "3"://Ahorros
                    try
                    {
                        string id_aho = pProducto;
                    }
                    catch (Exception ex)
                    {
                        VerError("Se genero un error al Consultar el producto" + ex.Message);
                        return;
                    }
                    break;

                case "4"://Servicios
                    try
                    {
                        pEntidad.tipocontrato = "Servicios";
                        bool ManejaClubAhorrador = ConfigurationManager.AppSettings["IncluirClubAhorradores"] == null ? true :
                            (ConfigurationManager.AppSettings["IncluirClubAhorradores"].ToString() == "1") ? false : true;

                        List<xpinnWSEstadoCuenta.Servicio> lstServicio = new List<xpinnWSEstadoCuenta.Servicio>();
                        string pFiltroServicio = " and SERVICIOS.ESTADO not in ('T') and Numero_Servicio = "+pProducto;
                        lstServicio = service.ListarServiciosClubAhorrador(pPersona.cod_persona, pFiltroServicio, ManejaClubAhorrador, pPersona.identificacion, pPersona.clavesinecriptar, "");

                        if (lstServicio != null)
                        {
                            List<xpinnWSEstadoCuenta.Servicio> lstSrv = new List<xpinnWSEstadoCuenta.Servicio>();
                            Servicio apo = lstServicio.ElementAt(0);
                            lstSrv.Add(apo);
                            //frmAportes.DataSource = lstSrv;
                            //frmAportes.DataBind();
                            frvData.Visible = false;
                            //frmAportes.Visible = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        VerError("Se genero un error al Consultar el producto" + ex.Message);
                        return;
                    }
                    break;
                case "9"://programado
                    try
                    {
                        string id_progra = pProducto;
                    }
                    catch (Exception ex)
                    {
                        VerError("Se genero un error al Consultar el producto" + ex.Message);
                        return;
                    }
                    break;
                default:
                    break;
            }


            lstInfo.Add(pEntidad);
            frvData.DataSource = lstInfo;
            frvData.DataBind();
        }
        catch (Exception ex) { VerError(ex.Message); }
    }

    private void ActualizarAportes(string numero)
    {        
        //carga encabezado
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


        //llena tabla de detalle movimientos 
        DateTime pFec = DateTime.Now;
        pPersona = (xpinnWSLogin.Persona1)Session["persona"];
        List<Object> lstMov = new List<Object>();

        DateTime pFecIni = Convert.ToDateTime(txtFecIni.Text);
        DateTime pFecFin = Convert.ToDateTime(txtFecFin.Text);

        List<xpinnWSCredito.MovimientoAporte> lstAportes = BOCreditos.ListarMovAporte(Convert.ToInt64(numero), pFecIni, pFecFin, pPersona.identificacion, pPersona.clavesinecriptar, "");
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
                VerificarGridView(Convert.ToInt32(1));
            }
        }
    }

    private void cargarAvances(long radicado)
    {
        List<xpinnWSCredito.ConsultaAvance> lstAvance = BOCreditos.ListarAvancesCredito(radicado, "", "");
        if(lstAvance != null && lstAvance.Count > 0)
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

    private void ActualizarCredito(string pProducto)
    {
        try
        {
            DateTime pFec = DateTime.Now;
            pPersona = (xpinnWSLogin.Persona1)Session["persona"];
            List<Object> lstMov = new List<Object>();

            DateTime pFecIni = Convert.ToDateTime(txtFecIni.Text);
            DateTime pFecFin = Convert.ToDateTime(txtFecFin.Text);

            List<xpinnWSCredito.MovimientoProducto> lstCredito = BOCreditos.ListarMovCreditos(Convert.ToInt64(pProducto), 1, pPersona.identificacion, pPersona.clavesinecriptar, "");
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
                VerificarGridView(Convert.ToInt32(2));
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
                string tipo = ViewState["pTipoProduct"].ToString();
                switch (tipo)
                {
                    case "1":
                        ActualizarAportes(ViewState["pProducto"].ToString());
                        break;
                    case "2":
                        long avances = 0;
                        xpinnWSCredito.CreditoPlan pEntidadCred = BOCreditos.ConsultarInformacionCreditos(Convert.ToInt64(ViewState["pProducto"].ToString()), false, "");
                        if (pEntidadCred.tipo_linea == 2)
                        {
                            avances = pEntidadCred.Numero_radicacion;
                            if (avances > 0)
                            {
                                cargarAvances(avances);
                            }
                            else
                            {
                                ActualizarCredito(ViewState["pProducto"].ToString());
                            }
                        }
                        else
                        {
                            btnPlanPagos.Visible = true;
                        }
                        break;
                default:
                        break;
                }                
            }
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }
}

