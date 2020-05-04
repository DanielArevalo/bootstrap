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
using Xpinn.FabricaCreditos.Services;
using Xpinn.FabricaCreditos.Entities;
using Xpinn.Asesores.Services;
using Xpinn.Asesores.Entities;
using System.Globalization;
using System.IO;
using GenCode128;
using Fath;
using System.Drawing;
using System.Drawing.Imaging;


using Microsoft.Reporting.WebForms;
using System.Net.Mail;
using System.Net.Mime;



public partial class Page_Asesores_Colocacion_Lista : GlobalWeb
{

    ClienteExtractoService clienteExtractoServicio = new ClienteExtractoService();
    ExcelService excelServicio = new ExcelService();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(clienteExtractoServicio.CodigoPrograma, "L");
            Site toolBar = (Site)this.Master;

            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
        }        
        catch (Exception ex)
        {
            BOexcepcion.Throw(clienteExtractoServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                mvLista.ActiveViewIndex = 0;
                pGrilla.Visible = false;
                CargaDropDown();                
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(clienteExtractoServicio.CodigoPrograma, "Page_Load", ex);
        }
    }

    protected void CargaDropDown()
    {
        PoblarLista("Ciudades", ddlCiudad);

        MotivoGeneracionExtractoService motivoGeneracionExtractoService = new MotivoGeneracionExtractoService();
        MotivoGeneracionExtracto Motivos = new MotivoGeneracionExtracto();
        ddlMotivoGeneracionExtracto.DataSource = motivoGeneracionExtractoService.ListarMotivoGeneracionExtractos(Motivos, (Usuario)Session["usuario"]);
        ddlMotivoGeneracionExtracto.DataTextField = "Nombre";
        ddlMotivoGeneracionExtracto.DataValueField = "Codigo";
        ddlMotivoGeneracionExtracto.DataBind();
        ddlMotivoGeneracionExtracto.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));
    }

    public void PoblarLista(string pTabla, DropDownList ddlControl)
    {
        PoblarLista(pTabla, "", "", "", ddlControl);
    }

    public void PoblarLista(string pTabla, string pColumnas, string pCondicion, string pOrden, DropDownList ddlControl)
    {
        List<Xpinn.Comun.Entities.ListaDesplegable> plista = new List<Xpinn.Comun.Entities.ListaDesplegable>();
        Xpinn.Comun.Entities.ListaDesplegable pentidad = new Xpinn.Comun.Entities.ListaDesplegable();
        Xpinn.Comun.Services.ListaDeplegableService pservicio = new Xpinn.Comun.Services.ListaDeplegableService();

        ddlControl.Items.Clear();
        plista = pservicio.ListarListaDesplegable(pentidad, pTabla, pColumnas, pCondicion, pOrden, (Usuario)Session["usuario"]);
        pentidad.idconsecutivo = null;
        pentidad.descripcion = "Seleccione un item";
        plista.Insert(0, pentidad);
        ddlControl.DataTextField = "descripcion";
        ddlControl.DataValueField = "idconsecutivo";
        ddlControl.DataSource = plista;
        ddlControl.DataBind();
    }


    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {       
        //add
        String filtro;
        List<ClienteExtracto> lstClientesExtracto = new List<ClienteExtracto>();
        filtro = obtFiltro();

        clienteExtractoServicio.GenerarExtractoClientes(filtro, (Usuario)Session["usuario"]);

        //

        Actualizar();
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        LimpiarControles();
    }


    protected void LimpiarControles()
    {
        btnGenerarExtracto.Visible = false;
        mvLista.Visible = false;
        mvLista.ActiveViewIndex = 0;    
        txtObservaciones.Text = "";
        ddlMotivoGeneracionExtracto.SelectedIndex = 0;
        lblResultado.Text = "";
        lblTotalRegs.Text = "";
        lblCreditoSeleccionado.Text = "";
        rdbEnvioPor.SelectedIndex = 0;
        gvListaClientesExtracto.DataSource = null;
        gvListaClientesExtracto.DataBind();
    }


    String sTipo = "EAN128";
    protected void btnGenerarExtracto_Click(object sender, EventArgs e)
    {
        //vCreditoSeleccionado codigo seleccionado NUMERO CREDITO

        if (ValidarDatos())
        {
            List<ResumenExtractos> lstResumenGeneral = new List<ResumenExtractos>();
            List<ComprobantePagoExtracto> lstComprobanteGeneral = new List<ComprobantePagoExtracto>();
            

            //tabla general 
            DataTable tablegeneral = new DataTable();
            tablegeneral.Columns.Add("NumeroRadicacion");
            tablegeneral.Columns.Add("CodPersona");
            tablegeneral.Columns.Add("Identificacion");
            tablegeneral.Columns.Add("Nombre");
            tablegeneral.Columns.Add("Linea");
            tablegeneral.Columns.Add("SALDO_INICIAL");
            tablegeneral.Columns.Add("SALDO_FINAL");
            tablegeneral.Columns.Add("Oficina");
            tablegeneral.Columns.Add("FechaInicial");
            tablegeneral.Columns.Add("FechaFinal");
            tablegeneral.Columns.Add("Direccion");
            tablegeneral.Columns.Add("Barrio");
            tablegeneral.Columns.Add("Ciudad");
            tablegeneral.Columns.Add("Asesor");
            tablegeneral.Columns.Add("numpagare");
            tablegeneral.Columns.Add("nomperiodicidad");
            tablegeneral.Columns.Add("estadocre");
            tablegeneral.Columns.Add("valorcuota");
            tablegeneral.Columns.Add("fec_proximo_pago");
            tablegeneral.Columns.Add("Imagen_barras");
            tablegeneral.Columns.Add("cod_barras");
            tablegeneral.Columns.Add("plazo");
            tablegeneral.Columns.Add("vrcapital");
            tablegeneral.Columns.Add("intcorriente");
            tablegeneral.Columns.Add("seguro");
            tablegeneral.Columns.Add("intmora");
            tablegeneral.Columns.Add("tasa");
            tablegeneral.Columns.Add("total");
            tablegeneral.Columns.Add("bancos");

            ResumenExtractosService resumenExtractosService = new ResumenExtractosService();
            ResumenExtractos ResumenExtracto = new ResumenExtractos();

            foreach (GridViewRow rFila in gvListaClientesExtracto.Rows)
            {
                CheckBox cbSeleccionar = (CheckBox)rFila.FindControl("cbSeleccionar");
                if (cbSeleccionar != null)
                {
                    if (cbSeleccionar.Checked == true)
                    {
                        Int64 vCreditoSeleccionado = Convert.ToInt64(rFila.Cells[1].Text);//NUMERO DE CREDITO Seleccionado

                        lblResultado.Text = "";

                        #region Trayendo datos desde BD para el informe

                        List<ResumenExtractos> lstResumenExtractos = new List<ResumenExtractos>();
                        List<ResumenExtractos> lstTemporal = new List<ResumenExtractos>();
                        List<ResumenExtractos> lstBancos = new List<ResumenExtractos>();
                        //Asignando parametros                        
                        ResumenExtracto.NumeroRadicacion = vCreditoSeleccionado;
                        ResumenExtracto.FechaInicial = txtfechaIni.ToDateTime;
                        ResumenExtracto.FechaFinal = txtfechaFin.ToDateTime;
                        
                        lstResumenExtractos = resumenExtractosService.GeneraryListarResumen(ResumenExtracto, (Usuario)Session["usuario"]);
                        string codlinea = "";
                        string[] sdata ;
                        if (lstResumenExtractos[0].Linea != "")
                        {
                            sdata = lstResumenExtractos[0].Linea.ToString().Split('-');
                            codlinea = sdata[0].ToString();
                        }
                        lstBancos = resumenExtractosService.ListarBancos(codlinea, (Usuario)Session["usuario"]);

                        string bancos = "";
                        int cont = 0;
                        if (lstBancos.Count > 0)
                        {
                            foreach (ResumenExtractos rfila in lstBancos)
                            {
                                cont++;
                                if (cont == 3)
                                    cont = 1;

                                if (cont != 2)
                                    bancos = bancos + "*"+ rfila.nombrebanco + "      CTA CTE : ";
                                else
                                    bancos = bancos + rfila.nombrebanco + "<br/>";                                
                            }                            
                        }

                        lstTemporal = resumenExtractosService.GeneraryListarResumenDetalle(ResumenExtracto, (Usuario)Session["usuario"]);
                     
                        #endregion Trayendo datos desde BD para el informe


                        if (int.Parse(rdbEnvioPor.SelectedValue) == 1 && lstResumenExtractos[0].Email == null)
                        {
                            lblmsj.Text = "La persona no tiene un correo configurado.";
                            return;
                        }

                        //lstResumenExtractos[0].Email = "souliaq@gmail.com"; //Esta línea se debe desactivar en producción, ya que sobreescribe el correo original.
                        //Verificar si la persona tiene un correo configurado:

                        #region GenerarCodigoDeBarras

                        string cRutaDeImagen = System.IO.Path.GetTempPath() + Guid.NewGuid().ToString() + ".png";
                        
                        string CadenaCodigoDeBarras, CadenaCodigoDeBarrasSinParentes;
                        long identifi = Convert.ToInt64(lstResumenExtractos[0].Identificacion);
                        string fechaFin = Convert.ToDateTime(txtfechaFin.Text).ToString("yyyyMMdd");
                        decimal total = Convert.ToDecimal(lstTemporal[0].capital + lstTemporal[0].interes_cte + lstTemporal[0].seguro + lstTemporal[0].interes_mora );


                        CadenaCodigoDeBarras = "(415)7709998005730" + "(8020)" + lstResumenExtractos[0].NumeroRadicacion.ToString("00000000")
                            + ("8020") + identifi.ToString("0000000000")+"(3900)" + total.ToString("0000000000") + "(96)" + fechaFin;
                        CadenaCodigoDeBarrasSinParentes = "4157709998005730" + "8020" + lstResumenExtractos[0].NumeroRadicacion.ToString("00000000") +
                            "8020" + identifi.ToString("0000000000") + "3900" + total.ToString("0000000000") + "96" + fechaFin;
                        if (sTipo == "code128")
                        {
                            System.Drawing.Image imgCodBarras = Code128Rendering.MakeBarcodeImage(CadenaCodigoDeBarras, 1, true);
                            imgCodBarras.Save(cRutaDeImagen, System.Drawing.Imaging.ImageFormat.Png);
                            hdFileName.Value = identifi + ".png";
                            imgCodBarras.Save(Server.MapPath("Imagenes\\") + identifi + ".png", System.Drawing.Imaging.ImageFormat.Png);
                        }
                        if (sTipo == "EAN128")
                        {
                            // Generar código de barras
                            VerError("");
                            Fath.BarcodeX b = new Fath.BarcodeX();
                            b.Data = CadenaCodigoDeBarrasSinParentes;
                            b.Orientation = 0;
                            b.Symbology = Fath.bcType.EAN128;
                            b.ShowText = true;
                            b.Font = new System.Drawing.Font("Arial", 8);
                            b.BackColor = Color.White;
                            b.ForeColor = Color.Black;
                            int w = 420;
                            int h = 80;
                            try
                            {
                                System.Drawing.Image g = b.Image(w, h);
                                g.Save(Server.MapPath("Imagenes\\") + identifi + ".png", System.Drawing.Imaging.ImageFormat.Png);
                            }
                            catch (Exception ex)
                            {
                                VerError(ex.Message);
                            }
                            // Mostrar imagen con el código de barras
                            //imgCodigoBarras.ImageUrl = "bcx.aspx?data=" + CadenaCodigoDeBarrasSinParentes + "&identific=" + identifi;
                            //imgCodigoBarras.ImageUrl = "bcx.aspx?data=" + CadenaCodigoDeBarrasSinParentes + "&identific=" + identifi;
                            //imgCodigoBarras.ImageUrl = "Handler.ashx?data=" + CadenaCodigoDeBarrasSinParentes + "&identific=" + txtIdentific.Text;
                            hdFileName.Value = identifi + ".png";
                            cRutaDeImagen = Server.MapPath("Imagenes\\") + identifi + ".png";
                        }


                        #endregion GenerarCodigoDeBarras

                        //tabla por usuario
                        DataTable table = new DataTable();
                        table.Columns.Add("NumeroRadicacion0");
                        table.Columns.Add("CodPersona0");
                        table.Columns.Add("Identificacion0");
                        table.Columns.Add("Nombre0");
                        table.Columns.Add("Linea0");
                        table.Columns.Add("SALDO_INICIAL0");
                        table.Columns.Add("SALDO_FINAL0");
                        table.Columns.Add("Oficina0");
                        table.Columns.Add("FechaInicial0");
                        table.Columns.Add("FechaFinal0");
                        table.Columns.Add("Direccion0");
                        table.Columns.Add("Barrio0");
                        table.Columns.Add("Ciudad0");
                        table.Columns.Add("Asesor0");
                        table.Columns.Add("numpagare0");
                        table.Columns.Add("nomperiodicidad0");
                        table.Columns.Add("estadocre0");
                        table.Columns.Add("valorcuota0");
                        table.Columns.Add("fec_proximo_pago0");
                        table.Columns.Add("Imagen_barras0");
                        table.Columns.Add("cod_barras0");
                        table.Columns.Add("plazo0");
                        table.Columns.Add("vrcapital0");
                        table.Columns.Add("intcorriente0");
                        table.Columns.Add("seguro0");
                        table.Columns.Add("intmora0");
                        table.Columns.Add("tasa0");
                        table.Columns.Add("total0");
                        table.Columns.Add("bancos0");

                        foreach (ResumenExtractos fila in lstResumenExtractos)
                        {
                            DataRow datarw;
                            datarw = table.NewRow();
                            datarw[0] = fila.NumeroRadicacion;
                            datarw[1] = fila.CodPersona;
                            datarw[2] = fila.Identificacion;                            
                            datarw[3] = fila.Nombre;                            
                            datarw[4] = fila.Linea;
                            datarw[5] = fila.SaldoInicial.ToString("n");
                            datarw[6] = fila.SaldoFinal.ToString("n");
                            datarw[7] = fila.Oficina;
                            datarw[8] = txtfechaIni.Text;
                            datarw[9] = txtfechaFin.Text;
                            datarw[10] = fila.Direccion;
                            datarw[11] = fila.Barrio;
                            datarw[12] = fila.Ciudad;      
                            datarw[13] = fila.Asesor;
                            datarw[14] = fila.numpagare;
                            datarw[15] = fila.nomperiodicidad;
                            datarw[16] = fila.estadocre;
                            datarw[17] = fila.valor_cuota.ToString("n");
                            datarw[18] = fila.fec_proximo_pago.ToShortDateString();
                            datarw[19] = cRutaDeImagen;
                            datarw[20] = CadenaCodigoDeBarrasSinParentes;
                            datarw[21] = fila.plazo;
                            if (lstTemporal.Count > 0)
                            {
                                datarw[22] = lstTemporal[0].capital.ToString("c");
                                datarw[23] = lstTemporal[0].interes_cte.ToString("c");
                                datarw[24] = lstTemporal[0].seguro.ToString("c");
                                datarw[25] = lstTemporal[0].interes_mora.ToString("c");
                                datarw[26] = lstTemporal[0].tasa_interes;
                            }
                           
                            datarw[27] = total.ToString("n");
                            datarw[28] = bancos.ToString();
                            table.Rows.Add(datarw);
                        }
                        //LLena el datatable general
                        foreach (DataRow rData in table.Rows)
                        {
                            DataRow datarw;
                            datarw = tablegeneral.NewRow();
                            datarw[0] = rData[0].ToString();
                            datarw[1] = rData[1].ToString();
                            datarw[2] = rData[2].ToString();
                            datarw[3] = rData[3].ToString();
                            datarw[4] = rData[4].ToString();
                            datarw[5] = rData[5].ToString();
                            datarw[6] = rData[6].ToString();
                            datarw[7] = rData[7].ToString();
                            datarw[8] = rData[8].ToString();
                            datarw[9] = rData[9].ToString();
                            datarw[10] = rData[10].ToString();
                            datarw[11] = rData[11].ToString();
                            datarw[12] = rData[12].ToString();
                            datarw[13] = rData[13].ToString();
                            datarw[14] = rData[14].ToString();
                            datarw[15] = rData[15].ToString();
                            datarw[16] = rData[16].ToString();
                            datarw[17] = rData[17].ToString();
                            datarw[18] = rData[18].ToString();
                            datarw[19] = rData[19].ToString();
                            datarw[20] = rData[20].ToString();
                            datarw[21] = rData[21].ToString();
                            datarw[22] = rData[22].ToString();
                            datarw[23] = rData[23].ToString();
                            datarw[24] = rData[24].ToString();
                            datarw[25] = rData[25].ToString();
                            datarw[26] = rData[26].ToString();
                            datarw[27] = rData[27].ToString();
                            datarw[28] = rData[28].ToString();
                            tablegeneral.Rows.Add(datarw);
                        }

                        

                        if (int.Parse(rdbEnvioPor.SelectedValue) == 1) //Se ha solicitado enviar el archivo por correo:
                        {
                            //Escribir PDF del reporte para su envio por correo electrónico NO FUNCIONA ENVIO DE CORREO
                            #region EscribirPDF
                            byte[] bytes = rvExtracto.LocalReport.Render("PDF");
                            EnviarCorreoElectrónico(lstResumenExtractos[0].Email, new MemoryStream(bytes), "Extracto.pdf");
                            #endregion EscribirPDF
                            LimpiarControles();
                            lblResultado.Text = "Se ha enviado un correo a: " + lstResumenExtractos[0].Email;
                        }
                        else //Se ha solicitado mostrar el reporte en pantalla:
                        {
                            mvLista.ActiveViewIndex = 1;
                            lblResultado.Text = "";
                        }


                        RegistroGeneraciónExtracto registroGeneraciónExtracto = new RegistroGeneraciónExtracto();

                        registroGeneraciónExtracto.pidextracto = 0;
                        registroGeneraciónExtracto.pfecha_generacion = DateTime.Now;
                        if (lstResumenExtractos[0].FechaCorte != DateTime.MinValue && lstResumenExtractos[0].FechaCorte != null)
                            registroGeneraciónExtracto.pfechacorte = lstResumenExtractos[0].FechaCorte;
                        else
                            registroGeneraciónExtracto.pfechacorte = DateTime.MinValue;
                        registroGeneraciónExtracto.pcod_persona = lstResumenExtractos[0].CodPersona;
                        registroGeneraciónExtracto.pnumero_radicacion = vCreditoSeleccionado;
                        registroGeneraciónExtracto.pusuario = ((Usuario)Session["usuario"]).identificacion.ToString();
                        registroGeneraciónExtracto.pcodmotivo = int.Parse(ddlMotivoGeneracionExtracto.SelectedValue);
                        registroGeneraciónExtracto.pobservaciones = txtObservaciones.Text;
                        RegistroGeneraciónExtractoService registroGeneraciónExtractoService = new RegistroGeneraciónExtractoService();
                        registroGeneraciónExtractoService.AlmacenarRegistroGeneraciónExtractos(registroGeneraciónExtracto, (Usuario)Session["usuario"]);

                        Site toolBar = (Site)Master;
                        toolBar.MostrarLimpiar(false);
                        toolBar.MostrarConsultar(false);
                    }
                }
            }

            ReportParameter[] param = new ReportParameter[3];
            param[0] = new ReportParameter("FechaInicial", Convert.ToDateTime(txtfechaIni.Text).ToShortDateString());
            param[1] = new ReportParameter("FechaFinal", Convert.ToDateTime(txtfechaFin.Text).ToShortDateString());
            param[2] = new ReportParameter("ImagenReport", ImagenReporte());
            rvExtracto.LocalReport.EnableExternalImages = true;
            rvExtracto.LocalReport.SetParameters(param);

            //rvExtracto.LocalReport.DataSources.Clear();
            ReportDataSource rds1 = new ReportDataSource("DataSet1", tablegeneral);
            rvExtracto.LocalReport.DataSources.Add(rds1);
            
            rvExtracto.LocalReport.Refresh();
        }

       
    }

    
    private string obtFiltro()
    {
        String filtro = String.Empty;
        if (txtNumCredito.Text != "")
            filtro += " and C.numero_radicacion = " + txtNumCredito.Text;
        if (txtEmpresa.Text != "")
            filtro += " and P.Empresa like '%"+ txtEmpresa.Text + "%'";        
        if (ddlCiudad.SelectedIndex != 0)
            filtro += " and P.Codciudadresidencia = " + ddlCiudad.SelectedValue;
        if (txtCodigo.Text != "")
            filtro += " and C.COD_DEUDOR = " + txtCodigo.Text;
        if (txtidentificacion.Text != "")
            filtro += " and P.identificacion = '" +txtidentificacion.Text+ "'";
        if (txtNombre.Text != "")
            filtro += " and P.nombre like '%"+ txtNombre.Text.ToUpper() +"%'";
        return filtro;
    }



    private void Actualizar()
    {
        try
        {
            String emptyQuery = "Fila de datos vacia";
            
            String filtro;            
            filtro = obtFiltro();

            List<ClienteExtracto> lstClientesExtracto = new List<ClienteExtracto>();
            //ADD

            lstClientesExtracto = clienteExtractoServicio.ListarExtractoCliente(filtro, (Usuario)Session["usuario"]);
            //
            gvListaClientesExtracto.EmptyDataText = emptyQuery;
            gvListaClientesExtracto.DataSource = lstClientesExtracto;
            if (lstClientesExtracto.Count > 0)
            {
                pGrilla.Visible = true;
                lblTotalRegs.Visible = true;
                btnGenerarExtracto.Visible = true;                
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstClientesExtracto.Count.ToString();
                gvListaClientesExtracto.DataBind();
                ValidarPermisosGrilla(gvListaClientesExtracto);
            }
            else
            {
                btnGenerarExtracto.Visible = false;
                pGrilla.Visible = false;
                lblTotalRegs.Visible = false;
            }

            Session.Add(clienteExtractoServicio.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(clienteExtractoServicio.CodigoPrograma, "Actualizar", ex);
        }
    }


   

    void EnviarCorreoElectrónico(string CorreoDeDestino, MemoryStream ArchivoAnexo,string AliasDeArchivoAnexo)
    {
        List<EnvioDeCorreoExtracto> lstEnvioDeCorreoExtracto = new List<EnvioDeCorreoExtracto>();
        EnvioDeCorreoExtractoService envioDeCorreoExtractoService = new EnvioDeCorreoExtractoService();
        Xpinn.Asesores.Entities.EnvioDeCorreoExtracto EnvioDeCorreoExtracto = new Xpinn.Asesores.Entities.EnvioDeCorreoExtracto();
        lstEnvioDeCorreoExtracto = envioDeCorreoExtractoService.ListarEnvioDeCorreoExtractos(EnvioDeCorreoExtracto, (Usuario)Session["usuario"]);

        //MailMessage mail = new MailMessage(lstEnvioDeCorreoExtracto[0].NombreCorreoOrigen, CorreoDeDestino);
        MailMessage mail = new MailMessage();
        mail.From = new System.Net.Mail.MailAddress(lstEnvioDeCorreoExtracto[0].NombreCorreoOrigen);
        mail.To.Add(CorreoDeDestino);
        SmtpClient client = new SmtpClient();
        
        client.Port = lstEnvioDeCorreoExtracto[0].PuertoDeServidorSMTP;
        client.EnableSsl = lstEnvioDeCorreoExtracto[0].UsarSSL;
        client.DeliveryMethod = SmtpDeliveryMethod.Network;
        client.Host = lstEnvioDeCorreoExtracto[0].ServidorSMTP;
        //client.UseDefaultCredentials = false;
        client.Credentials = new System.Net.NetworkCredential(lstEnvioDeCorreoExtracto[0].Usuario, lstEnvioDeCorreoExtracto[0].Clave);        
        mail.Subject = lstEnvioDeCorreoExtracto[0].TextoDelAsunto;
        mail.Body = lstEnvioDeCorreoExtracto[0].TextoDelMensaje;
        //mail.Attachments.Add(new Attachment(ArchivoAnexo, AliasDeArchivoAnexo, MediaTypeNames.Application.Octet));
        client.Send(mail);
    }

   
    Boolean ValidarDatos()
    {      
        lblmsj.Text = "";

        if (ddlMotivoGeneracionExtracto.SelectedIndex == 0)
        {
            lblmsj.Text = "No ha seleccionado un motivo de generación de extracto";
            return false;
        }
        if (txtObservaciones.Text == "")
        {
            lblmsj.Text = "Ingrese las observaciones";
            return false;
        }
        if (txtfechaIni.Text == "")
        {
            lblmsj.Text = "Ingrese la Fecha Inicial";
            return false;
        }
        if (txtfechaFin.Text == "")
        {
            lblmsj.Text = "Ingrese la fecha final";
            return false;
        }
        if (Convert.ToDateTime(txtfechaIni.Text) > Convert.ToDateTime(txtfechaFin.Text))
        {
            lblmsj.Text = "Rango de Fechas ingresadas de forma erronea";
            return false;
        }

        return true;
    }


    protected void cbSeleccionarEncabezado_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox cbSeleccionarEncabezado = (CheckBox)sender;
        if (cbSeleccionarEncabezado != null)
        {
            foreach (GridViewRow rFila in gvListaClientesExtracto.Rows)
            {
                CheckBox cbSeleccionar = (CheckBox)rFila.FindControl("cbSeleccionar");
                cbSeleccionar.Checked = cbSeleccionarEncabezado.Checked;
            }
        }
    }


    protected void gvListaClientesExtracto_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvListaClientesExtracto.PageIndex = e.NewPageIndex;
            Actualizar();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(clienteExtractoServicio.CodigoPrograma, "gvLista_PageIndexChanging", ex);
        }
    }


    protected void btnVerData_Click(object sender, EventArgs e)
    {   
        Site toolBar = (Site)Master;
        toolBar.MostrarLimpiar(true);
        toolBar.MostrarConsultar(true);
        mvLista.ActiveViewIndex = 0;
    }
}