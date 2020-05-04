using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using System.IO;
using System.Configuration;
using System.Globalization;
using System.Text;
using System.Web.UI.HtmlControls;
using Xpinn.Auxilios.Services;
using Xpinn.Auxilios.Entities;


public partial class Nuevo : GlobalWeb
{

    AprobacionAuxilioServices AprobacionServicios = new AprobacionAuxilioServices();
    SolicitudAuxilioServices SolicAuxilios = new SolicitudAuxilioServices();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            if (Session[AprobacionServicios.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(AprobacionServicios.CodigoPrograma, "E");
            else
                VisualizarOpciones(AprobacionServicios.CodigoPrograma, "A");
            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
            ctlMensaje.eventoClick += btnContinuarMen_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AprobacionServicios.GetType().Name + "L", "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                Session["Beneficiario"] = null;
                CargarDropdown();
                txtFechaAprobacion.Text = DateTime.Now.ToString();
                mvAplicar.ActiveViewIndex = 0;
                txtCodigo.Enabled = false;
                txtCupos.Enabled = false;
                txtMontoDisp.Enabled = false;

                if (Session[AprobacionServicios.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[AprobacionServicios.CodigoPrograma + ".id"].ToString();
                    Session.Remove(AprobacionServicios.CodigoPrograma + ".id");
                    ObtenerDatos(idObjeto);
                    Session["TEXTO"] = "modificar";                    
                }
                else
                {
                    Session["TEXTO"] = "grabar";
                    txtFecha.Text = DateTime.Now.ToShortDateString();
                    txtCodigo.Text = SolicAuxilios.ObtenerSiguienteCodigo((Usuario)Session["usuario"]).ToString();
                    InicializargvBeneficiario();
                }
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AprobacionServicios.GetType().Name + "L", "Page_Load", ex);
        }
    }


    void CargarDropdown()
    {
        PoblarLista("lineasauxilios", ddlLinea);
    }

    protected void btnConsultaPersonas_Click(object sender, EventArgs e)
    {
        ctlBusquedaPersonas.Motrar(true, "txtCodPersona", "txtIdPersona", "txtNomPersona");
    }


    protected void InicializargvBeneficiario()
    {
        List<DetalleSolicitudAuxilio> lstDeta = new List<DetalleSolicitudAuxilio>();
        for (int i = gvBeneficiarios.Rows.Count; i < 5; i++)
        {
            DetalleSolicitudAuxilio pDetalle = new DetalleSolicitudAuxilio();
            pDetalle.codbeneficiarioaux = -1;
            pDetalle.identificacion = "";
            pDetalle.nombre = "";
            pDetalle.cod_parentesco = null;
            pDetalle.porcentaje_beneficiario = null;
            lstDeta.Add(pDetalle);
        }
        gvBeneficiarios.DataSource = lstDeta;
        gvBeneficiarios.DataBind();

        Session["Beneficiario"] = lstDeta;
    }

    
    public void PoblarLista(string pTabla, DropDownList ddlControl)
    {
        PoblarLista(pTabla, "", "", "", ddlControl);
    }

    public void PoblarLista(string pTabla, string pColumnas, string pCondicion, string pOrden, DropDownList ddlControl)
    {
        List<Xpinn.Comun.Entities.ListaDesplegable> plista = new List<Xpinn.Comun.Entities.ListaDesplegable>();
        Xpinn.Comun.Entities.ListaDesplegable pentidad = new Xpinn.Comun.Entities.ListaDesplegable();
        Xpinn.Comun.Services.ListaDeplegableService pservicio = new Xpinn.Comun.Services.ListaDeplegableService();

        ddlControl.Items.Clear();
        plista = pservicio.ListarListaDesplegable(pentidad, pTabla, pColumnas, pCondicion, pOrden, (Usuario)Session["usuario"]);
        pentidad.idconsecutivo = null;
        pentidad.descripcion = "Seleccione un item";
        plista.Insert(0, pentidad);
        ddlControl.DataTextField = "descripcion";
        ddlControl.DataValueField = "idconsecutivo";
        ddlControl.DataSource = plista;
        ddlControl.DataBind();
    }


    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            SolicitudAuxilio vDetalle = new SolicitudAuxilio();

            vDetalle = SolicAuxilios.ConsultarAUXILIO(Convert.ToInt32(pIdObjeto), (Usuario)Session["usuario"]);

            if (vDetalle.numero_auxilio != 0)
                txtCodigo.Text = vDetalle.numero_auxilio.ToString().Trim();
            if (vDetalle.fecha_solicitud != DateTime.MinValue)
                txtFecha.Text = vDetalle.fecha_solicitud.ToString(gFormatoFecha).Trim();
            if (vDetalle.cod_persona != 0)
            {
                txtCodPersona.Text = vDetalle.cod_persona.ToString().Trim();
                txtIdPersona.Text = vDetalle.identificacion.ToString().Trim();
                txtNomPersona.Text = vDetalle.nombre.ToString().Trim();
            }
            if (vDetalle.cod_linea_auxilio != "")
                ddlLinea.SelectedValue = vDetalle.cod_linea_auxilio;
            ddlLinea_SelectedIndexChanged(ddlLinea, null);

            if (vDetalle.valor_solicitado != 0)
            {
                txtValorSoli.Text = vDetalle.valor_solicitado.ToString();
                txtValorAproba.Text = vDetalle.valor_solicitado.ToString();
            }
            if (vDetalle.detalle != "")
                txtDetalle.Text = vDetalle.detalle;

            //RECUPERAR DATOS - GRILLA BENEFICIARIO
            List<DetalleSolicitudAuxilio> LstBeneficiario = new List<DetalleSolicitudAuxilio>();
            
            LstBeneficiario = SolicAuxilios.ConsultarDETALLEAuxilio(Convert.ToInt32(pIdObjeto), (Usuario)Session["usuario"]);
            if (LstBeneficiario.Count > 0)
            {
                if ((LstBeneficiario != null) || (LstBeneficiario.Count != 0))
                {
                    gvBeneficiarios.DataSource = LstBeneficiario;
                    gvBeneficiarios.DataBind();
                }
                Session["Beneficiario"] = LstBeneficiario;
            }
            else
            {
                InicializargvBeneficiario();
            }

            SolicitudAuxilio Datos = new SolicitudAuxilio();
            Datos = SolicAuxilios.ListarLineasDauxilios(Convert.ToInt32(ddlLinea.SelectedValue), (Usuario)Session["usuario"]);

            ctlBusquedaProveedor.VisibleCtl = false;
            if (Datos.orden_servicio == 1)
            {
                ctlBusquedaProveedor.VisibleCtl = true;
                //CONSULTAR AUXILIO_ORDEN_SERVICIO
                Auxilio_Orden_Servicio pEntidad = new Auxilio_Orden_Servicio();

                String pFiltro = "WHERE NUMERO_AUXILIO = " + txtCodigo.Text;
                pEntidad = SolicAuxilios.ConsultarAUX_OrdenServicio(pFiltro, (Usuario)Session["usuario"]);
                if (pEntidad.idordenservicio != 0)
                {
                    ctlBusquedaProveedor.CheckedOrd = true;
                    ctlBusquedaProveedor.TextOrdAux = pEntidad.idordenservicio.ToString();

                    if (pEntidad.idproveedor != null && pEntidad.nomproveedor != null)
                        ctlBusquedaProveedor.AsignarDatos(pEntidad.idproveedor, pEntidad.nomproveedor);
                }
                else
                    ctlBusquedaProveedor.CheckedOrd = false;
            }

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AprobacionServicios.CodigoPrograma, "ObtenerDatos", ex);
        }
    }


    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }

    public Boolean ValidarDatos()
    {
        if (txtFechaAprobacion.Text == "")
        {
            VerError("Ingrese la fecha de Aprobación");
            return false;
        }
        if (Convert.ToDateTime(txtFechaAprobacion.Text) > DateTime.Now)
        {
            VerError("La fecha de Aprobación no puede ser superior a la fecha actual");
            return false;
        }
        if (txtValorAproba.Text == "0")
        {
            VerError("Ingrese el Valor aprobado");
            return false;
        }
        if (ctlBusquedaProveedor.VisibleCtl == true)
        {
            if (ctlBusquedaProveedor.CheckedOrd == true)
            {
                if (ctlBusquedaProveedor.TextIdentif == "")
                {
                    VerError("Ingrese la identificacion del proveedor.");
                    return false;
                }
                if (ctlBusquedaProveedor.TextNomProv == "" && ctlBusquedaProveedor.TextIdentif != "")
                {
                    VerError("Ingrese una identificacion válida del proveedor.");
                    return false;
                }
            }
        }
        return true;
    }


    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");       
        if (ValidarDatos())
        {
            ctlMensaje.MostrarMensaje("Desea modificar los Datos Ingresados?");          
        }
    }



    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        try
        {
            AprobacionAuxilio pVar = new AprobacionAuxilio();
            
            //----------------------------------
            pVar.numero_auxilios = Convert.ToInt64(txtCodigo.Text);
            pVar.fecha_aprobacion = DateTime.Now;
            pVar.valor_aprobado = Convert.ToDecimal(txtValorAproba.Text);
            
            if (idObjeto != "")
            {
                int opcion = 0;
                Auxilio_Orden_Servicio AuxOrden = new Auxilio_Orden_Servicio();
                if (ctlBusquedaProveedor.VisibleCtl == true)
                {
                    if (ctlBusquedaProveedor.TextOrdAux != "")
                    {
                        if (ctlBusquedaProveedor.CheckedOrd == true)
                        {  // MODIFICAR
                            Auxilio_Orden_Servicio pEntidad = new Auxilio_Orden_Servicio();
                            String pFiltro = "WHERE NUMERO_AUXILIO = " + txtCodigo.Text;
                            pEntidad = SolicAuxilios.ConsultarAUX_OrdenServicio(pFiltro, (Usuario)Session["usuario"]);

                            AuxOrden.idordenservicio = Convert.ToInt32(ctlBusquedaProveedor.TextOrdAux);
                            AuxOrden.numero_auxilio = Convert.ToInt32(txtCodigo.Text);
                            AuxOrden.cod_persona = Convert.ToInt64(txtCodPersona.Text);
                            AuxOrden.idproveedor = ctlBusquedaProveedor.TextIdentif;
                            AuxOrden.nomproveedor = ctlBusquedaProveedor.TextNomProv;
                            AuxOrden.detalle = pEntidad.detalle;
                            AuxOrden.estado = pEntidad.estado;
                            AuxOrden.numero_preimpreso = pEntidad.numero_preimpreso;
                            opcion = 2;
                        }
                        else
                        {  // ELIMINAR
                            AuxOrden.idordenservicio = Convert.ToInt32(ctlBusquedaProveedor.TextOrdAux);
                            AuxOrden.numero_auxilio = Convert.ToInt32(txtCodigo.Text);
                            opcion = 3;
                        }
                    }
                    else
                    {  // REGISTRAR
                        if (ctlBusquedaProveedor.CheckedOrd == true)
                        {
                            AuxOrden.idordenservicio = Convert.ToInt32(0);
                            AuxOrden.numero_auxilio = Convert.ToInt32(txtCodigo.Text);
                            AuxOrden.cod_persona = Convert.ToInt64(txtCodPersona.Text);
                            AuxOrden.idproveedor = ctlBusquedaProveedor.TextIdentif;
                            AuxOrden.nomproveedor = ctlBusquedaProveedor.TextNomProv;
                            AuxOrden.detalle = null;
                            AuxOrden.estado = 1;
                            opcion = 1;
                        }
                    }
                }

                //MODIFICAR
                AprobacionServicios.AprobarAuxilios(pVar,opcion,AuxOrden, (Usuario)Session["usuario"]);
            }

            AprobacionAuxilio pControl = new AprobacionAuxilio();

            pControl.idcontrolaux = 0;
            pControl.numero_auxilios = pVar.numero_auxilios;
            pControl.codtipo_proceso = 2;
            pControl.fecha_proceso = DateTime.Now;
            //CODIGO DE USUARIO EN CAPA DATOS
            if (txtObservacionAproba.Text != "")
                pControl.observaciones = txtObservacionAproba.Text;
            else
                pControl.observaciones = null;
            AprobacionServicios.CrearControlAuxilio(pControl, (Usuario)Session["usuario"]);

            lblMsj.Text = pVar.numero_auxilios.ToString();
            mvAplicar.ActiveViewIndex = 1;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AprobacionServicios.CodigoPrograma, "btnContinuar_Click", ex);
        }        
    }




    protected List<DetalleSolicitudAuxilio> ObtenerListaBeneficiario()
    {
        try
        {
            List<DetalleSolicitudAuxilio> lstDetalle = new List<DetalleSolicitudAuxilio>();
            List<DetalleSolicitudAuxilio> lista = new List<DetalleSolicitudAuxilio>();

            foreach (GridViewRow rfila in gvBeneficiarios.Rows)
            {
                DetalleSolicitudAuxilio ePogra = new DetalleSolicitudAuxilio();

                Label lblCodigo = (Label)rfila.FindControl("lblCodigo");
                if (lblCodigo != null)
                    ePogra.codbeneficiarioaux = Convert.ToInt32(lblCodigo.Text);
                else
                    ePogra.codbeneficiarioaux = -1;

                TextBoxGrid txtIdenti_Grid = (TextBoxGrid)rfila.FindControl("txtIdenti_Grid");
                if (txtIdenti_Grid.Text != "")
                    ePogra.identificacion = txtIdenti_Grid.Text;

                TextBoxGrid txtNombreComple = (TextBoxGrid)rfila.FindControl("txtNombreComple");
                if (txtNombreComple.Text != "")
                    ePogra.nombre = txtNombreComple.Text;

                DropDownListGrid ddlParentesco = (DropDownListGrid)rfila.FindControl("ddlParentesco");
                if (ddlParentesco.SelectedIndex != 0)
                    ePogra.cod_parentesco = Convert.ToInt32(ddlParentesco.SelectedValue);

                TextBoxGrid txtPorcBene = (TextBoxGrid)rfila.FindControl("txtPorcBene");
                if (txtPorcBene.Text != "")
                    ePogra.porcentaje_beneficiario = Convert.ToDecimal(txtPorcBene.Text);

                lista.Add(ePogra);
                Session["Beneficiario"] = lista;

                if (ePogra.identificacion != null && ePogra.cod_parentesco != 0 && ePogra.porcentaje_beneficiario != 0)
                {
                    lstDetalle.Add(ePogra);
                }
            }
            return lstDetalle;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AprobacionServicios.CodigoPrograma, "ObtenerListaBeneficiario", ex);
            return null;
        }
    }



    protected void btnAdicionarFila_Click(object sender, EventArgs e)
    {
        ObtenerListaBeneficiario();
        List<DetalleSolicitudAuxilio> LstPrograma = new List<DetalleSolicitudAuxilio>();
        if (Session["Beneficiario"] != null)
        {
            LstPrograma = (List<DetalleSolicitudAuxilio>)Session["Beneficiario"];

            for (int i = 1; i <= 1; i++)
            {
                DetalleSolicitudAuxilio pDetalle = new DetalleSolicitudAuxilio();
                pDetalle.codbeneficiarioaux = -1;
                pDetalle.identificacion = "";
                pDetalle.nombre = "";
                pDetalle.cod_parentesco = null;
                pDetalle.porcentaje_beneficiario = null;
                LstPrograma.Add(pDetalle);
            }
            gvBeneficiarios.DataSource = LstPrograma;
            gvBeneficiarios.DataBind();

            Session["Beneficiario"] = LstPrograma;
        }
    }
    protected void gvBeneficiarios_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownListGrid ddlParentesco = (DropDownListGrid)e.Row.FindControl("ddlParentesco");
            if (ddlParentesco != null)
                PoblarLista("parentescos", ddlParentesco);

            Label lblParentesco = (Label)e.Row.FindControl("lblParentesco");
            if (lblParentesco != null)
                ddlParentesco.SelectedValue = lblParentesco.Text;

        }
    }

    protected void gvBeneficiarios_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int conseID = Convert.ToInt32(gvBeneficiarios.DataKeys[e.RowIndex].Values[0].ToString());

        ObtenerListaBeneficiario();

        List<DetalleSolicitudAuxilio> LstDetalle = new List<DetalleSolicitudAuxilio>();
        LstDetalle = (List<DetalleSolicitudAuxilio>)Session["Beneficiario"];

        try
        {
            foreach (DetalleSolicitudAuxilio acti in LstDetalle)
            {
                if (acti.codbeneficiarioaux == conseID)
                {
                    if (conseID > 0)
                        SolicAuxilios.EliminarDETALLEAuxilio(conseID, (Usuario)Session["usuario"]);
                    LstDetalle.Remove(acti);
                    //LstDetalle.RemoveAt((gvBeneficiarios.PageIndex * gvBeneficiarios.PageSize) + e.RowIndex);
                    break;
                }
            }
            Session["Beneficiario"] = LstDetalle;

            gvBeneficiarios.DataSourceID = null;
            gvBeneficiarios.DataBind();
            gvBeneficiarios.DataSource = LstDetalle;
            gvBeneficiarios.DataBind();

        }
        catch (Xpinn.Util.ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AprobacionServicios.CodigoPrograma, "gvProgramacion_RowDeleting", ex);
        }
    }

    protected void txtIdPersona_TextChanged(object sender, EventArgs e)
    {
        Xpinn.Seguridad.Services.UsuarioService UsuarioServicio = new Xpinn.Seguridad.Services.UsuarioService();
        Xpinn.FabricaCreditos.Entities.Persona1 DatosPersona = new Xpinn.FabricaCreditos.Entities.Persona1();

        DatosPersona = UsuarioServicio.ConsultarPersona1(txtIdPersona.Text, (Usuario)Session["usuario"]);

        if (DatosPersona.cod_persona != 0)
        {
            if (DatosPersona.cod_persona != 0)
                txtCodPersona.Text = DatosPersona.cod_persona.ToString();
            if (DatosPersona.identificacion != "")
                txtIdPersona.Text = DatosPersona.identificacion;
            if (DatosPersona.nombre != "")
                txtNomPersona.Text = DatosPersona.nombre;
        }
        else
        {
            txtNomPersona.Text = "";
            txtCodPersona.Text = "";
        }
    }

    protected void ddlLinea_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlLinea.SelectedIndex != 0)
        {
            SolicitudAuxilio Datos = new SolicitudAuxilio();
            Datos = SolicAuxilios.ListarLineasDauxilios(Convert.ToInt32(ddlLinea.SelectedValue), (Usuario)Session["usuario"]);
            if (Datos.cupos != 0)
                txtCupos.Text = Datos.cupos.ToString();
            else
                txtCupos.Text = "";
            if (Datos.monto_maximo != 0)
                txtMontoDisp.Text = Datos.monto_maximo.ToString();
            else
                txtMontoDisp.Text = "";
        }
        else
        {
            txtCupos.Text = "";
            txtMontoDisp.Text = "";           
        }
    }   
   
  
}
