<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" EnableEventValidation="true" CodeFile="Lista.aspx.cs" Inherits="Page_Asesores_ReporteRecuperaciones_Lista" Title=".: Xpinn - Asesores :." %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:Panel ID="pConsulta" runat="server" Width="100%">
        <table style="width: 100%;">
            <tr>
                <td colspan="2" style="text-align: left; height: 50px;" width="100%">Reporte A Generar<br />
                    <asp:DropDownList ID="ddlConsultar" runat="server" AutoPostBack="True"
                        CssClass="textbox" OnSelectedIndexChanged="ddlConsultar_SelectedIndexChanged" Width="468px">
                        <asp:ListItem Value="0">&lt;Seleccione un Item&gt;</asp:ListItem>
                        <asp:ListItem Value="1">REPORTE DE DILIGENCIAS SEGUN FECHA DE ACUERDO</asp:ListItem>
                        <asp:ListItem Value="2">REPORTE DE DILIGENCIAS SEGUN FECHA DE DILIGENCIA</asp:ListItem>
                    </asp:DropDownList>
                    <br />
                </td>
            </tr>
            <tr>
                <td style="text-align: left; height: 52px;" width="20%">
                    <asp:Panel ID="Panel1" runat="server" HorizontalAlign="Center" Width="100%">
                        <asp:Label ID="LabelFecha_gara2" runat="server" Text="Fecha Inicial Acuerdo"></asp:Label>
                        <br />
                        <asp:TextBox ID="txtFechaIni" runat="server" CssClass="textbox"
                            MaxLength="10" Width="80%"></asp:TextBox>
                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                            PopupButtonID="Image2" TargetControlID="txtFechaIni">
                        </asp:CalendarExtender>
                        <asp:Label ID="Label5" runat="server" Style="color: #FF3300"></asp:Label>
                    </asp:Panel>
                    <asp:Panel ID="Panel3" runat="server" HorizontalAlign="Center" Width="100%">
                        <asp:Label ID="LabelFecha_gara1" runat="server" Text="Fecha Inicial Diligencia"></asp:Label>
                        <br />
                        <asp:TextBox ID="txtFechaInidilig" runat="server" CssClass="textbox"
                            MaxLength="10" Width="80%"></asp:TextBox>
                        <asp:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd/MM/yyyy"
                            PopupButtonID="Image1" TargetControlID="txtFechaInidilig">
                        </asp:CalendarExtender>
                        <asp:Label ID="Label3" runat="server" Style="color: #FF3300"></asp:Label>
                    </asp:Panel>
                </td>
                <td style="text-align: left; height: 52px;" width="15%">
                    <asp:Panel ID="Panel2" runat="server" HorizontalAlign="Center" Width="100%">
                        <asp:Label ID="LabelFecha_gara3" runat="server" Text="Fecha Final Acuerdo"></asp:Label>
                        <br />
                        <asp:TextBox ID="txtFechaFin" runat="server" CssClass="textbox"
                            MaxLength="10" Width="90%"></asp:TextBox>
                        <asp:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy"
                            PopupButtonID="Image2" TargetControlID="txtFechaFin">
                        </asp:CalendarExtender>
                        <asp:Label ID="Label6" runat="server" Style="color: #FF3300"></asp:Label>
                    </asp:Panel>
                    <asp:Panel ID="Panel4" runat="server" HorizontalAlign="Center" Width="100%">
                        <asp:Label ID="LabelFecha_gara0" runat="server" Text="Fecha Final Diligencia"></asp:Label>
                        <br />
                        <asp:TextBox ID="txtFechaFinDilig" runat="server" CssClass="textbox"
                            MaxLength="10" Width="90%"></asp:TextBox>
                        <asp:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy"
                            PopupButtonID="Image2" TargetControlID="txtFechaFinDilig">
                        </asp:CalendarExtender>
                        <asp:Label ID="Label4" runat="server" Style="color: #FF3300"></asp:Label>
                    </asp:Panel>
                </td>
                <td style="text-align: left; height: 52px; width: 42%;">
                    <asp:Label ID="Labelusuario" runat="server" Text="Usuario"></asp:Label>
                    <asp:DropDownList ID="ddlAsesores" runat="server" CssClass="textbox"
                        Style="margin-bottom: 0px" Width="70%">
                    </asp:DropDownList>
                </td>
                <td style="text-align: left; height: 52px;" width="50%">
                    <asp:Label ID="lblZona" runat="server" Text="Zona"></asp:Label>
                    <asp:DropDownList ID="ddlZona" runat="server" CssClass="textbox"
                        Style="margin-bottom: 0px" Width="77%">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <hr />
    <asp:MultiView ID="mvLista" runat="server">
        <asp:View ID="GvLista" runat="server">
            <div style="overflow: scroll; height: 272px; width: 1000px;">
                <div style="width: 1500px;">

                    <asp:Button ID="btnExportar" runat="server" CssClass="btn8"
                        OnClick="btnExportar_Click" Text="Exportar a excel" />

                    <asp:GridView ID="gvReportemovdiario" runat="server" AutoGenerateColumns="False" HeaderStyle-CssClass="gridHeader"
                        PagerStyle-CssClass="gridPager" RowStyle-CssClass="gridItem" ShowFooter="False"
                        Style="font-size: xx-small" Width="90%" GridLines="Both" OnRowDataBound="gvReportemovdiario_RowDataBound">
                        <Columns>
                            <asp:HyperLinkField DataNavigateUrlFields="NUMERO_RADICACION"
                                DataNavigateUrlFormatString="..//../Recuperacion/Detalle.aspx?radicado={0}"
                                DataTextField="NUMERO_RADICACION" HeaderText="NUMERO_RADICACION"
                                Target="_blank" />
                            <asp:TemplateField HeaderStyle-CssClass="gridIco" ItemStyle-CssClass="gridIco">
                                <HeaderStyle CssClass="gridIco" />
                                <ItemStyle CssClass="gridIco" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="identificacion" HeaderText="IDENTIFICACION" />
                            <asp:BoundField DataField="nombre" HeaderText="NOMBRES">
                                <ItemStyle HorizontalAlign="center" Width="300px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="fecha_diligencia" HeaderText="FECHA DILIGENCIA" DataFormatString="{0:d}">
                                <ItemStyle HorizontalAlign="center" Width="100px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="tipo_diligencia_consulta"
                                HeaderText="TIPO DILIGENCIA">
                                <ItemStyle HorizontalAlign="center" Width="420px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="atendio" HeaderText="ATENDIO">
                                <ItemStyle HorizontalAlign="center" Width="120px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="acuerdo_consulta" HeaderText="ACUERDO">
                                <ItemStyle HorizontalAlign="center" Width="15px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="fecha_acuerdo" HeaderText="FECHA ACUERDO" DataFormatString="{0:d}">
                                <ItemStyle HorizontalAlign="center" Width="100px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="valor_acuerdo" HeaderText="VALOR ACUERDO"
                                DataFormatString="{0:C}">
                                <ItemStyle HorizontalAlign="Right" Width="100px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="respuesta" HeaderText="RESPUESTA">
                                <ItemStyle HorizontalAlign="center" Width="200px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="tipo_contacto_consulta" HeaderText="TIPO CONTACTO">
                                <ItemStyle HorizontalAlign="center" Width="80px" />
                            </asp:BoundField>

                        </Columns>
                        <HeaderStyle CssClass="gridHeader" />
                        <PagerStyle CssClass="gridPager" />
                        <RowStyle CssClass="gridItem" />
                    </asp:GridView>
                </div>
            </div>
            <div class="align-rt" />
            <table style="width: 100%;">
                <tr>
                    <td align="center" colspan="9">
                        <br />
                    </td>
                </tr>
                <tr>
                    <td align="center" style="width: 139px">
                        <asp:Label ID="lblTotalRegs" runat="server" />
                    </td>
                    <td align="center" style="width: 140px">&nbsp;</td>
                    <td align="center" style="width: 92px">&nbsp;</td>
                    <td align="center" style="width: 174px">&nbsp;</td>
                    <td align="center" class="logo" style="width: 121px">&nbsp;</td>
                    <td align="center">&nbsp;</td>
                    <td align="center">&nbsp;</td>
                    <td align="center">&nbsp;</td>
                    <td align="center">&nbsp;</td>
                </tr>
            </table>
            <br />
            <br />
            &nbsp;
        </asp:View>
        <asp:View ID="GvListaDiligencias" runat="server">
            <div style="overflow: scroll; height: 272px; width: 1000px;">
            </div>
            <div style="width: 1500px;">

                <asp:Button ID="BtnExportarDilig" runat="server" CssClass="btn8"
                    OnClick="BtnExportarDilig_Click" Text="Exportar a excel" />

                <asp:GridView ID="GvDiligencias" runat="server"
                    AutoGenerateColumns="False" DataKeyNames="cod_oficina"
                    GridLines="Horizontal" HeaderStyle-CssClass="gridHeader"
                    PagerStyle-CssClass="gridPager" RowStyle-CssClass="gridItem"
                    Width="65%" Style="font-size: xx-small" ShowFooter="True"
                    OnRowDataBound="GvDiligencias_RowDataBound"
                    OnPageIndexChanged="GvDiligencias_PageIndexChanged"
                    OnPageIndexChanging="GvDiligencias_PageIndexChanging"
                    OnSelectedIndexChanged="GvDiligencias_SelectedIndexChanged">
                    <Columns>
                        <asp:HyperLinkField DataNavigateUrlFields="NUMERO_RADICACION"
                            DataNavigateUrlFormatString="..//../Recuperacion/Detalle.aspx?radicado={0}"
                            DataTextField="NUMERO_RADICACION" HeaderText="NUMERO RADICACION"
                            Target="_blank" />
                        <asp:TemplateField HeaderStyle-CssClass="gridIco" ItemStyle-CssClass="gridIco">
                            <HeaderStyle CssClass="gridIco" />
                            <ItemStyle CssClass="gridIco" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="identificacion" HeaderText="IDENTIFICACION"></asp:BoundField>
                        <asp:BoundField DataField="nombres" HeaderText="NOMBRES">
                            <ItemStyle HorizontalAlign="center" Width="300px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="fecha_diligencia" HeaderText="FECHA DILIGENCIA" DataFormatString="{0:d}">
                            <ItemStyle HorizontalAlign="center" Width="100px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="tipo_diligencia_consulta"
                            HeaderText="TIPO DILIGENCIA">
                            <ItemStyle HorizontalAlign="center" Width="420px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="atendio" HeaderText="ATENDIO">
                            <ItemStyle HorizontalAlign="center" Width="120px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="acuerdo_consulta" HeaderText="ACUERDO">
                            <ItemStyle HorizontalAlign="center" Width="15px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="fecha_acuerdo" HeaderText="FECHA ACUERDO" DataFormatString="{0:d}">
                            <ItemStyle HorizontalAlign="center" Width="100px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="valor_acuerdo" HeaderText="VALOR ACUERDO"
                            DataFormatString="{0:C}">
                            <ItemStyle HorizontalAlign="Right" Width="100px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="respuesta" HeaderText="RESPUESTA">
                            <ItemStyle HorizontalAlign="center" Width="200px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="tipo_contacto_consulta" HeaderText="TIPO CONTACTO">
                            <ItemStyle HorizontalAlign="center" Width="80px" />
                        </asp:BoundField>

                    </Columns>
                    <HeaderStyle CssClass="gridHeader" />
                    <PagerStyle CssClass="gridPager" />
                    <RowStyle CssClass="gridItem" />
                </asp:GridView>
            </div>
            <div class="align-rt" />
            <table style="width: 100%;">
                <tr>
                    <td align="center" colspan="9">
                        <br />
                    </td>
                </tr>
                <tr>
                    <td align="center" style="width: 139px">
                        <asp:Label ID="lblTotalRegs0" runat="server" />
                    </td>
                    <td align="center" style="width: 140px">&nbsp;</td>
                    <td align="center" style="width: 92px">&nbsp;</td>
                    <td align="center" style="width: 174px">&nbsp;</td>
                    <td align="center" class="logo" style="width: 121px">&nbsp;</td>
                    <td align="center">&nbsp;</td>
                    <td align="center">&nbsp;</td>
                    <td align="center">&nbsp;</td>
                    <td align="center">&nbsp;</td>
                </tr>
            </table>
            <br />
            <br />
            &nbsp;
        </asp:View>

    </asp:MultiView>

</asp:Content>

