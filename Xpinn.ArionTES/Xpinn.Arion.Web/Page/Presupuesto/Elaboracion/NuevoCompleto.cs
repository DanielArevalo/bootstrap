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
using System.Web.UI.HtmlControls;
using System.Drawing;

partial class Nuevo : GlobalWeb
{
    private Xpinn.Presupuesto.Services.PresupuestoService PresupuestoServicio = new Xpinn.Presupuesto.Services.PresupuestoService();
    private GridViewHelper helper;
    private List<int> mQuantities = new List<int>();
    private Boolean bCalcularCartera = false;

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[PresupuestoServicio.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(PresupuestoServicio.CodigoPrograma, "E");
            else
                VisualizarOpciones(PresupuestoServicio.CodigoPrograma, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;

            txtValPoliza.CambiarAncho(100);

            //txtValorPorCredito.eventoCambiar += txtValorPorCredito_TextChanged;
            //txtValPoliza.eventoCambiar += txtValPoliza_TextChanged;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(PresupuestoServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                mpeGrabar.Hide();
                Site toolBar = (Site)this.Master;
                toolBar.MostrarGuardar(false);

                CargarDDList();
                Usuario usuap = new Usuario();
                usuap = (Usuario)Session["Usuario"];
                txtFechaElabora.ToDateTime = DateTime.Now;
                txtFechaAprobacion.ToDateTime = DateTime.Now;
                txtElaborador.Text = usuap.nombre;
                txtAprobador.Text = usuap.nombre;

                txtCodigo.Enabled = false;
                txtElaborador.Enabled = false;
                txtAprobador.Enabled = false;
                txtFechaElabora.Enabled = false;
                txtFechaAprobacion.Enabled = false;
                ddlPeriodicidad.Enabled = false;

                if (Session[PresupuestoServicio.CodigoPrograma + ".idpresupuesto"] != null)
                {
                    idObjeto = Session[PresupuestoServicio.CodigoPrograma + ".idpresupuesto"].ToString();
                    Session.Remove(PresupuestoServicio.CodigoPrograma + ".idpresupuesto");
                    ObtenerDatos(idObjeto);
                }
                else
                {
                    txtCodigo.Text = PresupuestoServicio.ObtenerSiguienteCodigo((Usuario)Session["Usuario"]).ToString();
                    txtNumPeriodos.Text = "12";
                    txtPeriodoInicial.ToDateTime = new DateTime(DateTime.Now.Year, 1, 31);
                }
            }
            gvProyeccion.UseAccessibleHeader = true;
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(PresupuestoServicio.CodigoPrograma, "Page_Load", ex);
        }
    }

    /// <summary>
    /// Método para llenar los drop down list
    /// </summary>
    private void CargarDDList()
    {
        Xpinn.Presupuesto.Services.TipoPresupuestoService TipoPresupuestoService = new Xpinn.Presupuesto.Services.TipoPresupuestoService();
        Xpinn.Presupuesto.Entities.TipoPresupuesto TipoPresupuesto = new Xpinn.Presupuesto.Entities.TipoPresupuesto();
        ddlTipoPresupuesto.DataSource = TipoPresupuestoService.ListarTipoPresupuesto(TipoPresupuesto, (Usuario)Session["Usuario"]);
        ddlTipoPresupuesto.DataTextField = "descripcion";
        ddlTipoPresupuesto.DataValueField = "tipo_presupuesto";
        ddlTipoPresupuesto.DataBind();

        Xpinn.FabricaCreditos.Services.PeriodicidadService PeriodicidadService = new Xpinn.FabricaCreditos.Services.PeriodicidadService();
        Xpinn.FabricaCreditos.Entities.Periodicidad ePeriodicidad = new Xpinn.FabricaCreditos.Entities.Periodicidad();
        ddlPeriodicidad.DataSource = PeriodicidadService.ListarPeriodicidad(ePeriodicidad, (Usuario)Session["Usuario"]);
        ddlPeriodicidad.DataTextField = "Descripcion";
        ddlPeriodicidad.DataValueField = "Codigo";
        ddlPeriodicidad.DataBind();

        Xpinn.Contabilidad.Services.CentroCostoService cencosService = new Xpinn.Contabilidad.Services.CentroCostoService();
        ddlCentroCosto.DataSource = cencosService.ListarCentroCosto((Usuario)Session["Usuario"], "");
        ddlCentroCosto.DataTextField = "nom_centro";
        ddlCentroCosto.DataValueField = "centro_costo";
        ddlCentroCosto.DataBind();
    }

    /// <summary>
    /// Método para guardar los datos del presupuesto
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        if (Session["DTPRESUPUESTO"] == null)
        {
            VerError("No se ha generado el presupuesto");
            return;
        }
        VerError("");
        mpeGrabar.Show();
    }

    protected void btnContinuar_Click(object sender, EventArgs e)
    {
        try
        {
            Xpinn.Util.Usuario pUsuario = new Xpinn.Util.Usuario();
            pUsuario = (Usuario)Session["Usuario"];

            if (txtDescripcion.Text == "")
            {
                VerError("Debe ingresar la descripcion");
                return;
            }

            Xpinn.Presupuesto.Entities.Presupuesto vPresupuesto = new Xpinn.Presupuesto.Entities.Presupuesto();

            if (idObjeto != "")
                vPresupuesto = PresupuestoServicio.ConsultarPresupuesto(Convert.ToInt64(idObjeto), (Usuario)Session["usuario"]);

            vPresupuesto.idpresupuesto = Convert.ToInt64(txtCodigo.Text.Trim());
            vPresupuesto.descripcion = Convert.ToString(txtDescripcion.Text.Trim());
            vPresupuesto.fecha_elaboracion = Convert.ToDateTime(txtFechaElabora.ToDateTime);
            vPresupuesto.fecha_aprobacion = Convert.ToDateTime(txtFechaAprobacion.ToDateTime);
            vPresupuesto.cod_elaboro = pUsuario.codusuario;
            vPresupuesto.cod_aprobo = pUsuario.codusuario;
            vPresupuesto.tipo_presupuesto = Convert.ToInt32(ddlTipoPresupuesto.SelectedValue.ToString());
            vPresupuesto.num_periodos = Convert.ToInt32(txtNumPeriodos.Text);
            vPresupuesto.cod_periodicidad = Convert.ToInt32(ddlPeriodicidad.SelectedValue.ToString());
            vPresupuesto.periodo_inicial = Convert.ToDateTime(txtPeriodoInicial.ToDateTime);
            vPresupuesto.centro_costo = Convert.ToInt32(ddlCentroCosto.SelectedValue.ToString());

            // Cargar los datos
            vPresupuesto.dtPresupuesto = new DataTable();
            if (Session["DTPRESUPUESTO"] != null)
                vPresupuesto.dtPresupuesto = (DataTable)Session["DTPRESUPUESTO"];
            vPresupuesto.dtFlujo = new DataTable();
            if (Session["DTFLUJO"] != null)
                vPresupuesto.dtFlujo = (DataTable)Session["DTFLUJO"];
            vPresupuesto.dtFechas = new DataTable();
            if (Session["DTFECHAS"] != null)
                vPresupuesto.dtFechas = (DataTable)Session["DTFECHAS"];
            vPresupuesto.dtColocacion = new DataTable();
            if (Session["DTCOLOCACION"] != null)
                vPresupuesto.dtColocacion = (DataTable)Session["DTCOLOCACION"];
            // Cargando la grila de obligaciones
            vPresupuesto.dtObligacionesNuevas = new DataTable();
            if (Session["DTOBLIGACIONESNUEVAS"] != null)
                vPresupuesto.dtObligacionesNuevas = (DataTable)Session["DTOBLIGACIONESNUEVAS"];               

            // Determinar el valor de polizas
            Configuracion conf = new Configuracion();
            vPresupuesto.valorPromedioCredito = 0;
            vPresupuesto.porPolizasVencidas = 0;
            vPresupuesto.valorUnitPoliza = 0;
            vPresupuesto.comisionPoliza = 0;
            vPresupuesto.porLeyMiPyme = 0;
            vPresupuesto.porProvision = 0;
            vPresupuesto.porProvisionGen = 0;
            try
            {
                vPresupuesto.valorPromedioCredito = txtValorPorCredito.Text== "" ? Convert.ToDouble(0) : Convert.ToDouble(txtValorPorCredito.Text);
                vPresupuesto.porPolizasVencidas = Math.Round(txtPorPoliza.Text == "" ? Convert.ToDouble(0) : Convert.ToDouble(txtPorPoliza.Text) / 100, 4);
                vPresupuesto.valorUnitPoliza = txtValPoliza.Text == "" ? Convert.ToDouble(0) : Convert.ToDouble(txtValPoliza.Text);
                vPresupuesto.comisionPoliza = txtComision.Text == "" ? Convert.ToDouble(0) : Convert.ToDouble(txtComision.Text) / 100;
                vPresupuesto.porLeyMiPyme = txtLeyMiPYME.Text == "" ? Convert.ToDouble(0) : Convert.ToDouble(txtLeyMiPYME.Text) / 100;
                vPresupuesto.porProvision = txtPorProvision.Text == "" ? Convert.ToDouble(0) : Convert.ToDouble(txtPorProvision.Text) / 100;
                vPresupuesto.porProvisionGen = txtPorProvisionGeneral.Text == "" ? Convert.ToDouble(0) : Convert.ToDouble(txtPorProvisionGeneral.Text) / 100;
            }
            catch { VerError("Error al determinar el valor de polizas"); return; }
            try
            {
                vPresupuesto.flujoinicial = Convert.ToDouble(txtFlujoInicial.Text.Replace(conf.ObtenerSeparadorMilesConfig(), ""));
                vPresupuesto.fechacorte = Convert.ToDateTime(txtFechaCorte.ToDateTime);
            }
            catch { }

            if (idObjeto != "")
            {
                vPresupuesto.idpresupuesto = Convert.ToInt64(idObjeto);
                PresupuestoServicio.ModificarPresupuesto(vPresupuesto, (Usuario)Session["usuario"]);
            }
            else
            {
                vPresupuesto = PresupuestoServicio.CrearPresupuesto(vPresupuesto, (Usuario)Session["usuario"]);
                idObjeto = vPresupuesto.idpresupuesto.ToString();
            }

            Session[PresupuestoServicio.CodigoPrograma + ".id"] = idObjeto;
            mvPresupuesto.ActiveViewIndex = 8 ;
    }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
}
        catch (Exception ex)
        {
            BOexcepcion.Throw(PresupuestoServicio.CodigoPrograma, "btnGuardar_Click", ex);
        }
    }

    protected void btnParar_Click(object sender, EventArgs e)
    {
        mpeGrabar.Hide();
    }

    protected void btnFinalClick(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }


    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        if (idObjeto == "")
        {
            if (Session["DTPRESUPUESTO"] != null)
            {
                Session.Remove("DTPRESUPUESTO");
                Session.Remove("DTFECHAS");
                Session.Remove("DTCOLOCACION");
                Session.Remove("DTSALDOS");
                Session.Remove("DTNOMINA");
                Session.Remove("DTACTIVOSFIJ");
                Session.Remove("DTDIFERIDOS");
                Session.Remove("DTOTROS");
                Session.Remove("DTHONORARIOS");
                Session.Remove("DTOBLIGACIONES");
                Session.Remove("DTOBLIGACIONESNUEVAS");
                Session.Remove("DTOBLIGACIONESINT");
            }
        }
        else
        {
            Session[PresupuestoServicio.CodigoPrograma + ".id"] = idObjeto;
        }
        Navegar(Pagina.Lista);
    }

    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            Xpinn.Presupuesto.Entities.Presupuesto vPresupuesto = new Xpinn.Presupuesto.Entities.Presupuesto();
            vPresupuesto = PresupuestoServicio.ConsultarPresupuesto(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);

            txtCodigo.Text = HttpUtility.HtmlDecode(vPresupuesto.idpresupuesto.ToString().Trim());
            Session["IDPRESUPUESTO"] = vPresupuesto.idpresupuesto;
            ddlTipoPresupuesto.SelectedValue = vPresupuesto.tipo_presupuesto.ToString();
            ddlCentroCosto.SelectedValue = vPresupuesto.centro_costo.ToString();
            if (!string.IsNullOrEmpty(vPresupuesto.descripcion))
                txtDescripcion.Text = HttpUtility.HtmlDecode(vPresupuesto.descripcion.ToString().Trim());
            txtNumPeriodos.Text = HttpUtility.HtmlDecode(vPresupuesto.num_periodos.ToString().Trim());            
            ddlPeriodicidad.SelectedValue = HttpUtility.HtmlDecode(vPresupuesto.cod_periodicidad.ToString().Trim());
            if (vPresupuesto.periodo_inicial != null)
                txtPeriodoInicial.ToDateTime = Convert.ToDateTime(HttpUtility.HtmlDecode(vPresupuesto.periodo_inicial.ToString().Trim()));
            if (vPresupuesto.fecha_elaboracion != null)
                txtFechaElabora.ToDateTime = Convert.ToDateTime(HttpUtility.HtmlDecode(vPresupuesto.fecha_elaboracion.ToString().Trim()));
            if (vPresupuesto.fecha_aprobacion != null)
                txtFechaAprobacion.ToDateTime = Convert.ToDateTime(HttpUtility.HtmlDecode(vPresupuesto.fecha_aprobacion.ToString().Trim()));

            Configuracion conf = new Configuracion();

            try
            {
                txtValorPorCredito.Text = Convert.ToString(vPresupuesto.valorPromedioCredito);
                txtPorPoliza.Text = Convert.ToString(vPresupuesto.porPolizasVencidas*100);
                txtValPoliza.Text = Convert.ToString(vPresupuesto.valorUnitPoliza);
                txtComision.Text = Convert.ToString(vPresupuesto.comisionPoliza*100);
                txtLeyMiPYME.Text = Convert.ToString(vPresupuesto.porLeyMiPyme*100);
                txtPorProvision.Text = Convert.ToString(vPresupuesto.porProvision*100);
                txtPorProvisionGeneral.Text = Convert.ToString(vPresupuesto.porProvisionGen*100);
                txtFlujoInicial.Text = Convert.ToString(vPresupuesto.flujoinicial);
            }
            catch (Exception ex)
            {
                VerError(ex.Message);
            }

            Session.Remove("DTPRESUPUESTO");
            Session.Remove("DTFECHAS");
            Session.Remove("DTCOLOCACION");
            Session.Remove("DTSALDOS");
            Session.Remove("DTNOMINA");
            Session.Remove("DTACTIVOSFIJ");
            Session.Remove("DTDIFERIDOS");
            Session.Remove("DTOTROS");
            Session.Remove("DTHONORARIOS");
            Session.Remove("DTOBLIGACIONES");
            Session.Remove("DTOBLIGACIONESNUEVAS");
            Session.Remove("DTOBLIGACIONESINT");

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(PresupuestoServicio.CodigoPrograma, "ObtenerDatos", ex);
        }
    }

    /// <summary>
    /// Método para ir a la pantalla del detalle del presupuesto
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSiguiente_Click(object sender, ImageClickEventArgs e)
    {
        if (Session["DTPRESUPUESTO"] == null)
            {

            string Error = "";
            string filtro = "";

            // Determinar fecha del histórico
            DateTime fecha1erPeriodo = DateTime.MaxValue;
            try
            {
                fecha1erPeriodo = txtPeriodoInicial.ToDateTime;
            }
            catch
            {
                fecha1erPeriodo = DateTime.MaxValue;
            }
            DateTime fechahistorico = PresupuestoServicio.FechaUltimoHistorico(fecha1erPeriodo, (Usuario)Session["Usuario"]);
            txtFechaCorte.ToDateTime = fechahistorico;

            // Inicializar el datatable
            DataTable dtFechas = new DataTable();
            dtFechas.Clear();
            DataTable dtProy = new DataTable();
            dtProy.Clear();
            DataTable dtColocacion = new DataTable();
            dtColocacion.Clear();
            DataTable dtSaldos = new DataTable();
            dtSaldos.Clear();
            DataTable dtNomina = new DataTable();
            dtNomina.Clear();
            DataTable dtActivosFij = new DataTable();
            dtActivosFij.Clear();
            DataTable dtDiferidos = new DataTable();
            dtDiferidos.Clear();
            DataTable dtOtros = new DataTable();
            dtOtros.Clear();
            DataTable dtHonorarios = new DataTable();
            dtHonorarios.Clear();
            DataTable dtObligaciones = new DataTable();
            dtObligaciones.Clear();
            DataTable dtObligacionesNuevas = new DataTable();
            dtObligacionesNuevas.Clear();
            DataTable dtObligacionesPagos = new DataTable();
            dtObligacionesPagos.Clear();
            DataTable dtObligacionesTot = new DataTable();
            dtObligacionesTot.Clear();
            DataTable dtObligacionesTotPagos = new DataTable();
            dtObligacionesTotPagos.Clear();
            DataTable dtRequerido = new DataTable();
            dtRequerido.Clear();
            DataTable dtCargos = new DataTable();
            dtCargos.Clear();
            DataTable dtTecnologia = new DataTable();
            dtTecnologia.Clear();
            
            // Cargar la grilla con las cuentas contables para el presupuesto
            dtProy = PresupuestoServicio.ListarCuentas(filtro, (Usuario)Session["Usuario"]);
            DataColumn[] dtLlavePresupuesto = new DataColumn[1];
            dtLlavePresupuesto[0] = dtProy.Columns[0];
            dtProy.PrimaryKey = dtLlavePresupuesto;            

            // Llenar la grilla con la información inicial de los créditos a la fecha de cierre    
            dtFechas.Columns.Add("numero", typeof(int));
            dtFechas.Columns.Add("fecha_inicial", typeof(DateTime));
            dtFechas.Columns.Add("fecha_final", typeof(DateTime));            
            if (ddlCentroCosto.SelectedValue.ToString().Trim() != "0")
                filtro = " And h.cod_oficina In (Select x.cod_oficina From oficina x Where x.centro_costo = " + ddlCentroCosto.SelectedValue.ToString().Trim() + ") ";
            else
                filtro = "";
            dtSaldos = PresupuestoServicio.ListarClasificacionOficinas(fechahistorico, filtro, (Usuario)Session["Usuario"]);

            // Llenar la grila con la información de la nomina
            if (ddlCentroCosto.SelectedValue.ToString().Trim() != "0")
                filtro = " e.cod_oficina In (Select x.cod_oficina From oficina x Where x.centro_costo = " + ddlCentroCosto.SelectedValue.ToString().Trim() + ") ";
            else
                filtro = "";
            dtNomina = PresupuestoServicio.ListarNomina(fechahistorico, ref Error, (Usuario)Session["Usuario"], filtro);
            DataColumn[] dtLlaveNomina = new DataColumn[1];
            dtLlaveNomina[0] = dtNomina.Columns[0];
            dtNomina.PrimaryKey = dtLlaveNomina;
            dtCargos = PresupuestoServicio.ListarCargos((Usuario)Session["Usuario"]);
            DataColumn[] dtLlaveCargos = new DataColumn[1];
            dtLlaveCargos[0] = dtCargos.Columns[0];
            dtCargos.PrimaryKey = dtLlaveCargos;

            // Llenar la grila con la información de activos fijos
            dtActivosFij = PresupuestoServicio.ListarActivosFijos(fechahistorico, ref Error, (Usuario)Session["Usuario"]);
            if (dtActivosFij.Rows.Count == 0)
                CrearActivoFijoInicial(ref dtActivosFij);
            DataColumn[] dtLlaveActivosFij = new DataColumn[1];
            dtLlaveActivosFij[0] = dtActivosFij.Columns[0];
            dtActivosFij.PrimaryKey = dtLlaveActivosFij;

            // Llenar la grila con la información de Tecnologia
            dtTecnologia = PresupuestoServicio.ListarTecnologia(fechahistorico, ref Error, (Usuario)Session["Usuario"]);
            if (dtTecnologia.Rows.Count == 0)
                CrearTecnologiaInicial(ref dtTecnologia);
            DataColumn[] dtLlaveTecnologia = new DataColumn[1];
            dtLlaveTecnologia[0] = dtTecnologia.Columns[0];
            dtTecnologia.PrimaryKey = dtLlaveTecnologia;

            // Llenar la grila con la información de diferidos
            dtDiferidos = PresupuestoServicio.ListarDiferidos(fechahistorico, ref Error, (Usuario)Session["Usuario"]);
            if (dtDiferidos.Rows.Count == 0)
                CrearDiferidoInicial(ref dtDiferidos);
            DataColumn[] dtLlaveDiferidos = new DataColumn[1];
            dtLlaveDiferidos[0] = dtDiferidos.Columns[0];
            dtDiferidos.PrimaryKey = dtLlaveDiferidos;

            // Llenar la grila con la información de otros
            dtOtros = PresupuestoServicio.ListarOtros(fechahistorico, ref Error, (Usuario)Session["Usuario"]);
            DataColumn[] dtLlaveOtros = new DataColumn[1];
            dtLlaveOtros[0] = dtOtros.Columns[0];
            dtOtros.PrimaryKey = dtLlaveOtros;

            // Llenar la grila con la información de honorarios
            dtHonorarios = PresupuestoServicio.ListarHonorarios(fechahistorico, ref Error, (Usuario)Session["Usuario"]);
            if (dtHonorarios.Rows.Count == 0)
                CrearHonorariosInicial(ref dtHonorarios);
            DataColumn[] dtLlaveHonorarios = new DataColumn[1];
            dtLlaveHonorarios[0] = dtHonorarios.Columns[0];
            dtHonorarios.PrimaryKey = dtLlaveHonorarios;

            // Llenar la grila con la información de obligaciones financieras
            if (ddlCentroCosto.SelectedValue.ToString().Trim() != "0")
                filtro = " And o.cod_oficina In (Select x.cod_oficina From oficina x Where x.centro_costo = " + ddlCentroCosto.SelectedValue.ToString().Trim() + ") ";
            else
                filtro = "";
            dtObligaciones = PresupuestoServicio.ListarObligaciones(fechahistorico, ref Error, (Usuario)Session["Usuario"], filtro);
            DataColumn[] dtLlaveObligaciones = new DataColumn[1];
            dtLlaveObligaciones[0] = dtObligaciones.Columns[0];
            dtObligaciones.PrimaryKey = dtLlaveObligaciones;

            // Llenar la grila con la información de totales obligaciones financieras
            if (ddlCentroCosto.SelectedValue.ToString().Trim() != "0")
                filtro = " And o.cod_oficina In (Select x.cod_oficina From oficina x Where x.centro_costo = " + ddlCentroCosto.SelectedValue.ToString().Trim() + ") ";
            else
                filtro = "";
            dtObligacionesTot = PresupuestoServicio.ListarObligacionesTotal(fechahistorico, ref Error, (Usuario)Session["Usuario"], filtro);
            DataColumn[] dtLlaveObligacionesTot = new DataColumn[1];
            dtLlaveObligacionesTot[0] = dtObligacionesTot.Columns[0];
            dtObligacionesTot.PrimaryKey = dtLlaveObligacionesTot;

            // Llenar la grila con la información de totales obligaciones financieras
            if (ddlCentroCosto.SelectedValue.ToString().Trim() != "0")
                filtro = " And o.cod_oficina In (Select x.cod_oficina From oficina x Where x.centro_costo = " + ddlCentroCosto.SelectedValue.ToString().Trim() + ") ";
            else
                filtro = "";
            dtObligacionesTotPagos = PresupuestoServicio.ListarObligacionesTotalPagos(fechahistorico, ref Error, (Usuario)Session["Usuario"], filtro);
            DataColumn[] dtLlaveObligacionesTotPagos = new DataColumn[2];
            dtLlaveObligacionesTotPagos[0] = dtObligacionesTotPagos.Columns[0];
            dtLlaveObligacionesTotPagos[1] = dtObligacionesTotPagos.Columns[1];
            dtObligacionesTotPagos.PrimaryKey = dtLlaveObligacionesTotPagos;

            // Llenar la grila con la información de pagos de intereses y capital  de obligaciones financieras
            if (ddlCentroCosto.SelectedValue.ToString().Trim() != "0")
                filtro = " And o.cod_oficina In (Select x.cod_oficina From oficina x Where x.centro_costo = " + ddlCentroCosto.SelectedValue.ToString().Trim() + ") ";
            else
                filtro = "";
            dtObligacionesPagos = PresupuestoServicio.ListarObligacionesPagos(fechahistorico, ref Error, (Usuario)Session["Usuario"], filtro);
            DataColumn[] dtLlaveObligacionesPagos = new DataColumn[2];
            dtLlaveObligacionesPagos[0] = dtObligacionesPagos.Columns[0];
            dtLlaveObligacionesPagos[1] = dtObligacionesPagos.Columns[2];
            dtObligacionesPagos.PrimaryKey = dtLlaveObligacionesPagos;

            // Definir el datatable para las obligaciones nuevas
            dtObligacionesNuevas = PresupuestoServicio.ListarObligacionesNuevas(fechahistorico, ref Error, (Usuario)Session["Usuario"], filtro);
            DataColumn[] dtLlaveObligacionesNuevas = new DataColumn[1];
            dtLlaveObligacionesNuevas[0] = dtObligacionesNuevas.Columns[0];
            dtObligacionesNuevas.PrimaryKey = dtLlaveObligacionesNuevas;
            if (dtObligacionesNuevas.Rows.Count == 0)
                CrearObligacionesNuevasInicial(ref dtObligacionesNuevas);

            // Definir el datatable para los saldos de cartera con las columnas iniciales
            dtColocacion.Columns.Add("codigo", typeof(string));
            dtColocacion.Columns["codigo"].AllowDBNull = true;
            dtColocacion.Columns["codigo"].DefaultValue = " ";
            DataColumn[] dtLlave = new DataColumn[1];
            dtLlave[0] = dtColocacion.Columns[0];
            dtColocacion.PrimaryKey =  dtLlave;
            dtColocacion.Columns.Add("descripcion", typeof(string));
            dtColocacion.Columns["descripcion"].AllowDBNull = true;
            dtColocacion.Columns["descripcion"].DefaultValue = " ";
            dtColocacion.Columns.Add("num_ejecutivos", typeof(string));
            dtColocacion.Columns["num_ejecutivos"].AllowDBNull = true;
            dtColocacion.Columns["num_ejecutivos"].DefaultValue = " ";
    
            // Llenar la grilla de cartera
            DataRow drCar1;
            drCar1 = dtColocacion.NewRow();
            drCar1[0] = "1";
            drCar1[1] = "Tasa de Interes Cte Promedio";
            drCar1[2] = null;
            dtColocacion.Rows.Add(drCar1);
            DataRow drCar2;
            drCar2 = dtColocacion.NewRow();
            drCar2[0] = "2";
            drCar2[1] = "Plazo Promedio de Colocación";
            drCar2[2] = null;
            dtColocacion.Rows.Add(drCar2);
            DataRow drCar3;
            drCar3 = dtColocacion.NewRow();
            drCar3[0] = "3";
            drCar3[1] = "Colocacion por Ejecutivo";
            drCar3[2] = null;
            dtColocacion.Rows.Add(drCar3);
            DataRow drCar4;
            drCar4 = dtColocacion.NewRow();
            drCar4[0] = "4";
            drCar4[1] = "Colocacion por Oficina:";
            drCar4[2] = null;
            dtColocacion.Rows.Add(drCar4);
            if (ddlCentroCosto.SelectedValue.ToString().Trim() != "0")
                filtro = " a.centro_costo = " + ddlCentroCosto.SelectedValue.ToString().Trim();
            else
                filtro = "";
            List<Xpinn.Caja.Entities.Oficina> lstOficina = new List<Xpinn.Caja.Entities.Oficina>();
            Xpinn.Caja.Services.OficinaService OficinaServicio = new Xpinn.Caja.Services.OficinaService();
            Xpinn.Caja.Entities.Oficina eOficina = new Xpinn.Caja.Entities.Oficina();
            lstOficina = OficinaServicio.ListarOficina(eOficina, (Usuario)Session["Usuario"], filtro);
            int numtotalEjecutivos = 0;
            foreach (Xpinn.Caja.Entities.Oficina fila in lstOficina)
            {
                DataRow drCarOf;
                drCarOf = dtColocacion.NewRow();
                drCarOf[0] = "4." + fila.cod_oficina;
                drCarOf[1] = "........" + fila.cod_oficina + "-" + fila.nombre;
                drCarOf[2] = PresupuestoServicio.NumeroEjecutivosOficina(Convert.ToInt32(fila.cod_oficina), (Usuario)Session["Usuario"]);
                dtColocacion.Rows.Add(drCarOf);
                numtotalEjecutivos = numtotalEjecutivos + Convert.ToInt32(drCarOf[2]);
            };
            DataRow drCar5;
            drCar5 = dtColocacion.NewRow();
            drCar5[0] = "5";
            drCar5[1] = "Total Colocacion";
            drCar5[2] = numtotalEjecutivos;
            dtColocacion.Rows.Add(drCar5);
            DataRow drCar6;
            drCar6 = dtColocacion.NewRow();
            drCar6[0] = "6";
            drCar6[1] = "No. Créditos Colocados x Ejecutivo";
            drCar6[2] = null;
            dtColocacion.Rows.Add(drCar6);
            DataRow drCar7;
            drCar7 = dtColocacion.NewRow();
            drCar7[0] = "7";
            drCar7[1] = "Total Créditos Colocados";
            drCar7[2] = null;
            dtColocacion.Rows.Add(drCar7);
            DataRow drCar8_1;
            drCar8_1 = dtColocacion.NewRow();
            drCar8_1[0] = "8-1";
            drCar8_1[1] = "Recuperaciones de Capital (-Viejos-)";
            drCar8_1[2] = null;
            dtColocacion.Rows.Add(drCar8_1);
            DataRow drCar8_2;
            drCar8_2 = dtColocacion.NewRow();
            drCar8_2[0] = "8-2";
            drCar8_2[1] = "Recuperaciones de Capital (-Nuevos-)";
            drCar8_2[2] = null;
            dtColocacion.Rows.Add(drCar8_2);
            DataRow drCar8;
            drCar8 = dtColocacion.NewRow();
            drCar8[0] = "8";
            drCar8[1] = "Recuperaciones de Capital Total";
            drCar8[2] = null;
            dtColocacion.Rows.Add(drCar8);
            DataRow drCar9;
            drCar9 = dtColocacion.NewRow();
            drCar9[0] = "9";
            drCar9[1] = "Saldos de Cartera";
            drCar9[2] = null;
            dtColocacion.Rows.Add(drCar9);
            DataRow drCar9_1;
            drCar9_1 = dtColocacion.NewRow();
            drCar9_1[0] = "9-1";
            drCar9_1[1] = "% Variación de Cartera";
            drCar9_1[2] = null;
            dtColocacion.Rows.Add(drCar9_1);
            DataRow drCar9_2;
            drCar9_2 = dtColocacion.NewRow();
            drCar9_2[0] = "9-2";
            drCar9_2[1] = "% Cartera 1 a 30 días";
            drCar9_2[2] = null;
            dtColocacion.Rows.Add(drCar9_2);
            DataRow drCar9_3;
            drCar9_3 = dtColocacion.NewRow();
            drCar9_3[0] = "9-3";
            drCar9_3[1] = "% Cartera 31 a 60 días";
            drCar9_3[2] = null;
            dtColocacion.Rows.Add(drCar9_3);
            DataRow drCar9_4;
            drCar9_4 = dtColocacion.NewRow();
            drCar9_4[0] = "9-4";
            drCar9_4[1] = "% Cartera 61 a 90 días";
            drCar9_4[2] = null;
            dtColocacion.Rows.Add(drCar9_4);
            DataRow drCar9_5;
            drCar9_5 = dtColocacion.NewRow();
            drCar9_5[0] = "9-5";
            drCar9_5[1] = "% Cartera Mora > 90 días";
            drCar9_5[2] = null;
            dtColocacion.Rows.Add(drCar9_5);
            DataRow drCar10_1;
            drCar10_1 = dtColocacion.NewRow();
            drCar10_1[0] = "10-1";
            drCar10_1[1] = "Recuperaciones de IntCte (-Viejos-)";
            drCar10_1[2] = null;
            dtColocacion.Rows.Add(drCar10_1);
            DataRow drCar10_2;
            drCar10_2 = dtColocacion.NewRow();
            drCar10_2[0] = "10-2";
            drCar10_2[1] = "Recuperaciones de IntCte (-Nuevos-)";
            drCar10_2[2] = null;
            dtColocacion.Rows.Add(drCar10_2);
            DataRow drCar11;
            drCar11 = dtColocacion.NewRow();
            drCar11[0] = "11";
            drCar11[1] = "INGRESOS DE CARTERA";
            drCar11[2] = null;
            dtColocacion.Rows.Add(drCar11);
            DataRow drCar10_3;
            drCar10_3 = dtColocacion.NewRow();
            drCar10_3[0] = "10-3";
            drCar10_3[1] = "Causación de IntCte (-Viejos-)";
            drCar10_3[2] = null;
            dtColocacion.Rows.Add(drCar10_3);
            DataRow drCar12;
            drCar12 = dtColocacion.NewRow();
            drCar12[0] = "12";
            drCar12[1] = "Número Polizas Vendidas";
            drCar12[2] = null;
            dtColocacion.Rows.Add(drCar12);
            DataRow drCar13;
            drCar13 = dtColocacion.NewRow();
            drCar13[0] = "13";
            drCar13[1] = "Valor Polizas Vendidas";
            drCar13[2] = null;
            dtColocacion.Rows.Add(drCar13);
            DataRow drCar14;
            drCar14 = dtColocacion.NewRow();
            drCar14[0] = "14";
            drCar14[1] = "Comisión";
            drCar14[2] = null;
            dtColocacion.Rows.Add(drCar14);
            DataRow drCar15_1;
            drCar15_1 = dtColocacion.NewRow();
            drCar15_1[0] = "15-1";
            drCar15_1[1] = "Ley MiPYME (-Viejos-)";
            drCar15_1[2] = null;
            dtColocacion.Rows.Add(drCar15_1);
            DataRow drCar15_2;
            drCar15_2 = dtColocacion.NewRow();
            drCar15_2[0] = "15-2";
            drCar15_2[1] = "Ley MiPYME (-Nuevos-)";
            drCar15_2[2] = null;
            dtColocacion.Rows.Add(drCar15_2);
            DataRow drCar15;
            drCar15 = dtColocacion.NewRow();
            drCar15[0] = "15";
            drCar15[1] = "Ley MiPYME";
            drCar15[2] = null;
            dtColocacion.Rows.Add(drCar15);
            DataRow drCar16;
            drCar16 = dtColocacion.NewRow();
            drCar16[0] = "16";
            drCar16[1] = "Provisión Cartera";
            drCar16[2] = null;
            dtColocacion.Rows.Add(drCar16);
            DataRow drCar17;
            drCar17 = dtColocacion.NewRow();
            drCar17[0] = "17";
            drCar17[1] = "Provisión General";
            drCar17[2] = null;
            dtColocacion.Rows.Add(drCar17);

            // Llenar la grilla para el flujo requerido
            dtRequerido.Columns.Add("codigo", typeof(string));
            dtRequerido.Columns["codigo"].AllowDBNull = true;
            dtRequerido.Columns["codigo"].DefaultValue = " ";
            DataColumn[] dtLlaveR = new DataColumn[1];
            dtLlaveR[0] = dtRequerido.Columns[0];
            dtRequerido.PrimaryKey = dtLlaveR;
            dtRequerido.Columns.Add("descripcion", typeof(string));
            dtRequerido.Columns["descripcion"].AllowDBNull = true;
            dtRequerido.Columns["descripcion"].DefaultValue = " ";

            // Determinar el número de períodos a generar
            int nperiodos = 0;
            try { nperiodos = Convert.ToInt32(txtNumPeriodos.Text); }
            catch 
            {
                VerError("Error al realizar la conversión del número de periodos");
                return;
            }

            // Determinar la tasa promedio de colocación
            double TasaPromedio = PresupuestoServicio.TasaPromedioColocacion((Usuario)Session["Usuario"]);
            double PlazoPromedio = PresupuestoServicio.PlazoPromedioColocacion((Usuario)Session["Usuario"]);

            // Generar las columnas en la grilla y en el datatable según el número de períodos a proyectar
            gvProyeccion.AutoGenerateColumns = false;
            gvColocacion.AutoGenerateColumns = false;
            gvSaldos.AutoGenerateColumns = false;
            DateTime fechainicial;
            DateTime fechaprimerperiodo;
            DateTime fechaultimoperiodo;
            DateTime fecha = Convert.ToDateTime(new DateTime(txtPeriodoInicial.ToDateTime.Year, 1, 1));
            fechainicial = fecha;
            fecha = fecha.AddDays(31);
            fecha = new DateTime(fecha.Year, fecha.Month, 1);
            fecha = fecha.AddDays(-1);
            fechaprimerperiodo = fecha;
            for (int i = 1; i <= nperiodos; i++)
            {
                string nombreColumna = fecha.ToString("MMMM").ToLower() + " " + fecha.ToString("yyyy");
                string nombreCampo = fecha.ToString("MMM_") + fecha.ToString("yyyy");

                // Adicionar la columna a la grilla
                BoundField ColumnBoundKAP;
                ColumnBoundKAP = new BoundField();
                ColumnBoundKAP.HeaderText = nombreColumna;
                ColumnBoundKAP.DataField = nombreCampo;
                ColumnBoundKAP.DataFormatString = "{0:N}";
                ColumnBoundKAP.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                ColumnBoundKAP.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                ColumnBoundKAP.ItemStyle.Font.Size = FontUnit.XXSmall;
                ColumnBoundKAP.ItemStyle.Width = 100;
                ColumnBoundKAP.ControlStyle.Width = 100;
                ColumnBoundKAP.HeaderStyle.Width = 100;
                gvProyeccion.Columns.Add(ColumnBoundKAP);
                //gvTitulos.Columns.Add(ColumnBoundKAP);
                gvFlujo.Columns.Add(ColumnBoundKAP);
                gvColocacion.Columns.Add(ColumnBoundKAP);
                gvObligaciones.Columns.Add(ColumnBoundKAP);
                gvObligacionesNuevas.Columns.Add(ColumnBoundKAP);
                gvObligacionesPagos.Columns.Add(ColumnBoundKAP);
                gvObligacionesTot.Columns.Add(ColumnBoundKAP);
                gvObligacionesTotPagos.Columns.Add(ColumnBoundKAP);
                gvRequerido.Columns.Add(ColumnBoundKAP);

                // Adicionar la columna al datatable
                dtProy.Columns.Add(nombreCampo, typeof(Double));
                dtColocacion.Columns.Add(nombreCampo, typeof(Double));
                dtObligaciones.Columns.Add(nombreCampo, typeof(Double));
                dtObligacionesNuevas.Columns.Add(nombreCampo, typeof(Double));
                dtObligacionesPagos.Columns.Add(nombreCampo, typeof(Double));
                dtObligacionesTot.Columns.Add(nombreCampo, typeof(Double));
                dtObligacionesTotPagos.Columns.Add(nombreCampo, typeof(Double));
                dtRequerido.Columns.Add(nombreCampo, typeof(Double));

                // Registrar rango de fechas en el datatable
                DataRow drFec;
                drFec = dtFechas.NewRow();
                drFec[0] = i;
                drFec[1] = fechainicial;
                drFec[2] = fecha;
                dtFechas.Rows.Add(drFec);

                // Ir a la siguiente fecha
                fechaultimoperiodo = fecha;
                fecha = fecha.AddDays(1);
                fechainicial = fecha;
                fecha = fecha.AddDays(31);
                fecha = new DateTime(fecha.Year, fecha.Month, 1);
                fecha = fecha.AddDays(-1);
            }

            // Agregar columna de totales
            // Adicionar la columna a la grilla
            BoundField ColumnBoundTOTAL;
            ColumnBoundTOTAL = new BoundField();
            ColumnBoundTOTAL.HeaderText = "TOTAL";
            ColumnBoundTOTAL.DataField = "total";
            ColumnBoundTOTAL.DataFormatString = "{0:N}";
            ColumnBoundTOTAL.ItemStyle.HorizontalAlign = HorizontalAlign.Right;            
            ColumnBoundTOTAL.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
            ColumnBoundTOTAL.ItemStyle.Font.Size = FontUnit.XXSmall;
            ColumnBoundTOTAL.ItemStyle.Width = 100;
            ColumnBoundTOTAL.ControlStyle.Width = 100;
            ColumnBoundTOTAL.HeaderStyle.Width = 100;
            ColumnBoundTOTAL.ReadOnly = true;
            gvProyeccion.Columns.Add(ColumnBoundTOTAL);
            gvFlujo.Columns.Add(ColumnBoundTOTAL);
            gvObligacionesNuevas.Columns.Add(ColumnBoundTOTAL);
            dtObligacionesNuevas.Columns.Add("total", typeof(Double));
            dtProy.Columns.Add("total", typeof(Double));

            // Inicializar variable para número de períodos
            int numeroPeriodo = 0;

            // Inicializar la fila de colocación por ejecutivo
            DataRow drColxEje;
            drColxEje = dtColocacion.Rows.Find("3");
            numeroPeriodo = 0;
            foreach (DataRow drFecha in dtFechas.Rows)
            {
                if (drColxEje != null)
                    drColxEje[numeroPeriodo + 3] = "000000000";
                numeroPeriodo = numeroPeriodo + 1;
                drColxEje.AcceptChanges();
            }

            // Inicializar los porcentajes de mora de cartera
            for(int i=2;i <= 5;i++)
            {
                DataRow drPorcentajeCartera;
                drPorcentajeCartera = dtColocacion.Rows.Find("9-"+i);
                if (drPorcentajeCartera != null)
                {
                    numeroPeriodo = 0;
                    foreach (DataRow drFecha in dtFechas.Rows)
                    {
                        if (drPorcentajeCartera != null)
                            drPorcentajeCartera[numeroPeriodo + 3] = "0";
                        numeroPeriodo = numeroPeriodo + 1;
                    }
                    drPorcentajeCartera.AcceptChanges();
                }
            }

            // LLenar los datos de tasa promedio de colocación
            DataRow rFila0 = dtColocacion.Rows[0];
            if (rFila0 != null)
            {
                foreach (DataRow rFecha in dtFechas.Rows)
                {
                    int numerofila = (int)rFecha[0] + 2;
                    rFila0[numerofila] = TasaPromedio;
                }
            }

            // LLenar los datos de plazo promedio de colocación
            DataRow rFila1 = dtColocacion.Rows[1];
            if (rFila1 != null)
            {
                foreach (DataRow rFecha in dtFechas.Rows)
                {
                    int numerofila = (int)rFecha[0] + 2;
                    rFila1[numerofila] = PlazoPromedio;
                }
            }

            // LLenar la columna de recuperacion de capital e interes de los créditos viejos
            if (bCalcularCartera == true)
            {
                string causaPresup = ConfigurationManager.AppSettings["causaPresup"];
                if (ddlCentroCosto.SelectedValue.ToString().Trim() != "0")
                    filtro = " And c.cod_oficina In (Select x.cod_oficina From oficina x Where x.centro_costo = " + ddlCentroCosto.SelectedValue.ToString().Trim() + ") ";
                else
                    filtro = "";
                DataRow drRecuperaciones;
                drRecuperaciones = dtColocacion.Rows.Find("8-1");
                DataRow drRecuperacionesInteres;
                drRecuperacionesInteres = dtColocacion.Rows.Find("10-1");
                DataRow drCausacionInteres;
                drCausacionInteres = dtColocacion.Rows.Find("10-3");
                DataRow drLeyMiPYme;
                drLeyMiPYme = dtColocacion.Rows.Find("15-1");
                numeroPeriodo = 0;
                foreach (DataRow rFecha in dtFechas.Rows)
                {
                    decimal totRecuperacion = 0;
                    int cod_clasifica = int.MinValue;
                    fechainicial = (DateTime)rFecha[1];
                    fecha = (DateTime)rFecha[2];
                    totRecuperacion = PresupuestoServicio.ConsultarValorRecuperacion(fechahistorico, cod_clasifica, 1, fechainicial, fecha, filtro, (Usuario)Session["Usuario"]);
                    if (drRecuperaciones != null)
                        drRecuperaciones[numeroPeriodo + 3] = totRecuperacion;
                    totRecuperacion = PresupuestoServicio.ConsultarValorRecuperacion(fechahistorico, cod_clasifica, 2, fechainicial, fecha, filtro, (Usuario)Session["Usuario"]);
                    if (drRecuperacionesInteres != null)
                        drRecuperacionesInteres[numeroPeriodo + 3] = totRecuperacion;
                    if (causaPresup == "1")
                        totRecuperacion = PresupuestoServicio.ConsultarValorRecuperacionCausado(fechahistorico, cod_clasifica, 2, fechainicial, fecha, filtro, (Usuario)Session["Usuario"]);
                    if (drCausacionInteres != null)
                        drCausacionInteres[numeroPeriodo + 3] = totRecuperacion;
                    totRecuperacion = PresupuestoServicio.ConsultarValorRecuperacion(fechahistorico, cod_clasifica, 40, fechainicial, fecha, filtro, (Usuario)Session["Usuario"]);
                    if (drLeyMiPYme != null)
                        drLeyMiPYme[numeroPeriodo + 3] = totRecuperacion;
                    numeroPeriodo = numeroPeriodo + 1;
                }
            }

            // Calcular saldo promedio de las cuentas contables del presupuesto
            if (ddlCentroCosto.SelectedValue.ToString().Trim() != "0")
                filtro = " And b.centro_costo = " + ddlCentroCosto.SelectedValue.ToString().Trim();
            else
                filtro = "";
            string cod_cuenta = "";
            DateTime fecha_inicial = new System.DateTime(fechahistorico.Year, 1, 1);
            DateTime fecha_final = fechahistorico;
            gvProyeccion.Columns[6].HeaderText = "Saldo a " + fechahistorico.ToShortDateString();
            foreach (DataRow drFila in dtProy.Rows)
            {
                cod_cuenta = drFila[0].ToString().Trim();
                drFila[4] = PresupuestoServicio.SaldoPromedioCuenta(cod_cuenta, fecha_inicial, fecha_final, filtro, (Usuario)Session["Usuario"]);
                drFila[5] = PresupuestoServicio.SaldoFinalCuenta(cod_cuenta, fecha_final, filtro, (Usuario)Session["Usuario"]);
                drFila.AcceptChanges();
            }

            // Parámetros de la grilla
            gvProyeccion.Columns[0].ItemStyle.BackColor = System.Drawing.Color.LightBlue;
            gvColocacion.Columns[0].ItemStyle.BackColor = System.Drawing.Color.LightBlue;
            gvColocacion.Columns[1].ItemStyle.BackColor = System.Drawing.Color.LightBlue;
            gvColocacion.Columns[2].ItemStyle.BackColor = System.Drawing.Color.LightBlue;
            gvSaldos.Columns[4].HeaderText = "Saldo Cap. a " + fechahistorico.ToShortDateString();

            // LLenar la columna de recuperacion de capital e interes de las obligaciones viejas 
            foreach (DataRow drFila in dtObligacionesPagos.Rows)
            {
                numeroPeriodo = 0;
                if (drFila[0].ToString() != "")
                {
                    int cod_obligacion = Convert.ToInt32(drFila[0]);
                    int codcomponente = 0;
                    if (drFila[2].ToString().Contains("1-"))
                    {
                        codcomponente = 1;
                    }
                    else
                    {
                        if (drFila[2].ToString().Contains("-2-"))
                            codcomponente = -2;
                        else
                            codcomponente = 2;
                    }
                    foreach (DataRow rFecha in dtFechas.Rows)
                    {
                        decimal valPago = 0;
                        fechainicial = (DateTime)rFecha[1];
                        fecha = (DateTime)rFecha[2];
                        if (codcomponente == -2)
                        {
                            valPago = PresupuestoServicio.ConsultarValorProvisionObligacion(fechahistorico, cod_obligacion, fechainicial, fecha, (Usuario)Session["Usuario"]);                            
                        }
                        else
                        {
                            valPago = PresupuestoServicio.ConsultarValorAPagarObligacion(fechahistorico, cod_obligacion, fechainicial, fecha, codcomponente, (Usuario)Session["Usuario"]);
                        }
                        drFila[numeroPeriodo + 3] = valPago;
                        numeroPeriodo = numeroPeriodo + 1;
                    }
                }
            }

            foreach (DataRow drFila in dtObligaciones.Rows)
            {
                numeroPeriodo = 0;
                if (drFila[0].ToString() != "")
                {
                    int cod_obligacion = Convert.ToInt32(drFila[0]);
                    decimal saldoObligacion = Convert.ToDecimal(drFila[2]);
                    foreach (DataRow rFecha in dtFechas.Rows)
                    {
                        decimal valPagoCap = 0;
                        fechainicial = (DateTime)rFecha[1];
                        fecha = (DateTime)rFecha[2];
                        valPagoCap = PresupuestoServicio.ConsultarValorAPagarObligacion(fechahistorico, cod_obligacion, fechainicial, fecha, 1, (Usuario)Session["Usuario"]);
                        saldoObligacion = saldoObligacion - valPagoCap;
                        drFila[numeroPeriodo + 3] = saldoObligacion;
                        numeroPeriodo = numeroPeriodo + 1;
                    }
                }
            }

            // Cargando datos del presupuesto si se esta en una modificación
            if (Session["IDPRESUPUESTO"] != null)
            {
                Int64 idPresupuesto = Convert.ToInt64(Session["IDPRESUPUESTO"].ToString());
                if (idPresupuesto != 0)
                {
                    foreach (DataRow drFila in dtProy.Rows)
                    {
                        string cod_cuenta_pre = drFila[0].ToString();
                        numeroPeriodo = 0;
                        foreach (DataRow rFecha in dtFechas.Rows)
                        {
                            decimal valor = 0;
                            valor = PresupuestoServicio.ConsultarValorPresupuesto(idPresupuesto, numeroPeriodo + 1, cod_cuenta_pre, 0, (Usuario)Session["Usuario"]);
                            drFila[numeroPeriodo + PresupuestoServicio.GetNumeroColumnas()] = valor;
                            if (numeroPeriodo == 0)
                            {
                                decimal incremento = 0;
                                decimal saldoPromedio = 0;
                                try
                                {
                                    saldoPromedio = Convert.ToDecimal(drFila[4].ToString());
                                }
                                catch
                                {
                                    saldoPromedio = 0;
                                }
                                incremento = PresupuestoServicio.ConsultarIncrementoPresupuesto(idPresupuesto, numeroPeriodo + 1, cod_cuenta_pre, 0, (Usuario)Session["Usuario"]);
                                if (incremento == 0 || cod_cuenta_pre.Length < 6)
                                    if (saldoPromedio != 0)
                                        incremento = Math.Round(((valor - saldoPromedio) / saldoPromedio) * 100, 2);
                                    else
                                        incremento = 0;
                                drFila[6] = incremento;
                            }
                            numeroPeriodo = numeroPeriodo + 1;
                        }
                    }
                    numeroPeriodo = 0;
                    numtotalEjecutivos = 0;
                    foreach (DataRow rFecha in dtFechas.Rows)
                    {                        
                        foreach (DataRow drColocacion in dtColocacion.Rows)
                        {
                            if ((drColocacion[0].ToString() == "1" || drColocacion[0].ToString() == "2" || drColocacion[0].ToString() == "3"
                                || drColocacion[0].ToString() == "9-2" || drColocacion[0].ToString() == "9-3" || drColocacion[0].ToString() == "9-4" || drColocacion[0].ToString() == "9-5")
                                || bCalcularCartera == false)
                            {
                                decimal valor = 0;
                                valor = PresupuestoServicio.ConsultarValorPresupuestoColocacion(idPresupuesto, numeroPeriodo + 1, drColocacion[0].ToString(), 0, (Usuario)Session["Usuario"]);
                                drColocacion[numeroPeriodo + 3] = valor;
                            }

                            if (numeroPeriodo == 0)
                            {
                                if (drColocacion[0].ToString().Length >= 2)
                                {
                                    if (drColocacion[0].ToString().Substring(0, 2) == "4.")
                                    {
                                        string concepto = drColocacion[0].ToString();
                                        Int64 cod_oficina = 0;
                                        cod_oficina = Convert.ToInt64(concepto.Substring(2, concepto.Length - 2));
                                        drColocacion[2] = PresupuestoServicio.ConsultarNumeroEjecutivos(idPresupuesto, cod_oficina, (Usuario)Session["Usuario"]);
                                        numtotalEjecutivos = numtotalEjecutivos + Convert.ToInt32(drColocacion[2]);
                                    }
                                }
                            }
                            drColocacion.AcceptChanges();
                        }
                        foreach (DataRow drObligacionNueva in dtObligacionesNuevas.Rows)
                        {
                            decimal valor = 0;
                            valor = PresupuestoServicio.ConsultarValorPresupuestoObligacion(idPresupuesto, numeroPeriodo + 1, drObligacionNueva[0].ToString(), 0, (Usuario)Session["Usuario"]);
                            drObligacionNueva[numeroPeriodo + 8] = valor;
                            drObligacionNueva.AcceptChanges();
                        }
                        numeroPeriodo = numeroPeriodo + 1;
                    }                    
                    drCar5 = dtColocacion.Rows.Find("5");
                    if (drCar5 != null)
                    {
                        drCar5[2] = numtotalEjecutivos;
                        dtColocacion.AcceptChanges();
                    }
                }
            }

            // Cargando datos en la grilla           
            Session["DTPRESUPUESTO"] = dtProy;
            gvProyeccion.DataSource = dtProy;
            gvProyeccion.DataBind();
            Session["DTREQUERIDO"] = dtRequerido;
            gvRequerido.DataSource = dtRequerido;
            gvRequerido.DataBind();

            // Cargando datos en la grilla     
            Session["DTCOLOCACION"] = dtColocacion;
            gvColocacion.DataSource = dtColocacion;
            gvColocacion.DataBind();
            if (Session["IDPRESUPUESTO"] == null)
                EstiloGrillaCartera();

            // Cargando el datatable que contiene información de las fechas de cada período
            Session["DTFECHAS"] = dtFechas;

            // Cargando datos en la grilla
            Session["DTSALDOS"] = dtSaldos;
            gvSaldos.DataSource = dtSaldos;
            gvSaldos.DataBind();

            // Cargando la grila de nomina
            dtNomina.DefaultView.Sort = "COD_OFICINA ASC";
            Session["DTNOMINA"] = dtNomina;
            gvNomina.DataSource = dtNomina;
            gvNomina.DataBind();
            Session["DTCARGOS"] = dtCargos;
            gvCargos.DataSource = dtCargos;
            gvCargos.DataBind();

            // Cargando la grila de activos fijos
            Session["DTACTIVOSFIJ"] = dtActivosFij;
            gvActivosFij.DataSource = dtActivosFij;
            gvActivosFij.DataBind();

            // Cargando la grila de Tecnologia
            Session["DTTECNOLOGIA"] = dtTecnologia;
            gvTecnologia.DataSource = dtTecnologia;
            gvTecnologia.DataBind();

            // Cargando la grila de diferidos
            Session["DTDIFERIDOS"] = dtDiferidos;
            gvDiferidos.DataSource = dtDiferidos;
            gvDiferidos.DataBind();

            // Cargando la grila de diferidos
            Session["DTOTROS"] = dtOtros;
            gvOtros.DataSource = dtOtros;
            gvOtros.DataBind();

            // Cargando la grila de honorarios
            Session["DTHONORARIOS"] = dtHonorarios;
            gvHonorarios.DataSource = dtHonorarios;
            gvHonorarios.DataBind();

            // Cargando la grila de obligaciones
            Session["DTOBLIGACIONES"] = dtObligaciones;
            gvObligaciones.DataSource = dtObligaciones;
            gvObligaciones.DataBind();

            // Cargando la grila de obligaciones
            Session["DTOBLIGACIONESTOT"] = dtObligacionesTot;
            gvObligacionesTot.DataSource = dtObligacionesTot;
            gvObligacionesTot.DataBind();

            // Cargando la grila de obligaciones
            Session["DTOBLIGACIONESTOTPAGOS"] = dtObligacionesTotPagos;
            gvObligacionesTotPagos.DataSource = dtObligacionesTotPagos;
            gvObligacionesTotPagos.DataBind();

            // Cargando la grila de obligaciones
            Session["DTOBLIGACIONESNUEVAS"] = dtObligacionesNuevas;
            gvObligacionesNuevas.DataSource = dtObligacionesNuevas;
            gvObligacionesNuevas.DataBind();

            // Cargando la grila de pagos de obligaciones
            Session["DTOBLIGACIONESPAGOS"] = dtObligacionesPagos;
            gvObligacionesPagos.DataSource = dtObligacionesPagos;
            gvObligacionesPagos.DataBind();

            // Llenar grillas de totales de obligaciones
            GenerarTotalesObligaciones();


            // Si es modificación se muestra el flujo de caja
            if (Session["IDPRESUPUESTO"] != null)
            {
                if (bCalcularCartera == true)
                {
                    Totalizar_Cartera();
                    Procesar_Cartera();
                }
                //Calcular_Cartera(false);
                //Calcular_Nomina(false);
                //Calcular_Obligaciones(false);
                //Calcular_ActivosFijos(false);
                //Calcular_Tecnologia(false);
                //Calcular_Diferidos(false);
                //Calcular_Honorarios(false);
                //Calcular_Otros(true);
                dtProy = (DataTable)Session["DTPRESUPUESTO"];
                gvProyeccion.DataSource = dtProy;
                gvProyeccion.DataBind();                
                GenerarFlujo();
            }
            
            // Mostrar totales de OTROS CONCEPTOS, OBLIGACIONES y NOMINA
            Totalizar_Cartera();
            Totalizar_Otros();
            Totalizar_Honorarios();
            Totalizar_Obligaciones();
            Totalizar_Nomina();
            Totalizar_ObligacionesNuevas();
            PresupuestoServicio.TotalizarPresupuesto(ref dtProy, dtFechas);

            Site toolBar = (Site)this.Master;
            toolBar.MostrarGuardar(true);
            
        }

        mvPresupuesto.ActiveViewIndex = 1;

    }

    /// <summary>
    /// Método para colocar en BOLD algunos registros de la grilla de colocaciones
    /// </summary>
    private void EstiloGrillaCartera()
    {
        int numerofilac = 0;
        foreach (GridViewRow fila in gvColocacion.Rows)
        {
            if (gvColocacion.DataKeys[numerofilac].Values[0].ToString() == "1" || gvColocacion.DataKeys[numerofilac].Values[0].ToString() == "2" || gvColocacion.DataKeys[numerofilac].Values[0].ToString() == "3"
                || gvColocacion.DataKeys[numerofilac].Values[0].ToString() == "9-2" || gvColocacion.DataKeys[numerofilac].Values[0].ToString() == "9-3" || gvColocacion.DataKeys[numerofilac].Values[0].ToString() == "9-4" || gvColocacion.DataKeys[numerofilac].Values[0].ToString() == "9-5")
            {
                gvColocacion.Rows[numerofilac].BackColor = System.Drawing.Color.LightBlue;
            }
            else
            {
                ImageButton btnEditar = (ImageButton)fila.FindControl("btnEditar");
                if (btnEditar != null)
                    btnEditar.Visible = false;
            }
            Label lbDescripcion = new Label();
            lbDescripcion = (Label)fila.FindControl("lblDescripcion");
            if (lbDescripcion != null)
            {
                if (!lbDescripcion.Text.ToString().Contains("-"))
                {
                    gvColocacion.Rows[numerofilac].Font.Bold = true;
                }
            }
            if (lbDescripcion.Text == "Tasa de Interes Cte Promedio" || lbDescripcion.Text == "Plazo Promedio de Colocación" || lbDescripcion.Text == "Colocacion por Ejecutivo")
                gvColocacion.Rows[numerofilac].BackColor = System.Drawing.Color.LightBlue;
            numerofilac += 1;
        }
        gvColocacion.Columns[1].ItemStyle.Width = 140;
    }

    /// <summary>
    /// Método para llevar el DropDownList de Oficinas en la grilla de nomina
    /// </summary>
    /// <returns></returns>
    protected List<Xpinn.Caja.Entities.Oficina> ListaOficinas()
    {
        string filtro = "";
        if (ddlCentroCosto.SelectedValue.ToString().Trim() != "0")
            filtro = " a.centro_costo = " + ddlCentroCosto.SelectedValue.ToString().Trim() + " ";
        else
            filtro = "";
        Xpinn.Caja.Services.OficinaService oficinaServicio = new Xpinn.Caja.Services.OficinaService();
        Xpinn.Caja.Entities.Oficina eOficina = new Xpinn.Caja.Entities.Oficina();
        List<Xpinn.Caja.Entities.Oficina> LstOficina = new List<Xpinn.Caja.Entities.Oficina>();
        LstOficina = oficinaServicio.ListarOficina(eOficina, (Usuario)Session["Usuario"], filtro);
        return LstOficina;
    }

    protected List<Xpinn.FabricaCreditos.Entities.Periodicidad> ListaPeriodicidad()
    {
        Xpinn.FabricaCreditos.Services.PeriodicidadService periodicServicio = new Xpinn.FabricaCreditos.Services.PeriodicidadService();
        Xpinn.FabricaCreditos.Entities.Periodicidad ePeriodicidad = new Xpinn.FabricaCreditos.Entities.Periodicidad();
        List<Xpinn.FabricaCreditos.Entities.Periodicidad> LstPeriodicidad = new List<Xpinn.FabricaCreditos.Entities.Periodicidad>();
        LstPeriodicidad = periodicServicio.ListarPeriodicidad(ePeriodicidad, (Usuario)Session["Usuario"]);
        return LstPeriodicidad;
    }

    protected List<Xpinn.Caja.Entities.Bancos> ListaBanco()
    {
        Xpinn.Caja.Services.BancosService bancosServicio = new Xpinn.Caja.Services.BancosService();
        Xpinn.Caja.Entities.Bancos eBancos = new Xpinn.Caja.Entities.Bancos();
        List<Xpinn.Caja.Entities.Bancos> LstBancos = new List<Xpinn.Caja.Entities.Bancos>();
        LstBancos = bancosServicio.ListarBancos(eBancos, (Usuario)Session["Usuario"]);
        return LstBancos;
    }

    protected void btnAnterior_Click(object sender, ImageClickEventArgs e)
    {
        mvPresupuesto.ActiveViewIndex = 0;
    }

    /// <summary>
    /// Método para ir al presupuesto de cartera
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCartera_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        mvPresupuesto.ActiveViewIndex = 2;
    }

    protected void btnNomina_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        Site toolBar = (Site)this.Master;
        toolBar.MostrarGuardar(false);
        mvPresupuesto.ActiveViewIndex = 3;
    }

    protected void btnActivosFij_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        Site toolBar = (Site)this.Master;
        toolBar.MostrarGuardar(false);
        mpeIncremento.Hide();
        mvPresupuesto.ActiveViewIndex = 4;
    }

    protected void btnTecnologia_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        Site toolBar = (Site)this.Master;
        toolBar.MostrarGuardar(false);
        mpeIncremento.Hide();
        mvPresupuesto.ActiveViewIndex = 9;
    }

    protected void btnObligaciones_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        Site toolBar = (Site)this.Master;
        toolBar.MostrarGuardar(false);
        mvPresupuesto.ActiveViewIndex = 5;
    }

    protected void btnDiferidos_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        Site toolBar = (Site)this.Master;
        toolBar.MostrarGuardar(false);
        mvPresupuesto.ActiveViewIndex = 6;
    }

    protected void btnOtros_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        Site toolBar = (Site)this.Master;
        toolBar.MostrarGuardar(false);
        mvPresupuesto.ActiveViewIndex = 7;
    }

    #region Presupuesto

    protected void gvProyeccion_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        string cod_cuenta = e.Row.Cells[1].Text;
        if (cod_cuenta.Length != 6)
        {
            ImageButton btnEditar = (ImageButton)e.Row.FindControl("btnEditar");
            if (btnEditar != null)
                btnEditar.Visible = false;
        }

        // deshabilitar las cuentas que se generan por presupuestos individuales
        if (PresupuestoServicio.EsParametroCuenta(cod_cuenta, (Usuario)Session["Usuario"]) == true || cod_cuenta.Length != 6)
        {
            ImageButton btnEditar = (ImageButton)e.Row.FindControl("btnEditar");
            if (btnEditar != null)
            {
                btnEditar.Enabled = false;
                btnEditar.ImageUrl = "~/Images/check.jpg";
            }
            ImageButton btnModificar = (ImageButton)e.Row.FindControl("btnModificar");
            if (btnModificar != null)
            {
                btnModificar.Enabled = false;
                btnModificar.Visible = false;
            }
        }
    }

    protected void gvProyeccion_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        VerError("");

        if (e.CommandName == "Select")
        {

            string cod_cuenta = gvProyeccion.Rows[Convert.ToInt32(e.CommandArgument)].Cells[1].Text;
            if (cod_cuenta.Length != 6)
            {
                return;
            }

            mpeNuevo.Show();

            int IndiceDetalle = Convert.ToInt32(e.CommandArgument);
            Session[PresupuestoServicio.CodigoPrograma + ".indice"] = IndiceDetalle;
            if (IndiceDetalle >= 0)
            {
                GridViewRow drRow = gvProyeccion.Rows[IndiceDetalle];

                if (drRow.Cells[1] != null && drRow.Cells[1].ToString() != "")
                {
                    lblcodcuenta.Text = drRow.Cells[1].Text;
                }
                if (drRow.Cells[2] != null && drRow.Cells[2].ToString() != "")
                {
                    lblNomCuenta.Text = drRow.Cells[2].Text;
                }
                if (drRow.Cells[5] != null && drRow.Cells[5].ToString() != "")
                {
                    lblSaldoPromedio.Text = drRow.Cells[5].Text;
                }
                if (drRow.Cells[6] != null && drRow.Cells[6].ToString() != "")
                {
                    lblSaldoCuenta.Text = drRow.Cells[6].Text;
                }
                DateTime fecha_corte = DateTime.Now;
                fecha_corte = Convert.ToDateTime(txtFechaCorte.ToDateTime);
                lblSaldoPeriodo.Text = PresupuestoServicio.SaldoPeriodoCuenta(lblcodcuenta.Text, fecha_corte, "", (Usuario)Session["Usuario"]).ToString("N2");

                DataTable dtDatos = new DataTable();
                dtDatos.Columns.Add("fecha", typeof(DateTime));
                dtDatos.Columns["fecha"].AllowDBNull = true;
                dtDatos.Columns["fecha"].DefaultValue = "01/01/2000";
                dtDatos.Columns.Add("valor_presupuestado", typeof(double));
                dtDatos.Columns["valor_presupuestado"].AllowDBNull = true;
                dtDatos.Columns["valor_presupuestado"].DefaultValue = "0";
                DataTable dtFechas = new DataTable();
                dtFechas = (DataTable)Session["DTFECHAS"];
                int numPeriodo = 0;
                foreach (DataRow drFecha in dtFechas.Rows)
                {
                    DataRow drDatos = dtDatos.NewRow();
                    drDatos[0] = drFecha[2];
                    try { drDatos[1] = Convert.ToDouble(drRow.Cells[numPeriodo + 8].Text); } catch { drDatos[1] = 0; }
                    dtDatos.Rows.Add(drDatos);
                    dtDatos.AcceptChanges();
                    numPeriodo += 1;
                }
                gvDatos.DataSource = dtDatos;
                gvDatos.DataBind();
            }
        }
    }

    protected void gvProyeccion_RowEditing(object sender, GridViewEditEventArgs e)
    {
        // Determinando el código de la cuenta
        int conseID = 0;
        if (gvProyeccion.DataKeys[e.NewEditIndex] != null)
        {
            try
            {
                conseID = Convert.ToInt32(gvProyeccion.DataKeys[e.NewEditIndex].Values[0].ToString());
            }
            catch
            {
                conseID = 0;
            }
        }
        // Colocando la fila que se va a modificar         
        gvProyeccion.EditIndex = 0;
        gvProyeccion.SelectedIndex = 0;
        gvProyeccion.Height = 30;
        // Buscando datos del cargo
        DataTable dtPresupuesto = new DataTable();
        dtPresupuesto = (DataTable)Session["DTPRESUPUESTO"];
        DataTable dtPresupuestoEdit = new DataTable();
        dtPresupuestoEdit = dtPresupuesto.Clone();
        dtPresupuestoEdit.ImportRow(dtPresupuesto.Rows[e.NewEditIndex]);
        gvProyeccion.DataSource = dtPresupuestoEdit;
        try
        {
            gvProyeccion.DataBind();
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
        e.NewEditIndex = 0;
        btnExpPresupuesto.Visible = false;
    }

    protected void gvProyeccion_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvProyeccion.EditIndex = -1;
        gvProyeccion.ShowFooter = true;
        gvProyeccion.DataSource = Session["DTPRESUPUESTO"];
        gvProyeccion.DataBind();
        btnExpPresupuesto.Visible = true;
    }

    protected void gvProyeccion_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        // Cargando los datatable
        DataTable dtPresupuesto = new DataTable();
        dtPresupuesto = (DataTable)Session["DTPRESUPUESTO"];

        // Determinando el número de fila y actualizando el datatable con los valores
        string llave = "";
        llave = e.Keys[0].ToString();
        DataRow drFila = dtPresupuesto.Rows.Find(llave);
        string valor = "";
        for (var index = 0  ; index < e.NewValues.Count; index++)
        {
            if (e.NewValues[index] != null)
            {
                valor = e.NewValues[index].ToString();
                if (drFila != null)
                {
                    drFila[index+7] = valor;
                    drFila.AcceptChanges();
                }
            }
        }
        gvProyeccion.EditIndex = -1;

        // Mayorizando el presupuesto
        DataTable dtFechas = new DataTable();
        dtFechas = (DataTable)Session["DTFECHAS"];
        PresupuestoServicio.MayorizarPresupuesto(ref dtPresupuesto, dtFechas, (Usuario)Session["Usuario"], 4);

        // Actualizando la grilla
        gvProyeccion.DataSource = dtPresupuesto;
        gvProyeccion.DataBind();

        // Actualizando variable de sesión
        Session["DTPRESUPUESTO"] = dtPresupuesto;
    }

    protected void btnPresupuestar_Click(object sender, EventArgs e)
    {

        int IndiceDetalle = 0;

        if (Session[PresupuestoServicio.CodigoPrograma + ".indice"] != null)
        {
            IndiceDetalle = Convert.ToInt16(Session[PresupuestoServicio.CodigoPrograma + ".indice"]);
            Session.Remove(PresupuestoServicio.CodigoPrograma + ".indice");
        }

        DataTable dtPresupuesto = new DataTable();
        dtPresupuesto = (DataTable)Session["DTPRESUPUESTO"];
        DataTable dtFechas = new DataTable();
        dtFechas = (DataTable)Session["DTFECHAS"];

        if (IndiceDetalle >= 0 || (IndiceDetalle < 0 && dtPresupuesto.Rows.Count == 1))
        {
            if (txtValor.Text == "")
                txtValor.Text = "0";
            double valor = 0;
            double tipo = 1;
            try
            {
                valor = Convert.ToDouble(txtValor.Text);
                tipo = Convert.ToDouble(ddlTipo.SelectedValue);
            }
            catch (Exception ex)
            {
                VerError("Error al convertir el valor " + ex.Message);
            }

            DataRow drFila = dtPresupuesto.Rows[IndiceDetalle];
            if (tipo == 2 && drFila != null)
                drFila[6] = valor;
            int numeroPeriodo = 0;
            foreach(DataRow drFecha in dtFechas.Rows)
            {
                if (tipo == 1)
                {
                    drFila[numeroPeriodo + PresupuestoServicio.GetNumeroColumnas()] = valor.ToString();
                }
                else if (tipo == 2)
                {
                    double saldo_base = 0;
                    if (cbSaldoPromedio.Checked == true)
                    {
                        if (drFila[4].ToString() != "")
                            saldo_base = Convert.ToDouble(drFila[4].ToString());
                    }
                    if (cbSaldoCuenta.Checked == true)
                    {
                        if (drFila[5].ToString() != "")
                            saldo_base = Convert.ToDouble(drFila[5].ToString());
                    }
                    if (cbSaldoPeriodo.Checked == true)
                    {
                        try
                        {
                            Configuracion conf = new Configuracion();
                            saldo_base = Convert.ToDouble(lblSaldoPeriodo.Text.Replace(conf.ObtenerSeparadorMilesConfig(), ""));
                        }
                        catch
                        {
                            VerError("");
                        }
                    }
                    drFila[numeroPeriodo + PresupuestoServicio.GetNumeroColumnas()] = Math.Round(saldo_base + ((valor / 100) * saldo_base));
                }
                else if (tipo == 3)
                {
                    double valor_manual = 0;
                    GridViewRow gvrDatos = gvDatos.Rows[numeroPeriodo];
                    TextBox txtValorPresupuesto = (TextBox)gvrDatos.FindControl("txtValorPresupuesto");
                    if (txtValorPresupuesto != null)
                        if (txtValorPresupuesto.Text != "")
                            valor_manual = Convert.ToDouble(txtValorPresupuesto.Text);
                    drFila[numeroPeriodo + PresupuestoServicio.GetNumeroColumnas()] = valor_manual;
                }
                numeroPeriodo = numeroPeriodo + 1;
            }
            drFila.AcceptChanges();
        }

        PresupuestoServicio.MayorizarPresupuesto(ref dtPresupuesto, dtFechas, (Usuario)Session["Usuario"], 4);
        gvProyeccion.DataSource = dtPresupuesto;
        gvProyeccion.DataBind();
        gvProyeccion.Rows[IndiceDetalle].Font.Bold = true;
        gvProyeccion.Rows[IndiceDetalle].ForeColor = System.Drawing.Color.DarkGreen; 
        //CalcularTotal();
        Session["DTPRESUPUESTO"] = dtPresupuesto;
        mpeNuevo.Hide();
        mpeIncremento.Hide();

    }

    protected void btnGenerar_Click(object sender, EventArgs e)
    {

        int IndiceDetalle = 0;

        if (Session[PresupuestoServicio.CodigoPrograma + ".indice"] != null)
        {
            IndiceDetalle = Convert.ToInt16(Session[PresupuestoServicio.CodigoPrograma + ".indice"]);
            Session.Remove(PresupuestoServicio.CodigoPrograma + ".indice");
        }

        DataTable dtPresupuesto = new DataTable();
        dtPresupuesto = (DataTable)Session["DTPRESUPUESTO"];
        DataTable dtFechas = new DataTable();
        dtFechas = (DataTable)Session["DTFECHAS"];

        if (IndiceDetalle >= 0 || (IndiceDetalle < 0 && dtPresupuesto.Rows.Count == 1))
        {
            double valor = 0;
            double tipo = 1;
            try
            {
                valor = Convert.ToDouble(txtValor.Text);
                tipo = Convert.ToDouble(ddlTipo.SelectedValue);
            }
            catch (Exception ex)
            {
                VerError("Error al convertir el valor " + ex.Message);
            }

            DataRow drFila = dtPresupuesto.Rows[IndiceDetalle];
            if (tipo == 2 && drFila != null)
                drFila[6] = valor;
            int numeroPeriodo = 0;
            foreach (DataRow drFecha in dtFechas.Rows)
            {
                if (tipo == 1)
                {
                    drFila[numeroPeriodo + PresupuestoServicio.GetNumeroColumnas()] = valor.ToString();
                }
                else
                {
                    double saldo_base = 0;
                    if (cbSaldoPromedio.Checked == true)
                    {
                        if (drFila[4].ToString() != "")
                            saldo_base = Convert.ToDouble(drFila[4].ToString());
                    }
                    else
                    {
                        if (drFila[5].ToString() != "")
                            saldo_base = Convert.ToDouble(drFila[5].ToString());
                    }
                    drFila[numeroPeriodo + PresupuestoServicio.GetNumeroColumnas()] = Math.Round(saldo_base + ((valor / 100) * saldo_base));
                }
                numeroPeriodo = numeroPeriodo + 1;
            }
            drFila.AcceptChanges();
        }

        PresupuestoServicio.MayorizarPresupuesto(ref dtPresupuesto, dtFechas, (Usuario)Session["Usuario"], 4);
        gvProyeccion.DataSource = dtPresupuesto;
        gvProyeccion.DataBind();
        gvProyeccion.Rows[IndiceDetalle].Font.Bold = true;
        gvProyeccion.Rows[IndiceDetalle].ForeColor = System.Drawing.Color.DarkGreen;
        //CalcularTotal();
        Session["DTPRESUPUESTO"] = dtPresupuesto;
        mpeNuevo.Hide();

    }

    protected void btnCerrar_Click(object sender, EventArgs e)
    {
        mpeNuevo.Hide();
        Session.Remove(PresupuestoServicio.CodigoPrograma + ".indice");
    }

    protected void txtFlujoInicial_TextChanged(object sender, EventArgs e)
    {
        GenerarFlujo();
    }

    protected void gvFlujo_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        string cod_cuenta = e.Row.Cells[1].Text;
        if (cod_cuenta.Length != 6)
        {
            ImageButton btnEditar = (ImageButton)e.Row.FindControl("btnEditar");
            if (btnEditar != null)
                btnEditar.Visible = false;
        }

        // deshabilitar las cuentas que se generan por presupuestos individuales
        if (PresupuestoServicio.EsParametroCuenta(cod_cuenta, (Usuario)Session["Usuario"]) == true)
        {
            ImageButton btnEditar = (ImageButton)e.Row.FindControl("btnEditar");
            if (btnEditar != null)
            {
                btnEditar.Enabled = false;
                btnEditar.ImageUrl = "~/Images/check.jpg";
            }
        }
    }

    public void GenerarFlujo()
    {
        DataTable dtColocacion = new DataTable();
        dtColocacion = (DataTable)Session["DTCOLOCACION"];
        DataTable dtPresupuesto = new DataTable();
        dtPresupuesto = (DataTable)Session["DTPRESUPUESTO"];
        DataTable dtFlujo = new DataTable();
        dtFlujo = dtPresupuesto.Copy();
        DataTable dtFechas = new DataTable();
        dtFechas = (DataTable)Session["DTFECHAS"];
        DataTable dtObligaciones = new DataTable();
        dtObligaciones = (DataTable)Session["DTOBLIGACIONES"];
        DataTable dtObligacionesPagos = new DataTable();
        dtObligacionesPagos = (DataTable)Session["DTOBLIGACIONESPAGOS"];
        DataTable dtObligacionesNuevas = new DataTable();
        dtObligacionesNuevas = (DataTable)Session["DTOBLIGACIONESNUEVAS"];
        DataTable dtTecnologia = new DataTable();
        dtTecnologia = (DataTable)Session["DTTECNOLOGIA"];

        double valorFlujoInicial = 0;
        Configuracion conf = new Configuracion();
        try
        {
            valorFlujoInicial = Convert.ToDouble(txtFlujoInicial.Text.Replace(conf.ObtenerSeparadorMilesConfig(), ""));
        }
        catch
        {
            valorFlujoInicial = 0;
        }

        dtFlujo = PresupuestoServicio.GenerarFlujoCaja(dtFlujo, dtFechas, dtColocacion, dtObligaciones, dtObligacionesPagos, dtObligacionesNuevas, dtTecnologia, valorFlujoInicial, (Usuario)Session["Usuario"], 4);
        PresupuestoServicio.TotalizarPresupuesto(ref dtFlujo, dtFechas);
        gvFlujo.DataSource = dtFlujo;
        gvFlujo.DataBind();
        Session["DTFLUJO"] = dtFlujo;

        gvProyeccion.DataSource = dtPresupuesto;
        gvProyeccion.DataBind();

        // Mostrar el flujo requerido en la grilla en donde estan las obligaciones
        DataTable dtRequerido = new DataTable();
        dtRequerido = (DataTable)Session["DTREQUERIDO"];
        dtRequerido.Clear();
        DataRow drFondeo = dtFlujo.Rows.Find("17");
        DataRow drFila = dtRequerido.NewRow();
        drFila[0] = drFondeo[0];
        drFila[1] = drFondeo[1];
        int numeroPeriodo = 0;
        foreach (DataRow drFecha in dtFechas.Rows)
        {
            double valor = 0;
            if (drFondeo[numeroPeriodo + 7].ToString() != "")
                valor = Convert.ToDouble(drFondeo[numeroPeriodo + 7]);
            drFila[numeroPeriodo + 2] = valor;
            numeroPeriodo += 1;
        }
        dtRequerido.Rows.Add(drFila);
        gvRequerido.DataSource = dtRequerido;
        gvRequerido.DataBind();
    }

    #endregion Presupuesto

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //  PRESUPUESTO DE CARTERA
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    #region Cartera

    protected void btnRegresarCAR_Click(object sender, ImageClickEventArgs e)
    {
        Calcular_Cartera(true);
        GenerarFlujo();
        // Ir a la pantalla de detalle del presupuesto
        mvPresupuesto.ActiveViewIndex = 1;
    }

    protected void gvColocacion_RowEditing(object sender, GridViewEditEventArgs e)
    {
        string conseID = "0";
        if (gvColocacion.DataKeys[e.NewEditIndex] != null)
        {
            conseID = gvColocacion.DataKeys[e.NewEditIndex].Values[0].ToString();
        }
        if (conseID == "1" || conseID == "2" || conseID == "3" || conseID == "9-2" || conseID == "9-3" || conseID == "9-4" || conseID == "9-5")
        {
            gvColocacion.EditIndex = e.NewEditIndex;
            gvColocacion.DataSource = Session["DTCOLOCACION"];
            gvColocacion.DataBind();
            EstiloGrillaCartera();
        }
        else
        {
            e.Cancel = true;
        }
    }

    protected void gvColocacion_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string sConcepto = "";
            if (gvColocacion.DataKeys[e.Row.DataItemIndex] != null)
            {
                sConcepto = gvColocacion.DataKeys[e.Row.DataItemIndex].Values[0].ToString();
            }
            if (sConcepto.Length >= 2)
            {
                if (sConcepto.Substring(0, 2) == "4.")
                {
                    Label lblNumEjecutivos = (Label)e.Row.Cells[2].FindControl("lblNumEjecutivos");
                    TextBox txtNumEjecutivos = (TextBox)e.Row.Cells[2].FindControl("txtNumEjecutivos");
                    if (txtNumEjecutivos != null)
                    {
                        txtNumEjecutivos.Visible = true;
                        lblNumEjecutivos.Visible = false;
                        txtNumEjecutivos.TextChanged += txtNumEjecutivos_TextChanged;
                    }
                }
            }
        }
    }

    protected void gvColocacion_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvColocacion.EditIndex = -1;
        gvColocacion.DataSource = Session["DTCOLOCACION"];
        gvColocacion.DataBind();
        EstiloGrillaCartera();
    }

    protected void gvColocacion_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            // Cargando los datatable
            DataTable dtColocacion = new DataTable();
            dtColocacion = (DataTable)Session["DTCOLOCACION"];

            // Determinando el número de fila y actualizando el datatable con los valores
            string llave = "";
            llave = e.Keys[0].ToString();
            DataRow drFila = dtColocacion.Rows.Find(llave);
            string valor = "";
            for (var index = 0; index < e.NewValues.Count; index++)
            {
                if (e.NewValues[index] != null)
                {
                    valor = e.NewValues[index].ToString();
                    if (drFila != null)
                    {
                        drFila[index + 1] = valor;
                        drFila.AcceptChanges();
                    }
                }
            }
            gvColocacion.EditIndex = -1;
            gvColocacion.DataSource = dtColocacion;
            gvColocacion.DataBind();

            // Actualizando variable de sesión
            Session["DTCOLOCACION"] = dtColocacion;

            // Actualizando los calculos y refrescando las grillas  
            Procesar_Cartera();            
        }
        catch
        {
            // mensaje de error
        }
    }

    protected void txtValorPorCredito_TextChanged(object sender, EventArgs e)
    {
        try
        {
            Procesar_Cartera();
        }
        catch
        {
            // mensaje de error
        }
    }

    protected void txtPorPoliza_TextChanged(object sender, EventArgs e)
    {
        try
        {
            Configuracion conf = new Configuracion();
            decimal PorPoliza = Convert.ToDecimal(txtPorPoliza.Text.Replace(conf.ObtenerSeparadorMilesConfig(), ""));
            txtPorPoliza.Text = String.Format("{0:N2}", PorPoliza);
            Procesar_Cartera();
        }
        catch
        {
            VerError("Error en el porcentaje de polizas vendidas");
        }
    }

    protected void txtValPoliza_TextChanged(object sender, EventArgs e)
    {
        try
        {
            Procesar_Cartera();
        }
        catch
        {
            // mensaje de error
        }
    }

    protected void txtComision_TextChanged(object sender, EventArgs e)
    {
        try
        {
            Configuracion conf = new Configuracion();
            decimal Comision = Convert.ToDecimal(txtComision.Text.Replace(conf.ObtenerSeparadorMilesConfig(), ""));
            txtComision.Text = String.Format("{0:N2}", Comision);
            Procesar_Cartera();
        }
        catch
        {
            // mensaje de error
        }
    }

    protected void txtLeyMiPYME_TextChanged(object sender, EventArgs e)
    {
        try
        {
            Configuracion conf = new Configuracion();
            decimal LeyMiPYME = Convert.ToDecimal(txtLeyMiPYME.Text.Replace(conf.ObtenerSeparadorMilesConfig(), ""));
            txtLeyMiPYME.Text = String.Format("{0:N2}", LeyMiPYME);
            Procesar_Cartera();
        }
        catch
        {
            // mensaje de error
        }
    }

    protected void txtPorProvision_TextChanged(object sender, EventArgs e)
    {
        try
        {
            Configuracion conf = new Configuracion();
            decimal PorProvision = Convert.ToDecimal(txtPorProvision.Text.Replace(conf.ObtenerSeparadorMilesConfig(), ""));
            txtPorProvision.Text = String.Format("{0:N2}", PorProvision);
            Procesar_Cartera();
        }
        catch
        {
            VerError("Error en el porcentaje de provisión individual ingresado");
        }
    }

    protected void txtPorProvisionGeneral_TextChanged(object sender, EventArgs e)
    {
        try
        {
            Configuracion conf = new Configuracion();
            decimal PorProvisionGeneral = Convert.ToDecimal(txtPorProvisionGeneral.Text.Replace(conf.ObtenerSeparadorMilesConfig(), ""));
            txtPorProvisionGeneral.Text = String.Format("{0:N2}", PorProvisionGeneral);
            Procesar_Cartera();
        }
        catch
        {
            VerError("Error en el porcentaje de provisión general ingresado");
        }
    }

    protected void txtNumEjecutivos_TextChanged(object sender, EventArgs e)
    {
        DataTable dtColocacion = new DataTable();
        dtColocacion = (DataTable)Session["DTCOLOCACION"];
        Int64 totEjecutivos = 0;
        foreach (GridViewRow grColoca in gvColocacion.Rows)
        {
            string sConcepto = gvColocacion.DataKeys[grColoca.DataItemIndex].Values[0].ToString();
            if (sConcepto.Length >= 2)
            {
                if (sConcepto.Substring(0, 2) == "4.")
                {
                    Int64 numEjecutivos = 0;
                    TextBox txtNumEjecutivos = (TextBox)grColoca.Cells[2].FindControl("txtNumEjecutivos");
                    if (txtNumEjecutivos != null)
                    {
                        try
                        {
                            numEjecutivos = Convert.ToInt64(txtNumEjecutivos.Text);
                        }
                        catch
                        {
                            txtNumEjecutivos.Text = "0";
                            numEjecutivos = 0;
                        }
                        totEjecutivos = totEjecutivos + numEjecutivos;
                        if (dtColocacion != null)
                        {
                            DataRow drFila = dtColocacion.Rows.Find(sConcepto);
                            if (drFila != null)
                            {
                                drFila[2] = numEjecutivos;
                                drFila.AcceptChanges();
                            }
                        }
                    }
                }
            }
        }
        if (dtColocacion != null)
        {
            DataRow drTotEje = dtColocacion.Rows.Find("5");
            drTotEje[2] = totEjecutivos;
            drTotEje.AcceptChanges();
            Session["DTCOLOCACION"] = dtColocacion;
            gvColocacion.DataSource = dtColocacion;
            gvColocacion.DataBind();
            Procesar_Cartera();
        }
    }

    public void Procesar_Cartera()
    {
        // Cargando los datatable
        DataTable dtColocacion = new DataTable();
        dtColocacion = (DataTable)Session["DTCOLOCACION"];
        DataTable dtFechas = new DataTable();
        dtFechas = (DataTable)Session["DTFECHAS"];

        // Determinando configuracion
        Configuracion conf = new Configuracion();

        // Determinando saldo actual de cartera      
        double valorActualCartera = 0;
        GridViewRow rFilaS = gvSaldos.FooterRow;
        if (rFilaS != null)
            try { valorActualCartera = Convert.ToDouble(rFilaS.Cells[4].Text.Replace(conf.ObtenerSeparadorMilesConfig(), "")); }
            catch { VerError("Error al determinar el saldo actual de cartera"); }

        // Determinar el valor o monto por cada crédito
        double VrPorCredito = 0;
        try { VrPorCredito = Convert.ToDouble(txtValorPorCredito.Text); }
        catch { VrPorCredito = 0;  VerError("Error al determinar el valor por crédito"); }

        // Determinar el valor de polizas
        double porPolizasVencidas = 0; 
        double valorUnitPoliza = 0; 
        double comisionPoliza = 0;
        double porLeyMiPyme = 0;
        double porProvision = 0;
        double porProvisionGen = 0;
        try { 
            porPolizasVencidas = Convert.ToDouble(txtPorPoliza.Text)/100;
            valorUnitPoliza = Convert.ToDouble(txtValPoliza.Text);
            comisionPoliza = Convert.ToDouble(txtComision.Text)/100;
            porLeyMiPyme = Convert.ToDouble(txtLeyMiPYME.Text)/100;
            porProvision = Convert.ToDouble(txtPorProvision.Text) / 100;
            porProvisionGen = Convert.ToDouble(txtPorProvisionGeneral.Text) / 100;
        }
        catch { VerError("Error al determinar el valor de polizas"); }

        // Actualizando los calculos y refrescando las grillas  
        PresupuestoServicio.CalcularPresupuestoCartera(ref dtColocacion, dtFechas, VrPorCredito, valorActualCartera, porPolizasVencidas, valorUnitPoliza, comisionPoliza, porLeyMiPyme, porProvision, porProvisionGen);        
        Session["DTCOLOCACION"] = dtColocacion;
        gvColocacion.DataSource = dtColocacion;
        gvColocacion.DataBind();           
        EstiloGrillaCartera();

    }

    public void Calcular_Cartera(Boolean bMayorizar)
    {
        // Cargar los datatable
        DataTable dtColocacion = new DataTable();
        dtColocacion = (DataTable)Session["DTCOLOCACION"];
        DataTable dtPresupuesto = new DataTable();
        dtPresupuesto = (DataTable)Session["DTPRESUPUESTO"];
        DataTable dtFechas = new DataTable();
        dtFechas = (DataTable)Session["DTFECHAS"];

        // Cargar valores al presupuesto
        PresupuestoServicio.CargarPresupuestoCartera(ref dtPresupuesto, dtColocacion, dtFechas, bMayorizar, (Usuario)Session["Usuario"], 4);
        Session["DTPRESUPUESTO"] = dtPresupuesto;
        gvProyeccion.DataSource = dtPresupuesto;
        gvProyeccion.DataBind();
    }

    public void Totalizar_Cartera()
    {
        DataTable dtSaldos = new DataTable();
        dtSaldos = (DataTable)Session["DTSALDOS"];
        // Calcular los totales de saldos
        GridViewRow rFilaS = gvSaldos.FooterRow;
        if (rFilaS != null)
        {
            rFilaS.Cells[0].Text = "TOTALES";
            rFilaS.Font.Bold = true;
            Double TotalSaldo = 0;
            foreach (DataRow rValor in dtSaldos.Rows)
            {
                TotalSaldo = TotalSaldo + Convert.ToDouble(rValor[4]);
            }
            rFilaS.Cells[4].Text = TotalSaldo.ToString("N2");
        }
    }

    #endregion Cartera

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //  PRESUPUESTO DE NOMINA
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    #region Nomina

    protected void btnRegresarNOM_Click(object sender, ImageClickEventArgs e)
    {
        Calcular_Nomina(true);
        GenerarFlujo();
        Site toolBar = (Site)this.Master;
        toolBar.MostrarGuardar(true);

        mvPresupuesto.ActiveViewIndex = 1;
    }

    protected void gvNomina_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblIncremento = (Label)e.Row.FindControl("lblIncremento");
                if (lblIncremento != null)
                    lblIncremento.Attributes.CssStyle.Add("TEXT-ALIGN", "right");

                Label lblSalario = (Label)e.Row.FindControl("lblSalario");
                if (lblSalario != null)
                    lblSalario.Attributes.CssStyle.Add("TEXT-ALIGN", "right");

                Label lblCumplimiento = (Label)e.Row.FindControl("lblCumplimiento");
                if (lblCumplimiento != null)
                    lblCumplimiento.Attributes.CssStyle.Add("TEXT-ALIGN", "right");

            }

            if (e.Row.RowType == DataControlRowType.Footer)
            {                
                Control ctrl = e.Row.FindControl("txtIncrementoF");
                if (ctrl != null)
                {
                    TextBox txtIncrementoF = (TextBox)ctrl;
                    txtIncrementoF.Text = "0";
                    txtIncrementoF.Attributes.CssStyle.Add("TEXT-ALIGN", "right");
                }

                TextBox txtSalarioF = (TextBox)e.Row.FindControl("txtSalarioF");
                txtSalarioF.Attributes.CssStyle.Add("TEXT-ALIGN", "right");

                TextBox txtCumplimientoF = (TextBox)e.Row.FindControl("txtCumplimientoF");
                txtCumplimientoF.Attributes.CssStyle.Add("TEXT-ALIGN", "right");
            }
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }

    protected void gvNomina_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            DataTable dtNomina = new DataTable();
            dtNomina = (DataTable)Session["DTNOMINA"];

            if ((e.RowIndex == 0) && (dtNomina.Rows[0][0] != null || dtNomina.Rows[0][0].ToString() == "") && (dtNomina.Rows.Count == 1))
            {
                dtNomina.Rows.Add();
            }

            Label lblCodigo = (Label)gvNomina.Rows[e.RowIndex].FindControl("lblCodigo");            
            Xpinn.Presupuesto.Entities.Nomina eNomina = new Xpinn.Presupuesto.Entities.Nomina();
            eNomina.codigo = Convert.ToInt64(lblCodigo.Text);
            PresupuestoServicio.EliminarEmpleado(eNomina, (Usuario)Session["Usuario"]);

            dtNomina.Rows[e.RowIndex].Delete();
            dtNomina.DefaultView.Sort = "COD_OFICINA ASC";
            gvNomina.DataSource = dtNomina;
            gvNomina.DataBind();
            Session["DTNOMINA"] = dtNomina;
            Totalizar_Nomina();

            if ((e.RowIndex == 0) && (dtNomina.Rows[0][0] == null || dtNomina.Rows[0][0].ToString() == "") && (dtNomina.Rows.Count == 1))
                gvNomina.Rows[0].Visible = false;

        }
        catch (Xpinn.Util.ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(PresupuestoServicio.GetType().Name + "L", "gvComponente_RowDeleting", ex);
        }
    }

    protected void gvNomina_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("AddNew"))
        {
            TextBox txtCodigoF = (TextBox)gvNomina.FooterRow.FindControl("txtCodigoF");
            TextBox txtNombreF = (TextBox)gvNomina.FooterRow.FindControl("txtNombreF");
            TextBox txtFechaIngresoF = (TextBox)gvNomina.FooterRow.FindControl("txtFechaIngresoF");
            DropDownList ddlCodOficinaF = (DropDownList)gvNomina.FooterRow.FindControl("ddlCodOficinaF");
            TextBox txtSalarioF = (TextBox)gvNomina.FooterRow.FindControl("txtSalarioF");
            TextBox txtIncrementoF = (TextBox)gvNomina.FooterRow.FindControl("txtIncrementoF");
            TextBox txtCumplimientoF = (TextBox)gvNomina.FooterRow.FindControl("txtCumplimientoF");
            DropDownList ddlCargoF = (DropDownList)gvNomina.FooterRow.FindControl("ddlCargoF");
            DropDownList ddlTipoSalarioF = (DropDownList)gvNomina.FooterRow.FindControl("ddlTipoSalarioF");
           
            DataTable dtNomina = new DataTable();
            dtNomina = (DataTable)Session["DTNOMINA"];

            if (dtNomina.Rows[0][0] == null || dtNomina.Rows[0][0].ToString() == "")
            {
                dtNomina.Rows[0].Delete();
            }

            if (txtCumplimientoF.Text == "")
                txtCumplimientoF.Text = "0";

            DataRow fila = dtNomina.NewRow();
            fila["CODIGO"] = txtCodigoF.Text;
            fila["NOMBRE"] = txtNombreF.Text;
            try
            {
                fila["FECHA_INGRESO"] = txtFechaIngresoF.Text;
            }
            catch (Exception ex)
            {
                VerError("Debe ingresar la fecha de ingreso " + ex.Message);
                return;
            }

            fila["COD_OFICINA"] = Convert.ToInt64(ddlCodOficinaF.SelectedValue);
            fila["SALARIO"] = Convert.ToDecimal(txtSalarioF.Text.Replace(gSeparadorMiles, ""));
            fila["INCREMENTO"] = Convert.ToDecimal(txtIncrementoF.Text.Replace(gSeparadorMiles, ""));
            fila["CUMPLIMIENTO"] = Convert.ToDecimal(txtCumplimientoF.Text.Replace(gSeparadorMiles, ""));
            fila["OFICINA"] = ddlCodOficinaF.SelectedItem;
            fila["CARGO"] = Convert.ToInt64(ddlCargoF.SelectedValue);            
            fila["NOM_CARGO"] = ddlCargoF.SelectedItem;
            fila["TIPO_SALARIO"] = Convert.ToInt64(ddlTipoSalarioF.SelectedValue);
            fila["NOM_TIPO_SALARIO"] = ddlTipoSalarioF.SelectedItem;

            Xpinn.Presupuesto.Entities.Nomina eNomina = new Xpinn.Presupuesto.Entities.Nomina();
            eNomina.codigo = Convert.ToInt64(txtCodigoF.Text);
            eNomina.nombre = txtNombreF.Text.ToUpper();
            eNomina.fecha_ingreso = Convert.ToDateTime(txtFechaIngresoF.Text);
            eNomina.cod_oficina = Convert.ToInt64(ddlCodOficinaF.SelectedValue);
            eNomina.salario = Convert.ToDecimal(txtSalarioF.Text.Replace(gSeparadorMiles, ""));
            eNomina.cargo = Convert.ToInt64(ddlCargoF.SelectedValue); 
            eNomina.incremento = Convert.ToDecimal(txtIncrementoF.Text.Replace(gSeparadorMiles, ""));
            eNomina.cumplimiento = Convert.ToDecimal(txtCumplimientoF.Text.Replace(gSeparadorMiles, ""));
            eNomina.tipo_salario = Convert.ToInt64(ddlCargoF.SelectedValue);
            eNomina = PresupuestoServicio.CrearEmpleado(eNomina, (Usuario)Session["Usuario"]);

            fila[7] = eNomina.salario_nuevo;
            fila[8] = eNomina.aux_trans;
            fila[10] = eNomina.comisiones;
            fila[11] = eNomina.aux_tel;
            fila[12] = eNomina.aux_gas;
            fila[13] = eNomina.cesantias;
            fila[14] = eNomina.int_ces;
            fila[15] = eNomina.prima;
            fila[16] = eNomina.vacaciones;
            fila[17] = eNomina.dotacion;
            fila[18] = eNomina.salud;
            fila[19] = eNomina.pension;
            fila[20] = eNomina.arp;
            fila[21] = eNomina.caja_comp;
            fila[22] = eNomina.total;
            
            dtNomina.Rows.Add(fila);
            dtNomina.DefaultView.Sort = "COD_OFICINA ASC";
            try
            {
                gvNomina.DataSource = dtNomina;
                gvNomina.DataBind();
            }
            catch (Exception ex)
            {
                VerError(ex.Message);
            }
            Session["DTNOMINA"] = dtNomina;
            Totalizar_Nomina();

            //try
            //{
            //    helper = new GridViewHelper(gvNomina, true);
            //    helper.RegisterGroup("COD_OFICINA", true, true);
            //    helper.RegisterSummary("COD_OFICINA", SummaryOperation.Sum);
            //    helper.ApplyGroupSort();
            //}
            //catch (Exception ex)
            //{
            //    VerError(ex.Message);
            //}
        }

    }

    protected void gvNomina_RowEditing(object sender, GridViewEditEventArgs e)
    {
        // Determinando el código del empleado a modificar
        int conseID = 0;
        if (gvNomina.DataKeys[e.NewEditIndex] != null)
        {
            try
            {
                conseID = Convert.ToInt32(gvNomina.DataKeys[e.NewEditIndex].Values[0].ToString());
            }
            catch
            {
                conseID = 0;
            }
        }
        // Colocando la fila que se va a modificar         
        gvNomina.EditIndex = 0;
        gvNomina.SelectedIndex = 0;
        gvNomina.Height = 30;  
        gvNomina.ShowFooter = false;
        // Buscando datos del cargo
        DataTable dtNomina = new DataTable();
        dtNomina = (DataTable)Session["DTNOMINA"];
        DataTable dtNominaEdit = new DataTable();
        dtNominaEdit = dtNomina.Clone();
        dtNominaEdit.ImportRow(dtNomina.Rows[e.NewEditIndex]);
        gvNomina.DataSource = dtNominaEdit;
        try
        {
            gvNomina.DataBind();
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
        e.NewEditIndex = 0;
        btnExpNomina.Visible = false;
        btnIncrementoGeneral.Visible = false;
        
    }

    protected void gvNomina_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvNomina.EditIndex = -1;
        gvNomina.ShowFooter = true;
        gvNomina.DataSource = Session["DTNOMINA"];
        gvNomina.DataBind();
        btnExpNomina.Visible = true;
        btnIncrementoGeneral.Visible = true;        
    }

    protected void gvNomina_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            // Cargando los datatable
            DataTable dtNomina = new DataTable();
            dtNomina = (DataTable)Session["DTNOMINA"];

            // Ubicarse en la fila a modificar en el datatable
            string llave = "";
            llave = e.Keys[0].ToString();
            DataRow fila = dtNomina.Rows.Find(llave);

            // Actualizando filas del datatable
            GridViewRow gvrNomina = gvNomina.Rows[e.RowIndex];
           
            TextBox txtCodigo = (TextBox)gvrNomina.FindControl("txtCodigo");
            TextBox txtNombre = (TextBox)gvrNomina.FindControl("txtNombre");
            TextBox txtFechaIngreso = (TextBox)gvrNomina.FindControl("txtFechaIngreso");
            DropDownList ddlCodOficina = (DropDownList)gvrNomina.FindControl("ddlCodOficina");
            TextBox txtSalario = (TextBox)gvrNomina.FindControl("txtSalario");
            DropDownList ddlCargo = (DropDownList)gvrNomina.FindControl("ddlCargo");
            TextBox txtIncremento = (TextBox)gvrNomina.FindControl("txtIncremento");
            TextBox txtCumplimiento = (TextBox)gvrNomina.FindControl("txtCumplimiento");
            TextBox txtaux_tel = (TextBox)gvrNomina.FindControl("txtaux_tel");
            TextBox txtaux_gas = (TextBox)gvrNomina.FindControl("txtaux_gas");
            DropDownList ddlTipoSalario = (DropDownList)gvrNomina.FindControl("ddlTipoSalario");

            fila["CODIGO"] = txtCodigo.Text;
            fila["NOMBRE"] = txtNombre.Text;
            try
            {
                fila["FECHA_INGRESO"] = txtFechaIngreso.Text;
            }
            catch
            {
                fila["FECHA_INGRESO"] = System.DateTime.Now;
            }
            fila["COD_OFICINA"] = Convert.ToInt64(ddlCodOficina.SelectedValue);
            fila["OFICINA"] = ddlCodOficina.SelectedItem;
            fila["SALARIO"] = Convert.ToDecimal(txtSalario.Text.Replace(gSeparadorMiles, ""));
            fila["CARGO"] = Convert.ToInt64(ddlCargo.SelectedValue);
            fila["NOM_CARGO"] = ddlCargo.SelectedItem;
            fila["TIPO_SALARIO"] = Convert.ToInt64(ddlTipoSalario.SelectedValue);
            fila["NOM_TIPO_SALARIO"] = ddlTipoSalario.SelectedItem;
            if (txtIncremento.Text.Trim() == "")
                fila["INCREMENTO"] = "0";
            else
                fila["INCREMENTO"] = Convert.ToDecimal(txtIncremento.Text.Replace(gSeparadorMiles, ""));
            if (txtCumplimiento.Text.Trim() == "")
                fila["CUMPLIMIENTO"] = "0";
            else
                fila["CUMPLIMIENTO"] = Convert.ToDecimal(txtCumplimiento.Text.Replace(gSeparadorMiles, ""));
            if (txtaux_tel.Text.Trim() == "")
                fila["AUX_TEL"] = "0";
            else
                fila["AUX_TEL"] = Convert.ToDecimal(txtaux_tel.Text.Replace(gSeparadorMiles, ""));
            if (txtaux_gas.Text.Trim() == "")
                fila["AUX_GAS"] = "0";
            else
                fila["AUX_GAS"] = Convert.ToDecimal(txtaux_gas.Text.Replace(gSeparadorMiles, ""));           

            Xpinn.Presupuesto.Entities.Nomina eNomina = new Xpinn.Presupuesto.Entities.Nomina();
            eNomina.codigo = Convert.ToInt64(txtCodigo.Text);
            eNomina.nombre = txtNombre.Text.ToUpper();
            try
            {
                eNomina.fecha_ingreso = Convert.ToDateTime(txtFechaIngreso.Text);
            }
            catch
            {
                eNomina.fecha_ingreso = System.DateTime.Now;
            }
            eNomina.cod_oficina = Convert.ToInt64(ddlCodOficina.SelectedValue);
            eNomina.salario = Convert.ToDecimal(txtSalario.Text.Replace(gSeparadorMiles, ""));
            eNomina.cargo = Convert.ToInt64(ddlCargo.SelectedValue);
            eNomina.tipo_salario = Convert.ToInt64(ddlTipoSalario.SelectedValue);
            if (txtIncremento.Text.Trim() == "")
                eNomina.incremento = 0;
            else
                eNomina.incremento = Convert.ToDecimal(txtIncremento.Text.Replace(gSeparadorMiles, ""));
            if (txtCumplimiento.Text.Trim() == "")
                eNomina.cumplimiento = 0;
            else
                eNomina.cumplimiento = Convert.ToDecimal(txtCumplimiento.Text.Replace(gSeparadorMiles, ""));
            if (txtaux_tel.Text.Trim() == "")
                eNomina.aux_tel = 0;
            else
                eNomina.aux_tel = Convert.ToDecimal(txtaux_tel.Text.Replace(gSeparadorMiles, ""));
            if (txtaux_gas.Text.Trim() == "")
                eNomina.aux_gas = 0;
            else
                eNomina.aux_gas = Convert.ToDecimal(txtaux_gas.Text.Replace(gSeparadorMiles, ""));
            eNomina = PresupuestoServicio.ModificarEmpleado(eNomina, (Usuario)Session["Usuario"]);

            fila[5] = eNomina.cargo;
            fila[7] = eNomina.salario_nuevo;
            fila[8] = eNomina.aux_trans;
            fila[10] = eNomina.comisiones;
            fila[11] = eNomina.aux_tel;
            fila[12] = eNomina.aux_gas;
            fila[13] = eNomina.cesantias;
            fila[14] = eNomina.int_ces;
            fila[15] = eNomina.prima;
            fila[16] = eNomina.vacaciones;
            fila[17] = eNomina.dotacion;
            fila[18] = eNomina.salud;
            fila[19] = eNomina.pension;
            fila[20] = eNomina.arp;
            fila[21] = eNomina.caja_comp;
            fila[22] = eNomina.total;            
            dtNomina.AcceptChanges();

            // Actualizando la grilla            
            gvNomina.EditIndex = -1;
            gvNomina.ShowFooter = true;
            dtNomina.DefaultView.Sort = "COD_OFICINA ASC";
            gvNomina.DataSource = dtNomina;
            gvNomina.DataBind();

            // Actualizando variable de sesión
            Session["DTNOMINA"] = dtNomina;
            Totalizar_Nomina();
            btnExpNomina.Visible = true;
            btnIncrementoGeneral.Visible = true;
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
            // mensaje de error
        }
    }

    public void Calcular_Nomina(Boolean bMayorizar)
    {
        // Cargando los datatable
        DataTable dtNomina = new DataTable();
        dtNomina = (DataTable)Session["DTNOMINA"];
        DataTable dtFechas = new DataTable();
        dtFechas = (DataTable)Session["DTFECHAS"];
        DataTable dtPresupuesto = new DataTable();
        dtPresupuesto = (DataTable)Session["DTPRESUPUESTO"];

        // Cargar valores al presupuesto
        PresupuestoServicio.CargarPresupuestoNomina(ref dtPresupuesto, dtNomina, dtFechas, bMayorizar, (Usuario)Session["Usuario"], 4);
        Session["DTPRESUPUESTO"] = dtPresupuesto;
        gvProyeccion.DataSource = dtPresupuesto;
        gvProyeccion.DataBind();
    }

    public void Totalizar_Nomina()
    {
        // Llenar la grilla con los totales
        string Error = "";
        string filtro = "";
        if (ddlCentroCosto.SelectedValue.ToString().Trim() != "0")
            filtro = " e.cod_oficina In (Select x.cod_oficina From oficina x Where x.centro_costo = " + ddlCentroCosto.SelectedValue.ToString().Trim() + ") ";
        else
            filtro = "";
        DateTime fechahistorico = DateTime.MinValue;
        fechahistorico = txtFechaCorte.ToDateTime;
        DataTable dtNomina = new DataTable();
        dtNomina = (DataTable)Session["DTNOMINA"];
        DataTable dtTotNomina = new DataTable();
        dtTotNomina = PresupuestoServicio.ListarTotalesNomina(fechahistorico, ref Error, (Usuario)Session["Usuario"], filtro);
        gvTotalesNomina.DataSource = dtTotNomina;
        gvTotalesNomina.DataBind();

        // Mostrar totales en el footer
        GridViewRow rFilaS = gvTotalesNomina.FooterRow;
        if (rFilaS != null)
        {
            rFilaS.Cells[0].Text = "TOTALES";
            rFilaS.Font.Bold = true;
            for (int i = 2; i <= 19; i++)
            {
                Double Total = 0;
                Double cantidad = 0;
                try
                {
                    foreach (DataRow rValor in dtTotNomina.Rows)
                    {
                        Total = Total + Convert.ToDouble(rValor[i]);
                        cantidad += 1;
                    }
                    if(i == 3 && cantidad != 0)
                        rFilaS.Cells[i].Text = (Total/cantidad).ToString("N2");
                    else
                        rFilaS.Cells[i].Text = Total.ToString("N2");
                }
                catch
                {
                }
            }
        }
    }

    protected void gvCargos_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("AddNew"))
        {
            TextBox txtCodigoF = (TextBox)gvCargos.FooterRow.FindControl("txtCodigoF");
            TextBox txtDescripcionF = (TextBox)gvCargos.FooterRow.FindControl("txtDescripcionF");
            TextBox txtcomision_colocacion_antF = (TextBox)gvCargos.FooterRow.FindControl("txtcomision_colocacion_antF");
            TextBox txtcomision_cartera_antF = (TextBox)gvCargos.FooterRow.FindControl("txtcomision_cartera_antF");
            TextBox txtaux_gas_antF = (TextBox)gvCargos.FooterRow.FindControl("txtaux_gas_antF");
            TextBox txtaux_tel_antF = (TextBox)gvCargos.FooterRow.FindControl("txtaux_tel_antF");
            TextBox txtincremento_colocacionF = (TextBox)gvCargos.FooterRow.FindControl("txtincremento_colocacionF");
            TextBox txtincremento_carteraF = (TextBox)gvCargos.FooterRow.FindControl("txtincremento_carteraF");
            TextBox txtincremento_aux_gasF = (TextBox)gvCargos.FooterRow.FindControl("txtincremento_aux_gasF");
            TextBox txtincremento_aux_telF = (TextBox)gvCargos.FooterRow.FindControl("txtincremento_aux_telF");

            DataTable dtCargos = new DataTable();
            dtCargos = (DataTable)Session["DTCARGOS"];

            if (dtCargos.Rows[0][0] == null || dtCargos.Rows[0][0].ToString() == "")
            {
                dtCargos.Rows[0].Delete();
            }

            if (txtcomision_colocacion_antF.Text == "")
                txtcomision_colocacion_antF.Text = "0";
            if (txtcomision_cartera_antF.Text == "")
                txtcomision_cartera_antF.Text = "0";
            if (txtaux_gas_antF.Text == "")
                txtaux_gas_antF.Text = "0";
            if (txtaux_tel_antF.Text == "")
                txtaux_tel_antF.Text = "0";
            if (txtincremento_colocacionF.Text == "")
                txtincremento_colocacionF.Text = "0";
            if (txtincremento_carteraF.Text == "")
                txtincremento_carteraF.Text = "0";
            if (txtincremento_aux_gasF.Text == "")
                txtincremento_aux_gasF.Text = "0";
            if (txtincremento_aux_telF.Text == "")
                txtincremento_aux_telF.Text = "0";

            DataRow fila = dtCargos.NewRow();
            fila["cod_cargo"] = txtCodigoF.Text;
            fila["nom_cargo"] = txtDescripcionF.Text;
            fila["comision_colocacion_ant"] = Convert.ToDecimal(txtcomision_colocacion_antF.Text.Replace(gSeparadorMiles, "")); 
            fila["comision_cartera_ant"] = Convert.ToDecimal(txtcomision_cartera_antF.Text.Replace(gSeparadorMiles, ""));
            fila["aux_gas_ant"] = Convert.ToDecimal(txtaux_gas_antF.Text.Replace(gSeparadorMiles, ""));
            fila["aux_tel_ant"] = Convert.ToDecimal(txtaux_tel_antF.Text.Replace(gSeparadorMiles, ""));
            fila["incremento_colocacion"] = Convert.ToDecimal(txtincremento_colocacionF.Text.Replace(gSeparadorMiles, ""));
            fila["incremento_cartera"] = Convert.ToDecimal(txtincremento_carteraF.Text.Replace(gSeparadorMiles, ""));
            fila["incremento_aux_gas"] = Convert.ToDecimal(txtincremento_aux_gasF.Text.Replace(gSeparadorMiles, ""));
            fila["incremento_aux_tel"] = Convert.ToDecimal(txtincremento_aux_telF.Text.Replace(gSeparadorMiles, ""));

            Xpinn.Presupuesto.Entities.Cargos eCargos = new Xpinn.Presupuesto.Entities.Cargos();
            eCargos.cod_cargo = Convert.ToInt64(txtCodigoF.Text);
            eCargos.nom_cargo = txtDescripcionF.Text.ToUpper();
            eCargos.comision_colocacion_ant = Convert.ToDecimal(txtcomision_colocacion_antF.Text.Replace(gSeparadorMiles, ""));
            eCargos.comision_cartera_ant = Convert.ToDecimal(txtcomision_cartera_antF.Text.Replace(gSeparadorMiles, ""));
            eCargos.aux_gas_ant = Convert.ToDecimal(txtaux_gas_antF.Text.Replace(gSeparadorMiles, ""));
            eCargos.aux_tel_ant = Convert.ToDecimal(txtaux_tel_antF.Text.Replace(gSeparadorMiles, ""));
            eCargos.incremento_colocacion = Convert.ToDecimal(txtincremento_colocacionF.Text.Replace(gSeparadorMiles, ""));
            eCargos.incremento_cartera = Convert.ToDecimal(txtincremento_carteraF.Text.Replace(gSeparadorMiles, ""));
            eCargos.incremento_aux_gas = Convert.ToDecimal(txtincremento_aux_gasF.Text.Replace(gSeparadorMiles, ""));
            eCargos.incremento_aux_tel = Convert.ToDecimal(txtincremento_aux_telF.Text.Replace(gSeparadorMiles, ""));
            eCargos = PresupuestoServicio.CrearCargo(eCargos, (Usuario)Session["Usuario"]);

            fila["comision_colocacion"] = eCargos.comision_colocacion;
            fila["comision_cartera"] = eCargos.comision_cartera;
            fila["aux_gas"] = eCargos.aux_gas;
            fila["aux_tel"] = eCargos.aux_tel;

            dtCargos.Rows.Add(fila);
            try
            {
                gvCargos.DataSource = dtCargos;
                gvCargos.DataBind();
            }
            catch (Exception ex)
            {
                VerError(ex.Message);
            }
            Session["DTCARGOS"] = dtCargos;
        }
    }

    protected void gvCargos_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string sConcepto = "";
            if (gvCargos.DataKeys[e.Row.DataItemIndex] != null)
            {
                sConcepto = gvCargos.DataKeys[e.Row.DataItemIndex].Values[0].ToString();
            }       
       }
    }

    protected void gvCargos_RowEditing(object sender, GridViewEditEventArgs e)
    {
        int conseID = 0;
        if (gvCargos.DataKeys[e.NewEditIndex] != null)
        {
            try
            {
                conseID = Convert.ToInt32(gvCargos.DataKeys[e.NewEditIndex].Values[0].ToString());
            }
            catch
            {
                conseID = 0;
            }
        }
        gvCargos.EditIndex = e.NewEditIndex;
        gvCargos.DataSource = Session["DTCARGOS"];
        try
        {
            gvCargos.DataBind();
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }

    protected void gvCargos_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvCargos.EditIndex = -1;
        gvCargos.DataSource = Session["DTCARGOS"];
        gvCargos.DataBind();
    }

    protected void gvCargos_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            // Cargando los datatable
            DataTable dtCargos = new DataTable();
            dtCargos = (DataTable)Session["DTCARGOS"];

            // Ubicarse en la fila a modificar en el datatable
            string llave = "";
            llave = e.Keys[0].ToString();
            DataRow fila = dtCargos.Rows.Find(llave);

            // Actualizando filas del datatable
            GridViewRow gvrCargos = gvCargos.Rows[e.RowIndex];

            Label lblCodigo = (Label)gvrCargos.FindControl("lblCodigo");
            Label lblDescripcion = (Label)gvrCargos.FindControl("lblDescripcion");
            TextBox txtcomision_colocacion_ant = (TextBox)gvrCargos.FindControl("txtcomision_colocacion_ant");
            TextBox txtcomision_cartera_ant = (TextBox)gvrCargos.FindControl("txtcomision_cartera_ant");
            TextBox txtaux_gas_ant = (TextBox)gvrCargos.FindControl("txtaux_gas_ant");
            TextBox txtaux_tel_ant = (TextBox)gvrCargos.FindControl("txtaux_tel_ant");
            TextBox txtincremento_colocacion = (TextBox)gvrCargos.FindControl("txtincremento_colocacion");
            TextBox txtincremento_cartera = (TextBox)gvrCargos.FindControl("txtincremento_cartera");
            TextBox txtincremento_aux_gas = (TextBox)gvrCargos.FindControl("txtincremento_aux_gas");
            TextBox txtincremento_aux_tel = (TextBox)gvrCargos.FindControl("txtincremento_aux_tel");

            fila["cod_cargo"] = lblCodigo.Text;
            fila["nom_cargo"] = lblDescripcion.Text;
            fila["comision_colocacion_ant"] = Convert.ToDecimal(txtcomision_colocacion_ant.Text.Replace(gSeparadorMiles, ""));
            fila["comision_cartera_ant"] = Convert.ToDecimal(txtcomision_cartera_ant.Text.Replace(gSeparadorMiles, ""));
            fila["aux_gas_ant"] = Convert.ToDecimal(txtaux_gas_ant.Text.Replace(gSeparadorMiles, ""));
            fila["aux_tel_ant"] = Convert.ToDecimal(txtaux_tel_ant.Text.Replace(gSeparadorMiles, ""));
            fila["incremento_colocacion"] = Convert.ToDecimal(txtincremento_colocacion.Text.Replace(gSeparadorMiles, ""));
            fila["incremento_cartera"] = Convert.ToDecimal(txtincremento_cartera.Text.Replace(gSeparadorMiles, ""));
            fila["incremento_aux_gas"] = Convert.ToDecimal(txtincremento_aux_gas.Text.Replace(gSeparadorMiles, ""));
            fila["incremento_aux_tel"] = Convert.ToDecimal(txtincremento_aux_tel.Text.Replace(gSeparadorMiles, ""));

            Xpinn.Presupuesto.Entities.Cargos eCargos = new Xpinn.Presupuesto.Entities.Cargos();
            eCargos.cod_cargo = Convert.ToInt64(lblCodigo.Text);
            eCargos.nom_cargo = lblDescripcion.Text;
            eCargos.comision_colocacion_ant = Convert.ToDecimal(txtcomision_colocacion_ant.Text.Replace(gSeparadorMiles, ""));
            eCargos.comision_cartera_ant = Convert.ToDecimal(txtcomision_cartera_ant.Text.Replace(gSeparadorMiles, ""));
            eCargos.aux_gas_ant = Convert.ToDecimal(txtaux_gas_ant.Text.Replace(gSeparadorMiles, ""));
            eCargos.aux_tel_ant = Convert.ToDecimal(txtaux_tel_ant.Text.Replace(gSeparadorMiles, ""));
            eCargos.incremento_colocacion = Convert.ToDecimal(txtincremento_colocacion.Text.Replace(gSeparadorMiles, ""));
            eCargos.incremento_cartera = Convert.ToDecimal(txtincremento_cartera.Text.Replace(gSeparadorMiles, ""));
            eCargos.incremento_aux_gas = Convert.ToDecimal(txtincremento_aux_gas.Text.Replace(gSeparadorMiles, ""));
            eCargos.incremento_aux_tel = Convert.ToDecimal(txtincremento_aux_tel.Text.Replace(gSeparadorMiles, ""));
            eCargos = PresupuestoServicio.ModificarCargo(eCargos, (Usuario)Session["Usuario"]);

            fila["comision_colocacion"] = eCargos.comision_colocacion;
            fila["comision_cartera"] = eCargos.comision_cartera;
            fila["aux_gas"] = eCargos.aux_gas;
            fila["aux_tel"] = eCargos.aux_tel;

            dtCargos.AcceptChanges();

            // Actualizando la grilla            
            gvCargos.EditIndex = -1;
            gvCargos.DataSource = dtCargos;
            gvCargos.DataBind();

            // Actualizando variable de sesión
            Session["DTCARGOS"] = dtCargos;

            // Actualizar todos los empleados que tienen ese cargo
            DataTable dtNomina = new DataTable();
            dtNomina = (DataTable)Session["DTNOMINA"];
            foreach (DataRow drFila in dtNomina.Rows)
            {
                Xpinn.Presupuesto.Entities.Nomina eNomina = new Xpinn.Presupuesto.Entities.Nomina();
                eNomina.codigo = Convert.ToInt64(drFila["CODIGO"].ToString());
                eNomina.nombre = drFila["NOMBRE"].ToString();
                try
                {
                    eNomina.fecha_ingreso = Convert.ToDateTime(drFila["FECHA_INGRESO"].ToString());
                }
                catch
                {
                    eNomina.fecha_ingreso = System.DateTime.Now;
                }
                eNomina.cod_oficina = Convert.ToInt64(drFila["COD_OFICINA"].ToString());
                eNomina.salario = Convert.ToDecimal(drFila["SALARIO"].ToString());
                eNomina.cargo = Convert.ToInt64(drFila["CARGO"].ToString());
                if (drFila["INCREMENTO"].ToString().Trim() != "")
                    eNomina.incremento = Convert.ToDecimal(drFila["INCREMENTO"].ToString());
                else
                    eNomina.incremento = 0;
                if (drFila["CUMPLIMIENTO"].ToString().Trim() != "")
                    eNomina.cumplimiento = Convert.ToDecimal(drFila["CUMPLIMIENTO"].ToString());
                else
                    eNomina.cumplimiento = 0;
                if (eNomina.cargo == Convert.ToInt64(lblCodigo.Text))
                {
                    eNomina = PresupuestoServicio.ActualizarEmpleado(eNomina, (Usuario)Session["Usuario"]);
                    drFila[5] = eNomina.cargo;
                    drFila[7] = eNomina.salario_nuevo;
                    drFila[8] = eNomina.aux_trans;
                    drFila[10] = eNomina.comisiones;
                    drFila[11] = eNomina.aux_tel;
                    drFila[12] = eNomina.aux_gas;
                    drFila[13] = eNomina.cesantias;
                    drFila[14] = eNomina.int_ces;
                    drFila[15] = eNomina.prima;
                    drFila[16] = eNomina.vacaciones;
                    drFila[17] = eNomina.dotacion;
                    drFila[18] = eNomina.salud;
                    drFila[19] = eNomina.pension;
                    drFila[20] = eNomina.arp;
                    drFila[21] = eNomina.caja_comp;
                    drFila[22] = eNomina.total;
                    dtNomina.AcceptChanges();
                }
            }

            // Actualizando la grilla
            dtNomina.DefaultView.Sort = "COD_OFICINA ASC";
            gvNomina.DataSource = dtNomina;
            gvNomina.DataBind();

            // Actualizando variable de sesión
            Session["DTNOMINA"] = dtNomina;
            Totalizar_Nomina();
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
            // mensaje de error
        }
    }

    protected DataTable ListarCargos()
    {
        return PresupuestoServicio.ListarCargos((Usuario)Session["Usuario"]); ;
    }

    protected void btnIncrementoGeneral_Click(object sender, EventArgs e)
    {
        mpeIncremento.Show();
    }

    protected void btnIncrementoCancelar_Click(object sender, EventArgs e)
    {
        mpeIncremento.Hide();
    }

    protected void btnIncrementoAceptar_Click(object sender, EventArgs e)
    {
        Configuracion conf = new Configuracion();
        txtIncrementoGen.Text = txtIncrementoGen.Text.ToString().Replace(".", conf.ObtenerSeparadorDecimalConfig());
        if (txtIncrementoGen.Text != "")
        {
            DataTable dtNomina = new DataTable();
            dtNomina = (DataTable)Session["DTNOMINA"];
            foreach (DataRow drFila in dtNomina.Rows)
            {
                drFila["INCREMENTO"] = Convert.ToDouble(txtIncrementoGen.Text);
                Xpinn.Presupuesto.Entities.Nomina eNomina = new Xpinn.Presupuesto.Entities.Nomina();
                eNomina.codigo = Convert.ToInt64(drFila["CODIGO"].ToString());
                eNomina.nombre = drFila["NOMBRE"].ToString();
                try
                {
                    eNomina.fecha_ingreso = Convert.ToDateTime(drFila["FECHA_INGRESO"].ToString());
                }
                catch
                {
                    eNomina.fecha_ingreso = System.DateTime.Now;
                }
                eNomina.cod_oficina = Convert.ToInt64(drFila["COD_OFICINA"].ToString());
                eNomina.salario = Convert.ToDecimal(drFila["SALARIO"].ToString());
                eNomina.cargo = Convert.ToInt64(drFila["CARGO"].ToString());
                eNomina.tipo_salario = Convert.ToInt64(drFila["TIPO_SALARIO"].ToString());
                if (drFila["INCREMENTO"].ToString().Trim() != "")
                    eNomina.incremento = Convert.ToDecimal(drFila["INCREMENTO"].ToString());
                else
                    eNomina.incremento = 0;
                if (drFila["CUMPLIMIENTO"].ToString().Trim() != "")
                    eNomina.cumplimiento = Convert.ToDecimal(drFila["CUMPLIMIENTO"].ToString());
                else
                    eNomina.cumplimiento = 0;
                eNomina = PresupuestoServicio.ModificarEmpleado(eNomina, (Usuario)Session["Usuario"]);
                drFila[5] = eNomina.cargo;
                drFila[7] = eNomina.salario_nuevo;
                drFila[8] = eNomina.aux_trans;
                drFila[10] = eNomina.comisiones;
                drFila[11] = eNomina.aux_tel;
                drFila[12] = eNomina.aux_gas;
                drFila[13] = eNomina.cesantias;
                drFila[14] = eNomina.int_ces;
                drFila[15] = eNomina.prima;
                drFila[16] = eNomina.vacaciones;
                drFila[17] = eNomina.dotacion;
                drFila[18] = eNomina.salud;
                drFila[19] = eNomina.pension;
                drFila[20] = eNomina.arp;
                drFila[21] = eNomina.caja_comp;
                drFila[22] = eNomina.total;
                dtNomina.AcceptChanges();
            }

            // Actualizando la grilla
            dtNomina.DefaultView.Sort = "COD_OFICINA ASC";
            gvNomina.DataSource = dtNomina;
            gvNomina.DataBind();

            // Actualizando variable de sesión
            Session["DTNOMINA"] = dtNomina;
            Totalizar_Nomina();
        }
        mpeIncremento.Hide();
    }

    protected void btnCumplimientoGeneral_Click(object sender, EventArgs e)
    {
        mpeCumplimiento.Show();
    }

    protected void btnCumplimientoCancelar_Click(object sender, EventArgs e)
    {
        mpeCumplimiento.Hide();
    }

    protected void btnCumplimientoAceptar_Click(object sender, EventArgs e)
    {
        Configuracion conf = new Configuracion();
        txtCumplimientoGen.Text = txtCumplimientoGen.Text.ToString().Replace(".", conf.ObtenerSeparadorDecimalConfig());
        if (txtCumplimientoGen.Text != "")
        {
            DataTable dtNomina = new DataTable();
            dtNomina = (DataTable)Session["DTNOMINA"];
            foreach (DataRow drFila in dtNomina.Rows)
            {
                drFila["CUMPLIMIENTO"] = Convert.ToDouble(txtCumplimientoGen.Text);
                Xpinn.Presupuesto.Entities.Nomina eNomina = new Xpinn.Presupuesto.Entities.Nomina();
                eNomina.codigo = Convert.ToInt64(drFila["CODIGO"].ToString());
                eNomina.nombre = drFila["NOMBRE"].ToString();
                try
                {
                    eNomina.fecha_ingreso = Convert.ToDateTime(drFila["FECHA_INGRESO"].ToString());
                }
                catch
                {
                    eNomina.fecha_ingreso = System.DateTime.Now;
                }
                eNomina.cod_oficina = Convert.ToInt64(drFila["COD_OFICINA"].ToString());
                eNomina.salario = Convert.ToDecimal(drFila["SALARIO"].ToString());
                eNomina.cargo = Convert.ToInt64(drFila["CARGO"].ToString());
                eNomina.tipo_salario = Convert.ToInt64(drFila["TIPO_SALARIO"].ToString());
                if (drFila["INCREMENTO"].ToString().Trim() != "")
                    eNomina.incremento = Convert.ToDecimal(drFila["INCREMENTO"].ToString());
                else
                    eNomina.incremento = 0;
                if (drFila["CUMPLIMIENTO"].ToString().Trim() != "")
                    eNomina.cumplimiento = Convert.ToDecimal(drFila["CUMPLIMIENTO"].ToString());
                else
                    eNomina.cumplimiento = 0;
                if (drFila["AUX_TEL"].ToString().Trim() != "")
                    eNomina.aux_tel = Convert.ToDecimal(drFila["AUX_TEL"].ToString());
                else
                    eNomina.aux_tel = 0;
                if (drFila["AUX_GAS"].ToString().Trim() != "")
                    eNomina.aux_gas = Convert.ToDecimal(drFila["AUX_GAS"].ToString());
                else
                    eNomina.aux_gas = 0;
                eNomina = PresupuestoServicio.ModificarEmpleado(eNomina, (Usuario)Session["Usuario"]);
                drFila[5] = eNomina.cargo;
                drFila[7] = eNomina.salario_nuevo;
                drFila[8] = eNomina.aux_trans;
                drFila[10] = eNomina.comisiones;
                drFila[11] = eNomina.aux_tel;
                drFila[12] = eNomina.aux_gas;
                drFila[13] = eNomina.cesantias;
                drFila[14] = eNomina.int_ces;
                drFila[15] = eNomina.prima;
                drFila[16] = eNomina.vacaciones;
                drFila[17] = eNomina.dotacion;
                drFila[18] = eNomina.salud;
                drFila[19] = eNomina.pension;
                drFila[20] = eNomina.arp;
                drFila[21] = eNomina.caja_comp;
                drFila[22] = eNomina.total;
                dtNomina.AcceptChanges();
            }

            // Actualizando la grilla
            dtNomina.DefaultView.Sort = "COD_OFICINA ASC";
            gvNomina.DataSource = dtNomina;
            gvNomina.DataBind();

            // Actualizando variable de sesión
            Session["DTNOMINA"] = dtNomina;
            Totalizar_Nomina();
        }
        mpeCumplimiento.Hide();
    }

    #endregion Nomina

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //  PRESUPUESTO DE ACTIVOS FIJOS
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    #region ActivosFijos

    protected void btnRegresarACF_Click(object sender, ImageClickEventArgs e)
    {
        Calcular_ActivosFijos(true);
        GenerarFlujo();
        Site toolBar = (Site)this.Master;
        toolBar.MostrarGuardar(true);

        mvPresupuesto.ActiveViewIndex = 1;
    }

    protected void gvActivosFij_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblVrCompra = (Label)e.Row.FindControl("lblVrCompra");
                lblVrCompra.Attributes.CssStyle.Add("TEXT-ALIGN", "right");

            }

            if (e.Row.RowType == DataControlRowType.Footer)
            {
                List<Xpinn.Caja.Entities.Oficina> LstOficina;
                DropDownList ddlCodOficinaF = (DropDownList)e.Row.FindControl("ddlCodOficinaF");
                if (ddlCodOficinaF != null)
                {
                    LstOficina = ListaOficinas();
                    ddlCodOficinaF.DataSource = LstOficina;
                    ddlCodOficinaF.DataTextField = "cod_oficina";
                    ddlCodOficinaF.DataValueField = "nombre";
                    ddlCodOficinaF.SelectedValue = "";
                }

                TextBox txtVrCompraF = (TextBox)e.Row.FindControl("txtVrCompraF");
                txtVrCompraF.Attributes.CssStyle.Add("TEXT-ALIGN", "right");
            }
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }

    protected void gvActivosFij_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            DataTable dtActivosFij = new DataTable();
            dtActivosFij = (DataTable)Session["DTACTIVOSFIJ"];

            if ((e.RowIndex == 0) && (dtActivosFij.Rows[0][0] != null || dtActivosFij.Rows[0][0].ToString() == "0") && (dtActivosFij.Rows.Count == 1))
            {
                CrearActivoFijoInicial(ref dtActivosFij);
            }

            Label lblCodigo = (Label)gvActivosFij.Rows[e.RowIndex].FindControl("lblCodigo");
            Xpinn.Presupuesto.Entities.ActivosFijos eActivosFijos = new Xpinn.Presupuesto.Entities.ActivosFijos();
            eActivosFijos.codigo = Convert.ToInt64(lblCodigo.Text);
            PresupuestoServicio.EliminarActivoFijo(eActivosFijos, (Usuario)Session["Usuario"]);

            dtActivosFij.Rows[e.RowIndex].Delete();
            dtActivosFij.AcceptChanges();
            gvActivosFij.DataSource = dtActivosFij;
            gvActivosFij.DataBind();
            Session["DTACTIVOSFIJ"] = dtActivosFij;

            if ((e.RowIndex == 0) && (dtActivosFij.Rows[0][0] == null || dtActivosFij.Rows[0][0].ToString() == "0") && (dtActivosFij.Rows.Count == 1))
                gvActivosFij.Rows[0].Visible = false;

        }
        catch (Xpinn.Util.ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(PresupuestoServicio.GetType().Name + "L", "gvActivosFij_RowDeleting", ex);
        }
    }

    protected void gvActivosFij_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("AddNew"))
        {
            TextBox txtCodigoF = (TextBox)gvActivosFij.FooterRow.FindControl("txtCodigoF");
            TextBox txtDescripcionF = (TextBox)gvActivosFij.FooterRow.FindControl("txtDescripcionF");
            DropDownList ddlCodOficinaF = (DropDownList)gvActivosFij.FooterRow.FindControl("ddlCodOficinaF");
            TextBox txtVrCompraF = (TextBox)gvActivosFij.FooterRow.FindControl("txtVrCompraF");
            TextBox txtFechaCompraF = (TextBox)gvActivosFij.FooterRow.FindControl("txtFechaCompraF");
            DropDownList ddlTipoActivoF = (DropDownList)gvActivosFij.FooterRow.FindControl("ddlTipoActivoF");

            DataTable dtActivosFij = new DataTable();
            dtActivosFij = (DataTable)Session["DTACTIVOSFIJ"];

            if (dtActivosFij.Rows[0][0] == null || dtActivosFij.Rows[0][0].ToString() == "0")
            {
                dtActivosFij.Rows[0].Delete();
            }

            DataRow fila = dtActivosFij.NewRow();
            fila[0] = txtCodigoF.Text;
            fila[1] = txtDescripcionF.Text;
            fila[2] = Convert.ToInt64(ddlCodOficinaF.SelectedValue);
            fila[3] = Convert.ToDecimal(txtVrCompraF.Text.Replace(gSeparadorMiles, ""));
            fila[4] = Convert.ToDateTime(txtFechaCompraF.Text);
            fila[5] = Convert.ToInt64(ddlTipoActivoF.SelectedValue);
            fila[6] = Convert.ToString(ddlTipoActivoF.SelectedItem.Text);

            Xpinn.Presupuesto.Entities.ActivosFijos eActivosFijos = new Xpinn.Presupuesto.Entities.ActivosFijos();
            eActivosFijos.codigo = Convert.ToInt64(txtCodigoF.Text);
            eActivosFijos.descripcion = txtDescripcionF.Text.ToUpper();
            eActivosFijos.cod_oficina = Convert.ToInt64(ddlCodOficinaF.SelectedValue);
            eActivosFijos.vrcompra = Convert.ToDecimal(txtVrCompraF.Text.Replace(gSeparadorMiles, ""));
            eActivosFijos.fecha_compra = Convert.ToDateTime(txtFechaCompraF.Text);
            eActivosFijos.tipo_activo = Convert.ToInt64(ddlTipoActivoF.SelectedValue);
            eActivosFijos = PresupuestoServicio.CrearActivoFijo(eActivosFijos, (Usuario)Session["Usuario"]);

            dtActivosFij.Rows.Add(fila);
            gvActivosFij.DataSource = dtActivosFij;
            gvActivosFij.DataBind();
            Session["DTACTIVOSFIJ"] = dtActivosFij;

        }

    }

    protected void gvActivosFij_RowEditing(object sender, GridViewEditEventArgs e)
    {
        int conseID = 0;
        if (gvActivosFij.DataKeys[e.NewEditIndex] != null)
        {
            try
            {
                conseID = Convert.ToInt32(gvActivosFij.DataKeys[e.NewEditIndex].Values[0].ToString());
            }
            catch
            {
                conseID = 0;
            }
        }
        gvActivosFij.EditIndex = e.NewEditIndex;
        try
        {
            gvActivosFij.DataSource = Session["DTACTIVOSFIJ"];
            gvActivosFij.DataBind();
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }

    protected void gvActivosFij_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvActivosFij.EditIndex = -1;
        gvActivosFij.DataSource = Session["DTACTIVOSFIJ"];
        gvActivosFij.DataBind();
    }

    protected void gvActivosFij_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            // Cargando los datatable
            DataTable dtActivosFij = new DataTable();
            dtActivosFij = (DataTable)Session["DTACTIVOSFIJ"];

            // Ubicarse en la fila a modificar en el datatable
            string llave = "";
            llave = e.Keys[0].ToString();
            DataRow fila = dtActivosFij.Rows.Find(llave);

            // Actualizando filas del datatable
            GridViewRow gvrActivosFij = gvActivosFij.Rows[e.RowIndex];

            TextBox txtCodigo = (TextBox)gvrActivosFij.FindControl("txtCodigo");
            TextBox txtDescripcion = (TextBox)gvrActivosFij.FindControl("txtDescripcion");
            DropDownList ddlCodOficina = (DropDownList)gvrActivosFij.FindControl("ddlCodOficina");
            TextBox txtVrCompra = (TextBox)gvrActivosFij.FindControl("txtVrCompra");
            TextBox txtFechaCompra = (TextBox)gvrActivosFij.FindControl("txtFechaCompra");
            DropDownList ddlTipoActivo = (DropDownList)gvrActivosFij.FindControl("ddlTipoActivo");

            fila[0] = txtCodigo.Text;
            fila[1] = txtDescripcion.Text;
            fila[2] = Convert.ToInt64(ddlCodOficina.SelectedValue);
            fila[3] = Convert.ToDecimal(txtVrCompra.Text.Replace(gSeparadorMiles, ""));
            fila[4] = Convert.ToDateTime(txtFechaCompra.Text);
            fila[5] = Convert.ToInt64(ddlTipoActivo.SelectedValue);
            fila[6] = Convert.ToString(ddlTipoActivo.SelectedItem.Text);

            Xpinn.Presupuesto.Entities.ActivosFijos eActivosFijos = new Xpinn.Presupuesto.Entities.ActivosFijos();
            eActivosFijos.codigo = Convert.ToInt64(txtCodigo.Text);
            eActivosFijos.descripcion = txtDescripcion.Text.ToUpper();
            eActivosFijos.cod_oficina = Convert.ToInt64(ddlCodOficina.SelectedValue);
            eActivosFijos.vrcompra = Convert.ToDecimal(txtVrCompra.Text.Replace(gSeparadorMiles, ""));
            eActivosFijos.fecha_compra = Convert.ToDateTime(txtFechaCompra.Text);
            eActivosFijos.tipo_activo = Convert.ToInt64(ddlTipoActivo.SelectedValue);
            eActivosFijos = PresupuestoServicio.ModificarActivoFijo(eActivosFijos, (Usuario)Session["Usuario"]);

            dtActivosFij.AcceptChanges();

            // Actualizando la grilla            
            gvActivosFij.EditIndex = -1;
            gvActivosFij.DataSource = dtActivosFij;
            gvActivosFij.DataBind();

            // Actualizando variable de sesión
            Session["DTACTIVOSFIJ"] = dtActivosFij;
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
            // mensaje de error
        }
    }

    public void Calcular_ActivosFijos(Boolean bMayorizar)
    {
        // Cargando los datatable
        DataTable dtActivosFij = new DataTable();
        dtActivosFij = (DataTable)Session["DTACTIVOSFIJ"];
        DataTable dtFechas = new DataTable();
        dtFechas = (DataTable)Session["DTFECHAS"];
        DataTable dtPresupuesto = new DataTable();
        dtPresupuesto = (DataTable)Session["DTPRESUPUESTO"];
 
        // Cargar valores al presupuesto
        PresupuestoServicio.CargarPresupuestoActivosFijos(ref dtPresupuesto, dtActivosFij, dtFechas, bMayorizar, (Usuario)Session["Usuario"], 4);
        Session["DTPRESUPUESTO"] = dtPresupuesto;
        gvProyeccion.DataSource = dtPresupuesto;
        gvProyeccion.DataBind();
    }

    protected void CrearActivoFijoInicial(ref DataTable dt)
    {
        DataRow dr;
        dr = dt.NewRow();
        dr[0] = "0";
        dt.Rows.Add(dr);
    }

    #endregion ActivosFijos

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //  PRESUPUESTO DE OBLIGACIONES FINANCIERAS
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    #region ObligacionesFinancieras

    protected void btnRegresarOBL_Click(object sender, ImageClickEventArgs e)
    {
        // Cargar valores al presupuesto
        Calcular_Obligaciones(true);
        GenerarFlujo();
        // Ir a la pantalla de detalle del presupuesto
        Site toolBar = (Site)this.Master;
        toolBar.MostrarGuardar(true);

        mvPresupuesto.ActiveViewIndex = 1;
    }

    protected void gvObligacionesNuevas_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblCupo = (Label)e.Row.FindControl("lblCupo");
                if (lblCupo != null)
                    lblCupo.Attributes.CssStyle.Add("TEXT-ALIGN", "right");
            }

            if (e.Row.RowType == DataControlRowType.Footer)
            {
                TextBox txtCupoF = (TextBox)e.Row.FindControl("txtCupoF");
                txtCupoF.Attributes.CssStyle.Add("TEXT-ALIGN", "right");
            }
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }

    protected void gvObligacionesNuevas_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            DataTable dtObligacionesNuevas = new DataTable();
            dtObligacionesNuevas = (DataTable)Session["DTOBLIGACIONESNUEVAS"];

            if ((e.RowIndex == 0) && (dtObligacionesNuevas.Rows[0][0] != null || dtObligacionesNuevas.Rows[0][0].ToString() == "0") && (dtObligacionesNuevas.Rows.Count == 1))
            {
                CrearObligacionesNuevasInicial(ref dtObligacionesNuevas);
            }

            Label lblCodigo = (Label)gvObligacionesNuevas.Rows[e.RowIndex].FindControl("lblCodigo");
            Xpinn.Presupuesto.Entities.Obligacion eObligacionesNuevas = new Xpinn.Presupuesto.Entities.Obligacion();
            eObligacionesNuevas.codigo = Convert.ToInt64(lblCodigo.Text);
            if (eObligacionesNuevas.codigo != 0)
                PresupuestoServicio.EliminarObligacion(eObligacionesNuevas, (Usuario)Session["Usuario"]);

            dtObligacionesNuevas.Rows[e.RowIndex].Delete();
            dtObligacionesNuevas.AcceptChanges();
            gvObligacionesNuevas.DataSource = dtObligacionesNuevas;
            gvObligacionesNuevas.DataBind();
            Session["DTOBLIGACIONESNUEVAS"] = dtObligacionesNuevas;

            if ((e.RowIndex == 0) && (dtObligacionesNuevas.Rows[0][0] == null || dtObligacionesNuevas.Rows[0][0].ToString() == "") && (dtObligacionesNuevas.Rows.Count == 1))
                gvDiferidos.Rows[0].Visible = false;

        }
        catch (Xpinn.Util.ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(PresupuestoServicio.GetType().Name + "L", "gvObligacionesNuevas_RowDeleting", ex);
        }
    }

    protected void gvObligacionesNuevas_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("AddNew"))
        {
            TextBox txtCodigoF = (TextBox)gvObligacionesNuevas.FooterRow.FindControl("txtCodigoF");
            DropDownList ddlDescripcionF = (DropDownList)gvObligacionesNuevas.FooterRow.FindControl("ddlDescripcionF");
            TextBox txtCupoF = (TextBox)gvObligacionesNuevas.FooterRow.FindControl("txtCupoF");
            TextBox txtTasaF = (TextBox)gvObligacionesNuevas.FooterRow.FindControl("txtTasaF");
            DropDownList ddlPeriodicidadF = (DropDownList)gvObligacionesNuevas.FooterRow.FindControl("ddlPeriodicidadF");
            TextBox txtPlazoOblF = (TextBox)gvObligacionesNuevas.FooterRow.FindControl("txtPlazoOblF");
            TextBox txtGraciaF = (TextBox)gvObligacionesNuevas.FooterRow.FindControl("txtGraciaF");

            DataTable dtObligacionesNuevas = new DataTable();
            dtObligacionesNuevas = (DataTable)Session["DTOBLIGACIONESNUEVAS"];

            if (dtObligacionesNuevas.Rows.Count > 0)
            {
                try
                {
                    if (dtObligacionesNuevas.Rows[0][0] == null || dtObligacionesNuevas.Rows[0][0].ToString() == "0")
                    {
                        dtObligacionesNuevas.Rows[0].Delete();
                        dtObligacionesNuevas.AcceptChanges();
                    }
                }
                catch
                {
                    VerError("");
                }
            }

            DataRow fila = dtObligacionesNuevas.NewRow();
            fila["codigo"] = txtCodigoF.Text;
            fila["descripcion"] = ddlDescripcionF.SelectedValue;
            fila["cupo"] = Convert.ToDecimal(txtCupoF.Text.Replace(gSeparadorMiles, ""));
            fila["tasa"] = Convert.ToDecimal(txtTasaF.Text.Replace(gSeparadorMiles, ""));
            fila["cod_periodicidad"] = Convert.ToInt64(ddlPeriodicidadF.SelectedValue);
            fila["plazo"] = Convert.ToDecimal(txtPlazoOblF.Text.Replace(gSeparadorMiles, ""));
            fila["gracia"] = Convert.ToDecimal(txtGraciaF.Text.Replace(gSeparadorMiles, ""));
            DataTable dtFechas = new DataTable();
            dtFechas = (DataTable)Session["DTFECHAS"];
            int numeroPeriodo = 0;
            foreach (DataRow drFecha in dtFechas.Rows)
            {
                if (numeroPeriodo + 4 <= dtObligacionesNuevas.Columns.Count)
                    fila[numeroPeriodo + 8] = 0;
                numeroPeriodo += 1;
            }

            Xpinn.Presupuesto.Entities.Obligacion eObligacionesNuevas = new Xpinn.Presupuesto.Entities.Obligacion();
            eObligacionesNuevas.codigo = Convert.ToInt64(txtCodigoF.Text);
            eObligacionesNuevas.descripcion = ddlDescripcionF.SelectedValue;
            eObligacionesNuevas.valor = Convert.ToDecimal(txtCupoF.Text.Replace(gSeparadorMiles, ""));
            eObligacionesNuevas.cod_periodicidad = Convert.ToInt64(ddlPeriodicidadF.SelectedValue);
            eObligacionesNuevas.plazo = Convert.ToDecimal(txtPlazoOblF.Text.Replace(gSeparadorMiles, ""));
            eObligacionesNuevas.gracia = Convert.ToDecimal(txtGraciaF.Text.Replace(gSeparadorMiles, ""));
            eObligacionesNuevas = PresupuestoServicio.CrearObligacion(eObligacionesNuevas, (Usuario)Session["Usuario"]);

            dtObligacionesNuevas.Rows.Add(fila);
            gvObligacionesNuevas.DataSource = dtObligacionesNuevas;
            gvObligacionesNuevas.DataBind();
            Session["DTOBLIGACIONESNUEVAS"] = dtObligacionesNuevas;

            GenerarTotalesObligaciones();

        }

    }

    protected void gvObligacionesNuevas_RowEditing(object sender, GridViewEditEventArgs e)
    {
        int conseID = 0;
        if (gvObligacionesNuevas.DataKeys[e.NewEditIndex] != null)
        {
            try
            {
                conseID = Convert.ToInt32(gvObligacionesNuevas.DataKeys[e.NewEditIndex].Values[0].ToString());
            }
            catch
            {
                conseID = 0;
            }
        }
        gvObligacionesNuevas.EditIndex = e.NewEditIndex;
        gvObligacionesNuevas.DataSource = Session["DTOBLIGACIONESNUEVAS"];
        gvObligacionesNuevas.DataBind();
    }

    protected void gvObligacionesNuevas_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvObligacionesNuevas.EditIndex = -1;
        gvObligacionesNuevas.DataSource = Session["DTOBLIGACIONESNUEVAS"];
        gvObligacionesNuevas.DataBind();
    }

    protected void gvObligacionesNuevas_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            // Cargando los datatable
            DataTable dtObligacionesNuevas = new DataTable();
            dtObligacionesNuevas = (DataTable)Session["DTOBLIGACIONESNUEVAS"];

            // Ubicarse en la fila a modificar en el datatable
            string llave = "";
            llave = e.Keys[0].ToString();
            DataRow fila = dtObligacionesNuevas.Rows.Find(llave);

            // Actualizando filas del datatable
            GridViewRow gvrObligacionesNuevas = gvObligacionesNuevas.Rows[e.RowIndex];

            TextBox txtCodigo = (TextBox)gvrObligacionesNuevas.FindControl("txtCodigo");
            TextBox txtDescripcion = (TextBox)gvrObligacionesNuevas.FindControl("txtDescripcion");
            TextBox txtCupo = (TextBox)gvrObligacionesNuevas.FindControl("txtCupo");
            TextBox txtTasa = (TextBox)gvrObligacionesNuevas.FindControl("txtTasa");
            DropDownList ddlPeriodicidad = (DropDownList)gvrObligacionesNuevas.FindControl("ddlPeriodicidad");
            TextBox txtPlazoObl = (TextBox)gvrObligacionesNuevas.FindControl("txtPlazoObl");
            TextBox txtGracia = (TextBox)gvrObligacionesNuevas.FindControl("txtGracia");

            fila["codigo"] = txtCodigo.Text;
            fila["descripcion"] = txtDescripcion.Text;
            if (txtCupo.Text.Trim() != "")
                fila["cupo"] = Convert.ToDecimal(txtCupo.Text.Replace(gSeparadorMiles, ""));
            else
                fila["cupo"] = 0;
            if (txtTasa.Text.Trim() != "")
                fila["tasa"] = Convert.ToDecimal(txtTasa.Text.Replace(gSeparadorMiles, ""));
            else
                fila["tasa"] = 0;
            fila["cod_periodicidad"] = Convert.ToInt64(ddlPeriodicidad.SelectedValue);
            fila["plazo"] = Convert.ToDecimal(txtPlazoObl.Text.Replace(gSeparadorMiles, ""));
            fila["gracia"] = Convert.ToDecimal(txtGracia.Text.Replace(gSeparadorMiles, ""));
            
            string valor = "";
            for (var index = 1; index < e.NewValues.Count; index++)
            {
                if (e.NewValues[index] != null)
                {
                    valor = e.NewValues[index].ToString();
                    if (fila != null)
                    {
                        fila[index+7] = valor;
                        fila.AcceptChanges();
                    }
                }
            }

            Xpinn.Presupuesto.Entities.Obligacion eObligacionesNuevas = new Xpinn.Presupuesto.Entities.Obligacion();
            eObligacionesNuevas.codigo = Convert.ToInt64(txtCodigo.Text);
            eObligacionesNuevas.descripcion = txtDescripcion.Text.ToUpper();
            eObligacionesNuevas.valor = Convert.ToDecimal(txtCupo.Text.Replace(gSeparadorMiles, ""));
            eObligacionesNuevas.tasa = Convert.ToDecimal(txtTasa.Text.Replace(gSeparadorMiles, ""));
            eObligacionesNuevas.cod_periodicidad = Convert.ToInt64(ddlPeriodicidad.SelectedValue);
            eObligacionesNuevas.plazo = Convert.ToDecimal(txtPlazoObl.Text.Replace(gSeparadorMiles, ""));
            eObligacionesNuevas.gracia = Convert.ToDecimal(txtGracia.Text.Replace(gSeparadorMiles, ""));
            eObligacionesNuevas.dtObligacionesNuevas = new DataTable();
            eObligacionesNuevas.dtObligacionesNuevas = dtObligacionesNuevas;
            eObligacionesNuevas = PresupuestoServicio.ModificarObligacion(eObligacionesNuevas, (Usuario)Session["Usuario"]);

            dtObligacionesNuevas.AcceptChanges();

            // Actualizando la grilla            
            gvObligacionesNuevas.EditIndex = -1;
            gvObligacionesNuevas.DataSource = dtObligacionesNuevas;
            gvObligacionesNuevas.DataBind();

            // Actualizando variable de sesión
            Session["DTOBLIGACIONESNUEVAS"] = dtObligacionesNuevas;

            GenerarTotalesObligaciones();
            Totalizar_ObligacionesNuevas();
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
            // mensaje de error
        }
    }

    protected void CrearObligacionesNuevasInicial(ref DataTable dt)
    {
        DataRow dr;
        dr = dt.NewRow();
        dr[0] = "0";
        dt.Rows.Add(dr);
    }

    protected void Totalizar_Obligaciones()
    {
        DataTable dtObligaciones = new DataTable();
        dtObligaciones = (DataTable)Session["DTOBLIGACIONES"];
        DataTable dtFechas = new DataTable();
        dtFechas = (DataTable)Session["DTFECHAS"];
        // Calcular los totales de Obligaciones viejas
        GridViewRow rFilaO = gvObligaciones.FooterRow;
        if (rFilaO != null)
        {
            rFilaO.Cells[0].Text = "TOTALES";
            rFilaO.Font.Bold = true;
            Double TotalSaldo = 0;
            foreach (DataRow rValor in dtObligaciones.Rows)
            {
                TotalSaldo = TotalSaldo + Convert.ToDouble(rValor[2]);
            }
            rFilaO.Cells[2].Text = TotalSaldo.ToString("N2");
            int numPeriodo = 0;
            foreach (DataRow drFecha in dtFechas.Rows)
            {
                TotalSaldo = 0;
                foreach (DataRow rValor in dtObligaciones.Rows)
                {
                    TotalSaldo = TotalSaldo + Convert.ToDouble(rValor[numPeriodo + 3]);
                }
                rFilaO.Cells[numPeriodo + 3].Text = TotalSaldo.ToString("N2");
                numPeriodo += 1;
            }
        }

        // Mostrar el total de todas las obligaciones en el footer
        GridViewRow rFilaT = gvObligacionesTot.FooterRow;
        if (rFilaT != null)
        {
            rFilaT.Cells[0].Text = "TOTALES";
            rFilaT.Font.Bold = true;
            int numeroPeriodo = 0;
            foreach (DataRow drFila in dtFechas.Rows)
            {
                Double TotalSaldo = 0;
                foreach (GridViewRow rValor in gvObligacionesTot.Rows)
                {
                    TotalSaldo = TotalSaldo + Convert.ToDouble(rValor.Cells[numeroPeriodo + 2].Text);
                }
                rFilaT.Cells[numeroPeriodo + 2].Text = TotalSaldo.ToString("N2");
                numeroPeriodo += 1;
            }
        }

        // Mostrar el total los pagos en el footer
        GridViewRow rFilaTP = gvObligacionesTotPagos.FooterRow;
        if (rFilaTP != null)
        {
            rFilaTP.Cells[0].Text = "TOTALES";
            rFilaTP.Font.Bold = true;
            int numeroPeriodo = 0;
            foreach (DataRow drFila in dtFechas.Rows)
            {
                Double TotalSaldo = 0;
                foreach (GridViewRow rValor in gvObligacionesTotPagos.Rows)
                {
                    TotalSaldo = TotalSaldo + Convert.ToDouble(rValor.Cells[numeroPeriodo + 2].Text);
                }
                rFilaTP.Cells[numeroPeriodo + 2].Text = TotalSaldo.ToString("N2");
                numeroPeriodo += 1;
            }
        }
    }

    protected void Totalizar_ObligacionesNuevas()
    {
        DataTable dtFechas = new DataTable();
        dtFechas = (DataTable)Session["DTFECHAS"];

        // Mostrar el total de las obligaciones nuevas
        GridViewRow rFilaTN = gvObligacionesNuevas.FooterRow;
        if (rFilaTN != null)
        {
            rFilaTN.Font.Bold = true;
            int numeroPeriodo = 0;
            foreach (DataRow drFila in dtFechas.Rows)
            {
                Double TotalSaldo = 0;
                foreach (GridViewRow rValor in gvObligacionesNuevas.Rows)
                {
                    if (rValor.Cells[numeroPeriodo + 8].Text.Trim() != "" && rValor.Cells[numeroPeriodo + 8].Text.Trim() != "&nbsp;")
                        TotalSaldo = TotalSaldo + Convert.ToDouble(rValor.Cells[numeroPeriodo + 8].Text);
                }
                rFilaTN.Cells[numeroPeriodo + 8].Text = TotalSaldo.ToString("N2");
                rFilaTN.Cells[numeroPeriodo + 8].Attributes.CssStyle.Add("TEXT-ALIGN", "right");
                numeroPeriodo += 1;
            }
        }

        // Mostrar la columna de totales
        foreach (GridViewRow rValor in gvObligacionesNuevas.Rows)
        {
            Double TotalSaldo = 0;
            int numeroPeriodo = 0;
            foreach (DataRow drFila in dtFechas.Rows)
            {
                if (rValor.Cells[numeroPeriodo + 8].Text.Trim() != "" && rValor.Cells[numeroPeriodo + 8].Text.Trim() != "&nbsp;")
                    TotalSaldo = TotalSaldo + Convert.ToDouble(rValor.Cells[numeroPeriodo + 8].Text);
                numeroPeriodo += 1;
            }
            numeroPeriodo += 1;
            rValor.Cells[numeroPeriodo + 8].Text = TotalSaldo.ToString("N2");
            rValor.Cells[numeroPeriodo + 8].Attributes.CssStyle.Add("TEXT-ALIGN", "right");
        }
    }

    protected void Calcular_Obligaciones(Boolean bMayorizar)
    {
        // Cargar valores al presupuesto
        DataTable dtObligaciones = new DataTable();
        dtObligaciones = (DataTable)Session["DTOBLIGACIONES"];
        DataTable dtObligacionesNuevas = new DataTable();
        dtObligacionesNuevas = (DataTable)Session["DTOBLIGACIONESNUEVAS"];
        DataTable dtObligacionesPagos = new DataTable();
        dtObligacionesPagos = (DataTable)Session["DTOBLIGACIONESPAGOS"];
        DataTable dtPresupuesto = new DataTable();
        dtPresupuesto = (DataTable)Session["DTPRESUPUESTO"];
        DataTable dtFechas = new DataTable();
        dtFechas = (DataTable)Session["DTFECHAS"];
        PresupuestoServicio.CargarPresupuestoObligacion(ref dtPresupuesto, dtObligaciones, dtObligacionesNuevas, dtObligacionesPagos, dtFechas, true, bMayorizar, (Usuario)Session["Usuario"], 4);
        gvProyeccion.DataSource = dtPresupuesto;
        gvProyeccion.DataBind();
    }

    protected void GenerarTotalesObligaciones()
    {
        Usuario vusuario;
        vusuario = (Usuario)Session["Usuario"];
        DataTable dtObligacionesTot = new DataTable();
        dtObligacionesTot = (DataTable)Session["DTOBLIGACIONESTOT"];
        DataTable dtObligacionesTotPagos = new DataTable();
        dtObligacionesTotPagos = (DataTable)Session["DTOBLIGACIONESTOTPAGOS"];
        DataTable dtObligaciones = new DataTable();
        dtObligaciones = (DataTable)Session["DTOBLIGACIONES"];
        DataTable dtObligacionesNuevas = new DataTable();
        dtObligacionesNuevas = (DataTable)Session["DTOBLIGACIONESNUEVAS"];
        DataTable dtObligacionesPagos = new DataTable();
        dtObligacionesPagos = (DataTable)Session["DTOBLIGACIONESPAGOS"];
        DataTable dtFechas = new DataTable();
        dtFechas = (DataTable)Session["DTFECHAS"];
        try
        {
            PresupuestoServicio.ConsultarTotalObligaciones(ref dtObligacionesTot, ref dtObligacionesTotPagos, dtObligaciones, dtObligacionesNuevas, dtObligacionesPagos, dtFechas, vusuario);
            gvObligacionesTot.DataSource = dtObligacionesTot;
            gvObligacionesTot.DataBind();
            gvObligacionesTotPagos.DataSource = dtObligacionesTotPagos;
            gvObligacionesTotPagos.DataBind();
        }
        catch
        {
            VerError("");
        }
    }

    #endregion ObligacionesFinancieras

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //  PRESUPUESTO DE DIFERIDOS
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    #region Diferidos

    protected void btnRegresarDIF_Click(object sender, ImageClickEventArgs e)
    {
        Site toolBar = (Site)this.Master;
        toolBar.MostrarGuardar(true);
        Calcular_Diferidos(true);
        GenerarFlujo();
        mvPresupuesto.ActiveViewIndex = 1;
    }

    protected void gvDiferidos_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblValor = (Label)e.Row.FindControl("lblValor");
                if (lblValor != null)
                    lblValor.Attributes.CssStyle.Add("TEXT-ALIGN", "right");
            }

            if (e.Row.RowType == DataControlRowType.Footer)
            {
                List<Xpinn.Caja.Entities.Oficina> LstOficina;
                DropDownList ddlCodOficinaF = (DropDownList)e.Row.FindControl("ddlCodOficinaF");
                if (ddlCodOficinaF != null)
                {
                    LstOficina = ListaOficinas();
                    ddlCodOficinaF.DataSource = LstOficina;
                    ddlCodOficinaF.DataTextField = "cod_oficina";
                    ddlCodOficinaF.DataValueField = "nombre";
                    ddlCodOficinaF.SelectedValue = "";
                }

                TextBox txtValorF = (TextBox)e.Row.FindControl("txtValorF");
                txtValorF.Attributes.CssStyle.Add("TEXT-ALIGN", "right");
            }
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }

    protected void gvDiferidos_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            DataTable dtDiferidos = new DataTable();
            dtDiferidos = (DataTable)Session["DTDIFERIDOS"];

            if ((e.RowIndex == 0) && (dtDiferidos.Rows[0][0] != null || dtDiferidos.Rows[0][0].ToString() == "0") && (dtDiferidos.Rows.Count == 1))
            {
                CrearDiferidoInicial(ref dtDiferidos);
            }

            Label lblCodigo = (Label)gvDiferidos.Rows[e.RowIndex].FindControl("lblCodigo");
            Xpinn.Presupuesto.Entities.Diferidos eDiferidos = new Xpinn.Presupuesto.Entities.Diferidos();
            eDiferidos.codigo = Convert.ToInt64(lblCodigo.Text);
            if (eDiferidos.codigo != 0)
                PresupuestoServicio.EliminarDiferido(eDiferidos, (Usuario)Session["Usuario"]);

            dtDiferidos.Rows[e.RowIndex].Delete();
            gvDiferidos.DataSource = dtDiferidos;
            gvDiferidos.DataBind();
            Session["DTDIFERIDOS"] = dtDiferidos;

            if ((e.RowIndex == 0) && (dtDiferidos.Rows[0][0] == null || dtDiferidos.Rows[0][0].ToString() == "") && (dtDiferidos.Rows.Count == 1))
                gvDiferidos.Rows[0].Visible = false;

        }
        catch (Xpinn.Util.ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(PresupuestoServicio.GetType().Name + "L", "gvDiferidos_RowDeleting", ex);
        }
    }

    protected void gvDiferidos_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("AddNew"))
        {
            TextBox txtCodigoF = (TextBox)gvDiferidos.FooterRow.FindControl("txtCodigoF");
            TextBox txtDescripcionF = (TextBox)gvDiferidos.FooterRow.FindControl("txtDescripcionF");
            DropDownList ddlCodOficinaF = (DropDownList)gvDiferidos.FooterRow.FindControl("ddlCodOficinaF");
            TextBox txtValorF = (TextBox)gvDiferidos.FooterRow.FindControl("txtValorF");
            TextBox txtPlazoF = (TextBox)gvDiferidos.FooterRow.FindControl("txtPlazoF");
            TextBox txtFechaDiferidoF = (TextBox)gvDiferidos.FooterRow.FindControl("txtFechaDiferidoF");

            DataTable dtDiferidos = new DataTable();
            dtDiferidos = (DataTable)Session["DTDIFERIDOS"];

            if (dtDiferidos.Rows[0][0] == null || dtDiferidos.Rows[0][0].ToString() == "0")
            {
                dtDiferidos.Rows[0].Delete();
            }

            DataRow fila = dtDiferidos.NewRow();
            fila[0] = txtCodigoF.Text;
            fila[1] = txtDescripcionF.Text;
            fila[2] = Convert.ToInt64(ddlCodOficinaF.SelectedValue);
            fila[3] = Convert.ToDecimal(txtValorF.Text.Replace(gSeparadorMiles, ""));
            fila[4] = Convert.ToDecimal(txtPlazoF.Text.Replace(gSeparadorMiles, ""));
            fila[5] = Convert.ToDateTime(txtFechaDiferidoF.Text);

            Xpinn.Presupuesto.Entities.Diferidos eDiferidos = new Xpinn.Presupuesto.Entities.Diferidos();
            eDiferidos.codigo = Convert.ToInt64(txtCodigoF.Text);
            eDiferidos.descripcion = txtDescripcionF.Text.ToUpper();
            eDiferidos.cod_oficina = Convert.ToInt64(ddlCodOficinaF.SelectedValue);
            eDiferidos.valor = Convert.ToDecimal(txtValorF.Text.Replace(gSeparadorMiles, ""));
            eDiferidos.plazo = Convert.ToDecimal(txtPlazoF.Text.Replace(gSeparadorMiles, ""));
            eDiferidos.fecha_diferido = Convert.ToDateTime(txtFechaDiferidoF.Text);
            eDiferidos = PresupuestoServicio.CrearDiferido(eDiferidos, (Usuario)Session["Usuario"]);

            dtDiferidos.Rows.Add(fila);
            gvDiferidos.DataSource = dtDiferidos;
            gvDiferidos.DataBind();
            Session["DTDIFERIDOS"] = dtDiferidos;

        }

    }

    protected void gvDiferidos_RowEditing(object sender, GridViewEditEventArgs e)
    {
        int conseID = 0;
        if (gvDiferidos.DataKeys[e.NewEditIndex] != null)
        {
            try
            {
                conseID = Convert.ToInt32(gvDiferidos.DataKeys[e.NewEditIndex].Values[0].ToString());
            }
            catch
            {
                conseID = 0;
            }
        }
        gvDiferidos.EditIndex = e.NewEditIndex;
        gvDiferidos.DataSource = Session["DTDIFERIDOS"];
        gvDiferidos.DataBind();
    }

    protected void gvDiferidos_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvDiferidos.EditIndex = -1;
        gvDiferidos.DataSource = Session["DTDIFERIDOS"];
        gvDiferidos.DataBind();
    }

    protected void gvDiferidos_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            // Cargando los datatable
            DataTable dtDiferidos = new DataTable();
            dtDiferidos = (DataTable)Session["DTDIFERIDOS"];

            // Ubicarse en la fila a modificar en el datatable
            string llave = "";
            llave = e.Keys[0].ToString();
            DataRow fila = dtDiferidos.Rows.Find(llave);

            // Actualizando filas del datatable
            GridViewRow gvrDiferidos = gvDiferidos.Rows[e.RowIndex];

            TextBox txtCodigo = (TextBox)gvrDiferidos.FindControl("txtCodigo");
            TextBox txtDescripcion = (TextBox)gvrDiferidos.FindControl("txtDescripcion");
            DropDownList ddlCodOficina = (DropDownList)gvrDiferidos.FindControl("ddlCodOficina");
            TextBox txtValor = (TextBox)gvrDiferidos.FindControl("txtValor");
            TextBox txtPlazo = (TextBox)gvrDiferidos.FindControl("txtPlazo");
            TextBox txtFechaDiferido = (TextBox)gvrDiferidos.FindControl("txtFechaDiferido");

            fila[0] = txtCodigo.Text;
            fila[1] = txtDescripcion.Text;
            fila[2] = Convert.ToInt64(ddlCodOficina.SelectedValue);
            fila[3] = Convert.ToDecimal(txtValor.Text.Replace(gSeparadorMiles, ""));
            fila[4] = Convert.ToDecimal(txtPlazo.Text.Replace(gSeparadorMiles, ""));
            fila[5] = Convert.ToDateTime(txtFechaDiferido.Text);

            Xpinn.Presupuesto.Entities.Diferidos eDiferidos = new Xpinn.Presupuesto.Entities.Diferidos();
            eDiferidos.codigo = Convert.ToInt64(txtCodigo.Text);
            eDiferidos.descripcion = txtDescripcion.Text.ToUpper();
            eDiferidos.cod_oficina = Convert.ToInt64(ddlCodOficina.SelectedValue);
            eDiferidos.valor = Convert.ToDecimal(txtValor.Text.Replace(gSeparadorMiles, ""));
            eDiferidos.plazo = Convert.ToDecimal(txtPlazo.Text.Replace(gSeparadorMiles, ""));
            eDiferidos.fecha_diferido = Convert.ToDateTime(txtFechaDiferido.Text);
            eDiferidos = PresupuestoServicio.ModificarDiferido(eDiferidos, (Usuario)Session["Usuario"]);

            dtDiferidos.AcceptChanges();

            // Actualizando la grilla            
            gvDiferidos.EditIndex = -1;
            gvDiferidos.DataSource = dtDiferidos;
            gvDiferidos.DataBind();

            // Actualizando variable de sesión
            Session["DTDIFERIDOS"] = dtDiferidos;
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
            // mensaje de error
        }
    }

    public void Calcular_Diferidos(Boolean bMayorizar)
    {
        // Cargando los datatable
        DataTable dtDiferidos = new DataTable();
        dtDiferidos = (DataTable)Session["DTDIFERIDOS"];
        DataTable dtFechas = new DataTable();
        dtFechas = (DataTable)Session["DTFECHAS"];
        DataTable dtPresupuesto = new DataTable();
        dtPresupuesto = (DataTable)Session["DTPRESUPUESTO"];

        // Cargar valores al presupuesto
        PresupuestoServicio.CargarPresupuestoDiferidos(ref dtPresupuesto, dtDiferidos, dtFechas, bMayorizar, (Usuario)Session["Usuario"], 4);
        Session["DTPRESUPUESTO"] = dtPresupuesto;
        gvProyeccion.DataSource = dtPresupuesto;
        gvProyeccion.DataBind();
    }

    protected void CrearDiferidoInicial(ref DataTable dt)
    {     
        DataRow dr;
        dr = dt.NewRow();
        dr[0] = "0";
        dt.Rows.Add(dr);
    }

    #endregion Diferidos

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //  PRESUPUESTO DE OTROS CONCEPTOS
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    #region Otros

    protected void btnRegresarOTR_Click(object sender, ImageClickEventArgs e)
    {
        Calcular_Otros(true);
        Calcular_Honorarios(true);
        GenerarFlujo();
        Site toolBar = (Site)this.Master;
        toolBar.MostrarGuardar(true);

        mvPresupuesto.ActiveViewIndex = 1;
    }

    protected void gvOtros_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblArriendo = (Label)e.Row.FindControl("lblArriendo");
                if (lblArriendo != null)
                    lblArriendo.Attributes.CssStyle.Add("TEXT-ALIGN", "right");
                Label lblServicios = (Label)e.Row.FindControl("lblServicios");
                if (lblServicios != null)
                    lblServicios.Attributes.CssStyle.Add("TEXT-ALIGN", "right");
                Label lblVigilancia = (Label)e.Row.FindControl("lblVigilancia");
                if (lblVigilancia != null)
                    lblVigilancia.Attributes.CssStyle.Add("TEXT-ALIGN", "right");
            }

            if (e.Row.RowType == DataControlRowType.Footer)
            {
                TextBox txtArriendoF = (TextBox)e.Row.FindControl("txtArriendoF");
                if (txtArriendoF != null)
                    txtArriendoF.Attributes.CssStyle.Add("TEXT-ALIGN", "right");                
                TextBox txtServiciosF = (TextBox)e.Row.FindControl("txtServiciosF");
                if (txtServiciosF != null)
                    txtServiciosF.Attributes.CssStyle.Add("TEXT-ALIGN", "right");
                TextBox txtVigilanciaF = (TextBox)e.Row.FindControl("txtVigilanciaF");
                if (txtVigilanciaF != null)
                    txtVigilanciaF.Attributes.CssStyle.Add("TEXT-ALIGN", "right");
            }
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }

    protected void gvOtros_RowEditing(object sender, GridViewEditEventArgs e)
    {
        int conseID = 0;
        if (gvOtros.DataKeys[e.NewEditIndex] != null)
        {
            try
            {
                conseID = Convert.ToInt32(gvOtros.DataKeys[e.NewEditIndex].Values[0].ToString());
            }
            catch
            {
                conseID = 0;
            }
        }
        gvOtros.EditIndex = e.NewEditIndex;
        gvOtros.DataSource = Session["DTOTROS"];
        gvOtros.DataBind();
    }

    protected void gvOtros_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvOtros.EditIndex = -1;
        gvOtros.DataSource = Session["DTOTROS"];
        gvOtros.DataBind();
        Totalizar_Otros();
    }

    protected void gvOtros_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            // Cargando los datatable
            DataTable dtOtros = new DataTable();
            dtOtros = (DataTable)Session["DTOTROS"];

            // Ubicarse en la fila a modificar en el datatable
            string llave = "";
            llave = e.Keys[0].ToString();
            DataRow fila = dtOtros.Rows.Find(llave);

            // Actualizando filas del datatable
            GridViewRow gvrOtros = gvOtros.Rows[e.RowIndex];

            TextBox txtCodigo = (TextBox)gvrOtros.FindControl("txtCodigo");
            TextBox txtDescripcion = (TextBox)gvrOtros.FindControl("txtDescripcion");
            TextBox txtArriendo_ant = (TextBox)gvrOtros.FindControl("txtArriendo_ant");
            TextBox txtServicios_ant = (TextBox)gvrOtros.FindControl("txtServicios_ant");
            TextBox txtVigilancia_ant = (TextBox)gvrOtros.FindControl("txtVigilancia_ant");
            TextBox txtIncremento_Arriendo = (TextBox)gvrOtros.FindControl("txtIncremento_Arriendo");
            TextBox txtIncremento_Servicios = (TextBox)gvrOtros.FindControl("txtIncremento_Servicios");
            TextBox txtIncremento_Vigilancia = (TextBox)gvrOtros.FindControl("txtIncremento_Vigilancia");

            fila[0] = txtCodigo.Text;
            fila[1] = txtDescripcion.Text;
            fila[2] = Convert.ToDecimal(txtArriendo_ant.Text.Replace(gSeparadorMiles, ""));
            fila[3] = Convert.ToDecimal(txtServicios_ant.Text.Replace(gSeparadorMiles, ""));
            fila[4] = Convert.ToDecimal(txtVigilancia_ant.Text.Replace(gSeparadorMiles, ""));
            fila[5] = Convert.ToDecimal(txtIncremento_Arriendo.Text.Replace(gSeparadorMiles, ""));
            fila[6] = Convert.ToDecimal(txtIncremento_Servicios.Text.Replace(gSeparadorMiles, ""));
            fila[7] = Convert.ToDecimal(txtIncremento_Vigilancia.Text.Replace(gSeparadorMiles, ""));

            Xpinn.Presupuesto.Entities.Otros eOtros = new Xpinn.Presupuesto.Entities.Otros();
            eOtros.codigo = Convert.ToInt64(txtCodigo.Text);
            eOtros.descripcion = txtDescripcion.Text.ToUpper();
            eOtros.arriendo_ant = Convert.ToDecimal(txtArriendo_ant.Text.Replace(gSeparadorMiles, ""));
            eOtros.servicios_ant = Convert.ToDecimal(txtServicios_ant.Text.Replace(gSeparadorMiles, ""));
            eOtros.vigilancia_ant = Convert.ToDecimal(txtVigilancia_ant.Text.Replace(gSeparadorMiles, ""));
            eOtros.incremento_arriendo = Convert.ToDecimal(txtIncremento_Arriendo.Text.Replace(gSeparadorMiles, ""));
            eOtros.incremento_servicios = Convert.ToDecimal(txtIncremento_Servicios.Text.Replace(gSeparadorMiles, ""));
            eOtros.incremento_vigilancia = Convert.ToDecimal(txtIncremento_Vigilancia.Text.Replace(gSeparadorMiles, ""));
            eOtros = PresupuestoServicio.ModificarOtro(eOtros, (Usuario)Session["Usuario"]);
            fila[8] = eOtros.arriendo;
            fila[9] = eOtros.servicios;
            fila[10] = eOtros.vigilancia;

            dtOtros.AcceptChanges();

            // Actualizando la grilla            
            gvOtros.EditIndex = -1;
            gvOtros.DataSource = dtOtros;
            gvOtros.DataBind();

            // Actualizando variable de sesión
            Session["DTOTROS"] = dtOtros;
            Totalizar_Otros();
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
            // mensaje de error
        }
    }

    public void Calcular_Otros(Boolean bMayorizar)
    {
        // Cargando los datatable
        DataTable dtOtros = new DataTable();
        dtOtros = (DataTable)Session["DTOTROS"];
        DataTable dtFechas = new DataTable();
        dtFechas = (DataTable)Session["DTFECHAS"];
        DataTable dtPresupuesto = new DataTable();
        dtPresupuesto = (DataTable)Session["DTPRESUPUESTO"];

        // Cargar valores al presupuesto
        PresupuestoServicio.CargarPresupuestoOtros(ref dtPresupuesto, dtOtros, dtFechas, bMayorizar, (Usuario)Session["Usuario"], 4);
        Session["DTPRESUPUESTO"] = dtPresupuesto;
        gvProyeccion.DataSource = dtPresupuesto;
        gvProyeccion.DataBind();
    }

    public void Totalizar_Otros()
    {
        DataTable dtOtros = new DataTable();
        dtOtros = (DataTable)Session["DTOTROS"];
        // Calcular los totales de otros conceptos
        GridViewRow rFilaOt = gvOtros.FooterRow;
        if (rFilaOt != null)
        {
            rFilaOt.Cells[3].Text = "TOTALES";
            rFilaOt.Font.Bold = true;
            Double TotalArriendoAnt = 0;
            Double TotalServiciosAnt = 0;
            Double TotalVigilanciaAnt = 0;
            Double TotalArriendo = 0;
            Double TotalServicios = 0;
            Double TotalVigilancia = 0;
            foreach (DataRow rValor in dtOtros.Rows)
            {
                if (rValor["ARRIENDO_ANT"].ToString() != "")
                    TotalArriendoAnt = TotalArriendoAnt + Convert.ToDouble(rValor["ARRIENDO_ANT"]);
                if (rValor["SERVICIOS_ANT"].ToString() != "")
                    TotalServiciosAnt = TotalServiciosAnt + Convert.ToDouble(rValor["SERVICIOS_ANT"]);
                if (rValor["VIGILANCIA_ANT"].ToString() != "")
                    TotalVigilanciaAnt = TotalVigilanciaAnt + Convert.ToDouble(rValor["VIGILANCIA_ANT"]);
                if (rValor["ARRIENDO"].ToString() != "")
                    TotalArriendo = TotalArriendo + Convert.ToDouble(rValor["ARRIENDO"]);
                if (rValor["SERVICIOS"].ToString() != "")
                    TotalServicios = TotalServicios + Convert.ToDouble(rValor["SERVICIOS"]);
                if (rValor["VIGILANCIA"].ToString() != "")
                    TotalVigilancia = TotalVigilancia + Convert.ToDouble(rValor["VIGILANCIA"]);
            }
            rFilaOt.Cells[4].Text = TotalArriendoAnt.ToString("N2");
            rFilaOt.Cells[7].Text = TotalServiciosAnt.ToString("N2");
            rFilaOt.Cells[10].Text = TotalVigilanciaAnt.ToString("N2");
            rFilaOt.Cells[6].Text = TotalArriendo.ToString("N2");
            rFilaOt.Cells[9].Text = TotalServicios.ToString("N2");
            rFilaOt.Cells[12].Text = TotalVigilancia.ToString("N2");
        }
    }

    #endregion Otros

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //  PRESUPUESTO DE HONORARIOS
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    #region Honorarios

    protected void gvHonorarios_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblValor = (Label)e.Row.FindControl("lblValor");
                if (lblValor != null)
                    lblValor.Attributes.CssStyle.Add("TEXT-ALIGN", "right");
                Label lblValorAnt = (Label)e.Row.FindControl("lblValorAnt");
                if (lblValorAnt != null)
                    lblValorAnt.Attributes.CssStyle.Add("TEXT-ALIGN", "right");
            }

            if (e.Row.RowType == DataControlRowType.Footer)
            {
                TextBox txtValorAnt = (TextBox)e.Row.FindControl("txtValorAnt");
                if (txtValorAnt != null)
                    txtValorAnt.Attributes.CssStyle.Add("TEXT-ALIGN", "right");
            }
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }

    protected void gvHonorarios_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            DataTable dtHonorarios = new DataTable();
            dtHonorarios = (DataTable)Session["DTHONORARIOS"];

            if ((e.RowIndex == 0) && (dtHonorarios.Rows[0][0] != null || dtHonorarios.Rows[0][0].ToString() == "0") && (dtHonorarios.Rows.Count == 1))
            {
                CrearHonorariosInicial(ref dtHonorarios);
            }

            Label lblCodigo = (Label)gvHonorarios.Rows[e.RowIndex].FindControl("lblCodigo");
            Xpinn.Presupuesto.Entities.Honorarios eHonorarios = new Xpinn.Presupuesto.Entities.Honorarios();
            eHonorarios.codigo = Convert.ToInt64(lblCodigo.Text);
            if (eHonorarios.codigo != 0)
                PresupuestoServicio.EliminarHonorario(eHonorarios, (Usuario)Session["Usuario"]);

            dtHonorarios.Rows[e.RowIndex].Delete();
            gvHonorarios.DataSource = dtHonorarios;
            gvHonorarios.DataBind();
            Session["DTHONORARIOS"] = dtHonorarios;
            Totalizar_Honorarios();

            if ((e.RowIndex == 0) && (dtHonorarios.Rows[0][0] == null || dtHonorarios.Rows[0][0].ToString() == "") && (dtHonorarios.Rows.Count == 1))
                gvHonorarios.Rows[0].Visible = false;

        }
        catch (Xpinn.Util.ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(PresupuestoServicio.GetType().Name + "L", "gvHonorarios_RowDeleting", ex);
        }
    }

    protected void gvHonorarios_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("AddNew"))
        {
            TextBox txtCodigoF = (TextBox)gvHonorarios.FooterRow.FindControl("txtCodigoF");
            TextBox txtDescripcionF = (TextBox)gvHonorarios.FooterRow.FindControl("txtDescripcionF");
            TextBox txtValorAntF = (TextBox)gvHonorarios.FooterRow.FindControl("txtValorAntF");
            TextBox txtIncrementoF = (TextBox)gvHonorarios.FooterRow.FindControl("txtIncrementoF");
            

            DataTable dtHonorarios = new DataTable();
            dtHonorarios = (DataTable)Session["DTHONORARIOS"];

            if (dtHonorarios.Rows[0][0] == null || dtHonorarios.Rows[0][0].ToString() == "0")
            {
                dtHonorarios.Rows[0].Delete();
            }

            DataRow fila = dtHonorarios.NewRow();
            fila[0] = txtCodigoF.Text;
            fila[1] = txtDescripcionF.Text;
            fila[2] = Convert.ToDecimal(txtValorAntF.Text.Replace(gSeparadorMiles, ""));
            fila[3] = Convert.ToDecimal(txtIncrementoF.Text.Replace(gSeparadorMiles, ""));            

            Xpinn.Presupuesto.Entities.Honorarios eHonorarios = new Xpinn.Presupuesto.Entities.Honorarios();
            eHonorarios.codigo = Convert.ToInt64(txtCodigoF.Text);
            eHonorarios.descripcion = txtDescripcionF.Text.ToUpper();
            eHonorarios.valor_ant = Convert.ToDecimal(txtValorAntF.Text.Replace(gSeparadorMiles, ""));
            eHonorarios.incremento = Convert.ToDecimal(txtIncrementoF.Text.Replace(gSeparadorMiles, ""));
            eHonorarios = PresupuestoServicio.CrearHonorario(eHonorarios, (Usuario)Session["Usuario"]);
            fila[4] = eHonorarios.valor;

            dtHonorarios.Rows.Add(fila);
            gvHonorarios.DataSource = dtHonorarios;
            gvHonorarios.DataBind();
            Session["DTHONORARIOS"] = dtHonorarios;
            Totalizar_Honorarios();

        }

    }

    protected void gvHonorarios_RowEditing(object sender, GridViewEditEventArgs e)
    {
        int conseID = 0;
        if (gvHonorarios.DataKeys[e.NewEditIndex] != null)
        {
            try
            {
                conseID = Convert.ToInt32(gvHonorarios.DataKeys[e.NewEditIndex].Values[0].ToString());
            }
            catch
            {
                conseID = 0;
            }
        }
        gvHonorarios.EditIndex = e.NewEditIndex;
        gvHonorarios.DataSource = Session["DTHONORARIOS"];
        gvHonorarios.DataBind();
    }

    protected void gvHonorarios_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvHonorarios.EditIndex = -1;
        gvHonorarios.DataSource = Session["DTHONORARIOS"];
        gvHonorarios.DataBind();
    }

    protected void gvHonorarios_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            // Cargando los datatable
            DataTable dtHonorarios = new DataTable();
            dtHonorarios = (DataTable)Session["DTHONORARIOS"];

            // Ubicarse en la fila a modificar en el datatable
            string llave = "";
            llave = e.Keys[0].ToString();
            DataRow fila = dtHonorarios.Rows.Find(llave);

            // Actualizando filas del datatable
            GridViewRow gvrHonorarios = gvHonorarios.Rows[e.RowIndex];

            TextBox txtCodigo = (TextBox)gvrHonorarios.FindControl("txtCodigo");
            TextBox txtDescripcion = (TextBox)gvrHonorarios.FindControl("txtDescripcion");
            TextBox txtValorAnt = (TextBox)gvrHonorarios.FindControl("txtValorAnt");
            TextBox txtIncremento = (TextBox)gvrHonorarios.FindControl("txtIncremento");            

            fila[0] = txtCodigo.Text;
            fila[1] = txtDescripcion.Text;
            fila[2] = Convert.ToDecimal(txtValorAnt.Text.Replace(gSeparadorMiles, ""));
            fila[3] = Convert.ToDecimal(txtIncremento.Text.Replace(gSeparadorMiles, ""));            

            Xpinn.Presupuesto.Entities.Honorarios eHonorarios = new Xpinn.Presupuesto.Entities.Honorarios();
            eHonorarios.codigo = Convert.ToInt64(txtCodigo.Text);
            eHonorarios.descripcion = txtDescripcion.Text.ToUpper();
            eHonorarios.valor_ant = Convert.ToDecimal(txtValorAnt.Text.Replace(gSeparadorMiles, ""));
            eHonorarios.incremento = Convert.ToDecimal(txtIncremento.Text.Replace(gSeparadorMiles, ""));
            eHonorarios = PresupuestoServicio.ModificarHonorario(eHonorarios, (Usuario)Session["Usuario"]);
            fila[4] = eHonorarios.valor;

            dtHonorarios.AcceptChanges();

            // Actualizando la grilla            
            gvHonorarios.EditIndex = -1;
            gvHonorarios.DataSource = dtHonorarios;
            gvHonorarios.DataBind();

            // Actualizando variable de sesión
            Session["DTHONORARIOS"] = dtHonorarios;
            Totalizar_Honorarios();
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
            // mensaje de error
        }
    }

    public void Calcular_Honorarios(Boolean bMayorizar)
    {
        // Cargando los datatable
        DataTable dtHonorarios = new DataTable();
        dtHonorarios = (DataTable)Session["DTHONORARIOS"];
        DataTable dtFechas = new DataTable();
        dtFechas = (DataTable)Session["DTFECHAS"];
        DataTable dtPresupuesto = new DataTable();
        dtPresupuesto = (DataTable)Session["DTPRESUPUESTO"];

        // Cargar valores al presupuesto
        PresupuestoServicio.CargarPresupuestoHonorarios(ref dtPresupuesto, dtHonorarios, dtFechas, bMayorizar, (Usuario)Session["Usuario"], 4);
        Session["DTPRESUPUESTO"] = dtPresupuesto;
        gvProyeccion.DataSource = dtPresupuesto;
        gvProyeccion.DataBind();
    }

    protected void CrearHonorariosInicial(ref DataTable dt)
    {
        if (dt.Columns.Count == 0)
        {
            DataColumn dc1 = new DataColumn();
            dc1.ColumnName = "Codigo";
            dt.Columns.Add(dc1);
            DataColumn dc2 = new DataColumn();
            dc2.ColumnName = "Descripcion";
            dt.Columns.Add(dc2);
            DataColumn dc3 = new DataColumn();
            dc3.ColumnName = "Valor";
            dt.Columns.Add(dc3);
        }
        DataRow dr;
        dr = dt.NewRow();
        dr[0] = "0";
        dt.Rows.Add(dr);
    }

    public void Totalizar_Honorarios()
    {
        DataTable dtHonorarios = new DataTable();
        dtHonorarios = (DataTable)Session["DTHONORARIOS"];
        // Calcular los totales de honorarios
        GridViewRow rFilaOt = gvHonorarios.FooterRow;
        if (rFilaOt != null)
        {
            rFilaOt.Cells[3].Text = "TOTALES";
            rFilaOt.Font.Bold = true;
            Double Total = 0;
            foreach (DataRow rValor in dtHonorarios.Rows)
            {
                if (rValor["VALOR"].ToString() != "")
                    Total = Total + Convert.ToDouble(rValor["VALOR"]);
            }
            rFilaOt.Cells[6].Text = Total.ToString("N2");
        }
    }

    #endregion Honorarios

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //  PRESUPUESTO DE TECNOLOGIA
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    #region Tecnologia

    protected void btnRegresarTEC_Click(object sender, ImageClickEventArgs e)
    {
        Calcular_Tecnologia(true);
        GenerarFlujo();
        Site toolBar = (Site)this.Master;
        toolBar.MostrarGuardar(true);

        mvPresupuesto.ActiveViewIndex = 1;
    }

    protected void gvTecnologia_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblVrCompra = (Label)e.Row.FindControl("lblVrCompra");
                lblVrCompra.Attributes.CssStyle.Add("TEXT-ALIGN", "right");

            }

            if (e.Row.RowType == DataControlRowType.Footer)
            {
                List<Xpinn.Caja.Entities.Oficina> LstOficina;
                DropDownList ddlCodOficinaF = (DropDownList)e.Row.FindControl("ddlCodOficinaF");
                if (ddlCodOficinaF != null)
                {
                    LstOficina = ListaOficinas();
                    ddlCodOficinaF.DataSource = LstOficina;
                    ddlCodOficinaF.DataTextField = "cod_oficina";
                    ddlCodOficinaF.DataValueField = "nombre";
                    ddlCodOficinaF.SelectedValue = "";
                }

                TextBox txtVrCompraF = (TextBox)e.Row.FindControl("txtVrCompraF");
                txtVrCompraF.Attributes.CssStyle.Add("TEXT-ALIGN", "right");
            }
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }

    protected void gvTecnologia_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            DataTable dtTecnologia = new DataTable();
            dtTecnologia = (DataTable)Session["DTTECNOLOGIA"];

            if ((e.RowIndex == 0) && (dtTecnologia.Rows[0][0] != null || dtTecnologia.Rows[0][0].ToString() == "0") && (dtTecnologia.Rows.Count == 1))
            {
                CrearTecnologiaInicial(ref dtTecnologia);
            }

            Label lblCodigo = (Label)gvTecnologia.Rows[e.RowIndex].FindControl("lblCodigo");
            Xpinn.Presupuesto.Entities.Tecnologia eTecnologia = new Xpinn.Presupuesto.Entities.Tecnologia();
            eTecnologia.codigo = Convert.ToInt64(lblCodigo.Text);
            PresupuestoServicio.EliminarTecnologia(eTecnologia, (Usuario)Session["Usuario"]);

            dtTecnologia.Rows[e.RowIndex].Delete();
            dtTecnologia.AcceptChanges();
            gvTecnologia.DataSource = dtTecnologia;
            gvTecnologia.DataBind();
            Session["DTTECNOLOGIA"] = dtTecnologia;
            GenerarFlujo();

            if ((e.RowIndex == 0) && (dtTecnologia.Rows[0][0] == null || dtTecnologia.Rows[0][0].ToString() == "0") && (dtTecnologia.Rows.Count == 1))
                gvTecnologia.Rows[0].Visible = false;

        }
        catch (Xpinn.Util.ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(PresupuestoServicio.GetType().Name + "L", "gvTecnologia_RowDeleting", ex);
        }
    }

    protected void gvTecnologia_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("AddNew"))
        {
            TextBox txtCodigoF = (TextBox)gvTecnologia.FooterRow.FindControl("txtCodigoF");
            TextBox txtDescripcionF = (TextBox)gvTecnologia.FooterRow.FindControl("txtDescripcionF");
            DropDownList ddlCodOficinaF = (DropDownList)gvTecnologia.FooterRow.FindControl("ddlCodOficinaF");
            TextBox txtVrCompraF = (TextBox)gvTecnologia.FooterRow.FindControl("txtVrCompraF");
            TextBox txtFechaCompraF = (TextBox)gvTecnologia.FooterRow.FindControl("txtFechaCompraF");
            DropDownList ddlTipoActivoF = (DropDownList)gvTecnologia.FooterRow.FindControl("ddlTipoActivoF");

            DataTable dtTecnologia = new DataTable();
            dtTecnologia = (DataTable)Session["DTTECNOLOGIA"];

            if (dtTecnologia.Rows[0][0] == null || dtTecnologia.Rows[0][0].ToString() == "0")
            {
                dtTecnologia.Rows[0].Delete();
                dtTecnologia.AcceptChanges();
            }

            DataRow fila = dtTecnologia.NewRow();
            fila[0] = txtCodigoF.Text;
            fila[1] = txtDescripcionF.Text;
            fila[2] = Convert.ToInt64(ddlCodOficinaF.SelectedValue);
            fila[3] = Convert.ToDecimal(txtVrCompraF.Text.Replace(gSeparadorMiles, ""));
            fila[4] = Convert.ToDateTime(txtFechaCompraF.Text);
            fila[5] = Convert.ToInt64(ddlTipoActivoF.SelectedValue);
            fila[6] = Convert.ToString(ddlTipoActivoF.SelectedItem.Text);

            Xpinn.Presupuesto.Entities.Tecnologia eTecnologia = new Xpinn.Presupuesto.Entities.Tecnologia();
            eTecnologia.codigo = Convert.ToInt64(txtCodigoF.Text);
            eTecnologia.descripcion = txtDescripcionF.Text.ToUpper();
            eTecnologia.cod_oficina = Convert.ToInt64(ddlCodOficinaF.SelectedValue);
            eTecnologia.valor = Convert.ToDecimal(txtVrCompraF.Text.Replace(gSeparadorMiles, ""));
            eTecnologia.fecha_compra = Convert.ToDateTime(txtFechaCompraF.Text);
            eTecnologia.tipo_concepto = Convert.ToInt64(ddlTipoActivoF.SelectedValue);
            eTecnologia = PresupuestoServicio.CrearTecnologia(eTecnologia, (Usuario)Session["Usuario"]);

            dtTecnologia.Rows.Add(fila);
            gvTecnologia.DataSource = dtTecnologia;
            gvTecnologia.DataBind();
            Session["DTTECNOLOGIA"] = dtTecnologia;
            GenerarFlujo();

        }

    }

    protected void gvTecnologia_RowEditing(object sender, GridViewEditEventArgs e)
    {
        int conseID = 0;
        if (gvTecnologia.DataKeys[e.NewEditIndex] != null)
        {
            try
            {
                conseID = Convert.ToInt32(gvTecnologia.DataKeys[e.NewEditIndex].Values[0].ToString());
            }
            catch
            {
                conseID = 0;
            }
        }
        gvTecnologia.EditIndex = e.NewEditIndex;
        try
        {
            gvTecnologia.DataSource = Session["DTTECNOLOGIA"];
            gvTecnologia.DataBind();
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }

    protected void gvTecnologia_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvTecnologia.EditIndex = -1;
        gvTecnologia.DataSource = Session["DTTECNOLOGIA"];
        gvTecnologia.DataBind();
    }

    protected void gvTecnologia_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            // Cargando los datatable
            DataTable dtTecnologia = new DataTable();
            dtTecnologia = (DataTable)Session["DTTECNOLOGIA"];

            // Ubicarse en la fila a modificar en el datatable
            string llave = "";
            llave = e.Keys[0].ToString();
            DataRow fila = dtTecnologia.Rows.Find(llave);

            // Actualizando filas del datatable
            GridViewRow gvrTecnologia = gvTecnologia.Rows[e.RowIndex];

            TextBox txtCodigo = (TextBox)gvrTecnologia.FindControl("txtCodigo");
            TextBox txtDescripcion = (TextBox)gvrTecnologia.FindControl("txtDescripcion");
            DropDownList ddlCodOficina = (DropDownList)gvrTecnologia.FindControl("ddlCodOficina");
            TextBox txtVrCompra = (TextBox)gvrTecnologia.FindControl("txtVrCompra");
            TextBox txtFechaCompra = (TextBox)gvrTecnologia.FindControl("txtFechaCompra");
            DropDownList ddlTipoActivo = (DropDownList)gvrTecnologia.FindControl("ddlTipoActivo");

            fila[0] = txtCodigo.Text;
            fila[1] = txtDescripcion.Text;
            fila[2] = Convert.ToInt64(ddlCodOficina.SelectedValue);
            fila[3] = Convert.ToDecimal(txtVrCompra.Text.Replace(gSeparadorMiles, ""));
            fila[4] = Convert.ToDateTime(txtFechaCompra.Text);
            fila[5] = Convert.ToInt64(ddlTipoActivo.SelectedValue);
            fila[6] = Convert.ToString(ddlTipoActivo.SelectedItem.Text);

            Xpinn.Presupuesto.Entities.Tecnologia eTecnologia = new Xpinn.Presupuesto.Entities.Tecnologia();
            eTecnologia.codigo = Convert.ToInt64(txtCodigo.Text);
            eTecnologia.descripcion = txtDescripcion.Text.ToUpper();
            eTecnologia.cod_oficina = Convert.ToInt64(ddlCodOficina.SelectedValue);
            eTecnologia.valor = Convert.ToDecimal(txtVrCompra.Text.Replace(gSeparadorMiles, ""));
            eTecnologia.fecha_compra = Convert.ToDateTime(txtFechaCompra.Text);
            eTecnologia.tipo_concepto = Convert.ToInt64(ddlTipoActivo.SelectedValue);
            eTecnologia = PresupuestoServicio.ModificarTecnologia(eTecnologia, (Usuario)Session["Usuario"]);

            dtTecnologia.AcceptChanges();

            // Actualizando la grilla            
            gvTecnologia.EditIndex = -1;
            gvTecnologia.DataSource = dtTecnologia;
            gvTecnologia.DataBind();

            // Actualizando variable de sesión
            Session["DTTECNOLOGIA"] = dtTecnologia;
            GenerarFlujo();
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
            // mensaje de error
        }
    }

    public void Calcular_Tecnologia(Boolean bMayorizar)
    {
        // Cargando los datatable
        DataTable dtTecnologia = new DataTable();
        dtTecnologia = (DataTable)Session["DTTECNOLOGIA"];
        DataTable dtFechas = new DataTable();
        dtFechas = (DataTable)Session["DTFECHAS"];
        DataTable dtPresupuesto = new DataTable();
        dtPresupuesto = (DataTable)Session["DTPRESUPUESTO"];

        // Cargar valores al presupuesto
        PresupuestoServicio.CargarPresupuestoTecnologia(ref dtPresupuesto, dtTecnologia, dtFechas, bMayorizar, (Usuario)Session["Usuario"], 4);
        Session["DTPRESUPUESTO"] = dtPresupuesto;
        gvProyeccion.DataSource = dtPresupuesto;
        gvProyeccion.DataBind();        
    }

    protected void CrearTecnologiaInicial(ref DataTable dt)
    {
        DataRow dr;
        dr = dt.NewRow();
        dr[0] = "0";
        dt.Rows.Add(dr);
    }

    #endregion Tecnologia

    protected void btnExpFlujo_Click(object sender, EventArgs e)
    {
        ExportarExcelGrilla(gvFlujo, "PresupuestoFlujo");
    }

    protected void btnExpPresupuesto_Click(object sender, EventArgs e)
    {
        ExportarExcelDataTable("DTPRESUPUESTO", "PresupuestoPyG"); 
        //ExportarExcelGrilla(gvProyeccion, "PresupuestoPyG");
    }

    protected void btnExpNomina_Click(object sender, EventArgs e)
    {
        ExportarExcelDataTable("DTNOMINA", "PresupuertoNomina");
    }

    protected void btnExpCargos_Click(object sender, EventArgs e)
    {
        ExportarExcelDataTable("DTCARGOS", "PresupuertoCargos");
    }

    protected void btnExpObligaciones_Click(object sender, EventArgs e)
    {
        ExportarExcelDataTable("DTOBLIGACIONES", "PresupuertoObligaciones");
    }

    protected void btnExpObligacionesPagos_Click(object sender, EventArgs e)
    {
        ExportarExcelDataTable("DTOBLIGACIONESPAGOS", "PresupuestoObligacionesPagos");
    }

    protected void btnExpObligacionesTotales_Click(object sender, EventArgs e)
    {
        ExportarExcelGrilla(gvObligacionesTot, "PresupuestoObligacionesTot");
    }

    protected void btnExpColocacion_Click(object sender, EventArgs e)
    {
        ExportarExcelDataTable("DTCOLOCACION", "PresupuestoCartera");
    }

    protected void btnExpActivos_Click(object sender, EventArgs e)
    {
        ExportarExcelDataTable("DTACTIVOSFIJ", "PresupuestoActivos");
    }

    protected void btnExpTecnologia_Click(object sender, EventArgs e)
    {
        ExportarExcelDataTable("DTTECNOLOGIA", "PresupuestoTecnologia");
    }

    protected void btnExpDiferidos_Click(object sender, EventArgs e)
    {
        ExportarExcelDataTable("DTDIFERIDOS", "PresupuestoDiferidos");
    }

    protected void btnExpOtros_Click(object sender, EventArgs e)
    {
        ExportarExcelDataTable("DTOTROS", "PresupuestoOtros");
    }

    protected void btnExpHonorarios_Click(object sender, EventArgs e)
    {
        ExportarExcelDataTable("DTHONORARIOS", "PresupuestoHonorarios");
    }

    protected void btnExpTotalNomina_Click(object sender, EventArgs e)
    {
        ExportarExcelGrilla(gvTotalesNomina, "PresupuestoTotalNomina");
    }

    protected void btnExpObligacionesTotalesPagos_Click(object sender, EventArgs e)
    {
        ExportarExcelGrilla(gvObligacionesTotPagos, "PresupuestoTotalPagoObl");
    }

    protected void btnExpObligacionesNuevas_Click(object sender, EventArgs e)
    {        
        ExportarExcelDataTable("DTOBLIGACIONESNUEVAS", "PresupuestoObligacionesNuevas");
    }

    protected void ExportarExcelDataTable(string NombreDataTable, string NombreArchivo)
    {
        GridView gvGrillaExcel = new GridView();
        DataTable dtTabla = new DataTable();
        dtTabla = (DataTable)Session[NombreDataTable];
        gvGrillaExcel.ID = "gv" + NombreDataTable + "Excel";
        gvGrillaExcel.HeaderStyle.CssClass = "gridHeader";
        gvGrillaExcel.PagerStyle.CssClass = "gridPager";
        gvGrillaExcel.RowStyle.CssClass = "gridItem";
        gvGrillaExcel.DataSource = dtTabla;
        gvGrillaExcel.DataBind();
        ExportarExcelGrilla(gvGrillaExcel, NombreArchivo);
    }
  
    protected void ExportarExcelGrilla(GridView gvGrilla, string Archivo)
    {
        try
        {
            if (gvGrilla.Rows.Count > 0)
            {
                string style = "";
                StringBuilder sb = new StringBuilder();
                StringWriter sw = new StringWriter(sb);
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                Page pagina = new Page();
                dynamic form = new HtmlForm();
                style = "<link href=\"../../Styles/Styles.css\" rel=\"stylesheet\" type=\"text/css\" />";
                gvGrilla.EnableViewState = false;
                pagina.EnableEventValidation = false;
                pagina.DesignerInitialize();
                pagina.Controls.Add(form);
                form.Controls.Add(gvGrilla);
                pagina.RenderControl(htw);
                Response.Clear();
                style = @"<style> .textmode { mso-number-format:\@; } </style>";
                Response.Write(style);
                Response.Buffer = true;
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("Content-Disposition", "attachment;filename=" + Archivo + ".xls");
                Response.Charset = "UTF-8";
                Response.Write(sb.ToString());
                Response.End();
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex0)
        {
            VerError(ex0.Message);
        }
    }
    
    private void helper_ManualSummary(GridViewRow row)
    {
        GridViewRow newRow = helper.InsertGridRow(row);
        newRow.Cells[0].HorizontalAlign = HorizontalAlign.Right;
        newRow.Cells[0].Text = String.Format("Total: {0} itens, {1:c}", helper.GeneralSummaries["ProductName"].Value, helper.GeneralSummaries["ItemTotal"].Value);
    }

    private void helper_GroupSummary(string groupName, object[] values, GridViewRow row)
    {
        row.Cells[0].HorizontalAlign = HorizontalAlign.Right;
        row.Cells[0].Text = "Média";
    }

    private void helper_GroupHeader(string groupName, object[] values, GridViewRow row)
    {
        if (groupName == "ShipRegion")
        {
            row.BackColor = Color.LightGray;
            row.Cells[0].Text = "&nbsp;&nbsp;" + row.Cells[0].Text;
        }
        else if (groupName == "ShipName")
        {
            row.BackColor = Color.FromArgb(236, 236, 236);
            row.Cells[0].Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + row.Cells[0].Text;
        }
    }

    private void helper_Bug(string groupName, object[] values, GridViewRow row)
    {
        if (groupName == null) return;

        row.BackColor = Color.Bisque;
        row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
        row.Cells[0].Text = "[ Summary for " + groupName + " " + values[0] + " ]";
    }

    private void SaveQuantity(string column, string group, object value)
    {
        mQuantities.Add(Convert.ToInt32(value));
    }

    private object GetMinQuantity(string column, string group)
    {
        int[] qArray = new int[mQuantities.Count];
        mQuantities.CopyTo(qArray);
        Array.Sort(qArray);
        return qArray[0];
    }

    protected void cbSaldoCuenta_CheckedChanged(object sender, EventArgs e)
    {
        if (cbSaldoCuenta.Checked == true)
        {
            cbSaldoPromedio.Checked = false;
            cbSaldoPeriodo.Checked = false;
        }
        else
        {
            cbSaldoPromedio.Checked = true;
            cbSaldoPeriodo.Checked = false;
        }
    }

    protected void cbSaldoPromedio_CheckedChanged(object sender, EventArgs e)
    {
        if (cbSaldoPromedio.Checked == true)
        {
            cbSaldoCuenta.Checked = false;
            cbSaldoPeriodo.Checked = false;
        }
        else
        {
            cbSaldoCuenta.Checked = true;
            cbSaldoPeriodo.Checked = false;
        }
    }

    protected void cbSaldoPeriodo_CheckedChanged(object sender, EventArgs e)
    {
        if (cbSaldoPeriodo.Checked == true)
        {
            cbSaldoCuenta.Checked = false;
            cbSaldoPromedio.Checked = false;
        }
        else
        {
            cbSaldoCuenta.Checked = true;
            cbSaldoPromedio.Checked = false;
        }
    }

    protected void ddlTipo_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlTipo.SelectedValue == "3")
        {
            lbValor.Visible = false;
            txtValor.Visible = false;
            gvDatos.Visible = true;
        }
        else
        {
            lbValor.Visible = true;
            txtValor.Visible = true;
            gvDatos.Visible = false;
        }
    }

 }

