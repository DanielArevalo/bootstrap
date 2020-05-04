using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using Xpinn.FabricaCreditos.Services;
using Xpinn.FabricaCreditos.Entities;

public partial class Nuevo : GlobalWeb
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
            toolBar.eventoGuardar += btnGuardar_Click;
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
                {
                    lblNombreOficina.Text = Session["NombreOficina"].ToString();
                    lblTitOFicina.Visible = true;
                    lblNombreOficina.Visible = true;
                }
                else
                {
                    lblTitOFicina.Visible = false;
                    lblNombreOficina.Visible = false;
                }
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

    private Aprobador ObtenerValores()
    {
        Aprobador aprobador = new Aprobador();
        return aprobador;
    }

    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        //Consulta Lista Aprobador
        List<Aprobador> lstConsulta = new List<Aprobador>();
        lstConsulta = aprobadorServicio.ListarAprobador(ObtenerValores(), (Usuario)Session["usuario"]);

        bool Existe = false;

        if (lstConsulta.Count > 0)
        {
            int NumFila = lstConsulta.Count - 1;
            while (NumFila != 0)
            {
                if (lstConsulta[NumFila].UsuarioAprobador.ToString() == ddlUsuario.SelectedItem.ToString())
                {
                    if (lstConsulta[NumFila].LineaCredito.ToString() == ddlLinea.SelectedItem.ToString())
                    {
                        NumFila = 0;
                        Existe = true;
                    }
                    else
                    {
                        NumFila--;
                    }
                }
                else
                {
                    NumFila--;
                }
            }
        }

        if (Existe == false)    // Si no existe el registro de la linea y la persona lo permite guardar
        {
            try
            {
                if (ValidarDatos())
                {
                    Aprobador aprobador = new Aprobador();
                    aprobador.LineaCredito = ddlLinea.SelectedValue;
                    aprobador.UsuarioAprobador = ddlUsuario.SelectedValue;
                    aprobador.MontoMinimo = (Decimal.Parse(txtMinimo.Text));
                    aprobador.MontoMaximo = (Decimal.Parse(txtMaximo.Text));
                    aprobador.Nivel = Int32.Parse(ddlNivel.SelectedValue);
                    if (chkAprueba.Checked)
                        aprobador.Aprueba = 1;
                    else
                        aprobador.Aprueba = 0;

                    if (idObjeto != "")
                    {
                        aprobador.Id = Convert.ToInt64(idObjeto);
                        aprobadorServicio.ModificarAprobador(aprobador, (Usuario)Session["usuario"]);
                    }
                    else
                    {
                        aprobador = aprobadorServicio.CrearAprobador(aprobador, (Usuario)Session["usuario"]);
                    }

                    Navegar(Pagina.Lista);
                }

            }
            catch (ExceptionBusiness ex)
            {
                VerError(ex.Message);
            }
            catch (Exception ex)
            {
                BOexcepcion.Throw(aprobadorServicio.CodigoPrograma, "btnGuardar_Click", ex);
            }
        }
        else
        {
            VerError("Ya existe el aprobador para la línea de crédito dada");
        }
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        Navegar(Pagina.Lista);
    }

    protected void btnCancelar_Click(object sender, ImageClickEventArgs e)
    {
        if (idObjeto == "")
        {
            Navegar(Pagina.Lista);
        }
        else
        {
            //Session[aprobadorServicio.Codigo + ".id"] = idObjeto;
            Navegar(Pagina.Detalle);
        }
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        LimpiarValoresConsulta(pConsulta, aprobadorServicio.CodigoPrograma);
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
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(aprobadorServicio.CodigoPrograma, "ObtenerDatos", ex);
        }
    }

    protected void LlenarComboLineasCredito(DropDownList ddlLinea)
    {
        LineasCreditoService lineaService = new LineasCreditoService();
        LineasCredito linea = new LineasCredito();
        ddlLinea.DataSource = lineaService.ListarLineasCredito(linea, (Usuario)Session["usuario"]);
        ddlLinea.DataTextField = "nombre";
        ddlLinea.DataValueField = "cod_linea_credito";
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
        ddlPersona.Items.Insert(0, new ListItem("<Seleccione un Item>", "-1"));
    }

    private bool ValidarDatos()
    {
        string error = "";
        if (ddlLinea.SelectedValue == "0")
            error = "No ha Seleccionado ninguna Linea de credito";
        if (ddlUsuario.SelectedValue == "-1")
            error = "No ha Seleccionado ninguna usuario";
        if (txtMinimo.Text == "")
            error = "El monto minimo es obligatorio";
        if (txtMaximo.Text == "")
            error = "El monto Maximo es obligatorio";
        if (ddlNivel.SelectedValue == "0")
            error = "No ha seleccionado ningun nivel";

        if (error != "")
        {
            VerError(error);
            return false;
        }

        return true;
    }
}