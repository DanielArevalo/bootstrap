using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using System.Data;
using System.Data.Common;

public delegate void txtIdentificacion_TextChanged_ActionsDelegate(object sender, EventArgs e);
public partial class BusquedaRapida : System.Web.UI.UserControl
{
    public AjaxControlToolkit.ModalPopupExtender pmpePersonas { private set; get; }
    public event txtIdentificacion_TextChanged_ActionsDelegate eventotxtIdentificacion_TextChanged;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            hfCodigo.Value = null;
            hfIdentificacion.Value = null;
            hfNombre.Value = null;
            hfControl.Value = null;
            hfIdentificacion2.Value = null;
            hfNombre2.Value = null;
            hfDireccion.Value = null;
            hftelefono.Value = null;
            hfciudad.Value = null;
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

    public void Mostrar(Boolean pMostrar, String pctlCodigo, String pctlIdentificacion, String pctlNombre, String pctlIdentificacion2, String pctlNombre2)
    {
        InicializarGridPersonas();
        panelBusquedaRapida.Visible = pMostrar;
        Mostrado = pMostrar;
        hfCodigo.Value = pctlCodigo;
        hfIdentificacion.Value = pctlIdentificacion;
        hfNombre.Value = pctlNombre;;
        hfIdentificacion2.Value = pctlIdentificacion2;
        hfNombre2.Value = pctlNombre2;
        ViewState["MostrarBusqueda"] = pMostrar;
    }

    public void Motrar(Boolean pMostrar, String pctlCodigo, String pctlIdentificacion, String pctlTipoIdentificacion, String pctlNombre)
    {
        InicializarGridPersonas();
        panelBusquedaRapida.Visible = pMostrar;
        Mostrado = pMostrar;
        hfCodigo.Value = pctlCodigo;
        hfIdentificacion.Value = pctlIdentificacion;
        hfTipoIdentificacion.Value = pctlTipoIdentificacion;
        hfNombre.Value = pctlNombre;
        ViewState["MostrarBusqueda"] = pMostrar;
    }

    public void Motrar(Boolean pMostrar, String pctlCodigo, String pctlIdentificacion, String pctlTipoIdentificacion, String pctlNombre, String pctlApellido)
    {
        InicializarGridPersonas();
        panelBusquedaRapida.Visible = pMostrar;
        Mostrado = pMostrar;
        hfCodigo.Value = pctlCodigo;
        hfIdentificacion.Value = pctlIdentificacion;
        hfTipoIdentificacion.Value = pctlTipoIdentificacion;
        hfNombre.Value = pctlNombre;
        hfApellido.Value = pctlApellido;
        ViewState["MostrarBusqueda"] = pMostrar;
    }
    public void Motrar(Boolean pMostrar, String pctlCodigo, String pctlIdentificacion, String pctlTipoIdentificacion, String pctlNombre, String pctlApellido,String pctldireccion,String pctltelefono,String pctlciudad)
    {
        InicializarGridPersonas();
        panelBusquedaRapida.Visible = pMostrar;
        Mostrado = pMostrar;
        hfCodigo.Value = pctlCodigo;
        hfIdentificacion.Value = pctlIdentificacion;
        hfTipoIdentificacion.Value = pctlTipoIdentificacion;
        hfNombre.Value = pctlNombre;
        hfApellido.Value = pctlApellido;
        hfDireccion.Value = pctldireccion;
        hftelefono.Value = pctltelefono;
        hfciudad.Value = pctlciudad;
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
            if (!string.IsNullOrWhiteSpace(txtCod.Text))
                persona.cod_persona = Convert.ToInt64(txtCod.Text.Trim());
            if (!string.IsNullOrWhiteSpace(txtIde.Text))
                persona.identificacion = txtIde.Text.Trim();
            if (!string.IsNullOrWhiteSpace(txtNom.Text))
                persona.nombres = txtNom.Text.Trim().ToUpperInvariant();
            if (!string.IsNullOrWhiteSpace(txtApe.Text))
                persona.apellidos = txtApe.Text.Trim().ToUpperInvariant();
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
            rowIndex = rowIndex - (gvPersonas.PageIndex * gvPersonas.PageSize);
            gvPersonas.Rows[rowIndex].BackColor = System.Drawing.Color.DarkBlue;
            gvPersonas.Rows[rowIndex].ForeColor = System.Drawing.Color.White;
            panelBusquedaRapida.Visible = false;
            if (hfCodigo.Value != "")
            {
                TextBox txtCodigo;
                if (this.NamingContainer.ToString().Contains("GridViewRow"))
                {
                    GridViewRow mpContentPlaceHolder = (GridViewRow)this.NamingContainer;
                    txtCodigo = (TextBox)mpContentPlaceHolder.FindControl(hfCodigo.Value);
                }
                else if (this.NamingContainer.ToString().Contains("ctlpersonaed") || this.NamingContainer.ToString().Contains("ctlproveedor"))
                {
                    Control mpControl = (Control)this.NamingContainer;
                    txtCodigo = (TextBox)mpControl.FindControl(hfCodigo.Value);
                }
                else
                {
                    ContentPlaceHolder mpContentPlaceHolder = (ContentPlaceHolder)this.NamingContainer;
                    txtCodigo = (TextBox)mpContentPlaceHolder.FindControl(hfCodigo.Value);
                }
                if (txtCodigo != null)
                {
                    txtCodigo.Text = gvPersonas.Rows[rowIndex].Cells[1].Text;
                }
            }
            if (hfIdentificacion.Value != "")
            {
                TextBox txtIdentificacion, txtIdentificacion2;
                if (this.NamingContainer.ToString().Contains("GridViewRow"))
                {
                    GridViewRow mpContentPlaceHolder = (GridViewRow)this.NamingContainer;
                    txtIdentificacion = (TextBox)mpContentPlaceHolder.FindControl(hfIdentificacion.Value);
                    txtIdentificacion2 = (TextBox)mpContentPlaceHolder.FindControl(hfIdentificacion2.Value);
                }
                else if (this.NamingContainer.ToString().Contains("System.Web.UI.WebControls.ContentPlaceHolder"))
                {
                    txtIdentificacion = (TextBox)this.NamingContainer.FindControl(hfIdentificacion.Value);
                    txtIdentificacion2 = (TextBox)this.NamingContainer.FindControl(hfIdentificacion2.Value);
                    if (txtIdentificacion == null || txtIdentificacion2 == null)
                    { 
                        ContentPlaceHolder mpContentPlaceHolder = (ContentPlaceHolder)this.NamingContainer;
                        txtIdentificacion = (TextBox)mpContentPlaceHolder.FindControl(hfIdentificacion.Value);
                        txtIdentificacion2 = (TextBox)mpContentPlaceHolder.FindControl(hfIdentificacion2.Value);
                    }
                }            
                else if (this.NamingContainer.ToString().Contains("ctlpersonaed") || this.NamingContainer.ToString().Contains("ctlproveedor"))
                {
                    Control mpControl = (Control)this.NamingContainer;
                    txtIdentificacion = (TextBox)mpControl.FindControl(hfIdentificacion.Value);
                    txtIdentificacion2 = (TextBox)mpControl.FindControl(hfIdentificacion2.Value);
                }                
                else
                {                    
                    ContentPlaceHolder mpContentPlaceHolder = (ContentPlaceHolder)this.NamingContainer;
                    txtIdentificacion = (TextBox)mpContentPlaceHolder.FindControl(hfIdentificacion.Value);
                    txtIdentificacion2 = (TextBox)mpContentPlaceHolder.FindControl(hfIdentificacion2.Value);
                }
                if (txtIdentificacion != null)
                {
                    txtIdentificacion.Text = gvPersonas.Rows[rowIndex].Cells[2].Text;                    
                }
                if(txtIdentificacion2 != null)
                    txtIdentificacion2.Text = gvPersonas.Rows[rowIndex].Cells[2].Text;
            }
            if (hfTipoIdentificacion.Value != "")
            {
                DropDownList ddlTipoIdent;
                if (this.NamingContainer.ToString().Contains("ctlpersonaed"))
                {
                    Control mpControl = (Control)this.NamingContainer;
                    ddlTipoIdent = (DropDownList)mpControl.FindControl(hfTipoIdentificacion.Value);

                    if (ddlTipoIdent != null)
                    {
                        ddlTipoIdent.SelectedValue = gvPersonas.DataKeys[rowIndex].Values[0].ToString(); 
                    }
                }
                else
                {
                    ContentPlaceHolder mpContentPlaceHolder = (ContentPlaceHolder)this.NamingContainer;
                    DropDownList ddlTipoIdentificacion = (DropDownList)mpContentPlaceHolder.FindControl(hfTipoIdentificacion.Value);
                    if (ddlTipoIdentificacion != null)
                    {
                        Xpinn.Comun.Entities.TipoIdentificacion tipoIden = new Xpinn.Comun.Entities.TipoIdentificacion();
                        Xpinn.Comun.Services.TipoIdentificacionService servicioTipoIden = new Xpinn.Comun.Services.TipoIdentificacionService();
                        if (gvPersonas.Rows[rowIndex].Cells[3].Text.Trim() != "")
                        {
                            tipoIden = servicioTipoIden.ConsultarTipoIdentificacion(gvPersonas.Rows[rowIndex].Cells[3].Text, (Usuario)Session["Usuario"]);
                            if (tipoIden != null)
                                ddlTipoIdentificacion.SelectedValue = Convert.ToString(tipoIden.IdTipoIdentificacion);
                        }
                    }
                }
            }
            if (hfNombre.Value != "")
            {
                TextBox txtNombres, txtNombres2;
                if (this.NamingContainer.ToString().Contains("GridViewRow"))
                {
                    GridViewRow mpContentPlaceHolder = (GridViewRow)this.NamingContainer;
                    txtNombres = (TextBox)mpContentPlaceHolder.FindControl(hfNombre.Value);
                    txtNombres2 = (TextBox)mpContentPlaceHolder.FindControl(hfNombre2.Value);
                }
                else if (this.NamingContainer.ToString().Contains("ctlpersonaed") || this.NamingContainer.ToString().Contains("ctlproveedor"))
                {
                    Control mpControl = (Control)this.NamingContainer;
                    txtNombres = (TextBox)mpControl.FindControl(hfNombre.Value);
                    txtNombres2 = (TextBox)mpControl.FindControl(hfNombre2.Value);
                }
                else
                {
                    ContentPlaceHolder mpContentPlaceHolder = (ContentPlaceHolder)this.NamingContainer;
                    txtNombres = (TextBox)mpContentPlaceHolder.FindControl(hfNombre.Value);
                    txtNombres2 = (TextBox)mpContentPlaceHolder.FindControl(hfNombre2.Value);
                }
                if (txtNombres != null)
                {
                    if (hfApellido.Value != "")
                        txtNombres.Text = Server.HtmlDecode(gvPersonas.Rows[rowIndex].Cells[4].Text);
                    else
                    {
                        string Nombre = Server.HtmlDecode(gvPersonas.Rows[rowIndex].Cells[4].Text.Replace("&nbsp;", "").Trim());
                        string Apellidos = Server.HtmlDecode(gvPersonas.Rows[rowIndex].Cells[5].Text.Replace("&nbsp;", "").Trim());
                        if (Nombre == Apellidos)
                            txtNombres.Text = Server.HtmlDecode(Nombre);
                        else
                            txtNombres.Text = Server.HtmlDecode(Nombre + ' ' + Apellidos);
                    }
                }
                if (txtNombres2 != null)
                {
                    if (hfApellido.Value != "")
                        txtNombres2.Text = Server.HtmlDecode(gvPersonas.Rows[rowIndex].Cells[4].Text);
                    else
                    {
                        string Nombre = Server.HtmlDecode(gvPersonas.Rows[rowIndex].Cells[4].Text.Replace("&nbsp;", "").Trim());
                        string Apellidos = Server.HtmlDecode(gvPersonas.Rows[rowIndex].Cells[5].Text.Replace("&nbsp;", "").Trim());
                        if (Nombre == Apellidos)
                           txtNombres2.Text = Server.HtmlDecode(Nombre);
                        else
                            txtNombres2.Text = Server.HtmlDecode(Nombre + ' ' + Apellidos);                        
                    }
                }                
            }
            if (hfApellido.Value != "")
            {
                TextBox txtApellidos;
                if (this.NamingContainer.ToString().Contains("GridViewRow"))
                {
                    GridViewRow mpContentPlaceHolder = (GridViewRow)this.NamingContainer;
                    txtApellidos = (TextBox)mpContentPlaceHolder.FindControl(hfApellido.Value);
                }
                else if (this.NamingContainer.ToString().Contains("ctlpersonaed"))
                {
                    Control mpControl = (Control)this.NamingContainer;
                    txtApellidos = (TextBox)mpControl.FindControl(hfApellido.Value);
                }
                else
                {
                    ContentPlaceHolder mpContentPlaceHolder = (ContentPlaceHolder)this.NamingContainer;
                    txtApellidos = (TextBox)mpContentPlaceHolder.FindControl(hfApellido.Value);
                }
                if (txtApellidos != null)
                {
                    txtApellidos.Text = Server.HtmlDecode(gvPersonas.Rows[rowIndex].Cells[5].Text);
                }
            }
           
            if (hfDireccion.Value != "")
            {
                TextBox txtDireccion;
                if (this.NamingContainer.ToString().Contains("GridViewRow"))
                {
                    GridViewRow mpContentPlaceHolder = (GridViewRow)this.NamingContainer;
                    txtDireccion = (TextBox)mpContentPlaceHolder.FindControl(hfDireccion.Value);
                }
                else if (this.NamingContainer.ToString().Contains("ctlpersonaed"))
                {
                    Control mpControl = (Control)this.NamingContainer;
                    txtDireccion = (TextBox)mpControl.FindControl(hfDireccion.Value);
                }
                else
                {
                    ContentPlaceHolder mpContentPlaceHolder = (ContentPlaceHolder)this.NamingContainer;
                    txtDireccion = (TextBox)mpContentPlaceHolder.FindControl(hfDireccion.Value);
                }
                if (txtDireccion != null)
                {
                    txtDireccion.Text = Server.HtmlDecode(gvPersonas.Rows[rowIndex].Cells[7].Text);
                }
            }

            if (hftelefono.Value != "")
            {
                TextBox txtTelefono;
                if (this.NamingContainer.ToString().Contains("GridViewRow"))
                {
                    GridViewRow mpContentPlaceHolder = (GridViewRow)this.NamingContainer;
                    txtTelefono = (TextBox)mpContentPlaceHolder.FindControl(hftelefono.Value);
                }
                else if (this.NamingContainer.ToString().Contains("ctlpersonaed"))
                {
                    Control mpControl = (Control)this.NamingContainer;
                    txtTelefono = (TextBox)mpControl.FindControl(hftelefono.Value);
                }
                else
                {
                    ContentPlaceHolder mpContentPlaceHolder = (ContentPlaceHolder)this.NamingContainer;
                    txtTelefono = (TextBox)mpContentPlaceHolder.FindControl(hftelefono.Value);
                }
                if (txtTelefono != null)
                {
                    txtTelefono.Text = Server.HtmlDecode(gvPersonas.Rows[rowIndex].Cells[8].Text);
                }
            }
            if (hfciudad.Value != "")
            {
                TextBox txtCiudad;
                if (this.NamingContainer.ToString().Contains("GridViewRow"))
                {
                    GridViewRow mpContentPlaceHolder = (GridViewRow)this.NamingContainer;
                    txtCiudad = (TextBox)mpContentPlaceHolder.FindControl(hfciudad.Value);
                }
                else if (this.NamingContainer.ToString().Contains("ctlpersonaed"))
                {
                    Control mpControl = (Control)this.NamingContainer;
                    txtCiudad = (TextBox)mpControl.FindControl(hfciudad.Value);
                }
                else
                {
                    ContentPlaceHolder mpContentPlaceHolder = (ContentPlaceHolder)this.NamingContainer;
                    txtCiudad = (TextBox)mpContentPlaceHolder.FindControl(hfciudad.Value);
                }
                if (txtCiudad != null)
                {
                    txtCiudad.Text = Server.HtmlDecode(gvPersonas.Rows[rowIndex].Cells[9].Text);
                }
            }


            if (eventotxtIdentificacion_TextChanged != null)
                eventotxtIdentificacion_TextChanged(sender, e);
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
