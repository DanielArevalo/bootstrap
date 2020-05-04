using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Xpinn.NIIF.Entities;
using Xpinn.NIIF.Services;
using Xpinn.Util;

partial class Nuevo : GlobalWeb
{
    TransicionSegmentoNIFService _tranServicios = new TransicionSegmentoNIFService();


    #region Eventos Iniciales


    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            VisualizarOpciones(_tranServicios.CodigoProgramaoriginal, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoCancelar += btnCancelar_Click;
            toolBar.eventoGuardar += btnGuardar_Click;
            ctlMensaje.eventoClick += btnContinuarMen_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_tranServicios.CodigoProgramaoriginal, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                txtCodigo.Text = "";
                Session["Detalle"] = null;
                if (Session[_tranServicios.CodigoProgramaoriginal + ".id"] != null)
                {
                    Session["Operacion"] = "2";
                    idObjeto = Session[_tranServicios.CodigoProgramaoriginal + ".id"].ToString();
                    Session.Remove(_tranServicios.CodigoProgramaoriginal + ".id");

                    ObtenerDatos(idObjeto);
                }
                else
                {
                    Session["Operacion"] = "1";
                    InicializarListado();
                }
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_tranServicios.CodigoProgramaoriginal, "Page_Load", ex);
        }
    }

    void ObtenerDatos(String idObjeto)
    {
        TransicionSegmentoNIF vDatos = new TransicionSegmentoNIF();
        vDatos.codsegmento = Convert.ToInt32(idObjeto);

        vDatos = _tranServicios.ConsultarTransicionSegmentoNIF(vDatos, (Usuario)Session["usuario"]);

        if (vDatos.codsegmento != 0)
            txtCodigo.Text = vDatos.codsegmento.ToString();
        if (vDatos.nombre != "")
            txtDescripcion.Text = vDatos.nombre;

        List<TransicionDetalle> lstDetalle = new List<TransicionDetalle>();
        lstDetalle = _tranServicios.ListarDetalleTransicion(Convert.ToInt32(idObjeto), (Usuario)Session["usuario"]);

        if (lstDetalle.Count > 0)
        {
            gvLista.DataSource = lstDetalle;
            gvLista.DataBind();
            Session["Detalle"] = lstDetalle;
        }
        else
        {
            InicializarListado();
        }
    }

    void InicializarListado()
    {
        List<TransicionDetalle> lstTasa = new List<TransicionDetalle>();
        for (int i = gvLista.Rows.Count; i < 5; i++)
        {
            TransicionDetalle pDetalle = new TransicionDetalle();
            pDetalle.idcondiciontran = -1;
            pDetalle.variable = null;
            pDetalle.operador = "";
            pDetalle.valor = "";
            lstTasa.Add(pDetalle);
        }
        gvLista.DataSource = lstTasa;
        gvLista.DataBind();
        Session["Detalle"] = lstTasa;
    }


    #endregion


    #region Eventos Botonera


    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        string mensaje = string.Empty;
        if (ValidarDatos())
        {
            mensaje = Session["Operacion"] != null && Session["Operacion"].ToString().Trim() == "2" ? " actualización?" : " grabación?";

            ctlMensaje.MostrarMensaje("Desea realizar la" + mensaje);
        }
    }

    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        try
        {

            TransicionSegmentoNIF vSegment = new TransicionSegmentoNIF();
            if (idObjeto != "")
                vSegment.codsegmento = Convert.ToInt32(txtCodigo.Text);
            else
                vSegment.codsegmento = 0;
            vSegment.nombre = txtDescripcion.Text.ToUpper();

            vSegment.lstDetalle = ObtenerListaGridView(true);

            // Si llega nulo es porque hubo algun error
            if (vSegment.lstDetalle != null)
            {
                if (vSegment.lstDetalle.Count > 0)
                {
                    if (txtCodigo.Text == "")
                    {
                        _tranServicios.CrearTransicionSegmento(vSegment, (Usuario)Session["usuario"]);
                    }
                    else
                    {
                        _tranServicios.ModificarTransicionSegmento(vSegment, (Usuario)Session["usuario"]);
                    }

                    Navegar(Pagina.Lista);
                }
                else
                {
                    VerError("No puedes guardar un segmento sin criterios validos!.");
                }
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_tranServicios.CodigoProgramaoriginal, "btnGuardar_Click", ex);
        }
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }

    protected void btnDetalle_Click(object sender, EventArgs e)
    {
        ObtenerListaGridView();
        List<TransicionDetalle> lstDetalle = Session["Detalle"] as List<TransicionDetalle>;

        if (lstDetalle != null)
        {
            for (int i = 1; i <= 1; i++)
            {
                TransicionDetalle pDetalle = new TransicionDetalle();
                pDetalle.idcondiciontran = -1;
                pDetalle.variable = null;
                pDetalle.operador = "";
                pDetalle.valor = "";
                lstDetalle.Add(pDetalle);
            }
            gvLista.PageIndex = gvLista.PageCount;
            gvLista.DataSource = lstDetalle;
            gvLista.DataBind();

            Session["Detalle"] = lstDetalle;
        }
    }


    #endregion


    #region Eventos GridView


    protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int conseID = Convert.ToInt32(gvLista.DataKeys[e.RowIndex].Values[0].ToString());

        ObtenerListaGridView();

        List<TransicionDetalle> LstDetalle;
        LstDetalle = (List<TransicionDetalle>)Session["Detalle"];

        if (conseID > 0)
        {
            try
            {
                foreach (TransicionDetalle acti in LstDetalle)
                {
                    if (acti.idcondiciontran == conseID)
                    {
                        _tranServicios.EliminarDetalleTransicionNIF(conseID, (Usuario)Session["usuario"]);
                        LstDetalle.Remove(acti);
                        break;
                    }
                }
            }
            catch (Xpinn.Util.ExceptionBusiness ex)
            {
                VerError(ex.Message);
            }
        }
        else
        {
            if (LstDetalle.Count >= e.RowIndex)
            {
                LstDetalle.RemoveAt(e.RowIndex);
            }
        }
        gvLista.DataSourceID = null;
        gvLista.DataBind();

        gvLista.DataSource = LstDetalle;
        gvLista.DataBind();

        Session["Detalle"] = LstDetalle;

    }

    protected void gvLista_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownList ddlCondicion = (DropDownList)e.Row.FindControl("ddlCondicion");
            DropDownList ddlOperador = (DropDownList)e.Row.FindControl("ddlOperador");

            if (ddlCondicion != null)
            {
                TasaMercadoNIFService Tasa = new TasaMercadoNIFService();
                List<TasaMercadoNIF> lstTasa= Tasa.DatosCondicionNIIF(new TasaMercadoNIF(), Usuario);
                ddlCondicion.DataSource = lstTasa;
                ddlCondicion.DataTextField = "nombre";
                ddlCondicion.DataValueField = "variable";
                ddlCondicion.Items.Insert(0, new ListItem("<Seleccione un Item>", " "));
                ddlCondicion.SelectedIndex = 0;
                ddlCondicion.DataBind();
            }

            if (ddlOperador != null)
            {
                ddlOperador.Items.Insert(0, new ListItem("<Seleccione un Item>", " "));
                ddlOperador.Items.Insert(1, new ListItem("IGUAL", "1"));
                ddlOperador.Items.Insert(2, new ListItem("MAYOR", "2"));
                ddlOperador.Items.Insert(3, new ListItem("MENOR", "3"));
                ddlOperador.Items.Insert(4, new ListItem("MAYOR o IGUAL", "4"));
                ddlOperador.Items.Insert(5, new ListItem("MENOR o IGUAL", "5"));
                ddlOperador.Items.Insert(6, new ListItem("DIFERENTE", "6"));
                ddlOperador.Items.Insert(7, new ListItem("COMIENZA POR", "7"));
                ddlOperador.Items.Insert(8, new ListItem("CONTIENE", "8"));
                ddlOperador.Items.Insert(9, new ListItem("RANGO", "9"));
                ddlOperador.DataBind();
            }

            Label lblCondicion = (Label)e.Row.FindControl("lblCondicion");
            if (lblCondicion != null)
            {
                ddlCondicion.SelectedValue = lblCondicion.Text;
            }

            Label lblOperador = (Label)e.Row.FindControl("lblOperador");
            if (lblOperador != null)
            {
                ddlOperador.SelectedValue = lblOperador.Text;

                // Configuro la visibilidad de los controles en la fila
                switch (ddlCondicion.SelectedValue)
                {
                    case "H": // Nivel Academico
                        AlternarVisibilidadDropDownsDeUnaFila(e.Row, true, TipoLista.NivelEscolaridad);
                        break;
                    case "I": // Ciudad Residencia
                        AlternarVisibilidadDropDownsDeUnaFila(e.Row, true, TipoLista.Ciudades);
                        break;
                    case "J": // Actividad Economica
                        AlternarVisibilidadDropDownsDeUnaFila(e.Row, true, TipoLista.Actividad_Laboral);
                        break;
                    case "S": // Sexo
                        AlternarVisibilidadDropDownsDeUnaFila(e.Row, true, TipoLista.Sexo);
                        break;
                    default: // Si es cualquier otro no permito dropdowns
                        AlternarVisibilidadDropDownsDeUnaFila(e.Row, false);
                        break;
                }

                // Configurar columna de segundo valor
                TextBox txtSegundoValor = (TextBox)e.Row.FindControl("txtSegundoValor");
                if (txtSegundoValor != null)
                    txtSegundoValor.Enabled = ddlOperador.SelectedValue == "9";

                DropDownListGrid dropDownSegundoValor = e.Row.FindControl("ddlSegundoValor") as DropDownListGrid;
                if (dropDownSegundoValor != null)
                {
                    if (!string.IsNullOrWhiteSpace(txtSegundoValor.Text) && dropDownSegundoValor.Visible)
                    {
                        dropDownSegundoValor.SelectedValue = txtSegundoValor.Text;
                    }
                }
            }

            // Configurar columna de valor
            TextBox textBoxValor = e.Row.FindControl("txtValor") as TextBox;
            DropDownListGrid dropDownValor = e.Row.FindControl("ddlValor") as DropDownListGrid;
            if (textBoxValor != null && dropDownValor != null && dropDownValor.Visible && !string.IsNullOrWhiteSpace(textBoxValor.Text))
            {
                dropDownValor.SelectedValue = textBoxValor.Text;
            }
        }
    }

    protected void ddlOperador_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownListGrid dropDownCambiado = sender as DropDownListGrid;

        if (dropDownCambiado != null)
        {
            int indiceDropDown = Convert.ToInt32(dropDownCambiado.CommandArgument);
            TextBox textBoxSegundoValor = gvLista.Rows[indiceDropDown].FindControl("txtSegundoValor") as TextBox;

            // Significa que fue seleccionado el operador rango
            textBoxSegundoValor.Enabled = dropDownCambiado.SelectedValue == "9";

            if (!textBoxSegundoValor.Enabled)
            {
                textBoxSegundoValor.Text = string.Empty;
            }

            DropDownListGrid dropDownSegundoValor = gvLista.Rows[indiceDropDown].FindControl("ddlSegundoValor") as DropDownListGrid;
            if (dropDownSegundoValor != null)
                dropDownSegundoValor.Visible = dropDownCambiado.SelectedValue == "9" && !textBoxSegundoValor.Visible;
        }
    }

    protected void ddlCondicion_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownListGrid dropDownCambiado = sender as DropDownListGrid;

        if (dropDownCambiado != null)
        {
            int indiceDropDown = Convert.ToInt32(dropDownCambiado.CommandArgument);
            GridViewRow row = gvLista.Rows[indiceDropDown];

            switch (dropDownCambiado.SelectedValue)
            {
                case "H": // Nivel Academico
                    AlternarVisibilidadDropDownsDeUnaFila(row, true, TipoLista.NivelEscolaridad);
                    break;
                case "I": // Ciudad Residencia
                    AlternarVisibilidadDropDownsDeUnaFila(row, true, TipoLista.Ciudades);
                    break;
                case "J": // Actividad Economica
                    AlternarVisibilidadDropDownsDeUnaFila(row, true, TipoLista.Actividad_Laboral);
                    break;
                case "S": // Sexo
                    AlternarVisibilidadDropDownsDeUnaFila(row, true, TipoLista.Sexo);
                    break;
                default: // Si es cualquier otro no permito dropdowns
                    AlternarVisibilidadDropDownsDeUnaFila(row, false);
                    break;
            }
        }
    }


    #endregion


    #region Metodos De Ayuda


    List<TransicionDetalle> ObtenerListaGridView(bool validarGridView = false)
    {
        List<TransicionDetalle> lstTasaCondicion = new List<TransicionDetalle>();
        List<TransicionDetalle> lista = new List<TransicionDetalle>();

        foreach (GridViewRow rfila in gvLista.Rows)
        {
            TransicionDetalle eTasa = new TransicionDetalle();

            Label lblcodigo = (Label)rfila.FindControl("lblcodigo");
            if (lblcodigo != null && !string.IsNullOrWhiteSpace(lblcodigo.Text))
                eTasa.idcondiciontran = Convert.ToInt32(lblcodigo.Text);

            DropDownListGrid ddlCondicion = (DropDownListGrid)rfila.FindControl("ddlCondicion");
            if (ddlCondicion.SelectedValue != null)
                eTasa.variable = ddlCondicion.SelectedValue;

            DropDownListGrid ddlOperador = (DropDownListGrid)rfila.FindControl("ddlOperador");
            if (ddlOperador.SelectedValue != null)
                eTasa.operador = ddlOperador.SelectedValue;

            // Si el txtValor esta oculto significa que el valor lo debo sacar del dropdown
            TextBox txtValor = (TextBox)rfila.FindControl("txtValor");
            if (txtValor != null)
            {
                if (txtValor.Visible)
                {
                    eTasa.valor = txtValor.Text;
                }
                else
                {
                    DropDownListGrid dropDownValor = rfila.FindControl("ddlValor") as DropDownListGrid;
                    eTasa.valor = dropDownValor.SelectedValue;
                }
            }

            // Si el operador es rango busco el segundo valor
            if (eTasa.operador == "9")
            {
                // Si el txtSegundoValor esta oculto significa que debo sacar el valor del dropdown
                TextBox txtSegundoValor = (TextBox)rfila.FindControl("txtSegundoValor");
                if (txtSegundoValor != null)
                {
                    if (txtSegundoValor.Visible)
                    {
                        eTasa.segundo_valor = txtSegundoValor.Text;
                    }
                    else
                    {
                        DropDownListGrid dropDownSegundoValor = rfila.FindControl("ddlSegundoValor") as DropDownListGrid;
                        eTasa.segundo_valor = dropDownSegundoValor.SelectedValue;
                    }
                }
            }

            lista.Add(eTasa);
            Session["Detalle"] = lista;

            if (validarGridView && eTasa.operador == "9" && string.IsNullOrWhiteSpace(eTasa.segundo_valor) && string.IsNullOrWhiteSpace(eTasa.valor))
            {
                VerError("Si estas usando el operador de rango, debes otorgar ambos valores");
                return null;
            }
            else if (validarGridView && !string.IsNullOrWhiteSpace(eTasa.operador) && string.IsNullOrWhiteSpace(eTasa.valor))
            {
                VerError("Debes escribir un valor valido para el detalle del segmento");
                return null;
            }

            if (!string.IsNullOrWhiteSpace(eTasa.variable) && !string.IsNullOrWhiteSpace(eTasa.operador) && !string.IsNullOrWhiteSpace(eTasa.valor))
            {
                lstTasaCondicion.Add(eTasa);
            }
        }

        return lstTasaCondicion;
    }

    Boolean ValidarDatos()
    {
        if (string.IsNullOrWhiteSpace(txtDescripcion.Text))
        {
            VerError("Ingrese la Descripción");
            return false;
        }

        return true;
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
            }
        }
    }

    #endregion


}
