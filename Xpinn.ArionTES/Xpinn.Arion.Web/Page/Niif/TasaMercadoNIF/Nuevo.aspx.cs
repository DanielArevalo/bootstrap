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
using System.Text;
using System.IO;
using System.Web.UI.HtmlControls;
using System.Drawing;
using Xpinn.NIIF.Entities;

partial class Nuevo : GlobalWeb
{
    private Xpinn.NIIF.Services.TasaMercadoNIFService tasaMercado = new Xpinn.NIIF.Services.TasaMercadoNIFService();

    private List<int> mQuantities = new List<int>();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            VisualizarOpciones(tasaMercado.CodigoProgramaoriginal, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoCancelar += btnCancelar_Click;
            toolBar.eventoGuardar += btnGuardar_Click;
            ctlMensaje.eventoClick += btnContinuarMen_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(tasaMercado.CodigoProgramaoriginal, "Page_PreInit", ex);
        }
    }



    protected void Page_Load(object sender, EventArgs e)
    {
       try
        {
            if (!IsPostBack)
            {
                txtCodigo.Text = "";
                
                if (Session[tasaMercado.CodigoProgramaoriginal + ".id"] != null)
                {
                    Session["Operacion"] = "2";
                    idObjeto = Session[tasaMercado.CodigoProgramaoriginal + ".id"].ToString();
                    Session.Remove(tasaMercado.CodigoProgramaoriginal + ".id");

                    ObtenerDatos();
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
            BOexcepcion.Throw(tasaMercado.CodigoProgramaoriginal, "Page_Load", ex);
        }
    }


    protected void ObtenerDatos()
    {
        txtCodigo.Text = Session["CODIGO"].ToString();
        txtFechaIni.Text = Session["FECHAINI"].ToString();
        txtFechaFin.Text = Session["FECHAFIN"].ToString();
        txtTasa.Text = Session["TASA"].ToString();
        txtObser.Text = Session["OBSERVACIONES"].ToString();

        List<TasaMercadoCondicionNIF> lstTasaCondi = new List<TasaMercadoCondicionNIF>();

        lstTasaCondi = tasaMercado.FiltrarDatosTasaCondicion(Convert.ToInt32(txtCodigo.Text), (Usuario)Session["usuario"]);
        if (lstTasaCondi.Count > 0)
        {
            if ((lstTasaCondi != null) || (lstTasaCondi.Count != 0))
            {
                ValidarPermisosGrilla(gvLista);
                gvLista.DataSource = lstTasaCondi;
                gvLista.DataBind();
            }
            Session["Datos"] = lstTasaCondi;
        }
        else 
        {
            InicializarListado();
        }
    }



    protected List<TasaMercadoCondicionNIF> ObtenerListaTasaCondicion()
    {
        List<TasaMercadoCondicionNIF> lstTasaCondicion = new List<TasaMercadoCondicionNIF>();
        List<TasaMercadoCondicionNIF> lista = new List<TasaMercadoCondicionNIF>();

        foreach (GridViewRow rfila in gvLista.Rows)
        {
            TasaMercadoCondicionNIF eTasa = new TasaMercadoCondicionNIF();

            Label lblcodigo = (Label)rfila.FindControl("lblcodigo");
            if (lblcodigo != null)
                eTasa.cod_tasa_condicion = Convert.ToInt32(lblcodigo.Text);

            DropDownListGrid ddlCondicion = (DropDownListGrid)rfila.FindControl("ddlCondicion");
            if (ddlCondicion.SelectedValue != null)
                eTasa.variable = Convert.ToInt32(ddlCondicion.SelectedValue);

            DropDownListGrid ddlOperador = (DropDownListGrid)rfila.FindControl("ddlOperador");
            if (ddlOperador.SelectedValue != null)
                eTasa.operador = ddlOperador.SelectedValue;

            TextBox txtValor = (TextBox)rfila.FindControl("txtValor");
            if (txtValor != null)
                eTasa.valor = txtValor.Text;

            lista.Add(eTasa);
            Session["Datos"] = lista;

            if (eTasa.variable != 0 && eTasa.operador != null && eTasa.valor != null)
            {
                lstTasaCondicion.Add(eTasa);
            }
        }
        return lstTasaCondicion;
    }



    protected void InicializarListado()
    {
        List<TasaMercadoCondicionNIF> lstTasa = new List<TasaMercadoCondicionNIF>();
        for (int i = gvLista.Rows.Count; i < 5; i++)
        {
            TasaMercadoCondicionNIF pDetalle = new TasaMercadoCondicionNIF();
            pDetalle.cod_tasa_condicion = -1;
            pDetalle.variable = null;
            pDetalle.operador = "";
            pDetalle.valor = "";
            lstTasa.Add(pDetalle);
        }
        gvLista.DataSource = lstTasa;
        gvLista.DataBind();
        Session["Datos"] = lstTasa;
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        string mensaje = "";
        mensaje = Session["Operacion"] == "2"? " actualización?": " grabación?";
       
        ctlMensaje.MostrarMensaje("Desea realizar la"+ mensaje);
    }


    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        try
        {
            TasaMercadoNIF pTasaMercado = new TasaMercadoNIF();
            if (txtTasa.Text != "" || txtObser.Text != "")
            {
                if (txtFechaIni.ToDateTime < txtFechaFin.ToDateTime)
                {
                    TasaMercadoNIF vTasaMerc = new TasaMercadoNIF();
                    if (txtCodigo.Text != "")
                        vTasaMerc.cod_tasa_mercado = Convert.ToInt32(txtCodigo.Text);
                    else
                        vTasaMerc.cod_tasa_mercado= 0;
                    vTasaMerc.fecha_inicial = txtFechaIni.ToDateTime;
                    vTasaMerc.fecha_final = txtFechaFin.ToDateTime;
                    vTasaMerc.tasa = Convert.ToDecimal(txtTasa.Text);
                    vTasaMerc.tipo_tasa = 1;
                    vTasaMerc.observaciones = txtObser.Text.ToUpper();

                    vTasaMerc.lstTasaCondi = new List<TasaMercadoCondicionNIF>();
                    vTasaMerc.lstTasaCondi = ObtenerListaTasaCondicion();
                   
                    if (txtCodigo.Text == "")
                    {
                        tasaMercado.CrearTasaMercadoNIIF(vTasaMerc, (Usuario)Session["usuario"]);
                    }
                    else
                    {
                        tasaMercado.ModificarTasaMercadoNIIF(vTasaMerc, (Usuario)Session["usuario"]);
                    }
                    Navegar(Pagina.Lista);
                }
                else
                {
                    VerError("La fecha Final no puede ser menor o igual a la Fecha inicial");
                }
            }
            else
            {
                VerError("Ingrese los datos Faltantes");
            }

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(tasaMercado.CodigoProgramaoriginal, "btnGuardar_Click", ex);
        }
    }



    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }

    



    protected void gvLista_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownList ddlCondicion = (DropDownList)e.Row.FindControl("ddlCondicion");
            DropDownList ddlOperador = (DropDownList)e.Row.FindControl("ddlOperador");

            if (ddlCondicion != null)
            {
                TasaMercadoNIF vTasa = new TasaMercadoNIF();
                List<TasaMercadoNIF> lstTasa = new List<TasaMercadoNIF>();
                lstTasa = tasaMercado.DatosCondicionNIIF(vTasa, (Usuario)Session["Usuario"]);
                ddlCondicion.DataSource = lstTasa;
                ddlCondicion.DataTextField = "nombre";
                ddlCondicion.DataValueField = "idvariable";
                ddlCondicion.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));
                ddlCondicion.SelectedIndex = 0;
                ddlCondicion.DataBind();
            }

            if (ddlOperador != null)
            {
                ddlOperador.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));
                ddlOperador.Items.Insert(1, new ListItem("IGUAL", "1"));
                ddlOperador.Items.Insert(2, new ListItem("MAYOR", "2"));
                ddlOperador.Items.Insert(3, new ListItem("MENOR", "3"));
                ddlOperador.Items.Insert(4, new ListItem("MAYOR o IGUAL", "4"));
                ddlOperador.Items.Insert(5, new ListItem("MENOR o IGUAL", "5"));
                ddlOperador.Items.Insert(6, new ListItem("DIFERENTE", "6"));
                ddlOperador.Items.Insert(7, new ListItem("COMIENZA POR", "7"));
                ddlOperador.Items.Insert(8, new ListItem("CONTIENE", "8"));
                ddlOperador.SelectedIndex = 0;
                ddlOperador.DataBind();
            }

            Label lblCondicion = (Label)e.Row.FindControl("lblCondicion");
            if (lblCondicion != null)
                ddlCondicion.SelectedValue = lblCondicion.Text;

            Label lblOperador = (Label)e.Row.FindControl("lblOperador");
            if (lblOperador != null)
                ddlOperador.SelectedValue = lblOperador.Text;

        }


    }


    protected void btnDetalle_Click(object sender, EventArgs e)
    {
        ObtenerListaTasaCondicion();
        List<TasaMercadoCondicionNIF> lstTasaCondi = new List<TasaMercadoCondicionNIF>();
        
        if (Session["Datos"] != null)
        {
            lstTasaCondi = (List<TasaMercadoCondicionNIF>)Session["Datos"];

            for (int i = 1; i <= 1; i++)
            {
                TasaMercadoCondicionNIF pDetalle = new TasaMercadoCondicionNIF();
                pDetalle.cod_tasa_condicion = -1;
                pDetalle.variable = null;
                pDetalle.operador = "";
                pDetalle.valor = "";
                lstTasaCondi.Add(pDetalle);
            }
            gvLista.PageIndex = gvLista.PageCount;
            gvLista.DataSource = lstTasaCondi;
            gvLista.DataBind();

            Session["Datos"] = lstTasaCondi;
        }
    }



    protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int conseID = Convert.ToInt32(gvLista.DataKeys[e.RowIndex].Values[0].ToString());

        ObtenerListaTasaCondicion();

        List<TasaMercadoCondicionNIF> LstActi;
        LstActi = (List<TasaMercadoCondicionNIF>)Session["Datos"];

        if (conseID > 0)
        {
            try
            {
                foreach (TasaMercadoCondicionNIF acti in LstActi)
                {
                    if (acti.cod_tasa_condicion == conseID)
                    {
                        tasaMercado.EliminarTasaCondicionNIIF(acti, (Usuario)Session["usuario"]);
                        LstActi.Remove(acti);
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
            foreach (TasaMercadoCondicionNIF acti in LstActi)
            {
                if (acti.cod_tasa_condicion == conseID)
                {
                    LstActi.Remove(acti);
                    break;
                }
            }
        }
        gvLista.DataSourceID = null;
        gvLista.DataBind();

        gvLista.DataSource = LstActi;
        gvLista.DataBind();

        Session["Datos"] = LstActi;

    }
}
