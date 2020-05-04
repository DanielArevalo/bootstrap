<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" Title=".: Xpinn - Tipos de Documento :." ValidateRequest="False" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScritpManager1" runat="server" EnablePageMethods="true" />
    <script src="../../../Scripts/ckeditor/ckeditor.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/toastr.js/2.1.4/toastr.min.js"></script>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/toastr.js/2.1.4/toastr.css" />
    <br />
    <br />
    <asp:MultiView ID="mvTipoDopcumento" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwDatos" runat="server">
            <div>
                <table border="0" cellpadding="1" cellspacing="0" width="100%">
                    <tr>
                        <td class="tdI" style="text-align: left; width: 15%" colspan="2">Código*&nbsp;<asp:RequiredFieldValidator ID="rfvtipoliq" runat="server" ControlToValidate="txtTipoDocumento" ErrorMessage="Campo Requerido" SetFocusOnError="True" ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic" /><br />
                            <asp:TextBox ID="txtTipoDocumento" runat="server" CssClass="textbox" Width="90%"
                                MaxLength="128" Enabled="False" />
                        </td>
                        <td class="tdI" style="text-align: left; width: 40%" colspan="2">Descripción&nbsp;*&nbsp;<asp:RequiredFieldValidator ID="rfvDescripcion" runat="server" ControlToValidate="txtDescripcion" ErrorMessage="Campo Requerido" SetFocusOnError="True" ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic" /><br />
                            <asp:TextBox ID="txtDescripcion" runat="server" CssClass="textbox" MaxLength="128" Width="95%" />
                        </td>

                    </tr>
                </table>

            </div>
            <div style="margin-top: 2%; display: flex">
                <textarea id="FreeTextBox2" runat="Server" text="" width="900px"></textarea>
                <asp:DropDownList ID="ddlVariables" runat="server" multiple="" CssClass="textbox" onchange="Probando()">
                </asp:DropDownList>
            </div>
            <script>
                CKEDITOR.replace('cphMain_FreeTextBox2');
            </script>
            <script>
                function Probando() {
                    var aux = document.createElement("input");
                    aux.setAttribute("value", $("#cphMain_ddlVariables option:selected")[0].value);
                    document.body.appendChild(aux);
                    aux.select();
                    document.execCommand("copy");
                    document.body.removeChild(aux);
                    toastr.success('debe pegarlo en el texto.', 'Campo Copiado', { timeOut: 5000 });
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
