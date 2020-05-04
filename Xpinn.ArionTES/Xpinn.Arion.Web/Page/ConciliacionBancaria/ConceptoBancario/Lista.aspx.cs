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
    private Xpinn.ConciliacionBancaria.Services.ConciliacionBancariaService perfilServicio = new Xpinn.ConciliacionBancaria.Services.ConciliacionBancariaService();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[perfilServicio.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(perfilServicio.CodigoPrograma, "E");
            else
                VisualizarOpciones(perfilServicio.CodigoPrograma, "A");
            Site toolBar = (Site)this.Master;
            toolBar.eventoLimpiar += btnGrabar_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoNuevo += btnNuevo_Click;

            lblTotalReg.Visible = false;
            mvPrincipal.ActiveViewIndex = 0;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(perfilServicio.CodigoPrograma, "Page_PreInit", ex);
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

    protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    {
        Navegar(Pagina.Nuevo);
    }


    private void Actualizar()
    {

        try
        {

            String filtro = obtFiltro();
            Xpinn.ConciliacionBancaria.Entities.ConceptoBancario conceptobancario = new Xpinn.ConciliacionBancaria.Entities.ConceptoBancario();
            List<Xpinn.ConciliacionBancaria.Entities.ConceptoBancario> lstAccesos = new List<Xpinn.ConciliacionBancaria.Entities.ConceptoBancario>();
            lstAccesos = perfilServicio.Listarconceptobancario(filtro,conceptobancario, (Usuario)Session["Usuario"]);
            if (lstAccesos.Count > 0)
            {
                gvLista.DataSource = lstAccesos;
                gvLista.DataBind();
                lblTotalReg.Visible = false;
            }
            else 
            {
                gvLista.Visible = false;
                lblTotalReg.Text = "Registros no encontrados";
                lblTotalReg.Visible = true;
            }
        }
        catch (Exception ex)
        {
            gvLista.Visible = false;
            BOexcepcion.Throw(perfilServicio.CodigoPrograma, "ObtenerDatos", ex);
        }
    }


    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvLista.Rows[e.NewEditIndex].Cells[1].Text;
        Session[perfilServicio.CodigoPrograma + ".id"] = id;
        Navegar(Pagina.Nuevo);
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
            txtdescripcion.Text = "";
            txtcodigo.Text = "";
            txtCod_opcion.Text = "";
            lblTotalReg.Visible = false;
            gvLista.Visible = false;
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(perfilServicio.CodigoPrograma, "btnGuardar_Click", ex);
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
        if (rowIndex > 0)
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
            filtro += " and cod_concepto=" + txtCod_opcion.Text + "";

        if ((txtdescripcion.Text.Trim() != "") && ((txtdescripcion.Text.Trim() != "0")))

            filtro += " and DESCRIPCION like '%" + txtdescripcion.Text + "%'";

        return filtro;
    }

    protected void btnContinuar_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }

}