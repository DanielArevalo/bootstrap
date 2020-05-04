<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"   CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../../../General/Controles/ctlMensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="ctl" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager> 
    <table style="width: 100%">
        <tr>
            <td style="text-align:left">
            <strong>Filtrar por:</strong>
            </td>
            </tr>
            <tr>
                <td class="tdI" style="text-align:left; width:120px">
                    Identificación<br/>
                    <asp:TextBox ID="txtIdentificacion" runat="server" CssClass="textbox" Width="110px"/>
                </td>
            </tr>
    </table>
    <table style="width: 90%">
        <tr>
            <td colspan="3">
            <div style="height:271px; overflow-x: hidden;">
                <asp:GridView ID="gvLista" runat="server" Width="100%" 
                    AutoGenerateColumns="False" ShowHeaderWhenEmpty="True">
                    <Columns>                    
                        <asp:BoundField DataField="tipo_iden" HeaderText="Tipo identificación">
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="30px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="identificacion" HeaderText="Identificación" >
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"/>
                        </asp:BoundField>
                        <asp:BoundField DataField="nombre_asociado" HeaderText="Nombre" >
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"/>
                        </asp:BoundField>
                        <asp:BoundField DataField="estado_asociado" HeaderText="Estado" >
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"/>
                        </asp:BoundField>
                        <asp:BoundField DataField="antPaso" HeaderText="Paso terminado" >
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"/>
                        </asp:BoundField>
                        <asp:BoundField DataField="sigPaso" HeaderText="Siguiente paso" >
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"/>
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Detalle Historial" ItemStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <asp:UpdatePanel ID="upDetalle" runat="server">
                                    <ContentTemplate>
                                        <asp:HiddenField ID="hfValue" runat="server" Visible="false" />
                                        <asp:ImageButton ID="btnInfo" runat="server" AutoPostBack="false" onblur="ocultarFuera()" OnClientClick="mostrarDetalle(); return false;"  ImageUrl="~/Images/gr_info.jpg"
                                            ToolTip="Detalle" /></ItemTemplate>
							            <HeaderStyle CssClass="gridIco"></HeaderStyle>
                                        <asp:PopupControlExtender ID="btnInfo_PopupControlExtender" runat="server"
                                            Enabled="True" ExtenderControlID="" TargetControlID="btnInfo"
                                            PopupControlID="panelLista" OffsetY="22">
                                        </asp:PopupControlExtender>
                                        <asp:Panel ID="panelLista" runat="server" Width="40%"
                                            BorderStyle="Solid" BorderWidth="2px" Direction="LeftToRight"
                                            ScrollBars="Auto" BackColor="#CCCCCC" Style="margin-left: -268px;">
                                            <asp:GridView ID="gvDetalle" runat="server" Width="100%" AutoGenerateColumns="False"
                                                    HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager" RowStyle-CssClass="gridItem">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Fecha">
                                                        <itemtemplate>
                                                            <asp:Label ID="lbl_fecha" runat="server" Text='<%# Bind("fecha") %>'></asp:Label>
                                                        </itemtemplate>                                                        
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Estado">
                                                        <itemtemplate>
                                                            <asp:Label ID="lbl_estado_asociado" runat="server" Text='<%# Bind("estado_asociado") %>'></asp:Label>
                                                        </itemtemplate>
                                                        <ItemStyle HorizontalAlign="Left" Width="50px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Proceso anterior">
                                                        <itemtemplate>
                                                            <asp:Label ID="lbl_antPaso" runat="server" Text='<%# Bind("antPaso") %>'></asp:Label>
                                                        </itemtemplate>                                                        
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Proceso siguiente">
                                                        <itemtemplate>
                                                            <asp:Label ID="lbl_antPaso" runat="server" Text='<%# Bind("sigPaso") %>'></asp:Label>
                                                        </itemtemplate>                                                        
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                </Columns>  
                                            </asp:GridView>
                                        </asp:Panel>
                                    </ContentTemplate>                                
                                </asp:UpdatePanel>
                            </ItemTemplate>
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
    <script>
        $(document).ready(function () {
            $("#cphMain_gvLista_gvDetalle_0").hide();
        });
        function mostrarDetalle() {
            if ($("#cphMain_gvLista_gvDetalle_0").is(":visible")){
                $("#cphMain_gvLista_gvDetalle_0").hide();
                $("#cphMain_gvLista_panelLista_0").hide();
            } else {
                $("#cphMain_gvLista_gvDetalle_0").show();
                $("#cphMain_gvLista_panelLista_0").show();
            }
        }
        function ocultarFuera() {
            $("#cphMain_gvLista_gvDetalle_0").hide();
            $("#cphMain_gvLista_panelLista_0").hide();
        }
    </script>
</asp:Content>
