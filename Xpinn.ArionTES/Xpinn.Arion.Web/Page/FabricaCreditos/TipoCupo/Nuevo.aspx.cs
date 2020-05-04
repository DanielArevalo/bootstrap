using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Xpinn.FabricaCreditos.Entities;
using Xpinn.FabricaCreditos.Services;
using Xpinn.Util;

partial class Nuevo : GlobalWeb
{
    TipoCupoService _tipocupoService = new TipoCupoService();


    #region Eventos Iniciales


    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            VisualizarOpciones(_tipocupoService.CodigoPrograma, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoCancelar += btnCancelar_Click;
            toolBar.eventoGuardar += btnGuardar_Click;
            ctlMensaje.eventoClick += btnContinuarMen_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_tipocupoService.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                txtCodigo.Text = "";
                Session["Detalle"] = null;
                if (Session[_tipocupoService.CodigoPrograma + ".id"] != null)
                {
                    Session["Operacion"] = "2";
                    idObjeto = Session[_tipocupoService.CodigoPrograma + ".id"].ToString();
                    Session.Remove(_tipocupoService.CodigoPrograma + ".id");

                    ObtenerDatos(idObjeto);
                }
                else
                {
                    Session["Operacion"] = "1";
                    InicializarListado();
                }
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_tipocupoService.CodigoPrograma, "Page_Load", ex);
        }
    }

    void ObtenerDatos(String idObjeto)
    {
        TipoCupo vDatos = new TipoCupo();
        vDatos.tipo_cupo = Convert.ToInt32(idObjeto);

        vDatos = _tipocupoService.ConsultarTipoCupo(vDatos, Usuario);

        if (vDatos.tipo_cupo != 0)
            txtCodigo.Text = vDatos.tipo_cupo.ToString();
        if (vDatos.descripcion != "")
            txtDescripcion.Text = vDatos.descripcion;
        if (vDatos.resta_creditos != null)
            chkRestar.Checked = Convert.ToBoolean(vDatos.resta_creditos);
        List<DetTipoCupo> lstDetalle = new List<DetTipoCupo>();
        lstDetalle = _tipocupoService.ListarDetTipoCupo(Convert.ToInt32(idObjeto), Usuario);

        if (lstDetalle.Count > 0)
        {
            gvLista.DataSource = lstDetalle;
            gvLista.DataBind();
            Session["Detalle"] = lstDetalle;
        }
        InicializarListado();
    }

    void InicializarListado()
    {
        List<DetTipoCupo> lstTasa = new List<DetTipoCupo>();
        if (Session["Detalle"] != null)
            lstTasa = (List<DetTipoCupo>)Session["Detalle"];
        for (int i = gvLista.Rows.Count; i < 5; i++)
        {
            DetTipoCupo pDetalle = new DetTipoCupo();
            pDetalle.iddetalle = -1;
            pDetalle.tipo_cupo = 0;
            pDetalle.idvariable = 0;
            pDetalle.valor = "";
            lstTasa.Add(pDetalle);
        }
        gvLista.DataSource = lstTasa;
        gvLista.DataBind();
        Session["Detalle"] = lstTasa;
    }


    #endregion


    #region Eventos Botonera


    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        string mensaje = string.Empty;
        if (ValidarDatos())
        {
            mensaje = Session["Operacion"] != null && Session["Operacion"].ToString().Trim() == "2" ? " actualización?" : " grabación?";

            ctlMensaje.MostrarMensaje("Desea realizar la" + mensaje);
        }
    }

    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        try
        {

            TipoCupo vtipoCupo = new TipoCupo();
            if (idObjeto != "")
                vtipoCupo.tipo_cupo = Convert.ToInt32(idObjeto);
            else
                vtipoCupo.tipo_cupo = 0;
            vtipoCupo.descripcion = txtDescripcion.Text;
            vtipoCupo.resta_creditos = Convert.ToInt32(chkRestar.Checked);

            vtipoCupo.LstVariables = ObtenerListaGridView(true);

            // Si llega nulo es porque hubo algun error
            if (vtipoCupo.LstVariables != null)
            {
                if (vtipoCupo.LstVariables.Count > 0)
                {
                    if (txtCodigo.Text == "")
                    {
                        _tipocupoService.CrearTipoCupo(vtipoCupo, (Usuario)Session["usuario"]);
                    }
                    else
                    {
                        _tipocupoService.ModificarTipoCupo(vtipoCupo, (Usuario)Session["usuario"]);
                    }

                    Navegar(Pagina.Lista);
                }
                else
                {
                    VerError("No puedes guardar un tipo de cupo sin criterios validos!.");
                }
            }
            else
            {
                VerError("Debe seleccionar las variables");
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_tipocupoService.CodigoPrograma, "btnGuardar_Click", ex);
        }
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }

    protected void btnDetalle_Click(object sender, EventArgs e)
    {
        ObtenerListaGridView();
        List<DetTipoCupo> lstDetalle = Session["Detalle"] as List<DetTipoCupo>;

        if (lstDetalle != null)
        {
            for (int i = 1; i <= 1; i++)
            {
                DetTipoCupo pDetalle = new DetTipoCupo();
                pDetalle.iddetalle = -1;
                pDetalle.idvariable = 0;
                pDetalle.valor = "";
                lstDetalle.Add(pDetalle);
            }
            gvLista.PageIndex = gvLista.PageCount;
            gvLista.DataSource = lstDetalle;
            gvLista.DataBind();

            Session["Detalle"] = lstDetalle;
        }
    }


    #endregion


    #region Eventos GridView


    protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int conseID = Convert.ToInt32(gvLista.DataKeys[e.RowIndex].Values[0].ToString());

        List<DetTipoCupo> LstDetalle = ObtenerListaGridView();

        if (conseID > 0)
        {
            try
            {
                foreach (DetTipoCupo acti in LstDetalle)
                {
                    if (acti.iddetalle == conseID)
                    {
                        _tipocupoService.EliminarTipoCupo(conseID, (Usuario)Session["usuario"]);
                        LstDetalle.Remove(acti);
                        break;
                    }
                }
            }
            catch (Xpinn.Util.ExceptionBusiness ex)
            {
                VerError(ex.Message);
            }
        }
        gvLista.DataSourceID = null;
        gvLista.DataBind();

        gvLista.DataSource = LstDetalle;
        gvLista.DataBind();

        Session["Detalle"] = LstDetalle;

    }

    protected void gvLista_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblCondicion = (Label)e.Row.FindControl("lblCondicion");
            DropDownList ddlCondicion = (DropDownList)e.Row.FindControl("ddlCondicion");
            if (ddlCondicion != null)
            {
                if (lblCondicion != null)
                {
                    ddlCondicion.SelectedValue = lblCondicion.Text;
                }
            }
        }
    }

    
    #endregion


    #region Metodos De Ayuda

    List<DetTipoCupo> ObtenerListaGridView(bool validarGridView = false)
    {
        List<DetTipoCupo> lista = new List<DetTipoCupo>();

        foreach (GridViewRow rfila in gvLista.Rows)
        {
            DetTipoCupo eTasa = new DetTipoCupo();

            Label lblcodigo = (Label)rfila.FindControl("lblcodigo");
            if (lblcodigo != null && !string.IsNullOrWhiteSpace(lblcodigo.Text))
                eTasa.iddetalle = Convert.ToInt32(lblcodigo.Text);

            DropDownListGrid ddlCondicion = (DropDownListGrid)rfila.FindControl("ddlCondicion");
            if (ddlCondicion.SelectedValue != null)
                eTasa.idvariable = ConvertirStringToInt32(ddlCondicion.SelectedValue);

            TextBox txtValor = (TextBox)rfila.FindControl("txtValor");
            if (txtValor != null && !string.IsNullOrWhiteSpace(txtValor.Text))
                eTasa.valor = Convert.ToString(txtValor.Text);

            if (eTasa.idvariable != 0)
                lista.Add(eTasa);            

        }

        Session["Detalle"] = lista;
        return lista;
    }

    Boolean ValidarDatos()
    {
        if (string.IsNullOrWhiteSpace(txtDescripcion.Text))
        {
            VerError("Ingrese la Descripción");
            return false;
        }

        return true;
    }

 
    #endregion


}
