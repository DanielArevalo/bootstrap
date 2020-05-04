using System;
using System.Configuration;
using System.Management.Automation;
using System.Text;
using System.Web;
using System.Web.UI;

public partial class Nuevo : GlobalWeb
{
    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones("90116", "D");

            Site toolBar = (Site)Master;
            toolBar.eventoRegresar += (s, evt) =>
            {
                Navegar(Pagina.Lista);
            };
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw("90116", "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InicializarPagina();
        }
    }

    void InicializarPagina()
    {
        string scriptDefault = ConfigurationManager.AppSettings["ScriptParaEjecutar"] as string;
        txtScriptParaEjecutar.Text = HttpUtility.HtmlDecode(scriptDefault);
    }

    protected void btnEjecutarScript_Click(object sender, EventArgs e)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(txtScriptParaEjecutar.Text))
            {
                VerError("El script a ejecutar no debe estar vacio!.");
                return;
            }

            // Clean the Result TextBox
            ResultBox.Text = string.Empty;

            // Initialize PowerShell engine
            PowerShell shell = PowerShell.Create();

            // Add the script to the PowerShell object
            shell.Commands.AddScript(txtScriptParaEjecutar.Text.Trim());

            // Execute the script
            var results = shell.Invoke();

            // display results, with BaseObject converted to string
            // Note : use |out-string for console-like output
            if (results.Count > 0)
            {
                // We use a string builder ton create our result text
                var builder = new StringBuilder();

                foreach (var psObject in results)
                {
                    // Convert the Base Object to a string and append it to the string builder.
                    // Add \r\n for line breaks
                    builder.Append(psObject.BaseObject.ToString() + "\r\n");
                }

                // Encode the string in HTML (prevent security issue with 'dangerous' caracters like < >
                ResultBox.Text = Server.HtmlEncode(builder.ToString());
            }
        }
        catch (Exception ex)
        {
            VerError("Error al ejecutar el script, " + ex.Message);
        }
    }
}