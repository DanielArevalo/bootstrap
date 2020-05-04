<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Detalle.aspx.cs" Inherits="Detalle" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
 <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <table cellpadding="5" cellspacing="0" style="width: 100%">
    <tr>
        <td class="tdI">
             &#160;&#160;
            <asp:Label ID="lblCodigo" runat="server"></asp:Label>            
        </td>
    </tr>
     <tr>
        <td class="tdI">
            Oficina:&#160;<asp:Label ID="lblCodOficina" runat="server">&#160;</asp:Label><asp:Label ID="lblOficina" runat="server" ></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="tdI">
            Día &#160;&#160;
            <asp:DropDownList ID="ddlDias" enabled="false" CssClass="dropdown"  runat="server" 
                Height="27px" Width="182px">
            </asp:DropDownList> 
        </td>
    </tr>
    <tr>
        <td>
            Hora Inicial
            <asp:TextBox ID="txtHoraIni" enabled="false" runat="server" CssClass="textbox" MaxLength="13" Width="80"></asp:TextBox>
              <asp:MaskedEditExtender
                    ID="MaskedEditExtender1" runat="server" mask="99:99:99" MaskType="Time" AcceptAMPM="true" 
                    TargetControlID="txtHoraIni">
                </asp:MaskedEditExtender>
        </td>
    </tr>
    <tr>
        <td>
            Hora Final
            <asp:TextBox ID="txtHoraFin" enabled="false" runat="server" CssClass="textbox" MaxLength="13" Width="80"></asp:TextBox>
              <asp:MaskedEditExtender
                    ID="MaskedEditExtender2" runat="server" mask="99:99:99" MaskType="Time" AcceptAMPM="true" 
                    TargetControlID="txtHoraFin">
                </asp:MaskedEditExtender>
        </td>
    </tr>
    </table> 
</asp:Content>
