using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Xpinn.Util;
using System.Data;
using System.Data.Common;
using System.Text;
using System.IO;
using Microsoft.Reporting.WebForms;
using Xpinn.Comun.Entities;

public partial class Lista : GlobalWeb
{

    Xpinn.Caja.Services.MovimientoCajaService movCajaServicio = new Xpinn.Caja.Services.MovimientoCajaService();
    Xpinn.Caja.Entities.MovimientoCaja movCaja = new Xpinn.Caja.Entities.MovimientoCaja();

    Xpinn.Caja.Services.CajeroService cajeroServicio = new Xpinn.Caja.Services.CajeroService();
    Xpinn.Caja.Entities.Cajero cajero = new Xpinn.Caja.Entities.Cajero();

    Xpinn.Caja.Services.TransaccionCajaService tranCajaServicio = new Xpinn.Caja.Services.TransaccionCajaService();
    Xpinn.Caja.Entities.TransaccionCaja transac = new Xpinn.Caja.Entities.TransaccionCaja();
    Usuario user = new Usuario();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            VisualizarOpciones(movCajaServicio.CodigoPrograma4, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoCancelar += btnCancelar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(movCajaServicio.GetType().Name + "A", "Page_PreInit", ex);
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //se valida que el usuario conectado sea Cajero Principal o no
            cajero = cajeroServicio.ConsultarIfUserIsntCajeroPrincipal(0, (Usuario)Session["usuario"]);
            Session["estadoCaj"] = cajero.estado;
            Session["estadoOfi"] = cajero.estado_ofi;// estado Oficina

            ImprimirGrilla();
            if (!IsPostBack)
            {
                btnReporte.Visible = false;
                lblTotal.Visible = false;
                txtTotalMovs.Visible = false;
                Session["Oficina"] = cajero.cod_oficina;
                Session["Caja"] = cajero.cod_caja;
                Session["Cajero"] = cajero.cod_cajero;
                Session["conteo"] = cajero.conteo;

                long cod_caja = long.Parse(Session["Caja"].ToString());
                General general = ConsultarParametroGeneral(101);
                long cod_perfil_permisos = 0;
                Usuario pUsuario = (Usuario)Session["usuario"];
                // Si el parametro general es diferente a nulo, no esta vacio, y es un numero valido entro
                LlenarComboOficinas(ddlOficinas);
                LlenarComboMonedas(ddlMonedas);
                if (general != null && !string.IsNullOrWhiteSpace(general.valor) && long.TryParse(general.valor, out cod_perfil_permisos))
                {
                    if (general.valor == pUsuario.codperfil.ToString())
                    {
                        // se inicializan los combos                         
                        LlenarComboCajas(ddlCajas);                        
                    }
                    else
                    {
                        LlenarComboCajasCajero(ddlCajas);
                    }
                }
                else
                {
                    // se inicializan los combos 
                    LlenarComboCajas(ddlCajas);
                }
            }

        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(movCajaServicio.GetType().Name + "A", "Page_Load", ex);
        }

    }

    protected void btnCancelar_Click(object sender, ImageClickEventArgs e)
    {
        Navegar("../../../General/Global/inicio.aspx");
    }

    protected void ImprimirGrilla()
    {
        if (long.Parse(Session["estadoOfi"].ToString()) == 1)
        {
            if (long.Parse(Session["estadoCaj"].ToString()) == 1)
            {
                string printScript =
                @"function PrintGridView()
                {
                 div = document.getElementById('DivButtons');
                 div.style.display='none';
                 var gridInsideDiv = document.getElementById('gvDiv');
                 var printWindow = window.open('gview.htm','PrintWindow','letf=0,top=0,width=150,height=300,toolbar=1,scrollbars=1,status=1');
                 printWindow.document.write(gridInsideDiv.innerHTML);
                 printWindow.document.close();
                 printWindow.focus();
                 printWindow.print();
                 printWindow.close(); 
                }";
                this.ClientScript.RegisterStartupScript(Page.GetType(), "PrintGridView", printScript.ToString(), true);
                btnImprimir.Attributes.Add("onclick", "PrintGridView();");
            }
            else
                VerError("El Cajero se encuentra inactivo y no puede realizar Operación");
        }
        else
            VerError("La Oficina se encuentra cerrada y no puede realizar Operaciones");
    }

    protected void btnExportarExcel_Click(object sender, EventArgs e)
    {
        if (long.Parse(Session["estadoOfi"].ToString()) == 1)
        {
            if (long.Parse(Session["estadoCaj"].ToString()) == 1)
            {
                if (gvMovimiento.Rows.Count > 0)
                {
                    StringBuilder sb = new StringBuilder();
                    StringWriter sw = new StringWriter(sb);
                    HtmlTextWriter htw = new HtmlTextWriter(sw);
                    Page pagina = new Page();
                    dynamic form = new HtmlForm();
                    gvMovimiento.EnableViewState = false;
                    pagina.EnableEventValidation = false;
                    pagina.DesignerInitialize();
                    pagina.Controls.Add(form);
                    form.Controls.Add(gvMovimiento);
                    pagina.RenderControl(htw);
                    Response.Clear();
                    Response.Buffer = true;
                    Response.ContentType = "application/vnd.ms-excel";
                    Response.AddHeader("Content-Disposition", "attachment;filename=data.xls");
                    Response.Charset = "UTF-8";
                    Response.ContentEncoding = Encoding.Default;
                    Response.Write(sb.ToString());
                    Response.End();
                }
                else
                    VerError("Se debe generar el reporte primero");

            }
            else
                VerError("El Cajero se encuentra inactivo y no puede realizar Operación");
        }
        else
            VerError("La Oficina se encuentra cerrada y no puede realizar Operaciones");

    }

    protected void LlenarComboMonedas(DropDownList ddlMonedas)
    {
        Xpinn.Caja.Services.TipoMonedaService monedaService = new Xpinn.Caja.Services.TipoMonedaService();
        Xpinn.Caja.Entities.TipoMoneda moneda = new Xpinn.Caja.Entities.TipoMoneda();
        ddlMonedas.DataSource = monedaService.ListarTipoMoneda(moneda, (Usuario)Session["usuario"]);
        ddlMonedas.DataTextField = "descripcion";
        ddlMonedas.DataValueField = "cod_moneda";
        ddlMonedas.DataBind();
    }

    protected void LlenarComboOficinas(DropDownList ddlOficinas)
    {
        Xpinn.Caja.Services.OficinaService oficinaService = new Xpinn.Caja.Services.OficinaService();
        Xpinn.Caja.Entities.Oficina oficina = new Xpinn.Caja.Entities.Oficina();
        ddlOficinas.DataSource = oficinaService.ListarOficina(oficina, (Usuario)Session["usuario"]);
        ddlOficinas.DataTextField = "nombre";
        ddlOficinas.DataValueField = "cod_oficina";
        ddlOficinas.DataBind();
        ddlOficinas.Items.Insert(0, new ListItem("Todos", "0"));
    }

   

    protected void LlenarComboCajas(DropDownList ddlCajas)
    {
        Xpinn.Caja.Services.CajaService cajaService = new Xpinn.Caja.Services.CajaService();
        Xpinn.Caja.Entities.Caja caja = new Xpinn.Caja.Entities.Caja();
        caja.cod_oficina = long.Parse(Session["Oficina"].ToString());
        ddlCajas.DataSource = cajaService.ListarComboCajaXOficina(caja, (Usuario)Session["usuario"]);
        ddlCajas.DataTextField = "nombre";
        ddlCajas.DataValueField = "cod_caja";
        ddlCajas.DataBind();
        ddlCajas.Items.Insert(0, new ListItem("Todos", "0"));
    }

    protected void LlenarComboCajasCajero(DropDownList ddlCajas)
    {
        Xpinn.Caja.Services.CajaService cajaService = new Xpinn.Caja.Services.CajaService();
        Xpinn.Caja.Entities.Caja caja = new Xpinn.Caja.Entities.Caja();
        caja.cod_oficina = long.Parse(Session["Oficina"].ToString());
        caja.cod_caja = (Session["Caja"].ToString());
        ddlCajas.DataSource = cajaService.ListarComboCajaXOficinayCaja(caja, (Usuario)Session["usuario"]);
        ddlCajas.DataTextField = "nombre";
        ddlCajas.DataValueField = "cod_caja";
        ddlCajas.DataBind();
       ddlCajas.Items.Insert(0, new ListItem("Todos", "0"));
    }

    protected void LlenarComboCajeros(DropDownList ddlCajeros)
    {
        Xpinn.Caja.Services.CajeroService cajeroService = new Xpinn.Caja.Services.CajeroService();
        Xpinn.Caja.Entities.Cajero cajero1 = new Xpinn.Caja.Entities.Cajero();

        if (long.Parse(Session["conteo"].ToString()) != 1)
        {
            long cod_caja = long.Parse(Session["Caja"].ToString());
            General general = ConsultarParametroGeneral(28);
            General generalperfil = ConsultarParametroGeneral(101);
            long cod_caja_permisos = 0;
            long cod_perfil_permisos = 0;
            // Si el parametro general es diferente a nulo, no esta vacio, y es un numero valido entro
            if (general != null && !string.IsNullOrWhiteSpace(general.valor) && long.TryParse(general.valor, out cod_caja_permisos))
            {
                // Si entro verifico que mi caja sea igual a la del parametro general, si es asi le doy permisos de principal y que consulte todo
                if (cod_caja_permisos == cod_caja)
                {
                    cajero1.cod_caja = long.Parse(ddlCajas.SelectedValue);
                }
                else
                {
                    cajero1.cod_caja = cod_caja;
                }
            }
            else
            {
                cajero1.cod_caja = cod_caja;
            }

            if (generalperfil != null && !string.IsNullOrWhiteSpace(generalperfil.valor) && long.TryParse(generalperfil.valor, out cod_perfil_permisos))
            {
                // Si entro verifico que mi caja sea igual a la del parametro general, si es asi le doy permisos de principal y que consulte todas las oficinas  
                cajero1.cod_caja = Convert.ToInt64(ddlCajas.SelectedValue);
                ddlCajeros.DataSource = cajeroService.ListarCajeroXCaja(cajero1, (Usuario)Session["usuario"]);
                ddlCajeros.DataTextField = "nom_cajero";
                ddlCajeros.DataValueField = "cod_persona";
                ddlCajeros.DataBind();
                ddlCajeros.Items.Insert(0, new ListItem("Todos","0"));
            }
        }
        else
            cajero1.cod_caja = long.Parse(ddlCajas.SelectedValue);

        Session["caja1"] = cajero1.cod_caja;
        ddlCajeros.DataSource = cajeroService.ListarCajeroXCaja(cajero1, (Usuario)Session["usuario"]);
        ddlCajeros.DataTextField = "nom_cajero";
        ddlCajeros.DataValueField = "cod_persona";
        ddlCajeros.DataBind();
        ddlCajeros.Items.Insert(0, new ListItem("Todos", "0"));
    }

    protected void ObtenerDatos()
    {
        if (long.Parse(Session["conteo"].ToString()) != 1)
        {
            ddlOficinas.SelectedValue = Session["Oficina"].ToString();
            ddlCajas.SelectedValue = Session["Caja"].ToString();
            ddlCajeros.SelectedValue = Session["Cajero"].ToString();
        }
        else
        {

            ddlOficinas.SelectedValue = Session["Oficina"].ToString();
            ddlCajas.SelectedValue = Session["caja1"].ToString();
            ddlCajeros.SelectedValue = Session["Cajero"].ToString();
        }

    }

    protected void btnGenerar_Click(object sender, EventArgs e)
    {
        List<Xpinn.Caja.Entities.TransaccionCaja> lstConsulta = new List<Xpinn.Caja.Entities.TransaccionCaja>();
        transac.cod_caja = long.Parse(ddlCajas.SelectedValue);

        //transac.cod_cajero = long.Parse(Session["Cajero"].ToString());

        VerError("");
        if (long.Parse(Session["conteo"].ToString()) != 1)
            transac.cod_cajero = long.Parse(Session["Cajero"].ToString());
        else
        {
            try
            {
                transac.cod_cajero = (ddlCajeros.SelectedValue == "") ? long.Parse("0"): long.Parse(ddlCajeros.SelectedValue)  ;
            }
            catch
            {
                VerError("Debe seleccionar el cajero");
                return;
            }
        }

        transac.cod_moneda = long.Parse(ddlMonedas.SelectedValue);
        transac.fecha_consulta_inicial = Convert.ToDateTime(txtFechaIni.Text);
        transac.fecha_consulta_final = Convert.ToDateTime(txtFechaFin.Text);
        General generalperfil = ConsultarParametroGeneral(101);
        long cod_perfil_permisos = 0;
        Usuario pUsuario = (Usuario)Session["usuario"];
        try
        {
            if (long.Parse(Session["estadoOfi"].ToString()) == 1)
            {
                if (long.Parse(Session["estadoCaj"].ToString()) == 1)
                {
                    if (generalperfil != null && !string.IsNullOrWhiteSpace(generalperfil.valor) && long.TryParse(generalperfil.valor, out cod_perfil_permisos))
                    {
                        if (generalperfil.valor == pUsuario.codperfil.ToString())
                        {
                            transac.cod_caja = Convert.ToInt64(ddlCajas.SelectedValue);
                            transac.cod_cajero = Convert.ToInt64(ddlCajeros.SelectedValue);
                        }              

                    }

             lstConsulta = tranCajaServicio.ListarMovimientosCaja(transac, (Usuario)Session["usuario"]);

                    gvMovimiento.DataSource = lstConsulta;

                    if (lstConsulta.Count > 0)
                    {
                        gvMovimiento.Visible = true;
                        gvMovimiento.DataBind();
                        btnReporte.Visible = true;
                        lblTotal.Visible = true;
                        txtTotalMovs.Visible = true;
                    }
                    else
                    {
                        gvMovimiento.Visible = false;
                        lblTotal.Visible = false;
                        txtTotalMovs.Visible = false;
                    }

                    Session.Add(tranCajaServicio.GetType().Name + ".consulta", 1);

                }
                else
                    VerError("El Cajero se encuentra inactivo y no puede realizar Operación");
            }
            else
                VerError("La Oficina se encuentra cerrada y no puede realizar Operaciones");

            CalcularTotal();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(tranCajaServicio.GetType().Name + "L", "Actualizar", ex);
        }
    }

    protected void ddlCajas_SelectedIndexChanged(object sender, EventArgs e)
    {
        LlenarComboCajeros(ddlCajeros);
    }

    /// <summary>
    /// Calcular el valor neto de los movimientos
    /// </summary>
    protected void CalcularTotal()
    {
        decimal acum = 0;
        decimal valor =  0;
        string tipo_mov = "";

        foreach (GridViewRow fila in gvMovimiento.Rows)
        {
            valor = decimal.Parse(fila.Cells[12].Text.ToString().Replace("$", "").Replace(".", ""));        
                                      
            tipo_mov = Convert.ToString(fila.Cells[8].Text);
            acum = acum + valor;

        }

        txtTotalMovs.Text = acum.ToString("N0");
    }

    protected void gvMovimiento_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvMovimiento.PageIndex = e.NewPageIndex;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(movCajaServicio.CodigoPrograma, "gvMovimiento_PageIndexChanging", ex);
        }
    }

    protected void btnReporte_Click(object sender, EventArgs e)
    {
        List<Xpinn.Caja.Entities.TransaccionCaja> lstConsulta = new List<Xpinn.Caja.Entities.TransaccionCaja>();
        transac.cod_caja = long.Parse(ddlCajas.SelectedValue);

        //transac.cod_cajero = long.Parse(Session["Cajero"].ToString());

        if (long.Parse(Session["conteo"].ToString()) != 1)
            transac.cod_cajero = long.Parse(Session["Cajero"].ToString());
        else
            transac.cod_cajero = long.Parse(ddlCajeros.SelectedValue);

        transac.cod_moneda = long.Parse(ddlMonedas.SelectedValue);
        transac.fecha_consulta_inicial = Convert.ToDateTime(txtFechaIni.Text);
        transac.fecha_consulta_final = Convert.ToDateTime(txtFechaFin.Text);
        Xpinn.Caja.Entities.TransaccionCaja refe = new Xpinn.Caja.Entities.TransaccionCaja();
        System.Data.DataTable table = new System.Data.DataTable();

        lstConsulta = tranCajaServicio.ListarMovimientosCaja(transac, (Usuario)Session["usuario"]);
        table.Columns.Add("oficina");
        table.Columns.Add("caja");
        table.Columns.Add("moneda");
        table.Columns.Add("codigo");
        table.Columns.Add("comprobante");
        table.Columns.Add("total");

        DataRow datarw;
        if (lstConsulta.Count == 0)
        {
            datarw = table.NewRow();
            datarw[0] = " ";
            datarw[1] = " ";
            datarw[2] = " ";
            datarw[3] = " ";
            datarw[4] = " ";
            datarw[5] = " ";

            table.Rows.Add(datarw);
        }
        else
        {
            for (int i = 0; i < lstConsulta.Count; i++)
            {
                datarw = table.NewRow();
                refe = lstConsulta[i];
                datarw[0] = " " + refe.cod_ope;
                datarw[1] = " " + refe.tipo_movimiento;
                datarw[2] = " " + refe.nom_cliente;
                datarw[3] = " " + refe.num_producto;
                datarw[4] = " " + refe.fecha_movimiento;
                datarw[5] = " " + string.Format("{0:N0}", refe.valor_pago);
                table.Rows.Add(datarw);
            }
        }

        string fecha = Convert.ToString(DateTime.Now);
        ReportParameter[] param = new ReportParameter[6];
        param[0] = new ReportParameter("fecha", fecha);
        param[1] = new ReportParameter("cajero", Convert.ToString(ddlCajeros.SelectedItem));
        param[2] = new ReportParameter("caja", Convert.ToString(ddlCajas.SelectedItem));
        param[3] = new ReportParameter("oficina", Convert.ToString(ddlOficinas.SelectedItem));
        param[4] = new ReportParameter("total", txtTotalMovs.Text);
        param[5] = new ReportParameter("ImagenReport", ImagenReporte());

        ReportViewer1.LocalReport.EnableExternalImages = true;
        ReportViewer1.LocalReport.SetParameters(param);
        ReportViewer1.LocalReport.DataSources.Clear();
        ReportDataSource rds1 = new ReportDataSource("DataSet1", table);
        ReportViewer1.LocalReport.DataSources.Add(rds1);
        ReportViewer1.LocalReport.Refresh();
        mpeNuevo.Show();
    }


    protected void ddlOficinas_SelectedIndexChanged(object sender, EventArgs e)
    {
        long cod_caja = long.Parse(Session["Caja"].ToString());
        General general = ConsultarParametroGeneral(101);
        long cod_perfil_permisos = 0;
        Usuario pUsuario = (Usuario)Session["usuario"];
        // Si el parametro general es diferente a nulo, no esta vacio, y es un numero valido entro
        if (general != null && !string.IsNullOrWhiteSpace(general.valor) && long.TryParse(general.valor, out cod_perfil_permisos))
        {
            if (general.valor == pUsuario.codperfil.ToString())
            {
                // se inicializan los combos 
                //LlenarComboOficinas(ddlOficinas);
                LlenarComboCajas(ddlCajas);
                LlenarComboMonedas(ddlMonedas);
            }
            else
            {
                //  LlenarComboOficinas(ddlOficinas);
                LlenarComboMonedas(ddlMonedas);
                LlenarComboCajasCajero(ddlCajas);

            }
        }

        }
}