using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.IO;
using System.Web.UI.HtmlControls;
using System.Linq;
using Xpinn.Util;
using Xpinn.TarjetaDebito.Entities;
using Xpinn.TarjetaDebito.Services;
using Xpinn.Ahorros.Entities;
using Xpinn.Ahorros.Services;
using System.Net;
using System.Reflection;

partial class Lista : GlobalWeb
{    
    CuentaService CuentaService = new CuentaService();
    InterfazENPACTO interfazEnpacto;
    string convenio = "";
    int? _tipo_convenio = null;

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(CuentaService.CodigoProgramaMovimientoFinancial, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoExportar += btnExportar_Click;
            toolBar.MostrarGuardar(false);
            interfazEnpacto = new InterfazENPACTO("0123456789ABCDEFFEDCBA9876543210", "00000000000000000000000000000000");
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CuentaService.CodigoProgramaMovimientoFinancial, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Usuario usu = (Usuario)Session["Usuario"];
            if (usu != null)
            { 
                if (usu.codperfil == 1)
                {
                    txtNroCuenta.Visible = true;
                    lblNroCuenta.Visible = true;
                }
            }
            txtConvenio.Text = ConvenioTarjeta();
            txtIpAppliance.Text = IpApplianceConvenioTarjeta();
            _tipo_convenio = TipoConvenioTarjeta();
            if (!IsPostBack)
            {                
                txtFecha.Text = DateTime.Now.ToString(gFormatoFecha);
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CuentaService.CodigoProgramaMovimientoFinancial, "Page_Load", ex);
        }
    }


    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        Page.Validate();
        gvLista.Visible = true;
        if (Page.IsValid)
        {
            Actualizar();
        }
    }


    protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvLista.PageIndex = e.NewPageIndex;
            if (Session["DTMOVIMIENTOS"] != null)
            {
                List<Movimiento> lstConsulta = (List<Movimiento>)Session["DTMOVIMIENTOS"];
                gvLista.DataSource = lstConsulta;
                gvLista.DataBind();
                return;
            }
            Actualizar();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CuentaService.CodigoProgramaMovimientoFinancial, "gvLista_PageIndexChanging", ex);
        }
    }

    /// <summary>
    /// Método para consumir el WEBSERVICES de ENPACTO y leer los movimientos en el APPLIANCE
    /// </summary>
    private void Actualizar()
    {
        VerError("");
        try
        {
            // Determinar datos del convenio
            convenio = ConvenioTarjeta();
            string s_usuario_applicance = "";
            string s_clave_appliance = "";
            SeguridadConvenioTarjeta(ref s_usuario_applicance, ref s_clave_appliance);
            interfazEnpacto.ConfiguracionAppliance(IpApplianceConvenioTarjeta(), s_usuario_applicance, s_clave_appliance);

            // Consultar movimientos en FINANCIAL
            string error = "";
            List<Xpinn.TarjetaDebito.Entities.TransaccionFinancial> lstConsulta = new List<TransaccionFinancial>();
            try
            {
                DateTime _fecha = ConvertirStringToDate(txtFecha.Text);
                lstConsulta = CuentaService.ListarTransaccionesFinancial(convenio, _fecha, (Usuario)Session["Usuario"]);
            }
            catch (Exception ex)
            {
                lblError.Text = "No se pudo consultar movimientos. Error:" + ex.Message + " " + error;    
            }

            gvLista.PageSize = 15;
            String emptyQuery = "Fila de datos vacia";
            gvLista.EmptyDataText = emptyQuery;           
            if (lstConsulta.Count > 0)
            {
                // Llenar la tabla
                gvLista.DataSource = lstConsulta;
                gvLista.DataBind();
                panelGrilla.Visible = true;                
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();                               
            }
            else
            {
                panelGrilla.Visible = false;                
                lblTotalRegs.Visible = false;
            }
            Session["DTMOVIMIENTOS"] = lstConsulta;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CuentaService.CodigoProgramaMovimientoFinancial, "Actualizar", ex);
        }
    }

           
    /// <summary>
    /// Método para exportar listado de movimientos
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnExportar_Click(object sender, EventArgs e)
    {
        try
        {
            if (gvLista.Rows.Count > 0 && Session["DTMOVIMIENTOS"] != null)
            {
                string fic = "MovimientosFinancial.csv";
                // Eliminar archivo si ya existe
                try
                {
                    File.Delete(fic);
                }
                catch
                { }
                // Generar el archivo
                List<Movimiento> lstConsulta = (List<Movimiento>)Session["DTMOVIMIENTOS"];
                foreach (Movimiento item in lstConsulta)
                {
                    int i = 0;
                    string texto = "";
                    FieldInfo[] propiedades = item.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
                    foreach (FieldInfo f in propiedades)
                    {
                        i += 1;
                        object valorObject = f.GetValue(item);
                        // Si no soy nulo
                        if (valorObject != null)
                        {
                            string valorString = valorObject.ToString();
                            if (valorObject is DateTime)
                            {
                                DateTime? fechaValidar = valorObject as DateTime?;
                                if (fechaValidar.Value != DateTime.MinValue)
                                {
                                    texto += f.GetValue(item) + "|";
                                }
                                else
                                {
                                    texto += "" + "|";
                                }
                            }
                            else
                            {
                                texto += f.GetValue(item) + "|";
                            }
                        }
                        else
                        {
                            texto += "" + "|";
                        }
                    }
                    System.IO.StreamWriter sw = new System.IO.StreamWriter(Server.MapPath("") + fic, true);
                    sw.WriteLine(texto);
                    sw.Close();
                }
                System.IO.StreamReader sr;
                sr = File.OpenText(Server.MapPath("") + fic);
                string texo = sr.ReadToEnd();
                sr.Close();
                HttpContext.Current.Response.ClearContent();
                HttpContext.Current.Response.ClearHeaders();
                HttpContext.Current.Response.ContentType = "text/plain";
                HttpContext.Current.Response.Write(texo);
                HttpContext.Current.Response.AppendHeader("Content-disposition", "attachment;filename=" + fic);
                HttpContext.Current.Response.Flush();
                File.Delete(Server.MapPath("") + fic);
                HttpContext.Current.Response.End();
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch
        {
        }
    }


    private int TipoConvenioTarjeta()
    {
        return 1;
    }

    private string NomTipoConvenioTarjeta()
    {
        return "";
    }



}