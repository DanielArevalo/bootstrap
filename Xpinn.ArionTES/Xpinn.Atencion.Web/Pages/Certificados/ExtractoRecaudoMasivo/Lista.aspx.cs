using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;


partial class Lista : GlobalWeb
{
     xpinnWSEstadoCuenta.WSEstadoCuentaSoapClient RecaudosMasivosServicio = new xpinnWSEstadoCuenta.WSEstadoCuentaSoapClient();


    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            ValidarSession();
            VisualizarTitulo(OptionsUrl.ExtractoRecaudo, "Inf");
            Site toolBar = (Site)Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw("Reporte Extractos Masivos", "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                CargarEmpresa();
                CargarValoresConsulta(pConsulta, "180103");
               // if (Session["180103" + ".consulta"] != null)
                    btnConsultar_Click(sender,e);
                   // Actualizar();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw("Reporte Extractos Masivos", "Page_Load", ex);
        }
    }

    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        xpinnWSLogin.Persona1 DataPersona = new xpinnWSLogin.Persona1();
        DataPersona = (xpinnWSLogin.Persona1)Session["persona"];
        txtNumIdentifi.Text = DataPersona.identificacion;
        if (txtNumIdentifi.Text == "")
        {
            GuardarValoresConsulta(pConsulta, "180103");
            Actualizar();
        }
        else
        {
            xpinnWSEstadoCuenta.RecaudosMasivos vRecaudosMasivos = new xpinnWSEstadoCuenta.RecaudosMasivos();
            if (txtNumIdentifi.Text.Trim() != "")
                vRecaudosMasivos.identificacion = Convert.ToString(txtNumIdentifi.Text.Trim());
            if (txtNumeroRecaudo.Text.Trim() != "")
                vRecaudosMasivos.numero_recaudo = Convert.ToInt64(txtNumeroRecaudo.Text.Trim());
            if (txtFechaPeriodo.TieneDatos == true)
                vRecaudosMasivos.periodo_corte = txtFechaPeriodo.ToDateTime;
            if (ddlEmpresa.SelectedValue != "")
                vRecaudosMasivos.cod_empresa = Convert.ToInt64(ddlEmpresa.SelectedValue);
            //Aca determinamos el estado del recaudo
            if (NomGenerada.Checked == true)
            {
                vRecaudosMasivos.estado = "1";
            }
            else
            {
                vRecaudosMasivos.estado = "2";
            }

            //falta pasar el objeto 
            // Session.Add("RvRecaudosMasivos", vRecaudosMasivos);
            Session["RvRecaudosMasivos"] = vRecaudosMasivos;

            Navegar(Pagina.Detalle);
        }
    } //ImageClickEventArgs

    protected void btnLimpiar_Click(object sender, EventArgs e) //ImageClickEventArgs
    {
        try
        {
           LimpiarValoresConsulta(pConsulta, "180103");
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    } 

    protected void gvLista_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            ConfirmarEliminarFila(e, "btnBorrar");
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw("Reporte Extractos Masivos" + "L", "gvLista_RowDataBound", ex);
        }
    }

    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        String id = gvLista.DataKeys[gvLista.SelectedRow.RowIndex].Value.ToString();
        Session["180103" + ".id"] = id;
        Navegar(Pagina.Detalle);
    }

    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        xpinnWSEstadoCuenta.RecaudosMasivos vRecaudosMasivos = new xpinnWSEstadoCuenta.RecaudosMasivos();
        if (txtNumIdentifi.Text.Trim() != "")
            vRecaudosMasivos.identificacion = Convert.ToString(txtNumIdentifi.Text.Trim());
        if (txtNumeroRecaudo.Text.Trim() != "")
            vRecaudosMasivos.numero_recaudo = Convert.ToInt64(txtNumeroRecaudo.Text.Trim());
        if (txtFechaPeriodo.TieneDatos == true)
            vRecaudosMasivos.periodo_corte = txtFechaPeriodo.ToDateTime;
        if (ddlEmpresa.SelectedValue != "")
            vRecaudosMasivos.cod_empresa = Convert.ToInt64(ddlEmpresa.SelectedValue);
        //Aca determinamos el estado del recaudo
        if (NomGenerada.Checked == true)
        {
            vRecaudosMasivos.estado = "1";
        }
        else
        {
            vRecaudosMasivos.estado = "2";
        }

        //falta pasar el objeto 
        // Session.Add("RvRecaudosMasivos", vRecaudosMasivos);
        Session["Estadomasi"] = vRecaudosMasivos;

        String id = gvLista.Rows[e.NewEditIndex].Cells[0].Text;
        Session["180103" + ".id"] = id;
        Navegar(Pagina.Detalle);
    }

    protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            Int64 id = Convert.ToInt64(e.Keys[0]);
            try
            {
                // RecaudosMasivosServicio.EliminarRecaudosMasivos(id, (Usuario)Session["usuario"]);
            }
            catch (Exception ex)
            {
                VerError(ex.Message);
            }
            Actualizar();
        }
        catch (Xpinn.Util.ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw("Reporte Extractos Masivos", "gvLista_RowDeleting", ex);
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
            BOexcepcion.Throw("Reporte Extractos Masivos", "gvLista_PageIndexChanging", ex);
        }
    }


    /// <summary>
    /// EXTRACTO
    /// </summary>
    private void Actualizar()
    {
        try
        {
            List<xpinnWSEstadoCuenta.RecaudosMasivos> lstConsulta = new List<xpinnWSEstadoCuenta.RecaudosMasivos>();
            lstConsulta = RecaudosMasivosServicio.ListarRecaudoExtracto(ObtenerValores(), Session["sec"].ToString());
            gvLista.PageSize = pageSize;
            gvLista.EmptyDataText = emptyQuery;
            gvLista.DataSource = lstConsulta;

            if (lstConsulta.Count > 0)
            {
                gvLista.Visible = true;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                gvLista.DataBind();
                //ValidarPermisosGrilla(gvLista);
            }
            else
            {
                gvLista.Visible = false;
                lblTotalRegs.Visible = false;
            }

            Session.Add("180103" + ".consulta", 1);
            
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw("Reporte Extractos Masivos", "Actualizar", ex);
        }
    }

    private xpinnWSEstadoCuenta.RecaudosMasivos ObtenerValores()
    {
        //RecaudosMasivos vRecaudosMasivos = new RecaudosMasivos();
        xpinnWSEstadoCuenta.RecaudosMasivos vRecaudosMasivos = new xpinnWSEstadoCuenta.RecaudosMasivos();

        if (txtNumIdentifi.Text.Trim() != "")
            vRecaudosMasivos.identificacion = Convert.ToString(txtNumIdentifi.Text.Trim());
        if (txtNumeroRecaudo.Text.Trim() != "")
            vRecaudosMasivos.numero_recaudo = Convert.ToInt64(txtNumeroRecaudo.Text.Trim());
        if (txtFechaPeriodo.TieneDatos == true)
            vRecaudosMasivos.periodo_corte = txtFechaPeriodo.ToDateTime;
        if (ddlEmpresa.SelectedValue != "")
            vRecaudosMasivos.cod_empresa = Convert.ToInt64(ddlEmpresa.SelectedValue);
        //Aca determinamos el estado del recaudo
        if (NomGenerada.Checked == true)
        {
            vRecaudosMasivos.estado = "1";
        }
        else
        {
            vRecaudosMasivos.estado = "2";
        }
        return vRecaudosMasivos;
    }

    private void CargarEmpresa()
    {
        try
        {
            // Xpinn.Tesoreria.Services.RecaudosMasivosService recaudoServicio = new Xpinn.Tesoreria.Services.RecaudosMasivosService();
            //List<Xpinn.Tesoreria.Entities.EmpresaRecaudo> lstModulo = new List<Xpinn.Tesoreria.Entities.EmpresaRecaudo>();
            List<xpinnWSEstadoCuenta.EmpresaRecaudo> lstModulo = new List<xpinnWSEstadoCuenta.EmpresaRecaudo>();

            lstModulo = RecaudosMasivosServicio.ListarEmpresaRecaudo(null, Session["sec"].ToString());

            ddlEmpresa.DataSource = lstModulo;
            ddlEmpresa.DataTextField = "nom_empresa";
            ddlEmpresa.DataValueField = "cod_empresa";
            ddlEmpresa.DataBind();

            ddlEmpresa.Items.Insert(0, "");
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw("Reporte Extractos Masivos", "CargarEmpresa", ex);
        }
    }
}