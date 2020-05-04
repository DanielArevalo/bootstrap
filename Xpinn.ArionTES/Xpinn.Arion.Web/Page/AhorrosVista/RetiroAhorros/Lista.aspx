<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <script type="text/javascript" src="../../../Scripts/gridviewScroll.min.js"></script>   
    <script type="text/javascript">

        $(document).ready(function () {
            gridviewScroll();
        });

        function gridviewScroll() {
            $('#<%=gvLista.ClientID%>').gridviewScroll({
                width: CalcularAncho(),
                height: 500,
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

    <asp:Panel ID="pConsulta" runat="server">            
        <table cellpadding="5" cellspacing="0" style="width: 70%">
           
            <tr>
                <td style="height: 15px; text-align: left; font-size: x-small;" colspan="4">
                    <strong>Criterios de Búsqueda:</strong></td>
            </tr>
            <tr>
                <td style="height: 15px; text-align: left;">
                    Cuenta<br />
                    <asp:TextBox ID="txtCodigo" runat="server" CssClass="textbox" Width="119px"></asp:TextBox>
                </td>
                <td style="height: 15px; text-align: left;">
                    Id.Encargado<br />
                    <asp:TextBox ID="txtNumeIdentificacion" runat="server" CssClass="textbox" 
                        Width="157px"></asp:TextBox>
                </td>
                <td colspan="2" style="height: 15px; text-align: left;">
                </td>
            </tr>
            <tr>
                <td class="tdI" style="text-align: left; width: 205px;">
                </td>
                <td class="tdI" style="text-align: left">
                </td>
                <td class="tdI" style="text-align: left">
                </td>
                <td class="tdI" style="text-align: left">
                </td>
            </tr>
        </table>
    </asp:Panel>
    <table style="width:100%">
        <tr>  
            <td>
                <asp:GridView ID="gvLista" runat="server" Width="80%" 
                   AutoGenerateColumns="False" GridLines="Horizontal" ShowHeaderWhenEmpty="True"
                   OnPageIndexChanging="gvLista_PageIndexChanging" 
                   OnRowEditing="gvLista_RowEditing" OnSelectedIndexChanged="gvLista_SelectedIndexChanged"
                   OnRowDataBound="gvLista_RowDataBound" DataKeyNames="numero_cuenta"
                   style="font-size: xx-small">
                   <Columns>
                       <asp:TemplateField HeaderStyle-CssClass="gridIco" Visible="False">
                           <ItemTemplate>
                               <asp:ImageButton ID="btnInfo" runat="server" CommandName="Select" ImageUrl="~/Images/gr_info.jpg"
                                   ToolTip="Detalle" Width="16px" />
                           </ItemTemplate>
                           <HeaderStyle CssClass="gridIco"></HeaderStyle>
                       </asp:TemplateField>
                       <asp:TemplateField HeaderStyle-CssClass="gridIco">
                           <ItemTemplate>
                               <asp:ImageButton ID="btnEditar" runat="server" CommandName="Edit" ImageUrl="~/Images/gr_edit.jpg"
                                   ToolTip="Editar" Width="16px" />
                           </ItemTemplate>
                           <HeaderStyle CssClass="gridIco"></HeaderStyle>
                       </asp:TemplateField>
                       <asp:TemplateField HeaderStyle-CssClass="gridIco" Visible="False">
                           <ItemTemplate>
                               <asp:ImageButton ID="btnEliminar" runat="server" CommandName="Delete" ImageUrl="~/Images/gr_elim.jpg"
                                   ToolTip="Eliminar" Width="16px" />
                           </ItemTemplate>
                           <HeaderStyle CssClass="gridIco"></HeaderStyle>
                       </asp:TemplateField>
                       <asp:BoundField DataField="numero_cuenta" HeaderText="Num. Cuenta" >
                           <ItemStyle HorizontalAlign="Left" Width="50px" />
                       </asp:BoundField>
                       <asp:BoundField DataField="nom_linea" HeaderText="Línea" >
                           <ItemStyle HorizontalAlign="Left" Width="50px" />
                       </asp:BoundField>
                       <asp:BoundField DataField="cod_persona" HeaderText="Cod. Persona" >
                           <ItemStyle HorizontalAlign="Center" />
                       </asp:BoundField>
                       <asp:BoundField DataField="nom_oficina" HeaderText="Oficina">
                           <ItemStyle HorizontalAlign="Center" />
                       </asp:BoundField>
                       <asp:BoundField DataField="cod_destino" HeaderText="Destino">
                           <ItemStyle HorizontalAlign="Center" />
                       </asp:BoundField>
                       <asp:BoundField DataField="modalidad" HeaderText="modalidad">
                           <ItemStyle HorizontalAlign="Center" />
                       </asp:BoundField>
                       <asp:BoundField DataField="estado" HeaderText="Estado">
                           <ItemStyle HorizontalAlign="Center" />
                       </asp:BoundField>
                       <asp:BoundField DataField="fecha_apertura" HeaderText="Fec. Apertura" DataFormatString="{0:d}" >
                           <ItemStyle HorizontalAlign="Center" />
                       </asp:BoundField>
                       <asp:BoundField DataField="fecha_cierre" HeaderText="Fec. Cierre" DataFormatString="{0:d}" >
                           <ItemStyle HorizontalAlign="Center" />
                       </asp:BoundField>
                       <asp:BoundField DataField="saldo_total" HeaderText="Saldo Total" DataFormatString="{0:c2}" >
                           <ItemStyle HorizontalAlign="Center" />
                       </asp:BoundField>
                       <asp:BoundField DataField="saldo_canje" HeaderText="Saldo Canje" DataFormatString="{0:c2}" >
                           <ItemStyle HorizontalAlign="Center" />
                       </asp:BoundField>                      
                       <asp:BoundField DataField="fecha_interes" HeaderText="Fec. Interes" DataFormatString="{0:d}" >
                           <ItemStyle HorizontalAlign="Center" />
                       </asp:BoundField>
                       <asp:BoundField DataField="saldo_intereses" HeaderText="Saldo Interes" DataFormatString="{0:c2}" >
                           <ItemStyle HorizontalAlign="Center" />
                       </asp:BoundField>
                       <asp:BoundField DataField="retencion" HeaderText="Retencion" DataFormatString="{0:c0}" >
                           <ItemStyle HorizontalAlign="Center" />
                       </asp:BoundField>
                    </Columns>
                    <HeaderStyle CssClass="gridHeader" />
                    <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                    <RowStyle CssClass="gridItem" />
                </asp:GridView>
                <asp:Label ID="lblTotalRegs" runat="server" Visible="False" />
                <asp:Label ID="lblInfo" runat="server" Text="Su consulta no obtuvo ningún resultado." Visible="False" />
            </td>
        </tr>        
    </table>
</asp:Content>
