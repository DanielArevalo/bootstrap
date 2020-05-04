<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/General/Master/site.master"  CodeFile="Lista.aspx.cs" Inherits="Lista" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">

    <script type="text/javascript" src="../../../Scripts/gridviewScroll.min.js"></script>    
    <script type="text/javascript">
        function pageLoad() {
            $('#<%=gvObCredito.ClientID%>').gridviewScroll({
                width: 1000,
                height: 400,
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
    <br/><br/>
    <div id="gvDiv">
    <table cellpadding="5" cellspacing="0" border="0" style="width: 100%">
         <tr valign="middle">
            <td  class="tdI">
                <asp:Panel ID="pConsulta" runat="server">                    
                    <table border="0">                    
                        <tr align="left">
                            <td style="width: 107px">                                
                                <b>Buscar Por</b><br/>
                            </td>
                            <td> 
                                Número de Obligación<br/>
                                <asp:TextBox ID="txtNumeObl"  maxlength="20" Width="150px" CssClass="textbox" runat="server"></asp:TextBox>
                            </td>
                            <td class="logo" style="width: 164px">
                                Fecha Próximo Pago <br/> 
                                <asp:TextBox ID="txtFechaProxPago"  maxlength="10" cssClass="textbox" 
                                    runat="server" Width="100px"></asp:TextBox>
                                <img id="Image1" alt="Calendario" src="../../../Images/iconCalendario.png" />
                                <asp:CalendarExtender ID="CalendarExtender1" runat="server" 
                                        PopupButtonID="Image1" 
                                        TargetControlID="txtFechaProxPago"
                                        Format="dd/MM/yyyy" >
                                </asp:CalendarExtender>
                                <asp:RegularExpressionValidator ID="revtxtFechaIni" runat="server" 
                                    ValidationGroup="valida" 
                                    ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d" 
                                    ControlToValidate="txtFechaProxPago" ForeColor="Red" 
                                    ErrorMessage="Formato Invalida" Display="Dynamic"></asp:RegularExpressionValidator>
                            </td>
                            <td style="margin-left: 40px">
                                Entidad<br/>
                                <asp:DropDownList ID="ddlEntidad" Width="174px" class="dropdown" runat="server" Height="31px">
                                </asp:DropDownList>
                            </td>
                            <td>
                                Estado de la Obligación<br/>
                                <asp:DropDownList ID="ddlEstadoObl" Width="174px" class="dropdown" runat="server" Height="31px">
                                    <asp:ListItem Value="T">Todos</asp:ListItem>
                                    <asp:ListItem Value="S">Solicitado</asp:ListItem>
                                    <asp:ListItem Value="D">Desembolsado</asp:ListItem>
                                    <asp:ListItem Value="C">Cancelada</asp:ListItem>
                                    <asp:ListItem Value="A">Anulada</asp:ListItem>
                                    <asp:ListItem Value="P">Pendiente por Solicitud</asp:ListItem>
                                </asp:DropDownList>                            
                            </td>
                        </tr>
                    </table>
                    <table border="0" width="70%">   
                        <tr align="left">
                            <td style="width: 109px">
                                <b>Ordenar Por</b><br/>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlFiltro1" class="dropdown" runat="server" 
                                    Height="31px" onselectedindexchanged="ddlFiltro1_SelectedIndexChanged">
                                    <asp:ListItem Value="CODOBLIGACION">No Obligación</asp:ListItem>
                                    <asp:ListItem Value="NUMEROPAGARE">Pagaré</asp:ListItem>
                                    <asp:ListItem Value="CODENTIDAD">Entidad</asp:ListItem>
                                    <asp:ListItem Value="CODLINEAOBLIGACION">Línea Obligación</asp:ListItem>
                                    <asp:ListItem Value="FECHAPROXIMOPAGO">Fecha Próximo Pago</asp:ListItem>
                                    <asp:ListItem Value="CODPERIODICIDAD">Periodicidad</asp:ListItem>
                                    <asp:ListItem Value="MONTOAPROBADO">Monto Aprobado</asp:ListItem>
                                    <asp:ListItem Value="SALDOCAPITAL">Saldo a Capital</asp:ListItem>
                                    <asp:ListItem Value="VALOR_CUOTA">Cuota</asp:ListItem>
                                    <asp:ListItem Value="ESTADOOBLIGACION">Estado</asp:ListItem>
                                 </asp:DropDownList>
                            </td>
                            <td valign="bottom">
                                <asp:DropDownList ID="ddlFiltro2" class="dropdown" runat="server" Height="31px">
                                    <asp:ListItem Value="CODOBLIGACION">No Obligación</asp:ListItem>
                                    <asp:ListItem Value="NUMEROPAGARE">Pagaré</asp:ListItem>
                                    <asp:ListItem Value="CODENTIDAD">Entidad</asp:ListItem>
                                    <asp:ListItem Value="CODLINEAOBLIGACION">Línea Obligación</asp:ListItem>
                                    <asp:ListItem Value="FECHAPROXIMOPAGO">Fecha Próximo Pago</asp:ListItem>
                                    <asp:ListItem Value="CODPERIODICIDAD">Periodicidad</asp:ListItem>
                                    <asp:ListItem Value="MONTOAPROBADO">Monto Aprobado</asp:ListItem>
                                    <asp:ListItem Value="SALDOCAPITAL">Saldo a Capital</asp:ListItem>
                                    <asp:ListItem Value="VALOR_CUOTA">Cuota</asp:ListItem>
                                    <asp:ListItem Value="ESTADOOBLIGACION">Estado</asp:ListItem>
                                 </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td align="center">
                <div id="DivButtons" style="text-align: left">
                    <asp:Button ID="btnExpObligacion" runat="server" CssClass="btn8" 
                        onclick="btnExpObligacion_Click" onclientclick="btnExpObligacion_Click" 
                        Text="Exportar a excel" />
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:ImageButton ID="btnImprimir" runat="server"  ValidationGroup="valida" 
                        ImageUrl="~/Images/btnImprimir.jpg" />
                        &#160;
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="gvObCredito" runat="server" AutoGenerateColumns="False" 
                    PageSize="20" BackColor="White" 
                    BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
                    ForeColor="Black" GridLines="Vertical" style="font-size: x-small" >
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                         <asp:BoundField DataField="codobligacion"  HeaderText="No Obligación" />
                         <asp:BoundField DataField="numeropagare"  HeaderText="Pagaré" />
                         <asp:BoundField DataField="nomentidad"  HeaderText="Entidad" />
                         <asp:BoundField DataField="nomlineaobligacion"  HeaderText="Línea Obligación" />
                        <asp:BoundField DataField="fecha_aprobacion" DataFormatString="{0:d}" HeaderText="Fecha Desembolso" />
                        <asp:BoundField DataField="nomperiodicidad" HeaderText="Periodicidad" />
                          <asp:BoundField DataField="montoaprobado" HeaderText="Monto Aprobado" DataFormatString="{0:N0}">
                          <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                          <asp:BoundField DataField="saldocapital" HeaderText="Saldo a Capital" DataFormatString="{0:N0}">
                          <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="cuota" HeaderText="Cuota" DataFormatString="{0:N0}">
                          <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="fechaproximopago" DataFormatString="{0:g}" HtmlEncode="false" HeaderText="Fecha Próximo Pago" />
                        <asp:BoundField DataField="estadoobligacion" HeaderText="Estado" />
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
        </tr>
        <tr><td>&nbsp;</td></tr>
    </table>
    </div>
</asp:Content>


