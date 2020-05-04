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

partial class Lista : GlobalWeb
{

    PolizasSegurosService polizasSegurosServicio = new PolizasSegurosService();
   
    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
       {

            VisualizarOpciones(polizasSegurosServicio.CodigoPrograma.ToString(), "L");
           
            Site toolBar = (Site)this.Master;



            toolBar.eventoRegresar += btnAtras_Click;
            
            toolBar.eventoConsultar += btnConsultar_Click;

           

            //if (Convert.ToString(Session["Nombres"]) != "")
            //    ((Label)Master.FindControl("lblNombresApellidos")).Text = Session["Nombres"].ToString();
            //if (Convert.ToString(Session["Identificacion"]) != "")
            //    ((Label)Master.FindControl("lblIdCliente")).Text = Session["Identificacion"].ToString();

            //ImageButton btnAdelante = Master.FindControl("btnAdelante") as ImageButton;
            //ImageButton btnAtras = Master.FindControl("btnAtras") as ImageButton;

            // btnAdelante.ValidationGroup = "";


            //if (Session["TipoCredito"] != null)
            //{
            //    // Esto es para cuando se llama desde la solicitu para tipos de crédito de CONSUMO y MICROCREDITO
            //    if (Session["TipoCredito"].ToString() == "C")
            //        btnAdelante.ImageUrl = "~/Images/btnGuardar.jpg";
            //    else
            //        btnAdelante.ImageUrl = "~/Images/btnCapturaDocumentos.jpg";
            //}
            //else
            //{
            //    btnAdelante.Visible = false;
            //    btnAtras.Visible = false;
            //}



        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(polizasSegurosServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    
    protected void Page_Load(object sender, EventArgs e)
    {
        txtCodigoPoliza.Visible = false;
        TxtNumeroRadicación.Visible = false;
        TxtPrimerNombre.Visible = false;
        LabelCódigo_poli.Visible = false;
        LabelNombres.Visible = false;
        LabelNúmero_Radicación.Visible = false;
        lblTotalRegs.Visible = false;
        try
        {
            if (!IsPostBack)
            {

                CargarValoresConsulta(pConsulta, polizasSegurosServicio.CodigoPrograma);
                if (Session[polizasSegurosServicio.CodigoPrograma + ".consulta"] != null)
                Actualizar();
            }
        }
        catch (Exception ex)
        {

            BOexcepcion.Throw(polizasSegurosServicio.CodigoPrograma, "Page_Load", ex);
        }


    }
    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
          Page.Validate();

          if (Page.IsValid)
          {
              GuardarValoresConsulta(pConsulta, polizasSegurosServicio.CodigoPrograma);
              Actualizar();
          }
    }


    protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    {
        Session[polizasSegurosServicio.CodigoPoliza + ".id"] = null;
        
        
            GuardarValoresConsulta(pConsulta, polizasSegurosServicio.GetType().Name);
           // Navegar(Pagina.Nuevo);
            Navegar("~/Page/FabricaCreditos/PolizasSeguros/Filtrar.aspx");


            Session["Beneficiarios"] = null;
            Session["FamiliaresPolizas"] = null;         
            Session["operacion"] = "N";
        
       
    }
    protected void btnEditar_Click(object sender, ImageClickEventArgs e)
    {
        Session[polizasSegurosServicio.CodigoPoliza + ".id"] = idObjeto;
        Navegar(Pagina.Nuevo);

    }


    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
       // LimpiarValoresConsulta(pConsulta, polizasSegurosServicio.GetType().Name);
        LimpiarValoresConsulta(pConsulta, polizasSegurosServicio.CodigoPrograma);
    }

         
    private void Actualizar()
    {
        try
        {

             List<PolizasSeguros> lstConsulta = new List<PolizasSeguros>();
        
             PolizasSeguros poliza = new PolizasSeguros();
           
             String filtro = obtFiltro(ObtenerValores());
            
          
             if (RadioModificar.Checked == true)
             {
                 txtCodigoPoliza.Visible = true;
                 TxtNumeroRadicación.Visible = true;
                 TxtPrimerNombre.Visible = true;
                 LabelNúmero_Radicación.Visible = true;
                 LabelNombres.Visible = true;
                 LabelCódigo_poli.Visible = true;
                 lblTotalRegs.Visible = true;
                 try
                 {
                     lstConsulta = polizasSegurosServicio.ListarPolizasSeguros(poliza, (Usuario)Session["usuario"], filtro);//, (UserSession)Session["user"]);
                 }
                 catch (Exception ex)
                 {
                     this.Lblerror.Text = ex.Message;
                     return;
                 }
                 Lblerror.Text = "";
                 gvPolizasSeguros.PageSize = pageSize;
                 String emptyQuery = "Fila de datos vacia";
                 gvPolizasSeguros.EmptyDataText = emptyQuery;
                 gvPolizasSeguros.DataSource = lstConsulta;

                 if (lstConsulta.Count > 0)
                 {
                     gvPolizasSeguros.Visible = true;
                     lblInfo.Visible = false;
                     lblTotalRegs.Visible = true;
                     lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                     gvPolizasSeguros.DataBind();
                     ValidarPermisosGrilla(gvPolizasSeguros);
                 }
                 else
                 {
                     gvPolizasSeguros.Visible = false;
                     lblInfo.Visible = true;
                     lblTotalRegs.Visible = false;
                 }

             }
             if (RadioNuevo.Checked == true)
             {
                 TxtNumeroRadicación.Visible = true;
                 TxtPrimerNombre.Visible = true;
                 LabelNombres.Visible = true;
                 LabelNúmero_Radicación.Visible = true;
                 lblTotalRegs.Visible = true;
                 try
                 {
                     lstConsulta = polizasSegurosServicio.FiltrarCredito(poliza, (Usuario)Session["usuario"],filtro);//, (UserSession)Session["user"]);
                 }
                 catch (Exception ex)
                 {
                     this.Lblerror.Text = ex.Message;
                     return;
                 }
                 Lblerror.Text = "";
                 gvPolizassinSeguros.PageSize = pageSize;
                 String emptyQuery = "Fila de datos vacia";
                 gvPolizassinSeguros.EmptyDataText = emptyQuery;
                 gvPolizassinSeguros.DataSource = lstConsulta;

                 if (lstConsulta.Count > 0)
                 {
                     gvPolizassinSeguros.Visible = true;
                     lblInfo.Visible = false;
                     lblTotalRegs.Visible = true;
                     lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                     gvPolizassinSeguros.DataBind();
                     ValidarPermisosGrilla(gvPolizassinSeguros);
                 }
                 else
                 {
                     gvPolizasSeguros.Visible = false;
                     lblInfo.Visible = true;
                     lblTotalRegs.Visible = false;
                 }


                 MultiView1.SetActiveView(View2);
             }


             Session.Add(polizasSegurosServicio.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(polizasSegurosServicio.CodigoPrograma , "Actualizar", ex);
        }
    }
   // private PolizasSeguros  ObtenerValores()
   // {
      //  PolizasSeguros entityPolizasSeguros = new PolizasSeguros();

      //entityPolizasSeguros.cod_poliza = txtCodigoPoliza.Text.Trim();
      // // entityPolizasSeguros.cod_poliza = idOf;
      //  return entityPolizasSeguros;

    private PolizasSeguros ObtenerValores()
    {
        PolizasSeguros polizasSeguros = new PolizasSeguros();

        if (txtCodigoPoliza.Text.Trim() != "")        
            polizasSeguros.cod_poliza = Convert.ToInt64("0" + txtCodigoPoliza.Text.Trim());
         
        if(this.TxtNumeroRadicación.Text.Trim() != "")
            polizasSeguros.numero_radicacion = Convert.ToInt64("0" + TxtNumeroRadicación.Text.Trim());

        if (this.TxtPrimerNombre.Text.Trim() != "")
            polizasSeguros.primer_nombre = Convert.ToString(TxtPrimerNombre.Text.Trim());

       


        return polizasSeguros;
    }
   protected void gvPolizasSeguros_SelectedIndexChanged(object sender, EventArgs e)
    {
        String id = gvPolizasSeguros.SelectedRow.Cells[2].Text;
         Session[polizasSegurosServicio.CodigoPoliza + ".id"] = id;
        Navegar(Pagina.Detalle);

    }
    protected void gvPolizasSeguros_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvPolizasSeguros.Rows[e.NewEditIndex].Cells[2].Text;
        Session[polizasSegurosServicio.CodigoPoliza + ".id"] = id;
        //Session["PolizasSeguros.from"] = "l";
        Session["operacion"] = "";
        Navegar(Pagina.Editar);
    }
    protected void gvPolizasSeguros_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
       
        try
        {
            Int32 idObjeto1 = Convert.ToInt32(gvPolizasSeguros.Rows[e.RowIndex].Cells[3].Text);
            polizasSegurosServicio.EliminarPolizasSeguros(idObjeto1, (Usuario)Session["usuario"]);
           Actualizar();
            Navegar(Pagina.Lista);
        }
        catch (Xpinn.Util.ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
         //   BOexcepcion.Throw(polizasSegurosServicio.CodigoPoliza + "L", "gvPolizasSeguros_RowDeleting", ex);
            BOexcepcion.Throw(polizasSegurosServicio.GetType().Name + "L", "gvPolizasSeguros_RowDeleting", ex);
        }
    }
    
    protected void gvPolizasSeguros_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        ConfirmarEliminarFila(e, "btnEliminar");
    }
    protected void gvPolizasSeguros_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvPolizasSeguros.PageIndex = e.NewPageIndex;
            Actualizar();
        }
        catch (Exception ex)
        {
           // BOexcepcion.Throw(polizasSegurosServicio.GetType().Name + "L", "gvPolizasSeguros_PageIndexChanging", ex);
            BOexcepcion.Throw(polizasSegurosServicio.CodigoPrograma, "gvPolizasSeguros_PageIndexChanging", ex);
        }
 

    }
    private string obtFiltro(PolizasSeguros poliza)
    {
        String filtro = String.Empty;
        if (txtCodigoPoliza.Text.Trim() != "")
            filtro += " and cod_poliza like '%" + poliza.cod_poliza+"%'";
        if  (TxtNumeroRadicación.Text.Trim() != "")
            filtro += " and numero_radicacion like '%" + poliza.numero_radicacion +"%'";
        if (TxtPrimerNombre.Text.Trim() != "")
            filtro += " and NOM_PERONSA like '%" + poliza.primer_nombre + "%'";
       
        if (!string.IsNullOrEmpty(filtro))
        {
            filtro = filtro.Substring(4);
            filtro = "where " + filtro;
        }
        return filtro;
    }
    
    protected void btnAtras_Click(object sender, ImageClickEventArgs e)
    {
        if (Session["TipoCredito"]!=null)
        if (Session["TipoCredito"].ToString() == "C")
        {
            Session["GarantiaReal"] = "3";
            Response.Redirect("~/Page/FabricaCreditos/Solicitud/DatosSolicitud/SolicitudCredito.aspx");
        }            
        else
            Response.Redirect("~/Page/FabricaCreditos/Garantias/Lista.aspx");
    }

    protected void btnAdelante_Click(object sender, ImageClickEventArgs e)
    {
        if (Session["TipoCredito"].ToString() == "C")
        {
            Session["GarantiaReal"] = "2";  //redirecciona para solicitar garantia real
            Response.Redirect("~/Page/FabricaCreditos/Solicitud/DatosSolicitud/SolicitudCredito.aspx");
        }
            
        else
            Response.Redirect("~/Page/FabricaCreditos/Solicitud/CapturaDocumentos/DocumentosAnexos/Lista.aspx");
    }
    protected void RadioNuevo_CheckedChanged(object sender, EventArgs e)
    {
        Session["Beneficiarios"] = null;
        Session["FamiliaresPolizas"] = null;
        Session["operacion"] = "N";
        RadioModificar.Checked = false;
        RadioNuevo.Checked = true;
        MultiView1.SetActiveView(View2);
        Session[polizasSegurosServicio.CodigoPoliza + ".id"] = null;
        Session["operacion"] = "N";
    }
    protected void RadioModificar_CheckedChanged(object sender, EventArgs e)
    {
        RadioModificar.Checked = true;
        RadioNuevo.Checked = false;
        MultiView1.SetActiveView(View1);
    }

    protected void TxtPrimerNombre_TextChanged(object sender, EventArgs e)
    {

    }
    protected void gvPolizassinSeguros_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void gvPolizassinSeguros_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvPolizasSeguros.PageIndex = e.NewPageIndex;
            Actualizar();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(polizasSegurosServicio.CodigoPrograma, "gvFiltro_PageIndexChanging", ex);
        }
    }
}
