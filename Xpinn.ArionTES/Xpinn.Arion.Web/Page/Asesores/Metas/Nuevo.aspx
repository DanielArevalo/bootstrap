<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" %>


<%@ Register src="../../../General/Controles/decimales.ascx" tagname="decimales" tagprefix="uc2" %>


<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table style="width: 25%; margin-right: 0px;" align="left">    
        <tr>
            <td style="height: 25px; width: 444px;"><asp:Label ID="lblIdEjecMeta" runat="server" 
                    Visible="False" /></td>
            <td style="height: 25px; width: 258px;"><asp:Label ID="lblIdMeta" runat="server" Visible="False" /></td>
        </tr>
        <tr>
            <td style="height: 59px; " colspan="2" >
                &nbsp;<asp:Label ID="Label9" runat="server" Text="Ejecutivo"></asp:Label>
                <br />
<asp:DropDownList ID="DdlEjecutivo" runat="server" Enabled="True" Height="26px" Width="302px">
                   
                </asp:DropDownList>
                <br />
                <br />
            </td>
        </tr>
        <tr>
            <td class="columnForm50" colspan="2">
                Meta<br />
<asp:DropDownList ID="DdlMeta" runat="server" Height="26px" Width="302px">
                    
                </asp:DropDownList>
                <br />
            </td>
        </tr>
          <br />
            <td style="height: 7px; width: 444px;">
                <asp:Label ID="Label5" runat="server" Text="Mes"></asp:Label>
                <br />
<asp:DropDownList ID="DdlMes" runat="server">
                    <asp:ListItem Value="1">ENERO</asp:ListItem>
                    <asp:ListItem Value="2">FEBRERO</asp:ListItem>
                    <asp:ListItem Value="3">MARZO</asp:ListItem>
                    <asp:ListItem Value="4">ABRIL</asp:ListItem>
                    <asp:ListItem Value="5">MAYO</asp:ListItem>
                    <asp:ListItem Value="6">JUNIO</asp:ListItem>
                    <asp:ListItem  Value="7">JULIO</asp:ListItem>
                    <asp:ListItem Value="8">AGOSTO</asp:ListItem>
                    <asp:ListItem Value="9">SEPTIEMBRE</asp:ListItem>
                    <asp:ListItem Value="10">OCTUBRE</asp:ListItem>
                    <asp:ListItem Value="11">NOVIEMBRE</asp:ListItem>
                    <asp:ListItem Selected="True"  Value="12">DICIEMBRE</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td style="height: 7px; width: 258px;">
                <asp:Label ID="Label6" runat="server" Text="Valor meta"></asp:Label>
                <br />
                                        <uc2:decimales ID="txtVlrMeta" runat="server" />
            </td>
        </tr>
        <tr>
            <td style="height: 25px; width: 444px;">
                <asp:Label ID="Label7" runat="server" Text="Año"></asp:Label>
                <br />
                <asp:DropDownList ID="DdlYear" runat="server" 
                    onselectedindexchanged="DdlYear_SelectedIndexChanged">
                   <asp:ListItem Value="2015">2015</asp:ListItem>
                    <asp:ListItem>2016</asp:ListItem>
                    <asp:ListItem>2017</asp:ListItem>
                    <asp:ListItem>2018</asp:ListItem>
                    <asp:ListItem>2019</asp:ListItem>
                    <asp:ListItem>2020</asp:ListItem>
                    <asp:ListItem>2021</asp:ListItem>
                    <asp:ListItem>2022</asp:ListItem>
                    <asp:ListItem>2023</asp:ListItem>
                    <asp:ListItem>2024</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td style="height: 25px; width: 258px;">
                <asp:Label ID="Label10" runat="server" Text="Vigencia"></asp:Label>
                <br />
<asp:DropDownList ID="DdlVigencia" runat="server" Height="26px" Width="302px">
                    
                </asp:DropDownList>
                <br />
            </td>
        </tr>     
    </table>
</asp:Content>
