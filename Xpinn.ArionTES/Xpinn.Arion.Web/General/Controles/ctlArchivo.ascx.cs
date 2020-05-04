using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public delegate void btnAceptarArchivo_ActionsDelegate(object sender, EventArgs e);

public partial class ctlArchivo : System.Web.UI.UserControl
{
    public event btnAceptarArchivo_ActionsDelegate eventoClick;
    public string lDescripcion = "";
    public FileUpload lfuArchivo = new FileUpload();

    protected void btnAceptarArchivo_Click(object sender, EventArgs e)
    {
        lDescripcion = txtDescripcion.Text;
        lfuArchivo = fuArchivo;
        if (eventoClick != null)
            eventoClick(sender, e);
    }

    public void Mostrar(Boolean pMostrar)
    {
        if (pMostrar == true)
            mpeArchivo.Show();
        else
            mpeArchivo.Hide();
    }
}