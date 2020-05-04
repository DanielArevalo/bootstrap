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

    private Xpinn.FabricaCreditos.Services.CreditoService CargaCreditosServicio = new Xpinn.FabricaCreditos.Services.CreditoService();


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
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw("CargaCreditosServicio", "Page_PreInit", ex);
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
            BOexcepcion.Throw("CargaCreditosServicio", "Page_Load", ex);
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

    protected void btnCargarCreditos_Click(object sender, EventArgs e)
    {
       
        VerError("");
        string error = "";

        if (fupArchivoPersona.HasFile)
        {
            Stream stream = fupArchivoPersona.FileContent;

            List<Xpinn.FabricaCreditos.Entities.ErroresCarga> plstErrores = new List<Xpinn.FabricaCreditos.Entities.ErroresCarga>();
            List<Xpinn.FabricaCreditos.Entities.Credito> lstCreditos = new List<Xpinn.FabricaCreditos.Entities.Credito>();

            try
            {
                CargaCreditosServicio.CargarCreditos(ref error, stream, ref lstCreditos, ref plstErrores, (Usuario)Session["usuario"]);
            }
            catch (Exception ex)
            {
                BOexcepcion.Throw("CargaCreditosServicio" + "L", "btnCargarCreditos_Click", ex);
                return;
            }

            if (error.Trim() != "")
            {
                VerError(error);
                return;
            }

            try
            {
                if (plstErrores.Count() > 0)
                {
                    pErrores.Visible = true;
                    gvErrores.DataSource = plstErrores;
                    gvErrores.DataBind();
                    cpeDemo.CollapsedText = "(Click Aqui para ver " + plstErrores.Count() + " errores...)";
                    cpeDemo.ExpandedText = "(Click Aqui para ocultar listado de errores...)";
                    lblMostrarDetalles.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    lblMostrarDetalles.ForeColor = System.Drawing.Color.FromArgb(0, 101, 255);
                }
                panelData.Visible = false;
                if (lstCreditos.Count > 0)
                {
                    Session["lstData"] = lstCreditos;
                    panelData.Visible = true;
                    //CARGAR DATOS A GRILLA DE NATURALES
                    gvDatos.DataSource = lstCreditos;
                    gvDatos.DataBind();
                    Site toolBar = (Site)this.Master;
                    toolBar.MostrarGuardar(true);
                    toolBar.MostrarLimpiar(true);
                }
            }
            catch (Exception ex)
            {
                BOexcepcion.Throw("CargaCreditosServicio" + "L", "btnCargarCreditos_Click", ex);
            }

        }
        else
        {
            VerError("Seleccione el archivo a cargar, verifique los datos.");
            return;
        }
        
    }

    protected void gvDatos_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            List<Xpinn.FabricaCreditos.Entities.Credito> lstCreditos = new List<Xpinn.FabricaCreditos.Entities.Credito>();
            lstCreditos = (List<Xpinn.FabricaCreditos.Entities.Credito>)Session["lstData"];           

            lstCreditos.RemoveAt((gvDatos.PageIndex * gvDatos.PageSize) + e.RowIndex);

            gvDatos.DataSourceID = null;
            gvDatos.DataBind();

            gvDatos.DataSource = lstCreditos;
            gvDatos.DataBind();
            Session["lstData"] = lstCreditos;
            Site toolBar = (Site)this.Master;
            toolBar.MostrarGuardar(true);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw("CargaCreditosServicio", "gvDatos_RowDeleting", ex);
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
            BOexcepcion.Throw("CargaCreditosServicio", "btnGuardar_Click", ex);
        }
    }

    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        try
        {
            VerError("");        

            List<Xpinn.FabricaCreditos.Entities.Credito> lstCreditos = new List<Xpinn.FabricaCreditos.Entities.Credito>();
            lstCreditos = (List<Xpinn.FabricaCreditos.Entities.Credito>)Session["lstData"];
            List<Xpinn.FabricaCreditos.Entities.Credito> lst_Num_cred = new List<Xpinn.FabricaCreditos.Entities.Credito>();

            string pError = "";

            CargaCreditosServicio.CrearImportacionCred(lstCreditos, ref pError, ref lst_Num_cred, Usuario);
            
            if (pError != "")
            {
                VerError(pError);
                return;
            }
            
            if (lst_Num_cred.Count > 0)
            {
                 //CARGAR DATOS A GRILLA DE CREDITOS NO IMPORTADOS 
                    GridView1.DataSource = lst_Num_cred;
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
            BOexcepcion.Throw("CargaCreditosServicio", "btnContinuarMen_Click", ex);
        }
    }

    #endregion


}