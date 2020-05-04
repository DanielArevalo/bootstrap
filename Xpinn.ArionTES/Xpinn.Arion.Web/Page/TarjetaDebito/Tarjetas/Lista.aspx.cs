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

partial class Lista : GlobalWeb
{    
    CuentaService CuentaService = new CuentaService();
    InterfazENPACTO interfazEnpacto;
    string convenio = "";

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(CuentaService.CodigoProgramaTarjeta, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoExportar += btnExportar_Click;
            toolBar.eventoGuardar += btnGuardar_Click;
            ctlMensaje.eventoClick += btnContinuarMen_Click;
            interfazEnpacto = new InterfazENPACTO("0123456789ABCDEFFEDCBA9876543210", "00000000000000000000000000000000");
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CuentaService.CodigoProgramaTarjeta, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            // Cargar el dato del convenio
            convenio = ConvenioTarjeta();
            txtConvenio.Text = convenio;
            if (!IsPostBack)
            {                
                // Cargar listas desplegables
                cargarDropdown();
                Actualizar();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CuentaService.CodigoProgramaTarjeta, "Page_Load", ex);
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
        ddlTipoCuenta.Items.Insert(1, new ListItem("Ahorros", "1"));
        ddlTipoCuenta.Items.Insert(2, new ListItem("Credito Rotativo", "2"));
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
            BOexcepcion.Throw(CuentaService.CodigoProgramaTarjeta, "gvLista_PageIndexChanging", ex);
        }
    }

   
    private void Actualizar()
    {
        try
        {
            List<Tarjeta> lstConsulta = new List<Tarjeta>();
            lstConsulta = CuentaService.ListarTarjetas(ObtenerValores(), (Usuario)Session["usuario"]);
            if (ddlEstadoTarjeta.SelectedValue != "")
            {
                lstConsulta = lstConsulta.Where(x => x.estado == ddlEstadoTarjeta.SelectedValue).ToList();
            }           

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
            Session["DTTARJETAS"] = lstConsulta;
            Session.Add(CuentaService.CodigoProgramaTarjeta + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CuentaService.CodigoProgramaTarjeta, "Actualizar", ex);
        }
    }
             
    private Tarjeta ObtenerValores()
    {
        Tarjeta entitytarjeta = new Tarjeta();

        if (!string.IsNullOrEmpty(ddloficina.SelectedValue))
            entitytarjeta.cod_oficina = Convert.ToInt32(ddloficina.SelectedValue);

        if (!string.IsNullOrEmpty(ddlTipoCuenta.SelectedValue))
            entitytarjeta.tipo_cuenta = ddlTipoCuenta.SelectedValue;

        if (!string.IsNullOrEmpty(txtNumCuenta.Text.Trim()))
            entitytarjeta.numero_cuenta = Convert.ToString(txtNumCuenta.Text.Trim());

        if (!string.IsNullOrEmpty(txtFechaApertura.Text.Trim()))
            entitytarjeta.fecha_asignacion = Convert.ToDateTime(txtFechaApertura.Text.Trim());

        return entitytarjeta;
    }

    protected void btnExportar_Click(object sender, EventArgs e)
    {
        try
        {
            if (gvLista.Rows.Count > 0 && Session["DTTARJETAS"] != null)
            {
                StringBuilder sb = new StringBuilder();
                StringWriter sw = new StringWriter(sb);
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                Page pagina = new Page();
                dynamic form = new HtmlForm();
                gvLista.AllowPaging = false;
                gvLista.DataSource = Session["DTTARJETAS"];
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
                Response.AddHeader("Content-Disposition", "attachment;filename=Tarjetas.xls");
                Response.Charset = "UTF-8";
                Response.ContentEncoding = Encoding.Default;
                Response.Write(sb.ToString());
                Response.End();
                gvLista.AllowPaging = true;
                gvLista.DataBind();
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
        try
        {
            ctlMensaje.MostrarMensaje("Desea generar archivo de ENPACTO (CLIENTES.MOV)?");
        }
        catch
        {
        }
    }

   protected void btnContinuarMen_Click(object sender, EventArgs e)
   {
       if (Session["DTTARJETAS"] == null)
       {
           VerError("No se ha generado el listado de tarjetas para poder actualizar");
           return;
       }
       txtRespuesta.Text = "";
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
                string ruta = "Archivo\\";
                ruta = Server.MapPath(ruta);
                string archivo = "CLIENTES.MOV";
                string rutayarchivo = ruta + "\\" + archivo;
                System.IO.StreamWriter newfile = new StreamWriter(rutayarchivo);
                string separador = ",";
                List<Tarjeta> lstConsulta = (List<Tarjeta>)Session["DTTARJETAS"];
                decimal? sumaSaldoTotal = 0;
                decimal? sumaSaldoDisponible = 0;
                foreach (Tarjeta entidad in lstConsulta)
                {
                    string linea = "";
                    string nrocuenta = entidad.numero_cuenta;
                    if (entidad.tipo_cuenta == "1")
                        nrocuenta = "A" + convenio + entidad.numero_cuenta;
                    else
                        nrocuenta = "C" + convenio + entidad.numero_cuenta;
                    sumaSaldoTotal += entidad.saldo_total;
                    sumaSaldoDisponible += entidad.saldo_disponible;
                    linea = "0" + entidad.estado + separador + entidad.identificacion + separador + entidad.nombres + separador + entidad.numtarjeta + separador +
                                nrocuenta + separador + entidad.saldo_total + separador + entidad.saldo_disponible + separador + entidad.cupo_cajero + separador +
                                entidad.transacciones_cajero + separador + entidad.cupo_datafono + separador + entidad.transacciones_datafono + separador + "0";
                    newfile.WriteLine(linea);
                }                
                newfile.WriteLine("FOOTER" + separador + sumaSaldoTotal + separador + sumaSaldoDisponible);
                newfile.Close();
                // Verificar que el archivo se creeo correctamente
                System.IO.StreamReader file = new System.IO.StreamReader(rutayarchivo);
                if (file == null)
                {
                    lblError.Text = "No se pudo leer el archivo";
                    return;
                }
                // Enviando el archivo por el WEBServices
                // interfazEnpacto.ServicioCLIENTESENPACTO(convenio, archivo, rutayarchivo, ref proceso, ref error);
                file.Close();
                // Descargar el archivo
                string texto = "";
                System.IO.StreamReader sr;
                sr = File.OpenText(Server.MapPath("Archivo\\" + archivo));
                texto = sr.ReadToEnd();
                sr.Close();
                Response.ClearContent();
                Response.ClearHeaders();
                Response.ContentType = "text/plain";
                Response.Write(texto);
                Response.AppendHeader("Content-disposition", "attachment;filename=" + archivo);
                Response.Flush();
                //File.Delete(Server.MapPath(rutayarchivo));
                Response.End();
                txtRespuesta.Text = proceso;
                lblError.Text = error;
           }
           catch (Exception ex)
           {
               lblError.Text = "No se pudo leer el archivo. Error:" + ex.Message + " " + error;
           }
       }
       catch (Exception ex)
       {
           BOexcepcion.Throw(CuentaService.CodigoProgramaTarjeta, "btnContinuarMen_Click", ex);
       }
   }

}