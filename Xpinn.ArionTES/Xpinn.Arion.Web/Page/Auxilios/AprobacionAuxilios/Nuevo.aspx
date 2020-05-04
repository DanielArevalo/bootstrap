<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" %>

<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>
<%@ Register Src="~/General/Controles/ctlBusquedaRapida.ascx" TagName="ListadoPersonas" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/ctlProveedor.ascx" TagName="BuscarProveedor" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:MultiView ID="mvAplicar" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwLista" runat="server">
            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                <ContentTemplate>
                    <table style="width: 520px; text-align: center" cellspacing="0" cellpadding="0">
                        <tr>
                            <td style="text-align: left; width: 180px">
                                Num. Auxilio<br />
                                <asp:TextBox ID="txtCodigo" runat="server" CssClass="textbox" Width="100px" Enabled="false"/>
                            </td>
                            <td style="text-align: left; width: 140px">
                                Fec. Solicitud<br />
                                <ucFecha:fecha ID="txtFecha" runat="server" CssClass="textbox" Enabled="false"/>
                            </td>
                            <td style="text-align: left; width: 200px">
                                Fec. Aprobación<br />
                                <ucFecha:fecha ID="txtFechaAprobacion" runat="server" CssClass="textbox"/> 
                                <span style="color: #FF3300">*</span>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left; width: 180px">
                                Asociado<br />
                                <asp:TextBox ID="txtCodPersona" runat="server" CssClass="textbox"
                                    Width="50px" Visible="false" />
                                <asp:TextBox ID="txtIdPersona" runat="server" CssClass="textbox" AutoPostBack="true" Enabled="false"
                                    Width="100px" OnTextChanged="txtIdPersona_TextChanged" />
                                <asp:Button ID="btnConsultaPersonas" runat="server" CssClass="btn8" Height="26px" Enabled="false"
                                    OnClick="btnConsultaPersonas_Click" Text="..." />
                            </td>
                            <td style="text-align: left; width: 340px" colspan="2">
                                Nombre<br />
                                <uc1:ListadoPersonas ID="ctlBusquedaPersonas" runat="server" />
                                <asp:TextBox ID="txtNomPersona" runat="server" CssClass="textbox" Enabled="false"
                                    Width="270px" />
                                <asp:RequiredFieldValidator ID="rfvEncargado" runat="server" ControlToValidate="txtNomPersona"
                                    Display="Dynamic" ErrorMessage="Seleccione encargado" ForeColor="Red" InitialValue="0"
                                    Style="font-size: xx-small" ValidationGroup="vgGuardar" />
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left; width: 180px">
                                Linea de Auxilio<br />
                                <asp:DropDownList ID="ddlLinea" runat="server" CssClass="textbox" Width="95%" OnSelectedIndexChanged="ddlLinea_SelectedIndexChanged"
                                    AutoPostBack="True" Enabled="false"/>
                            </td>
                            <td style="text-align: left; width: 140px">
                                Cupos<br />
                                <asp:TextBox ID="txtCupos" runat="server" CssClass="textbox" Width="90%" Enabled="false"/>
                            </td>
                            <td style="text-align: left; width: 200px">
                                Monto Disponible<br />
                                <uc1:decimales ID="txtMontoDisp" runat="server" Enabled="false"/>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left; width: 180px;vertical-align:top">
                                Valor Solicitado<br />
                                <uc1:decimales ID="txtValorSoli" runat="server" Enabled="false"/>
                            </td>
                            <td style="text-align: left; width: 340px" colspan="2">
                                Detalle<br />
                                <asp:TextBox ID="txtDetalle" runat="server" CssClass="textbox" Width="270px" TextMode="MultiLine" Enabled="false"/>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left; width: 180px;vertical-align:top">
                                Valor Aprobado<br />
                                <uc1:decimales ID="txtValorAproba" runat="server" /> 
                                <span style="color: #FF0000">*</span>
                            </td>
                            <td style="text-align: left; width: 340px" colspan="2">
                                Observaciones<br />
                                <asp:TextBox ID="txtObservacionAproba" runat="server" CssClass="textbox" Width="270px"
                                    TextMode="MultiLine" /> <span style="color: #FF3300">*</span>
                            </td>
                        </tr>
                    </table>
                    <uc1:BuscarProveedor ID="ctlBusquedaProveedor" runat="server" />
                    <asp:Panel ID="panelGrilla" runat="server">
                        <table>
                            <tr>
                                <td style="text-align:left">
                                    <strong>Beneficiarios</strong><br />
                                    <asp:Button ID="btnAdicionarFila" runat="server" CssClass="btn8" OnClick="btnAdicionarFila_Click"
                                        OnClientClick="btnAdicionarFila_Click" Text="+ Adicionar Beneficiario" Height="22px" Visible="false"/>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:GridView ID="gvBeneficiarios" runat="server" PageSize="20" ShowHeaderWhenEmpty="True" Enabled="false" ReadOnly="True"
                                        AutoGenerateColumns="False" SelectedRowStyle-Font-Size="XX-Small" Style="font-size: small;
                                        margin-bottom: 0px;" OnRowDataBound="gvBeneficiarios_RowDataBound" OnRowDeleting="gvBeneficiarios_RowDeleting"
                                        DataKeyNames="codbeneficiarioaux" GridLines="Horizontal">
                                        <Columns>
                                            <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/gr_elim.jpg" ShowDeleteButton="True">
                                                <ItemStyle HorizontalAlign="left" />
                                            </asp:CommandField>
                                            <asp:TemplateField HeaderText="Codigo" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCodigo" runat="server" CssClass="textbox" Text='<%# Bind("codbeneficiarioaux") %>' />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Identificación" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <cc1:TextBoxGrid ID="txtIdenti_Grid" runat="server" CssClass="textbox" Text='<%# Bind("identificacion") %>'
                                                        Width="110px"></cc1:TextBoxGrid>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Nombres y Apellidos" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <cc1:TextBoxGrid ID="txtNombreComple" runat="server" CssClass="textbox" Text='<%# Bind("nombre") %>'
                                                        Width="250px" />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Parentesco">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblParentesco" runat="server" Text='<%# Bind("cod_parentesco") %>'
                                                        Visible="false" />
                                                    <cc1:DropDownListGrid ID="ddlParentesco" runat="server" CommandArgument="<%#Container.DataItemIndex %>"
                                                        CssClass="textbox" Width="170px">
                                                    </cc1:DropDownListGrid>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="%Beneficiario">
                                                <ItemTemplate>
                                                    <cc1:TextBoxGrid ID="txtPorcBene" runat="server" CssClass="textbox" Style="text-align: right"
                                                        Text='<%# Bind("porcentaje_beneficiario") %>' Width="100px" />
                                                    <asp:FilteredTextBoxExtender ID="fte3" runat="server" FilterType="Custom, Numbers"
                                                        TargetControlID="txtPorcBene" ValidChars="+-=/*()." />
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
                    <table style="width: 740px">
                        <tr>
                            <td style="width: 740px; text-align: center">
                                <asp:Label ID="lblTotalReg" runat="server" Visible="False" />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddlLinea" EventName="SelectedIndexChanged" />
                </Triggers>
            </asp:UpdatePanel>
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
                            Solicitud de Auxilio Nro &nbsp;<asp:Label ID="lblMsj" runat="server"/>&nbsp; Aprobada&nbsp;correctamente.<br />
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
