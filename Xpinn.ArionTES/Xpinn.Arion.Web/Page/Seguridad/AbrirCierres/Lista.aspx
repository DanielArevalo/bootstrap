<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Lista.aspx.cs" Inherits="Nuevo" Title=".: Xpinn - Abrir Cierres :." %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <br/><br/>
    <table border="0" cellpadding="5" cellspacing="0" width="80%">
        <tr>
            <td style="width: 312px">
                Fecha<br />
                <asp:TextBox ID="txtFechaIni" runat="server" CssClass="textbox" MaxLength="10"
                    Width="188px"></asp:TextBox>
                <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" PopupButtonID="Image1"
                    TargetControlID="txtFechaIni">
                </asp:CalendarExtender>
            </td>
            <td style="text-align: left">
                Tipo<br />
                <asp:DropDownList ID="DropDownList1" runat="server" CssClass="textbox" Width="189px">
                    <asp:ListItem Value="0">Seleccione un Item</asp:ListItem>
                    <asp:ListItem Value="A">A=Cierre de Aportes</asp:ListItem>
                    <asp:ListItem Value="B">B=Interfaz Nomina-Créditos</asp:ListItem>
                    <asp:ListItem Value="C">C=Cierre Contable</asp:ListItem>
                    <asp:ListItem Value="D">D=Interfaz Nomina-Aportes Patronales</asp:ListItem>
                    <asp:ListItem Value="E">E=Interfaz Nomina</asp:ListItem>
                    <asp:ListItem Value="F">F=Interfaz Nomina-Provisiones</asp:ListItem>
                    <asp:ListItem Value="G">G=Cierre Contable NIIF</asp:ListItem>
                    <asp:ListItem Value="H">H=Cierre Ahorros a la Vista</asp:ListItem>
                    <asp:ListItem Value="I">I=Provisión Ahorro Vista</asp:ListItem>
                    <asp:ListItem Value="J">J=Provisión General</asp:ListItem>
                    <asp:ListItem Value="K">K=Provisión Cdat</asp:ListItem>
                    <asp:ListItem Value="L">L=Cierre Ahorro Programado</asp:ListItem>
                    <asp:ListItem Value="M">M=Cierre Historico Cdat</asp:ListItem>
                    <asp:ListItem Value="N">N=Cierre Anual Contable</asp:ListItem>                    
                    <asp:ListItem Value="O">O=Cierre Anual Niif</asp:ListItem>
                    <asp:ListItem Value="P">P=Cierre Personas</asp:ListItem>
                    <asp:ListItem Value="Q">Q=Cierre de Servicios</asp:ListItem>  
                    <asp:ListItem Value="R">R=Cierre Cartera y Clasificación</asp:ListItem>                    
                    <asp:ListItem Value="S">S=Provisión Cartera</asp:ListItem>
                    <asp:ListItem Value="T">T=Interfa Nomina-Primas</asp:ListItem>
                    <asp:ListItem Value="U">U=Causacion</asp:ListItem>          
                    <asp:ListItem Value="V">V=Provision Programado</asp:ListItem>  
                    <asp:ListItem Value="W">W=Provision Aportes</asp:ListItem>  
                    <asp:ListItem Value="X">X=Cierre Gestión Riesgo</asp:ListItem>                     
                    <asp:ListItem Value="Y">Y=Cierre Activos Fijos</asp:ListItem>                     
                    <asp:ListItem Value="Z">Z=Cierre Segmentaciòn Créditos</asp:ListItem>
                    <asp:ListItem Value="1">Z=Provisión Obligaciones</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="width: 312px">                
                <br />
            </td>
            <td>
                &nbsp;
                <asp:Label ID="Label1" runat="server" Text="Label" Visible="false"></asp:Label>
                <asp:Label ID="Label2" runat="server" Text="Label" Visible="false"></asp:Label>
                <asp:Label ID="Label3" runat="server" Text="Label" Visible="false"></asp:Label>
                <asp:Label ID="Label4" runat="server" Text="Label" Visible="false"></asp:Label>
            </td>
            <td class="tdI">
                <br />
            </td>
        </tr>
    </table>
    <table width="100%">  
        <tr>
            <td>
                &nbsp;<br />
            </td>
        </tr>
        <tr>
            <td style="text-align:center">
                <div style="overflow:scroll; height:500px; text-align:center; width:90%">
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="gvConsolidado1" runat="server" AutoGenerateColumns="False" HorizontalAlign="Center"
                            GridLines="Horizontal" PageSize="3" ShowHeaderWhenEmpty="True" Visible="True"  OnRowDeleting="gvdeducciones_RowDelete"
                            style="font-size:small" Width="100%" OnRowDataBound="gvConsolidado1_RowDataBound">
                            <Columns>
                               <asp:TemplateField HeaderStyle-CssClass="gridIco" ItemStyle-CssClass="gridIco">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnEditar" runat="server" CommandName="Delete" ImageUrl="~/Images/gr_elim.jpg" ToolTip="Modificar" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="fecha" HeaderText="Fecha" />
                                <asp:BoundField DataField="tipo" HeaderText="Tipo" ItemStyle-HorizontalAlign="Left" />
                                <asp:BoundField DataField="estado" HeaderText="Estado" />
                                <asp:BoundField DataField="campo1" HeaderText="Campo1" />
                                <asp:BoundField DataField="campo2" HeaderText="Campo2" />
                                <asp:TemplateField HeaderStyle-CssClass="gridIco" ItemStyle-CssClass="gridIco" HeaderText="Anular Comprobante">
                                    <ItemTemplate>
                                        <asp:CheckBox runat="server" ID="cbAnulaComprobante" Checked="false" Enabled="false"/>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <HeaderStyle CssClass="gridHeader" />
                            <PagerStyle CssClass="gridPager" />
                            <RowStyle CssClass="gridItem" />
                            <SelectedRowStyle Font-Size="XX-Small" />
                        </asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
                </div>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="HiddenField1" runat="server" />
    <asp:ModalPopupExtender ID="mpeNuevo" runat="server" DropShadow="True" Drag="True" 
        BackgroundCssClass="backgroundColor" PopupControlID="panelActividadReg" 
        TargetControlID="HiddenField1">
    </asp:ModalPopupExtender>
    <asp:Panel ID="panelActividadReg" runat="server" BackColor="White" Style="text-align: right" BorderWidth="1px">
        <div ID="popupcontainer" style="width: 611px">
            <table style="width: 100%;">
                <tr>
                    <td colspan="3" >
                        &nbsp;</td>
                </tr>
                <tr>
                    <td colspan="3" style="text-align: center; font-size: medium;">
                        Esta Seguro de Realizar esta Operacion<br />
                    </td>
                </tr>
                <tr>
                    <td colspan="3" >
                        &nbsp;</td>
                </tr>
                <tr>
                    <td >
                        &nbsp;
                    </td>
                    <td style="text-align: center">
                        <asp:Button ID="btnContinuar" runat="server" CssClass="btn8" 
                            onclick="btnContinuar_Click" Text="Continuar" Width="182px" />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnCancelar" runat="server" CssClass="btn8" 
                            onclick="btnCancelar_Click" Text="Cancelar" Width="182px" />
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="3" >
                        &nbsp;</td>
                </tr>
            </table>
        </div>
    </asp:Panel>
</asp:Content>

