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
    private Xpinn.FabricaCreditos.Services.InformacionFinancieraService InformacionFinancieraServicio = new Xpinn.FabricaCreditos.Services.InformacionFinancieraService();
    
    Xpinn.FabricaCreditos.Entities.EstadosFinancieros vEstadosFinancieros = new Xpinn.FabricaCreditos.Entities.EstadosFinancieros();
    Xpinn.FabricaCreditos.Entities.InformacionFinanciera vInformacionFinanciera = new Xpinn.FabricaCreditos.Entities.InformacionFinanciera();
        
    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            ViewState["TipoInformacion"] = "";     
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
        //mvInfFinNegocio.ActiveViewIndex = 0;
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

        // Poner en negrilla y deshabilitar las columnas de totales

        if (e.Row.RowType == DataControlRowType.DataRow)
        {           
            //Cambia el codigo de la columna "Referencias" por texto             
            Label lblCod_cuenta = (Label)e.Row.Cells[0].FindControl("lblCod_cuenta");
            //string Tipo = e.Row.Cells[9].Text;
            
            switch (lblCod_cuenta.Text)
            {
                case "4":   // Costo de Ventas
                    e.Row.Cells[5].Font.Bold = true;
                    e.Row.Cells[6].Enabled = false;
                    break;
                case "5":   // Este es el valor de la utilidad bruta es la suma de las columnas 1 a 4                  
                    e.Row.Cells[5].Font.Bold = true;
                    e.Row.Cells[6].Enabled = false;
                    break;
                case "11":  // Total gastos operativos
                    e.Row.Cells[5].Font.Bold = true;
                    e.Row.Cells[6].Enabled = false;
                    break;
                case "12":
                    e.Row.Cells[5].Font.Bold = true;
                    e.Row.Cells[6].Enabled = false;
                    break;
                case "17":
                    e.Row.Cells[5].Font.Bold = true;
                    e.Row.Cells[6].Enabled = false;
                    break;
                case "18":                    
                    e.Row.Cells[5].Font.Bold = true;
                    e.Row.Cells[6].Enabled = false;
                    break;
                case "19":  // Ingresos
                    e.Row.Cells[5].Font.Bold = true;
                    decimalesGrid Valor19 = (decimalesGrid)e.Row.Cells[6].FindControl("txtValor");
                    Valor19.Visible = false;
                    break;
                case "26":  // Total Ingresos
                    e.Row.Cells[5].Font.Bold = true;
                    e.Row.Cells[6].Enabled = false;             
                    break;
                case "27":  // Egresos
                    e.Row.Cells[5].Font.Bold = true;
                    decimalesGrid Valor27 = (decimalesGrid)e.Row.Cells[6].FindControl("txtValor");
                    Valor27.Visible = false;
                    break;
                case "35":  // Total Egresos
                    e.Row.Cells[5].Font.Bold = true;
                    e.Row.Cells[6].Enabled = false;
                    break;
                case "36":  // Disponible
                    e.Row.Cells[5].Font.Bold = true;
                    e.Row.Cells[6].Enabled = false;
                    break;
                case "37":  // 75% Disponible
                    e.Row.Cells[5].Font.Bold = true;
                    e.Row.Cells[6].Enabled = false;
                    break;
                case "38":  // 50 % Disponible
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
                //Si no encuentra registros debe crear la estructura (Insertar los 69 registros para manejar la informacion financiera)
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
            //Crear el registro del cual se toma el cod_inffin
            vInformacionFinanciera.cod_inffin = 0;
            vInformacionFinanciera.cod_persona = Convert.ToInt64(Session["Cod_persona"].ToString());
            vInformacionFinanciera.fecha = DateTime.Now;

            InformacionFinancieraServicio.CrearInformacionFinanciera(vInformacionFinanciera, (Usuario)Session["usuario"]);
            Session["Cod_InfFin"] = vInformacionFinanciera.cod_inffin;
        }
             
        vEstadosFinancieros.cod_inffin = Convert.ToInt64(Session["Cod_InfFin"].ToString());//Convert.ToInt64(txtCod_inffin.Text.Trim());
        
        if(txtCod_cuenta.Text.Trim() != "")
            vEstadosFinancieros.cod_cuenta = Convert.ToInt64(txtCod_cuenta.Text.Trim());
  
        string TipoInfo = ViewState["TipoInformacion"].ToString();
        if (TipoInfo == "N" || TipoInfo == "")
        {
            lblTipoInformacion.Text = "INFORMACION DEL NEGOCIO";
            vEstadosFinancieros.tipoInformacion = "N";
        }
        else
        {
            lblTipoInformacion.Text = "INFORMACION FAMILIAR";
            vEstadosFinancieros.tipoInformacion = "F";
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

                //Determinar los datos
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
            // Recalcular totales
            if (txtCod_balance.Text != "") vEstadosFinancieros.cod_balance = Convert.ToInt64(txtCod_balance.Text.Trim().Replace(@".", ""));
            vEstadosFinancieros.cod_persona = Convert.ToInt64(Session["Cod_persona"].ToString());
            EstadosFinancierosServicio.RecalcularEstadosFinancieros(vEstadosFinancieros, (Usuario)Session["usuario"]);
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
   

    protected void btnInfoNegocio_Click(object sender, EventArgs e)
    {
        ViewState["TipoInformacion"] = "N";
        lblTipoInformacion.Text = "INFORMACION DEL NEGOCIO";
        Actualizar();

    }

    protected void btnInfoFamiliar_Click(object sender, EventArgs e)
    {
        ViewState["TipoInformacion"] = "F";
        lblTipoInformacion.Text = "INFORMACION FAMILIAR";
        Actualizar();
    }

}