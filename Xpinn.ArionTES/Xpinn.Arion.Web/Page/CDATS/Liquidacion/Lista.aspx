<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" Title=".: Xpinn - CDATS Liquidaci�n:." %>

<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/tnumero.ascx" TagName="numero" TagPrefix="ucnumero" %>
<%@ Register src="../../../General/Controles/ctlGiro.ascx" tagname="giro" tagprefix="uc3" %>


<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

<asp:MultiView ID="mvPrincipal" runat="server" ActiveViewIndex=0>
    <asp:View ID="vwData" runat="server">
        <table style="width: 100%">
            <tr>
                <td style="text-align: center;" colspan="4">
                    <asp:Button ID="btnGenerar" runat="server" CssClass="btn8" Height="25px" Text="  GENERAR  " 
                    OnClick="btnGenerar_Click" />
                </td>
            </tr>
            <tr>            
                <td style="text-align: left">
                    Fecha de Liquidaci�n<br />
                    <ucFecha:fecha ID="txtFechaLiqui" runat="server" />
                </td>            
                <td style="text-align: left">
                    &nbsp;
                </td>
                <td style="text-align: left">Numero CDAT<br />
                    <asp:TextBox ID="txtCodigo" runat="server" CssClass="textbox" />
                     <td style="text-align: left">Identificaci�n<br />
                    <asp:TextBox ID="txtIdentific" runat="server" CssClass="textbox" />
                    <br />                    
                </td>
 <td style="text-align: left">
                    &nbsp;&nbsp;
                    <asp:CheckBox ID="chkSoloInteres" runat="server" Text="Solo con Inter�s" />
                    &nbsp;&nbsp;
                    <asp:CheckBox ID="cbCapitalizaInteres" runat="server" AutoPostBack="true" Text="Capitaliza Inter�s" OnCheckedChanged="cbCapitalizaInteres_CheckedChanged" />
                </td>                
               
            </tr>
            <tr>
                <td colspan="4">
                    <hr style="width: 100%" />
                </td>
            </tr>
        </table>                    
        <table style="width: 100%">
            <tr>
                <td style="text-align:left"> 
                    <asp:Panel ID="panelGrilla" runat="server">              
                    <div style="overflow:scroll; width:100%"> 
                        <asp:GridView ID="gvLista" runat="server" GridLines="Horizontal" AutoGenerateColumns="False"
                            AllowPaging="True" OnPageIndexChanging="gvLista_PageIndexChanging" 
                            PageSize="20" HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager"
                            RowStyle-CssClass="gridItem" DataKeyNames="numero_cdat">
                            <Columns>                           
                                <asp:BoundField DataField="codigo_cdat" HeaderText="Cod_Cdat"><ItemStyle HorizontalAlign="center" /></asp:BoundField>
                                <asp:BoundField HeaderText="Num CDAT" DataField="numero_cdat"><ItemStyle HorizontalAlign="left" /></asp:BoundField>
                                <asp:BoundField HeaderText="Fec Inicio" DataField="fecha_inicial" DataFormatString="{0:d}"><ItemStyle HorizontalAlign="left" /></asp:BoundField>
                                <asp:BoundField DataField="fecha_final" HeaderText="Fec Final" DataFormatString="{0:d}"><ItemStyle HorizontalAlign="left" /></asp:BoundField>
                                <asp:BoundField DataField="identificacion" HeaderText="Identificaci�n"><ItemStyle HorizontalAlign="Left" /></asp:BoundField>
                                <asp:BoundField HeaderText="Titular" DataField="nombre"><ItemStyle HorizontalAlign="Left" /></asp:BoundField>
                                <asp:BoundField DataField="valor" DataFormatString="{0:n}" HeaderText="Valor"><ItemStyle HorizontalAlign="Right" /></asp:BoundField>
                                <asp:BoundField DataField="periodicidad" HeaderText="Periodicidad"><ItemStyle HorizontalAlign="Center" /></asp:BoundField>
                                <asp:BoundField DataField="tasa" HeaderText="Tasa"><ItemStyle HorizontalAlign="center" /></asp:BoundField>
                                <asp:BoundField HeaderText="Fecha Int" DataField="fecha_int" DataFormatString="{0:d}"><ItemStyle HorizontalAlign="Left" /></asp:BoundField>
                                <asp:BoundField HeaderText="Interes" DataField="totalinteres" DataFormatString="{0:n}"><ItemStyle HorizontalAlign="Right" /></asp:BoundField>
                                <asp:BoundField DataField="retencion" HeaderText="Retenci�n" DataFormatString="{0:n}"><ItemStyle HorizontalAlign="Right" /></asp:BoundField>
                                <asp:BoundField HeaderText=" Valor del GMF" DataField="valor_gmf" DataFormatString="{0:n}"><ItemStyle HorizontalAlign="Right" /></asp:BoundField>
                                <asp:BoundField HeaderText="Valor Neto" DataField="interes_neto" DataFormatString="{0:n}"><ItemStyle HorizontalAlign="Right" /></asp:BoundField>
                                <asp:BoundField HeaderText="Forma de pago" DataField="forma_pago"><ItemStyle HorizontalAlign="Left" /></asp:BoundField>
                                <asp:BoundField HeaderText="Cta de Ahorros" DataField="cta_ahorros"><ItemStyle HorizontalAlign="Left" /></asp:BoundField>
                            </Columns>
                            <HeaderStyle CssClass="gridHeader"></HeaderStyle>
                            <PagerStyle CssClass="gridPager"></PagerStyle>
                            <RowStyle CssClass="gridItem"></RowStyle>
                        </asp:GridView>
                        <br />
                        <asp:TextBox ID="TXTIDENTIFICACCION"  Visible="false" runat="server" CssClass="textbox" />
                    </div>
                    </asp:Panel>                    
                </td>
            </tr>
            <tr>
                <td style="text-align:left">
                    <uc3:giro ID="ctlGiro" runat="server" />
                </td>
            </tr>
            <tr>
                <td style="text-align: center">
                    <asp:Label ID="lblTotalRegs" runat="server" Visible="False" />
                    <asp:Label ID="Label2" runat="server" Text="Su consulta no obtuvo ning�n resultado." Visible="False" />
                </td>
            </tr>
        </table>    

    </asp:View>
    <asp:View ID="vwFinal" runat="server">
        <table style="width: 100%;">
            <tr>
                <td style="text-align: center; font-size: large;">
                    <br />
                    <br />
                    <br />
                    <br />
                </td>
            </tr>
            <tr>
                <td style="text-align: center; font-size: large; color:Red;">
                    Se Grabaron los Datos Correctamente
                </td>
            </tr>
            <tr>
                <td style="text-align: center; font-size: large;">
                    <br />
                    <br />
                </td>
            </tr>
            <tr>
                <td style="text-align: center; font-size: large;">
                 <br />
                </td>
            </tr>
        </table>
    </asp:View>
</asp:MultiView>

    <br />
    <br />

    <uc4:mensajeGrabar ID="ctlMensaje" runat="server"/>


</asp:Content>
