using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using System.Data;
using System.Data.Common;

public partial class BusquedaMovimientos : System.Web.UI.UserControl
{
    public AjaxControlToolkit.ModalPopupExtender pmpePersonas { private set; get; }    

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            hfNumAporte.Value = null;
            hfCodigo.Value = null;
            hfIdentificacion.Value = null;
            hfNombre.Value = null;
            hfControl.Value = null;
        }
    }

    public Boolean Mostrado
    {
        get { return Convert.ToBoolean(hfControl.Value); }
        set { hfControl.Value = Convert.ToString(value); }
    }

    public void Motrar(Boolean pMostrar, String pctlIdentificacion, String pctlNombre,String pctlNumAporte)
    {
        InicializarGridPersonas();
        panelBusquedaRapida.Visible = pMostrar;
        Mostrado = pMostrar;
        hfNumAporte.Value = pctlNumAporte;
        hfIdentificacion.Value = pctlIdentificacion;
        hfNombre.Value = pctlNombre;
        ViewState["MostrarBusqueda"] = pMostrar;        
    }

    //public void Motrar(Boolean pMostrar, String pctlCodigo, String pctlIdentificacion, String pctlNombre)
    //{
    //    InicializarGridPersonas();
    //    panelBusquedaRapida.Visible = pMostrar;
    //    Mostrado = pMostrar;
    //    hfCodigo.Value = pctlCodigo;
    //    hfIdentificacion.Value = pctlIdentificacion;
    //    hfNombre.Value = pctlNombre;
    //    ViewState["MostrarBusqueda"] = pMostrar;
    //}



    //public void Motrar(Boolean pMostrar, String pctlCodigo, String pctlIdentificacion, String pctlTipoIdentificacion, String pctlNombre)
    //{
    //    InicializarGridPersonas();
    //    panelBusquedaRapida.Visible = pMostrar;
    //    Mostrado = pMostrar;
    //    hfCodigo.Value = pctlCodigo;
    //    hfIdentificacion.Value = pctlIdentificacion;
    //    hfTipoIdentificacion.Value = pctlTipoIdentificacion;
    //    hfNombre.Value = pctlNombre;
    //    ViewState["MostrarBusqueda"] = pMostrar;
    //}

    public void InicializarGridPersonas()
    {        
        try
        {
            List<Xpinn.Aportes.Entities.Aporte> lstConsulta = new List<Xpinn.Aportes.Entities.Aporte>();
            Xpinn.Aportes.Entities.Aporte eAporte = new Xpinn.Aportes.Entities.Aporte();
            for (int i = 0; i <= 10; i++)
            {
                lstConsulta.Add(eAporte);
            }
            gvPersonas.DataSource = lstConsulta;
            gvPersonas.DataBind();
            gvPersonas.Visible = true;                        
        }
        catch (Exception ex)
        {
            ex.ToString();
        }
    }

    public void ActualizarGridPersonas()
    {
        Xpinn.Aportes.Services.AporteServices personaService = new Xpinn.Aportes.Services.AporteServices();
        try
        {
            string filtro = "";
            if (txtNumAporte.Text.Trim() != "")
                filtro += " and v.numero_aporte = " + txtNumAporte.Text;
            if (txtCod.Text.Trim() != "")
                filtro += " and v.cod_persona = "+ txtCod.Text;
            if (txtIde.Text.Trim() != "")
                filtro += " and v.identificacion = '" + txtIde.Text + "'";
            if (txtNom.Text.Trim() != "")
                filtro = " and v.nombre Like '%" + txtNom.Text +"'";
            List<Xpinn.Aportes.Entities.Aporte> lstConsulta = new List<Xpinn.Aportes.Entities.Aporte>();
            lstConsulta = personaService.ListarAportesControl(filtro, (Usuario)Session["usuario"]);
            gvPersonas.DataSource = lstConsulta;
            if (lstConsulta.Count > 0)
            {
                gvPersonas.Visible = true;
                gvPersonas.DataBind();
            }
            else
            {
                gvPersonas.Visible = false;
                InicializarGridPersonas();
            }

            Session.Add(personaService.GetType().Name + ".consulta", 1);
        }
        catch (Exception ex)
        {
            ex.ToString();
        }
    }

    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        ActualizarGridPersonas();
    }

    protected void gvPersonas_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvPersonas.PageIndex = e.NewPageIndex;
            ActualizarGridPersonas();
        }
        catch (Exception ex)
        {
            ex.ToString();
        }
    }    

    protected void btnSeleccionar_Click(object sender, ImageClickEventArgs e)
    {        
        ImageButtonGrid btnSeleccionar = (ImageButtonGrid)sender;
        if (btnSeleccionar != null)
        {
            int rowIndex = Convert.ToInt32(btnSeleccionar.CommandArgument);
            rowIndex = rowIndex - (gvPersonas.PageIndex * gvPersonas.PageSize);
            gvPersonas.Rows[rowIndex].BackColor = System.Drawing.Color.DarkBlue;
            gvPersonas.Rows[rowIndex].ForeColor = System.Drawing.Color.White;
            panelBusquedaRapida.Visible = false;
            if (hfNumAporte.Value != "")
            {
                ContentPlaceHolder mpContentPlaceHolder = (ContentPlaceHolder)this.NamingContainer;
                TextBox txtNumAporte = (TextBox)mpContentPlaceHolder.FindControl(hfNumAporte.Value);
                if (txtNumAporte != null)
                {
                    txtNumAporte.Text = gvPersonas.Rows[rowIndex].Cells[1].Text;
                }
            }
            if (hfCodigo.Value != "")
            {
                ContentPlaceHolder mpContentPlaceHolder = (ContentPlaceHolder)this.NamingContainer;
                TextBox txtCodigo = (TextBox)mpContentPlaceHolder.FindControl(hfCodigo.Value);
                if (txtCodigo != null)
                {
                    txtCodigo.Text = gvPersonas.Rows[rowIndex].Cells[2].Text;
                }
            }
            if (hfIdentificacion.Value != "")
            {
                ContentPlaceHolder mpContentPlaceHolder = (ContentPlaceHolder)this.NamingContainer;
                TextBox txtIdentificacion = (TextBox)mpContentPlaceHolder.FindControl(hfIdentificacion.Value);
                if (txtIdentificacion != null)
                {
                    txtIdentificacion.Text = gvPersonas.Rows[rowIndex].Cells[3].Text;
                }
            }
            if (hfTipoIdentificacion.Value != "")
            {
                ContentPlaceHolder mpContentPlaceHolder = (ContentPlaceHolder)this.NamingContainer;
                DropDownList ddlTipoIdentificacion = (DropDownList)mpContentPlaceHolder.FindControl(hfTipoIdentificacion.Value);
                if (ddlTipoIdentificacion != null)
                {
                    Xpinn.Comun.Entities.TipoIdentificacion tipoIden = new Xpinn.Comun.Entities.TipoIdentificacion();
                    Xpinn.Comun.Services.TipoIdentificacionService servicioTipoIden = new Xpinn.Comun.Services.TipoIdentificacionService();
                    if (gvPersonas.Rows[rowIndex].Cells[4].Text.Trim() != "")
                    {
                        tipoIden = servicioTipoIden.ConsultarTipoIdentificacion(gvPersonas.Rows[rowIndex].Cells[4].Text, (Usuario)Session["Usuario"]);
                        if (tipoIden != null)
                            ddlTipoIdentificacion.SelectedValue = Convert.ToString(tipoIden.IdTipoIdentificacion);
                    }
                }
            }
            if (hfNombre.Value != "")
            {
                ContentPlaceHolder mpContentPlaceHolder = (ContentPlaceHolder)this.NamingContainer;
                TextBox txtNombres = (TextBox)mpContentPlaceHolder.FindControl(hfNombre.Value);
                if (txtNombres != null)
                {
                    txtNombres.Text = Server.HtmlDecode(gvPersonas.Rows[rowIndex].Cells[5].Text);
                }
            }
        }
    }

    protected void gvPersonas_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            try
            {
                Int64? codPersona = Convert.ToInt64(e.Row.Cells[1].Text);
                if (codPersona == 0)
                {                  
                    ImageButtonGrid btnSeleccionar = (ImageButtonGrid)e.Row.FindControl("btnSeleccionar");
                    if (btnSeleccionar != null)
                    {
                        btnSeleccionar.Visible = false;
                        e.Row.Cells[1].Text = "";
                    }
                }
            }
            catch (Exception ex) 
            { string s = ex.Message; }

        }
    }

    protected void bntCerrar_Click(object sender, ImageClickEventArgs e)
    {
        Mostrado = false;
        panelBusquedaRapida.Visible = false;
    }
}
