using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Comun.Entities;
using Xpinn.Interfaces.Entities;
using Xpinn.Interfaces.Services;
using Xpinn.TarjetaDebito.Entities;
using Xpinn.TarjetaDebito.Services;
using Xpinn.Util;

partial class Lista : GlobalWeb
{
    TarjetaService _tarjetaService = new TarjetaService();
    EnpactoServices _enpactoService = new EnpactoServices();
    CuentaService _cuentaService = new CuentaService();

    string _HostAppliance = "";
    string _convenio = "";
    readonly string _llave = "0123456789ABCDEFFEDCBA9876543210";
    readonly string _vector = "00000000000000000000000000000000";

    #region Metodos Eventos Iniciales


    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(_tarjetaService.CodigoProgramaBloqueoDesbloqueo, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoGuardar += btnGuardar_Click;
            ctlMensaje.eventoClick += btnContinuarMen_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_tarjetaService.CodigoProgramaBloqueoDesbloqueo, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                _convenio = ConvenioTarjeta();
                _HostAppliance = IpApplianceConvenioTarjeta();
                if (_convenio != "" && _HostAppliance != "")
                {
                    ViewState["_convenio"] = _convenio;
                    ViewState["_HostAppliance"] = _HostAppliance;
                }
            }
            // en cada postback rellenar estos valores
            _convenio = ViewState["_convenio"] as string;
            _HostAppliance = ViewState["_HostAppliance"] as string;

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_tarjetaService.CodigoPrograma, "Page_Load", ex);
        }
    }


    #endregion


    #region Metodos Eventos Varios


    protected void cbSeleccionarEncabezado_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox checkBoxHeader = gvLista.HeaderRow.FindControl("cbSeleccionarEncabezado") as CheckBox;

        foreach (GridViewRow row in gvLista.Rows)
        {
            CheckBox check = row.FindControl("chkSeleccionEmpleado") as CheckBox;

            check.Checked = checkBoxHeader.Checked;
        }
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Actualizar();
        }
        catch (Exception ex)
        {
            VerError("No se pudo consultar para realizar la gestion de las tarjetas, " + ex.Message);
        }
    }

    private void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        lblMensaje.Text = "Iniciar.";
        ctlMensaje.MostrarMensaje("Esta seguro de continuar con la operacion?");
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // PROCESO BLOQUEO
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////// 

        // Obtenemos todas las tarjetas que han sido seleccionadas para gestionar
        List<Tarjeta> listaTarjetasParaGestionar = ObtenerListaTarjetas();
        List<Tarjeta> lstCuentasCredito = new List<Tarjeta>(); //Agregada para Separar las cuentas de credito de las de ahorro en el bloqueo
        List<Tarjeta> lstCuentasCreditoxSaldo = new List<Tarjeta>(); //Agregada para Separar las cuentas de credito de las de ahorro en el bloqueo
        List<Tarjeta> lstCuentasAhorros = new List<Tarjeta>(); //Agregada para Separar las cuentas de ahorro de las de credito 

        //Consultamos el Parametro si se bloquea la tarjeta o se llevan el saldo disponible a 0 de la cuenta credito
        General general = ConsultarParametroGeneral(102); //Determina tipo bloqueo tarjeta 1=Bloqueo Cupo, Otro Valor=Bloqueo Tarjeta

        // Se filtran las tarjetas a bloquear que se consultan de la gridview
        List<Tarjeta> listaTarjetasParaBloquear = listaTarjetasParaGestionar.Where(x => x.pendienteParaBloquear == 1).ToList();

        //validamos que el filtro deje tarjetas para continuar el proceso
        if (listaTarjetasParaBloquear != null && listaTarjetasParaBloquear.Count > 0)
        {
            if (general.valor.Trim() == "1") //|| general.valor.Trim() == "2"
            {
                //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                // BLOQUEAR AHORRO
                //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                foreach (Tarjeta tarjeta in listaTarjetasParaBloquear)
                {
                    if (tarjeta.tipo_cuenta == "1" || tarjeta.tipo_cuenta == "A" || tarjeta.tipo_cuenta == "")
                    {
                        lstCuentasAhorros.Add(tarjeta);
                    }
                    if (tarjeta.tipo_cuenta == "2" || tarjeta.tipo_cuenta == "C" || tarjeta.tipo_cuenta == "Credito Rotativo")
                    {
                        lstCuentasCredito.Add(tarjeta);
                    }
                }

                if (lstCuentasAhorros != null && lstCuentasAhorros.Count > 0)
                {
                    List<Tarjeta> listaTarjetaBloqueadasSatisfactoriamenteAhorro = BloquearTarjetasEnpacto(lstCuentasAhorros);

                    if (listaTarjetaBloqueadasSatisfactoriamenteAhorro != null && listaTarjetaBloqueadasSatisfactoriamenteAhorro.Count > 0)
                    {
                        BloquearTarjetasFinancial(listaTarjetaBloqueadasSatisfactoriamenteAhorro);
                    }
                }

                //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                //BLOQUEAR SALDO DEL CREDITO ROTATIVO
                //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                if (lstCuentasCredito != null && lstCuentasCredito.Count > 0)
                {
                    foreach (Tarjeta tarjetaBS in lstCuentasCredito)
                    {
                        tarjetaBS.saldo_disponible = 0; //Se indica 0 al saldo disponible para bloquearlas
                        tarjetaBS.estado_saldo = 1;
                        lstCuentasCreditoxSaldo.Add(tarjetaBS);
                    }
                }
                if (lstCuentasCreditoxSaldo != null && lstCuentasCreditoxSaldo.Count > 0)
                {
                    List<Tarjeta> listaTarjetasConCuentasSinSaldo = SaldoFinancial(lstCuentasCreditoxSaldo);
                    if (listaTarjetasConCuentasSinSaldo != null && listaTarjetasConCuentasSinSaldo.Count > 0)
                    {
                        SaldoEnpacto(listaTarjetasConCuentasSinSaldo, "B");
                    }
                }
            }

            //si el parametro es diferente a 1 entonces se bloquea normal
            if (general.valor.Trim() != "1")
            {
                List<Tarjeta> listaTarjetaBloqueadasSatisfactoriamente = BloquearTarjetasEnpacto(listaTarjetasParaBloquear);
                if (listaTarjetaBloqueadasSatisfactoriamente != null && listaTarjetaBloqueadasSatisfactoriamente.Count > 0)
                {
                    BloquearTarjetasFinancial(listaTarjetaBloqueadasSatisfactoriamente);
                }
            }
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // PROCESO DESBLOQUEO
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////// 

        // Se filtran las tarjetas a desbloquear que se consultan de la gridview
        List<Tarjeta> listaTarjetasParaDesbloquear = listaTarjetasParaGestionar.Where(x => x.pendienteParaDesbloquear == 1).ToList();

        if (listaTarjetasParaDesbloquear != null && listaTarjetasParaDesbloquear.Count > 0)
        {
            if (general.valor == "1") //|| general.valor == "2"
            {
                lstCuentasCredito.Clear(); // = null;
                lstCuentasAhorros.Clear(); // = null;

                //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                //DESBLOQUEAR AHORRO
                //////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                foreach (Tarjeta tarjetaD in listaTarjetasParaDesbloquear)
                {
                    if (tarjetaD.tipo_cuenta == "1" || tarjetaD.tipo_cuenta == "A" || tarjetaD.tipo_cuenta == "")
                    {
                        lstCuentasAhorros.Add(tarjetaD);
                    }

                    if (tarjetaD.tipo_cuenta == "2" || tarjetaD.tipo_cuenta == "C" || tarjetaD.tipo_cuenta == "Credito Rotativo")
                    {
                        lstCuentasCredito.Add(tarjetaD);
                    }
                }
                if (lstCuentasAhorros != null && lstCuentasAhorros.Count > 0)
                {
                    List<Tarjeta> listaTarjetaDesbloqueadasAhorros = DesbloquearTarjetasEnpacto(lstCuentasAhorros);

                    if (listaTarjetaDesbloqueadasAhorros != null && listaTarjetaDesbloqueadasAhorros.Count > 0)
                    {
                        DesbloquearTarjetasFinancial(listaTarjetaDesbloqueadasAhorros);
                    }
                }

                //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                // DESBLOQUEAR SALDO DEL CREDITO
                //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                //lstCuentasCredito = ListarTarjetasBloqueadasYAlDiaXSaldo();
                if (lstCuentasCredito != null && lstCuentasCredito.Count > 0)
                {
                    foreach (Tarjeta tarjetaBS in lstCuentasCredito)
                    {
                        tarjetaBS.saldo_disponible = 1; //Se indica un saldo diferente a 0 para verificar por la auditoria
                        tarjetaBS.estado_saldo = 0;
                        lstCuentasCreditoxSaldo.Add(tarjetaBS);
                    }
                }

                if (lstCuentasCreditoxSaldo != null && lstCuentasCreditoxSaldo.Count > 0)
                {
                    List<Tarjeta> listaTarjetasConCuentasSinSaldo = SaldoFinancial(lstCuentasCreditoxSaldo);
                    if (listaTarjetasConCuentasSinSaldo != null && listaTarjetasConCuentasSinSaldo.Count > 0)
                    {
                        SaldoEnpacto(listaTarjetasConCuentasSinSaldo, "D");
                    }
                }
            }

            // Se realiza proceso de desbloqueo de la tarjeta en ENPACTO según el parámetro general
            if (general.valor != "1")
            {
                // Desbloqueo de la tarjeta en ENPACTO
                //listaTarjetasParaDesbloquear = ListarTarjetasBloqueadasYAlDia();
                if (listaTarjetasParaDesbloquear != null && listaTarjetasParaDesbloquear.Count > 0)
                {
                    List<Tarjeta> listaTarjetaDesbloqueadasSatisfactoriamente = DesbloquearTarjetasEnpacto(listaTarjetasParaDesbloquear);

                    if (listaTarjetaDesbloqueadasSatisfactoriamente != null && listaTarjetaDesbloqueadasSatisfactoriamente.Count > 0)
                    {
                        DesbloquearTarjetasFinancial(listaTarjetaDesbloqueadasSatisfactoriamente);
                    }
                }
            }

            // Actualizamos la GridView
            lblMensaje.Text = "Proceso Terminado.";
            Actualizar();
        }

        else
        {
            lblMensaje.Text = " No se pudo obtener listado.";
        }
    }


    #endregion


    #region Metodos Ayuda


    void Actualizar()
    {
        VerError("");

        // Buscamos todas las tarjetas que estan en mora y deberian bloquearse
        List<Tarjeta> listaTarjetasParaBloquear = ListarTarjetasEnMoraYNoBloqueadas();

        // Preparamos todas estas tarjetas para mostrarlas en la GridView
        foreach (Tarjeta tarjetaPendienteParaBloquear in listaTarjetasParaBloquear)
        {
            tarjetaPendienteParaBloquear.pendienteParaBloquear = 1;
            tarjetaPendienteParaBloquear.desc_estado_pendiente = "Bloquear";
        }

        // Buscamos todas las tarjetas al dia que deberian desbloquearse
        List<Tarjeta> listaTarjetasParaDesbloquear = ListarTarjetasBloqueadasYAlDia();

        // Preparamos todas stas tarjetas para mostrarlas en la GridView
        foreach (Tarjeta tarjetaPendienteParaDesbloquear in listaTarjetasParaDesbloquear)
        {
            tarjetaPendienteParaDesbloquear.pendienteParaDesbloquear = 1;
            tarjetaPendienteParaDesbloquear.desc_estado_pendiente = "Desbloquear";
        }

        // Armamos una sola lista
        listaTarjetasParaBloquear.AddRange(listaTarjetasParaDesbloquear);

        // Filtramos segun los criterios antes de bondear la lista
        List<Tarjeta> listaTarjetasFiltradas = FiltrarLista(listaTarjetasParaBloquear);

        // Bindeamos la nueva lista
        gvLista.DataSource = listaTarjetasFiltradas;
        gvLista.DataBind();
    }

    // Buscamos todas las tarjetas que estan en mora y necesitan bloquearse segun el parametro general
    List<Tarjeta> ListarTarjetasEnMoraYNoBloqueadas()
    {
        General general = ConsultarParametroGeneral(41);

        if (!string.IsNullOrWhiteSpace(general.valor))
        {
            int numeroDeDiasParaBloquearTarjetas = Convert.ToInt32(general.valor);
            //Consultamos si tiene encuenta todos los productos para bloquear la cuenta o por lo contrario solo el rotativo  
            General generalProductos = ConsultarParametroGeneral(106);
            string ProductosenCuentaparaBloqueo = generalProductos.valor.ToString();

            //Consultamos el tipo de Bloqueo que se esta utilizando 1=Bloqueo Cupo, Otro Valor=Bloqueo Tarjeta
            General tipoBloqueo = ConsultarParametroGeneral(102);
            int tipo_bloqueo = Convert.ToInt32(tipoBloqueo.valor);


            List<Tarjeta> listaTarjetasParaBloquear = _tarjetaService.ListarTarjetasEnMoraYNoBloqueadas(numeroDeDiasParaBloquearTarjetas, ProductosenCuentaparaBloqueo, tipo_bloqueo, Usuario);

            return listaTarjetasParaBloquear;
        }
        else
        {
            return new List<Tarjeta>();
        }
    }

    // Listamos las tarjetas que fueron o estan bloqueadas pero estan al dia
    List<Tarjeta> ListarTarjetasBloqueadasYAlDia()
    {
        General tipoBloqueo = ConsultarParametroGeneral(102);
        int ptipo_bloqueo = Convert.ToInt32(tipoBloqueo.valor);

        List<Tarjeta> listaTarjetasParaDesbloquear = _tarjetaService.ListarTarjetasBloqueadasYAlDia(ptipo_bloqueo, Usuario);

        return listaTarjetasParaDesbloquear;
    }

    List<Tarjeta> FiltrarLista(List<Tarjeta> listaTarjetasParaFiltrar)
    {
        IQueryable<Tarjeta> queryable = listaTarjetasParaFiltrar.AsQueryable();

        if (!string.IsNullOrWhiteSpace(txtNumTarjeta.Text))
        {
            queryable = queryable.Where(x => x.numtarjeta.Trim().Contains(txtNumTarjeta.Text.Trim()));
        }

        if (!string.IsNullOrWhiteSpace(txtIdentificacion.Text))
        {
            queryable = queryable.Where(x => x.identificacion.Trim().Contains(txtIdentificacion.Text.Trim()));
        }

        if (!string.IsNullOrWhiteSpace(txtNumeroCuenta.Text))
        {
            queryable = queryable.Where(x => x.numero_cuenta.Trim().Contains(txtNumeroCuenta.Text.Trim()));
        }

        if (!string.IsNullOrWhiteSpace(ddlEstadoActual.SelectedValue))
        {
            queryable = queryable.Where(x => x.estado.Trim().Contains(ddlEstadoActual.SelectedValue.Trim()));
        }

        if (!string.IsNullOrWhiteSpace(ddlEstadoFuturo.SelectedValue))
        {
            int estadoFuturo = Convert.ToInt32(ddlEstadoFuturo.SelectedValue);

            if (estadoFuturo == 1) // Esta para desbloquear
            {
                queryable = queryable.Where(x => x.pendienteParaDesbloquear == 1);
            }
            else if (estadoFuturo == 2) // Esta para bloquear
            {
                queryable = queryable.Where(x => x.pendienteParaBloquear == 1);
            }
        }

        return queryable.ToList();
    }

    List<Tarjeta> ObtenerListaTarjetas()
    {
        List<Tarjeta> listaTarjetas = new List<Tarjeta>();

        foreach (GridViewRow row in gvLista.Rows)
        {
            CheckBox checkBox = row.FindControl("chkSeleccionEmpleado") as CheckBox;

            if (checkBox.Checked)
            {
                string nombre = HttpUtility.HtmlDecode(row.Cells[0].Text);
                string identificacion = HttpUtility.HtmlDecode(row.Cells[1].Text);
                string numeroTarjeta = HttpUtility.HtmlDecode(row.Cells[2].Text);
                string tipoCuenta = HttpUtility.HtmlDecode(row.Cells[3].Text);
                int codTipoCuenta = tipoCuenta.Trim() == "Credito Rotativo" ? 2 : 1;
                string numeroCuenta = HttpUtility.HtmlDecode(row.Cells[4].Text);
                string diasMora = HttpUtility.HtmlDecode(row.Cells[5].Text);

                Label lblidtarjeta = row.FindControl("lblidtarjeta") as Label;
                string sidTarjeta = HttpUtility.HtmlDecode(lblidtarjeta.Text);

                Label lblpendienteParaBloquear = row.FindControl("lblpendienteParaBloquear") as Label;
                string pendienteParaBloquear = HttpUtility.HtmlDecode(lblpendienteParaBloquear.Text);

                Label lblpendienteParaDesbloquear = row.FindControl("lblpendienteParaDesbloquear") as Label;
                string pendienteParaDesbloquear = HttpUtility.HtmlDecode(lblpendienteParaDesbloquear.Text);

                Label lblEstado = row.FindControl("lblEstado") as Label;
                string estado = HttpUtility.HtmlDecode(lblEstado.Text);

                Tarjeta tarjeta = new Tarjeta
                {
                    nombres = nombre,
                    identificacion = identificacion,
                    numtarjeta = numeroTarjeta,
                    tipo_cuenta = tipoCuenta,
                    cod_tipocta = codTipoCuenta,
                    numero_cuenta = numeroCuenta,
                    dias_mora = Convert.ToInt32(diasMora),
                    pendienteParaBloquear = Convert.ToInt32(pendienteParaBloquear),
                    pendienteParaDesbloquear = Convert.ToInt32(pendienteParaDesbloquear),
                    estado = estado,
                    idtarjeta = Convert.ToInt32(sidTarjeta)
                };

                listaTarjetas.Add(tarjeta);
            }
        }

        return listaTarjetas;
    }


    #endregion


    #region Metodos Bloquear Tarjetas


    void BloquearTarjetas(List<Tarjeta> listaTarjetasParaBloquear)
    {
        List<Tarjeta> listaTarjetaBloqueadasSatisfactoriamente = BloquearTarjetasEnpacto(listaTarjetasParaBloquear);

        if (listaTarjetaBloqueadasSatisfactoriamente != null && listaTarjetaBloqueadasSatisfactoriamente.Count > 0)
        {
            lblMensaje.Text += " Actualizando Financial.";
            BloquearTarjetasFinancial(listaTarjetaBloqueadasSatisfactoriamente);
        }
        else
        {
            lblMensaje.Text += " No se pudo bloquear las tarjetas.";
        }
    }

    // Mandamos las tarjetas a bloquear a Enpacto
    List<Tarjeta> BloquearTarjetasEnpacto(List<Tarjeta> listaTarjetasParaBloquear)
    {
        List<Tarjeta> listaTarjetaBloqueadasSatisfactoriamente = new List<Tarjeta>();

        InterfazENPACTO interfazENPACTO = new InterfazENPACTO(_llave, _vector);

        // Mando a generar la transaccion de enpacto     
        string s_usuario_applicance = "webservice";
        string s_clave_appliance = "WW.EE.99";
        SeguridadConvenioTarjeta(ref s_usuario_applicance, ref s_clave_appliance);
        interfazENPACTO.ConfiguracionAppliance(IpApplianceConvenioTarjeta(), s_usuario_applicance, s_clave_appliance);

        foreach (Tarjeta tarjeta in listaTarjetasParaBloquear)
        {
            DateTime dateToReport = DateTime.Now;
            string secuencia = dateToReport.Ticks.ToString().Substring(0, 12);

            Xpinn.TarjetaDebito.Entities.TransaccionEnpacto transaccion = new Xpinn.TarjetaDebito.Entities.TransaccionEnpacto
            {
                tipo = "B",
                fecha = dateToReport.ToString("yyMMdd"),
                hora = dateToReport.Hour.ToString("D2") + dateToReport.Minute.ToString("D2") + "00",
                secuencia = secuencia,
                cuenta = tarjeta.numero_cuenta,
                tarjeta = tarjeta.numtarjeta,
                monto = "0"
            };

            string error = string.Empty;
            Xpinn.TarjetaDebito.Entities.RespuestaEnpacto respuesta = new Xpinn.TarjetaDebito.Entities.RespuestaEnpacto();

            bool operacionExitosa = interfazENPACTO.GenerarTransaccionENPACTO(_convenio, transaccion, false, ref respuesta, ref error);

            if (operacionExitosa && string.IsNullOrWhiteSpace(error))
            {
                listaTarjetaBloqueadasSatisfactoriamente.Add(tarjeta);
            }
            else
            {
                lblMensaje.Text += " Error:" + error;
            }

            Enpacto_Aud enpactoAud = new Enpacto_Aud
            {
                exitoso = operacionExitosa && string.IsNullOrWhiteSpace(error) ? 1 : 0,
                jsonentidadpeticion = transaccion != null ? JsonConvert.SerializeObject(transaccion) : string.Empty,
                jsonentidadrespuesta = respuesta != null ? JsonConvert.SerializeObject(respuesta) : string.Empty,
                tipooperacion = 3 // 3 - WebServices EnpactoSVC Bloqueo/Desbloqueo
            };
            Usuario usu = new Usuario();
            if (Session["Usuario"] != null) usu = (Usuario)Session["Usuario"];
            _enpactoService.CrearEnpacto_Aud(enpactoAud, usu);
        }

        return listaTarjetaBloqueadasSatisfactoriamente;
    }

    // Segun las tarjetas bloqueadas por Enpacto, ahora las bloqueamos en Financial
    void BloquearTarjetasFinancial(List<Tarjeta> listaTarjetaBloqueadasSatisfactoriamente)
    {
        foreach (Tarjeta tarjeta in listaTarjetaBloqueadasSatisfactoriamente)
        {
            _tarjetaService.CambiarEstadoTarjeta(tarjeta, EstadoTarjetaEnpacto.Bloqueada, Usuario);
        }
    }


    #endregion


    #region Metodos Desbloqueo de Tarjetas


    void DesbloquearTarjetas(List<Tarjeta> listaTarjetasParaDesbloquear)
    {
        List<Tarjeta> listaTarjetaDesbloqueadasSatisfactoriamente = DesbloquearTarjetasEnpacto(listaTarjetasParaDesbloquear);

        if (listaTarjetaDesbloqueadasSatisfactoriamente != null && listaTarjetaDesbloqueadasSatisfactoriamente.Count > 0)
        {
            DesbloquearTarjetasFinancial(listaTarjetaDesbloqueadasSatisfactoriamente);
        }
    }

    // Mandamos las tarjetas a desbloquer a Enpacto
    List<Tarjeta> DesbloquearTarjetasEnpacto(List<Tarjeta> listaTarjetasParaDesbloquear)
    {
        List<Tarjeta> listaTarjetaDesbloqueadasSatisfactoriamente = new List<Tarjeta>();

        InterfazENPACTO interfazENPACTO = new InterfazENPACTO(_llave, _vector);

        // Mando a generar la transaccion de enpacto
        string s_usuario_applicance = "webservice";
        string s_clave_appliance = "WW.EE.99";
        SeguridadConvenioTarjeta(ref s_usuario_applicance, ref s_clave_appliance);
        interfazENPACTO.ConfiguracionAppliance(IpSwitchConvenioTarjeta(), s_usuario_applicance, s_clave_appliance);

        foreach (Tarjeta tarjeta in listaTarjetasParaDesbloquear)
        {
            DateTime dateToReport = DateTime.Now;
            string secuencia = dateToReport.Ticks.ToString().Substring(0, 12);

            Xpinn.TarjetaDebito.Entities.TransaccionEnpacto transaccion = new Xpinn.TarjetaDebito.Entities.TransaccionEnpacto
            {
                tipo = "D",
                fecha = dateToReport.ToString("yyMMdd"),
                hora = dateToReport.Hour.ToString("D2") + dateToReport.Minute.ToString("D2") + "00",
                secuencia = secuencia,
                cuenta = tarjeta.numero_cuenta,
                tarjeta = tarjeta.numtarjeta,
                monto = "0"
            };

            string error = string.Empty;
            Xpinn.TarjetaDebito.Entities.RespuestaEnpacto respuesta = new Xpinn.TarjetaDebito.Entities.RespuestaEnpacto();

            bool operacionExitosa = interfazENPACTO.GenerarTransaccionENPACTO(_convenio, transaccion, false, ref respuesta, ref error);

            if (operacionExitosa && string.IsNullOrWhiteSpace(error))
            {
                listaTarjetaDesbloqueadasSatisfactoriamente.Add(tarjeta);
            }

            Enpacto_Aud enpactoAud = new Enpacto_Aud
            {
                exitoso = operacionExitosa && string.IsNullOrWhiteSpace(error) ? 1 : 0,
                jsonentidadpeticion = transaccion != null ? JsonConvert.SerializeObject(transaccion) : string.Empty,
                jsonentidadrespuesta = respuesta != null ? JsonConvert.SerializeObject(respuesta) : string.Empty,
                tipooperacion = 3 // 3 - WebServices EnpactoSVC Bloqueo/Desbloqueo
            };

            Usuario usu = new Usuario();
            if (Session["Usuario"] != null) usu = (Usuario)Session["Usuario"];
            _enpactoService.CrearEnpacto_Aud(enpactoAud, usu);
        }

        return listaTarjetaDesbloqueadasSatisfactoriamente;
    }

    // Segun las tarjetas desbloqueadas por Enpacto, ahora las desbloqueamos en Financial
    void DesbloquearTarjetasFinancial(List<Tarjeta> listaTarjetaDesbloqueadasSatisfactoriamente)
    {
        foreach (Tarjeta tarjeta in listaTarjetaDesbloqueadasSatisfactoriamente)
        {
            _tarjetaService.CambiarEstadoTarjeta(tarjeta, EstadoTarjetaEnpacto.Activa, Usuario);
        }
    }


    #endregion


    #region CambiodeSaldo
    List<Tarjeta> SaldoEnpacto(List<Tarjeta> listaTarjetasSaldoCero, string ProcesoEnp)
    {
        InterfazENPACTO interfazENPACTO = new InterfazENPACTO(_llave, _vector);
        string s_usuario_applicance = "webservice";
        string s_clave_appliance = "WW.EE.99";
        SeguridadConvenioTarjeta(ref s_usuario_applicance, ref s_clave_appliance);
        interfazENPACTO.ConfiguracionAppliance(IpApplianceConvenioTarjeta(), s_usuario_applicance, s_clave_appliance);
        List<Tarjeta> ListTarjetasSaldoACero = new List<Tarjeta>();
        string error = "";
        string proceso = "";


        if (listaTarjetasSaldoCero != null && listaTarjetasSaldoCero.Count > 0)
        {
            string ruta = "C:\\Publica"; //"C:\\Publica\\LogEnpacto";
            string archivo = _convenio + DateTime.Now.ToString("ddMMyyyy") + ".cls";
            string rutayarchivo = ruta + "\\" + archivo;
            System.IO.StreamWriter newfile = new StreamWriter(rutayarchivo);
            string separador = ";";

            foreach (Tarjeta tarjeta in listaTarjetasSaldoCero)
            {
                Cuenta cuenta = new Cuenta();
                if (!string.IsNullOrEmpty(tarjeta.numero_cuenta))
                    cuenta.numero_cuenta = Convert.ToString(tarjeta.numero_cuenta);
                                
                General pGeneral = ConsultarParametroGeneral(107);
                List<Cuenta> entidad = new List<Cuenta>();
                entidad = _cuentaService.ListarCuenta(cuenta, pGeneral.valor, Usuario);

                if (entidad != null && ProcesoEnp == "B")
                {
                    entidad[0].saldodisponible = 0;
                }

                if (entidad[0].tipocuenta == "C" || entidad[0].tipocuenta == "R" || entidad[0].tipocuenta == "2")
                {
                    string linea = "";
                    linea = entidad[0].identificacion + separador + entidad[0].nombres.Trim() + separador + EsNulo(entidad[0].direccion, "").Trim() + separador + EsNulo(entidad[0].telefono, "").Trim() + separador +
                               EsNulo(entidad[0].email, "").Trim() + separador + entidad[0].tipocuenta + separador + entidad[0].nrocuenta.Trim() + separador + Math.Round(entidad[0].saldodisponible) + separador +
                               Math.Round(entidad[0].saldototal) + separador + entidad[0].fechasaldo.ToString("dd/MM/yyyy");
                    newfile.WriteLine(linea);
                }

            }
            newfile.Close();
            lblMensaje.Text = "Archivo " + rutayarchivo + " generado correctamente";
            // Verificar que el archivo se creeo correctamente
            System.IO.StreamReader file = new System.IO.StreamReader(rutayarchivo);
            if (file != null)
            {
                RespuestaEnpactoClientes respuestaEnpacto = null;
                //VAMOS AQUI debe consumir los valores del archivo por el web service
                lblMensaje.Text = "Ejecutando WEBSERVICES de ENPACTO. Archivo: " + rutayarchivo;
                interfazENPACTO.ServicioCLIENTESENPACTO(_convenio, archivo, rutayarchivo, "false", ref proceso, ref error, ref respuestaEnpacto);

                // Verificamos que se halla podido transformar la respuesta del servicio a la entidad respectiva y que halla cuentas que revisar
                if (respuestaEnpacto != null && respuestaEnpacto.relaciones != null && respuestaEnpacto.relaciones.Count > 0)
                {
                    foreach (RelacionClienteEnpacto relacion in respuestaEnpacto.relaciones)
                    {
                        if (relacion.tarjeta != null && relacion.cuenta != null)
                        {
                            // Pasamos la info a una entidad del sistema
                            Tarjeta tarjeta = new Tarjeta
                            {
                                numtarjeta = relacion.tarjeta,
                                numero_cuenta = relacion.cuenta
                            };

                            // Verificamos si la tarjeta existe en nuestro sistema
                            bool existe = _cuentaService.VerificarSiTarjetaExiste(tarjeta, Usuario);

                            // Si no existe la creamos, este SP crea la tarjeta segun la informacion de la cuenta asociada
                            // Consultara si es un Ahorro o un Credito y creara la tarjeta segun sea el caso
                            if (!existe)
                            {
                                tarjeta = _cuentaService.CrearTarjeta(tarjeta, Usuario);
                            }
                        }
                    }
                }
                file.Close();
            }
            return ListTarjetasSaldoACero;
        }
        return null;
    }

    private List<Tarjeta> SaldoFinancial(List<Tarjeta> listaTarjetasSaldo)
    {
        string Error = "";
        List<Tarjeta> LstFull = new List<Tarjeta>();
        foreach (Tarjeta tarjeta in listaTarjetasSaldo)
        {
            Tarjeta tarjetaR = _tarjetaService.ActualizarSaldoTarjeta(tarjeta, ref Error, Usuario);
            if ((tarjeta.saldo_disponible == 0 || tarjeta.saldo_disponible == 1) && (tarjeta.tipo_cuenta == "2" || tarjeta.tipo_cuenta == "Credito Rotativo" || tarjeta.tipo_cuenta == "C"))
            {
                LstFull.Add(tarjetaR);
            }
        }
        return LstFull;
    }

    private List<Tarjeta> ListarTarjetasEnMoraYNoBloqueadasXSaldo()
    {
        //Consulta los dias apartir se debe bloquear la tarjeta        
        General general = ConsultarParametroGeneral(41);

        if (!string.IsNullOrWhiteSpace(general.valor))
        {
            int numeroDeDiasParaBloquearTarjetas = Convert.ToInt32(general.valor);

            List<Tarjeta> listaTarjetasParaBloquear = _tarjetaService.ListarTarjetasEnMoraYNoBloqueadasXSaldo(numeroDeDiasParaBloquearTarjetas, Usuario);

            return listaTarjetasParaBloquear;
        }
        else
        {
            return new List<Tarjeta>();
        }
    }

    private List<Tarjeta> ListarTarjetasBloqueadasYAlDiaXSaldo()
    {
        List<Tarjeta> listaTarjetasParaDesbloquear = _tarjetaService.ListarTarjetasBloqueadasYAlDiaXSaldo(Usuario);

        return listaTarjetasParaDesbloquear;
    }

    private string EsNulo(string pDato, string pDefault)
    {
        if (pDato == null)
            return pDefault;
        pDato.Replace("á", "a");
        pDato.Replace("é", "e");
        pDato.Replace("í", "i");
        pDato.Replace("ó", "o");
        pDato.Replace("ú", "u");
        pDato.Replace("Á", "A");
        pDato.Replace("É", "E");
        pDato.Replace("Í", "I");
        pDato.Replace("Ó", "O");
        pDato.Replace("Ú", "U");
        return pDato;
    }

    #endregion

}