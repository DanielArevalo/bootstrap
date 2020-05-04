using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Nomina.Entities;
using Xpinn.Nomina.Services;
using Xpinn.Util;

public partial class Nuevo : GlobalWeb
{
    string controlfecha = System.DateTime.Now.ToString();
    string dotacion;

    List<Detalle_Dotacion> listadetalle;
    Xpinn.Nomina.Services.DotacionService _dotaservice = new Xpinn.Nomina.Services.DotacionService();
    Xpinn.Nomina.Services.Detalle_DotacionService _detalleservice = new Xpinn.Nomina.Services.Detalle_DotacionService();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            if (Session[_dotaservice.CodigoPrograma + ".id"] == null)
            {
                VisualizarOpciones(_dotaservice.CodigoPrograma, "A");
            }
            else
            {
                VisualizarOpciones(_dotaservice.CodigoPrograma, "D");
            }

            Site toolBar = (Site)Master;
            toolBar.eventoGuardar += btn_guardar_click;
            toolBar.eventoLimpiar += btnlimpiar_Click;
            toolBar.eventoConsultar += (s, evt) =>
            {
                ctlListarEmpleados.Actualizar();
            };
            toolBar.eventoRegresar += (s, evt) =>
            {
                cerrarsession();
                Session.Remove(_dotaservice.CodigoPrograma + ".id");
                Navegar(Pagina.Lista);
            };
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_dotaservice.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        VerError("");

        if (!IsPostBack)
        {
            this.gvdatos.DataSource = ObtenerNuevaLista();
            this.gvdatos.DataBind();
            Site toolBar = (Site)Master;
            LlenarListasDesplegables(TipoLista.TipoIdentificacion, ddlTipoIdentificacion);

            if (Session[_dotaservice.CodigoPrograma + ".id"] != null)
            {
                idObjeto = Session[_dotaservice.CodigoPrograma + ".id"].ToString();
                ObtenerDatos();
                mvPrincipal.SetActiveView(viewDatos);

                toolBar.MostrarConsultar(false);
            }
            else
            {
                toolBar.MostrarGuardar(false);
            }
        }
    }

    protected void ctlListarEmpleados_OnEmpleadoSeleccionado(object sender, EmpleadosArgs e)
    {
        Site toolBar = (Site)Master;
        toolBar.MostrarGuardar(true);
        toolBar.MostrarConsultar(false);

        mvPrincipal.SetActiveView(viewDatos);

        long consecutivoEmpleado = e.IDEmpleado;
        EmpleadoService empleadoService = new EmpleadoService();
        Empleados empleados = empleadoService.ConsultarEmpleadosCodigoEmpleado(consecutivoEmpleado.ToString(), Usuario);

        txtIdentificacion.Text = empleados.identificacion;
        ddlTipoIdentificacion.SelectedValue = empleados.cod_identificacion;
        txtNombreEmpleado.Text = empleados.nombre;
        txtCodigoEmpleado.Text = empleados.consecutivo.ToString();
    }

    protected void ctlListarEmpleados_OnErrorControl(object sender, EmpleadosArgs e)
    {
        if (e.Error != null)
        {
            VerError(e.Error.Message);
        }
    }

    void btn_guardar_click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        if (ValidarCampos())
        {
            GuardarDatos();
        }
    }

    protected void btnlimpiar_Click(object sender, EventArgs e)
    {
        if (mvPrincipal.GetActiveView() == viewEmpleados)
        {
            ctlListarEmpleados.Limpiar();
        }
        else if (mvPrincipal.GetActiveView() == viewDatos)
        {
            txtcantidad.Text = string.Empty;
            txtubicacion.Text = string.Empty;
            Session.Remove("lista");
            this.gvdatos.DataSource = null;
            this.gvdatos.DataBind();
        }
    }

    protected void ObtenerDatos()
    {
        try
        {
            Xpinn.Nomina.Services.DotacionService service = new Xpinn.Nomina.Services.DotacionService();
            Xpinn.Nomina.Entities.Dotacion entitie = new Xpinn.Nomina.Entities.Dotacion();
            Xpinn.Nomina.Entities.Detalle_Dotacion detentitie = new Xpinn.Nomina.Entities.Detalle_Dotacion();

            entitie.id_dotacion = Convert.ToInt64(idObjeto);
            entitie = service.ConsultarDotacion(entitie.id_dotacion, (Usuario)Session["usuario"]);
            var id = entitie.id_dotacion;
            detentitie.id_dotacion = entitie.id_dotacion;

            List<Detalle_Dotacion> entititedetalle = _detalleservice.ListarDetalle_Dotacion(detentitie, Usuario);

            if (entititedetalle.Count <= 0)
            {
                entititedetalle.Add(new Detalle_Dotacion());
            }

            ViewState["lista"] = entititedetalle;
            gvdatos.DataSource = entititedetalle;
            gvdatos.DataBind();

            if (entitie != null)
            {
                if (entitie.id_dotacion != Int64.MinValue)
                    txtconsecutivo.Text = HttpUtility.HtmlDecode(entitie.id_dotacion.ToString().Trim());

                if (entitie.cod_empleado != Int64.MinValue)
                    txtCodigoEmpleado.Text = HttpUtility.HtmlDecode(entitie.cod_empleado.ToString().Trim());

                if (entitie.fecha != DateTime.Now)
                    controlfecha = HttpUtility.HtmlDecode(entitie.fecha.ToString().Trim());

                if (!string.IsNullOrWhiteSpace(entitie.ubicacion))
                    txtubicacion.Text = HttpUtility.HtmlDecode(entitie.ubicacion.ToString().Trim());

                if (entitie.cantidad != Int64.MinValue)
                    txtcantidad.Text = HttpUtility.HtmlDecode(entitie.cantidad.ToString().Trim());

                if (!string.IsNullOrEmpty(entitie.centro_costo.ToString()))
                    ctlCentroCosto.Value = HttpUtility.HtmlDecode(entitie.centro_costo.ToString().Trim());

                txtNombreEmpleado.Text = HttpUtility.HtmlDecode(entitie.nombre_empleado);
                txtIdentificacion.Text = HttpUtility.HtmlDecode(entitie.identificacion);
                ddlTipoIdentificacion.SelectedValue = HttpUtility.HtmlDecode(entitie.cod_tipo_identificacion.ToString());
            }

        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }


    #region lista de detalles

    protected void gvdatos_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("AddNew"))
        {
            TextBox pdescripcion = (TextBox)gvdatos.FooterRow.FindControl("txtdescripcion");
            TextBox pcaracteristica = (TextBox)gvdatos.FooterRow.FindControl("txtcaracteristica");
            TextBox pvalor = (TextBox)gvdatos.FooterRow.FindControl("txtvalor");


            if (pdescripcion.Text == string.Empty)
            {
                pdescripcion.Focus();
                return;
            }

            if (pcaracteristica.Text == string.Empty)
            {
                pcaracteristica.Focus();
                return;
            }

            if (pvalor.Text == string.Empty)
            {
                pvalor.Focus();
                return;
            }

            Detalle_Dotacion pdetalle = new Detalle_Dotacion();
            pdetalle.descripcion = pdescripcion.Text;
            pdetalle.caracteristica = pcaracteristica.Text;
            pdetalle.valor = Convert.ToDecimal(pvalor.Text);

            this.GuardarLista(pdetalle);

            this.gvdatos.DataSource = this.Obtenerlista();
            this.gvdatos.DataBind();
        }
        else if (e.CommandName == "Delete")
        {
            int index = GetRowIndexOfControlInsideGridViewOneLevel(e.CommandSource as Control);

            object dataKey = gvdatos.DataKeys[index].Value.ToString();

            if (dataKey != null && !string.IsNullOrWhiteSpace(dataKey as string))
            {
                int consecutivoDetalle = Convert.ToInt32(dataKey);

                if (consecutivoDetalle > 0)
                {
                    _detalleservice.EliminarDetalle_Dotacion(consecutivoDetalle, Usuario);
                }
            }

            List<Detalle_Dotacion> lista = Obtenerlista();
            lista.RemoveAt(index);

            if (lista.Count <= 0)
            {
                lista.Add(new Detalle_Dotacion());
            }

            gvdatos.DataSource = lista;
            gvdatos.DataBind();
        }
    }

    private List<Detalle_Dotacion> GuardarLista(Detalle_Dotacion pdetalle)
    {
        if (ViewState["lista"] == null)
        {
            List<Detalle_Dotacion> p = new List<Detalle_Dotacion>();
            p.Add(pdetalle);
            ViewState["lista"] = p;
        }
        else
        {
            List<Detalle_Dotacion> p = (List<Detalle_Dotacion>)ViewState["lista"];
            p.Add(pdetalle);
            ViewState["lista"] = p;
        }
        return (List<Detalle_Dotacion>)ViewState["lista"];
    }

    private List<Detalle_Dotacion> Obtenerlista()
    {
        if (ViewState["lista"] == null)
            return this.ObtenerNuevaLista();
        else
            return (List<Detalle_Dotacion>)ViewState["lista"];
    }


    public List<Detalle_Dotacion> ObtenerNuevaLista()
    {
        List<Detalle_Dotacion> detalle = new List<Detalle_Dotacion>();

        Detalle_Dotacion pdetalle = new Detalle_Dotacion();

        detalle.Add(pdetalle);

        return detalle;
    }



    #endregion

    Dotacion ConsultarDotacion(Int64 iddotacion)
    {
        try
        {
            if (!string.IsNullOrWhiteSpace(iddotacion.ToString())) return null;

            Dotacion entiti = _dotaservice.ConsultarDotacion(Convert.ToInt64(iddotacion), Usuario);

            return entiti;
        }
        catch (Exception ex)
        {
            VerError("Error al consultar la nomina, " + ex.Message);
            return null;
        }
    }

    void LlenarDotacion(Dotacion pdatoacion, Detalle_Dotacion pdetalle)
    {
        txtconsecutivo.Text = Convert.ToInt64(pdatoacion.id_dotacion).ToString();
        txtCodigoEmpleado.Text = Convert.ToInt64(pdatoacion.cod_empleado).ToString();
        txtcantidad.Text = Convert.ToInt64(pdatoacion.cantidad).ToString();
        txtubicacion.Text = pdatoacion.ubicacion;
        controlfecha = Convert.ToString(pdatoacion.fecha);
        ctlCentroCosto.Text = Convert.ToString(pdatoacion.centro_costo);


        var list = ViewState["lista"] as List<Detalle_Dotacion>;
        list.Add(pdetalle);
    }

    void GuardarDatos()
    {

        Xpinn.Nomina.Services.Detalle_DotacionService detaservice = new Xpinn.Nomina.Services.Detalle_DotacionService();
        Xpinn.Nomina.Services.DotacionService dotaservice = new Xpinn.Nomina.Services.DotacionService();
        Xpinn.Nomina.Entities.Dotacion dotaentities = new Xpinn.Nomina.Entities.Dotacion();
        Xpinn.Nomina.Entities.Detalle_Dotacion detaentities = new Xpinn.Nomina.Entities.Detalle_Dotacion();

        try
        {
            if (idObjeto != "")
            {
                dotaentities = dotaservice.ConsultarDotacion(Convert.ToInt64(idObjeto), Usuario);
            }

            if (txtconsecutivo.Text != "")
                dotaentities.id_dotacion = Convert.ToInt64(txtconsecutivo.Text.Trim());

            if (txtCodigoEmpleado.Text != "")
                dotaentities.cod_empleado = Convert.ToInt64(txtCodigoEmpleado.Text.Trim());

            if (txtubicacion.Text != "")
                dotaentities.ubicacion = (txtubicacion.Text != "") ? Convert.ToString(txtubicacion.Text.Trim()).ToUpper() : String.Empty;

            if (txtcantidad.Text != "")
                dotaentities.cantidad = Convert.ToInt64(txtcantidad.Text.Trim());

            if (ctlCentroCosto.Text != "")
                dotaentities.centro_costo = Convert.ToInt32(ctlCentroCosto.Value.Trim());

            dotaentities.fecha = Convert.ToDateTime(controlfecha);

            if (idObjeto == "")
            {
                Dotacion pdotacion = dotaservice.CrearDotacion(dotaentities, Usuario);
                dotaentities.id_dotacion = pdotacion.id_dotacion;

                // se declara edtalle dotacion y despues se toma el id que se retorno de crear dotacion para asignarselo a detalle dotacion  
                Detalle_Dotacion pdetalle = new Detalle_Dotacion();
                pdetalle.id_dotacion = dotaentities.id_dotacion;
                // la session que contiene los registros de la grid, se castea a una lista de detalle
                var list = ViewState["lista"] as List<Detalle_Dotacion>;
                List<Detalle_Dotacion> detalle = new List<Detalle_Dotacion>();
                detalle = list;
                // foreach para recorer la lista de detalles
                foreach (var item in list)
                {
                    if (item.valor == 0 && string.IsNullOrEmpty(item.descripcion.ToUpper()) && string.IsNullOrEmpty(item.caracteristica.ToUpper()))
                    {
                        continue;
                    }
                    else
                    {
                        item.id_dotacion = dotaentities.id_dotacion;
                        pdetalle = detaservice.CrearDetalle_Dotacion(item, Usuario);
                    }
                }


            }
            else
            {
                Dotacion pdotacion = dotaservice.ModificarDotacion(dotaentities, Usuario);

                Detalle_Dotacion pdetalle = new Detalle_Dotacion();
                pdetalle.id_dotacion = dotaentities.id_dotacion;
                var list = ViewState["lista"] as List<Detalle_Dotacion>;

                // se evalua si la grid se ingresaron valores
                if (ViewState["lista"] != null)
                {
                    // for each para recorrer la session 
                    foreach (var item in list)
                    {
                        if (item.valor == 0 && string.IsNullOrEmpty(item.descripcion) && string.IsNullOrEmpty(item.caracteristica))
                        {
                            continue;
                        }
                        else if (item.id_detalle_dotacion > 0)
                        {
                            item.id_dotacion = dotaentities.id_dotacion;
                            pdetalle = detaservice.ModificarDetalle_Dotacion(item, Usuario);
                        }
                        else
                        {
                            item.id_dotacion = dotaentities.id_dotacion;
                            pdetalle = detaservice.CrearDetalle_Dotacion(item, Usuario);
                        }
                    }
                }

            }
            if (dotaentities.id_dotacion != 0)
            {
                Site toolBar = (Site)Master;
                toolBar.MostrarRegresar(true);
                toolBar.MostrarLimpiar(false);
                toolBar.MostrarGuardar(false);

                mvPrincipal.SetActiveView(viewGuardado);

                Session.Remove(dotaservice.CodigoPrograma + ".id");
            }
        }
        catch (Exception ex)
        {
            VerError("Error al guardar el registro, " + ex.Message);
        }

    }

    bool ValidarCampos()
    {
        Page.Validate();
        if (!Page.IsValid ||
            string.IsNullOrWhiteSpace(txtCodigoEmpleado.Text) ||
            string.IsNullOrWhiteSpace(txtubicacion.Text) ||
            string.IsNullOrWhiteSpace(txtcantidad.Text))
        {
            VerError("Faltan Campos por validar");
        }
        return true;
    }

    public void cerrarsession()
    {
        Session.Remove("lista");
    }

    protected void gvdatos_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
}


