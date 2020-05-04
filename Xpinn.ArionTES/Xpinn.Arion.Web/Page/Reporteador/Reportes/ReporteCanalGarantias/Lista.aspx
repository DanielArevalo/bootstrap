<%@ Page Title=".: Xpinn - Reporte Canal garantias comunitarias:." Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <table style="width: 100%">
        <tr>
            <td style="text-align: left; width: 150px;">Tipo de Archivo<br /><br />
                EXCEL
            </td>
            <td style="text-align: left; vertical-align: top">Nombre del Archivo<br /><br />
                <asp:TextBox ID="txtArchivo" runat="server" Width="346px" placeholder="Nombre del Archivo"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvArchivo" runat="server"
                    ErrorMessage="Ingrese el Nombre del archivo a Generar"
                    ValidationGroup="vgExportar" Display="Dynamic" ControlToValidate="txtArchivo"
                    ForeColor="Red" Style="font-size: x-small;"></asp:RequiredFieldValidator>
                <br />
            </td>
            <td style="text-align: left; width: 284px;">&nbsp;</td>
            <td style="text-align: left; width: 284px;">&nbsp;</td>
        </tr>
    </table>
</asp:Content>

