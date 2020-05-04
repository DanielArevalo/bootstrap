using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
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


partial class Nuevo : GlobalWeb
{
    private Xpinn.Ahorros.Services.AhorroVistaServices ahorrosServicio = new Xpinn.Ahorros.Services.AhorroVistaServices();
    Xpinn.FabricaCreditos.Services.Persona1Service DatosClienteServicio = new Xpinn.FabricaCreditos.Services.Persona1Service();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[ahorrosServicio.CodigoProgramaTrasladocuenta + ".id"] != null)
                VisualizarOpciones(ahorrosServicio.CodigoProgramaTrasladocuenta, "E");
            else
                VisualizarOpciones(ahorrosServicio.CodigoProgramaTrasladocuenta, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
            ctlMensaje.eventoClick += btnContinuarMen_Click;
            toolBar.MostrarConsultar(false);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ahorrosServicio.CodigoProgramaTrasladocuenta, "Page_PreInit", ex);
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
                InicializargvBeneficiario();
                calcular();

                if (Session[ahorrosServicio.CodigoProgramaTrasladocuenta + ".id"] != null)
                {
                    idObjeto = Session[ahorrosServicio.CodigoProgramaTrasladocuenta + ".id"].ToString();
                    Session.Remove(ahorrosServicio.CodigoProgramaTrasladocuenta + ".id");
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
            BOexcepcion.Throw(ahorrosServicio.CodigoProgramaTrasladocuenta, "Page_Load", ex);
        }
    }

    protected void InicializargvBeneficiario()
    {
        List<AhorroVista> lstDeta = new List<AhorroVista>();
        for (int i = gvDetMovs.Rows.Count; i < 5; i++)
        {
            AhorroVista eCuenta = new AhorroVista();
            eCuenta.numero_cuenta = "";
            eCuenta.saldo_total = 0;
            //eCuenta.cod_empresa = -1;
            eCuenta.identificacion = "";
            eCuenta.nombres = "";
            eCuenta.nom_oficina = "";
            eCuenta.nom_linea = "";
            eCuenta.V_Traslado = null;
            eCuenta.valor_gmf = null;
            eCuenta.cod_persona = null;
            lstDeta.Add(eCuenta);
        }
        gvDetMovs.DataSource = lstDeta;
        gvDetMovs.DataBind();

        Session["DatosDetalle"] = lstDeta;
    }

    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        VerError("");
        Navegar(Pagina.Lista);
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {

        VerError("");

        if (!validaCuenta())
        {
            VerError("hay un numero de cuenta repetido verifique por favor");
            return;
        }

        TxtSaldo_Total.Text = TxtSaldo_Total.Text != "" ? TxtSaldo_Total.Text : "0";
        lblvalor_total.Text = lblvalor_total.Text != "" ? lblvalor_total.Text : "0";

        if (Convert.ToDecimal(TxtSaldo_Total.Text) < Convert.ToDecimal(lblvalor_total.Text))
        {
            VerError("El valor total excede el saldo total de la persona");
            return;
        }
        ctlMensaje.MostrarMensaje("Desea guardar los datos de el traslado de la cuenta?");

    }

    bool isValido()
    {
        var FechCieCuen = ahorrosServicio.getFechaPosCierreConServices((Usuario)Session["usuario"]);
        var fehcCierProgr = ahorrosServicio.getfechaUltimaCierreAhorros((Usuario)Session["usuario"]);
        if (Convert.ToDateTime(txtFechaCambio.Texto.Trim()) <= fehcCierProgr)
        {
            VerError("La fecha de retiro debe ser  posterior a la fecha del último cierre contable definitivo  ");
            return false;
        }
        if (Convert.ToDateTime(txtFechaCambio.Texto.Trim()) <= FechCieCuen)
        {
            VerError(" La fecha de retiro debe ser posterior a la fecha del último cierre de ahorro programado definitivo");
            return false;
        }


        return true;
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
        if (listacuenta.Count > 1)
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
            VerError(string.Format("este numero{0} de cuenta no es valido ", texto));
            return false;
        }

        return true;
    }


    protected List<AhorroVista> ObtenerListaDetalle()
    {
        List<AhorroVista> lstBeneficiarios = new List<AhorroVista>();
        List<AhorroVista> lista = new List<AhorroVista>();

        foreach (GridViewRow rfila in gvDetMovs.Rows)
        {
            AhorroVista eBenef = new AhorroVista();

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

            Label lblcodpersona = (Label)rfila.FindControl("lblcodpersona");
            if (lblcodpersona.Text != null)
                if (lblcodpersona.Text != "")
                    eBenef.cod_persona = Convert.ToInt64(lblcodpersona.Text);

            lista.Add(eBenef);
            Session["DatosDetalle"] = lista;

            if (eBenef.V_Traslado.Value != 0 && eBenef.numero_cuenta.Trim() != null && eBenef.nom_linea != null)
            {
                lstBeneficiarios.Add(eBenef);
            }
        }
        return lstBeneficiarios;
    }

    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        try
        {
            ///Hago la operacion
            VerError("");

            if (TxtNumeroCuenta.Text != null)
            {
                if (TxtNumeroCuenta.Text == "")
                {
                    VerError("coloque un numero de cuenta valido");
                    return;
                }
            }

            Usuario vUsuario = new Usuario();
            vUsuario = (Usuario)Session["Usuario"];
            Xpinn.Tesoreria.Entities.Operacion poperacion = new Xpinn.Tesoreria.Entities.Operacion();
            poperacion.cod_ope = 0;
            poperacion.tipo_ope = 76;
            poperacion.cod_caja = 0;
            poperacion.cod_cajero = 0;
            poperacion.observacion = "Traslado realizado";
            poperacion.cod_proceso = null;
            poperacion.fecha_oper = Convert.ToDateTime(txtFechaCambio.Text);
            poperacion.fecha_calc = DateTime.Now;
            poperacion.cod_ofi = vUsuario.cod_oficina;

            List<AhorroVista> lstIngreso = new List<AhorroVista>();

            lstIngreso = ObtenerListaDetalle();

            if(string.IsNullOrWhiteSpace(lblCodPersona.Text))
            {
                VerError("No se obtuvo el código de la persona, por favor vuelva a intentar el proceso.");
                return;
            }
            ///Inicializo todo en una entidad ahorrovista
            AhorroVista ahorro = new AhorroVista();
            ahorro.numero_cuenta = Convert.ToString(TxtNumeroCuenta.Text);
            ahorro.cod_persona = Convert.ToInt64(lblCodPersona.Text);
            ahorro.V_Traslado = Convert.ToDecimal(lblvalor_total.Text.Replace(".", ""));

            if (txtFechaCambio.Text == "")
            {
                VerError("Ingrese la fecha de apertura");
                return;
            }
            String estado = "";
            DateTime fechacierrehistorico;
            DateTime fecha_traslado = Convert.ToDateTime(txtFechaCambio.Text);
            Xpinn.Ahorros.Entities.AhorroVista vAhorroVista = new Xpinn.Ahorros.Entities.AhorroVista();
            vAhorroVista = ahorrosServicio.ConsultarCierreAhorroVista((Usuario)Session["usuario"]);
            estado = vAhorroVista.estadocierre;
            fechacierrehistorico = Convert.ToDateTime(vAhorroVista.fecha_cierre.ToString());

            if (estado == "D" && fecha_traslado <= fechacierrehistorico)
            {
                VerError("NO PUEDE INGRESAR TRANSACCIONES EN PERIODOS YA CERRADOS, TIPO H,'AHORROS'");
            }
            else
            {
                string pError = "";
                if (idObjeto != "")
                {
                    ahorro = ahorrosServicio.AplicarTraslado(ref pError, ahorro, lstIngreso, poperacion, (Usuario)Session["usuario"]);
                }

                if (pError != "")
                {
                    VerError(pError.Substring(0, 90));
                    return;
                }
                if (pError == "")
                {
                    //Grabar comprobante contable 
                    if (poperacion.cod_ope != 0)
                    {
                        Xpinn.Contabilidad.Services.ComprobanteService ComprobanteServicio = new Xpinn.Contabilidad.Services.ComprobanteService();
                        Session[ComprobanteServicio.CodigoPrograma + ".cod_ope"] = poperacion.cod_ope;
                        Session[ComprobanteServicio.CodigoPrograma + ".tipo_ope"] = 76;
                        Session[ComprobanteServicio.CodigoPrograma + ".cod_persona"] = ahorro.cod_persona; //"<Colocar Aquí el código de la persona del servicio>"
                        Navegar("../../Contabilidad/Comprobante/Nuevo.aspx");
                        Session[ahorrosServicio.CodigoProgramaTrasladocuenta + ".id"] = idObjeto;
                    }

                    mvAhorroVista.ActiveViewIndex = 1;
                    Site toolBar = (Site)this.Master;
                    toolBar.MostrarGuardar(false);
                    toolBar.MostrarCancelar(false);
                    toolBar.MostrarConsultar(true);
                }
            }

        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ahorrosServicio.CodigoProgramaTrasladocuenta, "btnGuardar_Click", ex);
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
            Xpinn.Ahorros.Entities.AhorroVista vAhorroVista = new Xpinn.Ahorros.Entities.AhorroVista();
            vAhorroVista = ahorrosServicio.ConsultarAhorroVista(Convert.ToString(pIdObjeto), (Usuario)Session["usuario"]);


            if (!string.IsNullOrEmpty(vAhorroVista.numero_cuenta.ToString()))
                TxtNumeroCuenta.Text = HttpUtility.HtmlDecode(vAhorroVista.numero_cuenta.ToString());

            //numero de cuenta
            if (!string.IsNullOrEmpty(vAhorroVista.cod_persona.ToString()))
                lblCodPersona.Text = HttpUtility.HtmlDecode(vAhorroVista.cod_persona.ToString());

            //Codigo del cliente
            if (!string.IsNullOrEmpty(vAhorroVista.cod_linea_ahorro.ToString()))
                txtLinea.Text = HttpUtility.HtmlDecode(vAhorroVista.cod_linea_ahorro.ToString().Trim());

            //linea de ahorro
            if (!string.IsNullOrEmpty(vAhorroVista.nombres))
                txtNombre.Text = HttpUtility.HtmlDecode(vAhorroVista.nombres.ToString());

            //nombres
            if (!string.IsNullOrEmpty(vAhorroVista.nom_linea))
                txtNombreLinea.Text = HttpUtility.HtmlDecode(vAhorroVista.nom_linea.ToString());

            //nombre linea
            if (!string.IsNullOrEmpty(vAhorroVista.saldo_canje.ToString()))
                TxtSaldo.Text = HttpUtility.HtmlDecode(vAhorroVista.saldo_canje.ToString().Trim());

            //estado           
            if (!string.IsNullOrEmpty(vAhorroVista.fecha_apertura.ToString()))
                Fecha1.ToDateTime = Convert.ToDateTime(HttpUtility.HtmlDecode(vAhorroVista.fecha_apertura.ToString()));

            //Fecha de apertura
            if (!string.IsNullOrEmpty(vAhorroVista.saldo_total.ToString()))
                TxtSaldo_Total.Text = vAhorroVista.saldo_total.ToString("n0");

            //saldo total
            if (!string.IsNullOrEmpty(vAhorroVista.identificacion.ToString()))
                TxtIdentif.Text = HttpUtility.HtmlDecode(vAhorroVista.tipo_identificacion.ToString().Trim());

            //tipo identificacion
            if (!string.IsNullOrEmpty(vAhorroVista.identificacion.ToString()))
                Txtiden.Text = HttpUtility.HtmlDecode(vAhorroVista.identificacion.ToString().Trim());

            if (vAhorroVista.cod_linea_ahorro != null)
                txtLinea.Text = vAhorroVista.cod_linea_ahorro;

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

           // valo2 += Convert.ToDecimal(txtGMF.Text);

        }
        lblvalor_total.Text = valor.ToString("n0");
        txtsaldo_igual.Text = valo2.ToString("n0");

    }

    protected void txtNumCuenta_TextChanged(object sender, EventArgs e)
    {
        VerError("");
        TextBoxGrid txtNumCuenta = (TextBoxGrid)sender;

        if (txtNumCuenta.Text == TxtNumeroCuenta.Text)
        {
            VerError("Digite un número de cuenta diferente");
            return;
        }
        if (txtNumCuenta != null)
        {
            if (txtNumCuenta.Text == TxtNumeroCuenta.Text)
            {
                VerError("Digite un número de cuenta diferente");
                return;
            }

            Xpinn.Ahorros.Services.AhorroVistaServices CuentasServicio = new Xpinn.Ahorros.Services.AhorroVistaServices();
            Xpinn.Ahorros.Entities.AhorroVista Cuenta = new Xpinn.Ahorros.Entities.AhorroVista();
            Cuenta = CuentasServicio.ConsultarAhorroVistaTraslado(txtNumCuenta.Text, (Usuario)Session["usuario"]);
            Site toolBar = (Site)this.Master;
            if (Cuenta.numero_cuenta == null)
            {
                VerError("Esta cuenta no esta activa no se puede hacer el traslado");
                toolBar.MostrarGuardar(false);
                return;
            }
            else
            {
                toolBar.MostrarGuardar(true);
                int rowIndex = Convert.ToInt32(txtNumCuenta.CommandArgument);

                Label lblLinea = (Label)gvDetMovs.Rows[rowIndex].FindControl("lblLinea");
                if (lblLinea != null)
                    lblLinea.Text = Convert.ToString(Cuenta.nom_linea);

                Label lbloficina = (Label)gvDetMovs.Rows[rowIndex].FindControl("lbloficina");
                if (lbloficina != null)
                    lbloficina.Text = Convert.ToString(Cuenta.nom_oficina);

                Label lblidentificacion = (Label)gvDetMovs.Rows[rowIndex].FindControl("lblidentificacion");
                if (lblidentificacion != null)
                    lblidentificacion.Text = Convert.ToString(Cuenta.identificacion);

                Label lblnombre = (Label)gvDetMovs.Rows[rowIndex].FindControl("lblnombre");
                if (lblnombre != null)
                    lblnombre.Text = Convert.ToString(Cuenta.nombres);

                Label lblsaldo_total = (Label)gvDetMovs.Rows[rowIndex].FindControl("lblsaldo_total");
                if (lblsaldo_total != null)
                    lblsaldo_total.Text = Cuenta.saldo_total.ToString("n0");

                Label lblcodpersona = (Label)gvDetMovs.Rows[rowIndex].FindControl("lblcodpersona");
                if (lblcodpersona != null)
                    lblcodpersona.Text = Convert.ToString(Cuenta.cod_persona);
            }
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
                BOexcepcion.Throw(ahorrosServicio.CodigoProgramaTrasladocuenta + "L", "gvLista_RowDataBound", ex);
            }
        }
    }

    protected void gvDetMovs_SelectedIndexChanged(object sender, EventArgs e)
    {
        String id = gvDetMovs.DataKeys[gvDetMovs.SelectedRow.RowIndex].Value.ToString();
        Session[ahorrosServicio.CodigoProgramaTrasladocuenta + ".id"] = id;
        Navegar(Pagina.Nuevo);
    }

    protected void gvDetMovs_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvDetMovs.DataKeys[e.NewEditIndex].Value.ToString();
        Session[ahorrosServicio.CodigoProgramaTrasladocuenta + ".id"] = id;
        Navegar(Pagina.Nuevo);
    }

    protected void gvDetMovs_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string conseID = Convert.ToString(gvDetMovs.DataKeys[e.RowIndex].Values[0].ToString());
        ObtenerListaDetalle();

        List<AhorroVista> LstDeta;
        LstDeta = (List<AhorroVista>)Session["DatosDetalle"];
         if (conseID != null)
        {
            try
            {
                foreach (AhorroVista Deta in LstDeta)
                {
                    //if (Deta.numero_cuenta == conseID)
                    //{
                    //    string id = Convert.ToString(e.Keys[0]);
                    //    if (id.Trim() != "")
                    //        ahorrosServicio.EliminarAhorroVista(id, (Usuario)Session["usuario"]); //OPCION 1 Eliminar detalle
                    //    LstDeta.Remove(Deta);
                    //    break;
                    //}
                    if (Deta.numero_cuenta == conseID)
                    {
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
        }
        else
        {
            foreach (AhorroVista Deta in LstDeta)
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


    protected void btnExportar_Click(object sender, EventArgs e)
    {
        ExportToExcel(gvDetMovs);
    }

    protected void ExportToExcel(GridView GridView1)
    {
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=AhorroVista.xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";
        Response.ContentEncoding = Encoding.Default;
        StringWriter sw = new StringWriter();
        ExpGrilla expGrilla = new ExpGrilla();

        sw = expGrilla.ObtenerGrilla(GridView1, null);

        Response.Write(expGrilla.style);
        Response.Output.Write(sw.ToString());
        Response.Flush();
        Response.End();
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }

    protected void gvDetMovs_SelectedIndexChanged1(object sender, System.EventArgs e)
    {

    }
    protected void btnAgregar_Click(object sender, System.EventArgs e)
    {

        ObtenerListaDetalle();

        List<AhorroVista> lstDetalle = new List<AhorroVista>();

        if (Session["DatosDetalle"] != null)
        {
            lstDetalle = (List<AhorroVista>)Session["DatosDetalle"];

            for (int i = 1; i <= 1; i++)
            {
                AhorroVista eActi = new AhorroVista();
                eActi.numero_cuenta = "";
                eActi.saldo_total = 0;
                //eCuenta.cod_empresa = -1;
                eActi.identificacion = "";
                eActi.nombres = "";
                eActi.nom_oficina = "";
                eActi.nom_linea = "";
                eActi.V_Traslado = null;
                eActi.valor_gmf = null;
                lstDetalle.Add(eActi);
            }
            gvDetMovs.PageIndex = gvDetMovs.PageCount;
            gvDetMovs.DataSource = lstDetalle;
            gvDetMovs.DataBind();

            Session["DatosDetalle"] = lstDetalle;
        }

    }
}
