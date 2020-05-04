using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.FabricaCreditos.Entities;
using Xpinn.FabricaCreditos.Services;
using Xpinn.Util;

public partial class General_Controles_ctlCodeudores : System.Web.UI.UserControl
{
    #region Variables

    private Xpinn.FabricaCreditos.Services.Persona1Service DatosClienteServicio = new Xpinn.FabricaCreditos.Services.Persona1Service();
    private Usuario usu = new Usuario();
    private Xpinn.FabricaCreditos.Services.codeudoresService CodeudorServicio = new Xpinn.FabricaCreditos.Services.codeudoresService();
    DatosSolicitud datosSolicitud = new DatosSolicitud();
    private long _codPersona;
    string Error = "";

    #endregion

    #region Propiedades
    public string identificacion
    {
        get { return txtIdentificacion.Text.Trim().ToUpper(); }
        set { txtIdentificacion.Text = value; }
    }
    #endregion

    #region Metodos de inicio

    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["Cod_persona"] != null)
            _codPersona = Convert.ToInt64(Session["Cod_persona"]);
        else
            _codPersona = 0;
        AgregarOnchange();
    }

    #endregion

    #region Propiedades

    public string IdentifSolicitante
    {
        get { return txtIdSOlicitante.Text.Trim().ToUpper(); }
        set { txtIdSOlicitante.Text = value; }
    }

    #endregion

    #region Metodos de la tabla

    protected void gvListaCodeudores_RowEditing(object sender, GridViewEditEventArgs e)
    {
        // GENERAR EDICION
        gvListaCodeudores.EditIndex = e.NewEditIndex;
        string id = gvListaCodeudores.DataKeys[e.NewEditIndex].Value.ToString();
        if (Session[usu.codusuario + "Codeudores"] != null)
        {
            gvListaCodeudores.DataSource = Session[usu.codusuario + "Codeudores"];
            gvListaCodeudores.DataBind();
            ObtenerSiguienteOrden();
        }
        else
            InicialCodeudores();

        //OcultarGridFooter(gvListaCodeudores, false);
        AgregarOnchange();
    }

    protected void gvListaCodeudores_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        // GENERAR REVERSION
        gvListaCodeudores.EditIndex = -1;
        if (Session[usu.codusuario + "Codeudores"] != null)
        {
            gvListaCodeudores.DataSource = Session[usu.codusuario + "Codeudores"];
            gvListaCodeudores.DataBind();
            ObtenerSiguienteOrden();
        }
        else
            InicialCodeudores();
        //OcultarGridFooter(gvListaCodeudores, true);
        AgregarOnchange();
    }

    protected void gvListaCodeudores_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        gvListaCodeudores.EditIndex = -1;
        if (Session[usu.codusuario + "Codeudores"] != null)
        {
            TextBox txtOrdenRow = (TextBox)gvListaCodeudores.Rows[e.RowIndex].FindControl("txtOrdenRow");
            if (string.IsNullOrEmpty(txtOrdenRow.Text))
            {
                //lblError.Text = ("Ingrese el orden al que pertenece el codeudor.");
                return;
            }
            List<Persona1> lstCodeudores = (List<Persona1>)Session[usu.codusuario + "Codeudores"];
            lstCodeudores[e.RowIndex].orden = Convert.ToInt32(txtOrdenRow.Text);
            gvListaCodeudores.DataSource = lstCodeudores;
            gvListaCodeudores.DataBind();
            Session[usu.codusuario + "Codeudores"] = lstCodeudores;
            ObtenerSiguienteOrden();
        }
        else
            InicialCodeudores();
        //OcultarGridFooter(gvListaCodeudores, true);
        AgregarOnchange();
    }

    protected void gvListaCodeudores_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        List<Persona1> lstCodeudores = new List<Persona1>();
        lstCodeudores = (List<Persona1>)Session[usu.codusuario + "Codeudores"];
        if (lstCodeudores.Count >= 1)
        {
            Persona1 eCodeudor = new Persona1();
            int index = Convert.ToInt32(e.RowIndex);
            eCodeudor = lstCodeudores[index];
            if (eCodeudor.cod_persona != 0)
            {
                CodeudorServicio.EliminarcodeudoresCred(eCodeudor.idcodeudor, Convert.ToInt64(Session["Numero_Radicacion"].ToString()), (Usuario)Session["usuario"]);
                lstCodeudores.Remove(eCodeudor); //PENDIENTE
                                                 //CodeudorServicio.EliminarcodeudoresCred(eCodeudor.cod_persona, Convert.ToInt64(txtCredito.Text), (Usuario)Session["usuario"]);
            }
            Session[usu.codusuario + "Codeudores"] = lstCodeudores;
        }
        if (lstCodeudores.Count == 0)
        {
            lblTotReg.Visible = false;
            lblTotalRegsCodeudores.Visible = true;
            InicialCodeudores();
        }
        else
        {
            lblTotReg.Visible = true;
            lblTotReg.Text = "<br/> Codeudores a registrar : " + lstCodeudores.Count.ToString();
            lblTotalRegsCodeudores.Visible = false;
            gvListaCodeudores.DataSource = lstCodeudores;
            gvListaCodeudores.DataBind();
            Session[usu.codusuario + "Codeudores"] = lstCodeudores;
            ObtenerSiguienteOrden();
        }
        BorrarReferenciaCodeudorBorradoGVReferencias(lstCodeudores);
        AgregarOnchange();
        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "DoPostBack", "__doPostBack(sender, e)", true);
        Response.Redirect(Request.RawUrl);
    }

    protected void gvListaCodeudores_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        var cadena = HttpContext.Current.Request.Url.AbsoluteUri;
        string Page = cadena.Split('/').Last();
        // lblError.Text = ("");
        if (e.CommandName.Equals("AddNew"))
        {
            TextBox txtidentificacion = (TextBox)gvListaCodeudores.FooterRow.FindControl("txtidentificacion");
            TextBox txtOdenFooter = (TextBox)gvListaCodeudores.FooterRow.FindControl("txtOdenFooter");
            if (txtidentificacion.Text.Trim() == "")
            {
                lblError.Text = ("Ingrese la Identificación del Codeudor a Agregar por favor.");
                return;
            }
            if (string.IsNullOrEmpty(txtOdenFooter.Text))
            {
                lblError.Text = ("Ingrese el orden del codeudor por favor.");
                return;
            }
            string IdentifSolic = IdentifSolicitante;
            if (IdentifSolic.Trim() == txtidentificacion.Text.Trim())
            {
                lblError.Text = ("No puede ingresar como codeudor a la persona solicitante.");
                return;
            }

            Xpinn.Comun.Data.GeneralData DAGeneral = new Xpinn.Comun.Data.GeneralData();
            Xpinn.Comun.Entities.General pEntidad = new Xpinn.Comun.Entities.General();
            pEntidad = DAGeneral.ConsultarGeneral(480, (Usuario)Session["usuario"]);
            try
            {
                if (pEntidad.valor != null)
                {
                    if (Convert.ToInt32(pEntidad.valor) > 0)
                    {
                        int paramCantidad = 0, cantReg = 0;
                        paramCantidad = Convert.ToInt32(pEntidad.valor);
                        Xpinn.FabricaCreditos.Entities.codeudores pCodeu = new Xpinn.FabricaCreditos.Entities.codeudores();
                        pCodeu = CodeudorServicio.ConsultarCantidadCodeudores(txtidentificacion.Text, (Usuario)Session["usuario"]);
                        if (pCodeu.cantidad != null)
                        {
                            cantReg = Convert.ToInt32(pCodeu.cantidad);
                            if (cantReg >= paramCantidad)
                            {
                                lblError.Text = ("No puede adicionar esta persona debido a que ya mantiene el límite de veces como codeudor.");
                                return;
                            }
                        }
                    }
                }
            }
            catch { }

            List<Persona1> lstCodeudor = new List<Persona1>();
            if (Session[usu.codusuario + "Codeudores"] != null)
            {
                lstCodeudor = (List<Persona1>)Session[usu.codusuario + "Codeudores"];

                if (lstCodeudor.Count == 1)
                {
                    // si no se adicionón ningún codeudor entonces quita el que se creo para inicializar la gridView porque es vacio
                    Persona1 gItem = new Persona1();
                    gItem = lstCodeudor[0];
                    if (gItem.cod_persona == 0)
                        lstCodeudor.Remove(gItem);
                }
            }

            Xpinn.FabricaCreditos.Entities.codeudores vcodeudor = new Xpinn.FabricaCreditos.Entities.codeudores();
            vcodeudor = CodeudorServicio.ConsultarDatosCodeudor(txtidentificacion.Text, (Usuario)Session["usuario"]);
            Persona1 gItemNew = new Persona1();
            gItemNew.cod_persona = vcodeudor.codpersona;
            gItemNew.identificacion = vcodeudor.identificacion;
            gItemNew.primer_nombre = vcodeudor.primer_nombre;
            gItemNew.segundo_nombre = vcodeudor.segundo_nombre;
            gItemNew.primer_apellido = vcodeudor.primer_apellido;
            gItemNew.segundo_apellido = vcodeudor.segundo_apellido;
            gItemNew.direccion = vcodeudor.direccion;
            gItemNew.telefono = vcodeudor.telefono;
            gItemNew.orden = Convert.ToInt32(txtOdenFooter.Text);

            // validar que no existe el mismo codeudor en la gridview
            // PENDIENTE VALIDAR
            bool isValid = gvListaCodeudores.Rows.OfType<GridViewRow>().Where(x => ((Label)x.FindControl("lblCodPersona")).Text == gItemNew.cod_persona.ToString()).Any();
            if (!isValid)
                lstCodeudor.Add(gItemNew);

            gvListaCodeudores.DataSource = lstCodeudor;
            gvListaCodeudores.DataBind();
            Session[usu.codusuario + "Codeudores"] = lstCodeudor;
            if (lstCodeudor.Count > 0)
            {
                lblTotReg.Visible = true;
                lblTotReg.Text = "<br/> Codeudores a registrar : " + lstCodeudor.Count.ToString();
                lblTotalRegsCodeudores.Visible = false;
            }
            else
            {
                lblTotReg.Visible = false;
                lblTotalRegsCodeudores.Visible = true;
            }

            LlenarDDLQuienReferenciaGVReferencias(lstCodeudor);
            ObtenerSiguienteOrden();
            AgregarOnchange();
            GuardarCodeudores(ref Error);

            Response.Redirect(Page);
        }
    }

    protected void txtidentificacion_TextChanged(object sender, EventArgs e)
    {

        Control ctrl = gvListaCodeudores.FooterRow.FindControl("txtidentificacion");
        if (ctrl != null)
        {
            TextBox txtidentificacion = (TextBox)ctrl;
            if (txtidentificacion.Text != "")
            {
                if (identificacion != txtidentificacion.Text)
                {
                    Xpinn.FabricaCreditos.Services.codeudoresService codeudorServicio = new Xpinn.FabricaCreditos.Services.codeudoresService();
                    Xpinn.FabricaCreditos.Entities.codeudores vcodeudor = new Xpinn.FabricaCreditos.Entities.codeudores();
                    vcodeudor = codeudorServicio.ConsultarDatosCodeudor(txtidentificacion.Text, (Usuario)Session["usuario"]);
                    if (vcodeudor.codpersona != 0)
                    {
                        txtIdentificacion.Text = "";
                        ((Label)gvListaCodeudores.FooterRow.FindControl("lblCodPersonaFooter")).Text = vcodeudor.codpersona.ToString();
                        gvListaCodeudores.FooterRow.Cells[4].Text = vcodeudor.primer_nombre;
                        gvListaCodeudores.FooterRow.Cells[5].Text = vcodeudor.segundo_nombre;
                        gvListaCodeudores.FooterRow.Cells[6].Text = vcodeudor.primer_apellido;
                        gvListaCodeudores.FooterRow.Cells[7].Text = vcodeudor.segundo_apellido;
                        gvListaCodeudores.FooterRow.Cells[8].Text = vcodeudor.direccion;
                        gvListaCodeudores.FooterRow.Cells[9].Text = vcodeudor.telefono;
                    }
                    else
                    {
                        ((Label)gvListaCodeudores.FooterRow.FindControl("lblCodPersonaFooter")).ForeColor = System.Drawing.Color.Red;
                        string pagina = "";
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "jsKeys", "javascript:validar();", true);
                    }
                }
                else
                {
                    txtidentificacion.Text = "";
                }
            }
            else
            {
                ((Label)gvListaCodeudores.FooterRow.FindControl("lblCodPersonaFooter")).Text = "";
                gvListaCodeudores.FooterRow.Cells[4].Text = "";
                gvListaCodeudores.FooterRow.Cells[5].Text = "";
                gvListaCodeudores.FooterRow.Cells[6].Text = "";
                gvListaCodeudores.FooterRow.Cells[7].Text = "";
                gvListaCodeudores.FooterRow.Cells[8].Text = "";
                gvListaCodeudores.FooterRow.Cells[9].Text = "";
            }
        }

    }

    protected void btnConsultaPersonas_Click(object sender, EventArgs e)
    {
        BusquedaRapida ctlBusquedaPersonas = (BusquedaRapida)gvListaCodeudores.FooterRow.FindControl("ctlBusquedaPersonas");
        ctlBusquedaPersonas.Motrar(true, "txtidentificacion", "");
    }

    #endregion

    #region Metodos Externos

    public void TablaCodeudores()
    {
        try
        {
            List<Xpinn.FabricaCreditos.Entities.Persona1> lstConsulta = new List<Xpinn.FabricaCreditos.Entities.Persona1>();
            lstConsulta = DatosClienteServicio.ListarPersona1(ObtenerValoresCodeudores(), (Usuario)Session["usuario"]);

            gvListaCodeudores.PageSize = 5;

            if (lstConsulta.Count > 0)
            {
                gvListaCodeudores.Visible = true;
                lblTotalRegsCodeudores.Visible = false;
                lblTotReg.Visible = true;
                lblTotReg.Text = "<br/> Codeudores a registrar : " + lstConsulta.Count.ToString();

                gvListaCodeudores.DataSource = lstConsulta;
                gvListaCodeudores.DataBind();
                Session[usu.codusuario + "Codeudores"] = lstConsulta;
                ObtenerSiguienteOrden();
            }
            else
            {
                //idObjeto = "";
                gvListaCodeudores.Visible = true;
                lblTotReg.Visible = false;
                lblTotalRegsCodeudores.Visible = true;
                InicialCodeudores();
            }
            InicializarReferencias();
            LlenarDDLQuienReferenciaGVReferencias(new List<Persona1>());
        }
        catch (Exception ex)
        {
            //Ignore
        }
    }

    public void InicialCodeudores()
    {
        List<Xpinn.FabricaCreditos.Entities.Persona1> lstConsulta = new List<Xpinn.FabricaCreditos.Entities.Persona1>();
        Xpinn.FabricaCreditos.Entities.Persona1 eCodeudor = new Xpinn.FabricaCreditos.Entities.Persona1();
        lstConsulta.Add(eCodeudor);
        Session[usu.codusuario + "Codeudores"] = lstConsulta;
        gvListaCodeudores.DataSource = lstConsulta;
        gvListaCodeudores.DataBind();
        ObtenerSiguienteOrden();
    }

    public void ObtenerSiguienteOrden()
    {
        var maxValue = 0;
        if (Session[usu.codusuario + "Codeudores"] != null)
        {
            List<Persona1> lstCodeudor = (List<Persona1>)Session[usu.codusuario + "Codeudores"];
            maxValue = lstCodeudor.Max(x => x.orden);
        }
     ((TextBox)gvListaCodeudores.FooterRow.FindControl("txtOdenFooter")).Text = (maxValue + 1).ToString();
    }

    public Persona1 ObtenerValoresCodeudores()
    {
        Xpinn.FabricaCreditos.Entities.Persona1 vPersona1 = new Xpinn.FabricaCreditos.Entities.Persona1();

        if (Session["Numero_Radicacion"] != null)
            vPersona1.numeroRadicacion = Convert.ToInt64(Session["Numero_Radicacion"].ToString());

        vPersona1.seleccionar = "CD"; //Bandera para ejecuta el select del CODEUDOR

        return vPersona1;
    }

    public void BorrarReferenciaCodeudorBorradoGVReferencias(List<Persona1> lstCodeudores)
    {
        List<long> lstIDCodeudores = lstCodeudores.Select(x => x.cod_persona).ToList();
        lstIDCodeudores.Add(_codPersona);

        List<Referncias> lstReferencia = RecorresGrillaReferencias();

        lstReferencia = lstReferencia.Where(x => lstIDCodeudores.Contains(x.cod_persona_quien_referencia)).ToList();
        long[] idSeleccionado = lstReferencia.Select(x => x.cod_persona_quien_referencia).ToArray();

        if (lstReferencia.Count == 0)
        {
            lstReferencia.Add(new Referncias());
        }

        gvReferencias.DataSource = lstReferencia;
        gvReferencias.DataBind();

        LlenarDDLQuienReferenciaGVReferencias(lstCodeudores, idSeleccionado);
    }

    public void LlenarDDLQuienReferenciaGVReferencias(List<Persona1> lstCodeudor, long[] idSeleccionado = null)
    {
        int contador = 0;
        var listaABindearDDL = (from codeudor in lstCodeudor
                                where codeudor.cod_persona != 0
                                select codeudor).ToList();

        Persona1 deudor = new Persona1() { primer_nombre = "Solicitante", cod_persona = _codPersona };
        listaABindearDDL.Add(deudor);

        foreach (GridViewRow row in gvReferencias.Rows)
        {
            DropDownList ddlQuienReferencia = row.Cells[1].FindControl("ddlQuienReferencia") as DropDownList;

            var valueSeleccionadoEnDDL = ddlQuienReferencia.SelectedValue;

            if (ddlQuienReferencia != null)
            {
                ddlQuienReferencia.DataSource = listaABindearDDL;
                ddlQuienReferencia.DataTextField = "nombreYApellido";
                ddlQuienReferencia.DataValueField = "cod_persona";
                ddlQuienReferencia.DataBind();

                if (idSeleccionado != null)
                {
                    ddlQuienReferencia.SelectedValue = idSeleccionado[contador].ToString();
                    contador += 1;
                }
                else
                {
                    ddlQuienReferencia.SelectedValue = listaABindearDDL.FirstOrDefault(x => x.nombreYApellido.Trim() == "Solicitante").cod_persona.ToString();
                }
            }
            else
            {
                lblError.Text = ("Ocurrio un error al agregar la referencia del codeudor, LlenarDDLQuienReferenciaGVReferencias");
                return;
            }
        }
    }

    public List<Referncias> RecorresGrillaReferencias()
    {

        List<Referncias> lstReferencia = new List<Referncias>();

        foreach (GridViewRow gFila in gvReferencias.Rows)
        {
            Referncias referencia = new Referncias()
            {
                cod_persona_quien_referencia = Convert.ToInt64(((DropDownList)gFila.FindControl("ddlQuienReferencia")).SelectedValue),
                tiporeferencia = Convert.ToInt64(((DropDownList)gFila.FindControl("ddlTipoReferencia")).SelectedValue),
                nombres = ((TextBox)gFila.FindControl("txtNombres")).Text,
                codparentesco = Convert.ToInt64(((DropDownList)gFila.FindControl("ddlParentesco")).SelectedValue),
                direccion = ((TextBox)gFila.FindControl("txtDireccion")).Text,
                telefono = ((TextBox)gFila.FindControl("txtTelefono")).Text,
                teloficina = ((TextBox)gFila.FindControl("txtTelOficina")).Text,
                celular = ((TextBox)gFila.FindControl("txtCelular")).Text
            };

            lstReferencia.Add(referencia);
        }

        return lstReferencia;
    }

    private List<Referncias> LlenarListaDeGVReferencias(long numero_solicitud, out string error)
    {
        error = string.Empty;
        List<Referncias> lstReferencia = new List<Referncias>();

        foreach (GridViewRow gFila in gvReferencias.Rows)
        {
            Referncias referencia = new Referncias();
            string nombres = ((TextBox)gFila.FindControl("txtNombres")).Text;
            string telefono = ((TextBox)gFila.FindControl("txtTelefono")).Text;
            string tipoReferencia = ((DropDownList)gFila.FindControl("ddlTipoReferencia")).SelectedValue;
            string telefonoOficina = ((TextBox)gFila.FindControl("txtTelOficina")).Text;
            string direccion = ((TextBox)gFila.FindControl("txtDireccion")).Text;
            string celular = ((TextBox)gFila.FindControl("txtCelular")).Text;
            string codparentesco = ((DropDownList)gFila.FindControl("ddlParentesco")).SelectedValue;

            if (!string.IsNullOrWhiteSpace(nombres) || !string.IsNullOrWhiteSpace(telefono) || !string.IsNullOrWhiteSpace(direccion) || !string.IsNullOrWhiteSpace(telefonoOficina) || !string.IsNullOrWhiteSpace(celular))
            {
                if (string.IsNullOrWhiteSpace(nombres))
                {
                    error += "Debe ingresar el nombre en las referencias";
                    return lstReferencia;
                }
                if (string.IsNullOrWhiteSpace(telefono))
                {
                    error += "Debe ingresar el telefono en las referencias";
                    return lstReferencia;
                }
                if (tipoReferencia == "1" && codparentesco == "0")
                {
                    error += "Debe ingresar el tipo de parentesco si la referencia es personal";
                    return lstReferencia;
                }

                referencia.tiporeferencia = Convert.ToInt64(tipoReferencia);
                referencia.nombres = nombres;
                referencia.codparentesco = Convert.ToInt64(codparentesco);
                referencia.direccion = direccion;
                referencia.telefono = telefono;
                referencia.teloficina = telefonoOficina;
                referencia.celular = celular;
                referencia.numero_radicacion = Session["Numero_Radicacion"] != null ? Convert.ToInt64(Session["Numero_Radicacion"]) : 0;
                referencia.cod_persona = Convert.ToInt64(((DropDownList)gFila.FindControl("ddlQuienReferencia")).SelectedValue);
                referencia.numero_solicitud = numero_solicitud;
                referencia.estado = 0;

                lstReferencia.Add(referencia);
            }
        }

        return lstReferencia;
    }

    #endregion

    #region Guardar
    public bool GuardarCodeudores(ref string error)
    {
        List<codeudores> lstCodeudores = new List<codeudores>();
        int cont = 0;
        var errors = "";
        string orden = string.Empty;
        foreach (GridViewRow rFila in gvListaCodeudores.Rows)
        {
            Label codPersona = (Label)rFila.FindControl("lblCodPersona");
            string COD_CODEUDOR = codPersona.Text.Replace("&nbsp", "");
            Label lblOrdeRow = (Label)rFila.FindControl("lblOrdenRow");
            orden = lblOrdeRow.Text.ToString().Replace("&nbsp", "");
            if (COD_CODEUDOR != "0" && COD_CODEUDOR != "")
            {
                cont++;
                Label lblidentificacion = (Label)rFila.FindControl("lblidentificacion");
                if (string.IsNullOrEmpty(lblidentificacion.Text))
                {
                    error = "Error en la fila " + rFila.RowIndex;
                    return false;
                }

                Xpinn.FabricaCreditos.Entities.codeudores vCodeudores = new Xpinn.FabricaCreditos.Entities.codeudores();

                vCodeudores.codpersona = Convert.ToInt64(COD_CODEUDOR);
                vCodeudores.identificacion = lblidentificacion.Text;
                vCodeudores.numero_solicitud = Convert.ToInt64(datosSolicitud.numerosolicitud.ToString());
                Int64 num_Radica = 0;
                if (Session["Numero_Radicacion"] != null)
                    num_Radica = Convert.ToInt64(Session["Numero_Radicacion"].ToString());
                vCodeudores.numero_radicacion = num_Radica;

                // Validar datos del codeudor
                string sError = "";
                CodeudorServicio.ValidarCodeudor(vCodeudores, (Usuario)Session["usuario"], ref sError);
                if (sError.Trim() != "")
                {
                    error = sError.Substring(0, sError.IndexOf("ORA-"));
                    return false;
                }

                vCodeudores.idcodeud = 0;
                vCodeudores.tipo_codeudor = "C";
                vCodeudores.parentesco = 0;
                vCodeudores.opinion = "B";
                vCodeudores.responsabilidad = null;
                vCodeudores.orden = Convert.ToInt32(orden);
                lstCodeudores.Add(vCodeudores);
            }
        }
        CodeudorServicio.Crearcodeudores(lstCodeudores, (Usuario)Session["usuario"]);
        GuardarReferencias(ref errors);
        return true;
    }

    public bool GuardarReferencias(ref string errors)
    {

        /////////////////////////////////////////////////////////////////////////////////////////////
        // Guardar datos de referencias
        /////////////////////////////////////////////////////////////////////////////////////////////
        string error = string.Empty;
        List<Referncias> lstReferencia = LlenarListaDeGVReferencias(datosSolicitud.numerosolicitud, out error);
        // Si hay un error muestro y retorno
        if (!string.IsNullOrWhiteSpace(error))
        {
            errors = error;
            return false;
        }
        RefernciasService referenciaService = new RefernciasService();
        foreach (var referencia in lstReferencia)
        {
            referenciaService.CrearReferncias(referencia, (Usuario)Session["usuario"]);
        }
        return true;
    }
    #endregion

    #region Referencias


    protected void btnAgregarReferencia_Click(object sender, EventArgs e)
    {
        List<Referncias> lstReferencia = RecorresGrillaReferencias();

        lstReferencia.Insert(0, new Referncias() { tiporeferencia = 1, cod_persona_quien_referencia = _codPersona });
        var idSeleccionado = lstReferencia.Select(x => x.cod_persona_quien_referencia).ToArray();

        gvReferencias.DataSource = lstReferencia;
        gvReferencias.DataBind();

        LlenarDDLQuienReferenciaGVReferencias(((List<Persona1>)Session[usu.codusuario + "Codeudores"]), idSeleccionado);
    }


    protected void gvReferencia_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        List<Referncias> lstReferencia = RecorresGrillaReferencias();

        lstReferencia.RemoveAt(Convert.ToInt32(e.CommandArgument));
        var idSeleccionado = lstReferencia.Select(x => x.cod_persona_quien_referencia).ToArray();

        gvReferencias.DataSource = lstReferencia;
        gvReferencias.DataBind();

        LlenarDDLQuienReferenciaGVReferencias(((List<Persona1>)Session[usu.codusuario + "Codeudores"]), idSeleccionado);
    }

    protected void ddlTipoReferencia_SelectedIndexChanged(object sender, EventArgs e)
    {
        var ddlTipoReferenciaEvent = sender as DropDownListGrid;
        int rowIndex = Convert.ToInt32(ddlTipoReferenciaEvent.CommandArgument);

        var ddlParentesco = gvReferencias.Rows[rowIndex].FindControl("ddlParentesco") as DropDownList;

        var selectedValue = Convert.ToInt32(ddlTipoReferenciaEvent.SelectedValue);

        if (ddlTipoReferenciaEvent != null && selectedValue != (int)TipoReferencia.Familiar)
        {
            ddlParentesco.SelectedValue = "0";
            ddlParentesco.Enabled = false;
        }
        else
        {
            ddlParentesco.Enabled = true;
        }
    }


    // Es necesario este evento vacio para que pueda borrar la Row
    protected void gvReferencias_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }

    protected List<Referncias> ListarParentesco()
    {
        RefernciasService lineasServicio = new RefernciasService();
        List<Referncias> lstAtributos = lineasServicio.ListasDesplegables("Parentesco", (Usuario)Session["Usuario"]);

        return lstAtributos;
    }

    protected void InicializarReferencias()
    {
        Referncias[] lstAtributos = new Referncias[4]
        {
            new Referncias() { tiporeferencia = 1},
            new Referncias() { tiporeferencia = 1},
            new Referncias() { tiporeferencia = 1},
            new Referncias() { tiporeferencia = 1},
        };

        gvReferencias.DataSource = lstAtributos;
        gvReferencias.DataBind();
    }
    void AgregarOnchange()
    {
        Control ctrl = gvListaCodeudores.FooterRow.FindControl("txtidentificacion");
        Control ctrls = gvListaCodeudores.FooterRow.FindControl("btnNuevo");
        TextBox txtidentificacion = (TextBox)ctrl;
        txtidentificacion.Attributes.Add("onchange", "ValdiarCodeudor();");
        ImageButton btnNuevo = (ImageButton)ctrls;
        btnNuevo.Attributes.Add("onclick", "Recargar();");

    }
    #endregion

    protected void gvListaCodeudores_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //Finding label
            ImageButton btn = (ImageButton)e.Row.FindControl("btnDelete");
            btn.Attributes.Add("onclick", "Recargareli();");
        }
    }
}