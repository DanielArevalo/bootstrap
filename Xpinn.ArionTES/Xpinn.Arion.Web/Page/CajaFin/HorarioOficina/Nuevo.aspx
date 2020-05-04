<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" %>
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
            Oficina:&#160;<asp:Label ID="lblCodOficina" runat="server"></asp:Label>&#160;<asp:Label ID="lblOficina" runat="server" ></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="tdI">
            Día &#160;&#160;
            <asp:DropDownList ID="ddlDias" CssClass="dropdown"  runat="server" 
                Height="27px" Width="182px">
            </asp:DropDownList> 
        </td>
    </tr>
    <tr>
        <td>
            Hora Inicial
            <asp:TextBox ID="txtHoraIni" runat="server" CssClass="textbox" MaxLength="13" Width="80"></asp:TextBox>
              <asp:MaskedEditExtender
                    ID="MaskedEditExtender1" runat="server" mask="99:99:99" MaskType="Time" AcceptAMPM="true" 
                    TargetControlID="txtHoraIni">
                </asp:MaskedEditExtender>
                <asp:MaskedEditValidator ID="MaskedEditValidator1" ControlToValidate="txtHoraIni" runat="server" 
                 IsValidEmpty="False" 
                EmptyValueMessage="La Fecha Inicial no puede estar Vacia"
                InvalidValueMessage="Formato de Fecha Inicial Invalida"
                ControlExtender="MaskedEditExtender1" />
        </td>
    </tr>
    <tr>
        <td>
            Hora Final
            <asp:TextBox ID="txtHoraFin" runat="server" CssClass="textbox" MaxLength="13" Width="80"></asp:TextBox>
              <asp:MaskedEditExtender
                    ID="MaskedEditExtender2" runat="server" AcceptAMPM="true"  Mask="99:99:99" MaskType="Time"
                    TargetControlID="txtHoraFin">
                </asp:MaskedEditExtender>
                 <asp:MaskedEditValidator ID="MaskedEditValidator2"  ControlToValidate="txtHoraFin" runat="server" 
                 IsValidEmpty="False" 
                EmptyValueMessage="La Fecha Final no puede estar Vacia"
                InvalidValueMessage="Formato de Fecha Final Invalida"
                ControlExtender="MaskedEditExtender2" />
        </td>
    </tr>
    </table> 
</asp:Content>