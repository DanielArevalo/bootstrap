using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Transferencia : GlobalWeb
{
    xpinnWSIntegracion.WSintegracionSoapClient wsIntegra = new xpinnWSIntegracion.WSintegracionSoapClient();
    xpinnWSIntegracion.Monedero monedero = new xpinnWSIntegracion.Monedero();
    xpinnWSLogin.Persona1 Data = new xpinnWSLogin.Persona1();
    #region metodos iniciales 
    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            ValidarSession();
            VisualizarTitulo(OptionsUrl.Monedero, "Monedero");
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(OptionsUrl.Monedero, "Monedero", ex);
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Data = (xpinnWSLogin.Persona1)Session["persona"];

            if (Data.cod_persona != 0 && Data.identificacion != "" && Data.identificacion != null)
            {
                CargarDatos(Data.cod_persona);
            }
        }
    }
    private void CargarDatos(long cod_persona)
    {
        try
        {                 

            monedero = wsIntegra.consultarMonedero(Convert.ToInt32(cod_persona), Session["sec"].ToString());
            if(monedero != null && monedero.id_monedero > 0)
            {
                txtNombre.Text = Data.nombre;
                txtId.Text = monedero.id_monedero.ToString();
                txtSaldo.Text = monedero.saldo.ToString("C0");
            }
        }
        catch(Exception ex)
        {
            VerError("Parece que algo salió mal, intentalo más tarde.");
        }

    }
    #endregion    

    #region CargarCuenta
   

    protected void btnCargar_Click(object sender, EventArgs e)
    {
        try
        {
            Data = (xpinnWSLogin.Persona1)Session["persona"];
            //INTENRTA HACER LA TRANSFERENCIA
            xpinnWSIntegracion.TranMonedero tran = new xpinnWSIntegracion.TranMonedero()
            {
                num_tran = 0,
                cod_persona = Data.cod_persona,
                id_monedero = Convert.ToInt32(txtId.Text),
                tipo_tran = 2,
                valor = Convert.ToDecimal(txtValorCarga.Text.Replace("$", "").Replace(",", "").Replace(".", "")),
                estado = 1,
                fecha = DateTime.Now,
                descripcion = txtNombre.Text,
                id_operacion = 2, //transferencias
                cod_tipo_producto = 11,
                referencia = txtCodDestino.Text
            };
            tran = wsIntegra.guardarTransaccionMonedero(tran, Session["sec"].ToString());

            if(tran != null && tran.num_tran > 0)
            {
                panelFinal.Visible = true;
                pnlTran.Visible = false;
            }
            else
            {
                lblError.Text = "Se presentó un problema al realizar la transacción, intentalo más tarde.";
            }            
        }
        catch (Exception ex)
        {
            VerError("Parece que algo salió mal, intentalo más tarde.");
        }
    }

    protected void btnVolverMon_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Pages/Monedero/Monedero.aspx");
    }

    protected void btnInicio_Click(object sender, EventArgs e)
    {
        Navegar("~/Pages/Monedero/Monedero.aspx");
    }

    protected void txtIddestino_TextChanged(object sender, EventArgs e)
    {
        txtNomDestino.Text = "";
        txtCodDestino.Text = "";
        if (!string.IsNullOrEmpty(txtIddestino.Text.Trim()))
        {
            xpinnWSIntegracion.PersonaMonedero destino = wsIntegra.consultarPersonaMonedero(txtIddestino.Text.Trim(), Session["sec"].ToString());
            if(destino != null && destino.cod_persona > 0)
            {
                if (destino.id_monedero > 0)
                {
                    if(destino.estado == 1)
                    {
                        txtNomDestino.Text = destino.nombre;
                        txtCodDestino.Text = destino.id_monedero.ToString();                        
                    }
                    else
                        lblError.Text="La persona a la que quieres enviar dinero no tiene activo el monedero";
                }
                else
                    lblError.Text = "La persona a la que quieres enviar dinero aún no usa monedero";
            }
            else
                lblError.Text = "La persona a la que quieres enviar dinero no está registrada";
        }
        validar();
    }

    private bool validar()
    {
        if(!string.IsNullOrEmpty(txtValorCarga.Text.Replace("$", "").Replace(",", "").Replace(".", "")))
        {
            decimal valor = Convert.ToDecimal(txtValorCarga.Text.Replace("$", "").Replace(",", "").Replace(".", ""));
            if(valor > 0 && !string.IsNullOrEmpty(txtCodDestino.Text) && string.IsNullOrEmpty(lblError.Text))
            {
                btnCargar.Enabled = true;
                return true;
            }            
        }
        btnCargar.Enabled = false;
        return false;
    }

    protected void txtValorCarga_TextChanged(object sender, EventArgs e)
    {
        lblError.Text = "";
        string valor = txtValorCarga.Text.Replace("$", "").Replace(",", "").Replace(".", "");
        if(!string.IsNullOrEmpty(valor))
        {
            decimal disponible = Convert.ToDecimal(txtSaldo.Text.Replace("$", "").Replace(",", "").Replace(".", ""));
            decimal valor_tran = Convert.ToDecimal(valor);
            if(valor_tran > disponible)
            {
                lblError.Text = "La cantidad ingresada supera tu saldo disponible";
            }
        }
        validar();
    }

    #endregion    
}