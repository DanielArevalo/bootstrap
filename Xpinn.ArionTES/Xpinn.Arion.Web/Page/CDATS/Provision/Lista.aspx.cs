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
    AnulacionCDATService AnulaService = new AnulacionCDATService();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(AnulaService.CodigoProgramaCaus, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoExportar += btnExportar_Click;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
            ctlMensaje.eventoClick += btnContinuarMen_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
            toolBar.MostrarExportar(false);
            toolBar.MostrarGuardar(false);                   
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AnulaService.CodigoProgramaCaus, "Page_PreInit", ex);
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                Site toolBar = (Site)this.Master;
                txtFecha.ToDateTime = DateTime.Now;
                LlenarCombos();           
                Convert.ToDateTime(txtFecha.ToDateTime);
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AnulaService.CodigoProgramaCaus, "Page_Load", ex);
        }
    }
    protected void LlenarCombos()
    {
        // Llenar el DDL de la fecha de corte 
        Configuracion conf = new Configuracion();
        List<Xpinn.Comun.Entities.Cierea> lstFechaCierre = new List<Xpinn.Comun.Entities.Cierea>();
        lstFechaCierre = ApertuService.ListarFechaCierreCausacion((Usuario)Session["Usuario"]);
        ddlFechaCorte.DataSource = lstFechaCierre;
        ddlFechaCorte.DataTextFormatString = "{0:" + conf.ObtenerFormatoFecha() + "}";
        ddlFechaCorte.DataTextField = "fecha";
        ddlFechaCorte.DataBind();
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

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        gvLista.Visible = false;
        lblTotalRegs.Text = ("");
       
    }


    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        ///verifica que todo este llenado        
        ctlMensaje.MostrarMensaje("Desea guardar los datos de la provisión?");
    }

    private List<AdministracionCDAT> ObtenerListaGrilla()
    {
        List<AdministracionCDAT> lstLista = new List<AdministracionCDAT>();

        foreach (GridViewRow rFila in gvLista.Rows)
        {
            AdministracionCDAT vData = new AdministracionCDAT();
            if (rFila.Cells[1].Text != "" && rFila.Cells[1].Text != "&nbsp;")//CODIGO
                vData.codigo_cdat = Convert.ToInt64(rFila.Cells[1].Text);
            else
                vData.codigo_cdat = 0;

            if (rFila.Cells[2].Text != "" && rFila.Cells[2].Text != "&nbsp;")//FECHA INICIAL
                vData.fecha_inicio = Convert.ToDateTime(rFila.Cells[2].Text);

            if (rFila.Cells[3].Text != "" && rFila.Cells[3].Text != "&nbsp;")//FECHA FINAL
                vData.fecha_vencimiento = Convert.ToDateTime(rFila.Cells[3].Text);

            if (rFila.Cells[4].Text != "" && rFila.Cells[4].Text != "&nbsp;")//IDENTIFICACION
                vData.identificacion = rFila.Cells[4].Text;

            if (rFila.Cells[5].Text != "" && rFila.Cells[5].Text != "&nbsp;")//NOMBRE TITULAR
                vData.nombres = rFila.Cells[5].Text;

            if (rFila.Cells[6].Text != "" && rFila.Cells[6].Text != "&nbsp;")//VALOR
                vData.valor = Convert.ToDecimal(rFila.Cells[6].Text.ToString().Replace(".",""));
   
            if (rFila.Cells[7].Text != "" && rFila.Cells[7].Text != "&nbsp;")//TASA
                vData.tasa_interes = Convert.ToDecimal(rFila.Cells[7].Text.ToString().Replace(".", ""));

            if (rFila.Cells[8].Text != "" && rFila.Cells[8].Text != "&nbsp;")//FECHA INTERES
                vData.fecha_intereses = Convert.ToDateTime(rFila.Cells[8].Text);
            else
                vData.fecha_intereses = DateTime.MinValue;

            if (rFila.Cells[9].Text != "" && rFila.Cells[9].Text != "&nbsp;")//dias
                vData.dias = Convert.ToInt32(rFila.Cells[9].Text);
            else
                vData.dias = 0;

             if (rFila.Cells[10].Text != "" && rFila.Cells[10].Text != "&nbsp;")//INTERES
                 vData.intereses_cap = Convert.ToInt32(rFila.Cells[10].Text.ToString().Replace(".", ""));
            else
                 vData.intereses_cap = 0;

            if (rFila.Cells[11].Text != "" && rFila.Cells[11].Text != "&nbsp;")//RETENCION
                vData.retencion = Convert.ToDecimal(rFila.Cells[11].Text.ToString().Replace(".",""));
            else
                vData.retencion = 0;

            if (vData.numero_cdat != "" && vData.numero_cdat != null && vData.codigo_cdat != 0)
            {
                lstLista.Add(vData);
            }
        }
        return lstLista;
    }


    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        Xpinn.Tesoreria.Services.OperacionServices xTesoreria = new Xpinn.Tesoreria.Services.OperacionServices();
        Xpinn.Tesoreria.Entities.Operacion vOpe = new Xpinn.Tesoreria.Entities.Operacion();
          
        //GRABACION DE LA OPERACION           
        //vOpe.cod_ope = 0;
        vOpe.tipo_ope = 13;
        vOpe.cod_caja = 0;
        vOpe.cod_cajero = 0;
        vOpe.observacion = null;
        vOpe.cod_proceso = null;
        vOpe.fecha_oper = DateTime.Now;
        vOpe.fecha_calc = DateTime.Now;
        xTesoreria.GrabarOperacion(vOpe, (Usuario)Session["usuario"]);
              
        try
        {
            AdministracionCDAT pLiqui = new AdministracionCDAT();
            Usuario vUsuario = new Usuario();
            vUsuario = (Usuario)Session["Usuario"];
            List <AdministracionCDAT>lstLista = new List<AdministracionCDAT>();
            lstLista = ObtenerListaGrilla();
            AdministracionCDAT vprovision = new AdministracionCDAT();
            vprovision.cod_ope = vOpe.cod_ope;
            foreach (GridViewRow rFila in gvLista.Rows)
            {
                vprovision.cod_persona = Convert.ToInt64(rFila.Cells[0].Text);
                vprovision.fecha_apertura = Convert.ToDateTime(rFila.Cells[2].Text);
                vOpe.cod_ope = 0;
                vprovision.codigo_cdat = Convert.ToInt64(rFila.Cells[1].Text);
                vprovision.numero_cdat = Convert.ToString(rFila.Cells[0].Text);
                vprovision.tasa_interes = Convert.ToDecimal(rFila.Cells[7].Text.ToString().Replace(",","."));
                vprovision.fecha_intereses = Convert.ToDateTime(rFila.Cells[8].Text);
                vprovision.dias = Convert.ToInt32(rFila.Cells[9].Text);
                vprovision.intereses_cap = Convert.ToInt32(rFila.Cells[10].Text);
                vprovision.retencion = Convert.ToInt32(rFila.Cells[11].Text);
                vprovision.capitalizar_int = Convert.ToInt32(rFila.Cells[12].Text);
                vprovision.valor = Convert.ToDecimal(rFila.Cells[6].Text);

                lstLista = AdmService.guardargrilla(vprovision, lstLista, (Usuario)Session["usuario"]);
             
            }
            ///inicializo la operacion CIEREA
            ///inicializo la operacion CIEREA
            Xpinn.Comun.Entities.Cierea pcierea = new Xpinn.Comun.Entities.Cierea();
            pcierea.campo1 = " ";
            pcierea.tipo = "K";
            pcierea.estado = "D";
            pcierea.campo2 = " ";
            pcierea.fecha = Convert.ToDateTime(this.ddlFechaCorte.SelectedValue);
            pcierea.codusuario = vUsuario.codusuario;

            ApertuService.Crearcierea(pcierea, (Usuario)Session["usuario"]);    
           
            if (vOpe.cod_ope != Int64.MinValue)
            {
                 var usu = (Usuario)Session["usuario"];
                Xpinn.Contabilidad.Services.ComprobanteService ComprobanteServicio = new Xpinn.Contabilidad.Services.ComprobanteService();
                Session[ComprobanteServicio.CodigoPrograma + ".cod_ope"] = vOpe.cod_ope;
                Session[ComprobanteServicio.CodigoPrograma + ".tipo_ope"] = 13;
                Session[ComprobanteServicio.CodigoPrograma + ".fecha_ope"] = Convert.ToDateTime(this.ddlFechaCorte.SelectedValue);
                Session[ComprobanteServicio.CodigoPrograma + ".cod_persona"] = usu.cod_persona; //"<Colocar Aquí el código de la persona del servicio>"
                Navegar("../../Contabilidad/Comprobante/Nuevo.aspx");

                Session[AnulaService.CodigoProgramaANU + ".id"] = idObjeto;
            }

            Site toolBar = (Site)Master;     
            toolBar.MostrarExportar(false);
            toolBar.MostrarGuardar(false);
           
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AdmService.CodigoProgramaListarCDAT, "btnContinuarMen_Click", ex);
        }
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
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
                Response.AddHeader("Content-Disposition", "attachment;filename=DatosCDATS.xls");
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
            BOexcepcion.Throw(AnulaService.CodigoProgramaCaus, "gvLista_PageIndexChanging", ex);
        }
    }

    
    private void Actualizar()
    {
        try
        {
            List<AdministracionCDAT> lstConsulta = new List<AdministracionCDAT>();
         
            DateTime FechaApe;
            Site toolBar = (Site)this.Master;
           // FechaApe = txtFecha.ToDateTime == null ? DateTime.MinValue : txtFecha.ToDateTime;
           FechaApe = Convert.ToDateTime(this.ddlFechaCorte.SelectedValue);

            lstConsulta = AdmService.ListarIntereses(FechaApe, (Usuario)Session["usuario"]);

            gvLista.PageSize = 15;
            String emptyQuery = "Fila de datos vacia";
            gvLista.EmptyDataText = emptyQuery;           

            if (lstConsulta.Count > 0)
            {
                gvLista.DataSource = lstConsulta;
                gvLista.DataBind();
                panelGrilla.Visible = true;                
                lblTotalRegs.Visible = true;
                toolBar.MostrarGuardar(true);
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                Session["DTCDAT"] = lstConsulta;
            }
            else
            {
                Session["DTCDAT"] = null;
                panelGrilla.Visible = false;                
                lblTotalRegs.Visible = false;
            }
            Session.Add(AnulaService.CodigoProgramaCaus + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AnulaService.CodigoProgramaCaus, "Actualizar", ex);
        }
    }





}