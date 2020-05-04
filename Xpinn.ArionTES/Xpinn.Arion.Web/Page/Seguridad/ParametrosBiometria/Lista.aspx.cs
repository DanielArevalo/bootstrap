using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Seguridad.Services;
using Xpinn.Seguridad.Entities;
using Xpinn.Util;

partial class Nuevo : GlobalWeb
{
    private Xpinn.Seguridad.Services.OpcionService perfilServicio = new Xpinn.Seguridad.Services.OpcionService();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[perfilServicio.CodigoProgramaBio + ".id"] != null)
                VisualizarOpciones(perfilServicio.CodigoProgramaBio, "E");
            else
                VisualizarOpciones(perfilServicio.CodigoProgramaBio, "A");
            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGrabar_Click;
            
            mvPrincipal.ActiveViewIndex = 0;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(perfilServicio.CodigoProgramaBio, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {                
                ObtenerDatos();   
             }
       
       
      }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
    }
    


    protected void CargarOpciones(string sidPerfil, string sidModulo)
    {
        Int64 idPerfil = 0;
        if (sidPerfil.Trim() != "")
            idPerfil = Convert.ToInt64(sidPerfil);
        List<Xpinn.Seguridad.Entities.Opcion> lstAccesos = new List<Xpinn.Seguridad.Entities.Opcion>();
        lstAccesos = perfilServicio.ListarOpciones((Usuario)Session["Usuario"]);
        gvLista.DataSource = lstAccesos;
        gvLista.DataBind();
    }

    /// <summary>
    /// Crear los datos del perfil
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnGrabar_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Usuario pUsuario = new Usuario();
            pUsuario = (Usuario)Session["Usuario"];
            Xpinn.Seguridad.Entities.Opcion vPerfil = new Xpinn.Seguridad.Entities.Opcion();
            
                       
            foreach (GridViewRow rFila in gvLista.Rows)
            {
                Opcion pAcceso = new Opcion();                    

                // Traer datos de la opción
                if (rFila.Cells[0].Text != null) 
                    pAcceso.cod_opcion = Convert.ToInt64(rFila.Cells[0].Text);
                pAcceso = perfilServicio.ConsultarOpcion(pAcceso.cod_opcion, pUsuario);
                if (pAcceso != null)
                {
                    pAcceso.validar_Biometria = 0;
                    CheckBox chbconsulta = (CheckBox)rFila.Cells[2].FindControl("chbconsulta");
                    if (chbconsulta != null)
                        if (chbconsulta.Checked == true)
                            pAcceso.validar_Biometria = 1;
                    pAcceso.maneja_excepciones = 0;
                    CheckBox chbcreacion = (CheckBox)rFila.Cells[3].FindControl("chbcreacion");
                    if (chbcreacion != null)
                        if (chbcreacion.Checked == true)
                            pAcceso.maneja_excepciones = 1;
                    if (idObjeto == "")
                    {
                        perfilServicio.ModificarOpcion(pAcceso, (Usuario)Session["usuario"]);
                    }
                }
           }

            Session[perfilServicio.CodigoProgramaBio + ".id"] = idObjeto;
            mvPrincipal.ActiveViewIndex = 1;
            Site toolBar = (Site)this.Master;
            toolBar.MostrarGuardar(false);
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(perfilServicio.CodigoProgramaBio, "btnGuardar_Click", ex);
        }
    }

    protected void gvLista_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //DropDownList ddlCiudadOtr = (DropDownList)e.Row.Cells[1].FindControl("ddlCiudadOtr");
            //if (ddlCiudadOtr != null)
            //{
            //    ddlCiudadOtr.Visible = true;
            //}
        }
    }

    protected void ObtenerDatos()
    {
        try
        {

            List<Xpinn.Seguridad.Entities.Opcion> lstAccesos = new List<Xpinn.Seguridad.Entities.Opcion>();
            lstAccesos = perfilServicio.ListarOpciones((Usuario)Session["Usuario"]);
            gvLista.DataSource = lstAccesos;
            gvLista.DataBind();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(perfilServicio.CodigoProgramaBio, "ObtenerDatos", ex);
        }
    }


    protected void btnContinuar_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }

    
}