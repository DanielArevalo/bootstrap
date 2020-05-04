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
    <asp:MultiView ID="mvPrincipal" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwImportacion" runat="server">
            <table style="width: 95%">
                <tr>
                    <td style="text-align: left; width: 12.5%">Oficina<br />
                        <asp:DropDownList ID="ddloficina" runat="server" CssClass="textbox"
                            Width="95%" />
                    </td>
                    <td style="text-align: left; width: 14.5%">Tipo&nbsp; de Cuenta<br />
                        <asp:DropDownList ID="ddlTipoCuenta" runat="server" CssClass="textbox"
                            ReadOnly="True" Width="90%" />
                    </td>
                    <td style="text-align: left; width: 12.5%">Num Identificación<br />
                        <asp:TextBox ID="txtNumIdent" runat="server" CssClass="textbox" Width="90%" />
                    </td>
                    <td style="text-align: left; width: 12.5%">Num Cuenta<br />
                        <asp:TextBox ID="txtNumCuenta" runat="server" CssClass="textbox" Width="90%" />
                    </td>
                    <td style="text-align: left; width: 12.5%">
                        <asp:DropDownList ID="ddlEstadoCuenta" runat="server" CssClass="textbox" ReadOnly="True" Width="95%" Visible="false">
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
                            <br />
                            <asp:GridView ID="gvLista" runat="server" Width="100%" GridLines="Horizontal" AutoGenerateColumns="False"
                                AllowPaging="False" PageSize="20" HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager"
                                RowStyle-CssClass="gridItem">
                                <Columns>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="chkHeader" runat="server" AutoPostBack="true" OnCheckedChanged="Check_Clicked" />
                                        </HeaderTemplate>

                                        <ItemTemplate>
                                            <asp:CheckBox ID="CheckBoxgv" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
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


        </asp:View>
        <asp:View ID="vwFinal" runat="server">
            <table style="width: 100%;" id="tbnew">
                <tr>
                    <td style="text-align: center; font-size: large;">
                        <br />
                        <br />
                        <br />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center; font-size: large;">Las cuentas se agregaron correctamente.
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center; font-size: large;">
                        <br />
                        <br />
                        <br />
                    </td>
                </tr>
            </table>
        </asp:View>
    </asp:MultiView>
    <uc4:mensajegrabar ID="ctlMensaje" runat="server" />
</asp:Content>
