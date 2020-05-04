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


partial class Lista : GlobalWeb
{
    private Xpinn.Obligaciones.Services.ObligacionCreditoService ObligacionCreditoServicio = new Xpinn.Obligaciones.Services.ObligacionCreditoService();
    private int _tipoOpe = 43;

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(ObligacionCreditoServicio.CodigoPrograma7, "L");

            Site toolBar = (Site)this.Master;            
            toolBar.eventoLimpiar += btnLimpiar_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoGuardar += btnGuardar_Click;
            ctlproceso.eventoCancelarClick += btnCancelarProceso_Click;
            ctlproceso.eventoClick += btnAceptarProceso_Click;
            panelProceso.Visible = false;

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ObligacionCreditoServicio.CodigoPrograma7, "Page_PreInit", ex);
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            ImprimirGrilla();
            if (!IsPostBack)
            {
                Site toolBar = (Site)this.Master;
                toolBar.MostrarGuardar(false);
                pDatos.Visible = false;
                LlenarComboEntidades(ddlEntidad);
                LlenarCombofecha(ddlfecha);
                mvProvision.ActiveViewIndex = 0;
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ObligacionCreditoServicio.CodigoPrograma7, "Page_Load", ex);
        }
    }

    protected void btnLimpiar_Click(object sender, EventArgs e)
    {
        LimpiarValoresConsulta(pConsulta, ObligacionCreditoServicio.CodigoPrograma7);
        Navegar("Lista.aspx");
        pDatos.Visible = false;
        Site toolBar = (Site)this.Master;
        toolBar.MostrarGuardar(false);
        ddlfecha.Enabled = true;
        ddlEntidad.Enabled = true;
    }

    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        Actualizar();
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            DateTime FechaProceso = Convert.ToDateTime(ddlfecha.SelectedValue); 
            // Validar que exista la parametrización contable por procesos
            if (ValidarProcesoContable(FechaProceso, _tipoOpe) == false)
            {
                VerError("No se encontró parametrización contable por procesos para el tipo de operación XX = XXXXXXX");
                return;
            }

            // Determinar código de proceso contable para generar el comprobante
            Int64? rpta = 0;
            if (!panelProceso.Visible && panelGeneral.Visible)
            {
                rpta = ctlproceso.Inicializar(_tipoOpe, FechaProceso, (Usuario)Session["Usuario"]);
                if (rpta > 1)
                {
                    Site toolBar = (Site)Master;
                    toolBar.MostrarGuardar(false);
                    // Activar demás botones que se requieran
                    panelGeneral.Visible = false;
                    panelProceso.Visible = true;
                }
                else
                {
                    // Crear la tarea de ejecución del proceso                
                    if (AplicarDatos())
                        Navegar("../../Contabilidad/Comprobante/Nuevo.aspx");
                    else
                        VerError("Se presentó error");
                }
            }


        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }

    protected bool AplicarDatos()
    {
        try 
        { 
            Xpinn.Obligaciones.Entities.ObligacionCredito datos = new Xpinn.Obligaciones.Entities.ObligacionCredito();
            datos = ObligacionCreditoServicio.ModificarProvision(ObtenerValores(), gvObCredito, (Usuario)Session["usuario"]);
            //mvProvision.ActiveViewIndex = 1;
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
            return false;
        }

        // Se cargan las variables requeridas para generar el comprobante
        Usuario usuario = (Usuario)Session["Usuario"];
        DateTime FechaProceso = Convert.ToDateTime(ddlfecha.SelectedValue);
        ctlproceso.CargarVariables(null, _tipoOpe, FechaProceso, null, 1, usuario);

        return true;
    }


    private void Actualizar()
    {
        try
        {
            String filtro = null;
            List<Xpinn.Obligaciones.Entities.ObligacionCredito> lstConsulta = new List<Xpinn.Obligaciones.Entities.ObligacionCredito>();
            Xpinn.Obligaciones.Entities.ObligacionCredito eObligacion = new Xpinn.Obligaciones.Entities.ObligacionCredito();
            lstConsulta = ObligacionCreditoServicio.ListarProvicionCredito(ObtenerValores(), (Usuario)Session["usuario"],filtro);

            gvObCredito.DataSource = lstConsulta;

            if (lstConsulta.Count > 0)
            {
                pDatos.Visible = true;
                ddlfecha.Enabled = false;
                ddlEntidad.Enabled = false;
                gvObCredito.DataBind();
                decimal acum = 0;
                decimal acum1 = 0;
                decimal acum2 = 0;
                decimal acum3 = 0;
                decimal acum4 = 0;
                decimal monto = 0;
                decimal capital = 0;
                decimal palzo = 0;
               
                foreach (GridViewRow fila in gvObCredito.Rows)
                {
                    monto = decimal.Parse(fila.Cells[4].Text);
                    capital = decimal.Parse(fila.Cells[5].Text);
                    palzo = decimal.Parse(fila.Cells[9].Text);
                    if (palzo <= 12)
                    {
                        acum3 = acum3 + capital;
                    }
                    else
                    {
                        acum2 = acum2 + capital;
                    }    
                    acum = acum + monto;
                    acum1 = acum1 + capital;

                    try
                    {
                        eObligacion.fecha_corte = Convert.ToDateTime(ddlfecha.SelectedValue);
                    }
                    catch (Exception ex)
                    {
                        VerError("Error al convertir la fecha " + ex.Message);
                    }

                    eObligacion.codobligacion = Convert.ToInt64(fila.Cells[0].Text);
                    eObligacion = ObligacionCreditoServicio.ProvisionCredito(eObligacion, (Usuario)Session["usuario"]);
                    fila.Cells[12].Text = eObligacion.intereses.ToString("N0");
                    fila.Cells[11].Text = Convert.ToString(eObligacion.dias_causados);
                    acum4 = acum4 + eObligacion.intereses;
                }

                Txtmonto.Text = acum.ToString("N0");
                Txtsaldocapital.Text = acum1.ToString("N0");
                Txtcortoplazo.Text  = acum2.ToString("N0");
                Txtlagoplazo.Text = acum3.ToString("N0");
                Txtinteres_causado.Text = acum4.ToString("N0");
                TxtValorNota.Text = acum4.ToString("N0");

                Site toolBar = (Site)this.Master;
                toolBar.MostrarGuardar(true);
            }
            else
            {
                gvObCredito.Visible = false;
            }

            Session.Add(ObligacionCreditoServicio.CodigoPrograma7 + ".consulta", 1);
            
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ObligacionCreditoServicio.CodigoPrograma7, "Actualizar", ex);
        }
    }

    private Xpinn.Obligaciones.Entities.ObligacionCredito ObtenerValores()
    {
        Xpinn.Obligaciones.Entities.ObligacionCredito vObligacionCredito = new Xpinn.Obligaciones.Entities.ObligacionCredito();

        vObligacionCredito.codentidad = Convert.ToInt64(ddlEntidad.SelectedValue);
        try
        {
            vObligacionCredito.fecha_corte = Convert.ToDateTime(ddlfecha.SelectedValue);
        }
        catch (Exception ex)
        {
            VerError("Error al convertir la fecha " + ex.Message);
            return null;
        }  
       
        vObligacionCredito.codfiltroorderuno = Convert.ToString(ddlFiltro1.SelectedValue);
        vObligacionCredito.codfiltroorderdos = Convert.ToString(ddlFiltro2.SelectedValue);
        vObligacionCredito.codfiltroordertres = Convert.ToString(ddlFiltro3.SelectedValue);

        return vObligacionCredito;
    }

    protected void LlenarComboEntidades(DropDownList ddlEntidades)
    {
        Xpinn.Caja.Services.BancosService bancoService = new Xpinn.Caja.Services.BancosService();
        Xpinn.Caja.Entities.Bancos banco = new Xpinn.Caja.Entities.Bancos();
        Usuario usuario = new Usuario();
        ddlEntidades.DataSource = bancoService.ListarBancos(banco, usuario);
        ddlEntidades.DataTextField = "nombrebanco";
        ddlEntidades.DataValueField = "cod_banco";
        ddlEntidades.DataBind();
        ddlEntidades.Items.Insert(0, new ListItem("Todos", "0"));
    }

    protected void LlenarCombofecha(DropDownList ddlfecha)
    {
        Xpinn.Obligaciones.Services.ObligacionCreditoService provicionservice = new Xpinn.Obligaciones.Services.ObligacionCreditoService();
        Xpinn.Obligaciones.Entities.ObligacionCredito provicion = new Xpinn.Obligaciones.Entities.ObligacionCredito();

        Usuario usuario = new Usuario();
        provicion.tipo_cierre = "1";
        ddlfecha.DataSource = provicionservice.ListarProvisionFechas(provicion, usuario);
        ddlfecha.DataValueField = "sfecha_corte";
        ddlfecha.DataBind();         
    }


    protected void ImprimirGrilla()
    {
        string printScript =
        @"function PrintGridView()
            {
             div = document.getElementById('DivButtons');
             div.style.display='none';

            var gridInsideDiv = document.getElementById('gvDiv');
            var printWindow = window.open('gview.htm','PrintWindow','letf=0,top=0,width=150,height=300,toolbar=1,scrollbars=1,status=1');
            printWindow.document.write(gridInsideDiv.innerHTML);
            printWindow.document.close();
            printWindow.focus();
            printWindow.print();
            printWindow.close();}";
        this.ClientScript.RegisterStartupScript(Page.GetType(), "PrintGridView", printScript.ToString(), true);
        btnImprimir.Attributes.Add("onclick", "PrintGridView();");
    }


    protected void btnContinuar_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }

    protected void btnCancelarProceso_Click(object sender, EventArgs e)
    {
        Site toolBar = (Site)Master;
        toolBar.MostrarGuardar(true);
        toolBar.MostrarCancelar(true);
        toolBar.MostrarConsultar(true);
        panelGeneral.Visible = true;
        panelProceso.Visible = false;
    }

    protected void btnAceptarProceso_Click(object sender, EventArgs e)
    {
        try
        {
            panelGeneral.Visible = true;
            panelProceso.Visible = false;
            // Aquí va la función que hace lo que se requiera grabar en la funcionalidad
            if (AplicarDatos())
                Navegar("../../Contabilidad/Comprobante/Nuevo.aspx");
            else
                VerError("Se presentó error");

        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }




}