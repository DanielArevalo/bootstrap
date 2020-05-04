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
using Xpinn.NIIF.Entities;
using Xpinn.NIIF.Services;
using System.Text;
using System.IO;
using System.Web.UI.HtmlControls;

partial class Lista : GlobalWeb
{

    TipoActivoNIFServices TipoService = new TipoActivoNIFServices();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(TipoService.CodigoPrograma, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoNuevo += btnNuevo_Click;
            toolBar.eventoExportar += btnExportar_Click;
            ctlMensaje.eventoClick += btnContinuarMen_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(TipoService.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                cargarDropdown();
                Actualizar();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(TipoService.CodigoPrograma, "Page_Load", ex);
        }
    }

    protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    {
        Navegar(Pagina.Nuevo);
    }


    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {

        Page.Validate();
        gvLista.Visible = true;
        if (Page.IsValid)
        {
            Actualizar();
        }
    }


    public void PoblarLista(string pTabla, DropDownList ddlControl)
    {
        PoblarLista(pTabla, "", "", "", ddlControl);
    }

    public void PoblarLista(string pTabla, string pColumnas, string pCondicion, string pOrden, DropDownList ddlControl)
    {
        List<Xpinn.Comun.Entities.ListaDesplegable> plista = new List<Xpinn.Comun.Entities.ListaDesplegable>();
        Xpinn.Comun.Entities.ListaDesplegable pentidad = new Xpinn.Comun.Entities.ListaDesplegable();
        Xpinn.Comun.Services.ListaDeplegableService pservicio = new Xpinn.Comun.Services.ListaDeplegableService();

        ddlControl.Items.Clear();
        plista = pservicio.ListarListaDesplegable(pentidad, pTabla, pColumnas, pCondicion, pOrden, (Usuario)Session["usuario"]);
        pentidad.idconsecutivo = null;
        pentidad.descripcion = "Seleccione un item";
        plista.Insert(0, pentidad);
        ddlControl.DataTextField = "descripcion";
        ddlControl.DataValueField = "idconsecutivo";
        ddlControl.DataSource = plista;
        ddlControl.DataBind();
    }


    void cargarDropdown()
    {
        PoblarLista("Clasificacion_Activo_Nif", ddlClasificacion);
    }
    


    protected void btnExportar_Click(object sender, EventArgs e)
    {
        try
        {
            VerError("");
            if (gvLista.Rows.Count > 0 && Session["DTCDAT"] != null)
            {
                StringBuilder sb = new StringBuilder();
                StringWriter sw = new StringWriter(sb);
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                Page pagina = new Page();
                dynamic form = new HtmlForm();
                gvLista.AllowPaging = false;
                gvLista.Columns[0].Visible = false;
                gvLista.Columns[1].Visible = false;
                gvLista.DataSource = Session["DTCDAT"];
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
                Response.AddHeader("Content-Disposition", "attachment;filename=TiposActivosFijos.xls");
                Response.Charset = "UTF-8";
                Response.ContentEncoding = Encoding.Default;
                Response.Write(sb.ToString());
                Response.End();
                gvLista.AllowPaging = true;
                gvLista.DataBind();
            }
            else
            {
                VerError("No existen Datos");
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
    }

    protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvLista.PageIndex = e.NewPageIndex;
            Actualizar();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(TipoService.CodigoPrograma, "gvLista_PageIndexChanging", ex);
        }
    }



    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvLista.Rows[e.NewEditIndex].Cells[2].Text;

        Session[TipoService.CodigoPrograma + ".id"] = id;
        Navegar(Pagina.Nuevo);
    }



    protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int Id = Convert.ToInt32(gvLista.DataKeys[e.RowIndex].Values[0].ToString());
        Session["ID"] = Id;
        ctlMensaje.MostrarMensaje("Desea eliminar el registro?");
    }


    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        try
        {
            if(Session["ID"].ToString() != "")
                TipoService.EliminarTipoActivo(Convert.ToInt32(Session["ID"].ToString()),(Usuario)Session["usuario"]);
            Actualizar();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(TipoService.CodigoPrograma, "btnContinuarMen_Click", ex);        
        }
    }

    private void Actualizar()
    {
        try
        {
            List<TipoActivoNIF> lstConsulta = new List<TipoActivoNIF>();
            String filtro = obtFiltro(ObtenerValores());
            
            lstConsulta = TipoService.ListarTipoActivo(filtro, (Usuario)Session["usuario"]);

            gvLista.PageSize = 15;
            String emptyQuery = "Fila de datos vacia";
            gvLista.EmptyDataText = emptyQuery;           

            if (lstConsulta.Count > 0)
            {
                gvLista.DataSource = lstConsulta;
                gvLista.DataBind();
                panelGrilla.Visible = true;                
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                Session["DTCDAT"] = lstConsulta;
            }
            else
            {
                Session["DTCDAT"] = null;
                panelGrilla.Visible = false;                
                lblTotalRegs.Visible = false;
            }
            Session.Add(TipoService.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(TipoService.CodigoPrograma, "Actualizar", ex);
        }
    }

    private TipoActivoNIF ObtenerValores()
    {
        TipoActivoNIF vApertu = new TipoActivoNIF();
        if (txtTipoAct.Text.Trim() != "")
            vApertu.tipo_activo_nif = Convert.ToInt32(txtTipoAct.Text);
        if (txtDescripcion.Text != "")
            vApertu.descripcion = txtDescripcion.Text;
        if (ddlClasificacion.SelectedIndex != 0)
            vApertu.codclasificacion_nif = Convert.ToInt32(ddlClasificacion.SelectedValue);
        
        return vApertu;
    }



    private string obtFiltro(TipoActivoNIF vApertu)
    {
        String filtro = String.Empty;

        if (txtTipoAct.Text.Trim() != "")
            filtro += " and  TIPO_ACTIVO_NIF = " + vApertu.tipo_activo_nif;
        if (txtDescripcion.Text != "")
            filtro += " and  Tipo_Activo_Nif.DESCRIPCION Like '%" + vApertu.descripcion.ToUpper() + "%'";
        if (ddlClasificacion.SelectedIndex != 0)
            filtro += " and  Tipo_Activo_Nif.CODCLASIFICACION_NIF = " + vApertu.codclasificacion_nif;
        
        return filtro;
    }


    
}