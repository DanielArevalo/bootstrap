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
    private Xpinn.FabricaCreditos.Services.InventarioMateriaPrimaService InventarioMateriaPrimaServicio = new Xpinn.FabricaCreditos.Services.InventarioMateriaPrimaService();
    private Xpinn.FabricaCreditos.Services.InformacionFinancieraService InformacionFinancieraServicio = new Xpinn.FabricaCreditos.Services.InformacionFinancieraService();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(InventarioMateriaPrimaServicio.CodigoPrograma, "L");

            Site2 toolBar = (Site2)this.Master;
            //toolBar.eventoNuevo += btnNuevo_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
            //toolBar.eventoLimpiar += btnLimpiar_Click;
            toolBar.eventoGuardar += btnGuardar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(InventarioMateriaPrimaServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                CargarValoresConsulta(pConsulta, InventarioMateriaPrimaServicio.CodigoPrograma);
                //CargarListas();

                if (Session[InventarioMateriaPrimaServicio.GetType().Name + ".consulta"] != null)
                    Actualizar();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(InventarioMateriaPrimaServicio.GetType().Name, "Page_Load", ex);
        }
    }

    //protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    //{
    //    GuardarValoresConsulta(pConsulta, InventarioMateriaPrimaServicio.CodigoPrograma);
    //    Borrar();
    //    //Navegar(Pagina.Nuevo);
    //}

    private void Borrar()
    {
        //CargarListas();
        txtDescripcion.Text = "";
        txtValor.Text = "";      
        idObjeto = "";
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        GuardarValoresConsulta(pConsulta, InventarioMateriaPrimaServicio.CodigoPrograma);
        Actualizar();
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        LimpiarValoresConsulta(pConsulta, InventarioMateriaPrimaServicio.CodigoPrograma);
    }

    protected void gvLista_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            ConfirmarEliminarFila(e, "btnBorrar");
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(InventarioMateriaPrimaServicio.CodigoPrograma + "L", "gvLista_RowDataBound", ex);
        }
    }

    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        //String id = gvLista.DataKeys[gvLista.SelectedRow.RowIndex].Value.ToString();
        String id = gvLista.SelectedRow.Cells[0].Text;
        Session[InventarioMateriaPrimaServicio.CodigoPrograma + ".id"] = id;
        //Navegar(Pagina.Detalle);
        Edicion();
    }

    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvLista.Rows[e.NewEditIndex].Cells[0].Text;//gvLista.DataKeys[gvLista.SelectedRow.RowIndex].Value.ToString();
        Session[InventarioMateriaPrimaServicio.CodigoPrograma + ".id"] = id;
        Navegar(Pagina.Editar);
    }

    protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            Int64 Cod_persona = Convert.ToInt64(Session["Cod_persona"].ToString());
            Int64 Cod_InfFin = Convert.ToInt64(Session["Cod_InfFin"].ToString());
            long id = Convert.ToInt64(gvLista.Rows[e.RowIndex].Cells[0].Text);
            InventarioMateriaPrimaServicio.EliminarInventarioMateriaPrima(id, (Usuario)Session["usuario"], Cod_persona, Cod_InfFin);
            Actualizar();
        }
        catch (Xpinn.Util.ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(InventarioMateriaPrimaServicio.CodigoPrograma, "gvLista_RowDeleting", ex);
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
            BOexcepcion.Throw(InventarioMateriaPrimaServicio.CodigoPrograma, "gvLista_PageIndexChanging", ex);
        }
    }

    private void Actualizar()
    {
        try
        {
            List<Xpinn.FabricaCreditos.Entities.InventarioMateriaPrima> lstConsulta = new List<Xpinn.FabricaCreditos.Entities.InventarioMateriaPrima>();
            //Xpinn.FabricaCreditos.Entities.InventarioMateriaPrima entTotales = new Xpinn.FabricaCreditos.Entities.InventarioMateriaPrima(); // Entidad donde se devuelven los calculos
            lstConsulta = InventarioMateriaPrimaServicio.ListarInventarioMateriaPrima(ObtenerValores(), (Usuario)Session["usuario"]);
            
            gvLista.PageSize = 15;
            gvLista.EmptyDataText = "";
            gvLista.DataSource = lstConsulta;

            if (lstConsulta.Count > 0)
            {
                double total = lstConsulta.Sum(item => item.valor);
                txtTotalMateriaPrima.Text = total.ToString();

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

            Session.Add(InventarioMateriaPrimaServicio.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(InventarioMateriaPrimaServicio.CodigoPrograma, "Actualizar", ex);
        }
    }

    private Xpinn.FabricaCreditos.Entities.InventarioMateriaPrima ObtenerValores()
    {
        Xpinn.FabricaCreditos.Entities.InventarioMateriaPrima vInventarioMateriaPrima = new Xpinn.FabricaCreditos.Entities.InventarioMateriaPrima();

        vInventarioMateriaPrima.cod_persona = Convert.ToInt64(Session["Cod_persona"].ToString());
        vInventarioMateriaPrima.cod_inffin = Convert.ToInt64(Session["Cod_InfFin"].ToString());
    if(txtDescripcion.Text.Trim() != "")
        vInventarioMateriaPrima.descripcion = Convert.ToString(txtDescripcion.Text.Trim());
    if(txtValor.Text.Trim() != "")
        vInventarioMateriaPrima.valor = Convert.ToInt64(txtValor.Text.Trim().Replace(@".",""));

        return vInventarioMateriaPrima;
    }


    private void Edicion()
    {
        try
        {
            if (Session[InventarioMateriaPrimaServicio.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(InventarioMateriaPrimaServicio.CodigoPrograma, "E");
            else
                VisualizarOpciones(InventarioMateriaPrimaServicio.CodigoPrograma, "A");

            //Page_Load
            if (Session[InventarioMateriaPrimaServicio.CodigoPrograma + ".id"] != null)
            {
                idObjeto = Session[InventarioMateriaPrimaServicio.CodigoPrograma + ".id"].ToString();
                if (idObjeto != null)
                {
                    ObtenerDatos(idObjeto);
                    Session.Remove(InventarioMateriaPrimaServicio.CodigoPrograma + ".id");
                }

            }
            //else
            //{
            //    CargarListas();
            //}
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(InventarioMateriaPrimaServicio.GetType().Name + "A", "Page_PreInit", ex);
        }
    }


    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            Xpinn.FabricaCreditos.Entities.InventarioMateriaPrima vInventarioMateriaPrima = new Xpinn.FabricaCreditos.Entities.InventarioMateriaPrima();

            if (idObjeto != "")
                vInventarioMateriaPrima = InventarioMateriaPrimaServicio.ConsultarInventarioMateriaPrima(Convert.ToInt64(idObjeto), (Usuario)Session["usuario"]);

            if (txtCod_matprima.Text != "") vInventarioMateriaPrima.cod_matprima = Convert.ToInt64(txtCod_matprima.Text.Trim());
            vInventarioMateriaPrima.cod_inffin = Convert.ToInt64(Session["Cod_InfFin"].ToString());
            vInventarioMateriaPrima.descripcion = (txtDescripcion.Text != "") ? Convert.ToString(txtDescripcion.Text.Trim()) : String.Empty;
            if (txtValor.Text != "") vInventarioMateriaPrima.valor = Convert.ToInt64(txtValor.Text.Trim().Replace(@".", ""));
            vInventarioMateriaPrima.cod_persona = Convert.ToInt64(Convert.ToInt64(Session["Cod_persona"].ToString()));

            ////INFORMACION FINANCIERA:
            //Xpinn.FabricaCreditos.Entities.InformacionFinanciera vInformacionFinanciera = new Xpinn.FabricaCreditos.Entities.InformacionFinanciera();
            //vInformacionFinanciera.cod_persona = Convert.ToInt64(Session["Cod_persona"].ToString());
            //vInformacionFinanciera.fecha = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy"));

            if (idObjeto != "")
            {
                vInventarioMateriaPrima.cod_matprima = Convert.ToInt64(idObjeto);
                InventarioMateriaPrimaServicio.ModificarInventarioMateriaPrima(vInventarioMateriaPrima, (Usuario)Session["usuario"]);
            }
            else  //Crea informacion financiera y despues inventario
            {
                //InformacionFinancieraServicio.CrearInformacionFinanciera(vInformacionFinanciera, (Usuario)Session["usuario"]);
                //vInventarioMateriaPrima.cod_inffin = vInformacionFinanciera.cod_inffin;

                vInventarioMateriaPrima = InventarioMateriaPrimaServicio.CrearInventarioMateriaPrima(vInventarioMateriaPrima, (Usuario)Session["usuario"]);
                idObjeto = vInventarioMateriaPrima.cod_matprima.ToString();
            }
            Session[InventarioMateriaPrimaServicio.CodigoPrograma + ".id"] = idObjeto;
            Actualizar();
            Borrar();
            //Navegar(Pagina.Detalle);
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(InventarioMateriaPrimaServicio.CodigoPrograma, "btnGuardar_Click", ex);
        }
    }

    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            Xpinn.FabricaCreditos.Entities.InventarioMateriaPrima vInventarioMateriaPrima = new Xpinn.FabricaCreditos.Entities.InventarioMateriaPrima();
            vInventarioMateriaPrima = InventarioMateriaPrimaServicio.ConsultarInventarioMateriaPrima(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);

            if (vInventarioMateriaPrima.cod_matprima != Int64.MinValue)
                txtCod_matprima.Text = HttpUtility.HtmlDecode(vInventarioMateriaPrima.cod_matprima.ToString().Trim());
            
            if (!string.IsNullOrEmpty(vInventarioMateriaPrima.descripcion))
                txtDescripcion.Text = HttpUtility.HtmlDecode(vInventarioMateriaPrima.descripcion.ToString().Trim());
            if (vInventarioMateriaPrima.valor != Int64.MinValue)
                txtValor.Text = HttpUtility.HtmlDecode(vInventarioMateriaPrima.valor.ToString().Trim());
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(InventarioMateriaPrimaServicio.CodigoPrograma, "ObtenerDatos", ex);
        }
    }

}