<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" Title=".: Xpinn - Tercero :." %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="Fecha" TagPrefix="ucFecha" %>
<%@ Register Src="~/General/Controles/fechaeditable.ascx" TagName="fecha" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc2" %>
<%@ Register Src="~/General/Controles/ctlFormaPago.ascx" TagName="Forma" TagPrefix="uc3" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:MultiView ID="mvDatos" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwEscoger" runat="server" EnableTheming="True">
            <asp:Panel ID="PanelTipoComprobante" runat="server">
                <table cellpadding="5" cellspacing="0" style="width: 100%">
                    <tr>
                        <td colspan="5" style="text-align: center; color: #FFFFFF; background-color: #0066FF;
                            height: 20px">
                            Datos De La Persona
                        </td>
                    </tr>
                </table>
                <table cellpadding="5" cellspacing="0" style="width: 100%">
                    <tr>
                        <td style="text-align: left; width: 141px;">
                            Identificación<br />
                            <asp:TextBox ID="txtIdentificacion" runat="server" CssClass="textbox" Enabled="false"></asp:TextBox>
                        </td>
                        <td style="text-align: left; width: 189px;">
                            Tipo Identif.<br />
                            <asp:TextBox ID="txtTipoidentif" runat="server" CssClass="textbox" Enabled="false"></asp:TextBox>
                        </td>
                        <td style="text-align: left; width: 314px;">
                            Nombres.<br />
                            <asp:TextBox ID="txtNombres" runat="server" CssClass="textbox" Enabled="false" 
                                Width="316px"></asp:TextBox>
                        </td>
                        <td style="text-align: left;">
                            Apellidos<br />
                            <asp:TextBox ID="txtApellidos" runat="server" CssClass="textbox" Enabled="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; width: 141px;">
                            Ciudad Residencia.<br />
                            <asp:TextBox ID="txtciudadresidencia" runat="server" CssClass="textbox" Enabled="false"
                                Width="156px"></asp:TextBox>
                        </td>
                        <td style="text-align: left; width: 189px;">
                            Direccion.<br />
                            <asp:TextBox ID="txtDireccion" runat="server" CssClass="textbox" Enabled="false"
                                Width="178px"></asp:TextBox>
                        </td>
                        <td style="text-align: left; width: 314px;">
                            Telefono.<br />
                            <asp:TextBox ID="TXTtELEFONO" runat="server" CssClass="textbox" Enabled="false" 
                                Width="314px"></asp:TextBox>
                            &nbsp;
                        </td>
                    </tr>
                </table>
                <table cellpadding="5" cellspacing="0" style="width: 100%">
                    <tr>
                        <td style="text-align: left;">
                            Id. Afiliación<br />
                            <asp:TextBox ID="txtafiliacionid" runat="server" CssClass="textbox" Enabled="false"></asp:TextBox>
                        </td>
                        <td style="text-align: left;">
                            Fec.Ult.Afil.<br />
                            <uc1:fecha ID="txtfecafi" runat="server" CssClass="textbox" Enabled="false"></uc1:fecha>
                        </td>
                        <td style="text-align: left;">
                            Estado Actual<br />
                            <asp:TextBox ID="txtestado" runat="server" CssClass="textbox" Enabled="false"></asp:TextBox>
                        </td>
                        <td style="text-align: left;">
                            F.Retiro<br />
                            <asp:TextBox ID="txtfecretiro" runat="server" CssClass="textbox" Enabled="false"></asp:TextBox>
                        </td>
                        <td style="text-align: left;">
                            Motivo del Retiro<br />
                            <asp:TextBox ID="txtmotivo" runat="server" CssClass="textbox" Enabled="TRUE" 
                                Width="189px"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <table cellpadding="5" cellspacing="0" style="width: 100%">
                    <tr>
                        <td colspan="5" style="text-align: center; color: #FFFFFF; background-color: #0066FF;
                            height: 20px">
                            Datos De La Afiliacion
                        </td>
                    </tr>
                    <tr>
                        <td colspan="5">
                            <table>
                                <tr>
                                    <td style="text-align: left; width: 155px">
                                        Fecha de Reafiliación<br>
                                        <asp:TextBox ID="txtcodAfiliacion" runat="server" Width="100px" CssClass="textbox"
                                            Style="text-align: right" Visible="false" />
                                        <uc1:fecha ID="txtFechaAfili" runat="server" Enabled="True" style="width: 140px"/>
                                    </td>
                                    <td style="text-align: left; width: 170px">
                                        Forma de Pago<br />
                                        <asp:DropDownList ID="ddlFormaPago" runat="server" Width="95%" CssClass="textbox"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlFormaPago_SelectedIndexChanged">
                                            <asp:ListItem Value="1">Caja</asp:ListItem>
                                            <asp:ListItem Value="2">Nomina</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td style="text-align: left;">
                                        <asp:Label ID="lblEmpresa" runat="server" Text="Empresa" /><br />
                                        <asp:DropDownList ID="ddlEmpresa" runat="server" Width="180px" CssClass="textbox"
                                            OnSelectedIndexChanged="ddlEmpresa_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                    <td style="text-align: left;">
                                        Oficina<br />
                                        <asp:DropDownList ID="ddlOficina" runat="server" Width="180px" CssClass="textbox">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: left;">
                                        Valor<br />
                                        <uc2:decimales ID="txtValorAfili" runat="server" style="text-align: right;" />
                                    </td>
                                     <td style="text-align: left;">
                                        Periodicidad<br />
                                        <asp:DropDownList ID="ddlPeriodicidad" runat="server" Width="95%" CssClass="textbox"
                                            Enabled="false">
                                        </asp:DropDownList>
                                    </td>
                                    <td style="text-align: left;">
                                        Nro Cuotas<br />
                                        <asp:TextBox ID="txtCuotasAfili" runat="server" Width="100px" CssClass="textbox"
                                            Enabled="false" Style="text-align: right" /><asp:FilteredTextBoxExtender ID="txtValor_FilteredTextBoxExtender"
                                                runat="server" Enabled="True" FilterType="Numbers, Custom" TargetControlID="txtCuotasAfili"
                                                ValidChars="" />
                                    </td>
                                     <td style="text-align: left;">
                                        Fecha de 1er Pago<br />
                                        <uc1:fecha ID="txtFecha1Pago" runat="server" style="width: 140px" />
                                    </td>
                                   
                                    <td style="text-align: left;">
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <table cellpadding="5" cellspacing="0" style="width: 100%">
                    <tr>
                        <td colspan="5" style="text-align: center; color: #FFFFFF; background-color: #0066FF;
                            height: 20px">
                            Cuentas De Aportes
                        </td>
                    </tr>
                </table>
                <table style="width: 100%">
                    <tr>
                        <td>
                          <%--  <asp:UpdatePanel ID="udpGridAportes" runat="server" UpdateMode="Conditional" RenderMode="Inline">
                                <ContentTemplate>--%>
                                    <asp:GridView ID="gvLista" runat="server" Width="99%" AutoGenerateColumns="False"
                                        PageSize="20" GridLines="Horizontal" ShowHeaderWhenEmpty="True"
                                        DataKeyNames="numero_aporte, cod_linea_aporte">
                                        <Columns>
                                            <asp:BoundField DataField="numero_aporte" HeaderText="Num. Aporte">
                                                <ItemStyle HorizontalAlign="Left" Width="100px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="fecha_apertura" HeaderText="F. Apertura" DataFormatString="{0:d}" />
                                            <asp:BoundField DataField="cod_linea_aporte" HeaderText="Linea">
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="nom_linea_aporte" HeaderText="Nombre Linea">
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="cuota" HeaderText="Valor Cuota">
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="fecha_prox_pago" HeaderText="F. Próx. Pago" DataFormatString="{0:d}" />
                                            <asp:BoundField DataField="forma_pago" HeaderText="Forma Pago">
                                                <ItemStyle HorizontalAlign="center" />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="Activar" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkActivar" runat="server" EnableViewState="true" Checked="false"
                                                        Enabled="true" OnCheckedChanged="chkActivar_CheckedChanged" AutoPostBack="true" />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <HeaderStyle CssClass="gridHeader" />
                                        <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                                        <RowStyle CssClass="gridItem" />
                                    </asp:GridView>
                            <%--    </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="chkActivar" EventName="CheckedChanged" />
                                </Triggers>
                            </asp:UpdatePanel>--%>
                            <asp:Label ID="lblTotalRegs" runat="server" Visible="False" />
                            <asp:Label ID="lblInfo" runat="server" Text="Su consulta no obtuvo ningún resultado."
                                Visible="False" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </asp:View>
        <asp:View ID="View1" runat="server" EnableTheming="True">
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
                        <asp:Label ID="lblMsj" runat="server" text="DATOS MODIFICADOS CORRECTAMENTE"/>
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
        </asp:View>
    </asp:MultiView>
</asp:Content>
