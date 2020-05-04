using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.ConciliacionBancaria.Services;
using Xpinn.ConciliacionBancaria.Entities;
using Xpinn.Util;
using System.IO;
using System.Configuration;
using System.Globalization;
using System.Text;
using System.Web.UI.HtmlControls;

public partial class Nuevo : GlobalWeb
{
    Xpinn.ConciliacionBancaria.Services.ConciliacionBancariaService estructuraService = new Xpinn.ConciliacionBancaria.Services.ConciliacionBancariaService();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(estructuraService.CodigoPrograma, "L");

            
            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
            ctlMensaje.eventoClick += btnContinuarMen_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(estructuraService.GetType().Name + "L", "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                mvAplicar.ActiveViewIndex = 0;
                CargarDropDown();
                Session["DetalleCarga"] = null;
                
               
                if (Session[estructuraService.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[estructuraService.CodigoPrograma + ".id"].ToString();
                    Session.Remove(estructuraService.CodigoPrograma + ".id");
                    ObtenerDatos(idObjeto);
                   
                }
                else
                {      
                    InicializargvDetalle();
                }
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(estructuraService.GetType().Name + "L", "Page_Load", ex);
        }

    }

    public void PoblarLista(string pTabla, DropDownList ddlControl)
    {
        PoblarLista(pTabla, "", "", "", ddlControl);
    }

    public void PoblarLista(string pTabla, string pColumnas, string pCondicion, string pOrden, DropDownList ddlControl)
    {
        List<Xpinn.Comun.Entities.ListaDesplegable> plista = new List<Xpinn.Comun.Entities.ListaDesplegable>();
        Xpinn.Comun.Entities.ListaDesplegable pentidad = new Xpinn.Comun.Entities.ListaDesplegable();
        Xpinn.Comun.Services.ListaDeplegableService pservicio = new Xpinn.Comun.Services.ListaDeplegableService();

        ddlControl.Items.Clear();
        plista = pservicio.ListarListaDesplegable(pentidad, pTabla, pColumnas, pCondicion, pOrden, (Usuario)Session["usuario"]);
        pentidad.idconsecutivo = null;
        pentidad.descripcion = "Seleccione un item";
        plista.Insert(0, pentidad);
        ddlControl.DataTextField = "descripcion";
        ddlControl.DataValueField = "idconsecutivo";
        ddlControl.DataSource = plista;
        ddlControl.DataBind();
    }

    void CargarDropDown()
    {
        PoblarLista("bancos", ddlBancos);
    }

    protected void InicializargvDetalle()
    {
        List<ConceptoBancario> lstDetalle = new List<ConceptoBancario>();
        for (int i = gvDetalle.Rows.Count; i < 5; i++)
        {
            ConceptoBancario eDetCarga = new ConceptoBancario();
            eDetCarga.cod_banco = -1;
            //eCuenta.cod_empresa = -1;
            eDetCarga.descripcion = null;
            eDetCarga.tipo_concepto = null;
            
            lstDetalle.Add(eDetCarga);
        }
        gvDetalle.DataSource = lstDetalle;
        gvDetalle.DataBind();

        Session["DetalleCarga"] = lstDetalle;
      
    }

    protected void gvDetalle_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
        }
    }
    


    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            
             
            


            ConceptoBancario vRecaudos = new ConceptoBancario();



           

            //RECUPERAR DATOS - GRILLA PROGRAMACION-CONCEPTO
            ConceptoBancario lstDetalle =new  ConceptoBancario();
            List<Xpinn.ConciliacionBancaria.Entities.ConceptoBancario> lstAccesos = new List<Xpinn.ConciliacionBancaria.Entities.ConceptoBancario>();

           lstAccesos= estructuraService.Listarconceptobancario("",lstDetalle, (Usuario)Session["usuario"]);
           if (lstAccesos.Count > 0)
            {
                if ((lstDetalle != null) || (lstAccesos.Count != 0))
                {
                    gvDetalle.DataSource = lstAccesos;
                    gvDetalle.DataBind();
                }
                Session["DetalleCarga"] = lstAccesos;
            }
            else
            {
                InicializargvDetalle();
            }

           foreach (GridViewRow rfila in gvDetalle.Rows)
           {
               Label concepto = (Label)rfila.FindControl("lblconceptobancario");
               int conseID = Convert.ToInt32(concepto.Text);

               //ObtenerListaDetalle();

               List<ConceptoBancario> LstDetalle = new List<ConceptoBancario>();
               LstDetalle = (List<ConceptoBancario>)Session["DetalleCarga"];

               ConceptoBancario traercuentabancaria = new ConceptoBancario();
               foreach (ConceptoBancario acti in LstDetalle)
               {


                   if (conseID > 0)
                   {
                       traercuentabancaria = estructuraService.ConsultarConciliacionBancaria(conseID, (Usuario)Session["usuario"]);
                       break;
                   }


               }
               DropDownList ddlconcepto = (DropDownList)rfila.FindControl("ddlconcepto");
               ddlconcepto.SelectedValue = traercuentabancaria.tipo_concepto;
               ddlBancos.SelectedValue = Convert.ToString(traercuentabancaria.cod_banco);
           }

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(estructuraService.CodigoPrograma, "ObtenerDatos", ex);
        }
    }



    protected void btnFinal_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }


    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }

    public Boolean ValidarDatos()
    {
        if (ddlBancos.SelectedIndex == 0)
        {
            VerError("Seleccione un Banco");
            return false;
        }
         return true;
    }

    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        if (ValidarDatos())
        {
            string msj = idObjeto != "" ? "modificar" : "grabar";
            ctlMensaje.MostrarMensaje("Esta seguro de " + msj + " los datos ingresados?");
        }
    }


   protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        try
        {           
            foreach (GridViewRow rfila in gvDetalle.Rows)
            {

                TextBoxGrid txtcod_campo = (TextBoxGrid)rfila.FindControl("txtcod_campo");
                TextBox txtdescripcion = (TextBox)rfila.FindControl("txtdescripcion");
                DropDownList ddlconcepto = (DropDownList)rfila.FindControl("ddlconcepto");
                Label concepto = (Label)rfila.FindControl("lblconceptobancario");
                ConceptoBancario guardarconcepto = new ConceptoBancario();
                if (ddlBancos.SelectedValue != "0" & ddlBancos.SelectedValue != "")
                {
                    guardarconcepto.cod_banco = Convert.ToInt32(ddlBancos.SelectedValue);
                }
                if (txtdescripcion.Text != null & txtdescripcion.Text != "")
                guardarconcepto.descripcion = txtdescripcion.Text;
                if (ddlconcepto.SelectedValue != "0" & ddlconcepto.SelectedValue!="")
                {
                    guardarconcepto.tipo_concepto = Convert.ToString(ddlconcepto.SelectedValue);
                    if (txtcod_campo.Text != null & txtcod_campo.Text != "")
                    guardarconcepto.cod_concepto = Convert.ToString(txtcod_campo.Text);
                }
                if (concepto.Text != null & concepto.Text != "")
                    guardarconcepto.id_conceptobancario = Convert.ToInt32(concepto.Text);

                if (guardarconcepto.id_conceptobancario == null || guardarconcepto.id_conceptobancario == 0)                     //           CREAR
                {
                    if (guardarconcepto.tipo_concepto != null & guardarconcepto.cod_concepto != null & guardarconcepto.descripcion != null)
                    {
                        estructuraService.CrearConciliacionBancaria(guardarconcepto, (Usuario)Session["usuario"]);
                    }
                }
                else
                {
                    if (guardarconcepto.tipo_concepto != null & guardarconcepto.cod_concepto!=null& guardarconcepto.descripcion!=null)
                    {
                        estructuraService.ModificarConciliacionBancaria(guardarconcepto, (Usuario)Session["usuario"]);
                    }
                }
            }
        }
        catch (Exception ex)
        {
           VerError("DIGITE UN CODIGO DIFERENTE ESE YA ESTA EN USO");
        }
        
    }


    protected void btnAgregar_Click(object sender, EventArgs e)
    {
        ///ObtenerListaDetalle();
        List<ConceptoBancario> LstPrograma = new List<ConceptoBancario>();
        if (Session["DetalleCarga"] != null)
        {
            LstPrograma = (List<ConceptoBancario>)Session["DetalleCarga"];

            for (int i = 1; i <= 1; i++)
            {
                ConceptoBancario pDetalle = new ConceptoBancario();
                pDetalle.cod_banco = -1;
                pDetalle.cod_banco = -1;
                pDetalle.descripcion = null;
                pDetalle.tipo_concepto = null;
                LstPrograma.Add(pDetalle);
            }
            gvDetalle.PageIndex = gvDetalle.PageCount;
            gvDetalle.DataSource = LstPrograma;
            gvDetalle.DataBind();

            Session["DetalleCarga"] = LstPrograma;
        }
    }

    protected void gvDetalle_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        foreach (GridViewRow rfila in gvDetalle.Rows)
        {
            Label concepto = (Label)rfila.FindControl("lblconceptobancario");
            int conseID = Convert.ToInt32(concepto.Text);

            //ObtenerListaDetalle();

            List<ConceptoBancario> LstDetalle = new List<ConceptoBancario>();
            LstDetalle = (List<ConceptoBancario>)Session["DetalleCarga"];

            try
            {

                foreach (ConceptoBancario acti in LstDetalle)
                {


                    if (conseID > 0)
                    {
                        estructuraService.EliminarConciliacionBancaria(conseID, (Usuario)Session["usuario"]);
                        LstDetalle.Remove(acti);
                        break;
                    }

                }
            }
            catch (Xpinn.Util.ExceptionBusiness ex)
            {
                VerError(ex.Message);
            }
            catch (Exception ex)
            {
                BOexcepcion.Throw(estructuraService.CodigoPrograma, "gvDetalle_RowDeleting", ex);
            }

            Session["DetalleCarga"] = LstDetalle;

            gvDetalle.DataSourceID = null;
            gvDetalle.DataBind();
            gvDetalle.DataSource = LstDetalle;
            gvDetalle.DataBind();
        }
    }




    

}
