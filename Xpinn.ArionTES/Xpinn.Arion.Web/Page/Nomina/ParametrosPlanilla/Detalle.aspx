<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/General/Master/site.master" CodeFile="Detalle.aspx.cs" Inherits="Detalle" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <br />
    <br />
     <style type="text/css">
        table.tabs {
            border-collapse: separate;
            border-spacing: 0;
            background-color: green 0px;
            font-size: 0.5em;
            width: 100px;
            margin-right: 2px;
            height: 100px;
        }

        th.tabck {
            border: red 0px;
            border-bottom: 0;
            border-radius: 0.0em 0.0em 0 0;
            background-color: green 0px;
            padding: 0.3em;
            text-align: center;
            cursor: pointer;
        }

        tr.filadiv {
            background-color: rgb(255, 255, 255);
        }
        /* El ancho y alto de los div.tabdiv se configuran en cada aplicación */
        div.tabdiv {
            background-color: rgb(255, 255, 255);
            border: 0;
            padding: 0.5em;
            overflow: auto;
            display: none;
            width: 100%;
            height: auto;
        }
        /* Anchos y altos para varios contenedores en la misma página. Esta parte se particulariza para cada contenedor. (IE8 necesita !important) */
        td#tab-0 > div {
            width: 25em !important;
            height: 25em !important;
        }

        .style1 {
            width: 13px;
        }
       
    </style>
    <script type="text/javascript">
       

      

        function ActiveTabChanged(sender, e) {
        }

        var HighlightAnimations = {};

        function Highlight(el) {
            if (HighlightAnimations[el.uniqueID] == null) {
                HighlightAnimations[el.uniqueID] = Sys.Extended.UI.Animation.createAnimation({
                    AnimationName: "color",
                    duration: 0.5,
                    property: "style",
                    propertyKey: "backgroundColor",
                    startValue: "#FFFF90",
                    endValue: "#FFFFFF"
                }, el);
            }
            HighlightAnimations[el.uniqueID].stop();
            HighlightAnimations[el.uniqueID].play();
        }

        function ToggleHidden(value) {
            $find('<%=Tabs.ClientID%>').get_tabs()[2].set_enabled(value);
        }

        function mpeSeleccionOnOk() {
        }

        function mpeSeleccionOnCancel() {
        }

     
    </script>
     
    <asp:MultiView ID="mvDatos" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwDatos" runat="server">
           
            <asp:TabContainer  ID="Tabs" runat="server" ActiveTabIndex="9" CssClass="CustomTabStyle"
            OnClientActiveTabChanged="ActiveTabChanged" Style="margin-right: 30px"
            Width="1047px">
                
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:TabPanel ID="TabPanel1" runat="server" HeaderText="Parametrizacion  Columna 1">
                    <HeaderTemplate>
                        Columna 1
                    </HeaderTemplate>
                    <ContentTemplate>
                      <table cellspacing="10" style="width: 100%;">
                            <tr>
                                <td colspan="2" style="width: 100%; text-align: center;">
                                    <label><b>Parametrización Primera Columna</b></label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 50%">
                                    <label><b>Nombre Primera Columna</b></label>
                                </td>
                                <td style="width: 50%">
                                    <asp:TextBox ID="txtNombrePrimeraColumna" runat="server" CssClass="textbox" MaxLength="15" Width="100%" />
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 50%; text-align: center;">
                                    <asp:CheckBox ID="chkVisiblePrimeraColumna" runat="server" Font-Bold="True" Text="Es Visible?" />
                                </td>
                                <td style="width: 50%; text-align: center;">
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="width: 100%; text-align: center;">
                                    <label><b>Conceptos para agrupar en esta columna</b></label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="width: 100%; text-align: center;">
                                     <asp:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="1" CssClass="CustomTabStyle" OnClientActiveTabChanged="ActiveTabChanged" Style="margin-right: 30px" Width="1000px">
                <asp:TabPanel ID="TabPanel5" runat="server" HeaderText="Devengos">
                    <ContentTemplate> 

                                    <asp:CheckBoxList ID="chkConceptosPrimeraColumna1" runat="server" RepeatColumns="3" style="text-align: justify" Width="100%">
                                    </asp:CheckBoxList>
                                       </ContentTemplate>
                                    

                </asp:TabPanel>
                                         <asp:TabPanel ID="TabPanel6" runat="server" HeaderText="Deducciones">
                    <ContentTemplate> 

                                    <asp:CheckBoxList ID="chkConceptosPrimeraColumna2" runat="server" RepeatColumns="3" style="text-align: justify" Width="100%">
                                    </asp:CheckBoxList>
                                       </ContentTemplate>
                                    

                </asp:TabPanel>

                                     </asp:TabContainer>        
                               
                                    <br />
                                    <br />
                                </td>
                            </tr>
                        </table>
                        </ContentTemplate>
                
                </asp:TabPanel>
                
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:TabPanel ID="TabPanel2" runat="server" HeaderText="Parametrizacion Columna 2">
                    <HeaderTemplate>
                        Columna 2
                    </HeaderTemplate>
                    <ContentTemplate>
                         <table cellspacing="10" style="width: 100%;">
                            <tr>
                                <td colspan="2" style="width: 100%; text-align: center;">
                                    <label><b>Parametrización Segunda Columna</b></label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 50%">
                                    <label><b>Nombre Segunda Columna</b></label>
                                </td>
                                <td style="width: 50%">
                                    <asp:TextBox ID="txtNombreSegundaColumna" runat="server" CssClass="textbox" MaxLength="15" Width="100%" />
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 50%; text-align: center;">
                                    <asp:CheckBox ID="chkVisibleSegundaColumna" runat="server" Font-Bold="True" Text="Es Visible?" />
                                </td>
                                  <td style="width: 50%; text-align: center;">
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="width: 100%; text-align: center;">
                                    <label><b>Conceptos para agrupar en esta columna</b></label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="width: 100%; text-align: center;">
                                      <asp:TabContainer ID="TabContainer2" runat="server" ActiveTabIndex="0" CssClass="CustomTabStyle" OnClientActiveTabChanged="ActiveTabChanged" Style="margin-right: 30px" Width="1000px">
                <asp:TabPanel ID="TabPanel7" runat="server" HeaderText="Devengos">
                    <ContentTemplate> 

                                    <asp:CheckBoxList ID="chkConceptosSegundaColumna1" runat="server" RepeatColumns="3" style="text-align: justify" Width="100%">
                                    </asp:CheckBoxList>
                                    </ContentTemplate>
                                    
                    </asp:TabPanel>
                                         <asp:TabPanel ID="TabPanel8" runat="server" HeaderText="Deducciones">
                    <ContentTemplate> 

                                    <asp:CheckBoxList ID="chkConceptosSegundaColumna2" runat="server" RepeatColumns="3" style="text-align: justify" Width="100%">
                                    </asp:CheckBoxList>
                                       </ContentTemplate>
                                    

                </asp:TabPanel>

                                     </asp:TabContainer>        
                               
                                    <br />
                                    <br />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                
                </asp:TabPanel>
                
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:TabPanel ID="TabPanel3" runat="server" HeaderText="Parametrizacion Columna 3">
                     <HeaderTemplate>
                         Columna 3
                     </HeaderTemplate>
                     <ContentTemplate>
                           <table cellspacing="10" style="width: 100%;">
                            <tr>
                                <td colspan="2" style="width: 100%; text-align: center;">
                                    <label><b>Parametrización Tercera Columna</b></label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 50%">
                                    <label><b>Nombre Tercera Columna</b></label>
                                </td>
                                <td style="width: 50%">
                                    <asp:TextBox ID="txtNombreTerceraColumna" runat="server" CssClass="textbox" MaxLength="15" Width="100%" />
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 50%; text-align: center;">
                                    <asp:CheckBox ID="chkVisibleTerceraColumna" runat="server" Font-Bold="True" Text="Es Visible?" />
                                </td>
                                 <td style="width: 50%; text-align: center;">
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="width: 100%; text-align: center;">
                                    <label><b>Conceptos para agrupar en esta columna</b></label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="width: 100%; text-align: center;">
                                     <asp:TabContainer ID="TabContainer3" runat="server" ActiveTabIndex="1" CssClass="CustomTabStyle" OnClientActiveTabChanged="ActiveTabChanged" Style="margin-right: 30px" Width="1000px">
                <asp:TabPanel ID="TabPanel9" runat="server" HeaderText="Devengos">
                    <ContentTemplate> 
                                    <asp:CheckBoxList ID="chkConceptosTerceraColumna1" runat="server" RepeatColumns="3" style="text-align: justify" Width="100%">
                                    </asp:CheckBoxList>
                                    </ContentTemplate>
                                    
                    </asp:TabPanel>
                                         <asp:TabPanel ID="TabPanel10" runat="server" HeaderText="Deducciones">
                    <ContentTemplate> 

                                    <asp:CheckBoxList ID="chkConceptosTerceraColumna2" runat="server" RepeatColumns="3" style="text-align: justify" Width="100%">
                                    </asp:CheckBoxList>
                                       </ContentTemplate>
                                    

                </asp:TabPanel>

                                     </asp:TabContainer>     
                                    <br />
                                    <br />
                                </td>
                            </tr>
                        </table>
                     </ContentTemplate>
                
                </asp:TabPanel>
                
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:TabPanel ID="TabPanel4" runat="server" HeaderText="Parametrizacion Columna 4">
                     <HeaderTemplate>
                         Columna 4
                     </HeaderTemplate>
                     <ContentTemplate>
                           <table cellspacing="10" style="width: 100%;">
                            <tr>
                                <td colspan="2" style="width: 100%; text-align: center;">
                                    <label><b>Parametrización Cuarta Columna</b></label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 50%">
                                    <label><b>Nombre Cuarta Columna</b></label>
                                </td>
                                <td style="width: 50%">
                                    <asp:TextBox ID="txtNombreCuartaColumna" runat="server" CssClass="textbox" MaxLength="15" Width="100%" />
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 50%; text-align: center;">
                                    <asp:CheckBox ID="chkVisibleCuartaColumna" runat="server" Font-Bold="True" Text="Es Visible?" />
                                </td>
                                 <td style="width: 50%; text-align: center;">
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="width: 100%; text-align: center;">
                                    <label><b>Conceptos para agrupar en esta columna</b></label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="width: 100%; text-align: center;">
                                       <asp:TabContainer ID="TabContainer4" runat="server" ActiveTabIndex="1" CssClass="CustomTabStyle" OnClientActiveTabChanged="ActiveTabChanged" Style="margin-right: 30px" Width="1000px">
                <asp:TabPanel ID="TabPanel11" runat="server" HeaderText="Devengos">
                    <ContentTemplate> 
                                    <asp:CheckBoxList ID="chkConceptosCuartaColumna1" runat="server" RepeatColumns="3" style="text-align: justify" Width="100%">
                                    </asp:CheckBoxList>
                                    </ContentTemplate>
                                    
                    </asp:TabPanel>
                                         <asp:TabPanel ID="TabPanel12" runat="server" HeaderText="Deducciones">
                    <ContentTemplate> 

                                    <asp:CheckBoxList ID="chkConceptosCuartaColumna2" runat="server" RepeatColumns="3" style="text-align: justify" Width="100%">
                                    </asp:CheckBoxList>
                                       </ContentTemplate>
                                    

                </asp:TabPanel>
                                           </asp:TabContainer>
                                    <br />
                                    <br />
                                </td>
                            </tr>
                        </table>
                     </ContentTemplate>
                
                </asp:TabPanel>
                
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
               <asp:TabPanel ID="TabPanelQuinto" runat="server" HeaderText="Parametrizacion Columna 5">
                     <HeaderTemplate>
                         Columna 5
                     </HeaderTemplate>
                     <ContentTemplate>
                           <table cellspacing="10" style="width: 100%;">
                            <tr>
                                <td colspan="2" style="width: 100%; text-align: center;">
                                    <label><b>Parametrización Quinta Columna</b></label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 50%">
                                    <label><b>Nombre Quinta Columna</b></label>
                                </td>
                                <td style="width: 50%">
                                    <asp:TextBox ID="txtNombreQuintaColumna" runat="server" CssClass="textbox" MaxLength="15" Width="100%" />
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 50%; text-align: center;">
                                    <asp:CheckBox ID="chkVisibleQuintaColumna" runat="server" Font-Bold="True" Text="Es Visible?" />
                                </td>
                                 <td style="width: 50%; text-align: center;">
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="width: 100%; text-align: center;">
                                    <label><b>Conceptos para agrupar en esta columna</b></label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="width: 100%; text-align: center;">
                                       <asp:TabContainer ID="TabContainer5" runat="server" ActiveTabIndex="1" CssClass="CustomTabStyle" OnClientActiveTabChanged="ActiveTabChanged" Style="margin-right: 30px" Width="1000px">
                <asp:TabPanel ID="TabPanel14" runat="server" HeaderText="Devengos">
                    <ContentTemplate> 
                                    <asp:CheckBoxList ID="chkConceptosQuintaColumna1" runat="server" RepeatColumns="3" style="text-align: justify" Width="100%">
                                    </asp:CheckBoxList>
                                    </ContentTemplate>
                                    
                    </asp:TabPanel>
                                         <asp:TabPanel ID="TabPanel15" runat="server" HeaderText="Deducciones">
                    <ContentTemplate> 

                                    <asp:CheckBoxList ID="chkConceptosQuintaColumna2" runat="server" RepeatColumns="3" style="text-align: justify" Width="100%">
                                    </asp:CheckBoxList>
                                       </ContentTemplate>
                                    

                </asp:TabPanel>
                                           </asp:TabContainer>
                                    <br />
                                    <br />
                                </td>
                            </tr>
                        </table>
                     </ContentTemplate>
                
                </asp:TabPanel>

               <asp:TabPanel ID="TabPanelSexto" runat="server" HeaderText="Parametrizacion Columna 5">
                     <HeaderTemplate>
                         Columna 6
                     </HeaderTemplate>
                     <ContentTemplate>
                           <table cellspacing="10" style="width: 100%;">
                            <tr>
                                <td colspan="2" style="width: 100%; text-align: center;">
                                    <label><b>Parametrización Sexta Columna</b></label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 50%">
                                    <label><b>Nombre Sexta Columna</b></label>
                                </td>
                                <td style="width: 50%">
                                    <asp:TextBox ID="txtNombreSextaColumna" runat="server" CssClass="textbox" MaxLength="15" Width="100%" />
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 50%; text-align: center;">
                                    <asp:CheckBox ID="chkVisibleSextaColumna" runat="server" Font-Bold="True" Text="Es Visible?" />
                                </td>
                                 <td style="width: 50%; text-align: center;">
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="width: 100%; text-align: center;">
                                    <label><b>Conceptos para agrupar en esta columna</b></label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="width: 100%; text-align: center;">
                                       <asp:TabContainer ID="TabContainer6" runat="server" ActiveTabIndex="0" CssClass="CustomTabStyle" OnClientActiveTabChanged="ActiveTabChanged" Style="margin-right: 30px" Width="1000px">
                <asp:TabPanel ID="TabPanel13" runat="server" HeaderText="Devengos">
                    <ContentTemplate> 
                                    <asp:CheckBoxList ID="chkConceptosSextaColumna1" runat="server" RepeatColumns="3" style="text-align: justify" Width="100%">
                                    </asp:CheckBoxList>
                                    </ContentTemplate>
                                    
                    </asp:TabPanel>
                                         <asp:TabPanel ID="TabPanel16" runat="server" HeaderText="Deducciones">
                    <ContentTemplate> 

                                    <asp:CheckBoxList ID="chkConceptosSextaColumna2" runat="server" RepeatColumns="3" style="text-align: justify" Width="100%">
                                    </asp:CheckBoxList>
                                       </ContentTemplate>
                                    

                </asp:TabPanel>
                                           </asp:TabContainer>
                                    <br />
                                    <br />
                                </td>
                            </tr>
                        </table>
                     </ContentTemplate>
                
                </asp:TabPanel>
                   <asp:TabPanel ID="TabPanelSiete" runat="server" HeaderText="Parametrizacion Columna 5">
                       <HeaderTemplate>
                           &nbsp;Columna 7
                       </HeaderTemplate>
                     <ContentTemplate>
                           <table cellspacing="10" style="width: 100%;">
                            <tr>
                                <td colspan="2" style="width: 100%; text-align: center;">
                                    <label><b>Parametrización Séptima Columna</b></label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 50%">
                                    <label><b>Nombre Septima Columna</b></label>
                                </td>
                                <td style="width: 50%">
                                    <asp:TextBox ID="txtNombreSeptimaColumna" runat="server" CssClass="textbox" MaxLength="15" Width="100%" />
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 50%; text-align: center;">
                                    <asp:CheckBox ID="chkVisibleSeptimaColumna" runat="server" Font-Bold="True" Text="Es Visible?" />
                                </td>
                                 <td style="width: 50%; text-align: center;">
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="width: 100%; text-align: center;">
                                    <label><b>Conceptos para agrupar en esta columna</b></label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="width: 100%; text-align: center;">
                                       <asp:TabContainer ID="TabContainer7" runat="server" ActiveTabIndex="1" CssClass="CustomTabStyle" OnClientActiveTabChanged="ActiveTabChanged" Style="margin-right: 30px" Width="1000px">
                <asp:TabPanel ID="TabPanel17" runat="server" HeaderText="Devengos">
                    <ContentTemplate> 
                                    <asp:CheckBoxList ID="chkConceptosSeptimaColumna1" runat="server" RepeatColumns="3" style="text-align: justify" Width="100%">
                                    </asp:CheckBoxList>
                                    </ContentTemplate>
                                    
                    </asp:TabPanel>
                                         <asp:TabPanel ID="TabPanel18" runat="server" HeaderText="Deducciones">
                    <ContentTemplate> 

                                    <asp:CheckBoxList ID="chkConceptosSeptimaColumna2" runat="server" RepeatColumns="3" style="text-align: justify" Width="100%">
                                    </asp:CheckBoxList>
                                       </ContentTemplate>
                                    

                </asp:TabPanel>
                                           </asp:TabContainer>
                                    <br />
                                    <br />
                                </td>
                            </tr>
                        </table>
                     </ContentTemplate>
                
                </asp:TabPanel>
                
    <asp:TabPanel ID="TabPanelocho" runat="server" HeaderText="Parametrizacion Columna 5">
                     <HeaderTemplate>
                         Columna 8
                     </HeaderTemplate>
                     <ContentTemplate>
                           <table cellspacing="10" style="width: 100%;">
                            <tr>
                                <td colspan="2" style="width: 100%; text-align: center;">
                                    <label><b>Parametrización Octava Columna</b></label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 50%">
                                    <label><b>Nombre Octava Columna</b></label>
                                </td>
                                <td style="width: 50%">
                                    <asp:TextBox ID="txtNombreOctavaColumna" runat="server" CssClass="textbox" MaxLength="15" Width="100%" />
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 50%; text-align: center;">
                                    <asp:CheckBox ID="chkVisibleOctavaColumna" runat="server" Font-Bold="True" Text="Es Visible?" />
                                </td>
                                 <td style="width: 50%; text-align: center;">
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="width: 100%; text-align: center;">
                                    <label><b>Conceptos para agrupar en esta columna</b></label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="width: 100%; text-align: center;">
                                       <asp:TabContainer ID="TabContainer8" runat="server" ActiveTabIndex="1" CssClass="CustomTabStyle" OnClientActiveTabChanged="ActiveTabChanged" Style="margin-right: 30px" Width="1000px">
                <asp:TabPanel ID="TabPanel19" runat="server" HeaderText="Devengos">
                    <ContentTemplate> 
                                    <asp:CheckBoxList ID="chkConceptosOctavaColumna1" runat="server" RepeatColumns="3" style="text-align: justify" Width="100%">
                                    </asp:CheckBoxList>
                                    </ContentTemplate>
                                    
                    </asp:TabPanel>
                                         <asp:TabPanel ID="TabPanel20" runat="server" HeaderText="Deducciones">
                    <ContentTemplate> 

                                    <asp:CheckBoxList ID="chkConceptosOctavaColumna2" runat="server" RepeatColumns="3" style="text-align: justify" Width="100%">
                                    </asp:CheckBoxList>
                                       </ContentTemplate>
                                    

                </asp:TabPanel>


                                  


                                           </asp:TabContainer>
                                    <br />
                                    <br />
                                </td>
                            </tr>
                        </table>
                     </ContentTemplate>
                
                </asp:TabPanel>
   <asp:TabPanel ID="TabPanelnueve" runat="server" HeaderText="Parametrizacion Columna 5">
                     <HeaderTemplate>
                         Columna 9
                     </HeaderTemplate>
                     <ContentTemplate>
                           <table cellspacing="10" style="width: 100%;">
                            <tr>
                                <td colspan="2" style="width: 100%; text-align: center;">
                                    <label><b>Parametrización Novena Columna</b></label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 50%">
                                    <label><b>Nombre Novena Columna</b></label>
                                </td>
                                <td style="width: 50%">
                                    <asp:TextBox ID="txtNombreNovenaColumna" runat="server" CssClass="textbox" MaxLength="15" Width="100%" />
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 50%; text-align: center;">
                                    <asp:CheckBox ID="chkVisibleNovenaColumna" runat="server" Font-Bold="True" Text="Es Visible?" />
                                </td>
                                 <td style="width: 50%; text-align: center;">
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="width: 100%; text-align: center;">
                                    <label><b>Conceptos para agrupar en esta columna</b></label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="width: 100%; text-align: center;">
                                       <asp:TabContainer ID="TabContainer9" runat="server" ActiveTabIndex="1" CssClass="CustomTabStyle" OnClientActiveTabChanged="ActiveTabChanged" Style="margin-right: 30px" Width="1000px">
                <asp:TabPanel ID="TabPanel21" runat="server" HeaderText="Devengos">
                    <ContentTemplate> 
                                    <asp:CheckBoxList ID="chkConceptosNovenaColumna1" runat="server" RepeatColumns="3" style="text-align: justify" Width="100%">
                                    </asp:CheckBoxList>
                                    </ContentTemplate>
                                    
                    </asp:TabPanel>
                                         <asp:TabPanel ID="TabPanel22" runat="server" HeaderText="Deducciones">
                    <ContentTemplate> 

                                    <asp:CheckBoxList ID="chkConceptosNovenaColumna2" runat="server" RepeatColumns="3" style="text-align: justify" Width="100%">
                                    </asp:CheckBoxList>
                                       </ContentTemplate>
                                    

                </asp:TabPanel>


                                  


                                           </asp:TabContainer>
                                    <br />
                                    <br />
                                </td>
                            </tr>
                        </table>
                     </ContentTemplate>
                
</asp:TabPanel>

   <asp:TabPanel ID="TabPanelDiez" runat="server" HeaderText="Parametrizacion Columna 5">
                     <HeaderTemplate>
                         Columna 10
                     </HeaderTemplate>
                     <ContentTemplate>
                           <table cellspacing="10" style="width: 100%;">
                            <tr>
                                <td colspan="2" style="width: 100%; text-align: center;">
                                    <label><b>Parametrización Decima Columna</b></label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 50%">
                                    <label><b>Nombre Decima Columna</b></label>
                                </td>
                                <td style="width: 50%">
                                    <asp:TextBox ID="txtNombreDecimaColumna" runat="server" CssClass="textbox" MaxLength="15" Width="100%" />
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 50%; text-align: center;">
                                    <asp:CheckBox ID="chkVisibleDecimaColumna" runat="server" Font-Bold="True" Text="Es Visible?" />
                                </td>
                                 <td style="width: 50%; text-align: center;">
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="width: 100%; text-align: center;">
                                    <label><b>Conceptos para agrupar en esta columna</b></label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="width: 100%; text-align: center;">
                                       <asp:TabContainer ID="TabContainer10" runat="server" ActiveTabIndex="1" CssClass="CustomTabStyle" OnClientActiveTabChanged="ActiveTabChanged" Style="margin-right: 30px" Width="1000px">
                <asp:TabPanel ID="TabPanel24" runat="server" HeaderText="Devengos">
                    <ContentTemplate> 
                                    <asp:CheckBoxList ID="chkConceptosDecimaColumna1" runat="server" RepeatColumns="3" style="text-align: justify" Width="100%">
                                    </asp:CheckBoxList>
                                    </ContentTemplate>
                                    
                    </asp:TabPanel>
                                         <asp:TabPanel ID="TabPanel25" runat="server" HeaderText="Deducciones">
                    <ContentTemplate> 

                                    <asp:CheckBoxList ID="chkConceptosDecimaColumna2" runat="server" RepeatColumns="3" style="text-align: justify" Width="100%">
                                    </asp:CheckBoxList>
                                       </ContentTemplate>
                                    

                </asp:TabPanel>


                                  


                                           </asp:TabContainer>
                                    <br />
                                    <br />
                                </td>
                            </tr>
                        </table>
                     </ContentTemplate>
                
                </asp:TabPanel>

  <asp:TabPanel ID="TabPanelOnce" runat="server" HeaderText="Parametrizacion Columna 5">
                     <HeaderTemplate>
                         Columna 11
                     </HeaderTemplate>
                     <ContentTemplate>
                           <table cellspacing="10" style="width: 100%;">
                            <tr>
                                <td colspan="2" style="width: 100%; text-align: center;">
                                    <label><b>Parametrización Onceava Columna</b></label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 50%">
                                    <label><b>Nombre Onceava Columna</b></label>
                                </td>
                                <td style="width: 50%">
                                    <asp:TextBox ID="txtNombreOnceavaColumna" runat="server" CssClass="textbox" MaxLength="15" Width="100%" />
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 50%; text-align: center;">
                                    <asp:CheckBox ID="chkVisibleOnceavaColumna" runat="server" Font-Bold="True" Text="Es Visible?" />
                                </td>
                                 <td style="width: 50%; text-align: center;">
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="width: 100%; text-align: center;">
                                    <label><b>Conceptos para agrupar en esta columna</b></label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="width: 100%; text-align: center;">
                                       <asp:TabContainer ID="TabContainer11" runat="server" ActiveTabIndex="1" CssClass="CustomTabStyle" OnClientActiveTabChanged="ActiveTabChanged" Style="margin-right: 30px" Width="1000px">
                <asp:TabPanel ID="TabPanel23" runat="server" HeaderText="Devengos">
                    <ContentTemplate> 
                                    <asp:CheckBoxList ID="chkConceptosOnceavaColumna1" runat="server" RepeatColumns="3" style="text-align: justify" Width="100%">
                                    </asp:CheckBoxList>
                                    </ContentTemplate>
                                    
                    </asp:TabPanel>
                                         <asp:TabPanel ID="TabPanel26" runat="server" HeaderText="Deducciones">
                    <ContentTemplate> 

                                    <asp:CheckBoxList ID="chkConceptosOnceavaColumna2" runat="server" RepeatColumns="3" style="text-align: justify" Width="100%">
                                    </asp:CheckBoxList>
                                       </ContentTemplate>
                                    

                </asp:TabPanel>


                                  


                                           </asp:TabContainer>
                                    <br />
                                    <br />
                                </td>
                            </tr>
                        </table>
                     </ContentTemplate>
                
   </asp:TabPanel>
            </asp:TabContainer>
         
        </asp:View>
        <asp:View ID="vFinal" runat="server">
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
                            <asp:Label ID="lblMensaje" runat="server" Text="Información Guardada Correctamente"
                                Style="color: #FF3300"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                    </tr>
                    <tr>
                        <td>
                            <br />
                            <br />
                            <br />
                            <br />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </asp:View>
    </asp:MultiView>
</asp:Content>
