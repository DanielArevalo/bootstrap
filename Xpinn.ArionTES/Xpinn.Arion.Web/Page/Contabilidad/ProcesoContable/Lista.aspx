<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/ctlListarCodigo.ascx" TagName="ctlListarCodigo" TagPrefix="ctl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:Panel ID="pConsulta" runat="server">
        <br />
        <table style="width: 70%;">
            <tr>
                <td>Codigo<br />
                    <asp:TextBox ID="txtCodigo" runat="server" CssClass="textbox" />
                </td>
                <td>Tipo Operación<br />
                    <asp:DropDownList ID="ddloperacion" runat="server" CssClass="dropdown"
                        Width="174px" Height="25">
                    </asp:DropDownList>
                </td>
                <td style="width: 50%">Tipo de Comprobante<br />
                    <ctl:ctlListarCodigo ID="ctllistar" runat="server" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <hr />
    <asp:GridView ID="gvLista" runat="server" Width="80%" AutoGenerateColumns="False"
        AllowPaging="True" PageSize="20" GridLines="Horizontal" ShowHeaderWhenEmpty="True"
        OnPageIndexChanging="gvLista_PageIndexChanging" OnRowDeleting="gvLista_RowDeleting"
        OnRowEditing="gvLista_RowEditing" OnSelectedIndexChanged="gvLista_SelectedIndexChanged"
        OnRowDataBound="gvLista_RowDataBound" DataKeyNames="cod_proceso"
        Style="font-size: x-small">
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
            <asp:BoundField DataField="cod_proceso" HeaderText="Código">
                <ItemStyle HorizontalAlign="Left" Width="50px" />
            </asp:BoundField>
            <asp:BoundField DataField="tipo_ope" HeaderText="Tipo Ope.">
                <ItemStyle HorizontalAlign="Center" Width="40px" />
            </asp:BoundField>
            <asp:BoundField DataField="nom_tipo_ope" HeaderText="Descripcion Tipo Ope.">
                <ItemStyle HorizontalAlign="Left" />
            </asp:BoundField>
            <asp:BoundField DataField="tipo_comp" HeaderText="Tipo Comp.">
                <ItemStyle HorizontalAlign="Center" Width="40px" />
            </asp:BoundField>
            <asp:BoundField DataField="nom_tipo_comp" HeaderText="Descripcion Tipo Comp.">
                <ItemStyle HorizontalAlign="Left" />
            </asp:BoundField>
            <asp:BoundField DataField="fecha_inicial" DataFormatString="{0:d}" HeaderText="Fecha Inicial">
                <ItemStyle HorizontalAlign="Center" Width="40px" />
            </asp:BoundField>
            <asp:BoundField DataField="fecha_final" DataFormatString="{0:d}" HeaderText="Fecha Final">
                <ItemStyle HorizontalAlign="Center" Width="40px" />
            </asp:BoundField>
            <asp:BoundField DataField="concepto" HeaderText="Concepto">
                <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="cod_cuenta" HeaderText="Cod.Cuenta">
                <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="cod_est_det" HeaderText="Estru.Detalle">
                <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="tipo_mov" HeaderText="Tipo Mov.">
                <ItemStyle HorizontalAlign="Center" />
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
