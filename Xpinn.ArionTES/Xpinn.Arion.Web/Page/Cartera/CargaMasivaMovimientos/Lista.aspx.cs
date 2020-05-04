using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Contabilidad.Services;
using Xpinn.Contabilidad.Entities;
using Xpinn.Cartera.Entities;
using Xpinn.Util;
using System.IO;
using System.Configuration;
using System.Globalization;
using System.Text;
using System.Web.UI.HtmlControls;
using Xpinn.Cartera.Services;
using Tercero = Xpinn.Contabilidad.Entities.Tercero;

public partial class Lista : GlobalWeb
{
    private Usuario usuario;
    private Comprobante entityCargaComprobante = new Comprobante();
    private TerceroService terceroService = new TerceroService();
    private ComprobanteService servicecargacomprobante = new ComprobanteService();
    private Xpinn.Tesoreria.Entities.Operacion pOperacion = new Xpinn.Tesoreria.Entities.Operacion();
    private CargaMovimientosService serviceCargaMovimientos = new CargaMovimientosService();
    private bool bGrabar = false;
    private StreamReader strReader;
    private ComprobanteService ComprobanteServicio = new ComprobanteService();
    private int tipo_ope = 144;
    private string query;

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(serviceCargaMovimientos.CodigoProgramaCarga, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.MostrarGuardar(false);
            ctlproceso.eventoCancelarClick += btnCancelarProceso_Click;
            ctlproceso.eventoClick += btnAceptarProceso_Click;
            panelProceso.Visible = false;



        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(serviceCargaMovimientos.GetType().Name + "L", "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                CleanSession();
                mvAplicar.ActiveViewIndex = 0;
                btnCargarComp.Visible = true;
                CargarValoresConsulta(pConsulta, serviceCargaMovimientos.GetType().Name);
                if (Session[serviceCargaMovimientos.GetType().Name + ".consulta"] != null)
                    Actualizar((List<CargaMovimientos>)Session["lstMovimientos"]);
            }
            msg.Text = "";
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(servicecargacomprobante.GetType().Name + "L", "Page_Load", ex);
        }

    }

    protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            Actualizar((List<CargaMovimientos>)Session["lstMovimientos"]);
        }
        catch (Xpinn.Util.ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(servicecargacomprobante.CodigoProgramaCarga + "L", "gvLista_RowDeleting", ex);
        }
    }

    protected void gvLista_RowCommand(object sender, GridViewCommandEventArgs evt)
    {
        if (evt.CommandName == "Editar")
        {
            String[] tmp = evt.CommandArgument.ToString().Split('|');
            Comprobante ejeMeta = new Comprobante();
        }
    }

    protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            Actualizar((List<CargaMovimientos>)Session["lstMovimientos"]);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(servicecargacomprobante.GetType().Name + "L", "gvLista_PageIndexChanging", ex);
        }
    }

    private void Actualizar(List<CargaMovimientos> lstcargaMovimientos)
    {
        try
        {
            Site toolBar = (Site)this.Master;
            if (lstcargaMovimientos != null)
            {
                gvLista.DataSource = lstcargaMovimientos;
                gvLista.DataBind();
            }
            Session.Add(serviceCargaMovimientos.GetType().Name + ".consulta", 1);
            toolBar.MostrarGuardar(true);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(serviceCargaMovimientos.GetType().Name + "L", "Actualizar", ex);
        }
    }

    protected void btnCargarComp_Click(object sender, EventArgs e)
    {
        if (Session["lstMovimientos"] != null)
            Session.Remove("lstMovimientos");
        VerError("");
        int contador = 0;
        string error = "";
        String readLine;
        try
        {
            if (FileUploadMovimientos.HasFile)
            {
                List<CargaMovimientos> lstcargaMovimientos = new List<CargaMovimientos>();
                Stream stream = FileUploadMovimientos.FileContent;
                using (strReader = new StreamReader(stream))
                {
                    while (strReader.Peek() >= 0)
                    {
                        CargaMovimientos carga = new CargaMovimientos();
                        readLine = strReader.ReadLine();
                        String[] arrayLineas = readLine.Split(';');
                        if (contador > 0)
                        {
                            var persona = terceroService.ConsultarTercero(null, arrayLineas[2],
                                (Usuario)Session["Usuario"]);

                            if (ValidarDatos(arrayLineas))
                            {
                                var tipo_prod = arrayLineas[0];
                                TipoProducto producto = serviceCargaMovimientos.ConsultarProducto(tipo_prod,
                                    (Usuario)Session["usuario"]);
                                carga.TipoProducto = producto.CodProducto;
                                carga.Descripcion = producto.Descripcion;
                                carga.NumeroProducto = Convert.ToInt64(arrayLineas[1]);
                                carga.IdentificacionPer = Convert.ToInt64(arrayLineas[2]);
                                carga.Valor = Convert.ToInt32(arrayLineas[3]);
                                carga.TipoMovimiento = arrayLineas[4];
                                carga.CodPersona = Convert.ToInt32(persona.cod_persona);
                            }

                        }
                        lstcargaMovimientos.Add(carga);
                    }
                    contador = contador + 1;

                }

                Session["lstMovimientos"] = lstcargaMovimientos;
                Actualizar(lstcargaMovimientos);
                Label1.Visible = true;
                Label1.Text = "Su Archivo " + FileUploadMovimientos.FileName + " Se ha Cargado";


            }
        }
        catch (Exception ex)
        {
            VerError(ex.ToString());
        }

    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        VerError("");
        mpeNuevo.Show();
    }

    protected void btnFinal_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }

    private bool Aplicar()
    {
        usuario = (Usuario)Session["Usuario"];
        DateTime FechaProceso = DateTime.Now;
        // CREAR OPERACION
        pOperacion.cod_ope = 0;
        pOperacion.tipo_ope = 144;
        pOperacion.cod_caja = 0;
        pOperacion.cod_cajero = 0;
        pOperacion.observacion = "Operacion-Cuentas Por Pagar";
        pOperacion.cod_proceso = null;
        pOperacion.fecha_oper = FechaProceso;
        pOperacion.fecha_calc = FechaProceso;
        pOperacion.cod_ofi = ((Usuario)Session["usuario"]).cod_oficina;
        string Error = "";
        // Hace el proceso de subir y hacer todos los movimientos correspondientes 
        bool bcarga;
        bcarga = serviceCargaMovimientos.CargaMasivoMovimientos((List<CargaMovimientos>)Session["lstMovimientos"],
            usuario, pOperacion, ref Error);
        if (Error.Trim() == "" || bcarga)
        {
            Usuario pUsuario = new Usuario();
            pUsuario = (Usuario)Session["Usuario"];

            Session["Codoper"] = null;
            Session["numerocheque"] = null;
            Session["entidad"] = null;
            Session["cuenta"] = null;
            Session[ComprobanteServicio.CodigoPrograma + ".tipo_comp"] = 0;
            Session[ComprobanteServicio.CodigoPrograma + ".cod_ope"] = null;
            Session[ComprobanteServicio.CodigoPrograma + ".tipo_ope"] = null;
            Session[ComprobanteServicio.CodigoPrograma + ".cod_persona"] = pUsuario.codusuario;
            //Validar Proceso Contable de la operacion 
            try
            {
                VerError("");
                // Validar que exista la parametrización contable por procesos
                if (ValidarProcesoContable(FechaProceso, tipo_ope) == false)
                {
                    VerError(
                        "No se encontró parametrización contable por procesos para el tipo de operación" + tipo_ope);
                    return false;
                }
                // Determinar código de proceso contable para generar el comprobante
                Int64? rpta = 0;
                if (!panelProceso.Visible && panelGeneral.Visible)
                {
                    rpta = ctlproceso.Inicializar(tipo_ope, FechaProceso, usuario);
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
                        if (Aplicar())
                            Navegar("../../Contabilidad/Comprobante/Nuevo.aspx");
                        else
                            VerError("Se presentó error");
                    }
                    ctlproceso.CargarVariables(pOperacion.cod_ope, tipo_ope, usuario.cod_persona, usuario);
                }
                else
                {
                    if (Error.Trim() == "")
                        Error = "Error en detalle de comprobante";
                    VerError(Error);
                    return false;
                }

            }
            catch

            (Exception
                exception)
            {
                //
            }
        }
        return true;
    }

    protected void btnContinuar_Click(object sender, EventArgs e)
    {
        if (Aplicar())
        {
            Navegar("../../Contabilidad/Comprobante/Nuevo.aspx");
        }


    }

    protected void btnParar_Click(object sender, EventArgs e)
    {
        mpeNuevo.Hide();
        mvAplicar.ActiveViewIndex = 0;
    }

    void CleanSession()
    {
        Session["lstMovimientos"] = null;
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
            Aplicar();
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }

    Boolean ValidarDatos(String[] arrayLineas)
    {
        var persona = terceroService.ConsultarTercero(null, arrayLineas[2], (Usuario)Session["Usuario"]);
        if (persona.identificacion == null)
        {
            lblMensaje.Text = @"La Identificacion " + arrayLineas[2] +
                              @"no existe, validar los datos antes de subirlos.";
            return false;
        }
        TipoProducto producto = serviceCargaMovimientos.ConsultarProducto(arrayLineas[0], (Usuario)Session["usuario"]);
        if (producto.Descripcion == null)
        {
            lblMensaje.Text =
                @"el codigo del producto no es valido, revisar en la parte superior los codigos de cada producto.";
            return false;
        }
        if (!obtenerQueryProc(producto.CodProducto))
        {
            lblMensaje.Text =
                @"El numero del producto no existe, validar los datos antes de subirlo.";
            return false;
        }
        if (arrayLineas[0] == "1")
        {
            if (serviceCargaMovimientos.ConsultaSaldo(Convert.ToInt64(arrayLineas[1]), (Usuario)Session["usuario"]) == null)
            {
                lblMensaje.Text =
                @"el credito no tiene saldos, validar los datos ante de subirlo.";
                return false;
            }
        }
        return true;
    }
    Boolean obtenerQueryProc(int producto)
    {
        switch (producto)
        {
            case 1:
                query = " select numero_aporte COD_PRODUCTO from aporte where numero_aporte = " + producto;
                break;
            case 2:
                query = " select numero_radicacion COD_PRODUCTO from credito where numero_radicacion = " + producto;
                break;
            case 3:
                query = " select NUMERO_CUENTA COD_PRODUCTO from AHORRO_VISTA where NUMERO_CUENTA = " + producto;
                break;
            case 4:
                query = " select NUMERO_SERVICIO COD_PRODUCTO from SERVICIOS where NUMERO_SERVICIO = " + producto;
                break;
            case 6:
                query = " select COD_PERSONA COD_PRODUCTO from PERSONA_AFILIACION where COD_PERSONA = " + producto;
                break;
            case 8:
                query = " select num_devolucion COD_PRODUCTO from devolucion where num_devolucion = " + producto;
                break;
            case 9:
                query = " select NUMERO_PROGRAMADO COD_PRODUCTO from AHORRO_PROGRAMADO where NUMERO_PROGRAMADO = " + producto;
                break;
        }

        TipoProducto codigo = serviceCargaMovimientos.ConsultaNProducto(query, (Usuario)Session["usuario"]);
        if (codigo.CodProducto == 0) return false;

        return true;

    }

}