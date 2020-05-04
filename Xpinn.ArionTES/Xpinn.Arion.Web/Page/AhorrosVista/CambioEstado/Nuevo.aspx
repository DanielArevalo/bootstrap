<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" %>

<%@ Register Src="~/General/Controles/ctlLineaAhorro.ascx" TagName="ddlLineaAhorro" TagPrefix="ctl" %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/ctlOficina.ascx" TagName="ddlOficina" TagPrefix="ctl" %>
<%@ Register Src="~/General/Controles/ctlDestinacion.ascx" TagName="ddlDestinacion" TagPrefix="ctl" %>
<%@ Register Src="~/General/Controles/ctlPersona.ascx" TagName="ddlPersona" TagPrefix="ctl" %>

<%@ Register src="../../../General/Controles/ctlProcesoContable.ascx" tagname="procesocontable" tagprefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server"> 

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>  
     <asp:MultiView ID="mvAhorroVista" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwDatos" runat="server">
    <asp:Panel ID="pConsulta" runat="server">            
        <table cellpadding="5" cellspacing="0" style="width: 70%">
            <tr>
                <td style="text-align: left;">
                    Fecha<br />
                    <ucFecha:fecha ID="txtFechaCambio" runat="server" AutoPostBack="True" 
                        CssClass="textbox" MaxLength="1" ValidationGroup="vgGuardar" Width="148px" />
                </td>
                <td style="text-align: left;">
                    Nuevo Estado<br />
                    <asp:DropDownList ID="ddlEstado" runat="server" Width="200px "   
                    CssClass="textbox">  
                       <asp:ListItem Value="-1">Seleccione Un Item</asp:ListItem>
                        <asp:ListItem Value="0">Apertura</asp:ListItem>
                        <asp:ListItem Value="1">Activa</asp:ListItem>
                        <asp:ListItem Value="2">Inactiva</asp:ListItem>
                        <asp:ListItem Value="3">Bloqueada</asp:ListItem>
                        <asp:ListItem Value="4">Cerrada</asp:ListItem>
                        <asp:ListItem Value="5">Embargada</asp:ListItem>
                    </asp:DropDownList>
                    
                </td>
                <td style="text-align: left; width: 119px;" colspan="2">
                    Motivo Cambio<br />
                   <asp:TextBox ID="txtMotivo" runat="server" CssClass="textbox" Width="250px" ></asp:TextBox>
                </td>                
            </tr>           
            <tr>
                <td style="text-align: left; ">
                    Numero Cuenta<br />
                    <asp:TextBox ID="txtNumeroCuenta" runat="server" CssClass="textbox" Enabled="false"
                        Width="119px"></asp:TextBox>
                </td>
                <td style="text-align: left; width: 119px;">
                    Fecha  Apertura<br />
                   <ucFecha:fecha ID="txtFechaApertura" runat="server" AutoPostBack="True" Enabled="false"
                         CssClass="textbox" MaxLength="1" ValidationGroup="vgGuardar" Width="148px" />
                </td>
                <td style="text-align: left; ">
                    Línea<br />
                    <asp:TextBox ID="txtLinea" runat="server" CssClass="textbox" enabled="false"
                        Width="100px"></asp:TextBox>                   
               </td>
                <td style="text-align: left; ">
                    Nombre Línea<br />
                    <asp:TextBox ID="txtNombreLinea" runat="server" CssClass="textbox" Enabled="false"
                        Width="200px"></asp:TextBox>                    
                </td>
            </tr>
            <tr>
                <td style="text-align: left; ">
                    Saldo Total<br /><asp:TextBox ID="txtsaldo_total" runat="server" CssClass="textbox" Enabled="false"
                        Width="119px"></asp:TextBox>                    
                </td>
                 <td style="text-align: left;" colspan="2">
                    Estado<br /><asp:TextBox ID="TxtEstado" runat="server" CssClass="textbox" Enabled="false"
                        Width="119px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="text-align: left; ">
                    Identificacion<br / /><asp:TextBox ID="Txtiden" runat="server" CssClass="textbox" Enabled="false"
                        Width="119px"></asp:TextBox>
                </td>
                <td style="text-align: left; ">
                    Tipo de Identif.<br /><asp:TextBox ID="TxtIdentif" runat="server" CssClass="textbox" Enabled="false"
                        Width="119px"></asp:TextBox>                    
                </td>
                <td style="text-align: left; " colspan="2">
                    Nombre<br />
                    <asp:TextBox ID="txtNombre" runat="server" CssClass="textbox" Enabled="false" Width="300px"></asp:TextBox>                    
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
         <br />
         <asp:View ID="viewComprobante" runat="server">
             <asp:Panel ID="Panel2" runat="server">
                 <table style="width: 100%;">
                     <tr>
                         <td>
                             <asp:Panel ID="panelGeneral" runat="server">
                             </asp:Panel>
                           <asp:Panel ID="panelProceso" runat="server" Width="100%">
        <uc2:procesoContable ID="ctlproceso" runat="server" />  
    </asp:Panel> 
                         </td>
                     </tr>
                 </table>
             </asp:Panel>
         </asp:View>
</asp:MultiView>
    <uc1:mensajeGrabar ID="ctlMensaje" runat="server"/>
</asp:Content>