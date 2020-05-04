using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Contabilidad.Entities;
using Xpinn.Util;

partial class Nuevo : GlobalWeb
{
    private Xpinn.Contabilidad.Services.TipoImpuestoService TipoImpuestoServicio = new Xpinn.Contabilidad.Services.TipoImpuestoService();
    private Xpinn.Contabilidad.Services.ReporteImpuestosService reporteImp = new Xpinn.Contabilidad.Services.ReporteImpuestosService();
    private ReporteImpuestos pReport = new ReporteImpuestos();
    private List<ReporteImpuestos> lstDatos = new List<ReporteImpuestos>();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[TipoImpuestoServicio.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(TipoImpuestoServicio.CodigoPrograma, "E");
            else
                VisualizarOpciones(TipoImpuestoServicio.CodigoPrograma, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(TipoImpuestoServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Session[TipoImpuestoServicio.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[TipoImpuestoServicio.CodigoPrograma + ".id"].ToString();
                    Session.Remove(TipoImpuestoServicio.CodigoPrograma + ".id");
                    ObtenerDatos(idObjeto);
                    txtCodigo.Enabled = false;
                }
                ListarImpuestos(idObjeto);
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(TipoImpuestoServicio.CodigoPrograma, "Page_Load", ex);
        }
    }

    void ListarImpuestos(string sp)
    {
        string codigo = "";
        lstDatos = reporteImp.ListarImpuestosCombo(pReport, (Usuario)Session["usuario"]);
        Session["Impuestos"] = lstDatos;
        gvActEmpresa.DataSource = lstDatos.Where(x => Convert.ToInt32(x.principal) != 0);
        gvActEmpresa.DataBind();
        if (sp != "")
        {
            codigo = lstDatos.FirstOrDefault(x => x.cod_tipo_impuesto == Convert.ToInt32(sp)).depende_de;
            foreach (GridViewRow row in gvActEmpresa.Rows)
            {
              if (((Label)row.FindControl("lbl_Codigo")).Text == codigo)
                {
                    CheckBox impuesto = (CheckBox) row.FindControl("chkSecundario");
                    impuesto.Checked = true;
                }
                
            }
        }
       }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            Xpinn.Contabilidad.Entities.TipoImpuesto vTipoImpuesto = new Xpinn.Contabilidad.Entities.TipoImpuesto();

            if (idObjeto != "")
            {
                vTipoImpuesto = TipoImpuestoServicio.ConsultarTipoImpuesto(Convert.ToInt64(idObjeto), (Usuario)Session["usuario"]);
            }
            
            if (txtCodigo.Text.Trim() != "")
                vTipoImpuesto.cod_tipo_impuesto = Convert.ToInt32(txtCodigo.Text.Trim());
            vTipoImpuesto.nombre_impuesto = Convert.ToString(txtNombre.Text.Trim());
            vTipoImpuesto.descripcion = Convert.ToString(txtDescripcion.Text.Trim());
            if (chkPrincipal.Checked)
            {
                vTipoImpuesto.principal = 1;
                vTipoImpuesto.depende_de = 0;
            }
            else
            {
                foreach (GridViewRow row in gvActEmpresa.Rows)
                {
                    vTipoImpuesto.principal = 0;
                    if (((CheckBox)row.FindControl("chkSecundario")).Checked)
                    {
                        if (vTipoImpuesto.depende_de == 0)
                        {
                            vTipoImpuesto.depende_de = Convert.ToInt32(((Label)row.FindControl("lbl_Codigo")).Text);
                        }
                    }
                }
            }

            if (idObjeto != "")
            {
                vTipoImpuesto.cod_tipo_impuesto = Convert.ToInt32(idObjeto);
                TipoImpuestoServicio.ModificarTipoImpuesto(vTipoImpuesto, (Usuario)Session["usuario"]);
            }
            else
            {
                vTipoImpuesto = TipoImpuestoServicio.CrearTipoImpuesto(vTipoImpuesto, (Usuario)Session["usuario"]);
                idObjeto = vTipoImpuesto.cod_tipo_impuesto.ToString();
            }

            Session[TipoImpuestoServicio.CodigoPrograma + ".id"] = idObjeto;
            Navegar(Pagina.Lista);
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(TipoImpuestoServicio.CodigoPrograma, "btnGuardar_Click", ex);
        }
    }

    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        if (idObjeto == "")
        {
            Navegar(Pagina.Lista);
        }
        else
        {
            Session[TipoImpuestoServicio.CodigoPrograma + ".id"] = idObjeto;
            Navegar(Pagina.Detalle);
        }
    }

    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {

            TipoImpuesto vTipoImpuesto = new TipoImpuesto();
            vTipoImpuesto = TipoImpuestoServicio.ConsultarTipoImpuesto(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);

            txtCodigo.Text = HttpUtility.HtmlDecode(vTipoImpuesto.cod_tipo_impuesto.ToString().Trim());
            if (!string.IsNullOrEmpty(vTipoImpuesto.nombre_impuesto))
                txtNombre.Text = HttpUtility.HtmlDecode(vTipoImpuesto.nombre_impuesto.Trim());
            if (!string.IsNullOrEmpty(vTipoImpuesto.descripcion))
                txtDescripcion.Text = HttpUtility.HtmlDecode(vTipoImpuesto.descripcion.Trim());



        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(TipoImpuestoServicio.CodigoPrograma, "ObtenerDatos", ex);
        }
    }


}