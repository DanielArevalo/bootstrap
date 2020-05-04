using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Xpinn.Tesoreria.Entities;
using Xpinn.Util;
using Microsoft.Reporting.WebForms;

public partial class InformeArqueo : GlobalWeb
{
    private Xpinn.Tesoreria.Services.AreasCajService AreasCajServicio = new Xpinn.Tesoreria.Services.AreasCajService();
    private Xpinn.Tesoreria.Services.ReporteArqueoCajaMenorServices ArqueoCajServicio = new Xpinn.Tesoreria.Services.ReporteArqueoCajaMenorServices();
    private Xpinn.Tesoreria.Services.SoporteCajService SoporteCajServicio = new Xpinn.Tesoreria.Services.SoporteCajService();
    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[ArqueoCajServicio.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(ArqueoCajServicio.CodigoPrograma, "E");
            else
                VisualizarOpciones(ArqueoCajServicio.CodigoPrograma, "A");
            Site toolBar = (Site)this.Master;
            toolBar.eventoImprimir += btnImprimir_Click;
            toolBar.eventoCancelar += btnCancelar_Click;

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ArqueoCajServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {   
        try
        {
            if (!IsPostBack)
            {
                // Session["DatosEfectivo"] = null;
                mvPrincipal.ActiveViewIndex = 0;
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
    }
    protected List<Efectivo> ObtenerListaEfectivo()
    {
        List<Efectivo> listaE = new List<Efectivo>();
        List<Efectivo> lista = new List<Efectivo>();

        foreach (GridViewRow fila in gvEfectivoCajaMenor.Rows)
        {
            Efectivo eCaja = new Efectivo()
            { 
                denominacion = Convert.ToInt64(fila.Cells[1].Text),
                cantidad = Convert.ToInt64(fila.Cells[2].Text),
                total = Convert.ToInt64(fila.Cells[3].Text.Replace(".","")),
                tipo = fila.Cells[4].Text
            };
            lista.Add(eCaja);
            Session["DatosEfectivo"] = lista;
            if (eCaja.denominacion != 0 && eCaja.cantidad != 0 && eCaja.total != 0 && !eCaja.tipo.Equals(null))
            {
                listaE.Add(eCaja);
            }
        }
        return listaE;
    }

    protected bool ValidarEfectivo(string tipo, Int64 cantidad)
    {
        if(cantidad <= 0)
        {
            VerError("La cantidad de efectivo no puede ser menor o igual a cero");
            return false;
        }
        if(tipo.Equals("Billete"))
        { 
            foreach (GridViewRow fila in gvEfectivoCajaMenor.Rows)
            {
                if (Convert.ToInt64(fila.Cells[2].Text) == Convert.ToInt64(ddlBilletes.SelectedItem.Value) && fila.Cells[5].Text == tipo)
                {
                    VerError("El efectivo de esta denominación ya ha sido registrado");
                    return false;                    
                }
                    
            }
        }
        if (tipo.Equals("Moneda")){
            foreach (GridViewRow fila in gvEfectivoCajaMenor.Rows)
            {
                if (Convert.ToInt64(fila.Cells[2].Text) == Convert.ToInt64(ddlMonedas.SelectedItem.Value) && fila.Cells[5].Text == tipo)
                {                    
                    VerError("El efectivo de esta denominación ya ha sido registrado");
                    return false;
                }
                   
            }
        }
        return true;
    }

    protected void btnAgregarB_Click(object sender, EventArgs e)
    {
        List<Efectivo> listaEfectivo = new List<Efectivo>();
        listaEfectivo = ObtenerListaEfectivo();
        Int64 cantidad = txtCantB.Text != "" ? Convert.ToInt64(txtCantB.Text) : 0;
        if (ValidarEfectivo("Billete", cantidad)){
            VerError("");  
            Efectivo lista = new Efectivo()
            {
                denominacion = Convert.ToInt64(ddlBilletes.SelectedItem.Value),
                cantidad = Convert.ToInt64(txtCantB.Text),
                total = Convert.ToInt64(ddlBilletes.SelectedItem.Value) * Convert.ToInt64(txtCantB.Text),
                tipo = "Billete"
            };
            listaEfectivo.Add(lista);
            gvEfectivoCajaMenor.DataSource = listaEfectivo;
            gvEfectivoCajaMenor.DataBind();
        }

    }

    protected void btnagregarM_Click(object sender, EventArgs e)
    {
        List<Efectivo> listaEfectivo = new List<Efectivo>();
        listaEfectivo = ObtenerListaEfectivo();
        Int64 cantidad = txtCantM.Text != "" ? Convert.ToInt64(txtCantM.Text) : 0;
        if (ValidarEfectivo("Moneda", cantidad))
        {
            VerError("");
            Efectivo lista = new Efectivo()
            {
                denominacion = Convert.ToInt64(ddlMonedas.SelectedItem.Value),
                cantidad = Convert.ToInt64(txtCantM.Text),
                total = Convert.ToInt64(ddlMonedas.SelectedItem.Value) * Convert.ToInt64(txtCantM.Text),
                tipo = "Moneda"
            };
            listaEfectivo.Add(lista);
            gvEfectivoCajaMenor.DataSource = listaEfectivo;
            gvEfectivoCajaMenor.DataBind();
        }
    }
    protected void gvEfectivoCajaMenor_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        //int conseID = Convert.ToInt32(gvEfectivoCajaMenor.DataKeys[e.RowIndex].Values[0].ToString());
        GridViewRow fila = gvEfectivoCajaMenor.Rows[e.RowIndex];

        string tipo_efectivo = fila.Cells[4].Text;
        string denominacion = fila.Cells[1].Text;
        
        try
        {
            List<Efectivo> LstEfectivo = new List<Efectivo>();
            ObtenerListaEfectivo();
            LstEfectivo = (List<Efectivo>)Session["DatosEfectivo"];

            LstEfectivo.RemoveAt((gvEfectivoCajaMenor.PageIndex * gvEfectivoCajaMenor.PageSize) + e.RowIndex);
            Session["DatosEfectivo"] = LstEfectivo;
            gvEfectivoCajaMenor.DataSource = LstEfectivo;
            gvEfectivoCajaMenor.DataBind();
        }
        catch (Exception ex)
        {
            VerError(ex.Message.ToString());
        }
    }

    protected void ObtenerLstEfectivo()
    {
        List<ArqueoDetalle> listaE = new List<ArqueoDetalle>();
        AreasCaj vAreasC = new AreasCaj();
        SoporteCaj SoporteArqueo = new SoporteCaj();

        List<ArqueoDetalle> listaDetalle = new List<ArqueoDetalle>();
        vAreasC = AreasCajServicio.ConsultarCajaMenor(Convert.ToInt32(Usuario.codusuario), Usuario);
        //Objeto tipo ArqueoCaj 
        ArqueoCaj vArqueoCaj = new ArqueoCaj()
        {
            id_arqueo = null,
            fecha_arqueo = DateTime.Now,
            total_arqueo = 0,
            cod_usuario = Usuario.codusuario,
            idarea = vAreasC.idarea,
        };
        foreach (GridViewRow fila in gvEfectivoCajaMenor.Rows)
        {
            ArqueoDetalle ArqueoDet = new ArqueoDetalle();
            ArqueoDet = new ArqueoDetalle()
            {
                id_det_arqueo = null,
                id_arqueo = vArqueoCaj.id_arqueo,
                tipo_efectivo = fila.Cells[4].Text,
                denominacion = Convert.ToInt64(fila.Cells[1].Text),
                cantidad = Convert.ToInt64(fila.Cells[2].Text),
                total = Convert.ToInt64(fila.Cells[3].Text.Replace(".", ""))
            };
            listaE.Add(ArqueoDet);
            Session["DatosEfectivo"] = listaE;
            if (ArqueoDet.id_det_arqueo != 0 && ArqueoDet.id_arqueo != 0 && ArqueoDet.denominacion != 0 && ArqueoDet.cantidad != 0 && ArqueoDet.total != 0 && !ArqueoDet.tipo_efectivo.Equals(null))
            {
                listaDetalle.Add(ArqueoDet);
            }
        }

        Session["lstEfectivo"] = listaDetalle;
    }
    protected void btnImprimir_Click(object sender, EventArgs e)
    {

        try
        {
            //Insertar arqueo caja menor 
            AreasCaj vAreasC = new AreasCaj();
            SoporteCaj SoporteArqueo = new SoporteCaj();
            ArqueoDetalle ArqueoDet = new ArqueoDetalle();
            List<ArqueoDetalle> listaDetalle = new List<ArqueoDetalle>();

            vAreasC = AreasCajServicio.ConsultarCajaMenor(Convert.ToInt32(Usuario.codusuario), Usuario);
            //Objeto tipo ArqueoCaj 
            ArqueoCaj vArqueoCaj = new ArqueoCaj()
            {
                id_arqueo = null,
                fecha_arqueo = DateTime.Now,
                total_arqueo = 0,
                cod_usuario = Usuario.codusuario,
                idarea = vAreasC.idarea,
            };
            //Lista del efectivo
            ObtenerLstEfectivo();
            listaDetalle = (List<ArqueoDetalle>)Session["lstEfectivo"];
            //Servicio de CrearDetalleArqueo
            ArqueoDet = AreasCajServicio.CrearDetalleArqueo(listaDetalle, vArqueoCaj, (Usuario)Session["usuario"]);
            //Servicio para asignar el id_arqueo a los soportes de caja menor
            SoporteArqueo = SoporteCajServicio.ActualizarSoporteArqueo(vArqueoCaj.idarea, vArqueoCaj.id_arqueo, (Usuario)Session["usuario"]);
            //Información para el reporte

            ArqueoCajaMenor arqueo = ArqueoCajServicio.ReporteArqueoCaj(Convert.ToInt32(vArqueoCaj.id_arqueo), Usuario);
            ArqueoCajaMenor resumenArqueo = new ArqueoCajaMenor();

            DataTable tableEfectivo = tableEfectivo = CreateDataRowEfectivo(arqueo.lista_extracto_efectivo);
            DataTable tableLegalizados = tableLegalizados = CreateDataRowDocLegalizados(arqueo.lista_extracto_legalizados);
            DataTable tableNoLegalizados = tableNoLegalizados = CreateDataRowDocNoLegalizados(arqueo.lista_extracto_no_legalizados);
            resumenArqueo = arqueo.resumenArqueo;
            AreasCajServicio.ModificarArqueoCaja(Convert.ToInt64(vArqueoCaj.id_arqueo),resumenArqueo.total_legalizados, (Usuario)Session["usuario"]);

            Usuario pUsuario = Usuario;
            rvArqueoCaj.LocalReport.ReportEmbeddedResource = "Page/Tesoreria/AreaCajaMenor/ArqueoCajaMenor.rdlc";
            rvArqueoCaj.LocalReport.DataSources.Clear();
            ReportParameter[] parametros = new ReportParameter[15]
            {
                new ReportParameter("NombreEntidad", pUsuario.empresa),
                new ReportParameter("NITEmpresa", pUsuario.nitempresa),
                new ReportParameter("CodigoCaja", vAreasC.idarea.ToString()),
                new ReportParameter("FechaConstitucion", vAreasC.fecha_constitucion.ToShortDateString()),
                new ReportParameter("Descripcion", vAreasC.nombre.ToString() ),
                new ReportParameter("Responsable", pUsuario.nombre),
                new ReportParameter("ValorBase", vAreasC.base_valor.ToString()),
                new ReportParameter("MontoMinimo", vAreasC.valor_minimo.ToString()),
                new ReportParameter("RutaImagen", new Uri(Server.MapPath("~/Images/LogoEmpresa.jpg")).AbsoluteUri),
                new ReportParameter("FechaGeneracion", DateTime.Now.ToShortDateString()),
                new ReportParameter("GastosLeg", resumenArqueo.total_legalizados.ToString()),
                new ReportParameter("GastosNoLeg", resumenArqueo.total_no_legalizados.ToString()),
                new ReportParameter("TotalGastos", resumenArqueo.total_gastos.ToString()),
                new ReportParameter("TotalEfectivo", resumenArqueo.total_efectivo.ToString()),
                new ReportParameter("Diferencia", resumenArqueo.diferencia.ToString())
            };
            rvArqueoCaj.LocalReport.EnableExternalImages = true;
            rvArqueoCaj.LocalReport.SetParameters(parametros);

            if(tableEfectivo != null)
            {
                ReportDataSource RDEfectivo = new ReportDataSource("DataSetEfectivo", tableEfectivo);
                rvArqueoCaj.LocalReport.DataSources.Add(RDEfectivo);
            }
            if(tableLegalizados != null)
            {
                ReportDataSource RDLegalizados = new ReportDataSource("DataSetLegalizados", tableLegalizados);
                rvArqueoCaj.LocalReport.DataSources.Add(RDLegalizados);
            }
            if(tableNoLegalizados != null)
            {
                ReportDataSource RDNoLegalizados = new ReportDataSource("DataSetNoLegalizados", tableNoLegalizados);
                rvArqueoCaj.LocalReport.DataSources.Add(RDNoLegalizados);
            }

            rvArqueoCaj.LocalReport.Refresh();
            mvPrincipal.SetActiveView(vReportearqueo);
            Site toolbar = (Site)Master;
            toolbar.MostrarCancelar(true);
            toolbar.MostrarImprimir(false);
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ArqueoCajServicio.CodigoPrograma, "btnImprimir_Click", ex);
        }
    }

    DataTable CreateDataRowDocLegalizados(List<ArqueoCajaMenor> arqueo)
    {
        DataTable table = new DataTable();
        table.Columns.Add("Id_soporte");
        table.Columns.Add("Fecha");
        table.Columns.Add("Persona");
        table.Columns.Add("Valor");
        
        foreach(ArqueoCajaMenor arqueoCaj in arqueo)
        {
            DataRow datos = table.NewRow();
            datos[0] = arqueoCaj.id_soporte;
            datos[1] = arqueoCaj.fecha.ToShortDateString();
            datos[2] = arqueoCaj.persona;
            datos[3] = arqueoCaj.valor;

            table.Rows.Add(datos);
        }
        if (table.Rows.Count == 0)
        {
            CreateEmptyDataRowDocLegalizados(table);
        }

        return table;
    }
    void CreateEmptyDataRowDocLegalizados(DataTable table)
    {
        DataRow datos = table.NewRow();

        datos[0] = "0";
        datos[1] = "";
        datos[2] = "";
        datos[3] = "0";

        table.Rows.Add(datos);
    }
    DataTable CreateDataRowDocNoLegalizados(List<ArqueoCajaMenor> arqueo)
    {
        DataTable table = new DataTable();
        table.Columns.Add("Id_soporte");
        table.Columns.Add("Fecha");
        table.Columns.Add("Persona");
        table.Columns.Add("Valor");

        foreach (ArqueoCajaMenor arqueoCaj in arqueo)
        {
            DataRow datos = table.NewRow();
            datos[0] = arqueoCaj.id_soporte;
            datos[1] = arqueoCaj.fecha.ToShortDateString();
            datos[2] = arqueoCaj.persona;
            datos[3] = arqueoCaj.valor;

            table.Rows.Add(datos);
        }
        if (table.Rows.Count == 0)
        {
            CreateEmptyDataRowDocNoLegalizados(table);
        }

        return table;
    }
    void CreateEmptyDataRowDocNoLegalizados(DataTable table)
    {
        DataRow datos = table.NewRow();

        datos[0] = "0";
        datos[1] = "";
        datos[2] = "";
        datos[3] = "0";

        table.Rows.Add(datos);
    }
    DataTable CreateDataRowEfectivo(List<ArqueoCajaMenor> arqueo)
    {
        DataTable table = new DataTable();
        table.Columns.Add("Tipo_Efectivo");
        table.Columns.Add("Denominacion");
        table.Columns.Add("Cantidad");
        table.Columns.Add("Valor");

        foreach (ArqueoCajaMenor arqueoCaj in arqueo)
        {
            DataRow datos = table.NewRow();
            datos[0] = arqueoCaj.tipo_efectivo;
            datos[1] = arqueoCaj.denominacion;
            datos[2] = arqueoCaj.cantidad;
            datos[3] = arqueoCaj.valor;

            table.Rows.Add(datos);
        }
        if (table.Rows.Count == 0)
        {
            CreateEmptyDataRowEfectivo(table);
        }

        return table;
    }
    void CreateEmptyDataRowEfectivo(DataTable table)
    {
        DataRow datos = table.NewRow();

        datos[0] = "";
        datos[1] = "0";
        datos[2] = "0";
        datos[3] = "0";

        table.Rows.Add(datos);
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Site toolbar = (Site)Master;
        if(mvPrincipal.ActiveViewIndex == 1)
        {
            mvPrincipal.ActiveViewIndex = 0;
            toolbar.MostrarImprimir(true);
            toolbar.MostrarCancelar(false);
        } else if(mvPrincipal.ActiveViewIndex == 2)
        {
            mvPrincipal.ActiveViewIndex = 1;
        }
    }       

}