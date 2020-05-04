<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Lista.aspx.cs" Inherits="Lista" %>

<%@ Register Src="~/General/Controles/ctlLineaAhorro.ascx" TagName="ddlLineaAhorro"
    TagPrefix="ctl" %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar"
    TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/ctlOficina.ascx" TagName="ddlOficina" TagPrefix="ctl" %>

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
    <asp:Panel ID="pConsulta" runat="server">
        <table cellpadding="5" cellspacing="0" style="width: 80%; margin-right: 0px;">
            <tr>
                <td style="text-align: left; height: 15px; width: 99px;">
                    Línea<br />
                    <asp:DropDownList ID="ddlLineaAhorro" runat="server" Width="200px " CssClass="textbox">
                        <asp:ListItem Value="0">Seleccione Un Item</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td style="height: 15px; text-align: left; width: 150px;">
                    Fecha Inicial<br />
                    <ucFecha:fecha ID="fechainicial" runat="server" AutoPostBack="True" CssClass="textbox"
                        MaxLength="1" ValidationGroup="vgGuardar" Width="148px" />
                </td>
                <td style="height: 15px; text-align: left; width: 150px;">
                    Fecha final<br />
                    <ucFecha:fecha ID="fechafinal" runat="server" AutoPostBack="True" CssClass="textbox"
                        MaxLength="1" ValidationGroup="vgGuardar" Width="148px" />
                </td>
                <td style="text-align: left; height: 15px; width: 99px;">
                    Oficina<br />
                    <asp:DropDownList ID="ddlOficina" runat="server" Width="200px " CssClass="textbox">
                    </asp:DropDownList>
                </td>
                <td style="text-align: left; height: 15px;" colspan="2">
                </td>
            </tr>
        </table>
    </asp:Panel>
    <table style="width: 100%">
        <tr>
            <td>
                <asp:Button ID="btnExportar" runat="server" CssClass="btn8" OnClick="btnExportar_Click"
                    Text="Exportar a Excel" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="gvLista" runat="server" Width="95%" AutoGenerateColumns="False"
                    AllowPaging="False" GridLines="Horizontal" ShowHeaderWhenEmpty="True" OnPageIndexChanging="gvLista_PageIndexChanging"
                    OnRowDeleting="gvLista_RowDeleting" OnRowEditing="gvLista_RowEditing" OnSelectedIndexChanged="gvLista_SelectedIndexChanged"
                    OnRowDataBound="gvLista_RowDataBound" DataKeyNames="numero_cuenta" Style="font-size: xx-small">
                    <Columns>
                        <asp:TemplateField HeaderStyle-CssClass="gridIco">
                            <HeaderStyle CssClass="gridIco"></HeaderStyle>
                        </asp:TemplateField>
                        <asp:BoundField DataField="numero_cuenta" HeaderText="Numero Cuenta">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="cod_linea_ahorro" HeaderText="Linea Ahorro">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="identificacion" HeaderText="identificación">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="nombres" HeaderText="nombre">
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="cod_oficina" HeaderText="Oficina">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="fecha_apertura" HeaderText="Fecha Operación" DataFormatString="{0:d}">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="saldo_inicial" HeaderText="Saldo Inicial" DataFormatString="{0:n0}">
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="deposito" HeaderText="Depositos" DataFormatString="{0:n0}">
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="retiro" HeaderText="Retiros" DataFormatString="{0:n0}">
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="interes" HeaderText="Intereses" DataFormatString="{0:n0}">
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="retencion" HeaderText="Retencion" DataFormatString="{0:n0}">
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="saldo_final" HeaderText="Saldo final" DataFormatString="{0:n0}">
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                    </Columns>
                    <HeaderStyle CssClass="gridHeader" />
                    <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                    <RowStyle CssClass="gridItem" />
                </asp:GridView>
                <asp:Label ID="lblTotalRegs" runat="server" Visible="False" />
                <asp:Label ID="lblInfo" runat="server" Text="Su consulta no obtuvo ningún resultado."
                    Visible="False" />
            </td>
        </tr>
    </table>
    <uc1:mensajegrabar ID="ctlMensaje" runat="server" />
</asp:Content>
