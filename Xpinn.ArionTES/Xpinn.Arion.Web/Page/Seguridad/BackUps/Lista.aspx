<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" Title=".: Xpinn - BackUps :." %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <table border="0" width="90%">
        <tr>
            <td>
                <table id="tbCriterios" border="0" width="100%">
                    <tr>
                        <td style="width: 25%">Nombre Archivo<br />
                            <asp:TextBox ID="txtNombreArchivo" runat="server" CssClass="textbox" MaxLength="300" Width="70%" />
                        </td>
                        <td style="width: 25%">Nombre Extension<br />
                            <asp:TextBox ID="txtNombreExtension" runat="server" CssClass="textbox" MaxLength="300" Width="70%" />
                        </td>
                        <td style="width: 25%">Directorio<br />
                            <asp:TextBox ID="txtDirectorio" runat="server" CssClass="textbox" MaxLength="300" Width="70%" />
                        </td>
                        <td style="width: 25%">Creacion<br />
                            <asp:TextBox ID="txtFechaCreacion" MaxLength="10" CssClass="textbox"
                                runat="server" Width="80%"></asp:TextBox>
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
                <asp:GridView ID="gvLista"
                    runat="server"
                    AllowPaging="True"
                    AutoGenerateColumns="False"
                    GridLines="Horizontal"
                    PageSize="20"
                    HorizontalAlign="Center"
                    ShowHeaderWhenEmpty="True"
                    Width="100%"
                    OnRowCommand="gvLista_RowCommand"
                    OnRowDeleting="gvLista_RowDeleting"
                    OnPageIndexChanging="gvLista_PageIndexChanging"
                    Style="font-size: x-small">
                    <Columns>
                        <asp:TemplateField HeaderStyle-CssClass="gridIco">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnInfo" runat="server" CommandName="Guardar"
                                    ImageUrl="~/Images/gr_guardar.jpg" ToolTip="Guardar" Width="16px" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Name" HeaderText="Nombre" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="Extension" HeaderText="Extension" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="Length" HeaderText="Tamaño (bytes)" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="CreationTime" HeaderText="Creacion" ItemStyle-HorizontalAlign="Center" />
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
    <uc4:mensajegrabar ID="ctlMensajeBorrar" runat="server" />
</asp:Content>
