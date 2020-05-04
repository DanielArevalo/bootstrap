using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Diagnostics;
using System.Reflection;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System.Web.UI.WebControls;

/// <summary>
/// Descripción breve de CombinarCorrespondencia
/// </summary>
public class CombinarCorrespondencia
{
    string cDocsSubDir;
    string cDebug;

    public CombinarCorrespondencia()
    {
        cDocsSubDir = "Documentos";
        cDebug = "";
    }

    public void ReemplazarEnDocumentoDeWord(string ArchivoOrigen, string ArchivoDestino, Object rDatos)
    {
        // Crear archivo temporal para hacer el reemplazo
        string ArchivoTemporal = ArchivoDestino.Substring(0, ArchivoDestino.LastIndexOf(".")) + ".docx";
        File.Copy(ArchivoOrigen, ArchivoTemporal, true);
        object missing = System.Reflection.Missing.Value;
        Stream stream = File.Open(ArchivoTemporal, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);

        // Abrir archivo doc/docx.
        using (WordprocessingDocument wordDoc = WordprocessingDocument.Open(stream, true))
        {
            string docText = null;
            using (StreamReader sr = new StreamReader(wordDoc.MainDocumentPart.GetStream()))
            {
                docText = sr.ReadToEnd();
            }

            // Reemplazar los campos del archivo
            string sValor = "";
            int cont = 1;
            FieldInfo[] propiedadesD = rDatos.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
            foreach (FieldInfo f in propiedadesD)
            {
                object obj = f.GetValue(rDatos);
                if (obj != null)
                    sValor = obj.ToString();
                String sCampo = f.Name.ToUpper();
                if (sCampo.Contains(">") && sCampo.IndexOf('>', 0) > 1)
                {
                    sCampo = sCampo.Substring(1, sCampo.IndexOf('>', 0) - 1);
                }
                docText = docText.Replace("«" + sCampo + "»", sValor);
                cont += 1;
            }

            // Guardar el nuevo documento
            using (StreamWriter sw = new StreamWriter(wordDoc.MainDocumentPart.GetStream(FileMode.Create)))
            {
                sw.Write(docText);
            }

            wordDoc.Close();
        }

        // Terminar el proceso
        stream.Close();
    }

    public void ReemplazarEnDocumentoDeWordGrid(string ArchivoOrigen, string ArchivoDestino, GridView gvDatos, int RowIndex)
    {
        // Crear archivo temporal para hacer el reemplazo
        string ArchivoTemporal = ArchivoDestino.Substring(0, ArchivoDestino.LastIndexOf(".")) + ".docx";
        File.Copy(ArchivoOrigen, ArchivoTemporal, true);
        object missing = System.Reflection.Missing.Value;
        Stream stream = File.Open(ArchivoTemporal, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);

        // Abrir archivo doc/docx.
        using (WordprocessingDocument wordDoc = WordprocessingDocument.Open(stream, true))
        {
            string docText = null;
            using (StreamReader sr = new StreamReader(wordDoc.MainDocumentPart.GetStream()))
            {
                docText = sr.ReadToEnd();
            }

            // Reemplazar los campos del archivo
            string sValor = "";
            int cont = 0;
            try
            {
                foreach (BoundField f in gvDatos.Columns)
                {
                    object obj = gvDatos.Rows[RowIndex].Cells[cont].Text;
                    if (obj != null)
                        sValor = obj.ToString();
                    String sCampo = gvDatos.Columns[cont].HeaderText.ToString();
                    if (sCampo.Contains(">") && sCampo.IndexOf('>', 0) > 1)
                    {
                        sCampo = sCampo.Substring(1, sCampo.IndexOf('>', 0) - 1);
                    }
                    docText = docText.Replace("«" + sCampo + "»", sValor);
                    cont += 1;
                }

            }
            catch
            {
                cont = 0;
            }

            // Guardar el nuevo documento
            using (StreamWriter sw = new StreamWriter(wordDoc.MainDocumentPart.GetStream(FileMode.Create)))
            {
                sw.Write(docText);
            }

            wordDoc.Close();
        }

        // Terminar el proceso
        stream.Close();
    }

    public void UnirFicheros(string ficheroOrigen, string ficheroDestino)
    {
        Stream streamDOC = File.Open(ficheroDestino, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
        using (WordprocessingDocument doc = WordprocessingDocument.Open(streamDOC, true))
        {
            //Se coge el último párrafo o el último AltChunk del documento
            Paragraph paragraph = new Paragraph(); ;
            AltChunk alternativeChunk = new AltChunk();
            int numeroAltChunk = doc.MainDocumentPart.Document.Descendants<AltChunk>().Count();
            //Se comprueba si el documento a añadir se tiene que colocar al final del último párrafo o del último AltChunk
            if (numeroAltChunk == 0)
                paragraph = doc.MainDocumentPart.Document.Descendants<Paragraph>().Last();
            else
                alternativeChunk = doc.MainDocumentPart.Document.Descendants<AltChunk>().Last();

            //Añade un AlternativeFormatImportPart al MainDocumentPart
            AlternativeFormatImportPart inDocPart = doc.MainDocumentPart.AddAlternativeFormatImportPart(AlternativeFormatImportPartType.WordprocessingML);

            //Abre el fichero en el que se va a insertar y se añade el stream con el contenido en la parte
            using (FileStream stream = new FileStream(ficheroOrigen, FileMode.Open))
            {
                inDocPart.FeedData(stream);
            }
            //Crear un AltChunk - <w:altChunk>
            AltChunk altChunk = new AltChunk();
            altChunk.Id = doc.MainDocumentPart.GetIdOfPart(inDocPart);

            //Insertar el tag altChunk después del párrafo o después del último AltChunk
            if (numeroAltChunk == 0)
                paragraph.InsertAfterSelf(altChunk);
            else
                alternativeChunk.InsertAfterSelf(altChunk);

            //Guardar el documento
            doc.MainDocumentPart.Document.Save();
        }
        streamDOC.Close();
        System.IO.File.Delete(ficheroOrigen);
    }

    public void Merge(string strOrgDoc, string strCopy, string strOutDoc)
    {
        Microsoft.Office.Interop.Word.ApplicationClass objApp = null;

        //boxing of default values for COM interop purposes 
        object objMissing = Missing.Value;
        object objFalse = false;
        object objTarget = Microsoft.Office.Interop.Word.WdMergeTarget.wdMergeTargetSelected;
        object objUseFormatFrom = Microsoft.Office.Interop.Word.WdUseFormattingFrom.wdFormattingFromSelected;

        try
        {
            objApp = new Microsoft.Office.Interop.Word.ApplicationClass();
            object objOrgDoc = strOrgDoc;

            Microsoft.Office.Interop.Word.Document objDocLast = null;
            Microsoft.Office.Interop.Word.Document objDocBeforeLast = null;
            objDocLast = objApp.Documents.Open(
                ref objOrgDoc, //FileName 
                ref objMissing, //ConfirmVersions 
                ref objMissing, //ReadOnly 
                ref objMissing, //AddToRecentFiles 
                ref objMissing, //PasswordDocument 
                ref objMissing, //PasswordTemplate 
                ref objMissing, //Revert 
                ref objMissing, //WritePasswordDocument 
                ref objMissing, //WritePasswordTemplate 
                ref objMissing, //Format 
                ref objMissing, //Enconding 
                ref objMissing, //Visible 
                ref objMissing, //OpenAndRepair 
                ref objMissing, //DocumentDirection 
                ref objMissing, //NoEncodingDialog 
                ref objMissing //XMLTransform 
                );

            objDocLast.Merge(
                strCopy, //FileName 
                ref objTarget, //MergeTarget 
                ref objMissing, //DetectFormatChanges 
                ref objUseFormatFrom, //UseFormattingFrom 
                ref objMissing //AddToRecentFiles 
                );
            objDocBeforeLast = objDocLast;
            objDocLast = objApp.ActiveDocument;

            if (objDocBeforeLast != null)
            {
                objDocBeforeLast.Close(
                ref objFalse, //SaveChanges 
                ref objMissing, //OriginalFormat 
                ref objMissing //RouteDocument 
                );
            }

            object objOutDoc = strOutDoc;
            objDocLast.SaveAs(
                ref objOutDoc, //FileName 
                ref objMissing, //FileFormat 
                ref objMissing, //LockComments 
                ref objMissing, //PassWord 
                ref objMissing, //AddToRecentFiles 
                ref objMissing, //WritePassword 
                ref objMissing, //ReadOnlyRecommended 
                ref objMissing, //EmbedTrueTypeFonts 
                ref objMissing, //SaveNativePictureFormat 
                ref objMissing, //SaveFormsData 
                ref objMissing, //SaveAsAOCELetter, 
                ref objMissing, //Encoding 
                ref objMissing, //InsertLineBreaks 
                ref objMissing, //AllowSubstitutions 
                ref objMissing, //LineEnding 
                ref objMissing //AddBiDiMarks 
                );

            foreach (Microsoft.Office.Interop.Word.Document objDocument in objApp.Documents)
            {
                objDocument.Close(
                ref objFalse, //SaveChanges 
                ref objMissing, //OriginalFormat 
                ref objMissing //RouteDocument 
                );
            }

        }
        finally
        {

            objApp.Quit(
                ref objMissing, //SaveChanges 
                ref objMissing, //OriginalFormat 
                ref objMissing //RoutDocument 
                );
            objApp = null;
        }

    }

}