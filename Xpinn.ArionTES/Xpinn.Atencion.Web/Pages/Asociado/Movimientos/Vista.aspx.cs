﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Web.UI;
using xpinnWSEstadoCuenta;

public partial class Vista : GlobalWeb
{
    xpinnWSLogin.Persona1 pPersona;
    xpinnWSDeposito.WSDepositoSoapClient BODeposito = new xpinnWSDeposito.WSDepositoSoapClient();
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
        
                        //Obteniendo datos ahorro vista
                        List<xpinnWSDeposito.AhorroVista> lstAhorros = new List<xpinnWSDeposito.AhorroVista>();
                        string pFiltroAho = " AND A.ESTADO NOT IN (4) AND A.COD_PERSONA = " + pPersona.cod_persona + " AND A.Numero_Cuenta ="+pProducto;
                        lstAhorros = BODeposito.ListarAhorroVistaClubAhorrador(pPersona.cod_persona, pFiltroAho, ManejaClubAhorrador, pPersona.identificacion, pPersona.clavesinecriptar, Session["sec"].ToString());
                
                        if (lstAhorros != null)
                        {
                            List<xpinnWSDeposito.AhorroVista> lstprog = new List<xpinnWSDeposito.AhorroVista>();
                            xpinnWSDeposito.AhorroVista prog = lstAhorros.ElementAt(0);
                            lstprog.Add(prog);
                            frmVista.DataSource = lstprog;
                            frmVista.DataBind();
                            frmVista.Visible = true;
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


            List<xpinnWSDeposito.ReporteMovimiento> lstvista = BODeposito.ListarMovVista(Convert.ToInt64(pProducto),pFecIni, pFecFin, Session["sec"].ToString());
            pGrilla.Visible = false;
            lblInfo.Visible = true;
            lblTotalReg.Visible = false;
            if (lstvista != null && lstvista.Count > 0)
            {
                pGrilla.Visible = true;
                lblInfo.Visible = false;
                lblTotalReg.Visible = true;
                lblTotalReg.Text = "<br/> Movimientos encontrados :" + lstvista.Count;
                gvLista.DataSource = lstvista;
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