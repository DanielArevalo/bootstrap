<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" Title=".: Xpinn - Balance Comprobacion :." %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../../../General/Controles/fecha.ascx" TagName="fecha" TagPrefix="uc1" %>
<%@ Register src="../../../General/Controles/ctlMoneda.ascx" tagname="ddlMoneda" tagprefix="ctl" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
    <script type="text/javascript" src="../../../Scripts/gridviewScroll.min.js"></script>   

    <script type="text/javascript">
        $(document).ready(function () {
            gridviewScroll();
        });

        function gridviewScroll() {
            $('#<%=gvLista.ClientID%>').gridviewScroll({
                width: CalcularAncho(),
                height: 500,
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
                return screen.width - 300;
            }
            else {
                return 1000;
            }
        }
    </script>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <asp:MultiView ID="mvBalance" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwDatos" runat="server">
            <asp:CheckBox ID="chkGenerar" runat="server" AutoPostBack="True" Checked="True" 
                oncheckedchanged="chkCuentasCero_CheckedChanged" 
                style="font-size: x-small; text-align: left;" Visible="False" />
            <br />
            <asp:Panel ID="pConsulta" runat="server">
                <table id="tbCriterios" border="0" cellpadding="0" cellspacing="0" width="60%">
                    <tr >            
                        <td class="tdI" style="text-align:left" height="5">
                            Fecha de corte</td>
                        <td class="tdD" colspan="2" style="text-align:left" height="5">
                            <asp:Label ID="lblCentroCosto" runat="server" Text="Centro Costo"></asp:Label></td>
                        <td class="tdD" colspan="2" height="5" style="text-align:left">
                            Nivel&nbsp;</td>
                        <td class="tdD" height="5" colspan="2">
                            <asp:Label ID="lblMoneda0" runat="server" Text="Moneda"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdI" style="text-align:left">
                            <asp:DropDownList ID="ddlFechaCorte" runat="server" CssClass="dropdown" 
                                Width="158px" AutoPostBack="True" 
                                onselectedindexchanged="ddlFechaCorte_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td class="tdD" style="text-align:left" colspan="2">
                            <asp:DropDownList ID="ddlcentrocosto" runat="server" CssClass="dropdown" 
                                Width="168px" />
                        </td>
                        <td class="tdD" style="text-align:left">
                            <asp:DropDownList ID="ddlNivel" runat="server" CssClass="dropdown" 
                                Width="113px">
                                <asp:ListItem>3</asp:ListItem>
                                <asp:ListItem>4</asp:ListItem>
                                <asp:ListItem>5</asp:ListItem>
                                <asp:ListItem>6</asp:ListItem>
                                <asp:ListItem>7</asp:ListItem>
                                <asp:ListItem>8</asp:ListItem>
                                <asp:ListItem>9</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td class="tdD" colspan="3" style="text-align:left">
                            <ctl:ddlMoneda ID="ddlMoneda" runat="server" CssClass="dropdown" Width="151px" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tdI" style="text-align:left">
                            <asp:CheckBox ID="chkCuentasCero" runat="server" AutoPostBack="True" 
                                oncheckedchanged="chkCuentasCero_CheckedChanged" 
                                style="font-size: x-small; text-align: left;" Text="Mostrar Cuentas En Cero" />
                        </td>
                        <td class="tdD" style="text-align:left">

                        </td>
                        <td class="tdD" colspan="5" style="text-align:left">
                            <asp:CheckBox ID="chkmes13" runat="server" AutoPostBack="True" 
                                oncheckedchanged="chkmes13_CheckedChanged" style="font-size: x-small" 
                                Text="Mes 13" Visible="False" />
                            <strong>
                                <asp:Label ID="Lblmensaje" runat="server" ForeColor="Red" 
                                style="font-size: x-small"></asp:Label>
                            </strong>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdI" colspan="5">                                    
                            <asp:Button ID="btnInforme" runat="server" CssClass="btn8" 
                                OnClick="btnInforme_Click" OnClientClick="btnInforme_Click" 
                                Text="Visualizar informe" />
                            &nbsp;<asp:Button ID="btnExportar" runat="server" CssClass="btn8" 
                                onclick="btnExportar_Click" Text="Exportar a Excel" />
                            <br/>
                        </td>
                        <td class="tdD">
                        </td>
                        <td class="tdD">
                        &nbsp;</td>
                    </tr>
                </table>
            </asp:Panel>
            <table border="0" cellpadding="0" cellspacing="0" >
                <tr>
                    <td>
                        <hr style="width: 100%; text-align: left" />                        
                    </td>
                </tr>
                <tr>
                    <td>                                
                        <asp:GridView ID="gvLista" runat="server" 
                            AutoGenerateColumns="False" AllowPaging="False" 
                            OnPageIndexChanging="gvLista_PageIndexChanging" PageSize="20" 
                            HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager" 
                            RowStyle-CssClass="gridItem" Width="100%" style="font-size: xx-small" OnRowDataBound="gvLista_RowDataBound" OnSelectedIndexChanged="gvLista_SelectedIndexChanged" ShowFooter="True" >
                            <Columns>                        
                                <asp:BoundField DataField="cod_cuenta" HeaderText="Cod.Cuenta" >
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="nombrecuenta" HeaderText="Nombre Cuenta" >
                                    <ItemStyle HorizontalAlign="Left" Width="400px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="saldo_inicial" HeaderText="Saldo Inicial" DataFormatString="{0:c2}" >
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="debitos" HeaderText="Debitos" DataFormatString="{0:c2}" >
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="creditos" HeaderText="Creditos" DataFormatString="{0:c2}" >
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="saldo_final" HeaderText="Saldo Final" DataFormatString="{0:c2}" >
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                            </Columns>
                            <HeaderStyle CssClass="gridHeader" />
                            <PagerStyle CssClass="gridPager" />
                            <RowStyle CssClass="gridItem" />
                        </asp:GridView>
                        <asp:Label ID="lblTotalRegs" runat="server" Visible="False"/>
                        &nbsp;
                        </td>
                </tr>
            </table>
        </asp:View>                  
        <asp:View ID="vwReporte" runat="server">  
            <br /><br />&nbsp;
            <asp:Button ID="btnDatos" runat="server" CssClass="btn8" 
                onclick="btnDatos_Click" Text="Visualizar Datos" />       
            <rsweb:ReportViewer ID="RptReporte" runat="server" Width="100%" Height="500px">
            <localreport reportpath="Page\Contabilidad\BalanceComprobacion\RptbalanceComprobacion.rdlc"></localreport></rsweb:ReportViewer>         
        </asp:View>
          </asp:MultiView> 
   
</asp:Content>