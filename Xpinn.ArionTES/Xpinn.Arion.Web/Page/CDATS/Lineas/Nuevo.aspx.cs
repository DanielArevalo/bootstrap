using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using Xpinn.CDATS.Entities;
using Xpinn.CDATS.Services;
using System.Text;
using System.IO;
using System.Web.UI.HtmlControls;

partial class Nuevo : GlobalWeb
{
    private Xpinn.CDATS.Services.LineaCDATService lineaCDATServicio = new Xpinn.CDATS.Services.LineaCDATService();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[lineaCDATServicio.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(lineaCDATServicio.CodigoPrograma, "E");
            else
                VisualizarOpciones(lineaCDATServicio.CodigoPrograma, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
            toolBar.MostrarConsultar(false);
            toolBar.eventoConsultar += btnConsultar_Click;
            ctlMensaje.eventoClick += btnContinuarMen_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(lineaCDATServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                mvAhorros.ActiveViewIndex = 0;
                CargarListas();
                if (Session[lineaCDATServicio.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[lineaCDATServicio.CodigoPrograma + ".id"].ToString();
                    txtCodLineaCDAT.Enabled = false;
                    ObtenerDatos(idObjeto);
                }
                else
                {
                    Session.Remove("RangoCDAT");
                }
                InicializarTopes();
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(lineaCDATServicio.CodigoPrograma, "Page_Load", ex);
        }
    }

    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            VerError("");
            if (ValidarDatos())
            {
                string msj;
                msj = idObjeto != "" ? "Modificar" : "Grabar";
                ctlMensaje.MostrarMensaje("Desea " + msj + " los datos ingresados?");
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(lineaCDATServicio.CodigoPrograma, "btnGuardar_Click", ex);
        }
    }
    Boolean ValidarDatos()
    {

        if (ctlTasaInteres.FormaTasa == "0")
        {
            VerError("Debe ingresar una tasa ");
            return false;
        }
        if (chkTasaCierreAnticipado.Checked)
        {
            if (ctlTasaInteresAnt.FormaTasa == "0")
            {
                VerError("Debe ingresar una tasa del Cierre anticipado ");
                return false;
            }
        }
        List<RangoCDAT> lstTopes = new List<RangoCDAT>();
        List<RangoCDAT> lista = new List<RangoCDAT>();

        foreach (GridViewRow rfila in gvTopes.Rows)
        {
            RangoCDAT etope = new RangoCDAT();
            etope.cod_lineacdat = this.lblConsecutivo.Text;

            Label lblcodrango = (Label)rfila.FindControl("lblcodrango");
            if (lblcodrango.Text != "")
                etope.cod_rango = Convert.ToInt64(lblcodrango.Text);

            DropDownListGrid ddlTipoTope = (DropDownListGrid)rfila.FindControl("ddlTipoTope");
            if (ddlTipoTope.SelectedValue != null || ddlTipoTope.SelectedIndex != 0)
                etope.tipo_tope = Convert.ToInt32(ddlTipoTope.SelectedValue);

            TextBox txtminimo = (TextBox)rfila.FindControl("txtminimo");
            if (txtminimo.Text != "")
                etope.minimo = Convert.ToString(txtminimo.Text);
            else
                etope.minimo = null;

            TextBox txtmaximo = (TextBox)rfila.FindControl("txtmaximo");
            if (txtmaximo.Text != "")
                etope.maximo = Convert.ToString(txtmaximo.Text);
            else
                etope.maximo = null;

            lista.Add(etope);
            if (etope.tipo_tope != 1)
            {
                if (etope.minimo == "" || etope.minimo == null || etope.maximo == "" || etope.maximo == null && etope.tipo_tope == 1)
                {

                    if (etope.tipo_tope == 1 || etope.minimo == null || etope.maximo == null)
                    {
                        VerError("Debe ingresar un tope minimo y maximo para montos y plazos");
                        return false;
                    }
                }
            }
            if (etope.tipo_tope != 2)
            {
                if (etope.minimo == "" || etope.minimo == null || etope.maximo == "" || etope.maximo == null && etope.tipo_tope == 2)
                {
                    if (etope.tipo_tope == 2 || etope.minimo == null || etope.maximo == null)
                    {
                        VerError("Debe ingresar un tope minimo y maximo para montos y plazos");
                        return false;
                    }

                }
            }

            lstTopes.Add(etope);

        }


        return true;

        //List<RangoCDAT> LstTopes;
        //LstTopes = (List<RangoCDAT>)Session["RangoCDAT"];

        //// Borrar el tope del listado y si existe en la base de datos borrarlo
        //try
        //{
        //    foreach (RangoCDAT topes in LstTopes)
        //    {
        //        //para plazos 
        //        if (Convert.ToInt64(topes.tipo_tope) == 2)
        //        {
        //            Session["PlazoMin"] = topes.minimo;
        //            Session["PlazoMax"] = topes.maximo;

        //            break;
        //        }
        //  }



    }
    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        try
        {
            Usuario vUsuario = new Usuario();
            vUsuario = (Usuario)Session["Usuario"];

            Xpinn.CDATS.Entities.LineaCDAT vLineaCDAT = new Xpinn.CDATS.Entities.LineaCDAT();

            if (idObjeto != "")
                vLineaCDAT = lineaCDATServicio.ConsultarLineaCDAT(Convert.ToString(idObjeto), (Usuario)Session["usuario"]);

            if (lblConsecutivo.Text != "")
                vLineaCDAT.cod_lineacdat = lblConsecutivo.Text;
            else
                vLineaCDAT.cod_lineacdat = txtCodLineaCDAT.Text;
            vLineaCDAT.descripcion = txtDescripcion.Text;
            vLineaCDAT.cod_moneda = Convert.ToInt32(ddlMoneda.Value);
            vLineaCDAT.calculo_tasa = Convert.ToInt32(ctlTasaInteres.FormaTasa);
            if (vLineaCDAT.calculo_tasa != 0 && vLineaCDAT.calculo_tasa != 1)
            {
                vLineaCDAT.tipo_historico = Convert.ToInt32(ctlTasaInteres.TipoHistorico);
                vLineaCDAT.desviacion = Convert.ToDecimal(ctlTasaInteres.Desviacion);
            }
            else
            {
                vLineaCDAT.tipo_historico = null;
                vLineaCDAT.desviacion = null;
            }
            vLineaCDAT.cod_tipo_tasa = Convert.ToInt32(ctlTasaInteres.TipoTasa);
            vLineaCDAT.lstRangos = ObtenerListaTopes();
            try
            {
                vLineaCDAT.tasa = Convert.ToDecimal(ctlTasaInteres.Tasa);
            }
            catch
            {
                vLineaCDAT.tasa = null;
            }
            //Tasa para cierre anticipado

            vLineaCDAT.calculo_tasa_ven = Convert.ToInt32(ctlTasaInteresAnt.FormaTasa);
            if (vLineaCDAT.calculo_tasa_ven != 0 && vLineaCDAT.calculo_tasa_ven != 1)
            {
                vLineaCDAT.tipo_historico_ven = Convert.ToInt32(ctlTasaInteresAnt.TipoHistorico);
                vLineaCDAT.desviacion_ven = Convert.ToDecimal(ctlTasaInteresAnt.Desviacion);
            }
            else
            {
                vLineaCDAT.tipo_historico_ven = null;
                vLineaCDAT.desviacion_ven = null;
            }

            if (!chkTasaCierreAnticipado.Checked)
            {
                vLineaCDAT.cod_tipo_tasa_ven = null;
                vLineaCDAT.tasa_ven = null;
            }
            else
            {
                vLineaCDAT.cod_tipo_tasa_ven = Convert.ToInt32(ctlTasaInteresAnt.TipoTasa);
                try
                {
                    vLineaCDAT.tasa_ven = Convert.ToDecimal(ctlTasaInteresAnt.Tasa);
                }
                catch
                {
                    vLineaCDAT.tasa_ven = null;
                }
            }
            vLineaCDAT.estado = cbEstado.Checked == true ? 1 : 0;

            vLineaCDAT.interes_por_cuenta = cbInteresPorCuenta.Checked == true ? 1 : 0; 
            vLineaCDAT.tasa_simulacion = cbCambTasaSimulacion.Checked == true ? 1 : 0;  

            vLineaCDAT.interes_anticipado = cbInteresAnticipado.Checked == true ? 1 : 0;
            vLineaCDAT.capitaliza_interes = cbCapitalizaInteres.Checked == true ? 1 : 0;

            vLineaCDAT.numero_pre_impreso = cbNumeroImpreso.Checked == true ? 1 : 0;

            vLineaCDAT.interes_prroroga = cbInteresProrroga.Checked == true ? 1 : 0;


            if (TxtPorcentajeRete.Text != "")
                vLineaCDAT.retencion = Convert.ToInt32(TxtPorcentajeRete.Text);
            else
                vLineaCDAT.retencion = 0;
            if (ddlTipoCalendario.SelectedValue != "0")
                vLineaCDAT.tipo_calendario = Convert.ToInt32(ddlTipoCalendario.SelectedValue);
            else
                vLineaCDAT.tipo_calendario = 1;

            if (idObjeto != "")
            {
                vLineaCDAT.cod_lineacdat = idObjeto.ToString();
                lineaCDATServicio.ModificarLineaCDAT(vLineaCDAT, (Usuario)Session["usuario"]);
            }
            else
            {
                vLineaCDAT = lineaCDATServicio.CrearLineaCDAT(vLineaCDAT, (Usuario)Session["usuario"]);
                idObjeto = vLineaCDAT.cod_lineacdat;
            }

            Session[lineaCDATServicio.CodigoPrograma + ".id"] = idObjeto;
            Site toolBar = (Site)this.Master;
            toolBar.MostrarGuardar(false);
            toolBar.MostrarCancelar(false);
            toolBar.MostrarConsultar(true);
            toolBar.eventoConsultar += btnConsultar_Click;
            mvAhorros.ActiveViewIndex = 1;
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(lineaCDATServicio.CodigoPrograma, "btnGuardar_Click", ex);
        }
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }

    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            Xpinn.CDATS.Entities.LineaCDAT vLineaCDAT = new Xpinn.CDATS.Entities.LineaCDAT();
            vLineaCDAT = lineaCDATServicio.ConsultarLineaCDAT(Convert.ToString(pIdObjeto), (Usuario)Session["usuario"]);
            cbInteresPorCuenta.Checked = vLineaCDAT.interes_por_cuenta == 1 ? true : false;
            cbInteresProrroga.Checked = vLineaCDAT.interes_prroroga == 1 ? true : false;

            cbCambTasaSimulacion.Checked = vLineaCDAT.tasa_simulacion == 1 ? true : false;


           // cbInteresPorCuenta.Checked = ConvertirABoolean(HttpUtility.HtmlDecode(vLineaCDAT.interes_por_cuenta.ToString()));
          //  cbCambTasaSimulacion.Checked = ConvertirABoolean(HttpUtility.HtmlDecode(vLineaCDAT.tasa_simulacion.ToString()));

            lblConsecutivo.Text = HttpUtility.HtmlDecode(vLineaCDAT.cod_lineacdat.ToString().Trim());
            if (!string.IsNullOrEmpty(vLineaCDAT.cod_lineacdat.ToString()))
                txtCodLineaCDAT.Text = HttpUtility.HtmlDecode(vLineaCDAT.cod_lineacdat.ToString());
            if (!string.IsNullOrEmpty(vLineaCDAT.descripcion.ToString()))
                txtDescripcion.Text = HttpUtility.HtmlDecode(vLineaCDAT.descripcion.ToString());
            else
                txtDescripcion.Text = "";
            if (!string.IsNullOrEmpty(vLineaCDAT.cod_moneda.ToString()))
                ddlMoneda.Value = HttpUtility.HtmlDecode(vLineaCDAT.cod_moneda.ToString());
            if (!string.IsNullOrEmpty(vLineaCDAT.retencion.ToString()))
                TxtPorcentajeRete.Text = Convert.ToString(vLineaCDAT.retencion);

            if (!string.IsNullOrEmpty(vLineaCDAT.tipo_calendario.ToString()))
                ddlTipoCalendario.SelectedValue = Convert.ToString(vLineaCDAT.tipo_calendario);

            try
            {
                if (!string.IsNullOrEmpty(vLineaCDAT.calculo_tasa.ToString()))
                    ctlTasaInteres.FormaTasa = vLineaCDAT.calculo_tasa.ToString();
                if (!string.IsNullOrEmpty(vLineaCDAT.tipo_historico.ToString()))
                    ctlTasaInteres.TipoHistorico = Convert.ToInt32(vLineaCDAT.tipo_historico);
                if (!string.IsNullOrEmpty(vLineaCDAT.desviacion.ToString()))
                    ctlTasaInteres.Desviacion = Convert.ToDecimal(vLineaCDAT.desviacion);
                if (!string.IsNullOrEmpty(vLineaCDAT.cod_tipo_tasa.ToString()))
                    ctlTasaInteres.TipoTasa = Convert.ToInt32(vLineaCDAT.cod_tipo_tasa);
                if (!string.IsNullOrEmpty(vLineaCDAT.tasa.ToString()))
                    ctlTasaInteres.Tasa = Convert.ToDecimal(vLineaCDAT.tasa);
            }


            catch (Exception ex)
            {
                VerError(ex.Message);
            }

            //tasa para cierre anticipado
            try
            {
                if (!string.IsNullOrEmpty(vLineaCDAT.calculo_tasa_ven.ToString()))
                {
                    ctlTasaInteresAnt.FormaTasa = vLineaCDAT.calculo_tasa_ven.ToString();
                }
                if (!string.IsNullOrEmpty(vLineaCDAT.tipo_historico_ven.ToString()))
                {
                    ctlTasaInteresAnt.TipoHistorico = Convert.ToInt32(vLineaCDAT.tipo_historico_ven);
                    chkTasaCierreAnticipado.Checked = true;
                }
                if (!string.IsNullOrEmpty(vLineaCDAT.desviacion_ven.ToString()))
                {
                    ctlTasaInteresAnt.Desviacion = Convert.ToDecimal(vLineaCDAT.desviacion_ven);
                    chkTasaCierreAnticipado.Checked = true;
                }
                if (!string.IsNullOrEmpty(vLineaCDAT.cod_tipo_tasa_ven.ToString()))
                {
                    ctlTasaInteresAnt.TipoTasa = Convert.ToInt32(vLineaCDAT.cod_tipo_tasa_ven);
                    chkTasaCierreAnticipado.Checked = true;
                }
                if (!string.IsNullOrEmpty(vLineaCDAT.tasa_ven.ToString()))
                {
                    ctlTasaInteresAnt.Tasa = Convert.ToDecimal(vLineaCDAT.tasa_ven);
                    chkTasaCierreAnticipado.Checked = true;
                }
            }


            catch (Exception ex)
            {
                VerError(ex.Message);
            }


            cbEstado.Checked = vLineaCDAT.estado == 1 ? true : false;
            cbInteresAnticipado.Checked = vLineaCDAT.interes_anticipado == 1 ? true : false;
            cbCapitalizaInteres.Checked = vLineaCDAT.capitaliza_interes == 1 ? true : false;
            cbNumeroImpreso.Checked = vLineaCDAT.numero_pre_impreso == 1 ? true : false;
            


            if (vLineaCDAT.lstRangos != null)
            {
                Session["RangoCDAT"] = vLineaCDAT.lstRangos;
                gvTopes.DataSource = vLineaCDAT.lstRangos;
                gvTopes.DataBind();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(lineaCDATServicio.CodigoPrograma, "ObtenerDatos", ex);
        }
    }

    private void CargarListas()
    {
        Usuario pUsuario = new Usuario();
        pUsuario = (Usuario)Session["Usuario"];
        try
        {
            ctlTasaInteres.Inicializar();
            ddlMoneda.Inicializar();
            ctlTasaInteresAnt.Inicializar();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(lineaCDATServicio.GetType().Name + "L", "CargarListas", ex);
        }

        //ddlTipoCalendario.Items.Insert(1, new ListItem("Comercial", "1"));
        //ddlTipoCalendario.Items.Insert(2, new ListItem("Calendario", "2"));
        //ddlTipoCalendario.SelectedIndex = 0;
        //ddlTipoCalendario.DataBind();

        ddlTipoCalendario.Items.Insert(0, new ListItem("Seleccione un Item", "0"));
        ddlTipoCalendario.Items.Insert(1, new ListItem("Comercial", "1"));
        ddlTipoCalendario.Items.Insert(2, new ListItem("Calendario", "2"));
        ddlTipoCalendario.SelectedIndex = 1;
        ddlTipoCalendario.DataBind();
    }

    private Boolean ConvertirABoolean(string sParametro)
    {
        if (sParametro == null)
            return false;
        if (sParametro.Trim() == "1")
            return true;
        return false;
    }

    protected void InicializarTopes()
    {
        List<RangoCDAT> lstTopes = new List<RangoCDAT>();
        if (Session["RangoCDAT"] != null)
            lstTopes = (List<RangoCDAT>)Session["RangoCDAT"];
        for (int i = gvTopes.Rows.Count; i < 2; i++)
        {
            RangoCDAT eActi = new RangoCDAT();
            eActi.cod_rango = 0;
            eActi.minimo = "";
            eActi.maximo = "";

            lstTopes.Add(eActi);
        }
        gvTopes.DataSource = lstTopes;
        gvTopes.DataBind();
        Session["RangoCDAT"] = lstTopes;
    }

    protected List<RangoCDAT> ObtenerListaTopes()
    {
        Boolean continuar = true;
        List<RangoCDAT> lstTopes = new List<RangoCDAT>();
        List<RangoCDAT> lista = new List<RangoCDAT>();

        foreach (GridViewRow rfila in gvTopes.Rows)
        {
            RangoCDAT etope = new RangoCDAT();
            etope.cod_lineacdat = this.lblConsecutivo.Text;

            Label lblcodrango = (Label)rfila.FindControl("lblcodrango");
            if (lblcodrango.Text != "")
                etope.cod_rango = Convert.ToInt64(lblcodrango.Text);

            DropDownListGrid ddlTipoTope = (DropDownListGrid)rfila.FindControl("ddlTipoTope");
            if (ddlTipoTope.SelectedValue != null || ddlTipoTope.SelectedIndex != 0)
                etope.tipo_tope = Convert.ToInt32(ddlTipoTope.SelectedValue);

            TextBox txtminimo = (TextBox)rfila.FindControl("txtminimo");
            if (txtminimo.Text != "")
                etope.minimo = Convert.ToString(txtminimo.Text);
            else
                etope.minimo = null;

            TextBox txtmaximo = (TextBox)rfila.FindControl("txtmaximo");
            if (txtmaximo.Text != "")
                etope.maximo = Convert.ToString(txtmaximo.Text);
            else
                etope.maximo = null;

            lista.Add(etope);
            //if (etope.minimo == null && etope.maximo == null)
            //{
            //    VerError("Debe ingresar un tope minimo y maximo");
            //    continuar = false;
            //}
            //if (etope.tipo_tope != 1)
            //{
            //    VerError("Debe ingresar un valor para manejo de plazos");
            //    continuar = false;
            //}
            //if (etope.tipo_tope != 2)
            //{
            //    VerError("Debe ingresar un valor para manejo de montos");
            //    continuar = false;
            //}
            if (continuar == true)
            {
                if (etope.minimo != "" && etope.minimo != null || etope.maximo != "" && etope.maximo != null && etope.tipo_tope != 0)
                    lstTopes.Add(etope);
            }
        }

        Session["RangoCDAT"] = lista;
        return lstTopes;
    }

    protected void gvTopes_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //LineasCreditoService Topesservicio = new LineasCreditoService();

            DropDownList ddlTipoTope = (DropDownList)e.Row.FindControl("ddlTipoTope");
            Label lblTipoTope = (Label)e.Row.FindControl("lblTipoTope");
            if (lblTipoTope.Text != "")
            {
                if (ddlTipoTope != null)
                    ddlTipoTope.SelectedValue = lblTipoTope.Text;
            }
            else
            {
                if (ddlTipoTope != null)
                {
                    if (e.Row.RowIndex % 2 == 0)
                    {
                        ddlTipoTope.SelectedIndex = 0;
                    }
                    else
                    {

                        ddlTipoTope.SelectedIndex = 1;
                    }

                }

            }


        }
    }


    protected void gvTopes_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Int64 conseID = Convert.ToInt64(gvTopes.DataKeys[e.RowIndex].Values[0].ToString());

        // Obtener listado de topes
        ObtenerListaTopes();
        List<RangoCDAT> LstTopes;
        LstTopes = (List<RangoCDAT>)Session["RangoCDAT"];

        // Borrar el tope del listado y si existe en la base de datos borrarlo
        try
        {
            foreach (RangoCDAT topes in LstTopes)
            {
                if (Convert.ToInt64(topes.cod_rango) == conseID)
                {
                    lineaCDATServicio.EliminarRangoCDAT(Convert.ToInt64(topes.cod_rango), (Usuario)Session["usuario"]);
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

        Session["RangoCDAT"] = LstTopes;
    }

}