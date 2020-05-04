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
using Xpinn.Util;
using Xpinn.Reporteador.Services;
using Xpinn.Reporteador.Entities;


public partial class Nuevo : GlobalWeb
{

    Xpinn.Reporteador.Services.ReporteService ParametroService = new Xpinn.Reporteador.Services.ReporteService();
    
    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            if (Session[ParametroService.CodigoProgramaReportelista + ".id"] != null)
                VisualizarOpciones(ParametroService.CodigoProgramaReportelista, "E");
            else
                VisualizarOpciones(ParametroService.CodigoProgramaReportelista, "A");

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
            txtCodigo.Enabled = false;
            if (!IsPostBack)
            {
                mvAplicar.ActiveViewIndex = 0;
                txtCodigo.Enabled = false;
                if (Session[ParametroService.CodigoProgramaReportelista + ".id"] != null)
                {

                    idObjeto = Session[ParametroService.CodigoProgramaReportelista + ".id"].ToString();
                    Session.Remove(ParametroService.CodigoProgramaReportelista + ".id");
                    ObtenerDatos(idObjeto);
                }
                else 
                {
                    txtCodigo.Enabled = false;
                }
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ParametroService.CodigoProgramaReportelista + "L", "Page_Load", ex);
        }
    }

    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            Xpinn.Reporteador.Entities.Lista vDetalle = new Xpinn.Reporteador.Entities.Lista();
            vDetalle = ParametroService.ConsultarReporteLista(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);

            if (vDetalle.idlista != 0 && vDetalle.idlista != null)
                txtCodigo.Text = vDetalle.idlista.ToString().Trim();            
            if (vDetalle.descripcion != "" && vDetalle.descripcion != null)
                txtDescripcion.Text = vDetalle.descripcion.ToString().Trim();
            if (vDetalle.textfield != null)
                txttextfield.Text = vDetalle.textfield.ToString().Trim();
            if (vDetalle.valuefield != null)
                txtvaluefield.Text = vDetalle.valuefield;
            if (vDetalle.sentencia != null)
                txtsentencia.Text = vDetalle.sentencia;

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ParametroService.CodigoProgramaReportelista, "ObtenerDatos", ex);
        }
    }


    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }

    public Boolean ValidarDatos()
    {
        if (txtDescripcion.Text == "")
        {
            VerError("Ingrese la Descripción");
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
            Xpinn.Reporteador.Entities.Lista pVar = new Xpinn.Reporteador.Entities.Lista();
            if (txttextfield.Text != null && txttextfield.Text != "")
            {
                pVar.textfield = Convert.ToString(txttextfield.Text);
            }
            if (txtCodigo.Text != null && txtCodigo.Text != "")
            {
                pVar.idlista = Convert.ToInt64(txtCodigo.Text);
            }
            if (txtDescripcion.Text != null && txtDescripcion.Text != "")
            {
                pVar.descripcion = Convert.ToString(txtDescripcion.Text.Replace(".", "")); ;
            }
            if (txtvaluefield.Text != null && txtvaluefield.Text != "")
            {
                pVar.valuefield = txtvaluefield.Text;
            }
            if (txtsentencia.Text != null && txtsentencia.Text != "")
            {
                pVar.sentencia = txtsentencia.Text;
            }
                        
            if (idObjeto != "")
            {
                //MODIFICAR
                ParametroService.ModificarReporteLista(pVar, (Usuario)Session["usuario"]);
            }
            else
            {
                ParametroService.CrearReporteLista(pVar, (Usuario)Session["usuario"] );                     
            }
            
            
            lblMsj.Text = idObjeto != ""?"Se Modificaron correctamente los datos": "Se Grabaron Correctamente los datos ingresados. Codigo Nro: "+txtCodigo.Text;
            Site toolBar = (Site)Master;
            toolBar.MostrarGuardar(false);
            mvAplicar.ActiveViewIndex = 1;            
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ParametroService.CodigoProgramaReportelista, "btnContinuar_Click", ex);
        }        
    }
}
