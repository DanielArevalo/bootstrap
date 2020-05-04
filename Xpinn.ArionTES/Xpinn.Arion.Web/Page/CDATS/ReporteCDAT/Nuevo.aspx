<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" Title=".: Reporte CDAT :." ValidateRequest="false" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScritpManager1" runat="server" EnablePageMethods="true" />
    <script src="../../../Scripts/ckeditor/ckeditor.js"></script>
    <br />
    <br />
    <asp:MultiView ID="mvTipoDopcumento" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwDatos" runat="server">

            <div style="margin-top: 2%; display: flex">
                <textarea id="ftxReporte" runat="Server" text="" width="900px"></textarea>
            </div>

            <script>
                CKEDITOR.replace('cphMain_ftxReporte');
                function CopiarTexto() {
                    var aux = document.createElement("input");
                    aux.setAttribute("value", $("#cphMain_ddlVariables option:selected")[0].value);
                    document.body.appendChild(aux);
                    aux.select();
                    document.execCommand("copy");
                    document.body.removeChild(aux);
                    toastr.success('debe pegarlo en el texto.', 'Campo Copiado', { timeOut: 2000 });
                }
            </script>
        </asp:View>
        <asp:View ID="mvFinal" runat="server">
            <asp:Panel ID="PanelFinal" runat="server">
                <table style="width: 100%;">
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <br />
                            <br />
                            <br />
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <asp:Label ID="lblMensajeGrabar" runat="server" Text="Documento Grabado Correctamente"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <br />
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;"></td>
                    </tr>
                </table>

            </asp:Panel>
        </asp:View>
    </asp:MultiView>
</asp:Content>
