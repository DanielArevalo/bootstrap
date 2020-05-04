using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public delegate void btnContinuarMen_ActionsDelegate(object sender, EventArgs e);

public partial class ctlMensajeGrabar : System.Web.UI.UserControl
{
    public event btnContinuarMen_ActionsDelegate eventoClick;

    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        if (eventoClick != null)
            eventoClick(sender, e);
    }

    public void MostrarMensaje(string pMensaje)
    {
        Mensaje(pMensaje);        
        mpeMensaje.Show();        
    }

    public void Mostrar(Boolean pMostrar)
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