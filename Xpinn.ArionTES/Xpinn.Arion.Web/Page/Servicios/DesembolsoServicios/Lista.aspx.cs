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
using Xpinn.Servicios.Entities;
using Xpinn.Servicios.Services;


partial class Lista : GlobalWeb
{
    AprobacionServiciosServices AproServicios = new AprobacionServiciosServices();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(AproServicios.CodigoProgramaDesem, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AproServicios.CodigoProgramaDesem, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                cargarDropdown();
                if (Request.QueryString["num_serv"] != null)
                    txtNumServ.Text = Request.QueryString["num_serv"].ToString();                 
                Actualizar();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AproServicios.CodigoProgramaDesem, "Page_Load", ex);
        }
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        txtNumServ.Text = ""; txtFecha.Text = ""; ddlLinea.SelectedIndex = 0; txtIdentificacion.Text = ""; txtNombre.Text = "";
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
        PoblarLista("lineasservicios", ddlLinea);

        ddlOrdenar.Items.Insert(0, new ListItem("Seleccione un item","0"));
        ddlOrdenar.Items.Insert(1, new ListItem("Número de Servicio", "NUMERO_SERVICIO"));
        ddlOrdenar.Items.Insert(2, new ListItem("Fecha de Solicitud", "FECHA_SOLICITUD"));
        ddlOrdenar.Items.Insert(3, new ListItem("Linea de Servicio", "COD_LINEA_SERVICIO"));
        ddlOrdenar.Items.Insert(4, new ListItem("Valor Total", "VALOR_TOTAL"));
        ddlOrdenar.Items.Insert(5, new ListItem("Numero de Cuotas", "NUMERO_CUOTAS"));
        ddlOrdenar.SelectedIndex = 0;
        ddlOrdenar.DataBind();
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
            BOexcepcion.Throw(AproServicios.CodigoProgramaDesem, "gvLista_PageIndexChanging", ex);
        }
    }


    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvLista.DataKeys[e.NewEditIndex].Values[0].ToString();
        Session[AproServicios.CodigoProgramaDesem + ".id"] = id;
        Navegar(Pagina.Nuevo);
    }


    
    private void Actualizar()
    {
        try
        {
            List<Servicio> lstConsulta = new List<Servicio>();
            String filtro = obtFiltro(ObtenerValores());
            DateTime pFechaIni;
            pFechaIni = txtFecha.ToDateTime == null ? DateTime.MinValue : txtFecha.ToDateTime;
            string pOrden = "";
            if (ddlOrdenar.SelectedIndex != 0)
                pOrden = ddlOrdenar.SelectedValue;
            lstConsulta = AproServicios.ListarServicios(filtro,pOrden, pFechaIni, (Usuario)Session["usuario"]);

            gvLista.PageSize = 15;
            String emptyQuery = "Fila de datos vacia";
            gvLista.EmptyDataText = emptyQuery;

            if (lstConsulta.Count > 0)
            {
                gvLista.DataSource = lstConsulta;
                gvLista.DataBind();
                panelGrilla.Visible = true;                
                lblTotalRegs.Visible = true;                               
            }
            else
            {
                panelGrilla.Visible = false;                
                lblTotalRegs.Visible = false;
            }
            lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
            Session.Add(AproServicios.CodigoProgramaDesem + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AproServicios.CodigoProgramaDesem, "Actualizar", ex);
        }
    }

    private Servicio ObtenerValores()
    {
        Servicio vCuentas = new Servicio();
        if (txtNumServ.Text.Trim() != "")
            vCuentas.numero_servicio = Convert.ToInt32(txtNumServ.Text.Trim());        
        if (ddlLinea.SelectedIndex != 0)
            vCuentas.cod_linea_servicio = ddlLinea.SelectedValue;
        if (txtIdentificacion.Text != "")
            vCuentas.identificacion = txtIdentificacion.Text;
        
        if (txtNombre.Text.Trim() != "")
            vCuentas.nombre = txtNombre.Text.Trim().ToUpper();

        return vCuentas;
    }



    private string obtFiltro(Servicio Cuentas)
    {
        Configuracion conf = new Configuracion();

        String filtro = String.Empty;

        if (txtNumServ.Text.Trim() != "")
            filtro += " and s.numero_servicio = " + Cuentas.numero_servicio;
        if (ddlLinea.SelectedIndex != 0)
            filtro += " and s.COD_LINEA_SERVICIO = " + Cuentas.cod_linea_servicio;
        if (txtIdentificacion.Text != "")
            filtro += " and p.identificacion like '%" + Cuentas.identificacion + "%'";
        if (txtNombre.Text.Trim() != "")
            filtro += " and p.primer_nombre ||' '|| p.segundo_nombre||' '|| p.primer_apellido||' '||p.segundo_apellido like '%" + Cuentas.nombre + "%'";
        if (txtCodigoNomina.Text != "")
            filtro += " and p.cod_nomina like '%" + txtCodigoNomina.Text + "%'";

        filtro += " and s.estado = 'A'";

        return filtro;
    }


   
}