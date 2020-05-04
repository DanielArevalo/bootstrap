<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" Title=".: Xpinn - Desembolso Masivo :." %>

<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<%@ Register Src="~/General/Controles/tnumero.ascx" TagName="numero" TagPrefix="ucnumero" %>


<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <script type="text/javascript" src="../../../Scripts/gridviewScroll.min.js"></script>   
    <script type="text/javascript">
        $(document).ready(function () {
            gridviewScroll();
        });

        function gridviewScroll() {
            $('#<%=gvLista.ClientID%>').gridviewScroll({
                width: CalcularAncho(),
                height: 400,
                arrowsize: 30,
                varrowtopimg: "../../../Images/arrowvt.png",
                varrowbottomimg: "../../../Images/arrowvb.png",
                harrowleftimg: "../../../Images/arrowhl.png",
                harrowrightimg: "../../../Images/arrowhr.png"
            });
        }

        function CalcularAncho() {
            if (navigator.platform == 'Win32') {
                return screen.width - 330;
            }
            else {
                return 1000;
            }
        }
    </script>

    <table style="width: 985px">
        <tr>
            <td style="width: 120px; text-align: left">Num.Tarjeta<br />
                <asp:TextBox ID="txtNumTarjeta" runat="server" CssClass="textbox" Width="90%" />
            </td>
            <td style="text-align: left; width: 180px">Oficina<br />
                <asp:DropDownList ID="ddloficina" runat="server" CssClass="textbox"
                    Width="95%" />
            </td>
            <td style="text-align: left; width: 180px">F. Asignacion<br />
                <asp:TextBox ID="txtFechaAsignacion" runat="server" CssClass="textbox"
                    MaxLength="10" Width="80%" AutoPostBack="True"></asp:TextBox>
                <asp:CalendarExtender ID="ceFechaAsignacion" runat="server"
                    DaysModeTitleFormat="dd/MM/yyyy" Enabled="True" Format="dd/MM/yyyy"
                    TargetControlID="txtFechaAsignacion" TodaysDateFormat="dd/MM/yyyy">
                </asp:CalendarExtender>
            </td>
            <td style="text-align: left; width: 180px">Tipo&nbsp; de Cuenta<br />
                <asp:DropDownList ID="ddlTipoCuenta" runat="server" CssClass="textbox"
                    ReadOnly="True" Width="180px" />
            </td>
            <td style="text-align: left; width: 160px">Num Cuenta<br />
                <asp:TextBox ID="txtNumCuenta" runat="server" CssClass="textbox" Width="90%" />
            </td>
        </tr>
        <tr>
            <td style="width: 120px; text-align: left">&nbsp;</td>
            <td style="text-align: left; width: 180px">&nbsp;</td>
            <td style="text-align: left; width: 180px">identificación
                <br />
                <asp:TextBox ID="txtIdentificacion" runat="server" CssClass="textbox"
                    Width="80%" />
            </td>
            <td style="text-align: left">Código de nómina<br />
                <asp:TextBox ID="txtCodigoNomina" runat="server" CssClass="textbox" Width="90%" />
            </td>
            <td style="text-align: left; width: 160px">Estado<br />
                <asp:DropDownList ID="ddlEstado" runat="server" CssClass="textbox" Width="95%" />
            </td>
            <td style="text-align: left; width: 160px">Estado Cupo<br />
                <asp:DropDownList ID="ddlEstadoCupo" runat="server" CssClass="textbox" Width="95%" />
            </td>
        </tr>
        <tr>
            <td colspan="6">
                <hr style="width: 100%" />
            </td>
        </tr>
    </table>

    <asp:Panel ID="panelGrilla" runat="server">
        <table style="width: 100%">
            <tr>
                <td>
                    <asp:GridView ID="gvLista" runat="server" Width="100%" GridLines="Horizontal" AutoGenerateColumns="False"
                        AllowPaging="False" OnPageIndexChanging="gvLista_PageIndexChanging" OnRowEditing="gvLista_RowEditing"
                        PageSize="100" HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager"
                        RowStyle-CssClass="gridItem" DataKeyNames="idtarjeta"
                        OnRowDeleting="gvLista_RowDeleting">
                        <Columns>
                            <asp:CommandField ButtonType="Image" EditImageUrl="~/Images/gr_edit.jpg"
                                ShowEditButton="True" />
                            <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/gr_elim.jpg"
                                ShowDeleteButton="True" />
                            <asp:BoundField DataField="idtarjeta" HeaderText="Id" />
                            <asp:BoundField DataField="numtarjeta" HeaderText="Num.Tarjeta">
                                <ItemStyle HorizontalAlign="left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="cod_convenio" HeaderText="Convenio">
                                <ItemStyle HorizontalAlign="left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="cod_oficina" HeaderText="Oficina">
                                <ItemStyle HorizontalAlign="left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="fecha_asignacion" HeaderText="F.Asignación"
                                DataFormatString="{0:d}">
                                <ItemStyle HorizontalAlign="left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="tipo_cuenta" HeaderText="Tipo De Cuenta">
                                <ItemStyle HorizontalAlign="left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="numero_cuenta" HeaderText="Num. Cuenta">
                                <ItemStyle HorizontalAlign="left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="identificacion" HeaderText="Identificacion" />
                            <asp:BoundField DataField="nombres" HeaderText="Nombres" />
                            <%--<asp:BoundField DataField="cod_nomina" HeaderText="Código de nómina" />                            --%>
                            <asp:BoundField DataField="saldo_total" HeaderText="Saldo Total" />
                            <asp:BoundField DataField="saldo_disponible" HeaderText="Saldo" />
                            <asp:BoundField DataField="cuota_manejo" HeaderText="Cuota" />
                            <asp:BoundField DataField="estado" HeaderText="Estado" />
                            <asp:BoundField DataField="estado_saldo" HeaderText="Estado Cupo" />
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
    <br />
    <br />

    <uc4:mensajegrabar ID="ctlMensaje" runat="server" />


</asp:Content>
