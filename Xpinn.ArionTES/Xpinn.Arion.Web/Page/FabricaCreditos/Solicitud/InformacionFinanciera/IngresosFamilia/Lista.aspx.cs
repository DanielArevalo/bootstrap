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
    private Xpinn.FabricaCreditos.Services.IngresosFamiliaService IngresosFamiliaServicio = new Xpinn.FabricaCreditos.Services.IngresosFamiliaService();
    
    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(IngresosFamiliaServicio.CodigoPrograma, "L");
            Site2 toolBar = (Site2)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoGuardar += btnGuardar_Click;

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(IngresosFamiliaServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                CargarValoresConsulta(pConsulta, IngresosFamiliaServicio.CodigoPrograma);
                //CargarListas();

                if (Session[IngresosFamiliaServicio.GetType().Name + ".consulta"] != null)
                    Actualizar();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(IngresosFamiliaServicio.CodigoPrograma, "Page_Load", ex);
        }
    }

    protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    {
        GuardarValoresConsulta(pConsulta, IngresosFamiliaServicio.CodigoPrograma);
        //Navegar(Pagina.Nuevo);
        Borrar();
    }

    private void Borrar()
    {
        //CargarListas();
        txtNegocio.Text = "";
        txtConyuge.Text = "";
        txtHijos.Text = "";
        txtArriendos.Text = "";
        txtPension.Text = "";
        txtOtros.Text = "";
        idObjeto = "";
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        GuardarValoresConsulta(pConsulta, IngresosFamiliaServicio.CodigoPrograma);
        Actualizar();
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        LimpiarValoresConsulta(pConsulta, IngresosFamiliaServicio.CodigoPrograma);
    }

    protected void gvLista_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            ConfirmarEliminarFila(e, "btnBorrar");
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(IngresosFamiliaServicio.CodigoPrograma + "L", "gvLista_RowDataBound", ex);
        }
    }

    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        //String id = gvLista.DataKeys[gvLista.SelectedRow.RowIndex].Value.ToString();
        String id = gvLista.SelectedRow.Cells[0].Text;       
        Session[IngresosFamiliaServicio.CodigoPrograma + ".id"] = id;
        Edicion();
        //Navegar(Pagina.Detalle);
    }

    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvLista.Rows[e.NewEditIndex].Cells[0].Text;//gvLista.DataKeys[gvLista.SelectedRow.RowIndex].Value.ToString();
        Session[IngresosFamiliaServicio.CodigoPrograma + ".id"] = id;
        Navegar(Pagina.Editar);
    }

    protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            long id = Convert.ToInt64(gvLista.Rows[e.RowIndex].Cells[0].Text);//Convert.ToInt64(gvLista.DataKeys[gvLista.SelectedRow.RowIndex].Value);
            IngresosFamiliaServicio.EliminarIngresosFamilia(id, (Usuario)Session["usuario"]);
            Actualizar();
        }
        catch (Xpinn.Util.ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(IngresosFamiliaServicio.CodigoPrograma, "gvLista_RowDeleting", ex);
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
            BOexcepcion.Throw(IngresosFamiliaServicio.CodigoPrograma, "gvLista_PageIndexChanging", ex);
        }
    }

    private void Actualizar()
    {
        try
        {
            List<Xpinn.FabricaCreditos.Entities.IngresosFamilia> lstConsulta = new List<Xpinn.FabricaCreditos.Entities.IngresosFamilia>();
            lstConsulta = IngresosFamiliaServicio.ListarIngresosFamilia(ObtenerValores(), (Usuario)Session["usuario"]);

            gvLista.PageSize = 15;
            gvLista.EmptyDataText = "";
            gvLista.DataSource = lstConsulta;

            if (lstConsulta.Count > 0)
            {
                Int64 sumaIngresos = lstConsulta.Sum(item => item.negocio) + lstConsulta.Sum(item => item.conyuge) + lstConsulta.Sum(item => item.hijos) + lstConsulta.Sum(item => item.arriendos) + lstConsulta.Sum(item => item.pension) + lstConsulta.Sum(item => item.otros);
                txtTotalIngresos.Text = sumaIngresos.ToString();
                gvLista.Visible = true;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                gvLista.DataBind();
                ValidarPermisosGrilla(gvLista);
            }
            else
            {
                txtTotalIngresos.Text = "0";
                gvLista.Visible = false;
                lblTotalRegs.Visible = false;
            }
            Session.Add(IngresosFamiliaServicio.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(IngresosFamiliaServicio.CodigoPrograma, "Actualizar", ex);
        }
        //Actualizar();
    }

    private Xpinn.FabricaCreditos.Entities.IngresosFamilia ObtenerValores()
    {
        Xpinn.FabricaCreditos.Entities.IngresosFamilia vIngresosFamilia = new Xpinn.FabricaCreditos.Entities.IngresosFamilia();

    if(txtIngresos.Text.Trim() != "")
        vIngresosFamilia.ingresos = Convert.ToInt64(txtIngresos.Text.Trim().Replace(@".", ""));
    if(txtNegocio.Text.Trim() != "")
        vIngresosFamilia.negocio = Convert.ToInt64(txtNegocio.Text.Trim().Replace(@".", ""));
    if(txtConyuge.Text.Trim() != "")
        vIngresosFamilia.conyuge = Convert.ToInt64(txtConyuge.Text.Trim().Replace(@".", ""));
    if(txtHijos.Text.Trim() != "")
        vIngresosFamilia.hijos = Convert.ToInt64(txtHijos.Text.Trim().Replace(@".", ""));
    if(txtArriendos.Text.Trim() != "")
        vIngresosFamilia.arriendos = Convert.ToInt64(txtArriendos.Text.Trim().Replace(@".", ""));
    if(txtPension.Text.Trim() != "")
        vIngresosFamilia.pension = Convert.ToInt64(txtPension.Text.Trim().Replace(@".", ""));
    if(txtOtros.Text.Trim() != "")
        vIngresosFamilia.otros = Convert.ToInt64(txtOtros.Text.Trim().Replace(@".", ""));
    if(txtCod_persona.Text.Trim() != "")
        vIngresosFamilia.cod_persona = Convert.ToInt64(txtCod_persona.Text.Trim());

        return vIngresosFamilia;
    }





    private void Edicion()
    {
        try
        {
            if (Session[IngresosFamiliaServicio.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(IngresosFamiliaServicio.CodigoPrograma, "E");
            else
                VisualizarOpciones(IngresosFamiliaServicio.CodigoPrograma, "A");

            //Page_Load
            if (Session[IngresosFamiliaServicio.CodigoPrograma + ".id"] != null)
            {
                idObjeto = Session[IngresosFamiliaServicio.CodigoPrograma + ".id"].ToString();
                if (idObjeto != null)
                {
                    ObtenerDatos(idObjeto);
                    Session.Remove(IngresosFamiliaServicio.CodigoPrograma + ".id");
                }

            }
            //else
            //{
            //    CargarListas();
            //}
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(IngresosFamiliaServicio.GetType().Name + "A", "Page_PreInit", ex);
        }
    }


    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            Xpinn.FabricaCreditos.Entities.IngresosFamilia vIngresosFamilia = new Xpinn.FabricaCreditos.Entities.IngresosFamilia();

            if (idObjeto != "")
                vIngresosFamilia = IngresosFamiliaServicio.ConsultarIngresosFamilia(Convert.ToInt64(idObjeto), (Usuario)Session["usuario"]);

            if (txtCod_ingreso.Text != "") vIngresosFamilia.cod_ingreso = Convert.ToInt64(txtCod_ingreso.Text.Trim().Replace(@".", ""));
            if (txtIngresos.Text != "") vIngresosFamilia.ingresos = Convert.ToInt64(txtIngresos.Text.Trim().Replace(@".", ""));
            if (txtNegocio.Text != "") vIngresosFamilia.negocio = Convert.ToInt64(txtNegocio.Text.Trim().Replace(@".", ""));
            if (txtConyuge.Text != "") vIngresosFamilia.conyuge = Convert.ToInt64(txtConyuge.Text.Trim().Replace(@".", ""));
            if (txtHijos.Text != "") vIngresosFamilia.hijos = Convert.ToInt64(txtHijos.Text.Trim().Replace(@".", ""));
            if (txtArriendos.Text != "") vIngresosFamilia.arriendos = Convert.ToInt64(txtArriendos.Text.Trim().Replace(@".", ""));
            if (txtPension.Text != "") vIngresosFamilia.pension = Convert.ToInt64(txtPension.Text.Trim().Replace(@".", ""));
            if (txtOtros.Text != "") vIngresosFamilia.otros = Convert.ToInt64(txtOtros.Text.Trim().Replace(@".", ""));
            if (txtCod_persona.Text != "") vIngresosFamilia.cod_persona = Convert.ToInt64(txtCod_persona.Text.Trim());

            if (idObjeto != "")
            {
                vIngresosFamilia.cod_ingreso = Convert.ToInt64(idObjeto);
                IngresosFamiliaServicio.ModificarIngresosFamilia(vIngresosFamilia, (Usuario)Session["usuario"]);
            }
            else
            {
                vIngresosFamilia = IngresosFamiliaServicio.CrearIngresosFamilia(vIngresosFamilia, (Usuario)Session["usuario"]);
                idObjeto = vIngresosFamilia.cod_ingreso.ToString();
            }

            Session[IngresosFamiliaServicio.CodigoPrograma + ".id"] = idObjeto;
            //Navegar(Pagina.Detalle);
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(IngresosFamiliaServicio.CodigoPrograma, "btnGuardar_Click", ex);
        }
        Borrar();
        Actualizar();
    }

    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            Xpinn.FabricaCreditos.Entities.IngresosFamilia vIngresosFamilia = new Xpinn.FabricaCreditos.Entities.IngresosFamilia();
            vIngresosFamilia = IngresosFamiliaServicio.ConsultarIngresosFamilia(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);

            if (vIngresosFamilia.cod_ingreso != Int64.MinValue)
                txtCod_ingreso.Text = HttpUtility.HtmlDecode(vIngresosFamilia.cod_ingreso.ToString().Trim());
            if (vIngresosFamilia.ingresos != Int64.MinValue)
                txtIngresos.Text = HttpUtility.HtmlDecode(vIngresosFamilia.ingresos.ToString().Trim());
            if (vIngresosFamilia.negocio != Int64.MinValue)
                txtNegocio.Text = HttpUtility.HtmlDecode(vIngresosFamilia.negocio.ToString().Trim());
            if (vIngresosFamilia.conyuge != Int64.MinValue)
                txtConyuge.Text = HttpUtility.HtmlDecode(vIngresosFamilia.conyuge.ToString().Trim());
            if (vIngresosFamilia.hijos != Int64.MinValue)
                txtHijos.Text = HttpUtility.HtmlDecode(vIngresosFamilia.hijos.ToString().Trim());
            if (vIngresosFamilia.arriendos != Int64.MinValue)
                txtArriendos.Text = HttpUtility.HtmlDecode(vIngresosFamilia.arriendos.ToString().Trim());
            if (vIngresosFamilia.pension != Int64.MinValue)
                txtPension.Text = HttpUtility.HtmlDecode(vIngresosFamilia.pension.ToString().Trim());
            if (vIngresosFamilia.otros != Int64.MinValue)
                txtOtros.Text = HttpUtility.HtmlDecode(vIngresosFamilia.otros.ToString().Trim());
            if (vIngresosFamilia.cod_persona != Int64.MinValue)
                txtCod_persona.Text = HttpUtility.HtmlDecode(vIngresosFamilia.cod_persona.ToString().Trim());
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(IngresosFamiliaServicio.CodigoPrograma, "ObtenerDatos", ex);
        }
    }



    private Xpinn.FabricaCreditos.Entities.EstadosFinancieros ObtenerEstadosFinancieros()
    {
        Xpinn.FabricaCreditos.Entities.EstadosFinancieros vEstadosFinancieros = new Xpinn.FabricaCreditos.Entities.EstadosFinancieros();
        vEstadosFinancieros.cod_inffin = 18;
        vEstadosFinancieros.filtro = "UtiNet";
        return vEstadosFinancieros;
    }
}