using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Text;
using System.Web.UI.HtmlControls;
using Xpinn.Util;
using Xpinn.Aportes.Entities;
using Xpinn.Aportes.Services;


partial class Lista : GlobalWeb
{
    private Xpinn.Aportes.Services.AporteServices AporteServicio = new Xpinn.Aportes.Services.AporteServices();
    private Xpinn.Aportes.Services.AfiliacionServices AfiliacionServicio = new Xpinn.Aportes.Services.AfiliacionServices();
    ImagenesService imagenService = new ImagenesService();
    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(AporteServicio.ProgramaAperturaAporte, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoNuevo += btnNuevo_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
            toolBar.eventoImportar += btnImportar_Click;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoExportar += btnExportar_Click;
            toolBar.MostrarGuardar(false);
            ctlMensaje.eventoClick += btnContinuarMen_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AporteServicio.ProgramaAperturaAporte, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                CargaFormatosFecha();
                Session["lstData"] = null;
                LlenarComboLineaAporte(DdlLineaAporte);               
                //CargarValoresConsulta(pConsulta, AporteServicio.ProgramaAperturaAporte);
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AporteServicio.ProgramaAperturaAporte, "Page_Load", ex);
        }
    }

    protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    {
        Session.Remove("cedula");
        Session.Remove(imagenService.CodigoPrograma.ToString() + ".cod_persona");
        Session.Remove(AfiliacionServicio.CodigoPrograma + "last");
        Session.Remove(AfiliacionServicio.CodigoPrograma + "next");
        Session.Remove(AfiliacionServicio.CodigoPrograma + ".id");
        GuardarValoresConsulta(pConsulta, AporteServicio.ProgramaAperturaAporte);
        Navegar(Pagina.Nuevo);
        Session[AporteServicio.ProgramaAperturaAporte + ".id"] = null;
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        if (mvPrincipal.ActiveViewIndex == 0)
        {
            GuardarValoresConsulta(pConsulta, AporteServicio.ProgramaAperturaAporte);
            Actualizar();
        }
        else
        {
            Session["lstData"] = null;
            mvPrincipal.ActiveViewIndex = 0;
            Site toolBar = (Site)Master;
            toolBar.MostrarLimpiar(true);
            toolBar.MostrarImportar(true);
            toolBar.MostrarNuevo(true);
            toolBar.MostrarGuardar(false);
        }
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        if (mvPrincipal.ActiveViewIndex == 0)
        {
            LimpiarValoresConsulta(pConsulta, AporteServicio.ProgramaAperturaAporte);
            txtNombre.Text = "";
            txtNumAporte.Text = "";
            txtNumeIdentificacion.Text = "";
            gvLista.DataBind();
        }
        else
            LimpiarDataImportacion();
   
    }

    protected void gvLista_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            //ConfirmarEliminarFila(e, "btnBorrar");
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AporteServicio.ProgramaAperturaAporte + "L", "gvLista_RowDataBound", ex);
        }
    }

    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        String id = gvLista.DataKeys[gvLista.SelectedRow.RowIndex].Value.ToString();
        Session[AporteServicio.ProgramaAperturaAporte + ".id"] = id;
        Navegar(Pagina.Detalle);

    }

    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvLista.DataKeys[e.NewEditIndex].Value.ToString();
        Session[AporteServicio.ProgramaAperturaAporte + ".id"] = id;
        Navegar(Pagina.Editar);             
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
            BOexcepcion.Throw(AporteServicio.ProgramaAperturaAporte, "gvLista_PageIndexChanging", ex);
        }
    }

    private void ConsultarCliente(String pIdObjeto)
    {
        Xpinn.Aportes.Services.AporteServices AportesServicio = new Xpinn.Aportes.Services.AporteServices();
        Xpinn.Aportes.Entities.Aporte aporte = new Xpinn.Aportes.Entities.Aporte();
        String IdObjeto=txtNumeIdentificacion.Text;
        aporte = AportesServicio.ConsultarClienteAporte(IdObjeto,0, (Usuario)Session["usuario"]);
        if (aporte.nombre != null)
            if (!string.IsNullOrEmpty(aporte.nombre.ToString()))
                txtNombre.Text = HttpUtility.HtmlDecode(aporte.nombre);        
    }

    private void Actualizar()
    {
      
        try
        {
            List<Xpinn.Aportes.Entities.Aporte> lstConsulta = new List<Xpinn.Aportes.Entities.Aporte>();
            lstConsulta = AporteServicio.ListarAperturaAporte(ObtenerValores(), (Usuario)Session["usuario"], "V_APORTES." + DdlOrdenadorpor.SelectedValue);
            List<Xpinn.Aportes.Entities.Aporte> lstDiasCategoria = new List<Xpinn.Aportes.Entities.Aporte>();
            gvLista.PageSize = pageSize;
            gvLista.EmptyDataText = emptyQuery;
            gvLista.DataSource = lstConsulta;
            Aporte pEntidad = new Aporte();

            if (lstConsulta.Count > 0)
            {
                gvLista.Visible = true;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                gvLista.DataBind();
                ValidarPermisosGrilla(gvLista);

                foreach (Aporte item in lstConsulta)
                {
                    DateTime fecha1 = item.fecha_proximo_pago;
                    DateTime fecha2 = item.fecha_cierre;
                    TimeSpan Diferencia = fecha1.Subtract(fecha2);
                    Int64 DiasMora = Convert.ToInt32(Diferencia.Days);
                    pEntidad.DiasMora =Math.Abs(DiasMora);
                    //lstDiasCategoria = AporteServicio.ListarDiasCategoria(item.Cod_Clasificacion, (Usuario)Session["usuario"]);
                    foreach (Aporte rfila in lstDiasCategoria)
                    {
                        if (rfila.Dias_Minimo<=pEntidad.DiasMora && rfila.Dias_Maximo>=pEntidad.DiasMora)
                        {
                            pEntidad.Cod_Categoria = rfila.Cod_Categoria;
                            pEntidad.numero_aporte = item.numero_aporte;
                          //pEntidad= AporteServicio.ClasificarPorDiasMora(pEntidad, (Usuario)Session["usuario"]);
                        }
                    }
                }
            }
            else
            {
                gvLista.Visible = false;
                lblTotalRegs.Visible = false;
            }
            if (txtNumeIdentificacion.Text != "")
            {
                ConsultarCliente(txtNumeIdentificacion.Text);
            }
            else
            {
                txtNombre.Text = "";
            }
            Session["DTAPORTES"] = lstConsulta; 
            Session.Add(AporteServicio.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AporteServicio.CodigoPrograma, "Actualizar", ex);
        }
        
    }

    private Xpinn.Aportes.Entities.Aporte ObtenerValores()
    {
        Xpinn.Aportes.Entities.Aporte vAporte = new Xpinn.Aportes.Entities.Aporte();
        if (txtNumAporte.Text.Trim() != "")
            vAporte.numero_aporte = Convert.ToInt64(txtNumAporte.Text.Trim());
        if  (txtNumeIdentificacion.Text.Trim() != "")
            vAporte.identificacion = Convert.ToString(txtNumeIdentificacion.Text.Trim());
        if (DdlLineaAporte.SelectedValue.Trim() != "")
            vAporte.cod_linea_aporte = Convert.ToInt32(DdlLineaAporte.SelectedValue);
        if (DdlEstado.SelectedValue.Trim() != "")
            vAporte.estado = Convert.ToInt32(DdlEstado.SelectedValue);
        if (txtFecha_apertura.Text.Trim() != "")
            vAporte.fecha_apertura = Convert.ToDateTime(txtFecha_apertura.Text.Trim());
        if (txtFecha_vencimiento.Text.Trim() != "")
            vAporte.fecha_proximo_pago = Convert.ToDateTime(txtFecha_vencimiento.Text.Trim());
        if (txtCodigoNomina.Text.Trim() != "")
            vAporte.cod_nomina = Convert.ToString(txtCodigoNomina.Text.Trim());

        return vAporte;
    }

    protected void LlenarComboLineaAporte(DropDownList ddlOficina)
    {
          Xpinn.Aportes.Services.AporteServices aporteService = new  Xpinn.Aportes.Services.AporteServices();       
          Usuario usuap = (Usuario)Session["usuario"];
          Xpinn.Aportes.Entities.Aporte aporte = new Xpinn.Aportes.Entities.Aporte();
          DdlLineaAporte.DataSource = aporteService.ListarLineaAporte(aporte, (Usuario)Session["usuario"]);
          DdlLineaAporte.DataTextField = "nom_linea_aporte";
          DdlLineaAporte.DataValueField = "cod_linea_aporte";       
          DdlLineaAporte.DataBind();
          DdlLineaAporte.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));       

    }

    protected void btnInfo_Click(object sender, ImageClickEventArgs e)
    {

    }

    protected void DdlOrdenadorpor_SelectedIndexChanged(object sender, EventArgs e)
    {
        Actualizar();
    }
    protected void DdlLineaAporte_SelectedIndexChanged(object sender, EventArgs e)
    {
        Actualizar();
    }


    protected void btnExportar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        if (gvLista.Rows.Count > 0 && Session["DTAPORTES"] != null)
        {
            ExportarCSV();
        }
        else
        {
            VerError("No existen datos, genere la consulta");
        }
    }


    protected void ExportarCSV()
    {
        try
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition",
             "attachment;filename=Revalorizacion.csv");
            Response.Charset = "";
            Response.ContentType = "application/text";
            gvLista.AllowPaging = false;
            gvLista.DataSource = Session["DTAPORTES"];
            gvLista.DataBind();

            StringBuilder sb = ExportarGridCSV(gvLista);
            Response.Output.Write(sb.ToString());
            Response.Flush();
            gvLista.AllowPaging = true;
            gvLista.DataSource = Session["DTAPORTES"];
            gvLista.DataBind();

            Response.End();
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
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
        pErrores.Visible = false;
        gvDatos.DataSource = null;        
        ucFecha.Text = DateTime.Now.ToShortDateString();
        ddlFormatoFecha.SelectedIndex = 0;        
        Site toolBar = (Site)Master;
        toolBar.MostrarGuardar(false);
    }


    protected void btnImportar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        mvPrincipal.ActiveViewIndex = 1;
        panelData.Visible = false;
        Site toolBar = (Site)Master;
        toolBar.MostrarLimpiar(true);
        toolBar.MostrarImportar(false);
        toolBar.MostrarNuevo(false);
        LimpiarDataImportacion();
    }


    protected void btnCargarAportes_Click(object sender, EventArgs e)
    {
        try
        {
            VerError("");
            string error = "";
            if (ddlFormatoFecha.SelectedIndex == 0)
            {
                VerError("Seleccione el tipo de fecha que se carga en el archivo.");
                ddlFormatoFecha.Focus();
                return;
            }
            if (ucFecha.Text == "")
            {
                VerError("Ingrese la fecha de carga");
                ucFecha.Focus();
                return;
            }
            if (fupArchivoPersona.HasFile)
            {
                Stream stream = fupArchivoPersona.FileContent;
                List<Xpinn.Aportes.Entities.ErroresCargaAportes> plstErrores = new List<Xpinn.Aportes.Entities.ErroresCargaAportes>();
                List<Xpinn.Aportes.Entities.Aporte> lstAportes = new List<Xpinn.Aportes.Entities.Aporte>();
                
                //LLAMANDO AL METODO DE CAPTURA DE DATOS
                AporteServicio.CargaAportes(ref error, ddlFormatoFecha.SelectedValue, stream, ref lstAportes, ref plstErrores, (Usuario)Session["usuario"]);

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
            BOexcepcion.Throw(AporteServicio.ProgramaAperturaAporte, "btnCargarAportes_Click", ex);
        }
    }


    protected void gvDatos_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            List<Xpinn.Aportes.Entities.Aporte> lstAportes = new List<Xpinn.Aportes.Entities.Aporte>();
            lstAportes = (List<Xpinn.Aportes.Entities.Aporte>)Session["lstData"];

            lstAportes.RemoveAt((gvDatos.PageIndex * gvDatos.PageSize) + e.RowIndex);

            gvDatos.DataSourceID = null;
            gvDatos.DataBind();

            gvDatos.DataSource = lstAportes;
            gvDatos.DataBind();
            Session["lstData"] = lstAportes;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AporteServicio.ProgramaAperturaAporte, "gvDatos_RowDeleting", ex);
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
            BOexcepcion.Throw(AporteServicio.ProgramaAperturaAporte, "btnGuardar_Click", ex);
        }
    }

    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        try
        {
            VerError("");
            List<Xpinn.Aportes.Entities.Aporte> lstAportes = new List<Xpinn.Aportes.Entities.Aporte>();
            lstAportes = (List<Xpinn.Aportes.Entities.Aporte>)Session["lstData"];

            string pError = "";
            AporteServicio.CrearAporteImportacion(ucFecha.ToDateTime, ref pError, lstAportes, (Usuario)Session["usuario"]);
            if (pError != "")
            {
                VerError(pError);
                return;
            }
            mvPrincipal.ActiveViewIndex = 2;
            Site toolBar = (Site)this.Master;
            toolBar.MostrarGuardar(false);
            toolBar.MostrarConsultar(false);
            toolBar.MostrarLimpiar(false);
            Session.Remove("lstData");
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AporteServicio.ProgramaAperturaAporte, "btnContinuarMen_Click", ex);
        }
    }

    #endregion


}