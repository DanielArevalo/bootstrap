using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using System.Data;
using System.Data.Common;

public partial class Lista : GlobalWeb
{
    Xpinn.Obligaciones.Services.ObligacionCreditoService oblCredService = new Xpinn.Obligaciones.Services.ObligacionCreditoService();
    Xpinn.Obligaciones.Entities.ObligacionCredito oblCred = new Xpinn.Obligaciones.Entities.ObligacionCredito();
    List<Xpinn.Obligaciones.Entities.ObligacionCredito> lstConsulta = new List<Xpinn.Obligaciones.Entities.ObligacionCredito>();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            VisualizarOpciones(oblCredService.CodigoPrograma5, "L");

            Site toolBar = (Site)this.Master;
            //toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(oblCredService.GetType().Name + "A", "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            if (!IsPostBack)
            {                
                ObtenerDatos();
                Actualizar();
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(oblCredService.GetType().Name + "A", "Page_Load", ex);
        }
    }

    protected void btnCancelar_Click(object sender, ImageClickEventArgs e)
    {
        Navegar("../../../General/Global/inicio.aspx");
    }

    //protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    //{
    //    try
    //    {
    //        oblCred = oblCredService.AnularOperacion(oblCred, (Usuario)Session["usuario"]);
    //        Navegar("../../../General/Global/inicio.aspx");
    //    }
    //    catch (Exception ex)
    //    {
    //        BOexcepcion.Throw(oblCredService.GetType().Name + "A", "ObtenerDatos", ex);
    //    }

    //}

    protected void ObtenerDatos()
    {
        try
        {
          txtFechaAnula.Text = DateTime.Now.ToShortDateString();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(oblCredService.GetType().Name + "A", "ObtenerDatos", ex);
        }
    }

    public void Actualizar()
    {
        try
        {
            oblCred.fecha = Convert.ToDateTime(txtFechaAnula.Text);
            lstConsulta = oblCredService.ListarOperaciones(oblCred, (Usuario)Session["usuario"]);//, (UserSession)Session["user"]);

            gvOperacion.DataSource = lstConsulta;

            if (lstConsulta.Count > 0)
            {
                gvOperacion.Visible = true;
                gvOperacion.DataBind();
            }
            else
            {
                gvOperacion.Visible = false;
            }

            Session.Add(oblCredService.GetType().Name + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(oblCredService.GetType().Name + "L", "Actualizar", ex);
        }
    }

    protected void gvOperacion_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvOperacion.Rows[e.NewEditIndex].Cells[0].Text;
        Session[oblCredService.CodigoPrograma5 + ".id"] = id;

        String cod_radica = gvOperacion.Rows[e.NewEditIndex].Cells[2].Text;
        String nombre = gvOperacion.Rows[e.NewEditIndex].Cells[4].Text;
        String identificacion = gvOperacion.Rows[e.NewEditIndex].Cells[3].Text;

        Navegar(Pagina.Nuevo);
    }
}