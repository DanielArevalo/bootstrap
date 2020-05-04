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
    private Xpinn.FabricaCreditos.Services.BienesRaicesService BienesRaicesServicio = new Xpinn.FabricaCreditos.Services.BienesRaicesService();

  

        protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            VisualizarOpciones(BienesRaicesServicio.CodigoPrograma, "L");
                    
            

            Site2 toolBar = (Site2)this.Master;
            //toolBar.eventoNuevo += btnNuevo_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoGuardar += btnGuardar_Click;
            //toolBar.eventoLimpiar += btnLimpiar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(BienesRaicesServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                CargarValoresConsulta(pConsulta, BienesRaicesServicio.CodigoPrograma);
                //CargarListas();

                if (Session[BienesRaicesServicio.GetType().Name + ".consulta"] != null)
                    Actualizar();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(BienesRaicesServicio.GetType().Name, "Page_Load", ex);
        }
    }

    //protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    //{
    //    GuardarValoresConsulta(pConsulta, BienesRaicesServicio.CodigoPrograma);
    //    Borrar();
    //    //Navegar(Pagina.Nuevo);
    //}

    private void Borrar()
    {
        
        //txtCod_bien.Text = "";        
        //txtTipo.Text = "";
        txtMatricula.Text = "";
        txtValorcomercial.Text = "";
        txtValorhipoteca.Text = "";      

        idObjeto = "";
    } 
    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        GuardarValoresConsulta(pConsulta, BienesRaicesServicio.CodigoPrograma);
        Actualizar();
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        LimpiarValoresConsulta(pConsulta, BienesRaicesServicio.CodigoPrograma);
    }

    protected void gvLista_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            ConfirmarEliminarFila(e, "btnBorrar");
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(BienesRaicesServicio.CodigoPrograma + "L", "gvLista_RowDataBound", ex);
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {                    
            switch (e.Row.Cells[4].Text)
            {
                case "1":
                    e.Row.Cells[4].Text = "t1";
                    break;
                case "2":
                    e.Row.Cells[4].Text = "t2";
                    break;
                case "3":
                    e.Row.Cells[4].Text = "t3";
                    break;
            }
        }
    }

    

    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        //String id = gvLista.DataKeys[gvLista.SelectedRow.RowIndex].Value.ToString();
        String id = gvLista.SelectedRow.Cells[0].Text;        
        Session[BienesRaicesServicio.CodigoPrograma + ".id"] = id;
        //Navegar(Pagina.Detalle);
        Edicion();
    }

    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvLista.Rows[e.NewEditIndex].Cells[0].Text;//gvLista.DataKeys[gvLista.SelectedRow.RowIndex].Value.ToString();
        Session[BienesRaicesServicio.CodigoPrograma + ".id"] = id;
        Navegar(Pagina.Editar);
    }

    protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            long id = Convert.ToInt64(gvLista.Rows[e.RowIndex].Cells[0].Text);
            BienesRaicesServicio.EliminarBienesRaices(id, (Usuario)Session["usuario"]);
            Actualizar();
        }
        catch (Xpinn.Util.ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(BienesRaicesServicio.CodigoPrograma, "gvLista_RowDeleting", ex);
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
            BOexcepcion.Throw(BienesRaicesServicio.CodigoPrograma, "gvLista_PageIndexChanging", ex);
        }
    }

    private void Actualizar()
    {
        try
        {
            List<Xpinn.FabricaCreditos.Entities.BienesRaices> lstConsulta = new List<Xpinn.FabricaCreditos.Entities.BienesRaices>();
            lstConsulta = BienesRaicesServicio.ListarBienesRaices(ObtenerValores(), (Usuario)Session["usuario"]);

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

            Session.Add(BienesRaicesServicio.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(BienesRaicesServicio.CodigoPrograma, "Actualizar", ex);
        }
    }

    private Xpinn.FabricaCreditos.Entities.BienesRaices ObtenerValores()
    {
        Xpinn.FabricaCreditos.Entities.BienesRaices vBienesRaices = new Xpinn.FabricaCreditos.Entities.BienesRaices();

        vBienesRaices.cod_persona = Convert.ToInt64(Session["Cod_persona"].ToString());
    //if(txtTipo.Text.Trim() != "")
        vBienesRaices.tipo = txttipo.Text;
    if(txtMatricula.Text.Trim() != "")
        vBienesRaices.matricula = Convert.ToString(txtMatricula.Text.Trim());
    if(txtValorcomercial.Text.Trim() != "")
        vBienesRaices.valorcomercial = Convert.ToInt64(txtValorcomercial.Text.Trim().Replace(@".",""));
    if(txtValorhipoteca.Text.Trim() != "")
        vBienesRaices.valorhipoteca = Convert.ToInt64(txtValorhipoteca.Text.Trim().Replace(@".", ""));

        return vBienesRaices;
    }


    private void Edicion()
    {
        try
        {
            if (Session[BienesRaicesServicio.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(BienesRaicesServicio.CodigoPrograma, "E");
            else
                VisualizarOpciones(BienesRaicesServicio.CodigoPrograma, "A");

            //Page_Load
            if (Session[BienesRaicesServicio.CodigoPrograma + ".id"] != null)
            {
                idObjeto = Session[BienesRaicesServicio.CodigoPrograma + ".id"].ToString();
                if (idObjeto != null)
                {
                    ObtenerDatos(idObjeto);
                    Session.Remove(BienesRaicesServicio.CodigoPrograma + ".id");
                }

            }
            //else
            //{
            //    CargarListas();
            //}
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(BienesRaicesServicio.GetType().Name + "A", "Page_PreInit", ex);
        }
    }


    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            Xpinn.FabricaCreditos.Entities.BienesRaices vBienesRaices = new Xpinn.FabricaCreditos.Entities.BienesRaices();

            if (idObjeto != "")
                vBienesRaices = BienesRaicesServicio.ConsultarBienesRaices(Convert.ToInt64(idObjeto), (Usuario)Session["usuario"]);

            if (txtCod_bien.Text != "") vBienesRaices.cod_bien = Convert.ToInt64(txtCod_bien.Text.Trim());
            vBienesRaices.cod_persona = Convert.ToInt64(Session["Cod_persona"].ToString());

            vBienesRaices.tipo = txttipo.Text;
            //if (txtMatricula.Text != "") vBienesRaices.matricula = Convert.ToString(txtMatricula.Text.Trim());
            vBienesRaices.matricula = (txtMatricula.Text != "") ? Convert.ToString(txtMatricula.Text.Trim()) : String.Empty;
            if (txtValorcomercial.Text != "") vBienesRaices.valorcomercial = Convert.ToInt64(txtValorcomercial.Text.Trim().Replace(".",""));
            if (txtValorhipoteca.Text != "") vBienesRaices.valorhipoteca = Convert.ToInt64(txtValorhipoteca.Text.Trim().Replace(".", ""));

            if (idObjeto != "")
            {
                vBienesRaices.cod_bien = Convert.ToInt64(idObjeto);
                BienesRaicesServicio.ModificarBienesRaices(vBienesRaices, (Usuario)Session["usuario"]);
            }
            else
            {
                vBienesRaices = BienesRaicesServicio.CrearBienesRaices(vBienesRaices, (Usuario)Session["usuario"]);
                idObjeto = vBienesRaices.cod_bien.ToString();
            }

            Session[BienesRaicesServicio.CodigoPrograma + ".id"] = idObjeto;
            //Navegar(Pagina.Detalle);
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(BienesRaicesServicio.CodigoPrograma, "btnGuardar_Click", ex);
        }
        Actualizar();
        Borrar();
    }

    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            Xpinn.FabricaCreditos.Entities.BienesRaices vBienesRaices = new Xpinn.FabricaCreditos.Entities.BienesRaices();
            vBienesRaices = BienesRaicesServicio.ConsultarBienesRaices(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);

            if (vBienesRaices.cod_bien != Int64.MinValue)
                txtCod_bien.Text = HttpUtility.HtmlDecode(vBienesRaices.cod_bien.ToString().Trim());    
            if (vBienesRaices.tipo != null)
                txttipo.Text = HttpUtility.HtmlDecode(vBienesRaices.tipo.ToString().Trim());
            if (!string.IsNullOrEmpty(vBienesRaices.matricula))
                txtMatricula.Text = HttpUtility.HtmlDecode(vBienesRaices.matricula.ToString().Trim());
            if (vBienesRaices.valorcomercial != Int64.MinValue)
                txtValorcomercial.Text = HttpUtility.HtmlDecode(vBienesRaices.valorcomercial.ToString().Trim());
            if (vBienesRaices.valorhipoteca != Int64.MinValue)
                txtValorhipoteca.Text = HttpUtility.HtmlDecode(vBienesRaices.valorhipoteca.ToString().Trim());
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(BienesRaicesServicio.CodigoPrograma, "ObtenerDatos", ex);
        }
    }
}