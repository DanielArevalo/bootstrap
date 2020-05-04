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
using Xpinn.Contabilidad.Services;
using Xpinn.Contabilidad.Entities;
using System.Text;
using System.IO;
using System.Web.UI.HtmlControls;

partial class Lista : GlobalWeb
{
    ParametrosCtasNominaService _parametrosCuentaNomina = new ParametrosCtasNominaService();    

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(_parametrosCuentaNomina.CodigoPrograma, "L");

            Site toolBar = (Site)Master;
            toolBar.eventoNuevo += btnNuevo_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
            toolBar.eventoExportar += btnExportar_Click;
            ctlMensaje.eventoClick += btnContinuarMen_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_parametrosCuentaNomina.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                CargarListas();
                CargarValoresConsulta(pConsulta, _parametrosCuentaNomina.CodigoPrograma);
                Actualizar();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_parametrosCuentaNomina.CodigoPrograma, "Page_Load", ex);
        }
    }


    private void CargarListas()
    {
        try
        {
            List<Xpinn.Comun.Entities.ListaDesplegable> plista = new List<Xpinn.Comun.Entities.ListaDesplegable>();
            Xpinn.Comun.Entities.ListaDesplegable pentidad = new Xpinn.Comun.Entities.ListaDesplegable();
            Xpinn.Comun.Services.ListaDeplegableService pservicio = new Xpinn.Comun.Services.ListaDeplegableService();

            ctlListarCodigoTransaccion.LimpiarControl();
            plista = pservicio.ListarListaDesplegable(pentidad, "TIPO_TRAN", " ", " ", " ", (Usuario)Session["usuario"]);

            ctlListarCodigoTransaccion.TextField = "descripcion";
            ctlListarCodigoTransaccion.ValueField = "idconsecutivo";
            ctlListarCodigoTransaccion.BindearControl(plista);
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }


    protected void txtCodCuenta_TextChanged(object sender, EventArgs e)
    {
        if (txtCodCuenta.Text != "")
        {
            // Determinar los datos de la cuenta contable
            PlanCuentasService PlanCuentasServicio = new PlanCuentasService();
            PlanCuentas PlanCuentas = PlanCuentasServicio.ConsultarPlanCuentas(txtCodCuenta.Text, Usuario);

            // Mostrar el nombre de la cuenta            
            if (txtNomCuenta != null)
                txtNomCuenta.Text = PlanCuentas.nombre;
        }
        else
        {
            txtNomCuenta.Text = "";
        }
    }


    protected void btnListadoPlan_Click(object sender, EventArgs e)
    {
        ctlListadoPlan.Motrar(true, "txtCodCuenta", "txtNomCuenta");
    }


    private string obtFiltro()
    {
        string filtro = "";

        if (txtCodigo.Text != "")
            filtro += " and p.IDPARAMETRO = " + txtCodigo.Text;
        if (txtCodCuenta.Text != "")
            filtro += " and p.COD_CUENTA = '" + txtCodCuenta.Text + "'";
//        if (!string.IsNullOrWhiteSpace(ctlListarCodigoTransaccion.Codigo))
  //              filtro += " and p.TIPO_TRAN = " + ctlListarCodigoTransaccion.Codigo;

        return filtro;
    }


    private void Actualizar()
    {
        try
        {
            string filtro = "";
            filtro = obtFiltro();

            List<Par_Cue_Nomina> lstConsulta = _parametrosCuentaNomina.ListarPar_Cue_Nomina(filtro, (Usuario)Session["usuario"]);

            gvLista.PageSize = pageSize;
            gvLista.EmptyDataText = emptyQuery;
            gvLista.DataSource = lstConsulta;

            if (lstConsulta.Count > 0)
            {
                gvLista.Visible = true;               
                gvLista.DataBind();
                Session["DTPARAMETROS"] = lstConsulta;
            }
            else
            {
                gvLista.Visible = false;
                Session["DTPARAMETROS"] = null;
            }
            lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();

            Session.Add(_parametrosCuentaNomina.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_parametrosCuentaNomina.CodigoPrograma, "Actualizar", ex);
        }
    }


    protected void btnExportar_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (gvLista.Rows.Count > 0 && Session["DTPARAMETROS"] != null)
            {
                StringBuilder sb = new StringBuilder();
                StringWriter sw = new StringWriter(sb);
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                Page pagina = new Page();
                dynamic form = new HtmlForm();
                gvLista.AllowPaging = false;
                gvLista.Columns[0].Visible = false;
                gvLista.Columns[1].Visible = false;
                gvLista.DataSource = Session["DTPARAMETROS"];
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
                Response.AddHeader("Content-Disposition", "attachment;filename=ParamentrosCtasOtros.xls");
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
        catch
        {
        }
    }


    protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    {       
        Navegar(Pagina.Nuevo);
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        GuardarValoresConsulta(pConsulta, _parametrosCuentaNomina.CodigoPrograma);
        Actualizar();
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        ctlListarCodigoTransaccion.LimpiarControl();
        LimpiarValoresConsulta(pConsulta, _parametrosCuentaNomina.CodigoPrograma);
    }   

    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvLista.Rows[e.NewEditIndex].Cells[2].Text;
        Session[_parametrosCuentaNomina.CodigoPrograma + ".id"] = id;
        Navegar(Pagina.Nuevo);
    }

    protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            Int64 id = Convert.ToInt64(gvLista.DataKeys[e.RowIndex].Values[0].ToString());
            Session["ID"] = id;
            ctlMensaje.MostrarMensaje("Desea realizar la Eliminación del Registro Seleccionado?");            
        }
        catch (Xpinn.Util.ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }        
    }

    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        try
        {
            _parametrosCuentaNomina.EliminarPar_Cue_Nomina(Convert.ToInt32(Session["ID"].ToString()), (Usuario)Session["usuario"]);
            Actualizar();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_parametrosCuentaNomina.CodigoPrograma, "btnContinuarMen_Click", ex);
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
            BOexcepcion.Throw(_parametrosCuentaNomina.CodigoPrograma, "gvLista_PageIndexChanging", ex);
        }
    }   
}