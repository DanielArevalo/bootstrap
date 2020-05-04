using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using Xpinn.Contabilidad.Entities;
using System.Globalization;

partial class Lista : GlobalWeb
{
    private Xpinn.Contabilidad.Services.CierreTercerosService CierreTercerosSer = new Xpinn.Contabilidad.Services.CierreTercerosService();
    static ArrayList ar = new ArrayList();
    CierreTerceros datosApp = new CierreTerceros();
    private static string pCod_Programa;
    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            if (Request.UrlReferrer.ToString().Contains("niif"))
            {
                pCod_Programa = CierreTercerosSer.CodigoProgramaNiif;
                ViewState.Add("COD_PROGRAMA", "NIIF");
            }
            else
            {
                pCod_Programa = CierreTercerosSer.CodigoPrograma;
                ViewState.Add("COD_PROGRAMA", "LOCAL");
            }
            VisualizarOpciones(pCod_Programa, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(pCod_Programa, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                LlenarCombos();
                Actualizar();
                CargarValoresConsulta(pConsulta, pCod_Programa);
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(pCod_Programa, "Page_Load", ex);
        }
    }

    /// <summary>
    /// Método para llenar los DDLs requeridos para las consultas
    /// </summary>
    protected void LlenarCombos()
    {
        List<LibroMayor> lstFechaCierre = new List<LibroMayor>();
        Xpinn.Contabilidad.Services.LibroMayorService LibroMayorService = new Xpinn.Contabilidad.Services.LibroMayorService();
        LibroMayor LibroMayor = new LibroMayor();
        bool rpta = ViewState["COD_PROGRAMA"] != null && ViewState["COD_PROGRAMA"].ToString() == "NIIF" ? true : false;
        lstFechaCierre = LibroMayorService.ListarFechaCorte(Usuario, rpta);
        ddlFechaCorte.DataSource = lstFechaCierre;
        ddlFechaCorte.DataTextFormatString = "{0:dd/MM/yyyy}";
        ddlFechaCorte.DataTextField = "fecha";
        ddlFechaCorte.DataBind();
    }


    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        GuardarValoresConsulta(pConsulta, pCod_Programa);
        Guardar(idObjeto);
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        LimpiarValoresConsulta(pConsulta, pCod_Programa);
        LlenarCombos();
        Actualizar();
        Site toolBar = (Site)this.Master;
        toolBar.MostrarGuardar(true);
    }

    protected void gvLista_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            //ConfirmarEliminarFila(e, "btnBorrar");
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(pCod_Programa + "L", "gvLista_RowDataBound", ex);
        }
    }

    private void Actualizar()
    {
        try
        {
            // Generar consulta de centros de costo
            // LLenar el Gridview con centros de costo
            Xpinn.Contabilidad.Services.CentroCostoService CentroCostoService = new Xpinn.Contabilidad.Services.CentroCostoService();
            List<CentroCosto> LstCentroCosto = new List<CentroCosto>();
            string sFiltro = "";
            LstCentroCosto.Clear();
            LstCentroCosto = CentroCostoService.ListarCentroCosto((Usuario)Session["usuario"], sFiltro);
            gvLista.EmptyDataText = emptyQuery;            
            gvLista.DataSource = LstCentroCosto;
            if (LstCentroCosto.Count > 0)
            {
                mvCierreTerceros.ActiveViewIndex = 0;
                gvLista.DataBind();
            }
            else
            {
                mvCierreTerceros.ActiveViewIndex = -1;
            }
            Session.Add(pCod_Programa + ".consulta", 1);

        }

        catch (Exception ex)
        {
            BOexcepcion.Throw(pCod_Programa, "Actualizar", ex);
        }
    }

    private void Guardar(String pIdObjeto)
    {
        try
        {
            CierreTerceros datosApp = new CierreTerceros();
            string error = "";

            Configuracion conf = new Configuracion();
            String format = conf.ObtenerFormatoFecha();
            datosApp.fecha = DateTime.ParseExact(ddlFechaCorte.SelectedValue, format, CultureInfo.InvariantCulture);
            bool isNiif = ViewState["COD_PROGRAMA"] != null && ViewState["COD_PROGRAMA"].ToString() == "NIIF" ? true : false;
            try
            {

                foreach (GridViewRow wrow in gvLista.Rows)
                {
                    Label lblcodigo = (Label)wrow.FindControl("lblcodigo");
                    Label lblmensaje = (Label)wrow.FindControl("lblmensaje");
                    datosApp.centro_costo = Convert.ToInt64(lblcodigo.Text);
                    CierreTerceros cierreTerceros = new CierreTerceros();
                    cierreTerceros = CierreTercerosSer.CrearCierreTerceros(datosApp, ref error, isNiif, Usuario);
                    lblmensaje.Text = "Cierre Terceros Terminado Correctamente";
                   

                   
                }






            }
            catch (Exception ex)
            {
                int n = 1;
                if (ex.Message.Contains("ORA-20101:"))
                    n = ex.Message.IndexOf("ORA-20101:") + 10;
                if (n > 0)
                {
                    VerError("No se pudo realizar el cierre." + ex.Message.Substring(n, ex.Message.Length - n));
                    return;
                }
                else
                {
                    VerError(ex.Message);
                }
            }
            VerError(error);

            foreach (GridViewRow wrow in gvLista.Rows)
            {
                Label lblcodigo = (Label)wrow.FindControl("lblcodigo");
                Label lblmensaje = (Label)wrow.FindControl("lblmensaje");
                datosApp.centro_costo = Convert.ToInt64(lblcodigo.Text);
                lblmensaje.Text = "Cierre Terceros Terminado Correctamente";                  
            }
            Site toolBar = (Site)this.Master;
            toolBar.MostrarGuardar(false);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(pCod_Programa, "Actualizar", ex);
        }
    }

    private Xpinn.Util.Usuario ObtenerValores()
    {
        Xpinn.Util.Usuario vUsuario = new Xpinn.Util.Usuario();
        return vUsuario;
    }

}