<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" Title=".: Xpinn - Listado de Tarjetas :." %>

<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/tnumero.ascx" TagName="numero" TagPrefix="ucnumero" %>


<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <script type="text/javascript" src="../../../Scripts/gridviewScroll.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            gridviewScroll();
        });

        function gridviewScroll() {
            $('#<%=gvLista.ClientID%>').gridviewScroll({
                width: CalcularAncho(),
                height: 400,
                freezesize: 1,
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
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <table style="width: 95%">
        <tr>
            <td style="text-align: left; width: 150px">Oficina<br />
                <asp:DropDownList ID="ddloficina" runat="server" CssClass="textbox"
                    Width="95%" />
            </td>
            <td style="text-align: left;">F. Asignación<br />
                <asp:TextBox ID="txtFechaApertura" runat="server" CssClass="textbox"
                    MaxLength="10" Width="100px" AutoPostBack="True"></asp:TextBox>
                <asp:CalendarExtender ID="ceFechaAperturan" runat="server"
                    DaysModeTitleFormat="dd/MM/yyyy" Enabled="True" Format="dd/MM/yyyy"
                    TargetControlID="txtFechaApertura" TodaysDateFormat="dd/MM/yyyy">
                </asp:CalendarExtender>
            </td>
            <td style="text-align: left;">Tipo&nbsp; de Cuenta<br />
                <asp:DropDownList ID="ddlTipoCuenta" runat="server" CssClass="textbox"
                    ReadOnly="True" Width="180px" />
            </td>
            <td style="text-align: left; width: 160px">Num Cuenta<br />
                <asp:TextBox ID="txtNumCuenta" runat="server" CssClass="textbox" Width="90%" />
            </td>
            <td style="text-align: left; width: 80px">Convenio<br />
                <asp:TextBox ID="txtConvenio" runat="server" CssClass="textbox" Width="90%" ReadOnly="true" />
            </td>
            <td style="text-align: left;">Estado tarjeta<br />
                <asp:DropDownList ID="ddlEstadoTarjeta" runat="server" CssClass="textbox" ReadOnly="True" Width="180px">
                    <asp:ListItem Value="0"> Pendiente </asp:ListItem>
                    <asp:ListItem Selected="True" Value="1"> Activa </asp:ListItem>
                    <asp:ListItem Value="2"> Inactiva </asp:ListItem>
                    <asp:ListItem Value="3"> Bloqueada </asp:ListItem>
                    <asp:ListItem Value=""> Todos </asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
    </table>

    <hr style="width: 100%" />

    <asp:Panel ID="panelGrilla" runat="server">
        <table style="width: 100%">
            <tr>
                <td style="text-align: left;">
                    <asp:Label ID="lbltitulo" runat="server" Width="100%" Text="Listado de Tarjetas (CLIENTES.MOV)." />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblError" runat="server" Width="100%" ForeColor="Red" />
                    <br />
                    <asp:GridView ID="gvLista" runat="server" Width="100%" GridLines="Horizontal" AutoGenerateColumns="False"
                        AllowPaging="False" PageSize="20" HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager"
                        RowStyle-CssClass="gridItem">
                        <Columns>
                            <asp:BoundField DataField="estado" HeaderText="Estado" />
                            <asp:BoundField DataField="identificacion" HeaderText="NroDocumento">
                                <ItemStyle HorizontalAlign="left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="nombres" HeaderText="Nombres">
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="numtarjeta" HeaderText="Nro.Tarjeta" />
                            <asp:BoundField DataField="numero_cuenta" HeaderText="Nro.Cuenta" />
                            <asp:BoundField DataField="saldo_total" HeaderText="Saldo Total" DataFormatString="{0:n}">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="saldo_disponible" HeaderText="Saldo Disponible" DataFormatString="{0:n}">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="cupo_cajero" HeaderText="Tope Monto ATM" DataFormatString="{0:n}">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="transacciones_cajero" HeaderText="Tope Operaciones ATM" DataFormatString="{0:n}">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="cupo_datafono" HeaderText="Tope Monto POS" DataFormatString="{0:n}">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="transacciones_datafono" HeaderText="Tope Operaciones POS" DataFormatString="{0:n}">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                        </Columns>
                        <HeaderStyle CssClass="gridHeader"></HeaderStyle>
                        <PagerStyle CssClass="gridPager"></PagerStyle>
                        <RowStyle CssClass="gridItem"></RowStyle>
                    </asp:GridView>
                    <asp:Label ID="lblTotalRegs" runat="server" Visible="False" /><br />
                    <br />
                    <br />
                    <asp:TextBox ID="txtRespuesta" runat="server" Height="100px"
                        TextMode="MultiLine" Width="100%" BorderStyle="None" Font-Bold="True"
                        Font-Italic="True" Font-Size="Small"></asp:TextBox>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <br />
    <br />

    <uc4:mensajegrabar ID="ctlMensaje" runat="server" />


</asp:Content>
