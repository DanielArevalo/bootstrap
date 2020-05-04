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
using Xpinn.Tesoreria.Entities;
using Xpinn.Tesoreria.Services;
using System.Text;
using System.IO;
using System.Web.UI.HtmlControls;
using Microsoft.CSharp;
using Microsoft.Reporting.WebForms;
using System.Threading;
using Xpinn.Comun.Entities;

partial class Lista : GlobalWeb
{
    EmpresaNovedadService Recaudos = new EmpresaNovedadService();
    Thread tareaEjecucion;
    public static int result;
    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {

            VisualizarOpciones(Recaudos.CodigoPrograma, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoRegresar += btnCancelar_Click;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoExportar += btnExportar_Click;
            toolBar.eventoImprimir += btnInforme_Click;
            toolBar.MostrarGuardar(false);
            toolBar.MostrarExportar(false);
            toolBar.MostrarImprimir(false);
            ctlMensaje.eventoClick += btnContinuarMen_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(Recaudos.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                mtvGeneral.Visible = true;
                mvVentEmergente.Visible = false;
                Timer1.Enabled = false;
                Session["empProg"] = null;
                Session["DTDETALLE"] = null;
                Session["DTDETALLENEW"] = null;
                Session["VentEmergente"] = "0";
                Session["NovNew_Modifica"] = "0";
                Session["OPCION"] = "0";
                Session["DatosBene"] = null;
                Session["DatosBeneNew"] = null;
                CargarDropDown();
                ddlEstado.Enabled = false;
                if (Session[Recaudos.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[Recaudos.CodigoPrograma + ".id"].ToString();
                    Session.Remove(Recaudos.CodigoPrograma + ".id");
                    ObtenerDatos(idObjeto);
                    if (gvLista.Rows.Count > 0)
                        btnGenerarArchivos.Visible = true;
                    Site toolbar = (Site)this.Master;
                    toolbar.MostrarGuardar(true);
                    toolbar.MostrarExportar(true);
                    toolbar.MostrarImprimir(true);
                    Label1.Text = " Modificaron ";
                }
                else
                {
                    btnDetalle.Visible = false;
                    btnDetalleNew.Visible = false;
                    ddlPeriodo.Visible = false;
                    ddlPeriodo.Enabled = false;
                    txtfechaPeriodo.Visible = true;
                    txtfechaPeriodo.Enabled = true;
                    Label1.Text = " Grabaron ";
                    btnGenerarArchivos.Visible = false;
                    ddlEmpresa_SelectedIndexChanged(ddlEmpresa, null);
                }
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(Recaudos.CodigoPrograma, "Page_Load", ex);
        }
    }
    

    private void OcultarAtributos(bool IsVisible) {
        gvLista.Columns[15].Visible = IsVisible;
        gvLista.Columns[16].Visible = IsVisible;
        gvLista.Columns[17].Visible = IsVisible;
        gvLista.Columns[18].Visible = IsVisible;
        gvLista.Columns[19].Visible = IsVisible;

        gvLista.Columns[20].Visible = IsVisible;
        gvLista.Columns[21].Visible = IsVisible;
        gvExport.Columns[12].Visible = IsVisible;
        gvExport.Columns[13].Visible = IsVisible;
        gvExport.Columns[14].Visible = IsVisible;
        gvExport.Columns[15].Visible = IsVisible;
        gvExport.Columns[16].Visible = IsVisible;
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


    protected void CargarDropDown()
    {
        PoblarLista("empresa_recaudo", "", " ESTADO = 1 ", "2", ddlEmpresa);

        ddlEstado.Items.Insert(0, new ListItem("Seleccione un item", "0"));
        ddlEstado.Items.Insert(1, new ListItem("PENDIENTE", "1"));
        ddlEstado.Items.Insert(2, new ListItem("APLICADO", "2"));
        ddlEstado.Items.Insert(3, new ListItem("ANULADO", "3"));
        ddlEstado.SelectedIndex = 1;
        ddlEstado.DataBind();

        PoblarLista("tipoproducto", "", "", "1", ddlTipoProducto);
    }


    protected void btnCancelar_Click(object sender, ImageClickEventArgs e)
    {
        if (mtvGeneral.ActiveViewIndex == 1 || mvVentEmergente.Visible == true || mtvGeneral.ActiveViewIndex == 3)
        {
            Site toolBar = (Site)this.Master;
            toolBar.MostrarGuardar(true);
            toolBar.MostrarExportar(true);
            toolBar.MostrarImprimir(true);
            mvVentEmergente.Visible = false;
            mtvGeneral.Visible = true;
            mtvGeneral.ActiveViewIndex = 0;
        }
        else
        {
            Navegar(Pagina.Lista);
        }
    }



    private void ObtenerDatos(String pIdObjeto)
    {
        EmpresaNovedad vReca = new EmpresaNovedad();

        vReca = Recaudos.ConsultarRecaudo(pIdObjeto, (Usuario)Session["usuario"]);

        if (vReca.numero_novedad != 0)
            txtcodGeneracion.Text = Convert.ToString(vReca.numero_novedad);
        if (vReca.cod_empresa != 0)
            ddlEmpresa.SelectedValue = Convert.ToString(vReca.cod_empresa);

        ddlEmpresa_SelectedIndexChanged(ddlEmpresa, null);

        if (vReca.tipo_lista != 0)
        {
            ddlTipoLista.SelectedValue = Convert.ToString(vReca.tipo_lista);
            Xpinn.Tesoreria.Services.EmpresaRecaudoServices empresaServicio = new EmpresaRecaudoServices();

            EmpresaRecaudo_Programacion empProg = new EmpresaRecaudo_Programacion();

            empProg = empresaServicio.ConsultarEMPRESAPROGRAMACION(Convert.ToInt32(ddlEmpresa.SelectedValue), Convert.ToInt32(ddlTipoLista.SelectedValue), (Usuario)Session["Usuario"]);
            Session["empProg"] = empProg;
        }

        if (txtfechaPeriodo.Visible == true)
        {
            if (vReca.periodo_corte != DateTime.MinValue)
                txtfechaPeriodo.Text = Convert.ToDateTime(vReca.periodo_corte).ToString(gFormatoFecha);
            if (vReca.fecha_inicial != null)
                txtFechaInicial.Text = Convert.ToDateTime(vReca.fecha_inicial).ToShortDateString();
        }
        else
        {
            if (vReca.periodo_corte != null && vReca.periodo_corte != DateTime.MinValue)
            {
                ddlPeriodo.AppendDataBoundItems = false;
                ddlPeriodo.Items.Insert(1, new ListItem(Convert.ToDateTime(vReca.periodo_corte).ToString(gFormatoFecha), Convert.ToDateTime(vReca.periodo_corte).ToString(gFormatoFecha)));
                ddlPeriodo.SelectedIndex = 1;
            }
            if (vReca.fecha_inicial != null)
                txtFechaInicial.Text = Convert.ToDateTime(vReca.fecha_inicial).ToShortDateString();
            else
                ddlPeriodo_SelectedIndexChanged(ddlPeriodo, null);
        }

        if (vReca.fecha_generacion != DateTime.MinValue)
            txtfecha.Text = Convert.ToDateTime(vReca.fecha_generacion).ToString(gFormatoFecha);

        if (vReca.estado != "")
            ddlEstado.SelectedValue = vReca.estado;

        List<EmpresaNovedad> lstReca = new List<EmpresaNovedad>();

        lstReca = Recaudos.ListarDetalleGeneracion(vReca, (Usuario)Session["usuario"]);
        if (lstReca.Count > 0)
        {
            //Totalizar si en caso se maneja el mismo producto
            lstReca = lstReca.GroupBy(d => new { d.numero_producto, d.linea, d.identificacion, d.cod_persona, d.nombres, d.apellidos, d.cod_nomina_empleado })
                        .Select(g => new EmpresaNovedad()
                        {
                            iddetalle = g.First().iddetalle,
                            nom_tipo_producto = g.First().nom_tipo_producto,
                            tipo_producto = g.First().tipo_producto,
                            linea = g.First().linea,
                            numero_producto = g.First().numero_producto,
                            cod_persona = g.First().cod_persona,
                            identificacion = g.First().identificacion,
                            nombres = g.First().nombres,
                            apellidos = g.First().apellidos,
                            valor = g.Sum(s => s.valor),
                            codciudad = g.First().codciudad,
                            direccion = g.First().direccion,
                            telefono = g.First().telefono,
                            email = g.First().email,
                            cod_nomina_empleado = g.First().cod_nomina_empleado,
                            saldo = g.First().saldo,
                            fecha_proximo_pago = g.First().fecha_proximo_pago,
                            capital = g.Sum(s => s.capital),
                            intcte = g.Sum(s => s.intcte),
                            intmora = g.Sum(s => s.intmora),
                            seguro = g.Sum(s => s.seguro),
                            otros = g.Sum(s => s.otros),
                            total_fijos = g.Sum(s => s.total_fijos),
                            total_prestamos = g.Sum(s => s.total_prestamos),
                            fecha_inicio_producto = g.First().fecha_inicio_producto,
                            fecha_vencimiento_producto = g.First().fecha_vencimiento_producto
                        }).ToList();

            btnDetalle.Visible = true;
            Label2.Visible = false;
            lblTotalRegs.Visible = true;
            lblTotalRegs.Text = "<br/> Registros encontrados " + lstReca.Count.ToString();
            gvLista.DataSource = lstReca;
            gvLista.DataBind();
            Session["DTDETALLE"] = lstReca;
            Session["VentEmergente"] = "1";
        }
        else
        {
            Label2.Visible = true;
            btnDetalle.Visible = false;
            Session["DTDETALLE"] = null;
            Session["VentEmergente"] = "0";
        }
        TotalizarRegistros(lstReca, lblTotGenera);

        if (tabNuevos.Visible == true)
        {
            Session["NovNew_Modifica"] = "1";
            ActualizarNuevas();
        }
    }


    protected void TotalizarRegistros(List<EmpresaNovedad> lstInfo, Label lblObject)
    {
        decimal Total = 0;
        if (lstInfo != null && lstInfo.Count > 0)
        {
            Total = lstInfo.Sum(x => x.valor);
        }
        lblObject.Text = " Total Novedades : ";
        lblObject.Text += "<b>";
        lblObject.Text += Total.ToString("N2");
        lblObject.Text += "</b>";
    }


    protected EmpresaNovedad ObtenerValores()
    {
        EmpresaNovedad vRecaudos = new EmpresaNovedad();

        if (txtcodGeneracion.Text != "")
            vRecaudos.numero_novedad = Convert.ToInt64(txtcodGeneracion.Text);
        else
            vRecaudos.numero_novedad = 0;

        if (ddlEmpresa.SelectedValue != "")
            vRecaudos.cod_empresa = Convert.ToInt64(ddlEmpresa.SelectedValue);

        EmpresaRecaudoServices empresaServicio = new EmpresaRecaudoServices();
        EmpresaRecaudo empresaRecaudo = new EmpresaRecaudo();
        empresaRecaudo.cod_empresa = vRecaudos.cod_empresa;
        empresaRecaudo = empresaServicio.ConsultarEMPRESARECAUDO(empresaRecaudo, (Usuario)Session["Usuario"]);
        vRecaudos.tipo_recaudo = empresaRecaudo.tipo_recaudo;
        if (txtFechaInicial.Visible == true)
            if (txtFechaInicial.Text != "")
                vRecaudos.fecha_inicial = ConvertirStringToDate(txtFechaInicial.Text);

        if (txtfechaPeriodo.Visible == true)
        {
            if (txtfechaPeriodo.Text != "")
                vRecaudos.periodo_corte = ConvertirStringToDate(txtfechaPeriodo.Text);
        }
        else
        {
            if (ddlPeriodo.SelectedItem != null)
                vRecaudos.periodo_corte = ConvertirStringToDate(ddlPeriodo.SelectedItem.Text);
        }

        if (ddlTipoLista.SelectedValue != "")
            vRecaudos.tipo_lista = Convert.ToInt32(ddlTipoLista.SelectedValue);

        if (txtfecha.ToDate != DateTime.MinValue.ToShortDateString())
            vRecaudos.fecha_generacion = ConvertirStringToDate(txtfecha.Text);

        if (ddlEstado.SelectedValue != "")
            vRecaudos.estado = ddlEstado.SelectedValue;
        
        return vRecaudos;
    }

    protected void ddlEmpresa_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlPeriodo.Visible = false;
        ddlPeriodo.Enabled = false;
        txtfechaPeriodo.Visible = true;
        txtfechaPeriodo.Enabled = true;
        lblFechaInicial.Visible = false;
        txtFechaInicial.Visible = false;
        txtFechaInicial.Enabled = true;
        tabNuevos.Visible = false;
        if (ddlEmpresa.SelectedValue != "")
        {
            PoblarLista("tipo_lista_recaudo", ddlTipoLista);
            CargaDropEstructura(Convert.ToInt32(ddlEmpresa.SelectedValue));
            // Cargar el período de la lista
            Xpinn.Tesoreria.Services.EmpresaRecaudoServices empresaServicio = new EmpresaRecaudoServices();
            Xpinn.Tesoreria.Entities.EmpresaRecaudo empresa = new EmpresaRecaudo();
            empresa = empresaServicio.ConsultarEMPRESARECAUDO(Convert.ToInt32(ddlEmpresa.SelectedValue), (Usuario)Session["Usuario"]);
            if (empresa != null)
            {
                bool manejaAtributos = Convert.ToBoolean(empresa.maneja_atributos);
                lblManejaAtributo.Text = empresa.maneja_atributos.ToString();
                lblFechaPeriodo.Text = "Periodo";
                lblFechaInicial.Visible = false;
                txtFechaInicial.Visible = false;
                tabNuevos.Visible = false;
                lblnum_planilla.Text = empresa.numero_planilla.ToString();
                if (empresa.tipo_novedad == 1) // Nueva
                {
                    lblFechaPeriodo.Text = "Fecha Final";
                    lblFechaInicial.Visible = true;
                    txtFechaInicial.Visible = true;
                    txtFechaInicial.Enabled = false;
                    tabNuevos.Visible = true;
                }
                if (empresa.tipo_recaudo == 1)
                {
                    ddlPeriodo.Visible = true;
                    ddlPeriodo.Enabled = true;
                    txtfechaPeriodo.Visible = false;
                    txtfechaPeriodo.Enabled = false;
                    ddlTipoLista_SelectedIndexChanged(null, null);
                }
                OcultarAtributos(manejaAtributos);
            }
        }
        else
        {
            ddlTipoLista.SelectedIndex = 0;
            ddlTipoLista.DataBind();
        }
    }

    protected void ddlTipoLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        VerError("");
        if (ddlPeriodo.Visible == true)
        {

            // Llenar el listado de períodos a generar

            List<EmpresaRecaudo_Programacion> lstPeriodos = new List<EmpresaRecaudo_Programacion>();
            if (ddlEmpresa.SelectedValue != "")
            {
                ddlPeriodo.Items.Clear();
                ddlPeriodo.Items.Insert(0, new ListItem("Seleccione un item", ""));
                if (ddlTipoLista.SelectedValue != "")
                {
                    try
                    {
                        Xpinn.Tesoreria.Services.EmpresaRecaudoServices empresaServicio = new EmpresaRecaudoServices();
                        lstPeriodos = empresaServicio.GenerarPeriodos(Convert.ToInt32(ddlEmpresa.SelectedValue), Convert.ToInt32(ddlTipoLista.SelectedValue), DateTime.Now, (Usuario)Session["Usuario"]);
                        if (lstPeriodos.Count != 0)
                        {
                            ddlPeriodo.DataTextField = "fecha_inicio";
                            ddlPeriodo.DataTextFormatString = "{0:d}";
                            ddlPeriodo.DataValueField = "fecha_inicio";
                            ddlPeriodo.DataSource = lstPeriodos;
                            ddlPeriodo.DataBind();
                        }
                    }
                    catch
                    {
                        if (txtFechaInicial.Visible == true)
                            txtFechaInicial.Text = "";
                        VerError("No hay periodos para generar");
                        return;
                    }
                    ddlPeriodo.SelectedIndex = 0;
                }
            }
        }
    }

    public Boolean ValidarDatos()
    {
        if (ddlEmpresa.SelectedValue == "")
        {
            VerError("Seleccione una Empresa");
            return false;
        }
        if (ddlTipoLista.SelectedValue == "")
        {
            VerError("Seleccione un tipo de lista");
            return false;
        }
        if (txtfechaPeriodo.Visible == true)
        {
            if (txtfechaPeriodo.Text == "")
            {
                VerError("Seleccione un Periodo");
                return false;
            }
            else
            {
                if (txtFechaInicial.Visible == true)
                {
                    if (txtFechaInicial.Text == "")
                    {
                        VerError("Seleccione una Fecha Inicial.");
                        txtFechaInicial.Focus();
                        return false;
                    }
                    if (txtfechaPeriodo.Text != "" && txtFechaInicial.Text == "")
                    {
                        VerError("Seleccione una Fecha Inicial.");
                        txtFechaInicial.Focus();
                        return false;
                    }
                }
            }
        }
        if (txtfecha.Text == "")
        {
            VerError("Seleccione una fecha");
            return false;
        }
        if (ddlPeriodo.Visible == true)
        {
            if (ddlPeriodo.SelectedIndex == 0)
            {
                VerError("Seleccione un Periodo");
                ddlPeriodo.Focus();
                return false;
            }
            else
            {
                if (txtFechaInicial.Visible == true)
                {
                    if (ddlPeriodo.SelectedIndex != 0 && txtFechaInicial.Text == "")
                    {
                        VerError("No se calculo bien la fecha Inicial, Verifique los datos.");
                        ddlPeriodo.Focus();
                        return false;
                    }
                }
            }
        }

        //VALIDANDO LA GRIDVIEW PARA REALIZAR LA GRABACION
        foreach (GridViewRow rFila in gvLista.Rows)
        {
            Label lblnom_tipo_producto = (Label)rFila.FindControl("lblnom_tipo_producto");
            if (lblnom_tipo_producto.Text == "")
            {
                VerError("Error en la Novedad Fila : " + (rFila.RowIndex + 1) + ", Ingrese el tipo del producto.");
                return false;
            }

            Label lbllinea = (Label)rFila.FindControl("lbllinea");
            if (lbllinea.Text == "")
            {
                VerError("Error en la Novedad Fila : " + (rFila.RowIndex + 1) + ", Ingrese la Línea del producto.");
                return false;
            }

            Label lblnumero_producto = (Label)rFila.FindControl("lblnumero_producto");
            if (lblnumero_producto.Text == "")
            {
                VerError("Error en la Novedad Fila : " + (rFila.RowIndex + 1) + ", Ingrese el número del producto.");
                return false;
            }

            Label lblcod_persona = (Label)rFila.FindControl("lblcod_persona");
            if (lblcod_persona.Text == "")
            {
                VerError("Error en la Novedad Fila : " + (rFila.RowIndex + 1) + ", Ingrese el código de la Persona.");
                return false;
            }
            else
            {
                bool rpta = IsValidNumber(lblcod_persona.Text.Trim());
                if (rpta == false)
                {
                    VerError("Error en la Novedad Fila : " + (rFila.RowIndex + 1) + ", El código de la persona no tiene el formato correcto. Asegúrese que sea numérico");
                    return false;
                }
            }

            Label lblidentificacion = (Label)rFila.FindControl("lblidentificacion");
            if (lblidentificacion.Text == "")
            {
                VerError("Error en la Novedad Fila : " + (rFila.RowIndex + 1) + ", Ingrese la Identificación de la Persona.");
                return false;
            }

            Label lblnombres = (Label)rFila.FindControl("lblnombres");
            if (lblnombres.Text == "")
            {
                VerError("Error en la Novedad Fila : " + (rFila.RowIndex + 1) + ", Ingrese el Nombre de la Persona.");
                return false;
            }

            Label lblapellidos = (Label)rFila.FindControl("lblapellidos");
            if (lblapellidos.Text == "")
            {
                VerError("Error en la Novedad Fila : " + (rFila.RowIndex + 1) + " de " + gvLista.Rows.Count + ", Ingrese los Apellidos de la Persona. Identificación:" + lblidentificacion.Text);
                return false;
            }
        }
        return true;
    }


    public Boolean ValidarVentanaEmergente()
    {
        lblmsj.Text = "";
        if (ddlTipoProducto.SelectedIndex == 0)
        {
            lblmsj.Text = ("Seleccione un Tipo de Producto");
            ddlTipoProducto.Focus();
            return false;
        }
        if (txtCodProducto.Text == "")
        {
            lblmsj.Text = ("Seleccione un Producto");
            txtCodProducto.Focus();
            return false;
        }
        if (txtNomProducto.Text != "")
        {
            bool rpta = false;
            if (!txtNomProducto.Text.Contains("-"))
            {
                rpta = IsValidNumber(txtNomProducto.Text);
                if (rpta == false)
                {
                    lblmsj.Text = "Ingrese un Código de Linea valido.";
                    txtNomProducto.Focus();
                    return false;
                }
            }
            else
            {
                string[] pLinea = txtNomProducto.Text.Split('-');
                rpta = IsValidNumber(pLinea[0].ToString());
                if (rpta == false)
                {
                    lblmsj.Text = "Ingrese un Código de Linea valido.";
                    txtNomProducto.Focus();
                    return false;
                }
            }
        }
        else
        {
            lblmsj.Text = "Ingrese un Código de Linea valido.";
            txtNomProducto.Focus();
            return false;
        }

        if (txtIdentificacion.Text == "")
        {
            lblmsj.Text = ("Seleccione una Persona");
            return false;
        }
        if (txtNomPersona.Text == "")
        {
            lblmsj.Text = ("Seleccione una Persona");
            return false;
        }
        if (txtvalor.Text == "")
        {
            lblmsj.Text = ("Ingrese un Valor");
            return false;
        }
        if (panelEdNovNew.Visible == true)
        {
            if (ddlTipoProducto.SelectedValue == "2" || ddlTipoProducto.SelectedValue == "4")
            {
                if (ddlTipoNovedad.SelectedValue == "I")
                {
                    if (txtFecFinNov.Text == "")
                    {
                        lblmsj.Text = "Ingrese la Fecha Final de la novedad.";
                        txtFecFinNov.Focus();
                        return false;
                    }
                }
            }
            if (txtvalor.Text != "" && txtVrTotal.Text != "")
            {
                decimal cuota = 0, total = 0;
                cuota = Math.Round(Convert.ToDecimal(txtvalor.Text));
                total = Math.Round(Convert.ToDecimal(txtVrTotal.Text));
                if (cuota > total)
                {
                    lblmsj.Text = "El Valor de Cuota no puede ser mayor al valor total a registrar, Verifique los datos.";
                    txtvalor.Focus();
                    return false;
                }
            }

        }

        return true;
    }

    protected void ObtenerDatosEstructura(String pIdObjeto)
    {
        EstructuraRecaudoServices estructuraService = new EstructuraRecaudoServices();
        try
        {
            Estructura_Carga vRecaudos = new Estructura_Carga();
            vRecaudos.cod_estructura_carga = Convert.ToInt32(pIdObjeto);
            vRecaudos = estructuraService.ConsultarEstructuraCarga(vRecaudos, (Usuario)Session["usuario"]);

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(estructuraService.CodigoPrograma, "ObtenerDatos", ex);
        }
    }

        
    private void Actualizar()
    {
        try
        {
            List<EmpresaNovedad> lstConsulta = new List<EmpresaNovedad>();

            EmpresaNovedad recaudo = new EmpresaNovedad();
      
            if (idObjeto != "")
            {
                recaudo.numero_novedad = Convert.ToInt64(idObjeto);
                if (Session["VentEmergente"].ToString() == "1")
                    if (gvLista.Rows.Count > 0)
                    {
                        lstConsulta = Recaudos.ActualizarDetalleGeneracion(recaudo, (Usuario)Session["usuario"]);
                    }
                    else
                    {
                        lstConsulta = Recaudos.ListarTempNovedades(recaudo, (Usuario)Session["usuario"]);
                        Session["VentEmergente"] = "0";
                    }
                else
                {
                    lstConsulta = Recaudos.ListarTempNovedades(recaudo, (Usuario)Session["usuario"]);
                    Session["VentEmergente"] = "0";
                }
                
                //TOTALIZAR SI EN CASO SE MANEJA EL MISMO PRODUCTO
                if (lstConsulta.Count > 0)
                {
                    lstConsulta = lstConsulta.GroupBy(d => new { d.numero_producto, d.linea, d.identificacion, d.cod_persona, d.nombres, d.apellidos, d.cod_nomina_empleado })
                              .Select(g => new EmpresaNovedad()
                              {
                                  iddetalle = g.First().iddetalle,
                                  nom_tipo_producto = g.First().nom_tipo_producto,
                                  tipo_producto = g.First().tipo_producto,
                                  linea = g.First().linea,
                                  numero_producto = g.First().numero_producto,
                                  cod_persona = g.First().cod_persona,
                                  identificacion = g.First().identificacion,
                                  nombres = g.First().nombres,
                                  apellidos = g.First().apellidos,
                                  valor = g.Sum(s => s.valor),
                                  codciudad = g.First().codciudad,
                                  direccion = g.First().direccion,
                                  telefono = g.First().telefono,
                                  email = g.First().email,
                                  cod_nomina_empleado = g.First().cod_nomina_empleado,
                                  saldo = g.First().saldo,
                                  fecha_proximo_pago = g.First().fecha_proximo_pago,
                                  capital = g.Sum(s => s.capital),                                 
                                  intcte = g.Sum(s => s.intcte),
                                  intmora = g.Sum(s => s.intmora),
                                  seguro = g.Sum(s => s.seguro),
                                  otros = g.Sum(s => s.otros),
                                  total_fijos = g.Sum(s => s.total_fijos),
                                  total_prestamos = g.Sum(s => s.total_prestamos),
                                  fecha_inicio_producto = g.First().fecha_inicio_producto,
                                  fecha_vencimiento_producto = g.First().fecha_vencimiento_producto
                              }).ToList();
                }
            }
            else
            {
                lstConsulta = Recaudos.ListarTempNovedades(recaudo, (Usuario)Session["usuario"]);
            }
                      
            gvLista.AllowPaging = false;
            gvLista.PageSize = pageSize;
            if (ConfigurationManager.AppSettings["pageSizeNovedades"] != null)
            {                
                try
                {
                    int _tam = ConvertirStringToInt32(ConfigurationManager.AppSettings["pageSizeNovedades"].ToString());                    
                    if (_tam > 0)
                    {
                        gvLista.AllowPaging = true;
                        gvLista.PageSize = _tam;
                    }
                }
                catch { }

            }            
            String emptyQuery = "Fila de datos vacia";
            gvLista.EmptyDataText = emptyQuery;
            gvLista.DataSource = lstConsulta;

            Site toolBar = (Site)this.Master;
            toolBar.MostrarGuardar(false);
            toolBar.MostrarExportar(false);
            toolBar.MostrarImprimir(false);

            if (lstConsulta.Count > 0)
            {
                Label2.Visible = false;
                btnDetalle.Visible = true;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                gvLista.DataBind();
                Session["DTDETALLE"] = lstConsulta;
                toolBar.MostrarGuardar(true);
                toolBar.MostrarExportar(true);
                toolBar.MostrarImprimir(true);
            }
            else
            {
                Label2.Visible = true;
                btnDetalle.Visible = false;
                lblTotalRegs.Visible = false;
                Session["DTDETALLE"] = null;
            }

            Session.Add(Recaudos.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(Recaudos.CodigoPrograma, "Actualizar", ex);
        }
    }

    public void IniciarProceso()
    {
        mpeProcesando.Show();
        Image1.Visible = true;
        Session["Proceso"] = "INICIO";
        Timer1.Enabled = true;
    }

    protected void btnGenerar_Click(object sender, ImageClickEventArgs e)
    {
        if (ValidarDatos())
        {
            VerError("");
            Session["OPCION"] = 1;
            ctlMensaje.MostrarMensaje("Desea Generar las Novedades?");
            PanelEncabezado.Enabled = false;
            btnGenerar.Enabled = false;
        }
    }


    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        if (ValidarDatos())
        {
            if (gvLista.Rows.Count == 0)
            {
                VerError("No existen Datos por registrar, verifique los datos.");
                return;
            }
            Session["OPCION"] = 3;
            ctlMensaje.MostrarMensaje("Desea realizar la Grabación de las Novedades generadas?");
        }
    }

    protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            if (ddlEstado.Text != "APLICADO")
            {
                int conseID = Convert.ToInt32(gvLista.DataKeys[e.RowIndex].Values[0].ToString());
                List<EmpresaNovedad> lstDatosCarga = new List<EmpresaNovedad>();
                lstDatosCarga = ObtenerLista();
                if (conseID == 0)
                {
                    lstDatosCarga.RemoveAt((gvLista.PageIndex * gvLista.PageSize) + e.RowIndex);
                    gvLista.DataSourceID = null;
                    gvLista.DataBind();
                    gvLista.DataSource = lstDatosCarga;
                    gvLista.DataBind();
                    Session["DTDETALLE"] = lstDatosCarga;
                    lblTotalRegs.Visible = true;
                    lblTotalRegs.Text = "<br/> Registros encontrados " + lstDatosCarga.Count.ToString();
                }
                else
                {
                    if (idObjeto == "")
                    {
                        lstDatosCarga.RemoveAt((gvLista.PageIndex * gvLista.PageSize) + e.RowIndex);
                        gvLista.DataSourceID = null;
                        gvLista.DataBind();
                        gvLista.DataSource = lstDatosCarga;
                        gvLista.DataBind();
                        Session["DTDETALLE"] = lstDatosCarga;
                    }
                    else
                    {
                        Session["INDEX"] = conseID;
                        Session["OPCION"] = 2;
                        ctlMensaje.MostrarMensaje("Desea Eliminar el registro seleccionado?");
                    }
                }
                TotalizarRegistros(lstDatosCarga, lblTotGenera);
            }
            else
            {
                VerError("No se Puede realizar la eliminación");
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(Recaudos.CodigoPrograma, "gvLista_RowDeleting", ex);
        }
    }

    public void EjecutaProceso()
    {
        try
        {
            string sError = "";
            //GENERACION DE TODAS LAS NOVEDADES
            Recaudos.GenerarNovedades(ObtenerValores(), ref sError, (Usuario)Session["usuario"]);
            //GENERACION DE NOVEDADES NUEVAS
            if (tabNuevos.Visible == true)
            {
                EmpresaNovedad pEmpresaNov = new EmpresaNovedad();
                if (ddlEmpresa.SelectedIndex > 0)
                    pEmpresaNov.cod_empresa = Convert.ToInt64(ddlEmpresa.SelectedValue);
                if (ddlTipoLista.SelectedIndex > 0)
                    pEmpresaNov.tipo_lista = Convert.ToInt32(ddlTipoLista.SelectedValue);
                if (ddlPeriodo.Visible == true && ddlPeriodo.SelectedIndex != 0)
                    pEmpresaNov.periodo_corte = ConvertirStringToDate(ddlPeriodo.SelectedValue);
                if (txtfechaPeriodo.Visible == true && txtfechaPeriodo.Text != "")
                    pEmpresaNov.periodo_corte = ConvertirStringToDate(txtfechaPeriodo.Text);
                if (txtFechaInicial.Visible == true && txtFechaInicial.Text != "")
                    pEmpresaNov.fecha_inicial = ConvertirStringToDate(txtFechaInicial.Text);

                EmpresaRecaudoServices empresaServicio = new EmpresaRecaudoServices();
                EmpresaRecaudo empresaRecaudo = new EmpresaRecaudo();
                empresaRecaudo.cod_empresa = pEmpresaNov.cod_empresa;
                empresaRecaudo = empresaServicio.ConsultarEMPRESARECAUDO(empresaRecaudo, (Usuario)Session["Usuario"]);
                pEmpresaNov.tipo_recaudo = empresaRecaudo.tipo_recaudo;

                if (txtfecha.ToDate != DateTime.MinValue.ToShortDateString())
                    pEmpresaNov.fecha_generacion = ConvertirStringToDate(txtfecha.Text);

                Recaudos.GenerarNovedadesNuevas(pEmpresaNov, ref sError, (Usuario)Session["usuario"]);

            }
            Session["Proceso"] = "FINAL";
            Session["Error"] = sError;
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
            ProcesarError(ex.Message);
        }
    }

    public void TerminarProceso()
    {
        mpeProcesando.Hide();
        Image1.Visible = false;
        Session.Remove("Proceso");
        Timer1.Enabled = false;

        Actualizar();
        ActualizarNuevas();

        if (Session["Error"] != null)
        {
            if (Session["Error"].ToString().Trim() != "")
                lblError.Text = Session["Error"].ToString();
            Session.Remove("Error");
        }
        //Forzar el postback para que actualize los links creados
        ScriptManager.RegisterStartupScript(this, typeof(Page), "jsKeys", "javascript:Forzar();", true);
    }

    protected void ProcesarError(string serror)
    {
        Session["Error"] = serror;
        Session["Proceso"] = "FINAL";
    }

    protected void Timer1_Tick(object sender, EventArgs e)
    {
        if (Session["Proceso"] != null)
            if (Session["Proceso"].ToString() == "FINAL")
                TerminarProceso();
            else
                mpeProcesando.Show();
        else
            mpeProcesando.Hide();
    }

    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        try
        {
            if (Session["OPCION"].ToString() == "1") // GENERAR LAS NOVEDADES
            {
                lblError.Text = "";
                IniciarProceso();

                tareaEjecucion = new Thread(new ThreadStart(EjecutaProceso));
                tareaEjecucion.Start();

            }
            else if (Session["OPCION"].ToString() == "2")// ELIMINAR FILA
            {
                if (Session["INDEX"] != null)
                {
                    if (Tabs.ActiveTabIndex == 0)
                    {
                        if (idObjeto == "") //ELIMINA EL DETALLE DE LA TABLA temporal
                            Recaudos.Eliminar_1_Encabezado_2_Detalle_RECAUDO(Convert.ToInt64(Session["INDEX"].ToString()), (Usuario)Session["usuario"], 2);
                        else //ELIMINA EL DETALLE DE LA TABLA DETRECAUDO
                            Recaudos.Eliminar_1_Encabezado_2_Detalle_RECAUDO(Convert.ToInt64(Session["INDEX"].ToString()), (Usuario)Session["usuario"], 3);
                        Actualizar();
                    }
                    else
                    {
                        Recaudos.EliminarDetRecaudosGeneracionNew(Convert.ToInt64(Session["INDEX"].ToString()), (Usuario)Session["usuario"]);
                        ActualizarNuevas();
                    }
                }
            }
            else if (Session["OPCION"].ToString() == "3")// GRABAR
            {
                EmpresaNovedad reca = new EmpresaNovedad();
                reca = ObtenerValores();

                reca.lstTemp = new List<EmpresaNovedad>();
                reca.lstTemp = ObtenerLista();

                reca.lstTempNuevos = new List<EmpresaNovedad>();
                reca.lstTempNuevos = ObtenerListaNoveNuevos();

                if (reca.lstTemp.Count == 0)
                {
                    VerError("No se encontraron novedades para grabar");
                    return;
                }

                if (idObjeto == "")
                {
                    Recaudos.CrearRecaudosGeneracion(reca, (Usuario)Session["usuario"]);
                }
                else
                {
                    int OpcionTot = 0, OpcionNew = 0;
                    OpcionTot = Session["VentEmergente"].ToString() == "0" ? 1 : 2;
                    OpcionNew = Session["NovNew_Modifica"].ToString() == "0" ? 1 : 2;
                    Recaudos.ModificarRecaudosGeneracion(reca, (Usuario)Session["usuario"], OpcionTot, OpcionNew); //1 = crearDETALLE ___ 2 : MODIFICARDETALLE
                }

                Site toolBar = (Site)Master;
                toolBar.MostrarGuardar(false);
                toolBar.MostrarImprimir(false);
                toolBar.MostrarExportar(false);
                mtvGeneral.ActiveViewIndex = 2;

            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(Recaudos.CodigoPrograma, "btnContinuarMen_Click", ex);
        }
    }


    protected List<EmpresaNovedad> ObtenerLista()
    {
        VerError(gSeparadorMiles);
        List<EmpresaNovedad> lstTemp = new List<EmpresaNovedad>();
        List<EmpresaNovedad> lista = new List<EmpresaNovedad>();
        gvLista.AllowPaging = false;
        gvLista.DataBind();
        if (gvLista.Rows.Count == 0)
        {
            if (Session["DTDETALLE"] != null)
            {
                List<EmpresaNovedad> lstConsulta = (List<EmpresaNovedad>)Session["DTDETALLE"];
                gvLista.DataSource = lstConsulta;
                gvLista.AllowPaging = false;
                gvLista.DataBind();
            }
        }

        bool result = string.IsNullOrEmpty(lblManejaAtributo.Text) ? false : Convert.ToBoolean(Convert.ToInt32(lblManejaAtributo.Text));
        foreach (GridViewRow rfila in gvLista.Rows)
        {
            EmpresaNovedad eTemp = new EmpresaNovedad();

            Label lbliddetalle = (Label)rfila.FindControl("lbliddetalle");
            if (lbliddetalle != null)
                eTemp.iddetalle = Convert.ToInt32(lbliddetalle.Text);

            Label lblnom_tipo_producto = (Label)rfila.FindControl("lblnom_tipo_producto");
            if (lblnom_tipo_producto != null)
                eTemp.nom_tipo_producto = lblnom_tipo_producto.Text;

            Label lbllinea = (Label)rfila.FindControl("lbllinea");
            if (lbllinea != null)
                eTemp.linea = lbllinea.Text;

            Label lblnumero_producto = (Label)rfila.FindControl("lblnumero_producto");
            if (lblnumero_producto != null)
                eTemp.numero_producto = lblnumero_producto.Text;

            Label lblcod_persona = (Label)rfila.FindControl("lblcod_persona");
            if (lblcod_persona != null)
                eTemp.cod_persona = Convert.ToInt64(lblcod_persona.Text);
            else
                eTemp.cod_persona = 0;

            Label lblidentificacion = (Label)rfila.FindControl("lblidentificacion");
            if (lblidentificacion != null)
                eTemp.identificacion = lblidentificacion.Text;

            Label lblnombres = (Label)rfila.FindControl("lblnombres");
            if (lblnombres != null)
                eTemp.nombres = lblnombres.Text;

            Label lblapellidos = (Label)rfila.FindControl("lblapellidos");
            if (lblapellidos != null)
                eTemp.apellidos = lblapellidos.Text;

            Label lblCodigoNomina = (Label)rfila.FindControl("lblCodigoNomina");
            if (lblCodigoNomina != null)
            {
                if (!string.IsNullOrWhiteSpace(lblCodigoNomina.Text))
                {
                    eTemp.cod_nomina_empleado = lblCodigoNomina.Text;
                }
            }

            Label lblsaldo = (Label)rfila.FindControl("lblsaldo");
            if (lblsaldo != null)
            {
                if (!string.IsNullOrWhiteSpace(lblsaldo.Text))
                {
                    eTemp.saldo = Convert.ToDecimal(lblsaldo.Text);
                }
            }

            Label lblfechaGrid = (Label)rfila.FindControl("lblfechaGrid");
            if (lblfechaGrid != null)
                if (lblfechaGrid.Text != "")
                    eTemp.fecha_proximo_pago = ConvertirStringToDate(lblfechaGrid.Text);
                else
                    eTemp.fecha_proximo_pago = null;
            else
                eTemp.fecha_proximo_pago = null;

            Label lblvalor = (Label)rfila.FindControl("lblvalor");
            if (lblvalor != null)
            {
                eTemp.valor = ConvertirStringToDecimal(lblvalor.Text.Trim());
            }
            eTemp.estado = ddlEstado.SelectedValue;

            if (result)
            {
                Label lblVrCapital = (Label)rfila.FindControl("lblVrCapital");
                if (lblVrCapital != null)
                    eTemp.capital = ConvertirStringToDecimal(lblVrCapital.Text.Trim());

                Label lblVrInteresCte = (Label)rfila.FindControl("lblVrInteresCte");
                if (lblVrInteresCte != null)
                    eTemp.intcte = ConvertirStringToDecimal(lblVrInteresCte.Text.Trim());

                Label lblVrInteresMora = (Label)rfila.FindControl("lblVrInteresMora");
                if (lblVrInteresMora != null)
                    eTemp.intmora = ConvertirStringToDecimal(lblVrInteresMora.Text.Trim());

                Label lblVrSeguro = (Label)rfila.FindControl("lblVrSeguro");
                if (lblVrSeguro != null)
                    eTemp.seguro = ConvertirStringToDecimal(lblVrSeguro.Text.Trim());

                Label lblVrOtros = (Label)rfila.FindControl("lblVrOtros");
                if (lblVrOtros != null)
                    eTemp.otros = ConvertirStringToDecimal(lblVrOtros.Text.Trim());

                Label lblVrtotal_fijos = (Label)rfila.FindControl("lblVrtotal_fijos");
                if (lblVrtotal_fijos != null)
                    eTemp.total_fijos = ConvertirStringToDecimal(lblVrtotal_fijos.Text.Trim());

                Label lblVrtotal_prestamos = (Label)rfila.FindControl("lblVrtotal_prestamos");
                if (lblVrtotal_prestamos != null)
                    eTemp.total_prestamos = ConvertirStringToDecimal(lblVrtotal_prestamos.Text.Trim());               
            }

            Label lblFecInicio = (Label)rfila.FindControl("lblFecInicio");
            if (lblFecInicio != null)
                if(lblFecInicio.Text != null && lblFecInicio.Text != "")
                eTemp.fecha_inicio_producto = Convert.ToDateTime(lblFecInicio.Text.Trim());

            Label lblFecVencimiento = (Label)rfila.FindControl("lblFecVencimiento");
            if (lblFecVencimiento != null)
                if (lblFecVencimiento.Text != null && lblFecVencimiento.Text != "")
                    eTemp.fecha_vencimiento_producto = Convert.ToDateTime(lblFecVencimiento.Text.Trim());

            Label lblvacaciones = (Label)rfila.FindControl("lblvacaciones");
            if (lblvacaciones != null)
                if (!string.IsNullOrEmpty(lblvacaciones.Text))
                    eTemp.vacaciones = Convert.ToInt64(lblvacaciones.Text.Trim());
            lista.Add(eTemp);
            Session["DatosBene"] = lista;

            if (eTemp.nom_tipo_producto.Trim() != "" && eTemp.identificacion.Trim() != null)
            {
                lstTemp.Add(eTemp);
            }
        }

        return lstTemp;
    }


    public static bool IsValidNumber(string strNumber)
    {
        // Return true if strIn is in valid Number format.
        return System.Text.RegularExpressions.Regex.IsMatch(strNumber, "^[0-9]*$");
    }


    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        if (ddlEstado.SelectedValue.ToEnum<EstadoNovedad>() == EstadoNovedad.Aplicadas)
        {
            VerError("No puedes editar una novedad ya aplicada!.");
            return;
        }

        String id = gvLista.Rows[e.NewEditIndex].Cells[0].Text;

        Cambiarhabilitado(false);

        lblFecEmerg.Text = "F. Prox Pago";
        panelEdNovNew.Visible = false;
        Label lbliddetalle = (Label)gvLista.Rows[e.NewEditIndex].Cells[2].FindControl("lbliddetalle");
        if (lbliddetalle != null)
            txtidDetalle.Text = lbliddetalle.Text;

        Label lblnom_tipo_producto = (Label)gvLista.Rows[e.NewEditIndex].Cells[3].FindControl("lblnom_tipo_producto");
        if (lblnom_tipo_producto != null)
        {
            if (lblnom_tipo_producto.Text != "")
            {
                if (IsValidNumber(lblnom_tipo_producto.Text))
                {
                    ddlTipoProducto.SelectedValue = BuscarProducto(lblnom_tipo_producto.Text);
                }
                else
                {
                    if (lblnom_tipo_producto.Text.Contains("-"))
                    {
                        string[] sValor = lblnom_tipo_producto.Text.Split('-');
                        ddlTipoProducto.SelectedValue = BuscarProducto(sValor[0]);
                    }
                }
            }
        }


        Label lbllinea = (Label)gvLista.Rows[e.NewEditIndex].Cells[4].FindControl("lbllinea");
        if (lbllinea != null)
            txtNomProducto.Text = lbllinea.Text;

        Label lblnumero_producto = (Label)gvLista.Rows[e.NewEditIndex].Cells[5].FindControl("lblnumero_producto");
        if (lblnumero_producto != null)
            txtCodProducto.Text = lblnumero_producto.Text;

        Label lblcod_persona = (Label)gvLista.Rows[e.NewEditIndex].Cells[6].FindControl("lblcod_persona");
        if (lblcod_persona != null)
            txtCodPersona.Text = lblcod_persona.Text;

        Label lblidentificacion = (Label)gvLista.Rows[e.NewEditIndex].Cells[7].FindControl("lblidentificacion");
        if (lblidentificacion != null)
            txtIdentificacion.Text = lblidentificacion.Text;

        Label lblnombres = (Label)gvLista.Rows[e.NewEditIndex].Cells[8].FindControl("lblnombres");
        Label lblapellidos = (Label)gvLista.Rows[e.NewEditIndex].Cells[9].FindControl("lblapellidos");

        if (lblnombres != null && lblapellidos != null)
            txtNomPersona.Text = lblnombres.Text + ", " + lblapellidos.Text;

        Label lblfechaGrid = (Label)gvLista.Rows[e.NewEditIndex].Cells[10].FindControl("lblfechaGrid");
        if (lblfechaGrid != null)
            txtProxPago.Text = lblfechaGrid.Text;

        Label lblvalor = (Label)gvLista.Rows[e.NewEditIndex].Cells[11].FindControl("lblvalor");
        if (lblvalor != null)
            txtvalor.Text = lblvalor.Text;
        Cambiarhabilitado(true);

        lblmsj.Text = "";
        e.NewEditIndex = -1;
        mpeActualizarNovedad.Show();
        ScriptManager.RegisterStartupScript(this, typeof(Page), "jsKeys", "javascript:gridviewScroll();", true);
    }


    private string BuscarProducto(string pNomTipo)
    {
        string tipo_producto = string.Empty;
        pNomTipo = !string.IsNullOrEmpty(pNomTipo) ? pNomTipo.Trim() : string.Empty;

        if (!string.IsNullOrEmpty(pNomTipo))
        {
            if (pNomTipo == "APORTES" || pNomTipo == "1-APORTES" || pNomTipo == "1")
                tipo_producto = "1";
            if (pNomTipo == "CREDITOS" || pNomTipo == "2-CREDITOS" || pNomTipo == "CRÉDITOS" || pNomTipo == "2")
                tipo_producto = "2";
            if(pNomTipo == "DEPOSITOS" || pNomTipo == "3-DEPOSITOS" || pNomTipo == "DEPÓSITOS" || pNomTipo == "AHORROS A LA VISTA" || pNomTipo == "3")
                tipo_producto = "3";
            if(pNomTipo == "SERVICIOS" || pNomTipo == "4-SERVICIOS" || pNomTipo == "SERVICIOS" || pNomTipo == "4")
                tipo_producto = "4";
            if(pNomTipo == "AHORRO PROGRAMADO" || pNomTipo == "9-AHORRO PROGRAMADO" || pNomTipo == "AHORRO PROGRAMADO" || pNomTipo == "9")
                tipo_producto = "9";
            if(pNomTipo == "AFILIACION" || pNomTipo == "6-AFILIACION" || pNomTipo == "AFILIACIÓN" || pNomTipo == "6")
                tipo_producto = "6";
            if (pNomTipo == "DEVOLUCION" || pNomTipo == "DEVOLUCIONES")
                tipo_producto = "8";
            if(pNomTipo == "CREDITOS-CUOTAS EXTRAS" || pNomTipo == "10-CREDITOS-CUOTAS EXTRAS" || pNomTipo == "CRÉDITOS-CUOTAS EXTRAS" || pNomTipo == "10")
                tipo_producto = "10";
            if(pNomTipo == "INT.AHORRO PERMANENTE" || pNomTipo == "11-INT.AHORRO PERMANENTE" || pNomTipo == "11")
                tipo_producto = "11";
        }

        return tipo_producto;
    }

    protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvLista.PageIndex = e.NewPageIndex;
            if (Session["DTDETALLE"] != null)
            {
                List<EmpresaNovedad> lstConsulta = new List<EmpresaNovedad>();
                lstConsulta = (List<EmpresaNovedad>)Session["DTDETALLE"];
                gvLista.DataSource = lstConsulta;                
                if (lstConsulta.Count > 0)
                {
                    gvLista.DataBind();
                    return;
                }
            }
            Actualizar();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(Recaudos.CodigoPrograma, "gvLista_PageIndexChanging", ex);
        }
    }


    EmpresaNovedad DatosVentanaEmergente()
    {
        string[] sDatos;
        EmpresaNovedad reca = new EmpresaNovedad();

        if (txtidDetalle.Text != "")
            reca.iddetalle = Convert.ToInt32(txtidDetalle.Text);

        if (txtcodGeneracion.Text != "")
            reca.numero_novedad = Convert.ToInt32(txtcodGeneracion.Text);

        if (ddlTipoProducto.SelectedItem.Text != "" || ddlTipoProducto.SelectedIndex != 0)
        {
            reca.nom_tipo_producto = ddlTipoProducto.SelectedValue + "-" + ddlTipoProducto.SelectedItem.Text.ToUpper();
            if ((Session["VentEmergente"].ToString() != "1" && Session["VentEmergente"] != null) || (Session["NovNew_Modifica"].ToString() != "1" && Session["NovNew_Modifica"] != null))
            {
                reca.tipo_productotemp = Convert.ToInt32(ddlTipoProducto.SelectedValue);
                reca.nom_tipo_producto = ddlTipoProducto.SelectedItem.Text.ToUpper().Trim();
            }
        }

        if (txtCodProducto.Text != "")
            reca.numero_producto = Convert.ToString(txtCodProducto.Text);

        if (txtNomProducto.Text != "")
        {
            reca.linea = txtNomProducto.Text.Trim();
            if ((Session["VentEmergente"].ToString() != "1" && Session["VentEmergente"] != null) || (Session["NovNew_Modifica"].ToString() != "1" && Session["NovNew_Modifica"] != null))
            {
                string[] pLinea;
                if (txtNomProducto.Text.Contains("-"))
                {
                    pLinea = txtNomProducto.Text.Split('-');
                    if (IsValidNumber(pLinea[0].ToString()))
                    {
                        reca.linea = pLinea[0].ToString();
                        reca.nom_linea = pLinea[1].ToString();
                    }
                    else
                    {
                        lblmsj.Text = "El código de la linea es invalido, verifique los datos.";
                        txtNomProducto.Focus();
                        return null;
                    }
                }
                else
                {
                    if (IsValidNumber(txtNomProducto.Text))
                    {
                        reca.linea = txtNomProducto.Text.Trim();
                        reca.nom_linea = null;
                    }
                    else
                    {
                        lblmsj.Text = "El código de la linea es invalido, verifique los datos.";
                        txtNomProducto.Focus();
                        return null;
                    }
                }
                reca.nom_tipo_producto = ddlTipoProducto.SelectedItem.Text.ToUpper().Trim();
            }
        }

        if (txtCodPersona.Text != "")
            reca.cod_persona = Convert.ToInt64(txtCodPersona.Text);

        if (txtIdentificacion.Text != "")
            reca.identificacion = txtIdentificacion.Text;

        if (txtNomPersona.Text != "")
        {
            sDatos = txtNomPersona.Text.Split(',');
            reca.nombres = sDatos[0].Trim();
            reca.apellidos = sDatos[1].Trim();
            reca.nombre = txtNomPersona.Text;
        }

        if (txtvalor.Text != "")
            reca.valor = Convert.ToDecimal(txtvalor.Text);

        if (txtProxPago.Text != "")
            reca.fecha_proximo_pago = ConvertirStringToDate(txtProxPago.Text);

        //captura los datos faltantes de novedades Nuevas
        if (panelEdNovNew.Visible == true)
        {
            if (txtProxPago.Text != "")
                reca.fecha_inicial = ConvertirStringToDate(txtProxPago.Text);
            if (txtFecFinNov.Text != "")
                reca.fecha_final = ConvertirStringToDate(txtFecFinNov.Text);
            reca.tipo_novedad = ddlTipoNovedad.SelectedValue;

            if (txtVrTotal.Text != "")
                reca.valor_total = Convert.ToDecimal(txtVrTotal.Text);
            if (txtNumCuotas.Text != "")
                reca.numero_cuotas = Convert.ToInt32(txtNumCuotas.Text);
        }

        reca.estado = "1"; // PENDIENTE

        return reca;
    }


    protected void btnModificar_Click(object sender, EventArgs e)
    {
        try
        {
            if (ValidarVentanaEmergente() && IsPostBack)
            {
                EmpresaNovedad pEntidad = new EmpresaNovedad();
                Page.Validate();
                gvLista.Visible = true;
                if (Page.IsValid)
                {
                    if (ddlTipoProducto.SelectedIndex == 0)
                    {
                        VerError("Seleccione el Tipo de Producto");
                        return;
                    }

                    long CodEmpresa = 0;
                    EmpresaNovedad objDetalleNovedad = DatosVentanaEmergente();
                    if (ddlEmpresa.SelectedValue == null)
                    {
                        VerError("Seleecione la empresa para poder continuar con el ingreso de novedades");
                        return;
                    }
                    else
                        CodEmpresa = long.Parse(ddlEmpresa.SelectedValue);

                    if (!ValidarPersonaHabil(objDetalleNovedad.cod_persona.Value, CodEmpresa))
                    {
                        mpeActualizarNovedad.Show();
                        lblmsj.Text = "No se puede generar una novedad a una persona que se encuentra inhabil";
                        return;
                    }

                    if (Tabs.ActiveTabIndex == 0)
                    {
                        if (Session["VentEmergente"].ToString() == "1")//se trabaja en base a la tabla DETRECAUDO_MASIVO
                            if (txtidDetalle.Text == "")
                                pEntidad = Recaudos.CrearDetRecaudosGeneracion(objDetalleNovedad, (Usuario)Session["usuario"]);
                            else
                                pEntidad = Recaudos.ModificarDetRecaudosGeneracion(objDetalleNovedad, (Usuario)Session["usuario"]);
                        else // Se modifica o crea a la tabla temporal
                            if (txtidDetalle.Text != "")
                            pEntidad = Recaudos.MODIFICAR_TEMP_RECAUDO(objDetalleNovedad, (Usuario)Session["usuario"]);
                        else
                            pEntidad = Recaudos.CREAR_TEMP_RECAUDO(objDetalleNovedad, (Usuario)Session["usuario"]);
                        Actualizar();
                    }
                    else if (Tabs.ActiveTabIndex == 1)
                    {
                        if (Session["NovNew_Modifica"].ToString() == "1") // se trabaja en Base a la tabla DETRECAUDO_MASIVO_NUEVO
                        {
                            if (txtidDetalle.Text == "")
                                pEntidad = Recaudos.CrearDetRecaudosGeneracionNEW(objDetalleNovedad, (Usuario)Session["usuario"]);
                            else
                                pEntidad = Recaudos.CrearDetRecaudosGeneracionNEW(objDetalleNovedad, (Usuario)Session["usuario"]);
                        }
                        else // Se modifica o crea a la tabla temporal NUEVO
                        {
                            if (txtidDetalle.Text != "")
                                pEntidad = Recaudos.MODIFICAR_TEMP_RECAUDO_NUEVO(objDetalleNovedad, (Usuario)Session["usuario"]);
                            else
                                pEntidad = Recaudos.CREAR_TEMP_RECAUDO_NUEVO(objDetalleNovedad, (Usuario)Session["usuario"]);
                        }
                        ActualizarNuevas();
                    }
                }
                mpeActualizarNovedad.Hide();
            }
            else
            {
                mpeActualizarNovedad.Show();
            }
            ScriptManager.RegisterStartupScript(this, typeof(Page), "jsKeys", "javascript:gridviewScroll();", true);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(Recaudos.CodigoPrograma, "btnModificar_Click", ex);
        }
    }

    protected void btnCloseReg1_Click(object sender, EventArgs e)
    {
        limpiarVentanaEmergente();
        mpeActualizarNovedad.Hide();
        ScriptManager.RegisterStartupScript(this, typeof(Page), "jsKeys", "javascript:gridviewScroll();", true);
    }



    protected void btnBuscarPersona_Click(object sender, EventArgs e)
    {
        ctlBusquedaPersona.Motrar(true, "txtCodPersona", "txtIdentificacion", "txtNomPersona");
    }

    void limpiarVentanaEmergente()
    {
        txtidDetalle.Text = "";
        ddlTipoProducto.SelectedIndex = 0;
        txtCodProducto.Text = "";
        txtNomProducto.Text = "";
        txtIdentificacion.Text = "";
        txtNomPersona.Text = "";
        txtvalor.Text = "0";
        txtProxPago.Text = DateTime.Today.ToShortDateString();
        ddlTipoNovedad.SelectedIndex = 0;
        txtFecFinNov.Text = "";
        txtVrTotal.Text = "";
        txtNumCuotas.Text = "";
    }

    void Cambiarhabilitado(bool opc)
    {
        btnBuscarPersona.Enabled = opc;
        btnBuscaProductos.Enabled = opc;
        txtCodProducto.Enabled = opc;
        txtNomProducto.Enabled = opc;
        txtIdentificacion.Enabled = opc;
        txtvalor.Enabled = opc;
        txtProxPago.Enabled = opc;
        txtNomPersona.Enabled = opc;
        if (Tabs.ActiveTabIndex == 1)
            panelEdNovNew.Enabled = opc;
    }

    protected void btnDetalle_Click(object sender, EventArgs e)
    {
        VerError("");
        lblmsj.Text = "";
        lblFecEmerg.Text = "F. Prox Pago";
        panelEdNovNew.Visible = false;
        limpiarVentanaEmergente();
        Cambiarhabilitado(false);
        mpeActualizarNovedad.Show();
    }

    protected void ddlTipoProducto_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlTipoProducto.SelectedIndex != 0)
            Cambiarhabilitado(true);
        else
            Cambiarhabilitado(false);
    }

    protected void txtIdentificacion_TextChanged(object sender, EventArgs e)
    {
        RecaudosMasivosService recaudoServicio = new RecaudosMasivosService();
        string pReca;
        pReca = txtIdentificacion.Text;
        Xpinn.FabricaCreditos.Entities.Persona1 Data = new Xpinn.FabricaCreditos.Entities.Persona1();

        Data = recaudoServicio.ConsultarPersona(pReca, (Usuario)Session["usuario"]);
        if (Data.cod_persona != 0)
            txtCodPersona.Text = Data.cod_persona.ToString();
        if (Data.tipo_persona == "N")
        {
            if (Data.nombres != null && Data.apellidos != null)
                txtNomPersona.Text = Data.nombres.Trim() + ", " + Data.apellidos.Trim();
        }
        else
        {
            if (Data.nombre != null)
                txtNomPersona.Text = Data.nombre.Trim();
        }
    }

    /*
    protected void btnExportar_Click(object sender, EventArgs e)
    {
        try
        {
            VerError("");
            if (gvLista.Rows.Count > 0 && Session["DTDETALLE"] != null && Tabs.ActiveTabIndex == 0 || gvNovedadesNuevas.Rows.Count > 0 && Session["DTDETALLENEW"] != null && Tabs.ActiveTabIndex == 1)
            {
                StringBuilder sb = new StringBuilder();
                StringWriter sw = new StringWriter(sb);
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                Page pagina = new Page();
                dynamic form = new HtmlForm();
                if (Tabs.ActiveTabIndex == 0)
                {
                    gvExport.AllowPaging = false;
                    gvExport.DataSource = Session["DTDETALLE"];
                    gvExport.DataBind();
                    gvExport.EnableViewState = false;
                    pagina.EnableEventValidation = false;
                    pagina.DesignerInitialize();
                    pagina.Controls.Add(form);
                    form.Controls.Add(gvLista);
                }
                else if (Tabs.ActiveTabIndex == 1)
                {
                    gvNovedadesNuevas.Columns[0].Visible = false;
                    gvNovedadesNuevas.Columns[1].Visible = false;
                    gvNovedadesNuevas.AllowPaging = false;
                    gvNovedadesNuevas.DataSource = Session["DTDETALLENEW"];
                    gvNovedadesNuevas.DataBind();
                    gvNovedadesNuevas.EnableViewState = false;
                    pagina.EnableEventValidation = false;
                    pagina.DesignerInitialize();
                    pagina.Controls.Add(form);
                    form.Controls.Add(gvNovedadesNuevas);
                }
                pagina.RenderControl(htw);
                Response.Clear();
                Response.Buffer = true;
                Response.ContentType = "application/vnd.ms-excel";
                if (Tabs.ActiveTabIndex == 0)
                    Response.AddHeader("Content-Disposition", "attachment;filename=NovedadesTotales.xls");
                else
                    Response.AddHeader("Content-Disposition", "attachment;filename=NovedadesNuevas.xls");
                Response.Charset = "UTF-8";
                Response.ContentEncoding = Encoding.Default;
                Response.Write(sb.ToString());
                Response.End();
            }
            else
            {
                VerError("No Existen Datos");
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
    }
    */
    protected void btnExportar_Click(object sender, EventArgs e)
    {
        VerError("");
        if (gvLista.Rows.Count > 0 && Session["DTDETALLE"] != null && Tabs.ActiveTabIndex == 0 || gvNovedadesNuevas.Rows.Count > 0 && Session["DTDETALLENEW"] != null && Tabs.ActiveTabIndex == 1)
        {
            if (Tabs.ActiveTabIndex == 0)
                ExportarCSV(1);
            else
                ExportarCSV(2);
        }
    }
    protected void ExportarCSV(int Tab)
    {
        try
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition",
             "attachment;filename=GeneracionNovedades.csv");
            Response.Charset = "";
            Response.ContentType = "application/text";
            gvExport.AllowPaging = false;
            if (Tab == 1)
            {
                gvExport.DataSource = Session["DTDETALLE"];
            }
            else
            {
                gvExport.DataSource = Session["DTDETALLENEW"];
            }   
            gvExport.DataBind();

            StringBuilder sb = ExportarGridCSV(gvExport);
            Response.Output.Write(sb.ToString());
            Response.Flush();

            Response.End();
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }


    //PENDIENTE AJUSTAR ESTA OPCION
    protected void btnInforme_Click(object sender, EventArgs e)
    {
        VerError("");
        Xpinn.Tesoreria.Services.EmpresaRecaudoServices empresaServicio = new EmpresaRecaudoServices();
        Xpinn.Tesoreria.Entities.EmpresaRecaudo empresa = new EmpresaRecaudo();
        empresa = empresaServicio.ConsultarEMPRESARECAUDO(Convert.ToInt32(ddlEmpresa.SelectedValue), (Usuario)Session["Usuario"]);

        string cRutaDeImagen;
        cRutaDeImagen = Server.MapPath("~/Images\\") + "LogoEmpresa.jpg";


     

        if (Tabs.ActiveTabIndex == 0)
        {
            if (gvLista.Rows.Count > 0 && Session["DTDETALLE"] != null)
            {
                if (empresa.maneja_atributos == 1)
                {
                    Usuario usuap = new Usuario();
                    usuap = (Usuario)Session["Usuario"];
                    ReportViewRecaudos.LocalReport.ReportPath = "Page/RecaudosMasivos/Generacion/ReporteResumido.rdlc";
                     Microsoft.Reporting.WebForms.ReportParameter[] param = new Microsoft.Reporting.WebForms.ReportParameter[8];
                    param[0] = new ReportParameter("pEmpresa", ddlEmpresa.SelectedItem.Text);
                    param[1] = new ReportParameter("pPeriodo", vacios(txtfechaPeriodo.Text));
                    param[2] = new ReportParameter("pTipoLista", vacios(ddlTipoLista.SelectedItem.Text));
                    param[3] = new ReportParameter("pFechaRecaudo", vacios(txtfecha.Text));
                    param[4] = new ReportParameter("pEstado", vacios(ddlEstado.SelectedItem.Text));
                    param[5] = new ReportParameter("pFecha", Convert.ToString(DateTime.Now.ToShortDateString()));
                    param[6] = new ReportParameter("pEntidad", usuap.empresa);
                    param[7] = new ReportParameter("ImagenReport", cRutaDeImagen);
                    ReportViewRecaudos.LocalReport.EnableExternalImages = true;
                    ReportViewRecaudos.LocalReport.SetParameters(param);
                    ReportViewRecaudos.LocalReport.DataSources.Clear();
                    ReportDataSource rds = new ReportDataSource("DataSet1", CrearDataTableRecaudosResumidos());
                    ReportViewRecaudos.LocalReport.DataSources.Add(rds);
                    ReportViewRecaudos.LocalReport.Refresh();
                }
                else
                {
                    Usuario usuap = new Usuario();
                    usuap = (Usuario)Session["Usuario"];
                    Microsoft.Reporting.WebForms.ReportParameter[] param = new Microsoft.Reporting.WebForms.ReportParameter[8];
                    param[0] = new ReportParameter("pEmpresa", ddlEmpresa.SelectedItem.Text);
                    param[1] = new ReportParameter("pPeriodo", vacios(txtfechaPeriodo.Text));
                    param[2] = new ReportParameter("pTipoLista", vacios(ddlTipoLista.SelectedItem.Text));
                    param[3] = new ReportParameter("pFechaRecaudo", vacios(txtfecha.Text));
                    param[4] = new ReportParameter("pEstado", vacios(ddlEstado.SelectedItem.Text));
                    param[5] = new ReportParameter("pFecha", Convert.ToString(DateTime.Now.ToShortDateString()));
                    param[6] = new ReportParameter("pEntidad", usuap.empresa);
                    param[7] = new ReportParameter("ImagenReport", cRutaDeImagen);
                    ReportViewRecaudos.LocalReport.EnableExternalImages = true;
                    ReportViewRecaudos.LocalReport.DataSources.Clear();
                    ReportViewRecaudos.LocalReport.SetParameters(param);
                    ReportViewRecaudos.LocalReport.SetParameters(param);
                    ReportDataSource rds = new ReportDataSource("DataSet1", CrearDataTableRecaudos());
                    ReportViewRecaudos.LocalReport.DataSources.Add(rds);
                    ReportViewRecaudos.LocalReport.ReportPath = "Page/RecaudosMasivos/Generacion/Reporte.rdlc";
                    ReportViewRecaudos.LocalReport.Refresh();
                }
        }
            else
            {
                VerError("No existen Datos, verifique por favor.");
            }
        }
        else if (Tabs.ActiveTabIndex == 1)
        {
            if (gvNovedadesNuevas.Rows.Count > 0 && Session["DTDETALLENEW"] != null)
            {
                ReportViewRecaudos.LocalReport.DataSources.Clear();
                ReportDataSource rds = new ReportDataSource("DataSet1", CrearDataTableRecaudosNEW());
                ReportViewRecaudos.LocalReport.DataSources.Add(rds);
                ReportViewRecaudos.LocalReport.ReportPath = "Page/RecaudosMasivos/Generacion/rptNovedadesNew.rdlc";
                ReportViewRecaudos.LocalReport.Refresh();
            }
            else
            {
                VerError("No existen Datos, verifique por favor.");
            }
        }

        mtvGeneral.ActiveViewIndex = 1;
        ReportViewRecaudos.Visible = true;
        Site toolBar = (Site)this.Master;
        toolBar.MostrarGuardar(false);
        toolBar.MostrarExportar(false);
        toolBar.MostrarImprimir(false);
    }

    public DataTable CrearDataTableRecaudos()
    {
        List<EmpresaNovedad> lstConsulta = new List<EmpresaNovedad>();
        lstConsulta = (List<EmpresaNovedad>)Session["DTDETALLE"];
        System.Data.DataTable table = new System.Data.DataTable();

        table.Columns.Add("TipoProducto");
        table.Columns.Add("Linea");
        table.Columns.Add("NumeroProducto");
        table.Columns.Add("CodigoCliente");
        table.Columns.Add("Identificacion");
        table.Columns.Add("Nombres");
        table.Columns.Add("Apellidos");
        table.Columns.Add("fechaproxpago");
        table.Columns.Add("Valor");
        table.Columns.Add("Vacaciones");

        foreach (EmpresaNovedad item in lstConsulta)
        {
            DataRow datarw;
            datarw = table.NewRow();
            datarw[0] = item.nom_tipo_producto;
            if (txtcodGeneracion.Text != "")
                datarw[1] = item.linea;
            else
                datarw[1] = item.nom_linea;
            datarw[2] = item.numero_producto;
            datarw[3] = item.cod_persona;
            datarw[4] = item.identificacion;
            datarw[5] = item.nombres;
            datarw[6] = item.apellidos;
            datarw[7] = item.vacaciones;

            if (item.fecha_proximo_pago != null)
                datarw[7] = Convert.ToDateTime(item.fecha_proximo_pago).ToShortDateString();
            else
                datarw[7] = "";
            datarw[8] = item.valor;
            table.Rows.Add(datarw);
        }
        return table;
    }


    public DataTable CrearDataTableRecaudosResumidos()
    {
        List<EmpresaNovedad> lstConsulta = new List<EmpresaNovedad>();
        lstConsulta = (List<EmpresaNovedad>)Session["DTDETALLE"];
        System.Data.DataTable table = new System.Data.DataTable();
       
        table.Columns.Add("CodigoCliente");
        table.Columns.Add("Identificacion");
        table.Columns.Add("Nombres");
        table.Columns.Add("Apellidos");
        table.Columns.Add("Fijos");
        table.Columns.Add("Prestamos", typeof(decimal));
        table.Columns.Add("Int_Corriente");
        table.Columns.Add("Mora");
        table.Columns.Add("Seguro");
        table.Columns.Add("Otros");     
        table.Columns.Add("Total", typeof(decimal));
        table.Columns.Add("Capital");
        table.Columns.Add("Vacaciones");

        lstConsulta = lstConsulta.GroupBy(d => new {d.identificacion, d.cod_persona, d.nombres, d.apellidos, d.cod_nomina_empleado })
                   .Select(g => new EmpresaNovedad()
                   {
                    
                       cod_persona = g.First().cod_persona,
                       identificacion = g.First().identificacion,
                       nombres = g.First().nombres,
                       apellidos = g.First().apellidos,                    
                       capital = g.Sum(d => d.capital),
                       intcte = g.Sum(d => d.intcte),
                       intmora = g.Sum(d => d.intmora),
                       seguro = g.Sum(d => d.seguro),
                       otros = g.Sum(d => d.otros),
                       total_fijos = g.Sum(d => d.total_fijos),
                       total_prestamos = g.Sum(d =>  d.total_prestamos)


                   }).ToList();


        lstConsulta = lstConsulta.OrderBy(x => x.identificacion)
                 .ToList();

        foreach (EmpresaNovedad item in lstConsulta)
        {
            DataRow datarw;
            datarw = table.NewRow();
            datarw[0] = item.cod_persona;         
            datarw[1] = item.identificacion;
            datarw[2] = item.nombres;
            datarw[3] = item.apellidos;
            datarw[4] = item.total_fijos;
            datarw[5] = item.total_prestamos;           
            datarw[6] = item.intcte;
            datarw[7] = item.intmora;
            datarw[8] = item.seguro;
            datarw[9] = item.otros;
            item.total = item.total_fijos + item.total_prestamos + item.intcte + item.intmora + item.seguro + item.otros;
            datarw[10] = item.total;
            datarw[11] = item.capital;
            datarw[12] = item.vacaciones;
            table.Rows.Add(datarw);

        }


       


        return table;
    }

    public DataTable CrearDataTableRecaudosNEW()
    {
        List<EmpresaNovedad> lstConsulta = new List<EmpresaNovedad>();
        lstConsulta = (List<EmpresaNovedad>)Session["DTDETALLENEW"];
        System.Data.DataTable table = new System.Data.DataTable();

        table.Columns.Add("TipoProducto");
        table.Columns.Add("Linea");
        table.Columns.Add("NumeroProducto");
        table.Columns.Add("CodigoCliente");
        table.Columns.Add("Identificacion");
        table.Columns.Add("Nombres");
        table.Columns.Add("Apellidos");
        table.Columns.Add("FechaNovedad");
        table.Columns.Add("Valor");
        table.Columns.Add("FechaFinNove");
        table.Columns.Add("TipoNovedad");
        table.Columns.Add("VrTotal");
        table.Columns.Add("NumCuotas");
        table.Columns.Add("Vacaciones");

        foreach (EmpresaNovedad item in lstConsulta)
        {
            DataRow datarw;
            datarw = table.NewRow();
            datarw[0] = item.nom_tipo_producto;
            if (txtcodGeneracion.Text != "")
                datarw[1] = item.linea;
            else
                datarw[1] = item.nom_linea;
            datarw[2] = item.numero_producto;
            datarw[3] = item.cod_persona;
            datarw[4] = item.identificacion;
            datarw[5] = item.nombres;
            datarw[6] = item.apellidos;
            

            if (item.fecha_inicial != null)
                datarw[7] = Convert.ToDateTime(item.fecha_inicial).ToShortDateString();
            else
                datarw[7] = " ";
            datarw[8] = item.valor.ToString("n");
            if (item.fecha_final != null)
                datarw[9] = Convert.ToDateTime(item.fecha_final).ToShortDateString();
            else
                datarw[9] = " ";
            datarw[10] = " " + item.tipo_novedad;
            datarw[11] = " " + item.valor_total.ToString("n");
            datarw[12] = " " + item.numero_cuotas;
            datarw[13] = item.vacaciones;
            table.Rows.Add(datarw);
        }
        return table;
    }

    public String vacios(String texto)
    {
        if (String.IsNullOrEmpty(texto))
        {
            return " ";
        }
        else
        {
            return texto;
        }
    }

    //Agregado
    protected void btnGenerarArchivos_Click(object sender, EventArgs e)
    {
        lblMensj.Text = "";
        txtNombreArchivo.Text = "";
        if (ddlEstructura.SelectedItem != null)
        {
            ddlEstructura.SelectedIndex = 0;
            ddlEstructura_SelectedIndexChanged(ddlEstructura, null);
        }
        else
        {
            lblMensj.Text = "La Empresa seleccionada no cuenta con Estructuras Asignadas";
        }
        mtvGeneral.Visible = false;
        mvVentEmergente.Visible = true;
        Site toolbar = (Site)Master;
        toolbar.MostrarGuardar(false);
        toolbar.MostrarImprimir(false);
        toolbar.MostrarExportar(false);
    }

    protected void btnCancelarEstructura_Click(object sender, EventArgs e)
    {
        mtvGeneral.Visible = true;
        mvVentEmergente.Visible = false;
        Site toolbar = (Site)Master;
        toolbar.MostrarGuardar(true);
        toolbar.MostrarImprimir(true);
        toolbar.MostrarExportar(true);
    }

    protected void ddlEstructura_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlEstructura.SelectedItem != null)
            {
                Xpinn.Tesoreria.Services.EstructuraRecaudoServices estructuraService = new Xpinn.Tesoreria.Services.EstructuraRecaudoServices();
                Estructura_Carga vRecaudos = new Estructura_Carga();
                vRecaudos.cod_estructura_carga = Convert.ToInt32(ddlEstructura.SelectedValue);
                vRecaudos = estructuraService.ConsultarEstructuraCarga(vRecaudos, (Usuario)Session["usuario"]);
                txtArchivo.Text = vRecaudos.tipo_archivo != 0 ? "Archivo de Texto" : "Archivo Excel";
            }
            else
            {
                txtArchivo.Text = "";
            }
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }

    void CargaDropEstructura(int cod_empresa)
    {
        List<EmpresaNovedad> lstData = new List<EmpresaNovedad>();
        lstData = Recaudos.ListarEstructuraXempresa(cod_empresa, (Usuario)Session["usuario"]);
        if (lstData.Count > 0)
        {
            ddlEstructura.DataSource = lstData;
            ddlEstructura.DataTextField = "nombre";
            ddlEstructura.DataValueField = "cod_estructura_carga";
            ddlEstructura.DataBind();
        }
        else
        {
            ddlEstructura.Items.Clear();
        }
    }

    protected void btnAceptarEstructura_Click(object sender, EventArgs e)
    {
        //VALIDAR DATOS NULOS
        lblMensj.Text = "";
        if (txtNombreArchivo.Text == "")
        {
            lblMensj.Text = "Ingrese el nombre del Archivo a descargar";
            return;
        }
        if (ddlEstructura.SelectedItem == null && ddlEstructura.SelectedItem.Text != "")
        {
            lblMensj.Text = "La Empresa seleccionada no cuenta con Estructuras Asignadas";
            return;
        }

        //CONSULTAR DATOS DE LA ESTRUCTURA
        EmpresaRecaudo_Programacion empProg = new EmpresaRecaudo_Programacion();
        empProg = (EmpresaRecaudo_Programacion)Session["empProg"];
        Xpinn.FabricaCreditos.Services.PeriodicidadService PeriodicidadServices = new Xpinn.FabricaCreditos.Services.PeriodicidadService();
        Xpinn.FabricaCreditos.Entities.Periodicidad Periodicidad = new Xpinn.FabricaCreditos.Entities.Periodicidad();
        Periodicidad = PeriodicidadServices.ConsultarPeriodicidad(Convert.ToInt32(empProg.cod_periodicidad), (Usuario)Session["usuario"]);
        EstructuraRecaudoServices EstructuraService = new EstructuraRecaudoServices();
        Estructura_Carga estructura = new Estructura_Carga();
        Estructura_Carga estructura_encabezado = new Estructura_Carga();
        estructura.cod_estructura_carga = Convert.ToInt32(ddlEstructura.SelectedValue);
        estructura = EstructuraService.ConsultarEstructuraCarga(estructura, (Usuario)Session["usuario"]);
        if (estructura == null)
        {
            lblMensj.Text = "La estructura selecciona no existe";
            return;
        }
        // Traer la estructura para el encabezado del archivo
        List<Estructura_Carga_Detalle> lstEstructuraDetalleEncabezado = new List<Estructura_Carga_Detalle>();
        if (estructura.cod_estructura != null)
        {
            estructura_encabezado.cod_estructura_carga = estructura.cod_estructura;
            estructura_encabezado = EstructuraService.ConsultarEstructuraCarga(estructura_encabezado, (Usuario)Session["usuario"]);

            //CARGAR EL DETALLE DE LA ESTRUCTURA ENCABEZADO            
            Estructura_Carga_Detalle vDeta = new Estructura_Carga_Detalle();
            if (estructura.cod_estructura_carga != null)
            {
                vDeta.cod_estructura_carga = estructura_encabezado.cod_estructura_carga;
                string pOrden = string.Empty;
                if (estructura_encabezado.tipo_datos == 0 || estructura_encabezado.tipo_archivo == 0)
                    pOrden = " ORDER BY NUMERO_COLUMNA ";
                else
                    pOrden = " ORDER BY COD_ESTRUCTURA_DETALLE ";
                lstEstructuraDetalleEncabezado = EstructuraService.ListarEstructuraDetalle(vDeta, pOrden, (Usuario)Session["usuario"]);
            }
            if (estructura_encabezado.separador_campo == "0")
                estructura_encabezado.separador_campo = "  ";
            if (estructura_encabezado.separador_campo == "1")
                estructura_encabezado.separador_campo = ",";
            if (estructura_encabezado.separador_campo == "2")
                estructura_encabezado.separador_campo = ";";
            if (estructura_encabezado.separador_campo == "3")
                estructura_encabezado.separador_campo = " ";

            if (estructura_encabezado.formato_fecha == "1")
                estructura_encabezado.formato_fecha = "dd/MM/yyyy";
            if (estructura_encabezado.formato_fecha == "2")
                estructura_encabezado.formato_fecha = "yyyy/MM/dd";
            if (estructura_encabezado.formato_fecha == "3")
                estructura_encabezado.formato_fecha = "MM/dd/yyyy";
            if (estructura_encabezado.formato_fecha == "4")
                estructura_encabezado.formato_fecha = "ddMMyyyy";
            if (estructura_encabezado.formato_fecha == "5")
                estructura_encabezado.formato_fecha = "yyyyMMdd";
            if (estructura_encabezado.formato_fecha == "6")
                estructura_encabezado.formato_fecha = "MMddyyyy";
        }

        //CARGAR EL DETALLE DE LA ESTRUCTURA
        List<Estructura_Carga_Detalle> lstEstructuraDetalle = new List<Estructura_Carga_Detalle>();
        if (estructura.cod_estructura_carga != null)
        {
            Estructura_Carga_Detalle vDeta = new Estructura_Carga_Detalle();
            vDeta.cod_estructura_carga = estructura.cod_estructura_carga;
            string pOrden = string.Empty;
            if (estructura.tipo_datos == 0 || estructura.tipo_archivo == 0)
                pOrden = " ORDER BY NUMERO_COLUMNA ";
            else
                pOrden = " ORDER BY COD_ESTRUCTURA_DETALLE ";
            lstEstructuraDetalle = EstructuraService.ListarEstructuraDetalle(vDeta, pOrden, (Usuario)Session["usuario"]);
        }
        if (estructura.separador_campo == "0")
            estructura.separador_campo = "  ";
        if (estructura.separador_campo == "1")
            estructura.separador_campo = ",";
        if (estructura.separador_campo == "2")
            estructura.separador_campo = ";";
        if (estructura.separador_campo == "3")
            estructura.separador_campo = " ";

        if (estructura.formato_fecha == "1")
            estructura.formato_fecha = "dd/MM/yyyy";
        if (estructura.formato_fecha == "2")
            estructura.formato_fecha = "yyyy/MM/dd";
        if (estructura.formato_fecha == "3")
            estructura.formato_fecha = "MM/dd/yyyy";
        if (estructura.formato_fecha == "4")
            estructura.formato_fecha = "ddMMyyyy";
        if (estructura.formato_fecha == "5")
            estructura.formato_fecha = "yyyyMMdd";
        if (estructura.formato_fecha == "6")
            estructura.formato_fecha = "MMddyyyy";

        List<EmpresaNovedad> lstDatos = new List<EmpresaNovedad>();

        //CONSULTAR SI LA EMPRESA MANEJA NOVEDADES NUEVAS O TODAS
        Xpinn.Tesoreria.Services.EmpresaRecaudoServices empresaServicio = new EmpresaRecaudoServices();
        Xpinn.Tesoreria.Entities.EmpresaRecaudo empresa = new EmpresaRecaudo();
        empresa = empresaServicio.ConsultarEMPRESARECAUDO(Convert.ToInt32(ddlEmpresa.SelectedValue), (Usuario)Session["Usuario"]);
        if (empresa != null)
        {
            // CARGAR LOS DATOS
            if (empresa.tipo_novedad == 1) // Novedades Nuevas
            {
                ActualizarNuevas();
                if (Session["DTDETALLENEW"] == null)
                {
                    lblMensj.Text = "No se han cargado datos de las novedades nuevas para generar";
                    return;
                }
                lstDatos = (List<EmpresaNovedad>)Session["DTDETALLENEW"];
            }
            else
            {
                Actualizar();
                if (Session["DTDETALLE"] == null)
                {
                    lblMensj.Text = "No se han cargado datos de las novedades para generar";
                    return;
                }
                lstDatos = (List<EmpresaNovedad>)Session["DTDETALLE"];
            }
        }

        // TOTALIZAR LOS DATOS SI SE REQUIERE
        List<string> lstConsulta = new List<string>();
        if (estructura.totalizar == 1)
        {
            #region Totalizar valores
            List<EmpresaNovedad> lstTotales = new List<EmpresaNovedad>();
            foreach (EmpresaNovedad rDatos in lstDatos)
            {
                EmpresaNovedad registro = new EmpresaNovedad();
                foreach (Estructura_Carga_Detalle rDetalle in lstEstructuraDetalle)
                {
                    //1=codigoCliente 2=Identificacion 3=Nombre y Apellidos 4=Linea 5=Valor 6=FechaEncabezado 7=NumProducto 8=Fec Prox Pago 9=Tipo Producto
                    //10 = Tipo Novedad 11=Fecha de Novedad 12=Fecha Inicio Novedad 13=Fecha Final Novedad 14=Fecha Radicación 15=Monto Total 16=Número Cuotas
                    //17=Código de la Ciudad 18=Direccion 19=Teléfono 20=E-mail 21=Periodo
                    if (rDetalle.codigo_campo == 1) // Código del cliente
                    {
                        registro.cod_cliente = rDatos.cod_cliente;
                    }
                    if (rDetalle.codigo_campo == 2) // Identificación
                    {
                        registro.identificacion = rDatos.identificacion;
                    }
                    if (rDetalle.codigo_campo == 3) 
                    {
                        registro.nombres = rDatos.nombres + " " + rDatos.apellidos;
                    }
                    // Nombres y apellidos

                    string[] Nombres;
                    string[] Apellidos;

                    Nombres = !string.IsNullOrEmpty(rDatos.nombres) ? rDatos.nombres.Split(' ') : null;
                    Apellidos = !string.IsNullOrEmpty(rDatos.apellidos) ? rDatos.apellidos.Split(' ') : null;
                    if (rDetalle.codigo_campo == 30)
                    {
                        registro.nombres1 = Nombres[0].Trim();
                    }
                    if (rDetalle.codigo_campo == 31 && Nombres.Length>1)
                    {
                        registro.nombres2 = Nombres[1].Trim();
                    }
                    if (rDetalle.codigo_campo == 32)
                    {
                        registro.apellidos1 =  Apellidos[0].Trim();
                    }
                    if (rDetalle.codigo_campo == 33 && Apellidos.Length>1)
                    {
                        registro.apellidos2 =  Apellidos[1].Trim();
                    }
                    if (rDetalle.codigo_campo == 4) // Concepto
                    {
                        Int64 prioridad = 0;
                        String concepto = "";
                        if (Recaudos.ConsultarConcepto(Convert.ToInt64(ddlEmpresa.SelectedValue), Convert.ToInt64(rDatos.tipo_producto), rDatos.linea, ref prioridad, ref concepto, (Usuario)Session["Usuario"]))
                        {
                            registro.concepto = concepto;
                        }
                        else
                        {
                            registro.concepto = "";
                        }
                    }
                    if (rDetalle.codigo_campo == 5) // Valor
                    {
                        registro.valor = rDatos.valor;
                    }
                    if (rDetalle.codigo_campo == 7) // Número de producto
                    {
                        registro.numero_producto = rDatos.numero_producto;
                    }
                    if (rDetalle.codigo_campo == 8) // Fecha
                    {
                        registro.fecha_proximo_pago = rDatos.fecha_proximo_pago;
                    }
                    if (rDetalle.codigo_campo == 9) // Tipo de producto
                    {
                        registro.tipo_producto = rDatos.nom_tipo_producto;
                    }
                    if (rDetalle.codigo_campo == 10) // Tipo Novedad
                    {
                        registro.tipo_novedad = rDatos.tipo_novedad;
                    }
                    if (rDetalle.codigo_campo == 11) // Fecha de Novedad
                    {
                        registro.fecha_novedad = rDatos.fecha_inicial;
                    }
                    if (rDetalle.codigo_campo == 12) // Fecha Inicio Novedad
                    {
                        string FechaPeriodo = ddlPeriodo.Visible == true ? ddlPeriodo.SelectedValue : txtfechaPeriodo.Text;
                        string FechaIni = Convert.ToDateTime(rDatos.fecha_inicial).ToShortDateString();
                        FechaIni = "01/" + Convert.ToDateTime(rDatos.fecha_inicial).Month.ToString("00") + "/" + Convert.ToDateTime(rDatos.fecha_inicial).Year;
                        if (rDatos.tipo_novedad == "I")
                            registro.fecha_inicial = ConvertirStringToDate(FechaIni);
                        else if (rDatos.tipo_novedad == "R" || rDatos.tipo_novedad == "A")
                            registro.fecha_inicial = ConvertirStringToDate(FechaPeriodo.Trim());
                    }
                    if (rDetalle.codigo_campo == 13) // Fecha Final Novedad
                    {
                        registro.fecha_final = rDatos.fecha_final;
                    }
                    if (rDetalle.codigo_campo == 14) // Fecha Radicacion
                    {
                        string FechaIni = Convert.ToDateTime(rDatos.fecha_inicial).ToShortDateString();
                        FechaIni = "01/" + Convert.ToDateTime(rDatos.fecha_inicial).Month.ToString("00") + "/" + Convert.ToDateTime(rDatos.fecha_inicial).Year;

                        if (rDatos.tipo_novedad == "I")
                            registro.fecha_radicacion = ConvertirStringToDate(FechaIni).AddMonths(-1);
                        else if (rDatos.tipo_novedad == "R" || rDatos.tipo_novedad == "A")
                            registro.fecha_radicacion = ConvertirStringToDate(FechaIni).AddMonths(-2);
                    }
                    if (rDetalle.codigo_campo == 15) // Monto Total
                    {
                        registro.valor_total = rDatos.valor_total;
                    }
                    if (rDetalle.codigo_campo == 16) // Numero Cuotas
                    {
                        registro.numero_cuotas = rDatos.numero_cuotas;
                    }
                    if (rDetalle.codigo_campo == 17) // Codigo de la Ciudad
                    {
                        registro.codciudad = rDatos.codciudad;
                    }
                    if (rDetalle.codigo_campo == 18) // Direccion
                    {
                        registro.direccion = rDatos.direccion;
                    }
                    if (rDetalle.codigo_campo == 19) // Telefono
                    {
                        registro.telefono = rDatos.telefono;
                    }
                    if (rDetalle.codigo_campo == 20) // Email
                    {
                        registro.email = rDatos.email;
                    }
                    if (rDetalle.codigo_campo == 21) // Periodo
                    {
                        if (ddlPeriodo.Visible == true)
                            registro.periodo_dscto = ConvertirStringToDate(ddlPeriodo.SelectedValue).Month + "-" + Convert.ToDateTime(ddlPeriodo.SelectedValue).Year;
                        else
                            registro.periodo_dscto = ConvertirStringToDate(txtfechaPeriodo.Text).Month + "-" + Convert.ToDateTime(txtfechaPeriodo.Text).Year;
                    }
                    if (rDetalle.codigo_campo == 22) // CAMPO FIJO
                    {
                        registro.vr_campo_fijo = rDetalle.vr_campo_fijo;
                    }
                    if (rDetalle.codigo_campo == 23)
                    {
                        registro.cod_nomina_empleado = rDatos.cod_nomina_empleado;
                    }
                    if (rDetalle.codigo_campo == 24) // Numero de Planilla
                    {
                        registro.numero_planilla = lblnum_planilla.Text;
                    }
                    if (rDetalle.codigo_campo == 25) // Periodicidad
                    {
                        registro.periodicidad = Periodicidad.Descripcion;
                    }
                    if (rDetalle.codigo_campo == 26) // Saldo
                    {
                        if (registro.tipo_producto == "10" || registro.tipo_producto == "CREDITOS - CUOTAS EXTRAS") 
                            // Las cuotas extras se colocan en saldo en cero para que no sume doble el saldo del crédito
                            registro.saldo = 0;
                        else
                            registro.saldo = rDatos.saldo;
                    }
                    if (rDetalle.codigo_campo == 27) // Total
                    {
                        registro.total = rDatos.total;
                    }
                    if (rDetalle.codigo_campo == 28) // Saldo Total
                    {
                        registro.total_saldo = rDatos.total_saldo;
                    }
                    if (rDetalle.codigo_campo == 35) // Capital
                    {
                        registro.capital = rDatos.capital;
                    }
                    if (rDetalle.codigo_campo == 36) // Interes Cte
                    {
                        registro.intcte = rDatos.intcte;
                    }
                    if (rDetalle.codigo_campo == 37) // Interes Mora
                    {
                        registro.intmora = rDatos.intmora;
                    }
                    if (rDetalle.codigo_campo == 38) // Valor Seguro
                    {
                        registro.seguro = rDatos.seguro;
                    }
                    if (rDetalle.codigo_campo == 39) // Otros Atributos
                    {
                        registro.otros = rDatos.otros;
                    }

                    if (rDetalle.codigo_campo == 40) // Total Fijos
                    {
                        registro.total_fijos = rDatos.total_fijos;
                    }
                    if (rDetalle.codigo_campo == 41) // Total Prestamos 
                    {
                        registro.total_prestamos = rDatos.total_prestamos;
                    }
                    if (rDetalle.codigo_campo == 41) // Total Prestamos 
                    {
                        registro.total_prestamos = rDatos.total_prestamos;
                    }
                    if (rDetalle.codigo_campo == 42) // Fecha Inicio Producto
                    {
                        registro.fecha_inicio_producto = rDatos.fecha_inicio_producto;
                    }
                    if (rDetalle.codigo_campo == 43) // Fecha Vencimiento Producto
                    {
                        registro.fecha_vencimiento_producto = rDatos.fecha_vencimiento_producto;
                    }
                }
                lstTotales.Add(registro);
            }
            #endregion

            var objTotales = (from reg in lstTotales
                              group reg by new
                              {
                                  cod_cliente = reg.cod_cliente,
                                  identificacion = reg.identificacion,
                                  nombres = reg.nombres,
                                  apellidos = reg.apellidos,
                                  nombres1 = reg.nombres1,
                                  nombres2 = reg.nombres2,
                                  apellidos1 = reg.apellidos1,
                                  apellidos2 = reg.apellidos2,
                                  concepto = reg.concepto,
                                  numero_producto = reg.numero_producto,
                                  fecha_proximo_pago = reg.fecha_proximo_pago,
                                  tipo_producto = reg.tipo_producto,
                                  nomtipo_producto = reg.nom_tipo_producto,
                                  linea = reg.linea,
                                  cod_nomina_empleado = reg.cod_nomina_empleado,
                                  fecha_inicio_producto = reg.fecha_inicio_producto,
                                  fecha_vencimiento_producto = reg.fecha_vencimiento_producto
                              } into grupoPedido
                              select new
                              {
                                  grupoPedido.Key,
                                  valor = grupoPedido.Sum(p => p.valor),
                                  saldo = grupoPedido.Sum(p => p.saldo),
                                  totalprestamos = grupoPedido.Sum(p => p.total_prestamos),
                                  totalfijos = grupoPedido.Sum(p => p.total_fijos),                                
                                  mora = grupoPedido.Sum(p => p.intmora),
                                  interes = grupoPedido.Sum(p => p.intcte),
                                  seguros = grupoPedido.Sum(p => p.seguro),
                                  otros = grupoPedido.Sum(p => p.otros)
                              });

            EmpresaNovedad novedadEncabezado = new EmpresaNovedad();
            novedadEncabezado.periodicidad = Periodicidad.Descripcion;
            novedadEncabezado.saldo = lstTotales.Sum(x => x.saldo);
            novedadEncabezado.total = lstTotales.Sum(x => x.valor);
            novedadEncabezado.total_fijos = lstTotales.Sum(x => x.total_fijos);
            novedadEncabezado.total_prestamos = lstTotales.Sum(x => x.total_prestamos);
            novedadEncabezado.intcte = lstTotales.Sum(x => x.intcte);
            novedadEncabezado.intmora = lstTotales.Sum(x => x.intmora);
            novedadEncabezado.seguro = lstTotales.Sum(x => x.seguro);
            novedadEncabezado.otros  = lstTotales.Sum(x => x.otros);

            string LineaEncabezado = "";
            if (estructura_encabezado != null)
            {
                if (lstEstructuraDetalleEncabezado.Count > 0)
                {
                    LineaEncabezado = DatosArchivo(novedadEncabezado, estructura_encabezado, lstEstructuraDetalleEncabezado, empresa);
                    LineaEncabezado = LineaEncabezado.Substring(0, LineaEncabezado.Length - 1);
                    lstConsulta.Add(LineaEncabezado);
                }
            }

            //ARCHIVO DE TEXTO
            List<EmpresaNovedad> lstInfoData = new List<EmpresaNovedad>();
            foreach (var novedadObj in objTotales)
            {

                EmpresaNovedad novedadEnt = new EmpresaNovedad();
                novedadEnt.cod_cliente = novedadObj.Key.cod_cliente;
                novedadEnt.identificacion = novedadObj.Key.identificacion;
                novedadEnt.nombres = novedadObj.Key.nombres;
                novedadEnt.apellidos = novedadObj.Key.apellidos;
                novedadEnt.nombres1 = novedadObj.Key.nombres1;
                novedadEnt.nombres2 = novedadObj.Key.nombres2;
                novedadEnt.apellidos1 = novedadObj.Key.apellidos1;
                novedadEnt.apellidos2 = novedadObj.Key.apellidos2;
                novedadEnt.concepto = novedadObj.Key.concepto;
                novedadEnt.numero_producto = novedadObj.Key.numero_producto;
                novedadEnt.fecha_proximo_pago = novedadObj.Key.fecha_proximo_pago;
                novedadEnt.tipo_producto = novedadObj.Key.tipo_producto;
                novedadEnt.nom_tipo_producto = novedadObj.Key.nomtipo_producto;
                novedadEnt.linea = novedadObj.Key.linea;
                novedadEnt.intcte = novedadObj.interes;
                novedadEnt.intmora = novedadObj.mora;
                novedadEnt.total_prestamos = novedadObj.totalprestamos;
                novedadEnt.total_fijos = novedadObj.totalfijos;
                novedadEnt.otros = novedadObj.otros;
                novedadEnt.seguro = novedadObj.seguros;
                novedadEnt.valor = novedadObj.valor;
                novedadEnt.saldo = novedadObj.saldo;
                novedadEnt.cod_nomina_empleado = novedadObj.Key.cod_nomina_empleado;
                novedadEnt.fecha_inicio_producto = novedadObj.Key.fecha_inicio_producto;
                novedadEnt.fecha_vencimiento_producto = novedadObj.Key.fecha_vencimiento_producto;

                string Linea = "";
                Linea = DatosArchivo(novedadEnt, estructura, lstEstructuraDetalle, empresa);
                string NewLinea = "";
                if (Linea.Length > 0)
                { 
                    // NewLinea = Linea.Substring(0, Linea.Length - 1);
                    if (estructura.tipo_datos == 0)
                        NewLinea = Linea.Substring(0, Linea.Length - 1);
                    else
                        NewLinea = Linea;
                }
                else
                {
                    lblMensj.Text = "No se Adicionaron Registros. Verifique los Datos Seleccionados";
                    return;
                }
                lstInfoData.Add(novedadEnt);
                lstConsulta.Add(NewLinea);
            }
           
            decimal pSaldoFinal = lstInfoData.Sum(x => x.saldo);
            if (novedadEncabezado.saldo != pSaldoFinal)
            {
                novedadEncabezado.saldo = pSaldoFinal;
                LineaEncabezado = "";
                if (estructura_encabezado != null)
                {
                    if (lstEstructuraDetalleEncabezado.Count > 0)
                    {
                        LineaEncabezado = DatosArchivo(novedadEncabezado, estructura_encabezado, lstEstructuraDetalleEncabezado, empresa);
                        if (LineaEncabezado != "")
                            lstConsulta[0] = LineaEncabezado;
                    }
                }
            }
        }
        else
        {
            EmpresaNovedad novedadEncabezado = new EmpresaNovedad();
            novedadEncabezado.periodicidad = Periodicidad.Descripcion;
            novedadEncabezado.saldo = lstDatos.Sum(x => x.saldo);
            novedadEncabezado.total = lstDatos.Sum(x => x.valor);
            string LineaEncabezado = "";
            if (estructura_encabezado != null)
            {
                if (lstEstructuraDetalleEncabezado.Count > 0)
                {
                    LineaEncabezado = DatosArchivo(novedadEncabezado, estructura_encabezado, lstEstructuraDetalleEncabezado, empresa);
                    lstConsulta.Add(LineaEncabezado);
                }
            }

            foreach (EmpresaNovedad novedadEnt in lstDatos)
            {
                Int64 prioridad = 0;
                String concepto = "";

                novedadEnt.tipo_producto = novedadEnt.tipo_producto ==  "" ? "0" : novedadEnt.tipo_producto;
                if (Recaudos.ConsultarConcepto(Convert.ToInt64(ddlEmpresa.SelectedValue), Convert.ToInt64(novedadEnt.tipo_producto), novedadEnt.linea, ref prioridad, ref concepto, (Usuario)Session["Usuario"]))
                {
                    novedadEnt.concepto = concepto;
                }
                string Linea = "";
                Linea = DatosArchivo(novedadEnt, estructura, lstEstructuraDetalle, empresa);
                string NewLinea = "";
                if (Linea.Length > 0)
                {
                    if (estructura.tipo_datos == 0)
                        NewLinea = Linea.Substring(0, Linea.Length - 1);
                    else
                        NewLinea = Linea;
                }
                else
                {
                    lblMensj.Text = "No se Adicionaron Registros. Verifique los Datos Seleccionados";
                    return;
                }
                lstConsulta.Add(NewLinea);
            }
            
            decimal pSaldoFinal = lstDatos.Sum(x => x.saldo);
            if (novedadEncabezado.saldo != pSaldoFinal)
            {
                novedadEncabezado.saldo = pSaldoFinal;
                LineaEncabezado = "";
                if (estructura_encabezado != null)
                {
                    if (lstEstructuraDetalleEncabezado.Count > 0)
                    {
                        LineaEncabezado = DatosArchivo(novedadEncabezado, estructura_encabezado, lstEstructuraDetalleEncabezado, empresa);
                        if (LineaEncabezado != "")
                            lstConsulta[0] = LineaEncabezado;
                    }
                }
            }
        }

        if (estructura.tipo_archivo == 0)
        {
            //ARCHIVO EXCEL
            /*DataGrid dg = new DataGrid();
             dg.AllowPaging = false;
             dg.DataSource = lstConsulta;
             dg.DataBind();

             System.Web.HttpContext.Current.Response.Clear();
             System.Web.HttpContext.Current.Response.Buffer = true;
             System.Web.HttpContext.Current.Response.ContentEncoding = Encoding.UTF8;
             System.Web.HttpContext.Current.Response.Charset = "";
             System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition",
               "attachment; filename=" + txtNombreArchivo.Text + (txtNombreArchivo.Text.ToUpper().Contains(".XLS") ? "": ".xlsx"));
             System.Web.HttpContext.Current.Response.ContentType =
               "application/vnd.ms-excel";
             System.IO.StringWriter stringWriter = new System.IO.StringWriter();
             System.Web.UI.HtmlTextWriter htmlTextWriter =
               new System.Web.UI.HtmlTextWriter(stringWriter);
             dg.RenderControl(htmlTextWriter);
             System.Web.HttpContext.Current.Response.Write(stringWriter.ToString());
             System.Web.HttpContext.Current.Response.End();
           */

            string texto = "", fic = "";
            if (txtNombreArchivo.Text != "")
                if (txtNombreArchivo.Text.ToLower().Contains(".xls") == true || txtNombreArchivo.Text.ToLower().Contains(".xlsx") == true)
                    fic = txtNombreArchivo.Text;
                else
                    fic = txtNombreArchivo.Text + ".xls";
            if (File.Exists(Server.MapPath("Archivos\\") + fic))
                File.Delete(Server.MapPath("Archivos\\") + fic);
            try
            {
                //Guarda los Datos a la ruta especificada
                foreach (string item in lstConsulta)
                {
                    texto = item;
                    System.IO.StreamWriter sw = new System.IO.StreamWriter(Server.MapPath("Archivos\\") + fic, true);

                   

                    sw.WriteLine(texto);
                    sw.Close();
                }
            }
            catch (Exception ex)
            {
                lblMensj.Text = ex.Message;
                return;
            }

            if (lstConsulta.Count > 0 && File.Exists(Server.MapPath("Archivos\\") + fic))
            {
                System.IO.StreamReader sr;
                sr = File.OpenText(Server.MapPath("Archivos\\") + fic);
                texto = sr.ReadToEnd();
                sr.Close();
                HttpContext.Current.Response.ClearContent();
                HttpContext.Current.Response.ClearHeaders();
                HttpContext.Current.Response.ContentType = "text/plain";
                Response.HeaderEncoding = System.Text.Encoding.Default;
                Response.ContentEncoding = Encoding.Default;



                HttpContext.Current.Response.Write(texto);
                HttpContext.Current.Response.AppendHeader("Content-disposition", "attachment;filename=" + fic);
                HttpContext.Current.Response.Flush();
                File.Delete(Server.MapPath("Archivos\\") + fic);
                HttpContext.Current.Response.End();
            }
            else
            {
                lblMensj.Text = "No se genero el archivo, Verifique los Datos";
            
        }


    }
        else
        {
            //GENERAR EL ARCHIVO PLANO
            string texto = "", fic = "";
            if (txtNombreArchivo.Text != "")
                if (txtNombreArchivo.Text.ToLower().Contains(".txt") == true)
                    fic = txtNombreArchivo.Text;
                else
                    fic = txtNombreArchivo.Text + ".txt";
            if (File.Exists(Server.MapPath("Archivos\\") + fic))
                File.Delete(Server.MapPath("Archivos\\") + fic);
            try
            {
                //Guarda los Datos a la ruta especificada
                foreach (string item in lstConsulta)
                {
                    texto = item;
                    System.IO.StreamWriter sw = new System.IO.StreamWriter(Server.MapPath("Archivos\\") + fic, true);
                    sw.WriteLine(texto);
                    sw.Close();
                }
            }
            catch (Exception ex)
            {
                lblMensj.Text = ex.Message;
                return;
            }

            if (lstConsulta.Count > 0 && File.Exists(Server.MapPath("Archivos\\") + fic))
            {
                System.IO.StreamReader sr;
                sr = File.OpenText(Server.MapPath("Archivos\\") + fic);
                texto = sr.ReadToEnd();
                sr.Close();
                HttpContext.Current.Response.ClearContent();
                HttpContext.Current.Response.ClearHeaders();
                HttpContext.Current.Response.ContentType = "text/plain";
                HttpContext.Current.Response.Write(texto);
                HttpContext.Current.Response.AppendHeader("Content-disposition", "attachment;filename=" + fic);
                HttpContext.Current.Response.Flush();
                File.Delete(Server.MapPath("Archivos\\") + fic);
                HttpContext.Current.Response.End();
            }
            else
            {
                lblMensj.Text = "No se genero el archivo, Verifique los Datos";
            }
        }
    }


    private string DatosArchivo(EmpresaNovedad novedadObj, Estructura_Carga estructura, List<Estructura_Carga_Detalle> lstEstructuraDetalle, EmpresaRecaudo empresa)
    {
        //ARCHIVO DE TEXTO
        string Linea = "";
        foreach (Estructura_Carga_Detalle Detalle in lstEstructuraDetalle)
        {
            //1=codigoCliente 2=Identificacion 3=Nombre y Apellidos 4=Linea 5=Valor 6=FechaEncabezado 7=NumProducto 8=Fec Prox Pago 9=Tipo Producto
            //10 = Tipo Novedad 11=Fecha de Novedad 12=Fecha Inicio Novedad 13=Fecha Final Novedad 14=Fecha Radicación 15=Monto Total 16=Número Cuotas
            //17=Código de la Ciudad 18=Direccion 19=Teléfono 20=E-mail 21=Periodo                                                                     
            if (Detalle.codigo_campo == 1) // Código del cliente
            {
                if (Detalle.justificador != null)
                    Linea += AjustarCampos(novedadObj.cod_cliente.ToString(), Convert.ToChar(Detalle.justificador), Detalle.justificacion, Detalle.longitud, Detalle.posicion_inicial) + estructura.separador_campo;
                else
                    Linea += novedadObj.cod_cliente.ToString() + estructura.separador_campo;
            }
            if (Detalle.codigo_campo == 2) // Identificación
            {
                if (Detalle.justificador != null)
                    Linea += AjustarCampos(novedadObj.identificacion, Convert.ToChar(Detalle.justificador), Detalle.justificacion, Detalle.longitud, Detalle.posicion_inicial) + estructura.separador_campo;
                else
                    Linea += novedadObj.identificacion + estructura.separador_campo;
            }
            // Nombres y apellidos
            string[] Nombres;
            string[] Apellidos;

            Nombres = !string.IsNullOrEmpty(novedadObj.nombres) ? novedadObj.nombres.Split(' ') : null;
            try
            {
                if (Nombres == null)
                {
                    Nombres = new String[2];
                    Nombres[0] = novedadObj.nombres1 == null ? "": novedadObj.nombres1;
                    Nombres[1] = novedadObj.nombres2 == null ? "" : novedadObj.nombres2;
                }
            }
            catch { }
            Apellidos = !string.IsNullOrEmpty(novedadObj.apellidos) ? novedadObj.apellidos.Split(' ') : null;
            try
            {
                if (Apellidos == null)
                {
                    Apellidos = new String[2];
                    Apellidos[0] = novedadObj.apellidos1 == null ? "" : novedadObj.apellidos1;
                    Apellidos[1] = novedadObj.apellidos2 == null ? "" : novedadObj.apellidos2;
                }
            }
            catch { }
            if (Detalle.codigo_campo == 3)
            {
                if (Detalle.justificador != null)
                    Linea += AjustarCampos(novedadObj.nombres + ", " + novedadObj.apellidos, Convert.ToChar(Detalle.justificador), Detalle.justificacion, Detalle.longitud, Detalle.posicion_inicial) + estructura.separador_campo;
                else
                    Linea += novedadObj.nombres + " " + novedadObj.apellidos + estructura.separador_campo;
            }
            if (Detalle.codigo_campo == 30)
            {

                if (Detalle.justificador != null)
                    Linea += AjustarCampos(Nombres[0].Trim(), Convert.ToChar(Detalle.justificador)) + estructura.separador_campo;
                else
                    Linea += Nombres[0].Trim() + estructura.separador_campo;
            }
            if (Detalle.codigo_campo == 31 && Nombres.Length > 1)
            {
                if (Detalle.justificador != null)
                    Linea += AjustarCampos(Nombres[1].Trim(), Convert.ToChar(Detalle.justificador)) + estructura.separador_campo;
                else
                    Linea += Nombres[1].Trim() + estructura.separador_campo;
            }
            if (Detalle.codigo_campo == 32)
            {
                if (Detalle.justificador != null)
                    Linea += AjustarCampos(Apellidos[0].Trim(), Convert.ToChar(Detalle.justificador)) + estructura.separador_campo;
                else
                    Linea += Apellidos[0].Trim() + estructura.separador_campo;
            }
            if (Detalle.codigo_campo == 33 && Apellidos.Length > 1)
            {
                if (Detalle.justificador != null)
                    Linea += AjustarCampos(Apellidos[1].Trim(), Convert.ToChar(Detalle.justificador)) + estructura.separador_campo;
                else
                    Linea += Apellidos[1].Trim() + estructura.separador_campo;
            }
            if (Detalle.codigo_campo == 4) // Concepto
            {
                if (Detalle.justificador != null)
                    Linea += AjustarCampos(novedadObj.concepto, Convert.ToChar(Detalle.justificador), Detalle.justificacion, Detalle.longitud, Detalle.posicion_inicial) + estructura.separador_campo;
                else
                    Linea += novedadObj.concepto + estructura.separador_campo;
            }
            if (Detalle.codigo_campo == 5) // Valor
            {
                string lValor = novedadObj.valor.ToString();
                lValor = Reemplazar(lValor, Convert.ToChar(estructura.separador_miles), Convert.ToChar(estructura.separador_decimal));
                if (Detalle.justificador != null)
                    Linea += AjustarCampos(lValor.Replace("$", ""), Convert.ToChar(Detalle.justificador), Detalle.justificacion, Detalle.longitud, Detalle.posicion_inicial) + estructura.separador_campo;
                else
                    Linea += lValor.Replace("$", "") + estructura.separador_campo;
            }
            if (Detalle.codigo_campo == 6) // fecha
            {
                string sformato_fecha = estructura.formato_fecha;
                if (Detalle.justificador != null)
                    Linea += AjustarCampos(Convert.ToDateTime(ddlPeriodo.SelectedValue).ToString(sformato_fecha), Convert.ToChar(Detalle.justificador), Detalle.justificacion, Detalle.longitud, Detalle.posicion_inicial) + estructura.separador_campo;
                else
                    Linea += Convert.ToDateTime(ddlPeriodo.SelectedValue).ToString(sformato_fecha) + estructura.separador_campo;
            }
            if (Detalle.codigo_campo == 7) // Número de producto
            {
                // Fecem molesta y dice que para los aportes no necesita el numero del producto, que debe estar en 0
                if (novedadObj.tipo_producto != null)
                {
                    TipoDeProducto tipoProducto = novedadObj.tipo_producto.ToEnum<TipoDeProducto>();
                    if (tipoProducto == TipoDeProducto.Aporte && ConsultarParametroGeneral(29).valor == "1")
                    {
                        novedadObj.numero_producto = "00000";
                    }
                }

                if (Detalle.justificador != null)
                    Linea += AjustarCampos(novedadObj.numero_producto, Convert.ToChar(Detalle.justificador), Detalle.justificacion, Detalle.longitud, Detalle.posicion_inicial) + estructura.separador_campo;
                else
                    Linea += novedadObj.numero_producto + estructura.separador_campo;
            }
            if (Detalle.codigo_campo == 8) // Fecha
            {
                if (novedadObj.fecha_proximo_pago.ToString() != "")
                {
                    string sformato_fecha = estructura.formato_fecha;
                    if (Detalle.justificador != null)
                        Linea += DateTime.ParseExact(AjustarCampos(novedadObj.fecha_proximo_pago.ToString(), Convert.ToChar(Detalle.justificador)), sformato_fecha, null) + estructura.separador_campo;
                    else
                        Linea += DateTime.ParseExact(novedadObj.fecha_proximo_pago.ToString(), sformato_fecha, null) + estructura.separador_campo;
                }
                else
                {
                    if (Detalle.justificador != null)
                        Linea += AjustarCampos("", Convert.ToChar(Detalle.justificador), Detalle.justificacion, Detalle.longitud, Detalle.posicion_inicial) + estructura.separador_campo;
                    else
                        Linea += "" + estructura.separador_campo;
                }
            }
            if (Detalle.codigo_campo == 9) // Tipo de producto
            {
                if (Detalle.justificador != null)
                    Linea += AjustarCampos(novedadObj.nom_tipo_producto, Convert.ToChar(Detalle.justificador), Detalle.justificacion, Detalle.longitud, Detalle.posicion_inicial) + estructura.separador_campo;
                else
                    Linea += novedadObj.nom_tipo_producto + estructura.separador_campo;
            }
            if (Detalle.codigo_campo == 10) // Tipo Novedad
            {
                if (Detalle.justificador != null)
                    Linea += AjustarCampos(novedadObj.tipo_novedad, Convert.ToChar(Detalle.justificador), Detalle.justificacion, Detalle.longitud, Detalle.posicion_inicial) + estructura.separador_campo;
                else
                    Linea += novedadObj.tipo_novedad + estructura.separador_campo;
            }
            if (Detalle.codigo_campo == 11) // Fecha de Novedad
            {
                string sformato_fecha = estructura.formato_fecha;
                DateTime Fecha = novedadObj.fecha_inicial != null ? Convert.ToDateTime(novedadObj.fecha_inicial) : DateTime.MinValue;
                string pFec = Fecha.ToString(sformato_fecha);
                Linea += pFec + estructura.separador_campo;
            }
            if (Detalle.codigo_campo == 12) // Fecha Inicio Novedad
            {
                string sformato_fecha = estructura.formato_fecha;
                DateTime FechaPeriodo = ddlPeriodo.Visible == true ? ConvertirStringToDate(ddlPeriodo.SelectedValue) : ConvertirStringToDate(txtfechaPeriodo.Text);
                string FechaIni = "", pFec = "";

                if (String.IsNullOrEmpty(novedadObj.fecha_inicial.ToString())) novedadObj.fecha_inicial = FechaPeriodo;

                FechaIni = "01/" + Convert.ToDateTime(novedadObj.fecha_inicial).Month.ToString("00") + "/" + Convert.ToDateTime(novedadObj.fecha_inicial).Year;
                DateTime pFechaIni = Convert.ToDateTime(FechaIni);
                if (novedadObj.tipo_novedad == "I")
                {
                    if (Detalle.justificador != null)
                        pFec = AjustarCampos(pFechaIni.ToString(sformato_fecha), Convert.ToChar(Detalle.justificador), Detalle.justificacion, Detalle.longitud, Detalle.posicion_inicial);
                    else
                        pFec = pFechaIni.ToString(sformato_fecha);
                }
                else if (novedadObj.tipo_novedad == "R" || novedadObj.tipo_novedad == "A")
                {
                    if (Detalle.justificador != null)
                        pFec = AjustarCampos(FechaPeriodo.ToString(sformato_fecha), Convert.ToChar(Detalle.justificador), Detalle.justificacion, Detalle.longitud, Detalle.posicion_inicial);
                    else
                        pFec = FechaPeriodo.ToString(sformato_fecha);
                }

                else 
                {
                    if (Detalle.justificador != null)
                        pFec = AjustarCampos(pFechaIni.ToString(sformato_fecha), Convert.ToChar(Detalle.justificador), Detalle.justificacion, Detalle.longitud, Detalle.posicion_inicial);
                    else
                        pFec = pFechaIni.ToString(sformato_fecha);
                }

                Linea += pFec + estructura.separador_campo;
            }
            if (Detalle.codigo_campo == 13) // Fecha Final Novedad
            {
                string sformato_fecha = estructura.formato_fecha;
                DateTime Fecha = novedadObj.fecha_final != null ? Convert.ToDateTime(novedadObj.fecha_final) : DateTime.MinValue;
                string pFec = "";
                if (Detalle.justificador != null)
                    pFec = AjustarCampos(Fecha.ToString(sformato_fecha), Convert.ToChar(Detalle.justificador), Detalle.justificacion, Detalle.longitud, Detalle.posicion_inicial);
                else
                    pFec = Fecha.ToString(sformato_fecha);
                Linea += pFec + estructura.separador_campo;
            }
            if (Detalle.codigo_campo == 14) // Fecha Radicacion
            {
                string sformato_fecha = estructura.formato_fecha;
                string FechaIni = "", pFec = "";
                FechaIni = "01/" + Convert.ToDateTime(novedadObj.fecha_inicial).Month.ToString("00") + "/" + Convert.ToDateTime(novedadObj.fecha_inicial).Year;
                DateTime Fecha = Convert.ToDateTime(FechaIni);
                if (novedadObj.tipo_novedad == "I")
                {
                    if (Detalle.justificador != null)
                        pFec = AjustarCampos(Fecha.AddMonths(-1).ToString(sformato_fecha), Convert.ToChar(Detalle.justificador), Detalle.justificacion, Detalle.longitud, Detalle.posicion_inicial);
                    else
                        pFec = Fecha.AddMonths(-1).ToString(sformato_fecha);
                }
                else if (novedadObj.tipo_novedad == "R" || novedadObj.tipo_novedad == "A")
                {
                    if (Detalle.justificador != null)
                        pFec = AjustarCampos(Fecha.AddMonths(-2).ToString(sformato_fecha), Convert.ToChar(Detalle.justificador), Detalle.justificacion, Detalle.longitud, Detalle.posicion_inicial);
                    else
                        pFec = Fecha.AddMonths(-2).ToString(sformato_fecha);
                }
                Linea += pFec + estructura.separador_campo;
            }
            if (Detalle.codigo_campo == 15) // Monto Total
            {
                string lValor = novedadObj.valor_total.ToString();
                lValor = Reemplazar(lValor, Convert.ToChar(estructura.separador_miles), Convert.ToChar(estructura.separador_decimal));
                if (Detalle.justificador != null)
                    Linea += AjustarCampos(lValor.Replace("$", ""), Convert.ToChar(Detalle.justificador), Detalle.justificacion, Detalle.longitud, Detalle.posicion_inicial) + estructura.separador_campo;
                else
                    Linea += lValor.Replace("$", "") + estructura.separador_campo;
            }
            if (Detalle.codigo_campo == 16) // Numero Cuotas
            {
                if (Detalle.justificador != null)
                    Linea += AjustarCampos(novedadObj.numero_cuotas.ToString(), Convert.ToChar(Detalle.justificador), Detalle.justificacion, Detalle.longitud, Detalle.posicion_inicial) + estructura.separador_campo;
                else
                    Linea += novedadObj.numero_cuotas.ToString() + estructura.separador_campo;
            }
            if (Detalle.codigo_campo == 17) // Codigo de la Ciudad
            {
                if (Detalle.justificador != null)
                    Linea += AjustarCampos(novedadObj.codciudad.ToString(), Convert.ToChar(Detalle.justificador), Detalle.justificacion, Detalle.longitud, Detalle.posicion_inicial) + estructura.separador_campo;
                else
                    Linea += novedadObj.codciudad.ToString() + estructura.separador_campo;
            }
            if (Detalle.codigo_campo == 18) // Direccion
            {
                if (Detalle.justificador != null)
                    Linea += AjustarCampos(novedadObj.direccion, Convert.ToChar(Detalle.justificador), Detalle.justificacion, Detalle.longitud, Detalle.posicion_inicial) + estructura.separador_campo;
                else
                    Linea += novedadObj.direccion + estructura.separador_campo;
            }
            if (Detalle.codigo_campo == 19) // Telefono
            {
                if (Detalle.justificador != null)
                    Linea += AjustarCampos(novedadObj.telefono, Convert.ToChar(Detalle.justificador), Detalle.justificacion, Detalle.longitud, Detalle.posicion_inicial) + estructura.separador_campo;
                else
                    Linea += novedadObj.telefono + estructura.separador_campo;
            }
            if (Detalle.codigo_campo == 20) // Email
            {
                if (Detalle.justificador != null)
                    Linea += AjustarCampos(novedadObj.email, Convert.ToChar(Detalle.justificador), Detalle.justificacion, Detalle.longitud, Detalle.posicion_inicial) + estructura.separador_campo;
                else
                    Linea += novedadObj.email + estructura.separador_campo;
            }
            if (Detalle.codigo_campo == 21) // Periodo
            {
                string periodo = ddlPeriodo.Visible == true ? Convert.ToDateTime(ddlPeriodo.SelectedValue).Month.ToString("00") + "-" + Convert.ToDateTime(ddlPeriodo.SelectedValue).Year
                    : Convert.ToDateTime(txtfechaPeriodo.Text).Month.ToString("00") + "-" + Convert.ToDateTime(txtfechaPeriodo.Text).Year;

                if (Detalle.justificador != null)
                    Linea += AjustarCampos(periodo, Convert.ToChar(Detalle.justificador)) + estructura.separador_campo;
                else
                    Linea += periodo + estructura.separador_campo;
            }
            if (Detalle.codigo_campo == 22) // CAMPO FIJO
            {
                if (Detalle.justificador != null)
                    Linea += AjustarCampos(Detalle.vr_campo_fijo, Convert.ToChar(Detalle.justificador), Detalle.justificacion, Detalle.longitud, Detalle.posicion_inicial) + estructura.separador_campo;
                else
                    Linea += Detalle.vr_campo_fijo + estructura.separador_campo;
            }
            if (Detalle.codigo_campo == 23) // Codigo Nomina
            {
                if (Detalle.justificador != null)
                    Linea += AjustarCampos(novedadObj.cod_nomina_empleado, Convert.ToChar(Detalle.justificador), Detalle.justificacion, Detalle.longitud, Detalle.posicion_inicial) + estructura.separador_campo;
                else
                    Linea += novedadObj.cod_nomina_empleado + estructura.separador_campo;
            }
            if (Detalle.codigo_campo == 24) // Numero de Planilla
            {
                if (Detalle.justificador != null)
                    Linea += AjustarCampos(lblnum_planilla.Text, Convert.ToChar(Detalle.justificador), Detalle.justificacion, Detalle.longitud, Detalle.posicion_inicial) + estructura.separador_campo;
                else
                    Linea += lblnum_planilla.Text + estructura.separador_campo;
            }
            if (Detalle.codigo_campo == 25) // Periodicidad
            {
                if (Detalle.justificador != null)
                    Linea += AjustarCampos(novedadObj.periodicidad, Convert.ToChar(Detalle.justificador), Detalle.justificacion, Detalle.longitud, Detalle.posicion_inicial) + estructura.separador_campo;
                else
                    Linea += novedadObj.periodicidad + estructura.separador_campo;
            }
            if (Detalle.codigo_campo == 26) // Saldo
            {
                // Fecem pidio que ahorroprogramado y ahorrovista el saldo y el valor sean iguales
                // estupido pero es asi, se usa el parametro 27 para controlar eso
                if (novedadObj.tipo_producto != null)
                {
                    TipoDeProducto tipoProducto = novedadObj.tipo_producto.ToEnum<TipoDeProducto>();
                    if (tipoProducto == TipoDeProducto.AhorroProgramado || tipoProducto == TipoDeProducto.AhorrosVista || tipoProducto == TipoDeProducto.Aporte)
                    {
                        General general = ConsultarParametroGeneral(27);
                        if (general.valor != "0")
                        {
                            novedadObj.saldo = novedadObj.valor;
                        }
                    }
                }
                // Se redondio el saldo porque estaba saliendo el campo con decimales para FECEM. FerOrt. 6/Sep/2017.
                if (Detalle.justificador != null)
                    Linea += AjustarCampos(Convert.ToInt64(novedadObj.saldo).ToString(), Convert.ToChar(Detalle.justificador), Detalle.justificacion, Detalle.longitud, Detalle.posicion_inicial) + estructura.separador_campo;
                else
                    Linea += Convert.ToInt64(novedadObj.saldo).ToString() + estructura.separador_campo;
            }
            if (Detalle.codigo_campo == 27) // Total
            {
                if (Detalle.justificador != null)
                    Linea += AjustarCampos(novedadObj.total.ToString(), Convert.ToChar(Detalle.justificador), Detalle.justificacion, Detalle.longitud, Detalle.posicion_inicial) + estructura.separador_campo;
                else
                    Linea += novedadObj.total.ToString() + estructura.separador_campo;
            }
            if (Detalle.codigo_campo == 28) // Saldo Total
            {
                if (Detalle.justificador != null)
                    Linea += AjustarCampos(novedadObj.total_saldo.ToString(), Convert.ToChar(Detalle.justificador), Detalle.justificacion, Detalle.longitud, Detalle.posicion_inicial) + estructura.separador_campo;
                else
                    Linea += novedadObj.total_saldo.ToString() + estructura.separador_campo;
            }
            if (Detalle.codigo_campo == 29) // Codigo Recaudo Estructura
            {
                if (Detalle.justificador != null)
                    Linea += AjustarCampos(empresa.codigo_recaudo_estructura.ToString(), Convert.ToChar(Detalle.justificador), Detalle.justificacion, Detalle.longitud, Detalle.posicion_inicial) + estructura.separador_campo;
                else
                    Linea += empresa.codigo_recaudo_estructura.ToString() + estructura.separador_campo;
            }
            if (Detalle.codigo_campo == 35) // Capital
            {
                string lValor = novedadObj.capital.ToString();
                lValor = Reemplazar(lValor, Convert.ToChar(estructura.separador_miles), Convert.ToChar(estructura.separador_decimal));
                if (Detalle.justificador != null)
                    Linea += AjustarCampos(lValor.Replace("$", ""), Convert.ToChar(Detalle.justificador), Detalle.justificacion, Detalle.longitud, Detalle.posicion_inicial) + estructura.separador_campo;
                else
                    Linea += lValor.Replace("$", "") + estructura.separador_campo;
            }
            if (Detalle.codigo_campo == 36) // Interes Cte
            {
                string lValor = novedadObj.intcte.ToString();
                lValor = Reemplazar(lValor, Convert.ToChar(estructura.separador_miles), Convert.ToChar(estructura.separador_decimal));
                if (Detalle.justificador != null)
                    Linea += AjustarCampos(lValor.Replace("$", ""), Convert.ToChar(Detalle.justificador), Detalle.justificacion, Detalle.longitud, Detalle.posicion_inicial) + estructura.separador_campo;
                else
                    Linea += lValor.Replace("$", "") + estructura.separador_campo;
            }
            if (Detalle.codigo_campo == 37) // Interes Mora
            {
                string lValor = novedadObj.intmora.ToString();
                lValor = Reemplazar(lValor, Convert.ToChar(estructura.separador_miles), Convert.ToChar(estructura.separador_decimal));
                if (Detalle.justificador != null)
                    Linea += AjustarCampos(lValor.Replace("$", ""), Convert.ToChar(Detalle.justificador), Detalle.justificacion, Detalle.longitud, Detalle.posicion_inicial) + estructura.separador_campo;
                else
                    Linea += lValor.Replace("$", "") + estructura.separador_campo;
            }
            if (Detalle.codigo_campo == 38) // Valor Seguro
            {
                string lValor = novedadObj.seguro.ToString();
                lValor = Reemplazar(lValor, Convert.ToChar(estructura.separador_miles), Convert.ToChar(estructura.separador_decimal));
                if (Detalle.justificador != null)
                    Linea += AjustarCampos(lValor.Replace("$", ""), Convert.ToChar(Detalle.justificador), Detalle.justificacion, Detalle.longitud, Detalle.posicion_inicial) + estructura.separador_campo;
                else
                    Linea += lValor.Replace("$", "") + estructura.separador_campo;
            }
            if (Detalle.codigo_campo == 39) // Otros Atributos
            {
                string lValor = novedadObj.otros.ToString();
                lValor = Reemplazar(lValor, Convert.ToChar(estructura.separador_miles), Convert.ToChar(estructura.separador_decimal));
                if (Detalle.justificador != null)
                    Linea += AjustarCampos(lValor.Replace("$", ""), Convert.ToChar(Detalle.justificador), Detalle.justificacion, Detalle.longitud, Detalle.posicion_inicial) + estructura.separador_campo;
                else
                    Linea += lValor.Replace("$", "") + estructura.separador_campo;
            }
            if (Detalle.codigo_campo == 40) // Total Fijos
            {
                string lValor = novedadObj.total_fijos.ToString();
                lValor = Reemplazar(lValor, Convert.ToChar(estructura.separador_miles), Convert.ToChar(estructura.separador_decimal));
                if (Detalle.justificador != null)
                    Linea += AjustarCampos(lValor.Replace("$", ""), Convert.ToChar(Detalle.justificador), Detalle.justificacion, Detalle.longitud, Detalle.posicion_inicial) + estructura.separador_campo;
                else
                    Linea += lValor.Replace("$", "") + estructura.separador_campo;
            }
            if (Detalle.codigo_campo == 41) // Total Prestamos
            {
                string lValor = novedadObj.total_prestamos.ToString();
                lValor = Reemplazar(lValor, Convert.ToChar(estructura.separador_miles), Convert.ToChar(estructura.separador_decimal));
                if (Detalle.justificador != null)
                    Linea += AjustarCampos(lValor.Replace("$", ""), Convert.ToChar(Detalle.justificador), Detalle.justificacion, Detalle.longitud, Detalle.posicion_inicial) + estructura.separador_campo;
                else
                    Linea += lValor.Replace("$", "") + estructura.separador_campo;
            }
            if (Detalle.codigo_campo == 42) // Fecha Inicio Producto
            {
                if (novedadObj.fecha_inicio_producto.ToString() != "")
                {
                    string sformato_fecha = estructura.formato_fecha;
                    if (Detalle.justificador != null)
                        Linea += AjustarCampos(Convert.ToDateTime(novedadObj.fecha_inicio_producto).ToString(), Convert.ToChar(Detalle.justificador)) + estructura.separador_campo;
                    else
                        Linea += Convert.ToDateTime(novedadObj.fecha_inicio_producto).ToShortDateString() + estructura.separador_campo;
                }
                else
                {
                    if (Detalle.justificador != null)
                        Linea += AjustarCampos("", Convert.ToChar(Detalle.justificador), Detalle.justificacion, Detalle.longitud, Detalle.posicion_inicial) + estructura.separador_campo;
                    else
                        Linea += "" + estructura.separador_campo;
                }
            }
            if (Detalle.codigo_campo == 43) // Fecha Vencimiento Producto
            {
                if (novedadObj.fecha_vencimiento_producto.ToString() != "")
                {
                    string sformato_fecha = estructura.formato_fecha;
                    if (Detalle.justificador != null)
                        Linea += AjustarCampos(novedadObj.fecha_vencimiento_producto.ToString(), Convert.ToChar(Detalle.justificador)) + estructura.separador_campo;
                    else
                        Linea += Convert.ToDateTime(novedadObj.fecha_vencimiento_producto).ToShortDateString() + estructura.separador_campo;
                }
                else
                {
                    if (Detalle.justificador != null)
                        Linea += AjustarCampos("", Convert.ToChar(Detalle.justificador), Detalle.justificacion, Detalle.longitud, Detalle.posicion_inicial) + estructura.separador_campo;
                    else
                        Linea += "" + estructura.separador_campo;
                }
            }
        }
        return Linea;
    }

    public string AjustarCampos(string pcampo, char pcaracterajuste, string pjustificacion = "0", int? plongitud = 0, int? posicion_inicial = 1)
    {
        string lineaNueva = "";
        pjustificacion = string.IsNullOrEmpty(pjustificacion) ? "0" : pjustificacion;
        if (pcampo == null)
        {
            pcampo = "";
        }
        if (plongitud == 0 || plongitud == null)
        {
            Boolean bencontro = true;
            char[] array = pcampo.ToCharArray();
            for (int i = 0; i < array.Length; i++)
            {
                char letter = array[i];
                if (!(letter == pcaracterajuste))
                    bencontro = false;
                if (bencontro == false)
                    lineaNueva = lineaNueva + Convert.ToString(letter);
            }

            return lineaNueva;
        }
        else
        {
            if (pcampo.Length <= plongitud)
            {
                for (int i = 0; i < plongitud - pcampo.Length; i++)
                {
                    lineaNueva = lineaNueva + Convert.ToString(pcaracterajuste);
                }
            }
            else
            {
                try
                {
                    // pcampo = pcampo.Substring(Convert.ToInt32(posicion_inicial - 1), Convert.ToInt32(plongitud));
                    pcampo = pcampo.Substring(0, Convert.ToInt32(plongitud));
                }
                catch
                {
                    VerError("Campo" + pcampo  + " Posicion Inicial:" + posicion_inicial + " Longitud:" + plongitud);
                }                
            }
            if (pjustificacion == "1")
            {
                lineaNueva = lineaNueva + pcampo;
            }
            else if (pjustificacion == "2")
            {
                lineaNueva = pcampo + lineaNueva;
            }
            else
            {
                lineaNueva = lineaNueva + pcampo;
            }
            return lineaNueva;
        }
    }

    public string Reemplazar(string pTexto, char pMiles, char pDecimal)
    {
        string lineaNueva = "";
        char[] array = pTexto.ToCharArray();
        for (int i = 0; i < array.Length; i++)
        {
            char letter = array[i];
            if (letter == Convert.ToChar(gSeparadorMiles))
            {
                letter = pMiles;
            }
            else
            {
                if (letter == Convert.ToChar(gSeparadorDecimal))
                    letter = pDecimal;
            }
            lineaNueva = lineaNueva + Convert.ToString(letter);
        }
        return lineaNueva;
    }

    protected void ddlPeriodo_SelectedIndexChanged(object sender, EventArgs e)
    {
        VerError("");
        if (ddlPeriodo.SelectedValue == "")
        {
            txtfechaPeriodo.Text = "";
        }
        else
        {
            try
            {
                if (ddlEmpresa.SelectedIndex != 0 && ddlTipoLista.SelectedIndex != 0)
                {
                    txtfechaPeriodo.ToDateTime = DateTime.ParseExact(ddlPeriodo.SelectedItem.Text, gFormatoFecha, null);
                    //consultar la periodicidad de fecha inicio
                    DateTime? pFechaIni = null;
                    EmpresaRecaudoServices BOEmpresaReca = new EmpresaRecaudoServices();
                    pFechaIni = BOEmpresaReca.CalcularFechaInicialNovedad(Convert.ToInt64(ddlEmpresa.SelectedValue), Convert.ToInt64(ddlTipoLista.SelectedValue), Convert.ToDateTime(ddlPeriodo.SelectedValue), (Usuario)Session["usuario"]);
                    if (pFechaIni != null)
                        txtFechaInicial.Text = Convert.ToDateTime(pFechaIni).ToShortDateString();
                    else
                        txtFechaInicial.Text = "";
                }
            }
            catch (Exception ex)
            {
                VerError(ex.Message);
            }
        }
    }


    //Agregado datos de Paginaciòn
    protected void RegsPag_SelectedIndexChanged(object sender, EventArgs e)
    {
        //ObtenerDetalleComprobante(false);
        DropDownList _DropDownList = (DropDownList)sender;
        this.gvLista.PageSize = int.Parse(_DropDownList.SelectedValue);
        if (Session["DTDETALLE"] != null)
        {
            List<EmpresaNovedad> lstConsulta = new List<EmpresaNovedad>();
            lstConsulta = (List<EmpresaNovedad>)Session["DTDETALLE"];
            gvLista.DataSource = lstConsulta;
            if (lstConsulta.Count > 0)
            {
                gvLista.DataBind();
                return;
            }
        }
        Actualizar();
    }

    protected void gvLista_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Pager)
        {
            /*Label _TotalPags = (Label)e.Row.FindControl("lblTotalNumberOfPages");
            _TotalPags.Text = gvLista.PageCount.ToString();
            TextBox _IraPag = (TextBox)e.Row.FindControl("IraPag");
            _IraPag.Text = (gvLista.PageIndex + 1).ToString();
            DropDownList _DropDownList = (DropDownList)e.Row.FindControl("RegsPag");
            _DropDownList.SelectedValue = gvLista.PageSize.ToString();*/

        }
    }

    protected void IraPag(object sender, EventArgs e)
    {
        TextBox _IraPag = (TextBox)sender;
        int _NumPag = 0;

        if (int.TryParse(_IraPag.Text.Trim(), out _NumPag) && _NumPag > 0 && _NumPag <= this.gvLista.PageCount)
        {
            if (int.TryParse(_IraPag.Text.Trim(), out _NumPag) && _NumPag > 0 && _NumPag <= this.gvLista.PageCount)
            {
                this.gvLista.PageIndex = _NumPag - 1;
            }
            else
            {
                this.gvLista.PageIndex = 0;
            }
        }
        this.gvLista.SelectedIndex = -1;
    }


    protected void btnCargarDatos_Click(object sender, EventArgs e)
    {
        VerError("");
        lblmsjCarga.Text = "";
        mtvGeneral.Visible = true;
        mvVentEmergente.Visible = false;
        mtvGeneral.ActiveViewIndex = 3;
        Site toolbar = (Site)Master;
        toolbar.MostrarGuardar(false);
        toolbar.MostrarImprimir(false);
        toolbar.MostrarExportar(false);
        
    }

    protected void btnCancelarCarga_Click(object sender, EventArgs e)
    {
        mtvGeneral.Visible = true;
        mtvGeneral.ActiveViewIndex = 0;
        mvVentEmergente.Visible = false;
        Site toolbar = (Site)Master;
        toolbar.MostrarGuardar(true);
        toolbar.MostrarImprimir(true);
        toolbar.MostrarExportar(true);
    }


    protected void btnAceptarCarga_Click(object sender, EventArgs e)
    {
        VerError("");
        try
        {
            lblmsjCarga.Text = "";
            if (flpArchivo.HasFile)
            {
                String fileName = Path.GetFileName(this.flpArchivo.PostedFile.FileName);
                String extension = Path.GetExtension(this.flpArchivo.PostedFile.FileName).ToLower();
                if (extension != ".txt")
                {
                    lblmsjCarga.Text = "Para realizar la carga de Archivo solo debe seleccionar un archivo de texto";
                    return;
                }

                List<EmpresaNovedad> lstDatosCarga = new List<EmpresaNovedad>();

                //CARGANDO DATOS AL LISTADO POR SI EXISTEN EN LA GRIDVIEW
                lstDatosCarga = ObtenerLista();

                string readLine;
                StreamReader strReader;
                Stream stream = flpArchivo.FileContent;

                string ErrorCargue = "";

                //ARCHIVO DE TEXTO
                using (strReader = new StreamReader(stream))
                {
                    while (strReader.Peek() >= 0)
                    {
                        //PASANDO LA FILA DEL ARCHIVO A LA VARIABLE
                        readLine = strReader.ReadLine();
                        if (readLine != "")
                        {
                            //SEPARANDO CADA CAMPO
                            if (readLine.Contains("|") == false)
                            {
                                lblmsjCarga.Text = "El Archivo cargado no contiene los separadores correctos, verifique los datos ( Separador correcto : | )";
                                return;
                            }
                            string[] arrayline = readLine.Split('|');
                            int contadorreg = 0;

                            EmpresaNovedad novedadEnt = new EmpresaNovedad();
                            //INICIAR LA LECTURA DE DATOS DE LA PRIMERA LINEA
                            int posicionInicial = 1;
                            foreach (string variable in arrayline)
                            {
                                if (posicionInicial >= 0)
                                {
                                    if (variable != null)
                                    {
                                        if (contadorreg == 0) //TIPO PRODUCTO
                                        {
                                            novedadEnt.nom_tipo_producto = variable.ToUpper().Trim();
                                        }
                                        if (contadorreg == 1) //LINEA
                                        {
                                            novedadEnt.linea = variable.Trim();
                                        }
                                        if (contadorreg == 2) //NUMERO PRODUCTO
                                        {
                                            novedadEnt.numero_producto = variable.Trim();
                                        }
                                        if (contadorreg == 3) //CODIGO CLIENTE
                                        {
                                            novedadEnt.cod_persona = Convert.ToInt64(variable.Trim());
                                        }
                                        if (contadorreg == 4) //IDENTIFICACION
                                        {
                                            novedadEnt.identificacion = variable.Trim();
                                        }
                                        if (contadorreg == 5) //NOMBRE
                                        {
                                            novedadEnt.nombres = variable.ToUpper().Trim();
                                        }
                                        if (contadorreg == 6) //APELLIDO
                                        {
                                            novedadEnt.apellidos = variable.ToUpper().Trim();
                                        }
                                        if (contadorreg == 7) //FEC PROX PAGO
                                        {
                                            string sformato_fecha = "dd/MM/yyyy";
                                            novedadEnt.fecha_proximo_pago = DateTime.ParseExact(variable.ToString().Trim(), sformato_fecha, null);
                                        }
                                        if (contadorreg == 8) //VALOR
                                        {
                                            novedadEnt.valor = Decimal.Parse(variable.ToString().Trim().Replace(",", GlobalWeb.gSeparadorDecimal).Replace(".", ""));
                                        }
                                    }
                                }
                                contadorreg++;
                            }

                            if (ddlEmpresa.SelectedValue != "")
                            {
                                if (ValidarPersonaHabil(novedadEnt.cod_persona.Value, long.Parse(ddlEmpresa.SelectedValue)))
                                    lstDatosCarga.Add(novedadEnt);
                                else
                                    ErrorCargue += "* La persona " + novedadEnt.nombres + " " + novedadEnt.apellidos + " se encuentra inhabil <br>";
                            }
                            else
                                VerError("Seleccione la empresea antes de continuar con el cargue de novedades");
                        }
                    }
                }

                if (ErrorCargue != "")
                    VerError("Han surgido problemas al cargar los siguientes detalles: <br>" + ErrorCargue);

                Session["DTDETALLE"] = lstDatosCarga;
                gvLista.DataSource = lstDatosCarga;
                gvLista.DataBind();
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstDatosCarga.Count.ToString();

                mtvGeneral.Visible = true;
                mtvGeneral.ActiveViewIndex = 0;
                mvVentEmergente.Visible = false;
                btnDetalle.Visible = true;
                Site toolbar = (Site)Master;
                toolbar.MostrarGuardar(true);
                toolbar.MostrarImprimir(true);
                toolbar.MostrarExportar(true);

            }   
            else
            {
                lblmsjCarga.Text = "Seleccione un Archivo para realizar la carga de datos.";
            }
        }
        catch (Exception ex)
        {
            VerError("ERROR: " + ex.Message);
        }
    }

    private bool ValidarPersonaHabil(long pCodPersona , long pCodEmpresa)
    {
        //Validar si la persona es inhabil y de ser así si se le puede agregar una novedad
        Xpinn.Asesores.Services.PersonaService objPersonaServices = new Xpinn.Asesores.Services.PersonaService();
        Xpinn.Asesores.Entities.Persona objPersona = objPersonaServices.ConsultaryaExistente(pCodPersona, (Usuario)Session["usuario"]);

        EmpresaRecaudoServices objRecaudoServices = new EmpresaRecaudoServices();
        EmpresaRecaudo objEmpresaRecaudo = new EmpresaRecaudo();
        objEmpresaRecaudo.cod_empresa = pCodEmpresa;
        objEmpresaRecaudo = objRecaudoServices.ConsultarEMPRESARECAUDO(objEmpresaRecaudo, (Usuario)Session["usuario"]);

        return !( objEmpresaRecaudo.deshabilitar_desc_inhabiles && objPersona.Estado == "Inactivo" );
    }

    #region NOVEDADES NUEVAS

    protected List<EmpresaNovedad> ObtenerListaNoveNuevos()
    {
        List<EmpresaNovedad> lstTemp = new List<EmpresaNovedad>();
        List<EmpresaNovedad> lista = new List<EmpresaNovedad>();
        gvNovedadesNuevas.AllowPaging = false;
        gvNovedadesNuevas.DataBind();
        if (gvNovedadesNuevas.Rows.Count == 0)
        {
            if (Session["DTDETALLENEW"] != null)
            {
                List<EmpresaNovedad> lstConsulta = new List<EmpresaNovedad>();
                lstConsulta = (List<EmpresaNovedad>)Session["DTDETALLENEW"];
                gvNovedadesNuevas.DataSource = lstConsulta;
                gvNovedadesNuevas.AllowPaging = false;
                gvNovedadesNuevas.DataBind();
            }
        }
        foreach (GridViewRow rfila in gvNovedadesNuevas.Rows)
        {
            EmpresaNovedad eTemp = new EmpresaNovedad();

            Label lbliddetalle = (Label)rfila.FindControl("lbliddetalle");
            if (lbliddetalle != null)
                eTemp.iddetalle = Convert.ToInt32(lbliddetalle.Text);

            Label lblnom_tipo_producto = (Label)rfila.FindControl("lblnom_tipo_producto");
            if (lblnom_tipo_producto != null)
            {
                eTemp.nom_tipo_producto = lblnom_tipo_producto.Text;
                if (lblnom_tipo_producto.Text.Trim().Contains("-"))
                {
                    string[] pProd = lblnom_tipo_producto.Text.Trim().Split('-');
                    if (IsValidNumber(pProd[0].Trim()))
                        eTemp.tipo_producto = pProd[0].ToString();
                    else
                        eTemp.tipo_producto = "0";
                }
            }

            Label lbllinea = (Label)rfila.FindControl("lbllinea");
            if (lbllinea != null)
                eTemp.linea = lbllinea.Text;

            Label lblnumero_producto = (Label)rfila.FindControl("lblnumero_producto");
            if (lblnumero_producto != null)
                eTemp.numero_producto = lblnumero_producto.Text;

            Label lblcod_persona = (Label)rfila.FindControl("lblcod_persona");
            if (lblcod_persona != null)
                eTemp.cod_persona = Convert.ToInt64(lblcod_persona.Text);
            else
                eTemp.cod_persona = 0;
            Label lblidentificacion = (Label)rfila.FindControl("lblidentificacion");
            if (lblidentificacion != null)
                eTemp.identificacion = lblidentificacion.Text;

            Label lblnombres = (Label)rfila.FindControl("lblnombres");
            if (lblnombres != null)
                eTemp.nombres = lblnombres.Text;

            Label lblapellidos = (Label)rfila.FindControl("lblapellidos");
            if (lblapellidos != null)
                eTemp.apellidos = lblapellidos.Text;

        

            Label lblTipoNovedad = (Label)rfila.FindControl("lblTipoNovedad");
            if (lblTipoNovedad != null && lblTipoNovedad.Text != "")
                eTemp.tipo_novedad = lblTipoNovedad.Text;

            Label lblFechaNovedad = (Label)rfila.FindControl("lblFechaNovedad");
            if (lblFechaNovedad != null)
                if (lblFechaNovedad.Text != "")
                    eTemp.fecha_inicial = Convert.ToDateTime(lblFechaNovedad.Text);
                else
                    eTemp.fecha_inicial = null;
            else
                eTemp.fecha_inicial = null;


            Label lblFecFinNovedad = (Label)rfila.FindControl("lblFecFinNovedad");
            if (lblFecFinNovedad != null)
                if (lblFecFinNovedad.Text != "")
                    eTemp.fecha_final = Convert.ToDateTime(lblFecFinNovedad.Text);
                else
                    eTemp.fecha_final = null;
            else
                eTemp.fecha_final = null;

            Label lblvalorCuota = (Label)rfila.FindControl("lblvalorCuota");
            if (lblvalorCuota != null)
            {
                string[] Val;
                Val = lblvalorCuota.Text.Trim().Split(',');
                //eTemp.valor = Convert.ToDecimal(Val[0]);
                eTemp.valor = Math.Round(Convert.ToDecimal(lblvalorCuota.Text));
            }

            Label lblNumCuotas = (Label)rfila.FindControl("lblNumCuotas");
            if (lblNumCuotas.Text != "")
                eTemp.numero_cuotas = Convert.ToInt32(lblNumCuotas.Text);

            Label lblVrTotal = (Label)rfila.FindControl("lblVrTotal");
            if (lblVrTotal.Text != "")
                eTemp.valor_total = Math.Round(Convert.ToDecimal(lblVrTotal.Text));

            eTemp.estado = ddlEstado.SelectedValue;

            lista.Add(eTemp);
            Session["DatosBeneNew"] = lista;

            if (eTemp.nom_tipo_producto.Trim() != "" && eTemp.identificacion.Trim() != null)
            {
                lstTemp.Add(eTemp);
            }
        }

        return lstTemp;
    }

    private void ActualizarNuevas()
    {
        try
        {
            List<EmpresaNovedad> lstConsulta = new List<EmpresaNovedad>();

            EmpresaNovedad recaudo = new EmpresaNovedad();

            if (idObjeto != "")
            {
                recaudo.numero_novedad = Convert.ToInt64(idObjeto); ;
                if (Session["NovNew_Modifica"].ToString() == "1")
                    if (idObjeto != "")
                        lstConsulta = Recaudos.ListarDetalleGeneracionNuevas(recaudo, (Usuario)Session["usuario"]);
                    else
                    {
                        lstConsulta = Recaudos.ListarTempNovedadesNuevas(recaudo, (Usuario)Session["usuario"]);
                        Session["NovNew_Modifica"] = "0";
                    }
                else
                {
                    lstConsulta = Recaudos.ListarTempNovedadesNuevas(recaudo, (Usuario)Session["usuario"]);
                    Session["NovNew_Modifica"] = "0";
                }
            }
            else
            {
                lstConsulta = Recaudos.ListarTempNovedadesNuevas(recaudo, (Usuario)Session["usuario"]);
            }
            gvNovedadesNuevas.PageSize = 15;
            String emptyQuery = "Fila de datos vacia";
            gvNovedadesNuevas.EmptyDataText = emptyQuery;
            gvNovedadesNuevas.DataSource = lstConsulta;
            Session["DTDETALLENEW"] = lstConsulta;

            Site toolBar = (Site)this.Master;
            toolBar.MostrarGuardar(false);
            toolBar.MostrarExportar(false);
            toolBar.MostrarImprimir(false);
            
            if (lstConsulta.Count > 0)
            {
                lblInfoNuevo.Visible = false;
                btnDetalleNew.Visible = true;
                lblTotalRegNuevos.Visible = true;
                lblTotalRegNuevos.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                gvNovedadesNuevas.DataBind();
                Session["DTDETALLENEW"] = lstConsulta;
                toolBar.MostrarGuardar(true);
                toolBar.MostrarExportar(true);
                toolBar.MostrarImprimir(true);
            }
            else
            {
                if (gvLista.Rows.Count > 0)
                {
                    toolBar.MostrarGuardar(true);
                    toolBar.MostrarExportar(true);
                    toolBar.MostrarImprimir(true);
                }
                lblInfoNuevo.Visible = true;
                btnDetalleNew.Visible = false;
                lblTotalRegNuevos.Visible = false;
                Session["DTDETALLENEW"] = null;
            }
            TotalizarRegistros(lstConsulta, lblTotalNew);
            Session.Add(Recaudos.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(Recaudos.CodigoPrograma, "ActualizarNuevas", ex);
        }
    }

    //EVENTOS DE LA GRID VIEW NOVEDADES NUEVAS
    protected void gvNovedadesNuevas_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvNovedadesNuevas.PageIndex = e.NewPageIndex;
            ActualizarNuevas();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(Recaudos.CodigoPrograma, "gvNovedadesNuevas_PageIndexChanging", ex);
        }
    }

    protected void gvNovedadesNuevas_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvNovedadesNuevas.Rows[e.NewEditIndex].Cells[0].Text;

        Cambiarhabilitado(false);

        lblFecEmerg.Text = "F. Novedad";
        panelEdNovNew.Visible = true;
        Label lbliddetalle = (Label)gvNovedadesNuevas.Rows[e.NewEditIndex].Cells[2].FindControl("lbliddetalle");
        if (lbliddetalle != null)
            txtidDetalle.Text = lbliddetalle.Text;

        Label lblnom_tipo_producto = (Label)gvNovedadesNuevas.Rows[e.NewEditIndex].Cells[3].FindControl("lblnom_tipo_producto");
        if (lblnom_tipo_producto != null)
        {
            if (lblnom_tipo_producto.Text != "")
            {
                if (IsValidNumber(lblnom_tipo_producto.Text))
                {
                    ddlTipoProducto.SelectedValue = lblnom_tipo_producto.Text;
                }
                else
                {
                    if (lblnom_tipo_producto.Text.Contains("-"))
                    {
                        string[] sValor = lblnom_tipo_producto.Text.Split('-');
                        ddlTipoProducto.SelectedValue = sValor[0];
                    }
                }
            }
        }


        Label lbllinea = (Label)gvNovedadesNuevas.Rows[e.NewEditIndex].Cells[4].FindControl("lbllinea");
        if (lbllinea != null)
            txtNomProducto.Text = lbllinea.Text;

        Label lblnumero_producto = (Label)gvNovedadesNuevas.Rows[e.NewEditIndex].Cells[5].FindControl("lblnumero_producto");
        if (lblnumero_producto != null)
            txtCodProducto.Text = lblnumero_producto.Text;

        Label lblcod_persona = (Label)gvNovedadesNuevas.Rows[e.NewEditIndex].Cells[6].FindControl("lblcod_persona");
        if (lblcod_persona != null)
            txtCodPersona.Text = lblcod_persona.Text;

        Label lblidentificacion = (Label)gvNovedadesNuevas.Rows[e.NewEditIndex].Cells[7].FindControl("lblidentificacion");
        if (lblidentificacion != null)
            txtIdentificacion.Text = lblidentificacion.Text;

        Label lblnombres = (Label)gvNovedadesNuevas.Rows[e.NewEditIndex].Cells[8].FindControl("lblnombres");
        Label lblapellidos = (Label)gvNovedadesNuevas.Rows[e.NewEditIndex].Cells[9].FindControl("lblapellidos");

        if (lblnombres != null && lblapellidos != null)
            txtNomPersona.Text = lblnombres.Text + ", " + lblapellidos.Text;

        Label lblfechaGrid = (Label)gvNovedadesNuevas.Rows[e.NewEditIndex].Cells[10].FindControl("lblFechaNovedad");
        if (lblfechaGrid != null)
            txtProxPago.Text = lblfechaGrid.Text;

        Label lblvalor = (Label)gvNovedadesNuevas.Rows[e.NewEditIndex].Cells[13].FindControl("lblvalorCuota");
        if (lblvalor != null)
            txtvalor.Text = lblvalor.Text;

        Label lblTipoNovedad = (Label)gvNovedadesNuevas.Rows[e.NewEditIndex].Cells[10].FindControl("lblTipoNovedad");
        if (lblTipoNovedad != null)
            if (lblTipoNovedad.Text != "")
                ddlTipoNovedad.SelectedValue = lblTipoNovedad.Text;

        Label lblFecFinNovedad = (Label)gvNovedadesNuevas.Rows[e.NewEditIndex].Cells[12].FindControl("lblFecFinNovedad");
        if (lblFecFinNovedad != null)
            if (lblFecFinNovedad.Text != "")
                txtFecFinNov.Text = lblFecFinNovedad.Text;

        Label lblVrTotal = (Label)gvNovedadesNuevas.Rows[e.NewEditIndex].Cells[12].FindControl("lblVrTotal");
        if (lblVrTotal != null)
            if (lblVrTotal.Text != "")
                txtVrTotal.Text = lblVrTotal.Text;

        Label lblNumCuotas = (Label)gvNovedadesNuevas.Rows[e.NewEditIndex].Cells[12].FindControl("lblNumCuotas");
        if (lblNumCuotas != null)
            if (lblNumCuotas.Text != "")
                txtNumCuotas.Text = lblNumCuotas.Text;

        Cambiarhabilitado(true);

        lblmsj.Text = "";
        e.NewEditIndex = -1;
        mpeActualizarNovedad.Show();
    }

    protected void gvNovedadesNuevas_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            if (ddlEstado.Text != "APLICADO")
            {
                int conseID = Convert.ToInt32(gvNovedadesNuevas.DataKeys[e.RowIndex].Values[0].ToString());
                List<EmpresaNovedad> lstDatosCarga = new List<EmpresaNovedad>();
                lstDatosCarga = ObtenerListaNoveNuevos();
                if (conseID == 0)
                {
                    lstDatosCarga.RemoveAt((gvNovedadesNuevas.PageIndex * gvNovedadesNuevas.PageSize) + e.RowIndex);
                    gvNovedadesNuevas.DataSourceID = null;
                    gvNovedadesNuevas.DataBind();
                    gvNovedadesNuevas.DataSource = lstDatosCarga;
                    gvNovedadesNuevas.DataBind();
                    Session["DTDETALLENEW"] = lstDatosCarga;
                    lblTotalRegs.Visible = true;
                    lblTotalRegs.Text = "<br/> Registros encontrados " + lstDatosCarga.Count.ToString();
                }
                else
                {
                    if (idObjeto == "")
                    {
                        lstDatosCarga.RemoveAt((gvNovedadesNuevas.PageIndex * gvNovedadesNuevas.PageSize) + e.RowIndex);
                        gvNovedadesNuevas.DataSourceID = null;
                        gvNovedadesNuevas.DataBind();
                        gvNovedadesNuevas.DataSource = lstDatosCarga;
                        gvNovedadesNuevas.DataBind();
                        Session["DTDETALLENEW"] = lstDatosCarga;
                    }
                    else
                    {
                        Session["INDEX"] = conseID;
                        Session["OPCION"] = 2;
                        ctlMensaje.MostrarMensaje("Desea Eliminar el registro seleccionado?");
                    }
                }
                TotalizarRegistros(lstDatosCarga, lblTotalNew);
            }
            else
            {
                VerError("No se Puede realizar la eliminación");
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(Recaudos.CodigoPrograma, "gvNovedadesNuevas_RowDeleting", ex);
        }
    }

    //Agregado datos de Paginaciòn
    protected void RegsPagNew_SelectedIndexChanged(object sender, EventArgs e)
    {
        //ObtenerDetalleComprobante(false);
        DropDownList _DropDownList = (DropDownList)sender;
        this.gvLista.PageSize = int.Parse(_DropDownList.SelectedValue);
        ActualizarNuevas();
    }

    protected void gvNovedadesNuevas_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Pager)
        {
            Label _TotalPags = (Label)e.Row.FindControl("lblTotalNumberOfPages");
            _TotalPags.Text = gvLista.PageCount.ToString();
            TextBox _IraPag = (TextBox)e.Row.FindControl("IraPag");
            _IraPag.Text = (gvLista.PageIndex + 1).ToString();
            DropDownList _DropDownList = (DropDownList)e.Row.FindControl("RegsPag");
            _DropDownList.SelectedValue = gvLista.PageSize.ToString();
        }
    }

    //PENDIENTE AJUSTAR.
    protected void btnDetalleNew_Click(object sender, EventArgs e)
    {
        lblmsj.Text = "";
        lblFecEmerg.Text = "F. Novedad";
        panelEdNovNew.Visible = true;
        limpiarVentanaEmergente();
        Cambiarhabilitado(false);
        mpeActualizarNovedad.Show();
    }

    #endregion



    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        List<EmpresaNovedad> lstReca = new List<EmpresaNovedad>();
        List<EmpresaNovedad> lstFiltro = new List<EmpresaNovedad>();
        if (Session["DTDETALLE"] != null)
        {
            lstReca = (List<EmpresaNovedad>)Session["DTDETALLE"];
            if(txtBuscar.Text != "")
            {
            lstFiltro = lstReca.Where(x => x.cod_persona == Convert.ToInt64(txtBuscar.Text.Trim()) || x.identificacion == txtBuscar.Text.Trim()).ToList();
            }
            else
            {
                lstFiltro = lstReca;
            }
            //foreach(EmpresaNovedad detalle in lstFiltro)
            //{
            //    lstReca.Remove(detalle);
            //}
            //lstFiltro.AddRange(lstReca);
        }
      
        if (lstFiltro != null)
        {
            gvLista.DataSource = lstFiltro;
            gvLista.DataBind();
        }
        ScriptManager.RegisterStartupScript(this, typeof(Page), "jsKeys", "javascript:gridviewScroll();", true);
    }


}

