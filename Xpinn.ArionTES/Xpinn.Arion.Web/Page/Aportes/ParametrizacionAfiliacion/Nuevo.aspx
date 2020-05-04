<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"   CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../../../General/Controles/ctlMensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="ctl" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager> 
    <table style="width: 100%">
     
        </table>
         <table style="width: 90%">
        <tr>
        
            <td colspan="3">
            <div style="height:271px; overflow-x: hidden;">
                <asp:GridView ID="gvLista" runat="server" Width="80%" 
                    AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" 
                    onrowdatabound="gvLista_RowDataBound" >
                    <Columns>                    
                        <asp:BoundField DataField="cod_proceso" HeaderText="Código">
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="30px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="nombre" HeaderText="Nombre" >
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="400px" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Requerido">
                            <ItemTemplate>                    
                                <asp:checkbox ID="chbRequerido" runat="server"  Checked='<%# Convert.ToBoolean(Eval("requerido")) %>' Width="40px" />
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" Width="120px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Orden">
                            <ItemTemplate>
                                <asp:Label ID="lblOrden" runat="server" Text='<%# Bind("orden") %>' Visible="false"></asp:Label>
                                <cc1:DropDownListGrid 
                                    ID="ddlOrden" runat="server" AppendDataBoundItems="True" AutoPostBack="True" CommandArgument='<%#Container.DataItemIndex %>' OnSelectedIndexChanged="ddlOrden_SelectedIndexChanged"
                                    CssClass="textbox" Style="font-size: xx-small; text-align: left" Width="120px">
                                </cc1:DropDownListGrid>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" Width="120px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Notificar Asociados">
                            <ItemTemplate>                    
                                <asp:checkbox ID="chbNotAsociado" runat="server"  Checked='<%# Convert.ToBoolean(Eval("asociado")) %>' Width="40px" />
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" Width="120px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Notificar Asesor">
                            <ItemTemplate>                    
                                <asp:checkbox ID="chbNotAsesor" runat="server"  Checked='<%# Convert.ToBoolean(Eval("asesor")) %>' Width="40px" />
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" Width="120px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Notificar Otro" ItemStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <asp:TextBox ID="txtNotOtro" runat="server" Style="font-size: xx-small; text-align: left"
                                    Text='<%# Bind("otro") %>' Width="130px" CssClass="textbox" Visible="true"/>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="center" Width="130px" />
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                    </Columns>
                    <HeaderStyle CssClass="gridHeader" />
                    <PagerStyle CssClass="gridHeader" Font-Bold="False" />   
                    <RowStyle CssClass="gridItem" />     
                </asp:GridView>
                    </div> 
            </td>
        </tr>         
    </table>
    <ctl:mensajegrabar ID="ctlMensaje" runat="server"/>
</asp:Content>
