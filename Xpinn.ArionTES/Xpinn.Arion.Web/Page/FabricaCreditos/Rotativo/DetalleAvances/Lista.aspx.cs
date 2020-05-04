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
using Xpinn.FabricaCreditos.Entities;
using Xpinn.FabricaCreditos.Services;
using Xpinn.Asesores.Services;
using System.Text;
using System.Web.UI.HtmlControls;

using System.Web.UI.WebControls.WebParts;
partial class Lista : GlobalWeb
{
    MovGralCreditoService AproAvance = new MovGralCreditoService();
    private CreditoSolicitadoService creditoServicio = new CreditoSolicitadoService();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(AproAvance.CodigoProgramaAvances, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AproAvance.CodigoProgramaAvances, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                cargarDropdown();
                Actualizar();
                // Label1.Visible = false;
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AproAvance.CodigoProgramaAvances, "Page_Load", ex);
        }
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        txtNumCred.Text = ""; txtFecha.Text = ""; ddlLinea.SelectedIndex = 0; txtIdentificacion.Text = ""; txtNombre.Text = ""; txtCodigoNomina.Text = "";
        Actualizar();
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        Page.Validate();
        gvLista.Visible = true;
        if (Page.IsValid)
        {
            Actualizar();
        }
    }


    void cargarDropdown()
    {
        //PoblarLista("lineascredito", ddlLinea);
        Xpinn.FabricaCreditos.Services.LineasCreditoService LineaCreditoServicio = new Xpinn.FabricaCreditos.Services.LineasCreditoService();
        LineasCredito eLinea = new LineasCredito();
        eLinea.tipo_linea = 2;
        eLinea.estado = 1;
        ddlLinea.DataSource = LineaCreditoServicio.ListarLineasCredito(eLinea, (Usuario)Session["usuario"]);
        ddlLinea.DataTextField = "nom_linea_credito";
        ddlLinea.DataValueField = "Codigo";
        //  ddlLinea.Items.Insert(0, new ListItem("Seleccione un item", "0"));
        ddlLinea.SelectedIndex = 0;
        ddlLinea.DataBind();

        Xpinn.Caja.Services.OficinaService oficinaServicio = new Xpinn.Caja.Services.OficinaService();
        Xpinn.Caja.Entities.Oficina oficina = new Xpinn.Caja.Entities.Oficina();
        ddlOficina.DataSource = oficinaServicio.ListarOficina(oficina, (Usuario)Session["usuario"]);
        ddlOficina.DataTextField = "nombre";
        ddlOficina.DataValueField = "cod_oficina";
        ddlOficina.Items.Insert(0, new ListItem("Seleccione un item", "0"));
        ddlOficina.SelectedIndex = 0;
        ddlOficina.DataBind();
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



    protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvLista.PageIndex = e.NewPageIndex;
            Actualizar();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AproAvance.CodigoProgramaAvances, "gvLista_PageIndexChanging", ex);
        }
    }


    //protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        String id = gvLista.SelectedRow.Cells[6].Text; 
        Session["codigocliente"] = Convert.ToInt64(gvLista.SelectedRow.Cells[3].Text);
        Session["numcredito"] = Convert.ToInt64(gvLista.SelectedRow.Cells[1].Text);
        Xpinn.Asesores.Entities.Producto entityProducto = new Xpinn.Asesores.Entities.Producto();
        Xpinn.Asesores.Services.EstadoCuentaService serviceEstadoCuenta = new Xpinn.Asesores.Services.EstadoCuentaService();
        Xpinn.Asesores.Entities.Producto producto = new Xpinn.Asesores.Entities.Producto();
        producto.Persona = serviceEstadoCuenta.ConsultarPersona(producto.Persona.IdPersona, (Usuario)Session["usuario"]);
        Session[MOV_GRAL_CRED_PRODUC] = producto;
        Session["Retorno"] = "A";
        Navegar("~/Page/FabricaCreditos/Rotativo/DetalleAvances/Detalle.aspx?radicado=" + gvLista.SelectedRow.Cells[1].Text);
    }



    private void Actualizar()
    {
        try
        {
            List<Avance> lstConsulta = new List<Avance>();
            String filtro = obtFiltro(ObtenerValores());
            AvanceService servRotativo = new AvanceService();
            lstConsulta = servRotativo.ListarCreditoXaprobar(ObtenerValores(), (Usuario)Session["usuario"], filtro);

            gvLista.PageSize = 15;
            String emptyQuery = "Fila de datos vacia";
            gvLista.EmptyDataText = emptyQuery;

            if (lstConsulta.Count > 0)
            {
                gvLista.DataSource = lstConsulta;
                gvLista.DataBind();
                panelGrilla.Visible = true;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();

            }
            else
            {
                panelGrilla.Visible = false;
                lblTotalRegs.Visible = false;
            }

            Session.Add(AproAvance.CodigoProgramaAvances + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AproAvance.CodigoProgramaAvances, "Actualizar", ex);
        }
    }

    private Avance ObtenerValores()
    {
        Avance vAvance = new Avance();
        if (txtNumCred.Text.Trim() != "")
            vAvance.numero_radicacion = Convert.ToInt64(txtNumCred.Text.Trim());
        if (txtIdentificacion.Text != "")
            vAvance.identificacion = txtIdentificacion.Text;
        if (txtNombre.Text.Trim() != "")
            vAvance.nombre = txtNombre.Text.Trim().ToUpper();
        if (ddlLinea.SelectedIndex != 0)
            vAvance.cod_linea_credito = ddlLinea.SelectedValue;
        if (ddlOficina.SelectedIndex != 0)
            vAvance.cod_oficina = Convert.ToInt32(ddlOficina.SelectedValue);
        return vAvance;
    }



    private string obtFiltro(Avance credit)
    {
        String filtro = String.Empty;

        if (txtNumCred.Text.Trim() != "")
            filtro += " and c.numero_radicacion = " + credit.numero_radicacion;
        if (txtIdentificacion.Text != "")
            filtro += " and p.identificacion like '%" + credit.identificacion + "%'";
        if (txtNombre.Text.Trim() != "")
            filtro += " and p.primer_nombre ||' '|| p.segundo_nombre ||' '|| p.primer_apellido ||' '|| p.segundo_apellido like '%" + credit.nombre + "%'";
        if (txtCodigoNomina.Text != "")
            filtro += " and p.cod_nomina like '%" + credit.cod_nomina + "%'";
        if (ddlLinea.SelectedIndex != 0)
            filtro += " and c.cod_linea_credito = '" + credit.cod_linea_credito + "'";
        if (ddlOficina.SelectedIndex != 0)
            filtro += " and c.cod_oficina = " + credit.cod_oficina;
        filtro += " And a.estado In ('C')";
        return filtro;
    }


    public override void VerifyRenderingInServerForm(Control control)
    {
        return;
    }

}