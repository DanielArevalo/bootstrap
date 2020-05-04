<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master"  AutoEventWireup="true" CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" Title=".: Xpinn - Garantia :."%>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>
<%@ Register Src="~/General/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="uc2" %>

<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar"
    TagPrefix="uc4" %>
<%@ Register Src="~/General/Controles/decimalesGridRow.ascx" TagName="decimalesGridRow"
    TagPrefix="uc1" %>





<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
   <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:MultiView ID="mvPrincipal" runat="server" ActiveViewIndex="0" Visible="true">
        <asp:View ID="vwDatos" runat="server">
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td>
                        <asp:Panel ID="pConsulta" runat="server">
                            <table id="tbCriterios" border="0" cellpadding="5" cellspacing="0" width="100%">
                                <tr>
                                    <td class="tdI">
                                        <asp:Panel ID="Panel1" runat="server">
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td class="logo" style="text-align: left" colspan="2">
                                                        <asp:CompareValidator ID="cvnumero_radicacion1" runat="server" ControlToValidate="txtnumero_radicacion"
                                                            Display="Dynamic" ErrorMessage="Solo se admiten números" ForeColor="Red" Operator="DataTypeCheck"
                                                            SetFocusOnError="True" Type="Double" ValidationGroup="vgGuardar" />
                                                    </td>
                                                    <td style="text-align: left">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="logo" style="width: 195px; text-align: left">
                                                        Número Radicación
                                                    </td>
                                                    <td style="width: 138px; text-align: left">
                                                        Línea de Crédito
                                                    </td>
                                                    <td style="text-align: left">
                                                        &nbsp;Oficina
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="logo" style="width: 195px; text-align: left">
                                                        <asp:TextBox ID="txtNumero_radicacion" runat="server" CssClass="textbox" MaxLength="128" />
                                                    </td>
                                                    <td style="width: 138px; text-align: left">
                                                        <asp:TextBox ID="txtLinea_credito" runat="server" CssClass="textbox" Width="327px" />
                                                        <br />
                                                    </td>
                                                    <td style="text-align: left">
                                                        &nbsp;<asp:DropDownList ID="ddlOficinas" runat="server" CssClass="textbox" Height="30px"
                                                            Width="191px">
                                                            <asp:ListItem></asp:ListItem>
                                                        </asp:DropDownList>
                                                        <br />
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdI">
                                        <asp:Panel ID="Panel2" runat="server">
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td class="logo" style="width: 197px; text-align: left">
                                                        Identificación
                                                    </td>
                                                    <td style="width: 197px; text-align: left" class="logo">
                                                        Nombre Completo
                                                    </td>
                                                    <td style="text-align: left">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="logo" style="width: 197px; text-align: left">
                                                        <asp:TextBox ID="txtIdentificacion" runat="server" CssClass="textbox" />
                                                        <br />
                                                    </td>
                                                    <td style="width: 342px; text-align: left">
                                                        <asp:TextBox ID="txtNombre" runat="server" CssClass="textbox" Width="531px" />
                                                        <br />
                                                    </td>
                                                    <td style="text-align: left">
                                                        <br />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="logo" style="width: 197px; text-align: left; height: 25px;">
                                                        <asp:CompareValidator ID="cvidentificacion" runat="server" ControlToValidate="txtidentificacion"
                                                            Display="Dynamic" ErrorMessage="Solo se admiten números" ForeColor="Red" Operator="DataTypeCheck"
                                                            SetFocusOnError="True" Type="Double" ValidationGroup="vgGuardar" />
                                                    </td>
                                                    <td style="width: 342px; text-align: left; height: 25px;">
                                                    </td>
                                                    <td style="text-align: left; height: 25px;">
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td>
                        <hr width="100%" noshade>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:GridView ID="gvLista" runat="server" Width="100%" GridLines="Horizontal" AutoGenerateColumns="False"
                            AllowPaging="True" OnPageIndexChanging="gvLista_PageIndexChanging" OnSelectedIndexChanged="gvLista_SelectedIndexChanged"
                            OnRowEditing="gvLista_RowEditing" PageSize="10" HeaderStyle-CssClass="gridHeader"
                            PagerStyle-CssClass="gridPager" RowStyle-CssClass="gridItem" DataKeyNames="numero_radicacion">
                            <Columns>
                                <asp:BoundField DataField="numero_radicacion" HeaderStyle-CssClass="gridColNo" ItemStyle-CssClass="gridColNo">
                                    <HeaderStyle CssClass="gridColNo"></HeaderStyle>
                                    <ItemStyle CssClass="gridColNo" HorizontalAlign="Left"></ItemStyle>
                                </asp:BoundField>
                                <asp:TemplateField HeaderStyle-CssClass="gridIco" ItemStyle-CssClass="gridIco">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnEditar" runat="server" CommandName="Edit" ImageUrl="~/Images/gr_edit.jpg"
                                            ToolTip="Modificar" />
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="gridIco"></HeaderStyle>
                                    <ItemStyle CssClass="gridIco"></ItemStyle>
                                </asp:TemplateField>
                                <asp:BoundField DataField="numero_radicacion" HeaderText="Radicación">
                                    <ItemStyle HorizontalAlign="center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="cod_linea_credito" HeaderText="Línea">
                                    <ItemStyle HorizontalAlign="center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="identificacion" HeaderText="Identificación">
                                    <ItemStyle HorizontalAlign="center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="nombre" HeaderText="Nombre completo">
                                    <ItemStyle HorizontalAlign="center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="oficina" HeaderText="Oficina">
                                    <ItemStyle HorizontalAlign="center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="monto" HeaderText="Monto" DataFormatString="{0:n0}">
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="plazo" HeaderText="Plazo">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="periodicidad" HeaderText="Periodicidad">
                                    <ItemStyle HorizontalAlign="center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="forma_pago" HeaderText="Forma de pago">
                                    <ItemStyle HorizontalAlign="center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="estado" HeaderText="Estado">
                                    <ItemStyle HorizontalAlign="center" />
                                </asp:BoundField>
                            </Columns>
                            <HeaderStyle CssClass="gridHeader"></HeaderStyle>
                            <PagerStyle CssClass="gridPager"></PagerStyle>
                            <RowStyle CssClass="gridItem"></RowStyle>
                        </asp:GridView>
                        <asp:Label ID="lblTotalRegs" runat="server" Visible="False" />
                    </td>
                </tr>
            </table>
        </asp:View>
        <asp:View ID="vwReporte" runat="server">
            <table border="0" cellpadding="0" cellspacing="0" width="700px">
                <tr>
                    <td style="text-align: left; width: 200px">
                        Fecha Garantia<br />
                        <uc2:fecha ID="txtFechaGarantia2" runat="server" cssclass="textbox" />
                    </td>
                    <td style="text-align: left; width: 140px">
                        Valor En Garantia<br />
                        <uc1:decimales ID="txtValorGarantia" runat="server" cssclass="textbox" Enabled="false" />
                    </td>
                    <td style="text-align: left; width: 170px; vertical-align: top">
                        Estado De La Garantia<br />
                        <asp:RadioButtonList ID="rblEstadoGarantia" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="1">Vigente</asp:ListItem>
                            <asp:ListItem Value="2">Terminada</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left; width: 160px">
                        Identificacion<br />
                        <asp:TextBox ID="txtIdentificacion2" runat="server" CssClass="textbox" Width="60%" />
                    </td>
                    <td style="text-align: left; width: 160px">
                        Tipo De Identif.<br />
                        <asp:TextBox ID="txtTipoIdentif" runat="server" CssClass="textbox" Width="60%" />
                    </td>
                    <td style="text-align: left; width: 160px">
                        Nombre<br />
                        <asp:TextBox ID="txtNombre2" runat="server" CssClass="textbox" Width="60%" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left; width: 160px">
                        Numero Del Credito<br />
                        <asp:TextBox ID="txtNumCred" runat="server" CssClass="textbox" Width="60%" />
                    </td>
                    <td style="text-align: left; width: 160px">
                        Linea<br />
                        <asp:TextBox ID="txtLinea2" runat="server" CssClass="textbox" Width="60%" />
                    </td>
                    <td style="text-align: left; width: 200px">
                        F. Aprobacion<br />
                        <uc2:fecha ID="txtFechaAprobacion" runat="server" cssclass="textbox" Enabled="false" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left; width: 140px">
                        Monto<br />
                        <uc1:decimales ID="txtMonto" runat="server" cssclass="textbox" Enabled="false" />
                    </td>
                    <td style="text-align: left; width: 160px">
                        Moneda<br />
                        <asp:TextBox ID="txtMoneda" runat="server" CssClass="textbox" Width="60%" />
                    </td>
                    <td style="text-align: left; width: 140px">
                        Saldo Capital<br />
                        <uc1:decimales ID="txtSaldoCapital" runat="server" cssclass="textbox" Enabled="false" />
                    </td>
                    <td style="text-align: left; width: 160px">
                        Estado Del Credito<br />
                        <asp:TextBox ID="txtEstadoCredito" runat="server" CssClass="textbox" Width="60%"
                            Enabled="false" />
                    </td>
                </tr>
            </table>
            <table border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="text-align: left; width: 710px;" colspan="4">
                        <strong>CDATS En Garantia:</strong><br />
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                            PageSize="10" HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager"
                            RowStyle-CssClass="gridItem" Style="font-size: x-small; margin-right: 0px;" 
                            GridLines="Horizontal" DataKeyNames="numero_radicacion" 
                            Width="710px">
                            <Columns>
                                <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="check" Checked="true" runat="server" Style="text-align: right"
                                                TipoLetra="XX-Small" Habilitado="True" AutoPostBack_="True" Enabled="True" Width_="80" />
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="gridIco"></HeaderStyle>
                                    </asp:TemplateField>
                                <asp:TemplateField HeaderText="Codigo">
                                    <ItemTemplate>
                                        <asp:TextBox ID="lblcod_persona" runat="server" Text='<%# Bind("cod_deudor") %>'
                                            CssClass="textbox" Width="80px" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Numero">
                                    <ItemTemplate>
                                        <asp:TextBox ID="Numero" runat="server" Text='<%# Bind("numero_radicacion") %>' CssClass="textbox"
                                            Width="80px" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="F.Apertura">
                                    <ItemTemplate>
                                        <uc2:fecha ID="Fecha" runat="server" Text='<%# Bind("fecha_aprobacion") %>' CssClass="textbox"
                                            Width="80px" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Linea Cdat">
                                    <ItemTemplate>
                                        <asp:TextBox ID="lineacdat" runat="server" Text='<%# Bind("linea_credito") %>'
                                            CssClass="textbox" Width="80px" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Valor">
                                    <ItemTemplate>
                                        <asp:TextBox ID="valor" runat="server" Text='<%# Bind("valor_cuota") %>' CssClass="textbox"
                                            Width="80px" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Moneda">
                                    <ItemTemplate>
                                        <asp:TextBox ID="moneda" runat="server" Text='<%# Bind("moneda") %>' CssClass="textbox"
                                            Width="80px" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Plazo">
                                    <ItemTemplate>
                                        <asp:TextBox ID="Plazo" runat="server" Text='<%# Bind("plazo") %>' CssClass="textbox"
                                            Width="80px" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Oficina">
                                    <ItemTemplate>
                                        <asp:TextBox ID="Oficina" runat="server" Text='<%# Bind("oficina") %>' CssClass="textbox"
                                            Width="80px" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <HeaderStyle CssClass="gridHeader"></HeaderStyle>
                            <PagerStyle CssClass="gridPager"></PagerStyle>
                            <RowStyle CssClass="gridItem"></RowStyle>
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </asp:View>
       </asp:MultiView>
           <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0" Visible="false">
            <asp:View ID="vwFinal" runat="server">
            <asp:Panel ID="PanelFinal" runat="server">
               <table style="width: 100%;">
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <br />
                            <br />
                            <br />
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;">
                           
                            <asp:Label ID="lblMsj" runat="server"></asp:Label>
                           
                            <br />
                            <br />                            
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <br />
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;">
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </asp:View>
    </asp:MultiView>
    <asp:HiddenField ID="HiddenField1" runat="server" />
    <uc4:mensajegrabar ID="ctlMensaje" runat="server" />
</asp:Content>
