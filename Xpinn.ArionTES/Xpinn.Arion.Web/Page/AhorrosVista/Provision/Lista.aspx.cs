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
using System.Text;
using System.IO;
using System.Globalization;
using System.Web.UI.HtmlControls;

partial class Lista : GlobalWeb
{
    private Xpinn.Ahorros.Services.AhorroVistaServices AhorroVistaServicio = new Xpinn.Ahorros.Services.AhorroVistaServices();
   
    PoblarListas lista = new PoblarListas();
    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(AhorroVistaServicio.CodigoProgramaProvision, "L");

            Site toolBar = (Site)this.Master;

            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
            toolBar.eventoExportar += btnExportar_Click;
            toolBar.eventoGuardar += btnGuardar_Click;
            ctlMensaje.eventoClick += btnContinuarMen_Click;
            toolBar.MostrarGuardar(false);
            toolBar.MostrarExportar(false);

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AhorroVistaServicio.CodigoProgramaProvision, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                    cargar();
                LlenarCombos();
                CargarListar();
                //   txtfecha.ToDateTime = DateTime.Now;
                CargarValoresConsulta(pConsulta, AhorroVistaServicio.CodigoProgramaProvision);
                if (Session[AhorroVistaServicio.CodigoProgramaProvision + ".consulta"] != null)
                    Actualizar();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AhorroVistaServicio.CodigoProgramaProvision, "Page_Load", ex);
        }
    }

    protected void cargar()
    {

        lista.PoblarListaDesplegable("oficina", " COD_OFICINA,NOMBRE", " estado = 1", "1", ddlOficina, (Usuario)Session["usuario"]);

    }

    /// <summary>
    /// Método para llenar los DDLs requeridos para las consultas
    /// </summary>
    protected void LlenarCombos()
    {
        // Llenar el DDL de la fecha de corte 
        Configuracion conf = new Configuracion();
        List<Xpinn.Comun.Entities.Cierea> lstFechaCierre = new List<Xpinn.Comun.Entities.Cierea>();
        lstFechaCierre = AhorroVistaServicio.ListarFechaCierreCausacion((Usuario)Session["Usuario"]);
        ddlFechaCorte.DataSource = lstFechaCierre;
        ddlFechaCorte.DataTextFormatString = "{0:" + conf.ObtenerFormatoFecha() + "}";
        ddlFechaCorte.DataTextField = "fecha";
        ddlFechaCorte.DataBind();
    }
    protected void btnGuardar_Click(object sender, EventArgs e)
    {

        ///mensaje al guardar
        ctlMensaje.MostrarMensaje("Desea guardar los datos de la provision?");
    }
    private Xpinn.Ahorros.Entities.provision_ahorro ObtenerValor()
    {
        Xpinn.Ahorros.Entities.provision_ahorro vahorros = new Xpinn.Ahorros.Entities.provision_ahorro();
     
        if (ddlLineaAhorro.Text.Trim() != "")
            vahorros.cod_linea_ahorro = Convert.ToString(ddlLineaAhorro.Text.Trim());      

        if (ddlOficina.SelectedIndex != 0)
            vahorros.cod_oficina = Convert.ToInt32(ddlOficina.SelectedValue.Trim());
      

        return vahorros;
    }
    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        try
        {
            ///Inicializo la operacion tipo 60
            ObtenerListaDetalle();
            VerError("");
            Usuario vUsuario = new Usuario();
            vUsuario = (Usuario)Session["Usuario"];
            Xpinn.Tesoreria.Entities.Operacion poperacion = new Xpinn.Tesoreria.Entities.Operacion();
            poperacion.cod_ope = 0;
            poperacion.tipo_ope = 60;
            poperacion.cod_caja = 0;
            poperacion.cod_cajero = 0;
            poperacion.observacion = "Provision realizada";
            poperacion.cod_proceso = null;
            poperacion.fecha_oper = DateTime.Now;
            poperacion.fecha_calc = Convert.ToDateTime(this.ddlFechaCorte.SelectedValue);
            poperacion.cod_ofi = vUsuario.cod_oficina;

            ///inicializo la operacion CIEREA
            Xpinn.Comun.Entities.Cierea pcierea = new Xpinn.Comun.Entities.Cierea();
            pcierea.campo1 = " ";
            pcierea.tipo = "I";
            pcierea.estado = "D";
            pcierea.campo2 = vUsuario.cod_oficina.ToString();
            pcierea.fecha = Convert.ToDateTime(this.ddlFechaCorte.SelectedValue);
            pcierea.codusuario = vUsuario.codusuario;

            List<Xpinn.Ahorros.Entities.provision_ahorro> lstInsertar = new List<Xpinn.Ahorros.Entities.provision_ahorro>();
            ///inicializo lalista en la entidad provision_ahorro
            lstInsertar = ObtenerListaDetalle();
            Xpinn.Ahorros.Entities.provision_ahorro ahorro = new Xpinn.Ahorros.Entities.provision_ahorro();

            if (idObjeto != "&nbsp;")
            {
                AhorroVistaServicio.InsertarDatos(ahorro, lstInsertar, poperacion, (Usuario)Session["usuario"]);       
            }
            AhorroVistaServicio.Crearcierea(pcierea, (Usuario)Session["usuario"]);

            if (ahorro.cod_ope != null)
            {
                var usu = (Usuario)Session["usuario"];
                Xpinn.Contabilidad.Services.ComprobanteService ComprobanteServicio = new Xpinn.Contabilidad.Services.ComprobanteService();
                Session[ComprobanteServicio.CodigoPrograma + ".cod_ope"] = ahorro.cod_ope;
                Session[ComprobanteServicio.CodigoPrograma + ".tipo_ope"] = 60;
                Session[ComprobanteServicio.CodigoPrograma + ".fecha_ope"] = Convert.ToDateTime(this.ddlFechaCorte.SelectedValue);
                Session[ComprobanteServicio.CodigoPrograma + ".cod_persona"] = usu.cod_persona; //"<Colocar Aquí el código de la persona del servicio>"
                Navegar("../../Contabilidad/Comprobante/Nuevo.aspx");

                Session[AhorroVistaServicio.CodigoProgramaProvision + ".id"] = idObjeto;
            }
            Site toolBar = (Site)this.Master;
            toolBar.MostrarGuardar(false);
            toolBar.MostrarCancelar(false);
            toolBar.MostrarConsultar(true);
        }

        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AhorroVistaServicio.CodigoProgramaProvision, "btnGuardar_Click", ex);
        }
    }
    protected List<Xpinn.Ahorros.Entities.provision_ahorro> ObtenerListaDetalle()
    {
        List<Xpinn.Ahorros.Entities.provision_ahorro> lstBeneficiarios = new List<Xpinn.Ahorros.Entities.provision_ahorro>();
        List<Xpinn.Ahorros.Entities.provision_ahorro> lista = new List<Xpinn.Ahorros.Entities.provision_ahorro>();

        foreach (GridViewRow rfila in gvLista.Rows)
        {
            /// Cargo los datos de la grilla a la entidad provision_ahorro por eBenef
            Xpinn.Ahorros.Entities.provision_ahorro eBenef = new Xpinn.Ahorros.Entities.provision_ahorro();

            if (rfila.Cells[1].Text != " ")
                eBenef.numero_cuenta = rfila.Cells[1].Text;

            if (rfila.Cells[2].Text != " ")
                eBenef.cod_linea_ahorro = rfila.Cells[2].Text;

            if (rfila.Cells[3].Text != " ")
                eBenef.identificacion = rfila.Cells[3].Text;

            if (rfila.Cells[4].Text != " ")
                eBenef.nombres = Convert.ToString(rfila.Cells[4].Text);

            if (rfila.Cells[5].Text != " ")
                eBenef.cod_oficina = Convert.ToInt32(rfila.Cells[5].Text);

            // if (rfila.Cells[6].Text != " ")
            eBenef.fecha = Convert.ToDateTime(ddlFechaCorte.SelectedValue);

            if (rfila.Cells[7].Text != " ")
                eBenef.saldo_total = Convert.ToDecimal(rfila.Cells[7].Text.Replace(gSeparadorMiles, ""));

            if (rfila.Cells[8].Text != " ")
                eBenef.saldo_base = Convert.ToDecimal(rfila.Cells[8].Text.Replace(gSeparadorMiles, ""));

            if (rfila.Cells[9].Text != " ")
            {

                eBenef.intereses = Convert.ToDecimal(rfila.Cells[9].Text.Replace(gSeparadorMiles, ""));
            }

            if (rfila.Cells[10].Text != " ")
            {
                //rfila.Cells[10].Text = "0";
                eBenef.tasa_interes = Convert.ToDecimal(rfila.Cells[10].Text.Replace(gSeparadorMiles, ""));
            }

            if (rfila.Cells[11].Text != " ")
            {
                //rfila.Cells[10].Text = "0";
                eBenef.dias = Convert.ToInt32(rfila.Cells[11].Text.Replace(gSeparadorMiles, ""));
            }
            if (rfila.Cells[12].Text != " ")
            {
                //rfila.Cells[10].Text = "0";
                eBenef.valor_acumulado = Convert.ToInt32(rfila.Cells[12].Text.Replace(gSeparadorMiles, ""));
            }

            lista.Add(eBenef);
            Session["DatosDetalle"] = lista;
            ///Si codigo_linea_ahorro es diferente de nulo adiciona la lista con los datos
            // if (eBenef.cod_linea_ahorro.Trim() != null )
            //{
            lstBeneficiarios.Add(eBenef);
            // }
        }
        return lstBeneficiarios;
    }

    private void CargarListar()
    {
        ///carga y lista las variables a la entidad linea ahorro services 
        Xpinn.Ahorros.Services.LineaAhorroServices linahorroServicio = new Xpinn.Ahorros.Services.LineaAhorroServices();
        Xpinn.Ahorros.Entities.LineaAhorro linahorroVista = new Xpinn.Ahorros.Entities.LineaAhorro();
        ddlLineaAhorro.DataTextField = "descripcion";
        ddlLineaAhorro.DataValueField = "cod_linea_ahorro";
        ddlLineaAhorro.DataSource = linahorroServicio.ListarLineaAhorro(linahorroVista, (Usuario)Session["usuario"]);
        ddlLineaAhorro.DataBind();
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        ///pone en 0 los datos nulos cuando consulta y verifica las variables
        foreach (GridViewRow rfila in gvLista.Rows)
        {
            if (rfila.Cells[10].Text == "&nbsp;")
            {
                rfila.Cells[10].Text = "0";
            }
        }
        if (txtfecha.Text == "")
        {
            VerError("Ingrese una fecha valida");

        }

        //if (ddlLineaAhorro.SelectedIndex == 0) {
        //    VerError("Seleccione la linea");

        //    return;

        //}

        GuardarValoresConsulta(pConsulta, AhorroVistaServicio.CodigoProgramaProvision);
        Actualizar();
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        txtfecha.Text = ("");
        gvLista.Visible = false;
        lblTotalRegs.Visible = false;
        LimpiarValoresConsulta(pConsulta, AhorroVistaServicio.CodigoProgramaProvision);
    }

    protected void gvLista_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            ConfirmarEliminarFila(e, "btnEliminar");
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AhorroVistaServicio.CodigoProgramaProvision + "L", "gvLista_RowDataBound", ex);
        }
    }

    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        String id = gvLista.DataKeys[gvLista.SelectedRow.RowIndex].Value.ToString();
        Session[AhorroVistaServicio.CodigoProgramaProvision + ".id"] = id;
        Navegar(Pagina.Nuevo);
    }

    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvLista.DataKeys[e.NewEditIndex].Value.ToString();
        Session[AhorroVistaServicio.CodigoProgramaProvision + ".id"] = id;
        Navegar(Pagina.Nuevo);
    }

    protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            string id = Convert.ToString(e.Keys[0]);
            AhorroVistaServicio.EliminarAhorroVista(id, (Usuario)Session["usuario"]);
            Actualizar();
        }
        catch (Xpinn.Util.ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
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
            BOexcepcion.Throw(AhorroVistaServicio.CodigoProgramaProvision, "gvLista_PageIndexChanging", ex);
        }
    }

    private void Actualizar()
    {        

        VerError("");
        try
        {
            // Validar la fecha
            if (ddlFechaCorte.SelectedValue == "")
            {
                VerError("No hay fecha para generar la causación de ahorros (CIEREA Tipo='I') ");
                return;
            }

            ///carga todo a la entidad provision_ahorro por la variable ahorro

           Xpinn.Ahorros.Entities.provision_ahorro ahorro = new Xpinn.Ahorros.Entities.provision_ahorro();
          if (ddlLineaAhorro.SelectedIndex != 0)
              ahorro.cod_linea_ahorro = Convert.ToString(ddlLineaAhorro.SelectedValue);
            else
               ahorro.cod_linea_ahorro = Convert.ToString(0);
            if (ddlOficina.SelectedIndex != 0)
                ahorro.cod_oficina = Convert.ToInt32(ddlOficina.SelectedValue);
           else
               ahorro.cod_oficina = Convert.ToInt32(ddlOficina.SelectedIndex =0);

            List<Xpinn.Ahorros.Entities.provision_ahorro> lstConsulta = new List<Xpinn.Ahorros.Entities.provision_ahorro>();
            DateTime pFechaIni;
            Xpinn.Ahorros.Entities.provision_ahorro pAhorroVista = new Xpinn.Ahorros.Entities.provision_ahorro();
            //String format = "dd/MM/yyyy";
            // pFechaIni = DateTime.ParseExact(ddlFechaCorte.SelectedValue, format, CultureInfo.InvariantCulture);           
            pFechaIni = Convert.ToDateTime(this.ddlFechaCorte.SelectedValue);


            //verificar cierre historico 
            String estado = "";
            DateTime fechacierrehistorico;
            DateTime fechacausacion = pFechaIni;
            Xpinn.Ahorros.Entities.AhorroVista vAhorroVista = new Xpinn.Ahorros.Entities.AhorroVista();
            vAhorroVista = AhorroVistaServicio.ConsultarCierreAhorroVista((Usuario)Session["usuario"]);
            estado = vAhorroVista.estadocierre;
            fechacierrehistorico = Convert.ToDateTime(vAhorroVista.fecha_cierre.ToString());

            if (estado == "D" && fechacausacion <= fechacierrehistorico)
            {
                VerError("NO PUEDE  GENERAR CAUSACION EN PERIODOS YA CERRADOS, TIPO H,'AHORROS'");
                Site toolBar = (Site)this.Master;
                toolBar.MostrarConsultar(false);
              
            }

            else
            {

                ///ingresa a la capa de bussines por listar provision
                lstConsulta = AhorroVistaServicio.ListarProvision(pFechaIni, ahorro, (Usuario)Session["usuario"]);

                gvLista.PageSize = pageSize;
                gvLista.EmptyDataText = emptyQuery;
                gvLista.DataSource = lstConsulta;

                if (lstConsulta.Count > 0)
                {

                    Site toolBar = (Site)this.Master;
                    toolBar.MostrarGuardar(true);
                    toolBar.MostrarExportar(true);
                    gvLista.Visible = true;
                    lblTotalRegs.Visible = true;
                    lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                    Session["DTAhorroVista"] = lstConsulta;
                    gvLista.DataBind();
                    ValidarPermisosGrilla(gvLista);
                }
                else
                {

                    Site toolBar = (Site)this.Master;
                    toolBar.MostrarGuardar(false);
                    toolBar.MostrarExportar(false);
                    gvLista.Visible = false;
                    lblTotalRegs.Visible = false;
                }

                Session.Add(AhorroVistaServicio.CodigoProgramaProvision + ".consulta", 1);
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AhorroVistaServicio.CodigoProgramaProvision, "Actualizar", ex);
        }
    }

    private Xpinn.Ahorros.Entities.provision_ahorro ObtenerValores()
    {
        Xpinn.Ahorros.Entities.provision_ahorro vAhorroVista = new Xpinn.Ahorros.Entities.provision_ahorro();
        
        if (ddlLineaAhorro.Text.Trim() != "")
            vAhorroVista.cod_linea_ahorro = Convert.ToString(ddlLineaAhorro.Text.Trim());
        vAhorroVista.cod_linea_ahorro = Convert.ToString(ddlLineaAhorro.SelectedIndex = 0);
        //linea de ahorro

        if (ddlOficina.SelectedIndex != 0)
            vAhorroVista.cod_oficina = Convert.ToInt32(ddlOficina.SelectedValue.Trim());
        vAhorroVista.cod_oficina = Convert.ToInt32(ddlOficina.SelectedIndex = 0);

  
        //estado de cuenta
        if (ddlFechaCorte.SelectedIndex != 0)
            vAhorroVista.fecha = Convert.ToDateTime(ddlFechaCorte.SelectedValue);
   
     

        return vAhorroVista;
    }

    protected void btnExportar_Click(object sender, EventArgs e)
    {
        ExportToExcel(gvLista);
    }

    protected void ExportToExcel(GridView GridView1)
    {
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=Provision.xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";
        Response.ContentEncoding = Encoding.Default;
        StringWriter sw = new StringWriter();
        ExpGrilla expGrilla = new ExpGrilla();

        sw = expGrilla.ObtenerGrilla(GridView1, null);

        Response.Write(expGrilla.style);
        Response.Output.Write(sw.ToString());
        Response.Flush();
        Response.End();
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }

}