<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/solicitud.master"
    AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Page_FabricaCreditos_Solicitud_InformacionCodeudor_PatrimonioCodeudor_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <table style="width: 100%">
        <tr>
            <td>
                <iframe frameborder="0" height="100%" scrolling="no" onload="autoResize(this.id);"   
                    src="BienesRaices/Lista.aspx" width="100%" id="I1" name="I1"></iframe>
            </td>
            <td>
                <iframe frameborder="0" height="100%" scrolling="no" src="Vehiculos/Lista.aspx"  onload="autoResize(this.id);" 
                    width="100%" id="I2" name="I2"></iframe>
            </td>
        </tr>
        </table>
</asp:Content>

