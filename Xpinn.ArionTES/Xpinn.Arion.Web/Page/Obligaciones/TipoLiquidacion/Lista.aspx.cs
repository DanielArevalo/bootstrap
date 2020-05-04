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

public partial class Lista : GlobalWeb
{
    private Xpinn.Obligaciones.Services.TipoLiquidacionService TipoLiqnligacionServicio = new Xpinn.Obligaciones.Services.TipoLiquidacionService();
    Xpinn.Obligaciones.Entities.TipoLiquidacion TipoLiq = new Xpinn.Obligaciones.Entities.TipoLiquidacion();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(TipoLiqnligacionServicio.CodigoPrograma, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoNuevo += btnNuevo_Click;
            toolBar.eventoRegresar += btnRegresar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(TipoLiqnligacionServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void btnRegresar_Click(object sender, EventArgs e)
    {
        Navegar("../../../General/Global/inicio.aspx");
    }


    protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Navegar(Pagina.Nuevo);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(TipoLiqnligacionServicio.CodigoPrograma + "L", "btnNuevo_Click", ex);
        }

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                Actualizar();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(TipoLiqnligacionServicio.CodigoPrograma, "Page_Load", ex);
        }
    }

    private void Actualizar()
    {
        try
        {
            List<Xpinn.Obligaciones.Entities.TipoLiquidacion> lstConsulta = new List<Xpinn.Obligaciones.Entities.TipoLiquidacion>();
            lstConsulta = TipoLiqnligacionServicio.ListarTipoLiquidacion(TipoLiq, (Usuario)Session["usuario"]);

            gvTipoLiquidacion.DataSource = lstConsulta;

            if (lstConsulta.Count > 0)
            {
                gvTipoLiquidacion.Visible = true;
                gvTipoLiquidacion.DataBind();
            }
            else
            {
                gvTipoLiquidacion.Visible = false;
            }

            Session.Add(TipoLiqnligacionServicio.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(TipoLiqnligacionServicio.CodigoPrograma, "Actualizar", ex);
        }
    }


    protected void gvTipoLiquidacion_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            long idObjeto1 = Convert.ToInt64(gvTipoLiquidacion.Rows[e.RowIndex].Cells[2].Text);

            TipoLiq = TipoLiqnligacionServicio.ConsultarTipoLiquidacionXLineaObligacion(idObjeto1, (Usuario)Session["usuario"]);

            if (TipoLiq.conteo == 0)
            {
                TipoLiqnligacionServicio.EliminarTipoLiq(idObjeto1, (Usuario)Session["usuario"]);
                Navegar(Pagina.Lista);
            }
            else
                VerError("El Tipo de Liquidacion que esta tratando de Eliminar tiene Lineas de Obligacion Asociadas, No Puede realizar esta operación ");
        }
        catch (Xpinn.Util.ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(TipoLiqnligacionServicio.GetType().Name + "L", "gvTipoLiquidacion_RowDeleting", ex);
        }
    }

    protected void gvTipoLiquidacion_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvTipoLiquidacion.Rows[e.NewEditIndex].Cells[2].Text;
        Session[TipoLiqnligacionServicio.CodigoPrograma + ".id"] = id;
        Navegar(Pagina.Nuevo);
    }


    protected void gvTipoLiquidacion_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        ConfirmarEliminarFila(e, "btnEliminar");
    }
}