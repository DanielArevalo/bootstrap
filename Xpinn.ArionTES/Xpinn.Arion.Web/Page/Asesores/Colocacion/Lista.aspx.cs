using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using Xpinn.FabricaCreditos.Services;
using Xpinn.FabricaCreditos.Entities;
using Xpinn.Asesores.Entities.Common;
using Xpinn.Asesores.Services;
using Xpinn.Asesores.Entities;
using System.IO;
using System.Text;
using Microsoft.Reporting.WebForms;

using System.Linq;

using System.Configuration;

public partial class Page_Asesores_Colocacion_Lista : GlobalWeb
{
    EstadoCuentaService serviceEstadoCuenta = new EstadoCuentaService();
    ClienteService clienteServicio = new ClienteService();
    CreditoService creditoServicio = new CreditoService();
    ExcelService excelServicio = new ExcelService();
    
    MovGralCreditoService movGrlService = new MovGralCreditoService();
    Usuario usuario = new Usuario();
    string cod_persona, numero_radicacion, usu, observacion;
    DateTime fecha;
    
    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(clienteServicio.CodigoProgramaReal, "L");
            Site toolBar = (Site)this.Master;
            //toolBar.eventoNuevo += btnNuevo_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(clienteServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                CargarValoresConsulta(pConsulta, clienteServicio.CodigoPrograma);
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                LlenarComboLineasCredito(ddlLinea);
                LlenarComboZona(ddlZona);

                if (Session[clienteServicio.CodigoPrograma + ".consulta"] != null)
                {
                    
                    
                }
            }
            if (ddlConsultar.SelectedIndex == 4 || ddlConsultar.SelectedIndex == 5)
            {
                lblOrdenar.Visible = true;
                ddlOrdenar.Visible = true;
            }
            else
            {
                lblOrdenar.Visible = false;
                ddlOrdenar.Visible = false;
            }
            
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(clienteServicio.CodigoPrograma, "Page_Load", ex);
        }
    }


   
    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        Page.Validate();
        if (Page.IsValid)
        {
            GuardarValoresConsulta(pConsulta, clienteServicio.CodigoPrograma);
            
            switch (ddlConsultar.SelectedIndex)
            {
                case 1: Session["op1"] = 3; Session["op2"] = 1;  
                        break;
                case 2: Session["op1"] = 1; Session["op2"] = 2;  
                        break;
                case 3: Session["op1"] = 1; Session["op2"] = 3;  
                        break;
                case 4: Session["op1"] = 2; Session["op2"] = 4;  
                        break;
                case 5: Session["op1"] = 2; Session["op2"] = 5;  
                        break;
            }
            Actualizar();
            
        }
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        LimpiarValoresConsulta(pConsulta, clienteServicio.CodigoPrograma);
        Session.Remove("op1");
        Session.Remove("op2");
    }
    
    protected void gvListaCreditos_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvListaCreditos.PageIndex = e.NewPageIndex;
            Actualizar();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(clienteServicio.CodigoPrograma, "gvListaCreditos_PageIndexChanging", ex);
        }
    }

    protected void gvListaClientes_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvListaClientes.PageIndex = e.NewPageIndex;
            Actualizar();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(clienteServicio.CodigoPrograma, "gvListaClientes_PageIndexChanging", ex);
        }
    }

    protected void LlenarComboLineasCredito(DropDownList ddlLinea)
    {
        LineasCreditoService lineaService = new LineasCreditoService();
        LineasCredito linea = new LineasCredito();
        ddlLinea.DataSource = lineaService.ListarLineasCredito(linea, (Usuario)Session["usuario"]);
        ddlLinea.DataTextField = "nombre";
        ddlLinea.DataValueField = "codigo";
        ddlLinea.DataBind();
        ddlLinea.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));
    }

    protected void LlenarComboZona(DropDownList ddlZona)
    {
          AgendaTipoActividadService tipoActividadServicio = new AgendaTipoActividadService();
          Usuario usuap = (Usuario)Session["usuario"];
          int cod =Convert.ToInt32(usuap.codusuario);
          int consulta = tipoActividadServicio.UsuarioEsAsesor(cod, (Usuario)Session["Usuario"]);
          if (consulta >= 1)
          {
              CiudadService ciudadService = new CiudadService();
              Ciudad zona = new Ciudad();
              zona.tipo = 5;
              ddlZona.DataSource = ciudadService.ListarCiudad(zona, (Usuario)Session["usuario"]);
              ddlZona.DataTextField = "NOMCIUDAD";
              ddlZona.DataValueField = "CODCIUDAD";
              ddlZona.DataBind();
              ddlZona.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));
          }
          else
          {

              ddlZona.Items.Insert(0, new ListItem(Convert.ToString(usuap.nombre_oficina), Convert.ToString(usuap.cod_oficina)));
              ddlZona.DataBind();
          }

    } 
    
    private void Actualizar()
    {
        try
        {
            String emptyQuery = "Fila de datos vacia";
            String filtro;
            String orden;
            switch ((int)Session["op1"])
            {
                case 1: 
                    List<Cliente> lstConsultaClientes = new List<Cliente>();
                    filtro = obtFiltro();
                    lstConsultaClientes = clienteServicio.ListarClientesEjecutivo(new Cliente(), (Usuario)Session["usuario"], (int)Session["op2"], filtro);
                    gvListaClientes.EmptyDataText = emptyQuery;
                    gvListaClientes.DataSource = lstConsultaClientes;
                    if (lstConsultaClientes.Count > 0)
                    {
                        mvLista.ActiveViewIndex = 0;
                        lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsultaClientes.Count.ToString();
                        gvListaClientes.DataBind();
                    }
                    else
                    {
                        mvLista.ActiveViewIndex = -1;
                    }
                    break;
                case 2: 
                    List<Credito> lstConsultaCreditos = new List<Credito>();
                    filtro = obtFiltro();
                    orden = obtOrden();
                    lstConsultaCreditos = creditoServicio.ListarCreditoAsesor(new Credito(), (Usuario)Session["usuario"], filtro, orden);
                    gvListaCreditos.EmptyDataText = emptyQuery;
                    gvListaCreditos.DataSource = lstConsultaCreditos;
                    if (lstConsultaCreditos.Count > 0)
                    {
                        mvLista.ActiveViewIndex = 2;      
                        lblTotalRegs2.Text = "<br/> Registros encontrados " + lstConsultaCreditos.Count.ToString();
                        gvListaCreditos.DataBind();
                    }
                    else
                    {
                        mvLista.ActiveViewIndex = -1;
                    }
                    break;
                case 3: 
                    List<Cliente> lstConsultaClientespotenciales = new List<Cliente>();
                    filtro = obtFiltro();
                    lstConsultaClientespotenciales = clienteServicio.ListarClientesEjecutivopotencial(new Cliente(), (Usuario)Session["usuario"], (int)Session["op2"], filtro);
                    gvListaClientespotenciales.EmptyDataText = emptyQuery;
                    gvListaClientespotenciales.DataSource = lstConsultaClientespotenciales;
                    if (lstConsultaClientespotenciales.Count > 0)
                    {
                        mvLista.SetActiveView(VGclientespotenciales);
                        lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsultaClientespotenciales.Count.ToString();
                        gvListaClientespotenciales.DataBind();
                    }
                    else
                    {
                        mvLista.ActiveViewIndex = -1;
                    }
                    break;
            }

            Session.Add(clienteServicio.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(clienteServicio.CodigoPrograma, "Actualizar", ex);
        }
    }

    private string obtFiltro()
    {
        AgendaTipoActividadService tipoActividadServicio = new AgendaTipoActividadService();
        Usuario usuap = (Usuario)Session["usuario"];
        int cod = Convert.ToInt32(usuap.codusuario);
        int consulta = tipoActividadServicio.UsuarioEsAsesor(cod, (Usuario)Session["Usuario"]);
        String filtro = String.Empty;
        if (consulta >= 1)
        {          
            if (ddlZona.SelectedIndex != 0)
                filtro += " and cod_zona = " + ddlZona.SelectedValue;
            if (ddlLinea.SelectedIndex != 0)
                filtro += " and cod_linea_credito = '" + ddlLinea.SelectedValue + "' ";
        }
        else 
        {
            if (ddlZona.SelectedIndex != 0)
                filtro += " and cod_zona = " + ddlZona.SelectedValue + "  and cod_asesor_com = " + usuap.codusuario;                        
            if (ddlLinea.SelectedIndex != 0)
                filtro += " and cod_linea_credito = '" + ddlLinea.SelectedValue + "'  and cod_asesor_com = " + usuap.codusuario;

            if (ddlLinea.SelectedIndex == 0 && ddlZona.SelectedIndex == 0)
                filtro += " and cod_asesor_com = " + usuap.codusuario;
        }
        if (!string.IsNullOrEmpty(filtro))
        {
            filtro = filtro.Substring(4);
            filtro = "where " + filtro;
        }
        return filtro;
    }

    private string obtOrden()
    {
        String orden = String.Empty;
        if (ddlOrdenar.SelectedIndex != 0)
            orden += " order by "+ ddlOrdenar.SelectedValue+" asc";
        return orden;
    }

    protected void btnPrintPagina_Click(object sender, EventArgs e)
    { 
        ReportParameter[] param = new ReportParameter[1];
        param[0] = new ReportParameter("pFecha", Convert.ToString(DateTime.Now));
        ReportViewer1.LocalReport.SetParameters(param);

        ReportDataSource rds = new ReportDataSource("DataSet1", CrearDataTable());
        ReportViewer1.LocalReport.DataSources.Clear();
        ReportViewer1.LocalReport.DataSources.Add(rds);
        ReportViewer1.LocalReport.Refresh();

        mvLista.ActiveViewIndex = 2;
    }

    public DataTable CrearDataTable()
    {
        String filtro = String.Empty;
        String orden = String.Empty;
        //System.Data.DataTable table = new System.Data.DataTable("DataTable1");
        System.Data.DataTable table = new System.Data.DataTable();
        switch ((int)Session["op1"])
        {
            case 1: List<Cliente> lstConsultaClientes = new List<Cliente>();
                    filtro = obtFiltro();
                    lstConsultaClientes = clienteServicio.ListarClientesEjecutivo(new Cliente(), (Usuario)Session["usuario"], (int)Session["op2"], filtro);

                    table.Columns.Add("Id_cliente");
                    table.Columns.Add("Nombre_completo");
                    table.Columns.Add("Direccion");
                    table.Columns.Add("Telefono");
                    table.Columns.Add("Codigo_zona");
                    table.Columns.Add("Calificacion");

                    foreach (Cliente item in lstConsultaClientes)
                    {
                        DataRow datarw;
                        datarw = table.NewRow();
                        datarw[0] = item.IdCliente;
                        datarw[1] = item.NombreCompleto;
                        datarw[2] = item.Direccion;
                        datarw[3] = item.Telefono;
                        datarw[4] = item.CodigoZona;
                        datarw[5] = item.Calificacion;
                        table.Rows.Add(datarw);
                    } 
                    break;
            case 2: List<Credito> lstConsultaCreditos = new List<Credito>();
                    filtro = obtFiltro();
                    orden = obtOrden();
                    lstConsultaCreditos = creditoServicio.ListarCreditoAsesor(new Credito(), (Usuario)Session["usuario"], filtro, orden);
                    
                    table.Columns.Add("Id_informe");
                    table.Columns.Add("Codigo_cliente");
                    table.Columns.Add("Numero_radicacion");
                    table.Columns.Add("Linea");
                    table.Columns.Add("Fecha_solicitud");
                    table.Columns.Add("Monto_aprobado");
                    table.Columns.Add("Saldo");
                    table.Columns.Add("Cuota");
                    table.Columns.Add("Atributos");
                    table.Columns.Add("Plazo");
                    table.Columns.Add("Cuotas_pagadas");
                    table.Columns.Add("Fecha_proximo_pago");
                    table.Columns.Add("Oficina");
                    table.Columns.Add("Calif_promedio");
                    table.Columns.Add("Calif_cliente");
                    table.Columns.Add("Renov_cuotas");
                    table.Columns.Add("Renov_montos");

                    foreach (Credito item in lstConsultaCreditos)
                    {
                        DataRow datarw;
                        datarw = table.NewRow();                        
                        datarw[0] = item.idinforme;
                        datarw[1] = item.codigo_cliente;
                        datarw[2] = item.numero_radicacion;
                        datarw[3] = item.linea_credito;
                        datarw[4] = item.fecha_solicitud_string;
                        datarw[5] = item.monto_aprobado;
                        datarw[6] = item.saldo_capital;
                        datarw[7] = item.valor_cuota;
                        datarw[8] = item.otros_saldos;
                        datarw[9] = item.plazo;
                        datarw[10] = item.cuotas_pagadas;
                        datarw[11] = item.fecha_prox_pago_string;
                        datarw[12] = item.oficina;
                        datarw[13] = item.calificacion_promedio;
                        datarw[14] = item.calificacion_cliente;
                        datarw[15] = item.porc_renovacion_cuotas;
                        datarw[16] = item.porc_renovacion_montos;
                        table.Rows.Add(datarw);
                    } 
                break;
        }
        return table;
    }

    protected void btnExportar_Click(object sender, EventArgs e)
    {
        excelServicio.ExportarExcel(CrearDataTable());
    }

    protected void btnExportar0_Click(object sender, EventArgs e)
    {
        excelServicio.ExportarExcel(CrearDataTable());
    }

    protected void btnInforme0_Click(object sender, EventArgs e)
    {
        ReportParameter[] param = new ReportParameter[1];
        param[0] = new ReportParameter("pFecha", Convert.ToString(DateTime.Now));
        ReportViewer2.LocalReport.SetParameters(param);

        ReportDataSource rds = new ReportDataSource("DataSet1", CrearDataTable());
        ReportViewer2.LocalReport.DataSources.Clear();
        ReportViewer2.LocalReport.DataSources.Add(rds);
        ReportViewer2.LocalReport.Refresh();

        mvLista.ActiveViewIndex = 3;
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Page/Asesores/GestionarAgenda/Lista.aspx");
    }
     protected void gvGarantias_RowEditing(object sender, GridViewCommandEventArgs evt)
    {
         
        Producto producto = new Producto();

        
            producto.Persona.IdPersona = Convert.ToInt64(evt.CommandArgument.ToString());
            Session[MOV_GRAL_CRED_PRODUC] = producto;
            Navegar("~/Page/Asesores/EstadoCuenta/Detalle.aspx");
        

        /*if (evt.CommandName == "Movimimento"){
            String[] tmp = evt.CommandArgument.ToString().Split('|');
            producto.CodLineaCredito = Convert.ToInt64(tmp[1]);
            producto.Persona.IdPersona = Convert.ToInt64(tmp[0]);
            Session[MOV_GRAL_CRED_PRODUC] = producto;
            Navegar("~/Page/Asesores/EstadoCuenta/Creditos/MovimientoGeneral/Detalle.aspx");

        }*/

    

        //Actualizar();
        //String id = gvListaClientes.Rows[e.NewEditIndex].Cells[0].Text;
        //Session[MOV_GRAL_CRED_PRODUC] = id;
        //Navegar("~/Page/Asesores/EstadoCuenta/Detalle.aspx");
          

        
    }
     protected void gvGarantias_PageIndexChanging(object sender, GridViewPageEventArgs e)
     {
         
             gvListaClientes.PageIndex = e.NewPageIndex;
             Actualizar();
         
         
     }
     protected void gvLista_RowCommand(object sender, GridViewCommandEventArgs evt)
     {
         if (evt.CommandName == "EstadoCuenta")
         {
             Producto producto = new Producto();
             producto.Persona.IdPersona = Convert.ToInt64(evt.CommandArgument.ToString());
             Session[MOV_GRAL_CRED_PRODUC] = producto;
             Navegar("~/Page/Asesores/EstadoCuenta/Detalle.aspx");
         }
         if (evt.CommandName == "Observacion")
         {
             mpeNuevo.Show();
             Txtcliente.Enabled=false;
             Txtidentificacion.Enabled=false;
             Txtfecha.Enabled=false;
             Producto producto = new Producto();
             producto.Persona.IdPersona = Convert.ToInt64(evt.CommandArgument.ToString());
             long id = Convert.ToInt64(evt.CommandArgument.ToString());
             Label1.Text = Convert.ToString(id);
             producto.Persona = serviceEstadoCuenta.ConsultarPersona(Convert.ToInt64(producto.Persona.IdPersona), usuario);
             Txtcliente.Text = producto.Persona.PrimerNombre + " " + producto.Persona.SegundoNombre+" " +producto.Persona.PrimerApellido+" " +producto.Persona.SegundoApellido;
             Txtidentificacion.Text = producto.Persona.NumeroDocumento;
             Txtfecha.Text = DateTime.Today.ToString("dd/MM/yyyy");
             cod_persona = Convert.ToString(producto.Persona.IdPersona);
             numero_radicacion = Convert.ToString(producto.Persona.NoCredito);
         }
     }

     protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
     {
         //String id = gvListaClientes.SelectedRow.Cells[0].Text;
         //Session[serviceEstadocuenta.CodigoPrograma + ".id"] = id;
         //Navegar(Pagina.Detalle);
     }

     protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
     {
         try
         {
             gvListaClientes.PageIndex = e.NewPageIndex;
             Actualizar();
         }
         catch
         {
             //BOexcepcion.Throw(serviceEstadocuenta.GetType().Name + "L", "gvLstMoviGralCredito_PageIndexChanging", ex);
         }
     }
     protected void btnGuardarReg_Click(object sender, EventArgs e)
     {

     }
     protected void btnCloseReg2_Click(object sender, EventArgs e)
     {
        
         
     }
     protected void btnAgregar_Click1(object sender, EventArgs e)
     {

     }




     protected void btnGuardarReg_Click1(object sender, EventArgs e)
     {
         if (Convert.ToInt64(Session["op1"]) == 3)
         {
             Usuario usuap = (Usuario)Session["usuario"];
             usu = (usuap.nombre).ToString();
             Producto producto = new Producto();
             producto.Persona.IdPersona = Convert.ToInt64(Label1.Text);
             producto.Persona = serviceEstadoCuenta.ConsultarPersona(Convert.ToInt64(producto.Persona.IdPersona), usuario);
             fecha = DateTime.Today;
             cod_persona = Label1.Text;
             numero_radicacion = Convert.ToString(producto.Persona.NoCredito);
             observacion = txtDescripcionReg.Text;
             int tipo = 0;

             clienteServicio.guardarobservacion((Usuario)Session["usuario"], fecha, observacion, cod_persona, numero_radicacion, usu, tipo);
         }
         else
         {
             Usuario usuap = (Usuario)Session["usuario"];
             usu = (usuap.nombre).ToString();
             Producto producto = new Producto();
             producto.Persona.IdPersona = Convert.ToInt64(Label1.Text);
             producto.Persona = serviceEstadoCuenta.ConsultarPersona(Convert.ToInt64(producto.Persona.IdPersona), usuario);
             fecha = DateTime.Today;
             cod_persona = Convert.ToString(producto.Persona.IdPersona);
             numero_radicacion = Convert.ToString(producto.Persona.NoCredito);

             observacion = txtDescripcionReg.Text;
             int tipo=1;

             clienteServicio.guardarobservacion((Usuario)Session["usuario"], fecha, observacion, cod_persona, numero_radicacion, usu, tipo);
         }
         txtDescripcionReg.Text = null;
         Txtcliente.Text = null;
         Txtfecha.Text = null;
         Txtidentificacion.Text = null;
     }
     protected void ddlConsultar_SelectedIndexChanged(object sender, EventArgs e)
     {
         if (ddlConsultar.SelectedValue == "1")
         {
             ddlLinea.Visible = false;
             lblLinea.Visible = false;
         }
         else {
             ddlLinea.Visible = true;
             lblLinea.Visible = true;
         }
     }
}