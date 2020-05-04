using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using System.IO;
using System.Configuration;
using System.Globalization;
using System.Text;
using System.Web.UI.HtmlControls;
using Xpinn.ConciliacionBancaria.Services;
using Xpinn.ConciliacionBancaria.Entities;
using System.Data;

public partial class Nuevo : GlobalWeb
{

    ExtractoBancarioServices ExtractoServicio = new ExtractoBancarioServices();
    public static string id = "";


    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                if (Session[ExtractoServicio.CodigoPrograma + ".id"] != null)
                    VisualizarOpciones(ExtractoServicio.CodigoPrograma, "E");
                else
                    VisualizarOpciones(ExtractoServicio.CodigoPrograma, "A");
            }
            else
            {
                if (id != "")
                    VisualizarOpciones(ExtractoServicio.CodigoPrograma, "E");
                else
                    VisualizarOpciones(ExtractoServicio.CodigoPrograma, "A");
            }

            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
            toolBar.eventoCargar += btnCargar_Click;
            //decimalesGridRow txtValor = new decimalesGridRow();
            //txtValor.eventoCambiar += txtValor_TextChanged;
            
            ctlMensaje.eventoClick += btnContinuarMen_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ExtractoServicio.GetType().Name + "L", "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                Session["Extracto"] = null;
                CargarDropdown();
                mvAplicar.ActiveViewIndex = 0;
                txtCodigo.Enabled = false;

                if (Session[ExtractoServicio.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[ExtractoServicio.CodigoPrograma + ".id"].ToString();
                    Session.Remove(ExtractoServicio.CodigoPrograma + ".id");
                    ObtenerDatos(idObjeto);
                    id = idObjeto;
                }
                else
                {
                    id = "";
                    txtCodigo.Text = "Autogenerado";
                    InicializargvDetalle();
                }
            }
            else
            {
                ObtenerListaDetalle();
                CalcularTotal();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ExtractoServicio.GetType().Name + "L", "Page_Load", ex);
        }
    }


    void CargarDropdown()
    {
        Xpinn.Caja.Services.BancosService BOBancos = new Xpinn.Caja.Services.BancosService();
        Xpinn.Caja.Entities.Bancos pEntidad = new Xpinn.Caja.Entities.Bancos();
        List<Xpinn.Caja.Entities.Bancos> lstBancos = new List<Xpinn.Caja.Entities.Bancos>();
        lstBancos = BOBancos.ListarBancosegre((Usuario)Session["usuario"]);

        ddlEntidad.Items.Insert(0, new ListItem("Seleccione un item", "0"));
        if (lstBancos.Count > 0)
        {
            ddlEntidad.DataSource = lstBancos;
            ddlEntidad.DataTextField = "nombrebanco";
            ddlEntidad.DataValueField = "cod_banco";
            ddlEntidad.AppendDataBoundItems = true;
            ddlEntidad.DataBind();
            ddlEntidad.SelectedIndex = 0;
        }
        //PoblarLista("Bancos",ddlEntidad);
        for (int i = 1; i <= 12; i++)
        {
            DateTimeFormatInfo formatoFecha = CultureInfo.CurrentCulture.DateTimeFormat;
            string nombreMes = formatoFecha.GetMonthName(i);
            ddlMes.Items.Insert(i - 1, new ListItem(nombreMes,nombreMes));            
        }
        ddlMes.DataBind();

        PoblarLista("estructura_extracto", ddlEstructura);
    }



    protected void InicializargvDetalle()
    {
        List<DetExtractoBancario> lstDeta = new List<DetExtractoBancario>();
        for (int i = gvDetalleEx.Rows.Count; i < 2; i++)
        {
            DetExtractoBancario eDeta = new DetExtractoBancario();
            eDeta.iddetalle = -1;
            //eCuenta.cod_empresa = -1;
            eDeta.fecha = null;
            eDeta.cod_concepto = "";
            eDeta.tipo_movimiento = "";
            eDeta.num_documento = "";
            eDeta.referencia1 = "";
            eDeta.referencia2 = "";
            eDeta.valor = null;
            lstDeta.Add(eDeta);
        }
        gvDetalleEx.DataSource = lstDeta;
        gvDetalleEx.DataBind();
        txtNumReg.Text = gvDetalleEx.Rows.Count.ToString();
        Session["Extracto"] = lstDeta;        
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


    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            ExtractoBancario vDato = new ExtractoBancario();
            
            vDato = ExtractoServicio.ConsultarExtractoBancario(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);

            if (vDato.idextracto != 0)
                txtCodigo.Text = vDato.idextracto.ToString();
            if (vDato.cod_banco != 0)
                ddlEntidad.SelectedValue = vDato.cod_banco.ToString();
            ddlEntidad_SelectedIndexChanged(ddlEntidad, null);

            if (vDato.numero_cuenta != "")
                ddlCuentaBancaria.SelectedValue = vDato.numero_cuenta;

            if (vDato.periodo != "" && vDato.periodo != null)
            {
                string[] vPerio;
                vPerio = vDato.periodo.ToString().Split('-');
                ddlMes.SelectedValue = vPerio[0].Trim();
                txtPeriodo.Text = vPerio[1].Trim();
            } 
            if (vDato.saldo_anterior != 0)
                txtSaldoIni.Text = vDato.saldo_anterior.ToString();
            if (vDato.debitos != null)
                txtTotalDeb.Text = vDato.debitos.ToString();
            if (vDato.creditos != null)
                txtTotalCre.Text = vDato.creditos.ToString();
            
            decimal saldoFin = (Convert.ToDecimal(txtSaldoIni.Text) + Convert.ToDecimal(vDato.debitos)) - Convert.ToDecimal(vDato.creditos);
            txtSaldoFin.Text = saldoFin.ToString("n0");

            if (vDato.fechacreacion != DateTime.MinValue)
                Session["FECHA"] = vDato.fechacreacion;
            if (vDato.estado != null)
                Session["ESTADO"] = vDato.estado;

            //RECUPERAR DATOS - GRILLA DETALLE
            List<DetExtractoBancario> lstDetalle = new List<DetExtractoBancario>();
            lstDetalle = ExtractoServicio.ListarDetExtractoBancario(Convert.ToInt32(pIdObjeto), (Usuario)Session["usuario"]);

            if (lstDetalle.Count > 0)
            {
                if ((lstDetalle != null) || (lstDetalle.Count != 0))
                {
                    gvDetalleEx.DataSource = lstDetalle;
                    gvDetalleEx.DataBind();
                }
                Session["Extracto"] = lstDetalle;
            }
            else
            {
                InicializargvDetalle();
            }
            txtNumReg.Text = gvDetalleEx.Rows.Count.ToString();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ExtractoServicio.CodigoPrograma, "ObtenerDatos", ex);
        }
    }


    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }

    public Boolean ValidarDatos()
    {
        VerError("");
        if (ddlEntidad.SelectedIndex == 0)
        {
            VerError("Seleccione la entidad Por favor");
            return false;
        }
        if (ddlCuentaBancaria.SelectedItem == null)
        {
            VerError("Seleccione una entidad que tenga Cuentas Bancarias");
            return false;
        }
        if (ddlCuentaBancaria.SelectedIndex == 0)
        {
            VerError("Seleccione el numero de cuenta Por favor");
            return false;
        }
        if (txtPeriodo.Text == "")
        {
            VerError("Ingrese los datos del periodo");
            return false;
        }
        if (txtSaldoIni.Text == "0")
        {
            VerError("Ingrese el valor de Saldo Inicial");
            return false;
        }
        if (txtTotalCre.Text == "")
        {
            VerError("Ingrese el valor Total de Créditos");
            return false;
        }
        if (txtTotalDeb.Text == "")
        {
            VerError("Ingrese el valor Total de Débitos");
            return false;
        }
        if (txtSaldoFin.Text == "")
        {
            VerError("Ingrese el valor de Saldo Final");
            return false;
        }
        if (txtNumReg.Text == "")
        {
            VerError("Ingrese el número total de Registros");
            return false;
        }

        List<DetExtractoBancario> lstListado = ObtenerListaDetalle();

        if (lstListado.Count == 0)
        {
            VerError("Debe ingresar como mínimo un detalle");
            return false;            
        }
        else
        {
            int cont = 0;
            foreach (DetExtractoBancario rFila in lstListado)
            {
                cont++;
                if (rFila.cod_concepto == null)
                {
                    VerError("Debe ingresar el Concepto en la fila Nro "+ cont);
                    return false; 
                }                
            }
        }
        return true;
    }

    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        if (ValidarDatos())
        {
            string msj = idObjeto != "" ? "Modificar" : "Grabar";
            ctlMensaje.MostrarMensaje("Desea "+msj+" los Datos Ingresados?");          
        }
    }



    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        try
        {
            ExtractoBancario pVar = new ExtractoBancario();
            if (txtCodigo.Text != "" && idObjeto != "")
                pVar.idextracto = Convert.ToInt32(txtCodigo.Text);
            else
                pVar.idextracto = 0;

            pVar.fecha = DateTime.Now;
            pVar.cod_banco = Convert.ToInt32(ddlEntidad.SelectedValue);
            pVar.numero_cuenta = ddlCuentaBancaria.SelectedValue;
            pVar.saldo_anterior = Convert.ToDecimal(txtSaldoIni.Text);
            pVar.debitos = Convert.ToDecimal(txtTotalDeb.Text);
            pVar.creditos = Convert.ToDecimal(txtTotalCre.Text);
            pVar.mes = 0;
            pVar.dia = 0;
            if (idObjeto != "")
                pVar.estado = Convert.ToInt32(Session["ESTADO"].ToString());
            else
                pVar.estado = 0;
            //pVar.codusuariocreacion = ; ASIGNADO EN CAPA DATOS
            if (idObjeto != "")
                pVar.fechacreacion = Convert.ToDateTime(Session["FECHA"].ToString());
            else
                pVar.fechacreacion = DateTime.Now;
            pVar.periodo = ddlMes.SelectedValue+"-"+txtPeriodo.Text;

            pVar.lstDetalle = new List<DetExtractoBancario>();
            pVar.lstDetalle = ObtenerListaDetalle();
            
            if (idObjeto != "")
            {
                //MODIFICAR
                ExtractoServicio.ModificarExtractoBancario(pVar, (Usuario)Session["usuario"]);
            }
            else
            {
                //CREAR
                ExtractoServicio.CrearExtractoBancario(pVar, (Usuario)Session["usuario"]);
            }

            string msj = idObjeto != "" ? "modifico":"grabo";
            lblmsj.Text = "Se "+msj+" correctamente los Datos";

            Site toolBar = (Site)Master;
            toolBar.MostrarGuardar(false);
            toolBar.MostrarCargar(false);
            mvAplicar.ActiveViewIndex = 2;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ExtractoServicio.CodigoPrograma, "btnContinuar_Click", ex);
        }        
    }

    protected List<DetExtractoBancario> ObtenerListaDetalle()
    {
        try
        {
            List<DetExtractoBancario> lstDetalle = new List<DetExtractoBancario>();
            List<DetExtractoBancario> lista = new List<DetExtractoBancario>();

            foreach (GridViewRow rfila in gvDetalleEx.Rows)
            {
                DetExtractoBancario ePogra = new DetExtractoBancario();

                Label lblCodigo = (Label)rfila.FindControl("lblCodigo");
                if (Convert.ToInt32(lblCodigo.Text) > 0)
                    ePogra.iddetalle = Convert.ToInt32(lblCodigo.Text);
                else
                    ePogra.iddetalle = -1;

                fecha txtfecha = (fecha)rfila.FindControl("txtfecha");
                if (txtfecha.Text != "")
                    ePogra.fecha = Convert.ToDateTime(txtfecha.Text);

                TextBoxGrid txtNroDocumen = (TextBoxGrid)rfila.FindControl("txtNroDocumen");
                if (txtNroDocumen.Text != "")
                    ePogra.num_documento = txtNroDocumen.Text;
                else
                    ePogra.num_documento = null;

                DropDownListGrid ddlConcepto = (DropDownListGrid)rfila.FindControl("ddlConcepto");
                if (ddlConcepto.SelectedIndex != 0)
                    ePogra.cod_concepto = ddlConcepto.SelectedValue;
                else
                    ePogra.cod_concepto = null;

                DropDownListGrid ddlTipoMov = (DropDownListGrid)rfila.FindControl("ddlTipoMov");
                if (ddlTipoMov.SelectedItem != null)
                    ePogra.tipo_movimiento = ddlTipoMov.SelectedValue;

                TextBoxGrid txtIdentificacion = (TextBoxGrid)rfila.FindControl("txtIdentificacion");
                if (txtIdentificacion.Text != "")
                    ePogra.referencia1 = txtIdentificacion.Text;
                else
                    ePogra.referencia1 = null;

                TextBoxGrid txtReferencia = (TextBoxGrid)rfila.FindControl("txtReferencia");
                if (txtReferencia.Text != "")
                    ePogra.referencia2 = txtReferencia.Text;
                else
                    ePogra.referencia2 = null;
                
                decimalesGridRow txtValor = (decimalesGridRow)rfila.FindControl("txtValor");
                if (txtValor.Text != "")
                    ePogra.valor = Convert.ToDecimal(txtValor.Text);

                lista.Add(ePogra);
                Session["Extracto"] = lista;

                if (ePogra.fecha != DateTime.MinValue && ePogra.fecha != null && ePogra.tipo_movimiento != "" && ePogra.tipo_movimiento != null && ePogra.valor != 0 && ePogra.valor != null)
                {
                    lstDetalle.Add(ePogra);
                }
            }
            return lstDetalle;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ExtractoServicio.CodigoPrograma, "ObtenerListaDetalle", ex);          
            return null;
        }
    }



    protected void btnAdicionarFila_Click(object sender, EventArgs e)
    {
        ObtenerListaDetalle();
        List<DetExtractoBancario> LstPrograma = new List<DetExtractoBancario>();
        if (Session["Extracto"] != null)
        {
            LstPrograma = (List<DetExtractoBancario>)Session["Extracto"];

            for (int i = 1; i <= 1; i++)
            {
                DetExtractoBancario eDeta = new DetExtractoBancario();
                eDeta.iddetalle = -1;
                //eCuenta.cod_empresa = -1;
                eDeta.fecha = null;
                eDeta.cod_concepto = "";
                eDeta.tipo_movimiento = "";
                eDeta.num_documento = "";
                eDeta.referencia1 = "";
                eDeta.referencia2 = "";
                eDeta.valor = null;
                LstPrograma.Add(eDeta);
            }
            gvDetalleEx.DataSource = LstPrograma;
            gvDetalleEx.DataBind();
            txtNumReg.Text = gvDetalleEx.Rows.Count.ToString();
            Session["Extracto"] = LstPrograma;
        }
    }


    protected void gvDetalleEx_RowDataBound(object sender, GridViewRowEventArgs e)
    {        
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownListGrid ddlConcepto = (DropDownListGrid)e.Row.FindControl("ddlConcepto");
            if (ddlConcepto != null)
            {
                List<DetExtractoBancario> lstConsulta = new List<DetExtractoBancario>();
                lstConsulta = ExtractoServicio.ListarConceptos_Bancarios((Usuario)Session["usuario"]);
                if (lstConsulta.Count > 0)
                {
                    ddlConcepto.DataSource = lstConsulta;
                    ddlConcepto.DataTextField = "descripcion";
                    ddlConcepto.DataValueField = "cod_concepto";
                }
                ddlConcepto.AppendDataBoundItems = true;
                ddlConcepto.Items.Insert(0, new ListItem("Seleccione un item", "0"));
                ddlConcepto.SelectedIndex = 0;
                ddlConcepto.DataBind();
            }

            Label lblConcepto = (Label)e.Row.FindControl("lblConcepto");
            if (lblConcepto.Text != "")
                ddlConcepto.SelectedValue = lblConcepto.Text;


            DropDownListGrid ddlTipoMov = (DropDownListGrid)e.Row.FindControl("ddlTipoMov");
            if (ddlTipoMov != null)
            {
                ddlTipoMov.Items.Insert(0, new ListItem("Débito", "D"));
                ddlTipoMov.Items.Insert(1, new ListItem("Crédito", "C"));
                ddlTipoMov.SelectedIndex = 0;
                ddlTipoMov.DataBind();
            }

            Label lbltipoMov = (Label)e.Row.FindControl("lbltipoMov");
            if (lbltipoMov.Text != "")
                ddlTipoMov.SelectedValue = lbltipoMov.Text;

            decimalesGridRow txtValor = (decimalesGridRow)e.Row.FindControl("txtValor");
            if (txtValor != null)
                txtValor.eventoCambiar += txtValor_TextChanged;

        }
    }

    protected void gvDetalleEx_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int conseID = Convert.ToInt32(gvDetalleEx.DataKeys[e.RowIndex].Values[0].ToString());

        ObtenerListaDetalle();

        List<DetExtractoBancario> LstDetalle = new List<DetExtractoBancario>();
        LstDetalle = (List<DetExtractoBancario>)Session["Extracto"];
        try
        {
            foreach (DetExtractoBancario Deta in LstDetalle)
            {
                if (Deta.iddetalle == conseID)
                {
                    if (conseID > 0)
                        ExtractoServicio.EliminarDetExtractoBancario(conseID, (Usuario)Session["usuario"]);
                    LstDetalle.RemoveAt((gvDetalleEx.PageIndex * gvDetalleEx.PageSize) + e.RowIndex);
                    //LstDetalle.Remove(Deta);
                    break;
                }
            }
            Session["Extracto"] = LstDetalle;

            gvDetalleEx.DataSource = LstDetalle;
            gvDetalleEx.DataBind();
            txtNumReg.Text = gvDetalleEx.Rows.Count.ToString();
        }
        catch (Xpinn.Util.ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
    }
    
    

    protected void ddlEntidad_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlEntidad.SelectedIndex != 0)
        {
            Xpinn.Caja.Services.BancosService BancosService = new Xpinn.Caja.Services.BancosService();
            Xpinn.Caja.Entities.Bancos Bancos = new Xpinn.Caja.Entities.Bancos();
            List<Xpinn.Caja.Entities.CuentaBancaria> lstConsulta = new List<Xpinn.Caja.Entities.CuentaBancaria>();
            lstConsulta = BancosService.ListarCuentaBancos(Convert.ToInt64(ddlEntidad.SelectedValue), (Usuario)Session["Usuario"]);
            ddlCuentaBancaria.Items.Clear();
            if (lstConsulta.Count > 0)
            {
                ddlCuentaBancaria.DataSource = lstConsulta;
                ddlCuentaBancaria.DataTextField = "num_cuenta";
                ddlCuentaBancaria.DataValueField = "num_cuenta";
                ddlCuentaBancaria.Items.Insert(0, new ListItem("Seleccione un item", "0"));
                ddlCuentaBancaria.SelectedIndex = 0;
                ddlCuentaBancaria.DataBind();
                ddlCuentaBancaria.Enabled = true;
            }
            else
            {
                ddlCuentaBancaria.Enabled = false;
                ddlCuentaBancaria.Items.Clear();
            }
        }
        else
        {
            ddlCuentaBancaria.Items.Clear();
            ddlCuentaBancaria.Enabled = false;
        }
    }

    public void CalcularTotal()
    {
        decimal? totdeb = 0.00m;
        decimal? totcre = 0.00m;
        decimal saldoFinal = 0.00m;
        List<DetExtractoBancario> LstDetalle = new List<DetExtractoBancario>();
        if (Session["Extracto"] != null)
        {
            LstDetalle = (List<DetExtractoBancario>)Session["Extracto"];
            for (int i = 0; i < LstDetalle.Count; i++)
            {
                if (LstDetalle[i].valor != null)
                {
                    if (LstDetalle[i].tipo_movimiento == "D" | LstDetalle[i].tipo_movimiento == "d")
                        totdeb = totdeb + LstDetalle[i].valor;
                    else
                        totcre = totcre + LstDetalle[i].valor;
                }
            }
            if(txtSaldoIni.Text != "")
                saldoFinal =  Convert.ToDecimal((Convert.ToDecimal(txtSaldoIni.Text) + totdeb) - totcre);
            string sDeb = String.Format("{0:N0}", totdeb);
            txtTotalDeb.Text = sDeb;
            string sCre = String.Format("{0:N0}", totcre);
            txtTotalCre.Text = sCre;
            txtSaldoFin.Text = saldoFinal.ToString("n0");
        }
    }


    protected void txtValor_TextChanged(object sender, EventArgs e)
    {
        ObtenerListaDetalle();
        CalcularTotal();
    }
      

    protected void btnCargar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        lblMensajes.Text = "";
        mvAplicar.ActiveViewIndex = 1;
        Site toolBar = (Site)Master;
        toolBar.MostrarGuardar(false);
        toolBar.MostrarCancelar(false);
        toolBar.MostrarCargar(false);
    }

    protected void btnParar_Click(object sender, EventArgs e)
    {
        mvAplicar.ActiveViewIndex = 0;
        Site toolBar = (Site)Master;
        toolBar.MostrarGuardar(true);
        toolBar.MostrarCancelar(true);
        toolBar.MostrarCargar(true);
        VerError("");
    }



    public string AjustarCampos(string pcampo, char pcaracterajuste)
    {
        Boolean bencontro = true;
        string lineaNueva = "";
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


    protected void btnCargarArchivo_Click(object sender, EventArgs e)
    {
        VerError("");   
        lblMensajes.Text = "";        
        try
        {
            List<DetExtractoBancario> lstDetaGeneral = new List<DetExtractoBancario>();
            List<DetExtractoBancario> lstDatos = new List<DetExtractoBancario>();
            lstDatos = ObtenerListaDetalle();
            //CARGANDO LOS DATOS DE LA GRIDVIEW
            if (idObjeto != "") // Si es un editar cargo toda la gridView
            {
                lstDetaGeneral = (List<DetExtractoBancario>)Session["Extracto"];
            }
            else  // Si es nuevo verifico los datos si contienen
            {
                lstDetaGeneral = lstDatos;
            }

            if (fuArchivo.HasFile)
            {
                String fileName = Path.GetFileName(this.fuArchivo.PostedFile.FileName);
                String extension = Path.GetExtension(this.fuArchivo.PostedFile.FileName).ToLower();

                if (extension != ".xlsx" && extension != ".xls" && extension != ".txt" && extension != ".csv")
                {
                    lblMensajes.Text = "Ingrese Archivos Excel o de Texto";
                    return;
                }
                if (ddlEstructura.SelectedValue == "")
                {
                    lblMensajes.Text = "Debe seleccionar la estructura";
                    return;
                }                
                
                
                EstructuraExtracto estructura = new EstructuraExtracto();
                estructura.idestructuraextracto = Convert.ToInt32(ddlEstructura.SelectedValue);
                
                EstructuraExtractoServices EstructuraService = new EstructuraExtractoServices();
                estructura = EstructuraService.ConsultarEstructuraCarga(estructura, (Usuario)Session["usuario"]);
                if (estructura == null)
                {
                    lblMensajes.Text = "La estructura selecciona no existe";
                    return;
                }
                List<DetEstructuraExtracto> lstEstructuraDetalle = new List<DetEstructuraExtracto>();
                if (estructura.idestructuraextracto != null)
                {
                    lstEstructuraDetalle = EstructuraService.ListarEstructuraDetalle(Convert.ToInt32(ddlEstructura.SelectedValue), (Usuario)Session["usuario"]);
                }
                  
                //Lista para cargar los datos
                DataTable dt = new DataTable(); //Se Cargan los Datos si se sube por Excel
                List<DetExtractoBancario> lstDatosPorCargar = new List<DetExtractoBancario>();//Se Cargan los Datos si se sube por Arch de texto
                try
                {
                    if (estructura.tipo_archivo == 0)
                    {
                        //ARCHIVO EXCEL
                        if (extension != ".xlsx" && extension != ".xls")
                        {
                            lblMensajes.Text = "El Archivo seleccionado no es un archivo Excel";
                            return;
                        }

                        /* Se guarda el archivo en el servidor */
                        fuArchivo.PostedFile.SaveAs(Server.MapPath("Archivos\\") + fileName);
                        fuArchivo.Dispose();

                        
                        ExcelToGrid excel = new ExcelToGrid();
                        dt = excel.leerExcel(Server.MapPath("Archivos\\") + fileName, "Datos");
                        
                        //Eliminando el Archivo una vez cargado DataTable
                        File.Delete(Server.MapPath("Archivos\\") + fileName);

                        /* Generar columnas en la grilla */
                        if (dt.Rows.Count <= 0)
                        {
                            lblMensajes.Text = "No se pudo cargar el archivo o el archivo no contiene datos (Hoja: Datos)";
                            return;
                        }
                        else
                        {
                            foreach (DataRow rData in dt.Rows)
                            {
                                DetExtractoBancario vDetalle = new DetExtractoBancario();
                                foreach (DetEstructuraExtracto estDetalle in lstEstructuraDetalle)
                                {                                    
                                    Int64 posicion = Convert.ToInt64(estDetalle.posicion_inicial);
                                    if (posicion >= 0)
                                    {
                                        if (estDetalle.codigo_campo == 1)
                                        {
                                            if (estructura.formato_fecha == null)
                                                estructura.formato_fecha = "dd/MM/yyyy";

                                            if (estDetalle.justificacion == 1 && estDetalle.justificador != null)
                                                vDetalle.fecha = DateTime.ParseExact(AjustarCampos(rData.ItemArray[Convert.ToInt32(estDetalle.numero_columna - 1)].ToString(), Convert.ToChar(estDetalle.justificador)),estructura.formato_fecha,null);
                                            else
                                                vDetalle.fecha = DateTime.ParseExact(rData.ItemArray[Convert.ToInt32(estDetalle.numero_columna - 1)].ToString(), estructura.formato_fecha, null);
                                        }
                                        if (estDetalle.codigo_campo == 2)
                                        {
                                            if (estDetalle.justificacion == 1 && estDetalle.justificador != null)
                                                vDetalle.num_documento = AjustarCampos(rData.ItemArray[Convert.ToInt32(estDetalle.numero_columna - 1)].ToString(), Convert.ToChar(estDetalle.justificador));
                                            else
                                                vDetalle.num_documento = rData.ItemArray[Convert.ToInt32(estDetalle.numero_columna - 1)].ToString();                                            
                                        }
                                        if (estDetalle.codigo_campo == 3)
                                        {
                                            if (estDetalle.justificacion == 1 && estDetalle.justificador != null)
                                                vDetalle.cod_concepto = AjustarCampos(rData.ItemArray[Convert.ToInt32(estDetalle.numero_columna - 1)].ToString(), Convert.ToChar(estDetalle.justificador));
                                            else
                                                vDetalle.cod_concepto = rData.ItemArray[Convert.ToInt32(estDetalle.numero_columna - 1)].ToString();
                                        }
                                        if (estDetalle.codigo_campo == 4)
                                        {
                                            if (estDetalle.justificacion == 1 && estDetalle.justificador != null)
                                                vDetalle.tipo_movimiento = AjustarCampos(rData.ItemArray[Convert.ToInt32(estDetalle.numero_columna - 1)].ToString(), Convert.ToChar(estDetalle.justificador));
                                            else
                                                vDetalle.tipo_movimiento = rData.ItemArray[Convert.ToInt32(estDetalle.numero_columna - 1)].ToString();                                           
                                        }
                                        if (estDetalle.codigo_campo == 5)
                                        {
                                            if (estDetalle.justificacion == 1 && estDetalle.justificador != null)
                                                vDetalle.referencia1 = AjustarCampos(rData.ItemArray[Convert.ToInt32(estDetalle.numero_columna - 1)].ToString(), Convert.ToChar(estDetalle.justificador));
                                            else
                                                vDetalle.referencia1 = rData.ItemArray[Convert.ToInt32(estDetalle.numero_columna - 1)].ToString();                                           
                                        }
                                        if (estDetalle.codigo_campo == 6)
                                        {
                                            if (estDetalle.justificacion == 1 && estDetalle.justificador != null)
                                                vDetalle.referencia2 = AjustarCampos(rData.ItemArray[Convert.ToInt32(estDetalle.numero_columna - 1)].ToString().Replace(",", ""), Convert.ToChar(estDetalle.justificador));
                                            else
                                                vDetalle.referencia2 = rData.ItemArray[Convert.ToInt32(estDetalle.numero_columna - 1)].ToString().Replace(",", "");
                                            //vDetalle.referencia2 = Convert.ToString(rData.ItemArray[Convert.ToInt32(estDetalle.numero_columna - 1)].ToString());
                                        }
                                        if (estDetalle.codigo_campo == 7)
                                        {
                                            if (estDetalle.justificacion == 1 && estDetalle.justificador != null)
                                                vDetalle.valor = Decimal.Parse(AjustarCampos(rData.ItemArray[Convert.ToInt32(estDetalle.numero_columna - 1)].ToString().Replace(estructura.separador_decimal, GlobalWeb.gSeparadorDecimal), Convert.ToChar(estDetalle.justificador)));
                                            else
                                                vDetalle.valor = Decimal.Parse(rData.ItemArray[Convert.ToInt32(estDetalle.numero_columna - 1)].ToString().Replace(estructura.separador_miles, "").Replace(estructura.separador_decimal, GlobalWeb.gSeparadorDecimal));
                                            //vDetalle.valor = Convert.ToDecimal(rData.ItemArray[Convert.ToInt32(estDetalle.numero_columna - 1)].ToString());
                                        }
                                    }                                    
                                }
                                lstDatosPorCargar.Add(vDetalle);
                            }
                        }
                    }
                    else
                    {
                        int totallineas = 4;
                        string readLine;

                        int contador = 0;
                        StreamReader strReader;
                        Stream stream = fuArchivo.FileContent;

                        //ARCHIVO DE TEXTO
                        using (strReader = new StreamReader(stream))
                        {
                            while (strReader.Peek() >= 0)
                            {
                                readLine = strReader.ReadLine();
                                //RecaudosMasivos entidad = new RecaudosMasivos(); no se Usa
                                if (estructura.idestructuraextracto != null)
                                {
                                    Boolean bCargar = true;

                                    if (extension != ".txt" && extension != ".csv")
                                    {
                                        lblMensajes.Text = "El Archivo seleccionado no es un archivo de Texto";
                                        return;
                                    }

                                    // No cargar lineas del encabezado
                                    if (estructura.encabezado > 0 && contador < estructura.encabezado)
                                        bCargar = false;
                                    // No cargar líneas del final
                                    if (estructura.totales > 0 && totallineas - contador + 1 < estructura.totales)
                                        bCargar = false;
                                    // Validar que la línea tenga datos
                                    if (readLine.Trim() == "")
                                        bCargar = false;
                                    // Si es un archivo de TEXTO
                                    if (bCargar == true)
                                    {
                                        if (estructura.calificador == "0") //TIPO DE DATO
                                        {
                                            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                                            // Si el archivo es delimitado
                                            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                                            // Separar los campos del archivo
                                            if (estructura.delimitador == "0")
                                                estructura.delimitador = "  ";
                                            if (estructura.delimitador == "1")
                                                estructura.delimitador = ",";
                                            if (estructura.delimitador == "2")
                                                estructura.delimitador = ";";
                                            if (estructura.delimitador == "3")
                                                estructura.delimitador = " ";

                                            string[] arrayline = readLine.Split(Convert.ToChar(estructura.delimitador));

                                            int contadorreg = 0;

                                            DetExtractoBancario vEntidad = new DetExtractoBancario();
                                            foreach (DetEstructuraExtracto estDetalle in lstEstructuraDetalle)
                                            {
                                                Int64 posicion = Convert.ToInt64(estDetalle.posicion_inicial);
                                                if (posicion >= 0)
                                                {
                                                    //1=Fecha 2=Nro Documento 3=Concepto 4=Tipo Mov 5=Identificacion 6=Referencia 7=Valor


                                                    if (estDetalle.codigo_campo == 1)
                                                    {
                                                        string sformato_fecha = "dd/MM/yyyy";
                                                        if (estructura.formato_fecha != null)
                                                            sformato_fecha = estructura.formato_fecha;

                                                        if (estDetalle.justificacion == 1 && estDetalle.justificador != null)
                                                            vEntidad.fecha = DateTime.ParseExact(AjustarCampos(arrayline[contadorreg].ToString(), Convert.ToChar(estDetalle.justificador)), sformato_fecha, null);
                                                        else
                                                            vEntidad.fecha = DateTime.ParseExact(arrayline[contadorreg].ToString(), sformato_fecha, null); //estructura.separador_miles
                                                    }
                                                    if (estDetalle.codigo_campo == 2)
                                                    {
                                                        if (estDetalle.justificacion == 1 && estDetalle.justificador != null)
                                                            vEntidad.num_documento = AjustarCampos(arrayline[contadorreg].ToString(), Convert.ToChar(estDetalle.justificador));
                                                        else
                                                            vEntidad.num_documento = arrayline[contadorreg].ToString();
                                                    }
                                                    if (estDetalle.codigo_campo == 3)
                                                    {
                                                        if (estDetalle.justificacion == 1 && estDetalle.justificador != null)
                                                            vEntidad.cod_concepto = AjustarCampos(arrayline[contadorreg].ToString(), Convert.ToChar(estDetalle.justificador));
                                                        else
                                                            vEntidad.cod_concepto = arrayline[contadorreg].ToString();
                                                    }
                                                    if (estDetalle.codigo_campo == 4)
                                                    {
                                                        if (estDetalle.justificacion == 1 && estDetalle.justificador != null)
                                                            vEntidad.tipo_movimiento = AjustarCampos(arrayline[contadorreg].ToString(), Convert.ToChar(estDetalle.justificador));
                                                        else
                                                            vEntidad.tipo_movimiento = arrayline[contadorreg].ToString();
                                                    }
                                                    if (estDetalle.codigo_campo == 5)
                                                    {
                                                        if (estDetalle.justificacion == 1 && estDetalle.justificador != null)
                                                            vEntidad.referencia1 = AjustarCampos(arrayline[contadorreg].ToString(), Convert.ToChar(estDetalle.justificador));
                                                        else
                                                            vEntidad.referencia1 = arrayline[contadorreg].ToString();
                                                    }
                                                    if (estDetalle.codigo_campo == 6)
                                                    {
                                                        if (estDetalle.justificacion == 1 && estDetalle.justificador != null)
                                                            vEntidad.referencia2 = AjustarCampos(arrayline[contadorreg].ToString().Replace(",", ""), Convert.ToChar(estDetalle.justificador));
                                                        else
                                                            vEntidad.referencia2 = arrayline[contadorreg].ToString().Replace(",", "");
                                                    }
                                                    if (estDetalle.codigo_campo == 7)
                                                    {
                                                        if (estDetalle.justificacion == 1 && estDetalle.justificador != null)
                                                            vEntidad.valor = Decimal.Parse(AjustarCampos(arrayline[contadorreg].ToString().Replace(estructura.separador_decimal, GlobalWeb.gSeparadorDecimal), Convert.ToChar(estDetalle.justificador)));
                                                        else
                                                            vEntidad.valor = Decimal.Parse(arrayline[contadorreg].ToString().Replace(estructura.separador_decimal, GlobalWeb.gSeparadorDecimal));
                                                    }
                                                }
                                                contadorreg += 1;
                                            }

                                            lstDatosPorCargar.Add(vEntidad);
                                        }
                                        else
                                        {
                                            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                                            // Si el archivo es De Ancho Fijo
                                            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                                        }
                                    }
                                }

                                contador = contador + 1;
                            }
                        }
                    }
                }
                catch (IOException ex)
                {
                    VerError(ex.Message);
                }

                Session["Recaudos"] = lstDatosPorCargar;

                //CARGAR LOS DATOS POR CARGAR A LA LISTA GENERAL
                foreach (DetExtractoBancario rItem in lstDatosPorCargar)
                {
                    lstDetaGeneral.Add(rItem);
                }


                if (lstDatosPorCargar.Count > 0)
                {
                    gvDetalleEx.DataSource = lstDetaGeneral;
                    gvDetalleEx.DataBind();
                    ObtenerListaDetalle();
                    CalcularTotal();
                    txtNumReg.Text = lstDetaGeneral.Count.ToString();
                }

                mvAplicar.ActiveViewIndex = 0;
                Site toolBar = (Site)this.Master;
                toolBar.MostrarGuardar(true);
                toolBar.MostrarCancelar(true);
                toolBar.MostrarCargar(true);

            }
            else
            {
                lblMensajes.Text = "Seleccione un Archivo";
            }
        }
        catch (Exception ex)
        {
            VerError("ERROR: "+ex.Message);
        }
    }
}
