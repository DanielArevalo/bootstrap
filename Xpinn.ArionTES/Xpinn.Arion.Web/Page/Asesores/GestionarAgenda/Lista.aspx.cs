using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.SessionState;
using System.Drawing;
using System.Net;
using Subgurim.Controles;
using System.Globalization;
using System.Net.Mail;
using System.Text;
using System.IO;
using System.Web.UI.HtmlControls;
using Xpinn.Util;
using Xpinn.FabricaCreditos.Services;
using Xpinn.FabricaCreditos.Entities;
using Xpinn.Asesores.Services;
using Xpinn.Asesores.Entities;
using Microsoft.Reporting.WebForms;



public partial class Lista : GlobalWeb
{
    #region variables globales
    private CreditoService creditoServicio = new CreditoService();
    private ClienteService clienteServicio = new ClienteService();
    private AgendaActividadService agendaActividadServicio = new AgendaActividadService();
    private AgendaHoraService agendaHoraServicio = new AgendaHoraService();
    private ParentescoService parentescoServicio = new ParentescoService();
    private AgendaTipoActividadService tipoActividadServicio = new AgendaTipoActividadService();
    private EjecutivoService ejecutivoservicio = new EjecutivoService();
    private string emailEjec, emaildir;

    #endregion

    #region Metodos iniciales

    protected void Page_PreInit(object sender, EventArgs e)
    {

        try
        {
            //VisualizarOpciones(clienteServicio.CodigoPrograma, "L");
            Site toolBar = (Site)this.Master;

            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
            ucImprimir.PrintCustomEvent += ucImprimir_Click;
            ddlOficina.SelectedIndex = -1;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(creditoServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            if (!IsPostBack)
            {
                LlenarComboOficinas();
                ddlOficina.SelectedIndex = 0;
                //inicializargMap();
                CargarValoresConsulta(pConsulta, creditoServicio.CodigoPrograma);
                Actualizar();
                // Actualizargrillacorreo();    
                LlenarComboTipoActividad();
                Calendar1.SelectedDate = DateTime.Now;
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(creditoServicio.CodigoPrograma, "Page_Load", ex);
        }
    }

    #endregion

    #region Eventos Botones

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        GuardarValoresConsulta(pConsulta, creditoServicio.CodigoPrograma);
        Actualizar();
        AñadirActividades();
    }
    protected void ucImprimir_Click(object sender, ImageClickEventArgs evt)
    {

        Actualizar();
        AñadirActividades();

        Session.Remove("imprimirCtrl2");
        Session["imprimirCtrl2"] = ddlAsesores1.SelectedValue;
        Session.Remove("imprimirCtrl");
        Session["imprimirCtrl"] = gvListaActividades;

        ClientScript.RegisterStartupScript(this.GetType(), "onclick", General_Controles_EnviarCorreo.JSCRIPT_PRINT);
        Actualizar();
        AñadirActividades();
    }
    protected void btncolocacion_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Page/Asesores/Colocacion/Lista.aspx");
    }
    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        LimpiarValoresConsulta(pConsulta, creditoServicio.CodigoPrograma);
        //inicializargMap();
        Actualizar();
    }
    protected void ddlOficina_SelectedIndexChanged(object sender, EventArgs e)
    {
        LlenarComboAsesores(Convert.ToInt64(ddlOficina.SelectedValue));
    }
    protected void gvListaActividades_SelectedIndexChanged(object sender, EventArgs e)
    {
        Validate("vgGuardar");

        String id = gvListaActividades.DataKeys[gvListaActividades.SelectedRow.RowIndex].Value.ToString();
        GridViewRow row = gvListaActividades.SelectedRow;

        if (row.Cells[0].Text == "0" || string.IsNullOrEmpty(row.Cells[0].Text) || row.Cells[0].Text == @"&nbsp;")
        {
            LlenarComboHorasInicial(Convert.ToInt64(row.Cells[1].Text));
            LlenarComboTipoActividad();

            txtDescripcionReg.Text = String.Empty;

            Usuario usuap = (Usuario)Session["usuario"];
            int cod = Convert.ToInt32(usuap.codusuario);
            int consulta = tipoActividadServicio.UsuarioEsAsesor(cod, (Usuario)Session["Usuario"]);
            if (consulta == 1)
            {
                lblAgenda.Text = "AGENDA";
                gvListaActividades.Enabled = true;
                mpeNuevo.Show();
                mpeMensaje.Hide();
                btnConsultar_Click(null, null);
            }

            else
            {
                mpeNuevo.Show();
                mpeMensaje.Hide();
            }

        }
        else
        {
            lblMotivo.Text = "Hora ya asignada.";
            mpeMensaje.Show();
        }

        Actualizar();
        AñadirActividades();
    }
    protected void gvListaActividades_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {

    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        mpeNuevo.Hide();
        btnConsultar_Click(null, null);
    }
    protected void ddlAsesores_SelectedIndexChanged(object sender, EventArgs e)
    {
        AñadirActividades();
    }
    protected void txtRespuestaAct_TextChanged(object sender, EventArgs e)
    {

    }
    protected void ucImprimir_Click(object sender, EventArgs e)
    {
        VerError("");
        AñadirActividades();
        //Obtiene los datos de email del ejecutivo 
        Ejecutivo ejec = new Ejecutivo();

        Int64 idObjeto;
        idObjeto = Convert.ToInt64(ddlAsesores.SelectedValue);
        ejec = ejecutivoservicio.ConsultarDatosEjecutivo(Convert.ToInt64(idObjeto), (Usuario)Session["usuario"]);

        if (!string.IsNullOrEmpty(ejec.Email.ToString()))
            emailEjec = HttpUtility.HtmlDecode(ejec.Email);

        //Obtiene los datos de email del Director  de la oficina 
        Usuario usuap = (Usuario)Session["usuario"];
        Ejecutivo dir = new Ejecutivo();
        Int32 idObjetodir;
        idObjetodir = Convert.ToInt32(usuap.cod_oficina);
        try
        {
            dir = ejecutivoservicio.ConsultarDatosDirector(Convert.ToInt64(idObjetodir), (Usuario)Session["usuario"]);
        }
        catch
        {
            VerError("No existe un director asignado para el envio del email de actividades");
            return;
        }

        if (dir != null)
            if (!string.IsNullOrEmpty(dir.Email.ToString()))
                emaildir = HttpUtility.HtmlDecode(dir.Email);

        Session.Remove("imprimirCtrl3");
        Session.Remove("imprimirCtrl2");
        Session["imprimirCtrl2"] = emailEjec;
        Session["imprimirCtrl3"] = emaildir;
        Session.Remove("imprimirCtrl");
        Session["imprimirCtrl"] = gvListaActividades;

        ClientScript.RegisterStartupScript(this.GetType(), "onclick", General_Controles_EnviarCorreo.JSCRIPT_PRINT);
        Actualizar();
        AñadirActividades();
        HideNoPrintCells();

    }
    protected void txtIdentificacion_TextChanged(object sender, EventArgs e)
    {
        if (txtIdentificacion.Text == "")
        { }
        else
        {
            //DropDownListClientes.SelectedValue = txtIdentificacion.Text;
        }
    }
    protected void txtFecha_TextChanged(object sender, EventArgs e)
    {
        Actualizar();
    }
    protected void gvListaActCorreo_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        Validate("vgGuardar");
        if (Page.IsValid)
        {
            String id = gvListaActividades.DataKeys[gvListaActividades.SelectedRow.RowIndex].Value.ToString();
            GridViewRow row = gvListaActividades.SelectedRow;

            if (row.Cells[0].Text == "&nbsp;")
            {
                LlenarComboHorasInicial(Convert.ToInt64(row.Cells[1].Text));
                LlenarComboTipoActividad();

                txtDescripcionReg.Text = String.Empty;
                Usuario usuap = (Usuario)Session["usuario"];
                int cod = Convert.ToInt32(usuap.codusuario);
                int consulta = tipoActividadServicio.UsuarioEsAsesor(cod, (Usuario)Session["Usuario"]);
                if (consulta == 1)
                {
                    lblAgenda.Text = "AGENDA";
                    gvListaActividades.Enabled = true;
                    mpeNuevo.Show();
                    mpeMensaje.Hide();
                    btnConsultar_Click(null, null);
                }

                else
                {
                    mpeNuevo.Show();
                    mpeMensaje.Hide();
                }
            }
            else
            {
                lblMotivo.Text = "Hora ya asignada.";
                mpeMensaje.Show();
            }
        }
        Actualizar();
        AñadirActividades();
    }
    protected void btnActualizar_Click(object sender, EventArgs e)
    {
        try
        {
            Validate("vgActividadAct");
            if (Page.IsValid)
            {
                string i = Convert.ToString(Session["ACTIVIDAD"]);
                AgendaActividad agActividad = new AgendaActividad();
                if (i != "&nbsp;" || i != "")
                {
                    agActividad.idactividad = 0;
                }
                else
                {

                    agActividad.idactividad = Convert.ToInt64(i);
                }

                agActividad.estado = ddlEstadoAct.SelectedValue;
                agActividad.atendido = txtAtendidoAct.Text;
                agActividad.idparentesco = Convert.ToInt64(ddlParentescoAct.SelectedValue);
                agActividad.respuesta = txtRespuestaAct.Text;
                agActividad.observaciones = txtObservacionesAct.Text;
                agendaActividadServicio.ModificarAgendaActividad(agActividad, (Usuario)Session["usuario"]);
                mpeActualizar.Hide();


            }
            btnConsultar_Click(null, null);
        }
        catch (Xpinn.Util.ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(agendaActividadServicio.CodigoPrograma, "btnActualizar_Click", ex);
        }
    }
    protected void DropDownListClientes_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtIdentificacion.Text = DropDownListClientes.SelectedValue;
    }
    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            //  Validate("vgActividadReg");
            var fecha = Calendar1.SelectedDate.ToString("dd/MM/yyyy") == "01/01/0001"
                ? DateTime.Now.ToString("dd/MM/yyyy")
                : Calendar1.SelectedDate.ToString("dd/MM/yyyy");


            Usuario usuap = (Usuario)Session["usuario"];
            int cod = Convert.ToInt32(usuap.codusuario);
            int consulta = tipoActividadServicio.UsuarioEsAsesor(cod, (Usuario)Session["Usuario"]);

            AgendaActividad agActividad = new AgendaActividad();
            agActividad.fecha = Convert.ToDateTime(fecha);
            Int64 horaInicial = Convert.ToInt64(ddlHoraInicialReg.SelectedValue);
            Int64 horaFinal = Convert.ToInt64(ddlHoraFinalReg.SelectedValue);
            agActividad.hora = horaInicial;
            agActividad.idcliente = Convert.ToInt64(DropDownListClientes.SelectedValue);
            agActividad.tipoactividad = ddlTipoActividadReg.SelectedValue;
            agActividad.idactividad = Convert.ToInt64(ddlTipoActividadReg.SelectedValue);
            agActividad.descripcion = txtDescripcionReg.Text;
            if (consulta == 1)
                agActividad.estado = Convert.ToString(1);
            else
                agActividad.estado = Convert.ToString(5);

            agActividad.idasesor = Convert.ToInt64(ddlAsesores.SelectedValue);

            List<AgendaHora> lstConsultaHoras = new List<AgendaHora>();
            lstConsultaHoras = agendaHoraServicio.ListarAgendaHora(new AgendaHora(), (Usuario)Session["usuario"]);
            bool val = false;
            foreach (AgendaHora aghora in lstConsultaHoras)
            {
                if (aghora.idhora == horaInicial)
                    val = true;
                if (aghora.idhora == horaFinal)
                    val = false;
                if (val)
                {
                    agActividad.hora = aghora.idhora;
                    agendaActividadServicio.CrearAgendaActividad(agActividad, (Usuario)Session["usuario"]);
                }
            }

            mpeNuevo.Hide();
            Calendar1_OnDayRender(null,null);

        }
        catch (Xpinn.Util.ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(agendaActividadServicio.CodigoPrograma, "btnGuardar_Click", ex);
        }

        
        Actualizar();
        AñadirActividades();

    }
    protected void btnClose2_Click(object sender, EventArgs e)
    {
        mpeMensaje.Hide();

        Actualizar();
        AñadirActividades();
    }
    protected void btnClose3_Click(object sender, EventArgs e)
    {
        mpeActualizar.Hide();
        btnConsultar_Click(null, null);
    }
    protected void ddlHoraInicial_SelectedIndexChanged(object sender, EventArgs e)
    {
        LlenarComboHorasInicial(Convert.ToInt64(ddlHoraInicialReg.SelectedValue));
    }
    protected void Calendar1_SelectionChanged(object sender, EventArgs e)
    {

    }
    protected void btnAlarma_Click(object sender, EventArgs e)
    {
        enviarEmail();
    }

    #endregion

    #region Metodos Externos

    protected void LlenarComboOficinas()
    {
        Usuario usuap = (Usuario)Session["usuario"];
        int cod = Convert.ToInt32(usuap.codusuario);
        int consulta = tipoActividadServicio.UsuarioEsAsesor(cod, (Usuario)Session["Usuario"]);

        if (consulta == 1)// esto es para cuando no existe en asejecutivos
        {
            OficinaService oficinaService = new OficinaService();
            Xpinn.FabricaCreditos.Entities.Oficina oficina = new Xpinn.FabricaCreditos.Entities.Oficina();
            ddlOficina.DataSource = oficinaService.ListarOficinasUsuarios(oficina, (Usuario)Session["usuario"]);
            ddlOficina.DataTextField = "Nombre";
            ddlOficina.DataValueField = "Codigo";
            ddlOficina.DataBind();
            ddlOficina.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));
            ddlOficina.DataBind();
            ddlOficina.SelectedValue = Convert.ToString(oficinaService.ListarOficinasUsuarios(oficina, (Usuario)Session["usuario"]).FirstOrDefault().Codigo);
            if (ddlOficina.SelectedValue != "0")
            {
                Int64 iOficina = Convert.ToInt64(ddlOficina.SelectedValue.ToString());
                LlenarComboAsesores(iOficina);
            }
            txtObservacionesAct.Visible = true;
            txtRespuestaAct.Visible = true;
            ddlParentescoAct.Visible = true;
            txtAtendidoAct.Visible = true;
            UpdatePanel2.Visible = true;
            ddlEstadoAct.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));
            ddlEstadoAct.Items.Insert(1, new ListItem("Programada", "1"));
            ddlEstadoAct.Items.Insert(2, new ListItem("En proceso", "2"));
            ddlEstadoAct.Items.Insert(3, new ListItem("Realizada", "3"));
            ddlEstadoAct.Items.Insert(4, new ListItem("Cancelada", "4"));
            ddlEstadoAct.Items.Insert(4, new ListItem("Programada por asesor", "5"));
            ddlEstadoAct.Items.Insert(4, new ListItem("Reemplazada", "6"));

        }
        else
        {
            ddlOficina.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));
            ddlOficina.Items.Insert(1, new ListItem(Convert.ToString(usuap.nombre_oficina), Convert.ToString(usuap.cod_oficina)));
            ddlOficina.DataBind();

            ddlEstadoAct.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));
            ddlEstadoAct.Items.Insert(1, new ListItem("Realizada", "3"));
            ddlEstadoAct.Items.Insert(2, new ListItem("Reemplazada", "6"));
        }
    }
    protected void LlenarComboAsesores(Int64 iOficina)
    {
        Usuario usuap = (Usuario)Session["usuario"];
        int cod = Convert.ToInt32(usuap.codusuario);
        int consulta = tipoActividadServicio.UsuarioEsAsesor(cod, (Usuario)Session["Usuario"]);
        if (consulta == 1)
        {
            EjecutivoService serviceEjecutivo = new EjecutivoService();
            Ejecutivo ejec = new Ejecutivo();
            ejec.IOficina = iOficina;
            ddlAsesores.DataSource = serviceEjecutivo.ListarAsesores(ejec, (Usuario)Session["usuario"]);
            ddlAsesores.DataTextField = "NombreCompleto";
            ddlAsesores.DataValueField = "IdEjecutivo";
            ddlAsesores.DataBind();
            ddlAsesores.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));
        }
        else
        {
            ddlAsesores.Items.Clear();
            ddlAsesores.Items.Insert(0, new ListItem(Convert.ToString(usuap.nombre), Convert.ToString(usuap.codusuario)));
            ddlAsesores.DataBind();
        }
    }
    private void HideNoPrintCells()
    {
        gvListaActividades.Columns[0].Visible = false;
        gvListaActividades.Columns[1].Visible = false;
        gvListaActividades.Columns[2].Visible = false;
        gvListaActividades.Columns[3].Visible = false;
        gvListaActividades.Columns[4].Visible = false;
    }
    private void HidePrintCells()
    {
        gvListaActividades.Columns[0].Visible = true;
        gvListaActividades.Columns[1].Visible = true;
        gvListaActividades.Columns[2].Visible = true;
        gvListaActividades.Columns[3].Visible = true;
        gvListaActividades.Columns[4].Visible = true;
    }
    private void Actualizar()
    {
        HidePrintCells();
        try
        {
            List<AgendaHora> lstConsultaHoras = new List<AgendaHora>();
            AgendaHora hora = new AgendaHora();
            hora.respuesta = txtFecha.Text;
            hora.idactividad = Convert.ToInt64(ddlAsesores.SelectedValue);
            lstConsultaHoras = agendaHoraServicio.ListarAgendaHora(hora, (Usuario)Session["usuario"]);

            DataTable dt = new DataTable();
            dt.Columns.Add("CodActividad");
            dt.Columns.Add("IdHora");
            dt.Columns.Add("Hora");
            dt.Columns.Add("Tipo");
            dt.Columns.Add("Nombrecliente");
            dt.Columns.Add("TipoActividad");
            dt.Columns.Add("Descripcion");
            dt.Columns.Add("Estado");
            dt.Columns.Add("Respuesta");

            for (int i = 0; i < lstConsultaHoras.Count; i++)
            {
                AgendaHora agendaHora = lstConsultaHoras[i];
                DataRow row = dt.NewRow();
                //revizar si podemos añadir las actividades aca
                row["CodActividad"] = agendaHora.idactividad;
                row["IdHora"] = agendaHora.idhora;
                row["Hora"] = agendaHora.hora;
                row["Tipo"] = agendaHora.tipo;
                row["Nombrecliente"] = String.Empty;
                row["TipoActividad"] = String.Empty;
                row["Descripcion"] = agendaHora.descripcion;
                row["Estado"] = String.Empty;
                row["Respuesta"] = agendaHora.respuesta;
                dt.Rows.Add(row);

            }

            gvListaActividades.DataSource = dt;
            gvListaActividades.DataBind();

            if (lstConsultaHoras.Count > 0)
            {
                gvListaActividades.Visible = true;
                lblAgenda.Visible = true;
                gvListaActividades.DataBind();
                AñadirActividades();
            }
            else
            {
                lblAgenda.Visible = false;
                gvListaActividades.Visible = false;
            }

            Session.Add(creditoServicio.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(creditoServicio.CodigoPrograma, "Actualizar", ex);
        }
    }
    protected void Calendar1_OnDayRender(object sender, DayRenderEventArgs e)
    {
        txtFecha.Text = Calendar1.SelectedDate.ToString("dd/MM/yyyy");
        Actualizar();
    }
    private void Actualizargrillacorreo()
    {
        try
        {

            List<AgendaHora> lstConsultaHoras = new List<AgendaHora>();
            AgendaHora hora = new AgendaHora();
            lstConsultaHoras = agendaHoraServicio.ListarAgendaHora(hora, (Usuario)Session["usuario"]);

            DataTable dt = new DataTable();
            dt.Columns.Add("CodActividad");
            dt.Columns.Add("IdHora");
            dt.Columns.Add("Hora");
            dt.Columns.Add("Tipo");
            dt.Columns.Add("Nombrecliente");
            dt.Columns.Add("TipoActividad");
            dt.Columns.Add("Descripcion");
            dt.Columns.Add("Estado");
            dt.Columns.Add("Respuesta");

            for (int i = 0; i < lstConsultaHoras.Count - 1; i++)
            {
                AgendaHora agendaHora = lstConsultaHoras[i];
                DataRow row = dt.NewRow();
                //revizar si podemos añadir las actividades aca
                row["CodActividad"] = String.Empty;
                row["IdHora"] = agendaHora.idhora;
                row["Hora"] = agendaHora.hora;
                row["Tipo"] = agendaHora.tipo;
                row["Nombrecliente"] = String.Empty;
                row["TipoActividad"] = String.Empty;
                row["Descripcion"] = String.Empty;
                row["Estado"] = String.Empty;
                row["Respuesta"] = String.Empty;
                dt.Rows.Add(row);

            }

            if (lstConsultaHoras.Count > 0)
            {
                lblAgenda.Visible = true;
            }
            else
            {
                lblAgenda.Visible = false;
            }

            Session.Add(creditoServicio.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(creditoServicio.CodigoPrograma, "Actualizar", ex);
        }
    }
    private void AñadirActividades()
    {
        List<AgendaActividad> lstConsultaActividades = new List<AgendaActividad>();
        AgendaActividad actividad = new AgendaActividad();
        actividad.idasesor = Convert.ToInt64(ddlAsesores.SelectedValue);
        if (txtFecha.Text == "")
        {
            actividad.fecha = DateTime.Today;
        }
        else
        {
            actividad.fecha = Convert.ToDateTime(txtFecha.Text);
        }
        lstConsultaActividades = agendaActividadServicio.ListarAgendaActividad(actividad, (Usuario)Session["usuario"]);
        if (lstConsultaActividades.Count > 0)
        {
            foreach (AgendaActividad agactividad in lstConsultaActividades)
            {
                foreach (GridViewRow row in gvListaActividades.Rows)
                {
                    if (row.Cells[1].Text == agactividad.hora.ToString())
                    {
                        row.Cells[0].Text = agactividad.idactividad.ToString();
                        row.Cells[7].Text = agactividad.nombrecliente;
                        row.Cells[8].Text = agactividad.tipoactividad;
                        row.Cells[9].Text = agactividad.descripcion;
                        row.Cells[10].Text = agactividad.estado;
                        row.Cells[11].Text = agactividad.respuesta;
                        ColorearTareas(row, agactividad.estado);
                    }
                }
            }
        }
        else
        {
            LimpiarGrilla();
        }
    }

    private void LimpiarGrilla()
    {
        foreach (GridViewRow row in gvListaActividades.Rows)
        {
            row.Cells[0].Text = null;
            row.Cells[7].Text = null;
            row.Cells[8].Text = null;
            row.Cells[9].Text = null;
            row.Cells[10].Text = null;
            row.Cells[11].Text = null;
        }
    }

    private void ColorearTareas(GridViewRow row, String estado)
    {
        switch (estado)
        {
            case "Programada":
                row.BackColor = Color.FromName("#000099");
                break;
            case "En proceso":
                row.BackColor = Color.FromName("#CCCC00");
                break;
            case "Realizada":
                row.BackColor = Color.FromName("#009933");
                break;
            case "Programada por asesor":
                row.BackColor = Color.FromName("#0099FF");
                break;
            case "Reemplazada":
                row.BackColor = Color.FromName("#660066");
                break;
            case "Borrada":
                row.BackColor = Color.FromName("Black");
                break;
        }

    }
    protected void LlenarComboHorasInicial(Int64 sel)
    {
        List<AgendaHora> lstConsultaHorasInicial = new List<AgendaHora>();
        AgendaHora hora = new AgendaHora();
        lstConsultaHorasInicial = agendaHoraServicio.ListarAgendaHora(hora, (Usuario)Session["usuario"]);

        ddlHoraInicialReg.DataSource = lstConsultaHorasInicial;
        ddlHoraInicialReg.DataTextField = "horatipo";
        ddlHoraInicialReg.DataValueField = "idhora";
        ddlHoraInicialReg.DataBind();
        ddlHoraInicialReg.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));
        if (sel != 0)
        {
            ddlHoraInicialReg.SelectedValue = sel.ToString();
        }
        LlenarComboHorasFinal(lstConsultaHorasInicial, sel);
    }
    protected void LlenarComboParentesco()
    {
        List<Parentesco> lstConsultaParentesco = new List<Parentesco>();
        Parentesco parentesco = new Parentesco();
        ddlParentescoAct.DataSource = parentescoServicio.ListarParentesco(parentesco, (Usuario)Session["usuario"]);
        ddlParentescoAct.DataTextField = "descripcion";
        ddlParentescoAct.DataValueField = "codparentesco";
        ddlParentescoAct.DataBind();
        ddlParentescoAct.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));
    }
    protected void LlenarComboTipoActividad()
    {
        List<AgendaTipoActividad> lstConsultaTipoActividad = new List<AgendaTipoActividad>();
        var idUsuario = Session["IdUsuario"].ToString();

        AgendaTipoActividad tipoActividad = new AgendaTipoActividad();
        ddlTipoActividadReg.DataSource = tipoActividadServicio.ListarAgendaTipoActividad(tipoActividad, (Usuario)Session["usuario"]);
        ddlTipoActividadReg.DataTextField = "descripcion";
        ddlTipoActividadReg.DataValueField = "idtipo";
        ddlTipoActividadReg.DataBind();
        ddlTipoActividadReg.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));

        if (!string.IsNullOrEmpty(idUsuario))
            DropDownListClientes.DataSource = tipoActividadServicio.Listarclientes(Convert.ToInt32(idUsuario), (Usuario)Session["Usuario"]);
        else
            DropDownListClientes.DataSource = tipoActividadServicio.Listarclientes(0, (Usuario)Session["Usuario"]);

        DropDownListClientes.DataTextField = "PrimerNombre";
        DropDownListClientes.DataValueField = "SegundoNombre";
        DropDownListClientes.DataBind();
        DropDownListClientes.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));
    }
    protected void LlenarComboHorasFinal(List<AgendaHora> lstConsultaHorasInicial, Int64 sel)
    {
        List<AgendaHora> lstConsultaHorasFinal = new List<AgendaHora>();
        bool val = false;
        foreach (AgendaHora aghora in lstConsultaHorasInicial)
        {
            if (val)
                lstConsultaHorasFinal.Add(aghora);

            if (aghora.idhora == sel)
                val = true;
        }
        ddlHoraFinalReg.DataSource = lstConsultaHorasFinal;
        ddlHoraFinalReg.DataTextField = "horatipo";
        ddlHoraFinalReg.DataValueField = "idhora";
        ddlHoraFinalReg.DataBind();
        ddlHoraFinalReg.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));
    }
    private string getStrTabla()
    {

        //Vinculo los datos al datagrid

        List<AgendaHora> lstConsultaHoras = new List<AgendaHora>();
        AgendaHora hora = new AgendaHora();
        lstConsultaHoras = agendaHoraServicio.ListarAgendaHora(hora, (Usuario)Session["usuario"]);

        DataTable dt = new DataTable();
        dt.Columns.Add("CodActividad");
        dt.Columns.Add("IdHora");
        dt.Columns.Add("Hora");
        dt.Columns.Add("Tipo");
        dt.Columns.Add("Nombrecliente");
        dt.Columns.Add("TipoActividad");
        dt.Columns.Add("Descripcion");
        dt.Columns.Add("Estado");
        dt.Columns.Add("Respuesta");

        for (int i = 0; i < lstConsultaHoras.Count - 1; i++)
        {
            AgendaHora agendaHora = lstConsultaHoras[i];
            DataRow row = dt.NewRow();
            //revizar si podemos añadir las actividades aca
            row["CodActividad"] = String.Empty;
            row["IdHora"] = agendaHora.idhora;
            row["Hora"] = agendaHora.hora;
            row["Tipo"] = agendaHora.tipo;
            row["Nombrecliente"] = String.Empty;
            row["TipoActividad"] = String.Empty;
            row["Descripcion"] = String.Empty;
            row["Estado"] = String.Empty;
            row["Respuesta"] = String.Empty;
            dt.Rows.Add(row);
        }
        gvListaActividades.AllowPaging = false;
        gvListaActividades.EnableViewState = false;
        gvListaActividades.DataSource = dt;
        gvListaActividades.DataBind();
        //Obtenemos el html
        StringBuilder sb = new StringBuilder();
        StringWriter sw = new StringWriter(sb);
        HtmlTextWriter htmlTW = new HtmlTextWriter(sw);
        //llamamos al metodo RenderControl con el control TextWriter como parametro
        gvListaActividades.RenderControl(htmlTW);
        //vuelco el c¢digo HTML a una variable y la devuelvo
        string dataGridHTML = sb.ToString();
        return dataGridHTML;
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Confirms that an HtmlForm control is rendered for the specified ASP.NETserver control at runtime.*/
    }
    protected void enviarEmail()
    {
        General_Controles_EnviarCorreo correo = new General_Controles_EnviarCorreo();
        EjecutivoService serviceEjecutivo = new EjecutivoService();

        gvListaActividades.DataBind();
        Actualizar();
        AñadirActividades();
        Session.Remove("imprimirCtrl");
        Session["imprimirCtrl"] = gvListaActividades;

        ClientScript.RegisterStartupScript(this.GetType(), "onclick", General_Controles_EnviarCorreo.JSCRIPT_PRINT);

    }
    #endregion

    #region Metodos Tablas

    protected void gvListaActividades_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            ConfirmarEliminarFila(e, "btnBorrar");

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(agendaActividadServicio.CodigoPrograma + "L", "gvListaActividades_RowDataBound", ex);
        }
    }
    protected void gvListaActividades_RowEditing(object sender, GridViewEditEventArgs e)
    {
        Validate("vgGuardar");
        if (Page.IsValid)
        {
            String id = gvListaActividades.Rows[e.NewEditIndex].Cells[0].Text;
            Session["ACTIVIDAD"] = id;
            GridViewRow row = gvListaActividades.Rows[e.NewEditIndex];

            if (row.Cells[0].Text != "&nbsp;" || row.Cells[0].Text != "")
            {
                LlenarComboParentesco();
                txtAtendidoAct.Text = String.Empty;
                txtRespuestaAct.Text = String.Empty;
                txtObservacionesAct.Text = "0";
                LlenarComboTipoActividad();
                HiddenField2.Value = row.Cells[0].Text;
                HiddenField2.Value = row.Cells[1].Text;
                mpeActualizar.Show();
                mpeMensaje.Hide();
                btnConsultar_Click(null, null);
            }
            else
            {
                lblMotivo.Text = "No hay actividad para actualizar.";
                mpeMensaje.Show();
            }
            Actualizar();
            AñadirActividades();
        }
    }
    protected void gvListaActividades_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Validate("vgGuardar");
        if (Page.IsValid)
        {
            try
            {
                GridViewRow row = gvListaActividades.Rows[e.RowIndex];

                if (row.Cells[0].Text != "&nbsp;")
                {
                    Int64 id = Convert.ToInt64(gvListaActividades.Rows[e.RowIndex].Cells[0].Text);
                    agendaActividadServicio.EliminarAgendaActividad(id, (Usuario)Session["usuario"]);
                }
                Actualizar();
                AñadirActividades();
            }
            catch (Xpinn.Util.ExceptionBusiness ex)
            {
                VerError(ex.Message);
            }
            catch (Exception ex)
            {
                BOexcepcion.Throw(agendaActividadServicio.CodigoPrograma, "gvLista_RowDeleting", ex);
            }
        }
    }
    #endregion

    //protected void btnUbicacion_Click(object sender, EventArgs e)
    //{
    //    List<Cliente> lstClientes = new List<Cliente>();
    //    AgendaActividad agActividad = new AgendaActividad();
    //    agActividad.fecha = Convert.ToDateTime(txtFecha.Text);
    //    agActividad.idasesor = Convert.ToInt64(ddlAsesores.SelectedValue);

    //    lstClientes = clienteServicio.ListarUbicacionClientes(agActividad, (Usuario)Session["usuario"]);
    //    if (lstClientes.Count != 0)
    //    {
    //        GMap1.Visible = true;
    //        inicializargMap();
    //        foreach (Cliente iCliente in lstClientes)
    //        {
    //            using (var client = new WebClient())
    //            {
    //                string uri = "http://maps.google.com/maps/geo?q='" + iCliente.nomciudad + " " + iCliente.Direccion +
    //                  "'&output=csv&key=ABQIAAAAzr2EBOXUKnm_jVnk0OJI7xSosDVG8KKPE1" +
    //                  "-m51RBrvYughuyMxQ-i1QfUnH94QxWIa6N4U6MouMmBA";

    //                string[] geocodeInfo = client.DownloadString(uri).Split(',');
    //                ubicarClientesgMap(iCliente, geocodeInfo[2], geocodeInfo[3]);
    //            }
    //        }
    //    }
    //}

    //private void inicializargMap()
    //{
    //    GMap1.reset();
    //    GMap1.enableDragging = true;
    //    GMap1.enableGoogleBar = true;
    //    GMap1.Language = "es";
    //    GMap1.BackColor = Color.White;
    //    GMap1.Height = 600;
    //    GMap1.Width = 1000;
    //    GMap1.enableHookMouseWheelToZoom = true;
    //    GMap1.enableRotation = true;

    //    using (var client = new WebClient())
    //    {
    //        string uri = "http://maps.google.com/maps/geo?q='" + "Bogotá " +
    //          "'&output=csv&key=ABQIAAAAzr2EBOXUKnm_jVnk0OJI7xSosDVG8KKPE1" +
    //          "-m51RBrvYughuyMxQ-i1QfUnH94QxWIa6N4U6MouMmBA";

    //        string[] geocodeInfo = client.DownloadString(uri).Split(',');

    //        Double lat = Convert.ToDouble(geocodeInfo[2], CultureInfo.CreateSpecificCulture("en-US"));
    //        Double lon = Convert.ToDouble(geocodeInfo[3], CultureInfo.CreateSpecificCulture("en-US"));
    //        GMap1.setCenter(new GLatLng(lat, lon), 6);
    //    }

    //    GMap1.addGMapUI(new GMapUI());
    //}

    //private void ubicarClientesgMap(Cliente iCliente, string latitud, string longitud)
    //{
    //    Double lat = Convert.ToDouble(latitud, CultureInfo.CreateSpecificCulture("en-US"));
    //    Double lon = Convert.ToDouble(longitud, CultureInfo.CreateSpecificCulture("en-US"));

    //    GLatLng latlon = new GLatLng(lat, lon);
    //    GMap1.setCenter(latlon, 14);

    //    GMarker icono = new GMarker(latlon);
    //    GInfoWindow window = new GInfoWindow(icono, "INFORMACIÓN<br/><br/>Cliente:<br/>" + iCliente.NombreCompleto + "<br/>Ubicación:<br/>" + iCliente.nomciudad + "<br/>Dirección:<br/>" + iCliente.Direccion + "<br/>Teléfono:<br/>" + iCliente.Telefono + "<br/>LatLng:<br/>" + latlon, false, GListener.Event.mouseover);
    //    GMap1.addInfoWindow(window);
    //}



}