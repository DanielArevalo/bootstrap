<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" Title=".: Xpinn - Usuario :." %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register src="../../../General/Controles/fecha.ascx" tagname="fecha" tagprefix="uc" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<%@ Register src="../../../General/Controles/ctlMoneda.ascx" tagname="ddlMoneda" tagprefix="ctl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
    <script type="text/javascript" src="../../../Scripts/gridviewScroll.min.js"></script>   

    <script type="text/javascript">

        $(document).ready(function () {
            gridviewScroll();
        });

        function gridviewScroll() {
            $('#<%=gvLista.ClientID%>').gridviewScroll({
                width: 1200,
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
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <asp:MultiView ID="mvBalanceComp" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwDatos" runat="server">
            <asp:Panel ID="pConsulta" runat="server">
                <table id="tbCriterios" border="0" cellpadding="0" cellspacing="0" width="50%">
                    <tr >            
                        <td class="tdI" style="text-align:left" colspan="2">
                            Centro De Costo</td>
                        <td class="tdD" colspan="4" style="text-align:left">
                            Nivel</td>
                    </tr>
                    <tr>
                        <td class="tdI" style="text-align:left; height: 30px;" colspan="2">
                            <asp:DropDownList ID="ddlcentrocosto" Height="30px" runat="server" CssClass="dropdown" 
                                Width="168px" />
                        </td>
                        <td class="tdD" style="text-align:left; height: 30px;" colspan="2">
                            <asp:DropDownList ID="ddlNivel" Height="30px" runat="server" CssClass="dropdown" 
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
                        <td class="tdD" colspan="2" style="text-align:left; height: 21px;">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="tdD" style="text-align:left">
                            Comparar Fecha<br />
                            <asp:DropDownList ID="ddlFecha1" Height="30px" runat="server" CssClass="dropdown" 
                                Width="158px">
                            </asp:DropDownList>
                        </td>
                        <td class="tdD" style="text-align:left">
                            Con Fecha<br />
                            <asp:DropDownList ID="ddlFecha2" Height="30px" runat="server" CssClass="dropdown" 
                                Width="158px">
                            </asp:DropDownList>
                        </td>
                        <td class="tdD" style="text-align:left">
                            Y con Fecha<br />
                            <asp:DropDownList ID="ddlFechaa3" runat="server" Height="30px" AutoPostBack="True" 
                                CssClass="dropdown" onselectedindexchanged="ddlFechaa3_SelectedIndexChanged" Width="158px">
                            </asp:DropDownList>
                            <br />
                        </td>                       
                        <td class="tdD" style="text-align:left" colspan="2">
                            Moneda<br />
                            <ctl:ddlMoneda ID="ddlMoneda" runat="server" CssClass="dropdown" Width="151px" />
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td class="tdI" style="text-align:left">                                    
                            <asp:Button ID="btnInforme" runat="server" CssClass="btn8" 
                                OnClick="btnInforme_Click" OnClientClick="btnInforme_Click" 
                                Text="Visualizar informe" />
                            &nbsp;<br/>
                        </td>
                        <td class="tdI" colspan="2" style="text-align:left"">
                            <asp:Button ID="btnExportar" runat="server" CssClass="btn8" 
                                onclick="btnExportar_Click" Text="Exportar a Excel" />
                        </td>
                        <td class="tdI" colspan="3" style="text-align:left">
                            <asp:Label ID="Lblerror" runat="server" style="color: #FF0000"></asp:Label>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <table border="0" cellpadding="0" cellspacing="0" >
                <tr>
                    <td><hr style="width: 100%; text-align: left" /></td>
                </tr>
                <tr>
                    <td>                                
                        <asp:GridView ID="gvLista" runat="server" 
                            AutoGenerateColumns="False" AllowPaging="False" 
                            OnPageIndexChanging="gvLista_PageIndexChanging" PageSize="20" 
                            HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager" 
                            RowStyle-CssClass="gridItem" Width="98%" 
                            style="font-size: xx-small" >
                            <Columns>                        
                                <asp:BoundField DataField="cod_cuenta" HeaderText="Cod.Cuenta" >
                                    <ItemStyle HorizontalAlign="Left" Width="80px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="nombrecuenta" HeaderText="Nom. Cuenta" >
                                <ItemStyle HorizontalAlign="Left" Width="200px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="balance1" HeaderText="Balance 1" 
                                    DataFormatString="{0:c}" >
                                        <ItemStyle HorizontalAlign="Right" Width="100px" />
                                    </asp:BoundField>                                    
                                <asp:BoundField DataField="porcpart1" HeaderText="% Partic." 
                                    DataFormatString="{0:P1}">
                                        <ItemStyle HorizontalAlign="Right" Width="50px" />
                                    </asp:BoundField>
                                <asp:BoundField DataField="balance2" HeaderText="Balance2" 
                                    DataFormatString="{0:c}">
                                    <ItemStyle HorizontalAlign="Right" Width="100px" />
                                    </asp:BoundField>
                                <asp:BoundField DataField="porcpart2" DataFormatString="{0:P1}" 
                                    HeaderText="% Partic.">
                                <ItemStyle HorizontalAlign="Right" Width="50px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="diferencia" DataFormatString="{0:c}" 
                                HeaderText="Diferencia">
                                <ItemStyle HorizontalAlign="Right" Width="100px" />
                                    </asp:BoundField>
                                <asp:BoundField DataField="porcdif" DataFormatString="{0:P1}" 
                                    HeaderText="% Difer.">
                                <ItemStyle HorizontalAlign="Right" Width="50px" />
                                        </asp:BoundField>
                                <asp:BoundField DataField="balance3" DataFormatString="{0:c}" 
                                    HeaderText="Balance3">
                                        <ItemStyle HorizontalAlign="Right" Width="100px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="porcpart3" DataFormatString="{0:P1}" 
                                    HeaderText="% Partic.">
                                        <ItemStyle HorizontalAlign="Right" Width="50px" />
                                    </asp:BoundField>
                                <asp:BoundField DataField="diferencia2" DataFormatString="{0:c}" 
                                    HeaderText="Diferencia">
                                        <ItemStyle HorizontalAlign="Right" Width="100px" />
                                    </asp:BoundField>
                                <asp:BoundField DataField="porcdif2" DataFormatString="{0:P1}" 
                                    HeaderText="% Difer.">
                                        <ItemStyle HorizontalAlign="Right" Width="50px" />
                                    </asp:BoundField>
                            </Columns>
                            <HeaderStyle CssClass="gridHeader" />
                            <PagerStyle CssClass="gridPager" />
                            <RowStyle CssClass="gridItem" />
                        </asp:GridView>
                        <asp:Label ID="lblTotalRegs" runat="server" Visible="False" 
                            style="font-size: xx-small"/>
                        <span style="font-size: xx-small">
                        <asp:Label ID="lblInfo" runat="server" 
                            Text="Su consulta no obtuvo ningún resultado." Visible="False" />
                        &nbsp; </span>
                        </td>
                </tr>
            </table>
        </asp:View>
        
          
        <asp:View ID="vwReporte" runat="server">
            <br /><br />&nbsp;
            <asp:Button ID="btnDatos" runat="server" CssClass="btn8" 
                onclick="btnDatos_Click" Text="Visualizar Datos" />  
            <rsweb:ReportViewer id="RptReporte" runat="server" Width="98%" Height="500px">
                <localreport reportpath="Page\Contabilidad\PyGComp\RptPyGComparativo.rdlc"></localreport>
            </rsweb:ReportViewer>
        </asp:View>

    </asp:MultiView> 
   
</asp:Content>