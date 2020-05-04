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
using Xpinn.FabricaCreditos.Services;
using Xpinn.FabricaCreditos.Entities;

partial class Nuevo : GlobalWeb
{   
    private Xpinn.FabricaCreditos.Services.HistoricoTasaService HistoricoTasaService = new Xpinn.FabricaCreditos.Services.HistoricoTasaService();
    
    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[HistoricoTasaService.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(HistoricoTasaService.CodigoPrograma, "E");
            else
                VisualizarOpciones(HistoricoTasaService.CodigoPrograma, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(HistoricoTasaService.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                txtcodigo.Enabled = false;
                CargarListas();
                if (Session[HistoricoTasaService.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[HistoricoTasaService.CodigoPrograma + ".id"].ToString();
                    Session.Remove(HistoricoTasaService.CodigoPrograma + ".id");
                    ObtenerDatos(idObjeto);
                    ddlHistorico.Enabled = false;
                }
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(HistoricoTasaService.CodigoPrograma, "Page_Load", ex);
        }
    }

    /// <summary>
    /// Método para cargar información del histórico a modificar
    /// </summary>
    /// <param name="pIdObjeto"></param>
    protected void ObtenerDatos(String pIdObjeto)
    {
        HistoricoTasa historico = new HistoricoTasa();

        historico = HistoricoTasaService.obtenermod(pIdObjeto, (Usuario)Session["Usuario"]);
        txtFechaIni.Text = historico.FECHA_INICIAL.ToString("dd/MM/yyyy");
        txtFechaFin.Text = historico.FECHA_FINAL.ToString("dd/MM/yyyy");
        ddlHistorico.SelectedValue =Convert.ToString(historico.TIPO_HISTORICO);
        txtvalor.Text = Convert.ToString(historico.VALOR);
        txtcodigo.Text = pIdObjeto;                
    }

    /// <summary>
    /// Método para cargar los DDL de los tipos de tasas históricos
    /// </summary>
    private void CargarListas()
    {
        try
        {
            ddlHistorico.DataSource = HistoricoTasaService.tipohistorico((Usuario)Session["Usuario"]);
            ddlHistorico.DataTextField = "DESCRIPCION";
            ddlHistorico.DataValueField = "TIPODEHISTORICO";
            ddlHistorico.DataBind();           
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(HistoricoTasaService.GetType().Name + "L", "CargarListas", ex);
        }
    }

    /// <summary>
    /// Método para guardar los datos de la tasa histórica
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        VerError("");
        try
        {
            HistoricoTasa historico = new HistoricoTasa();
            try
            {
                historico.FECHA_INICIAL = DateTime.ParseExact(txtFechaIni.Text, gFormatoFecha, null);
                historico.FECHA_FINAL = DateTime.ParseExact(txtFechaFin.Text, gFormatoFecha, null);
            }
            catch
            {
                VerError("Error al convertir las fechas");
                return;
            }
            historico.TIPO_HISTORICO = Convert.ToInt64(ddlHistorico.SelectedValue);
            try
            {
                historico.VALOR = Convert.ToDecimal(txtvalor.Text);
            }
            catch
            {
                VerError("El valor es incorrecto");
                return;
            }
            if(txtcodigo.Text!= "")
                historico.IDHISTORICO = Convert.ToInt64(txtcodigo.Text);
            else
                historico.IDHISTORICO = 0;

            if (historico.IDHISTORICO == 0)
                HistoricoTasaService.CrearHistorico(historico, (Usuario)Session["Usuario"]);
            else
                HistoricoTasaService.ModHistorico(historico, (Usuario)Session["Usuario"]);

            Session[HistoricoTasaService.CodigoPrograma + ".id"] = historico.IDHISTORICO.ToString();
            Navegar(Pagina.Lista); 
         }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(HistoricoTasaService.CodigoPrograma, "btnGuardar_Click", ex);
        }
    }

    protected void btnConsultar_Click(object sender, EventArgs e)
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
            Session[HistoricoTasaService.CodigoPrograma + ".id"] = idObjeto;
            Navegar(Pagina.Lista);
        }
    }

    protected void ObtenerDatos()
    {
        try
        {
            Xpinn.FabricaCreditos.Entities.LineasCredito vLineasCredito = new Xpinn.FabricaCreditos.Entities.LineasCredito();            
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(HistoricoTasaService.CodigoPrograma, "ObtenerDatos", ex);
        }
    }
    

}