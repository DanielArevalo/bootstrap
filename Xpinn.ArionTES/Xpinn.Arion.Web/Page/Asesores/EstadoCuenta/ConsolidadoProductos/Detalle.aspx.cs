using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using Xpinn.Asesores.Entities;
using Xpinn.Asesores.Services;
using Xpinn.Aportes.Entities;
using Xpinn.Aportes.Services;
using Xpinn.Ahorros.Entities;
using Xpinn.Ahorros.Services;
using Xpinn.Programado.Services;
using Xpinn.Programado.Entities;
using Xpinn.Servicios.Entities;
using Xpinn.Servicios.Services;
using Microsoft.Reporting.WebForms;
using System.Data;

public partial class Detalle : GlobalWeb
{

    Configuracion conf = new Configuracion();
    private EstadoCuentaService serviceEstadoCuenta = new EstadoCuentaService();
    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(serviceEstadoCuenta.CodigoPrograma, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoRegresar += btnRegresar_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoImprimir += btnImprimir_Click;
            toolBar.MostrarConsultar(true);
            toolBar.MostrarImprimir(true);
            toolBar.MostrarRegresar(true);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw("L", "Page_PreInit", ex);
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)

                if (Session["cod_persona"] != null)
                {
                    TxtFechaInicial.ToDateTime = DateTime.Today.AddMonths(-5);
                    TxtFechaFinal.ToDateTime = DateTime.Today;
                    Actualizar();
                }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
    }

    void Actualizar()
    {
        PersonaService personaServicio = new PersonaService();
        Persona vPersona = new Persona();
        vPersona = personaServicio.ConsultarPersona(Convert.ToInt64(Session["cod_persona"]), (Usuario)Session["usuario"]);
        if (vPersona != null)
        {
            txtCodCliente.Text = vPersona.IdPersona.ToString();
            txtTipoDoc.Text = vPersona.TipoIdentificacion.NombreTipoIdentificacion;
            txtId.Text = vPersona.NumeroDocumento;
            txtNombres.Text = vPersona.PrimerNombre + " " + vPersona.PrimerApellido;
            txtTelefono.Text = vPersona.Telefono;
            txtDireccion.Text = vPersona.Direccion;

            //Listar Movimiento Aportes
            MovimientoAportes();
            //Listar Movimiento de Créditos
            MovimientoCreditos();
            //Listar Movimiento de Ahorros a la Vista
            MovimientoAhorrosVista();
            //Listar Movimiento de Ahorro Programado
            MovimientoProgramado();
            //Listar Movimiento de CDATS
            MovimientoCDATS();
            //Listar Movimiento de Servicios
            MovimientoServicios();
        }
    }

    void MovimientoAportes()
    {
        AporteServices aporteServicio = new AporteServices();
        Aporte vAporte = new Aporte();
        List<Aporte> lstAporte = new List<Aporte>();
        List<Object> lstMovAporte = new List<Object>();

        lstAporte = aporteServicio.ListarCuentasPersona(Convert.ToInt64(Session["cod_persona"]), (Usuario)Session["usuario"]);
        if (lstAporte.Count > 0)
        {
            foreach (Aporte obj in lstAporte)
            {
                List<MovimientoAporte> lstA = new List<MovimientoAporte>();
                lstA = aporteServicio.ListarMovAporte(obj.numero_aporte, Convert.ToDateTime(TxtFechaInicial.ToDateTime.ToString(conf.ObtenerFormatoFecha())), Convert.ToDateTime(TxtFechaFinal.ToDateTime.ToString(conf.ObtenerFormatoFecha())), (Usuario)Session["usuario"]);
                //lstMovAporte.AddRange(lstA);

                var movProd = from mp in lstA
                              where Convert.ToDateTime(mp.FechaPago.ToString(conf.ObtenerFormatoFecha())) >= Convert.ToDateTime(TxtFechaInicial.ToDateTime.ToString(conf.ObtenerFormatoFecha())) && Convert.ToDateTime(mp.FechaPago) <= Convert.ToDateTime(TxtFechaFinal.ToDateTime.ToString(conf.ObtenerFormatoFecha()))
                              orderby mp.FechaPago, mp.FechaCuota, mp.CodOperacion, mp.TipoOperacion, mp.TipoMovimiento
                              select new
                              {
                                  mp.num_comp,
                                  tipo_comp = mp.tipo_comp,
                                  nom_tipo_comp = mp.nom_tipo_comp,
                                  FechaPago = mp.FechaPago.ToShortDateString(),
                                  mp.CodOperacion,
                                  mp.TipoOperacion,
                                  mp.TipoMovimiento,
                                  Saldo = mp.Saldo,
                                  Capital = mp.Capital,
                                  totalpago = ((mp.TipoOperacion == "Pagos Caja") ? 0 : mp.Capital),
                                  mp.numero_aporte,
                                  obj.nom_linea_aporte
                              };
                foreach (var node in movProd)
                {
                    lstMovAporte.Add(node);
                }
            }
        }

        if (lstMovAporte.Count > 0)
        {
            gvMovAportes.DataSource = lstMovAporte;
            gvMovAportes.DataBind();
            upAporte.Visible = true;
            Session["MovAporte"] = lstMovAporte;
        }
        else
        {
            upAporte.Visible = false;
        }
    }

    void MovimientoCreditos()
    {
        Producto pEntityProducto = new Producto();
        List<DetalleProducto> lstCreditos = new List<DetalleProducto>();
        DetalleProductoService detCreditoServicio = new DetalleProductoService();

        List<Object> lstMov = new List<Object>();

        pEntityProducto.Persona.IdPersona = Convert.ToInt64(Session["cod_persona"]);
        lstCreditos = detCreditoServicio.ListarDetalleProductos(pEntityProducto, (Usuario)Session["usuario"], 2);

        foreach (DetalleProducto dproducto in lstCreditos)
        {
            var movProd = from mp in dproducto.MovimientosProducto
                          where Convert.ToDateTime(mp.FechaPago.ToString(conf.ObtenerFormatoFecha())) >= Convert.ToDateTime(TxtFechaInicial.ToDateTime.ToString(conf.ObtenerFormatoFecha())) && Convert.ToDateTime(mp.FechaPago) <= Convert.ToDateTime(TxtFechaFinal.ToDateTime.ToString(conf.ObtenerFormatoFecha()))
                          orderby mp.FechaPago, mp.FechaCuota, mp.CodOperacion
                          select new
                          {
                              mp.num_comp,
                              TIPO_COMP = mp.TIPO_COMP,
                              mp.NumCuota,
                              FechaPago = mp.FechaPago.ToShortDateString(),
                              FechaCuota = mp.FechaCuota.ToShortDateString(),
                              mp.DiasMora,
                              TipoOperacion = mp.CodOperacion,
                              Transaccion = mp.TipoOperacion,
                              mp.TipoMovimiento,
                              Saldo = mp.Saldo,
                              Capital = mp.Capital,
                              IntCte = mp.IntCte,
                              IntMora = mp.IntMora,
                              Poliza = mp.Poliza,
                              Otros = mp.Otros,
                              Prejuridico = mp.Prejuridico,
                              totalpago = ((mp.TipoOperacion == "Desembolsos") ? 0 : mp.Capital + mp.IntCte + mp.IntMora + mp.LeyMiPyme + mp.ivaMiPyme + mp.Poliza + mp.Otros + mp.Seguros + mp.Prejuridico),
                              mp.Calificacion,
                              Seguros = mp.Seguros,
                              mp.NumeroRadicacion,
                              dproducto.Producto.Linea
                          };
            foreach (var node in movProd)
            {
                lstMov.Add(node);
            }
        }
        if (lstMov.Count > 0)
        {
            gvMovCredito.DataSource = lstMov;
            gvMovCredito.DataBind();
            upCredito.Visible = true;
            Session["MovCredito"] = lstMov;
        }
        else
        {
            upCredito.Visible = false;
        }

    }

    void MovimientoAhorrosVista()
    {
        ReporteMovimientoServices ReporteMovService = new ReporteMovimientoServices();
        List<ReporteMovimiento> lstMovAhorroV = new List<ReporteMovimiento>();
        List<AhorroVista> lstAhorro = new List<AhorroVista>();

        lstAhorro = ReporteMovService.ListarCuentasPersona(Convert.ToInt64(Session["cod_persona"]), (Usuario)Session["usuario"]);

        foreach (AhorroVista cuenta in lstAhorro)
        {
            List<ReporteMovimiento> lstMovAhorro = new List<ReporteMovimiento>();
            lstMovAhorro = ReporteMovService.ListarReporteMovimiento(Convert.ToInt64(cuenta.numero_cuenta), Convert.ToDateTime(TxtFechaInicial.Text), Convert.ToDateTime(TxtFechaFinal.Text), (Usuario)Session["usuario"]);
            foreach(ReporteMovimiento mov in lstMovAhorro)
            {
                mov.nombre = cuenta.nom_linea;
            }
            lstMovAhorroV.AddRange(lstMovAhorro);
        }

        if (lstMovAhorroV.Count > 0)
        {
            gvMovAhorroVista.DataSource = lstMovAhorroV;
            gvMovAhorroVista.DataBind();
            upAhorroV.Visible = true;
            Session["MovAhorroVista"] = lstMovAhorroV;
        }
        else
        {
            upAhorroV.Visible = false;
        }

    }

    void MovimientoProgramado()
    {
        MovimientoCuentasServices ServicioProgramdo = new MovimientoCuentasServices();
        List<CuentasProgramado> lstProgramado = new List<CuentasProgramado>();
        List<ReporteMovimiento> lstMovProgramado = new List<ReporteMovimiento>();

        lstProgramado = ServicioProgramdo.ListarCuentasPersona(Convert.ToInt64(Session["cod_persona"]), (Usuario)Session["usuario"]);
        foreach (CuentasProgramado cuenta in lstProgramado)
        {
            List<ReporteMovimiento> lstConsulta = new List<ReporteMovimiento>();
            lstConsulta = ServicioProgramdo.ListarDetalleMovimiento(cuenta.numero_programado, Convert.ToDateTime(TxtFechaInicial.Text), Convert.ToDateTime(TxtFechaFinal.Text), (Usuario)Session["usuario"]);
            foreach(ReporteMovimiento mov in lstConsulta)
            {
                mov.nombre = cuenta.nom_linea;
            }
            lstMovProgramado.AddRange(lstConsulta);
        }
        if (lstMovProgramado.Count > 0)
        {
            gvMovProgramado.DataSource = lstMovProgramado;
            gvMovProgramado.DataBind();
            upProgramado.Visible = true;
            Session["MovProgramado"] = lstMovProgramado;
        }
        else
        {
            upProgramado.Visible = false;
        }
    }

    void MovimientoCDATS()
    {
        Xpinn.CDATS.Services.ReporteMovimientoServices ServicioCDAT = new Xpinn.CDATS.Services.ReporteMovimientoServices();
        List<Xpinn.CDATS.Entities.Cdat> lstCDAT = new List<Xpinn.CDATS.Entities.Cdat>();
        List<Xpinn.CDATS.Entities.ReporteMovimiento> lstMovCDAT = new List<Xpinn.CDATS.Entities.ReporteMovimiento>();

        lstCDAT = ServicioCDAT.ListarCuentasPersona(Convert.ToInt64(Session["cod_persona"]), (Usuario)Session["usuario"]);
        foreach (Xpinn.CDATS.Entities.Cdat cuenta in lstCDAT)
        {
            List<Xpinn.CDATS.Entities.ReporteMovimiento> lstConsulta = new List<Xpinn.CDATS.Entities.ReporteMovimiento>();
            lstConsulta = ServicioCDAT.ListarReporteMovimiento(Convert.ToInt64(cuenta.numero_cdat), Convert.ToDateTime(TxtFechaInicial.Text), Convert.ToDateTime(TxtFechaFinal.Text), (Usuario)Session["usuario"]);
            foreach(Xpinn.CDATS.Entities.ReporteMovimiento mov in lstConsulta)
            {
                mov.nombre = cuenta.nomlinea;
            }
            lstMovCDAT.AddRange(lstConsulta);
        }
        if (lstMovCDAT.Count > 0)
        {
            gvMovCDAT.DataSource = lstMovCDAT;
            gvMovCDAT.DataBind();
            upCDAT.Visible = true;
            Session["MovCDAT"] = lstMovCDAT;
        }
        else
        {
            upCDAT.Visible = false;
        }            
    }

    void MovimientoServicios()
    {
        List<Servicio> lstServicios = new List<Servicio>();
        AprobacionServiciosServices ExcluServicios = new AprobacionServiciosServices();
        List<Servicio> lstMovServicios = new List<Servicio>();
        lstServicios = ExcluServicios.ListarCuentasPersona(Convert.ToInt64(Session["cod_persona"]), (Usuario)Session["usuario"]);

        foreach (Servicio cuenta in lstServicios)
        {
            Servicio reportemovimiento = new Servicio();
            List<Servicio> lstMovSer = new List<Servicio>();
            reportemovimiento.Fec_fin = DateTime.ParseExact(TxtFechaFinal.Texto, gFormatoFecha, null);
            reportemovimiento.Fec_ini = DateTime.ParseExact(TxtFechaInicial.Texto, gFormatoFecha, null);
            reportemovimiento.numero_servicio = cuenta.numero_servicio;

            lstMovSer = ExcluServicios.Reportemovimiento(reportemovimiento, (Usuario)Session["usuario"]);
            foreach(Servicio serv in lstMovSer)
            {
                serv.numero_servicio = cuenta.numero_servicio;
                serv.nom_linea = cuenta.nom_linea;
            }
            lstMovServicios.AddRange(lstMovSer);
        }
        if (lstMovServicios.Count > 0)
        {
            gvMovServicios.DataSource = lstMovServicios;
            gvMovServicios.DataBind();
            upServicio.Visible = true;
            Session["MovServicio"] = lstMovServicios;
        }
        else
        {
            upServicio.Visible = false;
        }
    }

    protected void gvMovCDAT_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            if (e.NewPageIndex >= 0)
            {
                gvMovCDAT.PageIndex = e.NewPageIndex;
                if (Session["MovCDAT"] != null)
                {
                    List<Xpinn.CDATS.Entities.ReporteMovimiento> lstCDAT = new List<Xpinn.CDATS.Entities.ReporteMovimiento>();
                    lstCDAT = (List<Xpinn.CDATS.Entities.ReporteMovimiento>)Session["MovCDAT"];
                    gvMovCDAT.DataSource = lstCDAT;
                    gvMovCDAT.DataBind();
                }
                else
                {
                    MovimientoCDATS();
                }
            }
        }
        catch (Exception ex)
        {
            //BOexcepcion.Throw(personaServicio.CodigoPrograma, "gvCreditos_PageIndexChanging", ex);
        }
    }

    protected void gvMovServicios_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            if (e.NewPageIndex >= 0)
            {
                gvMovServicios.PageIndex = e.NewPageIndex;
                if (Session["MovServicio"] != null)
                {
                    List<Servicio> lstMovServicios = new List<Servicio>();
                    lstMovServicios = (List<Servicio>)Session["MovServicio"];
                    gvMovServicios.DataSource = lstMovServicios;
                    gvMovServicios.DataBind();
                }
                else
                {
                    MovimientoServicios();
                }
            }
        }
        catch (Exception ex)
        {
            //BOexcepcion.Throw(personaServicio.CodigoPrograma, "gvCreditos_PageIndexChanging", ex);
        }
    }

    protected void gvMovProgramado_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            if (e.NewPageIndex >= 0)
            {
                gvMovProgramado.PageIndex = e.NewPageIndex;
                if (Session["MovProgramado"] != null)
                {
                    List<ReporteMovimiento> lstMovProgramado = new List<ReporteMovimiento>();
                    lstMovProgramado = (List<ReporteMovimiento>)Session["MovProgramado"];
                    gvMovProgramado.DataSource = lstMovProgramado;
                    gvMovProgramado.DataBind();
                }
                else
                {
                    MovimientoProgramado();
                }
            }
        }
        catch (Exception ex)
        {
            //BOexcepcion.Throw(personaServicio.CodigoPrograma, "gvCreditos_PageIndexChanging", ex);
        }
    }

    protected void gvMovAhorroVista_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            if (e.NewPageIndex >= 0)
            {
                gvMovAhorroVista.PageIndex = e.NewPageIndex;
                if (Session["MovAhorroVista"] != null)
                {
                    List<ReporteMovimiento> lstMovAhorro = new List<ReporteMovimiento>();
                    lstMovAhorro = (List<ReporteMovimiento>)Session["MovAhorroVista"];
                    gvMovAhorroVista.DataSource = lstMovAhorro;
                    gvMovAhorroVista.DataBind();
                }
                else
                {
                    MovimientoAhorrosVista();
                }
            }
        }
        catch (Exception ex)
        {
            //BOexcepcion.Throw(personaServicio.CodigoPrograma, "gvCreditos_PageIndexChanging", ex);
        }
    }

    protected void gvMovCredito_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            if (e.NewPageIndex >= 0)
            {
                gvMovCredito.PageIndex = e.NewPageIndex;
                if (Session["MovCredito"] != null)
                {
                    List<Object> lstMov = new List<Object>();
                    lstMov = (List<Object>)Session["MovCredito"];
                    gvMovCredito.DataSource = lstMov;
                    gvMovCredito.DataBind();
                }
                else
                {
                    MovimientoCreditos();
                }
            }
        }
        catch (Exception ex)
        {
            //BOexcepcion.Throw(personaServicio.CodigoPrograma, "gvCreditos_PageIndexChanging", ex);
        }
    }

    protected void gvMovAportes_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            if (e.NewPageIndex >= 0)
            {
                gvMovAportes.PageIndex = e.NewPageIndex;
                if (Session["MovAporte"] != null)
                {
                    List<Object> lstMovAporte = new List<Object>();
                    lstMovAporte = (List<Object>)Session["MovAporte"];
                    gvMovAportes.DataSource = lstMovAporte;
                    gvMovAportes.DataBind();
                }
                else
                {
                    MovimientoAportes();
                }
            }
        }
        catch (Exception ex)
        {
            //BOexcepcion.Throw(personaServicio.CodigoPrograma, "gvCreditos_PageIndexChanging", ex);
        }
    }

    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        Actualizar();
    }

    protected void btnRegresar_Click(object sender, EventArgs e)
    {
        if (mvConsolidado.ActiveViewIndex == 0)
        {
            Producto producto = new Producto();
            producto.Persona.IdPersona = Convert.ToInt64(txtCodCliente.Text);
            Session[MOV_GRAL_CRED_PRODUC] = producto;
            Navegar("~/Page/Asesores/EstadoCuenta/Detalle.aspx");
        }
        else if (mvConsolidado.ActiveViewIndex == 1)
        {
            Session["cod_persona"] = txtCodCliente.Text;
            Navegar("~/Page/Asesores/EstadoCuenta/ConsolidadoProductos/Detalle.aspx");
        }
    }

    protected void btnImprimir_Click(object sender, EventArgs e)
    {
        Usuario pUsu = new Usuario();
        pUsu = (Usuario)Session["usuario"];
        ReportParameter[] param = new ReportParameter[16];
        param[0] = new ReportParameter("entidad", " " + pUsu.empresa);
        param[1] = new ReportParameter("nit", " " + pUsu.nitempresa);
        param[2] = new ReportParameter("cod_cliente", " " + txtCodCliente.Text);
        param[3] = new ReportParameter("documento", " " + txtId.Text);
        param[4] = new ReportParameter("nom_documento", " " + txtTipoDoc.Text);
        param[5] = new ReportParameter("nombres", " " + txtNombres.Text);
        param[6] = new ReportParameter("direccion", " " + txtDireccion.Text);
        param[7] = new ReportParameter("telefono", " " + txtTelefono.Text);
        param[8] = new ReportParameter("creado", " " + pUsu.nombre);
        param[9] = new ReportParameter("RutaImagen", new Uri(Server.MapPath("~/Images/LogoEmpresa.jpg")).AbsoluteUri);
        string mostrarA = "true";
        if (gvMovAportes.Rows.Count > 0)
            mostrarA = "false";
        param[10] = new ReportParameter("MostrarA", mostrarA);
        string mostrarC = "true";
        if (gvMovCredito.Rows.Count > 0)
            mostrarC = "false";
        param[11] = new ReportParameter("MostrarC", mostrarC);
        string mostrarAV = "true";
        if (gvMovAhorroVista.Rows.Count > 0)
            mostrarAV = "false";
        param[12] = new ReportParameter("MostrarAV", mostrarAV);
        string mostrarP = "true";
        if (gvMovProgramado.Rows.Count > 0)
            mostrarP = "false";
        param[13] = new ReportParameter("MostrarP", mostrarP);
        string mostrarCD = "true";
        if (gvMovCDAT.Rows.Count > 0)
            mostrarCD = "false";
        param[14] = new ReportParameter("MostrarCD", mostrarCD);
        string mostrarS = "true";
        if (gvMovServicios.Rows.Count > 0)
            mostrarS = "false";
        param[15] = new ReportParameter("MostrarS", mostrarS);

        Rpview1.LocalReport.EnableExternalImages = true;

        ReportDataSource rdsA = new ReportDataSource("DataSetA", CrearDataTableA());
        ReportDataSource rdsC = new ReportDataSource("DataSetC", CrearDataTableC());
        ReportDataSource rdsAV = new ReportDataSource("DataSetAV", CrearDataTableAV());
        ReportDataSource rdsP = new ReportDataSource("DataSetP", CrearDataTableP());
        ReportDataSource rdsCD = new ReportDataSource("DataSetCD", CrearDataTableCD());
        ReportDataSource rdsS = new ReportDataSource("DataSetS", CrearDataTableS());

        Rpview1.LocalReport.DataSources.Clear();
        Rpview1.LocalReport.DataSources.Add(rdsA);
        Rpview1.LocalReport.DataSources.Add(rdsC);
        Rpview1.LocalReport.DataSources.Add(rdsAV);
        Rpview1.LocalReport.DataSources.Add(rdsP);
        Rpview1.LocalReport.DataSources.Add(rdsCD);
        Rpview1.LocalReport.DataSources.Add(rdsS);

        Rpview1.LocalReport.ReportEmbeddedResource = "Page/Asesores/EstadoCuenta/ConsolidadoProductos/ReportMovimiento.rdlc";
        Rpview1.LocalReport.SetParameters(param);        
        Rpview1.LocalReport.Refresh();
        Rpview1.Visible = true;
        mvConsolidado.ActiveViewIndex = 1;
        Site toolbar = (Site)Master;
        toolbar.MostrarConsultar(false);
        toolbar.MostrarImprimir(false);
    }
    

    public DataTable CrearDataTableA()
    {
        DataTable table = new DataTable();

        table.Columns.Add("NumeroAporte");
        table.Columns.Add("LineaAporte");
        table.Columns.Add("FechaPago");
        table.Columns.Add("CodOperacion");
        table.Columns.Add("TipoOperacion");
        table.Columns.Add("num_comp");
        table.Columns.Add("TIPO_COMP");
        table.Columns.Add("TipoMovimiento");
        table.Columns.Add("Capital");
        table.Columns.Add("Saldo");

        int pageIndex = gvMovAportes.PageIndex; //Guarda la página actual de la grilla
        for (int i=0; i<gvMovAportes.PageCount; i++) //Recorrer todas las páginas de la grilla
        {
            gvMovAportes.SetPageIndex(i); //Iniciar el recorrido con la primera página de la grilla
            foreach (GridViewRow row in gvMovAportes.Rows) //Recorrer las filas de la página actual de la grilla
            {
                DataRow datarw;
                datarw = table.NewRow();
                for (int j = 0; j < row.Cells.Count; j++) //Recorer las celdas de la fila
                {
                    datarw[j] = row.Cells[j].Text;
                }
                
                table.Rows.Add(datarw);
            }            
        }
        gvMovAportes.SetPageIndex(pageIndex); //Reestablecer la página en la que se encontraba la grilla
        return table;
    }
    public DataTable CrearDataTableC()
    {
        DataTable table = new DataTable();

        table.Columns.Add("NumeroRadicacion");
        table.Columns.Add("LineaCredito");
        table.Columns.Add("FechaCuota");
        table.Columns.Add("FechaPago");
        table.Columns.Add("TipoOperacion");
        table.Columns.Add("num_comp");
        table.Columns.Add("TIPO_COMP");
        table.Columns.Add("Transaccion");
        table.Columns.Add("TipoMovimiento");
        table.Columns.Add("Capital");
        table.Columns.Add("IntCte");
        table.Columns.Add("IntMora");
        table.Columns.Add("Poliza");
        table.Columns.Add("Seguros");
        table.Columns.Add("otros");
        table.Columns.Add("Prejuridico");
        table.Columns.Add("totalpago");
        table.Columns.Add("Saldo");
        
        int pageIndex = gvMovCredito.PageIndex;
        for (int i = 0; i < gvMovCredito.PageCount; i++) 
        {
            gvMovCredito.SetPageIndex(i); 
            foreach (GridViewRow row in gvMovCredito.Rows) 
            {
                DataRow datarw;
                datarw = table.NewRow();
                for (int j = 0; j < row.Cells.Count; j++)
                {
                    datarw[j] = row.Cells[j].Text;
                }
                table.Rows.Add(datarw);
            }
        }
        gvMovCredito.SetPageIndex(pageIndex); 
        return table;
    }
    public DataTable CrearDataTableAV()
    {
        //CREACION DE LA TABLA
        DataTable table = new DataTable();
        table.Columns.Add("numero_cuenta");
        table.Columns.Add("LineaAhorro");
        table.Columns.Add("fecha");
        table.Columns.Add("cod_ope");
        table.Columns.Add("tipo_ope");
        table.Columns.Add("tipo_tran");
        table.Columns.Add("num_comp");
        table.Columns.Add("tipo_comp");
        table.Columns.Add("tipo_mov");
        table.Columns.Add("valor");
        table.Columns.Add("saldo");
        
        int pageIndex = gvMovAhorroVista.PageIndex;
        for (int i = 0; i < gvMovAhorroVista.PageCount; i++) 
        {
            gvMovAhorroVista.SetPageIndex(i); 
            foreach (GridViewRow row in gvMovAhorroVista.Rows) 
            {
                DataRow datarw;
                datarw = table.NewRow();
                for (int j = 0; j < row.Cells.Count; j++)
                {
                    datarw[j] = row.Cells[j].Text;
                }
                table.Rows.Add(datarw);
            }
        }
        gvMovAhorroVista.SetPageIndex(pageIndex); 

        return table;
    }
    public DataTable CrearDataTableP()
    {
        DataTable table = new DataTable();
        table.Columns.Add("numero_cuenta");
        table.Columns.Add("LineaAhorro");
        table.Columns.Add("fecha");
        table.Columns.Add("cod_ope");
        table.Columns.Add("tipo_ope");
        table.Columns.Add("tipo_tran");
        table.Columns.Add("num_comp");
        table.Columns.Add("tipo_comp");
        table.Columns.Add("tipo_mov");
        table.Columns.Add("valor");
        table.Columns.Add("saldo");
        
        int pageIndex = gvMovProgramado.PageIndex;
        for (int i = 0; i < gvMovProgramado.PageCount; i++)
        {
            gvMovProgramado.SetPageIndex(i); 
            foreach (GridViewRow row in gvMovProgramado.Rows) 
            {
                DataRow datarw;
                datarw = table.NewRow();
                for (int j = 0; j < row.Cells.Count; j++)
                {
                    datarw[j] = row.Cells[j].Text;
                }
                table.Rows.Add(datarw);
            }
        }
        gvMovProgramado.SetPageIndex(pageIndex); 

        return table;
    }
    public DataTable CrearDataTableCD()
    {
        DataTable table = new DataTable();
        table.Columns.Add("numero_cdat");
        table.Columns.Add("LineaCdat");
        table.Columns.Add("fecha");
        table.Columns.Add("cod_ope");
        table.Columns.Add("tipo_ope");
        table.Columns.Add("num_comp");
        table.Columns.Add("tipo_comp");
        table.Columns.Add("tipo_mov");
        table.Columns.Add("valor");
        table.Columns.Add("saldo");
        
        int pageIndex = gvMovCDAT.PageIndex;
        for (int i = 0; i < gvMovCDAT.PageCount; i++)
        {
            gvMovCDAT.SetPageIndex(i);
            foreach (GridViewRow row in gvMovCDAT.Rows)
            {
                DataRow datarw;
                datarw = table.NewRow();
                for (int j = 0; j < row.Cells.Count; j++)
                {
                    datarw[j] = row.Cells[j].Text;
                }
                table.Rows.Add(datarw);
            }
        }
        gvMovCDAT.SetPageIndex(pageIndex);

        return table;
    }
    public DataTable CrearDataTableS()
    {
        DataTable table = new DataTable();
        table.Columns.Add("numero_servicio");
        table.Columns.Add("LineaServicio");
        table.Columns.Add("Fec_1Pago");
        table.Columns.Add("cod_persona");
        table.Columns.Add("descripcion");
        table.Columns.Add("numero_cuotas");
        table.Columns.Add("tipo_comp");
        table.Columns.Add("tipo_mov");
        table.Columns.Add("valor_total");
        table.Columns.Add("saldo");
        
        int pageIndex = gvMovServicios.PageIndex;
        for (int i = 0; i < gvMovServicios.PageCount; i++)
        {
            gvMovServicios.SetPageIndex(i);
            foreach (GridViewRow row in gvMovServicios.Rows)
            {
                DataRow datarw;
                datarw = table.NewRow();
                for (int j = 0; j < row.Cells.Count; j++)
                {
                    datarw[j] = row.Cells[j].Text.Contains("&#243;") ? row.Cells[j].Text.Replace("&#243;", "ó") : row.Cells[j].Text.Contains("&#233;") ?
                                row.Cells[j].Text.Replace("&#233;", "é") : row.Cells[j].Text.Contains("&#201;") ? row.Cells[j].Text.Replace("&#201;", "é") :
                                row.Cells[j].Text.Contains("&#211;") ? row.Cells[j].Text.Replace("&#211;", "Ó") : row.Cells[j].Text.Contains("&nbsp;") ?
                                row.Cells[j].Text.Replace("&nbsp;", "") : row.Cells[j].Text;
                }
                table.Rows.Add(datarw);
            }
        }
        gvMovServicios.SetPageIndex(pageIndex);

        return table;
    }

    
}