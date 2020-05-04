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
            if (Session[perfilServicio.CodigoProgramaGenerales + ".id"] != null)
                VisualizarOpciones(perfilServicio.CodigoProgramaGenerales, "E");
            else
                VisualizarOpciones(perfilServicio.CodigoProgramaGenerales, "A");
            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGrabar_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
            
            mvPrincipal.ActiveViewIndex = 0;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(perfilServicio.CodigoProgramaGenerales, "Page_PreInit", ex);
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

    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        Actualizar();
    }

    private void Actualizar()
    {

        try
        {
            Site toolBar = (Site)this.Master;
            toolBar.MostrarGuardar(true);
            mvPrincipal.ActiveViewIndex = 0;
            String filtro = obtFiltro();
            List<Xpinn.Seguridad.Entities.Opcion> lstAccesos = new List<Xpinn.Seguridad.Entities.Opcion>();
            lstAccesos = perfilServicio.ListarOpcionesGeneral(filtro, (Usuario)Session["Usuario"]);
            gvLista.DataSource = lstAccesos;
            gvLista.DataBind();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(perfilServicio.CodigoProgramaGenerales, "ObtenerDatos", ex);
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
            List<Xpinn.Seguridad.Entities.Opcion> Lista = new List<Xpinn.Seguridad.Entities.Opcion>();

            // Actualizar los parámetros generales modificados
            foreach (GridViewRow rFila in gvLista.Rows)
            {
                if (rFila.Cells[0].Text != "")
                    vPerfil.cod_opcion = Convert.ToInt64(rFila.Cells[0].Text);

                TextBoxGrid txtvalor = (TextBoxGrid)rFila.FindControl("txtvalor");
                if (txtvalor.Text != null)
                    vPerfil.valor = Convert.ToString(txtvalor.Text);
                Lista.Add(vPerfil);
                Session["DatosDetalle"] = Lista;
                if (rFila.BackColor == System.Drawing.Color.LightGreen)
                    perfilServicio.Modificargeneral(vPerfil, (Usuario)Session["Usuario"]);
            }

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
            BOexcepcion.Throw(perfilServicio.CodigoProgramaGenerales, "btnGuardar_Click", ex);
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


    protected void txtvalor_TextChanged(object sender, EventArgs e)
    {
        VerError("");
        TextBoxGrid txtvalor = (TextBoxGrid)sender;
        Xpinn.Seguridad.Entities.Opcion vPerfil = new Xpinn.Seguridad.Entities.Opcion();
        List<Xpinn.Seguridad.Entities.Opcion> Lista = new List<Xpinn.Seguridad.Entities.Opcion>();
        int rowIndex = Convert.ToInt32(txtvalor.CommandArgument);
        if (rowIndex >= 0)
        {
            gvLista.Rows[rowIndex].BackColor = System.Drawing.Color.LightGreen;
        }
    }


    protected void ObtenerDatos()
    {
      
    }


    private string obtFiltro()
    {
        String filtro = String.Empty;

        if (txtCod_opcion.Text.Trim() != "")
            filtro += " and CODIGO=" + txtCod_opcion.Text + "";

        if ((txtdescripcion.Text.Trim() != "") && ((txtdescripcion.Text.Trim() != "0")))

            filtro += " and DESCRIPCION like '%" + txtdescripcion.Text + "%'";

        return filtro;
    }

    protected void btnContinuar_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }

}