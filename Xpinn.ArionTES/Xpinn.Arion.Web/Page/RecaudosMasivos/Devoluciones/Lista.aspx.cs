using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web;
using System.Text;
using System.IO;
using System.Globalization;
using System.Web.UI.HtmlControls;
using Microsoft.Reporting.WebForms;
using Xpinn.Util;
using Xpinn.Tesoreria.Entities;
using Xpinn.Tesoreria.Services;
using System.Linq;

partial class Lista : GlobalWeb
{
    PoblarListas poblar = new PoblarListas();
    DevolucionServices SoliServicios = new DevolucionServices();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(SoliServicios.CodigoPrograma, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoNuevo += btnNuevo_Click;
            ctlMensaje.eventoClick += btnContinuarMen_Click;
            toolBar.eventoExportar += btnExportar_Click;
            toolBar.MostrarExportar(false);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(SoliServicios.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                CargarDropdown();
                if (Session[SoliServicios.CodigoPrograma + ".consulta"] != null)
                    Actualizar();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(SoliServicios.CodigoPrograma, "Page_Load", ex);
        }
    }


    protected void btnExportar_Click(object sender, EventArgs e)
    {

        try
        {
            if (gvLista.Rows.Count > 0 && Session["DTDEVOLUCIONES"] != null)
            {
                StringBuilder sb = new StringBuilder();
                StringWriter sw = new StringWriter(sb);
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                Page pagina = new Page();
                dynamic form = new HtmlForm();

                List<Devolucion> lista = (List<Devolucion>)Session["DTDEVOLUCIONES"];
                List<Devolucion> listaFiltrada = lista;

                if (!chkExportarSaldosEnCero.Checked)
                {
                    listaFiltrada = lista.Where(x => x.saldo != 0).ToList();
                }

                gvLista.DataSource = listaFiltrada;
                gvLista.AllowPaging = false;
                gvLista.DataBind();

                if (chkExportarCSV.Checked)
                {
                    ExportarGridCSVDirecto(gvLista, "Devoluciones");
                }
                else
                {
                    pagina.EnableEventValidation = false;
                    pagina.DesignerInitialize();
                    pagina.Controls.Add(form);
                    form.Controls.Add(gvLista);
                    pagina.RenderControl(htw);
                    Response.Clear();
                    Response.Buffer = true;
                    Response.ContentType = "application/vnd.ms-excel";
                    Response.AddHeader("Content-Disposition", "attachment;filename=ConsultaDevoluciones.xls");
                    Response.Charset = "UTF-8";
                    Response.ContentEncoding = Encoding.Default;
                    Response.Write(sb.ToString());
                    Response.End();
                }

                gvLista.DataSource = lista;
                gvLista.AllowPaging = true;
                gvLista.DataBind();
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

    public override void VerifyRenderingInServerForm(Control control)
    {
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {

        Page.Validate();
        gvLista.Visible = true;
        if (Page.IsValid)
        {
            Actualizar();
            Session.Remove(SoliServicios.CodigoPrograma + ".consulta");
        }
    }


    private void CargarDropdown()
    {
        ddlEstado.Items.Insert(0, new ListItem("Seleccione un item", "0"));
        ddlEstado.Items.Insert(1, new ListItem("PENDIENTE", "1"));
        ddlEstado.Items.Insert(2, new ListItem("PAGADA", "2"));
        ddlEstado.Items.Insert(3, new ListItem("ANULADA", "3"));
        ddlEstado.SelectedIndex = 0;
        ddlEstado.DataBind();

        Xpinn.FabricaCreditos.Services.OficinaService oficinaService = new Xpinn.FabricaCreditos.Services.OficinaService();
        Xpinn.FabricaCreditos.Entities.Oficina oficina = new Xpinn.FabricaCreditos.Entities.Oficina();

        Usuario usuap = (Usuario)Session["usuario"];

        int cod = Convert.ToInt32(usuap.codusuario);
        int consulta = oficinaService.UsuarioPuedeConsultarCreditosOficinas(cod, (Usuario)Session["Usuario"]);
        if (consulta >= 1)
        {
            ddloficina.DataSource = oficinaService.ListarOficinas(oficina, (Usuario)Session["usuario"]);
            
            ddloficina.DataTextField = "nombre";
            ddloficina.DataValueField = "codigo";
            ddloficina.DataBind();
            ddloficina.SelectedValue = Convert.ToString(usuap.cod_oficina);
            ddloficina.Enabled = true;
            ddloficina.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));
        }
        else
        {           
            ddloficina.Items.Insert(0, new ListItem(Convert.ToString(usuap.nombre_oficina), Convert.ToString(usuap.cod_oficina)));
            ddloficina.DataBind();
            ddloficina.SelectedValue = Convert.ToString(usuap.cod_oficina);
            ddloficina.Enabled = false;
            ddloficina.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));
        }
        
    }

    protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    {
        if (Session[SoliServicios.CodigoPrograma + ".id"] != null)
        {
            Session.Remove(SoliServicios.CodigoPrograma + ".id");
        }
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
            BOexcepcion.Throw(SoliServicios.CodigoPrograma, "gvLista_PageIndexChanging", ex);
        }
    }


    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvLista.Rows[e.NewEditIndex].Cells[2].Text;
        string estado = gvLista.DataKeys[e.NewEditIndex].Values[1].ToString();
        Session[SoliServicios.CodigoPrograma + ".id"] = id;
        if (estado == "PENDIENTE")
        {
            Navegar(Pagina.Nuevo);
        }
        else
        {
            gvLista.EditIndex = -1;
            VerError("No puede editar devoluciones que no esten en estado PENDIENTE");
            Actualizar();
        }
    }


    protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        // Validar que exista la parametrización contable por procesos
        if (ValidarProcesoContable(DateTime.Now, 104) == false)
        {
            VerError("No se encontró parametrización contable por procesos para el tipo de operación 104=Anulación de Devoluciones");
            return;
        }
        int id = Convert.ToInt32(gvLista.DataKeys[e.RowIndex].Values[0].ToString());
        Session["ID"] = id;
        decimal valor = Convert.ToDecimal(gvLista.Rows[e.RowIndex].Cells[10].Text.ToString().Replace("$", "").Replace(gSeparadorMiles, "").Replace(",",""));
        Session["VALOR"] = valor;
        Int64 cod_persona = Convert.ToInt64(gvLista.Rows[e.RowIndex].Cells[3].Text.ToString());
        Session["COD_PERSONA"] = cod_persona;
        String estado = gvLista.Rows[e.RowIndex].Cells[13].Text.ToString();
        if (estado != "PENDIENTE")
        {
            VerError("No puede anular operaciones en estado " + estado);
            return;
        }
        ctlMensaje.MostrarMensaje("Desea eliminar la Devolución?");
    }

    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        try
        {
            Usuario pUsu = new Usuario();
            pUsu = (Usuario)Session["usuario"];
            Int64 codOpe = 0;
            codOpe = SoliServicios.EliminarDevolucion(Convert.ToInt32(Session["ID"]), Convert.ToInt32(Session["VALOR"]), pUsu);

            // Generar el comprobante
            if (codOpe != 0)
            {
                Xpinn.Contabilidad.Services.ComprobanteService ComprobanteServicio = new Xpinn.Contabilidad.Services.ComprobanteService();
                Session[ComprobanteServicio.CodigoPrograma + ".cod_ope"] = codOpe;
                Session[ComprobanteServicio.CodigoPrograma + ".tipo_ope"] = 104;
                Session[ComprobanteServicio.CodigoPrograma + ".fecha_ope"] = DateTime.Now;
                Session[ComprobanteServicio.CodigoPrograma + ".cod_ofi_ope"] = pUsu.cod_oficina;
                Session[ComprobanteServicio.CodigoPrograma + ".cod_persona"] = Session["COD_PERSONA"];
                Navegar("../../Contabilidad/Comprobante/Nuevo.aspx");
            }

            // Actualizar listado
            Actualizar();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(SoliServicios.CodigoPrograma, "btnContinuarMen_Click", ex);
        }
    }

    
    private void Actualizar()
    {
        try
        {
            Site toolBar = (Site)this.Master;
            List<Devolucion> lstConsulta = new List<Devolucion>();
            String filtro = obtFiltro(ObtenerValores());
            DateTime pFecha;
            pFecha = txtFechaDev.ToDateTime == null ? DateTime.MinValue : txtFechaDev.ToDateTime;
            
            lstConsulta = SoliServicios.ListarDevolucion(ObtenerValores(),pFecha,(Usuario)Session["usuario"], filtro);

            gvLista.PageSize = 15;
            String emptyQuery = "Fila de datos vacia";
            gvLista.EmptyDataText = emptyQuery;

            if (lstConsulta.Count > 0)
            {
                toolBar.MostrarExportar(true);
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
            Session["DTDEVOLUCIONES"] = lstConsulta;
            Session.Add(SoliServicios.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(SoliServicios.CodigoPrograma, "Actualizar", ex);
        }
    }

    private Devolucion ObtenerValores()
    {
        Devolucion vCuentas = new Devolucion();
        if (txtNumServ.Text.Trim() != "")
            vCuentas.num_devolucion = Convert.ToInt32(txtNumServ.Text.Trim());   
        if (txtConcepto.Text != "")
            vCuentas.concepto = txtConcepto.Text;        
        if (txtIdentificacion.Text.Trim() != "")
            vCuentas.identificacion = txtIdentificacion.Text;
        if (txtNumRec.Text.Trim() != "")
            vCuentas.num_recaudo = Convert.ToInt32(txtNumRec.Text);
        if (ddlEstado.SelectedIndex != 0)
            vCuentas.estado = ddlEstado.SelectedValue;
        return vCuentas;
    }



    private string obtFiltro(Devolucion Devol)
    {
        
        String filtro = String.Empty;

        if (txtNumServ.Text.Trim() != "")
            filtro += " and d.num_devolucion = " + Devol.num_devolucion;       
        if (txtConcepto.Text != "")
            filtro += " and d.concepto like '%" + Devol.concepto.ToUpper() + "%'";
        if (txtIdentificacion.Text.Trim() != "")
            filtro += " and d.identificacion like '%"+ Devol.identificacion+"%'";
        if (txtNumRec.Text.Trim() != "")
            filtro += " and d.num_recaudo = " + Devol.num_recaudo ;
        if (ddlEstado.SelectedIndex != 0)
            filtro += " and Trim(d.estado) = '" + Devol.estado +"'";
        if (ddloficina.SelectedIndex != 0)
            filtro += " and p.cod_oficina= " + ddloficina.SelectedValue + "";
        if (txtCodigoNomina.Text != "")
            filtro += " and cod_nomina like '%" + txtCodigoNomina.Text + "%'";

        return filtro;
    }

 }