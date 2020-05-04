using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Reflection;
using Xpinn.Util;
using Xpinn.TarjetaDebito.Entities;
using Xpinn.TarjetaDebito.Services;


partial class Lista : GlobalWeb
{
    
    TarjetaService TarjetaService = new TarjetaService();
    Tarjeta entityOficina = new Tarjeta();
    Tarjeta entityTipoCuenta = new Tarjeta();
    Tarjeta entitytarjeta  = new Tarjeta();
    Usuario usuario = new Usuario();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(TarjetaService.CodigoPrograma, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoNuevo += btnNuevo_Click;
            toolBar.eventoExportar += btnExportar_Click;
            ctlMensaje.eventoClick += btnContinuarMen_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(TarjetaService.CodigoPrograma, "Page_PreInit", ex);
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
            BOexcepcion.Throw(TarjetaService.CodigoPrograma, "Page_Load", ex);
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

    protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    {
        Navegar(Pagina.Nuevo);
    }

    void cargarDropdown()
    {
        
        List<Tarjeta> lstConsulta = new List<Tarjeta>();

        ddloficina.DataSource = TarjetaService.ListarOficina(entityOficina, usuario);
        ddloficina.DataTextField = "oficina";
        ddloficina.DataValueField = "cod_oficina";
        ddloficina.DataBind();
        ddloficina.Items.Insert(0, new ListItem("Selecione un item", "0"));
        ddloficina.SelectedIndex = 0;


        ddlTipoCuenta.Items.Insert(0, new ListItem("Seleccione un item", "0"));
        ddlTipoCuenta.Items.Insert(1, new ListItem("Ahorros", "1"));
        ddlTipoCuenta.Items.Insert(2, new ListItem("Credito Rotativo", "2"));
        ddlTipoCuenta.SelectedIndex = 0;
        ddlTipoCuenta.DataBind();
        

        ddlEstado.Items.Insert(0, new ListItem("Seleccione un item","0"));
        ddlEstado.Items.Insert(1, new ListItem("Activo", "1"));
        ddlEstado.Items.Insert(2, new ListItem("Inactivo", "2"));
        ddlEstado.DataBind();
        ddlEstado.SelectedIndex = 0;


        ddlEstadoCupo.Items.Insert(0, new ListItem("Seleccione un item", ""));
        ddlEstadoCupo.Items.Insert(1, new ListItem("Normal", "0"));
        ddlEstadoCupo.Items.Insert(2, new ListItem("Bloqueado", "1"));
        ddlEstadoCupo.DataBind();
        ddlEstadoCupo.SelectedIndex = 0;

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
            BOexcepcion.Throw(TarjetaService.CodigoPrograma, "gvLista_PageIndexChanging", ex);
        }
    }


    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvLista.Rows[e.NewEditIndex].Cells[2].Text;
        String identificacion = gvLista.Rows[e.NewEditIndex].Cells[9].Text;
        String nombres = gvLista.Rows[e.NewEditIndex].Cells[10].Text;
        Session["identificacion"] = identificacion;
        Session["nombres"] = nombres;
        Session[TarjetaService.CodigoPrograma + ".id"] = id;
        Navegar(Pagina.Nuevo);
    }


    protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int id = Convert.ToInt32(gvLista.DataKeys[e.RowIndex].Values[0].ToString());
        Session["ID"] = id;
        ctlMensaje.MostrarMensaje("Desea realizar la eliminación de la  tarjeta");
    }

    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        try
        {
            TarjetaService.EliminarAsignacion(Convert.ToInt32(Session["ID"]), (Usuario)Session["usuario"]);
            Actualizar();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(TarjetaService.CodigoPrograma, "btnContinuarMen_Click", ex);
        }
    }

    
    private void Actualizar()
    {
        try
        {
            List<Tarjeta> lstConsulta = new List<Tarjeta>();
            String filtro = obtFiltro(ObtenerValores());
            
            lstConsulta = TarjetaService.ListarAsignacionTarjetas(filtro,(Usuario)Session["usuario"]);

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
               
            }
            else
            {
                panelGrilla.Visible = false;                
                lblTotalRegs.Visible = false;
            }
            Session["DTCUENTAS"] = lstConsulta;
            Session.Add(TarjetaService.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(TarjetaService.CodigoPrograma, "Actualizar", ex);
        }
    }
      
   
    private string obtFiltro(Tarjeta tarjeta)
    {
        Configuracion conf = new Configuracion();

        String filtro = String.Empty;
        
        if (this.txtNumTarjeta.Text.Trim() != "")
            filtro += " and t.numero_tarjeta =" + tarjeta.numtarjeta;
        if (ddloficina.SelectedIndex != 0)
            filtro += " and t.cod_oficina =" + tarjeta.cod_oficina;
        if (this.txtFechaAsignacion.Text.Trim() != "")
            filtro += " and t.fecha_asignacion= " + tarjeta.fecha_asignacion;
        if (ddlTipoCuenta.SelectedIndex != 0)
            filtro += " and t.tipo_cuenta =" + tarjeta.tipo_cuenta;
        if (this.txtNumCuenta.Text.Trim() != "")
            filtro += " and t.numero_cuenta= " + tarjeta.numero_cuenta;
        if (txtIdentificacion.Text != "")
            filtro += "and p.identificacion LIKE '%" + txtIdentificacion.Text.Trim() + "%'";
        if (txtCodigoNomina.Text != "")
            filtro += "and p.cod_nomina LIKE '%" + txtCodigoNomina.Text.Trim() + "%'";
        if (ddlEstado.SelectedIndex != 0)
            filtro += " and t.estado =" + tarjeta.estado;
        if (ddlEstadoCupo.SelectedIndex != 0)
            filtro += " and t.estado_saldo =" + tarjeta.estado_saldo;

        if (!string.IsNullOrEmpty(filtro))
        {
            if (filtro == "")
            {
                filtro = "";
            }
            else
            {
                filtro = filtro.Substring(4);
                filtro = "where " + filtro;
            }
        }
        return filtro;        
    }

    private Tarjeta ObtenerValores()
    {
        Tarjeta entitytarjeta = new Tarjeta();

        if (!string.IsNullOrEmpty(ddlTipoCuenta.SelectedValue))
            entitytarjeta.tipo_cuenta = ddlTipoCuenta.SelectedValue;

        if (!string.IsNullOrEmpty(ddlEstado.SelectedValue))
            entitytarjeta.estado = ddlEstado.SelectedValue;

        if (!string.IsNullOrEmpty(ddlEstadoCupo.SelectedValue))
            entitytarjeta.estado_saldo = ConvertirStringToInt32(ddlEstadoCupo.SelectedValue);

        if (!string.IsNullOrEmpty(txtNumTarjeta.Text.Trim()))
            entitytarjeta.numtarjeta = Convert.ToString(txtNumTarjeta.Text.Trim());

        if (!string.IsNullOrEmpty(txtFechaAsignacion.Text.Trim()))
            entitytarjeta.fecha_asignacion = Convert.ToDateTime(txtFechaAsignacion.Text.Trim());

        if (!string.IsNullOrEmpty(ddloficina.SelectedValue))
            entitytarjeta.cod_oficina = Convert.ToInt32(ddloficina.SelectedValue);

        if (!string.IsNullOrEmpty(txtNumCuenta.Text.Trim()))
            entitytarjeta.numero_cuenta = Convert.ToString(txtNumCuenta.Text.Trim());

        return entitytarjeta;
    }

    protected void btnExportar_Click(object sender, EventArgs e)
    {
        try
        {
            if (gvLista.Rows.Count > 0 && Session["DTCUENTAS"] != null)
            {
                StringBuilder sb = new StringBuilder();
                StringWriter sw = new StringWriter(sb);
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                Page pagina = new Page();
                dynamic form = new HtmlForm();
                GridView gvExportar = CopiarGridViewParaExportar(gvLista, "DTCUENTAS");
                pagina.EnableEventValidation = false;
                pagina.DesignerInitialize();
                pagina.Controls.Add(form);
                form.Controls.Add(gvExportar);

                for (int i = 0; i < gvExportar.Rows.Count; i++)
                {
                    GridViewRow row = gvExportar.Rows[i];
                    row.Cells[3].Attributes.Add("style", "mso-number-format:\\@");
                    row.Cells[4].Attributes.Add("style", "mso-number-format:\\@");
                    row.Cells[10].Attributes.Add("style", "mso-number-format:\\@");
                    row.Cells[11].Attributes.Add("style", "mso-number-format:\\@");
                }
                pagina.RenderControl(htw);

                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("Content-Disposition", "attachment;filename=Tarjetas.xls");
                Response.Charset = "";
                Response.ContentType = "application/vnd.ms-excel";
                string style = @"<style> .text { mso-number-format:\@; } </style>";
                Response.Write(style);
                Response.Write(sb.ToString());
                Response.End();
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch
        {
        }
    }


}