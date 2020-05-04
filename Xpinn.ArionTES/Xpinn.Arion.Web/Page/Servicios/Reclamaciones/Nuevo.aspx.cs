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


partial class Nuevo : GlobalWeb
{
    PoblarListas Poblar = new PoblarListas();
    AprobacionServiciosServices ExcluServicios = new AprobacionServiciosServices();
    ReclamacionServiciosServicesService ServicioReclamacion = new ReclamacionServiciosServicesService();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(ExcluServicios.CodigoProgramaReclamacionServicios, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
            toolBar.eventoGuardar += btnGuardar_Click;
            ctlMensaje.eventoClick += btnContinuarMen_Click;
            toolBar.eventoCancelar += btnRegresar_Click;
            txtfechacrea.ToDateTime = DateTime.Now;
            toolBar.MostrarGuardar(false);
            toolBar.MostrarLimpiar(false);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ExcluServicios.CodigoProgramaReclamacionServicios, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                mvPrincipal.ActiveViewIndex = 0;
                cargarDropdown();
                cargarDropdowns();
              
                if (Session[ExcluServicios.CodigoProgramaReclamacionServicios + ".id"] != null)
                {
                    mvPrincipal.ActiveViewIndex = 1;
                    Site toolBar = (Site)this.Master;
                    toolBar.MostrarGuardar(true);
                    idObjeto = Session[ExcluServicios.CodigoProgramaReclamacionServicios + ".id"].ToString();
                    Session.Remove(ExcluServicios.CodigoProgramaReclamacionServicios + ".id");
                    ObtenerDatosReclamacion(idObjeto);
                }
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ExcluServicios.CodigoProgramaReclamacionServicios, "Page_Load", ex);
        }
    }
 
    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        LimpiarValoresConsulta(pConsulta, ExcluServicios.CodigoProgramaReclamacionServicios);
        txtFecha.Text = "";
        Actualizar();
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
       
        VerError("");
        Page.Validate();
        gvLista.Visible = true;
        if (Page.IsValid)
        {
            Actualizar();
        }
        if (mvPrincipal.ActiveViewIndex == 2)
        {
            Navegar(Pagina.Lista);
        }
    }

    void cargarDropdown()
    {
        Poblar.PoblarListaDesplegable("LINEASSERVICIOS", ddlLinea, (Usuario)Session["usuario"]);
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
            BOexcepcion.Throw(ExcluServicios.CodigoProgramaReclamacionServicios, "gvLista_PageIndexChanging", ex);
        }
    }


    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        Site toolBar = (Site)this.Master;
        String id = gvLista.DataKeys[e.NewEditIndex].Value.ToString();
        Session[ExcluServicios.CodigoProgramaReclamacionServicios + ".idServicio"] = id;
        mvPrincipal.ActiveViewIndex = 1;
       
        toolBar.MostrarConsultar(false);
        toolBar.MostrarGuardar(true);
        toolBar.MostrarLimpiar(false);
       

        ObtenerDatosServicio(id);
        txtfechareclamacion.Text = DateTime.Now.ToString(gFormatoFecha);
             
    }

    protected void cargarDropdowns()
    {
        Poblar.PoblarListaDesplegable("LINEASSERVICIOS", ddllineaservicio, (Usuario)Session["usuario"]);

        Xpinn.FabricaCreditos.Services.LineasCreditoService linahorroServicio = new Xpinn.FabricaCreditos.Services.LineasCreditoService();
        Xpinn.FabricaCreditos.Entities.LineasCredito linahorroVista = new Xpinn.FabricaCreditos.Entities.LineasCredito();
        ddlparentesco.DataTextField = "DESCRIPCION";
        ddlparentesco.DataValueField = "Codigo";
        ddlparentesco.DataSource = linahorroServicio.ListarParentesco(linahorroVista, (Usuario)Session["usuario"]);
        ddlparentesco.DataBind();
        ddlparentesco.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));
        
    }

    protected Boolean  validarcedulafallecido(String PidObjeto) 
    {
        Int32? numeroRecla = null;
        if (PidObjeto != "")
            Convert.ToInt32(idObjeto);

        Boolean resultado = true;

        if (ServicioReclamacion.ValidarFallecido(txtidentificacionfallecido.Text, numeroRecla, (Usuario)Session["usuario"]))
        {
            VerError("La cedula del fallecido ya esta");
            resultado = false;
        }
        else
        {
            VerError("");
            resultado = true;
        }

        return resultado;
    }



    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        if (validarcedulafallecido(idObjeto) == true)
        {
            if (ddlparentesco.SelectedIndex == 0)
            {
                VerError("Digite el parentesco con el asociado");
                return;
            }
            ctlMensaje.MostrarMensaje("Desea modificar los Datos Ingresados?");
        }
        else
        {
            VerError("La identificación del fallecido ya esta");
        }
    }



    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        try
        {

            Site toolBar = (Site)this.Master;

            ReclamacionServicios ReclamacionDeServicios = new ReclamacionServicios();
            ReclamacionDeServicios.codparentesco = ddlparentesco.SelectedIndex;
            ReclamacionDeServicios.fecha_reclamacion = Convert.ToDateTime(txtfechareclamacion.ToDate);
            ReclamacionDeServicios.fechacrea = Convert.ToDateTime(txtFecha.ToDate);
            ReclamacionDeServicios.nombre_fallecido = txtNombresFallecido.Text;
            ReclamacionDeServicios.identificacion_fallecido = txtidentificacionfallecido.Text;
            ReclamacionDeServicios.numero_servicio = Convert.ToInt32(txtCodigo.Text);
            ReclamacionDeServicios.fecha_creacion = Convert.ToDateTime(txtfechacrea.Text);

            DateTime pFechaIni;
            pFechaIni = txtfechacrea.ToDateTime == null ? DateTime.MinValue : txtfechacrea.ToDateTime;

            if (idObjeto == "") // Crear
            {
                ServicioReclamacion.CrearReclamacionServiciosServices(pFechaIni,ReclamacionDeServicios, (Usuario)Session["usuario"]);
            }
            else                //Modificar
            {
                ServicioReclamacion.ModificarReclamacionServiciosServices(pFechaIni, ReclamacionDeServicios, (Usuario)Session["usuario"]);
            }

            mvPrincipal.ActiveViewIndex = 2;
          
            toolBar.MostrarConsultar(true);
            toolBar.MostrarGuardar(false);
            toolBar.MostrarLimpiar(false);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ExcluServicios.CodigoProgramaReclamacionServicios, "btnContinuar_Click", ex);
        }
    }

    protected void btnRegresar_Click(object sender, ImageClickEventArgs e)
    {
        Navegar(Pagina.Lista);
    }

    private void Actualizar()
    {
        try
        {

            List<Servicio> lstConsulta = new List<Servicio>();
            String filtro = obtFiltro(ObtenerValores());
            DateTime pFechaIni;
            pFechaIni = txtFecha.ToDateTime == null ? DateTime.MinValue : txtFecha.ToDateTime;

            lstConsulta = ExcluServicios.ListarServicios(filtro,"", pFechaIni, (Usuario)Session["usuario"]);

            gvLista.PageSize = 15;
            String emptyQuery = "Fila de datos vacia";
            gvLista.EmptyDataText = emptyQuery;
            gvLista.DataSource = lstConsulta;
            if (lstConsulta.Count > 0)
            {
                gvLista.DataBind();
                panelGrilla.Visible = true;
                lblInfo.Visible = false;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                gvLista.Visible = true;
                
            }
            else
            {
                panelGrilla.Visible = false;                
                lblTotalRegs.Visible = false;
                lblInfo.Visible = true;
            }

            Session.Add(ExcluServicios.CodigoProgramaReclamacionServicios + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ExcluServicios.CodigoProgramaReclamacionServicios, "Actualizar", ex);
        }
    }

    protected void ObtenerDatosReclamacion(String PidObjeto)
    {
        ReclamacionServicios reclamacion = new ReclamacionServicios();
        reclamacion = ServicioReclamacion.ConsultarReclamacionServiciosServices(Convert.ToInt32(PidObjeto), (Usuario)Session["usuario"]);

        if (reclamacion.fecha_reclamacion != null)
            txtfechareclamacion.Text = reclamacion.fecha_reclamacion.ToString(gFormatoFecha);
        if (reclamacion.identificacion_fallecido != null)
            txtidentificacionfallecido.Text = reclamacion.identificacion_fallecido;
        if (reclamacion.nombre_fallecido != null)
            txtNombresFallecido.Text = reclamacion.nombre_fallecido;

        ObtenerDatosServicio(reclamacion.numero_servicio.ToString());
   
    }

    protected void ObtenerDatosServicio(String PidObjeto)
    {
       
        Servicio vCuentas = new Servicio();
        vCuentas = ExcluServicios.ConsultarSERVICIO(Convert.ToInt64(PidObjeto), (Usuario)Session["usuario"]);

        txtCodigo.Text = Convert.ToString(vCuentas.numero_servicio);
        if (vCuentas.nombre != null)
            txtNomPersona.Text = vCuentas.nombre.ToString().Trim();        
        txtNombresFallecido.Text=vCuentas.nombre_fallecido ;
        txtIdentificacionTitu.Text=vCuentas.identificacion ;
        ddllineaservicio.SelectedValue = Convert.ToString(vCuentas.cod_linea_servicio);
        txtidentificacionfallecido.Text= vCuentas.identificacion_fallecido ;
        txtfechasolicitud.Text = Convert.ToString(vCuentas.fecha_solicitud);
        txtTipoIdentificacion.Text = Convert.ToString(vCuentas.tipo_identificacion);

        List<DetalleServicio> lstvCuentasS = new List<DetalleServicio>();
        lstvCuentasS = ExcluServicios.ConsultarDETALLESERVICIO(Convert.ToInt64(PidObjeto), (Usuario)Session["usuario"]);

        gvBeneficiarios.PageSize = 15;
        String emptyQuery = "Fila de datos vacia";
        gvBeneficiarios.EmptyDataText = emptyQuery;
        gvBeneficiarios.DataSource = lstvCuentasS;
        if (lstvCuentasS.Count > 0)
        {
            gvBeneficiarios.DataBind();
            panel1.Visible = true;
            lblInfo.Visible = false;
            lblTotalRegs.Visible = true;
            lblTotalRegs.Text = "<br/> Registros encontrados " + lstvCuentasS.Count.ToString();
        }
        else
        {
            panel1.Visible = false;
            lblTotalRegs.Visible = false;
            lblInfo.Visible = true;
        }               
    }

    private Servicio ObtenerValores()
    {
        Servicio vCuentas = new Servicio();
        if (txtNumServ.Text.Trim() != "")
            vCuentas.numero_servicio = Convert.ToInt32(txtNumServ.Text.Trim());        
        if (ddlLinea.SelectedIndex != 0)
            vCuentas.cod_linea_servicio = ddlLinea.SelectedValue;
        if (txtIdentificacion.Text != "")
            vCuentas.identificacion = txtIdentificacion.Text;
        
        if (txtNombre.Text.Trim() != "")
            vCuentas.nombre = txtNombre.Text.Trim().ToUpper();

        return vCuentas;
    }



    private string obtFiltro(Servicio Cuentas)
    {
        String filtro = String.Empty;

        if (txtNumServ.Text.Trim() != "")
            filtro += " and s.numero_servicio = " + Cuentas.numero_servicio;
        if (ddlLinea.SelectedIndex != 0)
            filtro += " and s.COD_LINEA_SERVICIO = " + Cuentas.cod_linea_servicio;
        if (txtIdentificacion.Text != "")
            filtro += " and p.identificacion like '%" + Cuentas.identificacion + "%'";
        if (txtNombre.Text.Trim() != "")
            filtro += " and p.primer_nombre ||' '|| p.segundo_nombre||' '|| p.primer_apellido||' '||p.segundo_apellido like '%" + Cuentas.nombre + "%'";

        filtro += " and s.estado = 'C'";

        return filtro;
    }


   
}