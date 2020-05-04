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

partial class Nuevo : GlobalWeb
{
    private Xpinn.Tesoreria.Services.AreasCajService AreasCajServicio = new Xpinn.Tesoreria.Services.AreasCajService();
    public StringHelper _stringHelper = new StringHelper();
    PoblarListas poblar = new PoblarListas();
    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[AreasCajServicio.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(AreasCajServicio.CodigoPrograma, "E");
            else
                VisualizarOpciones(AreasCajServicio.CodigoPrograma, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AreasCajServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                
                txtfechaConst.ToDateTime = DateTime.Now;
                CargarCombo();
                if (Session[AreasCajServicio.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[AreasCajServicio.CodigoPrograma + ".id"].ToString();
                    Session.Remove(AreasCajServicio.CodigoPrograma + ".id");
                    ObtenerDatos(idObjeto);
                }
                else
                {
                    txtCodigo.Text = AreasCajServicio.ObtenerSiguienteCodigo((Usuario)Session["usuario"]).ToString();
                    ddlUsuario.SelectedValue = Usuario.codusuario.ToString();
                }
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AreasCajServicio.CodigoPrograma, "Page_Load", ex);
        }
    }

    protected void CargarCombo()
    {
        poblar.PoblarListaDesplegable("USUARIOS", "CODUSUARIO, NOMBRE", "ESTADO = 1", "NOMBRE", ddlUsuario, Usuario);
        Xpinn.Caja.Entities.CentroCosto CenCos = new Xpinn.Caja.Entities.CentroCosto();
        List<Xpinn.Caja.Entities.CentroCosto> LstCentroCosto = new List<Xpinn.Caja.Entities.CentroCosto>();
        Xpinn.Caja.Services.CentroCostoService CentroCostoServicio = new Xpinn.Caja.Services.CentroCostoService();
        LstCentroCosto = CentroCostoServicio.ListarCentroCosto(CenCos, (Usuario)Session["usuario"]);
        if (LstCentroCosto != null)
        {
            ddlCentroCosto.DataTextField = "nom_centro";
            ddlCentroCosto.DataValueField = "centro_costo";
            ddlCentroCosto.DataSource = LstCentroCosto;
            ddlCentroCosto.DataBind();            
        }
    }

    public Boolean ValidarDatos()
    {
        if(string.IsNullOrEmpty(txtBase.Text))
        {
            VerError("El valor base de la caja menor debe ser ingresado");
            return false;
        }
        if (Convert.ToDecimal(txtBase.Text.Replace(".","")) <= 0)
        {
            VerError("El valor base de la caja menor debe ser mayor a cero");
            return false;
        }
        if (ddlUsuario.SelectedItem == null)
        {
            VerError("No se encontraron registros de usuarios activos");
            return false;
        }
        if (ddlUsuario.SelectedIndex == 0)
        {
            VerError("Seleccione un responsable");
            return false;
        }
        if (txtDescripcion.Text == "")
        {
            VerError("Debe ingresar la descripción");
            return false;
        }
        if (DateTime.Now.ToString("dd/MM/yyyy") != txtfechaConst.Text && idObjeto == null)
        {
            VerError("La fecha de constitución debe ser la fecha actual");
            return false;
        }
        if (string.IsNullOrEmpty(txtVMinimo.Text))
        {
            VerError("Debe ingresar el monto mínimo");
            return false;
        }
        
        return true;
        
       
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        VerError("");
        try
        {
            Xpinn.Tesoreria.Entities.AreasCaj vAreasCaj = new Xpinn.Tesoreria.Entities.AreasCaj();
            Usuario pusuario = (Usuario)Session["usuario"];
            if (ValidarDatos())
            {
                if (idObjeto != "")
                    vAreasCaj = AreasCajServicio.ConsultarAreasCaj(Convert.ToInt64(idObjeto), (Usuario)Session["usuario"]);
                else
                    vAreasCaj.saldo_caja = Convert.ToInt64(_stringHelper.DesformatearNumerosEnteros(txtBase.Text.Trim()));
                vAreasCaj.base_valor = Convert.ToDecimal(_stringHelper.DesformatearNumerosEnteros(txtBase.Text.Trim()));
                vAreasCaj.idarea = Convert.ToInt32(txtCodigo.Text.Trim());
                vAreasCaj.fecha_constitucion = DateTime.Now;
                vAreasCaj.cod_usuario = Convert.ToInt64(ddlUsuario.SelectedValue);
                vAreasCaj.nombre = (txtDescripcion.Text.Trim());
                vAreasCaj.valor_minimo = Convert.ToInt64(_stringHelper.DesformatearNumerosEnteros(txtVMinimo.Text.Trim()));
                vAreasCaj.centro_costo = Convert.ToInt64(ddlCentroCosto.SelectedValue);
                vAreasCaj.identificacion = Convert.ToString(txtIdentificacion.Text);

                if (idObjeto != "")
                {
                    vAreasCaj.idarea = Convert.ToInt32(idObjeto);
                    AreasCajServicio.ModificarAreasCaj(vAreasCaj, Usuario);
                    idObjeto = vAreasCaj.idarea.ToString();
                    Session[AreasCajServicio.CodigoPrograma + ".id"] = idObjeto;
                    Navegar(Pagina.Lista);
                }
                else
                {
                    Xpinn.Tesoreria.Entities.AreasCaj pCajaMenor = AreasCajServicio.ConsultarCajaMenor(Convert.ToInt32(ddlUsuario.SelectedValue), Usuario);
                    if (pCajaMenor.idarea > 0)
                    {
                        VerError("El usuario actual ya tiene un area de caja menor registrada");
                    }
                    else
                    {
                        vAreasCaj = AreasCajServicio.CrearAreasCaj(vAreasCaj, (Usuario)Session["usuario"]);
                        idObjeto = vAreasCaj.idarea.ToString();
                        Session[AreasCajServicio.CodigoPrograma + ".id"] = idObjeto;
                        Navegar(Pagina.Lista);
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
            BOexcepcion.Throw(AreasCajServicio.CodigoPrograma, "btnGuardar_Click", ex);
        }
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }

    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            Xpinn.Tesoreria.Entities.AreasCaj areaCaja = new Xpinn.Tesoreria.Entities.AreasCaj();
            areaCaja = AreasCajServicio.ConsultarAreasCaj(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);
            Usuario pusuario = new Usuario();
            pusuario = (Usuario)Session["usuario"];
            if (!string.IsNullOrEmpty(areaCaja.idarea.ToString()))
                txtCodigo.Text = HttpUtility.HtmlDecode(areaCaja.idarea.ToString().Trim());
            if (!string.IsNullOrEmpty(areaCaja.fecha_constitucion.ToString()))
                txtfechaConst.Text = HttpUtility.HtmlDecode(areaCaja.fecha_constitucion.ToShortDateString().Trim());
            if (!string.IsNullOrEmpty(areaCaja.base_valor.ToString()))
                txtBase.Text = HttpUtility.HtmlDecode(areaCaja.base_valor.ToString().Trim());
            if (areaCaja.cod_usuario != null)
                ddlUsuario.SelectedValue = areaCaja.cod_usuario.ToString();
            if (!string.IsNullOrEmpty(areaCaja.base_valor.ToString()))
                txtDescripcion.Text = HttpUtility.HtmlDecode(areaCaja.nombre.ToString().Trim());
            if (!string.IsNullOrEmpty(areaCaja.valor_minimo.ToString()))
                txtVMinimo.Text = HttpUtility.HtmlDecode(areaCaja.valor_minimo.ToString().Trim());
            if (!string.IsNullOrEmpty(areaCaja.centro_costo.ToString()))
                ddlCentroCosto.SelectedValue = HttpUtility.HtmlDecode(areaCaja.centro_costo.ToString().Trim());


            if (!string.IsNullOrEmpty(areaCaja.identificacion.ToString()))
                txtIdentificacion.Text = HttpUtility.HtmlDecode(areaCaja.identificacion.ToString().Trim());
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AreasCajServicio.CodigoPrograma, "ObtenerDatos", ex);
        }
    }
    
}