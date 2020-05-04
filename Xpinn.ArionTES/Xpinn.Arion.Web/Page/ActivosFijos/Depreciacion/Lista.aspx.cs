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
using System.Text;
using System.IO;
using System.Globalization;
using System.Web.UI.HtmlControls;

partial class Lista : GlobalWeb
{
    private Xpinn.ActivosFijos.Services.ActivosFijoservices ActivosFijoservicio = new Xpinn.ActivosFijos.Services.ActivosFijoservices();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(ActivosFijoservicio.CodigoProgramaDepre, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.MostrarGuardar(false);
            ctlMensaje.eventoClick += btnContinuarMen_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ActivosFijoservicio.CodigoProgramaDepre, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                LimpiarValoresConsulta(pConsulta, ActivosFijoservicio.CodigoProgramaDepre);
                btnExportar.Visible = false;
                CargarValoresConsulta(pConsulta, ActivosFijoservicio.CodigoProgramaDepre);
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ActivosFijoservicio.CodigoProgramaDepre, "Page_Load", ex);
        }
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        GuardarValoresConsulta(pConsulta, ActivosFijoservicio.CodigoProgramaDepre);
        Actualizar();
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        ctlMensaje.MostrarMensaje("Desea guardar los datos de la depreciación de activos fijos?");        
    }

    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {

        //consultar cierre historico
        String estado = "";
        DateTime fechacierrehistorico;
        String format = gFormatoFecha;
        DateTime Fecharetiro = DateTime.ParseExact(txtFecha.Text, format, CultureInfo.InvariantCulture);

        Xpinn.ActivosFijos.Entities.ActivoFijo vaActivosFijos = new Xpinn.ActivosFijos.Entities.ActivoFijo();

        vaActivosFijos = ActivosFijoservicio.ConsultarCierreActivosFijos((Usuario)Session["usuario"]);
        estado = vaActivosFijos.estadocierre;
        fechacierrehistorico = Convert.ToDateTime(vaActivosFijos.fecha_cierre.ToString());

        if (estado == "D" && Fecharetiro <= fechacierrehistorico)
        {
            VerError("NO PUEDE INGRESAR DEPRECIACIONES EN PERIODOS YA CERRADOS, TIPO Y,'ACTIVOS FIJOS'");
            return;
        }

        else
        {
        // Cargar listado de depreciación
        string Error = "";
        List<Xpinn.ActivosFijos.Entities.ActivoFijo> lstActivosFijos = new List<Xpinn.ActivosFijos.Entities.ActivoFijo>();
        lstActivosFijos.Clear();
        lstActivosFijos = (List<Xpinn.ActivosFijos.Entities.ActivoFijo>)Session["DTDEPRECIACION"];

        // Aplicando las depreciaciones
        Xpinn.Contabilidad.Services.ComprobanteService ComprobanteServicio = new Xpinn.Contabilidad.Services.ComprobanteService();
        Usuario pUsuario = new Usuario();
        pUsuario = (Usuario)Session["Usuario"];        

        // Valida que exista parametrización contable para la operación        
        List<Xpinn.Contabilidad.Entities.ProcesoContable> LstProcesoContable;
        LstProcesoContable = ComprobanteServicio.ConsultaProceso(0, 23, txtFecha.ToDateTime, pUsuario);
        if (LstProcesoContable.Count() == 0)
        {
            VerError("No existen comprobantes parametrizados para esta operación (Tipo 23=Depreciación)");
            return;
        }
        Int64 pcod_proceso = Convert.ToInt64(LstProcesoContable[0].cod_proceso);
        Int64 pnum_comp = 0;
        Int64 ptipo_comp = 0;        
        Int64 CodOpe = 0;

        // Generar la depreciación
        ActivosFijoservicio.DepreciarActivosFijos(txtFecha.ToDateTime, lstActivosFijos, pcod_proceso, ref pnum_comp, ref ptipo_comp, ref Error, ref CodOpe, (Usuario)Session["Usuario"]);
        if (Error.Trim() == "")
        {                                    
            //Session[ComprobanteServicio.CodigoPrograma + ".cod_ope"] = CodOpe;
            //Session[ComprobanteServicio.CodigoPrograma + ".tipo_ope"] = 23;
            //Session[ComprobanteServicio.CodigoPrograma + ".cod_persona"] = pUsuario.codusuario;
            //Navegar("../../Contabilidad/Comprobante/Nuevo.aspx");
            // Modificar el comprobante            
            Session[ComprobanteServicio.CodigoPrograma + ".num_comp"] = pnum_comp;
            Session[ComprobanteServicio.CodigoPrograma + ".tipo_comp"] = ptipo_comp;            
            Session[ComprobanteServicio.CodigoPrograma + ".modificar"] = "~/Page/ActivosFijos/Depreciacion/Lista.aspx";
            Response.Redirect("~/Page/Contabilidad/Comprobante/Nuevo.aspx", false);
        }
        else
        {
            VerError(Error);
        }


        }

    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        LimpiarValoresConsulta(pConsulta, ActivosFijoservicio.CodigoProgramaDepre);
        List<Xpinn.ActivosFijos.Entities.ActivoFijo> lstConsulta = new List<Xpinn.ActivosFijos.Entities.ActivoFijo>();
        gvLista.DataSource = lstConsulta;
        gvLista.DataBind();
        btnExportar.Visible = false;
        gvLista.Visible = false;
        lblTotalRegs.Visible = false;
    }


    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        String id = gvLista.DataKeys[gvLista.SelectedRow.RowIndex].Value.ToString();
        Session[ActivosFijoservicio.CodigoProgramaDepre + ".id"] = id;
        Navegar(Pagina.Detalle);
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
            BOexcepcion.Throw(ActivosFijoservicio.CodigoProgramaDepre, "gvLista_PageIndexChanging", ex);
        }
    }

    private void Actualizar()
    {
        if (txtFecha.TieneDatos == false)
        {
            VerError("Debe ingresar la fecha de depreciación");
            return;
        }
        try
        {
            DateTime pFechaDepreciacion = new DateTime();
            pFechaDepreciacion = txtFecha.ToDateTime;
            List<Xpinn.ActivosFijos.Entities.ActivoFijo> lstConsulta = new List<Xpinn.ActivosFijos.Entities.ActivoFijo>();
            lstConsulta = ActivosFijoservicio.ListarActivoFijoDepre(pFechaDepreciacion, ObtenerValores(), (Usuario)Session["usuario"]);

            gvLista.PageSize = pageSize;
            gvLista.EmptyDataText = emptyQuery;
            gvLista.DataSource = lstConsulta;

            if (lstConsulta.Count > 0)
            {
                btnExportar.Visible = true;
                gvLista.Visible = true;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                decimal valortotal = (decimal)lstConsulta.Sum(x => x.valor_a_depreciar);
                lblTotalRegs.Text += "  : Total valor a depreciar " + valortotal.ToString("c2");
                Session["DTDEPRECIACION"] = lstConsulta;
                gvLista.DataBind();
                Site toolBar = (Site)this.Master;
                toolBar.MostrarGuardar(true);
            }
            else
            {
                btnExportar.Visible = false;
                gvLista.Visible = false;
                lblTotalRegs.Visible = false;
                Site toolBar = (Site)this.Master;
                toolBar.MostrarGuardar(false);
            }

            Session.Add(ActivosFijoservicio.CodigoProgramaDepre + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ActivosFijoservicio.CodigoProgramaDepre, "Actualizar", ex);
        }
    }

    private Xpinn.ActivosFijos.Entities.ActivoFijo ObtenerValores()
    {
        Xpinn.ActivosFijos.Entities.ActivoFijo vActivoFijo = new Xpinn.ActivosFijos.Entities.ActivoFijo();
        return vActivoFijo;
    }

    protected void btnExportar_Click(object sender, EventArgs e)
    {
        ExportToExcel(gvLista);
    }

    protected void ExportToExcel(GridView GridView1)
    {
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=DepActivosFijos.xls");
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

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }

}