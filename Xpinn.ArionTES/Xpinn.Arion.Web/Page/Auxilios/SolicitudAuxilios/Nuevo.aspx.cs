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
using Xpinn.FabricaCreditos.Services;
using Xpinn.FabricaCreditos.Entities;



public partial class Nuevo : GlobalWeb
{
    PeriodicidadService Perio = new PeriodicidadService();
    SolicitudAuxilioServices SolicAuxilios = new SolicitudAuxilioServices();
    ReporteAuxilioService SoliServicios = new ReporteAuxilioService();
    Xpinn.Auxilios.Services.LineaAuxilioServices LineaAux = new Xpinn.Auxilios.Services.LineaAuxilioServices();
    private Xpinn.Asesores.Services.CreditosService RecaudosMasivosServicio = new Xpinn.Asesores.Services.CreditosService();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            if (Session[SolicAuxilios.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(SolicAuxilios.CodigoPrograma, "E");
            else
                VisualizarOpciones(SolicAuxilios.CodigoPrograma, "A");
            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
            ctlMensaje.eventoClick += btnContinuarMen_Click;
            txtValorMatricula.eventoCambiar += txtValorMatricula_TextChanged;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(SolicAuxilios.GetType().Name + "L", "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                conversion();
                Session["Beneficiario"] = null;
                CargarDropdown();
                mvAplicar.ActiveViewIndex = 0;
                txtCodigo.Enabled = false;
                txtCupos.Enabled = false;
                txtMontoDisp.Enabled = false;

                if (Session[SolicAuxilios.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[SolicAuxilios.CodigoPrograma + ".id"].ToString();
                    Session.Remove(SolicAuxilios.CodigoPrograma + ".id");
                    ObtenerDatos(idObjeto);                    
                    Session["TEXTO"] = "modificar";
                    lblmsj.Text = "modificado";
                }
                else
                {
                    panelRequisitos.Visible = false;
                    Session["TEXTO"] = "grabar";
                    lblmsj.Text = "grabada";
                    txtFecha.Text = DateTime.Now.ToShortDateString();
                    txtCodigo.Text = SolicAuxilios.ObtenerSiguienteCodigo((Usuario)Session["usuario"]).ToString();
                    InicializargvBeneficiario();
                    ddlLinea_SelectedIndexChanged(ddlLinea, null);
                }
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(SolicAuxilios.GetType().Name + "L", "Page_Load", ex);
        }

    }

    void CargarDropdown()
    {
        PoblarLista("lineasauxilios", "", " ESTADO = 1 ","2", ddlLinea);
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

            if (vDetalle.porc_matricula != 0)
                txtValorMatricula.Text = vDetalle.porc_matricula.ToString();

            if (vDetalle.valor_solicitado != 0)
                txtValorSoli.Text = vDetalle.valor_solicitado.ToString();
            if (vDetalle.detalle != "")
                txtDetalle.Text = vDetalle.detalle;

            //RECUPERAR DATOS - GRILLA BENEFICIARIO
            List<DetalleSolicitudAuxilio> LstBeneficiario = new List<DetalleSolicitudAuxilio>();
            List<Requisitos> LstRequisitos = new List<Requisitos>();

            LstRequisitos = SolicAuxilios.CargarDatosRequisitos(Convert.ToInt32(pIdObjeto), (Usuario)Session["usuario"]);
            if (LstRequisitos.Count > 0)
            {
                panelRequisitos.Visible = true;
                gvValidacion.DataSource = LstRequisitos;
                gvValidacion.DataBind();
            }


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
            BOexcepcion.Throw(SolicAuxilios.CodigoPrograma, "ObtenerDatos", ex);
        }
    }


    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }

    public Boolean ValidarDatos()
    {
        if (txtFecha.Text == "")
        {
            VerError("Seleccione la fecha de Solicitud");
            return false;
        }
        if (txtCodPersona.Text == "")
        {
            VerError("Seleccione el Asociado");
            return false;
        }
        if (ddlLinea.SelectedIndex == 0)
        {
            VerError("Seleccione la Linea de Auxilio");
            return false;
        }       
        if (txtValorSoli.Text == "0")
        {
            VerError("Ingrese el Valor Solicitado");
            return false;
        }
        if (Convert.ToDecimal(txtValorSoli.Text) > Convert.ToDecimal(txtMontoDisp.Text))
        {
            VerError("El monto solicitado no puede ser mayor al monto disponible");
            return false;
        }
        if (panelValorMatri.Visible == true)
        {
            if (txtValorMatricula.Text == "0")
            {
                VerError("La linea seleccionada es educativa, debe ingresar el valor de la Matricula.");
                txtValorMatricula.Focus();
                return false;
            }
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
        //Validar Cantidad de Auxilios Por Persona 



        if (txtIdPersona.Text != "")
        {

            List<ReporteAuxilio> lstConsulta = new List<ReporteAuxilio>();
            lstConsulta = SoliServicios.ListarAuxilio(obtFiltro(), DateTime.MinValue, DateTime.MinValue, (Usuario)Session["usuario"]);
            if (lstConsulta.Count > 0)
            {
                foreach (var item in lstConsulta)
                {
                  

                    if (Convert.ToDateTime(txtFecha.Text) < item.fecha_proxima_solicitud)
                    {
                        VerError("La persona " + txtIdPersona.Text + " no puede solicitar auxilios por esta línea, hasta el día:  " + item.fecha_proxima_solicitud.ToShortDateString());
                        txtCupos.Text = "0";
                        return false;

                    }

                }
            }


            LineaAuxilio vDatosLinea = new LineaAuxilio();
            vDatosLinea = LineaAux.ConsultarLineaAUXILIO(ddlLinea.SelectedValue, (Usuario)Session["usuario"]);
            if (vDatosLinea.permite_mora != 1)
            {
                List<Xpinn.Asesores.Entities.PersonaMora> lstConsultas = new List<Xpinn.Asesores.Entities.PersonaMora>();
                lstConsultas = RecaudosMasivosServicio.ListarPersonasMora(Obtenerfiltro(), (Usuario)Session["usuario"]);

                    if (lstConsultas.Count > 0)
                    {
                        VerError("La persona " + txtIdPersona.Text + " no puede solicitar auxilios ya que se encuentra en mora:  ");
                        txtCupos.Text = "0";
                        return false;

                    }
            }

        }


        return true;
    }

    private string Obtenerfiltro()
    {

        string filtro = "";

        if (txtCodPersona.Text != "")
        {
            filtro = "and r.COD_PERSONA = " + txtCodPersona.Text.Trim();
        }

        if (txtIdPersona.Text != "")
        {
            filtro = filtro + "and p.identificacion = '" + txtIdPersona.Text.Trim() + "'";
        }

        return filtro;
    }

    protected void txtNombreComple_ontextchanged(Object sender, EventArgs e) 
    {
        foreach (GridViewRow rfila in gvBeneficiarios.Rows)
        {
            TextBoxGrid nombres = (TextBoxGrid)rfila.FindControl("txtNombreComple");
            nombres.Text = nombres.Text.ToUpper();
        }
    
    }

    protected void conversion() 
    
    {
        foreach (GridViewRow rfila in gvBeneficiarios.Rows)
        {
            TextBoxGrid nombres = (TextBoxGrid)rfila.FindControl("txtNombreComple");
            nombres.Text = nombres.Text.ToUpper();
        }
    
    }


    protected Boolean validarestado(String pIdObjeto)
    {
        Boolean result = true;
        try
        {
            // Determinar datos de la línea
            SolicitudAuxilio lineaAux = new SolicitudAuxilio();
            lineaAux = SolicAuxilios.ListarLineasDauxilios(Convert.ToInt32(ddlLinea.SelectedValue), (Usuario)Session["usuario"]);
            // Determinar datos de la persona
            Int64? codPersona = null;
            if (txtCodPersona.Text != "")
                codPersona = Convert.ToInt64(txtCodPersona.Text);
            result = SolicAuxilios.ConsultarEstadoPersona(codPersona, txtIdPersona.Text, "A", (Usuario)Session["usuario"]);
            if (result == false)
            {
                if (lineaAux.permite_retirados != 1)
                { 
                    VerError("El cliente no tiene un estado activo");
                    result = false;
                }
                else
                {
                    result = true;
                }
            }
            foreach (GridViewRow rfila in gvBeneficiarios.Rows)
            {
                TextBoxGrid identifi = (TextBoxGrid)rfila.FindControl("txtIdenti_Grid");
                TextBoxGrid nombres = (TextBoxGrid)rfila.FindControl("txtNombreComple");
                DropDownListGrid parentesco = (DropDownListGrid)rfila.FindControl("ddlParentesco");
                TextBoxGrid beneficiario = (TextBoxGrid)rfila.FindControl("txtPorcBene");

                if (identifi.Text != "" )
                {
                    if (nombres.Text != "" )
                    {
                        if (parentesco.SelectedValue != "" )
                        {
                            if (beneficiario.Text == "")
                            {
                                VerError("Digite al menos un beneficiario si relleno los campos");
                                result = false;
                            }
                        }
                    }
                }
                else
                {
                    beneficiario.Text = "0";
                }
            } 
            

        }
        catch 
        {
            VerError("El cliente no tiene un estado activo");
            result = false;
        }
        return result;
    }

    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        if (validarestado(idObjeto)==true)
        {
            VerError("");
            if (idObjeto == "")
                txtCodigo.Text = SolicAuxilios.ObtenerSiguienteCodigo((Usuario)Session["usuario"]).ToString();
            if (ValidarDatos())
            {
                ctlMensaje.MostrarMensaje("Desea " + Session["TEXTO"].ToString() + " los Datos Ingresados?");
            }
        }
    }



    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        try
        {
            SolicitudAuxilio pVar = new SolicitudAuxilio();
            if (txtCodigo.Text != "")
                pVar.numero_auxilio = Convert.ToInt32(txtCodigo.Text);
            else
                pVar.numero_auxilio = 0;

            pVar.fecha_solicitud = Convert.ToDateTime(txtFecha.Text);

            if (txtCodPersona.Text != "" && txtIdPersona.Text != "" && txtNomPersona.Text != "")
                pVar.cod_persona = Convert.ToInt64(txtCodPersona.Text);
            pVar.cod_linea_auxilio = ddlLinea.SelectedValue;

            pVar.porc_matricula = 0;
            if (panelValorMatri.Visible == true)
            {
                if (txtValorMatricula.Text != "0")
                {
                    pVar.porc_matricula = Convert.ToDecimal(txtValorMatricula.Text.Replace(".",""));
                }
            }
            pVar.valor_solicitado = Convert.ToDecimal(txtValorSoli.Text);

            
            //DATOS NULOS 
            //----------------------------------
            pVar.fecha_aprobacion = DateTime.MinValue;
            pVar.valor_aprobado = 0;
            pVar.fecha_desembolso = DateTime.MinValue;
            pVar.estado = "S";
            pVar.numero_radicacion = null;
            //----------------------------------
            
            if (txtDetalle.Text != "")
                pVar.detalle = txtDetalle.Text;
            else
                pVar.detalle = null;

            int Opcion = 0;

            Auxilio_Orden_Servicio AuxOrden = new Auxilio_Orden_Servicio();
            // Determinar si la línea de auxilio genera una orden de servicio
            LineaAuxilio vDatosLinea = new LineaAuxilio();
            vDatosLinea = LineaAux.ConsultarLineaAUXILIO(ddlLinea.SelectedValue, (Usuario)Session["usuario"]);
            if (vDatosLinea.orden_servicio == 1)
            {
                if (ctlBusquedaProveedor.VisibleCtl == true)
                {
                    if (ctlBusquedaProveedor.TextOrdAux != "" && idObjeto != "")
                    {
                        if (ctlBusquedaProveedor.CheckedOrd == true)
                        { // MODIFICAR
                            //CONSULTAR AUXILIO_ORDEN_SERVICIO
                            Auxilio_Orden_Servicio pEntidad = new Auxilio_Orden_Servicio();

                            String pFiltro = "WHERE NUMERO_AUXILIO = " + txtCodigo.Text;
                            pEntidad = SolicAuxilios.ConsultarAUX_OrdenServicio(pFiltro, (Usuario)Session["usuario"]);

                            AuxOrden.idordenservicio = Convert.ToInt32(ctlBusquedaProveedor.TextOrdAux);
                            AuxOrden.numero_auxilio = Convert.ToInt32(0); //SI ES EDICION EN LA CAPA BUSINEES SE MODIFICA EL NUMERO
                            AuxOrden.cod_persona = Convert.ToInt64(txtCodPersona.Text);
                            AuxOrden.idproveedor = ctlBusquedaProveedor.TextIdentif;
                            AuxOrden.nomproveedor = ctlBusquedaProveedor.TextNomProv;
                            if (pEntidad.idordenservicio != 0)
                            {
                                AuxOrden.detalle = pEntidad.detalle;
                                AuxOrden.estado = pEntidad.estado;
                                AuxOrden.numero_preimpreso = pEntidad.numero_preimpreso;
                            }
                            else
                            {
                                AuxOrden.detalle = null;
                                AuxOrden.estado = 1;
                                AuxOrden.numero_preimpreso = null;
                            }
                            Opcion = 2;
                        }
                        else
                        { // ELIMINAR
                            AuxOrden.idordenservicio = Convert.ToInt32(ctlBusquedaProveedor.TextOrdAux);
                            Opcion = 3;
                        }
                    }
                    else
                    { // CREAR
                        AuxOrden.idordenservicio = Convert.ToInt32(0);
                        AuxOrden.numero_auxilio = Convert.ToInt32(0); //SI ES EDICION EN LA CAPA BUSINEES SE MODIFICA EL NUMERO
                        AuxOrden.cod_persona = Convert.ToInt64(txtCodPersona.Text);
                        AuxOrden.idproveedor = ctlBusquedaProveedor.TextIdentif;
                        AuxOrden.nomproveedor = ctlBusquedaProveedor.TextNomProv;
                        AuxOrden.detalle = null;
                        AuxOrden.estado = 1;
                        AuxOrden.numero_preimpreso = null;
                        Opcion = 1;
                    }
                }
            }

            pVar.lstValidacion = new List<Requisitos>();
            pVar.lstValidacion = ObtenerListaRequisitos();

            pVar.lstDetalle = new List<DetalleSolicitudAuxilio>();
            pVar.lstDetalle = ObtenerListaBeneficiario();


            if (idObjeto != "")
            {
                //MODIFICAR
                SolicAuxilios.ModificarSolicitudAuxilio(pVar,AuxOrden,Opcion, (Usuario)Session["usuario"]);
            }
            else
            {
               
                    SolicAuxilios.CrearSolicitudAuxilio(pVar,AuxOrden, (Usuario)Session["usuario"]);
           
               

            }

            Site toolBar = (Site)Master;
            toolBar.MostrarGuardar(false);
            lblNroMsj.Text = pVar.numero_auxilio.ToString();
            mvAplicar.ActiveViewIndex = 1;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(SolicAuxilios.CodigoPrograma, "btnContinuar_Click", ex);
        }        
    }

    protected List<Requisitos> ObtenerListaRequisitos()
    {
        try
        {
            List<Requisitos> lstDetalle = new List<Requisitos>();
            
            foreach (GridViewRow rfila in gvValidacion.Rows)
            {
                Requisitos ePogra = new Requisitos();

                Label lblCodigo = (Label)rfila.FindControl("lblCodigo");
                if (lblCodigo != null)
                    ePogra.codrequisitoauxilio = Convert.ToInt32(lblCodigo.Text);
                else
                    ePogra.codrequisitoauxilio = -1;

                Label lblCodRequisito = (Label)rfila.FindControl("lblCodRequisito");
                if (lblCodRequisito != null)
                    ePogra.codrequisitoaux = Convert.ToInt32(lblCodRequisito.Text);

                CheckBoxGrid chkAceptado = (CheckBoxGrid)rfila.FindControl("chkAceptado");
                if (chkAceptado.Checked)
                    ePogra.aceptado = 1;
                else
                    ePogra.aceptado = 0;


                if (ePogra.codrequisitoaux != null && ePogra.numero_auxilio >= 0)
                {
                    lstDetalle.Add(ePogra);
                }
            }            
            return lstDetalle;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(SolicAuxilios.CodigoPrograma, "ObtenerListaRequisitos", ex);
            return null;
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
            BOexcepcion.Throw(SolicAuxilios.CodigoPrograma, "ObtenerListaBeneficiario", ex);          
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
        if (conseID > 0)
        {
            try
            {
                foreach (DetalleSolicitudAuxilio acti in LstDetalle)
                {
                    if (acti.codbeneficiarioaux == conseID)
                    {
                        SolicAuxilios.EliminarDETALLEAuxilio(conseID, (Usuario)Session["usuario"]);
                        LstDetalle.Remove(acti);
                        break;
                    }
                }
            }
            catch (Xpinn.Util.ExceptionBusiness ex)
            {
                VerError(ex.Message);
            }
            catch (Exception ex)
            {
                BOexcepcion.Throw(SolicAuxilios.CodigoPrograma, "gvProgramacion_RowDeleting", ex);
            }
        }
        else
        {
            LstDetalle.RemoveAt((gvBeneficiarios.PageIndex * gvBeneficiarios.PageSize) + e.RowIndex);
        }
        Session["Beneficiario"] = LstDetalle;

        gvBeneficiarios.DataSourceID = null;
        gvBeneficiarios.DataBind();
        gvBeneficiarios.DataSource = LstDetalle;
        gvBeneficiarios.DataBind();
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
        VerError("");
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

            if (Datos.educativo == 1)
            {
                panelValorMatri.Visible = true;
                txtValorSoli.Enabled = false;
                if (Datos.porc_matricula != 0)
                    lblPorcMATRI.Text = Datos.porc_matricula.ToString();
            }
            else
            {
                panelValorMatri.Visible = false;
                lblPorcMATRI.Text = "";
                txtValorSoli.Enabled = true;
            }
            ctlBusquedaProveedor.VisibleCtl = false;
            if (Datos.orden_servicio == 1)
            {
                ctlBusquedaProveedor.VisibleCtl = true;
                ctlBusquedaProveedor.CheckedOrd = true;
            }
            List<Requisitos> lstRequisitos = new List<Requisitos>();
            lstRequisitos = SolicAuxilios.ConsultarValidacionRequisitos(Convert.ToInt32(ddlLinea.SelectedValue), (Usuario)Session["usuario"]);

            if (lstRequisitos.Count > 0)
            {
                panelRequisitos.Visible = true;
                gvValidacion.DataSource = lstRequisitos;
                gvValidacion.DataBind();
            }
            else
            {
                gvValidacion.DataSource = null;
                gvValidacion.DataBind();
                panelRequisitos.Visible = false;
            }
        }
        else
        {
            txtCupos.Text = "";
            txtMontoDisp.Text = "";
            gvValidacion.DataSource = null;
            gvValidacion.DataBind();
            panelRequisitos.Visible = false;
            panelValorMatri.Visible = false;
            ctlBusquedaProveedor.VisibleCtl = false;
            lblPorcMATRI.Text = "";
        }


        //Validar Cantidad de Auxilios Por Persona 

      
    }


    private string obtFiltro()
    {

        String filtros = String.Empty;       
        filtros = "";
       
            filtros += "and auxilios.estado='D'";

      //  if (txtFechaReporte.Text != "")
      //  {
       //     Configuracion conf = new Configuracion();
         //   filtros += " and AUXILIOS.fecha_desembolso = To_Date('" + Convert.ToDateTime(txtFechaReporte.Text).ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "')";
      //  }

        if (txtIdPersona.Text != "")
        {
            filtros += "and persona.identificacion= '" + txtIdPersona.Text + "'";
        }
     

        if (ddlLinea.SelectedIndex != 0)
            filtros += "and LINEASAUXILIOS.cod_linea_auxilio= '" + ddlLinea.SelectedValue + "'";

     

        return filtros;
    }

    protected void txtValorMatricula_TextChanged(object sender, EventArgs e)
    {
        try
        {
            decimal valor = 0;
            lblPorcMATRI.Text = lblPorcMATRI.Text != "" ? lblPorcMATRI.Text : "0";
            valor = Convert.ToDecimal(txtValorMatricula.Text) * (Convert.ToDecimal(lblPorcMATRI.Text) / 100);
            txtValorSoli.Text = valor.ToString("n0");
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }

}
