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
    ChequeraService ChequeraServ = new ChequeraService();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(ChequeraServ.CodigoPrograma, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoNuevo += btnNuevo_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ChequeraServ.GetType().Name + "L", "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                Chequera cheq = new Chequera();
                List<Chequera> lstcheque = new List<Chequera>();
                lstcheque = ChequeraServ.ListarCuentasBancarias(cheq, (Usuario)Session["usuario"]);
                ddlCuenta.DataSource = lstcheque;
                ddlCuenta.DataTextField = "num_cuenta";
                ddlCuenta.DataValueField = "idctabancaria";
                //ddlCuenta.Items.Insert(0,new ListItem("Seleccione un item","0"));
                ddlCuenta.SelectedIndex = 0;
                ddlCuenta.DataBind();
                PoblarLista("bancos", ddlBanco);
                ddlEstado.Items.Insert(0, new ListItem("Seleccione un item", "0"));
                ddlEstado.Items.Insert(1, new ListItem("Activo", "1"));
                ddlEstado.Items.Insert(2, new ListItem("Inactivo", "2"));
                ddlEstado.SelectedIndex = 0;
                ddlEstado.DataBind();
                Actualizar();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ChequeraServ.GetType().Name + "L", "Page_Load", ex);
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

    private void Actualizar()
    {
        try
        {
            List<Chequera> lstConsulta = new List<Chequera>();
            String filtro = obtFiltro(ObtenerValores());
            lstConsulta = ChequeraServ.ListarChequera(ObtenerValores(), (Usuario)Session["usuario"], filtro);

            gvLista.PageSize = 15;
            String emptyQuery = "Fila de datos vacia";
            gvLista.EmptyDataText = emptyQuery;
            gvLista.DataSource = lstConsulta;

            if (lstConsulta.Count > 0)
            {
                panelGrilla.Visible = true;
                //gvLista.Visible = true;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                gvLista.DataBind();
                ValidarPermisosGrilla(gvLista);
            }
            else
            {
                panelGrilla.Visible = false;
                //gvLista.Visible = false;
                lblTotalRegs.Visible = false;
            }

            Session.Add(ChequeraServ.CodigoPrograma + ".consulta", 1);
        }
            catch (Exception ex)
        {
            BOexcepcion.Throw(ChequeraServ.CodigoPrograma, "Actualizar", ex);
        }
    }

    private Chequera ObtenerValores()
    {
        Chequera vCheque = new Chequera();
        if (ddlCuenta.SelectedValue != "0")
            if(ddlCuenta.SelectedValue != "")
                vCheque.idctabancaria = Convert.ToInt32(ddlCuenta.SelectedValue);
        if (ddlBanco.SelectedValue != "0")
            if(ddlBanco.SelectedValue != "")
                vCheque.cod_banco = Convert.ToInt32(ddlBanco.SelectedValue);
        if(ddlEstado.SelectedValue != "0")
            if(ddlEstado.SelectedValue != "")
                vCheque.estado = Convert.ToInt32(ddlEstado.SelectedValue);

            return vCheque;
    }

    private string obtFiltro(Chequera Cheq)
    {
        String filtro = String.Empty;
        if (ddlCuenta.SelectedValue != "0")
            if(ddlCuenta.SelectedValue != "")
                filtro += " and cuenta_bancaria.idctabancaria = " + Cheq.idctabancaria;
        if (ddlBanco.SelectedValue != "0")
            if(ddlBanco.SelectedValue != "")
                filtro += " and bancos.cod_banco = " + Cheq.cod_banco;
        if (ddlEstado.SelectedValue != "0")
            if(ddlEstado.SelectedValue != "")
                filtro += " and chequera.estado = " + Cheq.estado;

        //filtro += " and estado ='G'";
        //if (!string.IsNullOrEmpty(filtro))
        //{
        //    filtro = filtro.Substring(4);
        //    filtro = "where " + filtro;
        //}
        return filtro;
    }


    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvLista.DataKeys[e.NewEditIndex].Value.ToString();
        Session[ChequeraServ.CodigoPrograma + ".id"] = id;
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
            BOexcepcion.Throw(ChequeraServ.GetType().Name + "L", "gvLista_PageIndexChanging", ex);
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
            ChequeraServ.EliminarChequera(Convert.ToInt32(Session["Index"]), (Usuario)Session["usuario"]);
            Actualizar();
        }
        catch (Xpinn.Util.ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ChequeraServ.CodigoPrograma + "L", "gvLista_RowDeleting", ex);
        }
        
        mpeConfirmar.Hide();
    }


    protected void btnParar_Click(object sender, EventArgs e)
    {
        mpeConfirmar.Hide();
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

    
}