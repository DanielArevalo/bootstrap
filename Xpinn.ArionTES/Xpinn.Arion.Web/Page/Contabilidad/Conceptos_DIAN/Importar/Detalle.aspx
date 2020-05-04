<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Detalle.aspx.cs" Inherits="Page_Contabilidad_Conceptos_DIAN_Importar_Detalle" %>



<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Src="~/General/Controles/ctlSeleccionarPersona.ascx" TagName="ListadoPersonas"
    TagPrefix="uc1" %>
<%@ Import Namespace="Xpinn.FabricaCreditos.Entities" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
      <script src="../../../../Scripts/PCLBryan.js"></script>
    <script type="text/javascript">
        var upload = '<%= avatarUpload.ClientID  %>';
        function ActiveTabChanged(sender, e) {
        }

        var HighlightAnimations = {};

        function Highlight(el) {
            if (HighlightAnimations[el.uniqueID] == null) {
                HighlightAnimations[el.uniqueID] = Sys.Extended.UI.Animation.createAnimation({
                    AnimationName: "color",
                    duration: 0.5,
                    property: "style",
                    propertyKey: "backgroundColor",
                    startValue: "#FFFF90",
                    endValue: "#FFFFFF"
                }, el);
            }
            HighlightAnimations[el.uniqueID].stop();
            HighlightAnimations[el.uniqueID].play();
        }

    </script>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
   <br />
      <asp:Panel ID="Panel1" runat="server" Width="100%" >
            <table cellpadding="2">
                <tr>
                    <td style="text-align: left;" colspan="4">
                        <strong>Criterios de carga</strong>
                    </td>
                </tr>
               
                <tr>
                    <td colspan="4" style="text-align: left">
                        <strong>Separador del archivo : </strong>&nbsp;&nbsp;&nbsp;|
                        <table width="100%">
                            <tr>
                                <td style="text-align: left; font-size: x-small">
                                    <strong>Orden Carga : </strong> Cod_Cuenta,Cod_Concepto,Cod_Formato ;
                                 
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                 <tr>
                
                    <td style="text-align: left">
                        <input id="avatarUpload" type="file" name="file" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left" colspan="4">
                        <asp:Button ID="btnCargarPersonas" runat="server" CssClass="btn8" OnClientClick="return TestInputFileToImportData(upload);"
                            Height="22px" Text="Cargar" Width="150px" OnClick="btnCargarPersonas_Click" />
                    </td>
                </tr>
            </table>
              <hr style="width: 100%" />
    <table>
        <tr>
            <td style="align-items:flex-start;width:50%">
                Lista Conceptos :
                <br />
                <asp:GridView ID="gvConceptos" runat="server" AutoGenerateColumns="false">
                    <Columns>
                        <asp:BoundField DataField="codconcepto" HeaderText="Cod_Concepto" />
                        <asp:BoundField DataField="nombre" HeaderText="Nombre Conecpto" />
                    </Columns>
                </asp:GridView>
            </td>
            <td style="width:50%;vertical-align:top" >
                Lista Formatos :
                <br />
                <asp:GridView ID="gvFormatos" runat="server" AutoGenerateColumns="false">
                    <Columns>
                        <asp:BoundField DataField="codformato" HeaderText="Cod-Formato" />
                        <asp:BoundField DataField="formato" HeaderText="Foramto" />
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
    </table>
          </asp:Panel>
        
            <table cellpadding="2" width="100%">
             
                <tr>
                    <td>
                        <asp:Panel ID="pnlNotificacion" runat="server" Width="100%" Visible="false">
                            <table style="width: 100%;">
                                <tr>
                                    <td style="text-align: center; font-size: large;">
                                        <br />
                                        <br />
                                        <br />
                                        <br />
                                        <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: center; font-size: large;">Importación de datos generada correctamente
                                    <br />
                                        Revisa la tabla de errores en caso de haber registros que no se guardaron correctamente!.
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td>
                 
                    </td>
                </tr>
            </table>
      <uc4:mensajegrabar ID="ctlMensaje" runat="server" />
</asp:Content>

