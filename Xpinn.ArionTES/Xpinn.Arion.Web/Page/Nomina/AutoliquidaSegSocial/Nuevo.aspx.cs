using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Nomina.Services;
using Xpinn.Nomina.Entities;
using Xpinn.Util;


public partial class Page_Nomina_AutoliquidaSegSocial_Nuevo : GlobalWeb
{
    SeguridadSocialServices _SeguridadSocialService = new SeguridadSocialServices();
    Xpinn.Caja.Services.BancosService bancoService = new Xpinn.Caja.Services.BancosService();
    Xpinn.Caja.Entities.Bancos banco = new Xpinn.Caja.Entities.Bancos();

    Xpinn.Tesoreria.Services.CuentasBancariasServices CuentasService = new Xpinn.Tesoreria.Services.CuentasBancariasServices();
    Xpinn.Tesoreria.Entities.CuentasBancarias CuentaBanc = new Xpinn.Tesoreria.Entities.CuentasBancarias();

    public void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(_SeguridadSocialService.CodigoPrograma, "E");
            Site toolBar = (Site)Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            
            toolBar.MostrarRegresar(false);
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
     

        if (!IsPostBack)
        {
            ObtenerDatos();
        }
    }
    void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        if (ValidarCampos())
        {
            Guardar();
        }
       
    }
    bool ValidarCampos()
    {
        if(rdbSi1.Checked==false && rdbNo1.Checked == false)
        {
            return false;
        }
        if (rdbSi2.Checked==false && rdbNo2.Checked==false)
        {
            return false;
        }
        if (rdbSi3.Checked == false && rdbNo3.Checked == false)
        {
            return false;
        }
        if (rdbSi4.Checked == false && rdbNo4.Checked == false)
        {
            return false;
        }
        if (rdbSi5.Checked == false && rdbNo5.Checked == false)
        {
            return false;
        }
        if (rdbSi6.Checked == false && rdbNo6.Checked == false)
        {
            return false;
        }
        if (rdbEmpleado1.Checked == false && rdbNomina1.Checked == false && rdbCentroCostos1.Checked==false)
        {
            return false;
        }
        if (rdbMesIbcanterior.Checked == false && rdbSinImportar.Checked == false && rdbsinnovedad.Checked==false)
        {
            return false;
        }
        if (rdbCalcIBCempleado.Checked == false && rdbCalcSMLVemple.Checked == false)
        {
            return false;
        }
        if (rdbVacacionesIBC.Checked == false && rdbVacacionespag.Checked == false)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    void Guardar()
    {
        try
        {
            Xpinn.Nomina.Entities.SeguridadSocial lstConsultar = new Xpinn.Nomina.Entities.SeguridadSocial();
            lstConsultar = _SeguridadSocialService.ConsultarSeguridadSocial((Usuario)Session["usuario"]);
            Session["IDSEGURIDADSOC"] = lstConsultar.IDSEGURIDAD;
            lstConsultar.PORCENTAJE_SALUD = ConvertirStringToDecimal(txtFondoSalud.Text);
            lstConsultar.PORCENTAJE_PENSION = ConvertirStringToDecimal(txtFondoPension.Text);
            lstConsultar.PORC_EMPLEADOR_SALUD = ConvertirStringToDecimal(txtPorcEmpleadoSalud.Text);
            lstConsultar.PORC_EMPLEADOR_PENSION = ConvertirStringToDecimal(txtPorcEmlpension.Text);
            lstConsultar.PORCENTAJE_INCAPACIDAD = ConvertirStringToDecimal(txtporIncapacidad.Text);
            lstConsultar.PORCENTAJE_SALUD_PENSIONADO = ConvertirStringToDecimal(txtPorcSaludpensionado.Text);
            lstConsultar.PERIODOS_MAXIMOS_VACACIONES = ConvertirStringToDecimal(txtperiodosvacaciones.Text);
            lstConsultar.MAXSALARIOSARL = Convert.ToInt32(txtSMLVmaxArl.Text);
            lstConsultar.MAXSALARIOSPARAFISCALES = Convert.ToInt32(txtSMLVmaxParafiscales.Text);
            lstConsultar.MAXSALARIOSSALUDPENSION = Convert.ToInt32(txtSMLVmaxSaludPension.Text);
            lstConsultar.CajaCompensacion = ConvertirStringToDecimal(txtcajacompensacion.Text);
            lstConsultar.sena = ConvertirStringToDecimal(Txtsena.Text);
            lstConsultar.icbf = ConvertirStringToDecimal(txtIcbf.Text);

            lstConsultar.prima = ConvertirStringToDecimal(txtPrimaServicios.Text);
            lstConsultar.cesantias = ConvertirStringToDecimal(TxtCesantias.Text);
            lstConsultar.interescesantias = ConvertirStringToDecimal(TxtInteresCesantias.Text);
            lstConsultar.vacaciones = ConvertirStringToDecimal(txtVacaciones.Text);
            lstConsultar.diasvacaciones = Convert.ToInt16(txtDiaVacaciones.Text);
            lstConsultar.diasminimoprima = Convert.ToInt16(txtPrimaServiciosDias.Text);

            lstConsultar.aprobador = Convert.ToString(txtaprobador.Text);
            lstConsultar.revisor = Convert.ToString(txtrevisor.Text);

            lstConsultar.PORCENTAJE_SALARIO_INTEGRAL = ConvertirStringToDecimal(txtporSalarioIntegral.Text);
            lstConsultar.porcentaje_retencion = ConvertirStringToDecimal(txtPorcRetencion.Text);
            lstConsultar.cantidadsalretencion = Convert.ToInt16(txtCantidadSalRetencion.Text);
            lstConsultar.uvt = ConvertirStringToDecimal(txtuvt.Text);
            lstConsultar.baseRTE = Convert.ToDecimal(TXTbaseRTE.Text);
            lstConsultar.basemax = Convert.ToInt16(txtBaseMaxima.Text);
            lstConsultar.codigobanco = Convert.ToInt16(ddlEntidadOrigen.SelectedValue);
            lstConsultar.Cuentabancaria = Convert.ToInt16(ddlCuentaOrigen.SelectedValue);

            int Si = 1, No = 2;
            #region Selecciones
            if (rdbSi1.Checked)
            {
                lstConsultar.PERMITE_INCAPACIDAD_TOPE = Si;
            }
            if (rdbNo1.Checked)
            {
                lstConsultar.PERMITE_INCAPACIDAD_TOPE = No;
            }
            if (rdbSi3.Checked)
            {
                lstConsultar.DESCONTAR_APORTES = Si;
            }
            if (rdbNo3.Checked)
            {
                lstConsultar.DESCONTAR_APORTES = No;
            }
            if (rdbSi2.Checked)
            {
                lstConsultar.MARCAR_VST = Si;
            }
            if (rdbNo2.Checked)
            {
                lstConsultar.MARCAR_VST = No;
            }
            if (rdbSi4.Checked)
            {
                lstConsultar.DESCONTAR_APORTE_EMPL = Si;
            }
            if (rdbNo4.Checked)
            {
                lstConsultar.DESCONTAR_APORTE_EMPL = No;
            }
            if (rdbSi5.Checked)
            {
                lstConsultar.INACTIVIDAD_DIAS_CAL = Si;
            }
            if (rdbNo5.Checked)
            {
                lstConsultar.INACTIVIDAD_DIAS_CAL = No;
            }
            if (rdbSi6.Checked)
            {
                lstConsultar.DESCUENTA_DIAS_CASTIGO = Si;
            }
            if (rdbNo6.Checked)
            {
                lstConsultar.DESCUENTA_DIAS_CASTIGO = No;
            }
            if (chkEmpresa.Checked)
            {
                lstConsultar.NIT_ARCHIVO = Si;
            }
            if (chkNomina.Checked)
            {
                lstConsultar.NIT_ARCHIVO = No;
            }
            if (chk30dias.Checked)
            {
                lstConsultar.BASE_INACTIVIDAD_DIAS = Si;
            }
            if (chkTomarValor.Checked)
            {
                lstConsultar.BASE_INACTIVIDAD_DIAS = No;
            }
            if (rdbEmpleado1.Checked)
            {
                lstConsultar.PROCEDIMIENTO_CENTRO_ARP = Si;
            }
            if (rdbNomina1.Checked)
            {
                lstConsultar.PROCEDIMIENTO_CENTRO_ARP = No;
            }
            if (rdbCentroCostos1.Checked)
            {
                lstConsultar.PROCEDIMIENTO_CENTRO_ARP = 3;
            }
            if (rdbMesIbcanterior.Checked)
            {
                lstConsultar.IBC_INACTIVIDADES = Si;
            }
            if (rdbsinnovedad.Checked)
            {
                lstConsultar.IBC_INACTIVIDADES = No;
            }
            if (rdbSinImportar.Checked)
            {
                lstConsultar.IBC_INACTIVIDADES = 3;
            }
            if (rdbCalcSMLVemple.Checked)
            {
                lstConsultar.CALCULO_PRIMDIAS = Si;
            }
            if (rdbCalcIBCempleado.Checked)
            {
                lstConsultar.CALCULO_PRIMDIAS = No;
            }
            if (rdbVacacionespag.Checked)
            {
                lstConsultar.SALPEN_VACACIONES = Si;
            }
            if (rdbVacacionesIBC.Checked)
            {
                lstConsultar.SALPEN_VACACIONES = No;
            }

            int YES = 1, Not = 0;

            if (rdbRTESi.Checked)
            {
                lstConsultar.RegimenTEspecial = YES;
            }
            if (rdbRTENo.Checked)
            {
                lstConsultar.RegimenTEspecial = Not;
            }

            if (rdbContribuyenteSi.Checked)
            {
                lstConsultar.Contribuyente = YES;
                if (chkSalud.Checked)
                    lstConsultar.SaludContribuyente = YES;
                else
                    lstConsultar.SaludContribuyente = Not;
                if (chkSena.Checked)
                    lstConsultar.SenaContribuyente = YES;
                else
                    lstConsultar.SenaContribuyente = Not;

                if (chkICBF.Checked)
                    lstConsultar.icbfContribuyente = YES;
                else
                    lstConsultar.icbfContribuyente = Not;

                if (chkCCF.Checked)
                    lstConsultar.ccfContribuyente = YES;
                else
                    lstConsultar.ccfContribuyente = Not;

            }
            if (rdbContribuyenteNo.Checked)
            {
                lstConsultar.Contribuyente = Not;
                lstConsultar.SaludContribuyente = Not;
                lstConsultar.SenaContribuyente = Not;
                lstConsultar.icbfContribuyente = Not;
                lstConsultar.ccfContribuyente = Not;

            }

            if (rdbAproxSi.Checked)
            {
                lstConsultar.ManejaAproximacion = YES;
                if (rdbCentesima.Checked)
                {
                    lstConsultar.AproxCentesima = YES;
                    lstConsultar.AproxMilesima = Not;
                    lstConsultar.Aprox50mascercano = Not;
                }
                if (rdbMilesima.Checked)
                {
                    lstConsultar.AproxMilesima = YES;
                    lstConsultar.AproxCentesima = Not;
                    lstConsultar.Aprox50mascercano = Not;
                }

                if (rdb50cercano.Checked)
                {
                    lstConsultar.Aprox50mascercano = YES;
                    lstConsultar.AproxCentesima = Not;
                    lstConsultar.AproxMilesima = Not;
                }

            }

            if (rdbAproxNo.Checked)
            {
                lstConsultar.ManejaAproximacion = Not;
                lstConsultar.AproxCentesima = Not;
                lstConsultar.AproxMilesima = Not;
            }
            if (rdbTerceroEmp.Checked)
            { 
                lstConsultar.Tercero = 0;
            
             }
            if (rdbTerceroEntidad.Checked)
            {
                lstConsultar.Tercero = 1;

            }

            if (rdbNovVacacionesNo.Checked)
            {
                lstConsultar.novvacaciones = 0;

            }
            if (rdbNovVacacionesSi.Checked)
            {
                lstConsultar.novvacaciones = 1;

            }



            if (rdbVacacionesAntNo.Checked)
            {
                lstConsultar.vacacionesanticipadas = 0;

            }
            if (rdbVacacionesAntSi.Checked)
            {
                lstConsultar.vacacionesanticipadas = 1;

            }



            if (rdbRetroactivoSi.Checked)
            {
                lstConsultar.retroactivo = 1;

            }
            if (rdbRetroactivoNo.Checked)
            {
                lstConsultar.retroactivo = 0;

            }


            if (rdbInaSobreSMLVSI.Checked)
            {
                lstConsultar.incap_smlv = 1;

            }
            if (rdbInaSobreSMLVNO.Checked)
            {
                lstConsultar.incap_smlv = 0;

            }

            lstConsultar.diasincapacidades = Convert.ToInt16(txtIncapacidadesDias.Text);


            if (rdbDeprendible1.Checked)
            {
                lstConsultar.formato_desprendible = 1;

            }
            if (rdbDeprendible2.Checked)
            {
                lstConsultar.formato_desprendible = 2;

            }



            #endregion

            

            if (lstConsultar.IDSEGURIDAD==0)
            {
            
                lstConsultar.IDSEGURIDAD = Si;
                _SeguridadSocialService.CrearSeguridadSocial(lstConsultar,(Usuario)Session["usuario"]);
           
            }
            else
            {

                _SeguridadSocialService.ModificarSeguridadSocial(lstConsultar, (Usuario)Session["usuario"]);
            }
            Site toolBar = (Site)this.Master;
            toolBar.MostrarGuardar(false);
            mvDatos.ActiveViewIndex = 1;
        }
        catch (Exception ex)
        {

            error.Text="No se pudo guardar por: "+ex;
        }
    }
    public void ObtenerDatos()
    {
       


        try
        {
            Xpinn.Nomina.Entities.SeguridadSocial lstConsultar = new Xpinn.Nomina.Entities.SeguridadSocial();
            lstConsultar = _SeguridadSocialService.ConsultarSeguridadSocial((Usuario)Session["usuario"]);

            if (lstConsultar.IDSEGURIDAD != 0)
            {
                Session["IDSEGURIDADSOC"] = lstConsultar.IDSEGURIDAD;

                txtFondoSalud.Text = lstConsultar.PORCENTAJE_SALUD.ToString();
                txtFondoPension.Text = lstConsultar.PORCENTAJE_PENSION.ToString();
                txtPorcEmpleadoSalud.Text = lstConsultar.PORC_EMPLEADOR_SALUD.ToString();
                txtPorcEmlpension.Text = lstConsultar.PORC_EMPLEADOR_PENSION.ToString();
                txtporIncapacidad.Text = lstConsultar.PORCENTAJE_INCAPACIDAD.ToString();               
                txtPorcSaludpensionado.Text = lstConsultar.PORCENTAJE_SALUD_PENSIONADO.ToString();
                txtperiodosvacaciones.Text = lstConsultar.PERIODOS_MAXIMOS_VACACIONES.ToString();

                rdbSi1.Checked = lstConsultar.PERMITE_INCAPACIDAD_TOPE == 1 ? true : false;
                rdbNo1.Checked = lstConsultar.PERMITE_INCAPACIDAD_TOPE == 2 ? true : false;

                rdbSi2.Checked = lstConsultar.DESCONTAR_APORTES == 1 ? true : false;
                rdbNo2.Checked = lstConsultar.DESCONTAR_APORTES == 2 ? true : false;

                rdbSi3.Checked = lstConsultar.MARCAR_VST == 1 ? true : false;
                rdbNo3.Checked = lstConsultar.MARCAR_VST == 2 ? true : false;

                rdbSi4.Checked = lstConsultar.DESCONTAR_APORTE_EMPL == 1 ? true : false;
                rdbNo4.Checked = lstConsultar.DESCONTAR_APORTE_EMPL == 2 ? true : false;

                rdbSi5.Checked = lstConsultar.INACTIVIDAD_DIAS_CAL == 1 ? true : false;
                rdbNo5.Checked = lstConsultar.INACTIVIDAD_DIAS_CAL == 2 ? true : false;

                rdbSi6.Checked = lstConsultar.DESCUENTA_DIAS_CASTIGO == 1 ? true : false;
                rdbNo6.Checked = lstConsultar.DESCUENTA_DIAS_CASTIGO == 2 ? true : false;

                chkEmpresa.Checked = lstConsultar.NIT_ARCHIVO == 1 ? true : false;
                chkNomina.Checked = lstConsultar.NIT_ARCHIVO == 2 ? true : false;

                chk30dias.Checked = lstConsultar.BASE_INACTIVIDAD_DIAS == 1 ? true : false;
                chkTomarValor.Checked = lstConsultar.BASE_INACTIVIDAD_DIAS == 2 ? true : false;

                rdbEmpleado1.Checked = lstConsultar.PROCEDIMIENTO_CENTRO_ARP == 1 ? true : false;
                rdbNomina1.Checked = lstConsultar.PROCEDIMIENTO_CENTRO_ARP == 2 ? true : false;
                rdbCentroCostos1.Checked = lstConsultar.PROCEDIMIENTO_CENTRO_ARP == 3 ? true : false;

                rdbMesIbcanterior.Checked = lstConsultar.IBC_INACTIVIDADES == 1 ? true : false;
                rdbSinImportar.Checked = lstConsultar.IBC_INACTIVIDADES == 2 ? true : false;
                rdbsinnovedad.Checked = lstConsultar.IBC_INACTIVIDADES == 3 ? true : false;

                rdbCalcIBCempleado.Checked = lstConsultar.CALCULO_PRIMDIAS == 1 ? true : false;
                rdbCalcSMLVemple.Checked = lstConsultar.CALCULO_PRIMDIAS == 2 ? true : false;

                rdbVacacionespag.Checked = lstConsultar.SALPEN_VACACIONES == 1 ? true : false;
                rdbVacacionesIBC.Checked = lstConsultar.SALPEN_VACACIONES == 2 ? true : false;

                txtSMLVmaxArl.Text = lstConsultar.MAXSALARIOSARL.ToString();
                txtSMLVmaxParafiscales.Text = lstConsultar.MAXSALARIOSPARAFISCALES.ToString();
                txtSMLVmaxSaludPension.Text = lstConsultar.MAXSALARIOSSALUDPENSION.ToString();


                txtcajacompensacion.Text = lstConsultar.CajaCompensacion.ToString();
                Txtsena.Text = lstConsultar.sena.ToString();
                txtIcbf.Text = lstConsultar.icbf.ToString();

                txtVacaciones.Text = lstConsultar.vacaciones.ToString();
                txtPrimaServicios.Text = lstConsultar.prima.ToString();
                TxtCesantias.Text = lstConsultar.cesantias.ToString();
                TxtInteresCesantias.Text = lstConsultar.interescesantias.ToString();
                txtDiaVacaciones.Text = lstConsultar.diasvacaciones.ToString();
                txtPrimaServiciosDias.Text = lstConsultar.diasminimoprima.ToString();

                txtporSalarioIntegral.Text = lstConsultar.PORCENTAJE_SALARIO_INTEGRAL.ToString();
                txtuvt.Text = lstConsultar.uvt.ToString();
                txtBaseMaxima.Text = lstConsultar.basemax.ToString();

                ddlEntidadOrigen.DataSource = bancoService.ListarBancosEntidad(banco, (Usuario)Session["usuario"]);
                ddlEntidadOrigen.DataTextField = "nombrebanco";
                ddlEntidadOrigen.DataValueField = "cod_banco";
                ddlEntidadOrigen.DataBind();
              


                ddlEntidadOrigen.SelectedValue = lstConsultar.codigobanco.ToString();
                CargarCuentas();
                ddlCuentaOrigen.SelectedValue = lstConsultar.Cuentabancaria.ToString();

                if (lstConsultar.aprobador != null)

                {
                    txtaprobador.Text = lstConsultar.aprobador.ToString().ToUpper();
                }
                if (lstConsultar.revisor!= null)

                {
                    txtrevisor.Text = lstConsultar.revisor.ToString().ToUpper();
                }

                txtPorcRetencion.Text= lstConsultar.porcentaje_retencion.ToString();
                txtuvt.Text = lstConsultar.uvt.ToString();

                txtCantidadSalRetencion.Text= lstConsultar.cantidadsalretencion.ToString();
                rdbRTESi.Checked = lstConsultar.RegimenTEspecial == 1 ? true : false;
                rdbRTENo.Checked = lstConsultar.RegimenTEspecial == 0 ? true : false;

              
                rdbContribuyenteSi.Checked = lstConsultar.Contribuyente == 1 ? true : false;
                rdbContribuyenteNo.Checked = lstConsultar.Contribuyente == 0 ? true : false;
                TXTbaseRTE.Text = lstConsultar.baseRTE.ToString();

                if (rdbContribuyenteNo.Checked)
                {
                    Panel5.Visible = false;

                }
                if (rdbContribuyenteSi.Checked)
                {
                    Panel5.Visible = true;
                    chkSalud.Checked = lstConsultar.SaludContribuyente == 1 ? true : false;
                    chkSena.Checked = lstConsultar.SenaContribuyente == 1 ? true : false;
                    chkICBF.Checked = lstConsultar.icbfContribuyente == 1 ? true : false;
                    chkCCF.Checked = lstConsultar.ccfContribuyente == 1 ? true : false;
                }
                rdbAproxSi.Checked = lstConsultar.ManejaAproximacion == 1 ? true : false;
                rdbAproxNo.Checked = lstConsultar.ManejaAproximacion == 0 ? true : false;

                if (rdbAproxSi.Checked)
                {
                    rdbCentesima.Checked = lstConsultar.AproxCentesima == 1 ? true : false;
                    rdbMilesima.Checked = lstConsultar.AproxMilesima == 1 ? true : false;
                    rdb50cercano.Checked = lstConsultar.Aprox50mascercano == 1 ? true : false;


                    PanelAproximacion.Visible = true;

                    if (rdbCentesima.Checked)
                        rdbCentesima.Checked = lstConsultar.AproxCentesima == 1 ? true : false;
                    if (rdbMilesima.Checked)
                        rdbMilesima.Checked = lstConsultar.AproxMilesima == 1 ? true : false;
                    if (rdb50cercano.Checked)
                        rdb50cercano.Checked = lstConsultar.Aprox50mascercano == 1 ? true : false;
                }

                if (rdbAproxNo.Checked)
                {
                    PanelAproximacion.Visible = false;
                }

                txtIncapacidadesDias.Text = lstConsultar.diasincapacidades.ToString();

              
                rdbTerceroEmp.Checked = lstConsultar.Tercero == 0 ? true : false;               
                rdbTerceroEntidad.Checked = lstConsultar.Tercero == 1 ? true : false;

                rdbNovVacacionesNo.Checked = lstConsultar.novvacaciones == 0 ? true : false;
                rdbNovVacacionesSi.Checked = lstConsultar.novvacaciones == 1 ? true : false;


                rdbVacacionesAntNo.Checked = lstConsultar.vacacionesanticipadas == 0 ? true : false;
                rdbVacacionesAntSi.Checked = lstConsultar.vacacionesanticipadas == 1 ? true : false;


                rdbRetroactivoNo.Checked = lstConsultar.retroactivo == 0 ? true : false;
                rdbRetroactivoSi.Checked = lstConsultar.retroactivo == 1 ? true : false;

                rdbInaSobreSMLVNO.Checked = lstConsultar.incap_smlv == 0 ? true : false;
                rdbInaSobreSMLVSI.Checked = lstConsultar.incap_smlv == 1 ? true : false;


                rdbDeprendible1.Checked = lstConsultar.formato_desprendible == 1 ? true : false;
                rdbDeprendible2.Checked = lstConsultar.formato_desprendible == 2 ? true : false;


            }
        }
        catch (Exception)
        {

            throw;
        }
       
        
    }

    protected void rdbContribuyenteSi_CheckedChanged(object sender, EventArgs e)
    {
        if (rdbContribuyenteSi.Checked)
        {
            Panel5.Visible = true;

        }
        else
        {
            Panel5.Visible = false;

        }
    }

    protected void rdbContribuyenteNo_CheckedChanged(object sender, EventArgs e)
    {
        if (rdbContribuyenteNo.Checked)
        {
            Panel5.Visible = false;
        }
        else
        {
            Panel5.Visible = true;
        }
    }



    protected void rdbAproxSi_CheckedChanged(object sender, EventArgs e)
    {
        if (rdbAproxSi.Checked)
        {
            PanelAproximacion.Visible = true;

        }
        else
        {
            PanelAproximacion.Visible = false;

        }

    }

    protected void rdbAproxNo_CheckedChanged(object sender, EventArgs e)
    {
        if (rdbAproxNo.Checked)
        {
            PanelAproximacion.Visible = false;

        }
        else
        {
            PanelAproximacion.Visible = true;

        }
    }


    protected void CargarCuentas()
    {
        Int64 codbanco = 0;
        try
        {
            codbanco = Convert.ToInt64(ddlEntidadOrigen.SelectedValue);
        }
        catch
        {
        }
        if (codbanco != 0)
        {
            Xpinn.Caja.Services.BancosService bancoService = new Xpinn.Caja.Services.BancosService();
            Usuario usuario = (Usuario)Session["usuario"];
            ddlCuentaOrigen.DataSource = bancoService.ListarCuentaBancos(codbanco, usuario);
            ddlCuentaOrigen.DataTextField = "num_cuenta";
            ddlCuentaOrigen.DataValueField = "idctabancaria";
            ddlCuentaOrigen.DataBind();
        }
    }

    protected void ddlEntidadOrigen_SelectedIndexChanged(object sender, EventArgs e)
    {

        CargarCuentas();

    }
}