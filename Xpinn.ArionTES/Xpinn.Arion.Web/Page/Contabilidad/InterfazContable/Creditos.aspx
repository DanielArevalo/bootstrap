<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Creditos.aspx.cs" Inherits="Creditos" %>

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
                   Fecha de Aplicación:
                </td>
                <td>
                   Total a Aplicar:
                </td>
            </tr>
            <tr>
                <td>
                    <asp:DropDownList ID="ddlPeriodo" runat="server" AppendDataBoundItems="true" CssClass="textbox">
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
                    <asp:TextBox ID="txtTotal" runat="server" CssClass="textbox" Style="text-align:right" Enabled="False" />
                </td>
            </tr>
        </table>
        <br />
        <hr />
        <span>Listado de Créditos:</span><br />
        <asp:GridView ID="gvLista" runat="server" Width="100%" AutoGenerateColumns="False"
            AllowPaging="False" PageSize="20" GridLines="Horizontal" ShowHeaderWhenEmpty="True"
            OnPageIndexChanging="gvLista_PageIndexChanging" DataKeyNames="iden" 
            style="font-size: x-small" onrowdatabound="gvLista_RowDataBound">
            <Columns>                           
                <asp:BoundField DataField="iden" HeaderText="#" >
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="iden_prestamo" HeaderText="Num.Cre" >
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
                <asp:BoundField DataField="total" HeaderText="Valor a Aplicar" DataFormatString="{0:n}" >
                    <ItemStyle HorizontalAlign="Right"/>
                </asp:BoundField>                
                <asp:BoundField DataField="centrocosto" HeaderText="C.Costo" >
                    <ItemStyle HorizontalAlign="Center" Width="30px" />
                </asp:BoundField>
                <asp:BoundField DataField="valorprestamo" HeaderText="Monto" DataFormatString="{0:n}" >
                    <ItemStyle HorizontalAlign="Right"/>
                </asp:BoundField> 
                <asp:BoundField DataField="valorcuota" HeaderText="Cuota" DataFormatString="{0:n}" >
                    <ItemStyle HorizontalAlign="Right"/>
                </asp:BoundField> 
                <asp:BoundField DataField="saldoprestamo" HeaderText="Saldo" DataFormatString="{0:n}" >
                    <ItemStyle HorizontalAlign="Right"/>
                </asp:BoundField>  
                <asp:TemplateField HeaderText="Cod.Persona">
                    <ItemTemplate>
                        <asp:Label ID="lblCodPersona" runat="server" Text="" />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="No.Radicación">
                    <ItemTemplate>
                        <cc1:DropDownListGrid ID="ddlNumeroRadicacion" runat="server" Width="100px"
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
        <asp:Label ID="lblTotalRegs" runat="server" Visible="False" />
        <asp:Label ID="lblInfo" runat="server" Text="Su consulta no obtuvo ningun resultado."
            Visible="False" />
        <br />
    </asp:Panel>
    <asp:Panel ID="pProceso" runat="server" Width="100%">
        <uc2:procesoContable ID="ctlproceso" runat="server" />  
    </asp:Panel> 
    <uc4:mensajeGrabar ID="ctlMensaje" runat="server"/>
</asp:Content>
