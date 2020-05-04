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
using Xpinn.Reporteador.Services;
using Xpinn.Reporteador.Entities;
using System.Text;
using System.IO;
using System.Web.UI.HtmlControls;

partial class Nuevo : GlobalWeb
{

    private UIAFService ReporteService = new UIAFService();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[ReporteService.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(ReporteService.CodigoPrograma, "E");
            else
                VisualizarOpciones(ReporteService.CodigoPrograma, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoRegresar += btnRegresar_Click;            
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.MostrarGuardar(false);
            toolBar.eventoConsultar += btnConsultar_Click;
            ctlMensaje.eventoClick += btnContinuarMen_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ReporteService.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {           
            if (!IsPostBack)
            {
                mvCuentasxPagar.ActiveViewIndex = 0;
                txtIdReporte.Enabled = false;
               
                if (Session[ReporteService.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[ReporteService.CodigoPrograma + ".id"].ToString();
                    Session.Remove(ReporteService.CodigoPrograma + ".id");
                    ObtenerDatos(idObjeto);
                    txtFechaFin.Enabled = false;
                    txtFechaIni.Enabled = false;
                    txtIdReporte.Enabled = false;
                    gvProductos.Enabled = false;
                    Site toolBar = (Site)this.Master;
                    toolBar.MostrarGuardar(false);
                    toolBar.MostrarConsultar(false);

                    lblMsj.Text = " modificado ";
                }
                else
                {
                    lblMsj.Text = " grabado ";
                    txtFechaIni.Text = ""; txtFechaFin.Text = "";
                    //txtIdReporte.Text = ReporteService.ObtenerSiguienteCodigo((Usuario)Session["usuario"]).ToString();                   
                }               
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ReporteService.CodigoPrograma, "Page_Load", ex);
        }
    }


    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (ValidarDatos())
            {
                Actualizar();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ReporteService.GetType().Name + "L", "btnConsultar_Click", ex);
        }
    }


    private UIAFDetalle ObtenerValores()
    {
        UIAFDetalle vProducto = new UIAFDetalle();
        if (txtIdReporte.Text.Trim() != "")
            vProducto.idreporte = Convert.ToInt32(txtIdReporte.Text.Trim());
        
        return vProducto;
    }



    private string obtFiltro(UIAFDetalle Producto)
    {

        String filtro = String.Empty;
        if (txtIdReporte.Text.Trim() != "")
            filtro += " and idreporte = " + Producto.idreporte;
        //filtro += " and estado ='G'";

        return filtro;
    }


    private void Actualizar()
    {
        try
        {
            Configuracion conf = new Configuracion();
            List<UIAFDetalle> lstConsulta = new List<UIAFDetalle>();

            String filtro = obtFiltro(ObtenerValores());

            DateTime pFechaIni, pFechaFin;
            pFechaIni = txtFechaIni.ToDateTime == null ? DateTime.MinValue : txtFechaIni.ToDateTime;
            pFechaFin = txtFechaFin.ToDateTime == null ? DateTime.MinValue : txtFechaFin.ToDateTime;

            if (idObjeto == "")
            {
                if (chkFiltro.Checked == false)
                {
                    lstConsulta = ReporteService.ListarVistaProductos(filtro, pFechaIni, pFechaFin, (Usuario)Session["usuario"]);
                    if (lstConsulta.Count > 0)
                    {
                        gvProducto_vista.Visible = true;
                        gvProductos.Visible = false;
                        gvProducto_vista.DataSource = lstConsulta;
                        gvProducto_vista.DataBind();
                        lblInfo.Visible = false;
                        lblTotalRegs1.Visible = true;
                        Session["DTUIAF"] = lstConsulta;
                        Site toolBar = (Site)this.Master;
                        toolBar.MostrarGuardar(true);
                        lblTotalRegs1.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                    }
                    else
                    {
                        gvProducto_vista.Visible = false;
                        gvProductos.Visible = false;
                        lblInfo.Visible = true;
                        lblTotalRegs1.Visible = false;
                    }

                }
                else
                {
                    lstConsulta = ReporteService.ListarVistaProductosAll(filtro, (Usuario)Session["usuario"]);
                    if (lstConsulta.Count > 0)
                    {
                        gvProducto_vista.Visible = true;
                        gvProductos.Visible = false;
                        gvProducto_vista.DataSource = lstConsulta;
                        gvProducto_vista.DataBind();
                        lblInfo.Visible = false;
                        lblTotalRegs1.Visible = true;
                        Session["DTUIAF"] = lstConsulta;
                        Site toolBar = (Site)this.Master;
                        toolBar.MostrarGuardar(true);
                        lblTotalRegs1.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                    }
                    else
                    {
                        gvProducto_vista.Visible = false;
                        gvProductos.Visible = false;
                        lblInfo.Visible = true;
                        lblTotalRegs1.Visible = false;
                    }
                }


            }
            else
            {
                lstConsulta = ReporteService.ListarUIAFProductos(filtro, (Usuario)Session["usuario"]);
                if (lstConsulta.Count > 0)
                {
                    gvProductos.Visible = true;
                    gvProducto_vista.Visible = false;
                    gvProductos.DataSource = lstConsulta;
                    gvProductos.DataBind();
                    lblInfo.Visible = false;
                    lblTotalRegs1.Visible = true;
                    Session["DTUIAF"] = lstConsulta;
                    Site toolBar = (Site)this.Master;
                    toolBar.MostrarGuardar(true);
                    lblTotalRegs1.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                }
                else
                {
                    gvProducto_vista.Visible = false;
                    gvProductos.Visible = false;
                    lblInfo.Visible = true;
                    lblTotalRegs1.Visible = false;
                }
            }
            Session.Add(ReporteService.GetType().Name + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ReporteService.GetType().Name + "L", "Actualizar", ex);
        }
    }


    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            int IdReporte ;
            IdReporte = Convert.ToInt32(idObjeto);
            UIAF vRepor = new UIAF();
            vRepor = ReporteService.ConsultarReporteUIAF(IdReporte, (Usuario)Session["usuario"]);

            if (vRepor.idreporte != 0)
                txtIdReporte.Text = vRepor.idreporte.ToString();
            if (vRepor.fecha_inicial != DateTime.MinValue)
                txtFechaIni.Text = vRepor.fecha_inicial.ToShortDateString();
            if (vRepor.fecha_final != DateTime.MinValue)
                txtFechaFin.Text = vRepor.fecha_final.ToShortDateString();

            Actualizar();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ReporteService.CodigoPrograma, "ObtenerDatos", ex);
        }
    }


    protected Boolean ValidarDatos()
    {
        VerError("");
        if(chkFiltro.Checked == false)
        { 
            if (txtFechaIni.Text == "")
            {
                VerError("Seleccione la fecha Inicial");
                return false;
            }
            if (txtFechaFin.Text == "")
            {
                VerError("Seleccione la fecha Final");
                return false;
            }
            if (txtFechaIni.Text != "" && txtFechaFin.Text != "")
            {
                if (Convert.ToDateTime(txtFechaIni.Text) > Convert.ToDateTime(txtFechaFin.Text))
                {
                    VerError("No puede Ingresar una Fecha inicial mayor a la fecha final");
                    return false;
                }
            }
        }
        VerError("");
        return true;
    }


    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            VerError("");
            if (ValidarDatos())
            {
                Session["opcion"] = "GRABAR";
                ctlMensaje.MostrarMensaje("Desea grabar los datos ingresados?");
            }         
        }       
        catch (Exception ex)
        {
            BOexcepcion.Throw(ReporteService.CodigoPrograma, "btnGuardar_Click", ex);
        }
    }



    protected void gvProducto_vista_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int conseID = Convert.ToInt32(gvProducto_vista.DataKeys[e.RowIndex].Values[0].ToString());

        Session["ID"] = conseID;
        Session["opcion"] = "ELIMINAR";
        ctlMensaje.MostrarMensaje("Desea Eliminar el registro Seleccionado?");
    }

   protected void gvProductos_RowDatabound(object sender, GridViewRowEventArgs e)
    {

    Xpinn.Comun.Entities.General pData = new Xpinn.Comun.Entities.General();
    Xpinn.Comun.Data.GeneralData ConsultaData = new Xpinn.Comun.Data.GeneralData();
    pData = ConsultaData.ConsultarGeneral(9188, (Usuario)Session["usuario"]);
        Int64 valor = Convert.ToInt64(pData.valor);
        if (valor == 1)//si es 1 mostrar ciudad de oficina , si es 0 o null mostra ciudad de la persona
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                if (valor >= 1)
                {
                    e.Row.Cells[7].Controls.Clear();
                    e.Row.Cells[7].Visible = false;
                    e.Row.Cells[8].Controls.Clear();
                    e.Row.Cells[8].Visible = true;
                }

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    if (valor >= 1)
                    {
                        e.Row.Cells[7].Controls.Clear();
                        e.Row.Cells[7].Visible = false;
                        e.Row.Cells[8].Controls.Clear();
                        e.Row.Cells[8].Visible = true;
                    }
                }
            }
           

        }
        else
        {
            e.Row.Cells[7].Controls.Clear();
            e.Row.Cells[7].Visible = true;
            e.Row.Cells[8].Controls.Clear();
            e.Row.Cells[8].Visible = false;
        }
    }

    protected void gvProducto_vista_RowDatabound(object sender, GridViewRowEventArgs e)
    {

        Xpinn.Comun.Entities.General pData = new Xpinn.Comun.Entities.General();
        Xpinn.Comun.Data.GeneralData ConsultaData = new Xpinn.Comun.Data.GeneralData();
        pData = ConsultaData.ConsultarGeneral(9188, (Usuario)Session["usuario"]);
        Int64 valor = Convert.ToInt64(pData.valor);
        if (valor == 1)//si es 1 mostrar ciudad de oficina , si es 0 o null mostra ciudad de la persona
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                if (valor >= 1)
                {
                    e.Row.Cells[6].Controls.Clear();
                    e.Row.Cells[6].Visible = false;
                    e.Row.Cells[7].Controls.Clear();
                    e.Row.Cells[7].Visible = true;
                }

            }


            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (valor >= 1)
                {
                    e.Row.Cells[6].Controls.Clear();
                    e.Row.Cells[6].Visible = false;
                    e.Row.Cells[7].Controls.Clear();
                    e.Row.Cells[7].Visible = true;
                }
            }


         }
        else
        {
            e.Row.Cells[6].Controls.Clear();
            e.Row.Cells[6].Visible = true;
            e.Row.Cells[7].Controls.Clear();
            e.Row.Cells[7].Visible = false;
        }
    }


    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        try
        {
            if (Session["opcion"].ToString() == "GRABAR")
            {
                UIAF vUiaf = new UIAF();

                if (idObjeto != "")
                    vUiaf.idreporte = Convert.ToInt32(txtIdReporte.Text);
                else
                    vUiaf.idreporte = 0;

                vUiaf.idformato = null;
                vUiaf.fecha_generacion = DateTime.Now;

                //CodigoUsuario Asignado en Capa Datos

                if (chkFiltro.Checked == false)
                {
                    vUiaf.fecha_inicial = Convert.ToDateTime(txtFechaIni.Text);
                    vUiaf.fecha_final = Convert.ToDateTime(txtFechaFin.Text);
                }
                else
                {
                    vUiaf.fecha_inicial = DateTime.Now;
                    vUiaf.fecha_final = DateTime.Now;
                }
                vUiaf.lstProductos = new List<UIAFDetalle>();
                if (idObjeto == "")
                {
                    vUiaf.numero_registros = gvProducto_vista.Rows.Count;
                    if (gvProducto_vista.Rows.Count > 0)
                        vUiaf.lstProductos = ObtenerListaGridView();
                }
                else
                {
                    vUiaf.numero_registros = gvProductos.Rows.Count;
                    if (gvProductos.Rows.Count > 0)
                        vUiaf.lstProductos = ObtenerListaGridView();
                }
                
                
                if (idObjeto != "")
                    ReporteService.ModificarUiafREporte(vUiaf, (Usuario)Session["usuario"]);
                else
                    ReporteService.CrearUiafREporte(vUiaf, (Usuario)Session["usuario"]);               
                mvCuentasxPagar.ActiveViewIndex = 1;                
            }

            if (Session["opcion"].ToString() == "ELIMINAR")
            {
                int conseID = Convert.ToInt32(Session["ID"].ToString());
                ObtenerListaGridView();

                List<UIAFDetalle> LstProd;
                LstProd = (List<UIAFDetalle>)Session["DATOS"];


                if (conseID > 0)
                {
                    try
                    {
                        foreach (UIAFDetalle acti in LstProd)
                        {
                            if (Convert.ToInt32(acti.numero_producto) == conseID)
                            {
                                LstProd.Remove(acti);
                                break;
                            }
                        }
                    }
                    catch (Xpinn.Util.ExceptionBusiness ex)
                    {
                        VerError(ex.Message);
                    }
                }

                gvProducto_vista.DataSourceID = null;
                gvProducto_vista.DataBind();

                gvProducto_vista.DataSource = LstProd;
                gvProducto_vista.DataBind();

                Session["DATOS"] = LstProd;

                //ReporteService.EliminarReporteUIAF(Convert.ToInt32(Session["ID"]), (Usuario)Session["usuario"]);                
                //Actualizar();                
            }
            Session.Remove("opcion");

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ReporteService.CodigoPrograma, "btnContinuarMen_Click", ex);
        }    
    }


    protected List<UIAFDetalle> ObtenerListaGridView()
    {
        List<UIAFDetalle> lstDetalle = new List<UIAFDetalle>();
        List<UIAFDetalle> lista = new List<UIAFDetalle>();
                     
        if(idObjeto !="")
        {
            foreach (GridViewRow rfila in gvProductos.Rows)
            {
                UIAFDetalle eProd = new UIAFDetalle();

                if (rfila.Cells[1].Text != "" && rfila.Cells[1].Text != null)//codigo
                    eProd.idreporte = Convert.ToInt32(rfila.Cells[1].Text);
                else
                    eProd.idreporte = 0;

                if (rfila.Cells[3].Text != "" && rfila.Cells[3].Text != "&nbsp;")//nroProd
                    eProd.numero_producto = rfila.Cells[3].Text;
                else
                    eProd.numero_producto = null;
                if (rfila.Cells[4].Text != "" && rfila.Cells[4].Text != "&nbsp;")//FechaVinculacion
                    eProd.fecha_apertura = Convert.ToDateTime(rfila.Cells[4].Text);
                else
                    eProd.fecha_apertura = DateTime.MinValue;
                if (rfila.Cells[5].Text != "" && rfila.Cells[5].Text != "&nbsp;")//TipoProd
                    eProd.tipo_producto = rfila.Cells[5].Text;
                else
                    eProd.tipo_producto = null;
                if (rfila.Cells[6].Text != "" && rfila.Cells[6].Text != "&nbsp;")//CodCiudad
                    eProd.tipo_tran = rfila.Cells[6].Text;
                else
                    eProd.tipo_tran = null;
                if (rfila.Cells[7].Text != "" && rfila.Cells[7].Text != "&nbsp;")//CodCiudad
                    eProd.departamento = rfila.Cells[7].Text;
                else
                    eProd.departamento = null;
                if (rfila.Cells[8].Text != "" && rfila.Cells[8].Text != "&nbsp;")//TipoIdent1
                    eProd.tipo_identificacion1 = rfila.Cells[8].Text;
                else
                    eProd.tipo_identificacion1 = null;
                if (rfila.Cells[9].Text != "" && rfila.Cells[9].Text != "&nbsp;")  //Identifica1
                    eProd.identificacion1 = rfila.Cells[9].Text;
                else
                    eProd.identificacion1 = null;
                if (rfila.Cells[10].Text != "" && rfila.Cells[10].Text != "&nbsp;")  //Primer Nombre
                    eProd.primer_nombre1 = rfila.Cells[10].Text;
                else
                    eProd.primer_nombre1 = null;
                if (rfila.Cells[11].Text != "" && rfila.Cells[11].Text != "&nbsp;")  //Segundo Nombre
                    eProd.segundo_nombre1 = rfila.Cells[11].Text;
                else
                    eProd.segundo_nombre1 = null;
                if (rfila.Cells[12].Text != "" && rfila.Cells[12].Text != "&nbsp;")  //Primer Apellido
                    eProd.primer_apellido1 = rfila.Cells[12].Text;
                else
                    eProd.primer_apellido1 = null;
                if (rfila.Cells[13].Text != "" && rfila.Cells[13].Text != "&nbsp;")  //Segundo Apellido
                    eProd.segundo_apellido1 = rfila.Cells[13].Text;
                else
                    eProd.segundo_apellido1 = null;
                if (rfila.Cells[14].Text != "" && rfila.Cells[14].Text != "&nbsp;")  //Razon Social
                    eProd.razon_social1 = rfila.Cells[14].Text;
                else
                    eProd.razon_social1 = null;

                if (rfila.Cells[15].Text != "" && rfila.Cells[15].Text != "&nbsp;")//TipoIdent2
                    eProd.tipo_identificacion2 = rfila.Cells[15].Text;
                else
                    eProd.tipo_identificacion2 = null;
                if (rfila.Cells[16].Text != "" && rfila.Cells[16].Text != "&nbsp;")  //Identifica2
                    eProd.identificacion2 = rfila.Cells[16].Text;
                else
                    eProd.identificacion2 = null;
                if (rfila.Cells[17].Text != "" && rfila.Cells[17].Text != "&nbsp;")  //Primer Nombre2
                    eProd.primer_nombre2 = rfila.Cells[17].Text;
                else
                    eProd.primer_nombre2 = null;
                if (rfila.Cells[18].Text != "" && rfila.Cells[18].Text != "&nbsp;")  //Segundo Nombre2
                    eProd.segundo_nombre2 = rfila.Cells[18].Text;
                else
                    eProd.segundo_nombre2 = null;
                if (rfila.Cells[19].Text != "" && rfila.Cells[19].Text != "&nbsp;")  //Primer Apellido2
                    eProd.primer_apellido2 = rfila.Cells[19].Text;
                else
                    eProd.primer_apellido2 = null;
                if (rfila.Cells[20].Text != "" && rfila.Cells[20].Text != "&nbsp;")  //Segundo Apellido2
                    eProd.segundo_apellido2 = rfila.Cells[20].Text;
                else
                    eProd.segundo_apellido2 = null;
                if (rfila.Cells[21].Text != "" && rfila.Cells[21].Text != "&nbsp;")  //Razon Social2
                    eProd.razon_social2 = rfila.Cells[21].Text;
                else
                    eProd.razon_social2 = null;
                

                if (eProd.numero_producto != null && eProd.numero_producto != "")
                {
                    lstDetalle.Add(eProd);
                }
            }
        }
        else
        {

            foreach (GridViewRow rfila in gvProducto_vista.Rows)
            {
                UIAFDetalle eProd = new UIAFDetalle();
                if (rfila.Cells[1].Text != "" && rfila.Cells[1].Text != null)//Consecutivo
                    eProd.consecutivo = Convert.ToInt32(rfila.Cells[1].Text);
                else
                    eProd.consecutivo = 0;
                if (rfila.Cells[2].Text != "" && rfila.Cells[2].Text != "&nbsp;")//nroProd
                    eProd.numero_producto = rfila.Cells[2].Text;
                else
                    eProd.numero_producto = null;
                if (rfila.Cells[3].Text != "" && rfila.Cells[3].Text != "&nbsp;")//FechaVinculacion
                    eProd.fecha_apertura = Convert.ToDateTime(rfila.Cells[3].Text);
                else
                    eProd.fecha_apertura = DateTime.MinValue;
                if (rfila.Cells[4].Text != "" && rfila.Cells[4].Text != "&nbsp;")//TipoProd
                    eProd.tipo_producto_vista = Convert.ToInt32(rfila.Cells[4].Text);
                else
                    eProd.tipo_producto_vista = 0;
                if (rfila.Cells[5].Text != "" && rfila.Cells[5].Text != "&nbsp;")//CodCiudad
                    eProd.tipo_tran = Convert.ToString(rfila.Cells[5].Text);
                else
                    eProd.tipo_tran = null;
                if (rfila.Cells[6].Text != "" && rfila.Cells[6].Text != "&nbsp;")//CodCiudad
                    eProd.departamento = Convert.ToString(rfila.Cells[6].Text);
                else
                    eProd.departamento_vista = 0;
                if (rfila.Cells[7].Text != "" && rfila.Cells[7].Text != "&nbsp;")//TipoIdent1
                    eProd.tipo_identificacion1_vista = Convert.ToInt32(rfila.Cells[7].Text);
                else
                    eProd.tipo_identificacion1_vista = 0;
                if (rfila.Cells[8].Text != "" && rfila.Cells[8].Text != "&nbsp;")  //Identifica1
                    eProd.identificacion1 = rfila.Cells[8].Text;
                else
                    eProd.identificacion1 = null;
                if (rfila.Cells[9].Text != "" && rfila.Cells[9].Text != "&nbsp;")  //Primer Nombre
                    eProd.primer_nombre1 = rfila.Cells[9].Text;
                else
                    eProd.primer_nombre1 = null;
                if (rfila.Cells[10].Text != "" && rfila.Cells[10].Text != "&nbsp;")  //Segundo Nombre
                    eProd.segundo_nombre1 = rfila.Cells[10].Text;
                else
                    eProd.segundo_nombre1 = null;
                if (rfila.Cells[11].Text != "" && rfila.Cells[11].Text != "&nbsp;")  //Primer Apellido
                    eProd.primer_apellido1 = rfila.Cells[11].Text;
                else
                    eProd.primer_apellido1 = null;
                if (rfila.Cells[12].Text != "" && rfila.Cells[12].Text != "&nbsp;")  //Segundo Apellido
                    eProd.segundo_apellido1 = rfila.Cells[12].Text;
                else
                    eProd.segundo_apellido1 = null;
                if (rfila.Cells[13].Text != "" && rfila.Cells[13].Text != "&nbsp;")  //Razon Social
                    eProd.razon_social1 = rfila.Cells[13].Text;
                else
                    eProd.razon_social1 = null;

                if (rfila.Cells[14].Text != "" && rfila.Cells[14].Text != "&nbsp;")//TipoIdent2
                    eProd.tipo_identificacion2_vista = Convert.ToInt32(rfila.Cells[14].Text);
                else
                    eProd.tipo_identificacion2_vista = 0;
                if (rfila.Cells[15].Text != "" && rfila.Cells[15].Text != "&nbsp;")  //Identifica2
                    eProd.identificacion2 = rfila.Cells[15].Text;
                else
                    eProd.identificacion2 = null;
                if (rfila.Cells[16].Text != "" && rfila.Cells[16].Text != "&nbsp;")  //Primer Nombre2
                    eProd.primer_nombre2 = rfila.Cells[16].Text;
                else
                    eProd.primer_nombre2 = null;
                if (rfila.Cells[17].Text != "" && rfila.Cells[17].Text != "&nbsp;")  //Segundo Nombre2
                    eProd.segundo_nombre2 = rfila.Cells[17].Text;
                else
                    eProd.segundo_nombre2 = null;
                if (rfila.Cells[18].Text != "" && rfila.Cells[18].Text != "&nbsp;")  //Primer Apellido2
                    eProd.primer_apellido2 = rfila.Cells[18].Text;
                else
                    eProd.primer_apellido2 = null;
                if (rfila.Cells[19].Text != "" && rfila.Cells[19].Text != "&nbsp;")  //Segundo Apellido2
                    eProd.segundo_apellido2 = rfila.Cells[19].Text;
                else
                    eProd.segundo_apellido2 = null;
                if (rfila.Cells[20].Text != "" && rfila.Cells[20].Text != "&nbsp;")  //Razon Social2
                    eProd.razon_social2 = rfila.Cells[20].Text;
                else
                    eProd.razon_social2 = null;

                lista.Add(eProd);
                Session["DATOS"] = lstDetalle;

                if (eProd.numero_producto != null && eProd.numero_producto != "")
                {
                    lstDetalle.Add(eProd);
                }
            }
        }
           
        
        return lstDetalle;
    }


    protected void btnRegresar_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }

    protected string ReemplazarTextos(String pTexto)
    {
        return pTexto.Replace(";", "").Replace("&#201", "E").Replace("&#193", "A").Replace("&#211", "O").Replace("&#243", "o").Replace("&#205", "I").Replace("&nbsp", "").Replace("&#209", "N");
    }

    protected void btnArchivo_Click(object sender, EventArgs e)
    {
        string nombreArchivo = "UIAFReporte.txt";
        if (gvProductos.Rows.Count > 0 && Session["DTUIAF"] != null)
        {
            System.IO.StreamWriter sw = new System.IO.StreamWriter(Server.MapPath("") + nombreArchivo, true);
            // Decargar titulos
            int titcount = gvProductos.HeaderRow.Cells.Count;
            string linea = "";
            for (int j = 1; j < titcount - 1; j++)
            {
                string texto = ReemplazarTextos(gvProductos.HeaderRow.Cells[j].Text);
                linea += texto + ";";
            }
            sw.WriteLine(linea);
            // Descargar el texto de cada fila
            int rowcount = gvProductos.Rows.Count;
            for (int i = 0; i < rowcount - 1; i++)
            {
                int celcount = gvProductos.Rows[i].Cells.Count;
                linea = "";
                for (int j = 1; j < celcount - 1; j++)
                {
                    string texto = ReemplazarTextos(gvProductos.Rows[i].Cells[j].Text);
                    linea += texto + ";";
                }
                sw.WriteLine(linea);
            }
            sw.Close();
        }
        else
        {
            System.IO.StreamWriter sw = new System.IO.StreamWriter(Server.MapPath("") + nombreArchivo, true);
            // Decargar titulos
            int titcount = gvProducto_vista.HeaderRow.Cells.Count;
            string linea = "";
            for (int j = 1; j < titcount - 1; j++)
            {
                string texto = ReemplazarTextos(gvProducto_vista.HeaderRow.Cells[j].Text);
                linea += texto + ";";
            }
            sw.WriteLine(linea);
            // Descargar el texto de cada fila
            int rowcount = gvProducto_vista.Rows.Count;
            for (int i = 0; i < rowcount - 1; i++)
            {
                int celcount = gvProducto_vista.Rows[i].Cells.Count;
                linea = "";
                for (int j = 1; j < celcount - 1; j++)
                {
                    string texto = ReemplazarTextos(gvProducto_vista.Rows[i].Cells[j].Text);
                    linea += texto + ";";
                }
                sw.WriteLine(linea);
            }
            sw.Close();
        }
        if (File.Exists(Server.MapPath("") + nombreArchivo))
        {
            System.IO.StreamReader sr;
            sr = File.OpenText(Server.MapPath("") + nombreArchivo);
            string texto = sr.ReadToEnd();
            sr.Close();
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.ContentType = "text/plain";
            HttpContext.Current.Response.Write(texto);
            HttpContext.Current.Response.AppendHeader("Content-disposition", "attachment;filename=" + nombreArchivo);
            HttpContext.Current.Response.Flush();
            File.Delete(Server.MapPath("") + nombreArchivo);
            HttpContext.Current.Response.End();
        }
    }

    protected void btnExportar_Click(object sender, EventArgs e)
    {
        if (idObjeto != "")
        {
            if (gvProductos.Rows.Count > 0 && Session["DTUIAF"] != null)
            {
                StringBuilder sb = new StringBuilder();
                StringWriter sw = new StringWriter(sb);
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                Page pagina = new Page();
                dynamic form = new HtmlForm();
                gvProductos.AllowPaging = false;
                gvProductos.DataSource = Session["DTUIAF"];
                gvProductos.DataBind();
                gvProductos.EnableViewState = false;
                pagina.EnableEventValidation = false;
                pagina.DesignerInitialize();
                pagina.Controls.Add(form);
                form.Controls.Add(gvProductos);
                pagina.RenderControl(htw);
                Response.Clear();
                Response.Buffer = true;
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("Content-Disposition", "attachment;filename=UIAFProductos.xls");
                Response.Charset = "UTF-8";
                Response.ContentEncoding = Encoding.Default;
                Response.Write(sb.ToString());
                Response.End();
                gvProductos.AllowPaging = true;
                gvProductos.DataBind();

            }
            else
                VerError("Se debe generar el reporte primero");
        }
        else
        {
            if (gvProducto_vista.Rows.Count > 0 && Session["DTUIAF"] != null)
            {
                StringBuilder sb = new StringBuilder();
                StringWriter sw = new StringWriter(sb);
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                Page pagina = new Page();
                dynamic form = new HtmlForm();
                gvProducto_vista.AllowPaging = false;
                gvProducto_vista.DataSource = Session["DTUIAF"];
                gvProducto_vista.DataBind();
                gvProducto_vista.EnableViewState = false;
                pagina.EnableEventValidation = false;
                pagina.DesignerInitialize();
                pagina.Controls.Add(form);
                form.Controls.Add(gvProducto_vista);
                pagina.RenderControl(htw);
                Response.Clear();
                Response.Buffer = true;
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("Content-Disposition", "attachment;filename=UIAFProductos.xls");
                Response.Charset = "UTF-8";
                Response.ContentEncoding = Encoding.Default;
                Response.Write(sb.ToString());
                Response.End();
                gvProducto_vista.AllowPaging = true;
                gvProducto_vista.DataBind();
            }
            else
                VerError("Se debe generar el reporte primero");
        }
    }




    protected void gvProductos_DataBound(object sender, EventArgs e)
    {

    }
}