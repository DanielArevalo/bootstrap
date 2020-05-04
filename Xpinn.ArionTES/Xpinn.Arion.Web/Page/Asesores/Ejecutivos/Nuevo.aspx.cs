using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Asesores.Services;
using Xpinn.Asesores.Entities;
using Xpinn.Asesores.Entities.Common;
using Xpinn.Util;

public partial class AseEjecutivosNuevo : GlobalWeb
{
    Usuario usuario = new Usuario();
    Zona aseEntZona = new Zona();
    EjecutivoService serviceEjecutivo = new EjecutivoService();
    ParametricaService serviceParametrica = new ParametricaService();
    Ejecutivo entityEjecutivo = new Ejecutivo();
    Oficina entityOficina = new Oficina();
    Estado entityEstado = new Estado();
    TipoIdentificacion aseEntTipoDoc = new TipoIdentificacion();
    PersonaService servicepersona = new PersonaService();
    ClientePotencialService serviceCliente = new ClientePotencialService();
    UsuarioAseService serviceUsuario = new UsuarioAseService();

    private void Page_PreInit(object sender, EventArgs evt)
    {
        try
        {
            if (Session[serviceEjecutivo.CodigoPrograma + ".id"] != null)
            {
                VisualizarOpciones(serviceEjecutivo.CodigoPrograma, "E");
                txtNumeDoc.Enabled = false;
                ddlTipoDoc.Enabled = false;
            }
            else
            {
                VisualizarOpciones(serviceEjecutivo.CodigoPrograma, "A");
            }

            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(serviceEjecutivo.GetType().Name + "A", "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                //ddlZona.Enabled = false;
                ddlOficina.Enabled = false;
               
                Labelerroroficina.Text = null;
                ucFecha.ToDateTime = DateTime.Today;
                ObtenerDatosDropDownList();

                if (Session[serviceEjecutivo.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[serviceEjecutivo.CodigoPrograma + ".id"].ToString();
                    Session.Remove(serviceEjecutivo.CodigoPrograma + ".id");
                    ObtenerDatos(idObjeto);
                    ddlOficina.Enabled = true;
                }
            }

        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(serviceEjecutivo.GetType().Name + "A", "Page_Load", ex);
        }
    }// end pageLoad


    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ClientePotencial CLIENTE = new ClientePotencial();
            Persona persona = new Persona();
            if (idObjeto != "")
            { 
                CLIENTE = serviceCliente.ConsultarClienteyaExistente(Convert.ToInt64(idObjeto), (Usuario)Session["usuario"]);
            persona = servicepersona.ConsultaryaExistente(Convert.ToInt64(idObjeto), (Usuario)Session["usuario"]);
            }
            if (CLIENTE.NumeroDocumento == 000 && persona.NumeroDocumento == "000" || CLIENTE.NumeroDocumento == 0)
            {
                UsuarioAse cod_oficina = new UsuarioAse(); 
                ddlOficina.Enabled = true;
                cod_oficina = serviceUsuario.ConsultarUsuarios(txtNumeDoc.Text, (Usuario)Session["usuario"]);

        string aux = Convert.ToString(cod_oficina.cod_oficina);
        if (Convert.ToInt64(cod_oficina.cod_oficina) == Convert.ToInt64(ddlOficina.SelectedValue))
        {
           
            if (idObjeto != "")
                entityEjecutivo = serviceEjecutivo.ConsultarEjecutivo(Convert.ToInt64(idObjeto), (Usuario)Session["usuario"]);
            
            if (!string.IsNullOrEmpty(txtPrimerNombre.Text.ToUpper()))
                entityEjecutivo.PrimerNombre = txtPrimerNombre.Text.ToUpper();
            else
                entityEjecutivo.PrimerNombre = " ";
            if (!string.IsNullOrEmpty(txtSegundoNombre.Text.ToUpper()))
                entityEjecutivo.SegundoNombre = txtSegundoNombre.Text.ToUpper();
            else
                entityEjecutivo.SegundoNombre = " ";
            if (!string.IsNullOrEmpty(txtPrimerApellido.Text.ToUpper()))
                entityEjecutivo.PrimerApellido = txtPrimerApellido.Text.ToUpper();
            else
                entityEjecutivo.PrimerApellido = " ";
            if (!string.IsNullOrEmpty(txtSegundoApellido.Text.ToUpper()))
                entityEjecutivo.SegundoApellido = txtSegundoApellido.Text.ToUpper();
            else
                entityEjecutivo.SegundoApellido = " ";
            if (!string.IsNullOrEmpty(txtNumeDoc.Text.ToUpper()))
                entityEjecutivo.NumeroDocumento = txtNumeDoc.Text.ToUpper();
            if (!string.IsNullOrEmpty(txtDirCorrespondencia.Text.ToUpper()))
                entityEjecutivo.Direccion = txtDirCorrespondencia.Text.ToUpper();
            else
                entityEjecutivo.Direccion = " ";

            if (!string.IsNullOrEmpty(txtTeleResi.Text.ToUpper()))
                entityEjecutivo.Telefono = Convert.ToInt64(txtTeleResi.Text.ToUpper());
            else
                entityEjecutivo.Telefono = 0;
            if (!string.IsNullOrEmpty(txtCelular.Text.ToUpper()))
                entityEjecutivo.TelefonoCel = Convert.ToInt64(txtCelular.Text.ToUpper());
            else
                entityEjecutivo.TelefonoCel = 0;
            if (!string.IsNullOrEmpty(txtEmail.Text.ToUpper()))
                entityEjecutivo.Email = txtEmail.Text;
            else
                entityEjecutivo.Email = " ";

            if (!string.IsNullOrEmpty(ucFecha.ToDate))
                entityEjecutivo.FechaIngreso = ucFecha.ToDateTime;
            if (!string.IsNullOrEmpty(ddlEstado.SelectedValue))
                entityEjecutivo.IdEstado = Convert.ToInt64(ddlEstado.SelectedValue);
            if (!string.IsNullOrEmpty(ddlTipoDoc.SelectedValue))
                entityEjecutivo.TipoDocumento = Convert.ToInt64(ddlTipoDoc.SelectedValue);

            List<Ejecutivo> lstZonaPrincipal = new List<Ejecutivo>();
            lstZonaPrincipal = ObtenerListaGridViewZonas("0");
                    if (lstZonaPrincipal.Count <= 0)
                    {
                        VerError("Debe Seleccionar La Zona Principal");
                        return;
                    }
                    entityEjecutivo.IdZona = Convert.ToInt64(lstZonaPrincipal[0].icodciudad);
                    


             List<Ejecutivo> lstBarrioPrincipal = new List<Ejecutivo>();
            lstBarrioPrincipal = ObtenerListaGridViewBarrios("0");
                    if (lstBarrioPrincipal.Count <= 0)
                    {
                        VerError("Debe Seleccionar El Barrio Principal");
                        return;
                    }
                    entityEjecutivo.Barrio = Convert.ToString(lstBarrioPrincipal[0].icodciudad);
                    

            if (!string.IsNullOrEmpty(ddlOficina.SelectedValue))
                entityEjecutivo.IdOficina = Convert.ToInt64(ddlOficina.SelectedValue);

            if (idObjeto != "")
            {
                entityEjecutivo.IdEjecutivo = Convert.ToInt64(idObjeto);
                entityEjecutivo.UsuarioEdita = "Admin";  // Modificar por usuario en sesion
                serviceEjecutivo.ActualizarCliente(entityEjecutivo, (Usuario)Session["usuario"]);
                idObjeto = entityEjecutivo.IdEjecutivo.ToString();
            }
            else
            {
                entityEjecutivo.UsuarioCrea = "Admin";  // Crear por usuario en sesion
                entityEjecutivo = serviceEjecutivo.CrearEjecutivo(entityEjecutivo, (Usuario)Session["usuario"]);
                idObjeto = entityEjecutivo.IdEjecutivo.ToString();
            }

            //GUARDAR ZONAS
            List<Ejecutivo> lstZonasSeleccionadas = new List<Ejecutivo>();
                    lstZonasSeleccionadas = ObtenerListaGridViewZonas(idObjeto);
            if (lstZonasSeleccionadas.Count > 0)
            {
                serviceEjecutivo.guardarZonasEjecutivo(lstZonasSeleccionadas, (Usuario)Session["usuario"]);
                        
            }

            //GUARDAR BARRIOS
            List<Ejecutivo> lstBarriosSeleccionados = new List<Ejecutivo>();
            lstBarriosSeleccionados = ObtenerListaGridViewBarrios(idObjeto);
            if (lstBarriosSeleccionados.Count > 0)
            {
                serviceEjecutivo.guardarBarriosEjecutivo(lstBarriosSeleccionados, (Usuario)Session["usuario"]);
            }
            Session[serviceEjecutivo.CodigoPrograma + ".id"] = idObjeto;
            Navegar(Pagina.Lista);
        }
        else
        {      
            Labelerroroficina.Text = "La oficina debe ser igual";       
        }
            }
            else
            {
                Labelerror.Text = "El cliente ya se encuentra registraso en el Sistema ";
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(serviceEjecutivo.GetType().Name + "A", "btnGuardar_Click", ex);
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
            //Response.Redirect("~/Page/Asesores/EstadoCuenta/Detalle.aspx");
        }
        else
        {
            Session[serviceEjecutivo.CodigoPrograma + ".id"] = idObjeto;
            Navegar(Pagina.Detalle);
        }
    }

    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            Ejecutivo entityEjecutivo = new Ejecutivo();

            entityEjecutivo = serviceEjecutivo.ConsultarEjecutivo(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);
            if (!string.IsNullOrEmpty(entityEjecutivo.PrimerNombre)) txtPrimerNombre.Text = entityEjecutivo.PrimerNombre.Trim().ToString();
            if (!string.IsNullOrEmpty(entityEjecutivo.SegundoNombre)) txtSegundoNombre.Text = entityEjecutivo.SegundoNombre.Trim().ToString();
            if (!string.IsNullOrEmpty(entityEjecutivo.PrimerApellido)) txtPrimerApellido.Text = entityEjecutivo.PrimerApellido.Trim().ToString();
            if (!string.IsNullOrEmpty(entityEjecutivo.SegundoApellido)) txtSegundoApellido.Text = entityEjecutivo.SegundoApellido.Trim().ToString();
            if (!entityEjecutivo.NumeroDocumento.Equals(0)) txtNumeDoc.Text = entityEjecutivo.NumeroDocumento.ToString();
            if (!string.IsNullOrEmpty(entityEjecutivo.Direccion)) txtDirCorrespondencia.Text = entityEjecutivo.Direccion;
            //if (!string.IsNullOrEmpty(entityEjecutivo.Barrio)) txtBarrio.Text = entityEjecutivo.Barrio;
            //Lista de barrios por ejecutivo
            List<Ejecutivo> lstBarrios = new List<Ejecutivo>();
            lstBarrios = serviceEjecutivo.ListarBarriosEjecutivo(entityEjecutivo.IdEjecutivo, (Usuario)Session["usuario"]);
            if (lstBarrios.Count > 0)
            {
                cargarBarrios(lstBarrios);
            }
            if (!entityEjecutivo.Telefono.Equals(0)) txtTeleResi.Text = entityEjecutivo.Telefono.ToString();
            if (!string.IsNullOrEmpty(entityEjecutivo.TelefonoCel.ToString())) txtCelular.Text = entityEjecutivo.TelefonoCel.ToString();
            if (!string.IsNullOrEmpty(entityEjecutivo.Email)) txtEmail.Text = entityEjecutivo.Email;
            if (entityEjecutivo.FechaIngreso != null) ucFecha.ToDateTime = entityEjecutivo.FechaIngreso;
            if (!entityEjecutivo.TipoDocumento.Equals(0)) ddlTipoDoc.Items.FindByText(entityEjecutivo.NombreTipoDocumento).Selected = true;
            //if (!entityEjecutivo.IdZona.Equals(0)) ddlZona.Items.FindByText(entityEjecutivo.NombreZona).Selected= true;
            //Lista de zonas por ejecutivo
            List<Ejecutivo> lstZonas = new List<Ejecutivo>();
            lstZonas = serviceEjecutivo.ListarZonasEjecutivo(entityEjecutivo.IdEjecutivo, (Usuario)Session["usuario"]);
            if (lstZonas.Count > 0)
            {
                cargarZonas(lstZonas);
            }
            if (!entityEjecutivo.IdEstado.Equals(0)) ddlEstado.Items.FindByText(entityEjecutivo.NombreEstado).Selected = true;
            if (!entityEjecutivo.IdOficina.Equals(0)) ddlOficina.Items.FindByText(entityEjecutivo.NombreOficina).Selected = true;
           

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(serviceEjecutivo.GetType().Name + "A", "ObtenerDatos", ex);
        }
    }
    //RECURSOS PARA BARRIOS MULTIPLES
    List<Ejecutivo> ObtenerListaGridViewBarrios( string cod_ejecutivo)
    {
        List<Ejecutivo> lstBarriosSeleccionados = new List<Ejecutivo>();
        foreach (GridViewRow rfila in gvBarrios.Rows)
        {
            CheckBox cbListado = (CheckBox)rfila.FindControl("cbListado");
            if (Convert.ToBoolean(cbListado.Checked))
            {
                Ejecutivo barrio = new Ejecutivo();
                Label lbl_barrio = (Label)rfila.FindControl("lbl_barrio");
                barrio.icodciudad = Convert.ToInt64(lbl_barrio.Text);
                barrio.IdEjecutivo = Convert.ToInt64(cod_ejecutivo);
                lstBarriosSeleccionados.Add(barrio);
            }
        }

        return lstBarriosSeleccionados;
    }
    void cargarBarrios(List<Ejecutivo> lstBarrios)
    {
        foreach (Ejecutivo barrio in lstBarrios)
        {
            if (barrio.icodciudad != 0)
            {
                foreach (GridViewRow rfila in gvBarrios.Rows)
                {
                    Label id_barrio = (Label)rfila.FindControl("lbl_barrio");
                    if (barrio.icodciudad == Convert.ToInt64(id_barrio.Text))
                    {
                        CheckBox cbListado = (CheckBox)rfila.FindControl("cbListado");
                        Label lbl_nom_ciudad = (Label)rfila.FindControl("lbl_nom_ciudad");
                        cbListado.Checked = true;
                        txtBarrios.Text += Convert.ToString(lbl_nom_ciudad.Text)+" ";
                    }
                }
            }
        }
    }
    //RECURSOS PARA ZONAS MULTIPLES
    List<Ejecutivo> ObtenerListaGridViewZonas(string cod_ejecutivo)
    {
        List<Ejecutivo> lstZonasSeleccionadas = new List<Ejecutivo>();
        foreach (GridViewRow rfila in gvZonas.Rows)
        {
            CheckBox cbListado = (CheckBox)rfila.FindControl("cbListadoZ");
            if (Convert.ToBoolean(cbListado.Checked))
            {
                Ejecutivo zona = new Ejecutivo();
                Label lbl_zona = (Label)rfila.FindControl("lbl_zona");
                zona.icodciudad = Convert.ToInt64(lbl_zona.Text);
                zona.IdEjecutivo = Convert.ToInt64(cod_ejecutivo);
                lstZonasSeleccionadas.Add(zona);
            }
        }

        return lstZonasSeleccionadas;
    }
    void cargarZonas(List<Ejecutivo> lstZonas)
    {
        foreach (Ejecutivo zonas in lstZonas)
        {
            Ejecutivo zona = new Ejecutivo();
            zona.icodciudad = zonas.icodciudad;
            if (zona.icodciudad <= 0)
            {
                foreach (GridViewRow rfila in gvZonas.Rows)
                {
                    Label id_zona = (Label)rfila.FindControl("lbl_zona");
                    if (zona.icodciudad == Convert.ToInt64(id_zona.Text))
                    {
                        CheckBox cbListado = (CheckBox)rfila.FindControl("cbListadoZ");
                        Label lbl_nom_zona = (Label)rfila.FindControl("lbl_nom_zona");
                        cbListado.Checked = true;
                        txtZonas.Text += Convert.ToString(lbl_nom_zona.Text) + "";
                    }
                }
            }
        }
    }
    /// <summary>
    /// Método para llenar los DROPDOWNLIST
    /// </summary>
    private void ObtenerDatosDropDownList()
    {
        Barrios entityEjecutivo = new Barrios();
        List<Barrios> lstConsultaBarrios = new List<Barrios>();
        lstConsultaBarrios = serviceParametrica.ListarBarrios(6, (Usuario)Session["Usuario"]);

        if (lstConsultaBarrios.Count > 0)
        {
            gvBarrios.DataSource = lstConsultaBarrios;
            gvBarrios.DataBind();
        }

        ddlTipoDoc.DataSource = serviceParametrica.ListarTipoIdentificacion(aseEntTipoDoc, (Usuario)Session["usuario"]);
        ddlTipoDoc.DataTextField = "NombreTipoIdentificacion";
        ddlTipoDoc.DataValueField = "IdTipoIdentificacion";
        ddlTipoDoc.DataBind();

        Barrios entityZona = new Barrios();
        List<Barrios> lstConsultaZonas = new List<Barrios>();
        lstConsultaZonas = serviceParametrica.ListarZonasBarrios(Convert.ToString(0), (Usuario)Session["Usuario"]);

        if (lstConsultaZonas.Count > 0)
        {
            gvZonas.DataSource = lstConsultaZonas;
            gvZonas.DataBind();
        }

        ddlOficina.DataSource = serviceParametrica.ListarOficina(entityOficina, (Usuario)Session["Usuario"]);
        ddlOficina.DataTextField = "NombreOficina";
        ddlOficina.DataValueField = "IdOficina";
        ddlOficina.DataBind();

        ddlEstado.DataSource = serviceParametrica.ListarEstado(entityEstado, (Usuario)Session["Usuario"]);
        ddlEstado.DataTextField = "Descripcion";
        ddlEstado.DataValueField = "IdEstado";
        ddlEstado.DataBind();
    }

    protected void ddlTipoDoc_SelectedIndexChanged(object sender, EventArgs e)
    {
        /*ddlZona.DataSource = serviceParametrica.ListarZonasBarrios(Convert.ToInt64(txtBarrio.Text), (Usuario)Session["Usuario"]);
        ddlZona.DataTextField = "NOMCIUDAD";
        ddlZona.DataValueField = "CODCIUDAD";
        ddlZona.DataBind();*/
    }

    protected void txtNumeDoc_TextChanged(object sender, EventArgs e)
    {
        ClientePotencial Usuario = new ClientePotencial();
        Persona persona = new Persona();
          Usuario usuap = (Usuario)Session["usuario"];
        if(txtNumeDoc.Text.Trim()!="")
           Usuario = serviceCliente.Consultarusuario(Convert.ToInt64(txtNumeDoc.Text), (Usuario)Session["usuario"]);

        if (Usuario.IdUsuario == 0)
        {
            Labelerror.Text = " La identificación no existe en la tabla usuarios,Por favor ingresar primero como usuario";
            txtPrimerNombre.Enabled = false;
            txtSegundoNombre.Enabled = false;
            txtPrimerApellido.Enabled = false;
            txtSegundoApellido.Enabled = false;
            txtTeleResi.Enabled = false;
            txtCelular.Enabled = false;
            txtDirCorrespondencia.Enabled = false;
            //ddlZona.Enabled = false;
            txtBarrios.Enabled = false;
            ddlOficina.Enabled = false;
            ddlEstado.Enabled = false;
            ucFecha.Enabled = false;
            ddlOficina.SelectedValue = Convert.ToString(usuap.cod_oficina);
        }
        else
        { 
            Labelerror.Text = "";

            ddlOficina.SelectedValue = Convert.ToString(usuap.cod_oficina);
        }
    }
}