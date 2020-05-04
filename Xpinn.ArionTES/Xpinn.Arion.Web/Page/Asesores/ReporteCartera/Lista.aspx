<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" EnableEventValidation="true" CodeFile="Lista.aspx.cs" Inherits="Page_Asesores_Colocacion_Lista" Title=".: Xpinn - Asesores :."%>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" contentplaceholderid="cphMain"  runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:Panel ID="pConsulta" runat="server">
        <table style="width:80%;"> 
            <tr>
                <td align="left" width="15%">
                    <asp:Panel ID="Panel1" runat="server" Width="100%">
                        <asp:Label ID="LabelFecha" runat="server" Text="Fecha"></asp:Label>
                        <br />
                        <asp:TextBox ID="txtFechaIni" runat="server" cssClass="textbox" maxlength="10" Width="92px"></asp:TextBox>
                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" 
                            PopupButtonID="Image1" TargetControlID="txtFechaIni">
                        </asp:CalendarExtender>
                        <img id="Image1" alt="Calendario" src="../../../Images/iconCalendario.png" />
                    </asp:Panel>
                </td>
                <td align="left" width="30%">
                    Oficina<br />
                    <asp:DropDownList ID="ddlOficina" runat="server" AutoPostBack="True" Width="100%"
                        CssClass="textbox" onselectedindexchanged="ddlOficina_SelectedIndexChanged">
                        <asp:ListItem Value="0">Seleccione una Oficina</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td align="left" width="30%">
                    Asesor<br />
                    <asp:DropDownList ID="ddlAsesores" runat="server" AutoPostBack="True" Width="100%" CssClass="textbox" >
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="left" width="80%" colspan="3">
                    <span style="font-weight: bold">Morosidad</span> <br />
                    <table>
                        <tr>
                            <td>
                                <asp:RadioButtonList ID="RadioButtonList1" runat="server" Width="100%" 
                                    RepeatDirection="Horizontal" style="font-size: small">
                                    <asp:ListItem Value="1" Selected ="True">0-30 dias</asp:ListItem>
                                    <asp:ListItem Value="2">31-60 dias</asp:ListItem>
                                    <asp:ListItem Value="3">61-90 dias</asp:ListItem>
                                    <asp:ListItem Value="4">91-120 dias</asp:ListItem>
                                    <asp:ListItem Value="5">121-180 dias</asp:ListItem>
                                    <asp:ListItem Value="6">Mas de 180 dias</asp:ListItem>
                                    <asp:ListItem Value="7">Otros</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            <td>
                                <asp:Label ID="Label1" runat="server" Text="Rango:"></asp:Label>
                                <asp:TextBox ID="TxtDesde" runat="server" Width="30px"></asp:TextBox>
                                <asp:Label ID="Label2" runat="server" Text="-"></asp:Label>
                                <asp:TextBox ID="TxtHasta" runat="server" Width="30px"></asp:TextBox>
                                <asp:Label ID="dias" runat="server" Text="días"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <hr />
    <asp:MultiView ID="mvLista" runat="server">        
        <asp:View ID="vGridReporteCobranza" runat="server">
            <div style="overflow:scroll;height:400px;width:100%;">
                <asp:Button ID="btnExportar" runat="server" CssClass="btn8" 
                    onclick="btnExportar_Click" Text="Exportar a excel" />                       
                <asp:GridView ID="gvReoirtecobranza" runat="server"  
                    AutoGenerateColumns="False" DataKeyNames="NumRadicacion" HeaderStyle-CssClass="gridHeader"   
                    PagerStyle-CssClass="gridPager"  RowStyle-CssClass="gridItem"  
                    Width="100%" BorderStyle="Solid" BorderWidth="1px" style="font-size: x-small">
                    <Columns>                            
                        <asp:HyperLinkField DataNavigateUrlFields="NumRadicacion" 
                            DataNavigateUrlFormatString="..//../Recuperacion/Detalle.aspx?radicado={0}" 
                            DataTextField="NumRadicacion" HeaderText="NumRadicacion" Target="_blank" 
                            Text="NumRadicacion" />
                        <asp:BoundField DataField="icodigo" HeaderText="Codigo">
                        <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="identificacion" HeaderText="Identificación">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Nombres" HeaderText="Nombre del Cliente" ItemStyle-Width="180px">
                            <ItemStyle HorizontalAlign="left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="direccion" HeaderText="Dirección">
                            <ItemStyle HorizontalAlign="center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="telefono" HeaderText="Telefono">
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="celular" HeaderText="Celular">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="email" HeaderText="Email">
                            <ItemStyle HorizontalAlign="center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="telefono_oficina" HeaderText="Telefono Oficina">
                            <ItemStyle HorizontalAlign="center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="direccion_oficina" HeaderText="Direccion Oficina">
                            <ItemStyle HorizontalAlign="center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="direccion_correspondencia" 
                            HeaderText="Direccion Correspondencia">
                            <ItemStyle HorizontalAlign="center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="telefono_correspondencia" 
                            HeaderText="Telefono Correspondencia">
                            <ItemStyle HorizontalAlign="center" />
                        </asp:BoundField>
                          <asp:BoundField DataField="empresa_recaudo" HeaderText="Empresa">
                            <ItemStyle HorizontalAlign="center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="pagare" HeaderText="Pagaré" />
                            <asp:BoundField DataField="linea" HeaderText="Linea">
                            <ItemStyle HorizontalAlign="center" />
                        </asp:BoundField>
                            <asp:BoundField DataField="monto_aprobado" HeaderText="Monto Aprobado">
                            <ItemStyle HorizontalAlign="center" />
                        </asp:BoundField>
                            <asp:BoundField DataField="numero_cuotas" HeaderText="Numero Cuotas ">
                            <ItemStyle HorizontalAlign="center" />
                        </asp:BoundField>
                            <asp:BoundField DataField="cuotas_pagadas" HeaderText="Cuotas Pagadas">
                            <ItemStyle HorizontalAlign="center" />
                        </asp:BoundField>  
                        <asp:BoundField DataField="valor_cuota" HeaderText="Valor Cuota">
                            <ItemStyle HorizontalAlign="center" />
                        </asp:BoundField>   
                         <asp:BoundField DataField="fecha_proximo" HeaderText="Fecha Próximo Pago" 
                            DataFormatString="{0:dd/MM/yyyy}">
                            <ItemStyle HorizontalAlign="center" />
                        </asp:BoundField>  
                         <asp:BoundField DataField="saldo_capital" HeaderText="Saldo Capital">
                            <ItemStyle HorizontalAlign="center" />
                        </asp:BoundField>  
                        <asp:BoundField ReadOnly="True" SortExpression="dias_mora" DataField="dias_mora"  DataFormatString="{0}"  HeaderText="Dias Mora" >
                         <ItemStyle HorizontalAlign="center" />
                        </asp:BoundField>  
                         <asp:BoundField DataField="valor_pagar" HeaderText="Valor Pagar">
                            <ItemStyle HorizontalAlign="center" />
                        </asp:BoundField>   
                         <asp:BoundField DataField="identificacion_codeudor" 
                            HeaderText="Identificación Codeudor">
                            <ItemStyle HorizontalAlign="center" />
                        </asp:BoundField>  
                        <asp:BoundField DataField="nombre_codeudor" HeaderText="Nombre Codeudor">
                            <ItemStyle HorizontalAlign="center" />
                        </asp:BoundField>  
                         <asp:BoundField DataField="direcion_codeudor" HeaderText="Dirección_codeudor">
                        <ItemStyle HorizontalAlign="center" />
                        </asp:BoundField>  
                        <asp:BoundField DataField="telefono_codeudor" HeaderText="Telefono Codeudor">
                            <ItemStyle HorizontalAlign="center" />
                        </asp:BoundField>  
                         <asp:BoundField DataField="telefono_empresa_codeudor" HeaderText="Telefono Empresa Codeudor">
                            <ItemStyle HorizontalAlign="center" />
                        </asp:BoundField>  
                        <asp:BoundField DataField="direcion_corespondecia_codeudor" HeaderText="Dirección Corespondecia Codeudor">
                            <ItemStyle HorizontalAlign="center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="telefono_correspondecia_codeudor" HeaderText="Telefono Correspondecia Codeudor">
                            <ItemStyle HorizontalAlign="center" />
                        </asp:BoundField>
                           <asp:BoundField DataField="email_codeudor" HeaderText="Email Codeudor">
                            <ItemStyle HorizontalAlign="center" />
                        </asp:BoundField>
                                                   <asp:BoundField DataField="empresa_recaudo_code" HeaderText="Empresa Codeudor">
                            <ItemStyle HorizontalAlign="center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="nombre_asesor" HeaderText="Nombre Asesor">
                            <ItemStyle HorizontalAlign="center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="codigo_oficina" HeaderText="Oficina" />
                        <asp:BoundField DataField="estado" HeaderText="Estado" />
                    </Columns>
                    <HeaderStyle CssClass="gridHeader" />
                    <PagerStyle CssClass="gridPager" />
                    <RowStyle CssClass="gridItem" />
                </asp:GridView>
            </div>
            <div />
            <asp:Label ID="lblTotalRegs" runat="server" />
            <br />
            <br />
            &nbsp;                                                            
        </asp:View>

    </asp:MultiView> 

</asp:Content>

