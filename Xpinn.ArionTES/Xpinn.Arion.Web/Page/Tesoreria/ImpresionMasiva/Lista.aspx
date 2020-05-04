<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Lista.aspx.cs" Inherits="Lista" %>

<%@ Register Src="../../../General/Controles/fecha.ascx" TagName="fecha" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../../../General/Controles/decimales.ascx" TagName="decimales"
    TagPrefix="uc1" %>
<%@ Register Src="../../../General/Controles/ctlBusquedaRapida.ascx" TagName="ListadoPersonas"
    TagPrefix="uc1" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:MultiView ID="mvComprobante" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwTipoComprobante" runat="server">
            <asp:Panel ID="Principal" runat="server">
                <asp:Panel ID="pConsulta" runat="server" Width="100%">
                    <table width="100%">
                        <tr>
                            <td colspan="5" style="font-size: x-small">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td style="font-size: small; text-align: left" colspan="5">
                                <strong>Críterios de Búsqueda</strong>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left">
                                No. Comprobante<br />
                                <asp:TextBox ID="txtNumComp" runat="server" CssClass="textbox" Width="120px"></asp:TextBox>
                            </td>
                            <td style="text-align: left">
                                Estado<br />
                                <asp:DropDownList ID="ddlEstado" runat="server" AppendDataBoundItems="True" CssClass="textbox"
                                    Width="180px">
                                    <asp:ListItem Value="0" Selected="True">Seleccione un item</asp:ListItem>
                                    <asp:ListItem Value="E">Elaborado</asp:ListItem>
                                    <asp:ListItem Value="A">Aprobado</asp:ListItem>
                                    <asp:ListItem Value="N">aNulado</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td style="text-align: left">
                                Fecha<br />
                                <uc1:fecha ID="txtFechaIni" runat="server"></uc1:fecha>
                                a
                                <uc1:fecha ID="txtFechaFin" runat="server" />
                            </td>
                            <td style="text-align: left">
                                No.Sop<br />
                                <asp:TextBox ID="txtNumSop" runat="server" CssClass="textbox" Width="90px"></asp:TextBox>
                            </td>
                            <td style="text-align: left">
                                <asp:DropDownList ID="ddlTipoComprobante" runat="server" AppendDataBoundItems="True"
                                    CssClass="textbox" Width="95%" Visible="false">
                                    <asp:ListItem Value="0">Seleccione un Item</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left" colspan="5">
                                <table width="100%">
                                    <tr>
                                        <td style="text-align: left; width: 25%">
                                            Concepto<br />
                                            <asp:DropDownList ID="ddlConcepto" runat="server" AppendDataBoundItems="True" CssClass="textbox"
                                                Width="95%">
                                                <asp:ListItem Value=""></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="text-align: left; width: 25%">
                                            Ciudad<br />
                                            <asp:DropDownList ID="ddlCiudad" runat="server" AppendDataBoundItems="True" CssClass="textbox"
                                                Width="95%">
                                                <asp:ListItem Value=""></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="text-align: left; width: 25%">
                                            Ordenar por<br />
                                            <asp:DropDownList ID="ddlOrden1" runat="server" AppendDataBoundItems="True" CssClass="textbox"
                                                Width="70%">
                                                <asp:ListItem Value=""></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:CheckBox ID="chkDesc" runat="server" Text="Desc" TextAlign="Left" Style="font-size: x-small" />
                                        </td>
                                        <td style="text-align: left; width: 25%">
                                            Luego por<br />
                                            <asp:DropDownList ID="ddlOrdenLuego" runat="server" AppendDataBoundItems="True" CssClass="textbox"
                                                Width="70%">
                                                <asp:ListItem Value=""></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:CheckBox ID="chkDescLuego" runat="server" Text="Desc" TextAlign="Left" Style="font-size: x-small" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left">
                                Identificación<br />
                                <asp:TextBox ID="ddlIdentificacion" runat="server" AppendDataBoundItems="True" CssClass="textbox"
                                    Width="95px"></asp:TextBox>
                                <asp:Button ID="btnConsultaPersonas" runat="server" CssClass="btn8" Height="26px"
                                    OnClick="btnConsultaPersonas_Click" Text="..." />
                                <uc1:ListadoPersonas ID="ctlBusquedaPersonas" runat="server" />
                            </td>
                            <td style="text-align: left">
                                Nombres<br />
                                <asp:TextBox ID="ddlNombres" runat="server" AppendDataBoundItems="True" CssClass="textbox"
                                    Width="90%"></asp:TextBox>
                            </td>
                            <td style="text-align: left">
                                Apellidos/Razón Social<br />
                                <asp:TextBox ID="ddlApellidos" runat="server" AppendDataBoundItems="True" CssClass="textbox"
                                    Width="90%"></asp:TextBox>
                            </td>
                            <td colspan="2" style="text-align: left">
                                Valor Total<br />
                                <uc1:decimales ID="txtValorTotal" runat="server" Habilitado="True" Width_="80" />
                            </td>
                        </tr>
                    </table>
                    <asp:UpdatePanel ID="upFormaDesembolso" runat="server">
                        <ContentTemplate>
                            <asp:Panel ID="panelDeDonde" runat="server">
                                <table>
                                    <tr>
                                        <td style="text-align: left">
                                            Forma de Pago<br />
                                            <asp:DropDownList ID="DropDownFormaDesembolso" runat="server" Style="margin-left: 0px;
                                                text-align: left" Width="250px" CssClass="textbox">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="text-align: left">
                                            Entidad<br />
                                            <asp:DropDownList ID="ddlEntidadOrigen" runat="server" Style="margin-left: 0px; text-align: left"
                                                Width="250px" CssClass="textbox">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="text-align: left">
                                            &nbsp;
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:Panel>
                <hr />
                <asp:Panel ID="Listado" runat="server">
                    <table style="width: 100%;">
                        <tr>
                            <td>
                                <asp:GridView ID="gvLista" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                    BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px"
                                    CellPadding="4" Font-Size="X-Small" ForeColor="Black" GridLines="Vertical" PageSize="20"
                                    Width="100%" OnPageIndexChanging="gvLista_PageIndexChanging">
                                    <Columns>
                                        <asp:TemplateField HeaderStyle-CssClass="gridIco" ItemStyle-CssClass="gridIco">
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="cbSeleccionarEncabezado" runat="server" Checked="false" OnCheckedChanged="cbSeleccionarEncabezado_CheckedChanged"
                                                    AutoPostBack="True" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="cbSeleccionar" runat="server" Checked="false" />
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="gridIco"></HeaderStyle>
                                            <ItemStyle CssClass="gridIco"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="num_comp" HeaderText="Número" />
                                        <asp:BoundField DataField="tipo_comp" HeaderText="Tipo" />
                                        <asp:BoundField DataField="fecha" DataFormatString="{0:d}" HeaderText="Fecha" />
                                        <asp:BoundField DataField="soporte" HeaderText="Num.Sop." />
                                        <asp:BoundField DataField="descripcion_concepto" HeaderText="Concepto" />
                                        <asp:BoundField DataField="ciudad" HeaderText="Ciudad" />
                                        <asp:BoundField DataField="iden_benef" HeaderText="Identificacion" />
                                        <asp:BoundField DataField="nombres" HeaderText="Nombre" />
                                        <asp:BoundField DataField="apellidos" HeaderText="Apellidos" />
                                        <asp:BoundField DataField="razon_social" HeaderText="Razón Social" />
                                        <asp:BoundField DataField="elaboro" HeaderText="Elaborado por" />
                                        <asp:BoundField DataField="aprobo" HeaderText="Aprobado por" />
                                        <asp:BoundField DataField="estado" HeaderText="Estado" />
                                        <asp:BoundField DataField="totalcom" DataFormatString="{0:N0}" HeaderText="Valor">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="desembolso" DataFormatString="{0:N0}" HeaderText="Girado">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
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
                                <asp:Label ID="lblTotalRegs" runat="server" Visible="False" />
                                <asp:Label ID="lblInfo" runat="server" Text="Su consulta no obtuvo ningun resultado."
                                    Visible="False" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </asp:Panel>
        </asp:View>
        <asp:View ID="vwComprobanteImpr" runat="server">
            <br />
            <br />
            <br />
            
            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                <tr>
                <td>
                <asp:Button ID="btnRegresarComp" runat="server" CssClass="btn8" OnClick="btnRegresarComp_Click" Height="27px"
                    Text="Regresar al Comprobante" />
                </td>
                <td>
                <asp:Button ID="btnImprime" runat="server" CssClass="btn8" OnClick="btnImprime_Click" Height="27px"
                    Text="   IMPRIMIR   " />
                </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <iframe id="frmPrint" name="IframeName" width="100%" src="../../Reportes/Reporte.aspx" height="500px"
                            runat="server" style="border-style: groove; float: left;"></iframe>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <rsweb:ReportViewer ID="RpviewComprobante" runat="server" Font-Names="Verdana" Font-Size="8pt"
                            Height="450px" InteractiveDeviceInfos="(Colección)" WaitMessageFont-Names="Verdana"
                            WaitMessageFont-Size="14pt" Width="100%">
                            <LocalReport ReportPath="Page\Tesoreria\ImpresionMasiva\rptImpresionMasiva.rdlc">
                            </LocalReport>
                        </rsweb:ReportViewer>
                    </td>
                </tr>
            </table>           
        </asp:View>
    </asp:MultiView>
</asp:Content>
