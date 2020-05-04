<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Lista.aspx.cs" Inherits="Lista" Title=".: Xpinn - Desembolso Masivo :." %>

<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/tnumero.ascx" TagName="numero" TagPrefix="ucnumero" %>
<%@ Register Src="../../../General/Controles/ctlBusquedaRapida.ascx" TagName="ListadoPersonas" TagPrefix="uc1" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:MultiView ID="mvPrincipal" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwData" runat="server">
            <asp:Panel ID="pConsulta" runat="server">
                <br />
                <br />
                <asp:Panel ID="pEncBusqueda" runat="server" CssClass="collapsePanelHeader" Height="30px">
                    <div style="padding: 5px; cursor: pointer; vertical-align: middle;">
                        <div style="float: left; color: #0066FF; font-size: small">
                            Criterios de Selección</div>
                        <div style="float: left; margin-left: 5px; color: #0066FF; font-size: small">
                            <asp:Label ID="lblMostrarDetalles" runat="server">(Mostrar Detalles...)</asp:Label>
                        </div>
                        <div style="float: right; vertical-align: middle;">
                            <asp:ImageButton ID="imgExpand" runat="server" ImageUrl="~/Images/expand.jpg" AlternateText="(Show Details...)" />
                        </div>
                    </div>
                </asp:Panel>
                <%--<div id="divBusqueda" runat="server" style="overflow: scroll; width: 100%;">--%>
                    <asp:Panel ID="pBusqueda" runat="server" Height="200px">
                        <table width="100%">
                            <tr>
                                <td style="text-align: left">
                                    Código<br />
                                    <asp:TextBox ID="txtCodigo" runat="server" CssClass="textbox" Width="110px" />
                                </td>
                                <td style="text-align: left">
                                    Num. Factura<br />
                                    <asp:TextBox ID="txtNumFactura" runat="server" CssClass="textbox" Width="100px" />
                                </td>
                                <td style="text-align: left">
                                    Fecha Ingreso<br />
                                    <ucFecha:fecha ID="txtFechaIngreso" runat="server" CssClass="textbox" />
                                </td>
                                <td style="text-align: left" colspan="2">
                                    Fecha Vencimiento<br />
                                    <ucFecha:fecha ID="txtFechaVencIni" runat="server" CssClass="textbox" />
                                    a
                                    <ucFecha:fecha ID="txtFechaVencFin" runat="server" CssClass="textbox" />
                                </td>
                                <td style="text-align: left">
                                    Usuario<br />
                                    <asp:DropDownList ID="ddlUsuario" runat="server" CssClass="textbox" Width="200px" />
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left">
                                    Identificación<br />
                                    <asp:TextBox ID="txtIdentificacion" runat="server" CssClass="textbox" Width="110px" />
                                </td>
                                <td style="text-align: left" colspan="2">
                                    Nombres<br />
                                    <asp:TextBox ID="txtNombres" runat="server" CssClass="textbox" Width="90%" />
                                </td>
                                <td style="text-align: left">
                                    Tipo Cta por Pagar<br />
                                    <asp:DropDownList ID="ddlTipoCtaXpagar" runat="server" CssClass="textbox" Width="150px"
                                        AppendDataBoundItems="true">
                                        <asp:ListItem Value="0" Text="Seleccione un item"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td style="text-align: left">
                                    Ordenar Por:<br />
                                    <asp:DropDownList ID="ddlOrdenadoPor" runat="server" CssClass="textbox" Width="150px" />
                                </td>
                                <td style="text-align: left">
                                    Luego Por:<br />
                                    <asp:DropDownList ID="ddlLuegoPor" runat="server" CssClass="textbox" Width="150px" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                <%--</div>--%>
                <asp:CollapsiblePanelExtender ID="cpeDemo" runat="Server" TargetControlID="pBusqueda"
                    ExpandControlID="pEncBusqueda" CollapseControlID="pEncBusqueda" Collapsed="false"
                    ExpandedSize="100" TextLabelID="lblMostrarDetalles" ImageControlID="imgExpand"
                    ExpandedText="(Click Aqui para Ocultar Detalles...)" CollapsedText="(Click Aqui para Mostrar Detalles...)"
                    ExpandedImage="~/Images/collapse.jpg" CollapsedImage="~/Images/expand.jpg" SuppressPostBack="true"
                    SkinID="CollapsiblePanelDemo" />
            </asp:Panel>
            <hr style="width:100%" />
            
            <table width="100%">
                <tr>
                    <td style="text-align: left">
                        <strong>Datos de la Aprobación</strong>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left">
                        Fecha Aprobación<br />
                        <ucFecha:fecha ID="txtFechaAprobacion" runat="server" CssClass="textbox" />
                    </td>
                </tr>
            </table>
            
            <asp:UpdatePanel ID="UpdData" runat="server">
                <ContentTemplate>
                    <asp:Panel ID="panelGrilla" runat="server">
                        <table style="width: 95%">
                            <tr>
                                <td style="text-align: left">
                                    <strong>Listado de cuentas por pagar a aprobar</strong><br />
                                    <div id="divGiros" runat="server" style="overflow: scroll; width: 100%;">
                                        <asp:GridView ID="gvLista" runat="server" AutoGenerateColumns="False"
                                            AllowPaging="True" PageSize="20" GridLines="Horizontal" ShowHeaderWhenEmpty="True"
                                            OnPageIndexChanging="gvLista_PageIndexChanging" DataKeyNames="codpagofac,cod_persona" Style="font-size: x-small"
                                            OnRowDataBound="gvLista_RowDataBound">
                                            <Columns>
                                               <asp:BoundField DataField="codpagofac" HeaderText="codFormaPago" Visible="false">
                                                    <ItemStyle HorizontalAlign="Left"/>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="codigo_factura" HeaderText="Código">
                                                    <ItemStyle HorizontalAlign="Left"/>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="numero_factura" HeaderText="Num. Fac">
                                                    <ItemStyle HorizontalAlign="Left"/>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="fecha_ingreso" HeaderText="Fecha Ingreso" DataFormatString="{0:d}">
                                                    <ItemStyle HorizontalAlign="Left"/>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="fec_fact" HeaderText="Fecha Factu" DataFormatString="{0:d}">
                                                    <ItemStyle HorizontalAlign="Left"/>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="fecha_vencimiento" HeaderText="Fecha Vencimiento" DataFormatString="{0:d}">
                                                    <ItemStyle HorizontalAlign="Left"/>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="cod_persona" HeaderText="Cod. Persona" Visible="false">
                                                    <ItemStyle HorizontalAlign="Left"/>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="identificacion" HeaderText="Identific.">
                                                    <ItemStyle HorizontalAlign="Left"/>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="nombre" HeaderText="Nombre">
                                                    <ItemStyle HorizontalAlign="Left" Width="25%"/>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="valortotal" HeaderText="Vr a Pagar" DataFormatString="{0:N0}">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="valordescuento" HeaderText="Vr Descuento" DataFormatString="{0:N0}">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="valorneto" HeaderText="Neto a Pagar" DataFormatString="{0:N0}">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:BoundField>
                                             

                                                <asp:TemplateField HeaderText="Seleccionar" HeaderStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <cc1:CheckBoxGrid ID="cbSeleccionar" runat="server" Checked="false" AutoPostBack="true" 
                                                        OnCheckedChanged="cbSeleccionar_CheckedChanged" CommandArgument="<%#Container.DataItemIndex %>"/></ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Forma Pago" HeaderStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFormaPago" runat="server" Text='<%# Bind("forma_pago") %>' Visible="false"></asp:Label>
                                                        <cc1:DropDownListGrid ID="ddlFormaPago" runat="server" CssClass="textbox" AutoPostBack="true" Width="120px"
                                                            OnSelectedIndexChanged="ddlFormaPago_SelectedIndexChanged" CommandArgument="<%#Container.DataItemIndex %>" />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Banco" HeaderStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblEntidad" runat="server" Text='<%# Bind("cod_banco") %>' Visible="false"></asp:Label>
                                                        <cc1:DropDownListGrid ID="ddlEntidad" runat="server" CssClass="textbox" AutoPostBack="true"
                                                            CommandArgument="<%#Container.DataItemIndex %>"   OnSelectedIndexChanged="ddlEntidad_Cuenta_SelectedIndexChanged" Width="140px" />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Tipo Cuenta" HeaderStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTipoCuenta" runat="server" Text='<%# Bind("tipo_cuenta") %>' Visible="false"></asp:Label>
                                                        <cc1:DropDownListGrid ID="ddlTipo_Cuenta" runat="server" CssClass="textbox" AutoPostBack="true"
                                                            CommandArgument="<%#Container.DataItemIndex %>" Width="120px" />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Cuenta" HeaderStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <cc1:TextBoxGrid ID="txtNum_Cuenta" runat="server" CssClass="textbox" AutoPostBack="true"
                                                            Text='<%# Bind("num_cuenta") %>' Width="90px" />
                                                            <asp:FilteredTextBoxExtender ID="fte20" runat="server" Enabled="True" FilterType="Numbers, Custom"
                                                             TargetControlID="txtNum_Cuenta" ValidChars="-" />
                                                            </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                </asp:TemplateField>
                                                           <asp:BoundField DataField="cod_ope" HeaderText="Cod_ope">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:BoundField>


                                            </Columns>
                                            <HeaderStyle CssClass="gridHeader" />
                                            <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                                            <RowStyle CssClass="gridItem" />
                                        </asp:GridView>
                                    </div>
                                </td>
                            </tr>
                        </table>
                        <center>
                            <asp:Label ID="lblInfo" runat="server" Text="Su consulta no obtuvo ningún resultado"
                                Visible="false" /></center>
                        <table width="100%">
                            <tr>
                                <td style="text-align: center">
                                    Valor Total de facturas a Aprobar
                                </td>
                                <td style="text-align: center">
                                    <uc1:decimales ID="txtVrAprobar" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </ContentTemplate>
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
                        <td style="text-align: center; font-size: large;color:Red">
                            Se aprobaron correctamente las cuentas por pagar seleccionadas
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
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </asp:View>
    </asp:MultiView>
    <uc4:mensajegrabar ID="ctlMensaje" runat="server" />
</asp:Content>
