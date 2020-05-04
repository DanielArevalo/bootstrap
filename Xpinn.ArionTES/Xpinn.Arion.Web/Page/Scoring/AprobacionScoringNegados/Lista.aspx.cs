using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using Xpinn.Scoring.Services;
using Xpinn.Scoring.Entities;
using Microsoft.Reporting.WebForms;

public partial class Lista : GlobalWeb
{
    
    AprobacionScoringNegadosService AprobacionScoringNegadosServicio = new AprobacionScoringNegadosService();
    String ListaSolicitada = null;  // Cadena en la que se indica la lista a solicitar
    List<AprobacionScoringNegados> lstDatosSolicitud = new List<AprobacionScoringNegados>();  //Lista de los menus desplegables
    List<AprobacionScoringNegados> lstConsulta;

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            VisualizarOpciones(AprobacionScoringNegadosServicio.CodigoPrograma, "L");
            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoGuardar += btnGuardar_Click;
            
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AprobacionScoringNegadosServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        ListaSolicitada = "MotivosAprobacion";
        try
        {
            if (!IsPostBack)
            {
                CargarValoresConsulta(pConsulta, AprobacionScoringNegadosServicio.GetType().Name);
                if (Session[AprobacionScoringNegadosServicio.CodigoPrograma + ".consulta"] != null)
                    
                    CargarListas();
                    Actualizar();
                    mvAprobacionScoringNegados.ActiveViewIndex = 0;
            }
        }

        catch (Exception ex)
        {
            BOexcepcion.Throw(AprobacionScoringNegadosServicio.CodigoPrograma, "Page_Load", ex);
        }
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        
        {
            Page.Validate();

            if (Page.IsValid)
            {
                GuardarValoresConsulta(pConsulta, AprobacionScoringNegadosServicio.CodigoPrograma);
                if (mvAprobacionScoringNegados.ActiveViewIndex != 0)
                {
                    mvAprobacionScoringNegados.ActiveViewIndex = 0;
                    ViewState["id"] = null;
                }                   

                Actualizar();
                CargarListas();
            }
            
        }       
    }

   
    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        LimpiarValoresConsulta(pConsulta, AprobacionScoringNegadosServicio.CodigoPrograma);
    }

    /// <summary>
    /// Evento para cargar valores a la grilla.
    /// </summary>
    private void Actualizar()
    {
        try
        {
            lstConsulta = new List<AprobacionScoringNegados>();
            AprobacionScoringNegados credito = new AprobacionScoringNegados();
            String filtro = obtFiltro(ObtenerValores());
            lstConsulta = AprobacionScoringNegadosServicio.ListarAprobScoringNegados(credito, (Usuario)Session["usuario"], filtro);

            gvLista.DataSource = lstConsulta;
            if (lstConsulta.Count > 0)
            {
                gvLista.Visible = true;
                lblInfo.Visible = false;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                gvLista.DataBind();
               // ValidarPermisosGrilla(gvLista);
            }
            else
            {
                gvLista.Visible = false;
                lblInfo.Visible = true;
                lblTotalRegs.Visible = false;
            }

            Session.Add(AprobacionScoringNegadosServicio.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AprobacionScoringNegadosServicio.CodigoPrograma, "Actualizar", ex);
        }
    }


    /// <summary>
    /// Esta función permite mostrar el plan de pagos del crédito seleccionado
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        //Muestra el multiview con el detalle de la selecci
        ViewState["id"] = gvLista.SelectedRow.Cells[1].Text;
        Actualizar();
             
        mvAprobacionScoringNegados.ActiveViewIndex = 1;
    }


    /// <summary>
    /// Esta función actualiza la grilla de créditos al ir a la siguiente página de datos de la grilla
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvLista.PageIndex = e.NewPageIndex;
            Actualizar();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AprobacionScoringNegadosServicio.CodigoPrograma, "gvLista_PageIndexChanging", ex);
        }
    }

    /// <summary>
    /// Evento para obtener los filtros ingresados por el usuario para realizar la consulta
    /// </summary>
    /// <param name="credito">Clase que tiene los datos del filtro</param>
    /// <returns>Retorna los filtros a aplicar</returns>
    private string obtFiltro(AprobacionScoringNegados credito)
    {
        String filtro = String.Empty;
       
        if (txtCredito.Text.Trim() != "")
            filtro += " where numero_radicacion=" + txtCredito.Text.Trim();        
        if (txtCliente.Text.Trim() != "")
            filtro += " where nombres LIKE " + "'" + txtCliente.Text.Trim() + "%'";
        if (txtIdentificacion.Text.Trim() != "")
            filtro += " where identificacion LIKE " + "'" + txtIdentificacion.Text.Trim() + "%'";

        if (filtro != "")
        {
            string filtro1 = filtro.Substring(7, filtro.Length - 7).Replace(" where ", " and ");
            filtro = " where " + filtro1;
        }
            
        return filtro;
    }

    private AprobacionScoringNegados ObtenerValores()
    {
        AprobacionScoringNegados credito = new AprobacionScoringNegados();
        //if (txtCredito.Text.Trim() != "")
        //    credito.Numero_radicacion = Convert.ToInt32(txtCredito.Text.Trim());
        return credito;
    }

    /// <summary>
    /// Cargar información de las listas desplegables
    /// </summary>
    private void CargarListas()
    {
        try
        {
            
            TraerResultadosLista();
            ddlMotivo.DataSource = lstDatosSolicitud;            
            ddlMotivo.DataTextField = "ListaDescripcion";
            ddlMotivo.DataValueField = "ListaId";            
            ddlMotivo.DataBind();
        

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AprobacionScoringNegadosServicio.GetType().Name + "L", "CargarListas", ex);
        }
    }

    private void TraerResultadosLista()
    {
        lstDatosSolicitud.Clear();
        lstDatosSolicitud = AprobacionScoringNegadosServicio.ListasDesplegables(ListaSolicitada, (Usuario)Session["Usuario"]);

    }
    protected void btnImprimir_Click(object sender, ImageClickEventArgs e)
    {

    }

    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        //Modifica el estado de los creditos seleccionados (Los pasa de N a L). Añade registro de auditoria en la tabla "ControlCreditos"
        
        foreach (GridViewRow rw in gvLista.Rows)
        {
            CheckBox chkBx = (CheckBox)rw.FindControl("chkBxSelect");
            if (chkBx != null && chkBx.Checked)
            {
                AprobacionScoringNegados pAprobacionScoringNegados = new AprobacionScoringNegados();
                pAprobacionScoringNegados.idControl = 0;
                pAprobacionScoringNegados.NumeroSolicitud = Convert.ToInt64(rw.Cells[0].Text.ToString());
                pAprobacionScoringNegados.codTipoProceso = "3";
                pAprobacionScoringNegados.fechaProceso = DateTime.Now;
                pAprobacionScoringNegados.codMotivo = Convert.ToInt64(ddlMotivo.SelectedValue.ToString());
                pAprobacionScoringNegados.observaciones = txtObservacion.Text;

                AprobacionScoringNegadosServicio.ModificarCredito(pAprobacionScoringNegados, (Usuario)Session["usuario"]);               
                txtObservacion.Text = "";
                Actualizar();
           
            }
        }
       
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        mvAprobacionScoringNegados.ActiveViewIndex = 1;
        ActualizarMotivos();
       
    }


    private void ActualizarMotivos()

    {

        try
        {
            
            List<AprobacionScoringNegados> lstConsulta = new List<AprobacionScoringNegados>();
            lstConsulta = AprobacionScoringNegadosServicio.ListasDesplegables(ListaSolicitada, (Usuario)Session["Usuario"]);
            //gvMotivos.PageSize = Convert.ToInt32(ConfigurationManager.AppSettings["pageSize"]);
            gvMotivos.DataSource = lstConsulta;

            if (lstConsulta.Count > 0)
            {
                gvMotivos.Visible = true;
                lblInfo0.Visible = false;
                lblTotalRegs0.Visible = true;
                lblTotalRegs0.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                gvMotivos.DataBind();
                //ValidarPermisosGrilla(gvMotivos);
            }
            //else
            //{
            //    lblTotalRegs0.Text = "<br/> Registros encontrados: 0 ";
            //    AprobacionScoringNegados vAprobacionScoringNegados = new AprobacionScoringNegados();
            //    vAprobacionScoringNegados.idscorevar = 0;
            //    vAprobacionScoringNegados.idscore = 0;


            //    lstConsulta.Add(vAprobacionScoringNegados);
            //    gvMotivos.DataBind();
            //    gvMotivos.Rows[0].Visible = false;
            //    //gvMotivos.DeleteRow(0);
            //}

            Session.Add(AprobacionScoringNegadosServicio.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AprobacionScoringNegadosServicio.CodigoPrograma, "ActualizarVar", ex);
        }
    }
    protected void gvMotivos_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        // Guarda (Modifica) los valores que han sido editados desde el gridview
        long conseID = Convert.ToInt32(gvMotivos.DataKeys[e.RowIndex].Values[0].ToString());
        TextBox txtDescripcion = (TextBox)gvMotivos.Rows[e.RowIndex].FindControl("txtDescripcionE");

        AprobacionScoringNegados pScScoringBoardVar = new AprobacionScoringNegados();
        pScScoringBoardVar.ListaId = conseID;
        pScScoringBoardVar.ListaDescripcion = txtDescripcion.Text.Trim();

        gvMotivos.EditIndex = -1;
        AprobacionScoringNegadosServicio.ModificarMotivo(pScScoringBoardVar, (Usuario)Session["usuario"]);
        ActualizarMotivos();
    }


    protected void gvMotivos_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvMotivos.EditIndex = -1;
        long conseID = Convert.ToInt32(gvMotivos.DataKeys[e.RowIndex].Values[0].ToString());

        String consecutivo = conseID.ToString();


        MostrarMotivos(consecutivo, gvMotivos, "Lineas");
        ActualizarMotivos();
    }
    private void MostrarMotivos(String consecutivo, GridView gvVariables, String Var)
    {
        //Muestra el gridview con sus datos
        AprobacionScoringNegados pScoringBoard = new AprobacionScoringNegados();
        List<AprobacionScoringNegados> LstScoringBoard = new List<AprobacionScoringNegados>();
        LstScoringBoard = AprobacionScoringNegadosServicio.ListasDesplegables(ListaSolicitada, (Usuario)Session["Usuario"]);
        //if (LstScoringBoard.Count == 0)
        //    {
        //        LstScoringBoard = (List<ScScoringBoardVar>)Session["Clasificaciones"];
        //    }

        gvMotivos.DataSource = LstScoringBoard;
        gvMotivos.DataBind();
        Session[Var] = LstScoringBoard;
    }
    protected void gvMotivos_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName.Equals("AddNew"))
        {
            TextBox txtDescripcionF = (TextBox)gvMotivos.FooterRow.FindControl("txtDescripcionF");

            //if (ddlVariableF.SelectedItem.Text == "")
            //{
            //    String Error = "Por favor diligenciar los datos";
            //    this.Lblerror.Text = Error;
            //    quitarfilainicialGarantiasReales();
            //}
            //else
            //{
                //this.Lblerror.Text = "";
            AprobacionScoringNegados vAprobacionScoringNegados = new AprobacionScoringNegados();
            vAprobacionScoringNegados.ListaDescripcion = txtDescripcionF.Text.Trim() != "" ? txtDescripcionF.Text.Trim() : "";               
                gvMotivos.EditIndex = -1;
                AprobacionScoringNegadosServicio.CrearMotivo(vAprobacionScoringNegados, (Usuario)Session["usuario"]);
                ActualizarMotivos();
            //}

        }
    }
    protected void gvMotivos_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void gvMotivos_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        long ID = Convert.ToInt32(gvMotivos.DataKeys[e.RowIndex].Values[0].ToString());
        if (ID != 0)
        {
            AsignarEventoConfirmar();
            AprobacionScoringNegadosServicio.EliminarMotivo(ID, (Usuario)Session["usuario"]);
            ActualizarMotivos();
        }
    }
    protected void AsignarEventoConfirmar()
    {
        ConfirmarEventoBoton((LinkButton)Master.FindControl("btnEliminar"), "Esta seguro que desea eliminar el registro?");
    }
    protected void gvMotivos_RowEditing(object sender, GridViewEditEventArgs e)
    {
        int conseID = Convert.ToInt32(gvMotivos.DataKeys[e.NewEditIndex].Values[0].ToString());
        if (conseID != 0)
        {
            gvMotivos.EditIndex = e.NewEditIndex;
            this.ConsultarMotivos();
        }
    }
    
    private void ConsultarMotivos()
    {
        MostrarMotivos("consecutivo", gvMotivos, "Lineas");
    }

    
}