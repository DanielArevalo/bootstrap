<%@ Page Language="C#" MasterPageFile="~/General/Master/solicitud.master" AutoEventWireup="true"
    CodeFile="Nuevo.aspx.cs" Inherits="Lista" Title=".: Xpinn - Georeferencia :." %>

<%@ Register Assembly="GMaps" Namespace="Subgurim.Controles" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"  EnablePageMethods="true">
    </asp:ScriptManager>

        <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td colspan="2">
                <asp:Panel ID="pConsulta" runat="server" Visible="false">
                    <%--  <table id="tbCriterios" border="0" cellpadding="5" cellspacing="0" width="100%">
                   <tr>
                       <td class="tdI">
                       Codgeoreferencia&nbsp;<asp:CompareValidator ID="cvCODGEOREFENCIA" runat="server" ControlToValidate="txtCODGEOREFENCIA" ErrorMessage="Solo se admiten n&uacute;meros" Operator="DataTypeCheck" SetFocusOnError="True" Type="Double" ValidationGroup="vgGuardar" Display="Dynamic" ForeColor="Red" /><br/>
                       <asp:TextBox ID="txtCodgeoreferencia" CssClass="textbox" runat="server" MaxLength="128"/>
                       </td>
                       <td class="tdD">
                       Cod_persona&nbsp;<asp:CompareValidator ID="cvCOD_PERSONA" runat="server" ControlToValidate="txtCOD_PERSONA" ErrorMessage="Solo se admiten n&uacute;meros" Operator="DataTypeCheck" SetFocusOnError="True" Type="Double" ValidationGroup="vgGuardar" Display="Dynamic" ForeColor="Red" /><br/>
                       <asp:TextBox ID="txtCod_persona" CssClass="textbox" runat="server"/>
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI">
                       Latitud&nbsp;<br/>
                       <asp:TextBox ID="txtLatitud" CssClass="textbox" runat="server" MaxLength="128"/>
                       </td>
                       <td class="tdD">
                       Longitud&nbsp;<br/>
                       <asp:TextBox ID="txtLongitud" CssClass="textbox" runat="server"/>
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI">
                       Observaciones&nbsp;<br/>
                       <asp:TextBox ID="txtObservaciones" CssClass="textbox" runat="server" MaxLength="128"/>
                       </td>
                       <td class="tdD">
                       Fechacreacion&nbsp;<asp:CompareValidator ID="cvFECHACREACION" runat="server" ControlToValidate="txtFECHACREACION" ErrorMessage="Formato de Fecha (dd/mm/aaaa)" Operator="DataTypeCheck" SetFocusOnError="True" ToolTip="Formato fecha" Type="Date" ValidationGroup="vgGuardar" Display="Dynamic" ForeColor="Red" /><br/>
                       <asp:TextBox ID="txtFechacreacion" CssClass="textbox" runat="server"/>
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI">
                       Usuariocreacion&nbsp;<br/>
                       <asp:TextBox ID="txtUsuariocreacion" CssClass="textbox" runat="server" MaxLength="128"/>
                       </td>
                       <td class="tdD">
                       Fecultmod&nbsp;<asp:CompareValidator ID="cvFECULTMOD" runat="server" ControlToValidate="txtFECULTMOD" ErrorMessage="Formato de Fecha (dd/mm/aaaa)" Operator="DataTypeCheck" SetFocusOnError="True" ToolTip="Formato fecha" Type="Date" ValidationGroup="vgGuardar" Display="Dynamic" ForeColor="Red" /><br/>
                       <asp:TextBox ID="txtFecultmod" CssClass="textbox" runat="server"/>
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI">
                       Usuultmod&nbsp;<br/>
                       <asp:TextBox ID="txtUsuultmod" CssClass="textbox" runat="server" MaxLength="128"/>
                       </td>
                       <td class="tdD">
                       &nbsp;
                       </td>
                </table>--%>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td style="text-align: center" colspan="2">
                <strong>GEOREFERENCIACION</strong>
            </td>
        </tr>
        <tr>
            <td style="text-align:right " colspan="2">
                
                          &nbsp;</td>
        </tr>
        <tr>
            
            <td style="text-align: center">
                <asp:Panel ID="Panel1" runat="server" Visible="False">
                    Codgeoreferencia&nbsp;*<br />
                    </asp:Panel>
            </td>
        </tr>
        <tr>
            <td style="text-align: center" colspan="2">
                <asp:Label ID="Label1" runat="server" Text="" Visible="true"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="text-align: center" colspan="2">
                <hr />
            </td>
        </tr>
        <tr>
            <td style="text-align: center" colspan="2">
                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                <ContentTemplate>
                <br />
                <asp:TextBox ID="TextBox3" runat="server" Enabled =false></asp:TextBox>
                <asp:TextBox ID="TextBox4" runat="server" Enabled =false></asp:TextBox>
                </ContentTemplate>
                </asp:UpdatePanel>
                
            </td>
        </tr>
    </table>
    <%--<script type="text/javascript" language="javascript">
        function SetFocus()
        {
            document.getElementById('cphMain_txtCODGEOREFERENCIA').focus(); 
        }
        window.onload = SetFocus;
    </script>--%>
  <%--  <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
           
            <script language="javascript" type="text/javascript">

                var map;
                var geocoder;
                function InitializeMap() {

                    var latlng = new google.maps.LatLng(-34.397, 150.644);
                    var myOptions =
        {
            zoom: 14,
            center: latlng,
            mapTypeId: google.maps.MapTypeId.ROADMAP,
            disableDefaultUI: true
        };
                    map = new google.maps.Map(document.getElementById("map"), myOptions);
                }

                function FindLocaiton() {
                    geocoder = new google.maps.Geocoder();
                    InitializeMap();

                    var address = "<%=CsVariable%>";
                    geocoder.geocode({ 'address': address }, function (results, status) {
                        if (status == google.maps.GeocoderStatus.OK) {
                            map.setCenter(results[0].geometry.location);
                            var marker = new google.maps.Marker({
                                map: map,
                                position: results[0].geometry.location
                            });
                            if (results[0].formatted_address) {
                                region = results[0].formatted_address + '<br />';
                            }
                            var infowindow = new google.maps.InfoWindow({
                                content: '<div style =width:400px; height:400px;>Location info:<br/>Country Name:<br/>' + region + '<br/>LatLng:<br/>' + results[0].geometry.location + '</div>'
                            });
                            google.maps.event.addListener(marker, 'click', function () {
                                // Calling the open method of the infoWindow 
                                infowindow.open(map, marker);
                            });

                        }
                        else {
                            alert("Geocode was not successful for the following reason: " + status);
                        }
                    });


                }


                function Button1_onclick() {

                    FindLocaiton();
                }



                function addressinput_onclick() {

                }

            </script>--%>
      <%--  </ContentTemplate>
    </asp:UpdatePanel>--%>
    <script type="text/javascript" language="javascript">

        var asyncState = true;

        XMLHttpRequest.prototype.original_open = XMLHttpRequest.prototype.open;

        XMLHttpRequest.prototype.open = function (method, url, async, user, password) {

            async = asyncState;

            var eventArgs = Array.prototype.slice.call(arguments);

            return this.original_open.apply(this, eventArgs);
        }

</script>


    <script language="javascript">
        function obtener_localizacion() {
            if (navigator.geolocation) {
                navigator.geolocation.getCurrentPosition(mostrar_mapa, gestiona_errores);
            } else {
                alert('Tu navegador no soporta la API de geolocalizacion');
            }
        }
        function mostrar_mapa(position) {
            var latitud = position.coords.latitude;
            var longitud = position.coords.longitude;
            var TextBox1 = document.getElementById('<%=TextBox1.ClientID%>');
            TextBox1.value = latitud;
            var TextBox2 = document.getElementById('<%=TextBox2.ClientID%>');
            TextBox2.value = longitud;
            TextBox1.disabled = true;
            TextBox2.disabled = true;
            var boton = document.getElementById('z_btnAceptar');
//            boton.click();
            return true;
        }

        function gestiona_errores(err) {
            if (err.code == 0) {
                alert("error desconocido");
            }
            if (err.code == 1) {
                alert("El usuario no ha compartido su posicion");
            }
            if (err.code == 2) {
                alert("no se puede obtener la posicion actual");
            }
            if (err.code == 3) {
                alert("timeout recibiendo la posicion");
            }
        }

         

        

    </script>

    
    <script type="text/javascript">
        function z_metjsClick() {
            navigator.geolocation.getCurrentPosition(mostrar_mapa, gestiona_errores);
//            boton.click();
        }

    </script>
    
    

    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
        


        


        
        


        


         <asp:Button ID="z_btnAceptar" runat="server" Text="Mostrar Posición" onclick="z_btnAceptar_Click" OnClientClick="z_metjsClick()" />
       


    </ContentTemplate>
    </asp:UpdatePanel>

    <div align="center">
        <table width="100%">
            <tr>
                <td style="width: 325px">
                   

                    <cc1:GMap ID="gMap" runat="server" enableGoogleBar="True" enableHookMouseWheelToZoom="True"
                        enableServerEvents="True" Height="540px" Version="3" Width="430px" enableGKeyboardHandler="True"
                        serverEventsType="AspNetPostBack" enableStore="False" enableGetGMapElementById="True"
                        enableDragging="True" OnClick="gMap_Click" />

                   
                    
                </td>

                
                <td>
                <table  color="#333333">
                    <table  color="#333333">
                        <tr>
                            <td style="height: 25px">
                             
                            Observaciones
                            </td>
                            <td style="height: 25px">
                <asp:TextBox ID="txtObservaciones" runat="server" CssClass="textbox" MaxLength="128" 
                                    Width="363px" />
                            </td>
                        </tr>
                        
                        <tr>
                            <td style="width: 98px">
                                <strong>Referencia
                                1</strong></td>
                        </tr>
                        
                        <tr>
                            <td align="left" style="width: 98px;">
                                Nombre Referencia
                            </td>
                            <td align="left" class="ctrlLogin" style="width: 166px">
                                <asp:TextBox ID="txtreferencia1" runat="server" Width="394px"></asp:TextBox>
                                
                            </td>
                        </table>
                        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                             <ContentTemplate>
                         <table  color="#333333">
                         
                         
                            <td align="left" style="width: 98px">
                                Antiguedad del negocio
                            </td>
                            <td align="left" class="ctrlLogin" style="width: 160px">


                            
                                <asp:RadioButton ID="Radioreferencia11" runat="server" Text="Menos de 1 año" 
                                    AutoPostBack="True" oncheckedchanged="Radioreferencia11_CheckedChanged" />
                                    </td>
                                     
                             
                                                                 <td>

                                <asp:RadioButton ID="Radioreferencia12" runat="server" Text="Mas de un año" 
                                    oncheckedchanged="Radioreferencia12_CheckedChanged" AutoPostBack="True"  />
                                    </td>
                                
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 98px">
                                Sabe si es
                            </td>
                            
                            <td class="ctrlLogin" style="width: 166px">
                                <asp:RadioButton ID="Radioreferencia13" runat="server" Text="Propietario" 
                                    oncheckedchanged="Radioreferencia13_CheckedChanged" AutoPostBack="True"  />
                                    </td>
                                    <td>
                                <asp:RadioButton ID="Radioreferencia14" runat="server" Text="Empleado" 
                                    oncheckedchanged="Radioreferencia14_CheckedChanged" AutoPostBack="True"  />
                                    </td>
                                
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 98px">
                                Concepto
                            </td>
                            <td align="left" class="ctrlLogin" style="width: 166px">
                                <asp:RadioButton ID="Radioreferencia15" runat="server" Text="Bueno" 
                                    oncheckedchanged="Radioreferencia15_CheckedChanged" AutoPostBack="True"  />
                                &nbsp;&nbsp;
                                <asp:RadioButton ID="Radioreferencia16" runat="server" Text="Regular" 
                                    oncheckedchanged="Radioreferencia16_CheckedChanged" AutoPostBack="True"  />
                                </td>
                                <td>
                                <asp:RadioButton ID="Radioreferencia17" runat="server" Text="Malo" 
                                    oncheckedchanged="Radioreferencia17_CheckedChanged" AutoPostBack="True"  />
                                &nbsp;&nbsp;
                                <asp:RadioButton ID="Radioreferencia18" runat="server" Text="Ninguno" 
                                    oncheckedchanged="Radioreferencia18_CheckedChanged" AutoPostBack="True"  />
                                    </td>
                                  


                                </table>
                                </ContentTemplate>
                             </asp:UpdatePanel>
                        <table  color="#333333">
                        <td>
                        </td>
                        <td>
                        </td>
                        </table>
                        <table  color="#333333">
                        
                            <tr>
                            <td style="width: 98px">
                                <strong>Referencia
                                2</strong></td>
                        </tr>
                        
                        <tr>
                            <td align="left" style="width: 98px">
                                Nombre Referencia
                            </td>
                            <td align="left" class="ctrlLogin" style="width: 166px">
                                <asp:TextBox ID="txtreferencia2" runat="server" Width="394px"></asp:TextBox>
                                
                            </td>
                        </table>
                        <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                             <ContentTemplate>
                        <table  color="#333333">
                        <tr>
                            <td align="left" style="width: 107px">
                                Antiguedad del negocio
                            </td>
                            <td align="left" class="ctrlLogin" style="width: 166px">
                                <asp:RadioButton ID="Radioreferencia21" runat="server" Text="Menos de 1 año" 
                                    oncheckedchanged="Radioreferencia21_CheckedChanged" AutoPostBack="True" />
                                </td>
                                <td>
                                <asp:RadioButton ID="Radioreferencia22" runat="server" Text="Mas de un año" 
                                    oncheckedchanged="Radioreferencia22_CheckedChanged" AutoPostBack="True" />
                                    </td>
                                
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 107px">
                                Sabe si es
                            </td>
                            <td align="left" class="ctrlLogin" style="width: 166px">
                                <asp:RadioButton ID="Radioreferencia23" runat="server" Text="Propietario" 
                                    oncheckedchanged="Radioreferencia23_CheckedChanged" AutoPostBack="True" />
                                    </td>
                                    <td>
                                
                                <asp:RadioButton ID="Radioreferencia24" runat="server" Text="Empleado" 
                                    oncheckedchanged="Radioreferencia24_CheckedChanged" AutoPostBack="True" />
                                    </td>
                                
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 107px">
                                Concepto
                            </td>
                            <td align="left" class="ctrlLogin" style="width: 166px">
                                <asp:RadioButton ID="Radioreferencia25" runat="server" Text="Bueno" 
                                    oncheckedchanged="Radioreferencia25_CheckedChanged" AutoPostBack="True" />
                                
                                &nbsp;&nbsp;
                                
                                <asp:RadioButton ID="Radioreferencia26" runat="server" Text="Regular" 
                                    oncheckedchanged="Radioreferencia26_CheckedChanged" AutoPostBack="True" />
                                </td>
                                <td>
                                <asp:RadioButton ID="Radioreferencia27" runat="server" Text="Malo" 
                                    oncheckedchanged="Radioreferencia27_CheckedChanged" AutoPostBack="True" />
                                &nbsp;&nbsp;
                                <asp:RadioButton ID="Radioreferencia28" runat="server" Text="Ninguno" 
                                    oncheckedchanged="Radioreferencia28_CheckedChanged" AutoPostBack="True" />
                                    </td>
                                
                            <td style="width: 98px">
                                &nbsp;</td>
                        
                        </table>
                        </ContentTemplate>
                             </asp:UpdatePanel>


                        <table  color="#333333">
                        <tr><td><strong>Referencia 3
                            
                        <tr>
                            <td align="left" style="width: 98px">
                                Nombre Referencia
                            </td>
                            <td align="left" class="ctrlLogin" style="width: 166px">
                                <asp:TextBox ID="txtreferencia3" runat="server" Width="394px"></asp:TextBox>
                                
                            </td>
                      
                        </table>
                        <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                             <ContentTemplate>
                        <table  color="#333333">
                            <td align="left" style="width: 98px">
                                Antiguedad del negocio
                            </td>
                            <td align="left" class="ctrlLogin" style="width: 166px">
                                <asp:RadioButton ID="Radioreferencia31" runat="server" Text="Menos de 1 año" 
                                    AutoPostBack="True" oncheckedchanged="Radioreferencia31_CheckedChanged" />
                               </td>
                               <td> 
                                <asp:RadioButton ID="Radioreferencia32" runat="server" Text="Mas de un año" 
                                    AutoPostBack="True" oncheckedchanged="Radioreferencia32_CheckedChanged"/>
                                    </td>
                                
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 98px">
                                Sabe si es
                            </td>
                            <td align="left" class="ctrlLogin" style="width: 166px">
                                <asp:RadioButton ID="Radioreferencia33" runat="server" Text="Propietario" 
                                    AutoPostBack="True" oncheckedchanged="Radioreferencia33_CheckedChanged" />
                                </td>
                                <td>
                                <asp:RadioButton ID="Radioreferencia34" runat="server" Text="Empleado" 
                                    AutoPostBack="True" oncheckedchanged="Radioreferencia34_CheckedChanged" />
                                    </td>
                                
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 98px">
                                Concepto
                            </td>
                            <td align="left" class="ctrlLogin" style="width: 166px">
                                <asp:RadioButton ID="Radioreferencia35" runat="server" Text="Bueno" 
                                    AutoPostBack="True" oncheckedchanged="Radioreferencia35_CheckedChanged" />
                                
                                &nbsp;&nbsp;
                                
                                <asp:RadioButton ID="Radioreferencia36" runat="server" Text="Regular" 
                                    AutoPostBack="True" oncheckedchanged="Radioreferencia36_CheckedChanged" />
                                </td>
                                <td>
                                <asp:RadioButton ID="Radioreferencia37" runat="server" Text="Malo" 
                                    AutoPostBack="True" oncheckedchanged="Radioreferencia37_CheckedChanged" />
                                
                                &nbsp;&nbsp;
                                
                                <asp:RadioButton ID="Radioreferencia38" runat="server" Text="Ninguno" 
                                    AutoPostBack="True" oncheckedchanged="Radioreferencia38_CheckedChanged" />
                                    </td>
                                
                            </td>
                        </tr>
                        </table>
                        </ContentTemplate>
                             </asp:UpdatePanel>
                          
    

                        




                    </table>
                    </table>
                </td>
            </tr>
        </table>
    </div>
  <%--  <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>--%>
            <asp:TextBox ID="TextBox1" runat="server" Enabled="False" ForeColor="Black"></asp:TextBox>
            <asp:TextBox ID="TextBox2" runat="server" Enabled="False" ForeColor="Black"></asp:TextBox>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
      <%--  </ContentTemplate>
    </asp:UpdatePanel>
    --%>
</asp:Content>