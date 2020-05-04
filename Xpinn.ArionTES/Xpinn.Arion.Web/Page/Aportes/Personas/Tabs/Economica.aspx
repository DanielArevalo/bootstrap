<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Economica.aspx.cs" Inherits="Page_Aportes_Personas_Tabs_Personal" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/direccion.ascx" TagName="direccion" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/tnumero.ascx" TagName="txtPesos" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/decimalesGridRow.ascx" TagName="decimalesGridRow" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/ctlNumero.ascx" TagName="numero" TagPrefix="uc5" %>
<%@ Register Src="~/General/Controles/ctlFormaPago.ascx" TagName="Forma" TagPrefix="uc3" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <style type="text/css">
        * {
            font-family: Arial, Helvetica, sans-serif;
            font-size: 12px;
        }

        .btn8 {
            background-color: #0099ff;
            color: #fff;
            border: 2px solid #0099ff;
            margin: 10px auto;
            font-size: 12px;
        }

        .numeric {
            width: 110px;
            text-align: right;
        }

        .required:invalid {
            border: 1px solid #ff6a00; /*ff6a00  F28C00*/
        }
    </style>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UdpContenido" runat="server" UpdateMode="Conditional">
            <ContentTemplate>

                <div>
                    <asp:Label ID="lblerror" Text="" runat="server" />
                </div>
                <div>
                    <asp:TextBox ID="txtCodLinApor" runat="server" Visible="false"></asp:TextBox>
                    <asp:TextBox ID="txtTipCuoApor" runat="server" Visible="false"></asp:TextBox>
                </div>
                <div id="contenido">
                    <table style="text-align: left; width: 100%; padding-top: 15px;">
                        <tr style="text-align: center;">
                            <td colspan="8" style="color: #FFFFFF; background-color: #5295fa; height: 30px; width: 100%;">
                                <strong>Información Económica</strong>
                            </td>
                        </tr>
                    </table>
                    <table style="width: 100%; padding-top: 15px;">
                        <tr>
                            <td colspan="6">
                                <hr style="width: 100%" />
                            </td>
                        </tr>
                        <tr>
                            <td class="gridHeader" colspan="3" style="height: 20px; width: 100%;"><strong>Ingresos Mensuales</strong> </td>
                            <td class="gridHeader" colspan="3" style="height: 20px; width: 100%;"><strong>Egresos</strong> </td>
                        </tr>
                        <tr>
                            <td style="text-align: left; width: 20%;"></td>
                            <td style="text-align: left; width: 15%;">Solicitante </td>
                            <td style="text-align: left; width: 15%;">Cónyuge </td>
                            <td style="text-align: left; width: 20%;"></td>
                            <td style="text-align: left; width: 15%;">Solicitante </td>
                            <td style="text-align: left; width: 15%;">Cónyuge </td>
                        </tr>
                        <tr>
                            <td style="text-align: left;">Sueldo </td>
                            <td style="text-align: left;">
                                <asp:TextBox ID="cod_per" runat="server" Visible="false"></asp:TextBox>
                                <asp:TextBox ID="cod_cony" runat="server" Visible="false"></asp:TextBox>
                                <asp:TextBox ID="txtsueldo_soli" runat="server" CssClass="numeric required" onkeyup="TotalizarIngresosSoli(this)"
                                    TabIndex="1" required="required"></asp:TextBox><asp:FilteredTextBoxExtender ID="fte80" runat="server" Enabled="True" FilterType="Numbers, Custom"
                                        TargetControlID="txtsueldo_soli" ValidChars="." />
                            </td>
                            <td style="text-align: left;">
                                <asp:TextBox ID="txtsueldo_cony" runat="server" CssClass="textbox numeric" onkeyup="TotalizarIngresosCony(this)" TabIndex="2"></asp:TextBox><asp:FilteredTextBoxExtender ID="fte81" runat="server" Enabled="True" FilterType="Numbers, Custom"
                                    TargetControlID="txtsueldo_cony" ValidChars="." />
                            </td>
                            <td style="text-align: left;">Cuota Hipoteca </td>
                            <td style="text-align: left;">
                                <asp:TextBox ID="txthipoteca_soli" runat="server" CssClass="textbox numeric" onkeyup="TotalizarEgresosSoli(this)" TabIndex="3"></asp:TextBox><asp:FilteredTextBoxExtender ID="fte82" runat="server" Enabled="True" FilterType="Numbers, Custom"
                                    TargetControlID="txthipoteca_soli" ValidChars="." />
                            </td>
                            <td style="text-align: left;">
                                <asp:TextBox ID="txthipoteca_cony" runat="server" CssClass="textbox numeric" onkeyup="TotalizarEgresosCony(this)" TabIndex="4"></asp:TextBox><asp:FilteredTextBoxExtender ID="fte83" runat="server" Enabled="True" FilterType="Numbers, Custom"
                                    TargetControlID="txthipoteca_cony" ValidChars="." />
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left;">Honorarios </td>
                            <td style="text-align: left;">
                                <asp:TextBox ID="txthonorario_soli" runat="server" CssClass="textbox numeric" onkeyup="TotalizarIngresosSoli(this)"
                                    TabIndex="5"></asp:TextBox><asp:FilteredTextBoxExtender ID="fte84" runat="server" Enabled="True" FilterType="Numbers, Custom"
                                        TargetControlID="txthonorario_soli" ValidChars="." />
                            </td>
                            <td style="text-align: left;">
                                <asp:TextBox ID="txthonorario_cony" runat="server" CssClass="textbox numeric" onkeyup="TotalizarIngresosCony(this)" TabIndex="6"></asp:TextBox><asp:FilteredTextBoxExtender ID="fte85" runat="server" Enabled="True" FilterType="Numbers, Custom"
                                    TargetControlID="txthonorario_cony" ValidChars="." />
                            </td>
                            <td style="text-align: left;">Cuota Tarjeta de Crédito </td>
                            <td style="text-align: left;">
                                <asp:TextBox ID="txttarjeta_soli" runat="server" CssClass="textbox numeric" onkeyup="TotalizarEgresosSoli(this)" TabIndex="7"></asp:TextBox><asp:FilteredTextBoxExtender ID="fte86" runat="server" Enabled="True" FilterType="Numbers, Custom"
                                    TargetControlID="txttarjeta_soli" ValidChars="." />
                            </td>
                            <td style="text-align: left;">
                                <asp:TextBox ID="txttarjeta_cony" runat="server" CssClass="textbox numeric" onkeyup="TotalizarEgresosCony(this)" TabIndex="8"></asp:TextBox><asp:FilteredTextBoxExtender ID="fte87" runat="server" Enabled="True" FilterType="Numbers, Custom"
                                    TargetControlID="txttarjeta_cony" ValidChars="." />
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left;">Arrendamientos </td>
                            <td style="text-align: left;">
                                <asp:TextBox ID="txtarrenda_soli" runat="server" CssClass="textbox numeric" onkeyup="TotalizarIngresosSoli(this)"
                                    TabIndex="9"></asp:TextBox><asp:FilteredTextBoxExtender ID="fte88" runat="server" Enabled="True" FilterType="Numbers, Custom"
                                        TargetControlID="txtarrenda_soli" ValidChars="." />
                            </td>
                            <td style="text-align: left;">
                                <asp:TextBox ID="txtarrenda_cony" runat="server" CssClass="textbox numeric" onkeyup="TotalizarIngresosCony(this)" TabIndex="10"></asp:TextBox><asp:FilteredTextBoxExtender ID="fte89" runat="server" Enabled="True" FilterType="Numbers, Custom"
                                    TargetControlID="txtarrenda_cony" ValidChars="." />
                            </td>
                            <td style="text-align: left;">Cuota Otros Préstamos </td>
                            <td style="text-align: left;">
                                <asp:TextBox ID="txtotrosPres_soli" runat="server" CssClass="textbox numeric" onkeyup="TotalizarEgresosSoli(this)" TabIndex="11"></asp:TextBox><asp:FilteredTextBoxExtender ID="fte90" runat="server" Enabled="True" FilterType="Numbers, Custom"
                                    TargetControlID="txtotrosPres_soli" ValidChars="." />
                            </td>
                            <td style="text-align: left;">
                                <asp:TextBox ID="txtotrosPres_cony" runat="server" CssClass="textbox numeric" onkeyup="TotalizarEgresosCony(this)" TabIndex="12"></asp:TextBox><asp:FilteredTextBoxExtender ID="fte91" runat="server" Enabled="True" FilterType="Numbers, Custom"
                                    TargetControlID="txtotrosPres_cony" ValidChars="." />
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left;">Otros Ingresos </td>
                            <td style="text-align: left;">
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                        <asp:TextBox ID="txtotrosIng_soli" runat="server" CssClass="textbox numeric" onkeyup="TotalizarIngresosSoli(this)"
                                            TabIndex="13"></asp:TextBox><asp:FilteredTextBoxExtender ID="fte92" runat="server" Enabled="True" FilterType="Numbers, Custom"
                                                TargetControlID="txtotrosIng_soli" ValidChars="." />
                                        <asp:PopupControlExtender ID="PopupControlExtender2" runat="server"
                                            Enabled="True" ExtenderControlID="" TargetControlID="txtotrosIng_soli"
                                            PopupControlID="panelConceptoOtrosSoli" OffsetY="22">
                                        </asp:PopupControlExtender>
                                        <asp:Panel ID="panelConceptoOtrosSoli" runat="server" Height="70px" Width="250px"
                                            BorderStyle="Solid" BorderWidth="2px" Direction="LeftToRight"
                                            ScrollBars="Auto" BackColor="#CCCCCC">
                                            <table>
                                                <tr>
                                                    <td style="text-align: left;">Concepto otros</td>
                                                    <td style="text-align: left;">
                                                        <asp:TextBox ID="txtConceptoOtros_soli" runat="server" CssClass="textbox" TextMode="MultiLine" Rows="3"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                            <td style="text-align: left;">
                                <asp:UpdatePanel ID="upConceptoOtrosCony" runat="server">
                                    <ContentTemplate>
                                        <asp:TextBox ID="txtotrosIng_cony" runat="server" CssClass="textbox numeric" onkeyup="TotalizarIngresosCony(this)" TabIndex="14"></asp:TextBox><asp:FilteredTextBoxExtender ID="fte93" runat="server" Enabled="True" FilterType="Numbers, Custom"
                                            TargetControlID="txtotrosIng_cony" ValidChars="." />
                                        <asp:PopupControlExtender ID="PopupControlExtender1" runat="server"
                                            Enabled="True" ExtenderControlID="" TargetControlID="txtotrosIng_cony"
                                            PopupControlID="panelConceptoOtrosCony" OffsetY="22">
                                        </asp:PopupControlExtender>
                                        <asp:Panel ID="panelConceptoOtrosCony" runat="server" Height="70px" Width="250px"
                                            BorderStyle="Solid" BorderWidth="2px" Direction="LeftToRight"
                                            ScrollBars="Auto" BackColor="#CCCCCC">
                                            <table>
                                                <tr>
                                                    <td style="text-align: left;">Concepto otros</td>
                                                    <td style="text-align: left;">
                                                        <asp:TextBox ID="txtConceptoOtros_cony" runat="server" CssClass="textbox" TextMode="MultiLine" Rows="3"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                            <td style="text-align: left;">Gastos Familiares </td>
                            <td style="text-align: left;">
                                <asp:TextBox ID="txtgastosFam_soli" runat="server" CssClass="textbox numeric" onkeyup="TotalizarEgresosSoli(this)" TabIndex="15"></asp:TextBox><asp:FilteredTextBoxExtender ID="fte94" runat="server" Enabled="True" FilterType="Numbers, Custom"
                                    TargetControlID="txtgastosFam_soli" ValidChars="." />
                            </td>
                            <td style="text-align: left;">
                                <asp:TextBox ID="txtgastosFam_cony" runat="server" CssClass="textbox numeric" onkeyup="TotalizarEgresosCony(this)" TabIndex="16"></asp:TextBox><asp:FilteredTextBoxExtender ID="fte95" runat="server" Enabled="True" FilterType="Numbers, Custom"
                                    TargetControlID="txtgastosFam_cony" ValidChars="." />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4"></td>
                        </tr>
                        <tr>
                            <td style="text-align: left;"></td>
                            <td style="text-align: left;"></td>
                            <td style="text-align: left;"></td>
                            <td style="text-align: left;">Descuentos por Nomina </td>
                            <td style="text-align: left;">
                                <asp:TextBox ID="txtnomina_soli" runat="server" CssClass="textbox numeric" onkeyup="TotalizarEgresosSoli(this)" TabIndex="17"></asp:TextBox><asp:FilteredTextBoxExtender ID="fte96" runat="server" Enabled="True" FilterType="Numbers, Custom"
                                    TargetControlID="txtnomina_soli" ValidChars="." />
                            </td>
                            <td style="text-align: left;">
                                <asp:TextBox ID="txtnomina_cony" runat="server" CssClass="textbox numeric" onkeyup="TotalizarEgresosCony(this)" TabIndex="18"></asp:TextBox><asp:FilteredTextBoxExtender ID="fte97" runat="server" Enabled="True" FilterType="Numbers, Custom"
                                    TargetControlID="txtnomina_cony" ValidChars="." />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">
                                <hr style="width: 100%" />
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left;">Total Ingresos </td>
                            <td style="text-align: left;">
                                <asp:TextBox ID="txttotalING_soli" runat="server" CssClass="textbox numeric" ReadOnly="true"></asp:TextBox></td>
                            <td style="text-align: left;">
                                <asp:TextBox ID="txttotalING_cony" runat="server" CssClass="textbox numeric" ReadOnly="true"></asp:TextBox></td>
                            <td style="text-align: left;">Total Egresos </td>
                            <td style="text-align: left;">
                                <asp:TextBox ID="txttotalEGR_soli" runat="server" CssClass="textbox numeric" ReadOnly="true"></asp:TextBox></td>
                            <td style="text-align: left;">
                                <asp:TextBox ID="txttotalEGR_cony" runat="server" CssClass="textbox numeric" ReadOnly="true"></asp:TextBox></td>
                            <asp:HiddenField ID="hdtotalING_soli" runat="server" ClientIDMode="Static" />
                            <asp:HiddenField ID="hdtotalING_cony" runat="server" ClientIDMode="Static" />
                            <asp:HiddenField ID="hdtotalEGR_soli" runat="server" ClientIDMode="Static" />
                            <asp:HiddenField ID="hdtotalEGR_cony" runat="server" ClientIDMode="Static" />
                        </tr>
                        <tr>
                            <td colspan="6">
                                <hr style="width: 100%" />
                            </td>
                        </tr>
                    </table>

                    <table style="width: 100%;">
                        <tr>
                            <td style="text-align: left; width: 20%;"></td>
                            <td style="text-align: left; width: 15%;">Solicitante </td>
                            <td style="text-align: left; width: 15%;">Cónyuge </td>
                            <td style="text-align: left; width: 50%;"></td>
                        </tr>
                        <tr>
                            <td style="text-align: left;">Total Activos </td>
                            <td style="text-align: left;">
                                <asp:TextBox ID="txtactivos_soli" runat="server" CssClass="textbox numeric" TabIndex="19"></asp:TextBox>
                                <asp:FilteredTextBoxExtender ID="fte6" runat="server" Enabled="True" FilterType="Numbers, Custom"
                                    TargetControlID="txtactivos_soli" ValidChars="." />
                            </td>
                            <td style="text-align: left;">
                                <asp:TextBox ID="txtactivos_conyuge" runat="server" CssClass="textbox numeric" TabIndex="20"></asp:TextBox>
                                <asp:FilteredTextBoxExtender ID="fte7" runat="server" Enabled="True" FilterType="Numbers, Custom"
                                    TargetControlID="txtactivos_conyuge" ValidChars="." />
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left;">Total Pasivos </td>
                            <td style="text-align: left;">
                                <asp:TextBox ID="txtpasivos_soli" runat="server" CssClass="textbox numeric" TabIndex="21"></asp:TextBox>
                                <asp:FilteredTextBoxExtender ID="fte8" runat="server" Enabled="True" FilterType="Numbers, Custom"
                                    TargetControlID="txtpasivos_soli" ValidChars="." />
                            </td>
                            <td style="text-align: left;">
                                <asp:TextBox ID="txtpasivos_conyuge" runat="server" CssClass="textbox numeric" TabIndex="22"></asp:TextBox>
                                <asp:FilteredTextBoxExtender ID="fte9" runat="server" Enabled="True" FilterType="Numbers, Custom"
                                    TargetControlID="txtpasivos_conyuge" ValidChars="." />
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left;">Total Patrimonio </td>
                            <td style="text-align: left;">
                                <asp:TextBox ID="txtpatrimonio_soli" runat="server" CssClass="textbox numeric" TabIndex="23"></asp:TextBox>
                                <asp:FilteredTextBoxExtender ID="fte12" runat="server" Enabled="True" FilterType="Numbers, Custom"
                                    TargetControlID="txtpatrimonio_soli" ValidChars="." />
                            </td>
                            <td style="text-align: left;">
                                <asp:TextBox ID="txtpatrimonio_conyuge" runat="server" CssClass="textbox numeric" TabIndex="24"></asp:TextBox>
                                <asp:FilteredTextBoxExtender ID="fte13" runat="server" Enabled="True" FilterType="Numbers, Custom"
                                    TargetControlID="txtpatrimonio_conyuge" ValidChars="." />
                            </td>
                        </tr>
                    </table>
                    <table style="text-align: left; width: 100%; padding-top: 15px; padding-bottom: 10px;">
                        <tr style="text-align: center;">
                            <td colspan="8" style="color: #FFFFFF; background-color: #5295fa; height: 30px; width: 100%;">
                                <strong>Información financiera</strong>
                            </td>
                        </tr>
                    </table>
                    <asp:Button ID="btnAgregarFila" runat="server" CssClass="btn8" TabIndex="25" OnClick="btnAgregarFila_Click" UseSubmitBehavior="false"
                        Text="+ Adicionar Detalle" />
                    <asp:GridView ID="gvCuentasBancarias"
                        runat="server" AllowPaging="True" AutoGenerateColumns="false" BackColor="White"
                        BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="0" DataKeyNames="idcuentabancaria"
                        ForeColor="Black" GridLines="Both" OnRowDataBound="gvCuentasBancarias_RowDataBound"
                        OnRowDeleting="gvCuentasBancarias_RowDeleting" PageSize="10" ShowFooter="True"
                        Style="font-size: xx-small; padding-top: 10px" Width="80%">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:CommandField ButtonType="Image" EditImageUrl="../../../Images/gr_edit.jpg" ShowEditButton="true"
                                Visible="false" />
                            <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/gr_elim.jpg" ShowDeleteButton="True" />
                            <asp:TemplateField HeaderText="CodigoCuenta" ItemStyle-HorizontalAlign="Center" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblidcuentabancaria" runat="server" Text='<%# Bind("idcuentabancaria") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="120px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Tipo de Cuenta" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lbltipoProducto" runat="server" Text='<%# Bind("tipo_cuenta") %>'
                                        Visible="false"></asp:Label><cc1:DropDownListGrid ID="ddltipoProducto" runat="server"
                                            AppendDataBoundItems="True" CommandArgument="<%#Container.DataItemIndex %>" CssClass="textbox"
                                            Style="text-align: left" Width="120px">
                                        </cc1:DropDownListGrid>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" Width="120px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Número de Cuenta" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtnum_Producto" runat="server" CssClass="textbox" Style="font-size: xx-small; text-align: left"
                                        Text='<%# Bind("numero_cuenta") %>' Width="160px"></asp:TextBox>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" Width="165px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Entidad Financiera" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblentidad" runat="server" Text='<%# Bind("cod_banco") %>' Visible="false"></asp:Label><cc1:DropDownListGrid
                                        ID="ddlentidad" runat="server" AppendDataBoundItems="True" CommandArgument="<%#Container.DataItemIndex %>"
                                        CssClass="textbox" Style="text-align: left" Width="185px">
                                    </cc1:DropDownListGrid>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" Width="190px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Sucursal" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtsucursal" runat="server" CssClass="textbox" Style="font-size: xx-small; text-align: left"
                                        Text='<%# Bind("sucursal") %>' Width="160px"></asp:TextBox>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" Width="165px"/>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="F. Aprobación" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <uc1:fecha ID="txtfecha" runat="server" CssClass="textbox" Enabled="True" Habilitado="True"
                                        style="font-size: xx-small; text-align: left" Text='<%# Eval("fecha_apertura", "{0:" + FormatoFecha() + "}") %>'
                                        TipoLetra="XX-Small" Width_="80" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="120px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-CssClass="gridIco" HeaderText="Principal" ItemStyle-CssClass="gridIco">
                                <ItemTemplate>
                                    <asp:UpdatePanel ID="updcheck1" runat="server">
                                        <ContentTemplate>
                                            <cc1:CheckBoxGrid ID="cbSeleccionar" runat="server" AutoPostBack="true" Checked='<%#Convert.ToBoolean(Eval("principal"))%>'
                                                CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' OnCheckedChanged="cbSeleccionar_CheckedChanged" />
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="cbSeleccionar" EventName="CheckedChanged" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </ItemTemplate>
                                <HeaderStyle CssClass="gridIco"></HeaderStyle>
                                <ItemStyle CssClass="gridIco" HorizontalAlign="Center"></ItemStyle>
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle CssClass="gridHeader" />
                        <HeaderStyle CssClass="gridHeader" />
                        <RowStyle CssClass="gridItem" />
                        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                        <SortedAscendingCellStyle BackColor="#FBFBF2" />
                        <SortedAscendingHeaderStyle BackColor="#848384" />
                        <SortedDescendingCellStyle BackColor="#EAEAD3" />
                        <SortedDescendingHeaderStyle BackColor="#575357" />
                    </asp:GridView>
                    <table style="text-align: left; width: 100%; padding-top: 15px; padding-bottom: 10px;">
                        <tr style="text-align: center;">
                            <td colspan="8" style="color: #FFFFFF; background-color: #5295fa; height: 30px; width: 100%;">
                                <strong>Información Transacciones Y Productos En El Exterior</strong>
                            </td>
                        </tr>
                    </table>
                    <asp:CheckBox ID="chkMonedaExtranjera" runat="server" AutoPostBack="true" TabIndex="26"
                        OnCheckedChanged="chkMonedaExtranjera_CheckedChanged" Text="&lt;strong&gt;¿Maneja moneda extranjera?&lt;/strong&gt;" /><br />
                    <br />
                    <asp:Panel ID="panelMonedaExtranjera" runat="server" Visible="false">
                        <asp:Button ID="btnAgregarFilaM" runat="server" CssClass="btn8" OnClick="btnAgregarFilaM_Click" UseSubmitBehavior="false"
                            Text="+ Adicionar Detalle" /><br />
                        <br />
                        <asp:GridView ID="gvMonedaExtranjera" HorizontalAlign="Center" DataKeyNames="cod_moneda_ext"
                            runat="server" AllowPaging="True" AutoGenerateColumns="False" BackColor="White"
                            BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="0"
                            OnRowDeleting="gvMonedaExtranjera_RowDeleting" PageSize="10" ShowFooter="True"
                            Style="font-size: xx-small" Width="100%">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:ImageButton runat="server" CommandName="Delete" ImageUrl="~/Images/gr_elim.jpg" ToolTip="Delete" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' />
                                    </ItemTemplate>
                                    <ItemStyle Width="16px" />
                                </asp:TemplateField>
                                <%--<asp:TemplateField>
													<ItemTemplate>
														<asp:ImageButton runat="server" CommandName="Edit" ImageUrl="~/Images/gr_edit.jpg" ToolTip="Edit" />
													</ItemTemplate>
													<ItemStyle Width="16px" />
												</asp:TemplateField>    --%>
                                <asp:TemplateField HeaderText="CodMoneda" ItemStyle-HorizontalAlign="Center" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCodMoneda" runat="server" Text='<%# Bind("cod_moneda_ext") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="120px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Número de Cuenta" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtNumCuentaExt" runat="server" CssClass="textbox" Style="font-size: xx-small; text-align: left" MaxLength="35"
                                            Text='<%# Bind("num_cuenta_ext") %>' Width="140px"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="120px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Nombre del Banco" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtNomBancoExt" runat="server" CssClass="textbox" Style="font-size: xx-small; text-align: left"
                                            Text='<%# Bind("banco_ext") %>' Width="140px"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="120px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Pais" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtNomPais" runat="server" CssClass="textbox" Style="font-size: xx-small; text-align: left"
                                            Text='<%# Bind("pais") %>' Width="140px"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="120px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Ciudad" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtNomCiudad" runat="server" CssClass="textbox" Style="font-size: xx-small; text-align: left"
                                            Text='<%# Bind("ciudad") %>' Width="140px"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="120px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Moneda" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtNomMoneda" runat="server" CssClass="textbox" Style="font-size: xx-small; text-align: left"
                                            Text='<%# Bind("moneda") %>' Width="140px"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="120px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Operación/Transacción que realiza" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtOperacion" runat="server" CssClass="textbox" Style="font-size: xx-small; text-align: left"
                                            Text='<%# Bind("desc_operacion") %>' Width="140px"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="120px" />
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle CssClass="gridHeader" />
                            <HeaderStyle CssClass="gridHeader" />
                            <RowStyle CssClass="gridItem" />
                            <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                            <SortedAscendingCellStyle BackColor="#FBFBF2" />
                            <SortedAscendingHeaderStyle BackColor="#848384" />
                            <SortedDescendingCellStyle BackColor="#EAEAD3" />
                            <SortedDescendingHeaderStyle BackColor="#575357" />
                        </asp:GridView>
                    </asp:Panel>

                    <asp:CheckBox ID="chkTransaccionExterior" runat="server" OnCheckedChanged="chkTransaccionExterior_CheckedChanged" AutoPostBack="true" TabIndex="27"
                        Text="&lt;strong&gt;¿Posee productos financieros en el exterior?&lt;/strong&gt;" /><br />
                    <br />
                    <asp:Panel ID="pProductosExt" runat="server" Visible="false">
                        <asp:Button ID="btnProductoExt" runat="server" CssClass="btn8" OnClick="btnProductoExt_Click" UseSubmitBehavior="false"
                            Text="+ Adicionar Detalle" /><br />
                        <%--OnClientClick="btnProductoExt_Click"--%>
                        <br />
                        <asp:GridView ID="gvProductosExterior" HorizontalAlign="Center" DataKeyNames="cod_moneda_ext"
                            runat="server" AllowPaging="True" AutoGenerateColumns="False" BackColor="White"
                            BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="0"
                            OnRowDeleting="gvProductosExterior_RowDeleting" PageSize="10" ShowFooter="True"
                            Style="font-size: xx-small" Width="100%">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:ImageButton runat="server" CommandName="Delete" ImageUrl="~/Images/gr_elim.jpg" ToolTip="Delete" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' />
                                    </ItemTemplate>
                                    <ItemStyle Width="16px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cod Producto" ItemStyle-HorizontalAlign="Center" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCodProducto" runat="server" Text='<%# Bind("cod_moneda_ext") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="120px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Tipo de Producto" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtTipoProducto" runat="server" CssClass="textbox" Style="font-size: xx-small; text-align: left"
                                            Text='<%# Bind("tipo_producto") %>' Width="140px"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="120px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="No. Producto" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtNumProducto" runat="server" CssClass="textbox" Style="font-size: xx-small; text-align: left" MaxLength="35"
                                            Text='<%# Bind("num_cuenta_ext") %>' Width="140px"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="120px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Pais" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtNomPais" runat="server" CssClass="textbox" Style="font-size: xx-small; text-align: left"
                                            Text='<%# Bind("pais") %>' Width="140px"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="120px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Ciudad" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtNomCiudad" runat="server" CssClass="textbox" Style="font-size: xx-small; text-align: left"
                                            Text='<%# Bind("ciudad") %>' Width="140px"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="120px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Moneda" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtNomMoneda" runat="server" CssClass="textbox" Style="font-size: xx-small; text-align: left"
                                            Text='<%# Bind("moneda") %>' Width="140px"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="120px" />
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle CssClass="gridHeader" />
                            <HeaderStyle CssClass="gridHeader" />
                            <RowStyle CssClass="gridItem" />
                            <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                            <SortedAscendingCellStyle BackColor="#FBFBF2" />
                            <SortedAscendingHeaderStyle BackColor="#848384" />
                            <SortedDescendingCellStyle BackColor="#EAEAD3" />
                            <SortedDescendingHeaderStyle BackColor="#575357" />
                        </asp:GridView>
                    </asp:Panel>
                    <table style="text-align: left; width: 100%; padding-top: 15px; padding-bottom: 10px;">
                        <tr style="text-align: center;">
                            <td colspan="8" style="color: #FFFFFF; background-color: #5295fa; height: 30px; width: 100%;">
                                <strong>Información Activos/Bienes</strong>
                            </td>
                        </tr>
                    </table>
                    <asp:Label ID="lblInfoBienesActivos" Text="" runat="server" Style="color: red" />
                    <asp:Button ID="btnBienesActivos" runat="server" CssClass="btn8" TabIndex="28" OnClick="InicializarModal" OnClientClick="LinkButton1.click();" UseSubmitBehavior="false" Text="+ Adicionar Detalle" />
                    <asp:GridView ID="gvBienesActivos" runat="server"
                        AutoGenerateColumns="False" AllowPaging="False" PageSize="20" BackColor="White"
                        BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4"
                        ForeColor="Black" GridLines="Vertical" DataKeyNames="IdActivo"
                        OnRowCommand="gvBienesActivos_OnRowCommand" OnRowEditing="gvBienesActivos_RowEditing" Width="90%">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:ImageButton runat="server" CommandName="Delete" ImageUrl="~/Images/gr_elim.jpg" ToolTip="Delete" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' />
                                </ItemTemplate>
                                <ItemStyle Width="16px" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:ImageButton runat="server" CommandName="Edit" ImageUrl="~/Images/gr_edit.jpg" ToolTip="Edit" />
                                </ItemTemplate>
                                <ItemStyle Width="16px" />
                            </asp:TemplateField>
                            <%--<asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/gr_elim.jpg" ShowDeleteButton="True">
											<ItemStyle Width="16px" />
										</asp:CommandField>
										<asp:CommandField ButtonType="Image" EditImageUrl="~/Images/gr_edit.jpg" ShowEditButton="True">
											<ItemStyle Width="16px" />
										</asp:CommandField>--%>
                            <asp:BoundField DataField="IdActivo" NullDisplayText=" " HeaderText="Código" />
                            <asp:BoundField DataField="descripcion_activo" NullDisplayText=" " HeaderText="Tipo de Activo" />
                            <asp:BoundField DataField="Descripcion" NullDisplayText=" " HeaderText="Descripción" />
                            <asp:BoundField DataField="Fecha_adquisicionactivo" NullDisplayText=" " DataFormatString="{0:d}" HeaderText="Fecha de Adquisición" />
                            <asp:BoundField DataField="valor_comercial" NullDisplayText=" " HeaderText="Valor Comercial" DataFormatString="{0:N0}" />
                            <asp:BoundField DataField="estado_descripcion" NullDisplayText=" " HeaderText="Estado" />
                        </Columns>
                        <FooterStyle BackColor="#CCCC99" />
                        <HeaderStyle CssClass="gridHeader" />
                        <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                        <RowStyle CssClass="gridItem" />
                        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                        <SortedAscendingCellStyle BackColor="#FBFBF2" />
                        <SortedAscendingHeaderStyle BackColor="#848384" />
                        <SortedDescendingCellStyle BackColor="#EAEAD3" />
                        <SortedDescendingHeaderStyle BackColor="#575357" />
                    </asp:GridView>
                </div>
                         </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnAgregarFila" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="chkMonedaExtranjera" EventName="CheckedChanged" />
                <asp:AsyncPostBackTrigger ControlID="btnAgregarFilaM" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="chkTransaccionExterior" EventName="CheckedChanged" />
                <asp:AsyncPostBackTrigger ControlID="btnProductoExt" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
                <div style="visibility: hidden">
                    <asp:LinkButton ID="LinkButton1" runat="server" Text="Click here to change the paragraph style" />
                </div>
                <asp:ModalPopupExtender ID="mpeNuevoActividad" runat="server" PopupControlID="panelMostrarModal"
                    TargetControlID="LinkButton1" BackgroundCssClass="backgroundColor" CancelControlID="btnCancelarModal">
                </asp:ModalPopupExtender>
                <asp:Panel ID="panelMostrarModal" runat="server" BackColor="White" Style="overflow-y: scroll; text-align: left; max-height: 500px; padding: 20px; border: medium groove #0000FF; background-color: #FFFFFF;"
                    Width="700px">
                    <asp:UpdatePanel ID="upReclasificacion" runat="server">
                        <ContentTemplate>
                            <center><strong>ACTIVOS FIJOS</strong></center>
                            <table style="width: 100%">
                                <tr style="text-align: right">
                                    <td></td>
                                    <td style="width: 120px">
                                        <asp:ImageButton runat="server" ID="btnCancelarModal" ImageUrl="~/Images/btnCancelar.jpg" ToolTip="Cancelar" OnClick="btnCancelarModal_click" />
                                    </td>
                                    <td style="width: 120px">
                                        <asp:ImageButton runat="server" ID="btnGuardarModal" ImageUrl="~/Images/btnGuardar.jpg" ToolTip="Guardar" OnClick="btnGuardarModal_click" />
                                    </td>
                                </tr>
                            </table>
                            <table style="width: 100%">
                                <tr>
                                    <td colspan="3">
                                        <asp:Label ID="lblErrorModal" runat="server" Style="text-align: center" Width="100%" ForeColor="Red"></asp:Label><br />
                                        <asp:Label ID="lblTipoProceso" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: left;" colspan="3">&nbsp;
                                            <strong>Datos del Activo:  </strong>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 25%;">Identificación<br />
                                        <asp:TextBox ID="txtModalIdentificacion" Enabled="false" runat="server" CssClass="textbox"
                                            MaxLength="128" Width="92%" />
                                    </td>
                                    <td style="width: 25%;">Tipo Identificación<br />
                                        <asp:DropDownList ID="ddlModalIdentificacion" Enabled="false" runat="server"
                                            CssClass="textbox" Width="95%">
                                        </asp:DropDownList>
                                    </td>
                                    <td style="width: 50%;">Nombres y Apellidos<br />
                                        <asp:TextBox ID="txtModalNombres" Enabled="false" runat="server" CssClass="textbox" Style="text-transform: uppercase"
                                            MaxLength="128" Width="95%" /></td>
                                </tr>
                            </table>
                            <table style="width: 100%" cellpadding="5" cellspacing="0">
                                <tr>
                                    <td colspan="2" style="width: 148px;">Tipo de Activo<br />
                                        <asp:DropDownList ID="ddlModalTipoActivo" runat="server"
                                            CssClass="textbox" Width="199px" AutoPostBack="true" OnSelectedIndexChanged="ddlModalTipoActivo_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                    <td style="text-align: left">Estado<br />
                                        <asp:DropDownList ID="ddlEstadoModal" runat="server" CssClass="textbox" Width="95%">
                                            <asp:ListItem Value="0" Text="Inactivo" />
                                            <asp:ListItem Value="1" Text="Activo" />
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: left">Descripción</td>
                                    <td colspan="2" style="text-align: left">
                                        <asp:TextBox ID="txtModalDescripcion" runat="server" CssClass="textbox" Style="text-transform: uppercase"
                                            MaxLength="128" Width="350px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 30%">
                                        <asp:Label ID="LabelFecha_gara" runat="server" Text="Fecha Adquisición"></asp:Label>
                                        <uc1:fecha ID="txtModalFechaIni" runat="server" />
                                    </td>
                                    <td style="width: 35%; text-align: left">Valor Comercial:<br />
                                        <asp:TextBox ID="txtModalValorComercial" runat="server" onkeypress="return isNumber(event)" CssClass="textbox" MaxLength="128" Width="196px" />
                                    </td>
                                    <td style="width: 35%; text-align: left">Valor Comprometido:<br />
                                        <asp:TextBox ID="txtModalValorComprometido" runat="server" onkeypress="return isNumber(event)" CssClass="textbox" MaxLength="128" Width="196px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3" style="text-align: left; width: 684px;">
                                        <hr style="width: 99%" />
                                    </td>
                                </tr>
                            </table>
                            <asp:Panel ID="panelTipoActivoInmueble" Visible="false" runat="server">
                                <table>
                                    <tr>
                                        <td class="tdD" style="height: 36px; width: 148px;">Dirección
                                                <asp:TextBox ID="txtModalDireccion" runat="server" CssClass="textbox"
                                                    MaxLength="128" Width="196px" />
                                        </td>
                                        <td class="tdD" style="height: 36px; width: 148px;">Localización
                                                <asp:TextBox ID="txtModalLocalizacion" runat="server" CssClass="textbox"
                                                    MaxLength="128" Width="196px" />
                                        </td>
                                        <td class="tdD" style="width: 148px;">VIS<br />
                                            <asp:DropDownList ID="ddlModalVIS" Width="180px" AutoPostBack="true" class="textbox" runat="server" OnSelectedIndexChanged="ddlModalVIS_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Sin VIS
                                                </asp:ListItem>
                                                <asp:ListItem Value="1">Con VIS
                                                </asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 148px;">Nro. Matricula
                                                <asp:TextBox ID="txtModalNoMatricula" runat="server" CssClass="textbox"
                                                    MaxLength="128" Width="196px" />
                                        </td>
                                        <td style="width: 148px;">Escritura
                                                <asp:TextBox ID="txtModalEscritura" runat="server" CssClass="textbox"
                                                    MaxLength="128" Width="196px" />
                                        </td>
                                        <td style="width: 148px;">Notaria
                                                <asp:TextBox ID="txtModalNotaria" runat="server" CssClass="textbox"
                                                    MaxLength="128" Width="196px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 148px;">Entidad Redescuento<br />
                                            <asp:DropDownList ID="ddlModalEntidadReDesc" runat="server"
                                                CssClass="textbox" Width="199px">
                                                <asp:ListItem Value="0">Ninguna</asp:ListItem>
                                                <asp:ListItem Value="1">FINDETER</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 148px;">Margen Redescuento<br />
                                            <asp:TextBox ID="txtModalmargenReDesc" runat="server" CssClass="textbox"
                                                MaxLength="128" Width="196px" />
                                            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" Enabled="True" FilterType="Numbers, Custom"
                                                TargetControlID="txtModalmargenReDesc" ValidChars=".," />
                                        </td>
                                        <td style="width: 148px;">Tipo Vivienda<br />
                                            <asp:DropDownList ID="ddlModalTipoVivienda" runat="server"
                                                CssClass="textbox" Width="199px">
                                                <asp:ListItem Value="1">Nueva
                                                </asp:ListItem>
                                                <asp:ListItem Value="2">Usada
                                                </asp:ListItem>
                                                <asp:ListItem Value="3">Mejoramiento
                                                </asp:ListItem>
                                                <asp:ListItem Value="4">Lote con servicios
                                                </asp:ListItem>
                                                <asp:ListItem Value="5">Construccion en sitio propio
                                                </asp:ListItem>
                                            </asp:DropDownList>
                                        </td>

                                    </tr>
                                    <tr>
                                        <td style="width: 148px;">Desembolso
                                                <asp:DropDownList ID="ddlModalDesembolso" runat="server"
                                                    CssClass="textbox" Width="199px">
                                                    <asp:ListItem Value="1">Desembolso Directo</asp:ListItem>
                                                    <asp:ListItem Value="2">Desembolso a Constructor</asp:ListItem>
                                                    <asp:ListItem Value="3">Subrogración</asp:ListItem>
                                                </asp:DropDownList>
                                        </td>
                                        <td style="width: 148px;">Desembolso Directo
                                                <asp:TextBox ID="txtModalDesembolsoDirecto" runat="server" CssClass="textbox"
                                                    MaxLength="128" Width="196px" />
                                        </td>
                                        <td style="width: 148px;">Rango Vivienda<br />
                                            <asp:DropDownList ID="ddlModalRangoVivienda" runat="server"
                                                CssClass="textbox" Width="199px">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 148px;">
                                            <asp:CheckBox ID="chkHipoteca" runat="server" Text="Hipoteca" Width="199px"></asp:CheckBox>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <asp:Panel ID="pnlTipoActivoMaquinaria" Visible="false" runat="server">
                                <table>
                                    <tr>
                                        <td style="width: 148px;">Marca
                                                <asp:TextBox ID="txtModalMarca" runat="server" CssClass="textbox"
                                                    MaxLength="128" Width="196px" />
                                        </td>
                                        <td style="width: 148px;">Referencia
                                                <asp:TextBox ID="txtModalReferencia" runat="server" CssClass="textbox"
                                                    MaxLength="128" Width="196px" />
                                        </td>
                                        <td style="width: 148px;">Modelo
                                                <asp:TextBox ID="txtModalModelo" runat="server" CssClass="textbox"
                                                    MaxLength="128" Width="196px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 148px;">Uso<br />
                                            <asp:DropDownList ID="ddlModalUso" Width="180px" class="textbox" runat="server">
                                                <asp:ListItem Value="1">Particular
                                                </asp:ListItem>
                                                <asp:ListItem Value="2">Publico
                                                </asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 148px;">No.Chasis
                                                <asp:TextBox ID="txtModalNoChasis" runat="server" CssClass="textbox"
                                                    MaxLength="128" Width="196px" />
                                        </td>
                                        <td style="width: 148px;">Capacidad
                                                <asp:TextBox ID="txtModalCapacidad" runat="server" CssClass="textbox"
                                                    MaxLength="128" Width="196px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 148px;">No.Serie Motor
                                                <asp:TextBox ID="txtModalNoSerieMotor" runat="server"
                                                    CssClass="textbox" Width="199px" />
                                        </td>
                                        <td style="width: 148px;">Placa
                                                <asp:TextBox ID="txtModalPlaca" runat="server" CssClass="textbox"
                                                    MaxLength="128" Width="196px" />
                                        </td>
                                        <td style="width: 148px;">Color
                                                <asp:TextBox ID="txtModalColor" runat="server" CssClass="textbox"
                                                    MaxLength="128" Width="196px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 148px;">Doc.Importación
                                                <asp:TextBox ID="txtModalDocImportacion" runat="server"
                                                    CssClass="textbox" Width="199px" />
                                        </td>
                                        <td>
                                            <asp:Label ID="Label1" runat="server" Text="Fecha Importación"></asp:Label>
                                            <uc1:fecha ID="txtModalFechaImportacion" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 148px;">
                                            <asp:CheckBox ID="chkPignorado" runat="server" Text="Pignorado" Width="199px" OnCheckedChanged="chkPignorado_CheckedChanged" />
                                        </td>
                                        <td>
                                            <asp:Label ID="lblPorcPignorado" runat="server" Text="Porcentaje" Visible="false"></asp:Label>
                                            <asp:TextBox ID="txtPorcPignorado" runat="server" CssClass="textbox" Visible="false"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:Panel>
                <div>
                    <asp:Button runat="server" ID="GuardarEconomica" Text="Guardar Inf.Económica" Style="display: none;" OnClick="btnGuardarEconomica_click" UseSubmitBehavior="false" />
                </div>
   
    </form>
</body>
<script type="text/javascript">


    function Total(textbox) {
        var str = textbox.value;
        //var str = int.value;
        var formateado = "";
        str = str.replace(/\./g, "");
        if (str > 0) {
            str = parseInt(str);
            str = str.toString();

            if (str.length > 12)
            { str = str.substring(0, 12); }

            var long = str.length;
            var cen = str.substring(long - 3, long);
            var mil = str.substring(long - 6, long - 3);
            var mill = str.substring(long - 9, long - 6);
            var milmill = str.substring(0, long - 9);

            if (long > 0 && long <= 3)
            { formateado = parseInt(cen); }
            else if (long > 3 && long <= 6)
            { formateado = parseInt(mil) + "." + cen; }
            else if (long > 6 && long <= 9)
            { formateado = parseInt(mill) + "." + mil + "." + cen; }
            else if (long > 9 && long <= 12)
            { formateado = parseInt(milmill) + "." + mill + "." + mil + "." + cen; }
            else
            { formateado = "0"; }
        }
        else { formateado = "0"; }
        return formateado;
    }

    function TotalizarIngresosSoli(textbox) {

        var txtsueldo_solijq = document.getElementById('<%= txtsueldo_soli.ClientID %>');
        var txthonorario_solijq = document.getElementById('<%= txthonorario_soli.ClientID %>');
        var txtarrenda_solijq = document.getElementById('<%= txtarrenda_soli.ClientID %>');
        var txtotrosIng_solijq = document.getElementById('<%= txtotrosIng_soli.ClientID %>');

        var txttotalING_solijq = document.getElementById('<%= txttotalING_soli.ClientID %>');

        var A = txtsueldo_solijq.value == "" || txtsueldo_solijq.value == null ? "0" : replaceAll(".", "", txtsueldo_solijq.value);
        var E = txthonorario_solijq.value == "" || txthonorario_solijq.value == null ? "0" : replaceAll(".", "", txthonorario_solijq.value);
        var I = txtarrenda_solijq.value == "" || txtarrenda_solijq.value == null ? "0" : replaceAll(".", "", txtarrenda_solijq.value);
        var O = txtotrosIng_solijq.value == "" || txtotrosIng_solijq.value == null ? "0" : replaceAll(".", "", txtotrosIng_solijq.value);

        var totalGeneral = parseFloat(A) + parseFloat(E) + parseFloat(I) + parseFloat(O);

        txttotalING_solijq.value = totalGeneral;
        var hdtotalING_soli = document.getElementById('<%= hdtotalING_soli.ClientID %>');
        hdtotalING_soli.value = totalGeneral;
        Total(textbox);
        Total(document.getElementById('<%= txttotalING_soli.ClientID %>'));

    }

    function TotalizarIngresosCony(textbox) {

        var txtsueldo_cony = document.getElementById('<%= txtsueldo_cony.ClientID %>');
        var txthonorario_cony = document.getElementById('<%= txthonorario_cony.ClientID %>');
        var txtarrenda_cony = document.getElementById('<%= txtarrenda_cony.ClientID %>');
        var txtotrosIng_cony = document.getElementById('<%= txtotrosIng_cony.ClientID %>');

        var txttotalING_cony = document.getElementById('<%= txttotalING_cony.ClientID %>');

        var A = txtsueldo_cony.value == "" || txtsueldo_cony.value == null ? "0" : replaceAll(".", "", txtsueldo_cony.value);
        var E = txthonorario_cony.value == "" || txthonorario_cony.value == null ? "0" : replaceAll(".", "", txthonorario_cony.value);
        var I = txtarrenda_cony.value == "" || txtarrenda_cony.value == null ? "0" : replaceAll(".", "", txtarrenda_cony.value);
        var O = txtotrosIng_cony.value == "" || txtotrosIng_cony.value == null ? "0" : replaceAll(".", "", txtotrosIng_cony.value);

        var totalGeneral = parseFloat(A) + parseFloat(E) + parseFloat(I) + parseFloat(O);

        txttotalING_cony.value = totalGeneral;
        var hdtotalING_cony = document.getElementById('<%= hdtotalING_cony.ClientID %>');
        hdtotalING_cony.value = totalGeneral;
        Total(textbox);
        Total(document.getElementById('<%= txttotalING_cony.ClientID %>'));
    }

    function TotalizarEgresosSoli(textbox) {

        var txthipoteca_soli = document.getElementById('<%= txthipoteca_soli.ClientID %>');
        var txttarjeta_soli = document.getElementById('<%= txttarjeta_soli.ClientID %>');
        var txtotrosPres_soli = document.getElementById('<%= txtotrosPres_soli.ClientID %>');
        var txtgastosFam_soli = document.getElementById('<%= txtgastosFam_soli.ClientID %>');
        var txtnomina_soli = document.getElementById('<%= txtnomina_soli.ClientID %>');

        var txttotalEGR_soli = document.getElementById('<%= txttotalEGR_soli.ClientID %>');

        var A = txthipoteca_soli.value == "" || txthipoteca_soli.value == null ? "0" : replaceAll(".", "", txthipoteca_soli.value);
        var E = txttarjeta_soli.value == "" || txttarjeta_soli.value == null ? "0" : replaceAll(".", "", txttarjeta_soli.value);
        var I = txtotrosPres_soli.value == "" || txtotrosPres_soli.value == null ? "0" : replaceAll(".", "", txtotrosPres_soli.value);
        var O = txtgastosFam_soli.value == "" || txtgastosFam_soli.value == null ? "0" : replaceAll(".", "", txtgastosFam_soli.value);
        var U = txtnomina_soli.value == "" || txtnomina_soli.value == null ? "0" : replaceAll(".", "", txtnomina_soli.value);

        var totalGeneral = parseFloat(A) + parseFloat(E) + parseFloat(I) + parseFloat(O) + +parseFloat(U);

        txttotalEGR_soli.value = totalGeneral;
        var hdtotalEGR_soli = document.getElementById('<%= hdtotalEGR_soli.ClientID %>');
        hdtotalEGR_soli.value = totalGeneral;
        Total(textbox);
        Total(document.getElementById('<%= txttotalEGR_soli.ClientID %>'));
    }

    function TotalizarEgresosCony(textbox) {

        var txthipoteca_cony = document.getElementById('<%= txthipoteca_cony.ClientID %>');
        var txttarjeta_cony = document.getElementById('<%= txttarjeta_cony.ClientID %>');
        var txtotrosPres_cony = document.getElementById('<%= txtotrosPres_cony.ClientID %>');
        var txtgastosFam_cony = document.getElementById('<%= txtgastosFam_cony.ClientID %>');
        var txtnomina_cony = document.getElementById('<%= txtnomina_cony.ClientID %>');

        var txttotalEGR_cony = document.getElementById('<%= txttotalEGR_cony.ClientID %>');

        var A = txthipoteca_cony.value == "" || txthipoteca_cony.value == null ? "0" : replaceAll(".", "", txthipoteca_cony.value);
        var E = txttarjeta_cony.value == "" || txttarjeta_cony.value == null ? "0" : replaceAll(".", "", txttarjeta_cony.value);
        var I = txtotrosPres_cony.value == "" || txtotrosPres_cony.value == null ? "0" : replaceAll(".", "", txtotrosPres_cony.value);
        var O = txtgastosFam_cony.value == "" || txtgastosFam_cony.value == null ? "0" : replaceAll(".", "", txtgastosFam_cony.value);
        var U = txtnomina_cony.value == "" || txtnomina_cony.value == null ? "0" : replaceAll(".", "", txtnomina_cony.value);

        var totalGeneral = parseFloat(A) + parseFloat(E) + parseFloat(I) + parseFloat(O) + +parseFloat(U);

        txttotalEGR_cony.value = totalGeneral;
        var hdtotalEGR_cony = document.getElementById('<%= hdtotalEGR_cony.ClientID %>');
        hdtotalEGR_cony.value = totalGeneral;
        Total(textbox);
        Total(document.getElementById('<%= txttotalEGR_cony.ClientID %>'));
    }

    function isNumber(evt) {
        evt = (evt) ? evt : window.event;
        var charCode = (evt.which) ? evt.which : evt.keyCode;
        if (charCode > 31 && (charCode < 48 || charCode > 57)) {
            return false;
        }
        return true;
    }

    function replaceAll(find, replace, str) {
        while (str.indexOf(find) > -1) {
            str = str.replace(find, replace);
        }
        return str;
    }
</script>
</html>
