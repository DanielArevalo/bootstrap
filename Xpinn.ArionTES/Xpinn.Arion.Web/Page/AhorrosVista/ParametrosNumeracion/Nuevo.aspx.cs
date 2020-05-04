using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Ahorros.Services;
using Xpinn.Ahorros.Entities;
using Xpinn.Util;
using System.IO;
using System.Configuration;
using System.Globalization;
using System.Text;
using System.Web.UI.HtmlControls;

public partial class Nuevo : GlobalWeb
{

    NumeracionAhorrosServices numeracionServicio = new NumeracionAhorrosServices();

    Int64 posicion = 0;
    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(numeracionServicio.CodigoPrograma, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            ctlMensaje.eventoClick += btnContinuarMen_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(numeracionServicio.GetType().Name + "L", "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                CargarDropDown();
                Session["Numeracion"] = null;   
                ObtenerDatos(idObjeto);
                mvAplicar.ActiveViewIndex = 0;
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(numeracionServicio.GetType().Name + "L", "Page_Load", ex);
        }
    }

    private void CargarDropDown()
    {
        try
        {
          //  ddlTipoCuenta.Items.Insert(0, new ListItem("Seleccione un item", "0"));
            ddlTipoCuenta.Items.Insert(0, new ListItem("AHORROS A LA VISTA  ", "1"));
            ddlTipoCuenta.Items.Insert(1, new ListItem("AHORRO PROGRAMADO", "2"));
            ddlTipoCuenta.Items.Insert(2, new ListItem("CDATS", "3"));
            ddlTipoCuenta.SelectedIndex = 0;
            ddlTipoCuenta.DataBind();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(numeracionServicio.GetType().Name + "L", "CargarDropDown", ex);
        }
    }

    protected void InicializargvProgramacion()
    {
        List<NumeracionAhorros> lstProgra = new List<NumeracionAhorros>();
        for (int i = 0; i < 8; i++)
        {
            NumeracionAhorros eCuenta = new NumeracionAhorros();
            eCuenta.idconsecutivo = -1;
            eCuenta.posicion = null;
            eCuenta.tipo_campo = null;
            eCuenta.valor = null;
            eCuenta.longitud = null;
            eCuenta.alinear = null;
            eCuenta.caracter_llenado = null;

            lstProgra.Add(eCuenta);
        }
        gvProgramacion.DataSource = lstProgra;
        gvProgramacion.DataBind();

        Session["Numeracion"] = lstProgra;
    }

    protected void gvProgramacion_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownListGrid ddlTipo = (DropDownListGrid)e.Row.FindControl("ddlTipo");
            if (ddlTipo != null)
            {
                ddlTipo.Items.Insert(0, new ListItem("VALOR FIJO", "0"));
                ddlTipo.Items.Insert(1, new ListItem("OFICINA", "1"));
                ddlTipo.Items.Insert(2, new ListItem("IDENTIFICACION", "2"));
                ddlTipo.Items.Insert(3, new ListItem("CODPERSONA", "3"));
                ddlTipo.Items.Insert(4, new ListItem("CODLINEA", "4"));
                ddlTipo.Items.Insert(5, new ListItem("CONSECUTIVO", "5"));
                ddlTipo.Items.Insert(6, new ListItem("CONSECUTIVO OFICINA", "6"));
                ddlTipo.Items.Insert(7, new ListItem("CONSECUTIVO CLIENTE", "7"));
                ddlTipo.Items.Insert(8, new ListItem("CONSECUTIVO LINEA", "8"));
                ddlTipo.SelectedIndex = 0;
                ddlTipo.DataBind();
            }

            DropDownListGrid ddlAlinear = (DropDownListGrid)e.Row.FindControl("ddlAlinear");
            if (ddlAlinear != null)
            {
                ddlAlinear.Items.Insert(0, new ListItem("IZQUIERDA", "I"));
                ddlAlinear.Items.Insert(1, new ListItem("DERECHA ", "D"));
                ddlAlinear.SelectedIndex = 0;
                ddlAlinear.DataBind();
            }

            //recuperando datos
            Label lbltipo = (Label)e.Row.FindControl("lbltipo");
            if (lbltipo != null)
                if (lbltipo.Text != "")
                    ddlTipo.SelectedValue = lbltipo.Text;

            Label lblalinear = (Label)e.Row.FindControl("lblalinear");
            if (lblalinear != null)
                if (lblalinear.Text.Trim() != "")
                    ddlAlinear.SelectedValue = lblalinear.Text;

            TextBox TxtValor = (TextBox)e.Row.FindControl("txtValores");
            if (TxtValor != null)
            {
                if (ddlTipo.SelectedValue == "0")
                    TxtValor.Enabled = true;
                else
                    TxtValor.Enabled = false;
            }

            TextBox TxtLongitud = (TextBox)e.Row.FindControl("TxtLongitud");
            if (TxtLongitud != null && TxtValor != null)
            {
                if (TxtLongitud.Text.Trim() != "")
                {
                    int longitudin = Convert.ToInt32(TxtLongitud.Text.Trim()); 
                    if (TxtValor.Text.Trim() != "")
                    {
                        Label lblerrorrango = (Label)e.Row.FindControl("lblerrorrango");
                        lblerrorrango.Visible = false;
                        if (longitudin < Convert.ToInt32(TxtValor.Text.Trim().Length))
                        {   
                            lblerrorrango.Text = "El valor ingresado es mayor a la longitud.";
                            lblerrorrango.Visible = true;                            
                        }
                        if (longitudin > 20)
                        {
                            lblerrorrango.Text = "Debe estar entre 1 y 20";
                            lblerrorrango.Visible = true;
                        }                        
                    }
                }
            }
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

    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            NumeracionAhorros vNumeracion = new NumeracionAhorros();
            vNumeracion.tipo_producto = Convert.ToInt32(this.ddlTipoCuenta.SelectedValue);
            vNumeracion = numeracionServicio.ConsultarNumeracionAhorros(vNumeracion.tipo_producto, (Usuario)Session["usuario"]);

           // if (!string.IsNullOrEmpty(vNumeracion.cod_linea_producto.ToString()))
               // ddlTipoCuenta.SelectedValue = HttpUtility.HtmlDecode(vNumeracion.cod_linea_producto.ToString());

            //RECUPERAR DATOS - GRILLA NUMERACION
            List<NumeracionAhorros> LstNumeracion = new List<NumeracionAhorros>();
            NumeracionAhorros pNum = new NumeracionAhorros();
            pNum.tipo_producto = Convert.ToInt32(ddlTipoCuenta.SelectedValue);
            LstNumeracion = numeracionServicio.ListarNumeracionAhorros(pNum, (Usuario)Session["usuario"]);
            if (LstNumeracion.Count > 0)
            {
                if ((LstNumeracion != null) || (LstNumeracion.Count != 0))
                {
                    gvProgramacion.DataSource = LstNumeracion;
                    gvProgramacion.DataBind();
                }
                idObjeto = "1";
                Session["Numeracion"] = LstNumeracion;
            }
            else
            {
                idObjeto = "";
                InicializargvProgramacion();
            }


        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(numeracionServicio.CodigoPrograma, "ObtenerDatos", ex);
        }
    }

    protected void btnFinal_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Nuevo);
    }

    public Boolean ValidarDatos()
    {
        VerError("");        
        if (Session["Posicion"] != null)
        {
            VerError("No puede continuar la Posición no cumple las condiciones");
            return false;
        }

        List<NumeracionAhorros> LstNumeracion = new List<NumeracionAhorros>();
        LstNumeracion = ObtenerListaNumeracion();

        if (LstNumeracion.Count == 0)
        {
            VerError("No existen registros por grabar, verifique los datos ingresados.");
            return false;
        }
        int cont = 0;
        foreach (NumeracionAhorros pNumeracion in LstNumeracion)
        {
            if (pNumeracion.tipo_campo == 0 && pNumeracion.longitud != 0)
            {
                if (pNumeracion.valor == null)
                {
                    VerError("Error en la fila " + (cont + 1) + " , No existe un valor ingresado");
                    return false;
                }
                if (pNumeracion.longitud < pNumeracion.valor.Trim().Length)
                {
                    VerError("Error en la fila " + (cont + 1) + " , No puede ingresar un valor mayor a la longitud declarada");
                }
            }
            cont++;
        }
        
        if (ddlTipoCuenta.SelectedValue == "0")
        {
            VerError("Seleccione un Tipo de Cuenta");
            return false;
        }
        return true;
    }

    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        if (ValidarDatos())
        {
            lblmsj.Text = idObjeto != "" ? "modificada" : "grabada";
            string msj = idObjeto != "" ? "modificar" : "grabar";
            ctlMensaje.MostrarMensaje("Esta Seguro de " + msj + " los Datos Ingresados?");
        }
    }

    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        try
        {
            NumeracionAhorros enumeracion = new NumeracionAhorros();

            enumeracion.tipo_producto = Convert.ToInt32(ddlTipoCuenta.SelectedValue);
            enumeracion.lstNumeracion = new List<NumeracionAhorros>();
            enumeracion.lstNumeracion = ObtenerListaNumeracion();

            if (idObjeto != "")
            {
                //MODIFICAR                
                numeracionServicio.ModificarNumeracionAhorros(enumeracion, (Usuario)Session["usuario"]);
            }
            else
            {
                if (enumeracion.lstNumeracion.Count > 0)
                {
                    //CREAR                 
                    numeracionServicio.CrearNumeracionAhorros(enumeracion, (Usuario)Session["usuario"]);                    
                }
            }
            Session.Remove("Numeracion");
            Session.Remove("Posicion");
            mvAplicar.ActiveViewIndex = 1;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(numeracionServicio.CodigoPrograma, "btnContinuar_Click", ex);
        }
    }

    protected List<NumeracionAhorros> ObtenerListaNumeracion()
    {
        try
        {
            List<NumeracionAhorros> lstNumeracion = new List<NumeracionAhorros>();
            //lista para adicionar filas sin perder datos
            List<NumeracionAhorros> lista = new List<NumeracionAhorros>();

            foreach (GridViewRow rfila in gvProgramacion.Rows)
            {
                NumeracionAhorros eNum = new NumeracionAhorros();

                Label idconsecutivo = (Label)rfila.FindControl("idconsecutivo");
                if (idconsecutivo != null)
                    eNum.idconsecutivo = idconsecutivo.Text != "" ? Convert.ToInt32(idconsecutivo.Text) : -1;
                else
                    eNum.idconsecutivo = -1;

                DropDownListGrid ddlTipo = (DropDownListGrid)rfila.FindControl("ddlTipo");
                if (ddlTipo.SelectedItem != null)
                    if (ddlTipo.SelectedValue != "")
                        eNum.tipo_campo = Convert.ToInt32(ddlTipo.SelectedValue);

                TextBox Txtvalor = (TextBox)rfila.FindControl("Txtvalores");
                if (Txtvalor != null)
                    if (Txtvalor.Text.Trim() != "")
                        eNum.valor = Txtvalor.Text;

                TextBox TxtPosicion = (TextBox)rfila.FindControl("TxtPosicion");
                if (TxtPosicion != null)
                    if (TxtPosicion.Text.Trim() != "")
                        eNum.posicion = Convert.ToInt32(TxtPosicion.Text);

                TextBox TxtLongitud = (TextBox)rfila.FindControl("TxtLongitud");
                if (TxtLongitud != null)
                    if (TxtLongitud.Text.Trim() != "")
                        eNum.longitud = Convert.ToInt32(TxtLongitud.Text);

                DropDownListGrid ddlALinear = (DropDownListGrid)rfila.FindControl("ddlALinear");
                if (ddlALinear.SelectedValue != null)
                    if (ddlALinear.SelectedValue != "")
                        eNum.alinear = Convert.ToString(ddlALinear.SelectedValue);

                TextBox TxtCaracter = (TextBox)rfila.FindControl("TxtCaracter");
                if (TxtCaracter != null)
                    if (TxtCaracter.Text != "")
                        eNum.caracter_llenado = TxtCaracter.Text;

                lista.Add(eNum);
                Session["Numeracion"] = lista;

                if (eNum.posicion != null && eNum.tipo_campo != null && eNum.longitud != null)
                {
                    lstNumeracion.Add(eNum);
                }
            }
            return lstNumeracion;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(numeracionServicio.CodigoPrograma, "ObtenerListaNumeracion", ex);
            return null;
        }
    }
    protected void gvProgramacion_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int conseID = Convert.ToInt32(gvProgramacion.DataKeys[e.RowIndex].Values[0].ToString());

        ObtenerListaNumeracion();

        List<NumeracionAhorros> LstDetalle = new List<NumeracionAhorros>();
        LstDetalle = (List<NumeracionAhorros>)Session["Numeracion"];
        if (conseID > 0)
        {
            try
            {
                foreach (NumeracionAhorros acti in LstDetalle)
                {
                    if (acti.idconsecutivo == conseID)
                    {
                        numeracionServicio.EliminarNumeracionAhorros(conseID, (Usuario)Session["usuario"]);
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
        else
        {
            LstDetalle.RemoveAt((gvProgramacion.PageIndex * gvProgramacion.PageSize) + e.RowIndex);
        }
        Session["Numeracion"] = LstDetalle;

        gvProgramacion.DataSourceID = null;
        gvProgramacion.DataBind();
        gvProgramacion.DataSource = LstDetalle;
        gvProgramacion.DataBind();
        if (LstDetalle.Count == 0)
        {
            InicializargvProgramacion();
            idObjeto = "";
        }
    }

    protected void btnProgramacion_Click(object sender, EventArgs e)
    {
        ObtenerListaNumeracion();
        List<NumeracionAhorros> LstPrograma = new List<NumeracionAhorros>();
        if (Session["Numeracion"] != null)
        {
            LstPrograma = (List<NumeracionAhorros>)Session["Numeracion"];

            for (int i = 1; i <= 1; i++)
            {
                NumeracionAhorros pDetalle = new NumeracionAhorros();

                pDetalle.idconsecutivo = -1;
                pDetalle.posicion = null;
                pDetalle.tipo_campo = null;
                pDetalle.valor = null;
                pDetalle.longitud = null;
                pDetalle.alinear = null;
                pDetalle.caracter_llenado = null;
                LstPrograma.Add(pDetalle);
            }
            //gvProgramacion.PageIndex = gvProgramacion.PageCount;
            gvProgramacion.DataSource = LstPrograma;
            gvProgramacion.DataBind();

            Session["Numeracion"] = LstPrograma;
        }

    }

    protected void ddlTipoCuenta_SelectedIndexChanged1(object sender, EventArgs e)
    {
        ObtenerDatos(ddlTipoCuenta.SelectedValue);
    }

    protected void ddlTipo_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownListGrid ddlTipo = (DropDownListGrid)sender;
        int rowIndex = Convert.ToInt32(ddlTipo.CommandArgument);
        TextBox txtValor = (TextBox)gvProgramacion.Rows[rowIndex].FindControl("txtValores");

        if (ddlTipo.SelectedValue == "0")
            txtValor.Enabled = true;
        else
        {
            txtValor.Text = "";
            txtValor.Enabled = false;
        }
    }

    protected void TxtPosicion_TextChanged(object sender, EventArgs e)
    {
        GridViewRow gvProgramacion1 = (GridViewRow)(sender as Control).Parent.Parent;

        Boolean continuar = true;
        TextBox TxtPosicion = (TextBox)sender;
        GridViewRow gridRow = ((GridViewRow)((TextBox)sender).NamingContainer);
        int index = gridRow.RowIndex;
        Label lblerrorPrinc = (Label)gridRow.FindControl("lblerror");
        int posicion = 0;
        if (TxtPosicion.Text.Trim() == "")
        {
            lblerrorPrinc.Visible = false;
            return;
        }
        posicion = Convert.ToInt32("0" + TxtPosicion.Text.Trim());
        int posicion2 = 0;

        foreach (GridViewRow row in gvProgramacion.Rows)
        {
            if (row.RowIndex != index)
            {
                TxtPosicion = (TextBox)row.FindControl("TxtPosicion");
                Label lblerror = (Label)gvProgramacion1.FindControl("lblerror");
                lblerror.Visible = false;
                posicion2 = Convert.ToInt32("0" + TxtPosicion.Text.Trim());
                if (posicion == posicion2)
                {
                    lblerror.Visible = true;
                    Session["Posicion"] = lblerror.Visible;
                    continuar = false;
                    break;
                }
                else
                {
                    Session.Remove("Posicion");
                    continuar = true;                  
                }
            }
        }
    }

    protected void TxtLongitud_TextChanged(object sender, EventArgs e)
    {
        GridViewRow gvProgramacion1 = (GridViewRow)(sender as Control).Parent.Parent;
        Label lblerrorrango = (Label)gvProgramacion1.FindControl("lblerrorrango");
        TextBox TxtLongitud = (TextBox)sender;
        lblerrorrango.Visible = false;
        if (TxtLongitud.Text.Trim() == "")
        {
            lblerrorrango.Visible = false;
            return;
        }
        int longitudin = Convert.ToInt32(TxtLongitud.Text);
        int longitud = 20;

        //VALIDAR CON LA LONGITUD DEL VALOR SI SE AH INGRESADO
        TextBox txtValores = (TextBox)gvProgramacion1.FindControl("txtValores");
        if (txtValores.Enabled == true)
        {
            if (txtValores.Text.Trim() != "")
            {
                if (longitudin < Convert.ToInt32(txtValores.Text.Trim().Length))
                {
                    lblerrorrango.Text = "El valor ingresado es mayor a la longitud.";
                    lblerrorrango.Visible = true;
                    return;
                }
            }
        }

        if (longitudin > longitud)
        {
            lblerrorrango.Text = "Debe estar entre 1 y 20";
            lblerrorrango.Visible = true;
        }
    }
}
