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
    TipoCotizanteService service = new TipoCotizanteService();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           

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
        txtconsecutivo.Text = string.Empty;
        txtdescripcion.Text = string.Empty;
    }

    void GuardarDatos()
    {
        try
        {
            Xpinn.Nomina.Services.TipoCotizanteService cservice = new Xpinn.Nomina.Services.TipoCotizanteService();
            Xpinn.Nomina.Entities.TipoCotizante entities = new Xpinn.Nomina.Entities.TipoCotizante();

                      
            if (txtconsecutivo.Text != "")
                entities.consecutivo = Convert.ToInt64(txtconsecutivo.Text.Trim());

            if (txtdescripcion.Text != "")
                entities.descripcion = Convert.ToString(txtdescripcion.Text.Trim());

            if (txtPorcentajeSalud.Text != "")
                entities.porcentaje_salud = ConvertirStringToDecimal(txtPorcentajeSalud.Text);  

            if (txtPorcentajePension.Text != "")
                entities.porcentaje_pension = ConvertirStringToDecimal(txtPorcentajePension.Text);

            if(rdbTransSi.Checked)
            {
                entities.paga_subsidio = 1;
            }
            if (rdbTransNo.Checked)
            {
                entities.paga_subsidio = 0;
            }
            if (idObjeto == "")
            {
                TipoCotizante tiporetiro = cservice.CrearTipoCotizante(entities, Usuario);
                entities.consecutivo = tiporetiro.consecutivo;
            }
            else
            {
                TipoCotizante tiporetiro = cservice.ModificarTipoCotizante(entities, Usuario);
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
            Xpinn.Nomina.Services.TipoCotizanteService cservice = new Xpinn.Nomina.Services.TipoCotizanteService();
            Xpinn.Nomina.Entities.TipoCotizante entities = new Xpinn.Nomina.Entities.TipoCotizante();

            entities.consecutivo = Convert.ToInt64(idObjeto);
            entities = cservice.ConsultarTipoCotizante(entities.consecutivo, (Usuario)Session["usuario"]);
            var id = entities.consecutivo;


            if(entities != null)
            {
                if (entities.consecutivo != Int64.MinValue)
                   txtconsecutivo.Text = HttpUtility.HtmlDecode(entities.consecutivo.ToString().Trim());

                if (!string.IsNullOrEmpty(entities.descripcion.ToString()))
                    txtdescripcion.Text = HttpUtility.HtmlDecode(entities.descripcion.ToString().Trim());

                if (entities.porcentaje_salud != Int64.MinValue)
                    txtPorcentajeSalud.Text = HttpUtility.HtmlDecode(entities.porcentaje_salud.ToString().Trim());

                if (entities.porcentaje_pension != Int64.MinValue)
                    txtPorcentajePension.Text = HttpUtility.HtmlDecode(entities.porcentaje_pension.ToString().Trim());

                if (entities.paga_subsidio ==1)
                     rdbTransSi.Checked = true;
                 else
                    rdbTransNo.Checked = true;

                txtdescripcion_TextChanged(txtdescripcion, null);


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




    protected void txtdescripcion_TextChanged(object sender, EventArgs e)
    {
       

            txtPorcentajePension.Visible = true;
            txtPorcentajeSalud.Visible = true;
            LblPorcentajePension.Visible = true;
            LblPorcentajeSalud.Visible = true;

      

        
    }
}