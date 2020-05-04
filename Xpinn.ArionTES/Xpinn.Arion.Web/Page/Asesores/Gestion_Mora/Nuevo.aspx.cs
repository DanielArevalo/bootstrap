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
using Microsoft.Reporting.WebForms;
using System.IO;

partial class Nuevo : GlobalWeb
{
    private Xpinn.FabricaCreditos.Services.DestinacionService destinServicio = new Xpinn.FabricaCreditos.Services.DestinacionService();
    private Xpinn.Asesores.Services.EstadoCuentaService serviceEstadoCuenta = new Xpinn.Asesores.Services.EstadoCuentaService();
    private Xpinn.Asesores.Services.TiposDocCobranzasServices tipoDocumentoServicio = new Xpinn.Asesores.Services.TiposDocCobranzasServices();
    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            VisualizarOpciones("110305", "");

            Site toolBar = (Site)this.Master;
            toolBar.eventoImprimir += btnExportar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
            toolBar.eventoConsultar += btnConsultar_Click;

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(destinServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                mvPrincipal.ActiveViewIndex = 0;
                ucFechaCorte.Text = DateTime.Now.ToShortDateString();
                idObjeto = Session[destinServicio.CodigoPrograma + ".id"].ToString();
                if (idObjeto != null || idObjeto != "")
                {
                    ObtenerDatos(idObjeto);
                }
                else
                {
                    VerError("No cargo el Asociado a generar la documentación Pertinente");
                }
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(destinServicio.CodigoPrograma, "Page_Load", ex);
        }
    }
    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Site toolBar = (Site)Master;
        if (mvPrincipal.ActiveViewIndex == 0)
        {
            Session.Remove("Persona");
            Session.Remove("DatosGrilla");
            Navegar(Pagina.Lista);
        }
        else if (mvPrincipal.ActiveViewIndex == 2)
        {
            mvPrincipal.ActiveViewIndex = 0;
            toolBar.MostrarCancelar(true);
            toolBar.MostrarConsultar(true);
            toolBar.MostrarImprimir(true);
        }



    }
    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            string filtro_productos = ddltipoProducto.SelectedValue.ToString();
            string pfechacorte = Convert.ToString(ucFechaCorte.Text);
            Xpinn.Asesores.Entities.Persona persona = new Xpinn.Asesores.Entities.Persona();
            persona = serviceEstadoCuenta.ConsultarPersona(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);
            if (persona.IdPersona != 0)
            {
                if (persona.IdPersona != 0) { txtCodCliente.Text = persona.IdPersona.ToString(); } else { txtCodCliente.Text = ""; }
                if (persona.NumeroDocumento != null) { txtIdentiCliente.Text = persona.NumeroDocumento.ToString(); } else { txtIdentiCliente.Text = ""; }
                if (persona.PrimerNombre != "" && persona.PrimerApellido != "") { txtNombreCliente.Text = persona.PrimerNombre.ToString() + " " + persona.PrimerApellido.ToString(); } else { txtNombreCliente.Text = ""; }
                Session["Persona"] = persona;
            }
            Xpinn.Asesores.Services.CreditosService serviciosMoras = new Xpinn.Asesores.Services.CreditosService();
            List<Xpinn.Asesores.Entities.ProductosMora> lstProductosMora = new List<Xpinn.Asesores.Entities.ProductosMora>();
            lstProductosMora = null;
            lstProductosMora = serviciosMoras.ConsultarDetalleMoraPersona(Convert.ToString(pIdObjeto), "", Convert.ToDateTime(pfechacorte), filtro_productos, (Usuario)Session["usuario"]);

            gvLista.PageSize = pageSize;
            gvLista.EmptyDataText = emptyQuery;
            gvLista.DataSource = null;
            gvLista.DataSource = lstProductosMora;


            if (lstProductosMora.Count > 0)
            {

                Session["DatosGrilla"] = lstProductosMora;
                gvLista.Visible = true;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstProductosMora.Count.ToString();
                gvLista.DataBind();
                Totalizar(lstProductosMora);
            }
            else
            {
                Session.Remove("DatosGrilla");
                gvLista.Visible = false;
                lblTotalRegs.Visible = false;
                mvPrincipal.ActiveViewIndex = 0;
                Site toolBar = (Site)Master;
                toolBar.MostrarImprimir(false);
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(destinServicio.CodigoPrograma, "ObtenerDatos", ex);
        }
    }
    protected void ddltipoProducto_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddlTipoDocumento_SelectedIndexChanged(object sender, EventArgs e)
    {
        // ObtenerDatos(); 
    }
    protected void btnExportar_Click(object sender, EventArgs e)
    {
        if (ddlTipoDocumento.SelectedValue != "0")
        {
            GenerarDocumento();
        }
    }
    void GenerarDocumento()
    {

        Xpinn.Asesores.Entities.Persona persona = new Xpinn.Asesores.Entities.Persona();
        persona = (Xpinn.Asesores.Entities.Persona)Session["Persona"];

        List<Xpinn.Asesores.Entities.ProductosMora> lstConsulta = new List<Xpinn.Asesores.Entities.ProductosMora>();
        lstConsulta = (List<Xpinn.Asesores.Entities.ProductosMora>)Session["DatosGrilla"];

        Xpinn.Asesores.Entities.TiposDocCobranzas vTipoDoc = new Xpinn.Asesores.Entities.TiposDocCobranzas();
        vTipoDoc = tipoDocumentoServicio.ConsultarTiposDocumento(Convert.ToInt64(5), (Usuario)Session["usuario"]);

        string[] separadas;
        if (vTipoDoc.texto != null && vTipoDoc.texto != "")
        {
            separadas = vTipoDoc.texto.Split('-');
        }
        else
        {
            string msj = @"Nos permitimos informarle que la Cooperativa reportó para descuento por nomina a su pagaduria el valior correspondiente a las cuotas de sus obligaciones con corte a " + ucFechaCorte.Text.ToString() + " del cual se generó un saldo en NO DEDUCIDO por: - Es importante aclarar que este valor no incluye la cuota corriente del mes";
            separadas = msj.Split('-');
        }


        if (gvLista.Rows.Count > 0 && lstConsulta.Count > 0)
        {
            //CREACION DE LA TABLA
            System.Data.DataTable table = new System.Data.DataTable();
            table.Columns.Add("periodo");
            table.Columns.Add("obligacion");
            table.Columns.Add("descripcion");
            table.Columns.Add("fecvencimineto");
            table.Columns.Add("dias");
            table.Columns.Add("fp");
            table.Columns.Add("capital");
            table.Columns.Add("extras");
            table.Columns.Add("interes");
            table.Columns.Add("mora");
            table.Columns.Add("otros");
            table.Columns.Add("saldototal");

            //LLENAR LAS TABLAS CON LOS DATOS CORRESPONDIENTES

            foreach (Xpinn.Asesores.Entities.ProductosMora rFila in lstConsulta)
            {
                DataRow dr;
                dr = table.NewRow();
                dr[0] = " " + rFila.periodo;
                dr[1] = " " + rFila.numero_producto;
                dr[2] = " " + rFila.descripcion;
                dr[3] = " " + string.Format("{0:d}", rFila.fecha_vencimento);
                dr[4] = " " + rFila.dias;
                dr[5] = " " + rFila.forma_pago;
                dr[6] = " " + rFila.capital;
                dr[7] = " " + rFila.extras;
                dr[8] = " " + rFila.interes;
                dr[9] = " " + rFila.mora;
                dr[10] = " " + rFila.otros;
                dr[11] = " " + rFila.saldo_total;

                table.Rows.Add(dr);
            }

            //PASAR LOS DATOS AL REPORTE
            Usuario pUsuario = new Usuario();
            pUsuario = (Usuario)Session["Usuario"];

            // Determinar el logo de la empresa

            string cRutaDeImagen;
            cRutaDeImagen = Server.MapPath("~/Images\\") + "LogoEmpresa.jpg";


            ReportParameter[] param = new ReportParameter[17];
            param[0] = new ReportParameter("Entidad", pUsuario.empresa);
            param[1] = new ReportParameter("nit", pUsuario.nitempresa);
            param[2] = new ReportParameter("tel_entidad", pUsuario.telefono);
            param[3] = new ReportParameter("direccion_entidad", pUsuario.direccion);
            param[4] = new ReportParameter("fechahora", DateTime.Now.ToString());
            param[5] = new ReportParameter("nombre", persona.PrimerNombre.ToString() + " " + persona.PrimerApellido.ToString());
            param[6] = new ReportParameter("direccion", persona.Direccion);
            param[7] = new ReportParameter("telefono", persona.Telefono);
            param[8] = new ReportParameter("CiudadDireccion", persona.CiudadResidencia.nomciudad);
            param[9] = new ReportParameter("tipoidentificacion", persona.TipoIdentificacion.NombreTipoIdentificacion);
            param[10] = new ReportParameter("Num_identificacion", persona.NumeroDocumento);
            param[11] = new ReportParameter("fechacorte", ucFechaCorte.Text);
            param[12] = new ReportParameter("TextoHeader", separadas[0].ToString());
            param[13] = new ReportParameter("TextoFooter", separadas[1].ToString());
            param[14] = new ReportParameter("ImagenReport", cRutaDeImagen);
            param[15] = new ReportParameter("oficina", pUsuario.nombre_oficina);
            param[16] = new ReportParameter("celular", persona.Celular.ToString());


            Site toolBar = (Site)this.Master;
            toolBar.MostrarCancelar(true);
            toolBar.MostrarImprimir(false);
            toolBar.MostrarConsultar(false);

            mvPrincipal.ActiveViewIndex = 2;
            if (ddlTipoDocumento.SelectedValue == "1")
            {
                RpviewInfo1.LocalReport.EnableExternalImages = true;
                RpviewInfo1.LocalReport.SetParameters(param);
                RpviewInfo1.LocalReport.DataSources.Clear();
                ReportDataSource rds = new ReportDataSource("DataSet1", table);
                RpviewInfo1.LocalReport.DataSources.Add(rds);
                string ident = "Reporte Moras";
                RpviewInfo1.LocalReport.DisplayName = ident;
                RpviewInfo1.LocalReport.Refresh();

                RpviewInfo1.Visible = true;
            }
            else if (ddlTipoDocumento.SelectedValue == "2")
            {
                RpviewInfo2.LocalReport.EnableExternalImages = true;
                RpviewInfo2.LocalReport.SetParameters(param);
                RpviewInfo2.LocalReport.DataSources.Clear();
                ReportDataSource rds = new ReportDataSource("DataSet1", table);
                RpviewInfo2.LocalReport.DataSources.Add(rds);
                string ident = "Reporte Moras";
                RpviewInfo2.LocalReport.DisplayName = ident;
                RpviewInfo2.LocalReport.Refresh();

                RpviewInfo2.Visible = true;
            }
            else if (ddlTipoDocumento.SelectedValue == "3")
            {
                RpviewInfo3.LocalReport.EnableExternalImages = true;
                RpviewInfo3.LocalReport.SetParameters(param);
                RpviewInfo3.LocalReport.DataSources.Clear();
                ReportDataSource rds = new ReportDataSource("DataSet1", table);
                RpviewInfo3.LocalReport.DataSources.Add(rds);
                string ident = "Reporte Moras";
                RpviewInfo3.LocalReport.DisplayName = ident;
                RpviewInfo3.LocalReport.Refresh();

                RpviewInfo3.Visible = true;
            }
            else
            {
                VerError("No se a elegido un documento para generar o no existe");
            }
        }
        else
        {
            VerError("No existen Datos");
        }


    }
    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        ObtenerDatos(txtCodCliente.Text);
    }
    void Totalizar(List<Xpinn.Asesores.Entities.ProductosMora> lstProductosMora)
    {
        int filas = lstProductosMora.Count - 1;
        decimal totalcapital = 0;
        decimal totalExtras = 0;
        decimal totalInteres = 0;
        decimal totalMora = 0;
        decimal totalIOtros = 0;
        decimal totalCobranza = 0;


        for (int i = 0; i <= filas; i++)
        {
            totalcapital += Convert.ToDecimal(gvLista.Rows[i].Cells[6].Text);
            totalExtras += Convert.ToDecimal(gvLista.Rows[i].Cells[7].Text);
            totalInteres += Convert.ToDecimal(gvLista.Rows[i].Cells[8].Text);
            totalMora += Convert.ToDecimal(gvLista.Rows[i].Cells[9].Text);
            totalIOtros += Convert.ToDecimal(gvLista.Rows[i].Cells[10].Text);
            totalCobranza += Convert.ToDecimal(gvLista.Rows[i].Cells[11].Text);
        }

        txtTotCap.Text = totalcapital.ToString();
        txtTotExt.Text = totalExtras.ToString();
        txtTotInt.Text = totalInteres.ToString();
        txtTotMor.Text = totalMora.ToString();
        txtTotOtr.Text = totalIOtros.ToString();
        txtTototal.Text = totalCobranza.ToString();

    }

    protected void btnImprimir_Click(object sender, EventArgs e) //terminar de revisar
    {
        if (RpviewInfo1.Visible == true)
        {
            //MOSTRAR REPORTE EN PANTALLA
            Usuario pUsuario = (Usuario)Session["Usuario"];
            string cod_usuario = pUsuario.codusuario.ToString();

            byte[] bytes = RpviewInfo1.LocalReport.Render("PDF");
            string ruta = HttpContext.Current.Server.MapPath("output" + cod_usuario + ".pdf");

            if (File.Exists(ruta))
            {
                File.Delete(ruta);
            }

            FileStream fs = new FileStream(ruta, FileMode.Create);
            fs.Write(bytes, 0, bytes.Length);
            fs.Close();
            //frmPrint.Visible = true;
            Session["Archivo" + Usuario.codusuario] = Server.MapPath("output" + cod_usuario + ".pdf");
        }

    }
}