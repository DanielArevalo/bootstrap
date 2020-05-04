using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Xpinn.Riesgo.Entities;
using Xpinn.Riesgo.Services;
using Xpinn.Util;

partial class Nuevo : GlobalWeb
{
    MatrizServices _matrizCalor = new MatrizServices();


    #region Eventos Iniciales

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            VisualizarOpciones(_matrizCalor.CodigoProgramaFC, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoCancelar += btnCancelar_Click;
            toolBar.eventoGuardar += btnGuardar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_matrizCalor.CodigoProgramaFC, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                ObtenerDatos();
            }
            else
            {
                Session["Operacion"] = "1";
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_matrizCalor.CodigoProgramaFC, "Page_Load", ex);
        }
    }

    protected void ObtenerDatos()
    {
        List<FormaControl> lstConsulta = new List<FormaControl>();
        lstConsulta = _matrizCalor.ListaFormaControl();
        if (lstConsulta.Count > 0)
        {
            gvLista.DataSource = lstConsulta;
            gvLista.DataBind();
            Session["Detalle"] = lstConsulta;
        }
    }

    #endregion

    #region Eventos Botonera

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            List<FormaControl> lstDetalle = new List<FormaControl>();
            lstDetalle = ObtenerListaGridView();

            // Si llega nulo es porque hubo algun error
            if (lstDetalle != null)
            {
                if (lstDetalle.Count > 0)
                {
                    foreach (FormaControl eachCalor in lstDetalle)
                    {
                        FormaControl result = _matrizCalor.ModificarFormaControlPuntaje(eachCalor, (Usuario)Session["usuario"]);
                    }
                    ObtenerDatos();
                }
                else
                {
                    VerError("Algo salió mal, intenta nuevamente más tarde!.");
                }
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_matrizCalor.CodigoProgramaFC, "btnGuardar_Click", ex);
        }
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }
    #endregion

    #region Eventos GridView


    protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {}

    protected void gvLista_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            FormaControl mCalor = new FormaControl();
            mCalor.cod_formacontrol = ConvertirStringToInt32(e.Row.Cells[0].Text);
            mCalor.cod_atributo = ConvertirStringToInt32(e.Row.Cells[1].Text);
            mCalor.cod_opcion = ConvertirStringToInt32(e.Row.Cells[3].Text);
            TextBox txtValor = (TextBox)e.Row.FindControl("txtValor");
            if (txtValor != null)
            {
                if (txtValor.Visible)
                {
                    txtValor.Text = _matrizCalor.ConsultarPuntajeFormaControl(mCalor, (Usuario)Session["Usuario"]).ToString();
                }
            }            
        }
    }


    #endregion

    #region Metodos De Ayuda


    private List<FormaControl> ObtenerListaGridView()
    {
        List<FormaControl> lista = new List<FormaControl>();

        foreach (GridViewRow rfila in gvLista.Rows)
        {
            FormaControl mCalor = new FormaControl();
            mCalor.cod_formacontrol = ConvertirStringToInt32(rfila.Cells[0].Text);
            mCalor.cod_atributo = ConvertirStringToInt32(rfila.Cells[1].Text);
            mCalor.cod_opcion = ConvertirStringToInt32(rfila.Cells[3].Text);
            TextBox txtValor = (TextBox)rfila.FindControl("txtValor");
            if (txtValor != null)
            {
                if (txtValor.Visible)
                {
                    mCalor.valor = Convert.ToInt32(txtValor.Text);
                }
            }

            lista.Add(mCalor);
        }

        return lista;
    }

    protected void gvLista_OnDataBound(object sender, EventArgs e)
    {
        DataBoundTable(gvLista, 0);
    }

    public void DataBoundTable(GridView table, int ponerstylo)
    {
        for (int rowIndex = table.Rows.Count - 1; rowIndex >= 0; rowIndex--)
        {
            GridViewRow row = table.Rows[rowIndex];
            if (table.Rows.Count != rowIndex + 1)
            {
                GridViewRow previusrow = table.Rows[rowIndex + 1];
                if (row.Cells[2].Text == previusrow.Cells[2].Text)
                {
                    row.Cells[1].RowSpan = previusrow.Cells[1].RowSpan < 2 ? 2 : previusrow.Cells[1].RowSpan + 1;
                    previusrow.Cells[1].Visible = false;
                    row.Cells[2].RowSpan = previusrow.Cells[2].RowSpan < 2 ? 2 : previusrow.Cells[2].RowSpan + 1;
                    previusrow.Cells[2].Visible = false;
                }
                else if (ponerstylo != 1)
                {
                    styles(row);
                }
            }
            else if (ponerstylo != 1)
            {
                styles(row);
            }

        }
    }

    public void styles(GridViewRow row)
    {
        return;
    }

    #endregion


}
