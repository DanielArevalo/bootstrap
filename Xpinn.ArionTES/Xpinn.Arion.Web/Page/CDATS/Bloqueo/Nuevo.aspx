<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" Title=".: Xpinn - CDATS Cierre :." %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>
<%@ Register Src="~/General/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="uc2" %>
<%@ Register Src="~/General/Controles/fechaeditable.ascx" TagName="fecha" TagPrefix="uc5" %>
<%@ Register Src="~/General/Controles/ctlBusquedaRapida.ascx" TagName="ListadoPersonas"
    TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar"
    TagPrefix="uc4" %>
<%@ Register Src="~/General/Controles/ctlGiro.ascx" TagName="giro" TagPrefix="uc3" %>
<%@ Register Src="~/General/Controles/ctlTasa.ascx" TagName="tasa" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:MultiView ID="mvPrincipal" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwDatos" runat="server">
            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                    <table border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <td style="text-align: left;">
                                Fecha de Bloqueo<br />
                                <uc5:fecha ID="txtFechaCierre" runat="server" />
                            </td>
                            <td style="text-align: left;" colspan="3">
                                Observacion<br />
                                <asp:TextBox ID="txtObservacion" runat="server" CssClass="textbox" Width="300px" />
                            </td>
                            <td colspan="3">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left;" colspan="4">
                                <strong>Datos del CDAT</strong>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left; width: 150px;">
                                Número CDAT<br />
                                <asp:TextBox ID="txtCodigo" runat="server" CssClass="textbox" Visible="false" Width="15px" />
                                <asp:TextBox ID="txtNumCDAT" runat="server" CssClass="textbox" Width="110px" Enabled="false" />
                                <asp:TextBox ID="txtDigVerif" runat="server" CssClass="textbox" Width="30px" Enabled="false" />
                            </td>
                            <td style="text-align: left; width: 145px">
                                Fecha Apertura<br />
                                <uc2:fecha ID="txtFechaApertura" runat="server" CssClass="textbox" Enabled="false" />
                            </td>
                            <td style="text-align: left;" colspan="3">
                                Tipo/Linea de CDAT<br />
                                <asp:DropDownList ID="ddlTipoLinea" runat="server" CssClass="textbox" Width="300px"
                                    Height="28px" AppendDataBoundItems="True" Enabled="false" />
                            </td>
                            <td style="text-align: left;" colspan="3">
                                Estado<br />
                                <asp:TextBox ID="txtEstado" runat="server" CssClass="textbox" Width="40px" Enabled="false" />
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left; width: 205px">
                                Oficina<br />
                                <asp:DropDownList ID="ddlOficina" runat="server" CssClass="textbox" Width="200px"
                                    Height="28px" AppendDataBoundItems="True" Enabled="false" />
                            </td>
                            <td style="text-align: left;">
                                Valor<br />
                                <uc1:decimales ID="txtValor" runat="server" CssClass="textbox" Enabled="false" />
                            </td>
                            <td style="text-align: left; width: 160px">
                                Moneda<br />
                                <asp:DropDownList ID="ddlTipoMoneda" runat="server" CssClass="textbox" Width="155px"
                                    Height="28px" Enabled="false" />
                            </td>
                            <td style="text-align: left; width: 100px">
                                Plazo<br />
                                <asp:TextBox ID="txtPlazo" runat="server" CssClass="textbox" Width="90px" Enabled="false" />
                                <asp:FilteredTextBoxExtender ID="fte1" runat="server" Enabled="True" FilterType="Numbers, Custom"
                                    TargetControlID="txtPlazo" ValidChars="" />
                            </td>
                            <td style="text-align: left;">
                                Tipo Calendario<br />
                                <asp:DropDownList ID="ddlTipoCalendario" runat="server" CssClass="textbox" Width="150px"
                                    Height="28px" Enabled="false" />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:UpdatePanel ID="Panelgrilla" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                <ContentTemplate>
                    <table>
                        <tr>
                            <td style="text-align: left;" colspan="4">
                                <br />
                                <strong>Titulares:</strong><br />
                                <div style="overflow: scroll; width: 100%">
                                    <asp:GridView ID="gvDetalle" runat="server" Width="95%" AutoGenerateColumns="False"
                                        AllowPaging="True" PageSize="20" HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager"
                                        RowStyle-CssClass="gridItem" Style="font-size: x-small" GridLines="Horizontal" onpageindexchanging="gvDetalle_PageIndexChanging" 
                                        DataKeyNames="cod_usuario_cdat">
                                        <Columns>
                                            <asp:BoundField DataField="cod_usuario_cdat" HeaderText="Codigo">
                                                <ItemStyle HorizontalAlign="center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="identificacion" HeaderText="Identificación">
                                                <ItemStyle HorizontalAlign="left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="cod_persona" HeaderText="Cod. Persona">
                                                <ItemStyle HorizontalAlign="left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="nombres" HeaderText="Nombres">
                                                <ItemStyle HorizontalAlign="left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="apellidos" HeaderText="Apellidos">
                                                <ItemStyle HorizontalAlign="left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ciudad" HeaderText="Ciudad">
                                                <ItemStyle HorizontalAlign="left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="direccion" HeaderText="Dirección">
                                                <ItemStyle HorizontalAlign="left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="telefono" HeaderText="Teléfono">
                                                <ItemStyle HorizontalAlign="left" />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="Principal">
                                                <ItemTemplate>
                                                    <cc1:CheckBoxGrid ID="chkPrincipal" runat="server" Enabled="false" Checked='<%# Convert.ToBoolean(Eval("principal")) %>'
                                                        CommandArgument='<%#Container.DataItemIndex %>' AutoPostBack="true" OnCheckedChanged="chkPrincipal_CheckedChanged" /></ItemTemplate>
                                                <ItemStyle HorizontalAlign="center" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="conjuncion" HeaderText="Conjunción">
                                                <ItemStyle HorizontalAlign="left" />
                                            </asp:BoundField>
                                        </Columns>
                                        <HeaderStyle CssClass="gridHeader"></HeaderStyle>
                                        <PagerStyle CssClass="gridPager"></PagerStyle>
                                        <RowStyle CssClass="gridItem"></RowStyle>
                                    </asp:GridView>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left; vertical-align: top; width: 480px" colspan="2" rowspan="2">
                                <uc3:tasa ID="ctlTasa" runat="server" Enabled="false" />
                            </td>
                            <td style="text-align: left; vertical-align: top">
                                Modalidad Int<br />
                                <asp:RadioButtonList ID="rblModalidadInt" runat="server" Enabled="false" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="1">Vencido</asp:ListItem>
                                    <asp:ListItem Value="2">Anticipado</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            <td style="text-align: left; vertical-align: top">
                                  <Label ID="lblperiodicidad" Text="Periodicidad Intereses" runat="server"></Label>
                                <asp:DropDownList ID="ddlPeriodicidad" runat="server" Enabled="false" CssClass="textbox" Width="250px"
                                    Height="28px" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                &nbsp;
                            </td>
                        </tr>
                        <%--                        <tr>
                            <td style="text-align: left; width: 150px">
                                Valor
                            </td>
                            <td style="text-align: left">
                                <uc1:decimales ID="txtValorLiquidacion" runat="server" CssClass="textbox" />
                            </td>
                            <td style="width: 500px; text-align: left; vertical-align: top" colspan="2" rowspan="6">
                                <uc3:giro ID="ctlGiro" runat="server" />
                            </td>
                        </tr>--%>
                    </table>
                </ContentTemplate>
                <Triggers>
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
                        <td style="text-align: center; font-size: large;">
                            Apertura del CDAT
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
