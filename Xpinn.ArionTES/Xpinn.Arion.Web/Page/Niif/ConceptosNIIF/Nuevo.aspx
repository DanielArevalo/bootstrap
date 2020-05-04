<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="ctlmensaje" TagPrefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:Panel ID="pConsulta" runat="server">
        <table border="0" cellpadding="0" cellspacing="0" width="100%" style="align-items: center">
            <tr>
                <td>
                    <asp:Label ID="lblMensaje" runat="server" Visible="False" ForeColor="Blue" Font-Bold="true"></asp:Label>
                </td>
            </tr>
            <caption>
                <br />
                <tr>
                    <td style="text-align: center">Tipo Estado Financiero<asp:DropDownList ID="ddlTipoEstadoFinanciero" runat="server"  
                        AutoPostBack="true" CssClass="dropdown" OnSelectedIndexChanged="ddlTipoEstadoFinanciero_SelectedIndexChanged">
                        </asp:DropDownList>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Label ID="lblconcepto" Visible="true"  runat="server" Text="Cod Concepto"></asp:Label>
                        <asp:TextBox ID="txtcodigo"  Visible="true" Enabled="false" runat="server" CssClass="textbox" Width="10%" />
                    </td>
                </tr>
            </caption>
            </tr>
            <tr>
                <td style="color: #FFFFFF; background-color: #0066FF">Datos Conceptos NIIF</td>
            </tr>
        </table>
    </asp:Panel>
    <table border="0" cellpadding="0" cellspacing="0" width="100%" runat="server" style="align-items: center">
        <tr>
                    <td style="width: 272px; text-align: left">Descripción<asp:TextBox ID="txtDescripcion" runat="server" CssClass="textbox"
                            Width="400px" />
                    </td>
                    <td style="text-align: left; width: 301px;" class="logo" colspan="2">
                        &nbsp;</td>
                      
                    <td style="text-align: left">
                        &nbsp;Depende de
                        <br />
                        <asp:DropDownList ID="ddlDepende_De" runat="server" CssClass="dropdown"  AutoPostBack="true" OnSelectedIndexChanged="ddlDepende_De_SelectedIndexChanged">
                                <asp:ListItem Value="0">Seleccione un Item</asp:ListItem>
                            
                        </asp:DropDownList>
                        </td>
                      
                </tr>
        <tr>
            <td style="text-align: left; " colspan="2">

                            Filtro Nivel
                                   <asp:DropDownList ID="ddlNivel" Height="30px" runat="server" CssClass="dropdown" 
                                Width="113px" AutoPostBack="True" OnSelectedIndexChanged="ddlNivel_SelectedIndexChanged">
                               
                                <asp:ListItem>1</asp:ListItem>
                                <asp:ListItem>2</asp:ListItem>
                                <asp:ListItem>3</asp:ListItem>
                                <asp:ListItem>4</asp:ListItem>
                                <asp:ListItem>5</asp:ListItem>
                                <asp:ListItem>6</asp:ListItem>
                                <asp:ListItem>7</asp:ListItem>
                                <asp:ListItem>8</asp:ListItem>
                                <asp:ListItem>9</asp:ListItem>
                            
                            </asp:DropDownList>
            </td>
            <td style="text-align: left; " colspan="2">

                                    <asp:CheckBox ID="cbTitulo" runat="server" Text="Solo Título" AutoPostBack="True"
                                    />
            </td>
        </tr>
        <tr>
            <td style="text-align: center; " colspan="4">

                <asp:UpdatePanel ID="upRecoger" runat="server">
                    <ContentTemplate>
                        <div style="text-align: left">
                            &nbsp;Cuentas&nbsp;
                            <br />
                            <strong>
                           <asp:GridView ID="gvCuentasNIIF" runat="server" AutoGenerateColumns="False" DataKeyNames="codigo" 
                               HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager" RowStyle-CssClass="gridItem" Width="100%" style="font-size: xx-small">
                                <Columns>
                                   
                                    <asp:TemplateField HeaderText="Código">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_consecutivo" runat="server" Text='<%# Bind("cod_cuenta_niif") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" Width="50px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Descripción">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_descripcion" runat="server" Text='<%# Bind("desc_cuenta_niif") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Corriente">
                                        <ItemTemplate>
                                           
                                           
                                                 <asp:CheckBox ID="cbCorriente" runat="server" Checked='<%#Convert.ToBoolean(Eval("corriente")) %>' AutoPostBack="true" OnCheckedChanged="cbCorriente_CheckedChanged" />      

                                        </ItemTemplate>
                                        <HeaderStyle CssClass="gridIco" />
                                    </asp:TemplateField>

                                     <asp:TemplateField HeaderText="No Corriente">
                                        <ItemTemplate>
                                             
                                               <asp:CheckBox ID="cbNoCorriente" runat="server" Checked='<%#Convert.ToBoolean(Eval("nocorriente")) %>' AutoPostBack="true" OnCheckedChanged="cbNoCorriente_CheckedChanged" />      


                                             </ItemTemplate>
                                        <HeaderStyle CssClass="gridIco" />
                                    </asp:TemplateField>

                                      <asp:TemplateField HeaderText="Seleccionar">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="cbSeleccionar" runat="server" AutoPostBack="true" Checked='<%#Convert.ToBoolean(Eval("codigo")) %>' OnCheckedChanged="cbSeleccionar_CheckedChanged" />      

                                        </ItemTemplate>

                                          
                                        <HeaderStyle CssClass="gridIco" />
                                    </asp:TemplateField>
                                    
                                     <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                              <asp:Label ID="lblcodigo"  Visible="false" runat="server" Text='<%# Bind("codigo") %>'></asp:Label>

                                        </ItemTemplate>

                                          
                                        <HeaderStyle CssClass="gridIco" />
                                    </asp:TemplateField>




                                </Columns>
                                <HeaderStyle CssClass="gridHeader" />
                                <PagerStyle CssClass="gridPager" />
                                <RowStyle CssClass="gridItem" />
                            </asp:GridView>
                            </strong>
                            <br />
                        </div>
                        <asp:HiddenField ID="HiddenField1" runat="server" Visible="false" />
                        <asp:Panel ID="panelLista" runat="server" BackColor="#CCCCCC" BorderStyle="Solid" BorderWidth="2px" Direction="LeftToRight" Height="200px" ScrollBars="Auto" Style="display: none" Width="300px">
                           
                        </asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
    <uc2:ctlmensaje ID="ctlmensaje" runat="server" />
</asp:Content>

