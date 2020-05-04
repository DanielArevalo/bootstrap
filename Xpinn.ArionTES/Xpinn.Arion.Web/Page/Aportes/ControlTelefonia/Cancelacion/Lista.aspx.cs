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
using Xpinn.Servicios.Entities;
using Xpinn.Servicios.Services;
using Xpinn.Aportes.Entities;
using Xpinn.Aportes.Services;
using System.IO;

partial class Lista : GlobalWeb
{
    SolicitudServiciosServices SoliServicios = new SolicitudServiciosServices();
    Usuario _usuario;
    PlanesTelefonicosService LineaTeleServicio = new PlanesTelefonicosService();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones("170805", "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoNuevo += btnNuevo_Click;
            toolBar.eventoGuardar += btnGuardar_Click;
            ctlMensaje.eventoClick += btnContinuarMen_Click;
            toolBar.MostrarNuevo(false);
            toolBar.MostrarImportar(false);
            toolBar.eventoLimpiar += btnLimpiar_Click;
            toolBar.eventoImportar += btnImportar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(SoliServicios.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            _usuario = (Usuario)Session["usuario"];
            
            Site toolBar = (Site)this.Master;
            toolBar.MostrarGuardar(false);
            toolBar.MostrarLimpiar(false);

            if (!IsPostBack)
            {
                cargarDropdown();
                Actualizar();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(SoliServicios.CodigoPrograma, "Page_Load", ex);
        }
    }


    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {

        Page.Validate();
        gvLista.Visible = true;
        if (Page.IsValid)
        {
            Actualizar();
        }
    }


    void cargarDropdown()
    {
         PoblarLista("PLANES_TELEFONICOS", ddlPlan);
    }

    protected void btnImportar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        mvPrincipal.ActiveViewIndex = 1;
        panelData.Visible = false;
        Site toolBar = (Site)Master;
        toolBar.MostrarNuevo(false);
        toolBar.MostrarConsultar(false);
        toolBar.MostrarLimpiar(true);
        toolBar.MostrarImportar(false);
        //LimpiarDataImportacion();
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        //LimpiarValoresConsulta(pConsulta, LineaAporteServicio.CodigoProgramaLineas);
        //txtCodLinea.Text = "";
        //txtNombreLinea.Text = "";
        //gvLista.DataBind();

    }


    protected void btnCargarAdicionales_Click(object sender, EventArgs e)
    {
        try
        {
            VerError("");
            string error = "";

            if (fupArchivoPersona.HasFile)
            {
                Stream stream = fupArchivoPersona.FileContent;
                List<Xpinn.Aportes.Entities.ErroresCargaAportes> plstErrores = new List<Xpinn.Aportes.Entities.ErroresCargaAportes>();
                List<PlanTelefonico> lstAdicionales = new List<PlanTelefonico>();

                //LLAMANDO AL METODO DE CAPTURA DE DATOS
                LineaTeleServicio.CargaAdicionales(ref error, stream, ref lstAdicionales, ref plstErrores, (Usuario)Session["usuario"]);


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
                if (lstAdicionales.Count > 0)
                {
                    Session["lstData"] = lstAdicionales;
                    panelData.Visible = true;
                    //CARGAR DATOS A GRILLA DE NATURALES
                    gvDatos.DataSource = lstAdicionales;
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
            // BOexcepcion.Throw(LineaAporteServicio.CodigoProgramaLineas + "L", "btnCargarAportes_Click", ex);
        }
    }

    protected void gvDatos_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            List<PlanTelefonico> lstAdicionales = new List<PlanTelefonico>();
            lstAdicionales = (List<PlanTelefonico>)Session["lstData"];

            lstAdicionales.RemoveAt((gvDatos.PageIndex * gvDatos.PageSize) + e.RowIndex);

            gvDatos.DataSourceID = null;
            gvDatos.DataBind();

            gvDatos.DataSource = lstAdicionales;
            gvDatos.DataBind();
            Session["lstData"] = lstAdicionales;
            Site toolBar = (Site)this.Master;
            toolBar.MostrarGuardar(true);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(SoliServicios.CodigoPrograma, "gvDatos_RowDeleting", ex);

        }
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
            BOexcepcion.Throw(SoliServicios.CodigoPrograma, "btnGuardar_Click", ex);
        }
    }

    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        try
        {
            VerError("");
            List<PlanTelefonico> lstAdicionales = new List<PlanTelefonico>();
            lstAdicionales = (List<PlanTelefonico>)Session["lstData"];
            List<Int64> lst_Num_Lin = new List<Int64>();


            string pError = "";
            Xpinn.Tesoreria.Entities.Operacion vOperacion = new Xpinn.Tesoreria.Entities.Operacion();
            LineaTeleServicio.CrearImportacion(ref pError, lstAdicionales, ref vOperacion, (Usuario)Session["usuario"], ref lst_Num_Lin);
            //coun de la lista

            if (pError != "")
            {
                VerError(pError);
                return;
            }
            
            if (lst_Num_Lin.Count > 0)
            {
                foreach (int element in lst_Num_Lin)
                {
                    //CARGAR DATOS A GRILLA DE APORTES NO IMPORTADOS 
                    GridView1.DataSource = lst_Num_Lin;
                    GridView1.DataBind();
                    GridView1.HeaderRow.Cells[0].Text = "Número Linea Telefónica";
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
            BOexcepcion.Throw(SoliServicios.CodigoPrograma, "btnContinuarMen_Click", ex);
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
        plista = pservicio.ListarListaDesplegable(pentidad, pTabla, pColumnas, pCondicion, pOrden, _usuario);
        pentidad.idconsecutivo = null;
        pentidad.descripcion = "Seleccione un item";
        plista.Insert(0, pentidad);
        ddlControl.DataTextField = "descripcion";
        ddlControl.DataValueField = "idconsecutivo";
        ddlControl.DataSource = plista;
        ddlControl.DataBind();
    }


    protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    {
        Navegar(Pagina.Nuevo);
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
            BOexcepcion.Throw(SoliServicios.CodigoPrograma, "gvLista_PageIndexChanging", ex);
        }
    }


    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvLista.Rows[e.NewEditIndex].Cells[2].Text;
        Session[SoliServicios.CodigoPrograma + ".id"] = id;
        Navegar(Pagina.Nuevo);
    }


    protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

        string Estado = gvLista.DataKeys[e.RowIndex].Values[1].ToString();

        if (Estado == "S")
        {
            int id = Convert.ToInt32(gvLista.DataKeys[e.RowIndex].Values[0].ToString());
            Session["ID"] = id;
            ctlMensaje.MostrarMensaje("Desea realizar la eliminación del Servicio?");
        }
    }

    //protected void btnContinuarMen_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        SoliServicios.EliminarServicio(Convert.ToInt32(Session["ID"]), _usuario);
    //        Actualizar();
    //    }
    //    catch (Exception ex)
    //    {
    //        BOexcepcion.Throw(SoliServicios.CodigoPrograma, "btnContinuarMen_Click", ex);
    //    }
    //}


    private void Actualizar()
    {
        try
        {
            PlanTelefonico lineafiltro = new PlanTelefonico();

            if (txtNumLinea.Text != "")
                lineafiltro.num_linea_telefonica = txtNumLinea.Text;
            if (txtIdentificacion.Text != "")
                lineafiltro.identificacion_titular = txtIdentificacion.Text;
            if (ddlPlan.SelectedValue != "")
                lineafiltro.cod_plan = Convert.ToInt32(ddlPlan.SelectedValue);

            List<PlanTelefonico> lstConsulta = new List<PlanTelefonico>();
            lstConsulta = LineaTeleServicio.ListarLineasTelefonicas(lineafiltro, Usuario);

            gvLista.PageSize = 20;
            String emptyQuery = "Fila de datos vacia";
            gvLista.EmptyDataText = emptyQuery;
            Session["ListServicio"] = lstConsulta;
            if (lstConsulta.Count > 0)
            {
                gvLista.DataSource = lstConsulta;
                gvLista.DataBind();
                panelGrilla.Visible = true;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();

            }
            else
            {
                panelGrilla.Visible = false;
                lblTotalRegs.Visible = false;
            }

            Session.Add(SoliServicios.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(SoliServicios.CodigoPrograma, "Actualizar", ex);
        }
    }





}