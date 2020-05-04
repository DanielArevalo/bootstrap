using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using Xpinn.FabricaCreditos.Services;
using Xpinn.Asesores.Services;
using Xpinn.Asesores.Entities;

public partial class Page_Asesores_TrasladarClientes_Detalle : GlobalWeb
{
    AgendaTipoActividadService tipoActividadServicio = new AgendaTipoActividadService();
    TrasladoClientesService serviceTrasladar = new TrasladoClientesService();

    private void Page_PreInit(object sender, EventArgs e)
    {

        try
        {
            VisualizarOpciones(serviceTrasladar.CodigoPrograma, "D");

            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.MostrarConsultar(false);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(serviceTrasladar.CodigoPrograma + "A", "Page_PreInit", ex);
        }
    }

    void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        ActualizarProductoOficina();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            RadioClientes.Enabled = false;
            RadioProducto.Enabled = false;
            trasladopanel.Visible = false;
            PanelClientes.Visible = false;
            productopanel.Visible = false;
            Todos.Visible = false;
            LlenarComboOficinas();
            LlenarComboOficinas1();
            LlenarComboMotivos();
            ddlAsesores.Visible = false;
            ddlOficina.Visible = false;
            RadioClientes.Visible = false;
            RadioProducto.Visible = false;
            Label1.Visible = false;
            lblasesor.Visible = false;
            Todos.Visible = false;
            lbloficinas.Visible = false;
            productopanel.Visible = false;
            PanelClientes.Visible = false;
            trasladopanel.Visible = false;
            lblasesor.Visible = false;
            productopanel.Visible = false;


        }

    }

    protected void LlenarComboMotivos()
    {

        OficinaService oficinaService = new OficinaService();
        Xpinn.FabricaCreditos.Entities.Oficina oficina = new Xpinn.FabricaCreditos.Entities.Oficina();
        DropDownList2.DataSource = serviceTrasladar.ListarMotivos((Usuario)Session["Usuario"]);
        DropDownList2.DataTextField = "DESCRIPCION";
        DropDownList2.DataValueField = "COD_MOTIVO_TRASLADO";
        DropDownList2.DataBind();


    }
    protected void LlenarComboOficinas()
    {

        OficinaService oficinaService = new OficinaService();
        Xpinn.FabricaCreditos.Entities.Oficina oficina = new Xpinn.FabricaCreditos.Entities.Oficina();
        ddlOficina.DataSource = oficinaService.ListarOficinas(oficina, (Usuario)Session["usuario"]);
        ddlOficina.DataTextField = "Nombre";
        ddlOficina.DataValueField = "Codigo";
        ddlOficina.DataBind();


    }
    protected void LlenarComboOficinas1()
    {
        OficinaService oficinaService = new OficinaService();
        Xpinn.FabricaCreditos.Entities.Oficina oficina = new Xpinn.FabricaCreditos.Entities.Oficina();
        ddlOficina.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));
        ddlNuevaOficina.DataSource = oficinaService.ListarOficinas(oficina, (Usuario)Session["usuario"]);
        ddlNuevaOficina.DataTextField = "Nombre";
        ddlNuevaOficina.DataValueField = "Codigo";
        ddlNuevaOficina.DataBind();
        ddlNuevaOficina.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));

    }
    protected void RadioClientes_CheckedChanged(object sender, System.EventArgs e)
    {
        lblMensaje.Visible = false;
        RadioProducto.Checked = false;
        PanelClientes.Visible = true;
        RadioClientes.Checked = true;
        productopanel.Visible = false;
        trasladopanel.Visible = true;
        Todos.Visible = true;
        ddlTipoPersona.Visible = true;
        mensaje.Visible = true;
    }
    protected void RadioProducto_CheckedChanged(object sender, System.EventArgs e)
    {
        lblMensaje.Visible = false;
        RadioClientes.Checked = false;
        PanelClientes.Visible = false;
        RadioProducto.Checked = true;
        productopanel.Visible = true;
        trasladopanel.Visible = true;
        Todos.Visible = true;
        RadioProducto.Enabled = true;
        ddlTipoPersona.Visible = false;
        mensaje.Visible = false;
    }
    protected void ddlOficina_SelectedIndexChanged(object sender, EventArgs e)
    {

        Label1.Visible = true;
        RadioClientes.Enabled = true;
        RadioProducto.Enabled = true;

        RadioClientes.Visible = true;
        RadioProducto.Visible = true;

        //RadioProducto.Checked = true;
        Todos.Visible = true;

        LlenarComboAsesores(Convert.ToInt64(ddlOficina.SelectedValue));
        lblMensaje.Visible = false;
        if (RadioOficinas.Checked == true)
            ActualizarProductoOficina();
        else
        {
            Actualizar();
        }

        Site toolBar = (Site)this.Master;
        toolBar.MostrarConsultar(true);
    }
    protected void ddlOficina_SelectedIndexChanged1(object sender, EventArgs e)
    {
        LlenarComboAsesores1(Convert.ToInt64(ddlNuevaOficina.SelectedValue));
    }

    protected void LlenarComboAsesores(Int64 iOficina)
    {
        EjecutivoService serviceEjecutivo = new EjecutivoService();
        Ejecutivo ejec = new Ejecutivo();
        ejec.IOficina = iOficina;
        ddlAsesores.DataSource = serviceEjecutivo.ListarAsesores(ejec, (Usuario)Session["usuario"]);
        ddlAsesores.DataTextField = "NombreCompleto";
        ddlAsesores.DataValueField = "IdEjecutivo";
        ddlAsesores.DataBind();
        ddlAsesores.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));
    }

    protected void LlenarComboAsesores1(Int64 iOficina)
    {
        EjecutivoService serviceEjecutivo = new EjecutivoService();
        Ejecutivo ejec = new Ejecutivo();
        ejec.IOficina = iOficina;
        NuevoAsesor.DataSource = serviceEjecutivo.ListarAsesores(ejec, (Usuario)Session["usuario"]);
        NuevoAsesor.DataTextField = "NombreCompleto";
        NuevoAsesor.DataValueField = "IdEjecutivo";
        NuevoAsesor.DataBind();
        NuevoAsesor.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));
    }
    protected void ddlAsesores_SelectedIndexChanged(object sender, EventArgs e)
    {
        RadioClientes.Enabled = true;
        RadioProducto.Enabled = true;
        lblMensaje.Visible = false;
        Todos.Checked = false;
        Actualizar();
    }

    void Actualizar()
    {
        gvClientes.DataSource = serviceTrasladar.ListarClientesAsesor(ObtenerFiltroClientesAsesor(), Usuario);
        gvClientes.DataBind();

        gvProductos.DataSource = serviceTrasladar.ListarProductosAsesor(ObtenerFiltroProductosAsesor(), Usuario);
        gvProductos.DataBind();
    }

    string ObtenerFiltroProductosAsesor()
    {
        string filtro = string.Empty;

        if (!string.IsNullOrWhiteSpace(ddlAsesores.SelectedValue))
        {
            filtro += " and COD_ASESOR_COM = " + ddlAsesores.SelectedValue;
        }

        if (!string.IsNullOrWhiteSpace(txtIdentificacion.Text))
        {
            filtro += " and SIDENTIFICACION LIKE '%" + txtIdentificacion.Text.Trim() + "%'";
        }

        StringHelper stringHelper = new StringHelper();
        filtro = stringHelper.QuitarAndYColocarWhereEnFiltroQuery(filtro);

        return filtro;
    }

    string ObtenerFiltroClientesAsesor()
    {
        string filtro = string.Empty;

        if (!string.IsNullOrWhiteSpace(ddlAsesores.SelectedValue))
        {
            filtro += " and COD_ASESOR = " + ddlAsesores.SelectedValue;
        }

        if (!string.IsNullOrWhiteSpace(txtIdentificacion.Text))
        {
            filtro += " and IDENTIFICACION  LIKE '%" + txtIdentificacion.Text.Trim() + "%'";
        }

        StringHelper stringHelper = new StringHelper();
        filtro = stringHelper.QuitarAndYColocarWhereEnFiltroQuery(filtro);

        return filtro;
    }

    private void ActualizarProductoOficina()
    {
        PanelClientes.Visible = true;
        productopanel.Visible = true;
        trasladopanel.Visible = true;
        var lista = serviceTrasladar.ListarProductos(ObtenerFiltroProductos(), Usuario);
        if (RadioProducto.Checked == true)
        {
            gvProductosOficina.DataSource = lista;
        gvProductosOficina.DataBind();
            PanelClientes.Visible = false;
            productopanel.Visible = true;
        }

        if (RadioClientes.Checked == true)
        {
            gvClientesOficinas.DataSource = lista;
        gvClientesOficinas.DataBind();
            PanelClientes.Visible = true;
            productopanel.Visible = false;
           
        }
    }

    string ObtenerFiltroProductos()
    {
        string filtro = string.Empty;

        if (!string.IsNullOrWhiteSpace(ddlOficina.SelectedValue))
        {
            filtro += " and pr.cod_oficina = " + ddlOficina.SelectedValue;
        }

        if (!string.IsNullOrWhiteSpace(txtIdentificacion.Text))
        {
            filtro += " and pr.IDENTIFICACION  LIKE '%" + txtIdentificacion.Text.Trim() + "%'";
        }
        if (!string.IsNullOrWhiteSpace(txtIdentiClient.Text))
        {
            filtro += " and pr.IDENTIFICACION  LIKE '%" + txtIdentiClient.Text.Trim() + "%'";
        }
        if (ddlTipoPersona.Visible==true)
        {
            filtro += " and PER.tipo_persona='" + ddlTipoPersona.SelectedItem.Value + "'";
        }

        StringHelper stringHelper = new StringHelper();
        filtro = stringHelper.QuitarAndYColocarWhereEnFiltroQuery(filtro);

        return filtro;
    }

    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (RadioAsesores.Checked == true)
            {
                if (ddlAsesores.SelectedValue != NuevoAsesor.SelectedValue || ddlOficina.SelectedValue != ddlNuevaOficina.SelectedValue)
                {
                    serviceTrasladar.CrearRegistroAsesores(2, Convert.ToInt32(NuevoAsesor.SelectedValue), Convert.ToInt32(ddlAsesores.SelectedValue), DropDownList2.SelectedValue, observaciontxt.Text, (Usuario)Session["usuario"]);
                    // modificar si se selecciona traslado por asesores y clientes
                    if (RadioClientes.Checked == true && RadioAsesores.Checked == true)
                    {
                        if (Todos.Checked == true)
                        {
                            serviceTrasladar.ModificarClientesAsesorTodos(Convert.ToInt32(NuevoAsesor.SelectedValue), Convert.ToInt32(ddlAsesores.SelectedValue), (Usuario)Session["usuario"]);
                        }
                        else
                        {
                            foreach (GridViewRow row in gvClientesOficinas.Rows)
                            {
                                lblMensaje.Text = "Trasladando cliente " + GetCellByName(row, "Número Identificación").Text;
                                lblMensaje.Visible = true;
                                System.Web.UI.WebControls.CheckBox chkSeleccionar = ((System.Web.UI.WebControls.CheckBox)row.FindControl("cbx"));

                                System.Web.UI.WebControls.CheckBox chkGenerar = ((System.Web.UI.WebControls.CheckBox)row.FindControl("chkGenerar"));

                                if (chkGenerar.Checked)
                                {
                                    string Documento = GetCellByName(row, "Número Identificación").Text;
                                    serviceTrasladar.ModificarClientesAsesor(Convert.ToInt32(NuevoAsesor.SelectedValue), Convert.ToInt32(Documento), (Usuario)Session["usuario"]);

                                }
                            }
                        }
                    }

                    // si se actualiza por la opcion de asesores y producto 
                    if (RadioProducto.Checked == true && RadioAsesores.Checked == true)
                    {
                        if (Todos.Checked == true)
                        {
                            serviceTrasladar.ModificarProductosAsesorTodos(Convert.ToInt32(NuevoAsesor.SelectedValue), Convert.ToInt32(ddlAsesores.SelectedValue), (Usuario)Session["usuario"]);
                        }
                        else
                        {

                            foreach (GridViewRow row in gvProductos.Rows)
                            {
                                lblMensaje.Text = "Trasladando cliente " + GetCellByName(row, "Número de Producto").Text;
                                lblMensaje.Visible = true;
                                System.Web.UI.WebControls.CheckBox chkSeleccionar = ((System.Web.UI.WebControls.CheckBox)row.FindControl("cbx"));

                                System.Web.UI.WebControls.CheckBox chkGenerar = ((System.Web.UI.WebControls.CheckBox)row.FindControl("chkGenerar"));

                                if (chkGenerar.Checked)
                                {
                                    string Documento = GetCellByName(row, "Número de Producto").Text;
                                    serviceTrasladar.ModificarProductosAsesor(Convert.ToInt32(NuevoAsesor.SelectedValue), Convert.ToInt32(Documento), (Usuario)Session["usuario"]);
                                }
                            }
                        }
                    }
                    lblMensaje.Text = "Datos Actualizados";
                    lblMensaje.Visible = true;
                    GenerarComporbante();

                    // Actualizar();
                    observaciontxt.Text = "";
                }
                else
                {
                    lblMensaje.Text = "Error al actualizar verifique que su traslado no tenga como destino el mismo asesor";
                    lblMensaje.Visible = true;
                }
            }


            if (RadioOficinas.Checked == true && ddlOficina.SelectedValue != ddlNuevaOficina.SelectedValue)
            {
                serviceTrasladar.CrearRegistroOficinas(1, Convert.ToInt32(ddlNuevaOficina.SelectedValue), Convert.ToInt32(ddlOficina.SelectedValue), DropDownList2.SelectedValue, observaciontxt.Text, (Usuario)Session["usuario"]);


                // si se actualiza por la opcion de oficinas y clientes 

                if (RadioOficinas.Checked == true || RadioClientes.Checked == true)
                {
                    if (Todos.Checked == true)
                    {

                        foreach (GridViewRow row in gvClientesOficinas.Rows)
                        {
                            int Documento = Convert.ToInt32(row.Cells[2].Text);
                            //int Producto = Convert.ToInt32(row.Cells[3].Text);
                            serviceTrasladar.ModificarClientesOficinasTodos(Convert.ToInt32(ddlNuevaOficina.SelectedValue), Documento, (Usuario)Session["usuario"]);
                        }
                    }
                    else
                    {
                        foreach (GridViewRow row in gvClientesOficinas.Rows)
                        {
                            lblMensaje.Text = "Trasladando cliente ";// +GetCellByName(row, "Número Identificación").Text;
                            lblMensaje.Visible = true;
                            //System.Web.UI.WebControls.CheckBox chkSeleccionar = ((System.Web.UI.WebControls.CheckBox)row.FindControl("cbx"));

                            System.Web.UI.WebControls.CheckBox chkGenerar = ((System.Web.UI.WebControls.CheckBox)row.FindControl("chkGenerar"));

                            if (chkGenerar.Checked)
                            {
                                int Documento = Convert.ToInt32(row.Cells[0].Text);
                                int DocumentoId = Convert.ToInt32(row.Cells[2].Text);
                                // int Producto = Convert.ToInt32(row.Cells[3].Text);
                                serviceTrasladar.ModificarClientesOficinas(Convert.ToInt32(this.ddlNuevaOficina.SelectedValue), Convert.ToInt32(DocumentoId), (Usuario)Session["usuario"]);
                                serviceTrasladar.ModificarProductosOficina2(Convert.ToInt32(ddlNuevaOficina.SelectedValue), Convert.ToInt32(Documento), (Usuario)Session["usuario"]);

                            }
                        }
                    }
                }
                // si se actualiza por la opcion de oficinas y producto 

                if (RadioProducto.Checked == true && RadioOficinas.Checked == true)
                {
                    if (Todos.Checked == true)
                    {
                        foreach (GridViewRow row in gvProductosOficina.Rows)
                        {
                            string Documento = row.Cells[0].Text;
                            int Producto = Convert.ToInt32(row.Cells[3].Text);
                            serviceTrasladar.ModificarProductosOficinasTodos(Producto, Convert.ToInt32(ddlNuevaOficina.SelectedValue), Convert.ToInt32(ddlOficina.SelectedValue), (Usuario)Session["usuario"]);
                        }
                    }
                    else
                    {

                        foreach (GridViewRow row in gvProductosOficina.Rows)
                        {
                            lblMensaje.Text = "Trasladando cliente ";      //+GetCellByName(row, "Número de Producto").Text;
                            lblMensaje.Visible = true;
                            //System.Web.UI.WebControls.CheckBox chkSeleccionar = ((System.Web.UI.WebControls.CheckBox)row.FindControl("cbx"));

                            System.Web.UI.WebControls.CheckBox chkGenerar = ((System.Web.UI.WebControls.CheckBox)row.FindControl("chkGenerar"));

                            if (chkGenerar.Checked)
                            {
                                string Documento = row.Cells[0].Text;
                                int Producto = Convert.ToInt32(row.Cells[3].Text);
                                string Identificacion = row.Cells[6].Text;
                                // string Documento = GetCellByName(row, "Número de Producto").Text;
                                serviceTrasladar.ModificarProductosOficina(Producto, Convert.ToInt32(ddlNuevaOficina.SelectedValue), Convert.ToInt32(Documento), (Usuario)Session["usuario"]);
                                serviceTrasladar.ModificarClientesOficinas(Convert.ToInt32(this.ddlNuevaOficina.SelectedValue), Convert.ToInt32(Identificacion), (Usuario)Session["usuario"]);

                            }
                        }
                    }
                }

                lblMensaje.Text = "Datos Actualizados";
                lblMensaje.Visible = true;
                //Actualizar();
                GenerarComporbante();
                //ActualizarProductoOficina();
                observaciontxt.Text = "";

            }
            else
            {
                lblMensaje.Text = "Error al actualizar verifique que su traslado no tenga como destino la misma oficina";
                lblMensaje.Visible = true;
            }

        }
        catch
        {

            lblMensaje.Text = "Error al actualizar";
            lblMensaje.Visible = true;
        }
    }

    protected void GenerarComporbante()
    {
        Xpinn.Tesoreria.Entities.Operacion pOperacion = new Xpinn.Tesoreria.Entities.Operacion();
        Xpinn.Tesoreria.Data.OperacionData DATesoreria = new Xpinn.Tesoreria.Data.OperacionData();

        Usuario usuap = new Usuario();
        String Error = "0";
        if (lblMensaje.Text == "Datos Actualizados")
        {
            // Crear la operación
            pOperacion.cod_ope = 0;
            pOperacion.tipo_ope = 107;
            pOperacion.cod_usu = usuap.codusuario;
            pOperacion.cod_ofi = usuap.cod_oficina;
            pOperacion.fecha_oper = DateTime.Now;
            pOperacion.fecha_calc = DateTime.Now;
            pOperacion.num_lista = 0;
            pOperacion.cod_ope = DATesoreria.CrearOperacion(pOperacion, ref Error, (Usuario)Session["usuario"]);
            pOperacion.cod_ope = pOperacion.cod_ope;


            Xpinn.Contabilidad.Services.ComprobanteService ComprobanteServicio = new Xpinn.Contabilidad.Services.ComprobanteService();
            Session[ComprobanteServicio.CodigoPrograma + ".cod_ope"] = pOperacion.cod_ope;
            Session[ComprobanteServicio.CodigoPrograma + ".tipo_ope"] = pOperacion.tipo_ope;
            Session[ComprobanteServicio.CodigoPrograma + ".fecha_ope"] = pOperacion.fecha_oper;
            Session[ComprobanteServicio.CodigoPrograma + ".cod_persona"] = -1;
            Session[ComprobanteServicio.CodigoPrograma + ".cod_ofi_ope"] = pOperacion.cod_ofi;
            Navegar("../../Contabilidad/Comprobante/Nuevo.aspx");

        }
    }


    static public DataControlFieldCell GetCellByName(GridViewRow Row, String CellName)
    {
        foreach (DataControlFieldCell Cell in Row.Cells)
        {
            if (Cell.ContainingField.ToString().ToUpper() == CellName.ToUpper())
                return Cell;
        }
        return null;
    }

    protected void Todos_CheckedChanged(object sender, EventArgs e)
    {
        lblMensaje.Visible = false;
        if (Todos.Checked == true)
        {
            foreach (GridViewRow row in gvClientesOficinas.Rows)
            {
                System.Web.UI.WebControls.CheckBox chkSeleccionar = ((System.Web.UI.WebControls.CheckBox)row.FindControl("cbx"));

                System.Web.UI.WebControls.CheckBox chkGenerar = ((System.Web.UI.WebControls.CheckBox)row.FindControl("chkGenerar"));

                chkGenerar.Checked = true;
                chkGenerar.Enabled = false;
            }
            foreach (GridViewRow row in gvProductos.Rows)
            {
                System.Web.UI.WebControls.CheckBox chkSeleccionar = ((System.Web.UI.WebControls.CheckBox)row.FindControl("cbx"));

                System.Web.UI.WebControls.CheckBox chkGenerar = ((System.Web.UI.WebControls.CheckBox)row.FindControl("chkGenerar"));

                chkGenerar.Checked = true;
                chkGenerar.Enabled = false;
            }
            foreach (GridViewRow row in gvProductosOficina.Rows)
            {
                System.Web.UI.WebControls.CheckBox chkSeleccionar = ((System.Web.UI.WebControls.CheckBox)row.FindControl("cbx"));

                System.Web.UI.WebControls.CheckBox chkGenerar = ((System.Web.UI.WebControls.CheckBox)row.FindControl("chkGenerar"));

                chkGenerar.Checked = true;
                chkGenerar.Enabled = false;
            }
            foreach (GridViewRow row in gvClientesOficinas.Rows)
            {
                System.Web.UI.WebControls.CheckBox chkSeleccionar = ((System.Web.UI.WebControls.CheckBox)row.FindControl("cbx"));

                System.Web.UI.WebControls.CheckBox chkGenerar = ((System.Web.UI.WebControls.CheckBox)row.FindControl("chkGenerar"));

                chkGenerar.Checked = true;
                chkGenerar.Enabled = false;
            }
        }
        else
        {
            foreach (GridViewRow row in gvClientesOficinas.Rows)
            {
                System.Web.UI.WebControls.CheckBox chkSeleccionar = ((System.Web.UI.WebControls.CheckBox)row.FindControl("cbx"));

                System.Web.UI.WebControls.CheckBox chkGenerar = ((System.Web.UI.WebControls.CheckBox)row.FindControl("chkGenerar"));

                chkGenerar.Checked = false;
                chkGenerar.Enabled = true;

            }
            foreach (GridViewRow row in gvProductos.Rows)
            {
                System.Web.UI.WebControls.CheckBox chkSeleccionar = ((System.Web.UI.WebControls.CheckBox)row.FindControl("cbx"));

                System.Web.UI.WebControls.CheckBox chkGenerar = ((System.Web.UI.WebControls.CheckBox)row.FindControl("chkGenerar"));

                chkGenerar.Checked = false;
                chkGenerar.Enabled = true;

            }
            foreach (GridViewRow row in gvProductosOficina.Rows)
            {
                System.Web.UI.WebControls.CheckBox chkSeleccionar = ((System.Web.UI.WebControls.CheckBox)row.FindControl("cbx"));

                System.Web.UI.WebControls.CheckBox chkGenerar = ((System.Web.UI.WebControls.CheckBox)row.FindControl("chkGenerar"));

                chkGenerar.Checked = false;
                chkGenerar.Enabled = true;

            }
            foreach (GridViewRow row in gvClientesOficinas.Rows)
            {
                System.Web.UI.WebControls.CheckBox chkSeleccionar = ((System.Web.UI.WebControls.CheckBox)row.FindControl("cbx"));

                System.Web.UI.WebControls.CheckBox chkGenerar = ((System.Web.UI.WebControls.CheckBox)row.FindControl("chkGenerar"));

                chkGenerar.Checked = true;
                chkGenerar.Enabled = false;
            }

        }
    }

    protected void btnRegresar_Click(object sender, ImageClickEventArgs e)
    {
        Navegar("~/Page/Asesores/TrasladarCliente/Detalle.aspx");
    }
    protected void RadioOficinas_CheckedChanged(object sender, EventArgs e)
    {
        lblMensaje.Visible = false;
        ddlAsesores.Visible = false;
        lblasesor.Visible = false;
        cvAsesor.Visible = false;
        NuevoAsesor.Visible = false;
        Todos.Visible = true;
        lblcliente.Visible = false;
        lblasesornuevo.Visible = false;
        lblproductos.Visible = false;
        if (RadioOficinas.Checked == true)
            ddlOficina.Visible = true;
        RadioAsesores.Checked = false;

        Label1.Visible = false;
        lbloficinas.Visible = true;
        Todos.Visible = false;
        RadioClientes.Visible = false;
        RadioProducto.Visible = false;
        PanelClientes.Visible = false;
        productopanel.Visible = false;
        trasladopanel.Visible = false;


    }
    protected void RadioAsesores_CheckedChanged(object sender, EventArgs e)
    {
        NuevoAsesor.Visible = true;
        ddlAsesores.Visible = true;
        ddlOficina.Visible = true;
        Label1.Visible = true;
        lbloficinas.Visible = true;
        lblMensaje.Visible = false;
        RadioClientes.Visible = true;
        RadioProducto.Visible = true;
        PanelClientes.Visible = true;
        RadioProducto.Checked = true;
        productopanel.Visible = true;
        trasladopanel.Visible = true;
        lblasesor.Visible = true;
        lblasesornuevo.Visible = true;
        Todos.Visible = true;
        lblcliente.Visible = false;
        lblasesornuevo.Visible = true;
        lblproductos.Visible = false;
        if (RadioAsesores.Checked == true)
            RadioOficinas.Checked = false;
        gvClientesOficinas.DataBind();
        gvProductosOficina.DataBind();
    }

    protected void txtIdentificacion_TextChanged(object sender, EventArgs e)
    {
        ActualizarProductoOficina();
    }
    protected void txtIdentiClient_TextChanged(object sender, EventArgs e)
    {
        ActualizarProductoOficina();
    }
    protected void gvClientesOficinas_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvClientesOficinas.PageIndex = e.NewPageIndex;
            ActualizarProductoOficina();

           
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(serviceTrasladar.CodigoPrograma, "gvLista_PageIndexChanging", ex);
        }
    }
    protected void ddlTipoPersona_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblMensaje.Visible = false;
        RadioProducto.Checked = false;
        PanelClientes.Visible = true;
        RadioClientes.Checked = true;
        productopanel.Visible = false;
        trasladopanel.Visible = true;
        Todos.Visible = true;
        ddlTipoPersona.Visible = true;
        mensaje.Visible = true;
        ActualizarProductoOficina();
    }

}