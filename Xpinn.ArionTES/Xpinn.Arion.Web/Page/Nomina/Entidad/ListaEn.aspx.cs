using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Nomina.Entities;
using Xpinn.Nomina.Services;
using Xpinn.Util;

public partial class ListaEn : GlobalWeb
{
    Nomina_EntidadService entidadserver = new Nomina_EntidadService();

    #region Eventos Iniciales

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(entidadserver.CodigoPrograma, "L");

            Site toolBar = (Site)Master;
            toolBar.eventoLimpiar += btnLimpiar_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
            ctlMensajeBorrar.eventoClick += CtlMensajeBorrar_eventoClick;
            toolBar.eventoNuevo += (s, evt) => {
                Session.Remove(entidadserver.CodigoPrograma + ".id");
                Navegar(Pagina.Nuevo);
            };
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(entidadserver.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InicializarPagina();
        }

    }

    void InicializarPagina()
    {
        LlenarListasDesplegables(TipoLista.Ciudades, ddlCiudad);
    }

    #endregion


    #region Eventos Intermedios

    protected void ENgrid_SelectedIndexChanged(object sender, EventArgs e)
    {
        long id = Convert.ToInt64(ENgrid.SelectedRow.Cells[1].Text);

        Session[entidadserver.CodigoPrograma + ".id"] = id;
        Navegar(Pagina.Nuevo);
    }

    protected void ENgrid_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

        long idBorrar = Convert.ToInt64(e.Keys[0]);

        ViewState.Add("idBorrar", idBorrar);

        ctlMensajeBorrar.MostrarMensaje("Seguro que deseas eliminar esta registro?");
    }

    protected void ENgrid_RowDataBound(object sender, GridViewRowEventArgs e)
    {
      
        Label lblentidad = (Label)e.Row.FindControl("lblBiometria");
        if (lblentidad != null)
        {
            Int32 indicador = Convert.ToInt32(lblentidad.Text);
            if(indicador > 0)
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
                entidadserver.EliminarNomina_Entidad(idBorrar, Usuario);
                Actualizar();
            }
            catch (Exception ex)
            {
                VerError("Error al borrar el registro, " + ex.Message);
            }
        }
    }

    protected void ENgrid_PageIndexChanging(object sender, GridViewDeleteEventArgs e) { }

    void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        txtCodigo.Text = string.Empty;
        txtNumeIdentificacion.Text = string.Empty;
        txtNombres.Text = string.Empty;
        ddlCiudad.SelectedIndex = 0;
        ddlEntidad.SelectedIndex = 0;
    }

    void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        Actualizar();
    }

    protected void ENgrid_RowEditing(object sender, GridViewEditEventArgs e) {

        string id = ENgrid.DataKeys[e.NewEditIndex].Values[0].ToString();

        Session[entidadserver.CodigoPrograma + ".id"] = id;
        Navegar(Pagina.Nuevo);

    }


    #endregion 


    #region Metodos De ayuda

    void Actualizar()
    {
        try
        {
            VerError("");
            string filtro = ObtenerFiltro();

            List<Nomina_Entidad> lstEmpleado = entidadserver.ListarNomina_Entidad(filtro, Usuario);

            if (lstEmpleado.Count > 0)
            {
                lblTotalRegs.Text = "Se encontraron " + lstEmpleado.Count + " registros!.";
            }
            else
            {
                lblTotalRegs.Text = "Su consulta no obtuvo ningún resultado!.";
            }

            ENgrid.DataSource = lstEmpleado;
            ENgrid.DataBind();
        }
        catch (Exception ex)
        {
            VerError("Error al actualizar la grilla, " + ex.Message);
        }
    }

    string ObtenerFiltro()
    {
        string filtro = string.Empty;

        if (!string.IsNullOrWhiteSpace(txtCodigo.Text))
        {
            filtro += " and Nomina_Entidad.consecutivo like '%" + txtCodigo.Text + "%'";
        }

        if (!string.IsNullOrWhiteSpace(txtNumeIdentificacion.Text))
        {
            filtro += " and Nomina_Entidad.nit = " + txtNumeIdentificacion.Text;
        }

        if (!string.IsNullOrWhiteSpace(txtNombres.Text))
        {
            filtro += " and Nomina_Entidad.nom_persona like '%" + txtNombres.Text + "%'";
        }

        if (!string.IsNullOrWhiteSpace(ddlCiudad.SelectedValue))
        {
            filtro += " and Nomina_Entidad.ciudad = " + ddlCiudad.SelectedValue;
        }

        if (!string.IsNullOrWhiteSpace(ddlEntidad.SelectedValue))
        {
            filtro += " and Nomina_Entidad.clase = " + ddlEntidad.SelectedValue;
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