<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Lista.aspx.cs" Inherits="PreAnalisis" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:Panel ID="pConsulta" runat="server">
        <table cellpadding="5" cellspacing="0" style="width: 100%">
            <tr>
                <td class="tdI">
                    <br />
                </td>
                <td class="tdD">
                    <br />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="width: 100%;">
                <tr>
                    <td style="text-align: center">
                        <asp:Label ID="lblTituloPreAnalisis" runat="server" Text="Pre-Análisis" 
                            Visible="False"></asp:Label>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;<asp:GridView ID="gvParametrizacion" runat="server" Width="100%" AutoGenerateColumns="False"
                            AllowPaging="True" PageSize="20" GridLines="Horizontal" ShowHeaderWhenEmpty="True"
                            OnSelectedIndexChanged="gvParametrizacion_SelectedIndexChanged" OnRowDataBound="gvParametrizacion_RowDataBound"
                            OnRowDeleting="gvParametrizacion_RowDeleting" OnRowEditing="gvParametrizacion_RowEditing"
                            OnPageIndexChanging="gvParametrizacion_PageIndexChanging" HorizontalAlign="Center" DataKeyNames="IDP">
                            <Columns>                                
                                <asp:TemplateField HeaderStyle-CssClass="gridIco">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnInfo" runat="server" CommandName="Select" ImageUrl="~/Images/gr_info.jpg"
                                            ToolTip="Detalle" Width="16px" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-CssClass="gridIco">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnEditar" runat="server" CommandName="Edit" ImageUrl="~/Images/gr_edit.jpg"
                                            ToolTip="Editar" Width="16px" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-CssClass="gridIco">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnBorrar" runat="server" CommandName="Delete" ImageUrl="~/Images/gr_elim.jpg"
                                            ToolTip="Borrar" Width="16px" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="IDP" HeaderText="ID" ItemStyle-Width="0px" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="MINIMO" HeaderText="MINIMO" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n0}"/>
                                <asp:BoundField DataField="MAXIMO" HeaderText="MAXIMO" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n0}"/>
                                <asp:BoundField DataField="APRUEBA" HeaderText="APRUEBA" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="MUESTRA" HeaderText="MUESTRA" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="MENSAJE" HeaderText="MENSAJE" ItemStyle-HorizontalAlign="Center" />
                            </Columns>
                            <HeaderStyle CssClass="gridHeader" />
                            <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                            <RowStyle CssClass="gridItem" />
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblTotalRegs" runat="server" Visible="False" />
                        <asp:Label ID="lblInfo" runat="server" Text="Su consulta no obtuvo ningún resultado."
                            Visible="False" />
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center">
                        <asp:Label ID="lblTituloCentrales" runat="server" Text="Centrales de riesgo" Visible="False"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:GridView ID="gvCentrales" runat="server" Width="100%" AutoGenerateColumns="False"
                            AllowPaging="True" PageSize="20" GridLines="Horizontal" ShowHeaderWhenEmpty="True"
                            OnSelectedIndexChanged="gvCentrales_SelectedIndexChanged" OnRowDataBound="gvCentrales_RowDataBound"
                            OnRowDeleting="gvCentrales_RowDeleting" OnRowEditing="gvCentrales_RowEditing"
                            OnPageIndexChanging="gvCentrales_PageIndexChanging" HorizontalAlign="Center" DataKeyNames="IDC">
                            <Columns>                                
                                <asp:TemplateField HeaderStyle-CssClass="gridIco">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnInfo" runat="server" CommandName="Select" ImageUrl="~/Images/gr_info.jpg"
                                            ToolTip="Detalle" Width="16px" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-CssClass="gridIco">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnEditar" runat="server" CommandName="Edit" ImageUrl="~/Images/gr_edit.jpg"
                                            ToolTip="Editar" Width="16px" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-CssClass="gridIco">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnBorrar" runat="server" CommandName="Delete" ImageUrl="~/Images/gr_elim.jpg"
                                            ToolTip="Borrar" Width="16px" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="IDC" HeaderText="ID" ItemStyle-Width="0px" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="CENTRAL" HeaderText="CENTRAL" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="VALOR" HeaderText="VALOR" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n0}"/>
                                <asp:BoundField DataField="COBRA" HeaderText="COBRA IVA" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="PORCENTAJE" HeaderText="PORCENTAJE IVA" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="VALORIVA" HeaderText="VALOR IVA" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n0}"/>
                            </Columns>
                            <HeaderStyle CssClass="gridHeader" />
                            <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                            <RowStyle CssClass="gridItem" />
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblTotalRegsCentrales" runat="server" Visible="False" />
                        <asp:Label ID="lblInfoCentrales" runat="server" Text="Su consulta no obtuvo ningún resultado."
                            Visible="False" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <br />
</asp:Content>
