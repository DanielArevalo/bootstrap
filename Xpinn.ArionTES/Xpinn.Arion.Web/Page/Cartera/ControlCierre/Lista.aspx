<%@ Page Title=".: Control Cierres :." Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../../../General/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc1" %>
<%@ Register Src="../../../General/Controles/fecha.ascx" TagName="fecha" TagPrefix="uc1" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>
<script runat="server">
</script>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel runat="server" ChildrenAsTriggers="true">
        <ContentTemplate>
            <asp:Panel ID="pConsulta" runat="server" Visible="true" HorizontalAlign="Center">
                <table id="tbCriterios" border="0" cellpadding="5" cellspacing="0" width="100%">
                    <tr>
                        <td style="font-weight: normal; width: 230px;">Tipo de Cierre<br />
                            <asp:DropDownList ID="ddlTipoCierre" runat="server" CssClass="textbox"
                                Height="24px" AutoPostBack="True" Width="230px">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 150px;">Fecha de realización
                     <asp:TextBox ID="txtFechaCierre" runat="server" AutoPostBack="True"
                         CssClass="textbox" MaxLength="1" Width="150px" />
                            <asp:CalendarExtender ID="txtFechaCierre_CalendarExtender" runat="server"
                                Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtFechaCierre">
                            </asp:CalendarExtender>
                        </td>
                        <td style="width: 150px;">Periodo de cierre
                            <asp:DropDownList ID="ddlFechaPeriodo" runat="server" CssClass="textbox"
                                Height="24px" AutoPostBack="True" Width="150px">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 150px"><br/>
                            <asp:CheckBox ID="chkRealizados" runat="server" Text="Cierres Realizados" Checked="False" AutoPostBack="true"/>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">&nbsp;
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:MultiView ID="mvControlCierres" runat="server" ActiveViewIndex="0" Visible="true">
        <asp:View ID="vPendientes" runat="server">
            <asp:Panel ID="pPendientes" runat="server" Visible="True">
                <asp:GridView ID="gvPendientes" runat="server" AllowPaging="True" AutoGenerateColumns="False" BackColor="White"
                    BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" DataKeyNames="cod_proceso"
                    ForeColor="Black" GridLines="Vertical" Visible="true" Font-Size="Small" HorizontalAlign="Center" ShowHeaderWhenEmpty="false"
                    PageSize="20" Width="90%" OnRowDataBound="gvPendientes_RowDataBound" OnRowEditing="gvPendientes_RowEditing"
                    OnPageIndexChanging="gvPendientes_PageIndexChanging">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:TemplateField HeaderText="Gestionar">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnEditar" runat="server" CommandName="Edit"
                                    ImageUrl="~/Images/gr_edit.jpg" ToolTip="Navegar" Width="16px" /><asp:HyperLink ID="HyperConsulta" runat="server" Enabled="False"
                                        Visible="False">Ir</asp:HyperLink><asp:Label ID="lblRuta" Width="100px" runat="server" Text='<%#Bind("ruta_proceso")%>' Visible="false" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="nom_cierre_sig" HeaderText="Siguiente Proceso" />
                        <asp:BoundField DataField="nom_cierre_ant" HeaderText="Tipo de Cierre" />
                        <asp:BoundField DataField="fecha" HeaderText="Periodo de Cierre" DataFormatString="{0:d}" />
                        <asp:BoundField DataField="fecrea" HeaderText="Fecha de Realización" DataFormatString="{0:d}" />
                        <asp:BoundField DataField="estado" HeaderText="Estado del Cierre" />
                        <asp:BoundField DataField="nom_usuario" HeaderText="Usuario que Realiza" />
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
                <br />
                <asp:Label ID="lblTotalRegs" runat="server" />
            </asp:Panel>
        </asp:View>
        <asp:View ID="vRealizados" runat="server">
            <asp:Panel ID="pRealizados" runat="server">
                <div style="overflow: scroll; height: 500px; width: 100%;">
                    <asp:GridView ID="gvRealizados" runat="server" AllowPaging="True" AutoGenerateColumns="False" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None"
                        BorderWidth="1px" CellPadding="4"
                        ForeColor="Black" GridLines="Vertical"
                        PageSize="20" Width="90%" Font-Size="Small" HorizontalAlign="Center">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:BoundField DataField="nom_cierre_ant" HeaderText="Tipo de Cierre" />
                            <asp:BoundField DataField="fecha" HeaderText="Periodo de Cierre" DataFormatString="{0:d}" />
                            <asp:BoundField DataField="fecrea" HeaderText="Fecha de Realización" DataFormatString="{0:d}" />
                            <asp:BoundField DataField="estado" HeaderText="Estado del Cierre" />
                            <asp:BoundField DataField="nom_usuario" HeaderText="Usuario que Realiza" />
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
               </div>
                <br />
                <asp:Label ID="lblTotalRegs2" runat="server" />
            </asp:Panel>
        </asp:View>
    </asp:MultiView>
    <%--      </td>
        </tr>
    </table>--%>
</asp:Content>

