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
using System.Globalization;

public partial class AseMoviGralCredito : GlobalWeb
{
    MovGralCreditoService movGrlService = new MovGralCreditoService();
    Producto producto;
    EstadoCuentaService serviceEstadoCuenta = new EstadoCuentaService();
    private static string NAME_CACHE = "EstadoCuenta";

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(movGrlService.CodigoPrograma, "L");

            Site toolBar = (Site)Master;
            toolBar.eventoConsultar += ctlListarCreditos.Consultar;
            toolBar.eventoLimpiar += ctlListarCreditos.Limpiar;
            ctlListarCreditos.Error += CtlListarCreditos_Error;
            ctlListarCreditos.NuevaPagina += CtlListarCreditos_NuevaPagina;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(movGrlService.GetType().Name + "L", "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            if (!IsPostBack)
            {
                if (Session[movGrlService.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[movGrlService.CodigoPrograma + ".id"].ToString();
                }

                // Se puede definir un filtro permanente para los creditos en el primer parametro
                // Definir el filtro como un T-SQL query comenzando desde el WHERE
                // El segundo parametro obliga a que filtres usando los filtros de la grilla:D
                ctlListarCreditos.CargaInicial("", true);
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(movGrlService.GetType().Name + "L", "Page_Load", ex);
        }
    }


    void CtlListarCreditos_Error(object sender, ListarCreditosPorFiltroArgs e)
    {
        VerError(e.Error);
    }


    void CtlListarCreditos_NuevaPagina(object sender, ListarCreditosPorFiltroArgs e)
    {
        try
        {
            producto = new Producto();
            producto.CodLineaCredito = e.NrLinea;
            producto.CodRadicacion = e.Nradicacion;

            Session[MOV_GRAL_CRED_PRODUC] = producto;

            Navegar("~/Page/Asesores/EstadoCuenta/Creditos/MovimientoGeneral/Detalle.aspx");
        }
        catch(Exception ex)
        {
            VerError("Hubo un error al resolver los datos para ver el movimiento general, " + ex.Message);
            return;
        }
    }

}