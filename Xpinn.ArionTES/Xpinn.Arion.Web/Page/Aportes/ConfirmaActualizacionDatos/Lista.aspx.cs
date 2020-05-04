using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using Xpinn.Aportes.Services;
using Xpinn.Aportes.Entities;
using System.Text;
using System.IO;
using System.Web.UI.HtmlControls;
using Xpinn.Asesores.Entities;
using Xpinn.Asesores.Services;
using Xpinn.FabricaCreditos.Entities;
using Xpinn.FabricaCreditos.Services;

partial class Lista : GlobalWeb
{

    AfiliacionServices AfiliacionServicio = new AfiliacionServices();
    Xpinn.FabricaCreditos.Services.Persona1Service BOPersona = new Xpinn.FabricaCreditos.Services.Persona1Service();
    ParametrosAfiliacionServices BOActualizar = new ParametrosAfiliacionServices();
    PoblarListas poblarLista = new PoblarListas();
    Usuario _usuario;

    protected void Page_PreInit(object sender, EventArgs e)
    { 
        try
        {

            VisualizarOpciones(AfiliacionServicio.codigoprogramaConfirmarData, "L");
            
            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
            ctlMensaje.eventoClick += btnContinuarMen_Click;
           
            toolBar.MostrarGuardar(false);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AfiliacionServicio.codigoprogramaConfirmarData, "Page_PreInit", ex);
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
                Session[Usuario.codusuario + "DTSolicitud"] = null;
                txtFecha.Text = DateTime.Now.ToString("d");
                mvPrincipal.ActiveViewIndex = 0;
                CargarDropDown();
                LimpiarData();
                cargarCombos();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AfiliacionServicio.codigoprogramaConfirmarData, "Page_Load", ex);
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

    protected void LimpiarData()
    {
        VerError("");
        txtFechaSoli.Text = string.Empty;
        pDatos.Visible = false;
        lblTotalRegs.Visible = false;
        lblInfo.Visible = false;
        gvLista.DataSource = null;        
        gvLista.DataBind();
        LimpiarValoresConsulta(pConsulta, AfiliacionServicio.codigoprogramaConfirmarData);
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
            if (txtFecha.Text == "")
            {
                VerError("Ingrese la fecha de modificación.");
                return;
            }
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
        poblarLista.PoblarListaDesplegable("OFICINA", "cod_oficina,nombre", "estado = 1", "1", ddlOficinas, (Usuario)Session["usuario"]);
    }


    private void Actualizar(List<PersonaActualizacion> lstSolicitud = null)
    {
        try
        {
            List<PersonaActualizacion> lstConsulta;
            if (lstSolicitud != null)
                lstConsulta = lstSolicitud;
            else
            {
                String filtro = obtFiltro();
                lstConsulta = AfiliacionServicio.ListarDataPersonasXactualizar(filtro, (Usuario)Session["usuario"]);
            }

            gvLista.PageSize = 15;
            String emptyQuery = "Fila de datos vacia";
            gvLista.EmptyDataText = emptyQuery;
            gvLista.DataSource = lstConsulta;
            Session[Usuario.codusuario + "DTSolicitud"] = lstConsulta;
            Site toolBar = (Site)this.Master;
            
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
            BOexcepcion.Throw(AfiliacionServicio.codigoprogramaConfirmarData, "Actualizar", ex);
        }
    }


    private string obtFiltro()
    {
        Configuracion conf = new Configuracion();

        String filtro = String.Empty;

        if (txtFechaSoli.Text.Trim() != "")
            filtro += " and P.FECHA_SOLICITUD = to_date('" + txtFechaSoli.Text + "', '" + gFormatoFecha + "')";
        if (txtIdentificacion.Text.Trim() != "")
            filtro += " and v.identificacion = '" + txtIdentificacion.Text + "'";
        if (txtNombres.Text.Trim() != "")
            filtro += " and v.nombres like '%" + txtNombres.Text.Trim() + "%'";
        if (txtApellidos.Text.Trim() != "")
            filtro += " and v.apellidos like '%" + txtApellidos.Text.Trim() + "%'";
        if (ddlOficinas.SelectedIndex != 0)
            filtro += " and v.cod_oficina = " + ddlOficinas.SelectedValue;
        if (txtCodigoNomina.Text.Trim() != "")
            filtro += " and v.cod_nomina = '" + txtCodigoNomina.Text.Trim() + "'";
        if (ddlZona.SelectedValue != "")
        {
            if (ddlZona.SelectedValue == "0")
                filtro += " and v.cod_zona is null";
            else
                filtro += " and v.cod_zona = " + ddlZona.SelectedValue + " ";
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
                    filtro += " and (V.Cod_Asesor = " + ddlAsesores.SelectedValue + " or V.Cod_Asesor is null or V.Cod_Asesor = '' or v.Cod_asesor not in (select icodigo from Asejecutivos where Iestado = 1))";
                else
                    filtro += " and V.Cod_Asesor = " + ddlAsesores.SelectedValue + " ";
            }
        }
        else if (chkSinAsesor.Checked)
            filtro += " and (V.Cod_Asesor = " + _usuario.codusuario + " or V.Cod_Asesor is null or V.Cod_Asesor = '' or v.Cod_asesor not in (select icodigo from Asejecutivos where Iestado = 1))";
        else
            filtro += " and V.Cod_Asesor = " + _usuario.codusuario + " ";

        filtro += " and P.estado = 0";

        if (!string.IsNullOrEmpty(filtro))
        {
            filtro = filtro.Substring(4);
            filtro = "where " + filtro;
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
                List<PersonaActualizacion> lstActualizar = new List<PersonaActualizacion>();
                lstActualizar = ObtenerListaActualizacion();
                //ejecutar metodo de actualizar los datos
                string pError = "";
                BOActualizar.ModificarPersona_Actualizacion(ref pError, lstActualizar, pUsuario);
                if (pError.Trim() != "")
                {
                    VerError(pError);
                    return;
                }
                Site toolBar = (Site)Master;
                toolBar.MostrarGuardar(false);
                toolBar.MostrarLimpiar(false);
                mvPrincipal.ActiveViewIndex = 1;
                Session.Remove("TIPO");
            }
            else if (Session["TIPO"].ToString() == "ELIMINAR")
            {
                decimal idELiminar = 0;
                if (Session["ID"] != null)
                {
                    idELiminar = Convert.ToDecimal(Session["ID"].ToString());
                    //llamar metodo de eliminación
                    BOActualizar.EliminarPersona_Actualizacion(idELiminar, (Usuario)Session["usuario"]);
                    Actualizar();
                }
            }
        }
    }


    protected List<PersonaActualizacion> ObtenerListaActualizacion()
    {
        List<PersonaActualizacion> lstResultado = new List<PersonaActualizacion>();
        string primer_nombre, segundo_nombre, primer_ape, segundo_ape, direcion, telefono, email, celular;
        PersonaActualizacion pEntidad;
        foreach (GridViewRow rFila in gvLista.Rows)
        {
            CheckBox cbSeleccionar = (CheckBox)rFila.FindControl("cbSeleccionar");
            if (cbSeleccionar != null)
            {
                if (cbSeleccionar.Checked)
                {
                    pEntidad = new PersonaActualizacion();

                    Int64 pConsec = Convert.ToInt64(gvLista.DataKeys[rFila.RowIndex].Value);
                    Int64 pCod_persona = Convert.ToInt64(rFila.Cells[4].Text.Trim());
                    Xpinn.FabricaCreditos.Entities.Persona1 pPersona = new Xpinn.FabricaCreditos.Entities.Persona1();
                    pPersona = BOPersona.ConsultarPersona1(pCod_persona, (Usuario)Session["usuario"]);

                    pEntidad.idconsecutivo = pConsec;
                    pEntidad.cod_persona = pCod_persona;
                    primer_nombre = ""; segundo_nombre = ""; primer_ape = ""; segundo_ape = ""; direcion = ""; telefono = ""; email = ""; celular = "";
                    Int64 codCiudadResiden = 0;
                    int codCiudadEmp = 0;

                    if (rFila.Cells[6].Text != "&nbsp;" && rFila.Cells[6].Text.Trim() != "")
                        primer_nombre = rFila.Cells[6].Text.Trim();
                    else
                        primer_nombre = pPersona.primer_nombre;

                    if (rFila.Cells[7].Text != "&nbsp;" && rFila.Cells[7].Text.Trim() != "")
                        segundo_nombre = rFila.Cells[7].Text.Trim();
                    else
                        segundo_nombre = pPersona.segundo_nombre;

                    if (rFila.Cells[8].Text != "&nbsp;" && rFila.Cells[8].Text.Trim() != "")
                        primer_ape = rFila.Cells[8].Text.Trim();
                    else
                        primer_ape = pPersona.primer_apellido;

                    if (rFila.Cells[9].Text != "&nbsp;" && rFila.Cells[9].Text.Trim() != "")
                        segundo_ape = rFila.Cells[9].Text.Trim();
                    else
                        segundo_ape = pPersona.segundo_apellido;

                    pEntidad.primer_nombre = primer_nombre;
                    pEntidad.segundo_nombre = segundo_nombre;
                    pEntidad.primer_apellido = primer_ape;
                    pEntidad.segundo_apellido = segundo_ape;

                    pPersona.codciudadresidencia = pPersona.codciudadresidencia == null ? 0 : pPersona.codciudadresidencia;
                    codCiudadResiden = rFila.Cells[10].Text != "&nbsp;" && rFila.Cells[10].Text.Trim() != "" ? Convert.ToInt64(rFila.Cells[10].Text.Trim()) : Convert.ToInt64(pPersona.codciudadresidencia);
                    pEntidad.codciudadresidencia = codCiudadResiden;

                    direcion = rFila.Cells[12].Text != "&nbsp;" && rFila.Cells[12].Text.Trim() != "" ? rFila.Cells[12].Text.Trim() : pPersona.direccion;
                    pEntidad.direccion = direcion;

                    telefono = rFila.Cells[13].Text != "&nbsp;" && rFila.Cells[13].Text.Trim() != "" ? rFila.Cells[13].Text.Trim() : pPersona.telefono;
                    pEntidad.telefono = telefono;
                    celular = rFila.Cells[14].Text != "&nbsp;" && rFila.Cells[14].Text.Trim() != "" ? rFila.Cells[14].Text.Trim() : pPersona.celular;
                    pEntidad.celular = celular;
                    //Datos Empresa
                    pPersona.ciudad = pPersona.ciudad == null ? 0 : pPersona.ciudad;
                    codCiudadEmp = rFila.Cells[15].Text != "&nbsp;" && rFila.Cells[15].Text.Trim() != "" ? Convert.ToInt32(rFila.Cells[15].Text.Trim()) : Convert.ToInt32(pPersona.ciudad);
                    pEntidad.ciudadempresa = codCiudadEmp; 

                    direcion = ""; telefono = "";
                    direcion = rFila.Cells[17].Text != "&nbsp;" && rFila.Cells[17].Text.Trim() != "" ? rFila.Cells[17].Text.Trim() : pPersona.direccionempresa;
                    pEntidad.direccionempresa = direcion;

                    telefono = rFila.Cells[18].Text != "&nbsp;" && rFila.Cells[18].Text.Trim() != "" ? rFila.Cells[18].Text.Trim() : pPersona.telefonoempresa;
                    pEntidad.telefonoempresa = telefono;

                    email = rFila.Cells[19].Text != "&nbsp;" && rFila.Cells[19].Text.Trim() != "" ? rFila.Cells[19].Text.Trim() : pPersona.email;
                    pEntidad.email = email;
                    pEntidad.estado = 1;
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
            if (Session[Usuario.codusuario + "DTSolicitud"] != null)
            {
                var lstResult = (List<PersonaActualizacion>)Session[Usuario.codusuario + "DTSolicitud"];
                Actualizar(lstResult);
            }
            else
                Actualizar();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AfiliacionServicio.codigoprogramaConfirmarData, "gvLista_PageIndexChanging", ex);
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



    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        int Index = e.NewEditIndex;
        if(Session[Usuario.codusuario + "DTSolicitud"] != null)
        {
            if (Index >= 0)
            {
                List<PersonaActualizacion> lstSolicitudSelect = (List<PersonaActualizacion>)Session[Usuario.codusuario + "DTSolicitud"];
                var lstResult = new List<PersonaActualizacion>{
                    lstSolicitudSelect[Index]
                };
                frvData.DataSource = lstResult;
                frvData.DataBind();
                mpeShowDetail.Show();
            }
        }
        e.NewEditIndex = -1;
    }

    protected void frvData_DataBound(object sender, EventArgs e)
    {
        Label lblFecSoliModal = (Label)frvData.FindControl("lblFecSoliModal");
        if (lblFecSoliModal != null)
        {
            if (!string.IsNullOrWhiteSpace(lblFecSoliModal.Text))
            {
                if (Convert.ToDateTime(lblFecSoliModal.Text) == DateTime.MinValue)
                    lblFecSoliModal.Text = "";
            }
        }
    }
    protected void btnExportar_Click(object sender, EventArgs e)
    {
        VerError("");
        if (gvLista.Rows.Count > 0 )
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