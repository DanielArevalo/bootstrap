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
using Xpinn.Servicios.Entities;
using Xpinn.Servicios.Services;
using Xpinn.Aportes.Entities;
using Xpinn.Aportes.Services;
using System.IO;
using System.Text;

partial class Lista : GlobalWeb
{
    SolicitudServiciosServices SoliServicios = new SolicitudServiciosServices();
    Usuario _usuario;
    PlanesTelefonicosService LineaTeleServicio = new PlanesTelefonicosService();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones("80113", "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoNuevo += btnNuevo_Click;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoExportar += btnExportar_Click;
            ctlMensaje.eventoClick += btnContinuarMen_Click;

            toolBar.eventoLimpiar += btnLimpiar_Click;
            toolBar.eventoImportar += btnImportar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(LineaTeleServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }
    
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            _usuario = (Usuario)Session["usuario"];
            
            Site toolBar = (Site)this.Master;
            toolBar.MostrarGuardar(false);
            toolBar.MostrarLimpiar(false);

            if (!IsPostBack)
            {
                cargarDropdown();
                Actualizar();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(SoliServicios.CodigoPrograma, "Page_Load", ex);
        }
    }


    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {

        Page.Validate();
        gvLista.Visible = true;
        if (Page.IsValid)
        {
            Actualizar();
        }
    }


    void cargarDropdown()
    {
        PoblarLista("PLANES_TELEFONICOS", "COD_PLAN, NOMBRE", "", "NOMBRE", ddlPlan, true);
    }

    protected void btnImportar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        mvPrincipal.ActiveViewIndex = 1;
        panelData.Visible = false;
        DateTime fecha = DateTime.Now;
        ucFecha.Text = fecha.ToString();
        Site toolBar = (Site)Master;
        toolBar.MostrarNuevo(false);
        toolBar.MostrarConsultar(false);
        toolBar.MostrarLimpiar(true);
        toolBar.MostrarImportar(false);        
        //LimpiarDataImportacion();
    }

    private void btnExportar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        if (gvLista.Rows.Count > 0)
            ExportToExcel(gvLista);
    }

    protected void ExportToExcel(GridView GridView1)
    {
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=LineasTelefónicas.xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";
        Response.ContentEncoding = Encoding.Default;
        StringWriter sw = new StringWriter();
        ExpGrilla expGrilla = new ExpGrilla();

        List<PlanTelefonico> lstLineasTel = null;
        if (Session["ListServicio"] != null)
            lstLineasTel = (List<PlanTelefonico>)Session["ListServicio"];
        sw = expGrilla.ObtenerGrilla(GridView1, lstLineasTel);

        Response.Write(expGrilla.style);
        Response.Output.Write(sw.ToString());
        Response.Flush();
        Response.End();
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        //LimpiarValoresConsulta(pConsulta, LineaAporteServicio.CodigoProgramaLineas);
        //txtCodLinea.Text = "";
        //txtNombreLinea.Text = "";
        //gvLista.DataBind();

    }


    protected void btnCargarAdicionales_Click(object sender, EventArgs e)
    {
        try
        {
            VerError("");
            string error = "";
            //Valida fechas de ultimo cierre contable y de servicios
            //Cargar fechas de cierre
            Xpinn.Comun.Services.CiereaService com = new Xpinn.Comun.Services.CiereaService();
            DateTime contable = com.FechaUltimoCierre("C", (Usuario)Session["usuario"]);
            DateTime servicios = com.FechaUltimoCierre("Q", (Usuario)Session["usuario"]);
            DateTime carga = ucFecha.ToDateTime;
            if (carga > contable && carga > servicios)
            {
                if (fupArchivoPersona.HasFile)
                {
                    Stream stream = fupArchivoPersona.FileContent;
                    List<Xpinn.Aportes.Entities.ErroresCargaAportes> plstErrores = new List<Xpinn.Aportes.Entities.ErroresCargaAportes>();
                    List<PlanTelefonico> lstAdicionales = new List<PlanTelefonico>();

                    //LLAMANDO AL METODO DE CAPTURA DE DATOS
                    LineaTeleServicio.CargaAdicionales(ref error, stream, ref lstAdicionales, ref plstErrores, (Usuario)Session["usuario"]);


                    if (error.Trim() != "")
                    {
                        VerError(error);
                        return;
                    }
                    if (plstErrores.Count() > 0)
                    {
                        pErrores.Visible = true;
                        gvErrores.DataSource = plstErrores;
                        gvErrores.DataBind();
                        cpeDemo.CollapsedText = "(Click Aqui para ver " + plstErrores.Count() + " errores...)";
                        cpeDemo.ExpandedText = "(Click Aqui para ocultar listado de errores...)";
                    }
                    panelData.Visible = false;
                    if (lstAdicionales.Count > 0)
                    {
                        Session["lstData"] = lstAdicionales;
                        panelData.Visible = true;
                        //CARGAR DATOS A GRILLA DE NATURALES
                        gvDatos.DataSource = lstAdicionales;
                        gvDatos.DataBind();
                        Site toolBar = (Site)this.Master;
                        toolBar.MostrarGuardar(true);
                    }

                }
                else
                {
                    VerError("Seleccione el archivo a cargar, verifique los datos.");
                    return;
                }
            }
            else
            {
                if(ucFecha.ToDateTime <= contable)
                    VerError("La fecha seleccionada es inferior a la fecha del último cierre contable.");
                else
                    VerError("La fecha seleccionada es inferior a la fecha del último cierre de servicios.");
            }

        }
        catch (Exception ex)
        {
            // BOexcepcion.Throw(LineaAporteServicio.CodigoProgramaLineas + "L", "btnCargarAportes_Click", ex);
        }
    }

    protected void gvDatos_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            List<PlanTelefonico> lstAdicionales = new List<PlanTelefonico>();
            lstAdicionales = (List<PlanTelefonico>)Session["lstData"];

            lstAdicionales.RemoveAt((gvDatos.PageIndex * gvDatos.PageSize) + e.RowIndex);

            gvDatos.DataSourceID = null;
            gvDatos.DataBind();

            gvDatos.DataSource = lstAdicionales;
            gvDatos.DataBind();
            Session["lstData"] = lstAdicionales;
            Site toolBar = (Site)this.Master;
            toolBar.MostrarGuardar(true);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(SoliServicios.CodigoPrograma, "gvDatos_RowDeleting", ex);

        }
    }

    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            VerError("");
            if (ValidarData())
                ctlMensaje.MostrarMensaje("Desea realizar la grabación de datos?<br/>");
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(SoliServicios.CodigoPrograma, "btnGuardar_Click", ex);
        }
    }

    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        try
        {
            VerError("");
            List<PlanTelefonico> lstAdicionales = new List<PlanTelefonico>();
            lstAdicionales = (List<PlanTelefonico>)Session["lstData"];
            List<Int64> lst_Num_Lin = new List<Int64>();
            
            Usuario pUsuario = new Usuario();
            pUsuario = (Usuario)Session["usuario"];

            // DATOS DE LA OPERACION
            Xpinn.Tesoreria.Entities.Operacion vOpe = new Xpinn.Tesoreria.Entities.Operacion();
            vOpe.cod_ope = 0;
            vOpe.tipo_ope = 110;
            vOpe.cod_caja = 0;
            vOpe.cod_cajero = 0;
            vOpe.observacion = "Operacion-Carga de Servicios para línea telefónica Masivamente";
            vOpe.cod_proceso = null;
            vOpe.fecha_oper = DateTime.Now;
            vOpe.fecha_calc = ucFecha.ToDateTime;
            vOpe.cod_ofi = pUsuario.cod_oficina;
            
            string pError = "";
            LineaTeleServicio.CrearImportacion(ref pError, lstAdicionales, ref vOpe, (Usuario)Session["usuario"], ref lst_Num_Lin);
            
            //Consultar la linea de servicio para cargar el código del proveedor como beneficiario del comprobante
            LineaServiciosServices linServ = new LineaServiciosServices();
            PlanTelefonico pLinea = new PlanTelefonico();
            string num_linea = (from item in lstAdicionales
                                 where item.num_linea_telefonica != ""
                                 select item.num_linea_telefonica).FirstOrDefault();

            pLinea = LineaTeleServicio.ConsultarLineaTelefonica(num_linea, (Usuario)Session["usuario"]);
            LineaServicios vDetalle = linServ.ConsultarLineaSERVICIO(pLinea.cod_linea_servicio.ToString(), (Usuario)Session["usuario"]);

            if (pError != "")
            {
                VerError(pError);
                return;
            }
            
            if (lst_Num_Lin.Count > 0)
            {
                foreach (int element in lst_Num_Lin)
                {
                    //CARGAR DATOS A GRILLA DE ADICIONALES NO IMPORTADOS 
                    GridView1.DataSource = lst_Num_Lin;
                    GridView1.DataBind();
                    GridView1.HeaderRow.Cells[0].Text = "Número Linea Telefónica";
                    infApor_no.Visible = true;
                }
            }
            else
            {
                infApor_no.Visible = false;
                if (vOpe.cod_ope != 0) //Si no hay errores, generar el comprobante
                {
                    Xpinn.Contabilidad.Services.ComprobanteService ComprobanteServicio = new Xpinn.Contabilidad.Services.ComprobanteService();
                    Session[ComprobanteServicio.CodigoPrograma + ".cod_ope"] = vOpe.cod_ope;
                    Session[ComprobanteServicio.CodigoPrograma + ".tipo_ope"] = 110;
                    Session[ComprobanteServicio.CodigoPrograma + ".fecha_ope"] = vOpe.fecha_oper;
                    Session[ComprobanteServicio.CodigoPrograma + ".cod_persona"] = vDetalle.cod_proveedor;
                    Navegar("../../../Contabilidad/Comprobante/Nuevo.aspx");
                }
            }

            //mvPrincipal.ActiveViewIndex = 2;
            //Site toolBar = (Site)this.Master;
            //toolBar.MostrarGuardar(false);
            //toolBar.MostrarConsultar(false);
            //toolBar.MostrarLimpiar(false);
            Session.Remove("lstData");
            Session.Remove("lstData2");
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(SoliServicios.CodigoPrograma, "btnContinuarMen_Click", ex);
        }
    }

    Boolean ValidarData()
    {
        if (string.IsNullOrEmpty(ucFecha.Text))
        {
            VerError("Ingrese la fecha de próximo pago");
            ucFecha.Focus();
            return false;
        }
        if (gvDatos.Rows.Count <= 0)
        {
            VerError("No existen datos por registrar, verifique los datos.");
            return false;
        }
        return true;
    }


    public void PoblarLista(string pTabla, DropDownList ddlControl)
    {
        PoblarLista(pTabla, "", "", "", ddlControl);
    }

    public void PoblarLista(string pTabla, string pColumnas, string pCondicion, string pOrden, DropDownList ddlControl, bool EsPLan = false)
    {
        List<Xpinn.Comun.Entities.ListaDesplegable> plista = new List<Xpinn.Comun.Entities.ListaDesplegable>();
        Xpinn.Comun.Entities.ListaDesplegable pentidad = new Xpinn.Comun.Entities.ListaDesplegable();
        Xpinn.Comun.Services.ListaDeplegableService pservicio = new Xpinn.Comun.Services.ListaDeplegableService();

        ddlControl.Items.Clear();
        plista = pservicio.ListarListaDesplegable(pentidad, pTabla, pColumnas, pCondicion, pOrden, (Usuario)Session["usuario"]);
        if (EsPLan)
        {
            plista = plista.Select(x => new Xpinn.Comun.Entities.ListaDesplegable
            {
                idconsecutivo = x.idconsecutivo,
                descripcion = string.Format("{0} - {1}", x.idconsecutivo, x.descripcion)
            }).ToList();
        }
        pentidad.idconsecutivo = null;
        pentidad.descripcion = "Seleccione un item";
        plista.Insert(0, pentidad);
        ddlControl.DataTextField = "descripcion";
        ddlControl.DataValueField = "idconsecutivo";
        ddlControl.DataSource = plista;
        ddlControl.DataBind();
    }


    protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    {
        Navegar(Pagina.Nuevo);
    }

    protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvLista.PageIndex = e.NewPageIndex;
            Actualizar();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(SoliServicios.CodigoPrograma, "gvLista_PageIndexChanging", ex);
        }
    }


    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvLista.Rows[e.NewEditIndex].Cells[2].Text;
        Session[SoliServicios.CodigoPrograma + ".id"] = id;
        Navegar(Pagina.Nuevo);
    }


    protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

        string Estado = gvLista.DataKeys[e.RowIndex].Values[1].ToString();

        if (Estado == "S")
        {
            int id = Convert.ToInt32(gvLista.DataKeys[e.RowIndex].Values[0].ToString());
            Session["ID"] = id;
            ctlMensaje.MostrarMensaje("Desea realizar la eliminación del Servicio?");
        }
    }

    //protected void btnContinuarMen_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        SoliServicios.EliminarServicio(Convert.ToInt32(Session["ID"]), _usuario);
    //        Actualizar();
    //    }
    //    catch (Exception ex)
    //    {
    //        BOexcepcion.Throw(SoliServicios.CodigoPrograma, "btnContinuarMen_Click", ex);
    //    }
    //}


    private void Actualizar()
    {
        try
        {
            PlanTelefonico lineafiltro = new PlanTelefonico();

            if (txtNumLinea.Text != "")
                lineafiltro.num_linea_telefonica = txtNumLinea.Text;
            if (txtIdentificacion.Text != "")
                lineafiltro.identificacion_titular = txtIdentificacion.Text;
            if (ddlPlan.SelectedValue != "")
                lineafiltro.cod_plan = Convert.ToInt32(ddlPlan.SelectedValue);

            List<PlanTelefonico> lstConsulta = new List<PlanTelefonico>();
            lstConsulta = LineaTeleServicio.ListarLineasTelefonicas(lineafiltro, Usuario);

            gvLista.PageSize = 20;
            String emptyQuery = "Fila de datos vacia";
            gvLista.EmptyDataText = emptyQuery;
            Session["ListServicio"] = lstConsulta;
            if (lstConsulta.Count > 0)
            {
                gvLista.DataSource = lstConsulta;
                gvLista.DataBind();
                panelGrilla.Visible = true;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();

            }
            else
            {
                panelGrilla.Visible = false;
                lblTotalRegs.Visible = false;
            }

            Session.Add(SoliServicios.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(SoliServicios.CodigoPrograma, "Actualizar", ex);
        }
    }





}