using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.ConciliacionBancaria.Services;
using Xpinn.ConciliacionBancaria.Entities;
using Xpinn.Util;
using System.IO;
using System.Configuration;
using System.Globalization;
using System.Text;
using System.Web.UI.HtmlControls;

public partial class Nuevo : GlobalWeb
{
    EstructuraExtractoServices estructuraService = new EstructuraExtractoServices();

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
                CargarDropDown();
                Session["DetalleCarga"] = null;
                
                txtCodigo.Enabled = false;
                txtSepDecimales.Text = ",";
                txtSepMiles.Text = ".";
                if (Session[estructuraService.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[estructuraService.CodigoPrograma + ".id"].ToString();
                    Session.Remove(estructuraService.CodigoPrograma + ".id");
                    ObtenerDatos(idObjeto);
                    rblTipoArchivo_SelectedIndexChanged(rblTipoArchivo, null);
                }
                else
                {
                    rblTipoArchivo.SelectedValue = "1";
                    rblTipoArchivo_SelectedIndexChanged(rblTipoArchivo, null);
                    rblEstado.SelectedIndex = 0;
                    rblTipoDato.SelectedValue = "0";
                    chkSeparaCampo.SelectedValue = "2";
                    chkSeparaCampo.Enabled = true;                   
                    InicializargvDetalle();
                }
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(estructuraService.GetType().Name + "L", "Page_Load", ex);
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

    void CargarDropDown()
    {
        PoblarLista("bancos", ddlBancos);
     
        ddlFormatoFecha.Items.Insert(0, new ListItem("Seleccione un Item","0"));
        ddlFormatoFecha.Items.Insert(1, new ListItem("dd/MM/yyyy", "dd/MM/yyyy"));
        ddlFormatoFecha.Items.Insert(2, new ListItem("yyyy/MM/dd", "yyyy/MM/dd"));
        ddlFormatoFecha.Items.Insert(3, new ListItem("MM/dd/yyyy", "MM/dd/yyyy"));
        ddlFormatoFecha.Items.Insert(4, new ListItem("ddMMyyyy", "ddMMyyyy"));
        ddlFormatoFecha.Items.Insert(5, new ListItem("yyyyMMdd", "yyyyMMdd"));
        ddlFormatoFecha.Items.Insert(6, new ListItem("MMddyyyy", "MMddyyyy"));
        ddlFormatoFecha.SelectedIndex = 0;
        ddlFormatoFecha.DataBind();    
    }

    protected void InicializargvDetalle()
    {
        List<DetEstructuraExtracto> lstDetalle = new List<DetEstructuraExtracto>();
        for (int i = gvDetalle.Rows.Count; i < 5; i++)
        {
            DetEstructuraExtracto eDetCarga = new DetEstructuraExtracto();
            eDetCarga.iddetestructura = -1;
            //eCuenta.cod_empresa = -1;
            eDetCarga.codigo_campo = null;
            eDetCarga.numero_columna = null;
            eDetCarga.posicion_inicial = null;
            eDetCarga.longitud = null;
            eDetCarga.justificacion = null;
            eDetCarga.justificador = "";
            eDetCarga.decimales = null;
            lstDetalle.Add(eDetCarga);
        }
        gvDetalle.DataSource = lstDetalle;
        gvDetalle.DataBind();

        Session["DetalleCarga"] = lstDetalle;
      
    }

    protected List<DetEstructuraExtracto> ObtenerListaDetalle()
    {
        try
        {
            List<DetEstructuraExtracto> lstDetalle = new List<DetEstructuraExtracto>();
            //lista para adicionar filas sin perder datos
            List<DetEstructuraExtracto> lista = new List<DetEstructuraExtracto>();

            foreach (GridViewRow rfila in gvDetalle.Rows)
            {
                DetEstructuraExtracto ePogra = new DetEstructuraExtracto();

                Label lblcod_estructura_detalle = (Label)rfila.FindControl("lblcod_estructura_detalle");
                if (lblcod_estructura_detalle != null)
                    ePogra.iddetestructura = Convert.ToInt32(lblcod_estructura_detalle.Text);
                else
                    ePogra.iddetestructura = -1;

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
                        ePogra.justificacion = Convert.ToInt32(ddljustificacion.SelectedValue);

                TextBox txtjustificador = (TextBox)rfila.FindControl("txtjustificador");
                if (txtjustificador != null)
                    if (txtjustificador.Text != "")
                        ePogra.justificador = txtjustificador.Text;
                    else
                        ePogra.justificador = null;
                else
                    ePogra.justificador = null;
                TextBox txtDecimales = (TextBox)rfila.FindControl("txtDecimales");
                if (txtDecimales != null)
                    if (txtDecimales.Text != "")
                        ePogra.decimales = Convert.ToInt32(txtDecimales.Text);
                    else
                        ePogra.decimales = null;
                else
                    ePogra.decimales = null;


                lista.Add(ePogra);
                Session["DetalleCarga"] = lista;

                if (rblTipoArchivo.SelectedValue == "0")
                {
                    if (ePogra.codigo_campo != 0 && ePogra.numero_columna != 0 && ePogra.justificacion != null && ePogra.justificador != "")
                    {   
                        lstDetalle.Add(ePogra);
                    }
                }
                else 
                {
                    if (ePogra.codigo_campo != 0 && ePogra.posicion_inicial != 0 && ePogra.longitud != 0 && ePogra.justificacion != null && ePogra.justificador != "")
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
                ddlcodigo_campo.Items.Insert(1, new ListItem("Fecha", "1"));
                ddlcodigo_campo.Items.Insert(2, new ListItem("Numero Documento", "2"));
                ddlcodigo_campo.Items.Insert(3, new ListItem("Concepto", "3"));
                ddlcodigo_campo.Items.Insert(4, new ListItem("Tipo Movimiento", "4"));                
                ddlcodigo_campo.Items.Insert(5, new ListItem("Identificacion", "5"));
                ddlcodigo_campo.Items.Insert(6, new ListItem("Referencia", "6"));
                ddlcodigo_campo.Items.Insert(7, new ListItem("Valor", "7"));
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
            Label lblcodigo_campo = (Label)e.Row.FindControl("lblcodigo_campo");
            if (lblcodigo_campo != null)
                ddlcodigo_campo.SelectedValue = lblcodigo_campo.Text;

            Label lbljustificacion = (Label)e.Row.FindControl("lbljustificacion");
            if (lbljustificacion != null)
                ddljustificacion.SelectedValue = lbljustificacion.Text;
        }
    }
    


    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            EstructuraExtracto vRecaudos = new EstructuraExtracto();
            vRecaudos.idestructuraextracto = Convert.ToInt32(pIdObjeto);
            vRecaudos = estructuraService.ConsultarEstructuraCarga(vRecaudos, (Usuario)Session["usuario"]);

            if (!string.IsNullOrEmpty(vRecaudos.idestructuraextracto.ToString()))
                txtCodigo.Text = HttpUtility.HtmlDecode(vRecaudos.idestructuraextracto.ToString().Trim());
            if (!string.IsNullOrEmpty(vRecaudos.nombre.ToString()))
                txtNombre.Text = HttpUtility.HtmlDecode(vRecaudos.nombre.ToString().Trim());

            if (!string.IsNullOrEmpty(vRecaudos.cod_banco.ToString()))
                ddlBancos.SelectedValue = vRecaudos.cod_banco.ToString();
            if (!string.IsNullOrEmpty(vRecaudos.estado.ToString()))
                rblEstado.SelectedValue = vRecaudos.estado.ToString();

            if (!string.IsNullOrEmpty(vRecaudos.tipo_archivo.ToString()))
                rblTipoArchivo.SelectedValue = HttpUtility.HtmlDecode(vRecaudos.tipo_archivo.ToString().Trim());
            if (vRecaudos.calificador != null)
                rblTipoDato.SelectedValue = HttpUtility.HtmlDecode(vRecaudos.calificador.ToString().Trim());
            else
                rblTipoDato.SelectedValue = "0";

            if (vRecaudos.delimitador != null)
                chkSeparaCampo.SelectedValue = HttpUtility.HtmlDecode(vRecaudos.delimitador.ToString().Trim());
            else
                chkSeparaCampo.SelectedValue = "2";
            if (vRecaudos.encabezado != null)
                if(vRecaudos.encabezado!=0)
                    txtEncabezado.Text = HttpUtility.HtmlDecode(vRecaudos.encabezado.ToString().Trim());
            if (vRecaudos.totales != null)
                if(vRecaudos.totales != 0)
                    txtTotales.Text = HttpUtility.HtmlDecode(vRecaudos.totales.ToString().Trim());

            if (vRecaudos.separador_decimal != null)
                txtSepDecimales.Text = vRecaudos.separador_decimal.Trim();

            if (vRecaudos.separador_miles != null)
                txtSepMiles.Text = vRecaudos.separador_miles.Trim();

            if (vRecaudos.formato_fecha != null)
                ddlFormatoFecha.SelectedValue = vRecaudos.formato_fecha;

            //RECUPERAR DATOS - GRILLA PROGRAMACION-CONCEPTO
            List<DetEstructuraExtracto> lstDetalle = new List<DetEstructuraExtracto>();
            
            lstDetalle = estructuraService.ListarEstructuraDetalle(Convert.ToInt32(idObjeto), (Usuario)Session["usuario"]);
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
        Navegar(Pagina.Lista);
    }

    public Boolean ValidarDatos()
    {
        if (ddlBancos.SelectedIndex == 0)
        {
            VerError("Seleccione un Banco");
            return false;
        }
        if (rblTipoArchivo.SelectedItem == null)
        {
            VerError("Seleccione un Tipo de Archivo");
            return false;
        }
        if (rblTipoDato.SelectedItem == null && rblTipoDato.Enabled == true)
        {
            VerError("Seleccione un Tipo de Dato");
        }
        if (chkSeparaCampo.SelectedItem == null)
        {
            VerError("Seleccione un Separador de Campo");
            return false;
        }
        if (txtSepDecimales.Text == "")
        {
            VerError("Ingrese el separador de Decimales.");
            txtSepDecimales.Focus();
            return false;
        }
        if (txtSepMiles.Text == "")
        {
            VerError("Ingrese el separador de Miles.");
            txtSepMiles.Focus();
            return false;
        }

         return true;
    }

    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        if (ValidarDatos())
        {
            string msj = idObjeto != "" ? "modificar" : "grabar";
            ctlMensaje.MostrarMensaje("Esta seguro de " + msj + " los datos ingresados?");
        }
    }


    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        try
        {
            EstructuraExtracto eEmpre = new EstructuraExtracto();
            if (txtCodigo.Text != "" && idObjeto != "")
                eEmpre.idestructuraextracto = Convert.ToInt32(txtCodigo.Text);
            else
                eEmpre.idestructuraextracto = 0;
            eEmpre.nombre = txtNombre.Text.ToUpper();

            eEmpre.cod_banco = Convert.ToInt32(ddlBancos.SelectedValue);
            eEmpre.estado = Convert.ToInt32(rblEstado.SelectedValue);

            eEmpre.tipo_archivo = Convert.ToInt32(rblTipoArchivo.SelectedValue);
            if (rblTipoDato.Enabled == true)
                eEmpre.calificador = rblTipoDato.SelectedValue;
            else
                eEmpre.calificador = "-1";

            if (chkSeparaCampo.Enabled == true)
                eEmpre.delimitador = chkSeparaCampo.SelectedValue;
            else
                eEmpre.delimitador = null;

            if (txtEncabezado.Text != "")
                eEmpre.encabezado = Convert.ToInt32(txtEncabezado.Text);
            else
                eEmpre.encabezado = 0;

            if (txtTotales.Text != "")
                eEmpre.totales = Convert.ToInt32(txtTotales.Text);
            else
                eEmpre.totales = 0;

            eEmpre.separador_decimal = txtSepDecimales.Text.Trim();
            eEmpre.separador_miles = txtSepMiles.Text.Trim();
            eEmpre.formato_fecha = ddlFormatoFecha.SelectedIndex != 0 ? ddlFormatoFecha.SelectedValue : null;
            
            eEmpre.lstDetEstructura = new List<DetEstructuraExtracto>();
            eEmpre.lstDetEstructura = ObtenerListaDetalle();
            
                
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
            string mensaje = idObjeto != "" ? "modificado" : "grabado";
            lblmsj.Text = mensaje;
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
        List<DetEstructuraExtracto> LstPrograma = new List<DetEstructuraExtracto>();
        if (Session["DetalleCarga"] != null)
        {
            LstPrograma = (List<DetEstructuraExtracto>)Session["DetalleCarga"];

            for (int i = 1; i <= 1; i++)
            {
                DetEstructuraExtracto pDetalle = new DetEstructuraExtracto();
                pDetalle.iddetestructura = -1;
                pDetalle.codigo_campo = -1;
                pDetalle.numero_columna = null;
                pDetalle.posicion_inicial = null;
                pDetalle.longitud = null;
                pDetalle.justificacion = null;
                pDetalle.justificador = null;
                pDetalle.decimales = null;
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

        List<DetEstructuraExtracto> LstDetalle = new List<DetEstructuraExtracto>();
        LstDetalle = (List<DetEstructuraExtracto>)Session["DetalleCarga"];

        try
        {
            foreach (DetEstructuraExtracto acti in LstDetalle)
            {
                if (acti.iddetestructura == conseID)
                {
                    if (conseID > 0)
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

}
