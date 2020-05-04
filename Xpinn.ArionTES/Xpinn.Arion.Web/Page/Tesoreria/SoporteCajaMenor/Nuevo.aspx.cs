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
using Microsoft.Reporting.WebForms;
using System.Drawing.Printing;
using Cantidad_a_Letra;
using System.IO;


partial class Nuevo : GlobalWeb
{
    private Xpinn.Tesoreria.Services.SoporteCajService SoporteCajServicio = new Xpinn.Tesoreria.Services.SoporteCajService();
    private Xpinn.Tesoreria.Services.AreasCajService AreasCajaServicio = new Xpinn.Tesoreria.Services.AreasCajService();
    StringHelper _stringHelper = new StringHelper();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[SoporteCajServicio.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(SoporteCajServicio.CodigoPrograma, "E");
            else
                VisualizarOpciones(SoporteCajServicio.CodigoPrograma, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.MostrarConsultar(false);
            ctlMensaje.eventoClick += btnContinuarMen_Click;
            toolBar.eventoImprimir += btnInforme_Click;
            toolBar.MostrarImprimir(false);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(SoporteCajServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                CargarDDL();
                if (Session[SoporteCajServicio.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[SoporteCajServicio.CodigoPrograma + ".id"].ToString();
                    Session.Remove(SoporteCajServicio.CodigoPrograma + ".id");
                    ObtenerDatos(idObjeto);
                    if (txtEstado.Text != "Elaborado")
                    {
                        Site toolBar = (Site)this.Master;
                        toolBar.MostrarGuardar(false);
                    }
                }
                else
                {
                    Xpinn.Tesoreria.Entities.SoporteCaj vSoporteCaj = new Xpinn.Tesoreria.Entities.SoporteCaj();
                    Xpinn.Tesoreria.Entities.AreasCaj vAreasCaj = AreasCajaServicio.ConsultarCajaMenor(Convert.ToInt32(Usuario.codusuario), Usuario);
                    Int64 usuariocaja = Convert.ToInt64(vAreasCaj.cod_usuario);
                    Int64 usuarioingreso = Convert.ToInt64(Usuario.codusuario);

                    if (usuarioingreso == usuariocaja)
                    {

                        txtCodigo.Text = SoporteCajServicio.ObtenerSiguienteCodigo((Usuario)Session["usuario"]).ToString();
                        txtEstado.Text = "Elaborado";
                        txtNumComp.Visible = false;
                        lblNumComp.Visible = false;
                        txtTipoComp.Visible = false;
                        lblTipoComp.Visible = false;
                        Usuario pUsuario = new Usuario();
                        pUsuario = (Usuario)Session["usuario"];
                        Xpinn.Caja.Services.OficinaService oficinaServicio = new Xpinn.Caja.Services.OficinaService();
                        Xpinn.Caja.Entities.Oficina poficina = new Xpinn.Caja.Entities.Oficina();
                        poficina = oficinaServicio.ConsultarOficina(pUsuario.cod_oficina, pUsuario);
                        txtOficina.Text = poficina.nombre;
                        txtUsuario.Text = pUsuario.nombre;
                        Session["cod_ciudad"] = poficina.cod_ciudad;
                        Configuracion conf = new Configuracion();
                        txtFecha.ToDateTime = System.DateTime.Now;
                    }

                    else
                    {
                     //   VerError("El usuario que esta registrando el recibo no es cajero o no pertenece a esta caja ");
                    }
                }
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(SoporteCajServicio.CodigoPrograma, "Page_Load", ex);
        }
    }

    protected void CargarDDL()
    {
        Xpinn.FabricaCreditos.Services.Persona1Service personaService = new Xpinn.FabricaCreditos.Services.Persona1Service();
        Xpinn.FabricaCreditos.Entities.Persona1 persona = new Xpinn.FabricaCreditos.Entities.Persona1();
        List<Xpinn.FabricaCreditos.Entities.Persona1> lstConsulta = new List<Xpinn.FabricaCreditos.Entities.Persona1>();
        lstConsulta = personaService.ListadoPersonas1(persona, (Usuario)Session["usuario"]);

        Xpinn.Caja.Services.TipoIdenService IdenService = new Xpinn.Caja.Services.TipoIdenService();
        Xpinn.Caja.Entities.TipoIden identi = new Xpinn.Caja.Entities.TipoIden();
        Usuario usuario = new Usuario();
        usuario = (Usuario)Session["Usuario"];
        ddlTipoIdentificacion.DataSource = IdenService.ListarTipoIden(identi, usuario);
        ddlTipoIdentificacion.DataTextField = "descripcion";
        ddlTipoIdentificacion.DataValueField = "codtipoidentificacion";
        ddlTipoIdentificacion.DataBind();

        Xpinn.Tesoreria.Services.AreasCajService areasCajServicio = new Xpinn.Tesoreria.Services.AreasCajService();
        Xpinn.Tesoreria.Entities.AreasCaj areasCaj = new Xpinn.Tesoreria.Entities.AreasCaj();
        ddlArea.DataValueField = "idarea";
        ddlArea.DataTextField = "nombre";
        ddlArea.DataSource = areasCajServicio.ListarAreasCaj(areasCaj, (Usuario)Session["usuario"]);
        ddlArea.DataBind();

        Xpinn.Tesoreria.Services.TipSopCajService tipoCajServicio = new Xpinn.Tesoreria.Services.TipSopCajService();
        Xpinn.Tesoreria.Entities.TipSopCaj tipoCaj = new Xpinn.Tesoreria.Entities.TipSopCaj();
        ddlTipo.DataValueField = "idtiposop";
        ddlTipo.DataTextField = "descripcion";
        ddlTipo.DataSource = tipoCajServicio.ListarTipSopCaj(tipoCaj, (Usuario)Session["usuario"]);
        ddlTipo.DataBind();
    }
    private bool ValidarDatos()
    {
        if (string.IsNullOrEmpty(txtFecha.Text))
        {
            VerError("Ingrese la fecha de registro");
            return false;
        }
        if (ddlArea.SelectedIndex <= 0)
        {
            VerError("Seleccione el area de caja menor");
            return false;
        }
        if (string.IsNullOrEmpty(txtCod_Persona.Text))
        {
            VerError("No se asignó el código de la persona");
            return false;
        }
        if (txtCod_Persona.Text == "0")
        {
            VerError("No se asignó el código de la persona");
            return false;
        }
        if (string.IsNullOrEmpty(txtIdentificacion.Text))
        {
            VerError("Ingrese una identificación valida");
            return false;
        }
        if (ddlTipo.SelectedItem == null)
        {
            VerError("No se encontraron conceptos registrados.");
            return false;
        }
        if (txtValor.Text == "0")
        {
            VerError("Ingrese un valor valido");
            return false;
        }
        Xpinn.Tesoreria.Entities.AreasCaj vAreasCaj = new Xpinn.Tesoreria.Entities.AreasCaj();
        vAreasCaj = AreasCajaServicio.ConsultarCajaMenor(Convert.ToInt32(Usuario.codusuario), Usuario);
        decimal valor = Convert.ToDecimal(_stringHelper.DesformatearNumerosEnteros(txtValor.Text));

        // if (valor >= Convert.ToDecimal(vAreasCaj.base_valor))
        // {
        //   VerError("El valor a ingresar es mayor o igual al valor base de la caja menor");
        //   return false;
        //   }
        if (string.IsNullOrEmpty(txtDescripcion.Text))
        {
            VerError("Ingrese un detalle del recibo");
            return false;
        }

        return true;
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        if (ValidarDatos())
        {
            ctlMensaje.MostrarMensaje("Desea guardar los datos del soporte de caja menor?");
            VerError("");
        }
    }

    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        try
        {
            Int64 valorAnterior = 0;
            Xpinn.Tesoreria.Entities.SoporteCaj vSoporteCaj = new Xpinn.Tesoreria.Entities.SoporteCaj();
            Xpinn.Tesoreria.Entities.AreasCaj vAreasCaj = AreasCajaServicio.ConsultarCajaMenor(Convert.ToInt32(Usuario.codusuario), Usuario);
            Int64 usuariocaja = Convert.ToInt64(vAreasCaj.cod_usuario);
            Int64 usuarioingreso = Convert.ToInt64(Usuario.codusuario);

            if (usuarioingreso == usuariocaja)
            {
                if (idObjeto != "")
                {
                    vSoporteCaj = SoporteCajServicio.ConsultarSoporteCaj(Convert.ToInt64(idObjeto), Usuario);
                    valorAnterior = Convert.ToInt64(vSoporteCaj.valor);
                }

                vSoporteCaj.idsoporte = Convert.ToInt64(txtCodigo.Text);
                if (txtCod_Persona.Text != "")
                    vSoporteCaj.cod_per = Convert.ToInt64(txtCod_Persona.Text);
                else
                    vSoporteCaj.cod_per = 0;
                vSoporteCaj.fecha = txtFecha.ToDateTime;
                vSoporteCaj.valor = Convert.ToDecimal(_stringHelper.DesformatearNumerosEnteros(txtValor.Text));

                if (idObjeto == "")
                    vSoporteCaj.estado = "1";
                if (ddlVale.SelectedIndex == 0 || ddlVale.SelectedIndex == 2)
                    vSoporteCaj.idtiposop = Convert.ToInt32(ddlTipo.SelectedValue);

                //Verificar si es vale provisional
                vSoporteCaj.vale_prov = ddlVale.SelectedIndex.ToString();
                if (idObjeto == "")
                    vSoporteCaj.num_comp = -1;
                if (idObjeto == "")
                    vSoporteCaj.tipo_comp = -1;
                vSoporteCaj.descripcion = txtDescripcion.Text;
                if (idObjeto == "")
                    vSoporteCaj.cod_oficina = Usuario.cod_oficina;
                if (idObjeto == "")
                    vSoporteCaj.cod_usuario = Usuario.codusuario;
                vSoporteCaj.idarea = Convert.ToInt32(ddlArea.SelectedValue.Trim());
                vSoporteCaj.nombre = Convert.ToString(txtDescripcion.Text.Trim());
                vSoporteCaj.id_arqueo = null;

                if (idObjeto != "")
                {
                    vSoporteCaj.idsoporte = Convert.ToInt32(idObjeto);
                    if (vAreasCaj.saldo_caja != vAreasCaj.base_valor)
                    {
                        vAreasCaj.saldo_caja = vAreasCaj.saldo_caja + valorAnterior;
                    }
                    vAreasCaj.saldo_caja = vAreasCaj.saldo_caja - Convert.ToInt64(vSoporteCaj.valor);
                    if (vAreasCaj.saldo_caja < vAreasCaj.valor_minimo)
                        VerError("No se puede registrar el soporte, el saldo de la caja es igual al monto mínimo");
                    else
                    {
                        SoporteCajServicio.ModificarSoporteCaj(vSoporteCaj, (Usuario)Session["usuario"]);
                    }
                }
                else
                {
                    //Agregado para actualizar el saldo de la caja menor
                    vAreasCaj.saldo_caja = vAreasCaj.saldo_caja - Convert.ToInt64(vSoporteCaj.valor);
                    if (vAreasCaj.saldo_caja < vAreasCaj.valor_minimo)
                        VerError("No se puede registrar el soporte, el saldo de la caja es igual al monto mínimo");
                    else
                    {
                        vSoporteCaj = SoporteCajServicio.CrearSoporteCaj(vSoporteCaj, (Usuario)Session["usuario"]);
                        idObjeto = vSoporteCaj.idsoporte.ToString();
                        //  vAreasCaj = AreasCajaServicio.ModificarAreasCaj(vAreasCaj, (Usuario)Session["usuario"]);
                    }

                }

                Session[SoporteCajServicio.CodigoPrograma + ".id"] = idObjeto;
                Site toolBar = (Site)this.Master;
                toolBar.MostrarGuardar(false);
                toolBar.MostrarImprimir(true);
                toolBar.MostrarConsultar(true);
                mvCajaMenor.ActiveViewIndex = 1;
            }

            else
            {
                VerError("El usuario que esta registrando el recibo no es cajero o no pertenece a esta caja ");

            }

        }

        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(SoporteCajServicio.CodigoPrograma, "btnGuardar_Click", ex);
        }
    }

    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }

    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            Xpinn.Tesoreria.Entities.SoporteCaj vSoporteCaj = new Xpinn.Tesoreria.Entities.SoporteCaj();
            vSoporteCaj = SoporteCajServicio.ConsultarSoporteCaj(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);

            Xpinn.Caja.Entities.Persona persona = new Xpinn.Caja.Entities.Persona();
            Xpinn.Caja.Services.PersonaService personaService = new Xpinn.Caja.Services.PersonaService();
            persona.cod_persona = Convert.ToInt64(vSoporteCaj.cod_per);
            persona = personaService.ConsultarPersonaXCodigo(persona, (Usuario)Session["usuario"]);
            Session["cod_ofi"] = vSoporteCaj.cod_oficina;
            if (!string.IsNullOrEmpty(vSoporteCaj.idsoporte.ToString()))
                txtCodigo.Text = HttpUtility.HtmlDecode(vSoporteCaj.idsoporte.ToString());
            if (!string.IsNullOrEmpty(persona.tipo_identificacion.ToString()))
                txtIdentificacion.Text = HttpUtility.HtmlDecode(persona.identificacion);
            if (!string.IsNullOrEmpty(persona.cod_persona.ToString()))
                txtCod_Persona.Text = HttpUtility.HtmlDecode(persona.cod_persona.ToString());
            if (!string.IsNullOrEmpty(persona.tipo_identificacion.ToString()))
                ddlTipoIdentificacion.SelectedValue = HttpUtility.HtmlDecode(persona.tipo_identificacion.ToString());
            if (!string.IsNullOrEmpty(persona.nom_persona.ToString()))
                txtNombres.Text = HttpUtility.HtmlDecode(persona.nom_persona);
            if (!string.IsNullOrEmpty(vSoporteCaj.fecha.ToString()))
                txtFecha.ToDateTime = Convert.ToDateTime(HttpUtility.HtmlDecode(vSoporteCaj.fecha.ToString()));
            if (!string.IsNullOrEmpty(vSoporteCaj.valor.ToString()))
                txtValor.Text = HttpUtility.HtmlDecode(vSoporteCaj.valor.ToString());
            if (vSoporteCaj.nomestado != null)
                if (!string.IsNullOrEmpty(vSoporteCaj.nomestado.ToString()))
                    txtEstado.Text = HttpUtility.HtmlDecode(vSoporteCaj.nomestado.ToString());
            if (!string.IsNullOrEmpty(vSoporteCaj.idtiposop.ToString()))
                ddlTipo.SelectedValue = HttpUtility.HtmlDecode(vSoporteCaj.idtiposop.ToString());
            if (!string.IsNullOrEmpty(vSoporteCaj.num_comp.ToString()))
                txtNumComp.Text = HttpUtility.HtmlDecode(vSoporteCaj.num_comp.ToString());
            if (!string.IsNullOrEmpty(vSoporteCaj.tipo_comp.ToString()))
                txtTipoComp.Text = HttpUtility.HtmlDecode(vSoporteCaj.tipo_comp.ToString());
            if (!string.IsNullOrEmpty(vSoporteCaj.descripcion.ToString()))
                txtDescripcion.Text = HttpUtility.HtmlDecode(vSoporteCaj.descripcion.ToString());
            if (!string.IsNullOrEmpty(vSoporteCaj.nomoficina.ToString()))
                txtOficina.Text = HttpUtility.HtmlDecode(vSoporteCaj.nomoficina.ToString());
            if (!string.IsNullOrEmpty(vSoporteCaj.idarea.ToString()))
                ddlArea.SelectedValue = HttpUtility.HtmlDecode(vSoporteCaj.idarea.ToString());
            if (!string.IsNullOrEmpty(vSoporteCaj.nomusuario.ToString()))
                txtUsuario.Text = HttpUtility.HtmlDecode(vSoporteCaj.nomusuario.ToString());
            if (!string.IsNullOrEmpty(vSoporteCaj.vale_prov.ToString()))
                ddlVale.SelectedIndex = Convert.ToInt32(HttpUtility.HtmlDecode(vSoporteCaj.vale_prov));
            if (ddlVale.SelectedIndex == 0 || ddlVale.SelectedIndex == 2)
                ddlTipo.Enabled = true;
            else if (ddlVale.SelectedIndex == 1)
                ddlTipo.Enabled = false;


            Site toolBar = (Site)this.Master;
            toolBar.MostrarImprimir(true);

            // Si el soporte ya esta contabilizado no dejar modificar o si ya fue registrado en un  arqueo
            if (vSoporteCaj.estado != "1" || vSoporteCaj.id_arqueo != null)
            {
                txtCodigo.Enabled = false;
                txtIdentificacion.Enabled = false;
                txtNombres.Enabled = false;
                ddlTipoIdentificacion.Enabled = false;
                txtFecha.Enabled = false;
                txtValor.Enabled = false;
                ddlTipo.Enabled = false;
                txtDescripcion.Enabled = false;
                ddlArea.Enabled = false;
                ddlVale.Enabled = false;
                Site toolBar1 = (Site)this.Master;
                toolBar1.MostrarGuardar(false);
            }

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(SoporteCajServicio.CodigoPrograma, "ObtenerDatos", ex);
        }
    }


    /*protected void ddlIdentificacion_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlNombre.SelectedValue = ddlIdentificacion.SelectedValue;
    }

    protected void ddlNombre_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlIdentificacion.SelectedValue = ddlNombre.SelectedValue;
    }*/

    protected void btnInforme_Click(object sender, EventArgs e)
    {
        Usuario pUsuario = (Usuario)Session["usuario"];

        Xpinn.Caja.Services.OficinaService oficinaServicio = new Xpinn.Caja.Services.OficinaService();
        Xpinn.Caja.Entities.Oficina poficina = new Xpinn.Caja.Entities.Oficina();
        poficina = oficinaServicio.ConsultarOficina(pUsuario.cod_oficina, pUsuario);

        Xpinn.Asesores.Entities.Ciudad Ciudad = new Xpinn.Asesores.Entities.Ciudad();
        Xpinn.Asesores.Services.CiudadService ServiceCiudad = new Xpinn.Asesores.Services.CiudadService();
        Ciudad = ServiceCiudad.ConsultarCiudad(poficina.cod_ciudad, pUsuario);

        // Determinando el valor en letras
        Cardinalidad numero = new Cardinalidad();
        string cardinal = " ";
        if (Convert.ToString(_stringHelper.DesformatearNumerosEnteros(txtValor.Text)) != "0")
        {
            cardinal = numero.enletras(_stringHelper.DesformatearNumerosEnteros(txtValor.Text));
            int cont = cardinal.Length - 1;
            int cont2 = cont - 7;
            if (cont2 >= 0)
            {
                string c = cardinal.Substring(cont2);
                if (cardinal.Substring(cont2) == "MILLONES" || cardinal.Substring(cont2 + 2) == "MILLON")
                    cardinal = cardinal + " DE PESOS M/CTE";
                else
                    cardinal = cardinal + " PESOS M/CTE";
            }
        }

        // Parámetros del comprobante
        ReportParameter[] param = new ReportParameter[23];
        param[0] = new ReportParameter("empresa", pUsuario.empresa);
        param[1] = new ReportParameter("nitempresa", pUsuario.nitempresa);
        param[2] = new ReportParameter("idsoporte", txtCodigo.Text);
        param[3] = new ReportParameter("cod_per", txtCod_Persona.Text);
        param[4] = new ReportParameter("nombre", txtNombres.Text);
        param[5] = new ReportParameter("fecha", txtFecha.ToDateTime.ToString());
        param[6] = new ReportParameter("valor", Convert.ToString(_stringHelper.DesformatearNumerosEnteros(txtValor.Text)));
        param[7] = new ReportParameter("estado", txtEstado.Text);
        param[8] = new ReportParameter("nomestado", txtEstado.Text);
        param[9] = new ReportParameter("idtiposop", ddlTipo.SelectedItem.Value);
        param[10] = new ReportParameter("nomtiposop", ddlTipo.SelectedItem.Text);
        if (txtNumComp.Text != "")
            param[11] = new ReportParameter("num_comp", txtNumComp.Text);
        else
            param[11] = new ReportParameter("num_comp", "0");
        if (txtTipoComp.Text != "")
            param[12] = new ReportParameter("tipo_comp", txtTipoComp.Text);
        else
            param[12] = new ReportParameter("tipo_comp", "0");
        param[13] = new ReportParameter("nomtipo_comp", txtTipoComp.Text);
        param[14] = new ReportParameter("descripcion", txtDescripcion.Text);
        param[15] = new ReportParameter("cod_oficina", "0");
        param[16] = new ReportParameter("nomoficina", txtOficina.Text + " " + Ciudad.nomciudad);
        param[17] = new ReportParameter("cod_usuario", "0");
        param[18] = new ReportParameter("nomusuario", txtUsuario.Text);
        param[19] = new ReportParameter("idarea", ddlArea.SelectedItem.Value);
        param[20] = new ReportParameter("nomarea", ddlArea.SelectedItem.Text);
        param[21] = new ReportParameter("valor_letras", cardinal);
        param[22] = new ReportParameter("ImagenReport", ImagenReporte());
        RpviewRecibo.LocalReport.EnableExternalImages = true;
        RpviewRecibo.LocalReport.SetParameters(param);

        RpviewRecibo.LocalReport.Refresh();
        if (sender != null)
        {
            RpviewRecibo.Visible = true;
            mvCajaMenor.Visible = true;
            mvCajaMenor.ActiveViewIndex = 2;
        }

        if (idObjeto == "")
            btnRegresarComp.Visible = false;
        else
            btnRegresarComp.Visible = true;
    }

    protected void btnImprimir_Click(object sender, EventArgs e)
    {

        btnInforme_Click(null, null);
        // VARIABLES
        byte[] bytes = RpviewRecibo.LocalReport.Render("PDF");
        string cNombreDeArchivo = txtCod_Persona.Text + "_" + txtIdentificacion.Text + ".pdf";
        string cRutaLocalDeArchivoPDF = Server.MapPath("Archivo\\" + cNombreDeArchivo);

        if (File.Exists(cRutaLocalDeArchivoPDF))
            File.Delete(cRutaLocalDeArchivoPDF);

        FileStream fs = new FileStream(cRutaLocalDeArchivoPDF, FileMode.Create);
        fs.Write(bytes, 0, bytes.Length);
        fs.Close();

        FileInfo file = new FileInfo(cRutaLocalDeArchivoPDF);
        Response.Clear();
        Response.AppendHeader("Content-Disposition", "attachment; filename=SoporteCajaMenor" + txtIdentificacion.Text + ".pdf");
        Response.AddHeader("Content-Length", file.Length.ToString());
        Response.ContentType = "application/pdf";
        Response.TransmitFile(file.FullName);
        Response.End();
    }

    protected void btnRegresarComp_Click(object sender, EventArgs e)
    {
        mvCajaMenor.ActiveViewIndex = 0;
    }

    protected void ddlVale_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlVale.SelectedIndex == 0)
        {
            ddlTipo.Enabled = true;
        }
        else if (ddlVale.SelectedIndex == 1)
        {
            ddlTipo.Enabled = false;
        }
        else if (ddlVale.SelectedIndex == 2)
        {
            ddlTipo.Enabled = true;
        }
    }

    //Agregado para hacer uso del control de personas
    protected void txtIdentificacion_TextChanged(object sender, EventArgs e)
    {
        VerError("");
        txtCod_Persona.Text = string.Empty;
        txtNombres.Text = string.Empty;
        ddlTipoIdentificacion.SelectedIndex = 0;
        txtIdentificacion.Text = txtIdentificacion.Text.Trim();
        if (!string.IsNullOrEmpty(txtIdentificacion.Text))
        {
            Xpinn.Caja.Entities.Persona persona = new Xpinn.Caja.Entities.Persona();
            Xpinn.Caja.Services.PersonaService personaService = new Xpinn.Caja.Services.PersonaService();
            persona.identificacion = txtIdentificacion.Text;
            persona = personaService.ConsultarPersona(persona, (Usuario)Session["usuario"]);
            if (string.IsNullOrEmpty(persona.mensajer_error))
            {
                txtCod_Persona.Text = persona.cod_persona.ToString();
                txtNombres.Text = persona.nom_persona;
                ddlTipoIdentificacion.SelectedValue = persona.tipo_identificacion.ToString();
            }
            else
                VerError(persona.mensajer_error);
        }
    }

    protected void btnConsultaPersonas_Click(object sender, EventArgs e)
    {
        ctlBusquedaPersonas.Motrar(true, "txtCod_Persona", "txtIdentificacion", "ddlTipoIdentificacion", "txtNombres");
    }

    protected void ddlArea_SelectedIndexChanged(object sender, EventArgs e)
    {

        Xpinn.Tesoreria.Entities.SoporteCaj vSoporteCaj = new Xpinn.Tesoreria.Entities.SoporteCaj();
        Xpinn.Tesoreria.Entities.AreasCaj vAreasCaj = AreasCajaServicio.ConsultarAreasCaj(Convert.ToInt32(ddlArea.SelectedValue), Usuario);
        Int64 usuariocaja = Convert.ToInt64(vAreasCaj.cod_usuario);
        Int64 usuarioingreso = Convert.ToInt64(Usuario.codusuario);

        if (usuarioingreso == usuariocaja)
        {
            txtCodigo.Text = SoporteCajServicio.ObtenerSiguienteCodigo((Usuario)Session["usuario"]).ToString();
            txtEstado.Text = "Elaborado";
            txtNumComp.Visible = false;
            lblNumComp.Visible = false;
            txtTipoComp.Visible = false;
            lblTipoComp.Visible = false;
            Usuario pUsuario = new Usuario();
            pUsuario = (Usuario)Session["usuario"];
            Xpinn.Caja.Services.OficinaService oficinaServicio = new Xpinn.Caja.Services.OficinaService();
            Xpinn.Caja.Entities.Oficina poficina = new Xpinn.Caja.Entities.Oficina();
            poficina = oficinaServicio.ConsultarOficina(pUsuario.cod_oficina, pUsuario);
            txtOficina.Text = poficina.nombre;
            txtUsuario.Text = pUsuario.nombre;
            Session["cod_ciudad"] = poficina.cod_ciudad;
            Configuracion conf = new Configuracion();
            txtFecha.ToDateTime = System.DateTime.Now;
        }


        else
        {
            VerError("El usuario que esta registrando el recibo no es cajero o no pertenece a esta caja ");
            Site toolBar = (Site)this.Master;
            toolBar.MostrarGuardar(false);
        }

    }
}



