﻿  <%@ Page Title=".: Contratacion :." Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" %>

<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>


<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">

    <asp:MultiView ID="mvPrincipal" ActiveViewIndex="0" runat="server">
      <asp:View runat="server" ID="viewDatos">
  
                <table style=" width: 100%;">
                     <tr>
                        <td class="tdI" colspan="3" style="color: #FFFFFF; background-color: #0066FF; height: 20px;">
                            <strong>Tipos De Contrato</strong>
                        </td>
                    </tr>
                </table>

  <table style=" width: 100%;">
      <tr>
          <td  style="height:30px; text-align: center; width: 328px;">
              Consecutivo
          </td>
          <td style="height:40px; text-align: center; width: 328px;">
              <asp:TextBox ID="txtcodcontratacion" runat="server" CssClass="textbox"></asp:TextBox>
          </td>
      </tr>
 </table>

            <table style="width: 100%;">
            <tr>
                <td  style="height:40px;">

                </td>
            </tr>
        </table>

        <table>
            <tr>
                <td style="height:30px; text-align: center; width: 328px">
                    Código De Contrato
                </td>
                <td style="height:30px; text-align: center; width: 328px ">
                    Descripción </td>
                <td style="height:30px; text-align: center; width: 328px ">
                    Dia Habil/Sábado
                </td>
           </tr>  

             <tr>
                <td style="height:40px; text-align: center;">
                    <asp:TextBox ID="txtcodcontrato" runat="server" CssClass="textbox"></asp:TextBox>
                </td>
                <td style="height:40px; text-align: center ">
                    <asp:TextBox ID="txtdescripcion" runat="server" CssClass="textbox"></asp:TextBox>
                </td>
                <td style="height:40px; text-align: center ">
                         <asp:DropDownList ID="ddldiahabil" runat="server" CssClass="textbox">

                            <asp:ListItem Value="SeleccioneUnItem">Seleccione Un Item</asp:ListItem>
                            <asp:ListItem Value="Si"> Si </asp:ListItem>
                            <asp:ListItem Value="No"> No </asp:ListItem>   

                         </asp:DropDownList>
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
