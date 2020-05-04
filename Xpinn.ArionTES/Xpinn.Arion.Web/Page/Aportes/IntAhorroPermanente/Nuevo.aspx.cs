using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Tesoreria.Entities;
using Xpinn.Tesoreria.Services;
using Xpinn.Aportes.Services;
using Xpinn.Aportes.Entities;
using Xpinn.Util;
public partial class Nuevo : GlobalWeb
{
    AporteServices apoServicio = new AporteServices();
    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(apoServicio.CodigoProgramaPagoIntAPermanente, "A");
            Site toolbar = (Site)this.Master;
            toolbar.eventoGuardar += btnGuardar_Click;
            toolbar.eventoCancelar += btnCancelar_Click;
            ctlproceso.eventoCancelarClick += btnCancelarProceso_Click;
            ctlproceso.eventoClick += btnAceptarProceso_Click;
            panelProceso.Visible = false;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(apoServicio.CodigoProgramaPagoIntAPermanente, "Page_Load", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                string pIdObjeto = (string)Session["num_recaudo"];
                CargarCombo();
                ObtenerDatos(pIdObjeto);
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(apoServicio.CodigoProgramaPagoIntAPermanente, "Page_Load", ex);
        }
    }
    protected void CargarCombo()
    {
        try
        {
            PoblarListas poblar = new PoblarListas();
            poblar.PoblarListaDesplegable("empresa_recaudo", "cod_empresa, nom_empresa", "", "1", ddlEmpresa, (Usuario)Session["usuario"]);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(apoServicio.CodigoProgramaPagoIntAPermanente, "CargarEmpresa", ex);
        }
    }

    private void ObtenerDatos(string pIdObjeto)
    {
        try
        {
            RecaudosMasivos vRecaudos = new RecaudosMasivos();
            RecaudosMasivosService RecaudosMasivosServicio = new RecaudosMasivosService();
            vRecaudos = RecaudosMasivosServicio.ConsultarRecaudo(pIdObjeto, (Usuario)Session["usuario"]);

            if (!string.IsNullOrEmpty(vRecaudos.fecha_aplicacion.ToString()))
                txtFecPeriodo.Text = Convert.ToDateTime(vRecaudos.fecha_aplicacion).ToShortDateString();
            if (!string.IsNullOrEmpty(vRecaudos.cod_empresa.ToString()))
                ddlEmpresa.SelectedValue = vRecaudos.cod_empresa.ToString();
            if (!string.IsNullOrEmpty(vRecaudos.numero_novedad.ToString()))
                txtNumRecaudo.Text = vRecaudos.numero_recaudo.ToString().Trim();
            Actualizar();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(apoServicio.CodigoProgramaPagoIntAPermanente, "ObtenerDatos", ex);
        }
    }


    private void Actualizar()
    {
        try
        {
            AporteServices aporteServicio = new AporteServices();
            List<Pago_IntPermanente> lstIntereses = new List<Pago_IntPermanente>();
            lstIntereses = aporteServicio.ListarIntPermanenteRec(Convert.ToInt64(txtNumRecaudo.Text), (Usuario)Session["usuario"]);

            if (lstIntereses.Count > 0)
            {
                Site toolbar = (Site)this.Master;
                toolbar.MostrarGuardar(true);
                pLista.Visible = true;
                gvLista.DataSource = lstIntereses;
                gvLista.DataBind();
                lblTotalRegs.Text = "Registros encontrados " + lstIntereses.Count();
            }
            else
            {
                Site toolbar = (Site)this.Master;
                toolbar.MostrarGuardar(false);
                pLista.Visible = false;
                lblTotalRegs.Text = "La consulta obtuvo resultado";
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(apoServicio.CodigoProgramaPagoIntAPermanente, "Actualizar", ex);
        }
    }

    private List<Pago_IntPermanente> ObtenerLista()
    {
        try
        {
            List<Pago_IntPermanente> lstIntereses = new List<Pago_IntPermanente>();
            foreach (GridViewRow fila in gvLista.Rows)
            {
                Pago_IntPermanente pInteres = new Pago_IntPermanente()
                {
                    cod_persona = Convert.ToInt32(gvLista.DataKeys[fila.RowIndex].Values[0].ToString()),
                    numero_aporte = Convert.ToInt64(fila.Cells[2].Text),
                    valor = Convert.ToInt64(fila.Cells[3].Text.Replace(".","")),
                    cod_atr = 2,
                    tipo_tran = 124
                };
                lstIntereses.Add(pInteres);
            }
            return lstIntereses;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(apoServicio.CodigoProgramaPagoIntAPermanente, "ObtenerLista", ex);
            return null;
        }
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        Int64? rpta = 0;
        if (!panelProceso.Visible && panelGeneral.Visible)
        {
            rpta = ctlproceso.Inicializar(113, DateTime.Now, (Usuario)Session["Usuario"]);
            if (rpta > 1)
            {
                Site toolBar = (Site)Master;
                toolBar.MostrarGuardar(false);
                toolBar.MostrarCancelar(false);
                panelGeneral.Visible = false;
                panelProceso.Visible = true;
            }
            else
            {
                RegistrarInteres();
                Navegar("../../Contabilidad/Comprobante/Nuevo.aspx");
            }
        }
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Session.Remove("num_recaudo");
        Navegar(Pagina.Lista);
    }

    protected void btnCancelarProceso_Click(object sender, EventArgs e)
    {
        Site toolBar = (Site)Master;
        toolBar.MostrarGuardar(true);
        toolBar.MostrarCancelar(true);
        panelGeneral.Visible = true;
        panelProceso.Visible = false;
    }

    protected void btnAceptarProceso_Click(object sender, EventArgs e)
    {
        try
        {
            panelGeneral.Visible = true;
            panelProceso.Visible = false;
            RegistrarInteres();
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }

    private void RegistrarInteres()
    {
        try
        {
            if (gvLista.Rows.Count > 0)
            {
                AporteServices aporteServicio = new AporteServices();
                List<Pago_IntPermanente> lstIntereses = new List<Pago_IntPermanente>();
                Operacion pOperacion = new Operacion();
                lstIntereses = ObtenerLista();
                aporteServicio.CrearPagoIntereses(lstIntereses, DateTime.Now, ref pOperacion, (Usuario)Session["usuario"]);
                
                if (pOperacion != null)
                    ctlproceso.CargarVariables(pOperacion.cod_ope, Convert.ToInt32(pOperacion.tipo_ope), 0, (Usuario)Session["usuario"]);
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(apoServicio.CodigoProgramaPagoIntAPermanente, "Page_Load", ex);
        }
    }
}