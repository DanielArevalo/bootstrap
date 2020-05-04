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
using Xpinn.Ahorros.Services;
using Xpinn.Ahorros.Entities;
using Xpinn.Asesores.Entities;
using Xpinn.Asesores.Services;
using Xpinn.FabricaCreditos.Entities;
using Xpinn.FabricaCreditos.Services;
using System.Text;
using System.IO;
using System.Web.UI.HtmlControls;

partial class Lista : GlobalWeb
{
    CruceCtaAProductoServices CruceService = new CruceCtaAProductoServices();
    PoblarListas poblarLista = new PoblarListas();
    Usuario _usuario;

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(CruceService.CodigoPrograma, "L");
            
            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
            ctlMensaje.eventoClick += btnContinuarMen_Click;
            ctlproceso.eventoCancelarClick += btnCancelarProceso_Click;
            ctlproceso.eventoClick += btnAceptarProceso_Click;
            toolBar.MostrarGuardar(false);            
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CruceService.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    private void cargarCombos()
    {
        List<Ejecutivo> lstZonas = new List<Ejecutivo>();
        EjecutivoService serviceEjecutivo = new EjecutivoService();

        UsuarioAtribuciones atrusuarios = new UsuarioAtribuciones();
        UsuarioAtribucionesService atribuciones = new UsuarioAtribucionesService();
        atrusuarios.codusuario = _usuario.codusuario;
        atrusuarios.tipoatribucion = 1;
        atrusuarios.activo = 1;
        List<UsuarioAtribuciones> atrusuario = atribuciones.ListarUsuarioAtribuciones(atrusuarios, _usuario);
        if (atrusuarios != null && atrusuario.Count > 0)
        {
            //Lista de zonas en total
            lstZonas = serviceEjecutivo.ListarZonasDeEjecutivo(0, (Usuario)Session["usuario"]);
            List<Ejecutivo> LstEjecutivos = serviceEjecutivo.ListarEjecutivo((Usuario)Session["usuario"]);
            pnlAse.Visible = true;
            if (LstEjecutivos != null && LstEjecutivos.Count > 0)
            {
                ddlAsesores.DataSource = LstEjecutivos;
                ddlAsesores.DataValueField = "Codigo";
                ddlAsesores.DataTextField = "PrimerNombre";
                ddlAsesores.DataBind();
                ddlAsesores.Items.Insert(0, new ListItem("Seleccione", ""));
                ddlAsesores.SelectedValue = "";
            }
        }
        else
        {
            //Lista de zonas por ejecutivo
            lstZonas = serviceEjecutivo.ListarZonasDeEjecutivo(_usuario.codusuario, (Usuario)Session["usuario"]);
        }
        if (lstZonas.Count > 0)
        {
            ddlZona.DataSource = lstZonas;
            ddlZona.DataValueField = "icodciudad";
            ddlZona.DataTextField = "nomciudad";
            ddlZona.DataBind();
        }
        ddlZona.Items.Insert(0, new ListItem("Seleccione", ""));
        ddlZona.SelectedValue = "";
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            _usuario = (Usuario)Session["Usuario"];
            if (!Page.IsPostBack)
            {
                cargarCombos();
                panelProceso.Visible = false;
                ViewState["DTCruceAhorro"] = null;
                ViewState["ID"] = null;
                ViewState["TIPO"] = null;
                mvPrincipal.ActiveViewIndex = 0;
                CargarDropDown();
                LimpiarData();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CruceService.CodigoPrograma, "Page_Load", ex);
        }
    }

    protected void LimpiarData()
    {
        VerError("");
        txtFecha.Text = "";
        pDatos.Visible = false;
        lblTotalRegs.Visible = false;
        lblInfo.Visible = false;
        gvLista.DataSource = null;        
        gvLista.DataBind();
        LimpiarValoresConsulta(pConsulta, CruceService.CodigoPrograma);
        Site toolBar = (Site)this.Master;
        toolBar.MostrarGuardar(false);
        toolBar.MostrarLimpiar(true);
        mvPrincipal.ActiveViewIndex = 0;
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        if (mvPrincipal.ActiveViewIndex == 0)
        {
            Actualizar();
        }
        else
        {
            LimpiarData();
        }        
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        LimpiarData();
    }

    protected void CargarDropDown()
    {
        poblarLista.PoblarListaDesplegable("oficina", "COD_OFICINA, NOMBRE", "estado = 1", "2", ddlOficina, (Usuario)Session["usuario"]);
    }


    private void Actualizar()
    {
        try
        {
            List<Solicitud_cruce_ahorro> lstConsulta = new List<Solicitud_cruce_ahorro>();
            String filtro = obtFiltro();
            lstConsulta = CruceService.ListarSolicitud_cruce(filtro, (Usuario)Session["usuario"]);
            Session[Usuario.codusuario + "DTSolicitud"] = lstConsulta;
            gvLista.PageSize = 15;
            String emptyQuery = "Fila de datos vacia";
            gvLista.EmptyDataText = emptyQuery;
            gvLista.DataSource = lstConsulta;
            Site toolBar = (Site)this.Master;
            
            if (lstConsulta.Count > 0)
            {
                ViewState["DTCruceAhorro"] = lstConsulta;
                pDatos.Visible = true;
                lblInfo.Visible = false;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                gvLista.DataBind();
                toolBar.MostrarGuardar(true);
            }
            else
            {
                ViewState["DTCruceAhorro"] = null;
                pDatos.Visible = false;
                lblTotalRegs.Visible = false;
                lblInfo.Visible = true;
                toolBar.MostrarGuardar(false);
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CruceService.CodigoPrograma, "Actualizar", ex);
        }
    }


    private string obtFiltro()
    {
        ConnectionDataBase conexion = new ConnectionDataBase();

        String filtro = String.Empty;
        
        if (txtFecha.Text.Trim() != "")
            filtro += " and S.FECHA_PAGO = to_date('" + txtFecha.Text + "','" + gFormatoFecha + "')";
        if (txtIdentificacion.Text.Trim() != "")
            filtro += " and P.IDENTIFICACION = '" + txtIdentificacion.Text.Trim() + "'";
        if (txtNombres.Text.Trim() != "")
            filtro += " and upper(P.NOMBRES) like '%" + txtNombres.Text.Trim() + "%'";
        if (txtApellidos.Text.Trim() != "")
            filtro += " and upper(P.APELLIDOS) like '%" + txtApellidos.Text.Trim() + "%'";
        if (ddlOficina.SelectedItem != null && ddlOficina.SelectedIndex > 0)
            filtro += " and P.COD_OFICINA = " + ddlOficina.SelectedValue;

        if (txtFecha.Text != "")
        {
            if (conexion.TipoConexion() == "ORACLE")
            {
                filtro += " and S.FECHA_PAGO = to_date('" + txtFecha.ToDateTime.ToShortDateString() + "','" + gFormatoFecha + "')"; 
            }
            else
            {
                filtro += " and S.FECHA_CREACION = '" + txtFecha.ToDateTime.ToShortDateString() + "','" + gFormatoFecha + "')"; 
            }
        }

        UsuarioAtribuciones atrusuarios = new UsuarioAtribuciones();
        UsuarioAtribucionesService atribuciones = new UsuarioAtribucionesService();
        atrusuarios.codusuario = _usuario.codusuario;
        atrusuarios.tipoatribucion = 1;
        atrusuarios.activo = 1;
        List<UsuarioAtribuciones> atrusuario = atribuciones.ListarUsuarioAtribuciones(atrusuarios, _usuario);
        if (atrusuarios != null && atrusuario.Count > 0)
        {
            if (ddlAsesores.SelectedValue != "")
            {
                if (chkSinAsesor.Checked)
                    filtro += " and (p.Cod_Asesor = " + ddlAsesores.SelectedValue + " or p.Cod_Asesor is null or p.Cod_Asesor = '' or p.Cod_asesor not in (select icodigo from Asejecutivos where Iestado = 1))";
                else
                    filtro += " and p.Cod_Asesor = " + ddlAsesores.SelectedValue + " ";
            }
        }
        else if (chkSinAsesor.Checked)
            filtro += " and (p.Cod_Asesor = " + _usuario.codusuario + " or p.Cod_Asesor is null or p.Cod_Asesor = '' or p.Cod_asesor not in (select icodigo from Asejecutivos where Iestado = 1))";
        else
            filtro += " and p.Cod_Asesor = " + _usuario.codusuario + " ";

        if (ddlZona.SelectedValue != "")
        {
            if (ddlZona.SelectedValue == "0")
                filtro += " and p.cod_zona is null";
            else
                filtro += " and p.cod_zona = " + ddlZona.SelectedValue + " ";
        }


        filtro += " and S.estado = 1";

        return filtro;
    }


    protected Boolean ValidarDatos()
    {
        if (gvLista.Rows.Count == 0)
        {
            VerError("No existen datos por registrar, verifique los datos.");
            return false;
        }
        int cont = gvLista.Rows.OfType<GridViewRow>().Where( x => ((CheckBox)x.FindControl("cbSeleccionar")).Checked).Count();
        
        if (cont == 0)
        {
            VerError("No existen datos seleccionados para modificar, verifique los datos.");
            return false;
        }
        return true;
    }


    protected void btnCancelarProceso_Click(object sender, EventArgs e)
    {
        Site toolBar = (Site)Master;
        toolBar.MostrarGuardar(true);
        toolBar.MostrarCancelar(true);
        toolBar.MostrarConsultar(true);
        panelGeneral.Visible = true;
        panelProceso.Visible = false;
    }

    protected void btnAceptarProceso_Click(object sender, EventArgs e)
    {
        try
        {
            panelGeneral.Visible = true;
            panelProceso.Visible = false;
            // Aquí va la función que hace lo que se requiera grabar en la funcionalidad
            RealizarCruce();
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }

    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        if (ValidarDatos())
        {
            ViewState["TIPO"] = "GRABAR";
            ctlMensaje.MostrarMensaje("Desea aplicar el cruce de los productos seleccionados?");
        }
    }


    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        if (ViewState["TIPO"] != null)
        {
            if (ViewState["TIPO"].ToString() == "GRABAR")
            {
                Int64? rptaPro = ctlproceso.Inicializar(125, DateTime.Now, (Usuario)Session["Usuario"]);
                if (rptaPro > 1)
                {
                    Site toolBar = (Site)Master;
                    toolBar.MostrarGuardar(false);
                    // Activar demás botones que se requieran
                    panelGeneral.Visible = false;
                    panelProceso.Visible = true;
                }
                else
                {
                    RealizarCruce();
                }                           
            }
            else if (ViewState["TIPO"].ToString() == "ELIMINAR")
            {
                Int64 idELiminar = 0;
                if (ViewState["ID"] != null)
                {
                    idELiminar = Convert.ToInt64(ViewState["ID"].ToString());
                    ViewState["ID"] = null;
                    //llamar metodo de eliminación
                    CruceService.EliminarSolicitud_Cruce_ahorro(idELiminar, (Usuario)Session["usuario"]);
                    Actualizar();
                }
            }
        }
    }

    protected void RealizarCruce()
    {
        Usuario pUsuario = (Usuario)Session["Usuario"];
        //OBTENER LOS DATOS POR Cruzar
        List<Solicitud_cruce_ahorro> lstSolicitud = new List<Solicitud_cruce_ahorro>();
        lstSolicitud = ObtenerListaCruzar();
        //Declaracion de variables
        string pError = "";
        Boolean rpta = false;
        Int64 pCod_Ope = 0;
        List<Xpinn.Tesoreria.Entities.Operacion> lstOperaciones = new List<Xpinn.Tesoreria.Entities.Operacion>();
        
        //CONSULTANDO PROCESO CONTABLE
        Xpinn.Contabilidad.Services.ProcesoContableService procesoContable = new Xpinn.Contabilidad.Services.ProcesoContableService();
        Xpinn.Contabilidad.Entities.ProcesoContable eproceso = new Xpinn.Contabilidad.Entities.ProcesoContable();
        eproceso = procesoContable.ConsultarProcesoContableOperacion(125, pUsuario);
        if (eproceso == null)
        {
            VerError("No hay ningún proceso contable parametrizado para el tipo de operación 125");
            return;
        }
        if (eproceso.cod_proceso == null)
        {
            VerError("No hay ningún proceso contable parametrizado para el tipo de operación 125");
            return;
        }
        if (ctlproceso.cod_proceso != null)
            if (ctlproceso.cod_proceso != 0)
                eproceso.cod_proceso = Convert.ToInt32(ctlproceso.cod_proceso);

        rpta = CruceService.AplicarCruceProducto(lstSolicitud, pUsuario, eproceso.cod_proceso, ref lstOperaciones, ref pError);
        if (rpta == false || pError != "")
        {
            if (pError == "")
                VerError("Ocurrio un error al generar la confirmación de las personas seleccionadas.");
            else
                VerError(pError);
            return;
        }
        if (lstOperaciones.Count > 0)
        {
            gvOperacion.DataSource = lstOperaciones;
            gvOperacion.DataBind();
        }
        Site toolBar = (Site)Master;
        toolBar.MostrarGuardar(false);
        toolBar.MostrarLimpiar(false);
        mvPrincipal.ActiveViewIndex = 1;
    }

    protected List<Solicitud_cruce_ahorro> ObtenerListaCruzar()
    {
        List<Solicitud_cruce_ahorro> lstData = new List<Solicitud_cruce_ahorro>();
        List<Solicitud_cruce_ahorro> lstReturn = new List<Solicitud_cruce_ahorro>();
        if (ViewState["DTCruceAhorro"] != null)
        {
            lstData = (List<Solicitud_cruce_ahorro>)ViewState["DTCruceAhorro"];
        }
        if (gvLista.Rows.Count > 0)
        {
            int contador = 0;
            foreach (GridViewRow rFila in gvLista.Rows)
            {
                int rowIndex = -1;
                CheckBox cbSeleccionar = (CheckBox)rFila.FindControl("cbSeleccionar");
                if (cbSeleccionar != null)
                {
                    if (cbSeleccionar.Checked)
                    {
                        rowIndex = (gvLista.PageIndex * gvLista.PageSize) + contador;
                        Solicitud_cruce_ahorro pEntidad = new Solicitud_cruce_ahorro();
                        pEntidad = lstData[rowIndex];
                        
                        if (pEntidad.valor_pago > 0)
                            lstReturn.Add(pEntidad);
                    }
                }
                contador++;
            }            
        }
        return lstReturn;
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
            BOexcepcion.Throw(CruceService.CodigoPrograma, "gvLista_PageIndexChanging", ex);
        }
    }


    protected void cbSeleccionarEncabezado_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox cbSeleccionarEncabezado = (CheckBox)sender;
        if (cbSeleccionarEncabezado != null)
        {
            foreach (GridViewRow rFila in gvLista.Rows)
            {
                CheckBox cbSeleccionar = (CheckBox)rFila.FindControl("cbSeleccionar");
                cbSeleccionar.Checked = cbSeleccionarEncabezado.Checked;
            }
        }
    }

    protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            string id = gvLista.DataKeys[e.RowIndex].Values[0].ToString();
            ViewState["TIPO"] = "ELIMINAR";
            ViewState["ID"] = id; 
            ctlMensaje.MostrarMensaje("Seguro que desea eliminar este registro?");
        }
        catch
        {
            VerError("no puede borar el escalafon por que ya hay personas con este escalafon salarial");
        }
    }


    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        string id = gvLista.DataKeys[e.NewEditIndex].Values[0].ToString();
        Session[CruceService.CodigoPrograma + ".id"] = id;
        Navegar(Pagina.Detalle);
    }

    protected void btnExportar_Click(object sender, EventArgs e)
    {
        VerError("");
        if (gvLista.Rows.Count > 0)
        {
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            Page pagina = new Page();
            dynamic form = new HtmlForm();
            gvLista.Columns[0].Visible = false;
            gvLista.Columns[1].Visible = false;
            gvLista.Columns[2].Visible = false;
            gvLista.AllowPaging = false;
            gvLista.DataSource = Session[Usuario.codusuario + "DTSolicitud"];
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
            Response.AddHeader("Content-Disposition", "attachment;filename=ListaPersonas.xls");
            Response.Charset = "UTF-8";
            Response.ContentEncoding = Encoding.Default;
            Response.Write(sb.ToString());
            Response.End();
        }
        else
        {
            VerError("No existen datos, genere la consulta");
        }
    }

}