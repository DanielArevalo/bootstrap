using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Nomina.Entities;
using Xpinn.Nomina.Services;
using Xpinn.Util;

public partial class Lista : GlobalWeb
{
    EmpleadoService _empleadoServices = new EmpleadoService();

    #region Eventos Iniciales
    
    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(_empleadoServices.CodigoPrograma, "L");

            Site toolBar = (Site)Master;
            toolBar.eventoLimpiar += btnLimpiar_Click;
            toolBar.eventoConsultar += btnConsultar_Click;

            toolBar.eventoExportar += btnExportar_Click;

            ctlMensajeBorrar.eventoClick += CtlMensajeBorrar_eventoClick;
            toolBar.eventoNuevo += (s, evt) => {
                Session.Remove(_empleadoServices.CodigoPrograma + ".id");
                Navegar(Pagina.Nuevo);
            };
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_empleadoServices.CodigoPrograma, "Page_PreInit", ex);
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
        LlenarListasDesplegables(TipoLista.Oficinas, ddlOficina);
    }


    #endregion


    #region Eventos Intermedios GridView - Botonera


    protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvLista.PageIndex = e.NewPageIndex;
        Actualizar();
    }

    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        long id = Convert.ToInt64(gvLista.SelectedRow.Cells[2].Text);
        long codigoEmpleado = Convert.ToInt64(gvLista.DataKeys[gvLista.SelectedRow.RowIndex].Value);

        Session[_empleadoServices.CodigoPrograma + ".id"] = id;
        Session[_empleadoServices.CodigoPrograma + ".idEmpleado"] = codigoEmpleado;

        Navegar(Pagina.Nuevo);
    }

    protected void OnRowCommandDeleting(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName != "Delete") return;

        long idBorrar = Convert.ToInt64(gvLista.DataKeys[Convert.ToInt32(e.CommandArgument)].Value);

        ViewState.Add("idBorrar", idBorrar);

        ctlMensajeBorrar.MostrarMensaje("Seguro que deseas eliminar esta registro?");
    }

    void CtlMensajeBorrar_eventoClick(object sender, EventArgs e)
    {
        if (ViewState["idBorrar"] != null)
        {
            long idBorrar = Convert.ToInt64(ViewState["idBorrar"]);

            try
            {
                _empleadoServices.EliminarEmpleados(idBorrar, Usuario);
                Actualizar();
            }
            catch (Exception ex)
            {
                VerError("Error al borrar el registro, " + ex.Message);
            }
        }
    }

    protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }

    void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        txtIdentificacion.Text = string.Empty;
        txtCodPersona.Text = string.Empty;
        ddlOficina.SelectedIndex = 0;
        txtNombre.Text = string.Empty;
    }

    void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        Actualizar();
    }

    void btnExportar_Click(object sender, ImageClickEventArgs e)
    {
        gvLista.AllowPaging = false;
        Actualizar();

        ExportarGridViewEnExcel(gvLista, "Empleados");

        gvLista.AllowPaging = true;
        Actualizar();
    }





    #endregion


    #region Métodos Ayuda


    void Actualizar()
    {
        try
        {
            VerError("");
            string filtro = ObtenerFiltro();

            List<Empleados> lstEmpleado = _empleadoServices.ListarEmpleados(filtro, Usuario);

            if (lstEmpleado.Count > 0)
            {
                lblTotalRegs.Text = "Se encontraron " + lstEmpleado.Count + " registros!.";
            }
            else
            {
                lblTotalRegs.Text = "Su consulta no obtuvo ningún resultado!.";
            }

            gvLista.DataSource = lstEmpleado;
            gvLista.DataBind();
        }
        catch (Exception ex)
        {
            VerError("Error al actualizar la grilla, " + ex.Message);
        }
    }


    string ObtenerFiltro()
    {
        string filtro = string.Empty;

        if (!string.IsNullOrWhiteSpace(txtIdentificacion.Text))
        {
            filtro += " and per.identificacion like '%" + txtIdentificacion.Text + "%'";
        }

        if (!string.IsNullOrWhiteSpace(txtCodPersona.Text))
        {
            filtro += " and per.cod_persona = " + txtCodPersona.Text;
        }

        if (!string.IsNullOrWhiteSpace(ddlOficina.SelectedValue))
        {
            filtro += " and per.cod_oficina = " + ddlOficina.SelectedValue;
        }

        if (!string.IsNullOrWhiteSpace(txtNombre.Text))
        {
            filtro += " per.PRIMER_NOMBRE like '%" + txtNombre.Text.Trim() + "%'";
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