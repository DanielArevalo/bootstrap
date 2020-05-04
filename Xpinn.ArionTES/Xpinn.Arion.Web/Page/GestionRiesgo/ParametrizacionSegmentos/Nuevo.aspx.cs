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
    HistoricoSegmentacionService _historicoService = new HistoricoSegmentacionService();


    #region Eventos Iniciales


    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            VisualizarOpciones(_historicoService.CodigoPrograma3, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoCancelar += btnCancelar_Click;
            toolBar.eventoGuardar += btnGuardar_Click;
            ctlMensaje.eventoClick += btnContinuarMen_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_historicoService.CodigoPrograma3, "Page_PreInit", ex);
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
                if (Session[_historicoService.CodigoPrograma3 + ".id"] != null)
                {
                    Session["Operacion"] = "2";
                    idObjeto = Session[_historicoService.CodigoPrograma3 + ".id"].ToString();
                    Session.Remove(_historicoService.CodigoPrograma3 + ".id");

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
            BOexcepcion.Throw(_historicoService.CodigoPrograma3, "Page_Load", ex);
        }
    }

    void ObtenerDatos(String idObjeto)
    {
        Segmentos vDatos = new Segmentos();
        vDatos.codsegmento = Convert.ToInt32(idObjeto);

        vDatos = _historicoService.ConsultarSegmento(vDatos, Usuario);

        if (vDatos.codsegmento != 0)
            txtCodigo.Text = vDatos.codsegmento.ToString();
        if (vDatos.nombre != "")
            txtDescripcion.Text = vDatos.nombre;
        if (vDatos.tipo_variable != "")
            ddlTipoVariable.Text = Convert.ToString(vDatos.tipo_variable);
        if (vDatos.calificacion_segmento != 0)
            txtCalificacionSegmento.Text = Convert.ToString(vDatos.calificacion_segmento);
        chkAlguno.Checked = vDatos.valida_alguno;
        List<Segmento_Detalles> lstDetalle = new List<Segmento_Detalles>();
        lstDetalle = _historicoService.ListarDetalleSegmentos(Convert.ToInt32(idObjeto), Usuario);

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
        listaSegunVariable(false);
    }
    void InicializarListado()
    {
        List<Segmento_Detalles> lstTasa = new List<Segmento_Detalles>();
        for (int i = gvLista.Rows.Count; i < 5; i++)
        {
            Segmento_Detalles pDetalle = new Segmento_Detalles();
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

            Segmentos vSegment = new Segmentos();
            if (idObjeto != "")
                vSegment.codsegmento = Convert.ToInt32(idObjeto);
            else
                vSegment.codsegmento = 0;
            vSegment.nombre = txtDescripcion.Text.ToUpper();
            vSegment.tipo_variable = Convert.ToString(ddlTipoVariable.SelectedValue);
            vSegment.calificacion_segmento = txtCalificacionSegmento.Text == "" ? 0 : Convert.ToInt32(txtCalificacionSegmento.Text);
            vSegment.valida_alguno = Convert.ToBoolean(chkAlguno.Checked);

            vSegment.lstDetalle = ObtenerListaGridView(true);

            // Si llega nulo es porque hubo algun error
            if (vSegment.lstDetalle != null)
            {
                if (vSegment.lstDetalle.Count > 0)
                {
                    if (txtCodigo.Text == "")
                    {
                        _historicoService.CrearSegmento(vSegment, (Usuario)Session["usuario"]);
                    }
                    else
                    {
                        _historicoService.ModificarSegmento(vSegment, (Usuario)Session["usuario"]);
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
            BOexcepcion.Throw(_historicoService.CodigoPrograma3, "btnGuardar_Click", ex);
        }
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }

    protected void btnDetalle_Click(object sender, EventArgs e)
    {
        ObtenerListaGridView();
        List<Segmento_Detalles> lstDetalle = Session["Detalle"] as List<Segmento_Detalles>;

        if (lstDetalle != null)
        {
            for (int i = 1; i <= 1; i++)
            {
                Segmento_Detalles pDetalle = new Segmento_Detalles();
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

        List<Segmento_Detalles> LstDetalle = ObtenerListaGridView();

        if (conseID > 0)
        {
            try
            {
                foreach (Segmento_Detalles acti in LstDetalle)
                {
                    if (acti.idcondiciontran == conseID)
                    {
                        _historicoService.EliminarSegmentoDetalle(conseID, (Usuario)Session["usuario"]);
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
            /*if (LstDetalle.Count >= e.RowIndex)
            {
                LstDetalle.RemoveAt(e.RowIndex);
            }*/
        }
        gvLista.DataSourceID = null;
        gvLista.DataBind();

        gvLista.DataSource = LstDetalle;
        gvLista.DataBind();

        Session["Detalle"] = LstDetalle;

    }
    void cargarconjutas(GridView lista, String val, TextBox txtRecoger)
    {
        string[] arrAct = val.Split(';');
        foreach (string act in arrAct)
        {
            if (act != "")
            {
                foreach (GridViewRow rfila in lista.Rows)
                {
                    Label lbl_destino = (Label)rfila.FindControl("lbl_destino");
                    if (Convert.ToString(lbl_destino.Text) == act)
                    {
                        CheckBox cbListado = (CheckBox)rfila.FindControl("cbListado");
                        Label lbl_descripcion = (Label)rfila.FindControl("lbl_descripcion");
                        cbListado.Checked = true;
                        txtRecoger.Text = lbl_descripcion.Text;
                    }
                }
            }
        }
    }
    void modificarListaOperador(DropDownListGrid ddlOperador, bool modificarOperador, bool modificarOperadorActividadLaboral)
    {
        if (modificarOperador)
        {
            ddlOperador.Items.Clear();
            ddlOperador.DataBind();
            ddlOperador.Items.Insert(0, new ListItem("<Seleccione un Item>", " "));
            ddlOperador.Items.Insert(1, new ListItem("IGUAL", "1"));
            ddlOperador.Items.Insert(2, new ListItem("DIFERENTE", "6"));
            ddlOperador.Items.Insert(3, new ListItem("RANGO", "9"));
            if (modificarOperadorActividadLaboral) { ddlOperador.Items.Insert(4, new ListItem("CONJUNTO", "10")); }
            ddlOperador.DataBind();
        }
    }
    protected void gvLista_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownList ddlCondicion = (DropDownList)e.Row.FindControl("ddlCondicion");
            DropDownListGrid ddlOperador = (DropDownListGrid)e.Row.FindControl("ddlOperador");

            if (ddlCondicion != null)
            {
                List<Segmento_Detalles> lstVariables = new List<Segmento_Detalles>();
                switch (Convert.ToInt32(ddlTipoVariable.SelectedValue))
                {
                    case 1:
                        lstVariables.Add(new Segmento_Detalles() { variable = "C", descripcion = "Ingresos Mensuales" });
                        lstVariables.Add(new Segmento_Detalles() { variable = "A", descripcion = "Puntaje Calificación" });
                        lstVariables.Add(new Segmento_Detalles() { variable = "D", descripcion = "Edad" });
                        lstVariables.Add(new Segmento_Detalles() { variable = "E", descripcion = "Personas a Cargo" });
                        lstVariables.Add(new Segmento_Detalles() { variable = "F", descripcion = "Tipo de vivienda" });
                        lstVariables.Add(new Segmento_Detalles() { variable = "G", descripcion = "Estrato" });
                        lstVariables.Add(new Segmento_Detalles() { variable = "H", descripcion = "Nivel Academico" });
                        lstVariables.Add(new Segmento_Detalles() { variable = "I", descripcion = "Ciudad Residencia" });
                        lstVariables.Add(new Segmento_Detalles() { variable = "J", descripcion = "Actividad Economica" });
                        lstVariables.Add(new Segmento_Detalles() { variable = "S", descripcion = "Sexo" });
                        lstVariables.Add(new Segmento_Detalles() { variable = "T", descripcion = "Tipo de persona" });
                        lstVariables.Add(new Segmento_Detalles() { variable = "U", descripcion = "Tipo de Cliente" });
                        lstVariables.Add(new Segmento_Detalles() { variable = "AK", descripcion = "Antigüedad asociado sea: en fecha o en numero de meses" });
                        lstVariables.Add(new Segmento_Detalles() { variable = "AL", descripcion = "Total Activos" });
                        lstVariables.Add(new Segmento_Detalles() { variable = "AO", descripcion = "Total Pasivos" });
                        lstVariables.Add(new Segmento_Detalles() { variable = "AP", descripcion = "Total Patrimonio" });
                        break;
                    case 2:
                        lstVariables.Add(new Segmento_Detalles() { variable = "K", descripcion = "Saldo Promedio Ahorros" });
                        lstVariables.Add(new Segmento_Detalles() { variable = "L", descripcion = "Saldo Promedio Aportes" });
                        lstVariables.Add(new Segmento_Detalles() { variable = "M", descripcion = "Monto del Crédito SMLMV" });
                        lstVariables.Add(new Segmento_Detalles() { variable = "N", descripcion = "Saldo Creditos Activos" });
                        lstVariables.Add(new Segmento_Detalles() { variable = "O", descripcion = "Numero Creditos Activos" });
                        lstVariables.Add(new Segmento_Detalles() { variable = "P", descripcion = "# Operaciones productos al mes" });
                        lstVariables.Add(new Segmento_Detalles() { variable = "R", descripcion = "Monto operaciones en el mes" });
                        lstVariables.Add(new Segmento_Detalles() { variable = "V", descripcion = "Saldo Aportes Activos" });
                        lstVariables.Add(new Segmento_Detalles() { variable = "Y", descripcion = "Saldo total de captaciones" });
                        lstVariables.Add(new Segmento_Detalles() { variable = "Z", descripcion = "Monto de operaciones en el mes ahorro vista" });
                        lstVariables.Add(new Segmento_Detalles() { variable = "AB", descripcion = "Monto de operaciones en el mes aportes" });
                        lstVariables.Add(new Segmento_Detalles() { variable = "AC", descripcion = "Monto de operaciones en el mes créditos" });
                        lstVariables.Add(new Segmento_Detalles() { variable = "AD", descripcion = "Monto de operaciones en el mes ahorro programado" });
                        lstVariables.Add(new Segmento_Detalles() { variable = "AE", descripcion = "Monto de operaciones en el mes cdta" });
                        lstVariables.Add(new Segmento_Detalles() { variable = "AF", descripcion = "# de operaciones en el mes ahorro vista" });
                        lstVariables.Add(new Segmento_Detalles() { variable = "AG", descripcion = "# de operaciones en el mes aportes" });
                        lstVariables.Add(new Segmento_Detalles() { variable = "AH", descripcion = "# de operaciones en el mes créditos" });
                        lstVariables.Add(new Segmento_Detalles() { variable = "AI", descripcion = "# de operaciones en el mes ahorro permanente" });
                        lstVariables.Add(new Segmento_Detalles() { variable = "AJ", descripcion = "# de operaciones en el mes cdta" });
                        lstVariables.Add(new Segmento_Detalles() { variable = "B", descripcion = "Endeudamiento" });
                        lstVariables.Add(new Segmento_Detalles() { variable = "AQ", descripcion = "Incluir prepago de obligaciones antes del X % del cumplimiento del saldo de capital" });
                        lstVariables.Add(new Segmento_Detalles() { variable = "AR", descripcion = "Número de prepagos en el año" });
                        break;
                    case 3:
                        lstVariables.Add(new Segmento_Detalles() { variable = "AM", descripcion = "Caja" });
                        lstVariables.Add(new Segmento_Detalles() { variable = "AN", descripcion = "Bancos" });
                        lstVariables.Add(new Segmento_Detalles() { variable = "AS", descripcion = "Tarjeta Debito" });
                        lstVariables.Add(new Segmento_Detalles() { variable = "AU", descripcion = "Transacciones Internas" });
                        break;
                    case 4:
                        lstVariables.Add(new Segmento_Detalles() { variable = "W", descripcion = "Jurisdiccion" });
                        lstVariables.Add(new Segmento_Detalles() { variable = "X", descripcion = "Valoracion Jurisdiccion" });
                        break;

                }
		        if (Convert.ToInt32(ddlTipoVariable.SelectedValue) == 1 || Convert.ToInt32(ddlTipoVariable.SelectedValue) == 2)
                {
			        lstVariables.Add(new Segmento_Detalles() { variable = "P", descripcion = "# Operaciones productos al mes" });
                    lstVariables.Add(new Segmento_Detalles() { variable = "R", descripcion = "Monto operaciones en el mes" });
		        }
                ddlCondicion.DataSource = lstVariables;
                ddlCondicion.DataTextField = "descripcion";
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
                if (ddlCondicion.SelectedValue == "H" || ddlCondicion.SelectedValue == "I")
                {
                    modificarListaOperador(ddlOperador, true, false);
                }
                else if (ddlCondicion.SelectedValue == "J") { modificarListaOperador(ddlOperador, true, true); }
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
                        if (Convert.ToInt32(lblOperador.Text) == 10)
                        {
                            List<listaMultiple> lstConsultaAct = new List<listaMultiple>();
                            lstConsultaAct = _historicoService.ListarActividadesMultiple((Usuario)Session["usuario"]);
                            GridView listaAct = (GridView)e.Row.FindControl("gvRecoger");
                            TextBox txtRecoger = (TextBox)e.Row.FindControl("txtRecoger");
                            DropDownListGrid dropDownValorA = e.Row.FindControl("ddlValor") as DropDownListGrid;

                            listaAct.DataSource = lstConsultaAct;
                            if (lstConsultaAct.Count > 0)
                            {
                                txtRecoger.Visible = true;
                                dropDownValorA.Visible = false;
                                listaAct.DataBind();
                                TextBox txtValor = (TextBox)e.Row.FindControl("txtValor");
                                if (txtValor != null)
                                    cargarconjutas(listaAct, Convert.ToString(txtValor.Text), txtRecoger);
                            }
                            else
                            {
                                listaAct.Visible = false;
                            }
                        }
                        else { AlternarVisibilidadDropDownsDeUnaFila(e.Row, true, TipoLista.Actividad_Laboral); }
                        break;
                    case "S": // Sexo
                        AlternarVisibilidadDropDownsDeUnaFila(e.Row, true, TipoLista.Sexo);
                        break;
                    case "T": // Tipo persona
                        AlternarVisibilidadDropDownsDeUnaFila(e.Row, true, TipoLista.TipoPersona);
                        break;
                    case "U": // Tipo de Cliente
                        AlternarVisibilidadDropDownsDeUnaFila(e.Row, true, TipoLista.TipoCliente);
                        break;
                    case "W": // Jurisdiccion
                        AlternarVisibilidadDropDownsDeUnaFila(e.Row, true, TipoLista.Jurisdiccion);
                        break;
                    case "X": // Valoración Jurisdiccion
                        AlternarVisibilidadDropDownsDeUnaFila(e.Row, true, TipoLista.ValoracionJurisdiccion);
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
            // Determinar que fila esta el usuario trabajando
            int indiceDropDown = Convert.ToInt32(dropDownCambiado.CommandArgument);
            TextBox textBoxSegundoValor = gvLista.Rows[indiceDropDown].FindControl("txtSegundoValor") as TextBox;

            // Significa que fue seleccionado el operador rango
            textBoxSegundoValor.Enabled = dropDownCambiado.SelectedValue == "9";
            if (!textBoxSegundoValor.Enabled)
            {
                textBoxSegundoValor.Text = string.Empty;
            }

            // Activar la lista para el segundo valor cuando es un rango
            DropDownListGrid dropDownSegundoValor = gvLista.Rows[indiceDropDown].FindControl("ddlSegundoValor") as DropDownListGrid;
            if (dropDownSegundoValor != null)
                dropDownSegundoValor.Visible = dropDownCambiado.SelectedValue == "9" && !textBoxSegundoValor.Visible;

            // Cuando es conjunto llenar el control correspondiente. Esto es para la actividad y para la jurisdicción
	        TextBox txtRecoger = gvLista.Rows[indiceDropDown].FindControl("txtRecoger") as TextBox;
            if (dropDownCambiado.SelectedValue == "10")
            {
                List<listaMultiple> lstConsultaAct = new List<listaMultiple>();

                DropDownListGrid ddlCondicion = gvLista.Rows[indiceDropDown].FindControl("ddlCondicion") as DropDownListGrid;
                GridView listaAct = gvLista.Rows[indiceDropDown].FindControl("gvRecoger") as GridView;
                DropDownListGrid dropDownValor = gvLista.Rows[indiceDropDown].FindControl("ddlValor") as DropDownListGrid;

                bool bEncontro = false;
                if (ddlCondicion != null)
                {
                    if (ddlCondicion.SelectedValue == "W")
                    {
                        bEncontro = true;
                        Xpinn.Riesgo.Services.JurisdiccionDepaServices serviceRiesgo = new Xpinn.Riesgo.Services.JurisdiccionDepaServices();
                        List<Xpinn.Riesgo.Entities.JurisdiccionDepa> lista = new List<Xpinn.Riesgo.Entities.JurisdiccionDepa>();
                        lista = serviceRiesgo.ListasDesplegables(Usuario);
                        lstConsultaAct.Clear();
                        foreach (Xpinn.Riesgo.Entities.JurisdiccionDepa item in lista)
                        {
                            listaMultiple newItem = new listaMultiple();
                            newItem.cod_act = Convert.ToInt32(item.ListaIdStr);
                            newItem.nombre_act = item.ListaDescripcion;
                            lstConsultaAct.Add(newItem);
                        }
                    }
                }
                if (!bEncontro)
                    lstConsultaAct = _historicoService.ListarActividadesMultiple((Usuario)Session["usuario"]);

                listaAct.DataSource = lstConsultaAct;
                if (lstConsultaAct.Count > 0)
                {
                    txtRecoger.Visible = true;
                    dropDownValor.Visible = false;
                    listaAct.DataBind();
                }
                else
                {
                    listaAct.Visible = false;
                }
            }else
            {

                DropDownListGrid dropDownValor = gvLista.Rows[indiceDropDown].FindControl("ddlValor") as DropDownListGrid;
                txtRecoger.Visible = false;
                dropDownValor.Visible = true;
            }
        }
    }
    public void listaSegunVariable(bool init)
    {
        List<Segmento_Detalles> lstVariables = new List<Segmento_Detalles>();
	    if(Convert.ToInt32(ddlTipoVariable.SelectedValue) == 1 || Convert.ToInt32(ddlTipoVariable.SelectedValue) == 2){
			lstVariables.Add(new Segmento_Detalles() { variable = "P", descripcion = "# Operaciones productos al mes" });
            lstVariables.Add(new Segmento_Detalles() { variable = "R", descripcion = "Monto operaciones en el mes" });
		}
        switch (Convert.ToInt32(ddlTipoVariable.SelectedValue))
        {
            case 1:
                lstVariables.Add(new Segmento_Detalles() { variable = "C", descripcion = "Ingresos Mensuales" });
                lstVariables.Add(new Segmento_Detalles() { variable = "A", descripcion = "Puntaje Calificación" });
                lstVariables.Add(new Segmento_Detalles() { variable = "D", descripcion = "Edad" });
                lstVariables.Add(new Segmento_Detalles() { variable = "E", descripcion = "Personas a Cargo" });
                lstVariables.Add(new Segmento_Detalles() { variable = "F", descripcion = "Tipo de vivienda" });
                lstVariables.Add(new Segmento_Detalles() { variable = "G", descripcion = "Estrato" });
                lstVariables.Add(new Segmento_Detalles() { variable = "H", descripcion = "Nivel Academico" });
                lstVariables.Add(new Segmento_Detalles() { variable = "I", descripcion = "Ciudad Residencia" });
                lstVariables.Add(new Segmento_Detalles() { variable = "J", descripcion = "Actividad Economica" });
                lstVariables.Add(new Segmento_Detalles() { variable = "S", descripcion = "Sexo" });
                lstVariables.Add(new Segmento_Detalles() { variable = "T", descripcion = "Tipo de persona" });
                lstVariables.Add(new Segmento_Detalles() { variable = "U", descripcion = "Tipo de Cliente" });
                lstVariables.Add(new Segmento_Detalles() { variable = "AK", descripcion = "Antigüedad asociado sea: en fecha o en numero de meses" });
                lstVariables.Add(new Segmento_Detalles() { variable = "AL", descripcion = "Total Activos" });
                lstVariables.Add(new Segmento_Detalles() { variable = "AO", descripcion = "Total Pasivos" });
                lstVariables.Add(new Segmento_Detalles() { variable = "AP", descripcion = "Total Patrimonio" });
                break;
            case 2:
                lstVariables.Add(new Segmento_Detalles() { variable = "K", descripcion = "Saldo Promedio Ahorros" });
                lstVariables.Add(new Segmento_Detalles() { variable = "L", descripcion = "Saldo Promedio Aportes" });
                lstVariables.Add(new Segmento_Detalles() { variable = "M", descripcion = "Monto del Crédito SMLMV" });
                lstVariables.Add(new Segmento_Detalles() { variable = "N", descripcion = "Saldo Creditos Activos" });
                lstVariables.Add(new Segmento_Detalles() { variable = "O", descripcion = "Numero Creditos Activos" });
                lstVariables.Add(new Segmento_Detalles() { variable = "P", descripcion = "# Operaciones productos al mes" });
                lstVariables.Add(new Segmento_Detalles() { variable = "R", descripcion = "Monto operaciones en el mes" });
                lstVariables.Add(new Segmento_Detalles() { variable = "V", descripcion = "Saldo Aportes Activos" });
                lstVariables.Add(new Segmento_Detalles() { variable = "Y", descripcion = "Saldo total de captaciones" });
                lstVariables.Add(new Segmento_Detalles() { variable = "Z", descripcion = "Monto de operaciones en el mes ahorro vista" });
                lstVariables.Add(new Segmento_Detalles() { variable = "AB", descripcion = "Monto de operaciones en el mes aportes" });
                lstVariables.Add(new Segmento_Detalles() { variable = "AC", descripcion = "Monto de operaciones en el mes créditos" });
                lstVariables.Add(new Segmento_Detalles() { variable = "AD", descripcion = "Monto de operaciones en el mes ahorro programado" });
                lstVariables.Add(new Segmento_Detalles() { variable = "AE", descripcion = "Monto de operaciones en el mes cdta" });
                lstVariables.Add(new Segmento_Detalles() { variable = "AF", descripcion = "# de operaciones en el mes ahorro vista" });
                lstVariables.Add(new Segmento_Detalles() { variable = "AG", descripcion = "# de operaciones en el mes aportes" });
                lstVariables.Add(new Segmento_Detalles() { variable = "AH", descripcion = "# de operaciones en el mes créditos" });
                lstVariables.Add(new Segmento_Detalles() { variable = "AI", descripcion = "# de operaciones en el mes ahorro permanente" });
                lstVariables.Add(new Segmento_Detalles() { variable = "AJ", descripcion = "# de operaciones en el mes cdta" });
                lstVariables.Add(new Segmento_Detalles() { variable = "B", descripcion = "Endeudamiento" });
                lstVariables.Add(new Segmento_Detalles() { variable = "AQ", descripcion = "Incluir prepago de obligaciones antes del X % del cumplimiento del saldo de capital" });
                lstVariables.Add(new Segmento_Detalles() { variable = "AR", descripcion = "Número de prepagos en el año" });
                break;
            case 3:
                lstVariables.Add(new Segmento_Detalles() { variable = "AM", descripcion = "Caja" });
                lstVariables.Add(new Segmento_Detalles() { variable = "AN", descripcion = "Bancos" });
                lstVariables.Add(new Segmento_Detalles() { variable = "AS", descripcion = "Tarjeta Debito" });
                lstVariables.Add(new Segmento_Detalles() { variable = "AU", descripcion = "Transacciones Internas" });
                break;
            case 4:
                lstVariables.Add(new Segmento_Detalles() { variable = "W", descripcion = "Jurisdiccion" });
                lstVariables.Add(new Segmento_Detalles() { variable = "X", descripcion = "Valoracion Jurisdiccion" });
                break;

        }
        foreach (GridViewRow rfila in gvLista.Rows)
        {
            DropDownListGrid ddlCondicion = (DropDownListGrid)rfila.FindControl("ddlCondicion");
            //LIMPIAR LA LISTA
            ddlCondicion.Items.Clear();
            //LLENARLA NUEVAMENTE
            ddlCondicion.DataSource = lstVariables;
            ddlCondicion.DataTextField = "descripcion";
            ddlCondicion.DataValueField = "variable";
            ddlCondicion.Items.Insert(0, new ListItem("<Seleccione un Item>", " "));
            if (init)
                ddlCondicion.SelectedIndex = 0;
            try { ddlCondicion.DataBind(); } catch { VerError(""); }
        }
    }
    protected void ddlTipoVariable_SelectedIndexChanged(object sender, EventArgs e)
    {
        listaSegunVariable(false);
    }

    protected void ddlCondicion_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownListGrid dropDownCambiado = sender as DropDownListGrid;
        DropDownListGrid ddlOperador = null;
        if (dropDownCambiado != null)
        {
            int indiceDropDown = Convert.ToInt32(dropDownCambiado.CommandArgument);
            GridViewRow row = gvLista.Rows[indiceDropDown];
            ddlOperador = gvLista.Rows[indiceDropDown].FindControl("ddlOperador") as DropDownListGrid;

            switch (dropDownCambiado.SelectedValue)
            {
                case "H": // Nivel Academico
                    AlternarVisibilidadDropDownsDeUnaFila(row, true, TipoLista.NivelEscolaridad);
                    modificarListaOperador(ddlOperador, true, false);
                    break;
                case "I": // Ciudad Residencia
                    AlternarVisibilidadDropDownsDeUnaFila(row, true, TipoLista.Ciudades);
                    modificarListaOperador(ddlOperador, true, false);
                    break;
                case "J": // Actividad Economica
                    AlternarVisibilidadDropDownsDeUnaFila(row, true, TipoLista.Actividad_Laboral);
                    modificarListaOperador(ddlOperador, true, true);
                    break;
                case "S": // Sexo
                    AlternarVisibilidadDropDownsDeUnaFila(row, true, TipoLista.Sexo);
                    break;
                case "T": // Tipo persona
                    AlternarVisibilidadDropDownsDeUnaFila(row, true, TipoLista.TipoPersona);
                    break;
                case "U": // Tipo de cliente
                    AlternarVisibilidadDropDownsDeUnaFila(row, true, TipoLista.TipoCliente);
                    break;
                case "W": // Jurisdicciòn
                    AlternarVisibilidadDropDownsDeUnaFila(row, true, TipoLista.Jurisdiccion);
		    ddlOperador.Items.Insert(10, new ListItem("CONJUNTO", "10"));
                    break;
                case "X": // Valoración Jurisdicciòn
                    AlternarVisibilidadDropDownsDeUnaFila(row, true, TipoLista.ValoracionJurisdiccion);
                    break;
                default: // Si es cualquier otro no permito dropdowns
                    AlternarVisibilidadDropDownsDeUnaFila(row, false);
                    break;
            }
        }
    }


    #endregion


    #region Metodos De Ayuda

    string ObtenerListaGridViewConjunta(GridView listaAct)
    {
        String lstDatosConjuntos = "";
        foreach (GridViewRow rfila in listaAct.Rows)
        {
            CheckBox cbListado = (CheckBox)rfila.FindControl("cbListado");
            if (Convert.ToBoolean(cbListado.Checked))
            {
                Label lbl_destino = (Label)rfila.FindControl("lbl_destino");
                if (lbl_destino != null && !string.IsNullOrWhiteSpace(lbl_destino.Text))
                    lstDatosConjuntos += lbl_destino.Text + ";";
            }
        }

        return lstDatosConjuntos;
    }
    List<Segmento_Detalles> ObtenerListaGridView(bool validarGridView = false)
    {
        List<Segmento_Detalles> lstTasaCondicion = new List<Segmento_Detalles>();
        List<Segmento_Detalles> lista = new List<Segmento_Detalles>();

        foreach (GridViewRow rfila in gvLista.Rows)
        {

            Segmento_Detalles eTasa = new Segmento_Detalles();

            Label lblcodigo = (Label)rfila.FindControl("lblcodigo");
            if (lblcodigo != null && !string.IsNullOrWhiteSpace(lblcodigo.Text))
                eTasa.idcondiciontran = Convert.ToInt32(lblcodigo.Text);

            DropDownListGrid ddlCondicion = (DropDownListGrid)rfila.FindControl("ddlCondicion");
            if (ddlCondicion.SelectedValue != null)
                eTasa.variable = ddlCondicion.SelectedValue;

            DropDownListGrid ddlOperador = (DropDownListGrid)rfila.FindControl("ddlOperador");
            if (ddlOperador.SelectedValue != null)
                eTasa.operador = ddlOperador.SelectedValue;

            if (ddlOperador.SelectedValue != " ")
            {
                //recoger datos cuando la opcion sea conjunta
                if (Convert.ToInt32(ddlOperador.SelectedValue) == 10)
                {
                    GridView listaAct = (GridView)rfila.FindControl("gvRecoger");
                    eTasa.valor = ObtenerListaGridViewConjunta(listaAct);
                }
                else
                {
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
                case TipoLista.TipoPersona:
                    LlenarListasDesplegables(TipoLista.TipoPersona, dropDownValor, dropDownSegundoValor);
                    break;
                case TipoLista.TipoCliente:
                    LlenarListasDesplegables(TipoLista.TipoCliente, dropDownValor, dropDownSegundoValor);
                    break;
                case TipoLista.Jurisdiccion:
                    LlenarListasDesplegables(TipoLista.Jurisdiccion, dropDownValor, dropDownSegundoValor);
                    break;
                case TipoLista.ValoracionJurisdiccion:
                    LlenarListasDesplegables(TipoLista.ValoracionJurisdiccion, dropDownValor, dropDownSegundoValor);
                    break;
            }
        }
    }

    #endregion


}
