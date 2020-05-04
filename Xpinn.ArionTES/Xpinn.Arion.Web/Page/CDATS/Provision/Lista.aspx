<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" Title=".: Xpinn - CDATS Provision :." %>

<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<%@ Register Src="~/General/Controles/tnumero.ascx" TagName="numero" TagPrefix="ucnumero" %>


<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
    <asp:MultiView ID="mvAhorroVista" runat="server" ActiveViewIndex="0">
<asp:View ID="vwDatos" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
                                    </asp:ScriptManager>

    <table style="width: 850px">
        <tr>
            <td colspan="6" style="text-align:left">
                <strong>Criterios de Busqueda :</strong>
            </td>
        </tr>
        <tr>                      
            <td style="text-align: left; width:130px">
            Fecha de Provision<br />
            <ucFecha:fecha ID="txtFecha" runat="server" Visible="False"/>
                <br />
                <asp:DropDownList ID="ddlFechaCorte" runat="server" CssClass="dropdown" 
                    Width="160px" />
            </td>
            
        </tr>
        <tr>
            <td colspan="6">
                <hr style="width: 850px" />
            </td>
        </tr>
    </table>    
            <tr>
            <td>
                <asp:Button ID="btnExportar" runat="server" CssClass="btn8" 
                    onclick="btnExportar_Click" Text="Exportar a Excel" />
            </td>
        </tr> 
    <asp:Panel ID="panelGrilla" runat="server">
        <table style="width: 100%">
            <tr>
                <td style="text-align:left">               
                <div style="overflow:scroll; width:100%"> 
                    <asp:GridView ID="gvLista" runat="server" GridLines="Horizontal" 
                        AutoGenerateColumns="False" OnPageIndexChanging="gvLista_PageIndexChanging" 
                        PageSize="20" HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager"
                        RowStyle-CssClass="gridItem" DataKeyNames="codigo_cdat" 
                        style="margin-right: 54px">
                        <Columns>
                            
                             <asp:BoundField DataField="Numero_cdat" HeaderText="Número CDAT">
                                <ItemStyle HorizontalAlign="center" />
                                 </asp:BoundField>
                            <asp:BoundField DataField="codigo_cdat" HeaderText="Codigo CDAT">
                                <ItemStyle HorizontalAlign="center" />
                                 </asp:BoundField>
                                 <asp:BoundField DataField="fecha_inicio" DataFormatString="{0:d}" HeaderText="Fec Inicial">
                                <ItemStyle HorizontalAlign="Center" />
                                
                            </asp:BoundField>
                             <asp:BoundField DataField="fecha_vencimiento" DataFormatString="{0:d}" HeaderText="Fec Final">
                                <ItemStyle HorizontalAlign="Center" />
                                 </asp:BoundField>

                                  <asp:BoundField HeaderText="identificacion" DataField="identificacion">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                             <asp:BoundField HeaderText="nombres" DataField="nombres">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>

                             <asp:BoundField DataField="valor" DataFormatString="{0:n}" HeaderText="valor">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>


                            <asp:BoundField HeaderText="Tasa Interes" DataField="tasa_efectiva">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>

                            
                             <asp:BoundField DataField="fecha_provision" DataFormatString="{0:d}" HeaderText="Fecha int">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>

                               <asp:BoundField DataField="dias" HeaderText="Dias Causados">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>


                             <asp:BoundField HeaderText="Interes Causado" DataField="intereses_cap">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>

                             <asp:BoundField DataField="retencion" HeaderText="Retencion">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            
                             <asp:BoundField DataField="capitalizar_int" HeaderText="Interes Acumulado">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                    
                           
                        </Columns>
                        <HeaderStyle CssClass="gridHeader"></HeaderStyle>
                        <PagerStyle CssClass="gridPager"></PagerStyle>
                        <RowStyle CssClass="gridItem"></RowStyle>
                    </asp:GridView>
                    </div>                    
                </td>
            </tr>
            <tr>
                <td style="text-align: center">
                    <asp:Label ID="lblTotalRegs" runat="server" Visible="False" />
                </td>
            </tr>
        </table>
    </asp:Panel>

    <br />
    <br />
            </asp:View>
</asp:MultiView>
    <uc4:mensajeGrabar ID="ctlMensaje" runat="server"/>


</asp:Content>
