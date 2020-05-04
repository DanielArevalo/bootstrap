using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.FabricaCreditos.Entities;
using Xpinn.Nomina.Entities;
using Xpinn.Nomina.Services;
using Xpinn.Util;


public partial class Nuevo : GlobalWeb
{
    EmpleadoService _empleadoService = new EmpleadoService();
    String _codPersona;
    string _codEmpleado;


    #region Eventos Iniciales


    public void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            if (Session[_empleadoService.CodigoPrograma + ".id"] == null)
            {
                VisualizarOpciones(_empleadoService.CodigoPrograma, "A");
            }
            else
            {
                VisualizarOpciones(_empleadoService.CodigoPrograma, "D");
            }

            Site toolBar = (Site)Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoRegresar += (s, evt) =>
            {
                Session.Remove(_empleadoService.CodigoPrograma + ".id");
                Navegar("Lista.aspx");
            };
            //toolBar.eventoRegresar += (s, evt) =>
            //{
            //    Session.Remove(_empleadoService.CodigoPrograma + ".id");
            //    Session.Remove(_empleadoService.CodigoPrograma + ".idEmpleado");
            //    Navegar(Pagina.Lista);
            //};
            toolBar.MostrarGuardar(false);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_empleadoService.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    public void Page_Load(object sender, EventArgs e)
    {
        try
        {
            _codPersona = Session[_empleadoService.CodigoPrograma + ".id"] != null ? Session[_empleadoService.CodigoPrograma + ".id"].ToString() : default(string);
            _codEmpleado = Session[_empleadoService.CodigoPrograma + ".idEmpleado"] != null ? Session[_empleadoService.CodigoPrograma + ".idEmpleado"].ToString() : default(string);

            if (!IsPostBack)
            {
                InicializarPagina();

                if (Session[_empleadoService.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[_empleadoService.CodigoPrograma + ".id"].ToString();
                    ObtenerDatos(idObjeto);
                    idObjeto = "";
                }
            }
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }


    /// <summary>
    /// Iniciarlizar las listas desplegables
    /// </summary>
    private void InicializarPagina()
    {
        LlenarListasDesplegables(TipoLista.TipoIdentificacion, ddlTipoE);
        LlenarListasDesplegables(TipoLista.EstadoCivil, ddlEstadoCivil);
        LlenarListasDesplegables(TipoLista.Oficinas, ddlOficina);
        LlenarListasDesplegables(TipoLista.Barrio, ddlBarrioCorrespondencia);
        LlenarListasDesplegables(TipoLista.Actividad2, ddlActividadE);
        LlenarListasDesplegables(TipoLista.NivelEscolaridad, ddlNivelEscolaridad);

        LlenarListasDesplegables(TipoLista.Lugares, ddlLugarExpedicion, ddlLugarNacimiento, ddlCiuCorrespondencia);
        //LlenarListasDesplegables(TipoLista.Lugares, ddlLugarExpedicion, ddlLugarNacimiento, ddlCiuCorrespondencia, ddlCiu0);
        //LlenarListasDesplegables(TipoLista.TipoCargo, ddlCargo);
        //LlenarListasDesplegables(TipoLista.TipoContrato, ddlTipoContrato);
        //LlenarListasDesplegables(TipoLista.NominaEmpleado, ddlNomina);
        //LlenarListasDesplegables(TipoLista.Actividad_Negocio, ddlActividadE0);

        gvExperienciaLaboral.DataSource = new List<Experiencia_Laboral> { new Experiencia_Laboral() };
        gvExperienciaLaboral.DataBind();

        gvInformacionAcademica.DataSource = new List<Empleado_Estudios> { new Empleado_Estudios() };
        gvInformacionAcademica.DataBind();

        gvfam.DataSource = new List<Empleado_Familiar> { new Empleado_Familiar() };
        gvfam.DataBind();
    }



    #endregion


    #region Eventos Intermedios

    void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        Page.Validate();
        if (ValidarCampos() && Page.IsValid)
        {
            GuardarDatos();

        }
    }

    public void txtIdentificacionE_TextChanged(object sender, EventArgs e)
    {
        try
        {
            VerError("");
            Persona1 vPersona1 = new Persona1();
            Xpinn.FabricaCreditos.Services.Persona1Service persona1Servicio = new Xpinn.FabricaCreditos.Services.Persona1Service();
            vPersona1.seleccionar = "Identificacion";
            vPersona1.noTraerHuella = 1;
            vPersona1.identificacion = txtIdentificacionE.Text;
            vPersona1 = persona1Servicio.ConsultarPersona1Param(vPersona1, (Usuario)Session["usuario"]);

            if (vPersona1 != null && vPersona1.cod_persona != 0)
            {
                _codPersona = vPersona1.cod_persona.ToString();
                Session[_empleadoService.CodigoPrograma + ".id"] = _codPersona;
                VerError("La persona ya existe, Consultela y edite los datos");
                Site toolBar1 = (Site)Master;
                toolBar1.MostrarGuardar(true);
                pnlInformacionPersona.Visible = false;
                Panel1.Visible = false;
                txtIdentificacionE.Enabled = false;
                //  ObtenerDatos(_codPersona);
            }
            else
            {
                VerError("La persona no existe, se va a crear una nueva persona");
                _codPersona = string.Empty;
                Site toolBar = (Site)Master;
                toolBar.MostrarGuardar(true);
                pnlInformacionPersona.Visible = true;
                txtIdentificacionE.Enabled = false;
            }

         
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }

    public void rblTipoVivienda_SelectedIndexChanged(object sender, EventArgs e)
    {
        validarArriendo();
    }


    public void txtFechanacimiento_TextChanged(object sender, EventArgs e)
    {
        CalcularEdad();
    }


    #endregion


    #region Metodos Eventos GridView


    protected void gvInformacionAcademica_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Empleado_Estudios estudios = e.Row.DataItem as Empleado_Estudios;

            DropDownList ddlItemhorarioestudio = e.Row.FindControl("ddlItemhorarioestudio") as DropDownList;

            if (estudios.horario_estudio.HasValue)
            {
                ddlItemhorarioestudio.SelectedValue = estudios.horario_estudio.ToString();
            }

            DropDownList ddlItemhorarititulo = e.Row.FindControl("ddlItemhorarititulo") as DropDownList;

            if (estudios.horario_titulo.HasValue)
            {
                ddlItemhorarititulo.SelectedValue = estudios.horario_titulo.ToString();
            }

            CheckBox chkItemEstudia = e.Row.FindControl("chkItemEstudia") as CheckBox;

            chkItemEstudia.Checked = Convert.ToBoolean(estudios.estudia);
        }
    }

    protected void gvExperienciaLaboral_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownList ddlItemCargo = e.Row.FindControl("ddlItemCargo") as DropDownList;
            LlenarListasDesplegables(TipoLista.TipoCargo, ddlItemCargo);

            Experiencia_Laboral experiencia = e.Row.DataItem as Experiencia_Laboral;
            if (experiencia.codcargo.HasValue)
            {
                ddlItemCargo.SelectedValue = experiencia.codcargo.Value.ToString();
            }
        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            DropDownList ddlFooterCargo = e.Row.FindControl("ddlFooterCargo") as DropDownList;
            LlenarListasDesplegables(TipoLista.TipoCargo, ddlFooterCargo);
        }
    }

    protected void gvfam_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Empleado_Familiar familiarEmpleado = e.Row.DataItem as Empleado_Familiar;

            DropDownList ddlItemparenteco = e.Row.FindControl("ddlItemparenteco") as DropDownList;

            LlenarListasDesplegables(TipoLista.Parentesco, ddlItemparenteco);

            if (!string.IsNullOrWhiteSpace(familiarEmpleado.parentezco))
            {
                ddlItemparenteco.SelectedValue = familiarEmpleado.parentezco;
            }

            DropDownList ddlItemtipoidentificacion = e.Row.FindControl("ddlItemtipoidentificacion") as DropDownList;

            LlenarListasDesplegables(TipoLista.TipoIdentificacion, ddlItemtipoidentificacion);

            if (!string.IsNullOrWhiteSpace(familiarEmpleado.tipoidentificacion))
            {
                ddlItemtipoidentificacion.SelectedValue = familiarEmpleado.tipoidentificacion;
            }

            DropDownList ddlItemconvive = e.Row.FindControl("ddlItemconvive") as DropDownList;

            if (!string.IsNullOrWhiteSpace(familiarEmpleado.convivefamiliar))
            {
                ddlItemconvive.SelectedValue = familiarEmpleado.convivefamiliar;
            }
        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            DropDownList ddlFooterparenteco = e.Row.FindControl("ddlFooterparenteco") as DropDownList;
            LlenarListasDesplegables(TipoLista.Parentesco, ddlFooterparenteco);

            DropDownList ddlFootertipoidentificacion = e.Row.FindControl("ddlFootertipoidentificacion") as DropDownList;
            LlenarListasDesplegables(TipoLista.TipoIdentificacion, ddlFootertipoidentificacion);
        }
    }

    protected void gvfam_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("AddNewf"))
        {
            TextBox pnombrefamiliar = (TextBox)gvfam.FooterRow.FindControl("txtnombrefamiliar");
            DropDownList pparentezco = (DropDownList)gvfam.FooterRow.FindControl("ddlFooterparenteco");
            DropDownList ptipoidentificacion = (DropDownList)gvfam.FooterRow.FindControl("ddlFootertipoidentificacion");
            TextBox pidentificacion = (TextBox)gvfam.FooterRow.FindControl("txtidentificacionfamiliar");
            TextBox pprofesion = (TextBox)gvfam.FooterRow.FindControl("txtprofesion");
            DropDownList pconvive = (DropDownList)gvfam.FooterRow.FindControl("ddlconvive");

         


            if (pnombrefamiliar.Text == string.Empty)
            {
                pnombrefamiliar.Focus();
                return;
            }

            if (pidentificacion.Text == string.Empty)
            {
                pidentificacion.Focus();
                return;
            }
            if (pprofesion.Text == String.Empty)
            {
                pidentificacion.Focus();
                return;
            }

            if (pconvive.Text == "Seleccione Un Item")
            {
                return;
            }
            if (pparentezco.SelectedValue == "Seleccione Un Item")
            {
                return;
            }
            if (ptipoidentificacion.SelectedValue == "Seleccione Un Item")
            {
                return;
            }

            Empleado_Familiar pdetalle = new Empleado_Familiar();





            pdetalle.nombrefamiliar = pnombrefamiliar.Text;
            pdetalle.parentezco = pparentezco.SelectedValue;
            pdetalle.tipoidentificacion = ptipoidentificacion.SelectedValue;
            pdetalle.identificacionfamiliar = pidentificacion.Text;
            pdetalle.profesion = pprofesion.Text;
            pdetalle.convivefamiliar = pconvive.SelectedValue;

            this.GuardarListafam(pdetalle);

            this.gvfam.DataSource = this.Obtenerlistafam();
            this.gvfam.DataBind();
        }
        else if (e.CommandName == "Delete")
        {
            int index = ((GridViewRow)(((ImageButton)e.CommandSource).NamingContainer)).RowIndex;
            long consecutivo = Convert.ToInt64(gvfam.DataKeys[index].Value.ToString());
            if (consecutivo > 0)
            {
                Empleado_FamiliarService empFamiliaresService = new Empleado_FamiliarService();

                    empFamiliaresService.EliminarEmpleado_Familiar(consecutivo, Usuario);
                }
            

            List<Empleado_Familiar> listaEmpleadoFamiliar = ObtenerFamiliares();
            listaEmpleadoFamiliar.RemoveAt(index);

            if (listaEmpleadoFamiliar.Count <= 0)
            {
                listaEmpleadoFamiliar.Add(new Empleado_Familiar());
            }

            gvfam.DataSource = listaEmpleadoFamiliar;
            gvfam.DataBind();
        }
    }

    protected void gvInformacionAcademica_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("AddNew"))
        {
            TextBox psemestre = (TextBox)gvInformacionAcademica.FooterRow.FindControl("txtsemenestre");
            TextBox pprofesion = (TextBox)gvInformacionAcademica.FooterRow.FindControl("txtprofesion");
            DropDownList phorarioestudio = (DropDownList)gvInformacionAcademica.FooterRow.FindControl("ddlhorarioestudio");
            ASP.general_controles_ctlfecha_ascx pfechainicio = (ASP.general_controles_ctlfecha_ascx)gvInformacionAcademica.FooterRow.FindControl("ctlFechainicio");
            TextBox ptituloobtenido = (TextBox)gvInformacionAcademica.FooterRow.FindControl("txttitulo");
            TextBox pestablecimiento = (TextBox)gvInformacionAcademica.FooterRow.FindControl("txtestablecimiento");
            ASP.general_controles_ctlfecha_ascx pfechaterminacion = (ASP.general_controles_ctlfecha_ascx)gvInformacionAcademica.FooterRow.FindControl("ctlFechatermino");
            DropDownList phorariotitulo = (DropDownList)gvInformacionAcademica.FooterRow.FindControl("ddlhorarititulo");
            CheckBox checkboxEstudia = (CheckBox)gvInformacionAcademica.FooterRow.FindControl("chkFooterEstudia");

            if (psemestre.Text == string.Empty)
            {
                psemestre.Focus();
                return;
            }

            if (pprofesion.Text == string.Empty)
            {
                pprofesion.Focus();
                return;
            }

            if (ptituloobtenido.Text == string.Empty)
            {
                ptituloobtenido.Focus();
                return;
            }

            if (pestablecimiento.Text == string.Empty)
            {
                pestablecimiento.Focus();
                return;
            }
            if (phorarioestudio.SelectedValue == "0")
            {
                return;
            }
            if (phorariotitulo.SelectedValue == "0")
            {
                return;
            }

            Empleado_Estudios pdetalle = new Empleado_Estudios();
            pdetalle.semestre = psemestre.Text;
            pdetalle.profesion = pprofesion.Text;
            pdetalle.horario_estudio = Convert.ToInt64(phorarioestudio.Text);
            pdetalle.fecha_inicio = Convert.ToDateTime(pfechainicio.Text);
            pdetalle.titulo_obtenido = ptituloobtenido.Text;
            pdetalle.establecimiento = pestablecimiento.Text;
            pdetalle.fecha_terminacion = Convert.ToDateTime(pfechaterminacion.Text);
            pdetalle.horario_titulo = Convert.ToInt64(phorariotitulo.Text);
            pdetalle.estudia = checkboxEstudia.Checked ? 1 : 0;

            this.GuardarLista(pdetalle);

            this.gvInformacionAcademica.DataSource = this.Obtenerlista();
            this.gvInformacionAcademica.DataBind();
        }
        else if (e.CommandName == "Delete")
        {
            int index = ((GridViewRow)(((ImageButton)e.CommandSource).NamingContainer)).RowIndex;
            long consecutivo = Convert.ToInt64(gvInformacionAcademica.DataKeys[index].Value.ToString());
            if (consecutivo > 0)
            {
                Empleado_EstudiosService empService = new Empleado_EstudiosService();

                empService.EliminarEmpleado_Estudios(consecutivo, Usuario);
            }

            List<Empleado_Estudios> listaEstudios = ObtenerEstudios();
            listaEstudios.RemoveAt(index);

            if (listaEstudios.Count <= 0)
            {
                listaEstudios.Add(new Empleado_Estudios());
            }

            gvInformacionAcademica.DataSource = listaEstudios;
            gvInformacionAcademica.DataBind();
        }

    }

    protected void gvExperienciaLaboral_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("AddNewe"))
        {

            TextBox pnombreempresa = (TextBox)gvExperienciaLaboral.FooterRow.FindControl("txtnombre");
            DropDownList pcodcargo = (DropDownList)gvExperienciaLaboral.FooterRow.FindControl("ddlFooterCargo");
            TextBox pmotivoretiro = (TextBox)gvExperienciaLaboral.FooterRow.FindControl("txtmotivo");
            ASP.general_controles_ctlfecha_ascx pfecharetiro = (ASP.general_controles_ctlfecha_ascx)gvExperienciaLaboral.FooterRow.FindControl("ctlFecharetiro");
            ASP.general_controles_ctlfecha_ascx pfechaingreso = (ASP.general_controles_ctlfecha_ascx)gvExperienciaLaboral.FooterRow.FindControl("ctlFechaingreso");

            if (pnombreempresa.Text == string.Empty)
            {
                pnombreempresa.Focus();
                return;
            }

            if (pmotivoretiro.Text == string.Empty)
            {
                pmotivoretiro.Focus();
                return;
            }


            Experiencia_Laboral pdetalle = new Experiencia_Laboral();
            pdetalle.nombre_empresa = pnombreempresa.Text;
            pdetalle.codcargo = Convert.ToInt32(pcodcargo.SelectedValue);
            pdetalle.motivo_retiro = pmotivoretiro.Text;
            pdetalle.fecha_retiro = Convert.ToDateTime(pfecharetiro.Text);
            pdetalle.fecha_ingreso = Convert.ToDateTime(pfechaingreso.Text);


            this.GuardarListaExp(pdetalle);

            this.gvExperienciaLaboral.DataSource = this.ObtenerlistaExp();
            this.gvExperienciaLaboral.DataBind();
        }
        else if (e.CommandName == "Delete")
        {
            int index = ((GridViewRow)(((ImageButton)e.CommandSource).NamingContainer)).RowIndex;
            long consecutivo = Convert.ToInt64(gvExperienciaLaboral.DataKeys[index].Value.ToString());

            if (consecutivo > 0)
            {
                Experiencia_LaboralService empService = new Experiencia_LaboralService();

                empService.EliminarExperiencia_Laboral(consecutivo, Usuario);
            }

            List<Experiencia_Laboral> listaExperiencia = ObtenerExperiencia();

            if (listaExperiencia.Count <= 0)
            {
                listaExperiencia.Add(new Experiencia_Laboral());
            }

            gvExperienciaLaboral.DataSource = listaExperiencia;
            gvExperienciaLaboral.DataBind();
        }
    }


    #endregion


    #region Métodos de Ayuda


    private Empleados ConsultarEmpleado(string codPersona)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(codPersona)) return null;

            Empleados empleado = _empleadoService.ConsultarEmpleadosCodigoPersona(codPersona, Usuario);

            return empleado;
        }
        catch (Exception ex)
        {
            VerError("Error al consultar el empleado, " + ex.Message);
            return null;
        }
    }

    bool ValidarCampos()
    {
        Page.Validate();
        if (!Page.IsValid || string.IsNullOrWhiteSpace(txtIdentificacionE.Text) || string.IsNullOrWhiteSpace(txtFechaexpedicion.Text) 
            || string.IsNullOrWhiteSpace(txtCelular.Text) || string.IsNullOrWhiteSpace(txtTelefonoE.Text) || string.IsNullOrWhiteSpace(txtFechanacimiento.Text))
        {
            VerError("Faltan campos por validar!.");
            return false;
        }

        return true;
    }

    /// <summary>
    /// Determinar la edad de la persona con base en la fecha de nacimiento
    /// </summary>
    /// <param name="birthDate"></param>
    /// <returns></returns>
    public static int GetAge(DateTime birthDate)
    {
        return (int)Math.Floor((DateTime.Now - birthDate).TotalDays / 365.242199);
    }

    private void validarArriendo()
    {
        if (rblTipoVivienda.SelectedValue == "A")
        {
            txtArrendador.Enabled = true;
            txtTelefonoarrendador.Enabled = true;
            txtValorArriendo.Enabled = true;
            txtArrendador.Visible = true;
            txtTelefonoarrendador.Visible = true;
            txtValorArriendo.Visible = true;
        }
        else
        {
            txtArrendador.Enabled = false;
            txtTelefonoarrendador.Enabled = false;
            txtValorArriendo.Enabled = false;
            txtArrendador.Text = "";
            txtTelefonoarrendador.Text = "";
            txtValorArriendo.Text = "";
            txtArrendador.Visible = false;
            txtTelefonoarrendador.Visible = false;
            txtValorArriendo.Visible = false;
        }
    }

    /// <summary>
    /// Cálculo de la edad
    /// </summary>
    public void CalcularEdad()
    {
        if (!string.IsNullOrWhiteSpace(txtFechanacimiento.Text))
            txtEdadCliente.Text = Convert.ToString(GetAge(Convert.ToDateTime(txtFechanacimiento.Text)));
    }

    #endregion


    #region Metodos Obtener/Guardar Datos


    void GuardarDatos()
    {
        try
        {
            Xpinn.FabricaCreditos.Services.Persona1Service persona1Servicio = new Xpinn.FabricaCreditos.Services.Persona1Service();
            Xpinn.FabricaCreditos.Entities.Persona1 vPersona1 = new Xpinn.FabricaCreditos.Entities.Persona1();

            if (!string.IsNullOrWhiteSpace(_codPersona))
            {
                // Consultar datos ya existentes de la persona si se va a modificar
                vPersona1 = persona1Servicio.ConsultarPersona1(Convert.ToInt64(_codPersona), Usuario);
            }
            else
            {
                // Validar que la persona no exista si se va a crear
                vPersona1 = persona1Servicio.ConsultaDatosPersona(txtIdentificacionE.Text.Trim(), Usuario);
                if (vPersona1.cod_persona != 0)
                {
                    VerError("Ya existe una persona con la identificación dada");
                    return;
                }
            }

            vPersona1.origen = "Nomina";
            if (txtCod_persona.Text != "") vPersona1.cod_persona = Convert.ToInt64(txtCod_persona.Text.Trim());
            vPersona1.identificacion = (txtIdentificacionE.Text != "") ? Convert.ToString(txtIdentificacionE.Text.Trim()) : String.Empty;
            vPersona1.dirCorrespondencia = (txtDirCorrespondencia.Text != "") ? Convert.ToString(txtDirCorrespondencia.Text.Trim()) : String.Empty;
            vPersona1.barrioCorrespondencia = (ddlBarrioCorrespondencia.Text != "") ? Convert.ToInt64(ddlBarrioCorrespondencia.SelectedValue) : 0;
            vPersona1.telCorrespondencia = (txtTelCorrespondencia.Text != "") ? Convert.ToString(txtTelCorrespondencia.Text.Trim()) : String.Empty;
            vPersona1.ciuCorrespondencia = (ddlCiuCorrespondencia.Text != "") ? Convert.ToInt64(ddlCiuCorrespondencia.SelectedValue) : 0;
            if (string.Equals(rblTipo_persona.Text, "Jurídica"))
                vPersona1.tipo_persona = "J";
            else
                vPersona1.tipo_persona = "N";
            //if (txtDigito_verificacion.Text != "") vPersona1.digito_verificacion = Convert.ToInt64(txtDigito_verificacion.Text.Trim());
            if (ddlTipoE.Text != "") vPersona1.tipo_identificacion = Convert.ToInt64(ddlTipoE.SelectedValue);
            if (txtFechaexpedicion.Text != "") vPersona1.fechaexpedicion = Convert.ToDateTime(txtFechaexpedicion.Text.Trim());
            if (ddlLugarExpedicion.Text != "") vPersona1.codciudadexpedicion = Convert.ToInt64(ddlLugarExpedicion.SelectedValue);
            vPersona1.sexo = (rblSexo.Text != "") ? Convert.ToString(rblSexo.SelectedValue) : String.Empty;
            vPersona1.primer_nombre = (txtPrimer_nombreE.Text != "") ? Convert.ToString(txtPrimer_nombreE.Text.Trim().ToUpper()) : String.Empty;
            vPersona1.segundo_nombre = (txtSegundo_nombreE.Text != "") ? Convert.ToString(txtSegundo_nombreE.Text.Trim().ToUpper()) : String.Empty;
            vPersona1.primer_apellido = (txtPrimer_apellidoE.Text != "") ? Convert.ToString(txtPrimer_apellidoE.Text.Trim().ToUpper()) : String.Empty;
            vPersona1.segundo_apellido = (txtSegundo_apellidoE.Text != "") ? Convert.ToString(txtSegundo_apellidoE.Text.Trim().ToUpper()) : String.Empty;
            vPersona1.razon_social = String.Empty;
            if (txtFechanacimiento.Text != "") vPersona1.fechanacimiento = Convert.ToDateTime(txtFechanacimiento.Text.Trim());
            if (ddlLugarNacimiento.Text != "") vPersona1.codciudadnacimiento = Convert.ToInt64(ddlLugarNacimiento.SelectedValue);
            if (ddlEstadoCivil.Text != "") vPersona1.codestadocivil = Convert.ToInt64(ddlEstadoCivil.SelectedValue);
            if (ddlNivelEscolaridad.Text != "")
                vPersona1.codescolaridad = Convert.ToInt64(ddlNivelEscolaridad.SelectedValue);
            try { if (ddlActividadE.Text != "") vPersona1.codactividadStr = ddlActividadE.SelectedValue; }
            catch { }
            vPersona1.direccion = (txtDireccionE.Text != "") ? Convert.ToString(txtDireccionE.Text.Trim().ToUpper()) : String.Empty;
            vPersona1.codciudadresidencia = (ddlCiuCorrespondencia.Text != "") ? Convert.ToInt64(ddlCiuCorrespondencia.SelectedValue) : 0;
            vPersona1.telefono = (txtTelefonoE.Text != "") ? Convert.ToString(txtTelefonoE.Text.Trim()) : String.Empty;
            if (txtAntiguedadlugar.Text != "") vPersona1.antiguedadlugar = Convert.ToInt64(txtAntiguedadlugar.Text.Trim());
            vPersona1.tipovivienda = (rblTipoVivienda.Text != "") ? Convert.ToString(rblTipoVivienda.SelectedValue) : String.Empty;
            vPersona1.arrendador = (txtArrendador.Text != "") ? Convert.ToString(txtArrendador.Text.Trim()) : String.Empty;
            vPersona1.telefonoarrendador = (txtTelefonoarrendador.Text != "") ? Convert.ToString(txtTelefonoarrendador.Text.Trim()) : String.Empty;
            if (txtValorArriendo.Text != "") vPersona1.ValorArriendo = Convert.ToInt64(txtValorArriendo.Text.Trim().Replace(".", ""));
            vPersona1.celular = (txtCelular.Text != "") ? Convert.ToString(txtCelular.Text.Trim()) : String.Empty;
            vPersona1.email = (txtEmail.Text != "") ? Convert.ToString(txtEmail.Text.Trim()) : String.Empty;
            if (ddlOficina.SelectedValue != "") vPersona1.cod_oficina = Convert.ToInt64(ddlOficina.SelectedValue.Trim());
            vPersona1.fechacreacion = Convert.ToDateTime(DateTime.Now.ToString(GlobalWeb.gFormatoFecha));
            vPersona1.profecion = txtProfecion.Text;
            vPersona1.PersonasAcargo = Convert.ToInt32(txtPersonasCargo.Text);
            vPersona1.valor_afiliacion = 0;
            vPersona1.valor_afiliacion = 0;
            vPersona1.empleado_entidad = 1;
            vPersona1.zona = 0;
            //vPersona1.ActividadEconomicaEmpresaStr = ddlActividadE0.SelectedValue;
            //vPersona1.ciudad = Convert.ToInt64(ddlCiu0.SelectedValue);
            //vPersona1.relacionEmpleadosEmprender = Convert.ToInt32(ddlparentesco.SelectedValue);
            //vPersona1.CelularEmpresa = txtTelCell0.Text;
            //vPersona1.salario = !string.IsNullOrWhiteSpace(txtSueldo.Text) ? Convert.ToInt64(txtSueldo.Text) : 0;
            //vPersona1.fecha_ingresoempresa = !string.IsNullOrWhiteSpace(txtFechaContratacion1.Text) ? Convert.ToDateTime(txtFechaContratacion1.Text) : DateTime.MinValue;
            //vPersona1.empresa = (txtEmpresa.Text != "") ? Convert.ToString(txtEmpresa.Text.Trim().ToUpper()) : String.Empty;
            //vPersona1.telefonoempresa = (txtTelefonoempresa.Text != "") ? Convert.ToString(txtTelefonoempresa.Text.Trim()) : String.Empty;
            //vPersona1.direccionempresa = (txtDireccionEmpresa.Text != "") ? Convert.ToString(txtDireccionEmpresa.Text.Trim().ToUpper()) : String.Empty;
            //if (txtAntiguedadlugarEmpresa.Text != "") vPersona1.antiguedadlugarempresa = Convert.ToInt64(txtAntiguedadlugarEmpresa.Text.Trim());
            //if (ddlCargo.Text != "") vPersona1.codcargo = Convert.ToInt64(ddlCargo.Text.Trim());
            //if (ddlTipoContrato.Text != "") vPersona1.codtipocontrato = Convert.ToInt64(ddlTipoContrato.SelectedValue);

            try
            {
                vPersona1.Estrato = Convert.ToInt32(txtEstrato.Text);
            }
            catch
            {
                vPersona1.Estrato = 0;
            }
            vPersona1.usuariocreacion = Usuario.nombre;
            vPersona1.fecultmod = Convert.ToDateTime(DateTime.Now.ToString(GlobalWeb.gFormatoFecha));
            vPersona1.usuultmod = Usuario.nombre;

            if (string.IsNullOrWhiteSpace(_codPersona))
            {
                vPersona1 = persona1Servicio.CrearPersona1(vPersona1, Usuario);
                _codPersona = vPersona1.cod_persona.ToString();
                Session[_empleadoService.CodigoPrograma + ".id"] = _codPersona;

            }
            else
            {
                Persona1 persona = persona1Servicio.ModificarPersona1(vPersona1, Usuario);
            }

            Empleados empleado = ObtenerEntidadGuardar();
            if (empleado.consecutivo <= 0)
            {
                empleado = _empleadoService.CrearEmpleados(empleado, Usuario);
                _codEmpleado = empleado.consecutivo.ToString();
                Session[_empleadoService.CodigoPrograma + ".idEmpleado"] = _codEmpleado;
            }
            else
            {
                empleado = _empleadoService.ModificarEmpleados(empleado, Usuario);
            }

            // Grabar datos de estudios del empleado
            Empleado_EstudiosService estservice = new Empleado_EstudiosService();
            List<Empleado_Estudios> listaEstudios = ObtenerEstudios();

            foreach (Empleado_Estudios estudio in listaEstudios)
            {
                if (estudio.consecutivo <= 0 && !string.IsNullOrWhiteSpace(estudio.semestre) && !string.IsNullOrWhiteSpace(estudio.profesion) && !string.IsNullOrWhiteSpace(estudio.establecimiento))
                {
                    estservice.CrearEmpleado_Estudios(estudio, Usuario);
                }
            }

            // Grabar datos de los familiares del empleado
            Empleado_FamiliarService famservice = new Empleado_FamiliarService();
            List<Empleado_Familiar> listaFamiliares = ObtenerFamiliares();

            foreach (Empleado_Familiar familiares in listaFamiliares)
            {
                if (familiares.consecutivo <= 0 && !string.IsNullOrWhiteSpace(familiares.nombrefamiliar) && !string.IsNullOrWhiteSpace(familiares.identificacionfamiliar))
                {
                    famservice.CrearEmpleado_Familiar(familiares, Usuario);
                }
            }

            // Grabar datos de la experiencia laboral
            Experiencia_LaboralService expservice = new Experiencia_LaboralService();
            List<Experiencia_Laboral> listaExperiencia = ObtenerExperiencia();

            foreach (var experiencia in listaExperiencia)
            {
                if (experiencia.consecutivo <= 0 && !string.IsNullOrWhiteSpace(experiencia.nombre_empresa) && !string.IsNullOrWhiteSpace(experiencia.motivo_retiro) && experiencia.fecha_ingreso.HasValue)
                {
                    expservice.CrearExperiencia_Laboral(experiencia, Usuario);
                }
            }

            if (_codPersona != null)
            {
                mvFinal.ActiveViewIndex = 1;

                Site toolBar = (Site)Master;
                toolBar.MostrarRegresar(true);
                toolBar.MostrarCancelar(false);
                toolBar.MostrarGuardar(false);

                Session.Remove(_empleadoService.CodigoPrograma + ".id");
                Session.Remove(_empleadoService.CodigoPrograma + ".idEmpleado");
            }
        }
        catch (Exception ex)
        {
            VerError("Error al guardar el registro, " + ex.Message);
        }
    }

    public void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            Xpinn.FabricaCreditos.Services.Persona1Service persona1Servicio = new Xpinn.FabricaCreditos.Services.Persona1Service();
            Xpinn.FabricaCreditos.Entities.Persona1 vPersona1 = new Xpinn.FabricaCreditos.Entities.Persona1();
            Xpinn.Nomina.Services.Empleado_EstudiosService serviceestudio = new Xpinn.Nomina.Services.Empleado_EstudiosService();
            Xpinn.Nomina.Services.Experiencia_LaboralService serviceexperiencia = new Xpinn.Nomina.Services.Experiencia_LaboralService();
            Xpinn.Nomina.Services.Empleado_FamiliarService servicefamiliar = new Xpinn.Nomina.Services.Empleado_FamiliarService();

            // Cargar los datos del empleado 
            vPersona1.cod_persona = Convert.ToInt64(_codPersona);
            Int64 pCod_Persona = vPersona1.cod_persona;
            vPersona1.seleccionar = "Cod_persona";
            vPersona1.noTraerHuella = 1;
            vPersona1 = persona1Servicio.ConsultarPersona1Param(vPersona1, (Usuario)Session["usuario"]);

            if (vPersona1 != null)
            {


                #region persona


                if (vPersona1.cod_persona != Int64.MinValue)
                    txtCod_persona.Text = HttpUtility.HtmlDecode(vPersona1.cod_persona.ToString().Trim());
                if (!string.IsNullOrEmpty(vPersona1.dirCorrespondencia))
                {
                    try
                    {
                        txtDirCorrespondencia.Text = HttpUtility.HtmlDecode(vPersona1.dirCorrespondencia.ToString().Trim());
                        if (txtDirCorrespondencia.Text == "")
                            txtDirCorrespondencia.Text = HttpUtility.HtmlDecode(vPersona1.direccion.ToString().Trim());
                    }
                    catch
                    {
                        VerError("La dirección de correspondencia esta errada, no esta en el formato correcto");
                    }
                }
                if (vPersona1.barrioCorrespondencia != Int64.MinValue && vPersona1.barrioCorrespondencia != null)
                {
                    try
                    {
                        ddlBarrioCorrespondencia.SelectedValue = HttpUtility.HtmlDecode(vPersona1.barrioCorrespondencia.ToString().Trim());
                    }
                    catch
                    {
                        ddlBarrioCorrespondencia.SelectedValue = ddlBarrioCorrespondencia.SelectedValue;
                    }
                }
                if (!string.IsNullOrEmpty(vPersona1.telCorrespondencia))
                    txtTelCorrespondencia.Text = HttpUtility.HtmlDecode(vPersona1.telCorrespondencia.ToString().Trim());
                if (vPersona1.ciuCorrespondencia != Int64.MinValue && vPersona1.ciuCorrespondencia != null && vPersona1.ciuCorrespondencia != -1)
                    ddlCiuCorrespondencia.SelectedValue = HttpUtility.HtmlDecode(vPersona1.ciuCorrespondencia.ToString().Trim());
                if (!string.IsNullOrEmpty(vPersona1.tipo_persona))
                    if (string.Equals(vPersona1.tipo_persona, 'N'))
                        rblTipo_persona.SelectedValue = HttpUtility.HtmlDecode("Natural");
                if (string.Equals(vPersona1.tipo_persona, 'J'))
                    rblTipo_persona.SelectedValue = HttpUtility.HtmlDecode("Jurídica");
                if (!string.IsNullOrEmpty(vPersona1.identificacion))
                {
                    txtIdentificacionE.Text = HttpUtility.HtmlDecode(vPersona1.identificacion.ToString().Trim());
                }
                //if (vPersona1.digito_verificacion != Int64.MinValue)
                //    txtDigito_verificacion.Text = HttpUtility.HtmlDecode(vPersona1.digito_verificacion.ToString().Trim());
                //else
                //    txtDigito_verificacion.Text = "";
                if (vPersona1.tipo_identificacion != Int64.MinValue)
                    ddlTipoE.SelectedValue = HttpUtility.HtmlDecode(vPersona1.tipo_identificacion.ToString().Trim());
                if (vPersona1.fechaexpedicion != DateTime.MinValue && vPersona1.fechaexpedicion != null)
                    txtFechaexpedicion.Text = HttpUtility.HtmlDecode(vPersona1.fechaexpedicion.Value.ToShortDateString());
                else
                    txtFechaexpedicion.Text = "";
                if (vPersona1.codciudadexpedicion != Int64.MinValue && vPersona1.codciudadexpedicion != null && vPersona1.codciudadexpedicion != -1 && vPersona1.codciudadexpedicion != 0)
                    ddlLugarExpedicion.SelectedValue = HttpUtility.HtmlDecode(vPersona1.codciudadexpedicion.ToString().Trim());
                if (!string.IsNullOrEmpty(vPersona1.sexo) && !string.Equals(vPersona1.sexo.ToString().Trim(), ""))
                {
                    try
                    {
                        rblSexo.SelectedValue = HttpUtility.HtmlDecode(vPersona1.sexo.ToString().Trim());
                    }
                    catch
                    {
                        rblSexo.SelectedValue = rblSexo.SelectedValue;
                    }
                }
                if (!string.IsNullOrEmpty(vPersona1.primer_nombre))
                    txtPrimer_nombreE.Text = HttpUtility.HtmlDecode(vPersona1.primer_nombre.ToString().Trim());
                else
                    txtPrimer_nombreE.Text = "";
                if (!string.IsNullOrEmpty(vPersona1.segundo_nombre))
                    txtSegundo_nombreE.Text = HttpUtility.HtmlDecode(vPersona1.segundo_nombre.ToString().Trim());
                else
                    txtSegundo_nombreE.Text = "";
                if (!string.IsNullOrEmpty(vPersona1.primer_apellido))
                    txtPrimer_apellidoE.Text = HttpUtility.HtmlDecode(vPersona1.primer_apellido.ToString().Trim());
                else
                    txtPrimer_apellidoE.Text = "";
                if (!string.IsNullOrEmpty(vPersona1.segundo_apellido))
                    txtSegundo_apellidoE.Text = HttpUtility.HtmlDecode(vPersona1.segundo_apellido.ToString().Trim());
                else
                    txtSegundo_apellidoE.Text = "";
                if (vPersona1.razon_social == null && Session["Negocio"] != null)
                    vPersona1.razon_social = Session["Negocio"].ToString();
                if (vPersona1.fechanacimiento != DateTime.MinValue && vPersona1.fechanacimiento != null)
                {
                    txtFechanacimiento.Text = HttpUtility.HtmlDecode(vPersona1.fechanacimiento.Value.ToShortDateString());
                    txtEdadCliente.Text = Convert.ToString(GetAge(Convert.ToDateTime(txtFechanacimiento.Text)));
                }
                else
                {
                    txtFechanacimiento.Text = "";
                    txtEdadCliente.Text = "";
                }
                if (vPersona1.codciudadnacimiento != Int64.MinValue && vPersona1.codciudadnacimiento != null && vPersona1.codciudadnacimiento.ToString().Trim() != "" && vPersona1.codciudadnacimiento != 0)
                    ddlLugarNacimiento.SelectedValue = HttpUtility.HtmlDecode(vPersona1.codciudadnacimiento.ToString().Trim());
                if (vPersona1.codestadocivil != Int64.MinValue && vPersona1.codestadocivil.ToString().Trim() != "")
                {
                    try
                    {
                        ddlEstadoCivil.SelectedValue = HttpUtility.HtmlDecode(vPersona1.codestadocivil.ToString().Trim());
                    }
                    catch
                    {
                        ddlEstadoCivil.SelectedValue = ddlEstadoCivil.SelectedValue;
                    }
                }

                if (vPersona1.codescolaridad != Int64.MinValue && vPersona1.codescolaridad.ToString().Trim() != "")
                {
                    try
                    {
                        ddlNivelEscolaridad.SelectedValue = HttpUtility.HtmlDecode(vPersona1.codescolaridad.ToString().Trim());
                    }
                    catch
                    {
                        ddlNivelEscolaridad.SelectedValue = ddlNivelEscolaridad.SelectedValue;
                    }
                }

                if (vPersona1.codactividadStr != null)
                {
                    if (vPersona1.codactividadStr.ToString().Trim() != "")
                    {
                        try
                        {
                            ddlActividadE.SelectedValue = HttpUtility.HtmlDecode(vPersona1.codactividadStr.ToString().Trim());
                        }
                        catch
                        {
                            ddlActividadE.SelectedValue = ddlActividadE.SelectedValue;
                        }
                    }
                }
                if (!string.IsNullOrEmpty(vPersona1.direccion))
                {
                    try
                    {
                        txtDireccionE.Text = HttpUtility.HtmlDecode(vPersona1.direccion.ToString().Trim());
                    }
                    catch
                    {
                        VerError("El formato de la dirección no corresponde");
                    }
                }
                else
                {
                    txtDireccionE.Text = "";
                }
                if (!string.IsNullOrEmpty(vPersona1.telefono))
                    txtTelefonoE.Text = HttpUtility.HtmlDecode(vPersona1.telefono.ToString().Trim());
                else
                    txtTelefonoE.Text = "";
                if (vPersona1.antiguedadlugar != Int64.MinValue)
                    txtAntiguedadlugar.Text = HttpUtility.HtmlDecode(vPersona1.antiguedadlugar.ToString().Trim());
                else
                    txtAntiguedadlugar.Text = "";
                if (!string.IsNullOrEmpty(vPersona1.tipovivienda) && !string.Equals(vPersona1.tipovivienda.ToString().Trim(), "0"))
                {
                    if (vPersona1.tipovivienda != "P" && vPersona1.tipovivienda != "A" && vPersona1.tipovivienda != "F")
                        vPersona1.tipovivienda = "P";
                    rblTipoVivienda.SelectedValue = HttpUtility.HtmlDecode(vPersona1.tipovivienda.ToString().Trim());
                }
                if (!string.IsNullOrEmpty(vPersona1.arrendador))
                    txtArrendador.Text = HttpUtility.HtmlDecode(vPersona1.arrendador.ToString().Trim());
                else
                    txtArrendador.Text = "";
                if (!string.IsNullOrEmpty(vPersona1.telefonoarrendador))
                    txtTelefonoarrendador.Text = HttpUtility.HtmlDecode(vPersona1.telefonoarrendador.ToString().Trim());
                else
                    txtTelefonoarrendador.Text = "";
                if (vPersona1.ValorArriendo != Int64.MinValue)
                    txtValorArriendo.Text = HttpUtility.HtmlDecode(vPersona1.ValorArriendo.ToString().Trim());
                else
                    txtValorArriendo.Text = "";
                if (!string.IsNullOrEmpty(vPersona1.celular))
                    txtCelular.Text = HttpUtility.HtmlDecode(vPersona1.celular.ToString().Trim());
                else
                    txtCelular.Text = "";
                if (!string.IsNullOrEmpty(vPersona1.email))
                    txtEmail.Text = HttpUtility.HtmlDecode(vPersona1.email.ToString().Trim());
                else
                    txtEmail.Text = "";
                //if (!string.IsNullOrEmpty(vPersona1.empresa))
                //    txtEmpresa.Text = HttpUtility.HtmlDecode(vPersona1.empresa.ToString().Trim());
                //else
                //    txtEmpresa.Text = "";
                //if (!string.IsNullOrEmpty(vPersona1.telefonoempresa))
                //    txtTelefonoempresa.Text = HttpUtility.HtmlDecode(vPersona1.telefonoempresa.ToString().Trim());
                //else
                //    txtTelefonoempresa.Text = "";
                //if (!string.IsNullOrEmpty(vPersona1.direccionempresa))
                //{
                //    try
                //    {
                //        txtDireccionEmpresa.Text = HttpUtility.HtmlDecode(vPersona1.direccionempresa.ToString().Trim());
                //        if (vPersona1.direccionempresa == "" || vPersona1.direccionempresa == "0")
                //            txtDireccionEmpresa.Text = HttpUtility.HtmlDecode(vPersona1.direccion.ToString().Trim());
                //    }
                //    catch
                //    {
                //        VerError("El formato de dirección de la empresa no corresponde");
                //    }
                //}
                //else
                //    txtDireccionEmpresa.Text = "";
                //if (vPersona1.antiguedadlugarempresa != Int64.MinValue)
                //    txtAntiguedadlugarEmpresa.Text = HttpUtility.HtmlDecode(vPersona1.antiguedadlugarempresa.ToString().Trim());
                //else
                //    txtAntiguedadlugarEmpresa.Text = "";
                //if (vPersona1.codcargo != Int64.MinValue)
                //    ddlCargo.SelectedValue = HttpUtility.HtmlDecode(vPersona1.codcargo.ToString().Trim());
                //else
                //    ddlCargo.SelectedItem.Value = "0";
                //if (vPersona1.codtipocontrato != Int64.MinValue)
                //    ddlTipoContrato.Text = HttpUtility.HtmlDecode(vPersona1.codtipocontrato.ToString().Trim());
                //else
                //    ddlTipoContrato.SelectedItem.Value = "0";
                if (vPersona1.cod_oficina != Int64.MinValue)
                    ddlOficina.SelectedValue = HttpUtility.HtmlDecode(vPersona1.cod_oficina.ToString().Trim());
                else
                    ddlOficina.SelectedValue = "";

                //if (vPersona1.ActividadEconomicaEmpresa.HasValue)
                //{
                //    ddlActividadE0.SelectedValue = vPersona1.ActividadEconomicaEmpresa.ToString();
                //}

                //if (vPersona1.ciudad.HasValue)
                //{
                //    ddlCiu0.SelectedValue = vPersona1.ciudad.ToString();
                //}

                //if (vPersona1.relacionEmpleadosEmprender > 0)
                //{
                //    ddlparentesco.SelectedValue = vPersona1.relacionEmpleadosEmprender.ToString();
                //}
                
                //txtTelCell0.Text = vPersona1.CelularEmpresa;
                txtProfecion.Text = vPersona1.profecion;
                txtEstrato.Text = vPersona1.Estrato.ToString();
                txtPersonasCargo.Text = vPersona1.PersonasAcargo.ToString();
                //txtSueldo.Text = vPersona1.salario.ToString();

                //if (vPersona1.fecha_ingresoempresa != DateTime.MinValue)
                //{
                //    txtFechaContratacion1.Text = vPersona1.fecha_ingresoempresa.ToShortDateString();
                //}

                validarArriendo();
                CalcularEdad();

                #endregion

                // Cargar la información de los familiares
                Empleado_Familiar nomEmp = new Empleado_Familiar();
                nomEmp.cod_persona = vPersona1.cod_persona;
                List<Empleado_Familiar> detafamiliar = servicefamiliar.ListarEmpleado_Familiar(nomEmp, Usuario);

                if (detafamiliar.Count <= 0)
                {
                    detafamiliar.Add(new Empleado_Familiar());
                }

                Session["Empleado_Familiar"] = detafamiliar;

                gvfam.DataSource = detafamiliar;
                gvfam.DataBind();



                // Cargar la información de estudios
                List<Empleado_Estudios> detalleestudio = serviceestudio.ListarEmpleado_Estudios(vPersona1.cod_persona, Usuario);

                if (detalleestudio.Count <= 0)
                {
                    detalleestudio.Add(new Empleado_Estudios());
                }
                Session["Empleado_Estudios"] = detalleestudio;

                gvInformacionAcademica.DataSource = detalleestudio;
                gvInformacionAcademica.DataBind();

                // Cargar la experiencia laboral.
                List<Experiencia_Laboral> detaexperiencia = serviceexperiencia.ListarExperiencia_Laboral(vPersona1.cod_persona, Usuario);

                if (detaexperiencia.Count <= 0)
                {
                    detaexperiencia.Add(new Experiencia_Laboral());
                }
                Session["Experiencia_Laboral"] = detaexperiencia;



                gvExperienciaLaboral.DataSource = detaexperiencia;
                gvExperienciaLaboral.DataBind();

                Empleados empleado = ConsultarEmpleado(pIdObjeto);

                //if (!string.IsNullOrWhiteSpace(empleado.cod_nomina_emp))
                //{
                //    ddlNomina.SelectedValue = empleado.cod_nomina_emp;
                //}

                //txtTipoSueldo.Text = empleado.tipo_sueldo;

                Site toolBar = (Site)Master;
                toolBar.MostrarGuardar(true);
                pnlInformacionPersona.Visible = true;
                txtIdentificacionE.Enabled = false;
            }
            else
                VerError("Error de datos");
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }


    #endregion


    #region Manejo Entidades 


    private List<Empleado_Estudios> GuardarLista(Empleado_Estudios pdetalle)
    {
        List<Empleado_Estudios> LstEmpleado_Estudios = new List<Empleado_Estudios>();
        LstEmpleado_Estudios = (List<Empleado_Estudios>)Session["Empleado_Estudios"];
        ViewState["listaestudio"] = LstEmpleado_Estudios;

        if (ViewState["listaestudio"] == null)
        {
            List<Empleado_Estudios> p = this.InicializarListaEstudios();
            p.Add(pdetalle);
            ViewState["listaestudio"] = p;
        }
        else
        {
            List<Empleado_Estudios> p = (List<Empleado_Estudios>)ViewState["listaestudio"];
            p.Add(pdetalle);
            ViewState["listaestudio"] = p;
        }
        return (List<Empleado_Estudios>)ViewState["listaestudio"];
    }

    private List<Empleado_Estudios> Obtenerlista()
    {
        if (ViewState["listaestudio"] == null)
            return this.InicializarListaEstudios();
        else
            return (List<Empleado_Estudios>)ViewState["listaestudio"];
    }

    /// <summary>
    ///  Inicializar lista para estudios del empleado
    /// </summary>
    /// <returns></returns>
    public List<Empleado_Estudios> InicializarListaEstudios()
    {
        List<Empleado_Estudios> detalle = new List<Empleado_Estudios>();

        return detalle;
    }

    private List<Experiencia_Laboral> GuardarListaExp(Experiencia_Laboral pdetalle)
    {
        List<Experiencia_Laboral> LstExperiencia_Laboral = new List<Experiencia_Laboral>();
        LstExperiencia_Laboral = (List<Experiencia_Laboral>)Session["Experiencia_Laboral"];
        ViewState["listaexplaboral"] = LstExperiencia_Laboral;

        if (ViewState["listaexplaboral"] == null)
        {
            List<Experiencia_Laboral> p = this.InicializarListaExperienciaLaboral();
            p.Add(pdetalle);
            ViewState["listaexplaboral"] = p;
        }
        else
        {
            List<Experiencia_Laboral> p = (List<Experiencia_Laboral>)ViewState["listaexplaboral"];
            p.Add(pdetalle);
            ViewState["listaexplaboral"] = p;
        }
        return (List<Experiencia_Laboral>)ViewState["listaexplaboral"];
    }

    private List<Experiencia_Laboral> ObtenerlistaExp()
    {
        if (ViewState["listaexplaboral"] == null)
            return this.InicializarListaExperienciaLaboral();
        else
            return (List<Experiencia_Laboral>)ViewState["listaexplaboral"];
    }

    public List<Experiencia_Laboral> InicializarListaExperienciaLaboral()
    {
        List<Experiencia_Laboral> detalle = new List<Experiencia_Laboral>();

        return detalle;
    }



    private List<Empleado_Familiar> GuardarListafam(Empleado_Familiar pdetalle)
    {

        List<Empleado_Familiar> LstEmpleado_Familiar = new List<Empleado_Familiar>();
        LstEmpleado_Familiar = (List<Empleado_Familiar>)Session["Empleado_Familiar"];
        ViewState["listafamiliar"] = LstEmpleado_Familiar;


        if (ViewState["listafamiliar"] == null)
        {
            List<Empleado_Familiar> p = this.InicializarListaFamiliares();
            p.Add(pdetalle);

            ViewState["listafamiliar"] = p;
        }
        else
        {
            List<Empleado_Familiar> p = (List<Empleado_Familiar>)ViewState["listafamiliar"];
            p.Add(pdetalle);
            ViewState["listafamiliar"] = LstEmpleado_Familiar;
        }
        return (List<Empleado_Familiar>)ViewState["listafamiliar"];
    }

    private List<Empleado_Familiar> Obtenerlistafam()
    {
        if (ViewState["listafamiliar"] == null)
        {
            return this.InicializarListaFamiliares();
        }
        else
        {
            return (List<Empleado_Familiar>)ViewState["listafamiliar"];
        }
    }

    public List<Empleado_Familiar> InicializarListaFamiliares()
    {
        List<Empleado_Familiar> detalle = new List<Empleado_Familiar>();

               return detalle;
    }

    private Empleados ObtenerEntidadGuardar()
    {
        Empleados empleado = new Empleados();

        empleado.consecutivo = !string.IsNullOrWhiteSpace(_codEmpleado) ? Convert.ToInt64(_codEmpleado) : 0;
        empleado.cod_persona = !string.IsNullOrWhiteSpace(_codPersona) ? Convert.ToInt64(_codPersona) : 0;
        empleado.fecha_expedicion = !string.IsNullOrWhiteSpace(txtFechaexpedicion.Text) ? Convert.ToDateTime(txtFechaexpedicion.Text) : default(DateTime?);
        empleado.direccion = txtDireccionE.Text;
        empleado.celular = txtCelular.Text;
        empleado.cod_estado_civil = ddlEstadoCivil.SelectedValue;
        empleado.telefono = txtTelefonoE.Text;
        empleado.fecha_nacimiento = !string.IsNullOrWhiteSpace(txtFechanacimiento.Text) ? Convert.ToDateTime(txtFechanacimiento.Text) : default(DateTime?);
        empleado.cod_ciudad_nac = ddlLugarNacimiento.SelectedValue;
        empleado.cod_oficina = ddlOficina.SelectedValue;
        //empleado.cod_cargo = ddlCargo.SelectedValue;
        //empleado.cod_tipo_contrato = ddlTipoContrato.SelectedValue;
        //empleado.fecha_ingreso = !string.IsNullOrWhiteSpace(txtFechaContratacion1.Text) ? Convert.ToDateTime(txtFechaContratacion1.Text) : default(DateTime?);
        //empleado.salario = txtSueldo.Text;
        //empleado.jornada_laboral = rblJornadaLaboral.SelectedValue;
        //empleado.cod_nomina_emp = ddlNomina.SelectedValue;
        //empleado.tipo_sueldo = txtTipoSueldo.Text;

        return empleado;
    }

    List<Empleado_Estudios> ObtenerEstudios()
    {
        List<Empleado_Estudios> listaEmpleados = new List<Empleado_Estudios>();

        foreach (GridViewRow row in gvInformacionAcademica.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                Empleado_Estudios estudio = new Empleado_Estudios();

                long consecutivo = Convert.ToInt64(gvInformacionAcademica.DataKeys[row.RowIndex].Value.ToString());

                Label lblSemestre = row.FindControl("lblSemestre") as Label;
                Label lblProfesion = row.FindControl("lblProfesion") as Label;
                DropDownList ddlItemhorarioestudio = row.FindControl("ddlItemhorarioestudio") as DropDownList;
                Label lblFechaInicio = row.FindControl("lblFechaInicio") as Label;
                Label lblTiTuloObtenido = row.FindControl("lblTiTuloObtenido") as Label;
                Label lblEstablecimiento = row.FindControl("lblEstablecimiento") as Label;
                Label lblFechaTerminacion = row.FindControl("lblFechaTerminacion") as Label;
                DropDownList ddlItemhorarititulo = row.FindControl("ddlItemhorarititulo") as DropDownList;
                CheckBox chkItemEstudia = row.FindControl("chkItemEstudia") as CheckBox;

                estudio.consecutivo = consecutivo;
                estudio.cod_persona = !string.IsNullOrWhiteSpace(_codPersona) ? Convert.ToInt64(_codPersona) : 0;
                estudio.consecutivo_empleado = !string.IsNullOrWhiteSpace(_codEmpleado) ? Convert.ToInt64(_codEmpleado) : 0;
                estudio.semestre = lblSemestre.Text;
                estudio.profesion = lblProfesion.Text;
                estudio.horario_estudio = !string.IsNullOrWhiteSpace(ddlItemhorarioestudio.SelectedValue) ? Convert.ToInt64(ddlItemhorarioestudio.SelectedValue) : 0;
                estudio.fecha_inicio = !string.IsNullOrWhiteSpace(lblFechaInicio.Text) ? Convert.ToDateTime(lblFechaInicio.Text) : default(DateTime?);
                estudio.titulo_obtenido = lblTiTuloObtenido.Text;
                estudio.establecimiento = lblEstablecimiento.Text;
                estudio.fecha_terminacion = !string.IsNullOrWhiteSpace(lblFechaTerminacion.Text) ? Convert.ToDateTime(lblFechaTerminacion.Text) : default(DateTime?);
                estudio.horario_titulo = !string.IsNullOrWhiteSpace(ddlItemhorarititulo.SelectedValue) ? Convert.ToInt64(ddlItemhorarititulo.SelectedValue) : default(long?);
                estudio.estudia = chkItemEstudia.Checked ? 1 : 0;

                listaEmpleados.Add(estudio);
            }
        }

        return listaEmpleados;
    }

    List<Experiencia_Laboral> ObtenerExperiencia()
    {
        List<Experiencia_Laboral> listaExperiencia = new List<Experiencia_Laboral>();

        foreach (GridViewRow row in gvExperienciaLaboral.Rows)
        {
            Experiencia_Laboral experiencia = new Experiencia_Laboral();

            long consecutivo = Convert.ToInt64(gvExperienciaLaboral.DataKeys[row.RowIndex].Value.ToString());

            Label lblNombreEmpresa = row.FindControl("lblNombreEmpresa") as Label;
            DropDownList ddlItemCargo = row.FindControl("ddlItemCargo") as DropDownList;
            Label lblFechaIngreso = row.FindControl("lblFechaIngreso") as Label;
            Label lblFechaRetiro = row.FindControl("lblFechaRetiro") as Label;
            Label lblMotivoRetiro = row.FindControl("lblMotivoRetiro") as Label;

            experiencia.consecutivo = consecutivo;
            experiencia.cod_persona = !string.IsNullOrWhiteSpace(_codPersona) ? Convert.ToInt64(_codPersona) : 0;
            experiencia.consecutivo_empleado = !string.IsNullOrWhiteSpace(_codEmpleado) ? Convert.ToInt64(_codEmpleado) : 0;
            experiencia.nombre_empresa = lblNombreEmpresa.Text;
            experiencia.codcargo = Convert.ToInt32(ddlItemCargo.SelectedValue);
            experiencia.fecha_ingreso = !string.IsNullOrWhiteSpace(lblFechaIngreso.Text) ? Convert.ToDateTime(lblFechaIngreso.Text) : default(DateTime?);
            experiencia.fecha_retiro = !string.IsNullOrWhiteSpace(lblFechaRetiro.Text) ? Convert.ToDateTime(lblFechaRetiro.Text) : default(DateTime?);
            experiencia.motivo_retiro = lblMotivoRetiro.Text;

            listaExperiencia.Add(experiencia);
        }

        return listaExperiencia;
    }

    List<Empleado_Familiar> ObtenerFamiliares()
    {
        List<Empleado_Familiar> listaFamiliares = new List<Empleado_Familiar>();

        foreach (GridViewRow row in gvfam.Rows)
        {
            Empleado_Familiar familiar = new Empleado_Familiar();

           //nt consecutivo = Convert.ToInt32(gvfam.DataKeys[row.RowIndex].Value.ToString());

            long consecutivo = Convert.ToInt64(gvfam.DataKeys[row.RowIndex].Value.ToString());

            Label Labelnombre3 = row.FindControl("Labelnombre3") as Label;
            DropDownList ddlItemparenteco = row.FindControl("ddlItemparenteco") as DropDownList;
            DropDownList ddlItemtipoidentificacion = row.FindControl("ddlItemtipoidentificacion") as DropDownList;
            Label Labelidentificacion3 = row.FindControl("Labelidentificacion3") as Label;
            Label Labelprofesion3 = row.FindControl("Labelprofesion3") as Label;
            DropDownList ddlItemconvive = row.FindControl("ddlItemconvive") as DropDownList;

            familiar.consecutivo = (consecutivo);
            familiar.cod_persona = !string.IsNullOrWhiteSpace(_codPersona) ? Convert.ToInt64(_codPersona) : 0;
            familiar.codigoempleado = !string.IsNullOrWhiteSpace(_codEmpleado) ? Convert.ToInt32(_codEmpleado) : 0;
            familiar.nombrefamiliar = Labelnombre3.Text;
            familiar.parentezco = ddlItemparenteco.SelectedValue;
            familiar.tipoidentificacion = ddlItemtipoidentificacion.SelectedValue;
            familiar.identificacionfamiliar = Labelidentificacion3.Text;
            familiar.profesion = Labelprofesion3.Text;
            familiar.convivefamiliar = ddlItemconvive.SelectedValue;

            listaFamiliares.Add(familiar);
        }

        return listaFamiliares;
    }


    #endregion


    protected void gvInformacionAcademica_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }

    protected void gvExperienciaLaboral_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }

    protected void gvfam_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }

    protected void ddlOficina_SelectedIndexChanged(object sender, EventArgs e)
    {
        ObtenerDatos(idObjeto);
    }
}