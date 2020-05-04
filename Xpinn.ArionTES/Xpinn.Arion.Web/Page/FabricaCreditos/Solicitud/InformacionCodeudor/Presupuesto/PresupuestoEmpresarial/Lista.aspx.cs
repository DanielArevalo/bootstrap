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

partial class Lista : GlobalWeb
{
    private Xpinn.FabricaCreditos.Services.PresupuestoEmpresarialService PresupuestoEmpresarialServicio = new Xpinn.FabricaCreditos.Services.PresupuestoEmpresarialService();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(PresupuestoEmpresarialServicio.CodigoPrograma, "L");

            Site2 toolBar = (Site2)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(PresupuestoEmpresarialServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                CargarValoresConsulta(pConsulta, PresupuestoEmpresarialServicio.CodigoPrograma);
                if (Session["Cod_persona"] != null)
                    txtCod_persona.Text = Session["Cod_persona"].ToString();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(PresupuestoEmpresarialServicio.GetType().Name, "Page_Load", ex);
        }
    }


    private void Borrar()
    {
        txtCod_presupuesto.Text = "";
        txtCod_persona.Text = "";
        txtTotalactivo.Text = "";
        txtTotalpasivo.Text = "";
        txtTotalpatrimonio.Text = "";
        txtVentamensual.Text = "";
        txtCostototal.Text = "";
        txtUtilidad.Text = "";
      
        idObjeto = "";
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        GuardarValoresConsulta(pConsulta, PresupuestoEmpresarialServicio.CodigoPrograma);    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        LimpiarValoresConsulta(pConsulta, PresupuestoEmpresarialServicio.CodigoPrograma);
    }

    protected void gvLista_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            ConfirmarEliminarFila(e, "btnBorrar");
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(PresupuestoEmpresarialServicio.CodigoPrograma + "L", "gvLista_RowDataBound", ex);
        }
    }
        

    private Xpinn.FabricaCreditos.Entities.PresupuestoEmpresarial ObtenerValores()
    {
        Xpinn.FabricaCreditos.Entities.PresupuestoEmpresarial vPresupuestoEmpresarial = new Xpinn.FabricaCreditos.Entities.PresupuestoEmpresarial();

        vPresupuestoEmpresarial.cod_persona = Convert.ToInt64(Session["Cod_persona"].ToString());
        txtCod_persona.Text = vPresupuestoEmpresarial.cod_persona.ToString(); 
        if(txtTotalactivo.Text.Trim() != "")
            vPresupuestoEmpresarial.totalactivo = Convert.ToInt64(txtTotalactivo.Text.Trim().Replace(@".",""));
        if(txtTotalpasivo.Text.Trim() != "")
            vPresupuestoEmpresarial.totalpasivo = Convert.ToInt64(txtTotalpasivo.Text.Trim().Replace(@".", ""));
        if(txtTotalpatrimonio.Text.Trim() != "")
            vPresupuestoEmpresarial.totalpatrimonio = Convert.ToInt64(txtTotalpatrimonio.Text.Trim().Replace(@".", ""));
        if(txtVentamensual.Text.Trim() != "")
            vPresupuestoEmpresarial.ventamensual = Convert.ToInt64(txtVentamensual.Text.Trim().Replace(@".", ""));
        if(txtCostototal.Text.Trim() != "")
            vPresupuestoEmpresarial.costototal = Convert.ToInt64(txtCostototal.Text.Trim().Replace(@".", ""));
        if(txtUtilidad.Text.Trim() != "")
            vPresupuestoEmpresarial.utilidad = Convert.ToInt64(txtUtilidad.Text.Trim().Replace(@".", ""));

        return vPresupuestoEmpresarial;
    }


    private void Edicion()
    {
        try
        {
            if (Session[PresupuestoEmpresarialServicio.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(PresupuestoEmpresarialServicio.CodigoPrograma, "E");
            else
                VisualizarOpciones(PresupuestoEmpresarialServicio.CodigoPrograma, "A");

            if (Session[PresupuestoEmpresarialServicio.CodigoPrograma + ".id"] != null)
            {
                idObjeto = Session[PresupuestoEmpresarialServicio.CodigoPrograma + ".id"].ToString();
                if (idObjeto != null)
                {
                    ObtenerDatos(idObjeto);
                    Session.Remove(PresupuestoEmpresarialServicio.CodigoPrograma + ".id");
                }

            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(PresupuestoEmpresarialServicio.GetType().Name + "A", "Page_PreInit", ex);
        }
    }

    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            Xpinn.FabricaCreditos.Entities.PresupuestoEmpresarial vPresupuestoEmpresarial = new Xpinn.FabricaCreditos.Entities.PresupuestoEmpresarial();
            vPresupuestoEmpresarial = PresupuestoEmpresarialServicio.ConsultarPresupuestoEmpresarial(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);

            if (vPresupuestoEmpresarial.cod_presupuesto != Int64.MinValue)
                txtCod_presupuesto.Text = HttpUtility.HtmlDecode(vPresupuestoEmpresarial.cod_presupuesto.ToString().Trim());
            if (vPresupuestoEmpresarial.cod_persona != Int64.MinValue)
                txtCod_persona.Text = HttpUtility.HtmlDecode(vPresupuestoEmpresarial.cod_persona.ToString().Trim());
            if (vPresupuestoEmpresarial.totalactivo != Int64.MinValue)
                txtTotalactivo.Text = HttpUtility.HtmlDecode(vPresupuestoEmpresarial.totalactivo.ToString().Trim());
            if (vPresupuestoEmpresarial.totalpasivo != Int64.MinValue)
                txtTotalpasivo.Text = HttpUtility.HtmlDecode(vPresupuestoEmpresarial.totalpasivo.ToString().Trim());
            if (vPresupuestoEmpresarial.totalpatrimonio != Int64.MinValue)
                txtTotalpatrimonio.Text = HttpUtility.HtmlDecode(vPresupuestoEmpresarial.totalpatrimonio.ToString().Trim());
            if (vPresupuestoEmpresarial.ventamensual != Int64.MinValue)
                txtVentamensual.Text = HttpUtility.HtmlDecode(vPresupuestoEmpresarial.ventamensual.ToString().Trim());
            if (vPresupuestoEmpresarial.costototal != Int64.MinValue)
                txtCostototal.Text = HttpUtility.HtmlDecode(vPresupuestoEmpresarial.costototal.ToString().Trim());
            if (vPresupuestoEmpresarial.utilidad != Int64.MinValue)
                txtUtilidad.Text = HttpUtility.HtmlDecode(vPresupuestoEmpresarial.utilidad.ToString().Trim());
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(PresupuestoEmpresarialServicio.CodigoPrograma, "ObtenerDatos", ex);
        }
    }


    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            Xpinn.FabricaCreditos.Entities.PresupuestoEmpresarial vPresupuestoEmpresarial = new Xpinn.FabricaCreditos.Entities.PresupuestoEmpresarial();

            txtTotalpatrimonio.Text = Convert.ToString(Convert.ToInt64(txtTotalactivo.Text.Replace(".", "")) - Convert.ToInt64(txtTotalpasivo.Text.Replace(".", "")));
            txtUtilidad.Text = Convert.ToString(Convert.ToInt64(txtVentamensual.Text.Replace(".", "")) - Convert.ToInt64(txtCostototal.Text.Replace(".", "")));
            if (idObjeto != "")
                vPresupuestoEmpresarial = PresupuestoEmpresarialServicio.ConsultarPresupuestoEmpresarial(Convert.ToInt64(idObjeto), (Usuario)Session["usuario"]);

            if (txtCod_presupuesto.Text != "") vPresupuestoEmpresarial.cod_presupuesto = Convert.ToInt64(txtCod_presupuesto.Text.Trim());
            if (txtCod_persona.Text != "") vPresupuestoEmpresarial.cod_persona = Convert.ToInt64(txtCod_persona.Text.Trim());
            if (txtTotalactivo.Text != "") vPresupuestoEmpresarial.totalactivo = Convert.ToInt64(txtTotalactivo.Text.Trim().Replace(".",""));
            if (txtTotalpasivo.Text != "") vPresupuestoEmpresarial.totalpasivo = Convert.ToInt64(txtTotalpasivo.Text.Trim().Replace(".", ""));
            if (txtTotalpatrimonio.Text != "") vPresupuestoEmpresarial.totalpatrimonio = Convert.ToInt64(txtTotalpatrimonio.Text.Trim().Replace(".", ""));
            if (txtVentamensual.Text != "") vPresupuestoEmpresarial.ventamensual = Convert.ToInt64(txtVentamensual.Text.Trim().Replace(".", ""));
            if (txtCostototal.Text != "") vPresupuestoEmpresarial.costototal = Convert.ToInt64(txtCostototal.Text.Trim().Replace(".", ""));
            if (txtUtilidad.Text != "") vPresupuestoEmpresarial.utilidad = Convert.ToInt64(txtUtilidad.Text.Trim().Replace(".", ""));

            if (idObjeto != "")
            {
                vPresupuestoEmpresarial.cod_presupuesto = Convert.ToInt64(idObjeto);
                PresupuestoEmpresarialServicio.ModificarPresupuestoEmpresarial(vPresupuestoEmpresarial, (Usuario)Session["usuario"]);
            }
            else
            {
                vPresupuestoEmpresarial = PresupuestoEmpresarialServicio.CrearPresupuestoEmpresarial(vPresupuestoEmpresarial, (Usuario)Session["usuario"]);
                idObjeto = vPresupuestoEmpresarial.cod_presupuesto.ToString();
            }

            Session[PresupuestoEmpresarialServicio.CodigoPrograma + ".id"] = idObjeto;
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(PresupuestoEmpresarialServicio.CodigoPrograma, "btnGuardar_Click", ex);
        }
    }

    protected void txtTotalpasivo_TextChanged(object sender, EventArgs e)
    {

    }
}