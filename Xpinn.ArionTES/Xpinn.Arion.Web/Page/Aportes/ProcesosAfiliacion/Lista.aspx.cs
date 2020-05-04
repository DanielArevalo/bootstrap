using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Aportes.Services;
using Xpinn.Aportes.Entities;
using Xpinn.Util;

public partial class Lista : GlobalWeb
{    
    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            Usuario pUsuario = (Usuario)Session["usuario"];
            string codOpcion = Request.Url.Query.ToString().Replace("?CodOpcion=", "");
            Session["CodOpcion"] = codOpcion;
            
            VisualizarOpciones(codOpcion, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoNuevo += btnNuevo_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
            toolBar.MostrarLimpiar(false);
        }
        catch (Exception ex)
        {
            VerError(Session["CodOpcion"].ToString() + "Page_PreInit" + ex.Message);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                Usuario pUsuario = (Usuario)Session["usuario"];
                Session.Remove(pUsuario.codOpcionActual.ToString() + ".id");
                CargarListas();
                Actualizar();
            }
        }
        catch(Exception ex)
        {
            VerError(Session["CodOpcion"].ToString() + "Page_Load" + ex.Message);
        }
    }

    private void CargarListas()
    {
        PoblarListas poblar = new PoblarListas();

        //Cargar usuarios
        poblar.PoblarListaDesplegable("TIPOIDENTIFICACION", "CODTIPOIDENTIFICACION, DESCRIPCION", "", "1", ddlTipo_ID, (Usuario)Session["usuario"]);

        //Cargar tipo de concepto
        ddlConcepto.Items.Insert(0, new ListItem("", ""));
        ddlConcepto.Items.Insert(1, new ListItem("Favorable", "1"));
        ddlConcepto.Items.Insert(2, new ListItem("Desfavorable", "2"));
        ddlConcepto.DataBind();

        //Cargar lugares del proceso
        poblar.PoblarListaDesplegable("OFICINA", "COD_OFICINA, NOMBRE", "", "1", ddlOficina, (Usuario)Session["usuario"]);

        //Cargar usuarios
        poblar.PoblarListaDesplegable("USUARIOS", "CODUSUARIO, NOMBRE", "", "1", ddlUsuario, (Usuario)Session["usuario"]);
    }

    protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    {
        Navegar("~/Page/Aportes/ProcesosAfiliacion/Nuevo.aspx");
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        Actualizar();
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        Usuario pUsuario = (Usuario)Session["usuario"];
        LimpiarValoresConsulta(pBusqueda, pUsuario.codOpcionActual.ToString());
    }

    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        string id = gvLista.DataKeys[e.NewEditIndex].Value.ToString();
        string codOpcion = Session["CodOpcion"].ToString();
        Session[codOpcion.ToString() + ".id"] = id;
        Navegar(Pagina.Nuevo);
    }

    protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvLista.PageIndex = e.NewPageIndex;
            Actualizar();
        }
        catch (Exception ex)
        {
            VerError(Session["CodOpcion"].ToString() + "gvLista_PageIndexChanging" + ex.Message);
        }
    }

    private void Actualizar()
    {
        try
        {
            AfiliacionServices afiliacionServicio = new AfiliacionServices();
            List<ProcesoAfiliacion> lstProcesos = new List<ProcesoAfiliacion>();

            lstProcesos = afiliacionServicio.ListarProcesos(ObtenerFiltro(), (Usuario)Session["usuario"]);


            if (lstProcesos.Count > 0)
            {
                lblTituloGrid.Visible = true;
                gvLista.Visible = true;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados: " + lstProcesos.Count.ToString();
                gvLista.DataSource = lstProcesos;
                gvLista.DataBind();
            }
            else
            {
                lblTituloGrid.Visible = false;
                gvLista.Visible = false;
                lblTotalRegs.Text = "<br/> La consulta no obtuvo ningún resultado";
            }
        }catch(Exception ex)
        {
            VerError("Error al consultar los datos" + ex.Message);
        }
    }

    private string ObtenerFiltro()
    {
        string filtro = "";
        if (ddlTipo_ID.SelectedValue.Trim() != "")
        {
            filtro += filtro == "" ? " WHERE" : " AND";
            filtro += " P.TIPO_IDENTIFICACION = " + ddlTipo_ID.SelectedValue;
        }
        if (txtCodigo.Text != "")
        {
            filtro += filtro == "" ? " WHERE" : " AND";
            filtro += " P.COD_PERSONA = " + txtCodigo.Text;
        }
        if (txtNumeIdentificacion.Text != "")
        {
            filtro += filtro == "" ? " WHERE" : " AND";
            filtro += " P.IDENTIFICACION = " + txtNumeIdentificacion.Text;
        }
        if (txtNombres.Text != "")
        {
            filtro += filtro == "" ? " WHERE" : " AND";
            filtro += " P.NOMBRES = " + txtNombres.Text;
        }
        if (txtApellidos.Text != "")
        {
            filtro += filtro == "" ? " WHERE" : " AND";
            filtro += " P.APELLIDOS = " + txtApellidos.Text;
        }
        if (ddlOficina.SelectedValue.Trim() != "")
        {
            filtro += filtro == "" ? " WHERE" : " AND";
            filtro += " A.COD_OFICINA =" + ddlOficina.SelectedValue;
        }
        if (txtFecha.Text != "")
        {
            filtro += filtro == "" ? " WHERE" : " AND";
            filtro += " A.FECHA = TO_DATE('" + Convert.ToDateTime(txtFecha.Text).ToShortDateString() + "','DD/MM/YYYY')";
        }
        if (ddlUsuario.SelectedValue.Trim() != "")
        {
            filtro += filtro == "" ? " WHERE" : " AND";
            filtro += " A.COD_USUARIO = " + ddlUsuario.SelectedValue;
        }
        if (ddlConcepto.SelectedValue.Trim() != "")
        {
            filtro += filtro == "" ? " WHERE" : " AND";
            filtro += " A.CONCEPTO = " + ddlConcepto.SelectedValue;
        }

        string codOpcion = Session["CodOpcion"].ToString();
        int tipo_proceso  = codOpcion == "170901" ? 1 : codOpcion == "170902" ? 2 : codOpcion == "170903" ? 3 : codOpcion == "170904" ? 4 : 1;
        if(filtro != "")
            filtro += " AND A.TIPO_PROCESO = " + tipo_proceso;
        else
            filtro += " WHERE A.TIPO_PROCESO = " + tipo_proceso;

        return filtro;
    }
}