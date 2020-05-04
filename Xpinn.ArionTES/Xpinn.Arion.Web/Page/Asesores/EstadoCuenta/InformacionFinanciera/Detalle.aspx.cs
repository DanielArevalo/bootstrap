using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Asesores.Services;
using Xpinn.Asesores.Entities;
using Xpinn.Asesores.Entities.Common;
using Xpinn.Util;
using System.Configuration;
//using Xpinn.Util;
//using System.Configuration;
using System.Globalization;
public partial class Page_Asesores_EstadoCuenta_InformacionFinanciera_Default : GlobalWeb
{
    EstadoCuentaService serviceEstadoCuenta = new EstadoCuentaService();
    InformacionFinancieraService informacion = new InformacionFinancieraService();
    Producto producto;

    protected void ucImprimir_Click(object sender, ImageClickEventArgs evt)
    {
        Session.Remove("imprimirCtrl");
        Session["imprimirCtrl"] = contentTable;
        ClientScript.RegisterStartupScript(this.GetType(), "onclick", Imprimir.JSCRIPT_PRINT);
       
    }
    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(informacion.CodigoPrograma, "L");

            Site toolBar = (Site)this.Master;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(informacion.GetType().Name + "L", "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        
            if (!IsPostBack)
            {

                txtIngresos.Style["text-align"] = "right";
                txtDisponible.Style["text-align"] = "right";
                txtEgresos.Style["text-align"] = "right";
                txtactivos.Style["text-align"] = "right";
                txtPasivos.Style["text-align"] = "right";
                txtPatrimonio.Style["text-align"] = "right";
                ObtenerValores();
            }
       
       
       
    }
    protected void ObtenerValores()
    {
        
       
            producto = (Producto)(Session[MOV_GRAL_CRED_PRODUC]);

            
        
            
            if (!string.IsNullOrEmpty(producto.Persona.PrimerApellido)) txtNombre.Text = producto.Persona.PrimerNombre.Trim().ToString()+" "+ producto.Persona.PrimerApellido.Trim().ToString();
            if (!producto.Persona.NumeroDocumento.Equals(0)) txtNumDoc.Text = producto.Persona.NumeroDocumento.ToString();
            txtTipoIdentificacion.Text = producto.Persona.TipoIdentificacion.NombreTipoIdentificacion.Trim().ToString();
            if (!string.IsNullOrEmpty(producto.Persona.Direccion)) txtactivos.Text =Convert.ToString(producto.SaldoCapital + producto.OtrosSaldos);

            InformacionFinanciera datos = new InformacionFinanciera();
            datos = informacion.ListarInformacionFinanciera(Convert.ToInt32(producto.Persona.NumeroDocumento), (Usuario)Session["Usuario"]);
            txtIngresos.Text = datos.totingresos.ToString();
            txtDisponible.Text = datos.totdisponible.ToString();
            txtEgresos.Text = datos.totegresos.ToString();
            txtactivos.Text = datos.totactivos.ToString();
            txtPasivos.Text = datos.totpasivo.ToString();
            txtPatrimonio.Text = datos.totpatrimonio.ToString();

           
        

      
    }

    protected void imgBtnVolverHandler(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/Page/Asesores/EstadoCuenta/Detalle.aspx");
    }


    protected void txtAntiguedad_TextChanged(object sender, EventArgs e)
    {

    }
}