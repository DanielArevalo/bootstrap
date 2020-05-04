<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Nuevo.aspx.cs" Inherits="Lista" %>

<%@ Register Src="~/General/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc1" %>
<%@ Register src="~/General/Controles/mensajeGrabar.ascx" tagname="mensajeGrabar" tagprefix="uc1" %>
<%@ Register Src="~/General/Controles/ctlBusquedaRapida.ascx" TagName="ListadoPersonas" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <script type="text/javascript" src="../../../Scripts/gridviewScroll.min.js"></script>   
    
      <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
          <asp:View ID="View1" runat="server">
              <asp:Panel ID="pConsulta" runat="server" Style="margin-bottom: 0px">
                  <asp:ScriptManager ID="ScriptManager1" runat="server">
                  </asp:ScriptManager>
                  <table cellpadding="4" cellspacing="0" style="width: 100%">
                      <tr>
                          <td style="text-align: left" colspan="4">
                              <strong>Datos Principales: </strong>
                              <asp:Label ID="lblConsecutivo" runat="server" Text=""></asp:Label>
                          </td>
                      </tr>
                      <tr>
                          <td style="text-align: left; width: 25%;">
                              Código<br />
                              <asp:TextBox ID="txtCodigo" runat="server" CssClass="textbox" Width="90%"></asp:TextBox>
                          </td>
                          <td colspan="2" style="width: 45%; text-align: left">
                              Nombre<br />
                              <asp:TextBox ID="txtnombre" runat="server" CssClass="textbox" Width="90%"></asp:TextBox>
                          </td>
                          <td style="text-align: left; width: 30%">
                              Encargado<br />
                               <asp:TextBox ID="txtCodEncargado" runat="server" CssClass="textbox" Width="50px" ReadOnly="True" Visible="false" />
                            <asp:TextBox ID="txtIdenEncargado" runat="server" CssClass="textbox" Width="50px" ReadOnly="True" Visible="false" />
                            <asp:TextBox ID="txtNomEncargado" runat="server" CssClass="textbox" Width="260px" ReadOnly="True" />
                              <asp:Button ID="btnConsultaPersonas" runat="server" CssClass="btn8" 
                                  Height="26px" onclick="btnConsultaPersonas_Click" Text="..." />
                                  <uc1:ListadoPersonas id="ctlBusquedaPersonas" runat="server" />
                            <asp:RequiredFieldValidator ID="rfvEncargado" runat="server" ErrorMessage="Seleccione encargado" Display="Dynamic" ControlToValidate="txtNomEncargado" ValidationGroup="vgGuardar" InitialValue="0" />
                          </td>
                      </tr>
                      <table cellpadding="4" style="width: 100%">
                          <tr>
                              <td colspan="2" style="text-align: left; width: 40%;">
                                  Ubicación<br />
                                  <asp:DropDownList ID="ddlUbicacion" runat="server" AppendDataBoundItems="True" CssClass="textbox"
                                      Width="90%">
                                      <asp:ListItem Value="0">Seleccion un Item</asp:ListItem>
                                  </asp:DropDownList>
                              </td>
                              <td style="text-align: left; width: 30%;">
                                  Centro de Costo<br />
                                  <asp:DropDownList ID="ddlCentroCosto" runat="server" AppendDataBoundItems="True"
                                      CssClass="textbox" Width="90%">
                                      <asp:ListItem Value="0">Seleccion un Item</asp:ListItem>
                                  </asp:DropDownList>
                                  <asp:RequiredFieldValidator ID="rfvCentroCosto" runat="server" ControlToValidate="ddlCentroCosto"
                                      Display="Dynamic" ErrorMessage="Seleccione centro" ForeColor="Red" InitialValue="0"
                                      Style="font-size: xx-small" ValidationGroup="vgGuardar" />
                              </td>
                              <td style="text-align: left; width: 30%;">
                                  Oficina
                                  <br />
                                  <asp:DropDownList ID="ddlOficina" runat="server" AppendDataBoundItems="True" CssClass="textbox"
                                      Width="90%">
                                      <asp:ListItem Value="0">Seleccion un Item</asp:ListItem>
                                  </asp:DropDownList>
                              </td>
                          </tr>
                      </table>
                      </tr>
                  </table>
              </asp:Panel>
              <hr style="width: 100%" />
              <asp:Panel ID="pModificar" runat="server" Style="margin-bottom: 0px">
                  <table cellpadding="4" cellspacing="0" style="width: 100%">
                      <tr>
                          <td style="text-align: left" colspan="4">
                              <strong>Datos a Modificar:</strong>
                          </td>
                      </tr>
                      <tr>
                          <td style="text-align: left; width: 26%;">
                              Fecha de Modificación<br />
                              <asp:TextBox ID="txtfecha" runat="server" CssClass="textbox" Width="50%"></asp:TextBox>
                          </td>
                          <td style="text-align: left; width: 37%;">
                              Clasificación<br />
                              <asp:DropDownList ID="ddlClasificacion" runat="server" AppendDataBoundItems="True"
                                  CssClass="textbox" Width="90%">
                                  <asp:ListItem Value="0">Seleccion un Item</asp:ListItem>
                              </asp:DropDownList>
                          </td>
                          <td style="text-align: left; width: 37%">
                              Tipo de Activo<br />
                              <asp:DropDownList ID="ddlTipoActivo" runat="server" Width="90%" CssClass="textbox"
                                  AppendDataBoundItems="True">
                                  <asp:ListItem Value="0">Seleccion un Item</asp:ListItem>
                              </asp:DropDownList>
                              <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlTipoActivo"
                                      Display="Dynamic" ErrorMessage="Seleccione un tipo de activo" ForeColor="Red" InitialValue="0"
                                      Style="font-size: xx-small" ValidationGroup="vgGuardar" />
                          </td>
                      </tr>
                      <table cellpadding="4" style="width: 100%">
                          <tr>
                              <td style="text-align: left; width: 25%;">
                                  Valor Residual<br />
                                  <uc1:decimales ID="txtValorResidual" runat="server" style="font-size: xx-small; text-align: right"
                                      TipoLetra="XX-Small" Habilitado="True" Enabled="True" Width_="90%" />
                              </td>
                              <td style="text-align: left; width: 25%;">
                                  Vida Util(Años)<br />
                                  <asp:TextBox ID="txtVidaUtil" runat="server" CssClass="textbox" Width="70%"></asp:TextBox>
                              </td>
                              <td style="text-align: left; width: 25%;">
                                  Vr. Deterioro<br />
                                  <uc1:decimales ID="txtDeterioro" runat="server" style="font-size: xx-small; text-align: right"
                                      TipoLetra="XX-Small" Habilitado="True" Enabled="True" Width_="90%" />
                              </td>
                              <td style="text-align: left; width: 25%;">
                                  Vr. Recup Deterioro<br />
                                  <uc1:decimales ID="txtRecupDeterioro" runat="server" style="font-size: xx-small;
                                      text-align: right" TipoLetra="XX-Small" Habilitado="True" Enabled="True" Width_="90%" />
                              </td>
                          </tr>
                          <tr>
                              <td style="text-align: left; width: 25%;" colspan="4">
                                  Observaciones<br />
                                  <asp:TextBox ID="txtObservacion" runat="server" CssClass="textbox" Width="70%" Height="45px"
                                      TextMode="MultiLine"></asp:TextBox>
                              </td>
                          </tr>
                      </table>
                      </tr>
                  </table>
              </asp:Panel>
          </asp:View>
          <asp:View ID="vwDatos" runat="server">
              <table style="width: 100%">
                  <asp:Panel ID="PanelFinal" runat="server">
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
                              <td style="text-align: center; font-size: large;">
                                  <asp:Label ID="lblMensaje" runat="server" Text="Datos Grabados Correctamente" Style="color: #FF3300"></asp:Label>
                              </td>
                          </tr>
                          <tr>
                              <td style="text-align: center; font-size: large;">
                                  <br />
                                  <br />
                              </td>
                          </tr>
                      </table>
                  </asp:Panel>
              </table>
          </asp:View>
         </asp:MultiView>
    <uc1:mensajeGrabar ID="ctlMensaje" runat="server"/>
</asp:Content>
