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
using Xpinn.FabricaCreditos.Entities;
using Xpinn.Aportes.Services;
using System.Text;
using System.IO;

partial class Lista : GlobalWeb
{
    private Xpinn.Aportes.Services.AfiliacionServices AporteServicio = new Xpinn.Aportes.Services.AfiliacionServices();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(AporteServicio.codigoprogramaafiliacion, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoExportar += btnExportar_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoGuardar += btnGuardar_Click;
            ctlMensaje.eventoClick += btnContinuarMen_Click;
            toolBar.MostrarExportar(false);
            toolBar.MostrarGuardar(false);
            mvafiliacion.ActiveViewIndex = 0;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AporteServicio.codigoprogramaafiliacion, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                lblInfo.Text = "";
                lblInfo.Visible = false;
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AporteServicio.codigoprogramaafiliacion, "Page_Load", ex);
        }
    }

    protected bool ValidarProcesoContable(DateTime pFecha, Int64 pTipoOpe)
    {
        // Validar que exista la parametrización contable por procesos
        Xpinn.Contabilidad.Services.ComprobanteService compServicio = new Xpinn.Contabilidad.Services.ComprobanteService();
        List<Xpinn.Contabilidad.Entities.ProcesoContable> lstProceso = new List<Xpinn.Contabilidad.Entities.ProcesoContable>();
        lstProceso = compServicio.ConsultaProceso(0, pTipoOpe, pFecha, (Usuario)Session["Usuario"]);
        if (lstProceso == null)
        {
            return false;
        }
        if (lstProceso.Count <= 0)
        {
            return false;
        }
        return true;
    }

    protected Boolean validar() 
    {
        Boolean validando = true;
        Usuario usuap = new Usuario();
        Xpinn.Tesoreria.Data.OperacionData DATesoreria = new Xpinn.Tesoreria.Data.OperacionData();
        Xpinn.Tesoreria.Entities.Operacion pOperacion = new Xpinn.Tesoreria.Entities.Operacion();
        pOperacion.fecha_oper=DateTime.Now;
        pOperacion.tipo_ope=47;
        
        ///VALIDA EL PROCESO CONTABLE DE LA OPERACION 47
        if (ValidarProcesoContable(pOperacion.fecha_oper, pOperacion.tipo_ope) == true)
        {
            validando = true;
        }
        else
        {
            validando = false;
        }

        return validando;
    }

    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        try
        {
            if (validar() == true)
            {
                Xpinn.Aportes.Services.AfiliacionServices AportesServicio = new Xpinn.Aportes.Services.AfiliacionServices();
                Afiliacion aporte = new Afiliacion();
                Usuario usuap = (Usuario)Session["Usuario"];
                foreach (GridViewRow rfila in gvLista.Rows)
                {
                    CheckBox chk = (CheckBox)rfila.FindControl("chkafilia");
                    if (chk.Checked == true)
                    {

                        ///GUARDA LOS DATOS DE LA AFILIACIÓN

                        aporte.fecha_afiliacion = Convert.ToDateTime(txtfechacorte.Text);
                        aporte.idafiliacion = Convert.ToInt64(rfila.Cells[1].Text);
                        aporte.cod_persona = Convert.ToInt64(rfila.Cells[2].Text);
                        aporte.estado = rfila.Cells[9].Text;
                        if (txtvalortotal.Text != "")
                            aporte.valor = Convert.ToDecimal(txtvalortotal.Text.Replace(".", ""));
                        aporte.causacionafiliacion = 0;
                        aporte.cuotas = Convert.ToInt32(rfila.Cells[7].Text);
                        aporte.asist_ultasamblea = 0;
                        aporte.cod_ope = AportesServicio.crearcausacionafiliacion(aporte, (Usuario)Session["usuario"]);
                        txtcod_persona.Text = rfila.Cells[2].Text;

                        Site toolBar = (Site)this.Master;
                        toolBar.MostrarGuardar(false);
                        toolBar.MostrarExportar(false);
                        VerError("");
                        lblInfo.Visible = false;

                    }
                    else
                    {
                        lblInfo.Text = "No hay datos chequeados";
                        lblInfo.Visible = true;
                    }
                }

                Xpinn.Caja.Entities.TransaccionCaja tranCaja = new Xpinn.Caja.Entities.TransaccionCaja();

                // Se genera el comprobante
                DateTime fecha = Convert.ToDateTime(txtfechacorte.Text);
                Xpinn.Contabilidad.Services.ComprobanteService ComprobanteServicio = new Xpinn.Contabilidad.Services.ComprobanteService();
                Session[ComprobanteServicio.CodigoPrograma + ".cod_ope"] = aporte.cod_ope;
                Session[ComprobanteServicio.CodigoPrograma + ".tipo_ope"] = 47;
                Session[ComprobanteServicio.CodigoPrograma + ".fecha_ope"] = DateTime.Now.ToShortDateString();
                Session[ComprobanteServicio.CodigoPrograma + ".cod_persona"] = txtcod_persona.Text;
                Session[ComprobanteServicio.CodigoPrograma + ".cod_ofi_ope"] = usuap.cod_oficina;
                Session[ComprobanteServicio.CodigoPrograma + ".ventanilla"] = "1";
                Navegar("../../Contabilidad/Comprobante/Nuevo.aspx");
            }
            else 
            {
                VerError("No se puede generar el comprobante contable");
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AporteServicio.codigoprogramaafiliacion, "guardar", ex);
        }
    }
    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        if (txtfechacorte.Text != "")
        {
            ctlMensaje.MostrarMensaje("Desea guardar los datos del Cambio de Afiliación?");
        }
        else 
        {
            VerError("Digite una fecha de corte");
            return;
        }
    }

    protected void btnExportar_Click(object sender, EventArgs e)
    {
        ExportToExcel(gvimpresion);
    }

    protected void ExportToExcel(GridView GridView1)
    {
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=CausacionAfiliacion.xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";
        Response.ContentEncoding = Encoding.Default;
        StringWriter sw = new StringWriter();
        ExpGrilla expGrilla = new ExpGrilla();

        sw = expGrilla.ObtenerGrilla(GridView1, null);

        Response.Write(expGrilla.style);
        Response.Output.Write(sw.ToString());
        Response.Flush();
        Response.End();
    }


    protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    {
        GuardarValoresConsulta(pConsulta, AporteServicio.codigoprogramaafiliacion);
        Navegar(Pagina.Nuevo);
      Session["operacion"] = "N";
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        GuardarValoresConsulta(pConsulta, AporteServicio.codigoprogramaafiliacion);
        Actualizar();
        mvafiliacion.ActiveViewIndex = 0;
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        LimpiarValoresConsulta(pConsulta, AporteServicio.codigoprogramaafiliacion);
        txtfechacorte.ToDateTime = DateTime.Now;
        gvLista.DataBind();
   
    }

    protected void gvLista_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            //ConfirmarEliminarFila(e, "btnBorrar");
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AporteServicio.codigoprogramaafiliacion + "L", "gvLista_RowDataBound", ex);
        }
    }

    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        String id = gvLista.DataKeys[gvLista.SelectedRow.RowIndex].Value.ToString();
        Session[AporteServicio.codigoprogramaafiliacion + ".id"] = id;
      //  Navegar(Pagina.Detalle);
        Response.Redirect("~/Page/Aportes/CuentasAportes/Detalle.aspx");
    }

    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvLista.DataKeys[e.NewEditIndex].Value.ToString();
        Session[AporteServicio.codigoprogramaafiliacion + ".id"] = id;
        Response.Redirect("~/Page/Aportes/CuentasAportes/Nuevo.aspx");
        Session["operacion"] = "";
     
    }

    protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        
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
            BOexcepcion.Throw(AporteServicio.codigoprogramaafiliacion, "gvLista_PageIndexChanging", ex);
        }
    }

    private void ConsultarCliente(String pIdObjeto)
    {
        Xpinn.Aportes.Services.AporteServices AportesServicio = new Xpinn.Aportes.Services.AporteServices();
        Xpinn.Aportes.Entities.Aporte aporte = new Xpinn.Aportes.Entities.Aporte();
        String IdObjeto = txtfechacorte.Text;
        aporte = AportesServicio.ConsultarClienteAporte(IdObjeto,0, (Usuario)Session["usuario"]);

        if (!string.IsNullOrEmpty(aporte.nombre.ToString()))
            txtfechacorte.Text = HttpUtility.HtmlDecode(aporte.nombre);
        

    }

    private void Actualizar()
    {
      
        try
        {
            List<Afiliacion> lstConsulta = new List<Afiliacion>();
            Afiliacion consulta= new Afiliacion();
            if (txtfechacorte.Text != "")
            {
                consulta.fecha_afiliacion = Convert.ToDateTime(txtfechacorte.Text);
            }
            lstConsulta = AporteServicio.listarpersonaafiliacion(consulta, (Usuario)Session["usuario"]);
            Site toolBar = (Site)this.Master;
            gvimpresion.EmptyDataText = emptyQuery;
            gvimpresion.DataSource = lstConsulta;
            gvimpresion.PageSize = pageSize;

            gvLista.PageSize = pageSize;
            gvLista.EmptyDataText = emptyQuery;
            gvLista.DataSource = lstConsulta;
            lblInfo.Visible = false;

            if (lstConsulta.Count > 0)
            {
                txtvalortotal.Visible = true;
                lblvalortotal.Visible = true;
                gvLista.Visible = true;
                gvimpresion.Visible = false;
                Label1.Visible = true;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                gvLista.DataBind();
                gvimpresion.DataBind();
                ValidarPermisosGrilla(gvLista);
                toolBar.MostrarExportar(true);
                toolBar.MostrarGuardar(true);
            }
            else
            {
                lblvalortotal.Visible = false;
                txtvalortotal.Visible = false;
                gvimpresion.Visible = false;
                Label1.Visible = false;
                gvLista.Visible = false;
                lblTotalRegs.Visible = false;
            }
            Int64 valorcausado = 0;
            foreach (GridViewRow rfila in gvLista.Rows)
            {
                CheckBox chk = (CheckBox)rfila.FindControl("chkafilia");
                if (chk.Checked == true)
                {

                    valorcausado += Convert.ToInt64(rfila.Cells[6].Text.Replace(".", "").Replace(",", ""));
                    txtvalortotal.Text = Convert.ToString(valorcausado);
                }
                else
                {
                    txtvalortotal.Text = "0";
                }
            }

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AporteServicio.CodigoPrograma, "Actualizar", ex);
        }
        
    }

    protected void checkeado_oncheckedchanged(object sender, EventArgs e)
    {
        
        Int64 valorcausado = 0;
        foreach (GridViewRow rfila in gvLista.Rows)
        {
            CheckBox chk = (CheckBox)rfila.FindControl("chkafilia");
            if (chk.Checked == true)
            {
                txtvalortotal.Visible = true;

                if (!string.IsNullOrWhiteSpace(rfila.Cells[6].Text) && rfila.Cells[6].Text != "&nbsp;")
                {
                    valorcausado += Convert.ToInt64(rfila.Cells[6].Text.Replace(".", "").Replace(",", ""));
                }
                
                txtvalortotal.Text = Convert.ToString(valorcausado);
                Session["valores"] = txtvalortotal.Text.Replace(".", "");
                txtvalortotal.Text = "0";
            }
            else if (chk.Checked == false)
            {
                txtvalortotal.Visible = true;
                txtvalortotal.Text = "0";
                Session["valor"] = txtvalortotal.Text.Replace(".","");
                txtvalortotal.Text = "0";
            
            }
        }

        if (Session["valores"] != null)
        {
            txtvalortotal.Text = Session["valores"].ToString();
            Session["valores"] = null;
        }
        else 
        {
            txtvalortotal.Text = "0";
            Session["valores"] = null;
        
        }



    }

    protected void btnInfo_Click(object sender, ImageClickEventArgs e)
    {

    }

   

    protected void DdlOrdenadorpor_SelectedIndexChanged(object sender, EventArgs e)
    {
        Actualizar();
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }
}