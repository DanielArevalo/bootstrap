using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using System.IO;
using System.Configuration;
using System.Globalization;
using System.Text;
using System.Web.UI.HtmlControls;
using Xpinn.Auxilios.Services;
using Xpinn.Auxilios.Entities;


public partial class Nuevo : GlobalWeb
{

    LineaAuxilioServices LineaAux = new LineaAuxilioServices();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(LineaAux.CodigoPrograma, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
            ctlMensaje.eventoClick += btnContinuarMen_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(LineaAux.GetType().Name + "L", "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                Session["DatosRequisitos"] = null;
                CargarDropdown();
                mvAplicar.ActiveViewIndex = 0;

                if (Session[LineaAux.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[LineaAux.CodigoPrograma + ".id"].ToString();
                    Session.Remove(LineaAux.CodigoPrograma + ".id");
                    ObtenerDatos(idObjeto);
                    txtCodigo.Enabled = false;
                    Session["TEXTO"] = "modificar";
                    lblmsj.Text = "modificado";
                }
                else
                {
                    Session["TEXTO"] = "grabar";
                    lblmsj.Text = "grabada";
                    InicializargvRequisitos();
                }
                chkEducativo_CheckedChanged(chkEducativo, null);
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(LineaAux.GetType().Name + "L", "Page_Load", ex);
        }

    }

    void CargarDropdown()
    {
        PoblarLista("PERIODICIDAD", ddlPeriodicidad);


        ddlTipoPersona.Items.Insert(0, new ListItem("Seleccione un item", "0"));
        ddlTipoPersona.Items.Insert(1, new ListItem("Asociado", "1"));
        ddlTipoPersona.Items.Insert(2, new ListItem("Familiar", "2"));
        ddlTipoPersona.SelectedIndex = 0;
        ddlTipoPersona.DataBind();
    }



    protected void InicializargvRequisitos()
    {
        List<RequisitosLineaAuxilio> lstDeta = new List<RequisitosLineaAuxilio>();
        for (int i = gvRequisitos.Rows.Count; i < 5; i++)
        {
            RequisitosLineaAuxilio ePlan = new RequisitosLineaAuxilio();
            ePlan.codrequisitoaux = -1;
            ePlan.descripcion = "";
            ePlan.requerido = null;

            lstDeta.Add(ePlan);
        }
        gvRequisitos.DataSource = lstDeta;
        gvRequisitos.DataBind();

        Session["DatosRequisitos"] = lstDeta;
    }


    public void PoblarLista(string pTabla, DropDownList ddlControl)
    {
        PoblarLista(pTabla, "", "", "", ddlControl);
    }

    public void PoblarLista(string pTabla, string pColumnas, string pCondicion, string pOrden, DropDownList ddlControl)
    {
        List<Xpinn.Comun.Entities.ListaDesplegable> plista = new List<Xpinn.Comun.Entities.ListaDesplegable>();
        Xpinn.Comun.Entities.ListaDesplegable pentidad = new Xpinn.Comun.Entities.ListaDesplegable();
        Xpinn.Comun.Services.ListaDeplegableService pservicio = new Xpinn.Comun.Services.ListaDeplegableService();

        ddlControl.Items.Clear();
        plista = pservicio.ListarListaDesplegable(pentidad, pTabla, pColumnas, pCondicion, pOrden, (Usuario)Session["usuario"]);
        pentidad.idconsecutivo = null;
        pentidad.descripcion = "Seleccione un item";
        plista.Insert(0, pentidad);
        ddlControl.DataTextField = "descripcion";
        ddlControl.DataValueField = "idconsecutivo";
        ddlControl.DataSource = plista;
        ddlControl.DataBind();
    }


    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            LineaAuxilio vDetalle = new LineaAuxilio();

            vDetalle = LineaAux.ConsultarLineaAUXILIO(pIdObjeto.ToString(), (Usuario)Session["usuario"]);

            if (vDetalle.cod_linea_auxilio != "")
                txtCodigo.Text = vDetalle.cod_linea_auxilio.ToString().Trim();

            if (vDetalle.descripcion != "")
                txtNombre.Text = vDetalle.descripcion.ToString().Trim();

            if (vDetalle.estado == 1)
                chkEstado.Checked = true;
            else
                chkEstado.Checked = false;

            if (vDetalle.monto_minimo != 0)
                txtMntoMinimo.Text = vDetalle.monto_minimo.ToString();

            if (vDetalle.monto_maximo != 0)
                txtMtoMaximo.Text = vDetalle.monto_maximo.ToString();

            if (vDetalle.cod_periodicidad != 0)
                ddlPeriodicidad.SelectedValue = vDetalle.cod_periodicidad.ToString();

            if (vDetalle.tipo_persona != "")
                ddlTipoPersona.SelectedValue = vDetalle.tipo_persona;

            if (vDetalle.numero_auxilios != 0)
                txtNumAuxilios.Text = vDetalle.numero_auxilios.ToString();

            if (vDetalle.dias_desembolso != 0)
                txtDiasDesembolso.Text = vDetalle.dias_desembolso.ToString();

            if (vDetalle.cobra_retencion == 1)
                chkRetencion.Checked = true;
            else
                chkRetencion.Checked = false;

            if (vDetalle.permite_retirados == 1)
                chkRetirados.Checked = true;
            else
                chkRetirados.Checked = false;

            chkEducativo.Checked = vDetalle.educativo == 1 ? true : false;
            if (vDetalle.educativo == 1)
            {
                if (vDetalle.porc_matricula != 0)
                    txtPorcentajeMatri.Text = vDetalle.porc_matricula.ToString();
            }
            cbOrdenSErvicio.Checked = vDetalle.orden_servicio == 1 ? true : false;
            chkMora.Checked = vDetalle.permite_mora == 1 ? true : false;
            //RECUPERAR DATOS - GRILLA BENEFICIARIO
            List<RequisitosLineaAuxilio> LstConsulta = new List<RequisitosLineaAuxilio>();

            LstConsulta = LineaAux.ConsultarDETALLELineaAux(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);
            if (LstConsulta.Count > 0)
            {
                if ((LstConsulta != null) || (LstConsulta.Count != 0))
                {
                    gvRequisitos.DataSource = LstConsulta;
                    gvRequisitos.DataBind();
                }
                Session["DatosRequisitos"] = LstConsulta;
            }
            else
            {
                InicializargvRequisitos();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(LineaAux.CodigoPrograma, "ObtenerDatos", ex);
        }
    }


    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }

    public Boolean ValidarDatos()
    {

        if (txtCodigo.Text == "")
        {
            VerError("Ingrese un Codigo");
            return false;
        }
        if (txtNombre.Text == "")
        {
            VerError("Ingrese el Nombre");
            return false;
        }
        if (txtMntoMinimo.Text == "0")
        {
            VerError("Ingrese el monto mínimo");
            return false;
        }
        if (txtMtoMaximo.Text == "0")
        {
            VerError("Ingrese el monto máximo");
            return false;
        }
        if (chkEducativo.Checked)
        {
            if (txtPorcentajeMatri.Text == "")
            {
                VerError("Ingrese el Porcentaje de matricula");
                txtPorcentajeMatri.Focus();
                return false;
            }
            if (Convert.ToDecimal(txtPorcentajeMatri.Text) > 100)
            {
                VerError("El porcentaje ingresado no puede superar el 100%");
                txtPorcentajeMatri.Focus();
                return false;
            }
        }

        return true;
    }

    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");

        if (ValidarDatos())
        {
            ctlMensaje.MostrarMensaje("Desea " + Session["TEXTO"].ToString() + " los Datos Ingresados?");
        }
    }



    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        try
        {
            LineaAuxilio pVar = new LineaAuxilio();

            if (txtCodigo.Text != "")
                pVar.cod_linea_auxilio = txtCodigo.Text;
            else
                pVar.cod_linea_auxilio = "";
            pVar.descripcion = txtNombre.Text.ToUpper();

            if (chkEstado.Checked)
                pVar.estado = 1;
            else
                pVar.estado = 0;

            pVar.monto_minimo = Convert.ToDecimal(txtMntoMinimo.Text);
            pVar.monto_maximo = Convert.ToDecimal(txtMtoMaximo.Text);

            if (ddlPeriodicidad.SelectedIndex != 0)
                pVar.cod_periodicidad = Convert.ToInt32(ddlPeriodicidad.SelectedValue);
            else
                pVar.cod_periodicidad = 0;

            if (ddlTipoPersona.SelectedIndex != 0)
                pVar.tipo_persona = ddlTipoPersona.SelectedValue;
            else
                pVar.tipo_persona = null;

            if (txtNumAuxilios.Text != "")
                pVar.numero_auxilios = Convert.ToInt32(txtNumAuxilios.Text);
            else
                pVar.numero_auxilios = 0;

            if (txtDiasDesembolso.Text != "")
                pVar.dias_desembolso = Convert.ToInt32(txtDiasDesembolso.Text);
            else
                pVar.dias_desembolso = 0;

            if (chkRetencion.Checked)
                pVar.cobra_retencion = 1;
            else
                pVar.cobra_retencion = 0;

            if (chkRetirados.Checked)
                pVar.permite_retirados = 1;
            else
                pVar.permite_retirados = 0;

            pVar.educativo = chkEducativo.Checked ? 1 : 0;
            pVar.orden_servicio = cbOrdenSErvicio.Checked ? 1 : 0;
            pVar.permite_mora = chkMora.Checked ? 1 : 0;
            pVar.porc_matricula = 0;
            if (txtPorcentajeMatri.Visible == true)
            {
                if (txtPorcentajeMatri.Text != "")
                    pVar.porc_matricula = Convert.ToDecimal(txtPorcentajeMatri.Text);
            }

            pVar.lstRequisitos = new List<RequisitosLineaAuxilio>();
            pVar.lstRequisitos = ObtenerListaRequisitos();


            if (idObjeto != "")
            {
                //MODIFICAR
                LineaAux.Crear_MOD_LineaAuxilio(pVar, (Usuario)Session["usuario"], 2);
            }
            else
            {
                LineaAuxilio validar = new LineaAuxilio();
                validar = LineaAux.ConsultarLineaAUXILIO(txtCodigo.Text, (Usuario)Session["usuario"]);
                if (validar.cod_linea_auxilio != null)
                {
                    VerError("Ya Existe una Linea con el mismo Código");
                    return;
                }
                //CREAR
                LineaAux.Crear_MOD_LineaAuxilio(pVar, (Usuario)Session["usuario"], 1);
            }
            lblNroMsj.Text = pVar.cod_linea_auxilio.ToString();
            Site toolBar = (Site)Master;
            toolBar.MostrarGuardar(false);

            mvAplicar.ActiveViewIndex = 1;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(LineaAux.CodigoPrograma, "btnContinuar_Click", ex);
        }
    }

    protected List<RequisitosLineaAuxilio> ObtenerListaRequisitos()
    {
        try
        {
            List<RequisitosLineaAuxilio> lstDetalle = new List<RequisitosLineaAuxilio>();
            List<RequisitosLineaAuxilio> lista = new List<RequisitosLineaAuxilio>();

            foreach (GridViewRow rfila in gvRequisitos.Rows)
            {
                RequisitosLineaAuxilio ePogra = new RequisitosLineaAuxilio();

                TextBoxGrid txtCodigo = (TextBoxGrid)rfila.FindControl("txtCodigo");
                if (txtCodigo.Text != "")
                    ePogra.codrequisitoaux = Convert.ToInt64(txtCodigo.Text);
                else
                    ePogra.codrequisitoaux = -1;

                TextBoxGrid txtDescripcion = (TextBoxGrid)rfila.FindControl("txtDescripcion");
                if (txtDescripcion.Text != "")
                    ePogra.descripcion = txtDescripcion.Text;

                CheckBoxGrid chkRequerido = (CheckBoxGrid)rfila.FindControl("chkRequerido");
                if (chkRequerido.Checked)
                    ePogra.requerido = 1;
                else
                    ePogra.requerido = 0;

                lista.Add(ePogra);
                Session["DatosRequisitos"] = lista;

                if (ePogra.codrequisitoaux != 0 && ePogra.descripcion != null)
                {
                    lstDetalle.Add(ePogra);
                }
            }
            return lstDetalle;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(LineaAux.CodigoPrograma, "ObtenerListaplan", ex);
            return null;
        }
    }



    protected void btnAdicionarFila_Click(object sender, EventArgs e)
    {
        ObtenerListaRequisitos();
        List<RequisitosLineaAuxilio> LstPrograma = new List<RequisitosLineaAuxilio>();
        if (Session["DatosRequisitos"] != null)
        {
            LstPrograma = (List<RequisitosLineaAuxilio>)Session["DatosRequisitos"];

            for (int i = 1; i <= 1; i++)
            {
                RequisitosLineaAuxilio ePlan = new RequisitosLineaAuxilio();
                ePlan.codrequisitoaux = -1;
                ePlan.descripcion = "";
                ePlan.requerido = null;

                LstPrograma.Add(ePlan);
            }
            gvRequisitos.PageIndex = gvRequisitos.PageCount;
            gvRequisitos.DataSource = LstPrograma;
            gvRequisitos.DataBind();

            Session["DatosRequisitos"] = LstPrograma;
        }
    }



    protected void gvPlanes_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int conseID = Convert.ToInt32(gvRequisitos.DataKeys[e.RowIndex].Values[0].ToString());

        ObtenerListaRequisitos();

        List<RequisitosLineaAuxilio> LstDetalle = new List<RequisitosLineaAuxilio>();
        LstDetalle = (List<RequisitosLineaAuxilio>)Session["DatosRequisitos"];
        if (conseID > 0)
        {
            try
            {
                foreach (RequisitosLineaAuxilio acti in LstDetalle)
                {
                    if (acti.codrequisitoaux == conseID)
                    {
                        LineaAux.EliminarDETALLELineaAux(conseID, (Usuario)Session["usuario"]);
                        LstDetalle.Remove(acti);
                        break;
                    }
                }
                Session["DatosRequisitos"] = LstDetalle;

                gvRequisitos.DataSourceID = null;
                gvRequisitos.DataBind();
                gvRequisitos.DataSource = LstDetalle;
                gvRequisitos.DataBind();

            }
            catch (Xpinn.Util.ExceptionBusiness ex)
            {
                VerError(ex.Message);
            }
            catch (Exception ex)
            {
                BOexcepcion.Throw(LineaAux.CodigoPrograma, "gvPlanes_RowDeleting", ex);
            }
        }
        else
        {
            foreach (RequisitosLineaAuxilio acti in LstDetalle)
            {
                if (acti.codrequisitoaux == conseID)
                {
                    LstDetalle.Remove(acti);
                    break;
                }
            }
            Session["DatosRequisitos"] = LstDetalle;

            gvRequisitos.DataSourceID = null;
            gvRequisitos.DataBind();
            gvRequisitos.DataSource = LstDetalle;
            gvRequisitos.DataBind();
        }
    }

    protected void gvPlanes_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownListGrid ddlGrupo = (DropDownListGrid)e.Row.FindControl("ddlGrupo");
            if (ddlGrupo != null)
                PoblarLista("grupo_familiar", ddlGrupo);

            Label lblGrupo = (Label)e.Row.FindControl("lblGrupo");
            if (lblGrupo != null)
                ddlGrupo.SelectedValue = lblGrupo.Text;

        }
    }

    protected void chkEducativo_CheckedChanged(object sender, EventArgs e)
    {
        lblPorcentMatr.Visible = chkEducativo.Checked ? true : false;
        txtPorcentajeMatri.Visible = chkEducativo.Checked ? true : false;
    }
}

