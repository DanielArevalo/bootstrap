<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Detalle.aspx.cs" Inherits="Detalle" %>


<%@ Register src="../../../General/Controles/decimales.ascx" tagname="decimales" tagprefix="uc2" %>


<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table style="width: 25%; margin-right: 0px;" align="left">    
        <tr>
            <td style="height: 25px; width: 177px;"><asp:Label ID="lblIdEjecMeta" runat="server" 
                    Visible="False" /></td>
            <td style="height: 25px; width: 258px;"><asp:Label ID="lblIdMeta" runat="server" Visible="False" /></td>
        </tr>
        <tr>
            <td style="height: 59px; width: 177px;" >
                &nbsp;<asp:Label ID="Label9" runat="server" Text="Primer Nombre"></asp:Label>
                <br />
                <asp:TextBox ID="txtPrimerNombre" runat="server" CssClass="textbox" 
                    Enabled="false" Width="270px"></asp:TextBox>
            </td>
            <td style="height: 59px; width: 258px;">
                <asp:Label ID="Label2" runat="server" Text="Segundo Nombre"></asp:Label>
                <br />
                <asp:TextBox ID="txtSegundoNombre" runat="server" CssClass="textbox" 
                    Enabled="false" Width="296px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td  style="width: 177px">
                <asp:Label ID="Label8" runat="server" Text="Oficina"></asp:Label>
                <br />
                <asp:TextBox ID="txtOficina" runat="server" CssClass="textbox" Enabled="false" 
                    Width="271px"></asp:TextBox>
            </td>
            <td style="width: 258px">
                <asp:Label ID="Label4" runat="server" Text="Nombre Meta"></asp:Label>
                <br />
                <asp:TextBox ID="txtNombMeta" runat="server" CssClass="textbox" Enabled="false" 
                    Width="276px"></asp:TextBox>
                <br />
            </td>
        </tr>
          <br />
            <td style="height: 7px; width: 177px;">
                <asp:Label ID="Label5" runat="server" Text="Mes"></asp:Label>
                <br />
<asp:DropDownList ID="DdlMes" runat="server" Enabled="False">
                    <asp:ListItem Value="1">ENERO</asp:ListItem>
                    <asp:ListItem Value="2">FEBRERO</asp:ListItem>
                    <asp:ListItem Value="3">MARZO</asp:ListItem>
                    <asp:ListItem Value="4">ABRIL</asp:ListItem>
                    <asp:ListItem Value="5">MAYO</asp:ListItem>
                    <asp:ListItem Value="6">JUNIO</asp:ListItem>
                    <asp:ListItem Selected="True" Value="7">JULIO</asp:ListItem>
                    <asp:ListItem Value="8">AGOSTO</asp:ListItem>
                    <asp:ListItem Value="9">SEPTIEMBRE</asp:ListItem>
                    <asp:ListItem Value="10">OCTUBRE</asp:ListItem>
                    <asp:ListItem Value="11">NOVIEMBRE</asp:ListItem>
                    <asp:ListItem Value="12">DICIEMBRE</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td style="height: 7px; width: 258px;">
                <asp:Label ID="Label6" runat="server" Text="Valor meta"></asp:Label>
                <br />
                                        <uc2:decimales ID="txtVlrMeta" runat="server" />
            </td>
        </tr>
        <tr>
            <td style="height: 25px; width: 177px;">
                <asp:Label ID="Label7" runat="server" Text="Año"></asp:Label>
                <br />
                <asp:DropDownList ID="DdlYear" runat="server" 
                    onselectedindexchanged="DdlYear_SelectedIndexChanged" Enabled="False">
                    <asp:ListItem>2014</asp:ListItem>
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
                <br />
            </td>
        </tr>     
    </table>
</asp:Content>
