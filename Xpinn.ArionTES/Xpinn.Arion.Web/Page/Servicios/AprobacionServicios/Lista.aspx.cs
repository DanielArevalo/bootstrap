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
            VisualizarOpciones(AproServicios.CodigoPrograma, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoGuardar += btnGrabar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
            ctlMensaje.eventoClick += btnContinuarMen_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AproServicios.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                cargarDropdown();
                txtFechaApro.Text = DateTime.Now.ToShortDateString();
                Actualizar();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AproServicios.CodigoPrograma, "Page_Load", ex);
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


    Boolean ValidarDatos()
    {
        if (txtFechaApro.Text == "")
        { 
            VerError("Ingrese la fecha de Aprobación");
            txtFechaApro.Focus();
            return false;
        }
        int cont = 0;
        foreach (GridViewRow rFila in gvLista.Rows)
        {
            CheckBox chkAprobar = (CheckBox)rFila.FindControl("chkAprobar");
            if (chkAprobar.Checked)
                cont++;
        }
        if (cont == 0)
        {
            VerError("No existen datos seleccionados por Aprobar");
            return false;
        }
        return true;
    }

    protected void btnGrabar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        if(ValidarDatos())
            ctlMensaje.MostrarMensaje("Desea Aprobar las solicitudes seleccionadas?");        
    }


    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        try
        {
            List<Servicio> lstAprobar = new List<Servicio>();

            foreach (GridViewRow rFila in gvLista.Rows)
            {
                CheckBox chkAprobar = (CheckBox)rFila.FindControl("chkAprobar");
                if (chkAprobar.Checked)
                {
                    int Num = Convert.ToInt32(rFila.Cells[2].Text);

                    Servicio pServ = new Servicio();
                    pServ.numero_servicio = Num;
                    pServ.estado = "A";
                    pServ.fecha_aprobacion = Convert.ToDateTime(txtFechaApro.Text);
                    pServ.saldo = rFila.Cells[11].Text != "&nbsp;" ? Convert.ToDecimal(rFila.Cells[11].Text) :0;
                    pServ.cuotas_pendientes = rFila.Cells[13].Text != "&nbsp;" ? Convert.ToInt32(rFila.Cells[13].Text) : 0;
                    lstAprobar.Add(pServ);
                }
            }
            AproServicios.AprobarSolicitud(lstAprobar, (Usuario)Session["usuario"]);
            Actualizar();            
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AproServicios.CodigoPrograma, "btnContinuarMen_Click", ex);
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
            BOexcepcion.Throw(AproServicios.CodigoPrograma, "gvLista_PageIndexChanging", ex);
        }
    }


    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvLista.Rows[e.NewEditIndex].Cells[2].Text;
        Session[AproServicios.CodigoPrograma + ".id"] = id;
        Navegar(Pagina.Detalle);
    }


    
    private void Actualizar()
    {
        try
        {
            List<Servicio> lstConsulta = new List<Servicio>();
            String filtro = obtFiltro(ObtenerValores());
            DateTime pFechaIni;
            pFechaIni = txtFecha.ToDateTime == null ? DateTime.MinValue : txtFecha.ToDateTime;

            lstConsulta = AproServicios.ListarServicios(filtro,"", pFechaIni, (Usuario)Session["usuario"]);

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

            Session.Add(AproServicios.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AproServicios.CodigoPrograma, "Actualizar", ex);
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

        filtro += " and s.estado = 'S'";

        return filtro;
    }


   
}