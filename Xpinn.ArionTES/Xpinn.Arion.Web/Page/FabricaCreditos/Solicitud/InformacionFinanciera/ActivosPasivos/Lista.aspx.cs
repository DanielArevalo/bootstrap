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



partial class Lista : GlobalWeb
{    
    private Xpinn.FabricaCreditos.Services.EstadosFinancierosService EstadosFinancierosServicio = new Xpinn.FabricaCreditos.Services.EstadosFinancierosService();
    private Xpinn.FabricaCreditos.Services.VentasSemanalesService VentasSemanalesServicio = new Xpinn.FabricaCreditos.Services.VentasSemanalesService();
    private Xpinn.FabricaCreditos.Services.MargenVentasService MargenVentasServicio = new Xpinn.FabricaCreditos.Services.MargenVentasService();
    private Xpinn.FabricaCreditos.Services.InventarioMateriaPrimaService InventarioMateriaPrimaServicio = new Xpinn.FabricaCreditos.Services.InventarioMateriaPrimaService();
    private Xpinn.FabricaCreditos.Services.ProductosProcesoService ProductosProcesoServicio = new Xpinn.FabricaCreditos.Services.ProductosProcesoService();
    private Xpinn.FabricaCreditos.Services.ProductosTerminadosService ProductosTerminadosServicio = new Xpinn.FabricaCreditos.Services.ProductosTerminadosService();
    private Xpinn.FabricaCreditos.Services.BalanceFamiliaService BalanceFamiliaServicio = new Xpinn.FabricaCreditos.Services.BalanceFamiliaService();
    private Xpinn.FabricaCreditos.Services.InventarioActivoFijoService InventarioActivoFijoServicio = new Xpinn.FabricaCreditos.Services.InventarioActivoFijoService();
    private Xpinn.FabricaCreditos.Services.ComposicionPasivoService ComposicionPasivoServicio = new Xpinn.FabricaCreditos.Services.ComposicionPasivoService();
    private Xpinn.FabricaCreditos.Services.InformacionFinancieraService InformacionFinancieraServicio = new Xpinn.FabricaCreditos.Services.InformacionFinancieraService();
    
    Xpinn.FabricaCreditos.Entities.EstadosFinancieros vEstadosFinancieros = new Xpinn.FabricaCreditos.Entities.EstadosFinancieros();
    Xpinn.FabricaCreditos.Entities.InformacionFinanciera vInformacionFinanciera = new Xpinn.FabricaCreditos.Entities.InformacionFinanciera();
        
    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            ViewState["TipoInformacion"] = "A";     //Muestra los activos
            VisualizarOpciones(EstadosFinancierosServicio.CodigoPrograma, "L");
            Site2 toolBar = (Site2)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoGuardar += btnGuardar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(EstadosFinancierosServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        mvInfFinNegocio.ActiveViewIndex = 0;
        try
        {
            if (!IsPostBack)
            {
                CargarValoresConsulta(pConsulta, EstadosFinancierosServicio.CodigoPrograma);
                Actualizar();
                Guardar(); // Actualiza los calculos de la informacion financiera (Estacionalidad semanal, etc)
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(EstadosFinancierosServicio.CodigoPrograma, "Page_Load", ex);
        }
    }


    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        GuardarValoresConsulta(pConsulta, EstadosFinancierosServicio.CodigoPrograma);
        Guardar();
        Actualizar();
    }

    protected void gvLista_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            ConfirmarEliminarFila(e, "btnBorrar");
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(EstadosFinancierosServicio.CodigoPrograma + "L", "gvLista_RowDataBound", ex);
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {         
            Label lblCod_cuenta = (Label)e.Row.Cells[0].FindControl("lblCod_cuenta");
            
            switch (lblCod_cuenta.Text)
            {        
                case "39": // Activos Corrientes
                    e.Row.Cells[5].Font.Bold = true;
                    decimalesGrid Valor37 = (decimalesGrid)e.Row.Cells[6].FindControl("txtValor");
                    Valor37.Visible = false;
                    break;

                case "45": // Total Act. Corriente
                    e.Row.Cells[5].Font.Bold = true;
                    e.Row.Cells[6].Enabled = false;
                    break;

                case "46": // Activos Fijos
                    e.Row.Cells[5].Font.Bold = true;
                    decimalesGrid Valor44 = (decimalesGrid)e.Row.Cells[6].FindControl("txtValor");
                    Valor44.Visible = false;
                    break;

                case "51": // Total Activos Fijos
                    e.Row.Cells[5].Font.Bold = true;
                    e.Row.Cells[6].Enabled = false;
                    break;

                case "52": // Total Activos
                    e.Row.Cells[5].Font.Bold = true;
                    e.Row.Cells[6].Enabled = false;
                    break;

                case "53": // Pasivos Corrientes
                    e.Row.Cells[5].Font.Bold = true;
                    decimalesGrid Valor51 = (decimalesGrid)e.Row.Cells[6].FindControl("txtValor");
                    Valor51.Visible = false;
                    break;

                case "57": // Total Pasivos Corrientes
                    e.Row.Cells[5].Font.Bold = true;
                    e.Row.Cells[6].Enabled = false;
                    break;

                case "61": //  Total Pasivo Largo Plazo
                    e.Row.Cells[5].Font.Bold = true;
                    e.Row.Cells[6].Enabled = false;
                    break;

                case "62": // Total Pasivos
                    e.Row.Cells[5].Font.Bold = true;
                    e.Row.Cells[6].Enabled = false;
                    break;

                case "63": // Total Patrimonio
                    e.Row.Cells[5].Font.Bold = true;
                    e.Row.Cells[6].Enabled = false;
                    break;

                case "64": // Total Pasivo + Patrimonio
                    e.Row.Cells[5].Font.Bold = true;
                    e.Row.Cells[6].Enabled = false;
                    break;

            }
        }


    }

  

    protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvLista.PageIndex = e.NewPageIndex;
            Actualizar();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(EstadosFinancierosServicio.CodigoPrograma, "gvLista_PageIndexChanging", ex);
        }
    }

    private void Actualizar()
    {
        try
        {   
            List<Xpinn.FabricaCreditos.Entities.EstadosFinancieros> lstConsulta = new List<Xpinn.FabricaCreditos.Entities.EstadosFinancieros>();
            lstConsulta = EstadosFinancierosServicio.ListarEstadosFinancieros(ObtenerValores(), (Usuario)Session["usuario"]);

            gvLista.PageSize = 20;
            gvLista.EmptyDataText = "";
            gvLista.DataSource = lstConsulta;

            if (lstConsulta.Count > 0)
            {
                
                gvLista.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                gvLista.DataBind();
                ValidarPermisosGrilla(gvLista);
            }
            else
            {
                //Si no encuentra registros debe crear la estructura (Insertar los 39 registros para manejar la informacion financiera)
                EstadosFinancierosServicio.CrearEstadosFinancieros(vEstadosFinancieros, (Usuario)Session["usuario"]);
                
                //Muestra en el gridview los 39 registros
                lstConsulta = EstadosFinancierosServicio.ListarEstadosFinancieros(ObtenerValores(), (Usuario)Session["usuario"]);
                gvLista.DataSource = lstConsulta;
                gvLista.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                gvLista.DataBind();
                ValidarPermisosGrilla(gvLista);
            }

            Session.Add(EstadosFinancierosServicio.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(EstadosFinancierosServicio.CodigoPrograma, "Actualizar", ex);
        }
    }

    private Xpinn.FabricaCreditos.Entities.EstadosFinancieros ObtenerValores()
    {

        if (Session["Cod_InfFin"] == null)
        {
            //Crear registros en informacionfinanciera y en estadosfinancieros
            vInformacionFinanciera.cod_inffin = 0;
            vInformacionFinanciera.cod_persona = Convert.ToInt64(Session["Cod_persona"].ToString());
            vInformacionFinanciera.fecha = DateTime.Now;

            InformacionFinancieraServicio.CrearInformacionFinanciera(vInformacionFinanciera, (Usuario)Session["usuario"]);
            Session["Cod_InfFin"] = vInformacionFinanciera.cod_inffin;
        }
              
        vEstadosFinancieros.cod_inffin = Convert.ToInt64(Session["Cod_InfFin"].ToString());
        
        if(txtCod_cuenta.Text.Trim() != "")
            vEstadosFinancieros.cod_cuenta = Convert.ToInt64(txtCod_cuenta.Text.Trim());

        string TipoInfo = ViewState["TipoInformacion"].ToString();
        if (TipoInfo == "A") //Trae los activos
        {
            vEstadosFinancieros.tipoInformacion = "A";
        }
        else
        {
            vEstadosFinancieros.tipoInformacion = "P";
        }

        return vEstadosFinancieros;


    }

 
    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        Guardar();
    }
    
    private void Guardar()    
    {        
     try
        {           
            int NumFila = gvLista.Rows.Count;
            idObjeto = "";
            while (NumFila > 0)
            {
                NumFila--;
                idObjeto = gvLista.Rows[NumFila].Cells[0].Text;
                if (idObjeto != "")
                    vEstadosFinancieros = EstadosFinancierosServicio.ConsultarEstadosFinancieros(Convert.ToInt64(idObjeto), (Usuario)Session["usuario"]); //Trae la fila de cada cod_balance

                if (txtCod_balance.Text != "") vEstadosFinancieros.cod_balance = Convert.ToInt64(txtCod_balance.Text.Trim().Replace(@".", ""));
                if (txtCod_inffin.Text != "") vEstadosFinancieros.cod_inffin = Convert.ToInt64(txtCod_inffin.Text.Trim().Replace(@".", ""));
                if (txtCod_cuenta.Text != "") vEstadosFinancieros.cod_cuenta = Convert.ToInt64(txtCod_cuenta.Text.Trim().Replace(@".", ""));
                decimalesGrid Valor = (decimalesGrid)gvLista.Rows[NumFila].FindControl("txtValor");
                vEstadosFinancieros.valor = Convert.ToInt64(Valor.Text.Trim().Replace(@".", ""));

                //Crear nueva informacion financiera del negocio (Estado resultados, activos, patrimonio)
                if (txtCodCuenta.Text != "") vEstadosFinancieros.cod_cuenta = Convert.ToInt64(txtCodCuenta.Text.Trim().Replace(@".", ""));

                if (idObjeto != "")
                {
                    vEstadosFinancieros.cod_balance = Convert.ToInt64(idObjeto);
                    vEstadosFinancieros.cod_persona = Convert.ToInt64(Session["Cod_persona"].ToString());
                    EstadosFinancierosServicio.ModificarEstadosFinancieros(vEstadosFinancieros, (Usuario)Session["usuario"]);
                    Session[EstadosFinancierosServicio.CodigoPrograma + ".id"] = idObjeto;
                }
            }
                
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(EstadosFinancierosServicio.CodigoPrograma, "btnGuardar_Click", ex);
        }

        Actualizar();
}

    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            vEstadosFinancieros = EstadosFinancierosServicio.ConsultarEstadosFinancieros(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);

            if (vEstadosFinancieros.cod_balance != Int64.MinValue)
                txtCod_balance.Text = HttpUtility.HtmlDecode(vEstadosFinancieros.cod_balance.ToString().Trim().Replace(@".", "")); 
            if (vEstadosFinancieros.cod_inffin != Int64.MinValue)
                txtCod_inffin.Text = HttpUtility.HtmlDecode(vEstadosFinancieros.cod_inffin.ToString().Trim().Replace(@".", ""));
            if (vEstadosFinancieros.cod_cuenta != Int64.MinValue)
                txtCod_cuenta.Text = HttpUtility.HtmlDecode(vEstadosFinancieros.cod_cuenta.ToString().Trim().Replace(@".", ""));
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(EstadosFinancierosServicio.CodigoPrograma, "ObtenerDatos", ex);
        }
    }


    protected void grdContact_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

    }

   
    protected void btnInfoNegocio_Click(object sender, EventArgs e)
    {
        

    }
   

    protected void TextBox1_TextChanged(object sender, EventArgs e)
    {

    }
    protected void btnActivos_Click(object sender, EventArgs e)
    {
        ViewState["TipoInformacion"] = "A";
        vEstadosFinancieros.tipoInformacion = "A";
        Actualizar();
    }
    protected void btnPasivos_Click(object sender, EventArgs e)
    {
        ViewState["TipoInformacion"] = "P";
        vEstadosFinancieros.tipoInformacion = "P";
        Actualizar();
    }

}