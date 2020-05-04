<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Detalle.aspx.cs" Inherits="Page_Asesores_TrasladarClientes_Detalle" MasterPageFile="~/General/Master/site.master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../../../General/Controles/imprimir.ascx" TagName="imprimir" TagPrefix="ucImprimir" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphMain">
    <meta content="text/html; charset=iso-8859-1" http-equiv="Content-Type">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true">
    </asp:ScriptManager>
    <script language="javascript" type="text/javascript">          
    </script>
    <asp:Panel ID="pConsulta" runat="server" Width="847px" Height="181px">
        <table style="width: 110%;">
            <tr>
                <td colspan="4" style="text-align: center">
                    <asp:ImageButton ID="btnDescargarMetas" runat="server"
                        ImageUrl="~/Images/btnLimpiar.jpg" OnClick="btnRegresar_Click" />
                    <asp:Label ID="lblMensaje" runat="server" ForeColor="Red"
                        Style="font-size: medium; color: #990000"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="4" style="text-align: center">
                    <table style="width: 98%;">
                        <tr>
                            <td style="height: 29px">
                                <asp:Label ID="Label2" runat="server"
                                    Style="text-align: center; color: #FFFFFF; background-color: #359AF2"
                                    Text=" Seleccione una Opción" Width="100%"></asp:Label>
                            </td>
                            <td style="height: 29px"></td>
                        </tr>
                        <tr>
                            <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:RadioButton ID="RadioOficinas" runat="server" AutoPostBack="True"
                                    OnCheckedChanged="RadioOficinas_CheckedChanged" Text="Oficina" />
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:RadioButton ID="RadioAsesores" runat="server" AutoPostBack="True"
                                    OnCheckedChanged="RadioAsesores_CheckedChanged" Text="Asesores" />
                                &nbsp;&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td>&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="text-align: left">
                    <asp:Label ID="lbloficinas" runat="server" Text="Oficina"></asp:Label>
                    <br />
                    <asp:DropDownList ID="ddlOficina" runat="server" CssClass="dropdown"
                        AutoPostBack="True" OnSelectedIndexChanged="ddlOficina_SelectedIndexChanged">
                    </asp:DropDownList>
                    <br />
                    <asp:CompareValidator ID="cvOficina" runat="server"
                        ControlToValidate="ddlOficina" Display="Dynamic"
                        ErrorMessage="Seleccione una oficina" ForeColor="Red"
                        Operator="GreaterThan" SetFocusOnError="true" Type="Integer"
                        ValidationGroup="vgGuardar" ValueToCompare="0"></asp:CompareValidator>
                    <br />
                </td>
                <td style="text-align: left; width: 184px;">
                    <asp:Label ID="lblasesor" runat="server" Text="Asesor"></asp:Label>
                    <br />
                    <asp:DropDownList ID="ddlAsesores" runat="server" CssClass="dropdown"
                        OnSelectedIndexChanged="ddlAsesores_SelectedIndexChanged"
                        AutoPostBack="True">
                        <asp:ListItem Value="0">&lt;Seleccione un Item&gt;</asp:ListItem>
                    </asp:DropDownList>
                    <br />
                    <asp:CompareValidator ID="cvAsesor" runat="server"
                        ControlToValidate="ddlAsesores" Display="Dynamic"
                        ErrorMessage="Seleccione un asesor" ForeColor="Red"
                        Operator="GreaterThan" SetFocusOnError="true" Type="Integer"
                        ValidationGroup="vgGuardar" ValueToCompare="0">
                    </asp:CompareValidator>
                    <br />
                </td>
                <td style="text-align: left">
                    <table style="width: 100%;">
                        <tr>
                            <td style="height: 23px; width: 465px">
                                <asp:Label ID="Label1" runat="server" Text=" Seleccione una Opción de Traslado" Width="100%"
                                    Style="text-align: center; color: #FFFFFF; background-color: #359AF2; margin-right: 63px;"></asp:Label>
                            </td>
                            <td style="height: 23px"></td>
                        </tr>
                        <tr>
                            <td style="width: 465px">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:RadioButton ID="RadioClientes" runat="server" AutoPostBack="True"
                                OnCheckedChanged="RadioClientes_CheckedChanged" Text="Clientes" />
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:RadioButton ID="RadioProducto" runat="server" AutoPostBack="True"
                                OnCheckedChanged="RadioProducto_CheckedChanged" Text="Producto"
                                Checked="True" />
                                &nbsp;&nbsp;</td>
                            <td></td>
                        </tr>
                       <tr>
                           <td>
                             <asp:Label ID="mensaje" runat="server" Visible="false">Tipo Persona</asp:Label>   &nbsp;
                                <asp:DropDownList ID="ddlTipoPersona" runat="server" CssClass="dropdown" Visible="false"
                                     AutoPostBack="true"  OnSelectedIndexChanged="ddlTipoPersona_SelectedIndexChanged">
                                    <asp:ListItem Text="Natural" Value="N"> </asp:ListItem>
                                    <asp:ListItem Text="Juridica" Value="J"></asp:ListItem>
                                   </asp:DropDownList>
                           </td>
                       </tr>
                        <tr>
                            <td style="width: 465px">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;<asp:CheckBox ID="Todos" runat="server" Text="Seleccionar Todos"
                                Visible="False" OnCheckedChanged="Todos_CheckedChanged"
                                AutoPostBack="True" />
                                &nbsp;
                            </td>
                            <td>
                            </td>
                        </tr>
                    </table>
                    <br />
                </td>
                <td style="text-align: left">
                    <br />
                    <br />
                </td>
            </tr>
            <tr>
                <td colspan="4" style="text-align: left">&nbsp;</td>
            </tr>
        </table>
    </asp:Panel>

    <asp:Panel ID="panelProducto" runat="server" BackColor="White"
        Style="text-align: left">

        <asp:UpdatePanel ID="productopanel" runat="server">
            <ContentTemplate>
                <table style="width: 101%;">
                    <tr>
                        <td style="text-align: center" colspan="5">
                            &nbsp;<b><asp:Label ID="lblproductos" runat="server" Text="Productos"></asp:Label></b>
                        </td>
                    </tr>
                    <tr>
                        <td>Identificación <br />
                            <asp:TextBox runat="server" onkeypress="return isNumber(event)" CssClass="textbox" AutoPostBack="true" ID="txtIdentificacion" OnTextChanged="txtIdentificacion_TextChanged"/>
                        </td>
                    </tr>
                </table>
              
                <br />
                <hr />
                <br />
                <br />
                <asp:GridView ID="gvProductosOficina" runat="server" AllowPaging="True"
                    AutoGenerateColumns="False" GridLines="Horizontal"
                    HeaderStyle-CssClass="gridHeader" Width="100%">
                    <Columns>
                        <asp:BoundField DataField="numproducto" HeaderStyle-CssClass="gridColNo"
                            ItemStyle-CssClass="gridColNo">
                            <HeaderStyle CssClass="gridColNo" />
                            <ItemStyle CssClass="gridColNo" HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderStyle-CssClass="gridIco" ItemStyle-CssClass="gridIco">
                            <HeaderStyle CssClass="gridIco" />
                            <ItemStyle CssClass="gridIco" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="numproducto" HeaderText="Número de  Producto">
                            <ItemStyle HorizontalAlign="center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="TipoProducto" HeaderText="Tipo Producto" />
                        <asp:BoundField DataField="nomlinea" HeaderText="Línea de Crédito">
                            <ItemStyle HorizontalAlign="center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="saldo" DataFormatString="{0:N0}" HeaderText="Saldo">
                            <ItemStyle HorizontalAlign="center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="identificacion" HeaderText="Identificacion" />
                        <asp:BoundField DataField="nombrecliente" HeaderText="Cliente">
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkGenerar" runat="server"
                                    Checked="false"  />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <HeaderStyle CssClass="gridHeader" />
                    <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                    <RowStyle CssClass="gridItem" />
                </asp:GridView>
                <br />
                <asp:GridView ID="gvProductos" runat="server" AllowPaging="True" AutoGenerateColumns="False" GridLines="Horizontal"
                    Width="100%" HeaderStyle-CssClass="gridHeader">
                    <Columns>
                        <asp:BoundField DataField="IdPersona" HeaderStyle-CssClass="gridColNo" ItemStyle-CssClass="gridColNo">
                            <HeaderStyle CssClass="gridColNo"></HeaderStyle>
                            <ItemStyle CssClass="gridColNo" HorizontalAlign="Left"></ItemStyle>
                        </asp:BoundField>
                        <asp:TemplateField HeaderStyle-CssClass="gridIco" ItemStyle-CssClass="gridIco">

                            <HeaderStyle CssClass="gridIco"></HeaderStyle>
                            <ItemStyle CssClass="gridIco"></ItemStyle>

                        </asp:TemplateField>
                        <asp:BoundField DataField="TelefonoEmpresa" HeaderText="Número de Radicación">
                            <ItemStyle HorizontalAlign="center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="PrimerNombre" HeaderText="Asesor">
                            <ItemStyle HorizontalAlign="center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="SegundoNombre" HeaderText="Línea de Crédito">
                            <ItemStyle HorizontalAlign="center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="NumeroDocumento" DataFormatString="{0:N0}" HeaderText="Monto Aprobado">
                            <ItemStyle HorizontalAlign="center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Empresa" HeaderText="Cliente">
                            <ItemStyle HorizontalAlign="Right" />

                        </asp:BoundField>
                        <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkGenerar" runat="server" Checked="false" />
                            </ItemTemplate>
                        </asp:TemplateField>

                    </Columns>
                    <HeaderStyle CssClass="gridHeader" />
                    <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                    <RowStyle CssClass="gridItem" />
                </asp:GridView>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>

    <asp:Panel ID="clientespanel" runat="server" BackColor="White"
        Style="text-align: left">
        <asp:UpdatePanel ID="PanelClientes" runat="server">
             <Triggers>
  <asp:AsyncPostBackTrigger ControlID="gvClientesOficinas" EventName="PageIndexChanging" />

  </Triggers>
            <ContentTemplate>
                <table style="width: 101%;">
                    <tr>
                        <td style="text-align: center" colspan="5">
                            <b>
                                <asp:Label ID="lblcliente" runat="server" Text="Clientes"></asp:Label>
                            </b></td>
                    </tr>
                     <tr>
                        <td><br /> Identificación <br />
                            <asp:TextBox runat="server" onkeypress="return isNumber(event)" CssClass="textbox" AutoPostBack="true" ID="txtIdentiClient" OnTextChanged="txtIdentiClient_TextChanged"/>
                        </td>
                    </tr>
                </table>
                 <br />
                 <br />
                 <br />
                <hr />
                <br />
                <asp:GridView ID="gvClientesOficinas" runat="server" AllowPaging="true"
                    AutoGenerateColumns="False" GridLines="Horizontal" Width="100%"
                     OnPageIndexChanging="gvClientesOficinas_PageIndexChanging">
                    <Columns>
                        <asp:BoundField DataField="codpersona" HeaderStyle-CssClass="gridColNo"
                            ItemStyle-CssClass="gridColNo">
                            <HeaderStyle CssClass="gridColNo" />
                            <ItemStyle CssClass="gridColNo" />
                        </asp:BoundField>
                        <asp:BoundField DataField="codpersona" HeaderText="Codigo" />
                        <asp:BoundField DataField="identificacion" HeaderText="Número Identificación" />
                        <asp:BoundField DataField="nombrecliente" HeaderText="Nombre Cliente" />
                        <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkGenerar" runat="server"
                                    Checked="false"  />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <HeaderStyle CssClass="gridHeader" />
                    <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                    <RowStyle CssClass="gridItem" />
                </asp:GridView>
                <br />
                <asp:GridView ID="gvClientes" runat="server" AllowPaging="True"
                    AutoGenerateColumns="False" GridLines="Horizontal" Width="100%">
                    <Columns>
                        <asp:BoundField DataField="codpersona" HeaderStyle-CssClass="gridColNo"
                            ItemStyle-CssClass="gridColNo">
                            <HeaderStyle CssClass="gridColNo" />
                            <ItemStyle CssClass="gridColNo" />
                        </asp:BoundField>
                        <asp:BoundField DataField="SegundoNombre" HeaderText="Número Identificación" />
                        <asp:BoundField DataField="PrimerNombre" HeaderText="Nombre Cliente" />
                        <asp:BoundField DataField="RazonSocial" HeaderText="Nombre Asesor" />
                        <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkGenerar0" runat="server"
                                    Checked="false" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <HeaderStyle CssClass="gridHeader" />
                    <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                    <RowStyle CssClass="gridItem" />
                </asp:GridView>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
    <hr />
    <asp:Panel ID="trasladopanel" runat="server" BackColor="White"
        Style="text-align: left" Width="888px">
        <%--<asp:UpdatePanel ID="paneltraslado" runat="server">--%>
        <contenttemplate>
            <table  style="width:105%;">
               <tr>
                    <td style="text-align: left; width: 432px;">Oficina
                        <asp:DropDownList ID="ddlNuevaOficina" runat="server" CssClass="dropdown" 
                            AutoPostBack="True" 
                            onselectedindexchanged="ddlOficina_SelectedIndexChanged1">
                        </asp:DropDownList>
                        <br />                  
                    </td>
                    <td>
                        <br />
                        <asp:Label ID="lblasesornuevo" runat="server" Text="Nuevo Asesor"></asp:Label>
                        <br />
                        <asp:DropDownList ID="NuevoAsesor" runat="server" CssClass="dropdown" 
                            AutoPostBack="True" >
                             <asp:ListItem Value="0">&lt;Seleccione un Item&gt;</asp:ListItem>
                        </asp:DropDownList>
                        <br />
                        <br />
                    </td>
               </tr>
               <tr>
                    <td style="text-align: left; width: 432px;">
                        Observaciones
                        <asp:TextBox ID="observaciontxt" runat="server" Width="416px"></asp:TextBox>
                        <br />
                        <br />
                    </td>
                    <td style="text-align: left; width: 476px;">
                        Motivo<br />
                        <asp:DropDownList ID="DropDownList2" runat="server" CssClass="dropdown">
                            <asp:ListItem Value="0">&lt;Seleccione un Item&gt;</asp:ListItem>
                        </asp:DropDownList>
                        <br />
                        <br />
                    </td>
               <tr>
                    <td style="text-align: left; width: 476px;">
                        <br />
                        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/btnGuardar.jpg" onclick="ImageButton1_Click" />
                        <br />             
                        <br />
                        <br />
                        <br />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left; width: 476px;">
                        
                    </td>
                </tr>
            </table>   
        </contenttemplate>
        <%--</asp:UpdatePanel>--%>
    </asp:Panel>
</asp:Content>
