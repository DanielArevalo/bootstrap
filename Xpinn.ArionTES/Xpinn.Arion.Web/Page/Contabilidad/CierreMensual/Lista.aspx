<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" Title=".: Xpinn - Usuario :." %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register src="../../../General/Controles/fecha.ascx" tagname="fecha" tagprefix="uc" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:MultiView ID="mvCierreMensual" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwDatos" runat="server">
            <asp:Panel ID="pConsulta" runat="server">
                <table id="tbCriterios" border="0" cellpadding="0" cellspacing="0" width="60%">
                    <tr>
                        <td class="tdI" style="text-align:left;">
                            Fecha de corte<br />
                            <asp:DropDownList ID="ddlFechaCorte" runat="server" CssClass="dropdown" 
                                Width="158px">
                            </asp:DropDownList>
                        </td>
                        <td class="tdD" style="text-align:left;">
                            Estado<br />
                            <asp:RadioButtonList ID="RadioButtonList1" runat="server" Width="188px" 
                                AutoPostBack="True" style="font-size: x-small" 
                                RepeatDirection="Horizontal">
                                <asp:ListItem>Definitivo</asp:ListItem>
                                <asp:ListItem>Prueba</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td class="tdD" style="text-align: left;">
                            <br />
                            <asp:CheckBox ID="chkcierreporterceros" runat="server" AutoPostBack="True" 
                                style="font-size: x-small" Text="Realizar Cierre Por Terceros" Checked="true" Enabled="false" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <table border="0" cellpadding="0" cellspacing="0" width="90%">
                <tr>
                    <td><hr style="width: 100%; text-align: left" /></td>
                </tr>
                <tr>
                    <td>
                        <div style="overflow: scroll; height:400px; width:100%; margin-right: 0px;">                                  
                            <asp:GridView ID="gvLista" runat="server" 
                                AutoGenerateColumns="False" OnRowDataBound="gvLista_RowDataBound" AllowPaging="True" 
                                OnPageIndexChanging="gvLista_PageIndexChanging" PageSize="20" 
                                HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager" 
                                RowStyle-CssClass="gridItem" Width="98%" >
                                <Columns>                        
                                    <asp:TemplateField HeaderText="Código">
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtcodigo" runat="server" MaxLength="19" 
                                                Text='<%# Eval("centro_costo") %>'></asp:TextBox>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblcodigo" runat="server" Text='<%# Bind("centro_costo") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Nombre">
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtxnombrecentro" runat="server"  Width="200px"
                                                Text='<%# Bind("nom_centro") %>'></asp:TextBox>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblnomcentrocosto" runat="server" 
                                                Text='<%# Bind("nom_centro") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" Width="400px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Seleccionar">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TxtSeleccionar" runat="server"></asp:TextBox>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:CheckBox ID="chkSeleccionar" runat="server" Enabled="true" />
                                        </FooterTemplate>
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="chkSeleccionar" runat="server" Enabled="true" 
                                                AutoPostBack="True" oncheckedchanged="chkSeleccionar_CheckedChanged" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkSeleccionarcentro" runat="server" Enabled="true" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Mensaje">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TextBox1" runat="server" style="font-size: x-small"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblmensaje" runat="server" Width="500px" 
                                                style="font-size: x-small"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <HeaderStyle CssClass="gridHeader" />
                                <PagerStyle CssClass="gridPager" />
                                <RowStyle CssClass="gridItem" />
                            </asp:GridView>
                        </div>
                        <asp:Label ID="lblTotalRegs" runat="server" Visible="False"/>
                        &nbsp;
                        </td>
                </tr>
            </table>
        </asp:View>                
     </asp:MultiView> 
   
</asp:Content>