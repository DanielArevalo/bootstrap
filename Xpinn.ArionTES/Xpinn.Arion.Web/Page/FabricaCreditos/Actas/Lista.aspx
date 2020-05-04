<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" Title=".: Xpinn - Credito :." %>
<%@ Register src="../../../General/Controles/fecha.ascx" tagname="fecha" tagprefix="uc1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td>
                <asp:Panel ID="pConsulta" runat="server">
                <table id="tbCriterios" border="0" cellpadding="5" cellspacing="0" width="100%">
                   <tr>
                       <td class="tdI">
                           <asp:Panel ID="Panel1" runat="server">
                               <table style="width:100%;">
                                   <tr>
                                       <td class="logo" style="text-align:left" colspan="2">
                                           &nbsp;</td>
                                       <td style="text-align:left">
                                           &nbsp;</td>
                                   </tr>
                                   <tr>
                                       <td class="logo" style="width: 148px; text-align:left">
                                           Número Acta</td>
                                       <td style="width: 138px; text-align:left">
                                           Fecha Acta </td>
                                       <td style="text-align:left">
                                           &nbsp;</td>
                                   </tr>
                                   <tr>
                                       <td class="logo" style="width: 148px; text-align:left">
                                            <asp:TextBox ID="txtacta" runat="server" AutoPostBack="True" CssClass="textbox" 
                                                 OnTextChanged="txtFechaaprobacion_TextChanged" 
                                                ValidationGroup="vgGuardar" Width="148px"></asp:TextBox>
                                       </td>
                                       <td style="width: 138px; text-align:left">
                                           <asp:TextBox ID="txtFechaacta" runat="server" AutoPostBack="True" 
                                               CssClass="textbox" MaxLength="1" OnTextChanged="txtFechaaprobacion_TextChanged" 
                                               ValidationGroup="vgGuardar" Width="148px" />
                                           <asp:CalendarExtender ID="txtFechaacta_CalendarExtender" runat="server" 
                                               Enabled="True" Format="MM/dd/yyyy" TargetControlID="txtFechaacta">
                                           </asp:CalendarExtender>
                                          
                                           <br />
                                       </td>
                                       <td style="text-align:left">
                                           &nbsp;<br />
                                       </td>
                                   </tr>
                                   
                               </table>
                           </asp:Panel>
                       </td>
                   </tr>
                </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td><hr width="100%" noshade></td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="gvLista" runat="server" Width="50%"  GridLines="Horizontal" 
                    AutoGenerateColumns="False" OnRowDataBound="gvLista_RowDataBound" 
                    OnRowDeleting="gvLista_RowDeleting" AllowPaging="True" 
                    OnPageIndexChanging="gvLista_PageIndexChanging" 
                    onselectedindexchanged="gvLista_SelectedIndexChanged" 
                    onrowediting="gvLista_RowEditing" PageSize="20" 
                    HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager" 
                    RowStyle-CssClass="gridItem"  DataKeyNames="acta" onload="gvLista_Load" >
                    <Columns>
                        <asp:BoundField DataField="acta" HeaderStyle-CssClass="gridColNo" 
                            ItemStyle-CssClass="gridColNo" >
                            <HeaderStyle CssClass="gridColNo"></HeaderStyle>
                            <ItemStyle CssClass="gridColNo" HorizontalAlign="Left"></ItemStyle>
                        </asp:BoundField>
                        <asp:TemplateField HeaderStyle-CssClass="gridIco"  ItemStyle-CssClass="gridIco">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnInfo" runat="server" CommandName="Select" 
                                    ImageUrl="~/Images/gr_info.jpg" ToolTip="Detalle" Width="16px"/>
                            </ItemTemplate>
                            <HeaderStyle CssClass="gridIco"></HeaderStyle>
                            <ItemStyle CssClass="gridIco"></ItemStyle>
                        </asp:TemplateField>                       
                        <asp:BoundField DataField="acta" HeaderText="Número Acta" >
                        </asp:BoundField>
                        <asp:BoundField DataField="fechaacta" HeaderText="Fecha Acta" >
                        <ItemStyle HorizontalAlign="center" />
                        </asp:BoundField>
                    </Columns>
                    <HeaderStyle CssClass="gridHeader"></HeaderStyle>
                    <PagerStyle CssClass="gridPager"></PagerStyle>
                    <RowStyle CssClass="gridItem"></RowStyle>
                </asp:GridView>
                <asp:Label ID="lblTotalRegs" runat="server" Visible="False"/>
            </td>
        </tr>
    </table>    
</asp:Content>