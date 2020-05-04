<%@ Page Title="Expinn - Extructura" Language="C#" MasterPageFile="~/General/Master/site.master"
    AutoEventWireup="true" CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" %>

<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar"
    TagPrefix="uc4" %>
<%@ Register Src="~/General/Controles/ctlNumero.ascx" TagName="decimales" TagPrefix="uc1" %>
<%@ Register Src="../../../General/Controles/decimalesGridRow.ascx" TagName="decimalesGridRow"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <%--<asp:Timer runat="server" id="UpdateTimer" interval="15000" ontick="UpdateTimer_Tick" />--%>
    <asp:MultiView ID="mvAplicar" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwLista" runat="server">
            <asp:UpdatePanel ID="UpdatePanel3" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                <ContentTemplate>
                    <table style="width: 740px; text-align: center" cellspacing="0" cellpadding="0">
                        <tr>
                            <td style="text-align: left; width: 200px">
                                Código<br />
                                <asp:TextBox ID="txtCodigo" runat="server" CssClass="textbox" Width="110px" />
                            </td>
                            <td style="text-align: left; width: 160px">
                                &nbsp;
                            </td>
                            <td style="text-align: left; width: 160px">
                                &nbsp;
                            </td>
                            <td style="text-align: left; width: 140px">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left; width: 200px">
                                Entidad<br />
                                <asp:DropDownList ID="ddlEntidad" runat="server" CssClass="textbox" Width="95%" OnSelectedIndexChanged="ddlEntidad_SelectedIndexChanged"
                                    AutoPostBack="True" />
                            </td>
                            <td style="text-align: left;">
                                Cuenta Bancaria<br />
                                <asp:DropDownList ID="ddlCuentaBancaria" runat="server" CssClass="textbox" Width="95%"
                                    AppendDataBoundItems="True" />
                            </td>
                            <td style="text-align: left;">
                                Periodo<br />
                                <asp:DropDownList ID="ddlMes" runat="server" CssClass="textbox" Width="95px" />
                                -
                                <asp:TextBox ID="txtPeriodo" runat="server" CssClass="textbox" Width="50px" placeholder="Año"
                                    MaxLength="4" />
                                <asp:FilteredTextBoxExtender ID="fte11" runat="server" TargetControlID="txtPeriodo"
                                    FilterType="Custom, Numbers" ValidChars="" />
                            </td>
                            <td style="text-align: left;">
                                Saldo Inicial<br />
                                <uc1:decimales ID="txtSaldoIni" runat="server" CssClass="textbox" Habilitado="True"
                                    AutoPostBack_="True" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <hr style="width: 100%" />
                            </td>
                        </tr>
                    </table>
                    <asp:Panel ID="panelGrilla" runat="server">
                        <table style="width: 100%">
                            <tr>
                                <td style="text-align: left">
                                    <strong>Detalle del Extracto</strong><br />
                                    <asp:Button ID="btnAdicionarFila" runat="server" CssClass="btn8" OnClick="btnAdicionarFila_Click"
                                        OnClientClick="btnAdicionarFila_Click" Text="+ Adicionar Detalle" Height="22px" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:GridView ID="gvDetalleEx" runat="server" PageSize="20" ShowHeaderWhenEmpty="True"
                                        AutoGenerateColumns="False" SelectedRowStyle-Font-Size="XX-Small" Style="font-size: small;
                                        margin-bottom: 0px;" DataKeyNames="iddetalle" GridLines="Horizontal" OnRowDataBound="gvDetalleEx_RowDataBound"
                                        OnRowDeleting="gvDetalleEx_RowDeleting">
                                        <Columns>
                                            <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/gr_elim.jpg" ShowDeleteButton="True">
                                                <ItemStyle HorizontalAlign="left" />
                                            </asp:CommandField>
                                            <asp:TemplateField HeaderText="Codigo" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCodigo" runat="server" Text='<%# Bind("iddetalle") %>' /></ItemTemplate>
                                                <ItemStyle HorizontalAlign="center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Fecha" ItemStyle-HorizontalAlign="left">
                                                <ItemTemplate>
                                                    <ucFecha:fecha ID="txtfecha" runat="server" Text='<%# Eval("fecha", "{0:d}") %>' />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Nro Documento" ItemStyle-HorizontalAlign="left">
                                                <ItemTemplate>
                                                    <cc1:TextBoxGrid ID="txtNroDocumen" runat="server" Text='<%# Bind("num_documento") %>'
                                                        Width="130px" Style="text-align: right" CssClass="textbox" /><asp:FilteredTextBoxExtender
                                                            ID="fte11" runat="server" TargetControlID="txtNroDocumen" FilterType="Custom, Numbers"
                                                            ValidChars="+-=/*()." />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Concepto">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblConcepto" runat="server" Text='<%# Bind("cod_concepto") %>' Visible="false" /><cc1:DropDownListGrid
                                                        ID="ddlConcepto" runat="server" CommandArgument="<%#Container.DataItemIndex %>"
                                                        CssClass="textbox" Width="180px">
                                                    </cc1:DropDownListGrid>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Tipo Movimiento">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbltipoMov" runat="server" Text='<%# Bind("tipo_movimiento") %>' Visible="false" /><cc1:DropDownListGrid
                                                        ID="ddlTipoMov" runat="server" CommandArgument="<%#Container.DataItemIndex %>" AutoPostBack="true"
                                                        CssClass="textbox" Width="120px">
                                                    </cc1:DropDownListGrid>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Identificación">
                                                <ItemTemplate>
                                                    <cc1:TextBoxGrid ID="txtIdentificacion" runat="server" Text='<%# Bind("referencia1") %>'
                                                        Width="110px" Style="text-align: right" CssClass="textbox" /></ItemTemplate>
                                                <ItemStyle HorizontalAlign="center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Referencia">
                                                <ItemTemplate>
                                                    <cc1:TextBoxGrid ID="txtReferencia" runat="server" Text='<%# Bind("referencia2") %>'
                                                        Width="110px" Style="text-align: right" CssClass="textbox" /></ItemTemplate>
                                                <ItemStyle HorizontalAlign="center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Valor">
                                                <ItemTemplate>
                                                    <uc1:decimalesGridRow ID="txtValor" runat="server" Text='<%# Eval("valor") %>' Style="text-align: right"
                                                        Habilitado="True" AutoPostBack_="True" Enabled="True" Width_="80" TipoLetra="Small" />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="center" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <HeaderStyle CssClass="gridHeader" />
                                        <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                                        <RowStyle CssClass="gridItem" />
                                        <SelectedRowStyle Font-Size="XX-Small"></SelectedRowStyle>
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <table style="width: 600px">
                        <tr>
                            <td style="width: 150px; text-align: left">
                                Total Débitos<br />
                                <uc1:decimales ID="txtTotalDeb" runat="server" Enabled="false" Habilitado="false" />
                            </td>
                            <td style="width: 150px; text-align: left">
                                Total Créditos<br />
                                <uc1:decimales ID="txtTotalCre" runat="server" Enabled="false" Habilitado="false"/>
                            </td>
                            <td style="width: 150px; text-align: left">
                                Saldo Final<br />
                                <uc1:decimales ID="txtSaldoFin" runat="server" Enabled="false" Habilitado="false"/>
                            </td>
                            <td style="width: 150px; text-align: left">
                                Nro de Registros<br />
                                <asp:TextBox ID="txtNumReg" runat="server" CssClass="textbox" Width="50px" Enabled="false" Habilitado="false"/>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddlEntidad" EventName="SelectedIndexChanged" />
                </Triggers>
            </asp:UpdatePanel>

        </asp:View>
        <asp:View ID="vwVentEmergente" runat="server">
         <table style="width: 90%;">
                            <tr>
                                <td colspan="3" class="gridHeader" style="text-align: center">
                                    CARGA DE ARCHIVOS                                   
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3" style="text-align: center;">
                                    <br />
                                    Estructura de Archivo &nbsp; <asp:DropDownList ID="ddlEstructura" runat="server" CssClass="textbox" Width="300px" />
                                </td>                                
                            </tr>
                            <tr>
                                <td colspan="3" style="text-align: center;">
                                    <asp:FileUpload ID="fuArchivo" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3" style="color: Red; text-align: center">
                                    <asp:Label ID="lblMensajes" runat="server" Style="font-size: x-small"></asp:Label>
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3" style="text-align: center">
                                    <asp:Button ID="btnCargarArchivo" runat="server" CssClass="btn8" Text="Cargar Archivo"
                                        Width="182px" OnClick="btnCargarArchivo_Click" />
                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="btnParar" runat="server" CssClass="btn8" Text="Cancelar" Width="182px"
                                        OnClick="btnParar_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3" style="color: Red; text-align: center">
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
        </asp:View>
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
                        <td style="text-align: center; font-size: large; color: Red">
                            <asp:Label ID="lblmsj" runat="server"></asp:Label>
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
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </asp:View>
    </asp:MultiView>
    <asp:HiddenField ID="HiddenField1" runat="server" />
    <uc4:mensajegrabar ID="ctlMensaje" runat="server" />
</asp:Content>
