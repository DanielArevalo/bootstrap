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


partial class Lista : GlobalWeb
{
    AvanceService AproAvance = new AvanceService();
    private CreditoSolicitadoService creditoServicio = new CreditoSolicitadoService();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(AproAvance.CodigoProgramaAprobacion, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            // toolBar.eventoGuardar += btnGrabar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
            ctlMensaje.eventoClick += btnContinuarMen_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AproAvance.CodigoProgramaAprobacion, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                txtFechaApro.Text = DateTime.Now.ToString(gFormatoFecha);
                cargarDropdown();
                Actualizar();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AproAvance.CodigoProgramaAprobacion, "Page_Load", ex);
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

        Xpinn.FabricaCreditos.Services.LineasCreditoService LineaCreditoServicio = new Xpinn.FabricaCreditos.Services.LineasCreditoService();
        LineasCredito eLinea = new LineasCredito();
        eLinea.tipo_linea = 2;
        eLinea.estado = 1;
        ddlLinea.DataSource = LineaCreditoServicio.ListarLineasCredito(eLinea, (Usuario)Session["usuario"]);
        ddlLinea.DataTextField = "nom_linea_credito";
        ddlLinea.DataValueField = "Codigo";
        ddlLinea.Items.Insert(0, new ListItem("Seleccione un item", "0"));
        ddlLinea.SelectedIndex = 0;
        ddlLinea.DataBind();


        //PoblarLista("lineascredito", ddlLinea);

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


    protected void btnGrabar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        if (panelGrilla.Visible == true)
            if (txtFechaApro.Text != "")
                ctlMensaje.MostrarMensaje("Desea Aprobar las solicitudes seleccionadas?");
            else
                VerError("Ingrese la fecha de Aprobación");
        else
            return;
    }


    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        try
        {
            foreach (GridViewRow rFila in gvLista.Rows)
            {
                CheckBoxGrid chkAprobar = (CheckBoxGrid)rFila.FindControl("chkAprobar");
                CheckBoxGrid chkNegar = (CheckBoxGrid)rFila.FindControl("chkNegar");


                Avance pServ = new Avance();
                pServ.idavance = Convert.ToInt32(rFila.Cells[8].Text);

                if (chkAprobar.Checked)
                {
                    ControlCreditos pControl = new ControlCreditos();
                    pControl.numero_radicacion = Convert.ToInt64(rFila.Cells[2].Text);
                    pControl.codtipoproceso = "2";
                    pControl.fechaproceso = Convert.ToDateTime(txtFechaApro.Text);
                    //pControl.cod_persona = 0;
                    pControl.cod_motivo = 0;
                    pControl.observaciones = null;
                    pControl.anexos = null;
                    pControl.nivel = 0;
                    pControl.fechaconsulta_dat = DateTime.Now;
                    AproAvance.CrearControlCreditos(pControl, (Usuario)Session["usuario"]);

                    pServ.estado = "A";

                    //MODIFICANDO EL GIRO
                    Usuario pUsu = (Usuario)Session["usuario"];
                    Giro vDetalle = new Giro();
                    Giro pGiro = new Giro();
                    vDetalle = AproAvance.ConsultarFormaDesembolso(pControl.numero_radicacion, (Usuario)Session["usuario"]);
                    if (vDetalle.idgiro != 0)
                        pGiro.idgiro = vDetalle.idgiro;
                    pGiro.fec_apro = DateTime.Now;
                    pGiro.usu_apro = pUsu.nombre;
                    pGiro.valor = Convert.ToInt64(rFila.Cells[10].Text.Trim().Replace("$", "").Replace(".", "").Replace(",", ""));
                    pGiro.estadogi = 1;
                    pGiro.numero_radicacion = pControl.numero_radicacion;

                    AproAvance.ModificarGiro(pGiro, (Usuario)Session["usuario"]);
                }
                if (chkNegar.Checked)
                {
                    pServ.estado = "N";
                }

                pServ.fecha_aprobacion = Convert.ToDateTime(txtFechaApro.Text);
                AproAvance.GrabarAprobacionDavance(pServ, (Usuario)Session["usuario"]);



            }

            Actualizar();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AproAvance.CodigoProgramaAprobacion, "btnContinuarMen_Click", ex);
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
            BOexcepcion.Throw(AproAvance.CodigoProgramaAprobacion, "gvLista_PageIndexChanging", ex);
        }
    }


    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        //String id = gvLista.Rows[e.NewEditIndex].Cells[2].Text;
        String id = gvLista.Rows[e.NewEditIndex].Cells[2].Text;
        Session[AproAvance.CodigoProgramaAprobacion + ".id"] = id;
        Session["codigocliente"] = Convert.ToInt64(gvLista.Rows[e.NewEditIndex].Cells[4].Text);
        Session["numavanace"] = Convert.ToInt64(gvLista.Rows[e.NewEditIndex].Cells[10].Text);
        Session["numcredito"] = Convert.ToInt64(gvLista.Rows[e.NewEditIndex].Cells[2].Text);
        Navegar(Pagina.Detalle);
    }



    private void Actualizar()
    {
        try
        {
            List<Avance> lstConsulta = new List<Avance>();
            String filtro = obtFiltro(ObtenerValores());
            DateTime pFechaIni;
            pFechaIni = txtFecha.ToDateTime == null ? DateTime.MinValue : txtFecha.ToDateTime;

            lstConsulta = AproAvance.ListarCreditoXaprobar(ObtenerValores(), pFechaIni, (Usuario)Session["usuario"], filtro);
            //Avance avances = null;
            //foreach (Avance avan in lstConsulta)
            //{
            //    if (avan.idavance != null)
            //    {
            //           if(avan.aprobar_avances==1)

            //            break;


            //    }

            //}

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

            Session.Add(AproAvance.CodigoProgramaAprobacion + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AproAvance.CodigoProgramaAprobacion, "Actualizar", ex);
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
            filtro += " and p.primer_nombre ||' '|| p.segundo_nombre||' '|| p.primer_apellido||' '||p.segundo_apellido like '%" + credit.nombre + "%'";
        if (txtCodigoNomina.Text != "")
            filtro += " and p.cod_nomina like '%" + credit.cod_nomina + "%'";

        if (ddlLinea.SelectedIndex != 0)
            filtro += " and c.COD_LINEA_CREDITO = '" + credit.cod_linea_credito + "'";

        if (ddlOficina.SelectedIndex != 0)
            filtro += " and c.cod_oficina = " + credit.cod_oficina;

        CreditoSolicitado creditos = new CreditoSolicitado();
        if (string.IsNullOrEmpty(ddlLinea.SelectedValue))
            return null;
        creditos.cod_linea_credito = ddlLinea.SelectedValue;
        creditos = creditoServicio.ConsultarParamAprobacion(creditos, (Usuario)Session["usuario"]);
        if (creditos.aprobar_avances == 0)
        {

            filtro += " and a.estado != 'C'";
            filtro += " and a.estado != 'S'";
        }
        if (creditos.aprobar_avances == 1)
        {
            filtro += " and a.estado = 'S'";
        }

        return filtro;
    }



    protected void chkNegar_CheckedChanged(object sender, EventArgs e)
    {
        CheckBoxGrid chkNegar = (CheckBoxGrid)sender;
        int nItem = Convert.ToInt32(chkNegar.CommandArgument);

        CheckBoxGrid chkAprobar = (CheckBoxGrid)gvLista.Rows[nItem].FindControl("chkAprobar");

        if (chkNegar.Checked)
            chkAprobar.Checked = false;
    }


    protected void chkAprobar_CheckedChanged(object sender, EventArgs e)
    {
        CheckBoxGrid chkAprobar = (CheckBoxGrid)sender;
        int nItem = Convert.ToInt32(chkAprobar.CommandArgument);

        CheckBoxGrid chkNegar = (CheckBoxGrid)gvLista.Rows[nItem].FindControl("chkNegar");
        if (chkAprobar.Checked)
            chkNegar.Checked = false;

    }


}