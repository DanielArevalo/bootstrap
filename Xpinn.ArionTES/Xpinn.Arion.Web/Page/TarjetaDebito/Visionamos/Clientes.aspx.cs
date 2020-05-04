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
using Xpinn.Util;
using Xpinn.TarjetaDebito.Entities;
using Xpinn.TarjetaDebito.Services;
using System.Linq;
using System.Text.RegularExpressions;
using Xpinn.Comun.Entities;
using Newtonsoft.Json;

partial class Lista : GlobalWeb
{
    CuentaService CuentaService = new CuentaService();
    InterfazCOOPCENTRAL interfazCoopcentral;
    string convenio = "";
    int? _tipo_convenio = 1;

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(CuentaService.CodigoProgramaClientesCoopcentral, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoExportar += btnExportar_Click;
            toolBar.eventoGuardar += btnGuardar_Click;
            ctlMensaje.eventoClick += btnContinuarMen_Click;
            interfazCoopcentral = new InterfazCOOPCENTRAL("0123456789ABCDEFFEDCBA9876543210", "00000000000000000000000000000000");
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CuentaService.CodigoProgramaClientesCoopcentral, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            // Cargar el dato del convenio
            convenio = ConvenioTarjeta(Convert.ToInt32(_tipo_convenio));
            txtConvenio.Text = convenio;
            txtIpAppliance.Text = IpApplianceConvenioTarjeta();
            txtTipoConvenio.Text = "COOPCENTRAL";
            if (!IsPostBack)
            {
                rbTipoArchivo.SelectedIndex = 1;
                // Cargar listas desplegables
                cargarDropdown();
                Actualizar();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CuentaService.CodigoProgramaClientesCoopcentral, "Page_Load", ex);
        }
    }


    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        Page.Validate();
        if (Page.IsValid)
        {
            Actualizar();
        }
    }


    protected void cargarDropdown()
    {
        TarjetaService tarjetaServicio = new TarjetaService();
        ddloficina.DataSource = tarjetaServicio.ListarOficina(new Tarjeta(), (Usuario)Session["Usuario"]);
        ddloficina.DataTextField = "oficina";
        ddloficina.DataValueField = "cod_oficina";
        ddloficina.DataBind();
        ddloficina.Items.Insert(0, new ListItem("Selecione un item", "0"));
        ddloficina.SelectedIndex = 0;

        ddlTipoCuenta.Items.Insert(0, new ListItem("Seleccione un item", "0"));
        ddlTipoCuenta.Items.Insert(1, new ListItem("Ahorros", "A"));
        ddlTipoCuenta.Items.Insert(2, new ListItem("Credito Rotativo", "C"));
        ddlTipoCuenta.SelectedIndex = 0;
        ddlTipoCuenta.DataBind();
    }



    private void Actualizar()
    {
        try
        {
            gvListaCoopcentral.Visible = true;

            List<CuentaCoopcentral> lstConsulta = new List<CuentaCoopcentral>();
            CuentaService cuentaservice = new CuentaService();
            lstConsulta = CuentaService.ListarCuentaCoopcentral(ObtenerValores(), (Usuario)Session["usuario"]);
            gvListaCoopcentral.PageSize = 15;
            String emptyQuery = "Fila de datos vacia";
            gvListaCoopcentral.EmptyDataText = emptyQuery;
            if (lstConsulta.Count > 0)
            {
                gvListaCoopcentral.DataSource = lstConsulta;
                gvListaCoopcentral.DataBind();
                panelGrilla.Visible = true;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
            }
            else
            {
                panelGrilla.Visible = false;
                lblTotalRegs.Visible = false;
            }
            Session["DTCUENTAS"] = lstConsulta;
            Session.Add(CuentaService.CodigoProgramaClientesCoopcentral + ".consulta", 1);
                            
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CuentaService.CodigoProgramaClientesCoopcentral, "Actualizar", ex);
        }
    }

    private Cuenta ObtenerValores()
    {
        Cuenta entitytarjeta = new Cuenta();

        if (!string.IsNullOrEmpty(ddloficina.SelectedValue))
            entitytarjeta.cod_oficina = Convert.ToInt32(ddloficina.SelectedValue);

        if (!string.IsNullOrEmpty(ddlTipoCuenta.SelectedValue))
            entitytarjeta.tipocuenta = ddlTipoCuenta.SelectedValue;

        if (!string.IsNullOrEmpty(txtNumCuenta.Text.Trim()))
            entitytarjeta.numero_cuenta = Convert.ToString(txtNumCuenta.Text.Trim());

        if (!string.IsNullOrEmpty(txtFechaApertura.Text.Trim()))
            entitytarjeta.fechaapertura = Convert.ToDateTime(txtFechaApertura.Text.Trim());

        return entitytarjeta;
    }

    protected void btnExportar_Click(object sender, EventArgs e)
    {
        try
        {
            if ((gvListaCoopcentral.Rows.Count > 0) && Session["DTCUENTAS"] != null)
            {
                if (rbTipoArchivo.SelectedValue == "1")
                {
                    GenerarArchivo(false);
                }
                else
                {
                    StringBuilder sb = new StringBuilder();
                    StringWriter sw = new StringWriter(sb);
                    HtmlTextWriter htw = new HtmlTextWriter(sw);
                    Page pagina = new Page();
                    dynamic form = new HtmlForm();
                    gvListaCoopcentral.AllowPaging = false;
                    gvListaCoopcentral.DataSource = Session["DTCUENTAS"];
                    gvListaCoopcentral.DataBind();
                    gvListaCoopcentral.EnableViewState = false;
                    pagina.EnableEventValidation = false;
                    pagina.DesignerInitialize();
                    pagina.Controls.Add(form);
                    form.Controls.Add(gvListaCoopcentral);
                    pagina.RenderControl(htw);
                    Response.Clear();
                    Response.Buffer = true;
                    Response.ContentType = "application/vnd.ms-excel";
                    Response.AddHeader("Content-Disposition", "attachment;filename=CuentasCoopcentral.xls");
                    Response.Charset = "UTF-8";
                    Response.ContentEncoding = Encoding.Default;
                    Response.Write(sb.ToString());
                    Response.End();
                    gvListaCoopcentral.AllowPaging = true;
                    gvListaCoopcentral.DataBind();
                }
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

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        VerError("");
        txtRespuesta.Text = ""; 
        try
        {
            ctlMensaje.MostrarMensaje("Desea actualizar datos en COOPCENTRAL?");
        }
        catch
        {
        }
    }

    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        GenerarArchivo(true);
    }

    private void GenerarArchivo(bool pActualizar)
    {
        if (Session["DTCUENTAS"] == null)
        {
            VerError("No se ha generado el listado de cuentas para poder actualizar");
            return;
        }
        txtRespuesta.Text = "";
        lblError.Text = "";
        try
        {
            // Inicializar la interfaz
            string s_usuario_applicance = "webservice";
            string s_clave_appliance = "WW.EE.99";
            SeguridadConvenioTarjeta(ref s_usuario_applicance, ref s_clave_appliance);
            interfazCoopcentral.ConfiguracionAppliance(IpApplianceConvenioTarjeta(), s_usuario_applicance, s_clave_appliance);
            string error = "";
            string proceso = "";
            txtRespuesta.Text = "";
            lblError.Text = "";
            // Generar archivo
            try
            {
                Usuario _usuario = (Usuario)Session["Usuario"];
                // Crear el archivo con los datos de las cuentas
                string ruta = "", archivo = "";
                ruta = Server.MapPath(ruta);
                archivo = convenio + DateTime.Now.ToString("ddMMyyyy") + ".cls";
                string rutayarchivo = ruta + "\\" + archivo; 
                System.IO.StreamWriter newfile = new StreamWriter(rutayarchivo, true, Encoding.UTF8);
                string separador = ",";
                List<CuentaCoopcentral> lstConsulta = (List<CuentaCoopcentral>)Session["DTCUENTAS"];
                newfile = CuentaService.GenerarArchivoClientesCoopcentral(lstConsulta, separador, newfile, _usuario);
                newfile.Close();
                // Verificar que el archivo se creeo correctamente
                System.IO.StreamReader file = new System.IO.StreamReader(rutayarchivo);
                if (file == null)
                {
                    lblError.Text = "No se pudo leer el archivo";
                    return;
                }
                // Enviando el archivo por el WEBServices
                int numeroTarjetas = 0;
                if (pActualizar)
                {
                    RespuestaCoopcentral respuestaCoopcentral = null;
                    try
                    { 
                        interfazCoopcentral.ServicioCLIENTESCOOPCENTRAL(convenio, archivo, file, ref respuestaCoopcentral, ref error);

                        // Verificamos que se halla podido transformar la respuesta del servicio a la entidad respectiva y que halla cuentas que revisar
                        if (respuestaCoopcentral != null)
                        {
                            proceso = respuestaCoopcentral.respuesta;
                        }
                        else
                        {
                            VerError("No hay datos de tarjetas de respuesta del WebServices de Coopcentral");
                        }
                    }                    
                    catch (Exception ex)
                    {
                        lblError.Text = "Error=>" + ex.Message + " " + error;
                    }
                }
                else
                {
                    // Descargar el archivo
                    string texto = "";
                    System.IO.StreamReader sr;
                    sr = File.OpenText(rutayarchivo);
                    texto = sr.ReadToEnd();
                    sr.Close();
                    Response.ClearContent();
                    Response.ClearHeaders();
                    Response.ContentType = "text/plain";
                    Response.Write(texto);
                    Response.AppendHeader("Content-disposition", "attachment;filename=" + archivo);
                    Response.Flush();
                    try { HttpContext.Current.Response.End(); } catch { }
                }
                file.Close();
                txtRespuesta.Text = proceso;
                lblError.Text = error + " Num.Tarjetas:" + numeroTarjetas;
            }
            catch (Exception ex)
            {
                lblError.Text = "Error->" + ex.Message + " " + error;
            }
        }
        catch (Exception ex)
        {
            //BOexcepcion.Throw(CuentaService.CodigoProgramaClientesCoopcentral, "btnContinuarMen_Click", ex);
            lblError.Text = "Error->" + ex.Message;
        }
    }

    private string EsNulo(string pDato, string pDefault)
    {
        if (pDato == null || pDato == "")
            return pDefault;
        var arra = pDato.Split(' ');
        var strs = "";
        var str = "";
        for (int i = 0; i < arra.Length; i++)
        {
            if (arra[i] == "" || arra[i] == " ") continue;
            strs = arra[i].Replace(" ", "");
            strs = strs.Replace(".", "");
            strs = strs.Replace(",", "");
            strs = strs.Replace("/", "");
            strs = strs.Replace("-", " ");
            strs = strs.Replace("#", "");
            strs = strs.Replace("$", "");
            strs = strs.Replace("Á", "A");
            strs = strs.Replace("É", "E");
            strs = strs.Replace("Í", "I");
            strs = strs.Replace("Ó", "O");
            strs = strs.Replace("Ú", "U");
            strs = strs.Replace("á", "a");
            strs = strs.Replace("é", "e");
            strs = strs.Replace("í", "i");
            strs = strs.Replace("ó", "o");
            strs = strs.Replace("ú", "u");
            strs = strs.Replace("À", "A");
            strs = strs.Replace("È", "E");
            strs = strs.Replace("Ì", "I");
            strs = strs.Replace("Ò", "O");
            strs = strs.Replace("Ù", "U");
            strs = strs.Replace("à", "a");
            strs = strs.Replace("è", "e");
            strs = strs.Replace("ì", "i");
            strs = strs.Replace("ò", "o");
            strs = strs.Replace("ù", "u");
            strs = strs.Replace(";", " ");
            if (arra.Length > 1)
            {
                str += strs + " ";
            }
            else
            {
                str += strs;
            }

        }

        return str;
    }

    private string VerificarCampo(string pcampo)
    {
        return CuentaService.VerificarCampo(pcampo);
    }

    private string VerificarTexto(string pcampo, string pseparador)
    {
        return CuentaService.VerificarTexto(pcampo, pseparador);
    }



}