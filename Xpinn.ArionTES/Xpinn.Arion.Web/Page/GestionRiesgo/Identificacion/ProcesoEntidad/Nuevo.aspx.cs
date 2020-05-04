using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Riesgo.Services;
using Xpinn.Riesgo.Entities;
using Xpinn.Util;

public partial class Nuevo : GlobalWeb
{
    IdentificacionServices identificacionServicio = new IdentificacionServices();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            if (Session[identificacionServicio.CodigoProgramaP + ".id"] != null)
                VisualizarOpciones(identificacionServicio.CodigoProgramaP, "E");
            else
                VisualizarOpciones(identificacionServicio.CodigoProgramaP, "A");
            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(identificacionServicio.CodigoProgramaP, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
	{
        try
        {
            if (!IsPostBack)
            {
                if (Session[identificacionServicio.CodigoProgramaP + ".id"] != null)
                {
                    idObjeto = Session[identificacionServicio.CodigoProgramaP + ".id"].ToString();
                    Session.Remove(identificacionServicio.CodigoProgramaP + ".id");
                    ObtenerDatos(idObjeto);
                }
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(identificacionServicio.CodigoProgramaP, "Page_Load", ex);
        }
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtDescripcion.Text.Trim() != "" && txtDescripcion.Text != null)
            {

                Identificacion pProceso = new Identificacion();
                pProceso.descripcion = txtDescripcion.Text;
                if (idObjeto == "")
                    pProceso = identificacionServicio.CrearProcesoEntidad(pProceso, (Usuario)Session["usuario"]);
                else
                {
                    pProceso.cod_proceso = Convert.ToInt64(idObjeto);
                    pProceso = identificacionServicio.ModificarProcesoEntidad(pProceso, (Usuario)Session["usuario"]);
                }
                Navegar(Pagina.Lista);
            }
        }
        catch (Exception ex)
        {
            VerError("Error al guardar los datos" + ex.Message);
        }
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Session.Remove(identificacionServicio.CodigoProgramaP + ".id");
        Navegar(Pagina.Lista);
    }

    private void ObtenerDatos(string idObjeto)
    {        
        Identificacion pProceso = new Identificacion();
        pProceso.cod_proceso = Convert.ToInt64(idObjeto);

        pProceso = identificacionServicio.ConsultarProcesoEntidad(pProceso, 1, (Usuario)Session["usuario"]);

        if(pProceso != null)
        {
            if(!string.IsNullOrWhiteSpace(pProceso.cod_proceso.ToString()))
                txtCodigoProceso.Text = pProceso.cod_proceso.ToString();
            if (!string.IsNullOrWhiteSpace(pProceso.descripcion.ToString()))
                txtDescripcion.Text = pProceso.descripcion.ToString(); 
        }
    }
}