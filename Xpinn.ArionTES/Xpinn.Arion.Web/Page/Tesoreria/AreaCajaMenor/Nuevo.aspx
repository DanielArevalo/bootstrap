<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" Title=".: Xpinn - Area :." %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha"%>
<%@ Register Src="../../../General/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc1"%>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>  
    <br />
    <table border="0" cellpadding="5" cellspacing="0" width="100%" >
        <tr>
            <td class="tdI" style="text-align:left; width: 342px;">
                Código&nbsp;*&nbsp;<asp:RequiredFieldValidator ID="rfvtipopago" runat="server" ControlToValidate="txtCodigo" ErrorMessage="Campo Requerido" SetFocusOnError="True" ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic"/><br />
                <asp:TextBox ID="txtCodigo" runat="server" CssClass="textbox" MaxLength="128" Width="131px" />
            </td>
            <td class="tdD" style="text-align:left">Fecha de Constitución<br />
                <ucFecha:fecha ID="txtfechaConst" runat="server" Width="131px" ></ucFecha:fecha>                  
            </td>
            <td class="tdD" style="text-align: left;">Responsable&nbsp;&nbsp;<br />
            <asp:DropDownList ID="ddlUsuario" runat="server" CssClass="textbox" Width="95%" />
            <%--<asp:Label ID="lblCodUsuarioResponsable" runat="server" Visible="false" />
            <asp:TextBox ID="txtResponsable" runat="server" CssClass="textbox" MaxLength="128" Width="131px" />--%>
        </td>
            <td class="tdD">    
                Identificacion&nbsp;*&nbsp;<br />
                <asp:TextBox ID="txtIdentificacion" runat="server" CssClass="textbox" 
                    MaxLength="128" AutoPostBack="True" 
                    />
        </tr>
        <tr>
             <td class="tdI" style="text-align:left; width: 342px;">
                Descripción&nbsp;*&nbsp;<asp:RequiredFieldValidator ID="rfvDescripcion" runat="server" ControlToValidate="txtDescripcion" ErrorMessage="Campo Requerido" SetFocusOnError="True" ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic"/><br />
                <asp:TextBox ID="txtDescripcion" runat="server" CssClass="textbox" MaxLength="128" Width="307px" />
            </td>
            <td class="tdD" style="text-align:left; width: 342px;">
                Monto máximo&nbsp;*&nbsp;
                <uc1:decimales ID="txtBase" runat="server" CssClass="textbox" MaxLength="15" Width="131px" />
            </td>   
            <td class="tdI" style="text-align:left; width: 342px;">
                Monto minimo&nbsp;*&nbsp;
                <uc1:decimales ID="txtVMinimo" runat="server" CssClass="textbox" MaxLength="15" Width="131px" />
            </td>                   
            <td class="tdI" style="text-align:left; width: 342px;">
                Centro de Costo&nbsp;*&nbsp;<asp:RequiredFieldValidator ID="rfvCC" runat="server" ControlToValidate="ddlCentroCosto" ErrorMessage="Campo Requerido" SetFocusOnError="True" ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic"  /><br />
                <asp:DropDownList ID="ddlCentroCosto" runat="server" CssClass="textbox" MaxLength="15" Width="131px" />
            </td> 
         </tr>
    </table>
</asp:Content>