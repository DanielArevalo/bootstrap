using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using Xpinn.Contabilidad.Services;
using Xpinn.Contabilidad.Entities;
using System.Text;
using System.IO;
using System.Globalization;
using System.Web.UI.HtmlControls;
using Microsoft.Reporting.WebForms;

public partial class Lista : GlobalWeb
{
    Xpinn.Tesoreria.Services.ImpresionMasivaServices ComprobanteServicio = new Xpinn.Tesoreria.Services.ImpresionMasivaServices();
    Xpinn.Contabilidad.Services.ComprobanteService consultaServicio = new Xpinn.Contabilidad.Services.ComprobanteService();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                if (Session["DATA_RGIROS"] == null)
                    VisualizarOpciones(ComprobanteServicio.CodigoPrograma, "L");

                Site toolBar = (Site)this.Master;
                toolBar.eventoNuevo += btnNuevo_Click;
                toolBar.eventoLimpiar += btnLimpiar_Click;
                toolBar.eventoImprimir += btnImprimir_Click;
                toolBar.eventoExportar += btnExportar_Click;
                toolBar.eventoConsultar += btnConsultar_Click;
                toolBar.eventoCancelar += btnCancelar_Click;
                toolBar.MostrarNuevo(false);
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ComprobanteServicio.GetType().Name + "L", "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                LimpiarValoresConsulta(pConsulta, ComprobanteServicio.GetType().Name);
                CargarDropDown();
                Session["R_REGIRO"] = null; 
                //CargarValoresConsulta(pConsulta, ComprobanteServicio.GetType().Name);
                if (Request["num_comp"] != null)
                {
                    txtNumComp.Text = Request["num_comp"].ToString();
                    Actualizar();
                }
                if (Session[ComprobanteServicio.CodigoPrograma + ".id"] != null)
                {                    
                    idObjeto = Session[ComprobanteServicio.CodigoPrograma + ".id"].ToString();
                    ddlIdentificacion.Text = idObjeto;                    
                    Actualizar();
                }
                else
                {
                    if (Session[ComprobanteServicio.GetType().Name + ".consulta"] != null)
                        Actualizar();
                }                
                if (Session["DATA_RGIROS"] != null)
                 {
                     ImpresionDefault();
                 }     
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ComprobanteServicio.GetType().Name + "L", "Page_Load", ex);
        }
    }

    private void CargarDropDown()
    {
        Usuario usuario = new Usuario();
        usuario = (Usuario)Session["Usuario"];

        Xpinn.Contabilidad.Services.TipoComprobanteService TipoComprobanteService = new Xpinn.Contabilidad.Services.TipoComprobanteService();
        Xpinn.Contabilidad.Entities.TipoComprobante TipoComprobante = new Xpinn.Contabilidad.Entities.TipoComprobante();
        ddlTipoComprobante.DataSource = TipoComprobanteService.ListarTipoComprobante(TipoComprobante, "", (Usuario)Session["Usuario"]);
        ddlTipoComprobante.DataTextField = "descripcion";
        ddlTipoComprobante.DataValueField = "tipo_comprobante";
        ddlTipoComprobante.DataBind();

        Xpinn.Caja.Services.CiudadService CiudadService = new Xpinn.Caja.Services.CiudadService();
        Xpinn.Caja.Entities.Ciudad Ciudad = new Xpinn.Caja.Entities.Ciudad();
        ddlCiudad.DataSource = CiudadService.ListadoCiudad(Ciudad, usuario);
        ddlCiudad.DataTextField = "nom_ciudad";
        ddlCiudad.DataValueField = "cod_ciudad";
        ddlCiudad.DataBind();

        Xpinn.Contabilidad.Services.ConceptoService ConceptoService = new Xpinn.Contabilidad.Services.ConceptoService();
        Xpinn.Contabilidad.Entities.Concepto Concepto = new Xpinn.Contabilidad.Entities.Concepto();
        ddlConcepto.DataSource = ConceptoService.ListarConcepto(Concepto, usuario);
        ddlConcepto.DataTextField = "descripcion";
        ddlConcepto.DataValueField = "concepto";
        ddlConcepto.DataBind();

        ddlOrden1.Items.Insert(0, new ListItem("Seleccione un item","0"));
        ddlOrden1.Items.Insert(1, new ListItem("Número Comprobante", "V_Comprobante.Num_Comp"));
        ddlOrden1.Items.Insert(2, new ListItem("Tipo Comprobante", "V_Comprobante.Tipo_Comp"));
        ddlOrden1.Items.Insert(3, new ListItem("Fecha", "V_Comprobante.Fecha"));
        ddlOrden1.Items.Insert(4, new ListItem("Concepto", "V_Comprobante.Descripcion_Concepto"));
        ddlOrden1.Items.Insert(5, new ListItem("Ciudad", "V_Comprobante.Ciudad"));
        ddlOrden1.Items.Insert(6, new ListItem("Identificación", "V_Comprobante.Iden_Benef"));
        ddlOrden1.Items.Insert(7, new ListItem("Nombres", "V_Comprobante.Nombres"));
        ddlOrden1.Items.Insert(8, new ListItem("Apellidos", "V_Comprobante.Apellidos"));
        ddlOrden1.Items.Insert(9, new ListItem("Estado", "V_Comprobante.Estado"));
        ddlOrden1.Items.Insert(10, new ListItem("Valor", "V_Comprobante.Totalcom"));
        ddlOrden1.SelectedIndex = 0;
        ddlOrden1.DataBind();

        ddlOrdenLuego.Items.Insert(0, new ListItem("Seleccione un item", "0"));
        ddlOrdenLuego.Items.Insert(1, new ListItem("Número Comprobante", "V_Comprobante.Num_Comp"));
        ddlOrdenLuego.Items.Insert(2, new ListItem("Tipo Comprobante", "V_Comprobante.Tipo_Comp"));
        ddlOrdenLuego.Items.Insert(3, new ListItem("Fecha", "V_Comprobante.Fecha"));
        ddlOrdenLuego.Items.Insert(4, new ListItem("Concepto", "V_Comprobante.Descripcion_Concepto"));
        ddlOrdenLuego.Items.Insert(5, new ListItem("Ciudad", "V_Comprobante.Ciudad"));
        ddlOrdenLuego.Items.Insert(6, new ListItem("Identificación", "V_Comprobante.Iden_Benef"));
        ddlOrdenLuego.Items.Insert(7, new ListItem("Nombres", "V_Comprobante.Nombres"));
        ddlOrdenLuego.Items.Insert(8, new ListItem("Apellidos", "V_Comprobante.Apellidos"));
        ddlOrdenLuego.Items.Insert(9, new ListItem("Estado", "V_Comprobante.Estado"));
        ddlOrdenLuego.Items.Insert(10, new ListItem("Valor", "V_Comprobante.Totalcom"));
        ddlOrdenLuego.SelectedIndex = 0;
        ddlOrdenLuego.DataBind();

        DropDownFormaDesembolso.Items.Insert(0, new ListItem("Seleccione un Item", "0"));
        DropDownFormaDesembolso.Items.Insert(1, new ListItem("Efectivo", "1"));
        DropDownFormaDesembolso.Items.Insert(2, new ListItem("Cheque", "2"));
        DropDownFormaDesembolso.Items.Insert(3, new ListItem("Transferencia", "3"));
        DropDownFormaDesembolso.SelectedIndex = 0;
        DropDownFormaDesembolso.DataBind();

        Xpinn.Caja.Services.BancosService bancoService = new Xpinn.Caja.Services.BancosService();
        Xpinn.Caja.Entities.Bancos banco = new Xpinn.Caja.Entities.Bancos();

        ddlEntidadOrigen.DataSource = bancoService.ListarBancosEntidad(banco, (Usuario)Session["usuario"]);
        ddlEntidadOrigen.DataTextField = "nombrebanco";
        ddlEntidadOrigen.DataValueField = "cod_banco";
        ddlEntidadOrigen.AppendDataBoundItems = true;
        ddlEntidadOrigen.Items.Insert(0,new ListItem("Seleccione un item","0"));
        ddlEntidadOrigen.DataBind();

    }

    protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            GuardarValoresConsulta(pConsulta, ComprobanteServicio.GetType().Name);
            Navegar(Pagina.Nuevo);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ComprobanteServicio.GetType().Name + "L", "btnNuevo_Click", ex);
        }

    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            GuardarValoresConsulta(pConsulta, ComprobanteServicio.GetType().Name);
            Actualizar();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ComprobanteServicio.GetType().Name + "L", "btnConsultar_Click", ex);
        }
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        LimpiarValoresConsulta(pConsulta, ComprobanteServicio.GetType().Name);
    }


    protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvLista.PageIndex = e.NewPageIndex;
            Actualizar();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ComprobanteServicio.GetType().Name + "L", "gvLista_PageIndexChanging", ex);
        }
    }

    private void Actualizar()
    {
        VerError("");
        try
        {
            Configuracion conf = new Configuracion();
            List<Xpinn.Contabilidad.Entities.Comprobante> lstConsulta = new List<Xpinn.Contabilidad.Entities.Comprobante>();
            string sFiltro = " ";
            if (txtFechaIni.TieneDatos)
                if (txtFechaIni.ToDate.Trim() != "" )
                    sFiltro += " And v_comprobante.fecha >= To_Date('" + txtFechaIni.ToDate.Trim() + "', '" + conf.ObtenerFormatoFecha() + "')";
            if (txtFechaFin.TieneDatos)
                if (txtFechaFin.ToDate.Trim() != "")
                    sFiltro += " And v_comprobante.fecha <= To_Date('" + txtFechaFin.ToDate.Trim() + "', '" + conf.ObtenerFormatoFecha() + "')"; 
            if (txtNumSop.Text.Trim() != "")
                sFiltro += " And v_comprobante.n_documento = '" + txtNumSop.Text.Trim() + "' ";
            if (txtValorTotal.Text.Trim() != "" && txtValorTotal.Text.Trim() != "0")
                sFiltro += " And v_comprobante.totalcom = " + txtValorTotal.Text.Trim().Replace(".", "") + " ";
            if (DropDownFormaDesembolso.SelectedIndex != 0)
                sFiltro += " AND V_Comprobante.tipo_pago = " + DropDownFormaDesembolso.SelectedValue;
            if (ddlEntidadOrigen.SelectedIndex != 0)
                sFiltro += " AND  V_Comprobante.Entidad  = " + ddlEntidadOrigen.SelectedValue;
            
            string Orden = "";
            if (ddlOrden1.SelectedIndex != 0)
            {
                Orden += ddlOrden1.SelectedValue;
                if (chkDesc.Checked)
                    Orden += " Desc";
            }
            if (ddlOrdenLuego.SelectedIndex != 0)
            {
                if (Orden != "")
                    Orden += ", " + ddlOrdenLuego.SelectedValue;
                else
                    Orden += ddlOrdenLuego.SelectedValue;
                if (chkDescLuego.Checked)
                    Orden += " Desc";
            }
            lstConsulta = ComprobanteServicio.ListarComprobante(ObtenerValores(), (Usuario)Session["usuario"], sFiltro, Orden);
            
            gvLista.DataSource = lstConsulta;
            Session["DTLISTA"] = lstConsulta;

            if (lstConsulta.Count > 0)
            {
                gvLista.Visible = true;
                lblInfo.Visible = false;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                gvLista.DataBind();
                ValidarPermisosGrilla(gvLista);
            }
            else
            {
                gvLista.Visible = false;
                lblInfo.Visible = true;
                lblTotalRegs.Visible = false;
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ComprobanteServicio.GetType().Name + "L", "Actualizar", ex);
        }
    }

    

    private Xpinn.Contabilidad.Entities.Comprobante ObtenerValores()
    {
        Xpinn.Contabilidad.Entities.Comprobante Comprobante = new Xpinn.Contabilidad.Entities.Comprobante();

        try
        {
            if (txtNumComp.Text.Trim() != "")
                Comprobante.num_comp = Convert.ToInt64(txtNumComp.Text.Trim());
           
            Comprobante.tipo_comp = 5;
            if (ddlConcepto.SelectedValue != null && ddlConcepto.SelectedIndex != 0)
                Comprobante.concepto = Convert.ToInt64(ddlConcepto.SelectedValue);
            if (ddlCiudad.SelectedValue != null && ddlCiudad.SelectedIndex != 0)
                Comprobante.ciudad = Convert.ToInt64(ddlCiudad.SelectedValue);
            if (ddlEstado.SelectedValue != null && ddlEstado.SelectedIndex != 0)
                Comprobante.estado = ddlEstado.SelectedValue;
            if (ddlIdentificacion.Text != null && !ddlIdentificacion.Text.Equals("0"))
                Comprobante.iden_benef = ddlIdentificacion.Text;
            if (ddlNombres.Text != null && !ddlNombres.Text.Equals("0"))
                Comprobante.nombres = ddlNombres.Text;
            if (ddlApellidos.Text != null && !ddlApellidos.Text.Equals("0"))
                Comprobante.apellidos = ddlApellidos.Text;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ComprobanteServicio.GetType().Name + "L", "ObtenerValores", ex);
        }

        return Comprobante;
    }


    private void ImpresionDefault()
    {
        Usuario pUsuario = new Usuario();
        pUsuario = (Usuario)Session["Usuario"];
         
        //CREAR TABLA GENERAL;
        System.Data.DataTable table = new System.Data.DataTable();

        table.Columns.Add("fecha");
        table.Columns.Add("valor");
        table.Columns.Add("nombre1");
        table.Columns.Add("cardinal");
        table.Columns.Add("entidad");
        table.Columns.Add("nit");
        table.Columns.Add("tipoComprobante");
        table.Columns.Add("num_comp");
        table.Columns.Add("identificacion");
        table.Columns.Add("tipo_indetificacion");
        table.Columns.Add("nombres");
        table.Columns.Add("ciudad");
        table.Columns.Add("concepto");
        table.Columns.Add("cod_cuenta");
        table.Columns.Add("nom_cuenta");
        table.Columns.Add("ident_deta");
        table.Columns.Add("detalle");
        table.Columns.Add("valordebito");
        table.Columns.Add("valorcredito");
        table.Columns.Add("tipo");
        table.Columns.Add("totaldebito");
        table.Columns.Add("totalcredito");
        table.Columns.Add("num_cheque");
        table.Columns.Add("detalle_general");
        table.Columns.Add("rptaEgreso");
        table.Columns.Add("CC");

        //CONSULTAR SI SE IMPRIMEN EL MONTO EN TEXTO
        Xpinn.Comun.Data.GeneralData DAGenenal = new Xpinn.Comun.Data.GeneralData();
        Xpinn.Comun.Entities.General pEntity = new Xpinn.Comun.Entities.General();

        pEntity = DAGenenal.ConsultarGeneral(90163, (Usuario)Session["usuario"]);
        
        int contador = 0;
        if (Session["DATA_RGIROS"] == null)
        {
            foreach (GridViewRow rFila in gvLista.Rows)
            {
                CheckBox cbSeleccionar = (CheckBox)rFila.FindControl("cbSeleccionar");
                if (cbSeleccionar != null)
                {
                    if (cbSeleccionar.Checked == true)
                    {
                        //DECLARACION DE VARIABLES
                        Xpinn.Contabilidad.Entities.Comprobante vComprobante = new Xpinn.Contabilidad.Entities.Comprobante();
                        List<Xpinn.Contabilidad.Entities.DetalleComprobante> LstDetalleComprobante = new List<Xpinn.Contabilidad.Entities.DetalleComprobante>();

                        String num_comp = "", tipo_comp = "", nomtipo_comp = "", tipo_Anterior = "", labelValor = "", nom_ciudad = "", nom_concepto = "";
                        num_comp = rFila.Cells[1].Text;
                        Session[ComprobanteServicio.CodigoPrograma + ".num_comp"] = num_comp;
                        tipo_comp = rFila.Cells[2].Text;
                        Session[ComprobanteServicio.CodigoPrograma + ".tipo_comp"] = tipo_comp;

                        //CAPTURAR NOMBRES DE DATOS
                        tipo_Anterior = ddlTipoComprobante.SelectedValue;
                        ddlTipoComprobante.SelectedValue = tipo_comp;
                        nomtipo_comp = ddlTipoComprobante.SelectedItem.Text;
                        ddlTipoComprobante.SelectedValue = tipo_Anterior;

                        //CONSULTANDO DATOS DEL COMPROBANTE SELECCIONADO
                        if (consultaServicio.ConsultarComprobante(Convert.ToInt64(num_comp), Convert.ToInt64(tipo_comp), ref vComprobante, ref LstDetalleComprobante, (Usuario)Session["Usuario"]))
                        {
                            Session["DetalleComprobante"] = LstDetalleComprobante;

                            if (vComprobante.ciudad != 0)
                            {
                                tipo_Anterior = ddlCiudad.SelectedValue;
                                ddlCiudad.SelectedValue = vComprobante.ciudad.ToString();
                                nom_ciudad = ddlCiudad.SelectedItem.Text;
                                ddlCiudad.SelectedValue = tipo_Anterior;
                            }
                            if (vComprobante.concepto != 0)
                            {
                                tipo_Anterior = ddlConcepto.SelectedValue;
                                ddlConcepto.SelectedValue = vComprobante.concepto.ToString();
                                nom_concepto = ddlConcepto.SelectedItem.Text;
                                ddlConcepto.SelectedValue = tipo_Anterior;
                            }

                            // Determinar la fecha del comprobante            
                            string a = vComprobante.fecha.ToString("yyyy/MM/dd");
                            string fecha = a.Replace("/", "     ");

                            // Determinar el valor del giro cuando es comprobante de egreso
                            DetalleComprobante var = new DetalleComprobante();
                            if (tipo_comp == "5")
                            {
                                for (int i = 0; i < LstDetalleComprobante.Count; i++)
                                {
                                    var = LstDetalleComprobante[i];
                                    if (var.cod_cuenta != null)
                                    {
                                        if (consultaServicio.CuentaEsGiro(var.cod_cuenta, (Usuario)Session["Usuario"]))
                                        {
                                            labelValor = string.Format("{0:N2}", var.valor);
                                            i = LstDetalleComprobante.Count + 1;
                                        }
                                    }
                                }
                            }
                            if (labelValor == "")
                                labelValor = "0";

                            Cantidad_a_Letra.Cardinalidad numero = new Cantidad_a_Letra.Cardinalidad();
                            string cardinal = " ";
                            if (labelValor != "0")
                            {
                                cardinal = numero.enletras(labelValor.Replace(".", ""));
                                int cont = cardinal.Trim().Length - 1;
                                int cont2 = cont - 7;
                                if (cont2 >= 0)
                                {
                                    string c = cardinal.Substring(cont2);
                                    if (cardinal.Trim().Substring(cont2) == "MILLONES" || cardinal.Trim().Substring(cont2 + 2) == "MILLON")
                                        cardinal = cardinal + " DE PESOS M/CTE";
                                    else
                                        cardinal = cardinal + " PESOS M/CTE";
                                }
                            }

                            string valoresTotales = "", totDeb = "", totCred = "", Difer;
                            valoresTotales = CalcularTotal();
                            string[] pDatos = valoresTotales.ToString().Split('|');

                            totDeb = pDatos[0];
                            totCred = pDatos[1];
                            Difer = pDatos[2];

                            string num_cheque = "";
                            if (vComprobante.tipo_comp == 1)
                                num_cheque = vComprobante.num_consig;
                            else
                                num_cheque = vComprobante.n_documento;

                            //RECUPERAR EL DETALLE DEL COMPROBANTE SELECCIONADO
                            foreach (DetalleComprobante item in LstDetalleComprobante)
                            {
                                System.Data.DataRow datarw;
                                datarw = table.NewRow();
                                datarw[0] = vacios(fecha);
                                datarw[1] = labelValor;
                                if (vComprobante.cheque_nombre != null)
                                    datarw[2] = vacios(vComprobante.cheque_nombre);
                                else
                                    datarw[2] = vacios(vComprobante.nombre);
                                if (pEntity != null && pEntity.valor != null)
                                {
                                    if (pEntity.valor == "1")
                                        cardinal = " ";
                                }
                                datarw[3] = cardinal;
                                datarw[4] = pUsuario.empresa;
                                datarw[5] = pUsuario.nitempresa;
                                datarw[6] = vacios(nomtipo_comp.ToUpper());
                                datarw[7] = vacios(num_comp);
                                datarw[8] = vacios(vComprobante.iden_benef.ToString().Trim());
                                datarw[9] = vacios(vComprobante.nom_tipo_iden);
                                datarw[10] = vacios(vComprobante.nombre);
                                datarw[11] = vacios(nom_ciudad);
                                datarw[12] = vacios(nom_concepto);
                                datarw[13] = item.cod_cuenta;
                                datarw[14] = item.nombre_cuenta;
                                datarw[15] = item.identificacion;
                                datarw[16] = item.detalle;

                                if (item.tipo == "D")
                                {
                                    datarw[17] = item.valor;
                                    datarw[18] = "";
                                }
                                if (item.tipo == "C")
                                {
                                    datarw[17] = "";
                                    datarw[18] = item.valor;
                                }
                                datarw[19] = item.tipo;
                                datarw[20] = vacios(totDeb);
                                datarw[21] = vacios(totCred);
                                datarw[22] = num_cheque;
                                datarw[23] = vacios(vComprobante.observaciones);
                                datarw[24] = tipo_comp.Trim();
                                datarw[25] = item.centro_costo;
                                table.Rows.Add(datarw);
                            }
                        }
                        contador++;
                    }
                }
            }
        }
        else
        {
            Session["R_REGIRO"] = 1;
            List<Xpinn.Contabilidad.Entities.Comprobante> lstCompro = new List<Xpinn.Contabilidad.Entities.Comprobante>();
            lstCompro = (List<Xpinn.Contabilidad.Entities.Comprobante>)Session["DATA_RGIROS"];
            
            foreach(Comprobante pComp in lstCompro)
            {
                //DECLARACION DE VARIABLES
                Xpinn.Contabilidad.Entities.Comprobante vComprobante = new Xpinn.Contabilidad.Entities.Comprobante();
                List<Xpinn.Contabilidad.Entities.DetalleComprobante> LstDetalleComprobante = new List<Xpinn.Contabilidad.Entities.DetalleComprobante>();

                String num_comp = "", tipo_comp = "", nomtipo_comp = "", tipo_Anterior = "", labelValor = "", nom_ciudad = "", nom_concepto = "";

                num_comp = pComp.num_comp.ToString();
                tipo_comp = pComp.tipo_comp.ToString();
                //CAPTURAR NOMBRES DE DATOS

                tipo_Anterior = ddlTipoComprobante.SelectedValue;
                ddlTipoComprobante.SelectedValue = tipo_comp;
                nomtipo_comp = ddlTipoComprobante.SelectedItem.Text;
                ddlTipoComprobante.SelectedValue = tipo_Anterior;

                //CONSULTANDO DATOS DEL COMPROBANTE SELECCIONADO
                if (consultaServicio.ConsultarComprobante(Convert.ToInt64(num_comp), Convert.ToInt64(tipo_comp), ref vComprobante, ref LstDetalleComprobante, (Usuario)Session["Usuario"]))
                {
                    Session["DetalleComprobante"] = LstDetalleComprobante;

                    if (vComprobante.ciudad != 0)
                    {
                        tipo_Anterior = ddlCiudad.SelectedValue;
                        ddlCiudad.SelectedValue = vComprobante.ciudad.ToString();
                        nom_ciudad = ddlCiudad.SelectedItem.Text;
                        ddlCiudad.SelectedValue = tipo_Anterior;
                    }
                    if (vComprobante.concepto != 0)
                    {
                        tipo_Anterior = ddlConcepto.SelectedValue;
                        ddlConcepto.SelectedValue = vComprobante.concepto.ToString();
                        nom_concepto = ddlConcepto.SelectedItem.Text;
                        ddlConcepto.SelectedValue = tipo_Anterior;
                    }

                    // Determinar la fecha del comprobante            
                    string a = vComprobante.fecha.ToString("yyyy/MM/dd");
                    string fecha = a.Replace("/", "     ");

                    // Determinar el valor del giro cuando es comprobante de egreso
                    DetalleComprobante var = new DetalleComprobante();
                    if (tipo_comp == "5")
                    {
                        for (int i = 0; i < LstDetalleComprobante.Count; i++)
                        {
                            var = LstDetalleComprobante[i];
                            if (var.cod_cuenta != null)
                            {
                                if (consultaServicio.CuentaEsGiro(var.cod_cuenta, (Usuario)Session["Usuario"]))
                                {
                                    labelValor = string.Format("{0:N2}", var.valor);
                                    i = LstDetalleComprobante.Count + 1;
                                }
                            }
                        }
                    }
                    if (labelValor == "")
                        labelValor = "0";

                    Cantidad_a_Letra.Cardinalidad numero = new Cantidad_a_Letra.Cardinalidad();
                    string cardinal = " ";
                    if (labelValor != "0")
                    {
                        cardinal = numero.enletras(labelValor.Replace(".", ""));
                        int cont = cardinal.Trim().Length - 1;
                        int cont2 = cont - 7;
                        if (cont2 >= 0)
                        {
                            string c = cardinal.Substring(cont2);
                            if (cardinal.Trim().Substring(cont2) == "MILLONES" || cardinal.Trim().Substring(cont2 + 2) == "MILLON")
                                cardinal = cardinal + " DE PESOS M/CTE";
                            else
                                cardinal = cardinal + " PESOS M/CTE";
                        }
                    }

                    string valoresTotales = "", totDeb = "", totCred = "", Difer;
                    valoresTotales = CalcularTotal();
                    string[] pDatos = valoresTotales.ToString().Split('|');

                    totDeb = pDatos[0];
                    totCred = pDatos[1];
                    Difer = pDatos[2];

                    string num_cheque = "";
                    if (vComprobante.tipo_comp == 1)
                        num_cheque = vComprobante.num_consig;
                    else
                        num_cheque = vComprobante.n_documento;

                    string pCheque_nom = "";
                    if (vComprobante.cheque_nombre != null)
                        pCheque_nom = vComprobante.cheque_nombre;
                    else
                        pCheque_nom = vComprobante.nombre;
                    //RECUPERAR EL DETALLE DEL COMPROBANTE SELECCIONADO
                    foreach (DetalleComprobante item in LstDetalleComprobante)
                    {
                        System.Data.DataRow datarw;
                        datarw = table.NewRow();
                        datarw[0] = vacios(fecha);
                        datarw[1] = labelValor;
                        datarw[2] = vacios(pCheque_nom);
                        if (pEntity != null && pEntity.valor != null)
                        {
                            if (pEntity.valor == "1")
                                cardinal = " ";
                        }
                        datarw[3] = cardinal;
                        datarw[4] = pUsuario.empresa;
                        datarw[5] = pUsuario.nitempresa;
                        datarw[6] = vacios(nomtipo_comp.ToUpper());
                        datarw[7] = vacios(num_comp);
                        datarw[8] = vacios(vComprobante.iden_benef.ToString().Trim());
                        datarw[9] = vacios(vComprobante.nom_tipo_iden);
                        datarw[10] = vacios(vComprobante.nombre);
                        datarw[11] = vacios(nom_ciudad);
                        datarw[12] = vacios(nom_concepto);
                        datarw[13] = item.cod_cuenta;
                        datarw[14] = item.nombre_cuenta;
                        datarw[15] = item.identificacion;
                        datarw[16] = item.detalle;

                        if (item.tipo == "D")
                        {
                            datarw[17] = item.valor;
                            datarw[18] = "";
                        }
                        if (item.tipo == "C")
                        {
                            datarw[17] = "";
                            datarw[18] = item.valor;
                        }
                        datarw[19] = item.tipo;
                        datarw[20] = vacios(totDeb);
                        datarw[21] = vacios(totCred);
                        datarw[22] = num_cheque;
                        datarw[23] = vacios(vComprobante.observaciones);
                        datarw[24] = tipo_comp.Trim();
                        datarw[25] = item.centro_costo;
                        table.Rows.Add(datarw);
                    }
                }
                contador++;
            }
        }

        if (contador != 0)
        {
            if (Session[ComprobanteServicio.CodigoPrograma + ".tipo"] != null)
                RpviewComprobante.LocalReport.ReportPath = "Page/Tesoreria/ImpresionMasiva/rptImpresionTrans.rdlc";
            else
                RpviewComprobante.LocalReport.ReportPath = "Page/Tesoreria/ImpresionMasiva/rptImpresionMasiva.rdlc";
            // PARAMETROS DEL COMPROBANTE
            ReportParameter[] param = new ReportParameter[2];
            param[0] = new ReportParameter("ImagenReport", ImagenReporte());
            param[1] = new ReportParameter("pElaborado", pUsuario.nombre);
            RpviewComprobante.LocalReport.EnableExternalImages = true;
            RpviewComprobante.LocalReport.SetParameters(param);

            // Determinando las margenes del comprobante
            System.Drawing.Printing.Margins margins;
            if (Session[ComprobanteServicio.CodigoPrograma + ".tipo"] == null)
                margins = new System.Drawing.Printing.Margins(100, 100, 100, 100);
            var sa = RpviewComprobante.LocalReport.GetDefaultPageSettings();
            RpviewComprobante.LocalReport.DataSources.Clear();
            ReportDataSource rds = new ReportDataSource("DataSet1", table);
            RpviewComprobante.LocalReport.DataSources.Add(rds);
            RpviewComprobante.LocalReport.Refresh();

            Site toolBar = (Site)Master;
            toolBar.MostrarImprimir(false);
            toolBar.MostrarExportar(false);
            toolBar.MostrarLimpiar(false);
            toolBar.MostrarConsultar(false);

            frmPrint.Visible = false;
            RpviewComprobante.Visible = true;
            Session.Remove("DATA_RGIROS");
            if (Session[ComprobanteServicio.CodigoPrograma + ".tipo"] != null)
                Session.Remove(ComprobanteServicio.CodigoPrograma + ".tipo");
            mvComprobante.ActiveViewIndex = 1;
        }
    }


    protected void btnImprimir_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImpresionDefault();
        }
        catch(Exception ex)
        {
            BOexcepcion.Throw(ComprobanteServicio.CodigoPrograma, "btnImprimir_Click", ex);
        }
    }

    public string CalcularTotal()
    {
        decimal? totdeb = 0.00m;
        decimal? totcre = 0.00m;
        decimal? diferencia = 0.00m;
        string valorRetorno = "";
        List<DetalleComprobante> LstDetalleComprobante = new List<DetalleComprobante>();
        if (Session["DetalleComprobante"] != null)
        {
            LstDetalleComprobante = (List<DetalleComprobante>)Session["DetalleComprobante"];
            for (int i = 0; i < LstDetalleComprobante.Count; i++)
            {
                if (LstDetalleComprobante[i].valor != null)
                {
                    if (LstDetalleComprobante[i].tipo == "D" | LstDetalleComprobante[i].tipo == "d")
                        totdeb = totdeb + LstDetalleComprobante[i].valor;
                    else
                        totcre = totcre + LstDetalleComprobante[i].valor;
                }
            }
            diferencia = totdeb - totcre;
            string sDeb = "", sCre = "", sDif = "";
            sDeb = String.Format("{0:N2}", totdeb);
            sCre = String.Format("{0:N2}", totcre);
            sDif = String.Format("{0:N2}", diferencia);
            valorRetorno = sDeb + "|" + sCre + "|" + sDif;
        }
        return valorRetorno;
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

    protected void cbSeleccionarEncabezado_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox cbSeleccionarEncabezado = (CheckBox)sender;
        if (cbSeleccionarEncabezado != null)
        {
            foreach (GridViewRow rFila in gvLista.Rows)
            {
                CheckBox cbSeleccionar = (CheckBox)rFila.FindControl("cbSeleccionar");
                cbSeleccionar.Checked = cbSeleccionarEncabezado.Checked;
            }
        }
    }


    protected void btnExportar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        if (gvLista.Rows.Count > 0 && Session["DTLISTA"] != null)
        {
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            Page pagina = new Page();
            dynamic form = new HtmlForm();
            gvLista.Columns[0].Visible = false;
            gvLista.AllowPaging = false;
            gvLista.DataSource = Session["DTLISTA"];
            gvLista.DataBind();
            gvLista.EnableViewState = false;
            pagina.EnableEventValidation = false;
            pagina.DesignerInitialize();
            pagina.Controls.Add(form);
            form.Controls.Add(gvLista);
            pagina.RenderControl(htw);
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=ComprobantesEgreso.xls");
            Response.Charset = "UTF-8";
            Response.ContentEncoding = Encoding.Default;
            Response.Write(sb.ToString());
            Response.End();
        }
        else
        {
            VerError("No existen datos, genere la consulta");
        }
    }

    
    protected void btnConsultaPersonas_Click(object sender, EventArgs e)
    {        
        ctlBusquedaPersonas.Motrar(true, "ddlIdentificacion", "");
    }

    protected void btnRegresarComp_Click(object sender, EventArgs e)
    {
        mvComprobante.ActiveViewIndex = 0;
        Site toolBar = (Site)Master;
        toolBar.MostrarImprimir(true);
        toolBar.MostrarExportar(true);
        toolBar.MostrarLimpiar(true);
        toolBar.MostrarConsultar(true);
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        if (Session["R_REGIRO"] == null)
        {
            mvComprobante.ActiveViewIndex = 0;
            Site toolBar = (Site)Master;
            toolBar.MostrarImprimir(true);
            toolBar.MostrarExportar(true);
            toolBar.MostrarLimpiar(true);
            toolBar.MostrarConsultar(true);
        }
        else
        {
            Navegar("../../Tesoreria/RealizacionGiros/Lista.aspx");
        }
    }
    protected void btnImprime_Click(object sender, EventArgs e)
    {
        // Guardar el reporte en un PDF
        Warning[] warnings;
        string[] streamids;
        string mimeType;
        string encoding;
        string extension;
        byte[] bytes = RpviewComprobante.LocalReport.Render("PDF", null, out mimeType,
                       out encoding, out extension, out streamids, out warnings);
        FileStream fs = new FileStream(HttpContext.Current.Server.MapPath("output.pdf"),
        FileMode.Create);
        fs.Write(bytes, 0, bytes.Length);
        fs.Close();
        Session["Archivo" + Usuario.codusuario] = Server.MapPath("output.pdf");
        frmPrint.Visible = true;
        RpviewComprobante.Visible = false;
    }
}