<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" Title=".: Xpinn - Presupuesto :." %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/ctlNumero.ascx" TagName="numero" TagPrefix="uc2" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>


<%@ Register assembly="Xpinn.Util" namespace="Xpinn.Util" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">

    <br />

    <script type="text/javascript" src="../../../Scripts/gridviewScroll.min.js"></script>    

    <asp:ScriptManager ID="ScriptManager1" runat="server">
        <Scripts>
            <asp:ScriptReference Path="../../../Scripts/PopUp.js" />
        </Scripts>
    </asp:ScriptManager>

    
    <asp:Panel ID="pnlLoading" runat="server" Width="200" Height="100" HorizontalAlign="Center"
        CssClass="ModalPopup" EnableViewState="false" Style="display: none">
        <asp:Image ID="Image1" ImageUrl="../../../Images/loading.gif" runat="server"/>
        <br />Generando el Presupuesto Ejecutado...        
    </asp:Panel>
    <asp:Button ID="btnLoading" runat="server" Style="display: none" />

    <asp:MultiView ID="mvPresupuesto" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwEncabezado" runat="server">          
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" ChildrenAsTriggers="true">
            <ContentTemplate>                                        
                <table id="tbEncabezado" border="0" cellpadding="5" cellspacing="0" width="80%" style=" text-align:left">
                    <tr>
                        <td style="width: 20%; text-align: left">
                            Fecha Inicial<br />
                            <uc1:fecha ID="txtFechaIni" runat="server" CssClass="textbox" MaxLength="1" Width="148px" />
                        </td>
                        <td style="width: 20%; text-align: left">
                            Fecha Final<br />
                            <uc1:fecha ID="txtFechaFin" runat="server" CssClass="textbox" MaxLength="1" Width="148px" />
                        </td>
                        <td style="width: 50%; text-align: left">
                            Tasa<br />                            
                            <asp:TextBox ID="txtTasa" runat="server" CssClass="textbox" Width="148px" />
                            <span style="font-size: xx-small">Efectiva Anual Vencida</span>
                            <ajaxToolkit:FilteredTextBoxExtender ID="fte1" runat="server"
                                Enabled="True" FilterType="Numbers, Custom" TargetControlID="txtTasa" ValidChars=",">
                            </ajaxToolkit:FilteredTextBoxExtender>
                        </td>
                       
                    </tr>
                    <tr>
                        <td colspan="3" style=" text-align: left">
                            Observaciones<br />
                            <asp:TextBox ID="txtObser" runat="server" CssClass="textbox" Width="80%"></asp:TextBox>
                            <br />
                            <asp:TextBox ID="txtCodigo" runat="server" CssClass="textbox" Visible="False"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td  colspan="3" style="text-align: left">
                            <hr style="width: 100%;" />                                    
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" style="text-align: left">
                            Condiciones de Aplicación de la Tasa
                        </td>
                    </tr>
                    <tr>     
                        <td  colspan="3" style="text-align: left">                               
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                               <ContentTemplate>  
                                    <asp:Button ID="btnDetalle" runat="server" CssClass="btn8" OnClick="btnDetalle_Click"
                                        OnClientClick="btnDetalle_Click" Text="+ Adicionar Detalle" />
                                    <asp:GridView ID="gvLista" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                        DataKeyNames="cod_tasa_condicion" HeaderStyle-CssClass="gridHeader" OnRowDataBound="gvLista_RowDataBound"
                                        PagerStyle-CssClass="gridPager" PageSize="20" RowStyle-CssClass="gridItem" Style="font-size: x-small"
                                        Width="80%" GridLines="Horizontal" OnRowDeleting="gvLista_RowDeleting">
                                        <Columns>
                                            <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/gr_elim.jpg" ShowDeleteButton="True" />
                                            <asp:TemplateField HeaderText="Codigo" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblcodigo" runat="server" Text='<%# Bind("cod_tasa_condicion") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="120px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Condicion">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCondicion" runat="server" Text='<%# Bind("variable") %>' Visible="false"></asp:Label>
                                                    <cc1:DropDownListGrid ID="ddlCondicion" runat="server" AppendDataBoundItems="True"
                                                        CommandArgument='<%#Container.DataItemIndex %>' CssClass="textbox" Style="font-size: xx-small;
                                                        text-align: left" Width="200px">
                                                    </cc1:DropDownListGrid>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="200px" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Operador">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblOperador" runat="server" Text='<%# Bind("operador") %>' Visible="false"></asp:Label>
                                                    <cc1:DropDownListGrid ID="ddlOperador" runat="server" AppendDataBoundItems="True"
                                                        CommandArgument='<%#Container.DataItemIndex %>' CssClass="textbox" Style="font-size: xx-small;
                                                        text-align: left" Width="120px">
                                                    </cc1:DropDownListGrid>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="140px" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Valor" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtValor" runat="server" Style="font-size: xx-small; text-align: left"
                                                        Text='<%# Bind("valor") %>' Width="130px" CssClass="textbox"></asp:TextBox></ItemTemplate>
                                                <ItemStyle HorizontalAlign="center" Width="130px" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <HeaderStyle CssClass="gridHeader" />
                                        <PagerStyle CssClass="gridPager" />
                                        <RowStyle CssClass="gridItem" />
                                    </asp:GridView>
                               </ContentTemplate>
                            </asp:UpdatePanel>                                                       
                        </td>
                    </tr>
                    
                </table>
                
            </ContentTemplate>
        </asp:UpdatePanel>
       
        
        </asp:View>

    </asp:MultiView>
    <uc4:mensajeGrabar ID="ctlMensaje" runat="server"/>

</asp:Content>