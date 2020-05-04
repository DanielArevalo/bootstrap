using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Nomina.Entities;
using Xpinn.Nomina.Services;
using Xpinn.Util;

public partial class Lista : GlobalWeb
{
    DotacionService dotaservice = new DotacionService();
    Detalle_DotacionService detalleservice = new Detalle_DotacionService();
   
    #region iniciales
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InicializarPagina();
        }
    }

    void InicializarPagina()
    {

    }

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(dotaservice.CodigoPrograma, "L");

            Site toolBar = (Site)Master;
            toolBar.eventoLimpiar += btnLimpiar_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
            ctlMensajeBorrar.eventoClick += CtlMensajeBorrar_eventoClick;
            toolBar.eventoNuevo += (s, evt) =>
            {
                Session.Remove(dotaservice.CodigoPrograma + ".id");
                Navegar(Pagina.Nuevo);

            };
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(dotaservice.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    #endregion

    #region eventos intermedios

    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        long id = Convert.ToInt64(gvContent.DataKeys[gvContent.SelectedRow.RowIndex].Values[0].ToString());

        Session[dotaservice.CodigoPrograma + ".id"] = id;
        Navegar(Pagina.Nuevo);
    }

    protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        long idBorrar = Convert.ToInt64(e.Keys[0]);

        ViewState.Add("idBorrar", idBorrar);

        ctlMensajeBorrar.MostrarMensaje("Seguro que deseas eliminar esta registro?");
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

    void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        txtcodempleado.Text = string.Empty;
        txtconsecutivo.Text = string.Empty;
        txtubicacion.Text = string.Empty;
    }

    void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        Actualizar();
    }

    void CtlMensajeBorrar_eventoClick(object sender, EventArgs e)
    {
        if (ViewState["idBorrar"] != null)
        {
            long idBorrar = Convert.ToInt64(ViewState["idBorrar"]);

            try
            {
                detalleservice.EliminarDetalle_Dotacion(idBorrar, Usuario);

                dotaservice.EliminarDotacion(idBorrar, Usuario);

                Actualizar();
            }
            catch (Exception ex)
            {
                VerError("Error al borrar el registro, " + ex.Message);
            }
        }
    }

    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        string id = gvContent.DataKeys[e.NewEditIndex].Values[0].ToString();

        Session[dotaservice.CodigoPrograma + ".id"] = id;
        Navegar(Pagina.Nuevo);
    }

    #endregion

    string ObtenerFiltro()
    {
        string filtro = string.Empty;

        if (!string.IsNullOrWhiteSpace(txtconsecutivo.Text))
        {
            filtro += " and dot.id_dotacion  = " + txtconsecutivo.Text.Trim();
        }

        if (!string.IsNullOrWhiteSpace(txtcodempleado.Text))
        {
            filtro += " and dot.cod_empleado  = " + txtcodempleado.Text.Trim();
        }

        if (!string.IsNullOrWhiteSpace(txtubicacion.Text))
        {
            filtro += " and dot.ubicacion  like '%" + txtubicacion.Text.Trim() + "%'";
        }

        if (ctlCentroCosto.Text != "Seleccion un Item")
        {
            filtro += " and dot.centro_costo  like '%" + ctlCentroCosto.Text.Trim() + "%'";
        }


        if (!string.IsNullOrWhiteSpace(filtro))
        {
            StringHelper stringHelper = new StringHelper();
            filtro = stringHelper.QuitarAndYColocarWhereEnFiltroQuery(filtro);
        }

        return filtro;
    }

    void Actualizar()
    {
        try
        {
            VerError("");
            string filtro = ObtenerFiltro();

            List<Dotacion> lstEmpleado = dotaservice.ListarDotacion(filtro,Usuario);
                
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

}