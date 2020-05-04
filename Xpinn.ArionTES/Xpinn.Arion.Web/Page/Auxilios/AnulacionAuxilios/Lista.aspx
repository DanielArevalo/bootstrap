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
            <asp:Panel ID="pConsulta" runat="server">
                <br />
                <br />
                <table cellpadding="2" cellspacing="3">
                    <tr>
                        <td style="text-align: left;">Fecha Solicitud<br />
                            <ucFecha:fecha ID="txtFecha" runat="server" CssClass="textbox" />
                        </td>
                        <td style="text-align: left;">Linea De Auxilios<br />
                            <asp:DropDownList ID="ddllineaAuxilios" runat="server" CssClass="textbox" Width="150px" />
                        </td>
                        <td style="text-align: left;">Identificación<br />
                            <asp:TextBox ID="txtidentificacion" runat="server" CssClass="textbox" Width="120px" />
                        </td>
                        <td style="text-align: left;">Nombre<br />
                            <asp:TextBox ID="txtNombre" runat="server" CssClass="textbox" Width="150px" />
                        </td>
                        <td style="text-align: left;" visible="false"><%--Código de nómina--%><br />
                            <asp:TextBox ID="txtCodigoNomina" runat="server" CssClass="textbox" Width="150px" visible="false"/>
                        </td>
                        <td style="text-align: left;">Estado<br />
                            <asp:DropDownList ID="ddlestado" runat="server" CssClass="textbox" Width="190px" />
                        </td>
                    </tr>
                </table>
                <hr style="width: 100%" />
            </asp:Panel>
            <asp:Panel ID="panelGrilla" runat="server">
                <table style="width: 100%">
                    <tr>
                        <td>
                            <asp:GridView ID="gvLista" runat="server" EnableEventValidation="false" Width="99%"
                                GridLines="Horizontal" AutoGenerateColumns="False" PageSize="30" AllowPaging="True"
                                HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="width: 840px;" RowStyle-CssClass="gridItem"
                                DataKeyNames="numero_auxilio" OnPageIndexChanging="gvLista_PageIndexChanging"
                                OnSelectedIndexChanged="gvLista_SelectedIndexChanged" OnRowDeleting="gvLista_RowDeleting"
                                Style="font-size: x-small">
                                <Columns>
                                    <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/gr_elim.jpg"
                                        ShowDeleteButton="True" />
                                    <asp:BoundField DataField="numero_auxilio" HeaderText="Numero Auxilio">
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="nom_linea" HeaderText="Linea">
                                        <ItemStyle HorizontalAlign="left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="fecha_solicitud" HeaderText="Fecha Solicitud" DataFormatString="{0:d}">
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="fecha_aprobacion" HeaderText="Fecha Aprobación" DataFormatString="{0:d}">
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="cod_persona" HeaderText="Códigu">
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="identificacion" HeaderText="Identificación">
                                        <ItemStyle HorizontalAlign="left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="nombre" HeaderText="Nombres">
                                        <ItemStyle HorizontalAlign="left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="cod_nomina" HeaderText="Código de nómina" visible="false">
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
                                    <asp:BoundField DataField="descripcion" HeaderText="Estado">
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
        <asp:View ID="vwFinal" runat="server">
            <table style="width: 100%;">
                <tr>
                    <td style="text-align: center; font-size: large;" colspan="3">
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        El Auxilio Nro :&nbsp;<asp:Label ID="lblNro" runat="server" />&nbsp; fue Anulado correctamente.
                    </td>
                </tr>
            </table>
        </asp:View>
    </asp:MultiView>
    <br />
    <br />
    <uc4:mensajegrabar ID="ctlMensaje" runat="server" />
</asp:Content>
