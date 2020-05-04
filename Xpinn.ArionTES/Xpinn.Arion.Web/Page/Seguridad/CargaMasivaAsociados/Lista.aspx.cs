using System;
using System.Data;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using Xpinn.Seguridad.Entities;
using Xpinn.Seguridad.Services;
using System.IO;
using System.Text;
using System.Web.UI.HtmlControls;

partial class Lista : GlobalWeb
{
    UsuarioService BOCarga = new UsuarioService();
    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(BOCarga.CodigoProgramaCarga, "E");
            Site toolBar = (Site)Master;
            toolBar.eventoImportar += btnImportar_Click;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.MostrarGuardar(false);
            ctlMensaje.eventoClick += btnContinuarMen_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(BOCarga.CodigoProgramaCarga, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                Session["INCONSISTENCIA"] = null;
                mvServicio.ActiveViewIndex = 0;
                pErrores.Visible = false;
                panelGrilla.Visible = false;
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(BOCarga.CodigoProgramaCarga, "Page_Load", ex);
        }
    }



    protected void btnImportar_Click(object sender, EventArgs e)
    {
        VerError("");
        if (flpArchivo.HasFile)
        {
            Stream stream = flpArchivo.FileContent;
            ReadCSVtoGrid(stream);
            if (gvLista.Rows.Count > 0)
                panelGrilla.Visible = true;
        }
        else
        {
            VerError("Por favor seleccione un archivo");
        }
    }

    private void ReadCSVtoGrid(Stream stream)
    {
        try
        {
            StreamReader strReader;
            DataTable dtDataSource = new DataTable();
            dtDataSource.Columns.Add("Identificación");
            dtDataSource.Columns.Add("Clave");
            string ext = System.IO.Path.GetExtension(flpArchivo.FileName);
            if (ext.ToLower() != ".txt")
            {
                VerError("Ingrese un archivo de tipo TXT");
                return;
            }

            string readLine = string.Empty;
            //spliting row after new line  
            using (strReader = new StreamReader(stream))
            {
                while (strReader.Peek() >= 0)
                {
                    readLine = strReader.ReadLine();
                    int cont = 0;
                    DataRow dr = dtDataSource.NewRow();
                    foreach (string FileRec in readLine.Split(','))
                    {
                        dr[cont] = FileRec;
                        cont++;
                    }
                    dtDataSource.Rows.Add(dr);
                }
            }
            lblTotReg.Text = "<b>Registros encontrados : </b>" + dtDataSource.Rows.Count;
            gvLista.DataSource = dtDataSource;
            gvLista.DataBind();
            Site toolBar = (Site)Master;
            if (dtDataSource.Rows.Count > 0)
            {
                toolBar.MostrarGuardar(true);
            }
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }


    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        if (ValidarDatos())
            ctlMensaje.MostrarMensaje("Desea realizar la grabación de los asociados cargados ?");
    }

    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        try
        {
            List<PersonaUsuario> lstDatos = new List<PersonaUsuario>();
            List<Xpinn.Tesoreria.Entities.ErroresCarga> lstErrores = new List<Xpinn.Tesoreria.Entities.ErroresCarga>();
            lstDatos = obtListaAsociados();
            string pError = string.Empty;
            bool rpta = BOCarga.CrearPersonasAsociadas(lstDatos, lstErrores, ref pError, Usuario);
            if (!string.IsNullOrEmpty(pError))
            {
                VerError(pError);
                return;
            }
            if (lstErrores.Count > 0)
            {
                pErrores.Visible = true;
                Session["INCONSISTENCIA"] = lstErrores;
                gvInconsistencia.DataSource = lstErrores;
                gvInconsistencia.DataBind();
            }
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }

    private List<PersonaUsuario> obtListaAsociados()
    {
        List<PersonaUsuario> lstData = new List<PersonaUsuario>();
        PersonaUsuario pEntidad;
        foreach (GridViewRow rFila in gvLista.Rows)
        {
            pEntidad = new PersonaUsuario();
            pEntidad.identificacion = rFila.Cells[0].Text;
            pEntidad.clave = rFila.Cells[1].Text;
            lstData.Add(pEntidad);
        }
        return lstData;
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

  

    Boolean ValidarDatos()
    {
        if (gvLista.Rows.Count == 0)
        {
            VerError("No existen Datos cargado para realizar la grabación");
            return false;
        }
        return true;
    }

}