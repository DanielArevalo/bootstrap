<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" Title=".: Xpinn - Versión :." %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <table border="0" style="width:90%">
        <tr>
            <td>
                <span>Buscar por:&nbsp;&nbsp;&nbsp;</span>
            </td>
        </tr>
        <tr>
            <td>
                <table id="tbCriterios" border="0" width="70%">
                    <tr>
                        <td style="width: 70%">Nombre Archivo<br />
                            <asp:TextBox ID="txtNombreArchivo" runat="server" CssClass="textbox" MaxLength="300" Width="70%" />
                        </td>
                        <td style="width: 30%">Creacion<br />
                            <asp:TextBox ID="txtFechaCreacion" MaxLength="10" CssClass="textbox" runat="server" Width="80%"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender7" runat="server"
                                PopupButtonID="imagenCalendario"
                                TargetControlID="txtFechaCreacion"
                                Format="dd/MM/yyyy">
                            </asp:CalendarExtender>
                            <img id="imagenCalendario" alt="Calendario"
                                src="../../../Images/iconCalendario.png" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <hr />
            </td>
        </tr>
        <tr>
            <td>
                Version&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtVersion" runat="server" CssClass="textbox" Width="20%" ReadOnly="true" /><br />
                <asp:GridView ID="gvLista"
                    runat="server"
                    AllowPaging="False"
                    AutoGenerateColumns="False"
                    GridLines="Horizontal"
                    PageSize="20"
                    HorizontalAlign="Center"
                    ShowHeaderWhenEmpty="True"
                    Width="100%"
                    Style="font-size: small">
                    <Columns>
                        <asp:BoundField DataField="Name" HeaderText="Nombre" ItemStyle-HorizontalAlign="Left" />
                        <asp:BoundField DataField="Extension" HeaderText="Extension" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="Length" HeaderText="Tamaño (bytes)" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="LastWriteTime" HeaderText="Creación" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="DirectoryName" HeaderText="Directorio" ItemStyle-HorizontalAlign="Center" />
                    </Columns>
                    <HeaderStyle CssClass="gridHeader" />
                    <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                    <RowStyle CssClass="gridItem" />
                </asp:GridView>
                <asp:Label ID="lblTotalRegs" runat="server" />
            </td>
        </tr>
    </table>
</asp:Content>
