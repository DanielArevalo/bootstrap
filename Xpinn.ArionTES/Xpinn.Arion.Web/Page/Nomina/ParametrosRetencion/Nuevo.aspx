  <%@ Page Title=".: Contratacion :." Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" %>

<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>


<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">

    <asp:MultiView ID="mvPrincipal" ActiveViewIndex="0" runat="server">
      <asp:View runat="server" ID="viewDatos">
  
                <table style=" width: 100%;">
                     <tr>
                        <td class="tdI" colspan="3" style="color: #FFFFFF; background-color: #0066FF; height: 20px;">
                            Rangos de Retención</td>
                    </tr>
                </table>

  <table>
      <tr>
          <td style="height:30px; text-align: center; width: 328px;">
              Consecutivo
          </td>
          <td style="height:30px; text-align: center; width: 328px ">Descripción </td>
          <td style="height:30px; text-align: center; width: 328px ">&nbsp;</td>
      </tr>
      <tr>
          <td style="height:40px; text-align: center;">
              <asp:TextBox ID="txtconsecutivo" runat="server"  Enabled="false" CssClass="textbox"></asp:TextBox>
          </td>
          <td style="height:40px; text-align: center ">
              <asp:TextBox ID="txtdescripcion" runat="server" CssClass="textbox" Width="256px"></asp:TextBox>
          </td>
          <td style="height:40px; text-align: center ">&nbsp;</td>
      </tr>
      <tr>
          <td style="height:40px; text-align: center;">
              <br />
          </td>
          <td style="height:40px; text-align: center ">
              <asp:Label ID="LblMaximo" runat="server" Text="Hasta Prom. UVT" Visible="true"></asp:Label>
              <br />
              <asp:TextBox ID="txtMaximo" runat="server" CssClass="textbox" Visible="true"></asp:TextBox>
              <br />
          </td>
          <td style="height:40px; text-align: center ">
              <asp:Label ID="LblPorcentaje" runat="server" Text="Porcentaje" Visible="true"></asp:Label>
              <br />
              <asp:TextBox ID="txtPorcentaje" runat="server" CssClass="textbox" Visible="true"></asp:TextBox>
          </td>
      </tr>
 </table>

      </asp:View>
          <asp:View runat="server" ID="viewGuardado">
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
                            <asp:Label ID="lblMensaje" runat="server" Text="Información  Guardada Correctamente"
                                Style="color: #FF3300"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <br />
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;"></td>
                    </tr>
                </table>
            </asp:Panel>
     
            
       </asp:View>
     
    </asp:MultiView>
</asp:Content>
