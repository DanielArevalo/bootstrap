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
    DesembolsoAuxilioServices DesemAuxilios = new DesembolsoAuxilioServices();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(DesemAuxilios.CodigoPrograma, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(DesemAuxilios.CodigoPrograma, "Page_PreInit", ex);
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
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(DesemAuxilios.CodigoPrograma, "Page_Load", ex);
        }
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        LimpiarPanel(pConsulta);
        txtFecha.Text = "";
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

        ddlOrdenar.Items.Insert(0, new ListItem("Seleccione un item", "0"));
        ddlOrdenar.Items.Insert(1, new ListItem("Número de Auxilio", "a.numero_auxilio"));
        ddlOrdenar.Items.Insert(2, new ListItem("Fecha de Solicitud", "a.fecha_solicitud"));
        ddlOrdenar.Items.Insert(3, new ListItem("Fecha de Aprobación", "a.fecha_aprobacion"));
        ddlOrdenar.Items.Insert(4, new ListItem("Identificación", "p.identificacion"));
        ddlOrdenar.Items.Insert(5, new ListItem("Linea de Auxilio", "a.cod_linea_auxilio"));
        ddlOrdenar.Items.Insert(6, new ListItem("Valor Solicitado", "a.valor_solicitado"));
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
            BOexcepcion.Throw(DesemAuxilios.CodigoPrograma, "gvLista_PageIndexChanging", ex);
        }
    }


    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        if (!ValidarAccionesGrilla("CONSULTAR"))
        {
            e.NewEditIndex = -1;
            return;
        }
        String id = gvLista.Rows[e.NewEditIndex].Cells[1].Text;
        Session[DesemAuxilios.CodigoPrograma + ".id"] = id;
        Navegar(Pagina.Nuevo);
    }

        
    private void Actualizar()
    {
        try
        {            
            List<SolicitudAuxilio> lstConsulta = new List<SolicitudAuxilio>();
            String filtro = obtFiltro();
            DateTime pFechaIni;
            pFechaIni = txtFecha.ToDateTime == null ? DateTime.MinValue : txtFecha.ToDateTime;
            string orden = "";
            if(ddlOrdenar.SelectedIndex != 0)
                orden = " "+ddlOrdenar.SelectedValue;

            lstConsulta = DesemAuxilios.ListarSolicitudAuxilio(filtro,pFechaIni,orden, (Usuario)Session["usuario"]);

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

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(DesemAuxilios.CodigoPrograma, "Actualizar", ex);
        }
    }
    

    private string obtFiltro()
    {
        Configuracion conf = new Configuracion();

        String filtro = String.Empty;

        if (txtNumAux.Text.Trim() != "")
            filtro += " and a.numero_auxilio = " + txtNumAux.Text.Trim();
        if (ddlLinea.SelectedIndex != 0)
            filtro += " and a.cod_linea_auxilio = " + ddlLinea.SelectedValue;
        if (txtIdentificacion.Text != "")
            filtro += " and p.identificacion like '%" + txtIdentificacion.Text.Trim() + "%'";
        if (txtNombre.Text.Trim() != "")
            filtro += " and p.primer_nombre ||' '|| p.segundo_nombre||' '|| p.primer_apellido||' '||p.segundo_apellido like '%" + txtNombre.Text.Trim() + "%'";
        if (txtCodigoNomina.Text != "")
            filtro += " and p.cod_nomina like '%" + txtCodigoNomina.Text + "%'";

        filtro += " and a.estado = 'A'";

        return filtro;
    }
   
}