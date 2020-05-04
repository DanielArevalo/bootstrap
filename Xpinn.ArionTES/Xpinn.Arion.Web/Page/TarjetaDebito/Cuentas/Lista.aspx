<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" Title=".: Xpinn - Listado de Cuentas :." %>

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
            <td style="text-align: left; width:12.5%"><br />Oficina<br />
                <asp:DropDownList ID="ddloficina" runat="server" CssClass="textbox"
                    Width="95%" />
            </td>
            <td style="text-align: left; width:12.5%""><br />F. Apertura<br />
                <asp:TextBox ID="txtFechaApertura" runat="server" CssClass="textbox"
                    MaxLength="10" Width="90%" AutoPostBack="True"></asp:TextBox>
                <asp:CalendarExtender ID="ceFechaAperturan" runat="server"
                    DaysModeTitleFormat="dd/MM/yyyy" Enabled="True" Format="dd/MM/yyyy"
                    TargetControlID="txtFechaApertura" TodaysDateFormat="dd/MM/yyyy">
                </asp:CalendarExtender>
            </td>
            <td style="text-align: left; width:14.5%""><br />Tipo&nbsp; de Cuenta<br />
                <asp:DropDownList ID="ddlTipoCuenta" runat="server" CssClass="textbox"
                    ReadOnly="True" Width="90%" />
            </td>
            <td style="text-align: left; width:12.5%"><br />Num Cuenta<br />
                <asp:TextBox ID="txtNumCuenta" runat="server" CssClass="textbox" Width="90%" />
            </td>
            <td style="text-align: left; width: 5%"><br />Convenio<br />
                <asp:TextBox ID="txtConvenio" runat="server" CssClass="textbox" Width="90%" ReadOnly="true" />
            </td>
            <td style="text-align: left; width: 5%">Tipo <br />Convenio<br />
                <asp:TextBox ID="txtTipoConvenio" runat="server" CssClass="textbox" Width="90%" ReadOnly="true" />
            </td>
            <td style="text-align: left; width: 12.5%"><br />Host<br />
                <asp:TextBox ID="txtIpAppliance" runat="server" CssClass="textbox" Width="90%" ReadOnly="true" />
            </td>
            <td style="text-align: left; width: 18.5%"><br />Tipo de Archivo a Exportar<br />
                <asp:RadioButtonList ID="rbTipoArchivo" runat="server" RepeatDirection="Horizontal"
                    Width="95%">
                    <asp:ListItem Value="1">CLS</asp:ListItem>
                    <asp:ListItem Value="2">EXCEL</asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td style="text-align: left; width: 12.5%">Estado <br />Cuenta<br />
                <asp:DropDownList ID="ddlEstadoCuenta" runat="server" CssClass="textbox" ReadOnly="True" Width="95%">
                    <asp:ListItem Value="INACTIVA"> Inactiva </asp:ListItem>
                    <asp:ListItem Selected="True" Value="ACTIVA"> Activa </asp:ListItem>
                    <asp:ListItem Value=""> Todos </asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
    </table>

    <hr style="width: 100%" />

    <asp:Panel ID="panelGrilla" runat="server">
        <table style="width: 100%">
            <tr>
                <td>
                    <asp:Label ID="lblError" runat="server" Width="100%" ForeColor="Red" />
                    Nota: Generaciòn de Archivo para Refresco de Saldos
                    <br />
                    <asp:GridView ID="gvLista" runat="server" Width="100%" AutoGenerateColumns="False" AllowPaging="False" PageSize="20" >
                        <Columns>
                            <asp:BoundField DataField="cod_persona" HeaderText="Id" />
                            <asp:BoundField DataField="identificacion" HeaderText="Identificación">
                                <ItemStyle HorizontalAlign="left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="nombres" HeaderText="Nombres">
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="direccion" HeaderText="Oficina">
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="telefono" HeaderText="Telefóno">
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="email" HeaderText="E-Mail">
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="tipocuenta" HeaderText="Tipo de Cuenta">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="nrocuenta" HeaderText="Nro.Cuenta" />
                            <asp:BoundField DataField="saldodisponible" HeaderText="Saldo Disponible" DataFormatString="{0:n}">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="saldototal" HeaderText="Saldo Total" DataFormatString="{0:n}">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="fechasaldo" HeaderText="Fecha del Saldo" DataFormatString="{0:d}">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="estado" HeaderText="Estado Cuenta">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                        </Columns>
                        <HeaderStyle CssClass="gridHeader"></HeaderStyle>
                        <PagerStyle CssClass="gridPager"></PagerStyle>
                        <RowStyle CssClass="gridItem"></RowStyle>
                    </asp:GridView>
                    <br />
                    <br />
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
