using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public delegate void btnCtlAceptar_ActionsDelegate(object sender, EventArgs e);
public delegate void btnCtlCancelar_ActionsDelegate(object sender, EventArgs e);

public partial class ctlConfirmar : System.Web.UI.UserControl
{
    public event btnCtlAceptar_ActionsDelegate eventoAceptar;
    public event btnCtlCancelar_ActionsDelegate eventoCancelar;

    protected void btnCtlAceptar_Click(object sender, EventArgs e)
    {
        if (eventoAceptar != null)
            eventoAceptar(sender, e);
    }

    protected void btnCtlCancelar_Click(object sender, EventArgs e)
    {
        if (eventoCancelar != null)
            eventoCancelar(sender, e);
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