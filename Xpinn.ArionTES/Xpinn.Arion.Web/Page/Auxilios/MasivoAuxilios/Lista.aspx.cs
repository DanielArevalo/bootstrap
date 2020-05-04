using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;

using System.IO;
using System.Text;
using System.Web.UI.HtmlControls;
using Xpinn.Auxilios.Services;
using Xpinn.Auxilios.Entities;
using Xpinn.Aportes.Services;
using Xpinn.FabricaCreditos.Entities;
using Xpinn.FabricaCreditos.Services;

public partial class Lista : GlobalWeb
{
    PoblarListas Poblar = new PoblarListas();
    AprobacionAuxilioServices AproAuxilios = new AprobacionAuxilioServices();
    Xpinn.Auxilios.Services.LineaAuxilioServices LineaAux = new Xpinn.Auxilios.Services.LineaAuxilioServices();
    private Xpinn.Asesores.Services.CreditosService RecaudosMasivosServicio = new Xpinn.Asesores.Services.CreditosService();
    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(AproAuxilios.CodigoPrograma, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoCargar += btnCargar_Click;
            toolBar.MostrarGuardar(false);
            ctlMensaje.eventoClick += btnContinuarMen_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AproAuxilios.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                pErrores.Visible = false;
                panelGrilla.Visible = false;
                Session["AUXILIOS"] = null;
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AproAuxilios.CodigoPrograma, "Page_Load", ex);
        }
    }





    protected void btnCargar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        StreamReader strReader;

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
            List<SolicitudAuxilio> lstCargaDatos = new List<SolicitudAuxilio>();
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
                    foreach (SolicitudAuxilio eAux in lstCargaDatos)
                    {
                        eAux.numero_auxilio = RowIndex;
                        eAux.fecha_aprobacion = eAux.fecha_solicitud;
                        eAux.fecha_desembolso = eAux.fecha_solicitud;
                        eAux.estado = "D";
                        RowIndex += 1;
                    }

                    gvLista.DataSource = lstCargaDatos;
                    gvLista.DataBind();
                    Session["AUXILIOS"] = lstCargaDatos;
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

    protected void Cargar(String lineFile, ref List<SolicitudAuxilio> lstCarga, int contador, ref string perror, ref List<Xpinn.Tesoreria.Entities.ErroresCarga> plstErrores)
    {
        perror = "";
        try
        {
            LineaAuxilio vDetalle = new LineaAuxilio();
            
            AfiliacionServices afiliacionServices = new AfiliacionServices();
            DateTimeHelper dateTimeHelper = new DateTimeHelper();
            SolicitudAuxilio entidad = new SolicitudAuxilio();
            if (lineFile.Trim() != "")
            {
                String[] arrayline = lineFile.Split('|');


                try //FECHA SOLICITUD
                {
                    if (!string.IsNullOrWhiteSpace(arrayline[0]))
                    {
                        entidad.fecha_solicitud = DateTime.ParseExact(arrayline[0].ToString().Trim(), "dd/MM/yyyy", null);
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

                    if (estado != "A")
                    {
                        RegistrarError(contador, arrayline[1].ToString(), "La persona afiliada esta en estado retirado!.", lineFile.ToString(), ref plstErrores);
                        return;
                    }


                    if (entidad.cod_persona != 0)
                    {

                        List<ReporteAuxilio> lstConsulta = new List<ReporteAuxilio>();
                        ReporteAuxilioService SoliAuxilios = new ReporteAuxilioService();
                        PeriodicidadService Perio = new PeriodicidadService();


                        String filtros = String.Empty;
                        filtros += " and auxilios.estado ='D'";

                        if (entidad.identificacion != "" || entidad.identificacion != null)
                        {
                            filtros += " and persona.identificacion= '" + entidad.identificacion + "'";
                        }

                        if (arrayline[2].ToString().Trim() != "")
                        {
                            filtros += " and LINEASAUXILIOS.cod_linea_auxilio= '" + arrayline[2].ToString().Trim() + "'";
                        }






                        lstConsulta = SoliAuxilios.ListarAuxilio(filtros, DateTime.MinValue, DateTime.MinValue, (Usuario)Session["usuario"]);
                        if (lstConsulta.Count > 0)
                        {
                            foreach (var item in lstConsulta)
                            {
                                if (entidad.fecha_solicitud < item.fecha_proxima_solicitud)
                                {
                                    RegistrarError(contador, arrayline[1].ToString(), "La persona " + entidad.identificacion + " no puede solicitar auxilios por esta línea, hasta el día:  " + item.fecha_proxima_solicitud.ToShortDateString(), lineFile.ToString(), ref plstErrores);
                                    return;
                                }
                            }
                        }

                        filtros = "";
                        if (entidad.identificacion != "" || entidad.identificacion != null)
                        {
                            filtros += " and p.identificacion = '" + entidad.identificacion + "'";
                        }

                        LineaAuxilio vDatosLinea = new LineaAuxilio();
                        //vDatosLinea = LineaAux.ConsultarLineaAUXILIO(filtros, (Usuario)Session["usuario"]);
                       
                            List<Xpinn.Asesores.Entities.PersonaMora> lstConsultas = new List<Xpinn.Asesores.Entities.PersonaMora>();
                            lstConsultas = RecaudosMasivosServicio.ListarPersonasMora(filtros, (Usuario)Session["usuario"]);
                        if (vDatosLinea.permite_mora != 1)
                        {
                            if (lstConsultas.Count > 0)
                            {

                                RegistrarError(contador, arrayline[1].ToString(), "Esta " + entidad.identificacion + " no puede solicitar auxilios ya que se encuentra en mora.", lineFile.ToString(), ref plstErrores);
                                return ;

                            }
                        }
                        return ;
                    }
                   

                }
                

                catch (Exception ex)
                {
                    entidad.identificacion = arrayline[1].ToString();
                    RegistrarError(contador, arrayline[1].ToString(), ex.Message, lineFile.ToString(), ref plstErrores);
                    return;
                }

                try //LINEA AUXILIO
                {
                    if (arrayline[2].ToString().Trim() != "")
                    {
                        entidad.cod_linea_auxilio = arrayline[2].ToString().Trim();
                        vDetalle = LineaAux.ConsultarLineaAUXILIO(arrayline[2].ToString().Trim(), (Usuario)Session["usuario"]);
                        if (vDetalle.cod_linea_auxilio == "" || vDetalle.cod_linea_auxilio == "0" || vDetalle.cod_linea_auxilio == null)
                        {
                            RegistrarError(contador, arrayline[2].ToString(), "La Linea " + arrayline[2].ToString().Trim() + " no existe ", lineFile.ToString(), ref plstErrores);
                            return;
                        }
                    }

                    else
                        entidad.cod_linea_auxilio = null;
                }
                catch (Exception ex)
                {
                    entidad.cod_linea_auxilio = arrayline[2].ToString().Trim();
                    RegistrarError(contador, arrayline[2].ToString().Trim(), ex.Message, lineFile.ToString(), ref plstErrores);
                }

                try //VALOR SOLICITADO
                {
                    if (arrayline[3].ToString().Trim() != "")
                    {
                        entidad.valor_solicitado = Convert.ToDecimal(arrayline[3].ToString().Trim().Replace(gSeparadorMiles, ""));

                        if (entidad.valor_solicitado < vDetalle.monto_minimo && entidad.valor_solicitado > vDetalle.monto_maximo)
                        {
                            RegistrarError(contador, arrayline[3].ToString(), "El valor solicitado" + arrayline[3].ToString().Trim() + " no esta en los limites de los valores de la linea de auxilio", lineFile.ToString(), ref plstErrores);
                            return;
                        }
                    }
                    else
                        entidad.valor_solicitado = 0;
                }
                catch (Exception ex)
                {
                    entidad.valor_solicitado = Convert.ToDecimal(arrayline[3].ToString().Trim().Replace(gSeparadorMiles, ""));
                    RegistrarError(contador, arrayline[3].ToString().Trim(), ex.Message, lineFile.ToString(), ref plstErrores);
                }

                //ADICIONANDO AUXILIO                

                lstCarga.Add(entidad);
            }
        }
        catch (Exception ex)
        {
            perror = ex.Message;
        }
    }



    protected List<SolicitudAuxilio> ObtenerListaAuxilios()
    {
        List<SolicitudAuxilio> lstAuxilios = new List<SolicitudAuxilio>();
        List<SolicitudAuxilio> lista = new List<SolicitudAuxilio>();

        for (int i = 0; i < gvLista.PageCount; i++)
        {
            gvLista.PageIndex = i;
            gvLista.DataBind();
            foreach (GridViewRow rfila in gvLista.Rows)
            {
                SolicitudAuxilio eAuxilio = new SolicitudAuxilio();

                eAuxilio.numero_auxilio = rfila.RowIndex;

                if (rfila.Cells[1].Text != "&nbsp;") //FECHA SOLICITUD
                    eAuxilio.fecha_solicitud = Convert.ToDateTime(rfila.Cells[1].Text);

                String cod_persona = gvLista.DataKeys[rfila.RowIndex].Values[0].ToString();
                if (cod_persona != "&nbsp;" && cod_persona != "")
                    eAuxilio.cod_persona = Convert.ToInt64(cod_persona);

                if (rfila.Cells[3].Text != "&nbsp;") //IDENTIFICACION
                    eAuxilio.identificacion = rfila.Cells[3].Text.Trim();

                if (rfila.Cells[4].Text != "&nbsp;") //CODIGO LINEA AUXILIO
                    eAuxilio.cod_linea_auxilio = rfila.Cells[4].Text.Trim();

                lista.Add(eAuxilio);

                if (eAuxilio.fecha_solicitud != DateTime.MinValue && eAuxilio.valor_solicitado != 0)
                {
                    lstAuxilios.Add(eAuxilio);
                }
            }
        }
        Session["AUXILIOS"] = lista;
        return lstAuxilios;
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
            ctlMensaje.MostrarMensaje("Desea realizar la grabación de los Auxilios cargados ?");
    }

    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        List<SolicitudAuxilio> lstDatos = new List<SolicitudAuxilio>();
        if (Session["AUXILIOS"] != null)
            lstDatos = (List<SolicitudAuxilio>)Session["AUXILIOS"];
        else
            lstDatos = ObtenerListaAuxilios();

        Usuario pUsuario = new Usuario();
        pUsuario = (Usuario)Session["usuario"];

        //DATOS DE LA OPERACION
        Xpinn.Tesoreria.Entities.Operacion vOpe = new Xpinn.Tesoreria.Entities.Operacion();
        vOpe.cod_ope = 0;
        vOpe.tipo_ope = 111;
        vOpe.cod_caja = 0;
        vOpe.cod_cajero = 0;
        vOpe.observacion = "Operacion - Carga de Auxilios";
        vOpe.cod_proceso = null;
        vOpe.fecha_oper = Convert.ToDateTime(DateTime.Today);
        vOpe.fecha_calc = DateTime.Now;
        vOpe.cod_ofi = pUsuario.cod_oficina;

        Int64 COD_OPE = 0;
        //PASAR DATOS A LA CAPA SERVICE
        AproAuxilios.RegistrarAuxiliosCargados(ref COD_OPE, vOpe, lstDatos, (Usuario)Session["usuario"]);

        //GENERAR EL COMPROBANTE
        if (COD_OPE != 0)
        {
            Xpinn.Contabilidad.Services.ComprobanteService ComprobanteServicio = new Xpinn.Contabilidad.Services.ComprobanteService();
            Session[ComprobanteServicio.CodigoPrograma + ".cod_ope"] = COD_OPE;
            Session[ComprobanteServicio.CodigoPrograma + ".tipo_ope"] = 111;
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
        if (!ValidarAccionesGrilla("DELETE"))
            return;
        try
        {
            ObtenerListaAuxilios();
            int conseID = Convert.ToInt32(gvLista.DataKeys[e.RowIndex].Values[1].ToString());

            List<SolicitudAuxilio> LstDeta;
            LstDeta = (List<SolicitudAuxilio>)Session["AUXILIOS"];

            try
            {
                foreach (SolicitudAuxilio Deta in LstDeta)
                {
                    if (Deta.numero_auxilio == conseID)
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
            Session["AUXILIOS"] = LstDeta;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AproAuxilios.CodigoPrograma, "gvLista_RowDeleting", ex);
        }
    }


    protected Boolean ValidarDatos()
    {
        Xpinn.Contabilidad.Services.ComprobanteService ComprobanteServicio = new Xpinn.Contabilidad.Services.ComprobanteService();

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


        List<SolicitudAuxilio> LstDeta = new List<SolicitudAuxilio>();

        if (Session["AUXILIOS"] != null)
            LstDeta = (List<SolicitudAuxilio>)Session["AUXILIOS"];
        else
            LstDeta = ObtenerListaAuxilios();
        if (LstDeta.Count == 0 || LstDeta == null)
        {
            VerError("No existen Datos cargado para realizar la grabación");
            return false;
        }


        int cont = 0;
        foreach (SolicitudAuxilio pAuxi in LstDeta)
        {
            cont++;
            //Validar cada fila de datos ingresados
            if (pAuxi.identificacion != null)
            {
                Xpinn.Contabilidad.Entities.DetalleComprobante vData = new Xpinn.Contabilidad.Entities.DetalleComprobante();
                Xpinn.Contabilidad.Data.ComprobanteData DAComprobante = new Xpinn.Contabilidad.Data.ComprobanteData();
                vData = DAComprobante.Identificacion_RETORNA_CodPersona(pAuxi.identificacion, (Usuario)Session["usuario"]);

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


            if (pAuxi.cod_linea_auxilio == "" || pAuxi.cod_linea_auxilio == "0")
            {
                VerError("Error en la Fila : " + cont.ToString() + " Ingrese la Linea del Auxilio o la linea no existe");
                return false;
            }


            if (pAuxi.valor_solicitado <= 0)
            {
                VerError("Error en la Fila : " + cont.ToString() + " Ingrese el valor a registrar correctamente");
                return false;
            }

        }

        return true;
    }


    protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvLista.PageIndex = e.NewPageIndex;
        List<SolicitudAuxilio> LstDeta;
        LstDeta = (List<SolicitudAuxilio>)Session["AUXILIOS"];
        gvLista.DataSource = LstDeta;
        gvLista.DataBind();
    }

    protected void gvLista_PageIndexChanged(object sender, EventArgs e)
    {

    }
}