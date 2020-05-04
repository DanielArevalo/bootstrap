<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" Title=".: Xpinn - Conciliacion Tarjeta :." %>

<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/tnumero.ascx" TagName="numero" TagPrefix="ucnumero" %>
<%@ Register Src="~/General/Controles/ctlProcesoContable.ascx" TagName="procesoContable" TagPrefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
    <script type="text/javascript" src="../../../Scripts/gridviewScroll.min.js"></script>   
    <script type="text/javascript">
        $(document).ready(function () {
            gridviewScroll();
        });

        function gridviewScroll() {
            $('#<%=gvLista.ClientID%>').gridviewScroll({
                width: CalcularAncho(),
                height: 600,
                arrowsize: 30,
                varrowtopimg: "../../../Images/arrowvt.png",
                varrowbottomimg: "../../../Images/arrowvb.png",
                harrowleftimg: "../../../Images/arrowhl.png",
                harrowrightimg: "../../../Images/arrowhr.png"
            });
        }

        function CalcularAncho() {
            if (navigator.platform == 'Win32') {
                return screen.width - 370;
            }
            else {
                return 1000;
            }
        }
    </script>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <table>
        <tr>
            <td style="text-align: left;">
                Fecha Inicial<br />
                <asp:TextBox ID="txtFecha" runat="server" CssClass="textbox" 
                    MaxLength="10" Width="100px" AutoPostBack="True"></asp:TextBox>
                <asp:CalendarExtender ID="ceFechaAperturan" runat="server" 
                    DaysModeTitleFormat="dd/MM/yyyy" Enabled="True" Format="dd/MM/yyyy" 
                    TargetControlID="txtFecha" TodaysDateFormat="dd/MM/yyyy">
                </asp:CalendarExtender>
            </td>
            <td style="text-align: left;">
                Fecha Final<br />
                <asp:TextBox ID="txtFechaFinal" runat="server" CssClass="textbox" 
                    MaxLength="10" Width="100px" AutoPostBack="True"></asp:TextBox>
                <asp:CalendarExtender ID="ceFechaFinal" runat="server" 
                    DaysModeTitleFormat="dd/MM/yyyy" Enabled="True" Format="dd/MM/yyyy" 
                    TargetControlID="txtFechaFinal" TodaysDateFormat="dd/MM/yyyy">
                </asp:CalendarExtender>
            </td>
            <td style="text-align: left; width:80px">
                <asp:CheckBox ID="cbdetallado" runat="server" Checked="false" Text="Detallado" />
            </td>
            <td style="text-align: left; width:80px">
                Convenio<br />
                <asp:TextBox ID="txtConvenio" runat="server" CssClass="textbox" Width="90%" ReadOnly="true" />
            </td>
            <td style="text-align: left; width:80px">
                Host<br />
                <asp:TextBox ID="txtIpAppliance" runat="server" CssClass="textbox" Width="90%" ReadOnly="true" />
            </td>
            <td>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            </td>
        </tr>
    </table>
            
    <hr style="width: 100%" />

    <asp:Panel ID="panelGrilla" runat="server">
        <table style="width: 50%">
            <tr>
                <td>
                    <asp:Label ID="lblError" runat="server" Width="95%" ForeColor="Red" />
                    <br />
                    <asp:GridView ID="gvLista" runat="server" Width="95%" GridLines="Horizontal" AutoGenerateColumns="False" 
                        AllowPaging="False" PageSize="30" HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager"
                        style="font-size: x-small" >
                        <Columns>         
                            <asp:BoundField DataField="num_comp_apl" HeaderText="Num.Comp." />
                            <asp:BoundField DataField="tipo_comp_apl" HeaderText="T.Comp." />      
                            <asp:BoundField DataField="fecha_movimiento" HeaderText="Fecha Mov." DataFormatString="{0:dd/MM/yyyy}" />
                            <asp:BoundField DataField="monto_ath" HeaderText="Monto ATH" DataFormatString="{0:n}" >                                
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="monto_pos" HeaderText="Monto POS" DataFormatString="{0:n}" >                                
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="monto_plastico" HeaderText="Monto Plastico" DataFormatString="{0:n}" >                                
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="monto" HeaderText="Monto Total" DataFormatString="{0:n}" >                                
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="comision" HeaderText="Comisión" DataFormatString="{0:n}" >                                
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="saldo_total" HeaderText="Total" DataFormatString="{0:n}" >                                
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="valor_apl" HeaderText="Vr.Aplicado" DataFormatString="{0:n}" >                                
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="comision_apl" HeaderText="Vr.Bancos" DataFormatString="{0:n}" >                                
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField> 
                            <asp:BoundField DataField="cuenta_porcobrar" HeaderText="Vr.C x C" DataFormatString="{0:n}" >                                
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField> 
                            <%--<asp:BoundField DataField="num_tran_verifica" HeaderText="Num.Tran.Tar." />--%>
                        </Columns>
                        <HeaderStyle CssClass="gridHeader"></HeaderStyle>
                        <PagerStyle CssClass="gridPager"></PagerStyle>
                    </asp:GridView>
                    <asp:Label ID="lblTotalRegs" runat="server" Visible="False" /><br /><br /><br />
                    <asp:TextBox ID="txtRespuesta" runat="server" Height="100px" 
                        TextMode="MultiLine" Width="95%" BorderStyle="None" Font-Bold="True" 
                        Font-Italic="True" Font-Size="Small"></asp:TextBox>
                    <asp:TextBox ID="txtpRequestXmlString" runat="server" Height="100px" visible="false"
                        TextMode="MultiLine" Width="95%" BorderStyle="None" Font-Bold="True" 
                        Font-Italic="True" Font-Size="Small"></asp:TextBox>
                </td>
            </tr>
        </table>
    </asp:Panel>


</asp:Content>
