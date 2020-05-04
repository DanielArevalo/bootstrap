using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Xpinn.NIIF.Entities;
using Xpinn.NIIF.Services;
using Xpinn.Riesgo.Entities;
using Xpinn.Riesgo.Services;
using Xpinn.Util;

partial class Nuevo : GlobalWeb
{
    RangoPerfilServices _rangoPerfil = new RangoPerfilServices();


    #region Eventos Iniciales


    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            VisualizarOpciones(_rangoPerfil.CodigoPrograma, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoCancelar += btnCancelar_Click;
            toolBar.eventoGuardar += btnGuardar_Click;
            ctlMensaje.eventoClick += btnContinuarMen_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_rangoPerfil.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                ObtenerDatos(idObjeto);   
            }
            else
            {
                Session["Operacion"] = "1";
                //InicializarListado();
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_rangoPerfil.CodigoPrograma, "Page_Load", ex);
        }
    }

    void ObtenerDatos(String idObjeto)
    {
        List<RangoPerfil> lstConsulta = new List<RangoPerfil>();
        lstConsulta = _rangoPerfil.ListarRangosPerfil(ObtenerValores(), Usuario);

        if (lstConsulta.Count > 0)
        {
            gvLista.DataSource = lstConsulta;
            gvLista.DataBind();
            Session["Detalle"] = lstConsulta;
        }
        else
        {
            //InicializarListado();
        }
    }
    RangoPerfil ObtenerValores()
    {
        RangoPerfil rango = new RangoPerfil();


        if (txtCodigo.Text.Trim() != "")
            rango.cod_rango_perfil = Convert.ToInt32(txtCodigo.Text.Trim());

        return rango;
    }
    void InicializarListado()
    {
        List<RangoPerfil> lstT = new List<RangoPerfil>();
        for (int i = gvLista.Rows.Count; i < 5; i++)
        {
            RangoPerfil lstDetalle = new RangoPerfil();
            lstDetalle.cod_rango_perfil = -1;
            lstDetalle.calificacion = 0;
            lstDetalle.rango_minimo = 0;
            lstDetalle.rango_maximo = 0;
            lstDetalle.cod_monitoreo = 0;
            lstT.Add(lstDetalle);
        }
        gvLista.DataSource = lstT;
        gvLista.DataBind();
        //Session["Detalle"] = lstT;
    }


    #endregion


    #region Eventos Botonera


    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            RangoPerfil vRangoPerfil = new RangoPerfil();
            vRangoPerfil.lstDetalle = ObtenerListaGridView(true);

            // Si llega nulo es porque hubo algun error
            if (vRangoPerfil.lstDetalle != null)
            {
                if (vRangoPerfil.lstDetalle.Count > 0)
                {
                    foreach (RangoPerfil eachRango in vRangoPerfil.lstDetalle)
                    {
                        RangoPerfil result =  _rangoPerfil.ModificarRangoPerfil(eachRango, Usuario);
                    }
                    //Navegar(Pagina.Lista);
                    ObtenerDatos("true");
                }
                else
                {
                    VerError("Algo salió mal, intenta nuevamente más tarde!.");
                }
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_rangoPerfil.CodigoPrograma, "btnGuardar_Click", ex);
        }
    }

    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        try
        {
            RangoPerfil vRangoPerfil = new RangoPerfil();
            vRangoPerfil.lstDetalle = ObtenerListaGridView(true);

            // Si llega nulo es porque hubo algun error
            if (vRangoPerfil.lstDetalle != null)
            {
                if (vRangoPerfil.lstDetalle.Count > 0)
                {
                    _rangoPerfil.ModificarRangoPerfil(vRangoPerfil, Usuario);
                    ObtenerDatos("true");
                }
                else
                {
                    VerError("No puedes guardar un segmento sin criterios validos!.");
                }
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_rangoPerfil.CodigoPrograma, "btnGuardar_Click", ex);
        }
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }

    protected void btnDetalle_Click(object sender, EventArgs e)
    {
        
    }


    #endregion


    #region Eventos GridView


    protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
       

    }

    protected void gvLista_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownList ddlMonitoreo = (DropDownList)e.Row.FindControl("ddlMonitoreo");

            if (ddlMonitoreo != null)
            {
                ddlMonitoreo.Items.Insert(0, new ListItem("<Seleccione un Item>", " "));
                ddlMonitoreo.Items.Insert(1, new ListItem("Diaria", "1"));
                ddlMonitoreo.Items.Insert(2, new ListItem("Quincenal", "2"));
                ddlMonitoreo.Items.Insert(3, new ListItem("Mensual", "3"));
                ddlMonitoreo.Items.Insert(4, new ListItem("Bimestral", "4"));
                ddlMonitoreo.Items.Insert(5, new ListItem("Trimestral", "5"));
                ddlMonitoreo.Items.Insert(5, new ListItem("Semestral", "6"));
                ddlMonitoreo.Items.Insert(5, new ListItem("Anual", "7"));
                ddlMonitoreo.DataBind();
            }

            Label lblMonitoreo = (Label)e.Row.FindControl("lblMonitoreo");
            if (lblMonitoreo != null)
            {
                ddlMonitoreo.SelectedValue = lblMonitoreo.Text;
            }
            Label lblCalificacion = (Label)e.Row.FindControl("lblCalificacion");
            if (Convert.ToInt32(lblCalificacion.Text) > 0)
                switch (Convert.ToInt32(lblCalificacion.Text))
                {
                    case 1: lblCalificacion.Text = "Riesgo Normal"; break;
                    case 2: lblCalificacion.Text = "Riesgo Moderado"; break;
                    case 3: lblCalificacion.Text = "Riesgo Alto"; break;
                    case 4: lblCalificacion.Text = "Riesgo Muy Alto"; break;
                }
        }
    }

    protected void ddlOperador_SelectedIndexChanged(object sender, EventArgs e)
    {
        
    }

    protected void ddlCondicion_SelectedIndexChanged(object sender, EventArgs e)
    {
       
    }


    #endregion


    #region Metodos De Ayuda


    List<RangoPerfil> ObtenerListaGridView(bool validarGridView = false)
    {
        List<RangoPerfil> lista = new List<RangoPerfil>();

        foreach (GridViewRow rfila in gvLista.Rows)
        {
            RangoPerfil sRango = new RangoPerfil();

            Label lblcodigoRango = (Label)rfila.FindControl("lblcodigoRango");
            if (lblcodigoRango != null && !string.IsNullOrWhiteSpace(lblcodigoRango.Text))
                sRango.cod_rango_perfil = Convert.ToInt32(lblcodigoRango.Text);

            Label lblCalificacion = (Label)rfila.FindControl("lblCalificacion");
            if (lblCalificacion.Text != null)
                switch (Convert.ToString(lblCalificacion.Text))
                {
                    case "Riesgo Normal": sRango.calificacion = 1; break;
                    case "Riesgo Moderado": sRango.calificacion = 2; break;
                    case "Riesgo Alto": sRango.calificacion = 3; break;
                    case "Riesgo Muy Alto": sRango.calificacion = 4; break;
                }

            TextBox txtRangoMinimo = (TextBox)rfila.FindControl("txtRangoMinimo");
            if (txtRangoMinimo != null)
            {
                if (txtRangoMinimo.Visible)
                {
                    sRango.rango_minimo = Convert.ToInt32(txtRangoMinimo.Text);
                }
            }

            TextBox txtRangoMaximo = (TextBox)rfila.FindControl("txtRangoMaximo");
            if (txtRangoMinimo != null)
            {
                if (txtRangoMaximo.Visible)
                {
                    sRango.rango_maximo = Convert.ToInt32(txtRangoMaximo.Text);
                }
            }

            DropDownListGrid dllMonitoreo = (DropDownListGrid)rfila.FindControl("ddlMonitoreo");
            if (dllMonitoreo.SelectedValue != null)
                sRango.cod_monitoreo = Convert.ToInt32(dllMonitoreo.SelectedValue);

            lista.Add(sRango);
            //Session["Detalle"] = lista;
        }

        return lista;
    }

    void AlternarVisibilidadDropDownsDeUnaFila(GridViewRow row, bool permitirDropDown, TipoLista tipoListaParaLlenar = TipoLista.SinTipoLista)
    {
        TextBox textBoxValor = row.FindControl("txtValor") as TextBox;
        TextBox textBoxSegundoValor = row.FindControl("txtSegundoValor") as TextBox;

        DropDownListGrid dropDownValor = row.FindControl("ddlValor") as DropDownListGrid;
        DropDownListGrid dropDownSegundoValor = row.FindControl("ddlSegundoValor") as DropDownListGrid;
        DropDownListGrid dropDownOperador = row.FindControl("ddlOperador") as DropDownListGrid;

        textBoxValor.Visible = !permitirDropDown;
        textBoxSegundoValor.Visible = !permitirDropDown;
        dropDownValor.Visible = permitirDropDown;
        dropDownSegundoValor.Visible = permitirDropDown && dropDownOperador.SelectedValue == "9"; /// Si no es operador rango no lo muestro

        // Switch se encargara de alternar campos entre textbox y dropdownlist
        if (permitirDropDown && tipoListaParaLlenar != TipoLista.SinTipoLista)
        {
            switch (tipoListaParaLlenar)
            {
                case TipoLista.NivelEscolaridad:
                    LlenarListasDesplegables(TipoLista.NivelEscolaridad, dropDownValor, dropDownSegundoValor);
                    break;
                case TipoLista.Ciudades:
                    LlenarListasDesplegables(TipoLista.Ciudades, dropDownValor, dropDownSegundoValor);
                    break;
                case TipoLista.Actividad_Laboral:
                    LlenarListasDesplegables(TipoLista.Actividad_Laboral, dropDownValor, dropDownSegundoValor);
                    break;
                case TipoLista.Sexo:
                    LlenarListasDesplegables(TipoLista.Sexo, dropDownValor, dropDownSegundoValor);
                    break;
                case TipoLista.TipoPersona:
                    LlenarListasDesplegables(TipoLista.TipoPersona, dropDownValor, dropDownSegundoValor);
                    break;
            }
        }
    }

    #endregion


}
