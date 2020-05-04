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

partial class Nuevo : GlobalWeb
{
    private Xpinn.Ahorros.Services.ParametroGMFService ahorrosServicio = new Xpinn.Ahorros.Services.ParametroGMFService();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[ahorrosServicio.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(ahorrosServicio.CodigoPrograma, "E");
            else
                VisualizarOpciones(ahorrosServicio.CodigoPrograma, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
            ctlMensaje.eventoClick += btnContinuarMen_Click;
            toolBar.MostrarConsultar(false);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ahorrosServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
         
            if (!IsPostBack)
            {
                if (chkasume.Checked == false)
                {
                    txtAsume.Enabled = false;
                }
                mvAhorroVista.ActiveViewIndex = 0;
                CargarListas();
                if (Session[ahorrosServicio.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[ahorrosServicio.CodigoPrograma + ".id"].ToString();
                    Session.Remove(ahorrosServicio.CodigoPrograma + ".id");
                    txtAsume.Enabled = true;
                    ObtenerDatos(idObjeto);
                    CargarListar();

                }
                else
                {
                    CargarListar();
                }
                if (chkasume.Checked == false)
                {
                    txtAsume.Enabled = false;
                }
                
            }
            
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ahorrosServicio.CodigoPrograma, "Page_Load", ex);
        }
    }


    private void CargarListar()
    {
        ///carga y lista las variables a la entidad linea ahorro services 
        Xpinn.Ahorros.Services.ParametroGMFService linahorroServicio = new Xpinn.Ahorros.Services.ParametroGMFService();
        Xpinn.Ahorros.Entities.ParametroGMF linahorroVista = new Xpinn.Ahorros.Entities.ParametroGMF();
        ddloperacion.DataTextField = "descripcion";
        ddloperacion.DataValueField = "TIPO_OPE";
        ddloperacion.DataSource = linahorroServicio.combooperacion((Usuario)Session["usuario"]);
        ddloperacion.DataBind();

        Xpinn.Aportes.Services.TipoProductoServices linahorroServicios = new Xpinn.Aportes.Services.TipoProductoServices();
        Xpinn.Aportes.Entities.TipoProducto linahorroVistas = new Xpinn.Aportes.Entities.TipoProducto();
        ddlproductos.DataTextField = "nombre";
        ddlproductos.DataValueField = "COD_TIPO_PROD";
        ddlproductos.DataSource = linahorroServicios.ListarTipoProducto(linahorroVistas, (Usuario)Session["usuario"]);
        ddlproductos.DataBind();
        ddlproductos.Items.Insert(0, new ListItem("seleccione un item"));

        if (ddlproductos.SelectedValue == "1")
        {
            Xpinn.Aportes.Services.LineaAporteServices linea = new Xpinn.Aportes.Services.LineaAporteServices();
            Xpinn.Aportes.Entities.LineaAporte lineaproducto = new Xpinn.Aportes.Entities.LineaAporte();
            ddllinea.DataTextField = "NOMBRE";
            ddllinea.DataValueField = "COD_LINEA_APORTE";
            ddllinea.DataSource = linea.ListarLineaAporte(lineaproducto, (Usuario)Session["usuario"]);
            ddllinea.DataBind();
            ddllinea.Items.Insert(0, new ListItem("seleccione un item"));
        }
        if (ddlproductos.SelectedValue == "2")
        {
            Xpinn.FabricaCreditos.Services.LineasCreditoService linea = new Xpinn.FabricaCreditos.Services.LineasCreditoService();
            Xpinn.FabricaCreditos.Entities.LineasCredito lineaproducto = new Xpinn.FabricaCreditos.Entities.LineasCredito();
            ddllinea.DataTextField = "NOM_LINEA_CREDITO";
            ddllinea.DataValueField = "COD_LINEA_CREDITO";
            ddllinea.DataSource = linea.ListarLineasCredito(lineaproducto, (Usuario)Session["usuario"]);
            ddllinea.DataBind();
            ddllinea.Items.Insert(0, new ListItem("seleccione un item"));
        }
        if (ddlproductos.SelectedValue == "3")
        {
            Xpinn.Ahorros.Services.LineaAhorroServices linea = new Xpinn.Ahorros.Services.LineaAhorroServices();
            Xpinn.Ahorros.Entities.LineaAhorro lineaproducto = new Xpinn.Ahorros.Entities.LineaAhorro();
            ddllinea.DataTextField = "DESCRIPCION";
            ddllinea.DataValueField = "COD_LINEA_AHORRO";
            ddllinea.DataSource = linea.ListarLineaAhorro(lineaproducto, (Usuario)Session["usuario"]);
            ddllinea.DataBind();
            ddllinea.Items.Insert(0, new ListItem("seleccione un item"));
        }
        if (ddlproductos.SelectedValue == "4")
        {
            Xpinn.Servicios.Services.LineaServiciosServices linea = new Xpinn.Servicios.Services.LineaServiciosServices();
            Xpinn.Servicios.Entities.LineaServicios lineaproducto = new Xpinn.Servicios.Entities.LineaServicios();
            ddllinea.DataTextField = "NOMBRE";
            ddllinea.DataValueField = "COD_LINEA_SERVICIO";
            ddllinea.DataSource = linea.ListarLineaServicios(lineaproducto, (Usuario)Session["usuario"],"");
            ddllinea.DataBind();
            ddllinea.Items.Insert(0, new ListItem("seleccione un item"));
        }
        if (ddlproductos.SelectedValue == "5")
        {
            Xpinn.CDATS.Services.LineaCDATService linea = new Xpinn.CDATS.Services.LineaCDATService();
            Xpinn.CDATS.Entities.LineaCDAT lineaproducto = new Xpinn.CDATS.Entities.LineaCDAT();
            ddllinea.DataTextField = "DESCRIPCION";
            ddllinea.DataValueField = "COD_LINEACDAT";
            ddllinea.DataSource = linea.ListarLineaCDAT(lineaproducto, (Usuario)Session["usuario"]);
            ddllinea.DataBind();
            ddllinea.Items.Insert(0, new ListItem("seleccione un item"));
        }
        if (ddlproductos.SelectedValue == "9")
        {
            Xpinn.Programado.Services.LineasProgramadoServices linea = new Xpinn.Programado.Services.LineasProgramadoServices();
            Xpinn.Programado.Entities.LineasProgramado lineaproducto = new Xpinn.Programado.Entities.LineasProgramado();
            ddllinea.DataTextField = "NOMBRE";
            ddllinea.DataValueField = "COD_LINEA_PROGRAMADO";
            ddllinea.DataSource = linea.ListarLineasProgramado("", (Usuario)Session["usuario"]);
            ddllinea.DataBind();
            ddllinea.Items.Insert(0, new ListItem("seleccione un item"));
        }


        PoblarLista("TIPO_TRAN", Ddltipotran);

    }

    protected void tipo_producto_onselectedindexchanged(object sender, EventArgs e)
    { 
    
     if (ddlproductos.SelectedValue == "1")
        {
            Xpinn.Aportes.Services.LineaAporteServices linea = new Xpinn.Aportes.Services.LineaAporteServices();
            Xpinn.Aportes.Entities.LineaAporte lineaproducto = new Xpinn.Aportes.Entities.LineaAporte();
            ddllinea.DataTextField = "NOMBRE";
            ddllinea.DataValueField = "COD_LINEA_APORTE";
            ddllinea.DataSource = linea.ListarLineaAporte(lineaproducto, (Usuario)Session["usuario"]);
            ddllinea.DataBind();
        }
        if (ddlproductos.SelectedValue == "2")
        {
            Xpinn.FabricaCreditos.Services.LineasCreditoService linea = new Xpinn.FabricaCreditos.Services.LineasCreditoService();
            Xpinn.FabricaCreditos.Entities.LineasCredito lineaproducto = new Xpinn.FabricaCreditos.Entities.LineasCredito();
            ddllinea.DataTextField = "NOM_LINEA_CREDITO";
            ddllinea.DataValueField = "COD_LINEA_CREDITO";
            ddllinea.DataSource = linea.ListarLineasCredito(lineaproducto, (Usuario)Session["usuario"]);
            ddllinea.DataBind();
        }
        if (ddlproductos.SelectedValue == "3")
        {
            Xpinn.Ahorros.Services.LineaAhorroServices linea = new Xpinn.Ahorros.Services.LineaAhorroServices();
            Xpinn.Ahorros.Entities.LineaAhorro lineaproducto = new Xpinn.Ahorros.Entities.LineaAhorro();
            ddllinea.DataTextField = "DESCRIPCION";
            ddllinea.DataValueField = "COD_LINEA_AHORRO";
            ddllinea.DataSource = linea.ListarLineaAhorro(lineaproducto, (Usuario)Session["usuario"]);
            ddllinea.DataBind();
        }
        if (ddlproductos.SelectedValue == "4")
        {
            Xpinn.Servicios.Services.LineaServiciosServices linea = new Xpinn.Servicios.Services.LineaServiciosServices();
            Xpinn.Servicios.Entities.LineaServicios lineaproducto = new Xpinn.Servicios.Entities.LineaServicios();
            ddllinea.DataTextField = "NOMBRE";
            ddllinea.DataValueField = "COD_LINEA_SERVICIO";
            ddllinea.DataSource = linea.ListarLineaServicios(lineaproducto, (Usuario)Session["usuario"],"");
            ddllinea.DataBind();
        }
        if (ddlproductos.SelectedValue == "5")
        {
            Xpinn.CDATS.Services.LineaCDATService linea = new Xpinn.CDATS.Services.LineaCDATService();
            Xpinn.CDATS.Entities.LineaCDAT lineaproducto = new Xpinn.CDATS.Entities.LineaCDAT();
            ddllinea.DataTextField = "DESCRIPCION";
            ddllinea.DataValueField = "COD_LINEACDAT";
            ddllinea.DataSource = linea.ListarLineaCDAT(lineaproducto, (Usuario)Session["usuario"]);
            ddllinea.DataBind();
        }
        if (ddlproductos.SelectedValue == "9")
        {
            Xpinn.Programado.Services.LineasProgramadoServices linea = new Xpinn.Programado.Services.LineasProgramadoServices();
            Xpinn.Programado.Entities.LineasProgramado lineaproducto = new Xpinn.Programado.Entities.LineasProgramado();
            ddllinea.DataTextField = "NOMBRE";
            ddllinea.DataValueField = "COD_LINEA_PROGRAMADO";
            ddllinea.DataSource = linea.ListarLineasProgramado("", (Usuario)Session["usuario"]);
            ddllinea.DataBind();
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

    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        if (validardatos() == true)
        {
            ctlMensaje.MostrarMensaje("Desea guardar los datos del Cambio de Estado?");
        }
    }

    protected void asumecliente_oncheckedchange(object sender, EventArgs e)
    {
        txtAsume.Text = "";
        if (chkasume.Checked == true)
        {
            txtAsume.Text = "";
            if (txtAsume.Text == "")
            {
                txtAsume.Enabled = true;
                VerError("Debe dar un porcentaje a asumir");
                return;
            }
        
        }
    
    }

    protected Boolean validardatos() 
    {
        Boolean paso = true;
        if (chkasume.Checked == true)
        {
            if (txtAsume.Text == "")
            {
                txtAsume.Enabled = true;
                VerError("Debe dar el porcentaje a asumir");
                paso = false;
            }

        }

        if (chkasume.Checked == false)
        {

            if (txtAsume.Text != "")
            {
                txtAsume.Enabled = true;
                VerError("Debe Chequear el Asume Cliente");
                paso = false;
            }
        }

        
        if (ddloperacion.SelectedValue == "" )
        {
            VerError("Seleccione una operación");
            paso = false;
        }
        return paso;
    
    }

    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        try
        {
            ///carga todo a una entodad vAhorroVista en AhorroVista
            Usuario vUsuario = new Usuario();
            vUsuario = (Usuario)Session["Usuario"];

            Xpinn.Ahorros.Entities.ParametroGMF vAhorroVista = new Xpinn.Ahorros.Entities.ParametroGMF();

            if (ddloperacion.SelectedValue != "")
                vAhorroVista.tipo_ope = Convert.ToInt32(ddloperacion.SelectedValue);
            if (chkaceptaexcepcion.Checked != false)
                vAhorroVista.afecta_producto = Convert.ToInt32(chkaceptaexcepcion.Checked);
            if (chkmanejacuentas.Checked != false)
                vAhorroVista.maneja_exentas = Convert.ToInt32(chkmanejacuentas.Checked);

            if (Ddltipotran.SelectedValue != "")
               vAhorroVista.tipo_tran= Convert.ToInt32(Ddltipotran.SelectedValue);
            if (chkasume.Checked == true)
                vAhorroVista.asume=Convert.ToInt32(1);
            else
                vAhorroVista.asume=Convert.ToInt32(0);

            if (txtAsume.Text != "")
                vAhorroVista.porasume_cliente=Convert.ToDecimal(txtAsume.Text);
            if (chkEfectivo.Checked != false)
                vAhorroVista.pago_efectivo=Convert.ToInt32(chkEfectivo.Checked);

            if (ChechkChequeckBox2.Checked != false)
                vAhorroVista.pago_cheque=Convert.ToInt32(ChechkChequeckBox2.Checked) ;
            if (chkTraslado.Checked != false)
                vAhorroVista.pago_traslado=Convert.ToInt32(chkTraslado.Checked);

            if (ddlproductos.SelectedIndex != 0)
               vAhorroVista.tipo_producto= Convert.ToInt32(ddlproductos.SelectedValue);
            if (ddllinea.SelectedValue != "")
                vAhorroVista.cod_linea=ddllinea.SelectedValue;

            if (idObjeto != "")
                ahorrosServicio.ModificarParametroGMF(Convert.ToInt64(idObjeto), vAhorroVista, (Usuario)Session["usuario"]);
            else
                ahorrosServicio.CrearParametroGMF(vAhorroVista, (Usuario)Session["usuario"]);

            mvAhorroVista.ActiveViewIndex = 1;
            Site toolBar = (Site)this.Master;
            toolBar.MostrarGuardar(false);
            toolBar.MostrarCancelar(false);
            toolBar.MostrarConsultar(true);
            VerError("");
           
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ahorrosServicio.CodigoPrograma, "btnGuardar_Click", ex);
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
            Xpinn.Ahorros.Entities.ParametroGMF vAhorroVista = new Xpinn.Ahorros.Entities.ParametroGMF();
            vAhorroVista = ahorrosServicio.ConsultarParametroGMF(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);
            ///carga los datos de vAhorroVista
            ///
            if (vAhorroVista.tipo_ope != null)
                ddloperacion.SelectedValue = vAhorroVista.tipo_ope.ToString();
            if (vAhorroVista.afecta_producto != null)
                chkaceptaexcepcion.Checked = Convert.ToBoolean(vAhorroVista.afecta_producto);
            if (vAhorroVista.maneja_exentas != null)
            chkmanejacuentas.Checked = Convert.ToBoolean(vAhorroVista.maneja_exentas);
            if (vAhorroVista.tipo_tran != null)
            Ddltipotran.SelectedValue = vAhorroVista.tipo_tran.ToString();
            if (vAhorroVista.asume != null)
                chkasume.Checked = Convert.ToBoolean(vAhorroVista.asume);
            if (vAhorroVista.porasume_cliente != null)
                txtAsume.Text = vAhorroVista.porasume_cliente.ToString();
            if (vAhorroVista.pago_efectivo != null)
                chkEfectivo.Checked = Convert.ToBoolean(vAhorroVista.pago_efectivo);
            if (vAhorroVista.pago_cheque != null)
                ChechkChequeckBox2.Checked =Convert.ToBoolean(vAhorroVista.pago_cheque);
            if (vAhorroVista.pago_traslado != null)
                chkTraslado.Checked = Convert.ToBoolean(vAhorroVista.pago_traslado);
            if (vAhorroVista.tipo_producto != null)
            ddlproductos.SelectedValue = vAhorroVista.tipo_producto.ToString();
            if (vAhorroVista.cod_linea != null)
            ddllinea.SelectedValue = vAhorroVista.cod_linea.ToString();


            
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ahorrosServicio.CodigoPrograma, "ObtenerDatos", ex);
        }
    }
    private void CargarListas()
    {
        ///carga las listas a la session
        Usuario pUsuario = new Usuario();
        pUsuario = (Usuario)Session["Usuario"];
        try
        {
            //ddlProveedor.DataTextField = "nombre";
            //ddlProveedor.DataValueField = "cod_persona";
            //ddlProveedor.DataSource = personaServicio.ListadoPersonas1(persona, pUsuario);
            //ddlProveedor.DataBind();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ahorrosServicio.GetType().Name + "L", "CargarListas", ex);
        }
    }

    #region Titulares

    /// <summary>
    /// Método para instar un detalle en blanco para cuando la grilla no tiene datos
    /// </summary>
    /// <param name="consecutivo"></param>
   

    /// <summary>
    /// Método para cambio de página
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    

    protected void ActualizarDetalle()
    {
       // List<DetalleComprobante> LstDetalleComprobante = new List<DetalleComprobante>();
        // LstDetalleComprobante = (List<DetalleComprobante>)Session["DetalleComprobante"];
        //gvDetMovs.DataSource = LstDetalleComprobante;
        //gvDetMovs.DataBind();
    }



    /// <summary>
    /// Método para borrar un registro de la grilla
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    
    
       
    #endregion

    protected void ddlFormaPago_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

}
