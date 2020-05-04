<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" 
    CodeFile="Lista.aspx.cs" Inherits="Lista" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:MultiView ID="mvPrincipal" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwCanje" runat="server">
            <asp:Panel ID="pConsulta" runat="server">
                <table id="tbCriterios" border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td class="tdI">
                            <asp:Panel ID="Panel1" runat="server">
                                <table style="width:100%;">
                                    <tr>
                                        <td style="text-align:left" colspan="2">
                                            
                                        </td>
                                        <td style="text-align:left; width: 330px;">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td style="text-align:left">
                                            Fecha :  <ucFecha:fecha ID="txtFechaRealiza" runat="server" CssClass="textbox" />
                                            </td>
                                        <td style="text-align:left; width: 43px;">
                                            Banco:
                                            <asp:DropDownList ID="ddlBancos" runat="server" CssClass="textbox" 
                                                Width="230px" AppendDataBoundItems="True">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="text-align:left; width:330px;">
                                             Fecha Operacion:  <ucFecha:fecha ID="txtFechaInicial" runat="server" CssClass="textbox" />
                                         a
                                            <ucFecha:fecha ID="txtFechaFinal" runat="server" CssClass="textbox" />
                                        </td>
                                    </tr>
                                    <tr>
                                       <td style="width: 10px">
                                            <asp:CheckBox ID="chkChequesSinConsignar" runat="server" AutoPostBack="True" 
                                                OnCheckedChanged="chkChequesSinConsignar_CheckedChanged" 
                                                Text="Mostrar Cheques Sin Consignar" Width="160px" />
                                        </td>
                                        <td style="text-align:left; width: 43px;">
                                            Cuentas:
                                            <asp:DropDownList ID="ddlcuentas" runat="server" CssClass="textbox" 
                                                Width="230px" AppendDataBoundItems="True">
                                            </asp:DropDownList>
                                        </td>
                                        </tr>
                                </table>
                           
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Panel ID="pDatos" runat="server">                                       
                <table style="width: 100%"> 
                    <tr>
                        <td>
                            <asp:GridView ID="gvLista" runat="server" Width="100%" 
                                OnRowDataBound="gvLista_RowDataBound" 
                                AutoGenerateColumns="False"   Style="font-size: x-small"
                                PageSize="20" HeaderStyle-CssClass="gridHeader" 
                                PagerStyle-CssClass="gridPager" RowStyle-CssClass="gridItem"  
                                DataKeyNames="consignacion" >
                                <Columns>
                                   <asp:BoundField DataField="consignacion" HeaderText="Cod" ><ItemStyle HorizontalAlign="center" /></asp:BoundField>
                                    <asp:BoundField DataField="cod_ope" HeaderText="Cod.Ope" ><ItemStyle HorizontalAlign="center" /></asp:BoundField>             
                                    
                                    <asp:BoundField DataField="fec_ope" HeaderText="Fec. Ope" DataFormatString="{0:d}" ><ItemStyle HorizontalAlign="center" /></asp:BoundField>
                                    <asp:BoundField DataField="identificacion" HeaderText="Identificación" ><ItemStyle HorizontalAlign="center" /></asp:BoundField>
                                    <asp:BoundField DataField="nombre" HeaderText="Nombre" ><ItemStyle HorizontalAlign="left" /></asp:BoundField>
                                    <asp:BoundField DataField="nom_banco" HeaderText="Banco" ><ItemStyle HorizontalAlign="center" /></asp:BoundField>
                                    <asp:BoundField DataField="numcuenta" HeaderText="Num. Cuenta" ><ItemStyle HorizontalAlign="Center" /></asp:BoundField>
                                    <asp:BoundField DataField="valor" HeaderText="Valor" DataFormatString="{0:n0}" ><ItemStyle HorizontalAlign="center" /></asp:BoundField>
                                    <asp:BoundField DataField="nom_moneda" HeaderText="Moneda" ><ItemStyle HorizontalAlign="center" /></asp:BoundField>
                                       <asp:TemplateField HeaderText="Estado Cheque">
                                           <EditItemTemplate>
                                               <asp:TextBox ID="Txtestadocheque" runat="server" Text='<%# Bind("estado_cheque") %>'></asp:TextBox>
                                           </EditItemTemplate>
                                           <ItemTemplate>
                                               <asp:Label ID="Lblestadocheque" runat="server" Text='<%# Bind("estado_cheque") %>'></asp:Label>
                                           </ItemTemplate>
                                           <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderStyle-CssClass="gridIco"  ItemStyle-CssClass="gridIco" HeaderText="Seleccionar"><ItemTemplate><asp:CheckBox ID="chkCanjear" runat="server" Checked="false"/></ItemTemplate><HeaderStyle CssClass="gridIco"></HeaderStyle><ItemStyle CssClass="gridIco"></ItemStyle></asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-CssClass="gridIco"  ItemStyle-CssClass="gridIco" HeaderText="Estado"><ItemTemplate><asp:Label ID="lblTipo" runat="server" Text='<%# Bind("Estado") %>' Visible="false"></asp:Label>
                                        <cc1:DropDownListGrid ID="ddlestado" appenddatabounditems="True"  
                                            commandargument="<%#Container.DataItemIndex %>" 
                                            OnSelectedIndexChanged="ddlestado_SelectedIndexChanged" runat="server" 
                                            CssClass="textbox" Width="130px" AutoPostBack="True" ></cc1:DropDownListGrid></ItemTemplate><HeaderStyle CssClass="gridIco"></HeaderStyle><ItemStyle CssClass="gridIco"></ItemStyle></asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-CssClass="gridIco"  ItemStyle-CssClass="gridIco" HeaderText="Motivo Devolucion"><ItemTemplate><cc1:DropDownListGrid ID="ddlmotivo" appenddatabounditems="True"  runat="server" CssClass="textbox" Width="130px" CommandArgument="<%#Container.DataItemIndex %>" DataValueField="cod_motivo" DataTextField="descripcion" /></ItemTemplate><HeaderStyle CssClass="gridIco"></HeaderStyle><ItemStyle CssClass="gridIco"></ItemStyle></asp:TemplateField>
                                    <asp:TemplateField HeaderText="Observaciones"><ItemTemplate><asp:TextBox ID="txtobservaciones" runat="server"  Visible="true" Width="80px"/></ItemTemplate><ItemStyle HorizontalAlign="left" /></asp:TemplateField>
                                </Columns>
                                <HeaderStyle CssClass="gridHeader"></HeaderStyle>
                                <PagerStyle CssClass="gridPager"></PagerStyle>
                                <RowStyle CssClass="gridItem"></RowStyle>
                            </asp:GridView>
                            <asp:Label ID="lblTotalRegs" runat="server" Visible="False"/>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </asp:View>
       
    </asp:MultiView> 
    <uc4:mensajeGrabar ID="ctlMensaje" runat="server"/>
</asp:Content>

