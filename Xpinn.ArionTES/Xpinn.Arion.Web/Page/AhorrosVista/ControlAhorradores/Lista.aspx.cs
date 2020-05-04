using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Xpinn.Util;
using Xpinn.Ahorros.Services;
using Xpinn.Ahorros.Entities;

public partial class Lista : GlobalWeb
{
    AhorroVistaServices _ahorroService = new AhorroVistaServices();

    #region Eventos Iniciales


    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(_ahorroService.CodigoProgramaCuentaBeneficiarios, "L");

            Site toolBar = (Site)Master;
            ctlMensaje.eventoClick += CtlMensaje_eventoClick;
            toolBar.eventoGuardar += (s, evt) => ctlMensaje.MostrarMensaje("Seguro que deseas realizar la operación?");
            toolBar.eventoConsultar += (s, evt) => Actualizar();
            toolBar.eventoRegresar += (s, evt) =>
            {
                toolBar.MostrarGuardar(true);
                toolBar.MostrarConsultar(true);
                toolBar.MostrarRegresar(false);

                mvPrincipal.ActiveViewIndex = 0;
            };
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_ahorroService.CodigoProgramaCuentaBeneficiarios, "Page_PreInit", ex);
        }
    }

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
            BOexcepcion.Throw(_ahorroService.CodigoProgramaCuentaBeneficiarios, "Page_Load", ex);
        }
    }

    void InicializarPagina()
    {
        txtFechaIni.Text = DateTime.Today.ToShortDateString();
        Actualizar();
    }


    #endregion


    #region Eventos Intermedios


    void CtlMensaje_eventoClick(object sender, EventArgs e)
    {
        try
        {
            List<AhorroVista> lstBen = ObtenerListaBeneficiarios();

            lstBen.ForEach(x => _ahorroService.EliminarBeneficiarioAhorro(x.idbeneficiario, Usuario));

            Site toolBar = (Site)Master;
            toolBar.MostrarGuardar(false);
            toolBar.MostrarConsultar(false);
            toolBar.MostrarRegresar(true);

            mvPrincipal.ActiveViewIndex = 1;
        }
        catch (Exception)
        {
            VerError("Error al realizar la eliminación de los beneficiarios");
        }
    }

    protected void gvLista_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DateTime today = Convert.ToDateTime(txtFechaIni.Text);
            string fechaNacCell = e.Row.Cells[11].Text;

            if (!string.IsNullOrWhiteSpace(fechaNacCell))
            {
                CheckBox chkSelect = e.Row.FindControl("chkSelect") as CheckBox;
                DateTimeHelper dateHelper = new DateTimeHelper();
                DateTime fechaNac = Convert.ToDateTime(fechaNacCell);

                long edad = dateHelper.DiferenciaEntreDosFechasAños(today, fechaNac);
                e.Row.Cells[12].Text = edad.ToString();

                if (edad >= 18)
                {
                    chkSelect.Checked = true;
                }
                else
                {
                    chkSelect.Enabled = false;
                }
            }
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
            VerError("Error al paginar la grilla, " + ex.Message);
        }
    }


    #endregion


    #region Métodos Ayuda


    void Actualizar()
    {
        VerError("");
        try
        {
            string filtro = string.Empty;

            List<AhorroVista> lstAhorros = _ahorroService.ListarAhorrosBeneficiaros(filtro, Usuario);

            if (lstAhorros.Count > 0)
            {
                lblTotalRegs.Text = "Se han encontrado " + lstAhorros.Count + " registros!.";
            }
            else
            {
                lblTotalRegs.Text = "Su consulta no obtuvo resultados";
            }

            gvLista.DataSource = lstAhorros;
            gvLista.DataBind();
        }
        catch (Exception ex)
        {
            VerError("Error al actualizar la grilla, " + ex.Message);
        }
    }

    List<AhorroVista> ObtenerListaBeneficiarios()
    {
        List<AhorroVista> lstBen = new List<AhorroVista>();

        foreach (GridViewRow row in gvLista.Rows)
        {
            CheckBox chk = row.FindControl("chkSelect") as CheckBox;

            if (chk == null || !chk.Checked) continue;

            AhorroVista ben = new AhorroVista();
            string id = row.Cells[8].Text;

            if (!string.IsNullOrWhiteSpace(id))
            {
                ben.idbeneficiario = Convert.ToInt64(id);
                lstBen.Add(ben);
            }
        }

        return lstBen;
    }


    #endregion


}