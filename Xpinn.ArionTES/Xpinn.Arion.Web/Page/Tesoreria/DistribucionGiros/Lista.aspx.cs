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
using System.Globalization;
using Xpinn.Tesoreria.Entities;
using Xpinn.Tesoreria.Services;

public partial class Lista : GlobalWeb
{
    GiroDistribucionService objGiro = new GiroDistribucionService();
    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(objGiro.CodigoPrograma, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
            ctlMensaje.eventoClick += btnContinuar_Click;
            cargarDatosDDL();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(objGiro.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                listaCombos();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(objGiro.CodigoPrograma, "Page_Load", ex);
        }
    }

    protected void listaCombos()
    {

        List<GiroDistribucion> ListaTipo = null;

        cargarListaParaDdll(ListaTipo , ddlTipoCompro, 1);

        cargarListaParaDdll(ListaTipo = null, ddlGenerado, 2);

        cargarListaParaDdll(ListaTipo = null, ddlBancoGiro, 3);

        cargarListaParaDdll(ListaTipo = null, ddlCuentaGiro, 4);

        cargarListaParaDdll(ListaTipo = null, ddlUsuario, 5);

        Dictionary<int, string> ListaFormaPago = new Dictionary<int, string>();
        ListaFormaPago.Add(1, "Efectivo");
        ListaFormaPago.Add(2, "Cheque");
        ListaFormaPago.Add(3, "Transferencia");
        
        ddlFormaPago.DataSource = ListaFormaPago.ToList();
        ddlFormaPago.DataTextField = "Value";
        ddlFormaPago.DataValueField = "Key";
        ddlFormaPago.DataBind();
    }

    protected void cargarListaParaDdll(List<GiroDistribucion> listaAcargar, DropDownList controlAcargar, int opcion)
    {
        try
        { 
        listaAcargar = objGiro.listarDDlTipoComServices((Usuario)Session["usuario"], opcion);
        listaAcargar.Insert(0, new GiroDistribucion { Descripcion = "Seleccione", iddetgiro = -1 });
        if (listaAcargar.Count > 0)
            foreach (var item in listaAcargar)
            {
                controlAcargar.Items.Add(item.Descripcion.ToString());
                controlAcargar.Items.FindByText(item.Descripcion).Value = item.iddetgiro.ToString();
            }
        }
        catch
        {
            VerError("Error al cargar la lista");
        }
    }


    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        Actualizar();
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        gvLista.DataBind();
        lblTotalRegs.Text = "";
    }

    protected void gvLista_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            //ConfirmarEliminarFila(e, "btnBorrar");
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(objGiro.CodigoPrograma + "L", "gvLista_RowDataBound", ex);
        }
    }

    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        String id = gvLista.DataKeys[gvLista.SelectedRow.RowIndex].Value.ToString();
        Session[objGiro.CodigoPrograma + ".id"] = id;
        Navegar(Pagina.Nuevo);
    }

    void cargarDatosDDL() 
    {
        Dictionary<string,string> listaddl = new Dictionary<string,string>();
        listaddl.Add("Codigo", "g.idgiro");
        listaddl.Add("Fecha Giro", "g.fec_reg");
        listaddl.Add("Cod.Persona", "g.cod_persona");
        listaddl.Add("Identificacion", "p.identificacion");
        listaddl.Add("Nombre", "p.nombre");
        listaddl.Add("Cod_Operacion", "g.cod_ope");
        listaddl.Add("Num.Com", "g.num_comp");
        listaddl.Add("Tipo.Com", "g.tipo_comp");
        listaddl.Add("Generada", "ta.descripcion");
        listaddl.Add("Forma Pago", "g.forma_pago");
        listaddl.Add("Banco", " b.nombrebanco");
        listaddl.Add("Cuenta", "g.num_cuenta");


        ddlOrdenarPor.DataSource = listaddl;
        ddlOrdenarPor.DataTextField = "Key";
        ddlOrdenarPor.DataValueField = "Value";
        ddlOrdenarPor.DataBind();
        ddlLuegoPor.DataSource = listaddl;
        ddlLuegoPor.DataTextField = "Key";
        ddlLuegoPor.DataValueField = "Value";
        ddlLuegoPor.DataBind();
        var item = new ListItem("asc", "asc");
        var items = new ListItem("desc", "desc");
        ddlOrdes.Items.Add(item);
        ddlOrdes.Items.Add(items);
    }

    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvLista.DataKeys[e.NewEditIndex].Value.ToString();
        String identifi = Convert.ToString(gvLista.DataKeys[e.NewEditIndex].Values[1].ToString());
        String Nombre = Convert.ToString(gvLista.DataKeys[e.NewEditIndex].Values[2].ToString());
        Session["Nombre"] = Nombre;
        Session["Idccion"] = identifi;
        Session[objGiro.CodigoPrograma + ".id"] = id;
        Navegar(Pagina.Nuevo);
    }

    protected String getFiltro()
    {
        String Filtro = "";
        if (txtCodGiro.Text.Trim()!= "")
            Filtro += " and g.idgiro = "+txtCodGiro.Text;

        if (txtFechaGiro.Text.Trim() != "")
            Filtro += " and g.fec_reg = TO_DATE('" + txtFechaGiro.Text +"','"+gFormatoFecha+"')";

        if (txtIdentificacion.Text.Trim() != "")
            Filtro += " and p.identificacion = '" + txtIdentificacion.Text +"'";

        if (txtNombres.Text.Trim() != "" && txtApellidos.Text.Trim()!= "")
            Filtro += " and p.nombre LIKE '%" + txtIdentificacion.Text+" %'";

        if (txtNumCom.Text.Trim() != "")
            Filtro += " and g.num_comp = " + txtNumCom.Text;

        if(ddlTipoCompro.SelectedIndex>0)
            Filtro+= " and g.tipo_comp = "+ ddlTipoCompro.SelectedValue;

        if (ddlGenerado.SelectedIndex > 0)
            Filtro += " and ta.tipo_acto = " + ddlGenerado.SelectedValue;

        if (ddlFormaPago.SelectedIndex > 0)
            Filtro += " and g.forma_pago = " + ddlFormaPago.SelectedValue;

        if (ddlBancoGiro.SelectedIndex > 0)
            Filtro += " and b.cod_banco = " + ddlBancoGiro.SelectedValue;

        if (ddlCuentaGiro.SelectedIndex > 0)
            Filtro += " and cb.cod_cuenta= '" + ddlCuentaGiro.SelectedValue + "'";

        if (ddlUsuario.SelectedIndex > 0)
            Filtro += "" + ddlUsuario.SelectedValue;

        if (ddlOrdenarPor.SelectedIndex<=0 && ddlLuegoPor.SelectedIndex>0)
        {
            Filtro+=" order by " + ddlLuegoPor.SelectedValue;
            Filtro += " " + ddlOrdes.SelectedValue;
        }

        if (ddlOrdenarPor.SelectedIndex > 0 && ddlLuegoPor.SelectedIndex<=0)
        {
            Filtro += " order by "+ddlOrdenarPor.SelectedValue;
            Filtro += " " + ddlOrdes.SelectedValue;
        }
        if (ddlOrdenarPor.SelectedIndex>0 && ddlLuegoPor.SelectedIndex>0)
        {
            Filtro += " order by " + ddlOrdenarPor.SelectedValue + "," + ddlLuegoPor.SelectedValue;
            Filtro += " " + ddlOrdes.SelectedValue;
        }
        //if (ddlLuegoPor.SelectedIndex > 0)
        //    Filtro += "," + ddlLuegoPor.SelectedValue;


        return Filtro;
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
            BOexcepcion.Throw(objGiro.CodigoPrograma, "gvLista_PageIndexChanging", ex);
        }
    }


    private void Actualizar()
    {
        try
        {
            List<GiroDistribucion> ListarGrid = objGiro.getListaGiroServices((Usuario)Session["usuario"], getFiltro());
            gvLista.PageSize = pageSize;
            gvLista.EmptyDataText = emptyQuery;
            gvLista.DataSource = ListarGrid;

            if (ListarGrid.Count > 0)
            {
                foreach (var item in ListarGrid)
                {
                    switch (item.forma_Pago)
                    {
                        case "1":
                            item.forma_Pago = "Efectivo";
                            break;
                        case "2":
                            item.forma_Pago = "Cheque";
                            break;
                        case "3":
                            item.forma_Pago = "Transferencia";
                            break;
                    }
                    item.nom_estado = NomEstadosGiro(item.estado.ToString());
                }
                gvLista.Visible = true;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + ListarGrid.Count.ToString();
                gvLista.DataBind();
                ValidarPermisosGrilla(gvLista);
            }
            else
            {
                gvLista.Visible = false;
                lblTotalRegs.Visible = false;
            }
            Session.Add(objGiro.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(objGiro.CodigoPrograma, "Actualizar", ex);
        }

    }//40205


    private void btnContinuar_Click(object sender, EventArgs e)
    {
        if (Session["ID"] != null)
        {
            //objGiro.EliminarTipoOpeServices((Int64)Session["ID"], (Usuario)Session["usuario"]);
            Actualizar();
        }
    }


}