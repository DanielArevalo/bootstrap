<%@ Page Title=".: Xpinn - Reporte clientes garantias comunitarias :." Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" %>

<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>


<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <table style="width: 100%">
        <tr>
            <td style="text-align: left; width: 150px;">Fecha de corte<br />
                <ucFecha:fecha ID="ucFechaCorte" runat="server" style="text-align: center" /><br />
            </td>
        </tr>
        <tr>
            <td style="text-align: left; width: 150px;">Tipo de Archivo<br />
                EXCEL
            </td>
            <td style="text-align: left; vertical-align: top">Nombre del Archivo<br />
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

