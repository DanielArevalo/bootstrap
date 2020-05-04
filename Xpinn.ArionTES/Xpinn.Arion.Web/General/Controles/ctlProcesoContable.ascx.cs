using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using Xpinn.Contabilidad.Services;
using Xpinn.Contabilidad.Entities;

public delegate void imgAceptarProceso_ActionsDelegate(object sender, EventArgs e);
public delegate void btnCancelar_ActionsDelegate(object sender, EventArgs e);

public partial class ctlProcesoContable : System.Web.UI.UserControl
{
    public event imgAceptarProceso_ActionsDelegate eventoClick;
    public event btnCancelar_ActionsDelegate eventoCancelarClick;

    private Xpinn.Contabilidad.Services.ComprobanteService ComprobanteServicio = new Xpinn.Contabilidad.Services.ComprobanteService();

    protected void Page_Load(object sender, EventArgs e)
    {
        NoGeneraComprobante = false;
    }

    public Int64? Inicializar(Int64 ptip_ope, DateTime pfecha, Usuario pusuario)
    {
        List<Xpinn.Contabilidad.Entities.ProcesoContable> LstProcesoContable = new List<ProcesoContable>();
        LstProcesoContable = ComprobanteServicio.ConsultaProcesoUlt(0, ptip_ope, pfecha, pusuario);
        lstProcesos.DataTextField = "descripcion";
        lstProcesos.DataValueField = "cod_proceso";
        lstProcesos.DataSource = LstProcesoContable;
        lstProcesos.DataBind();
        if (LstProcesoContable.Count() >= 1)
        {
            cod_proceso = LstProcesoContable[0].cod_proceso;
            return LstProcesoContable.Count;
        }
        return 0;
    }

    public void CargarVariables(Int64? ptipo_comp)
    {
        Xpinn.Contabilidad.Services.ComprobanteService ComprobanteServicio = new Xpinn.Contabilidad.Services.ComprobanteService();
        Session[ComprobanteServicio.CodigoPrograma + ".tipo_comp"] = ptipo_comp;        
    }


    public void CargarVariables(Int64? pcod_ope, Int32? ptipo_ope, Int64? pcod_persona, Usuario pusuario)
    {
        Xpinn.Contabilidad.Services.ComprobanteService ComprobanteServicio = new Xpinn.Contabilidad.Services.ComprobanteService();
        Session[ComprobanteServicio.CodigoPrograma + ".cod_ope"] = pcod_ope;
        Session[ComprobanteServicio.CodigoPrograma + ".tipo_ope"] = ptipo_ope;
        Session[ComprobanteServicio.CodigoPrograma + ".cod_persona"] = pcod_persona;
    }

    public void CargarVariables(Int64? pcod_ope, Int32? ptipo_ope, DateTime? pfecha, Int64? pcod_persona, Int64? poficina, Usuario pusuario)
    {
        Xpinn.Contabilidad.Services.ComprobanteService ComprobanteServicio = new Xpinn.Contabilidad.Services.ComprobanteService();
        Session[ComprobanteServicio.CodigoPrograma + ".cod_ope"] = pcod_ope;
        Session[ComprobanteServicio.CodigoPrograma + ".tipo_ope"] = ptipo_ope;
        Session[ComprobanteServicio.CodigoPrograma + ".fecha_ope"] = Convert.ToDateTime(pfecha);
        Session[ComprobanteServicio.CodigoPrograma + ".cod_persona"] = pcod_persona;
        Session[ComprobanteServicio.CodigoPrograma + ".cod_ofi_ope"] = poficina;        
    }


    protected void imgAceptarProceso_Click(object sender, ImageClickEventArgs e)
    {
        lblError.Text = "";
        if (eventoClick != null)
        {
            if (lstProcesos.SelectedValue == null)
            {
                lblError.Text = "Seleccione un tipo de comprobante.";
                return;
            }
            if (lstProcesos.SelectedValue == "")
            {
                lblError.Text = "Seleccione un tipo de comprobante.";
                return;
            }
            cod_proceso = Convert.ToInt64(lstProcesos.SelectedValue);
            eventoClick(sender, e);
            if (NoGeneraComprobante == false)
                GenerarComprobante();
        }
    }

    public void GenerarComprobante()
    {
        lblError.Text = "";
        // Generar el comprobante
        Usuario usuap = (Usuario)Session["usuario"];
        if (lstProcesos.SelectedValue.Trim() == "")
            lstProcesos.SelectedIndex = 0;
        Int64 pcod_proceso = Convert.ToInt64(lstProcesos.SelectedValue);
        Int64 pnum_comp = 0;
        Int64 ptipo_comp = 0;
        Int64 pcod_persona = 0;
        Int64 pcod_ope = 0;
        string pError = "";
        if (Session[ComprobanteServicio.CodigoPrograma + ".cod_ope"] != null)
            pcod_ope = Convert.ToInt64(Session[ComprobanteServicio.CodigoPrograma + ".cod_ope"]);
        Int64 ptip_ope = 0;
        if (Session[ComprobanteServicio.CodigoPrograma + ".tipo_ope"] != null)
            ptip_ope = Convert.ToInt64(Session[ComprobanteServicio.CodigoPrograma + ".tipo_ope"]);
        DateTime pfecha = System.DateTime.Now;
        if (Session[ComprobanteServicio.CodigoPrograma + ".fecha_ope"] != null)
            pfecha = Convert.ToDateTime(Session[ComprobanteServicio.CodigoPrograma + ".fecha_ope"]);
        Int64 pcod_ofi = usuap.cod_oficina;
        if (Session[ComprobanteServicio.CodigoPrograma + ".cod_ofi_ope"] != null)
            pcod_ofi = Convert.ToInt64(Session[ComprobanteServicio.CodigoPrograma + ".cod_ofi_ope"]);
        if (Session[ComprobanteServicio.CodigoPrograma + ".cod_persona"] != null)
            pcod_persona = Convert.ToInt64(Session[ComprobanteServicio.CodigoPrograma + ".cod_persona"]);
        if (pcod_ope != 0 || ptip_ope != 0)
        {
            if (ComprobanteServicio.GenerarComprobante(pcod_ope, ptip_ope, pfecha, pcod_ofi, pcod_persona, pcod_proceso, ref pnum_comp, ref ptipo_comp, ref pError, usuap) == true)
            {
                Session[ComprobanteServicio.CodigoPrograma + ".num_comp"] = pnum_comp;
                Session[ComprobanteServicio.CodigoPrograma + ".tipo_comp"] = ptipo_comp;
                Response.Redirect("../../Contabilidad/Comprobante/Nuevo.aspx");
            }
            else
            {
                lblError.Text = "No se pudo generar el comprobante. Error:" + pError;
            }
        }
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        if (eventoCancelarClick != null)
            eventoCancelarClick(sender, e);
    }

    public Int64? cod_proceso
    {
        get
        {
            Int64? s = ViewState["Cod_Proceso"] as Int64?;
            return s == null ? 0 : s;
        }
        set
        {
            ViewState["Cod_Proceso"] = value;
        }
    }

    public Int64? tipo_comp
    {
        get
        {
            if (cod_proceso > 0)
            { 
                Usuario usuap = (Usuario)Session["usuario"];
                ProcesoContableService procesoservicio = new ProcesoContableService();
                ProcesoContable proceso = new ProcesoContable();
                proceso = procesoservicio.ConsultarProcesoContable(Convert.ToInt64(cod_proceso), usuap);
                return proceso.tipo_comp;
            }
            return null;
        }
    }

    private bool _noGeneraComprobante;
    public bool NoGeneraComprobante
    {
        get { return _noGeneraComprobante; }
        set
        {
            _noGeneraComprobante = value;
        }
    }

}