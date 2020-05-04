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
using Xpinn.Cartera.Services;
using Xpinn.Cartera.Entities;

partial class Lista : GlobalWeb
{
    private Xpinn.Cartera.Services.CertificacionService CertificacionService = new Xpinn.Cartera.Services.CertificacionService();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(CertificacionService.CodigoPrograma, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CertificacionService.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                LlenarComboOficinas(ddlOficinas);
                CargarValoresConsulta(pConsulta, CertificacionService.CodigoPrograma);
                if (Session[CertificacionService.CodigoPrograma + ".consulta"] != null)
                    Actualizar();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CertificacionService.CodigoPrograma, "Page_Load", ex);
        }
    }

    /// <summary>
    /// Método para consultar los datos del crédito seleccionado
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        Page.Validate();
        gvLista.Visible = true;

        if (Page.IsValid)
        {
            GuardarValoresConsulta(pConsulta, CertificacionService.CodigoPrograma);
            Actualizar();
        }
    }

    /// <summary>
    /// Método para limpiar los datos en pantalla
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        gvLista.DataSourceID = null;
        gvLista.DataBind();
        gvLista.Visible = false;
        txtNombre.Text = "";
        txtIdentificacion.Text = "";
        txtNumero_radicacion.Text = "";
        txtLinea_credito.Text = "";
        lblTotalRegs.Visible = false;
        LimpiarValoresConsulta(pConsulta, CertificacionService.CodigoPrograma);
    }

    /// <summary>
    /// Método para control de selección de la grilla
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        String id = gvLista.DataKeys[gvLista.SelectedRow.RowIndex].Value.ToString();
        Session[CertificacionService.CodigoPrograma + ".id"] = id;
        Navegar(Pagina.Detalle);
    }

    /// <summary>
    /// Méotod para cuando se selecciona un registro de la grilla
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvLista.Rows[e.NewEditIndex].Cells[0].Text;

        Session[CertificacionService.CodigoPrograma + ".id"] = id;
        Navegar(Pagina.Editar);
    }

    /// <summary>
    /// Método para cambio de página en la grilla
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvLista.PageIndex = e.NewPageIndex;
            Actualizar();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CertificacionService.CodigoPrograma, "gvLista_PageIndexChanging", ex);
        }
    }

    /// <summary>
    /// Método para llenar la grilla.
    /// </summary>
    private void Actualizar()
    {
        try
        {
            List<Xpinn.FabricaCreditos.Entities.Credito> lstConsulta = new List<Xpinn.FabricaCreditos.Entities.Credito>();
            String filtro = obtFiltro();
            DateTime pFecha;
            pFecha = System.DateTime.Now;
            lstConsulta = CertificacionService.ListarCredito(pFecha,filtro,(Usuario)Session["usuario"]);

            gvLista.PageSize = 15;
            String emptyQuery = "Fila de datos vacia";
            gvLista.EmptyDataText = emptyQuery;
            gvLista.DataSource = lstConsulta;

            if (lstConsulta.Count > 0)
            {
                gvLista.Visible = true;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                gvLista.DataBind();
                ValidarPermisosGrilla(gvLista);
            }
            else
            {
                gvLista.Visible = false;
                lblTotalRegs.Visible = false;
            }

            Session.Add(CertificacionService.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }


    /// <summary>
    /// LLenar el dropdownlist que permite filtras por oficinas
    /// </summary>
    /// <param name="ddlOficinas"></param>
    protected void LlenarComboOficinas(DropDownList ddlOficinas)
    {
        Xpinn.FabricaCreditos.Services.OficinaService oficinaService = new Xpinn.FabricaCreditos.Services.OficinaService();
        Xpinn.FabricaCreditos.Entities.Oficina oficina = new Xpinn.FabricaCreditos.Entities.Oficina();

        Usuario usuap = (Usuario)Session["usuario"];

        int cod = Convert.ToInt32(usuap.codusuario);
        int consulta = oficinaService.UsuarioPuedeConsultarCreditosOficinas(cod, (Usuario)Session["Usuario"]);
        if (consulta >= 1)
        {
            ddlOficinas.DataSource = oficinaService.ListarOficinas(oficina, (Usuario)Session["usuario"]);
            ddlOficinas.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));

            ddlOficinas.DataTextField = "nombre";
            ddlOficinas.DataValueField = "codigo";
            ddlOficinas.DataBind();
            ddlOficinas.SelectedValue = Convert.ToString(usuap.cod_oficina);
        }
        else
        {
            ddlOficinas.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));
            ddlOficinas.Items.Insert(1, new ListItem(Convert.ToString(usuap.nombre_oficina), Convert.ToString(usuap.cod_oficina)));
            ddlOficinas.DataBind();
        }

    }

    /// <summary>
    /// Generar las condiciones de acuerdo a los filtros ingresados
    /// </summary>
    /// <param name="credito"></param>
    /// <returns></returns>
    private string obtFiltro()
    {
        String filtro = String.Empty;
        if (txtNumero_radicacion.Text.Trim() != "")
            filtro += " and v_creditos.numero_radicacion = " + txtNumero_radicacion.Text.Trim();
        if (txtIdentificacion.Text.Trim() != "")
            filtro += " and v_creditos.identificacion like '%" + txtIdentificacion.Text.Trim() + "%'";
        if (txtNombre.Text.Trim() != "")
            filtro += " and v_creditos.nombres like '%" + txtNombre.Text.Trim() + "%'";
        if (txtLinea_credito.Text.Trim() != "")
            filtro += " and v_creditos.cod_linea_credito = '" + txtLinea_credito.Text.Trim() + "'";
        if (ddlOficinas.SelectedIndex != 0)
            filtro += " and v_creditos.cod_oficina = '" + ddlOficinas.SelectedValue + "'";
        if (txtCodigoNomina.Text != "")
            filtro += " and cod_nomina like '%" + txtCodigoNomina.Text + "%'";

        filtro += " and v_creditos.estado = 'C'";

        if (!string.IsNullOrEmpty(filtro))
        {
            filtro = filtro.Substring(4);
            filtro = "where " + filtro;
        }

        return filtro;
    }


}