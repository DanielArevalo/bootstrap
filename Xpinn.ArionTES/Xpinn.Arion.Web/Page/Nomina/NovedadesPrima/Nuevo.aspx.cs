using System;
using Xpinn.Nomina.Services;

public partial class Nuevo : GlobalWeb
{
    NovedadPrimaService _novedadPrimaService = new NovedadPrimaService();


    #region  Eventos Carga Inicial


    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(_novedadPrimaService.CodigoPrograma, "L");

            Site toolBar = (Site)Master;

            toolBar.eventoRegresar += (s, evt) =>
            {
                Session.Remove(_novedadPrimaService.CodigoPrograma + ".idEmpleado");
                Navegar(Pagina.Lista);
            };
            toolBar.eventoLimpiar += (s, evt) => ctlListarEmpleados.Limpiar();
            toolBar.eventoConsultar += (s, evt) =>
            {
                VerError("");
                ctlListarEmpleados.Actualizar();
            };
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_novedadPrimaService.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
        }
    }

    #endregion


    protected void ctlListarEmpleados_OnEmpleadoSeleccionado(object sender, EmpleadosArgs e)
    {
        Session[_novedadPrimaService.CodigoPrograma + ".idEmpleado"] = e.IDEmpleado;
        Navegar(Pagina.Detalle);
    }

    protected void ctlListarEmpleados_OnErrorControl(object sender, EmpleadosArgs e)
    {
        if (e.Error != null)
        {
            VerError(e.Error.Message);
        }
    }
}