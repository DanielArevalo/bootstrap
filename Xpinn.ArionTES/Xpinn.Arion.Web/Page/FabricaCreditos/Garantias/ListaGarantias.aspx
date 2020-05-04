<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/General/Master/site.master" CodeFile="ListaGarantias.aspx.cs" Inherits="ListaGarantias" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <script src="../../../Scripts/PCLBryan.js" type="text/javascript"></script>
    <table border="0" style="height: 84px; width: 900px" cellspan="1">
        <tr>
            <td style="width: 209px; height: 51px;" class="logo">
                <asp:Label ID="Labelnumero_credito"
                    runat="server" Text="Número de Crédito"></asp:Label>
                <br />
                <asp:TextBox ID="txtNumCredito" CssClass="textbox" onkeypress="return isNumber(event)" runat="server" Width="180px">
                </asp:TextBox>
            </td>
            <td style="width: 218px; height: 51px;" class="logo">&nbsp;
                <asp:Label ID="Labelestado" runat="server"
                    Text="Estado"></asp:Label>
                <br />
                <asp:DropDownList ID="ddlEstado" Width="180px" class="textbox" runat="server">
                    <asp:ListItem Value="0">Todos los estados 
                    </asp:ListItem>
                    <asp:ListItem Value="1">Activo
                    </asp:ListItem>
                    <asp:ListItem Value="2">Terminado/Liberado
                    </asp:ListItem>
                    <asp:ListItem Value="3">Anulado
                    </asp:ListItem>
                </asp:DropDownList>
            </td>
            <td align="left" class="logo"
                style="width: 197px; height: 51px; text-align:center">
                <asp:Label ID="LabelMonto" runat="server" Text="Monto"></asp:Label>
                <br />
                <asp:Label ID="LabelInicial" runat="server" Text="Inicial"></asp:Label>
                <asp:TextBox ID="txtMontoIni" onkeypress="return isNumber(event)" CssClass="textbox" runat="server"
                    Width="100px" />
            </td>
            <td style="width: 73px; height: 51px;">
                <asp:Label ID="LabelPlazo" runat="server" Text="Plazo"></asp:Label>
                <br />
                <asp:TextBox ID="txtPlazoIni" onkeypress="return isNumber(event)" MaxLength="2" CssClass="textbox"
                    runat="server" Width="31px" />
            </td>
            <td style="height: 51px">&nbsp;&nbsp;
                <asp:Panel ID="Panel1" runat="server" Width="130px">
                    <asp:Label ID="LabelFecha_gara" runat="server" Text="Fecha Garantía"></asp:Label>
                    <asp:TextBox ID="txtFechaIni" MaxLength="10" CssClass="textbox"
                        runat="server" Width="80px"></asp:TextBox>
                    <asp:CalendarExtender ID="CalendarExtender1" runat="server"
                        PopupButtonID="Image1"
                        TargetControlID="txtFechaIni"
                        Format="dd/MM/yyyy">
                    </asp:CalendarExtender>
                    <img id="Image1" alt="Calendario"
                        src="../../../Images/iconCalendario.png" />
                </asp:Panel>
                &nbsp;<br />
            </td>
            <td>
                Identificación <br />
                <asp:TextBox runat="server"  CssClass="textbox" ID="txtIdentificacion" onkeypress="return isNumber(event)" />
            </td>
        </tr>
        <tr>
            <td class="logo" style="width: 209px">&nbsp;<asp:Label ID="LabelLínea" runat="server" Text="Línea"></asp:Label>
                <asp:DropDownList ID="ddlLineasCred" Width="180px" class="textbox" runat="server">
                </asp:DropDownList>
                <br />
            </td>
            <td class="logo" style="width: 218px">
                <asp:Label ID="LabelTipo_Garantía" runat="server" Text="Tipo de Garantía"></asp:Label>
                <asp:DropDownList ID="ddlTipoGarantia" Width="180px" class="textbox" runat="server">
                    <asp:ListItem Value="0" Text="Todos los tipos"></asp:ListItem>
                    <asp:ListItem Value="2" Text="Hipotecario"></asp:ListItem>
                    <asp:ListItem Value="3" Text="Prendaria"></asp:ListItem>
                </asp:DropDownList>
                <br />
            </td>
            <td class="logo" style="width: 197px" align="left">
                <br />
                <asp:Label ID="LabelFinal" runat="server" Text="Final "></asp:Label>
                <asp:TextBox ID="txtMontoMax" onkeypress="return isNumber(event)" CssClass="textbox" runat="server" Width="100px" />
            </td>
            <td style="width: 73px">
                <br />
                <asp:TextBox ID="txtPlazoMax" onkeypress="return isNumber(event)" CssClass="textbox" MaxLength="2" runat="server" Width="31px" />
            <td>
                <br />
                <asp:Panel ID="Panel2" runat="server" Width="130px">
                    <asp:TextBox ID="txtFechaFin" CssClass="textbox"
                        MaxLength="10" runat="server"
                        Width="80px"></asp:TextBox>
                    <asp:CalendarExtender ID="CalendarExtender2" runat="server"
                        PopupButtonID="Image2"
                        TargetControlID="txtFechaFin"
                        Format="dd/MM/yyyy">
                    </asp:CalendarExtender>
                    <img id="Image2" alt="Calendario"
                        src="../../../Images/iconCalendario.png" />
                </asp:Panel>
            </td>
        </tr>
    </table>
    <br />
    <br />
    <table border="0" style="height: 84px; width: 100%">
        <tr>
            <td>
                <asp:GridView ID="gvGarantia" runat="server" Width="100%" GridLines="Horizontal" AutoGenerateColumns="False"
                    OnPageIndexChanging="gvGarantia_PageIndexChanging" AllowPaging="True" OnRowEditing="gvGarantias_RowEditing"
                    OnRowCommand="OnRowCommandDeleting" OnRowDeleting="gvGarantias_RowDeleting"
                    PageSize="20" HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager" RowStyle-CssClass="gridItem" DataKeyNames="IdGarantia" style="font-size: small">
                    <Columns>
                        <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/gr_elim.jpg" ShowDeleteButton="True" HeaderStyle-CssClass="gridIco" Visible="False">
<HeaderStyle CssClass="gridIco"></HeaderStyle>

                            <ItemStyle Width="16px" CssClass="gridIco" />
                        </asp:CommandField>
                        <asp:TemplateField HeaderStyle-CssClass="gridIco" ItemStyle-CssClass="gridIco">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnEditar" runat="server" CommandName="Edit" ImageUrl="~/Images/gr_edit.jpg" ToolTip="Modificar" />
                            </ItemTemplate>

<HeaderStyle CssClass="gridIco"></HeaderStyle>

<ItemStyle CssClass="gridIco"></ItemStyle>
                        </asp:TemplateField>
                        <asp:BoundField DataField="identificacion" HeaderText="Identificación" />
                        <asp:BoundField DataField="NumeroRadicacion" HeaderText="Número Radicación" />
                        <asp:BoundField DataField="FechaGarantia" DataFormatString="{0:d}" HeaderText="Fecha Garantía" />
                        <asp:BoundField DataField="nombre_tipo_garantia" HeaderText="Tipo Garantia" />
                        <asp:BoundField DataField="Valor" HeaderText="Valor Garantía " DataFormatString="{0:N0}">
                            <ItemStyle HorizontalAlign="center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Ubicacion" HeaderText="Ubicación" />
                        <asp:BoundField DataField="FechaVencimiento" DataFormatString="{0:d}" HeaderText="Fecha Venc. Seguro" />
                        <asp:BoundField DataField="Encargado" HeaderText="Encargado" />
                        <asp:BoundField DataField="FechaAvaluo" DataFormatString="{0:d}" HeaderText="Fecha Avalúo" />
                        <asp:BoundField DataField="FechaLiberacion" DataFormatString="{0:d}" HeaderText="Fecha Liberación" />
                        <asp:BoundField DataField="Estado" HeaderText="Estado" />
                    </Columns>

<HeaderStyle CssClass="gridHeader"></HeaderStyle>

<PagerStyle CssClass="gridPager"></PagerStyle>

<RowStyle CssClass="gridItem"></RowStyle>
                </asp:GridView>
                <asp:Label ID="lblNumeroRegistros" Visible="false" runat="server"></asp:Label>
            </td>
        </tr>
    </table>
    <asp:Label ID="lblAvisoNoResultadoGrilla" Visible="false" runat="server" Text="No hay resultados para la búsqueda"></asp:Label>
    <uc4:mensajegrabar ID="ctlMensaje" runat="server" />
</asp:Content>
