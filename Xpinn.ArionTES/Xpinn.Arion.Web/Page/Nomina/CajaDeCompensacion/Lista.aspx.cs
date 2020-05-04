using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Nomina.Entities;
using Xpinn.Nomina.Services;
using Xpinn.Util;

public partial class Lista : GlobalWeb
{
    CajaDeCompensacionService service = new CajaDeCompensacionService();

    #region Iniciales

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


    protected void Page_PreInit(object sender, EventArgs e) {
        try
        {
            VisualizarOpciones(service.CodigoPrograma, "L");

            Site toolBar = (Site)Master;
            toolBar.eventoLimpiar += btnLimpiar_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
            ctlMensajeBorrar.eventoClick += CtlMensajeBorrar_eventoClick;
            toolBar.eventoNuevo += (s, evt) => {
                Session.Remove(service.CodigoPrograma + ".id");
                Navegar(Pagina.Nuevo);

            };
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(service.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    #endregion

    #region eventos intermedios

    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        long id = Convert.ToInt64(gvContent.SelectedRow.Cells[1].Text);

        Session[service.CodigoPrograma + ".id"] = id;
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

    void CtlMensajeBorrar_eventoClick(object sender, EventArgs e)
    {
        if (ViewState["idBorrar"] != null)
        {
            long idBorrar = Convert.ToInt64(ViewState["idBorrar"]);

            try
            {
                service.EliminarDIRCAJADECOMPENSACION(idBorrar, Usuario);
                Actualizar();
            }
            catch (Exception ex)
            {
                VerError("Error al borrar el registro, " + ex.Message);
            }
        }
    }

    void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        txtconsecutivo.Text = string.Empty;
        txtIdentificacion.Text = string.Empty;
        txtnombre.Text = string.Empty;
        txtdir.Text = string.Empty;
        txttel.Text = string.Empty;
        txtcodpila.Text = string.Empty;
    }

    void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        Actualizar();
    }

    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        string id = gvContent.DataKeys[e.NewEditIndex].Values[0].ToString();

        Session[service.CodigoPrograma + ".id"] = id;
        Navegar(Pagina.Nuevo);
    }


    #endregion

    #region Metodos de ayuda

    void Actualizar()
    {
        try
        {
            VerError("");
            string filtro = ObtenerFiltro();

            List<CajaDeCompensacion> lstEmpleado = service.ListarDIRCAJADECOMPENSACION(filtro,Usuario);

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

    string ObtenerFiltro()
    {
        string filtro = string.Empty;

        if (!string.IsNullOrWhiteSpace(txtconsecutivo.Text))
        {
            filtro += " and CAJACOMPENSACION.consecutivo  = " + txtconsecutivo.Text;
        }

        if (!string.IsNullOrWhiteSpace(txtIdentificacion.Text))
        {
            filtro += " and CAJACOMPENSACION.nit  = " + txtIdentificacion.Text;
        }

        if (!string.IsNullOrWhiteSpace(txtnombre.Text))
        {
            filtro += " and CAJACOMPENSACION.nombre  like '%" + txtnombre.Text + "%'";
        }

        if (!string.IsNullOrWhiteSpace(txtdir.Text))
        {
            filtro += " and CAJACOMPENSACION.direccion  like '%" + txtdir.Text + "%'";
        }

        if (!string.IsNullOrWhiteSpace(txttel.Text))
        {
            filtro += " and CAJACOMPENSACION.telefono   = " + txttel.Text;
        }

        if (!string.IsNullOrWhiteSpace(txtcodpila.Text))
        {
            filtro += " and CAJACOMPENSACION.codigopila = " + txtcodpila.Text;
        }

        if (!string.IsNullOrWhiteSpace(filtro))
        {
            StringHelper stringHelper = new StringHelper();
            filtro = stringHelper.QuitarAndYColocarWhereEnFiltroQuery(filtro);
        }

        return filtro;
    }

    #endregion

}