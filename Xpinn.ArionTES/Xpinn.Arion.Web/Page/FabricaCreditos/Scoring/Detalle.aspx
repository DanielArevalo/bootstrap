<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Detalle.aspx.cs" Inherits="Detalle" Title=".: Xpinn - Fabrica de Creditos :." %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <br />
    <table style="width: 100%; margin: auto;">
        <tr>
            <td style="text-align: center; width: 50%">
                <strong>Visualización de Documento</strong>
            </td>
        </tr>
        <tr>
            <td>
                <hr />
            </td>
        </tr>
        <tr>
            <td style="text-align: center; margin-bottom: 20px; width: 100%; margin: auto;">
                <asp:Literal ID="ltPagare" runat="server" />
            </td>
        </tr>
    </table>
</asp:Content>
