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
using System.Web.UI.HtmlControls;
using System.IO;
using System.Text;

partial class Lista : GlobalWeb
{
    private Xpinn.Cartera.Services.DebitoAutomaticoService DebitoAutomaticoService = new Xpinn.Cartera.Services.DebitoAutomaticoService();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(DebitoAutomaticoService.CodigoPrograma, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
            toolBar.eventoExportar += btnExportar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(DebitoAutomaticoService.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                LlenarComboOficinas(ddlOficinas);
                CargarValoresConsulta(pConsulta, DebitoAutomaticoService.CodigoPrograma);
                if (Session[DebitoAutomaticoService.CodigoPrograma + ".consulta"] != null)
                    Actualizar();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(DebitoAutomaticoService.CodigoPrograma, "Page_Load", ex);
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
            GuardarValoresConsulta(pConsulta, DebitoAutomaticoService.CodigoPrograma);
            Actualizar();
        }
    }

    void btnExportar_Click(object sender, ImageClickEventArgs e)
    {
        ExportToExcel(gvLista);
    }
    protected void ExportToExcel(GridView GridView1)
    {
        try
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=DebitoAutomatico.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            Response.ContentEncoding = Encoding.Default;
            StringWriter sw = new StringWriter();
            ExpGrilla expGrilla = new ExpGrilla();

            sw = expGrilla.ObtenerGrilla(GridView1, (List<Xpinn.Cartera.Entities.DebitoAutomatico>)Session["DTDEBITOAUTOMATICO"]);

            Response.Write(expGrilla.style);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }
        catch
        { }
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
        TxtApellidos.Text = "";

        txtIdentificacion.Text = "";
        txtCodPersona.Text = "";
        lblTotalRegs.Visible = false;
        LimpiarValoresConsulta(pConsulta, DebitoAutomaticoService.CodigoPrograma);
    }

    /// <summary>
    /// Método para control de selección de la grilla
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        String id = gvLista.DataKeys[gvLista.SelectedRow.RowIndex].Value.ToString();
        Session[DebitoAutomaticoService.CodigoPrograma + ".id"] = id;
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

        Session[DebitoAutomaticoService.CodigoPrograma + ".id"] = id;
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
            BOexcepcion.Throw(DebitoAutomaticoService.CodigoPrograma, "gvLista_PageIndexChanging", ex);
        }
    }

    /// <summary>
    /// Método para llenar la grilla.
    /// </summary>
    private void Actualizar()
    {
        try
        {
            List<Xpinn.Cartera.Entities.DebitoAutomatico> lstConsulta = new List<Xpinn.Cartera.Entities.DebitoAutomatico>();
            String filtro = obtFiltro(ObtenerValores());
            DateTime pFecha;
            Int64 cuenta = 0;
            pFecha = System.DateTime.Now;
       
          
            if (chkcuentaahorros.Checked && filtro == "")
            {
                cuenta = 1;
            }

            if (filtro != "")
            {

                cuenta = 0;
                chkcuentaahorros.Checked = false;
            }
            lstConsulta = DebitoAutomaticoService.ListarDatosClientes(ObtenerValores(), cuenta,(Usuario)Session["usuario"]);

            gvLista.PageSize = 15;
            String emptyQuery = "Fila de datos vacia";
            gvLista.EmptyDataText = emptyQuery;
            gvLista.DataSource = lstConsulta;
            Session["DTDEBITOAUTOMATICO"] = lstConsulta;

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

            Session.Add(DebitoAutomaticoService.CodigoPrograma + ".consulta", 1);
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

       
            ddlOficinas.DataSource = oficinaService.ListarOficinas(oficina, (Usuario)Session["usuario"]);
          
            ddlOficinas.DataTextField = "nombre";
            ddlOficinas.DataValueField = "codigo";
            ddlOficinas.DataBind();
           ddlOficinas.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));


    }

    /// <summary>
    /// Generar las condiciones de acuerdo a los filtros ingresados
    /// </summary>
    /// <param name="credito"></param>
    /// <returns></returns>
    private string obtFiltro(DebitoAutomatico debitoautomatico)
    {
        String filtro = String.Empty;

        if (txtCodPersona.Text.Trim() != "")
            filtro += " and vp.cod_persona = " + txtCodPersona.Text.Trim();

        if (txtIdentificacion.Text.Trim() != "")
            filtro += " and vp.identificacion = " + txtIdentificacion.Text.Trim();
        if (txtNombre.Text.Trim() != "")
            filtro += " and vp.nombres like '%" + txtNombre.Text.Trim() + "%'";
        if (TxtApellidos.Text.Trim() != "")
            filtro += " and vp.apellidos like '%" + TxtApellidos.Text.Trim() + "%'";
        if (ddlOficinas.SelectedIndex != 0)
            filtro += " and vp.cod_oficina = '" + ddlOficinas.SelectedValue + "'";

        

        if (!string.IsNullOrEmpty(filtro))
        {
            filtro = filtro.Substring(4);
            filtro = " where " + filtro;
        }

        return filtro;
    }


    private Xpinn.Cartera.Entities.DebitoAutomatico ObtenerValores()
    {
        Xpinn.Cartera.Entities.DebitoAutomatico vdebito = new Xpinn.Cartera.Entities.DebitoAutomatico();

        if (txtCodPersona.Text.Trim() != "")
            vdebito.cod_persona = Convert.ToInt64(txtCodPersona.Text.Trim());

        if (txtIdentificacion.Text.Trim() != "")
            vdebito.identificacion = Convert.ToString(txtIdentificacion.Text.Trim());
         if (txtNombre.Text.Trim() != "")
            vdebito.Nombres = Convert.ToString(txtNombre.Text.Trim());

        if (TxtApellidos.Text.Trim() != "")
            vdebito.Apellidos = Convert.ToString(TxtApellidos.Text.Trim());

        if (ddlOficinas.SelectedIndex != 0)
            vdebito.cod_oficina = Convert.ToInt64(ddlOficinas.SelectedValue);

        
        return vdebito;
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }



}