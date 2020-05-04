using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using System.Text;
using System.IO;
using System.Web.UI.HtmlControls;
using Microsoft.Reporting.WebForms;
using System.IO.Packaging;
using System.Net;
using System.ComponentModel;
using Ionic.Zip;

partial class Lista : GlobalWeb
{
    Xpinn.Asesores.Services.ParametricaService serviceParametrica = new Xpinn.Asesores.Services.ParametricaService();
    Xpinn.Asesores.Entities.Oficina entityOficina = new Xpinn.Asesores.Entities.Oficina();
    private Xpinn.FabricaCreditos.Services.DestinacionService destinServicio = new Xpinn.FabricaCreditos.Services.DestinacionService();
    private Xpinn.Asesores.Services.EstadoCuentaService serviceEstadoCuenta = new Xpinn.Asesores.Services.EstadoCuentaService();
    private Xpinn.Asesores.Services.TiposDocCobranzasServices tipoDocumentoServicio = new Xpinn.Asesores.Services.TiposDocCobranzasServices();
    private Xpinn.Asesores.Services.CreditosService RecaudosMasivosServicio = new Xpinn.Asesores.Services.CreditosService();
    private const long BUFFER_SIZE = 4096;
    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones("110305", "L"); //Recordar Cambiar

            Site toolBar = (Site)this.Master;
            toolBar.eventoImprimir += btnImprimir_click;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
          
        
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
            Cargar_listas();

            if (!IsPostBack)
            {
                CargarValoresConsulta(pConsulta, destinServicio.CodigoPrograma);
                //if (Session[destinServicio.CodigoPrograma + ".consulta"] != null)
                   // Actualizar();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(destinServicio.CodigoPrograma, "Page_Load", ex);
        }
    }

    protected void btnImprimir_click(object sender, ImageClickEventArgs e)
    {
        
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        Actualizar();
    }

    protected void btnGenerar_Click(object sender, EventArgs e)
    {
        if (gvLista.Rows.Count > 0)
        {
            foreach (GridViewRow row in gvLista.Rows)
            {
                String id = row.Cells[3].Text.ToString();
                ObtenerDatos(id);

                if (ddlTipoDocumento.SelectedValue != "0")
                {
                    GenerarDocumento();

                }
            }


            try
            {

            string fileName = "Cartas.zip";
            string path = "c:\\Cartas.zip";
            string fullPath = path;
            FileInfo file = new FileInfo(fullPath);

            Response.Clear();
            //Response.ClearContent();
            //Response.ClearHeaders();
            //Response.Buffer = true;
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName);
            //Response.AppendHeader("Content-Cength", file.Length.ToString());
            Response.ContentType = "application/x-zip-compressed";
            Response.WriteFile(fullPath);
            //Response.Flush();
            Response.End();

            }
            catch
            {  }
            

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
            string msj = @"Nos permitimos informarle que la Cooperativa reportó para descuento por nomina a su pagaduria el valior correspondiente a las cuotas de sus obligaciones con corte a " + DateTime.Now .ToString() + " del cual se generó un saldo en NO DEDUCIDO por: - Es importante aclarar que este valor no incluye la cuota corriente del mes";
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
            param[11] = new ReportParameter("fechacorte", DateTime.Now.ToString());
            param[12] = new ReportParameter("TextoHeader", separadas[0].ToString());
            param[13] = new ReportParameter("TextoFooter", separadas[1].ToString());
            param[14] = new ReportParameter("ImagenReport", cRutaDeImagen);
            param[15] = new ReportParameter("oficina", pUsuario.nombre_oficina);
            param[16] = new ReportParameter("celular", persona.Celular.ToString());


            Site toolBar = (Site)this.Master;
            toolBar.MostrarCancelar(true);
            toolBar.MostrarImprimir(false);
            toolBar.MostrarConsultar(false);

        
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
                CreatePDF("Morosos", RpviewInfo1,persona.NumeroDocumento);
               // RpviewInfo1.Visible = true;
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
                CreatePDF("Carta1", RpviewInfo2, persona.NumeroDocumento);
                //RpviewInfo2.Visible = true;
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
                CreatePDF("Carta2", RpviewInfo3, persona.NumeroDocumento);

               // RpviewInfo3.Visible = true;
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

    private void CreatePDF(string fileName,ReportViewer r,string Numero_documento)
    {
        // Variables
        Warning[] warnings;
        string[] streamIds;
        string mimeType = string.Empty;
        string encoding = string.Empty;
        string extension = string.Empty;


        // Setup the report viewer object and get the array of bytes
        ReportViewer viewer = new ReportViewer();
        r.ProcessingMode = ProcessingMode.Local;
        //r.LocalReport.ReportPath = Server.MapPath("~/Page/Asesores/Gestion_Mora/Cartas"); 

        byte[] bytes = r.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);

        using (FileStream fs = new FileStream("C:\\Cartas\\" + Numero_documento + ".pdf", FileMode.Create))
        {
            fs.Write(bytes, 0, bytes.Length);
        }

        

        //// Now that you have all the bytes representing the PDF report, buffer it and send it to the client.
        //Response.Buffer = true;
        //Response.Clear();
        //Response.ContentType = mimeType;
        //Response.AddHeader("content-disposition", "attachment; filename=" + fileName + "." + extension);
        //Response.BinaryWrite(bytes); // create the file
        //Response.Flush(); // send it to the client to download
        //this.Context.ApplicationInstance.CompleteRequest();
    }

  
    

    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            string filtro_productos = "1,2,4,6";
            string pfechacorte = Convert.ToString(DateTime.Now.ToShortDateString());
            Xpinn.Asesores.Entities.Persona persona = new Xpinn.Asesores.Entities.Persona();
            persona = serviceEstadoCuenta.ConsultarPersona(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);
            if (persona.IdPersona != 0)
            {
               Session["Persona"] = persona;
            }
            Xpinn.Asesores.Services.CreditosService serviciosMoras = new Xpinn.Asesores.Services.CreditosService();
            List<Xpinn.Asesores.Entities.ProductosMora> lstProductosMora = new List<Xpinn.Asesores.Entities.ProductosMora>();
            lstProductosMora = serviciosMoras.ConsultarDetalleMoraPersona(Convert.ToString(pIdObjeto), "", Convert.ToDateTime(pfechacorte), filtro_productos, (Usuario)Session["usuario"]);
            Session["DatosGrilla"] = lstProductosMora;           
             
             
                  
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(destinServicio.CodigoPrograma, "ObtenerDatos", ex);
        }
    }
    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        txtIdentiCliente.Text = "";
        txtCliente.Text = "";
        txtCodigoNomina.Text = "";
        ddlCalificacion.SelectedValue = "0";
        ddlOficina.SelectedValue = "0";
    }


  
    protected void gvLista_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            ConfirmarEliminarFila(e, "btnBorrar");
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(destinServicio.CodigoPrograma + "L", "gvLista_RowDataBound", ex);
        }
    }

    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        String id = gvLista.DataKeys[gvLista.SelectedRow.RowIndex].Value.ToString();
        Session[destinServicio.CodigoPrograma + ".id"] = id;
        Navegar(Pagina.Nuevo);
    }

    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvLista.Rows[e.NewEditIndex].Cells[0].Text;
        //String identifi = gvLista.Rows[e.NewEditIndex].Cells[1].Text;
        Session[destinServicio.CodigoPrograma + ".id"] = id;
        Navegar(Pagina.Nuevo);
    }
 

    protected void btnExportar_Click(object sender, EventArgs e)
    {
        StringBuilder sb = new StringBuilder();
        StringWriter sw = new StringWriter(sb);
        HtmlTextWriter htw = new HtmlTextWriter(sw);
        Page pagina = new Page();
        dynamic form = new HtmlForm();
        gvLista.AllowPaging = false;
        gvLista.DataSource = Session["Morosos"];
        gvLista.DataBind();
        //gvListaCreditos.Columns[0].Visible = false;
        gvLista.EnableViewState = false;
        pagina.EnableEventValidation = false;
        pagina.DesignerInitialize();
        pagina.Controls.Add(form);

        form.Controls.Add(gvLista);
        Response.Clear();
        Response.Buffer = true;
        Response.ContentType = "application/vnd.ms-excel";
        Response.AddHeader("Content-Disposition", "attachment;filename=ReporteCreditos.xls");
        Response.Charset = "UTF-8";
        Response.ContentEncoding = Encoding.Default;
        pagina.RenderControl(htw);
        Response.Write(sb.ToString());
        Response.End();
    }

    protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            Int64 id = Convert.ToInt64(e.Keys[0]);
            try
            {
                destinServicio.EliminarDestinacion(id, (Usuario)Session["usuario"]);
            }
            catch (Exception ex)
            {
                VerError(ex.Message);
            }
            Actualizar();
        }
        catch (Xpinn.Util.ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(destinServicio.CodigoPrograma, "gvLista_RowDeleting", ex);
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
            BOexcepcion.Throw(destinServicio.CodigoPrograma, "gvLista_PageIndexChanging", ex);
        }
    }

    private void Actualizar()
    {
        try
        {
            List<Xpinn.Asesores.Entities.PersonaMora> lstConsulta = new List<Xpinn.Asesores.Entities.PersonaMora>();
            //if (Session["Morosos"] == null)
            // {
                lstConsulta = RecaudosMasivosServicio.ListarPersonasMora(Obtenerfiltro(), (Usuario)Session["usuario"]);
                gvLista.PageSize = pageSize;
                gvLista.EmptyDataText = emptyQuery;
                gvLista.DataSource = lstConsulta;
                Session["Morosos"] = lstConsulta;
            //}
            //else
            //{
            //lstConsulta = (List<Xpinn.Asesores.Entities.PersonaMora>)Session["Morosos"];
            //gvLista.PageSize = pageSize;
            //gvLista.EmptyDataText = emptyQuery;
            //gvLista.DataSource = lstConsulta;
            //}

            if (lstConsulta.Count > 0)
            {
                gvLista.Visible = true;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                gvLista.DataBind();
            }
            else
            {
                gvLista.Visible = false;
                lblTotalRegs.Visible = false;
                // lblInfo.Visible = true;
            }
            Session.Add(destinServicio.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(destinServicio.CodigoPrograma, "Actualizar", ex);
        }
    }

    private string Obtenerfiltro()
    {

        string filtro = "";

        if (txtCliente.Text != "")
        {
            filtro = "and r.COD_PERSONA = " + txtCliente.Text.Trim();
        }

        if (txtIdentiCliente.Text != "")
        {
            filtro = filtro + "and  p.identificacion = '" + txtIdentiCliente.Text.Trim() + "'";
        }

        if (txtCodigoNomina.Text != "")
        {
            filtro = filtro + "and  p.COD_NOMINA = " + txtCodigoNomina.Text.Trim();
        }


        if (ddlMora.SelectedIndex != 0 && ddlMora.SelectedIndex != 9)
        {
            filtro += " and ( DIAS_MORA_C between " + ddlMora.SelectedValue;
            filtro += " OR DIAS_MORA_A between " + ddlMora.SelectedValue;
            filtro += " OR DIAS_MORA_S between " + ddlMora.SelectedValue;
            filtro += " OR DIAS_MORA_PA between " + ddlMora.SelectedValue + ") ";
        }

        //if (txtCliente.Text != "")
        //{
        //   filtro = filtro + "and r.COD_OFICINA = " + txtCliente.Text.Trim();
        //}
        //if (txtCliente.Text != "")
        //{
        //    filtro = filtro + "and r.COD_ZONA = " + txtCliente.Text.Trim();
        //}
        //FALTA VALIDAR

        return filtro;
    }

    protected void Check_Clicked(object sender, EventArgs e)
    {
        CheckBox chkHeader = sender as CheckBox;

        if (chkHeader.Checked == true)
        {
            if (gvLista.Rows.Count > 0)
            {
                foreach (GridViewRow row in gvLista.Rows)
                {
                    CheckBox CheckBoxgv = row.FindControl("CheckBoxgv") as CheckBox;
                    CheckBoxgv.Checked = true;

                }

            }
        }
        else
        {
            foreach (GridViewRow row in gvLista.Rows)
            {
                CheckBox CheckBoxgv = row.FindControl("CheckBoxgv") as CheckBox;
                CheckBoxgv.Checked = false;

            }
        }
    }

    private void Cargar_listas()
    {

        ddlOficina.DataSource = serviceParametrica.ListarOficina(entityOficina, (Usuario)Session["Usuario"]);
        ddlOficina.DataTextField = "NombreOficina";
        ddlOficina.DataValueField = "IdOficina";
        ddlOficina.DataBind();

    }







}


