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

    CambioMonedaServices CAMBIOSERVICE = new CambioMonedaServices();
    
    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            if (Session[CAMBIOSERVICE.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(CAMBIOSERVICE.CodigoPrograma, "E");
            else
                VisualizarOpciones(CAMBIOSERVICE.CodigoPrograma, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoRegresar += btnRegresar_Click;
            ctlMensaje.eventoClick += btnContinuarMen_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CAMBIOSERVICE.GetType().Name + "L", "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                mvAplicar.ActiveViewIndex = 0;
                txtCodigo.Enabled = false;
                CargarDropDown();
                txtFecha.Text = DateTime.Now.ToShortDateString();

                if (Session[CAMBIOSERVICE.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[CAMBIOSERVICE.CodigoPrograma + ".id"].ToString();
                    Session.Remove(CAMBIOSERVICE.CodigoPrograma + ".id");
                    ObtenerDatos(idObjeto);
                }              
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CAMBIOSERVICE.CodigoPrograma + "L", "Page_Load", ex);
        }
    }
    


    void CargarDropDown()
    {
        PoblarLista("TIPOMONEDA", ddlOrigen);
        PoblarLista("TIPOMONEDA", ddlDestino);
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


    protected void ObtenerDatos(string pidObjeto)
    {
        try
        {
            CambioMoneda vDetalle = new CambioMoneda();
            vDetalle = CAMBIOSERVICE.ConsultarCambioMoneda(Convert.ToInt64(pidObjeto), (Usuario)Session["usuario"]);

            if (vDetalle.idcambiomoneda != 0 && vDetalle.idcambiomoneda != null)
                txtCodigo.Text = vDetalle.idcambiomoneda.ToString();            

            if (vDetalle.cod_moneda_ini != 0 && vDetalle.cod_moneda_ini != null)
                ddlOrigen.SelectedValue = vDetalle.cod_moneda_ini.ToString().Trim();

            if (vDetalle.cod_moneda_fin != 0 && vDetalle.cod_moneda_fin != null)
                ddlDestino.SelectedValue = vDetalle.cod_moneda_fin.ToString().Trim();
            
            if (vDetalle.fecha != DateTime.MinValue && vDetalle.fecha != null)
                txtFecha.Text = vDetalle.fecha.ToShortDateString();

            if (vDetalle.valor_venta != 0 && vDetalle.valor_venta != null)
                txtValorVenta.Text = vDetalle.valor_venta.ToString();

            if (vDetalle.valor_compra != 0 && vDetalle.valor_compra != null)
                txtValorCompra.Text = vDetalle.valor_compra.ToString();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CAMBIOSERVICE.CodigoPrograma, "ObtenerDatos", ex);
        }
    }



    public Boolean ValidarDatos()
    {
        if (ddlOrigen.SelectedIndex == 0)
        {
            VerError("Seleccione la moneda de Origen");
            return false;
        }
        if (ddlDestino.SelectedIndex == 0)
        {
            VerError("Seleccione la moneda de destino");
            return false;
        }
        if (txtFecha.Text == "")
        {
            VerError("Ingrese la Fecha");
            return false;
        }
        if (txtValorVenta.Text == "")
        {
            VerError("Ingrese el valor de venta");
            return false;
        }
        if (txtValorVenta.Text == "")
        {
            VerError("Ingrese el valor de compra");
            return false;
        }        
        return true;
    }

    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        if (ValidarDatos())
        {            
            string msj = idObjeto != "" ? "modificar" : "registrar";
             ctlMensaje.MostrarMensaje("Desea " + msj + " los Datos Ingresados?");
        }
    }



    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        try
        {
            CambioMoneda vDatos = new CambioMoneda();
            
            if (idObjeto != "")
                vDatos.idcambiomoneda = Convert.ToInt32(txtCodigo.Text);
            else
                vDatos.idcambiomoneda = 0;
            Usuario pUsu = (Usuario)Session["usuario"];

            vDatos.cod_moneda_ini = Convert.ToInt32(ddlOrigen.SelectedValue);
            vDatos.cod_moneda_fin = Convert.ToInt32(ddlDestino.SelectedValue);
            vDatos.fecha = Convert.ToDateTime(txtFecha.Text);
            vDatos.valor_venta = Convert.ToDecimal(txtValorVenta.Text);
            vDatos.valor_compra = Convert.ToDecimal(txtValorCompra.Text);

            if (idObjeto != "")
            {
                //MODIFICAR
                CAMBIOSERVICE.ModificarCambioMoneda(vDatos, (Usuario)Session["usuario"]);
            }
            else
            {
                //CREAR
                CAMBIOSERVICE.CrearCambioMoneda(vDatos, (Usuario)Session["usuario"]);
            }

            lblMsj.Text = idObjeto != "" ? "Se Modificaron correctamente los datos" : "Se Grabaron Correctamente los datos ingresados.";
            Site toolBar = (Site)Master;
            toolBar.MostrarGuardar(false);
            mvAplicar.ActiveViewIndex = 1;            
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CAMBIOSERVICE.CodigoPrograma, "btnContinuar_Click", ex);
        }        
    }

    protected void btnRegresar_Click(object sender, ImageClickEventArgs e)
    {
        Navegar(Pagina.Lista);
    }

    
}
