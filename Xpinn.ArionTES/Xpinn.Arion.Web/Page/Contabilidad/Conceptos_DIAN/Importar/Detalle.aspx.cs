using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Reporteador.Services;
using Xpinn.Reporteador.Entities;
using System.Data;
using Xpinn.Util;
using System.Text;
using System.IO;
using System.Web.UI.HtmlControls;
using ClosedXML.Excel;
using System.Globalization;
using System.Threading.Tasks;

public partial class Page_Contabilidad_Conceptos_DIAN_Importar_Detalle : GlobalWeb
{
    ExogenaReportService objReporteService = new ExogenaReportService();
    String operacion = "";
    List<ExogenaReport> _lstRegistroConcepto;
    int _contadorRegistro;
    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(objReporteService.CodigoTiposConcepto, "D");

            Site toolBar = (Site)Master;
            toolBar.eventoCancelar += btnCancelar_Click;


        }
        catch (Exception ex)
        {
            //BOexcepcion.Throw(_datosClienteServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DataTable table = new DataTable();
            table.Columns.Add("codformato");
            table.Columns.Add("formato");

        

            DataRow dr = table.NewRow();


            table.Rows.Add("1", "FORMATO 1019-Movimiento cuenta Ahorro");
            table.Rows.Add("2","FORMATO 1020-Inversiones en CDATS");
            table.Rows.Add("3","FORMATO 1010-Información socios accionistas, cooperados");
            table.Rows.Add("4","FORMATO 1008-Saldo de cuentas por Cobrar");
            table.Rows.Add("5","FORMATO 1001-Pago o Abonos en cuenta y Retenciones Practicadas");
            table.Rows.Add("6","FORMATO 1007-Ingresos Recibidos");
            table.Rows.Add("7","FORMATO 1009-Saldo de cuentas por pagar");
     

            gvFormatos.DataSource = table;
            gvFormatos.DataBind();

            List<ExogenaReport> LstConceptos = new List<ExogenaReport>();

            LstConceptos = objReporteService.TiposConceptos((Usuario)Session["Usuario"]);
            gvConceptos.DataSource = LstConceptos;
            gvConceptos.DataBind();
        }
      
    }
    protected void btnCancelar_Click(object sender, EventArgs e)
    {

        Navegar(Pagina.Lista);
    }
    protected void btnCargarPersonas_Click(object sender, EventArgs e)
    {
        string cod_cuenta = "";
        try
        {
            VerError("");
            if (avatarUpload.PostedFile.ContentLength > 0)
            {
                _contadorRegistro = 1;

                _lstRegistroConcepto = new List<ExogenaReport>();
                ConcurrentHelper<Stream, string[]> concurrentHelper = new ConcurrentHelper<Stream, string[]>();
                Task<bool> producerWork = null;
                Task<bool> consumerWork = null;

                using (Stream stream = avatarUpload.PostedFile.InputStream)
                {
                    // Producer - Consumer Design :D
                    producerWork = Task.Factory.StartNew(() => concurrentHelper.ProduceWork(stream, LeerLineaDeArchivo));
                    consumerWork = Task.Factory.StartNew(() => concurrentHelper.ConsumeWork(ProcesarLineaDeArchivo));
                    Task.WaitAll(producerWork, consumerWork);
                }
                foreach (ExogenaReport item in _lstRegistroConcepto)
                {
                    cod_cuenta = Convert.ToString(item.cod_cuenta);
                    objReporteService.CrearPlanCtasHomologacionDIAN(item, (Usuario)Session["Usuario"]);
                    pnlNotificacion.Visible = true;
                    Panel1.Visible = false;
                }

            }
        }
        catch (Exception ex)
        {
            VerError("La cuenta " + cod_cuenta + "no existe, por favor verificar los datos.");
        }
    }
    public IEnumerable<string[]> LeerLineaDeArchivo(Stream stream)
    {
        string linea = string.Empty;
        char separador = '|';

        using (StreamReader strReader = new StreamReader(stream))
        {
            while ((linea = strReader.ReadLine()) != null)
            {
                if (string.IsNullOrWhiteSpace(linea)) continue;

                // Retorna el string[] por cada vuelta, no espera a que el while termine
                // Despues de retornar, vuelve al while y retorna el siguiente string[]
                // Sale del while al no haber mas lineas que leer (trReader.ReadLine()) == null)
                // Ese es el comportamiento del yield return
                yield return linea.Split(separador);
            }
        }
    }
    public void ProcesarLineaDeArchivo(string[] lineaAProcesar)
    {
        ExogenaReport concepto = new ExogenaReport();
        bool sinErrores = true;
        for (int i = 0; i < 3; i++)
        {
            if (i == 0)
            { try { concepto.cod_cuenta = lineaAProcesar[i].Trim(); } catch (Exception ex) { sinErrores = false; break; } }
            if (i == 1)
            { try { concepto.codconcepto =Convert.ToInt64(lineaAProcesar[i].Trim()); } catch (Exception ex) { sinErrores = false; break; } }
            if (i == 2)
            { try { concepto.Formato = lineaAProcesar[i].Trim(); } catch (Exception ex) { sinErrores = false; break; } }
        }

        if (sinErrores)
        {

            _lstRegistroConcepto.Add(concepto);
        }

        _contadorRegistro += 1;
    }
    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {

    }

}