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
            txtcodcontratacion.Enabled = false;

            if (Session[service.CodigoPrograma + ".id"] != null)
            {
                idObjeto = Session[service.CodigoPrograma + ".id"].ToString();
                ObtenerDatos();
            }
        }
    }

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            if (Session[service.CodigoPrograma + ".id"] == null)
            {
                VisualizarOpciones(service.CodigoPrograma, "A");
            }
            else
            {
                VisualizarOpciones(service.CodigoPrograma, "E");
            }

            Site toolBar = (Site)Master;
            toolBar.eventoGuardar += btn_guardar_click;
            toolBar.eventoLimpiar += btnlimpiar_Click;
            toolBar.eventoCancelar += (s, evt) => Navegar("Lista.aspx");
            toolBar.eventoRegresar += (s, evt) =>
            {
                Session.Remove(service.CodigoPrograma + ".id");
                Navegar("Lista.aspx");
            };
            toolBar.MostrarRegresar(false);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(service.CodigoPrograma, "Page_PreInit", ex);
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
        txtcodcontratacion.Text = string.Empty;
    }

    void GuardarDatos()
    {
        try
        {
            Xpinn.Nomina.Services.TipoContratoService cservice = new Xpinn.Nomina.Services.TipoContratoService();
            Xpinn.Nomina.Entities.TipoContrato entities = new Xpinn.Nomina.Entities.TipoContrato();

            if (idObjeto != "")
            {
                entities = service.ConsultarContratacion(Convert.ToInt64(idObjeto), Usuario);
            }
            else
            {
                if (txtcodcontratacion.Text.Trim() != "")
                {
                    entities = service.ConsultarContratacion(Convert.ToInt64(idObjeto), Usuario);
                    if (entities.cod_contratacion != 0)
                    {
                        VerError("Ya existe una entidad con la misma identificacion");
                        return;
                    }
                }
            }

            if (txtcodcontratacion.Text != "")
                entities.cod_contratacion = Convert.ToInt64(txtcodcontratacion.Text.Trim());

            if (txtcodcontrato.Text != "")
                entities.cod_contrato = Convert.ToInt64(txtcodcontrato.Text.Trim());

            if (txtdescripcion.Text != "")
                entities.tipo_contrato = Convert.ToString(txtdescripcion.Text);

            if (ddldiahabil.SelectedValue != "")
                entities.dia_habil = Convert.ToString(ddldiahabil.Text);

            if(idObjeto == "")
            {
                TipoContrato contrato = cservice.CrearContratacion(entities, Usuario);
                entities.cod_contratacion = contrato.cod_contratacion;
            }
            else
            {
                TipoContrato contrato = cservice.ModificarContratacion(entities, Usuario);
            }

            if (entities.cod_contratacion != 0)
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
            VerError("Error al guardar el registro, " + ex.Message);
        }
    }

 
    protected void ObtenerDatos()
        {
            try
            {
            Xpinn.Nomina.Services.TipoContratoService cservice = new Xpinn.Nomina.Services.TipoContratoService();
            Xpinn.Nomina.Entities.TipoContrato entities = new Xpinn.Nomina.Entities.TipoContrato();

            entities.cod_contratacion = Convert.ToInt64(idObjeto);
            entities = cservice.ConsultarContratacion(entities.cod_contratacion, (Usuario)Session["usuario"]);
            var id = entities.cod_contratacion;


            if(entities != null)
            {

                if (entities.cod_contratacion != Int64.MinValue)
                   txtcodcontratacion.Text = HttpUtility.HtmlDecode(entities.cod_contratacion.ToString().Trim());

                if (entities.cod_contrato != Int64.MinValue)
                   txtcodcontrato.Text = HttpUtility.HtmlDecode(entities.cod_contrato.ToString().Trim());

                if (!string.IsNullOrEmpty(entities.tipo_contrato.ToString()))
                    txtdescripcion.Text = HttpUtility.HtmlDecode(entities.tipo_contrato.ToString().Trim());

                if (!string.IsNullOrEmpty(entities.dia_habil.ToString()))
                    ddldiahabil.Text = HttpUtility.HtmlDecode(entities.dia_habil.ToString().Trim());
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
            string.IsNullOrWhiteSpace(txtcodcontrato.Text))
        {
            VerError("El Codigo del contrato esta vacio");
        }
        return true;
    }


    TipoContrato ObtenerContrato()
    {
        TipoContrato pcontrato = new TipoContrato();
        if (!String.IsNullOrWhiteSpace(idObjeto))
        {
            pcontrato.cod_contratacion = Consultarcontrato(Convert.ToInt64(idObjeto)).cod_contratacion;
        }

        pcontrato.cod_contratacion = Convert.ToInt64(txtcodcontratacion.Text);
        pcontrato.cod_contrato = Convert.ToInt64(txtcodcontrato.Text);
        pcontrato.tipo_contrato = txtdescripcion.Text;
        pcontrato.dia_habil = ddldiahabil.SelectedValue;

        return pcontrato;
    }

    TipoContrato Consultarcontrato(Int64 codcontratacion)
    {
        try
        {
            if (!string.IsNullOrWhiteSpace(codcontratacion.ToString())) return null;
            TipoContrato entitie = service.ConsultarContratacion(Convert.ToInt64(codcontratacion), Usuario);
            return entitie;
        }
        catch (Exception ex)
        {
            VerError("Error al consultar la nomina, " + ex.Message);
            return null;
        }
    }






















}