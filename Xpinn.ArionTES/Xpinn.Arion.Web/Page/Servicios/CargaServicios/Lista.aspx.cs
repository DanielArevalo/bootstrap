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
using Xpinn.Servicios.Entities;
using Xpinn.Servicios.Services;
using System.IO;
using System.Text;
using System.Web.UI.HtmlControls;
using Xpinn.Aportes.Services;
using Xpinn.FabricaCreditos.Services;
using Xpinn.FabricaCreditos.Entities;
using Xpinn.Servicios.Entities;

partial class Lista : GlobalWeb
{
    PoblarListas Poblar = new PoblarListas();
    LineaServiciosServices LineaServicios = new LineaServiciosServices();
    AprobacionServiciosServices AproServicios = new AprobacionServiciosServices();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(AproServicios.CodigoProgramaCarga, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoCargar += btnCargar_Click;
            toolBar.MostrarGuardar(false);
            ctlMensaje.eventoClick += btnContinuarMen_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AproServicios.CodigoProgramaCarga, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                txtFecha.Text = DateTime.Now.ToShortDateString();
                pErrores.Visible = false;
                panelGrilla.Visible = false;
                cargarDropdown();
                Session["SERVICIOS"] = null;
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AproServicios.CodigoProgramaCarga, "Page_Load", ex);
        }
    }


    private void cargarDropdown()
    {
        Poblar.PoblarListaDesplegable("LINEASSERVICIOS", ddlLinea, (Usuario)Session["usuario"]);

        ddlFormatoFecha.Items.Insert(0, new ListItem("Seleccione un Item", "0"));
        ddlFormatoFecha.Items.Insert(1, new ListItem("dd/MM/yyyy", "dd/MM/yyyy"));
        ddlFormatoFecha.Items.Insert(2, new ListItem("yyyy/MM/dd", "yyyy/MM/dd"));
        ddlFormatoFecha.Items.Insert(3, new ListItem("MM/dd/yyyy", "MM/dd/yyyy"));
        ddlFormatoFecha.Items.Insert(4, new ListItem("ddMMyyyy", "ddMMyyyy"));
        ddlFormatoFecha.Items.Insert(5, new ListItem("yyyyMMdd", "yyyyMMdd"));
        ddlFormatoFecha.Items.Insert(6, new ListItem("MMddyyyy", "MMddyyyy"));
        ddlFormatoFecha.SelectedIndex = 0;
        ddlFormatoFecha.DataBind();
    }


    protected void btnCargar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        StreamReader strReader;

        if (ddlLinea.SelectedValue == "")
        {
            VerError("Seleccione la linea de servicio para realizar la carga");
            return;
        }
        if (ddlFormatoFecha.SelectedIndex == 0)
        {
            VerError("Seleccione la linea de servicio para realizar la carga");
            return;
        }

        if (flpArchivo.HasFile)
        {
            FileInfo fi = new FileInfo(flpArchivo.FileName);
            string ext = fi.Extension;
            if (ext != ".txt")
            {
                VerError("Ingrese archivos a cargar de formato .txt");
                return;
            }
            Stream stream = flpArchivo.FileContent;
            String readLine, error = "";
            int contador = 0;
            List<Servicio> lstCargaDatos = new List<Servicio>();
            List<Xpinn.Tesoreria.Entities.ErroresCarga> plstErrores = new List<Xpinn.Tesoreria.Entities.ErroresCarga>();

            try
            {
                using (strReader = new StreamReader(stream))
                {
                    while (strReader.Peek() >= 0)
                    {
                        contador++;
                        readLine = strReader.ReadLine();
                        String[] arrayLineas = readLine.Split(Convert.ToChar(13));
                        Cargar(arrayLineas[0].ToString(), ref lstCargaDatos, contador, ref error, ref plstErrores);
                        //contador = contador + 1;
                    }
                }

                panelGrilla.Visible = true;
                pErrores.Visible = false;
                gvInconsistencia.DataSource = null;
                lblTotalIncon.Visible = false;

                //ADICIONANDO LOS DATOS A LA GRID
                if (lstCargaDatos.Count > 0)
                {
                    Site toolBar = (Site)this.Master;
                    toolBar.MostrarGuardar(true);
                    int RowIndex = 0;
                    foreach (Servicio eServi in lstCargaDatos)
                    {
                        eServi.numero_servicio = RowIndex;
                        eServi.saldo = 0;
                        eServi.fecha_aprobacion = eServi.fecha_solicitud;
                        eServi.fecha_activacion = eServi.fecha_solicitud;
                        eServi.estado = "S";
                        eServi.fecha_proximo_pago = DateTime.MinValue;
                        RowIndex += 1;
                    }

                    gvLista.DataSource = lstCargaDatos;
                    gvLista.DataBind();
                    Session["SERVICIOS"] = lstCargaDatos;

                    //List<Servicio> lstDatos = new List<Servicio>();
                    //lstDatos = ObtenerListaServicio();
                    //gvLista.DataSource = lstDatos;
                    //gvLista.DataBind();
                    //Session["SERVICIOS"] = lstDatos;

                    CalcularTotal();
                }
                if (plstErrores.Count > 0)
                {
                    gvInconsistencia.DataSource = plstErrores;
                    gvInconsistencia.DataBind();
                    Session["INCONSISTENCIA"] = plstErrores;
                    pErrores.Visible = true;
                    cpeDemo.CollapsedText = "(Click Aqui para ver el Listado de errores...)";
                    cpeDemo.ExpandedText = "(Click Aqui para ocultar Listado de errores...)";
                    lblTotalIncon.Visible = true;
                    lblTotalIncon.Text = "<br/> Registros de inconsistencias encontrados " + plstErrores.Count.ToString();
                    lblMostrarDetalles.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    lblMostrarDetalles.ForeColor = System.Drawing.Color.FromArgb(0, 101, 255);
                }
            }
            catch (IOException ex)
            {
                VerError(ex.Message);
            }
        }
        else
        {
            VerError("Seleccione el Archivo a cargar");
        }
    }

    protected void Cargar(String lineFile, ref List<Servicio> lstCarga, int contador, ref string perror, ref List<Xpinn.Tesoreria.Entities.ErroresCarga> plstErrores)
    {
        perror = "";
        try
        {
            AfiliacionServices afiliacionServices = new AfiliacionServices();
            Persona1Service personaServices = new Persona1Service();
            PeriodicidadService periodicidadService = new PeriodicidadService();
            DateTimeHelper dateTimeHelper = new DateTimeHelper();
            string s = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
            /*
             Estructura de archivo: Fecha, Identificación, Plan, Num poliza, Fecha inicial vigencia, Fecha final vigencia, Valor total, Fecha primera cuota, Num Cuotas,
          vr cuota,Periodicidad, Forma de pago, Identificacion titular, Nombre titular, Codigo Empresa, Cod Destinacion
             */
            Servicio entidad = new Servicio();
            if (lineFile.Trim() != "")
            {
                String[] arrayline = lineFile.Split('|');

                string sformato_fecha = ddlFormatoFecha.SelectedIndex != 0 ? ddlFormatoFecha.SelectedValue : "dd/MM/yyyy";
                try //FECHA SOLICITUD
                {
                    if (!string.IsNullOrWhiteSpace(arrayline[0]))
                    {
                        entidad.fecha_solicitud = DateTime.ParseExact(arrayline[0].ToString().Trim(), sformato_fecha, null);
                    }
                    else
                    {
                        RegistrarError(contador, arrayline[0].ToString(), "Fecha de solicitud vacio y/o invalida!.", lineFile.ToString(), ref plstErrores);
                    }
                }
                catch (Exception ex)
                {
                    RegistrarError(contador, arrayline[0].ToString(), ex.Message, lineFile.ToString(), ref plstErrores);
                    return;
                }
                try //IDENTIFICACION
                {
                    string Identificacion = arrayline[1].ToString().Trim();
                    //Consultando el codigo de la persona
                    Xpinn.Contabilidad.Entities.DetalleComprobante vData = new Xpinn.Contabilidad.Entities.DetalleComprobante();
                    Xpinn.Contabilidad.Data.ComprobanteData DAComprobante = new Xpinn.Contabilidad.Data.ComprobanteData();
                    vData = DAComprobante.Identificacion_RETORNA_CodPersona(Identificacion, (Usuario)Session["usuario"]);


                    if (vData.tercero != null && vData.tercero != 0 && !string.IsNullOrWhiteSpace(Identificacion))
                    {
                        entidad.identificacion = Identificacion;
                        entidad.cod_persona = Convert.ToInt64(vData.tercero);
                    }
                    else
                    {
                        RegistrarError(contador, arrayline[1].ToString(), "La identificación ingresada no pertenece a un afiliado", lineFile.ToString(), ref plstErrores);
                        return;
                    }

                    string estado = afiliacionServices.ConsultarEstadoAfiliacion(entidad.cod_persona, Usuario);
                    LineaServicios vDetalle = LineaServicios.ConsultarLineaSERVICIO(ddlLinea.SelectedValue, Usuario);
                    if (vDetalle.maneja_retirados != 1)
                    {
                        if (estado != "A")
                        {
                            RegistrarError(contador, arrayline[1].ToString(), "La persona afiliada esta en estado retirado!.", lineFile.ToString(), ref plstErrores);
                            return;
                        }
                    }
                    
                }
                catch (Exception ex)
                {
                    entidad.identificacion = arrayline[1].ToString();
                    RegistrarError(contador, arrayline[1].ToString(), ex.Message, lineFile.ToString(), ref plstErrores);
                    return;
                }

                try //PLAN SERVICIO
                {
                    if (arrayline[2].ToString().Trim() != "")
                        entidad.cod_plan_servicio = arrayline[2].ToString().Trim();
                    else
                        entidad.cod_plan_servicio = null;
                }
                catch (Exception ex)
                {
                    entidad.cod_plan_servicio = arrayline[2].ToString().Trim();
                    RegistrarError(contador, arrayline[2].ToString().Trim(), ex.Message, lineFile.ToString(), ref plstErrores);
                }
                try //NUM POLIZA
                {
                    if (arrayline[3].ToString().Trim() != "")
                        entidad.num_poliza = arrayline[3].ToString().Trim();
                    else
                        entidad.num_poliza = null;
                }
                catch (Exception ex)
                {
                    entidad.num_poliza = arrayline[3].ToString().Trim();
                    RegistrarError(contador, arrayline[3].ToString().Trim(), ex.Message, lineFile.ToString(), ref plstErrores);
                }
                try //FECHA INICIAL VIGENCIA
                {
                    if (arrayline[4].ToString().Trim() != "")
                        entidad.fecha_inicio_vigencia = DateTime.ParseExact(arrayline[4].ToString().Trim(), sformato_fecha, null);
                    else
                    {
                        RegistrarError(contador, arrayline[4].ToString().Trim(), "Fecha de inicio de vigencia invalida!.", lineFile.ToString(), ref plstErrores);
                        return;
                    }
                }
                catch (Exception ex)
                {
                    RegistrarError(contador, arrayline[4].ToString().Trim(), ex.Message, lineFile.ToString(), ref plstErrores);
                    return;
                }
                try //PERIODICIDAD
                {
                    if (arrayline[10].ToString().Trim() != "")
                        entidad.cod_periodicidad = Convert.ToInt32(arrayline[10].ToString().Trim().Replace(gSeparadorMiles, ""));
                    else
                    {
                        RegistrarError(contador, arrayline[10].ToString().Trim(), "Periodicidad Invalida!.", lineFile.ToString(), ref plstErrores);
                        return;
                    }
                }
                catch (Exception ex)
                {
                    RegistrarError(contador, arrayline[10].ToString().Trim(), ex.Message, lineFile.ToString(), ref plstErrores);
                }
                try //FECHA PRIMERA CUOTA
                {
                    if (arrayline[7].ToString().Trim() != "")
                        entidad.fecha_primera_cuota = DateTime.ParseExact(arrayline[7].ToString().Trim(), sformato_fecha, null);
                    else
                    {
                        RegistrarError(contador, arrayline[7].ToString().Trim(), "Fecha primera cuota invalida!.", lineFile.ToString(), ref plstErrores);
                        return;
                    }
                }
                catch (Exception ex)
                {
                    RegistrarError(contador, arrayline[7].ToString().Trim(), ex.Message, lineFile.ToString(), ref plstErrores);
                }
                try //NUMERO CUOTAS
                {
                    if (!string.IsNullOrWhiteSpace(arrayline[8]))
                    {
                        entidad.numero_cuotas = Convert.ToInt32(arrayline[8].Trim().Replace(gSeparadorMiles, ""));
                    }
                    else
                    {
                        RegistrarError(contador, arrayline[8].ToString().Trim(), "Numero de cuotas invalido!.", lineFile.ToString(), ref plstErrores);
                    }
                }
                catch (Exception ex)
                {
                    entidad.numero_cuotas = 0;
                    RegistrarError(contador, arrayline[8].ToString().Trim(), ex.Message, lineFile.ToString(), ref plstErrores);
                }
                try //FECHA FINAL VIGENCIA
                {
                    if (arrayline[5].ToString().Trim() != "")
                        entidad.fecha_final_vigencia = DateTime.ParseExact(arrayline[5].ToString().Trim(), sformato_fecha, null);
                    else
                    {
                        Periodicidad periodicidad = periodicidadService.ConsultarPeriodicidad(entidad.cod_periodicidad, Usuario);

                        int numeroCuotas = entidad.numero_cuotas.Value - 1;
                        DateTime fechaPrimeraCuota = entidad.fecha_primera_cuota.Value;
                        entidad.fecha_final_vigencia = dateTimeHelper.SumarDiasSegunTipoCalendario(fechaPrimeraCuota, Convert.ToInt32(Math.Round(periodicidad.numero_dias * entidad.numero_cuotas.Value)), Convert.ToInt32(periodicidad.tipo_calendario));
                    }
                }
                catch (Exception ex)
                {
                    RegistrarError(contador, arrayline[5].ToString().Trim(), ex.Message, lineFile.ToString(), ref plstErrores);
                }
                try //VALOR TOTAL
                {
                    if (arrayline[6].ToString().Trim() != "")
                    {
                        decimal pVrTotal = Convert.ToDecimal(arrayline[6].ToString().Trim().Replace(gSeparadorMiles, ""));
                        if (pVrTotal < 0)
                        {
                            RegistrarError(contador, arrayline[6].ToString().Trim(), "El valor no puede ser negativo!.", lineFile.ToString(), ref plstErrores);
                            return;
                        } 
                        string str = pVrTotal.ToString();
                        int posDec = 0;
                        if (s == ".")
                            str = str.Replace(",", "");
                        else
                        {
                            str = str.Replace(".", "");
                            str = str.Replace(",", ".");
                        }
                        posDec = s == "." ? str.IndexOf(",") : str.IndexOf(".");
                        if (posDec > 0)
                        {
                            RegistrarError(contador, arrayline[6].ToString().Trim(), "Ingrese un valor sin decimales!.", lineFile.ToString(), ref plstErrores);
                            return;
                        }
                        entidad.valor_total = pVrTotal;
                    }
                    else
                    {
                        RegistrarError(contador, arrayline[6].ToString().Trim(), "Valor total solicitado invalido!.", lineFile.ToString(), ref plstErrores);
                    }
                }
                catch (Exception ex)
                {
                    entidad.valor_total = 0;
                    RegistrarError(contador, arrayline[6].ToString().Trim(), ex.Message, lineFile.ToString(), ref plstErrores);
                }
                try //VALOR CUOTA
                {
                    entidad.valor_cuota = Convert.ToDecimal(arrayline[9].ToString().Trim().Replace(gSeparadorMiles, ""));
                }
                catch (Exception ex)
                {
                    entidad.valor_cuota = 0;
                    RegistrarError(contador, arrayline[9].ToString().Trim(), ex.Message, lineFile.ToString(), ref plstErrores);
                }
                try //FORMA PAGO
                {
                    entidad.forma_pago = arrayline[11].ToString().Trim();
                }
                catch (Exception ex)
                {
                    RegistrarError(contador, arrayline[11].ToString().Trim(), ex.Message, lineFile.ToString(), ref plstErrores);
                }
                try //IDENTIFICACION TITULAR
                {
                    if (arrayline[12].ToString().Trim() != "")
                        entidad.identificacion_titular = arrayline[12].ToString().Trim();
                    else
                        entidad.identificacion_titular = null;
                }
                catch (Exception ex)
                {
                    RegistrarError(contador, arrayline[12].ToString().Trim(), ex.Message, lineFile.ToString(), ref plstErrores);
                }
                try //NOMBRE TITULAR
                {
                    if (arrayline[13].ToString().Trim() != "")
                        entidad.nombre_titular = arrayline[13].ToString().Trim();
                    else
                        entidad.nombre_titular = null;
                }
                catch (Exception ex)
                {
                    RegistrarError(contador, arrayline[13].ToString().Trim(), ex.Message, lineFile.ToString(), ref plstErrores);
                }
                try //Codigo Empresa
                {
                    TipoFormaPago formapago = entidad.forma_pago.ToEnum<TipoFormaPago>();
                    if (arrayline[14].ToString().Trim() != "" && formapago == TipoFormaPago.Nomina)
                        entidad.cod_empresa = Convert.ToInt64(arrayline[14].ToString().Trim());
                    else if (formapago == TipoFormaPago.Nomina)
                    {
                        string Identificacion = arrayline[1].ToString().Trim();

                        // Agarro la primera pagaduria que encuentre de esa persona
                        long cod_empresa;
                        if (entidad.cod_persona == 0)
                            cod_empresa = personaServices.ConsultarCodigoEmpresaPagaduria(Identificacion, Usuario);
                        else
                            cod_empresa = personaServices.ConsultarCodigoEmpresaPagaduria(entidad.cod_persona, Usuario);

                        // Si no tengo un cod_empresa valido entonces lo cambio a caja
                        if (cod_empresa <= 0)
                        {
                            entidad.forma_pago = "1";
                        }
                        else
                        {
                            entidad.cod_empresa = cod_empresa;
                        }
                    }
                }
                catch (Exception ex)
                {
                    RegistrarError(contador, arrayline[14].ToString().Trim(), ex.Message, lineFile.ToString(), ref plstErrores);
                }

                //cod_destinacion 
                try
                {

                    if (arrayline[15].ToString().Trim() != "")
                    {
                        LineaServiciosServices serviciolin = new LineaServiciosServices();
                        LineaServ_Destinacion destinacion = new LineaServ_Destinacion();
                        destinacion = serviciolin.consultaDestinacionservicio(arrayline[15].ToString().Trim(), Convert.ToString(ddlLinea.SelectedValue), Usuario);

                        if (destinacion.cod_destino == Convert.ToInt32(arrayline[15].ToString().Trim()) && destinacion.cod_linea_servicio != 0)
                        {
                            entidad.cod_destino = Convert.ToInt64(arrayline[15].ToString().Trim());
                        }
                        else
                        {
                            RegistrarError(contador, arrayline[15].ToString(), "Codigo de destinación no pertenece a la linea asignada!.", lineFile.ToString(), ref plstErrores);
                            return;
                        }

                    }
                    else
                    {
                        RegistrarError(contador, arrayline[15].ToString(), "Codigo de destinación del servicio no debe estar vacio!.", lineFile.ToString(), ref plstErrores);
                        return;
                    }
                }
                catch (Exception ex)
                {
                    RegistrarError(contador, arrayline[15].ToString().Trim(), ex.Message, lineFile.ToString(), ref plstErrores);
                }

                //ADICIONANDO SERVICIO                

                lstCarga.Add(entidad);
            }
        }
        catch (Exception ex)
        {
            perror = ex.Message;
        }
    }


    protected void CalcularTotal()
    {
        decimal Total = 0;
        int cont = 0;
        for (int i = 0; i < gvLista.PageCount; i++)
        {
            gvLista.PageIndex = i;
            gvLista.DataBind();
            foreach (GridViewRow rfila in gvLista.Rows)
            {
                cont++;
                rfila.Cells[8].Text = rfila.Cells[8].Text != "&nbsp;" ? rfila.Cells[8].Text : "0";
                Total += Convert.ToDecimal(rfila.Cells[8].Text);
            }
        }
        gvLista.PageIndex = 0;
        gvLista.DataBind();
        if (cont != 0)
        {
            String msj = "";
            if (cont == 1)
                msj = "1 Servicio";
            else
                msj = cont + " Servicios";
            lblTotalRegs.Visible = true;
            lblTotalRegs.Text = "<br/>" + msj + " a cargar por un valor total de " + Total.ToString("c0");
        }
        else
        {
            lblTotalRegs.Visible = false;
        }
    }


    protected List<Servicio> ObtenerListaServicio()
    {
        List<Servicio> lstServicios = new List<Servicio>();
        List<Servicio> lista = new List<Servicio>();

        for (int i = 0; i < gvLista.PageCount; i++)
        {
            gvLista.PageIndex = i;
            gvLista.DataBind();
            foreach (GridViewRow rfila in gvLista.Rows)
            {
                Servicio eServi = new Servicio();

                eServi.numero_servicio = rfila.RowIndex;

                if (rfila.Cells[1].Text != "&nbsp;") //FECHA SOLICITUD
                    eServi.fecha_solicitud = Convert.ToDateTime(rfila.Cells[1].Text);

                String cod_persona = gvLista.DataKeys[rfila.RowIndex].Values[0].ToString();
                if (cod_persona != "&nbsp;" && cod_persona != "")
                    eServi.cod_persona = Convert.ToInt64(cod_persona);

                if (rfila.Cells[3].Text != "&nbsp;") //IDENTIFICACIOn
                    eServi.identificacion = rfila.Cells[3].Text.Trim();

                if (rfila.Cells[4].Text != "&nbsp;") //PLAN SERVICIO
                    eServi.cod_plan_servicio = rfila.Cells[4].Text.Trim();

                if (rfila.Cells[5].Text != "&nbsp;") //NUM POLIZA
                    eServi.num_poliza = rfila.Cells[5].Text.Trim();

                if (rfila.Cells[6].Text != "&nbsp;") //FECHA INICIAL VIGENCIA
                    eServi.fecha_inicio_vigencia = Convert.ToDateTime(rfila.Cells[6].Text.Trim());

                if (rfila.Cells[7].Text != "&nbsp;") //FECHA FINAL VIGENCIA
                    eServi.fecha_final_vigencia = Convert.ToDateTime(rfila.Cells[7].Text.Trim());

                if (rfila.Cells[8].Text != "&nbsp;") //VALOR TOTAL
                {
                    eServi.valor_total = Convert.ToDecimal(rfila.Cells[8].Text.Trim().Replace(gSeparadorMiles, ""));
                    eServi.saldo = Convert.ToDecimal(rfila.Cells[8].Text.Trim().Replace(gSeparadorMiles, ""));
                }
                if (rfila.Cells[9].Text != "&nbsp;") //FECHA PRIMERA CUOTA
                    eServi.fecha_primera_cuota = Convert.ToDateTime(rfila.Cells[9].Text.Trim());
                else
                    eServi.fecha_primera_cuota = DateTime.MinValue;

                if (rfila.Cells[10].Text != "&nbsp;") //NUMERO CUOTAS
                {
                    eServi.numero_cuotas = Convert.ToInt32(rfila.Cells[10].Text.Trim());
                    eServi.cuotas_pendientes = Convert.ToInt32(rfila.Cells[10].Text.Trim());
                }

                if (rfila.Cells[11].Text != "&nbsp;") //VALOR CUOTA
                    eServi.valor_cuota = Convert.ToDecimal(rfila.Cells[11].Text.Trim().Replace(gSeparadorMiles, ""));

                if (rfila.Cells[12].Text != "&nbsp;") //PERIODICIDAD
                    eServi.cod_periodicidad = Convert.ToInt32(rfila.Cells[12].Text.Trim());

                if (rfila.Cells[13].Text != "&nbsp;") //FORMA DE PAGO
                    eServi.forma_pago = rfila.Cells[13].Text.Trim();

                if (rfila.Cells[14].Text != "&nbsp;") //IDENTIFICACION TITULAR
                    eServi.identificacion_titular = rfila.Cells[14].Text.Trim();

                if (rfila.Cells[15].Text != "&nbsp;") //NOMBRE TITULAR
                    eServi.nombre_titular = rfila.Cells[15].Text.Trim();

                if (!string.IsNullOrWhiteSpace(rfila.Cells[17].Text) && rfila.Cells[17].Text != "&nbsp;") //Codigo Empresa
                    eServi.cod_empresa = Convert.ToInt64(rfila.Cells[17].Text.Trim());

                if (rfila.Cells[18].Text != "&nbsp;") // Codigo destinación
                    eServi.cod_destino = Convert.ToInt64(rfila.Cells[18].Text.Trim());

                eServi.fecha_aprobacion = Convert.ToDateTime(rfila.Cells[1].Text); //DateTime.Now; Se cambio solicitado por el tiquet #31
                eServi.fecha_activacion = Convert.ToDateTime(rfila.Cells[1].Text); //DateTime.Now; Se cambio solicitado por el tiquet #31
                eServi.estado = "S"; // Si lo cambias no genera novedades luego

                //NULO
                eServi.fecha_proximo_pago = DateTime.MinValue;

                lista.Add(eServi);

                if (eServi.fecha_solicitud != DateTime.MinValue && eServi.valor_total != 0)
                {
                    lstServicios.Add(eServi);
                }
            }
        }
        Session["SERVICIOS"] = lista;
        return lstServicios;
    }

    protected void RegistrarError(int pNumeroLinea, string pRegistro, string pError, string pDato, ref List<Xpinn.Tesoreria.Entities.ErroresCarga> plstErrores)
    {
        if (pNumeroLinea == -1)
        {
            plstErrores.Clear();
            return;
        }
        Xpinn.Tesoreria.Entities.ErroresCarga registro = new Xpinn.Tesoreria.Entities.ErroresCarga();
        registro.numero_registro = pNumeroLinea.ToString();
        registro.datos = pDato;
        registro.error = " Campo No.:" + pRegistro + " Error:" + pError;
        plstErrores.Add(registro);
    }


    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        if (ValidarDatos())
            ctlMensaje.MostrarMensaje("Desea realizar la grabación de los servicios cargados ?");
    }

    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        List<Servicio> lstDatos = new List<Servicio>();
        if (Session["SERVICIOS"] != null)
            lstDatos = (List<Servicio>)Session["SERVICIOS"];
        else
            lstDatos = ObtenerListaServicio();

        Usuario pUsuario = new Usuario();
        pUsuario = (Usuario)Session["usuario"];

        //DATOS DE LA OPERACION
        Xpinn.Tesoreria.Entities.Operacion vOpe = new Xpinn.Tesoreria.Entities.Operacion();
        vOpe.cod_ope = 0;
        vOpe.tipo_ope = 110;
        vOpe.cod_caja = 0;
        vOpe.cod_cajero = 0;
        vOpe.observacion = "Operacion-Carga de Servicios";
        vOpe.cod_proceso = null;
        vOpe.fecha_oper = Convert.ToDateTime(txtFecha.Text);
        vOpe.fecha_calc = DateTime.Now;
        vOpe.cod_ofi = pUsuario.cod_oficina;

        LineaServiciosServices BOLineaServ = new LineaServiciosServices();
        LineaServicios pLineaServ = BOLineaServ.ConsultarLineaSERVICIO(ddlLinea.SelectedValue, Usuario);

        Int64 COD_OPE = 0;
        //PASAR DATOS A LA CAPA SERVICE

        AproServicios.RegistrarServiciosCargados(ref COD_OPE, vOpe, ddlLinea.SelectedValue, lstDatos, (Usuario)Session["usuario"]);

        //GENERAR EL COMPROBANTE
        if (COD_OPE != 0)
        {
            Xpinn.Contabilidad.Services.ComprobanteService ComprobanteServicio = new Xpinn.Contabilidad.Services.ComprobanteService();
            Session[ComprobanteServicio.CodigoPrograma + ".cod_ope"] = COD_OPE;
            Session[ComprobanteServicio.CodigoPrograma + ".tipo_ope"] = 110;
            Session[ComprobanteServicio.CodigoPrograma + ".fecha_ope"] = DateTime.ParseExact(txtFecha.Text, gFormatoFecha, null);
            long pCodProveedor = pLineaServ.cod_proveedor != null ? Convert.ToInt64(pLineaServ.cod_proveedor) : pUsuario.codusuario;
            Session[ComprobanteServicio.CodigoPrograma + ".cod_persona"] = pCodProveedor;
            Navegar("../../Contabilidad/Comprobante/Nuevo.aspx");
        }

        Site toolBar = (Site)this.Master;
        toolBar.MostrarGuardar(false);
        toolBar.MostrarCargar(false);

    }


    protected void btnExportar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        if (gvInconsistencia.Rows.Count > 0 && Session["INCONSISTENCIA"] != null)
        {
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            Page pagina = new Page();
            dynamic form = new HtmlForm();
            gvInconsistencia.AllowPaging = false;
            gvInconsistencia.DataSource = Session["INCONSISTENCIA"];
            gvInconsistencia.DataBind();
            gvInconsistencia.EnableViewState = false;
            pagina.EnableEventValidation = false;
            pagina.DesignerInitialize();
            pagina.Controls.Add(form);
            form.Controls.Add(gvInconsistencia);
            pagina.RenderControl(htw);
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=Inconsistencias.xls");
            Response.Charset = "UTF-8";
            Response.ContentEncoding = Encoding.Default;
            Response.Write(sb.ToString());
            Response.End();
        }
        else
        {
            VerError("No existen datos, genere la consulta");
        }
    }

    protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            ObtenerListaServicio();
            int conseID = Convert.ToInt32(gvLista.DataKeys[e.RowIndex].Values[1].ToString());

            List<Servicio> LstDeta;
            LstDeta = (List<Servicio>)Session["SERVICIOS"];

            try
            {
                foreach (Servicio Deta in LstDeta)
                {
                    if (Deta.numero_servicio == conseID)
                    {
                        LstDeta.Remove(Deta);
                        break;
                    }
                }
            }
            catch (Xpinn.Util.ExceptionBusiness ex)
            {
                VerError(ex.Message);
            }

            gvLista.DataSourceID = null;
            gvLista.DataBind();

            gvLista.DataSource = LstDeta;
            gvLista.DataBind();

            Site toolBar = (Site)this.Master;
            if (LstDeta.Count == 0)
            {
                toolBar.MostrarGuardar(false);
                panelGrilla.Visible = false;
            }
            else
            {
                panelGrilla.Visible = true;
                toolBar.MostrarGuardar(true);
            }
            CalcularTotal();
            Session["SERVICIOS"] = LstDeta;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AproServicios.CodigoProgramaCarga, "gvLista_RowDeleting", ex);
        }
    }


    protected Boolean ValidarDatos()
    {
        Xpinn.Contabilidad.Services.ComprobanteService ComprobanteServicio = new Xpinn.Contabilidad.Services.ComprobanteService();
        if (txtFecha.Text == "")
        {
            VerError("Ingrese la fecha de carga");
            return false;
        }
        DateTime dtUltCierre;
        try
        {
            dtUltCierre = Convert.ToDateTime(ComprobanteServicio.Consultafecha((Usuario)Session["Usuario"]));
        }
        catch
        {
            VerError("No se encontro la fecha del último cierre contable");
            return false;
        }
        if (Convert.ToDateTime(txtFecha.Text) <= dtUltCierre)
        {
            VerError("No puede cargar servicios en períodos ya cerrados. Fecha Ultimo Cierre: " + dtUltCierre.ToShortDateString());
            return false;
        }

        if (ddlLinea.SelectedIndex == 0)
        {
            VerError("Seleccione la linea de servicio para realizar la grabación");
            return false;
        }

        List<Servicio> LstDeta = new List<Servicio>();

        if (Session["SERVICIOS"] != null)
            LstDeta = (List<Servicio>)Session["SERVICIOS"];
        else
            LstDeta = ObtenerListaServicio();
        if (LstDeta.Count == 0 || LstDeta == null)
        {
            VerError("No existen Datos cargado para realizar la grabación");
            return false;
        }

        List<Servicio> lstDatos = new List<Servicio>(); // PLAN SERVICIO POR LINEA
        lstDatos = AproServicios.CargarPlanXLinea(Convert.ToInt32(ddlLinea.SelectedValue), (Usuario)Session["usuario"]);

        int cont = 0;
        foreach (Servicio pServi in LstDeta)
        {
            cont++;
            //Validar cada fila de datos ingresados
            if (pServi.identificacion != null)
            {
                Xpinn.Contabilidad.Entities.DetalleComprobante vData = new Xpinn.Contabilidad.Entities.DetalleComprobante();
                Xpinn.Contabilidad.Data.ComprobanteData DAComprobante = new Xpinn.Contabilidad.Data.ComprobanteData();
                vData = DAComprobante.Identificacion_RETORNA_CodPersona(pServi.identificacion, (Usuario)Session["usuario"]);

                if (vData.tercero == null || vData.tercero == 0)
                {
                    VerError("Error en la Fila : " + cont.ToString() + " La identificación ingresada no pertenece a un Afiliado valido");
                    return false;
                }
            }
            else
            {
                VerError("No existe la identificación");
                return false;
            }

            int val = 0;
            foreach (Servicio Plan in lstDatos)
            {
                if (Plan.cod_plan_servicio == pServi.cod_plan_servicio)
                {
                    val = 1;
                }
            }
            if (lstDatos.Count > 0 && pServi.cod_plan_servicio != null)
            {
                if (val == 0)
                {
                    VerError("Error en la Fila : " + cont.ToString() + " El código de plan no existe para la linea seleccionada (" + ddlLinea.SelectedItem.Text + ")");
                    return false;
                }
            }
            if (pServi.valor_total == 0)
            {
                VerError("Error en la Fila : " + cont.ToString() + " Ingrese el valor total a registrar");
                return false;
            }
            if (pServi.forma_pago != "1" && pServi.forma_pago != "2")
            {
                VerError("Error en la Fila : " + cont.ToString() + " El código de la forma de pago es incorrecto, Codigos Validos ( CAJA => 1 , NOMINA => 2)");
                return false;
            }
            if (pServi.fecha_primera_cuota == DateTime.MinValue)
            {
                VerError("Error en la Fila : " + cont.ToString() + " La fecha de primera cuota es incorrecta!.");
                return false;
            }
            if (pServi.forma_pago == "2" && (!pServi.cod_empresa.HasValue || pServi.cod_empresa <= 0))
            {
                VerError("Error en la Fila : " + cont.ToString() + " Debes proporcionar un codigo de empresa valido si la forma de pago es nomina!.");
                return false;
            }
        }

        return true;
    }


    protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvLista.PageIndex = e.NewPageIndex;
        List<Servicio> LstDeta;
        LstDeta = (List<Servicio>)Session["SERVICIOS"];
        gvLista.DataSource = LstDeta;
        gvLista.DataBind();
    }

    protected void gvLista_PageIndexChanged(object sender, EventArgs e)
    {

    }
}