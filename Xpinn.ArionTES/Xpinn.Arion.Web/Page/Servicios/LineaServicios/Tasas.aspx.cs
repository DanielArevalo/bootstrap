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
using Xpinn.Servicios.Entities;
using Xpinn.Servicios.Services;

partial class Nuevo : GlobalWeb
{
    LineaServiciosServices LineaServicios = new LineaServiciosServices();
    AtributosTasasServices AtriTasaService = new AtributosTasasServices();
    private String operacion;


    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[LineaServicios.CodigoPrograma + ".CodRango"] != null)
                VisualizarOpciones(LineaServicios.CodigoPrograma, "E");
            else
                VisualizarOpciones(LineaServicios.CodigoPrograma, "A");
            
            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;            
            toolBar.eventoCancelar += btnCancelar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(LineaServicios.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Session[LineaServicios.CodigoPrograma + ".LineaServicio"] != null)
                {
                    lblCodLineaServicio.Text = Session[LineaServicios.CodigoPrograma + ".LineaServicio"].ToString();
                    RangoTasasTope nuevo = new RangoTasasTope();
                    nuevo.cod_linea_servicio = Session[LineaServicios.CodigoPrograma + ".LineaServicio"].ToString();
                    //Session["ValorMaximo"] = AtriTasaService.ObtenerValorTopeMaximo(nuevo, (Usuario)Session["usuario"]);
                    Session.Remove(LineaServicios.CodigoPrograma + ".LineaServicio");
                } 
                Session["RangosTopes"] = null;
                Inicializar();
                if (Session[LineaServicios.CodigoPrograma + ".CodRango"] != null)
                {
                    idObjeto = Session[LineaServicios.CodigoPrograma + ".CodRango"].ToString();
                    ObtenerDatos(idObjeto);
                }
                else
                {
                    lblCodRango.Text = "";
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
            BOexcepcion.Throw(LineaServicios.CodigoPrograma, "Page_Load", ex);
        }
    }

    private void Inicializar()
    {
        ctlTasa.Inicializar();
    }


    protected void InicializarTopes()
    {
        List<RangoTasasTope> lstTopes = new List<RangoTasasTope>();
        for (int i = gvTopes.Rows.Count; i < 4; i++)
        {
            RangoTasasTope eTope = new RangoTasasTope();
            eTope.codtope = 0;
            eTope.tipo_tope = 0;
            eTope.minimo = "";
            eTope.maximo = "";
            lstTopes.Add(eTope);
        }
        gvTopes.DataSource = lstTopes;
        gvTopes.DataBind();
        Session["RangosTopes"] = lstTopes;
    }

    protected List<RangoTasasTope> ObtenerListaTopes()
    {
        List<RangoTasasTope> lstTopes = new List<RangoTasasTope>();
        List<RangoTasasTope> lista = new List<RangoTasasTope>();

        foreach (GridViewRow rfila in gvTopes.Rows)
        {
            RangoTasasTope etope = new RangoTasasTope();

            Label lbltope = (Label)rfila.FindControl("lbltope");
            if (lbltope.Text != "")
                etope.codtope = Convert.ToInt32(lbltope.Text);

            etope.cod_linea_servicio = this.lblCodLineaServicio.Text;
            if (lblCodRango.Text != "")
                etope.codrango = Convert.ToInt32(lblCodRango.Text);

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

            if (((etope.minimo != "" && etope.minimo != null) || (etope.maximo != "" && etope.maximo != null)) && etope.tipo_tope != 0)
                lstTopes.Add(etope);
        }

        return lstTopes;
    }

    protected void btnDetalleTopes_Click(object sender, EventArgs e)
    {
        ObtenerListaTopes();
        List<RangoTasasTope> lstTopes = new List<RangoTasasTope>();
        if (gvTopes.Rows.Count > 0)
        {
            if (Session["RangosTopes"] != null)
            {

                lstTopes = (List<RangoTasasTope>)Session["RangosTopes"];

                for (int i = 1; i <= 1; i++)
                {
                    RangoTasasTope eTope = new RangoTasasTope();
                    eTope.codtope = 0;
                    eTope.tipo_tope = 0;
                    eTope.minimo = "";
                    eTope.maximo = "";
                    lstTopes.Add(eTope);
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

    protected void gvTopes_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Xpinn.FabricaCreditos.Services.LineasCreditoService Topesservicio = new Xpinn.FabricaCreditos.Services.LineasCreditoService();
            DropDownListGrid ddlDescrpTope = (DropDownListGrid)e.Row.FindControl("ddlDescrpTope");
            if (ddlDescrpTope != null)
            {
                //PENDIENTE CARGAR DROP
                RangosTopes tope = new RangosTopes();
                ddlDescrpTope.DataSource = Topesservicio.ListarTopes(tope, (Usuario)Session["usuario"]);
                ddlDescrpTope.DataTextField = "descripcion";
                ddlDescrpTope.DataValueField = "tipo_tope";
                ddlDescrpTope.Items.Insert(0, new ListItem("Seleccione un Item", "0"));
                ddlDescrpTope.SelectedIndex = 0;
                ddlDescrpTope.DataBind();

                Label lbldescripciontope = (Label)e.Row.FindControl("lbldescripciontope");
                if (lbldescripciontope != null)
                {
                    if (lbldescripciontope.Text.Trim() != "")
                        ddlDescrpTope.SelectedValue = lbldescripciontope.Text;
                }
            }
        }
    }


    protected void gvTopes_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Int64 conseID = Convert.ToInt64(gvTopes.DataKeys[e.RowIndex].Values[0].ToString());

        // Obtener listado de topes
        ObtenerListaTopes();
        List<RangoTasasTope> LstTopes;
        LstTopes = (List<RangoTasasTope>)Session["RangosTopes"];

        // Borrar el tope del listado y si existe en la base de datos borrarlo
        try
        {
            if (conseID > 0)
            {
                foreach (RangoTasasTope topes in LstTopes)
                {
                    if (Convert.ToInt64(topes.codtope) == conseID)
                    {
                        AtriTasaService.EliminarSoloRangoTasasTope(conseID, (Usuario)Session["usuario"]);
                        LstTopes.RemoveAt((gvTopes.PageIndex * gvTopes.PageSize) + e.RowIndex);
                        break;
                    }
                }
            }
            else
                LstTopes.RemoveAt(e.RowIndex);
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


    protected void ObtenerDatos(string idObjeto)
    {
        try
        {
            RangoTasas ptasas = new RangoTasas();
            ptasas.codrango = Convert.ToInt32(idObjeto);
            ptasas.cod_linea_servicio = lblCodLineaServicio.Text;
            AtributosLineaServicio pAtributos = new AtributosLineaServicio();
            bool rpta = false;
            string pError = "";
            rpta = AtriTasaService.ConsultarRangoTasasGeneral(ref ptasas, ref pAtributos, ref pError, (Usuario)Session["usuario"]);
            if (pError.Trim() != "")
            {
                VerError(pError.ToString());
                return;
            }
            if (rpta)
            {
                if (ptasas.codrango != 0)
                    lblCodRango.Text = ptasas.codrango.ToString();
                if (ptasas.descripcion != null)
                    txtNombreGrupo.Text = ptasas.descripcion;

                if (ptasas.lstTopes != null && ptasas.lstTopes.Count > 0)
                {
                    gvTopes.DataSource = ptasas.lstTopes;
                    gvTopes.DataBind();
                    Session["RangosTopes"] = ptasas.lstTopes;
                }
                else
                {
                    InicializarTopes();
                }

                if (pAtributos != null)
                {
                    lblCodAtrSer.Text = pAtributos.codatrser.ToString(); 
                    ctlTasa.FormaTasa = pAtributos.calculo_atr;
                    if (pAtributos.tipo_historico != null)
                        ctlTasa.TipoHistorico = Convert.ToInt32(pAtributos.tipo_historico);
                    if (pAtributos.desviacion != null)
                        ctlTasa.Desviacion = Convert.ToDecimal(pAtributos.desviacion);
                    if (pAtributos.tipo_tasa != null)
                        ctlTasa.TipoTasa = Convert.ToInt32(pAtributos.tipo_tasa);
                    if (pAtributos.tasa != null)
                        ctlTasa.Tasa = Convert.ToDecimal(pAtributos.tasa);
                }
            }
            else
                VerError("Se generó un error al realizar la consulta");
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(LineaServicios.CodigoPrograma, "ObtenerDatos", ex);
        }
    }


    private Boolean validarDatos()
    {
        if (txtNombreGrupo.Text == "")
        {
            VerError("Ingrese la Descripcion del Grupo");
            return false;
        }

        if (ctlTasa.VisibleHistorico)
        {
            if (ctlTasa.TipoHistorico == 0)
            {
                VerError("Seleccione un tipo de Histórico");
                return false;
            }
            if (ctlTasa.Desviacion == 0)
            {
                VerError("Ingrese el monto de desviación");
                return false;
            }
        }
        else if (ctlTasa.VisibleFijo)
        {
            if (ctlTasa.TipoTasa == 0)
            {
                VerError("Seleccione un tipo de Tasa");
                return false;
            }
            if (ctlTasa.Tasa == 0)
            {
                VerError("Ingrese el valor de la tasa.");
                return false;
            }
        }

        return true;
    }


    protected void btnGuardar_Click(object sender, EventArgs e)
    {        
        try
        {
            RangoTasas vTasa = new RangoTasas();
            if (validarDatos())
            {
                vTasa.cod_linea_servicio = lblCodLineaServicio.Text;
                if (idObjeto != "" && lblCodRango.Text != "")
                    vTasa.codrango = Convert.ToInt32(lblCodRango.Text);
                else
                    vTasa.codrango = 0;
                vTasa.descripcion = txtNombreGrupo.Text; //NOMBRE DEL GRUPO

                //Obtener lista de rango de topes
                vTasa.lstTopes = new List<RangoTasasTope>();

                foreach (GridViewRow rfila in gvTopes.Rows)
                {
                    DropDownListGrid ddlDescrpTope = (DropDownListGrid)rfila.FindControl("ddlDescrpTope");
                    if (ddlDescrpTope.SelectedValue == "3")
                    { 
                        TextBox txttopemaximo = (TextBox)rfila.FindControl("txttopemaximo");
                        if (!string.IsNullOrWhiteSpace(txttopemaximo.Text))
                        {
                            //if (Convert.ToInt64(Session["ValorMaximo"]) != 0)
                            //{
                            //    if (Convert.ToInt64(Session["ValorMaximo"].ToString()) < Convert.ToInt64(txttopemaximo.Text))
                            //    {
                            //        VerError("El valor maximo no debe se mayor a " + Session["ValorMaximo"].ToString() + "");
                            //        return;
                            //    }
                            //}
                        }
                        else
                        {
                            VerError("Debe digitar el valor maximo");
                            return;
                        }

                    }
                    else
                    {
                        VerError("");
                    }
                }

                vTasa.lstTopes = ObtenerListaTopes();
                if (vTasa.lstTopes.Count == 0)
                {
                    VerError("No existen topes para el grupo actual, Ingrese al menos un registro");
                    return;
                }
                AtributosLineaServicio pEntidad = new AtributosLineaServicio();
                pEntidad.codatrser = lblCodAtrSer.Text != "" ? Convert.ToInt32(lblCodAtrSer.Text) : 0;
                pEntidad.cod_atr = 2;
                pEntidad.calculo_atr = ctlTasa.FormaTasa;
                if (ctlTasa.VisibleHistorico)
                {
                    pEntidad.tipo_historico = ctlTasa.TipoHistorico;
                    pEntidad.desviacion = Convert.ToDecimal(ctlTasa.Desviacion);
                    pEntidad.tipo_tasa = null;
                    pEntidad.tasa = null;
                }
                else if (ctlTasa.VisibleFijo)
                {
                    pEntidad.tipo_historico = null;
                    pEntidad.desviacion = null;
                    pEntidad.tipo_tasa = ctlTasa.TipoTasa;
                    pEntidad.tasa = ctlTasa.Tasa;
                }
                else
                {
                    pEntidad.tipo_historico = null;
                    pEntidad.desviacion = null;
                    pEntidad.tipo_tasa = null;
                    pEntidad.tasa = null;
                }
                string pError = "";
                bool rpta = false;
                // Si se esta creando un nuevo grupo determinar si el grupo ya esta creado
                if (Session["Operacion"].ToString() == "N" && lblCodRango.Text == "")
                {
                    rpta = AtriTasaService.CrearRangoTasas(vTasa, pEntidad, ref pError, (Usuario)Session["usuario"]);
                }
                else
                {
                    rpta = AtriTasaService.ModificarRangoTasas(vTasa, pEntidad, ref pError, (Usuario)Session["usuario"]);                    
                }
                if (pError.Trim() != "")
                {
                    VerError(pError.ToString());
                    return;
                }
                if (rpta)
                {
                    // Se vuelve a cargar la variable de sesión para que al regresar a la línea cargue los datos
                    Session[LineaServicios.CodigoPrograma + ".id"] = lblCodLineaServicio.Text;
                    operacion = null;
                    Navegar(Pagina.Nuevo);
                }
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(LineaServicios.CodigoPrograma, "btnGuardar_Click", ex);
        }
    }


    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        // Se vuelve a cargar la variable de sesión para que al regresar a la línea cargue los datos
        Session[LineaServicios.CodigoPrograma + ".id"] = lblCodLineaServicio.Text;
        operacion = null;
        Navegar(Pagina.Nuevo);
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


}
