using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.FabricaCreditos.Entities;
using Xpinn.FabricaCreditos.Services;
using Xpinn.Util;

partial class Nuevo : GlobalWeb
{
    private Xpinn.FabricaCreditos.Services.LineasCreditoService LineasCreditoServicio = new Xpinn.FabricaCreditos.Services.LineasCreditoService();
    private List<Xpinn.FabricaCreditos.Entities.Persona1> lstDatosSolicitud = new List<Xpinn.FabricaCreditos.Entities.Persona1>();
    private String ListaSolicitada = null;  // Cadena en la que se indica la lista a solicitar
    private String operacion;

    #region Métodos Iniciales

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            VisualizarOpciones("100214", "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
            toolBar.eventoRegresar += btnRegresar_Click;
            toolBar.MostrarConsultar(true);
            toolBar.MostrarGuardar(false);
            toolBar.MostrarRegresar(false);
            toolBar.MostrarCancelar(false);
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
                CargarLista();
                InicializarRangos();
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

    #endregion

    #region Inicializar Campos
    protected void CargarLista()
    {
        PoblarListas poblar = new PoblarListas();
        poblar.PoblarListaDesplegable("ATRIBUTOS", "COD_ATR, NOMBRE", "", "1", ddlAtributos, (Usuario)Session["usuario"]);
    }

    protected void InicializarRangos()
    {
        List<RangosTopes> lstRangos = new List<RangosTopes>();
        for (int i = gvRangos.Rows.Count; i < 1; i++)
        {
            RangosTopes vTope = new RangosTopes();
            vTope.cod_atr = 0;
            vTope.minimo = "0";
            vTope.maximo = "0";
            //vTope.tipo_valor = 0;
            vTope.valor = 0;

            lstRangos.Add(vTope);
        }
        gvRangos.DataSource = lstRangos;
        gvRangos.DataBind();
        Session["RangosTopes"] = lstRangos;
    }

    #endregion

    #region Métodos para obtener datos
    protected void ObtenerDatos(string idObjeto)
    {

        LineasCreditoService Atributosservicio = new LineasCreditoService();
        try
        {            
            //Obtener Grilla de Topes         
            List<RangosTopes> LstRangos = new List<RangosTopes>();
            if (idObjeto != "")
            {
                LstRangos = LineasCreditoServicio.ListarRangosAtributos(Convert.ToInt64(ddlAtributos.SelectedValue), (Usuario)Session["Usuario"]);
            }
            if (LstRangos.Count > 0)
            {
                if ((LstRangos != null) || (LstRangos.Count != 0))
                {
                    ddlTipoTope.SelectedValue = Convert.ToString(LstRangos[0].tipo_tope);
                    ddlTipoValor.SelectedValue = Convert.ToString(LstRangos[0].tipo_valor);
                    gvRangos.DataSource = LstRangos;
                    gvRangos.DataBind();
                }
                Session["RangosTopes"] = LstRangos;
            }
            else
            {
                InicializarRangos();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(LineasCreditoServicio.CodigoPrograma, "ObtenerDatos", ex);
        }
    }

    protected List<RangosTopes> ObtenerListaRangos()
    {
        List<RangosTopes> lstTopes = new List<RangosTopes>();
        List<RangosTopes> lista = new List<RangosTopes>();

        foreach (GridViewRow rfila in gvRangos.Rows)
        {
            RangosTopes etope = new RangosTopes();

            TextBox txtDesde = (TextBox)rfila.FindControl("txtDesde");
            if (txtDesde.Text != "")
                etope.minimo = Convert.ToString(txtDesde.Text);
            else
                etope.minimo = null;

            TextBox txtHasta = (TextBox)rfila.FindControl("txtHasta");
            if (txtHasta.Text != "")
                etope.maximo = Convert.ToString(txtHasta.Text);
            else
                etope.maximo = null;

            ctlDecimales txtValor = (ctlDecimales)rfila.FindControl("txtValor");
            if (txtValor.Text != "")
                etope.valor = Convert.ToDecimal(txtValor.Text);
            else
                etope.valor = 0;

            if (ddlAtributos.SelectedIndex > 0)
                etope.cod_atr = Convert.ToInt64(ddlAtributos.SelectedValue);

            if (ddlTipoTope.SelectedIndex > 0)
                etope.tipo_tope = Convert.ToInt64(ddlTipoTope.SelectedValue);

            if (ddlTipoValor.SelectedIndex > 0)
                etope.tipo_valor = Convert.ToInt64(ddlTipoValor.SelectedValue);

            lista.Add(etope);
            Session["RangosTopes"] = lista;

            if (etope.minimo != "" && etope.minimo != null || etope.maximo != "" && etope.maximo != null && etope.tipo_tope != 0)
                lstTopes.Add(etope);
        }

        return lstTopes;
    }

    Boolean validarDatos()
    {
        VerError("");
        if (ddlAtributos.SelectedIndex <= 0)
        {
            VerError("Seleccione un atributo");
            return false;
        }

        if (ddlTipoValor.SelectedIndex <= 0)
        {
            VerError("Debe seleccionar el tipo de valor");
            return false;
        }


        List<RangosTopes> lstRangos = new List<RangosTopes>();
        lstRangos = ObtenerListaRangos();

        Dictionary<string, string> rangos = new Dictionary<string, string>();
        Dictionary<decimal, decimal> valores = new Dictionary<decimal, decimal>();

        ///Validar que no hayan mínimos repetidos
        foreach (RangosTopes rango in lstRangos)
        {
            if (!rangos.ContainsKey(rango.minimo))
            {
                rangos.Add(rango.minimo, "");
            }
            else
            {
                VerError(string.Format("El rango entre {0} y {1} con valor {2} tiene un valor DESDE que ya se encuentra ingresado ", rango.minimo, rango.maximo, rango.valor));
                return false;
            }

        }

        ///Validar que no hayan máximos repetidos
        rangos.Clear();

        foreach (RangosTopes rango in lstRangos)
        {
            if (!rangos.ContainsKey(rango.maximo))
            {
                rangos.Add(rango.maximo, "");
            }
            else
            {
                VerError(string.Format("El rango entre {0} y {1} con valor {2} tiene un valor MÁXIMO que ya se encuentra ingresado }", rango.minimo, rango.maximo, rango.valor));
                return false;
            }

        }

        ///Validar que no hayan valores repetidos
        valores.Clear();

        foreach (RangosTopes rango in lstRangos)
        {
            if (!valores.ContainsKey(rango.valor))
            {
                valores.Add(rango.valor, 0);
            }
            else
            {
                VerError(string.Format("El valor {0} del rango entre {1} y {2} ya se encuentra ingresado ", rango.valor, rango.minimo, rango.maximo));
                return false;
            }
        }


        foreach (RangosTopes rango in lstRangos)
        {
            //Validar que el rango desde y el rango hasta no sean iguales
            if (rango.minimo == rango.maximo)
            {
                VerError(string.Format("El rango desde: {0} es igual al rango hasta: {1} correspondientes al valor: {2}", rango.minimo, rango.maximo, rango.valor));
                return false;
            }
            //Validar que el rango mínimo no sea mayor al rango máximo 
            if (Convert.ToInt64(rango.minimo) > Convert.ToInt64(rango.maximo))
            {
                VerError(string.Format("El rango desde: {0} es mayor al rango hasta: {1} correspondientes al valor: {2}", rango.minimo, rango.maximo, rango.valor));
                return false;
            }

            //Validar que el valor no sea cero
            if (rango.valor == 0)
            {
                VerError(string.Format("Debe ingresar el valor correspondiente a los rangos desde: {0} y hasta: {1} ", rango.minimo, rango.maximo));
                return false;
            }

            //Validar cruce de rangos
            foreach (RangosTopes vRango in lstRangos)
            {
                if (Convert.ToInt64(rango.minimo) != Convert.ToInt64(vRango.minimo) && Convert.ToInt64(rango.minimo) != Convert.ToInt64(vRango.minimo) && rango.valor != vRango.valor)
                {
                    if (Convert.ToInt64(rango.minimo) > Convert.ToInt64(vRango.minimo) && Convert.ToInt64(rango.minimo) < Convert.ToInt64(vRango.maximo))
                    {
                        VerError(string.Format("El rango entre {0} y {1} se cruza con el rango entre {2} y {3} ", rango.minimo, rango.maximo, vRango.minimo, vRango.maximo));
                        return false;
                    }
                    else if (Convert.ToInt64(rango.minimo) < Convert.ToInt64(vRango.minimo) && Convert.ToInt64(rango.minimo) < Convert.ToInt64(vRango.maximo) && Convert.ToInt64(rango.maximo) > Convert.ToInt64(vRango.minimo))
                    {
                        VerError(string.Format("El rango entre {0} y {1} se cruza con el rango entre {2} y {3} ", vRango.minimo, vRango.maximo, rango.minimo, rango.maximo));
                        return false;
                    }
                    else if (Convert.ToInt64(rango.minimo) == Convert.ToInt64(vRango.maximo))
                    {
                        VerError(string.Format("El rango desde: {0} es igual al ramgo maximo y {1} ", rango.minimo, vRango.maximo));
                        return false;
                    }
                }
            }
        }
        return true;
    }

    #endregion

    #region Eventos de botones
    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            if (validarDatos())
            {
                ObtenerListaRangos();
                RangosTopes vRangos = new RangosTopes();
                List<RangosTopes> lstRangos = new List<RangosTopes>();
                if (Session["RangosTopes"] != null)
                    lstRangos = (List<RangosTopes>)Session["RangosTopes"];
                lstRangos = LineasCreditoServicio.CrearRanValAtributo(lstRangos, (Usuario)Session["usuario"]);
                Session["RangosTopes"] = lstRangos;
                lblMensajeGrabar.Text = "Datos Grabados Correctamente";
                mvParametrosAtributos.ActiveViewIndex = 1;
                Site toolBar = (Site)Master;
                toolBar.MostrarRegresar(true);
                toolBar.MostrarGuardar(false);
                toolBar.MostrarCancelar(false);
                toolBar.MostrarConsultar(false);
            }
        }
        catch (ExceptionBusiness ex)
        {
            //VerError(ex.Message);
            lblMensajeGrabar.Text = "Se ha generado un error, los datos no han sido grabados";
            mvParametrosAtributos.ActiveViewIndex = 1;
            Site toolBar = (Site)Master;
            toolBar.MostrarRegresar(true);
            toolBar.MostrarGuardar(false);
            toolBar.MostrarCancelar(false);
            toolBar.MostrarConsultar(false);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(LineasCreditoServicio.CodigoPrograma, "btnGuardar_Click", ex);
            lblMensajeGrabar.Text = "Se ha generado un error, los datos no han sido grabados";
            mvParametrosAtributos.ActiveViewIndex = 1;
            Site toolBar = (Site)Master;
            toolBar.MostrarRegresar(true);
            toolBar.MostrarGuardar(false);
            toolBar.MostrarCancelar(false);
            toolBar.MostrarConsultar(false);
        }
    }

    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        if (ddlAtributos.SelectedIndex <= 0)
            VerError("Debe seleccionar el atributo");
        else
        {
            ObtenerDatos(ddlAtributos.SelectedValue);
            panelValores.Visible = true;
            btnDetalle.Visible = true;
            gvRangos.Visible = true;
            Site toolBar = (Site)this.Master;
            toolBar.MostrarGuardar(true);
            toolBar.MostrarCancelar(true);
        }
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Session["RangosTopes"] = null;
        Site toolBar = (Site)this.Master;
        toolBar.MostrarGuardar(false);
        toolBar.MostrarConsultar(true);
        toolBar.MostrarCancelar(false);        
        panelValores.Visible = false;
        btnDetalle.Visible = false;
        gvRangos.Visible = false;
    }

    protected void btnRegresar_Click(object sender, EventArgs e)
    {
        Session["RangosTopes"] = null;
        Site toolBar = (Site)this.Master;
        toolBar.MostrarGuardar(false);
        toolBar.MostrarConsultar(true);
        toolBar.MostrarRegresar(false);
        mvParametrosAtributos.ActiveViewIndex = 0;
        panelValores.Visible = false;
        btnDetalle.Visible = false;
        gvRangos.Visible = false;
    }

    protected void btnDetalle_Click(object sender, EventArgs e)
    {
        ObtenerListaRangos();
        List<RangosTopes> lstRangos = new List<RangosTopes>();

        if (Session["RangosTopes"] != null && gvRangos.Rows.Count > 0)
        {
            lstRangos = (List<RangosTopes>)Session["RangosTopes"];

            for (int i = 1; i <= 1; i++)
            {
                RangosTopes eActi = new RangosTopes();
                //eActi.idtope = 0;
                eActi.valor = 0;
                eActi.minimo = "";
                eActi.maximo = "";

                lstRangos.Add(eActi);
            }
            gvRangos.PageIndex = gvRangos.PageCount;
            gvRangos.DataSource = lstRangos;
            gvRangos.DataBind();
            Session["RangosTopes"] = lstRangos;
        }
        else
        {
            InicializarRangos();
        }
    }

    #endregion

    #region Eventos tabla gvRangos
    protected void gvRangos_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Int64 minimo = 0;
        Int64 maximo = 0;
        foreach (GridViewRow rfila in gvRangos.Rows)
        {
            if(rfila.RowIndex == e.RowIndex)
            {
                TextBox txtDesde = (TextBox)rfila.FindControl("txtDesde");
                if (txtDesde.Text != "")
                    minimo = Convert.ToInt64(txtDesde.Text);

                TextBox txtHasta = (TextBox)rfila.FindControl("txtHasta");
                if (txtHasta.Text != "")
                    maximo = Convert.ToInt64(txtHasta.Text);
            }
            
        }
        ObtenerListaRangos();
        List<RangosTopes> LstRangos;
        LstRangos = (List<RangosTopes>)Session["RangosTopes"];

        try
        {
            foreach (RangosTopes vTope in LstRangos)
            {
                if (Convert.ToInt64(vTope.minimo) == minimo && Convert.ToInt64(vTope.maximo) == maximo && vTope.cod_atr == Convert.ToInt64(ddlAtributos.SelectedValue))
                {
                    LineasCreditoServicio.EliminarRanValAtributo(vTope.cod_atr, (Usuario)Session["usuario"]);
                    LstRangos.Remove(vTope);
                    break;
                }
                else if (Convert.ToInt64(vTope.minimo) == minimo && Convert.ToInt64(vTope.maximo) == maximo && vTope.cod_atr == 0)
                {
                    LstRangos.Remove(vTope);
                    break;
                }

            }
        }
        catch (Xpinn.Util.ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }

        gvRangos.DataSourceID = null;
        gvRangos.DataBind();

        gvRangos.DataSource = LstRangos;
        gvRangos.DataBind();

        Session["Atributos"] = LstRangos;

    }

    protected void gvTopes_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    LineasCreditoService Topesservicio = new LineasCreditoService();

        //    DropDownList ddlDescrpTope = (DropDownList)e.Row.FindControl("ddlDescrpTope");

        //    if (ddlDescrpTope != null)
        //    {                
        //        RangosTopes tope = new RangosTopes();
        //        ddlDescrpTope.DataSource = Topesservicio.ListarTopes(tope, (Usuario)Session["usuario"]);
        //        ddlDescrpTope.DataTextField = "descripcion";
        //        ddlDescrpTope.DataValueField = "tipo_tope";
        //        ddlDescrpTope.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));
        //        ddlDescrpTope.SelectedIndex = 0;
        //        ddlDescrpTope.DataBind();                
        //    }

        //    Label lbldescripciontope = (Label)e.Row.FindControl("lbldescripciontope");
        //    if(lbldescripciontope.Text != "")
        //        ddlDescrpTope.SelectedValue = lbldescripciontope.Text;
        //}
    }
    #endregion
}
