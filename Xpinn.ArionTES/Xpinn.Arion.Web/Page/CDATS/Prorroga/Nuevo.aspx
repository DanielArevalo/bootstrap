<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" Title=".: Xpinn - CDATS Prorroga :." %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>
<%@ Register Src="../../../General/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc1" %>
<%@ Register Src="../../../General/Controles/fecha.ascx" TagName="fecha" TagPrefix="uc2" %>
<%@ Register Src="../../../General/Controles/ctlBusquedaRapida.ascx" TagName="ListadoPersonas" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>
<%@ Register Src="../../../General/Controles/decimalesGridRow.ascx" TagName="decimalesGridRow" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/ctlTasa.ascx" TagName="ctlTasa" TagPrefix="ctl" %>
<%@ Register Src="~/General/Controles/ctlTasa.ascx" TagName="ctlTasaPro" TagPrefix="ctl" %>
<%@ Register Src="~/General/Controles/ctlGiro.ascx" TagName="ctlgiro" TagPrefix="uc3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">


    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <asp:MultiView ID="mvPrincipal" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwDatos" runat="server">

            <asp:UpdatePanel ID="Panelgrilla" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Panel ID="PanelBloqueo" runat="server">
                        <table border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td style="text-align: left;" colspan="3">
                                    <asp:Label ID="lblError" runat="server" ForeColor="Red"
                                        Style="text-align: right" Visible="False"></asp:Label>
                                </td>
                                <td style="text-align: left; width: 320px">&nbsp;</td>
                            </tr>
                            <tr>
                                <td style="text-align: left; width: 140px;">Número CDAT<br />
                                    <asp:TextBox ID="txtCodigo" runat="server" CssClass="textbox" Visible="false" />
                                    <asp:TextBox ID="txtNumCDAT" runat="server" CssClass="textbox" Width="90%" />
                                </td>
                                <td style="text-align: left; width: 140px">Fecha Apertura<br />
                                    <uc2:fecha ID="txtFechaApertura" runat="server" CssClass="textbox" />
                                </td>
                                <td style="text-align: left; width: 140px">Fecha Vencimiento<br />
                                    <uc2:fecha ID="txtFechaVenci" runat="server" CssClass="textbox" />
                                </td>
                                <td style="text-align: left; width: 320px">Tipo/Linea de CDAT<br />
                                    <asp:DropDownList ID="ddlTipoLinea" runat="server" AppendDataBoundItems="True"
                                        CssClass="textbox" Width="90%" />
                                </td>
                            </tr>
                        </table>

                        <table border="0" cellpadding="0" cellspacing="0" width="740px">
                            <tr>
                                <td style="text-align: left; width: 150px" colspan="2">Valor<br />
                                    <uc1:decimales ID="txtValor" runat="server" CssClass="textbox" />
                                </td>
                                <td style="text-align: left; width: 160px">Moneda<br />
                                    <asp:DropDownList ID="ddlTipoMoneda" runat="server" CssClass="textbox" Width="90%" />
                                </td>
                                <td style="text-align: left; width: 110px">Plazo Días
                                <br />
                                    <asp:TextBox ID="txtPlazo" runat="server" CssClass="textbox" Width="60%" />
                                    <asp:FilteredTextBoxExtender ID="fte1" runat="server" Enabled="True"
                                        FilterType="Numbers, Custom" TargetControlID="txtPlazo" ValidChars="" />
                                </td>
                                <td style="text-align: left; width: 160px; margin-left: 40px;">Tipo Calendario<br />
                                    <asp:DropDownList ID="ddlTipoCalendario" runat="server" CssClass="textbox" Width="90%" />

                                </td>
                                <td style="text-align: left; width: 160px">Oficina<br />
                                    <asp:DropDownList ID="ddlOficina" runat="server" CssClass="textbox" Width="90%" AppendDataBoundItems="True" />
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left; width: 150px;">Modalidad<br />
                                    <asp:DropDownList ID="ddlModalidad" runat="server" CssClass="textbox" AutoPostBack="True"
                                        OnSelectedIndexChanged="ddlModalidad_SelectedIndexChanged" />
                                </td>
                                <td style="text-align: left; width: 150px;">Periodicidad Interes<br />
                                    <asp:DropDownList ID="ddlPeriodicidad" runat="server" AutoPostBack="True"
                                        CssClass="textbox"
                                        OnSelectedIndexChanged="ddlPeriodicidad_SelectedIndexChanged" Visible="True" />
                                    <asp:TextBox ID="txtDiasValida" runat="server" CssClass="textbox"
                                        Visible="False" />
                                    <asp:TextBox ID="Txtperiodicidad" runat="server" CssClass="textbox"
                                        Visible="False" />
                                </td>
                                <td style="text-align: left;" colspan="4">
                                    <asp:Panel ID="panelTasa" runat="server">
                                        <ctl:ctlTasa ID="ctlTasaInteres" runat="server" Width="400px" />
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>

                    <table>
                        <tr>
                            <td style="text-align: left; width: 740px" colspan="2">
                                <strong>Titulares:</strong><br />

                                <div style="overflow: scroll; width: 740px;">
                                    <asp:GridView ID="gvDetalle" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                                        PageSize="5" HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager"
                                        RowStyle-CssClass="gridItem" Style="font-size: x-small" OnRowDataBound="gvDetalle_RowDataBound"
                                        GridLines="Horizontal" DataKeyNames="cod_usuario_cdat"
                                        OnRowDeleting="gvDetalle_RowDeleting" Enabled="false">
                                        <Columns>
                                            <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/gr_elim.jpg" ShowDeleteButton="True" Visible="false" />
                                            <asp:TemplateField HeaderText="Codigo" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblcodigo" runat="server" Text='<%# Bind("cod_usuario_cdat") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Identificación">
                                                <ItemTemplate>
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <cc1:TextBoxGrid ID="txtIdentificacion" runat="server" Text='<%# Bind("identificacion") %>'
                                                                    CommandArgument='<%#Container.DataItemIndex %>' Width="90px" AutoPostBack="True"
                                                                    OnTextChanged="txtIdentificacion_TextChanged" />
                                                            </td>
                                                            <td>
                                                                <cc1:ButtonGrid ID="btnListadoPersona" CssClass="btnListado" runat="server" Text="..."
                                                                    OnClick="btnListadoPersona_Click" CommandArgument='<%#Container.DataItemIndex %>' /><uc1:ListadoPersonas
                                                                        ID="ctlListadoPersona" runat="server" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" Width="170px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Principal">
                                                <ItemTemplate>
                                                    <cc1:CheckBoxGrid ID="chkPrincipal" runat="server" AutoPostBack="true"
                                                        Checked='<%# Convert.ToBoolean(Eval("principal")) %>'
                                                        CommandArgument="<%#Container.DataItemIndex %>"
                                                        OnCheckedChanged="chkPrincipal_CheckedChanged" />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Cod. Persona">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="lblcod_persona" runat="server" Text='<%# Bind("cod_persona") %>'
                                                        CssClass="textbox" Width="80px" Enabled="false" />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Nombres">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="lblNombre" runat="server" Text='<%# Bind("nombres") %>' CssClass="textbox"
                                                        Width="160px" Enabled="false" />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Apellidos">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="lblApellidos" runat="server" Text='<%# Bind("apellidos") %>' CssClass="textbox"
                                                        Width="160px" Enabled="false" />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Ciudad">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="lblCiudad" runat="server" Text='<%# Bind("ciudad") %>' CssClass="textbox"
                                                        Width="120px" Enabled="false" />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Dirección">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="lblDireccion" runat="server" Text='<%# Bind("direccion") %>' CssClass="textbox"
                                                        Width="170px" Enabled="false" />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Teléfono">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="lbltelefono" runat="server" Text='<%# Bind("telefono") %>' CssClass="textbox"
                                                        Width="80px" Enabled="false" />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Conjunción">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblConjuncion" runat="server" Text='<%# Eval("conjuncion")  %>' Visible="false" /><cc1:DropDownListGrid
                                                        ID="ddlConjuncion" runat="server" CssClass="textbox" CommandArgument='<%#Container.DataItemIndex %>'
                                                        Width="120px" />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <HeaderStyle CssClass="gridHeader"></HeaderStyle>
                                        <PagerStyle CssClass="gridPager"></PagerStyle>
                                        <RowStyle CssClass="gridItem"></RowStyle>
                                    </asp:GridView>
                                    <asp:Button ID="btnAddRow" runat="server" CssClass="btn8" OnClick="btnAddRow_Click"
                                        OnClientClick="btnAddRow_Click" Text="+ Adicionar Titular" Visible="false" />
                                </div>
                            </td>
                        </tr>
                    </table>
                    <hr style="width: 100%" />
                    <asp:Panel ID="PanelLiquida" runat="server">
                        <table>
                            <tr>
                                <td colspan="6" style="text-align: left; background-color: #3399FF;">
                                    <strong style="color: #FFFFFF">Liquidación de Intereses</strong><br />
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left;">Interés<br />
                                    <uc1:decimales ID="txtinteres" runat="server" CssClass="textbox"
                                        Enabled="false" />
                                </td>
                                <td style="text-align: left;">Retención<br />
                                    <uc1:decimales ID="txtretencion" runat="server" CssClass="textbox"
                                        Enabled="false" />
                                </td>
                                <td style="text-align: left;">MenosGMF<br />
                                    <uc1:decimales ID="txtmenosgmf" runat="server" CssClass="textbox"
                                        Enabled="false" />
                                </td>
                                <td style="text-align: left;">Total A Pagar<br />
                                    <uc1:decimales ID="txttotalapagar" runat="server" CssClass="textbox"
                                        Enabled="false" />
                                </td>
                                <td style="text-align: left;">
                                    <br />
                                </td>
                                <td style="text-align: left;">Valor  Prorroga<br />
                                    <uc1:decimales ID="txtvalorarenovar" runat="server" CssClass="textbox"
                                        Enabled="false" />
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left;">
                                    <asp:TextBox ID="txtcodigotitular" runat="server" Visible="false" CssClass="textbox" Width="90%" />
                                </td>
                                <td style="text-align: left;">&nbsp;</td>
                                <td style="text-align: left;">&nbsp;</td>
                                <td style="text-align: left;">&nbsp;</td>
                                <td style="text-align: left;">&nbsp;</td>
                                <td style="text-align: left;">&nbsp;</td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:Panel ID="PanelPrroroga" runat="server">
                        <table>
                            <tr>
                                <td colspan="3" style="text-align: left">
                                    <strong>DATOS DE LA PRORROGA</strong>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 150px; text-align: left">Fecha Prorroga<br />
                                    <uc2:fecha ID="txtfechaProrr" Enabled="False" runat="server" CssClass="textbox" />
                                </td>
                                <td style="width: 150px; text-align: left">Nuevo Plazo<br />
                                    <asp:TextBox ID="txtPlazoPro" runat="server" CssClass="textbox" Width="60%"
                                        AutoPostBack="True" OnTextChanged="txtPlazoPro_TextChanged"
                                        Enabled="False" />Días
                                <asp:FilteredTextBoxExtender ID="fte99" runat="server" Enabled="True" FilterType="Numbers, Custom"
                                    TargetControlID="txtPlazoPro" ValidChars="" />
                                </td>
                                <td style="width: 150px; text-align: left">Nuevo Vencimiento<br />
                                    <uc2:fecha ID="txtFechaVenciPro" Enabled="false" runat="server" CssClass="textbox" />
                                </td>
                                <td style="width: 150px; text-align: left">Fec.Ult.Liq.Int<br />
                                    <uc2:fecha ID="txtfechaInt" runat="server" />
                                </td>
                                <td style="width: 150px; text-align: left">
                                    <asp:CheckBox ID="cbCapitalizaInteres"  AutoPostBack="true" runat="server" Text="Capitaliza Interés" OnCheckedChanged="cbCapitalizaInteres_CheckedChanged" />
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left; width: 150px;">
                                    <asp:CheckBox ID="cbInteresCuenta" runat="server"  Text="Interés por Cuenta" />
                                </td>
                                <td style="width: 590px" colspan="3">
                                    <asp:Panel ID="PanelTasaPro" runat="server">
                                        <ctl:ctlTasa ID="ctlTasaPro" runat="server" Width="400px" />
                                    </asp:Panel>

                                    &nbsp;</td>
                                <td style="width: 590px">&nbsp;</td>
                                <td></td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <table>
                        <tr>
                            <td colspan="3" style="text-align: left">
                                <strong><asp:Label ID="lblgiro" runat="server" ForeColor="Black" Style="text-align: right" Visible="true" Text="Datos Del Giro"></asp:Label>
                               
                                <uc3:ctlgiro ID="ctlGiro" runat="server" />

                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="ddlModalidad" />
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
                        <td style="text-align: center; font-size: large;">Prorroga 
                            <asp:Label ID="lblMsj" runat="server"></asp:Label>
                            Correctamente 
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
                        <td style="text-align: center; font-size: large;"></td>
                    </tr>
                </table>
            </asp:Panel>
        </asp:View>
    </asp:MultiView>


    <asp:HiddenField ID="HiddenField1" runat="server" />
    <uc4:mensajegrabar ID="ctlMensaje" runat="server" />
</asp:Content>
