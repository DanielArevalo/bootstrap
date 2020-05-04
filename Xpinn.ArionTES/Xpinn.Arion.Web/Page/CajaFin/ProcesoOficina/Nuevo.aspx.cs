using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using System.Data;
using System.Data.Common;

public partial class Nuevo : GlobalWeb
{
    Xpinn.Caja.Data.OficinaData oficinaData = new Xpinn.Caja.Data.OficinaData();
    Xpinn.Caja.Services.OficinaService oficinaService = new Xpinn.Caja.Services.OficinaService();
    Xpinn.Caja.Entities.Oficina oficina = new Xpinn.Caja.Entities.Oficina();
    Xpinn.Caja.Services.CajeroService cajeroService = new Xpinn.Caja.Services.CajeroService();
    Xpinn.Caja.Entities.Cajero cajero = new Xpinn.Caja.Entities.Cajero();
    Xpinn.Caja.Services.CajaService cajaServicio = new Xpinn.Caja.Services.CajaService();
    Xpinn.Caja.Entities.Caja caja = new Xpinn.Caja.Entities.Caja();
    Xpinn.Caja.Entities.ProcesoOficina procesoOficina = new Xpinn.Caja.Entities.ProcesoOficina();
    Xpinn.Caja.Services.ProcesoOficinaService procesoOficinaService = new Xpinn.Caja.Services.ProcesoOficinaService();
    Usuario user = new Usuario();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            VisualizarOpciones(procesoOficinaService.CodigoPrograma, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;

            if (Session[oficinaService.CodigoOficina + ".id"] != null)
                toolBar.eventoCancelar += btnCancelar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(oficinaService.GetType().Name + "A", "Page_PreInit", ex);
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                //se inicializa el combo de ciudades, centro de costos
                LlenarComboOficinas(ddlOficinas);

                cajero = cajeroService.ConsultarIfUserIsntCajeroPrincipal(user.codusuario, (Usuario)Session["usuario"]);
                Session["estadoCaj"] = cajero.estado;
                Session["codCaj"] = cajero.cod_cajero;

                if (long.Parse(Session["estadoCaj"].ToString()) == 0)
                    VerError("El Cajero se encuentra inactivo y no puede realizar Operación");

                user = (Usuario)Session["usuario"];
                ObtenerDatos(user.cod_oficina);

            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(oficinaService.GetType().Name + "A", "Page_Load", ex);
        }
    }

    protected void LlenarComboOficinas(DropDownList ddlOficinas)
    {

        Xpinn.Caja.Services.OficinaService oficinaService = new Xpinn.Caja.Services.OficinaService();
        Xpinn.Caja.Entities.Oficina oficina = new Xpinn.Caja.Entities.Oficina();
        ddlOficinas.DataSource = oficinaService.ListarOficina(oficina, (Usuario)Session["usuario"]);
        ddlOficinas.DataTextField = "nombre";
        ddlOficinas.DataValueField = "cod_oficina";
        ddlOficinas.DataBind();
    }

    protected void ddlOficinas_SelectedIndexChanged(object sender, EventArgs e)
    {
        int OficinaId = int.Parse(ddlOficinas.SelectedValue);
        ObtenerDatos(OficinaId);
    }

    protected void consultarusuarioAperturo()
    {
        procesoOficina.cod_oficina = Convert.ToInt64(ddlOficinas.SelectedValue);
        procesoOficina.fecha_proceso = Convert.ToDateTime(txtFechaProceso.Text);
        procesoOficina = procesoOficinaService.ConsultarUsuarioAperturo(procesoOficina, (Usuario)Session["usuario"]);
        txtAperturo.Visible = false;
        lblAperturo.Visible = false;
    }

    protected void ObtenerDatos(Int64 OficinaId)
    {

        oficina = oficinaService.ConsultarXOficina(OficinaId, (Usuario)Session["usuario"]);

        Usuario codusu = (Usuario)Session["usuario"];
        procesoOficina.cod_usuario = codusu.codusuario;

        Session["codusuario"] = procesoOficina.cod_usuario;
        procesoOficina.fecha_proceso = oficina.fechaproceso;// fecha maxima que puede ser la de apertura o cierre
        procesoOficina.cod_oficina = OficinaId;
        procesoOficina.tipo_proceso = oficina.tipo_proceso;

        txtEncargado.Text = oficina.nom_persona;
        txtFechaReal.Text = oficina.fecha_nuevo_proceso.ToString();
        txtFechaProceso.Text = procesoOficina.fecha_proceso.ToString("dd/MM/yyyy");

        int anio, dia, mes;
        DateTime dtFecha;
        anio = procesoOficina.fecha_proceso.Year;
        mes = procesoOficina.fecha_proceso.Month;
        dia = procesoOficina.fecha_proceso.Day;

        dtFecha = new DateTime(anio, mes, dia, 23, 59, 0);

        tbxFechaNuevoProceso.Text = dtFecha.ToString("dd/MM/yyyy");

        //sin son iguales entonces se muestra la fecha de apertura
        if (Convert.ToDateTime(tbxFechaNuevoProceso.Text) == Convert.ToDateTime(txtFechaProceso.Text))
        {
            tbxFechaNuevoProceso.Text = oficina.fecha_nuevo_proceso.ToString("dd/MM/yyyy");
        }

        Session["fechaproceso"] = procesoOficina.fecha_proceso;

        procesoOficina = procesoOficinaService.ConsultarXProcesoOficina(procesoOficina, (Usuario)Session["usuario"]);
        ddlTipoHorario.SelectedValue = procesoOficina.tipo_horario.ToString();

        // Cierre de Oficina   
        if (int.Parse(oficina.estado.ToString()) == 1)//caso de que la oficina este activa; oficina.estado es el estado actual de la oficina a aperturar no del procesoOficina 
        {
            txtEstadoAct.Text = "Activo";
            chkNuevoEstado.Text = "Inactivo";
        }
        else // Apertura de Oficina - caso de que la oficina este inactiva
        {
            txtEstadoAct.Text = "Inactivo";
            chkNuevoEstado.Text = "Activo";
        }

        ddlOficinas.SelectedValue = oficina.cod_oficina;
        // consultarusuarioAperturo();

        if (chkNuevoEstado.Text == "Activo")
        {
            // Mostrar las cajas de la oficina para poderlas seleccionar
            caja.cod_oficina = OficinaId;
            List<Xpinn.Caja.Entities.Caja> listcaja = new List<Xpinn.Caja.Entities.Caja>();
            listcaja = cajaServicio.ListarComboCajaXOficina(caja, codusu);
            gvLista.DataSource = listcaja;

            if (listcaja.Count > 0)
            {
                pDatos.Visible = true;
                gvLista.DataBind();
            }
           
        }

        if (chkNuevoEstado.Text == "Inactivo")
        {
            // Mostrar las cajas de la oficina para poderlas seleccionar
            caja.cod_oficina = OficinaId;
            List<Xpinn.Caja.Entities.Caja> listcaja = new List<Xpinn.Caja.Entities.Caja>();
            listcaja = cajaServicio.ListarComboCajaXOficinaActiva(caja, codusu);
            gvLista.DataSource = listcaja;

            if (listcaja.Count > 0)
            {
                pDatos.Visible = true;
                gvLista.DataBind();
            }

        }

    }


    protected void btnCancelar_Click(object sender, ImageClickEventArgs e)
    {
        Navegar("../../../General/Global/inicio.aspx");
    }

    private Boolean validarreglasGrabar()
    {
        String estado;
        Boolean result = true;
        Usuario codusu = (Usuario)Session["usuario"];
        procesoOficina.cod_usuario = codusu.codusuario;
        DateTime fechaactual;
        fechaactual = Convert.ToDateTime(DateTime.Now.ToShortDateString());

    //foreach (GridViewRow rFila in gvLista.Rows)
        //{
        //    estado = (rFila.Cells[4].Text);
        //    if (estado == "Activa")
        //    {
        //        result = false;
        //        VerError("No puede Activar/Inactivar la oficina hay cajas sin cerrar");
        //    }
        //}


        Int64 Usuarioaperturo = 0;
        procesoOficina.cod_oficina = Convert.ToInt64(ddlOficinas.SelectedValue);
        procesoOficina.fecha_proceso = Convert.ToDateTime(txtFechaProceso.Text);
        procesoOficina = procesoOficinaService.ConsultarUsuarioAperturo(procesoOficina, (Usuario)Session["usuario"]);
        Usuarioaperturo = procesoOficina.cod_usuario;
        return result;
    }

    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            List<Xpinn.Caja.Entities.Caja> lstConsulta = new List<Xpinn.Caja.Entities.Caja>();
            List<Xpinn.Caja.Entities.Caja> lista = new List<Xpinn.Caja.Entities.Caja>();
            Xpinn.Caja.Entities.Caja variable = new Xpinn.Caja.Entities.Caja();
            Xpinn.Caja.Entities.Caja caja = new Xpinn.Caja.Entities.Caja();

            // Determinar las cajas que pertenecen a la oficina
            lstConsulta = (from GridViewRow row in gvLista.Rows
                           where ((CheckBox)row.FindControl("chkSeleccionar")).Checked
                           select new Xpinn.Caja.Entities.Caja
                           {
                               cod_caja = row.Cells[1].Text,
                               nombre = row.Cells[2].Text,
                               fecha_creacion = Convert.ToDateTime(tbxFechaNuevoProceso.Text),
                               estado = row.Cells[4].Text == "Activa" ? 1 : row.Cells[4].Text == "Inactiva" ? 0 : 2
                           }).ToList();
            cajaServicio.ListarCaja(caja, (Usuario)Session["usuario"]);


            // Verificar que la caja se encuentra activa no dejar activar nuevamente hasta que este cerrada 

            if (validarreglasGrabar())
            {
                VerError("");
                // Realizar el cambio de estado
                if (long.Parse(Session["estadoCaj"].ToString()) == 1)
                {
                    //se atrapan los datos del formulario
                    procesoOficina.cod_oficina = long.Parse(ddlOficinas.SelectedValue);
                    procesoOficina.tipo_horario = long.Parse(ddlTipoHorarioNuevo.SelectedValue);
                    procesoOficina.cod_usuario = long.Parse(Session["codusuario"].ToString());
                    Usuario usuoficina = (Usuario)Session["usuario"];
                    int dias = 0;
                    if (procesoOficina.cod_oficina == usuoficina.cod_oficina)
                    {
                        // Cambiar la oficina a INACTIVA si se escogio ese nuevo estado
                        if (chkNuevoEstado.Checked && chkNuevoEstado.Text == "Inactivo")
                        {
                            procesoOficina.tipo_proceso = 2; //Cierre de oficina
                            if (ddlTipoHorarioNuevo.SelectedValue != ddlTipoHorario.SelectedValue)
                                procesoOficina.tipo_horario = long.Parse(ddlTipoHorario.SelectedValue);
                        }

                        // Cambiar la oficina a ACTIVA si se escogio ese nuevo estado
                        if (chkNuevoEstado.Checked && chkNuevoEstado.Text == "Activo")
                            procesoOficina.tipo_proceso = 1; //Apertura de oficina

                        if (chkNuevoEstado.Checked)
                        {
                            procesoOficina.fecha_proceso = Convert.ToDateTime(Session["fechaproceso"].ToString());

                            dias = (Convert.ToDateTime(txtFechaReal.Text) - procesoOficina.fecha_proceso.Date).Days;

                            if ((dias >= 1) && (txtEstadoAct.Text == "Activo"))
                            {
                                int anio, dia, mes;
                                DateTime dtFecha;
                                anio = procesoOficina.fecha_proceso.Year;
                                mes = procesoOficina.fecha_proceso.Month;
                                dia = procesoOficina.fecha_proceso.Day;

                                dtFecha = new DateTime(anio, mes, dia, 23, 59, 0);
                                procesoOficina.fecha_proceso = dtFecha;
                            }
                            else if ((dias >= 1) && (txtEstadoAct.Text == "Inactivo"))
                            {
                                procesoOficina.fecha_proceso = DateTime.Now;
                            }
                            else
                            {
                                procesoOficina.fecha_proceso = DateTime.Now;
                            }
                            for (int i = 0; i < lista.Count; i++)
                            {
                                variable = lista[i];
                                if (variable.estado == 2)
                                    procesoOficina.estado = variable.estado;
                            }
                            string pError = "";
                            procesoOficina = procesoOficinaService.CrearProcesoOficina(procesoOficina, ref pError, (Usuario)Session["usuario"]);
                            if (pError.Trim() == "")
                                Navegar("../../../General/Global/inicio.aspx");
                            else
                                VerError(pError);
                        }
                        else
                        {

                            if (lstConsulta.Count > 0)
                            {
                                int resultado = 0;
                                resultado = procesoOficinaService.CrearCajasAbrir(lstConsulta, Session["codCaj"].ToString(), usuoficina);
                                if (resultado == 1)
                                {
                                    Navegar("Nuevo.aspx");
                                }
                                else
                                {
                                    VerError("No se pudo insertar las cajas seleccionadas");
                                    return;
                                }
                            }

                            else
                            {
                                VerError("Se debe seleccionar al menos una caja");
                                return;
                            }
                        }

                    }
                    else
                        VerError("El Usuario solo puede cambiar la apertura de la oficina a la cual esta asociado");
                }
                else
                    VerError("El Cajero se encuentra inactivo y no puede realizar Operación");
            }

        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(oficinaService.GetType().Name + "A", "btnGuardar_Click", ex);
        }
    }



    protected void gvLista_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        int validate = 0;
        if (int.TryParse(e.Row.Cells[4].Text, out validate))
        {
            if (int.Parse(e.Row.Cells[4].Text) > 0)
            {
                e.Row.Cells[4].Text = "Activa";
            }
            else
            {
                e.Row.Cells[4].Text = "Inactiva";
            }
        }
    }

}

