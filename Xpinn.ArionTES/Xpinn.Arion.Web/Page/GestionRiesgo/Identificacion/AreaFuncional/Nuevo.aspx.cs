using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using Xpinn.Riesgo.Entities;
using Xpinn.Riesgo.Services;

public partial class Nuevo : GlobalWeb
{
    IdentificacionServices identificacionServicio = new IdentificacionServices();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            if (Session[identificacionServicio.CodigoProgramaA + ".id"] != null)
                VisualizarOpciones(identificacionServicio.CodigoProgramaA, "E");
            else
                VisualizarOpciones(identificacionServicio.CodigoProgramaA, "A");
            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(identificacionServicio.CodigoProgramaA, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Session[identificacionServicio.CodigoProgramaA + ".id"] != null)
                {
                    idObjeto = Session[identificacionServicio.CodigoProgramaA + ".id"].ToString();
                    Session.Remove(identificacionServicio.CodigoProgramaA + ".id");
                    ObtenerDatos(idObjeto);
                }
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(identificacionServicio.CodigoProgramaA, "Page_Load", ex);
        }
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtDescripcionArea.Text.Trim() != "")
            {
                Identificacion pArea = new Identificacion();
                pArea.descripcion = txtDescripcionArea.Text;

                if (idObjeto == "")
                    pArea = identificacionServicio.CrearAreaFuncional(pArea, (Usuario)Session["usuario"]);
                else
                {
                    pArea.cod_area = Convert.ToInt64(idObjeto);
                    pArea = identificacionServicio.ModificarAreaFuncional(pArea, (Usuario)Session["usuario"]);
                }
                Navegar(Pagina.Lista);
            }
            else
                VerError("Ingrese la descripción");
        }
        catch (Exception ex)
        {
            VerError("Error al guardar los datos " + ex.Message);
        }
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Session.Remove(identificacionServicio.CodigoProgramaA + ".id");
        Navegar(Pagina.Lista);
    }

    private void ObtenerDatos(string idObjeto)
    {
        Identificacion pArea = new Identificacion();
        pArea.cod_area = Convert.ToInt64(idObjeto);

        pArea = identificacionServicio.ConsultarAreasEntidad(pArea, (Usuario)Session["usuario"]);

        if (pArea != null)
        {
            if (!string.IsNullOrEmpty(pArea.cod_area.ToString()))
                txtCodigoArea.Text = pArea.cod_area.ToString();
            if (!string.IsNullOrEmpty(pArea.descripcion.ToString()))
                txtDescripcionArea.Text = pArea.descripcion.ToString();
        }
    }
}