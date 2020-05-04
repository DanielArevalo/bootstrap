<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table cellpadding="5" cellspacing="0" style="width: 100%">
    <tr>
        <td class="tdI" width="35%">
             Código &#160; <asp:Label ID="lblCodigo" runat="server"></asp:Label>
        </td>
         <td class="tdI">
            Usuario <br/>
            <asp:DropDownList ID="ddlUsuarios" CssClass="dropdown"  runat="server" 
                Height="28px" Width="182px">
            </asp:DropDownList> 
        </td>
         <td class="tdI">
                Identificacion&nbsp;*&nbsp;<br />
                <asp:TextBox ID="txtIdentificacion" runat="server" CssClass="textbox" 
                    MaxLength="128" AutoPostBack="True" 
                    />
        </td>
    </tr> 
    <tr>
        <td>
         Caja <br/>
            <asp:DropDownList ID="ddlCajas" CssClass="dropdown"  runat="server" 
                Height="28px" Width="182px">
            </asp:DropDownList> 
        </td>
        <td>
          Fecha Ingreso <br/> <asp:TextBox ID="txtFechaIngreso" enabled="True" 
                runat="server" CssClass="textbox" 
                MaxLength="10" Width="110px"></asp:TextBox>
                  &nbsp;<asp:CalendarExtender ID="CalendarExtender1" runat="server" 
                        PopupButtonID="Image1" 
                        TargetControlID="txtFechaIngreso"
                        Format="dd/MM/yyyy" >
                    </asp:CalendarExtender>
        </td>
        <td>
            &nbsp;</td>
    </tr>
     <tr>
        <td>
            Fecha Retiro <br/> <asp:TextBox ID="txtFechaRetiro" enabled="False" 
                runat="server" CssClass="textbox" 
                MaxLength="10" Width="110px"></asp:TextBox>
                  &nbsp;<asp:CalendarExtender ID="CalendarExtender2" runat="server" 
                        PopupButtonID="Image2" 
                        TargetControlID="txtFechaRetiro"
                        Format="dd/MM/yyyy" >
                    </asp:CalendarExtender>
        </td>
          <td>
        Estado <asp:CheckBox ID="chkEstado" runat="server" />
        </td>
          <td>
              &nbsp;</td>
    </tr>
</table>
</asp:Content>


