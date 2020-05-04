using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using Xpinn.Riesgo.Services;
using Xpinn.Riesgo.Entities;
using System.IO;
using System.Text;

public partial class Lista : GlobalWeb
{
    IdentificacionServices identificacionServicio = new IdentificacionServices();
    SeguimientoServices seguimientoServicio = new SeguimientoServices();
    MatrizServices matrizServicio = new MatrizServices();
    PoblarListas poblar = new PoblarListas();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(matrizServicio.CodigoProgramaG, "L");
            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoExportar += btnExportar_Click;
            toolBar.MostrarExportar(false);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(matrizServicio.CodigoProgramaG, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                Actualizar();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(matrizServicio.CodigoProgramaG, "Page_Load", ex);
        }
    }


    #region Métodos de carga de datos
    
    /// <summary>
    /// Cargar datos
    /// </summary>
    private void Actualizar()
    {
        List<Matriz> lstMatriz = new List<Matriz>();

        lstMatriz = matrizServicio.ListarMatrizGlobal((Usuario)Session["usuario"]);
        if (lstMatriz.Count > 0)
        {
            ViewState["lstMatriz"] = lstMatriz;
            gvMatriz.DataSource = lstMatriz;
            gvMatriz.DataBind();

            //Matriz pValorGlobal = new Matriz();
            double valor_rinherente = Math.Round(Convert.ToDouble(lstMatriz.Select(x => x.valor_rinherente).Sum())/lstMatriz.Count);
            double valor_control = Math.Round(Convert.ToDouble(lstMatriz.Select(x => x.valor_control).Sum()) / lstMatriz.Count);
            double valor_rresidual = Math.Round(Convert.ToDouble(lstMatriz.Select(x => x.valor_rinherente).Sum()) / lstMatriz.Count);

            if (valor_rinherente > 0)
            {
                txtRiesgoIG.Text = matrizServicio.ColoresRInherente[Convert.ToInt32(valor_rinherente)].descripcion;
                txtRiesgoIG.BackColor = System.Drawing.Color.FromName(matrizServicio.ColoresRInherente[Convert.ToInt32(valor_rinherente)].nivel);
            }

            //Cargar valoración control
            if (valor_control > 0)
            {
                txtValorCG.Text = matrizServicio.ColoresVControl[Convert.ToInt32(valor_control)].descripcion;
                txtValorCG.BackColor = System.Drawing.Color.FromName(matrizServicio.ColoresVControl[Convert.ToInt32(valor_control)].nivel);
            }

            //Cargar riesgo residual
            if (valor_rresidual > 0)
            {
                Matriz pValorResidual = new Matriz();
                pValorResidual = matrizServicio.ColoresRResidual.Where(x => Convert.ToInt32(valor_rresidual) <= x.valor_rresidual && x.valor_rresidual > 0).FirstOrDefault();
                txtRiesgoRG.Text = pValorResidual.descripcion;
                txtRiesgoRG.BackColor = System.Drawing.Color.FromName(pValorResidual.nivel);
            }


        }
        if (gvMatriz.Rows.Count > 0)
        {
            Site toolBar = (Site)this.Master;
            toolBar.MostrarExportar(true);
        }
    }
    
    #endregion

    #region Eventos de botones
    
    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        Actualizar();
    }

    /// <summary>
    /// Evento para exportar a Excel
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnExportar_Click(object sender, EventArgs e)
    {
        if (gvMatriz.Rows.Count > 0)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=MatrizRResidual.xls");
            Response.Charset = "UTF-8";
            Response.ContentEncoding = Encoding.Default;
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            GridView gvExportar = gvMatriz;
            gvExportar.AllowPaging = false;
            gvExportar.DataSource = (List<Matriz>)ViewState["lstMatriz"];
            gvExportar.DataBind();
            sw = ObtenerGrilla(gvExportar);
            Response.Output.Write("<div>" + sw.ToString() + "</div>");
            Response.Flush();
            Response.End();
        }
    }
    #endregion

    #region Eventos GridView

    protected void gvMatriz_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            List<Matriz> lstMatriz = ViewState["lstMatriz"] != null ? (List<Matriz>)ViewState["lstMatriz"] : null;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TextBox txtRiesgoI = (TextBox)e.Row.FindControl("txtRiesgoI");
                TextBox txtValorC = (TextBox)e.Row.FindControl("txtValorC");
                TextBox txtRiesgoR = (TextBox)e.Row.FindControl("txtRiesgoR");

                //Cargar descripción y color para la calificación del riesgo inherente
                Matriz dataItem = e.Row.DataItem as Matriz;
                if (dataItem.valor_rinherente > 0)
                {
                    txtRiesgoI.Text = matrizServicio.ColoresRInherente[dataItem.valor_rinherente].descripcion;
                    txtRiesgoI.BackColor = System.Drawing.Color.FromName(matrizServicio.ColoresRInherente[dataItem.valor_rinherente].nivel);
                }

                //Cargar valoración control
                if (dataItem.valor_control > 0)
                {
                    txtValorC.Text = matrizServicio.ColoresVControl[dataItem.valor_control].descripcion;
                    txtValorC.BackColor = System.Drawing.Color.FromName(matrizServicio.ColoresVControl[dataItem.valor_control].nivel);
                }

                //Cargar riesgo residual
                if (dataItem.valor_rresidual > 0)
                {
                    Matriz pValorResidual = new Matriz();
                    pValorResidual = matrizServicio.ColoresRResidual.Where(x => dataItem.valor_rresidual <= x.valor_rresidual && x.valor_rresidual > 0).FirstOrDefault();
                    txtRiesgoR.Text = pValorResidual.descripcion;
                    txtRiesgoR.BackColor = System.Drawing.Color.FromName(pValorResidual.nivel);
                }
            }
        }
        catch (Exception ex)
        {
            VerError(matrizServicio.CodigoProgramaG + "gvMatriz_RowDataBound" + ex.Message);
        }
    }
    
    #endregion
    
    /// <summary>
    /// Cargar grilla sin controles internos
    /// </summary>
    /// <param name="GridView1">Grilla a cargar</param>
    /// <returns></returns>             
    public StringWriter ObtenerGrilla(GridView GridView1)
    {
        using (StringWriter sw = new StringWriter())
        {
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            foreach (GridViewRow row in GridView1.Rows)
            {
                //row.BackColor = Color.White;
                foreach (TableCell cell in row.Cells)
                {
                    List<Control> lstControls = new List<Control>();

                    //Agregar controles para luego removerlos
                    foreach (Control control in cell.Controls)
                    {
                        lstControls.Add(control);
                    }
                    //Consultar controles para reemplazarlos
                    foreach (Control control in lstControls)
                    {
                        switch (control.GetType().Name)
                        {
                            case "TextBox":
                                cell.Controls.Add(new Literal { Text = (control as TextBox).Text });
                                cell.BackColor = (control as TextBox).BackColor;
                                break;
                            case "DropDownListGrid":
                                cell.Controls.Add(new Literal { Text = (control as DropDownListGrid).SelectedItem.Text });
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

    public override void VerifyRenderingInServerForm(Control control)
    {
        //required to avoid the runtime error "  
        //Control 'GridView1' of type 'GridView' must be placed inside a form tag with runat=server."  
    }
}