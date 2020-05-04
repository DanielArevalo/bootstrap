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
            if (Session[identificacionServicio.CodigoProgramaS + ".id"] != null)
                VisualizarOpciones(identificacionServicio.CodigoProgramaS, "E");
            else
                VisualizarOpciones(identificacionServicio.CodigoProgramaS, "A");
            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(identificacionServicio.CodigoProgramaS, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                CargarLista();
                if (Session[identificacionServicio.CodigoProgramaS + ".id"] != null)
                {
                    idObjeto = Session[identificacionServicio.CodigoProgramaS + ".id"].ToString();
                    Session.Remove(identificacionServicio.CodigoProgramaS + ".id");
                    ObtenerDatos(idObjeto);
                }
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(identificacionServicio.CodigoProgramaS, "Page_Load", ex);
        }
    }

    private void CargarLista()
    {
        PoblarListas poblar = new PoblarListas();

        poblar.PoblarListaDesplegable("GR_PROCESO_ENTIDAD","COD_PROCESO, DESCRIPCION","","1", ddlProceso, (Usuario)Session["usuario"]);
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtDescripcionSubproceso.Text.Trim() != "" && ddlProceso.SelectedValue != "0")
            {
                Identificacion pSubproceso = new Identificacion();
                pSubproceso.descripcion = txtDescripcionSubproceso.Text;
                pSubproceso.cod_proceso = Convert.ToInt64(ddlProceso.SelectedValue);
                if (idObjeto == "")
                    pSubproceso = identificacionServicio.CrearSubProcesoEntidad(pSubproceso, (Usuario)Session["usuario"]);
                else
                {
                    pSubproceso.cod_subproceso = Convert.ToInt64(idObjeto);
                    pSubproceso = identificacionServicio.ModificarSubProcesoEntidad(pSubproceso, (Usuario)Session["usuario"]);
                }
                Navegar(Pagina.Lista);
            }
            else
                VerError("Ingrese los datos completos");
        }
        catch(Exception ex)
        {
            VerError("Error al guardar los datos " + ex.Message);
        }
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Session.Remove(identificacionServicio.CodigoProgramaS + ".id");
        Navegar(Pagina.Lista);
    }

    private void ObtenerDatos(string idObjeto)
    {
        Identificacion pSubproceso = new Identificacion();
        pSubproceso.cod_subproceso = Convert.ToInt64(idObjeto);

        pSubproceso = identificacionServicio.ConsultarProcesoEntidad(pSubproceso, 2, (Usuario)Session["usuario"]);

        if (pSubproceso != null)
        {
            if(!string.IsNullOrWhiteSpace(pSubproceso.cod_subproceso.ToString()))
                txtCodigoSubproceso.Text = pSubproceso.cod_subproceso.ToString();
            if (!string.IsNullOrWhiteSpace(pSubproceso.descripcion.ToString()))
                txtDescripcionSubproceso.Text = pSubproceso.descripcion.ToString();
            if (!string.IsNullOrWhiteSpace(pSubproceso.cod_proceso.ToString()))
                ddlProceso.SelectedValue = pSubproceso.cod_proceso.ToString();
        }
    }
}