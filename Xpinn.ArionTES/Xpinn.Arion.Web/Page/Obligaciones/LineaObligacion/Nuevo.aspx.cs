using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using System.Data;
using System.Data.Common;

public partial class Nuevo : GlobalWeb
{
    Xpinn.Obligaciones.Services.LineaObligacionService lineaObService = new Xpinn.Obligaciones.Services.LineaObligacionService();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[lineaObService.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(lineaObService.CodigoPrograma, "E");
            else
                VisualizarOpciones(lineaObService.CodigoPrograma, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoRegresar += btnRegresar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(lineaObService.GetType().Name + "A", "Page_PreInit", ex);
        }
    }



    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                //se inicializa el combo de ciudades, centro de costos
                LlenarComboTipoMoneda(ddlMoneda );
                LlenarComboTipoLiquidacion(ddlTipoLiquidacion);

                if (Session[lineaObService.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[lineaObService.CodigoPrograma + ".id"].ToString();
                    Session.Remove(lineaObService.CodigoPrograma + ".id");
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
            BOexcepcion.Throw(lineaObService.GetType().Name + "A", "Page_Load", ex);
        }
    }

    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Xpinn.Obligaciones.Entities.LineaObligacion linea = new Xpinn.Obligaciones.Entities.LineaObligacion();
            if (idObjeto != "")
                linea = lineaObService.ConsultarLineaOb(Convert.ToInt64(idObjeto), (Usuario)Session["usuario"]);

            //se atrapan los datos del formulario
            linea.CODLINEAOBLIGACION = txtCodigo.Text == "" ? 0 : long.Parse(txtCodigo.Text);
            linea.NOMBRELINEA = txtLinea.Text.Trim();
            linea.TIPOLIQUIDACION = Convert.ToInt64(ddlTipoLiquidacion.SelectedValue);
            linea.TIPOMONEDA = Convert.ToInt64(ddlMoneda.SelectedValue);
            
            if (idObjeto != "")
            {
                linea.CODLINEAOBLIGACION = long.Parse(idObjeto);
                lineaObService.ModificarLineaObligacion(linea, (Usuario)Session["usuario"]);
            }
            else
            {
                linea = lineaObService.CrearLineaOb(linea, (Usuario)Session["usuario"]);
                idObjeto = linea.CODLINEAOBLIGACION.ToString();
            }

            Session[lineaObService.CodigoPrograma + ".id"] = idObjeto;
            Navegar(Pagina.Lista);
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(lineaObService.GetType().Name + "A", "btnGuardar_Click", ex);
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
            Xpinn.Obligaciones.Entities.LineaObligacion linea = new Xpinn.Obligaciones.Entities.LineaObligacion();
            if (idObjeto != "")
                linea = lineaObService.ConsultarLineaOb(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);//, (TOSesion)Session["user"]);


            if (idObjeto != "")
            {
                if (!string.IsNullOrEmpty(linea.CODLINEAOBLIGACION.ToString()))
                    txtCodigo.Text = linea.CODLINEAOBLIGACION.ToString();
                if (!string.IsNullOrEmpty(linea.NOMBRELINEA))
                    txtLinea.Text = linea.NOMBRELINEA.Trim().ToString();

                if (!string.IsNullOrEmpty(linea.TIPOMONEDA.ToString()))
                    ddlMoneda.SelectedValue = linea.TIPOMONEDA.ToString();
                if (!string.IsNullOrEmpty(linea.TIPOLIQUIDACION.ToString()))
                    ddlTipoLiquidacion.SelectedValue = linea.TIPOLIQUIDACION.ToString();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(lineaObService.GetType().Name + "A", "ObtenerDatos", ex);
        }
    }

    protected void LlenarComboTipoMoneda(DropDownList ddlMoneda)
    {
        Xpinn.Caja.Services.TipoMonedaService monedaService = new Xpinn.Caja.Services.TipoMonedaService();

        Xpinn.Caja.Entities.TipoMoneda moneda = new Xpinn.Caja.Entities.TipoMoneda();
        ddlMoneda.DataSource = monedaService.ListarTipoMoneda(moneda, (Usuario)Session["usuario"]);
        ddlMoneda.DataTextField = "descripcion";
        ddlMoneda.DataValueField = "cod_moneda";
        ddlMoneda.DataBind();
    }

    protected void LlenarComboTipoLiquidacion(DropDownList ddlTipoLiq)
    {

        Xpinn.Obligaciones.Services.TipoLiquidacionService tipoLiquidacionService = new Xpinn.Obligaciones.Services.TipoLiquidacionService();
        Xpinn.Obligaciones.Entities.TipoLiquidacion tipoLiq = new Xpinn.Obligaciones.Entities.TipoLiquidacion();
        ddlTipoLiq.DataSource = tipoLiquidacionService.ListarTipoLiquidacion(tipoLiq, (Usuario)Session["usuario"]);
        ddlTipoLiq.DataTextField = "descripcion";
        ddlTipoLiq.DataValueField = "codtipoliquidacion";
        ddlTipoLiq.DataBind();

    }
}