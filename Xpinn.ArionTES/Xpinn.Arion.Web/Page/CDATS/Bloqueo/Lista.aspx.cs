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
using Xpinn.CDATS.Entities;
using Xpinn.CDATS.Services;
using System.Text;
using System.IO;
using System.Web.UI.HtmlControls;

partial class Lista : GlobalWeb
{
    AdministracionCDATService AdmService = new AdministracionCDATService();
    AperturaCDATService ApertuService = new AperturaCDATService();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(AdmService.CodigoProgramaCDAT, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoExportar += btnExportar_Click;
            ctlMensaje.eventoClick += btnContinuarMen_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AdmService.CodigoProgramaCDAT, "Page_PreInit", ex);
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
            BOexcepcion.Throw(AdmService.CodigoProgramaCDAT, "Page_Load", ex);
        }
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


    void cargarDropdown()
    {
        Cdat Data = new Cdat();

        List<Cdat> lstTipoLinea = new List<Cdat>();

        lstTipoLinea = ApertuService.ListarTipoLineaCDAT(Data, (Usuario)Session["usuario"]);
        if (lstTipoLinea.Count > 0)
        {
            ddlTipoLinea.DataSource = lstTipoLinea;
            ddlTipoLinea.DataTextField = "NOMBRE";
            ddlTipoLinea.DataValueField = "COD_LINEACDAT";
            ddlTipoLinea.Items.Insert(0, new ListItem("Seleccione un item", "0"));
            ddlTipoLinea.SelectedIndex = 0;
            ddlTipoLinea.DataBind();
        }


        List<Cdat> lstAsesores = new List<Cdat>();

        lstAsesores = ApertuService.ListarUsuariosAsesores(Data, (Usuario)Session["usuario"]);
        if (lstAsesores.Count > 0)
        {
            ddlAsesor.DataSource = lstAsesores;
            ddlAsesor.DataTextField = "nombre";
            ddlAsesor.DataValueField = "codusuario";
            ddlAsesor.Items.Insert(0, new ListItem("Seleccione un item", "0"));
            ddlAsesor.SelectedIndex = 0;
            ddlAsesor.DataBind();
        }

        List<Cdat> lstOficina = new List<Cdat>();

        lstOficina = ApertuService.ListarOficinas(Data, (Usuario)Session["usuario"]);
        if (lstOficina.Count > 0)
        {
            ddlOficina.DataSource = lstOficina;
            ddlOficina.DataTextField = "nombre";
            ddlOficina.DataValueField = "cod_oficina";
            ddlOficina.Items.Insert(0, new ListItem("Seleccione un item", "0"));
            ddlOficina.SelectedIndex = 0;
            ddlOficina.DataBind();
        }

    }



    // exportar
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
                Response.AddHeader("Content-Disposition", "attachment;filename=AdministracionCDATS.xls");
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
            BOexcepcion.Throw(AdmService.CodigoProgramaCDAT, "gvLista_PageIndexChanging", ex);
        }
    }

  

    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvLista.DataKeys[e.NewEditIndex].Values[0].ToString();
        int estado = Convert.ToInt32(gvLista.DataKeys[e.NewEditIndex].Values[1].ToString());
        if (estado == 5 || estado == 2)
        {
            Session["ADMI"] = "";
            Session["RETURNO"] = "ADMINISTRACION";
            Session[AdmService.CodigoProgramaCDAT + ".id"] = id;
            Response.Redirect("~/Page/CDATS/Bloqueo/Nuevo.aspx");
        }
        else
        {
            Session[AdmService.CodigoProgramaCDAT + ".id"] = id;
            VerError("No puede Editar el registro seleccionado");
            Response.Redirect("~/Page/CDATS/Bloqueo/Nuevo.aspx");
        }
        e.NewEditIndex = -1;
    }



    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        try
        {
          Actualizar();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AdmService.CodigoProgramaCDAT, "btnContinuarMen_Click", ex);
        }
    }

    //correcto
    private void Actualizar()
    {
        try
        {
            List<AdministracionCDAT> lstConsulta = new List<AdministracionCDAT>();
            String filtro = obtFiltro();
            DateTime FechaApe = txtFecha.ToDateTime == null ? DateTime.MinValue : txtFecha.ToDateTime;

            lstConsulta = AdmService.getListarCdatBloquearServices(filtro,FechaApe, (Usuario)Session["usuario"]);

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

            Session.Add(AdmService.CodigoProgramaCDAT + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AdmService.CodigoProgramaCDAT, "Actualizar", ex);
        }
    }


    // correcto filtro
    private string obtFiltro()
    {
        String filtro = String.Empty;

        if (txtNroCDAT.Text.Trim() != "")
            filtro += " and  C.numero_cdat = " + txtNroCDAT.Text; 
        if (ddlTipoLinea.SelectedIndex != 0)
            filtro += " and  C.cod_lineacdat = '" + ddlTipoLinea.SelectedValue + "'";
        if (ddlAsesor.SelectedIndex != 0)
            filtro += " and  C.cod_asesor_com = " + Convert.ToInt32(ddlAsesor.SelectedValue);
        if (ddlOficina.SelectedIndex != 0)
            filtro += " and C.cod_oficina = " + Convert.ToInt32(ddlOficina.SelectedValue);

       
        return filtro;
    }



    protected void gvLista_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName == "Imprimir")
        {
            string[] valores; 
            valores = (e.CommandArgument).ToString().Split(';'); //RECUPERA CODIGO
            int cod = 0, NumFisico = 0;
            if (valores[0] != "")
                cod = Convert.ToInt32(valores[0]);

            Session["NUMFISICO"] = 0;
            if (valores[1] != "")
            {
                NumFisico = Convert.ToInt32(valores[1]);
                Session["NUMFISICO"] = NumFisico;
            }
        }
    }


}