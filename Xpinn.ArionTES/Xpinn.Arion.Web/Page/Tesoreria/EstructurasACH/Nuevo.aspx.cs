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
using Xpinn.Tesoreria.Entities;
using Xpinn.Tesoreria.Services;

partial class Nuevo : GlobalWeb
{
    private Xpinn.Tesoreria.Services.ACHplantillaService ACHplantillaServicio = new Xpinn.Tesoreria.Services.ACHplantillaService();
    private Xpinn.Tesoreria.Services.ACHregistroService registroServicio = new Xpinn.Tesoreria.Services.ACHregistroService();
    private Xpinn.Tesoreria.Services.ACHcampoService campoServicio = new Xpinn.Tesoreria.Services.ACHcampoService();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[ACHplantillaServicio.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(ACHplantillaServicio.CodigoPrograma, "E");
            else
                VisualizarOpciones(ACHplantillaServicio.CodigoPrograma, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ACHplantillaServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                Session["LstRegistros"] = null;
                Session["LstCampos"] = null;
                CargarDropDown();
                if (Session[ACHplantillaServicio.CodigoPrograma + ".id"] != null)
                {
                    txtCodigo.Enabled = false;
                    idObjeto = Session[ACHplantillaServicio.CodigoPrograma + ".id"].ToString();
                    ObtenerDatos(idObjeto);
                }
                else
                {
                    txtCodigo.Text = ACHplantillaServicio.ObtenerSiguienteCodigo((Usuario)Session["usuario"]).ToString();
                    txtFecha.ToDateTime = System.DateTime.Now;
                    CrearDetalleInicial();
                }
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ACHplantillaServicio.CodigoPrograma, "Page_Load", ex);
        }
    }

    void CargarDropDown()
    {
        Xpinn.Caja.Services.BancosService bancoService = new Xpinn.Caja.Services.BancosService();
        Xpinn.Caja.Entities.Bancos banco = new Xpinn.Caja.Entities.Bancos();
        ddlEntidad.DataSource = bancoService.ListarBancos(banco, (Usuario)Session["usuario"]);
        ddlEntidad.DataTextField = "nombrebanco";
        ddlEntidad.DataValueField = "cod_banco";
        ddlEntidad.AppendDataBoundItems = true;
        ddlEntidad.Items.Insert(0, new ListItem("Seleccione un item", "0"));
        ddlEntidad.DataBind();
    }

    Boolean ValidarGridView(int NumeroGrid)
    {
        VerError("");
        if (NumeroGrid == 1)
        {
            //GRID REGISTROS
            ObtenerACHregistro(true);
            List<Xpinn.Tesoreria.Entities.ACHregistro> lstRegistros = new List<Xpinn.Tesoreria.Entities.ACHregistro>();
            if (Session["LstRegistros"] != null)
                lstRegistros = (List<Xpinn.Tesoreria.Entities.ACHregistro>)Session["LstRegistros"];

            var lstDatosRegistro = from c in lstRegistros
                                   select new
                                   {
                                       c.codigo
                                   };
            lstDatosRegistro = lstDatosRegistro.OrderBy(c => c.codigo).ToList();

            int cont = 0, campo = 0, campoAnterior = 0, cont2 = 0;
            foreach (var rFilaReg in lstDatosRegistro)
            {
                if (rFilaReg.codigo > 0)
                {
                    campo = Convert.ToInt32(rFilaReg.codigo);
                    if (cont == 0)
                        campoAnterior = campo;
                    if (campo == campoAnterior)
                        cont2++;
                    else
                    {
                        if (cont2 >= 2)
                        {
                            VerError("No puede seleccionar mas de un registro con la misma descripcion.");
                            return false;
                        }
                        cont = 0; campoAnterior = 0; cont2 = 0;
                        if(cont == 0)
                            campoAnterior = campo;
                        if (campo == campoAnterior)
                            cont2++;
                    }
                }
                else
                {
                    VerError("No se grabaran los registros con valor -1. ");
                }
                cont++;
            }
            if (cont2 >= 2)
            {
                VerError("No puede seleccionar mas de un registro con la misma descripcion.");
                return false;
            }
        }
        else if (NumeroGrid == 2)
        {
            ObtenerACHcampo(true);
            List<Xpinn.Tesoreria.Entities.ACHcampo> lstCampos = new List<Xpinn.Tesoreria.Entities.ACHcampo>();
            if (Session["lstCampos"] != null)
                lstCampos = (List<Xpinn.Tesoreria.Entities.ACHcampo>)Session["lstCampos"];
            //GRID CAMPOS POR CADA REGISTRO
            var lstDatosCampos = from c in lstCampos
                                 select new
                                 {
                                     c.codigo,
                                     c.registro,
                                     c.orden
                                 };
            lstDatosCampos = lstDatosCampos.OrderBy(c => c.codigo).ToList();

            int cont = 0, campo = 0,campoAnterior = 0, cont2 = 0;            
            //RECORRER LA LISTA CAPTURADA
            foreach (var rFilaCampo in lstDatosCampos)
            {
                if (rFilaCampo.codigo > 0)
                {
                    campo = Convert.ToInt32(rFilaCampo.codigo);
                    if (cont == 0)
                        campoAnterior = campo;
                    if (campoAnterior == campo)
                    {
                        cont2++; //REALIZA CONTEO DEL NUMERO DE CAMPOS QUE EXISTEN EN LA GRID
                    }
                    else
                    {
                        if (cont2 >= 2)
                        {
                            VerError("No puede seleccionar mas de un campo con la misma descripcion.");
                            return false;
                        }
                        else
                        {
                            cont = 0; campoAnterior = 0; cont2 = 0;
                            if (cont == 0)
                                campoAnterior = campo;
                            if (campoAnterior == campo)
                                cont2++;
                        }
                    }
                    campoAnterior = campo;
                }
                else
                {
                    VerError("No se grabaran los campos con valor -1. ");
                }
                cont++;
            }
            if (cont2 >= 2)
            {
                VerError("No puede seleccionar mas de un campo con la misma descripcion.");
                return false;
            }

        }
        
        return true;
    }


    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            if (mvACH.ActiveViewIndex == 0)
            {
                Xpinn.Tesoreria.Entities.ACHplantilla vACHplantilla = new Xpinn.Tesoreria.Entities.ACHplantilla();

                if (idObjeto != "")
                    vACHplantilla = ACHplantillaServicio.ConsultarACHplantilla(Convert.ToInt64(idObjeto), (Usuario)Session["usuario"]);

                vACHplantilla.codigo = Convert.ToInt32(txtCodigo.Text.Trim());
                vACHplantilla.nombre = Convert.ToString(txtNombre.Text.Trim());
                vACHplantilla.descripcion = Convert.ToString(txtDescripcion.Text.Trim());
                vACHplantilla.fecha = txtFecha.ToDateTime;
                vACHplantilla.activo = Convert.ToInt32(cbEstado.Checked);
                if (ddlEntidad.SelectedIndex == 0)
                {
                    VerError("Seleccione el Banco por favor.");
                    return;
                }
                vACHplantilla.cod_banco = Convert.ToInt64(ddlEntidad.SelectedValue);

                ObtenerACHregistro(true);
                if (!ValidarGridView(1))
                    return;
                if (Session["LstRegistros"] != null)
                    vACHplantilla.LstRegistros = (List<Xpinn.Tesoreria.Entities.ACHregistro>)Session["LstRegistros"];

                if (idObjeto != "")
                {
                    vACHplantilla.codigo = Convert.ToInt32(idObjeto);
                    ACHplantillaServicio.ModificarACHplantilla(vACHplantilla, (Usuario)Session["usuario"]);
                }
                else
                {
                    vACHplantilla = ACHplantillaServicio.CrearACHplantilla(vACHplantilla, (Usuario)Session["usuario"]);
                    idObjeto = vACHplantilla.codigo.ToString();
                }

                Navegar(Pagina.Lista);

            }
            else if (mvACH.ActiveViewIndex == 1)
            {
                Xpinn.Tesoreria.Entities.ACHregistro vACHregistro = new Xpinn.Tesoreria.Entities.ACHregistro();

                if (idObjetoReg.Value != "")
                    vACHregistro = registroServicio.ConsultarACHregistro(Convert.ToInt64(idObjetoReg.Value), (Usuario)Session["usuario"]);

                vACHregistro.codigo = Convert.ToInt32(txtCodigoReg.Text.Trim());
                vACHregistro.nombre = Convert.ToString(txtNombreReg.Text.Trim());
                vACHregistro.separador = Convert.ToString(txtSeparadorReg.Text.Trim());
                if (ddlTipoReg.SelectedValue != null)
                    vACHregistro.tipo = Convert.ToInt32(ddlTipoReg.SelectedValue);
                else
                    vACHregistro.tipo = 0;

                ObtenerACHcampo(true);
                if (!ValidarGridView(2))
                    return;
                
                if (Session["lstCampos"] != null)
                    vACHregistro.LstCampos = (List<Xpinn.Tesoreria.Entities.ACHcampo>)Session["lstCampos"];

                if (idObjetoReg.Value != "")
                {
                    vACHregistro.codigo = Convert.ToInt32(idObjetoReg.Value);
                    registroServicio.ModificarACHregistro(vACHregistro, (Usuario)Session["usuario"]);
                }
                else
                {
                    vACHregistro = registroServicio.CrearACHregistro(vACHregistro, (Usuario)Session["usuario"]);
                    idObjetoReg.Value = vACHregistro.codigo.ToString();
                }

                ObtenerACHregistro(false);
                List<Xpinn.Tesoreria.Entities.ACHregistro> LstRegistros = new List<Xpinn.Tesoreria.Entities.ACHregistro>();
                if (Session["LstRegistros"] != null)
                    LstRegistros = (List<Xpinn.Tesoreria.Entities.ACHregistro>)Session["LstRegistros"];
                gvLista.DataSource = LstRegistros;
                gvLista.DataBind();
                mvACH.ActiveViewIndex = 0;

            }
            else if (mvACH.ActiveViewIndex == 2)
            {
                if (validarCrearCampo())
                {
                    Xpinn.Tesoreria.Entities.ACHcampo vACHcampo = new Xpinn.Tesoreria.Entities.ACHcampo();

                    if (idObjetoCam.Value != "")
                        vACHcampo = campoServicio.ConsultarACHcampo(Convert.ToInt64(idObjetoCam.Value), (Usuario)Session["usuario"]);

                    vACHcampo.codigo = Convert.ToInt32(txtCodigoCam.Text.Trim());
                    vACHcampo.nombre = Convert.ToString(txtNombreCam.Text.Trim());
                    vACHcampo.tipo = Convert.ToInt32(rbTipoCam.SelectedValue);
                    vACHcampo.valor = Convert.ToString(txtValorCam.Text.Trim());
                    if (ddlTipoDatoCam.SelectedValue != null)
                        vACHcampo.tipo_dato = Convert.ToInt32(ddlTipoDatoCam.SelectedValue);
                    else
                        vACHcampo.tipo_dato = 0;
                    if (ddlAlineacionCam.SelectedValue != null)
                        vACHcampo.justificacion = Convert.ToInt32(ddlAlineacionCam.SelectedValue);
                    else
                        vACHcampo.justificacion = 0;
                    vACHcampo.longitud = Convert.ToInt32(txtLongitudCam.Text);
                    vACHcampo.llenado = Convert.ToString(txtCaracterCam.Text);
                    if (txtFormatoCam.Text != "")
                        vACHcampo.formato = Convert.ToString(txtFormatoCam.Text);
                    if (ddlPuntoCam.SelectedValue != null)
                        vACHcampo.punto = ddlPuntoCam.SelectedValue;
                    else
                        vACHcampo.punto = "";
                    vACHcampo.suma = 0;
                    if (txtDecimalesCam.Text != "")
                        vACHcampo.num_dec = Convert.ToInt32(txtDecimalesCam.Text);

                    if (idObjetoCam.Value != "")
                    {
                        //vACHcampo.codigo = Convert.ToInt32(idObjeto);
                        campoServicio.ModificarACHcampo(vACHcampo, (Usuario)Session["usuario"]);
                    }
                    else
                    {
                        vACHcampo = campoServicio.CrearACHcampo(vACHcampo, (Usuario)Session["usuario"]);
                        idObjetoCam.Value = vACHcampo.codigo.ToString();
                    }

                    ObtenerACHcampo(false);
                    List<Xpinn.Tesoreria.Entities.ACHcampo> lstCampos = new List<Xpinn.Tesoreria.Entities.ACHcampo>();
                    if (Session["lstCampos"] != null)
                        lstCampos = (List<Xpinn.Tesoreria.Entities.ACHcampo>)Session["lstCampos"];
                    gvListaReg.DataSource = lstCampos;
                    gvListaReg.DataBind();
                    mvACH.ActiveViewIndex = 1;
                }
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ACHplantillaServicio.CodigoPrograma, "btnGuardar_Click", ex);
        }
    }


    Boolean validarCrearCampo()
    {
        if (txtNombreCam.Text == "")
        {
            VerError("Ingrese el nombre del campo a registrar.");
            txtNombreCam.Focus();
            return false;
        }
        if (rbTipoCam.SelectedItem == null)
        {
            VerError("Seleccione el tipo de variable a ingresar (Constante - Sentencia SQL).");
            return false;
        }
        if (txtValorCam.Text == "")
        {
            VerError("Debe ingresar el valor del campo por crear.");
            txtValorCam.Focus();
            return false;
        }
        if (ddlTipoDatoCam.SelectedIndex == 0)
        {
            VerError("Seleccione el tipo de dato al que pertenecera el campo.");
            ddlTipoDatoCam.Focus();
            return false;
        }        
        if (txtLongitudCam.Text == "")
        {
            VerError("Ingrese la longitud del campo.");
            txtLongitudCam.Focus();
            return false;
        }
        if (ddlTipoDatoCam.SelectedValue == "3") // DE TIPO FECHA
        {
            if (txtFormatoCam.Text == "")
            {
                VerError("Debe Ingresar el formato si es de tipo Fecha. (yyyyMMdd)");
                txtFormatoCam.Focus();
                return false;
            }
        }
        return true;
    }


    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        VerError("");
        if (mvACH.ActiveViewIndex == 0)
            Navegar(Pagina.Lista);
        if (mvACH.ActiveViewIndex > 0)
            mvACH.ActiveViewIndex -= 1;        
    }    

    #region Plantilla

    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            Xpinn.Tesoreria.Entities.ACHplantilla vPlantilla = new Xpinn.Tesoreria.Entities.ACHplantilla();
            vPlantilla = ACHplantillaServicio.ConsultarACHplantilla(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);

            if (!string.IsNullOrEmpty(vPlantilla.codigo.ToString()))
                txtCodigo.Text = HttpUtility.HtmlDecode(vPlantilla.codigo.ToString().Trim());
            if (!string.IsNullOrEmpty(vPlantilla.fecha.ToString()))
                txtFecha.ToDateTime = Convert.ToDateTime(HttpUtility.HtmlDecode(vPlantilla.fecha.ToString().Trim()));
            if (!string.IsNullOrEmpty(vPlantilla.nombre))
                txtNombre.Text = HttpUtility.HtmlDecode(vPlantilla.nombre.ToString().Trim());
            if (!string.IsNullOrEmpty(vPlantilla.descripcion))
                txtDescripcion.Text = HttpUtility.HtmlDecode(vPlantilla.descripcion.ToString().Trim());
            cbEstado.Checked = false;
            if (!string.IsNullOrEmpty(vPlantilla.activo.ToString()))
                if (HttpUtility.HtmlDecode(vPlantilla.activo.ToString().Trim()) == "1")
                    cbEstado.Checked = true;
            if (vPlantilla.cod_banco != null)
                ddlEntidad.SelectedValue = vPlantilla.cod_banco.ToString();

            Session.Remove("LstRegistros");
            if (vPlantilla.LstRegistros != null)
            {
                if (vPlantilla.LstRegistros.Count > 0)
                {
                    gvLista.DataSource = vPlantilla.LstRegistros;
                    gvLista.DataBind();
                    Session["LstRegistros"] = vPlantilla.LstRegistros;
                }
            }
            CrearDetalleInicial();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ACHplantillaServicio.CodigoPrograma, "ObtenerDatos", ex);
        }
    }    

    protected List<Xpinn.Tesoreria.Entities.ACHregistro> ListaRegistros()
    {
        Xpinn.Tesoreria.Entities.ACHregistro eRegistro = new Xpinn.Tesoreria.Entities.ACHregistro();
        Xpinn.Tesoreria.Services.ACHregistroService RegistroServicio = new Xpinn.Tesoreria.Services.ACHregistroService();
        List<Xpinn.Tesoreria.Entities.ACHregistro> LstRegistros = new List<Xpinn.Tesoreria.Entities.ACHregistro>();
        LstRegistros = RegistroServicio.ListarACHregistro(eRegistro, (Usuario)Session["usuario"]);
        return LstRegistros;
    }

    protected void btnDetalle_Click(object sender, EventArgs e)
    {
        ObtenerACHregistro(false);
        List<Xpinn.Tesoreria.Entities.ACHregistro> LstRegistros = new List<Xpinn.Tesoreria.Entities.ACHregistro>();
        LstRegistros = (List<Xpinn.Tesoreria.Entities.ACHregistro>)Session["LstRegistros"];
        for (int i = 1; i <= 1; i++)
        {
            Xpinn.Tesoreria.Entities.ACHregistro pACHregistro = new Xpinn.Tesoreria.Entities.ACHregistro();
            pACHregistro.codigo = -1;
            LstRegistros.Add(pACHregistro);
        }
        gvLista.PageIndex = gvLista.PageCount;
        gvLista.DataSource = LstRegistros;
        gvLista.DataBind();

        Session["LstRegistros"] = LstRegistros;
    }

    protected void btnCrearRegistro_Click(object sender, EventArgs e)
    {
        txtCodigoReg.Enabled = true;
        idObjetoReg.Value = "";
        Usuario pUsuario = new Usuario();
        pUsuario = (Usuario)Session["Usuario"];
        txtCodigoReg.Text = Convert.ToString(registroServicio.ObtenerSiguienteCodigo(pUsuario));
        txtNombreReg.Text = "";
        ddlTipoReg.SelectedValue = "";
        txtSeparadorReg.Text = "";
        mvACH.ActiveViewIndex = 1;
        Session["LstCampos"] = null;
        CrearDetalleInicialReg();
    }

    private void CrearDetalleInicial()
    {
        List<Xpinn.Tesoreria.Entities.ACHregistro> LstRegistros = new List<Xpinn.Tesoreria.Entities.ACHregistro>();
        if (Session["LstRegistros"] != null)
            LstRegistros = (List<Xpinn.Tesoreria.Entities.ACHregistro>)Session["LstRegistros"];
        for (int i = LstRegistros.Count(); i <= 2; i++)
        {
            Xpinn.Tesoreria.Entities.ACHregistro pRegistro = new Xpinn.Tesoreria.Entities.ACHregistro();
            pRegistro.codigo = -1;
            LstRegistros.Add(pRegistro);
        }
        gvLista.DataSource = LstRegistros;
        gvLista.DataBind();

        Session["LstRegistros"] = LstRegistros;

    }

    public Boolean ObtenerACHregistro(bool bValidar)
    {
        VerError("");
        try
        {
            List<Xpinn.Tesoreria.Entities.ACHregistro> lstRegistro = new List<Xpinn.Tesoreria.Entities.ACHregistro>();
            
            foreach (GridViewRow rfila in gvLista.Rows)
            {
                Xpinn.Tesoreria.Entities.ACHregistro detalle = new Xpinn.Tesoreria.Entities.ACHregistro();
               
                    DropDownListGrid ddlRegistro = (DropDownListGrid)rfila.Cells[2].FindControl("ddlRegistro");
                    if (ddlRegistro != null)
                        if (ddlRegistro.SelectedItem != null && ddlRegistro.SelectedItem.ToString().Trim() != "")
                            detalle.codigo = Convert.ToInt64(ddlRegistro.SelectedItem.Value);
                        else
                            detalle.codigo = 0;
                    else
                        detalle.codigo = 0;                    
                    
                    if (detalle.codigo != 0)
                    {
                        lstRegistro.Add(detalle);
                    }
            }
            Session["LstRegistros"] = lstRegistro;
            return true;
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
            return false;
        }
    }    

    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        VerError("");
        DropDownListGrid ddlRegistro = (DropDownListGrid)gvLista.Rows[e.NewEditIndex].FindControl("ddlRegistro");
        if (ddlRegistro != null)
        {
            txtCodigoReg.Enabled = false;
            String id = ddlRegistro.Text;
            if (Convert.ToInt32(id) > 0)
            {
                Session["lstCampos"] = null;
                idObjetoReg.Value = id;
                ObtenerDatosReg(id);
                mvACH.ActiveViewIndex = 1;
            }            
        }
    }

    protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        if (!ObtenerACHregistro(true))
            return;
        VerError("");
        int conseID = Convert.ToInt32(gvLista.DataKeys[e.RowIndex].Values[0].ToString());

        if (conseID != 0)
        {
            try
            {
                List<Xpinn.Tesoreria.Entities.ACHregistro> LstRegistros = new List<Xpinn.Tesoreria.Entities.ACHregistro>();
                LstRegistros = (List<Xpinn.Tesoreria.Entities.ACHregistro>)Session["LstRegistros"];
                if (conseID > 0)
                { 
                    DropDownListGrid ddlRegistro = (DropDownListGrid)gvLista.Rows[e.RowIndex].FindControl("ddlRegistro");
                    if(ddlRegistro != null)
                    {
                        if (ddlRegistro.SelectedItem.Text != "-1")
                        {
                            Xpinn.Tesoreria.Entities.ACHregistro entidad = new Xpinn.Tesoreria.Entities.ACHregistro();
                            entidad = ACHplantillaServicio.ConsultarRegisPlantilla(Convert.ToInt64(txtCodigo.Text), Convert.ToInt64(ddlRegistro.SelectedValue), (Usuario)Session["usuario"]);
                            if(entidad.codigo > 0)
                                ACHplantillaServicio.EliminarACH_PLANTILLA(Convert.ToInt32(txtCodigo.Text), conseID, (Usuario)Session["usuario"]);
                        }
                    }                  
                }
                LstRegistros.RemoveAt(e.RowIndex);
                Session["LstRegistros"] = LstRegistros;

                gvLista.DataSourceID = null;
                gvLista.DataBind();
                gvLista.DataSource = LstRegistros;
                gvLista.DataBind();

            }
            catch (Xpinn.Util.ExceptionBusiness ex)
            {
                VerError(ex.Message);
            }
            catch (Exception ex)
            {
                BOexcepcion.Throw(this.ACHplantillaServicio.CodigoPrograma + "L", "gvLista_RowDeleting", ex);
            }
        }
        else
        {
            e.Cancel = true;
        }
    }

    #endregion

    #region Registros

    protected void ObtenerDatosReg(String pIdObjeto)
    {
        try
        {
            Xpinn.Tesoreria.Entities.ACHregistro vRegistro = new Xpinn.Tesoreria.Entities.ACHregistro();
            Xpinn.Tesoreria.Services.ACHregistroService ACHregistroServicio = new Xpinn.Tesoreria.Services.ACHregistroService();
            vRegistro = ACHregistroServicio.ConsultarACHregistro(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);

            if (!string.IsNullOrEmpty(vRegistro.codigo.ToString()))
                txtCodigoReg.Text = HttpUtility.HtmlDecode(vRegistro.codigo.ToString().Trim());
            if (!string.IsNullOrEmpty(vRegistro.nombre))
                txtNombreReg.Text = HttpUtility.HtmlDecode(vRegistro.nombre.ToString().Trim());
            if (!string.IsNullOrEmpty(vRegistro.tipo.ToString()))
                ddlTipoReg.SelectedValue = HttpUtility.HtmlDecode(vRegistro.tipo.ToString().Trim());
            if (!string.IsNullOrEmpty(vRegistro.separador))
                txtSeparadorReg.Text = HttpUtility.HtmlDecode(vRegistro.separador.ToString().Trim());
            Session.Remove("LstCampos");
            if (vRegistro.LstCampos != null)
            {
                if (vRegistro.LstCampos.Count > 0)
                {
                    gvListaReg.DataSource = vRegistro.LstCampos;
                    gvListaReg.DataBind();
                    Session["LstCampos"] = vRegistro.LstCampos;
                }
            }            
            //CrearDetalleInicialReg();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ACHplantillaServicio.CodigoPrograma, "ObtenerDatos", ex);
        }
    }
    
    protected List<Xpinn.Tesoreria.Entities.ACHcampo> ListaCampos()
    {
        Xpinn.Tesoreria.Entities.ACHcampo eCampo = new Xpinn.Tesoreria.Entities.ACHcampo();
        Xpinn.Tesoreria.Services.ACHcampoService campoServicio = new Xpinn.Tesoreria.Services.ACHcampoService();
        List<Xpinn.Tesoreria.Entities.ACHcampo> LstCampos = new List<Xpinn.Tesoreria.Entities.ACHcampo>();
        LstCampos = campoServicio.ListarACHcampo(eCampo, (Usuario)Session["usuario"]);
        return LstCampos;
    }

    protected void btnDetalleReg_Click(object sender, EventArgs e)
    {
        ObtenerACHcampo(false);
        List<Xpinn.Tesoreria.Entities.ACHcampo> LstCampos = new List<Xpinn.Tesoreria.Entities.ACHcampo>();
        LstCampos = (List<Xpinn.Tesoreria.Entities.ACHcampo>)Session["LstCampos"];
        for (int i = 1; i <= 1; i++)
        {
            Xpinn.Tesoreria.Entities.ACHcampo pACHcampo = new Xpinn.Tesoreria.Entities.ACHcampo();
            pACHcampo.codigo = -1;
            LstCampos.Add(pACHcampo);
        }
        gvListaReg.PageIndex = gvLista.PageCount;
        gvListaReg.DataSource = LstCampos;
        gvListaReg.DataBind();

        Session["LstCampos"] = LstCampos;
    }

    protected void btnCrearCampo_Click(object sender, EventArgs e)
    {
        txtCodigoCam.Enabled = true;
        idObjetoCam.Value = "";
        Usuario pUsuario = new Usuario();
        pUsuario = (Usuario)Session["Usuario"];
        txtCodigoCam.Text = Convert.ToString(campoServicio.ObtenerSiguienteCodigo(pUsuario));
        Limpiar();
        mvACH.ActiveViewIndex = 2;
        //CrearDetalleInicialReg();
        ddlTipoDatoCam_SelectedIndexChanged(ddlTipoDatoCam, null);
    }

    private void Limpiar()
    {
        txtNombreCam.Text = "";
        rbTipoCam.SelectedValue = "1";
        lblTitValorCam.Text = "Valor";
        txtValorCam.Text = "";
        ddlTipoDatoCam.SelectedValue = "";
        ddlAlineacionCam.SelectedValue = "";
        txtLongitudCam.Text = "";
        txtCaracterCam.Text = "";
        txtFormatoCam.Text = "";
        ddlPuntoCam.SelectedValue = "";
        txtDecimalesCam.Text = "";
    }

    private void CrearDetalleInicialReg()
    {
        List<Xpinn.Tesoreria.Entities.ACHcampo> LstCampos = new List<Xpinn.Tesoreria.Entities.ACHcampo>();
        if (Session["LstCampos"] != null)
            LstCampos = (List<Xpinn.Tesoreria.Entities.ACHcampo>)Session["LstCampos"];
        for (int i = LstCampos.Count(); i <= 2; i++)
        {
            Xpinn.Tesoreria.Entities.ACHcampo pcampo = new Xpinn.Tesoreria.Entities.ACHcampo();
            pcampo.codigo = -1;
            LstCampos.Add(pcampo);
        }
        gvListaReg.DataSource = LstCampos;
        gvListaReg.DataBind();

        Session["LstCampos"] = LstCampos;

    }

    public Boolean ObtenerACHcampo(bool bValidar)
    {
        VerError("");
        try
        {
            List<Xpinn.Tesoreria.Entities.ACHcampo> lstCampos = new List<Xpinn.Tesoreria.Entities.ACHcampo>();

            foreach (GridViewRow rfila in gvListaReg.Rows)
            {
                Xpinn.Tesoreria.Entities.ACHcampo detalle = new Xpinn.Tesoreria.Entities.ACHcampo();

                DropDownListGrid ddlCampo = (DropDownListGrid)rfila.Cells[2].FindControl("ddlCampo");
                if (ddlCampo != null)
                    if (ddlCampo.SelectedItem != null && ddlCampo.SelectedItem.ToString().Trim() != "")
                        detalle.codigo = Convert.ToInt64(ddlCampo.SelectedItem.Value);
                    else
                        detalle.codigo = 0;
                else
                    detalle.codigo = 0;

                TextBox txtOrden = (TextBox)rfila.FindControl("txtOrden");
                if (txtOrden != null && txtOrden.Text != "")
                    detalle.orden = Convert.ToInt32(txtOrden.Text);
                else
                    detalle.orden = 0;

                if (detalle.codigo != 0)
                {
                    lstCampos.Add(detalle);
                }
            }
            
            Session["lstCampos"] = lstCampos;
            return true;
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
            return false;
        }
    }

    protected void gvListaReg_RowEditing(object sender, GridViewEditEventArgs e)
    {
        DropDownListGrid ddlCampo = (DropDownListGrid)gvListaReg.Rows[e.NewEditIndex].FindControl("ddlCampo");
        if (ddlCampo != null)
        {
            txtCodigoCam.Enabled = false;
            String id = ddlCampo.Text;
            if (Convert.ToInt32(id) > 0)
            {
                idObjetoCam.Value = id;
                ObtenerDatosCam(id);
                mvACH.ActiveViewIndex = 2;
            }
        }
    }

    protected void gvListaReg_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        if (!ObtenerACHcampo(true))
            return;
        VerError("");
        int conseID = Convert.ToInt32(gvListaReg.DataKeys[e.RowIndex].Values[0].ToString());

        if (conseID != 0)
        {
            try
            {
                 List<Xpinn.Tesoreria.Entities.ACHcampo>  lstCampos = new List<Xpinn.Tesoreria.Entities.ACHcampo>();
                 lstCampos = (List<Xpinn.Tesoreria.Entities.ACHcampo>)Session["lstCampos"];                
                if (conseID > 0)
                    registroServicio.EliminarCampoXACHregistro(Convert.ToInt64(txtCodigoReg.Text), Convert.ToInt64(conseID), (Usuario)Session["usuario"]);
                
                lstCampos.RemoveAt(e.RowIndex);
                Session["lstCampos"] = lstCampos;

                gvListaReg.DataSourceID = null;
                gvListaReg.DataBind();
                gvListaReg.DataSource = lstCampos;
                gvListaReg.DataBind();

            }
            catch (Xpinn.Util.ExceptionBusiness ex)
            {
                VerError(ex.Message);
            }
            catch (Exception ex)
            {
                BOexcepcion.Throw(this.ACHplantillaServicio.CodigoPrograma + "L", "gvListaReg_RowDeleting", ex);
            }
        }
        else
        {
            e.Cancel = true;
        }
    }

    protected void rbTipoCam_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbTipoCam.SelectedValue == "1")
        {
            lblTitValorCam.Text = "Valor";
            txtValorCam.TextMode = TextBoxMode.SingleLine;
            txtValorCam.Height = 14;
        }
        else
        {
            lblTitValorCam.Text = "Sentencia SQL";
            txtValorCam.TextMode = TextBoxMode.MultiLine;
            txtValorCam.Height = 50;
        }
    }

    protected void ObtenerDatosCam(String pIdObjeto)
    {
        try
        {
            Limpiar();
            ACHcampoService ACHcampoServicio = new ACHcampoService();
            ACHcampo vCampo = ACHcampoServicio.ConsultarACHcampo(Convert.ToInt64(pIdObjeto), Usuario);

            if (!string.IsNullOrEmpty(vCampo.codigo.ToString()))
                txtCodigoCam.Text = HttpUtility.HtmlDecode(vCampo.codigo.ToString().Trim());
            if (!string.IsNullOrEmpty(vCampo.nombre.ToString()))
                txtNombreCam.Text = HttpUtility.HtmlDecode(vCampo.nombre.ToString().Trim());
            rbTipoCam.SelectedValue = "1";
            if (!string.IsNullOrEmpty(vCampo.tipo.ToString()))
            {
                if (HttpUtility.HtmlDecode(vCampo.tipo.ToString().Trim()) == "2")
                {
                    rbTipoCam.SelectedValue = "2";
                }
            }
            if (vCampo.valor != null)
                if (!string.IsNullOrEmpty(vCampo.valor.ToString()))
                    txtValorCam.Text = HttpUtility.HtmlDecode(vCampo.valor.ToString().Trim());
            if (vCampo.tipo_dato != null)
                if (!string.IsNullOrEmpty(vCampo.tipo_dato.ToString()))
                {
                    ddlTipoDatoCam.SelectedValue = HttpUtility.HtmlDecode(vCampo.tipo_dato.ToString().Trim());
                    ddlTipoDatoCam_SelectedIndexChanged(ddlTipoDatoCam, null);
                }
            if (vCampo.justificacion != null)
                if (!string.IsNullOrEmpty(vCampo.justificacion.ToString()))
                    ddlAlineacionCam.SelectedValue = HttpUtility.HtmlDecode(vCampo.justificacion.ToString().Trim());
            if (vCampo.longitud != null)
                if (!string.IsNullOrEmpty(vCampo.longitud.ToString()))
                    txtLongitudCam.Text = HttpUtility.HtmlDecode(vCampo.longitud.ToString().Trim());
            if (vCampo.llenado != null)
                if (!string.IsNullOrEmpty(vCampo.llenado.ToString()))
                    txtCaracterCam.Text = HttpUtility.HtmlDecode(vCampo.llenado.ToString().Trim());
            if (vCampo.formato != null)
                if (!string.IsNullOrEmpty(vCampo.formato.ToString()))
                    txtFormatoCam.Text = HttpUtility.HtmlDecode(vCampo.formato.ToString().Trim());
            if (vCampo.punto != null)
                if (!string.IsNullOrEmpty(vCampo.punto.ToString()))
                    ddlPuntoCam.SelectedValue = HttpUtility.HtmlDecode(vCampo.punto.ToString().Trim());
            if (vCampo.num_dec != null)
                if (!string.IsNullOrEmpty(vCampo.num_dec.ToString()))
                    txtDecimalesCam.Text = HttpUtility.HtmlDecode(vCampo.num_dec.ToString().Trim());
            rbTipoCam_SelectedIndexChanged(null, null);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ACHplantillaServicio.CodigoPrograma, "ObtenerDatos", ex);
        }
    }

    #endregion

    protected void ddlTipoDatoCam_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblFormatoCam.Visible = false;
        txtFormatoCam.Visible = false;
        lblPuntoCam.Visible = false;
        ddlPuntoCam.Visible = false;
        lblDecimalesCam.Visible = false;
        txtDecimalesCam.Visible = false;
        if (ddlTipoDatoCam.SelectedIndex != 0)
        {
            if (ddlTipoDatoCam.SelectedValue == "1") //NUMERICO
            {
                lblPuntoCam.Visible = true;
                ddlPuntoCam.Visible = true;
                lblDecimalesCam.Visible = true;
                txtDecimalesCam.Visible = true;
            }
            else if(ddlTipoDatoCam.SelectedValue == "3" || ddlTipoDatoCam.SelectedValue == "4") //FECHA U HORA
            {
                lblFormatoCam.Visible = true;
                txtFormatoCam.Visible = true;
            }
        }
    }
}