using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Tesoreria.Services;
using Xpinn.Tesoreria.Entities;
using Xpinn.Util;
using System.IO;
using System.Configuration;
using System.Globalization;
using System.Text;
using System.Web.UI.HtmlControls;

public partial class Lista : GlobalWeb
{
    //EstructuraRecaudoServices EstructuraRecaudo = new EstructuraRecaudoServices();
    EmpresaNovedadService Recaudos = new EmpresaNovedadService();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(Recaudos.CodigoPrograma, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoNuevo += btnNuevo_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(Recaudos.GetType().Name + "L", "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                CargarDropDown();
                Actualizar();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(Recaudos.GetType().Name + "L", "Page_Load", ex);
        }
    }

    void CargarDropDown()
    {
        PoblarLista("empresa_recaudo", "", " ESTADO = 1 ", "2", ddlEmpresa);
        PoblarLista("tipo_lista_recaudo", ddlTipoLista);

        ddlEstado.Items.Insert(0, new ListItem("Seleccione un item", "0"));
        ddlEstado.Items.Insert(1, new ListItem("PENDIENTE", "1"));
        ddlEstado.Items.Insert(2, new ListItem("APLICADO", "2"));
        ddlEstado.Items.Insert(3, new ListItem("ANULADO", "3"));
        ddlEstado.SelectedIndex = 0;
        ddlEstado.DataBind();
    }


    public void PoblarLista(string pTabla, DropDownList ddlControl)
    {
        PoblarLista(pTabla, "", "", "", ddlControl);
    }

    public void PoblarLista(string pTabla, string pColumnas, string pCondicion, string pOrden, DropDownList ddlControl)
    {
        List<Xpinn.Comun.Entities.ListaDesplegable> plista = new List<Xpinn.Comun.Entities.ListaDesplegable>();
        Xpinn.Comun.Entities.ListaDesplegable pentidad = new Xpinn.Comun.Entities.ListaDesplegable();
        Xpinn.Comun.Services.ListaDeplegableService pservicio = new Xpinn.Comun.Services.ListaDeplegableService();

        ddlControl.Items.Clear();
        plista = pservicio.ListarListaDesplegable(pentidad, pTabla, pColumnas, pCondicion, pOrden, (Usuario)Session["usuario"]);
        pentidad.idconsecutivo = null;
        pentidad.descripcion = "Seleccione un item";
        plista.Insert(0, pentidad);
        ddlControl.DataTextField = "descripcion";
        ddlControl.DataValueField = "idconsecutivo";
        ddlControl.DataSource = plista;
        ddlControl.DataBind();
    }


    private void Actualizar()
    {
        try
        {
            List<EmpresaNovedad> lstConsulta = new List<EmpresaNovedad>();
            String sOrden = "";
            if (ddlOrdenar.SelectedValue != "")
                sOrden += ddlOrdenar.SelectedValue; 
            if (ddlOrdenarLuego.SelectedValue != "")
                sOrden += (sOrden.Trim().Length > 0 ? ", ": "") + ddlOrdenarLuego.SelectedValue;
            if (sOrden.Trim() == "")
                sOrden = " 6 Desc ,1 Desc";
            lstConsulta = Recaudos.ListarRecaudo(ObtenerValores(), sOrden, (Usuario)Session["usuario"]);

            gvLista.PageSize = 15;
            String emptyQuery = "Fila de datos vacia";
            gvLista.EmptyDataText = emptyQuery;
            gvLista.DataSource = lstConsulta;

            if (lstConsulta.Count > 0)
            {
                Label2.Visible = false;
                panelGrilla.Visible = true;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados : " + lstConsulta.Count.ToString();
                gvLista.DataBind();
            }
            else
            {
                panelGrilla.Visible = false;
                Label2.Visible = true;
                lblTotalRegs.Visible = false;
            }

            Session.Add(Recaudos.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(Recaudos.CodigoPrograma, "Actualizar", ex);
        }
    }



    protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    {
        Navegar(Pagina.Nuevo);
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        Page.Validate();
        gvLista.Visible = true;
        if (Page.IsValid)
        {
            Actualizar();
        } 
    }
     

    private EmpresaNovedad ObtenerValores()
    {
        EmpresaNovedad vReca = new EmpresaNovedad();
        
        Configuracion conf = new Configuracion();

        if (ddlEmpresa.SelectedValue != "")
            vReca.cod_empresa = Convert.ToInt64(ddlEmpresa.SelectedValue);
        if (ddlTipoLista.SelectedValue != "")
            vReca.tipo_lista = Convert.ToInt32(ddlTipoLista.SelectedValue);             
        if (txtfecha.Text != "")
            vReca.fecha_generacion = Convert.ToDateTime(txtfecha.Text);            
        if (ddlEstado.SelectedValue != "")
            vReca.estado = ddlEstado.SelectedValue;            
        return vReca;
    }

    private string obtFiltro(Estructura_Carga Estruc)
    {
        String filtro = String.Empty;
        if (!string.IsNullOrEmpty(filtro))
        {
            filtro = filtro.Substring(4);
            filtro = "where " + filtro;
        }
        return filtro;
    }


    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvLista.DataKeys[e.NewEditIndex].Value.ToString();
        Session[Recaudos.CodigoPrograma + ".id"] = id;
        Navegar(Pagina.Nuevo);
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
            BOexcepcion.Throw(Recaudos.GetType().Name + "L", "gvLista_PageIndexChanging", ex);
        }
    }


    protected void btnFinal_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }


    protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int conseID = Convert.ToInt32(gvLista.DataKeys[e.RowIndex].Values[0].ToString());
        Session["Index"] = conseID;
        mpeConfirmar.Show();
    }

    protected void btnContinuar_Click(object sender, EventArgs e)
    {
        try
        {
            Recaudos.Eliminar_1_Encabezado_2_Detalle_RECAUDO(Convert.ToInt64(Session["Index"]), (Usuario)Session["usuario"],1);
            Actualizar();
        }
        catch (Xpinn.Util.ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(Recaudos.CodigoPrograma + "L", "gvLista_RowDeleting", ex);
        }
        
        mpeConfirmar.Hide();
    }

    protected void btnParar_Click(object sender, EventArgs e)
    {
        mpeConfirmar.Hide();
    }

    protected void ddlOrdenar_SelectedIndexChanged(object sender, EventArgs e)
    {
        Actualizar();
    }

    protected void ddlOrdenarLuego_SelectedIndexChanged(object sender, EventArgs e)
    {
        Actualizar();
    }
}