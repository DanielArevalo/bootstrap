<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" %>

<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<%@ Register Src="~/General/Controles/tnumero.ascx" TagName="numero" TagPrefix="ucnumero" %>



<%@ Register Src="../../../General/Controles/ctlGiro.ascx" TagName="giro" TagPrefix="uc3" %>



<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <script type="text/javascript" src="../../../Scripts/gridviewScroll.min.js"></script>
    <script type="text/javascript">

        $(document).ready(function () {
            gridviewScroll();
        });

        function gridviewScroll() {
            $('#<%=gvLista.ClientID%>').gridviewScroll({
                width: CalcularAncho(),
                height: 500,
                freezesize: 3,
                arrowsize: 30,
                varrowtopimg: "../../../Images/arrowvt.png",
                varrowbottomimg: "../../../Images/arrowvb.png",
                harrowleftimg: "../../../Images/arrowhl.png",
                harrowrightimg: "../../../Images/arrowhr.png"
            });
        }

        function CalcularAncho() {
            if (navigator.platform == 'Win32') {
                return screen.width - 300;
            }
            else {
                return 1000;
            }
        }
    </script>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:MultiView ID="mvPrincipal" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwData" runat="server">
            <asp:Panel ID="pConsulta" runat="server">
                <table cellpadding="5" cellspacing="0" style="width: 30%">
                    <tr>
                        <td style="height: 15px; text-align: left;">Numero Cuenta<br />
                            <asp:TextBox ID="txtCodigo" runat="server" CssClass="textbox" OnTextChanged="txtCodigo_TextChanged" />
                        </td>
                        <td style="height: 15px; text-align: left;">Línea<br />
                            <asp:DropDownList ID="ddlLinea" runat="server" ClientIDMode="Static"
                                CssClass="textbox" Width="310px">
                            </asp:DropDownList>
                        </td>
                        <td style="height: 15px; text-align: left;">Fecha<br />
                            <ucFecha:fecha ID="txtFecha" runat="server" CssClass="textbox" Width="85px" />
                        </td>
                        <td style="height: 15px; text-align: left;">Fecha Aplicacion<br />
                            <ucFecha:fecha ID="txtFechaAplica" runat="server" CssClass="textbox" Width="85px" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <table style="width: 100%">
                <tr>
                    <td>
                        <asp:Button ID="btnExportar" runat="server" CssClass="btn8"
                            OnClick="btnExportar_Click" Text="Exportar a Excel" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Panel ID="panelGrilla" runat="server">
                            <asp:GridView ID="gvLista" runat="server" Width="85%" AutoGenerateColumns="False"
                                AllowPaging="False" GridLines="Horizontal" ShowHeaderWhenEmpty="True"
                                OnPageIndexChanging="gvLista_PageIndexChanging" OnSelectedIndexChanged="gvLista_SelectedIndexChanged"
                                DataKeyNames="NumeroCuenta"
                                Style="font-size: xx-small">
                                <Columns>
                                    <asp:BoundField DataField="NumeroCuenta" HeaderText="Número Cuenta">
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:BoundField>

                                    <asp:BoundField DataField="Cod_Usuario" HeaderText="Cód.Persona">
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:BoundField>

                                    <asp:BoundField DataField="identificacion" HeaderText="Identificación">
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:BoundField>

                                    <asp:BoundField DataField="Nombre" HeaderText="Nombres" Visible="False">
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:BoundField>

                                    <asp:BoundField DataField="Saldo" DataFormatString="{0:n}" HeaderText="Saldo Total">
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:BoundField>

                                    <asp:BoundField DataField="Tasa_interes" HeaderText="Tasa">
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:BoundField>

                                    <asp:BoundField DataField="dias" HeaderText="Días Liquid.">
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:BoundField>

                                    <asp:BoundField HeaderText="Interes" DataField="Interes" DataFormatString="{0:n}">
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:BoundField>

                                    <asp:BoundField HeaderText="Retención" DataField="Retefuente" DataFormatString="{0:n}">
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:BoundField>

                                    <asp:BoundField HeaderText="Interes Causado" DataField="Interescausado" DataFormatString="{0:n}">
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:BoundField>

                                    <asp:BoundField HeaderText="Retención Causada" DataField="retencion_causado" DataFormatString="{0:n}">
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:BoundField>

                                    <asp:BoundField DataField="fecha_int" HeaderText="Fec.Ult.Liquid" DataFormatString="{0:d}">
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:BoundField>

                                </Columns>
                                <HeaderStyle CssClass="gridHeader" />
                                <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                                <RowStyle CssClass="gridItem" />
                            </asp:GridView>
                            <asp:Label ID="lblTotalRegs" runat="server" Visible="False" />
                            <asp:Label ID="lblInfo" runat="server" Text="Su consulta no obtuvo ningún resultado." Visible="False" />
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td>
                        <uc3:giro ID="ctlGiro" runat="server" />
                    </td>
                </tr>
            </table>
        </asp:View>
        <asp:View ID="vwFinal" runat="server">
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
                    <td style="text-align: center; font-size: large; color: Red;">Se Grabaron los Datos Correctamente
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
                        <br />
                    </td>
                </tr>
            </table>
        </asp:View>

    </asp:MultiView>
    <uc4:mensajegrabar ID="ctlMensaje" runat="server" />
</asp:Content>
