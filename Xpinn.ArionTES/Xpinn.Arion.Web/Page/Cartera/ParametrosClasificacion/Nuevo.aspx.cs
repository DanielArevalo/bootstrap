using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.SessionState;
using Xpinn.Util;
using Microsoft.Reporting.WebForms;
using Xpinn.FabricaCreditos.Entities;
using Xpinn.FabricaCreditos.Services;
using System.Drawing;
using System.Net;
using System.Text;
using System.IO;
using System.Xml;
using System.Design;
using System.Threading;
using Xpinn.Cartera.Services;
using Xpinn.Cartera.Entities;
using System.Globalization;
using Subgurim.Controles;
using System.Web.UI.HtmlControls;

partial class Nuevo : GlobalWeb
{
    private Xpinn.Cartera.Services.ClasificacionCarteraService ClasificacioncarteraServicio = new Xpinn.Cartera.Services.ClasificacionCarteraService();
    
     ///<summary>
    /// Mostrar la barra de herramientas al ingresar a la funcionalidad
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[ClasificacioncarteraServicio.CodigoProgramaParametros + ".id"] != null)
                VisualizarOpciones(ClasificacioncarteraServicio.CodigoProgramaParametros, "E");
            else
                VisualizarOpciones(ClasificacioncarteraServicio.CodigoProgramaParametros, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;            
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ClasificacioncarteraServicio.CodigoProgramaParametros, "Page_PreInit", ex);
        }
    }

    /// <summary>
    /// Cargar datos de la funcionalidad
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Session[ClasificacioncarteraServicio.CodigoProgramaParametros + ".id"] != null)
                {
                    Session["categoria"] = null;
                    idObjeto = Session[ClasificacioncarteraServicio.CodigoProgramaParametros + ".id"].ToString();
                    Session.Remove(ClasificacioncarteraServicio.CodigoProgramaParametros + ".id");
                    ObtenerDatos(idObjeto);
                    Actualizar();
                }
                else
                {
                    ObtenerDatos(idObjeto);
                }
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ClasificacioncarteraServicio.CodigoProgramaParametros, "Page_Load", ex);
        }
    }

    /// <summary>
    ///  Evento para guardar 
    /// </summary>
    protected void Guardar()
    {                                    
        try
        {               
            //Validate("vgActividadReg");
            if (Page.IsValid)
            {
                Usuario usuap = (Usuario)Session["usuario"];
                int cod = Convert.ToInt32(usuap.codusuario);                     
                ClasificacionCartera vclasificacion = new ClasificacionCartera();
                vclasificacion.rango=Convert.ToInt64(Session["rango"]);
                vclasificacion.clasifica = Convert.ToInt64(idObjeto);
                vclasificacion.categoria= Convert.ToString(Session["categoria"]);
                vclasificacion.codigo = Convert.ToInt64(idObjeto);
                vclasificacion.diasminimo = Convert.ToInt64(txtdiasminimo.Text);
                vclasificacion.diasmaximo = Convert.ToInt64(txtdiasmaximo.Text);
                vclasificacion.tipo_provision= Convert.ToInt64(Ddlprovision.SelectedValue);
                vclasificacion.por_provision= Convert.ToInt64(porce_provision.Text);
                if (Chkcausa.Checked)
                {
                    vclasificacion.causa = 1;
                }
                else
                {
                    vclasificacion.causa = 0;
                }
                ClasificacioncarteraServicio.CrearCategorias(vclasificacion, (Usuario)Session["usuario"]);                 
            }
            btnConsultar_Click(null, null);
        }          
        catch (Exception ex)
        {
            BOexcepcion.Throw(ClasificacioncarteraServicio.CodigoProgramaParametros, "ObtenerDatos", ex);
        }         
        

    }
    /// <summary>
    ///  Evento para guardar 
    /// </summary>
    protected void Modificar()
    {
        try
        {            
            if (Page.IsValid)
            {
                Usuario usuap = (Usuario)Session["usuario"];
                int cod = Convert.ToInt32(usuap.codusuario);
                ClasificacionCartera vclasificacion = new ClasificacionCartera();
                vclasificacion.rango = Convert.ToInt64(Session["rango"]);
                vclasificacion.clasifica = Convert.ToInt64(idObjeto);
                vclasificacion.categoria = Convert.ToString(Session["categoria"]);
                vclasificacion.codigo = Convert.ToInt64(idObjeto);
                vclasificacion.diasminimo = Convert.ToInt64(txtdiasminimo.Text);
                vclasificacion.diasmaximo = Convert.ToInt64(txtdiasmaximo.Text);
                vclasificacion.tipo_provision = Convert.ToInt64(Ddlprovision.SelectedValue);
                vclasificacion.por_provision = Convert.ToInt64(porce_provision.Text);
                if (Chkcausa.Checked)
                {
                    vclasificacion.causa = 1;
                }
                else
                {
                    vclasificacion.causa = 0;

                }
                ClasificacioncarteraServicio.ModificarCategorias(vclasificacion, (Usuario)Session["usuario"]);
            }
            btnConsultar_Click(null, null);
        }

        catch (Exception ex)
        {
            BOexcepcion.Throw(ClasificacioncarteraServicio.CodigoProgramaParametros, "ObtenerDatos", ex);
        }


    }
    
    
    /// <summary>
    /// Evento para consultar los datos
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        Actualizar();               
    }

    // <summary>
    /// Evento para guardar los datos
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        Guardar();               
    }
         
    /// <summary>
    /// Evento para cancelar
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        if (idObjeto == "")
        {
            Navegar(Pagina.Lista);
        }
        else
        {
            Session[ClasificacioncarteraServicio.CodigoPrograma + ".id"] = idObjeto;
            Navegar(Pagina.Detalle);
        }
    }

    protected void btnEditar_Click(object sender, ImageClickEventArgs e)
    {       
      //  Navegar(Pagina.Nuevo);
    }

    /// <summary>
    /// Mostrar los datos de la categoria de clasificacin escogida
    /// </summary>
    /// <param name="pIdObjeto"></param>
    protected void ObtenerDatos(String pIdObjeto)
    {        
        try
        {
            ClasificacionCartera vclasificacion = new ClasificacionCartera();

            if (pIdObjeto != null)
            {
                vclasificacion.codigo = Int32.Parse(pIdObjeto);
                vclasificacion = ClasificacioncarteraServicio.ConsultarClasificacion(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);

                if (!string.IsNullOrEmpty(vclasificacion.codigo.ToString()))
                {
                    txtdescripcion.Text = vclasificacion.descripcion.ToString().Trim();
                    txtcodigo.Text = vclasificacion.codigo.ToString().Trim();
                }                               
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ClasificacioncarteraServicio.CodigoProgramaParametros, "ObtenerDatos", ex);
        }
    }

    /// <summary>
    /// Mostrar los datos de la categoria de clasificacin escogida
    /// </summary>
    /// <param name="pIdObjeto"></param>
    protected void ObtenerDatosCategorias(String pIdObjeto)
    {

        try
        {
            ClasificacionCartera vclasificacion = new ClasificacionCartera();

            if (pIdObjeto != null)
            {
                vclasificacion.codigo = Int32.Parse(pIdObjeto);
                vclasificacion = ClasificacioncarteraServicio.ConsultarDiasCategoria(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);

                if (!string.IsNullOrEmpty(vclasificacion.codigo.ToString()))
                {
                    txtdiasminimo.Text = vclasificacion.diasminimo.ToString().Trim();
                    txtdiasmaximo.Text = vclasificacion.diasmaximo.ToString().Trim();
                    Ddlprovision.SelectedValue = vclasificacion.tipo_provision.ToString();
                    porce_provision.Text = vclasificacion.por_provision.ToString().Trim();
                    Chkcausa.Checked = Convert.ToBoolean(vclasificacion.causa);
                }
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ClasificacioncarteraServicio.CodigoProgramaParametros, "ObtenerDatos", ex);
        }
    }

/// <summary>
    /// Esto es para actualizar la grilla
    /// </summary>
    private void Actualizar()
    {
        string idObjeto;
        idObjeto = txtcodigo.Text;

        try
        {
            List<Xpinn.Cartera.Entities.ClasificacionCartera> lstConsulta = new List<Xpinn.Cartera.Entities.ClasificacionCartera>();
            lstConsulta = ClasificacioncarteraServicio.ListarDiasCategoria(Convert.ToInt64(idObjeto), (Usuario)Session["usuario"]);

            gvLista.PageSize = 15;
            String emptyQuery = "Fila de datos vacia";
            gvLista.EmptyDataText = emptyQuery;
            gvLista.DataSource = lstConsulta;

            if (lstConsulta.Count > 0)
            {
                gvLista.Visible = true;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                gvLista.DataBind();
                ValidarPermisosGrilla(gvLista);                
            }
            else
            {
                gvLista.Visible = false;
                lblTotalRegs.Visible = false;
            }
            ActualizarGarantias();
            Session.Add(ClasificacioncarteraServicio.CodigoProgramaParametros + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ClasificacioncarteraServicio.CodigoProgramaParametros, "Actualizar", ex);
        }
    }


    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {

        Validate("vgGuardar");
        if (Page.IsValid)
        {

            String id = gvLista.Rows[e.NewEditIndex].Cells[1].Text;
            Session["rango"] = id;
            String categoria = gvLista.Rows[e.NewEditIndex].Cells[2].Text;
            Session["categoria"] = categoria;
          
            GridViewRow row = gvLista.Rows[e.NewEditIndex];

            if (id == "0")
            {
                txtdiasminimo.Text = String.Empty;
                txtdiasmaximo.Text = String.Empty;
                Ddlprovision.Text = String.Empty;
                porce_provision.Text = "0";
                Chkcausa.Checked = false;
                mpeNuevo.Show();
            }
            if(id == "0")     
            {
            txtdiasminimo.Text = String.Empty;
            txtdiasmaximo.Text = String.Empty;
            Ddlprovision.Text = String.Empty;
            HiddenField1.Value = row.Cells[0].Text;
            HiddenField1.Value = row.Cells[1].Text;
            mpeNuevo.Show();
            btnGuardar.Visible = true;
            btnActualizar.Visible = false;
            e.Cancel = true;
            }
            else
            {
                e.Cancel = true;
            }
            if (id != "0")
            {
                try
                {
                    btnActualizar.Visible = true;
                    btnGuardar.Visible = false;
                    ClasificacionCartera vclasificacion = new ClasificacionCartera();
                    String pIdObjeto = id;
                    if (pIdObjeto != null)
                    {
                        vclasificacion.rango = Int32.Parse(pIdObjeto);
                        vclasificacion.rango = Convert.ToInt64(Session["rango"]);
                        vclasificacion = ClasificacioncarteraServicio.ConsultarDiasCategoria(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);
                        Ddlprovision.SelectedValue = "";
                        if (!string.IsNullOrEmpty(vclasificacion.codigo.ToString()))
                        {
                            mpeNuevo.Show();
                            vclasificacion.categoria = Convert.ToString(categoria);
                            txtdiasminimo.Text = vclasificacion.diasminimo.ToString().Trim();
                            txtdiasmaximo.Text = vclasificacion.diasmaximo.ToString().Trim();
                            porce_provision.Text = vclasificacion.por_provision.ToString().Trim();
                            Chkcausa.Checked = Convert.ToBoolean(vclasificacion.causa);
                            txt_categoria.Text = vclasificacion.categoria;
                            Ddlprovision.SelectedValue = vclasificacion.tipo_provision.ToString().Trim();                         
                        }
                    }
                }
                catch (Exception ex)
                {
                    BOexcepcion.Throw(ClasificacioncarteraServicio.CodigoPrograma, "ObtenerDatos", ex);
                }
            }
            
        }
    }

    
    protected void btnClose_Click(object sender, EventArgs e)
    {
        mpeNuevo.Hide();
        Actualizar();
    }


    protected void btnActualizar_Click(object sender, EventArgs e)
    {
        Modificar();
    }

    #region Efecto de las Garantías sobre la Provisión
    private void ActualizarGarantias()
    {
        string idObjeto;
        idObjeto = txtcodigo.Text;
        if (idObjeto.Trim() == "")
        {
            VerError("No hay código de clasificación");
            return;
        }
        try
        {
            List<Xpinn.Cartera.Entities.GarantiasClasificacion> lstConsulta = new List<Xpinn.Cartera.Entities.GarantiasClasificacion>();
            Xpinn.Cartera.Entities.GarantiasClasificacion garantias = new GarantiasClasificacion();
            garantias.cod_clasifica = Convert.ToInt32(idObjeto);
            lstConsulta = ClasificacioncarteraServicio.ListarGarantiasClasificacion(garantias, (Usuario)Session["usuario"]);
            String emptyQuery = "Fila de datos vacia";
            gvGarantias.EmptyDataText = emptyQuery;
            gvGarantias.DataSource = lstConsulta;
            if (lstConsulta.Count <= 0)
            {
                lstConsulta.Add(garantias);
            }
            gvGarantias.Visible = true;
            gvGarantias.DataBind();            
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ClasificacioncarteraServicio.CodigoProgramaParametros, "Actualizar", ex);
        }
    }

    protected void btnAgregar_Click(object sender, EventArgs e)
    {
        VerError("");
        lblMensaje.Text = "";
        txtDiasIni.Text = "";
        txtDiasFin.Text = "";
        txtPorcentaje.Text = "";
        ddlTipoGarantia.SelectedIndex = 0;
        mpeGarantias.Show();
    }

    protected void btnCloseReg_Click(object sender, EventArgs e)
    {
        mpeGarantias.Hide();
    }

    protected void btnGuardarReg_Click(object sender, EventArgs e)
    {
        lblMensaje.Text = "";
        // Validar los datos
        if (txtcodigo.Text.Trim() == "")
        {
            lblMensaje.Text = "Debe especificar el código de la clasificación";
            return;
        }
        if (txtDiasIni.Text.Trim() == "")
        {
            lblMensaje.Text = "Debe especificar el rango de meses en mora del crédito inicial";
            return;
        }
        if (txtDiasFin.Text.Trim() == "")
        {
            lblMensaje.Text = "Debe especificar el rango de meses en mora del crédito final";
            return;
        }
        if (ddlTipoGarantia.SelectedItem == null)
        {
            lblMensaje.Text = "Debe especificar el tipo de garantía";
            return; 
        }
        if (ddlTipoGarantia.SelectedItem.Value.Trim() == "")
        {
            lblMensaje.Text = "Debe especificar el tipo de garantía";
            return;
        }
        if (txtPorcentaje.Text.Trim() == "")
        {
            lblMensaje.Text = "Debe especificar el porcentaje";
            return;
        }
        // Grabar los datos
        Xpinn.Cartera.Entities.GarantiasClasificacion garantia = new GarantiasClasificacion();
        garantia.cod_clasifica = Convert.ToInt32(txtcodigo.Text);
        garantia.dias_inicial = Convert.ToInt32(ConvertirStringToInt(txtDiasIni.Text));
        garantia.dias_final = Convert.ToInt32(ConvertirStringToInt(txtDiasFin.Text));
        if (ddlTipoGarantia.SelectedItem != null)
            if (ddlTipoGarantia.SelectedItem.Value.Trim() != "")
                garantia.tipo_garantia = Convert.ToInt32(ConvertirStringToInt(ddlTipoGarantia.SelectedItem.Value));
        if (txtPorcentaje.Text.Trim() != "")
            garantia.porcentaje = ConvertirStringToDecimal(txtPorcentaje.Text);
        try
        {
            if (Session["categoria"] != null)
            {
                garantia.idgarantia = Convert.ToInt32(Session["categoria"]);
                garantia.cod_clasifica = Convert.ToInt32(Session["clasifica"]); 
                ClasificacioncarteraServicio.ModificarGarantiasClasificacion(garantia, (Usuario)Session["Usuario"]);
                Session["categoria"] = null;
                Session["clasifica"] = null;
            }
            else 
                ClasificacioncarteraServicio.CrearGarantiasClasificacion(garantia, (Usuario)Session["Usuario"]);
        }
        catch (Exception ex)
        {
            lblMensaje.Text = "Se presento error. Error: " + ex.Message;
        }

        mpeGarantias.Hide();
        ActualizarGarantias();
    }

    #endregion


    protected void gvGarantias_RowEditing(object sender, GridViewEditEventArgs e)
    {
        if (Page.IsValid)
        {
            string texto = gvGarantias.Rows[e.NewEditIndex].Cells[2].Text;
            String id = gvGarantias.Rows[e.NewEditIndex].Cells[1].Text;
            Session["categoria"] = id;
            String cod_categoria = ((Label)gvGarantias.Rows[e.NewEditIndex].FindControl("lblcod_clasifica")).Text;
            Session["clasifica"] = cod_categoria;
           
            if (id != "0")
            {
                try
                {
                    e.Cancel = true;
                    btnCloseReg.Visible = true;
                    btnGuardarReg.Visible = true;
                  
                    String pIdObjeto = id;
                    if (pIdObjeto != null)
                    {
                            mpeGarantias.Show();
                            txtDiasIni.Text = gvGarantias.Rows[e.NewEditIndex].Cells[3].Text;
                            txtDiasFin.Text= gvGarantias.Rows[e.NewEditIndex].Cells[4].Text;
                            txtPorcentaje.Text = gvGarantias.Rows[e.NewEditIndex].Cells[5].Text;
                            ddlTipoGarantia.SelectedValue = (from ListItem i in ddlTipoGarantia.Items
                                                             where i.Text == texto
                                                             select i.Value).ToList()[0];

                    }
                }
                catch (Exception ex)
                {
                    BOexcepcion.Throw(ClasificacioncarteraServicio.CodigoPrograma, "ObtenerDatos", ex);
                }
            }

        }
    }
}