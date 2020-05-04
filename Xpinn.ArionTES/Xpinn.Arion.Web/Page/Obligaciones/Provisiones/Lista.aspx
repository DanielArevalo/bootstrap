<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/General/Master/site.master"  CodeFile="Lista.aspx.cs" Inherits="Lista" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/ctlProcesoContable.ascx" TagName="procesoContable" TagPrefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <br/><br/>
    <asp:Panel ID="panelGeneral" runat="server">
        <div id="gvDiv">
            <asp:MultiView ID="mvProvision" runat="server" ActiveViewIndex="0">
            <asp:View ID="vwProceso" runat="server">
                <asp:Panel ID="pConsulta" runat="server">
                    <table cellpadding="5" cellspacing="0" border="0" style="width: 100%">
                        <tr valign="middle">
                            <td  class="tdI"> 
                                Entidad<asp:DropDownList ID="ddlEntidad" runat="server" class="dropdown" 
                                    Height="31px" Width="174px">
                                </asp:DropDownList>                    
                            </td>
                            <td>
                                Fecha Corte
                                <asp:DropDownList ID="ddlfecha" runat="server" class="dropdown" 
                                    Height="31px" Width="174px">
                                </asp:DropDownList>
                            </td>
                            <td style="width: 55px">
                                &nbsp;</td>
                            <td style="width: 198px">&nbsp;</td>
                            <td style="width: 191px">
                                &nbsp;</td>
                            <td style="width: 191px">
                                &nbsp;</td>
                        </tr>                                 
                        <tr align="left">
                            <td style="width: 198px">
                                <b>Ordenar Por</b></td>
                            <td>
                                <br/>
                            </td>
                            <td>
                                &nbsp;</td>
                            <td style="width: 55px">
                            </td>
                            <td style="width: 191px">
                                &nbsp;
                            </td>
                            <td style="width: 191px">
                                &nbsp;
                            </td>
                        </tr>
                        <tr align="left">
                            <td style="width: 198px">
                                No de Obligación<asp:DropDownList ID="ddlFiltro1" runat="server" 
                                    class="dropdown" Height="31px" Width="174px">
                                    <asp:ListItem Value="codobligacion">Numero de Obligación</asp:ListItem>                               
                                    <asp:ListItem Value="NUMEROPAGARE">Pagaré</asp:ListItem>
                                    <asp:ListItem Value="CODENTIDAD">Entidad</asp:ListItem>                                                               
                                    <asp:ListItem Value="CODPERIODICIDAD">Periodicidad</asp:ListItem>
                                    <asp:ListItem Value="MONTOAPROBADO">Monto Aprobado</asp:ListItem>
                                    <asp:ListItem Value="SALDOCAPITAL">Saldo a Capital</asp:ListItem>                                                               
                                </asp:DropDownList>
                            </td>
                            <td style="width: 191px">
                                Fecha de Desembolso<br />
                                <asp:DropDownList ID="ddlFiltro2" runat="server" class="dropdown" Height="31px" 
                                    Width="174px">
                                    <asp:ListItem Value="fecha_aprobacion">Fecha Aprobación</asp:ListItem>
                                    <asp:ListItem Value="NUMEROPAGARE">Pagaré</asp:ListItem>
                                    <asp:ListItem Value="CODENTIDAD">Entidad</asp:ListItem>                               
                                    <asp:ListItem Value="CODPERIODICIDAD">Periodicidad</asp:ListItem>
                                    <asp:ListItem Value="MONTOAPROBADO">Monto Aprobado</asp:ListItem>
                                    <asp:ListItem Value="SALDOCAPITAL">Saldo a Capital</asp:ListItem>                                
                                </asp:DropDownList>
                            </td>
                            <td style="width: 191px">
                                Monto Aprobado<br />
                                <asp:DropDownList ID="ddlFiltro3" runat="server" class="dropdown" Height="31px" 
                                    Width="174px">
                                    <asp:ListItem Value="CODOBLIGACION">Monto</asp:ListItem>
                                    <asp:ListItem Value="NUMEROPAGARE">Pagaré</asp:ListItem>
                                    <asp:ListItem Value="CODENTIDAD">Entidad</asp:ListItem>
                                    <asp:ListItem Value="CODPERIODICIDAD">Periodicidad</asp:ListItem>
                                    <asp:ListItem Value="MONTOAPROBADO">Monto Aprobado</asp:ListItem>
                                    <asp:ListItem Value="SALDOCAPITAL">Saldo a Capital</asp:ListItem>                               
                                </asp:DropDownList>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td style="width: 55px">
                                &nbsp;
                            </td>
                        </tr>                                     
                    </table>
                </asp:Panel>

                <asp:Panel ID="pDatos" runat="server">
                    <table cellpadding="5" cellspacing="0" border="0" style="width: 100%">
                        <tr>
                            <td align="center" colspan="6">
                                <div id="DivButtons">
                                    <asp:ImageButton ID="btnImprimir" runat="server"  ValidationGroup="valida" 
                                        ImageUrl="~/Images/btnImprimir.jpg" />
                                        &#160;
                                </div>
                            </td>
                            <td align="center">
                                &nbsp;</td>
                            <td align="center">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td colspan="6">            
                                <asp:GridView ID="gvObCredito" runat="server"
                                    AutoGenerateColumns="False" PageSize="20" BackColor="White" 
                                    BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
                                    ForeColor="Black" GridLines="Vertical" 
                                    style="font-size: x-small" >
                                    <AlternatingRowStyle BackColor="White" />
                                    <Columns>                    
                                         <asp:BoundField DataField="codobligacion"  HeaderText="No Obligación" />
                                         <asp:BoundField DataField="numeropagare"  HeaderText="Pagaré" />
                                         <asp:BoundField DataField="fecha_aprobacion" DataFormatString="{0:g}" HeaderText="Fecha Desembolso" />
                                         <asp:BoundField DataField="entidad"  HeaderText="Entidad" />
                                         <asp:BoundField DataField="montoaprobado" HeaderText="Monto Aprobado" DataFormatString="{0:N0}">
                                         <ItemStyle HorizontalAlign="Right" />
                                         </asp:BoundField>
                                         <asp:BoundField DataField="saldocapital" HeaderText="Saldo a Capital" DataFormatString="{0:N0}">
                                         <ItemStyle HorizontalAlign="Right" />
                                         </asp:BoundField>
                                         <asp:BoundField DataField="nombre_tasa"  HeaderText="Tipo de Tasa" />                        
                                         <asp:BoundField DataField="tasa" HeaderText="Tasa de Interes" />
                                         <asp:BoundField DataField="puntos_adicionales" HeaderText="Puntos Adicionales" />
                                         <asp:BoundField DataField="plazo"  HeaderText="Plazo"  />                         
                                         <asp:BoundField DataField="nomperiodicidad" HeaderText="Periocidad" />
                                         <asp:BoundField DataField="" HeaderText="Dias" />
                                         <asp:BoundField DataField="" HeaderText="Interes Causados" >                                                                            
                                         <ItemStyle HorizontalAlign="Right" />
                                         </asp:BoundField>
                                    </Columns>
                                    <FooterStyle BackColor="#CCCC99" />
                                    <HeaderStyle CssClass="gridHeader" />
                                    <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                                    <RowStyle CssClass="gridItem" />
                                    <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                    <SortedAscendingCellStyle BackColor="#FBFBF2" />
                                    <SortedAscendingHeaderStyle BackColor="#848384" />
                                    <SortedDescendingCellStyle BackColor="#EAEAD3" />
                                    <SortedDescendingHeaderStyle BackColor="#575357" />
                                </asp:GridView>                
                            </td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td colspan="6">               
                                <hr style="width: 100%; height: -12px;" />
                            </td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td align="right">
                                Montos Aprobados<br />
                                <asp:TextBox ID="Txtmonto" runat="server" style="text-align: right"></asp:TextBox>
                            </td>
                            <td>
                                Saldos a Capital<br />
                                <asp:TextBox ID="Txtsaldocapital" runat="server" style="text-align: right"></asp:TextBox>
                            </td>
                            <td>
                                A Corto Plazo<br />
                                <asp:TextBox ID="Txtcortoplazo" runat="server" style="text-align: right"></asp:TextBox>
                            </td>
                            <td>
                                A Largo Plazo<br />
                                <asp:TextBox ID="Txtlagoplazo" runat="server" style="text-align: right"></asp:TextBox>
                            </td>
                            <td>
                                Intereses Causados<br />
                                <asp:TextBox ID="Txtinteres_causado" runat="server" style="text-align: right"></asp:TextBox>                        
                            </td>
                            <td>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;</td>
                            <td>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td>
                            </td>
                        </tr>                            
                        <tr>
                            <td align="right">
                                Total en Libros<br />
                                <asp:TextBox ID="TxtValorLibros" runat="server" style="text-align: right"></asp:TextBox>
                            </td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td>
                                Valor Nota<br />
                                <asp:TextBox ID="TxtValorNota" runat="server" style="text-align: right"></asp:TextBox>
                            </td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td>
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
                                <asp:Label ID="lblMensajeGrabar" runat="server" Text="Provisión Grabada Correctamente"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: center; font-size: large;">
                                <br /><br />
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: center; font-size: large;">
                                <asp:Button ID="btnContinuar" runat="server" Text="Continuar" 
                                    onclick="btnContinuar_Click" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </asp:View>

            </asp:MultiView>
        </div>
    </asp:Panel>

    <asp:Panel ID="panelProceso" runat="server" Width="100%">
        <uc2:procesoContable ID="ctlproceso" runat="server" />  
    </asp:Panel> 


</asp:Content>


