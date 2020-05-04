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
using Xpinn.ActivosFijos.Entities;
using Xpinn.ActivosFijos.Services;
using System.Text;
using System.IO;
using System.Web.UI.HtmlControls;

partial class Nuevo : GlobalWeb
{
    private Xpinn.ActivosFijos.Services.ActivosFijoservices activosServicio = new Xpinn.ActivosFijos.Services.ActivosFijoservices();


    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[activosServicio.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(activosServicio.CodigoPrograma, "E");
            else
                VisualizarOpciones(activosServicio.CodigoPrograma, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
            toolBar.MostrarConsultar(false);
            toolBar.eventoConsultar += btnConsultar_Click;
            ctlMensaje.eventoClick += btnContinuarMen_Click;
            ctlproceso.eventoCancelarClick += btnCancelarProceso_Click;
            ctlproceso.eventoClick += btnAceptarProceso_Click;
            panelProceso.Visible = false;

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(activosServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                mvActivosFijos.ActiveViewIndex = 0;
                txtCodigo.Enabled = false;
                CargarListas();
                txtFechaDepreciacion.Text = DateTime.Today.ToString("dd/MM/yyyy");
                txtFechaCompra.Text = DateTime.Today.ToString("dd/MM/yyyy");
                if (Session[activosServicio.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[activosServicio.CodigoPrograma + ".id"].ToString();
                    Session.Remove(activosServicio.CodigoPrograma + ".id");
                    ObtenerDatos(idObjeto);
                    txtAcumuladoDepreciacion.Visible = true;
                    txtAcumuladoDepreciacion.Enabled = false;
                    lblacumulado.Visible = true;
                }
                else
                {
                    // ddlUsoBien.SelectedValue = "2";
                    txtCodigo.Text = activosServicio.ObtenerSiguienteCodigo((Usuario)Session["Usuario"]).ToString();
                    txtEstado.Text = "ACTIVO";
                    txtAcumuladoDepreciacion.Visible = false;
                    lblacumulado.Visible = false;
                }
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(activosServicio.CodigoPrograma, "Page_Load", ex);
        }
    }

    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }


    protected Boolean validarDatos()
    {
        VerError("");
        if (txtNombre.Text == "")
        {
            VerError("Debe ingresar el nombre del Activo Fijo. (Datos Principales)");
            txtNombre.Focus();
            return false;
        }
        if (ddlClase.SelectedItem == null)
        {
            VerError("No existen clases creadas, Verifique los datos. (Datos Principales)");
            ddlClase.Focus();
            return false;
        }
        if (ddlClase.SelectedIndex == 0)
        {
            VerError("Seleccione la clase, Verifique los datos. (Datos Principales)");
            ddlClase.Focus();
            return false;
        }
        if (ddlTipo.SelectedItem == null)
        {
            VerError("No existen tipos creados, Verifique los datos. (Datos Principales)");
            ddlTipo.Focus();
            return false;
        }
        if (ddlTipo.SelectedIndex == 0)
        {
            VerError("Seleccione el Tipo, Verifique los datos. (Datos Principales)");
            ddlTipo.Focus();
            return false;
        }
        if (ddlUbicacion.SelectedItem == null)
        {
            VerError("No existen ubicaciones creados, Verifique los datos. (Datos Principales)");
            ddlUbicacion.Focus();
            return false;
        }
        if (ddlUbicacion.SelectedIndex == 0)
        {
            VerError("Seleccione la ubicación, Verifique los datos. (Datos Principales)");
            ddlUbicacion.Focus();
            return false;
        }
        if (ddlCentroCosto.SelectedItem == null)
        {
            VerError("No existen centros de costos creados, Verifique los datos. (Datos Principales)");
            ddlCentroCosto.Focus();
            return false;
        }
        if (ddlCentroCosto.SelectedIndex == 0)
        {
            VerError("Seleccione el centro de costo, Verifique los datos. (Datos Principales)");
            ddlCentroCosto.Focus();
            return false;
        }
        if (ddlOficina.SelectedItem == null)
        {
            VerError("No existen oficinas creadas, Verifique los datos. (Datos Principales)");
            ddlOficina.Focus();
            return false;
        }
        if (ddlOficina.SelectedIndex == 0)
        {
            VerError("Seleccione una oficina, Verifique los datos. (Datos Principales)");
            ddlOficina.Focus();
            return false;
        }
        if (txtCodEncargado.Text == "")
        {
            VerError("Seleccione el nombre del encargado, Verifique los datos. (Datos Principales)");
            txtNomEncargado.Focus();
            return false;
        }
        if (txtNomEncargado.Text != "" && txtCodEncargado.Text == "")
        {
            VerError("Seleccione un encargado de forma correcta, Verifique los datos. (Datos Principales)");
            txtNomEncargado.Focus();
            return false;
        }
        if (txtVidaUtil.Text == "")
        {
            VerError("Ingrese los años de vida util, Verifique los datos. (Datos Principales)");
            txtVidaUtil.Focus();
            return false;
        }
        if (txtValorAvaluo.Text == "")
        {
            VerError("Ingrese el valor de Avaluo, Verifique los datos. (Datos Principales)");
            txtValorAvaluo.Focus();
            return false;
        }
        if (txtValorSalvamento.Text == "")
        {
            VerError("Ingrese el valor de salvamento, Verifique los datos. (Datos Principales)");
            txtValorSalvamento.Focus();
            return false;
        }
        if (ddlClasificacion.SelectedItem == null)
        {
            VerError("No existen Clasificaciones Niif creadas, Verifique los datos. (Datos NIIF)");
            ddlClasificacion.Focus();
            return false;
        }
        if (ddlClasificacion.SelectedIndex == 0)
        {
            VerError("Seleccione una Clasificaciones Niif, Verifique los datos. (Datos NIIF)");
            ddlClasificacion.Focus();
            return false;
        }
        if (ddltipo_Activo.SelectedItem == null)
        {
            VerError("No existen Tipos de activos Niif creadas, Verifique los datos. (Datos NIIF)");
            ddltipo_Activo.Focus();
            return false;
        }
        if (ddltipo_Activo.SelectedIndex == 0)
        {
            VerError("Seleccione un tipo de activo Niif, Verifique los datos. (Datos NIIF)");
            ddltipo_Activo.Focus();
            return false;
        }
        if (ddlmetodo_costo.SelectedItem == null)
        {
            VerError("No existen metodos de costos Niif creadas, Verifique los datos. (Datos NIIF)");
            ddlmetodo_costo.Focus();
            return false;
        }
        if (ddlmetodo_costo.SelectedIndex == 0)
        {
            VerError("Seleccione un metodos de costos Niif, Verifique los datos. (Datos NIIF)");
            ddlmetodo_costo.Focus();
            return false;
        }
        if (txtValorNiif.Text == "")
        {
            VerError("Ingrese el valor de activo Niif, Verifique los datos. (Datos NIIF)");
            txtValorNiif.Focus();
            return false;
        }
        if (txtvidaNiif.Text == "")
        {
            VerError("Ingrese los años de vida util Niif, Verifique los datos. (Datos NIIF)");
            txtvidaNiif.Focus();
            return false;
        }
        if (txtVrResidualNiif.Text == "")
        {
            VerError("Ingrese el valor residual Niif, Verifique los datos. (Datos NIIF)");
            txtVrResidualNiif.Focus();
            return false;
        }
        if (txtVrResidualPor.Text == "")
        {
            VerError("Ingrese el porcentaje del valor residual, Verifique los datos. (Datos NIIF)");
            txtVrResidualPor.Focus();
            return false;
        }
        if (ddlUniGenerado.SelectedIndex == 0)
        {
            VerError("Seleccione una unidad generadora de efectivo, Verifique los datos. (Datos NIIF)");
            ddlmetodo_costo.Focus();
            return false;
        }
        if (txtFactura.Text == "")
        {
            VerError("Ingrese el número de la factura, Verifique los datos. (Datos de Compra)");
            txtFactura.Focus();
            return false;
        }
        if (txtCodProveedor.Text == "")
        {
            VerError("Seleccione el nombre del proveedor, Verifique los datos. (Datos de Compra)");
            txtNomProveedor.Focus();
            return false;
        }
        if (txtNomProveedor.Text != "" && txtCodProveedor.Text == "")
        {
            VerError("Seleccione un proveedor de forma correcta, Verifique los datos. (Datos de Compra)");
            txtNomProveedor.Focus();
            return false;
        }
        if (txtValorCompra.Text == "" || txtValorCompra.Text == "0")
        {
            VerError("Ingrese el valor de compra del activo, Verifique los datos. (Datos de Compra)");
            ddlEstadoPol.Focus();
            return false;
        }
        if (txtFechaCompra.Text == "")
        {
            VerError("Ingrese la fecha de compra del activo, Verifique los datos. (Datos de Compra)");
            txtFechaCompra.Focus();
            return false;
        }
        if (ddlmoneda.SelectedItem == null)
        {
            VerError("No existen registradas tipos de monedas, Verifique los datos. (Datos de Compra)");
            ddlmoneda.Focus();
            return false;
        }
        if (ddlmoneda.SelectedIndex == 0)
        {
            VerError("Seleccione el tipo de moneda, Verifique los datos. (Datos de Compra)");
            ddlmoneda.Focus();
            return false;
        }
        if (ddlEstadoPol.SelectedIndex == 0)
        {
            VerError("Seleccione un estado, Verifique los datos. (Datos Adicionales - Póliza de seguro)");
            ddlEstadoPol.Focus();
            return false;
        }
        /*
        if (ddlPeriodicidad.SelectedIndex == 0)
        {
            VerError("Seleccione una periodicidad, Verifique los datos. (Datos Adicionales - Leasing)");
            ddlPeriodicidad.Focus();
            return false;
        }
        */
        return true;
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        VerError("");
        if (validarDatos())
            ctlMensaje.MostrarMensaje("Desea guardar los datos del activo fijo?");
    }

    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        // Determinar el código del encargado

        // Guardar los datos
        try
        {
            Usuario vUsuario = new Usuario();
            vUsuario = (Usuario)Session["Usuario"];

            Xpinn.ActivosFijos.Entities.ActivoFijo vActivoFijo = new Xpinn.ActivosFijos.Entities.ActivoFijo();

            if (idObjeto != "")
                vActivoFijo = activosServicio.ConsultarActivoFijo(Convert.ToInt64(idObjeto), (Usuario)Session["usuario"]);
            else
                txtCodigo.Text = activosServicio.ObtenerSiguienteCodigo((Usuario)Session["Usuario"]).ToString();

            if (lblConsecutivo.Text != "")
                vActivoFijo.consecutivo = Convert.ToInt64(lblConsecutivo.Text);
            else
                vActivoFijo.consecutivo = 0;

            if (txtCodigo.Text.Trim() != "")
                vActivoFijo.cod_act = Convert.ToInt64(txtCodigo.Text.Trim());
            else
                vActivoFijo.cod_act = 0;
            vActivoFijo.clase = Convert.ToInt32(ddlClase.SelectedValue);
            vActivoFijo.tipo = Convert.ToInt32(ddlTipo.SelectedValue);
            vActivoFijo.cod_ubica = Convert.ToInt32(ddlUbicacion.SelectedValue);
            vActivoFijo.cod_costo = Convert.ToInt32(ddlCentroCosto.SelectedValue);
            vActivoFijo.nombre = txtNombre.Text;
            if (txtVidaUtil.Text.Trim() != "")
                vActivoFijo.anos_util = Convert.ToDouble(ConvertirStringToDecimal(txtVidaUtil.Text));
            else
                vActivoFijo.anos_util = 0;
            if (lblEstado.Text.Trim() != "")
                vActivoFijo.estado = Convert.ToInt32(lblEstado.Text);
            else
                vActivoFijo.estado = 1;
            vActivoFijo.serial = txtSerial.Text;
            if (txtCodEncargado.Text != null && txtCodEncargado.Text != "")
                vActivoFijo.cod_encargado = Convert.ToInt32(txtCodEncargado.Text);
            else
                vActivoFijo.cod_encargado = 0;
            if (txtCodProveedor.Text != null && txtCodProveedor.Text != "")
                vActivoFijo.cod_proveedor = Convert.ToInt32(txtCodProveedor.Text);
            else
                vActivoFijo.cod_proveedor = 0;
            vActivoFijo.fecha_compra = txtFechaCompra.ToDateTime;
            if (txtValorCompra.Text.Trim() != "")
                vActivoFijo.valor_compra = Convert.ToDecimal(txtValorCompra.Text);
            else
                vActivoFijo.valor_compra = 0;
            if (txtValorAvaluo.Text.Trim() != "")
                vActivoFijo.valor_avaluo = Convert.ToDecimal(txtValorAvaluo.Text);
            else
                vActivoFijo.valor_avaluo = 0;
            if (txtValorSalvamento.Text.Trim() != "")
                vActivoFijo.valor_salvamen = Convert.ToDecimal(txtValorSalvamento.Text);
            else
                vActivoFijo.valor_salvamen = 0;
            if (txtFactura.Text.Trim() != "")
                vActivoFijo.num_factura = Convert.ToDecimal(txtFactura.Text);
            else
                vActivoFijo.num_factura = 0;
            if (txtAcumuladoDepreciacion.Text.Trim() != "")
                vActivoFijo.acumulado_depreciacion = Convert.ToDecimal(txtAcumuladoDepreciacion.Text);
            else
                vActivoFijo.acumulado_depreciacion = 0;
            if (txtFechaDepreciacion.TieneDatos)
                vActivoFijo.fecha_ult_depre = txtFechaDepreciacion.ToDateTime;
            vActivoFijo.observaciones = txtObservacion.Text;
            vActivoFijo.cod_oficina = Convert.ToInt32(ddlOficina.SelectedValue);

            vActivoFijo.fechacreacion = DateTime.Now;
            vActivoFijo.usuariocreacion = vUsuario.identificacion;
            vActivoFijo.fecultmod = DateTime.Now;
            vActivoFijo.usuultmod = vUsuario.identificacion;

            vActivoFijo.marca = txtMarca.Text;
            vActivoFijo.modelo = txtModelo.Text;
            vActivoFijo.num_motor = txtMotor.Text;
            vActivoFijo.placa = txtPlaca.Text;

            vActivoFijo.matricula = txtMatricula.Text;
            vActivoFijo.escritura = txtEscritura.Text;
            vActivoFijo.notaria = txtNotaria.Text;
            vActivoFijo.direccion = txtDireccion.Text;

            if (txtNumeroPol.Text != "")
                vActivoFijo.num_poliza = Convert.ToInt64(txtNumeroPol.Text);
            else
                vActivoFijo.num_poliza = 0;
            vActivoFijo.asegurador = txtNombrePol.Text;
            if (txtValorPol.Text != "")
                vActivoFijo.valor = Convert.ToDecimal(txtValorPol.Text);
            if (txtFechaPoliza.TieneDatos)
                vActivoFijo.fecha_poliza = txtFechaPoliza.ToDateTime;
            if (txtFechaVigencia.TieneDatos)
                vActivoFijo.fecha_vigencia = txtFechaVigencia.ToDateTime;
            if (txtFechaGarantia.TieneDatos)
                vActivoFijo.fecha_garantia = txtFechaGarantia.ToDateTime;
            if (ddlEstadoPol.SelectedValue != "")
                vActivoFijo.estadop = Convert.ToInt64(ddlEstadoPol.SelectedValue);

            //Agregado Crear NIIF
            if (ddlClasificacion.SelectedValue != "")
                vActivoFijo.codclasificacion_nif = Convert.ToInt32(ddlClasificacion.SelectedValue);
            if (ddltipo_Activo.SelectedValue != "")
                vActivoFijo.tipo_activo_nif = Convert.ToInt32(ddltipo_Activo.SelectedValue);
            if (ddlmetodo_costo.SelectedValue != "")
                vActivoFijo.metodo_costeo_nif = Convert.ToInt32(ddlmetodo_costo.SelectedValue);
            if (txtValorNiif.Text != "")
                vActivoFijo.valor_activo_nif = Convert.ToDecimal(txtValorNiif.Text);
            if (txtvidaNiif.Text != "")
                vActivoFijo.vida_util_nif = Convert.ToDecimal(txtvidaNiif.Text);
            if (txtVrResidualNiif.Text != "")
                vActivoFijo.valor_residual_nif = Convert.ToDecimal(txtVrResidualNiif.Text);
            if (txtVrResidualPor.Text != "")
                vActivoFijo.porcentaje_residual_nif = Convert.ToDecimal(txtVrResidualPor.Text);
            if (ddlUniGenerado.SelectedValue != "")
                vActivoFijo.unigeneradora_nif = Convert.ToInt32(ddlUniGenerado.SelectedValue);
            if (txtadicionesNiif.Text != "")
                vActivoFijo.adiciones_nif = Convert.ToDecimal(txtadicionesNiif.Text);
            if (txtvrDeterioro.Text != "")
                vActivoFijo.vrdeterioro_nif = Convert.ToDecimal(txtvrDeterioro.Text);
            if (txtrecdeterioroNiif.Text != "")
                vActivoFijo.vrrecdeterioro_nif = Convert.ToDecimal(txtrecdeterioroNiif.Text);
            if (txtrevaluacionNiif.Text != "")
                vActivoFijo.revaluacion_nif = Convert.ToDecimal(txtrevaluacionNiif.Text);
            if (txtrevRevaluacion.Text != "")
                vActivoFijo.revrevaluacion_nif = Convert.ToDecimal(txtrevRevaluacion.Text);
            if (ddlmoneda.SelectedValue != "")
                vActivoFijo.cod_moneda = Convert.ToInt32(ddlmoneda.SelectedValue);

            // Datos del leasing
            if (txtNumPagos.Text != "")
                vActivoFijo.numero_cuotas = Convert.ToInt32(txtNumPagos.Text);
            if (txtvalorLeasing.Text != "")
                vActivoFijo.valor_cuota = Convert.ToDecimal(txtvalorLeasing.Text);
            if (ddlPeriodicidad.SelectedValue != "")
                vActivoFijo.cod_periodicidad = Convert.ToInt32(ddlPeriodicidad.SelectedValue);
            if (txtValorCompLeasing.Text != "")
                vActivoFijo.opcion_compra = Convert.ToDecimal(txtValorCompLeasing.Text);

            if (ddlUsoBien.SelectedValue != "")
                vActivoFijo.uso_del_bien = Convert.ToInt32(ddlUsoBien.SelectedValue);

            if (idObjeto != "")
            {
                if (!string.IsNullOrEmpty(txtFec_ult_adicion.Text.Trim()))
                    vActivoFijo.fecha_ult_adicion = Convert.ToDateTime(txtFec_ult_adicion.Text);
                vActivoFijo.consecutivo = Convert.ToInt64(idObjeto);
                activosServicio.ModificarActivoFijo(vActivoFijo, (Usuario)Session["usuario"]);
            }
            else
            {
                vActivoFijo.saldo_por_depreciar = Convert.ToDecimal(txtValorCompra.Text);
                vActivoFijo.acumulado_depreciacion = Convert.ToDecimal(0);


                vActivoFijo.fecha_ult_adicion = DateTime.Now;
                vActivoFijo = activosServicio.CrearActivoFijo(vActivoFijo, (Usuario)Session["usuario"]);
                idObjeto = Convert.ToString(vActivoFijo.consecutivo);
            }

            Session[activosServicio.CodigoPrograma + ".id"] = idObjeto;
            //Navegar(Pagina.Lista);
            Site toolBar = (Site)this.Master;
            toolBar.MostrarGuardar(false);
            toolBar.MostrarCancelar(false);
            toolBar.MostrarConsultar(true);
            toolBar.eventoConsultar += btnConsultar_Click;

            mvActivosFijos.ActiveViewIndex = 1;

        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(activosServicio.CodigoPrograma, "btnGuardar_Click", ex);
        }
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }

    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            Xpinn.ActivosFijos.Entities.ActivoFijo vActivoFijo = new Xpinn.ActivosFijos.Entities.ActivoFijo();
            vActivoFijo = activosServicio.ConsultarActivoFijo(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);

            lblConsecutivo.Text = HttpUtility.HtmlDecode(vActivoFijo.consecutivo.ToString().Trim());
            txtCodigo.Text = HttpUtility.HtmlDecode(vActivoFijo.cod_act.ToString().Trim());
            if (!string.IsNullOrEmpty(vActivoFijo.clase.ToString()))
                ddlClase.SelectedValue = HttpUtility.HtmlDecode(vActivoFijo.clase.ToString().Trim());
            if (!string.IsNullOrEmpty(vActivoFijo.tipo.ToString()))
                ddlTipo.SelectedValue = HttpUtility.HtmlDecode(vActivoFijo.tipo.ToString().Trim());
            if (!string.IsNullOrEmpty(vActivoFijo.cod_ubica.ToString()))
                ddlUbicacion.SelectedValue = HttpUtility.HtmlDecode(vActivoFijo.cod_ubica.ToString().Trim());
            if (!string.IsNullOrEmpty(vActivoFijo.cod_costo.ToString()))
                ddlCentroCosto.SelectedValue = HttpUtility.HtmlDecode(vActivoFijo.cod_costo.ToString().Trim());
            if (!string.IsNullOrEmpty(vActivoFijo.nombre))
                txtNombre.Text = HttpUtility.HtmlDecode(vActivoFijo.nombre.ToString());
            if (vActivoFijo.anos_util.ToString() != "")
                txtVidaUtil.Text = HttpUtility.HtmlDecode(vActivoFijo.anos_util.ToString().Trim());
            if (!string.IsNullOrEmpty(vActivoFijo.estado.ToString()))
                lblEstado.Text = HttpUtility.HtmlDecode(vActivoFijo.estado.ToString().Trim());
            if (!string.IsNullOrEmpty(vActivoFijo.nomestado))
                txtEstado.Text = HttpUtility.HtmlDecode(vActivoFijo.nomestado);
            if (!string.IsNullOrEmpty(vActivoFijo.serial))
                txtSerial.Text = HttpUtility.HtmlDecode(vActivoFijo.serial);
            if (!string.IsNullOrEmpty(vActivoFijo.cod_encargado.ToString()))
            {
                Xpinn.FabricaCreditos.Entities.Persona1 persona1 = new Xpinn.FabricaCreditos.Entities.Persona1();
                txtCodEncargado.Text = HttpUtility.HtmlDecode(vActivoFijo.cod_encargado.ToString().Trim());
                persona1 = DatosPersona(vActivoFijo.cod_encargado);
                txtIdenEncargado.Text = persona1.identificacion;
                txtNomEncargado.Text = persona1.nombre;
            }
            if (!string.IsNullOrEmpty(vActivoFijo.cod_proveedor.ToString()))
            {
                Xpinn.FabricaCreditos.Entities.Persona1 persona1 = new Xpinn.FabricaCreditos.Entities.Persona1();
                txtCodProveedor.Text = HttpUtility.HtmlDecode(vActivoFijo.cod_proveedor.ToString().Trim());
                persona1 = DatosPersona(vActivoFijo.cod_proveedor);
                txtIdeProveedor.Text = persona1.identificacion;
                txtNomProveedor.Text = persona1.nombre;
            }
            if (!string.IsNullOrEmpty(vActivoFijo.fecha_compra.ToString()))
                txtFechaCompra.ToDateTime = Convert.ToDateTime(HttpUtility.HtmlDecode(vActivoFijo.fecha_compra.ToString()));
            if (!string.IsNullOrEmpty(vActivoFijo.fecha_ult_depre.ToString()))
                txtFechaDepreciacion.ToDateTime = Convert.ToDateTime(HttpUtility.HtmlDecode(vActivoFijo.fecha_ult_depre.ToString()));
            if (!string.IsNullOrEmpty(vActivoFijo.valor_compra.ToString()))
                txtValorCompra.Text = HttpUtility.HtmlDecode(vActivoFijo.valor_compra.ToString());
            if (!string.IsNullOrEmpty(vActivoFijo.valor_avaluo.ToString()))
                txtValorAvaluo.Text = HttpUtility.HtmlDecode(vActivoFijo.valor_avaluo.ToString());
            if (!string.IsNullOrEmpty(vActivoFijo.valor_salvamen.ToString()))
                txtValorSalvamento.Text = HttpUtility.HtmlDecode(vActivoFijo.valor_salvamen.ToString());
            if (!string.IsNullOrEmpty(vActivoFijo.num_factura.ToString()))
                txtFactura.Text = HttpUtility.HtmlDecode(vActivoFijo.num_factura.ToString().Trim());
            if (!string.IsNullOrEmpty(vActivoFijo.observaciones))
                txtObservacion.Text = HttpUtility.HtmlDecode(vActivoFijo.observaciones);
            if (!string.IsNullOrEmpty(vActivoFijo.cod_oficina.ToString()))
                ddlOficina.SelectedValue = HttpUtility.HtmlDecode(vActivoFijo.cod_oficina.ToString().Trim());
            if (!string.IsNullOrEmpty(vActivoFijo.acumulado_depreciacion.ToString()))
                txtAcumuladoDepreciacion.Text = HttpUtility.HtmlDecode(vActivoFijo.acumulado_depreciacion.ToString());

            if (vActivoFijo.marca != null)
                if (!string.IsNullOrEmpty(vActivoFijo.marca.ToString()))
                    txtMarca.Text = HttpUtility.HtmlDecode(vActivoFijo.marca.ToString());
            if (vActivoFijo.modelo != null)
                if (!string.IsNullOrEmpty(vActivoFijo.modelo.ToString()))
                    txtModelo.Text = HttpUtility.HtmlDecode(vActivoFijo.modelo.ToString());
            if (vActivoFijo.num_motor != null)
                if (!string.IsNullOrEmpty(vActivoFijo.num_motor.ToString()))
                    txtMotor.Text = HttpUtility.HtmlDecode(vActivoFijo.num_motor.ToString());
            if (vActivoFijo.placa != null)
                if (!string.IsNullOrEmpty(vActivoFijo.placa.ToString()))
                    txtPlaca.Text = HttpUtility.HtmlDecode(vActivoFijo.placa.ToString());

            if (vActivoFijo.matricula != null)
                if (!string.IsNullOrEmpty(vActivoFijo.matricula.ToString()))
                    txtMatricula.Text = HttpUtility.HtmlDecode(vActivoFijo.matricula.ToString());
            if (vActivoFijo.escritura != null)
                if (!string.IsNullOrEmpty(vActivoFijo.escritura.ToString()))
                    txtEscritura.Text = HttpUtility.HtmlDecode(vActivoFijo.escritura.ToString());
            if (vActivoFijo.notaria != null)
                if (!string.IsNullOrEmpty(vActivoFijo.notaria.ToString()))
                    txtNotaria.Text = HttpUtility.HtmlDecode(vActivoFijo.notaria.ToString());
            if (vActivoFijo.direccion != null)
                if (!string.IsNullOrEmpty(vActivoFijo.direccion.ToString()))
                    txtDireccion.Text = HttpUtility.HtmlDecode(vActivoFijo.direccion.ToString());

            if (vActivoFijo.num_poliza != null)
                if (!string.IsNullOrEmpty(vActivoFijo.num_poliza.ToString()))
                    txtNumeroPol.Text = HttpUtility.HtmlDecode(vActivoFijo.num_poliza.ToString());
            if (vActivoFijo.asegurador != null)
                if (!string.IsNullOrEmpty(vActivoFijo.asegurador.ToString()))
                    txtNombrePol.Text = HttpUtility.HtmlDecode(vActivoFijo.asegurador.ToString());
            if (vActivoFijo.valor != null)
                if (!string.IsNullOrEmpty(vActivoFijo.valor.ToString()))
                    txtValorPol.Text = HttpUtility.HtmlDecode(vActivoFijo.valor.ToString());
            if (vActivoFijo.fecha_poliza != null)
                if (!string.IsNullOrEmpty(vActivoFijo.fecha_poliza.ToString()))
                    txtFechaPoliza.ToDateTime = Convert.ToDateTime(HttpUtility.HtmlDecode(vActivoFijo.fecha_poliza.ToString()));
            if (vActivoFijo.fecha_vigencia != null)
                if (!string.IsNullOrEmpty(vActivoFijo.fecha_vigencia.ToString()))
                    txtFechaVigencia.ToDateTime = Convert.ToDateTime(HttpUtility.HtmlDecode(vActivoFijo.fecha_vigencia.ToString()));
            if (vActivoFijo.fecha_garantia != null)
                if (!string.IsNullOrEmpty(vActivoFijo.fecha_garantia.ToString()))
                    txtFechaGarantia.ToDateTime = Convert.ToDateTime(HttpUtility.HtmlDecode(vActivoFijo.fecha_garantia.ToString()));
            if (vActivoFijo.estadop != null)
                if (!string.IsNullOrEmpty(vActivoFijo.estadop.ToString()))
                    ddlEstadoPol.SelectedValue = HttpUtility.HtmlDecode(vActivoFijo.estadop.ToString());

            //AGREGADO NIIF 
            if (vActivoFijo.codclasificacion_nif != null)
                if (!string.IsNullOrEmpty(vActivoFijo.codclasificacion_nif.ToString()))
                    ddlClasificacion.SelectedValue = HttpUtility.HtmlDecode(vActivoFijo.codclasificacion_nif.ToString());
            if (vActivoFijo.tipo_activo_nif != null)
                if (!string.IsNullOrEmpty(vActivoFijo.tipo_activo_nif.ToString()))
                    ddltipo_Activo.SelectedValue = HttpUtility.HtmlDecode(vActivoFijo.tipo_activo_nif.ToString());
            if (vActivoFijo.metodo_costeo_nif != null)
                if (!string.IsNullOrEmpty(vActivoFijo.metodo_costeo_nif.ToString()))
                    ddlmetodo_costo.SelectedValue = HttpUtility.HtmlDecode(vActivoFijo.metodo_costeo_nif.ToString());
            if (vActivoFijo.valor_activo_nif != null)
                if (!string.IsNullOrEmpty(vActivoFijo.valor_activo_nif.ToString()))
                    txtValorNiif.Text = HttpUtility.HtmlDecode(vActivoFijo.valor_activo_nif.ToString());
            if (vActivoFijo.vida_util_nif != null)
                if (!string.IsNullOrEmpty(vActivoFijo.vida_util_nif.ToString()))
                    txtvidaNiif.Text = HttpUtility.HtmlDecode(vActivoFijo.vida_util_nif.ToString());
            if (vActivoFijo.valor_residual_nif != null)
                if (!string.IsNullOrEmpty(vActivoFijo.valor_residual_nif.ToString()))
                    txtVrResidualNiif.Text = HttpUtility.HtmlDecode(vActivoFijo.valor_residual_nif.ToString());
            if (vActivoFijo.porcentaje_residual_nif != null)
                if (!string.IsNullOrEmpty(vActivoFijo.porcentaje_residual_nif.ToString()))
                    txtVrResidualPor.Text = HttpUtility.HtmlDecode(vActivoFijo.porcentaje_residual_nif.ToString());
            if (vActivoFijo.unigeneradora_nif != null)
                if (!string.IsNullOrEmpty(vActivoFijo.unigeneradora_nif.ToString()))
                    ddlUniGenerado.SelectedValue = HttpUtility.HtmlDecode(vActivoFijo.unigeneradora_nif.ToString());
            if (vActivoFijo.adiciones_nif != null)
                if (!string.IsNullOrEmpty(vActivoFijo.adiciones_nif.ToString()))
                    txtadicionesNiif.Text = HttpUtility.HtmlDecode(vActivoFijo.adiciones_nif.ToString());
            if (vActivoFijo.vrdeterioro_nif != null)
                if (!string.IsNullOrEmpty(vActivoFijo.vrdeterioro_nif.ToString()))
                    txtvrDeterioro.Text = HttpUtility.HtmlDecode(vActivoFijo.vrdeterioro_nif.ToString());
            if (vActivoFijo.vrrecdeterioro_nif != null)
                if (!string.IsNullOrEmpty(vActivoFijo.vrrecdeterioro_nif.ToString()))
                    txtrecdeterioroNiif.Text = HttpUtility.HtmlDecode(vActivoFijo.vrrecdeterioro_nif.ToString());
            if (vActivoFijo.revaluacion_nif != null)
                if (!string.IsNullOrEmpty(vActivoFijo.revaluacion_nif.ToString()))
                    txtrevaluacionNiif.Text = HttpUtility.HtmlDecode(vActivoFijo.revaluacion_nif.ToString());
            if (vActivoFijo.revrevaluacion_nif != null)
                if (!string.IsNullOrEmpty(vActivoFijo.revrevaluacion_nif.ToString()))
                    txtrevRevaluacion.Text = HttpUtility.HtmlDecode(vActivoFijo.revrevaluacion_nif.ToString());
            if (vActivoFijo.cod_moneda != null)
                if (!string.IsNullOrEmpty(vActivoFijo.cod_moneda.ToString()))
                    ddlmoneda.SelectedValue = HttpUtility.HtmlDecode(vActivoFijo.cod_moneda.ToString());
            if (vActivoFijo.fecha_ult_adicion != null)
                txtFec_ult_adicion.Text = vActivoFijo.fecha_ult_adicion.ToString();

            //Agregar LEASING
            if (vActivoFijo.numero_cuotas != null)
                if (!string.IsNullOrEmpty(vActivoFijo.numero_cuotas.ToString()))
                    txtNumPagos.Text = HttpUtility.HtmlDecode(vActivoFijo.numero_cuotas.ToString());
            if (vActivoFijo.valor_cuota != null)
                if (!string.IsNullOrEmpty(vActivoFijo.valor_cuota.ToString()))
                    txtvalorLeasing.Text = HttpUtility.HtmlDecode(vActivoFijo.valor_cuota.ToString());
            if (vActivoFijo.cod_periodicidad != null)
                if (!string.IsNullOrEmpty(vActivoFijo.cod_periodicidad.ToString()))
                    ddlPeriodicidad.SelectedValue = HttpUtility.HtmlDecode(vActivoFijo.cod_periodicidad.ToString());
            if (vActivoFijo.opcion_compra != null)
                if (!string.IsNullOrEmpty(vActivoFijo.opcion_compra.ToString()))
                    txtValorCompLeasing.Text = HttpUtility.HtmlDecode(vActivoFijo.opcion_compra.ToString());

            if (!string.IsNullOrEmpty(vActivoFijo.uso_del_bien.ToString()))
                ddlUsoBien.SelectedValue = HttpUtility.HtmlDecode(vActivoFijo.uso_del_bien.ToString());


        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(activosServicio.CodigoPrograma, "ObtenerDatos", ex);
        }
    }


    private List<Xpinn.FabricaCreditos.Entities.Persona1> TraerResultadosLista(string pListaSolicitada)
    {
        Xpinn.FabricaCreditos.Services.Persona1Service DatosClienteServicio = new Xpinn.FabricaCreditos.Services.Persona1Service();
        List<Xpinn.FabricaCreditos.Entities.Persona1> lstDatosSolicitud = new List<Xpinn.FabricaCreditos.Entities.Persona1>();
        lstDatosSolicitud.Clear();
        lstDatosSolicitud = DatosClienteServicio.ListasDesplegables(pListaSolicitada, (Usuario)Session["Usuario"]);
        return lstDatosSolicitud;
    }


    private void CargarListas()
    {
        Usuario pUsuario = new Usuario();
        pUsuario = (Usuario)Session["Usuario"];
        try
        {
            ClaseActivoservices claseServicio = new ClaseActivoservices();
            ClaseActivo claseAct = new ClaseActivo();
            ddlClase.DataTextField = "nombre";
            ddlClase.DataValueField = "clase";
            ddlClase.DataSource = claseServicio.ListarClaseActivo(claseAct, pUsuario);
            ddlClase.DataBind();

            TipoActivoservices tipoServicio = new TipoActivoservices();
            TipoActivo tipoAct = new TipoActivo();
            ddlTipo.DataTextField = "nombre";
            ddlTipo.DataValueField = "tipo";
            ddlTipo.DataSource = tipoServicio.ListarTipoActivo(tipoAct, pUsuario);
            ddlTipo.DataBind();

            UbicacionActivoservices ubicacionServicio = new UbicacionActivoservices();
            UbicacionActivo ubicacionAct = new UbicacionActivo();
            ddlUbicacion.DataTextField = "nombre";
            ddlUbicacion.DataValueField = "cod_ubica";
            ddlUbicacion.DataSource = ubicacionServicio.ListarUbicacionActivo(ubicacionAct, pUsuario);
            ddlUbicacion.DataBind();

            Xpinn.Contabilidad.Services.CentroCostoService centroServicio = new Xpinn.Contabilidad.Services.CentroCostoService();
            Xpinn.Contabilidad.Entities.CentroCosto centroCosto = new Xpinn.Contabilidad.Entities.CentroCosto();
            ddlCentroCosto.DataTextField = "nom_centro";
            ddlCentroCosto.DataValueField = "centro_costo";
            ddlCentroCosto.DataSource = centroServicio.ListarCentroCosto(centroCosto, pUsuario);
            ddlCentroCosto.DataBind();

            Xpinn.Caja.Services.OficinaService oficinaServicio = new Xpinn.Caja.Services.OficinaService();
            Xpinn.Caja.Entities.Oficina oficina = new Xpinn.Caja.Entities.Oficina();
            ddlOficina.DataTextField = "nombre";
            ddlOficina.DataValueField = "cod_oficina";
            ddlOficina.DataSource = oficinaServicio.ListarOficina(oficina, pUsuario);
            ddlOficina.DataBind();

            //AGREGADO NIIF
            PoblarLista("clasificacion_activo_nif", ddlClasificacion);

            //ClaseActivoservices Clasificacion = new ClaseActivoservices();
            //ClaseActivo Clasifi = new ClaseActivo();
            //ddlClasificacion.DataSource = Clasificacion.ListarClasificacion(Clasifi, pUsuario);
            //ddlClasificacion.DataTextField = "DESCRIPCION";
            //ddlClasificacion.DataValueField = "CODCLASIFICACION_NIF";
            //ddlClasificacion.DataBind();

            TipoActivoservices TipoActivos = new TipoActivoservices();
            TipoActivo tipoActi = new TipoActivo();
            ddltipo_Activo.DataTextField = "descripcion";
            ddltipo_Activo.DataValueField = "tipo_activo_nif";
            ddltipo_Activo.DataSource = TipoActivos.ListarTipoActivo_NIIF(tipoActi, pUsuario);
            ddltipo_Activo.DataBind();

            ddlmetodo_costo.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));
            ddlmetodo_costo.Items.Insert(1, new ListItem("MODELO DEL COSTO", "1"));
            ddlmetodo_costo.Items.Insert(2, new ListItem("MODELO DE REVALUACIÓN", "2"));
            ddlmetodo_costo.SelectedIndex = 0;
            ddlmetodo_costo.DataBind();

            PoblarLista("unigeneradora_activo_nif", ddlUniGenerado);

            Xpinn.Caja.Services.TipoTopeService tipoTopeServicio = new Xpinn.Caja.Services.TipoTopeService();
            Xpinn.Caja.Entities.TipoTope tipoTope = new Xpinn.Caja.Entities.TipoTope();
            List<Xpinn.Caja.Entities.TipoTope> lstConsulta = new List<Xpinn.Caja.Entities.TipoTope>();
            lstConsulta = tipoTopeServicio.ListarTipoTope(tipoTope, (Usuario)Session["usuario"]);
            ddlmoneda.DataSource = lstConsulta;
            ddlmoneda.DataTextField = "descmoneda";
            ddlmoneda.DataValueField = "cod_moneda";
            ddlmoneda.DataBind();

            ddlPeriodicidad.DataSource = TraerResultadosLista("Periodicidad"); ;
            ddlPeriodicidad.DataTextField = "ListaDescripcion";
            ddlPeriodicidad.DataValueField = "ListaIdStr";
            ddlPeriodicidad.DataBind();

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(activosServicio.GetType().Name + "L", "CargarListas", ex);
        }
    }

    public void PoblarLista(string pTabla, DropDownList ddlControl)
    {
        PoblarLista(pTabla, "", "", "", ddlControl);
    }

    public void PoblarLista(string pTabla, string pColumnas, string pCondicion, string pOrden, DropDownList ddlControl)
    {
        List<Xpinn.Comun.Entities.ListaDesplegable> plista = new List<Xpinn.Comun.Entities.ListaDesplegable>();
        Xpinn.Comun.Entities.ListaDesplegable pentidad = new Xpinn.Comun.Entities.ListaDesplegable();
        Xpinn.Comun.Services.ListaDeplegableService pservicio = new Xpinn.Comun.Services.ListaDeplegableService();

        ddlControl.Items.Clear();
        plista = pservicio.ListarListaDesplegable(pentidad, pTabla, pColumnas, pCondicion, pOrden, (Usuario)Session["usuario"]);
        pentidad.idconsecutivo = null;
        pentidad.descripcion = "Seleccione un item";
        plista.Insert(0, pentidad);
        ddlControl.DataTextField = "descripcion";
        ddlControl.DataValueField = "idconsecutivo";
        ddlControl.DataSource = plista;
        ddlControl.DataBind();

    }

    protected void btnConsultaPersonas_Click(object sender, EventArgs e)
    {
        ctlBusquedaPersonas.Motrar(true, "txtCodEncargado", "txtIdenEncargado", "txtNomEncargado");
    }

    protected void btnProveedor_Click(object sender, EventArgs e)
    {
        ctlBusquedaProveedor.Motrar(true, "txtCodProveedor", "txtIdeProveedor", "txtNomProveedor");
    }

    protected Xpinn.FabricaCreditos.Entities.Persona1 DatosPersona(Int64 pCodigo)
    {
        Xpinn.FabricaCreditos.Services.Persona1Service persona1Servicio = new Xpinn.FabricaCreditos.Services.Persona1Service();
        Xpinn.FabricaCreditos.Entities.Persona1 persona1 = new Xpinn.FabricaCreditos.Entities.Persona1();
        persona1 = persona1Servicio.ConsultaDatosPersona(pCodigo, (Usuario)Session["Usuario"]);
        return persona1;
    }


    protected Boolean validar()
    {
        Boolean validar = true;

        Xpinn.Tesoreria.Data.OperacionData DATesoreria = new Xpinn.Tesoreria.Data.OperacionData();
        Xpinn.Tesoreria.Entities.Operacion pOperacion = new Xpinn.Tesoreria.Entities.Operacion();
        pOperacion.fecha_oper = DateTime.Now;
        if (ValidarProcesoContable(pOperacion.fecha_oper, 21) == true)
        {
            validar = true;
        }
        else
        {
            validar = false;
        }
        return validar;
    }

    protected void generar_Onclick(object sender, EventArgs e)
    {
        if (validar() == true)
        {
            // Validar para generar comprobante
            VerError("");
            // Validar que exista la parametrización contable por procesos
            if (ValidarProcesoContable(Convert.ToDateTime(txtFechaCompra.Text), 21) == false)
            {
                VerError("No se encontró parametrización contable por procesos para el tipo de operación 21 = COMPRA DE ACTIVOS FIJOS");
                return;
            }

            // Determinar código de proceso contable para generar el comprobante
            Int64? rpta = 0;
            if (!panelProceso.Visible && panelGeneral.Visible)
            {
                rpta = ctlproceso.Inicializar(21, Convert.ToDateTime(txtFechaCompra.Text), (Usuario)Session["Usuario"]);
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
                        VerError("Se presentó error al generar la contabilización de la compra del activo fijo");
                }
            }


            // Usuario usuap = (Usuario)Session["usuario"];
            //// Crear el registro de compra                                    
            //DateTime fecha = DateTime.Now;
            //Xpinn.ActivosFijos.Entities.ActivoFijo crearcompra = new Xpinn.ActivosFijos.Entities.ActivoFijo();
            //crearcompra.cod_ope = 0;
            //crearcompra.codigo_act = Convert.ToInt32(txtCodigo.Text);
            //crearcompra.consecutivo = 0;
            //crearcompra.valor_compra = Convert.ToDecimal(txtValorCompra.Text);
            //crearcompra.fecha_compra = Convert.ToDateTime(txtFechaCompra.Text);
            //crearcompra.cod_ope = activosServicio.CrearCOMPRA_ACTIVO(crearcompra, (Usuario)Session["usuario"]);

            //Xpinn.Contabilidad.Services.ComprobanteService ComprobanteServicio = new Xpinn.Contabilidad.Services.ComprobanteService();
            //Session[ComprobanteServicio.CodigoPrograma + ".cod_ope"] = crearcompra.cod_ope;
            //Session[ComprobanteServicio.CodigoPrograma + ".tipo_ope"] = 21;
            //Session[ComprobanteServicio.CodigoPrograma + ".fecha_ope"] = Convert.ToDateTime(fecha.ToShortDateString());
            //Session[ComprobanteServicio.CodigoPrograma + ".cod_persona"] = Convert.ToInt64(usuap.codusuario);
            //Session[ComprobanteServicio.CodigoPrograma + ".cod_ofi_ope"] = usuap.cod_oficina;
            //Session[ComprobanteServicio.CodigoPrograma + ".ventanilla"] = "1";
            //Navegar("../../Contabilidad/Comprobante/Nuevo.aspx");
        }
        else
        {
            VerError("No se pudo validar el tipo de comprobante");
        }
    }

    protected bool AplicarDatos()
    {
        // Determinar el usuario según la variable de sesión
        Usuario usuap = (Usuario)Session["usuario"];

        // Crear el registro de compra            
        DateTime fecha = DateTime.Now;
        Xpinn.ActivosFijos.Entities.ActivoFijo crearcompra = new Xpinn.ActivosFijos.Entities.ActivoFijo();
        crearcompra.cod_ope = 0;
        crearcompra.codigo_act = Convert.ToInt32(txtCodigo.Text);
        crearcompra.consecutivo = 0;
        crearcompra.valor_compra = Convert.ToDecimal(txtValorCompra.Text);
        crearcompra.fecha_compra = Convert.ToDateTime(txtFechaCompra.Text);
        crearcompra.cod_ope = activosServicio.CrearCOMPRA_ACTIVO(crearcompra, (Usuario)Session["usuario"]);
        if (crearcompra.cod_ope <= 0)
            return false;

        // Se cargan las variables requeridas para generar el comprobante
        Xpinn.Contabilidad.Services.ComprobanteService ComprobanteServicio = new Xpinn.Contabilidad.Services.ComprobanteService();
        Session[ComprobanteServicio.CodigoPrograma + ".fecha_ope"] = Convert.ToDateTime(fecha.ToShortDateString());
        Session[ComprobanteServicio.CodigoPrograma + ".cod_ofi_ope"] = usuap.cod_oficina;
        Session[ComprobanteServicio.CodigoPrograma + ".ventanilla"] = "1";
        Int64? cod_proveedor;
        if (txtCodProveedor.Text != null && txtCodProveedor.Text != "")
            cod_proveedor = Convert.ToInt32(txtCodProveedor.Text);
        else
            cod_proveedor = Convert.ToInt64(usuap.codusuario);
        ctlproceso.CargarVariables(crearcompra.cod_ope, 21, cod_proveedor, usuap);

        return true;
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
            AplicarDatos();
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }



}