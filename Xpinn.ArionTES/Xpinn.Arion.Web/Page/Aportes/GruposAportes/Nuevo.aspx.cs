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

public partial class Nuevo : GlobalWeb
{
    Xpinn.FabricaCreditos.Services.Persona1Service DatosClienteServicio = new Xpinn.FabricaCreditos.Services.Persona1Service();
    private Xpinn.Aportes.Services.GrupoLineaAporteServices GrupoAporteServicio = new Xpinn.Aportes.Services.GrupoLineaAporteServices();

    List<Xpinn.FabricaCreditos.Entities.Persona1> lstDatosSolicitud = new List<Xpinn.FabricaCreditos.Entities.Persona1>();  //Lista de los menus desplegables
    
    String operacion = "";
    
    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(GrupoAporteServicio.CodigoPrograma, "L");

            Site toolBar = (Site)this.Master;
             toolBar.eventoGuardar += btnGuardar_Click;
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
                //idObjeto = Session[GrupoAporteServicio.CodigoPrograma.ToString() + ".id"].ToString();
                //txtIdGrupo.Text = idObjeto;
                // TablaLineas();
                Session["LineaAportes"] = null;
                CargarDropDown();

                operacion = (String)Session["operacion"];
                if (operacion == "N")
                {
                    ConsultarMaxGrupoAporte();
                    InicialLinea();
                }
                if (Session[GrupoAporteServicio.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[GrupoAporteServicio.CodigoPrograma.ToString() + ".id"].ToString();
                    txtIdGrupo.Text = idObjeto;
                    Session.Remove(GrupoAporteServicio.CodigoPrograma.ToString() + ".id");
                    InicialLinea();

                    ObtenerValoresLineas();
                    if (operacion == null)
                    {
                        txtIdGrupo.Enabled = false;
                        TablaLineas();
                        ddlTipoDistribucion_SelectedIndexChanged(ddlTipoDistribucion, null);
                    }
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
        ddlTipoDistribucion.Items.Insert(0, new ListItem("Por Porcentaje","1"));
        ddlTipoDistribucion.Items.Insert(1, new ListItem("Por Tope SMLMV", "2"));
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

    protected List<GrupoLineaAporte> ListaAporte()
    {
        Xpinn.Aportes.Services.GrupoLineaAporteServices LineaAporteServicio = new Xpinn.Aportes.Services.GrupoLineaAporteServices();

        GrupoLineaAporte aporte = new GrupoLineaAporte();
        List<GrupoLineaAporte> LstAporte = new List<GrupoLineaAporte>();
        LstAporte = (List<GrupoLineaAporte>)Session["LineaAportes"];

        //  aporte.cod_linea_aporte = 0;
        // aporte.nombre = "";
        // LstAporte.Add(aporte);
        return LstAporte;

    }
    protected void LlenarComboLineaAporte(DropDownList DdlLineaAporte)
    {
        //DdlLineaAporte.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));
        AporteServices aporteService = new AporteServices();
        Usuario usuap = (Usuario)Session["usuario"];
        Aporte aporte = new Aporte();
        DdlLineaAporte.DataSource = aporteService.ListarLineaAporte(aporte, (Usuario)Session["usuario"]);
        DdlLineaAporte.DataTextField = "nom_linea_aporte";
        DdlLineaAporte.DataValueField = "cod_linea_aporte";
        DdlLineaAporte.DataBind();
        //DdlLineaAporte.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));

    }

    protected void gvLista_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        this.Lblerror.Text = "";
        Boolean result;
        List<GrupoLineaAporte> lstLinea = new List<GrupoLineaAporte>();
        lstLinea = (List<GrupoLineaAporte>)Session["LineaAportes"];
        Xpinn.Aportes.Services.GrupoLineaAporteServices LineaAporteServicio = new Xpinn.Aportes.Services.GrupoLineaAporteServices();
        Xpinn.Aportes.Entities.GrupoLineaAporte vlinea = new Xpinn.Aportes.Entities.GrupoLineaAporte();
        //Xpinn.Aportes.Entities.GrupoAporte grupoAporte = new Xpinn.Aportes.Entities.GrupoAporte();
        CheckBox chkprincipal = (CheckBox)gvLista.FooterRow.FindControl("ChkPpal");

        if (e.CommandName.Equals("AddNew"))
        {
            DropDownList Ddlcodigolinea = (DropDownList)gvLista.FooterRow.FindControl("DdlLineaAporte");

            if (lstLinea.Count == 1)
            {
                GrupoLineaAporte gItem = new GrupoLineaAporte();
                gItem = lstLinea[0];
                if (gItem.cod_linea_aporte == 0)
                    lstLinea.Remove(gItem);
            }
            vlinea = LineaAporteServicio.ConsultarGrupoAporte(Convert.ToInt64(Ddlcodigolinea.SelectedValue), (Usuario)Session["usuario"]);

            if ((vlinea.cod_linea_aporte != 0 && vlinea.idgrupo != 0))
            {

                String Error = "La linea ya pertenece al grupo " + vlinea.idgrupo;
                this.Lblerror.Text = Error;
                result = false;
                return;
            }
            else
            {
                vlinea = LineaAporteServicio.ConsultarLineaAporte(Convert.ToInt64(Ddlcodigolinea.SelectedValue), (Usuario)Session["usuario"]);
                GrupoLineaAporte gItemNew = new GrupoLineaAporte();

                gItemNew.idgrupo = Convert.ToInt64(txtIdGrupo.Text);
                gItemNew.cod_linea_aporte = vlinea.cod_linea_aporte;
                gItemNew.nombre = vlinea.nombre;
                gItemNew.porcentaje = vlinea.porcentaje;

                if (chkprincipal.Checked)
                {
                    vlinea.principal = 1;
                }
                else
                {
                    vlinea.principal = 0;
                }

                gItemNew.principal = vlinea.principal;

                if (gItemNew.porcentaje == 0)
                {
                    String Error = "EL porcentaje no puede ser 0";
                    this.Lblerror.Text = Error;
                    result = false;
                }

                if (this.validarreglas(vlinea))
                {
                    List<GrupoLineaAporte> LstLineaAporte = new List<GrupoLineaAporte>();
                    LstLineaAporte = (List<GrupoLineaAporte>)Session["LineaAportes"];
                    int val = 0;
                    if (LstLineaAporte != null)
                    {
                        //primer regla suma de porcentajes...
                        foreach (GrupoLineaAporte aporte in LstLineaAporte)
                        {
                            if (aporte.principal == 1)
                                val++;
                            if (aporte.cod_linea_aporte == Convert.ToInt64(Ddlcodigolinea.SelectedValue))
                            {
                                String Error = "La linea ya se adicionó al grupo " + txtIdGrupo.Text;
                                this.Lblerror.Text = Error;
                                result = false;
                                return;
                            }
                        }
                    }
                    if (chkprincipal.Checked == false && val == 0)
                    {
                        String Error = "Por favor marque uno como principal";
                        this.Lblerror.Text = Error;
                        result = false;
                        // btnGuardar1.Visible = false;
                    }

                    gvLista.EditIndex = -1;
                    lstLinea.Add(gItemNew);
                    gvLista.DataSource = lstLinea;
                    gvLista.DataBind();
                    Session["LineaAportes"] = lstLinea;

                    //this.GrupoAporteServicio.CrearGrupoAporte(gItemNew, (Usuario)Session["usuario"]);

                    if (chkprincipal.Checked == true)
                    {
                        String Error = "";
                        this.Lblerror.Text = Error;
                        result = false;
                        //  btnGuardar1.Visible = true;
                    }

                }
            }            
        }
        if (e.CommandName.Equals("Update"))
        {
            
        }
        //if (e.CommandName.Equals("Delete"))
        //{
        //    List<GrupoLineaAporte> lstGrupos = new List<GrupoLineaAporte>();
        //    lstGrupos = (List<GrupoLineaAporte>)Session["LineaAportes"];
        //    if (lstGrupos.Count >= 1)
        //    {
        //        GrupoLineaAporte eLinea = new GrupoLineaAporte();
        //        int index = Convert.ToInt32(e.CommandArgument);
        //        eLinea = lstGrupos[index];
        //        if (eLinea.cod_linea_aporte != 0)
        //            lstGrupos.Remove(eLinea);
        //        Int64 id = eLinea.cod_linea_aporte;
        //        GrupoAporte = id;
        //    }



        //    if (lstGrupos.Count == 0)
        //    {
        //        InicialLinea();
        //    }
        //    else
        //    {

        //        gvLista.DataSource = lstGrupos;
        //        gvLista.DataBind();
        //        Session["LineaAportes"] = lstGrupos;

        //    }


        //}

        //  TablaLineas();
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
        quitarfilainicial();
    }
    private Xpinn.Aportes.Entities.GrupoLineaAporte ObtenerValoresLineas()
    {
        Xpinn.Aportes.Entities.GrupoLineaAporte vLinea = new Xpinn.Aportes.Entities.GrupoLineaAporte();

        if (idObjeto != "")
            vLinea.cod_linea_aporte = Convert.ToInt64(idObjeto.ToString());

        return vLinea;

    }

    private Xpinn.Aportes.Entities.GrupoLineaAporte ObtenerValores()
    {
        Xpinn.Aportes.Entities.GrupoLineaAporte vLinea = new Xpinn.Aportes.Entities.GrupoLineaAporte();

        if (idObjeto != "")
            vLinea.idgrupo = Convert.ToInt64(idObjeto.ToString());

        return vLinea;

    }
    

    private void TablaLineas()
    {
        try
        {
            List<Xpinn.Aportes.Entities.GrupoLineaAporte> lstConsulta = new List<Xpinn.Aportes.Entities.GrupoLineaAporte>();
            lstConsulta = GrupoAporteServicio.ListarGrupoAporteDetalle(ObtenerValores(), (Usuario)Session["usuario"]);
            if (lstConsulta.Count > 0)
            {
                if (lstConsulta[0].tipo_distribucion != 0)
                    ddlTipoDistribucion.SelectedValue = lstConsulta[0].tipo_distribucion.ToString();
            }            
 
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
               // lblTotalRegs.Text = "No hay lineas para este producto";
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
                    totalPorcentaje = Convert.ToDecimal(aporte.porcentaje + totalPorcentaje);
                    contar++;
                }

                //valido que la suma del porcentaje no sea diferente a 100
                if ((totalPorcentaje + lineasaporte.porcentaje) > 100)
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
        int contPrincipal = 0;
        int contar = 0;

        List<GrupoLineaAporte> LstLineaAporte = new List<GrupoLineaAporte>();
        LstLineaAporte = (List<GrupoLineaAporte>)Session["LineaAportes"];
        this.Lblerror.Text = "";
        if (txtIdGrupo.Text.Trim() == "")
        {
            VerError("ingrese el grupo");
            result = false;
        }
        if (LstLineaAporte != null)
        {
            //primer regla suma de porcentajes...
            foreach (GrupoLineaAporte aporte in LstLineaAporte)
            {
                if (aporte.cod_linea_aporte > 0)
                {
                    totalPorcentaje = Convert.ToDecimal(aporte.porcentaje + totalPorcentaje);
                    contar++;
                }
                if (aporte.principal == 1)
                    contPrincipal++;
            }
        }

        if (ddlTipoDistribucion.SelectedIndex == 0)
        {
            //valido que la suma del porcentaje no sea diferente a 100
            if ((totalPorcentaje) > 100)
            {
                String Error = "Supera el 100% del porcentaje";
                this.Lblerror.Text = Error;
                result = false;

            }
            if ((totalPorcentaje) < 100)
            {
                String Error = "Inferior el 100% del porcentaje";
                this.Lblerror.Text = Error;
                result = false;
            }
        }
        if (contPrincipal != 1)
        {
            String Error = "Por favor marque UNO como principal";
            this.Lblerror.Text = Error;
            result = false;
        }

        return result;
    }
    

    private void GuardarGruposAportes(long conseid)
    {
        Boolean result = true;
        try
        {
            GrupoLineaAporte lineaaporte = new GrupoLineaAporte();
            Usuario usuap = new Usuario();
            List<GrupoLineaAporte> LstGrupo = new List<GrupoLineaAporte>();
            List<GrupoLineaAporte> LstGrupoAporte = new List<GrupoLineaAporte>();
            LstGrupoAporte = (List<GrupoLineaAporte>)Session["LineaAportes"];
            foreach (GrupoLineaAporte grupoaporte in LstGrupoAporte)
            {               
                grupoaporte.idgrupo = Convert.ToInt64(txtIdGrupo.Text);      
                grupoaporte.tipo_distribucion = Convert.ToInt32(ddlTipoDistribucion.SelectedValue);
                if (grupoaporte.idgrupo != null)
                {
                    if (grupoaporte.cod_linea_aporte != null)
                    {
                        if (grupoaporte.cod_linea_aporte > 0)
                        {
                            LstGrupo.Add(grupoaporte);
                        }
                    }
                }
                else
                {
                    LstGrupo.Add(grupoaporte);
                }
            }

            Navegar(Pagina.Lista);      
            if(idObjeto == "")
                this.GrupoAporteServicio.CrearGrupoAporte(LstGrupo, (Usuario)Session["usuario"]);
            else
                this.GrupoAporteServicio.ModificarGrupoAporte(LstGrupo, (Usuario)Session["usuario"]);
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(GrupoAporteServicio.GetType().Name, "btnGuardar_Click", ex);
        }
    }



    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        int conseID = Convert.ToInt32(gvLista.DataKeys[e.NewEditIndex].Values[0].ToString());
        // Session["Cod_linea_Aportes"] = conseID;
        if (conseID != 0)
        {
            gvLista.EditIndex = e.NewEditIndex;

            List<GrupoLineaAporte> LstGrupoAporte = new List<GrupoLineaAporte>();
            LstGrupoAporte = (List<GrupoLineaAporte>)Session["LineaAportes"];

            gvLista.DataSource = LstGrupoAporte;
            gvLista.DataBind();
            //this.ConsultarGrupoAporte();

            //String linea = "";
            //linea = this.buscarLinea(conseID);
            //DropDownList DdlLineaAporteEdit = new DropDownList();
            //DdlLineaAporteEdit = gvLista.Rows[e.NewEditIndex].Cells[2].FindControl("DdlLineaAporteEdit") as DropDownList;

            //if (DdlLineaAporteEdit != null)
            //{
            //    DdlLineaAporteEdit.SelectedValue = linea;
            //}
        }
        else
        {
            e.Cancel = true;
        }
    }

    private String buscarLinea(Int64 idconse)
    {
        String linea = "";
        List<GrupoLineaAporte> LstGrupoLineaAporte = new List<GrupoLineaAporte>();
        LstGrupoLineaAporte = (List<GrupoLineaAporte>)Session["LineaAportes"];
        if (LstGrupoLineaAporte != null)
        {
            foreach (GrupoLineaAporte grupo in LstGrupoLineaAporte)
            {
                if (grupo.cod_linea_aporte == idconse)
                {
                    linea = grupo.nombre;
                }
            }
        }
        return linea;
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
                //this.gvLista.Rows[0].Cells[6].Visible = false;
            }
        }
        catch
        {
        }
    }

    protected void gvLista_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Footer)
        {

            Control ctrl = e.Row.FindControl("DdlLineaAporte");
            if (ctrl != null)
            {
                DropDownList dd = ctrl as DropDownList;
                LlenarComboLineaAporte(dd);
            }
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ImageButton btned = (ImageButton)e.Row.FindControl("btnEditar");

            if (operacion == "N")
            {

                if (btned != null)
                {
                    btned.Visible = false;
                }
            }

            if (operacion == "")
            {
                if (btned != null)
                {
                    btned.Visible = true;
                }
            }

            try
            {
                ConfirmarEliminarFila(e, "btnBorrar");
            }
            catch (Exception ex)
            {
                BOexcepcion.Throw(GrupoAporteServicio.CodigoPrograma + "L", "gvLista_RowDataBound", ex);
            }
        }

        List<GrupoLineaAporte> LstGrupoAporte;

        DropDownList DdlLineaAporteEdit = (DropDownList)e.Row.FindControl("DdlLineaAporteEdit");

        if (DdlLineaAporteEdit != null)
        {
            LstGrupoAporte = this.ListaAporte(); if (operacion != "N")
            {

                ImageButton btned = (ImageButton)e.Row.FindControl("btnEditar");
                //btned.Visible = true;
            }
            DdlLineaAporteEdit.DataSource = LstGrupoAporte;
            DdlLineaAporteEdit.DataTextField = "COD_LINEA_APORTE";
            DdlLineaAporteEdit.DataValueField = "COD_LINEA_APORTE";
            // DdlLineaAporteEdit.SelectedValue = "";
            // ddlparentescoedit.DataBind();
        }
    }

    private void eliminarGrupo_Aporte(int conseID)
    {
        Xpinn.Aportes.Services.GrupoLineaAporteServices LineaAporteServicio = new Xpinn.Aportes.Services.GrupoLineaAporteServices();
        Xpinn.Aportes.Entities.GrupoLineaAporte grupoaporte = new Xpinn.Aportes.Entities.GrupoLineaAporte();

        List<GrupoLineaAporte> LstGrupoAporte;
        LstGrupoAporte = (List<GrupoLineaAporte>)Session["LineaAportes"];
        foreach (GrupoLineaAporte grupoaportes in LstGrupoAporte)
        {
            if (grupoaportes.cod_linea_aporte == conseID)
            {
                LstGrupoAporte.Remove(grupoaportes);
                break;
            }
        }
        Session["LineaAportes"] = LstGrupoAporte;
        MostrarDatosAportes();
    }

    private void MostrarDatosGrupoAportes(String cod_linea, GridView gvLista, String Var)
    {

        GrupoLineaAporte pGrupoAporte = new GrupoLineaAporte();
        List<GrupoLineaAporte> LstGrupoAporte = new List<GrupoLineaAporte>();
        String codaporte = Convert.ToString(Session["Cod_linea_Aportes"]);

        //pGrupoAporte.cod_linea_aporte = Convert.ToInt64("0" + codaporte);

        pGrupoAporte.cod_linea_aporte = Convert.ToInt64("0" + codaporte);
        LstGrupoAporte = GrupoAporteServicio.ListarGrupoAporte(pGrupoAporte, (Usuario)Session["usuario"]);

        if (LstGrupoAporte.Count == 0)
        {
            crearGrupoinicial(0, "GrupoLineaAporte");
            LstGrupoAporte = (List<GrupoLineaAporte>)Session["LineaAportes"];
        }

        gvLista.DataSource = LstGrupoAporte;
        gvLista.DataBind();
        Session[Var] = LstGrupoAporte;
    }

    private void ConsultarGrupoAporte()
    {
        crearGrupoinicial(0, "GrupoAporte");

        String codaporte = Convert.ToString(Session["Cod_linea_Aportes"]);
        //Convert.ToInt64("0" + txtIdGrupo.Text).ToString();
        // codaporte = Convert.ToString(Session["Cod_linea_Aportes"]);
        if (codaporte != "0")
        {
            MostrarDatosGrupoAportes("cod_linea", gvLista, "GrupoLineaAporte");
        }
        else
        {
            MostrarDatosGrupoAporteNuevo("cod_linea", gvLista, "GrupoLineaAporte");
        }

        //quitarfilainicialbeneficiarios();
    }

    private void MostrarDatosGrupoAporteNuevo(String cod_linea, GridView gvLista, String Var)
    {

        GrupoLineaAporte pGrupoAporte = new GrupoLineaAporte();
        List<GrupoLineaAporte> LstGrupoAporte = new List<GrupoLineaAporte>();

        String codaporte = Convert.ToString(Session["Cod_linea_Aportes"]);
        pGrupoAporte.cod_linea_aporte = Convert.ToInt64("0" + codaporte);

        if (Convert.ToInt64(pGrupoAporte.cod_linea_aporte) > 0)
        {
            LstGrupoAporte = GrupoAporteServicio.ListarGrupoAporte(pGrupoAporte, (Usuario)Session["usuario"]);
        }
        else
        {
            LstGrupoAporte = (List<GrupoLineaAporte>)Session["LineaAportes"];
        }
        if (LstGrupoAporte.Count == 0)
        {
            crearGrupoinicial(0, "LineaAportes");
            LstGrupoAporte = (List<GrupoLineaAporte>)Session["LineaAportes"];
        }

        gvLista.DataSource = LstGrupoAporte;
        gvLista.DataBind();
        Session[Var] = LstGrupoAporte;
    }

    private void crearGrupoinicial(int consecutivo, String nombresession)
    {
        GrupoLineaAporte pGrupoAporte = new GrupoLineaAporte();
        List<GrupoLineaAporte> LstGrupoAporte = new List<GrupoLineaAporte>();

        pGrupoAporte.cod_linea_aporte = consecutivo;
        pGrupoAporte.nombre = "";
        pGrupoAporte.porcentaje = 0;

        LstGrupoAporte.Add(pGrupoAporte);
        Session[nombresession] = LstGrupoAporte;
    }

    private void MostrarDatosAportes()
    {
        List<GrupoLineaAporte> LstGrupoAporte = new List<GrupoLineaAporte>();

        TablaLineas();
        LstGrupoAporte = (List<GrupoLineaAporte>)Session["LineaAportes"];
        if ((LstGrupoAporte == null) || (LstGrupoAporte.Count == 0))
        {
            crearGrupoinicial(0, "LineaAportes");
            LstGrupoAporte = (List<GrupoLineaAporte>)Session["LineaAportes"];
        }

        gvLista.DataSource = LstGrupoAporte;
        gvLista.DataBind();
        quitarfilainicial();
    }


    protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            int rowIndex = e.RowIndex;
            List<GrupoLineaAporte> lstLinea = new List<GrupoLineaAporte>();
            lstLinea = (List<GrupoLineaAporte>)Session["LineaAportes"];

            lstLinea.RemoveAt(rowIndex);
            Int64 id = Convert.ToInt64(e.Keys[0]);
            GrupoAporteServicio.EliminarGrupoAporte(Convert.ToInt64(txtIdGrupo.Text), id, (Usuario)Session["usuario"]);
            gvLista.DataSource = lstLinea;
            gvLista.DataBind();
        }
        catch (Xpinn.Util.ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(GrupoAporteServicio.CodigoPrograma, "gvLista_RowDeleting", ex);
        }
    }
    
    protected void DdlLineaAporte_SelectedIndexChanged(object sender, EventArgs e)
    {
        Decimal porcentaje;
        Xpinn.Aportes.Services.GrupoLineaAporteServices LineaAporteServicio = new Xpinn.Aportes.Services.GrupoLineaAporteServices();
        Xpinn.Aportes.Entities.GrupoLineaAporte vlinea = new Xpinn.Aportes.Entities.GrupoLineaAporte();
        DropDownList Ddlcodigolinea = (DropDownList)gvLista.FooterRow.FindControl("DdlLineaAporte");

        vlinea = LineaAporteServicio.ConsultarGrupoAporte(Convert.ToInt64(Ddlcodigolinea.SelectedValue), (Usuario)Session["usuario"]);
        porcentaje = vlinea.porcentaje;
        Lblerror.Text = "EL PORCENTAJE DE ESTA LINEA ES DE:" + Convert.ToString(porcentaje);
    }

    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        List<GrupoLineaAporte> lstLinea;

        GrupoLineaAporte grupoaporte = new GrupoLineaAporte();
        lstLinea = (List<GrupoLineaAporte>)Session["LineaAportes"];

        if (this.validarreglasgrabar())
        {
            GuardarGruposAportes(grupoaporte.idgrupo);
        }
    }


    protected void btnCancelar_Click(object sender, ImageClickEventArgs e)
    {

        if (idObjeto == "")
        {
            Navegar(Pagina.Lista);
        }
        else
        {
            Session[GrupoAporteServicio.CodigoPrograma + ".id"] = idObjeto;
            Session["Cod_linea_Aportes"] = null;
            Session["LineaAportes"] = null;
            Session["operacion"] = "";
            Navegar(Pagina.Lista);
        }
    }

    protected void gvLista_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvLista.EditIndex = -1;
        List<GrupoLineaAporte> lstLinea = new List<GrupoLineaAporte>();
        lstLinea = (List<GrupoLineaAporte>)Session["LineaAportes"];
        int rowIndex = Convert.ToInt32(e.RowIndex);

        GrupoLineaAporte gItemNew = new GrupoLineaAporte();
        gItemNew = lstLinea[rowIndex];

        Label lblValor = (Label)gvLista.Rows[rowIndex].FindControl("lblValorAnt");
        DropDownList DdlLineaAporteEdit = (DropDownList)gvLista.Rows[rowIndex].FindControl("DdlLineaAporteEdit");
        Label lblLinea = (Label)gvLista.Rows[rowIndex].FindControl("lblLinea");
        //Capturando datos a modificar
        gItemNew.idgrupo = Convert.ToInt64(txtIdGrupo.Text);
        gItemNew.tipo_distribucion = Convert.ToInt32(ddlTipoDistribucion.SelectedValue);
        gItemNew.cod_linea_aporte = Convert.ToInt64(DdlLineaAporteEdit.SelectedItem.Text);
        gItemNew.nombre = lblLinea.Text != "" ? lblLinea.Text.Trim() : null;
        gItemNew.porcentaje = Convert.ToDecimal(lblValor.Text.Replace(".", ""));

        gvLista.EditIndex = -1;
        gvLista.DataSource = lstLinea;
        gvLista.DataBind();

        //long codgrupoaporte = Convert.ToInt64("0" + txtIdGrupo.Text);
        //ConsultarGrupoAporte(); 
    }

    protected void gvLista_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        int rowIndex = e.RowIndex;
        List<GrupoLineaAporte> lstLinea = new List<GrupoLineaAporte>();
        lstLinea = (List<GrupoLineaAporte>)Session["LineaAportes"];
        GrupoLineaAporte gItemNew = new GrupoLineaAporte();
        gItemNew = lstLinea[rowIndex];

        decimalesGridRow txtValor = (decimalesGridRow)gvLista.Rows[rowIndex].FindControl("txtValor");
        txtValor.Text = txtValor.Text == "" ? "0" : txtValor.Text.Trim();
        DropDownList DdlLineaAporteEdit = (DropDownList)gvLista.Rows[rowIndex].FindControl("DdlLineaAporteEdit");
        Label lblLinea = (Label)gvLista.Rows[rowIndex].FindControl("lblLinea");
        //Capturando datos a modificar
        gItemNew.idgrupo = Convert.ToInt64(txtIdGrupo.Text);
        gItemNew.tipo_distribucion = Convert.ToInt32(ddlTipoDistribucion.SelectedValue);
        gItemNew.cod_linea_aporte = Convert.ToInt64(DdlLineaAporteEdit.SelectedItem.Text);
        gItemNew.nombre = lblLinea.Text != "" ? lblLinea.Text.Trim() : null;
        gItemNew.porcentaje = Convert.ToDecimal(txtValor.Text.Replace(".", ""));

        gvLista.EditIndex = -1;
        gvLista.DataSource = lstLinea;
        gvLista.DataBind();
    }

    protected void ddlTipoDistribucion_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlTipoDistribucion.SelectedIndex == 0)
            {
                gvLista.HeaderRow.Cells[4].Text = "Porcentaje"; 
            }
            else
            {
                gvLista.HeaderRow.Cells[4].Text = "Valor";
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(GrupoAporteServicio.GetType().Name, "ddlTipoDistribucion_SelectedIndexChanged", ex);
        }
    }
}
