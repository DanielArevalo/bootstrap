using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Tesoreria.Services;
using Xpinn.Tesoreria.Entities;
using Xpinn.Util;
using System.IO;
using System.Configuration;
using System.Globalization;
using System.Text;
using System.Web.UI.HtmlControls;

public partial class Nuevo : GlobalWeb
{
    EstructuraRecaudoServices estructuraService = new EstructuraRecaudoServices();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(estructuraService.CodigoPrograma, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
            ctlMensaje.eventoClick += btnContinuarMen_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(estructuraService.GetType().Name + "L", "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                mvAplicar.ActiveViewIndex = 0;
                CargaFormatosFecha();
                Session["DetalleCarga"] = null;
                txtCodigo.Enabled = false;
                if (Session[estructuraService.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[estructuraService.CodigoPrograma + ".id"].ToString();
                    Session.Remove(estructuraService.CodigoPrograma + ".id");
                    ObtenerDatos(idObjeto);
                    rblTipoArchivo_SelectedIndexChanged(rblTipoArchivo, null);
                    Session["TEXTO"] = "modificar";
                    lblmsj.Text = "modificado";
                }
                else
                {
                    rblTipoArchivo.SelectedValue = "1";
                    rblTipoArchivo_SelectedIndexChanged(rblTipoArchivo, null);
                    if (Session["ListaEstructura"] != null)
                    {
                        List<Estructura_Carga> lstConsulta = new List<Estructura_Carga>();
                        Estructura_Carga datos = (Estructura_Carga)Session["obtenciondatos"];
                        lstConsulta = estructuraService.ListarEstructuraRecaudo(datos, (Usuario)Session["usuario"], Session["filtro"].ToString(),2);
                        ddlEstructura.DataTextField = "descripcion";
                        ddlEstructura.DataValueField = "cod_estructura_carga";
                        ddlEstructura.DataSource = lstConsulta;
                        ddlEstructura.DataBind();
                    }
                    ddlEstructura.Items.Insert(0, new ListItem("Seleccionar Item", "0"));
                    rblTipoDato.SelectedValue = "0";
                    chkSeparaCampo.SelectedValue = "2";
                    chkSeparaCampo.Enabled = true;
                    Session["TEXTO"] = "grabar";
                    lblmsj.Text = "grabado";
                    InicializargvDetalle();
                }
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(estructuraService.GetType().Name + "L", "Page_Load", ex);
        }

    }


    void CargaFormatosFecha()
    {
        ddlFormatoFecha.Items.Insert(0, new ListItem("Seleccione un Item", "0"));
        ddlFormatoFecha.Items.Insert(1, new ListItem("dd/MM/yyyy", "1"));
        ddlFormatoFecha.Items.Insert(2, new ListItem("yyyy/MM/dd", "2"));
        ddlFormatoFecha.Items.Insert(3, new ListItem("MM/dd/yyyy", "3"));
        ddlFormatoFecha.Items.Insert(4, new ListItem("ddMMyyyy", "4"));
        ddlFormatoFecha.Items.Insert(5, new ListItem("yyyyMMdd", "5"));
        ddlFormatoFecha.Items.Insert(6, new ListItem("MMddyyyy", "6"));
        ddlFormatoFecha.SelectedIndex = 0;
        ddlFormatoFecha.DataBind();
    }

    protected void InicializargvDetalle()
    {
        List<Estructura_Carga_Detalle> lstDetalle = new List<Estructura_Carga_Detalle>();
        for (int i = gvDetalle.Rows.Count; i < 5; i++)
        {
            Estructura_Carga_Detalle eDetCarga = new Estructura_Carga_Detalle();
            eDetCarga.cod_estructura_detalle = -1;
            eDetCarga.codigo_campo = null;
            eDetCarga.numero_columna = null;
            eDetCarga.posicion_inicial = null;
            eDetCarga.longitud = null;
            eDetCarga.justificacion = "";
            eDetCarga.justificador = "";
            eDetCarga.vr_campo_fijo = null;
            lstDetalle.Add(eDetCarga);
        }
        gvDetalle.DataSource = lstDetalle;
        gvDetalle.DataBind();

        Session["DetalleCarga"] = lstDetalle;

    }

    protected List<Estructura_Carga_Detalle> ObtenerListaDetalle()
    {
        try
        {
            List<Estructura_Carga_Detalle> lstDetalle = new List<Estructura_Carga_Detalle>();
            //lista para adicionar filas sin perder datos
            List<Estructura_Carga_Detalle> lista = new List<Estructura_Carga_Detalle>();

            foreach (GridViewRow rfila in gvDetalle.Rows)
            {
                Estructura_Carga_Detalle ePogra = new Estructura_Carga_Detalle();

                Label lblcod_estructura_detalle = (Label)rfila.FindControl("lblcod_estructura_detalle");
                if (lblcod_estructura_detalle != null)
                    ePogra.cod_estructura_detalle = Convert.ToInt32(lblcod_estructura_detalle.Text);
                else
                    ePogra.cod_estructura_detalle = -1;

                DropDownListGrid ddlcodigo_campo = (DropDownListGrid)rfila.FindControl("ddlcodigo_campo");
                if (ddlcodigo_campo.SelectedValue != null)
                    if (ddlcodigo_campo.SelectedValue != "")
                        ePogra.codigo_campo = Convert.ToInt32(ddlcodigo_campo.SelectedValue);

                TextBox txtnumero_columna = (TextBox)rfila.FindControl("txtnumero_columna");
                if (txtnumero_columna != null)
                    if (txtnumero_columna.Text != "")
                        ePogra.numero_columna = Convert.ToInt32(txtnumero_columna.Text);
                    else ePogra.numero_columna = null;
                else
                    ePogra.numero_columna = null;

                TextBox txtposicion_inicial = (TextBox)rfila.FindControl("txtposicion_inicial");
                if (txtposicion_inicial != null)
                    if (txtposicion_inicial.Text != "")
                        ePogra.posicion_inicial = Convert.ToInt32(txtposicion_inicial.Text);
                    else
                        ePogra.posicion_inicial = null;
                else
                    ePogra.posicion_inicial = null;

                TextBox txtlongitud = (TextBox)rfila.FindControl("txtlongitud");
                if (txtlongitud != null)
                    if (txtlongitud.Text != "")
                        ePogra.longitud = Convert.ToInt32(txtlongitud.Text);
                    else
                        ePogra.longitud = null;
                else
                    ePogra.longitud = null;

                DropDownListGrid ddljustificacion = (DropDownListGrid)rfila.FindControl("ddljustificacion");
                if (ddljustificacion.SelectedValue != null)
                    if (ddljustificacion.SelectedValue != "")
                        ePogra.justificacion = ddljustificacion.SelectedValue;

                TextBox txtjustificador = (TextBox)rfila.FindControl("txtjustificador");
                if (txtjustificador != null)
                    if (txtjustificador.Text != "")
                        ePogra.justificador = txtjustificador.Text;
                    else
                        ePogra.justificador = null;
                else
                    ePogra.justificador = null;

                TextBox txtVrCampoFijo = (TextBox)rfila.FindControl("txtVrCampoFijo");
                if (txtVrCampoFijo.Visible == true)
                {
                    if (txtVrCampoFijo.Text != "")
                        ePogra.vr_campo_fijo = txtVrCampoFijo.Text;
                }

                lista.Add(ePogra);
                Session["DetalleCarga"] = lista;

                if (rblTipoArchivo.SelectedValue == "0")
                {
                    if (ePogra.codigo_campo != 0 && ePogra.numero_columna != 0 && ePogra.justificacion != "" && ePogra.justificador != "")
                    {
                        lstDetalle.Add(ePogra);
                    }
                }
                else
                {
                    if (ePogra.codigo_campo != 0 && ePogra.posicion_inicial != 0 && ePogra.longitud != 0 && ePogra.justificacion != "" && ePogra.justificador != "")
                    {
                        lstDetalle.Add(ePogra);
                    }
                }

            }
            return lstDetalle;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(estructuraService.CodigoPrograma, "ObtenerListaDetalle", ex);
            return null;
        }
    }



    protected void gvDetalle_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownListGrid ddlcodigo_campo = (DropDownListGrid)e.Row.FindControl("ddlcodigo_campo");
            if (ddlcodigo_campo != null)
            {

                ddlcodigo_campo.Items.Insert(0, new ListItem("Seleccione un Item", "0"));
                ddlcodigo_campo.Items.Insert(1, new ListItem("Código de la Persona", "1"));
                ddlcodigo_campo.Items.Insert(2, new ListItem("Identificación", "2"));
                ddlcodigo_campo.Items.Insert(3, new ListItem("Nombre", "3"));
                ddlcodigo_campo.Items.Insert(4, new ListItem("Concepto", "4"));
                ddlcodigo_campo.Items.Insert(5, new ListItem("Valor", "5"));
                ddlcodigo_campo.Items.Insert(6, new ListItem("Fecha", "6"));
                ddlcodigo_campo.Items.Insert(7, new ListItem("Nro Producto", "7"));
                ddlcodigo_campo.Items.Insert(8, new ListItem("Fecha Prox Pago", "8"));
                ddlcodigo_campo.Items.Insert(9, new ListItem("Tipo Producto", "9"));

                ddlcodigo_campo.Items.Insert(10, new ListItem("Tipo de Novedad", "10"));
                ddlcodigo_campo.Items.Insert(11, new ListItem("Fecha de Novedad", "11"));
                ddlcodigo_campo.Items.Insert(12, new ListItem("Fecha Inicio Novedad", "12"));
                ddlcodigo_campo.Items.Insert(13, new ListItem("Fecha Final Novedad", "13"));
                ddlcodigo_campo.Items.Insert(14, new ListItem("Fecha Radicación", "14"));
                ddlcodigo_campo.Items.Insert(15, new ListItem("Monto Total", "15"));
                ddlcodigo_campo.Items.Insert(16, new ListItem("Número Cuotas", "16"));
                ddlcodigo_campo.Items.Insert(17, new ListItem("Código de la Ciudad", "17"));
                ddlcodigo_campo.Items.Insert(18, new ListItem("Direccion", "18"));
                ddlcodigo_campo.Items.Insert(19, new ListItem("Teléfono", "19"));
                ddlcodigo_campo.Items.Insert(20, new ListItem("E-mail", "20"));
                ddlcodigo_campo.Items.Insert(21, new ListItem("Periodo", "21"));
                ddlcodigo_campo.Items.Insert(22, new ListItem("Campo Fijo", "22"));
                ddlcodigo_campo.Items.Insert(23, new ListItem("Codigo Nómina", "23"));
                ddlcodigo_campo.Items.Insert(24, new ListItem("Código de Planilla", "24"));
                ddlcodigo_campo.Items.Insert(25, new ListItem("Periodicidad", "25"));
                ddlcodigo_campo.Items.Insert(26, new ListItem("Saldo", "26"));
                ddlcodigo_campo.Items.Insert(27, new ListItem("Total", "27"));
                ddlcodigo_campo.Items.Insert(28, new ListItem("Saldo Total", "28"));
                ddlcodigo_campo.Items.Insert(29, new ListItem("Codigo Recaudo Estructura", "29"));
                ddlcodigo_campo.Items.Insert(30, new ListItem("Primer Nombre", "30"));
                ddlcodigo_campo.Items.Insert(31, new ListItem("Segundo Nombre", "31"));
                ddlcodigo_campo.Items.Insert(32, new ListItem("Primer Apellido", "32"));
                ddlcodigo_campo.Items.Insert(33, new ListItem("Segundo Apellido", "33"));
                ddlcodigo_campo.Items.Insert(34, new ListItem("N° Radicado", "34"));

                ddlcodigo_campo.Items.Insert(35, new ListItem("Capital", "35"));
                ddlcodigo_campo.Items.Insert(36, new ListItem("Valor Interes Cte", "36"));
                ddlcodigo_campo.Items.Insert(37, new ListItem("Valor Interes Mora", "37"));
                ddlcodigo_campo.Items.Insert(38, new ListItem("Valor Seguro", "38"));
                ddlcodigo_campo.Items.Insert(39, new ListItem("Valor Otros", "39"));
                ddlcodigo_campo.Items.Insert(40, new ListItem("Fijos", "40"));
                ddlcodigo_campo.Items.Insert(41, new ListItem("Prestamos", "41"));
                ddlcodigo_campo.Items.Insert(41, new ListItem("Fecha Inicio Crédito", "42"));
                ddlcodigo_campo.Items.Insert(41, new ListItem("Fecha Vencimiento Crédito", "43"));
                ddlcodigo_campo.SelectedIndex = 0;
                ddlcodigo_campo.DataBind();
            }

            DropDownListGrid ddljustificacion = (DropDownListGrid)e.Row.FindControl("ddljustificacion");
            if (ddljustificacion != null)
            {
                ddljustificacion.Items.Insert(0, new ListItem("Seleccione un Item", "0"));
                ddljustificacion.Items.Insert(1, new ListItem("Derecha", "1"));
                ddljustificacion.Items.Insert(2, new ListItem("Izquierda", "2"));
                ddljustificacion.SelectedIndex = 0;
                ddljustificacion.DataBind();
            }


            //RECUPERANDO DATOS
            TextBox txtVrCampoFijo = (TextBox)e.Row.FindControl("txtVrCampoFijo");
            if (txtVrCampoFijo != null)
                txtVrCampoFijo.Visible = false;

            Label lblcodigo_campo = (Label)e.Row.FindControl("lblcodigo_campo");
            if (lblcodigo_campo != null)
            {
                ddlcodigo_campo.SelectedValue = lblcodigo_campo.Text;
                if (ddlcodigo_campo.SelectedValue == "22")
                {
                    txtVrCampoFijo.Visible = true;
                }
            }
            Label lbljustificacion = (Label)e.Row.FindControl("lbljustificacion");
            if (lbljustificacion != null)
                ddljustificacion.SelectedValue = lbljustificacion.Text;
        }
    }



    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            Estructura_Carga vRecaudos = new Estructura_Carga();
            vRecaudos.cod_estructura_carga = Convert.ToInt32(pIdObjeto);
            vRecaudos = estructuraService.ConsultarEstructuraCarga(vRecaudos, (Usuario)Session["usuario"]);
            List<Estructura_Carga> lstConsulta = new List<Estructura_Carga>();
            if (Session["ListaEstructura"] != null)
            {
                Estructura_Carga datos = (Estructura_Carga)Session["obtenciondatos"];
                lstConsulta = estructuraService.ListarEstructuraRecaudo(datos, (Usuario)Session["usuario"], Session["filtro"].ToString(), 2);
                lstConsulta = lstConsulta.Where(x => x.cod_estructura_carga != Convert.ToInt32(idObjeto)).ToList();
                if (lstConsulta.Count>0)
                {
                    ddlEstructura.DataTextField = "descripcion";
                    ddlEstructura.DataValueField = "cod_estructura_carga";
                    ddlEstructura.DataSource = lstConsulta;
                    ddlEstructura.DataBind();
                }
                ddlEstructura.Items.Insert(0, new ListItem("Seleccionar Item", "0"));
            }

            if (!string.IsNullOrEmpty(vRecaudos.cod_estructura_carga.ToString()))
                txtCodigo.Text = HttpUtility.HtmlDecode(vRecaudos.cod_estructura_carga.ToString().Trim());
            if (!string.IsNullOrEmpty(vRecaudos.descripcion.ToString()))
                txtNombre.Text = HttpUtility.HtmlDecode(vRecaudos.descripcion.ToString().Trim());
            if (!string.IsNullOrEmpty(vRecaudos.tipo_archivo.ToString()))
                rblTipoArchivo.SelectedValue = HttpUtility.HtmlDecode(vRecaudos.tipo_archivo.ToString().Trim());
            if (!string.IsNullOrEmpty(vRecaudos.tipo_datos.ToString()))
                rblTipoDato.SelectedValue = HttpUtility.HtmlDecode(vRecaudos.tipo_datos.ToString().Trim());
            if (vRecaudos.separador_campo != null)
                chkSeparaCampo.SelectedValue = HttpUtility.HtmlDecode(vRecaudos.separador_campo.ToString().Trim());
            else
                chkSeparaCampo.SelectedValue = "2";
            if (vRecaudos.encabezado != null)
                if (vRecaudos.encabezado != 0)
                    txtEncabezado.Text = HttpUtility.HtmlDecode(vRecaudos.encabezado.ToString().Trim());
            if (vRecaudos.final != null)
                if (vRecaudos.final != 0)
                    txtFinal.Text = HttpUtility.HtmlDecode(vRecaudos.final.ToString().Trim());
            if (vRecaudos.formato_fecha != null)
                ddlFormatoFecha.Text = HttpUtility.HtmlDecode(vRecaudos.formato_fecha.ToString().Trim());
            if (vRecaudos.separador_decimal != null)
                txtSepDecimales.Text = HttpUtility.HtmlDecode(vRecaudos.separador_decimal.ToString().Trim());
            if (vRecaudos.separador_miles != null)
                txtSepMiles.Text = HttpUtility.HtmlDecode(vRecaudos.separador_miles.ToString().Trim());
            cbTotalizar.Checked = false;
            if (vRecaudos.totalizar != null)
                if (HttpUtility.HtmlDecode(vRecaudos.totalizar.ToString().Trim()) == "1")
                    cbTotalizar.Checked = true;
            if (vRecaudos.cod_estructura != null)
                ddlEstructura.SelectedValue = HttpUtility.HtmlDecode(vRecaudos.cod_estructura.ToString().Trim());
           
            //RECUPERAR DATOS - GRILLA PROGRAMACION-CONCEPTO
            List<Estructura_Carga_Detalle> lstDetalle = new List<Estructura_Carga_Detalle>();
            Estructura_Carga_Detalle pDeta = new Estructura_Carga_Detalle();
            pDeta.cod_estructura_carga = vRecaudos.cod_estructura_carga;
            lstDetalle = estructuraService.ListarEstructuraDetalle(pDeta," ORDER BY COD_ESTRUCTURA_DETALLE ", (Usuario)Session["usuario"]);
            if (lstDetalle.Count > 0)
            {
                if ((lstDetalle != null) || (lstDetalle.Count != 0))
                {
                    gvDetalle.DataSource = lstDetalle;
                    gvDetalle.DataBind();
                }
                Session["DetalleCarga"] = lstDetalle;
            }
            else
            {
                InicializargvDetalle();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(estructuraService.CodigoPrograma, "ObtenerDatos", ex);
        }
    }

    protected void btnFinal_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }


    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Navegar("~/Page/RecaudosMasivos/Estructura/Lista.aspx");
    }

    public Boolean ValidarDatos()
    {
        List<Estructura_Carga_Detalle> LstDetalle = new List<Estructura_Carga_Detalle>();
        if (Session["DetalleCarga"] != null)
        {
            LstDetalle = (List<Estructura_Carga_Detalle>)Session["DetalleCarga"];
        }
        if (rblTipoArchivo.SelectedItem == null)
        {
            VerError("Seleccione un Tipo de Archivo");
            return false;
        }
        if (rblTipoDato.SelectedItem == null)
        {
            VerError("Seleccione un Tipo de Dato");
        }
        if (chkSeparaCampo.SelectedItem == null)
        {
            VerError("Seleccione un Separador de Campo");
            return false;
        }
        if (ddlEstructura.SelectedIndex == 0 && txtEncabezado.Text.Trim() != "")
        {
            VerError("Seleccione la estructura para el encabezado");
            return false;
        }
        for (int i = 0; i < LstDetalle.Count; i++)
        {
            try
            {

            }
            catch (Exception ex)
            {
                VerError(ex.Message);
                return false;
            }
        }
        return true;
    }

    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        if (ValidarDatos())
        {
            ctlMensaje.MostrarMensaje("Esta seguro de " + Session["TEXTO"].ToString() + " los datos ingresados?");
        }
    }


    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        try
        {
            Estructura_Carga eEmpre = new Estructura_Carga();
            if (txtCodigo.Text != "")
                eEmpre.cod_estructura_carga = Convert.ToInt32(txtCodigo.Text);
            else
                eEmpre.cod_estructura_carga = 0;
            eEmpre.descripcion = txtNombre.Text.ToUpper();
            eEmpre.tipo_archivo = Convert.ToInt32(rblTipoArchivo.SelectedValue);
            if (rblTipoDato.Enabled == true)
                eEmpre.tipo_datos = Convert.ToInt32(rblTipoDato.SelectedValue);
            else
                eEmpre.tipo_datos = -1;

            if (chkSeparaCampo.Enabled == true)
                eEmpre.separador_campo = chkSeparaCampo.SelectedValue;
            else
                eEmpre.separador_campo = null;

            if (txtEncabezado.Text != "")
            {
                eEmpre.encabezado = Convert.ToInt32(txtEncabezado.Text);
                eEmpre.cod_estructura = Convert.ToInt32(ddlEstructura.SelectedValue);
            }
            else
                eEmpre.encabezado = 0;

            if (txtFinal.Text != "")
                eEmpre.final = Convert.ToInt32(txtFinal.Text);
            else
                eEmpre.final = 0;

            if (ddlFormatoFecha.SelectedValue != "0")
                eEmpre.formato_fecha = ddlFormatoFecha.Text;
            else
                eEmpre.formato_fecha = null;

            if (txtSepDecimales.Text != "")
                eEmpre.separador_decimal = txtSepDecimales.Text;
            else
                eEmpre.separador_decimal = null;

            if (txtSepMiles.Text != "")
                eEmpre.separador_miles = txtSepMiles.Text;
            else
                eEmpre.separador_miles = null;

            if (cbTotalizar.Checked)
                eEmpre.totalizar = 1;
            else
                eEmpre.totalizar = 0;


            
            eEmpre.lstDetalle = new List<Estructura_Carga_Detalle>();
            eEmpre.lstDetalle = ObtenerListaDetalle();


            if (idObjeto != "")
            {
                //MODIFICAR
                estructuraService.ModificarEstructuraCarga(eEmpre, (Usuario)Session["usuario"]);
            }
            else
            {
                //CREAR
                estructuraService.CrearEstructuraCarga(eEmpre, (Usuario)Session["usuario"]);
            }

            Session.Remove("DetalleCarga");
            Session.Remove("TEXTO");
            mvAplicar.ActiveViewIndex = 1;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(estructuraService.CodigoPrograma, "btnContinuar_Click", ex);
        }

    }


    protected void btnAgregar_Click(object sender, EventArgs e)
    {
        ObtenerListaDetalle();
        List<Estructura_Carga_Detalle> LstPrograma = new List<Estructura_Carga_Detalle>();
        if (Session["DetalleCarga"] != null)
        {
            LstPrograma = (List<Estructura_Carga_Detalle>)Session["DetalleCarga"];

            for (int i = 1; i <= 1; i++)
            {
                Estructura_Carga_Detalle pDetalle = new Estructura_Carga_Detalle();
                pDetalle.cod_estructura_detalle = -1;
                pDetalle.codigo_campo = -1;
                pDetalle.numero_columna = null;
                pDetalle.posicion_inicial = null;
                pDetalle.longitud = null;
                pDetalle.justificacion = null;
                pDetalle.justificador = null;
                pDetalle.vr_campo_fijo = null;
                LstPrograma.Add(pDetalle);
            }
            gvDetalle.PageIndex = gvDetalle.PageCount;
            gvDetalle.DataSource = LstPrograma;
            gvDetalle.DataBind();

            Session["DetalleCarga"] = LstPrograma;
        }
    }

    protected void gvDetalle_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int conseID = Convert.ToInt32(gvDetalle.DataKeys[e.RowIndex].Values[0].ToString());

        ObtenerListaDetalle();

        List<Estructura_Carga_Detalle> LstDetalle = new List<Estructura_Carga_Detalle>();
        LstDetalle = (List<Estructura_Carga_Detalle>)Session["DetalleCarga"];
        if (conseID > 0)
        {
            try
            {
                foreach (Estructura_Carga_Detalle acti in LstDetalle)
                {
                    if (acti.cod_estructura_detalle == conseID)
                    {
                        estructuraService.EliminarEstructuraDetalle(conseID, (Usuario)Session["usuario"]);
                        LstDetalle.Remove(acti);
                        break;
                    }
                }
            }
            catch (Xpinn.Util.ExceptionBusiness ex)
            {
                VerError(ex.Message);
            }
            catch (Exception ex)
            {
                BOexcepcion.Throw(estructuraService.CodigoPrograma, "gvDetalle_RowDeleting", ex);
            }
        }
        else
        {
            foreach (Estructura_Carga_Detalle acti in LstDetalle)
            {
                if (acti.cod_estructura_detalle == conseID)
                {
                    LstDetalle.Remove(acti);
                    break;
                }
            }
        }
        Session["DetalleCarga"] = LstDetalle;

        gvDetalle.DataSourceID = null;
        gvDetalle.DataBind();
        gvDetalle.DataSource = LstDetalle;
        gvDetalle.DataBind();
    }


    protected void rblTipoArchivo_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rblTipoArchivo.SelectedValue == "0")
        {
            rblTipoDato.Enabled = false;
            if (rblTipoDato.SelectedValue == "0")
                chkSeparaCampo.Enabled = true;
            else
                chkSeparaCampo.Enabled = false;
            chkSeparaCampo.Enabled = false;
            gvDetalle.Columns[2].Visible = true;
            gvDetalle.Columns[3].Visible = true;
            gvDetalle.Columns[4].Visible = false;
            gvDetalle.Columns[5].Visible = false;
            gvDetalle.Columns[6].Visible = true;
            gvDetalle.Columns[7].Visible = true;
        }
        else
        {
            rblTipoDato.Enabled = true;
            if (rblTipoDato.SelectedValue == "0")
                chkSeparaCampo.Enabled = true;
            else
                chkSeparaCampo.Enabled = false;
            gvDetalle.Columns[2].Visible = true;
            gvDetalle.Columns[3].Visible = false;
            gvDetalle.Columns[4].Visible = true;
            gvDetalle.Columns[5].Visible = true;
            gvDetalle.Columns[6].Visible = true;
            gvDetalle.Columns[7].Visible = true;
        }
        rblTipoDato_SelectedIndexChanged(rblTipoDato, null);
    }


    protected void rblTipoDato_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rblTipoDato.SelectedValue == "0")
        {
            chkSeparaCampo.Enabled = true;
            gvDetalle.Columns[2].Visible = true;
            gvDetalle.Columns[3].Visible = true;
            gvDetalle.Columns[4].Visible = false;
            gvDetalle.Columns[5].Visible = false;
            gvDetalle.Columns[6].Visible = true;
            gvDetalle.Columns[7].Visible = true;
        }
        else
        {
            chkSeparaCampo.Enabled = false;
            gvDetalle.Columns[2].Visible = true;
            gvDetalle.Columns[3].Visible = false;
            gvDetalle.Columns[4].Visible = true;
            gvDetalle.Columns[5].Visible = true;
            gvDetalle.Columns[6].Visible = true;
            gvDetalle.Columns[7].Visible = true;
        }
    }

    protected void chkSeparaCampo_SelectedIndexChanged(object sender, EventArgs e)
    {
        //string valor = chkSeparaCampo.SelectedItem.Text;
        //if (valor != null)
        //{
        //    chkSeparaCampo.ClearSelection();
        //    //foreach (ListItem items in chkSeparaCampo.Items)
        //    //{
        //    //    if (items.ToString() == valor)
        //    //        items.Selected = true;
        //    //    else
        //    //        items.Selected = false;
        //    //}
        //    ListItem selectedListItem = chkSeparaCampo.Items.FindByText(valor);
        //    if (selectedListItem != null)
        //        selectedListItem.Selected = true;
        //}
    }

    protected void ddlcodigo_campo_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DropDownListGrid ddlcodigo_campo = (DropDownListGrid)sender;
            int rowIndex = Convert.ToInt32(ddlcodigo_campo.CommandArgument);

            TextBox txtVrCampoFijo = (TextBox)gvDetalle.Rows[rowIndex].FindControl("txtVrCampoFijo");
            if (txtVrCampoFijo != null)
            {
                txtVrCampoFijo.Visible = ddlcodigo_campo.SelectedValue == "22" ? true : false;
            }
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }
}