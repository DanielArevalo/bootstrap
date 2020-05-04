using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Web;
using System.Web.UI.WebControls;
using Xpinn.Util;
using System.Web.UI.HtmlControls;

using Xpinn.FabricaCreditos.Entities;
using Xpinn.FabricaCreditos.Services;
using System.Linq;
using System.IO;
using System.Xml;
using System.Design;
using System.Threading;
using Xpinn.Asesores.Services;
using Xpinn.Asesores.Entities;
using System.Configuration;
using System.Globalization;
using System.Web.UI;
using Xpinn.Cartera.Services;
using Xpinn.Cartera.Entities;
using System.Text;


partial class Nuevo : GlobalWeb
{
    private Xpinn.Cartera.Services.DebitoAutomaticoService DebitoAutomaticoService = new Xpinn.Cartera.Services.DebitoAutomaticoService();

    DebitoAutomaticoService DebitoAutomaticoServicio = new DebitoAutomaticoService();

    /// <summary>
    /// Mostrar la barra de herramientas al ingresar a la funcionalidad
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {

            if (Session[DebitoAutomaticoServicio.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(DebitoAutomaticoServicio.CodigoPrograma, "E");
            else
                VisualizarOpciones(DebitoAutomaticoServicio.CodigoPrograma, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoCancelar += btnCancelar_Click;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.MostrarGuardar(true);
            toolBar.MostrarExportar(true);
            ctlMensaje.eventoClick += btnContinuarMen_Click;
            ctlMensajeBorrar.eventoClick += CtlMensajeBorrar_eventoClick;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(DebitoAutomaticoServicio.CodigoPrograma, "Page_PreInit", ex);
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
            mvPrincipal.ActiveViewIndex = 0;
            if (!IsPostBack)
            {
                if (Session[DebitoAutomaticoServicio.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[DebitoAutomaticoServicio.CodigoPrograma + ".id"].ToString();
                    Session.Remove(DebitoAutomaticoServicio.CodigoPrograma + ".id");                    
                    Session.Remove("num_cuenta");
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
            BOexcepcion.Throw(DebitoAutomaticoServicio.CodigoPrograma, "Page_Load", ex);
        }
    }





    protected void txtFechaCondonacion_eventoCambiar(object sender, EventArgs e)
    {
        ObtenerDatos(idObjeto);
        RegistrarPostBack();
    }



    /// <summary>
    /// Evento para cancelar
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }
    protected void cbSeleccionarEncabezado_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox cbSeleccionarEncabezado = (CheckBox)sender;
        if (cbSeleccionarEncabezado != null)
        {
            foreach (GridViewRow rFila in gvLista.Rows)
            {
                CheckBox cbSeleccionar = (CheckBox)rFila.FindControl("cbSeleccionar");
                cbSeleccionar.Checked = cbSeleccionarEncabezado.Checked;
            }
        }
    }


    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        if (ValidarCampos())
        {
            ctlMensaje.MostrarMensaje("Desea guardar los datos?");

        }



    }

    bool ValidarCampos()
    {
        Page.Validate();
        if (!Page.IsValid ||
            string.IsNullOrWhiteSpace(ddlCuentaAhorros.SelectedItem.Text))
        {
            VerError("La Cuenta de Ahorros  esta vacia");
        }
        return true;
    }



    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {


        try
        {

            DebitoAutomatico pDebito = new DebitoAutomatico();
            pDebito.lstLista = ObtenerListaGrilla2();


            if (pDebito.lstLista != null)
            {
                if (pDebito.lstLista.Count > 0)
                {

                    foreach (var elemento in pDebito.lstLista)
                    {
                        pDebito.consecutivo = Convert.ToInt64(elemento.consecutivo);

                        pDebito.cod_persona = Convert.ToInt64(elemento.cod_persona);
                        pDebito.cod_tipo_producto = Convert.ToInt64(elemento.cod_tipo_producto);
                        pDebito.cod_linea = Convert.ToInt64(elemento.cod_linea);
                        pDebito.numero_producto = Convert.ToInt64(elemento.numero_producto);
                        pDebito.numero_cuenta_ahorro = Convert.ToString(ddlCuentaAhorros.SelectedItem.Text);

                        if (pDebito.consecutivo==0)
                        { 
                        DebitoAutomaticoService.CrearDebitoAutomatico(pDebito, Usuario);
                        }


                        if (pDebito.consecutivo >0)
                        {
                            pDebito.numero_cuenta_ahorro = Convert.ToString(ddlCuentaAhorros.SelectedValue);
                            pDebito.consecutivo= Convert.ToInt64(elemento.consecutivo);
                            DebitoAutomaticoService.ModificarDebitoAutomatico(pDebito, Usuario);
                        }
                        

                        Site toolBar = (Site)Master;
                        toolBar.MostrarExportar(false);
                        toolBar.MostrarGuardar(false);
                        toolBar.MostrarCancelar(true);
                        mvPrincipal.ActiveViewIndex = 1;
                    }
                }
            }

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(DebitoAutomaticoService.CodigoPrograma, "btnContinuarMen_Click", ex);
        }
    }

    protected void btnRegresar_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }

    protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

        long idBorrar = Convert.ToInt64(e.Keys[0]);
        ViewState.Add("idBorrar", idBorrar);
        ctlMensajeBorrar.MostrarMensaje("Seguro que deseas eliminar esta registro?");


       
    }



    void CtlMensajeBorrar_eventoClick(object sender, EventArgs e)
    {
        if (ViewState["idBorrar"] != null)
        {

            DebitoAutomatico Entitie = new DebitoAutomatico();
            Entitie.consecutivo = Convert.ToInt64(ViewState["idBorrar"]);

            try
            {
                DebitoAutomaticoServicio.EliminarDebitoAutomatico(Entitie.consecutivo, (Usuario)Session["usuario"]);
                idObjeto = txtCodPersona.Text;

                ObtenerDatos(idObjeto);
            }
            catch (Xpinn.Util.ExceptionBusiness ex)
            {
                VerError(ex.Message);
            }
            catch (Exception ex)
            {
                BOexcepcion.Throw(DebitoAutomaticoService.CodigoPrograma, "gvLista_RowDeleting", ex);
            }

            
        }
    }

    protected void btnExportar_Click(object sender, EventArgs e)
    {
        try
        {
            VerError("");
            if (gvLista.Rows.Count > 0 && ViewState["DTDebitoAutomatico"] != null)
            {
                StringBuilder sb = new StringBuilder();
                StringWriter sw = new StringWriter(sb);
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                Page pagina = new Page();
                dynamic form = new HtmlForm();
                gvLista.AllowPaging = false;
                gvLista.DataSource = ViewState["DTDebitoAutomatico"];
                gvLista.DataBind();
                gvLista.EnableViewState = false;
                pagina.EnableEventValidation = false;
                pagina.DesignerInitialize();
                pagina.Controls.Add(form);
                form.Controls.Add(gvLista);
                pagina.RenderControl(htw);
                Response.Clear();
                Response.Buffer = true;
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("Content-Disposition", "attachment;filename=ProductosDebitoAutomatico.xls");
                Response.Charset = "UTF-8";
                Response.ContentEncoding = Encoding.Default;
                Response.Write(sb.ToString());
                Response.End();
                gvLista.AllowPaging = true;
                gvLista.DataBind();
            }
            else
            {
                VerError("No existen Datos");
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
    }



    private List<DebitoAutomatico> ObtenerListaGrilla2()
    {
        List<DebitoAutomatico> lstLista = new List<DebitoAutomatico>();

        foreach (GridViewRow rFila in gvLista.Rows)
        {

            DebitoAutomatico vData = new DebitoAutomatico();

            if (rFila.Cells[2].Text != "" && rFila.Cells[2].Text != "&nbsp;")//CONSECUTIVO
                vData.consecutivo = Convert.ToInt64(rFila.Cells[2].Text);
            else
                vData.consecutivo = 0;


            if (rFila.Cells[3].Text != "" && rFila.Cells[3].Text != "&nbsp;")//CODIGO DE LA PERSONA 
                vData.cod_persona = Convert.ToInt64(rFila.Cells[3].Text);
            else
                vData.cod_persona = 0;

            if (rFila.Cells[5].Text != "" && rFila.Cells[5].Text != "&nbsp;")//CODIGO DEL TIPO DE PRODUCTO 
                vData.cod_tipo_producto = Convert.ToInt64(rFila.Cells[5].Text);

            if (rFila.Cells[6].Text != "" && rFila.Cells[6].Text != "&nbsp;")//CODIGO DE LA LINEA DEL PRODUCTO 
                vData.cod_linea = Convert.ToInt64(rFila.Cells[6].Text);

            if (rFila.Cells[8].Text != "" && rFila.Cells[8].Text != "&nbsp;")//NUMERO DEL PRODUCTO 
                vData.numero_producto = Convert.ToInt64(rFila.Cells[8].Text);



            CheckBox check = (CheckBox)rFila.FindControl("cbSeleccionar");
            if (check != null)
            {
                if (check.Checked == true)
               {
                    if (vData.cod_persona != 0 && vData.cod_tipo_producto != 0 && vData.cod_linea != 0 && vData.numero_producto != 0)
                    {
                        lstLista.Add(vData);
                    }
                }
            }
        }
        return lstLista;
    }



    /// <summary>
    /// Mostrar los datos del crédito
    /// </summary>
    /// <param name="pIdObjeto"></param>
    protected void ObtenerDatos(String pIdObjeto)
    {
        Session.Remove("num_cuenta");
        List<DebitoAutomatico> lstConsulta = new List<DebitoAutomatico>();
        List<DebitoAutomatico> lstConsultaAhorros = new List<DebitoAutomatico>();

        Int64 credito = 0;
        try
        {
            DebitoAutomatico vDebitoAutomatico = new DebitoAutomatico();
            vDebitoAutomatico = DebitoAutomaticoServicio.ConsultarDatosCliente(Convert.ToString(pIdObjeto), (Usuario)Session["usuario"]);


            // Si existe algo en la consulta trayendo datos  de la tabla productos_debito_automatico, entonces deja por defecto la cuenta de ahorros grabada  
            if (vDebitoAutomatico.descripcion != null)
            {

                Session["num_cuenta"] = vDebitoAutomatico.descripcion;
              
                //Cargar los  numeros de productos de ahorro a la vista del cliente  
                lstConsultaAhorros = DebitoAutomaticoServicio.ListarProductosAhorrosClientes(vDebitoAutomatico, (Usuario)Session["usuario"]);
                if (lstConsultaAhorros != null)
                {
                    ddlCuentaAhorros.DataSource = lstConsultaAhorros;
                    ddlCuentaAhorros.DataTextField = "descripcion";
                    ddlCuentaAhorros.DataValueField = "descripcion";
                    ddlCuentaAhorros.DataBind();
                }

                lstConsulta = DebitoAutomaticoServicio.ListarProductosClientesDebAutomatico(vDebitoAutomatico, (Usuario)Session["usuario"]);

            }

            // Si no tiene datos en la tbala productos_debito_automatico 
            if (vDebitoAutomatico.descripcion == null)
            {
                //Cargar los  numeros de productos de ahorro a la vista del cliente  
                lstConsultaAhorros = DebitoAutomaticoServicio.ListarProductosAhorrosClientes(vDebitoAutomatico, (Usuario)Session["usuario"]);
                if (lstConsultaAhorros != null)
                {
                    ddlCuentaAhorros.DataSource = lstConsultaAhorros;
                    ddlCuentaAhorros.DataTextField = "descripcion";
                    ddlCuentaAhorros.DataValueField = "descripcion";
                    ddlCuentaAhorros.DataBind();
                }


                lstConsulta = DebitoAutomaticoServicio.ListarProductosClientes(vDebitoAutomatico, (Usuario)Session["usuario"]);
            }


            if (vDebitoAutomatico.identificacion.ToString() != "")
                txtIdentificacion.Text = vDebitoAutomatico.identificacion.ToString().Trim();

            if (vDebitoAutomatico.cod_persona.ToString() != "")
                txtCodPersona.Text = HttpUtility.HtmlDecode(vDebitoAutomatico.cod_persona.ToString().Trim());

            if (!string.IsNullOrEmpty(vDebitoAutomatico.tipoidentificacion))
                txtTipo_identificacion.Text = HttpUtility.HtmlDecode(vDebitoAutomatico.tipoidentificacion.ToString().Trim());
            if (!string.IsNullOrEmpty(vDebitoAutomatico.Nombres))
                txtNombre.Text = HttpUtility.HtmlDecode(vDebitoAutomatico.Nombres.ToString().Trim());



            gvLista.PageSize = 15;
            String emptyQuery = "Fila de datos vacia";
            gvLista.EmptyDataText = emptyQuery;
            gvLista.DataSource = lstConsulta;

            if (lstConsulta.Count > 0)
            {
                Site toolBar = (Site)this.Master;
                toolBar.MostrarExportar(true);
                gvLista.Visible = true;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                gvLista.DataBind();
                ValidarPermisosGrilla(gvLista);
                ViewState.Add("DTDebitoAutomatico", lstConsulta);


            }
            else
            {
                gvLista.Visible = false;
                //lblTotalRegs.Visible = false;
            }



        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(DebitoAutomaticoServicio.CodigoPrograma, "ObtenerDatos", ex);
        }
    }







    protected void gvLista_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            CheckBox cbSeleccionar = (CheckBox)e.Row.FindControl("cbSeleccionar");
            String consecutivo = "0";
            if (e.Row.Cells[2].Text != consecutivo)
            {
                e.Row.BackColor = System.Drawing.Color.Aquamarine;
                cbSeleccionar.Checked = true;

            }
        }
    }

}