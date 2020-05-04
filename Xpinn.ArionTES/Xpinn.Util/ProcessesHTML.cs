using iTextSharp.text.html.simpleparser;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using iTextSharp.text;
using iTextSharp.text.html;
using iTextSharp.text.pdf;

namespace Xpinn.Util
{
    public static class ProcessesHTML
    {

        #region Metodos estaticos conversion de pdf y remplazar

        public static void ConvertPdf(string html, string pDocumentoGenerado, ref string pError)
        {
            using (var ms = new MemoryStream())
            {

                //Create an iTextSharp Document which is an abstraction of a PDF but **NOT** a PDF
                using (var doc = new Document(PageSize.A4, 20f, 10f, 10f, 10f))
                {

                    //Create a writer that's bound to our PDF abstraction and our stream
                    using (var writer = PdfWriter.GetInstance(doc, ms))
                    {

                        //Open the document for writing
                        doc.Open();

                        //Create a new HTMLWorker bound to our document
                        using (var htmlWorker = new HTMLWorker(doc))
                        {

                            //HTMLWorker doesn't read a string directly but instead needs a TextReader (which StringReader subclasses)
                            using (var sr = new StringReader(html))
                            {

                                var tags = new HTMLTagProcessors();
                                // Replace the built-in image processor
                                tags[HtmlTags.IMG] = new CustomImageHTMLTagProcessor();
                                try
                                {
                                    List<IElement> ie = HTMLWorker.ParseToList(sr, null, tags, null);
                                    foreach (IElement element in ie)
                                    {
                                        try
                                        {
                                            doc.Add((IElement)element);
                                        }
                                        catch (Exception ex) { pError = ex.Message; }
                                    }
                                }
                                catch (Exception ex) { pError = ex.Message; }
                            }
                        }
                        doc.Close();

                        try
                        {
                            //Exist File, Then Delete And create again 
                            if (File.Exists(pDocumentoGenerado))
                                File.Delete(pDocumentoGenerado);

                            using (var fileStream = new FileStream(pDocumentoGenerado, FileMode.CreateNew))
                            {
                                fileStream.Write(ms.ToArray(), 0, ms.ToArray().Length);
                            }
                        }
                        catch (Exception e)
                        {
                            //Ignore
                        }
                    }
                }
            }

        }

        public static void ReemplazarEnDocumentoDeWordYGuardarPdf<T>(string pTexto, List<T> plstReemplazos, string pDocumentoGenerado, ref string pError)
        {
            // Validar que exista el texto
            if (pTexto.Trim().Length <= 0)
                return;
            // Hacer los reemplazos de los campos
            foreach (var dFila in plstReemplazos)
            {
                try
                {
                    string cCampo = GetPropValue(dFila, "Campo").ToString().Trim();
                    string cValor = "";
                    cValor = GetPropValue(dFila, "Campo").ToString().Trim().Replace("'", "");
                    pTexto = pTexto.Replace(cCampo, cValor).Replace("'", "");
                }
                catch (Exception exception)
                {
                    //ignore
                }
            }
            // Convertir a PDF
            var pErrors = "";
            ConvertPdf(pTexto, pDocumentoGenerado, ref pErrors);

            if (!string.IsNullOrEmpty(pError))
                pError = pErrors;
        }

        #endregion

        #region Metodos de conversion

        public static Object GetPropValue(this Object obj, String name)
        {
            foreach (String part in name.Split('.'))
            {
                if (obj == null) { return null; }

                Type type = obj.GetType();
                PropertyInfo info = type.GetProperty(part);
                if (info == null) { return null; }

                obj = info.GetValue(obj, null);
            }
            return obj;
        }

        public static T GetPropValue<T>(this Object obj, String name)
        {
            Object retval = GetPropValue(obj, name);
            if (retval == null) { return default(T); }

            // throws InvalidCastException if types are incompatible
            return (T)retval;
        }
        public class CustomImageHTMLTagProcessor : IHTMLTagProcessor
        {
            /// <summary>
            /// Tells the HTMLWorker what to do when a close tag is encountered.
            /// </summary>
            public void EndElement(HTMLWorker worker, string tag)
            {
            }

            /// <summary>
            /// Tells the HTMLWorker what to do when an open tag is encountered.
            /// </summary>
            public void StartElement(HTMLWorker worker, string tag, IDictionary<string, string> attrs)
            {
                iTextSharp.text.Image image;
                var src = attrs["src"];

                if (src.StartsWith("data:image/"))
                {
                    // data:[<MIME-type>][;charset=<encoding>][;base64],<data>
                    var base64Data = src.Substring(src.IndexOf(",") + 1);
                    var imagedata = Convert.FromBase64String(base64Data);
                    image = iTextSharp.text.Image.GetInstance(imagedata);
                }
                else
                {
                    image = iTextSharp.text.Image.GetInstance(src);
                }

                worker.UpdateChain(tag, attrs);
                worker.ProcessImage(image, attrs);
                worker.UpdateChain(tag);
            }
        }

        #endregion
    }
}
