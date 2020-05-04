using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Contabilidad.Services;
using Xpinn.Contabilidad.Entities;
using Xpinn.Util;
public partial class Nuevo : GlobalWeb
{
    SaldosTercerosService SaldoServicio = new SaldosTercerosService();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            VisualizarOpciones(SaldoServicio.CodigoProgramaTraslado, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
            ctlmensaje.eventoClick += btnContinuar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(SaldoServicio.CodigoProgramaTraslado, "Page_PreInit", ex);
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                CargarLista();
                if (Session["cuenta_traslado"] != null)
                {
                    if (Session["fecha_traslado"] != null)
                    {
                        if (Session["fecha_traslado"].ToString() != "")
                            txtFecha.Text = ConvertirStringToDate(Session["fecha_traslado"].ToString()).ToShortDateString();                     
                    }
                    if (txtFecha.Text.Trim() == "")
                    { 
                        txtFecha.Text = DateTime.Now.ToShortDateString();
                    }
                    idObjeto = Session["cuenta_traslado"].ToString();
                    txtFechaComprobante.Text = txtFecha.Text;
                    ObtenerDatos(idObjeto);
                }
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(SaldoServicio.CodigoProgramaTraslado, "Page_Load", ex);
        }
    }



    protected void CargarLista()
    {
        PoblarListas Poblar = new PoblarListas();
        //Poblar.PoblarListaDesplegable("CENTRO_COSTO", " CENTRO_COSTO, NOM_CENTRO ", "", " 1 ", ddlCentroCosto, Usuario);
    }

    protected void Actualizar()
    {
        List<SaldosTerceros> lstSaldo = new List<SaldosTerceros>();
        SaldosTercerosService SaldoServicio = new SaldosTercerosService();
        DateTime fecha = (txtFecha.Text.Trim() == "" ? DateTime.Now : txtFecha.ToDateTime);
        lstSaldo = SaldoServicio.ListarTercerosTraslado(fecha, Convert.ToInt64(txtCodCuenta.Text), 0, (Usuario)Session["usuario"]);

        if (lstSaldo.Count > 0)
        {
            pListado.Visible = true;
            lblInfo.Visible = false;
            gvLista.DataSource = lstSaldo;
            gvLista.DataBind();
            lblTotalRegs.Text = "Registros encontrados " + lstSaldo.Count();
            lblTotalRegs.Visible = true;
            lblTraslado.Visible = true;

            Int64 valorDebito = Convert.ToInt64(lstSaldo.Where(x => x.tipo_mov == "Debito").Sum(x => x.saldo));
            Int64 valorCredito = Convert.ToInt64(lstSaldo.Where(x => x.tipo_mov == "Credito").Sum(x => x.saldo));
            lblTotalTraslado.Text = valorDebito - valorCredito > 0 ? (valorDebito - valorCredito).ToString("c0") : (valorCredito - valorDebito).ToString("c0");
        }
        else
        {
            pListado.Visible = false;
            lblInfo.Visible = true;
            lblTotalRegs.Visible = false;
            lblTraslado.Visible = false;
        }
    }

    protected void ObtenerDatos(string cod_cuenta)
    {
        PlanCuentasService CuentasServicio = new PlanCuentasService();
        PlanCuentas pCuenta = new PlanCuentas();

        string filtro = " And cod_cuenta = '" + cod_cuenta + "' ";
        DateTime fecha = (txtFecha.Text.Trim() == "" ? DateTime.Now : txtFecha.ToDateTime);
        pCuenta = CuentasServicio.ListarCuentasTraslado(filtro, fecha, (Usuario)Session["usuario"]).FirstOrDefault();
        if (pCuenta == null)
        {
            VerError("No se encontraron saldos de terceros en la cuenta para la fecha daada");
            return;
        }

        txtCodCuenta.Text = pCuenta.cod_cuenta;
        txtNomCuenta.Text = pCuenta.nombre;
        txtNaturaleza.Text = pCuenta.tipo;
        txtSaldo.Text = pCuenta.saldo.ToString("c0");

        Actualizar();
    }

    protected List<SaldosTerceros> ObtenerListado()
    {
        List<SaldosTerceros> lstTraslados = new List<SaldosTerceros>();

        foreach (GridViewRow fila in gvLista.Rows)
        {
            SaldosTerceros vTraslado = new SaldosTerceros();          
            if (gvLista.DataKeys[fila.RowIndex].Values.Count > 0)
                vTraslado.codtercero = gvLista.DataKeys[fila.RowIndex].Values[0].ToString();
            vTraslado.centro_costo = Convert.ToInt64(fila.Cells[3].Text);
            vTraslado.tipo_mov = fila.Cells[4].Text == "Debito" ? "C" : "D";
            vTraslado.saldo = Convert.ToInt64(fila.Cells[5].Text.Replace("$","").Replace(".","").Replace(",","."));
            
            lstTraslados.Add(vTraslado);
        }
        return lstTraslados;
    }


    protected void txtFecha_eventoCambiar(object sender, EventArgs e)
    {
        Actualizar();
    }

    protected bool ValidarDatos()
    {
        if (gvLista.Rows.Count == 0)
        {
            VerError("No hay valores para trasladar");
            return false;
        }
        if (txtFecha.Text == "")
        {
            VerError("Seleccione la fecha del traslado");
            return false;
        }
        if (txtFechaComprobante.Text == "")
        {
            VerError("Seleccione la fecha del comprobante");
            return false;
        }
        if (txtIdentificacion.Text == "")
        {
            VerError("Ingrese la indentificación del tercero que recibe el traslado");
            return false;
        }
        return true;
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            if (ValidarDatos())
                ctlmensaje.MostrarMensaje("¿Desea generar el traslado?");
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(SaldoServicio.CodigoProgramaTraslado, "btnGuardar_Click", ex);
        }
    }

    protected void btnContinuar_Click(object sender, EventArgs e)
    {
        VerError("");
        try
        {
            SaldosTerceros vTraslado = new SaldosTerceros();
            List<SaldosTerceros> lstTraslado = new List<SaldosTerceros>();
            SaldosTercerosService SaldosServicio = new SaldosTercerosService();
            lstTraslado = ObtenerListado();

            vTraslado.codtercero = txtCodTercero.Text;
            vTraslado.cod_cuenta = txtCodCuenta.Text;
            vTraslado.fechaini = Convert.ToDateTime(txtFecha.Texto);

            Int64 valorDebito = Convert.ToInt64(lstTraslado.Where(x => x.tipo_mov == "D").Sum(x => x.saldo));
            Int64 valorCredito = Convert.ToInt64(lstTraslado.Where(x => x.tipo_mov == "C").Sum(x => x.saldo));

            vTraslado.saldo = valorDebito - valorCredito > 0 ? valorDebito - valorCredito : valorCredito - valorDebito;
            vTraslado.tipo_mov = valorDebito - valorCredito > 0 ? "C" : "D";

            SaldosServicio.CrearTrasladoSaldoTer(vTraslado, lstTraslado, (Usuario)Session["usuario"]);

            if (vTraslado.cod_traslado != 0)
            {
                DateTime fecha_contabilizacion = txtFechaComprobante.ToDateTime;
                if (GenerarComprobanteTraslado(vTraslado.cod_traslado, fecha_contabilizacion))
                {
                    Navegar("../../Contabilidad/Comprobante/Nuevo.aspx");
                }                
            }

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(SaldoServicio.CodigoProgramaTraslado, "btnGuardar_Click", ex);
        }
    }

    protected bool GenerarComprobanteTraslado(Int64 pcod_traslado, DateTime fecha_contabilizacion)
    {
        try
        {
            Xpinn.Contabilidad.Services.ComprobanteService ComprobanteServicio = new Xpinn.Contabilidad.Services.ComprobanteService();
            Int64 num_comp = 0;
            Int64 tipo_comp = 0;
            try
            {                
                // Generar el comprobante de traslado  
                string error = "";
                bool generado = ComprobanteServicio.GenerarComprobanteTraslado(fecha_contabilizacion, pcod_traslado, ref num_comp, ref tipo_comp, ref error, (Usuario)Session["usuario"]);
                if (error.Trim() != "")
                {
                    VerError(error);
                    return false;
                }
            }
            catch (Exception ex)
            {
                VerError("Error al generar comprobante de traslado" + ex.Message);
                return false;
            }
            // Se cargan las variables requeridas para generar el comprobante
            Session[ComprobanteServicio.CodigoPrograma + ".num_comp"] = num_comp;
            Session[ComprobanteServicio.CodigoPrograma + ".tipo_comp"] = tipo_comp;
            Session[ComprobanteServicio.CodigoPrograma + ".cod_traslado"] = pcod_traslado;

            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }


    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }

    protected void txtIdPersona_TextChanged(object sender, EventArgs e)
    {
        Xpinn.Seguridad.Services.UsuarioService UsuarioServicio = new Xpinn.Seguridad.Services.UsuarioService();
        Xpinn.FabricaCreditos.Entities.Persona1 DatosPersona = new Xpinn.FabricaCreditos.Entities.Persona1();

        DatosPersona = UsuarioServicio.ConsultarPersona1(txtIdentificacion.Text, (Usuario)Session["usuario"]);

        if (DatosPersona.cod_persona != 0)
        {
            txtCodTercero.Text = DatosPersona.cod_persona.ToString();
            txtIdentificacion.Text = DatosPersona.identificacion != "" ? DatosPersona.identificacion : ""; 
            txtNomTercero.Text = DatosPersona.nombre != "" ? DatosPersona.nombre : "";
        }
    }

    protected void btnConsultaPersonas_Click(object sender, EventArgs e)
    {
        ctlBusquedaPersonas.Motrar(true, "txtCodTercero", "txtIdentificacion", "txtNomTercero");
    }



}