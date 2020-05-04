<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" Title=".: Xpinn - Movimientos Tarjeta :." %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>
<%@ Register Src="~/General/Controles/tnumero.ascx" TagName="numero" TagPrefix="ucnumero" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
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
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <asp:Panel ID="panelGeneral" runat="server">
        <table style="width: 95%">
            <tr>
                <td style="text-align: left;">
                    Fecha<br />
                    <asp:TextBox ID="txtFecha" runat="server" CssClass="textbox" 
                        MaxLength="10" Width="90px" AutoPostBack="True"></asp:TextBox>
                    <asp:CalendarExtender ID="ceFechaAperturan" runat="server" 
                        DaysModeTitleFormat="dd/MM/yyyy" Enabled="True" Format="dd/MM/yyyy" 
                        TargetControlID="txtFecha" TodaysDateFormat="dd/MM/yyyy">
                    </asp:CalendarExtender>
                </td>
                <td>
                    &nbsp;&nbsp;
                </td>
                <td style="text-align: left;">
                </td>
                <td style="text-align: left; width:80px">
                  <asp:Label ID="lblNroCuenta" Text="Nro.Cuenta" runat="server" Visible="false"/>
                  <br />
                  <asp:TextBox ID="txtNroCuenta" runat="server" CssClass="textbox" Width="90%" Visible="false" />
                </td>
                <td style="text-align: left; width:80px">
                  Convenio<br />
                  <asp:TextBox ID="txtConvenio" runat="server" CssClass="textbox" Width="90%" ReadOnly="true" />
                </td>
                <td style="text-align: left; width:80px">
                  Host<br />
                  <asp:TextBox ID="txtIpAppliance" runat="server" CssClass="textbox" Width="90%" ReadOnly="true" />
                </td>
                <td style="text-align: left; width:140px">
                  <br />&nbsp;
                </td>
                <td style="text-align: left; width:140px">
                  <br />&nbsp;
                </td>
                <td style="text-align: left; width:80px">
                </td>
            </tr>
        </table>
            
        <hr style="width: 100%" />

        <asp:Panel ID="panelGrilla" runat="server">
            <table style="width: 90%">
                <tr>
                    <td>
                        <asp:Label ID="lblError" runat="server" Width="95%" ForeColor="Red" />
                        <br />
                        <asp:GridView ID="gvLista" runat="server" Width="95%" AutoGenerateColumns="False" visible="false"
                            AllowPaging="False" PageSize="20" HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager"
                            style="font-size: x-small" >
                            <Columns>     
                                <asp:BoundField DataField="cod_persona" HeaderText="Cod.Persona">
                                    <ItemStyle HorizontalAlign="left" />
                                </asp:BoundField>   
                                <asp:BoundField DataField="tipoIdentificacion" HeaderText="T.Ident.">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="identificacion" HeaderText="Identificación">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="nombre" HeaderText="Nombre" />
                                <asp:BoundField DataField="tipoProducto" HeaderText="T.Producto" />
                                <asp:BoundField DataField="numeroproducto" HeaderText="Número Producto">
                                    <ItemStyle HorizontalAlign="left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="tipoMov" HeaderText="Tipo Movimiento">
                                    <ItemStyle HorizontalAlign="left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="valor" HeaderText="Valor" DataFormatString="{0:n}" >
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="codOpe" HeaderText="Cod.Ope">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="observaciones" HeaderText="Observaciones" >
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="fecha_oper" HeaderText="Fecha Oper." />  
                                <asp:BoundField DataField="numero_tarjeta" HeaderText="Nùmero Tarjeta" >                                
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                            </Columns>
                            <HeaderStyle CssClass="gridHeader"></HeaderStyle>
                            <PagerStyle CssClass="gridPager"></PagerStyle>
                        </asp:GridView>
                        <asp:Label ID="lblTotalRegs" runat="server" Visible="False" /><br />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <br />
        <br />
    </asp:Panel>
    <uc4:mensajeGrabar ID="ctlMensaje" runat="server"/>

</asp:Content>
