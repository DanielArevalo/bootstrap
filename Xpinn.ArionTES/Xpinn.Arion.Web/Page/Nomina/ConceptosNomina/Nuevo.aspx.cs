using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Nomina.Entities;
using Xpinn.Nomina.Services;
using Xpinn.Util;

public partial class Page_Nomina_ConceptosNomina_Nuevo : GlobalWeb
{
    ConceptoNominaService _conceptoNominaService = new ConceptoNominaService();


    #region Eventos Iniciales


    public void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            if (Session[_conceptoNominaService.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(_conceptoNominaService.CodigoPrograma, "E");
            else
                VisualizarOpciones(_conceptoNominaService.CodigoPrograma, "A");

            Site toolBar = (Site)Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;

            toolBar.MostrarRegresar(false);
        }
        catch (Exception ex)
        {
            VerError("Error al inicializar la pagina, " + ex.Message);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                LlenarListasDesplegables(TipoLista.TipoConceptoNomina, ddlTipConcepto);
                //TRAER DATOS DE los conceptos base

                List<ConceptosNomina> lstConsultaConceptosBase = new List<ConceptosNomina>();
                String filtro = ObtenerFiltro();
                lstConsultaConceptosBase = _conceptoNominaService.ConsultarConceptosNominaConfiltro((Usuario)Session["usuario"], filtro);

                gvConceptosBase.PageSize = pageSize;
                gvConceptosBase.EmptyDataText = emptyQuery;
                gvConceptosBase.DataSource = lstConsultaConceptosBase;

                if (lstConsultaConceptosBase.Count > 0)
                {
                    gvConceptosBase.Visible = true;
                    gvConceptosBase.DataBind();
                    //ValidarPermisosGrilla(gvRecoger);
                }
                else
                {
                    gvConceptosBase.Visible = false;
                }


                if (Session[_conceptoNominaService.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[_conceptoNominaService.CodigoPrograma + ".id"].ToString();
                    ConsultarConceptoNominaYLlenar(idObjeto);
                }
                else
                {
                    ConceptosNomina conceptoNomina = _conceptoNominaService.ConsultarCodigoMaximoConceptoNomina(Usuario);
                    txtConsecutivo.Text = (conceptoNomina.CONSECUTIVO + 1).ToString();
                }
            }
        }
        catch (Exception ex)
        {
            VerError("Error al inicializar la pagina, " + ex.Message);
        }
    }

    protected void ConsultarConceptoNominaYLlenar(String pIdObjeto)
    {
        try
        {
            ConceptosNomina conceptosNomina = _conceptoNominaService.ConsultarConceptoNomina(Usuario, pIdObjeto);

            txtConsecutivo.Text = conceptosNomina.CONSECUTIVO.ToString();
            conceptosNomina.CONSECUTIVOConcepto = conceptosNomina.CONSECUTIVO.ToString();
            txtDescripcion.Text = conceptosNomina.DESCRIPCION;
            txtPonderado.Text = conceptosNomina.PONDERADO.ToString();
            ddlClaseConcepto.SelectedValue = conceptosNomina.CLASE.ToString();
            txtConceptos.Text = conceptosNomina.FORMULA;
            ddlTipConcepto.SelectedValue = conceptosNomina.TIPO_CONCEPTO.ToString();
            rbdUnidadConcepto.SelectedValue = conceptosNomina.UNIDAD_CONCEPTO.ToString();
            ddlTipo.SelectedValue = conceptosNomina.tipo.ToString();


            txtPorcentaje.Text = conceptosNomina.porcentajeprovisionextralegal.ToString();

            if (conceptosNomina.provisiona_extralegal == 0)
                rbdProvisonExtralegalNo.Checked = true;

            if (conceptosNomina.provisiona_extralegal == 1)
                rbdProvisonExtralegalSi.Checked = true;


            //RECUPERAR DATOS DE DESTINACIÓN

            List<ConceptosNomina> lstConsultaConceptosBase = new List<ConceptosNomina>();
            String filtro = ObtenerFiltro2();
            if (filtro != null && txtConceptos.Text != "")
            {
                lstConsultaConceptosBase = _conceptoNominaService.ConsultarConceptosNominaConfiltro((Usuario)Session["usuario"], filtro);

                if (lstConsultaConceptosBase.Count > 0)
                {
                    foreach (var item in lstConsultaConceptosBase)
                    {
                        foreach (GridViewRow rFila in gvConceptosBase.Rows)
                        {
                            CheckBoxGrid chkSeleccione = rFila.FindControl("cbListado") as CheckBoxGrid;
                            Label lbl_consecutivo = (Label)rFila.FindControl("lbl_consecutivo");
                            if (item.CONSECUTIVO == Convert.ToInt32(lbl_consecutivo.Text))
                            {
                                chkSeleccione.Checked = true;
                            }
                        }

                    }
                }
            }











        }
        catch (Exception ex)
        {
            VerError("No se pudo consultar el concepto, " + ex.Message);
        }
    }


    #endregion


    #region Eventos Botones


    void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        try
        {



            if (ValidarDatos())
            {
                ConceptosNomina conceptosNomina = ObtenerDatos();
                if (rbdProvisonExtralegalNo.Checked)
                    conceptosNomina.provisiona_extralegal = 0;
                if (rbdProvisonExtralegalSi.Checked)
                    conceptosNomina.provisiona_extralegal = 1;


                if (!string.IsNullOrWhiteSpace(idObjeto))
                {
                    conceptosNomina.CONSECUTIVO = Convert.ToInt64(idObjeto);




                    _conceptoNominaService.ModificarConceptoNomina(conceptosNomina, Usuario);
                }
                else
                {
                    conceptosNomina.CONSECUTIVO = Convert.ToInt64(txtConsecutivo.Text);

                    _conceptoNominaService.CrearConceptoNomina(conceptosNomina, Usuario);
                }
            }
        }
        catch (Exception ex)
        {
            VerError("No se pudo guardar el concepto nomina" + ex.Message);
        }
        Site toolBar = (Site)this.Master;
        toolBar.MostrarGuardar(false);
        mvDatos.ActiveViewIndex = 1;

    }

    void btnCancelar_Click(object sender, ImageClickEventArgs e)
    {
        Navegar(Pagina.Lista);

    }


    #endregion


    #region Metodos Ayuda


    bool ValidarDatos()
    {
        if (string.IsNullOrWhiteSpace(txtConsecutivo.Text) || string.IsNullOrWhiteSpace(txtDescripcion.Text) || string.IsNullOrWhiteSpace(txtPonderado.Text)
            || string.IsNullOrWhiteSpace(ddlClaseConcepto.SelectedValue) || string.IsNullOrWhiteSpace(ddlTipConcepto.SelectedValue) || string.IsNullOrWhiteSpace(ddlTipo.SelectedValue)
            || string.IsNullOrWhiteSpace(rbdUnidadConcepto.SelectedValue))
        {
            VerError("Faltan campos por validar!.");
            return false;
        }

        return true;
    }

    ConceptosNomina ObtenerDatos()
    {
        ConceptosNomina conceptoNomina = new ConceptosNomina();

        conceptoNomina.DESCRIPCION = txtDescripcion.Text;
        conceptoNomina.CLASE = Convert.ToInt32(ddlClaseConcepto.SelectedItem.Value);
        conceptoNomina.TIPO_CONCEPTO = Convert.ToInt32(ddlTipConcepto.SelectedItem.Value);
        conceptoNomina.UNIDAD_CONCEPTO = Convert.ToInt32(rbdUnidadConcepto.SelectedItem.Value);

        if (rbdProvisonExtralegalNo.Checked == true)
        {
            conceptoNomina.provisiona_extralegal = 0;

            conceptoNomina.porcentajeprovisionextralegal = 0;
        }
        if (rbdProvisonExtralegalSi.Checked == true)
        {
            conceptoNomina.provisiona_extralegal = 1;
            conceptoNomina.porcentajeprovisionextralegal = ConvertirStringToDecimal(txtPorcentaje.Text);

        }


        // if (txtConceptos.Text != null)
        // {

        conceptoNomina.FORMULA = txtConceptos.Text;
        if (txtConceptos.Text != "")
        {
            conceptoNomina.FORMULA = conceptoNomina.FORMULA.Remove(conceptoNomina.FORMULA.Length - 2);
        }
        //}

        conceptoNomina.PONDERADO = Convert.ToDecimal(txtPonderado.Text);
        conceptoNomina.tipo = Convert.ToInt64(ddlTipo.SelectedValue);



        //RECUPERAR DATOS DE  LOS CONCEPTOS BASE


        return conceptoNomina;


    }

    string ObtenerFiltro()
    {
        string filtro = string.Empty;
        filtro += " and tipo=1 and clase=1";


        return filtro;
    }


    string ObtenerFiltro2()
    {
        string filtro = string.Empty;
        if (txtConceptos.Text != null)
        {
            filtro += "and consecutivo in (" + txtConceptos.Text + ")";
        }
        return filtro;
    }

    protected void cbListado_CheckedChanged(object sender, EventArgs e)
    {
        txtConceptos.Text = "";
        foreach (GridViewRow item in gvConceptosBase.Rows)
        {
            CheckBox cbListado = (CheckBox)item.FindControl("cbListado");
            if (cbListado != null)
            {
                if (cbListado.Checked)
                {
                    Label lconsecutivo = (Label)item.FindControl("lbl_consecutivo");
                    if (lconsecutivo != null)
                    {
                        txtConceptos.Text += lconsecutivo.Text + ",";
                    }
                }
            }
        }
    }



    #endregion


    protected void rbdProvisonExtralegal_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void rbdProvisonExtralegal_TextChanged(object sender, EventArgs e)
    {

    }



    protected void rbdProvisonExtralegalSi_CheckedChanged(object sender, EventArgs e)
    {
        if (rbdProvisonExtralegalSi.Checked)
        {
            txtPorcentaje.Visible = true;
            lblporcentaje.Visible = true;
        }
        else
        {
            txtPorcentaje.Visible = false;
            lblporcentaje.Visible = false;
        }
    }

    protected void rbdProvisonExtralegalNo_CheckedChanged(object sender, EventArgs e)
    {
        if (rbdProvisonExtralegalNo.Checked)
        {
            txtPorcentaje.Visible = false;
            lblporcentaje.Visible = false;
        }
        else
        {
            txtPorcentaje.Visible = true;
            lblporcentaje.Visible = true;
        }
    }
}