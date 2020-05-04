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
using Xpinn.FabricaCreditos.Services;
using Xpinn.FabricaCreditos.Entities;
using Xpinn.Comun.Entities;
using Xpinn.Interfaces.Services;
using Xpinn.Interfaces.Entities;
using Xpinn.Asesores.Entities;
using Xpinn.Asesores.Services;
using System.Text;
using System.IO;
using System.Web.UI.HtmlControls;

partial class Lista : GlobalWeb
{
    private Xpinn.FabricaCreditos.Services.ControlCreditosService ControlCreditosServicio = new Xpinn.FabricaCreditos.Services.ControlCreditosService();
    SolicitudCreditosRecogidosService SolicitudCreditoServicio = new SolicitudCreditosRecogidosService();
    Xpinn.FabricaCreditos.Services.Persona1Service BOPersona = new Xpinn.FabricaCreditos.Services.Persona1Service();
    PoblarListas poblarLista = new PoblarListas();
    Usuario _usuario;

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {

            VisualizarOpciones(SolicitudCreditoServicio.CodigoprogramaSoliciCredi, "L");
            
            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
            ctlMensaje.eventoClick += btnContinuarMen_Click;
           
            toolBar.MostrarGuardar(false);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(SolicitudCreditoServicio.CodigoprogramaSoliciCredi, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            _usuario = (Usuario)Session["Usuario"];
            if (!Page.IsPostBack)
            {
                Session["ID"] = null;
                Session["TIPO"] = null;
                mvPrincipal.ActiveViewIndex = 0;
                CargarDropDown();
                LimpiarData();
                cargarCombos();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(SolicitudCreditoServicio.CodigoprogramaSoliciCredi, "Page_Load", ex);
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
            if(LstEjecutivos != null && LstEjecutivos.Count > 0)
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

    protected void LimpiarData()
    {
        VerError("");
        pDatos.Visible = false;
        lblTotalRegs.Visible = false;
        lblInfo.Visible = false;
        gvLista.DataSource = null;        
        gvLista.DataBind();
        LimpiarValoresConsulta(pConsulta, SolicitudCreditoServicio.CodigoprogramaSoliciCredi);
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
        poblarLista.PoblarListaDesplegable("OFICINA", "cod_oficina,nombre", "estado = 1", "1", ddlPeriodicidad, (Usuario)Session["usuario"]);
        poblarLista.PoblarListaDesplegable("LINEASCREDITO", "", "estado = 1", "2", ddlLinea, (Usuario)Session["usuario"]);
    }


    private void Actualizar()
    {
        try
        {
            List<SolicitudCreditoAAC> lstConsulta = new List<SolicitudCreditoAAC>();
            String filtro = obtFiltro();
            lstConsulta = SolicitudCreditoServicio.ListarSolicitudCreditoAAC(filtro, (Usuario)Session["usuario"]);

            gvLista.PageSize = 15;
            String emptyQuery = "Fila de datos vacia";
            gvLista.EmptyDataText = emptyQuery;
            gvLista.DataSource = lstConsulta;
            Site toolBar = (Site)this.Master;
            Session[Usuario.codusuario + "DTSolicitud"] = lstConsulta;

            if (lstConsulta.Count > 0)
            {
                pDatos.Visible = true;
                lblInfo.Visible = false;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                gvLista.DataBind();
                toolBar.MostrarGuardar(true);
            }
            else
            {
                pDatos.Visible = false;
                lblTotalRegs.Visible = false;
                lblInfo.Visible = true;
                toolBar.MostrarGuardar(false);
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(SolicitudCreditoServicio.CodigoprogramaSoliciCredi, "Actualizar", ex);
        }
    }


    private string obtFiltro()
    {
        ConnectionDataBase conexion = new ConnectionDataBase();
        Configuracion conf = new Configuracion();
        String filtro = String.Empty;

        if (ddlLinea.SelectedItem != null && ddlLinea.SelectedIndex != 0)
            filtro += " and S.TIPOCREDITO = " + ddlLinea.SelectedValue;
        if (ddlPeriodicidad.SelectedItem != null && ddlPeriodicidad.SelectedIndex != 0)
            filtro += " and S.PERIODICIDAD = " + ddlPeriodicidad.SelectedValue;

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
                    filtro += " and (V.Cod_Asesor = " + ddlAsesores.SelectedValue + " or V.Cod_Asesor is null or V.Cod_Asesor = '' or v.Cod_asesor not in (select icodigo from Asejecutivos where Iestado = 1))";
                else
                    filtro += " and V.Cod_Asesor = " + ddlAsesores.SelectedValue + " ";
            }

        }
        else if (chkSinAsesor.Checked)
            filtro += " and (V.Cod_Asesor = " + _usuario.codusuario + " or V.Cod_Asesor is null or V.Cod_Asesor = '' or v.Cod_asesor not in (select icodigo from Asejecutivos where Iestado = 1))";
        else
            filtro += " and V.Cod_Asesor = " + _usuario.codusuario + " ";


        filtro += " and S.estado = 0 and S.CONCEPTO like 'SOLICITUD ATENCION AL CLIENTE%' ";

        if (!string.IsNullOrEmpty(filtro))
        {
            filtro = filtro.Substring(4);
            filtro = "where " + filtro;
        }

        if (ddlZona.SelectedValue != "")
        {
            if(ddlZona.SelectedValue == "0")
                filtro += " and v.cod_zona is null";
            else
            filtro += " and v.cod_zona = " + ddlZona.SelectedValue + " ";
        }

        return filtro;
    }


    protected Boolean ValidarDatos()
    {
        if (gvLista.Rows.Count == 0)
        {
            VerError("No existen datos por registrar, verifique los datos.");
            return false;
        }
        int cont = 0;
        foreach (GridViewRow rFila in gvLista.Rows)
        {
            CheckBox cbSeleccionar = (CheckBox)rFila.FindControl("cbSeleccionar");
            if (cbSeleccionar != null)
            {
                if (cbSeleccionar.Checked)
                    cont++;
            }
        }
        if (cont == 0)
        {
            VerError("No existen datos seleccionados para modificar, verifique los datos.");
            return false;
        }
        return true;
    }

    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        if (ValidarDatos())
        {
            Session["TIPO"] = "GRABAR";
            ctlMensaje.MostrarMensaje("Desea realizar la actualización de datos de las personas seleccionadas?");
        }
    }


    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        if (Session["TIPO"] != null)
        {
            if (Session["TIPO"].ToString() == "GRABAR")
            {
                Usuario pUsuario = (Usuario)Session["Usuario"];
                //OBTENER LOS DATOS POR ACTUALIZAR
                List<SolicitudCreditoAAC> lstActualizar = new List<SolicitudCreditoAAC>();
                lstActualizar = ObtenerListaSolicitud();
                if (lstActualizar == null)
                    return;
                //ejecutar metodo de actualizar los datos
                string pError = "";
                List<Credito> lstGenerados = new List<Credito>();
                SolicitudCreditoServicio.ConfirmacionSolicitudCredito(lstActualizar, ref pError, ref lstGenerados, pUsuario);
                if (pError.Trim() != "")
                {
                    VerError(pError);
                    return;
                }
                panelGrid.Visible = false;
                if (lstGenerados.Count > 0)
                {
                    panelGrid.Visible = true;
                    gvGenerados.DataSource = lstGenerados;
                    gvGenerados.DataBind();
                }



                #region Interacciones WM

                foreach (Credito nSolicitud in lstGenerados)
                {
                    Xpinn.FabricaCreditos.Entities.DatosSolicitud datosSolicitud = new Xpinn.FabricaCreditos.Entities.DatosSolicitud();

                    // Parametro general para habilitar proceso de WM                  
                        Xpinn.FabricaCreditos.Entities.ControlCreditos vControlCreditos = new Xpinn.FabricaCreditos.Entities.ControlCreditos();
                        vControlCreditos.numero_radicacion = Convert.ToInt64(nSolicitud.numero_radicacion);
                        vControlCreditos.codtipoproceso = ControlCreditosServicio.obtenerCodTipoProceso("S", (Usuario)Session["usuario"]);
                        vControlCreditos.fechaproceso = Convert.ToDateTime(DateTime.Now);
                        vControlCreditos.cod_persona = pUsuario.codusuario;
                        vControlCreditos.cod_motivo = 0;
                        vControlCreditos.anexos = null;
                        vControlCreditos.nivel = 0;
                        vControlCreditos = ControlCreditosServicio.CrearControlCreditos(vControlCreditos, (Usuario)Session["usuario"]);
                }



                #endregion

                Site toolBar = (Site)Master;
                toolBar.MostrarGuardar(false);
                toolBar.MostrarLimpiar(false);
                mvPrincipal.ActiveViewIndex = 1;
                Session.Remove("TIPO");
            }
            else if (Session["TIPO"].ToString() == "ELIMINAR")
            {
                Int64 idELiminar = 0;
                if (Session["ID"] != null)
                {
                    idELiminar = Convert.ToInt64(Session["ID"].ToString());
                    Session.Remove("ID");
                    //llamar metodo de eliminación
                    SolicitudCreditoServicio.EliminarSolicitudCreditoAAC(idELiminar, (Usuario)Session["usuario"]);
                    Actualizar();
                }
            }
        }
    }


    protected List<SolicitudCreditoAAC> ObtenerListaSolicitud()
    {
        List<SolicitudCreditoAAC> lstResultado = new List<SolicitudCreditoAAC>();
        foreach (GridViewRow rFila in gvLista.Rows)
        {
            CheckBox cbSeleccionar = (CheckBox)rFila.FindControl("cbSeleccionar");
            if (cbSeleccionar != null)
            {
                if (cbSeleccionar.Checked)
                {
                    SolicitudCreditoAAC pEntidad = new SolicitudCreditoAAC();
                    Int64 pNumCredito = Convert.ToInt64(gvLista.DataKeys[rFila.RowIndex].Values[0].ToString());
                    pEntidad.numerosolicitud = pNumCredito;
                    if (gvLista.DataKeys[rFila.RowIndex].Values[1] != null)
                        pEntidad.cod_persona = Convert.ToInt64(gvLista.DataKeys[rFila.RowIndex].Values[1].ToString());
                    else
                    {
                        VerError("Error: Fila " + (rFila.RowIndex + 1) + " , Debe confirmar la Solicitud de la persona con identificación : " + rFila.Cells[5].Text.Trim());
                        return null;
                    }
                    lstResultado.Add(pEntidad);
                }
            }
        }
        return lstResultado;
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
            BOexcepcion.Throw(SolicitudCreditoServicio.CodigoprogramaSoliciCredi, "gvLista_PageIndexChanging", ex);
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
            Session["TIPO"] = "ELIMINAR";
            Session["ID"] = id; 
            ctlMensaje.MostrarMensaje("Seguro que desea eliminar este registro?");
        }
        catch
        {
            VerError("no puede borar el escalafon por que ya hay personas con este escalafon salarial");
        }
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