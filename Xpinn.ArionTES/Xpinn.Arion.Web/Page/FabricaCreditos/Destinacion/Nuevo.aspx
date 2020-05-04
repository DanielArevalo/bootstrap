<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" Title=".: Xpinn - Destinacion :." %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
    <br /><br />
    <table border="0" cellpadding="5" cellspacing="0" width="100%" >
        <tr>
            <td class="tdI" style="text-align:left">
                Código&nbsp;*&nbsp;<asp:RequiredFieldValidator ID="rfvdestinacion" runat="server" ControlToValidate="txtCodigo" ErrorMessage="Campo Requerido" SetFocusOnError="True" ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic"/><br />
                <asp:TextBox ID="txtCodigo" runat="server" CssClass="textbox" MaxLength="128" />
            </td>
            <td class="tdD">
            </td>
        </tr>
        <tr>
            <td class="tdI" style="text-align:left">
                Descripción&nbsp;*&nbsp;<asp:RequiredFieldValidator ID="rfvDescripcion" runat="server" ControlToValidate="txtDescripcion" ErrorMessage="Campo Requerido" SetFocusOnError="True" ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic"/><br />
                <asp:TextBox ID="txtDescripcion" runat="server" CssClass="textbox" MaxLength="128" Width="519px" />
            </td>
            <td class="tdD">
            </td>
        </tr>
        <tr>
                    <td colspan="2" style="text-align: left">
                        <asp:CheckBox ID="chkOficinaVirtual" runat="server" Text="Mostrar en oficina virtual" />
                    </td>
                    <td colspan="6" style="text-align: left">Enlace <br />
                        <asp:TextBox ID="txtEnlace" runat="server" CssClass="textbox" Style="width:100%;" />
                    </td>
        </tr>
    </table>
    <br />
    <div style="float:right;">
                    <asp:Label ID="lblerror" Text="" runat="server" /><br />
                    <asp:FileUpload ID="fuFoto" runat="server" BorderWidth="0px" Font-Size="XX-Small"
                                Height="20px" ToolTip="Seleccionar el archivo que contiene la foto" Width="200px" />
                    <asp:HiddenField ID="hdFileName" runat="server" />
                    <asp:HiddenField ID="hdFileNameThumb" runat="server" />
                    <asp:LinkButton ID="linkBt" runat="server" OnClick="linkBt_Click" ClientIDMode="Static" UseSubmitBehavior="false" />
                    <br />
                    <asp:Image ID="imgFoto" runat="server" Height="160px" Width="121px" />
                    <br />
                    <asp:Button ID="btnCargarImagen" runat="server" Text="Cargar Imagen" Font-Size="xx-Small"
                            Height="20px" Width="100px" OnClick="btnCargarImagen_Click" ClientIDMode="Static" UseSubmitBehavior="false" />
                </div>
    <br />
    <div style="margin-right: 90px;">
                <asp:Label ID="lblerrorB" Text="" runat="server" /><br />
                <asp:FileUpload ID="fuBanner" runat="server" BorderWidth="0px" Font-Size="XX-Small"
                            Height="20px" ToolTip="Seleccionar el archivo que contiene el banner" Width="200px" />
                <asp:HiddenField ID="hdFileNameB" runat="server" />
                <asp:HiddenField ID="hdFileNameThumbB" runat="server" />
                <asp:LinkButton ID="linkBtB" runat="server" OnClick="linkBtB_Click" ClientIDMode="Static" UseSubmitBehavior="false" />
                <br />
                <asp:Image ID="imgBanner" runat="server" Height="216px" Width="82%" />
                <br />
                <asp:Button ID="btnCargarImagenB" runat="server" Text="Cargar Banner" Font-Size="xx-Small"
                        Height="20px" Width="100px" OnClick="btnCargarBanner_Click" ClientIDMode="Static" UseSubmitBehavior="false" />
            </div>
</asp:Content>