<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" %>

<%@ Register Src="~/General/Controles/ctlLineaAhorro.ascx" TagName="ddlLineaAhorro" TagPrefix="ctl" %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/ctlOficina.ascx" TagName="ddlOficina" TagPrefix="ctl" %>
<%@ Register Src="~/General/Controles/ctlDestinacion.ascx" TagName="ddlDestinacion" TagPrefix="ctl" %>
<%@ Register Src="~/General/Controles/ctlPersona.ascx" TagName="ddlPersona" TagPrefix="ctl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server"> 

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>  
     <asp:MultiView ID="mvAhorroVista" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwDatos" runat="server">
    <asp:Panel ID="pConsulta" runat="server">            
        <table cellpadding="5" cellspacing="0" style="width: 83%">
            <tr>
               <td style="text-align: left; width: 172px;">
                    Tipo Operación<br />
                    <asp:DropDownList ID="ddloperacion" runat="server" Width="200px "   
                    CssClass="textbox">  
                       
                    </asp:DropDownList>
                    
                </td>
                <td style="text-align: center; width: 176px;" class="logo">
                    
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:CheckBox ID="chkaceptaexcepcion" runat="server" />
                    Afecta Producto
                </td>
                
                <td style="text-align: left; width: 187px;" colspan="2">
                   <asp:CheckBox ID="chkmanejacuentas" runat="server" ></asp:CheckBox>Maneja Cuentas Excentas
                </td>                
            </tr> 
            </table>
            <table cellpadding="5" cellspacing="0" style="width: 81%">          
            <tr>
                <td style="text-align: left; width: 203px;">
                    Tipo Transacción<br />
                    <asp:DropDownList ID="Ddltipotran" runat="server" Width="200px "   
                    CssClass="textbox">  
                       
                    </asp:DropDownList>
                <td style="text-align: center; width: 180px;">
                    <asp:CheckBox ID="chkasume" runat="server" OnCheckedChanged="asumecliente_oncheckedchange" AutoPostBack="true">
                    </asp:CheckBox> Asume Cliente
                </td>
                <td style="text-align: left;">
                    %Asume
                    <asp:TextBox ID="txtAsume" runat="server" CssClass="textbox" enabled="false" 
                        Width="100px"></asp:TextBox>                   
               </td>
               
            </tr>
            
            </table>
            <table cellpadding="5" cellspacing="0" style="width: 55%" >
                <caption>
                    
                    <caption>
                        <hr colspan="5" />
                        <tr>
                            <td colspan="3" style="text-align: left;">
                                <strong>
                                <asp:Label ID="labelformaspago" runat="server" 
                                    Text="Formas de Pago a las que Aplica GMF" Visible="true"></asp:Label>
                                </strong>
                            </td>
                        </tr>
                    </caption>
                    </tr>
                    <tr>
                        <td style="text-align: left; width: 99px;">
                            <asp:CheckBox ID="chkEfectivo" runat="server" />
                            Efectivo
                        </td>
                        <td style="text-align: left; width: 84px;">
                            <asp:CheckBox ID="ChechkChequeckBox2" runat="server" />
                            Cheque
                        </td>
                        <td style="text-align: left;">
                            <asp:CheckBox ID="chkTraslado" runat="server" />
                            Traslado
                        </td>
                    </tr>
                    </table>
            <table cellpadding="5" cellspacing="0" style="width: 55%" >
                    <caption>
                        <hr colspan="5" />
                        <tr>
                            <td colspan="3" style="text-align: left;">
                                <strong>
                                <asp:Label ID="lblproductoaaplicar" runat="server" colspan="5" 
                                    Text="Producto al que Aplica GMF" Visible="true"></asp:Label>
                                </strong>
                            </td>
                        </tr>
                    </caption>
                    </tr>
                </caption>
            </table>
            <table cellpadding="5" cellspacing="0" style="width: 84%">
            <tr>
                    <td style="text-align: left; width: 192px;" class="logo">
                        Tipo Producto<br />
                        <asp:DropDownList ID="ddlproductos" runat="server" CssClass="textbox" OnSelectedIndexChanged="tipo_producto_onselectedindexchanged" AutoPostBack="true"
                            Width="300px ">
                        </asp:DropDownList>
                    </td>
                    <td style="text-align: left;">
                        Linea Producto<br />
                        <asp:DropDownList ID="ddllinea" runat="server" CssClass="textbox" 
                            Width="300px ">
                        </asp:DropDownList>
                    </td>
                </tr>
        </table>

       
    </asp:Panel>
</asp:View>
 <asp:View ID="mvFinal" runat="server">
            <asp:Panel id="PanelFinal" runat="server">
                <table style="width: 100%;">
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <br /><br /><br /><br />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <asp:Label ID="lblMensaje" runat="server" 
                                Text="Datos Modificados Correctamente" style="color: #FF3300"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <br /><br />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </asp:View>
</asp:MultiView>
    <uc1:mensajeGrabar ID="ctlMensaje" runat="server"/>
</asp:Content>