using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using System.Data;
using System.Data.Common;

public partial class BusquedaRapidaVentEmergente : System.Web.UI.UserControl
{
    public AjaxControlToolkit.ModalPopupExtender pmpePersonas { private set; get; }    

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
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

    public void Motrar(Boolean pMostrar, String pctlIdentificacion, String pctlNombre)
    {
        InicializarGridPersonas();
        panelBusquedaRapida.Visible = pMostrar;
        Mostrado = pMostrar;
        hfIdentificacion.Value = pctlIdentificacion;
        hfNombre.Value = pctlNombre;
        ViewState["MostrarBusqueda"] = pMostrar;        
    }

    public void Motrar(Boolean pMostrar, String pctlCodigo, String pctlIdentificacion, String pctlNombre)
    {
        InicializarGridPersonas();
        panelBusquedaRapida.Visible = pMostrar;
        Mostrado = pMostrar;
        hfCodigo.Value = pctlCodigo;
        hfIdentificacion.Value = pctlIdentificacion;
        hfNombre.Value = pctlNombre;
        ViewState["MostrarBusqueda"] = pMostrar;
    }

    public void InicializarGridPersonas()
    {        
        try
        {           
            List<Xpinn.FabricaCreditos.Entities.Persona1> lstConsulta = new List<Xpinn.FabricaCreditos.Entities.Persona1>();
            Xpinn.FabricaCreditos.Entities.Persona1 ePersona = new Xpinn.FabricaCreditos.Entities.Persona1();
            for (int i = 0; i <= 10; i++)
            {
                lstConsulta.Add(ePersona);
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
        Xpinn.FabricaCreditos.Services.Persona1Service personaService = new Xpinn.FabricaCreditos.Services.Persona1Service();
        Xpinn.FabricaCreditos.Entities.Persona1 persona = new Xpinn.FabricaCreditos.Entities.Persona1();
        try
        {
            if (txtCod.Text.Trim() != "")
                persona.cod_persona = Convert.ToInt64(txtCod.Text);
            if (txtIde.Text.Trim() != "")
                persona.identificacion = txtIde.Text;
            if (txtNom.Text.Trim() != "")
                persona.nombres = txtNom.Text;
            if (txtApe.Text.Trim() != "")
                persona.apellidos = txtApe.Text;
            List<Xpinn.FabricaCreditos.Entities.Persona1> lstConsulta = new List<Xpinn.FabricaCreditos.Entities.Persona1>();
            lstConsulta = personaService.ListadoPersonas1(persona, (Usuario)Session["usuario"]);
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
            gvPersonas.Rows[rowIndex].BackColor = System.Drawing.Color.DarkBlue;
            gvPersonas.Rows[rowIndex].ForeColor = System.Drawing.Color.White;
            panelBusquedaRapida.Visible = false;
            if (hfCodigo.Value != "")
            {
                ContentPlaceHolder mpContentPlaceHolder = (ContentPlaceHolder)this.NamingContainer;
                TextBox txtCodigo = (TextBox)mpContentPlaceHolder.FindControl(hfCodigo.Value);
                if (txtCodigo != null)
                {
                    txtCodigo.Text = gvPersonas.Rows[rowIndex].Cells[1].Text;
                }
            }
            if (hfIdentificacion.Value != "")
            {
                ContentPlaceHolder mpContentPlaceHolder = (ContentPlaceHolder)this.NamingContainer;
                TextBox txtIdentificacion = (TextBox)mpContentPlaceHolder.FindControl(hfIdentificacion.Value);
                if (txtIdentificacion != null)
                {
                    txtIdentificacion.Text = gvPersonas.Rows[rowIndex].Cells[2].Text;
                }
            }
            if (hfNombre.Value != "")
            {
                ContentPlaceHolder mpContentPlaceHolder = (ContentPlaceHolder)this.NamingContainer;
                TextBox txtNombres = (TextBox)mpContentPlaceHolder.FindControl(hfNombre.Value);
                if (txtNombres != null)
                {
                    txtNombres.Text = Server.HtmlDecode(gvPersonas.Rows[rowIndex].Cells[3].Text + ", " + gvPersonas.Rows[rowIndex].Cells[4].Text);
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