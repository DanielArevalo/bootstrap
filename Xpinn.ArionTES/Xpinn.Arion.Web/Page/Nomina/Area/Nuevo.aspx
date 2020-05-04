<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Nuevo.aspx.cs" Inherits="Page_Nomina_Area_Nuevo" %>


<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>


<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager> 
    <asp:MultiView ID="mvComprobante" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwCuenta" runat="server">
                <table style=" width: 100%;">
                     <tr>
                        <td class="tdI" colspan="3" style="color: #FFFFFF; background-color: #0066FF; height: 20px;">
                            <strong>Areas - Departamentos </strong>
                        </td>
                    </tr>
                </table>

        <table style="width: 100%;">
            <tr>
                <td  style="height:40px;">

                </td>
            </tr>
        </table>

  <table style=" width: 50%;">
      <tr>
          <td  style="height:30px; text-align: left; ">
              Consecutivo
          </td>
          <td style="height:40px; text-align: left; width: 328px;">
              <asp:TextBox ID="txtIdArea" runat="server" CssClass="textbox" Enabled="false"></asp:TextBox>
          </td>
          
      </tr>
      <tr>
          <td style="height:30px; text-align: left; ">
              Nombre
          </td>
          <td style="height:40px; text-align: left; width: 328px;">
              <asp:TextBox ID="txtNombre" runat="server" CssClass="textbox"></asp:TextBox>
          </td>
      </tr>
    
 </table>

           
       </asp:View>
       <asp:View ID="vwFinal" runat="server">
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
                                 <asp:Label ID="lblMensajeGrabar" runat="server" Style="color: #FF3300" Text="Información Guardada Correctamente"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <br />
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;">&nbsp;
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </asp:View>
    </asp:MultiView>
</asp:Content>

