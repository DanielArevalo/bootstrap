using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using System.Data;
using Xpinn.Seguridad.Services;
using Xpinn.Seguridad.Entities;
using Xpinn.Util;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Excel = Microsoft.Office.Interop.Excel;

partial class ArchivoTablas : GlobalWeb
{
    private Xpinn.Seguridad.Services.ArchivoServices ArchivosServicio = new Xpinn.Seguridad.Services.ArchivoServices();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            VisualizarOpciones(ArchivosServicio.CodigoPrograma, "D");


        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ArchivosServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            if (IsPostBack)
            {
                BaseDatosEntidad basedatosin = new BaseDatosEntidad();


                btnConsultarEsq_Click(null, null);
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }

        catch (Exception ex)
        {
            BOexcepcion.Throw(ArchivosServicio.CodigoPrograma, "Page_Load", ex);
        }

    }

    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        ArchivoServices archivoservice = new ArchivoServices();
        BaseDatosEntidad basedatosin = new BaseDatosEntidad();
        if (ddTipoBaseDatos.SelectedValue.ToString().ToUpper().Equals("SQLSERVER"))
        {
            basedatosin.TipoProveedor = TipoProveedorBaseDatos.SqlServer;
        }
        if (ddTipoBaseDatos.SelectedValue.ToString().ToUpper().Equals("ORACLE"))
        {
            basedatosin.TipoProveedor = TipoProveedorBaseDatos.Oracle;
        }
        if (ddTipoBaseDatos.Text == "SqlServer")
        {
            basedatosin.Owner = "";
            DDlEsquemas.Visible = false;
            btnConsultarEsq.Visible = false;
            ddTipoBaseDatos.SelectedIndex = 0;
        }
        else
        {
            basedatosin.Owner = DDlEsquemas.SelectedValue;
        }

        Usuario usuario = new Usuario();
        // basedatosin.Owner = DDlEsquemas.SelectedValue;
        BaseDatosEntidad basedatosout = archivoservice.infoBaseDatos(basedatosin, (Usuario)Session["usuario"]);
        if (basedatosout.Tablas != null)
        {
            this.DDlTablas.DataSource = basedatosout.Tablas;
            this.DDlTablas.DataValueField = "Nombre";
            this.DDlTablas.DataTextField = "Nombre";
            this.DDlTablas.DataBind();
            Session["infobaseDatos"] = basedatosout;

        }


    }
    protected void DDTablas_SelectedIndexChanged(object sender, EventArgs e)
    {
        ArchivoServices archivoservice = new ArchivoServices();

        BaseDatosEntidad basedatosout = Session["infobaseDatos"] as BaseDatosEntidad;
        TablaEntidadExt tablaextendida = Session["tablaextendida"] as TablaEntidadExt;
        if (tablaextendida == null)
        {
            tablaextendida = new TablaEntidadExt();
        }
        tablaextendida.ColumnasEntidadExt.Clear();

        String nombre = "";

        if (basedatosout != null)
        {
            nombre = DDlTablas.SelectedValue.ToString().Trim();

            ///--20161227
            BaseDatosEntidad basedatosin = new BaseDatosEntidad();
            if (ddTipoBaseDatos.SelectedValue.ToString().ToUpper().Equals("SQLSERVER"))
            {
                basedatosin.TipoProveedor = TipoProveedorBaseDatos.SqlServer;
            }
            if (ddTipoBaseDatos.SelectedValue.ToString().ToUpper().Equals("ORACLE"))
            {
                basedatosin.TipoProveedor = TipoProveedorBaseDatos.Oracle;
            }
            Usuario usuario = new Usuario();
            basedatosout.Owner = DDlEsquemas.SelectedValue;
            basedatosout = archivoservice.infoBaseDatosColumnas(basedatosout, nombre, (Usuario)Session["usuario"]);

            ///-20161227


            foreach (TablaEntidad tabla in basedatosout.Tablas)
            {
                if (tabla.Nombre.Equals(nombre))
                {
                    tablaextendida.NombreTablaDestino = nombre;
                    GvColumnas.DataSource = tabla.ColumnasEntidad;
                    GvColumnas.DataBind();
                    foreach (ColumnaEntidad columna in tabla.ColumnasEntidad)
                    {
                        ColumnaEntidadExt columnaext = new ColumnaEntidadExt();
                        columnaext.LongitudDestino = columna.Longitud;
                        columnaext.NombreDestino = columna.Nombre;
                        columnaext.TipoDestino = columna.Tipo;
                        tablaextendida.ColumnasEntidadExt.Add(columnaext);
                    }
                    gvColumnasUnidas.DataSource = tablaextendida.ColumnasEntidadExt;
                    gvColumnasUnidas.DataBind();
                    break;
                }
            }
            tablaextendida.Tipoprovedorbd = basedatosout.TipoProveedor;
            Session["tablaextendida"] = tablaextendida;
        }
        /*Entidad.TablaEntidad tablaentidad = (Entidad.TablaEntidad)DDTablas.DataSource;
        GvColumnas.DataSource = tablaentidad.ColumnasEntidad;
        GvColumnas.DataBind();*/
    }
    protected void Button1_Click(object sender, EventArgs e)
    {

        try
        {
            string fileSrc = Server.MapPath("~/Archivos/" + this.lblNombreArchivo.Text);

            String NombreArchivo = this.lblNombreArchivo.Text;



            this.LLenarGrid(fileSrc, this.txtHoja.Text.ToUpper());
        }
        catch (Exception ex)
        {
            lblMensajeError.Text = ex.Message;
        }

    }

    private void LLenarGrid(string archivo, string hoja)
    {
        //declaramos las variables         
        OleDbConnection conexion = null;
        DataSet dataSet = null;
        OleDbDataAdapter dataAdapter = null;
        string consultaHojaExcel = "Select * from [" + hoja + "$]";
        TablaEntidad tablaentidad;
        tablaentidad = Session["TablaEntidadOrigen"] as TablaEntidad;
        if (tablaentidad == null)
        {
            tablaentidad = new TablaEntidad();
        }
        tablaentidad.ColumnasEntidad.Clear();


        //esta cadena es para archivos excel 2007 y 2010
        string cadenaConexionArchivoExcel = "provider=Microsoft.ACE.OLEDB.12.0;Data Source='" + archivo + "';Extended Properties=Excel 12.0;";

        //para archivos de 97-2003 usar la siguiente cadena
        //string cadenaConexionArchivoExcel = "provider=Microsoft.Jet.OLEDB.4.0;Data Source='" + archivo + "';Extended Properties=Excel 8.0;";

        //Validamos que el usuario ingrese el nombre de la hoja del archivo de excel a leer
        if (string.IsNullOrEmpty(hoja))
        {
            this.lblMensajeError.Text = "No hay una hoja para leer";
        }
        else
        {
            try
            {
                //Si el usuario escribio el nombre de la hoja se procedera con la busqueda
                conexion = new OleDbConnection(cadenaConexionArchivoExcel);//creamos la conexion con la hoja de excel
                conexion.Open(); //abrimos la conexion
                dataAdapter = new OleDbDataAdapter(consultaHojaExcel, conexion); //traemos los datos de la hoja y las guardamos en un dataSdapter
                dataSet = new DataSet(); // creamos la instancia del objeto DataSet
                dataAdapter.Fill(dataSet, hoja);//llenamos el dataset
                System.Data.DataTable datatable = dataSet.Tables[0];
                foreach (System.Data.DataColumn column in datatable.Columns)
                {
                    ColumnaEntidad columna = new ColumnaEntidad();
                    columna.Nombre = column.ColumnName;
                    columna.Longitud = 0;
                    columna.Tipo = column.DataType.Name;
                    tablaentidad.ColumnasEntidad.Add(columna);

                }

                Session["DatosOrigen"] = dataSet.Tables[0]; //le asignamos al DataGridView el contenido del dataSet
                GvColumnasArchivo.DataSource = tablaentidad.ColumnasEntidad;
                GvColumnasArchivo.DataBind();
                conexion.Close();//cerramos la conexion
                Session["TablaEntidadOrigen"] = tablaentidad;
                //GridView2.AllowUserToAddRows = false;       //eliminamos la ultima fila del datagridview que se autoagrega
            }
            catch (Exception ex)
            {
                lblMensajeError.Text = ex.Message;
                //en caso de haber una excepcion que nos mande un mensaje de error
                //MessageBox.Show("Error, Verificar el archivo o el nombre de la hoja", ex.Message);

            }
        }
    }
    protected void gvColumnasUnidas_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void gvColumnasUnidas_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            List<ColumnaEntidad> columnasorigen = new List<ColumnaEntidad>();
            ColumnaEntidad columnaentidad = new ColumnaEntidad();

            columnaentidad.Nombre = "No Importar";
            columnaentidad.Tipo = "-1";
            columnaentidad.Longitud = 0;
            columnasorigen.Add(columnaentidad);
            Label milabel = (Label)e.Row.FindControl("Label1");


            TablaEntidad tablaentidad = Session["TablaEntidadOrigen"] as TablaEntidad;
            if (tablaentidad != null)
            {

                foreach (ColumnaEntidad column in tablaentidad.ColumnasEntidad)
                {
                    columnasorigen.Add(column);
                }

                //Cargar Tipo Direccion
                DropDownList ddlnombreorigen = (DropDownList)e.Row.FindControl("ddNombreOrigen");
                ddlnombreorigen.DataSource = columnasorigen;
                ddlnombreorigen.DataValueField = "Nombre";
                ddlnombreorigen.DataTextField = "Nombre";


                ddlnombreorigen.DataBind();
                if (milabel != null)
                {
                    ddlnombreorigen.SelectedValue = milabel.Text;
                }

            }
        }
    }


    protected void BtnCargar_Click(object sender, EventArgs e)
    {
        Upload();
        btnConsultarEsq_Click(btnConsultarEsq, null);
    }

    private String Upload()
    {
        String saveDir = Server.MapPath("~/Archivos/");

        String Fecha = Convert.ToString(DateTime.Now.ToString("MMddyyyy"));

        string appPath = Request.PhysicalApplicationPath;

        if (this.FileUpLoad.HasFile)
        {


            String fileName = FileUpLoad.FileName;

            string savePath = saveDir + fileName;
            string archivo = fileName;
            FileUpLoad.SaveAs(savePath);
            this.lblNombreArchivo.Text = archivo;

            // copiar();
            return savePath;

        }

        else
        {
            //Lblerror.Visible = false;
            return String.Empty;

        }


    }
    private void copiar()
    {
        string sourcePath = Server.MapPath("~/Archivos/");
        string targetPath = Server.MapPath("~/Archivos/ArchivosSubidos/");

        string sourceFile = System.IO.Path.Combine(sourcePath, lblNombreArchivo.Text);
        var excel = new Excel.Application();

        string destFile = System.IO.Path.Combine(targetPath, lblNombreArchivo.Text);
        System.IO.File.Copy(sourceFile, destFile, true);

        Excel.Workbook libro = excel.Workbooks.Open(destFile);

        foreach (Microsoft.Office.Interop.Excel.Worksheet Hojas in libro.Sheets)
        {
            DdlHojas.Items.Add(Hojas.Name);

        }
    }
    protected void BtnProcesarCargue_Click(object sender, EventArgs e)
    {
        ArchivoServices archivoservices = new ArchivoServices();
        //  BaseDatosEntidad basedatosin;
        BaseDatosEntidad basedatosin = new BaseDatosEntidad();

        TablaEntidad tablaentidadorigen = Session["TablaEntidadOrigen"] as TablaEntidad;
        TablaEntidadExt tablaextendidadestino = Session["tablaextendida"] as TablaEntidadExt;
        TablaEntidadExt tablaprocesar = new TablaEntidadExt();

        DataTable tablaDatosorigen = Session["DatosOrigen"] as DataTable;
        Usuario pUsuario = new Usuario();

        if (tablaextendidadestino == null || string.IsNullOrEmpty(tablaextendidadestino.NombreTablaDestino))
        {
            VerError("No estan todos los campos llenos");
            return;
        }
        tablaprocesar.NombreTablaDestino = tablaextendidadestino.NombreTablaDestino.ToUpper();
        tablaprocesar.NombreTablaOrigen = tablaextendidadestino.NombreTablaOrigen.ToUpper();


        if (ddTipoBaseDatos.Text == "Oracle")
        {
            basedatosin.Owner = DDlEsquemas.SelectedValue;
        }
        else
        {
            basedatosin.Owner = "";
        }

        foreach (GridViewRow row in gvColumnasUnidas.Rows)
        {
            ColumnaEntidadExt columnaext = new ColumnaEntidadExt();
            DropDownList ddlnombreorigen = (DropDownList)row.Cells[0].FindControl("ddNombreOrigen");

            string columnOrigen = ddlnombreorigen.SelectedValue.ToString().Trim();
            string columnDestiono = ((Label)row.Cells[1].FindControl("Label1")).Text;
            columnaext.NombreOrigen = columnOrigen;
            columnaext.NombreDestino = columnDestiono;
            foreach (ColumnaEntidadExt column in tablaextendidadestino.ColumnasEntidadExt)
            {
                if (column.NombreDestino.Equals(columnDestiono))
                {
                    columnaext.LongitudDestino = column.LongitudDestino;
                    columnaext.TipoDestino = column.TipoDestino;
                    break;
                }
            }
            if (tablaentidadorigen.ColumnasEntidad != null)
            {
                foreach (ColumnaEntidad column in tablaentidadorigen.ColumnasEntidad)
                {
                    if (column.Nombre.Equals(columnOrigen))
                    {
                        columnaext.LongitudOrigen = column.Longitud;
                        columnaext.TipoOrigen = column.Tipo;
                        break;
                    }
                }
            }
            tablaprocesar.ColumnasEntidadExt.Add(columnaext);
        }
        tablaprocesar.Tipoprovedorbd = tablaextendidadestino.Tipoprovedorbd;
        basedatosin = archivoservices.ingresarDatosBaseDatos(tablaprocesar, tablaDatosorigen, (Usuario)Session["usuario"], basedatosin);
        if (basedatosin != null)
        {
            this.txtExitosos.Text = basedatosin.Registros.RegistrosExitosos.Count.ToString();
            this.txtFallidos.Text = basedatosin.Registros.RegistrosFallidos.Count.ToString();
            this.gvFallidos.DataSource = basedatosin.Registros.RegistrosFallidos;
            this.gvFallidos.DataBind();
        }
    }


    protected void btnConsultarEsq_Click(object sender, EventArgs e)
    {
        ArchivoServices archivoservice = new ArchivoServices();
        BaseDatosEntidad basedatosin = new BaseDatosEntidad();
        TablaEntidad tabla = new TablaEntidad();

        if (ddTipoBaseDatos.SelectedValue.ToString().ToUpper().Equals("SQLSERVER"))
        {
            basedatosin.TipoProveedor = TipoProveedorBaseDatos.SqlServer;
        }
        if (ddTipoBaseDatos.SelectedValue.ToString().ToUpper().Equals("ORACLE"))
        {
            basedatosin.TipoProveedor = TipoProveedorBaseDatos.Oracle;
        }


        Usuario usuario = new Usuario();
        String usuariobd = archivoservice.consultarusuariobd((Usuario)Session["usuario"]);
        BaseDatosEntidad basedatosout = new BaseDatosEntidad();
        tabla.Nombre = usuariobd;
        basedatosout.Tablas.Add(tabla);

        //  BaseDatosEntidad basedatosout = archivoservice.infoEsquemasBaseDatos(basedatosin, usuario);
        if (basedatosout.Tablas != null)
        {
            this.DDlEsquemas.DataSource = basedatosout.Tablas;
            this.DDlEsquemas.DataValueField = "Nombre";
            this.DDlEsquemas.DataTextField = "Nombre";
            this.DDlEsquemas.DataBind();
            Session["infobaseDatosEdsquemas"] = basedatosout;

        }
    }
}