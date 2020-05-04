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
using System.Web.UI.HtmlControls;
using System.Drawing;
using Xpinn.Reporteador.Entities;

partial class Nuevo : GlobalWeb
{
    private Xpinn.Reporteador.Services.ReporteService ReporteServicio = new Xpinn.Reporteador.Services.ReporteService();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            if (Session[ReporteServicio.CodigoProgramaRep + ".idReporte"] != null)
                VisualizarOpciones(ReporteServicio.CodigoProgramaRep, "E");
            else
                VisualizarOpciones(ReporteServicio.CodigoProgramaRep, "A");

            Site toolBar = (Site)Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ReporteServicio.CodigoProgramaRep, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Request["__EVENTARGUMENT"] != null && Request["__EVENTARGUMENT"] == "EventDblclick")
            {
                List<Tabla> lstAux = new List<Tabla>();
                // Cargar listado de tablas existentes
                if (Session["LSTTABLAS"] != null)
                    lstAux = (List<Tabla>)Session["LSTTABLAS"];
                // Verificar que la tabla no exista
                Boolean bExiste = false;
                foreach (Tabla rTabla in lstAux)
                {
                    if (rTabla.nombre == lstRepTablas.SelectedItem.Text)
                        bExiste = true;
                }
                // Adicionar nueva tabla
                if (!bExiste)
                {
                    Tabla eTabla = new Tabla();
                    eTabla.idtabla = Convert.ToInt64(lstRepTablas.SelectedItem.Value);
                    eTabla.nombre = lstRepTablas.SelectedItem.Text;
                    eTabla.tipo = "";
                    eTabla.descripcion = "";
                    lstAux.Add(eTabla);
                }
                lstTablas.DataSource = lstAux;
                lstTablas.DataValueField = "idtabla";
                lstTablas.DataTextField = "nombre";
                lstTablas.DataBind();
                Session["LSTTABLAS"] = lstAux;
                // Actualizar los dropdowlist
                ActualizarDropDownList(lstAux);
            }
            if (Request["__EVENTARGUMENT"] != null && Request["__EVENTARGUMENT"] == "EventDblclickB")
            {
                List<Tabla> lstAux = new List<Tabla>();
                // Cargar listado de tablas existentes
                if (Session["LSTTABLAS"] != null)
                    lstAux = (List<Tabla>)Session["LSTTABLAS"];
                // Adicionar nueva tabla
                int posicion = -1;
                int contador = 0;
                foreach (Tabla rTabla in lstAux)
                {
                    if (rTabla.nombre == lstTablas.SelectedItem.Text)
                        posicion = contador;
                    contador += 1;
                }
                if (posicion >= 0)
                    lstAux.RemoveAt(posicion);
                lstTablas.DataSource = lstAux;
                lstTablas.DataValueField = "idtabla";
                lstTablas.DataTextField = "nombre";
                lstTablas.DataBind();
                Session["LSTTABLAS"] = lstAux;
                ActualizarDropDownList(lstAux);
            }
            if (!IsPostBack)
            {
                mpeGrabar.Hide();
                Site toolBar = (Site)this.Master;

                Usuario usuap = new Usuario();
                usuap = (Usuario)Session["Usuario"];
                txtFechaElabora.ToDateTime = DateTime.Now;
                txtElaborador.Text = usuap.nombre;

                txtCodigo.Enabled = false;
                txtElaborador.Enabled = false;
                txtFechaElabora.Enabled = false;

                if (Session[ReporteServicio.CodigoProgramaRep + ".idReporte"] != null)
                {
                    idObjeto = Session[ReporteServicio.CodigoProgramaRep + ".idReporte"].ToString();
                    Session.Remove(ReporteServicio.CodigoProgramaRep + ".idReporte");
                    mvReporte.ActiveViewIndex = 1;
                    ObtenerDatos(idObjeto);
                }
                else
                {
                    txtCodigo.Text = ReporteServicio.ObtenerSiguienteCodigo((Usuario)Session["Usuario"]).ToString();
                    toolBar.MostrarGuardar(false);
                    rbSql.Checked = true;
                    mvReporte.ActiveViewIndex = 0;
                }
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ReporteServicio.CodigoProgramaRep, "Page_Load", ex);
        }
    }

    /// <summary>
    /// Método para guardar los datos del Reporte
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        VerError("");
        mpeGrabar.Show();
    }

    /// <summary>
    /// Grabar los datos una vez el usuario verifico que desea realizar la operación
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnContinuar_Click(object sender, EventArgs e)
    {
        try
        {
            Xpinn.Util.Usuario pUsuario = new Xpinn.Util.Usuario();
            pUsuario = (Usuario)Session["Usuario"];

            if (txtDescripcion.Text == "")
            {
                VerError("Debe ingresar la descripcion");
                return;
            }

            Xpinn.Reporteador.Entities.Reporte vReporte = new Xpinn.Reporteador.Entities.Reporte();

            if (idObjeto != "")
                vReporte = ReporteServicio.ConsultarReporte(Convert.ToInt64(idObjeto), (Usuario)Session["usuario"]);

            vReporte.idreporte = Convert.ToInt64(txtCodigo.Text.Trim());
            vReporte.descripcion = Convert.ToString(txtDescripcion.Text.Trim());
            vReporte.fecha_creacion = Convert.ToDateTime(txtFechaElabora.ToDateTime);
            vReporte.cod_elabora = pUsuario.codusuario;
            vReporte.tipo_reporte = Convert.ToInt32(ddlTipoReporte.SelectedValue.ToString());
            vReporte.sentencia_sql = Convert.ToString(txtSql.Text);
            vReporte.url_crystal = Convert.ToString(txtURLCrystal.Text);
            vReporte.encabezado = Convert.ToString(txtEncabezado.Text);
            vReporte.piepagina = Convert.ToString(txtPiePagina.Text);
            vReporte.numerar = Convert.ToInt32(cbNumerar.Checked);

            if (vReporte.tipo_reporte == 1)
            {
                rbGenerador.Checked = true;
                rbSql.Checked = false;
                rbCrystal.Checked = false;
                vReporte = DeterminarSentenciaSQL(vReporte);
            }
            else
            {
                vReporte.lstUsuarios = new List<UsuariosReporte>();                               
                int RowIndex = 0;
                foreach (GridViewRow rFila in gvUsuarios.Rows)
                {
                    UsuariosReporte vUsuario = new UsuariosReporte();
                    vUsuario.codusuario = Convert.ToInt64(rFila.Cells[0].Text);
                    vUsuario.nombre = rFila.Cells[1].Text;
                    vUsuario.idreporte = Convert.ToInt64(txtCodigo.Text);
                    CheckBox chkAutorizar = (CheckBox)rFila.Cells[2].FindControl("chkAutorizar");
                    if (chkAutorizar != null)
                        vUsuario.autorizar = chkAutorizar.Checked;
                    vReporte.lstUsuarios.Add(vUsuario);
                    RowIndex += 1;
                }
                vReporte.lstParametros = new List<Parametros>();
                RowIndex = 0;
                foreach (GridViewRow rFila in gvParametros.Rows)
                {
                    Label lblidParametro = (Label)rFila.FindControl("lblidParametro");
                    Label lblDescripcion = (Label)rFila.FindControl("lblDescripcion");
                    Label lblTipo = (Label)rFila.FindControl("lblTipo"); 
                    Label lblListaPar = (Label)rFila.FindControl("lblListaPar");
                    Parametros vParametros = new Parametros();
                    vParametros.idparametro = Convert.ToInt64(lblidParametro.Text);
                    vParametros.idreporte = Convert.ToInt64(txtCodigo.Text);
                    vParametros.descripcion = lblDescripcion.Text;
                    if (lblTipo.Text == "Lista")
                        vParametros.tipo = 4;
                    else if (lblTipo.Text == "Número" || lblTipo.Text == "Numero")
                        vParametros.tipo = 3;
                    else if (lblTipo.Text == "Fecha")
                        vParametros.tipo = 2;
                    else
                        vParametros.tipo = 1;
                    if (lblTipo.Text == "Lista")
                        vParametros.idlista = Convert.ToInt64(lblListaPar.Text);
                    vReporte.lstParametros.Add(vParametros);
                }
            }
            if (vReporte.tipo_reporte == 2)
            {
                rbSql.Checked = true;
            }
            if (vReporte.tipo_reporte == 3)
            {
                rbCrystal.Checked = true;
            }

            if (idObjeto != "")
            {
                vReporte.idreporte = Convert.ToInt64(idObjeto);
                ReporteServicio.ModificarReporte(vReporte, (Usuario)Session["usuario"]);
            }
            else
            {
                vReporte = ReporteServicio.CrearReporte(vReporte, (Usuario)Session["usuario"]);
                idObjeto = vReporte.idreporte.ToString();
            }

            Session[ReporteServicio.CodigoPrograma + ".idReporte"] = idObjeto;
            mvReporte.ActiveViewIndex = 2;
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ReporteServicio.CodigoProgramaRep, "btnGuardar_Click", ex);
        }
    }

    /// <summary>
    /// Obtener la sentencia SQL que se forma con los datos seleccionados
    /// </summary>
    /// <param name="vReporte"></param>
    /// <returns></returns>
    protected Xpinn.Reporteador.Entities.Reporte DeterminarSentenciaSQL(Xpinn.Reporteador.Entities.Reporte vReporte)
    {
        Xpinn.Util.Usuario pUsuario = new Xpinn.Util.Usuario();
        pUsuario = (Usuario)Session["Usuario"];
        // Determinar los datos                 
        vReporte.lstTablas = new List<Tabla>();
        vReporte.lstTablas = (List<Tabla>)Session["LSTTABLAS"];
        vReporte.lstEncadenamiento = new List<Encadenamiento>();
        vReporte.lstEncadenamiento = (List<Encadenamiento>)Session["LSTENCADENAMIENTOS"];
        vReporte.lstCondicion = new List<Condicion>();
        vReporte.lstCondicion = (List<Condicion>)Session["LSTCONDICIONES"];
        vReporte.lstColumnaReporte = new List<ColumnaReporte>();
        vReporte.lstColumnaReporte = (List<ColumnaReporte>)Session["LSTCOLUMNAS"];
        vReporte.lstOrden = new List<Orden>();
        vReporte.lstOrden = (List<Orden>)Session["LSTORDEN"];
        vReporte.lstGrupo = new List<Grupo>();
        vReporte.lstGrupo = (List<Grupo>)Session["LSTGRUPO"];
        vReporte.lstUsuarios = new List<UsuariosReporte>();
        vReporte.lstUsuarios = (List<UsuariosReporte>)Session["LSTUSUARIOS"];
        vReporte.lstPlantilla = new List<Plantilla>();
        vReporte.lstPlantilla = (List<Plantilla>)Session["LSTPLANTILLA"];
        int RowIndex = 0;
        foreach (GridViewRow rFila in gvUsuariosRep.Rows)
        {
            CheckBoxGrid chkAutorizar1 = (CheckBoxGrid)rFila.Cells[2].FindControl("chkAutorizar1");
            if (chkAutorizar1 != null)
                vReporte.lstUsuarios[RowIndex].autorizar = chkAutorizar1.Checked;
            RowIndex += 1;
        }
        vReporte.lstPerfil = new List<PerfilReporte>();
        vReporte.lstPerfil = (List<PerfilReporte>)Session["LSTPERFIL"];
        RowIndex = 0;
        foreach (GridViewRow rFila in gvPerfiles.Rows)
        {
            CheckBoxGrid chkAutorizar2 = (CheckBoxGrid)rFila.Cells[2].FindControl("chkAutorizar2");
            if (chkAutorizar2 != null)
                vReporte.lstPerfil[RowIndex].autorizar = chkAutorizar2.Checked;
            RowIndex += 1;
        }
        vReporte.lstParametros = new List<Parametros>();
        // Determinar la sentencia SQL
        Boolean bTieneFormulas = false;
        vReporte.sentencia_sql = "SELECT ";
        int pos = 0;        
        foreach (ColumnaReporte eColumna in vReporte.lstColumnaReporte)
        {
            if (eColumna.idcolumna != -1)
            {
                if (pos > 0)
                    vReporte.sentencia_sql += ", ";
                if (eColumna.tipo == "Formula")
                {
                    bTieneFormulas = true;
                    vReporte.sentencia_sql += eColumna.formula + "(";
                }
                // Verificar si la columna ya existe en el SQL
                Boolean bRepetida = false;
                string snomfor = eColumna.formula;
                string snomcol = eColumna.columna;
                for (int posC = 0; posC < pos && posC <= vReporte.lstColumnaReporte.Count(); posC += 1)
                {
                    if (vReporte.lstColumnaReporte[posC].idcolumna != -1)
                    {
                        if (snomcol == vReporte.lstColumnaReporte[posC].columna && snomfor == vReporte.lstColumnaReporte[posC].formula)
                            bRepetida = true;
                    }
                }
                if (eColumna.tipo == "Constante")
                    vReporte.sentencia_sql += eColumna.columna;
                else
                    vReporte.sentencia_sql += eColumna.tabla + "." + eColumna.columna;
                if (eColumna.tipo != "Constante")
                {
                    if (bTieneFormulas == true)
                        vReporte.sentencia_sql += ") AS " + eColumna.columna + pos.ToString(); 
                    if (bRepetida == true)
                        vReporte.sentencia_sql += " AS " + eColumna.columna + pos.ToString();
                }
                vReporte.lstColumnaReporte[pos].orden = pos + 1;
                pos += 1;                
            }
        }
        vReporte.sentencia_sql += " FROM ";
        pos = 0;
        foreach (Tabla eTabla in vReporte.lstTablas)
        {
            if (eTabla.idtabla != -1)
            {
                if (pos > 0)
                    vReporte.sentencia_sql += ", ";
                vReporte.sentencia_sql += eTabla.nombre;
                pos += 1;
            }
        }
        if (vReporte.lstEncadenamiento.Count > 0 || vReporte.lstCondicion.Count > 0)
        {
            int contador = 0;
            foreach (Encadenamiento eEncadenamiento in vReporte.lstEncadenamiento)
            {
                if (eEncadenamiento.idencadenamiento != -1)
                    contador += 1;
            }
            if (contador > 0)
            {
                if (contador > 0)
                {
                    vReporte.sentencia_sql += " WHERE ";
                    vReporte.sentencia_sql += " (";
                    pos = 0;
                    foreach (Encadenamiento eEncadenamiento in vReporte.lstEncadenamiento)
                    {
                        if (eEncadenamiento.idencadenamiento != -1)
                        {
                            if (pos > 0)
                                vReporte.sentencia_sql += " AND ";
                            vReporte.sentencia_sql += eEncadenamiento.tabla1 + "." + eEncadenamiento.columna1 + " = " + eEncadenamiento.tabla2 + "." + eEncadenamiento.columna2;
                            pos += 1;
                        }
                    }
                    vReporte.sentencia_sql += ") ";
                }
            }
            contador = 0;
            foreach (Condicion eCondicion in vReporte.lstCondicion)
            {
                if (eCondicion.idcondicion != -1)
                    contador += 1;
            }
            if (contador > 0)
            {
                if (vReporte.sentencia_sql.Contains("WHERE"))
                    vReporte.sentencia_sql += " AND ";
                else
                    vReporte.sentencia_sql += " WHERE ";
                pos = 0;
                foreach (Condicion eCondicion in vReporte.lstCondicion)
                {
                    if (eCondicion.idcondicion != -1)
                    {
                        // Operador lógino
                        if (pos > 0)
                            vReporte.sentencia_sql += " " + eCondicion.andor + " ";
                        vReporte.sentencia_sql += eCondicion.parentesisizq;
                        // Primer operando
                        if (eCondicion.tipo1 == 1)
                            vReporte.sentencia_sql += eCondicion.tabla1 + "." + eCondicion.columna1;
                        else
                            vReporte.sentencia_sql += eCondicion.valor1;
                        // Operador
                        if (eCondicion.operador == "Es igual a")
                            vReporte.sentencia_sql += " = ";
                        if (eCondicion.operador == "Es diferente a")
                            vReporte.sentencia_sql += " != ";
                        if (eCondicion.operador == "Es menor a")
                            vReporte.sentencia_sql += " < ";
                        if (eCondicion.operador == "Es menor o igual a")
                            vReporte.sentencia_sql += " <= ";
                        if (eCondicion.operador == "Es mayor a")
                            vReporte.sentencia_sql += " > ";
                        if (eCondicion.operador == "Es mayor o igual a")
                            vReporte.sentencia_sql += " >= ";
                        if (eCondicion.operador == "Conjunto")
                            vReporte.sentencia_sql += " In ";
                        if (eCondicion.operador == "No conjunto")
                            vReporte.sentencia_sql += " Not In ";
                        if (eCondicion.operador == "Nulo")
                            vReporte.sentencia_sql += " Is Null ";
                        if (eCondicion.operador == "No nulo")
                            vReporte.sentencia_sql += " Is Not Null ";
                        // Segundo operando
                        if (eCondicion.tipo2 == 1)
                        {
                            vReporte.sentencia_sql += eCondicion.tabla2 + "." + eCondicion.columna2;
                        }
                        else if (eCondicion.tipo2 == 2)
                        {
                            vReporte.sentencia_sql += eCondicion.valor2;
                        }
                        else if (eCondicion.tipo2 == 3 || eCondicion.tipo2 == 4)
                        {
                            if (eCondicion.valor2 != null)
                                eCondicion.valor2 = eCondicion.valor2.Trim();
                            else
                                eCondicion.valor2 = eCondicion.columna1;
                            vReporte.sentencia_sql += " &" + eCondicion.valor2 + "& ";
                            Parametros eParametro = new Parametros();
                            eParametro.idreporte = 0;
                            eParametro.idparametro = 0;
                            if (eCondicion.valor2.Trim() != "")
                                eParametro.descripcion = eCondicion.valor2;
                            else
                                eParametro.descripcion = eCondicion.columna1;
                            Columna eColumna = new Columna();
                            eColumna = ReporteServicio.ConsultarColumna(eCondicion.tabla1, eCondicion.columna1, pUsuario);
                            eParametro.tipo = ReporteServicio.CodTipoDato(eColumna.tipo_dato);
                            eParametro.idlista = eCondicion.idlista;
                            vReporte.lstParametros.Add(eParametro);
                        }
                        vReporte.sentencia_sql += eCondicion.parentesisder;
                        pos += 1;
                    }
                }
            }
        }
        if (bTieneFormulas)
        {
            vReporte.sentencia_sql += " GROUP BY ";
            pos = 0;
            foreach (ColumnaReporte eColumna in vReporte.lstColumnaReporte)
            {
                if (eColumna.idcolumna != -1)
                {
                    if (pos > 0 && eColumna.tipo != "Formula")
                        vReporte.sentencia_sql += ", ";
                    if (eColumna.tipo != "Formula")
                        vReporte.sentencia_sql += eColumna.tabla + "." + eColumna.columna;
                    pos += 1;
                }
            }
        }
        int contadorORD = 0;
        foreach (Orden eOrden in vReporte.lstOrden)
        {
            if (eOrden.idorden != -1)
                contadorORD += 1;
        }
        if (contadorORD > 0)
        {
            vReporte.sentencia_sql += " ORDER BY ";
            pos = 0;
            foreach (Orden eOrden in vReporte.lstOrden)
            {
                if (eOrden.idorden != -1)
                {
                    if (pos > 0)
                        vReporte.sentencia_sql += " , ";
                    vReporte.sentencia_sql += eOrden.tabla + "." + eOrden.columna;
                    pos += 1;
                }
            }
        }

        return vReporte;
    }

    protected void btnParar_Click(object sender, EventArgs e)
    {
        mpeGrabar.Hide();
    }

    protected void btnFinalClick(object sender, EventArgs e)
    {
        Session[ReporteServicio.CodigoProgramaRep + ".idreporte"] = txtCodigo.Text;
        Navegar(Pagina.Nuevo);
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        if (idObjeto != "")
        {
            Session[ReporteServicio.CodigoProgramaRep + ".idReporte"] = idObjeto;
        }
        Navegar(Pagina.Lista);
    }

    /// <summary>
    /// Mostrar los datos según el reporte seleccionado y que ya esta creado
    /// </summary>
    /// <param name="pIdObjeto"></param>
    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            Xpinn.Reporteador.Entities.Reporte vReporte = new Xpinn.Reporteador.Entities.Reporte();
            vReporte = ReporteServicio.ConsultarReporte(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);

            txtCodigo.Text = HttpUtility.HtmlDecode(vReporte.idreporte.ToString().Trim());
            Session["idReporte"] = vReporte.idreporte;
            ddlTipoReporte.SelectedValue = vReporte.tipo_reporte.ToString();
            ActivarTipoReporte(vReporte.tipo_reporte);
            if (!string.IsNullOrEmpty(vReporte.descripcion))
                txtDescripcion.Text = HttpUtility.HtmlDecode(vReporte.descripcion.ToString().Trim());
            if (!string.IsNullOrEmpty(vReporte.sentencia_sql))
                txtSql.Text = HttpUtility.HtmlDecode(vReporte.sentencia_sql.ToString().Trim());
            if (!string.IsNullOrEmpty(vReporte.url_crystal))
                txtURLCrystal.Text = HttpUtility.HtmlDecode(vReporte.url_crystal.ToString().Trim());
            if (vReporte.fecha_creacion != null)
                txtFechaElabora.ToDateTime = Convert.ToDateTime(HttpUtility.HtmlDecode(vReporte.fecha_creacion.ToString().Trim()));            
            if (vReporte.tipo_reporte != 1)
            {
                Actualizar_Parametros();
                Actualizar_Usuarios();
            }
            if (vReporte.lstTablas != null)
            {
                lstTablas.DataSource = vReporte.lstTablas;
                lstTablas.DataValueField = "idtabla";
                lstTablas.DataTextField = "nombre";
                lstTablas.DataBind();
                Session["LSTTABLAS"] = vReporte.lstTablas;
            }
            if (vReporte.lstEncadenamiento != null)
            {
                gvEncadenamientos.DataSource = vReporte.lstEncadenamiento;
                gvEncadenamientos.DataBind();
                Session["LSTENCADENAMIENTOS"] = vReporte.lstEncadenamiento;                
            }
            InicializarEncadenamientos();
            if (vReporte.lstCondicion != null)
            {
                gvCondiciones.DataSource = vReporte.lstCondicion;
                gvCondiciones.DataBind();
                Session["LSTCONDICIONES"] = vReporte.lstCondicion;                
            }
            InicializarCondiciones();
            if (vReporte.lstColumnaReporte != null)
            {
                gvColumnas.DataSource = vReporte.lstColumnaReporte;
                gvColumnas.DataBind();
                Session["LSTCOLUMNAS"] = vReporte.lstColumnaReporte;                
            }
            InicializarColumnaReporte();
            if (vReporte.lstOrden != null)
            {
                gvOrden.DataSource = vReporte.lstOrden;
                gvOrden.DataBind();
                Session["LSTORDEN"] = vReporte.lstOrden;                
            }
            InicializarOrden();
            if (vReporte.lstGrupo != null)
            {
                gvGrupo.DataSource = vReporte.lstGrupo;
                gvGrupo.DataBind();
                Session["LSTGRUPO"] = vReporte.lstGrupo;                
            }
            InicializarGrupo();            
            if (vReporte.lstPlantilla != null)
            {
                gvPlantillas.DataSource = vReporte.lstPlantilla;
                gvPlantillas.DataBind();
                Session["LSTPLANTILLA"] = vReporte.lstPlantilla;
            }
            InicializarPlantilla();
            InicializarUsuarios();
            InicializarPerfil();            
            if (vReporte.lstTablas != null)
            {
                ActualizarDropDownList(vReporte.lstTablas);
            }
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }

    protected void imgAceptar_Click(object sender, ImageClickEventArgs e)
    {
        int tipoReporte = 0;
        Session.Remove("LSTTABLAS");
        Session.Remove("LSTENCADENAMIENTOS");
        Session.Remove("LSTCONDICIONES");
        Session.Remove("LSTCOLUMNAS");
        Session.Remove("LSTORDEN");
        Session.Remove("LSTGRUPO");
        Session.Remove("LSTUSUARIOS");
        Session.Remove("LSTPERFIL");
        Session.Remove("LSTPLANTILLA");
        LimpiaBotonesCondiciones();
        TabsConsulta.ActiveTabIndex = 0;

        if (rbGenerador.Checked == true)
            tipoReporte = 1;
        if (rbSql.Checked == true)
            tipoReporte = 2;
        if (rbCrystal.Checked == true)
            tipoReporte = 3;
        ActivarTipoReporte(tipoReporte);
        Site toolBar = (Site)this.Master;
        toolBar.MostrarGuardar(true);
        mvReporte.ActiveViewIndex = 1;
        VerificaBotones();
    }

    protected void rbGenerador_CheckedChanged(object sender, EventArgs e)
    {
        Session.Remove("LSTTABLAS");
        if (rbGenerador.Checked == true)
        {
            rbSql.Checked = false;
            rbCrystal.Checked = false;
        }

    }

    protected void rbSql_CheckedChanged(object sender, EventArgs e)
    {
        if (rbSql.Checked == true)
        {
            rbGenerador.Checked = false;
            rbCrystal.Checked = false;
        }
    }

    protected void rbCrystal_CheckedChanged(object sender, EventArgs e)
    {
        if (rbCrystal.Checked == true)
        {
            rbGenerador.Checked = false;
            rbSql.Checked = false;
        }
    }

    protected void OkButtonSQL_Click(object sender, EventArgs e)
    {
        mpeLinkSQL.Hide();
    }

    /// <summary>
    /// Mostrar la sentencia SQL que se forma con el reporte
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSQL_Click(object sender, ImageClickEventArgs e)
    {
        Reporte vReporte = new Reporte();
        vReporte = DeterminarSentenciaSQL(vReporte);
        txtMostrarSQL.Text = vReporte.sentencia_sql;
        mpeLinkSQL.Show();
    }

    /// <summary>
    /// Según el tipo de reporte activar los campos
    /// </summary>
    /// <param name="ptipo_reporte"></param> 
    protected void ActivarTipoReporte(Int64 ptipo_reporte)
    {
        ddlTipoReporte.Enabled = false;

        if (ptipo_reporte == 1)
        {
            // Determina el tipo de reporte es el generador de consulta
            mvDetalle.SetActiveView(vwConsulta);
            ddlTipoReporte.SelectedIndex = 1;
            btnSQL.Visible = true;
            // Activa el doble click en las tablas
            lstRepTablas.Attributes.Add("ondblclick", Page.ClientScript.GetPostBackEventReference(lstRepTablas, "EventDblclick"));
            lstTablas.Attributes.Add("ondblclick", Page.ClientScript.GetPostBackEventReference(lstTablas, "EventDblclickB"));
            // Llena listado de tablas
            Usuario pUsuario = new Usuario();
            pUsuario = (Usuario)Session["Usuario"];
            Tabla pTabla = new Tabla();
            List<Tabla> lstTabla = new List<Tabla>();
            lstTabla = ReporteServicio.ListarTablaBase(pTabla, pUsuario);
            lstRepTablas.DataSource = lstTabla;
            lstRepTablas.DataValueField = "idtabla";
            lstRepTablas.DataTextField = "nombre";
            lstRepTablas.DataBind();
            // Inicializa la tabla de encadenamientos
            InicializarEncadenamientos();
            InicializarCondiciones();
            InicializarColumnaReporte();
            InicializarOrden();
            InicializarGrupo();
            InicializarUsuarios();
            InicializarPerfil();
        }
        if (ptipo_reporte == 2)
        {
            // Determina el tipo de reporte es una sentencia sql
            mvDetalle.SetActiveView(vwSentencia);
            ddlTipoReporte.SelectedIndex = 2;
            btnSQL.Visible = false;
        }
        if (ptipo_reporte == 3)
        {
            // Determina el tipo de reporte es crystal
            mvDetalle.SetActiveView(vwCrystal);
            ddlTipoReporte.SelectedIndex = 3;
            btnSQL.Visible = false;
        }
    }

    /// <summary>
    /// Actualizar listados según tablas seleccionadas
    /// </summary>
    /// <param name="lstAux"></param>
    protected void ActualizarDropDownList(List<Tabla> lstAux)
    {
        DropDownList ddlTabla1 = (DropDownList)gvEncadenamientos.FooterRow.Cells[2].FindControl("ddlTabla1");
        if (ddlTabla1 != null)
        {
            ddlTabla1.Items.Clear();
            ddlTabla1.DataSource = lstAux;
            ddlTabla1.DataValueField = "idtabla";
            ddlTabla1.DataTextField = "nombre";
            ddlTabla1.DataBind();
            ddlTabla1_SelectedIndexChanged(null, null);
        }
        DropDownList ddlTabla2 = (DropDownList)gvEncadenamientos.FooterRow.Cells[2].FindControl("ddlTabla2");
        if (ddlTabla2 != null)
        {
            ddlTabla2.Items.Clear();
            ddlTabla2.DataSource = lstAux;
            ddlTabla2.DataValueField = "idtabla";
            ddlTabla2.DataTextField = "nombre";
            ddlTabla2.DataBind();
            ddlTabla2_SelectedIndexChanged(null, null);
        }

        DropDownList ddlTablaCondicion1 = (DropDownList)gvCondiciones.FooterRow.Cells[2].FindControl("ddlTablaCondicion1");
        if (ddlTablaCondicion1 != null)
        {
            ddlTablaCondicion1.Items.Clear();
            ddlTablaCondicion1.DataSource = lstAux;
            ddlTablaCondicion1.DataValueField = "idtabla";
            ddlTablaCondicion1.DataTextField = "nombre";
            ddlTablaCondicion1.DataBind();
            ddlTablaCondicion1_SelectedIndexChanged(null, null);
        }

        DropDownList ddlTablaCondicion2 = (DropDownList)gvCondiciones.FooterRow.Cells[2].FindControl("ddlTablaCondicion2");
        if (ddlTablaCondicion2 != null)
        {
            ddlTablaCondicion2.Items.Clear();
            ddlTablaCondicion2.DataSource = lstAux;
            ddlTablaCondicion2.DataValueField = "idtabla";
            ddlTablaCondicion2.DataTextField = "nombre";
            ddlTablaCondicion2.DataBind();
            ddlTablaCondicion2_SelectedIndexChanged(null, null);
        }

        DropDownList ddlCOLTabla = (DropDownList)gvColumnas.FooterRow.Cells[2].FindControl("ddlCOLTabla");
        if (ddlCOLTabla != null)
        {
            ddlCOLTabla.Items.Clear();
            ddlCOLTabla.DataSource = lstAux;
            ddlCOLTabla.DataValueField = "idtabla";
            ddlCOLTabla.DataTextField = "nombre";
            ddlCOLTabla.DataBind();
            ddlCOLTabla_SelectedIndexChanged(null, null);
        }

        DropDownList ddlORDTabla = (DropDownList)gvOrden.FooterRow.Cells[2].FindControl("ddlORDTabla");
        if (ddlORDTabla != null)
        {
            ddlORDTabla.Items.Clear();
            ddlORDTabla.DataSource = lstAux;
            ddlORDTabla.DataValueField = "idtabla";
            ddlORDTabla.DataTextField = "nombre";
            ddlORDTabla.DataBind();
            ddlORDTabla_SelectedIndexChanged(null, null);
        }

        DropDownList ddlGRUTabla = (DropDownList)gvGrupo.FooterRow.Cells[2].FindControl("ddlGRUTabla");
        if (ddlGRUTabla != null)
        {
            ddlGRUTabla.Items.Clear();
            ddlGRUTabla.DataSource = lstAux;
            ddlGRUTabla.DataValueField = "idtabla";
            ddlGRUTabla.DataTextField = "nombre";
            ddlGRUTabla.DataBind();
            ddlGRUTabla_SelectedIndexChanged(null, null);
        }
    }
    
    #region parametros

    protected void gvParametros_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvParametros.EditIndex = -1;
        long conseID = Convert.ToInt32(gvParametros.DataKeys[e.RowIndex].Values[0].ToString());
        String consecutivo = conseID.ToString();
        Actualizar_Parametros();
    }

    protected void gvParametros_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        VerError("");
        if (e.CommandName.Equals("AddNew"))
        {
            TextBox txtDescripcionF = (TextBox)gvParametros.FooterRow.FindControl("txtDescripcionF");
            DropDownList ddlTipoF = (DropDownList)gvParametros.FooterRow.FindControl("ddlTipoF");
            DropDownList ddlListaParF = (DropDownList)gvParametros.FooterRow.FindControl("ddlListaParF");

            if (txtDescripcionF.Text == "" || txtDescripcionF.Text == "")
            {
                VerError("Por favor diligenciar los datos");
            }
            else
            {
                Parametros vParametros = new Parametros();
                vParametros.idparametro = 0;
                vParametros.idreporte = Convert.ToInt64(txtCodigo.Text);
                vParametros.descripcion = txtDescripcionF.Text;
                vParametros.tipo = Convert.ToInt64(ddlTipoF.SelectedIndex);
                if (ddlListaParF.SelectedValue != null)
                    if (ddlListaParF.SelectedValue != "")
                        vParametros.idlista =  Convert.ToInt64(ddlListaParF.SelectedValue);
                 gvParametros.EditIndex = -1;
                if (txtCodigo.Text != "0" && txtCodigo.Text.Trim() != "")
                    ReporteServicio.CrearParametro(vParametros, (Usuario)Session["usuario"]);
                Actualizar_Parametros();
            }
        }
    }

    protected void gvParametros_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        // Al actualizar el gridview, cambia el codigo de la columna "Modelo" por la respectiva descripcion        
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblTipo = (Label)e.Row.FindControl("lblTipo");
            DropDownListGrid ddlTipo = (DropDownListGrid)e.Row.FindControl("ddlTipo");
            if (ddlTipo != null)
            {
                DropDownList ddlListaPar = (DropDownList)e.Row.FindControl("ddlListaPar");
                if (ddlListaPar != null)
                {
                    if (ddlTipo.SelectedIndex == 4)
                        ddlListaPar.Visible = true;
                    else
                        ddlListaPar.Visible = false;
                    ddlListaPar.DataTextField = "descripcion";
                    ddlListaPar.DataValueField = "idlista";
                    ddlListaPar.DataSource = ReporteServicio.ListarLista(new Xpinn.Reporteador.Entities.Lista(), (Usuario)Session["Usuario"]);
                    ddlListaPar.DataBind();
                }
            }
        }
    }

    protected void gvParametros_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        long ID = Convert.ToInt32(gvParametros.DataKeys[e.RowIndex].Values[0].ToString());
        if (ID != 0)
        {
            ReporteServicio.EliminarParametro(Convert.ToInt64(txtCodigo.Text), ID, (Usuario)Session["usuario"]);
            Actualizar_Parametros();
        }
    }

    protected void gvParametros_RowEditing(object sender, GridViewEditEventArgs e)
    {
        int conseID = Convert.ToInt32(gvParametros.DataKeys[e.NewEditIndex].Values[0].ToString());
        if (conseID != 0)
        {
            gvParametros.EditIndex = e.NewEditIndex;
            Actualizar_Parametros();
        }
    }

    protected void gvParametros_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        // Guarda (Modifica) los valores que han sido editados desde el gridview
        long conseID = Convert.ToInt32(gvParametros.DataKeys[e.RowIndex].Values[0].ToString());
        TextBox txtDescripcion = (TextBox)gvParametros.Rows[e.RowIndex].FindControl("txtDescripcion");
        DropDownListGrid ddlTipo = (DropDownListGrid)gvParametros.Rows[e.RowIndex].FindControl("ddlTipo");
        DropDownList ddlListaPar = (DropDownList)gvParametros.Rows[e.RowIndex].FindControl("ddlListaPar");

        Parametros pParametros = new Parametros();
        pParametros.idparametro = conseID;
        pParametros.idreporte = Convert.ToInt64(txtCodigo.Text);
        pParametros.descripcion = txtDescripcion.Text.Trim() != "" ? txtDescripcion.Text : string.Empty;
        pParametros.tipo = Convert.ToInt64(ddlTipo.SelectedIndex);
        if (ddlListaPar.SelectedValue != null)
            if (ddlListaPar.SelectedValue.Trim() != "")
                pParametros.idlista = Convert.ToInt64(ddlListaPar.SelectedValue);

        gvParametros.EditIndex = -1;
        if (txtCodigo.Text != "0" && txtCodigo.Text.Trim() != "")
            ReporteServicio.ModificarParametro(pParametros, (Usuario)Session["usuario"]);
        Actualizar_Parametros();
    }

    public void Actualizar_Parametros()
    {
        lblTitParametros.Visible = true;        
        try
        {
            List<Parametros> lstConsulta = new List<Parametros>();
            Parametros pParametros = new Parametros();
            pParametros.idreporte = Convert.ToInt64(txtCodigo.Text);
            lstConsulta = ReporteServicio.ListarParametro(pParametros, (Usuario)Session["usuario"]);
            gvParametros.PageSize = Convert.ToInt32(ConfigurationManager.AppSettings["pageSize"]);
            gvParametros.DataSource = lstConsulta;

            if (lstConsulta.Count > 0)
            {
                gvParametros.Visible = true;
                gvParametros.DataBind();
                ValidarPermisosGrilla(gvParametros);
            }
            else
            {   //Permite visualizar el footer del gridview cuando no hay ningun registro para mostrar
                Parametros vParametros = new Parametros();
                vParametros.idparametro = 0;
                vParametros.idreporte = 0;
                vParametros.descripcion = "";
                vParametros.tipo = 0;
                lstConsulta.Add(vParametros);
                gvParametros.DataBind();
                gvParametros.Rows[0].Visible = false;
            }

            Session.Add(ReporteServicio.CodigoPrograma + ".consulta", 1);

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ReporteServicio.CodigoPrograma, "Actualizar", ex);
        }
    }

    protected void ddlTipo_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownListGrid ddlTipo = (DropDownListGrid)sender;
        if (ddlTipo != null)
        {
            int rowindex = Convert.ToInt32(ddlTipo.CommandArgument.ToString());
            DropDownList ddlListaPar = (DropDownList)gvParametros.Rows[rowindex].FindControl("ddlListaPar");
            if (ddlListaPar != null)
            {
                if (ddlTipo.SelectedIndex == 4)
                    ddlListaPar.Visible = true;
                else
                    ddlListaPar.Visible = false;
                ddlListaPar.DataTextField = "descripcion";
                ddlListaPar.DataValueField = "idlista";
                ddlListaPar.DataSource = ReporteServicio.ListarLista(new Xpinn.Reporteador.Entities.Lista(), (Usuario)Session["Usuario"]);
                ddlListaPar.DataBind();
            }
        }
    }

    protected void ddlTipoF_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddlTipoF = (DropDownList)sender;
        if (ddlTipoF != null)
        {
            DropDownList ddlListaParF = (DropDownList)gvParametros.FooterRow.FindControl("ddlListaParF");
            if (ddlListaParF != null)
            {
                if (ddlTipoF.SelectedIndex == 4)
                    ddlListaParF.Visible = true;
                else
                    ddlListaParF.Visible = false;
                ddlListaParF.DataTextField = "descripcion";
                ddlListaParF.DataValueField = "idlista";
                ddlListaParF.DataSource = ReporteServicio.ListarLista(new Xpinn.Reporteador.Entities.Lista(), (Usuario)Session["Usuario"]);
                ddlListaParF.DataBind();
            }
        }
    }


    #endregion

    #region usuarios

    private void Actualizar_Usuarios()
    {
        lblTitUsuarios.Visible = true;
        try
        {
            List<UsuariosReporte> lstConsulta = new List<UsuariosReporte>();
            UsuariosReporte pusuariosReporte = new UsuariosReporte();
            if (txtCodigo.Text.Trim() != "")
                pusuariosReporte.idreporte = Convert.ToInt64(txtCodigo.Text);
            else
                pusuariosReporte.idreporte = 0;
            lstConsulta = ReporteServicio.ListarUsuarios(pusuariosReporte, (Usuario)Session["Usuario"]);
            Session["LSTUSUARIOS"] = lstConsulta;
            String emptyQuery = "Fila de datos vacia";
            gvUsuarios.EmptyDataText = emptyQuery;
            gvUsuarios.DataSource = lstConsulta;
            if (lstConsulta.Count > 0)
            {
                gvUsuarios.Visible = true;
                gvUsuarios.DataBind();
                //ValidarPermisosGrilla(gvUsuarios);
            }
            else
            {
                gvUsuarios.Visible = false;
            }
        }
        catch
        {
        }

    }
   
    #endregion

    #region encadenamientos

    protected void InicializarEncadenamientos()
    {
        List<Encadenamiento> lstEncadenamiento = new List<Encadenamiento>();
        if (Session["LSTENCADENAMIENTOS"] != null)
            lstEncadenamiento = (List<Encadenamiento>)Session["LSTENCADENAMIENTOS"];
        for (int i = lstEncadenamiento.Count + 1; i <= 5; i++)
        {
            Encadenamiento eEncadena = new Encadenamiento();
            eEncadena.idencadenamiento = -1;
            eEncadena.columna1 = "";
            eEncadena.columna2 = "";
            lstEncadenamiento.Add(eEncadena);
        }
        gvEncadenamientos.DataSource = lstEncadenamiento;
        gvEncadenamientos.DataBind();
        Session["LSTENCADENAMIENTOS"] = lstEncadenamiento;
    }

    protected void gvEncadenamientos_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("Add"))
        {
            List<Encadenamiento> lstEncadenamiento = new List<Encadenamiento>();
            if (Session["LSTENCADENAMIENTOS"] != null)
                lstEncadenamiento = (List<Encadenamiento>)Session["LSTENCADENAMIENTOS"];
            Encadenamiento eEncadena = new Encadenamiento();
            eEncadena.idencadenamiento = 0;
            eEncadena.nombre = "";
            DropDownList ddlTabla1 = new DropDownList();
            ddlTabla1 = (DropDownList)gvEncadenamientos.FooterRow.FindControl("ddlTabla1");
            if (ddlTabla1 != null)
                eEncadena.tabla1 = ddlTabla1.SelectedItem.Text;
            DropDownList ddlColumna1 = new DropDownList();
            ddlColumna1 = (DropDownList)gvEncadenamientos.FooterRow.FindControl("ddlColumna1");
            if (ddlColumna1 != null)
                eEncadena.columna1 = ddlColumna1.SelectedItem.Text;
            DropDownList ddlTabla2 = new DropDownList();
            ddlTabla2 = (DropDownList)gvEncadenamientos.FooterRow.FindControl("ddlTabla2");
            if (ddlTabla2 != null)
                eEncadena.tabla2 = ddlTabla2.SelectedItem.Text;
            DropDownList ddlColumna2 = new DropDownList();
            ddlColumna2 = (DropDownList)gvEncadenamientos.FooterRow.FindControl("ddlColumna2");
            if (ddlColumna2 != null)
                eEncadena.columna2 = ddlColumna2.SelectedItem.Text;
            Boolean bYaExiste = false;
            for (int i = 0; i < lstEncadenamiento.Count(); i++)
            {
                if (lstEncadenamiento[i].tabla1 == eEncadena.tabla1 && lstEncadenamiento[i].columna1 == eEncadena.columna1 &&
                    lstEncadenamiento[i].tabla2 == eEncadena.tabla2 && lstEncadenamiento[i].columna2 == eEncadena.columna2)
                {
                    bYaExiste = true;
                    i = lstEncadenamiento.Count();
                }
            }
            if (!bYaExiste)
            {
                int posicion = -1;
                for (int i = 0; i < lstEncadenamiento.Count(); i++)
                {
                    if (lstEncadenamiento[i].idencadenamiento == -1)
                    {
                        lstEncadenamiento[i].idencadenamiento = eEncadena.idencadenamiento;
                        lstEncadenamiento[i].nombre = eEncadena.nombre;
                        lstEncadenamiento[i].tabla1 = eEncadena.tabla1;
                        lstEncadenamiento[i].columna1 = eEncadena.columna1;
                        lstEncadenamiento[i].tabla2 = eEncadena.tabla2;
                        lstEncadenamiento[i].columna2 = eEncadena.columna2;
                        posicion = i;
                        i = lstEncadenamiento.Count();
                    }
                }
                if (posicion == -1)
                    lstEncadenamiento.Add(eEncadena);
            }
            gvEncadenamientos.DataSource = lstEncadenamiento;
            gvEncadenamientos.DataBind();
            Session["LSTENCADENAMIENTOS"] = lstEncadenamiento;
        }
        if (e.CommandName.Equals("Deleted"))
        {
            try
            {
                int RowIndex = Convert.ToInt32(e.CommandArgument.ToString());
                List<Encadenamiento> lstEncadenamiento = new List<Encadenamiento>();
                lstEncadenamiento = (List<Encadenamiento>)Session["LSTENCADENAMIENTOS"];
                if (lstEncadenamiento.Count > RowIndex)
                {
                    lstEncadenamiento.RemoveAt(RowIndex);
                    gvEncadenamientos.DataSource = lstEncadenamiento;
                    gvEncadenamientos.DataBind();
                    Session["LSTENCADENAMIENTOS"] = lstEncadenamiento;
                    if (lstEncadenamiento.Count < 5)
                        InicializarEncadenamientos();
                }
            }
            catch
            {
                VerError("");
            }
        }
        List<Tabla> lstAux = new List<Tabla>();
        // Cargar listado de tablas existentes
        if (Session["LSTTABLAS"] != null)
            lstAux = (List<Tabla>)Session["LSTTABLAS"];
        Session["LSTTABLAS"] = lstAux;
        ActualizarDropDownList(lstAux);
    }

    protected void gvEncadenamientos_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        Label lblidEncadenamiento = (Label)e.Row.Cells[1].FindControl("lblidEncadenamiento");
        if (lblidEncadenamiento != null)
        {
            if (lblidEncadenamiento.Text == "-1")
            {
                ImageButton btnEliminar = (ImageButton)e.Row.Cells[0].FindControl("btnEliminar");
                btnEliminar.Visible = false;
                Label lblOperador = (Label)e.Row.Cells[2].FindControl("lblOperador");
                if (lblOperador != null)
                    lblOperador.Visible = false;
            }
        }
    }
   
    protected void ddlTabla1_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddlTabla1 = (DropDownList)gvEncadenamientos.FooterRow.Cells[2].FindControl("ddlTabla1");
        if (ddlTabla1 != null)
        {
            Usuario pUsuario = new Usuario();
            pUsuario = (Usuario)Session["Usuario"];
            List<Columna> lstColumnas = new List<Columna>();
            Columna pColumna = new Columna();
            if (ddlTabla1.SelectedItem != null)
            {
                pColumna.tabla = ddlTabla1.SelectedItem.Text;
                lstColumnas = ReporteServicio.ListarColumnaBase(ddlTabla1.SelectedItem.Text, pColumna, pUsuario);
            }
            DropDownList ddlColumna1 = (DropDownList)gvEncadenamientos.FooterRow.Cells[2].FindControl("ddlColumna1");
            if (ddlColumna1 != null)
            {
                ddlColumna1.DataSource = lstColumnas;
                ddlColumna1.DataTextField = "nombre";
                ddlColumna1.DataValueField = "idcolumna";
                ddlColumna1.DataBind();
            }
        }
    }

    protected void ddlTabla2_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddlTabla2 = (DropDownList)gvEncadenamientos.FooterRow.Cells[2].FindControl("ddlTabla2");
        if (ddlTabla2 != null)
        {
            Usuario pUsuario = new Usuario();
            pUsuario = (Usuario)Session["Usuario"];
            List<Columna> lstColumnas = new List<Columna>();
            Columna pColumna = new Columna();
            if (ddlTabla2.SelectedItem != null)
            {
                pColumna.tabla = ddlTabla2.SelectedItem.Text;
                lstColumnas = ReporteServicio.ListarColumnaBase(ddlTabla2.SelectedItem.Text, pColumna, pUsuario);
            }
            DropDownList ddlColumna2 = (DropDownList)gvEncadenamientos.FooterRow.Cells[2].FindControl("ddlColumna2");
            if (ddlColumna2 != null)
            {
                ddlColumna2.DataSource = lstColumnas;
                ddlColumna2.DataTextField = "nombre";
                ddlColumna2.DataValueField = "idcolumna";
                ddlColumna2.DataBind();
            }
        }
    }

    #endregion

    #region condiciones

    protected void InicializarCondiciones()
    {
        // Inicializar la grilla
        List<Condicion> lstCondicion = new List<Condicion>();
        if (Session["LSTCONDICIONES"] != null)
            lstCondicion = (List<Condicion>)Session["LSTCONDICIONES"];
        for (int i = lstCondicion.Count; i <= 5; i++)
        {
            Condicion eCondicion = new Condicion();
            eCondicion.idcondicion = -1;
            lstCondicion.Add(eCondicion);
        }
        gvCondiciones.DataSource = lstCondicion;
        gvCondiciones.DataBind();
        Session["LSTCONDICIONES"] = lstCondicion;
        // Inicializa DDL de listados                
        DropDownList ddlLista = (DropDownList)gvCondiciones.FooterRow.Cells[2].FindControl("ddlLista");
        if (ddlLista != null)
        {
            List<Xpinn.Reporteador.Entities.Lista> lstLista = new List<Xpinn.Reporteador.Entities.Lista>();
            Xpinn.Reporteador.Entities.Lista pLista = new Xpinn.Reporteador.Entities.Lista();            
            Usuario pUsuario = new Usuario();
            pUsuario = (Usuario)Session["Usuario"];
            lstLista = ReporteServicio.ListarLista(pLista, pUsuario);
            ddlLista.DataSource = lstLista;
            ddlLista.DataTextField = "descripcion";
            ddlLista.DataValueField = "idlista";
            ddlLista.DataBind();
        }
        // Activar/Desactivar botones
        VerificaBotones();
    }

    protected void gvCondiciones_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("Add"))
        {
            List<Condicion> lstCondicion = new List<Condicion>();
            if (Session["LSTCONDICIONES"] != null)
                lstCondicion = (List<Condicion>)Session["LSTCONDICIONES"];
            Condicion eCondicion = new Condicion();
            eCondicion.idcondicion = 0;
            eCondicion.tipo1 = 1;
            RadioButton rbValor0 = (RadioButton)gvCondiciones.FooterRow.FindControl("rbValor0");
            RadioButton rbColumna0 = (RadioButton)gvCondiciones.FooterRow.FindControl("rbColumna0");
            if (rbColumna0.Checked == true)
                eCondicion.tipo1 = 1;
            else if (rbValor0.Checked == true)
                eCondicion.tipo1 = 2;
            eCondicion.parentesisizq = "";
            ImageButtonGrid btnParentesisIzq = (ImageButtonGrid)gvCondiciones.FooterRow.Cells[2].FindControl("btnParentesisIzq");
            if (btnParentesisIzq != null)                
                if (btnParentesisIzq.ImageUrl == "~/Images/ParentesisIzqAct.jpg")
                    eCondicion.parentesisizq = "(";
            eCondicion.andor = "AND";
            ImageButtonGrid btnAndOr = (ImageButtonGrid)gvCondiciones.FooterRow.Cells[2].FindControl("btnAndOr");
            if (btnAndOr != null)
            {
                if (btnAndOr.Visible == true)
                {
                    if (btnAndOr.ImageUrl != "~/Images/And.jpg")
                        eCondicion.andor = "OR";
                }
                else
                {
                    eCondicion.andor = "";
                }
            }
            if (eCondicion.tipo1 == 1)
            {
                DropDownList ddlTablaCondicion1 = new DropDownList();
                ddlTablaCondicion1 = (DropDownList)gvCondiciones.FooterRow.FindControl("ddlTablaCondicion1");
                if (ddlTablaCondicion1 != null)
                    eCondicion.tabla1 = ddlTablaCondicion1.SelectedItem.Text;
                DropDownList ddlColumna1 = new DropDownList();
                ddlColumna1 = (DropDownList)gvCondiciones.FooterRow.FindControl("ddlColumna1");
                if (ddlColumna1 != null)
                    eCondicion.columna1 = ddlColumna1.SelectedItem.Text;
            }
            else
            {
                eCondicion.tabla1 = "";
                eCondicion.columna1 = "";
            }
            TextBox txtValor1 = new TextBox();
            txtValor1 = (TextBox)gvCondiciones.FooterRow.FindControl("txtValor1");
            if (txtValor1 != null)
                eCondicion.valor1 = txtValor1.Text;
            RadioButton rbValor = (RadioButton)gvCondiciones.FooterRow.FindControl("rbValor");
            RadioButton rbParametro = (RadioButton)gvCondiciones.FooterRow.FindControl("rbParametro");
            RadioButton rbLista = (RadioButton)gvCondiciones.FooterRow.FindControl("rbLista");
            RadioButton rbColumna = new RadioButton();
            rbColumna = (RadioButton)gvCondiciones.FooterRow.FindControl("rbColumna");
            if (rbColumna.Checked == true)
                eCondicion.tipo2 = 1;
            else if (rbValor.Checked == true)
                eCondicion.tipo2 = 2;
            else if (rbParametro.Checked == true)
                eCondicion.tipo2 = 3;
            else if (rbLista.Checked == true)
                eCondicion.tipo2 = 4;
            else
                eCondicion.tipo2 = 1;
            DropDownList ddlOperador = new DropDownList();
            ddlOperador = (DropDownList)gvCondiciones.FooterRow.FindControl("ddlOperador");
            if (ddlOperador != null)
                if (ddlOperador.SelectedItem != null)
                    eCondicion.operador = ddlOperador.SelectedItem.Text;
            if (eCondicion.tipo2 == 1)
            {
                DropDownList ddlTablaCondicion2 = new DropDownList();
                ddlTablaCondicion2 = (DropDownList)gvCondiciones.FooterRow.FindControl("ddlTablaCondicion2");
                if (ddlTablaCondicion2 != null)
                    if (ddlTablaCondicion2.SelectedItem != null)
                        eCondicion.tabla2 = ddlTablaCondicion2.SelectedItem.Text;
                DropDownList ddlColumna2 = new DropDownList();
                ddlColumna2 = (DropDownList)gvCondiciones.FooterRow.FindControl("ddlColumna2");
                if (ddlColumna2 != null)
                    if (ddlColumna2.SelectedItem != null)
                        eCondicion.columna2 = ddlColumna2.SelectedItem.Text;
            }
            else
            {
                eCondicion.tabla2 = "";
                eCondicion.columna2 = "";
            }
            TextBox txtValor2 = new TextBox();
            txtValor2 = (TextBox)gvCondiciones.FooterRow.FindControl("txtValor2");
            if (txtValor2 != null)
                eCondicion.valor2 = txtValor2.Text;
            eCondicion.parentesisder = "";
            ImageButtonGrid btnParentesisDer = (ImageButtonGrid)gvCondiciones.FooterRow.Cells[2].FindControl("btnParentesisDer");
            if (btnParentesisDer != null)
                if (btnParentesisDer.ImageUrl == "~/Images/ParentesisDerAct.jpg")
                    eCondicion.parentesisder = ")";
            if (eCondicion.tipo2 == 4)
            { 
                DropDownList ddlLista = new DropDownList();
                ddlLista = (DropDownList)gvCondiciones.FooterRow.FindControl("ddlLista");
                if (ddlLista != null)
                    if (ddlLista.SelectedItem != null)
                    {
                        eCondicion.idlista = Convert.ToInt32(ddlLista.SelectedItem.Value);
                        eCondicion.nomlista = ddlLista.SelectedItem.Text;
                    }
            }
            else
            {
                eCondicion.idlista = null;
                eCondicion.nomlista = "";
            }
            int posicion = -1;
            for (int i = 0; i < lstCondicion.Count(); i++)
            {
                if (lstCondicion[i].idcondicion == -1)
                {
                    lstCondicion[i].idcondicion = eCondicion.idcondicion;
                    lstCondicion[i].tipo1 = eCondicion.tipo1;
                    lstCondicion[i].andor = eCondicion.andor;
                    lstCondicion[i].parentesisizq = eCondicion.parentesisizq;
                    lstCondicion[i].tabla1 = eCondicion.tabla1;
                    lstCondicion[i].columna1 = eCondicion.columna1;
                    lstCondicion[i].valor1 = eCondicion.valor1;
                    lstCondicion[i].operador = eCondicion.operador;
                    lstCondicion[i].tipo2 = eCondicion.tipo2;
                    lstCondicion[i].tabla2 = eCondicion.tabla2;
                    lstCondicion[i].columna2 = eCondicion.columna2;
                    lstCondicion[i].valor2 = eCondicion.valor2;
                    lstCondicion[i].parentesisder = eCondicion.parentesisder;
                    lstCondicion[i].idlista = eCondicion.idlista;
                    lstCondicion[i].nomlista = eCondicion.nomlista;
                    posicion = i;
                    i = lstCondicion.Count();
                }
            }
            if (posicion == -1)
                lstCondicion.Add(eCondicion);
            gvCondiciones.DataSource = lstCondicion;
            gvCondiciones.DataBind();
            Session["LSTCONDICIONES"] = lstCondicion;
            LimpiaBotonesCondiciones();
        }
        if (e.CommandName.Equals("Deleted"))
        {
            try
            {
                int RowIndex = Convert.ToInt32(e.CommandArgument.ToString());
                List<Condicion> lstCondicion = new List<Condicion>();
                lstCondicion = (List<Condicion>)Session["LSTCONDICIONES"];
                if (lstCondicion.Count > RowIndex)
                {
                    lstCondicion.RemoveAt(RowIndex);
                    gvCondiciones.DataSource = lstCondicion;
                    gvCondiciones.DataBind();
                    Session["LSTCONDICIONES"] = lstCondicion;
                    if (lstCondicion.Count < 5)
                        InicializarCondiciones();
                }
            }
            catch
            {
                VerError("");
            }
        }
        VerificaBotones();
        List<Tabla> lstAux = new List<Tabla>();
        // Cargar listado de tablas existentes
        if (Session["LSTTABLAS"] != null)
            lstAux = (List<Tabla>)Session["LSTTABLAS"];
        Session["LSTTABLAS"] = lstAux;
        ActualizarDropDownList(lstAux);
    }

    protected void VerificaBotones()
    {
        Boolean bYaHayRegistros = false;
        List<Condicion> lstCondicion = new List<Condicion>();
        if (Session["LSTCONDICIONES"] != null)
            lstCondicion = (List<Condicion>)Session["LSTCONDICIONES"];        
        if (lstCondicion != null)
        {
            for (int i = 0; i < lstCondicion.Count(); i++)
            {
                if (lstCondicion[i].idcondicion != -1)
                {
                    bYaHayRegistros = true;
                    i = lstCondicion.Count();
                }
            }
        }

        if (gvCondiciones.FooterRow != null)
        {
            ImageButtonGrid btnParentesisIzq = new ImageButtonGrid();
            btnParentesisIzq = (ImageButtonGrid)gvCondiciones.FooterRow.Cells[2].FindControl("btnParentesisIzq");
            if (btnParentesisIzq != null)
                if (bYaHayRegistros == true)
                    btnParentesisIzq.Visible = true;
                else
                    btnParentesisIzq.Visible = false;

            ImageButtonGrid btnParentesisDer = new ImageButtonGrid();
            btnParentesisDer = (ImageButtonGrid)gvCondiciones.FooterRow.Cells[2].FindControl("btnParentesisDer");
            if (btnParentesisDer != null)
                if (bYaHayRegistros == true)
                    btnParentesisDer.Visible = true;
                else
                    btnParentesisDer.Visible = false;

            ImageButtonGrid btnAndOr = new ImageButtonGrid();
            btnAndOr = (ImageButtonGrid)gvCondiciones.FooterRow.Cells[2].FindControl("btnAndOr");
            if (btnAndOr != null)
                if (bYaHayRegistros == true)
                    btnAndOr.Visible = true;
                else
                    btnAndOr.Visible = false;
        }
    }

    protected void LimpiaBotonesCondiciones()
    {
        if (gvCondiciones.FooterRow != null)
        {
            ImageButtonGrid btnParentesisIzq = new ImageButtonGrid();
            btnParentesisIzq = (ImageButtonGrid)gvCondiciones.FooterRow.Cells[2].FindControl("btnParentesisIzq");
            if (btnParentesisIzq != null)
                btnParentesisIzq.ImageUrl = "~/Images/ParentesisIzqIna.jpg";

            ImageButtonGrid btnParentesisDer = new ImageButtonGrid();
            btnParentesisDer = (ImageButtonGrid)gvCondiciones.FooterRow.Cells[2].FindControl("btnParentesisDer");
            if (btnParentesisDer != null)
                btnParentesisDer.ImageUrl = "~/Images/ParentesisDerIna.jpg";

            ImageButtonGrid btnAndOr = new ImageButtonGrid();
            btnAndOr = (ImageButtonGrid)gvCondiciones.FooterRow.Cells[2].FindControl("btnAndOr");
            if (btnAndOr != null)
                btnAndOr.ImageUrl = "~/Images/And.jpg";
        }
    }

    protected void gvCondiciones_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        Label lblidCondicion = (Label)e.Row.Cells[1].FindControl("lblidCondicion");
        if (lblidCondicion != null)
        {
            if (lblidCondicion.Text == "-1")
            {
                ImageButton btnEliminar = (ImageButton)e.Row.Cells[0].FindControl("btnEliminar");
                btnEliminar.Visible = false;
                Label lblOperador = (Label)e.Row.Cells[2].FindControl("lblOperador");
                if (lblOperador != null)
                    lblOperador.Visible = false;
            }
        }
    }

    protected void ddlTablaCondicion1_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddlTablaCondicion1 = (DropDownList)gvCondiciones.FooterRow.Cells[2].FindControl("ddlTablaCondicion1");
        if (ddlTablaCondicion1 != null)
        {
            Usuario pUsuario = new Usuario();
            pUsuario = (Usuario)Session["Usuario"];
            List<Columna> lstColumnas = new List<Columna>();
            Columna pColumna = new Columna();
            if (ddlTablaCondicion1.SelectedItem != null)
            {
                pColumna.tabla = ddlTablaCondicion1.SelectedItem.Text;
                lstColumnas = ReporteServicio.ListarColumnaBase(ddlTablaCondicion1.SelectedItem.Text, pColumna, pUsuario);
            }
            DropDownList ddlColumna1 = (DropDownList)gvCondiciones.FooterRow.Cells[2].FindControl("ddlColumna1");
            if (ddlColumna1 != null)
            {
                ddlColumna1.DataSource = lstColumnas;
                ddlColumna1.DataTextField = "nombre";
                ddlColumna1.DataValueField = "idcolumna";
                ddlColumna1.DataBind();
            }
        }
    }

    protected void ddlTablaCondicion2_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddlTablaCondicion2 = (DropDownList)gvCondiciones.FooterRow.Cells[2].FindControl("ddlTablaCondicion2");
        if (ddlTablaCondicion2 != null)
        {
            Usuario pUsuario = new Usuario();
            pUsuario = (Usuario)Session["Usuario"];
            List<Columna> lstColumnas = new List<Columna>();
            Columna pColumna = new Columna();
            if (ddlTablaCondicion2.SelectedItem != null)
            {
                pColumna.tabla = ddlTablaCondicion2.SelectedItem.Text;
                lstColumnas = ReporteServicio.ListarColumnaBase(ddlTablaCondicion2.SelectedItem.Text, pColumna, pUsuario);
            }
            DropDownList ddlColumna2 = (DropDownList)gvCondiciones.FooterRow.Cells[2].FindControl("ddlColumna2");
            if (ddlColumna2 != null)
            {
                ddlColumna2.DataSource = lstColumnas;
                ddlColumna2.DataTextField = "nombre";
                ddlColumna2.DataValueField = "idcolumna";
                ddlColumna2.DataBind();
            }
        }
    }

    protected void ActualizarCondicion1(Boolean bColumna)
    {
        if (bColumna == true)
        {
            TextBox txtValor1 = (TextBox)gvCondiciones.FooterRow.Cells[2].FindControl("txtValor1");
            if (txtValor1 != null)
                txtValor1.Visible = false;
            DropDownList ddlColumna1 = (DropDownList)gvCondiciones.FooterRow.Cells[2].FindControl("ddlColumna1");
            if (ddlColumna1 != null)
                ddlColumna1.Visible = true;
            DropDownList ddlTablaCondicion1 = (DropDownList)gvCondiciones.FooterRow.Cells[2].FindControl("ddlTablaCondicion1");
            if (ddlTablaCondicion1 != null)
                ddlTablaCondicion1.Visible = true;
        }
        else
        {
            TextBox txtValor1 = (TextBox)gvCondiciones.FooterRow.Cells[2].FindControl("txtValor1");
            if (txtValor1 != null)
                txtValor1.Visible = true;
            DropDownList ddlColumna1 = (DropDownList)gvCondiciones.FooterRow.Cells[2].FindControl("ddlColumna1");
            if (ddlColumna1 != null)
                ddlColumna1.Visible = false;
            DropDownList ddlTablaCondicion1 = (DropDownList)gvCondiciones.FooterRow.Cells[2].FindControl("ddlTablaCondicion1");
            if (ddlTablaCondicion1 != null)
                ddlTablaCondicion1.Visible = false;
        }

    }

    protected void ActualizarCondicion2(Boolean bColumna, Boolean bLista)
    {

        CheckBox rbLista = (CheckBox)gvCondiciones.FooterRow.Cells[2].FindControl("rbLista");

        if (bColumna == true    )
        {
            TextBox txtValor2 = (TextBox)gvCondiciones.FooterRow.Cells[2].FindControl("txtValor2");
            if (txtValor2 != null)
                txtValor2.Visible = false;
            DropDownList ddlTablaCondicion2 = (DropDownList)gvCondiciones.FooterRow.Cells[2].FindControl("ddlTablaCondicion2");
            if (ddlTablaCondicion2 != null)
                ddlTablaCondicion2.Visible = true;
            DropDownList ddlColumna2 = (DropDownList)gvCondiciones.FooterRow.Cells[2].FindControl("ddlColumna2");
            if (ddlColumna2 != null)
                ddlColumna2.Visible = true;
            if (rbLista.Checked == true)
            {
                DropDownList ddlLista = (DropDownList)gvCondiciones.FooterRow.Cells[2].FindControl("ddlLista");
                if (ddlLista != null)
                    ddlLista.Visible = false;
            }
           
        }
        else
        {
            if (bLista == true)
            {
                DropDownList ddlLista = (DropDownList)gvCondiciones.FooterRow.Cells[2].FindControl("ddlLista");
                if (ddlLista != null)
                {
                    ddlLista.Visible = true;
                    List<Xpinn.Reporteador.Entities.Lista> lstLista = new List<Xpinn.Reporteador.Entities.Lista>();
                    Xpinn.Reporteador.Entities.Lista pLista = new Xpinn.Reporteador.Entities.Lista();
                    Usuario pUsuario = new Usuario();
                    pUsuario = (Usuario)Session["Usuario"];
                    lstLista = ReporteServicio.ListarLista(pLista, pUsuario);
                    ddlLista.DataSource = lstLista;
                    ddlLista.DataTextField = "descripcion";
                    ddlLista.DataValueField = "idlista";
                    ddlLista.DataBind();
                }
                DropDownList ddlTablaCondicion2 = (DropDownList)gvCondiciones.FooterRow.Cells[2].FindControl("ddlTablaCondicion2");
                if (ddlTablaCondicion2 != null)
                    ddlTablaCondicion2.Visible = false;
                DropDownList ddlColumna2 = (DropDownList)gvCondiciones.FooterRow.Cells[2].FindControl("ddlColumna2");
                if (ddlColumna2 != null)
                    ddlColumna2.Visible = false;
            }
            else
            {
                TextBox txtValor2 = (TextBox)gvCondiciones.FooterRow.Cells[2].FindControl("txtValor2");
                if (txtValor2 != null)
                    txtValor2.Visible = true;
                DropDownList ddlTablaCondicion2 = (DropDownList)gvCondiciones.FooterRow.Cells[2].FindControl("ddlTablaCondicion2");
                if (ddlTablaCondicion2 != null)
                    ddlTablaCondicion2.Visible = false;
                DropDownList ddlColumna2 = (DropDownList)gvCondiciones.FooterRow.Cells[2].FindControl("ddlColumna2");
                if (ddlColumna2 != null)
                    ddlColumna2.Visible = false;
                DropDownList ddlLista = (DropDownList)gvCondiciones.FooterRow.Cells[2].FindControl("ddlLista");
                if (ddlLista != null)
                    ddlLista.Visible = false;
            }
        }

    }

    protected void rbColumna0_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox rbColumna0 = (CheckBox)sender;
        if (rbColumna0 != null)
        {
            CheckBox rbValor0 = (CheckBox)gvCondiciones.FooterRow.Cells[2].FindControl("rbValor0");
            if (rbValor0 != null)
            {
                if (rbColumna0.Checked == true)
                    rbValor0.Checked = false;
                else
                    rbValor0.Checked = true;
            }
            ActualizarCondicion1(rbColumna0.Checked);
        }
    }

    protected void rbValor0_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox rbValor0 = (CheckBox)sender;
        if (rbValor0 != null)
        {
            CheckBox rbColumna0 = (CheckBox)gvCondiciones.FooterRow.Cells[2].FindControl("rbColumna0");
            if (rbColumna0 != null)
            {
                if (rbValor0.Checked == true)
                    rbColumna0.Checked = false;
                else
                    rbColumna0.Checked = true;
            }
            ActualizarCondicion1(rbColumna0.Checked);
        }
    }

    protected void rbColumna_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox rbColumna = (CheckBox)sender;
        if (rbColumna != null)
        {
            CheckBox rbValor = (CheckBox)gvCondiciones.FooterRow.Cells[2].FindControl("rbValor");
            if (rbValor != null)
            {
                if (rbColumna.Checked == true)
                    rbValor.Checked = false;
                else
                    rbValor.Checked = true;
            }
            CheckBox rbParametro = (CheckBox)gvCondiciones.FooterRow.Cells[2].FindControl("rbParametro");
            if (rbParametro != null)
            {
                if (rbColumna.Checked == true)
                    rbParametro.Checked = false;
                else
                    rbParametro.Checked = true;
            }
            CheckBox rbLista = (CheckBox)gvCondiciones.FooterRow.Cells[2].FindControl("rbLista");
            if (rbLista != null)
            {
                if (rbColumna.Checked == true)
                    rbLista.Checked = false;
                else
                    rbLista.Checked = true;
            }
            ActualizarCondicion2(rbColumna.Checked, rbLista.Checked);
        }        
    }

    protected void rbValor_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox rbValor = (CheckBox)sender;
        if (rbValor != null)
        {
            CheckBox rbColumna = (CheckBox)gvCondiciones.FooterRow.Cells[2].FindControl("rbColumna");
            if (rbColumna != null)
            {
                if (rbValor.Checked == true)
                    rbColumna.Checked = false;
                else
                    rbColumna.Checked = true;
            }
            CheckBox rbParametro = (CheckBox)gvCondiciones.FooterRow.Cells[2].FindControl("rbParametro");
            if (rbParametro != null)
            {
                if (rbValor.Checked == true)
                    rbParametro.Checked = false;
                else
                    rbParametro.Checked = true;
            }
            CheckBox rbLista = (CheckBox)gvCondiciones.FooterRow.Cells[2].FindControl("rbLista");
            if (rbLista != null)
            {
                if (rbValor.Checked == true)
                    rbLista.Checked = false;
                else
                    rbLista.Checked = true;
            }
            ActualizarCondicion2(rbColumna.Checked, rbLista.Checked);
            
        }        
    }

    protected void rbParametro_CheckedChanged(object sender, EventArgs e)
    {
        TextBox txtValor2 = (TextBox)gvCondiciones.FooterRow.Cells[2].FindControl("txtValor2");
        txtValor2.Text = "";
        txtValor2.Enabled = true;
        CheckBox rbParametro = (CheckBox)sender;
        if (rbParametro != null)
        {
            CheckBox rbColumna = (CheckBox)gvCondiciones.FooterRow.Cells[2].FindControl("rbColumna");
            if (rbColumna != null)
            {
                if (rbParametro.Checked == true)
                    rbColumna.Checked = false;
                else
                    rbColumna.Checked = true;
            }
            CheckBox rbValor = (CheckBox)gvCondiciones.FooterRow.Cells[2].FindControl("rbValor");
            if (rbValor != null)
            {
                if (rbParametro.Checked == true)
                    rbValor.Checked = false;
                else
                    rbValor.Checked = true;
            }
            CheckBox rbLista = (CheckBox)gvCondiciones.FooterRow.Cells[2].FindControl("rbLista");
            if (rbLista != null)
            {
                if (rbParametro.Checked == true)
                    rbLista.Checked = false;
                else
                    rbLista.Checked = true;
            }
            Label lblTitParametro = (Label)gvCondiciones.FooterRow.Cells[2].FindControl("lblTitParametro");
            if (rbParametro.Checked == true)
                lblTitParametro.Visible = true;
            else
                lblTitParametro.Visible = false;
            ActualizarCondicion2(rbColumna.Checked, rbLista.Checked);

        }
    }

    protected void rbLista_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox rbLista = (CheckBox)sender;
        if (rbLista != null)
        {
            CheckBox rbColumna = (CheckBox)gvCondiciones.FooterRow.Cells[2].FindControl("rbColumna");
            if (rbColumna != null)
            {
                if (rbLista.Checked == true)
                    rbColumna.Checked = false;
                else
                    rbColumna.Checked = true;
            }
            CheckBox rbValor = (CheckBox)gvCondiciones.FooterRow.Cells[2].FindControl("rbValor");
            if (rbValor != null)
            {
                if (rbLista.Checked == true)
                    rbValor.Checked = false;
                else
                    rbValor.Checked = true;
            }
            CheckBox rbParametro = (CheckBox)gvCondiciones.FooterRow.Cells[2].FindControl("rbParametro");
            if (rbParametro != null)
            {
                if (rbLista.Checked == true)
                    rbParametro.Checked = false;
                else
                    rbParametro.Checked = true;
            }
            TextBox txtValor2 = (TextBox)gvCondiciones.FooterRow.Cells[2].FindControl("txtValor2");
            txtValor2.Visible = true;
           
            ActualizarCondicion2(rbColumna.Checked, rbLista.Checked);
        }
    }

    protected void btnParentesisIzq_Click(object sender, ImageClickEventArgs e)
    {
        ImageButtonGrid btnParentesisIzq = new ImageButtonGrid();
        btnParentesisIzq = (ImageButtonGrid)gvCondiciones.FooterRow.Cells[2].FindControl("btnParentesisIzq");
        if (btnParentesisIzq != null)
        {
            if (btnParentesisIzq.ImageUrl == "~/Images/ParentesisIzqAct.jpg")
                btnParentesisIzq.ImageUrl = "~/Images/ParentesisIzqIna.jpg";
            else
                btnParentesisIzq.ImageUrl = "~/Images/ParentesisIzqAct.jpg";
        }
    }

    protected void btnParentesisDer_Click(object sender, ImageClickEventArgs e)
    {
        ImageButtonGrid btnParentesisDer = new ImageButtonGrid();
        btnParentesisDer = (ImageButtonGrid)gvCondiciones.FooterRow.Cells[2].FindControl("btnParentesisDer");
        if (btnParentesisDer != null)
        {
            if (btnParentesisDer.ImageUrl == "~/Images/ParentesisDerAct.jpg")
                btnParentesisDer.ImageUrl = "~/Images/ParentesisDerIna.jpg";
            else
                btnParentesisDer.ImageUrl = "~/Images/ParentesisDerAct.jpg";
        }
    }

    protected void btnAndOr_Click(object sender, ImageClickEventArgs e)
    {
        ImageButtonGrid btnAndOr = new ImageButtonGrid();
        btnAndOr = (ImageButtonGrid)gvCondiciones.FooterRow.Cells[2].FindControl("btnAndOr");
        if (btnAndOr != null)
        {
            if (btnAndOr.ImageUrl == "~/Images/And.jpg")
                btnAndOr.ImageUrl = "~/Images/Or.jpg";
            else
                btnAndOr.ImageUrl = "~/Images/And.jpg";
        }
    }

    #endregion

    #region columnareporte

    protected void CargarDllFormato()
    {
        DropDownList ddlCOLFormato = (DropDownList)gvColumnas.FooterRow.Cells[2].FindControl("ddlCOLFormato");
        if (ddlCOLFormato != null)
        {
            ddlCOLFormato.Items.Clear();
            Usuario pUsuario = new Usuario();
            pUsuario = (Usuario)Session["Usuario"];
            Columna eColumna = new Columna();
            DropDownList ddlCOLTabla = (DropDownList)gvColumnas.FooterRow.Cells[2].FindControl("ddlCOLTabla");
            DropDownList ddlCOLColumna = (DropDownList)gvColumnas.FooterRow.Cells[2].FindControl("ddlCOLColumna");
            if (ddlCOLTabla != null && ddlCOLColumna != null)
            {
                if (ddlCOLTabla.SelectedItem != null)
                {
                    eColumna = ReporteServicio.ConsultarColumna(ddlCOLTabla.SelectedItem.Text, ddlCOLColumna.SelectedItem.Text, pUsuario);
                    TextBox txtTipoDato = (TextBox)gvColumnas.FooterRow.Cells[2].FindControl("txtTipoDato");
                    txtTipoDato.Text = eColumna.tipo_dato;
                    Formato eFormato = new Formato();
                    eFormato.tipo_dato = ReporteServicio.CodTipoDato(eColumna.tipo_dato); ;
                    ddlCOLFormato.DataSource = ReporteServicio.ListarFormato(eFormato, pUsuario);
                    ddlCOLFormato.DataTextField = "descripcion";
                    ddlCOLFormato.DataValueField = "formato";
                    ddlCOLFormato.DataBind();
                }
            }            
        }
    }

    protected void CargarDllFormatoE()
    {
        DropDownList ddlCOLFormatoE = (DropDownList)gvColumnas.Rows[gvColumnas.EditIndex].Cells[2].FindControl("ddlCOLFormatoE");
        if (ddlCOLFormatoE != null)
        {
            ddlCOLFormatoE.Items.Clear();
            Usuario pUsuario = new Usuario();
            pUsuario = (Usuario)Session["Usuario"];
            Columna eColumna = new Columna();
            DropDownList ddlCOLTablaE = (DropDownList)gvColumnas.Rows[gvColumnas.EditIndex].Cells[2].FindControl("ddlCOLTablaE");
            DropDownList ddlCOLColumnaE = (DropDownList)gvColumnas.Rows[gvColumnas.EditIndex].Cells[2].FindControl("ddlCOLColumnaE");
            if (ddlCOLTablaE != null && ddlCOLColumnaE != null)
            {
                if (ddlCOLTablaE.SelectedItem != null)
                {
                    eColumna = ReporteServicio.ConsultarColumna(ddlCOLTablaE.SelectedItem.Text, ddlCOLColumnaE.SelectedItem.Text, pUsuario);
                    TextBox txtTipoDato = (TextBox)gvColumnas.Rows[gvColumnas.EditIndex].Cells[2].FindControl("txtTipoDatoE");
                    txtTipoDato.Text = eColumna.tipo_dato;
                    Formato eFormato = new Formato();
                    eFormato.tipo_dato = ReporteServicio.CodTipoDato(eColumna.tipo_dato); ;
                    ddlCOLFormatoE.DataSource = ReporteServicio.ListarFormato(eFormato, pUsuario);
                    ddlCOLFormatoE.DataTextField = "descripcion";
                    ddlCOLFormatoE.DataValueField = "formato";
                    ddlCOLFormatoE.DataBind();
                }
            }
        }
    }

    protected void InicializarColumnaReporte()
    {
        List<ColumnaReporte> lstColumnaReporte = new List<ColumnaReporte>();
        if (Session["LSTCOLUMNAS"] != null)
            lstColumnaReporte = (List<ColumnaReporte>)Session["LSTCOLUMNAS"];
        for (int i = 1; i <= 5; i++)
        {
            ColumnaReporte eColumnaReporte = new ColumnaReporte();
            eColumnaReporte.idcolumna = -1;
            eColumnaReporte.orden = 99999;
            lstColumnaReporte.Add(eColumnaReporte);
        }
        gvColumnas.DataSource = lstColumnaReporte;
        gvColumnas.DataBind();
        Session["LSTCOLUMNAS"] = lstColumnaReporte;
    }

    protected void gvColumnas_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("Add"))
        {
            List<ColumnaReporte> lstColumnaReporte = new List<ColumnaReporte>();
            if (Session["LSTCOLUMNAS"] != null)
                lstColumnaReporte = (List<ColumnaReporte>)Session["LSTCOLUMNAS"];
            ColumnaReporte eColumnaReporte = new ColumnaReporte();
            eColumnaReporte.idcolumna = 0;

            RadioButton rbCOLColumna = new RadioButton();
            rbCOLColumna = (RadioButton)gvColumnas.FooterRow.FindControl("rbCOLColumna");
            RadioButton rbCOLFormula = new RadioButton();
            rbCOLFormula = (RadioButton)gvColumnas.FooterRow.FindControl("rbCOLFormula");
            if (rbCOLColumna != null)
                if (rbCOLColumna.Checked == true)
                {
                    eColumnaReporte.tipo = "Columna";
                }
                else
                {
                    if (rbCOLFormula.Checked == true)
                        eColumnaReporte.tipo = "Formula";
                    else
                        eColumnaReporte.tipo = "Constante";
                }
            CheckBox cbTotal = new CheckBox();
            cbTotal = (CheckBox)gvColumnas.FooterRow.FindControl("cbTotal");
            if (cbTotal != null)
                eColumnaReporte.total = cbTotal.Checked;
            if (eColumnaReporte.tipo == "Constante")
            {
                eColumnaReporte.tabla = "";
                eColumnaReporte.formula = "";
                TextBox txtCOLColumna = new TextBox();
                txtCOLColumna = (TextBox)gvColumnas.FooterRow.FindControl("txtCOLColumna");
                if (txtCOLColumna != null)
                    if (txtCOLColumna.Text != null)
                        eColumnaReporte.columna = txtCOLColumna.Text;
            }
            else
            {
                DropDownList ddlCOLTabla = new DropDownList();
                ddlCOLTabla = (DropDownList)gvColumnas.FooterRow.FindControl("ddlCOLTabla");
                if (ddlCOLTabla != null)
                    if (ddlCOLTabla.SelectedItem != null)
                        eColumnaReporte.tabla = ddlCOLTabla.SelectedItem.Text;
                DropDownList ddlCOLColumna = new DropDownList();
                ddlCOLColumna = (DropDownList)gvColumnas.FooterRow.FindControl("ddlCOLColumna");
                if (ddlCOLColumna != null)
                    if (ddlCOLColumna.SelectedItem != null)
                        eColumnaReporte.columna = ddlCOLColumna.SelectedItem.Text;
                DropDownList ddlCOLFormula = new DropDownList();
                ddlCOLFormula = (DropDownList)gvColumnas.FooterRow.FindControl("ddlCOLFormula");
                if (ddlCOLFormula != null)
                    if (ddlCOLFormula.SelectedItem != null)
                        eColumnaReporte.formula = ddlCOLFormula.SelectedItem.Text;
            }
            TextBox txtTipoDato = new TextBox();
            txtTipoDato = (TextBox)gvColumnas.FooterRow.FindControl("txtTipoDato");
            if (txtTipoDato != null)
                eColumnaReporte.tipodato = txtTipoDato.Text;
            TextBox txtTitulo = new TextBox();
            txtTitulo = (TextBox)gvColumnas.FooterRow.FindControl("txtTitulo");
            if (txtTitulo != null)
                eColumnaReporte.titulo = txtTitulo.Text;
            DropDownList ddlCOLFormato = new DropDownList();
            ddlCOLFormato = (DropDownList)gvColumnas.FooterRow.FindControl("ddlCOLFormato");
            if (ddlCOLFormato != null)
                if (ddlCOLFormato.SelectedItem != null)
                    eColumnaReporte.formato = ddlCOLFormato.SelectedItem.Value;
                else
                    eColumnaReporte.formato = "";
            DropDownList ddlCOLAlineacion = new DropDownList();
            ddlCOLAlineacion = (DropDownList)gvColumnas.FooterRow.FindControl("ddlCOLAlineacion");
            if (ddlCOLAlineacion != null)
                if (ddlCOLAlineacion.SelectedItem != null)
                    eColumnaReporte.alineacion = ddlCOLAlineacion.SelectedItem.Text;
            TextBox txtAncho = new TextBox();
            txtAncho = (TextBox)gvColumnas.FooterRow.FindControl("txtAncho");
            if (txtAncho != null)
                if (txtAncho.Text.Trim() != "")
                    eColumnaReporte.ancho = Convert.ToInt32(txtAncho.Text);

            int contador = 0;
            int maxOrden = 0;
            for (int i = 0; i < lstColumnaReporte.Count(); i++)
            {
                if (lstColumnaReporte[i].idcolumna != -1)
                {
                    if (lstColumnaReporte[i].orden > maxOrden)
                        maxOrden = Convert.ToInt32(lstColumnaReporte[i].orden);
                    contador += 1;
                }
            }
            if (maxOrden < contador)
                maxOrden = contador;
            int posicion = -1;
            for (int i = 0; i < lstColumnaReporte.Count(); i++)
            {
                if (lstColumnaReporte[i].idcolumna == -1)
                {
                    lstColumnaReporte[i].idcolumna = eColumnaReporte.idcolumna;
                    lstColumnaReporte[i].orden = maxOrden + 1;
                    lstColumnaReporte[i].tipo = eColumnaReporte.tipo;
                    lstColumnaReporte[i].total = eColumnaReporte.total;
                    lstColumnaReporte[i].tabla = eColumnaReporte.tabla;
                    lstColumnaReporte[i].columna = eColumnaReporte.columna;
                    lstColumnaReporte[i].tipodato = eColumnaReporte.tipodato;
                    lstColumnaReporte[i].titulo = eColumnaReporte.titulo;
                    lstColumnaReporte[i].formato = eColumnaReporte.formato;
                    lstColumnaReporte[i].alineacion = eColumnaReporte.alineacion;
                    lstColumnaReporte[i].ancho = eColumnaReporte.ancho;
                    lstColumnaReporte[i].formula = eColumnaReporte.formula;
                    posicion = i;
                    i = lstColumnaReporte.Count();
                }
            }
            if (posicion == -1)
            {
                eColumnaReporte.orden = 1;
                lstColumnaReporte.Add(eColumnaReporte);
            }
            gvColumnas.DataSource = lstColumnaReporte;
            gvColumnas.DataBind();
            Session["LSTCOLUMNAS"] = lstColumnaReporte;
        }
        if (e.CommandName.Equals("Deleted"))
        {
            try
            {
                int RowIndex = Convert.ToInt32(e.CommandArgument.ToString());
                List<ColumnaReporte> lstColumnaReporte = new List<ColumnaReporte>();
                lstColumnaReporte = (List<ColumnaReporte>)Session["LSTCOLUMNAS"];
                if (lstColumnaReporte.Count > RowIndex)
                {
                    lstColumnaReporte.RemoveAt(RowIndex);
                    gvColumnas.DataSource = lstColumnaReporte;
                    gvColumnas.DataBind();
                    Session["LSTCOLUMNAS"] = lstColumnaReporte;
                    if (lstColumnaReporte.Count < 5)
                        InicializarColumnaReporte();
                }
            }
            catch
            {
                VerError("");
            }
        }
        if (e.CommandName.Equals("Up"))
        {
            try
            {
                int RowIndex = Convert.ToInt32(e.CommandArgument.ToString());
                List<ColumnaReporte> lstColumnaReporte = new List<ColumnaReporte>();
                lstColumnaReporte = (List<ColumnaReporte>)Session["LSTCOLUMNAS"];
                if (lstColumnaReporte.Count > RowIndex)
                {
                    if (lstColumnaReporte[RowIndex].orden > 0 && RowIndex > 0)
                    {
                        Boolean bIguales = false;
                        int ordenAux = Convert.ToInt32(lstColumnaReporte[RowIndex - 1].orden);
                        if (lstColumnaReporte[RowIndex - 1].orden == lstColumnaReporte[RowIndex].orden)
                            bIguales = true;
                        lstColumnaReporte[RowIndex - 1].orden = lstColumnaReporte[RowIndex].orden;
                        lstColumnaReporte[RowIndex].orden = ordenAux;
                        lstColumnaReporte = lstColumnaReporte.OrderBy(columna => columna.orden).ToList();
                        if (bIguales)
                        {
                            ordenAux = 1;
                            for (int i = 0; i < lstColumnaReporte.Count; i++)
                            {
                                if (lstColumnaReporte[i].idcolumna != -1)
                                {
                                    lstColumnaReporte[i].orden = ordenAux;
                                    ordenAux += 1;
                                }
                            }
                        }
                    }
                    gvColumnas.DataSource = lstColumnaReporte;
                    gvColumnas.DataBind();
                    Session["LSTCOLUMNAS"] = lstColumnaReporte;
                    if (lstColumnaReporte.Count < 5)
                        InicializarColumnaReporte();
                }
            }
            catch
            {
                VerError("");
            }
        }
        if (e.CommandName.Equals("Down"))
        {
            try
            {
                int RowIndex = Convert.ToInt32(e.CommandArgument.ToString());
                List<ColumnaReporte> lstColumnaReporte = new List<ColumnaReporte>();
                lstColumnaReporte = (List<ColumnaReporte>)Session["LSTCOLUMNAS"];
                if (lstColumnaReporte.Count > RowIndex)
                {
                    if (lstColumnaReporte[RowIndex].orden > 0 && RowIndex > 0 && lstColumnaReporte[RowIndex + 1].orden != 99999)
                    {
                        int ordenAux = Convert.ToInt32(lstColumnaReporte[RowIndex + 1].orden);
                        lstColumnaReporte[RowIndex + 1].orden = lstColumnaReporte[RowIndex].orden;
                        lstColumnaReporte[RowIndex].orden = ordenAux;
                        lstColumnaReporte = lstColumnaReporte.OrderBy(columna => columna.orden).ToList();
                    }
                    gvColumnas.DataSource = lstColumnaReporte;
                    gvColumnas.DataBind();
                    Session["LSTCOLUMNAS"] = lstColumnaReporte;
                    if (lstColumnaReporte.Count < 5)
                        InicializarColumnaReporte();
                }
            }
            catch
            {
                VerError("");
            }
        }
        List<Tabla> lstAux = new List<Tabla>();
        // Cargar listado de tablas existentes
        if (Session["LSTTABLAS"] != null)
            lstAux = (List<Tabla>)Session["LSTTABLAS"];
        Session["LSTTABLAS"] = lstAux;
        ActualizarDropDownList(lstAux);
    }

    protected void gvColumnas_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        Label lblidColumna = (Label)e.Row.Cells[1].FindControl("lblidColumna");
        if (lblidColumna != null)
        {
            if (lblidColumna.Text == "-1")
            {
                ImageButton btnEliminar = (ImageButton)e.Row.Cells[0].FindControl("btnEliminar");
                btnEliminar.Visible = false;
                ImageButtonGrid btnSubir = (ImageButtonGrid)e.Row.Cells[0].FindControl("btnSubir");
                btnSubir.Visible = false;
                ImageButtonGrid btnBajar = (ImageButtonGrid)e.Row.Cells[0].FindControl("btnBajar");
                btnBajar.Visible = false;
                CheckBox cbTotal = (CheckBox)e.Row.Cells[0].FindControl("cbTotal");
                cbTotal.Visible = false;
                Label lblOrden = (Label)e.Row.Cells[1].FindControl("lblOrden");
                if (lblOrden != null)
                    if (lblOrden.Text == "99999")
                        lblOrden.Visible = false;
            }
        }
        DataRowView drv = e.Row.DataItem as DataRowView;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if ((e.Row.RowState & DataControlRowState.Edit) > 0)
            {
                DropDownList ddlCOLColumnaE = (DropDownList)e.Row.FindControl("ddlCOLColumnaE");
                if (ddlCOLColumnaE != null)
                {
                    DropDownList ddlCOLTablaE = (DropDownList)e.Row.FindControl("ddlCOLTablaE");
                    Session["TABLA"] = ddlCOLTablaE.SelectedItem.Text;
                    Session["COLUMNA"] = ddlCOLColumnaE.SelectedItem.Text;
                }
            }
        }
    }

    protected void gvColumnas_RowEditing(object sender, GridViewEditEventArgs e)
    {
        VerError("");
        try
        {
            Label lblTabla = (Label)gvColumnas.Rows[e.NewEditIndex].Cells[2].FindControl("lblTabla");
            Session["TABLA"] = lblTabla.Text;
            Label lblColumna = (Label)gvColumnas.Rows[e.NewEditIndex].Cells[2].FindControl("lblColumna");
            Session["COLUMNA"] = lblColumna.Text;
            gvColumnas.EditIndex = e.NewEditIndex;
            gvColumnas.DataSource = Session["LSTCOLUMNAS"];
            gvColumnas.DataBind();
        }
        catch
        {
            VerError("No se pudo editar");
        }
    }

    protected List<Tabla> ListaTablas()
    {
        List<Tabla> LstTabla = new List<Tabla>();
        if (Session["LSTTABLAS"] != null)
            LstTabla = (List<Tabla>)Session["LSTTABLAS"];
        return LstTabla;
    }

    protected List<Columna> ListaColumnas()
    {
        Usuario pUsuario = new Usuario();
        pUsuario = (Usuario)Session["Usuario"];
        List<Columna> lstColumnas = new List<Columna>();
        Columna pColumna = new Columna();
        string tabla = Session["TABLA"].ToString();
        if (tabla != null)
        {
            if (tabla.Trim() != "")
            {
                pColumna.tabla = tabla;
                lstColumnas = ReporteServicio.ListarColumnaBase(tabla, pColumna, pUsuario);
            }
        }
        return lstColumnas;
    }

    protected List<Formato> ListaFormatos()
    {
        Usuario pUsuario = new Usuario();
        pUsuario = (Usuario)Session["Usuario"];
        List<Formato> lstFormato = new List<Formato>();
        Formato pFormato = new Formato();
        string tabla = Session["TABLA"].ToString();
        string columna = Session["COLUMNA"].ToString();
        if (tabla != null && columna != null)
        {
            if (tabla.Trim() != "" && columna.Trim() != "")
            {
                Columna eColumna = new Columna();
                eColumna = ReporteServicio.ConsultarColumna(tabla, columna, pUsuario);                
                Formato eFormato = new Formato();
                eFormato.tipo_dato = ReporteServicio.CodTipoDato(eColumna.tipo_dato); ;
                lstFormato = ReporteServicio.ListarFormato(eFormato, pUsuario);
                if (eFormato.tipo_dato == 1)
                {
                    eFormato.formato = "";
                    eFormato.descripcion = "";
                    lstFormato.Add(eFormato);
                }
            }
        }
        return lstFormato;
    }
  
    protected void gvColumnas_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvColumnas.EditIndex = -1;
        gvColumnas.DataSource = Session["LSTCOLUMNAS"];
        gvColumnas.DataBind();
    }

    protected void gvColumnas_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        // Cargando los datost
        List<ColumnaReporte> lstColumnas = new List<ColumnaReporte>();
        lstColumnas = (List<ColumnaReporte>)Session["LSTCOLUMNAS"];
        GridViewRow gvCol = gvColumnas.Rows[e.RowIndex];
        CheckBox cbTotalE = (CheckBox)gvCol.FindControl("cbTotalE");
        if (cbTotalE != null)
            lstColumnas[e.RowIndex].total = cbTotalE.Checked;
        DropDownList ddlCOLTablaE = (DropDownList)gvCol.FindControl("ddlCOLTablaE");
        if (ddlCOLTablaE != null)
            lstColumnas[e.RowIndex].tabla = ddlCOLTablaE.SelectedItem.Text;
        DropDownList ddlCOLColumnaE = (DropDownList)gvCol.FindControl("ddlCOLColumnaE");
        if (ddlCOLColumnaE != null)
            lstColumnas[e.RowIndex].columna = ddlCOLColumnaE.SelectedItem.Text;
        TextBox txtTipoDatoE = (TextBox)gvCol.FindControl("txtTipoDatoE");
        if (txtTipoDatoE != null)
            lstColumnas[e.RowIndex].tipodato = txtTipoDatoE.Text;
        TextBox txtTituloE = (TextBox)gvCol.FindControl("txtTituloE");
        if (txtTituloE != null)
            lstColumnas[e.RowIndex].titulo = txtTituloE.Text;
        DropDownList ddlCOLFormatoE = (DropDownList)gvCol.FindControl("ddlCOLFormatoE");
        if (ddlCOLFormatoE != null)
            if (ddlCOLFormatoE.SelectedItem != null)
                lstColumnas[e.RowIndex].formato = ddlCOLFormatoE.SelectedItem.Value;
        DropDownList ddlCOLAlineacionE = (DropDownList)gvCol.FindControl("ddlCOLAlineacionE");
        if (ddlCOLAlineacionE != null)
            lstColumnas[e.RowIndex].alineacion = ddlCOLAlineacionE.SelectedItem.Text;
        TextBox txtAnchoE = (TextBox)gvCol.FindControl("txtAnchoE");
        if (txtAnchoE != null)
            if (txtAnchoE.Text != "")
                lstColumnas[e.RowIndex].ancho = Convert.ToInt32(txtAnchoE.Text);

        // Actualizando la grilla            
        gvColumnas.EditIndex = -1;
        gvColumnas.DataSource = lstColumnas;
        gvColumnas.DataBind();

        // Actualizando variable de sesión
        Session["LSTCOLUMNAS"] = lstColumnas;
    }

    protected void ddlCOLTabla_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddlCOLTabla = (DropDownList)gvColumnas.FooterRow.Cells[2].FindControl("ddlCOLTabla");
        if (ddlCOLTabla != null)
        {
            Usuario pUsuario = new Usuario();
            pUsuario = (Usuario)Session["Usuario"];
            List<Columna> lstColumnas = new List<Columna>();
            Columna pColumna = new Columna();
            if (ddlCOLTabla.SelectedItem != null)
            {
                pColumna.tabla = ddlCOLTabla.SelectedItem.Text;
                lstColumnas = ReporteServicio.ListarColumnaBase(ddlCOLTabla.SelectedItem.Text, pColumna, pUsuario);
            }
            DropDownList ddlCOLColumna = (DropDownList)gvColumnas.FooterRow.Cells[2].FindControl("ddlCOLColumna");
            if (ddlCOLColumna != null)
            {
                ddlCOLColumna.DataSource = lstColumnas;
                ddlCOLColumna.DataTextField = "nombre";
                ddlCOLColumna.DataValueField = "idcolumna";
                ddlCOLColumna.DataBind();
            }
            CargarDllFormato();
        }
    }

    protected void ddlCOLTablaE_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddlCOLTablaE = (DropDownList)sender;
        if (ddlCOLTablaE != null)
        {
            Session["ddlCOLTablaE"] = ddlCOLTablaE;
            Usuario pUsuario = new Usuario();
            pUsuario = (Usuario)Session["Usuario"];
            List<Columna> lstColumnas = new List<Columna>();
            Columna pColumna = new Columna();
            if (ddlCOLTablaE.SelectedItem != null)
            {
                pColumna.tabla = ddlCOLTablaE.SelectedItem.Text;
                lstColumnas = ReporteServicio.ListarColumnaBase(ddlCOLTablaE.SelectedItem.Text, pColumna, pUsuario);
            }
            DropDownList ddlCOLColumnaE = (DropDownList)gvColumnas.Rows[gvColumnas.EditIndex].Cells[2].FindControl("ddlCOLColumnaE");
            if (ddlCOLColumnaE != null)
            {
                ddlCOLColumnaE.DataSource = lstColumnas;
                ddlCOLColumnaE.DataTextField = "nombre";
                ddlCOLColumnaE.DataValueField = "idcolumna";
                ddlCOLColumnaE.DataBind();
            }
            // CargarDllFormato();
        }
    }

    protected void ddlCOLColumna_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddlCOLColumna = (DropDownList)gvColumnas.FooterRow.Cells[2].FindControl("ddlCOLColumna");
        if (ddlCOLColumna != null)
        {
            Usuario pUsuario = new Usuario();
            pUsuario = (Usuario)Session["Usuario"];
            Columna eColumna = new Columna();
            DropDownList ddlCOLTabla = (DropDownList)gvColumnas.FooterRow.Cells[2].FindControl("ddlCOLTabla");
            if (ddlCOLTabla != null)
            {
                if (ddlCOLTabla.SelectedItem != null)
                {
                    eColumna = ReporteServicio.ConsultarColumna(ddlCOLTabla.SelectedItem.Text, ddlCOLColumna.SelectedItem.Text, pUsuario);
                    TextBox txtTipoDato = (TextBox)gvColumnas.FooterRow.Cells[2].FindControl("txtTipoDato");
                    txtTipoDato.Text = eColumna.tipo_dato;                    
                }
            }
            CargarDllFormato();
        }
    }

    protected void ddlCOLColumnaE_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddlCOLColumnaE = (DropDownList)sender;
        if (ddlCOLColumnaE != null)
        {
            Usuario pUsuario = new Usuario();
            pUsuario = (Usuario)Session["Usuario"];
            Columna eColumna = new Columna();
            DropDownList ddlCOLTablaE = (DropDownList)gvColumnas.Rows[gvColumnas.EditIndex].Cells[2].FindControl("ddlCOLTablaE");
            if (ddlCOLTablaE != null)
            {
                if (ddlCOLTablaE.SelectedItem != null)
                {
                    eColumna = ReporteServicio.ConsultarColumna(ddlCOLTablaE.SelectedItem.Text, ddlCOLColumnaE.SelectedItem.Text, pUsuario);
                    TextBox txtTipoDatoE = (TextBox)gvColumnas.Rows[gvColumnas.EditIndex].Cells[2].FindControl("txtTipoDatoE");
                    txtTipoDatoE.Text = eColumna.tipo_dato;
                }
            }
            CargarDllFormatoE();
        }
    }

    protected void rbCOLColumna_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox rbCOLColumna = (CheckBox)sender;
        if (rbCOLColumna != null)
        {
            CheckBox rbCOLFormula = (CheckBox)gvColumnas.FooterRow.Cells[2].FindControl("rbCOLFormula");
            if (rbCOLFormula != null)
            {
                if (rbCOLColumna.Checked == true)
                    rbCOLFormula.Checked = false;
                else
                    rbCOLFormula.Checked = true;
            }
            CheckBox rbCOLConstante = (CheckBox)gvColumnas.FooterRow.Cells[2].FindControl("rbCOLConstante");
            if (rbCOLConstante != null)
            {
                if (rbCOLFormula.Checked == true)
                    rbCOLConstante.Checked = false;
                else
                    rbCOLConstante.Checked = true;
            }
            ActualizarColumna1(rbCOLColumna.Checked, rbCOLFormula.Checked);
        }
    }

    protected void rbCOLFormula_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox rbCOLFormula = (CheckBox)sender;
        if (rbCOLFormula != null)
        {
            CheckBox rbCOLColumna = (CheckBox)gvColumnas.FooterRow.Cells[2].FindControl("rbCOLColumna");
            if (rbCOLColumna != null)
            {
                if (rbCOLFormula.Checked == true)
                    rbCOLColumna.Checked = false;
                else
                    rbCOLColumna.Checked = true;
            }
            CheckBox rbCOLConstante = (CheckBox)gvColumnas.FooterRow.Cells[2].FindControl("rbCOLConstante");
            if (rbCOLConstante != null)
            {
                if (rbCOLFormula.Checked == true)
                    rbCOLConstante.Checked = false;
                else
                    rbCOLConstante.Checked = true;
            }
            ActualizarColumna1(rbCOLColumna.Checked, rbCOLFormula.Checked);
        }
    }

    protected void rbCOLConstante_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox rbCOLConstante = (CheckBox)sender;
        if (rbCOLConstante != null)
        {
            CheckBox rbCOLColumna = (CheckBox)gvColumnas.FooterRow.Cells[2].FindControl("rbCOLColumna");
            if (rbCOLColumna != null)
            {
                if (rbCOLConstante.Checked == true)
                    rbCOLColumna.Checked = false;
                else
                    rbCOLColumna.Checked = true;
            }
            CheckBox rbCOLFormula = (CheckBox)gvColumnas.FooterRow.Cells[2].FindControl("rbCOLFormula");
            if (rbCOLFormula != null)
            {
                if (rbCOLConstante.Checked == true)
                    rbCOLFormula.Checked = false;
                else
                    rbCOLFormula.Checked = true;
            }
            ActualizarColumna1(rbCOLColumna.Checked, rbCOLFormula.Checked);
        }
    }

    protected void ActualizarColumna1(Boolean bColumna, Boolean bFormula)
    {
        if (bColumna == true)
        {
            Label lblFormula = (Label)gvColumnas.FooterRow.Cells[2].FindControl("lblFormula");
            if (lblFormula != null)
                lblFormula.Visible = false;
            DropDownList ddlCOLFormula = (DropDownList)gvColumnas.FooterRow.Cells[2].FindControl("ddlCOLFormula");
            if (ddlCOLFormula != null)
                ddlCOLFormula.Visible = false;
            DropDownList ddlCOLTabla = (DropDownList)gvColumnas.FooterRow.Cells[2].FindControl("ddlCOLTabla");
            if (ddlCOLTabla != null)
                ddlCOLTabla.Visible = true;
            DropDownList ddlCOLColumna = (DropDownList)gvColumnas.FooterRow.Cells[2].FindControl("ddlCOLColumna");
            if (ddlCOLColumna != null)
                ddlCOLColumna.Visible = true;
            TextBox txtCOLColumna = (TextBox)gvColumnas.FooterRow.Cells[2].FindControl("txtCOLColumna");
            if (txtCOLColumna != null)
                txtCOLColumna.Visible = false;
        }
        else
        {
            if (bFormula == true)
            {
                Label lblFormula = (Label)gvColumnas.FooterRow.Cells[2].FindControl("lblFormula");
                if (lblFormula != null)
                    lblFormula.Visible = true;
                DropDownList ddlCOLFormula = (DropDownList)gvColumnas.FooterRow.Cells[2].FindControl("ddlCOLFormula");
                if (ddlCOLFormula != null)
                    ddlCOLFormula.Visible = true;
                DropDownList ddlCOLTabla = (DropDownList)gvColumnas.FooterRow.Cells[2].FindControl("ddlCOLTabla");
                if (ddlCOLTabla != null)
                    ddlCOLTabla.Visible = true;
                DropDownList ddlCOLColumna = (DropDownList)gvColumnas.FooterRow.Cells[2].FindControl("ddlCOLColumna");
                if (ddlCOLColumna != null)
                    ddlCOLColumna.Visible = true;
                TextBox txtCOLColumnaE = (TextBox)gvColumnas.FooterRow.Cells[2].FindControl("txtCOLColumna");
                if (txtCOLColumnaE != null)
                    txtCOLColumnaE.Visible = false;
            }
            else
            {
                Label lblFormula = (Label)gvColumnas.FooterRow.Cells[2].FindControl("lblFormula");
                if (lblFormula != null)
                    lblFormula.Visible = false;
                DropDownList ddlCOLFormula = (DropDownList)gvColumnas.FooterRow.Cells[2].FindControl("ddlCOLFormula");
                if (ddlCOLFormula != null)
                    ddlCOLFormula.Visible = false;
                DropDownList ddlCOLTabla = (DropDownList)gvColumnas.FooterRow.Cells[2].FindControl("ddlCOLTabla");
                if (ddlCOLTabla != null)
                    ddlCOLTabla.Visible = false;
                DropDownList ddlCOLColumna = (DropDownList)gvColumnas.FooterRow.Cells[2].FindControl("ddlCOLColumna");
                if (ddlCOLColumna != null)
                    ddlCOLColumna.Visible = false;
                TextBox txtCOLColumnaE = (TextBox)gvColumnas.FooterRow.Cells[2].FindControl("txtCOLColumna");
                if (txtCOLColumnaE != null)
                    txtCOLColumnaE.Visible = true;
            }
        }
    }

    #endregion

    #region orden

    protected void InicializarOrden()
    {
        List<Orden> lstOrden = new List<Orden>();
        if (Session["LSTORDEN"] != null)
            lstOrden = (List<Orden>)Session["LSTORDEN"];
        for (int i = lstOrden.Count; i <= 5; i++)
        {
            Orden eOrden = new Orden();
            eOrden.idorden = -1;
            lstOrden.Add(eOrden);
        }
        gvOrden.DataSource = lstOrden;
        gvOrden.DataBind();
        Session["LSTORDEN"] = lstOrden;
    }

    protected void gvOrden_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("Add"))
        {
            List<Orden> lstOrden = new List<Orden>();
            if (Session["LSTORDEN"] != null)
                lstOrden = (List<Orden>)Session["LSTORDEN"];
            Orden eOrden = new Orden();
            eOrden.idorden = 0;

            DropDownList ddlORDTabla = new DropDownList();
            ddlORDTabla = (DropDownList)gvOrden.FooterRow.FindControl("ddlORDTabla");
            if (ddlORDTabla != null)
                if (ddlORDTabla.SelectedItem != null)
                    eOrden.tabla = ddlORDTabla.SelectedItem.Text;
            DropDownList ddlORDColumna = new DropDownList();
            ddlORDColumna = (DropDownList)gvOrden.FooterRow.FindControl("ddlORDColumna");
            if (ddlORDColumna != null)
                if (ddlORDColumna.SelectedItem != null)
                    eOrden.columna = ddlORDColumna.SelectedItem.Text;
            DropDownList ddlOrden = new DropDownList();
            ddlOrden = (DropDownList)gvOrden.FooterRow.FindControl("ddlOrden");
            if (ddlOrden != null)
                if (ddlOrden.SelectedItem != null)
                    eOrden.orden = ddlOrden.SelectedItem.Text;            
            Boolean bYaExiste = false;
            for (int i = 0; i < lstOrden.Count(); i++)
            {
                if (lstOrden[i].tabla == eOrden.tabla && lstOrden[i].columna == eOrden.columna)                    
                {
                    bYaExiste = true;
                    i = lstOrden.Count();
                }
            }
            if (!bYaExiste)
            {
                int posicion = -1;
                for (int i = 0; i < lstOrden.Count(); i++)
                {
                    if (lstOrden[i].idorden == -1)
                    {
                        lstOrden[i].idorden = eOrden.idorden;
                        lstOrden[i].tabla = eOrden.tabla;
                        lstOrden[i].columna = eOrden.columna;
                        lstOrden[i].orden = eOrden.orden;

                        posicion = i;
                        i = lstOrden.Count();
                    }
                }
                if (posicion == -1)
                {
                    lstOrden.Add(eOrden);
                }
            }
            gvOrden.DataSource = lstOrden;
            gvOrden.DataBind();
            Session["LSTORDEN"] = lstOrden;
            
        }
        if (e.CommandName.Equals("Deleted"))
        {
            try
            {
                int RowIndex = Convert.ToInt32(e.CommandArgument.ToString());
                List<Orden> lstOrden = new List<Orden>();
                lstOrden = (List<Orden>)Session["LSTORDEN"];
                if (lstOrden.Count > RowIndex)
                {
                    lstOrden.RemoveAt(RowIndex);
                    gvOrden.DataSource = lstOrden;
                    gvOrden.DataBind();
                    Session["LSTORDEN"] = lstOrden;
                    if (lstOrden.Count < 5)
                        InicializarOrden();
                }
            }
            catch
            {
                VerError("");
            }
        }
        
        List<Tabla> lstAux = new List<Tabla>();
        // Cargar listado de tablas existentes
        if (Session["LSTTABLAS"] != null)
            lstAux = (List<Tabla>)Session["LSTTABLAS"];
        Session["LSTTABLAS"] = lstAux;
        ActualizarDropDownList(lstAux);
    }

    protected void gvOrden_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        Label lblidOrden = (Label)e.Row.Cells[1].FindControl("lblidOrden");
        if (lblidOrden != null)
        {
            if (lblidOrden.Text == "-1")
            {
                ImageButton btnEliminar = (ImageButton)e.Row.Cells[0].FindControl("btnEliminar");
                btnEliminar.Visible = false;
            }
        }
    }

    protected void ddlORDTabla_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddlORDTabla = (DropDownList)gvOrden.FooterRow.Cells[2].FindControl("ddlORDTabla");
        if (ddlORDTabla != null)
        {
            Usuario pUsuario = new Usuario();
            pUsuario = (Usuario)Session["Usuario"];
            List<Columna> lstColumna = new List<Columna>();
            Columna pColumna = new Columna();
            if (ddlORDTabla.SelectedItem != null)
            {
                pColumna.tabla = ddlORDTabla.SelectedItem.Text;
                lstColumna = ReporteServicio.ListarColumnaBase(ddlORDTabla.SelectedItem.Text, pColumna, pUsuario);
            }
            DropDownList ddlORDColumna = (DropDownList)gvOrden.FooterRow.Cells[2].FindControl("ddlORDColumna");
            if (ddlORDColumna != null)
            {
                ddlORDColumna.DataSource = lstColumna;
                ddlORDColumna.DataTextField = "nombre";
                ddlORDColumna.DataValueField = "idcolumna";
                ddlORDColumna.DataBind();
            }
        }
    }

    #endregion

    #region grupo

    protected void InicializarGrupo()
    {
        List<Grupo> lstGrupo = new List<Grupo>();
        if (Session["LSTGRUPO"] != null)
            lstGrupo = (List<Grupo>)Session["LSTGRUPO"];
        for (int i = lstGrupo.Count; i <= 5; i++)
        {
            Grupo eGrupo = new Grupo();
            eGrupo.idgrupo = -1;
            lstGrupo.Add(eGrupo);
        }
        gvGrupo.DataSource = lstGrupo;
        gvGrupo.DataBind();
        Session["LSTGRUPO"] = lstGrupo;
    }

    protected void gvGrupo_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("Add"))
        {
            List<Grupo> lstGrupo = new List<Grupo>();
            if (Session["LSTGRUPO"] != null)
                lstGrupo = (List<Grupo>)Session["LSTGRUPO"];
            Grupo eGrupo = new Grupo();
            eGrupo.idgrupo = 0;

            DropDownList ddlGRUTabla = new DropDownList();
            ddlGRUTabla = (DropDownList)gvGrupo.FooterRow.FindControl("ddlGRUTabla");
            if (ddlGRUTabla != null)
                if (ddlGRUTabla.SelectedItem != null)
                    eGrupo.tabla = ddlGRUTabla.SelectedItem.Text;
            DropDownList ddlGRUColumna = new DropDownList();
            ddlGRUColumna = (DropDownList)gvGrupo.FooterRow.FindControl("ddlGRUColumna");
            if (ddlGRUColumna != null)
                if (ddlGRUColumna.SelectedItem != null)
                    eGrupo.columna = ddlGRUColumna.SelectedItem.Text;
            Boolean bYaExiste = false;
            for (int i = 0; i < lstGrupo.Count(); i++)
            {
                if (lstGrupo[i].tabla == eGrupo.tabla && lstGrupo[i].columna == eGrupo.columna)
                {
                    bYaExiste = true;
                    i = lstGrupo.Count();
                }
            }
            if (!bYaExiste)
            {
                int posicion = -1;
                for (int i = 0; i < lstGrupo.Count(); i++)
                {
                    if (lstGrupo[i].idgrupo == -1)
                    {
                        lstGrupo[i].idgrupo = eGrupo.idgrupo;
                        lstGrupo[i].tabla = eGrupo.tabla;
                        lstGrupo[i].columna = eGrupo.columna;

                        posicion = i;
                        i = lstGrupo.Count();
                    }
                }
                if (posicion == -1)
                {
                    lstGrupo.Add(eGrupo);
                }
            }
            gvGrupo.DataSource = lstGrupo;
            gvGrupo.DataBind();
            Session["LSTGRUPO"] = lstGrupo;

        }
        if (e.CommandName.Equals("Deleted"))
        {
            try
            {
                int RowIndex = Convert.ToInt32(e.CommandArgument.ToString());
                List<Grupo> lstGrupo = new List<Grupo>();
                lstGrupo = (List<Grupo>)Session["LSTGRUPO"];
                if (lstGrupo.Count > RowIndex)
                {
                    lstGrupo.RemoveAt(RowIndex);
                    gvOrden.DataSource = lstGrupo;
                    gvOrden.DataBind();
                    Session["LSTGRUPO"] = lstGrupo;
                    if (lstGrupo.Count < 5)
                        InicializarGrupo();
                }
            }
            catch
            {
                VerError("");
            }
        }

        List<Tabla> lstAux = new List<Tabla>();
        // Cargar listado de tablas existentes
        if (Session["LSTTABLAS"] != null)
            lstAux = (List<Tabla>)Session["LSTTABLAS"];
        Session["LSTTABLAS"] = lstAux;
        ActualizarDropDownList(lstAux);
    }

    protected void gvGrupo_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        Label lblidGrupo = (Label)e.Row.Cells[1].FindControl("lblidGrupo");
        if (lblidGrupo != null)
        {
            if (lblidGrupo.Text == "-1")
            {
                ImageButton btnEliminar = (ImageButton)e.Row.Cells[0].FindControl("btnEliminar");
                btnEliminar.Visible = false;
            }
        }
    }

    protected void ddlGRUTabla_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddlGRUTabla = (DropDownList)gvGrupo.FooterRow.Cells[2].FindControl("ddlGRUTabla");
        if (ddlGRUTabla != null)
        {
            Usuario pUsuario = new Usuario();
            pUsuario = (Usuario)Session["Usuario"];
            List<Columna> lstColumna = new List<Columna>();
            Columna pColumna = new Columna();
            if (ddlGRUTabla.SelectedItem != null)
            {
                pColumna.tabla = ddlGRUTabla.SelectedItem.Text;
                lstColumna = ReporteServicio.ListarColumnaBase(ddlGRUTabla.SelectedItem.Text, pColumna, pUsuario);
            }
            DropDownList ddlGRUColumna = (DropDownList)gvGrupo.FooterRow.Cells[2].FindControl("ddlGRUColumna");
            if (ddlGRUColumna != null)
            {
                ddlGRUColumna.DataSource = lstColumna;
                ddlGRUColumna.DataTextField = "nombre";
                ddlGRUColumna.DataValueField = "idcolumna";
                ddlGRUColumna.DataBind();
            }
        }
    }

    #endregion

    #region usuarios reporte

    private void InicializarUsuarios()
    {
        try
        {
            List<UsuariosReporte> lstConsulta = new List<UsuariosReporte>();
            UsuariosReporte pusuariosReporte = new UsuariosReporte();
            if (txtCodigo.Text.Trim() != "")
                pusuariosReporte.idreporte = Convert.ToInt64(txtCodigo.Text);
            else
                pusuariosReporte.idreporte = 0;
            lstConsulta = ReporteServicio.ListarUsuarios(pusuariosReporte, (Usuario)Session["Usuario"]);
            Session["LSTUSUARIOS"] = lstConsulta;
            String emptyQuery = "Fila de datos vacia";
            gvUsuariosRep.EmptyDataText = emptyQuery;
            gvUsuariosRep.DataSource = lstConsulta;
            if (lstConsulta.Count > 0)
            {
                gvUsuariosRep.Visible = true;
                gvUsuariosRep.DataBind();
                ValidarPermisosGrilla(gvUsuariosRep);
            }
            else
            {
                gvUsuariosRep.Visible = false;
            }
        }
        catch
        {
        }

    }

    #endregion

    #region perfil reporte

    private void InicializarPerfil()
    {
        try
        {
            List<PerfilReporte> lstConsulta = new List<PerfilReporte>();
            PerfilReporte pPerfilReporte = new PerfilReporte();
            if (txtCodigo.Text.Trim() != "")
                pPerfilReporte.idreporte = Convert.ToInt64(txtCodigo.Text);
            else
                pPerfilReporte.idreporte = 0;
            lstConsulta = ReporteServicio.ListarPerfil(pPerfilReporte, (Usuario)Session["Usuario"]);
            Session["LSTPERFIL"] = lstConsulta;
            String emptyQuery = "Fila de datos vacia";
            gvPerfiles.EmptyDataText = emptyQuery;
            gvPerfiles.DataSource = lstConsulta;
            if (lstConsulta.Count > 0)
            {
                gvPerfiles.Visible = true;
                gvPerfiles.DataBind();
                ValidarPermisosGrilla(gvPerfiles);
            }
            else
            {
                gvPerfiles.Visible = false;
            }
        }
        catch
        {
        }

    }

    #endregion

    #region Plantilla

    protected void InicializarPlantilla()
    {
        List<Plantilla> lstPlantilla = new List<Plantilla>();
        if (Session["LSTPLANTILLA"] != null)
            lstPlantilla = (List<Plantilla>)Session["LSTPLANTILLA"];
        for (int i = lstPlantilla.Count; i <= 1; i++)
        {
            Plantilla ePlantilla = new Plantilla();
            ePlantilla.idplantilla = -1;
            lstPlantilla.Add(ePlantilla);
        }
        gvPlantillas.DataSource = lstPlantilla;
        gvPlantillas.DataBind();
        Session["LSTPLANTILLA"] = lstPlantilla;
    }

    protected void gvPlantillas_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("Add"))
        {
            List<Plantilla> lstPlantilla = new List<Plantilla>();
            if (Session["LSTPLANTILLA"] != null)
                lstPlantilla = (List<Plantilla>)Session["LSTPLANTILLA"];
            Plantilla ePlantilla = new Plantilla();
            ePlantilla.idplantilla = 0;

            TextBox txtDescripcion = new TextBox();
            txtDescripcion = (TextBox)gvPlantillas.FooterRow.FindControl("txtDescripcion");
            if (txtDescripcion != null)
                if (txtDescripcion.Text != null)
                    ePlantilla.descripcion = txtDescripcion.Text;
            String path = Server.MapPath("~/Page/Reporteador/Reporte/Plantillas/");
            FileUpload lfuArchivo = new FileUpload();
            lfuArchivo = (FileUpload)gvPlantillas.FooterRow.FindControl("fuArchivo");
            if (lfuArchivo.HasFile)
            {
                Boolean fileOK = false;
                string fileExtension = "";
                fileExtension = System.IO.Path.GetExtension(lfuArchivo.FileName).ToUpper();
                string[] allowedExtensions = { ".DOC", ".DOCX" };
                for (int i = 0; i <= allowedExtensions.Length - 1; i++)
                {
                    if (fileExtension == allowedExtensions[i])
                        fileOK = true;
                }
                if (fileOK)
                {
                    try
                    {
                        lfuArchivo.PostedFile.SaveAs(path + lfuArchivo.FileName);
                    }
                    catch (Exception ex)
                    {
                        VerError("El archivo no pudo ser cargado." + ex.Message);
                    }
                }
                else
                {
                    VerError("Debe seleccionar un archivo .DOC o .DOCX");
                    return;
                }
            }
            else
            {
                VerError("Debe seleccionar un archivo .DOC o .DOCX");
                return;
            }
            ePlantilla.archivo = lfuArchivo.FileName;
            int posicion = -1;
            for (int i = 0; i < lstPlantilla.Count(); i++)
            {
                if (lstPlantilla[i].idplantilla == -1)
                {
                    lstPlantilla[i].idplantilla = ePlantilla.idplantilla;
                    lstPlantilla[i].descripcion = ePlantilla.descripcion;
                    lstPlantilla[i].archivo = ePlantilla.archivo;

                    posicion = i;
                    i = lstPlantilla.Count();
                }
            }
            if (posicion == -1)
            {
                lstPlantilla.Add(ePlantilla);
            }
            gvPlantillas.DataSource = lstPlantilla;
            gvPlantillas.DataBind();
            Session["LSTPLANTILLA"] = lstPlantilla;

        }
        if (e.CommandName.Equals("Deleted"))
        {
            try
            {
                int RowIndex = Convert.ToInt32(e.CommandArgument.ToString());
                List<Plantilla> lstPlantilla = new List<Plantilla>();
                lstPlantilla = (List<Plantilla>)Session["LSTPLANTILLA"];
                if (lstPlantilla.Count > RowIndex)
                {
                    lstPlantilla.RemoveAt(RowIndex);
                    gvPlantillas.DataSource = lstPlantilla;
                    gvPlantillas.DataBind();
                    Session["LSTPLANTILLA"] = lstPlantilla;
                    if (lstPlantilla.Count < 1)
                        InicializarPlantilla();
                }
            }
            catch
            {
                VerError("");
            }
        }

    }

    protected void gvPlantillas_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        Label lblidPlantilla = (Label)e.Row.Cells[1].FindControl("lblidPlantilla");
        if (lblidPlantilla != null)
        {
            if (lblidPlantilla.Text == "-1")
            {
                ImageButton btnEliminar = (ImageButton)e.Row.Cells[0].FindControl("btnEliminar");
                btnEliminar.Visible = false;
            }
        }
        HyperLink hlArchivo = (HyperLink)e.Row.Cells[3].FindControl("hlArchivo");
        if (hlArchivo != null)
        {
            hlArchivo.NavigateUrl = @"Plantillas\" + hlArchivo.Text;
        }
    }

    #endregion


    protected void ddlColumna1_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddlColumna1 = (DropDownList)gvCondiciones.FooterRow.Cells[2].FindControl("ddlColumna1");

        CheckBox rbLista = (CheckBox)gvCondiciones.FooterRow.Cells[2].FindControl("rbLista");
        TextBox txtValor2 = (TextBox)gvCondiciones.FooterRow.Cells[2].FindControl("txtValor2");
        txtValor2.Visible = true;
        txtValor2.Enabled = false;
        if (rbLista.Checked == true)
        {
            txtValor2.Text = ddlColumna1.SelectedItem.Text;
        }
        else
        {
            txtValor2.Enabled = true;
            txtValor2.Text = "";
        }
    }
}