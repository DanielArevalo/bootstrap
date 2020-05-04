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
using Xpinn.Aportes.Entities;
using Xpinn.Aportes.Services;
using System.Text;
using System.Web.UI.HtmlControls;
using System.IO;

partial class Lista : GlobalWeb
{
    private Xpinn.Aportes.Services.AporteServices AporteServicio = new Xpinn.Aportes.Services.AporteServices();
    PoblarListas Poblar = new PoblarListas();


    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            // VisualizarOpciones(AporteServicio.ProgramaCruce, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoNuevo += btnNuevo_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
            toolBar.MostrarExportar(true);
            toolBar.eventoExportar += btnExportar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AporteServicio.ProgramaCruce, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                Session["solicitudRetiro"] = null;
                Actualizar();
                CargarDropDown();
                            
                if (Session[AporteServicio.ProgramaCruce + ".consulta"] != null)
                    Actualizar();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AporteServicio.ProgramaCruce, "Page_Load", ex);
        }
    }


    protected void CargarDropDown()
    {
        Poblar.PoblarListaDesplegable("TIPOIDENTIFICACION", "*","","1", ddlTipoIdentificacion, (Usuario)Session["usuario"]);
        Poblar.PoblarListaDesplegable("MOTIVO_RETIRO", "*", "", "1", DdlMotRetiro, (Usuario)Session["usuario"]);
    }

    protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    {
        GuardarValoresConsulta(pConsulta, AporteServicio.ProgramaCruce);
        Session[AporteServicio.ProgramaCruce + ".consulta"] = 0;
        Navegar(Pagina.Nuevo);
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        GuardarValoresConsulta(pConsulta, AporteServicio.ProgramaCruce);
        Actualizar();
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        LimpiarValoresConsulta(pConsulta, AporteServicio.ProgramaCruce);
        txtFecharetiro.Text = "";
        gvLista.DataSource = null;
        panelGrilla.Visible = false;
    }


    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        String id = gvLista.DataKeys[gvLista.SelectedRow.RowIndex].Value.ToString();
        Session[AporteServicio.ProgramaCruce + ".id"] = id;
        Session[AporteServicio.ProgramaCruce + ".consulta"] = 1;
        Navegar(Pagina.Nuevo);
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
            BOexcepcion.Throw(AporteServicio.ProgramaCruce, "gvLista_PageIndexChanging", ex);
        }
    }

    private void ConsultarCliente(String pIdObjeto)
    {
        Xpinn.Aportes.Services.AporteServices AportesServicio = new Xpinn.Aportes.Services.AporteServices();
        Xpinn.Aportes.Entities.Aporte aporte = new Xpinn.Aportes.Entities.Aporte();
        String IdObjeto = txtIdentificacion.Text;
        aporte = AportesServicio.ConsultarClienteAporte(IdObjeto,0, (Usuario)Session["usuario"]);

        if (!string.IsNullOrEmpty(aporte.nombre.ToString()))
            txtNombres.Text = HttpUtility.HtmlDecode(aporte.nombre);
    }

    private void Actualizar()
    {      
        try
        {
            List<Xpinn.Aportes.Entities.Aporte> lstConsulta = new List<Xpinn.Aportes.Entities.Aporte>();
            string pFiltro = obtFiltro();
            DateTime pFechaRetiro;
            pFechaRetiro = txtFecharetiro.ToDateTime == null ? DateTime.MinValue : txtFecharetiro.ToDateTime;
            lstConsulta = AporteServicio.ListarRetiros(pFiltro, pFechaRetiro, (Usuario)Session["usuario"]);

            gvLista.PageSize = pageSize;
            gvLista.EmptyDataText = emptyQuery;
            gvLista.DataSource = lstConsulta;
            Session["DTDETALLE"] = lstConsulta;
            if (lstConsulta.Count > 0)
            {
                panelGrilla.Visible = true;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                gvLista.DataBind();
                lblInfo.Visible = false;
            }
            else
            {
                panelGrilla.Visible = false;
                lblInfo.Visible = true;
                lblTotalRegs.Visible = false;
            }
           
            Session.Add(AporteServicio.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AporteServicio.CodigoPrograma, "Actualizar", ex);
        }        
    }



    public string obtFiltro()
    {
        string filtro = string.Empty;

        if(txtIdentificacion.Text != "")
            filtro += " AND A.IDENTIFICACION = '" + txtIdentificacion.Text.Trim() + "'";
        if (ddlTipoIdentificacion.SelectedIndex > 0)
            filtro += " AND A.TIPO_IDENTIFICACION = "+ ddlTipoIdentificacion.SelectedValue;
        if(txtNombres.Text != "")
            filtro += " AND A.NOMBRES LIKE '%" + txtNombres.Text.Trim() + "%'";
        if(txtApellidos.Text != "")
            filtro += " AND A.APELLIDOS LIKE '%" + txtApellidos.Text.Trim() + "%'";
        if (DdlMotRetiro.SelectedIndex > 0)
            filtro += " AND B.COD_MOTIVO = " + DdlMotRetiro.SelectedValue ;
        if(txtActa.Text != "")
            filtro += " AND B.ACTA = '" + txtActa.Text.Trim() + "'";
        if(txtCodigoNomina.Text != "")
            filtro += " AND A.COD_NOMINA = " + txtCodigoNomina.Text.Trim();

        if (!string.IsNullOrEmpty(filtro))
        {
            filtro = filtro.Substring(4);
            filtro = " WHERE " + filtro;
        }

        return filtro;
    }


    protected void btnExportar_Click(object sender, EventArgs e)
    {
        try
        {
            //   ExportToExcel(gvLista);


            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            Page pagina = new Page();
            dynamic form = new HtmlForm();
            gvLista.AllowPaging = false;
            gvLista.PageSize = 1500;
            gvLista.EnableViewState = false;
            pagina.EnableEventValidation = false;
            pagina.DesignerInitialize();
            pagina.Controls.Add(form);
            form.Controls.Add(gvLista);
            pagina.RenderControl(htw);
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=Cruces.xls");
            Response.Charset = "UTF-8";
            Response.Write(sb.ToString());
            Response.End();

        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }

    }


}