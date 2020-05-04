<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" Title=".: Xpinn - Cobranzas & Moras:." %>

<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>


<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager runat="server"></asp:ScriptManager>
    <asp:MultiView ID="mvPrincipal" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwDatos" runat="server">
            <asp:Panel ID="pConsulta" runat="server">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td style="text-align: left; width: 150px;">Fecha de corte<br />
                            <ucFecha:fecha ID="ucFechaCorte" runat="server" style="text-align: center" />
                        </td>
                        <td style="text-align: left; width: 150px;">Productos a Anexar<br />
                            <asp:DropDownList ID="ddltipoProducto" runat="server"
                                CssClass="textbox">
                                <asp:ListItem Value="1,2,4,6">Todos</asp:ListItem>
                                <asp:ListItem Value="2">Creditos </asp:ListItem>
                                <asp:ListItem Value="1">Aportes</asp:ListItem>
                                <asp:ListItem Value="4">Servicios</asp:ListItem>
                                <asp:ListItem Value="1,2,4">Creditos, Aportes y Servicios</asp:ListItem>
                                <asp:ListItem Value="2,4">Creditos y Servicios</asp:ListItem>
                                <asp:ListItem Value="2,1"> Creditos y Aportes</asp:ListItem>
                                <asp:ListItem Value="6"> Afiliación</asp:ListItem>
                                <asp:ListItem Value="22">Creditos como codeudor</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td style="text-align: left; width: 150px;">Tipo documento a Generar<br />
                            <asp:DropDownList ID="ddlTipoDocumento" runat="server"
                                CssClass="textbox"
                                OnSelectedIndexChanged="ddlTipoDocumento_SelectedIndexChanged">
                                <%--<asp:ListItem Value="0"> Seleccione Un Item</asp:ListItem>--%>
                                <asp:ListItem Value="1">Reporte Moras</asp:ListItem>
                                <asp:ListItem Value="2">Carta 1</asp:ListItem>
                                <asp:ListItem Value="3">Carta 2</asp:ListItem>
                                <%--<asp:ListItem Value="4">Prejuridico Codeudor</asp:ListItem>
                                <asp:ListItem Value="5">Juridico</asp:ListItem>
                                <asp:ListItem Value="6">Juridico Codeudor</asp:ListItem>
                                <asp:ListItem Value="7">Campaña</asp:ListItem>
                                <asp:ListItem Value="8">Citación</asp:ListItem>--%>
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
                <table>
                    <tr>
                        <td style="text-align: left; width: 150px;">Codigó Cliente<br />
                            <asp:TextBox ID="txtCodCliente" runat="server" CssClass="textbox" Width="150px"></asp:TextBox>
                        </td>
                        <td style="text-align: left; width: 150px;">Identificación Cliente<br />
                            <asp:TextBox ID="txtIdentiCliente" runat="server" CssClass="textbox" Width="150px"></asp:TextBox>
                        </td>
                        <td style="text-align: left; width: 350px;">Nombre Cliente<br />
                            <asp:TextBox ID="txtNombreCliente" runat="server" CssClass="textbox" Width="150px"></asp:TextBox>
                        </td>
                        <%--      <td style="text-align: left; width: 150px;">cliente<br />

                        </td>--%>
                    </tr>
                </table>
                <table width="100%">
                    <tr>
                        <td colspan="12">
                            <asp:GridView ID="gvLista" runat="server" Width="100%" AutoGenerateColumns="False">
                                <Columns>
                                    <asp:BoundField DataField="periodo" HeaderText="Periodo">
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="numero_producto" HeaderText="Obligación">
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="descripcion" HeaderText="Descripción">
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="fecha_vencimento" HeaderText="Fec. Vencimiento" DataFormatString="{0:d}">
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="dias" HeaderText="Dias">
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="forma_pago" HeaderText="F.P.">
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="capital" HeaderText="Capital" DataFormatString="{0:N0}">
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="extras" HeaderText="Extras" DataFormatString="{0:N0}">
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="interes" HeaderText="Interes" DataFormatString="{0:N0}">
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="mora" HeaderText="Mora" DataFormatString="{0:N0}">
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="otros" HeaderText="Otros" DataFormatString="{0:N0}">
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="saldo_total" HeaderText="Saldo Total" DataFormatString="{0:N0}">
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                </Columns>
                                <HeaderStyle CssClass="gridHeader"></HeaderStyle>
                                <PagerStyle CssClass="gridPager"></PagerStyle>
                                <RowStyle CssClass="gridItem"></RowStyle>
                            </asp:GridView>
                            <asp:Label ID="lblTotalRegs" runat="server" Visible="False" />
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td style="text-align: right; width: 100px;">Total Capital<br />
                            <asp:TextBox ID="txtTotCap" runat="server" CssClass="textbox" Width="90%" disabled="false"></asp:TextBox>
                        </td>
                        <td style="text-align: right; width: 100px;">Total Extras<br />
                            <asp:TextBox ID="txtTotExt" runat="server" CssClass="textbox" Width="90%" disabled="false"></asp:TextBox>
                        </td>
                        <td style="text-align: right; width: 100px;">Total Interes<br />
                            <asp:TextBox ID="txtTotInt" runat="server" CssClass="textbox" Width="90%" disabled="false"></asp:TextBox>
                        </td>
                        <td style="text-align: right; width: 100px;">Total Mora<br />
                            <asp:TextBox ID="txtTotMor" runat="server" CssClass="textbox" Width="90%" disabled="false"></asp:TextBox>
                        </td>
                        <td style="text-align: right; width: 100px;">Total Otros<br />
                            <asp:TextBox ID="txtTotOtr" runat="server" CssClass="textbox" Width="90%" disabled="false"></asp:TextBox>
                        </td>
                        <td style="text-align: right; width: 100px;">Total<br />
                            <asp:TextBox ID="txtTototal" runat="server" CssClass="textbox" Width="90%" disabled="false"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </asp:View>
        <asp:View ID="vwListado" runat="server">
        </asp:View>
        <asp:View ID="vReporteExtracto" runat="server">
            <asp:Button ID="btnImprime" runat="server" CssClass="btn8" Height="25px" Width="120px" Text="Imprimir" OnClick="btnImprimir_Click" />
            <rsweb:ReportViewer ID="RpviewInfo1" runat="server" Font-Names="Verdana" Font-Size="8pt"
                Height="600px" InteractiveDeviceInfos="(Colección)" WaitMessageFont-Names="Verdana"
                WaitMessageFont-Size="14px" Width="990px" EnableViewState="True" Visible="false">
                <LocalReport ReportPath="Page\Asesores\Gestion_Mora\ReporteMoras.rdlc">
                </LocalReport>
            </rsweb:ReportViewer>

            <rsweb:ReportViewer ID="RpviewInfo2" runat="server" Font-Names="Verdana" Font-Size="8pt"
                Height="600px" InteractiveDeviceInfos="(Colección)" WaitMessageFont-Names="Verdana"
                WaitMessageFont-Size="14px" Width="990px" EnableViewState="True" Visible="false">
                <LocalReport ReportPath="Page\Asesores\Gestion_Mora\Carta_Aviso.rdlc">
                </LocalReport>
            </rsweb:ReportViewer>

            <rsweb:ReportViewer ID="RpviewInfo3" runat="server" Font-Names="Verdana" Font-Size="8pt"
                Height="600px" InteractiveDeviceInfos="(Colección)" WaitMessageFont-Names="Verdana"
                WaitMessageFont-Size="14px" Width="990px" EnableViewState="True" Visible="false">
                <LocalReport ReportPath="Page\Asesores\Gestion_Mora\Carta_Codeudor.rdlc">
                </LocalReport>
            </rsweb:ReportViewer>
        </asp:View>
    </asp:MultiView>

    <asp:HiddenField ID="hdFileName" runat="server" />
    <asp:HiddenField ID="hdFileNameThumb" runat="server" />
</asp:Content>
