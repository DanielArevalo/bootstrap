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
            if (Session[identificacionServicio.CodigoProgramaF + ".id"] != null)
                VisualizarOpciones(identificacionServicio.CodigoProgramaF, "E");
            else
                VisualizarOpciones(identificacionServicio.CodigoProgramaF, "A");
            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(identificacionServicio.CodigoProgramaF, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                CargarListas();
                if (Session[identificacionServicio.CodigoProgramaF + ".id"] != null)
                {
                    idObjeto = Session[identificacionServicio.CodigoProgramaF + ".id"].ToString();
                    Session.Remove(identificacionServicio.CodigoProgramaF + ".id");
                    ObtenerDatos(idObjeto);
                }
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(identificacionServicio.CodigoProgramaF, "Page_Load", ex);
        }
    }

    private void CargarListas()
    {
        PoblarListas poblar = new PoblarListas();
        poblar.PoblarListaDesplegable("GR_RIESGO_GENERAL", "COD_RIESGO, SIGLA", "", "1", ddlSistemaRiesgo, (Usuario)Session["usuario"]);
        poblar.PoblarListaDesplegable("GR_SUBPROCESO_ENTIDAD", "COD_SUBPROCESO, DESCRIPCION", "", "1", ddlProcedimiento, (Usuario)Session["usuario"]);

        ddlFactorRiesgo.Items.Insert(0, new ListItem("Seleccione un item","0"));
        ddlFactorRiesgo.Items.Insert(1, new ListItem("Humano", "H"));
        ddlFactorRiesgo.Items.Insert(2, new ListItem("Tecnólogico", "T"));
        ddlFactorRiesgo.Items.Insert(3, new ListItem("Mixto", "M"));
        ddlFactorRiesgo.DataBind();
    }

    private bool ValidarDatos()
    {
        VerError("");
        if (ddlSistemaRiesgo.SelectedValue == "0")
        {
            VerError("Ingrese el sistema de riesgo al cual corresponde el factor");
            return false;
        }
        if (txtDescripcionFactor.Text == "" || txtDescripcionFactor == null)
        {
            VerError("Ingrese la descripción del factor de riesgo");
            return false;
        }
        if (ddlProcedimiento.SelectedValue == "0")
        {
            VerError("Ingrese el procedimiento asociado al factor de riesgo");
            return false;
        }
        if (ddlFactorRiesgo.SelectedValue == "0")
        {
            VerError("Ingrese el procedimiento asociado al factor de riesgo");
            return false;
        }
        return true;
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            if (ValidarDatos())
            {
                Identificacion pFactor = new Identificacion();
                pFactor.descripcion = txtDescripcionFactor.Text;
                pFactor.cod_riesgo = Convert.ToInt64(ddlSistemaRiesgo.SelectedValue);
                pFactor.cod_subproceso = Convert.ToInt64(ddlProcedimiento.SelectedValue);
                pFactor.factor_riesgo = ddlFactorRiesgo.SelectedValue;
            
                if (hdCodigo.Value == "")
                    pFactor = identificacionServicio.CrearFactorRiesgo(pFactor, (Usuario)Session["usuario"]);
                else
                {
                    pFactor.cod_factor = Convert.ToInt64(hdCodigo.Value);
                    pFactor = identificacionServicio.ModificarFactorRiesgo(pFactor, (Usuario)Session["usuario"]);
                }
                Navegar(Pagina.Lista);
            }
        }
        catch (Exception ex)
        {
            VerError("Error al guardar los datos " + ex.Message);
        }
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Session.Remove(identificacionServicio.CodigoProgramaF + ".id");
        Navegar(Pagina.Lista);
    }

    private void ObtenerDatos(string idObjeto)
    {
        Identificacion pFactor = new Identificacion();
        pFactor.cod_factor = Convert.ToInt64(idObjeto);

        pFactor = identificacionServicio.ConsultarFactorRiesgo(pFactor, (Usuario)Session["usuario"]);

        if (pFactor != null)
        {
            if (!string.IsNullOrWhiteSpace(pFactor.cod_factor.ToString()))
                hdCodigo.Value = pFactor.cod_factor.ToString();
            if (!string.IsNullOrWhiteSpace(pFactor.descripcion.ToString()))
                txtDescripcionFactor.Text = pFactor.descripcion.ToString();
            if (!string.IsNullOrWhiteSpace(pFactor.cod_subproceso.ToString()))
                ddlProcedimiento.SelectedValue = pFactor.cod_subproceso.ToString();
            if (!string.IsNullOrWhiteSpace(pFactor.cod_riesgo.ToString()))
            {
                ddlSistemaRiesgo.SelectedValue = pFactor.cod_riesgo.ToString();

                //Consultar la abreviatura para el código del factor según el sistema de riesgo
                ddlSistemaRiesgo_SelectedIndexChanged(ddlSistemaRiesgo, null);
            }
            if (!string.IsNullOrWhiteSpace(pFactor.factor_riesgo.ToString()))
                ddlFactorRiesgo.SelectedValue = pFactor.factor_riesgo.ToString();
        }
    }

    protected void ddlSistemaRiesgo_SelectedIndexChanged(object sender, EventArgs e)
    {
        Identificacion pFactor = new Identificacion();
        pFactor.cod_riesgo = Convert.ToInt64(ddlSistemaRiesgo.SelectedValue);
        pFactor = identificacionServicio.ConsultarSistemaRiesgo(pFactor, (Usuario)Session["usuario"]);

        txtCodigoFactorRiesgo.Text = pFactor.abreviatura + "-" + hdCodigo.Value;
    }
}