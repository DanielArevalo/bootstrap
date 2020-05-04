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
using Xpinn.FabricaCreditos.Services;
using Xpinn.FabricaCreditos.Entities;
using Xpinn.Obligaciones.Services;
using Xpinn.Obligaciones.Entities;
using Xpinn.Caja.Services;
using Xpinn.Caja.Entities;

partial class Nuevo : GlobalWeb
{
    private Xpinn.Obligaciones.Services.SolicitudService SolicitudServicio = new Xpinn.Obligaciones.Services.SolicitudService();
    private Xpinn.Caja.Services.UsuariosService UsuarioServicio = new Xpinn.Caja.Services.UsuariosService();
    private Xpinn.Caja.Entities.Usuarios user2 = new Xpinn.Caja.Entities.Usuarios();

    private Xpinn.Obligaciones.Services.ObligacionCreditoService ObligacionCreditoServicio = new Xpinn.Obligaciones.Services.ObligacionCreditoService();

    PeriodicidadService periodicidadServicio = new PeriodicidadService();
    ComponenteService componenteServicio = new ComponenteService();
    Usuario user = new Usuario();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[SolicitudServicio.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(SolicitudServicio.CodigoPrograma, "E");
            else
                VisualizarOpciones(SolicitudServicio.CodigoPrograma, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(SolicitudServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                Session.Remove("Componentes");
                idObjeto = "";
                user = (Usuario)Session["usuario"];
                user2.codusuario = user.codusuario;
                user2 = UsuarioServicio.ConsultarUsuarios(user2.codusuario, (Usuario)Session["usuario"]);

                LlenarComboEntidades(ddlEntidad);
                LlenarComboMonedas(ddlTipoMoneda);
                LlenarComboTipoCuota(ddlTipoCuota);
                LlenarComboPeriodicidadCuota(ddlPeriodCuotas);
                rbCalculoTasa.SelectedIndex = 0;
                LlenarComboTipoTasa(ddlTipoTasa, true);
                LlenarLineaObligacion(ddlLineaObligacion);
                CrearComponenteInicial();
                CrearPagosExtInicial();

                AsignarEventoConfirmar();
                CargarCuentas();

                txtCajero.Text = user2.nombre;
                txtFechaSolicitud.Text = user2.fecha_actual.ToShortDateString();
                txtFechaTransaccion.Text = user2.fecha_actual.ToString();

                gvComponente.Rows[0].Visible = false;
                gvPagosExt.Rows[0].Visible = false;

                CambiarDatoTipoCuota();

                if (Session[SolicitudServicio.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[SolicitudServicio.CodigoPrograma + ".id"].ToString();
                    Session.Remove(SolicitudServicio.CodigoPrograma + ".id");
                    ObtenerDatos(idObjeto);
                    Actualizar();
                }

                mvSolicitud.ActiveViewIndex = 0;
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(SolicitudServicio.CodigoPrograma, "Page_Load", ex);
        }
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            Xpinn.Obligaciones.Entities.Solicitud vSolicitud = new Xpinn.Obligaciones.Entities.Solicitud();

            DataTable dtComp = new DataTable();
            dtComp = (DataTable)Session["Componentes"];

            DataTable dtPagos = new DataTable();
            dtPagos = (DataTable)Session["PagosExt"];

            if (idObjeto == "")
            {
                if (txtMontoSol.Text == "0")
                {
                    VerError("Diligenciar  el monto solicitado");
                }
                if (txtFechaDesembolso.Text == "")
                {
                    VerError("Diligenciar la fecha de desembolso");
                }
                if (txtPlazo.Text == "")
                {
                    VerError("Diligenciar  el plazo de la obligación");
                }
                if (ddlTipoGracia.SelectedValue.ToString() == "0")
                {
                    txtGracia.Text = "0";
                }
                if (txtGracia.Text == "")
                {
                    VerError("Diligenciar  el plazo de gracia de la obligación");
                }
                if (txtValorTasa.Text == "")
                {
                    VerError("Diligenciar  la tasa  de la obligación");
                }
                if (Convert.ToInt64(txtPlazo.Text) <= Convert.ToInt64(txtGracia.Text))
                {
                    VerError("El plazo de gracia no puede ser mayor o igual al plazo");
                }

                if (ddlCuentas.SelectedValue == "0")
                {
                    VerError("Seleccionar el codigo de cuenta a la que va a desembolsar la obligación");
                }
                if (ddlPeriodCuotas.SelectedValue == "0")
                {
                    VerError("Seleccionar la periodicidad de las cuotas");
                }

                if (TxtNit.Text =="")
                {
                    VerError("El tercero no cuenta con nit");
                }

                if (txtMontoSol.Text != "0")
                {
                    if (txtFechaDesembolso.Text != "")
                    {
                        if (txtPlazo.Text != "")
                        {
                            bool result = true;
                            if (rbCalculoTasa.SelectedValue == "1")
                            {
                                if (txtValorTasa.Text.Trim() == "")
                                {
                                    result = false;
                                }
                            }
                            else
                            {
                                if (txtPuntosads.Text.Trim() == "" || txtPuntosads.Text.Trim() == "0")
                                {
                                    result = false;
                                }
                            }
                            
                            if(result)
                            {
                                vSolicitud.montosolicitado = txtMontoSol.Text == "" ? 0 : decimal.Parse(txtMontoSol.Text);
                                vSolicitud.montoaprobado = txtMontoApro.Text == "" ? 0 : decimal.Parse(txtMontoApro.Text.Trim());
                                vSolicitud.tipomoneda = long.Parse(ddlTipoMoneda.SelectedValue.ToString());
                                vSolicitud.fechasolicitud = Convert.ToDateTime(txtFechaSolicitud.Text.Trim());
                                vSolicitud.fecha_aprobacion = Convert.ToDateTime(txtFechaDesembolso.Text.Trim());
                                vSolicitud.tipoliquidacion = long.Parse(ddlTipoCuota.SelectedValue.ToString());
                                vSolicitud.plazo = Convert.ToInt64(txtPlazo.Text.Trim());
                                vSolicitud.gracia = Convert.ToInt64(txtGracia.Text.Trim());
                                vSolicitud.tipo_gracia = Convert.ToInt32(ddlTipoGracia.SelectedValue.ToString());
                                vSolicitud.codperiodicidad = long.Parse(ddlPeriodCuotas.SelectedValue.ToString());
                                vSolicitud.estadoobligacion = "P"; // Estado Pendiente de Solicitud
                                vSolicitud.numeropagare = txtPagare.Text == "" ? 0 : Convert.ToInt64(txtPagare.Text.Trim());
                                vSolicitud.codentidad = long.Parse(ddlEntidad.SelectedValue.ToString());
                                vSolicitud.codlineaobligacion = long.Parse(ddlLineaObligacion.SelectedValue);
                                string separdor = GlobalWeb.gSeparadorDecimal;
                                vSolicitud.calculocomponente = rbCalculoTasa.SelectedIndex + 1;
                                if (rbCalculoTasa.SelectedIndex == 1)
                                {
                                    vSolicitud.tipo_historico = long.Parse(ddlTipoTasa.SelectedValue);
                                    vSolicitud.cod_tipo_tasa = 0;
                                }
                                else
                                {
                                    vSolicitud.tipo_historico = 0;
                                    vSolicitud.cod_tipo_tasa = long.Parse(ddlTipoTasa.SelectedValue);
                                }
                                vSolicitud.tasa = txtValorTasa.Text == "" ? 0 : Convert.ToDecimal(txtValorTasa.Text.Trim());
                                vSolicitud.spread = txtPuntosads.Text == "" ? 0 : decimal.Parse(txtPuntosads.Text);
                                vSolicitud.cuenta = ddlCuentas.SelectedValue.ToString();
                            }
                        }
                    }
                }
                // Se crea el registro de la solicitud
                if (vSolicitud.montosolicitado != 0)
                {
                    vSolicitud = SolicitudServicio.CrearSolicitud(vSolicitud, dtComp, dtPagos, (Usuario)Session["usuario"]);
                    idObjeto = vSolicitud.codobligacion.ToString();
                    Session[SolicitudServicio.CodigoPrograma + ".id"] = idObjeto;
                    lblNroObligacion.Text = idObjeto;
                    try
                    {
                        txtValSol.Text = txtMontoSol.Text;
                    }
                    catch
                    {
                    }

                    // Se inactivan los campos para que no se pueda modificar
                    mvSolicitud.ActiveViewIndex = 1;
                    LlenarListBoxDescuentos(lbxValDescon);
                    Actualizar();
                }
                else
                {
                    VerError("Debe digitar los datos completos de monto solicitado, fecha de desembolso, tasa, plazo");
                }

            }
            else
            {
                VerError("");
                mvSolicitud.ActiveViewIndex = 2;
                Site toolBar = (Site)this.Master;
                toolBar.MostrarGuardar(false);
                lblMensajeGrabar.Text = "Se grabo correctamente la obligación, No." + idObjeto;
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(SolicitudServicio.CodigoPrograma, "btnGuardar_Click", ex);
        }
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Navegar("../../../General/Global/inicio.aspx");
    }

    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            Xpinn.Obligaciones.Entities.Solicitud vSolicitud = new Xpinn.Obligaciones.Entities.Solicitud();

            vSolicitud = SolicitudServicio.ConsultarSolicitud(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);

            if (!string.IsNullOrEmpty(vSolicitud.codentidad.ToString()))
                ddlEntidad.SelectedValue = vSolicitud.codentidad.ToString();
            if (!string.IsNullOrEmpty(vSolicitud.montosolicitado.ToString()))
                txtMontoSol.Text = vSolicitud.montosolicitado.ToString();
            if (!string.IsNullOrEmpty(vSolicitud.montoaprobado.ToString()))
                txtMontoApro.Text = vSolicitud.montoaprobado.ToString();
            if (!string.IsNullOrEmpty(vSolicitud.tipomoneda.ToString()))
                ddlTipoMoneda.SelectedValue = vSolicitud.tipomoneda.ToString();
            if (!string.IsNullOrEmpty(vSolicitud.fechasolicitud.ToString()))
                txtFechaSolicitud.Text = vSolicitud.fechasolicitud.ToString();
            if (!string.IsNullOrEmpty(vSolicitud.fecha_aprobacion.ToString()))
                txtFechaDesembolso.Text = vSolicitud.fecha_aprobacion.ToString();
            if (!string.IsNullOrEmpty(vSolicitud.plazo.ToString()))
                txtPlazo.Text = vSolicitud.plazo.ToString();
            if (!string.IsNullOrEmpty(vSolicitud.gracia.ToString()))
                txtGracia.Text = vSolicitud.gracia.ToString();
            if (!string.IsNullOrEmpty(vSolicitud.tipo_gracia.ToString()))
                ddlTipoGracia.SelectedValue = vSolicitud.tipo_gracia.ToString();
            if (!string.IsNullOrEmpty(vSolicitud.codperiodicidad.ToString()))
                ddlPeriodCuotas.SelectedValue = vSolicitud.codperiodicidad.ToString();
            if (!string.IsNullOrEmpty(vSolicitud.tipoliquidacion.ToString()))
                ddlTipoCuota.SelectedValue = vSolicitud.tipoliquidacion.ToString();
            if (!string.IsNullOrEmpty(vSolicitud.numeropagare.ToString()))
                txtPagare.Text = vSolicitud.numeropagare.ToString();
            if (!string.IsNullOrEmpty(vSolicitud.calculocomponente.ToString()))
                rbCalculoTasa.SelectedValue = vSolicitud.calculocomponente.ToString();
            if (vSolicitud.calculocomponente == 2)
            {
                LlenarComboTipoTasa(ddlTipoTasa, false);
                if (!string.IsNullOrEmpty(vSolicitud.tipo_historico.ToString()))
                    ddlTipoTasa.SelectedValue = vSolicitud.tipo_historico.ToString();
            }
            else
            {
                LlenarComboTipoTasa(ddlTipoTasa, true);
                if (!string.IsNullOrEmpty(vSolicitud.cod_tipo_tasa.ToString()))
                    ddlTipoTasa.SelectedValue = vSolicitud.cod_tipo_tasa.ToString();
            }
            ddlTipoTasa.SelectedItem.Text = vSolicitud.cod_tipo_tasa.ToString();
            if (!string.IsNullOrEmpty(vSolicitud.tasa.ToString()))
                txtValorTasa.Text = vSolicitud.tasa.ToString();
            if (!string.IsNullOrEmpty(vSolicitud.spread.ToString()))
                txtPuntosads.Text = vSolicitud.spread.ToString();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(SolicitudServicio.CodigoPrograma, "ObtenerDatos", ex);
        }
    }

    protected void LlenarComboEntidades(DropDownList ddlEntidades)
    {
        Xpinn.Caja.Services.BancosService bancoService = new Xpinn.Caja.Services.BancosService();
        Xpinn.Caja.Entities.Bancos banco = new Xpinn.Caja.Entities.Bancos();
        ddlEntidades.DataSource = bancoService.ListarBancos(banco, (Usuario)Session["usuario"]);
        ddlEntidades.DataTextField = "nombrebanco";
        ddlEntidades.DataValueField = "cod_banco";
        ddlEntidades.DataBind();
        ddlEntidades.Items.Insert(0, new ListItem("Seleccionar...", "0"));
    }

    protected void LlenarComboMonedas(DropDownList ddlMonedas)
    {

        Xpinn.Caja.Services.TipoMonedaService monedaService = new Xpinn.Caja.Services.TipoMonedaService();
        Xpinn.Caja.Entities.TipoMoneda moneda = new Xpinn.Caja.Entities.TipoMoneda();
        ddlMonedas.DataSource = monedaService.ListarTipoMoneda(moneda, (Usuario)Session["usuario"]);
        ddlMonedas.DataTextField = "descripcion";
        ddlMonedas.DataValueField = "cod_moneda";
        ddlMonedas.DataBind();
    }

    protected void LlenarComboTipoCuota(DropDownList ddlTipoCuotas)
    {
        Xpinn.Obligaciones.Services.TipoLiquidacionService tipoLiqService = new Xpinn.Obligaciones.Services.TipoLiquidacionService();
        Xpinn.Obligaciones.Entities.TipoLiquidacion tipoLiq = new Xpinn.Obligaciones.Entities.TipoLiquidacion();
        ddlTipoCuotas.DataSource = tipoLiqService.ListarTipoLiquidacion(tipoLiq, (Usuario)Session["usuario"]);
        ddlTipoCuotas.DataTextField = "descripcion";
        ddlTipoCuotas.DataValueField = "codtipoliquidacion";
        ddlTipoCuotas.DataBind();
    }

    protected void LlenarComboPeriodicidadCuota(DropDownList ddlPeriodicidadCuotas)
    {
        Xpinn.Obligaciones.Services.PeriodicidadCuotaService PeriodicidadCuotaService = new Xpinn.Obligaciones.Services.PeriodicidadCuotaService();
        Xpinn.Obligaciones.Entities.PeriodicidadCuota PeriodicidadCuota = new Xpinn.Obligaciones.Entities.PeriodicidadCuota();
        ddlPeriodicidadCuotas.DataSource = PeriodicidadCuotaService.ListarPeriodicidadCuota(PeriodicidadCuota, (Usuario)Session["usuario"]);
        ddlPeriodicidadCuotas.DataTextField = "DESCRIPCION";
        ddlPeriodicidadCuotas.DataValueField = "COD_PERIODICIDAD";
        ddlPeriodicidadCuotas.DataBind();
        ddlPeriodicidadCuotas.Items.Insert(0, new ListItem("Seleccionar...", "0"));
    }

    protected void LlenarComboTipoTasa(DropDownList ddlTipoTasa, Boolean tipo)
    {
        Xpinn.Obligaciones.Services.TipoTasaService tasaService = new Xpinn.Obligaciones.Services.TipoTasaService();
        Xpinn.Obligaciones.Entities.TipoTasa tasa = new Xpinn.Obligaciones.Entities.TipoTasa();
        if (tipo == true)
        {
            ddlTipoTasa.DataSource = tasaService.ListarTipoTasa(tasa, (Usuario)Session["usuario"]);
            txtPuntosads.Text = "0";
            txtPuntosads.Enabled = false;
            txtValorTasa.Enabled = true;
        }
        else
        {
            ddlTipoTasa.DataSource = tasaService.ListarTipoHistorico(tasa, (Usuario)Session["usuario"]);
            txtValorTasa.Enabled = false;
            txtPuntosads.Enabled = true;
        }
        ddlTipoTasa.DataTextField = "NOMBRE";
        ddlTipoTasa.DataValueField = "COD_TIPO_TASA";
        ddlTipoTasa.DataBind();
    }

    protected void LlenarLineaObligacion(DropDownList ddLineaObligacion)
    {
        Xpinn.Obligaciones.Services.LineaObligacionService lineaObService = new Xpinn.Obligaciones.Services.LineaObligacionService();
        Xpinn.Obligaciones.Entities.LineaObligacion lineaOb = new Xpinn.Obligaciones.Entities.LineaObligacion();
        ddLineaObligacion.DataSource = lineaObService.ListarLineaObligacion(lineaOb, (Usuario)Session["usuario"]);
        ddLineaObligacion.DataTextField = "NOMBRELINEA";
        ddLineaObligacion.DataValueField = "CODLINEAOBLIGACION";
        ddLineaObligacion.DataBind();
    }

    protected void gvPagosExt_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Control ctrl = e.Row.FindControl("ddPeriodo");
            if (ctrl != null)
            {
                DropDownList dd = ctrl as DropDownList;

                this.LlenarComboddPeriodo(dd);
            }
        }

    }

    protected void gvComponente_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Control ctrl = e.Row.FindControl("ddlComponente");
            if (ctrl != null)
            {
                DropDownList dd = ctrl as DropDownList;

                this.LlenarComboComponente(dd);
            }
        }

    }

    protected void LlenarComboddPeriodo(DropDownList ddlPeriodo)
    {
        Periodicidad periodo = new Periodicidad();
        List<Periodicidad> LstPeriodo = new List<Periodicidad>();
        LstPeriodo = periodicidadServicio.ListarPeriodicidad(periodo, (Usuario)Session["usuario"]);
        ddlPeriodo.DataSource = LstPeriodo;
        ddlPeriodo.DataTextField = "Descripcion";
        ddlPeriodo.DataValueField = "Codigo";
        ddlPeriodo.DataBind();
        
    }

    protected void LlenarComboComponente(DropDownList ddlComponente)
    {
        Componente component = new Componente();
        List<Componente> LstComponent = new List<Componente>();
        LstComponent = componenteServicio.ListarComponentes(component, (Usuario)Session["usuario"]);
        ddlComponente.DataSource = LstComponent;
        ddlComponente.DataTextField = "NOMBRE";
        ddlComponente.DataValueField = "CODCOMPONENTE";
        ddlComponente.DataBind();
    }

    protected void CrearComponenteInicial()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("componente");
        dt.Columns.Add("formula");
        dt.Columns.Add("valor");
        dt.Columns.Add("chkFin");
        dt.Columns.Add("nomcomponente");
        dt.Columns.Add("nomformula");
        dt.Rows.Add();
        Session["Componentes"] = dt;
        gvComponente.DataSource = dt;
        gvComponente.DataBind();
        gvComponente.Visible = true;

    }

    protected void CrearPagosExtInicial()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("periodo");
        dt.Columns.Add("valor");
        dt.Columns.Add("nomperiodo");
        dt.Rows.Add();
        Session["PagosExt"] = dt;
        gvPagosExt.DataSource = dt;
        gvPagosExt.DataBind();
        gvPagosExt.Visible = true;
    }

    protected void gvComponente_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        VerError("");
        if (e.CommandName.Equals("AddNew"))
        {
            DropDownList ddlComponentes = (DropDownList)gvComponente.FooterRow.FindControl("ddlComponente");
            DropDownList ddlTipoFormula = (DropDownList)gvComponente.FooterRow.FindControl("ddlFormula");
            TextBox txtnewvalore = (TextBox)gvComponente.FooterRow.FindControl("txtnewvalor");
            CheckBox chkFinanciado = (CheckBox)gvComponente.FooterRow.FindControl("chkFinanciado");

            DataTable dtAgre = new DataTable();
            dtAgre = (DataTable)Session["Componentes"];

            if (dtAgre.Rows[0][0] == null || dtAgre.Rows[0][0].ToString() == "")
            {
                dtAgre.Rows[0].Delete();
            }

            DataRow fila = dtAgre.NewRow();

            fila[0] = ddlComponentes.SelectedValue;
            fila[1] = ddlTipoFormula.SelectedValue;
            fila[2] = txtnewvalore.Text;
            if (chkFinanciado.Checked)
            {
                fila[3] = true;
            }
            else
            {
                fila[3] = false;
            }
            try
            {
                fila[4] = ddlComponentes.SelectedItem.Text;// nombre del componente
                fila[5] = ddlTipoFormula.SelectedItem.Text;// nombre de la formula
            }
            catch (Exception ex)
            {
                VerError("Se presento error" + ex.Message);
            }

            dtAgre.Rows.Add(fila);
            gvComponente.DataSource = dtAgre;
            gvComponente.DataBind();
            Session["Componentes"] = dtAgre;
        }
    }

    protected void gvPagosExt_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("AddNew"))
        {
            DropDownList ddlPeriodo = (DropDownList)gvPagosExt.FooterRow.FindControl("ddPeriodo");
            TextBox txtnewvalore = (TextBox)gvPagosExt.FooterRow.FindControl("txtnewvalor");

            DataTable dtAgre = new DataTable();
            dtAgre = (DataTable)Session["PagosExt"];

            if (dtAgre.Rows[0][0] == null || dtAgre.Rows[0][0].ToString() == "")
            {
                dtAgre.Rows[0].Delete();
            }


            DataRow fila = dtAgre.NewRow();

            fila[0] = ddlPeriodo.SelectedValue;
            fila[1] = txtnewvalore.Text;
            fila[2] = ddlPeriodo.SelectedItem.Text;

            dtAgre.Rows.Add(fila);
            gvPagosExt.DataSource = dtAgre;
            gvPagosExt.DataBind();
            Session["PagosExt"] = dtAgre;
        }
    }

    protected void gvComponente_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            DataTable table = new DataTable();
            table = (DataTable)Session["Componentes"];//se pilla los componentes

            if ((e.RowIndex == 0) && (table.Rows[0][0] != null || table.Rows[0][0].ToString() == "") && (table.Rows.Count == 1))
            {
                table.Rows.Add();
            }

            table.Rows[e.RowIndex].Delete();

            gvComponente.DataSource = table;
            gvComponente.DataBind();
            Session["Componentes"] = table;

            if ((e.RowIndex == 0) && (table.Rows[0][0] == null || table.Rows[0][0].ToString() == "") && (table.Rows.Count == 1))
                gvComponente.Rows[0].Visible = false;

        }
        catch (Xpinn.Util.ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(componenteServicio.GetType().Name + "L", "gvComponente_RowDeleting", ex);
        }

    }

    protected void gvPagosExt_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            DataTable table = new DataTable();
            table = (DataTable)Session["PagosExt"];//se pilla los Pagos Extraordinarios

            if ((e.RowIndex == 0) && (table.Rows[0][0] != null || table.Rows[0][0].ToString() == "") && (table.Rows.Count == 1))
            {
                table.Rows.Add();
            }

            table.Rows[e.RowIndex].Delete();
            gvPagosExt.DataSource = table;
            gvPagosExt.DataBind();
            Session["PagosExt"] = table;

            if ((e.RowIndex == 0) && (table.Rows[0][0] == null || table.Rows[0][0].ToString() == "") && (table.Rows.Count == 1))
                gvPagosExt.Rows[0].Visible = false;
        }
        catch (Xpinn.Util.ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(componenteServicio.GetType().Name + "L", "gvComponente_RowDeleting", ex);
        }

    }

    protected void AsignarEventoConfirmar()
    {
        ConfirmarEventoBoton((LinkButton)Master.FindControl("btnGuardar"), "Desea grabar la información de la Obligación?");
    }

    private void Actualizar()
    {
        VerError("");
        try
        {
            List<Xpinn.Obligaciones.Entities.ObPlanPagos> lstConsulta = new List<Xpinn.Obligaciones.Entities.ObPlanPagos>();
            Xpinn.Obligaciones.Entities.ObPlanPagos obPlan = new Xpinn.Obligaciones.Entities.ObPlanPagos();
            Xpinn.Obligaciones.Services.ObPlanPagosService obPlanService = new Xpinn.Obligaciones.Services.ObPlanPagosService();

            obPlan.cod_obligacion = long.Parse(idObjeto);
            obPlan.tasa_efectiva = ddlTipoTasa.SelectedValue == "" ? 0 : long.Parse(ddlTipoTasa.SelectedValue);
            obPlan.tasa_per = lblTasaIntPer.Text == "" ? 0 : long.Parse(lblTasaIntPer.Text);
            obPlan.cuota = lblCuota.Text == "" ? 0 : long.Parse(lblCuota.Text);

            lstConsulta = obPlanService.ListarObPlanPagos(obPlan, (Usuario)Session["usuario"]);

            gvObPlan.DataSource = lstConsulta;

            if (lstConsulta.Count > 0)
            {
                gvObPlan.Visible = true;
                gvObPlan.DataBind();
            }

            ObPlanPagosService planpagosservicio = new ObPlanPagosService();

            ObPlanPagos planpagos = new ObPlanPagos();
            planpagos = planpagosservicio.ConsultarObcomponente(Convert.ToInt64(idObjeto), (Usuario)Session["usuario"]);


            if (!string.IsNullOrEmpty(planpagos.tasa.ToString()))
                lblTasaIntPer.Text = Convert.ToString(planpagos.tasa.ToString());

            if (!string.IsNullOrEmpty(planpagos.cuota.ToString()))
                lblCuota.Text = Convert.ToString(planpagos.cuota.ToString("##,##0"));

            if (!string.IsNullOrEmpty(planpagos.tipo_tasa.ToString()))
            {
                lblTasaEfectiva.Text = Convert.ToString(planpagos.tipo_tasa.ToString());
            }
            else
            {
                gvObPlan.Visible = false;
            }

            Session.Add(ObligacionCreditoServicio.CodigoPrograma2 + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ObligacionCreditoServicio.CodigoPrograma2, "Actualizar", ex);
        }
    }

    private void Consultar()
    {
        VerError("");
        try
        {
            List<Xpinn.Obligaciones.Entities.ObPlanPagos> lstConsulta = new List<Xpinn.Obligaciones.Entities.ObPlanPagos>();
            Xpinn.Obligaciones.Entities.ObPlanPagos obPlan = new Xpinn.Obligaciones.Entities.ObPlanPagos();
            Xpinn.Obligaciones.Services.ObPlanPagosService obPlanService = new Xpinn.Obligaciones.Services.ObPlanPagosService();

            obPlan.cod_obligacion = long.Parse(idObjeto);
            obPlan.tasa_efectiva = (ddlTipoTasa.SelectedValue == "" ? 0 : long.Parse(ddlTipoTasa.SelectedValue));

            lstConsulta = obPlanService.ConsultarObPlanPagos(obPlan, (Usuario)Session["usuario"]);

            gvObPlan.DataSource = lstConsulta;

            if (lstConsulta.Count > 0)
            {
                gvObPlan.Visible = true;
                gvObPlan.DataBind();

            }

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ObligacionCreditoServicio.CodigoPrograma6, "Actualizar", ex);
        }
    }

    private void actualizarcomponentes(String pIdObjeto)
    {
        ObPlanPagosService planpagosservicio = new ObPlanPagosService();
        idObjeto = Session[ObligacionCreditoServicio.CodigoPrograma6 + ".id"].ToString();

        ObPlanPagos planpagos = new ObPlanPagos();
        planpagos = planpagosservicio.ConsultarObcomponente(Convert.ToInt64(idObjeto), (Usuario)Session["usuario"]);

        if (!string.IsNullOrEmpty(planpagos.tasa.ToString()))
            lblTasaIntPer.Text = Convert.ToString(planpagos.tasa.ToString("##,##0"));

        if (!string.IsNullOrEmpty(planpagos.cuota.ToString()))
            lblCuota.Text = Convert.ToString(planpagos.cuota.ToString("##,##0"));

        if (!string.IsNullOrEmpty(planpagos.tipo_tasa.ToString()))
            lblTasaEfectiva.Text = Convert.ToString(planpagos.tipo_tasa.ToString());

    }


    protected void LlenarListBoxDescuentos(ListBox LstBoxDescon)
    {
        List<Xpinn.Obligaciones.Entities.ComponenteAdicional> lstConsulta = new List<Xpinn.Obligaciones.Entities.ComponenteAdicional>();
        Xpinn.Obligaciones.Services.ComponenteAdicionalService obCompAdService = new Xpinn.Obligaciones.Services.ComponenteAdicionalService();
        lstConsulta = obCompAdService.ListarComponenteAdicional(long.Parse(idObjeto), (Usuario)Session["usuario"]);
        LstBoxDescon.DataSource = lstConsulta;
        LstBoxDescon.DataTextField = "DESCRIPCION";
        LstBoxDescon.DataBind();

    }



    protected void gvObPlan_RowCommand(object sender, GridViewCommandEventArgs evt)
    {

        if (evt.CommandName == "DetallePago")
        {

            int index = Convert.ToInt32(evt.CommandArgument);

            GridViewRow gvObPlanRow = gvObPlan.Rows[index];

            txtFechaCuota.Text = gvObPlan.Rows[index].Cells[2].Text;
            txtNroCuota.Text = gvObPlan.Rows[index].Cells[1].Text;
            txtCapital.Text = gvObPlan.Rows[index].Cells[3].Text;
            txtIntCorr.Text = gvObPlan.Rows[index].Cells[4].Text;
            txtIntMora.Text = "0";
            txtSeguro.Text = "0";
            mpeRegObPlanPago.Show();
        }
    }


    protected void AceptarButton_Click(object sender, EventArgs e)
    {
        Xpinn.Obligaciones.Services.ObPlanPagosService obPLanService = new Xpinn.Obligaciones.Services.ObPlanPagosService();
        Xpinn.Obligaciones.Entities.ObPlanPagos obPlan = new Xpinn.Obligaciones.Entities.ObPlanPagos();

        obPlan.cod_obligacion = long.Parse(idObjeto);
        obPlan.nrocuota = long.Parse(txtNroCuota.Text);
        obPlan.fecha = Convert.ToDateTime(txtFechaCuota.Text);
        obPlan.amort_cap = txtCapital.Text == "" ? 0 : decimal.Parse(txtCapital.Text);
        obPlan.interes_corriente = txtIntCorr.Text == "" ? 0 : decimal.Parse(txtIntCorr.Text);
        obPlan.interes_mora = txtIntMora.Text == "" ? 0 : decimal.Parse(txtIntMora.Text);
        obPlan.seguro = txtSeguro.Text == "" ? 0 : decimal.Parse(txtSeguro.Text);

        obPlan = obPLanService.ModificarPlanPagos(obPlan, (Usuario)Session["usuario"]);

        mpeRegObPlanPago.Hide();
        Consultar();
    }

    protected void ddlLineaObligacion_SelectedIndexChanged(object sender, EventArgs e)
    {
        CambiarDatoTipoCuota();
    }

    protected void CambiarDatoTipoCuota()
    {
        Xpinn.Obligaciones.Services.LineaObligacionService LineaObService = new Xpinn.Obligaciones.Services.LineaObligacionService();
        Xpinn.Obligaciones.Entities.LineaObligacion lineaOb = new Xpinn.Obligaciones.Entities.LineaObligacion();

        if (ddlLineaObligacion.SelectedValue != "")
        {
            lineaOb = LineaObService.ConsultarLineaOb(long.Parse(ddlLineaObligacion.SelectedValue), (Usuario)Session["usuario"]);
            ddlTipoCuota.SelectedValue = lineaOb.TIPOLIQUIDACION.ToString();
        }
    }

    /// <summary>
    /// Método para terminar el proceso
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnContinuar_Click(object sender, EventArgs e)
    {
        idObjeto = "";
        Navegar("../../../General/Global/inicio.aspx");
        Session.Remove(SolicitudServicio.CodigoPrograma + ".id");
    }

    protected void rbCalculoTasa_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtValorTasa.Text = "";
        txtPuntosads.Text = "";
        if (rbCalculoTasa.SelectedIndex == 1)
            LlenarComboTipoTasa(ddlTipoTasa, false);
        else
            LlenarComboTipoTasa(ddlTipoTasa, true);
        ddlTipoTasa_SelectedIndexChanged(ddlTipoTasa, null);
    }

    protected void ddlTipoTasa_SelectedIndexChanged(object sender, EventArgs e)
    {
        VerError("");
        if (rbCalculoTasa.SelectedIndex == 1)
        {
            Xpinn.Obligaciones.Services.TipoTasaService tasaService = new Xpinn.Obligaciones.Services.TipoTasaService();
            Xpinn.Obligaciones.Entities.TipoTasa tasa = new Xpinn.Obligaciones.Entities.TipoTasa();
            try
            {
                double Valortasa = tasaService.ConsultaTasaHistorica(Convert.ToInt64(ddlTipoTasa.SelectedValue.ToString()), Convert.ToDateTime(txtFechaSolicitud.Text), (Usuario)Session["usuario"]);
                Valortasa = Math.Round(Valortasa, 2);
                txtValorTasa.Text = Convert.ToString(Valortasa).Replace(gSeparadorDecimal, ".");
                Int32 posicionSeparadorDecimal = txtValorTasa.Text.IndexOf(".", 0);
                string sdec = txtValorTasa.Text.Substring(posicionSeparadorDecimal+1);
                Int64 decimales = ConvertirStringToInt(sdec);
                if (decimales < 10)
                    txtValorTasa.Text += "0";
            }
            catch (Exception ex)
            {
                VerError("Error al determinar el valor de la tasa histórica " + ex.Message);
            }
        }
    }

    protected void CargarNit_SelectedIndexChanged(object sender, EventArgs e)
    {
        string iden_entidad = "";
        string codigo = "";
        codigo = ddlEntidad.SelectedValue;
        Xpinn.Caja.Services.BancosService bancoService = new Xpinn.Caja.Services.BancosService();
        iden_entidad = bancoService.ConsultaBancoPersona(codigo , (Usuario)Session["usuario"]);
        TxtNit.Text = Convert.ToString(iden_entidad);
    }


    protected void CargarCuentas()
    {
        Xpinn.Tesoreria.Services.CuentasBancariasServices CuentaService = new Xpinn.Tesoreria.Services.CuentasBancariasServices();
        List<Xpinn.Tesoreria.Entities.CuentasBancarias> lstConsulta = new List<Xpinn.Tesoreria.Entities.CuentasBancarias>();
        ddlCuentas.DataSource = CuentaService.ListarCuentasBancarias("", (Usuario)Session["usuario"]);
        ddlCuentas.DataTextField = "cod_cuenta";
        ddlCuentas.DataValueField = "cod_cuenta";
        ddlCuentas.DataBind();
        ddlCuentas.Items.Insert(0, new ListItem("Seleccionar...", "0"));
    }

    protected void ddlCuentas_SelectedIndexChanged(object sender, EventArgs e)
    {    

    }

}