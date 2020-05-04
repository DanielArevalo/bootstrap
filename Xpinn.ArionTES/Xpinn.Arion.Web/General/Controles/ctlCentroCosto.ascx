<%@ Control Language="C#" AutoEventWireup="true" 
    CodeFile="ctlCentroCosto.ascx.cs" Inherits="General_Controles_ctlCentroCosto" %>

<asp:DropDownList 
    ID="ddlCentroCosto" 
    runat="server" 
    CssClass="textbox" 
    Width="300px" 
    AppendDataBoundItems="True">
    <asp:ListItem Value="0">Seleccion un Item</asp:ListItem>
</asp:DropDownList>

<asp:RequiredFieldValidator 
    ID="rfcentrocosto" runat="server" ErrorMessage="Seleccione un Item" Display="Dynamic" ControlToValidate="ddlCentroCosto" 
    ValidationGroup="vgGuardar" InitialValue="0" Font-Size="X-Small" ForeColor="Red" />