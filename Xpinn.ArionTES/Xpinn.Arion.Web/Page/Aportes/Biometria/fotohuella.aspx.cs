using System;

using System.Collections.Generic;

using System.Linq;

using System.Web;

using System.Web.UI;

using System.Web.UI.WebControls;



using System.IO;

using System.Web.Script.Services;

using System.Web.Services;



[ScriptService]

public partial class Page_Aportes_Biometria_fotohuella : System.Web.UI.Page
{

    static string path = @"C:\estaciona\nacl_sdk\pepper_47\examples\";

    protected void Page_Load(object sender, EventArgs e)
    {



    }



    [WebMethod()]

    public static void UploadImage(string imageData)
    {

        string fileNameWitPath = path + DateTime.Now.ToString().Replace("/", "-").Replace(" ", "- ").Replace(":", "") + ".jpeg";

        using (FileStream fs = new FileStream(fileNameWitPath, FileMode.Create))
        {

            using (BinaryWriter bw = new BinaryWriter(fs))
            {

                byte[] data = Convert.FromBase64String(imageData);

                bw.Write(data);

                bw.Close();
            }

        }

    }

}
