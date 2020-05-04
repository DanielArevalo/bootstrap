using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

[DataContract]
[Serializable]
class ListarCodigoHelper
{
    public ListarCodigoHelper(string codigo, string descripcion, string hidden = "")
    {
        Codigo = codigo;
        Descripcion = descripcion;
        HiddenValue = hidden;
    }

    [DataMember]
    public string Codigo { get; set; }
    [DataMember]
    public string HiddenValue { get; set; }
    [DataMember]
    public string Descripcion { get; set; }

}


public partial class ctlListarCodigo : UserControl
{
    List<ListarCodigoHelper> _lstActual;

    [DataMember]
    public string ValueField { get; set; }
    [DataMember]
    public string TextField { get; set; }
    [DataMember]
    public string HiddenField { get; set; }

    public string Descripcion
    {
        get { return txtDatos.Text.Replace("&nbsp;",""); }
        private set { txtDatos.Text = value; }
    }

    public string Codigo
    {
        get { return hiddenCodigo.Value.Replace("&nbsp;",""); }
        private set { hiddenCodigo.Value = value; }
    }

    public string HiddenValue
    {
        get { return hiddenValue.Value.Replace("&nbsp;", ""); }
        private set { hiddenValue.Value = value; }
    }


    protected void Page_Load(object sender, EventArgs e)
    {  
        try
        {
            if (ViewState["lstBindearControlctlListarCodigo"] != null)
            {
                _lstActual = (List<ListarCodigoHelper>)ViewState["lstBindearControlctlListarCodigo"];
            }

            if (!IsPostBack)
            {
                txtBuscarCodigo.Attributes.Add("placeholder", "Código");
                txtBuscarDescripcion.Attributes.Add("placeholder", "Descripción");
            }
        }
        catch { }
    }


    /// <summary>
    /// Se debe asignar las propiedades "Descripcion" y "Codigo" antes de llamar a este metodo
    /// </summary>
    /// <param name="lstBindearDynamic">Lista a bindear al control</param>
    /// <returns>true si la operacion fue exitosa</returns>
    public bool BindearControl(dynamic lstBindearDynamic)
    {
        try
        {
            _lstActual = new List<ListarCodigoHelper>();
            _lstActual.Add(new ListarCodigoHelper("", "Seleccione un Item"));

            if (((ICollection)lstBindearDynamic).Count > 0)
            {
                foreach (var item in lstBindearDynamic)
                {
                    dynamic codigo = item.GetType().GetProperty(ValueField).GetValue(item, null);
                    dynamic descripcion = item.GetType().GetProperty(TextField).GetValue(item, null);
                    dynamic hidden = null;

                    if (!string.IsNullOrWhiteSpace(HiddenField))
                    {
                        hidden = item.GetType().GetProperty(HiddenField).GetValue(item, null);
                    }

                    if (codigo == null || string.IsNullOrWhiteSpace(codigo.ToString())) continue;

                    if (descripcion == null) descripcion = string.Empty;

                    if (hidden == null) hidden = string.Empty;

                    _lstActual.Add(new ListarCodigoHelper(codigo.ToString(), descripcion.ToString(), hidden.ToString()));
                }
            }

            gvFiltrarCodigo.DataSource = _lstActual;
            gvFiltrarCodigo.DataBind();

            ViewState.Add("lstBindearControlctlListarCodigo", _lstActual);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }


    protected void imgBuscar_Click(object sender, ImageClickEventArgs e)
    {
        IEnumerable<ListarCodigoHelper> grillaFiltrada; 

        if (txtBuscarCodigo.Text.Trim() != "" && txtBuscarDescripcion.Text.Trim() != "")
        {
            grillaFiltrada = _lstActual.Where(x => x.Codigo.Contains(txtBuscarCodigo.Text) || x.Descripcion.Contains(txtBuscarDescripcion.Text));
        }
        else if (txtBuscarCodigo.Text.Trim() != "")
        {
            if (_lstActual != null)
            {
                grillaFiltrada = _lstActual.Where(x => x.Codigo.Contains(txtBuscarCodigo.Text));
            }
            else
            {
                grillaFiltrada = new List<ListarCodigoHelper>();
            }
        }
        else if (txtBuscarDescripcion.Text.Trim() != "")
        {
            grillaFiltrada = _lstActual.Where(x => x.Descripcion.Contains(txtBuscarDescripcion.Text));  
        }
        else
        {
            grillaFiltrada = _lstActual;
        }

        gvFiltrarCodigo.DataSource = grillaFiltrada;
        gvFiltrarCodigo.DataBind();

        MostrarModal();
    }


    protected void gvFiltrarCodigo_SelectedIndexChanged(Object sender, EventArgs e)
    {
        GridViewRow row = gvFiltrarCodigo.Rows[gvFiltrarCodigo.SelectedIndex];

        hiddenCodigo.Value = HttpUtility.HtmlDecode(row.Cells[1].Text);
        txtDatos.Text = HttpUtility.HtmlDecode(row.Cells[2].Text);
        hiddenValue.Value = HttpUtility.HtmlDecode((row.FindControl("hiddenValue") as HiddenField).Value);
    }


    private void MostrarModal()
    {
        var ahh = txtDato_PopupControlExtender.ClientID;
        var script = @"Sys.Application.add_load(function() { $find('" + ahh + "').showPopup(); });";
        ScriptManager.RegisterStartupScript(this, GetType(), "ShowPopup", script, true);
    }


    // Le pasas el codigo que quieres que sea seleccionado como string
    public void SelectedValue(string codigo)
    {
        try
        {
            if (codigo.Trim() != "")
            {
                var grillaFiltrada = _lstActual.Where(x => x.Codigo.Contains(codigo));
                    //.AsEnumerable().OrderBy(i => i.Codigo.IndexOf(codigo)).ToList();
                foreach (var item in grillaFiltrada)
                {
                    dynamic auxDesc = item.GetType().GetProperty("Descripcion").GetValue(item, null);
                    Descripcion = auxDesc.ToString();
                    dynamic auxCod = item.GetType().GetProperty("Codigo").GetValue(item, null);
                    Codigo = auxCod.ToString();
                }
            }
        }
        catch { }
    }

    public void SelectedValueEqual(string codigo)
    {
        try
        {
            if (codigo.Trim() != "")
            {
                var grillaFiltrada = _lstActual.Where(x => x.Codigo == codigo);
                foreach (var item in grillaFiltrada)
                {
                    dynamic auxDesc = item.GetType().GetProperty("Descripcion").GetValue(item, null);
                    Descripcion = auxDesc.ToString();
                    dynamic auxCod = item.GetType().GetProperty("Codigo").GetValue(item, null);
                    Codigo = auxCod.ToString();
                }
            }
        }
        catch { }
    }

    // Limpia el control
    public void LimpiarControl()
    {
        hiddenCodigo.Value = "";
        txtDatos.Text = "";
    }
}