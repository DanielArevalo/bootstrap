<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" Title=".: Xpinn - Gestion Documental :." %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
   
    <asp:Panel ID="pConsulta" runat="server">
                    <div style="border-style: none; border-width: medium;">
                        <table style="width: 96%; height: 179px;" border="0">
                            <tbody style="text-align: center">
                                <tr>
                                    
                                    <td>Código<br />
                                        <asp:TextBox ID="txtCodigo" runat="server" CssClass="textbox" Enabled="false"
                                            Style="text-align: center"></asp:TextBox>
                                    </td>
                                    <td>Tipo Identificación<br />
                                        <asp:TextBox ID="txtTipoDoc" runat="server" CssClass="textbox" Enabled="false"
                                            Style="text-align: center" Width="130px"></asp:TextBox>
                                    </td>
                                    <td style="text-align: center">Identificación<br />
                                        <asp:TextBox ID="txtNumDoc" runat="server" CssClass="textbox" Enabled="false"
                                            Style="text-align: center"></asp:TextBox>
                                    </td>
                                    <td class="style3">Tipo Cliente<br />
                                        <asp:TextBox ID="txtTipoCliente" runat="server" CssClass="textbox"
                                            Style="text-align: center" Enabled="false" Width="163px"></asp:TextBox>
                                    </td>
                                    
                                </tr>
                                <tr>
                                    <td style="width: 153px; margin-left: 40px;">Nombres<br />
                                        <asp:TextBox ID="txtPrimerNombre" runat="server" CssClass="textbox"
                                            Enabled="false"></asp:TextBox>
                                    </td>
                                    <td>Apellidos<br />
                                        <asp:TextBox ID="txtPrimerApellido" runat="server" CssClass="textbox"
                                            Enabled="false"></asp:TextBox>
                                        <br />
                                    </td>
                                    <td style="text-align: left">
                                        
                                                Estado<br />
                                                    <asp:TextBox ID="txtestado" runat="server" CssClass="textbox"
                                                        Enabled="false" Width="80px"></asp:TextBox>
                                              
                                                
                                    
                                    </td>                                   
                                    <td class="style3">Dirección<br />
                                        <asp:TextBox ID="txtDireccion" runat="server" CssClass="textbox" Enabled="false"
                                            Width="163px"></asp:TextBox>
                                        <br />
                                    </td>
                                </tr>                           
                             
                             
                            </tbody>
                        </table>
                    </div>
                    <div style="border-style: none; border-width: medium;">

                        <table border="0" cellpadding="5" cellspacing="0" width="100%">
                            <tr>
                                <td class="tdI" style="text-align: left">


                                    <div style="font-family: Arial">
                                        <asp:TreeView runat="server" ID="TreeView1">
                                        </asp:TreeView>
                                    </div>
                                </td>

                                <td>
                                    <div style="border-color: black">
                                        <iframe style="border-color: black"></iframe>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>

                </asp:Panel>
    



</asp:Content>