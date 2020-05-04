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
using Xpinn.Aportes.Services;
using Xpinn.Aportes.Entities;
using Xpinn.Comun.Services;
using Xpinn.Comun.Entities;
using System.Data.Common;
using Xpinn.FabricaCreditos.Services;
using Xpinn.FabricaCreditos.Entities;

public partial class Nuevo : GlobalWeb
{
   
    InformacionAdicionalServices InformacionServicio = new InformacionAdicionalServices();   

    List<Xpinn.FabricaCreditos.Entities.Persona1> lstDatosSolicitud = new List<Xpinn.FabricaCreditos.Entities.Persona1>();  //Lista de los menus desplegables
   
    
    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(InformacionServicio.CodigoProgramaParametros, "A");

            Site toolBar = (Site)this.Master;            
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.MostrarGuardar(false);
            toolBar.eventoRegresar += btnRegresar_Click;
            ctlMensaje.eventoClick += btnContinuarMen_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(InformacionServicio.CodigoProgramaParametros, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Session["InfoAdicional"] = null;
            if (!IsPostBack)
            {
                mtvInformacion.ActiveViewIndex = 0;
                InicializarInfAdicional();
                lblmsj.Text = "grabados";
                if (Session[InformacionServicio.CodigoProgramaParametros + ".id"] != null)
                {
                    idObjeto = Session[InformacionServicio.CodigoProgramaParametros.ToString() + ".id"].ToString();
                    Session.Remove(InformacionServicio.CodigoProgramaParametros.ToString() + ".id");
                    ObtenerDatos(idObjeto);
                    lblmsj.Text = "modificados";
                }
            }
        }

        catch (Exception ex)
        {
            BOexcepcion.Throw(InformacionServicio.CodigoProgramaParametros, "Page_Load", ex);
        }
    }



    #region INFORMACION ADICIONAL

    protected void InicializarInfAdicional()
    {
        List<InformacionAdicional> lstInformacion = new List<InformacionAdicional>();
        for (int i = 0; i < 1; i++)
        {
            InformacionAdicional eInf = new InformacionAdicional();
            eInf.cod_infadicional = -1;
            eInf.descripcion = "";
            eInf.tipo = null;
            eInf.items_lista = "";

            lstInformacion.Add(eInf);
        }
        gvInformacionADD.DataSource = lstInformacion;
        gvInformacionADD.DataBind();
        Session["InfoAdicional"] = lstInformacion;

    }

    protected List<InformacionAdicional> ObtenerLista_InfAdicional()
    {
        List<InformacionAdicional> lstInformacion = new List<InformacionAdicional>();
        List<InformacionAdicional> lista = new List<InformacionAdicional>();

        foreach (GridViewRow rfila in gvInformacionADD.Rows)
        {
            InformacionAdicional eInfo = new InformacionAdicional();
            Label lblcod_infadicional = (Label)rfila.FindControl("lblcod_infadicional");
            if (lblcod_infadicional != null)
                eInfo.cod_infadicional = Convert.ToInt32(lblcod_infadicional.Text);

            TextBox txtDescrip = (TextBox)rfila.FindControl("txtDescrip");
            if (txtDescrip != null)
                eInfo.descripcion = txtDescrip.Text;

            DropDownListGrid ddltipoInf = (DropDownListGrid)rfila.FindControl("ddltipoInf");
            if (ddltipoInf.SelectedValue != null)
                eInfo.tipo = Convert.ToInt32(ddltipoInf.SelectedValue);

            TextBox txtItems_lista = (TextBox)rfila.FindControl("txtItems_lista");
            if (txtItems_lista != null)
                eInfo.items_lista = txtItems_lista.Text;

            if (ddlTipoPersona.SelectedIndex != 0)
                eInfo.tipo_persona = ddlTipoPersona.SelectedValue;

            lista.Add(eInfo);
            Session["InfoAdicional"] = lista;

            if (eInfo.descripcion.Trim() != "" && eInfo.tipo != 0)
            {
                lstInformacion.Add(eInfo);    
            }
        }
        return lstInformacion;
    }

    protected void ddlTipoPersona_SelectedIndexChanged(object sender, EventArgs e)
    {
        Site toolBar = (Site)this.Master;        
       
        if (ddlTipoPersona.SelectedValue != "0")
        {           
            toolBar.MostrarGuardar(true);
            panelInforAdicional.Visible = true;            
        }
        else
        {
            InicializarInfAdicional();
            toolBar.MostrarGuardar(false);
            panelInforAdicional.Visible = false;
        }   
    }


    protected void btnInfAdicional_Click(object sender, EventArgs e)
    {
        ObtenerLista_InfAdicional();

        List<InformacionAdicional> lstInformacion = new List<InformacionAdicional>();

        if (Session["InfoAdicional"] != null)
        {
            lstInformacion = (List<InformacionAdicional>)Session["InfoAdicional"];

            for (int i = 1; i <= 1; i++)
            {
                InformacionAdicional eInf = new InformacionAdicional();
                eInf.cod_infadicional = -1;
                eInf.descripcion = "";
                eInf.tipo = null;
                eInf.items_lista = "";
                lstInformacion.Add(eInf);
            }
            gvInformacionADD.PageIndex = gvInformacionADD.PageCount;
            gvInformacionADD.DataSource = lstInformacion;
            gvInformacionADD.DataBind();

            Session["InfoAdicional"] = lstInformacion;
        }
    }


    protected void gvInformacionADD_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownListGrid ddltipoInf = (DropDownListGrid)e.Row.FindControl("ddltipoInf");
            if (ddltipoInf != null)
            {
                ddltipoInf.Items.Insert(0, new ListItem("Seleccione un item", "0"));
                ddltipoInf.Items.Insert(1, new ListItem("Carácter", "1"));
                ddltipoInf.Items.Insert(2, new ListItem("Fecha", "2"));
                ddltipoInf.Items.Insert(3, new ListItem("Número", "3"));
                ddltipoInf.Items.Insert(4, new ListItem("Lista", "4"));
                ddltipoInf.SelectedIndex = 0;
                ddltipoInf.DataBind();
            }

            Label lblTipo = (Label)e.Row.FindControl("lblTipo");
            if (lblTipo != null)
                ddltipoInf.SelectedValue = lblTipo.Text;

            TextBox txtItems_lista = (TextBox)e.Row.FindControl("txtItems_lista");
            if (ddltipoInf != null)
            {
                if (ddltipoInf.SelectedItem.Text == "Lista")
                    txtItems_lista.Visible = true;
                else
                    txtItems_lista.Visible = false;
            }
        }
    }


    protected void gvInformacionADD_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }

    protected void gvInformacionADD_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }

    protected void ddltipoInf_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownListGrid ddltipoInf = (DropDownListGrid)sender;
        int nItem = Convert.ToInt32(ddltipoInf.CommandArgument);

        TextBox txtItems_lista = (TextBox)gvInformacionADD.Rows[nItem].FindControl("txtItems_lista");

        if (ddltipoInf != null)
        {
            if (ddltipoInf.SelectedItem.Text == "Lista")
            {
                txtItems_lista.Text = "";
                txtItems_lista.Visible = true;
            }
            else
            {
                txtItems_lista.Visible = false;
            }
        }        
    }


    #endregion


    
    protected void ObtenerDatos(String pIdObjeto)
    { 
        try
        {
            List<InformacionAdicional> lstlista = new List<InformacionAdicional>();
            if (pIdObjeto != null)
            {
                InformacionAdicional info = new InformacionAdicional();
                if (pIdObjeto == "N" || pIdObjeto == "J" || pIdObjeto == "M")
                {
                    info.descripcion = pIdObjeto;
                    lstlista = InformacionServicio.ConsultarInformacionAdicional(info, (Usuario)Session["usuario"]);
                }
                else
                {
                    info.cod_infadicional = Convert.ToInt32(pIdObjeto);
                    lstlista = InformacionServicio.ConsultarInformacionAdicional(info, (Usuario)Session["usuario"]);
                }

                if (lstlista.Count > 0)
                {
                    if (lstlista[0].tipo_persona != "")
                        ddlTipoPersona.SelectedValue = lstlista[0].tipo_persona;
                    gvInformacionADD.DataSource = lstlista;
                    gvInformacionADD.DataBind();
                    ddlTipoPersona_SelectedIndexChanged(ddlTipoPersona, null);
                }
                else
                {
                    ddlTipoPersona.SelectedValue = pIdObjeto;
                    ddlTipoPersona_SelectedIndexChanged(ddlTipoPersona, null);
                }
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(InformacionServicio.GetType().Name + "A", "ObtenerDatos", ex);
        }
    }

    private void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ctlMensaje.MostrarMensaje("Desea realizar la Grabación?");
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(InformacionServicio.CodigoProgramaParametros, "btnGuardar_Click", ex);
        }
    }

    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        try
        {
            InformacionAdicional adicional = new InformacionAdicional();
            adicional.lstInfor = new List<InformacionAdicional>();
            adicional.lstInfor = ObtenerLista_InfAdicional();

            if (idObjeto != "")
            {
                InformacionServicio.ModificarTipo_InforAdicional(adicional, (Usuario)Session["usuario"]);
            }
            else
            {
                InformacionServicio.CrearTipo_InforAdicional(adicional, (Usuario)Session["usuario"]);
            }
            Session[InformacionServicio.CodigoProgramaParametros + ".id"] = idObjeto;

            mtvInformacion.ActiveViewIndex = 1;

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(InformacionServicio.CodigoProgramaParametros, "btnContinuar_Click", ex);
        }
    }

    private void btnRegresar_Click(object sender, ImageClickEventArgs e)
    {
        Navegar(Pagina.Lista);
    }
}