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
    private Xpinn.FabricaCreditos.Services.BalanceFamiliaService BalanceFamiliaServicio = new Xpinn.FabricaCreditos.Services.BalanceFamiliaService();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(BalanceFamiliaServicio.CodigoPrograma, "L");

            Site2 toolBar = (Site2)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoGuardar += btnGuardar_Click;                      

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(BalanceFamiliaServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                CargarValoresConsulta(pConsulta, BalanceFamiliaServicio.CodigoPrograma);

                if (Session[BalanceFamiliaServicio.GetType().Name + ".consulta"] != null)
                    Actualizar();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(BalanceFamiliaServicio.GetType().Name, "Page_Load", ex);
        }
    }

    protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    {
        GuardarValoresConsulta(pConsulta, BalanceFamiliaServicio.CodigoPrograma);
        Borrar();
    }

    private void Borrar()
    {
        txtTerrenosyedificios.Text = "";
        txtCorriente.Text = "";
        txtOtros.Text = "";
        txtLargoplazo.Text = "";
        
        idObjeto = "";
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        GuardarValoresConsulta(pConsulta, BalanceFamiliaServicio.CodigoPrograma);
        Actualizar();
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        LimpiarValoresConsulta(pConsulta, BalanceFamiliaServicio.CodigoPrograma);
    }

    protected void gvLista_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            ConfirmarEliminarFila(e, "btnBorrar");
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(BalanceFamiliaServicio.CodigoPrograma + "L", "gvLista_RowDataBound", ex);
        }
    }

    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        String id = gvLista.SelectedRow.Cells[0].Text;
        Session[BalanceFamiliaServicio.CodigoPrograma + ".id"] = id;
        Edicion();
    }

    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvLista.Rows[e.NewEditIndex].Cells[0].Text;//gvLista.DataKeys[gvLista.SelectedRow.RowIndex].Value.ToString();
        Session[BalanceFamiliaServicio.CodigoPrograma + ".id"] = id;       
        Navegar(Pagina.Editar);
    }

    protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            Int64 Cod_persona = Convert.ToInt64(Session["Cod_persona"].ToString());
            Int64 Cod_InfFin = Convert.ToInt64(Session["Cod_InfFin"].ToString());

            long id = Convert.ToInt64(gvLista.Rows[e.RowIndex].Cells[0].Text);
            BalanceFamiliaServicio.EliminarBalanceFamilia(id, (Usuario)Session["usuario"], Cod_persona, Cod_InfFin);
            Actualizar();
        }
        catch (Xpinn.Util.ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(BalanceFamiliaServicio.CodigoPrograma, "gvLista_RowDeleting", ex);
        }
    }

    protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvLista.PageIndex = e.NewPageIndex;
            Actualizar();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(BalanceFamiliaServicio.CodigoPrograma, "gvLista_PageIndexChanging", ex);
        }
    }

    private void Actualizar()
    {
        try
        {
            List<Xpinn.FabricaCreditos.Entities.BalanceFamilia> lstConsulta = new List<Xpinn.FabricaCreditos.Entities.BalanceFamilia>();
            lstConsulta = BalanceFamiliaServicio.ListarBalanceFamilia(ObtenerValores(), (Usuario)Session["usuario"]);

            gvLista.PageSize = 15;
            gvLista.EmptyDataText = "";
            gvLista.DataSource = lstConsulta;

            if (lstConsulta.Count > 0)
            {
                gvLista.Visible = true;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                gvLista.DataBind();
                ValidarPermisosGrilla(gvLista);
            }
            else
            {
                gvLista.Visible = false;
                lblTotalRegs.Visible = false;
            }

            Session.Add(BalanceFamiliaServicio.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(BalanceFamiliaServicio.CodigoPrograma, "Actualizar", ex);
        }
    }

    private Xpinn.FabricaCreditos.Entities.BalanceFamilia ObtenerValores()
    {
        Xpinn.FabricaCreditos.Entities.BalanceFamilia vBalanceFamilia = new Xpinn.FabricaCreditos.Entities.BalanceFamilia();
               
        vBalanceFamilia.cod_persona = Convert.ToInt64(Session["Cod_persona"].ToString());
        vBalanceFamilia.cod_inffin = Convert.ToInt64(Session["Cod_InfFin"].ToString());
        return vBalanceFamilia;
    }



    private void Edicion()
    {
        try
        {
            if (Session[BalanceFamiliaServicio.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(BalanceFamiliaServicio.CodigoPrograma, "E");
            else
                VisualizarOpciones(BalanceFamiliaServicio.CodigoPrograma, "A");

            if (Session[BalanceFamiliaServicio.CodigoPrograma + ".id"] != null)
            {
                idObjeto = Session[BalanceFamiliaServicio.CodigoPrograma + ".id"].ToString();
                if (idObjeto != null)
                {
                    ObtenerDatos(idObjeto);
                    Session.Remove(BalanceFamiliaServicio.CodigoPrograma + ".id");
                }

            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(BalanceFamiliaServicio.GetType().Name + "A", "Page_PreInit", ex);
        }
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            Xpinn.FabricaCreditos.Entities.BalanceFamilia vBalanceFamilia = new Xpinn.FabricaCreditos.Entities.BalanceFamilia();

            if (idObjeto != "")
                vBalanceFamilia = BalanceFamiliaServicio.ConsultarBalanceFamilia(Convert.ToInt64(idObjeto), (Usuario)Session["usuario"]);

            if (txtCod_balance.Text != "") vBalanceFamilia.cod_balance = Convert.ToInt64(txtCod_balance.Text.Trim());

            if (Session["Cod_persona"] == null)
            {
                VerError("No se pudo determinar código de la persona");                
            }
            vBalanceFamilia.cod_persona = Convert.ToInt64(Session["Cod_persona"].ToString());
            if (Session["Cod_InfFin"] == null)
            {
                VerError("No se pudo determinar código de la información financiera");                
            }
            vBalanceFamilia.cod_inffin = Convert.ToInt64(Session["Cod_InfFin"].ToString());

            if (txtTerrenosyedificios.Text != "") vBalanceFamilia.terrenosyedificios = Convert.ToInt64(txtTerrenosyedificios.Text.Replace(".",""));
            if (txtOtros.Text != "") vBalanceFamilia.otros = Convert.ToInt64(txtOtros.Text.Replace(".", ""));
            if (txtCorriente.Text != "") vBalanceFamilia.corriente = Convert.ToInt64(txtCorriente.Text.Replace(".", ""));
            if (txtLargoplazo.Text != "") vBalanceFamilia.largoplazo = Convert.ToInt64(txtLargoplazo.Text.Replace(".", ""));

            if (idObjeto != "")
            {
                vBalanceFamilia.cod_balance = Convert.ToInt64(idObjeto);
                BalanceFamiliaServicio.ModificarBalanceFamilia(vBalanceFamilia, (Usuario)Session["usuario"]);
            }
            else
            {
                vBalanceFamilia = BalanceFamiliaServicio.CrearBalanceFamilia(vBalanceFamilia, (Usuario)Session["usuario"]);
                idObjeto = vBalanceFamilia.cod_balance.ToString();
            }

            Session[BalanceFamiliaServicio.CodigoPrograma + ".id"] = idObjeto;
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(BalanceFamiliaServicio.CodigoPrograma, "btnGuardar_Click", ex);
        }
        Actualizar();
    }



    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            Xpinn.FabricaCreditos.Entities.BalanceFamilia vBalanceFamilia = new Xpinn.FabricaCreditos.Entities.BalanceFamilia();
            vBalanceFamilia = BalanceFamiliaServicio.ConsultarBalanceFamilia(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);

            if (vBalanceFamilia.cod_balance != Int64.MinValue)
                txtCod_balance.Text = HttpUtility.HtmlDecode(vBalanceFamilia.cod_balance.ToString().Trim());
            if (vBalanceFamilia.terrenosyedificios != Int64.MinValue)
                txtTerrenosyedificios.Text = HttpUtility.HtmlDecode(vBalanceFamilia.terrenosyedificios.ToString().Trim());
            if (vBalanceFamilia.otros != Int64.MinValue)
                txtOtros.Text = HttpUtility.HtmlDecode(vBalanceFamilia.otros.ToString().Trim());
            if (vBalanceFamilia.corriente != Int64.MinValue)
                txtCorriente.Text = HttpUtility.HtmlDecode(vBalanceFamilia.corriente.ToString().Trim());
            if (vBalanceFamilia.largoplazo != Int64.MinValue)
                txtLargoplazo.Text = HttpUtility.HtmlDecode(vBalanceFamilia.largoplazo.ToString().Trim());
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(BalanceFamiliaServicio.CodigoPrograma, "ObtenerDatos", ex);
        }
    }


}