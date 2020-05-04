using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Text;
using System.Drawing;
using System.Web.UI.HtmlControls;
using Xpinn.Confecoop.Entities;
using Xpinn.Confecoop.Services;

public partial class Lista : GlobalWeb
{
    ConfecoopService pucService = new ConfecoopService();
    RiesgoLiquidezServices BORiesgo = new RiesgoLiquidezServices();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(pucService.CodigoProgramaRLiquidez, "L");
            Site toolBar = (Site)Master;

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(pucService.GetType().Name + "L", "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                mvActivosFijos.ActiveViewIndex = 0;
                CargarFecha();
                //Actualizar();
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(pucService.GetType().Name + "D", "Page_Load", ex);
        }
    }


    protected void CargarFecha()
    {
        //PUC pPuc = new PUC();
        //string tipo, estado;
        //tipo = "C";
        //estado = "D";
        //ddlFecha.DataSource = pucService.ListarFechaCierreGLOBAL(tipo, estado, (Usuario)Session["usuario"]);
        //ddlFecha.DataTextField = "fecha";
        //ddlFecha.DataTextFormatString = "{0:" + gFormatoFecha + "}";
        //ddlFecha.DataValueField = "fecha";
        //ddlFecha.DataBind();

        CargarFecha("C");

        ddlTipoCuenta.Items.Insert(0, new ListItem("Local", "0"));
        ddlTipoCuenta.Items.Insert(1, new ListItem("Niif", "1"));
        ddlTipoCuenta.DataBind();
        ddlTipoCuenta.SelectedIndex = 0;
    }

    protected void Actualizar()
    {
        VerError("");
        List<RiesgoLiquidez> lstRiesgo = new List<RiesgoLiquidez>();
        if (ddlFecha.SelectedItem == null)
        {
            VerError("No existen fechas de cortes para realizar el proceso.");
            return;
        }
        DateTime pFechaCorte = Convert.ToDateTime(ddlFecha.SelectedValue);
        bool _esNiif = false;
        if (ddlTipoCuenta.SelectedIndex == 1)
            _esNiif = true;
        lstRiesgo = BORiesgo.ListarRiesgoLiquidez(pFechaCorte, _esNiif, (Usuario)Session["usuario"]);
        if (lstRiesgo.Count > 0)
        {
            gvLista.DataSource = lstRiesgo;
            gvLista.DataBind();
            btnExportar.Visible = true;
        }
    }

    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        VerError("");
        try
        {
            if (ddlFecha.SelectedItem == null)
            {
                VerError("No existen fechas para seleccionar, verifique los datos");
                return;
            }
            Actualizar();
        }
        catch (Exception ex)
        {
            VerError(ex.Message);            
        }
    }


    protected void btnExportar_Click(object sender, EventArgs e)
    {
        VerError("");
        if (ddlFecha.SelectedItem == null)
        {
            VerError("No existen fechas para seleccionar, verifique los datos");
            return;
        }
        if (rbTipoArchivo.SelectedItem != null)
        {
            PUC pPuc = new PUC();
            // Determinando el tipo de archivo y el Separador
            if (rbTipoArchivo.SelectedIndex == 0)
                pPuc.separador = ";";
            else if (rbTipoArchivo.SelectedIndex == 1)
                pPuc.separador = "  ";
            else if (rbTipoArchivo.SelectedIndex == 2)
                pPuc.separador = "|";

            // Determinando el nombre del archivo
            string fic = "";
            if (txtArchivo.Text != "")
            {
                if (rbTipoArchivo.SelectedIndex == 0)
                {
                    fic = txtArchivo.Text.Trim().Contains(".csv") ? txtArchivo.Text : txtArchivo.Text + ".csv";
                }
                else if (rbTipoArchivo.SelectedIndex == 1)
                {
                    fic = txtArchivo.Text.Trim().Contains(".txt") ? txtArchivo.Text : txtArchivo.Text + ".txt";
                }
                else if (rbTipoArchivo.SelectedIndex == 2)
                {
                    fic = txtArchivo.Text.Trim().Contains(".xls") ? txtArchivo.Text : txtArchivo.Text + ".xls";
                }
            }
            else
            {
                VerError("Ingrese el Nombre del archivo a Generar");
                return;
            }

            // ELIMINANDO ARCHIVOS GENERADOS
            try
            {
                string[] ficherosCarpeta = Directory.GetFiles(Server.MapPath("Archivos\\"));
                foreach (string ficheroActual in ficherosCarpeta)
                    File.Delete(ficheroActual);
            }
            catch
            { }

            
            Int32 Fila = 0;
            try
            {
                // Copiar el archivo al cliente        
                
                    if (rbTipoArchivo.SelectedItem.Text == "CSV" || rbTipoArchivo.SelectedItem.Text == "TEXTO") // TEXTO O CSV
                    {
                        Response.Clear();
                        Response.Buffer = true;
                        Response.AddHeader("content-disposition","attachment;filename=" + fic);
                        Response.Charset = "";
                        Response.ContentType = "application/text";
                        StringBuilder sb = new StringBuilder();
                        string pSeparador = string.Empty;
                        if (rbTipoArchivo.SelectedIndex == 0)
                            pSeparador = ";";
                        else if (rbTipoArchivo.SelectedIndex == 1)
                            pSeparador = "  ";
                        sb = ObtenerGrillaCSVTXT(gvLista, pSeparador);

                        Response.Output.Write(sb.ToString());
                        Response.Flush();
                        Response.End();
                    }
                    else
                    {
                        Response.Clear();
                        Response.Buffer = true;
                        Response.AddHeader("content-disposition", "attachment;filename=" + fic);
                        Response.Charset = "";
                        Response.ContentType = "application/vnd.ms-excel";
                        Response.ContentEncoding = Encoding.Default;
                        StringWriter sw = new StringWriter();
                        ExpGrilla expGrilla = new ExpGrilla();

                        sw = ObtenerGrilla(gvLista);
                        Response.Write(expGrilla.style);
                        Response.Output.Write(sw.ToString());
                        Response.Flush();
                        Response.End();
                    }

                    mvActivosFijos.ActiveViewIndex = 1;
                
            }
            catch
            {
                VerError("Se generó un error al realizar el archivo. En la Fila " + Fila);
            }
        }
        else
        {
            VerError("Seleccione el Tipo de Archivo");
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }

    public StringBuilder ObtenerGrillaCSVTXT(GridView GridView1, string pSeparador)
    {
        StringBuilder sb = new StringBuilder();
        //Adicionando HEADER
        for (int k = 0; k < GridView1.Columns.Count; k++)
        {
            //Adicionando separador
            sb.Append(GridView1.Columns[k].HeaderText + pSeparador);
        }
        sb.Append("\r\n");
        //Adicionando CONTENT
        foreach (GridViewRow row in GridView1.Rows)
        {
            foreach (TableCell cell in row.Cells)
            {
                List<Control> lstControls = new List<Control>();

                foreach (Control control in cell.Controls)
                {
                    lstControls.Add(control);
                }

                if (lstControls.Count > 0)
                {
                    foreach (Control control in lstControls)
                    {
                        switch (control.GetType().Name)
                        {
                            case "Label":
                                sb.Append((control as Label).Text + pSeparador);
                                break;
                            case "general_controles_decimales_ascx":
                                sb.Append((control as decimales).Text + pSeparador);
                                break;
                        }
                    }
                }
                else
                {
                    sb.Append(cell.Text + pSeparador);
                }
            }           
            sb.Append("\r\n");
        }        
        return sb;
    }

    public StringWriter ObtenerGrilla(GridView GridView1)
    {
        using (StringWriter sw = new StringWriter())
        {
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            foreach (GridViewRow row in GridView1.Rows)
            {
                row.BackColor = Color.White;
                foreach (TableCell cell in row.Cells)
                {
                    if (row.RowIndex % 2 == 0)
                    {
                        cell.BackColor = GridView1.AlternatingRowStyle.BackColor;
                    }
                    else
                    {
                        cell.BackColor = GridView1.RowStyle.BackColor;
                    }
                    cell.CssClass = "gridItem";
                    List<Control> lstControls = new List<Control>();

                    //Add controls to be removed to Generic List
                    foreach (Control control in cell.Controls)
                    {
                        lstControls.Add(control);
                    }

                    //Loop through the controls to be removed and replace then with Literal
                    foreach (Control control in lstControls)
                    {
                        switch (control.GetType().Name)
                        {
                            case "Label":
                                cell.Controls.Add(new Literal { Text = (control as Label).Text });
                                break;
                            case "general_controles_decimales_ascx":
                                cell.Controls.Add(new Literal { Text = (control as decimales).Text });
                                break;
                        }
                        cell.Controls.Remove(control);
                    }
                }
            }

            GridView1.RenderControl(hw);

            return sw;
        }
    }


    protected void gvLista_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string unidad = e.Row.Cells[0].Text;
            string renglon = e.Row.Cells[1].Text;
            if (renglon == "999")
            {
                e.Row.BackColor = Color.Green;
            }
        }
    }

    protected void ddlTipoCuenta_SelectedIndexChanged(object sender, EventArgs e)
    {
        string pTipo = ddlTipoCuenta.SelectedValue == "1" ? "G" : "C";
        CargarFecha(pTipo);
    }

    void CargarFecha(string pTipo)
    {
        string pFecAnterior = ddlFecha.SelectedItem != null ? ddlFecha.SelectedValue : null;
        PUC pPuc = new PUC();
        string tipo, estado;
        tipo = pTipo;
        estado = "D";
        ddlFecha.DataSource = pucService.ListarFechaCierreGLOBAL(tipo, estado, (Usuario)Session["usuario"]);
        ddlFecha.DataTextField = "fecha";
        ddlFecha.DataTextFormatString = "{0:" + gFormatoFecha + "}";
        ddlFecha.DataValueField = "fecha";
        ddlFecha.DataBind();
        try
        {
            if (pFecAnterior != null)
                ddlFecha.SelectedValue = pFecAnterior;
        }
        catch
        {
        }
    }


}
