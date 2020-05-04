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
using Xpinn.Servicios.Services;
using Xpinn.Servicios.Entities;
using Microsoft.Reporting.WebForms;
using Xpinn.Ahorros.Entities;

public partial class Nuevo : GlobalWeb
{

     PoblarListas poblar = new PoblarListas();
    Xpinn.Ahorros.Services.AhorroVistaServices objahorroLibretas = new Xpinn.Ahorros.Services.AhorroVistaServices();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(objahorroLibretas.CodigoProgramaLibreta, "L");
            Site toolBar = (Site)this.Master;
            if (Session[objahorroLibretas.CodigoProgramaLibreta + ".id"] != null)
            {
                
                toolBar.eventoCancelar += btnCancelar_Click;
                toolBar.MostrarGuardar(false);  
                multPrincipal.ActiveViewIndex = 1;                
            }
            else
            {
                multPrincipal.ActiveViewIndex = 0;
                toolBar.eventoCancelar += btnCancelar_Click;
                toolBar.eventoConsultar += btnConsulta_click;
                toolBar.eventoLimpiar += btnLimpiar_Click;
                toolBar.eventoGuardar += btnGuardar_Click;
                toolBar.MostrarGuardar(false);
            }

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(objahorroLibretas.GetType().Name + "L", "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
               
                cargarddl();
                if (Session[objahorroLibretas.CodigoProgramaLibreta + ".id"] != null)
                {
                    
                    idObjeto = Session[objahorroLibretas.CodigoProgramaLibreta + ".id"].ToString();
                    Session.Remove(objahorroLibretas.CodigoProgramaLibreta + ".id");
                    Site toolBar = (Site)this.Master;
                    toolBar.MostrarGuardar(true);
                    cargarCampos(Convert.ToInt64(idObjeto));
                }
                else
                {

                }
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(objahorroLibretas.GetType().Name + "L", "Page_Load", ex);
        }

    }
    // carga ddl
    void cargarddl()
    {
        GlobalWeb glob = new GlobalWeb();
        ddlEstado.DataSource = glob.ListaEstadosLibreta();
        ddlEstado.DataTextField = "descripcion";
        ddlEstado.DataValueField = "codigo";
        ddlEstado.DataBind();
       // ddlEstado.SelectedIndex = 1;

        poblar.PoblarListaDesplegable("lineaahorro", ddlLinea, (Usuario)Session["usuario"]);

        Xpinn.Asesores.Data.OficinaData listaOficina = new Xpinn.Asesores.Data.OficinaData();
        Xpinn.Asesores.Entities.Oficina oficina = new Xpinn.Asesores.Entities.Oficina();
        oficina.Estado = 1;
        var lista = listaOficina.ListarOficina(oficina, (Usuario)Session["usuario"]);

        if (lista != null)
        {
            lista.Insert(0, new Xpinn.Asesores.Entities.Oficina { NombreOficina = "Seleccione un Item", IdOficina = 0 });
            ddlOficina.DataSource = lista;
            ddlOficina.DataTextField = "NombreOficina";
            ddlOficina.DataValueField = "IdOficina";
            ddlOficina.DataBind();
        }
        ddlMotivo.Items.Insert(0, new ListItem("Nueva ", "0"));
        ddlMotivo.Items.Insert(1, new ListItem("Perdida ", "1"));
        ddlMotivo.Items.Insert(2, new ListItem("Cambio", "2"));


    }

    // redirecciona ala pagina principal para cancelar acciones 
    public void btnCancelar_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }

    void cargarCampos(Int64 idObjeto) 
    {
        ELibretas entidad = objahorroLibretas.getLibretaByIdLibretaServices(idObjeto, (Usuario)Session["usuario"]);

        txtNunCuentaG.Text = entidad.numero_cuenta.ToString();
        txtfechaAsig2.ToDateTime =  Convert.ToDateTime(entidad.fecha_asignacion.ToLongDateString());
        TxtLineaG.Text = entidad.Linea.ToString();
        txtSaldoTotalG.Text = entidad.saldo_total.ToString();
        txtIdentifG.Text = entidad.identificacion.ToString();
        txtIdentificacion.Text = entidad.identificacion.ToString();
        txtTipoIdenG.Text = entidad.TipoIdentific.ToString();
        txtNombreG.Text = entidad.nombre.ToString();
        txtLibretaAnG.Text = entidad.numero_libreta.ToString();
        txtDesprendible2.Text = entidad.Num_Desprendible.ToString();
        ddlEstado.SelectedValue = entidad.estado.ToString();
        txtDesprendible.Text = entidad.desde.ToString();
        txtDesGa.Text = entidad.hasta.ToString();
        txtFechaApeG.ToDateTime = Convert.ToDateTime(entidad.fecha_apertura.ToLongDateString());
        
        
       panelnueva.Visible = false;
        multPrincipal.ActiveViewIndex = 1;
        txtNombreG.Enabled = true;

    }

    public String getFiltro()
    {
        String filtro = " ";
        if (txtNumeroCuenta.Text.Trim() != "")
            filtro += " and a.numero_cuenta = " + txtNumeroCuenta.Text;
        if (ddlLinea.SelectedIndex > 0)
            filtro += " and l.cod_linea_ahorro = " + ddlLinea.SelectedValue;
        if (txtIdentificacion.Text.Trim() != "")
            filtro += " and v.identificacion = " + txtIdentificacion.Text;
        if (txtNombre.Text.Trim() != "")
            filtro += " and v.nombre = " + txtNombre.Text;
        if (ddlOficina.SelectedIndex > 0)
            filtro += " and o.cod_oficina = " + ddlOficina.SelectedValue;// editar 
        return filtro;
    }

    public void actualizar()
    {
        List<ELibretas> lista;
        try
        {
            DateTime fecha = txtFechaAperturaBus.Text == "" ? DateTime.MinValue : Convert.ToDateTime(txtFechaAperturaBus.Texto);
            lista = objahorroLibretas.llenarListaNuevoService(fecha, getFiltro(), (Usuario)Session["usuario"]);
            if (lista.Count > 0)
            {
                gvDetalle.Visible = true;
                gvDetalle.DataSource = lista.ToList();
                gvDetalle.DataBind();
            }
            else
            {
                gvDetalle.Visible = false;
                gvDetalle.DataSource = null;
            }
        }
        catch (Exception)
        {

            throw;
        }
    }

    public void btnConsulta_click(object sender, EventArgs e)
    {
        actualizar();
    }

    public void btnLimpiar_Click(object sender, EventArgs e)
    {
        gvDetalle.Visible = false;
        gvDetalle.DataSource = null;
        txtNumeroCuenta.Text = "";
        txtFechaAperturaBus.Text = "";
        ddlLinea.ClearSelection();
        txtIdentificacion.Text = "";
        txtNombre.Text = "";
        ddlOficina.ClearSelection();
    }

    protected void gvDetalle_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvDetalle.DataKeys[e.NewEditIndex].Value.ToString();

        ELibretas entidad = objahorroLibretas.getLibretaByNumeroCuentaService(id, (Usuario)Session["usuario"]);
        Session["idLibreta"] = entidad.id_Libreta;
        Session["numCuenta"] = entidad.numero_cuenta;
        Site toolBar =(Site) this.Master;
        toolBar.MostrarGuardar(true);

        if (entidad != null)
        {
            txtfechaAsig2.Text = DateTime.Now.ToShortDateString();
            txtNunCuentaG.Text = entidad.numero_cuenta.ToString();
            txtFechaApeG.Text = entidad.fecha_asignacion.ToShortDateString();
            TxtLineaG.Text = entidad.Linea.ToString();
            txtSaldoTotalG.Text = entidad.saldo_total.ToString();
            txtIdentifG.Text = entidad.identificacion.ToString();
            txtIdentificacion.Text = entidad.identificacion.ToString();
            txtTipoIdenG.Text = entidad.TipoIdentific.ToString();
            txtNombreG.Text = entidad.nombre.ToString();
            txtLibretaAnG.Text = entidad.numero_libreta.ToString();
            txtDesprendible2.Text = entidad.Num_Desprendible.ToString(); 
            ddlEstado.SelectedValue = entidad.estado.ToString();
            multPrincipal.ActiveViewIndex = 1;

        }

    }
    
    protected void ddlMotivo_SelectedIndexChanged(object sender, EventArgs e)
    {
        String codCuenta = Session["numCuenta"].ToString();
        var resultado = objahorroLibretas.consultarServices(Convert.ToInt64(ddlMotivo.SelectedValue), codCuenta,(Usuario)Session["usuario"]);
        if (resultado != -1)
            txtValorLibreta.Text = resultado.ToString();             
    }

    Int64 calcularHasta(string numeroCuenta, String txtDesde) 
    {
        //calcula desprendible
        Int64 res = objahorroLibretas.getNumeroDesprendibleBusines((Usuario)Session["usuario"], numeroCuenta);
        return res + Convert.ToInt64(txtDesde);
    }

    // para guardar libreta nuevo
    ELibretas getDatosEntidaLibreta()
    {
        ELibretas entidadcargar = new ELibretas();
        entidadcargar.fecha_asignacion = Convert.ToDateTime(txtfechaAsig2.Text);
        entidadcargar.valor_libreta = Convert.ToDecimal(txtValorLibreta.Text);
        entidadcargar.numero_cuenta = txtNunCuentaG.Text;
        entidadcargar.numero_libreta =Convert.ToInt64(txtNumeLibreta1.Text);
        entidadcargar.desde =Convert.ToInt64(txtDesprendible2.Text);
        entidadcargar.hasta =  Convert.ToInt64(txtaDes.Text );
        entidadcargar.estado = 0;
        entidadcargar.id_Libreta = (Int64)Session["idLibreta"];// cambia el estado de la libreta anterior para ingresar la libreta nueva
        // en caso de que ya tenga libretsas asignadas

        return entidadcargar;
    }

    public void btnGuardar_Click(object sender, EventArgs e)
    {
        idObjeto = txtNumeroCuenta.Text;
        try
        {
            if (idObjeto != "")
            {
                if (Validar())
                {
                    objahorroLibretas.InsertarLibretaServices((Usuario)Session["usuario"], getDatosEntidaLibreta(), Convert.ToInt64(ddlMotivo.SelectedValue));
                }
            }
            else
            {
                if (Validar())
                {
                    objahorroLibretas.updateLibretaServices((Usuario)Session["usuario"], getDatosEntidaLibreta(), Convert.ToInt64(ddlMotivo.SelectedValue));
                }
            }
        }


        catch (Exception ex)
        {
            BOexcepcion.Throw(objahorroLibretas.GetType().Name + "L", "btnGuardar_Click", ex);
        }

        
        lblmsj.Text = "Creada";
        
        multPrincipal.ActiveViewIndex = 2;

        Site toolBar = (Site)this.Master;
        toolBar.MostrarConsultar(false);
        toolBar.MostrarLimpiar(false);
        toolBar.MostrarGuardar(false);
        toolBar.MostrarCancelar(false);
    }

    bool Validar()
    {
        if (txtfechaAsig2.Text.Trim() == "")
        {
            VerError("Complete el campo fecha");
            return false ;
        }
        if (txtValorLibreta.Text.Trim() == "")
        {
            VerError("Completar campo valor Libreta");
            return false ;
        }

        if (txtNunCuentaG.Text.Trim() == "")
        {
            VerError("Completar campo Numero de Cuenta");
            return false ;
        }
        if (txtNumeLibreta1.Text.Trim() == "")
        {
            VerError("Completar el campo Numero Libreta");
            return false ;
        }

        if (txtDesprendible2.Text.Trim() == "")
        {
            VerError("Completar campo Desprendible");
            return false;
        }

        if (txtaDes.Text.Trim() == "")
        {
            VerError("Completar campo valor Libreta");
            return false;
        }
        if (ddlMotivo.SelectedIndex < 0)
        {
            VerError("Seleccione un Motivo");
            return false;
        }


        return true;
    }


    protected void txtDesprendible2_TextChanged(object sender, EventArgs e)
    {
        txtaDes.Text = calcularHasta((string)Session["numCuenta"], txtDesprendible2.Text).ToString();        
    }

    protected List<Xpinn.Comun.Entities.ListasFijas> ListaEstadosLibreta()
    {
        GlobalWeb glob = new GlobalWeb();
        return glob.ListaEstadosLibreta();
    }
}

