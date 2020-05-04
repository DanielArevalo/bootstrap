using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using Xpinn.FabricaCreditos.Services;
using Xpinn.FabricaCreditos.Entities;

public partial class Detalle : GlobalWeb
{
    AprobadorService aprobadorServicio = new AprobadorService();
    OficinaService oficinaServicio = new OficinaService();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
           
            if (Session[aprobadorServicio.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(aprobadorServicio.CodigoPrograma, "E");
            else
                VisualizarOpciones(aprobadorServicio.CodigoPrograma, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoNuevo += btnNuevo_Click;
            toolBar.eventoEliminar += btnEliminar_Click;
            toolBar.eventoEditar += btnEditar_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(aprobadorServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Session["NombreOficina"] != null)
                    txtNombreOficina.Text = Session["NombreOficina"].ToString();

                //se inicializa el combo de lineas de credito, usuarios y oficinas
                LlenarComboLineasCredito(ddlLinea);
                LlenarComboPersona(ddlUsuario);

                if (Session[aprobadorServicio.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[aprobadorServicio.CodigoPrograma + ".id"].ToString();
                    Session.Remove(aprobadorServicio.CodigoPrograma + ".id");
                    ObtenerDatos(idObjeto);
                }
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(aprobadorServicio.CodigoPrograma, "Page_Load", ex);
        }
    }

    protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    {
        Navegar(Pagina.Nuevo);
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        Navegar(Pagina.Lista);
    }

    protected void btnEliminar_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            aprobadorServicio.EliminarAprobador(Convert.ToInt32(idObjeto), (Usuario)Session["usuario"]);
            Navegar(Pagina.Lista);
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(aprobadorServicio.CodigoPrograma, "btnEliminar_Click", ex);
        }
    }

    protected void btnEditar_Click(object sender, ImageClickEventArgs e)
    {
        Session[aprobadorServicio.CodigoPrograma + ".id"] = idObjeto;
        Navegar(Pagina.Nuevo);
    }

    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            Aprobador aprobador = new Aprobador();

            if (pIdObjeto != null)
            {
                aprobador.Id = Int32.Parse(pIdObjeto);
                aprobador = aprobadorServicio.ConsultarAprobador(aprobador, (Usuario)Session["usuario"]);

                if (!string.IsNullOrEmpty(aprobador.LineaCredito))
                    ddlLinea.SelectedValue = aprobador.LineaCredito.ToString();
                if (!string.IsNullOrEmpty(aprobador.UsuarioAprobador))
                    ddlUsuario.SelectedValue = aprobador.UsuarioAprobador.ToString();
                if (!string.IsNullOrEmpty(aprobador.Nivel.ToString()))
                    ddlNivel.SelectedValue = aprobador.Nivel.ToString();
                if (!string.IsNullOrEmpty(aprobador.MontoMinimo.ToString()))
                    txtMinimo.Text = HttpUtility.HtmlDecode(aprobador.MontoMinimo.ToString());
                if (!string.IsNullOrEmpty(aprobador.MontoMaximo.ToString()))
                    txtMaximo.Text = HttpUtility.HtmlDecode(aprobador.MontoMaximo.ToString());
                if (!string.IsNullOrEmpty(aprobador.Aprueba.ToString()))
                {
                    if (aprobador.Aprueba == 1)
                        chkAprueba.Checked = true;
                    else
                        chkAprueba.Checked = false;
                }
            }
            //VerAuditoria(programa.UsuarioCrea, programa.FechaCrea, programa.UsuarioEdita, programa.FechaEdita);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(aprobadorServicio.CodigoPrograma, "ObtenerDatos", ex);
        }
    }

    protected void LlenarComboLineasCredito(DropDownList ddlLinea)
    {
        LineasCreditoService lineaService = new LineasCreditoService();
        LineasCredito nombre = new LineasCredito();
        ddlLinea.DataSource = lineaService.ListarLineasCredito(nombre, (Usuario)Session["usuario"]);
        ddlLinea.DataTextField = "nombre";
        ddlLinea.DataValueField = "codigo";
        ddlLinea.DataBind();
        ddlLinea.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));
    }

    protected void LlenarComboPersona(DropDownList ddlPersona)
    {
        PersonaService personaService = new PersonaService();
        Persona persona = new Persona();
        ddlPersona.DataSource = personaService.ListarPersonas(persona, (Usuario)Session["usuario"]);
        ddlPersona.DataTextField = "nombre";
        ddlPersona.DataValueField = "codigousuario";
        ddlPersona.DataBind();
        ddlPersona.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));
    }
}