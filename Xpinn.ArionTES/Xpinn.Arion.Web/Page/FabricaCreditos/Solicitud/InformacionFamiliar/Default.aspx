<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Page_FabricaCreditos_Solicitud_InformacionFamiliar_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
    <table style="width: 100%">
        <tr>
            <td style="text-align: center">
                &nbsp;</td>
            <td style="text-align: center">
                &nbsp;</td>
        </tr>
        <tr>
            <td style="text-align: center">
                GrupoFamiliar<br />
                <asp:ImageButton ID="btnGrupoFamiliar" runat="server" 
                    ImageUrl="~/Images/btnConsultar.jpg" onclick="btnGrupoFamiliar_Click" />
            </td>
            <td style="text-align: center">
                Conyuge<br />
                <asp:ImageButton ID="btnConyugue" runat="server" 
                    ImageUrl="~/Images/btnConsultar.jpg" onclick="btnConyugue_Click" />
            </td>
        </tr>
        <tr>
            <td style="text-align: center">
                &nbsp;</td>
            <td style="text-align: center">
                &nbsp;</td>
        </tr>
    </table>
</asp:Content>

