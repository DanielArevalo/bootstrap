using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
//using Oracle.DataAccess.Client;

public partial class PreAnalisis : GlobalWeb
{
    Xpinn.FabricaCreditos.Services.PreAnalisisService preAnalisisServicio = new Xpinn.FabricaCreditos.Services.PreAnalisisService();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(preAnalisisServicio.CodigoPrograma, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoNuevo += btnNuevo_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
            //toolBar.eventoLimpiar += btnLimpiar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(preAnalisisServicio.GetType().Name + "L", "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                CargarValoresConsulta(pConsulta, preAnalisisServicio.GetType().Name);
                //if (Session[preAnalisisServicio.GetType().Name + ".consulta"] != null)
                //{
                    Actualizar();
                //}
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(preAnalisisServicio.GetType().Name + "L", "Page_Load", ex);
        }
    }

    protected void gvParametrizacion_SelectedIndexChanged(object sender, EventArgs e)
    {
        String id = gvParametrizacion.DataKeys[gvParametrizacion.SelectedRow.RowIndex].Values[0].ToString();
        Session[preAnalisisServicio.CodigoPrograma + ".id"] = "P" + id;
        Navegar(Pagina.Detalle);
    }

    protected void gvParametrizacion_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        ConfirmarEliminarFila(e, "btnBorrar");
    }

    protected void gvParametrizacion_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            long idObjeto = Convert.ToInt64(gvParametrizacion.Rows[e.RowIndex].Cells[0].Text);
            preAnalisisServicio.EliminarPrograma(idObjeto, (Usuario)Session["usuario"]);
            Actualizar();
        }
        catch (Xpinn.Util.ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(preAnalisisServicio.CodigoPrograma + "L", "gvParametrizacion_RowDeleting", ex);
        }
    }
    protected void gvParametrizacion_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvParametrizacion.DataKeys[e.NewEditIndex].Values[0].ToString();
        Session[preAnalisisServicio.CodigoPrograma + ".id"] = "P" + id;
        Session[preAnalisisServicio.CodigoPrograma + ".from"] = "l";
        Response.Redirect("~/Page/FabricaCreditos/ConsultaDatacredito/Parametrizacion/Nuevo.aspx");
    }

    
    protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    {
        GuardarValoresConsulta(pConsulta, preAnalisisServicio.GetType().Name);
        Navegar(Pagina.Nuevo);
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        GuardarValoresConsulta(pConsulta, preAnalisisServicio.GetType().Name);
        Actualizar();
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        LimpiarValoresConsulta(pConsulta, preAnalisisServicio.GetType().Name);
    }

    protected void gvParametrizacion_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvParametrizacion.PageIndex = e.NewPageIndex;
            Actualizar();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(preAnalisisServicio.GetType().Name + "L", "gvParametrizacion_PageIndexChanging", ex);
        }
    }


    private void Actualizar()
    {
        try
        {
            List<Xpinn.FabricaCreditos.Entities.Parametrizar> lstConsulta = new List<Xpinn.FabricaCreditos.Entities.Parametrizar>();
            lstConsulta = preAnalisisServicio.ListarPrograma(ObtenerValores(), (Usuario)Session["usuario"]);

            gvParametrizacion.DataSource = lstConsulta;
            lblTituloPreAnalisis.Visible = true;

            if (lstConsulta.Count > 0)
            {
                gvParametrizacion.Visible = true;
                lblInfo.Visible = false;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                gvParametrizacion.DataBind();
                ValidarPermisosGrilla(gvParametrizacion);
            }
            else
            {
                gvParametrizacion.Visible = false;
                lblInfo.Visible = true;
                lblTotalRegs.Visible = false;
            }

            Session.Add(preAnalisisServicio.GetType().Name + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(preAnalisisServicio.GetType().Name + "L", "Actualizar", ex);
        }


        try
        {
            List<Xpinn.FabricaCreditos.Entities.Parametrizar> lstCentrales = new List<Xpinn.FabricaCreditos.Entities.Parametrizar>();
            lstCentrales = preAnalisisServicio.ListarCentral(ObtenerValores(), (Usuario)Session["usuario"]);

            gvCentrales.PageSize = Convert.ToInt32(ConfigurationManager.AppSettings["pageSize"]);
            gvCentrales.DataSource = lstCentrales;
            lblTituloCentrales.Visible = true;

            if (lstCentrales.Count > 0)
            {
                gvCentrales.Visible = true;
                lblInfoCentrales.Visible = false;
                lblTotalRegsCentrales.Visible = true;
                lblTotalRegsCentrales.Text = "<br/> Registros encontrados " + lstCentrales.Count.ToString();
                gvCentrales.DataBind();
                ValidarPermisosGrilla(gvCentrales);
            }
            else
            {
                gvCentrales.Visible = false;
                lblInfoCentrales.Visible = true;
                lblTotalRegsCentrales.Visible = false;
            }

            Session.Add(preAnalisisServicio.GetType().Name + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(preAnalisisServicio.GetType().Name + "L", "Actualizar", ex);
        }
    }



    private Xpinn.FabricaCreditos.Entities.Parametrizar ObtenerValores()
    {
        Xpinn.FabricaCreditos.Entities.Parametrizar parametrizar = new Xpinn.FabricaCreditos.Entities.Parametrizar();
        return parametrizar;
    }


    protected void gvCentrales_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvCentrales.PageIndex = e.NewPageIndex;
            Actualizar();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(preAnalisisServicio.GetType().Name + "L", "gvParametrizacion_PageIndexChanging", ex);
        }
    }
    protected void gvCentrales_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        ConfirmarEliminarFila(e, "btnBorrar");
    }

    protected void gvCentrales_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            long idObjeto = Convert.ToInt64(gvCentrales.Rows[e.RowIndex].Cells[0].Text);
            preAnalisisServicio.EliminarCentral(idObjeto, (Usuario)Session["usuario"]);
            Actualizar();
        }
        catch (Xpinn.Util.ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(preAnalisisServicio.CodigoPrograma + "L", "gvCentral_RowDeleting", ex);
        }
    }
    protected void gvCentrales_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvCentrales.DataKeys[e.NewEditIndex].Values[0].ToString();
        Session[preAnalisisServicio.CodigoPrograma + ".id"] = "C" + id;
        Session[preAnalisisServicio.CodigoPrograma + ".from"] = "l";
        Response.Redirect("~/Page/FabricaCreditos/ConsultaDatacredito/Parametrizacion/Nuevo.aspx");
        //Navegar(Pagina.Editar);
    }
    protected void gvCentrales_SelectedIndexChanged(object sender, EventArgs e)
    {
        String id = gvCentrales.DataKeys[gvCentrales.SelectedRow.RowIndex].Values[0].ToString();
        Session[preAnalisisServicio.CodigoPrograma + ".id"] = "C" + id;
        Navegar(Pagina.Detalle);
    }


  
}