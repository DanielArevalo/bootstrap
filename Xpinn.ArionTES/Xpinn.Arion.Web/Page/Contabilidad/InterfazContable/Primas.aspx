<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Primas.aspx.cs" Inherits="Primas" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>
<%@ Register Src="~/General/Controles/ctlProcesoContable.ascx" TagName="procesoContable" TagPrefix="uc2" %>


<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:Panel ID="pConsulta" runat="server">
        <br />
        <table style="width: 100%;">
            <tr>
                <td>
                   Período:
                </td>
                <td>
                   Fecha de Contabilización:
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr><td>
                    <asp:DropDownList ID="ddlPeriodo" runat="server" AppendDataBoundItems="true">
                        <asp:ListItem Value="" Text="<Seleccione un Período>" />
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:TextBox ID="txtFecha" runat="server" CssClass="textbox" Style="text-align: left"></asp:TextBox>
                    <asp:CalendarExtender ID="txtFecha_CalendarExtender" runat="server" Enabled="True"
                        TargetControlID="txtFecha" Format="dd/MM/yyyy">
                    </asp:CalendarExtender>
                    <asp:RequiredFieldValidator ID="rfvFecha" runat="server" ControlToValidate="txtFecha"
                        ErrorMessage="Campo Requerido" SetFocusOnError="True" ValidationGroup="vgGuardar"
                        ForeColor="Red" Display="Dynamic" Style="font-size: x-small" />
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
        </table>
        <br />    
        <hr />
        <div id="divLista" runat="server" style="overflow: scroll; height: 600px">
            <asp:GridView ID="gvLista" runat="server" Width="100%" AutoGenerateColumns="False"
                AllowPaging="False" PageSize="20" GridLines="Horizontal" ShowHeaderWhenEmpty="True"
                OnPageIndexChanging="gvLista_PageIndexChanging" DataKeyNames="iden" 
                style="font-size: x-small" onrowdatabound="gvLista_RowDataBound">
                <Columns>                           
                    <asp:BoundField DataField="iden" HeaderText="#" >
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>         
                    <asp:BoundField DataField="iden_pago" HeaderText="#Pago" >
                        <ItemStyle HorizontalAlign="Left" Width="30px" />
                    </asp:BoundField>                       
                    <asp:BoundField DataField="iden_empleado" HeaderText="Cod." >
                        <ItemStyle HorizontalAlign="Center" Width="30px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="identificacion" HeaderText="Identificación" >
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="nombre1" HeaderText="Nombre" >
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="apellido1" HeaderText="Apellido" >
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>                
                    <asp:BoundField DataField="iden_concepto" HeaderText="Cod.Con." >
                        <ItemStyle HorizontalAlign="Left" Width="30px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="nombre" HeaderText="Concepto" >
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="total" HeaderText="Valor" DataFormatString="{0:n}" >
                        <ItemStyle HorizontalAlign="Right"/>
                    </asp:BoundField>
                    <asp:BoundField DataField="totalempleador" HeaderText="Vr.Empleador" DataFormatString="{0:n}" >
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>                    
                    <asp:BoundField DataField="tercero" HeaderText="Tercero" >
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="centrocosto" HeaderText="C.Costo" >
                        <ItemStyle HorizontalAlign="Center" Width="30px" />
                    </asp:BoundField>                    
                    <asp:BoundField DataField="formapago" HeaderText="F.Pago" >
                        <ItemStyle HorizontalAlign="Center" Width="10px" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="Cod.Persona">
                        <ItemTemplate>
                            <asp:Label ID="lblCodPersona" runat="server" Text="" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Cod.Cuenta">
                        <ItemTemplate>
                            <cc1:DropDownListGrid ID="ddlCodCuenta" runat="server" Width="150px"
                                CommandArgument="<%#Container.DataItemIndex %>" AppendDataBoundItems="True" >
                                <asp:ListItem Value="" Text="" />
                            </cc1:DropDownListGrid>  
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="left" />
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle CssClass="gridHeader" />
                <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                <RowStyle CssClass="gridItem" />
            </asp:GridView>
        </div>
        <asp:Label ID="lblTotalRegs" runat="server" Visible="False" />
        <asp:Label ID="lblInfo" runat="server" Text="Su consulta no obtuvo ningun resultado."
            Visible="False" />
        <br />
    </asp:Panel>
    <asp:Panel ID="pFinal" runat="server">
        <table style="width: 100%;">
            <tr>
                <td style="text-align: center; font-size: large;" colspan="3">
                    <br />
                    <br />
                    <br />
                    <br />
                    Se ha realizado correctamente los comprobantes de nomina
                </td>
            </tr>
            <tr>
                <td style="text-align: center; font-size: large; width:35%">     
                    &#160;             
                </td>
                <td style="text-align: center; font-size: large;width:40%">
                    <br />
                    <asp:GridView ID="gvOperacion" runat="server" AllowPaging="False" AutoGenerateColumns="False"
                        BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="0px"
                        CellPadding="0" ForeColor="Black" GridLines="Vertical" PageSize="5" Style="font-size: x-small;
                        text-align: left;" Width="100%" DataKeyNames="cod_ope">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:BoundField DataField="cod_ope" HeaderText="Cod.Ope">
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>                                    
                            <asp:BoundField DataField="num_comp" HeaderText="No.Comp" />
                            <asp:BoundField DataField="tipo_comp" HeaderText="Tipo.Comp" />
                            <asp:BoundField DataField="observacion" HeaderText="Nombre">
                                <ItemStyle HorizontalAlign="Left" />
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
                <td style="text-align: center; font-size: large; width:35%">     
                    &#160;             
                </td>
            </tr>
            <tr>
                <td style="text-align: center; font-size: large;" colspan="3">
                    <br />
                    <br />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="pProceso" runat="server" Width="100%">
        <uc2:procesoContable ID="ctlproceso" runat="server" />  
    </asp:Panel> 
    <uc4:mensajeGrabar ID="ctlMensaje" runat="server"/>
</asp:Content>
