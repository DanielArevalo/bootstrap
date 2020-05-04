<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" %>  

<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>
<%@ Register Src="~/General/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>    
    <br /><br />
    <asp:MultiView ID="mvAplicar" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwLista" runat="server">            
            <asp:UpdatePanel ID="panelUPD" runat="server">
                <ContentTemplate>
                    <table style="text-align: center" cellspacing="0" cellpadding="0">
                        <tr>
                            <td style="text-align: left; width: 150px">
                                Código<br />
                                <asp:TextBox ID="txtCodigo" runat="server" CssClass="textbox" Width="100px"></asp:TextBox>
                            </td>
                            <td style="text-align: left; width: 330px" colspan="2">
                                Nombre<br />
                                <asp:TextBox ID="txtNombre" runat="server" CssClass="textbox" Width="280px"></asp:TextBox>
                            </td>
                            <td style="text-align: left;" colspan="3">
                                Estado<br />
                                <asp:CheckBox ID="chkEstado" runat="server" Text="Activo" />
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left; width: 150px">
                                Monto Mínimo<br />
                                <uc1:decimales ID="txtMntoMinimo" runat="server" CssClass="textbox" />
                            </td>
                            <td style="text-align: left; width: 150px">
                                Monto Máximo<br />
                                <uc1:decimales ID="txtMtoMaximo" runat="server" CssClass="textbox" />
                            </td>
                            <td style="text-align: left; width: 180px">
                                Periodicidad<br />
                                <asp:DropDownList ID="ddlPeriodicidad" runat="server" CssClass="textbox" Width="90%"
                                    AppendDataBoundItems="True" />
                            </td>
                            <td style="text-align: left;" colspan="2">
                                Tipo de Persona<br />
                                <asp:DropDownList ID="ddlTipoPersona" runat="server" CssClass="textbox" Width="200px"
                                    AppendDataBoundItems="True" />
                            </td>
                            <td style="text-align: left; width: 250px">
                                <br />
                                <asp:CheckBox ID="chkRetirados" runat="server" Text="Permite Asociados Retirados" />
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left; width: 150px">
                                Num. Auxilios<br />
                                <asp:TextBox ID="txtNumAuxilios" runat="server" CssClass="textbox" Style="text-align: right"
                                    Width="70%" />
                                <asp:FilteredTextBoxExtender ID="fte1" runat="server" TargetControlID="txtNumAuxilios"
                                    FilterType="Custom, Numbers" ValidChars="" />
                            </td>
                            <td style="text-align: left; width: 150px">
                                Dias para Desembolso<br />
                                <asp:TextBox ID="txtDiasDesembolso" runat="server" CssClass="textbox" Style="text-align: right"
                                    Width="70%" />
                                <asp:FilteredTextBoxExtender ID="fte2" runat="server" TargetControlID="txtDiasDesembolso"
                                    FilterType="Custom, Numbers" ValidChars="" />
                            </td>
                            <td style="text-align: left; width: 180px">
                                <br />
                                <asp:CheckBox ID="chkRetencion" runat="server" Text="Cobra Retención" />
                            </td>
                            <td style="text-align: left;">
                                <br />
                                <asp:CheckBox ID="chkEducativo" runat="server" Text="Educativo" AutoPostBack="true"
                                    OnCheckedChanged="chkEducativo_CheckedChanged" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            </td>
                            <td style="text-align: left;">
                                <br />
                                <asp:CheckBox ID="cbOrdenSErvicio" runat="server" Text="Orden de Servicio" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            </td>
                            <td style="text-align: left; width: 180px">
                                <br />
                                <asp:CheckBox ID="chkMora" runat="server" Text="Permite Mora" />
                            </td>
                            <td style="text-align: left; width: 150px">
                                <asp:Label ID="lblPorcentMatr" runat="server" Text="% Auxilio a Otorgar" /><br />
                                <asp:TextBox ID="txtPorcentajeMatri" runat="server" CssClass="textbox" style="text-align:right" />
                                <asp:FilteredTextBoxExtender ID="fte4" runat="server" TargetControlID="txtPorcentajeMatri"
                                    FilterType="Custom, Numbers" ValidChars=",." />
                            </td>                            
                        </tr>
                    </table>
                    <hr style="width: 100%" />
                </ContentTemplate>
                <Triggers>
                  <asp:AsyncPostBackTrigger ControlID="chkEducativo" EventName="CheckedChanged" />
                </Triggers>
            </asp:UpdatePanel>
            <asp:Panel ID="panelGrilla" runat="server">
                <table>                  
                    <tr>
                        <td style="text-align:left">
                        <strong>Requisitos</strong><br />    
                         <asp:Button ID="btnAdicionarFila" runat="server" CssClass="btn8" OnClick="btnAdicionarFila_Click"
                                        OnClientClick="btnAdicionarFila_Click" Text="+ Adicionar Detalle" Height="22px" />
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                <ContentTemplate>                                   
                                    <asp:GridView ID="gvRequisitos" runat="server" PageSize="20" ShowHeaderWhenEmpty="True"
                                        AutoGenerateColumns="False" SelectedRowStyle-Font-Size="XX-Small" Style="font-size: small;
                                        margin-bottom: 0px;" OnRowDataBound="gvPlanes_RowDataBound" OnRowDeleting="gvPlanes_RowDeleting"
                                        DataKeyNames="CODREQUISITOAUX" GridLines="Horizontal">
                                        <Columns>
                                            <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/gr_elim.jpg" ShowDeleteButton="True">
                                                <ItemStyle HorizontalAlign="left" />
                                            </asp:CommandField>
                                            <asp:TemplateField HeaderText="Código" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                <ItemTemplate>
                                                    <cc1:TextBoxGrid ID="txtCodigo" runat="server" Text='<%# Bind("CODREQUISITOAUX") %>'
                                                        Width="80px" CssClass="textbox"></cc1:TextBoxGrid>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Descripción" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <cc1:TextBoxGrid ID="txtDescripcion" runat="server" Text='<%# Bind("DESCRIPCION") %>'
                                                        Width="250px" CssClass="textbox"></cc1:TextBoxGrid>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="center" />
                                            </asp:TemplateField>                                            
                                            <asp:TemplateField HeaderText="Requerido" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <cc1:CheckBoxGrid ID="chkRequerido" runat="server" Checked='<%#Convert.ToBoolean(Eval("REQUERIDO"))%>'/>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="center" />
                                            </asp:TemplateField>                                                                         
                                        </Columns>
                                        <HeaderStyle CssClass="gridHeader" />
                                        <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                                        <RowStyle CssClass="gridItem" />
                                        <SelectedRowStyle Font-Size="XX-Small"></SelectedRowStyle>
                                    </asp:GridView>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </asp:Panel>

            <table style="width: 740px">
                <tr>
                    <td style="width: 740px; text-align: center">
                        <asp:Label ID="lblTotalReg" runat="server" Visible="False" />
                    </td>
                </tr>               
            </table>             

        </asp:View>

        <asp:View ID="vwFinal" runat="server">
                <asp:Panel id="PanelFinal" runat="server">
                <table style="width: 100%;">
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <br /><br /><br /><br />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large; color:Red">
                            Linea de Auxilio
                            <asp:Label ID="lblmsj" runat="server"></asp:Label>
                            &nbsp;correctamente.<br /> Nro. de Servicio :
                            <asp:Label ID="lblNroMsj" runat="server"></asp:Label>
                            </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <br /><br />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            &nbsp;</td>
                    </tr>
                </table>
            </asp:Panel>
        </asp:View>
    </asp:MultiView>

    <asp:HiddenField ID="HiddenField1" runat="server" />    
  
     <uc4:mensajeGrabar ID="ctlMensaje" runat="server"/>
     
</asp:Content>