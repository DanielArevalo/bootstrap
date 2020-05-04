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
using Xpinn.FabricaCreditos.Entities;

partial class Nuevo : GlobalWeb
{
    private Xpinn.Seguridad.Services.UsuarioService UsuarioServicio = new Xpinn.Seguridad.Services.UsuarioService();
    string claveOculta = "********";


    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[UsuarioServicio.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(UsuarioServicio.CodigoPrograma, "E");
            else
                VisualizarOpciones(UsuarioServicio.CodigoPrograma, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(UsuarioServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                CargarPerfil();
                txtFechacreacion.Text = DateTime.Now.ToShortDateString();
                CargarDllOficinas();
                CrearDireccionInicial();
                CrearDireccionMacInicial();
                if (Session[UsuarioServicio.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[UsuarioServicio.CodigoPrograma + ".id"].ToString();
                    Session.Remove(UsuarioServicio.CodigoPrograma + ".id");
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
            BOexcepcion.Throw(UsuarioServicio.CodigoPrograma, "Page_Load", ex);
        }
    }

    private void CargarDllOficinas()
    {
        List<Xpinn.Caja.Entities.Oficina> lstoficina = new List<Xpinn.Caja.Entities.Oficina>();
        Xpinn.Caja.Entities.Oficina oficina = new Xpinn.Caja.Entities.Oficina();
        Xpinn.Caja.Services.OficinaService oficinaservicio = new Xpinn.Caja.Services.OficinaService();
        lstoficina = oficinaservicio.ListarOficina(oficina, (Usuario)Session["usuario"]);
        txtCod_oficina.DataTextField = "nombre";
        txtCod_oficina.DataValueField = "cod_oficina";
        txtCod_oficina.DataSource = lstoficina;
        txtCod_oficina.DataBind();
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            Xpinn.Util.Usuario vUsuario = new Xpinn.Util.Usuario();

            if (idObjeto != "")
                vUsuario = UsuarioServicio.ConsultarUsuario(Convert.ToInt64(idObjeto), (Usuario)Session["usuario"]);

            vUsuario.identificacion = Convert.ToString(txtIdentificacion.Text.Trim());
            vUsuario.cod_persona = !string.IsNullOrEmpty(txtCod_persona.Text) ? Convert.ToInt64(txtCod_persona.Text) : 0 ;
            vUsuario.nombre = Convert.ToString(txtNombre.Text.Trim());
            vUsuario.direccion = Convert.ToString(txtDireccion.Text.Trim());
            vUsuario.telefono = Convert.ToString(txtTelefono.Text.Trim());
            if (txtLogin.Text == claveOculta)
            { 
                vUsuario.login = Convert.ToString(lblClave.Text.Trim());
                vUsuario.clave = Convert.ToString(lblClave.Text.Trim());
            }
            else
            {
                vUsuario.login = Convert.ToString(txtLogin.Text.Trim());
                vUsuario.clave = Convert.ToString(txtLogin.Text.Trim());
            }
            try
            {
                vUsuario.fechacreacion = DateTime.ParseExact(txtFechacreacion.Text.Trim(), gFormatoFecha, null);
            }
            catch
            {
                vUsuario.fechacreacion = DateTime.Now;
            }
            vUsuario.estado = Convert.ToInt64(txtEstado.Text.Trim());
            vUsuario.codperfil = Convert.ToInt64(txtCodperfil.Text.Trim());
            vUsuario.cod_oficina = Convert.ToInt64(txtCod_oficina.Text.Trim());

            DataTable dtDireccionip = new DataTable();
            dtDireccionip = (DataTable)Session["direccionip"];
            vUsuario.LstIP = new List<string>();
            vUsuario.LstIP.Clear();
            foreach (DataRow sDireccionIp in dtDireccionip.Rows)
            {
                if (sDireccionIp[0].ToString().Trim() != "" && sDireccionIp[0].ToString().Trim() != "0")
                    vUsuario.LstIP.Add(sDireccionIp[0].ToString());
            }

            DataTable dtDireccionMac = new DataTable();
            dtDireccionMac = (DataTable)Session["direccionMac"];
            vUsuario.LstMac = new List<string>();
            vUsuario.LstMac.Clear();
            foreach (DataRow sDireccionMac in dtDireccionMac.Rows)
            {
                if (sDireccionMac[0].ToString().Trim() != "" && sDireccionMac[0].ToString().Trim() != "0")
                    vUsuario.LstMac.Add(sDireccionMac[0].ToString());
            }

            if (vUsuario.LstAtribuciones == null)
                vUsuario.LstAtribuciones = new List<int>();
            vUsuario.LstAtribuciones.Clear();
            for (int i = 0; i <= 7; i += 1)
            {
                vUsuario.LstAtribuciones.Add(0);
            }
            if (ckbTipoAtribucion0.Checked == true)
                vUsuario.LstAtribuciones[0] = 1;
            else
                vUsuario.LstAtribuciones[0] = 0;
            if (ckbTipoAtribucion1.Checked == true)
                vUsuario.LstAtribuciones[1] = 1;
            else
                vUsuario.LstAtribuciones[1] = 0;
            if (ckbTipoAtribucion2.Checked == true)
                vUsuario.LstAtribuciones[2] = 1;
            else
                vUsuario.LstAtribuciones[2] = 0;
            if (ckbTipoAtribucion3.Checked == true)
                vUsuario.LstAtribuciones[3] = 1;
            else
                vUsuario.LstAtribuciones[3] = 0;
            if (ckbTipoAtribucion4.Checked == true)
                vUsuario.LstAtribuciones[4] = 1;
            else
                vUsuario.LstAtribuciones[4] = 0;
            if (ckbTipoAtribucion5.Checked == true)
                vUsuario.LstAtribuciones[5] = 1;
            else
                vUsuario.LstAtribuciones[5] = 0;
            if (ckbTipoAtribucion6.Checked == true)
                vUsuario.LstAtribuciones[6] = 1;
            else
                vUsuario.LstAtribuciones[6] = 0;

            vUsuario.LstAtribuciones[7] = ckbTipoAtribucion7.Checked ? 1 : 0;


            vUsuario.cod_cuenta = txtCodCuenta.Text;



            if (idObjeto != "")
            {
                vUsuario.codusuario = Convert.ToInt64(idObjeto);
                UsuarioServicio.ModificarUsuario(vUsuario, (Usuario)Session["usuario"]);
            }
            else
            {
                vUsuario = UsuarioServicio.CrearUsuario(vUsuario, (Usuario)Session["usuario"]);
                idObjeto = vUsuario.codusuario.ToString();
            }

            Session[UsuarioServicio.CodigoPrograma + ".id"] = idObjeto;
            Navegar(Pagina.Detalle);
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
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
            Session[UsuarioServicio.CodigoPrograma + ".id"] = idObjeto;
            Navegar(Pagina.Detalle);
        }
    }

    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            Xpinn.Util.Usuario vUsuario = new Xpinn.Util.Usuario();
            vUsuario = UsuarioServicio.ConsultarUsuario(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);

            if (!string.IsNullOrEmpty(vUsuario.identificacion))
                txtIdentificacion.Text = HttpUtility.HtmlDecode(vUsuario.identificacion.ToString().Trim());
            if (!string.IsNullOrEmpty(vUsuario.documento))
                txtIdentdoc.Text = HttpUtility.HtmlDecode(vUsuario.documento.ToString().Trim());
            if (!string.IsNullOrEmpty(vUsuario.cod_persona.ToString()))
                txtCod_persona.Text = HttpUtility.HtmlDecode(vUsuario.cod_persona.ToString().Trim());
            if (!string.IsNullOrEmpty(vUsuario.nombre))
                txtNombre.Text = HttpUtility.HtmlDecode(vUsuario.nombre.ToString().Trim());
            if (!string.IsNullOrEmpty(vUsuario.direccion))
                txtDireccion.Text = HttpUtility.HtmlDecode(vUsuario.direccion.ToString().Trim());
            if (!string.IsNullOrEmpty(vUsuario.telefono))
                txtTelefono.Text = HttpUtility.HtmlDecode(vUsuario.telefono.ToString().Trim());
            if (!string.IsNullOrEmpty(vUsuario.login))
            {            
                txtLogin.Attributes.Add("Value", claveOculta);
                lblClave.Text = vUsuario.login.Trim();
            }
            if (!string.IsNullOrEmpty(vUsuario.login))                
            {
                txtLoginConf.Attributes.Add("Value", claveOculta);
                lblConfirmaClave.Text = vUsuario.login.Trim();
            }
            if (vUsuario.fechacreacion != DateTime.MinValue)
                txtFechacreacion.Text = HttpUtility.HtmlDecode(vUsuario.fechacreacion.ToShortDateString());
            if (vUsuario.estado != Int64.MinValue)
                txtEstado.Text = HttpUtility.HtmlDecode(vUsuario.estado.ToString().Trim());
            if (vUsuario.codperfil != Int64.MinValue)
                txtCodperfil.Text = HttpUtility.HtmlDecode(vUsuario.codperfil.ToString().Trim());
            if (vUsuario.cod_oficina != Int64.MinValue)
                txtCod_oficina.Text = HttpUtility.HtmlDecode(vUsuario.cod_oficina.ToString().Trim());

            if (vUsuario.cod_cuenta != null)
                txtCodCuenta.Text = vUsuario.cod_cuenta;
            txtCodCuenta_TextChanged(txtCodCuenta, null);

            DataTable dtDireccionip = new DataTable();
            if (vUsuario.LstIP.Count() > 0)
            {
                dtDireccionip.Columns.Add("direccionip");
                foreach (string direccionIp in vUsuario.LstIP)
                {
                    dtDireccionip.Rows.Add(direccionIp);
                }
                Session["direccionip"] = dtDireccionip;
                gvIPS.DataSource = dtDireccionip;
                gvIPS.DataBind();
            }
            else
            {
                Session["direccionip"] = dtDireccionip;
                CrearDireccionInicial();
            }

            DataTable dtDireccionMac = new DataTable();
            if (vUsuario.LstMac.Count() > 0)
            {
                dtDireccionMac.Columns.Add("direccionMac");
                foreach (string direccionMac in vUsuario.LstMac)
                {
                    dtDireccionMac.Rows.Add(direccionMac);
                }
                Session["direccionMac"] = dtDireccionMac;
                gvMac.DataSource = dtDireccionMac;
                gvMac.DataBind();
            }
            else
            {
                Session["direccionMac"] = dtDireccionMac;
                CrearDireccionMacInicial();
            }

            if (vUsuario.LstAtribuciones.Count() > 0)
            {
                if (vUsuario.LstAtribuciones[0] == 1)
                    ckbTipoAtribucion0.Checked = true;
                else
                    ckbTipoAtribucion0.Checked = false;
                if (vUsuario.LstAtribuciones[1] == 1)
                    ckbTipoAtribucion1.Checked = true;
                else
                    ckbTipoAtribucion1.Checked = false;
                if (vUsuario.LstAtribuciones[2] == 1)
                    ckbTipoAtribucion2.Checked = true;
                else
                    ckbTipoAtribucion2.Checked = false;
                if (vUsuario.LstAtribuciones[3] == 1)
                    ckbTipoAtribucion3.Checked = true;
                else
                    ckbTipoAtribucion3.Checked = false;
                //Adicionado
                if (vUsuario.LstAtribuciones[4] == 1)
                    ckbTipoAtribucion4.Checked = true;
                else
                    ckbTipoAtribucion4.Checked = false;
                if (vUsuario.LstAtribuciones[5] == 1)
                    ckbTipoAtribucion5.Checked = true;
                else
                    ckbTipoAtribucion5.Checked = false;
                if (vUsuario.LstAtribuciones[6] == 1)
                    ckbTipoAtribucion6.Checked = true;
                else
                    ckbTipoAtribucion6.Checked = false;

                ckbTipoAtribucion7.Checked = vUsuario.LstAtribuciones[7] == 1 ? true : false;

            }

            Xpinn.Seguridad.Entities.Perfil vPerfil = new Xpinn.Seguridad.Entities.Perfil();
            Xpinn.Seguridad.Services.PerfilService perfilServicio = new Xpinn.Seguridad.Services.PerfilService();
            Usuario usu = (Usuario)Session["usuario"];
            vPerfil = perfilServicio.ConsultarPerfil(usu.codperfil, usu);

            if (vPerfil.es_administrador == 1)
                txtEstado.Enabled = true;
            else
                txtEstado.Enabled = false;

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(UsuarioServicio.CodigoPrograma, "ObtenerDatos", ex);
        }
    }

    private void CargarPerfil()
    {
        try
        {
            Xpinn.Seguridad.Services.PerfilService perfilServicio = new Xpinn.Seguridad.Services.PerfilService();
            List<Xpinn.Seguridad.Entities.Perfil> lstPerfil = new List<Xpinn.Seguridad.Entities.Perfil>();
            Usuario pUsuario = new Usuario();
            pUsuario = (Usuario)Session["usuario"];

            lstPerfil = perfilServicio.ListarPerfil(null, pUsuario);

            if (pUsuario.codperfil != 1)
                lstPerfil.Remove(lstPerfil.Where(x => x.codperfil == 1).FirstOrDefault());

            txtCodperfil.DataSource = lstPerfil;
            txtCodperfil.DataTextField = "nombreperfil";
            txtCodperfil.DataValueField = "codperfil";
            txtCodperfil.DataBind();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(UsuarioServicio.CodigoPrograma, "CargarPerfil", ex);
        }
    }

    protected void gvIPS_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        VerError("");
        if (e.CommandName.Equals("AddNew"))
        {
            TextBox txtDireccionIP = (TextBox)gvIPS.FooterRow.FindControl("txtDireccionIP");

            DataTable dtAgre = new DataTable();
            dtAgre = (DataTable)Session["direccionip"];

            if (dtAgre.Rows[0][0] == null || dtAgre.Rows[0][0].ToString() == "")
            {
                dtAgre.Rows[0].Delete();
            }

            DataRow fila = dtAgre.NewRow();

            fila[0] = txtDireccionIP.Text;
            dtAgre.Rows.Add(fila);
            gvIPS.DataSource = dtAgre;
            gvIPS.DataBind();
            Session["direccionip"] = dtAgre;
        }
    }



    protected void gvMac_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        VerError("");
        if (e.CommandName.Equals("AddNew"))
        {
            TextBox txtDireccionMac = (TextBox)gvMac.FooterRow.FindControl("txtDireccionMac");

            DataTable dtAgre = new DataTable();
            dtAgre = (DataTable)Session["direccionmac"];

            if (dtAgre.Rows[0][0] == null || dtAgre.Rows[0][0].ToString() == "")
            {
                dtAgre.Rows[0].Delete();
            }

            DataRow fila = dtAgre.NewRow();

            fila[0] = txtDireccionMac.Text;
            dtAgre.Rows.Add(fila);
            gvMac.DataSource = dtAgre;
            gvMac.DataBind();
            Session["direccionmac"] = dtAgre;
        }
    }

    protected void gvIPS_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            // Se puede colocar alguna validación al cargar la fila
        }
    }


    protected void gvMac_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            // Se puede colocar alguna validación al cargar la fila
        }
    }
    protected void gvIPS_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            DataTable table = new DataTable();
            table = (DataTable)Session["direccionip"];

            if ((e.RowIndex == 0) && (table.Rows[0][0] != null || table.Rows[0][0].ToString() == "") && (table.Rows.Count == 1))
            {
                table.Rows.Add();
            }

            table.Rows[e.RowIndex].Delete();
            gvIPS.DataSource = table;
            gvIPS.DataBind();
            Session["direccionip"] = table;

            if ((e.RowIndex == 0) && (table.Rows[0][0] == null || table.Rows[0][0].ToString() == "") && (table.Rows.Count == 1))
                gvIPS.Rows[0].Visible = false;
        }
        catch (Xpinn.Util.ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(UsuarioServicio.GetType().Name + "L", "gvIPS_RowDeleting", ex);
        }

    }

    protected void gvMac_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            DataTable table = new DataTable();
            table = (DataTable)Session["direccionmac"];

            if ((e.RowIndex == 0) && (table.Rows[0][0] != null || table.Rows[0][0].ToString() == "") && (table.Rows.Count == 1))
            {
                table.Rows.Add();
            }

            table.Rows[e.RowIndex].Delete();
            gvMac.DataSource = table;
            gvMac.DataBind();
            Session["direccionmac"] = table;

            if ((e.RowIndex == 0) && (table.Rows[0][0] == null || table.Rows[0][0].ToString() == "") && (table.Rows.Count == 1))
                gvMac.Rows[0].Visible = false;
        }
        catch (Xpinn.Util.ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(UsuarioServicio.GetType().Name + "L", "gvMac_RowDeleting", ex);
        }

    }

    protected void CrearDireccionInicial()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("direccionip");
        dt.Rows.Add();
        Session["direccionip"] = dt;
        gvIPS.DataSource = dt;
        gvIPS.DataBind();
        gvIPS.Visible = true;

    }


    protected void CrearDireccionMacInicial()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("direccionMac");
        dt.Rows.Add();
        Session["direccionMac"] = dt;
        gvMac.DataSource = dt;
        gvMac.DataBind();
        gvMac.Visible = true;

    }

    protected void txtIdentificacion_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string identificacion;
            identificacion = txtIdentificacion.Text.Trim();

            Persona1 DatosPersona = new Persona1();

            DatosPersona = UsuarioServicio.ConsultarPersona1(identificacion, (Usuario)Session["usuario"]);

            if (!string.IsNullOrEmpty(DatosPersona.nombre))
                txtNombre.Text = HttpUtility.HtmlDecode(DatosPersona.nombre.ToString().Trim());
            if (!string.IsNullOrEmpty(DatosPersona.direccion))
                txtDireccion.Text = HttpUtility.HtmlDecode(DatosPersona.direccion.ToString().Trim());
            if (!string.IsNullOrEmpty(DatosPersona.telefono))
                txtTelefono.Text = HttpUtility.HtmlDecode(DatosPersona.telefono.ToString().Trim());
            if (!string.IsNullOrEmpty(DatosPersona.estado))
                txtEstado.SelectedValue = HttpUtility.HtmlDecode(DatosPersona.estado.ToString().Trim());
            if (DatosPersona.fechacreacion != null && DatosPersona.fechacreacion != DateTime.MinValue)
                txtFechacreacion.Text = HttpUtility.HtmlDecode(DatosPersona.fechacreacion.ToString().Trim());
            if (DatosPersona.cod_oficina != 0)
                txtCod_oficina.SelectedValue = HttpUtility.HtmlDecode(DatosPersona.cod_oficina.ToString().Trim());
            if (DatosPersona.cod_persona != 0)
                txtCod_persona.Text = HttpUtility.HtmlDecode(DatosPersona.cod_persona.ToString().Trim());            

        }
        catch //(Exception ex)
        {
            //BOexcepcion.Throw(UsuarioServicio.CodigoPrograma, "Filtro de Usuario", ex);
        }

    }

    protected void btnListadoPlan_Click(object sender, EventArgs e)
    {
        ctlListadoPlan.Motrar(true, "txtCodCuenta", "txtNomCuenta");
    }
    protected void txtCodCuenta_TextChanged(object sender, EventArgs e)
    {
        Xpinn.Contabilidad.Services.PlanCuentasService PlanCuentasServicio = new Xpinn.Contabilidad.Services.PlanCuentasService();
        Xpinn.Contabilidad.Entities.PlanCuentas PlanCuentas = new Xpinn.Contabilidad.Entities.PlanCuentas();
        PlanCuentas = PlanCuentasServicio.ConsultarPlanCuentas(txtCodCuenta.Text, (Usuario)Session["usuario"]);

        // Mostrar el nombre de la cuenta           
        if (txtNomCuenta != null)
            txtNomCuenta.Text = PlanCuentas.nombre;
    }



}
