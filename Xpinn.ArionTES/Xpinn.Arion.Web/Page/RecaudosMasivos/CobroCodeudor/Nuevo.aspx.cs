using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Tesoreria.Entities;
using Xpinn.Tesoreria.Services;
using Xpinn.FabricaCreditos.Services;
using Xpinn.Util;
using Xpinn.FabricaCreditos.Entities;

public partial class Nuevo : GlobalWeb
{
    CobroCodeudorService _cobroCodeudorService = new CobroCodeudorService();
    CreditoService _creditoService = new CreditoService();
    Usuario _usuario;
    long _nroRadicacion;


    #region Eventos Carga Inicial


    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(_cobroCodeudorService.CodigoPrograma, "A");

            Site toolBar = (Site)Master;

            toolBar.eventoRegresar += btnRegresar_Click;
            toolBar.eventoConsultar += ctlListarCreditos.Consultar;
            toolBar.eventoLimpiar += ctlListarCreditos.Limpiar;
            toolBar.eventoCancelar += btnCancelar_Click;
            toolBar.eventoGuardar += btnGuardar_Click;
            ctlListarCreditos.Error += CtlListarCreditos_Error;
            ctlListarCreditos.NuevaPagina += CtlListarCreditos_NuevaPagina;
            ctlMensaje.eventoClick += CtlMensaje_eventoClick;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_cobroCodeudorService.CodigoPrograma, "Page_PreInit", ex);
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        _usuario = (Usuario)Session["usuario"];
        Site toolBar = (Site)Master;

        if (Session[_cobroCodeudorService.CodigoPrograma + ".numeroRadi"] != null || ViewState["numeroRadi"] != null)
        {
            toolBar.MostrarConsultar(false);
            toolBar.MostrarLimpiar(false);
            toolBar.MostrarRegresar(false);

        }
        else
        {
            if (mvNuevoCobro.ActiveViewIndex == 0)
            {
                toolBar.MostrarGuardar(false);
                toolBar.MostrarCancelar(false);
            }
            else
            {
                toolBar.MostrarGuardar(true);
                toolBar.MostrarCancelar(true);
            }
        }

        if (!IsPostBack)
        {
            object session = Session[_cobroCodeudorService.CodigoPrograma + ".numeroRadi"];

            if (session != null)
            {
                _nroRadicacion = (long)session;

                // Es necesario borrar las sesiones o explota si sale y vuelva entrar de una manera diferente
                // Por esa misma razon se borra la sesion y se pasa a un ViewState
                Session.Remove(_cobroCodeudorService.CodigoPrograma + ".numeroRadi");
                ViewState.Add("numeroRadi", _nroRadicacion);

                InicializarPaginaCodeudores(_nroRadicacion);

                mvNuevoCobro.ActiveViewIndex = 1;
            }
            else
            {
                // Se le debe pasar un filtro desde el WHERE incluyendolo, si no se le pasa consulta sin filtro, solo usa el filtro de lo que se le escriba en los textbox del control
                ctlListarCreditos.CargaInicial(" where c.numero_radicacion not in (select numero_radicacion from cobro_codeudor) and c.numero_radicacion in (select numero_radicacion from codeudores) and c.estado = 'C' ");
            }
        }
        else
        {
            _nroRadicacion = ViewState["numeroRadi"] != null ? Convert.ToInt64(ViewState["numeroRadi"]) : 0;
        }
    }


    #endregion


    #region Eventos Botones


    //Evento para regresar del control de seleccionar creditos a la lista
    private void btnRegresar_Click(object sender, ImageClickEventArgs e)
    {
        Navegar(Pagina.Lista);
    }


    //Evento para regresar del formulario de cobro a Lista o al Control segun sea el caso
    private void btnCancelar_Click(object sender, ImageClickEventArgs e)
    {
        if (_nroRadicacion != 0)
        {
            Navegar(Pagina.Lista);
        }
        else
        {
            mvNuevoCobro.ActiveViewIndex = 0;

            Site toolBar = (Site)Master;

            toolBar.MostrarConsultar(true);
            toolBar.MostrarLimpiar(true);
            toolBar.MostrarRegresar(true);
            toolBar.MostrarGuardar(false);
            toolBar.MostrarCancelar(false);
        }
    }


    //Evento del control que activa el formulario de cobro
    private void CtlListarCreditos_NuevaPagina(object sender, ListarCreditosPorFiltroArgs e)
    {
        InicializarPaginaCodeudores(Convert.ToInt64(e.Nradicacion));
        Site toolBar = (Site)Master;
        toolBar.MostrarConsultar(false);
        toolBar.MostrarLimpiar(false);
        toolBar.MostrarRegresar(false);
        toolBar.MostrarGuardar(true);
        toolBar.MostrarCancelar(true);

        mvNuevoCobro.ActiveViewIndex = 1;
    }


    //Evento para confirmar el guardado
    private void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        ctlMensaje.MostrarMensaje("Desea proseguir con el guardado?");
    }


    //Evento para guardar formulario de cobro sin importar de donde venga
    private void CtlMensaje_eventoClick(object sender, EventArgs e)
    {
        GuardarCobroCodeudores();
    }


    private void GuardarCobroCodeudores()
    {
        try
        {
            List<CobroCodeudor> lstCobro = RecorrerGrilla();
            Site toolBar = (Site)Master;

            if (lstCobro.Count == 0)
            {
                VerError("Debe llenar al menos un porcentaje de un codeudor, si ninguno tiene empresa recaudo no puede proseguir");
            }

            foreach (var cobro in lstCobro)
            {
                // Si numero de radicacion es diferente de 0 significa que vengo directo de la pagina de lista.aspx
                // Por ende estoy modificando
                if (cobro.idcobrocodeud != 0)
                {
                    _cobroCodeudorService.ModificarCobroCodeudor(cobro, _usuario);
                }
                else
                {
                    _cobroCodeudorService.CrearCobroCodeudor(cobro, _usuario);
                }

                toolBar.MostrarGuardar(false);
                toolBar.MostrarCancelar(false);
                mvNuevoCobro.ActiveViewIndex = 2;
            }
        }
        catch (Exception ex)
        {
            VerError("Error guardando el cobro de los codeudores, " + ex.Message);
        }
    }


    private List<CobroCodeudor> RecorrerGrilla()
    {
        StringHelper stringHelper = new StringHelper();
        List<CobroCodeudor> lstCobro = new List<CobroCodeudor>();

        foreach (GridViewRow fila in gvCodeudores.Rows)
        {
            DropDownListGrid ddlGrid = fila.FindControl("ddlEmpresaRecaudo") as DropDownListGrid;
            string cod_empresa = string.Empty;

            if (ddlGrid != null)
            {
                cod_empresa = ddlGrid.SelectedValue;
            }

            if (cod_empresa == "0")
            {
                continue;
            }

            CobroCodeudor cobro = new CobroCodeudor();
            cobro.idcobrocodeud = Convert.ToInt32(gvCodeudores.DataKeys[fila.RowIndex].Value);
            cobro.numero_radicacion = Convert.ToInt64(txtNroRadicacion.Text);
            cobro.cod_deudor = Convert.ToInt64(txtCodDeudor.Text);
            cobro.fecha_cobro = Convert.ToDateTime(txtFechaProximoPago.Text);
            cobro.cod_persona = Convert.ToInt64(fila.Cells[0].Text);
            cobro.porcentaje = Convert.ToDecimal(((TextBoxGrid)fila.FindControl("txtPorcentaje")).Text);
            cobro.valor = Convert.ToDecimal(stringHelper.DesformatearNumerosDecimales(((Label)fila.FindControl("txtValor")).Text));
            cobro.cod_empresa = Convert.ToInt32(cod_empresa);
            cobro.fechacrea = Convert.ToDateTime(txtFecha.Text); ;
            cobro.cod_usuario = _usuario.codusuario;
            cobro.estado = 0;
            lstCobro.Add(cobro);
        }

        return lstCobro;
    }


    #endregion


    #region Metodos de Inicializacion (Consultar/Llenar)


    private void InicializarPaginaCodeudores(long nradicacion)
    {
        Credito credito = ConsultarCredito(nradicacion);

        if (credito != null)
        {
            LlenarCamposCredito(credito);
        }

        ConsultarYLlenarGVCodeudores(nradicacion);
    }


    private Credito ConsultarCredito(long nradicacion)
    {
        try
        {
            Credito credito = _creditoService.ConsultarCredito(nradicacion, _usuario);

            return credito;
        }
        catch (Exception ex)
        {
            VerError("Error ConsultarCredito, " + ex.Message);
            return null;
        }
    }


    private void LlenarCamposCredito(Credito credito)
    {
        StringHelper stringHelper = new StringHelper();

        txtFecha.Text = DateTime.Now.ToShortDateString();
        txtNroRadicacion.Text = credito.numero_radicacion.ToString();
        txtLinea.Text = credito.linea_credito;
        txtFechaDesembolso.Text = credito.fecha_inicio.ToShortDateString();
        txtMonto.Text = stringHelper.FormatearNumerosComoCurrency(credito.monto.ToString());
        txtSaldoCapital.Text = stringHelper.FormatearNumerosComoCurrency(credito.saldo_capital.ToString());
        txtCuota.Text = stringHelper.FormatearNumerosComoCurrency(credito.valor_cuota.ToString());
        txtPlazo.Text = credito.plazo.ToString();
        txtFechaProximoPago.Text = credito.fecha_prox_pago.ToShortDateString();
        txtNombre.Text = credito.nombre;
        txtIdentificacion.Text = credito.identificacion;
        txtCodDeudor.Text = credito.cod_deudor.ToString();
    }


    private void ConsultarYLlenarGVCodeudores(long nradicacion)
    {
        try
        {
            List<CobroCodeudor> lstEntidad = _cobroCodeudorService.ConsultarCodeudoresDeUnCredito(nradicacion, _usuario);

            if (lstEntidad.Count == 0)
            {
                lstEntidad.Add(new CobroCodeudor());
            }

            gvCodeudores.DataSource = lstEntidad;
            gvCodeudores.DataBind();
        }
        catch (Exception ex)
        {
            VerError("Error llenando la GridView Codeudores, " + ex.Message);
        }
    }


    #endregion


    #region Eventos Varios


    private void CtlListarCreditos_Error(object sender, ListarCreditosPorFiltroArgs e)
    {
        VerError(e.Error);
    }


    protected void txtPorcentajeGVCOD_TextChanged(object sender, EventArgs e)
    {
        VerError("");

        TextBoxGrid textBox = sender as TextBoxGrid;

        if (textBox != null)
        {
            decimal porcentaje;
            bool valido = decimal.TryParse(textBox.Text, out porcentaje);

            if (!valido)
            {
                VerError("Has ingresado un porcentaje erroneo");
                return;
            }

            decimal sumaPorcentaje = gvCodeudores.Rows
                                        .OfType<GridViewRow>()
                                        .Select(x => (x.FindControl("txtPorcentaje") as TextBoxGrid).Text)
                                        .Where(x => !string.IsNullOrWhiteSpace(x))
                                        .Select(x => Convert.ToDecimal(x))
                                        .Sum();

            if (sumaPorcentaje > 100)
            {
                VerError("El Porcentaje tiene que ser menor o igual a 100");
                return;
            }

            StringHelper stringHelper = new StringHelper();
            int index = Convert.ToInt32(textBox.CommandArgument);
            Label lblRow = gvCodeudores.Rows[index].FindControl("txtValor") as Label;

            if (lblRow != null)
            {
                lblRow.Text = stringHelper.FormatearNumerosComoCurrency((porcentaje / 100) * Convert.ToDecimal(stringHelper.DesformatearNumerosDecimales(txtCuota.Text)));
            }
        }
    }


    protected void gvCodeudores_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string lblCodPersona = e.Row.Cells[0].Text;
                TextBoxGrid txtPorcentajeGrid = e.Row.FindControl("txtPorcentaje") as TextBoxGrid;

                if (!string.IsNullOrWhiteSpace(lblCodPersona) && txtPorcentajeGrid != null)
                {
                    long cod_persona = Convert.ToInt64(lblCodPersona);
                    List<EmpresaRecaudo> lstEmpresaRecaudo = _cobroCodeudorService.ListarEmpresaRecaudo(cod_persona, _usuario);

                    DropDownListGrid ddl = e.Row.FindControl("ddlEmpresaRecaudo") as DropDownListGrid;

                    if (ddl != null)
                    {
                        ddl.DataTextField = "nom_empresa";
                        ddl.DataValueField = "cod_empresa";

                        if (lstEmpresaRecaudo.Count == 0)
                        {
                            lstEmpresaRecaudo.Add(new EmpresaRecaudo { nom_empresa = "No tiene empresa recaudo", cod_empresa = 0 });
                            ddl.Enabled = false;
                            txtPorcentajeGrid.Enabled = false;
                        }

                        ddl.DataSource = lstEmpresaRecaudo;
                        ddl.DataBind();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            VerError("Error llenando la lista de empresa recaudo, " + ex.Message);
        }
    }


    #endregion


}