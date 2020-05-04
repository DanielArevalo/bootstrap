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
using Xpinn.Aportes.Services;
using Xpinn.Aportes.Entities;


public partial class Nuevo : GlobalWeb
{

    Par_Cue_LinApoervices ParametroService = new Par_Cue_LinApoervices();
    
    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            if (Session[ObtenerCodigoPrograma() + ".id"] != null)
                VisualizarOpciones(ObtenerCodigoPrograma(), "E");
            else
                VisualizarOpciones(ObtenerCodigoPrograma(), "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
            ctlMensaje.eventoClick += btnContinuarMen_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ParametroService.GetType().Name + "L", "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                mvAplicar.ActiveViewIndex = 0;
                txtCodigo.Enabled = false;
                CargarDropDown();
                ddlOpcion.Enabled = false;
                ddlOpcion.SelectedValue = Session["OpcionParam"].ToString();
                if (Session[ObtenerCodigoPrograma() + ".id"] != null)
                {
                    ddlOpcion.Enabled = false;
                    idObjeto = Session[ObtenerCodigoPrograma() + ".id"].ToString();
                    Session.Remove(ObtenerCodigoPrograma() + ".id");
                    if (Session["OpcionParam"].ToString() != null)
                        ObtenerDatos(idObjeto, Convert.ToInt32(Session["OpcionParam"].ToString()));
                }
                else
                {
                    ddlOpcion_SelectedIndexChanged(ddlOpcion, null);
                }                
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ObtenerCodigoPrograma() + "L", "Page_Load", ex);
        }
    }
    

    public string ObtenerCodigoPrograma()
    {
        string codPrograma = "";
        if (Session["OpcionParam"].ToString() != "" && Session["OpcionParam"] != null)
        {
            if (Session["OpcionParam"].ToString() == "1")
                codPrograma = ParametroService.CodigoProgramaActivi;
            else if (Session["OpcionParam"].ToString() == "2")
                codPrograma = ParametroService.CodigoProgramaTipoIden;
            else if (Session["OpcionParam"].ToString() == "3")
                codPrograma = ParametroService.CodigoProgramaCargo;
            else if (Session["OpcionParam"].ToString() == "4")
                codPrograma = ParametroService.CodigoProgramaNivEsco;
            else if (Session["OpcionParam"].ToString() == "5")
                codPrograma = ParametroService.CodigoProgramaEstaCiv;
            else if (Session["OpcionParam"].ToString() == "6")
                codPrograma = ParametroService.CodigoProgramaTipoCont;
            else if (Session["OpcionParam"].ToString() == "7")
                codPrograma = ParametroService.CodigoProgramaParent;
            else if (Session["OpcionParam"].ToString() == "8")
                codPrograma = ParametroService.CodigoProgramaTipoDoc;
        }
        return codPrograma;
    }


    void CargarDropDown()
    {
        ddlOpcion.Items.Insert(0, new ListItem("Seleccione un item","0"));
        ddlOpcion.Items.Insert(1, new ListItem("Actividad", "1"));
        ddlOpcion.Items.Insert(2, new ListItem("Tipo Identificación", "2"));
        ddlOpcion.Items.Insert(3, new ListItem("Cargo", "3"));
        ddlOpcion.Items.Insert(4, new ListItem("Nivel Escolaridad", "4"));
        ddlOpcion.Items.Insert(5, new ListItem("Estado Civil", "5"));
        ddlOpcion.Items.Insert(6, new ListItem("Tipo de Contrato", "6"));
        ddlOpcion.Items.Insert(7, new ListItem("Parentescos", "7"));
        ddlOpcion.Items.Insert(8, new ListItem("Tipo Documento", "8"));
        ddlOpcion.SelectedIndex = 0;
        ddlOpcion.DataBind();
    }

    protected void ObtenerDatos(String pIdObjeto,int opcion)
    {
        try
        {
            ParametrosAporte vDetalle = new ParametrosAporte();
            vDetalle = ParametroService.ConsultarParametrosAporte(pIdObjeto,opcion, (Usuario)Session["usuario"]);

            if (vDetalle.codigoStr != "" && vDetalle.codigoStr != null)
                txtCodigo.Text = vDetalle.codigoStr.ToString().Trim();            

            if (vDetalle.descripcion != "" && vDetalle.descripcion != null)
                txtDescripcion.Text = vDetalle.descripcion.ToString().Trim();

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ObtenerCodigoPrograma(), "ObtenerDatos", ex);
        }
    }


    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        string codPrograma = Request.QueryString["opcion"].ToString().Trim();
        Navegar("~/Page/Aportes/Parametros/Lista.aspx?opcion=" + codPrograma);
    }

    public Boolean ValidarDatos()
    {

        if (txtCodigo.Text == "")
        {
            VerError("Ingrese un Codigo");
            return false;
        }
        if (idObjeto == "")
        {
            if (ddlOpcion.SelectedIndex == 0)
            {
                VerError("Seleccione a que Tabla se realizara la Inserción");
                return false;
            }
        }
        if (txtDescripcion.Text == "")
        {
            VerError("Ingrese la Descripción correspondiente");
            return false;
        }
       
         return true;
    }

    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");

        if (ValidarDatos())
        {
            string msj = idObjeto != "" ? "modificar" : "registrar";
             ctlMensaje.MostrarMensaje("Desea " + msj + " los Datos Ingresados?");
        }
    }



    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        try
        {
            ParametrosAporte pVar = new ParametrosAporte();

            pVar.cod_opcion = Convert.ToInt32(ddlOpcion.SelectedValue);

            if (ddlOpcion.SelectedItem.Text == "Actividad") // si pertenece a la tabla actividad
            {
                pVar.codigoInt = 0;
                pVar.codigoStr = txtCodigo.Text;
            }
            else
            {
                pVar.codigoInt = Convert.ToInt32(txtCodigo.Text);
                pVar.codigoStr = null;
            }
            pVar.descripcion = txtDescripcion.Text;
                        
            if (idObjeto != "")
            {
                //MODIFICAR
                ParametroService.CrearPar_Cue_LinApo(pVar, (Usuario)Session["usuario"], 2);
            }
            else
            {
                ParametrosAporte vDetalle = new ParametrosAporte();
                vDetalle = ParametroService.ConsultarParametrosAporte(txtCodigo.Text,Convert.ToInt32(ddlOpcion.SelectedValue), (Usuario)Session["usuario"]);

                if (vDetalle.codigoStr != "" && vDetalle.codigoStr != null)
                {
                    ddlOpcion_SelectedIndexChanged(ddlOpcion, null);
                    if (ddlOpcion.SelectedItem.Text == "Actividad") // si pertenece a la tabla actividad
                    {
                        pVar.codigoInt = 0;
                        pVar.codigoStr = txtCodigo.Text;
                    }
                    else
                    {
                        pVar.codigoInt = Convert.ToInt32(txtCodigo.Text);
                        pVar.codigoStr = null;
                    }
                    ParametroService.CrearPar_Cue_LinApo(pVar, (Usuario)Session["usuario"], 1);                    
                }
                else
                {
                    //CREAR
                    ParametroService.CrearPar_Cue_LinApo(pVar, (Usuario)Session["usuario"], 1);
                }                
            }
            string codigo = "";
            codigo = ddlOpcion.SelectedItem.Text == "Actividad" ? pVar.codigoStr : pVar.codigoInt.ToString(); 
            
            lblMsj.Text = idObjeto != ""?"Se Modificaron correctamente los datos": "Se Grabaron Correctamente los datos ingresados. Codigo Nro: "+codigo;
            Site toolBar = (Site)Master;
            toolBar.MostrarGuardar(false);
            mvAplicar.ActiveViewIndex = 1;            
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ObtenerCodigoPrograma(), "btnContinuar_Click", ex);
        }        
    }


    protected void ddlOpcion_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlOpcion.SelectedIndex != 0)
        {
            txtCodigo.Text = ParametroService.ObtenerSiguienteCodigo(Convert.ToInt32(ddlOpcion.SelectedValue), (Usuario)Session["usuario"]).ToString();
        }
        else
            txtCodigo.Text = "";
    }
}
