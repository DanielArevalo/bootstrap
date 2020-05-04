using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using Xpinn.FabricaCreditos.Services;
using Xpinn.FabricaCreditos.Entities;
using Xpinn.Asesores.Services;
using Xpinn.Asesores.Entities;

public partial class ListaGarantias : GlobalWeb
{
    GarantiaService _garantiasservicio = new GarantiaService();
    Usuario _usuario;


    #region  Eventos Carga Inicial


    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(_garantiasservicio.CodigoPrograma, "L");

            Site toolBar = (Site)Master;

            toolBar.eventoCancelar += btnCancelar_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoNuevo += btnNuevo_Click;
            toolBar.eventoExportar += btnExportar_Click;
            ctlMensaje.eventoClick += btnContinuarMen_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_garantiasservicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }
    

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            _usuario = (Usuario)Session["Usuario"];

            if (!IsPostBack)
            {
                LlenarDDLLineaCredito();

                txtFechaIni.Attributes.Add("readonly", "readonly");
                txtFechaFin.Attributes.Add("readonly", "readonly");
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_garantiasservicio.CodigoPrograma, "Page_Load", ex);
        }
    }


    #endregion


    #region EventoBotones


    protected void btnCancelar_Click(object sender, ImageClickEventArgs e)
    {
        Navegar("../../../General/Global/inicio.aspx");
    }


    private void btnNuevo_Click(object sender, ImageClickEventArgs e)
    {
        Navegar(Pagina.Nuevo);
    }


    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        VerError("");
        LlenarCreditosConGarantia();
    }

    private void btnExportar_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (gvGarantia.Rows.Count > 0 && ViewState["DTGarantia"] != null)
            {
                gvGarantia.AllowPaging = false;
                gvGarantia.DataSource = ViewState["DTGarantia"];
                gvGarantia.DataBind();
                ExportarGridCSVDirecto(gvGarantia, "Garantias");
                gvGarantia.AllowPaging = true;
                gvGarantia.DataSource = ViewState["DTGarantia"];
                gvGarantia.DataBind();
            }
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }

    #endregion


    #region Eventos Grilla


    protected void gvGarantias_RowEditing(object sender, GridViewEditEventArgs e)
    {
        string Nradicacion = gvGarantia.Rows[e.NewEditIndex].Cells[3].Text;
        string cod_persona = string.Empty;

        try
        {
            cod_persona = _garantiasservicio.ConsultarCliente(Nradicacion, _usuario); // No se me ocurrio otro approach de sacar el cod_deudor sin complicaciones
        }
        catch (Exception ex)
        {
            VerError("gvGarantias_RowEditing: " + ex.Message);
            return;
        }

        Session[_garantiasservicio.CodigoPrograma2 + ".No.Radicacion.ListaGarantia"] = Nradicacion;
        Session[_garantiasservicio.CodigoPrograma2 + ".codPersona"] = cod_persona;

        // Remuevo la sesion de "Nueva Garantía" para evitar conflictos cuando entre a Detalle.aspx
        Session.Remove(_garantiasservicio.CodigoPrograma2 + ".No.Radicacion.Nuevo");

        Navegar(Pagina.Detalle);
    }


    protected void gvGarantia_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvGarantia.PageIndex = e.NewPageIndex;
        LlenarCreditosConGarantia();
    }


    protected void OnRowCommandDeleting(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Delete")
        {
            if (e.CommandArgument.ToString() != "")
            {
                try
                {
                    long idBorrar = Convert.ToInt64(gvGarantia.DataKeys[Convert.ToInt32(e.CommandArgument)].Value);

                    ViewState.Add("idBorrar", idBorrar);

                    ctlMensaje.MostrarMensaje("Seguro que deseas eliminar esta garantía?");
                }
                catch { }
            }
        }
    }

    private void btnContinuarMen_Click(object sender, EventArgs e)
    {
        if (ViewState["idBorrar"] != null)
        {
            long idBorrar = Convert.ToInt64(ViewState["idBorrar"]);

            try
            {
                _garantiasservicio.EliminarGarantia(idBorrar, _usuario);
                LlenarCreditosConGarantia();
            }
            catch (Exception ex)
            {
                VerError("Error al borrar las garantías, " + ex.Message);
            }
        }
    }


    protected void gvGarantias_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }

    #endregion


    #region Metodos de Llenado


    // Lleno el DDL de TIPO DE LINEA
    protected void LlenarDDLLineaCredito()
    {
        Persona1Service DatosClienteServicio = new Persona1Service();
        List<Persona1> lstDatosSolicitud = new List<Persona1>(1);  //Lista de los menus desplegables
        string ListaSolicitada = "LineasCredito";

        try
        {
            lstDatosSolicitud = DatosClienteServicio.ListasDesplegables(ListaSolicitada, _usuario);
            ddlLineasCred.DataSource = lstDatosSolicitud;
            ddlLineasCred.DataTextField = "ListaDescripcion";
            ddlLineasCred.DataValueField = "ListaIdStr";
            ddlLineasCred.DataBind();
        }
        catch (Exception ex)
        {

            VerError("LlenarComboLineasCred:  " + ex.Message);
        }

        ddlLineasCred.Items.Insert(0, new ListItem("Todas las líneas", "0"));
    }


    // Obtengo el filtro y hago el query de los Creditos CON GARANTIA
    private void LlenarCreditosConGarantia()
    {
        string filtro = ObtenerFiltroToQuery();

        List<Garantia> lstConsulta = new List<Garantia>(1);

        try
        {
            lstConsulta = _garantiasservicio.ListarFullGarantias(filtro, _usuario);
        }
        catch (Exception ex)
        {
            VerError("LlenarCreditosConGarantia:  " + ex.Message);
        }

        if (lstConsulta.Count == 0)
        {
            lblAvisoNoResultadoGrilla.Visible = true;
            lblNumeroRegistros.Visible = false;
        }
        else
        {
            lblAvisoNoResultadoGrilla.Visible = false;
            lblNumeroRegistros.Text = "Número de registros encontrados: " + lstConsulta.Count.ToString();
            lblNumeroRegistros.Visible = true;
        }
        ViewState["DTGarantia"] = lstConsulta.Count == 0 ? null : lstConsulta;
        gvGarantia.DataSource = lstConsulta;
        gvGarantia.DataBind();
    }


    #endregion


    #region Metodo para obtener filtro de acuerdo a la información suministrada


    // Dependiendo de lo escrito en los campos armo el filtro para filtrar el query a realizar
    private string ObtenerFiltroToQuery()
    {
        string filtro = string.Empty;
        string radicacion = txtNumCredito.Text.Trim();
        string estado = ddlEstado.SelectedValue;
        string tipoGarantia = ddlTipoGarantia.SelectedValue;
        string lineaCredito = ddlLineasCred.SelectedValue;
        string montoInicial = txtMontoIni.Text.Trim();
        string montoMax = txtMontoMax.Text.Trim();
        string plazoInicial = txtPlazoIni.Text.Trim();
        string plazoMax = txtPlazoMax.Text.Trim();
        string fechaGarantiaInicial = txtFechaIni.Text.Trim();
        string FechaGarantiaFinal = txtFechaFin.Text.Trim();

        //Filtro estado
        if (estado != "0")
        {
            filtro += " and g.estado ='" + estado + "'";
        }

        //Filtro tipo garantia
        if (tipoGarantia != "0")
        {
            filtro += " and g.tipo_garantia ='" + tipoGarantia + "'";
        }

        //Filtro linea credito
        if (lineaCredito != "0")
        {
            filtro += " and l.COD_LINEA_CREDITO ='" + lineaCredito + "'";
        }

        // Filtro radicacion
        if (radicacion != "")
        {
            filtro += " and g.numero_radicacion like '%" + radicacion + "%'";
        }

        // identificación deudor
        if (!string.IsNullOrWhiteSpace(txtIdentificacion.Text))
        {
            filtro += " and per.identificacion like '%" + txtIdentificacion.Text.Trim() + "%'";
        }


        // Filtro Monto
        if (montoInicial != "" && montoMax != "")
        {
            filtro += " and g.VALOR_GARANTIA BETWEEN " + montoInicial + " AND " + montoMax;
        }
        else if (montoInicial != "")
        {
            filtro += " and g.VALOR_GARANTIA > " + montoInicial;
        }
        else if (montoMax != "")
        {
            filtro += " and g.VALOR_GARANTIA < " + montoMax;
        }



        // Filtro Plazo
        if (plazoInicial != "" && plazoMax != "")
        {
            filtro += " and c.NUMERO_CUOTAS BETWEEN " + plazoInicial + " AND " + plazoMax;
        }
        else if (plazoInicial != "")
        {
            filtro += " and c.NUMERO_CUOTAS > " + plazoInicial;
        }
        else if (plazoMax != "")
        {
            filtro += " and c.NUMERO_CUOTAS < " + plazoMax;
        }


        // Filtro Fecha Garantia
        if (!string.IsNullOrWhiteSpace(fechaGarantiaInicial) && !string.IsNullOrWhiteSpace(FechaGarantiaFinal))
        {
            filtro += " and g.FECHA_GARANTIA BETWEEN TO_DATE('" + fechaGarantiaInicial + "', 'dd/MM/yyyy') AND TO_DATE('" + FechaGarantiaFinal + "', 'dd/MM/yyyy')";
        }
        else if (!string.IsNullOrWhiteSpace(fechaGarantiaInicial))
        {
            filtro += " and g.FECHA_GARANTIA > TO_DATE('" + fechaGarantiaInicial + "', 'dd/MM/yyyy')";
        }
        else if (!string.IsNullOrWhiteSpace(FechaGarantiaFinal))
        {
            filtro += " and g.FECHA_GARANTIA < TO_DATE('" + FechaGarantiaFinal + "', 'dd/MM/yyyy')";
        }


        // Si la Query esta llena la ordeno de manera que no explote por tener un "and" al iniciar
        if (filtro.StartsWith(" and "))
        {
            filtro = filtro.Remove(0, 4).Insert(0, " WHERE ");
        }

        return filtro;
    }


    #endregion


}