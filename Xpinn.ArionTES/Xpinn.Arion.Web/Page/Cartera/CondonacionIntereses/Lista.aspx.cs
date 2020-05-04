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
using Xpinn.FabricaCreditos.Services;
using Xpinn.FabricaCreditos.Entities;


partial class Lista : GlobalWeb
{
    private Xpinn.Cartera.Services.CondonacionInteresService CondonacionServicio = new Xpinn.Cartera.Services.CondonacionInteresService();    

    /// <summary>
    /// Ingrsesar a la funcionalidad y mostrar la tool bar con botón de consulta y limpiar
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(CondonacionServicio.CodigoPrograma, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CondonacionServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    /// <summary>
    /// Cargar la página, llenar combos de listado de oficinas
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
       
        try
        {
            if (!IsPostBack)
            {
                 LlenarComboOficinas(ddlOficinas);
                CargarValoresConsulta(pConsulta, CondonacionServicio.CodigoPrograma);
                if (Session[CondonacionServicio.CodigoPrograma + ".consulta"] != null)
                    Actualizar();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CondonacionServicio.CodigoPrograma, "Page_Load", ex);
        }
    }

    /// <summary>
    /// Inhabilitar botón de nuevo
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    {

    }

    /// <summary>
    /// Botón de consulta de los datos del crédito según los filtros realizados.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        Page.Validate();

        if (Page.IsValid)
        {
            GuardarValoresConsulta(pConsulta, CondonacionServicio.CodigoPrograma);
            Actualizar();
        }    
    }

    /// <summary>
    /// Boton para limpiar los campos de la pantalla
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        LimpiarValoresConsulta(pConsulta, CondonacionServicio.CodigoPrograma);
    }

    /// <summary>
    /// Inhabilitar botón de la grilla
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvLista_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }

    /// <summary>
    /// Acción para cuando se pasa a la siguiente página en la grilla
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        String id = gvLista.DataKeys[gvLista.SelectedRow.RowIndex].Value.ToString();
        Session[CondonacionServicio.CodigoPrograma + ".id"] = id;
        Navegar(Pagina.Detalle);
    }

    /// <summary>
    /// Evento cuando se selecciona un crédito de la grilla
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvLista.Rows[e.NewEditIndex].Cells[0].Text;

        Session[CondonacionServicio.CodigoPrograma + ".id"] = id;
        Navegar(Pagina.Editar);
    }

    /// <summary>
    /// Evento para borrar datos que en esta funcionalidad no aplica
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }

    /// <summary>
    /// Esto es cuando se da click para cambio de página de la grilla
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
            BOexcepcion.Throw(CondonacionServicio.CodigoPrograma, "gvLista_PageIndexChanging", ex);
        }
    }
   
    /// <summary>
    /// Esto es para actualizar la grilla
    /// </summary>
    private void Actualizar()
    {
        try
        {
            List<Xpinn.Cartera.Entities.CondonacionInteres> lstConsulta = new List<Xpinn.Cartera.Entities.CondonacionInteres>();
            String filtro = obtFiltro(ObtenerValores());
            lstConsulta = CondonacionServicio.ListarCredito(ObtenerValores(), (Usuario)Session["usuario"]);

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

            Session.Add(CondonacionServicio.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CondonacionServicio.CodigoPrograma, "Actualizar", ex);
        }
    }

    /// <summary>
    ///  Esto es para cargar los valores de los filtros en la entidad
    /// </summary>
    /// <returns></returns>
    private Xpinn.Cartera.Entities.CondonacionInteres ObtenerValores()
    {
        Xpinn.Cartera.Entities.CondonacionInteres vCredito = new Xpinn.Cartera.Entities.CondonacionInteres();
        if (txtNumero_radicacion.Text.Trim() != "")
            vCredito.numero_radicacion = Convert.ToInt64(txtNumero_radicacion.Text.Trim());
        if(txtIdentificacion.Text.Trim() != "")
            vCredito.identificacion = Convert.ToString(txtIdentificacion.Text.Trim());
        if(txtNombre.Text.Trim() != "")
            vCredito.nombres = Convert.ToString(txtNombre.Text.Trim());
        if (ddlOficinas.SelectedIndex != 0)
            vCredito.oficina = ddlOficinas.SelectedValue;
        if(txtLinea_credito.Text.Trim() != "")
            vCredito.linea_credito = Convert.ToString(txtLinea_credito.Text.Trim());
        if (txtCodigoNomina.Text != "")
            vCredito.cod_nomina = Convert.ToString(txtCodigoNomina.Text.Trim());

        return vCredito;
    }

    /// <summary>
    /// Llenar el combo de las oficinas
    /// </summary>
    /// <param name="ddlOficinas"></param>
    protected void LlenarComboOficinas(DropDownList ddlOficinas)
    {
        OficinaService oficinaService = new OficinaService();
        Oficina oficina = new Oficina();
        ddlOficinas.DataSource = oficinaService.ListarOficinas(oficina, (Usuario)Session["usuario"]);
        ddlOficinas.DataTextField = "nombre";
        ddlOficinas.DataValueField = "codigo";
        ddlOficinas.DataBind();
        ddlOficinas.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));
    }

    /// <summary>
    /// Obtener el filtro según los criterios ingresados
    /// </summary>
    /// <param name="credito"></param>
    /// <returns></returns>
    private string obtFiltro(CondonacionInteres credito)
    {
        String filtro = String.Empty;
        if (txtNumero_radicacion.Text.Trim() != "")
            filtro += " and numero_radicacion= " + credito.numero_radicacion;
        if (txtIdentificacion.Text.Trim() != "")
            filtro += " and identificacion like '%" + credito.identificacion + "%'";
        if (txtNombre.Text.Trim() != "")
            filtro += " and nombres like '%" + credito.nombre + "%'";
        if (txtPrimer_apellido.Text.Trim() != "")
            filtro += " or nombres like '%" + credito.primer_apellido + "%'";     
        if (txtLinea_credito.Text.Trim() != "")
            filtro += " and cod_linea_credito= '" + credito.linea_credito + "'";
        if (ddlOficinas.SelectedIndex != 0)
            filtro += " and cod_oficina= '" + credito.oficina + "'";      
        else
            filtro += "and estado In ('A', 'G', 'C')";

        if (!string.IsNullOrEmpty(filtro))
        {
            filtro = filtro.Substring(4);
            filtro = "Where " + filtro;
        }
        return filtro;
    }

}