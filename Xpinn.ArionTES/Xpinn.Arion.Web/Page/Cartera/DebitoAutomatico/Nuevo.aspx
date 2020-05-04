<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" Title=".: Xpinn - Cartera :." %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register src="../../../General/Controles/decimales.ascx" tagname="decimales" tagprefix="uc2" %>
<%@ Register Src="../../../General/Controles/fechaeditable.ascx" TagName="fecha" TagPrefix="uc1" %>
<%@ Register src="../../../General/Controles/ctlLineaAhorro.ascx" tagname="ddlLineaAhorro" tagprefix="ctl" %>
<%@ Register src="../../../General/Controles/mensajeGrabar.ascx" tagname="mensajeGrabar" tagprefix="uc4" %>
<%@ Register src="../../../General/Controles/mensajeGrabar.ascx" tagname="mensajeGrabar" tagprefix="uc5" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    
<asp:MultiView ID="mvPrincipal" runat="server" ActiveViewIndex=0>
<asp:View ID="vwData" runat="server">

    <table border="0" cellpadding="5" cellspacing="0" width="100%" >
        <tr>
            <td class="tdI" colspan="4">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="tdI" colspan="4">
                <asp:Panel ID="Panel2" runat="server">
                    <table style="width:60%;">
                        <tr>
                            <td class="logo" colspan="3" style="text-align:left">
                                <strong>DATOS DEL DEUDOR</strong>
                            </td>
                        </tr>
                        <tr>
                            <td class="logo" style="width: 150px; text-align:left">
                                Cod.Persona
                            </td>
                            <td class="logo" style="width: 150px; text-align:left">
                                Identificación
                            </td>
                            <td style="text-align:left">
                                Tipo Identificación
                            </td>
                            <td style="text-align:left">
                                Nombre
                            </td>
                        </tr>
                        <tr>
                            <td class="logo" style="width: 150px; text-align:left">
                                <asp:TextBox ID="txtCodPersona" runat="server" CssClass="textbox" 
                                    Enabled="false" />
                            </td>
                            <td class="logo" style="width: 150px; text-align:left">
                                <asp:TextBox ID="txtIdentificacion" runat="server" CssClass="textbox" 
                                    Enabled="false" />
                            </td>
                            <td style="text-align:left">
                                <asp:TextBox ID="txtTipo_identificacion" runat="server" CssClass="textbox" Enabled="false" />
                            </td>
                            <td style="text-align:left">
                                <asp:TextBox ID="txtNombre" runat="server" CssClass="textbox" Enabled="false" Width="377px" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td class="tdI" colspan="4">
                Cuentas de Ahorros  Cliente 
                <br />
                            <asp:DropDownList ID="ddlCuentaAhorros" runat="server" ClientIDMode="Static" 
                                CssClass="textbox" Width="310px">
                            </asp:DropDownList>
                        </td>
        </tr>
        <tr>
            <td class="tdI" colspan="4" style="height: 9px; text-align:left">
                <strong>DATOS DE PRODUCTOS DEL CLIENTE </strong>
            </td>
        </tr>
        <tr>
            <td class="tdI" colspan="4" style="height: 80px">
                <div id="divValores" runat="server" style="overflow: scroll; height: 300px">
                    <asp:GridView ID="gvLista"   runat="server"   width="100%"  GridLines="Horizontal" AutoGenerateColumns="False"
                        HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager"
                        RowStyle-CssClass="gridItem"   OnRowDeleting="gvLista_RowDeleting"  DataKeyNames="consecutivo" OnRowDataBound="gvLista_RowDataBound" >
                        <Columns>       
                                  <asp:TemplateField HeaderStyle-CssClass="gridIco" ItemStyle-CssClass="gridIco">
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="cbSeleccionarEncabezado" runat="server" Checked="true" OnCheckedChanged="cbSeleccionarEncabezado_CheckedChanged"
                                                AutoPostBack="True" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="cbSeleccionar" runat="server" />
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="gridIco"></HeaderStyle>
                                        <ItemStyle CssClass="gridIco"></ItemStyle>
                                    </asp:TemplateField> 

                             <asp:TemplateField HeaderStyle-CssClass="gridIco"  ItemStyle-CssClass="gridIco">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnBorrar" runat="server" CommandName="Delete" ImageUrl="~/Images/gr_elim.jpg" ToolTip="Borrar" />
                            </ItemTemplate>
                            <ItemStyle CssClass="gI" />
                        </asp:TemplateField>
                            
                             <asp:BoundField DataField="consecutivo" HeaderText="#" visible="true" />        
                             <asp:BoundField DataField="cod_persona" HeaderText="Código Persona" />        
                           <asp:BoundField DataField="tipoproducto" HeaderText="Tipo Producto"  />
                            <asp:BoundField DataField="cod_tipo_producto" HeaderText="Cod.tipo_producto" />
                            <asp:BoundField DataField="cod_linea" HeaderText="Código Línea"  />
                             <asp:BoundField DataField="nombre_linea" HeaderText="Línea"  />
                             <asp:BoundField DataField="numero_producto" HeaderText="Número Producto" />
                             <asp:BoundField DataField="cuota" HeaderText="Valor Cuota"  />
                          
                               <asp:BoundField DataField="descripcion" HeaderText="Cuenta Ahorros Debitar" />
                          

                        </Columns>
                        <HeaderStyle CssClass="gridHeader"></HeaderStyle>
                        <PagerStyle CssClass="gridPager"></PagerStyle>
                        <RowStyle CssClass="gridItem"></RowStyle>
                    </asp:GridView>
                    <br />
                </div>
                <asp:Label ID="lblTotalRegs" runat="server" Visible="False" />          
                &nbsp;&nbsp;&nbsp;&nbsp;
            </td>
        </tr>
        <tr>
            <td class="tdI" colspan="4">

    <uc4:mensajeGrabar ID="ctlMensaje" runat="server"/>


                <br />
                <uc5:mensajeGrabar ID="ctlMensajeBorrar" runat="server" />


            </td>
        </tr>        
        <tr>
            <td class="tdI" style="height: 35px">

    </asp:View>
    <asp:View ID="vwFinal" runat="server">
        <table style="width: 100%;">
            <tr>
                <td style="text-align: center; font-size: large;">
                    <br />
                    <br />
                    <br />
                    <br />
                </td>
            </tr>
            <tr>
                <td style="text-align: center; font-size: large; color:Red;">
                    Se Grabaron los Datos Correctamente
                </td>
            </tr>
            <tr>
                <td style="text-align: center; font-size: large;">
                    <br />
                    <br />
                </td>
            </tr>
            <tr>
                <td style="text-align: center; font-size: large;">
                 <br />
                </td>
            </tr>
        </table>


                </td>
            <td class="tdI" style="height: 35px; width: 33px;">
                </td>
            <td class="tdI" style="height: 35px">
                </td>
            <td class="tdI" style="height: 35px">
                </td>
        </tr>
    </table>
  </asp:View>
</asp:MultiView>
</asp:Content>