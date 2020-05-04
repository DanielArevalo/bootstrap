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
using Xpinn.Tesoreria.Services;
using Xpinn.Tesoreria.Entities;


public partial class Nuevo : GlobalWeb
{

    DevolucionServices DevolServices = new DevolucionServices();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(DevolServices.CodigoPrograma, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
            ctlMensaje.eventoClick += btnContinuarMen_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(DevolServices.GetType().Name + "L", "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                CargarDropdown();
                mvAplicar.ActiveViewIndex = 0;
                txtNumero.Enabled = false;
                txtSaldo.Enabled = false;

                if (Session[DevolServices.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[DevolServices.CodigoPrograma + ".id"].ToString();
                    Session.Remove(DevolServices.CodigoPrograma + ".id");
                    ObtenerDatos(idObjeto);
                   
                    Session["TEXTO"] = "modificar";
                    lblmsj.Text = "modificada";
                }
                else
                {
                    txtIdDetalle.Enabled = false;
                    txtNumRecaudo.Enabled = false;
                    Session["TEXTO"] = "grabar";
                    lblmsj.Text = "grabada";
                    txtNumero.Text = DevolServices.ObtenerSiguienteCodigo((Usuario)Session["usuario"]).ToString();
                    ddlEstado.SelectedValue = "1";
                    ddlEstado.Enabled = false;
                }
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(DevolServices.GetType().Name + "L", "Page_Load", ex);
        }

    }

    private void CargarDropdown()
    {       
        ddlEstado.Items.Insert(0,new ListItem("Seleccione un item","0"));
        ddlEstado.Items.Insert(1, new ListItem("PENDIENTE", "1"));
        ddlEstado.Items.Insert(2, new ListItem("PAGADA", "2"));
        ddlEstado.Items.Insert(3, new ListItem("ANULADA", "3"));
        ddlEstado.SelectedIndex = 0;
        ddlEstado.DataBind();
    }

    protected void btnConsultaPersonas_Click(object sender, EventArgs e)
    {
        ctlBusquedaPersonas.Motrar(true, "txtCodPersona", "txtIdPersona", "txtNomPersona");
    }
    

    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            Devolucion vDetalle = new Devolucion();

            vDetalle = DevolServices.ConsultarDevolucion(Convert.ToInt32(pIdObjeto), (Usuario)Session["usuario"]);

            if (vDetalle.num_devolucion != 0)
                txtNumero.Text = vDetalle.num_devolucion.ToString().Trim();
            if (vDetalle.concepto != null)
                if(vDetalle.concepto != "")
                    txtConcepto.Text = vDetalle.concepto.ToString().Trim();
            if (vDetalle.cod_persona != 0)
            {
                txtCodPersona.Text = vDetalle.cod_persona.ToString().Trim();
                txtIdPersona.Text = vDetalle.identificacion.ToString().Trim();
                txtNomPersona.Text = vDetalle.nombre.ToString().Trim();
            }
            
            if (vDetalle.fecha_devolucion != DateTime.MinValue)
                txtFechaDev.Text = vDetalle.fecha_devolucion.ToShortDateString();
            if (vDetalle.valor != 0)
                txtValor.Text = vDetalle.valor.ToString();

            txtSaldo.Text = vDetalle.saldo.ToString();

            if (vDetalle.origen != null)
                if (vDetalle.origen != "")
                    txtOrigen.Text = vDetalle.origen.ToString();
            if (vDetalle.estado != "")
                ddlEstado.SelectedValue = vDetalle.estado;
            if (vDetalle.fecha_descuento != DateTime.MinValue && vDetalle.fecha_descuento != null)
                txtFecDescuento.Text = vDetalle.fecha_descuento.Value.ToShortDateString();
            if (vDetalle.num_recaudo != null)
                if (vDetalle.num_recaudo != 0)
                    txtNumRecaudo.Text = vDetalle.num_recaudo.ToString();
            if (vDetalle.iddetalle != 0)
            {
                txtIdDetalle.Text = vDetalle.iddetalle.ToString();
                txtIdDetalle_TextChanged(txtIdDetalle, null);
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(DevolServices.CodigoPrograma, "ObtenerDatos", ex);
        }
    }


    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }

    public Boolean ValidarDatos()
    {
        
        if (txtConcepto.Text == "")
        {
            VerError("Ingrese concepto");
            return false;
        }
        if (txtCodPersona.Text == "")
        {
            VerError("Ingrese los Datos de la Persona");
            return false;
        }
        if (txtFechaDev.Text == "")
        {
            VerError("Seleccione la fecha de Devolución");
            return false;
        }
        if (txtValor.Text == "0")
        {
            VerError("Ingrese el Valor");
            return false;
        }
        if (ddlEstado.SelectedIndex == 0)
        {
            VerError("Seleccione el Estado");
            return false;
        }

        // Validar que exista la parametrización contable por procesos
        if (ValidarProcesoContable(Convert.ToDateTime(txtFechaDev.Text), 105) == false)
        {
            VerError("No se encontró parametrización contable por procesos para el tipo de operación 105=Creación de Devoluciones");
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
            Devolucion pVar = new Devolucion();
            if (txtNumero.Text != "")
                pVar.num_devolucion = Convert.ToInt32(txtNumero.Text);
            else
                pVar.num_devolucion = 0;
            pVar.concepto = txtConcepto.Text.ToUpper();
            pVar.cod_persona = Convert.ToInt64(txtCodPersona.Text);
            pVar.identificacion = txtIdPersona.Text;

            pVar.fecha_devolucion = Convert.ToDateTime(txtFechaDev.Text);
            pVar.valor = Convert.ToDecimal(txtValor.Text);

            if (idObjeto != "")
                pVar.saldo = Convert.ToDecimal(txtSaldo.Text);
            else
                pVar.saldo = pVar.valor;

            if (txtOrigen.Text != "")
                pVar.origen = txtOrigen.Text;
            else
                pVar.origen = null;
            pVar.estado = ddlEstado.SelectedValue;

            if (txtFecDescuento.Text != "")
                pVar.fecha_descuento = Convert.ToDateTime(txtFecDescuento.Text);
            else
                pVar.fecha_descuento = DateTime.MinValue;

            if (txtNumRecaudo.Text != "")
                pVar.num_recaudo = Convert.ToInt64(txtNumRecaudo.Text);
            else
                pVar.num_recaudo = 0;

            if (txtIdDetalle.Text != "")
                pVar.iddetalle = Convert.ToInt64(txtIdDetalle.Text);
            else
                pVar.iddetalle = 0;

          
            if (idObjeto != "")
            {
                //MODIFICAR
                DevolServices.Crear_Mod_Devolucion(pVar, pUsu, 2);
            }
            else
            {
                if (idObjeto != "")
                    txtNumero.Text = DevolServices.ObtenerSiguienteCodigo((Usuario)Session["usuario"]).ToString();
                //CREAR
                DevolServices.Crear_Mod_Devolucion(pVar, pUsu, 1);
            }
            Session[DevolServices.CodigoPrograma + ".id"] = idObjeto;
            mvAplicar.ActiveViewIndex = 1;
            
            // Generar el comprobante
            Xpinn.Contabilidad.Services.ComprobanteService ComprobanteServicio = new Xpinn.Contabilidad.Services.ComprobanteService();
            Session[ComprobanteServicio.CodigoPrograma + ".cod_ope"] = pVar.cod_ope;
            Session[ComprobanteServicio.CodigoPrograma + ".tipo_ope"] = 105;
            Session[ComprobanteServicio.CodigoPrograma + ".fecha_ope"] = pVar.fecha_devolucion;
            Session[ComprobanteServicio.CodigoPrograma + ".cod_ofi_ope"] = pUsu.cod_oficina;
            Session[ComprobanteServicio.CodigoPrograma + ".cod_persona"] = pVar.cod_persona;
            Navegar("../../Contabilidad/Comprobante/Nuevo.aspx");
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(DevolServices.CodigoPrograma, "btnContinuar_Click", ex);
        }        
    }

   
   
    protected void txtIdPersona_TextChanged(object sender, EventArgs e)
    {

        Xpinn.Seguridad.Services.UsuarioService UsuarioServicio = new Xpinn.Seguridad.Services.UsuarioService();
        Xpinn.FabricaCreditos.Entities.Persona1 DatosPersona = new Xpinn.FabricaCreditos.Entities.Persona1();

        DatosPersona = UsuarioServicio.ConsultarPersona1(txtIdPersona.Text, (Usuario)Session["usuario"]);

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



    protected void txtIdDetalle_TextChanged(object sender, EventArgs e)
    {
        if (txtIdDetalle.Text != "")
        {
            Devolucion pEnt = new Devolucion();
            pEnt = DevolServices.ConsultarDetalleRecaudo(Convert.ToInt32(txtIdDetalle.Text), (Usuario)Session["usuario"]);

            if (pEnt.numero_recaudo != 0)
            {

                if (pEnt.numero_recaudo != 0)
                    txtNumRecaudo.Text = pEnt.numero_recaudo.ToString();
                if (pEnt.detalle != null)
                    txtDetalleRec.Text = pEnt.detalle;
            }
            else
            {
                txtDetalleRec.Text = "";
                txtNumRecaudo.Text = "";
            }
        }
    }
}
