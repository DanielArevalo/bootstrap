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

partial class Nuevo : GlobalWeb
{
    private Xpinn.Asesores.Services.CreacionMensajeService mensajeService = new Xpinn.Asesores.Services.CreacionMensajeService();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(mensajeService.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Session[mensajeService.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[mensajeService.CodigoPrograma + ".id"].ToString();
                    Session.Remove(mensajeService.CodigoPrograma + ".id");
                    ObtenerDatos(idObjeto);

                }
                BorrarDatosTemp();
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(mensajeService.CodigoPrograma, "Page_Load", ex);
        }
    }

    protected void btnGuardarTemp_Click(object sender, EventArgs e)
    {
        try
        {
            Xpinn.Asesores.Entities.CreacionMensaje vCreacionMensaje = new Xpinn.Asesores.Entities.CreacionMensaje();
            vCreacionMensaje.documentoPersona = Convert.ToString(txtIdPersona.Text.Trim());
            vCreacionMensaje = mensajeService.CrearMensajePersona(vCreacionMensaje, (Usuario)Session["usuario"]);

            if (vCreacionMensaje != null)
                ObtenerDatosTemp();
            txtIdPersona.Text = "";
            txtNomPersona.Text = "";
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(mensajeService.CodigoPrograma, "btnGuardarTemp_Click", ex);
        }
    }

    protected void ObtenerDatosTemp()
    {
        try
        {
            var id = Session["IdMensaje"];
            List<Xpinn.Asesores.Entities.PesonasTemp> vPesonasTemp = new List<Xpinn.Asesores.Entities.PesonasTemp>();
            if (id != null)
            {
                vPesonasTemp = mensajeService.ConsultarPersonasTemp(id.ToString(), (Usuario)Session["usuario"]);
            }
            else
            {
                vPesonasTemp = mensajeService.ConsultarPersonasTemp(null, (Usuario)Session["usuario"]);
            }

            gvLista.PageSize = pageSize;
            gvLista.EmptyDataText = emptyQuery;
            gvLista.DataSource = vPesonasTemp;

            if (vPesonasTemp.Count > 0)
            {
                gvLista.Visible = true;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + vPesonasTemp.Count.ToString();
                gvLista.DataBind();
            }
            else
            {
                gvLista.Visible = false;
                lblTotalRegs.Visible = false;
            }

        }
        catch (Exception ex)
        {
        }
    }

    protected void BorrarDatosTemp()
    {
        try
        {
            mensajeService.EliminarPersonaMensajeTemp((Usuario)Session["usuario"]);
        }
        catch (Exception ex)
        {
        }
    }
    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            Xpinn.Asesores.Entities.CreacionMensaje vCreacionMensaje = new Xpinn.Asesores.Entities.CreacionMensaje();

            if (idObjeto != "")
                vCreacionMensaje = mensajeService.ConsultarMensajes(Convert.ToInt64(idObjeto), (Usuario)Session["usuario"]);
            vCreacionMensaje.Descripcion = Convert.ToString(txtDescripcion.Text.Trim());

            if (idObjeto != "")
            {
                vCreacionMensaje.IdMensaje = Convert.ToInt64(idObjeto);
                mensajeService.ModificarMensajes(vCreacionMensaje, (Usuario)Session["usuario"]);
            }
            else
            {
                vCreacionMensaje = mensajeService.CrearMensaje(vCreacionMensaje, (Usuario)Session["usuario"]);
                idObjeto = vCreacionMensaje.IdMensaje.ToString();
            }

            Session[mensajeService.CodigoPrograma + ".id"] = idObjeto;
            Navegar(Pagina.Detalle);
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(mensajeService.CodigoPrograma, "btnGuardar_Click", ex);
        }
    }

    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        if (idObjeto == "")
        {
            Navegar(Pagina.Lista);
        }
        else
        {
            Session[mensajeService.CodigoPrograma + ".id"] = idObjeto;
            Navegar(Pagina.Detalle);
        }
    }

    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            Session["IdMensaje"] = pIdObjeto;
            Xpinn.Asesores.Entities.CreacionMensaje vCreacionMensaje = new Xpinn.Asesores.Entities.CreacionMensaje();
            List<Xpinn.Asesores.Entities.PesonasTemp> vPesonasTemp = new List<Xpinn.Asesores.Entities.PesonasTemp>();

            vCreacionMensaje = mensajeService.ConsultarMensajes(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);
            vPesonasTemp = mensajeService.ConsultarPersonasTemp(pIdObjeto, (Usuario)Session["usuario"]);

            if (!string.IsNullOrEmpty(vCreacionMensaje.Descripcion))
                txtDescripcion.Text = HttpUtility.HtmlDecode(vCreacionMensaje.Descripcion.ToString().Trim());

            gvLista.PageSize = pageSize;
            gvLista.EmptyDataText = emptyQuery;
            gvLista.DataSource = vPesonasTemp;

            if (vPesonasTemp.Count > 0)
            {
                gvLista.Visible = true;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + vPesonasTemp.Count.ToString();
                gvLista.DataBind();
            }
            else
            {
                gvLista.Visible = false;
                lblTotalRegs.Visible = false;
            }
        }
        catch (Exception ex)
        {
        }
    }
    public void cargarDatosPersona(Int64 cod_persona)
    {
        Persona1Service DatosPersona = new Persona1Service();
        VerError("");

        Persona1 pPersona = new Persona1();
        Persona1 lstOficina = new Persona1();

        if (cod_persona != 0)
        {
            pPersona = DatosPersona.ConsultaDatosPersona(cod_persona, (Usuario)Session["usuario"]);

            if (pPersona.cod_persona != 0)
                txtCodPersona.Text = pPersona.cod_persona.ToString();
            if (pPersona.identificacion != null)
            {
                txtIdPersona.Text = pPersona.identificacion;
                txtNomPersona.Text = pPersona.nombre;
            }
        }
        else
        {
            VerError("No se encontraron datos de las persona");
        }
    }
    protected void btnConsultaPersonas_Click(object sender, EventArgs e)
    {
        List<Xpinn.Caja.Entities.Oficina> lstOficina = new List<Xpinn.Caja.Entities.Oficina>();
        ctlBusquedaPersonas.Mostrar(true, "txtCodPersona", "txtIdPersona", "txtNomPersona", "txtIdentificacionTitu", "txtNombreTit");
    }

    protected void txtIdPersona_TextChanged(object sender, EventArgs e)
    {
        Xpinn.Seguridad.Services.UsuarioService UsuarioServicio = new Xpinn.Seguridad.Services.UsuarioService();
        Xpinn.FabricaCreditos.Entities.Persona1 DatosPersona = new Xpinn.FabricaCreditos.Entities.Persona1();

        DatosPersona = UsuarioServicio.ConsultarPersona1(txtIdPersona.Text, (Usuario)Session["usuario"]);

        if (DatosPersona.cod_persona != 0)
        {
            txtCodPersona.Text = DatosPersona.cod_persona.ToString();

            if (DatosPersona.identificacion != "")
            {
                txtIdPersona.Text = DatosPersona.identificacion;

            }
            if (DatosPersona.nombre != "")
            {
                txtNomPersona.Text = DatosPersona.nombre;
            }

        }
        else
        {
            txtNomPersona.Text = "";
            txtCodPersona.Text = "";
        }

    }
}