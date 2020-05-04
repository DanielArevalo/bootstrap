using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class General_Controles_Ctlmultiempresas : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void cbListado_SelectedIndexChanged(object sender, EventArgs e)
    {
        ObtenerDatos();
    }

    protected void ObtenerDatos()
    {
        string codigo = "";
        string name = "";
        int contador = 0;
        for (int i = 0; i < cbListado.Items.Count; i++)
        {
            if (cbListado.Items[i].Selected)
            {
                if (contador == 0)
                    name += cbListado.Items[i].Text;
                else
                    name += "," + cbListado.Items[i].Text;
                if (contador == 0)
                    codigo += cbListado.Items[i].Value;
                else
                    codigo += "," + cbListado.Items[i].Value;
                contador += 1;
            }
        }
        hfValue.Value = codigo;
        txtDato.Text = name;
    }

    protected void Asignar(string pDatos, int pTipo)
    {
        string[] sDatos;
        sDatos = pDatos.Split(',');
        if (sDatos.Count() > 0)
        {
            // Inicializar lista
            foreach (ListItem lItem in cbListado.Items)
            {
                lItem.Selected = false;
            }
            // Asignar valores
            foreach (string sDato in sDatos)
            {
                foreach (ListItem lItem in cbListado.Items)
                {
                    if (pTipo == 0)
                    {
                        if (lItem.Text == sDato)
                        {
                            lItem.Selected = true;
                        }
                    }
                    else
                    {
                        if (lItem.Value == sDato)
                        {
                            lItem.Selected = true;
                        }
                    }
                }
            }
            ObtenerDatos();
        }
    }

    public string SelectedText
    {
        get { return txtDato.Text; }
        set { Asignar(value, 0); }
    }

    public Unit Width
    {
        get { return txtDato.Width; }
        set { txtDato.Width = value; panelLista.Width = value; }
    }

    public string SelectedValue
    {
        get { return hfValue.Value; }
        set { Asignar(value, 1); }
    }

    public string DataTextField
    {
        get { return cbListado.DataTextField; }
        set { cbListado.DataTextField = value; }
    }

    public string DataValueField
    {
        get { return cbListado.DataValueField; }
        set { cbListado.DataValueField = value; }
    }

    public object DataSource
    {
        get { return cbListado.DataSource; }
        set { cbListado.DataSource = value; }
    }

    public ListItemCollection Items
    {
        get { return cbListado.Items; }
        set
        {
            cbListado.Items.Clear();
            foreach (ListItem lItem in value)
            {
                cbListado.Items.Add(lItem);
            }
        }
    }

    public void Add(ListItem pItem)
    {
        cbListado.Items.Add(pItem);
    }

    public void Clear()
    {
        cbListado.Items.Clear();
    }

    public void DataBind()
    {
        cbListado.DataBind();
    }


}