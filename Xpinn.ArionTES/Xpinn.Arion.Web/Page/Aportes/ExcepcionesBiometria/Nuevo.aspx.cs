using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
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
using Xpinn.Ahorros.Entities;
using Xpinn.Ahorros.Services;
using System.Text;
using System.IO;
using System.Web.UI.HtmlControls;

partial class Nuevo : GlobalWeb
{
    private Xpinn.Aportes.Services.PersonaAutorizacionService TerceroServicio = new Xpinn.Aportes.Services.PersonaAutorizacionService();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[TerceroServicio.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(TerceroServicio.CodigoPrograma, "E");
            else
                VisualizarOpciones(TerceroServicio.CodigoPrograma, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
            ctlMensaje.eventoClick += btnContinuarMen_Click;
            toolBar.MostrarConsultar(false);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(TerceroServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
         
            if (!IsPostBack)
            {
                mvAhorroVista.ActiveViewIndex = 0;
                txtFechaExcep.ToDateTime = DateTime.Now;

                CargarListar();
                if (Session[TerceroServicio.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[TerceroServicio.CodigoPrograma + ".id"].ToString();
                    Session.Remove(TerceroServicio.CodigoPrograma + ".id");
                    ObtenerDatos(idObjeto);
                }
                else
                {
                    Navegar(Pagina.Lista);
                }
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(TerceroServicio.CodigoPrograma, "Page_Load", ex);
        }
    }

    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);

    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        if (validardatos() == true)
        {
            ///verifica que todo este llenado
            if (Convert.ToString(ddlMotivoExcep.SelectedItem) == "")
            {
                VerError("Usted no tiene ningun Motivo");
                return;
            }




            ctlMensaje.MostrarMensaje("Desea guardar la excepción de biometria?");
        }
        else 
        {
            VerError("Usted no tiene ningun Motivo");
            return;
        
        }
    }

    private void CargarListar()
    {
        Xpinn.FabricaCreditos.Services.LineasCreditoService linahorroServicio = new Xpinn.FabricaCreditos.Services.LineasCreditoService();
        Xpinn.FabricaCreditos.Entities.LineasCredito linahorroVista = new Xpinn.FabricaCreditos.Entities.LineasCredito();
        ddlMotivoExcep.DataTextField = "descripcion";
        ddlMotivoExcep.DataValueField = "cod_clasifica";
       
        ddlMotivoExcep.DataSource = linahorroServicio.MotivoCredito(linahorroVista, (Usuario)Session["usuario"]);
        if (ddlMotivoExcep.DataSource != Convert.ToString(0))
        {
                ddlMotivoExcep.DataBind();
        }
    }

    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        try
        {
            List<Xpinn.Aportes.Entities.PersonaAutorizacion> lstConsulta = new List<Xpinn.Aportes.Entities.PersonaAutorizacion>();
            Xpinn.Aportes.Entities.PersonaAutorizacion valores = new Xpinn.Aportes.Entities.PersonaAutorizacion();
            ///carga todo a una entodad vAhorroVista en AhorroVista
            Usuario vUsuario = new Usuario();
            vUsuario = (Usuario)Session["Usuario"];

           
            if (idObjeto != "")
            valores.direccion = txtDireccion.Text;
            valores.cod_usuario = Convert.ToInt32(txtUsUARIO.Text);
            valores.estado = 2;
            valores.fecha_excepcion = Convert.ToDateTime(txtFechaExcep.Text);
            valores.idautorizacion = Convert.ToInt64(txtId.Text);
            valores.ip = txtip.Text;
            valores.numero_producto = txtNumeroprod.Text;
            valores.observacion = txtObservaciones.Text;
            valores.primer_apellido = txtApellidos.Text;
            valores.primer_nombre = txtNombres.Text;
            valores.telefono = TXTtELEFONO.Text;
            valores.tipo_identificacion = txtTipoidentif.Text;
            valores.cod_persona = Convert.ToInt64(lblCodPersona.Text);
            if(ddlMotivoExcep.SelectedIndex!=0)
            valores.cod_motivo_excepcion = Convert.ToInt32(ddlMotivoExcep.SelectedIndex);
            valores.observacion = txtObservaciones.Text;
                   


                TerceroServicio.ModificarPersonaAutorizacion(valores, (Usuario)Session["usuario"]);
           

            if (idObjeto != "")
            {
                
              ///  TerceroServicio.ModificarCambioEstados(vAhorroVista, (Usuario)Session["usuario"]);
            }

            mvAhorroVista.ActiveViewIndex = 1;
            Site toolBar = (Site)this.Master;
            toolBar.MostrarGuardar(false);
            toolBar.MostrarCancelar(false);
            toolBar.MostrarConsultar(true);       
           
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(TerceroServicio.CodigoPrograma, "btnGuardar_Click", ex);
        }
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }

    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {

            List<Xpinn.Aportes.Entities.PersonaAutorizacion> lstConsulta = new List<Xpinn.Aportes.Entities.PersonaAutorizacion>();
            Xpinn.Aportes.Entities.PersonaAutorizacion valores = new Xpinn.Aportes.Entities.PersonaAutorizacion();

            valores = TerceroServicio.ConsultarPersonaAutorizacion(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);

            txtip.Text = valores.ip; 
            ddlMotivoExcep.SelectedValue = Convert.ToString(valores.cod_motivo_excepcion) ;
            txtNumeroprod.Text=valores.numero_producto ;
            txtObservaciones.Text=valores.observacion ;
            txtId.Text= Convert.ToString(valores.idautorizacion) ;
            txtUsUARIO.Text =Convert.ToString(valores.cod_usuario) ;
            lblCodPersona.Text = Convert.ToString(valores.cod_persona);
            txtIdentificacion.Text = valores.identificacion;
            txtTipoidentif.Text = valores.tipo_identificacion;
            txtApellidos.Text = valores.primer_apellido;
            txtNombres.Text = valores.primer_nombre;
            TXTtELEFONO.Text = valores.telefono;
            txtDireccion.Text = valores.direccion;
            ddlMotivoExcep.SelectedValue = Convert.ToString(valores.cod_motivo_excepcion);
            txtTipoproducto.Text = Convert.ToString(valores.tipo_producto);


          


            MostrarHuellaFoto(idObjeto);
            if (valores.estado == 2)
            {
                VerError("Excepción Ya Autorizada");
                Site toolBar = (Site)this.Master;
                toolBar.MostrarGuardar(false);
                return;
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(TerceroServicio.CodigoPrograma, "ObtenerDatos", ex);
        }
    }


    protected void MostrarHuellaFoto(String pIdObjeto)
    {
        try
        {
            // Mostrar la información de la huella
            Xpinn.FabricaCreditos.Services.Persona1Service persona1Servicio = new Xpinn.FabricaCreditos.Services.Persona1Service();
            Xpinn.FabricaCreditos.Entities.Persona1 vPersona1 = new Xpinn.FabricaCreditos.Entities.Persona1();
            Xpinn.Aportes.Entities.PersonaAutorizacion valores = new Xpinn.Aportes.Entities.PersonaAutorizacion();
            Usuario pUsuario = new Usuario();
            valores = TerceroServicio.ConsultarPersonaAutorizacion(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);
            pUsuario = (Usuario)Session["usuario"];
          
            
            string pIdObjetos = "huella";
            // Mostrar información de la foto de la persona
            pIdObjetos = Convert.ToString(vPersona1.tipo_persona);
            Int64 pCod_Persona = valores.cod_persona;
            vPersona1.cod_persona = pCod_Persona;
            vPersona1.seleccionar = "Cod_persona";
            vPersona1 = persona1Servicio.ConsultarPersona1Param(vPersona1, (Usuario)Session["usuario"]);
            if (vPersona1.foto != null)
            {
                try
                {
                    imgFoto.ImageUrl = string.Format("Handler.ashx?id={0}", vPersona1.idimagen);
                }
                catch // (Exception ex)
                {
                    // VerError("No pudo abrir archivo con imagen de la persona " + ex.Message);
                }
            }
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }

    }



    protected Boolean validardatos() 
    {
        Boolean motivo = true;
        if (Convert.ToString(ddlMotivoExcep.SelectedItem) == "")
        {
            VerError("No puede grabar sin el motivo de la excepción");
            motivo = false;
        }
        return motivo;
    }




    private void CargarListas()
    {
        ///carga las listas a la session
        Usuario pUsuario = new Usuario();
        pUsuario = (Usuario)Session["Usuario"];
        try
        {
            //ddlProveedor.DataTextField = "nombre";
            //ddlProveedor.DataValueField = "cod_persona";
            //ddlProveedor.DataSource = personaServicio.ListadoPersonas1(persona, pUsuario);
            //ddlProveedor.DataBind();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(TerceroServicio.GetType().Name + "L", "CargarListas", ex);
        }
    }

    #region Titulares

    /// <summary>
    /// Método para instar un detalle en blanco para cuando la grilla no tiene datos
    /// </summary>
    /// <param name="consecutivo"></param>
   

    /// <summary>
    /// Método para cambio de página
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    

    protected void ActualizarDetalle()
    {
       // List<DetalleComprobante> LstDetalleComprobante = new List<DetalleComprobante>();
        // LstDetalleComprobante = (List<DetalleComprobante>)Session["DetalleComprobante"];
        //gvDetMovs.DataSource = LstDetalleComprobante;
        //gvDetMovs.DataBind();
    }



    /// <summary>
    /// Método para borrar un registro de la grilla
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    
    
       
    #endregion

    protected void ddlFormaPago_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

}
