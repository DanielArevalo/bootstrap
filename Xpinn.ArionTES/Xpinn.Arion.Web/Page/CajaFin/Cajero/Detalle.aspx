<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Detalle.aspx.cs" Inherits="Detalle" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table cellpadding="5" cellspacing="0" style="width: 100%">
    <tr>
        <td width="35%">
            C&oacute;digo 
            <asp:Label ID="lblCodigo" runat="server" Text=""></asp:Label>
        </td>
         <td class="tdI">
            Usuario  <br/>
            <asp:DropDownList ID="ddlUsuarios" Enabled="false" CssClass="dropdown"  runat="server" 
                Height="28px" Width="182px">
            </asp:DropDownList> 
        </td>
         <td class="tdI">
                Identificacion&nbsp;*&nbsp;<br />
                <asp:TextBox ID="txtIdentificacion" runat="server" CssClass="textbox" 
                    MaxLength="128" AutoPostBack="True" Enabled="false"
                    />
        </td>
    </tr>
    <tr>
        <td>
         Caja <br/>
            <asp:DropDownList ID="ddlCajas" Enabled="false" CssClass="dropdown"  runat="server" 
                Height="28px" Width="182px">
            </asp:DropDownList> 
        </td>
        <td colspan="2">
          Fecha Ingreso <br/><asp:TextBox ID="txtFechaIngreso" enabled="false" runat="server" CssClass="textbox" 
                MaxLength="10" Width="110px"></asp:TextBox>
        </td>
    </tr>
     <tr>
        <td>
          Fecha Retiro <br><asp:TextBox ID="txtFechaRetiro" enabled="false" runat="server" CssClass="textbox" 
                MaxLength="10" Width="110px"></asp:TextBox>
        </td>
         <td colspan="2">
            Estado <asp:CheckBox ID="chkEstado" enabled="false" runat="server" />
        </td>
    </tr>
</table>
</asp:Content>


