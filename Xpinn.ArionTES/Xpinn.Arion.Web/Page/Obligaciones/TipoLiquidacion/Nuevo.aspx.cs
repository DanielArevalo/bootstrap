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

public partial class Nuevo : GlobalWeb
{
    Xpinn.Obligaciones.Services.TipoLiquidacionService tipoLiqObService = new Xpinn.Obligaciones.Services.TipoLiquidacionService();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[tipoLiqObService.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(tipoLiqObService.CodigoPrograma, "E");
            else
                VisualizarOpciones(tipoLiqObService.CodigoPrograma, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoRegresar += btnRegresar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(tipoLiqObService.GetType().Name + "A", "Page_PreInit", ex);
        }
    }



    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                //se inicializa el combo de ciudades, centro de costos                
                ArmarComboTipoAmortizacion(ddlTipoCuota);
                if (Session[tipoLiqObService.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[tipoLiqObService.CodigoPrograma + ".id"].ToString();
                    Session.Remove(tipoLiqObService.CodigoPrograma + ".id");
                    ObtenerDatos(idObjeto);
                }
                else
                    ObtenerDatos("");
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(tipoLiqObService.GetType().Name + "A", "Page_Load", ex);
        }
    }

    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Xpinn.Obligaciones.Entities.TipoLiquidacion tipoliq = new Xpinn.Obligaciones.Entities.TipoLiquidacion();
            if (idObjeto != "")
                tipoliq = tipoLiqObService.ConsultarTipoLiq(long.Parse(idObjeto), (Usuario)Session["usuario"]);

            //se atrapan los datos del formulario
            tipoliq.codtipoliquidacion = txtCodigo.Text == "" ? 0 : long.Parse(txtCodigo.Text);
            tipoliq.descripcion = txtTipLiq.Text.Trim();
            tipoliq.tipocuota = Convert.ToInt64(ddlTipoCuota.SelectedValue);
            tipoliq.tipoamortizacion = Convert.ToInt64(ddlTipoAmortizacion.SelectedValue);
            tipoliq.tipointeres = Convert.ToInt64(ddlTipoInteres.SelectedValue);
            tipoliq.tipopago = Convert.ToInt64(ddlTipoPago.SelectedValue);
            tipoliq.cobrointeresajuste = Convert.ToInt64(ddlCobroIntAju.SelectedValue);
            tipoliq.tipocuotasextras = Convert.ToInt64(ddlTipoCuotaExtra.SelectedValue);
            tipoliq.tipointeresextras = Convert.ToInt64(ddlTipoIntExtra.SelectedValue);
            tipoliq.tipopagosextras = Convert.ToInt64(ddlTipoPagoExtra.SelectedValue);
            
            

            if (idObjeto != "")
            {
                tipoliq.codtipoliquidacion = long.Parse(idObjeto);
                tipoLiqObService.ModificarTipoLiq(tipoliq, (Usuario)Session["usuario"]);
            }
            else
            {
                tipoliq = tipoLiqObService.CrearTipoLiq(tipoliq, (Usuario)Session["usuario"]);
                idObjeto = tipoliq.codtipoliquidacion.ToString();
            }

            Session[tipoLiqObService.CodigoPrograma + ".id"] = idObjeto;
            Navegar(Pagina.Lista);
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(tipoLiqObService.GetType().Name + "A", "btnGuardar_Click", ex);
        }
    }


    protected void btnRegresar_Click(object sender, ImageClickEventArgs e)
    {
        Navegar(Pagina.Lista);
    }

    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            Xpinn.Obligaciones.Entities.TipoLiquidacion tipoliq = new Xpinn.Obligaciones.Entities.TipoLiquidacion();
            if (idObjeto != "")
                tipoliq = tipoLiqObService.ConsultarTipoLiq(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);//, (TOSesion)Session["user"]);


            if (idObjeto != "")
            {
                if (!string.IsNullOrEmpty(tipoliq.codtipoliquidacion.ToString()))
                    txtCodigo.Text = tipoliq.codtipoliquidacion.ToString();
                if (!string.IsNullOrEmpty(tipoliq.descripcion))
                    txtTipLiq.Text = tipoliq.descripcion.Trim().ToString();

                if (!string.IsNullOrEmpty(tipoliq.tipocuota.ToString()))
                    ddlTipoCuota.SelectedValue = tipoliq.tipocuota.ToString();
                if (!string.IsNullOrEmpty(tipoliq.tipoamortizacion.ToString()))
                    ddlTipoAmortizacion.SelectedValue = tipoliq.tipoamortizacion.ToString();

                if (!string.IsNullOrEmpty(tipoliq.tipointeres.ToString()))
                    ddlTipoInteres.SelectedValue = tipoliq.tipointeres.ToString();
                if (!string.IsNullOrEmpty(tipoliq.tipopago.ToString()))
                    ddlTipoPago.SelectedValue = tipoliq.tipopago.ToString();
                if (!string.IsNullOrEmpty(tipoliq.cobrointeresajuste.ToString()))
                    ddlCobroIntAju.SelectedValue = tipoliq.cobrointeresajuste.ToString();

                if (!string.IsNullOrEmpty(tipoliq.tipocuotasextras.ToString()))
                    ddlTipoCuotaExtra.SelectedValue = tipoliq.tipocuotasextras.ToString();

                if (!string.IsNullOrEmpty(tipoliq.tipointeresextras.ToString()))
                    ddlTipoIntExtra.SelectedValue = tipoliq.tipointeresextras.ToString();

                if (!string.IsNullOrEmpty(tipoliq.tipopagosextras.ToString()))
                    ddlTipoPagoExtra.SelectedValue = tipoliq.tipopagosextras.ToString();

                ArmarComboTipoAmortizacion(ddlTipoCuota);

                
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(tipoLiqObService.GetType().Name + "A", "ObtenerDatos", ex);
        }
    }


    protected void LlenarComboTipoLiquidacion(DropDownList ddlTipoLiq)
    {

        Xpinn.Obligaciones.Services.TipoLiquidacionService tipoLiquidacionService = new Xpinn.Obligaciones.Services.TipoLiquidacionService();
        Xpinn.Obligaciones.Entities.TipoLiquidacion tipoLiq = new Xpinn.Obligaciones.Entities.TipoLiquidacion();
        ddlTipoLiq.DataSource = tipoLiquidacionService.ListarTipoLiquidacion(tipoLiq, (Usuario)Session["usuario"]);
        ddlTipoLiq.DataTextField = "descripcion";
        ddlTipoLiq.DataValueField = "tipoliquidacion";
        ddlTipoLiq.DataBind();

    }

    protected void ddlTipoCuota_SelectedIndexChanged(object sender, EventArgs e)
    {
        ArmarComboTipoAmortizacion(ddlTipoCuota);
    }


    protected void ArmarComboTipoAmortizacion(DropDownList ddlTipoCuota)
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("codigo");
        dt.Columns.Add("nombre");

        if (long.Parse(ddlTipoCuota.SelectedValue) == 1) // Pago Unico
        {
            DataRow fila = dt.NewRow();
            fila[0] = 1;
            fila[1] = "Pago Único";
            dt.Rows.Add(fila);
        }
        else if (long.Parse(ddlTipoCuota.SelectedValue) == 2) // Serie Uniforme
        {
            for (int i = 1; i <= 2; i++)
            {
                DataRow fila = dt.NewRow();
                if (i == 1)
                {
                    fila[0] = 2;
                    fila[1] = "Cuota Fija";
                }
                else
                {
                    fila[0] = 3;
                    fila[1] = "Cuota Variable";
                }

                dt.Rows.Add(fila);
            }
            
        }
        else // gradiente
        {
            for (int i = 1; i <= 3; i++)
            {
                DataRow fila = dt.NewRow();
                if (i == 1)
                {
                    fila[0] = 4;
                    fila[1] = "Aritmético";
                }
                else if (i == 2)
                {
                    fila[0] = 5;
                    fila[1] = "Geométrico";
                }
                else
                {
                    fila[0] = 6;
                    fila[1] = "Escalonado";
                }
                dt.Rows.Add(fila);
            }

        }


        ddlTipoAmortizacion.DataSource = dt;
        ddlTipoAmortizacion.DataTextField = "nombre";
        ddlTipoAmortizacion.DataValueField = "codigo";
        ddlTipoAmortizacion.DataBind();
    
    }
}