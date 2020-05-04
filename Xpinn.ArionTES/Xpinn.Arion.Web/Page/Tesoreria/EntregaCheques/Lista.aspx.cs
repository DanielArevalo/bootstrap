using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Text;
using System.IO;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Xpinn.Util;

partial class Lista : GlobalWeb
{
    private Xpinn.Contabilidad.Services.TerceroService TerceroServicio = new Xpinn.Contabilidad.Services.TerceroService();
    private Xpinn.Tesoreria.Services.EntregaChequesServices entregaCheques = new Xpinn.Tesoreria.Services.EntregaChequesServices();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(entregaCheques.CodigoPrograma, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
            toolBar.eventoExportar += btnExportar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(entregaCheques.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                ctlBusquedaPersonas.CodigoPrograma = entregaCheques.CodigoPrograma;
                ctlBusquedaPersonas.Filtro = " persona.cod_persona In (Select e.cod_benef From v_entrega_cheques e Where e.estado_cheque = 1) ";
                CargarValoresConsulta(pConsulta, entregaCheques.CodigoPrograma);
                if (Session[entregaCheques.CodigoPrograma + ".nid"] != null)
                {
                    ctlBusquedaPersonas.Identificacion = Session[entregaCheques.CodigoPrograma + ".nid"].ToString();
                    Session.Remove(entregaCheques.CodigoPrograma + ".nid");
                    ctlBusquedaPersonas.Actualizar(0);
                }
                if (Session[entregaCheques.CodigoPrograma + ".consulta"] != null)
                    ctlBusquedaPersonas.Actualizar(0);

            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(entregaCheques.CodigoPrograma, "Page_Load", ex);
        }
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        GuardarValoresConsulta(pConsulta, entregaCheques.CodigoPrograma);        
        ctlBusquedaPersonas.Actualizar(0);
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        ctlBusquedaPersonas.Identificacion = "";
        LimpiarValoresConsulta(ctlBusquedaPersonas.pBusquedaDatos, entregaCheques.CodigoPrograma);
    }

    protected void btnExportar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        GridView gvLista = ctlBusquedaPersonas.gvListado;
        if (gvLista.Rows.Count > 0 && Session["DTPERSONAS"] != null)
        {
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            Page pagina = new Page();
            dynamic form = new HtmlForm();
            gvLista.Columns[0].Visible = false;
            gvLista.Columns[1].Visible = false;
            gvLista.Columns[2].Visible = false;
            gvLista.AllowPaging = false;
            gvLista.DataSource = Session["DTPERSONAS"];
            gvLista.DataBind();
            gvLista.EnableViewState = false;
            pagina.EnableEventValidation = false;
            pagina.DesignerInitialize();
            pagina.Controls.Add(form);
            form.Controls.Add(gvLista);
            pagina.RenderControl(htw);
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=Personas.xls");
            Response.Charset = "UTF-8";
            Response.ContentEncoding = Encoding.Default;
            Response.Write(sb.ToString());
            Response.End();
        }
        else
        {
            VerError("No existen datos, genere la consulta");
        }
    }


}