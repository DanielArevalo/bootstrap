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
using Xpinn.Servicios.Services;
using Xpinn.Servicios.Entities;
using Xpinn.Aportes.Entities;
using Xpinn.Aportes.Services;

public partial class Nuevo : GlobalWeb
{

    SolicitudServiciosServices SolicServicios = new SolicitudServiciosServices();
    LineaServiciosServices BOLineaServ = new LineaServiciosServices();
    PlanesTelefonicosService PlanServic = new PlanesTelefonicosService();


    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            if (Session[SolicServicios.CodigoPrograma + ".id"] != null)
                VisualizarOpciones("170804", "E");
            else
                VisualizarOpciones("170804", "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
            ctlMensaje.eventoClick += btnContinuarMen_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(SolicServicios.GetType().Name + "L", "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {


            if (!IsPostBack)
            {
                mvAplicar.ActiveViewIndex = 0;


                if (Session[SolicServicios.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[SolicServicios.CodigoPrograma + ".id"].ToString();
                    Session.Remove(SolicServicios.CodigoPrograma + ".id");
                    ObtenerDatos(idObjeto);

                    Session["TEXTO"] = "modificar";
                    lblmsj.Text = "registrada";
                }
                else
                {
                    Session["TEXTO"] = "grabar";
                    lblmsj.Text = "grabada";
                    txtFecha.Text = DateTime.Now.ToShortDateString();
                }

            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(SolicServicios.GetType().Name + "L", "Page_Load", ex);
        }

    }



    protected void btnConsultaPersonas_Click(object sender, EventArgs e)
    {
        ctlBusquedaPersonas.Mostrar(true, "txtCodPersona", "txtIdPersona", "txtNomPersona", "txtIdentificacionTitu", "txtNombreTit");
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

    protected Boolean validarestado(String pCodPesona, string pIdentificacion)
    {
        VerError("");
        Boolean result = true;
        try
        {
            Int64? codigo = null;
            if (pCodPesona.Trim() != "")
                codigo = Convert.ToInt64(pCodPesona);
            result = SolicServicios.ConsultarEstadoPersona(codigo, pIdentificacion, "A", (Usuario)Session["usuario"]);
            if (result == false)
            {
                VerError("El cliente no tiene un estado activo");
                result = false;
            }
        }
        catch
        {
            VerError("El cliente no tiene un estado activo");
            result = false;
        }
        return result;
    }

    void ValidarPersonaVacaciones(Int64 pCod_Persona)
    {
        if (pCod_Persona == 0)
            return;
        Xpinn.Tesoreria.Services.EmpresaNovedadService RecaudoService = new Xpinn.Tesoreria.Services.EmpresaNovedadService();
        Xpinn.Tesoreria.Entities.EmpresaNovedad pPersonaVac = new Xpinn.Tesoreria.Entities.EmpresaNovedad();
        string pFiltro = " where vac.cod_persona = " + pCod_Persona + " order by vac.fecha_novedad desc";
        pPersonaVac = RecaudoService.ConsultarPersonaVacaciones(pFiltro, Usuario);

        if (pPersonaVac != null)
        {
            if (pPersonaVac.cod_persona > 0)
            {
                if (pPersonaVac.fechacreacion != null && pPersonaVac.fecha_inicial != null && pPersonaVac.fecha_final != null)
                {
                    DateTime pFechaActual = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                    if (pPersonaVac.fechacreacion <= pFechaActual && pPersonaVac.fecha_final >= pFechaActual)
                    {
                        VerError("La persona tiene un periodo de vacaciones del [ " + Convert.ToDateTime(pPersonaVac.fecha_inicial).ToString(gFormatoFecha) + " al " + Convert.ToDateTime(pPersonaVac.fecha_final).ToString(gFormatoFecha) + " ]");
                        RegistrarPostBack();
                    }
                }
            }
        }
    }


    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            PlanTelefonico vDetalle = new PlanTelefonico();
            vDetalle = PlanServic.ConsultarLineaTelefonica(pIdObjeto, (Usuario)Session["usuario"]);

            if (vDetalle.fecha_solicitud != null)
                txtFecha.Text = Convert.ToString(vDetalle.fecha_solicitud);
            if (vDetalle.cod_titular != null)
                txtCodPersona.Text = Convert.ToString(vDetalle.cod_titular);
            if (vDetalle.identificacion_titular != null)
                txtIdPersona.Text = Convert.ToString(vDetalle.identificacion_titular);
            if (vDetalle.nombre_titular != null)
                txtNomPersona.Text = Convert.ToString(vDetalle.nombre_titular);
            if (vDetalle.num_linea_telefonica != null)
                txtNumeroLineaTel.Text = Convert.ToString(vDetalle.num_linea_telefonica);
            if (vDetalle.identificacion_titular != null)
                txtIdentificacionTitu.Text = Convert.ToString(vDetalle.identificacion_titular);
            if (vDetalle.nombre_titular != null)
                txtNombreTit.Text = Convert.ToString(vDetalle.nombre_titular);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(SolicServicios.CodigoPrograma, "ObtenerDatos", ex);
        }
    }



    protected void txtIdPersona_TextChanged(object sender, EventArgs e)
    {
        Xpinn.Seguridad.Services.UsuarioService UsuarioServicio = new Xpinn.Seguridad.Services.UsuarioService();
        Xpinn.FabricaCreditos.Entities.Persona1 DatosPersona = new Xpinn.FabricaCreditos.Entities.Persona1();

        DatosPersona = UsuarioServicio.ConsultarPersona1(txtIdPersona.Text, (Usuario)Session["usuario"]);

        if (DatosPersona.cod_persona != 0)
        {
            txtCodPersona.Text = DatosPersona.cod_persona.ToString();

            if (DatosPersona.identificacion != "")
            {
                txtIdPersona.Text = DatosPersona.identificacion;
            }
            if (DatosPersona.nombre != "")
            {
                txtNomPersona.Text = DatosPersona.nombre;
            }
            ValidarPersonaVacaciones(DatosPersona.cod_persona);
        }
        else
        {
            txtNomPersona.Text = "";
            txtCodPersona.Text = "";
            txtIdPersona.Text = "";
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
            txtFecha.Focus();
            return false;
        }
        if (txtCodPersona.Text == "")
        {
            VerError("Seleccione el Solicitante");
            txtIdPersona.Focus();
            return false;
        }

        if (txtIdentificacionTitu.Text == "")
        {
            VerError("Ingrese la Identificación del titular");
            txtIdPersona.Focus();
            return false;
        }
        if (txtNombreTit.Text == "")
        {
            VerError("Ingrese el nombre del titular");
            txtIdPersona.Focus();
            return false;
        }

        if (txtNumeroLineaTel.Text == "")
        {
            VerError("Ingrese el número de la Linea Telefonica");
            txtNumeroLineaTel.Focus();
            return false;
        }


        if (txtValorRep.Text == "")
        {
            VerError("Ingrese el Valor de la cuota");
            txtValorRep.Focus();
            return false;
        }

        return true;
    }




    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");

        if (validarestado(txtCodPersona.Text, txtIdPersona.Text) == true)
        {
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
            PlanTelefonico dtllplan = new PlanTelefonico();

            if (validarestado(txtCodPersona.Text, txtIdPersona.Text) == true)
            {

                //Fecha Traspaso
                if (txtFecha.Text != "")
                {
                    dtllplan.fecha_solicitud = Convert.ToDateTime(txtFecha.Text);
                    dtllplan.fecha_activacion = Convert.ToDateTime(txtFecha.Text);
                    dtllplan.fecha_ult_reposicion = Convert.ToDateTime(txtFecha.Text);
                }
                //datos solicitante o titular
                if (txtCodPersona.Text != "")
                {
                    dtllplan.cod_titular = Convert.ToInt64(txtCodPersona.Text);
                }

                if (txtIdPersona.Text != "")
                {
                    dtllplan.identificacion_titular = txtIdPersona.Text;
                }

                if (txtNomPersona.Text != "")
                {
                    dtllplan.nombre_titular = txtNomPersona.Text;
                }

                //Numero Linea Telefonica

                if (txtNumeroLineaTel.Text != "")
                {
                    dtllplan.num_linea_telefonica = txtNumeroLineaTel.Text;
                }


                if (txtValorRep.Text != "")
                {
                    dtllplan.valor_reposicion = Convert.ToDecimal(txtValorRep.Text);
                }

                if (txtobservaciones.Text != "")
                {
                    dtllplan.observaciones = Convert.ToString(txtobservaciones.Text);
                }


            }


            //Int64 COD_OPE = 0;
            if (idObjeto != "")
            {
                //MODIFICAR
                PlanServic.Reposicion(dtllplan, Usuario);
                // PlanServic.ModificarLineaTelefonica(dtllplan, Usuario);

            }
            else
            {
                VerError("No se cargo con exito la Reposicion");
            }


            Site toolbar = (Site)Master;
            toolbar.MostrarGuardar(false);
            lblNroMsj.Text = dtllplan.num_linea_telefonica.ToString();
            btnDesembolso.Visible = false;
            mvAplicar.ActiveViewIndex = 1;

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(SolicServicios.CodigoPrograma, "btnContinuar_Click", ex);
        }
    }


    protected void btnDesembolso_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Page/Servicios/DesembolsoServicios/Lista.aspx?num_serv=" + lblNroMsj.Text);
    }

}
