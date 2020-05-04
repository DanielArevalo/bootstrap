using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Nomina.Entities;
using Xpinn.Nomina.Services;
using Xpinn.Util;

public partial class Nuevo : GlobalWeb
{
    TipoContratoService service = new TipoContratoService();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           

            if (Session[service.CodigoProgramaTiposRetiroContrato + ".id"] != null)
            {
                idObjeto = Session[service.CodigoProgramaTiposRetiroContrato + ".id"].ToString();
                ObtenerDatos();
            }
        }
    }

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            if (Session[service.CodigoProgramaTiposRetiroContrato + ".id"] == null)
            {
                VisualizarOpciones(service.CodigoProgramaTiposRetiroContrato, "A");
            }
            else
            {
                VisualizarOpciones(service.CodigoProgramaTiposRetiroContrato, "E");
            }

            Site toolBar = (Site)Master;
            toolBar.eventoGuardar += btn_guardar_click;
            toolBar.eventoLimpiar += btnlimpiar_Click;
            toolBar.eventoCancelar += (s, evt) => Navegar("Lista.aspx");
            toolBar.eventoRegresar += (s, evt) =>
            {
                Session.Remove(service.CodigoProgramaTiposRetiroContrato + ".id");
                Navegar("Lista.aspx");
            };
            toolBar.MostrarRegresar(false);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(service.CodigoProgramaTiposRetiroContrato, "Page_PreInit", ex);
        }

    }


    void btn_guardar_click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        if (ValidarCampos())
        {
            GuardarDatos();
        }
    }

    protected void btnlimpiar_Click(object sender, EventArgs e)
    {
        txtconsecutivo.Text = string.Empty;
        txtdescripcion.Text = string.Empty;
    }

    void GuardarDatos()
    {
        try
        {
            Xpinn.Nomina.Services.TipoContratoService cservice = new Xpinn.Nomina.Services.TipoContratoService();
            Xpinn.Nomina.Entities.TipoContrato entities = new Xpinn.Nomina.Entities.TipoContrato();

                      
            if (txtconsecutivo.Text != "")
                entities.consecutivo = Convert.ToInt64(txtconsecutivo.Text.Trim());

            if (txtdescripcion.Text != "")
                entities.descripciontiporetiro = Convert.ToString(txtdescripcion.Text.Trim());


            if (idObjeto == "")
            {
                TipoContrato tiporetiro = cservice.CrearTipoRetirocontrato(entities, Usuario);
                entities.consecutivo = tiporetiro.consecutivo;
            }
            else
            {
                TipoContrato tiporetiro = cservice.ModificarTipoRetirocontrato(entities, Usuario);
            }

            if (entities.consecutivo != 0)
            {
                Site toolBar = (Site)Master;
                toolBar.MostrarRegresar(true);
                toolBar.MostrarCancelar(false);
                toolBar.MostrarGuardar(false);
                toolBar.MostrarLimpiar(false);
               
                Session.Remove(service.CodigoPrograma + ".id");
                mvPrincipal.SetActiveView(viewGuardado);
            }
          mvPrincipal.ActiveViewIndex = 1;
        }
        catch (Exception ex)
        {
            VerError("Error al guardar/modificar el registro, " + ex.Message);
        }
    }

 
    protected void ObtenerDatos()
        {
            try
            {
            Xpinn.Nomina.Services.TipoContratoService cservice = new Xpinn.Nomina.Services.TipoContratoService();
            Xpinn.Nomina.Entities.TipoContrato entities = new Xpinn.Nomina.Entities.TipoContrato();

            entities.consecutivo = Convert.ToInt64(idObjeto);
            entities = cservice.ConsultarTipoRetiroContrato(entities.consecutivo, (Usuario)Session["usuario"]);
            var id = entities.consecutivo;


            if(entities != null)
            {
                if (entities.consecutivo != Int64.MinValue)
                   txtconsecutivo.Text = HttpUtility.HtmlDecode(entities.consecutivo.ToString().Trim());

                if (!string.IsNullOrEmpty(entities.descripciontiporetiro.ToString()))
                    txtdescripcion.Text = HttpUtility.HtmlDecode(entities.descripciontiporetiro.ToString().Trim());

                
            }

        }
            catch (Exception ex)
            {
                VerError(ex.Message);
            }
    }


    bool ValidarCampos()
    {
        Page.Validate();
        if (!Page.IsValid ||
            string.IsNullOrWhiteSpace(txtdescripcion.Text))
        {
            VerError("La descripción esta vacia");
        }
        return true;
    }   



}