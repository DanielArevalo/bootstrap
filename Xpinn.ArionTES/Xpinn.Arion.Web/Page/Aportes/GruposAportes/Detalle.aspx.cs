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
using Xpinn.Comun.Services;
using Xpinn.Comun.Entities;
using Xpinn.Aportes.Services;
using Xpinn.Aportes.Entities;
using System.Data.Common;

public partial class Detalle : GlobalWeb
{
    Xpinn.FabricaCreditos.Services.Persona1Service DatosClienteServicio = new Xpinn.FabricaCreditos.Services.Persona1Service();
    private Xpinn.Aportes.Services.GrupoLineaAporteServices GrupoAporteServicio = new Xpinn.Aportes.Services.GrupoLineaAporteServices();

    List<Xpinn.FabricaCreditos.Entities.Persona1> lstDatosSolicitud = new List<Xpinn.FabricaCreditos.Entities.Persona1>();  //Lista de los menus desplegables
    String ListaSolicitada = null;  // Cadena en la que se indica la lista a solicitar

    String lineaaporte = "";


    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(GrupoAporteServicio.CodigoPrograma, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoEditar += btnEditar_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(GrupoAporteServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                Session["LineaAportes"] = null;
                CargarDropDown();
                ConsultarMaxGrupoAporte();
                InicialLinea();
                if (Session[GrupoAporteServicio.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[GrupoAporteServicio.CodigoPrograma.ToString() + ".id"].ToString();
                    txtIdGrupo.Text = idObjeto;
                    Session.Remove(GrupoAporteServicio.CodigoPrograma.ToString() + ".id");
                    InicialLinea();
                    //quitarfilainicial();
                    ObtenerValoresLineas();
                    TablaLineas();
                }
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(GrupoAporteServicio.CodigoPrograma, "Page_Load", ex);
        }
    }

    protected void CargarDropDown()
    {
        ddlTipoDistribucion.Items.Insert(0, new ListItem("Por Porcentaje", "1"));
        ddlTipoDistribucion.Items.Insert(1, new ListItem("Por Tope SMLMV", "2"));
    }

    protected void btnEditar_Click(object sender, ImageClickEventArgs e)
    {
        Session[GrupoAporteServicio.CodigoPrograma + ".id"] = idObjeto;

        Navegar(Pagina.Nuevo);
        Session["operacion"] = null;
    }

    protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    {
        GuardarValoresConsulta(pConsulta, GrupoAporteServicio.CodigoPrograma);
        Navegar(Pagina.Nuevo);
    }

    public void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        GuardarValoresConsulta(pConsulta, GrupoAporteServicio.CodigoPrograma);
        Navegar(Pagina.Lista);
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        LimpiarValoresConsulta(pConsulta, GrupoAporteServicio.CodigoPrograma);

    }

    protected void gvLista_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        List<GrupoLineaAporte> lstLinea = new List<GrupoLineaAporte>();
        lstLinea = (List<GrupoLineaAporte>)Session["LineaAportes"];
        Xpinn.Aportes.Services.GrupoLineaAporteServices LineaAporteServicio = new Xpinn.Aportes.Services.GrupoLineaAporteServices();
        Xpinn.Aportes.Entities.GrupoLineaAporte vlinea = new Xpinn.Aportes.Entities.GrupoLineaAporte();
        //Xpinn.Aportes.Entities.GrupoAporte grupoAporte = new Xpinn.Aportes.Entities.GrupoAporte();
        CheckBox chkprincipal= (CheckBox)gvLista.FooterRow.FindControl("ChkPpal");
        if (e.CommandName.Equals("AddNew"))
        {
            TextBox txtcodigolinea = (TextBox)gvLista.FooterRow.FindControl("txtcodigo");

            if (lstLinea.Count == 1)
            {
                GrupoLineaAporte gItem = new GrupoLineaAporte();
                gItem = lstLinea[0];
                if (gItem.cod_linea_aporte == 0)
                    lstLinea.Remove(gItem);
            }
            vlinea = LineaAporteServicio.ConsultarGrupoAporte(Convert.ToInt64(txtcodigolinea.Text), (Usuario)Session["Usuario"]);



            if ((vlinea.cod_linea_aporte != 0))
            {

                String Error = "Linea ya pertenece al grupo   " + vlinea.idgrupo;
                this.Lblerror.Text = Error;
            }
            else
            {
                vlinea = LineaAporteServicio.ConsultarLineaAporte(Convert.ToInt64(txtcodigolinea.Text), (Usuario)Session["Usuario"]);
                GrupoLineaAporte gItemNew = new GrupoLineaAporte();

                gItemNew.idgrupo = Convert.ToInt64(txtIdGrupo.Text);
                gItemNew.cod_linea_aporte = vlinea.cod_linea_aporte;
                gItemNew.nombre = vlinea.nombre;
                gItemNew.porcentaje_distrib = vlinea.porcentaje_distrib;
              
                if (chkprincipal.Checked)
                     {
                         vlinea.principal = 1;
                    }
                gItemNew.principal = vlinea.principal;
                if (gItemNew.porcentaje_distrib == 0)
                {
                    String Error = "EL porcentaje no puede ser 0";
                    this.Lblerror.Text = Error;

                }
                if (this.validarreglas(vlinea))
                {

                    gvLista.EditIndex = -1;
                    lstLinea.Add(gItemNew);
                    gvLista.DataSource = lstLinea;
                    gvLista.DataBind();
                    Session["LineaAportes"] = lstLinea;



                }
            }
        }
        if (e.CommandName.Equals("Delete"))
        {
            if (lstLinea.Count >= 1)
            {
                GrupoLineaAporte eLinea = new GrupoLineaAporte();
                int index = Convert.ToInt32(e.CommandArgument);
                eLinea = lstLinea[index];
                if (eLinea.cod_linea_aporte != 0)
                    lstLinea.Remove(eLinea);
            }



            if (lstLinea.Count == 0)
            {
                InicialLinea();
            }
            else
            {

                gvLista.DataSource = lstLinea;
                gvLista.DataBind();
                Session["LineaAportes"] = lstLinea;

            }


        }


    }

    /// <summary>
    ///  Método para insertar un registro en la grilla cuando no hay Lineas
    /// </summary>
    protected void InicialLinea()
    {

        List<Xpinn.Aportes.Entities.GrupoLineaAporte> lstConsulta = new List<Xpinn.Aportes.Entities.GrupoLineaAporte>();
        Xpinn.Aportes.Entities.GrupoLineaAporte eLinea = new Xpinn.Aportes.Entities.GrupoLineaAporte();
        lstConsulta.Add(eLinea);
        Session["LineaAportes"] = lstConsulta;
        gvLista.DataSource = lstConsulta;
        gvLista.DataBind();
        gvLista.Visible = true;
    }
    private Xpinn.Aportes.Entities.GrupoLineaAporte ObtenerValoresLineas()
    {
        Xpinn.Aportes.Entities.GrupoLineaAporte vLinea = new Xpinn.Aportes.Entities.GrupoLineaAporte();

        if (idObjeto != "")
            vLinea.idgrupo= Convert.ToInt64(idObjeto.ToString());

        return vLinea;

    }


    private void TablaLineas()
    {
        try
        {
            List<Xpinn.Aportes.Entities.GrupoLineaAporte> lstConsulta = new List<Xpinn.Aportes.Entities.GrupoLineaAporte>();
            lstConsulta = GrupoAporteServicio.ListarGrupoAporteDetalle(ObtenerValoresLineas(), (Usuario)Session["usuario"]);

            if (lstConsulta[0].tipo_distribucion != 0)
                ddlTipoDistribucion.SelectedValue = lstConsulta[0].tipo_distribucion.ToString();

            gvLista.PageSize = 5;
            gvLista.EmptyDataText = "No se encontraron registros";
            gvLista.DataSource = lstConsulta;

            if (lstConsulta.Count > 0)
            {
                gvLista.Visible = true;
                lblTotalRegs.Visible = false;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                gvLista.DataBind();
                ValidarPermisosGrilla(gvLista);
                Session["LineaAportes"] = lstConsulta;
            }
            else
            {
                idObjeto = "";
                gvLista.Visible = false;
                lblTotalRegs.Text = "No hay lineas para este crédito";
                lblTotalRegs.Visible = true;
                InicialLinea();

            }

            Session.Add(GrupoAporteServicio.CodigoPrograma + ".consulta", 1);
        }
            
        catch (Exception ex)
        {
            BOexcepcion.Throw(GrupoAporteServicio.CodigoPrograma, "Actualizar", ex);
        }
       
    }
    protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvLista.PageIndex = e.NewPageIndex;
            TablaLineas();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(GrupoAporteServicio.CodigoPrograma, "gvLista_PageIndexChanging", ex);
        }

    }


    private Boolean validarreglas(GrupoLineaAporte lineasaporte)
    {
        Boolean result = true;
        decimal totalPorcentaje = 0;
        int contar = 0;

        List<GrupoLineaAporte> LstLineaAporte = new List<GrupoLineaAporte>();
        LstLineaAporte = (List<GrupoLineaAporte>)Session["LineaAportes"];
        this.Lblerror.Text = "";
        if (LstLineaAporte != null)
        {
            //primer regla suma de porcentajes...
            foreach (GrupoLineaAporte aporte in LstLineaAporte)
            {
                if (aporte.cod_linea_aporte > 0)
                {
                    totalPorcentaje = Convert.ToDecimal(aporte.porcentaje_distrib + totalPorcentaje);

                    contar++;
                }


                //valido que la suma del porcentaje no sea diferente a 100
                if ((totalPorcentaje + lineasaporte.porcentaje_distrib) > 100)
                {
                    String Error = "Supera el 100% del porcentaje";
                    this.Lblerror.Text = Error;
                    result = false;

                }




            }

        }
        return result;
    }
    private Boolean validarreglasgrabar()
    {
        Boolean result = true;
        decimal totalPorcentaje = 0;
        int contar = 0;

        List<GrupoLineaAporte> LstLineaAporte = new List<GrupoLineaAporte>();
        LstLineaAporte = (List<GrupoLineaAporte>)Session["LineaAportes"];
        this.Lblerror.Text = "";
        if (LstLineaAporte != null)
        {
            //primer regla suma de porcentajes...
            foreach (GrupoLineaAporte aporte in LstLineaAporte)
            {
                if (aporte.cod_linea_aporte > 0)
                {
                    totalPorcentaje = Convert.ToDecimal(aporte.porcentaje_distrib + totalPorcentaje);

                    contar++;
                }


                //valido que la suma del porcentaje no sea diferente a 100
                if ((totalPorcentaje) > 100)
                {
                    String Error = "Supera el 100% del porcentaje";
                    this.Lblerror.Text = Error;
                    result = false;

                }

                //valido que la suma del porcentaje no sea diferente a 100


            }
        }

        if ((totalPorcentaje) < 100)
        {
            String Error = "Inferior el 100% del porcentaje";
            this.Lblerror.Text = Error;
            result = false;
        }



        return result;
    }



    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {

        int conseID = -99;

        //Convert.ToInt32(gvLista.DataKeys[e.NewEditIndex].Values[0].ToString());
        if (conseID != 0)
        {
        }

    }
    private void ConsultarMaxGrupoAporte()
    {
        Int64 maxgrupoaporte = 0;
        Int64 numerogrupo = 1;
        Xpinn.Aportes.Services.GrupoLineaAporteServices LineaAporteServicio = new Xpinn.Aportes.Services.GrupoLineaAporteServices();
        Xpinn.Aportes.Entities.GrupoLineaAporte grupoaporte = new Xpinn.Aportes.Entities.GrupoLineaAporte();
        grupoaporte = LineaAporteServicio.ConsultarMaxGrupoAporte((Usuario)Session["usuario"]);

        if (!string.IsNullOrEmpty(grupoaporte.idgrupo.ToString()))
            maxgrupoaporte = grupoaporte.idgrupo + numerogrupo;
        this.txtIdGrupo.Text = Convert.ToInt64(maxgrupoaporte).ToString();

    }


    private void quitarfilainicial()
    {

        try
        {
            int conseID = Convert.ToInt32(gvLista.DataKeys[0].Values[0].ToString());
            if (conseID <= 0)
            {
                ImageButton link = (ImageButton)this.gvLista.Rows[0].Cells[0].FindControl("btnEditar");

                link.Enabled = false;

                link.Visible = false;

                this.gvLista.Rows[0].Cells[1].Text = "";
                this.gvLista.Rows[0].Cells[2].Text = "";
                this.gvLista.Rows[0].Cells[3].Visible = false;
                this.gvLista.Rows[0].Cells[4].Visible = false;
                this.gvLista.Rows[0].Cells[5].Text = "";
                this.gvLista.Rows[0].Cells[5].Visible = false;
                this.gvLista.Rows[0].Cells[6].Visible = false;
                //this.gvBeneficiarios.Rows[0].Cells[7].Visible = false;
                // this.gvBeneficiarios.Rows[0].Cells[8].Visible = false;


            }
        }
        catch
        {
        }

    }
}