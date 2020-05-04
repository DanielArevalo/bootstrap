<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Page_Contabilidad_Conceptos_DIAN_Lista" %>

<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
       <script type="text/javascript" src="../../../Scripts/gridviewScroll.min.js"></script>   
    <script type="text/javascript">

        $(document).ready(function () {
            gridviewScroll();
        });

        function gridviewScroll() {
            $('#<%=gvLista.ClientID%>').gridviewScroll({
                width: 1200,
                height: 500,
                arrowsize: 30,
                varrowtopimg: "../../../Images/arrowvt.png",
                varrowbottomimg: "../../../Images/arrowvb.png",
                harrowleftimg: "../../../Images/arrowhl.png",
                harrowrightimg: "../../../Images/arrowhr.png"
            });
        }        
    </script>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>  
    
    <asp:Panel ID="pConsulta" runat="server">            
        <table cellpadding="0" cellspacing="0" style="width: 70%">
           
            <tr>
                <td style="text-align: left; font-size: x-small;" colspan="4">
                    <strong>Criterios de Búsqueda:</strong></td>
            </tr>
            <tr>
                <td style="text-align: left;">
                    Código<br />
                    <asp:TextBox ID="txtCodigo" runat="server" CssClass="textbox" Width="119px"></asp:TextBox>
                </td>
                <td style="text-align: left;">
                    Descripción<br />
                    <asp:TextBox ID="txtDescripcion" runat="server" CssClass="textbox" Width="300px"></asp:TextBox>
                </td>
                <td colspan="2" style="height: 15px; text-align: left;">
                </td>
            </tr>
            <tr>
                <td class="tdI" style="text-align: left; width: 205px;">
                </td>
                <td class="tdI" style="text-align: left">
                </td>
                <td class="tdI" style="text-align: left">
                </td>
                <td class="tdI" style="text-align: left">
                </td>
            </tr>
        </table>
    </asp:Panel>
       <hr />
    <table style="width:100%">
       
        <tr>  
            <td>
                <asp:GridView ID="gvLista" runat="server" Width="80%" AutoGenerateColumns="False"
                    AllowPaging="True" GridLines="Horizontal" ShowHeaderWhenEmpty="True"
                    DataKeyNames="codconcepto" OnRowEditing="gvLista_RowEditing"
                       OnRowDeleting="gvLista_RowDeleting" OnPageIndexChanging="gvLista_PageIndexChanging"
                    style="font-size: xx-small">
                    <Columns>                  
                     
                      
                      <asp:CommandField ButtonType="Image" EditImageUrl="~/Images/gr_edit.jpg" ShowEditButton="True" />
                        <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/gr_elim.jpg" ShowDeleteButton="True" />
                         <asp:BoundField DataField="codconcepto" HeaderText="Cod.Concepto">
                            <ItemStyle HorizontalAlign="Left"/>
                        </asp:BoundField>
                        <asp:BoundField DataField="Nombre" HeaderText="Nombre" >
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                    </Columns>
                    <HeaderStyle CssClass="gridHeader" />
                    <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                    <RowStyle CssClass="gridItem" />
                </asp:GridView>
                <asp:Label ID="lblTotalRegs" runat="server" Visible="False" />
                <asp:Label ID="lblInfo" runat="server" Text="Su consulta no obtuvo ningún resultado." Visible="False" />
            </td>
        </tr>        
    </table>

      <uc4:mensajeGrabar ID="ctlMensaje" runat="server"/>

</asp:Content>

