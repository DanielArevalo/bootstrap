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
using Xpinn.Cartera.Entities;
using Xpinn.Cartera.Services;
using System.Text;
using System.IO;
using System.Web.UI.HtmlControls;

partial class Lista : GlobalWeb
{
    private Xpinn.Cartera.Services.ProyeccionCarteraService serviceProyeccion = new Xpinn.Cartera.Services.ProyeccionCarteraService();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[serviceProyeccion.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(serviceProyeccion.CodigoPrograma, "E");
            else
                VisualizarOpciones(serviceProyeccion.CodigoPrograma, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(serviceProyeccion.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                btnExportarExcel.Visible = false;
                // Mostrar mensaje
                const string quote = "\"";
                string jqScript =
                @"function fillinbox(){
                    $.showprogress('Inbox','Loading.....','<img src=" + quote + "loadingimage.gif" + quote + "/>'); " + 
                        "$.post(" + quote + "controller/messagecontroller.aspx" + quote + ",{action:'inbox',page:'1'}, " +                         
                        "function(ret){ "+
                        "     $(" + quote + "#divResult" + quote + ").html(ret);"+
                        "} "+
                    ");"+
                    "$.hideprogress();"+
                    "};";
                this.ClientScript.RegisterStartupScript(Page.GetType(), "fillinbox", jqScript.ToString(), true);
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(serviceProyeccion.CodigoPrograma, "Page_Load", ex);
        }
    }


    protected void btnLimpiar_Click(object sender, EventArgs e)
    {
        txtFechaIni.Enabled = true;
        txtPeriodos.Enabled = true;
        btnExportarExcel.Visible = false;
    }

    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        if (txtFechaIni.Text.Trim() == "")
        {
            VerError("Debe seleccionar la fecha");
            return;
        }
        try
        {
            Int64 nperiodos = 36;
            try
            {
                nperiodos = Convert.ToInt64(txtPeriodos.Text);
            }
            catch
            {
                nperiodos = 36;
                txtPeriodos.Text = "36";
            }
            DateTime fecha = Convert.ToDateTime(txtFechaIni.Text); 
            DateTime fechaprimerperiodo = Convert.ToDateTime(txtFechaIni.Text);
            DateTime fechaultimoperiodo = Convert.ToDateTime(txtFechaIni.Text);

            // Validar si el cierre histórico ya existe
            VerError("");
            try
            {
                Xpinn.Comun.Services.CiereaService CiereaService = new Xpinn.Comun.Services.CiereaService();
                if (!CiereaService.ExisteCierre(fecha, "R", (Usuario)Session["Usuario"]))
                {
                    VerError("No existe el cierre histórico de cartera a la fecha dada");
                    return;
                }
            }
            catch
            {
                VerError("Error al validar la fecha del cierre");
            }

            // Validar si ya se ejecuto el proceso
            DateTime lfecha_corte = Convert.ToDateTime(txtFechaIni.Text);
            if (serviceProyeccion.ValidarProyeccionCartera(lfecha_corte, (Usuario)Session["Usuario"]) == 0)
            {
                string sError = "";
                serviceProyeccion.Proyeccion(lfecha_corte, (Usuario)Session["Usuario"], ref sError);        
            }


            txtFechaIni.Enabled = false;
            txtPeriodos.Enabled = false;
            //ClientScript.RegisterStartupScript(this.GetType(), "myScript", "fillinbox();", true);

            // Cargar columnas fijas en el datatable
            DataTable dtProy = new DataTable();
            dtProy.Clear();
            dtProy.Columns.Add("fecha", typeof(DateTime));
            dtProy.Columns.Add("numero_radicacion", typeof(Int64));
            dtProy.PrimaryKey = new DataColumn[] { dtProy.Columns["numero_radicacion"] };
            dtProy.Columns.Add("oficina", typeof(Int64));
            dtProy.Columns.Add("pagare", typeof(string));
            dtProy.Columns.Add("identificacion", typeof(string));
            dtProy.Columns.Add("nombre", typeof(string));
            dtProy.Columns.Add("fecha_inicio", typeof(DateTime));
            dtProy.Columns.Add("fecha_terminacion", typeof(DateTime));
            dtProy.Columns.Add("cod_linea", typeof(string));
            dtProy.Columns.Add("dias_mora", typeof(Int64));
            dtProy.Columns.Add("monto", typeof(Double));
            dtProy.Columns.Add("saldo", typeof(Double));
            dtProy.Columns.Add("cuota", typeof(Double));
            dtProy.Columns.Add("fecha_proximo_pago", typeof(DateTime));

            // Generar las columnas en la grilla y en el datatable según el número de períodos a proyectar
            gvProyeccion.AutoGenerateColumns = false;
            fecha = fecha.AddDays(1);
            fecha = fecha.AddDays(31);
            fecha = new DateTime(fecha.Year, fecha.Month, 1);
            fecha = fecha.AddDays(-1);
            fechaprimerperiodo = fecha;
            for (int i = 1; i <= nperiodos; i++)
            {
                // Adicionar la columna a la grilla
                BoundField ColumnBoundKAP;
                ColumnBoundKAP = new BoundField();
                ColumnBoundKAP.HeaderText = "KAP." + fecha.ToString("MMMM").ToUpper() + " " + fecha.ToString("yyyy");
                ColumnBoundKAP.DataField = "KAP" + fecha.ToString("MM") + fecha.ToString("yyyy");
                ColumnBoundKAP.DataFormatString = "{0:N}";
                ColumnBoundKAP.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                ColumnBoundKAP.ItemStyle.BackColor = System.Drawing.Color.LightBlue;
                gvProyeccion.Columns.Add(ColumnBoundKAP);
                BoundField ColumnBoundINT;
                ColumnBoundINT = new BoundField();
                ColumnBoundINT.HeaderText = "INTCTE." + fecha.ToString("MMMM").ToUpper() + " " + fecha.ToString("yyyy");
                ColumnBoundINT.DataField = "INTCTE" + fecha.ToString("MM") + fecha.ToString("yyyy");
                ColumnBoundINT.DataFormatString = "{0:N}";
                ColumnBoundINT.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                gvProyeccion.Columns.Add(ColumnBoundINT);
                BoundField ColumnBoundOTR;
                ColumnBoundOTR = new BoundField();
                ColumnBoundOTR.HeaderText = "OTR." + fecha.ToString("MMMM").ToUpper() + " " + fecha.ToString("yyyy");
                ColumnBoundOTR.DataField = "OTR" + fecha.ToString("MM") + fecha.ToString("yyyy");
                ColumnBoundOTR.DataFormatString = "{0:N}";
                ColumnBoundOTR.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                gvProyeccion.Columns.Add(ColumnBoundOTR);
                // Adicionar la columna al datatable
                dtProy.Columns.Add("KAP" + fecha.ToString("MM") + fecha.ToString("yyyy"), typeof(Double));
                dtProy.Columns.Add("INTCTE" + fecha.ToString("MM") + fecha.ToString("yyyy"), typeof(Double));
                dtProy.Columns.Add("OTR" + fecha.ToString("MM") + fecha.ToString("yyyy"), typeof(Double));
                // Ir a la siguiente fecha
                fechaultimoperiodo = fecha;
                fecha = fecha.AddDays(1);
                fecha = fecha.AddDays(31);
                fecha = new DateTime(fecha.Year, fecha.Month, 1);
                fecha = fecha.AddDays(-1);
            }
            
            // Llenar la grilla con la información inicial de los créditos a la fecha de cierre
            DateTime lfecha_historico = Convert.ToDateTime(txtFechaIni.Text); 
            List<ProyeccionCartera> lstProyeccionCartera = new List<ProyeccionCartera>();
            lstProyeccionCartera = serviceProyeccion.listarProyeccionCartera(lfecha_historico, fecha, (Usuario)Session["Usuario"]);
            lblTotRegs.Text = "Se encontraron " + lstProyeccionCartera.Count.ToString() + " registros";
            Int64 nContador = 0;

            foreach (ProyeccionCartera rfila in lstProyeccionCartera)
            {
                if (rfila.fecha_cuota <= lfecha_historico)
                    rfila.fecha_cuota = fechaprimerperiodo;
                if (rfila.fecha_cuota > fechaultimoperiodo)
                    rfila.fecha_cuota = fechaultimoperiodo;
                // Determinar la columna a la que pertenece
                String sColumna = "";
                switch (rfila.cod_atr)
                {
                    case 1:
                        sColumna = "KAP" + rfila.fecha_cuota.ToString("MM") + rfila.fecha_cuota.ToString("yyyy");
                        break;
                    case 2:
                        sColumna = "INTCTE" + rfila.fecha_cuota.ToString("MM") + rfila.fecha_cuota.ToString("yyyy");
                        break;
                    default:
                        sColumna = "OTR" + rfila.fecha_cuota.ToString("MM") + rfila.fecha_cuota.ToString("yyyy");
                        break;
                }
                // Asignar los datos al datatable
                DataRow drProy;
                drProy = dtProy.Rows.Find(rfila.numero_radicacion);
                if (drProy == null)
                {
                    drProy = dtProy.NewRow();
                    drProy["fecha"] = rfila.fecha;
                    drProy["numero_radicacion"] = rfila.numero_radicacion;
                    drProy["oficina"] = rfila.oficina;
                    drProy["pagare"] = rfila.pagare;
                    drProy["identificacion"] = rfila.identificacion;
                    drProy["nombre"] = rfila.nombre;
                    drProy["fecha_inicio"] = rfila.fecha_inicio;
                    drProy["fecha_terminacion"] = rfila.fecha_terminacion;
                    drProy["cod_linea"] = rfila.cod_linea;
                    drProy["dias_mora"] = rfila.dias_mora;
                    drProy["monto"] = rfila.monto;
                    drProy["saldo"] = rfila.saldo;
                    drProy["cuota"] = rfila.cuota;
                    drProy["fecha_proximo_pago"] = rfila.fecha_proximo_pago;
                    drProy[sColumna] = rfila.valor;
                    dtProy.Rows.Add(drProy);
                }
                else
                {
                    DateTime lfecha = rfila.fecha_cuota;
                    Double valor = rfila.valor;
                    if (drProy[sColumna] == null || drProy[sColumna].ToString().Trim() == "")
                        drProy[sColumna] = 0;
                    Double total = 0;
                    try
                    {                        
                        total = Convert.ToDouble(drProy[sColumna]);                        
                    }
                    catch
                    {
                        VerError("Error:" + drProy[sColumna] + "<-");
                    }
                    drProy[sColumna] = total + valor;
                }
                nContador = nContador + 1;
            }

            gvProyeccion.PageSize = 30;
            Session["DTPROYECCION"] = dtProy;
            gvProyeccion.DataSource = dtProy;
            gvProyeccion.DataBind();
            btnExportarExcel.Visible = true;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(serviceProyeccion.CodigoPrograma, "btnConsultar_Click", ex);
        }
    }

    /// <summary>
    /// Método para exportar los datos a excel
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnExportarExcel_Click(object sender, EventArgs e)
    {
        if (gvProyeccion.Rows.Count > 0)
        {
            gvProyeccion.AllowPaging = false;
            gvProyeccion.DataSource = (DataTable)Session["DTPROYECCION"];
            gvProyeccion.DataBind();
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            Page pagina = new Page();
            dynamic form = new HtmlForm();
            gvProyeccion.EnableViewState = false;
            pagina.EnableEventValidation = false;
            pagina.DesignerInitialize();
            pagina.Controls.Add(form);
            form.Controls.Add(gvProyeccion);
            pagina.RenderControl(htw);
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=ProyeccionCartera.xls");
            Response.Charset = "UTF-8";
            Response.ContentEncoding = Encoding.Default;
            Response.Write(sb.ToString());
            Response.End();
            gvProyeccion.AllowPaging = true;            
            gvProyeccion.DataBind();
        }
        else
            VerError("Se debe generar el reporte primero");

    }


}