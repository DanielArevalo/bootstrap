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
using Xpinn.Ahorros.Entities;
using Xpinn.Ahorros.Services;
using System.Text;
using System.IO;
using System.Web.UI.HtmlControls;
using System.Globalization;
using Xpinn.Programado.Services;


partial class Nuevo : GlobalWeb
{
    private Xpinn.Ahorros.Services.AhorroVistaServices ahorrosServicio = new Xpinn.Ahorros.Services.AhorroVistaServices();
    Xpinn.FabricaCreditos.Services.Persona1Service DatosClienteServicio = new Xpinn.FabricaCreditos.Services.Persona1Service();
    CuentasProgramadoServices cuentasProgramado = new CuentasProgramadoServices();
    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[cuentasProgramado.CodigoProgramaTraslado + ".id"] != null)
                VisualizarOpciones(cuentasProgramado.CodigoProgramaTraslado, "E");
            else
                VisualizarOpciones(cuentasProgramado.CodigoProgramaTraslado, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
            ctlMensaje.eventoClick += btnContinuarMen_Click;
            toolBar.MostrarConsultar(false);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(cuentasProgramado.CodigoProgramaTraslado, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            if (!IsPostBack)
            {
                mvAhorroVista.ActiveViewIndex = 0;
                txtFechaCambio.ToDateTime = DateTime.Now;
                TxtNumeroCuenta.Enabled = false;
                //InicializargvBeneficiario();
                InicializargvBeneficiarioRecursivo(0);
                calcular();

                if (Session[cuentasProgramado.CodigoProgramaTraslado + ".id"] != null)
                {
                    idObjeto = Session[cuentasProgramado.CodigoProgramaTraslado + ".id"].ToString();
                    Session.Remove(cuentasProgramado.CodigoProgramaTraslado + ".id");
                    ObtenerDatos(idObjeto);
                }
                else
                {
                    Navegar(Pagina.Lista);
                }
            }
            else
            {
                calcular();
            }

        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(cuentasProgramado.CodigoProgramaTraslado, "Page_Load", ex);
        }
    }

    protected void InicializargvBeneficiario()
    {
        List<Xpinn.Programado.Entities.CuentasProgramado> lstDeta = new List<Xpinn.Programado.Entities.CuentasProgramado>();
        for (int i = 0; i < 5; i++)
        {
            Xpinn.Programado.Entities.CuentasProgramado eCuenta = new Xpinn.Programado.Entities.CuentasProgramado();
            eCuenta.numero_cuenta = "";
            eCuenta.saldo_total = null;
            //eCuenta.cod_empresa = -1;
            eCuenta.identificacion = "";
            eCuenta.nombres = "";
            eCuenta.nom_oficina = "";
            eCuenta.nom_linea = "";
            eCuenta.V_Traslado = null;
            eCuenta.valor_gmf = null;
            lstDeta.Add(eCuenta);
        }
        gvDetMovs.DataSource = lstDeta;
        gvDetMovs.DataBind();

        Session["DatosDetalle"] = lstDeta;
    }


    List<Xpinn.Programado.Entities.CuentasProgramado> listacuen = new List<Xpinn.Programado.Entities.CuentasProgramado>();
    protected void InicializargvBeneficiarioRecursivo(int i)
    {
        if (i < 5)
        {
            Xpinn.Programado.Entities.CuentasProgramado entidad = new Xpinn.Programado.Entities.CuentasProgramado
            {
                numero_cuenta = "",
                saldo_total = null,
                identificacion = "",
                nombres = "",
                nom_oficina = "",
                nom_linea = "",
                V_Traslado = null,
                valor_gmf = null
            };
            listacuen.Add(entidad);
            InicializargvBeneficiarioRecursivo(i + 1);
        }
        /// recursivo llenar lista beneficiario
        gvDetMovs.DataSource = listacuen;
        gvDetMovs.DataBind();
        Session["DatosDetalle"] = listacuen;

    }

    bool isValido()
    {
        var FechCieCuen = cuentasProgramado.getFechaPosCierreConServices((Usuario)Session["usuario"]);
        var fehcCierProgr = cuentasProgramado.getFechaposProgra((Usuario)Session["usuario"]);
        if (Convert.ToDateTime(txtFechaCambio.Texto) <= FechCieCuen)
        {
            VerError("La fecha de retiro debe ser  posterior a la fecha del último cierre contable definitivo  ");
            return false;
        }
        if (Convert.ToDateTime(txtFechaCambio.Texto) <= fehcCierProgr)
        {
            VerError(" La fecha de retiro debe ser posterior a la fecha del último cierre de ahorro programado definitivo");
            return false;
        }
      

        return true;
    }
    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        if (!validaCuenta())
        {
            VerError("hay un numero de cuenta repetido verifique por favor");
            return;
        }

        //if (TxtNumeroCuenta.Text == txtNumeroCuenta.Text)
        //{
        //    VerError("Coloque un numero de cuenta valido");
        //    return;
        //}

        TxtSaldo_Total.Text = TxtSaldo_Total.Text != "" ? TxtSaldo_Total.Text : "0";
        lblvalor_total.Text = lblvalor_total.Text != "" ? lblvalor_total.Text : "0";

        if (Convert.ToDecimal(TxtSaldo_Total.Text) < Convert.ToDecimal(lblvalor_total.Text))
        {
            VerError("El valor total excede el saldo total de la persona");
            return;
        }
        if (isValido())
        {
            ctlMensaje.MostrarMensaje("Desea guardar los datos de el traslado de la cuenta?");
        }
    }

    protected List<Xpinn.Programado.Entities.CuentasProgramado> ObtenerListaDetalle()
    {
        List<Xpinn.Programado.Entities.CuentasProgramado> lstBeneficiarios = new List<Xpinn.Programado.Entities.CuentasProgramado>();
        List<Xpinn.Programado.Entities.CuentasProgramado> lista = new List<Xpinn.Programado.Entities.CuentasProgramado>();

        foreach (GridViewRow rfila in gvDetMovs.Rows)
        {
            Xpinn.Programado.Entities.CuentasProgramado eBenef = new Xpinn.Programado.Entities.CuentasProgramado();

            TextBoxGrid txtNumeroCuenta = (TextBoxGrid)rfila.FindControl("txtNumeroCuenta");
            if (txtNumeroCuenta.Text != null)
                eBenef.numero_cuenta = Convert.ToString(txtNumeroCuenta.Text);

            Label lbloficina = (Label)rfila.FindControl("lbloficina");
            if (lbloficina.Text != null)
                eBenef.nom_oficina = Convert.ToString(lbloficina.Text);

            Label lbllinea = (Label)rfila.FindControl("lbllinea");
            if (lbllinea.Text != null)
                eBenef.nom_linea = Convert.ToString(lbllinea.Text);


            Label lblidentificacion = (Label)rfila.FindControl("lblidentificacion");
            if (lblidentificacion.Text != null)
                eBenef.identificacion = Convert.ToString(lblidentificacion.Text);

            Label lblnombre = (Label)rfila.FindControl("lblnombre");
            if (lblnombre.Text != null)
                eBenef.nombres = Convert.ToString(lblnombre.Text);

            Label lblsaldo_total = (Label)rfila.FindControl("lblsaldo_total");
            if (lblsaldo_total.Text != "")
                eBenef.saldo_total = Convert.ToDecimal(lblsaldo_total.Text);

            decimalesGridRow txtTraslado = (decimalesGridRow)rfila.FindControl("txtTraslado");
            if (txtTraslado.Text != "")
                eBenef.V_Traslado = Convert.ToDecimal(txtTraslado.Text);

            decimalesGridRow txtGMF = (decimalesGridRow)rfila.FindControl("txtGMF");
            if (txtGMF.Text != "")
                eBenef.valor_gmf = Convert.ToDecimal(txtGMF.Text);

            lista.Add(eBenef);
            Session["DatosDetalle"] = lista;

            if (eBenef.V_Traslado.Value != 0 && eBenef.numero_cuenta.Trim() != null && eBenef.nom_linea != null)
            {
                lstBeneficiarios.Add(eBenef);
            }
        }
        return lstBeneficiarios;
    }

    protected bool validaCuenta()
    {
        List<string> listacuenta = new List<string>();
        foreach (GridViewRow rfila in gvDetMovs.Rows)
        {
            TextBoxGrid txtNumeroCuenta = (TextBoxGrid)rfila.FindControl("txtNumeroCuenta");
            if (txtNumeroCuenta.Text != null)
                if (txtNumeroCuenta.Text.Trim() != "")
                    listacuenta.Add(txtNumeroCuenta.Text);
        }

        List<string> listt = (from e in listacuenta select e).Distinct().ToList();
        if(listacuenta.Count>1)
        if (listacuenta.Count != listt.Count)
        {
            return false;
        }
        string texto = "";
        foreach (var item in listacuenta)
            if (item.Equals(idObjeto))
                texto = item;

        if (!string.IsNullOrEmpty(texto))
        {
            VerError(string.Format("este numero{0} de cuenta no es valido ",texto));
            return false;
        }

        return true;
    }

    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        try
        {
            if (TxtNumeroCuenta.Text != null)
            {
                if (TxtNumeroCuenta.Text == "")
                {
                    VerError("coloque un numero de cuenta valido");
                    return;
                }
            }

            Usuario vUsuario = (Usuario)Session["usuario"];
            Xpinn.Tesoreria.Entities.Operacion poperacion = new Xpinn.Tesoreria.Entities.Operacion();
            poperacion.cod_ope = 0;
            poperacion.tipo_ope = 79;
            poperacion.cod_caja = 0;
            poperacion.cod_cajero = 0;
            poperacion.observacion = "Traslado realizado";
            poperacion.cod_proceso = null;
            poperacion.fecha_oper = Convert.ToDateTime(txtFechaCambio.Text);
            poperacion.fecha_calc = DateTime.Now;
            poperacion.cod_ofi = vUsuario.cod_oficina;
            Int64 codigo = 0;
            List<Xpinn.Programado.Entities.CuentasProgramado> lstIngreso = new List<Xpinn.Programado.Entities.CuentasProgramado>();

            lstIngreso = ObtenerListaDetalle();

            ///Inicializo todo en una entidad AHORRO PROGARMADO

            Xpinn.Programado.Entities.CuentasProgramado ahorro = new Xpinn.Programado.Entities.CuentasProgramado();
            ahorro.numero_cuenta = Convert.ToString(TxtNumeroCuenta.Text);
            ahorro.cod_persona = Convert.ToInt64(Session["COD_PERSONA"].ToString());
            ahorro.V_Traslado = Convert.ToDecimal(lblvalor_total.Text.Replace(".", ""));
            DateTime fechaoperacion = Convert.ToDateTime(txtFechaCambio.Text);

            if (txtFechaCambio.Text == "")
            {
                VerError("Ingrese la fecha de apertura");
                return;
            }

            String estado = "";
            DateTime fechacierrehistorico;
            DateTime fechaliquidacion = Convert.ToDateTime(txtFechaCambio.Text);
            Xpinn.Programado.Entities.CuentasProgramado vAhorroProgramado= new Xpinn.Programado.Entities.CuentasProgramado();
            vAhorroProgramado = cuentasProgramado.ConsultarCierreAhorroProgramado((Usuario)Session["usuario"]);
            estado = vAhorroProgramado.estadocierre;
            fechacierrehistorico = Convert.ToDateTime(vAhorroProgramado.fecha_cierre.ToString());

            if (estado == "D" && fechaliquidacion <= fechacierrehistorico)
            {
                VerError("NO PUEDE INGRESAR TRANSACCIONES EN PERIODOS YA CERRADOS, TIPO L,'AH. PROGRAMADO'");
            }

            else
            {
                string pError = "";
                if (idObjeto != "")
                    cuentasProgramado.deposiTrasaccionServices(ref pError, (Usuario)Session["usuario"], lstIngreso, ref codigo, poperacion, ahorro.numero_cuenta, ahorro.cod_persona, DateTime.Now, Convert.ToDecimal(ahorro.V_Traslado), fechaoperacion);

                if (pError != "")
                {
                    VerError(pError.Substring(0, 90));
                    return;
                }
                
                    if (codigo != 0)
                    {
                        Xpinn.Contabilidad.Services.ComprobanteService ComprobanteServicio = new Xpinn.Contabilidad.Services.ComprobanteService();
                        Session[ComprobanteServicio.CodigoPrograma + ".cod_ope"] = codigo;
                        Session[ComprobanteServicio.CodigoPrograma + ".tipo_ope"] = 79;
                        Session[ComprobanteServicio.CodigoPrograma + ".cod_persona"] = ahorro.cod_persona; //"<Colocar Aquí el código de la persona del servicio>"
                        Navegar("../../Contabilidad/Comprobante/Nuevo.aspx");
                        Session[cuentasProgramado.CodigoProgramaCierreCuenta + ".id"] = idObjeto;
                    }


                    mvAhorroVista.ActiveViewIndex = 1;
                    Site toolBar = (Site)this.Master;
                    toolBar.MostrarGuardar(false);
                    toolBar.MostrarCancelar(false);
                    toolBar.MostrarConsultar(true);

                }
            
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(cuentasProgramado.CodigoProgramaTraslado, "btnGuardar_Click", ex);
        }
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        VerError("");
        Navegar(Pagina.Lista);
    }

    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {

            Xpinn.Programado.Entities.cierreCuentaDetalle objetoEntida = new CuentasProgramadoServices().cierreCuentaDService(pIdObjeto, (Usuario)Session["usuario"]);
            if (objetoEntida == null)
            {
                VerError("verifique Por favor que la cuenta este activa ");
                return;
            }
            if (!string.IsNullOrEmpty(objetoEntida.Cuenta.ToString()))
                TxtNumeroCuenta.Text = HttpUtility.HtmlDecode(objetoEntida.Cuenta.ToString());

            if (!string.IsNullOrEmpty(objetoEntida.Cod_persona.ToString()))
                Session["COD_PERSONA"] = HttpUtility.HtmlDecode(objetoEntida.Cod_persona.ToString());

            if (!string.IsNullOrEmpty(objetoEntida.Linea.ToString()))
                txtLinea.Text = HttpUtility.HtmlDecode(objetoEntida.Linea.ToString().Trim());

            if (!string.IsNullOrEmpty(objetoEntida.Nombre))
                txtNombre.Text = HttpUtility.HtmlDecode(objetoEntida.Nombre.ToString());

            if (!string.IsNullOrEmpty(objetoEntida.nombre_linea))
                txtNombreLinea.Text = HttpUtility.HtmlDecode(objetoEntida.nombre_linea.ToString());

            if (!string.IsNullOrEmpty(objetoEntida.Fecha_Apertura.ToString()))
                Fecha1.ToDateTime = Convert.ToDateTime(HttpUtility.HtmlDecode(objetoEntida.Fecha_Apertura.ToString()));

            if (!string.IsNullOrEmpty(objetoEntida.saldo.ToString()))
                TxtSaldo_Total.Text = objetoEntida.saldo.ToString("n0");

            if (!string.IsNullOrEmpty(objetoEntida.Descripcion_Id))
                TxtIdentif.Text = HttpUtility.HtmlDecode(objetoEntida.Descripcion_Id.ToString().Trim());

            if (!string.IsNullOrEmpty(objetoEntida.Identificacion.ToString()))
                Txtiden.Text = HttpUtility.HtmlDecode(objetoEntida.Identificacion.ToString().Trim());
            
            if (!string.IsNullOrEmpty(objetoEntida.Plazo.ToString()))
                txtPlazo.Text= HttpUtility.HtmlDecode(objetoEntida.Plazo.ToString());

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ahorrosServicio.CodigoProgramaTrasladocuenta, "ObtenerDatos", ex);
        }
    }

    protected void calcular()
    {

        decimal valor = 0;
        decimal valo2 = 0;

        foreach (GridViewRow rfila in gvDetMovs.Rows)
        {

            decimalesGridRow txtTraslado = (decimalesGridRow)rfila.FindControl("txtTraslado");
            decimalesGridRow txtGMF = (decimalesGridRow)rfila.FindControl("txtGMF");

            if (txtGMF.Text == "")
            {
                txtGMF.Text = "0";

            }
            if (txtTraslado.Text == "")
            {
                txtTraslado.Text = "0";

            }

            valor += Convert.ToDecimal(txtTraslado.Text);
            valo2 += Convert.ToDecimal(txtGMF.Text);

        }
        lblvalor_total.Text = valor.ToString("n0");
        txtsaldo_igual.Text = valo2.ToString("n0");

    }

    protected void txtNumCuenta_TextChanged(object sender, EventArgs e)
    {
        VerError("");

        TextBoxGrid txtNumCuenta = (TextBoxGrid)sender;
        if (txtNumCuenta.Text != null )
        {
            if (txtNumCuenta.Text == TxtNumeroCuenta.Text)
            {
                VerError("Digite un numero de cuenta valido");
                return;
            }
            if (txtNumCuenta.Text.Trim() == "")
                txtNumCuenta.Text = "''";
            Xpinn.Programado.Entities.cierreCuentaDetalle objetoEntida = new CuentasProgramadoServices().cierreCuentaDService(txtNumCuenta.Text, (Usuario)Session["usuario"]);
            int rowIndex = Convert.ToInt32(txtNumCuenta.CommandArgument);

            Label lblLinea = (Label)gvDetMovs.Rows[rowIndex].FindControl("lblLinea");

            if (lblLinea != null)
                lblLinea.Text = Convert.ToString(objetoEntida.nombre_linea);

            Label lbloficina = (Label)gvDetMovs.Rows[rowIndex].FindControl("lbloficina");

            if (lbloficina != null)
                lbloficina.Text = Convert.ToString(objetoEntida.Oficina);

            Label lblidentificacion = (Label)gvDetMovs.Rows[rowIndex].FindControl("lblidentificacion");

            if (lblidentificacion != null)
                lblidentificacion.Text = Convert.ToString(objetoEntida.Identificacion);

            Label lblnombre = (Label)gvDetMovs.Rows[rowIndex].FindControl("lblnombre");

            if (lblnombre != null)
                lblnombre.Text = Convert.ToString(objetoEntida.Nombre);

            Label lblsaldo_total = (Label)gvDetMovs.Rows[rowIndex].FindControl("lblsaldo_total");

            if (lblsaldo_total != null)
                lblsaldo_total.Text = objetoEntida.saldo.ToString("n0");
            ///los campos valor traslado gmf no se actualizan hasta ahora se me pidio que lo dejara en espera 
            ///<sumary>//
            ///

        }
    }

    protected void gvDetMovs_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                decimal total = 0;
                decimalesGridRow txtTraslado = (decimalesGridRow)e.Row.FindControl("txtTraslado");
                if (txtTraslado.Text != "")
                    total = Convert.ToDecimal(txtTraslado.Text);


                decimalesGridRow txtGMF = (decimalesGridRow)e.Row.FindControl("txtGMF");
                if (txtGMF.Text != "")
                    total = Convert.ToDecimal(txtGMF.Text);


                TextBoxGrid txtNumeroCuenta = (TextBoxGrid)e.Row.FindControl("txtNumeroCuenta");
                if (txtNumeroCuenta.Text != "")
                    total = Convert.ToDecimal(txtNumeroCuenta.Text);

            }

            try
            {
                ConfirmarEliminarFila(e, "btnEliminar");
            }
            catch (Exception ex)
            {
                BOexcepcion.Throw(cuentasProgramado.CodigoProgramaTraslado + "L", "gvLista_RowDataBound", ex);
            }
        }
    }

    protected void gvDetMovs_SelectedIndexChanged(object sender, EventArgs e)
    {
        String id = gvDetMovs.DataKeys[gvDetMovs.SelectedRow.RowIndex].Value.ToString();
        Session[cuentasProgramado.CodigoProgramaTraslado + ".id"] = id;
        Navegar(Pagina.Nuevo);
    }

    protected void gvDetMovs_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string conseID = Convert.ToString(gvDetMovs.DataKeys[e.RowIndex].Values[0].ToString());
        ObtenerListaDetalle();

        List<Xpinn.Programado.Entities.CuentasProgramado> LstDeta;
        LstDeta = (List<Xpinn.Programado.Entities.CuentasProgramado>)Session["DatosDetalle"];
        if (conseID != null)
        {
            try
            {
                foreach (Xpinn.Programado.Entities.CuentasProgramado Deta in LstDeta)
                {
                    if (Deta.numero_cuenta == conseID)
                    {
                        string id = Convert.ToString(e.Keys[0]);
                        if (id.Trim() != "")
                            ahorrosServicio.EliminarAhorroVista(id, (Usuario)Session["usuario"]); //OPCION 1 Eliminar detalle
                        LstDeta.Remove(Deta);
                        break;
                    }
                }
            }
            catch (Xpinn.Util.ExceptionBusiness ex)
            {
                VerError(ex.Message);
            }
            catch (Exception ex)
            {
                VerError(ex.Message);
            }
            finally 
            {
            }
        }
        else
        {
            foreach (Xpinn.Programado.Entities.CuentasProgramado Deta in LstDeta)
            {
                if (Deta.numero_cuenta == conseID)
                {
                    LstDeta.Remove(Deta);
                    break;
                }
            }
        }

        gvDetMovs.DataSourceID = null;
        gvDetMovs.DataBind();

        gvDetMovs.DataSource = LstDeta;
        gvDetMovs.DataBind();

        Session["DatosDetalle"] = LstDeta;
    }

    protected void gvDetMovs_SelectedIndexChanged1(object sender, System.EventArgs e)
    {

    }

    protected void btnAgregar_Click(object sender, System.EventArgs e)
    {
        ObtenerListaDetalle();

        List<Xpinn.Programado.Entities.CuentasProgramado> lstDetalle = new List<Xpinn.Programado.Entities.CuentasProgramado>();

        if (Session["DatosDetalle"] != null)
        {
            lstDetalle = (List<Xpinn.Programado.Entities.CuentasProgramado>)Session["DatosDetalle"];

            Xpinn.Programado.Entities.CuentasProgramado eActi = new Xpinn.Programado.Entities.CuentasProgramado();
            eActi.numero_cuenta = "";
            eActi.saldo_total = null;
            //eCuenta.cod_empresa = -1;
            eActi.identificacion = "";
            eActi.nombres = "";
            eActi.nom_oficina = "";
            eActi.nom_linea = "";
            eActi.V_Traslado = null;
            eActi.valor_gmf = null;
            lstDetalle.Add(eActi);
            gvDetMovs.PageIndex = gvDetMovs.PageCount;
            gvDetMovs.DataSource = lstDetalle;
            gvDetMovs.DataBind();

            Session["DatosDetalle"] = lstDetalle;
        }
    }
}
