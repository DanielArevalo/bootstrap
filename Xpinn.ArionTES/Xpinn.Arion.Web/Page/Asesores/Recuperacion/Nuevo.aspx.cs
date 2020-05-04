using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Asesores.Services;
using Xpinn.Util;
using Xpinn.Asesores.Entities;
using Xpinn.FabricaCreditos.Services;
using Xpinn.FabricaCreditos.Entities;

public partial class Nuevo : GlobalWeb
{
    ClienteService clienteServicio = new ClienteService();
    CreditoService creditoServicio = new CreditoService();
    ProcesosCobroService procesosCobroServicio = new ProcesosCobroService();
    Int64 codProceso, codUsuario, codMotivo;

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            //if (Session[clienteServicio.CodigoPrograma + ".id"] != null)
            //    VisualizarOpciones(clienteServicio.CodigoPrograma, "E");
            //else
            //    VisualizarOpciones(clienteServicio.CodigoPrograma, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(procesosCobroServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Session[clienteServicio.CodigoPrograma + ".id"] != null && Session[creditoServicio.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[clienteServicio.CodigoPrograma + ".id"].ToString();
                    Session.Remove(clienteServicio.CodigoPrograma + ".id");
                    ObtenerDatosCliente(idObjeto);
                    idObjeto = Session[creditoServicio.CodigoPrograma + ".id"].ToString();
                    Session.Remove(creditoServicio.CodigoPrograma + ".id");
                    ObtenerDatosCredito(idObjeto);
                    ObtenerDatosProceso(idObjeto);
                }
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(procesosCobroServicio.CodigoPrograma, "Page_Load", ex);
        }
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            CobrosCreditoService cobroCreditoServicio = new CobrosCreditoService();
            CobrosCredito cobroCredito = new CobrosCredito();
            cobroCredito.numero_radicacion = Convert.ToInt64(idObjeto);
            cobroCredito.estado_proceso = Convert.ToInt64(ddlProceso.SelectedValue);
            cobroCredito.encargado = Convert.ToInt64(ddlUsuario.SelectedValue);
            cobroCredito.cod_motivo_cambio = Convert.ToInt64(ddlMotivo.SelectedValue);

            if (idObjeto != "")
            {
                //vReferencia.numero_radicacion = Convert.ToInt64(idObjeto);
                cobroCreditoServicio.ModificarCobrosCredito(cobroCredito, (Usuario)Session["usuario"]);
            }
            else
            {
                //vReferencia = ReferenciaServicio.CrearReferencia(vReferencia, (Usuario)Session["usuario"]);
                //idObjeto = vReferencia.numero_radicacion.ToString();
            }

            Session[procesosCobroServicio.CodigoPrograma + ".id"] = idObjeto;
            //Navegar(Pagina.Detalle);
            Navegar(Pagina.Lista);
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(procesosCobroServicio.CodigoPrograma, "btnGuardar_Click", ex);
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
            Session[procesosCobroServicio.CodigoPrograma + ".id"] = idObjeto;
            Navegar(Pagina.Detalle);
        }
    }

    protected void ObtenerDatosCliente(String pIdObjeto)
    {
        try
        {
            Cliente cliente = new Cliente();

            if (pIdObjeto != null)
            {
                cliente.IdCliente = Int64.Parse(pIdObjeto);
                cliente = clienteServicio.ConsultarClienteEjecutivo(cliente.IdCliente, (Usuario)Session["usuario"]);

                if (!string.IsNullOrEmpty(cliente.IdCliente.ToString()))
                    txtCodigoCliente.Text = HttpUtility.HtmlDecode(cliente.IdCliente.ToString());
                if (!string.IsNullOrEmpty(cliente.TipoDocumento))
                    txtTipoIdCliente.Text = HttpUtility.HtmlDecode(cliente.TipoDocumento);
                if (!string.IsNullOrEmpty(cliente.NumeroDocumento.ToString()))
                    txtIdentificacionCliente.Text = HttpUtility.HtmlDecode(cliente.NumeroDocumento.ToString());
                if (!string.IsNullOrEmpty(cliente.PrimerNombre))
                    txtPrimerNombreCliente.Text = HttpUtility.HtmlDecode(cliente.PrimerNombre);
                if (!string.IsNullOrEmpty(cliente.SegundoNombre))
                    txtSegundoNombreCliente.Text = HttpUtility.HtmlDecode(cliente.SegundoNombre);
                if (!string.IsNullOrEmpty(cliente.PrimerApellido))
                    txtPrimerApellidoCliente.Text = HttpUtility.HtmlDecode(cliente.PrimerApellido);
                if (!string.IsNullOrEmpty(cliente.SegundoApellido))
                    txtSegundoApellidoCliente.Text = HttpUtility.HtmlDecode(cliente.SegundoApellido);
                if (!string.IsNullOrEmpty(cliente.Direccion))
                    txtDireccionCliente.Text = HttpUtility.HtmlDecode(cliente.Direccion);
                if (!string.IsNullOrEmpty(cliente.Telefono))
                    txtTelefonoCliente.Text = HttpUtility.HtmlDecode(cliente.Telefono);
                if (!string.IsNullOrEmpty(cliente.Email))
                    txtEmailCliente.Text = HttpUtility.HtmlDecode(cliente.Email);
                if (!string.IsNullOrEmpty(cliente.Calificacion.ToString()))
                    txtCalificacionCliente.Text = HttpUtility.HtmlDecode(cliente.Calificacion.ToString());
                if (!string.IsNullOrEmpty(cliente.NombreZona))
                    txtZonaCliente.Text = HttpUtility.HtmlDecode(cliente.NombreZona);
                if (!string.IsNullOrEmpty(cliente.NombreOficina))
                    txtOficinaCliente.Text = HttpUtility.HtmlDecode(cliente.NombreOficina);
                if (!string.IsNullOrEmpty(cliente.NombreAsesor))
                    txtEjecutivoCliente.Text = HttpUtility.HtmlDecode(cliente.NombreAsesor);
                if (!string.IsNullOrEmpty(cliente.Estado))
                    txtEstadoCliente.Text = HttpUtility.HtmlDecode(cliente.Estado);
            }
            //VerAuditoria(programa.UsuarioCrea, programa.FechaCrea, programa.UsuarioEdita, programa.FechaEdita);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(clienteServicio.GetType().Name, "ObtenerDatosCliente", ex);
        }
    }

    protected void ObtenerDatosCredito(String pIdObjeto2)
    {
        try
        {
            Credito credito = new Credito();

            if (pIdObjeto2 != null)
            {
                credito.numero_radicacion = Int64.Parse(pIdObjeto2);
                credito = creditoServicio.ConsultarCreditoAsesor(credito.numero_radicacion, (Usuario)Session["usuario"]);

                if (!string.IsNullOrEmpty(credito.numero_radicacion.ToString()))
                    txtIdCredito.Text = HttpUtility.HtmlDecode(credito.numero_radicacion.ToString());
                if (!string.IsNullOrEmpty(credito.linea_credito))
                    txtLineaCredito.Text = HttpUtility.HtmlDecode(credito.linea_credito);
                if (!string.IsNullOrEmpty(credito.fecha_solicitud.ToString()))
                    txtFechaSolicitudCredito.Text = HttpUtility.HtmlDecode(credito.fecha_solicitud.ToString());
                if (!string.IsNullOrEmpty(credito.monto_aprobado.ToString()))
                    txtMontoCredito.Text = HttpUtility.HtmlDecode(credito.monto_aprobado.ToString());
                if (!string.IsNullOrEmpty(credito.plazo.ToString()))
                    txtPlazoCredito.Text = HttpUtility.HtmlDecode(credito.plazo.ToString());
                if (!string.IsNullOrEmpty(credito.NombreAsesor))
                    txtEjecutivoCredito.Text = HttpUtility.HtmlDecode(credito.NombreAsesor);
                if (!string.IsNullOrEmpty(credito.saldo_capital.ToString()))
                    txtSaldoCredito.Text = HttpUtility.HtmlDecode(credito.saldo_capital.ToString());
                if (!string.IsNullOrEmpty(credito.fecha_vencimiento.ToString()))
                    txtFechaTerminacionCredito.Text = HttpUtility.HtmlDecode(credito.fecha_vencimiento.ToString());
                if (!string.IsNullOrEmpty(credito.valor_cuota.ToString()))
                    txtCuotaCredito.Text = HttpUtility.HtmlDecode(credito.valor_cuota.ToString());
                if (!string.IsNullOrEmpty(credito.cuotas_pagadas.ToString()))
                    txtCuotasPagadasCredito.Text = HttpUtility.HtmlDecode(credito.cuotas_pagadas.ToString());
                if (!string.IsNullOrEmpty(credito.calificacion_promedio.ToString()))
                    txtCalificacionCredito.Text = HttpUtility.HtmlDecode(credito.calificacion_promedio.ToString());
                if (!string.IsNullOrEmpty(credito.fecha_ultimo_pago.ToString()))
                    txtFechaUltimoPagoCredito.Text = HttpUtility.HtmlDecode(credito.fecha_ultimo_pago.ToString());
                if (!string.IsNullOrEmpty(credito.ult_valor_pagado.ToString()))
                    txtUltimoValorPagadoCredito.Text = HttpUtility.HtmlDecode(credito.ult_valor_pagado.ToString());
                if (!string.IsNullOrEmpty(credito.fecha_prox_pago.ToString()))
                    txtFechaProximoPagoCredito.Text = HttpUtility.HtmlDecode(credito.fecha_prox_pago.ToString());
                if (!string.IsNullOrEmpty(credito.valor_a_pagar.ToString()))
                    txtValorPagarCredito.Text = HttpUtility.HtmlDecode(credito.valor_a_pagar.ToString());

                txtSaldoMoraCredito.Text = (credito.saldo_mora + credito.saldo_atributos_mora).ToString();

                if (!string.IsNullOrEmpty(credito.total_a_pagar.ToString()))
                    txtValorTotalPagarCredito.Text = HttpUtility.HtmlDecode(credito.total_a_pagar.ToString());
            }
            //VerAuditoria(programa.UsuarioCrea, programa.FechaCrea, programa.UsuarioEdita, programa.FechaEdita);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(clienteServicio.GetType().Name, "ObtenerDatosCredito", ex);
        }
    }

    protected void ObtenerDatosProceso(String pIdObjeto)
    {
        try
        {
            ProcesosCobro proceso = new ProcesosCobro();

            if (pIdObjeto != null)
            {
                proceso = procesosCobroServicio.ConsultarDatosProceso(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);

                if (!string.IsNullOrEmpty(proceso.codprocesocobro.ToString()))
                    codProceso = proceso.codprocesocobro;
                LlenarComboProcesos(codProceso);

                LlenarComboUsuarios();
                if (!string.IsNullOrEmpty(proceso.codusuario.ToString()))
                    codUsuario = proceso.codusuario;
                    ddlUsuario.SelectedValue=codUsuario.ToString();
                
                LlenarComboMotivos();
                if (!string.IsNullOrEmpty(proceso.codmotivo.ToString()))
                    codMotivo = proceso.codmotivo;
                    ddlMotivo.SelectedValue=codMotivo.ToString();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(procesosCobroServicio.GetType().Name, "ObtenerDatosProceso", ex);
        }
    }

    protected void LlenarComboProcesos(Int64 codProceso)
    {
        ProcesosCobro procesoCobro = new ProcesosCobro();
        procesoCobro.codprocesocobro = codProceso;
        ddlProceso.DataSource = procesosCobroServicio.ListarProcesosCobroSiguientes(procesoCobro, (Usuario)Session["usuario"]);
        ddlProceso.DataTextField = "descripcion";
        ddlProceso.DataValueField = "codprocesocobro";
        ddlProceso.DataBind();
        ddlProceso.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));
    }

    protected void LlenarComboUsuarios()
    {
        UsuarioAseService usuarioAseServicio = new UsuarioAseService();
        UsuarioAse usuarioAse = new UsuarioAse();
        ddlUsuario.DataSource = usuarioAseServicio.ListarUsuario(usuarioAse, (Usuario)Session["usuario"]);
        ddlUsuario.DataTextField = "nombre";
        ddlUsuario.DataValueField = "codusuario";
        ddlUsuario.DataBind();
        ddlUsuario.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));
    }

    protected void LlenarComboMotivos()
    {
        MotivosCambioService motivosServicio = new MotivosCambioService();
        MotivosCambio motivo = new MotivosCambio();
        ddlMotivo.DataSource = motivosServicio.ListarMotivosCambio(motivo, (Usuario)Session["usuario"]);
        ddlMotivo.DataTextField = "descripcion";
        ddlMotivo.DataValueField = "cod_motivo_cambio";
        ddlMotivo.DataBind();
        ddlMotivo.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));
    }
}