<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
        <asp:Panel ID="panelLocal" runat="server">
            <table border:"0" cellpadding:"0" cellspacing:"0" width:"100%">
                <tr>
                    <td style="text-align: left; width:250px;">Grupo Actividad <br />
                       <asp:DropDownList ID="ddlGrupoact" runat="server" CssClass="textbox" Width="200px" OnSelectedIndexChanged="ddlGrupoact_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                    </td>
                    <td colspan="2" style="text-align: left">Actividad de Riesgo<br />
                           <asp:DropDownList ID="ddlActividad" runat="server" CssClass="textbox" Width="250px" AutoPostBack="True" ></asp:DropDownList>
                     </td>
                    <td></td>
                     <td colspan="3" style="text-align: left">Valor<br />
                         <asp:DropDownList ID="ddlValor" runat="server" CssClass="textbox" Width="150px">
                             <asp:ListItem Value="0">Selecione un valor </asp:ListItem>
                              <asp:ListItem Value="1">Bajo</asp:ListItem>
                              <asp:ListItem Value="2">Medio</asp:ListItem>
                             <asp:ListItem Value="3">Alto</asp:ListItem>
                             <asp:ListItem Value="4">Extremo</asp:ListItem>
                     </asp:DropDownList>
                    </td>
                    <td>
                        <asp:HiddenField ID="hdIdActividad" runat="server" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
</asp:Content>

