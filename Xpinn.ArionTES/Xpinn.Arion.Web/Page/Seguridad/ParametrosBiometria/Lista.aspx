<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"   CodeFile="Lista.aspx.cs" Inherits="Nuevo" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../../../General/Controles/ctlMensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="ctl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager> 
    <asp:MultiView ID="mvPrincipal" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwDatos" runat="server">   
            <table style="width: 100%; margin-right: 22px;">
                <tr>
                    <td style="text-align:left; width: 118px;">                         
                        <asp:Label ID="Labelerror" runat="server" 
                            style="color: #FF0000; font-weight: 700; font-size: x-small;" colspan="5" 
                            Text=""></asp:Label>            
                    </td>
                </tr>  
                <tr>
                    <td style="text-align: left" colspan="4">                
                        <strong>PARAMEROS BIOMETRIA<br /></strong>
                    </td>
                </tr>                   
                <tr>
                    <td colspan="3">
                        <asp:GridView ID="gvLista" runat="server" Width="88%" 
                            AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" 
                            onrowdatabound="gvLista_RowDataBound" style="margin-right: 43px" >
                            <Columns>                    
                                <asp:BoundField DataField="cod_opcion" HeaderText="Cod.Opción">
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="30px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="nombre" HeaderText="Descripción" >
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="150px" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Validar Biometria">
                                    <ItemTemplate>                    
                                        <asp:checkbox ID="chbconsulta" runat="server"  Checked='<%# Convert.ToBoolean(Eval("validar_Biometria")) %>'  Width="40px" />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Maneja Excepciones">     
                                   <ItemTemplate>             
                                        <asp:checkbox ID="chbcreacion" runat="server" Checked='<%# Convert.ToBoolean(Eval("maneja_excepciones")) %>' Width="40px" />                         
                                   </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center"  Width="120px" />                            
                                </asp:TemplateField>
                                 <asp:BoundField DataField="cod_proceso" HeaderText="Cod.Proceso" Visible="false">
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="30px" />
                                </asp:BoundField>
                                 <asp:BoundField DataField="refayuda" HeaderText="refayuda" Visible="false">
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="30px" />
                                </asp:BoundField>
                                 <asp:BoundField DataField="generalog" HeaderText="generalog" Visible="false">
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="30px" />
                                </asp:BoundField>
                                 <asp:BoundField DataField="ruta" HeaderText="ruta" Visible="false">
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="30px"  />
                                </asp:BoundField>
                                 <asp:BoundField DataField="accion" HeaderText="accion" Visible="false">
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="30px" />
                                </asp:BoundField>
                            </Columns>
                            <HeaderStyle CssClass="gridHeader" />
                            <PagerStyle CssClass="gridHeader" Font-Bold="False" />   
                            <RowStyle CssClass="gridItem" />     
                        </asp:GridView>
                    </td>
                </tr>     
            </table> 
       </asp:View>
       <asp:View ID="mvFinal" runat="server">
            <asp:Panel id="PanelFinal" runat="server">
                <table style="width: 100%;">
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <br /><br /><br /><br />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <asp:Label ID="lblMensaje" runat="server" 
                                Text="Datos Modificados Correctamente" style="color: #FF3300"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <br /><br />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </asp:View>

    </asp:MultiView>

</asp:Content>
