<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
        <asp:Panel ID="panelLocal" runat="server">
            <br /><br />
            <table border:"0" cellpadding:"0" cellspacing:"0" width:"100%">
                <tr>
                    <td>Tipo<br />
                        <asp:DropDownList ID="ddlTipo" runat="server" Width="100px" CssClass="dropdown" Height="25px" AutoPostBack="True"
                             OnSelectedIndexChanged="ddlTipo_OnSelectedIndexChanged" >
                            <asp:ListItem Value="P">País</asp:ListItem>
                            <asp:ListItem Value="C">Ciudad</asp:ListItem>
                            <asp:ListItem Value="D">Estado/Departamento</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td style="text-align: left;">Departamento/Ciudad Jurisdiccion<br />
                       <asp:DropDownList ID="ddlDep" runat="server" CssClass="textbox" Width="300px"  AutoPostBack="True"></asp:DropDownList>
                    </td>
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
                        <asp:HiddenField ID="hdIdjurisdic" runat="server" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
</asp:Content>

