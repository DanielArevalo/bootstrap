using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Aportes.Services;
using Xpinn.Aportes.Entities;
using Xpinn.Util;

public partial class Nuevo : GlobalWeb
{
    ParametrizacionProcesoAfilicacionService _paramProceso = new ParametrizacionProcesoAfilicacionService();
    Xpinn.Aportes.Services.AfiliacionServices AfiliacionServicio = new Xpinn.Aportes.Services.AfiliacionServices();

    private void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            Usuario pUsuario = (Usuario)Session["usuario"];
            string codOpcion = Session["CodOpcion"].ToString();
            if (Session[codOpcion + ".id"] != null)
                VisualizarOpciones(codOpcion.ToString(), "E");
            else
                VisualizarOpciones(codOpcion.ToString(), "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoRegresar += btnRegresar_Click;
            toolBar.MostrarGuardar(false);
            ctlmensaje.eventoClick += btnContinuar_Click;
            ctlBusquedaPersonas.eventoEditar += gvControl_RowEditing;
            if (Session[AfiliacionServicio.CodigoPrograma + ".id"] != null)
            {
                btnSiguiente.Visible = true;
            }
        }
        catch (Exception ex)
        {
            VerError(Session["CodOpcion"].ToString() + "Page_PreInit" + ex.Message);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                CargarListas();
                CargarCampos();
                txtFecha.Text = DateTime.Now.ToShortDateString();
                string codOpcion = Session["CodOpcion"].ToString();
                string idObjeto = "";
                if (Session[codOpcion + ".id"] != null)
                {
                    idObjeto = Session[codOpcion + ".id"].ToString();                    
                    if (idObjeto != null && idObjeto != "")
                        ObtenerDatos(idObjeto);
                    else
                        mvProcesos.ActiveViewIndex = 0;
                }
                else
                    mvProcesos.ActiveViewIndex = 0;
            }
        }
        catch (Exception ex)
        {
            VerError(Session["CodOpcion"].ToString() + "Page_Load" + ex.Message);
        }
    }
    //hoja de ruta
    protected void btnSiguiente_Click(object sender, ImageClickEventArgs e)
    {
        string cod_per = Session[AfiliacionServicio.CodigoPrograma + ".id"].ToString();
        string nvNext = Session[AfiliacionServicio.CodigoPrograma + "next"].ToString();
        string nvLast = Session[AfiliacionServicio.CodigoPrograma + "last"].ToString();
        ParametrizacionProcesoAfiliacion vParam = new ParametrizacionProcesoAfiliacion();
        vParam.lstParametros = _paramProceso.ListarParametrosProcesoAfiliacion((Usuario)Session["usuario"]);
        Int32 opc = Convert.ToInt32(Session["CodOpcion"].ToString());
        Int32 number = 0;
        switch (opc)
        {
            case 170901: number = 4;  break;
            case 170902: number = 5; break;
            case 170903: number = 6; break;
        }
        bool stop = false;
        foreach (ParametrizacionProcesoAfiliacion redirect in vParam.lstParametros)
        {
            if (redirect.cod_proceso != Convert.ToInt32(nvLast) && redirect.cod_proceso > Convert.ToInt32(number))
            {
                switch (redirect.cod_proceso)
                {
                    case 1:
                        if (Convert.ToBoolean(vParam.lstParametros[0].requerido))
                        {
                            Session[AfiliacionServicio.CodigoPrograma + ".id"] = cod_per;
                            Session[AfiliacionServicio.CodigoPrograma + "last"] = vParam.lstParametros[0].cod_proceso;
                            Session[AfiliacionServicio.CodigoPrograma + "next"] = vParam.lstParametros[1].cod_proceso;
                            Navegar("../../GestionRiesgo/ConsultasOFAC/Nuevo.aspx");
                            stop = true;
                        }
                        break;
                    case 2:
                        if (Convert.ToBoolean(vParam.lstParametros[1].requerido))
                        {
                            Session["cedula"] = txtIdentificacion.Text;
                            Session[AfiliacionServicio.CodigoPrograma + "last"] = vParam.lstParametros[0].cod_proceso;
                            Session[AfiliacionServicio.CodigoPrograma + "next"] = vParam.lstParametros[1].cod_proceso;
                            Navegar("../../Aportes/CuentasAportes/Nuevo.aspx");
                            stop = true;
                        }
                        break;
                    case 3:
                        if (Convert.ToBoolean(vParam.lstParametros[2].requerido))
                        {
                            ImagenesService imagenService = new ImagenesService();
                            Session[imagenService.CodigoPrograma.ToString() + ".cod_persona"] = cod_per;
                            Session[AfiliacionServicio.CodigoPrograma + "last"] = vParam.lstParametros[1].cod_proceso;
                            Session[AfiliacionServicio.CodigoPrograma + "next"] = vParam.lstParametros[2].cod_proceso;
                            Navegar("../../Aportes/ImagenesPersona/Nuevo.aspx");
                            stop = true;
                        }
                        break;
                    case 4:
                        if (Convert.ToBoolean(vParam.lstParametros[3].requerido))
                        {
                            string id = cod_per;
                            string codOpcion = "170901";
                            Session["CodOpcion"] = codOpcion;
                            Session[codOpcion.ToString() + ".id"] = id;
                            Session[AfiliacionServicio.CodigoPrograma + "last"] = vParam.lstParametros[2].cod_proceso;
                            Session[AfiliacionServicio.CodigoPrograma + "next"] = vParam.lstParametros[3].cod_proceso;
                            Navegar("../../Aportes/ProcesosAfiliacion/Nuevo.aspx");
                            stop = true;
                        }
                        break;
                    case 5:
                        if (Convert.ToBoolean(vParam.lstParametros[4].requerido))
                        {
                            string id = cod_per;
                            string codOpcion = "170902";
                            Session["CodOpcion"] = codOpcion;
                            Session[codOpcion.ToString() + ".id"] = id;
                            Session[AfiliacionServicio.CodigoPrograma + "last"] = vParam.lstParametros[3].cod_proceso;
                            Session[AfiliacionServicio.CodigoPrograma + "next"] = vParam.lstParametros[4].cod_proceso;
                            Navegar("../../Aportes/ProcesosAfiliacion/Nuevo.aspx");
                            stop = true;
                        }
                        break;
                    case 6:
                        if (Convert.ToBoolean(vParam.lstParametros[5].requerido))
                        {
                            string id = cod_per;
                            string codOpcion = "170903";
                            Session["CodOpcion"] = codOpcion;
                            Session[codOpcion.ToString() + ".id"] = id;
                            Session[AfiliacionServicio.CodigoPrograma + "last"] = vParam.lstParametros[4].cod_proceso;
                            Session[AfiliacionServicio.CodigoPrograma + "next"] = "lst";
                            Navegar("../../Aportes/ProcesosAfiliacion/Nuevo.aspx");
                            stop = true;
                        }
                        break;
                }
            }
            if (stop) break;
        }
        if (stop == false)
        {
            Session.Remove(AfiliacionServicio.CodigoPrograma + ".id");
            Navegar("../../Aportes/Afiliaciones/Lista.aspx");
        }
    }
    /// <summary>
    /// Método para cargar listas desplegables
    /// </summary>
    private void CargarListas()
    {
        PoblarListas poblar = new PoblarListas();

        //Cargar tipo de concepto
        rbConcepto.Items.Insert(0, new ListItem("Favorable", "1"));
        rbConcepto.Items.Insert(1, new ListItem("Desfavorable", "2"));
        rbConcepto.SelectedIndex = 0;
        rbConcepto.DataBind();

        //Cargar verificación de consulta listas restrictivas
        rbConsulta_Asociado.Items.Insert(0, new ListItem("Si", "1"));
        rbConsulta_Asociado.Items.Insert(1, new ListItem("No", "2"));
        rbConsulta_Asociado.DataBind();

        //Cargar coincidencias
        rbCoincidencias.Items.Insert(0, new ListItem("Si", "1"));
        rbCoincidencias.Items.Insert(1, new ListItem("No", "2"));
        rbCoincidencias.DataBind();

        //Cargar consulta documentos
        rbDocumentos_Asociado.Items.Insert(0, new ListItem("Si", "1"));
        rbDocumentos_Asociado.Items.Insert(1, new ListItem("No", "2"));
        rbDocumentos_Asociado.DataBind();

        //Cargar tipos de identificación
        poblar.PoblarListaDesplegable("TIPOIDENTIFICACION", "CODTIPOIDENTIFICACION, DESCRIPCION", "", "1", ddlTipo_ID, (Usuario)Session["usuario"]);

        //Cargar lugares del proceso
        poblar.PoblarListaDesplegable("OFICINA", "COD_OFICINA, NOMBRE", "", "1", ddlOficina, (Usuario)Session["usuario"]);

        //Cargar usuarios
        poblar.PoblarListaDesplegable("USUARIOS", "CODUSUARIO, NOMBRE", "", "1", ddlUsuario, (Usuario)Session["usuario"]);
    }

    //Método para determinar campos a visualizar según la opción
    private void CargarCampos()
    {
        string codOpcion = Session["CodOpcion"].ToString();
        if (codOpcion == "170902" || codOpcion == "170904")
        {
            lblLugar.Text = "Lugar de Verificación";
            lblFecha.Text = "Fecha de Verificación";           
            rbDocumentos_Asociado.Visible = false;
            lblDocumentos.Visible = false;
            lblActa.Visible = false;
            txtNumActa.Visible = false;
        }
        else if(codOpcion == "170903")
        {
            ddlOficina.Visible = false;
            ddlUsuario.Visible = false;
            rbCoincidencias.Visible = false;
            rbConsulta_Asociado.Visible = false;
            rbDocumentos_Asociado.Visible = false;
            txtObservacion.Visible = false;
            txtFecha.Visible = false;
            lblLugar.Visible = false;
            lblFecha.Visible = false;
            lblFuncionario.Visible = false;
            lblConsultaListas.Visible = false;
            lblCoincidencias.Visible = false;
            lblDocumentos.Visible = false;
            lblObservacion.Visible = false;
            lblFecha1.Visible = true;
            txtFecha1.Visible = true;
        }
    }

    /// <summary>
    /// Cargar datos del proceso en los controles
    /// </summary>
    /// <param name="idObjeto">Código de la persona a la que se le realiza el proceso</param>
    private void ObtenerDatos(string idObjeto)
    {
        try
        {
            ProcesoAfiliacion pProceso = new ProcesoAfiliacion();
            Xpinn.FabricaCreditos.Entities.Persona1 pPersona = new Xpinn.FabricaCreditos.Entities.Persona1();
            AfiliacionServices AfiliacionServicio = new AfiliacionServices();
            Xpinn.FabricaCreditos.Services.Persona1Service PersonaServicio = new Xpinn.FabricaCreditos.Services.Persona1Service();
            Usuario pUsuario = (Usuario)Session["Usuario"];

            //Obtener datos de la persona
            pProceso.cod_persona = Convert.ToInt64(idObjeto);
            pPersona = PersonaServicio.ConsultaDatosPersona(pProceso.cod_persona, pUsuario);
            if (pPersona.identificacion != "" && pPersona.identificacion != null)
            {
                txtIdentificacion.Text = pPersona.identificacion;
                txtNombres.Text = pPersona.nombre;
                ddlTipo_ID.SelectedValue = pPersona.tipo_identificacion.ToString();
                hdCod_Persona.Value = pProceso.cod_persona.ToString();
            }

            //Obtener datos del proceso si ya se realizó uno
            pProceso.cod_persona = Convert.ToInt64(idObjeto);
            string codOpcion = Session["CodOpcion"].ToString();

            if (codOpcion == "170901")
                pProceso.tipo_proceso = 1;
            else if (codOpcion == "170902")
                pProceso.tipo_proceso = 2;
            else if (codOpcion == "170903")
                pProceso.tipo_proceso = 3;
            else if (codOpcion == "170904")
                pProceso.tipo_proceso = 4;

            pProceso = AfiliacionServicio.ConsultarProceso(pProceso, pUsuario);

            if (pProceso.cod_proceso != 0)
            {
                hdCod_Proceso.Value = pProceso.cod_proceso.ToString();

                hdCod_Persona.Value = pProceso.cod_persona.ToString();
                ddlOficina.SelectedValue = pProceso.cod_oficina.ToString();
                ddlUsuario.SelectedValue = pProceso.cod_usuario.ToString();
                if (pProceso.tipo_proceso != 3)
                    txtFecha.Text = pProceso.fecha.ToShortDateString();
                else
                    txtFecha1.Text = pProceso.fecha.ToShortDateString();
                txtNumActa.Text = pProceso.numero_acta.ToString();

                if (pProceso.concepto == 1)
                    rbConcepto.SelectedValue = "1";
                else if (pProceso.concepto == 2)
                    rbConcepto.SelectedValue = "2";

                txtObservacion.Text = pProceso.observacion;
                pDatosProceso.Enabled = false;
                mvProcesos.ActiveViewIndex = 1;

                Site toolbar = (Site)this.Master;
                toolbar.MostrarConsultar(false);
            }
        }
        catch (Exception ex)
        {
            VerError("Error al cargar los datos del proceso"+ ex.Message);
        }

    }

    /// <summary>
    /// Validar que los campos obligatorios se encuentren registrados
    /// </summary>
    /// <returns></returns>
    private bool ValidarDatos()
    {
        string codOpcion = Session["CodOpcion"].ToString();

        if (txtFecha.Text == "" || txtFecha.Text == null)
        {
            VerError("Registre la fecha del proceso");
            txtFecha.Focus();
            return false;
        }

        if (rbConcepto.SelectedIndex < 0)
        {
            VerError("Indique el concepto dado por el funcionario");
            rbConcepto.Focus();
            return false;
        }

        if (codOpcion == "170901" || codOpcion == "170902" || codOpcion == "170904")
        {
            if (ddlOficina.SelectedIndex <= 0)
            {
                VerError("Seleccione la oficina en la que se realizó el proceso");
                ddlOficina.Focus();
                return false;
            }
        }

        if (codOpcion != "170903")
        {
            if (rbCoincidencias.SelectedIndex < 0)
            {
                VerError("Indique si registra coincidencias");
                rbCoincidencias.Focus();
                return false;
            }
            else if(rbConsulta_Asociado.SelectedIndex < 0)
            {
                VerError("Indique si se consultaron los datos de la persona en listas restrictivas");
                rbConsulta_Asociado.Focus();
                return false;
            }
        }

        if (codOpcion != "170903")
        {
            if (ddlUsuario.SelectedIndex < 0)
            {
                VerError("Seleccione el usuario que realizó el proceso");
                ddlUsuario.Focus();
                return false;
            }
        }

        if (codOpcion == "170901")
        {
            if (rbDocumentos_Asociado.SelectedIndex < 0)
            {
                VerError("Indique si la perona anexó los documentos requeridos");
                rbDocumentos_Asociado.Focus();
                return false;
            }
        }
        return true;
    }

    /// <summary>
    /// Evento para seleccionar la persona a la cual se le realiza el proceso
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvControl_RowEditing(object sender, EventArgs e)
    {
        ctlBusquedaPersonas.CodigoPrograma = "";
        String Codigo = ctlBusquedaPersonas.gvListado.DataKeys[ctlBusquedaPersonas.gvListado.SelectedRow.RowIndex].Value.ToString();
        
        Site toolBar = (Site)this.Master;
        toolBar.MostrarLimpiar(false);
        toolBar.MostrarGuardar(true);
        toolBar.MostrarConsultar(false);
        toolBar.MostrarRegresar(true);

        mvProcesos.ActiveViewIndex = 1;
        ObtenerDatos(Codigo);
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        int tipo_proceso = 0;
        string codOpcion = Session["CodOpcion"].ToString();

        if (codOpcion == "170901")
            tipo_proceso = 1;
        else if (codOpcion == "170902")
            tipo_proceso = 2;
        else if (codOpcion == "170903")
            tipo_proceso = 3;
        else if (codOpcion == "170904")
            tipo_proceso = 4;

        ctlBusquedaPersonas.Filtro = " Persona.Cod_Persona Not In (Select Cod_Persona From Proceso_Afiliacion Where Tipo_Proceso = " + tipo_proceso + ")";
        ctlBusquedaPersonas.Actualizar(0);
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            if (ValidarDatos())
            {
                ctlmensaje.MostrarMensaje("¿Desea registrar los datos del proceso?");
            }
        }
        catch (Exception ex)
        {
            VerError("Error al registrar los datos del proceso" + ex.Message);
        }
    }

    protected void btnContinuar_Click(object sender, EventArgs e)
    {
        if (Session[AfiliacionServicio.CodigoPrograma + ".id"] != null)
        {
            string nvNext = Session[AfiliacionServicio.CodigoPrograma + "next"].ToString();
            Session.Remove(AfiliacionServicio.CodigoPrograma + ".id");
            Navegar("../../Aportes/Afiliaciones/Lista.aspx");
        }else
        {
            ProcesoAfiliacion pProceso = new ProcesoAfiliacion();
            Usuario pUsuario = (Usuario)Session["usuario"];
            string codOpcion = Session["CodOpcion"].ToString();

            pProceso.cod_proceso = hdCod_Proceso.Value != "" && hdCod_Proceso.Value != null ? Convert.ToInt64(hdCod_Proceso.Value) : 0;
            pProceso.cod_persona = Convert.ToInt64(hdCod_Persona.Value);

            if (codOpcion == "170901" || codOpcion == "170902" || codOpcion == "170904")
                pProceso.cod_oficina = Convert.ToInt64(ddlOficina.SelectedValue);
        
            if (codOpcion != "170903")
                pProceso.fecha = Convert.ToDateTime(txtFecha.Text);
            else
                pProceso.fecha = Convert.ToDateTime(txtFecha1.Text);

            if (codOpcion != "170903")
                pProceso.cod_usuario = Convert.ToInt64(ddlUsuario.SelectedValue);
            else
                pProceso.cod_usuario = pUsuario.codusuario;

            if (codOpcion == "170901" || codOpcion == "170903")
                pProceso.numero_acta = txtNumActa.Text != "" && txtNumActa.Text != null ? Convert.ToInt64(txtNumActa.Text) : 0;
            else
                pProceso.numero_acta = 0;

            pProceso.concepto = Convert.ToInt32(rbConcepto.SelectedValue);

            if (codOpcion != "170903")
                pProceso.observacion = txtObservacion.Text != "" && txtObservacion.Text != null ? txtObservacion.Text : "";
            else
                pProceso.observacion = "";

            if (codOpcion == "170901")
                pProceso.tipo_proceso = 1;
            else if (codOpcion == "170902")
                pProceso.tipo_proceso = 2;
            else if (codOpcion == "170903")
                pProceso.tipo_proceso = 3;
            else if (codOpcion == "170904")
                pProceso.tipo_proceso = 4;     

            if (pProceso.cod_proceso == 0)
                pProceso = AfiliacionServicio.CrearProceso(pProceso, (Usuario)Session["usuario"]);
            else if (pProceso.cod_proceso > 0)
                pProceso = AfiliacionServicio.ModificarProceso(pProceso, (Usuario)Session["usuario"]);

            Navegar("~/Page/Aportes/ProcesosAfiliacion/Lista.aspx?CodOpcion=" + codOpcion);
        }
    }
    void registrarControl(Int32 cod_proceso, Int64 cod_per)
    {
        Usuario us = new Usuario();
        us = (Usuario)Session["usuario"];

        ParametrizacionProcesoAfiliacion control = new ParametrizacionProcesoAfiliacion();
        control.numero_solicitud = 0;
        control.identificacion = Convert.ToInt64(Session["identificacion"]);
        control.cod_persona = cod_per;
        control.ip_local = us.IP;
        control.cod_proceso = cod_proceso;

        AfiliacionServicio.controlRutaAfiliacion(control, (Usuario)Session["Usuario"]);
    }
    protected void btnRegresar_Click(object sender, EventArgs e)
    {
        if (Session[AfiliacionServicio.CodigoPrograma + ".id"] != null)
        {
            string cod_per = Session[AfiliacionServicio.CodigoPrograma + ".id"].ToString();
            Int32 act = Convert.ToInt32(Session[AfiliacionServicio.CodigoPrograma + "last"].ToString());
            switch (act)
            {
                case 1:
                    //CONTROL DE RUTA PARA LA EVALUACIÓN 
                    registrarControl(1, Convert.ToInt64(cod_per));
                    Session[AfiliacionServicio.CodigoPrograma + ".id"] = cod_per;
                    Session[AfiliacionServicio.CodigoPrograma + "last"] = 1;
                    Session[AfiliacionServicio.CodigoPrograma + "next"] = 2;
                    Navegar("../../GestionRiesgo/ConsultasOFAC/Nuevo.aspx");
                    break;
                case 2:
                    //CONTROL DE RUTA PARA LA EVALUACIÓN 
                    registrarControl(2, Convert.ToInt64(cod_per));
                    Session["cedula"] = txtIdentificacion.Text;
                    Session[AfiliacionServicio.CodigoPrograma + "last"] = 1;
                    Session[AfiliacionServicio.CodigoPrograma + "next"] = 2;
                    Navegar("../../Aportes/CuentasAportes/Nuevo.aspx");
                    break;
                case 3:
                    //CONTROL DE RUTA PARA LA EVALUACIÓN 
                    registrarControl(3, Convert.ToInt64(cod_per));
                    ImagenesService imagenService = new ImagenesService();
                    Session[imagenService.CodigoPrograma.ToString() + ".cod_persona"] = cod_per;
                    Session[AfiliacionServicio.CodigoPrograma + "last"] = 2;
                    Session[AfiliacionServicio.CodigoPrograma + "next"] = 3;
                    Navegar("../../Aportes/ImagenesPersona/Nuevo.aspx");
                    break;
                case 4:
                    //CONTROL DE RUTA PARA LA EVALUACIÓN 
                    registrarControl(4, Convert.ToInt64(cod_per));
                    string codOpc = "170901";
                    Session["CodOpcion"] = codOpc;
                    Session[codOpc.ToString() + ".id"] = cod_per;
                    Session[AfiliacionServicio.CodigoPrograma + "last"] = 3;
                    Session[AfiliacionServicio.CodigoPrograma + "next"] = 4;
                    Navegar("../../Aportes/ProcesosAfiliacion/Nuevo.aspx");
                    break;
                case 5:
                    //CONTROL DE RUTA PARA LA EVALUACIÓN 
                    registrarControl(5, Convert.ToInt64(cod_per));
                    string codOpcion = "170902";
                    Session["CodOpcion"] = codOpcion;
                    Session[codOpcion.ToString() + ".id"] = cod_per;
                    Session[AfiliacionServicio.CodigoPrograma + "last"] = 4;
                    Session[AfiliacionServicio.CodigoPrograma + "next"] = 5;
                    Navegar("../../Aportes/ProcesosAfiliacion/Nuevo.aspx");
                    break;
                case 6:
                    //CONTROL DE RUTA PARA LA EVALUACIÓN 
                    registrarControl(6, Convert.ToInt64(cod_per));
                    string codOpci = "170903";
                    Session["CodOpcion"] = codOpci;
                    Session[codOpci.ToString() + ".id"] = cod_per;
                    Session[AfiliacionServicio.CodigoPrograma + "last"] = 5;
                    Session[AfiliacionServicio.CodigoPrograma + "next"] = "lst";
                    Navegar("../../Aportes/ProcesosAfiliacion/Nuevo.aspx");
                    break;
            }
        }else
        {
            string codOpcion = Session["CodOpcion"].ToString();
            Session.Remove(codOpcion + ".id");
            Navegar("~/Page/Aportes/ProcesosAfiliacion/Lista.aspx?CodOpcion=" + codOpcion);
        }
    }
}