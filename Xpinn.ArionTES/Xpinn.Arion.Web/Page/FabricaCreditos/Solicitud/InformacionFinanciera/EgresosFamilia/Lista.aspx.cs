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
    private Xpinn.FabricaCreditos.Services.EgresosFamiliaService EgresosFamiliaServicio = new Xpinn.FabricaCreditos.Services.EgresosFamiliaService();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(EgresosFamiliaServicio.CodigoPrograma, "L");

            Site2 toolBar = (Site2)this.Master;
            //toolBar.eventoNuevo += btnNuevo_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
            //toolBar.eventoLimpiar += btnLimpiar_Click;
            toolBar.eventoGuardar += btnGuardar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(EgresosFamiliaServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                CargarValoresConsulta(pConsulta, EgresosFamiliaServicio.CodigoPrograma);
                //CargarListas();

                if (Session[EgresosFamiliaServicio.GetType().Name + ".consulta"] != null)
                    Actualizar();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(EgresosFamiliaServicio.CodigoPrograma, "Page_Load", ex);
        }
    }

    protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    {
        GuardarValoresConsulta(pConsulta, EgresosFamiliaServicio.CodigoPrograma);
        //Navegar(Pagina.Nuevo);
        Borrar();
    }

    private void Borrar()
    {
        //CargarListas();
        txtPagodeudas.Text = "";
        txtAlimentacion.Text = "";
        txtVivienda.Text = "";
        txtEducacion.Text = "";
        txtServiciospublicos.Text = "";
        txtTransporte.Text = "";
        txtOtros.Text = "";   
        idObjeto = "";
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        GuardarValoresConsulta(pConsulta, EgresosFamiliaServicio.CodigoPrograma);
        Actualizar();
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        LimpiarValoresConsulta(pConsulta, EgresosFamiliaServicio.CodigoPrograma);
    }

    protected void gvLista_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            ConfirmarEliminarFila(e, "btnBorrar");
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(EgresosFamiliaServicio.CodigoPrograma + "L", "gvLista_RowDataBound", ex);
        }
    }

    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        String id = gvLista.DataKeys[gvLista.SelectedRow.RowIndex].Value.ToString();
        Session[EgresosFamiliaServicio.CodigoPrograma + ".id"] = id;
        //Navegar(Pagina.Detalle);
        Edicion();
    }

    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvLista.Rows[e.NewEditIndex].Cells[0].Text;//gvLista.DataKeys[gvLista.SelectedRow.RowIndex].Value.ToString();
        Session[EgresosFamiliaServicio.CodigoPrograma + ".id"] = id;
        //Navegar(Pagina.Editar);
    }

    protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            long id = Convert.ToInt64(gvLista.Rows[e.RowIndex].Cells[0].Text);
            EgresosFamiliaServicio.EliminarEgresosFamilia(id, (Usuario)Session["usuario"]);
            Actualizar();
        }
        catch (Xpinn.Util.ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(EgresosFamiliaServicio.CodigoPrograma, "gvLista_RowDeleting", ex);
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
            BOexcepcion.Throw(EgresosFamiliaServicio.CodigoPrograma, "gvLista_PageIndexChanging", ex);
        }
    }

    private void Actualizar()
    {
        try
        {
            List<Xpinn.FabricaCreditos.Entities.EgresosFamilia> lstConsulta = new List<Xpinn.FabricaCreditos.Entities.EgresosFamilia>();
            lstConsulta = EgresosFamiliaServicio.ListarEgresosFamilia(ObtenerValores(), (Usuario)Session["usuario"]);

            gvLista.PageSize = 15;
            gvLista.EmptyDataText = "";
            gvLista.DataSource = lstConsulta;

            if (lstConsulta.Count > 0)
            {
                Int64 sumaEgresos = lstConsulta.Sum(item => item.pagodeudas) + lstConsulta.Sum(item => item.alimentacion) + lstConsulta.Sum(item => item.vivienda) + lstConsulta.Sum(item => item.educacion) + lstConsulta.Sum(item => item.serviciospublicos) + lstConsulta.Sum(item => item.transporte) + lstConsulta.Sum(item => item.otros);
                txtTotalEgresos.Text = sumaEgresos.ToString();
                
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

            Session.Add(EgresosFamiliaServicio.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(EgresosFamiliaServicio.CodigoPrograma, "Actualizar", ex);
        }
    }

    private Xpinn.FabricaCreditos.Entities.EgresosFamilia ObtenerValores()
    {
        Xpinn.FabricaCreditos.Entities.EgresosFamilia vEgresosFamilia = new Xpinn.FabricaCreditos.Entities.EgresosFamilia();


        vEgresosFamilia.cod_persona = Convert.ToInt64(Session["Cod_persona"].ToString());
    if(txtEgresos.Text.Trim() != "")
        vEgresosFamilia.egresos = Convert.ToInt64(txtEgresos.Text.Trim().Replace(@".", ""));
    if(txtAlimentacion.Text.Trim() != "")
        vEgresosFamilia.alimentacion = Convert.ToInt64(txtAlimentacion.Text.Trim().Replace(@".", ""));
    if(txtVivienda.Text.Trim() != "")
        vEgresosFamilia.vivienda = Convert.ToInt64(txtVivienda.Text.Trim().Replace(@".", ""));
    if(txtEducacion.Text.Trim() != "")
        vEgresosFamilia.educacion = Convert.ToInt64(txtEducacion.Text.Trim().Replace(@".", ""));
    if(txtServiciospublicos.Text.Trim() != "")
        vEgresosFamilia.serviciospublicos = Convert.ToInt64(txtServiciospublicos.Text.Trim().Replace(@".", ""));
    if(txtTransporte.Text.Trim() != "")
        vEgresosFamilia.transporte = Convert.ToInt64(txtTransporte.Text.Trim().Replace(@".", "")); 
    if(txtPagodeudas.Text.Trim() != "")
        vEgresosFamilia.pagodeudas = Convert.ToInt64(txtPagodeudas.Text.Trim().Replace(@".", ""));
    if(txtOtros.Text.Trim() != "")
        vEgresosFamilia.otros = Convert.ToInt64(txtOtros.Text.Trim().Replace(@".", ""));

        return vEgresosFamilia;
    }


    private void Edicion()
    {
        try
        {
            if (Session[EgresosFamiliaServicio.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(EgresosFamiliaServicio.CodigoPrograma, "E");
            else
                VisualizarOpciones(EgresosFamiliaServicio.CodigoPrograma, "A");

            //Page_Load
            if (Session[EgresosFamiliaServicio.CodigoPrograma + ".id"] != null)
            {
                idObjeto = Session[EgresosFamiliaServicio.CodigoPrograma + ".id"].ToString();
                if (idObjeto != null)
                {
                    ObtenerDatos(idObjeto);
                    Session.Remove(EgresosFamiliaServicio.CodigoPrograma + ".id");
                }

            }
            //else
            //{
            //    CargarListas();
            //}
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(EgresosFamiliaServicio.GetType().Name + "A", "Page_PreInit", ex);
        }
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            Xpinn.FabricaCreditos.Entities.EgresosFamilia vEgresosFamilia = new Xpinn.FabricaCreditos.Entities.EgresosFamilia();

            if (idObjeto != "")
                vEgresosFamilia = EgresosFamiliaServicio.ConsultarEgresosFamilia(Convert.ToInt64(idObjeto), (Usuario)Session["usuario"]);

            if (txtCod_egreso.Text != "") vEgresosFamilia.cod_egreso = Convert.ToInt64(txtCod_egreso.Text.Trim());
            if (txtCod_persona.Text != "") vEgresosFamilia.cod_persona = Convert.ToInt64(txtCod_persona.Text.Trim());
            if (txtEgresos.Text != "") vEgresosFamilia.egresos = Convert.ToInt64(txtEgresos.Text.Trim());
            if (txtAlimentacion.Text != "") vEgresosFamilia.alimentacion = Convert.ToInt64(txtAlimentacion.Text.Trim().Replace(@".", ""));
            if (txtVivienda.Text != "") vEgresosFamilia.vivienda = Convert.ToInt64(txtVivienda.Text.Trim().Replace(@".", ""));
            if (txtEducacion.Text != "") vEgresosFamilia.educacion = Convert.ToInt64(txtEducacion.Text.Trim().Replace(@".", ""));
            if (txtServiciospublicos.Text != "") vEgresosFamilia.serviciospublicos = Convert.ToInt64(txtServiciospublicos.Text.Trim().Replace(@".", ""));
            if (txtTransporte.Text != "") vEgresosFamilia.transporte = Convert.ToInt64(txtTransporte.Text.Trim().Replace(@".", ""));
            if (txtPagodeudas.Text != "") vEgresosFamilia.pagodeudas = Convert.ToInt64(txtPagodeudas.Text.Trim().Replace(@".", ""));
            if (txtOtros.Text != "") vEgresosFamilia.otros = Convert.ToInt64(txtOtros.Text.Trim().Replace(@".", ""));

            if (idObjeto != "")
            {
                vEgresosFamilia.cod_egreso = Convert.ToInt64(idObjeto);
                EgresosFamiliaServicio.ModificarEgresosFamilia(vEgresosFamilia, (Usuario)Session["usuario"]);
            }
            else
            {
                vEgresosFamilia = EgresosFamiliaServicio.CrearEgresosFamilia(vEgresosFamilia, (Usuario)Session["usuario"]);
                idObjeto = vEgresosFamilia.cod_egreso.ToString();
            }

            Session[EgresosFamiliaServicio.CodigoPrograma + ".id"] = idObjeto;
            //Navegar(Pagina.Detalle);
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(EgresosFamiliaServicio.CodigoPrograma, "btnGuardar_Click", ex);
        }
        Borrar();
        Actualizar();
    }


    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            Xpinn.FabricaCreditos.Entities.EgresosFamilia vEgresosFamilia = new Xpinn.FabricaCreditos.Entities.EgresosFamilia();
            vEgresosFamilia = EgresosFamiliaServicio.ConsultarEgresosFamilia(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);

            if (vEgresosFamilia.cod_egreso != Int64.MinValue)
                txtCod_egreso.Text = HttpUtility.HtmlDecode(vEgresosFamilia.cod_egreso.ToString().Trim());
            if (vEgresosFamilia.cod_persona != Int64.MinValue)
                txtCod_persona.Text = HttpUtility.HtmlDecode(vEgresosFamilia.cod_persona.ToString().Trim());
            if (vEgresosFamilia.egresos != Int64.MinValue)
                txtEgresos.Text = HttpUtility.HtmlDecode(vEgresosFamilia.egresos.ToString().Trim());
            if (vEgresosFamilia.alimentacion != Int64.MinValue)
                txtAlimentacion.Text = HttpUtility.HtmlDecode(vEgresosFamilia.alimentacion.ToString().Trim());
            if (vEgresosFamilia.vivienda != Int64.MinValue)
                txtVivienda.Text = HttpUtility.HtmlDecode(vEgresosFamilia.vivienda.ToString().Trim());
            if (vEgresosFamilia.educacion != Int64.MinValue)
                txtEducacion.Text = HttpUtility.HtmlDecode(vEgresosFamilia.educacion.ToString().Trim());
            if (vEgresosFamilia.serviciospublicos != Int64.MinValue)
                txtServiciospublicos.Text = HttpUtility.HtmlDecode(vEgresosFamilia.serviciospublicos.ToString().Trim());
            if (vEgresosFamilia.transporte != Int64.MinValue)
                txtTransporte.Text = HttpUtility.HtmlDecode(vEgresosFamilia.transporte.ToString().Trim());
            if (vEgresosFamilia.pagodeudas != Int64.MinValue)
                txtPagodeudas.Text = HttpUtility.HtmlDecode(vEgresosFamilia.pagodeudas.ToString().Trim());
            if (vEgresosFamilia.otros != Int64.MinValue)
                txtOtros.Text = HttpUtility.HtmlDecode(vEgresosFamilia.otros.ToString().Trim());
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(EgresosFamiliaServicio.CodigoPrograma, "ObtenerDatos", ex);
        }
    }
}