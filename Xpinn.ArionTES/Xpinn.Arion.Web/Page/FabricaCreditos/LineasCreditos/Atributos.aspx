<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Atributos.aspx.cs" Inherits="Nuevo" Title=".: Xpinn - LineasCredito :." %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/tnumero.ascx" TagName="txtPesos" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/decimalesGridRow.ascx" TagName="decimalesGridRow" TagPrefix="uc1" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <asp:UpdatePanel runat="server" RenderMode="Inline" UpdateMode="Conditional">
    <ContentTemplate>
    
    <table border="0" cellpadding="5" cellspacing="0" width="100%">
        <tr>
            <td style="text-align: left">
                Linea de Credito
            </td>
            <td style="text-align: left">
                <asp:Label ID="lblCodLineaCredito" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="text-align: left">
                Descripción Grupo<br />
            </td>
            <td style="text-align: left">                
                <asp:TextBox ID="txtNombreGrupo" runat="server" CssClass="textbox" MaxLength="128"
                    Height="16px" Width="234px" />
                &nbsp;
                <asp:TextBox ID="txtCodRango" runat="server" CssClass="textbox"
                    Height="16px" Visible="false" />
            </td>
        </tr>
    </table>
    <table width="100%">
        <tr>
            <td>
                <span>
                    <asp:Button ID="btnDetalleTopes" runat="server" CssClass="btn8" OnClick="btnDetalleTopes_Click"
                        OnClientClick="btnDetalle_Click" Text="+ Adicionar Detalle" />
                    <br />
                    <asp:GridView ID="gvTopes" runat="server" AutoGenerateColumns="False" BackColor="White"
                        BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="0" ForeColor="Black"
                        OnRowDataBound="gvTopes_RowDataBound" ShowFooter="True" Style="font-size: xx-small;
                        margin-right: 0px;" Width="39%" PageSize="2" DataKeyNames="idtope" OnRowDeleting="gvTopes_RowDeleting">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:TemplateField>                                
                                <ItemTemplate>
                                    <span>
                                        <asp:Label ID="lbltope" runat="server" Text='<%# Bind("idtope") %>' Visible="False"></asp:Label>
                                    </span>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/gr_elim.jpg" ShowDeleteButton="True" />
                            <asp:TemplateField HeaderText="Descripcion" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <span>
                                        <asp:Label ID="lbldescripciontope" runat="server" Text='<%# Bind("tipo_tope") %>'
                                            Visible="False"></asp:Label>
                                        <cc1:DropDownListGrid ID="ddlDescrpTope" runat="server" AppendDataBoundItems="True"
                                            AutoPostBack="True" CommandArgument="<%#Container.DataItemIndex %>" CssClass="textbox"
                                            Style="font-size: xx-small; text-align: left" Width="120px">
                                        </cc1:DropDownListGrid>
                                    </span>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="120px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Tope Minimo" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <span>
                                        <asp:TextBox ID="txttopeminimo" runat="server" CssClass="textbox" Style="font-size: xx-small;
                                            text-align: left" Text='<%# Bind("minimo") %>' Width="100px"></asp:TextBox>
                                    </span>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" Width="100px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Tope Maximo" ItemStyle-HorizontalAlign="Right">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox8" runat="server"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <span>
                                        <asp:TextBox ID="txttopemaximo" runat="server" CssClass="textbox" Style="font-size: xx-small;
                                            text-align: left" Text='<%# Bind("maximo") %>' Width="100px"></asp:TextBox>
                                    </span>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle CssClass="gridHeader" />
                        <HeaderStyle CssClass="gridHeader" />
                        <RowStyle CssClass="gridItem" />
                        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                        <SortedAscendingCellStyle BackColor="#FBFBF2" />
                        <SortedAscendingHeaderStyle BackColor="#848384" />
                        <SortedDescendingCellStyle BackColor="#EAEAD3" />
                        <SortedDescendingHeaderStyle BackColor="#575357" />
                    </asp:GridView>
                </span>
            </td>
        </tr>
        <tr>
            <td style="margin-left: 80px">
                <span>
                    <br />
                    <asp:Button ID="btnDetalle" runat="server" CssClass="btn8" OnClick="btnDetalle_Click"
                        OnClientClick="btnDetalle_Click" Text="+ Adicionar Detalle" />                   
                    <asp:GridView ID="gvAtributos" runat="server" AutoGenerateColumns="False" BackColor="White"
                        BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="0" ForeColor="Black"
                        OnRowDataBound="gvAtributos_RowDataBound" OnRowDeleting="gvAtributos_RowDeleting"
                        ShowFooter="True" Style="font-size: xx-small; margin-right: 0px;" Width="80%"
                        PageSize="2" DataKeyNames="cod_atr">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:TemplateField>                                
                                <ItemTemplate>
                                    <span>
                                        <asp:Label ID="lblcodatributo" runat="server" Text='<%# Bind("cod_atr") %>' Visible="False"></asp:Label>
                                    </span>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/gr_elim.jpg" ShowDeleteButton="True" />
                            <asp:TemplateField HeaderText="DescripcionAtributo" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lbldescripcionatributo" runat="server" Text='<%# Bind("descripcion") %>'
                                        Visible="False"></asp:Label>
                                    <cc1:DropDownListGrid ID="ddlAtributos" runat="server" AppendDataBoundItems="True"
                                        CommandArgument="<%#Container.DataItemIndex %>" CssClass="textbox" Style="font-size: xx-small;
                                        text-align: left" Width="120px" >
                                    </cc1:DropDownListGrid>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="120px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Forma Calculo" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <span>
                                        <asp:Label ID="lblformacalculo" runat="server" Text='<%# Bind("formacalculo") %>'
                                            Visible="False"></asp:Label>
                                        <cc1:DropDownListGrid ID="ddlFormaCalculo" runat="server" AppendDataBoundItems="True"
                                            CommandArgument="<%#Container.DataItemIndex %>" CssClass="textbox" Style="font-size: xx-small;
                                            text-align: left" Width="120px" AutoPostBack="True" OnSelectedIndexChanged="ddlFormaCalculo_SelectedIndexChanged">
                                        </cc1:DropDownListGrid>
                                    </span>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" Width="100px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Tasa" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <span>
                                        <asp:TextBox ID="txttasa" runat="server" CssClass="textbox" Style="font-size: xx-small;
                                            text-align: left" Text='<%# Bind("tasa") %>' Width="100px"></asp:TextBox>
                                    </span>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="TipoTasa">
                                <ItemTemplate>
                                    <span>
                                        <asp:Label ID="lbltipotasa" runat="server" Text='<%# Bind("tipotasa") %>' Visible="False"></asp:Label>
                                        <cc1:DropDownListGrid ID="ddltipotasa" runat="server" AppendDataBoundItems="True"
                                            CommandArgument="<%#Container.DataItemIndex %>" CssClass="textbox" Style="font-size: xx-small;
                                            text-align: left" Width="120px">
                                        </cc1:DropDownListGrid>
                                    </span>
                                </ItemTemplate>                                
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Desviación">
                                <ItemTemplate>
                                    <span>
                                        <asp:TextBox ID="txtDesviacion" runat="server" CssClass="textbox" Style="font-size: xx-small;
                                            text-align: left" Text='<%# Bind("desviacion") %>' Width="100px"></asp:TextBox>
                                    </span>
                                </ItemTemplate>                               
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="TipoHistorico">
                                <ItemTemplate>
                                    <span>
                                        <asp:Label ID="lbltipohistorico" runat="server" Text='<%# Bind("tipo_historico") %>'
                                            Visible="False"></asp:Label>
                                        <cc1:DropDownListGrid ID="ddlTipoHistorico" runat="server" AppendDataBoundItems="True"
                                            CommandArgument="<%#Container.DataItemIndex %>" CssClass="textbox" Style="font-size: xx-small;
                                            text-align: left" Width="120px">
                                        </cc1:DropDownListGrid>
                                    </span>
                                </ItemTemplate>                                
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Cobra Mora">
                                <ItemTemplate>
                                    <span>
                                        <asp:CheckBox ID="chkCobramora" runat="server" AutoPostBack="True" Checked='<%#Convert.ToBoolean(Eval("cobra_mora"))%>' />
                                    </span>
                                </ItemTemplate>                                
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle CssClass="gridHeader" />
                        <HeaderStyle CssClass="gridHeader" />
                        <RowStyle CssClass="gridItem" />
                        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                        <SortedAscendingCellStyle BackColor="#FBFBF2" />
                        <SortedAscendingHeaderStyle BackColor="#848384" />
                        <SortedDescendingCellStyle BackColor="#EAEAD3" />
                        <SortedDescendingHeaderStyle BackColor="#575357" />
                    </asp:GridView>
                </span>
                <br />
            </td>
        </tr>        
    </table>
    </ContentTemplate>
    </asp:UpdatePanel>
    

    <script type="text/javascript" language="javascript">
        function SetFocus() {
            document.getElementById('cphMain_txtCOD_LINEA_CREDITO').focus();
        }
        window.onload = SetFocus;
    </script>
</asp:Content>
