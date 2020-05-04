using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Aportes.Services;
using Xpinn.Aportes.Entities;
using Xpinn.Util;
using System.IO;
using System.Configuration;
using System.Globalization;
using System.Text;
using System.Web.UI.HtmlControls;

public partial class Nuevo : GlobalWeb
{
    CiudadServices CiudadService = new CiudadServices();
    Oficina_ciudadService objOficina = new Oficina_ciudadService();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            if (Session[CiudadService.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(CiudadService.CodigoPrograma, "E");
            else
                VisualizarOpciones(CiudadService.CodigoPrograma, "A");
            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
            ctlMensaje.eventoClick += btnContinuarMen_Click;

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CiudadService.GetType().Name + "L", "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                mvAplicar.ActiveViewIndex = 0;
                cargarCombos();
                
                if (Session[CiudadService.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[CiudadService.CodigoPrograma + ".id"].ToString();
                    Session.Remove(CiudadService.CodigoPrograma + ".id");
                    ObtenerDatos(idObjeto);
                    txtCodigo.Enabled = false;
                }
                else
                {
                    txtCodigo.Enabled = true;
                }
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CiudadService.GetType().Name + "L", "Page_Load", ex);
        }
    }

    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            Ciudad vDatos = new Ciudad();

            vDatos = CiudadService.ConsultarCiudad(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);

            if (vDatos.codciudad != 0)
                txtCodigo.Text = HttpUtility.HtmlDecode(vDatos.codciudad.ToString().Trim());
                ddlCiudad.SelectedValue = vDatos.tipo.ToString();
            if (vDatos.depende_de != null)
                ddlOficina.SelectedValue = vDatos.depende_de.ToString();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CiudadService.CodigoPrograma, "ObtenerDatos", ex);
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
        if (txtCodigo.Text.Trim() == "")
        {
            VerError("Ingrese el Codigo correspondiente");
            return false;
        }
        
        if (ddlOficina.SelectedIndex == 0)
        {
            VerError("Seleccione La Oficina");
            return false;
        }

        if (ddlCiudad.SelectedIndex == 0)
        {
            VerError("Seleccione La ciudad");
            return false;
        }      
        
         return true;
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        VerError("");
        if (ValidarDatos())
        {
            string msj = idObjeto != "" ? "Modificar" : "Grabar";
            ctlMensaje.MostrarMensaje("Desea " + msj + " los datos ingresados?");
        }
    }


    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        try
        {
            Usuario User = (Usuario)Session["usuario"];
            
            Oficina_ciudad vData = new Oficina_ciudad();
            vData.codciudad = Convert.ToInt64(txtCodigo.Text);
            vData.cod_oficina = Convert.ToInt64(ddlOficina.SelectedValue);
            vData.codciudad = Convert.ToInt64(ddlCiudad.SelectedValue);

            if (idObjeto != "")
            {
                //MODIFICAR
                objOficina.ModificarOficina_ciudad(vData, (Usuario)Session["usuario"]);
            }
            else
            {
                Oficina_ciudad vDatos = new Oficina_ciudad();
                vDatos = objOficina.validaOficinaGuardaServices((Usuario)Session["usuario"], vData, 3);// que esa oficina no tenga la misma Ciudad

                if (vDatos.codciudad != 0 && vDatos.cod_oficina !=0 )
                {
                    VerError("Esta Oficina ya Tiene esta Ciudad Asignada");
                    return;
                }
                else
                {//CREAR
                    vDatos = objOficina.validaOficinaGuardaServices((Usuario)Session["usuario"], vData, 2);// que la ciudad no este ocupada
                    if (vDatos.codciudad != 0 && vDatos.cod_oficina != 0)
                    {
                        VerError("La Ciudad ya esta asignada a Otra Oficina");
                        return;
                    }
                    else
                    {
                        objOficina.CrearOficina_ciudad(vData, (Usuario)Session["usuario"]);
                    }

                }
            }
            Site toolBar = (Site)this.Master;
            toolBar.MostrarGuardar(false);
            string msj = idObjeto != "" ? "Modificado" : "Grabado";
            mvAplicar.ActiveViewIndex = 1;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CiudadService.CodigoPrograma, "btnContinuar_Click", ex);
        }
    }

    //[System.Web.Services.WebMethod]
    //public static Boolean validaOficina(Int64 codOfi, Int64 codCiu) 
    //{

    //}

    void cargarCombos()
    {
        PoblarListas poblar = new PoblarListas();
        List<Oficina_ciudad> Listddl = new List<Oficina_ciudad>();
        Oficina_ciudad obj = new Oficina_ciudad();
        Listddl = objOficina.ListarOficina_ciudad(obj, (Usuario)Session["usuario"]);

        if (Listddl.Count > 0)
        {
            Listddl.Insert(0, new Oficina_ciudad { Nombre_Oficina = "Seleccione", cod_oficina = -1 });
            foreach (var item in Listddl)
            {
                ddlOficina.Items.Add(item.Nombre_Oficina.ToString());
                ddlOficina.Items.FindByText(item.Nombre_Oficina.ToString()).Value = item.cod_oficina.ToString();
            }
        }
        poblar.PoblarListaDesplegable("ciudades", ddlCiudad, (Usuario)Session["usuario"]);
    }
}


