using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using Xpinn.FabricaCreditos.Services;
using Xpinn.FabricaCreditos.Entities;
using Xpinn.Nomina.Services;
using Xpinn.Seguridad.Entities;
using Xpinn.Seguridad.Services;
using System.Reflection;
using System.Web;


public partial class Lista : GlobalWeb
{
    CreditoSolicitadoService _creditoServicio = new CreditoSolicitadoService();
    Usuario _usuario;
    ControlTiempos control = new ControlTiempos();
    CreditoSolicitadoService CreditoSolicitadoServicio = new CreditoSolicitadoService();
    CierresService _seguridad = new CierresService();
    private Usuario usu;

    #region Eventos


    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            VisualizarOpciones(_creditoServicio.CodigoProgramaAnalisisCredito, "L");
            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoExportar += btnExportar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_creditoServicio.CodigoProgramaAnalisisCredito, "Page_PreInit", ex);
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        _usuario = (Usuario)Session["usuario"];

        if (!IsPostBack)
        {
            Acceso accs;
            usu = (Usuario)Session["Usuario"];
            List<Acceso> acc = _seguridad.ListarAcceso(Convert.ToInt64(usu.codperfil), usu);
            accs = acc.FirstOrDefault(x => x.cod_opcion == Convert.ToInt64(_creditoServicio.CodigoProgramaAnalisisCredito));

            if (accs.modificar == 1)
            {
                lblCheck.Visible = true;
                ChkReimprimir.Visible = true;
            }

            LlenarDDLOficina();

            ChkReimprimir_OnCheckedChanged(ChkReimprimir, null);
        }
    }


    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        ActualizarGridView();
    }


    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        long id = Convert.ToInt64(gvLista.SelectedRow.Cells[1].Text);
        String identificacion = Convert.ToString(gvLista.SelectedRow.Cells[2].Text);
        Session[_creditoServicio.CodigoProgramaAnalisisCredito + ".id"] = id;
        Session[_creditoServicio.CodigoProgramaAnalisisCredito + ".identificacion"] = identificacion;
        Session[_creditoServicio.CodigoProgramaAnalisisCredito + ".imprimir"] = ChkReimprimir.Checked;// Guardo el N°Radicacion para verlo en Nuevo.aspx
        Navegar(Pagina.Nuevo);
    }


    protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvLista.PageIndex = e.NewPageIndex;
        ActualizarGridView(); // Necesario si no se pierden datos al cambiar de pagina
    }


    #endregion
    
    #region Metodos Llenado y Actualizar


    // Obtengo texto de los campos (Si hay) y armo un filtro, si no hay texto en los campos consulto todo los creditos en estado "V"
    private void ActualizarGridView()
    {
        try
        {
            Session["ListaCredito"] = null;
            List<CreditoSolicitado> lstConsulta;
            string filtro = ObtenerFiltroToQuery(); // Obtengo el filtro a aplicar en la consulta

            lstConsulta = _creditoServicio.ListaCreditosFiltradosEstadoV(new CreditoSolicitado(), _usuario, filtro); // Listo los creditos segun filtro (Creditos estado "V")
            gvLista.DataSource = lstConsulta;
            Session["ListaCredito"] = lstConsulta;

            if (lstConsulta.Count > 0)
            {
                gvLista.Visible = true;
                lblInfo.Visible = false;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                gvLista.DataBind();
            }
            else
            {
                gvLista.Visible = false;
                lblInfo.Visible = true;
                lblTotalRegs.Visible = false;
            }

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_creditoServicio.CodigoProgramaAnalisisCredito, "ActualizarGridView", ex);
        }
    }


    // Consulto si el usuario tiene permisos para consultar varias oficinas, 
    // si es asi lleno el DDL con todas las oficinas, si no solo lleno el DDL con la oficina del usuario
    protected void LlenarDDLOficina()
    {
        OficinaService oficinaService = new OficinaService();

        try
        {
            int cod = Convert.ToInt32(_usuario.codusuario);
            int consulta = 0;
            consulta = oficinaService.UsuarioPuedeConsultarCreditosOficinas(cod, _usuario);
            ddlOficina.Visible = true;
            lbloficina.Visible = true;

            if (consulta >= 1)
            {
                ddlOficina.DataSource = oficinaService.ListarOficinas(new Oficina(), _usuario);
                ddlOficina.DataTextField = "nombre";
                ddlOficina.DataValueField = "codigo";
                ddlOficina.DataBind();
                ddlOficina.Items.Insert(0, new ListItem("Todas las oficinas", "0"));
            }
            else
            {
                ddlOficina.Items.Insert(0, new ListItem(Convert.ToString(_usuario.nombre_oficina), Convert.ToString(_usuario.cod_oficina)));
                ddlOficina.DataBind();
                ddlOficina.Enabled = false;
            }

            Xpinn.FabricaCreditos.Services.ControlTiemposService ControlTiemposServicio = new Xpinn.FabricaCreditos.Services.ControlTiemposService();
            List<Xpinn.FabricaCreditos.Entities.ControlTiempos> lstDatosSolicitud = new List<Xpinn.FabricaCreditos.Entities.ControlTiempos>();
            lstDatosSolicitud.Clear();
            lstDatosSolicitud = ControlTiemposServicio.ListasDesplegables("Estado", (Usuario)Session["Usuario"]);
            if (lstDatosSolicitud != null)
            {
                string estado = "";
                control = CreditoSolicitadoServicio.ConsultarProcesoAnterior("Pre Aprobado", (Usuario)Session["usuario"]);
                if (control != null)
                    estado = control.estado;
                var lista = lstDatosSolicitud.Where(x => x.ListaIdStr != "V" && x.ListaIdStr != "T" && x.ListaIdStr != estado).ToList();
                lstDatosSolicitud = lista;
            }
            ddlEstado.DataSource = lstDatosSolicitud;
            ddlEstado.DataTextField = "ListaDescripcion";
            ddlEstado.DataValueField = "ListaIdStr";
            ddlEstado.DataBind();
            ddlEstado.Items.Add("");
            ddlEstado.Text = "";
        }
        catch
        {
            ddlOficina.Visible = false;
            lbloficina.Visible = false;
        }
    }


    #endregion
    
    #region Obtener Filtro


    // Obtengo texto de los campos y armo el filtro a aplicar en la consulta de los creditos en estado "V"
    private string ObtenerFiltroToQuery()
    {
        string filtro = string.Empty;
        string radicacion = txtRadicacion.Text.Trim();
        string identificacion = txtIdentificacion.Text.Trim();
        string nombre = txtNombre.Text.Trim();
        string apellido = txtApellido.Text.Trim();

        //Agregado para consultar proceso anterior a Pre Aprobado según la parametrización de la entidad        
        string estado = "Pre Aprobado";
        control = CreditoSolicitadoServicio.ConsultarProcesoAnterior(estado, (Usuario)Session["usuario"]);
        // si no existe el estado como credito normal asigno se consulte por rotativo, por defecto al solicitar queda con V
        // POR DEFECTO AL SOLICITAR UN ROTATIVO LO DEJA EN ESTADO V.
        if (!ChkReimprimir.Checked)
        {
            estado = " c.estado IN ('V')";
            if (!string.IsNullOrEmpty(control.estado))
                estado = " c.estado IN ('V', '" + control.estado + "') ";
        }
        else
        {
            estado = " c.estado NOT IN ('V','T')  AND  lineascredito.nombre NOT LIKE '%CASTIGO%' AND  C.NUMERO_OBLIGACION IN (select NUMEROSOLICITUD from SOLICITUDCRED )";
            if (!string.IsNullOrEmpty(control.estado))
                estado = " c.estado NOT IN ('V','T', '" + control.estado + "') AND lineascredito.nombre NOT LIKE '%CASTIGO%'  AND  C.NUMERO_OBLIGACION IN (select NUMEROSOLICITUD from SOLICITUDCRED ) ";
            if (ddlEstado.SelectedItem != null)
                if (ddlEstado.SelectedItem.Value.Trim() != "")
                    estado += " AND c.estado = '" + ddlEstado.SelectedItem.Value + "' ";
        }

        filtro += estado;

        if (radicacion != "")
            filtro += " and c.numero_radicacion like '%" + radicacion + "%'";
        if (identificacion != "")
            filtro += " and p.identificacion like '%" + identificacion + "%'";
        if (nombre != "")
            filtro += " and p.primer_nombre like '%" + nombre.ToUpper() + "%'";
        if (apellido != "")
            filtro += " and p.primer_apellido like '%" + apellido.ToUpper() + "%'";
        if (ddlOficina.Visible == true)
        {
            if (ddlOficina.SelectedIndex != 0)
                filtro += " and c.cod_oficina = " + ddlOficina.SelectedValue + "";
        }
        if (txtCodigoNomina.Text.Trim() != "")
            filtro += " and cod_nomina = '" + txtCodigoNomina.Text + "'";

        // ADICIONANDO ORDEN
        filtro += " ORDER BY C.NUMERO_RADICACION";
        return filtro;
    }


    #endregion

    #region Metodos Externos

    protected void btnExportar_Click(object sender, EventArgs e)
    {
        List<CreditoSolicitado> lstConsulta = (List<CreditoSolicitado>)Session["ListaCredito"];
        if (Session["ListaCredito"] != null)
        {
            string fic = "AnalisisCredito.csv";
            try
            {
                File.Delete(fic);
            }
            catch
            {
            }
            // Generar el archivo
            bool bTitulos = false;
            System.IO.StreamWriter sw = new System.IO.StreamWriter(Server.MapPath("") + fic, true);
            var lstConsultas = from sls in lstConsulta
                               select new
                               {
                                   NumeroCredito = sls.NumeroCredito,
                                   Identificacion = sls.Identificacion,
                                   Nombres = sls.Nombres,
                                   codigo_Nomina = sls.cod_nomina,
                                   Monto = sls.Monto,
                                   Cuota = sls.Cuota,
                                   Plazo = sls.Plazo,
                                   Linea_Credito = sls.LineaCredito,
                                   Oficina = sls.oficina,
                                   Zona = sls.NomZona,
                                   Fecha_solicitu = sls.fecha_solicitud,
                                   Estado = sls.estado
                               };

            foreach (var item in lstConsultas)
            {
                string texto = "";
                FieldInfo[] propiedades = item.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
                if (!bTitulos)
                {
                    foreach (FieldInfo f in propiedades)
                    {
                        try
                        {
                            texto += f.Name.Split('>').First().Replace("<", "") + ";";
                        }
                        catch { texto += ";"; };
                    }
                    sw.WriteLine(texto);
                    bTitulos = true;
                }
                texto = "";
                int i = 0;
                foreach (FieldInfo f in propiedades)
                {
                    i += 1;
                    object valorObject = f.GetValue(item);
                    // Si no soy nulo
                    if (valorObject != null)
                    {
                        string valorString = valorObject.ToString();
                        if (valorObject is DateTime)
                        {
                            DateTime? fechaValidar = valorObject as DateTime?;
                            if (fechaValidar.Value != DateTime.MinValue)
                            {
                                texto += f.GetValue(item) + ";";
                            }
                            else
                            {
                                texto += "" + ";";
                            }
                        }
                        else
                        {
                            texto += f.GetValue(item) + ";";
                            texto.Replace("\r", "").Replace(";", "");
                        }
                    }
                    else
                    {
                        texto += "" + ";";
                    }
                }
                sw.WriteLine(texto);
            }
            sw.Close();
            System.IO.StreamReader sr;
            sr = File.OpenText(Server.MapPath("") + fic);
            string texo = sr.ReadToEnd();
            sr.Close();
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.ContentType = "text/plain";
            HttpContext.Current.Response.Write(texo);
            HttpContext.Current.Response.AppendHeader("Content-disposition", "attachment;filename=" + fic);
            HttpContext.Current.Response.Flush();
            File.Delete(Server.MapPath("") + fic);
            HttpContext.Current.Response.End();

        }
    }

    protected void ChkReimprimir_OnCheckedChanged(object sender, EventArgs e)
    {
        ddlEstado.Visible = ChkReimprimir.Checked;
        lblEstado.Visible = ChkReimprimir.Checked;
    }

    #endregion

}