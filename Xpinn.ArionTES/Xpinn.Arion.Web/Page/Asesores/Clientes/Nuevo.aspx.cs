using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Asesores.Business;
using Xpinn.Asesores.Services;
using Xpinn.Asesores.Entities;
using Xpinn.Asesores.Entities.Common;
using Xpinn.Util;

public partial class AseClienteNuevo : GlobalWeb
{
    Usuario usuario = new Usuario();
    ClientePotencialService serviceCliente = new ClientePotencialService();
    ParametricaService serviceParametrica = new ParametricaService();
    
    Xpinn.Asesores.Services.PersonaService servicepersona = new Xpinn.Asesores.Services.PersonaService(); 
    Zona entityZona = new Zona();
    Xpinn.Asesores.Entities.Common.Actividad entityActividad = new Xpinn.Asesores.Entities.Common.Actividad();
    MotivoAfiliacion entityMotfilia = new MotivoAfiliacion();
    MotivoModificacion entityMotModi = new MotivoModificacion();
    TipoIdentificacion entityTipoIdentificacion = new TipoIdentificacion();

    private void Page_PreInit(object sender, EventArgs evt)
    {
        try
        {
            if (Session[serviceCliente.CodigoPrograma + ".id"] != null)
            {
                VisualizarOpciones(serviceCliente.CodigoPrograma, "E");
            }
            else
            {
                VisualizarOpciones(serviceCliente.CodigoPrograma, "A");
            }
            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(serviceCliente.GetType().Name + "A", "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                ObtenerDatosDropDownList();
                TurnOffMotivos();
                TurnOnTiposBasicos();

                if (Session[serviceCliente.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[serviceCliente.CodigoPrograma + ".id"].ToString();
                    Session.Remove(serviceCliente.CodigoPrograma + ".id");
                    ObtenerDatos(idObjeto);
                    TurnOnMotivoModificacion();
                }
                else
                {
                    TurnOnMotivoAfiliacion();
                }
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(serviceCliente.GetType().Name + "A", "Page_Load", ex);
        }
    }

    private void TurnOnTiposBasicos()
    {
        txtPrimerNombre.Enabled = true;
        txtSegundoNombre.Enabled = true;
        txtPrimerApellido.Enabled = true;
        txtSegundoApellido.Enabled = true;
        ddlTipoDoc.Enabled = true;
        txtNumDoc.Enabled = true;
    }

    private void TurnOffTiposBasicos()
    {
        txtPrimerNombre.Enabled = false;
        txtSegundoNombre.Enabled = false;
        txtPrimerApellido.Enabled = false;
        txtSegundoApellido.Enabled = false;
        ddlTipoDoc.Enabled = false;
        txtNumDoc.Enabled = false;
    }

    private void TurnOffMotivos()
    {
        txtObservaciones.Visible = false;
        ddlMotAfilia.Visible = false;
        lbMotAfili.Visible = false;
        lbObsAfilia.Visible = false;

        txtObservacionModif.Visible = false;
        ddlMotModi.Visible = false;
        lbMotMod.Visible = false;
        lbObsMod.Visible = false;

    }

    private void TurnOnMotivoAfiliacion()
    {
        txtObservaciones.Visible = true;
        ddlMotAfilia.Visible = true;
        lbMotAfili.Visible = true;
        lbObsAfilia.Visible = true;
        MultiView1.SetActiveView(View1);
    }

    private void TurnOnMotivoModificacion()
    {
        txtObservacionModif.Visible = true;
        ddlMotModi.Visible = true;
        lbMotMod.Visible = true;
        lbObsMod.Visible = true;
        MultiView1.SetActiveView(View2);
    }

    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ClientePotencial entityCliente = new ClientePotencial();
            ClientePotencial CLIENTE = new ClientePotencial();
            Persona persona = new Persona();

            if (idObjeto != "")
            entityCliente = serviceCliente.ConsultarCliente(Convert.ToInt64(idObjeto), (Usuario)Session["usuario"]);

           
            CLIENTE = serviceCliente.ConsultarClienteyaExistente(Convert.ToInt64(txtNumDoc.Text), (Usuario)Session["usuario"]);
            persona = servicepersona.ConsultaryaExistente(Convert.ToInt64(txtNumDoc.Text), (Usuario)Session["usuario"]);
            if (CLIENTE.NumeroDocumento == 000 && persona.NumeroDocumento=="000")
            {
            
                entityCliente.PrimerNombre = txtPrimerNombre.Text.Trim();
                entityCliente.SegundoNombre = txtSegundoNombre.Text.Trim() != "" ? txtSegundoNombre.Text.Trim() : null ;
                entityCliente.PrimerApellido = txtPrimerApellido.Text.Trim();
                entityCliente.SegundoApellido = txtSegundoApellido.Text.Trim() != "" ? txtSegundoApellido.Text.Trim() : null;
                entityCliente.NumeroDocumento = Convert.ToInt64(txtNumDoc.Text.Trim());
                entityCliente.Telefono = txtTelefono.Text.Trim();

                entityCliente.RazonSocial = txtRazonSocial.Text.Trim() != "" ? txtRazonSocial.Text.Trim() : null;
                entityCliente.SiglaNegocio = txtSigla.Text.Trim() != ""?  txtSigla.Text.Trim() : null;
                entityCliente.Direccion = txtDirCorrespondencia.Text.Trim();
                entityCliente.FechaRegistro = DateTime.Now;
                entityCliente.Email = txtEmail.Text.Trim();

                entityCliente.TipoIdentificacion.IdTipoIdentificacion = Convert.ToInt64(ddlTipoDoc.SelectedValue.ToString().Trim());
                entityCliente.Zona.IdZona = Convert.ToInt64(ddlZona.SelectedValue.ToString());
                entityCliente.Actividad.IdActividad = Convert.ToInt64(ddlActividad.SelectedValue);
                entityCliente.codasesor = Convert.ToInt64(ddlejecutivo.SelectedValue);
                entityCliente.MotivoAfiliacion.IdMotivoAfiliacion = Convert.ToInt64(ddlMotAfilia.SelectedValue.ToString());
                entityCliente.MotivoAfiliacion.Observaciones = txtObservaciones.Text.Trim();

                if (idObjeto != "")
                {
                    entityCliente.IdCliente = Convert.ToInt64(idObjeto);
                    entityCliente.UsuarioEdita = "Admin";  // Modificar por usuario en sesion
                    serviceCliente.ActualizarCliente(entityCliente, (Usuario)Session["usuario"]);
                }
                else
                {
                    entityCliente.UsuarioCrea = "Admin";  // Crear por usuario en sesion
                    entityCliente = serviceCliente.CrearCliente(entityCliente, (Usuario)Session["usuario"]); //OJOOOO ESTE ES EL QUE DEBERIA SER (Usuario)Session["usuario"]);
                    idObjeto = entityCliente.IdCliente.ToString();
                }
                Navegar(Pagina.Lista);
            }
            else
            {
                Labelerror.Text = "El cliente ya se encuentra registrado en el Sistema ";
            }

        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(serviceCliente.GetType().Name + "A", "btnGuardar_Click", ex);
        }
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        Navegar(Pagina.Lista);
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        if (idObjeto == "")
        {
            Navegar(Pagina.Lista);
        }
        else
        {
            Session[serviceCliente.CodigoPrograma + ".id"] = idObjeto;
            Navegar(Pagina.Detalle);
        }
    }

    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            TurnOffTiposBasicos();

            ClientePotencial entityCliente = new ClientePotencial();
            entityCliente = serviceCliente.ConsultarCliente(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);

            if (!string.IsNullOrEmpty(entityCliente.PrimerNombre)) txtPrimerNombre.Text = entityCliente.PrimerNombre.Trim().ToString();
            if (!string.IsNullOrEmpty(entityCliente.SegundoNombre)) txtSegundoNombre.Text = entityCliente.SegundoNombre.Trim().ToString();
            if (!string.IsNullOrEmpty(entityCliente.PrimerApellido)) txtPrimerApellido.Text = entityCliente.PrimerApellido.Trim().ToString();
            if (!string.IsNullOrEmpty(entityCliente.SegundoApellido)) txtSegundoApellido.Text = entityCliente.SegundoApellido.Trim().ToString();
            if (!entityCliente.NumeroDocumento.Equals(0)) txtNumDoc.Text = entityCliente.NumeroDocumento.ToString();
            if (!string.IsNullOrEmpty(entityCliente.Direccion)) txtDirCorrespondencia.Text = entityCliente.Direccion;
            if (!entityCliente.Telefono.Equals(0)) txtTelefono.Text = entityCliente.Telefono.ToString();
            if (!string.IsNullOrEmpty(entityCliente.Email)) txtEmail.Text = entityCliente.Email;
            if (!string.IsNullOrEmpty(entityCliente.RazonSocial)) txtRazonSocial.Text = entityCliente.RazonSocial;
            if (!string.IsNullOrEmpty(entityCliente.SiglaNegocio)) txtSigla.Text = entityCliente.SiglaNegocio;

           // if (!string.IsNullOrEmpty(entityCliente.Zona.IdZona.ToString())) ddlZona.Items.FindByText(entityCliente.Zona.NombreZona).Selected = true;
            if (!string.IsNullOrEmpty(entityCliente.Actividad.NombreActividad)) ddlActividad.Items.FindByText(entityCliente.Actividad.NombreActividad).Selected = true;
            if (!entityCliente.TipoIdentificacion.IdTipoIdentificacion.Equals(0)) ddlTipoDoc.Items.FindByText(entityCliente.TipoIdentificacion.NombreTipoIdentificacion).Selected = true;

            //VerAuditoria(aseEntCliente.UsuarioCrea, aseEntCliente.FechaCrea, aseEntCliente.UsuarioEdita, aseEntCliente.FechaEdita);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(serviceCliente.GetType().Name + "A", "ObtenerDatos", ex);
        }
    }

    private List<Xpinn.FabricaCreditos.Entities.Persona1> TraerResultadosLista(string ListaSolicitada)
    {
        Xpinn.FabricaCreditos.Services.Persona1Service persona1Servicio = new Xpinn.FabricaCreditos.Services.Persona1Service();
        List<Xpinn.FabricaCreditos.Entities.Persona1> lstDatosSolicitud = new List<Xpinn.FabricaCreditos.Entities.Persona1>();
        lstDatosSolicitud.Clear();
        lstDatosSolicitud = persona1Servicio.ListasDesplegables(ListaSolicitada, (Usuario)Session["Usuario"]);
        return lstDatosSolicitud;
    }

    private void ObtenerDatosDropDownList()
    {
        String ListaSolicitada = null;  // Cadena en la que se indica la lista a solicitar
        List<Xpinn.FabricaCreditos.Entities.Persona1> lstDatosSolicitud = new List<Xpinn.FabricaCreditos.Entities.Persona1>();

        //ddlTipoDoc.DataSource = serviceParametrica.ListarTipoIdentificacion(entityTipoIdentificacion, usuario);        
        
        ddlTipoDoc.DataSource = serviceParametrica.ListarTipoIdentificacion(entityTipoIdentificacion, (Usuario)Session["usuario"]);
        ddlTipoDoc.DataTextField = "NombreTipoIdentificacion";
        ddlTipoDoc.DataValueField = "IdTipoIdentificacion";
        ddlTipoDoc.DataBind();

        usuario = (Usuario)Session["usuario"];

        ddlejecutivo.DataSource = serviceParametrica.listarejecutivoszona(0, (Usuario)Session["usuario"]);
        ddlejecutivo.DataTextField = "NombreCompleto";
        ddlejecutivo.DataValueField = "Codigo";
        ddlejecutivo.DataBind();
        ddlejecutivo.Items.Add(new ListItem("Seleccione un item","0"));
        ddlejecutivo.SelectedValue = "0";

  
        PoblarListaDesplegable("Zonas","COD_ZONA,DESCRIPCION","","2", ddlZona, (Usuario)Session["usuario"]);        

        /*ddlActividad.DataSource = serviceParametrica.ListarActividades(entityActividad, (Usuario)Session["usuario"]);
        ddlActividad.DataTextField = "NombreActividad";
        ddlActividad.DataValueField = "IdActividad";
        ddlActividad.DataBind();
        */
        ListaSolicitada = "Actividad2";
        lstDatosSolicitud = TraerResultadosLista(ListaSolicitada);
        ddlActividad.DataSource = lstDatosSolicitud;
        ddlActividad.DataTextField = "ListaDescripcion";
        ddlActividad.DataValueField = "ListaIdStr";
        ddlActividad.DataBind();

        ddlMotAfilia.DataSource = serviceParametrica.ListarMotivoAfiliacion(entityMotfilia, (Usuario)Session["usuario"]);
        ddlMotAfilia.DataTextField = "Observaciones";
        ddlMotAfilia.DataValueField = "IdMotivoAfiliacion";
        ddlMotAfilia.DataBind();

        ddlMotModi.DataSource = serviceParametrica.ListarMotivoModificacion(entityMotModi, (Usuario)Session["usuario"]);
        ddlMotModi.DataTextField = "NombreMotivoModificacion";
        ddlMotModi.DataValueField = "IdMotivoModificacion";
        ddlMotModi.DataBind();
    }

    public void PoblarListaDesplegable(string pTabla, DropDownList ddlControl, Usuario pUsuario)
    {
        PoblarListaDesplegable(pTabla, "", "", "", ddlControl, pUsuario);
    }

    public void PoblarListaDesplegable(string pTabla, string pColumnas, string pCondicion, string pOrden, DropDownList ddlControl, Usuario pUsuario)
    {
        List<Xpinn.Comun.Entities.ListaDesplegable> plista = new List<Xpinn.Comun.Entities.ListaDesplegable>();
        Xpinn.Comun.Entities.ListaDesplegable pentidad = new Xpinn.Comun.Entities.ListaDesplegable();
        Xpinn.Comun.Services.ListaDeplegableService pservicio = new Xpinn.Comun.Services.ListaDeplegableService();

        ddlControl.Items.Clear();
        plista = pservicio.ListarListaDesplegable(pentidad, pTabla, pColumnas, pCondicion, pOrden, pUsuario);
        pentidad.idconsecutivo = null;
        pentidad.descripcion = "Seleccione un item";
        plista.Insert(0, pentidad);
        ddlControl.DataTextField = "descripcion";
        ddlControl.DataValueField = "idconsecutivo";
        ddlControl.DataSource = plista;
        ddlControl.DataBind();

    }
}//end class