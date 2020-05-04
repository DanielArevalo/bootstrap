<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="ctlmensaje" TagPrefix="uc2" %>
<%@ Register Src="~/General/Controles/ctlProcesoContable.ascx" TagName="procesoContable" TagPrefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <script type="text/javascript">
        $(document).ready(function () {
            gridviewScroll();
        });

        function gridviewScroll() {
            $('#<%=gvLista.ClientID%>').gridviewScroll({
            width: 950,
            height: 500,
            freezesize: 3,
            arrowsize: 30,
            varrowtopimg: "../../../Images/arrowvt.png",
            varrowbottomimg: "../../../Images/arrowvb.png",
            harrowleftimg: "../../../Images/arrowhl.png",
            harrowrightimg: "../../../Images/arrowhr.png"
        });
    }
    </script>
    <asp:Panel ID="panelGeneral" runat="server">
        <asp:Panel ID="pDatos" runat="server">
            <table style="width: 90%" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="text-align: left; width: 25%"></td>
                </tr>
                <tr>
                    <td style="text-align: left; width:400px">Empresa Recaudadora<br />
                        <asp:DropDownList ID="ddlEmpresa" runat="server" Enabled="false" Width="250px" />
                    </td>
                    <td style="text-align: left; width: 2%"><br/></td>
                    <td style="text-align: left; width:200px">Fecha de Aplicación<br />
                        <asp:TextBox ID="txtFecPeriodo" runat="server" Enabled="false" />
                    </td>
                    <td style="text-align: left;">Número de Novedad<br />
                        <asp:TextBox ID="txtNumRecaudo" runat="server" Enabled="false"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <br />
        <asp:Panel ID="pLista" runat="server">
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td colspan="3" style="width: 100%"><strong>Listado de cuentas</strong><br/>
                        <asp:GridView ID="gvLista" runat="server" AutoGenerateColumns="False"
                            HeaderStyle-CssClass="gridHeader" DataKeyNames="cod_persona" HorizontalAlign="Center"
                            PagerStyle-CssClass="gridPager" RowStyle-CssClass="gridItem" Style="font-size: small; width: 90%"
                            ShowHeaderWhenEmpty="True">
                            <Columns>
                                <asp:BoundField DataField="identificacion" HeaderText="Identificación" />
                                <asp:BoundField DataField="nombre" HeaderText="Nombre" />
                                <asp:BoundField DataField="numero_aporte" HeaderText="Número Ahorro" />
                                <asp:BoundField DataField="valor" HeaderText="Valor Interés" DataFormatString="{0:N0}" />
                            </Columns>
                            <HeaderStyle CssClass="gridHeader" />
                            <PagerStyle CssClass="gridPager" />
                            <RowStyle CssClass="gridItem" />
                        </asp:GridView>
                        <asp:Label ID="lblTotalRegs" runat="server" />
                    </td>
            </table>
        </asp:Panel>
    </asp:Panel>
    <asp:Panel ID="panelProceso" runat="server" Width="100%">
        <uc2:procesoContable ID="ctlproceso" runat="server" />
    </asp:Panel>
    <uc2:ctlmensaje ID="ctlmensaje" runat="server" />


</asp:Content>

