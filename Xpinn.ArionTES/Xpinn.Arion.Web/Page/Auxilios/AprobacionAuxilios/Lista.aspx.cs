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
using Xpinn.Auxilios.Entities;
using Xpinn.Auxilios.Services;


partial class Lista : GlobalWeb
{
    AprobacionAuxilioServices AproAuxilios = new AprobacionAuxilioServices();
    SolicitudAuxilioServices SoliAuxilios = new SolicitudAuxilioServices();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(AproAuxilios.CodigoPrograma, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoGuardar += btnGrabar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
            ctlMensaje.eventoClick += btnContinuarMen_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AproAuxilios.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                cargarDropdown();
                txtFechaApro.Text = DateTime.Now.ToString();
                Actualizar();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AproAuxilios.CodigoPrograma, "Page_Load", ex);
        }
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        txtNumAux.Text = ""; txtFecha.Text = ""; ddlLinea.SelectedIndex = 0; txtIdentificacion.Text = ""; txtNombre.Text = "";
        txtCodigoNomina.Text = "";
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
        PoblarLista("lineasauxilios", "", " ESTADO = 1 ", "2", ddlLinea);
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


    protected void btnGrabar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        if (txtFechaApro.Text != "")
            if(Convert.ToDateTime(txtFechaApro.Text) <= DateTime.Now)
                ctlMensaje.MostrarMensaje("Desea Aprobar las solicitudes seleccionadas?");
            else
                VerError("La fecha de Aprobación no puede ser superior a la fecha actual");
        else
            VerError("Ingrese la fecha de Aprobación");
    }


    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        try
        {           
            foreach (GridViewRow rFila in gvLista.Rows)
            {
                CheckBox chkAprobar = (CheckBox)rFila.FindControl("chkAprobar");
                if (chkAprobar.Checked)
                {
                    int Num = Convert.ToInt32(rFila.Cells[1].Text);

                    AprobacionAuxilio pVar = new AprobacionAuxilio();
                    //----------------------------------

                    pVar.numero_auxilios = Convert.ToInt64(Num);
                    pVar.fecha_aprobacion = DateTime.Now;
                    decimal valor_soli = Convert.ToDecimal(rFila.Cells[7].Text.Replace("$", "").Replace(",", "."));
                    pVar.valor_aprobado = Convert.ToDecimal(valor_soli);

                    Auxilio_Orden_Servicio pOrdenAux = new Auxilio_Orden_Servicio();
                    //MODIFICAR
                    AproAuxilios.AprobarAuxilios(pVar,0,pOrdenAux, (Usuario)Session["usuario"]);

                    AprobacionAuxilio pControl = new AprobacionAuxilio();
                    pControl.idcontrolaux = 0;
                    pControl.numero_auxilios = Num;
                    pControl.codtipo_proceso = 2;
                    pControl.fecha_proceso = DateTime.Now;
                    //CODIGO DE USUARIO EN CAPA DATOS

                    TextBox txtObservaciones = (TextBox)rFila.FindControl("txtObservaciones");
                    if (txtObservaciones.Text != "")
                        pControl.observaciones = txtObservaciones.Text;
                    else
                        pControl.observaciones = null;
                    AproAuxilios.CrearControlAuxilio(pControl, (Usuario)Session["usuario"]);
                }
            }

            Actualizar();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AproAuxilios.CodigoPrograma, "btnContinuarMen_Click", ex);
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
            BOexcepcion.Throw(AproAuxilios.CodigoPrograma, "gvLista_PageIndexChanging", ex);
        }
    }


    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        if (!ValidarAccionesGrilla("UPDATE"))
        {
            e.NewEditIndex = -1;
            return;
        }
        String id = gvLista.Rows[e.NewEditIndex].Cells[1].Text;
        Session[AproAuxilios.CodigoPrograma + ".id"] = id;
        Navegar(Pagina.Nuevo);
    }

        
    private void Actualizar()
    {
        try
        {            
            List<SolicitudAuxilio> lstConsulta = new List<SolicitudAuxilio>();
            String filtro = obtFiltro(ObtenerValores());
            DateTime pFechaIni;
            pFechaIni = txtFecha.ToDateTime == null ? DateTime.MinValue : txtFecha.ToDateTime;

            lstConsulta = SoliAuxilios.ListarSolicitudAuxilio(ObtenerValores(), pFechaIni, (Usuario)Session["usuario"], filtro);

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

            Session.Add(SoliAuxilios.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AproAuxilios.CodigoPrograma, "Actualizar", ex);
        }
    }


    private SolicitudAuxilio ObtenerValores()
    {
        SolicitudAuxilio vCuentas = new SolicitudAuxilio();
        if (txtNumAux.Text.Trim() != "")
            vCuentas.numero_auxilio = Convert.ToInt32(txtNumAux.Text.Trim());
        if (ddlLinea.SelectedIndex != 0)
            vCuentas.cod_linea_auxilio = ddlLinea.SelectedValue;
        if (txtIdentificacion.Text != "")
            vCuentas.identificacion = txtIdentificacion.Text;

        if (txtNombre.Text.Trim() != "")
            vCuentas.nombre = txtNombre.Text.Trim().ToUpper();

        return vCuentas;
    }



    private string obtFiltro(SolicitudAuxilio Cuentas)
    {
        Configuracion conf = new Configuracion();

        String filtro = String.Empty;

        if (txtNumAux.Text.Trim() != "")
            filtro += " and a.numero_auxilio = " + Cuentas.numero_auxilio;
        if (ddlLinea.SelectedIndex != 0)
            filtro += " and a.cod_linea_auxilio = " + Cuentas.cod_linea_auxilio;
        if (txtIdentificacion.Text != "")
            filtro += " and p.identificacion like '%" + Cuentas.identificacion + "%'";
        if (txtNombre.Text.Trim() != "")
            filtro += " and p.primer_nombre ||' '|| p.segundo_nombre||' '|| p.primer_apellido||' '||p.segundo_apellido like '%" + Cuentas.nombre + "%'";
        if (txtCodigoNomina.Text != "")
            filtro += " and p.cod_nomina like '%" + txtCodigoNomina.Text + "%'";

        filtro += " and a.estado = 'S'";

        return filtro;
    }
   
}