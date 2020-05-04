using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;

public partial class General_Controles_CtlEditDocument : System.Web.UI.UserControl
{
    List<Xpinn.FabricaCreditos.Entities.DatosDeDocumento> Variables = new List<Xpinn.FabricaCreditos.Entities.DatosDeDocumento>();
    private Xpinn.FabricaCreditos.Services.DatosDeDocumentoService datosDeDocumentoService = new Xpinn.FabricaCreditos.Services.DatosDeDocumentoService();
    public string Texto { get; set; }

    protected void Page_Load(object sender, EventArgs e)
    {
        MostrarVariables();
        if (!string.IsNullOrEmpty(Texto))
            FreeTextBox2.Value = Texto;
    }

    private void MostrarVariables()
    {
        Variables = datosDeDocumentoService.ListarVariables((Usuario)Session["usuario"]);
        //inserta en el select los valores
        ddlVariables.DataSource = Variables.OrderBy(x => x.Campo).ToList();
        ddlVariables.Items.Add(new ListItem("Seleccione un item", "0"));
        ddlVariables.DataValueField = "Campo";
        ddlVariables.DataTextField = "Valor";
        ddlVariables.DataBind();
    }

    public string DevolverTexto()
    {
        Texto = FreeTextBox2.Value;
        return Texto;
    }
}