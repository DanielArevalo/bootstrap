using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using Xpinn.Contabilidad.Entities;
using Xpinn.Contabilidad.Services;
using System.Text;
using System.IO;
using System.Web.UI.HtmlControls;
using Xpinn.Aportes.Services;
using Xpinn.Aportes.Entities;
using Xpinn.FabricaCreditos.Services;
using Xpinn.FabricaCreditos.Entities;

partial class Detalle : GlobalWeb
{
    private Xpinn.Contabilidad.Services.TerceroService TerceroServicio = new Xpinn.Contabilidad.Services.TerceroService();
    private Xpinn.Aportes.Services.AfiliacionServices AfiliacionServicio = new Xpinn.Aportes.Services.AfiliacionServices();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[AfiliacionServicio.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(AfiliacionServicio.CodigoPrograma, "E");
            else
                VisualizarOpciones(AfiliacionServicio.CodigoPrograma, "A");

            Site toolBar = (Site)this.Master;
            //toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
            toolBar.eventoImprimir += btnImprimir_Click;
            ctlFormatos.eventoClick += btnImpresion_Click;
            toolBar.MostrarGuardar(false);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AfiliacionServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                PanelTipoComprobante.Enabled = false;
                pConsulta.Enabled = false;
                obtenerControlesAdicionales();
                txtCodigo.Enabled = false;
                CargarListas();
                rbJuridica.Checked = true;
                rbNatural.Checked = false;
                if (Session[AfiliacionServicio.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[AfiliacionServicio.CodigoPrograma + ".id"].ToString();
                    Session.Remove(AfiliacionServicio.CodigoPrograma + ".id");
                    ObtenerDatos(idObjeto);
                    mvDatos.ActiveViewIndex = 1;
                }
                else
                {
                    txtCodigo.Text = TerceroServicio.ObtenerSiguienteCodigo((Usuario)Session["Usuario"]).ToString();
                }
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AfiliacionServicio.CodigoPrograma, "Page_Load", ex);
        }
    }


    private void obtenerControlesAdicionales()
    {
        InformacionAdicionalServices informacion = new InformacionAdicionalServices();
        InformacionAdicional pInfo = new InformacionAdicional();
        List<InformacionAdicional> lstControles = new List<InformacionAdicional>();
        string tipo = "J";
        lstControles = informacion.ListarInformacionAdicional(pInfo,tipo, (Usuario)Session["usuario"]);
        if (lstControles.Count > 0)
        {
            gvInfoAdicional.DataSource = lstControles;
            gvInfoAdicional.DataBind();
            
            if (idObjeto != "")
            {
                //DataTable dt = new DataTable();
                //dt.Columns.Add("ID");
                //dt.Columns.Add("cod_control");
                //dt.Columns.Add("Descripcion");
                //dt.Columns.Add("item");

                //foreach(GridViewRow rFila in gvInfoAdicional.Rows)
                //{
                //    DataRow fila = dt.NewRow();
                //    fila[0] = rFila.FindControl("lblidinfadicional");
                //}
            }
        }
    }

    protected List<InformacionAdicional> ObtenerListaInformacionAdicional()
    {
        List<InformacionAdicional> lstInformacionAdd = new List<InformacionAdicional>();

        foreach (GridViewRow rfila in gvInfoAdicional.Rows)
        {
            InformacionAdicional eActi = new InformacionAdicional();

            if (idObjeto != "")
            {
                Label lblidinfadicional = (Label)rfila.FindControl("lblidinfadicional");
                if (lblidinfadicional != null)
                    eActi.idinfadicional = Convert.ToInt32(lblidinfadicional.Text);
            }
            else
                eActi.idinfadicional = 0;
            Label lblcod_infadicional = (Label)rfila.FindControl("lblcod_infadicional");
            if (lblcod_infadicional != null)
                eActi.cod_infadicional = Convert.ToInt32(lblcod_infadicional.Text);

            Label lblopcionaActivar = (Label)rfila.FindControl("lblopcionaActivar");

            if (lblopcionaActivar != null)
            {
                if (lblopcionaActivar.Text == "1")//CARACTER
                {
                    TextBox txtCadena = (TextBox)rfila.FindControl("txtCadena");
                    if (txtCadena != null)
                        eActi.valor = txtCadena.Text;
                }
                else if (lblopcionaActivar.Text == "2")//FECHA
                {
                    fecha txtctlfecha = (fecha)rfila.FindControl("txtctlfecha");
                    if (txtctlfecha != null)
                        eActi.valor = txtctlfecha.Text;
                }
                else if (lblopcionaActivar.Text == "3") //NUMERO
                {
                    TextBox txtNumero = (TextBox)rfila.FindControl("txtNumero");
                    if (txtNumero != null)
                        eActi.valor = txtNumero.Text;
                }
                else if (lblopcionaActivar.Text == "4") // DROPDOWNLIST
                {
                    DropDownListGrid ddlDropdown = (DropDownListGrid)rfila.FindControl("ddlDropdown");
                    if (ddlDropdown != null)
                        eActi.valor = ddlDropdown.SelectedItem.Text;
                    if (ddlDropdown.Text != "")
                        eActi.valor = ddlDropdown.SelectedItem.Text;
                }
            }

            if (eActi.cod_infadicional != 0)
            {
                lstInformacionAdd.Add(eActi);
            }
        }
        return lstInformacionAdd;
    }



    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Session[AfiliacionServicio.CodigoPrograma + ".id"] = idObjeto;
        Navegar(Pagina.Lista);        
    }

    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            Xpinn.Contabilidad.Entities.Tercero vTercero = new Xpinn.Contabilidad.Entities.Tercero();
            vTercero = TerceroServicio.ConsultarTercero(Convert.ToInt64(pIdObjeto), null, (Usuario)Session["usuario"]);

            txtCodigo.Text = HttpUtility.HtmlDecode(vTercero.cod_persona.ToString().Trim());
            if (!string.IsNullOrEmpty(vTercero.identificacion))
                txtIdentificacion.Text = HttpUtility.HtmlDecode(vTercero.identificacion.ToString().Trim());
            if (vTercero.digito_verificacion.ToString() != "")
                txtDigitoVerificacion.Text = HttpUtility.HtmlDecode(vTercero.digito_verificacion.ToString().Trim());
            if (!string.IsNullOrEmpty(vTercero.primer_apellido))
                txtSigla.Text = HttpUtility.HtmlDecode(vTercero.primer_apellido.ToString().Trim());
            if (!string.IsNullOrEmpty(vTercero.razon_social))
                txtRazonSocial.Text = HttpUtility.HtmlDecode(vTercero.razon_social.ToString().Trim());
            if (!string.IsNullOrEmpty(vTercero.direccion))
                txtDireccion.Text = HttpUtility.HtmlDecode(vTercero.direccion.ToString().Trim());
            if (vTercero.codciudadexpedicion.ToString() != "")
                ddlCiudad.SelectedValue = HttpUtility.HtmlDecode(vTercero.codciudadexpedicion.ToString().Trim());
            if (!string.IsNullOrEmpty(vTercero.telefono))       
                txtTelefono.Text = HttpUtility.HtmlDecode(vTercero.telefono.ToString().Trim());
            if (!string.IsNullOrEmpty(vTercero.email))
                txtEmail.Text = HttpUtility.HtmlDecode(vTercero.email.ToString().Trim());
            if (vTercero.fechaexpedicion != null)
                txtFecha.ToDateTime = Convert.ToDateTime(HttpUtility.HtmlDecode(vTercero.fechaexpedicion.ToString()));           
            try
            {
                if (vTercero.codactividadStr.ToString() != "")
                    ddlActividad.SelectedValue = HttpUtility.HtmlDecode(vTercero.codactividadStr.ToString().Trim());
            }
            catch { ddlActividad.SelectedValue = ""; }
            if (!string.IsNullOrEmpty(vTercero.regimen))
                ddlRegimen.SelectedValue = HttpUtility.HtmlDecode(vTercero.regimen.ToString().Trim());
            
            if (vTercero.tipo_acto_creacion != 0)
                ddlTipoActoCrea.SelectedValue = vTercero.tipo_acto_creacion.ToString();

            if (vTercero.num_acto_creacion != "")
                txtNumActoCrea.Text = vTercero.num_acto_creacion;

            if (vTercero.celular != "")
                txtcelular.Text = vTercero.celular;

            //RECUPERAR INFORMACION ADICIONAL
            InformacionAdicionalServices infoService = new InformacionAdicionalServices();
            List<InformacionAdicional> LstInformacion = new List<InformacionAdicional>();

            LstInformacion = infoService.ListarPersonaInformacion(Convert.ToInt64(pIdObjeto), "J", (Usuario)Session["usuario"]);
            if (LstInformacion.Count > 0)
            {
                gvInfoAdicional.DataSource = LstInformacion;
                gvInfoAdicional.DataBind();
            }


            #region RECUPERAR DATOS DE AFILIACIóN

            Afiliacion pAfili = new Afiliacion();
            pAfili = AfiliacionServicio.ConsultarAfiliacion(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);
            if (pAfili.idafiliacion != 0)
                txtcodAfiliacion.Text = Convert.ToString(pAfili.idafiliacion);
            if (pAfili.fecha_afiliacion != DateTime.MinValue)
                txtFechaAfili.Text = Convert.ToString(pAfili.fecha_afiliacion.ToString(gFormatoFecha));
            if (pAfili.estado != "")
                ddlEstadoAfi.SelectedValue = pAfili.estado;

            ddlEstadoAfi_SelectedIndexChanged(ddlEstadoAfi, null);
            if (pAfili.fecha_retiro != DateTime.MinValue)
                txtFechaRetiro.Text = Convert.ToString(pAfili.fecha_retiro.ToString(gFormatoFecha));
            if (pAfili.valor != 0)
                txtValorAfili.Text = Convert.ToString(pAfili.valor);
            if (pAfili.fecha_primer_pago != DateTime.MinValue && pAfili.fecha_primer_pago != null)
                txtFecha1Pago.Text = Convert.ToString(pAfili.fecha_primer_pago.Value.ToString(gFormatoFecha));
            if (pAfili.cuotas != 0)
                txtCuotasAfili.Text = Convert.ToString(pAfili.cuotas);
            if (pAfili.cod_periodicidad != 0)
                ddlPeriodicidad.SelectedValue = Convert.ToString(pAfili.cod_periodicidad);

            if (pAfili.forma_pago != 0)
                ddlFormaPago.SelectedValue = pAfili.forma_pago.ToString();

            if (pAfili.empresa_formapago != 0 && pAfili.empresa_formapago != null)
                ddlEmpresa.SelectedValue = pAfili.empresa_formapago.ToString();

            ddlFormaPago_SelectedIndexChanged(ddlFormaPago, null);

            #endregion


            txtDigitoVerificacion.Enabled = false;
            txtIdentificacion.Enabled = false;
            Site toolBar = (Site)this.Master;
            toolBar.MostrarGuardar(true);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AfiliacionServicio.CodigoPrograma, "ObtenerDatos", ex);
        }
    }

    private void CargarListas()
    {
        try
        {
            PoblarLista("Tipo_Acto_Creacion", ddlTipoActoCrea);
            
            // Llenar las listas que tienen que ver con ciudades
            ddlCiudad.DataTextField = "ListaDescripcion";
            ddlCiudad.DataValueField = "ListaId";
            ddlCiudad.DataSource = TraerResultadosLista("Ciudades");
            ddlCiudad.DataBind();

            // Llenar la actividad
            ddlActividad.DataTextField = "ListaDescripcion";
            ddlActividad.DataValueField = "ListaIdStr";
            ddlActividad.DataSource = TraerResultadosLista("Actividad_Negocio");
            ddlActividad.DataBind();


            ddlEstadoAfi.Items.Insert(0, new ListItem("Seleccione un item", "0"));
            ddlEstadoAfi.Items.Insert(1, new ListItem("Activo", "A"));
            ddlEstadoAfi.Items.Insert(2, new ListItem("Retirado", "R"));
            ddlEstadoAfi.Items.Insert(3, new ListItem("Inactivo", "I"));
            ddlEstadoAfi.SelectedIndex = 0;
            ddlEstadoAfi.DataBind();
            ctlFormatos.Inicializar("1");
            PoblarLista("periodicidad", ddlPeriodicidad);
 
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(TerceroServicio.GetType().Name + "L", "CargarListas", ex);
        }
    }

    private List<Xpinn.FabricaCreditos.Entities.Persona1> TraerResultadosLista(string ListaSolicitada)
    {
        Xpinn.FabricaCreditos.Services.Persona1Service DatosClienteServicio = new Xpinn.FabricaCreditos.Services.Persona1Service();    
        List<Xpinn.FabricaCreditos.Entities.Persona1> lstDatosSolicitud = new List<Xpinn.FabricaCreditos.Entities.Persona1>();
        lstDatosSolicitud.Clear();
        lstDatosSolicitud = DatosClienteServicio.ListasDesplegables(ListaSolicitada, (Usuario)Session["Usuario"]);
        return lstDatosSolicitud;
    }

    protected void imgAceptar_Click(object sender, ImageClickEventArgs e)
    {
        if (rbJuridica.Checked == true)
        {
            mvDatos.ActiveViewIndex = 1;
            Site toolBar = (Site)this.Master;           
            toolBar.MostrarGuardar(true);
        }
        else
            Navegar("../Personas/Nuevo.aspx");

    }

    protected void rbJuridica_CheckedChanged(object sender, EventArgs e)
    {
        if (rbJuridica.Checked)
            rbNatural.Checked = false;
        else
            rbNatural.Checked = true;
    }

    protected void rbNatural_CheckedChanged(object sender, EventArgs e)
    {
        if (rbNatural.Checked)
            rbJuridica.Checked = false;
        else
            rbJuridica.Checked = true;
    }

    protected void gvInfoAdicional_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TextBox txtCadena = (TextBox)e.Row.FindControl("txtCadena");
            TextBox txtNumero = (TextBox)e.Row.FindControl("txtNumero");
            fecha txtctlfecha = (fecha)e.Row.FindControl("txtctlfecha");
            txtCadena.Visible = false;
            txtNumero.Visible = false;
            txtctlfecha.Visible = false;

            DropDownListGrid ddlDropdown = (DropDownListGrid)e.Row.FindControl("ddlDropdown");
            ddlDropdown.Visible = false;

            Label lblopcionaActivar = (Label)e.Row.FindControl("lblopcionaActivar");
            if (lblopcionaActivar != null)
            {
                if (lblopcionaActivar.Text == "1")//CARACTER
                {
                    txtCadena.Visible = true;
                }
                else if (lblopcionaActivar.Text == "2")//FECHA
                {
                    txtctlfecha.Visible = true;
                }
                else if (lblopcionaActivar.Text == "3") //NUMERO
                {
                    txtNumero.Visible = true;
                }
                else if (lblopcionaActivar.Text == "4") // DROPDOWNLIST
                {
                    ddlDropdown.Visible = true;

                    Label lblDropdown = (Label)e.Row.FindControl("lblDropdown");

                    if (lblDropdown.Text != "")
                        Session["Datos"] = lblDropdown.Text;
                    if (ddlDropdown != null)
                    {
                        string[] sDatos;

                        if (lblDropdown.Text != "")
                            sDatos = lblDropdown.Text.Split(',');
                        else
                            sDatos = Session["Datos"].ToString().Split(',');
                        if (sDatos.Count() > 0)
                        {
                            ddlDropdown.Items.Clear();
                            for (int i = 0; i < sDatos.Count(); i++)
                            {
                                ddlDropdown.Items.Insert(i, new ListItem(sDatos[i].ToString(),sDatos[i].ToString()));                                
                            }
                            ddlDropdown.DataBind();                         
                        }
                    }

                    Label lblValorDropdown = (Label)e.Row.FindControl("lblValorDropdown");
                    if (lblValorDropdown.Text != "")
                    {
                        ddlDropdown.SelectedValue = lblValorDropdown.Text;
                    }
                }
            }
        }
    }


    public void PoblarLista(string pTabla, DropDownList ddlControl)
    {
        PoblarLista(pTabla, "", "", "", ddlControl);
    }

    public void PoblarLista(string pTabla, string pColumnas, string pCondicion, string pOrden, DropDownList ddlControl)
    {
        List<Xpinn.Comun.Entities.ListaDesplegable> plista = new List<Xpinn.Comun.Entities.ListaDesplegable>();
        Xpinn.Comun.Entities.ListaDesplegable pentidad = new Xpinn.Comun.Entities.ListaDesplegable();
        Xpinn.Comun.Services.ListaDeplegableService pservicio = new Xpinn.Comun.Services.ListaDeplegableService();

        ddlControl.Items.Clear();
        plista = pservicio.ListarListaDesplegable(pentidad, pTabla, pColumnas, pCondicion, pOrden, (Usuario)Session["usuario"]);
        pentidad.idconsecutivo = null;
        pentidad.descripcion = "Seleccione un item";
        plista.Insert(0, pentidad);
        ddlControl.DataTextField = "descripcion";
        ddlControl.DataValueField = "idconsecutivo";
        ddlControl.DataSource = plista;
        ddlControl.DataBind();
    }


    protected void ddlEstadoAfi_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlEstadoAfi.SelectedValue == "1")
            panelFecha.Enabled = false;
        else
            panelFecha.Enabled = true;
    }

    protected void ddlFormaPago_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlFormaPago.SelectedItem.Value == "2" || ddlFormaPago.SelectedItem.Text == "Nomina")
        {
            lblEmpresa.Visible = true;
            ddlEmpresa.Visible = true;
        }
        else
        {
            lblEmpresa.Visible = false;
            ddlEmpresa.Visible = false;
        }
    }

    protected void btnImprimir_Click(object sender, ImageClickEventArgs e)
    {
        ctlFormatos.lblErrorText = "";
        if (ctlFormatos.ddlFormatosItem != null)
            ctlFormatos.ddlFormatosIndex = 0;
        ctlFormatos.MostrarControl();
    }
    protected void btnImpresion_Click(object sender, EventArgs e)
    {
        try
        {
            // ELIMINANDO ARCHIVOS GENERADOS
            try
            {
                string[] ficherosCarpeta = Directory.GetFiles(Server.MapPath("Documentos\\"));
                foreach (string ficheroActual in ficherosCarpeta)
                    File.Delete(ficheroActual);
            }
            catch
            { }
            string pRuta = "~/Page/Aportes/Afiliaciones/Documentos/";
            string pVariable = txtCodigo.Text.Trim();
            ctlFormatos.ImprimirFormato(pVariable, pRuta);

            //Descargando el Archivo PDF
            string cNombreDeArchivo = pVariable.Trim() + "_" + ctlFormatos.ddlFormatosValue + ".pdf";
            string cRutaLocalDeArchivoPDF = Server.MapPath("Documentos\\" + cNombreDeArchivo);

            if (GlobalWeb.bMostrarPDF == true)
            {
                // Copiar el archivo a una ruta local
                try
                {
                    FileStream fs = File.OpenRead(cRutaLocalDeArchivoPDF);
                    if (fs.Length <= 0)
                    {
                        ctlFormatos.lblErrorText = cRutaLocalDeArchivoPDF;
                        ctlFormatos.lblErrorIsVisible = true;
                        //ScriptManager.RegisterStartupScript(this, typeof(Page), "jsKeys", "javascript:Forzar();", true);
                    }
                    byte[] data = new byte[fs.Length];
                    fs.Read(data, 0, (int)fs.Length);
                    fs.Close();
                    Session["Archivo" + Usuario.codusuario] = cRutaLocalDeArchivoPDF;
                    panelFinal.Visible = true;
                }
                catch (Exception ex)
                {
                    ctlFormatos.lblErrorText = ex.Message;
                    ctlFormatos.lblErrorIsVisible = true;
                }
            }
            else
            {
                Response.Clear();
                Response.ClearContent();
                Response.ClearHeaders();
                Response.Buffer = false;
                Response.AddHeader("Content-Disposition", "attachment;filename=" + cNombreDeArchivo);
                Response.ContentType = "application/octet-stream";
                Response.Flush();
                Response.WriteFile(cRutaLocalDeArchivoPDF);
                Response.End();
            }
            RegistrarPostBack();
        }
        catch (Exception ex)
        {
            ctlFormatos.lblErrorIsVisible = true;
            ctlFormatos.lblErrorText = ex.Message;
            //ScriptManager.RegisterStartupScript(this, typeof(Page), "jsKeys", "javascript:Forzar();", true);
        }
    }
    protected void btnVerData_Click(object sender, EventArgs e)
    {
        panelFinal.Visible = false;
    }
}