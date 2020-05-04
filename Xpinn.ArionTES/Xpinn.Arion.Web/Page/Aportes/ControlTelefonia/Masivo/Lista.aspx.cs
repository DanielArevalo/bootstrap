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

    private PlanesTelefonicosService LineaServicio = new PlanesTelefonicosService();


    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            Site toolBar = (Site)this.Master;
            toolBar.eventoLimpiar += btnLimpiar_Click;
            toolBar.eventoImportar += btnImportar_Click;
            toolBar.eventoGuardar += btnGuardar_Click;
            ctlMensaje.eventoClick += btnContinuarMen_Click;
            toolBar.MostrarLimpiar(false);
            toolBar.MostrarGuardar(false);
            toolBar.MostrarImportar(false);
            VisualizarOpciones("170806", "A");
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw("CargaLineasTelefonicas", "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {

            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw("CargaLineasTelefonicas", "Page_Load", ex);
        }
    }


    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        Site toolBar = (Site)this.Master;
        toolBar.MostrarGuardar(false);
        gvDatos.DataSource = null;
        gvDatos.DataBind();
        toolBar.MostrarLimpiar(false);
    }


    #region CODIGO DE IMPORTACION

    void LimpiarDataImportacion()
    {
        gvDatos.DataSource = null;
        Site toolBar = (Site)Master;
        toolBar.MostrarGuardar(false);
    }

    protected void btnImportar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        // mvPrincipal.ActiveViewIndex = 1;
        panelData.Visible = false;
        Site toolBar = (Site)Master;
        toolBar.MostrarNuevo(false);
        toolBar.MostrarConsultar(false);
        toolBar.MostrarLimpiar(true);
        toolBar.MostrarImportar(false);
        LimpiarDataImportacion();
    }

    protected void btnCargarLineas_Click(object sender, EventArgs e)
    {
        try
        {
            VerError("");
            string error = "";

            if (fupArchivoPersona.HasFile)
            {
                Stream stream = fupArchivoPersona.FileContent;

                List<Xpinn.Aportes.Entities.ErroresCargaAportes> plstErrores = new List<Xpinn.Aportes.Entities.ErroresCargaAportes>();
                List<PlanTelefonico> lstLineas = new List<PlanTelefonico>();


                LineaServicio.CargarLineasMasivo(ref error, stream, ref lstLineas, ref plstErrores, (Usuario)Session["usuario"]);


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
                if (lstLineas.Count > 0)
                {
                    Session["lstData"] = lstLineas;
                    panelData.Visible = true;
                    //CARGAR DATOS A GRILLA DE NATURALES
                    gvDatos.DataSource = lstLineas;
                    gvDatos.DataBind();
                    Site toolBar = (Site)this.Master;
                    toolBar.MostrarGuardar(true);
                    toolBar.MostrarLimpiar(true);
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
            BOexcepcion.Throw("PlanesTelefonicosService" + "L", "btnCargarLineas_Click", ex);
        }
    }

    protected void gvDatos_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {

            List<PlanTelefonico> lstLineas = new List<PlanTelefonico>();

            lstLineas = (List<PlanTelefonico>)Session["lstData"];

            lstLineas.RemoveAt((gvDatos.PageIndex * gvDatos.PageSize) + e.RowIndex);

            gvDatos.DataSourceID = null;
            gvDatos.DataBind();

            gvDatos.DataSource = lstLineas;
            gvDatos.DataBind();
            Session["lstData"] = lstLineas;
            Site toolBar = (Site)this.Master;
            toolBar.MostrarGuardar(true);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw("PlanesTelefonicosService", "gvDatos_RowDeleting", ex);
        }
    }

    Boolean ValidarData()
    {
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
            BOexcepcion.Throw("PlanesTelefonicosService", "btnGuardar_Click", ex);
        }
    }

    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        try
        {
            VerError("");

            List<PlanTelefonico> lstLineas = new List<PlanTelefonico>();
            lstLineas = (List<PlanTelefonico>)Session["lstData"];
            List<PlanTelefonico> lst_num_lin = new List<PlanTelefonico>();

            string pError = "";

            Usuario pUsuario = new Usuario();
            pUsuario = (Usuario)Session["usuario"];

            //Agregado para cargar código del proveedor
            Xpinn.Servicios.Services.LineaServiciosServices lineaServicio = new Xpinn.Servicios.Services.LineaServiciosServices();
            Xpinn.Servicios.Entities.LineaServicios linea = new Xpinn.Servicios.Entities.LineaServicios();
            linea = lineaServicio.ConsultarLineaSERVICIO("2", pUsuario);


            //DATOS DE LA OPERACION
            Xpinn.Tesoreria.Entities.Operacion vOpe = new Xpinn.Tesoreria.Entities.Operacion();
            vOpe.cod_ope = 0;
            vOpe.tipo_ope = 110;
            vOpe.cod_caja = 0;
            vOpe.cod_cajero = 0;
            vOpe.observacion = "Operacion-Carga de Servicios para línea telefónica Masivamente";
            vOpe.cod_proceso = null;
            vOpe.fecha_oper = DateTime.Now;
            vOpe.fecha_calc = DateTime.Now;
            vOpe.cod_ofi = pUsuario.cod_oficina;

             LineaServicio.CrearMasivoLineas(lstLineas, ref pError, ref lst_num_lin, vOpe, Usuario);

            //GENERAR EL COMPROBANTE
            if (vOpe.cod_ope != 0)
            {
                Xpinn.Contabilidad.Services.ComprobanteService ComprobanteServicio = new Xpinn.Contabilidad.Services.ComprobanteService();
                Session[ComprobanteServicio.CodigoPrograma + ".cod_ope"] = vOpe.cod_ope;
                Session[ComprobanteServicio.CodigoPrograma + ".tipo_ope"] = 110;
                Session[ComprobanteServicio.CodigoPrograma + ".fecha_ope"] = vOpe.fecha_oper;
                Session[ComprobanteServicio.CodigoPrograma + ".cod_persona"] = linea.cod_proveedor;
                Navegar("../../../Contabilidad/Comprobante/Nuevo.aspx");
            }

            if (pError != "")
            {
                VerError(pError);
                return;
            }

            if (lst_num_lin.Count > 0)
            {
                //CARGAR DATOS A GRILLA DE LINEAS TELEFONICAS NO IMPORTADAS
                GridView1.DataSource = lst_num_lin;
                GridView1.DataBind();
                infApor_no.Visible = true;
            }
            else
            {
                infApor_no.Visible = false;
            }

            mvPrincipal.ActiveViewIndex = 1;
            Site toolBar = (Site)this.Master;
            toolBar.MostrarGuardar(false);
            toolBar.MostrarConsultar(false);
            toolBar.MostrarLimpiar(false);
            Session.Remove("lstData");
            Session.Remove("lstData2");
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw("PlanesTelefonicosService", "btnContinuarMen_Click", ex);
        }
    }

    #endregion


}