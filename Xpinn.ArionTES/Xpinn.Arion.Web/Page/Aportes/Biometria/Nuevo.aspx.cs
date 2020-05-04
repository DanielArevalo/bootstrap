using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.Script.Services;
using System.Web.Services;
using System.Text;
using System.IO;
using System.Globalization;
using Xpinn.Aportes.Services;
using Xpinn.Aportes.Entities;
using Xpinn.FabricaCreditos.Entities;
using Xpinn.Util;
using System.Drawing.Imaging;

[ScriptService]
public partial class Nuevo : GlobalWeb
{

    private Xpinn.Contabilidad.Services.TerceroService TerceroServicio = new Xpinn.Contabilidad.Services.TerceroService();
    private Xpinn.Aportes.Services.AfiliacionServices AfiliacionServicio = new Xpinn.Aportes.Services.AfiliacionServices();
    private Xpinn.FabricaCreditos.Services.Persona1Service persona1Servicio = new Xpinn.FabricaCreditos.Services.Persona1Service();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(AfiliacionServicio.codigoprogramabiometria, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoCancelar += btnCancelar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AfiliacionServicio.codigoprogramabiometria, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                mvPrincipal.ActiveViewIndex = 0;
                if (Session[AfiliacionServicio.codigoprogramabiometria + ".id"] != null)
                {
                    idObjeto = Session[AfiliacionServicio.codigoprogramabiometria + ".id"].ToString();
                    ObtenerDatos(idObjeto);

                }
                else
                {
                    Navegar(Pagina.Lista);
                }
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AfiliacionServicio.codigoprogramabiometria, "Page_Load", ex);
        }
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }



    protected void ObtenerDatos(String pCodPersona)
    {
        Xpinn.FabricaCreditos.Services.Persona1Service personaServicio = new Xpinn.FabricaCreditos.Services.Persona1Service();
        Xpinn.FabricaCreditos.Entities.Persona1 persona = new Xpinn.FabricaCreditos.Entities.Persona1();
        persona.cod_persona = Convert.ToInt64(pCodPersona);
        persona.seleccionar = "Cod_persona";
        persona = personaServicio.ConsultarPersona1Param(persona, (Usuario)Session["Usuario"]);
        String tipo_persona = persona.tipo_persona;
        if (tipo_persona == "Juridica")
        {
            Navegar(Pagina.Lista);
        }
        else
        {
            Site toolBar = (Site)this.Master;
            toolBar.MostrarCancelar(true);
            txtCod_persona.Text = persona.cod_persona.ToString();
            txtIdentificacion.Text = persona.identificacion;
            txtTipoIdentificacion.Text = persona.tipo_identificacion.ToString();
            txtNombres.Text = persona.nombres;
            txtApellidos.Text = persona.apellidos;
            txtDireccion.Text = persona.direccion;
            txtTelefono.Text = persona.telefono;
        }
    }


    private static object obtenervalor()
    {
        Xpinn.Contabilidad.Entities.Tercero vTercero = new Xpinn.Contabilidad.Entities.Tercero();
        List<Xpinn.Contabilidad.Entities.Tercero> lstConsulta = new List<Xpinn.Contabilidad.Entities.Tercero>();
        return vTercero;
    }


    static Xpinn.FabricaCreditos.Services.Persona1Service persona1Servicios = new Xpinn.FabricaCreditos.Services.Persona1Service();
    public String getNombreImagenServidor(String extension)
    {
        /*Devuelve el nombre temporal de la imagen*/
        Random nRandom = new Random();
        String nr = Convert.ToString(nRandom.Next(0, 32000));
        String nombre = nr + "_" + DateTime.Today.ToString("ddMMyyyy") + extension;
        nRandom = null;
        return nombre;
    }


    public void guardarimagen(string imageData)
    {
        PersonaResponsable pResponsable = new PersonaResponsable();
        Usuario vUsuario = new Usuario();
        vUsuario = (Usuario)Session["Usuario"];
        Xpinn.FabricaCreditos.Entities.Persona1 vPersona1 = new Xpinn.FabricaCreditos.Entities.Persona1();
        persona1Servicios.CrearPersonaAporte(vPersona1, false, pResponsable, (Usuario)Session["Usuario"]);
    }

    public string UploadImages(string CodPersona, string ImagenData, string tipoiden)
    {
        Xpinn.Contabilidad.Entities.Tercero vTercero = new Xpinn.Contabilidad.Entities.Tercero();
        string fileNameWitPath = "C:/Publica/Huella.jpg";
        try
        {
            using (FileStream fs = new FileStream(fileNameWitPath, FileMode.Create))
            {
                using (BinaryWriter bw = new BinaryWriter(fs))
                {
                    obtenervalor();
                    byte[] data = Convert.FromBase64String(ImagenData.Replace("data:image/jpeg;base64,", ""));
                    Persona1 vPersona1 = new Persona1();
                    vPersona1.cod_persona = Convert.ToInt64(CodPersona);
                    vPersona1.seleccionar = "Cod_persona";
                    vPersona1 = persona1Servicios.ConsultarPersona1Param(vPersona1, (Usuario)Session["Usuario"]);
                    vPersona1.foto = data;
                    vPersona1.tipo_identificacion = Convert.ToInt32(tipoiden);
                    if (vPersona1.idimagen == null || vPersona1.idimagen == 0)
                        persona1Servicios.CrearPersonasImagenes(vPersona1, (Usuario)Session["Usuario"]);
                    else
                        persona1Servicios.ModificarPersonasImagenes(vPersona1, (Usuario)Session["Usuario"]);
                    bw.Write(data);
                    bw.Close();
                }
            }
            return "";
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
            return ex.Message;
        }

    }

    protected void validar_persona(object sender, System.EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, typeof(Page), "jsKeys", "javascript:validar();", true);
    }

    [WebMethod()]
    public static void PruebaAjax(string imageData, string valor1, string tipoiden)
    {
        var txto = valor1;
    }

    protected void OnServerClick(object sender, EventArgs e)
    {
        var error = UploadImages(txtCod_persona.Text, Base64.Value, txtTipoIdentificacion.Text);
        ScriptManager.RegisterStartupScript(this, typeof(Page), "abc",
           "resultado(" + error + ")", true);
    }
}


