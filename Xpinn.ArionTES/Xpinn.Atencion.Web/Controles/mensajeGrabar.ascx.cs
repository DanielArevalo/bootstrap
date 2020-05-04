using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public delegate void btnContinuar_ActionsDelegate(object sender, EventArgs e);

public partial class mensajeGrabar : System.Web.UI.UserControl
{
    public event btnContinuar_ActionsDelegate eventoClick;

    protected void btnContinuar_Click(object sender, EventArgs e)
    {
        if (eventoClick != null)
            eventoClick(sender, e);
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        mpeMensaje.Hide();
    }

    public void MostrarMensaje(string pMensaje)
    {
        Mensaje(pMensaje);
        mpeMensaje.Show();
    }

    private void Mostrar(Boolean pMostrar)
    {
        if (pMostrar == true)
            mpeMensaje.Show();
        else
            mpeMensaje.Hide();
    }

    private void Mensaje(string pMensaje)
    {
        lblMensaje.Text = pMensaje;
    }

}