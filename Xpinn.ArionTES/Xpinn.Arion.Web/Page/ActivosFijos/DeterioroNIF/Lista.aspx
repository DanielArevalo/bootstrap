<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" %>
<%@ Register src="../../../General/Controles/fecha.ascx" tagname="fecha" tagprefix="uc" %>
<%@ Register Src="../../../General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc1" %>
<%@ Register Src="../../../General/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/ctlProcesoContable.ascx" TagName="procesoContable" TagPrefix="uc2" %>
<%@ Register Src="~/General/Controles/decimalesGridRow.ascx" TagName="decimalesGridRow" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
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

    <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
          <asp:View ID="View1" runat="server">

    <asp:Panel ID="pConsulta" runat="server" Width="100%">            
        <table cellpadding="5" cellspacing="0" style="width: 100%">
            <tr>
                <td style="height: 15px; text-align: left;">
                    Fecha Deterioro<br />
                    <uc:fecha ID="txtFecha" runat="server" CssClass="textbox" Width="85px" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <hr style="width: 100%" />
    <table style="width:100%">
        <tr>
            <td style="height: 100%">
                <asp:Button ID="btnExportar" runat="server" CssClass="btn8" 
                    onclick="btnExportar_Click" Text="Exportar a Excel" />
            </td>
        </tr>
        <tr>  
            <td>
                <asp:GridView ID="gvLista" runat="server" AutoGenerateColumns="False" GridLines="Horizontal"
                    ShowHeaderWhenEmpty="True" OnPageIndexChanging="gvLista_PageIndexChanging" OnSelectedIndexChanged="gvLista_SelectedIndexChanged"
                    DataKeyNames="consecutivo,valor_activo_nif,valor_residual_nif,vida_util_nif" Width="100%" Style="font-size: xx-small;">
                    <Columns>
                        <asp:BoundField DataField="consecutivo" HeaderText="Consec.">
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="cod_act" HeaderText="Código">
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="nombre" HeaderText="Nombre">
                            <ItemStyle HorizontalAlign="Left" Width="150px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="nomoficina" HeaderText="Oficina">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="fecha_compra" HeaderText="F.Compra" DataFormatString="{0:d}">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="valor_compra" HeaderText="Vr.Compra" DataFormatString="{0:c0}">
                            <ItemStyle HorizontalAlign="Right" Width="90px"/>
                        </asp:BoundField>
                        <asp:BoundField DataField="nomMoneda" HeaderText="Moneda">
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="cod_cuenta_depreciacion" HeaderText="Cod.Cuenta">
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="nomClasificacion" HeaderText="Clasificación">
                            <ItemStyle HorizontalAlign="Left" Width="90px"/>
                        </asp:BoundField>
                        <asp:BoundField DataField="nomtipo" HeaderText="Tipo">
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="nomMetodo" HeaderText="Metodo">
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="unigeneradora_nif" HeaderText="U. Gene Ef">
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="fecha_ult_deterioro" HeaderText="Fecha Ult Deterioro" DataFormatString="{0:d}">
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="vida_util_nif" HeaderText="Vida Util NIIF">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="valor_activo_nif" HeaderText="Vr.Activo" DataFormatString="{0:c0}">
                            <ItemStyle HorizontalAlign="Right" Width="90px"/>
                        </asp:BoundField>
                        <asp:BoundField DataField="valor_residual_nif" HeaderText="Vr. Residual" DataFormatString="{0:c0}">
                            <ItemStyle HorizontalAlign="Right" Width="90px"/>
                        </asp:BoundField>
                        <asp:BoundField DataField="vrdeterioro_nif" HeaderText="Vr Deterioro" DataFormatString="{0:c0}">
                            <ItemStyle HorizontalAlign="Right" Width="85px"/>
                        </asp:BoundField>
                        <asp:BoundField DataField="vrrecdeterioro_nif" HeaderText="Vr Rec Deterioro" DataFormatString="{0:c0}">
                            <ItemStyle HorizontalAlign="Right" Width="85px"/>
                        </asp:BoundField>
                        <asp:BoundField DataField="valoractual" HeaderText="Vr Actual" DataFormatString="{0:c0}">
                            <ItemStyle HorizontalAlign="Right" Width="90px"/>
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Nuevo">
                            <ItemTemplate>
                                <uc1:decimalesGridRow ID="txtnuevo" runat="server" Text='<%# Eval("valor_deterioro", "{0:n0}") %>' style="text-align: right;font-size:x-small"
                                            Habilitado="True" Enabled="True" Width_="90" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Observaciones">
                            <ItemTemplate>
                                <asp:TextBox ID="txtObser" runat="server" CssClass="textbox" Width="100px"></asp:TextBox>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" Width="100px" />
                        </asp:TemplateField>
                    </Columns>
                    <HeaderStyle CssClass="gridHeader" />
                    <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                    <RowStyle CssClass="gridItem" />
                </asp:GridView>
                <asp:Label ID="lblTotalRegs" runat="server" Visible="False" />
                <asp:Label ID="lblInfo" runat="server" Text="Su consulta no obtuvo ningún resultado."
                    Visible="False" />
            </td>
        </tr>        
    </table>
     </asp:View>
     <asp:View ID="vwDatos" runat="server">
              <table style="width: 100%">
                  <asp:Panel ID="PanelFinal" runat="server">
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
                              <td style="text-align: center; font-size: large;">
                                  <asp:Label ID="lblMensaje" runat="server" Text="Datos Grabados Correctamente" Style="color: #FF3300"></asp:Label>
                              </td>
                          </tr>
                          <tr>
                              <td style="text-align: center; font-size: large;">
                                  <br />
                                  <br />
                              </td>
                          </tr>
                      </table>
                  </asp:Panel>
              </table>
          </asp:View>
      </asp:MultiView>
    <uc1:mensajeGrabar ID="ctlMensaje" runat="server"/>
    <asp:Panel ID="panelProceso" runat="server" Width="100%">
        <uc2:procesoContable ID="ctlproceso" runat="server" />  
    </asp:Panel>
</asp:Content>
