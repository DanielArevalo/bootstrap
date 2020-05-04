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

partial class Nuevo : GlobalWeb
{
    private Xpinn.Tesoreria.Services.TipoListaRecaudoService tipoListaServicio = new Xpinn.Tesoreria.Services.TipoListaRecaudoService();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[tipoListaServicio.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(tipoListaServicio.CodigoPrograma, "E");
            else
                VisualizarOpciones(tipoListaServicio.CodigoPrograma, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
            ctlMensaje.eventoClick += btnContinuarMen_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(tipoListaServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Session[tipoListaServicio.CodigoPrograma + ".id"] != null)
                {
                    Session["TEXTO"] = "modificar";
                    idObjeto = Session[tipoListaServicio.CodigoPrograma + ".id"].ToString();
                    ObtenerDatos(idObjeto);
                }
                else
                {
                    Session["TEXTO"] = "grabar";
                    InicializargvProgramacion();
                }
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(tipoListaServicio.CodigoPrograma, "Page_Load", ex);
        }
    }

    public Boolean ValidarDatos()
    {
        return true;
    }

    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        if (ValidarDatos())
        {
            ctlMensaje.MostrarMensaje("Esta Seguro de " + Session["TEXTO"].ToString() + " los Datos Ingresados?");
        }
    }

    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        try
        {
            Xpinn.Tesoreria.Entities.TipoListaRecaudo vTipoComp = new Xpinn.Tesoreria.Entities.TipoListaRecaudo();

            if (idObjeto != "")
                vTipoComp = tipoListaServicio.ConsultarTipoListaRecaudo(Convert.ToInt64(idObjeto), (Usuario)Session["usuario"]);

            vTipoComp.idtipo_lista = Convert.ToInt32(txtTipoLista.Text.Trim());
            vTipoComp.descripcion = Convert.ToString(txtDescripcion.Text.Trim());
            if (vTipoComp.lstDetalle != null)
                vTipoComp.lstDetalle = new List<TipoListaRecaudoDetalle>();
            vTipoComp.lstDetalle = ObtenerListaProgramacion();

            if (idObjeto != "")
            {
                vTipoComp.idtipo_lista = Convert.ToInt32(idObjeto);
                tipoListaServicio.ModificarTipoListaRecaudo(vTipoComp, (Usuario)Session["usuario"]);
            }
            else
            {
                vTipoComp = tipoListaServicio.CrearTipoListaRecaudo(vTipoComp, (Usuario)Session["usuario"]);
                idObjeto = vTipoComp.idtipo_lista.ToString();
            }            
            Session[tipoListaServicio.CodigoPrograma + ".id"] = idObjeto;
            Navegar(Pagina.Lista);
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(tipoListaServicio.CodigoPrograma, "btnGuardar_Click", ex);
        }
    }

    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }

    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            Xpinn.Tesoreria.Entities.TipoListaRecaudo vTipoComp = new Xpinn.Tesoreria.Entities.TipoListaRecaudo();
            vTipoComp = tipoListaServicio.ConsultarTipoListaRecaudo(Convert.ToInt32(pIdObjeto), (Usuario)Session["usuario"]);

            if (!string.IsNullOrEmpty(vTipoComp.idtipo_lista.ToString()))
                txtTipoLista.Text = HttpUtility.HtmlDecode(vTipoComp.idtipo_lista.ToString().Trim());
            if (!string.IsNullOrEmpty(vTipoComp.descripcion))
                txtDescripcion.Text = HttpUtility.HtmlDecode(vTipoComp.descripcion.ToString().Trim());
            if (vTipoComp.lstDetalle != null)
            {
                gvProgramacion.DataSource = vTipoComp.lstDetalle;
                gvProgramacion.DataBind();
                Session["Programacion"] = vTipoComp.lstDetalle;
            }
            InicializargvProgramacion();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(tipoListaServicio.CodigoPrograma, "ObtenerDatos", ex);
        }
    }

    protected void btnFinal_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }

    protected void InicializargvProgramacion()
    {
        List<TipoListaRecaudoDetalle> lstProgra = new List<TipoListaRecaudoDetalle>();        
        lstProgra = ObtenerListaProgramacion();
        for (int i = gvProgramacion.Rows.Count; i < 5; i++)
        {
            TipoListaRecaudoDetalle eCuenta = new TipoListaRecaudoDetalle();
            eCuenta.codtipo_lista_detalle = -1;
            eCuenta.idtipo_lista = null;
            eCuenta.tipo_producto = null;
            eCuenta.cod_linea = null;

            lstProgra.Add(eCuenta);
        }
        gvProgramacion.DataSource = lstProgra;
        gvProgramacion.DataBind();

        Session["Programacion"] = lstProgra;
    }

    protected void gvProgramacion_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownListGrid ddlTipo = (DropDownListGrid)e.Row.FindControl("ddlTipo");
            if (ddlTipo != null)
            {
                ddlTipo.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));
                ddlTipo.Items.Insert(1, new ListItem("APORTES", "1"));
                ddlTipo.Items.Insert(2, new ListItem("CRÉDITOS", "2"));
                ddlTipo.Items.Insert(3, new ListItem("DEPÓSITOS", "3"));
                ddlTipo.Items.Insert(4, new ListItem("AFILIACION", "6"));
                ddlTipo.Items.Insert(5, new ListItem("SERVICIOS", "4"));
                ddlTipo.Items.Insert(6, new ListItem("CDAT", "5"));
                ddlTipo.Items.Insert(7, new ListItem("AHORRO PROGRAMADO", "9"));
                ddlTipo.Items.Insert(8, new ListItem("CRÉDITOS-CUOTAS EXTRAS", "10"));
                ddlTipo.SelectedIndex = 0;
                ddlTipo.DataBind();
            }
            Label lbltipo = (Label)e.Row.FindControl("lbltipo");
            if (lbltipo != null)
            {
                ddlTipo.SelectedValue = lbltipo.Text;                                
                DropDownListGrid ddlCodLinea = (DropDownListGrid)e.Row.FindControl("ddlCodLinea");
                if (ddlCodLinea != null)
                {
                    CargarLinea(ddlTipo.SelectedValue, ddlCodLinea); 
                    Label lblCodLinea = (Label)e.Row.FindControl("lblCodLinea");
                    if (lblCodLinea != null)
                        try
                        {
                            ddlCodLinea.SelectedValue = lblCodLinea.Text;
                        }
                        catch
                        { }
                }
            }
        }
    }

    protected void gvProgramacion_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int conseID = Convert.ToInt32(gvProgramacion.DataKeys[e.RowIndex].Values[0].ToString());

        ObtenerListaProgramacion();

        List<TipoListaRecaudoDetalle> LstDetalle = new List<TipoListaRecaudoDetalle>();
        LstDetalle = (List<TipoListaRecaudoDetalle>)Session["Programacion"];
        if (conseID > 0)
        {
            try
            {                
                foreach (TipoListaRecaudoDetalle acti in LstDetalle)
                {
                    if (acti.codtipo_lista_detalle == conseID)
                    {
                        tipoListaServicio.EliminarTipoListaRecaudoDetalle(conseID, (Usuario)Session["usuario"]);
                        LstDetalle.Remove(acti);
                        break;
                    }
                }
                Session["Programacion"] = LstDetalle;

                gvProgramacion.DataSourceID = null;
                gvProgramacion.DataBind();
                gvProgramacion.DataSource = LstDetalle;
                gvProgramacion.DataBind();

            }
            catch (Xpinn.Util.ExceptionBusiness ex)
            {
                VerError(ex.Message);
            }
            catch (Exception ex)
            {
                BOexcepcion.Throw(tipoListaServicio.CodigoPrograma, "gvProgramacion_RowDeleting", ex);
            }
        }
        else
        {
            foreach (TipoListaRecaudoDetalle acti in LstDetalle)
            {
                if (acti.codtipo_lista_detalle == conseID)
                {
                    LstDetalle.Remove(acti);
                    break;
                }
            }
            Session["Programacion"] = LstDetalle;

            gvProgramacion.DataSourceID = null;
            gvProgramacion.DataBind();
            gvProgramacion.DataSource = LstDetalle;
            gvProgramacion.DataBind();
        }
    }


    protected void gvProgramacion_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void btnDetalle_Click(object sender, EventArgs e)
    {
        ObtenerListaProgramacion();
        List<TipoListaRecaudoDetalle> LstPrograma = new List<TipoListaRecaudoDetalle>();
        if (Session["Programacion"] != null)
        {
            LstPrograma = (List<TipoListaRecaudoDetalle>)Session["Programacion"];

            for (int i = 1; i <= 1; i++)
            {
                TipoListaRecaudoDetalle pDetalle = new TipoListaRecaudoDetalle();
                pDetalle.codtipo_lista_detalle = -1;
                pDetalle.cod_linea = null;
                pDetalle.tipo_producto = null;
                LstPrograma.Add(pDetalle);
            }
            gvProgramacion.PageIndex = gvProgramacion.PageCount;
            gvProgramacion.DataSource = LstPrograma;
            gvProgramacion.DataBind();

            Session["Programacion"] = LstPrograma;
        }

    }

    protected List<TipoListaRecaudoDetalle> ObtenerListaProgramacion()
    {
        try
        {
            List<TipoListaRecaudoDetalle> lstProgramacion = new List<TipoListaRecaudoDetalle>();
            //lista para adicionar filas sin perder datos
            List<TipoListaRecaudoDetalle> lista = new List<TipoListaRecaudoDetalle>();

            foreach (GridViewRow rfila in gvProgramacion.Rows)
            {
                TipoListaRecaudoDetalle ePogra = new TipoListaRecaudoDetalle();

                Label lblcodtipo_lista_detalle = (Label)rfila.FindControl("lblcodtipo_lista_detalle");
                if (lblcodtipo_lista_detalle != null)
                    ePogra.codtipo_lista_detalle = Convert.ToInt64(lblcodtipo_lista_detalle.Text);
                else
                    ePogra.codtipo_lista_detalle = -1;

                DropDownListGrid ddlTipo = (DropDownListGrid)rfila.FindControl("ddlTipo");
                if (ddlTipo.SelectedValue != null)
                    if (ddlTipo.SelectedValue != "")
                        ePogra.tipo_producto = Convert.ToInt32(ddlTipo.SelectedValue);

                DropDownListGrid ddlCodLinea = (DropDownListGrid)rfila.FindControl("ddlCodLinea");
                if (ddlCodLinea.SelectedValue != null)
                    if (ddlCodLinea.SelectedValue != "")
                        ePogra.cod_linea = ddlCodLinea.SelectedValue;

                lista.Add(ePogra);                
                if (ePogra.codtipo_lista_detalle != null && ePogra.tipo_producto != null)
                {
                    lstProgramacion.Add(ePogra);
                }
            }
            Session["Programacion"] = lista;
            return lstProgramacion;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(tipoListaServicio.CodigoPrograma, "ObtenerListaProgramacion", ex);
            return null;
        }
    }

    protected void ddlTipo_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownListGrid ddlTipo = (DropDownListGrid)sender;
        int nItem = Convert.ToInt32(ddlTipo.CommandArgument);

        DropDownListGrid ddlCodLinea = (DropDownListGrid)gvProgramacion.Rows[nItem].FindControl("ddlCodLinea");

        if (ddlCodLinea != null)
        {
            CargarLinea(ddlTipo.SelectedValue, ddlCodLinea);
        }
    }

    public void CargarLinea(string tipo, DropDownList ddlCodLinea)
    {
        if (tipo == "1")
            PoblarLista("lineaaporte", ddlCodLinea);
        else if (tipo == "2")
            PoblarLista("lineascredito", ddlCodLinea);
        else if (tipo == "3")
            PoblarLista("lineaahorro", ddlCodLinea);
        else if (tipo == "0")
        {
            ddlCodLinea.DataSource = null;
            ddlCodLinea.DataBind();
        }
        else if (tipo == "4")
        {
            PoblarLista("LINEASSERVICIOS", ddlCodLinea);
        }
        else if (tipo == "5")
        {
            PoblarLista("lineacdat", ddlCodLinea);
        }
        else if (tipo == "9")
        {
            PoblarLista("lineaprogramado", ddlCodLinea);
        }
        else if (tipo == "10")
            PoblarLista("lineascredito", ddlCodLinea);
        else
        {
            ddlCodLinea.DataSource = null;
            ddlCodLinea.DataBind();
        }
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

    /// <summary>
    /// Generar automáticamente la lista de todos los productos y lineas
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void chkConsolidada_CheckedChanged(object sender, EventArgs e)
    {
        if (chkConsolidada.Checked)
            ListaConsolidada();
        else
        {
            gvProgramacion.DataSource = null;
            gvProgramacion.DataBind();
        }

    }

    /// <summary>
    /// Generar una lista que incluya todos los tipos de productos con sus respectivas lineas
    /// </summary>
    public void ListaConsolidada()
    {
        List<TipoListaRecaudoDetalle> lstProgramacion = new List<TipoListaRecaudoDetalle>();

        Xpinn.FabricaCreditos.Services.LineasCreditoService linCredServicio = new Xpinn.FabricaCreditos.Services.LineasCreditoService();
        List<Xpinn.FabricaCreditos.Entities.LineasCredito> lstLinCredito = new List<Xpinn.FabricaCreditos.Entities.LineasCredito>();
        lstLinCredito = linCredServicio.ListarLineasCredito((Usuario)Session["usuario"]);

        for (int i = 0; i <= 9; i++)
        {
            if (i == 0)
            {
                Xpinn.Aportes.Services.LineaAporteServices linAporServ = new Xpinn.Aportes.Services.LineaAporteServices();
                List<Xpinn.Aportes.Entities.LineaAporte> lstLinAporte = new List<Xpinn.Aportes.Entities.LineaAporte>();
                Xpinn.Aportes.Entities.LineaAporte lineaAporte = new Xpinn.Aportes.Entities.LineaAporte();

                lstLinAporte = linAporServ.ListarLineaAporte(lineaAporte, (Usuario)Session["usuario"]);
                lstProgramacion.AddRange(from item in lstLinAporte
                                         select new TipoListaRecaudoDetalle
                                         {
                                             codtipo_lista_detalle = -1,
                                             tipo_producto = 1,
                                             cod_linea = item.cod_linea_aporte.ToString()
                                         });
            }
            else if (i == 1)
            {
                lstProgramacion.AddRange(from item in lstLinCredito
                                         select new TipoListaRecaudoDetalle
                                         {
                                             codtipo_lista_detalle = -1,
                                             tipo_producto = 2,
                                             cod_linea = item.cod_linea_credito
                                         });
            }
            else if (i == 2)
            {
                Xpinn.Ahorros.Services.LineaAhorroServices linAhorroServ = new Xpinn.Ahorros.Services.LineaAhorroServices();
                List<Xpinn.Ahorros.Entities.LineaAhorro> lstLinAhorro = new List<Xpinn.Ahorros.Entities.LineaAhorro>();
                Xpinn.Ahorros.Entities.LineaAhorro lineaAhorro = new Xpinn.Ahorros.Entities.LineaAhorro();

                lstLinAhorro = linAhorroServ.ListarLineaAhorro(lineaAhorro, (Usuario)Session["usuario"]);
                lstProgramacion.AddRange(from item in lstLinAhorro
                                         select new TipoListaRecaudoDetalle
                                         {
                                             codtipo_lista_detalle = -1,
                                             tipo_producto = 3,
                                             cod_linea = item.cod_linea_ahorro
                                         });
            }
            else if (i == 3)
            {
                lstProgramacion.Add(new TipoListaRecaudoDetalle
                {
                    codtipo_lista_detalle = -1,
                    tipo_producto = 6,
                    cod_linea = ""
                });
            }
            else if (i == 4)
            {
                Xpinn.Servicios.Services.LineaServiciosServices linServicio = new Xpinn.Servicios.Services.LineaServiciosServices();
                List<Xpinn.Servicios.Entities.LineaServicios> lisLinServicio = new List<Xpinn.Servicios.Entities.LineaServicios>();
                Xpinn.Servicios.Entities.LineaServicios lineaServicio = new Xpinn.Servicios.Entities.LineaServicios();

                lisLinServicio = linServicio.ListarLineaServicios(lineaServicio, (Usuario)Session["usuario"], "");
                lstProgramacion.AddRange(from item in lisLinServicio
                                         select new TipoListaRecaudoDetalle
                                         {
                                             codtipo_lista_detalle = -1,
                                             tipo_producto = 4,
                                             cod_linea = item.cod_linea_servicio
                                         });
            }
            else if (i == 5)
            {
                Xpinn.CDATS.Services.LineaCDATService linCDATServicio = new Xpinn.CDATS.Services.LineaCDATService();
                List<Xpinn.CDATS.Entities.LineaCDAT> listLinCDAT = new List<Xpinn.CDATS.Entities.LineaCDAT>();
                Xpinn.CDATS.Entities.LineaCDAT lineaCDAT = new Xpinn.CDATS.Entities.LineaCDAT();

                listLinCDAT = linCDATServicio.ListarLineaCDAT(lineaCDAT, (Usuario)Session["usuario"]);
                lstProgramacion.AddRange(from item in listLinCDAT
                                         select new TipoListaRecaudoDetalle
                                         {
                                             codtipo_lista_detalle = -1,
                                             tipo_producto = 5,
                                             cod_linea = item.cod_lineacdat
                                         });
            }
            else if (i == 7)
            {
                Xpinn.Programado.Services.LineasProgramadoServices linProgServicio = new Xpinn.Programado.Services.LineasProgramadoServices();
                List<Xpinn.Programado.Entities.LineasProgramado> lstLineaProg = new List<Xpinn.Programado.Entities.LineasProgramado>();
                Xpinn.Programado.Entities.LineasProgramado lineaProgramado = new Xpinn.Programado.Entities.LineasProgramado();

                lstLineaProg = linProgServicio.ListarLineasProgramado("", (Usuario)Session["usuario"]);
                lstProgramacion.AddRange(from item in lstLineaProg
                                         select new TipoListaRecaudoDetalle
                                         {
                                             codtipo_lista_detalle = -1,
                                             tipo_producto = 9,
                                             cod_linea = item.cod_linea_programado
                                         });
            }
            else if (i == 8)
            {
                lstProgramacion.AddRange(from item in lstLinCredito
                                         select new TipoListaRecaudoDetalle
                                         {
                                             codtipo_lista_detalle = -1,
                                             tipo_producto = 10,
                                             cod_linea = item.cod_linea_credito
                                         });
            }
            if(lstProgramacion.Count > 0)
            {
                gvProgramacion.DataSource = lstProgramacion;
                gvProgramacion.DataBind();
            }
        }
    }


    
}