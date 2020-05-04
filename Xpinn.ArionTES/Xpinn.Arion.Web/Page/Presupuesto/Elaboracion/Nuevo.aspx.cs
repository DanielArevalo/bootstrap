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
    Int16 nivel = 0;


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


        Xpinn.Asesores.Services.UsuarioAseService usuarioAseServicio = new Xpinn.Asesores.Services.UsuarioAseService();
        Xpinn.Asesores.Entities.UsuarioAse usuarioAse = new Xpinn.Asesores.Entities.UsuarioAse();
        Xpinn.Comun.Entities.General pData = new Xpinn.Comun.Entities.General();
        Xpinn.Comun.Data.GeneralData ConsultaData = new Xpinn.Comun.Data.GeneralData();
        pData = ConsultaData.ConsultarGeneral(1100, (Usuario)Session["usuario"]);
         nivel = Convert.ToInt16(pData.valor);
        txtNivel.Text = Convert.ToString(nivel);

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
                vPresupuesto.flujoinicial = Convert.ToDouble(txtFlujoInicial.Text.Replace(conf.ObtenerSeparadorMilesConfig(), ""));
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
            mvPresupuesto.ActiveViewIndex = 2 ;
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
                Session.Remove("DTSALDOS");               
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
                txtFlujoInicial.Text = Convert.ToString(vPresupuesto.flujoinicial);
            }
            catch (Exception ex)
            {
                VerError(ex.Message);
            }

            Session.Remove("DTPRESUPUESTO");
            Session.Remove("DTFECHAS");
            Session.Remove("DTSALDOS");

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

            // Inicializar el datatable
            DataTable dtFechas = new DataTable();
            dtFechas.Clear();
            DataTable dtProy = new DataTable();
            dtProy.Clear();
            DataTable dtSaldos = new DataTable();
            dtSaldos.Clear();            
            
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

            // Determinar el número de períodos a generar
            int nperiodos = 0;
            try { nperiodos = Convert.ToInt32(txtNumPeriodos.Text); }
            catch 
            {
                VerError("Error al realizar la conversión del número de periodos");
                return;
            }

            // Generar las columnas en la grilla y en el datatable según el número de períodos a proyectar
            gvProyeccion.AutoGenerateColumns = false;
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

                // Adicionar la columna al datatable
                dtProy.Columns.Add(nombreCampo, typeof(Double));

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
            dtProy.Columns.Add("total", typeof(Double));

            // Inicializar variable para número de períodos
            int numeroPeriodo = 0;

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
                //drFila[4] = PresupuestoServicio.SaldoPromedioCuenta(cod_cuenta, fecha_inicial, fecha_final, filtro, (Usuario)Session["Usuario"]);
                drFila[5] = PresupuestoServicio.SaldoFinalCuenta(cod_cuenta, fecha_final, filtro, (Usuario)Session["Usuario"]);
                drFila.AcceptChanges();
            }

            // Parámetros de la grilla
            gvProyeccion.Columns[0].ItemStyle.BackColor = System.Drawing.Color.LightBlue;

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
                }
            }

            // Cargando datos en la grilla           
            Session["DTPRESUPUESTO"] = dtProy;
            gvProyeccion.DataSource = dtProy;
            gvProyeccion.DataBind();

            // Cargando el datatable que contiene información de las fechas de cada período
            Session["DTFECHAS"] = dtFechas;

            // Si es modificación se muestra el flujo de caja
            if (Session["IDPRESUPUESTO"] != null)
            {
                dtProy = (DataTable)Session["DTPRESUPUESTO"];
                gvProyeccion.DataSource = dtProy;
                gvProyeccion.DataBind();                
                GenerarFlujo();
            }
            
            // Mostrar totales de OTROS CONCEPTOS, OBLIGACIONES y NOMINA
            PresupuestoServicio.TotalizarPresupuesto(ref dtProy, dtFechas);

            Site toolBar = (Site)this.Master;
            toolBar.MostrarGuardar(true);
            
        }

        mvPresupuesto.ActiveViewIndex = 1;

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


    #region Presupuesto

    protected void gvProyeccion_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        string cod_cuenta = e.Row.Cells[1].Text;
        if (cod_cuenta.Length<=4)
        {
            ImageButton btnEditar = (ImageButton)e.Row.FindControl("btnEditar");
            if (btnEditar != null)
                btnEditar.Visible = false;
        }
        else
        {
            ImageButton btnEditar = (ImageButton)e.Row.FindControl("btnEditar");
            if (btnEditar != null)
                btnEditar.Visible = true;
        }

        // deshabilitar las cuentas que se generan por presupuestos individuales
        if (PresupuestoServicio.EsParametroCuenta(cod_cuenta, (Usuario)Session["Usuario"]) == true || cod_cuenta.Length <=4)
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
            if (cod_cuenta.Length <=4)
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
        for (var index = 0; index < e.NewValues.Count; index++)
        {
            if (e.NewValues[index] != null)
            {
                valor = e.NewValues[index].ToString();
                if (drFila != null)
                {
                    drFila[index + 7] = valor;
                    drFila.AcceptChanges();
                }
            }
        }
        gvProyeccion.EditIndex = -1;

        // Mayorizando el presupuesto
        DataTable dtFechas = new DataTable();
        dtFechas = (DataTable)Session["DTFECHAS"];



        PresupuestoServicio.MayorizarPresupuesto(ref dtPresupuesto, dtFechas, (Usuario)Session["Usuario"], Convert.ToInt16(txtNivel.Text));

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

        PresupuestoServicio.MayorizarPresupuesto(ref dtPresupuesto, dtFechas, (Usuario)Session["Usuario"], Convert.ToInt16(txtNivel.Text));
        gvProyeccion.DataSource = dtPresupuesto;
        gvProyeccion.DataBind();
        gvProyeccion.Rows[IndiceDetalle].Font.Bold = true;
        gvProyeccion.Rows[IndiceDetalle].ForeColor = System.Drawing.Color.DarkGreen; 
        //CalcularTotal();
        Session["DTPRESUPUESTO"] = dtPresupuesto;
        mpeNuevo.Hide();

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

        PresupuestoServicio.MayorizarPresupuesto(ref dtPresupuesto, dtFechas, (Usuario)Session["Usuario"], Convert.ToInt16(txtNivel.Text));
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
        if (cod_cuenta.Length <= 4)
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
        DataTable dtPresupuesto = new DataTable();
        dtPresupuesto = (DataTable)Session["DTPRESUPUESTO"];
        DataTable dtFlujo = new DataTable();
        dtFlujo = dtPresupuesto.Copy();
        DataTable dtFechas = new DataTable();
        dtFechas = (DataTable)Session["DTFECHAS"];

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

        //dtFlujo = PresupuestoServicio.GenerarFlujoCaja(dtFlujo, dtFechas, valorFlujoInicial, (Usuario)Session["Usuario"]);
        PresupuestoServicio.TotalizarPresupuesto(ref dtFlujo, dtFechas);
        gvFlujo.DataSource = dtFlujo;
        gvFlujo.DataBind();
        Session["DTFLUJO"] = dtFlujo;

        gvProyeccion.DataSource = dtPresupuesto;
        gvProyeccion.DataBind();
    }

    #endregion Presupuesto

    protected void btnExpFlujo_Click(object sender, EventArgs e)
    {
        ExportarExcelGrilla(gvFlujo, "PresupuestoFlujo");
    }

    protected void btnExpPresupuesto_Click(object sender, EventArgs e)
    {
        ExportarExcelDataTable("DTPRESUPUESTO", "PresupuestoPyG"); 
        //ExportarExcelGrilla(gvProyeccion, "PresupuestoPyG");
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

