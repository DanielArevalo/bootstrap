<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" Title=".: Xpinn - Verificación Referencias :." %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:MultiView ID="MvControl" ActiveViewIndex="0" runat="server">
        <asp:View runat="server">
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td>
                        <asp:Panel ID="pConsulta" runat="server">
                            <table id="tbCriterios" border="0" cellpadding="0" cellspacing="0" width="70%">
                                <tr>
                                    <td class="tdI">&nbsp;</td>
                                    <td class="tdD">&nbsp;</td>
                                    <td class="tdD">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="tdI" style="text-align: left">Fecha Esperada De Ent.<br />
                                        <ucFecha:fecha ID="txtFecha" runat="server" style="text-align: left" Width="140px" />
                                    </td>
                                    <td class="tdD">&nbsp;</td>
                                    <td class="tdI" style="text-align: left">Identificación<br />
                                        <asp:TextBox ID="txtidentificacion" runat="server" CssClass="textbox" Width="120px">
                                        </asp:TextBox>
                                    </td>
                                    <td class="tdD">&nbsp;</td>
                                    <td class="tdI" style="text-align: left" visible="false"><%--Código de nómina--%><br />
                                        <asp:TextBox ID="txtCodigoNomina" runat="server" CssClass="textbox" Width="110px" Visible="false" />
                                        <br />
                                    </td>
                                    <td class="tdD">&nbsp;</td>
                                    <td class="tdD" style="text-align: left">Linea Credito:<br />
                                        <asp:DropDownList ID="ddlLineaCredito" runat="server" CssClass="dropdown" Width="150px">
                                        </asp:DropDownList>
                                        <br />
                                    </td>
                                    <td class="tdD">&nbsp;</td>
                                    <td class="tdI" style="text-align: left">Oficina &nbsp;<br />
                                        <asp:DropDownList ID="ddlOficina" runat="server" CssClass="dropdown" Width="150px">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="tdD">&nbsp;</td>
                                    <td class="tdI" style="text-align: left">Estado<br />
                                        <asp:DropDownList ID="ddlestado" runat="server" CssClass="dropdown" Width="150px">
                                            <asp:ListItem Value="-1">Seleccione un item</asp:ListItem>
                                            <asp:ListItem Value="1">Entregado</asp:ListItem>
                                            <asp:ListItem Value="0">Pendiente</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>

                                    <td class="tdD">&nbsp;</td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td>
                        <hr width="100%" noshade />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:GridView ID="gvLista" runat="server" Width="100%" GridLines="Horizontal"
                            AutoGenerateColumns="False" OnRowDataBound="gvLista_RowDataBound" Style="font-size: xx-small"
                            AllowPaging="True" OnPageIndexChanging="gvLista_PageIndexChanging" PageSize="20" HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager" RowStyle-CssClass="gridItem" DataKeyNames="numero_radicacion">
                            <Columns>
                                <asp:BoundField DataField="numero_radicacion" HeaderStyle-CssClass="gridColNo"
                                    ItemStyle-CssClass="gridColNo">
                                    <HeaderStyle CssClass="gridColNo"></HeaderStyle>
                                    <ItemStyle CssClass="gridColNo" HorizontalAlign="Left"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="numero_radicacion" HeaderText="Radicación">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="cod_linea_credio" HeaderText="Linea">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="iddocumento" HeaderText="Identificacion">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Nombre" HeaderText="Nombre">
                                    <ItemStyle HorizontalAlign="Left" Width="200px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="cod_nomina" HeaderText="Código de nómina" Visible="false">
                                    <ItemStyle HorizontalAlign="center" Width="70px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="fecha_solicitud" HeaderText="Fec. Solicitud" DataFormatString="{0:d}">
                                    <ItemStyle HorizontalAlign="left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="fechaentrega" HeaderText="Fecha" DataFormatString="{0:d}">
                                    <ItemStyle HorizontalAlign="left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="monto_solicitado" HeaderText="Monto" DataFormatString="{0:n0}">
                                    <ItemStyle HorizontalAlign="center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Nun_Cuoatas" HeaderText="Plazo">
                                    <ItemStyle HorizontalAlign="center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="estado_cre" HeaderText="Estado Cred.">
                                    <ItemStyle HorizontalAlign="center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="nom_tipo_documento" HeaderText="Tipo Documento">
                                    <ItemStyle HorizontalAlign="center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="fechaanexo" HeaderText="Fecha" DataFormatString="{0:d}">
                                    <ItemStyle HorizontalAlign="center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="descripcion" HeaderText="Descripcion">
                                    <ItemStyle HorizontalAlign="center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="estados" HeaderText="Estado Documentos Anexos">
                                    <ItemStyle HorizontalAlign="center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="fec_estimada_entrga" HeaderText="Fecha Estimada" DataFormatString="{0:d}">
                                    <ItemStyle HorizontalAlign="center" />
                                </asp:BoundField>
                            </Columns>
                        </asp:GridView>
                        <asp:Label ID="lblTotalRegs" runat="server" Visible="False" />
                    </td>
                </tr>
            </table>
        </asp:View>
        <asp:View ID="vwReporte" runat="server">
            <br />
            <br />
            <table border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        <hr width="100%" />
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        <rsweb:ReportViewer ID="rvReporte" runat="server" Font-Names="Verdana"
                            Font-Size="8pt" InteractiveDeviceInfos="(Colección)"
                            WaitMessageFont-Names="Verdana" WaitMessageFont-Size="10pt" Width="100%">
                            <LocalReport ReportPath="Page\FabricaCreditos\ReporteControlDocumentos\ReporteControlDoc.rdlc">
                                <DataSources>
                                    <rsweb:ReportDataSource />
                                </DataSources>
                            </LocalReport>
                        </rsweb:ReportViewer>
                    </td>
                </tr>
            </table>
        </asp:View>
    </asp:MultiView>
</asp:Content>
