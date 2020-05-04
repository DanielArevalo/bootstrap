using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using System.Data;
using Microsoft.Reporting.WebForms;
using System.IO;
using System.Configuration;
using System.Globalization;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.xml;
using iTextSharp.text.html.simpleparser;
using System.Security.Permissions;
using System.Security;

public partial class Detalle : GlobalWeb
{
    xpinnWSEstadoCuenta.WSEstadoCuentaSoapClient service = new xpinnWSEstadoCuenta.WSEstadoCuentaSoapClient();
    xpinnWSCredito.WSCreditoSoapClient BOCredito = new xpinnWSCredito.WSCreditoSoapClient();
    xpinnWSDeposito.WSDepositoSoapClient BODeposito = new xpinnWSDeposito.WSDepositoSoapClient();
    xpinnWSAppFinancial.WSAppFinancialSoapClient BOFinancial = new xpinnWSAppFinancial.WSAppFinancialSoapClient();
    xpinnWSIntegracion.WSintegracionSoapClient wsIntegra = new xpinnWSIntegracion.WSintegracionSoapClient();

    xpinnWSLogin.Persona1 pPersona;
    Int16 cont = 0;
    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            ValidarSession();
            VisualizarTitulo(OptionsUrl.EstadoCuenta, "Inf");
            Site toolBar = (Site)Master;
            toolBar.eventoRegresar += btnRegresar_Click;
            toolBar.MostrarRegresar(false);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw("SolicitudServicio", "Page_PreInit", ex);
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ViewState["APORTES"] = null;
            ViewState["DTCREDITOS"] = null;
            ViewState["DTACODEUDADOS"] = null;
            panelGeneral.Visible = true;
            panelImpresion.Visible = false;
            ObtenerDatos();
            CifradoBusiness cifrar = new CifradoBusiness();
            string clave = cifrar.Desencriptar("Ync7VhsawBFQHzCqffhdfg==");
        }
    }


    private void ObtenerDatos()
    {
        try
        {
            //INICIALIZAR TABS
            xpinnWSEstadoCuenta.General parametroahorros;
            xpinnWSEstadoCuenta.General parametroservicios;

            pPersona = (xpinnWSLogin.Persona1)Session["persona"];

            xpinnWSIntegracion.Integracion pse = wsIntegra.consultarConvenioIntegracion(Convenios_Integracion.pse, Session["sec"].ToString());

            if (pse != null && !string.IsNullOrEmpty(pse.entidad))
            {
                //Actualiza PSE
                wsIntegra.UpdatePaymentsACH(pPersona.cod_persona, Session["sec"].ToString());
            }
            parametroahorros = service.ConsultarGeneral(3, pPersona.identificacion, pPersona.clavesinecriptar, Session["sec"].ToString());
            parametroservicios = service.ConsultarGeneral(4, pPersona.identificacion, pPersona.clavesinecriptar, Session["sec"].ToString());



            // Mostrar Servicios
            string tbServicio = null;
            if (parametroservicios != null)
            {
                string[] pParamServi = null;
                if (parametroservicios.valor.Contains('|'))
                    pParamServi = parametroservicios.valor.Split('|');
                else
                    pParamServi = new string[] { parametroservicios.valor, parametroservicios.valor };
                tbServicio = pParamServi[1];
            }



            xpinnWSCredito.Persona Persona = new xpinnWSCredito.Persona();
            Persona = BOCredito.ConsultarPersona(pPersona.cod_persona, pPersona.clavesinecriptar, Session["sec"].ToString());
            if (Persona.PrimerNombre != "" && Persona.PrimerApellido != "")
                Persona.PrimerNombre = Persona.PrimerNombre.Trim() + " " + Persona.PrimerApellido.Trim();
            Persona.SegundoNombre = Persona.Ciudad.nomciudad;
            List<xpinnWSCredito.Persona> lstData = new List<xpinnWSCredito.Persona>();
            lstData.Add(Persona);
            frvData.DataSource = lstData;
            frvData.DataBind();

            bool ManejaClubAhorrador = ConfigurationManager.AppSettings["IncluirClubAhorradores"] == null ? true :
                (ConfigurationManager.AppSettings["IncluirClubAhorradores"].ToString() == "1") ? false : true;

            string pFiltro = string.Empty;
            pFiltro = " And v_aportes.estado in (1)";
            List<xpinnWSEstadoCuenta.Aporte> lstConsulta = new List<xpinnWSEstadoCuenta.Aporte>();
            lstConsulta = service.ListarAportesEstadoCuenta(Convert.ToInt64(pPersona.cod_persona), ManejaClubAhorrador, pFiltro, DateTime.Now);

            if (lstConsulta != null && lstConsulta.Count > 0)
            {
                Session["ListaAportes"] = lstConsulta;
                ViewState.Add("APORTES", lstConsulta);
                panelAporte.Visible = true;
                gvAportes.DataSource = lstConsulta;
                gvAportes.DataBind();
                lblMsj1.Text = "Registros encontrados " + lstConsulta.Count();
            }
            else
            {
                panelAporte.Visible = false;
                lblMsj1.Text = "No se encontraron Datos";
            }


            //LISTANDO CREDITOS

            List<xpinnWSEstadoCuenta.ProductoResumen> lstCredito = new List<xpinnWSEstadoCuenta.ProductoResumen>();
            lstCredito = service.EstadoCuenta(ManejaClubAhorrador, ((Label)frvData.FindControl("txtIdentificacion")).Text, pPersona.clavesinecriptar);
            if (lstCredito != null && lstCredito.Count > 0)
            {
                //for (int i = 0; i < lstCredito.Count; i++)
                //{
                //    lstCredito[i].Tasainteres = Convert.ToDecimal(lstCredito[i].Tasainteres) / 100;
                //}
                ViewState.Add("DTCREDITOS", lstCredito);
                panelCredito.Visible = true;
                gvCreditos.DataSource = lstCredito;
                gvCreditos.DataBind();
                lblMsj2.Text = "Registros encontrados " + lstCredito.Count();
            }
            else
            {
                panelCredito.Visible = false;
                lblMsj2.Text = "No se encontraron Datos";
            }

            //LISTANDO CREDITOS ACODEUDADOS
            List<xpinnWSEstadoCuenta.Acodeudados> lstAcodeudados = new List<xpinnWSEstadoCuenta.Acodeudados>();
            xpinnWSEstadoCuenta.Cliente pCliente = new xpinnWSEstadoCuenta.Cliente();
            pCliente.IdCliente = pPersona.cod_persona;
            lstAcodeudados = service.ListarAcodeudadoss(pCliente);
            if (lstAcodeudados != null && lstAcodeudados.Count > 0)
            {
                ViewState["DTACODEUDADOS"] = lstAcodeudados;
                panelAcodeudado.Visible = true;
                gvAcodeudados.DataSource = lstAcodeudados;
                gvAcodeudados.DataBind();
                lblMsj3.Text = "Registros encontrados " + lstAcodeudados.Count();
            }
            else
            {
                panelAcodeudado.Visible = false;
                lblMsj3.Text = "No se encontraron Datos";
            }

            //LISTANDO AHORROS A LA VISTA
            if (parametroahorros != null && !string.IsNullOrEmpty(parametroahorros.valor) && parametroahorros.valor == "3")
            {
                List<xpinnWSDeposito.AhorroVista> lstAhorros = new List<xpinnWSDeposito.AhorroVista>();
                string pFiltroAho = " AND A.ESTADO NOT IN (4,3,2) AND A.COD_PERSONA = " + pPersona.cod_persona;
                lstAhorros = BODeposito.ListarAhorroVistaClubAhorrador(pPersona.cod_persona, pFiltroAho, ManejaClubAhorrador, pPersona.identificacion, pPersona.clavesinecriptar, Session["sec"].ToString());
                gvAhorros.DataSource = lstAhorros;
                if (lstAhorros.Count > 0)
                {
                    ViewState["DTAHORRO"] = lstAhorros;
                    panelAhorros.Visible = true;
                    //lblTotRegAhorro.Visible = true;
                    lblTotRegAhorro.Text = "<br/> Registros encontrados " + lstAhorros.Count.ToString();
                    lblInfoAhorro.Visible = false;
                }
                else
                {
                    ViewState["DTAHORRO"] = null;
                    panelAhorros.Visible = false;
                    lblTotRegAhorro.Visible = false;
                    lblInfoAhorro.Visible = true;
                }
                gvAhorros.DataBind();
            }

            //LISTANDO CDATS
            List<xpinnWSDeposito.Cdat> lstCdat = new List<xpinnWSDeposito.Cdat>();
            string pFiltroCdat = " AND C.ESTADO IN (1,2) AND T.COD_PERSONA = " + pPersona.cod_persona;
            lstCdat = BODeposito.ListarCdats(pFiltroCdat, DateTime.MinValue, pPersona.identificacion, pPersona.clavesinecriptar, Session["sec"].ToString());
            gvCDATS.DataSource = lstCdat;
            if (lstCdat.Count > 0)
            {
                ViewState["DTCDAT"] = lstCdat;
                panelCdat.Visible = true;
                //lblTotRegCdat.Visible = true;
                lblTotRegCdat.Text = "<br/> Registros encontrados " + lstCdat.Count.ToString();
                lblInfoCdat.Visible = false;
            }
            else
            {
                ViewState["DTCDAT"] = null;
                panelCdat.Visible = false;
                lblTotRegCdat.Visible = false;
                //lblInfoCdat.Visible = true;
            }
            gvCDATS.DataBind();

            //LISTANDO AHORRO PROGRAMADO
            List<xpinnWSDeposito.CuentasProgramado> lstProgramado = new List<xpinnWSDeposito.CuentasProgramado>();
            string pFiltroProg = " WHERE A.ESTADO NOT IN (2,3) AND A.COD_PERSONA = " + pPersona.cod_persona;
            var a = DateTime.MinValue;
            DateTime fechaFiltro = DateTime.Now;
            lstProgramado = BODeposito.ListarAhorrosProgramado(pFiltroProg, DateTime.MinValue, pPersona.identificacion, pPersona.clavesinecriptar, Session["sec"].ToString());
            gvAhoProgra.DataSource = lstProgramado;
            if (lstProgramado.Count > 0)
            {
                ViewState["DTAHOPRO"] = lstProgramado;
                panelProgra.Visible = true;
                //lblTotRegProg.Visible = true;
                lblTotRegProg.Text = "<br/> Registros encontrados " + lstProgramado.Count.ToString();
                lblInfoProg.Visible = false;
            }
            else
            {
                ViewState["DTAHOPRO"] = null;
                panelProgra.Visible = false;
                lblTotRegProg.Visible = false;
                //lblInfoProg.Visible = true;
            }
            gvAhoProgra.DataBind();

            //LISTANDO SERVICIOS
            if (parametroservicios != null && !string.IsNullOrEmpty(tbServicio) && tbServicio == "4")
            {
                List<xpinnWSEstadoCuenta.Servicio> lstServicio = new List<xpinnWSEstadoCuenta.Servicio>();
                string pFiltroServicio = " and SERVICIOS.ESTADO not in ('T') ";
                lstServicio = service.ListarServiciosClubAhorrador(pPersona.cod_persona, pFiltroServicio, ManejaClubAhorrador, pPersona.identificacion, pPersona.clavesinecriptar, Session["sec"].ToString());
                gvServicio.DataSource = lstServicio;
                if (lstServicio.Count > 0)
                {
                    ViewState["DTSERVICIO"] = lstServicio;
                    panelServicio.Visible = true;
                    //lblTotRegServ.Visible = true;
                    lblTotRegServ.Text = "<br/> Registros encontrados " + lstServicio.Count.ToString();
                    lblInfoServ.Visible = false;
                }
                else
                {
                    ViewState["DTSERVICIO"] = null;
                    panelServicio.Visible = false;
                    lblTotRegServ.Visible = false;
                    lblInfoServ.Visible = true;
                }
                gvServicio.DataBind();
            }

            //LISTANDO DEVOLUCIONES
            ActualizarDevoluciones(pPersona);

            //LISTANDO COMENTARIOS
            List<xpinnWSEstadoCuenta.Comentario> lstComentarios = service.ListarComentarios(pPersona.cod_persona, Session["sec"].ToString());
            if (lstComentarios != null && lstComentarios.Count > 0)
            {
                //ViewState["DTACOMENTARIOS"] = lstComentarios;
                pnlComentarios.Visible = true;
                gvComentarios.DataSource = lstComentarios;
                gvComentarios.DataBind();
                lblMensajeComentarios.Text = "Registros encontrados " + lstComentarios.Count();
            }
            else
            {
                pnlComentarios.Visible = false;
                lblMensajeComentarios.Text = "No se encontraron Datos";
            }

            TotalizarGridView();

            int ManejaFormaPagos = 0;
            if (ConfigurationManager.AppSettings["ManejaFormaPagos"] != null)
                ManejaFormaPagos = Convert.ToInt32(ConfigurationManager.AppSettings["ManejaFormaPagos"].ToString());

            ValidarProcesoPago(ManejaFormaPagos);
        }
        catch (Exception ex)
        {
            VerError(ex.ToString());
        }
    }

    protected void ValidarProcesoPago(int pManejaPago)
    {
        bool rpta = pManejaPago == 0 ? false : true;
        gvAportes.Columns[0].Visible = rpta;
        gvCreditos.Columns[0].Visible = rpta;
        gvServicio.Columns[0].Visible = rpta;
    }


    protected void frvData_DataBound(object sender, EventArgs e)
    {
        Label lblFechaAfiliacion = (Label)frvData.FindControl("lblFechaAfiliacion");
        if (lblFechaAfiliacion != null)
        {
            if (!string.IsNullOrWhiteSpace(lblFechaAfiliacion.Text))
            {
                if (Convert.ToDateTime(lblFechaAfiliacion.Text) == DateTime.MinValue)
                    lblFechaAfiliacion.Text = "";
            }
        }
    }


    protected void TotalizarGridView()
    {
        decimal totalsaldo = 0;
        decimal totalcuotas = 0;
        decimal pendientepago = 0;
        //TOTALIZANDO APORTES
        if (gvAportes.Rows.Count > 0)
        {
            decimal totalcausacion_aportes = 0;
            decimal totalcausacionmasaportes = 0;


            foreach (GridViewRow rfila in gvAportes.Rows)
            {
                if (gvAportes.DataKeys[rfila.RowIndex].Values[0] != null)
                    if (gvAportes.DataKeys[rfila.RowIndex].Values[0].ToString() != "" && gvAportes.DataKeys[rfila.RowIndex].Values[0].ToString() != "&nbsp;")
                        totalsaldo += Convert.ToDecimal(gvAportes.DataKeys[rfila.RowIndex].Values[0].ToString());

                if (gvAportes.DataKeys[rfila.RowIndex].Values[1] != null)
                    if (gvAportes.DataKeys[rfila.RowIndex].Values[1].ToString() != "" && gvAportes.DataKeys[rfila.RowIndex].Values[1].ToString() != "&nbsp;")
                        totalcuotas += Convert.ToDecimal(gvAportes.DataKeys[rfila.RowIndex].Values[1].ToString());

                if (gvAportes.DataKeys[rfila.RowIndex].Values[2] != null)
                    if (gvAportes.DataKeys[rfila.RowIndex].Values[2].ToString() != "" && gvAportes.DataKeys[rfila.RowIndex].Values[2].ToString() != "&nbsp;")
                        pendientepago += Convert.ToDecimal(gvAportes.DataKeys[rfila.RowIndex].Values[2].ToString());

                if (gvAportes.DataKeys[rfila.RowIndex].Values[4] != null)
                    if (gvAportes.DataKeys[rfila.RowIndex].Values[4].ToString() != "" && gvAportes.DataKeys[rfila.RowIndex].Values[4].ToString() != "&nbsp;")
                        totalcausacion_aportes += Convert.ToDecimal(gvAportes.DataKeys[rfila.RowIndex].Values[4].ToString());
            }
            GridViewRow row = gvAportes.FooterRow;
            row.Cells[5].Text = "Total";
            row.Cells[8].Text = Convert.ToString(totalcuotas.ToString("C0"));
            row.Cells[9].Text = Convert.ToString(totalsaldo.ToString("C0"));
            row.Cells[11].Text = Convert.ToString(pendientepago.ToString("C0"));
            txtTotalAportes.Text = Convert.ToString(totalsaldo.ToString("C0"));
            txtTotalCuotasAportes.Text = Convert.ToString(totalcuotas.ToString("C0"));
            txtAPortesPendientesporPagar.Text = Convert.ToString(pendientepago.ToString("C0"));
            txtTotalCausadosApo.Text = Convert.ToString(totalcausacion_aportes.ToString("C0"));
            totalcausacionmasaportes = totalsaldo + totalcausacion_aportes;
            txtTotalmasRendCausados.Text = Convert.ToString(totalcausacionmasaportes.ToString("C0"));
        }
        else { panelAporte.Visible = false; btnConsolidado.Visible = false; }


        //TOTALIZANDO LOS CREDITOS
        if (gvCreditos.Rows.Count > 0)
        {
            totalsaldo = 0;
            totalcuotas = 0;
            pendientepago = 0;

            foreach (GridViewRow rfila in gvCreditos.Rows)
            {
                if (gvCreditos.DataKeys[rfila.RowIndex].Values[0] != null)
                    if (gvCreditos.DataKeys[rfila.RowIndex].Values[0].ToString() != "" && gvCreditos.DataKeys[rfila.RowIndex].Values[0].ToString() != "&nbsp;")
                        totalsaldo += Convert.ToDecimal(gvCreditos.DataKeys[rfila.RowIndex].Values[0].ToString());

                if (gvCreditos.DataKeys[rfila.RowIndex].Values[1] != null)
                    if (gvCreditos.DataKeys[rfila.RowIndex].Values[1].ToString() != "" && gvCreditos.DataKeys[rfila.RowIndex].Values[1].ToString() != "&nbsp;")
                        totalcuotas += Convert.ToDecimal(gvCreditos.DataKeys[rfila.RowIndex].Values[1].ToString());

                if (gvCreditos.DataKeys[rfila.RowIndex].Values[2] != null)
                    if (gvCreditos.DataKeys[rfila.RowIndex].Values[2].ToString() != "" && gvCreditos.DataKeys[rfila.RowIndex].Values[2].ToString() != "&nbsp;")
                        pendientepago += Convert.ToDecimal(gvCreditos.DataKeys[rfila.RowIndex].Values[2].ToString());
            }


            GridViewRow rowAp = gvCreditos.FooterRow;
            rowAp.Cells[5].Text = "Total";
            rowAp.Cells[10].Text = Convert.ToString(totalcuotas.ToString("C0"));
            rowAp.Cells[11].Text = Convert.ToString(totalsaldo.ToString("C0"));
            rowAp.Cells[13].Text = Convert.ToString(pendientepago.ToString("C0"));
            txtTotalSaldos.Text = Convert.ToString(totalsaldo.ToString("C0"));
            txtTotalCoutasCreditos.Text = Convert.ToString(totalcuotas.ToString("C0"));
            txtVlrPendienteApagar.Text = Convert.ToString(pendientepago.ToString("C0"));
        }
        else { panelCredito.Visible = false; }


        //TOTALIZANDO LOS AHORROS 
        //   ahorros 
        if (gvAhorros.Rows.Count > 0)
        {
            decimal ahorros = 0;
            decimal totalcuotasahorros = 0;
            decimal totalcausacion_ahorros = 0;
            decimal totalcanje_ahorros = 0;
            decimal totalcausacionmasahorros = 0;
            foreach (GridViewRow rfila in gvAhorros.Rows)
            {

                if (gvAhorros.DataKeys[rfila.RowIndex].Values[0] != null)
                    if (gvAhorros.DataKeys[rfila.RowIndex].Values[0].ToString() != "" && gvAhorros.DataKeys[rfila.RowIndex].Values[0].ToString() != "&nbsp;")
                        ahorros += Convert.ToDecimal(gvAhorros.DataKeys[rfila.RowIndex].Values[0].ToString());

                if (gvAhorros.DataKeys[rfila.RowIndex].Values[1] != null)
                    if (gvAhorros.DataKeys[rfila.RowIndex].Values[1].ToString() != "" && gvAhorros.DataKeys[rfila.RowIndex].Values[1].ToString() != "&nbsp;")
                        totalcuotasahorros += Convert.ToDecimal(gvAhorros.DataKeys[rfila.RowIndex].Values[1].ToString());

                if (gvAhorros.DataKeys[rfila.RowIndex].Values[2] != null)
                    if (gvAhorros.DataKeys[rfila.RowIndex].Values[2].ToString() != "" && gvAhorros.DataKeys[rfila.RowIndex].Values[2].ToString() != "&nbsp;")
                        totalcanje_ahorros += Convert.ToDecimal(gvAhorros.DataKeys[rfila.RowIndex].Values[2].ToString());

                if (gvAhorros.DataKeys[rfila.RowIndex].Values[3] != null)
                    if (gvAhorros.DataKeys[rfila.RowIndex].Values[3].ToString() != "" && gvAhorros.DataKeys[rfila.RowIndex].Values[3].ToString() != "&nbsp;")
                        totalcausacion_ahorros += Convert.ToDecimal(gvAhorros.DataKeys[rfila.RowIndex].Values[3].ToString());

                txtTotalAhorros.Text = Convert.ToString(ahorros.ToString("C0"));
                txtTotalCuotasAhorros.Text = Convert.ToString(totalcuotasahorros.ToString("C0"));
                txtTotalCausacionAhorros.Text = Convert.ToString(totalcausacion_ahorros.ToString("C0"));
                txtTotalCanjeAhorros.Text = Convert.ToString(totalcanje_ahorros.ToString("C0"));
                totalcausacionmasahorros = ahorros + totalcausacion_ahorros;
                txtTotalAhmasRendCausados.Text = Convert.ToString(totalcausacionmasahorros.ToString("C0"));
                GridViewRow rowProgra = gvAhorros.FooterRow;
                rowProgra.Cells[1].Text = "Total";
                rowProgra.Cells[5].Text = Convert.ToString(ahorros.ToString("C0"));
                rowProgra.Cells[9].Text = Convert.ToString(totalcuotasahorros.ToString("C0"));
                rowProgra.Cells[12].Text = Convert.ToString(totalcausacion_ahorros.ToString("C0"));
                rowProgra.Cells[13].Text = Convert.ToString(totalcausacionmasahorros.ToString("C0"));
            }
        }
        else { panelAhorros.Visible = false; }

        // TOTALIZANDO LOS CDATS
        if (gvCDATS.Rows.Count > 0)
        {
            decimal cdat = 0;
            decimal totalcausacioncdat = 0;
            decimal totalcausacionmascdat = 0;
            foreach (GridViewRow rfila in gvCDATS.Rows)
            {

                if (gvCDATS.DataKeys[rfila.RowIndex].Values[1] != null)
                    if (gvCDATS.DataKeys[rfila.RowIndex].Values[1].ToString() != "" && gvCDATS.DataKeys[rfila.RowIndex].Values[1].ToString() != "&nbsp;")
                        cdat += Convert.ToDecimal(gvCDATS.DataKeys[rfila.RowIndex].Values[1].ToString());

                if (gvCDATS.DataKeys[rfila.RowIndex].Values[2] != null)
                    if (gvCDATS.DataKeys[rfila.RowIndex].Values[2].ToString() != "" && gvCDATS.DataKeys[rfila.RowIndex].Values[2].ToString() != "&nbsp;")
                        totalcausacioncdat += Convert.ToDecimal(gvCDATS.DataKeys[rfila.RowIndex].Values[2].ToString());

                txtTotalCdats.Text = Convert.ToString(cdat.ToString("C0"));
                txtTotalCausacionCdat.Text = Convert.ToString(totalcausacioncdat.ToString("C0"));

                GridViewRow rowCDAT = gvCDATS.FooterRow;
                rowCDAT.Cells[3].Text = "Total";
                rowCDAT.Cells[9].Text = Convert.ToString((cdat + totalcausacioncdat).ToString("C0"));
                totalcausacionmascdat = cdat + totalcausacioncdat;
                rowCDAT.Cells[13].Text = Convert.ToString(totalcausacioncdat.ToString("C0"));
                txtTotalCdatRendCausados.Text = Convert.ToString(totalcausacionmascdat.ToString("C0"));
            }
        }
        else { panelCdat.Visible = false; }

        // TOTALIZANDO LOS AHORROS PROGRAMADO
        if (gvAhoProgra.Rows.Count > 0)
        {
            decimal programado = 0;
            decimal totalcausacion_programado = 0;
            decimal totalcuotasprogramado = 0;
            decimal totalcausacionmasprogramado = 0;
            foreach (GridViewRow rfila in gvAhoProgra.Rows)
            {

                if (gvAhoProgra.DataKeys[rfila.RowIndex].Values[1] != null)
                    if (gvAhoProgra.DataKeys[rfila.RowIndex].Values[1].ToString() != "" && gvAhoProgra.DataKeys[rfila.RowIndex].Values[1].ToString() != "&nbsp;")
                        programado += Convert.ToDecimal(gvAhoProgra.DataKeys[rfila.RowIndex].Values[1].ToString());

                if (gvAhoProgra.DataKeys[rfila.RowIndex].Values[2] != null)
                    if (gvAhoProgra.DataKeys[rfila.RowIndex].Values[2].ToString() != "" && gvAhoProgra.DataKeys[rfila.RowIndex].Values[2].ToString() != "&nbsp;")
                        totalcuotasprogramado += Convert.ToDecimal(gvAhoProgra.DataKeys[rfila.RowIndex].Values[2].ToString());

                if (gvAhoProgra.DataKeys[rfila.RowIndex].Values[3] != null)
                    if (gvAhoProgra.DataKeys[rfila.RowIndex].Values[3].ToString() != "" && gvAhoProgra.DataKeys[rfila.RowIndex].Values[3].ToString() != "&nbsp;")
                        totalcausacion_programado += Convert.ToDecimal(gvAhoProgra.DataKeys[rfila.RowIndex].Values[3].ToString());

                txtTotalAhoProgramado.Text = Convert.ToString(programado.ToString("C0"));
                txtTotalCuotasAhoProgra.Text = Convert.ToString(totalcuotasprogramado.ToString("C0"));
                txtTotalCausacionProgramado.Text = Convert.ToString(totalcausacion_programado.ToString("C0"));

                totalcausacionmasprogramado = programado + totalcausacion_programado;
                txtTotalProgmasRendCausados.Text = Convert.ToString(totalcausacionmasprogramado.ToString("C0"));

                GridViewRow rowProgra = gvAhoProgra.FooterRow;
                rowProgra.Cells[1].Text = "Total";
                rowProgra.Cells[6].Text = Convert.ToString(programado.ToString("C0"));
                rowProgra.Cells[11].Text = Convert.ToString(totalcuotasprogramado.ToString("C0"));
                rowProgra.Cells[15].Text = Convert.ToString(totalcausacion_programado.ToString("C0"));
            }
        }
        else { panelProgra.Visible = false; }

        // TOTALIZANDO LOS SERVICIOS
        if (gvServicio.Rows.Count > 0)
        {
            decimal servicios = 0;
            decimal totalcuotasservicios = 0;
            decimal totalvalorinicial = 0;
            foreach (GridViewRow rfila in gvServicio.Rows)
            {
                if (gvServicio.DataKeys[rfila.RowIndex].Values[1] != null)
                    if (gvServicio.DataKeys[rfila.RowIndex].Values[1].ToString() != "" && gvServicio.DataKeys[rfila.RowIndex].Values[1].ToString() != "&nbsp;")
                        servicios += Convert.ToDecimal(gvServicio.DataKeys[rfila.RowIndex].Values[1].ToString());

                if (gvServicio.DataKeys[rfila.RowIndex].Values[2] != null)
                    if (gvServicio.DataKeys[rfila.RowIndex].Values[2].ToString() != "" && gvServicio.DataKeys[rfila.RowIndex].Values[2].ToString() != "&nbsp;")
                        totalcuotasservicios += Convert.ToDecimal(gvServicio.DataKeys[rfila.RowIndex].Values[2].ToString());


                if (gvServicio.DataKeys[rfila.RowIndex].Values[3] != null)
                    if (gvServicio.DataKeys[rfila.RowIndex].Values[3].ToString() != "" && gvServicio.DataKeys[rfila.RowIndex].Values[3].ToString() != "&nbsp;")
                        totalvalorinicial += Convert.ToDecimal(gvServicio.DataKeys[rfila.RowIndex].Values[3].ToString());



                txtTotalServicios.Text = Convert.ToString(servicios.ToString("C0"));
                txtTotalCuotasServicios.Text = Convert.ToString(totalcuotasservicios.ToString("C0"));
                txtTotalValorInicialServicios.Text = Convert.ToString(totalvalorinicial.ToString("C0"));

                GridViewRow rowserv = gvServicio.FooterRow;
                rowserv.Cells[3].Text = "Total";
                rowserv.Cells[11].Text = Convert.ToString(totalcuotasservicios.ToString("C0"));
                rowserv.Cells[12].Text = Convert.ToString(servicios.ToString("C0"));
            }
        }
        else { panelServicio.Visible = false; }
    }

    #region EVENTOS GRIDVIEW APORTES

    protected void gvAportes_RowEditing(object sender, GridViewEditEventArgs e)
    {
        pPersona = (xpinnWSLogin.Persona1)Session["persona"];
        String id = gvAportes.Rows[e.NewEditIndex].Cells[2].Text;
        decimal totalCuotas = 0;
        /*Se valida que cuenta del aporte es dentro de un grupo la principal
           esto para sumar los valores del grupo de aportes y cancelar dicho valor*/

        if (gvAportes.DataKeys[e.NewEditIndex].Values[5] != null)
        { 
            String Principal = gvAportes.DataKeys[e.NewEditIndex].Values[5].ToString();     
            if (Principal == "1")
            foreach (GridViewRow rfila in gvAportes.Rows)
            {
                totalCuotas += Convert.ToDecimal(gvAportes.DataKeys[rfila.RowIndex].Values[2].ToString());
            }
        }

        Session[pPersona.cod_persona + "Identificacion"] = ((Label)frvData.FindControl("txtIdentificacion")).Text;
        Session[pPersona.cod_persona + "Nombre"] = ((Label)frvData.FindControl("txtNombre")).Text;
        Session[pPersona.cod_persona + "NroProducto"] = id;
        Session[pPersona.cod_persona + "VrPago"] = totalCuotas;//Convert.ToDecimal(gvAportes.DataKeys[e.NewEditIndex].Values[2].ToString());
        Session[pPersona.cod_persona + "TipoProducto"] = "Aporte";
        Session[pPersona.cod_persona + "CodProducto"] = "1";
        Navegar("~/Pages/Asociado/EstadoCuenta/AplicarPagos.aspx");
        e.NewEditIndex = -1;
    }

    protected void gvAportes_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        decimal interescausado = 0;
        decimal totalsaldo = 0;
        string pTipo = string.Empty;
        string pVrPago = string.Empty;
        var html = string.Empty;
        List<xpinnWSEstadoCuenta.Aporte> lstAportes = (List<xpinnWSEstadoCuenta.Aporte>)Session["ListaAportes"];
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            interescausado = 0;
            totalsaldo = 0;
            Label lblBeneficiario = (Label)e.Row.FindControl("lblBeneficiario");
            if (lblBeneficiario != null)
                lblBeneficiario.Visible = false;
            pTipo = gvAportes.DataKeys[e.Row.RowIndex].Values[3].ToString();
            pVrPago = gvAportes.DataKeys[e.Row.RowIndex].Values[2].ToString();
            if (!string.IsNullOrEmpty(pVrPago))
            {
                if (Convert.ToDecimal(pVrPago) <= 0)
                {
                    LinkButton btnPagar = (LinkButton)e.Row.FindControl("btnPagar");
                    btnPagar.Visible = false;
                }
            }
            if (pTipo != "&nbsp;")
            {
                if (pTipo == "CLUB AHORRADOR")
                {
                    lblBeneficiario.Visible = true;
                    e.Row.BackColor = System.Drawing.Color.FromName("#F3E2A9");
                }
            }
            /*Se valida que cuenta del aporte es dentro de un grupo la principal
             esto para cancelar el valor total del grupo de aportes*/
            if (gvAportes.DataKeys[e.Row.RowIndex].Values[5] != null)
            {
                String Principal = gvAportes.DataKeys[e.Row.RowIndex].Values[5].ToString();
                if (Principal == "1")
                {
                    foreach (GridViewRow rfila in gvAportes.Rows)
                    {
                        LinkButton btnPagar = (LinkButton)e.Row.FindControl("btnPagar");
                        btnPagar.Visible = true;
                    }
                }
                else
                {
                    LinkButton btnPagar = (LinkButton)e.Row.FindControl("btnPagar");
                    btnPagar.Visible = false;
                }
            }
        }
    }


    //MOVIMIENTOS
    protected void gvAportes_SelectedIndexChanged(object sender, EventArgs e)
    {
        pPersona = (xpinnWSLogin.Persona1)Session["persona"];
        String id = gvAportes.Rows[gvAportes.SelectedRow.RowIndex].Cells[2].Text;
        Session[pPersona.cod_persona + "NroProducto"] = id;
        Response.Redirect("~/Pages/Asociado/Movimientos/Aportes.aspx");
    }

    protected void gvAhorros_RowEditing(object sender, GridViewEditEventArgs e)
    {
        pPersona = (xpinnWSLogin.Persona1)Session["persona"];
        String id = gvAhorros.Rows[e.NewEditIndex].Cells[3].Text;
        Session[pPersona.cod_persona + "Identificacion"] = ((Label)frvData.FindControl("txtIdentificacion")).Text;
        Session[pPersona.cod_persona + "Nombre"] = ((Label)frvData.FindControl("txtNombre")).Text;
        Session[pPersona.cod_persona + "NroProducto"] = id;
        Session[pPersona.cod_persona + "VrPago"] = Convert.ToDecimal(gvAhorros.Rows[e.NewEditIndex].Cells[11].Text.Replace(".", "").Replace(",", "").Replace("$", ""));
        Session[pPersona.cod_persona + "TipoProducto"] = "Ahorros a la vista";
        Session[pPersona.cod_persona + "CodProducto"] = "1";
        Navegar("~/Pages/Asociado/Movimientos/Vista.aspx");
        e.NewEditIndex = -1;
    }

    protected void gvAhorros_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("Pagar"))
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            pPersona = (xpinnWSLogin.Persona1)Session["persona"];
            String id = gvAhorros.Rows[index].Cells[3].Text;
            Session[pPersona.cod_persona + "Identificacion"] = ((Label)frvData.FindControl("txtIdentificacion")).Text;
            Session[pPersona.cod_persona + "Nombre"] = ((Label)frvData.FindControl("txtNombre")).Text;
            Session[pPersona.cod_persona + "NroProducto"] = id;
            Session[pPersona.cod_persona + "VrPago"] = Convert.ToDecimal(gvAhorros.Rows[index].Cells[11].Text.Replace(".", "").Replace(",", "").Replace("$", ""));
            Session[pPersona.cod_persona + "TipoProducto"] = "Ahorros a la vista";
            Session[pPersona.cod_persona + "CodProducto"] = "1";
            Navegar("~/Pages/Asociado/EstadoCuenta/AplicarPagos.aspx");
        }
    }

    protected void gvAhoProgra_RowEditing(object sender, GridViewEditEventArgs e)
    {
        pPersona = (xpinnWSLogin.Persona1)Session["persona"];
        String id = gvAhoProgra.Rows[e.NewEditIndex].Cells[2].Text;
        Session[pPersona.cod_persona + "NroProducto"] = id;
        Session[pPersona.cod_persona + "CodProducto"] = "9";
        Response.Redirect("~/Pages/Asociado/Movimientos/Programado.aspx");
    }

    protected void gvCDATS_RowEditing(object sender, GridViewEditEventArgs e)
    {
        pPersona = (xpinnWSLogin.Persona1)Session["persona"];
        String id = gvCDATS.Rows[e.NewEditIndex].Cells[4].Text;
        Session[pPersona.cod_persona + "Identificacion"] = ((Label)frvData.FindControl("txtIdentificacion")).Text;
        Session[pPersona.cod_persona + "Nombre"] = ((Label)frvData.FindControl("txtNombre")).Text;
        Session[pPersona.cod_persona + "NroProducto"] = id;
        Session[pPersona.cod_persona + "VrPago"] = Convert.ToDecimal(gvCDATS.Rows[e.NewEditIndex].Cells[10].Text.Replace(".", "").Replace(",", "").Replace("$", ""));
        Session[pPersona.cod_persona + "TipoProducto"] = "CDT";
        Session[pPersona.cod_persona + "CodProducto"] = "1";        
        e.NewEditIndex = -1;
        Navegar("~/Pages/Asociado/Movimientos/Cdat.aspx");
    }

    protected void gvCDATS_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("Pagar"))
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            pPersona = (xpinnWSLogin.Persona1)Session["persona"];
            String id = gvCDATS.Rows[index].Cells[3].Text;
            Session[pPersona.cod_persona + "Identificacion"] = ((Label)frvData.FindControl("txtIdentificacion")).Text;
            Session[pPersona.cod_persona + "Nombre"] = ((Label)frvData.FindControl("txtNombre")).Text;
            Session[pPersona.cod_persona + "NroProducto"] = id;
            Session[pPersona.cod_persona + "VrPago"] = Convert.ToDecimal(gvCDATS.Rows[index].Cells[7].Text.Replace(".", "").Replace(",", "").Replace("$", ""));
            Session[pPersona.cod_persona + "TipoProducto"] = "CDATS";
            Session[pPersona.cod_persona + "CodProducto"] = "1";
            Navegar("~/Pages/Asociado/EstadoCuenta/AplicarPagos.aspx");
        }
    }


    protected void gvServicio_SelectedIndexChanged(object sender, EventArgs e)
    {
        pPersona = (xpinnWSLogin.Persona1)Session["persona"];
        string id = gvServicio.DataKeys[gvServicio.SelectedRow.RowIndex].Values[0].ToString();
        Session[pPersona.cod_persona + "NroProducto"] = id;
        Session[pPersona.cod_persona + "CodProducto"] = "4";
        Response.Redirect("~/Pages/Asociado/Movimientos/Servicios.aspx");
    }


    //protected void gvAhoProgra_RowEditing(object sender, GridViewEditEventArgs e)
    //{
    //    pPersona = (xpinnWSLogin.Persona1)Session["persona"];
    //    String id = gvCreditos.Rows[e.NewEditIndex].Cells[3].Text;

    //    Session[pPersona.cod_persona + "Identificacion"] = ((Label)frvData.FindControl("txtIdentificacion")).Text;
    //    Session[pPersona.cod_persona + "Nombre"] = ((Label)frvData.FindControl("txtNombre")).Text;
    //    Session[pPersona.cod_persona + "NroProducto"] = id;
    //    Session[pPersona.cod_persona + "VrPago"] = Convert.ToDecimal(gvCreditos.DataKeys[e.NewEditIndex].Values[2]).ToString("n0");
    //    Session[pPersona.cod_persona + "TipoProducto"] = "Crédito";
    //    Session[pPersona.cod_persona + "CodProducto"] = "2";
    //    Navegar("~/Pages/Asociado/EstadoCuenta/AplicarPagos.aspx");
    //    e.NewEditIndex = -1;
    //}



    #endregion

    #region EVENTOS GRIDVIEW CREDITOS

    protected void gvCreditos_RowEditing(object sender, GridViewEditEventArgs e)
    {
        pPersona = (xpinnWSLogin.Persona1)Session["persona"];
        String id = gvCreditos.Rows[e.NewEditIndex].Cells[3].Text;

        Session[pPersona.cod_persona + "Identificacion"] = ((Label)frvData.FindControl("txtIdentificacion")).Text;
        Session[pPersona.cod_persona + "Nombre"] = ((Label)frvData.FindControl("txtNombre")).Text;
        Session[pPersona.cod_persona + "NroProducto"] = id;
        Session[pPersona.cod_persona + "VrPago"] = Convert.ToDecimal(gvCreditos.DataKeys[e.NewEditIndex].Values[2]).ToString("n0");
        Session[pPersona.cod_persona + "TipoProducto"] = "Crédito";
        Session[pPersona.cod_persona + "CodProducto"] = "2";
        Navegar("~/Pages/Asociado/EstadoCuenta/AplicarPagos.aspx");
        e.NewEditIndex = -1;
    }

    protected void gvCreditos_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        string pVrPago = string.Empty;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblBeneficiario = (Label)e.Row.FindControl("lblBeneficiario");
            if (lblBeneficiario != null)
                lblBeneficiario.Visible = false;
            pVrPago = gvCreditos.DataKeys[e.Row.RowIndex].Values[2].ToString();
            if (!string.IsNullOrEmpty(pVrPago))
            {
                if (Convert.ToDecimal(pVrPago) <= 0)
                {
                    LinkButton btnPagar = (LinkButton)e.Row.FindControl("btnPagar");
                    btnPagar.Visible = false;
                }
            }
            string pTipo = gvCreditos.DataKeys[e.Row.RowIndex].Values[3].ToString();
            if (pTipo != "&nbsp;")
            {
                if (pTipo == "CLUB AHORRADOR")
                {
                    lblBeneficiario.Visible = true;
                    e.Row.BackColor = System.Drawing.Color.FromName("#F3E2A9");

                }
            }
        }
    }

    protected void gvCreditos_SelectedIndexChanged(object sender, EventArgs e)
    {
        pPersona = (xpinnWSLogin.Persona1)Session["persona"];
        String id = gvCreditos.Rows[gvCreditos.SelectedRow.RowIndex].Cells[3].Text;
        Session[pPersona.cod_persona + "NroProducto"] = id;
        Response.Redirect("~/Pages/Asociado/Movimientos/Credito.aspx");
    }

    protected void gvCreditos_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            pPersona = (xpinnWSLogin.Persona1)Session["persona"];
            String id = gvCreditos.Rows[e.RowIndex].Cells[3].Text;
            Session["credito"] = id;
            Navegar("~/Pages/Credito/Solicitud/Garantias.aspx");
        }
        catch (Exception ex)
        {
            lblError.Text = "Se presentó un problema, intentelo nuevamente"; lblError.Visible = true;
        }
    }
    #endregion

    #region EVENTOS GRIDVIEW SERVICIOS    

    protected void gvServicio_RowEditing(object sender, GridViewEditEventArgs e)
    {
        pPersona = (xpinnWSLogin.Persona1)Session["persona"];
        string id = gvServicio.DataKeys[e.NewEditIndex].Values[0].ToString();

        Session[pPersona.cod_persona + "Identificacion"] = ((Label)frvData.FindControl("txtIdentificacion")).Text;
        Session[pPersona.cod_persona + "Nombre"] = ((Label)frvData.FindControl("txtNombre")).Text;
        Session[pPersona.cod_persona + "NroProducto"] = id;
        Session[pPersona.cod_persona + "VrPago"] = Convert.ToDecimal(gvServicio.DataKeys[e.NewEditIndex].Values[2]).ToString("n0");
        Session[pPersona.cod_persona + "TipoProducto"] = "Aporte";
        Session[pPersona.cod_persona + "CodProducto"] = "1";
        Navegar("~/Pages/Asociado/EstadoCuenta/AplicarPagos.aspx");
        e.NewEditIndex = -1;
    }

    #endregion

    #region EVENTOS GRIDVIEW AHORROS
    /// <summary>
    /// Establece la fila seleccionada de AhorrosVista bajo la acción Hacer Retiro
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvAhorros_SelectedIndexChanged(object sender, EventArgs e)
    {
        pPersona = (xpinnWSLogin.Persona1)Session["persona"];

        decimal saldo = Convert.ToDecimal(gvAhorros.Rows[gvAhorros.SelectedRow.RowIndex].Cells[7].Text.ToString().Replace(",", "").Replace(".", "").Replace("$", ""));
        //Convert.ToDecimal(gvAhorros.Rows[e.NewEditIndex].Cells[11].Text.Replace(".", "").Replace(",", "").Replace("$", ""));
        txtFechaSolicitud.Text = DateTime.Today.ToString("dd/MM/yyyy");
        txtNumeroProducto.Text = gvAhorros.Rows[gvAhorros.SelectedRow.RowIndex].Cells[3].Text;
        txtDisponible.Text = (saldo).ToString("C0");
        tipo_prod_retiro.Text = "3";
        cargarBanco();
        lblError.Visible = false;
        btnRetiro.Visible = true;
        pnlDatosRetiro.Visible = true;
        pnlHacerRetiro.Visible = true;
        panelConsulta.Visible = false;
    }
    #endregion

    protected void gvCDATS_SelectedIndexChanged(object sender, EventArgs e)
    {
        pPersona = (xpinnWSLogin.Persona1)Session["persona"];
        decimal saldo = Convert.ToDecimal((gvCDATS.Rows[gvCDATS.SelectedRow.RowIndex].Cells[14].Text.ToString()).Replace(",", "").Replace(".", "").Replace("$", ""));
        //5-6       

        txtFechaSolicitud.Text = DateTime.Today.ToString("dd/MM/yyyy");
        txtNumeroProducto.Text = gvCDATS.Rows[gvCDATS.SelectedRow.RowIndex].Cells[3].Text;
        txtDisponible.Text = saldo.ToString("c0");
        txtValorRetiro.Text = saldo.ToString("c0");
        txtValorRetiro.Enabled = false;
        tipo_prod_retiro.Text = "5";
        // 5 cdat
        cargarBanco();
        lblError.Visible = false;
        btnRetiro.Visible = true;
        pnlDatosRetiro.Visible = true;
        pnlHacerRetiro.Visible = true;
        panelConsulta.Visible = false;
    }

    protected void gvAhoProgra_SelectedIndexChanged(object sender, EventArgs e)
    {
        pPersona = (xpinnWSLogin.Persona1)Session["persona"];
        decimal saldo = Convert.ToDecimal((gvAhoProgra.Rows[gvAhoProgra.SelectedRow.RowIndex].Cells[8].Text.ToString()).Replace(",", "").Replace(".", "").Replace("$", ""));
        //5-6
        txtFechaSolicitud.Text = DateTime.Today.ToString("dd/MM/yyyy");
        txtNumeroProducto.Text = gvAhoProgra.Rows[gvAhoProgra.SelectedRow.RowIndex].Cells[3].Text;
        txtDisponible.Text = saldo.ToString("C0");
        txtValorRetiro.Text = saldo.ToString("C0");
        txtValorRetiro.Enabled = false;
        tipo_prod_retiro.Text = "9";
        // 9 cdats
        cargarBanco();
        lblError.Visible = false;
        btnRetiro.Visible = true;
        pnlDatosRetiro.Visible = true;
        pnlHacerRetiro.Visible = true;
        panelConsulta.Visible = false;
    }

    #region EVENTOS DE DEVOLUCIONES

    protected void ActualizarDevoluciones(xpinnWSLogin.Persona1 Persona)
    {
        if (Persona == null)
            return;
        string pFiltroDev = "And p.identificacion = '" + Persona.identificacion + "'";
        List<xpinnWSEstadoCuenta.Devolucion> lstDevolucion = null;
        if (ViewState["DTDEVOLUCION"] == null)
            lstDevolucion = service.ListarDevolucion(pFiltroDev, Session["sec"].ToString());
        else
            lstDevolucion = (List<xpinnWSEstadoCuenta.Devolucion>)ViewState["DTDEVOLUCION"];

        if (lstDevolucion != null && lstDevolucion.Count > 0)
        {
            ViewState["DTDEVOLUCION"] = lstDevolucion;
            // VERIFICAR SI INDICA MOSTAR SIN SALDO
            if (!chekSaldo.Checked)
                lstDevolucion = lstDevolucion.Where(x => x.saldo != 0).ToList();

            pnlDevolucion.Visible = true;
            gvDevolucion.DataSource = lstDevolucion;
            gvDevolucion.DataBind();
            lblMsjDevolucion.Text = "Registros encontrados " + lstDevolucion.Count();
        }
        else
        {
            pnlDevolucion.Visible = false;
            lblMsjDevolucion.Text = "No se encontraron Datos";
        }
    }


    protected void chekSaldo_CheckedChanged(object sender, EventArgs e)
    {
        pPersona = (xpinnWSLogin.Persona1)Session["persona"];
        ActualizarDevoluciones(pPersona);
    }

    #endregion
    // PENDIENTE METODOS DE LOS DEMAS PRODUCTOS


    #region Proceso Impresion

    protected void btnImprimir_Click(object sender, EventArgs e)
    {
        VerError("");
        try
        {
            // APORTES
            System.Data.DataTable tableAporte = new System.Data.DataTable();
            tableAporte.Columns.Add("numero_aporte");
            tableAporte.Columns.Add("estado");
            tableAporte.Columns.Add("linea");
            tableAporte.Columns.Add("fecha_apertura");
            tableAporte.Columns.Add("saldo_total");
            tableAporte.Columns.Add("valor_cuota");
            tableAporte.Columns.Add("fecha_prox_pago");
            tableAporte.Columns.Add("valor_acumulado");
            tableAporte.Columns.Add("valor_total_acumu");

            List<xpinnWSEstadoCuenta.Aporte> lstAporte = new List<xpinnWSEstadoCuenta.Aporte>();
            lstAporte = (List<xpinnWSEstadoCuenta.Aporte>)ViewState["APORTES"];
            if (lstAporte.Count > 0)
            {
                foreach (xpinnWSEstadoCuenta.Aporte item in lstAporte)
                {
                    DataRow data;

                    data = tableAporte.NewRow();
                    data[0] = item.numero_aporte;
                    data[1] = item.estado_Linea;
                    data[2] = item.nom_linea_aporte;
                    data[3] = item.fecha_apertura.ToShortDateString();
                    data[4] = item.Saldo;
                    data[5] = item.cuota;
                    data[6] = item.fecha_proximo_pago.ToShortDateString();
                    data[7] = item.valor_acumulado;
                    data[8] = item.valor_total_acumu;

                    tableAporte.Rows.Add(data);
                }
            }


            System.Data.DataTable tableCred = new System.Data.DataTable();
            tableCred.Columns.Add("Estado");
            tableCred.Columns.Add("Linea");
            tableCred.Columns.Add("MontoAprobado");
            tableCred.Columns.Add("SaldoCapital");
            tableCred.Columns.Add("vrCuota");
            tableCred.Columns.Add("CtaPagadas");
            tableCred.Columns.Add("Plazo");
            tableCred.Columns.Add("FechaProximoPago");
            tableCred.Columns.Add("Vlrapagar");
            tableCred.Columns.Add("Vlrtotalapagar");
            tableCred.Columns.Add("NumRadicion");
            tableCred.Columns.Add("FechaDesembolso");
            tableCred.Columns.Add("Tasainteres");
            tableCred.Columns.Add("NombreOficina");
            tableCred.Columns.Add("pagadurias");
            tableCred.Columns.Add("fecha_termina");

            List<xpinnWSEstadoCuenta.ProductoResumen> lstEstadoCuenta = new List<xpinnWSEstadoCuenta.ProductoResumen>();
            lstEstadoCuenta = (List<xpinnWSEstadoCuenta.ProductoResumen>)ViewState["DTCREDITOS"];
            if (lstEstadoCuenta != null)
            {
                foreach (xpinnWSEstadoCuenta.ProductoResumen item in lstEstadoCuenta)
                {
                    DataRow datarw;
                    datarw = tableCred.NewRow();
                    datarw[0] = item.estado;
                    datarw[1] = item.linea;
                    datarw[2] = item.monto;
                    datarw[3] = item.saldo;
                    datarw[4] = item.valorcuota;
                    datarw[5] = item.CuotasPagadas;
                    datarw[6] = item.Plazo;
                    datarw[7] = item.fechaproximopago.ToShortDateString();
                    datarw[8] = item.valorapagar;
                    datarw[9] = item.valortotalapagar;
                    datarw[10] = item.numero_producto;
                    datarw[11] = item.fechaapertura != null ? Convert.ToDateTime(item.fechaapertura).ToString(gFormatoFecha) : " ";
                    datarw[12] = item.Tasainteres;
                    datarw[13] = item.NombreOficina;
                    datarw[14] = item.pagadurias;
                    datarw[15] = item.fecha_vencimiento != null ? Convert.ToDateTime(item.fecha_vencimiento).ToString(gFormatoFecha) : " ";

                    tableCred.Rows.Add(datarw);
                }
            }

            // ACODEUDADOS
            System.Data.DataTable tableAcodeudados = new System.Data.DataTable();
            tableAcodeudados.Columns.Add("NumRadicacion");
            tableAcodeudados.Columns.Add("cod_persona");
            tableAcodeudados.Columns.Add("Estado");
            tableAcodeudados.Columns.Add("Linea");
            tableAcodeudados.Columns.Add("Montos");
            tableAcodeudados.Columns.Add("Saldos");
            tableAcodeudados.Columns.Add("Cuotas");
            tableAcodeudados.Columns.Add("Nombre");
            tableAcodeudados.Columns.Add("ciudad");
            tableAcodeudados.Columns.Add("Valor_apagar");
            tableAcodeudados.Columns.Add("fecha_prox_pago");
            tableAcodeudados.Columns.Add("Identificacion");

            List<xpinnWSEstadoCuenta.Acodeudados> lstAcodeudados = new List<xpinnWSEstadoCuenta.Acodeudados>();
            lstAcodeudados = (List<xpinnWSEstadoCuenta.Acodeudados>)ViewState["DTACODEUDADOS"];
            if (ViewState["DTACODEUDADOS"] != null)
            {
                foreach (xpinnWSEstadoCuenta.Acodeudados asc in lstAcodeudados)
                {
                    DataRow datas;
                    datas = tableAcodeudados.NewRow();
                    datas[0] = asc.NumRadicacion.ToString();
                    datas[1] = asc.CodPersona.ToString();
                    datas[2] = asc.Estado;
                    datas[3] = asc.Linea;
                    datas[4] = Convert.ToDecimal(asc.Monto).ToString("n0");
                    datas[5] = Convert.ToDecimal(asc.Saldo).ToString("n0");
                    datas[6] = Convert.ToDecimal(asc.Cuota).ToString("n0");
                    datas[7] = asc.Nombres;
                    datas[8] = asc.ciudad;
                    datas[9] = Convert.ToDecimal(asc.Valor_apagar).ToString("n0");
                    datas[10] = Convert.ToDateTime(asc.FechaProxPago).ToShortDateString();
                    datas[11] = asc.identificacion;

                    tableAcodeudados.Rows.Add(datas);
                }
            }


            DataTable tableAhorro = new DataTable();
            if (ViewState["DTAHORRO"] != null)
            {
                string[] pEncabezado = { "numero_cuenta", "nom_estado", "nom_linea", "fecha_apertura", "saldo_total", "saldo_canje", "nom_oficina", "valor_cuota", "fecha_proximo_pago", "valor_acumulado", "valor_total_acumu" };
                List<xpinnWSDeposito.AhorroVista> lstAhorro = new List<xpinnWSDeposito.AhorroVista>();
                lstAhorro = (List<xpinnWSDeposito.AhorroVista>)ViewState["DTAHORRO"];
                tableAhorro = lstAhorro.ToDataTable(pEncabezado);
            }

            DataTable tableAhoPro = new DataTable();
            if (ViewState["DTAHOPRO"] != null)
            {
                string[] pEncabezado = { "numero_programado", "nom_estado", "nomlinea", "fecha_apertura", "saldo", "nomoficina", "valor_cuota", "fecha_proximo_pago", "valor_acumulado", "valor_total_acumu", "plazo", "cuotas_pagadas", "fecha_ultimo_pago", "fecha_vencimiento", "nom_periodicidad", "tasa_interes", };
                List<xpinnWSDeposito.CuentasProgramado> lstProgramado = new List<xpinnWSDeposito.CuentasProgramado>();
                lstProgramado = (List<xpinnWSDeposito.CuentasProgramado>)ViewState["DTAHOPRO"];
                tableAhoPro = lstProgramado.ToDataTable(pEncabezado);
            }

            DataTable tableServi = new DataTable();
            if (ViewState["DTSERVICIO"] != null)
            {
                string[] pEncabezado = { "numero_servicio", "estado", "nom_linea", "fecha_solicitud", "nom_plan", "fecha_inicio_vigencia", "fecha_final_vigencia", "valor_total", "valor_cuota", "saldo", "numero_cuotas", "fecha_proximo_pago", "fecha_activacion", "nom_forma_pago" };
                List<xpinnWSEstadoCuenta.Servicio> lstServicios = new List<xpinnWSEstadoCuenta.Servicio>();
                lstServicios = (List<xpinnWSEstadoCuenta.Servicio>)ViewState["DTSERVICIO"];
                tableServi = lstServicios.ToDataTable(pEncabezado);
            }

            DataTable tableCdat = new DataTable();
            if (ViewState["DTCDAT"] != null)
            {
                string[] pEncabezado = { "numero_cdat", "nom_estado", "nomlinea", "fecha_apertura", "fecha_inicio", "fecha_vencimiento", "nom_oficina", "valor", "plazo", "tasa_interes", "valor_acumulado", "valor_total_acumu" };
                List<xpinnWSDeposito.Cdat> lstCdat = new List<xpinnWSDeposito.Cdat>();
                lstCdat = (List<xpinnWSDeposito.Cdat>)ViewState["DTCDAT"];
                tableCdat = lstCdat.ToDataTable(pEncabezado);
            }

            xpinnWSLogin.Persona1 pPersona = (xpinnWSLogin.Persona1)Session["persona"];

            ReportParameter[] param = new ReportParameter[23];
            param[0] = new ReportParameter("Entidad", pPersona.empresa);
            param[1] = new ReportParameter("fechahora", " " + DateTime.Now);
            param[2] = new ReportParameter("IdPersona", pPersona.cod_persona.ToString());
            param[3] = new ReportParameter("NumeroDocumento", pPersona.identificacion);
            param[4] = new ReportParameter("pFecha", Convert.ToString(DateTime.Now.ToShortDateString()));
            param[5] = new ReportParameter("email", " " + ((Label)frvData.FindControl("lblEmail")).Text);
            param[6] = new ReportParameter("nombre", " " + ((Label)frvData.FindControl("txtNombre")).Text);
            param[7] = new ReportParameter("fechaingreso", " " + ((Label)frvData.FindControl("lblFechaAfiliacion")).Text);
            param[8] = new ReportParameter("estado", " " + ((Label)frvData.FindControl("lblEstado")).Text);
            param[9] = new ReportParameter("tipocliente", " " + ((Label)frvData.FindControl("lblTipoCliente")).Text);
            param[10] = new ReportParameter("motivo", " " + ((Label)frvData.FindControl("lblMotivo")).Text);
            param[11] = new ReportParameter("direccion", " " + HttpUtility.HtmlDecode(((Label)frvData.FindControl("txtDireccion")).Text));
            param[12] = new ReportParameter("tipoidentificacion", pPersona.nomtipo_identificacion);
            param[13] = new ReportParameter("telefono", pPersona.telefono);
            param[14] = new ReportParameter("CiudadDireccion", " ");
            param[15] = new ReportParameter("ImagenReport", ImagenReporte());
            if (gvAportes.Rows.Count > 0)
                param[16] = new ReportParameter("ReporteAporte", "false");
            else
                param[16] = new ReportParameter("ReporteAporte", "true");
            if (gvAcodeudados.Rows.Count > 0)
                param[17] = new ReportParameter("ReporteAcodeudados", "false");
            else
                param[17] = new ReportParameter("ReporteAcodeudados", "true");

            string pResult = tableAhorro.Rows.Count > 0 ? "false" : "true";
            param[18] = new ReportParameter("ReporteAhoVista", pResult);
            pResult = tableAhoPro.Rows.Count > 0 ? "false" : "true";
            param[19] = new ReportParameter("ReporteProgra", pResult);
            pResult = tableServi.Rows.Count > 0 ? "false" : "true";
            param[20] = new ReportParameter("ReporteServicio", pResult);
            pResult = tableCdat.Rows.Count > 0 ? "false" : "true";
            param[21] = new ReportParameter("ReporteCdat", pResult);
            param[22] = new ReportParameter("TituloAhoVista", lblTituloAhorroVista.Text.ToUpper());
            rptEstadoCuenta.LocalReport.EnableExternalImages = true;
            rptEstadoCuenta.LocalReport.SetParameters(param);
            rptEstadoCuenta.LocalReport.DataSources.Clear();

            ReportDataSource rds = new ReportDataSource("DataSet1", tableAporte);
            rptEstadoCuenta.LocalReport.DataSources.Add(rds);
            ReportDataSource rds1 = new ReportDataSource("DataSet2", tableCred);
            rptEstadoCuenta.LocalReport.DataSources.Add(rds1);
            ReportDataSource rds2 = new ReportDataSource("DataSet3", tableAcodeudados);
            rptEstadoCuenta.LocalReport.DataSources.Add(rds2);
            ReportDataSource rds3 = new ReportDataSource("DataSet4", tableAhorro);
            rptEstadoCuenta.LocalReport.DataSources.Add(rds3);
            ReportDataSource rds4 = new ReportDataSource("DataSet5", tableAhoPro);
            rptEstadoCuenta.LocalReport.DataSources.Add(rds4);
            ReportDataSource rds5 = new ReportDataSource("DataSet6", tableServi);
            rptEstadoCuenta.LocalReport.DataSources.Add(rds5);
            ReportDataSource rds6 = new ReportDataSource("DataSet7", tableCdat);
            rptEstadoCuenta.LocalReport.DataSources.Add(rds6);

            rptEstadoCuenta.LocalReport.Refresh();

            // ELIMINANDO ARCHIVOS GENERADOS
            try
            {
                string[] ficherosCarpeta = Directory.GetFiles(Server.MapPath("Archivos\\"));
                foreach (string ficheroActual in ficherosCarpeta)
                    if (ficheroActual.Contains(pPersona.nombre))
                        File.Delete(ficheroActual);
            }
            catch
            { }
            //CREANDO REPORTE
            string pNomUsuario = pPersona.nombre != "" && pPersona.nombre != null ? "_" + pPersona.nombre + DateTime.Now.ToString("HHmmss") : "";

            Warning[] warnings;
            string[] streamids;
            string mimeType;
            string encoding;
            string extension;
            byte[] bytes = rptEstadoCuenta.LocalReport.Render("PDF", null, out mimeType,
                               out encoding, out extension, out streamids, out warnings);
            FileStream fs = new FileStream(HttpContext.Current.Server.MapPath("Archivos/output" + pNomUsuario + ".pdf"),
                FileMode.Create);
            fs.Write(bytes, 0, bytes.Length);
            fs.Close();
            //MOSTRANDO REPORTE
            string adjuntar = "<object data=\"{0}\" type=\"application/pdf\" width=\"100%\" height=\"550px\">";
            adjuntar += "If you are unable to view file, you can download from <a href = \"{0}\">here</a>";
            adjuntar += " or download <a target = \"_blank\" href = \"http://get.adobe.com/reader/\">Adobe PDF Reader</a> to view the file.";
            adjuntar += "</object>";

            ltReport.Text = string.Format(adjuntar, ResolveUrl("Archivos/output" + pNomUsuario + ".pdf"));

            panelGeneral.Visible = false;
            panelImpresion.Visible = true;
            btnConsolidado.Visible = false;
            btnRegresar.Visible = true;
            rptEstadoCuenta.Visible = false;
            ltReport.Visible = true;
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }

    #endregion


    protected void btnRegresar_Click(object sender, EventArgs e)
    {
        txtValorRetiro.Enabled = true;
        VerError("");
        btnRegresar.Visible = false;
        btnConsolidado.Visible = true;
        panelGeneral.Visible = true;
        panelImpresion.Visible = false;
    }

    public void cargarBanco()
    {
        //Cargar bancos
        xpinnWSAppFinancial.Bancos bancos = new xpinnWSAppFinancial.Bancos();
        List<xpinnWSAppFinancial.Bancos> lstBancos = new List<xpinnWSAppFinancial.Bancos>();
        lstBancos = BOFinancial.ListarBancos(bancos, Session["sec"].ToString());

        //ddlBancos.Items.Clear();            
        ddlBancos.DataTextField = "nombrebanco";
        ddlBancos.DataValueField = "cod_banco";
        ddlBancos.DataSource = lstBancos;
        ddlBancos.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Seleccione un item", ""));
        ddlBancos.DataBind();

        //Cargar Datos de Cuenta Bancaria
        pPersona = (xpinnWSLogin.Persona1)Session["persona"];
        xpinnWSEstadoCuenta.AhorroVista cuenta = service.ConsultarCuentaBancaria(Convert.ToString(pPersona.cod_persona), Session["sec"].ToString());

        if (cuenta != null && !string.IsNullOrEmpty(cuenta.numero_cuenta))
        {
            txtNumCuenta.Text = cuenta.numero_cuenta;
            try { ddlTipoCuenta.SelectedValue = Convert.ToString(cuenta.tipo_cuenta); } catch { lblError.Text = cuenta.tipo_cuenta.ToString(); lblError.Visible = true; }
            ddlBancos.SelectedValue = Convert.ToString(cuenta.cod_banco);
            //Muestra panel de transferencia
            panelTransferencia.Visible = true;
        }
        else
        {
            string empresa = ConfigurationManager.AppSettings["Empresa"] != null ?
                ConfigurationManager.AppSettings["Empresa"].ToString() : "la entidad";
            lblError.Text = "No tiene un numero de cuenta asociado. Comuniquese con " + empresa;
            lblError.Visible = true;
        }
    }

    protected void btnRetiro_Click(object sender, EventArgs e)
    {
        if (validarCamposRetiro())
        {
            pPersona = (xpinnWSLogin.Persona1)Session["persona"];
            xpinnWSDeposito.AhorroVista ahorro = new xpinnWSDeposito.AhorroVista();
            ahorro.cod_persona = pPersona.cod_persona;
            ahorro.numero_cuenta = txtNumeroProducto.Text;
            ahorro.observaciones = txtObsRetiro.Text;
            if (ddlFormaDesembolso.SelectedValue == "3")
            {
                ahorro.cod_banco = Convert.ToInt32(ddlBancos.SelectedValue);
                ahorro.numero_cuenta_final = txtNumCuenta.Text;
                ahorro.tipo_cuenta = Convert.ToInt32(ddlTipoCuenta.SelectedValue);
            }
            else
            {
                ahorro.cod_banco = 0;
                ahorro.numero_cuenta_final = "0";
                ahorro.tipo_cuenta = 0;
            }
            string x = txtDisponible.Text.ToString().Replace(",", "").Replace(".", "").Replace("$", "");
            var disponible = Convert.ToInt32(x);
            if (disponible >= Convert.ToInt64(txtValorRetiro.Text.Replace(",", "").Replace(".", "").Replace("$", "")))
            {
                ahorro.retiro = Convert.ToInt64(txtValorRetiro.Text.Replace(",", "").Replace(".", "").Replace("$", ""));
                ahorro.forma_giro = Convert.ToInt32(ddlFormaDesembolso.SelectedValue);
                ahorro.estado = 0;
                if (pnlCierre.Visible)
                    ahorro.estadocierre = ddlCierre.SelectedValue;
                else
                    ahorro.estadocierre = "NO";
                ahorro.tipo_producto = Convert.ToInt64(tipo_prod_retiro.Text.Trim());
                int solicitud = BODeposito.CrearSolicitudRetiroAhorro(ahorro, Session["sec"].ToString());

                if (solicitud != 0)
                {
                    limpiarRetiro();
                    lblError.Text = "Tu solicitud se generó correctamente con el código " + solicitud + ". Si realizaste la solicitud antes de las 9:00 a.m. la devolución se hará el mismo día hábil, si generaste la solicitud después de esta hora, la devolución se hará el siguiente día hábil. ";
                    lblError.Visible = true;
                    btnRetiro.Visible = false;
                    pnlDatosRetiro.Visible = false;
                }
                else
                {
                    lblError.Text = "Se presentó un problema, intentelo nuevamente";
                    lblError.Visible = true;
                }
            }
            else
            {
                lblError.Text = "El valor del retiro no debe superar el saldo disponible";
                lblError.Visible = true;
            }
        }

    }

    protected void btnCerrarRetiro_Click(object sender, EventArgs e)
    {
        panelConsulta.Visible = true;
        limpiarRetiro();
        pnlHacerRetiro.Visible = false;
    }

    private void limpiarRetiro()
    {
        txtFechaSolicitud.Text = "";
        txtNumeroProducto.Text = "";
        txtDisponible.Text = "";
        txtValorRetiro.Text = "";
        panelTransferencia.Visible = false;
        lblError.ForeColor = System.Drawing.Color.Red;
    }

    public bool validarCamposRetiro()
    {
        if (String.IsNullOrWhiteSpace(txtValorRetiro.Text))
        { lblError.Text = "Ingrese un valor a retirar"; lblError.Visible = true; return false; }
        if (ddlFormaDesembolso.SelectedValue == "0")
        { lblError.Text = "Seleccione una forma de giro"; lblError.Visible = true; return false; }
        if (ddlFormaDesembolso.SelectedValue == "3")
        {
            if (String.IsNullOrWhiteSpace(txtNumCuenta.Text))
            { lblError.Text = "No es posible continuar sin un numero de cuenta, por favor pongase en contacto con la entidad"; lblError.Visible = true; return false; }
        }
        if (pnlCierre.Visible)
        {
            if (ddlCierre.SelectedValue == "0")
            { lblError.Text = "Va a retirar el total disponible, seleccione si desea cerrar la cuenta o no"; lblError.Visible = true; return false; }
        }
        return true;
    }

    protected void txtValorRetiro_TextChanged(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtValorRetiro.Text.ToString().Replace(",", "").Replace(".", "").Replace("$", "")))
        {
            string x = txtDisponible.Text.ToString().Replace(",", "").Replace(".", "").Replace("$", "");
            string y = txtValorRetiro.Text.ToString().Replace(",", "").Replace(".", "").Replace("$", "");
            string z= y == "" ? "0" : y;
            var disponible = Convert.ToInt32(x);
            if (disponible == Convert.ToInt64(z))///== Convert.ToInt64(txtValorRetiro.Text.ToString().Replace(",", "").Replace(".", "").Replace("$", "")))
            pnlCierre.Visible = true;
            else
                pnlCierre.Visible = false;

        }
    }


    protected void gvCDATS_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        pPersona = (xpinnWSLogin.Persona1)Session["persona"];
        String id = gvCDATS.Rows[e.RowIndex].Cells[4].Text;
        string pFechaIni = gvCDATS.Rows[e.RowIndex].Cells[6].Text;
        string pFechaFin = gvCDATS.Rows[e.RowIndex].Cells[8].Text;

        string doc = BODeposito.ObtenerDocCDAT(id, pFechaIni, pFechaFin, Session["sec"].ToString());
        if (!string.IsNullOrEmpty(doc))
        {
            string cRutaLocalDeArchivoPDF = Server.MapPath("~/files/pdf/CDAT_" + id.Trim() + ".pdf");
            //Añadir la imagen al reporte
            string cRutaDeImagen, cRutaDeImagens;
            string pError = "";
            cRutaDeImagen = Server.MapPath("~/Imagenes\\") + "LogoEmpresa.jpg";
            doc = doc.Replace("pImagenReporte", cRutaDeImagen);
            cRutaDeImagens = Server.MapPath("~/Imagenes\\") + "SelloEmpresa.png";
            doc = doc.Replace("pSelloReporte", cRutaDeImagens);

            ProcessesHTML.ConvertPdf(doc, cRutaLocalDeArchivoPDF, ref pError);

            Boolean bExiste = System.IO.File.Exists(cRutaLocalDeArchivoPDF);
            if (bExiste)
            {
                Response.Write("<script>window.__doPostBack('','');</script>");

                VerError("");

                Response.Clear();
                Response.ClearContent();
                Response.ClearHeaders();
                Response.Buffer = false;
                Response.AddHeader("Content-Disposition", "attachment;filename=" + id.Trim() + ".pdf");
                Response.ContentType = "application/octet-stream";
                Response.Flush();
                Response.WriteFile(cRutaLocalDeArchivoPDF);
                File.Delete(cRutaLocalDeArchivoPDF);
            }
            else
            {
                VerError("No se pudo generar el reporte");
                return;
            }
        }
    }
}