using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Nomina.Entities;
using Xpinn.Nomina.Services;
using Xpinn.Util;

public class EmpleadosArgs : EventArgs
{
    public long IDEmpleado { get; set; }
    public Exception Error { get; set; }

    public EmpleadosArgs(long idEmpleado)
    {
        IDEmpleado = idEmpleado;
    }

    public EmpleadosArgs(Exception error)
    {
        Error = error;
    }
}

public partial class ctlListarEmpleados : UserControl
{
    public event EventHandler<EmpleadosArgs> EmpleadoSeleccionado;
    public event EventHandler<EmpleadosArgs> ErrorControl;

    EmpleadoService _empleadoServices = new EmpleadoService();

    public bool ModoSeleccionCheckBox { get; set; }
    public bool SeleccionaEmpleadosSinContratos { get; set; }


    #region Eventos Iniciales


    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                InicializarPagina();
            }
        }
        catch (Exception ex)
        {
            if (ErrorControl != null)
            {
                ErrorControl(this, new EmpleadosArgs(ex));
            }
        }
    }

    void InicializarPagina()
    {
        GlobalWeb globalWeb = new GlobalWeb();

        globalWeb.LlenarListasDesplegables(TipoLista.Oficinas, ddlOficina);
        //globalWeb.LlenarListasDesplegables(TipoLista.TipoContrato, ddlTipoContrato);

        if (!ModoSeleccionCheckBox)
        {
            gvLista.Columns[9].Visible = false;
        }
        else
        {
            gvLista.Columns[0].Visible = false;
            gvLista.AllowPaging = false;
        }

        if (SeleccionaEmpleadosSinContratos)
        {
            lblContrato.Text = "Lista Todos los Empleados";
        }
        else
        {
            lblContrato.Text = "Lista Empleados Con Contrato";
        }
    }


    #endregion


    #region Eventos Intermedios GridView


    protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvLista.PageIndex = e.NewPageIndex;
        Actualizar();
    }

    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        long id = Convert.ToInt64(gvLista.SelectedRow.Cells[1].Text);

        if (EmpleadoSeleccionado != null)
        {
            EmpleadoSeleccionado(this, new EmpleadosArgs(id));
        }
    }

    protected void cbSeleccionarEncabezado_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox checkBoxHeader = gvLista.HeaderRow.FindControl("cbSeleccionarEncabezado") as CheckBox;

        foreach (GridViewRow row in gvLista.Rows)
        {
            CheckBox check = row.FindControl("chkSeleccionEmpleado") as CheckBox;

            check.Checked = checkBoxHeader.Checked;
        }
    }


    #endregion


    #region Métodos Ayuda


    public void Limpiar()
    {
        txtIdentificacion.Text = string.Empty;
        txtCodPersona.Text = string.Empty;
        ddlOficina.SelectedIndex = 0;
        txtCodEmpleado.Text = string.Empty;
        txtNombre.Text = string.Empty;

        lblTotalRegs.Visible = false;
        gvLista.Visible = false;
        gvLista.DataSource = null;
        gvLista.DataBind();
        //ddlTipoContrato.SelectedIndex = 0;
    }

    public void Actualizar()
    {
        try
        {
            string filtro = ObtenerFiltro();

            List<Empleados> lstEmpleado = null;
            if (SeleccionaEmpleadosSinContratos)
            {
                lstEmpleado = _empleadoServices.ListarEmpleados(filtro, (Usuario)Session["Usuario"]);
            }
            else
            {
                lstEmpleado = _empleadoServices.ListarEmpleadosConContratoActivo(filtro, (Usuario)Session["Usuario"]);
            }

            if (lstEmpleado.Count > 0)
            {
                lblTotalRegs.Text = "Se encontraron " + lstEmpleado.Count + " registros!.";
            }
            else
            {
                lblTotalRegs.Text = "Su consulta no obtuvo ningún resultado!.";
            }

            lblTotalRegs.Visible = true;
            gvLista.Visible = true;
            gvLista.DataSource = lstEmpleado;
            gvLista.DataBind();
        }
        catch (Exception ex)
        {
            if (ErrorControl != null)
            {
                ErrorControl(this, new EmpleadosArgs(ex));
            }
        }
    }

    string ObtenerFiltro()
    {
        string filtro = string.Empty;

        if (!string.IsNullOrWhiteSpace(txtNombre.Text))
        {
            filtro += " and per2.nombre like '%" + txtNombre.Text.Trim() + "%'";
        }

        if (!string.IsNullOrWhiteSpace(txtIdentificacion.Text))
        {
            filtro += " and per.identificacion like '%" + txtIdentificacion.Text.Trim() + "%'";
        }

        if (!string.IsNullOrWhiteSpace(txtCodPersona.Text))
        {
            filtro += " and per.cod_persona = " + txtCodPersona.Text.Trim();
        }

        if (!string.IsNullOrWhiteSpace(ddlOficina.SelectedValue))
        {
            filtro += " and per.cod_oficina = " + ddlOficina.SelectedValue;
        }

        if (!string.IsNullOrWhiteSpace(txtCodEmpleado.Text))
        {
            filtro += " and emp.consecutivo = " + txtCodEmpleado.Text;
        }

        //if (!string.IsNullOrWhiteSpace(ddlTipoContrato.SelectedValue))
        //{
        //    filtro += " and tip.CODTIPOCONTRATO = " + ddlTipoContrato.SelectedValue;
        //}

        if (!string.IsNullOrWhiteSpace(filtro))
        {
            StringHelper stringHelper = new StringHelper();
            filtro = stringHelper.QuitarAndYColocarWhereEnFiltroQuery(filtro);
        }

        return filtro;
    }

    public List<Empleados> ObtenerListaEmpleados()
    {
        CheckBox checkBoxHeader = gvLista.HeaderRow.FindControl("cbSeleccionarEncabezado") as CheckBox;

        if (!ModoSeleccionCheckBox || (checkBoxHeader != null && checkBoxHeader.Checked))
        {
            string filtro = ObtenerFiltro();

            List<Empleados> lstEmpleado = null;
            if (SeleccionaEmpleadosSinContratos)
            {
                lstEmpleado = _empleadoServices.ListarEmpleados(filtro, (Usuario)Session["Usuario"]);
            }
            else
            {
                lstEmpleado = _empleadoServices.ListarEmpleadosConContratoActivo(filtro, (Usuario)Session["Usuario"]);
            }

            return lstEmpleado;
        }
        else
        {
            List<Empleados> lstEmpleado = ObtenerListaEmpleadosSeleccionadosCheckBox();

            return lstEmpleado;
        }
    }

    List<Empleados> ObtenerListaEmpleadosSeleccionadosCheckBox()
    {
        List<Empleados> listaEmpleados = new List<Empleados>();

        foreach (GridViewRow row in gvLista.Rows)
        {
            CheckBox check = row.FindControl("chkSeleccionEmpleado") as CheckBox;

            if (check.Checked)
            {
                Empleados empleado = new Empleados
                {
                    consecutivo = Convert.ToInt64(gvLista.DataKeys[row.RowIndex].Value.ToString()),
                    cod_persona = Convert.ToInt64(HttpUtility.HtmlDecode(row.Cells[2].Text.ToString())),
                    identificacion = HttpUtility.HtmlDecode(row.Cells[3].Text.ToString()),
                    nombre = HttpUtility.HtmlDecode(row.Cells[4].Text.ToString()),
                    profesion = HttpUtility.HtmlDecode(row.Cells[5].Text.ToString()),
                    email = HttpUtility.HtmlDecode(row.Cells[6].Text.ToString()),
                    celular = HttpUtility.HtmlDecode(row.Cells[7].Text.ToString()),
                    nom_oficina = HttpUtility.HtmlDecode(row.Cells[8].Text.ToString())
                };

                listaEmpleados.Add(empleado);
            }
        }

        return listaEmpleados;
    }

    #endregion


}