<%@ Control Language="C#" AutoEventWireup="true" 
    CodeFile="ctlCargo.ascx.cs" Inherits="General_Controles_ctlCargo" %>

<asp:DropDownList 
    ID="ddlcargo" 
    runat="server" 
    CssClass="textbox" 
    Width="147px" 
    AppendDataBoundItems="True">
    <asp:ListItem Value="0">Seleccion un Item</asp:ListItem>
</asp:DropDownList>

<asp:RequiredFieldValidator ID="rrfcargo" runat="server" 
    ErrorMessage="Seleccione un Item" Display="Dynamic" 
    ControlToValidate="ddlCentroCosto" 
    ValidationGroup="vgGuardar" InitialValue="0" Font-Size="X-Small" ForeColor="Red" />
