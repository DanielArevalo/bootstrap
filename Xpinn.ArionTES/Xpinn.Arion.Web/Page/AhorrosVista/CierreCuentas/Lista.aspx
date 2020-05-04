<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" EnableEventValidation="false"%>

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

    <asp:Panel ID="pConsulta" runat="server">            
        <table cellpadding="5" cellspacing="0" style="width: 80%">
           
            <tr>
                <td style="height: 15px; text-align: left; font-size: x-small;" colspan="4">
                    <strong>Criterios de Búsqueda:</strong></td>
                <td style="height: 15px; text-align: left; font-size: x-small; width: 4px;">
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="height: 15px; text-align: left;">
                    Número. Cuenta<br />
                    <asp:TextBox ID="txtCodigo" runat="server" CssClass="textbox" Width="119px"></asp:TextBox>
                </td>
                <td style="height: 25px; text-align: left; width: 21%;">
                    Nombre Cliente<br />
                    <asp:TextBox ID="txtCliente" runat="server" CssClass="textbox" 
                        Width="157px"></asp:TextBox>
                </td>
                 <td style="height: 25px; text-align: left; width: 21%;">
                    Id.Cliente<br />
                    <asp:TextBox ID="txtIdentificacion" runat="server" CssClass="textbox" 
                        Width="157px"></asp:TextBox>
                </td>
                <td style="height: 25px; text-align: left; width: 21%;">
                    Código de nómina<br />
                    <asp:TextBox ID="txtCodigoNomina" runat="server" CssClass="textbox" 
                        Width="157px"></asp:TextBox>
                </td>
                <td colspan="2" style="height: 15px; text-align: left;">
                    Tipo/Linea de Ahorro<br />
                    <asp:DropDownList ID="ddlTipoLinea" runat="server" AppendDataBoundItems="True" 
                        CssClass="textbox" Width="150px" />
                </td>
                <td style="height: 15px; text-align: left; width: 27%;">
                    Oficina<br />
                    <asp:DropDownList ID="ddlOficina" runat="server" CssClass="textbox" />
                </td>            
            </tr>
            <tr>
                <td>Con Tarjeta. <asp:CheckBox ID="chkTarjeta" runat="server" /></td>
                <td>Cuentas Cerradas <asp:CheckBox ID="chkCtaCerradas" runat="server" /></td>
            </tr>
            <tr>
                <td class="tdI" style="text-align: left; width: 28%;">
                </td>
                <td class="tdI" style="text-align: left; width: 21%;">
                </td>
                <td class="tdI" style="text-align: left; width: 21%;">
                </td>
                <td class="tdI" style="text-align: left; width: 2%;">
                </td>
                <td class="tdI" style="text-align: left; width: 4px;">
                    &nbsp;</td>
            </tr>
            
        </table>
    </asp:Panel>
    <table style="width:100%">
        <tr>  
            <td>
                <asp:GridView ID="gvLista" runat="server" Width="100%" AutoGenerateColumns="False"
                   AllowPaging="True" GridLines="Horizontal" ShowHeaderWhenEmpty="True"
                   OnPageIndexChanging="gvLista_PageIndexChanging" OnRowDeleting="gvLista_RowDeleting"
                   OnRowEditing="gvLista_RowEditing" OnSelectedIndexChanged="gvLista_SelectedIndexChanged"
                   OnRowDataBound="gvLista_RowDataBound" DataKeyNames="numero_cuenta"
                   style="font-size: xx-small" PageSize="20" Height="10%">
                   <Columns>
                       <asp:TemplateField HeaderStyle-CssClass="gridIco">
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
                       <asp:TemplateField HeaderStyle-CssClass="gridIco">
                           <ItemTemplate>
                               <asp:ImageButton ID="btnEliminar" runat="server" CommandName="Delete" ImageUrl="~/Images/gr_elim.jpg"
                                   ToolTip="Eliminar" Width="16px" />
                           </ItemTemplate>
                           <HeaderStyle CssClass="gridIco"></HeaderStyle>
                       </asp:TemplateField>
                       <asp:BoundField DataField="numero_cuenta" HeaderText="Num. Cuenta">
                           <ItemStyle HorizontalAlign="Left" Width="100px" />
                       </asp:BoundField>
                       <asp:BoundField DataField="nom_linea" HeaderText="Línea" >
                           <ItemStyle HorizontalAlign="Left" Width="50px" />
                       </asp:BoundField>
                       <asp:BoundField DataField="cod_persona" HeaderText="Cod. Persona">
                           <ItemStyle HorizontalAlign="Center" />
                       </asp:BoundField>
                       <asp:BoundField DataField="cod_nomina" HeaderText="Código de nómina">
                           <ItemStyle HorizontalAlign="Center" />
                       </asp:BoundField>
                       <asp:BoundField DataField="nom_oficina" HeaderText="Oficina">
                           <ItemStyle HorizontalAlign="Center" />
                       </asp:BoundField>
                       <asp:BoundField DataField="cod_destino" HeaderText="Destino">
                           <ItemStyle HorizontalAlign="Center" />
                       </asp:BoundField>
                       <asp:BoundField DataField="modalidad" HeaderText="Modalidad">
                           <ItemStyle HorizontalAlign="Center" />
                       </asp:BoundField>
                       <asp:BoundField DataField="nom_estado" HeaderText="Estado">
                           <ItemStyle HorizontalAlign="Center" />
                       </asp:BoundField>
                       <asp:BoundField DataField="fecha_apertura" HeaderText="Fec. Apertura" DataFormatString="{0:d}" >
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
                       <asp:BoundField DataField="retencion" HeaderText="Retención">
                           <ItemStyle HorizontalAlign="Center" />
                       </asp:BoundField>
                       <asp:BoundField DataField="Tarjeta" HeaderText="Con Tarjeta">
                           <ItemStyle HorizontalAlign="Center" />
                       </asp:BoundField>
                         <asp:BoundField DataField="Num_Tarjeta" HeaderText="Numero de Tarjeta">
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
