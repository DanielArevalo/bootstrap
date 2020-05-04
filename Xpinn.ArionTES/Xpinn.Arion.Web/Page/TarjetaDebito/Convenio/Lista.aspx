<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:Panel ID="pConsulta" runat="server">
        <br />
        <table style="width: 50%;">
            <tr>
                <td>
                    Código<br/>
                    <asp:TextBox ID="txtCodigoBin" runat="server" CssClass="textbox" />
                </td>
                <td>
                    Nombre<br/>
                    <asp:TextBox ID="txtNombre" runat="server" CssClass="textbox" />
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
        </table>
    </asp:Panel>
    <hr />
        <asp:GridView ID="gvLista" runat="server" Width="100%" AutoGenerateColumns="False"
            AllowPaging="True" PageSize="20" GridLines="Horizontal" ShowHeaderWhenEmpty="True"
            OnPageIndexChanging="gvLista_PageIndexChanging" OnRowDeleting="gvLista_RowDeleting"
            OnRowEditing="gvLista_RowEditing" OnSelectedIndexChanged="gvLista_SelectedIndexChanged"
            OnRowDataBound="gvLista_RowDataBound" DataKeyNames="cod_convenio">
            <Columns>                   
                <asp:TemplateField HeaderStyle-CssClass="gridIco">
                    <ItemTemplate>
                        <asp:ImageButton ID="btnInfo" runat="server" CommandName="Select" ImageUrl="~/Images/gr_info.jpg"
                            ToolTip="Detalle" Width="16px" />
                    </ItemTemplate>
                    <HeaderStyle CssClass="gridIco"></HeaderStyle>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-CssClass="gridIco">
                    <ItemTemplate>
                        <asp:ImageButton ID="btnEditar" runat="server" CommandName="Edit" ImageUrl="~/Images/gr_edit.jpg"
                            ToolTip="Editar" Width="16px" />
                    </ItemTemplate>
                    <HeaderStyle CssClass="gridIco"></HeaderStyle>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-CssClass="gridIco">
                    <ItemTemplate>
                        <asp:ImageButton ID="btnEliminar" runat="server" CommandName="Delete" ImageUrl="~/Images/gr_elim.jpg"
                            ToolTip="Eliminar" Width="16px" />
                    </ItemTemplate>
                    <HeaderStyle CssClass="gridIco"></HeaderStyle>
                </asp:TemplateField>
                <asp:BoundField DataField="cod_convenio" HeaderText="Código" >
                    <ItemStyle HorizontalAlign="Left" Width="50px" />
                </asp:BoundField>
                <asp:BoundField DataField="nombre" HeaderText="Nombre" >
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="codigo_bin" HeaderText="Bin" >
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="ip_switch" HeaderText="Ip Switch" >
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="puerto_switch" HeaderText="Puerto" >
                    <ItemStyle HorizontalAlign="Center" Width="40px" />
                </asp:BoundField>
                <asp:BoundField DataField="fecha_convenio" HeaderText="Fecha" >
                    <ItemStyle HorizontalAlign="Center" Width="40px" />
                </asp:BoundField>
                <asp:BoundField DataField="e_mail" HeaderText="EMail" >
                    <ItemStyle HorizontalAlign="Center" Width="40px" />
                </asp:BoundField>
                <asp:BoundField DataField="tipo_procesamiento" HeaderText="Tipo Proceso" >
                    <ItemStyle HorizontalAlign="Center" Width="40px" />
                </asp:BoundField>
                <asp:BoundField DataField="ip_appliance" HeaderText="Ip Appliance" >
                    <ItemStyle HorizontalAlign="Center" Width="40px" />
                </asp:BoundField>
            </Columns>
            <HeaderStyle CssClass="gridHeader" />
            <PagerStyle CssClass="gridHeader" Font-Bold="False" />
            <RowStyle CssClass="gridItem" />
        </asp:GridView>
    <asp:Label ID="lblTotalRegs" runat="server" Visible="False" />
    <asp:Label ID="lblInfo" runat="server" Text="Su consulta no obtuvo ningun resultado."
        Visible="False" />
    <br />
</asp:Content>
