using System;
using System.Web.UI;
using Xpinn.Util;
using Xpinn.Tesoreria.Services;
using Xpinn.Tesoreria.Entities;


public partial class Nuevo : GlobalWeb
{

    ConvenioRecaudoService ConvenioServices = new ConvenioRecaudoService();
    PoblarListas poblar = new PoblarListas();
    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(ConvenioServices.CodigoPrograma, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
            ctlMensaje.eventoClick += btnContinuarMen_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ConvenioServices.GetType().Name + "L", "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Fecha_convenio.Text = DateTime.Now.ToString();

            if (!IsPostBack)
            {
                CargarDropdown();
                mvAplicar.ActiveViewIndex = 0;


                if (Session[ConvenioServices.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[ConvenioServices.CodigoPrograma + ".id"].ToString();
                    Session.Remove(ConvenioServices.CodigoPrograma + ".id");
                    ObtenerDatos(idObjeto);

                    Session["TEXTO"] = "modificar";
                    lblmsj.Text = "modificada";
                }
                else
                {
                    chkCuentaPropia.Checked = true;
                    Session["TEXTO"] = "grabar";
                    lblmsj.Text = "grabada";
                }
                Xpinn.Aportes.Entities.TipoProducto tipoprodu = new Xpinn.Aportes.Entities.TipoProducto();
                tipoprodu.cod_tipo_prod = Convert.ToInt32(ddlTipo_producto.SelectedValue);
                CargarTipoTran(tipoprodu);
                if (Session["tipo_tran"] != null)
                    ddlTipo_tran.SelectedValue = Session["tipo_tran"].ToString();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ConvenioServices.GetType().Name + "L", "Page_Load", ex);
        }

    }

    private void CargarDropdown()
    {
        //TIPO PPRODUCTO
        Xpinn.Aportes.Entities.TipoProducto tipoprodu = new Xpinn.Aportes.Entities.TipoProducto();
        Xpinn.Aportes.Services.TipoProductoServices tipoproducservice = new Xpinn.Aportes.Services.TipoProductoServices();
        ddlTipo_producto.DataSource = tipoproducservice.ListarTipoProducto(tipoprodu, Usuario);
        ddlTipo_producto.DataTextField = "nombre";
        ddlTipo_producto.DataValueField = "cod_tipo_prod";
        ddlTipo_producto.DataBind();

        poblar.PoblarListaDesplegable("CONVENIO_RECAUDO_NATURALEZA", ddlNaturalezaConvenio, Usuario);
    }
    private void CargarTipoTran(Xpinn.Aportes.Entities.TipoProducto tipoprodu)
    {
        //TIPO PPRODUCTO

        Xpinn.Aportes.Services.TipoProductoServices tipoproducservice = new Xpinn.Aportes.Services.TipoProductoServices();
        ddlTipo_tran.DataSource = tipoproducservice.ListarTipoTran(tipoprodu, Usuario);
        ddlTipo_tran.DataTextField = "Descripcion";
        ddlTipo_tran.DataValueField = "tipo_tran";
        ddlTipo_tran.DataBind();
    }
    private void CargarNumeroProducto(Int64 codpersona, Xpinn.Aportes.Entities.TipoProducto tipoprodu)
    {
        //TIPO Numero_Producto

        ddl_NumeroProducto.DataSource = ConvenioServices.ConsultarNumeroProducto(codpersona, tipoprodu.cod_tipo_prod, Usuario);
        ddl_NumeroProducto.DataTextField = "num_producto";
        ddl_NumeroProducto.DataValueField = "num_producto";
        ddl_NumeroProducto.DataBind();
    }

    protected void btnConsultaPersonas_Click(object sender, EventArgs e)
    {
        ctlBusquedaPersonas.Motrar(true, "txtCodPersona", "txtIdPersona", "txtNomPersona");
    }


    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            ConvenioRecaudo vDetalle = new ConvenioRecaudo();
            vDetalle = ConvenioServices.ConsultarConvenio(pIdObjeto, (Usuario)Session["usuario"]);
            if (vDetalle.fecha_convenio != null) Fecha_convenio.Text = Convert.ToString(vDetalle.fecha_convenio);
            if (vDetalle.cod_convenio != null) txtCod_convenio.Text = vDetalle.cod_convenio.ToString();
            if (vDetalle.nombre_convenio != "") txt_nom_convenio.Text = vDetalle.nombre_convenio;
            if (vDetalle.EAN  != "") txtEAN.Text = vDetalle.EAN ;
            //falta lo de la persona
            if (vDetalle.cod_persona != 0) txtCodPersona.Text = vDetalle.cod_persona.ToString();
            actualizarpersona();

            if (vDetalle.tipo_producto != null) ddlTipo_producto.SelectedValue = Convert.ToString(vDetalle.tipo_producto);
            Xpinn.Aportes.Entities.TipoProducto tipoprodu = new Xpinn.Aportes.Entities.TipoProducto();
            tipoprodu.cod_tipo_prod = Convert.ToInt32(ddlTipo_producto.SelectedValue);
            CargarNumeroProducto(txtCodPersona.Text == "" ? 0 : Int64.Parse(txtCodPersona.Text), tipoprodu);
            if (vDetalle.num_producto != "") ddl_NumeroProducto.SelectedValue = vDetalle.num_producto;
            if (vDetalle.tipo_tran != null)
            {
                Session["tipo_tran"] = vDetalle.tipo_tran;

            }
            chkCuentaPropia.Checked = vDetalle.cuenta_propia == 0 ? false : true;
            chkContratoFirmado.Checked = vDetalle.contrato_firmado == 0 ? false : true;
            if (vDetalle.naturaleza_convenio != 0)
                ddlNaturalezaConvenio.SelectedValue = vDetalle.naturaleza_convenio.ToString();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ConvenioServices.CodigoPrograma, "ObtenerDatos", ex);
        }
    }


    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }

    public Boolean ValidarDatos()
    {
        if (string.IsNullOrEmpty(txtCodPersona.Text))
        {
            VerError("No se ingreso identificacion del mandante.");
            return false;
        }
        if (ddlNaturalezaConvenio.SelectedItem == null)
        {
            VerError("No se encontró registros de naturaleza del convenio");
            return false;
        }
        if (ddlNaturalezaConvenio.SelectedIndex == 0)
        {
            VerError("Seleccione la naturaleza del convenio");
            return false;
        }
        return true;
    }

    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        if (ValidarDatos())
        {
            ctlMensaje.MostrarMensaje("Desea " + Session["TEXTO"].ToString() + " los Datos Ingresados?");
        }
    }


    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        try
        {
            Usuario pUsu = (Usuario)Session["usuario"];
            ConvenioRecaudo pVar = new ConvenioRecaudo();
            pVar.cod_convenio = txtCod_convenio.Text == "" ? 0 : Int64.Parse(txtCod_convenio.Text.Replace(".", ""));
            pVar.nombre_convenio = txt_nom_convenio.Text;
            pVar.fecha_convenio = Convert.ToDateTime(Fecha_convenio.Text);
            pVar.cod_persona = Convert.ToInt64(txtCodPersona.Text);
            pVar.tipo_producto = Convert.ToInt64(ddlTipo_producto.SelectedValue);
            pVar.num_producto = ddl_NumeroProducto.SelectedValue;
            pVar.tipo_tran = Convert.ToInt64(ddlTipo_tran.SelectedValue);
            pVar.cuenta_propia = chkCuentaPropia.Checked ? 1 : 0;
            pVar.contrato_firmado = chkContratoFirmado.Checked ? 1 : 0;
            pVar.naturaleza_convenio = Convert.ToInt32(ddlNaturalezaConvenio.SelectedValue);
            pVar.EAN  = Convert.ToString(txtEAN.Text);
            if (idObjeto != "")
            {
                //MODIFICAR
                ConvenioServices.Cre_Mod_Convenio(pVar, 2, pUsu);
            }
            else
            {
                //CREAR
                ConvenioServices.Cre_Mod_Convenio(pVar, 1, pUsu);
            }
            Session[ConvenioServices.CodigoPrograma + ".id"] = idObjeto;
            mvAplicar.ActiveViewIndex = 1;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ConvenioServices.CodigoPrograma, "btnContinuar_Click ", ex);
        }
    }



    protected void txtIdPersona_TextChanged(object sender, EventArgs e)
    {
        actualizarpersona();

    }
    public void actualizarpersona()
    {
        Xpinn.Seguridad.Services.UsuarioService UsuarioServicio = new Xpinn.Seguridad.Services.UsuarioService();
        Xpinn.FabricaCreditos.Services.Persona1Service PersonaServicio = new Xpinn.FabricaCreditos.Services.Persona1Service();
        Xpinn.FabricaCreditos.Entities.Persona1 DatosPersona = new Xpinn.FabricaCreditos.Entities.Persona1();

        string identificacion = PersonaServicio.ConsultarIdentificacionPersona(Convert.ToInt64(txtCodPersona.Text), (Usuario)Session["usuario"]);
        DatosPersona = UsuarioServicio.ConsultarPersona1(identificacion, (Usuario)Session["usuario"]);

        if (DatosPersona.cod_persona != 0)
        {
            if (DatosPersona.cod_persona != 0)
                txtCodPersona.Text = DatosPersona.cod_persona.ToString();
            if (DatosPersona.identificacion != "")
                txtIdPersona.Text = DatosPersona.identificacion;
            if (DatosPersona.nombre != "")
                txtNomPersona.Text = DatosPersona.nombre;
        }
        else
        {
            txtNomPersona.Text = "";
            txtCodPersona.Text = "";
        }
    }
    protected void ddlTipo_producto_SelectedIndexChanged(object sender, EventArgs e)
    {
        Xpinn.Aportes.Entities.TipoProducto tipoprodu = new Xpinn.Aportes.Entities.TipoProducto();
        tipoprodu.cod_tipo_prod = Convert.ToInt32(ddlTipo_producto.SelectedValue);
        CargarTipoTran(tipoprodu);
        CargarNumeroProducto(txtCodPersona.Text == "" ? 0 : Int64.Parse(txtCodPersona.Text), tipoprodu);
    }

}
