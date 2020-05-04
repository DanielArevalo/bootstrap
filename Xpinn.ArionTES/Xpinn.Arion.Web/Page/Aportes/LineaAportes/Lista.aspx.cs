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
using Xpinn.Aportes.Entities;
using Xpinn.Aportes.Services;
using System.IO;

partial class Lista : GlobalWeb
{
    private Xpinn.Aportes.Services.GrupoLineaAporteServices LineaAporteServicio = new Xpinn.Aportes.Services.GrupoLineaAporteServices();
   

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(LineaAporteServicio.CodigoProgramaLineas, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoNuevo += btnNuevo_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
            toolBar.eventoImportar += btnImportar_Click;
            toolBar.eventoGuardar += btnGuardar_Click;            
            ctlMensaje.eventoClick += btnContinuarMen_Click;
            toolBar.MostrarGuardar(false);
            divfecha.Visible = false;
            divffecha.Visible = false;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(LineaAporteServicio.CodigoProgramaLineas, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {               
                Actualizar();         
                if (Session[LineaAporteServicio.CodigoProgramaLineas + ".consulta"] != null)
                    Actualizar();                
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(LineaAporteServicio.CodigoProgramaLineas, "Page_Load", ex);
        }
    }

    protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    {
        GuardarValoresConsulta(pConsulta, LineaAporteServicio.CodigoProgramaLineas);
        Navegar(Pagina.Nuevo);
      Session["operacion"] = "N";
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        GuardarValoresConsulta(pConsulta, LineaAporteServicio.CodigoProgramaLineas);
        Actualizar();
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        LimpiarValoresConsulta(pConsulta, LineaAporteServicio.CodigoProgramaLineas);
        txtCodLinea.Text = "";
        txtNombreLinea.Text = "";     
        gvLista.DataBind();
   
    }

    protected void gvLista_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            //ConfirmarEliminarFila(e, "btnBorrar");
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(LineaAporteServicio.CodigoProgramaLineas + "L", "gvLista_RowDataBound", ex);
        }
    }

    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        String id = gvLista.DataKeys[gvLista.SelectedRow.RowIndex].Value.ToString();
        Session[LineaAporteServicio.CodigoProgramaLineas + ".id"] = id;
        Navegar(Pagina.Editar);

    }

    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
     
        String id = gvLista.DataKeys[e.NewEditIndex].Value.ToString();
        Session[LineaAporteServicio.CodigoProgramaLineas + ".id"] = id;
        Navegar(Pagina.Editar);
        Session["LineaAportes"] = "";     
    }

    protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        
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
            BOexcepcion.Throw(LineaAporteServicio.CodigoProgramaLineas, "gvLista_PageIndexChanging", ex);
        }
    }   

    private void Actualizar()
    {
      
        try
        {
            List<Xpinn.Aportes.Entities.GrupoLineaAporte> lstConsulta = new List<Xpinn.Aportes.Entities.GrupoLineaAporte>();
            lstConsulta = LineaAporteServicio.ListarLineaAporte(ObtenerValores(), (Usuario)Session["usuario"]);
        
            gvLista.PageSize = pageSize;
            gvLista.EmptyDataText = emptyQuery;
            gvLista.DataSource = lstConsulta;

            if (lstConsulta.Count > 0)
            {
                gvLista.Visible = true;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                gvLista.DataBind();
                ValidarPermisosGrilla(gvLista);
            }
            else
            {
                gvLista.Visible = false;
                lblTotalRegs.Visible = false;
            }

            Session.Add(LineaAporteServicio.CodigoProgramaLineas + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(LineaAporteServicio.CodigoProgramaLineas, "Actualizar", ex);
        }
        
    }

    private Xpinn.Aportes.Entities.GrupoLineaAporte ObtenerValores()
    {
        Xpinn.Aportes.Entities.GrupoLineaAporte vAporte = new Xpinn.Aportes.Entities.GrupoLineaAporte();
        if (txtCodLinea.Text.Trim() != "")
            vAporte.cod_linea_aporte = Convert.ToInt32(txtCodLinea.Text.Trim());
        if (txtNombreLinea.Text.Trim() != "")
            vAporte.nombre = Convert.ToString(txtNombreLinea.Text.Trim());
        if (DdlEstado.SelectedValue.Trim() != "")
            vAporte.estado = Convert.ToInt32(DdlEstado.SelectedValue);

            return vAporte;
    }    

    protected void btnInfo_Click(object sender, ImageClickEventArgs e)
    {

    }

    protected void DdlOrdenadorpor_SelectedIndexChanged(object sender, EventArgs e)
    {
        Actualizar();
    }



    #region CODIGO DE IMPORTACION

    void CargaFormatosFecha()
    {
        ddlFormatoFecha.Items.Insert(0, new ListItem("Seleccione un Item", "0"));
        ddlFormatoFecha.Items.Insert(1, new ListItem("dd/MM/yyyy", "dd/MM/yyyy"));
        ddlFormatoFecha.Items.Insert(2, new ListItem("yyyy/MM/dd", "yyyy/MM/dd"));
        ddlFormatoFecha.Items.Insert(3, new ListItem("MM/dd/yyyy", "MM/dd/yyyy"));
        ddlFormatoFecha.Items.Insert(4, new ListItem("ddMMyyyy", "ddMMyyyy"));
        ddlFormatoFecha.Items.Insert(5, new ListItem("yyyyMMdd", "yyyyMMdd"));
        ddlFormatoFecha.Items.Insert(6, new ListItem("MMddyyyy", "MMddyyyy"));
        ddlFormatoFecha.SelectedIndex = 0;
        ddlFormatoFecha.DataBind();
    }

    void LimpiarDataImportacion()
    {
        //pErrores.Visible = false;
        gvDatos.DataSource = null;
        ucFecha.Text = DateTime.Now.ToShortDateString();
        //ddlFormatoFecha.SelectedIndex = 0;
        Site toolBar = (Site)Master;
        toolBar.MostrarGuardar(false);
    }

    protected void btnImportar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        CargaFormatosFecha();
        LlenarComboLineaAporte(this.DdlLineaAporte);
        mvPrincipal.ActiveViewIndex = 1;
        panelData.Visible = false;
        Site toolBar = (Site)Master;
        toolBar.MostrarNuevo(false);
        toolBar.MostrarConsultar(false);
        toolBar.MostrarLimpiar(true);
        toolBar.MostrarImportar(false);
        LimpiarDataImportacion();
    }

    protected void btnCargarAportes_Click(object sender, EventArgs e)
    {
        try
        {
            VerError("");
            string error = "";
            //if (ddlFormatoFecha.SelectedIndex == 0)
            //{
            //    VerError("Seleccione el tipo de fecha que se carga en el archivo.");
            //    ddlFormatoFecha.Focus();
            //    return;
            //}
            //if (ucFecha.Text == "")
            //{
            //    VerError("Ingrese la fecha de carga");
            //    ucFecha.Focus();
            //    return;
            //}
            if (DdlLineaAporte.SelectedIndex == 0)
            {
                VerError("Ingrese la linea de los Aportes");
                DdlLineaAporte.Focus();
                
                return;
            }
            if (fupArchivoPersona.HasFile)
            {
                Stream stream = fupArchivoPersona.FileContent;
                List<Xpinn.Aportes.Entities.ErroresCargaAportes> plstErrores = new List<Xpinn.Aportes.Entities.ErroresCargaAportes>();
                List<Xpinn.Aportes.Entities.GrupoLineaAporte> lstAportes = new List<Xpinn.Aportes.Entities.GrupoLineaAporte>();

                //LLAMANDO AL METODO DE CAPTURA DE DATOS
                LineaAporteServicio.CargaAportes(ref error, ddlFormatoFecha.SelectedValue, stream, ref lstAportes, ref plstErrores, (Usuario)Session["usuario"]);
                
                
                if (error.Trim() != "")
                {
                    VerError(error);
                    return;
                }
                if (plstErrores.Count() > 0)
                {
                    pErrores.Visible = true;
                    gvErrores.DataSource = plstErrores;
                    gvErrores.DataBind();
                    cpeDemo.CollapsedText = "(Click Aqui para ver " + plstErrores.Count() + " errores...)";
                    cpeDemo.ExpandedText = "(Click Aqui para ocultar listado de errores...)";
                }
                panelData.Visible = false;
                if (lstAportes.Count > 0)
                {
                    Session["lstData"] = lstAportes;
                    panelData.Visible = true;
                    //CARGAR DATOS A GRILLA DE NATURALES
                    gvDatos.DataSource = lstAportes;
                    gvDatos.DataBind();
                    Site toolBar = (Site)this.Master;
                    toolBar.MostrarGuardar(true);
                }

            }
            else
            {
                VerError("Seleccione el archivo a cargar, verifique los datos.");
                return;
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(LineaAporteServicio.CodigoProgramaLineas + "L", "btnCargarAportes_Click", ex);
        }
    }

    protected void gvDatos_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            List<Xpinn.Aportes.Entities.GrupoLineaAporte> lstAportes = new List<Xpinn.Aportes.Entities.GrupoLineaAporte>();
            lstAportes = (List<Xpinn.Aportes.Entities.GrupoLineaAporte>)Session["lstData"];

            lstAportes.RemoveAt((gvDatos.PageIndex * gvDatos.PageSize) + e.RowIndex);

            gvDatos.DataSourceID = null;
            gvDatos.DataBind();

            gvDatos.DataSource = lstAportes;
            gvDatos.DataBind();
            Session["lstData"] = lstAportes;
            Site toolBar = (Site)this.Master;
            toolBar.MostrarGuardar(true);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(LineaAporteServicio.CodigoProgramaLineas, "gvDatos_RowDeleting", ex);
        }
    }

    Boolean ValidarData()
    {
        if (ucFecha.Text == "")
        {
            VerError("Ingrese la fecha de carga");
            ucFecha.Focus();
            return false;
        }
        if (gvDatos.Rows.Count <= 0)
        {
            VerError("No existen datos por registrar, verifique los datos.");
            return false;
        }
        return true;
    }

    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            VerError("");
            if (ValidarData())
                ctlMensaje.MostrarMensaje("Desea realizar la grabación de datos?");
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(LineaAporteServicio.CodigoProgramaLineas, "btnGuardar_Click", ex);
        }
    }

    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        try
        {
            VerError("");
            string cod_linea;
            List<Xpinn.Aportes.Entities.GrupoLineaAporte> lstAportes = new List<Xpinn.Aportes.Entities.GrupoLineaAporte>();            
            lstAportes = (List<Xpinn.Aportes.Entities.GrupoLineaAporte>)Session["lstData"];
            cod_linea = DdlLineaAporte.SelectedValue;
            List<Int64> lst_Num_Apor = new List<Int64>();


            string pError = "";
            LineaAporteServicio.CrearImportacion(ucFecha.ToDateTime, ref pError, lstAportes, (Usuario)Session["usuario"], cod_linea, ref lst_Num_Apor);
            //coun de la lista
                     
            if (pError != "")
            {
                VerError(pError);
                return;
            }



            if (lst_Num_Apor.Count > 0)
            {
                 foreach (int element in lst_Num_Apor)
                    {
                    //CARGAR DATOS A GRILLA DE APORTES NO IMPORTADOS 
                    GridView1.DataSource = lst_Num_Apor;
                    GridView1.DataBind();
                    GridView1.HeaderRow.Cells[0].Text = "Número Aporte";
                    infApor_no.Visible = true;
                }
            }
            else
            {
                infApor_no.Visible = false;
            }

            mvPrincipal.ActiveViewIndex = 2;
            Site toolBar = (Site)this.Master;
            toolBar.MostrarGuardar(false);
            toolBar.MostrarConsultar(false);
            toolBar.MostrarLimpiar(false);
            Session.Remove("lstData");
            Session.Remove("lstData2");
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(LineaAporteServicio.CodigoProgramaLineas, "btnContinuarMen_Click", ex);
        }
    }

    #endregion

    protected void DdlLineaAporte_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DdlLineaAporte.SelectedValue != "0")
        {

        }
    }

    protected void LlenarComboLineaAporte(DropDownList DdlLineaApo)
    {

        AporteServices aporteService = new AporteServices();
        Usuario usuap = (Usuario)Session["usuario"];
        Aporte aporte = new Aporte();
        DdlLineaApo.DataSource = aporteService.ListarLineaAporte(aporte, (Usuario)Session["usuario"]);
        DdlLineaApo.DataTextField = "nom_linea_aporte";
        DdlLineaApo.DataValueField = "cod_linea_aporte";
        DdlLineaApo.DataBind();
        DdlLineaApo.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));

    }

}