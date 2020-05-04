using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Nomina.Entities;
using Xpinn.Nomina.Services;
using Xpinn.Util;


public partial class Page_Nomina_Cargos_Lista : GlobalWeb
{
    CargosService CargoServise = new CargosService();
    public void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(CargoServise.CodigoPrograma, "L");
            Site toolBar = (Site)Master;
            toolBar.eventoLimpiar += btnLimpiar_Click;

            //toolBar.eventoLimpiar += btnLimpiar_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
           ctlMensajeBorrar.eventoClick += CtlMensajeBorrar_eventoClick;
            toolBar.eventoNuevo += (s, evt) => {
                Session.Remove(CargoServise.CodigoPrograma + ".id");
                Navegar(Pagina.Nuevo);
            };
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CargoServise.CodigoPrograma, "Page_PreInit", ex);
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        Actualizar();
    }
    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        long id = Convert.ToInt64(gvContent.SelectedRow.Cells[1].Text);

        Session[CargoServise.CodigoPrograma + ".id"] = id;
        Navegar(Pagina.Nuevo);
    }
    void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        txtNombre.Text = string.Empty;
        txtCodigo.Text = string.Empty;
    }
    protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        long idBorrar = Convert.ToInt64(e.Keys[0]);

        ViewState.Add("idBorrar", idBorrar);

        ctlMensajeBorrar.MostrarMensaje("Seguro que deseas eliminar esta registro?");
    }
    protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvContent.PageIndex = e.NewPageIndex;
        Actualizar();
    }


    protected void gvLista_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        Label lblentidad = (Label)e.Row.FindControl("lblBiometria");
        if (lblentidad != null)
        {
            Int32 indicador = Convert.ToInt32(lblentidad.Text);
            if (indicador > 0)
            {
                Image imgentidad = (Image)e.Row.FindControl("imgentidad");
                if (imgentidad != null)
                {
                    imgentidad.Visible = true;
                }
            }
        }
    }

    void CtlMensajeBorrar_eventoClick(object sender, EventArgs e)
    {
        if (ViewState["idBorrar"] != null)
        {
           
            Cargos Entitie = new Cargos();
            Entitie.IdCargo = Convert.ToInt64(ViewState["idBorrar"]);
          
            try
            {
                CargoServise.EliminarCargo(Entitie, Usuario);
                Actualizar();
            }
            catch (Exception ex)
            {
                VerError("Error al borrar el registro, " + ex.Message);
            }
        }
    }

    string ObtenerFiltro()
    {
        string filtro = string.Empty;

        if (!string.IsNullOrWhiteSpace(txtCodigo.Text))
        {
            filtro += " and idcargo = " + txtCodigo.Text;
        }

        if (!string.IsNullOrWhiteSpace(txtNombre.Text))
        {
            filtro += " and  descripcion like '%" + txtNombre.Text + "%'".ToUpper();
        }

     

        return filtro;
    }

    void Actualizar()
    {
        try
        {
            VerError("");
            string filtro = ObtenerFiltro();



            List<Cargos> lstEmpleado = CargoServise.ListarCargos(filtro,Usuario);

            if (lstEmpleado.Count > 0)
            {
                lblTotalRegs.Text = "Se encontraron " + lstEmpleado.Count + " registros!.";
            }
            else
            {
                lblTotalRegs.Text = "Su consulta no obtuvo ningún resultado!.";
            }

            gvContent.DataSource = lstEmpleado;
            gvContent.DataBind();
        }
        catch (Exception ex)
        {
            VerError("Error al actualizar la grilla, " + ex.Message);
        }
    }
    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)

    {
        string id = gvContent.DataKeys[e.NewEditIndex].Values[0].ToString();

        Session[CargoServise.CodigoPrograma + ".id"] = id;
        Navegar(Pagina.Editar);
    }


}