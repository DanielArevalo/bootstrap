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
    private Xpinn.FabricaCreditos.Services.PresupuestoFamiliarService PresupuestoFamiliarServicio = new Xpinn.FabricaCreditos.Services.PresupuestoFamiliarService();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(PresupuestoFamiliarServicio.CodigoPrograma, "L");

            Site2 toolBar = (Site2)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoGuardar += btnGuardar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(PresupuestoFamiliarServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                CargarValoresConsulta(pConsulta, PresupuestoFamiliarServicio.CodigoPrograma);
                //CargarListas();

                if (Session[PresupuestoFamiliarServicio.GetType().Name + ".consulta"] != null)
                    Actualizar();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(PresupuestoFamiliarServicio.GetType().Name, "Page_Load", ex);
        }
    }

    private void Borrar()
    {
        //CargarListas();
        txtCod_presupuesto.Text = "";
        txtCod_persona.Text = "";
        txtActividadprincipal.Text = "";
        txtConyuge.Text = "";
        txtOtrosingresos.Text = "";
        txtConsumofamiliar.Text = "";
        txtObligaciones.Text = "";
        txtExcedente.Text = "";
      
        idObjeto = "";
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        GuardarValoresConsulta(pConsulta, PresupuestoFamiliarServicio.CodigoPrograma);
        Actualizar();
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        LimpiarValoresConsulta(pConsulta, PresupuestoFamiliarServicio.CodigoPrograma);
    }

    protected void gvLista_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            ConfirmarEliminarFila(e, "btnBorrar");
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(PresupuestoFamiliarServicio.CodigoPrograma + "L", "gvLista_RowDataBound", ex);
        }
    }

    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        //String id = gvLista.DataKeys[gvLista.SelectedRow.RowIndex].Value.ToString();
        String id = gvLista.SelectedRow.Cells[0].Text;
        Session[PresupuestoFamiliarServicio.CodigoPrograma + ".id"] = id;
        Edicion();
        //Navegar(Pagina.Detalle);
    }

    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvLista.Rows[e.NewEditIndex].Cells[0].Text;//gvLista.DataKeys[gvLista.SelectedRow.RowIndex].Value.ToString();
        Session[PresupuestoFamiliarServicio.CodigoPrograma + ".id"] = id;
        Navegar(Pagina.Editar);
    }

    protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            long id = Convert.ToInt64(gvLista.Rows[e.RowIndex].Cells[0].Text);
            PresupuestoFamiliarServicio.EliminarPresupuestoFamiliar(id, (Usuario)Session["usuario"]);
            Actualizar();
        }
        catch (Xpinn.Util.ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(PresupuestoFamiliarServicio.CodigoPrograma, "gvLista_RowDeleting", ex);
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
            BOexcepcion.Throw(PresupuestoFamiliarServicio.CodigoPrograma, "gvLista_PageIndexChanging", ex);
        }
    }

    private void Actualizar()
    {
        try
        {
            List<Xpinn.FabricaCreditos.Entities.PresupuestoFamiliar> lstConsulta = new List<Xpinn.FabricaCreditos.Entities.PresupuestoFamiliar>();
            lstConsulta = PresupuestoFamiliarServicio.ListarPresupuestoFamiliar(ObtenerValores(), (Usuario)Session["usuario"]);

            gvLista.PageSize = 15;
            gvLista.EmptyDataText = "";
            gvLista.DataSource = lstConsulta;

            if (lstConsulta.Count > 0)
            {
                gvLista.Visible = false;
                lblTotalRegs.Visible = false;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                gvLista.DataBind();
                ValidarPermisosGrilla(gvLista);
            }
            else
            {
                gvLista.Visible = false;
                lblTotalRegs.Visible = false;
            }

            Session.Add(PresupuestoFamiliarServicio.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(PresupuestoFamiliarServicio.CodigoPrograma, "Actualizar", ex);
        }
    }

    private Xpinn.FabricaCreditos.Entities.PresupuestoFamiliar ObtenerValores()
    {
        Xpinn.FabricaCreditos.Entities.PresupuestoFamiliar vPresupuestoFamiliar = new Xpinn.FabricaCreditos.Entities.PresupuestoFamiliar();
            
        if (Session["CodCodeudor"].ToString() != "0" && Session["CodCodeudor"].ToString() != null)
        { 
            vPresupuestoFamiliar.cod_persona = Convert.ToInt64(Session["CodCodeudor"].ToString());
            if(txtActividadprincipal.Text.Trim() != "")
                vPresupuestoFamiliar.actividadprincipal = Convert.ToInt64(txtActividadprincipal.Text.Trim().Replace(@".",""));
            if(txtConyuge.Text.Trim() != "")
                vPresupuestoFamiliar.conyuge = Convert.ToInt64(txtConyuge.Text.Trim().Replace(@".",""));
            if(txtOtrosingresos.Text.Trim() != "")
                vPresupuestoFamiliar.otrosingresos = Convert.ToInt64(txtOtrosingresos.Text.Trim().Replace(@".",""));
            if(txtConsumofamiliar.Text.Trim() != "")
                vPresupuestoFamiliar.consumofamiliar = Convert.ToInt64(txtConsumofamiliar.Text.Trim().Replace(@".",""));
            if(txtObligaciones.Text.Trim() != "")
                vPresupuestoFamiliar.obligaciones = Convert.ToInt64(txtObligaciones.Text.Trim().Replace(@".", ""));
            if(txtExcedente.Text.Trim() != "")
                vPresupuestoFamiliar.excedente = Convert.ToInt64(txtExcedente.Text.Trim().Replace(@".", ""));            
        }
        return vPresupuestoFamiliar;
        
    }

    private void Edicion()
    {
        try
        {
            if (Session[PresupuestoFamiliarServicio.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(PresupuestoFamiliarServicio.CodigoPrograma, "E");
            else
                VisualizarOpciones(PresupuestoFamiliarServicio.CodigoPrograma, "A");

            //Page_Load
            if (Session[PresupuestoFamiliarServicio.CodigoPrograma + ".id"] != null)
            {
                idObjeto = Session[PresupuestoFamiliarServicio.CodigoPrograma + ".id"].ToString();
                if (idObjeto != null)
                {
                    ObtenerDatos(idObjeto);
                    Session.Remove(PresupuestoFamiliarServicio.CodigoPrograma + ".id");
                }
            }
            //else
            //{
            //    CargarListas();
            //}
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(PresupuestoFamiliarServicio.GetType().Name + "A", "Page_PreInit", ex);
        }
    }

    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            Xpinn.FabricaCreditos.Entities.PresupuestoFamiliar vPresupuestoFamiliar = new Xpinn.FabricaCreditos.Entities.PresupuestoFamiliar();
            vPresupuestoFamiliar = PresupuestoFamiliarServicio.ConsultarPresupuestoFamiliar(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);

            if (vPresupuestoFamiliar.cod_presupuesto != Int64.MinValue)
                txtCod_presupuesto.Text = HttpUtility.HtmlDecode(vPresupuestoFamiliar.cod_presupuesto.ToString().Trim());
            if (vPresupuestoFamiliar.cod_persona != Int64.MinValue)
                txtCod_persona.Text = HttpUtility.HtmlDecode(vPresupuestoFamiliar.cod_persona.ToString().Trim());
            if (vPresupuestoFamiliar.actividadprincipal != Int64.MinValue)
                txtActividadprincipal.Text = HttpUtility.HtmlDecode(vPresupuestoFamiliar.actividadprincipal.ToString().Trim());
            if (vPresupuestoFamiliar.conyuge != Int64.MinValue)
                txtConyuge.Text = HttpUtility.HtmlDecode(vPresupuestoFamiliar.conyuge.ToString().Trim());
            if (vPresupuestoFamiliar.otrosingresos != Int64.MinValue)
                txtOtrosingresos.Text = HttpUtility.HtmlDecode(vPresupuestoFamiliar.otrosingresos.ToString().Trim());
            if (vPresupuestoFamiliar.consumofamiliar != Int64.MinValue)
                txtConsumofamiliar.Text = HttpUtility.HtmlDecode(vPresupuestoFamiliar.consumofamiliar.ToString().Trim());
            if (vPresupuestoFamiliar.obligaciones != Int64.MinValue)
                txtObligaciones.Text = HttpUtility.HtmlDecode(vPresupuestoFamiliar.obligaciones.ToString().Trim());
            if (vPresupuestoFamiliar.excedente != Int64.MinValue)
                txtExcedente.Text = HttpUtility.HtmlDecode(vPresupuestoFamiliar.excedente.ToString().Trim());
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(PresupuestoFamiliarServicio.CodigoPrograma, "ObtenerDatos", ex);
        }
    }


    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            txtExcedente.Text = Convert.ToString(Convert.ToInt64(txtActividadprincipal.Text.Replace(".", "")) + Convert.ToInt64(txtConyuge.Text.Replace(".", "")) + Convert.ToInt64(txtOtrosingresos.Text.Replace(".", "")) - Convert.ToInt64(txtObligaciones.Text.Replace(".", "")));

            Xpinn.FabricaCreditos.Entities.PresupuestoFamiliar vPresupuestoFamiliar = new Xpinn.FabricaCreditos.Entities.PresupuestoFamiliar();

            if (idObjeto != "")
                vPresupuestoFamiliar = PresupuestoFamiliarServicio.ConsultarPresupuestoFamiliar(Convert.ToInt64(idObjeto), (Usuario)Session["usuario"]);

            if (txtCod_presupuesto.Text != "") vPresupuestoFamiliar.cod_presupuesto = Convert.ToInt64(txtCod_presupuesto.Text.Trim());
            if (Session["CodCodeudor"] == null)
            {
                VerError("No se pudo determinar el código del codeudor");
            }
            vPresupuestoFamiliar.cod_persona = Convert.ToInt64(Session["CodCodeudor"].ToString());
            if (txtActividadprincipal.Text != "") vPresupuestoFamiliar.actividadprincipal = Convert.ToInt64(txtActividadprincipal.Text.Trim().Replace(".", ""));
            if (txtConyuge.Text != "") vPresupuestoFamiliar.conyuge = Convert.ToInt64(txtConyuge.Text.Trim().Replace(".", ""));
            if (txtOtrosingresos.Text != "") vPresupuestoFamiliar.otrosingresos = Convert.ToInt64(txtOtrosingresos.Text.Trim().Replace(".", ""));
            if (txtConsumofamiliar.Text != "") vPresupuestoFamiliar.consumofamiliar = Convert.ToInt64(txtConsumofamiliar.Text.Trim().Replace(".", ""));
            if (txtObligaciones.Text != "") vPresupuestoFamiliar.obligaciones = Convert.ToInt64(txtObligaciones.Text.Trim().Replace(".", ""));
            if (txtExcedente.Text != "") vPresupuestoFamiliar.excedente = Convert.ToInt64(txtExcedente.Text.Trim().Replace(".", ""));

            if (idObjeto != "")
            {
                vPresupuestoFamiliar.cod_presupuesto = Convert.ToInt64(idObjeto);
                PresupuestoFamiliarServicio.ModificarPresupuestoFamiliar(vPresupuestoFamiliar, (Usuario)Session["usuario"]);
            }
            else
            {
                vPresupuestoFamiliar = PresupuestoFamiliarServicio.CrearPresupuestoFamiliar(vPresupuestoFamiliar, (Usuario)Session["usuario"]);
                idObjeto = vPresupuestoFamiliar.cod_presupuesto.ToString();
            }

            Session[PresupuestoFamiliarServicio.CodigoPrograma + ".id"] = idObjeto;
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(PresupuestoFamiliarServicio.CodigoPrograma, "btnGuardar_Click", ex);
        }
        Actualizar();
     //   Borrar();
    }

}