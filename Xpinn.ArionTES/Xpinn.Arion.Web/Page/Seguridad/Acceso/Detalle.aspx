<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"   CodeFile="Detalle.aspx.cs" Inherits="Detalle" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../../../General/Controles/ctlMensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="ctl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager> 
    <asp:Panel ID="pConsulta" runat="server">
    <table style="width: 100%">
        <tr>
            <td style="text-align:left; width: 118px;">                         
                <asp:Label ID="Labelerror" runat="server" 
                    style="color: #FF0000; font-weight: 700; font-size: x-small;" colspan="5" 
                    Text=""></asp:Label>
            </td>
            <td style="text-align:right; width: 7px;">
                &nbsp;</td>
            <td style="text-align:right">                
            </td>
        </tr>
        <tr>
            <td style="text-align: left">                
                <span style="font-weight: bold;">Datos del Perfil</span>
            </td>
            <td style="width: 7px">
                &nbsp;
            </td>
            <td style="text-align: left">
               
            </td>
        </tr>    
        <tr>
            <td style="text-align: left">                
                Código
                <br />
                <asp:TextBox ID="txtCodigo" runat="server" CssClass="textbox" title="Código del Perfil" ></asp:TextBox>
            </td>
            <td style="width: 7px">
                &nbsp;
            </td>
            <td style="text-align: left">
                Nombre                
                <asp:RequiredFieldValidator ID="rfvDescripcion" runat="server" ErrorMessage="Campo Requerido"
                    ControlToValidate="txtDescripcion" Display="Dynamic" ForeColor="Red" 
                    ValidationGroup="vgGuardar" style="font-size: xx-small" />
                <br />
                <asp:TextBox ID="txtDescripcion" runat="server" 
                    CssClass="textbox" title="Descripción o nombre del Perfil"
                    Width="350px" style="text-transform:uppercase" ></asp:TextBox>                
            </td>
        </tr>        
        <tr>
            <td style="width: 118px; text-align: left" colspan="3">
                <span style="font-weight: bold;">Permisos Asignados</span>
            </td>
        </tr>                    
        <tr>
            <td style="width: 118px; text-align: left">
                Modulo
            </td>
            <td style="width: 118px; text-align: left" colspan="2">
                <asp:DropDownList ID="ddlModulo" runat="server" CssClass="textbox" Width="200px" AutoPostBack="True" 
                    onselectedindexchanged="ddlModulo_SelectedIndexChanged" />
            </td>
        </tr>   
        <tr>
            <td colspan="3">
                <asp:GridView ID="gvLista" runat="server" Width="80%" 
                    AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" 
                    onrowdatabound="gvLista_RowDataBound" >
                    <Columns>                    
                        <asp:BoundField DataField="cod_opcion" HeaderText="No.">
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="30px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="nombreopcion" HeaderText="Descripción" >
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="150px" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Consultar">
                            <ItemTemplate>                    
                                <asp:checkbox ID="chbconsulta" runat="server"  Checked='<%# Convert.ToBoolean(Eval("consultar")) %>' Width="40px" />
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" Width="50px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Crear">
                            <ItemTemplate>                    
                                <asp:checkbox ID="chbcreacion" runat="server" Checked='<%# Convert.ToBoolean(Eval("insertar")) %>' Width="40px" />
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" Width="50px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Modificar">
                            <ItemTemplate>                    
                                <asp:checkbox ID="chbmodificacion" runat="server" Checked='<%# Convert.ToBoolean(Eval("modificar")) %>' Width="40px" />
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" Width="50px" />
                        </asp:TemplateField> 
                        <asp:TemplateField HeaderText="Borrar">
                            <ItemTemplate>                    
                                <asp:checkbox ID="chbborrado" runat="server" Checked='<%# Convert.ToBoolean(Eval("borrar")) %>' Width="40px" />
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" Width="50px" />
                        </asp:TemplateField> 
                    </Columns>
                    <HeaderStyle CssClass="gridHeader" />
                    <PagerStyle CssClass="gridHeader" Font-Bold="False" />   
                    <RowStyle CssClass="gridItem" />     
                </asp:GridView>
            </td>
        </tr>        
    </table>
    </asp:Panel>
</asp:Content>
