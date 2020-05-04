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

partial class Lista : GlobalWeb
{
    private Xpinn.FabricaCreditos.Services.SolicitudCreditosRecogidosService SolicitudCreditosRecogidosServicio = new Xpinn.FabricaCreditos.Services.SolicitudCreditosRecogidosService();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(SolicitudCreditosRecogidosServicio.CodigoPrograma, "L");

            Site1 toolBar = (Site1)this.Master;
            toolBar.eventoAdelante -= btnAdelante_Click;
            toolBar.eventoAtras -= btnAtras_Click;
         
            if (Session["Nombres"] != null) 
                ((Label)Master.FindControl("lblNombresApellidos")).Text = Session["Nombres"].ToString();
            if (Session["Identificacion"] != null)
                ((Label)Master.FindControl("lblIdCliente")).Text = Session["Identificacion"].ToString();
            if (Session["Identificacion"] != null)
                txtIdentificacion.Text = Session["Identificacion"].ToString();
            txtFecha.Text = System.DateTime.Now.ToShortDateString();

            ImageButton btnAdelante = Master.FindControl("btnAdelante") as ImageButton;

            if (Session["TipoCredito"] != null)
            {
                if (Session["TipoCredito"].ToString() == "C")
                    btnAdelante.ImageUrl = "~/Images/btnGeoreferenciacion.jpg";
                else
                    btnAdelante.ImageUrl = "~/Images/btnPlanPagos.jpg";
            }

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(SolicitudCreditosRecogidosServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                CargarValoresConsulta(pConsulta, SolicitudCreditosRecogidosServicio.CodigoPrograma);
                if (Session[SolicitudCreditosRecogidosServicio.CodigoPrograma + ".consulta"] != null)
                    Actualizar();
                if (! string.Equals(txtIdentificacion.Text, ""))
                    MostrarTablaTemporal();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(SolicitudCreditosRecogidosServicio.CodigoPrograma, "Page_Load", ex);
        }
    }

    protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    {
        GuardarValoresConsulta(pConsulta, SolicitudCreditosRecogidosServicio.CodigoPrograma);
        Navegar(Pagina.Nuevo);
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        GuardarValoresConsulta(pConsulta, SolicitudCreditosRecogidosServicio.CodigoPrograma);
        Actualizar();
    }

    protected void gvLista_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            ConfirmarEliminarFila(e, "btnBorrar");
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(SolicitudCreditosRecogidosServicio.CodigoPrograma + "L", "gvLista_RowDataBound", ex);
        }
    }

    /// <summary>
    /// Evento para controlar cuando se selecciona un item de la lista de créditos recogidos
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        String id = gvLista.DataKeys[gvLista.SelectedRow.RowIndex].Value.ToString();
        Session[SolicitudCreditosRecogidosServicio.CodigoPrograma + ".id"] = id;
        Navegar(Pagina.Detalle);
    }

    /// <summary>
    /// Evento para cuando se selecciona un item de la lista de créditos para edición
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvLista.Rows[e.NewEditIndex].Cells[0].Text;
        Session[SolicitudCreditosRecogidosServicio.CodigoPrograma + ".id"] = id;
        Navegar(Pagina.Editar);
    }

    /// <summary>
    /// Evento para cuando se selecciona un item de la lista de créditos para borrado
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            Int64 id = Convert.ToInt64(e.Keys[0]);
            SolicitudCreditosRecogidosServicio.EliminarSolicitudCreditosRecogidos(id, (Usuario)Session["usuario"]);
            Actualizar();
        }
        catch (Xpinn.Util.ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(SolicitudCreditosRecogidosServicio.CodigoPrograma, "gvLista_RowDeleting", ex);
        }
    }

    /// <summary>
    /// Cuando la lista de créditos a recoger es muy grande controla la paginación.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvLista.PageIndex = e.NewPageIndex;
            Actualizar();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(SolicitudCreditosRecogidosServicio.CodigoPrograma, "gvLista_PageIndexChanging", ex);
        }
    }

    /// <summary>
    /// Este evento es para controlar comandos de la grilla
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvLista_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string a = Convert.ToString(e.ToString());
    }

    /// <summary>
    /// Esta función es para actualizar la grilla cuando se cambia de página
    /// </summary>
    private void Actualizar()
    {
        try
        {
            List<Xpinn.FabricaCreditos.Entities.SolicitudCreditosRecogidos> lstConsulta = new List<Xpinn.FabricaCreditos.Entities.SolicitudCreditosRecogidos>();
            lstConsulta = SolicitudCreditosRecogidosServicio.ListarSolicitudCreditosRecogidos(ObtenerValores(), (Usuario)Session["usuario"]);

            gvLista.PageSize = pageSize;
            gvLista.EmptyDataText = emptyQuery;
            gvLista.DataSource = lstConsulta;

            if (lstConsulta.Count > 0)
            {
                gvLista.Visible = true;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                gvLista.DataBind();
                ValidarPermisosGrilla(gvLista);
            }
            else
            {
                gvLista.Visible = false;
                lblTotalRegs.Visible = false;
            }

            Session.Add(SolicitudCreditosRecogidosServicio.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(SolicitudCreditosRecogidosServicio.CodigoPrograma, "Actualizar", ex);
        }
    }

    /// <summary>
    ///  Esto es para obtener los datos de los filtros y cargarlos a la entidad y hacer la consulta
    /// </summary>
    /// <returns></returns>
    private Xpinn.FabricaCreditos.Entities.SolicitudCreditosRecogidos ObtenerValores()
    {
        Xpinn.FabricaCreditos.Entities.SolicitudCreditosRecogidos vSolicitudCreditosRecogidos = new Xpinn.FabricaCreditos.Entities.SolicitudCreditosRecogidos();

        if (txtNumerosolicitud.Text.Trim() != "")
            vSolicitudCreditosRecogidos.numerosolicitud = Convert.ToInt64(txtNumerosolicitud.Text.Trim());
        if (txtNumero_recoge.Text.Trim() != "")
            vSolicitudCreditosRecogidos.numero_recoge = Convert.ToInt64(txtNumero_recoge.Text.Trim());
        if (txtFecharecoge.Text.Trim() != "")
            vSolicitudCreditosRecogidos.fecharecoge = Convert.ToDateTime(txtFecharecoge.Text.Trim());
        if (txtValorrecoge.Text.Trim() != "")
            vSolicitudCreditosRecogidos.valorrecoge = Convert.ToInt64(txtValorrecoge.Text.Trim());
        if (txtFechapago.Text.Trim() != "")
            vSolicitudCreditosRecogidos.fechapago = Convert.ToDateTime(txtFechapago.Text.Trim());
        if (txtSaldocapital.Text.Trim() != "")
            vSolicitudCreditosRecogidos.saldocapital = Convert.ToInt64(txtSaldocapital.Text.Trim());
        if (txtSaldointcorr.Text.Trim() != "")
            vSolicitudCreditosRecogidos.saldointcorr = Convert.ToInt64(txtSaldointcorr.Text.Trim());
        if (txtSaldointmora.Text.Trim() != "")
            vSolicitudCreditosRecogidos.saldointmora = Convert.ToInt64(txtSaldointmora.Text.Trim());
        if (txtSaldomipyme.Text.Trim() != "")
            vSolicitudCreditosRecogidos.saldomipyme = Convert.ToInt64(txtSaldomipyme.Text.Trim());
        if (txtSaldoivamipyme.Text.Trim() != "")
            vSolicitudCreditosRecogidos.saldoivamipyme = Convert.ToInt64(txtSaldoivamipyme.Text.Trim());
        if (txtSaldootros.Text.Trim() != "")
            vSolicitudCreditosRecogidos.saldootros = Convert.ToInt64(txtSaldootros.Text.Trim());

        return vSolicitudCreditosRecogidos;
    }


    /// <summary>
    /// Llenar la tabla de créditos recogidos con los créditos que posee actualmente la persona
    /// </summary>
    private void MostrarTablaTemporal()
    {

        try
        {
            Xpinn.FabricaCreditos.Entities.SolicitudCreditosRecogidos vSolicitudCreditosRecogidos = new Xpinn.FabricaCreditos.Entities.SolicitudCreditosRecogidos();
            vSolicitudCreditosRecogidos.identificacion = Convert.ToString(txtIdentificacion.Text.Trim());
            vSolicitudCreditosRecogidos.fecha_pago = Convert.ToDateTime(txtFecha.Text.Trim());

            SolicitudCreditosRecogidosServicio.ParametrosSolicredRecoger(vSolicitudCreditosRecogidos, (Usuario)Session["usuario"]);

        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(SolicitudCreditosRecogidosServicio.CodigoPrograma, "btnGuardar_Click", ex);
        }

        try
        {
            List<Xpinn.FabricaCreditos.Entities.SolicitudCreditosRecogidos> lstConsulta = new List<Xpinn.FabricaCreditos.Entities.SolicitudCreditosRecogidos>();
            lstConsulta = SolicitudCreditosRecogidosServicio.ListarTemp_recoger(ObtenerValores(), (Usuario)Session["usuario"]);

            gvLista.PageSize = pageSize;
            gvLista.EmptyDataText = emptyQuery;
            gvLista.DataSource = lstConsulta;

            if (lstConsulta.Count > 0)
            {
                gvLista.Visible = true;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                gvLista.DataBind();
                ValidarPermisosGrilla(gvLista);
            }
            else
            {
                gvLista.Visible = false;
                lblTotalRegs.Visible = false;
                mvCreditosRecogidos.ActiveViewIndex = 1;
                lblMensaje.Text = "No tiene créditos par recoger/refinanciar";
            }

            Session.Add(SolicitudCreditosRecogidosServicio.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(SolicitudCreditosRecogidosServicio.CodigoPrograma, "Actualizar", ex);
        }

        ViewState["ValorTotalRecoger"] = 0;

    }


    /// <summary>
    /// Este evento es para actualizar el valor total de créditos recogidos cuando se marca o desmarca un crédito.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Check_Clicked(object sender, EventArgs e)
    {
        CheckBox chkRecoger = (CheckBox)sender;
        GridViewRow row = (GridViewRow)chkRecoger.NamingContainer;

        long ValorFila = Convert.ToInt64(gvLista.Rows[row.RowIndex].Cells[13].Text);
        long Total = 0;

        if (chkRecoger.Checked == true)
            Total = Convert.ToInt64(ViewState["ValorTotalRecoger"].ToString()) + ValorFila;
        else
            Total = Convert.ToInt64(ViewState["ValorTotalRecoger"].ToString()) - ValorFila;

        ViewState["ValorTotalRecoger"] = Total;

        long a = Convert.ToInt64(ViewState["ValorTotalRecoger"].ToString());

        txtValorTotalRecoger.Text = a.ToString();

    }


    /// <summary>
    /// Este evento es para ir a la ventana del plan de pagos y guardar los datos
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnLiquidacionCreditos_Click(object sender, ImageClickEventArgs e)
    {
       GuardarRegistro();
       Response.Redirect("~/Page/FabricaCreditos/Solicitud/PlanPagos/Lista.aspx");
    }

    /// <summary>
    ///  Método para guardar los datos de los créditos recogidos
    /// </summary>
    private Boolean GuardarRegistro()
    {

        int NumReg = gvLista.Rows.Count;

        while (NumReg != 0)
        {

            try
            {
                Xpinn.FabricaCreditos.Entities.SolicitudCreditosRecogidos vSolicitudCreditosRecogidos = new Xpinn.FabricaCreditos.Entities.SolicitudCreditosRecogidos();

                vSolicitudCreditosRecogidos.numerosolicitud = Convert.ToInt64(gvLista.Rows[NumReg - 1].Cells[3].Text);
                vSolicitudCreditosRecogidos.numero_recoge = Convert.ToInt64(txtIdentificacion.Text.Trim());
                vSolicitudCreditosRecogidos.fecharecoge = Convert.ToDateTime(txtFecha.Text.Trim());
                vSolicitudCreditosRecogidos.valorrecoge = Convert.ToInt64(gvLista.Rows[NumReg - 1].Cells[5].Text);
                vSolicitudCreditosRecogidos.fechapago = Convert.ToDateTime(txtFecha.Text.Trim());
                vSolicitudCreditosRecogidos.saldocapital = Convert.ToInt64(gvLista.Rows[NumReg - 1].Cells[6].Text);
                vSolicitudCreditosRecogidos.saldointcorr = Convert.ToInt64(gvLista.Rows[NumReg - 1].Cells[7].Text);
                vSolicitudCreditosRecogidos.saldointmora = Convert.ToInt64(gvLista.Rows[NumReg - 1].Cells[8].Text);
                vSolicitudCreditosRecogidos.saldomipyme = Convert.ToInt64(gvLista.Rows[NumReg - 1].Cells[10].Text);
                vSolicitudCreditosRecogidos.saldoivamipyme = Convert.ToInt64(gvLista.Rows[NumReg - 1].Cells[11].Text);
                vSolicitudCreditosRecogidos.saldootros = Convert.ToInt64(gvLista.Rows[NumReg - 1].Cells[12].Text);
                NumReg--;

                vSolicitudCreditosRecogidos = SolicitudCreditosRecogidosServicio.CrearSolicitudCreditosRecogidos(vSolicitudCreditosRecogidos, (Usuario)Session["usuario"]);
                idObjeto = vSolicitudCreditosRecogidos.idsolicitudrecoge.ToString();

                Session[SolicitudCreditosRecogidosServicio.CodigoPrograma + ".id"] = idObjeto;

                mvCreditosRecogidos.ActiveViewIndex = 1;
                lblMensaje.Text = "Operación exitosa";

            }
            catch (ExceptionBusiness ex)
            {
                VerError(ex.Message);
                return false;
            }
            catch (Exception ex)
            {
                BOexcepcion.Throw(SolicitudCreditosRecogidosServicio.CodigoPrograma, "btnGuardar_Click", ex);
                return false;
            }
        }
        return true;
    }

    /// <summary>
    ///  Evento que controla el botón para devolverse de pantalla
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAtras_Click(object sender, ImageClickEventArgs e)
    {
        if (Session["TipoCredito"].ToString() == "C")
            Response.Redirect("~/Page/FabricaCreditos/Solicitud/Referencias/Lista.aspx");
        else
            Response.Redirect("~/Page/FabricaCreditos/Solicitud/Georeferenciacion/Lista.aspx");
    
    }

    /// <summary>
    /// Evento que controla para ir a la siguiente pantalla
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAdelante_Click(object sender, ImageClickEventArgs e)
    {
        if (Session["TipoCredito"].ToString() == "C")
            Response.Redirect("~/Page/FabricaCreditos/Solicitud/Georeferenciacion/Lista.aspx");    //Captura biometría
        else
            Response.Redirect("~/Page/FabricaCreditos/Solicitud/PlanPagos/Lista.aspx");
    }

    /// <summary>
    /// Evento para ir a la siguiente pantalla
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAceptar_Click(object sender, ImageClickEventArgs e)
    {
        mvCreditosRecogidos.ActiveViewIndex = 0;
    }
}