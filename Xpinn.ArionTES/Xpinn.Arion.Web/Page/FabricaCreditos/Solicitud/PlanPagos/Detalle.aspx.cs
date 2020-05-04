using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using Xpinn.FabricaCreditos.Services;
using Xpinn.FabricaCreditos.Entities;
using Microsoft.Reporting.WebForms;
using Microsoft.CSharp;
using System.Text;
using System.IO;
using System.Web.UI.HtmlControls;
using System.Globalization;

public partial class Detalle : GlobalWeb
{
    CreditoPlanService creditoPlanServicio = new CreditoPlanService();
    DatosPlanPagosService datosServicio = new DatosPlanPagosService();
    ExcelServiceFC excelServicio = new ExcelServiceFC();
    CreditoRecogerService creditoRecogerServicio = new CreditoRecogerService();
    Persona1Service Persona1Servicio = new Persona1Service();
    VentasSemanalesService VentasSemanalesServicio = new VentasSemanalesService();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[creditoPlanServicio.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(creditoPlanServicio.CodigoPrograma, "E");
            else
                VisualizarOpciones(creditoPlanServicio.CodigoPrograma, "A");
            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(creditoPlanServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
              
                UpdatePanel1.Visible = false;
                if (Session[creditoPlanServicio.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[creditoPlanServicio.CodigoPrograma + ".id"].ToString();
                    Session.Remove(creditoPlanServicio.CodigoPrograma + ".id");
                    ObtenerDatos(idObjeto);
                    
                }
                mvLista.ActiveViewIndex = 0;
                mvLista0.ActiveViewIndex = 0;
                if (Session[creditoPlanServicio.CodigoPrograma + ".origen"] != null)
                {
                    Button2.Visible = false;
                    BtnPagare0.Visible = false;
                    Btnautorizacion0.Visible = false;
                }
            }

            String radicado = "";
            radicado = Request["radicado"];
            if (radicado != null)
            {
                this.txtNumRadic.Text = radicado;
                this.ObtenerDatos2(radicado);

            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(creditoPlanServicio.CodigoPrograma, "Page_Load", ex);
        }
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        if (Session[creditoPlanServicio.CodigoPrograma + ".origen"] != null)
            Response.Redirect(Session[creditoPlanServicio.CodigoPrograma + ".origen"].ToString());
        else
            Navegar(Pagina.Lista);
    }


    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            CreditoPlan credito = new CreditoPlan();

            if (pIdObjeto != null)
            {
                
                credito.Numero_radicacion = Int32.Parse(pIdObjeto);
                credito = creditoPlanServicio.ConsultarCredito(credito.Numero_radicacion, true, (Usuario)Session["usuario"]);

                if (!string.IsNullOrEmpty(credito.Numero_radicacion.ToString()))
                    txtNumRadic.Text = HttpUtility.HtmlDecode(credito.Numero_radicacion.ToString());
                if (!string.IsNullOrEmpty(credito.Linea))
                    txtLinea.Text = HttpUtility.HtmlDecode(credito.LineaCredito);
                if (!string.IsNullOrEmpty(credito.LineaCredito))
                    txtNombreLinea.Text = HttpUtility.HtmlDecode(credito.Linea);
                if (!string.IsNullOrEmpty(credito.Identificacion))
                    txtIdentific.Text = HttpUtility.HtmlDecode(credito.Identificacion);
                if (!string.IsNullOrEmpty(credito.Nombres))
                    txtNombre.Text = HttpUtility.HtmlDecode(credito.Nombres);
                if (!string.IsNullOrEmpty(credito.Plazo.ToString()))
                    txtPlazo.Text = HttpUtility.HtmlDecode(credito.Plazo.ToString());
                if (!string.IsNullOrEmpty(credito.Monto.ToString()))
                {
                    txtMonto.Text = HttpUtility.HtmlDecode(credito.Monto.ToString());
                    txtMonto.Text = String.Format("{0:C}", Convert.ToInt64(txtMonto.Text));
                    txtMontoCalculado.Text = HttpUtility.HtmlDecode(credito.Monto.ToString());
                    txtMontoCalculado.Text = String.Format("{0:C}", Convert.ToInt64(txtMontoCalculado.Text));
                    txtVrDesembolsado.Text = HttpUtility.HtmlDecode(credito.Monto.ToString());
                    txtVrDesembolsado.Text = String.Format("{0:C}", Convert.ToInt64(txtVrDesembolsado.Text));
                };
                if (!string.IsNullOrEmpty(credito.Periodicidad))
                    txtPeriodicidad.Text = HttpUtility.HtmlDecode(credito.Periodicidad);
                if (!string.IsNullOrEmpty(credito.FormaPago))
                    txtFormaPago.Text = HttpUtility.HtmlDecode(credito.FormaPago);
                if (!string.IsNullOrEmpty(credito.FechaInicio.ToString()))
                {
                    txtFechaInicial.Text = HttpUtility.HtmlDecode(credito.FechaInicio.ToString());
                    txtFechaInicial.Text = String.Format("{0:d}", Convert.ToDateTime(txtFechaInicial.Text));
                };
                if (!string.IsNullOrEmpty(credito.Cuota.ToString()))
                {
                    txtCuota.Text = HttpUtility.HtmlDecode(credito.Cuota.ToString());
                    txtCuota.Text = String.Format("{0:C}", Convert.ToDouble(txtCuota.Text));
                };
                if (!string.IsNullOrEmpty(credito.DiasAjuste.ToString()))
                {
                    txtDiasAjuste.Text = HttpUtility.HtmlDecode(credito.DiasAjuste.ToString());
                };
                if (!string.IsNullOrEmpty(credito.TasaNom.ToString()))
                {
                    txtTasaInteres.Text = HttpUtility.HtmlDecode(credito.TasaNom.ToString());
                    txtTasaInteres.Text = String.Format("{0:P2}", Convert.ToDouble(txtTasaInteres.Text) / 100);
                };
                if (!string.IsNullOrEmpty(credito.LeyMiPyme.ToString()))
                {
                    txtLeyMiPyme.Text = HttpUtility.HtmlDecode(credito.LeyMiPyme.ToString());
                    txtLeyMiPyme.Text = String.Format("{0:P2}", Convert.ToDouble(txtLeyMiPyme.Text) / 100);
                };
                if (!string.IsNullOrEmpty(credito.Moneda))
                    txtMoneda.Text = HttpUtility.HtmlDecode(credito.Moneda);
                if (Convert.ToString(credito.ciudad) == null)
                {
                    Txtciudad.Text = "  ";
                }
                else
                {
                    Txtciudad.Text = Convert.ToString(credito.ciudad);
                }

                if (Convert.ToString(credito.Direccion) == null)
                {
                    Txtdireccion.Text = "  ";
                }
                else
                {
                    Txtdireccion.Text = Convert.ToString(credito.Direccion);
                }

                if (Convert.ToString(credito.FechaPrimerPago) == null)
                {
                    Txtprimerpago.Text = "  ";
                }
                else
                {
                    Txtprimerpago.Text = Convert.ToDateTime(credito.FechaPrimerPago).ToString(gFormatoFecha);
                }

                if (Convert.ToString(credito.FechaSolicitud) == null)
                {
                    Txtgeneracion.Text = "  ";
                }
                else
                {
                    Txtgeneracion.Text = (credito.FechaSolicitud).ToString("dd/MM/yyyy");

                }

                if (Convert.ToString(credito.TasaEfe) == null)
                {
                    Txtinteresefectiva.Text = "  ";
                }
                else
                {
                    Txtinteresefectiva.Text = Convert.ToString(credito.TasaEfe);
                    Txtinteresefectiva.Text = String.Format("{0:P2}", Convert.ToDouble(Txtinteresefectiva.Text) / 100);
                }

                if (Convert.ToString(credito.numero_cuotas) == null)
                {
                    Txtcuotas.Text = "  ";
                }
                else
                {
                    Txtcuotas.Text = Convert.ToString(credito.numero_cuotas);
                }

                if (Convert.ToString(credito.pagare) == null)
                {
                    Txtpagare.Text = "  ";
                }
                else
                {
                    Txtpagare.Text = Convert.ToString(credito.pagare);
                }
                Session["Cod_persona"] = credito.Cod_persona;
                TablaPlanPagos(pIdObjeto);
            }

            //VerAuditoria(programa.UsuarioCrea, programa.FechaCrea, programa.UsuarioEdita, programa.FechaEdita);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(creditoPlanServicio.CodigoPrograma, "ObtenerDatos", ex);
        }
    }

    protected void ObtenerDatos2(String radicado)
    {
        try
        {
            CreditoPlan credito = new CreditoPlan();

            if (radicado != null)
            {

                credito.Numero_radicacion = Int32.Parse(radicado);
                credito = creditoPlanServicio.ConsultarCredito(credito.Numero_radicacion, true, (Usuario)Session["usuario"]);

                if (!string.IsNullOrEmpty(credito.Numero_radicacion.ToString()))
                    txtNumRadic.Text = HttpUtility.HtmlDecode(credito.Numero_radicacion.ToString());
                if (!string.IsNullOrEmpty(credito.Linea))
                    txtLinea.Text = HttpUtility.HtmlDecode(credito.LineaCredito);
                if (!string.IsNullOrEmpty(credito.LineaCredito))
                    txtNombreLinea.Text = HttpUtility.HtmlDecode(credito.Linea);
                if (!string.IsNullOrEmpty(credito.Identificacion))
                    txtIdentific.Text = HttpUtility.HtmlDecode(credito.Identificacion);
                if (!string.IsNullOrEmpty(credito.Nombres))
                    txtNombre.Text = HttpUtility.HtmlDecode(credito.Nombres);
                if (!string.IsNullOrEmpty(credito.Plazo.ToString()))
                    txtPlazo.Text = HttpUtility.HtmlDecode(credito.Plazo.ToString());
                if (!string.IsNullOrEmpty(credito.Monto.ToString()))
                {
                    txtMonto.Text = HttpUtility.HtmlDecode(credito.Monto.ToString());
                    txtMonto.Text = String.Format("{0:C}", Convert.ToInt64(txtMonto.Text));
                    txtMontoCalculado.Text = HttpUtility.HtmlDecode(credito.Monto.ToString());
                    txtMontoCalculado.Text = String.Format("{0:C}", Convert.ToInt64(txtMontoCalculado.Text));
                    txtVrDesembolsado.Text = HttpUtility.HtmlDecode(credito.Monto.ToString());
                    txtVrDesembolsado.Text = String.Format("{0:C}", Convert.ToInt64(txtVrDesembolsado.Text));
                };
                if (!string.IsNullOrEmpty(credito.Periodicidad))
                    txtPeriodicidad.Text = HttpUtility.HtmlDecode(credito.Periodicidad);
                if (!string.IsNullOrEmpty(credito.FormaPago))
                    txtFormaPago.Text = HttpUtility.HtmlDecode(credito.FormaPago);
                if (!string.IsNullOrEmpty(credito.FechaInicio.ToString()))
                {
                    txtFechaInicial.Text = HttpUtility.HtmlDecode(credito.FechaInicio.ToString());
                    txtFechaInicial.Text = String.Format("{0:d}", Convert.ToDateTime(txtFechaInicial.Text));
                };
                if (!string.IsNullOrEmpty(credito.Cuota.ToString()))
                {
                    txtCuota.Text = HttpUtility.HtmlDecode(credito.Cuota.ToString());
                    txtCuota.Text = String.Format("{0:C}", Convert.ToDouble(txtCuota.Text));
                };
                if (!string.IsNullOrEmpty(credito.DiasAjuste.ToString()))
                {
                    txtDiasAjuste.Text = HttpUtility.HtmlDecode(credito.DiasAjuste.ToString());
                };
                if (!string.IsNullOrEmpty(credito.TasaNom.ToString()))
                {
                    txtTasaInteres.Text = HttpUtility.HtmlDecode(credito.TasaNom.ToString());
                    txtTasaInteres.Text = String.Format("{0:P2}", Convert.ToDouble(txtTasaInteres.Text) / 100);
                };
                if (!string.IsNullOrEmpty(credito.LeyMiPyme.ToString()))
                {
                    txtLeyMiPyme.Text = HttpUtility.HtmlDecode(credito.LeyMiPyme.ToString());
                    txtLeyMiPyme.Text = String.Format("{0:P2}", Convert.ToDouble(txtLeyMiPyme.Text) / 100);
                };
                if (!string.IsNullOrEmpty(credito.Moneda))
                    txtMoneda.Text = HttpUtility.HtmlDecode(credito.Moneda);
                if (Convert.ToString(credito.ciudad) == null)
                {
                    Txtciudad.Text = "  ";
                }
                else
                {
                    Txtciudad.Text = Convert.ToString(credito.ciudad);
                }

                if (Convert.ToString(credito.Direccion) == null)
                {
                    Txtdireccion.Text = "  ";
                }
                else
                {
                    Txtdireccion.Text = Convert.ToString(credito.Direccion);
                }

                if (Convert.ToString(credito.FechaPrimerPago) == null)
                {
                    Txtprimerpago.Text = "  ";
                }
                else
                {
                    Txtprimerpago.Text = Convert.ToDateTime(credito.FechaPrimerPago).ToString(gFormatoFecha);
                }

                if (Convert.ToString(credito.FechaSolicitud) == null)
                {
                    Txtgeneracion.Text = "  ";
                }
                else
                {
                    Txtgeneracion.Text = (credito.FechaSolicitud).ToString("dd/MM/yyyy");

                }

                if (Convert.ToString(credito.TasaEfe) == null)
                {
                    Txtinteresefectiva.Text = "  ";
                }
                else
                {
                    Txtinteresefectiva.Text = Convert.ToString(credito.TasaEfe);
                    Txtinteresefectiva.Text = String.Format("{0:P2}", Convert.ToDouble(Txtinteresefectiva.Text) / 100);
                }

                if (Convert.ToString(credito.numero_cuotas) == null)
                {
                    Txtcuotas.Text = "  ";
                }
                else
                {
                    Txtcuotas.Text = Convert.ToString(credito.numero_cuotas);
                }

                if (Convert.ToString(credito.pagare) == null)
                {
                    Txtpagare.Text = "  ";
                }
                else
                {
                    Txtpagare.Text = Convert.ToString(credito.pagare);
                }
                Session["Cod_persona"] = credito.Cod_persona;
                TablaPlanPagos(radicado);
            }

            //VerAuditoria(programa.UsuarioCrea, programa.FechaCrea, programa.UsuarioEdita, programa.FechaEdita);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(creditoPlanServicio.CodigoPrograma, "ObtenerDatos", ex);
        }
    }

   

    private void TablaPlanPagos(String pIdObjeto)
    {
        Int32 anchocolumna = 90;
        Int32 longitud = 0;

        Credito datosApp = new Credito();
        datosApp.numero_radicacion = Int64.Parse(pIdObjeto);
        List<DatosPlanPagos> lstConsulta = new List<DatosPlanPagos>();
        lstConsulta = datosServicio.ListarDatosPlanPagos(datosApp, (Usuario)Session["usuario"]);
        Session["PlanPagos"] = lstConsulta;

        gvPlanPagos.DataSource = lstConsulta;
        gvPlanPagos0.DataSource = lstConsulta;

        // Ajustar informaciòn de la grila para mostrar en pantalla
        if (lstConsulta.Count > 0)
        {
            // Mostrando la grilla y validar permisos
            gvPlanPagos.Visible = true;
            gvPlanPagos.DataBind();
            ValidarPermisosGrilla(gvPlanPagos);
            gvPlanPagos.Columns[1].ItemStyle.Width = 90;
            // Ocultando las columnas que no deben mostrarse
            List<Atributos> lstAtr = new List<Atributos>();
            lstAtr = datosServicio.GenerarAtributosPlan((Usuario)Session["usuario"]);
            Session["AtributosPlanPagos"] = lstAtr;
            for (int i = 4; i <= 18; i++)
            {
                gvPlanPagos.Columns[i].Visible = false;
                int j = 0;
                foreach (Atributos item in lstAtr)
                {
                    if (j == i - 4)
                        gvPlanPagos.Columns[i].HeaderText = item.nom_atr;
                    j = j + 1;
                }
            }
            // Establecer el ancho de las columnas de valores
            for (int i = 2; i < 20; i++)
            {
                gvPlanPagos.Columns[i].ItemStyle.Width = anchocolumna;
            }
            // Ajustando el tamaño de la grilla
            longitud = 0;
            for (int i = 0; i < 20; i++)
            {
                longitud = longitud + Convert.ToInt32(gvPlanPagos.Columns[i].ItemStyle.Width.Value);
            }
            gvPlanPagos.Width = longitud / 2;
            // Mostrando las columnas que tienen valores
            foreach (DatosPlanPagos ItemPlanPagos in lstConsulta)
            {
                if (ItemPlanPagos.int_1 != 0) { gvPlanPagos.Columns[4].Visible = true; }
                if (ItemPlanPagos.int_2 != 0) { gvPlanPagos.Columns[5].Visible = true; }
                if (ItemPlanPagos.int_3 != 0) { gvPlanPagos.Columns[6].Visible = true; }
                if (ItemPlanPagos.int_4 != 0) { gvPlanPagos.Columns[7].Visible = true; }
                if (ItemPlanPagos.int_5 != 0) { gvPlanPagos.Columns[8].Visible = true; }
                if (ItemPlanPagos.int_6 != 0) { gvPlanPagos.Columns[9].Visible = true; }
                if (ItemPlanPagos.int_7 != 0) { gvPlanPagos.Columns[10].Visible = true; }
                if (ItemPlanPagos.int_8 != 0) { gvPlanPagos.Columns[11].Visible = true; }
                if (ItemPlanPagos.int_9 != 0) { gvPlanPagos.Columns[12].Visible = true; }
                if (ItemPlanPagos.int_10 != 0) { gvPlanPagos.Columns[13].Visible = true; }
                if (ItemPlanPagos.int_11 != 0) { gvPlanPagos.Columns[14].Visible = true; }
                if (ItemPlanPagos.int_12 != 0) { gvPlanPagos.Columns[15].Visible = true; }
                if (ItemPlanPagos.int_13 != 0) { gvPlanPagos.Columns[16].Visible = true; }
                if (ItemPlanPagos.int_14 != 0) { gvPlanPagos.Columns[17].Visible = true; }
                if (ItemPlanPagos.int_15 != 0) { gvPlanPagos.Columns[18].Visible = true; }
            }
            gvPlanPagos.DataBind();
        }
        else
        {
            gvPlanPagos.Visible = true;
        }

        // Ajustar valores para la grilla que se usa para descargar los datos a excel.
        if (lstConsulta.Count > 0)
        {
            gvPlanPagos0.Visible = true;
            gvPlanPagos0.DataBind();
            ValidarPermisosGrilla(gvPlanPagos0);
            gvPlanPagos0.Columns[1].ItemStyle.Width = 90;
            // Ocultando las columnas que no deben mostrarse
            List<Atributos> lstAtr = new List<Atributos>();
            lstAtr = datosServicio.GenerarAtributosPlan((Usuario)Session["usuario"]);
            for (int i = 4; i <= 18; i++)
            {
                gvPlanPagos0.Columns[i].Visible = false;
                int j = 0;
                foreach (Atributos item in lstAtr)
                {
                    if (j == i - 4)
                        gvPlanPagos0.Columns[i].HeaderText = item.nom_atr;
                    j = j + 1;
                }
            }
            // Establecer el ancho de las columnas de valores
            for (int i = 2; i < 20; i++)
            {
                gvPlanPagos0.Columns[i].ItemStyle.Width = anchocolumna;
            }
            // Ajustando el tamaño de la grilla
            longitud = 0;
            for (int i = 0; i < 20; i++)
            {
                longitud = longitud + Convert.ToInt32(gvPlanPagos0.Columns[i].ItemStyle.Width.Value);
            }
            gvPlanPagos0.Width = longitud / 2;
            foreach (DatosPlanPagos ItemPlanPagos in lstConsulta)
            {
                if (ItemPlanPagos.int_1 != 0) { gvPlanPagos0.Columns[4].Visible = true; }
                if (ItemPlanPagos.int_2 != 0) { gvPlanPagos0.Columns[5].Visible = true; }
                if (ItemPlanPagos.int_3 != 0) { gvPlanPagos0.Columns[6].Visible = true; }
                if (ItemPlanPagos.int_4 != 0) { gvPlanPagos0.Columns[7].Visible = true; }
                if (ItemPlanPagos.int_5 != 0) { gvPlanPagos0.Columns[8].Visible = true; }
                if (ItemPlanPagos.int_6 != 0) { gvPlanPagos0.Columns[9].Visible = true; }
                if (ItemPlanPagos.int_7 != 0) { gvPlanPagos0.Columns[10].Visible = true; }
                if (ItemPlanPagos.int_8 != 0) { gvPlanPagos0.Columns[11].Visible = true; }
                if (ItemPlanPagos.int_9 != 0) { gvPlanPagos0.Columns[12].Visible = true; }
                if (ItemPlanPagos.int_10 != 0) { gvPlanPagos0.Columns[13].Visible = true; }
                if (ItemPlanPagos.int_11 != 0) { gvPlanPagos0.Columns[14].Visible = true; }
                if (ItemPlanPagos.int_12 != 0) { gvPlanPagos0.Columns[15].Visible = true; }
                if (ItemPlanPagos.int_13 != 0) { gvPlanPagos0.Columns[16].Visible = true; }
                if (ItemPlanPagos.int_14 != 0) { gvPlanPagos0.Columns[17].Visible = true; }
                if (ItemPlanPagos.int_15 != 0) { gvPlanPagos0.Columns[18].Visible = true; }
            }
            gvPlanPagos0.DataBind();
        }
        else
        {
            gvPlanPagos0.Visible = false;
        }
    }

    /// <summary>
    /// Generar tabla para pasar al reporte
    /// </summary>
    /// <returns></returns>
    public DataTable CrearDataTable(String pIdObjeto)
    {
        if (Session["PlanPagos"] == null)
            return null;

        System.Data.DataTable table = new System.Data.DataTable();

        DatosPlanPagos datosApp = new DatosPlanPagos();
        datosApp.numero_radicacion = Int64.Parse(pIdObjeto);
        List<DatosPlanPagos> lstPlanPagos = new List<DatosPlanPagos>();
        lstPlanPagos = (List<DatosPlanPagos>)Session["PlanPagos"];
        List<Atributos> lstAtr = new List<Atributos>();
        lstAtr = (List<Atributos>)Session["AtributosPlanPagos"];

        table.Columns.Add("numerocuota");
        table.Columns.Add("fechacuota");
        table.Columns.Add("sal_ini");
        table.Columns.Add("capital");
        table.Columns.Add("int_1");
        table.Columns.Add("int_2");
        table.Columns.Add("int_3");
        table.Columns.Add("int_4");
        table.Columns.Add("int_5");
        table.Columns.Add("int_6");
        table.Columns.Add("int_7");
        table.Columns.Add("int_8");
        table.Columns.Add("int_9");
        table.Columns.Add("int_10");
        table.Columns.Add("int_11");
        table.Columns.Add("int_12");
        table.Columns.Add("int_13");
        table.Columns.Add("int_14");
        table.Columns.Add("int_15");
        table.Columns.Add("total");
        table.Columns.Add("sal_fin");

        foreach (DatosPlanPagos item in lstPlanPagos)
        {
            DataRow datarw;
            datarw = table.NewRow();
            datarw[0] = item.numerocuota;
            datarw[1] = item.fechacuota.Value.ToShortDateString();
            datarw[2] = item.sal_ini.ToString("0,0");
            datarw[3] = item.capital.ToString("0,0");
            datarw[4] = item.int_1.ToString("0,0");
            datarw[5] = item.int_2.ToString("0,0");
            datarw[6] = item.int_3.ToString("0,0");
            datarw[7] = item.int_4.ToString("0,0");
            datarw[8] = item.int_5.ToString("0,0");
            datarw[9] = item.int_6.ToString("0,0");
            datarw[10] = item.int_7.ToString("0,0");
            datarw[11] = item.int_8.ToString("0,0");
            datarw[12] = item.int_9.ToString("0,0");
            datarw[13] = item.int_10.ToString("0,0");
            datarw[14] = item.int_11.ToString("0,0");
            datarw[15] = item.int_12.ToString("0,0");
            datarw[16] = item.int_13.ToString("0,0");
            datarw[17] = item.int_14.ToString("0,0");
            datarw[18] = item.int_15.ToString("0,0");
            datarw[19] = item.total.ToString("0,0");
            datarw[20] = item.sal_fin.ToString("0,0");
            table.Rows.Add(datarw);
        }

        return table;
    }

    protected void btnExportar0_Click(object sender, EventArgs e)
    {
        try
        {
            if (gvPlanPagos0.Rows.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                StringWriter sw = new StringWriter(sb);
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                Page pagina = new Page();
                dynamic form = new HtmlForm();
                gvPlanPagos0.EnableViewState = false;
                pagina.EnableEventValidation = false;
                pagina.DesignerInitialize();
                pagina.Controls.Add(form);
                form.Controls.Add(gvPlanPagos0);
                pagina.RenderControl(htw);
                Response.Clear();
                Response.Buffer = true;
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("Content-Disposition", "attachment;filename=data.xls");
                Response.Charset = "UTF-8";
                //Response.ContentEncoding = Encoding.Default;
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


    /// <summary>
    /// Esta función corresponde al botón de imprimir y permite generar el reporte del plan de pagos
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnInforme0_Click(object sender, EventArgs e)
    {
        if (Session["AtributosPlanPagos"] == null)
            return;
        if (Session["PlanPagos"] == null)
            return;

        // ---------------------------------------------------------------------------------------------------------
        // Traer listado de crèditos recogidos
        // ---------------------------------------------------------------------------------------------------------
        List<CreditoRecoger> lstConsulta = new List<CreditoRecoger>();
        Xpinn.FabricaCreditos.Entities.CreditoRecoger refe = new Xpinn.FabricaCreditos.Entities.CreditoRecoger();
        lstConsulta = creditoRecogerServicio.ConsultarCreditoRecoger(Convert.ToInt64(txtNumRadic.Text), (Usuario)Session["usuario"]);

        // LLenar data table con los datos a recoger
        System.Data.DataTable table = new System.Data.DataTable();
        table.Columns.Add("numero");
        table.Columns.Add("linea");
        table.Columns.Add("monto");
        table.Columns.Add("tasa");
        table.Columns.Add("saldo");
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
                datarw[0] = " " + refe.numero_radicacion;
                datarw[1] = " " + refe.linea_credito;
                datarw[2] = " " + refe.monto.ToString("0,0");
                datarw[3] = " " + refe.interes_corriente.ToString("0,0");
                datarw[4] = " " + refe.saldo_capital.ToString("0,0");
                datarw[5] = " " + refe.valor_recoge.ToString("0,0");
                table.Rows.Add(datarw);
            }
        }

        // ---------------------------------------------------------------------------------------------------------
        // Traer los datos de los codeudores
        // ---------------------------------------------------------------------------------------------------------
        Xpinn.FabricaCreditos.Entities.Persona1 refere = new Xpinn.FabricaCreditos.Entities.Persona1();
        List<Xpinn.FabricaCreditos.Entities.Persona1> lstConsultas = new List<Xpinn.FabricaCreditos.Entities.Persona1>();
        lstConsultas = Persona1Servicio.ListarPersona1(ObtenerValoresCodeudores(), (Usuario)Session["usuario"]);
        // LLenar la tabla con datos de codeudores
        System.Data.DataTable table2 = new System.Data.DataTable();
        table2.Columns.Add("codigo");
        table2.Columns.Add("identificacion");
        table2.Columns.Add("tipo");
        table2.Columns.Add("nombres");
        table2.Columns.Add("empresa");
        table2.Columns.Add("direccion");
        DataRow datarw2;
        if (lstConsultas.Count == 0)
        {
            datarw2 = table2.NewRow();
            datarw2[0] = " ";
            datarw2[1] = " ";
            datarw2[2] = " ";
            datarw2[3] = " ";
            datarw2[4] = " ";
            datarw2[5] = " ";
            table2.Rows.Add(datarw2);
        }
        else
        {
            for (int i = 0; i < lstConsultas.Count; i++)
            {
                datarw2 = table2.NewRow();
                refere = lstConsultas[i];
                datarw2[0] = " " + refere.cod_persona;
                datarw2[1] = " " + refere.identificacion;
                datarw2[2] = " " + refere.tipo_identificacion;
                datarw2[3] = " " + refere.primer_nombre + " " + refere.segundo_nombre + " " + refere.primer_apellido + " " + refere.segundo_apellido;
                datarw2[4] = " " + refere.empresa;
                datarw2[5] = " " + refere.direccionempresa;
                table2.Rows.Add(datarw2);

            }
        }


        // ---------------------------------------------------------------------------------------------------------
        // Traer los datos de las cuotas extras
        // ---------------------------------------------------------------------------------------------------------

        List<CreditoRecoger> lstConsulta1 = new List<CreditoRecoger>();
        CreditoRecoger referen = new CreditoRecoger();
        lstConsulta1 = creditoRecogerServicio.Consultarterminosfijos(txtNumRadic.Text, (Usuario)Session["usuario"]);

        System.Data.DataTable table3 = new System.Data.DataTable();
        table3.Columns.Add("fecha");
        table3.Columns.Add("valor");
        table3.Columns.Add("formapago");

        DataRow datarw3;
        if (lstConsulta1.Count == 0)
        {
            datarw3 = table3.NewRow();
            datarw3[0] = " ";
            datarw3[1] = " ";
            datarw3[2] = " ";
            table3.Rows.Add(datarw3);
        }
        else
        {
            for (int i = 0; i < lstConsulta1.Count; i++)
            {
                datarw3 = table3.NewRow();
                referen = lstConsulta1[i];
                datarw3[0] = " " + referen.fecha_pago;
                datarw3[1] = " " + referen.valor_total;
                datarw3[2] = " " + referen.formapago;
                table3.Rows.Add(datarw3);
            }
        }

        // ---------------------------------------------------------------------------------------------------------
        // Pasar datos al reporte
        // ---------------------------------------------------------------------------------------------------------

        string cRutaDeImagen;
        cRutaDeImagen = Server.MapPath("~/Images\\") + "LogoEmpresa.jpg";
        ReportParameter[] param = new ReportParameter[51];

        param[0] = new ReportParameter("Entidad", "FUNDACION EMPRENDER");
        param[1] = new ReportParameter("numero_radicacion", txtNumRadic.Text);
        param[2] = new ReportParameter("cod_linea_credito", txtLinea.Text);
        param[3] = new ReportParameter("linea", txtNombreLinea.Text);
        param[4] = new ReportParameter("nombre", txtNombre.Text);
        param[5] = new ReportParameter("identificacion", txtIdentific.Text);
        param[6] = new ReportParameter("direccion", Txtdireccion.Text);
        param[7] = new ReportParameter("ciudad", Txtciudad.Text);
        param[8] = new ReportParameter("fecha_inico", txtFechaInicial.Text);
        param[9] = new ReportParameter("fecha_primer", Txtprimerpago.Text);
        param[10] = new ReportParameter("palzo", txtPlazo.Text);
        param[11] = new ReportParameter("fecha_generacion", Txtgeneracion.Text);
        param[12] = new ReportParameter("periocidad", txtPeriodicidad.Text);
        param[13] = new ReportParameter("cuotas", Txtcuotas.Text);
        param[14] = new ReportParameter("valor_cuota", txtCuota.Text);
        param[15] = new ReportParameter("forma_pago", txtFormaPago.Text);
        param[16] = new ReportParameter("tasa_nominal", txtTasaInteres.Text);
        param[17] = new ReportParameter("tasa_efectiva", Txtinteresefectiva.Text);
        param[18] = new ReportParameter("desembolso", txtVrDesembolsado.Text);
        param[19] = new ReportParameter("pagare", Txtpagare.Text);

        List<Atributos> lstAtr = new List<Atributos>();
        lstAtr = (List<Atributos>)Session["AtributosPlanPagos"];
        int j = 0;
        foreach (Atributos item in lstAtr)
        {
            param[20 + j] = new ReportParameter("titulo" + j, item.nom_atr);
            j = j + 1;
        }
        for (int i = j; i < 15; i++)
        {
            param[20 + i] = new ReportParameter("titulo" + i, " ");
        }
        List<DatosPlanPagos> lstPlan = new List<DatosPlanPagos>();
        lstPlan = (List<DatosPlanPagos>)Session["PlanPagos"];
        Boolean[] bVisible = new Boolean[16];
        for (int i = 1; i <= 15; i++)
        {
            bVisible[i] = false;
            i = i + 1;
        }
        foreach (DatosPlanPagos ItemPlanPagos in lstPlan)
        {
            if (ItemPlanPagos.int_1 != 0) { bVisible[1] = true; }
            if (ItemPlanPagos.int_2 != 0) { bVisible[2] = true; }
            if (ItemPlanPagos.int_3 != 0) { bVisible[3] = true; }
            if (ItemPlanPagos.int_4 != 0) { bVisible[4] = true; }
            if (ItemPlanPagos.int_5 != 0) { bVisible[5] = true; }
            if (ItemPlanPagos.int_6 != 0) { bVisible[6] = true; }
            if (ItemPlanPagos.int_7 != 0) { bVisible[7] = true; }
            if (ItemPlanPagos.int_8 != 0) { bVisible[8] = true; }
            if (ItemPlanPagos.int_9 != 0) { bVisible[9] = true; }
            if (ItemPlanPagos.int_10 != 0) { bVisible[10] = true; }
            if (ItemPlanPagos.int_11 != 0) { bVisible[11] = true; }
            if (ItemPlanPagos.int_12 != 0) { bVisible[12] = true; }
            if (ItemPlanPagos.int_13 != 0) { bVisible[13] = true; }
            if (ItemPlanPagos.int_14 != 0) { bVisible[14] = true; }
            if (ItemPlanPagos.int_15 != 0) { bVisible[15] = true; }
        }
        for (int i = 0; i < 15; i++)
        {
            param[35 + i] = new ReportParameter("visible" + i, bVisible[i + 1].ToString());
        }
        param[50] = new ReportParameter("ImagenReport", cRutaDeImagen);

        ReportViewerPlan.LocalReport.EnableExternalImages = true;
        ReportViewerPlan.LocalReport.SetParameters(param);

        ReportDataSource rds1 = new ReportDataSource("DataSet1", table);
        ReportDataSource rds2 = new ReportDataSource("DataSet2", table2);
        ReportDataSource rds3 = new ReportDataSource("DataSet3", table3);
        ReportDataSource rds = new ReportDataSource("DataSetPlanPagos", CrearDataTable(idObjeto));

        ReportViewerPlan.LocalReport.DataSources.Clear();
        ReportViewerPlan.LocalReport.DataSources.Add(rds);
        ReportViewerPlan.LocalReport.DataSources.Add(rds1);
        ReportViewerPlan.LocalReport.DataSources.Add(rds2);
        ReportViewerPlan.LocalReport.DataSources.Add(rds3);
        ReportViewerPlan.LocalReport.Refresh();

        // Mostrar el reporte en pantalla.
        mvLista.ActiveViewIndex = 1;

    }

    private void Actualizar(String pIdObjeto)
    {
        try
        {
            String emptyQuery = "Fila de datos vacia";
            Credito datosApp = new Credito();
            datosApp.numero_radicacion = Int64.Parse(pIdObjeto);
            List<DatosPlanPagos> lstConsultaCreditos = new List<DatosPlanPagos>();
            lstConsultaCreditos.Clear();
            lstConsultaCreditos = datosServicio.ListarDatosPlanPagos(datosApp, (Usuario)Session["usuario"]);
            gvPlanPagos.EmptyDataText = emptyQuery;
            gvPlanPagos.DataSource = lstConsultaCreditos;
            if (lstConsultaCreditos.Count > 0)
            {
                mvLista.ActiveViewIndex = 0;
                gvPlanPagos.DataBind();
                ValidarPermisosGrilla(gvPlanPagos);
            }
            else
            {
                mvLista.ActiveViewIndex = -1;
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(creditoPlanServicio.CodigoPrograma, "Actualizar", ex);
        }
    }


    protected void gvPlanPagos_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvPlanPagos.PageIndex = e.NewPageIndex;
            Actualizar(idObjeto);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(creditoPlanServicio.CodigoPrograma, "vPlanPagos_PageIndexChanging", ex);
        }
    }

    private Xpinn.FabricaCreditos.Entities.Persona1 ObtenerValoresCodeudores()
    {
        Xpinn.FabricaCreditos.Entities.Persona1 vPersona1 = new Xpinn.FabricaCreditos.Entities.Persona1();

        if (idObjeto != "")
            vPersona1.numeroRadicacion = Convert.ToInt64(idObjeto.ToString());

        vPersona1.seleccionar = "CD"; //Bandera para ejecuta el select del CODEUDOR

        return vPersona1;
    }

    protected void txtNumRadic_TextChanged(object sender, EventArgs e)
    {

    }

    /// <summary>
    /// Método del botón para ir a imprimir el talonario del crédito
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button1_Click(object sender, EventArgs e)
    {
        Xpinn.FabricaCreditos.Services.CreditoService CreditoServicio = new Xpinn.FabricaCreditos.Services.CreditoService();
        Session[CreditoServicio.CodigoPrograma + ".id"] = txtNumRadic.Text;
        Session["talonario"] = 1;
        Navegar("~/Page/FabricaCreditos/GeneracionDocumentos/Detalle.aspx");

    }

    protected void BtnPagare_Click(object sender, EventArgs e)
    {
        string ruta = Server.MapPath("~/Page/FabricaCreditos/GeneracionDocumentos/Documentos/");

        if (Directory.Exists(ruta))
        {
            String fileName = txtNumRadic.Text + '_' + '1' + '.' + 'p' + 'd' + 'f';
            string savePath =ruta+fileName;

            FileInfo fi = new FileInfo(savePath);
            bool exists = fi.Exists;
            if (exists == false)
            {
                String Error = "No se envuentra pagaré generado";
                this.LblError.Text = Error;
            }
            else
            {
                FileStream archivo = new FileStream(savePath, FileMode.Open, FileAccess.Read);
                FileInfo file = new FileInfo(savePath);
                Response.ClearContent();
                Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName);
                Response.AddHeader("Content-Length", file.Length.ToString());
                Response.TransmitFile(file.FullName);
                Response.End();
            }

        }
        
    }

    protected void Btnautorizacion_Click(object sender, EventArgs e)
    {
        string ruta = Server.MapPath("~/Page/FabricaCreditos/GeneracionDocumentos/Documentos/");

        if (Directory.Exists(ruta))
        {
            String fileName = txtNumRadic.Text + '_' + '2' + '5' + '.' + 'p' + 'd' + 'f';
            string savePath = ruta + fileName;

            FileInfo fi = new FileInfo(savePath);
            bool exists = fi.Exists;
            if (exists == false)
            {
                String Error = "No se encuentra carta de instrucciones generada";
                this.LblError.Text = Error;
            }
            else
            {
                FileStream archivo = new FileStream(savePath, FileMode.Open, FileAccess.Read);
                FileInfo file = new FileInfo(savePath);
                Response.ClearContent();
                Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName);
                Response.AddHeader("Content-Length", file.Length.ToString());
                Response.TransmitFile(file.FullName);
                Response.End();
            }

        }
    }

    protected void btnInforme1_Click(object sender, EventArgs e)
    {
        Xpinn.FabricaCreditos.Entities.Persona1 vPersona1 = new Xpinn.FabricaCreditos.Entities.Persona1();

        Xpinn.FabricaCreditos.Entities.Persona1 vPersona2 = new Xpinn.FabricaCreditos.Entities.Persona1();
        Xpinn.FabricaCreditos.Entities.Persona1 vPersona3 = new Xpinn.FabricaCreditos.Entities.Persona1();
        Xpinn.FabricaCreditos.Entities.Referncias vPersona4 = new Xpinn.FabricaCreditos.Entities.Referncias();
        Xpinn.FabricaCreditos.Entities.Referencia refe = new Xpinn.FabricaCreditos.Entities.Referencia();
        Xpinn.FabricaCreditos.Entities.CreditoPlan creditos = new Xpinn.FabricaCreditos.Entities.CreditoPlan();
        Xpinn.FabricaCreditos.Services.Persona1Service DatosClienteServicio = new Xpinn.FabricaCreditos.Services.Persona1Service();
        Xpinn.FabricaCreditos.Entities.InformacionNegocio negocio = new Xpinn.FabricaCreditos.Entities.InformacionNegocio();
        Xpinn.FabricaCreditos.Entities.codeudores Codeudores = new Xpinn.FabricaCreditos.Entities.codeudores();
        Xpinn.FabricaCreditos.Services.codeudoresService ServicioCodeudores = new Xpinn.FabricaCreditos.Services.codeudoresService();
        Session["Identificacion"] = txtIdentific.Text;
        negocio = DatosClienteServicio.Consultardatosnegocio(Convert.ToInt64(txtNumRadic.Text), Session["Identificacion"].ToString(), (Usuario)Session["usuario"]);

        Codeudores = ServicioCodeudores.ConsultarDatosCodeudorRepo(Convert.ToString(txtNumRadic.Text),(Usuario)Session["usuario"]);
        Session["Codeudores"] = Codeudores.codpersona;
        creditos = DatosClienteServicio.ConsultarPersona1Paramcred(Convert.ToInt64(txtNumRadic.Text), Session["Identificacion"].ToString(), (Usuario)Session["usuario"]);

       
        mvLista.ActiveViewIndex = 1;

        List<Persona1> resultado = new List<Persona1>();
        List<Referencia> referencias = new List<Referencia>();
        vPersona1.numeroRadicacion = Convert.ToInt64(txtNumRadic.Text);
        vPersona1.cod_persona = Convert.ToInt64(Session["Cod_persona"].ToString());
        resultado = DatosClienteServicio.ListadoPersonas1Reporte(vPersona1, (Usuario)Session["usuario"]);
        referencias = DatosClienteServicio.referencias(vPersona1, Convert.ToInt64(txtNumRadic.Text), (Usuario)Session["usuario"]);

   
        System.Data.DataTable table2 = new System.Data.DataTable();
        table2.Columns.Add("OBSERVACIONES");
        table2.Columns.Add("NOMBRE");
        table2.Columns.Add("TIEMPO");
        table2.Columns.Add("PROPIETARIO");
        table2.Columns.Add("CONCEPTO");

        DataRow datarw2;
        for (int i = 0; i < referencias.Count; i++)
        {
            datarw2 = table2.NewRow();
            refe = referencias[i];

            datarw2[0] = " " + refe.observaciones;
            datarw2[1] = " " + refe.nombrereferencia;
            datarw2[2] = " " + refe.tiempo;
            datarw2[3] = " " + refe.propietario;
            datarw2[4] = " " + refe.concepto;
            table2.Rows.Add(datarw2);

        }


        ReportParameter[] parame = new ReportParameter[79];

        System.Data.DataTable table = new System.Data.DataTable();
        table.Columns.Add("nombre");
        table.Columns.Add("identificacion");
        table.Columns.Add("telefono");
        table.Columns.Add("direccion");
        table.Columns.Add("nombrer");
        table.Columns.Add("tipor");
        table.Columns.Add("parentesco");
        table.Columns.Add("direccionr");
        table.Columns.Add("telefonor");

        DataRow datarw;

        for (int i = 0; i < resultado.Count; i++)
        {
            datarw = table.NewRow();
            if (i == 0)
                vPersona1 = resultado[i];
            if (i == 1)
                vPersona2 = resultado[i];
            if (i >= 2)
            {
                vPersona2 = resultado[i];


                datarw[0] = vPersona2.primer_nombre + " " + vPersona2.segundo_nombre + " " + vPersona2.primer_apellido + " " + vPersona2.segundo_apellido;
                datarw[1] = vPersona3.cod_persona;
                datarw[2] = vPersona2.direccion;
                datarw[3] = vPersona2.telefono;
                datarw[4] = "";
                datarw[5] = "";
                datarw[6] = "";
                datarw[7] = "";
                datarw[8] = "";
               table.Rows.Add(datarw);
            }
            if (txtNumRadic.Text == "" || Convert.ToString(resultado[i].identificacion) == "")
            { }          
               
            
        }
        if (resultado.Count < 3)
        {

            parame[76] = new ReportParameter("datosvacios", "NO SE ENCONTRARON DATOS PARA ESTE ITEM");

            datarw = table.NewRow();
            datarw[0] = "";
            datarw[1] = "";
            datarw[2] = "";
            datarw[3] = "";
            datarw[4] = "";
            datarw[5] = "";
            datarw[6] = "";
            datarw[7] = "";
            datarw[8] = "";
            table.Rows.Add(datarw);
        }
        else
            parame[77] = new ReportParameter("datosvacios", " ");

        string cRutaDeImagen;
        cRutaDeImagen = Server.MapPath("~/Images\\") + "LogoEmpresa.jpg";
        Usuario pUsu = (Usuario)Session["usuario"];


        parame[0] = new ReportParameter("Nombres", " " + vPersona1.primer_nombre + " " + vPersona1.segundo_nombre);
        parame[1] = new ReportParameter("Identificación", " " + vPersona1.tipo_identif + " " + vPersona1.identificacion);
        parame[2] = new ReportParameter("Fecha_nacimiento", " " + Convert.ToDateTime(vPersona1.fechanacimiento).ToShortDateString());
        parame[3] = new ReportParameter("Nivel_Estudio", " ");
        parame[4] = new ReportParameter("Telefono", " " + vPersona1.telefono);
        parame[5] = new ReportParameter("Apellidos", " " + vPersona1.primer_apellido + " " + vPersona1.segundo_apellido);
        parame[6] = new ReportParameter("Lugar_Expedicion", " " + vPersona1.ciudadexpedicion);
        parame[7] = new ReportParameter("Sexo", " " + vPersona1.sexo);
        parame[8] = new ReportParameter("mail", " " + vPersona1.email);
        parame[9] = new ReportParameter("direccion", " " + vPersona1.direccion);
        //CONYUGE
        parame[10] = new ReportParameter("IdentificaciónConyuge", " " + vPersona2.tipo_identif + " " + vPersona2.identificacion);
        parame[11] = new ReportParameter("TelefonoConyuge", " " + vPersona2.telefono);
        parame[12] = new ReportParameter("ApellidosConyuge", " " + vPersona2.primer_apellido + " " + vPersona2.segundo_apellido);
        parame[13] = new ReportParameter("SexoConyuge", " " + vPersona2.sexo);
        parame[14] = new ReportParameter("mailConyuge", " " + vPersona2.email);
        parame[15] = new ReportParameter("NombresConyuge", " " + vPersona2.primer_nombre + " " + vPersona2.segundo_nombre);
        parame[16] = new ReportParameter("DireccionConyuge", " " + vPersona2.direccion);
        parame[17] = new ReportParameter("EmpresaConyuge", " " + vPersona2.empresa);
        parame[18] = new ReportParameter("ContratoConyuge", " " + vPersona2.tipocontrato);
        parame[19] = new ReportParameter("AntiguedadEmpConyuge", " " + vPersona2.antiguedadlugarempresa);
        parame[20] = new ReportParameter("CelularConyuge", " " + vPersona2.celular);
        parame[21] = new ReportParameter("CelularEmpConyuge", " " + vPersona2.CelularEmpresa);
        //CREDITO
        parame[22] = new ReportParameter("MontoSolicitado", " " + creditos.Monto.ToString("0,0", CultureInfo.InvariantCulture));   
        parame[23] = new ReportParameter("NumeroSolicitud", " " + creditos.Numero_Obligacion);
        parame[24] = new ReportParameter("NumerodeCredito", " " + creditos.Numero_radicacion);
        parame[25] = new ReportParameter("LineadeCredito", " " + creditos.LineaCredito);
        parame[26] = new ReportParameter("ValorCuota", " " + creditos.Cuota.ToString("0,0", CultureInfo.InvariantCulture));   
        parame[27] = new ReportParameter("FechaSolicitud", " " + creditos.FechaSolicitud.ToShortDateString());
        //NEGOCIO
        parame[28] = new ReportParameter("nombrenegocio", " " + negocio.nombrenegocio);
        parame[29] = new ReportParameter("descripcionnegocio", " " + negocio.descripcion);
        parame[30] = new ReportParameter("direccionnegocio", " " + negocio.direccion);
        parame[31] = new ReportParameter("telefononegocio", " " + negocio.telefono);
        parame[32] = new ReportParameter("Fecha_Expedicion", " " + Convert.ToDateTime(vPersona1.fechaexpedicion).ToShortDateString());
        parame[33] = new ReportParameter("Medio", " " + creditos.Medio);
        //persona
        parame[34] = new ReportParameter("TipoVivienda", " " + creditos.tipo_propiedad);
        parame[35] = new ReportParameter("ArrendadorViv", " " + creditos.arrendador);
        parame[36] = new ReportParameter("AntiguedadLugar", " " + creditos.antiguedad);
        parame[37] = new ReportParameter("TelArrendador", " " + creditos.telefonoarren);
        parame[38] = new ReportParameter("ValorArriendo", " " + creditos.valorarriendo);
        parame[39] = new ReportParameter("EstadoCivil", " " + creditos.EstadoCivil);
        // negocio
        parame[40] = new ReportParameter("Propiedad", " " + negocio.tipo_propiedad);
        parame[41] = new ReportParameter("Arrendador", " " + negocio.arrendador);
        parame[42] = new ReportParameter("TelefonoArrendador", " " + negocio.telefonoarrendador);
        parame[43] = new ReportParameter("Arriendo", " " + negocio.valor_arriendo);
        parame[44] = new ReportParameter("Experiencia", " " + negocio.experiencia);
        parame[45] = new ReportParameter("Antiguedad", " " + negocio.antiguedad);
        parame[46] = new ReportParameter("EmpleadosPerm", " " + negocio.emplperm);
        parame[47] = new ReportParameter("EmpleadosTemp", " " + negocio.empltem);
        parame[48] = new ReportParameter("Barrio", " " + negocio.barrioneg);
        parame[49] = new ReportParameter("Actividad", " " + negocio.descactividad);
        // codeudores
        parame[50] = new ReportParameter("NomCodeudor", " " + Codeudores.primer_nombre + " " + Codeudores.segundo_nombre + " " + Codeudores.primer_apellido + " "+ Codeudores.segundo_apellido);
        parame[51] = new ReportParameter("IdentificacionCod", " " + Codeudores.identificacion);
        parame[52] = new ReportParameter("TIdentificacioncod", " " + Codeudores.tipo_identificacion);
        parame[53] = new ReportParameter("EstadoCivilCod", " " + Codeudores.estadocivil);
        parame[54] = new ReportParameter("EscolaridadCod", " " + Codeudores.escolaridad);
        parame[55] = new ReportParameter("DireccionCod", " " + Codeudores.direccion);
        parame[56] = new ReportParameter("BarrioCod", " " + Codeudores.barrio);
        parame[57] = new ReportParameter("TelCod", " " + Codeudores.telefono);
        parame[58] = new ReportParameter("TipoViviendaCod", " " + Codeudores.tipovivienda);
        parame[59] = new ReportParameter("NumPersCargoCod", " " + Codeudores.personascargo);
        parame[60] = new ReportParameter("ArrendadorCod", " " + Codeudores.arrendador);
        parame[61] = new ReportParameter("TelArrendadorCod", " " + Codeudores.telefonoarrendador);
        parame[62] = new ReportParameter("EmpresaCod", " " + Codeudores.empresa);
        parame[63] = new ReportParameter("CargoCod", " " + Codeudores.cargo);
        parame[64] = new ReportParameter("AntiguedadCod", " " + Codeudores.antiguedadempresa);
        parame[65] = new ReportParameter("ContratoCod", " " + Codeudores.tipocontrato);
        parame[66] = new ReportParameter("FechaExpediCod", " " + Codeudores.fechaexpedicion.ToShortDateString());
        parame[67] = new ReportParameter("CiudadExpeCod", " " + Codeudores.ciudadexpedicion);
        parame[68] = new ReportParameter("ValorArriendoCod", " " + Codeudores.valorarriendo);
        parame[69] = new ReportParameter("DirEmpresaCod", " " + Codeudores.direccionempresa);
        parame[70] = new ReportParameter("TelEmpresaCod", " " + Codeudores.telefonoempresa);
      
        Usuario User = (Usuario)Session["usuario"];
        // Analisis solicitud 
        parame[71] = new ReportParameter("Concepto", " " + creditos.concepto);
        parame[72] = new ReportParameter("Monto", " " + creditos.Monto.ToString("0,0", CultureInfo.InvariantCulture));    
        parame[73] = new ReportParameter("Plazo", " " + creditos.Plazo);
        parame[74] = new ReportParameter("Cuota", " " + creditos.Cuota.ToString("0,0", CultureInfo.InvariantCulture));    
        parame[75] = new ReportParameter("Opinion", " " + creditos.Observaciones);
        parame[76] = new ReportParameter("nomUsuario", " " + User.nombre);
        parame[77] = new ReportParameter("ImagenReport", cRutaDeImagen);
        parame[78] = new ReportParameter("entidad", pUsu.empresa);

        ReportViewersolicitud.LocalReport.EnableExternalImages = true;
        ReportViewersolicitud.LocalReport.SetParameters(parame);

        ReportViewersolicitud.LocalReport.DataSources.Clear();
        ReportDataSource rds1 = new ReportDataSource("DataSet1", table);
        ReportViewersolicitud.LocalReport.DataSources.Add(rds1);
        ReportDataSource rds2 = new ReportDataSource("DataSet2", CrearDataTablereferencias());
        ReportViewersolicitud.LocalReport.DataSources.Add(rds2);
        ReportDataSource rds3 = new ReportDataSource("DataSet3", CrearDataTableCodeudores());
        ReportViewersolicitud.LocalReport.DataSources.Add(rds3);
        ReportDataSource rds4 = new ReportDataSource("DataSet4", CrearDataTableFamiliares());
        ReportViewersolicitud.LocalReport.DataSources.Add(rds4);
        ReportDataSource rds5 = new ReportDataSource("DataSet5", CrearDataTableVentasSemanales());
        ReportViewersolicitud.LocalReport.DataSources.Add(rds5);
        ReportDataSource rds6 = new ReportDataSource("DataSet6", CrearDataTableVentasMensuales());
        ReportViewersolicitud.LocalReport.DataSources.Add(rds6);
        ReportDataSource rds7 = new ReportDataSource("DataSet7", CrearDataTableMargenVentas());
        ReportViewersolicitud.LocalReport.DataSources.Add(rds7);
        ReportDataSource rds8 = new ReportDataSource("DataSet8", CrearDataTableInfNegocio());
        ReportViewersolicitud.LocalReport.DataSources.Add(rds8);
        ReportDataSource rds9 = new ReportDataSource("DataSet9", CrearDataTableInffinfamNegocio());
        ReportViewersolicitud.LocalReport.DataSources.Add(rds9);       
        ReportDataSource rds10 = new ReportDataSource("DataSet10", CrearDataTableInffinfamNegocioegresos());
        ReportViewersolicitud.LocalReport.DataSources.Add(rds10);
        ReportDataSource rds11 = new ReportDataSource("DataSet11", CrearDataTableActivos());
        ReportViewersolicitud.LocalReport.DataSources.Add(rds11);
        ReportDataSource rds12 = new ReportDataSource("DataSet12", CrearDataTablePasivos());
        ReportViewersolicitud.LocalReport.DataSources.Add(rds12);
        ReportDataSource rds13 = new ReportDataSource("DataSet13",CrearDataTableBalanceFamActivos());
        ReportViewersolicitud.LocalReport.DataSources.Add(rds13);
        ReportDataSource rds14 = new ReportDataSource("DataSet14", CrearDataTableBalanceFamPasivos());
        ReportViewersolicitud.LocalReport.DataSources.Add(rds14);
        ReportDataSource rds15 = new ReportDataSource("DataSet15", CrearDataTableComposicionPasivo());
        ReportViewersolicitud.LocalReport.DataSources.Add(rds15);
        ReportDataSource rds16 = new ReportDataSource("DataSet16", CrearDataTableInvenACtivoFijos());
        ReportViewersolicitud.LocalReport.DataSources.Add(rds16);
        ReportDataSource rds17 = new ReportDataSource("DataSet17", CrearDataTableInvenMateriaPrima());
        ReportViewersolicitud.LocalReport.DataSources.Add(rds17);
        ReportDataSource rds18 = new ReportDataSource("DataSet18", CrearDataTableProductosProceso());
        ReportViewersolicitud.LocalReport.DataSources.Add(rds18);
        ReportDataSource rds19 = new ReportDataSource("DataSet19", CrearDataTableProductosTerminados());
        ReportViewersolicitud.LocalReport.DataSources.Add(rds19);
        ReportDataSource rds20 = new ReportDataSource("DataSet20", CrearDataTableRelacionDocumentos());
        ReportViewersolicitud.LocalReport.DataSources.Add(rds20);
        ReportDataSource rds21 = new ReportDataSource("DataSet21",CrearDataTableRelacionDocDesembolso());
        ReportViewersolicitud.LocalReport.DataSources.Add(rds21);
        ReportDataSource rds22 = new ReportDataSource("DataSet22", CrearDataTableReldocControltiempo());
        ReportViewersolicitud.LocalReport.DataSources.Add(rds22);
        ReportDataSource rds23 = new ReportDataSource("DataSet23", CrearDataTableBienesUnidadFamCode());
        ReportViewersolicitud.LocalReport.DataSources.Add(rds23);
        ReportDataSource rds24 = new ReportDataSource("DataSet24", CrearDataTableVehiculosCode());
        ReportViewersolicitud.LocalReport.DataSources.Add(rds24);
        ReportDataSource rds25 = new ReportDataSource("DataSet25", CrearDataTablePresupuestoEmpresarialCode());
        ReportViewersolicitud.LocalReport.DataSources.Add(rds25);
        ReportDataSource rds26 = new ReportDataSource("DataSet26", CrearDataTablePresupuestoFamiliarCode());
        ReportViewersolicitud.LocalReport.DataSources.Add(rds26);
        ReportDataSource rds27 = new ReportDataSource("DataSet27", CrearDataTableReferenciasCodeudor());
        ReportViewersolicitud.LocalReport.DataSources.Add(rds27);
        ReportDataSource rds28 = new ReportDataSource("DataSet28", CrearDataTableViabilidadFinanciera());
        ReportViewersolicitud.LocalReport.DataSources.Add(rds28); 

        ReportViewersolicitud.LocalReport.Refresh();
        mvLista.ActiveViewIndex = 0;
        mvLista0.ActiveViewIndex = 1;
    }

    public DataTable CrearDataTablereferencias()
    {
        Xpinn.FabricaCreditos.Services.Persona1Service DatosClienteServicio = new Xpinn.FabricaCreditos.Services.Persona1Service();
        List<Referncias> LstReferencias = new List<Referncias>();          
        Referncias referencia = new Referncias();
        referencia.numero_radicacion = Convert.ToInt64(this.txtNumRadic.Text);


        LstReferencias = DatosClienteServicio.ListadoPersonas1ReporteReferencias(referencia, (Usuario)Session["usuario"]);
        System.Data.DataTable table = new System.Data.DataTable();

        table.Columns.Add("nombre");
        table.Columns.Add("identificacion");
        table.Columns.Add("telefono");
        table.Columns.Add("direccion");
        table.Columns.Add("nombrer");
        table.Columns.Add("tipor");
        table.Columns.Add("parentesco");
        table.Columns.Add("direccionr");
        table.Columns.Add("telefonor");
        table.Columns.Add("identificacionr");
        foreach (Referncias vPersona4 in LstReferencias)
        {

            DataRow datarw;
            datarw = table.NewRow(); 
            datarw[0] = "";
            datarw[1] = "";
            datarw[2] = "";
            datarw[3] = "";
            datarw[4] = vPersona4.nombres;
            datarw[5] = vPersona4.descripcion;
            datarw[6] = vPersona4.ListaDescripcion;
            datarw[7] = vPersona4.direccion;
            datarw[8] = vPersona4.telefono;
            datarw[9] = vPersona4.identificacion;
            table.Rows.Add(datarw);
         }
        return table;
    }

    public DataTable CrearDataTableCodeudores()
    {
        Xpinn.FabricaCreditos.Services.Persona1Service DatosClienteServicio = new Xpinn.FabricaCreditos.Services.Persona1Service();
        List<Persona1> LstCodeudor= new List<Persona1>();
        Persona1 codeudor = new Persona1();
        codeudor.numeroRadicacion = Convert.ToInt64(this.txtNumRadic.Text);


        LstCodeudor = DatosClienteServicio.ListadoPersonas1ReporteCodeudor(codeudor, (Usuario)Session["usuario"]);
        System.Data.DataTable table = new System.Data.DataTable();

        table.Columns.Add("nombre");
        table.Columns.Add("identificacion");
        table.Columns.Add("telefono");
        table.Columns.Add("direccion");
        table.Columns.Add("barrio");
       

        foreach (Persona1 vCodeudor in LstCodeudor)
        {

            DataRow datarw;
            datarw = table.NewRow();
            datarw[0] = vCodeudor.primer_nombre + ' ' +vCodeudor.segundo_nombre + ' '+ vCodeudor.primer_apellido + ' ' + vCodeudor.segundo_apellido;
            datarw[1] = vCodeudor.identificacion;
            datarw[2] = vCodeudor.direccion;
            datarw[3] = vCodeudor.telefono;
            datarw[4] = vCodeudor.barrioCorresponden;
            table.Rows.Add(datarw);
        }
        return table;
    }

    public DataTable CrearDataTableFamiliares()
    {
        Xpinn.FabricaCreditos.Services.Persona1Service DatosClienteServicio = new Xpinn.FabricaCreditos.Services.Persona1Service();
        List<Persona1> LstFamiliar = new List<Persona1>();
        Persona1 familiar = new Persona1();
        familiar.identificacion = Convert.ToString(txtIdentific.Text);


        LstFamiliar = DatosClienteServicio.ListadoPersonas1ReporteFamiliares(familiar, (Usuario)Session["usuario"]);
        System.Data.DataTable table = new System.Data.DataTable();

        table.Columns.Add("nombre");
        table.Columns.Add("Parentesco");
        table.Columns.Add("Sexo");
        table.Columns.Add("ACargo");
        table.Columns.Add("FechaNacimiento");       
        table.Columns.Add("Observaciones");

        foreach (Persona1 vFamiliar in LstFamiliar)
        {

            DataRow datarw;
            datarw = table.NewRow();
            datarw[0] = vFamiliar.nombres;
            datarw[1] = vFamiliar.parentesco;
            datarw[2] = vFamiliar.sexo;
            datarw[3] = vFamiliar.acargo;
            datarw[4] = Convert.ToDateTime(vFamiliar.fechanacimiento).ToShortDateString();
            datarw[5] = vFamiliar.Observaciones;
            table.Rows.Add(datarw);
        }
        return table;
    }

    public DataTable CrearDataTableVentasSemanales()
    {
        Xpinn.FabricaCreditos.Services.Persona1Service DatosClienteServicio = new Xpinn.FabricaCreditos.Services.Persona1Service();
       // Xpinn.FabricaCreditos.Services.VentasSemanalesService VentasSemanalesServicio = new Xpinn.FabricaCreditos.Services.VentasSemanalesService();
        List<VentasSemanales> LstVentassemanales = new List<VentasSemanales>();
        VentasSemanales cliente = new VentasSemanales();
        cliente.identificacion = Convert.ToString(txtIdentific.Text);

        LstVentassemanales = DatosClienteServicio.ListadoEstacionalidadSemanal(cliente, (Usuario)Session["usuario"]);
        System.Data.DataTable table = new System.Data.DataTable();
        table.Columns.Add("TipoVenta");
        table.Columns.Add("Valor");
        table.Columns.Add("Lunes");
        table.Columns.Add("Martes");
        table.Columns.Add("Miercoles");
        table.Columns.Add("Jueves");
        table.Columns.Add("Viernes");
        table.Columns.Add("Sabado");
        table.Columns.Add("Domingo");
        table.Columns.Add("Total");
        foreach (VentasSemanales vVentasSemanales in LstVentassemanales)
        {

            DataRow datarw;
            datarw = table.NewRow();
            datarw[0] = vVentasSemanales.tipoventa;
            datarw[1] = vVentasSemanales.valor.ToString("0,0", CultureInfo.InvariantCulture); ;
            datarw[2] = vVentasSemanales.lunesrepo;
            datarw[3] = vVentasSemanales.martesrepo;
            datarw[4] = vVentasSemanales.miercolesrepo;
            datarw[5] = vVentasSemanales.juevesrepo;
            datarw[6] = vVentasSemanales.viernesrepo;
            datarw[7] = vVentasSemanales.sabadorepo;
            datarw[8] = vVentasSemanales.domingorepo;
            datarw[9] = vVentasSemanales.totalSemanal.ToString(); 
             table.Rows.Add(datarw);
        }
        return table;
    }

    public DataTable CrearDataTableVentasMensuales()
    {
        Xpinn.FabricaCreditos.Services.Persona1Service DatosClienteServicio = new Xpinn.FabricaCreditos.Services.Persona1Service();
        // Xpinn.FabricaCreditos.Services.VentasSemanalesService VentasSemanalesServicio = new Xpinn.FabricaCreditos.Services.VentasSemanalesService();
        List<EstacionalidadMensual> LstVentasmensuales = new List<EstacionalidadMensual>();
        EstacionalidadMensual cliente = new EstacionalidadMensual();
        cliente.identificacion= Convert.ToString(txtIdentific.Text);

        LstVentasmensuales = DatosClienteServicio.ListadoEstacionalidadMensual(cliente, (Usuario)Session["usuario"]);
        System.Data.DataTable table = new System.Data.DataTable();
        table.Columns.Add("TipoVenta");
        table.Columns.Add("Valor");
        table.Columns.Add("Enero");
        table.Columns.Add("Febrero");
        table.Columns.Add("Marzo");
        table.Columns.Add("Abril");
        table.Columns.Add("Mayo");
        table.Columns.Add("Junio");
        table.Columns.Add("Julio");
        table.Columns.Add("Agosto");
        table.Columns.Add("Septiembre");
        table.Columns.Add("Octubre");
        table.Columns.Add("Noviembre");
        table.Columns.Add("Diciembre");
        table.Columns.Add("Total");
        foreach (EstacionalidadMensual vVentasMensuales in LstVentasmensuales)
        {

            DataRow datarw;
            datarw = table.NewRow();
            datarw[0] = vVentasMensuales.tipoventa;
            datarw[1] = vVentasMensuales.valor.ToString("0,0", CultureInfo.InvariantCulture); 
            datarw[2] = vVentasMensuales.enerorepo;
            datarw[3] = vVentasMensuales.febrerorepo;
            datarw[4] = vVentasMensuales.marzorepo;
            datarw[5] = vVentasMensuales.abrilrepo;
            datarw[6] = vVentasMensuales.mayorepo;
            datarw[7] = vVentasMensuales.juniorepo;
            datarw[8] = vVentasMensuales.juliorepo;
            datarw[9] = vVentasMensuales.agostorepo;
            datarw[10] = vVentasMensuales.septiembrerepo;
            datarw[11] = vVentasMensuales.octubrerepo;
            datarw[12] = vVentasMensuales.noviembrerepo;
            datarw[13] = vVentasMensuales.diciembrerepo;
            datarw[14] = vVentasMensuales.totalMensual.ToString("0,0", CultureInfo.InvariantCulture); 
            table.Rows.Add(datarw);
        }
        return table;
    }

    public DataTable CrearDataTableMargenVentas()
    {
        Xpinn.FabricaCreditos.Services.MargenVentasService ventasServicio = new Xpinn.FabricaCreditos.Services.MargenVentasService();
        List<MargenVentas> LstMargenVentas = new List<MargenVentas>();
        MargenVentas cliente = new MargenVentas();
        Xpinn.FabricaCreditos.Entities.Persona1 vPersona1 = new Xpinn.FabricaCreditos.Entities.Persona1();        
        vPersona1.cod_persona = Convert.ToInt64(Session["Cod_persona"].ToString());
        cliente.cod_persona = vPersona1.cod_persona;

        LstMargenVentas = ventasServicio.ListarMargenVentas(cliente, (Usuario)Session["usuario"]);
        System.Data.DataTable table = new System.Data.DataTable();
        table.Columns.Add("NombreProducto");
        table.Columns.Add("UnidadesVendidas");
        table.Columns.Add("CostoUnidadVendida");
        table.Columns.Add("PrecioVentaUnidad");
        table.Columns.Add("CostoDeVentas");
        table.Columns.Add("VentaTotal");       
        
        foreach (MargenVentas vMargenVentas in LstMargenVentas)
        {

            DataRow datarw;
            datarw = table.NewRow();
            datarw[0] = vMargenVentas.nombreproducto;
            datarw[1] = vMargenVentas.univendida;
            datarw[2] = vMargenVentas.costounidven.ToString("0,0", CultureInfo.InvariantCulture); 
            datarw[3] = vMargenVentas.preciounidven.ToString("0,0", CultureInfo.InvariantCulture); 
            datarw[4] = vMargenVentas.costoventa.ToString(); 
            datarw[5] = vMargenVentas.ventatotal.ToString(); 
           
         
            table.Rows.Add(datarw);
        }
        return table;
    }

    
    public DataTable CrearDataTableInfNegocio()
    {
        Xpinn.FabricaCreditos.Services.InformacionFinancieraService informacionfinancieraServicio = new Xpinn.FabricaCreditos.Services.InformacionFinancieraService();
        List<InformacionFinanciera> LstInformacionFinanciera = new List<InformacionFinanciera>();
        InformacionFinanciera cliente = new InformacionFinanciera();
        Xpinn.FabricaCreditos.Entities.Persona1 vPersona1 = new Xpinn.FabricaCreditos.Entities.Persona1();
        vPersona1.cod_persona = Convert.ToInt64(Session["Cod_persona"].ToString());
        cliente.cod_persona = vPersona1.cod_persona;

        LstInformacionFinanciera = informacionfinancieraServicio.ListarInformacionFinanNegRepo(cliente, (Usuario)Session["usuario"]);
        System.Data.DataTable table = new System.Data.DataTable();
        table.Columns.Add("Cuenta");
        table.Columns.Add("Descripcion");
        table.Columns.Add("Valor");
        

        foreach (InformacionFinanciera vinformacionfinancieraServicio in LstInformacionFinanciera)
        {

            DataRow datarw;
            datarw = table.NewRow();
            datarw[0] = vinformacionfinancieraServicio.cuenta;
            datarw[1] = vinformacionfinancieraServicio.descripcion;
            datarw[2] = vinformacionfinancieraServicio.valor.ToString("0,0", CultureInfo.InvariantCulture);    
  
           

            table.Rows.Add(datarw);
        }
        return table;
    }

    public DataTable CrearDataTableInffinfamNegocio()
    {
        Xpinn.FabricaCreditos.Services.InformacionFinancieraService informacionfinancieraServicio = new Xpinn.FabricaCreditos.Services.InformacionFinancieraService();
        List<InformacionFinanciera> LstInformacionFinanciera = new List<InformacionFinanciera>();
        InformacionFinanciera cliente = new InformacionFinanciera();
        Xpinn.FabricaCreditos.Entities.Persona1 vPersona1 = new Xpinn.FabricaCreditos.Entities.Persona1();
        vPersona1.cod_persona = Convert.ToInt64(Session["Cod_persona"].ToString());
        cliente.cod_persona = vPersona1.cod_persona;

        LstInformacionFinanciera = informacionfinancieraServicio.ListarInformacionFinanFamRepo(cliente, (Usuario)Session["usuario"]);
        System.Data.DataTable table = new System.Data.DataTable();
        table.Columns.Add("Cuenta");
        table.Columns.Add("Descripcion");
        table.Columns.Add("Valor");
        

        foreach (InformacionFinanciera vinformacionfinancieraServicio in LstInformacionFinanciera)
        {

            DataRow datarw;
            datarw = table.NewRow();
            datarw[0] = vinformacionfinancieraServicio.cuenta;
            datarw[1] = vinformacionfinancieraServicio.descripcion;
            datarw[2] = vinformacionfinancieraServicio.valor.ToString("0,0", CultureInfo.InvariantCulture); 
           

            table.Rows.Add(datarw);
        }
        return table;
    }

    public DataTable CrearDataTableInffinfamNegocioegresos()
    {
        Xpinn.FabricaCreditos.Services.InformacionFinancieraService informacionfinancieraServicio = new Xpinn.FabricaCreditos.Services.InformacionFinancieraService();
        List<InformacionFinanciera> LstInformacionFinanciera = new List<InformacionFinanciera>();
        InformacionFinanciera cliente = new InformacionFinanciera();
        Xpinn.FabricaCreditos.Entities.Persona1 vPersona1 = new Xpinn.FabricaCreditos.Entities.Persona1();
        vPersona1.cod_persona = Convert.ToInt64(Session["Cod_persona"].ToString());
        cliente.cod_persona = vPersona1.cod_persona;

        LstInformacionFinanciera = informacionfinancieraServicio.ListarInformacionFinanFamRepoeg(cliente, (Usuario)Session["usuario"]);
        System.Data.DataTable table = new System.Data.DataTable();
        table.Columns.Add("Cuenta");
        table.Columns.Add("Descripcion");
        table.Columns.Add("Valor");


        foreach (InformacionFinanciera vinformacionfinancieraServicio in LstInformacionFinanciera)
        {

            DataRow datarw;
            datarw = table.NewRow();
            datarw[0] = vinformacionfinancieraServicio.cuenta;
            datarw[1] = vinformacionfinancieraServicio.descripcion;
            datarw[2] = vinformacionfinancieraServicio.valor.ToString("0,0", CultureInfo.InvariantCulture); 


            table.Rows.Add(datarw);
        }
        return table;
    }

    public DataTable CrearDataTableActivos()
    {
        Xpinn.FabricaCreditos.Services.InformacionFinancieraService informacionfinancieraServicio = new Xpinn.FabricaCreditos.Services.InformacionFinancieraService();
        List<InformacionFinanciera> LstInformacionFinanciera = new List<InformacionFinanciera>();
        InformacionFinanciera cliente = new InformacionFinanciera();
        Xpinn.FabricaCreditos.Entities.Persona1 vPersona1 = new Xpinn.FabricaCreditos.Entities.Persona1();
        vPersona1.cod_persona = Convert.ToInt64(Session["Cod_persona"].ToString());
        cliente.cod_persona = vPersona1.cod_persona;

        LstInformacionFinanciera = informacionfinancieraServicio.ListarActivos(cliente, (Usuario)Session["usuario"]);
        System.Data.DataTable table = new System.Data.DataTable();
        table.Columns.Add("Cuenta");
        table.Columns.Add("Descripcion");
        table.Columns.Add("Valor");

        foreach (InformacionFinanciera vinformacionfinancieraServicio in LstInformacionFinanciera)
        {

            DataRow datarw;
            datarw = table.NewRow();
            datarw[0] = vinformacionfinancieraServicio.cuenta;
            datarw[1] = vinformacionfinancieraServicio.descripcion;
            datarw[2] = vinformacionfinancieraServicio.valor.ToString("0,0", CultureInfo.InvariantCulture);


            table.Rows.Add(datarw);
        }
        return table;
    }

    public DataTable CrearDataTablePasivos()
    {
        Xpinn.FabricaCreditos.Services.InformacionFinancieraService informacionfinancieraServicio = new Xpinn.FabricaCreditos.Services.InformacionFinancieraService();
        List<InformacionFinanciera> LstInformacionFinanciera = new List<InformacionFinanciera>();
        InformacionFinanciera cliente = new InformacionFinanciera();
        Xpinn.FabricaCreditos.Entities.Persona1 vPersona1 = new Xpinn.FabricaCreditos.Entities.Persona1();
        vPersona1.cod_persona = Convert.ToInt64(Session["Cod_persona"].ToString());
        cliente.cod_persona = vPersona1.cod_persona;

        LstInformacionFinanciera = informacionfinancieraServicio.ListarPasivos(cliente, (Usuario)Session["usuario"]);
        System.Data.DataTable table = new System.Data.DataTable();
        table.Columns.Add("Cuenta");
        table.Columns.Add("Descripcion");
        table.Columns.Add("Valor");

        foreach (InformacionFinanciera vinformacionfinancieraServicio in LstInformacionFinanciera)
        {

            DataRow datarw;
            datarw = table.NewRow();
            datarw[0] = vinformacionfinancieraServicio.cuenta;
            datarw[1] = vinformacionfinancieraServicio.descripcion;
            datarw[2] = vinformacionfinancieraServicio.valor.ToString("0,0", CultureInfo.InvariantCulture); 


            table.Rows.Add(datarw);
        }
        return table;
    }

    public DataTable CrearDataTableBalanceFamActivos()
    {
        Xpinn.FabricaCreditos.Services.InformacionFinancieraService informacionfinancieraServicio = new Xpinn.FabricaCreditos.Services.InformacionFinancieraService();
        List<InformacionFinanciera> LstInformacionFinanciera = new List<InformacionFinanciera>();
        InformacionFinanciera cliente = new InformacionFinanciera();
        Xpinn.FabricaCreditos.Entities.Persona1 vPersona1 = new Xpinn.FabricaCreditos.Entities.Persona1();
        vPersona1.cod_persona = Convert.ToInt64(Session["Cod_persona"].ToString());
        cliente.cod_persona = vPersona1.cod_persona;

        LstInformacionFinanciera = informacionfinancieraServicio.ListarbalanceFamActivos(cliente, (Usuario)Session["usuario"]);
        System.Data.DataTable table = new System.Data.DataTable();
        table.Columns.Add("Cuenta");
        table.Columns.Add("Descripcion");
        table.Columns.Add("Valor");

        foreach (InformacionFinanciera vinformacionfinancieraServicio in LstInformacionFinanciera)
        {

            DataRow datarw;
            datarw = table.NewRow();
            datarw[0] = vinformacionfinancieraServicio.cuenta;
            datarw[1] = vinformacionfinancieraServicio.descripcion;
            datarw[2] = vinformacionfinancieraServicio.valor.ToString("0,0", CultureInfo.InvariantCulture); 


            table.Rows.Add(datarw);
        }
        return table;
    }

    public DataTable CrearDataTableBalanceFamPasivos()
    {
        Xpinn.FabricaCreditos.Services.InformacionFinancieraService informacionfinancieraServicio = new Xpinn.FabricaCreditos.Services.InformacionFinancieraService();
        List<InformacionFinanciera> LstInformacionFinanciera = new List<InformacionFinanciera>();
        InformacionFinanciera cliente = new InformacionFinanciera();
        Xpinn.FabricaCreditos.Entities.Persona1 vPersona1 = new Xpinn.FabricaCreditos.Entities.Persona1();
        vPersona1.cod_persona = Convert.ToInt64(Session["Cod_persona"].ToString());
        cliente.cod_persona = vPersona1.cod_persona;

        LstInformacionFinanciera = informacionfinancieraServicio.ListarbalanceFamPasivos(cliente, (Usuario)Session["usuario"]);
        System.Data.DataTable table = new System.Data.DataTable();
        table.Columns.Add("Cuenta");
        table.Columns.Add("Descripcion");
        table.Columns.Add("Valor");


        foreach (InformacionFinanciera vinformacionfinancieraServicio in LstInformacionFinanciera)
        {

            DataRow datarw;
            datarw = table.NewRow();
            datarw[0] = vinformacionfinancieraServicio.cuenta;
            datarw[1] = vinformacionfinancieraServicio.descripcion;
            datarw[2] = vinformacionfinancieraServicio.valor.ToString("0,0", CultureInfo.InvariantCulture);


            table.Rows.Add(datarw);
        }
        return table;
    }

    public DataTable CrearDataTableComposicionPasivo()
    {
        Xpinn.FabricaCreditos.Services.ComposicionPasivoService informacionfinancieraServicio = new Xpinn.FabricaCreditos.Services.ComposicionPasivoService();
        List<ComposicionPasivo> LstInformacionFinanciera = new List<ComposicionPasivo>();
        ComposicionPasivo cliente = new ComposicionPasivo();
        Xpinn.FabricaCreditos.Entities.Persona1 vPersona1 = new Xpinn.FabricaCreditos.Entities.Persona1();
        vPersona1.cod_persona = Convert.ToInt64(Session["Cod_persona"].ToString());
        cliente.cod_persona = vPersona1.cod_persona;

        LstInformacionFinanciera = informacionfinancieraServicio.ListarComposicionPasivoRepo(cliente, (Usuario)Session["usuario"]);
        System.Data.DataTable table = new System.Data.DataTable();
        table.Columns.Add("Acreedor");
        table.Columns.Add("MontoOtorgado");
        table.Columns.Add("ValorCuota");
        table.Columns.Add("Frecuencia");
        table.Columns.Add("CuotaActual");
        table.Columns.Add("Plazo");

        foreach (ComposicionPasivo vinformacionfinancieraServicio in LstInformacionFinanciera)
        {

            DataRow datarw;
            datarw = table.NewRow();
            datarw[0] = vinformacionfinancieraServicio.acreedor;
            datarw[1] = vinformacionfinancieraServicio.monto_otorgado.ToString("0,0", CultureInfo.InvariantCulture);
            datarw[2] = vinformacionfinancieraServicio.valor_cuota.ToString("0,0", CultureInfo.InvariantCulture);
            datarw[3] = vinformacionfinancieraServicio.periodicidad;
            datarw[4] = vinformacionfinancieraServicio.cuota;
            datarw[5] = vinformacionfinancieraServicio.plazo;
            
            table.Rows.Add(datarw);
        }
        return table;
    }

    public DataTable CrearDataTableInvenACtivoFijos()
    {
        Xpinn.FabricaCreditos.Services.InventarioActivoFijoService informacionfinancieraServicio = new Xpinn.FabricaCreditos.Services.InventarioActivoFijoService();
        List<InventarioActivoFijo> LstInformacionFinanciera = new List<InventarioActivoFijo>();
        InventarioActivoFijo cliente = new InventarioActivoFijo();
        Xpinn.FabricaCreditos.Entities.Persona1 vPersona1 = new Xpinn.FabricaCreditos.Entities.Persona1();
        vPersona1.cod_persona = Convert.ToInt64(Session["Cod_persona"].ToString());
        cliente.cod_persona = vPersona1.cod_persona;

        LstInformacionFinanciera = informacionfinancieraServicio.ListarInventarioActivoFijoRepo(cliente, (Usuario)Session["usuario"]);
        System.Data.DataTable table = new System.Data.DataTable();
        table.Columns.Add("Descripcion");
        table.Columns.Add("Marca");
        table.Columns.Add("Valor");


        foreach (InventarioActivoFijo vinformacionfinancieraServicio in LstInformacionFinanciera)
        {

            DataRow datarw;
            datarw = table.NewRow();
            datarw[0] = vinformacionfinancieraServicio.descripcion;
            datarw[1] = vinformacionfinancieraServicio.marca;
            datarw[2] = vinformacionfinancieraServicio.valor.ToString();
          //  table.Compute("SUM(valor)", string.Empty);
            table.Rows.Add(datarw);
        }
        
        return table;
    }

    public DataTable CrearDataTableInvenMateriaPrima()
    {
        Xpinn.FabricaCreditos.Services.InventarioMateriaPrimaService informacionfinancieraServicio = new Xpinn.FabricaCreditos.Services.InventarioMateriaPrimaService();
        List<InventarioMateriaPrima> LstInformacionFinanciera = new List<InventarioMateriaPrima>();
        InventarioMateriaPrima cliente = new InventarioMateriaPrima();
        Xpinn.FabricaCreditos.Entities.Persona1 vPersona1 = new Xpinn.FabricaCreditos.Entities.Persona1();
        vPersona1.cod_persona = Convert.ToInt64(Session["Cod_persona"].ToString());
        cliente.cod_persona = vPersona1.cod_persona;

        LstInformacionFinanciera = informacionfinancieraServicio.ListarInventarioMateriaPrimaRepo(cliente, (Usuario)Session["usuario"]);
        System.Data.DataTable table = new System.Data.DataTable();
        table.Columns.Add("Descripcion");       
        table.Columns.Add("Valor");

        foreach (InventarioMateriaPrima vinformacionfinancieraServicio in LstInformacionFinanciera)
        {

            DataRow datarw;
            datarw = table.NewRow();
            datarw[0] = vinformacionfinancieraServicio.descripcion;
            datarw[1] = vinformacionfinancieraServicio.valor.ToString();

            table.Rows.Add(datarw);
        }
        return table;
    }

    public DataTable CrearDataTableProductosProceso()
    {
        Xpinn.FabricaCreditos.Services.ProductosProcesoService informacionfinancieraServicio = new Xpinn.FabricaCreditos.Services.ProductosProcesoService();
        List<ProductosProceso> LstInformacionFinanciera = new List<ProductosProceso>();
        ProductosProceso cliente = new ProductosProceso();
        Xpinn.FabricaCreditos.Entities.Persona1 vPersona1 = new Xpinn.FabricaCreditos.Entities.Persona1();
        vPersona1.cod_persona = Convert.ToInt64(Session["Cod_persona"].ToString());
        cliente.cod_persona = vPersona1.cod_persona;

        LstInformacionFinanciera = informacionfinancieraServicio.ListarProductosProcesoRepo(cliente, (Usuario)Session["usuario"]);
        System.Data.DataTable table = new System.Data.DataTable();
        table.Columns.Add("Cantidad");
        table.Columns.Add("Producto");
        table.Columns.Add("Porcentaje");
        table.Columns.Add("ValorUnitario");
        table.Columns.Add("Total");
        foreach (ProductosProceso vinformacionfinancieraServicio in LstInformacionFinanciera)
        {

            DataRow datarw;
            datarw = table.NewRow();
            datarw[0] = vinformacionfinancieraServicio.cantidad;
            datarw[1] = vinformacionfinancieraServicio.producto;
            datarw[2] = vinformacionfinancieraServicio.porcpd;
            datarw[3] = vinformacionfinancieraServicio.valunitario.ToString("0,0", CultureInfo.InvariantCulture);
            datarw[4] = vinformacionfinancieraServicio.valortotal.ToString();
            table.Rows.Add(datarw);
        }
        return table;
    }

    public DataTable CrearDataTableProductosTerminados()
    {
        Xpinn.FabricaCreditos.Services.ProductosTerminadosService informacionfinancieraServicio = new Xpinn.FabricaCreditos.Services.ProductosTerminadosService();
        List<ProductosTerminados> LstInformacionFinanciera = new List<ProductosTerminados>();
        ProductosTerminados cliente = new ProductosTerminados();
        Xpinn.FabricaCreditos.Entities.Persona1 vPersona1 = new Xpinn.FabricaCreditos.Entities.Persona1();
        vPersona1.cod_persona = Convert.ToInt64(Session["Cod_persona"].ToString());
        cliente.cod_persona = vPersona1.cod_persona;

        LstInformacionFinanciera = informacionfinancieraServicio.ListarProductosTerminadosRepo(cliente, (Usuario)Session["usuario"]);
        System.Data.DataTable table = new System.Data.DataTable();
        table.Columns.Add("Cantidad");
        table.Columns.Add("Producto");     
        table.Columns.Add("ValorUnitario");
        table.Columns.Add("Total");
        foreach (ProductosTerminados vinformacionfinancieraServicio in LstInformacionFinanciera)
        {

            DataRow datarw;
            datarw = table.NewRow();
            datarw[0] = vinformacionfinancieraServicio.cantidad;
            datarw[1] = vinformacionfinancieraServicio.producto;
            datarw[2] = vinformacionfinancieraServicio.vrunitario.ToString("0,0", CultureInfo.InvariantCulture);
            datarw[3] = vinformacionfinancieraServicio.vrtotal.ToString();
            table.Rows.Add(datarw);
        }
        return table;
    }

    public DataTable CrearDataTableRelacionDocumentos()
    {
        Xpinn.FabricaCreditos.Services.DatosPlanPagosService informacionfinancieraServicio = new Xpinn.FabricaCreditos.Services.DatosPlanPagosService();
        List<DatosPlanPagos> LstInformacionFinanciera = new List<DatosPlanPagos>();
        DatosPlanPagos cliente = new DatosPlanPagos();
        System.Data.DataTable table = new System.Data.DataTable();
        table.Columns.Add("Documentos");
        table.Columns.Add("Solicitante");
        table.Columns.Add("Codeudor");
        table.Rows.Add("Solicitud de crédito");
        table.Rows.Add(" * Datos básicos y demográficos del solicitante");
        table.Rows.Add(" * Autorización de consultas a las centrales de Riesgo");
        table.Rows.Add(" * Información Comercial y financiera del negocio del solicitante");
        table.Rows.Add(" * Información del Codeudor");
        table.Rows.Add(" * Concepto de Aprobación");
        table.Rows.Add("Reporte de consulta a las centrales de riesgo");
        table.Rows.Add("Reporte de confirmación de referencias");
        table.Rows.Add("Fotocopia cédula de ciudadanía (150%)");
        table.Rows.Add("Documentos SOporte del negocio(camara de comercio rut y facturas)");
        table.Rows.Add("Fotos(3 domicilio y 3 negocio)");
        table.Rows.Add("Recibos de servicios Públicos(2)");
        table.Rows.Add("Impuesto Predial");
        table.Rows.Add("Certificado de libertad y tradición");
        table.Rows.Add("Certificación Laboral");
        table.Rows.Add("Recibos de Nómina/Certificados de ingresos y retenciones)");
        table.Rows.Add("Póliza Adicional(Opcional)");
        table.Rows.Add("Anexo 1 Garantias Comunitarias(Opcional)");
        table.Rows.Add("Otros");

        return table;
    }

    public DataTable CrearDataTableRelacionDocDesembolso()
    {
        Xpinn.FabricaCreditos.Services.DatosPlanPagosService informacionfinancieraServicio = new Xpinn.FabricaCreditos.Services.DatosPlanPagosService();
        List<DatosPlanPagos> LstInformacionFinanciera = new List<DatosPlanPagos>();
        DatosPlanPagos cliente = new DatosPlanPagos();
        System.Data.DataTable table = new System.Data.DataTable();

        table.Columns.Add("Documentos");
        table.Columns.Add("Solicitante");
        table.Columns.Add("Codeudor");

        table.Rows.Add("Comprobante de egreso");
        table.Rows.Add("Plan de pagos");       
        table.Rows.Add("Compromiso de actualización de datos");
        table.Rows.Add("Seguro de vida deudores(obligatorio)");     

        return table;
    }

    public DataTable CrearDataTableReldocControltiempo()
    {
        Xpinn.FabricaCreditos.Services.DatosPlanPagosService informacionfinancieraServicio = new Xpinn.FabricaCreditos.Services.DatosPlanPagosService();
        List<DatosPlanPagos> LstInformacionFinanciera = new List<DatosPlanPagos>();
        DatosPlanPagos cliente = new DatosPlanPagos();
        System.Data.DataTable table = new System.Data.DataTable();
        table.Columns.Add("Documentos");
        table.Columns.Add("Solicitante");
        table.Columns.Add("Codeudor");
        table.Rows.Add("Fecha de solicitud de crédito");
        table.Rows.Add("Consulta en las centrales de Riesgo");
        table.Rows.Add("Levantamiento y análisis de la información");
        table.Rows.Add("Comité de Crédito");
        table.Rows.Add("Notificación al cliente");
        table.Rows.Add("Creación del cliente");
        table.Rows.Add("Radicación de crédito");
        table.Rows.Add("Desembolso");
        table.Rows.Add("           TOTAL DIAS     ");

        return table;
    }

    public DataTable CrearDataTableBienesUnidadFamCode()
    {
        Xpinn.FabricaCreditos.Services.BienesRaicesService informacionfinancieraServicio = new Xpinn.FabricaCreditos.Services.BienesRaicesService();
        List<BienesRaices> LstInformacioncodeudor = new List<BienesRaices>();
        BienesRaices cliente = new BienesRaices();  
        cliente.cod_persona = Convert.ToInt64(Session["Codeudores"].ToString());

        LstInformacioncodeudor = informacionfinancieraServicio.ListarBienesRaicesRepo(cliente, (Usuario)Session["usuario"]);
        System.Data.DataTable table = new System.Data.DataTable();
        table.Columns.Add("Tipo");
        table.Columns.Add("MatriculaInmobiliaria");
        table.Columns.Add("ValorComercial");
        table.Columns.Add("ValorHipoteca");
        foreach (BienesRaices vinformacionfinancieraServicio in LstInformacioncodeudor)
        {

            DataRow datarw;
            datarw = table.NewRow();
            datarw[0] = vinformacionfinancieraServicio.tipo;
            datarw[1] = vinformacionfinancieraServicio.matricula;
            datarw[2] = vinformacionfinancieraServicio.valorcomercial.ToString("0,0", CultureInfo.InvariantCulture);
            datarw[3] = vinformacionfinancieraServicio.valorhipoteca.ToString("0,0", CultureInfo.InvariantCulture);
            table.Rows.Add(datarw);
        }
        return table;
    }

    public DataTable CrearDataTableVehiculosCode()
    {
        Xpinn.FabricaCreditos.Services.VehiculosService informacionfinancieraServicio = new Xpinn.FabricaCreditos.Services.VehiculosService();
        List<Vehiculos> LstInformacioncodeudor = new List<Vehiculos>();
        Vehiculos cliente = new Vehiculos();
        cliente.cod_persona = Convert.ToInt64(Session["Codeudores"].ToString());

        LstInformacioncodeudor = informacionfinancieraServicio.ListarVehiculosRepo(cliente, (Usuario)Session["usuario"]);
        System.Data.DataTable table = new System.Data.DataTable();
        table.Columns.Add("Marca");
        table.Columns.Add("Placa");
        table.Columns.Add("Modelo");
        table.Columns.Add("ValorComercial");
        table.Columns.Add("ValorPrenda");
        foreach (Vehiculos vinformacionfinancieraServicio in LstInformacioncodeudor)
        {

            DataRow datarw;
            datarw = table.NewRow();
            datarw[0] = vinformacionfinancieraServicio.marca;
            datarw[1] = vinformacionfinancieraServicio.placa;
            datarw[2] = vinformacionfinancieraServicio.modelo;
            datarw[3] = vinformacionfinancieraServicio.valorcomercial.ToString("0,0", CultureInfo.InvariantCulture);
            datarw[4] = vinformacionfinancieraServicio.valorprenda.ToString("0,0", CultureInfo.InvariantCulture);
            table.Rows.Add(datarw);
        }
        return table;
    }

    public DataTable CrearDataTablePresupuestoEmpresarialCode()
    {
        Xpinn.FabricaCreditos.Services.PresupuestoEmpresarialService informacionfinancieraServicio = new Xpinn.FabricaCreditos.Services.PresupuestoEmpresarialService();
        List<PresupuestoEmpresarial> LstInformacioncodeudor = new List<PresupuestoEmpresarial>();
        PresupuestoEmpresarial cliente = new PresupuestoEmpresarial();
        cliente.cod_persona = Convert.ToInt64(Session["Codeudores"].ToString());

        LstInformacioncodeudor = informacionfinancieraServicio.ListarPresupuestoEmpresarialREPO(cliente, (Usuario)Session["usuario"]);
        System.Data.DataTable table = new System.Data.DataTable();
        table.Columns.Add("TotalActivo");
        table.Columns.Add("TotalPasivo");
        table.Columns.Add("TotalPatrimonio");
        table.Columns.Add("VentaMensual");
        table.Columns.Add("CostoTotal");
        table.Columns.Add("Utilidad");
        foreach (PresupuestoEmpresarial vinformacionfinancieraServicio in LstInformacioncodeudor)
        {

            DataRow datarw;
            datarw = table.NewRow();
            datarw[0] = vinformacionfinancieraServicio.totalactivo.ToString("0,0", CultureInfo.InvariantCulture);
            datarw[1] = vinformacionfinancieraServicio.totalpasivo.ToString("0,0", CultureInfo.InvariantCulture);
            datarw[2] = vinformacionfinancieraServicio.totalpatrimonio.ToString("0,0", CultureInfo.InvariantCulture);
            datarw[3] = vinformacionfinancieraServicio.ventamensual.ToString("0,0", CultureInfo.InvariantCulture);
            datarw[4] = vinformacionfinancieraServicio.costototal.ToString("0,0", CultureInfo.InvariantCulture);
            datarw[5] = vinformacionfinancieraServicio.utilidad.ToString("0,0", CultureInfo.InvariantCulture);
            table.Rows.Add(datarw);
        }
        return table;
    }

    public DataTable CrearDataTablePresupuestoFamiliarCode()
    {
        Xpinn.FabricaCreditos.Services.PresupuestoFamiliarService informacionfinancieraServicio = new Xpinn.FabricaCreditos.Services.PresupuestoFamiliarService();
        List<PresupuestoFamiliar> LstInformacioncodeudor = new List<PresupuestoFamiliar>();
        PresupuestoFamiliar cliente = new PresupuestoFamiliar();
        cliente.cod_persona = Convert.ToInt64(Session["Codeudores"].ToString());

        LstInformacioncodeudor = informacionfinancieraServicio.ListarPresupuestoFamiliarRepo(cliente, (Usuario)Session["usuario"]);
        System.Data.DataTable table = new System.Data.DataTable();
        table.Columns.Add("ActividadPrincipal");
        table.Columns.Add("Conyuge");
        table.Columns.Add("OtrosIngresos");
        table.Columns.Add("ConsumoFamiliar");
        table.Columns.Add("ObligacionesOCuotas");
        table.Columns.Add("Excedente");
       
        foreach (PresupuestoFamiliar vinformacionfinancieraServicio in LstInformacioncodeudor)
        {

            DataRow datarw;
            datarw = table.NewRow();
            datarw[0] = vinformacionfinancieraServicio.actividadprincipal.ToString("0,0", CultureInfo.InvariantCulture);
            datarw[1] = vinformacionfinancieraServicio.conyuge.ToString("0,0", CultureInfo.InvariantCulture);
            datarw[2] = vinformacionfinancieraServicio.otrosingresos.ToString("0,0", CultureInfo.InvariantCulture);
            datarw[3] = vinformacionfinancieraServicio.consumofamiliar.ToString("0,0", CultureInfo.InvariantCulture);
            datarw[4] = vinformacionfinancieraServicio.obligaciones.ToString("0,0", CultureInfo.InvariantCulture);
            datarw[5] = vinformacionfinancieraServicio.excedente.ToString("0,0", CultureInfo.InvariantCulture);
            table.Rows.Add(datarw);
        }
        return table;
    }

    public DataTable CrearDataTableReferenciasCodeudor()
    {
        Xpinn.FabricaCreditos.Services.RefernciasService informacionfinancieraServicio = new Xpinn.FabricaCreditos.Services.RefernciasService();
        List<Referncias> LstInformacioncodeudor = new List<Referncias>();
        Referncias cliente = new Referncias();
        cliente.cod_persona = Convert.ToInt64(Session["Codeudores"].ToString());

        LstInformacioncodeudor = informacionfinancieraServicio.ListarReferenciasRepo(cliente, (Usuario)Session["usuario"]);
        System.Data.DataTable table = new System.Data.DataTable();
        table.Columns.Add("nombres");
        table.Columns.Add("tiporeferencia");
        table.Columns.Add("telefonoref");
        table.Columns.Add("direccionref");
      
        foreach (Referncias vinformacionfinancieraServicio in LstInformacioncodeudor)
        {

            DataRow datarw;
            datarw = table.NewRow();
            datarw[0] = vinformacionfinancieraServicio.nombres;
            datarw[1] = vinformacionfinancieraServicio.descripcion;
            datarw[2] = vinformacionfinancieraServicio.telefono;
            datarw[3] = vinformacionfinancieraServicio.direccion;
            table.Rows.Add(datarw);
        }
        return table;
    }

    public DataTable CrearDataTableViabilidadFinanciera()
    {
        Xpinn.FabricaCreditos.Services.ViabilidadFinancieraService informacionviabilidad = new Xpinn.FabricaCreditos.Services.ViabilidadFinancieraService();
        List<ViabilidadFinanciera> Lstviabilidad= new List<ViabilidadFinanciera>();
        ViabilidadFinanciera cliente = new ViabilidadFinanciera();
        Xpinn.FabricaCreditos.Entities.Persona1 vPersona1 = new Xpinn.FabricaCreditos.Entities.Persona1();
        vPersona1.cod_persona = Convert.ToInt64(Session["Cod_persona"].ToString());
        cliente.cod_persona = vPersona1.cod_persona;
        Lstviabilidad = informacionviabilidad.ListarViabilidadFinancieraRepo(cliente, (Usuario)Session["usuario"]);
        System.Data.DataTable table = new System.Data.DataTable();
        table.Columns.Add("PruebaAcida");
        table.Columns.Add("EndeudamientoTotal");
        table.Columns.Add("RotacionCuentasXCobrar");
        table.Columns.Add("GastosFamiliares");
        table.Columns.Add("RotacionCuentasXPagar");
        table.Columns.Add("RotacionCapitalTrabajo");
        table.Columns.Add("RotacionInventarios");  
        table.Columns.Add("PuntoEquilibrio");
        table.Columns.Add("EF");

        foreach (ViabilidadFinanciera vinformacionviabilidad in Lstviabilidad)
        {

            DataRow datarw;
            datarw = table.NewRow();
            datarw[0] = vinformacionviabilidad.prueba;
            datarw[1] = vinformacionviabilidad.endeudamiento;
            datarw[2] = vinformacionviabilidad.rotacioncuentas;
            datarw[3] = vinformacionviabilidad.gastos;
            datarw[4] = vinformacionviabilidad.rotacioncuentaspagar;
            datarw[5] = vinformacionviabilidad.rotacioncapital;      
            datarw[6] = vinformacionviabilidad.rotacioninventarios;
            datarw[7] = vinformacionviabilidad.puntoequilibrio;      
            datarw[8] = vinformacionviabilidad.ef;
            table.Rows.Add(datarw);
        }
        return table;
    }

    protected void btnInforme4_Click(object sender, EventArgs e)
    {
        mvLista.ActiveViewIndex = 0;
        mvLista0.ActiveViewIndex = 0;
    }
}