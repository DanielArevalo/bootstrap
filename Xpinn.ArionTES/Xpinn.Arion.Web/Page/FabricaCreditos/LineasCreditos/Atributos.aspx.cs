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

partial class Nuevo : GlobalWeb
{
    private Xpinn.FabricaCreditos.Services.LineasCreditoService LineasCreditoServicio = new Xpinn.FabricaCreditos.Services.LineasCreditoService();
    private List<Xpinn.FabricaCreditos.Entities.Persona1> lstDatosSolicitud = new List<Xpinn.FabricaCreditos.Entities.Persona1>();
    private String ListaSolicitada = null;  // Cadena en la que se indica la lista a solicitar
    private String operacion;


    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[LineasCreditoServicio.CodigoPrograma + ".CodRango"] != null)
                VisualizarOpciones(LineasCreditoServicio.CodigoPrograma, "E");
            else
                VisualizarOpciones(LineasCreditoServicio.CodigoPrograma, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(LineasCreditoServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Session[LineasCreditoServicio.CodigoPrograma + ".LineaCredito"] != null)
                    lblCodLineaCredito.Text = Session[LineasCreditoServicio.CodigoPrograma + ".LineaCredito"].ToString();
                Session["Atributos"] = null;
                Session["RangosTopes"] = null;
                if (Session[LineasCreditoServicio.CodigoPrograma + ".CodRango"] != null)
                {
                    idObjeto = Session[LineasCreditoServicio.CodigoPrograma + ".CodRango"].ToString();
                    ObtenerDatos(idObjeto);
                }
                else
                {
                    txtCodRango.Text = "";
                    InicializarAtributos();
                    InicializarTopes();
                }
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(LineasCreditoServicio.CodigoPrograma, "Page_Load", ex);
        }
    }



    protected void InicializarAtributos()
    {
        List<LineasCredito> lstAtributos = new List<LineasCredito>();
        for (int i = gvAtributos.Rows.Count; i < 4; i++)
        {
            LineasCredito eActi = new LineasCredito();
            eActi.cod_atr = 0;
            eActi.descripcion = "";
            eActi.tasa = null;
            eActi.tipotasa = null;
            eActi.desviacion = null;
            eActi.tipo_historico = 0;
            eActi.formacalculo = "";

            lstAtributos.Add(eActi);
        }
        gvAtributos.DataSource = lstAtributos;
        gvAtributos.DataBind();
        Session["Atributos"] = lstAtributos;
    }

    protected void InicializarTopes()
    {
        List<RangosTopes> lstTopes = new List<RangosTopes>();
        for (int i = gvTopes.Rows.Count; i < 4; i++)
        {
            RangosTopes eActi = new RangosTopes();
            eActi.idtope = 0;
            eActi.descripcion = "";
            eActi.minimo = "";
            eActi.maximo = "";

            lstTopes.Add(eActi);
        }
        gvTopes.DataSource = lstTopes;
        gvTopes.DataBind();
        Session["RangosTopes"] = lstTopes;
    }



    protected void ObtenerDatos(string idObjeto)
    {

        LineasCreditoService Atributosservicio = new LineasCreditoService();
        try
        {
            Xpinn.FabricaCreditos.Entities.LineasCredito vLineasCredito = new Xpinn.FabricaCreditos.Entities.LineasCredito();

            if (Session[LineasCreditoServicio.CodigoPrograma + ".NombreGrupo"] != null)
                txtNombreGrupo.Text = Session[LineasCreditoServicio.CodigoPrograma + ".NombreGrupo"].ToString();
            if (idObjeto != null)
                txtCodRango.Text = idObjeto.ToString();
            //Obtener Grilla de Topes         
            List<RangosTopes> LstTopes = new List<RangosTopes>();
            if (idObjeto != "")
            {
                LstTopes = LineasCreditoServicio.ConsultarLineasCreditopes(lblCodLineaCredito.Text, Convert.ToString(idObjeto.ToString()), (Usuario)Session["Usuario"]);
            }
            if (LstTopes.Count > 0)
            {
                if ((LstTopes != null) || (LstTopes.Count != 0))
                {
                    gvTopes.DataSource = LstTopes;
                    gvTopes.DataBind();
                }
                Session["RangosTopes"] = LstTopes;
            }
            else
            {
                InicializarTopes();
            }

            //Obtener Grilla de Atributos
            List<LineasCredito> LstAtributos = new List<LineasCredito>();
            if (idObjeto != "")
            {
                LstAtributos = LineasCreditoServicio.ConsultarLineasCrediatributo(lblCodLineaCredito.Text, Convert.ToInt32(idObjeto.ToString()), (Usuario)Session["Usuario"]);
            }
            if (LstAtributos.Count > 0)
            {
                if ((LstAtributos != null) || (LstAtributos.Count != 0))
                {
                    gvAtributos.DataSource = LstAtributos;
                    gvAtributos.DataBind();
                }
                Session["Atributos"] = LstAtributos;
            }
            else
            {
                InicializarAtributos();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(LineasCreditoServicio.CodigoPrograma, "ObtenerDatos", ex);
        }
    }


    Boolean validarDatos()
    {
        if (txtNombreGrupo.Text == "")
        {
            VerError("Ingrese la Descripcion del Grupo");
            return false;
        }

        List<LineasCredito> lstAtributos = new List<LineasCredito>();
        lstAtributos = ObtenerListaAtributos();

        int contador = 0;
        foreach (LineasCredito rFila in lstAtributos)
        {
            contador++;
            int cont = 0;
            Int64 cod = rFila.cod_atr;
            foreach (LineasCredito Linea in lstAtributos)
            {
                if (Linea.cod_atr == cod)
                    cont++;
            }
            if (cont > 1)
            {
                VerError("No puede seleccionar mas de una vez la descripcion del atributo");
                return false;
            }

            if (rFila.formacalculo == "6") continue;
            if (rFila.formacalculo == "1")
            {
                if (rFila.tasa == null)
                {
                    VerError("Error en los atributos Fila Nro: " + contador + ", Ingrese el valor de la tasa");
                    return false;
                }
                if (rFila.tipotasa == null)
                {
                    VerError("Error en los atributos Fila Nro: " + contador + ", Ingrese el tipo de la tasa");
                    return false;
                }
            }
            else
            {
                if (rFila.desviacion == null)
                {
                    VerError("Error en los atributos Fila Nro: " + contador + ", Ingrese el valor de la desviación");
                    return false;
                }
                if (rFila.tipo_historico == null)
                {
                    VerError("Error en los atributos Fila Nro: " + contador + ", Ingrese el tipo de histórico");
                    return false;
                }
            }
        }

        return true;
    }


    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            if (validarDatos())
            {
                Xpinn.FabricaCreditos.Entities.LineasCredito vAtributos = new Xpinn.FabricaCreditos.Entities.LineasCredito();

                vAtributos.cod_linea_credito = lblCodLineaCredito.Text;
                if (idObjeto != "" && txtCodRango.Text != "")
                    vAtributos.cod_rango_atr = Convert.ToInt64(txtCodRango.Text);
                else
                    vAtributos.cod_rango_atr = 0;
                vAtributos.nombre = txtNombreGrupo.Text; //NOMBRE DEL GRUPO

                //Obtener Lista de Atributos
                vAtributos.lstAtributos = new List<LineasCredito>();
                vAtributos.lstAtributos = ObtenerListaAtributos();

                //Obtener lista de rango de topes
                vAtributos.lstTopes = new List<RangosTopes>();
                vAtributos.lstTopes = ObtenerListaTopes();

                // Si se esta creando un nuevo grupo determinar si el grupo ya esta creado
                if (Session["Operacion"].ToString() == "N" && txtCodRango.Text == "")
                {
                    vAtributos = LineasCreditoServicio.CrearAtributosEXPINN(vAtributos, (Usuario)Session["usuario"]);
                }
                else
                {
                    vAtributos = LineasCreditoServicio.ModificarAtributosEXPINN(vAtributos, (Usuario)Session["usuario"]);
                }

                // Se vuelve a cargar la variable de sesión para que al regresar a la línea cargue los datos
                Session[LineasCreditoServicio.CodigoPrograma + ".id"] = lblCodLineaCredito.Text;
                operacion = null;
                Navegar(Pagina.Nuevo);
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(LineasCreditoServicio.CodigoPrograma, "btnGuardar_Click", ex);
        }
    }


    private void TraerResultadosLista()
    {
        lstDatosSolicitud.Clear();
        lstDatosSolicitud = LineasCreditoServicio.ListasDesplegables(ListaSolicitada, (Usuario)Session["Usuario"]);
    }


    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        // Se vuelve a cargar la variable de sesión para que al regresar a la línea cargue los datos
        Session[LineasCreditoServicio.CodigoPrograma + ".id"] = lblCodLineaCredito.Text;
        operacion = null;
        Navegar(Pagina.Nuevo);
    }


    protected List<LineasCredito> ObtenerListaAtributos()
    {
        List<LineasCredito> lstAtributos = new List<LineasCredito>();
        List<LineasCredito> lista = new List<LineasCredito>();

        foreach (GridViewRow rfila in gvAtributos.Rows)
        {
            LineasCredito eatrib = new LineasCredito();

            eatrib.cod_linea_credito = this.lblCodLineaCredito.Text;
            if (txtCodRango.Text != "")
                eatrib.cod_rango_atr = Convert.ToInt64(txtCodRango.Text);

            DropDownListGrid ddlAtributos = (DropDownListGrid)rfila.FindControl("ddlAtributos");
            if (ddlAtributos.SelectedValue != null || ddlAtributos.SelectedIndex != 0)
                eatrib.cod_atr = Convert.ToInt32(ddlAtributos.SelectedValue);

            DropDownListGrid ddlFormaCalculo = (DropDownListGrid)rfila.FindControl("ddlFormaCalculo");
            if (ddlFormaCalculo.SelectedValue != null || ddlFormaCalculo.SelectedIndex != 0)
                eatrib.formacalculo = Convert.ToString(ddlFormaCalculo.SelectedValue);

            DropDownListGrid ddltipotasa = (DropDownListGrid)rfila.FindControl("ddltipotasa");
            if (ddltipotasa.SelectedIndex != 0)
                eatrib.tipotasa = ConvertirAEntero(ddltipotasa.SelectedValue);
            else
                eatrib.tipotasa = null;

            TextBox txttasa = (TextBox)rfila.FindControl("txttasa");
            if (txttasa.Text != "")
                eatrib.tasa = ConvertirADecimal(txttasa.Text);
            else
                eatrib.tasa = null;

            TextBox txtDesviacion = (TextBox)rfila.FindControl("txtDesviacion");
            if (txtDesviacion.Text != "")
                eatrib.desviacion = ConvertirADecimal(txtDesviacion.Text);
            else
                eatrib.desviacion = null;

            DropDownListGrid ddlTipoHistorico = (DropDownListGrid)rfila.FindControl("ddlTipoHistorico");
            if (ddlTipoHistorico.SelectedIndex != 0)
                eatrib.tipo_historico = Convert.ToInt32(ddlTipoHistorico.SelectedValue);
            else
                eatrib.tipo_historico = null;

            CheckBox chkCobramora = (CheckBox)rfila.FindControl("chkCobramora");
            if (chkCobramora.Checked == true)
                eatrib.cobra_mora = 1;
            else
                eatrib.cobra_mora = 0;

            lista.Add(eatrib);
            Session["Atributos"] = lista;

            if (eatrib.cod_atr != 0 && eatrib.formacalculo != "" && eatrib.formacalculo != null)
                lstAtributos.Add(eatrib);
        }
        return lstAtributos;
    }

    protected decimal? ConvertirADecimal(string sDato)
    {
        try
        {
            return Convert.ToDecimal(sDato);
        }
        catch
        {
            return null;
        }
    }

    protected Int64? ConvertirAEntero(string sDato)
    {
        try
        {
            return Convert.ToInt64(sDato);
        }
        catch
        {
            return null;
        }
    }

    protected List<RangosTopes> ObtenerListaTopes()
    {
        List<RangosTopes> lstTopes = new List<RangosTopes>();
        List<RangosTopes> lista = new List<RangosTopes>();

        foreach (GridViewRow rfila in gvTopes.Rows)
        {
            RangosTopes etope = new RangosTopes();

            Label lbltope = (Label)rfila.FindControl("lbltope");
            if (lbltope.Text != "")
                etope.idtope = Convert.ToInt64(lbltope.Text);

            etope.cod_linea_credito = this.lblCodLineaCredito.Text;
            if (txtCodRango.Text != "")
                etope.cod_rango_atr = Convert.ToInt64(txtCodRango.Text);

            DropDownListGrid ddlDescrpTope = (DropDownListGrid)rfila.FindControl("ddlDescrpTope");
            if (ddlDescrpTope.SelectedValue != null || ddlDescrpTope.SelectedIndex != 0)
                etope.tipo_tope = Convert.ToInt32(ddlDescrpTope.SelectedValue);

            TextBox txttopeminimo = (TextBox)rfila.FindControl("txttopeminimo");
            if (txttopeminimo.Text != "")
                etope.minimo = Convert.ToString(txttopeminimo.Text);
            else
                etope.minimo = null;

            TextBox txttopemaximo = (TextBox)rfila.FindControl("txttopemaximo");
            if (txttopemaximo.Text != "")
                etope.maximo = Convert.ToString(txttopemaximo.Text);
            else
                etope.maximo = null;

            lista.Add(etope);
            Session["RangosTopes"] = lista;

            if (etope.minimo != "" && etope.minimo != null || etope.maximo != "" && etope.maximo != null && etope.tipo_tope != 0)
                lstTopes.Add(etope);
        }

        return lstTopes;
    }



    protected void btnDetalle_Click(object sender, EventArgs e)
    {
        ObtenerListaAtributos();
        List<LineasCredito> lstAtributos = new List<LineasCredito>();

        if (Session["Atributos"] != null && gvAtributos.Rows.Count > 0)
        {
            // InicializarAtributos();
            lstAtributos = (List<LineasCredito>)Session["Atributos"];

            for (int i = 1; i <= 1; i++)
            {
                LineasCredito eActi = new LineasCredito();
                eActi.cod_atr = 0;
                eActi.descripcion = "";
                eActi.tasa = null;
                eActi.tipotasa = null;
                eActi.desviacion = null;
                eActi.tipo_historico = 0;
                eActi.formacalculo = "";
                lstAtributos.Add(eActi);
            }
            gvAtributos.PageIndex = gvAtributos.PageCount;
            gvAtributos.DataSource = lstAtributos;
            gvAtributos.DataBind();
            Session["Atributos"] = lstAtributos;
        }
        else
        {
            InicializarAtributos();
        }
    }


    protected void gvAtributos_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LineasCreditoService Atributosservicio = new LineasCreditoService();
            HistoricoTasaService HistoricoServicio = new HistoricoTasaService();
            TipoTasaService TasaServicio = new TipoTasaService();

            DropDownList ddlAtributos = (DropDownList)e.Row.FindControl("ddlAtributos");
            if (ddlAtributos != null)
            {
                Atributos atributo = new Atributos();
                ddlAtributos.DataSource = Atributosservicio.ListarAtributos(atributo, (Usuario)Session["usuario"]);
                ddlAtributos.DataTextField = "descripcion";
                ddlAtributos.DataValueField = "cod_atr";
                ddlAtributos.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));
                ddlAtributos.SelectedIndex = 0;
                ddlAtributos.DataBind();
            }

            Label lblcodatributo = (Label)e.Row.FindControl("lblcodatributo");
            if (lblcodatributo.Text != "")
                ddlAtributos.SelectedValue = lblcodatributo.Text;

            DropDownList ddlFormaCalculo = (DropDownList)e.Row.FindControl("ddlFormaCalculo");
            if (ddlFormaCalculo != null)
            {
                ddlFormaCalculo.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));
                ddlFormaCalculo.Items.Insert(1, new ListItem("Tasa Fija", "1"));
                ddlFormaCalculo.Items.Insert(2, new ListItem("Ponderada", "2"));
                ddlFormaCalculo.Items.Insert(3, new ListItem("Tasa Historico", "3"));
                ddlFormaCalculo.Items.Insert(4, new ListItem("Promedio Historico", "4"));
                ddlFormaCalculo.Items.Insert(5, new ListItem("Tasa Variable", "5"));
                ddlFormaCalculo.Items.Insert(6, new ListItem("Rangos", "6"));
                ddlFormaCalculo.SelectedIndex = 0;
                ddlFormaCalculo.DataBind();
            }

            Label lblformacalculo = (Label)e.Row.FindControl("lblformacalculo");
            if (lblformacalculo != null)
                ddlFormaCalculo.SelectedValue = lblformacalculo.Text;

            DropDownList ddlTipoHistorico = (DropDownList)e.Row.FindControl("ddlTipoHistorico");
            if (ddlTipoHistorico != null)
            {
                TipoTasaHist tasahistorica = new TipoTasaHist();
                ddlTipoHistorico.DataSource = HistoricoServicio.tipohistorico((Usuario)Session["usuario"]);
                ddlTipoHistorico.DataTextField = "DESCRIPCION";
                ddlTipoHistorico.DataValueField = "TIPODEHISTORICO";
                ddlTipoHistorico.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));
                ddlTipoHistorico.SelectedIndex = 0;
                ddlTipoHistorico.DataBind();
            }

            Label lbltipohistorico = (Label)e.Row.FindControl("lbltipohistorico");
            if (lbltipohistorico.Text != "")
                ddlTipoHistorico.SelectedValue = lbltipohistorico.Text;

            DropDownList ddltipotasa = (DropDownList)e.Row.FindControl("ddltipotasa");
            if (ddltipotasa != null)
            {
                TipoTasa tasa = new TipoTasa();
                ddltipotasa.DataSource = TasaServicio.ListarTipoTasa(tasa, (Usuario)Session["usuario"]);
                ddltipotasa.DataTextField = "NOMBRE";
                ddltipotasa.DataValueField = "COD_TIPO_TASA";
                ddltipotasa.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));
                ddltipotasa.SelectedIndex = 0;
                ddltipotasa.DataBind();
            }

            Label lbltipotasa = (Label)e.Row.FindControl("lbltipotasa");
            if (lbltipotasa.Text != "")
                ddltipotasa.SelectedValue = lbltipotasa.Text;

            if (idObjeto != "")
            {
                TextBox txtDesviacion = (TextBox)e.Row.FindControl("txtDesviacion");
                TextBox txttasa = (TextBox)e.Row.FindControl("txttasa");
                if (ddlFormaCalculo.SelectedValue == "1")
                {
                    txttasa.Enabled = true;
                    ddltipotasa.Enabled = true;
                    txtDesviacion.Enabled = false;
                    ddlTipoHistorico.Enabled = false;
                }
                else
                {
                    txttasa.Enabled = false;
                    ddltipotasa.Enabled = false;
                    txtDesviacion.Enabled = true;
                    ddlTipoHistorico.Enabled = true;
                    txttasa.Text = "";
                    ddltipotasa.SelectedValue = "0";
                }
            }
        }
    }


    protected void gvAtributos_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int conseID = Convert.ToInt32(gvAtributos.DataKeys[e.RowIndex].Values[0].ToString());

        // Determinar la lista de atributos de la gridview
        ObtenerListaAtributos();
        List<LineasCredito> LstAtributos;
        LstAtributos = (List<LineasCredito>)Session["Atributos"];

        // Borrar el atributo de la lista y si ya fue grabado entonces borrarlo de la base de datos.
        try
        {
            foreach (LineasCredito atributos in LstAtributos)
            {
                if (Convert.ToInt64(atributos.cod_atr) == conseID)
                {
                    atributos.cod_linea_credito = this.lblCodLineaCredito.Text;
                    if (txtCodRango.Text != "")
                    {
                        atributos.cod_rango_atr = Convert.ToInt64(txtCodRango.Text);
                        if (conseID > 0)
                            LineasCreditoServicio.EliminarAtributos(atributos, (Usuario)Session["usuario"]);
                    }
                    LstAtributos.Remove(atributos);

                    break;
                }
            }
        }
        catch (Xpinn.Util.ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }

        gvAtributos.DataSourceID = null;
        gvAtributos.DataBind();

        gvAtributos.DataSource = LstAtributos;
        gvAtributos.DataBind();

        Session["Atributos"] = LstAtributos;

    }


    protected void ddlFormaCalculo_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownListGrid ddlFormaCalculo = (DropDownListGrid)sender;
        int nItem = Convert.ToInt32(ddlFormaCalculo.CommandArgument);
        if (ddlFormaCalculo != null)
        {
            DropDownList ddltipotasa = (DropDownList)gvAtributos.Rows[nItem].FindControl("ddltipotasa");
            DropDownList ddlAtributos = (DropDownList)gvAtributos.Rows[nItem].FindControl("ddlAtributos");
            DropDownList ddlTipoHistorico = (DropDownList)gvAtributos.Rows[nItem].FindControl("ddlTipoHistorico");
            TextBox txtDesviacion = (TextBox)gvAtributos.Rows[nItem].FindControl("txtDesviacion");

            TextBox txttasa = (TextBox)gvAtributos.Rows[nItem].FindControl("txttasa");

            if (ddlFormaCalculo.SelectedValue == "1" || ddlFormaCalculo.SelectedValue == "6")
            {
                txttasa.Enabled = true;
                ddltipotasa.Enabled = true;
                txtDesviacion.Enabled = false;
                ddlTipoHistorico.Enabled = false;
            }
            else
            {
                txttasa.Enabled = false;
                ddltipotasa.Enabled = false;
                txtDesviacion.Enabled = true;
                ddlTipoHistorico.Enabled = true;
                txttasa.Text = "";
                ddltipotasa.SelectedValue = "0";
            }
        }
    }



    protected void gvTopes_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LineasCreditoService Topesservicio = new LineasCreditoService();

            DropDownList ddlDescrpTope = (DropDownList)e.Row.FindControl("ddlDescrpTope");

            if (ddlDescrpTope != null)
            {
                RangosTopes tope = new RangosTopes();
                ddlDescrpTope.DataSource = Topesservicio.ListarTopes(tope, (Usuario)Session["usuario"]);
                ddlDescrpTope.DataTextField = "descripcion";
                ddlDescrpTope.DataValueField = "tipo_tope";
                ddlDescrpTope.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));
                ddlDescrpTope.SelectedIndex = 0;
                ddlDescrpTope.DataBind();
            }

            Label lbldescripciontope = (Label)e.Row.FindControl("lbldescripciontope");
            if (lbldescripciontope.Text != "")
                ddlDescrpTope.SelectedValue = lbldescripciontope.Text;
        }
    }


    protected void gvTopes_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int conseID = Convert.ToInt32(gvTopes.DataKeys[e.RowIndex].Values[0].ToString());

        // Obtener listado de topes
        ObtenerListaTopes();
        List<RangosTopes> LstTopes;
        LstTopes = (List<RangosTopes>)Session["RangosTopes"];

        // Borrar el tope del listado y si existe en la base de datos borrarlo
        try
        {
            foreach (RangosTopes topes in LstTopes)
            {
                if (Convert.ToInt64(topes.idtope) == conseID)
                {
                    if (conseID > 0)
                    {
                        if (txtCodRango.Text != "")
                            topes.cod_rango_atr = Convert.ToInt64(txtCodRango.Text);
                        LineasCreditoServicio.EliminarTopes(topes, (Usuario)Session["usuario"]);
                    }
                    LstTopes.Remove(topes);
                    break;
                }
            }
        }
        catch (Xpinn.Util.ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }

        gvTopes.DataSourceID = null;
        gvTopes.DataBind();

        gvTopes.DataSource = LstTopes;
        gvTopes.DataBind();

        Session["RangosTopes"] = LstTopes;
    }


    protected void btnDetalleTopes_Click(object sender, EventArgs e)
    {
        ObtenerListaTopes();
        List<RangosTopes> lstTopes = new List<RangosTopes>();
        if (gvTopes.Rows.Count > 0)
        {
            if (Session["RangosTopes"] != null)
            {

                lstTopes = (List<RangosTopes>)Session["RangosTopes"];

                for (int i = 1; i <= 1; i++)
                {
                    RangosTopes eActi = new RangosTopes();
                    eActi.idtope = 0;
                    eActi.descripcion = "";
                    eActi.minimo = "";
                    eActi.maximo = "";

                    lstTopes.Add(eActi);
                }
                gvTopes.PageIndex = gvTopes.PageCount;
                gvTopes.DataSource = lstTopes;
                gvTopes.DataBind();
                Session["RangosTopes"] = lstTopes;
            }
        }
        else
        {
            InicializarTopes();
        }
    }
}
