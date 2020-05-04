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
    InterfazENPACTO interfazEnpacto;
    string convenio = "";
    int _tipo_convenio = 0;

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(CuentaService.CodigoPrograma, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoExportar += btnExportar_Click;
            toolBar.eventoGuardar += btnGuardar_Click;
            ctlMensaje.eventoClick += btnContinuarMen_Click;
            interfazEnpacto = new InterfazENPACTO("0123456789ABCDEFFEDCBA9876543210", "00000000000000000000000000000000");
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CuentaService.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            // Cargar el dato del convenio
            convenio = ConvenioTarjeta(_tipo_convenio);
            txtConvenio.Text = convenio;
            txtIpAppliance.Text = IpApplianceConvenioTarjeta();            
            gvLista.Visible = true;
            txtTipoConvenio.Text = "BANCO DE BOGOTA";
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
            BOexcepcion.Throw(CuentaService.CodigoPrograma, "Page_Load", ex);
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


    protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvLista.PageIndex = e.NewPageIndex;
            Actualizar();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CuentaService.CodigoPrograma, "gvLista_PageIndexChanging", ex);
        }
    }


    private void Actualizar()
    {
        try
        {
            gvLista.Visible = true;

            List<Cuenta> lstConsulta = new List<Cuenta>();
            List<Cuenta> lsttarjeta = new List<Cuenta>();
            List<Tarjeta> tajeta_verifi = new List<Tarjeta>();
            CuentaService cuentaservice = new CuentaService();
            General pGeneral = new General();
            pGeneral = ConsultarParametroGeneral(107, (Usuario)Session["usuario"]);

            lstConsulta = CuentaService.ListarCuenta(ObtenerValores(), pGeneral.valor, (Usuario)Session["usuario"]);
            if (lstConsulta.Count > 0)
            {
                if (ddlEstadoCuenta.SelectedValue != "")
                {
                    lstConsulta = lstConsulta.Where(x => x.estado == ddlEstadoCuenta.SelectedValue).ToList();
                }
                foreach (Cuenta entidad in lstConsulta)
                {
                    Tarjeta enviotarje = new Tarjeta();
                    enviotarje.numero_cuenta = entidad.nrocuenta;
                    tajeta_verifi = cuentaservice.ListarTarjetas(enviotarje, (Usuario)Session["usuario"]);
                    if (tajeta_verifi.Count > 0)
                    {
                        if (tajeta_verifi[0].estado_saldo == 1)
                        {
                            entidad.saldodisponible = 0;
                        }
                    }
                    bool bexiste = false;
                    var lstverifica = from g in lsttarjeta where g.cod_persona == entidad.cod_persona && g.nrocuenta == entidad.nrocuenta select g;
                    if (lstverifica.ToList().Count > 0)
                        bexiste = true;
                    if (!bexiste)
                        lsttarjeta.Add(entidad);
                }

                lstConsulta = lsttarjeta;

                gvLista.PageSize = 15;
                String emptyQuery = "Fila de datos vacia";
                gvLista.EmptyDataText = emptyQuery;
                if (lstConsulta.Count > 0)
                {
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
                Session["DTCUENTAS"] = lstConsulta;
                Session.Add(CuentaService.CodigoPrograma + ".consulta", 1);
            }                            
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CuentaService.CodigoPrograma, "Actualizar", ex);
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
            if (gvLista.Rows.Count > 0 && Session["DTCUENTAS"] != null)
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
                    gvLista.AllowPaging = false;
                    gvLista.DataSource = Session["DTCUENTAS"];
                    gvLista.DataBind();
                    gvLista.EnableViewState = false;
                    pagina.EnableEventValidation = false;
                    pagina.DesignerInitialize();
                    pagina.Controls.Add(form);
                    form.Controls.Add(gvLista);
                    pagina.RenderControl(htw);
                    Response.Clear();
                    Response.Buffer = true;
                    Response.ContentType = "application/vnd.ms-excel";
                    Response.AddHeader("Content-Disposition", "attachment;filename=Cuentas.xls");
                    Response.Charset = "UTF-8";
                    Response.ContentEncoding = Encoding.Default;
                    Response.Write(sb.ToString());
                    Response.End();
                    gvLista.AllowPaging = true;
                    gvLista.DataBind();
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
            ctlMensaje.MostrarMensaje("Desea actualizar datos en ENPACTO-APPLIANCE?");
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
            interfazEnpacto.ConfiguracionAppliance(IpApplianceConvenioTarjeta(), s_usuario_applicance, s_clave_appliance);
            string error = "";
            string proceso = "";
            txtRespuesta.Text = "";
            lblError.Text = "";
            try
            {
                // Crear el archivo con los datos de las cuentas
                string ruta = "";
                ruta = Server.MapPath(ruta);
                string archivo = convenio + DateTime.Now.ToString("ddMMyyyy") + ".cls";
                string rutayarchivo = ruta + "\\" + archivo;
                System.IO.StreamWriter newfile = new StreamWriter(rutayarchivo);
                string separador = ";";
                List<Cuenta> lstConsulta = (List<Cuenta>)Session["DTCUENTAS"];
                foreach (Cuenta entidad in lstConsulta)
                {
                    decimal saldoTotal = 0;
                    if (entidad.saldototal < 0)
                        saldoTotal = 0;
                    else
                        saldoTotal = entidad.saldototal;
                    string linea = "";
                    linea = entidad.identificacion + separador + EsNulo(entidad.nombres, "").Trim() + separador + EsNulo(entidad.direccion, "").Trim() + separador + EsNulo(entidad.telefono, "").Trim() + separador +
                               entidad.email + separador + EsNulo(entidad.tipocuenta, "") + separador + EsNulo(entidad.nrocuenta, "").Trim() + separador + Math.Round(entidad.saldodisponible) + separador +
                               Math.Round(saldoTotal) + separador + EsNulo(entidad.fechasaldo.ToString("dd/MM/yyyy"), "");
                    newfile.WriteLine(linea);
                }
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
                    RespuestaEnpactoClientes respuestaEnpacto = null;
                    try
                    {
                        interfazEnpacto.ServicioCLIENTESENPACTO(convenio, archivo, rutayarchivo, ref proceso, ref error, ref respuestaEnpacto);

                        // Verificamos que se halla podido transformar la respuesta del servicio a la entidad respectiva y que halla cuentas que revisar
                        if (respuestaEnpacto != null && respuestaEnpacto.relaciones != null && respuestaEnpacto.relaciones.Count > 0)
                        {
                            foreach (RelacionClienteEnpacto relacion in respuestaEnpacto.relaciones)
                            {
                                numeroTarjetas += 1;
                                if (relacion.tarjeta != null && relacion.cuenta != null)
                                {
                                    // Pasamos la info a una entidad del sistema
                                    Tarjeta tarjeta = new Tarjeta
                                    {
                                        numtarjeta = relacion.tarjeta,
                                        numero_cuenta = relacion.cuenta
                                    };

                                    // Verificamos si la tarjeta existe en nuestro sistema
                                    bool existe = CuentaService.VerificarSiTarjetaExiste(tarjeta, Usuario);

                                    // Si no existe la creamos, este SP crea la tarjeta segun la informacion de la cuenta asociada
                                    // Consultara si es un Ahorro o un Credito y creara la tarjeta segun sea el caso
                                    if (!existe)
                                    {
                                        tarjeta = CuentaService.CrearTarjeta(tarjeta, Usuario);
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (respuestaEnpacto != null)
                                if (respuestaEnpacto.relaciones != null)
                                    VerError("No hay tarjetas para actualizar." + respuestaEnpacto.relaciones.Count());
                                else
                                    VerError("No hay relación de tarjetas de respuesta");
                            else
                                VerError("No hay datos de tarjetas de respuesta del WebServices de Enpacto");
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
            //BOexcepcion.Throw(CuentaService.CodigoPrograma, "btnContinuarMen_Click", ex);
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
        return pcampo == null ? "": pcampo.Replace(",", " ");
    }

    private string VerificarTexto(string pcampo, string pseparador)
    {
        if (pcampo == null)
            return null;
        pcampo = pcampo.Replace("á", "a");
        pcampo = pcampo.Replace("é", "e");
        pcampo = pcampo.Replace("í", "i");
        pcampo = pcampo.Replace("ó", "o");
        pcampo = pcampo.Replace("ú", "u");
        pcampo = pcampo.Replace("ñ", "n");
        pcampo = pcampo.Replace("Á", "A");
        pcampo = pcampo.Replace("É", "E");
        pcampo = pcampo.Replace("Í", "I");
        pcampo = pcampo.Replace("Ó", "O");
        pcampo = pcampo.Replace("Ú", "U");
        pcampo = pcampo.Replace("Ñ", "N");
        return pcampo;
    }



}