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
using Xpinn.FabricaCreditos.Entities;
using Xpinn.FabricaCreditos.Services;

partial class Lista : GlobalWeb
{
    private Xpinn.Cartera.Services.ReliquidacionService ReliquidacionServicio = new Xpinn.Cartera.Services.ReliquidacionService();
    private CreditoService CreditoServicio = new CreditoService();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(CreditoServicio.CodigoProgramaModificacion, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CreditoServicio.CodigoProgramaModificacion, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                LlenarComboOficinas(ddlOficinas);
                cargarlistas();
                CargarValoresConsulta(pConsulta, CreditoServicio.CodigoProgramaModificacion);
                if (Session[CreditoServicio.CodigoProgramaModificacion + ".consulta"] != null)
                    Actualizar();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CreditoServicio.CodigoProgramaModificacion, "Page_Load", ex);
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
            GuardarValoresConsulta(pConsulta, CreditoServicio.CodigoProgramaModificacion);
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
        txtNombres.Text = "";
        txtIdentificacion.Text = "";
        txtNumero_radicacion.Text = "";
        txtCodigoNomina.Text = "";
        //ddllineacredito.Text = "";
        lblTotalRegs.Visible = false;
        LimpiarValoresConsulta(pConsulta, CreditoServicio.CodigoProgramaModificacion);
    }
    
    /// <summary>
    /// Método para control de selección de la grilla
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        String id = gvLista.DataKeys[gvLista.SelectedRow.RowIndex].Value.ToString();
        Session[CreditoServicio.CodigoProgramaModificacion + ".id"] = id;
        Navegar(Pagina.Nuevo);
    }

    /// <summary>
    /// Méotod para cuando se selecciona un registro de la grilla
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = Convert.ToString(gvLista.Rows[e.NewEditIndex].Cells[0].Text);
        Session[CreditoServicio.CodigoProgramaModificacion + ".id"] = id;
        Navegar(Pagina.Nuevo);
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
            BOexcepcion.Throw(CreditoServicio.CodigoProgramaModificacion, "gvLista_PageIndexChanging", ex);
        }
    }

    protected void cargarlistas() 
    {
        Xpinn.FabricaCreditos.Services.LineasCreditoService linahorroServicio = new Xpinn.FabricaCreditos.Services.LineasCreditoService();
        Xpinn.FabricaCreditos.Entities.LineasCredito linahorroVista = new Xpinn.FabricaCreditos.Entities.LineasCredito();
        ddllineacredito.DataTextField = "NOMBRE";
        ddllineacredito.DataValueField = "COD_LINEA_CREDITO";
        ddllineacredito.DataSource = linahorroServicio.ListarLineasCredito(linahorroVista, (Usuario)Session["usuario"]);
        ddllineacredito.DataBind();
        ddllineacredito.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));

    
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
            lstConsulta = ReliquidacionServicio.ListarCreditoss((Usuario)Session["usuario"], filtro);

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

            Session.Add(CreditoServicio.CodigoProgramaModificacion + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CreditoServicio.CodigoProgramaModificacion, "Actualizar", ex);
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
        if(txtIdentificacion.Text!="")
            filtro += " And identificacion = '" + txtIdentificacion.Text+"'";
        if(txtNumero_radicacion.Text!="")
            filtro += " And numero_radicacion = " + txtNumero_radicacion.Text;
        if (txtNumero_Solicitud.Text != "")
            filtro += " And numerosolicitud = " + txtNumero_Solicitud.Text;
        if (txtCodigoNomina.Text != "")
            filtro += " and cod_nomina like '" + txtCodigoNomina.Text + "'";

        return filtro;
    }

    
}