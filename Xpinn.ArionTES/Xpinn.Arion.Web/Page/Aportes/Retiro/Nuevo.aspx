<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="~/General/Controles/fecha.ascx" tagname="fecha" tagprefix="ucFecha" %>
<%@ Register Src="~/General/Controles/direccion.ascx" TagName="direccion" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/tnumero.ascx" TagName="txtPesos" TagPrefix="uc1" %>
<%@ Register src="~/General/Controles/decimales.ascx" tagname="decimales" tagprefix="uc2" %>
<%@ Register Src="~/General/Controles/ctlProcesoContable.ascx" TagName="procesoContable" TagPrefix="uc2" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

   <script type="text/javascript">
       function formatNumber(control, num_decimal) {
           var nums = new Array();
           var simb = "."; //Éste es el separador
           valor = control.value;

           var numeroLetras = valor.length; // numero de caracteres

           if (control.value.indexOf('.') > 0) {

               var posicion_decimal = valor.indexOf('.'); // Posicion del punto en la cadena
               var decimal = valor.substring(posicion_decimal); // Se obtiene el valor numerico decimal sin el punto

               decimal = decimal.replace(".", ""); // Se eliminan todos los puntos
               decimal = decimal.replace(/D/g, "");   //Ésta expresión regular solo permitira ingresar números
               decimal = decimal.substring(0, num_decimal) // Se acota el numero para que se muesten solo la cantidad de decimales especificada
               valor = valor.substring(0, posicion_decimal) // Se elimina el valor decimla del valor numerico

           }

           valor = valor.replace(/D/g, "");   //Ésta expresión regular solo permitira ingresar números

           nums = valor.split(""); //Se vacia el valor en un arreglo
           var long = nums.length - 1; // Se saca la longitud del arreglo
           var patron = 3; //Indica cada cuanto se ponen las comas
           var prox = 2; // Indica en que lugar se debe insertar la siguiente coma
           var res = "";

           while (long > prox) {
               nums.splice((long - prox), 0, simb); //Se agrega la coma
               prox += patron; //Se incrementa la posición próxima para colocar la coma

           }

           for (var i = 0; i <= nums.length - 1; i++) {
               res += nums[i]; //Se crea la nueva cadena para devolver el valor formateado
           }


           if (control.value.indexOf('.') > 0) {
               var enviar = res + "." + decimal
           }
           else {
               var enviar = res
           }

           control.value = enviar;
       }

       function control_clear(control) {
           var valor = control.value;

           if (valor.length > 0) {
               control.value = "";
           }
       }

    </script>
    
    <asp:Panel ID="panelGeneral" runat="server" style="margin-right: 0px" Width="866px">            
       
            <table cellpadding="5" cellspacing="0" style="width: 98%">           
            <tr>
                <td style="text-align: left; ">
              
                    <asp:MultiView ID="MvAfiliados" runat="server" ActiveViewIndex="0">
                        <asp:View ID="ViewAporte" runat="server">
                            <table style="width: 890px">
                                <tr>
                                    <td colspan="4" style="text-align: left; ">
                                        <strong>
                                        <asp:Label ID="LblMensaje" runat="server" Font-Bold="True" Font-Size="Medium" 
                                            ForeColor="Red" Visible="False" Width="911px"></asp:Label>
                                        Datos de la cuenta</strong>
                                        <hr style="width: 100%" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: left; width: 245px;">
                                        <asp:TextBox ID="txtgrupoaporte0" runat="server" CssClass="textbox" 
                                            Visible="False" Width="16px"></asp:TextBox>
                                        Número Aporte
                                        <br />
                                        <asp:TextBox ID="txtNumAporte" runat="server" CssClass="textbox" Width="192px" Enabled="False"></asp:TextBox>
                                    </td>
                                    <td style="text-align: left; width: 367px;">
                                        Linea Aporte
                                        <br />
                                        <asp:DropDownList ID="DdlLineaAporte" runat="server" Height="26px" 
                                            style="margin-bottom: 0px" Width="250px" Enabled="False" CssClass="textbox">
                                            <asp:ListItem Value="0">Seleccione un item</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td colspan="2" style="text-align: left;">                                       
                                        Saldo Total<br />                                       
                                        <uc2:decimales ID="txtSaldoTotal" runat="server" CssClass="textbox" Enabled="False" />                                        
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: left; width: 245px; ">
                                        <asp:TextBox ID="txtCodigoCliente" runat="server" CssClass="textbox" 
                                            Visible="False" Width="16px"></asp:TextBox>
                                        Identificación<span style="font-size: xx-small"><asp:RequiredFieldValidator 
                                            ID="rfvIdentificacion1" runat="server" 
                                            ControlToValidate="txtNumeIdentificacion" Display="Dynamic" 
                                            ErrorMessage="Campo Requerido" ForeColor="Red" SetFocusOnError="True" 
                                            style="font-size: x-small" ValidationGroup="vgConsultar" Width="121px" />
                                        <asp:TextBox ID="txtNumeIdentificacion" runat="server" AutoPostBack="True" 
                                            CssClass="textbox" onkeyup="Actualizar()" 
                                            Width="192px" Enabled="False"></asp:TextBox>
                                        </span>
                                    </td>
                                    <td style="text-align: left; width: 367px; ">
                                        Tipo Identificacion<br />
                                        <asp:DropDownList ID="DdlTipoIdentificacion" runat="server" Height="26px" Width="250px" Enabled="False" CssClass="textbox" />
                                    </td>
                                    <td colspan="2" style="text-align: left; ">
                                        Nombre<br />
                                        <asp:TextBox ID="txtNombre" runat="server" CssClass="textbox" Enabled="False" Width="371px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" style="text-align: left; height: 10px;">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td colspan="4" style="text-align: left; ">
                                        <strong>Datos del retiro </strong>
                                        <hr style="width: 100%" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: left; width: 245px;">
                                        Fecha Retiro&nbsp;<br />
                                        <asp:TextBox ID="txtFecha_retiro" runat="server" AutoPostBack="True" 
                                            CssClass="textbox" MaxLength="128" 
                                           Width="157px" />
                                        <asp:CalendarExtender ID="txtFecha_retiro_CalendarExtender" runat="server" 
                                            Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtFecha_retiro">
                                        </asp:CalendarExtender>
                                        <span style="font-size: xx-small">
                                        <asp:RequiredFieldValidator ID="rfvFechaRetiro" runat="server" 
                                            ControlToValidate="txtFecha_retiro" Display="Dynamic" 
                                            ErrorMessage="Campo Requerido" ForeColor="Red" SetFocusOnError="True" 
                                            style="font-size: x-small" ValidationGroup="vgConsultar" Width="245px" />
                                        <asp:CompareValidator ID="cvFecha_Retiro" runat="server" 
                                            ControlToValidate="txtFecha_retiro" Display="Dynamic" 
                                            ErrorMessage="Formato de Fecha (dd/MM/yyyy)" ForeColor="Red" 
                                            Operator="DataTypeCheck" SetFocusOnError="True" style="font-size: xx-small" 
                                            ToolTip="Formato fecha" Type="Date" ValidationGroup="vgGuardar" Width="242px" />
                                        </span>
                                    </td>
                                    <td style="text-align: left; width: 367px;">
                                        Valor Retiro <br />
                                        <uc2:decimales ID="txtValorRetiro" runat="server" CssClass="textbox" />                                          
                                    </td>
                                    <td style="text-align: left; width: 218px;">
                                        Num Autorización<span style="font-size: xx-small"><asp:TextBox ID="txtNumAutorizacion" runat="server" CssClass="textbox" Width="212px" />
                                        </span>
                                    </td>
                                    <td style="text-align: left; width: 218px;">
                                        <asp:Label ID="lblPorcentajeMax" runat="server" Text="% Máximo a retirar"></asp:Label>
                                        <span style="font-size: xx-small">
                                        <asp:TextBox ID="txtMaximoPorcentaje" runat="server" CssClass="textbox" Enabled="false"/>
                                        </span>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 15px; text-align: left; width: 245px;">
                                        Forma Pago<br />
                                        <asp:DropDownList ID="DropDownFormaDesembolso" runat="server" AutoPostBack="True" 
                                            CssClass="textbox" Height="26px" Width="84%" 
                                            onselectedindexchanged="DropDownFormaDesembolso_SelectedIndexChanged"
                                            style="margin-left: 0px; text-align:left">
                                        </asp:DropDownList>
                                    </td>
                                    <td style="height: 15px; text-align: left; width: 367px;">
                                        &nbsp;</td>
                                    <td style="height: 15px; text-align: left; width: 218px;">
                                        &nbsp;</td>
                                    <td style="height: 15px; text-align: left; width: 160px;">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="height: 6px; text-align: left; width: 245px;">
                                        <asp:Label ID="lblEntidadOrigen" runat="server" style="text-align:left" 
                                            Text="Banco de donde se Gira"></asp:Label>
                                        <asp:DropDownList ID="ddlEntidadOrigen" runat="server" AutoPostBack="True" 
                                            CssClass="textbox" onselectedindexchanged="ddlEntidadOrigen_SelectedIndexChanged" 
                                            style="margin-left: 0px; text-align:left" Width="84%">
                                        </asp:DropDownList>
                                    </td>
                                    <td style="height: 6px; text-align: left; width: 367px;">
                                        <asp:Label ID="lblNumCuentaOrigen" runat="server" style="text-align:left" 
                                            Text="Cuenta de donde se Gira"></asp:Label>
                                        <asp:DropDownList ID="ddlCuentaOrigen" runat="server" CssClass="textbox" 
                                            onselectedindexchanged="ddlCuentaOrigen_SelectedIndexChanged" 
                                            style="margin-left: 0px; text-align:left" Width="84%">
                                        </asp:DropDownList>
                                    </td>
                                    <td style="height: 6px; text-align: center; font-weight: 700; width: 218px;">
                                        &nbsp;</td>
                                    <td style="height: 6px; text-align: left; width: 160px;">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="height: 6px; text-align: left; width: 245px;">
                                        <asp:Label ID="lblEntidad" runat="server" style="text-align:left" 
                                            Text="Entidad"></asp:Label>
                                        <asp:DropDownList ID="DropDownEntidad" runat="server" CssClass="textbox" 
                                            Width="84%" onselectedindexchanged="DropDownEntidad_SelectedIndexChanged" 
                                            style="margin-left: 0px; text-align:left">
                                        </asp:DropDownList>
                                        <br />
                                    </td>
                                    <td style="height: 6px; text-align: left; width: 367px;">
                                        <asp:Label ID="lblNumCuenta" runat="server" style="text-align:left" 
                                            Text="Numero de Cuenta" Width="189px"></asp:Label><br />
                                        <asp:TextBox ID="txtnumcuenta" runat="server" CssClass="textbox" style="text-align:left" Width="129px"></asp:TextBox>
                                    </td>
                                    <td style="height: 6px; text-align: center;">
                                        <asp:Label ID="lblTipoCuenta" runat="server" style="text-align:left"
                                            Text="Tipo Cuenta"></asp:Label>
                                        <asp:DropDownList ID="DropDownCuenta" runat="server" 
                                            AppendDataBoundItems="True" CssClass="textbox"
                                            style="margin-left: 0px; text-align:left" Width="100%">
                                        </asp:DropDownList>
                                    </td>
                                    <td style="height: 6px; text-align: left; width: 160px;">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td colspan="4" style="height: 6px; text-align: left; ">
                                        Observaciones<br /> <span style="font-size: xx-small">
                                        <asp:TextBox ID="txtObservaciones" runat="server" CssClass="textbox" Width="731px"></asp:TextBox>                                        
                                        </span>
                                    </td>
                                </tr>
                            </table>
                        </asp:View>
                    </asp:MultiView>
                </td>
            </tr>   
        </table>
   

    <table style="width:86%">
        <tr>  
            <td>
                <asp:Label ID="lblTotalRegs" runat="server" Visible="False" />
                <asp:Label ID="lblInfo" runat="server" Text="Su consulta no obtuvo ningún resultado." Visible="False" />
                            <asp:Label ID="lblMensajeGrabar" runat="server" 
                                Text="Retiro Generados Correctamente" Visible="False"></asp:Label>
            </td>
        </tr>   
    </table>
   </asp:Panel>
    <uc4:mensajegrabar ID="ctlMensaje" runat="server" />
    <br />
    <asp:Panel ID="panelProceso" runat="server" Width="100%">
        <uc2:procesoContable ID="ctlproceso" runat="server" />
    </asp:Panel>
</asp:Content>
