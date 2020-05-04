using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Text;
using Microsoft.Reporting.WebForms;
using System.Globalization;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Tesoreria.Services;
using Xpinn.Tesoreria.Entities;
using Xpinn.Util;
using System.IO;
using System.Configuration;
using System.Globalization;
using System.Text;
using System.Web.UI.HtmlControls;

public partial class Nuevo : GlobalWeb
{
    Xpinn.Caja.Services.ArqueoCajaService arqueoCajaService = new Xpinn.Caja.Services.ArqueoCajaService();
    Xpinn.Caja.Entities.ArqueoCaja arqueoCaja = new Xpinn.Caja.Entities.ArqueoCaja();

    List<Xpinn.Caja.Entities.MovimientoCaja> lstSaldos = new List<Xpinn.Caja.Entities.MovimientoCaja>();
    Usuario user = new Usuario();
    System.Data.DataTable table = new System.Data.DataTable();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            VisualizarOpciones(arqueoCajaService.CodigoProgramaArqueoCaja, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoCancelar += btnCancelar_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoImprimir += btnReporte_Click;
            toolBar.eventoExportar += btnExportar_Click;
            toolBar.MostrarCancelar(false);
            toolBar.MostrarImprimir(false);
            toolBar.MostrarExportar(false);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(arqueoCajaService.GetType().Name + "A", "Page_PreInit", ex);
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
          
            if (!Page.IsPostBack)
            {
                CargarDropDowns();
                user = (Usuario)Session["usuario"]; 
                Session["lstSaldos"] = null;
                mvPrincipal.ActiveViewIndex = 0;


                Session["FechaArqueo"] = "01/01/1900";
                //se inicializa la informacion ppal del Cierre: Caja, Cajero, Oficina, Fecha Cierre                
                Xpinn.Caja.Services.OficinaService oficinaServicio = new Xpinn.Caja.Services.OficinaService();
                Xpinn.Caja.Entities.Oficina oficina = new Xpinn.Caja.Entities.Oficina();
                oficina = oficinaServicio.ConsultarOficina(user.cod_oficina, user);
                this.txtFechaArqueo.Text = Convert.ToString(DateTime.Now.ToShortDateString());

            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(arqueoCajaService.GetType().Name + "A", "Page_Load", ex);
        }
    }


    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        
        
        VerError("");
        ActualizarSaldos();
        
    }

    protected void btnCancelar_Click(object sender, ImageClickEventArgs e)
    {
            mvPrincipal.ActiveViewIndex = 0;
            Site toolBar = (Site)this.Master;
            toolBar.MostrarCancelar(false);
    }

    
    private Xpinn.Caja.Entities.MovimientoCaja ObtenerValoresSaldos()
    {
        user = (Usuario)Session["Usuario"];
        Xpinn.Caja.Entities.MovimientoCaja movCaja = new Xpinn.Caja.Entities.MovimientoCaja();
        movCaja.cod_usuario = user.codusuario;
        movCaja.fechacierre = Convert.ToString(txtFechaArqueo.Text); 
        if (txtFechaArqueo.TieneDatos)
            movCaja.fechaCierre = txtFechaArqueo.ToDateTime; 
        Session["FechaArqueo"] = movCaja.fechaCierre;
        return movCaja;
    }

    protected void btnGenerarArqueo_Click(object sender, EventArgs e)
    {
        VerError("");
        ActualizarSaldos();
    }
    protected string obtfiltro()
    {
        string filtrar = "";
        ConnectionDataBase conexion = new ConnectionDataBase();

        Configuracion conf = new Configuracion();

        if (txtFechaArqueo.Text != "" && txtfechafinal.Text != "")
        { 
            if (conexion.TipoConexion() == "ORACLE")
            {
                filtrar += " and ARQUEOCAJA_DETALLE.fecha between to_date('" + txtFechaArqueo.ToDateTime.ToShortDateString() + "','" + gFormatoFecha + "')" + " and  to_date('" + txtfechafinal.ToDateTime.ToShortDateString() + "','" + gFormatoFecha + "')";
            }
            else
            {
                filtrar += " and ARQUEOCAJA_DETALLE.fecha between to_date('" + txtFechaArqueo.ToDateTime.ToShortDateString() + "','" + gFormatoFecha + "')" + " and  to_date('" + txtfechafinal.ToDateTime.ToShortDateString() + "','" + gFormatoFecha + "')";
            }
       }
        if (ddloficina.SelectedText != "")
            filtrar += " and usuarios.cod_oficina in(" + ddloficina.SelectedValue + ")";
        if (ddlusuarios.SelectedText != "")
            filtrar += " and ARQUEOCAJA_DETALLE.cod_cajero in(" + ddlusuarios.SelectedValue + ")";
        return filtrar;    

      
    }

    public void ActualizarSaldos()
    {
        try
        {
           Usuario pusu= (Usuario)Session["usuario"];
            if (txtFechaArqueo.Text == "")
            {
                VerError("Coloque la fecha del arqueo");
                return ;
            }
            if (txtfechafinal.Text == "")
            {
                VerError("Coloque la fecha Final");
                return ;
            }

            Site toolBar = (Site)this.Master;
            string filtro = obtfiltro();

            Xpinn.Caja.Services.MovimientoCajaService movCajaServicio = new Xpinn.Caja.Services.MovimientoCajaService();
          //  movCajaServicio.EliminarTempArqueoTesoreria(ObtenerValoresSaldos(), (Usuario)Session["usuario"]);

            //aqui va el metodo para realizar la insercion     
            //movCajaServicio.CrearTempArqueoTesoreria(ObtenerValoresSaldos(), (Usuario)Session["usuario"]);

            //este es el metodo que sirve para consultar los datos que hay en ese momento en la tabla de TempArqueoCaja
            lstSaldos = arqueoCajaService.consultararqueolista(ObtenerValoresSaldos(), (Usuario)Session["usuario"], filtro);

            //este guarda los datos de la tabla temarqueocaja y los guarda en una table para el reporte
            Xpinn.Caja.Entities.MovimientoCaja refe = new Xpinn.Caja.Entities.MovimientoCaja();
           
            table.Columns.Add("moneda");
            table.Columns.Add("concepto");
            table.Columns.Add("efectivo");
            table.Columns.Add("cheque");
            table.Columns.Add("consignacion");
            table.Columns.Add("datafono");
            table.Columns.Add("total");
           
            DataRow datarw;
            Xpinn.Caja.Entities.MovimientoCaja totales= new Xpinn.Caja.Entities.MovimientoCaja();
            if (lstSaldos.Count == 0)
            {
                datarw = table.NewRow();
                datarw[0] = " ";
                datarw[1] = " ";
                datarw[2] = " ";
                datarw[3] = " ";
                datarw[4] = " ";
                datarw[5] = " ";
                datarw[6] = " ";
              
                table.Rows.Add(datarw);
            }
            else
            {
                string fecha;
                foreach (Xpinn.Caja.Entities.MovimientoCaja saldos in lstSaldos)
                {
                    fecha = saldos.fec_ope.ToString();
               
                int contar = 0;
                for (int i = 0; i < lstSaldos.Count; i++)
                {
                    if(refe.fec_ope==Convert.ToDateTime(fecha))
                        {
                            contar=+1;
                        }
                    if (contar == 0)
                    {
                        datarw = table.NewRow();
                        refe = lstSaldos[i];
                        datarw[0] = " " + refe.nom_moneda;
                        datarw[1] = " " + refe.concepto;
                        datarw[2] = " " + refe.efectivo.ToString("0,0");
                        datarw[3] = " " + refe.cheque.ToString("0,0");
                        datarw[4] = " " + refe.consignacion.ToString("0,0");
                        datarw[5] = " " + refe.datafono.ToString("0,0");
                        datarw[6] = " " + refe.total.ToString("0,0");

                        table.Rows.Add(datarw);
                           if (refe.concepto == "Saldo Final")
                        {
                            contar = 0;
                            datarw = table.NewRow();
                            datarw[0] = "TOTAL:";
                            datarw[1] = " ";
                            datarw[2] = " ";
                            datarw[3] = " ";
                            datarw[4] = " ";
                            datarw[5] = " ";
                            datarw[6] = " " + refe.total.ToString("##,##");

                            table.Rows.Add(datarw);
                            contar = 0;

                            datarw = table.NewRow();
                            datarw[0] = " ";
                            datarw[1] = " ";
                            datarw[2] = " ";
                            datarw[3] = " ";
                            datarw[4] = " ";
                            datarw[5] = " ";
                            datarw[6] = " ";

                            table.Rows.Add(datarw);

                            contar = 0;
                            datarw = table.NewRow();
                            datarw[0] = "OFICINA:";
                            datarw[1] = " " + pusu.nombre_oficina;
                            datarw[2] = "USUARIO: ";
                            datarw[3] = pusu.nombre;
                            datarw[4] = " ";
                            datarw[5] = " ";
                            datarw[6] = " ";

                            table.Rows.Add(datarw);
                            contar = 0;
                        }
                    
                        
                    }
                    else
                    {
                        datarw = table.NewRow();
                        refe = lstSaldos[i];
                        datarw[0] = " " + refe.nom_moneda;
                        datarw[1] = " " + refe.concepto;
                        datarw[2] = " " + refe.efectivo.ToString("0,0");
                        datarw[3] = " " + refe.cheque.ToString("0,0");
                        datarw[4] = " " + refe.consignacion.ToString("0,0");
                        datarw[5] = " " + refe.datafono.ToString("0,0");
                        datarw[6] = " " + refe.total.ToString("0,0");

                        table.Rows.Add(datarw);
                        if (refe.concepto == "Saldo Final")
                        {
                            contar = 0;
                            datarw = table.NewRow();
                            datarw[0] = "TOTAL:";
                            datarw[1] = " ";
                            datarw[2] = " ";
                            datarw[3] = " ";
                            datarw[4] = " ";
                            datarw[5] = " ";
                            datarw[6] = " " + refe.total.ToString("##,##");

                            table.Rows.Add(datarw);
                            contar = 0;

                            datarw = table.NewRow();
                            datarw[0] = " ";
                            datarw[1] = " ";
                            datarw[2] = " ";
                            datarw[3] = " ";
                            datarw[4] = " ";
                            datarw[5] = " ";
                            datarw[6] = " ";

                            table.Rows.Add(datarw);

                            contar = 0;
                            datarw = table.NewRow();
                            datarw[0] = "OFICINA:";
                            datarw[1] = " " + pusu.nombre_oficina;
                            datarw[2] = "USUARIO: ";
                            datarw[3] = pusu.nombre;
                            datarw[4] = " ";
                            datarw[5] = " ";
                            datarw[6] = " ";

                            table.Rows.Add(datarw);
                            contar = 0;
                        }
                    }
                }
                }
                datarw = table.NewRow();
                datarw[0] = " ";
                datarw[1] = " ";
                datarw[2] = " ";
                datarw[3] = " ";
                datarw[4] = " ";
                datarw[5] = " ";
                datarw[6] = " ";

                table.Rows.Add(datarw);
                lstSaldos.Add(totales);
            }
            rptArqueoPagos.LocalReport.DataSources.Clear();
            ReportDataSource rds1 = new ReportDataSource("DataSet1", table);
            rptArqueoPagos.LocalReport.DataSources.Add(rds1);
            rptArqueoPagos.LocalReport.Refresh();

            arqueoCaja.cod_cajero = user.codusuario;
            arqueoCaja.fecha_cierre =Convert.ToDateTime(txtFechaArqueo.Text);


            gvSaldos.DataSource = lstSaldos;
            Session["lstSaldos"] = lstSaldos;
            if (lstSaldos.Count > 0)
            {
                gvSaldos.Visible = true;
                gvSaldos.DataBind();
                toolBar.MostrarImprimir(true);
                toolBar.MostrarExportar(true);
            }
            else
            {
                gvSaldos.Visible = false;
                toolBar.MostrarImprimir(false);
                toolBar.MostrarExportar(false);
            }

            Session.Add(movCajaServicio.GetType().Name + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(arqueoCajaService.GetType().Name + "L", "Actualizar", ex);
        }
    }

    protected void btnExportar_Click(object sender, EventArgs e)
    {
        try
        {
            if (gvSaldos.Rows.Count > 0)
            {
                //                string style;
                //                style = @"<style> 
                //                    .gridHeader { background-color: #359af2; font-weight: bold; color: White; border: 1px solid #d7e6e9; text-align: center; } 
                //                    .gridItem   { border: 1px solid #d7e6e9; mso-number-format:\@; }  
                //                  </style>";
                StringBuilder sb = new StringBuilder();
                StringWriter sw = new StringWriter(sb);
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                Page pagina = new Page();
                dynamic form = new HtmlForm();
                gvSaldos.EnableViewState = false;
                pagina.EnableEventValidation = false;
                pagina.DesignerInitialize();
                pagina.Controls.Add(form);
                form.Controls.Add(gvSaldos);
                pagina.RenderControl(htw);
                Response.Clear();
                Response.Buffer = true;
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("Content-Disposition", "attachment;filename=ConsultaArqueos.xls");
                Response.Charset = "UTF-8";
                Response.ContentEncoding = Encoding.Default;
                Response.Write(sb.ToString());
                Response.End();
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch
        {
        }
    }

    protected void CargarDropDowns() 
    {
        Xpinn.FabricaCreditos.Services.OficinaService oficinaService = new Xpinn.FabricaCreditos.Services.OficinaService();
        Xpinn.FabricaCreditos.Entities.Oficina oficina = new Xpinn.FabricaCreditos.Entities.Oficina();

        Usuario usuap = (Usuario)Session["usuario"];

        int cod = Convert.ToInt32(usuap.codusuario);
        int consulta = oficinaService.UsuarioPuedeConsultarCreditosOficinas(cod, (Usuario)Session["Usuario"]);
        if (consulta >= 1)
        {
            ddloficina.DataSource = oficinaService.ListarOficinas(oficina, (Usuario)Session["usuario"]);
            ddloficina.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));
            ddloficina.DataTextField = "nombre";
            ddloficina.DataValueField = "codigo";
            ddloficina.DataBind();
            ddloficina.SelectedValue = Convert.ToString(usuap.cod_oficina);
        }
        else
        {
            ddloficina.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));
            ddloficina.Items.Insert(1, new ListItem(Convert.ToString(usuap.nombre_oficina), Convert.ToString(usuap.cod_oficina)));
            ddloficina.DataBind();
            ddloficina.SelectedValue = Convert.ToString(usuap.cod_oficina);
        }

        Xpinn.Asesores.Services.UsuarioAseService usuarioAseServicio = new Xpinn.Asesores.Services.UsuarioAseService();
        Xpinn.Asesores.Entities.UsuarioAse usuarioAse = new Xpinn.Asesores.Entities.UsuarioAse();
        ddlusuarios.DataSource = usuarioAseServicio.ListarUsuario(usuarioAse, (Usuario)Session["usuario"]);
        ddlusuarios.DataTextField = "nombre";
        ddlusuarios.DataValueField = "codusuario";
        ddlusuarios.DataBind();
        //ddlusuarios.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));
    }
    protected void btnVerCheques_Click(object sender, EventArgs e)
    {        
        VerError("");
        if (Convert.ToDateTime(Session["FechaArqueo"]).ToShortDateString() != "01/01/1900")
        {
            Site toolBar = (Site)this.Master;
            toolBar.MostrarCancelar(true);
            mvPrincipal.ActiveViewIndex = 1;
        }
        else
            VerError("Se debe consultar primero el Arqueo para despues consultar el detalle de los Cheques");

    }

    protected void btnReporte_Click(object sender, EventArgs e)
    {
        Usuario pusu = (Usuario)Session["usuario"];
        string filtro = obtfiltro();
        string cRutaDeImagen;
        cRutaDeImagen = Server.MapPath("~/Images\\") + "LogoEmpresa.jpg";

        Xpinn.Caja.Services.MovimientoCajaService movCajaServicio = new Xpinn.Caja.Services.MovimientoCajaService();

      
        lstSaldos = (List<Xpinn.Caja.Entities.MovimientoCaja>)Session["lstSaldos"];


      

        //este guarda los datos de la tabla temarqueocaja y los guarda en una table para el reporte
        Xpinn.Caja.Entities.MovimientoCaja refe = new Xpinn.Caja.Entities.MovimientoCaja();

        table.Columns.Add("moneda");
        table.Columns.Add("concepto");
        table.Columns.Add("efectivo");
        table.Columns.Add("cheque");
        table.Columns.Add("consignacion");
        table.Columns.Add("datafono");
        table.Columns.Add("total");
      
        DataRow datarw;
   
        
            string fecha;
            foreach (Xpinn.Caja.Entities.MovimientoCaja saldos in lstSaldos)
            {
                  datarw = table.NewRow();
                        
                  datarw[0] = " " + saldos.nom_moneda;
                  datarw[1] = " " + saldos.concepto;
                  datarw[2] = " " + saldos.efectivo.ToString("0,0");
                  datarw[3] = " " + saldos.cheque.ToString("0,0");
                  datarw[4] = " " + saldos.consignacion.ToString("0,0");
                  datarw[5] = " " + saldos.datafono.ToString("0,0");
                  datarw[6] = " " + saldos.total.ToString("0,0");

               table.Rows.Add(datarw);
            
        }

        Usuario pUsu = (Usuario)Session["usuario"];

        ReportParameter[] param = new ReportParameter[6];
        param[0] = new ReportParameter("entidad", pUsu.empresa);
        param[1] = new ReportParameter("nit", pUsu.nitempresa);
        param[2] = new ReportParameter("fecha", Convert.ToDateTime(txtFechaArqueo.Text).ToShortDateString());
        param[3] = new ReportParameter("usuario", pUsu.nombre);
        param[4] = new ReportParameter("oficina", pUsu.nombre_oficina);
        param[5] = new ReportParameter("ImagenReport", cRutaDeImagen);
        rptArqueoPagos.LocalReport.EnableExternalImages = true;
        rptArqueoPagos.LocalReport.SetParameters(param);
        
        rptArqueoPagos.LocalReport.DataSources.Clear();
        ReportDataSource rds1 = new ReportDataSource("DataSet1", table);
        rptArqueoPagos.LocalReport.DataSources.Add(rds1);
        rptArqueoPagos.LocalReport.Refresh();
        rptArqueoPagos.Visible = true;
        frmPrint.Visible = false;
        mvPrincipal.ActiveViewIndex = 1;

    }

    protected void btnImprime_Click(object sender, EventArgs e)
    {
        if (rptArqueoPagos.Visible == true)
        {
            //MOSTRAR REPORTE EN PANTALLA
            Warning[] warnings;
            string[] streamids;
            string mimeType;
            string encoding;
            string extension;
            byte[] bytes = rptArqueoPagos.LocalReport.Render("PDF", null, out mimeType,
                           out encoding, out extension, out streamids, out warnings);
            Usuario pUsuario = new Usuario();
            string pNomUsuario = pUsuario.nombre != "" && pUsuario.nombre != null ? "_" + pUsuario.nombre : "";
            FileStream fs = new FileStream(HttpContext.Current.Server.MapPath("output" + pNomUsuario + ".pdf"),
            FileMode.Create);
            fs.Write(bytes, 0, bytes.Length);
            fs.Close();
            Session["Archivo" + Usuario.codusuario] = Server.MapPath("output" + pNomUsuario + ".pdf");
            frmPrint.Visible = true;
            rptArqueoPagos.Visible = false;

        }
    }

    protected void btnCloseReg_Click(object sender, EventArgs e)
    {
        mvPrincipal.ActiveViewIndex = 0;
    }

}