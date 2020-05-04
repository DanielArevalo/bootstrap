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

partial class Nuevo : GlobalWeb
{
    private Xpinn.Aportes.Services.PlanesTelefonicosService plantelservicio = new Xpinn.Aportes.Services.PlanesTelefonicosService();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
          
            if (Session[plantelservicio.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(plantelservicio.CodigoPrograma, "E");
            else
                VisualizarOpciones(plantelservicio.CodigoPrograma, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(plantelservicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            
            if (!IsPostBack)
            {              
                if (Session[plantelservicio.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[plantelservicio.CodigoPrograma + ".id"].ToString();
                    ObtenerDatos(idObjeto);
                }
                else
                {
                    txtCodigo.Enabled = false;
                    txtCodigo.Text = "0";
                }
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(plantelservicio.CodigoPrograma, "Page_Load", ex);
        }
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            Xpinn.Aportes.Entities.PlanTelefonico planTelefonico = new Xpinn.Aportes.Entities.PlanTelefonico();

            if (idObjeto != "")
                planTelefonico = plantelservicio.ConsultarPlanTelefonico(Convert.ToInt32(idObjeto), (Usuario)Session["usuario"]);

            planTelefonico.cod_plan = Convert.ToInt32(txtCodigo.Text.Trim());
            planTelefonico.nombre = Convert.ToString(txtDescripcion.Text.Trim());
            planTelefonico.valor = Convert.ToDecimal(txtValor.Text.Trim());            

            if (idObjeto != "")
            {
                planTelefonico.cod_plan = Convert.ToInt32(idObjeto);
                plantelservicio.ModificarPlanTelefonico(planTelefonico, (Usuario)Session["usuario"]);
            }
            else
            {
                planTelefonico = plantelservicio.CrearPlanTelefonico(planTelefonico, (Usuario)Session["usuario"]);
                idObjeto = planTelefonico.cod_plan.ToString();
            }

            Session[plantelservicio.CodigoPrograma + ".id"] = idObjeto;
            Navegar(Pagina.Lista);
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(plantelservicio.CodigoPrograma, "btnGuardar_Click", ex);
        }
    }

    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }

    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            Xpinn.Aportes.Entities.PlanTelefonico vPlanTel = new Xpinn.Aportes.Entities.PlanTelefonico();

            vPlanTel = plantelservicio.ConsultarPlanTelefonico(Convert.ToInt32(pIdObjeto), (Usuario)Session["usuario"]);

            if (!string.IsNullOrEmpty(vPlanTel.cod_plan.ToString()))
                txtCodigo.Text = HttpUtility.HtmlDecode(vPlanTel.cod_plan.ToString().Trim());
            if (!string.IsNullOrEmpty(vPlanTel.nombre))
                txtDescripcion.Text = HttpUtility.HtmlDecode(vPlanTel.nombre.ToString().Trim());
            if (!string.IsNullOrEmpty(vPlanTel.valor.ToString()))
                txtValor.Text = HttpUtility.HtmlDecode(vPlanTel.valor.ToString().Trim());
     
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(plantelservicio.CodigoPrograma, "ObtenerDatos", ex);
        }
    }




}