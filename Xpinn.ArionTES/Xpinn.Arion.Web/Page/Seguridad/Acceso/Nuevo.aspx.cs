using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ASP;
using Xpinn.Seguridad.Services;
using Xpinn.Seguridad.Entities;
using Xpinn.Util;

partial class Nuevo : GlobalWeb
{
    private Xpinn.Seguridad.Services.PerfilService perfilServicio = new Xpinn.Seguridad.Services.PerfilService();
    List<Xpinn.Seguridad.Entities.Acceso> lstAccesos = new List<Xpinn.Seguridad.Entities.Acceso>();
    ImageButton btnInfo;
    private CamposPermiso campos = new CamposPermiso();
    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[perfilServicio.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(perfilServicio.CodigoPrograma, "E");
            else
                VisualizarOpciones(perfilServicio.CodigoPrograma, "A");
            ctlMensaje.eventoClick += btnContinuar_Click;
            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGrabar_Click;
            toolBar.eventoRegresar += btnCancelar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(perfilServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                CargarModulo();
                if (Session[perfilServicio.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[perfilServicio.CodigoPrograma + ".id"].ToString();
                    ObtenerDatos(idObjeto);
                }
                else { txtCodigo.Text = perfilServicio.ObtenerSiguienteCodigo((Usuario)Session["usuario"]).ToString(); }
                CargarOpciones(idObjeto, ddlModulo.SelectedValue);
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(perfilServicio.CodigoPrograma, "Page_Load", ex);
        }
    }


    protected void CargarOpciones(string sidPerfil, string sidModulo)
    {
        Int64 idPerfil = 0;
        if (sidPerfil.Trim() != "")
            idPerfil = Convert.ToInt64(sidPerfil);
        lstAccesos = perfilServicio.ListarOpciones(idPerfil, Convert.ToInt64(sidModulo), (Usuario)Session["Usuario"]);
        gvLista.DataSource = lstAccesos;
        gvLista.DataBind();
    }

    /// <summary>
    /// Crear los datos del perfil
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnGrabar_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Usuario pUsuario = new Usuario();
            pUsuario = (Usuario)Session["Usuario"];
            Perfil vPerfil = new Perfil();

            if (idObjeto != "")
                vPerfil = perfilServicio.ConsultarPerfil(Convert.ToInt64(idObjeto), (Usuario)Session["usuario"]);
            else
                vPerfil.codperfil = 0;

            vPerfil.codperfil = Convert.ToInt64(txtCodigo.Text);
            vPerfil.nombreperfil = txtDescripcion.Text;
            vPerfil.lstAccesos = new List<Xpinn.Seguridad.Entities.Acceso>();
            foreach (GridViewRow rFila in gvLista.Rows)
            {
                Acceso pAcceso = new Acceso();
                pAcceso.cod_opcion = Convert.ToInt64(rFila.Cells[0].Text);
                pAcceso.nombreopcion = rFila.Cells[1].Text;
                CheckBox chbconsulta = (CheckBox)rFila.Cells[2].FindControl("chbconsulta");
                if (chbconsulta != null)
                {
                    if (chbconsulta.Checked == true)
                        pAcceso.consultar = 1;
                }
                CheckBox chbcreacion = (CheckBox)rFila.Cells[2].FindControl("chbcreacion");
                if (chbcreacion != null)
                {
                    if (chbcreacion.Checked == true)
                        pAcceso.insertar = 1;
                }
                CheckBox chbmodificacion = (CheckBox)rFila.Cells[2].FindControl("chbmodificacion");
                if (chbmodificacion != null)
                {
                    if (chbmodificacion.Checked == true)
                        pAcceso.modificar = 1;
                }
                CheckBox chbborrado = (CheckBox)rFila.Cells[2].FindControl("chbborrado");
                if (chbborrado != null)
                {
                    if (chbborrado.Checked == true)
                        pAcceso.borrar = 1;
                }
                vPerfil.lstAccesos.Add(pAcceso);
            }

            if (idObjeto != "")
            {
                vPerfil.codperfil = Convert.ToInt64(idObjeto);
                perfilServicio.ModificarPerfil(vPerfil, (Usuario)Session["usuario"]);
            }
            else
            {
                vPerfil = perfilServicio.CrearPerfil(vPerfil, (Usuario)Session["usuario"]);
                idObjeto = vPerfil.codperfil.ToString();
            }

            Session[perfilServicio.CodigoPrograma + ".id"] = idObjeto;
            ctlMensaje.MostrarMensaje("Perfil: " + idObjeto.ToString());
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(perfilServicio.CodigoPrograma, "btnGuardar_Click", ex);
        }
    }

    protected void gvLista_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        GridView gvLista = (GridView)sender;
        foreach (GridViewRow itemss in gvLista.Rows)
        {
            btnInfo = (ImageButton)itemss.FindControl("btnInfo");
            Label cod_opcion = (Label)itemss.FindControl("cod_opcion");


            if (btnInfo != null)
            {
                foreach (Acceso item in lstAccesos.Where(x => x.cod_opcion == Convert.ToInt64(cod_opcion.Text)).ToList())
                {

                    if (item.PermisoCampo == 1)
                        btnInfo.Visible = true;
                }
            }
        }


    }

    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            Perfil vPerfil = new Perfil();
            vPerfil = perfilServicio.ConsultarPerfil(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);

            if (!string.IsNullOrEmpty(vPerfil.codperfil.ToString()))
                txtCodigo.Text = HttpUtility.HtmlDecode(vPerfil.codperfil.ToString().Trim());
            if (!string.IsNullOrEmpty(vPerfil.nombreperfil.ToString()))
                txtDescripcion.Text = HttpUtility.HtmlDecode(vPerfil.nombreperfil.ToString().Trim());

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(perfilServicio.CodigoPrograma, "ObtenerDatos", ex);
        }
    }


    protected void btnContinuar_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }

    protected void btnCancelar_Click(object sender, ImageClickEventArgs e)
    {
        Navegar(Pagina.Lista);
    }

    private void CargarModulo()
    {
        try
        {
            Xpinn.Seguridad.Services.ModuloService moduloServicio = new Xpinn.Seguridad.Services.ModuloService();
            List<Xpinn.Seguridad.Entities.Modulo> lstModulo = new List<Xpinn.Seguridad.Entities.Modulo>();
            lstModulo = moduloServicio.ListarModulo(null, (Usuario)Session["usuario"]);
            ddlModulo.DataSource = lstModulo;
            ddlModulo.DataTextField = "nom_modulo";
            ddlModulo.DataValueField = "cod_modulo";
            ddlModulo.DataBind();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(perfilServicio.CodigoPrograma, "CargarModulo", ex);
        }
    }

    protected void ddlModulo_SelectedIndexChanged(object sender, EventArgs e)
    {
        CargarOpciones(idObjeto, ddlModulo.SelectedValue);
    }
    protected void BtnCerrarCampos_Click(object sender, EventArgs e)
    {
        mpeDocAnexo.Hide();
    }

    public void MostrarOpciones()
    {
        //Limpio la tabla antes de cargarle datos
        List<CamposPermiso> lstCampos = new List<CamposPermiso>();
        gvCampos.DataSource = lstCampos;
        gvCampos.DataBind();
        Object obj = new object();
        page_fabricacreditos_modificacioncredito_nuevo_aspx pEntidad = new page_fabricacreditos_modificacioncredito_nuevo_aspx();
        obj = pEntidad;
        //Recorro los atributos que tiene los tipos y lo voy metiendo dentro de una entidad 
        FieldInfo[] propiedades = obj.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
        List<string> lstcampos = new List<string>();
        string nuevoCampo;
        var campo = "";
        foreach (FieldInfo propiedade in propiedades)
        {
            CamposPermiso entidad = new CamposPermiso();
            var status = propiedade.DeclaringType;
            campo = "CAMPO";
            if (status.IsVisible)
            {
                if (propiedade.Name.Substring(0, 3).ToLower() == "txt" ||
                    propiedade.Name.Substring(0, 3).ToLower() == "lbl"
                )
                {
                    entidad.Campo = propiedade.Name.Substring(3);
                    nuevoCampo = propiedade.Name.Substring(3);
                    if (!lstCampos.Exists(x => x.Campo.ToLower() == nuevoCampo.ToLower()))
                        lstCampos.Add(entidad);

                }

                if (!lstCampos.Exists(x => x.Campo == campo))
                {
                    entidad.Campo = campo;
                    lstCampos.Add(entidad);
                }
            }
        }

        foreach (FieldInfo propiedade in propiedades)
        {
            CamposPermiso entidad = new CamposPermiso();
            var status = propiedade.DeclaringType;
            campo = "TABLAS";
            if (status.IsVisible)
            {
                if (propiedade.Name.Substring(0, 2).ToLower() == "gv")
                {
                    entidad.Campo = propiedade.Name.Substring(2);

                    nuevoCampo = propiedade.Name.Substring(2);
                    if (!lstCampos.Exists(x => x.Campo.ToLower() == nuevoCampo.ToLower()))
                        lstCampos.Add(entidad);

                }
                if (!lstCampos.Exists(x => x.Campo == campo))
                {
                    entidad.Campo = campo;
                    lstCampos.Add(entidad);
                }
            }
        }
        foreach (FieldInfo propiedade in propiedades)
        {
            CamposPermiso entidad = new CamposPermiso();
            var status = propiedade.DeclaringType;
            campo = "CHECKS";
            if (status.IsVisible)
            {
                if (propiedade.Name.Substring(0, 2).ToLower() == "ch")
                {
                    entidad.Campo = propiedade.Name.Substring(2);
                    nuevoCampo = propiedade.Name.Substring(2);
                    if (!lstCampos.Exists(x => x.Campo.ToLower() == nuevoCampo.ToLower()))
                        lstCampos.Add(entidad);

                }
                if (!lstCampos.Exists(x => x.Campo == campo))
                {
                    entidad.Campo = campo;
                    lstCampos.Add(entidad);
                }
            }
        }
        foreach (FieldInfo propiedade in propiedades)
        {
            CamposPermiso entidad = new CamposPermiso();
            var status = propiedade.DeclaringType;
            campo = "BOTONES";
            if (status.IsVisible)
            {
                if (propiedade.Name.Substring(0, 3).ToLower() == "btn")
                {
                    entidad.Campo = propiedade.Name.Substring(3);
                    nuevoCampo = propiedade.Name.Substring(3);
                    nuevoCampo = propiedade.Name.Substring(3);
                    if (!lstCampos.Exists(x => x.Campo.ToLower() == nuevoCampo.ToLower()))
                        lstCampos.Add(entidad);
                }
                if (!lstCampos.Exists(x => x.Campo == campo))
                {
                    entidad.Campo = campo;
                    lstCampos.Add(entidad);
                }
            }
        }

        gvCampos.DataSource = lstCampos;
        gvCampos.Visible = true;
        gvCampos.DataBind();
    }

    protected void gvLista_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        mpeDocAnexo.Show();
        MostrarOpciones();
    }

    protected void btnGuardarCampos_Click(object sender, EventArgs e)
    {
        CamposPermiso en = new CamposPermiso();
        en.CodPerfl = Convert.ToInt32(idObjeto);
        bool proceso = true;

        perfilServicio.EliminarCamposPerfil(en, (Usuario)Session["Usuario"]);
        foreach (GridViewRow row in gvCampos.Rows)
        {
            CamposPermiso entidad = new CamposPermiso();
            CheckBox chVisaulizar = (CheckBox)row.FindControl("chVisaulizar");
            CheckBox chEditable = (CheckBox)row.FindControl("chEditable");
            Label lblCampo = (Label)row.FindControl("lblCampo");

            if (ValidarCampo(lblCampo)) continue;

            entidad.Campo = lblCampo.Text;
            entidad.CodPerfl = Convert.ToInt32(idObjeto);
            entidad.Visible = chVisaulizar.Checked ? 1 : 0;
            entidad.Editable = chEditable.Checked ? 1 : 0;

            proceso = perfilServicio.CrearCamposPerfil(entidad, (Usuario)Session["Usuario"]);
        }
        if (proceso)
            Navegar(Pagina.Nuevo);
        else
            VerError("Hubo un error al Guardar Los campos.");
    }

    protected void gvCampos_OnDataBound(object sender, EventArgs e)
    {
        GridView gvCampos = (GridView)sender;
        campos.CodPerfl = Convert.ToInt32(idObjeto);
        List<CamposPermiso> lstcampos = perfilServicio.ConsultarCamposPerfil(campos, Usuario);

        foreach (GridViewRow row in gvCampos.Rows)
        {
            Label lblCampo = (Label)row.FindControl("lblCampo");

            CheckBox chVisualizar = (CheckBox)row.FindControl("chVisaulizar");
            CheckBox chEditable = (CheckBox)row.FindControl("chEditable");
            if (lstcampos.Count == 0)
            {
                chVisualizar.Checked = true;
            }
            else
            {
                foreach (CamposPermiso items in lstcampos.Where(x => x.Campo == lblCampo.Text))
                {
                    if (items.Visible != 0)
                        chVisualizar.Checked = true;
                    if (items.Editable != 0)
                        chEditable.Checked = true;
                }
            }


            if (ValidarCampo(lblCampo))
            {
                for (int i = 0; i < row.Cells.Count; i++)
                {
                    if (i == 0)
                    {
                        row.Cells[i].ColumnSpan = 3;
                        row.Cells[i].Font.Bold = true;
                        row.Cells[i].HorizontalAlign = HorizontalAlign.Center;
                        row.Cells[i].BackColor = Color.DarkTurquoise;
                    }
                    else
                    {
                        row.Cells[i].Visible = false;
                    }

                }


            }
        }
    }

    bool ValidarCampo(Label lblCampo)
    {
        return lblCampo.Text == "CAMPO" || lblCampo.Text == "BOTONES" || lblCampo.Text == "CHECKS" ||
               lblCampo.Text == "TABLAS";
    }
}