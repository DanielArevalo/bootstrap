<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Lista.aspx.cs" Inherits="Lista" %>


<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>
<%@ Register Src="~/General/Controles/ctlCentroCosto.ascx" TagPrefix="uc2" TagName="ctlCentroCosto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <table>
        <tr>
            <td style="height: 15px; text-align: left; font-size: x-small;" colspan="6">
                <strong>Criterios de Búsqueda:</strong></td>
        </tr>
    </table>
    <table id="tbCriterios" border="0" cellpadding="5" cellspacing="0" width="100%">
        <tr>
            <td>Consecutivo<br />
                <asp:TextBox ID="txtconsecutivo" runat="server" onkeypress="return isNumber(event)" CssClass="textbox" />
            </td>
            <td>Código De Empleado<br />
                <asp:TextBox ID="txtcodempleado" runat="server" onkeypress="return isString(event)" CssClass="textbox" />
            </td>
            <td>Ubicación<br />
                <asp:TextBox ID="txtubicacion" runat="server" onkeypress="return isString(event)" CssClass="textbox" />
            </td>
            <td>Centro De Costo<br />
                <uc2:ctlCentroCosto runat="server" ID="ctlCentroCosto" />
            </td>
        </tr>
    </table>

    <asp:GridView ID="gvContent" runat="server"
        AllowPaging="True"
        AutoGenerateColumns="False"
        GridLines="Horizontal"
        PageSize="20"
        HorizontalAlign="Center"
        ShowHeaderWhenEmpty="True"
        Width="100%"
        DataKeyNames="id_dotacion"
        OnRowEditing="gvLista_RowEditing"
        OnRowDeleting="gvLista_RowDeleting"
        OnRowDataBound="gvLista_RowDataBound"
        OnSelectedIndexChanged="gvLista_SelectedIndexChanged"
        Style="font-size: x-small">
        <Columns>
            <asp:TemplateField HeaderStyle-CssClass="gridIco">
                <ItemTemplate>
                    <asp:ImageButton ID="btnElim" runat="server" CommandName="Delete" ImageUrl="~/Images/gr_elim.jpg" ToolTip="Eliminar" Width="16px" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:ImageButton ID="btnEditar" runat="server" CommandName="Edit" ImageUrl="~/Images/gr_edit.jpg" ToolTip="Editar" Width="16px" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="id_dotacion" HeaderText="Consecutivo" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="cod_empleado" HeaderText="Código Empleado" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="nombre_empleado" HeaderText="Nombre Empleado" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="fecha" HeaderText="Fecha De Dotación" DataFormatString="{0:d}" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="ubicacion" HeaderText="Ubicación" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="cantidad" HeaderText="Cantidad" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="centro_costo" HeaderText="Cod. Centro De Costo" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="desc_centro_costo" HeaderText="Centro De Costo" ItemStyle-HorizontalAlign="Center" />
        </Columns>
        <HeaderStyle CssClass="gridHeader" />
        <PagerStyle CssClass="gridHeader" Font-Bold="False" />
        <RowStyle CssClass="gridItem" />
    </asp:GridView>
    <uc4:mensajegrabar ID="ctlMensajeBorrar" runat="server" />
    <asp:Label ID="lblTotalRegs" runat="server" />
</asp:Content>

