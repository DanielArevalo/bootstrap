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
    private Xpinn.Obligaciones.Services.LineaObligacionService LineaObnligacionServicio = new Xpinn.Obligaciones.Services.LineaObligacionService();
    Xpinn.Obligaciones.Entities.LineaObligacion LineaOb = new Xpinn.Obligaciones.Entities.LineaObligacion();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(LineaObnligacionServicio.CodigoPrograma, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoNuevo += btnNuevo_Click;
            toolBar.eventoRegresar += btnRegresar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(LineaObnligacionServicio.CodigoPrograma, "Page_PreInit", ex);
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
            BOexcepcion.Throw(LineaObnligacionServicio.CodigoPrograma + "L", "btnNuevo_Click", ex);
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
            BOexcepcion.Throw(LineaObnligacionServicio.CodigoPrograma, "Page_Load", ex);
        }
    }    

    private void Actualizar()
    {
        try
        {
            List<Xpinn.Obligaciones.Entities.LineaObligacion> lstConsulta = new List<Xpinn.Obligaciones.Entities.LineaObligacion>();
            lstConsulta = LineaObnligacionServicio.ListarLineaObligacion(LineaOb, (Usuario)Session["usuario"]);

            gvLineaOblig.DataSource = lstConsulta;

            if (lstConsulta.Count > 0)
            {
                gvLineaOblig.Visible = true;
                gvLineaOblig.DataBind();
            }
            else
            {
                gvLineaOblig.Visible = false;
            }

            Session.Add(LineaObnligacionServicio.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(LineaObnligacionServicio.CodigoPrograma, "Actualizar", ex);
        }
    }


    protected void gvLineaOblig_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            long idObjeto1 = Convert.ToInt64(gvLineaOblig.Rows[e.RowIndex].Cells[2].Text);

            LineaOb = LineaObnligacionServicio.ConsultarObligacionXLineaObligacion(idObjeto1, (Usuario)Session["usuario"]);

            if (LineaOb.conteo == 0)
            {
                LineaObnligacionServicio.EliminarLineaOb(idObjeto1, (Usuario)Session["usuario"]);
                Navegar(Pagina.Lista);
            }
            else
                VerError("La Línea de Obligación esta relacionada a uno o mas Obligaciones Financieras, no se puede realizar esta operación"); 
                
            
        }
        catch (Xpinn.Util.ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(LineaObnligacionServicio.GetType().Name + "L", "gvLineaOblig_RowDeleting", ex);
        }
    }

    protected void gvLineaOblig_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvLineaOblig.Rows[e.NewEditIndex].Cells[2].Text;
        Session[LineaObnligacionServicio.CodigoPrograma  + ".id"] = id;
        Navegar(Pagina.Nuevo);
    }


    protected void gvLineaOblig_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        ConfirmarEliminarFila(e, "btnEliminar");
    }
}