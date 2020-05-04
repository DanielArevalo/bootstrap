<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" Title=".: Xpinn - Usuario :." %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register src="../../../General/Controles/fecha.ascx" tagname="fecha" tagprefix="ucFecha" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <br />
    <asp:CollapsiblePanelExtender ID="cpeDemo" runat="Server" TargetControlID="pConsulta"
        ExpandControlID="pEncBusqueda" CollapseControlID="pEncBusqueda" Collapsed="False"
        TextLabelID="lblMostrarDetalles" ImageControlID="imgExpand" ExpandedText="(Click Aqui para Ocultar Detalles...)"
        CollapsedText="(Click Aqui para Mostrar Detalles...)" ExpandedImage="~/Images/collapse.jpg"
        CollapsedImage="~/Images/expand.jpg" SuppressPostBack="true" SkinID="CollapsiblePanelDemo" />
    <asp:Panel ID="pEncBusqueda" runat="server" CssClass="collapsePanelHeader" Height="30px">
        <div style="padding: 5px; cursor: pointer; vertical-align: middle;">
            <div style="float: left; color: #0066FF; font-size: small">
                Criterios de Búsqueda</div>
            <div style="float: left; margin-left: 5px; color: #0066FF; font-size: small">
                <asp:Label ID="lblMostrarDetalles" runat="server">(Mostrar Detalles...)</asp:Label>
            </div>
            <div style="float: right; vertical-align: middle;">
                <asp:ImageButton ID="imgExpand" runat="server" ImageUrl="~/Images/expand.jpg" AlternateText="(Show Details...)" />
            </div>
        </div>
    </asp:Panel>
    <asp:Panel ID="pConsulta" runat="server">
        <table id="tbCriterios" border="0" cellpadding="0" cellspacing="5" width="90%">
            <tr>
                <td>
                    Código Cuenta<br />
                    <asp:TextBox ID="txtCodCuenta" runat="server" CssClass="textbox" Width="100px"></asp:TextBox>
                    <br />
                </td>
                <td class="tdD">
                    Nombre de Cuenta<br />
                    <asp:TextBox ID="txtNombre" runat="server" CssClass="textbox" Width="180px" />
                </td>
                <td class="tdD">
                    Tipo<br />
                    <asp:DropDownList ID="ddlTipo" runat="server" Width="100px" CssClass="textbox">
                        <asp:ListItem Value=""></asp:ListItem>
                        <asp:ListItem Value="D">Débito</asp:ListItem>
                        <asp:ListItem Value="C">Crédito</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="tdD">
                    Nivel<br />
                    <asp:TextBox ID="txtNivel" runat="server" Width="40px" CssClass="textbox"></asp:TextBox>
                </td>
                <td class="tdD">
                    Depende de<br />
                    <asp:DropDownList ID="ddlDepende" runat="server" CssClass="textbox" Width="120px"
                        AppendDataBoundItems="True">
                        <asp:ListItem Value=""></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="tdI">
                    Estado<br />
                    <asp:DropDownList ID="ddlEstado" runat="server" Width="120px" CssClass="textbox">
                        <asp:ListItem Value=""></asp:ListItem>
                        <asp:ListItem Value="0">Inactiva</asp:ListItem>
                        <asp:ListItem Value="1">Activa</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="tdD">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="7" style="text-align: left">
                    <asp:CheckBox ID="chkTerceros" runat="server" Style="font-size: x-small" Text="Maneja Terceros" />
                    &nbsp;
                    <asp:CheckBox ID="chkCentroCosto" runat="server" Style="font-size: x-small" Text="Maneja Centro Costo" />
                    &nbsp;
                    <asp:CheckBox ID="chkImpuestos" runat="server" Style="font-size: x-small" Text="Maneja Impuestos" />
                    <asp:CheckBox ID="chkCentroGestion" runat="server" Style="font-size: x-small" Text="Maneja Centros Gestión" />
                    &nbsp;
                    <asp:CheckBox ID="chkGiro" runat="server" Style="font-size: x-small" Text="Maneja Cuenta x Pagar" />
                    &nbsp;
                    <asp:CheckBox ID="chkReportarMayor" runat="server" Style="font-size: x-small" Text="Reportar Cuentas Mayores" />
                    &nbsp;
                    <br />
                </td>
            </tr>
            <tr>
                <td class="tdD" colspan="5" style="text-align: left">
                </td>
                <td class="tdD" style="text-align: left">
                </td>
                <td class="tdD" style="text-align: left">
                </td>
            </tr>
        </table>
    </asp:Panel>
    <hr />
    <asp:GridView ID="gvLista" runat="server" Width="100%" AutoGenerateColumns="False"
        OnRowDataBound="gvLista_RowDataBound" OnRowDeleting="gvLista_RowDeleting" AllowPaging="True"
        OnPageIndexChanging="gvLista_PageIndexChanging" OnRowEditing="gvLista_RowEditing"
        PageSize="20" HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager"
        RowStyle-CssClass="gridItem" DataKeyNames="cod_cuenta_niif"
        Style="font-size: x-small">
        <Columns>           
            <asp:TemplateField HeaderStyle-CssClass="gridIco" ItemStyle-CssClass="gridIco">
                <ItemTemplate>
                    <asp:ImageButton ID="btnEditar" runat="server" CommandName="Edit" ImageUrl="~/Images/gr_edit.jpg"
                        ToolTip="Modificar" /></ItemTemplate>
                <HeaderStyle CssClass="gridIco"></HeaderStyle>
                <ItemStyle CssClass="gridIco"></ItemStyle>
            </asp:TemplateField>
            <asp:TemplateField HeaderStyle-CssClass="gridIco" ItemStyle-CssClass="gridIco">
                <ItemTemplate>
                    <asp:ImageButton ID="btnBorrar" runat="server" CommandName="Delete" ImageUrl="~/Images/gr_elim.jpg"
                        ToolTip="Borrar" /></ItemTemplate>
                <HeaderStyle CssClass="gridIco"></HeaderStyle>
                <ItemStyle CssClass="gI" />
            </asp:TemplateField>
            <asp:BoundField DataField="cod_cuenta_niif" HeaderText="Cod.Cuenta NIF">
                <ItemStyle HorizontalAlign="Left" />
            </asp:BoundField>
            <asp:BoundField DataField="nombre_niif" HeaderText="Nombre NIF">
                <ItemStyle HorizontalAlign="Left" />
            </asp:BoundField>
            <asp:BoundField DataField="tipo" HeaderText="Tipo" />
            <asp:BoundField DataField="nivel" HeaderText="Nivel" />
            <asp:BoundField DataField="depende_de_niif" HeaderText="Depende de.NIF" />
            <asp:BoundField DataField="moneda" HeaderText="Moneda" />
            <asp:BoundField DataField="estado" HeaderText="Activa" />
            <asp:TemplateField HeaderText="Terceros">
                <ItemTemplate>
                    <asp:CheckBox ID="chkTerceros" runat="server" EnableViewState="true" Checked='<%#Convert.ToBoolean(Eval("maneja_ter")) %>'
                        Enabled="False" /></ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Maneja C/C">
                <ItemTemplate>
                    <asp:CheckBox ID="chkManejaCC" runat="server" EnableViewState="true" Checked='<%#Convert.ToBoolean(Eval("maneja_cc")) %>'
                        Enabled="False" /></ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Maneja C/G">
                <ItemTemplate>
                    <asp:CheckBox ID="chkManejaCG" runat="server" EnableViewState="true" Checked='<%#Convert.ToBoolean(Eval("maneja_sc")) %>'
                        Enabled="False" /></ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Maneja Impuestos">
                <ItemTemplate>
                    <asp:CheckBox ID="chkManejaImp" runat="server" EnableViewState="true" Checked='<%#Convert.ToBoolean(Eval("impuesto")) %>'
                        Enabled="False" /></ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Maneja CxP">
                <ItemTemplate>
                    <asp:CheckBox ID="chkManejaCP" runat="server" EnableViewState="true" Checked='<%#Convert.ToBoolean(Eval("maneja_gir")) %>'
                        Enabled="False" /></ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Rep.Mayor">
                <ItemTemplate>
                    <asp:CheckBox ID="chkReportarMayor" runat="server" EnableViewState="true" Checked='<%#Convert.ToBoolean(Eval("reportarmayor")) %>'
                        Enabled="False" /></ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="base_minima" HeaderText="Base Mínima" />
            <asp:BoundField DataField="porcentaje_impuesto" HeaderText="% Impuesto" />
        </Columns>
        <HeaderStyle CssClass="gridHeader"></HeaderStyle>
        <PagerStyle CssClass="gridPager"></PagerStyle>
        <RowStyle CssClass="gridItem"></RowStyle>
    </asp:GridView>
    <center>
        <asp:Label ID="lblTotalRegs" runat="server" Visible="False" />
        <asp:Label ID="lblInfo" runat="server" Text="Su consulta no obtuvo ningún resultado"
            Visible="False" /></center>
</asp:Content>
