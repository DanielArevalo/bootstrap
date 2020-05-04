using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Xpinn.Cartera.Entities;
using Xpinn.Cartera.Services;
using Xpinn.Util;

public partial class Detalle : GlobalWeb
{
    CuadreCarteraService _cuadreServicio = new CuadreCarteraService();
    CuadreCartera _cuadreCartera;


    #region Eventos Iniciales


    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(_cuadreServicio.CodigoPrograma, "D");
            Site toolBar = (Site)Master;
            toolBar.eventoRegresar += (s, evt) =>
            {
                Session.Remove(_cuadreServicio.CodigoPrograma + ".cuadre");
                Navegar(Pagina.Lista);
            };
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_cuadreServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Session[_cuadreServicio.CodigoPrograma + ".cuadre"] != null)
                {
                    _cuadreCartera = (CuadreCartera)Session[_cuadreServicio.CodigoPrograma + ".cuadre"];

                    if (_cuadreCartera.tipo_comp == 0 || _cuadreCartera.num_comp == 0)
                    {
                        VerError("No se pudo obtener los valores para el número y/o tipo de comprobante");
                        return;
                    }

                    InicializarPagina();
                }
                else
                {
                    VerError("No se pudo obtener los valores para el número y/o tipo de comprobante");
                }
            }
            else
            {   
                if (Session[_cuadreServicio.CodigoPrograma + ".cuadre"] != null)
                {
                    _cuadreCartera = (CuadreCartera)Session[_cuadreServicio.CodigoPrograma + ".cuadre"];
                }
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_cuadreServicio.CodigoPrograma, "Page_Load", ex);
        }
    }

    private void InicializarPagina()
    {
        LlenarPagina();

        bool exitosoOperativo = CuadreOperativoVsContable();
    }


    #endregion


    #region Métodos Ayuda


    void LlenarPagina()
    {
        StringHelper stringHelper = new StringHelper();
        txtNumeroComprobante.Text = _cuadreCartera.num_comp.ToString();
        txtTipoComprobante.Text = _cuadreCartera.tipo_comp.ToString();
        txtCodAtributo.Text = _cuadreCartera.cod_atr.ToString();
        txtNombreAtributo.Text = _cuadreCartera.nom_atr;
        txtDescripcion.Text = _cuadreCartera.detalle;
        txtFecha.Text = _cuadreCartera.fecha != DateTime.MinValue ? _cuadreCartera.fecha.ToShortDateString() : string.Empty;
        txtValorContable.Text = stringHelper.FormatearNumerosComoCurrency(_cuadreCartera.valor_contable);
        txtValorOperativo.Text = stringHelper.FormatearNumerosComoCurrency(_cuadreCartera.valor_operativo);
        txtDiferencia.Text = stringHelper.FormatearNumerosComoCurrency(_cuadreCartera.diferencia);
        txtTipoDiferencia.Text = _cuadreCartera.tipo;
        txtTipoProducto.Text = _cuadreCartera.tipo_producto_enum.ToString();
    }
    

    private bool CuadreOperativoVsContable()
    {
        try
        {
            List<CuadreCartera> lstCuadreContable  = _cuadreServicio.ConsultarCuadreContablePorComprobante(_cuadreCartera, Usuario);
            List<CuadreCartera> lstCuadreOperativo = _cuadreServicio.ConsultarCuadreOperativoPorComprobante(_cuadreCartera, Usuario);
            foreach (CuadreCartera itemC in lstCuadreContable)
            {
                // Buscar el item en la contabilidad
                foreach (CuadreCartera itemO in lstCuadreOperativo)
                {
                    if (itemO.cod_tipo_producto == itemC.cod_tipo_producto && itemO.numero_transaccion == itemC.numero_transaccion)
                    {
                        itemC.cod_ope = itemO.cod_ope;
                        itemC.nom_linea = itemO.nom_linea;
                        itemC.nom_atr = itemO.nom_atr;
                        itemC.nom_tipo_tran = itemO.nom_tipo_tran;
                        itemC.tipo_mov_operativo = itemO.tipo_mov_operativo;
                        itemC.valor_operativo = itemO.valor_operativo;
                        itemC.diferencia = itemC.valor_contable - itemC.valor_operativo;
                        itemC.conciliado = 1;
                        itemO.conciliado = 1;
                    }
                }
            }
            foreach (CuadreCartera itemO in lstCuadreOperativo)
            {
                if (itemO.conciliado == 0)
                {
                    itemO.diferencia = itemO.valor_contable - itemO.valor_operativo;
                    itemO.conciliado = 1;
                    lstCuadreContable.Add(itemO);
                }
            }

            if (lstCuadreContable.Count > 0)
            {
                lblTotalRegsOperativo.Text = "Se encontraron " + lstCuadreContable.Count + " registros!.";
                ViewState["lstCuadreContable"] = lstCuadreContable;
            }
            else
            {
                lblTotalRegsOperativo.Text = "No se encontraron registros!.";
            }

            gvOperativo.DataSource = lstCuadreContable;
            gvOperativo.DataBind();

            return true;
        }
        catch (Exception ex)
        {
            VerError("Error al llenar cuadre contable, " + ex.Message);
            return false;
        }
    }


    #endregion

    protected void gvOperativo_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridViewRow gvHeaderRow = e.Row;
            GridViewRow gvHeaderRowCopy = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);

            this.gvOperativo.Controls[0].Controls.AddAt(0, gvHeaderRowCopy);

            //TableCell tcFirst = e.Row.Cells[0];
            //tcFirst.RowSpan = 2;
            //gvHeaderRowCopy.Cells.AddAt(0, tcFirst);

            TableCell tcMergePeriodo = new TableCell();
            tcMergePeriodo.Text = "Contable";
            tcMergePeriodo.ColumnSpan = 7;
            gvHeaderRowCopy.Cells.AddAt(0, tcMergePeriodo);

            TableCell tcMergeAcumulado = new TableCell();
            tcMergeAcumulado.Text = "Operativo";
            tcMergeAcumulado.ColumnSpan = 6;
            gvHeaderRowCopy.Cells.AddAt(1, tcMergeAcumulado);

            TableCell tcSecond = e.Row.Cells[13];
            tcSecond.RowSpan = 2;
            gvHeaderRowCopy.Cells.AddAt(2, tcSecond);
        }
    }

}