<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" EnableEventValidation="false" CodeFile="Lista.aspx.cs" Inherits="Lista" Title=".: Xpinn - Desembolso Masivo :." %>

<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Src="~/General/Controles/tnumero.ascx" TagName="numero" TagPrefix="ucnumero" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <script type="text/javascript" src="../../../Scripts/gridviewScroll.min.js"></script>
    <script type="text/javascript">

          
    </script>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:MultiView ID="mvPrincipal" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwDatos" runat="server">
            <table style="width: 100%">
                <tr>
                    <td style="text-align: left;">Fecha Desembolso<br />
                        <ucFecha:fecha ID="txtFechaReporte" runat="server" CssClass="textbox" Width="100px" />
                    </td>
                    <td style="text-align: left;">Linea De Auxilios<br />
                        <asp:DropDownList ID="ddllineaAuxilios" runat="server" CssClass="textbox"
                            Width="95%" />
                    </td>
                    <td>Identificación<br />
                        <asp:TextBox ID="txtidentificacion" runat="server" CssClass="textbox"
                            Width="100px" />
                    </td>
                    <td>Nombre<br />
                        <asp:TextBox ID="txtNombre" runat="server" CssClass="textbox" Width="100px" />
                    </td>
                    <td style="text-align: left;" visible="false"><%--Código de nómina--%><br />
                        <asp:TextBox ID="txtCodigoNomina" runat="server" CssClass="textbox" Width="120px" visible="false"/>
                    </td>
                    <td>Periodo<br />
                        <ucFecha:fecha ID="txtMora1" runat="server" CssClass="textbox" Width="60px" />
                        <strong>a</strong>
                        <ucFecha:fecha ID="txtMora2" runat="server" CssClass="textbox" Width="50px" />
                    </td>
                    <td style="text-align: left; width: 100px">Estado<br />
                        <asp:DropDownList ID="ddlestado" runat="server" CssClass="textbox"
                            Width="100%" />
                    </td>
                </tr>
            </table>
            <asp:Panel ID="panelGrilla" runat="server" EnableEventValidation="false">
                <table style="width: 99%">
                    <tr>
                        <td>
                            <asp:GridView ID="gvLista" runat="server" EnableEventValidation="false" Width="99%"
                                GridLines="Horizontal" AutoGenerateColumns="False" PageSize="30" AllowPaging="true"
                                HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="width: 840px;" RowStyle-CssClass="gridItem"
                                DataKeyNames="numero_auxilio" OnPageIndexChanging="gvLista_PageIndexChanging" OnSelectedIndexChanged="gvLista_SelectedIndexChanged"
                                Style="font-size: x-small">
                                <Columns>
                                    <asp:BoundField DataField="numero_auxilio" HeaderText="Numero Auxilio">
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="descripcion" HeaderText="Linea">
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="fecha_solicitud" HeaderText="Fecha Solicitud" DataFormatString="{0:d}">
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="fecha_aprobacion" HeaderText="Fecha Aprobación" DataFormatString="{0:d}">
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="fecha_desembolso" HeaderText="Fecha Desembolso" DataFormatString="{0:d}">
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="identificacionPersona" HeaderText="Identificación">
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="nombre" HeaderText="Nombres" ControlStyle-Font-Size="Large">
                                        <ItemStyle HorizontalAlign="center" Width="280" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="valor_solicitado" HeaderText="Valor Solicitado" DataFormatString="{0:c}">
                                        <ItemStyle HorizontalAlign="Right" Width="280" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="valor_aprobado" HeaderText="Valor Aprobado" DataFormatString="{0:c}">
                                        <ItemStyle HorizontalAlign="Right" Width="280" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="valor_matricula" HeaderText="Valor Matricula" DataFormatString="{0:c}">
                                        <ItemStyle HorizontalAlign="Right" Width="280" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="estado" HeaderText="Estado">
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="identificacion" HeaderText="identificacion Beneficiario">
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="nombres" HeaderText="Nombre Benef">
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="descripciones" HeaderText="Parentesco">
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:BoundField>
                                </Columns>
                                <HeaderStyle CssClass="gridHeader"></HeaderStyle>
                                <PagerStyle CssClass="gridPager"></PagerStyle>
                                <RowStyle CssClass="gridItem"></RowStyle>
                            </asp:GridView>
                            <asp:Label ID="lblTotalRegs" runat="server" Visible="False" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </asp:View>
        <asp:View ID="vReporteExtracto" runat="server">
            <table>
                <tr>
                    <td style="width: 100%">
                        <br />
                        <br />
                        <rsweb:ReportViewer ID="rvExtracto" runat="server" Width="100%" Font-Names="Verdana"
                            Font-Size="8pt" InteractiveDeviceInfos="(Colección)" WaitMessageFont-Names="Verdana"
                            WaitMessageFont-Size="14pt" Height="500px">
                            <LocalReport ReportPath="Page\Auxilios\ReporteAuxilios\rptAuxilios.rdlc" EnableExternalImages="True">
                            </LocalReport>
                        </rsweb:ReportViewer>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;
                    </td>
                </tr>
            </table>
        </asp:View>
    </asp:MultiView>

    <asp:Panel ID="panelimpresion" runat="server" EnableEventValidation="false">
        <table style="width: 99%">


            <tr>
                <td>
                    <asp:GridView ID="gvImpresion" runat="server" EnableEventValidation="false" Width="99%"
                        GridLines="Horizontal" AutoGenerateColumns="False" PageSize="30" AllowPaging="true"
                        HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="width: 840px;" RowStyle-CssClass="gridItem"
                        DataKeyNames="numero_auxilio" OnPageIndexChanging="gvLista_PageIndexChanging" OnSelectedIndexChanged="gvLista_SelectedIndexChanged"
                        Style="font-size: x-small">
                        <Columns>
                            <asp:BoundField DataField="numero_auxilio" HeaderText="Numero Auxilio">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="descripcion" HeaderText="Linea">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="fecha_solicitud" HeaderText="Fecha Solicitud" DataFormatString="{0:d}">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="fecha_aprobacion" HeaderText="Fecha Aprobación" DataFormatString="{0:d}">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="fecha_desembolso" HeaderText="Fecha Desembolso" DataFormatString="{0:d}">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="identificacionPersona" HeaderText="Identificación">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="nombre" HeaderText="Nombres" ControlStyle-Font-Size="Large">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="valor_solicitado" HeaderText="Valor Solicitado" DataFormatString="{0:c}">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="valor_aprobado" HeaderText="Valor Aprobado" DataFormatString="{0:c}">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="valor_matricula" HeaderText="Valor Matricula" DataFormatString="{0:c}">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="estado" HeaderText="Estado">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="identificacion" HeaderText="identificacion Beneficiario">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="nombres" HeaderText="Nombre Benef">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="cod_parentesco" HeaderText="Parentesco">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                        </Columns>
                        <HeaderStyle CssClass="gridHeader"></HeaderStyle>
                        <PagerStyle CssClass="gridPager"></PagerStyle>
                        <RowStyle CssClass="gridItem"></RowStyle>
                    </asp:GridView>
                    <asp:Label ID="Label1" runat="server" Visible="False" />
                </td>


            </tr>
        </table>
    </asp:Panel>
    <br />
    <br />
    <uc4:mensajegrabar ID="ctlMensaje" runat="server" />
</asp:Content>
