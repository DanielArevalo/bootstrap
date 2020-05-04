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
using Xpinn.Aportes.Services;
using Xpinn.Aportes.Entities;


public partial class Nuevo : GlobalWeb
{

    EscalafonSalarialService ParametroService = new EscalafonSalarialService();
    
    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            if (Session[ParametroService.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(ParametroService.CodigoPrograma, "E");
            else
                VisualizarOpciones(ParametroService.CodigoPrograma, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
            ctlMensaje.eventoClick += btnContinuarMen_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ParametroService.GetType().Name + "L", "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                mvAplicar.ActiveViewIndex = 0;
                txtCodigo.Enabled = false;
                if (Session[ParametroService.CodigoPrograma + ".id"] != null)
                {

                    idObjeto = Session[ParametroService.CodigoPrograma + ".id"].ToString();
                    Session.Remove(ParametroService.CodigoPrograma + ".id");
                    ObtenerDatos(idObjeto);
                }
                else 
                {
                    txtCodigo.Enabled = true;
                }
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ParametroService.CodigoPrograma + "L", "Page_Load", ex);
        }
    }

    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            EscalafonSalarial vDetalle = new EscalafonSalarial();
            vDetalle = ParametroService.ConsultarEscalafonSalarial(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);

            if (vDetalle.grado != "" && vDetalle.grado != null)
                txtCodigo.Text = vDetalle.grado.ToString().Trim();            

            if (vDetalle.asignacion_mensual != 0 && vDetalle.asignacion_mensual != null)
                txtDescripcion.Text = vDetalle.asignacion_mensual.ToString().Trim();

            if (vDetalle.idescalafon != null)
                txtidescalafon.Text = vDetalle.idescalafon.ToString().Trim();

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ParametroService.CodigoPrograma, "ObtenerDatos", ex);
        }
    }


    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }

    public Boolean ValidarDatos()
    {

        if (txtCodigo.Text == "")
        {
            VerError("Ingrese un Grado del Escalafón");
            return false;
        }
        if (txtDescripcion.Text == "")
        {
            VerError("Ingrese la Asignación Basica correspondiente");
            return false;
        }
       
         return true;
    }

    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");

        if (ValidarDatos())
        {
            string msj = idObjeto != "" ? "modificar" : "registrar";
             ctlMensaje.MostrarMensaje("Desea " + msj + " los Datos Ingresados?");
        }
    }



    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        try
        {
            EscalafonSalarial pVar = new EscalafonSalarial();
            if (txtidescalafon.Text != null && txtidescalafon.Text != "")
            {
                pVar.idescalafon = Convert.ToInt64(txtidescalafon.Text);
            }
                pVar.grado = Convert.ToString(txtCodigo.Text);
                pVar.asignacion_mensual = Convert.ToInt64(txtDescripcion.Text.Replace(".","")); ;
            
                        
            if (idObjeto != "")
            {
                //MODIFICAR
                ParametroService.ModificarEscalafonSalarial(pVar, (Usuario)Session["usuario"]);
            }
            else
            {
                ParametroService.CrearEscalafonSalarial(pVar, (Usuario)Session["usuario"] );                     
            }
            
            
            lblMsj.Text = idObjeto != ""?"Se Modificaron correctamente los datos": "Se Grabaron Correctamente los datos ingresados. Codigo Nro: "+txtCodigo.Text;
            Site toolBar = (Site)Master;
            toolBar.MostrarGuardar(false);
            mvAplicar.ActiveViewIndex = 1;            
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ParametroService.CodigoPrograma, "btnContinuar_Click", ex);
        }        
    }
}
