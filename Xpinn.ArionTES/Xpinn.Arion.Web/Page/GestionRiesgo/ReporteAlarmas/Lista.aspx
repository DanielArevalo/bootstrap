<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" Title=".: Xpinn - Gestión Alarmas Riesgo :." %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>
<%@ Register Src="~/General/Controles/tnumero.ascx" TagName="numero" TagPrefix="ucnumero" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager> 
    <script type="text/javascript">
        function Consultar(btnConsultar_Click) {
            var obj = document.getElementById("btnConsultar_Click");
            if (obj) { obj.click(); }
        }
    </script>

    <br /><br />    
    <table id="tbCriterios" border="0" cellpadding="1" cellspacing="0" style="width: 50%;">
        <tr>
            <td colspan="2" style="text-align:left">
                <asp:Label ID="lblTitulo" runat="server" Text="Ingrese el período de búsqueda y presione botón de consultar" Font-Bold="True"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="text-align: left;">
                Período<br />
                <ucFecha:fecha ID="txtFecIni" runat="server" CssClass="textbox" Width="85px" />
                a
                <ucFecha:fecha ID="txtFecFin" runat="server" CssClass="textbox" Width="85px" />
            </td>
            <td style="text-align: left; width: 200px">
                Ordenado Por<br />
                <asp:DropDownList ID="ddlOrdenar" runat="server" CssClass="textbox" Width="200px" AutoPostBack="True" OnSelectedIndexChanged="ddlOrdenar_OnSelectedIndexChanged">
                    <asp:ListItem Value=""></asp:ListItem>
                    <asp:ListItem Value="0">Identificación</asp:ListItem>
                    <asp:ListItem Value="1">Nombre</asp:ListItem>
                    <asp:ListItem Value="2">Fecha</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
    </table>
    <br />
    <asp:Panel ID="pListado" runat="server" Visible="false">
    <div style="overflow:scroll; max-height:630px; text-align:center">
    <asp:GridView ID="gvLista" runat="server" Width="100%"  GridLines="Horizontal" AutoGenerateColumns="False"
        onrowediting="gvLista_RowEditing" OnRowDataBound="gvLista_RowDataBound" Style="font-size: x-small" HorizontalAlign="Center"
        HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager" RowStyle-CssClass="gridItem"  DataKeyNames="idalerta" >
        <Columns>
            <asp:BoundField DataField="idalerta" HeaderStyle-CssClass="gridColNo" ItemStyle-CssClass="gridColNo">
                <HeaderStyle CssClass="gridColNo"></HeaderStyle>
                <ItemStyle CssClass="gridColNo" HorizontalAlign="Left"></ItemStyle>
            </asp:BoundField>
            <asp:TemplateField HeaderText="Seleccionar">
                <ItemTemplate>
                    <asp:CheckBox ID="cbModificado" runat="server" Checked="false" />
                </ItemTemplate>
                <ItemStyle HorizontalAlign="left" />
            </asp:TemplateField>
            <asp:BoundField DataField="fecha_alerta" HeaderText="Fecha">
                <ItemStyle HorizontalAlign="center" />
            </asp:BoundField>
            <asp:BoundField DataField="cod_usuario" HeaderText="Cod.Usuario">
                <ItemStyle HorizontalAlign="left" />
            </asp:BoundField>
            <asp:BoundField DataField="tipo_alerta" HeaderText="Tipo Alerta">
                <ItemStyle HorizontalAlign="left" />
            </asp:BoundField>
            <asp:BoundField DataField="cod_persona" HeaderText="Cod.Persona">
                <ItemStyle HorizontalAlign="left" />
            </asp:BoundField>
            <asp:BoundField DataField="identificacion" HeaderText="Identificación">
                <ItemStyle HorizontalAlign="left" />
            </asp:BoundField>
            <asp:BoundField DataField="nombre" HeaderText="Nombre">
                <ItemStyle HorizontalAlign="left" />
            </asp:BoundField>
            <asp:BoundField DataField="tipo_producto" HeaderText="T.Producto">
                <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="numero_producto" HeaderText="Num.Producto">
                <ItemStyle HorizontalAlign="left" />
            </asp:BoundField>
            <asp:BoundField DataField="descripcion" HeaderText="Descripción">
                <ItemStyle HorizontalAlign="left" />
            </asp:BoundField>
            <%--<asp:BoundField DataField="consulta" HeaderText="Consulta">
                <ItemStyle HorizontalAlign="left" />
            </asp:BoundField>--%>
            <asp:BoundField DataField="estado" HeaderText="Estado Actual" >
                <ItemStyle HorizontalAlign="center" />
            </asp:BoundField>
            <asp:BoundField DataField="fechacrea" HeaderText="F.Creación" Visible="False">
                <ItemStyle HorizontalAlign="center" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="Observaciones">
                <ItemTemplate>
                    <cc1:TextBoxGrid ID="txtGestion" runat="server" Visible="true" Width="140px" Font-Size="XX-Small" CssClass="textbox" OnTextChanged="txtGestion_TextChanged" CommandArgument='<%#Container.DataItemIndex %>' AutoPostBack="True" />
                    <%--<asp:CheckBox ID="cbModificado" runat="server" Checked="false" />--%>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="left" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Nuevo Estado">
                <ItemTemplate>
                    <asp:Label ID="lblestado" runat="server" Visible="false" />
                    <cc1:DropDownListGrid ID="ddlestado" runat="server" CssClass="textbox" Width="80px" CommandArgument="<%#Container.DataItemIndex %>" >
                        <asp:ListItem Value="G" Text="Gestionada" />
                    </cc1:DropDownListGrid>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="left" />
            </asp:TemplateField>
        </Columns>
        <HeaderStyle CssClass="gridHeader"></HeaderStyle>
        <PagerStyle CssClass="gridPager"></PagerStyle>
        <RowStyle CssClass="gridItem"></RowStyle>
    </asp:GridView>
    </div>
    </asp:Panel>
    <asp:Label ID="lblTotalRegs" runat="server" Visible="False" />
    
    <br />
    <br />
    
    <uc4:mensajegrabar ID="ctlMensaje" runat="server" />

</asp:Content>
