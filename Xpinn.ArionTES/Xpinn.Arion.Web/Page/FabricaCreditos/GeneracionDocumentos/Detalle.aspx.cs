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
using Microsoft.Reporting.WebForms;
using Microsoft.CSharp;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Xpinn.FabricaCreditos.Entities;
using GenCode128;
using Xpinn.Asesores.Entities;
using Xpinn.Asesores.Business;
using Xpinn.Asesores.Services;

partial class Detalle : GlobalWeb
{
    private Xpinn.FabricaCreditos.Services.CreditoService CreditoServicio = new Xpinn.FabricaCreditos.Services.CreditoService();
    String sTipo = "code128";

    /// <summary>
    /// Cargar la barra de herramientas al inicial la aplicación
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(CreditoServicio.CodigoPrograma, "D");

            Site toolBar = (Site)this.Master;
            if (Session["talonario"].ToString() == "1")
            {
                toolBar.eventoRegresar += btnRegresar_Click;
            }
            else
            {
                toolBar.eventoEditar += btnEditar_Click;
                toolBar.eventoConsultar += btnConsultar_Click;
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CreditoServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    /// <summary>
    /// Cargar datos de la funcionalidad
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                ReportViewer1.Visible = false;
                AsignarEventoConfirmar();
                if (Session[CreditoServicio.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[CreditoServicio.CodigoPrograma + ".id"].ToString();
                    Session.Remove(CreditoServicio.CodigoPrograma + ".id");
                    ObtenerDatos(idObjeto);
                }
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CreditoServicio.CodigoPrograma, "Page_Load", ex);
        }
    }

    /// <summary>
    /// Inhabilitar botón para crear
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    {
        //Navegar(Pagina.Nuevo);
    }

    /// <summary>
    /// Realizar la consulta
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        Navegar(Pagina.Lista);
    }

    /// <summary>
    ///  Evento para eliminar datos que se inhabilita
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnEliminar_Click(object sender, ImageClickEventArgs e)
    {

    }

    /// <summary>
    /// Evento que se llama al editar
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnEditar_Click(object sender, ImageClickEventArgs e)
    {
        Session[CreditoServicio.CodigoPrograma + ".id"] = idObjeto;
        Navegar(Pagina.Nuevo);
    }
    protected void btnRegresar_Click(object sender, ImageClickEventArgs e)
    {
        Navegar("~/Page/FabricaCreditos/PlanPagos/Lista.aspx");
    }
    

    /// <summary>
    /// Evento para confirmar
    /// </summary>
    protected void AsignarEventoConfirmar()
    {
        ConfirmarEventoBoton((LinkButton)Master.FindControl("btnEliminar"), "Esta seguro que desea eliminar el registro?");
    }

    /// <summary>
    ///  Mostrar los datos del crédito
    /// </summary>
    /// <param name="pIdObjeto"></param>
    /// 
   
    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            Xpinn.FabricaCreditos.Entities.Credito vCredito = new Xpinn.FabricaCreditos.Entities.Credito();
            vCredito = CreditoServicio.ConsultarCredito(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);
            Label1.Text = Convert.ToString(vCredito.cod_deudor);
            if (vCredito.numero_radicacion != Int64.MinValue)
                txtNumero_radicacion.Text = vCredito.numero_radicacion.ToString().Trim();
            if (vCredito.identificacion != string.Empty)
                txtIdentificacion.Text = vCredito.identificacion.ToString().Trim();
            if (!string.IsNullOrEmpty(vCredito.tipo_identificacion))
                txtTipo_identificacion.Text = vCredito.tipo_identificacion.ToString().Trim();
            if (!string.IsNullOrEmpty(vCredito.nombre))
                txtNombre.Text = vCredito.nombre.ToString().Trim();
            if (!string.IsNullOrEmpty(vCredito.linea_credito))
                txtLinea_credito.Text = vCredito.linea_credito.ToString().Trim();
            if (vCredito.monto != Int64.MinValue)
                txtMonto.Text = vCredito.monto.ToString().Trim();
            if (vCredito.plazo != Int64.MinValue)
                txtPlazo.Text = vCredito.plazo.ToString().Trim();
            if (!string.IsNullOrEmpty(vCredito.periodicidad))
                txtPeriodicidad.Text = vCredito.periodicidad.ToString().Trim();
            if (vCredito.valor_cuota != Int64.MinValue)
                txtValor_cuota.Text = vCredito.valor_cuota.ToString().Trim();
            if (!string.IsNullOrEmpty(vCredito.forma_pago))
                txtForma_pago.Text = vCredito.forma_pago.ToString().Trim();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CreditoServicio.CodigoPrograma + "D", "ObtenerDatos", ex);
        }
    }
  
    protected void talonario_Click(object sender, EventArgs e)
    {
        List<Credito> resultado = new List<Credito>();
        Credito cuotas = new Credito();
        resultado = CreditoServicio.ConsultarCuotas(Convert.ToInt64(txtNumero_radicacion.Text), (Usuario)Session["Usuario"]);
        ReportViewer1.Visible = true;

        #region GenerarCodigoDeBarras    
            
        System.Data.DataTable table = new System.Data.DataTable();
        table.Columns.Add("colmna");
        table.Columns.Add("identificacion");
        table.Columns.Add("codigo");
        table.Columns.Add("nombre");
        table.Columns.Add("linea");
        table.Columns.Add("plazo");
        table.Columns.Add("cuota");
        table.Columns.Add("radicacion");
        table.Columns.Add("cliente");
        table.Columns.Add("banco");
        table.Columns.Add("efectivo");
        table.Columns.Add("cheque");
        table.Columns.Add("total");
        table.Columns.Add("numche");
        table.Columns.Add("codban");
        table.Columns.Add("t1");
        table.Columns.Add("t2");
        table.Columns.Add("m1");
        table.Columns.Add("m2");
        table.Columns.Add("m3");
        for (int i = 0; i < resultado.Count; i++)
        {
            cuotas = resultado[i];
            string cRutaDeImagen = System.IO.Path.GetTempPath() + Guid.NewGuid().ToString() + ".png";
            string CadenaCodigoDeBarras;
            long identifi= Convert.ToInt64(txtIdentificacion.Text);
            long radicacion = Convert.ToInt64(txtNumero_radicacion.Text);
            CadenaCodigoDeBarras = "(415)7709998005730" + "(8020)" + radicacion.ToString("00000000") + "(8020)" + identifi.ToString("00000000") + "(3900)" + cuotas.valor_a_pagar.ToString("0000000000") + "(96)" + Convert.ToDateTime(cuotas.fecha_corte_string).ToString("yyyy/MM/dd").Replace("/", "");
            if (sTipo == "code128")
            {
                System.Drawing.Image imgCodBarras = Code128Rendering.MakeBarcodeImage(CadenaCodigoDeBarras, 1, true);
                imgCodBarras.Save(cRutaDeImagen, System.Drawing.Imaging.ImageFormat.Png);
            }
        
            DataRow datarw;
            datarw = table.NewRow();
            datarw[0] = " ";
            datarw[1] = @"Radicación                 " + txtNumero_radicacion.Text + "  " +
                         "cuota              " + cuotas.numero_radicacion + "/" +txtPlazo.Text+
                         "Código                    " + Label1.Text + "  " +
                         "Nombre                   " + txtNombre.Text + "  " +
                         "Identificación           " + txtIdentificacion.Text + "\r\n" +
                         "Línea                      " + txtLinea_credito.Text + "\r\n" +
                         "Plazo                      " + txtPlazo.Text + "  " +
                         "Valor Cuota             " + cuotas.valor_a_pagar.ToString("###,###,##0") + "\r\n" +
                         "Fecha a Pagar        " + Convert.ToDateTime(cuotas.fecha_corte_string).ToShortDateString();
            datarw[2] = cRutaDeImagen;
            datarw[3] = CadenaCodigoDeBarras;
            datarw[4] = "linea " + " " + txtLinea_credito.Text;
            datarw[5] = "plazo " + " " + txtPlazo.Text;
            datarw[6] = "cuota " + " " + cuotas.valor_a_pagar;
            datarw[7] = "radicacion " + " " + txtNumero_radicacion.Text;
            datarw[8] = "numerocuota " + " " + txtNumero_radicacion.Text;
            datarw[9] = "fechacuota " + " " + txtNumero_radicacion.Text;
            datarw[10] = "valor " + " " + txtNumero_radicacion.Text;
            datarw[10] = "DESPRENDIBLE PARA EL CLIENTE ";
            datarw[10] = "DESPRENDIBLE PARA EL BANCO";
            datarw[10] = "Efectivo ";
            datarw[10] = "Número Cheque ";
            datarw[10] = "Código Banco";
            datarw[10] = "numche";
            datarw[10] = "codban";
            datarw[10] = "FORMA DE PAGO";
            datarw[10] = "CHEQUE";
            datarw[10] = "Si efectua su pago con cheque, por favor girarlo a nombre de EMPRENDER NIT 900534833-5";
            datarw[10] = "Usted puede realizar sus pagos en los siguientes bancos            Banco Occidente Cta Cte No.288-078116       Banco Bogota Cta Cte  No.338-08388-3";
            datarw[10] = "APRECIADO CLIENTE: Realice oportunamente el pago de sus obligaciones con  EMPRENDER  de  lo  contrario se reportará  negativamente antte las Centrales de Riesgo dando cumplimiento a la ley 1266 (HABEAS DATA) del 2008, Arts. 12 y siguies ";

            table.Rows.Add(datarw);

        } 

        #endregion GenerarCodigoDeBarras

        ReportParameter[] param = new ReportParameter[1];
        param[0] = new ReportParameter("imagen"," ");
        ReportViewer1.LocalReport.EnableExternalImages = true;       
        ReportViewer1.LocalReport.SetParameters(param);
        ReportDataSource rds1 = new ReportDataSource("DataSet1", table);
        ReportViewer1.LocalReport.DataSources.Add(rds1);
        ReportViewer1.LocalReport.Refresh();
    }

}