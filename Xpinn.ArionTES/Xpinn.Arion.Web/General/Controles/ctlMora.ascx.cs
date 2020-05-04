using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;

public partial class ctlMora : System.Web.UI.UserControl
{
    private Xpinn.Asesores.Services.TiposDocCobranzasServices tipoDocumentoServicio = new Xpinn.Asesores.Services.TiposDocCobranzasServices();
    private Xpinn.Asesores.Services.EstadoCuentaService serviceEstadoCuenta = new Xpinn.Asesores.Services.EstadoCuentaService();
    protected void Page_Load(object sender, EventArgs e)
    {
        Actualizar();
    }


    protected void btnImpMora_Click(object sender, EventArgs e)
    {
        Actualizar();
    }

    public void Actualizar()
    {
        Usuario pusuario = new Usuario();
        pusuario = (Usuario)Session["Usuario"];
        Xpinn.Asesores.Services.CreditosService _servicio = new Xpinn.Asesores.Services.CreditosService();
        try { txtMora.Text = Convert.ToString(Math.Round(_servicio.ConsultarTotalValorMoraPersona(txtCodigo.Text, txtIdentificacion.Text, Convert.ToDateTime(txtFechaCorte.Text), pusuario), 0)); } catch { }
        if (txtMora.Text == "0")
        {
            Imprimir.Enabled = false;
        }
        else
        {
            Imprimir.Enabled = true;
        }
    }

    public void btnImprimir_Click2(object sender, EventArgs e)
    {
        GenerarDocumento();
        mpeMostrar.Show();
    }

    protected void btnClose_Click(object sender, EventArgs e)
    {
        mpeMostrar.Hide();
    }

    void GenerarDocumento()
    {
        Xpinn.Asesores.Entities.Persona persona = new Xpinn.Asesores.Entities.Persona();
        persona = serviceEstadoCuenta.ConsultarPersona(Convert.ToInt64(txtCodigo.Text), (Usuario)Session["usuario"]);

        Xpinn.Asesores.Services.CreditosService serviciosMoras = new Xpinn.Asesores.Services.CreditosService();
        List<Xpinn.Asesores.Entities.ProductosMora> lstConsulta = new List<Xpinn.Asesores.Entities.ProductosMora>();
        lstConsulta = serviciosMoras.ConsultarDetalleMoraPersona(Convert.ToString(txtCodigo.Text), "", Convert.ToDateTime(txtFechaCorte.Text), "1,2,3,4,6", (Usuario)Session["usuario"]);
        //"1,2,4,6" filtro de los productos a cargar

        Xpinn.Asesores.Entities.TiposDocCobranzas vTipoDoc = new Xpinn.Asesores.Entities.TiposDocCobranzas();
        vTipoDoc = tipoDocumentoServicio.ConsultarTiposDocumento(Convert.ToInt64(5), (Usuario)Session["usuario"]);

        string[] separadas;
        if (vTipoDoc.texto != null && vTipoDoc.texto != "")
        {
            separadas = vTipoDoc.texto.Split('-');
        }
        else
        {
            separadas = @"".Split('-');
        }


        if (lstConsulta.Count > 0)
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
            param[11] = new ReportParameter("fechacorte", txtFechaCorte.Text);
            param[12] = new ReportParameter("TextoHeader", separadas[0].ToString());
            param[13] = new ReportParameter("TextoFooter", separadas[0].ToString());
            param[14] = new ReportParameter("ImagenReport", cRutaDeImagen);
            param[15] = new ReportParameter("oficina", pUsuario.nombre_oficina);
            param[16] = new ReportParameter("celular", persona.Celular == null ? "" : persona.Celular.ToString());

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
        else
        {
            //  VerError("No existen Datos");
        }

    }

}