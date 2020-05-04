using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using Xpinn.NIIF.Services;
using Xpinn.NIIF.Entities;
using System.IO;
using System.Text;

public partial class Lista : GlobalWeb
{
    Xpinn.NIIF.Services.EstadosFinancierosNIIFService EstadosFinancierosNIFServicio = new Xpinn.NIIF.Services.EstadosFinancierosNIIFService();

    PoblarListas poblar = new PoblarListas();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(EstadosFinancierosNIFServicio.CodigoPrograma, "L");
            Site toolBar = (Site)this.Master;
            //  toolBar.eventoConsultar += btnConsultar_Click;
            // toolBar.eventoExportar += btnExportar_Click;
      
            toolBar.eventoNuevo += (s, evt) => {
                Session.Remove(EstadosFinancierosNIFServicio.CodigoPrograma + ".id");
                long idestadofinanciero = Convert.ToInt64(ddlTipoEstadoFinanciero.SelectedValue);                
                Session[EstadosFinancierosNIFServicio.CodigoPrograma + ".idestadofinanciero"] = idestadofinanciero;

                Navegar(Pagina.Nuevo);
            };

            toolBar.eventoRegresar += (s, evt) =>
            {
                Session.Remove(EstadosFinancierosNIFServicio.CodigoPrograma + ".id");
                Navegar("~/Page/Niif/EstadosFinancierosNIIF/Lista.aspx");
            };
            toolBar.MostrarRegresar(true);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(EstadosFinancierosNIFServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                CargarLista();
                Actualizar();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(EstadosFinancierosNIFServicio.CodigoPrograma, "Page_Load", ex);
        }
    }


    #region Métodos de carga de datos

    private void CargarLista()
    {
        LlenarListasDesplegables(TipoLista.TipoEstadoFinanciero, ddlTipoEstadoFinanciero);

    }




    /// <summary>
    /// Cargar datos si ya hay una EstadosFinancierosNIIF existente
    /// </summary>
    private void Actualizar()

    {

        ddlTipoEstadoFinanciero.SelectedValue = Convert.ToString(Session[EstadosFinancierosNIFServicio.CodigoPrograma + ".id"]);
        List<EstadosFinancierosNIIF> LstEstadosFinancierosNIIF = new List<EstadosFinancierosNIIF>();        
            VerError("");
            LstEstadosFinancierosNIIF = EstadosFinancierosNIFServicio.ListarConceptosNIF(Convert.ToInt64(ddlTipoEstadoFinanciero.SelectedValue), (Usuario)Session["usuario"]);
            if (LstEstadosFinancierosNIIF.Count > 0)
            {
                ViewState["LstEstadosFinancierosNIIF"] = LstEstadosFinancierosNIIF;
                gvConceptos.DataSource = LstEstadosFinancierosNIIF;
                gvConceptos.DataBind();
              
            }
            else
            {
                ViewState["LstEstadosFinancierosNIIF"] = null;
                gvConceptos.DataSource = null;
                gvConceptos.DataBind();
              
                VerError("No hay conceptos asociados al sistema seleccionado");
            }
        
        if (gvConceptos.Rows.Count > 0)
        {
            Site toolBar = (Site)this.Master;
            toolBar.MostrarGuardar(true);
            toolBar.MostrarExportar(true);
        }
    }

    /// <summary>
    /// Obtener los registros de la EstadosFinancierosNIIF
    /// </summary>
    /// <returns>Lista con los registros</returns>  
    private void ObtenerLista()
    {
        try
        {
            List<EstadosFinancierosNIIF> LstEstadosFinancierosNIIF = new List<EstadosFinancierosNIIF>();
            bool modificar = false;
            int contador = 0;

            //Verificar que la sesion no se encuentre vacia
            if (ViewState["LstEstadosFinancierosNIIF"] != null)
            {
                modificar = true;
                LstEstadosFinancierosNIIF = (List<EstadosFinancierosNIIF>)ViewState["LstEstadosFinancierosNIIF"];
            }

            //Verificar página actual de la grilla
            foreach (GridViewRow rFila in gvConceptos.Rows)
            {
                int rowindex = -1;
                EstadosFinancierosNIIF objDetalle = new EstadosFinancierosNIIF();

                //Obtener el objeto correspondiente a la fila y actualizarlo con los datos
                if (modificar == true)
                {
                    rowindex = (gvConceptos.PageIndex * gvConceptos.PageSize) + contador;
                    if (rowindex < LstEstadosFinancierosNIIF.Count)
                        objDetalle = LstEstadosFinancierosNIIF[rowindex];
                    else
                        objDetalle = null; 
                }
                if (modificar == false || (modificar == true && objDetalle != null))
                {
                    Int64 cod = Convert.ToInt32(gvConceptos.DataKeys[rFila.RowIndex].Values[0]);

                    TextBox txt_descripcion = (TextBox)rFila.FindControl("txtdescripcion");
                    TextBox txtcodigo = (TextBox)rFila.FindControl("txtcodigo");
                    TextBox txtcuentas = (TextBox)rFila.FindControl("txtcuentas");


                    objDetalle.cod_concepto = cod != 0 ? cod : 0;
                    objDetalle.descripcion = Convert.ToString(txt_descripcion.Text);
                    objDetalle.cuentascontables = Convert.ToString(txtcuentas.Text);



                    if (!modificar)
                        LstEstadosFinancierosNIIF.Add(objDetalle);
                }
                contador += 1;
            }
            ViewState["LstEstadosFinancierosNIIF"] = LstEstadosFinancierosNIIF;
        }
        catch (Exception ex)
        {
            VerError("Error al obtener la lista de datos " + ex.Message);
        }

    }

    #endregion

    #region Eventos de botones

   
    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        Actualizar();
    }

    /// <summary>
    /// Evento para agregar un nuevo detalle a la grilla
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnDetalle_Click(object sender, EventArgs e)
    {
        List<EstadosFinancierosNIIF> LstEstadosFinancierosNIIF = new List<EstadosFinancierosNIIF>();
        EstadosFinancierosNIIF pEstadoFinancieroConceptos = new EstadosFinancierosNIIF();

        ObtenerLista();
        LstEstadosFinancierosNIIF = ViewState["LstEstadosFinancierosNIIF"] != null ? (List<EstadosFinancierosNIIF>)ViewState["LstEstadosFinancierosNIIF"] : null;
        LstEstadosFinancierosNIIF.Add(pEstadoFinancieroConceptos);
        ViewState["LstEstadosFinancierosNIIF"] = LstEstadosFinancierosNIIF;
        gvConceptos.DataSource = LstEstadosFinancierosNIIF;
        gvConceptos.DataBind();
    }

    /// <summary>
    /// Evento para exportar a Excel
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnExportar_Click(object sender, EventArgs e)
    {
        if (gvConceptos.Rows.Count > 0)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=MatrizRInherente.xls");
            Response.Charset = "UTF-8";
            Response.ContentEncoding = Encoding.Default;
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            ExpGrilla expGrilla = new ExpGrilla();
            ObtenerLista();
            GridView gvExportar = gvConceptos;
            gvExportar.AllowPaging = false;
            gvExportar.DataSource = (List<EstadosFinancierosNIIF>)ViewState["LstEstadosFinancierosNIIF"];
            gvExportar.DataBind();
            sw = ObtenerGrilla(gvExportar);
            Response.Output.Write("<div>" + sw.ToString() + "</div>");
            Response.Flush();
            Response.End();
        }
    }
    #endregion

    #region Eventos GridView
    protected void gvConceptos_SelectedIndexChanged(object sender, EventArgs e)
    {
        long id = Convert.ToInt64(gvConceptos.SelectedRow.Cells[1].Text);
        long idestadofinanciero = Convert.ToInt64(ddlTipoEstadoFinanciero.SelectedValue);
        Session[EstadosFinancierosNIFServicio.CodigoPrograma + ".id"] = id;
        Session[EstadosFinancierosNIFServicio.CodigoPrograma + ".idestadofinanciero"] = idestadofinanciero;

        Navegar(Pagina.Nuevo);
    }


    protected void gvConceptos_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            List<EstadosFinancierosNIIF> LstEstadosFinancierosNIIF = new List<EstadosFinancierosNIIF>();
            ObtenerLista();
            LstEstadosFinancierosNIIF = ViewState["LstEstadosFinancierosNIIF"] != null ? (List<EstadosFinancierosNIIF>)ViewState["LstEstadosFinancierosNIIF"] : null;
            if (LstEstadosFinancierosNIIF != null)
            {
                gvConceptos.PageIndex = e.NewPageIndex;
                gvConceptos.DataSource = LstEstadosFinancierosNIIF;
                gvConceptos.DataBind();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(EstadosFinancierosNIFServicio.CodigoPrograma, "gvMatriz_PageIndexChanging", ex);
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
            //No visualizar columna de eliminación
            GridView1.Columns[0].Visible = false;
            GridView1.RenderControl(hw);
            return sw;
        }
    }

    protected void gvConceptos_PageIndexChanged(object sender, EventArgs e)
    {
        Actualizar();

    }
}