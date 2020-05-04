using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using System.IO;
using System.Configuration;
using System.Globalization;
using System.Text;
using System.Web.UI.HtmlControls;
using Xpinn.NIIF.Entities;
using Xpinn.NIIF.Services;


public partial class Lista : GlobalWeb
{

    TransicionRangosNIFService TransRangos = new TransicionRangosNIFService();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            //VisualizarOpciones(TransRangos.CodigoPrograma, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoRegresar += btnRegresar_Click;
            ctlMensaje.eventoClick += btnContinuarMen_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(TransRangos.GetType().Name + "L", "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                Session["Transicion"] = null;
                mvAplicar.ActiveViewIndex = 0;
                Actualizar();
                //ObtenerDatos(idObjeto);
                //lblmsj.Text = "0";
                //lblmsj1.Text = "0"; 
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(TransRangos.GetType().Name + "L", "Page_Load", ex);
        }
    }


    protected void btnRegresar_Click(object sender, ImageClickEventArgs e)
    {
        Actualizar();
        Site toolBar = (Site)this.Master;
        toolBar.MostrarGuardar(true);
        mvAplicar.ActiveViewIndex = 0;
    }

    private void Actualizar()
    {
        try
        {
            List<TransicionRangosNIF> lstConsulta = new List<TransicionRangosNIF>();
            TransicionRangosNIF eTasa = new TransicionRangosNIF();

            lstConsulta = TransRangos.ListarTransicionRango(eTasa, (Usuario)Session["usuario"]);

            gvLista.PageSize = 15;
            String emptyQuery = "Fila de datos vacia";
            gvLista.EmptyDataText = emptyQuery;


            if (lstConsulta.Count > 0)
            {
                gvLista.DataSource = lstConsulta;
                Site toolBar = (Site)this.Master;
                //btnExportar.Visible = true;

                gvLista.Visible = true;
                lblTotalReg.Visible = true;
                lblTotalReg.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                gvLista.DataBind();               
                VerError("");
            }
            else
            {               
                gvLista.DataSource = null;                             
                lblTotalReg.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                InicializargvLista();
            }


            Session.Add(TransRangos.CodigoPrograma + ".consulta", 1);

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(TransRangos.CodigoPrograma, "ACTUALIZAR", ex);

        }
    }



    protected void InicializargvLista()
    {
        List<TransicionRangosNIF> lstProgra = new List<TransicionRangosNIF>();
        for (int i = gvLista.Rows.Count; i < 10; i++)
        {
            TransicionRangosNIF eCuenta = new TransicionRangosNIF();
            eCuenta.codrango = -1;           
            eCuenta.descripcion = "";
            eCuenta.dias_minimo = null;
            eCuenta.dias_maximo = null;
            
            lstProgra.Add(eCuenta);
        }
        gvLista.DataSource = lstProgra;
        gvLista.DataBind();

        Session["Transicion"] = lstProgra;        
    }



    protected void btnAdicionar_Click(object sender, EventArgs e)
    {
        ObtenerListaGrilla();
        List<TransicionRangosNIF> LstPrograma = new List<TransicionRangosNIF>();
        if (Session["Transicion"] != null)
        {
            LstPrograma = (List<TransicionRangosNIF>)Session["Transicion"];

            for (int i = 1; i <= 1; i++)
            {
                TransicionRangosNIF eCuenta = new TransicionRangosNIF();
                eCuenta.codrango = -1;
                eCuenta.descripcion = "";
                eCuenta.dias_minimo = null;
                eCuenta.dias_maximo = null;

                LstPrograma.Add(eCuenta);
            }
            gvLista.PageIndex = gvLista.PageCount;
            gvLista.DataSource = LstPrograma;
            gvLista.DataBind();

            Session["Transicion"] = LstPrograma;
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
   

    public Boolean ValidarDatos()
    {
        ObtenerListaGrilla();
        List<TransicionRangosNIF> LstTransi = new List<TransicionRangosNIF>();
        if (Session["Transicion"] != null)
        {
            LstTransi = (List<TransicionRangosNIF>)Session["Transicion"];
        }

        for (int i = 0; i < LstTransi.Count; i++)
        {
            try
            {
                if (LstTransi[i].dias_minimo != null && LstTransi[i].dias_maximo != null)
                {
                    if (LstTransi[i].dias_minimo > LstTransi[i].dias_maximo)
                    {
                        VerError("No se puede ingresar el Rango en la Fila : " + (i+1));
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                VerError(ex.Message);
                return false;
            }
        }    
         return true;
    }

    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        if (ValidarDatos())
        {
            Session["opcion"] = "GRABAR";
            
            ctlMensaje.MostrarMensaje("Desea Realizar la Grabación?");          
        }
    }



    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        try
        {
            if (Session["opcion"].ToString() == "GRABAR")
            {
                TransicionRangosNIF eTransi = new TransicionRangosNIF();

                eTransi.lstRangos = new List<TransicionRangosNIF>();
                eTransi.lstRangos = ObtenerListaGrilla();

                //MODIFICAR 0 CREAR
                TransRangos.ModificarTransicionRangos(eTransi, (Usuario)Session["usuario"]);

                lblmsj.Text = eTransi.num_grab.ToString();
                lblmsj1.Text = eTransi.num_modi.ToString();

                Session[TransRangos.CodigoPrograma + ".id"] = idObjeto;
                
                Site toolBar = (Site)this.Master;
                toolBar.MostrarGuardar(false);

                mvAplicar.ActiveViewIndex = 1;
            }

            
            if (Session["opcion"].ToString() == "ELIMINAR")
            {
                int newID = Convert.ToInt32(Session["ID"].ToString());

                ObtenerListaGrilla();

                List<TransicionRangosNIF> LstDetalle = new List<TransicionRangosNIF>();
                LstDetalle = (List<TransicionRangosNIF>)Session["Transicion"];
                if (newID > 0)
                {
                    try
                    {
                        foreach (TransicionRangosNIF acti in LstDetalle)
                        {
                            if (acti.codrango == newID)
                            {
                                TransRangos.EliminarTransicionRango(newID, (Usuario)Session["usuario"]);
                                LstDetalle.Remove(acti);
                                break;
                            }
                        }
                        Session["Transicion"] = LstDetalle;

                        gvLista.DataSourceID = null;
                        gvLista.DataBind();
                        gvLista.DataSource = LstDetalle;
                        gvLista.DataBind();

                    }
                    catch (Xpinn.Util.ExceptionBusiness ex)
                    {
                        VerError(ex.Message);
                    }
                    catch (Exception ex)
                    {
                        BOexcepcion.Throw(TransRangos.CodigoPrograma, "gvProgramacion_RowDeleting", ex);
                    }
                }
                else
                {
                    foreach (TransicionRangosNIF acti in LstDetalle)
                    {
                        if (acti.codrango == newID)
                        {
                            LstDetalle.Remove(acti);
                            break;
                        }
                    }
                    Session["Transicion"] = LstDetalle;

                    gvLista.DataSourceID = null;
                    gvLista.DataBind();
                    gvLista.DataSource = LstDetalle;
                    gvLista.DataBind();
                }
                Actualizar();
            }

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(TransRangos.CodigoPrograma, "btnContinuar_Click", ex);
        }
        
    }

    protected List<TransicionRangosNIF> ObtenerListaGrilla()
    {
        try
        {
            List<TransicionRangosNIF> lstTransi = new List<TransicionRangosNIF>();
            //lista para adicionar filas sin perder datos
            List<TransicionRangosNIF> lista = new List<TransicionRangosNIF>();

            foreach (GridViewRow rfila in gvLista.Rows)
            {
                TransicionRangosNIF eTransi = new TransicionRangosNIF();

                Label txtCodigo = (Label)rfila.FindControl("txtCodigo");
                if (txtCodigo != null)
                    eTransi.codrango = Convert.ToInt32(txtCodigo.Text);
                else
                    eTransi.codrango = -1;

                TextBox txtNombre = (TextBox)rfila.FindControl("txtNombre");
                if (txtNombre.Text != "")
                    eTransi.descripcion = txtNombre.Text;

                TextBox txtDiasMin = (TextBox)rfila.FindControl("txtDiasMin");
                if (txtDiasMin.Text != "")
                    eTransi.dias_minimo = Convert.ToInt32(txtDiasMin.Text);

                TextBox txtDiasMax = (TextBox)rfila.FindControl("txtDiasMax");
                if (txtDiasMax.Text != "")
                    eTransi.dias_maximo = Convert.ToInt32(txtDiasMax.Text);   
                
                lista.Add(eTransi);
                Session["Transicion"] = lista;

                if (eTransi.descripcion != null && eTransi.dias_minimo != null && eTransi.dias_maximo != null)
                {
                    lstTransi.Add(eTransi);
                }
            }
            return lstTransi;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(TransRangos.CodigoPrograma, "ObtenerListaGrilla", ex);          
            return null;
        }
    }




    protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int conseID = Convert.ToInt32(gvLista.DataKeys[e.RowIndex].Values[0].ToString());
        Session["opcion"] = "ELIMINAR";
        Session["ID"] = conseID;
        ctlMensaje.MostrarMensaje("Desea Realizar la Eliminación?");       
    }



}

