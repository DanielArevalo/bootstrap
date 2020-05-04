<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Nuevo.aspx.cs" Inherits="Page_Nomina_AutoliquidaSegSocial_Nuevo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
     <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:Label ID="error" runat="server"></asp:Label>
    &nbsp;&nbsp;&nbsp; 
    <script src="../../../Scripts/PCLBryan.js" type="text/javascript"></script>


    <asp:MultiView ID="mvDatos" ActiveViewIndex="0" runat="server">
        <asp:View runat="server">
    <asp:Panel ID="PnGeneral" runat="server" Height="105px">
        <table  border="0" style="height: 118px">
            <tr>
                 <td class="tdI" style="color: #FFFFFF; background-color: #0066FF; height: 20px; width: 100%;">
                            <strong>GENERALES</strong>
                        </td>
            </tr>
            <tr>
                <td class="tdI">
                    <asp:Panel ID="Pn1" runat="server">
                        <table>
                            <tr>
                <td style="width:200px;">
                    FONDO SALUD<br />
                    <br />
                    <asp:TextBox ID="txtFondoSalud"  MaxLength="4" runat="server" CssClass="textbox" Enabled="true" ></asp:TextBox>
                </td>
                <td style="width:200px;">
                    FONDO PENSIÓN<br /><br />
                    <asp:TextBox ID="txtFondoPension" MaxLength="4"  runat="server" CssClass="textbox"  Enabled="true"></asp:TextBox>
                </td>
                <td style="width:215px;" class="logo">
                    PORCENTAJE EMPLEADOR SALUD<br />
                    <asp:TextBox ID="txtPorcEmpleadoSalud" MaxLength="4"  runat="server" CssClass="textbox"  Enabled="true" ></asp:TextBox>
                </td>
                                 <td style="width:200px;">
                    PORCENTAJE SALUD PENSIONADO<br />
                    <asp:TextBox ID="txtPorcSaludpensionado" MaxLength="4"  runat="server" CssClass="textbox"  Enabled="true" ></asp:TextBox>
                </td>
                                 <td style="width:200px;">
                    PORCENTAJE EMPLEADOR PENSIÓN<br />
                    <asp:TextBox ID="txtPorcEmlpension" MaxLength="4"  runat="server" CssClass="textbox"  Enabled="true" ></asp:TextBox>
                </td>
                                 <td style="width:200px;">
                                     &nbsp;</td>
                                </tr>
                            </table>
                        </asp:Panel>
                </td>
            </tr>
        </table>
    </asp:Panel>
                 <asp:Panel ID="PanelSeguridadSocial" runat="server" Width="813px" >
        <table  border="0" style="width: 804px">
            <tr>
                 <td class="tdI" colspan="4" style="color: #FFFFFF; background-color: #0066FF; height: 21px; ">
                            APORTES PARAFISCALES&nbsp;
                            <br />
                        </td>
                 <td class="tdI"  height: 21px; width: 65%;">&nbsp;</td>
                 <td></td>
                 <tr>
                     <td style="width: 121px">Caja&nbsp;Compensación
                         <br />
                         <asp:TextBox ID="txtcajacompensacion" runat="server" CssClass="textbox" Enabled="true" MaxLength="4"></asp:TextBox>
                     </td>
                     <td style="width: 110px">Sena<br />
                         <asp:TextBox ID="Txtsena" runat="server" CssClass="textbox" Enabled="true" MaxLength="4"></asp:TextBox>
                     </td>
                     <td style="width: 110px">ICBF
                         <br />
                         <asp:TextBox ID="txtIcbf" runat="server" CssClass="textbox" Enabled="true" MaxLength="4"></asp:TextBox>
                     </td>
                     <td style="width: 110px">%Salario Integral<br />
                         <asp:TextBox ID="txtporSalarioIntegral" runat="server" CssClass="textbox" Enabled="true" MaxLength="4"></asp:TextBox>
                         <br />
                     </td>
                     <td style="width: 26px">&nbsp;</td>
                     <td>&nbsp;</td>
                     <td style="width: 70px">&nbsp;</td>
                     <td style="width: 150px">&nbsp;</td>
                 </tr>
            </tr>  
            </table>
                     </asp:Panel>
            <asp:Panel ID="PanelSeguridadSocial1" runat="server">
                <table border="0" style="width: 696px">
                    <tr>
                        <td class="tdI" colspan="9" style="color: #FFFFFF; background-color: #0066FF; height: 20px; ">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; PRESTACIONES SOCIALES&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <br />
                        </td>
                         <tr>
                            <td>Prima de Servicios<br />
                                <br />
                                <asp:TextBox ID="txtPrimaServicios" runat="server" CssClass="textbox" Enabled="true" MaxLength="4"></asp:TextBox>
                            </td>
                            <td>&nbsp;</td>
                            <td>Dias mínimo
                                <br />
                                para Prima<br />
                                <asp:TextBox ID="txtPrimaServiciosDias" runat="server" CssClass="textbox" Enabled="true"></asp:TextBox>
                            </td>
                            <td style="width: 109px">Cesantías<br />
                                <br />
                                <asp:TextBox ID="TxtCesantias" runat="server" CssClass="textbox" Enabled="true" MaxLength="4"></asp:TextBox>
                            </td>
                            <td>Intereses sobre<br /> &nbsp;las cesantías
                                <br />
                                <asp:TextBox ID="TxtInteresCesantias" runat="server" CssClass="textbox" Enabled="true" MaxLength="4"></asp:TextBox>
                            </td>
                            <td style="width: 76px">&nbsp; Vacaciones<br />
                                <br />
                                <asp:TextBox ID="txtVacaciones" runat="server" CssClass="textbox" Enabled="true" MaxLength="4"></asp:TextBox>
                            </td>
                            <td>Dias ley
                                <br />
                                vacaciones<br />
                                <asp:TextBox ID="txtDiaVacaciones" runat="server" CssClass="textbox" Enabled="true"></asp:TextBox>
                            </td>
                            <td>Max. Periodos acumulados<br />
                                <asp:TextBox ID="txtperiodosvacaciones" runat="server" CssClass="textbox" Enabled="true"></asp:TextBox>
                            </td>
                        </tr>
                    </tr>
                    <tr>
                          <td class="tdI" colspan="4" style="color: #FFFFFF; background-color: #0066FF;  height: 21px; ">INCAPACIDADES&nbsp; </td>
                    
                        <td class="tdI" colspan="4" style="color: #FFFFFF; background-color: #0066FF;  height: 21px; ">RETENCIÓN EN LA FUENTE&nbsp; </td>
                      
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>Dias empresa</td>
                        <td>&nbsp;</td>
                        <td>Porcentaje</td>
                        <td style="margin-left: 40px;">Reconoce /smlv-<br /> <span style="font-size: xx-small">cuando base es menor</span></td>
                        <td># Salarios Retención</td>
                        <td style="width: 76px">%Retención</td>
                        <td style="width: 150px">
                            Base
                            <br />
                            Máxima</td>
                        <td style="width: 150px">UVT</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtIncapacidadesDias" runat="server" CssClass="textbox" Enabled="true"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                        <td>
                            <asp:TextBox ID="txtporIncapacidad" runat="server" CssClass="textbox" Enabled="true" MaxLength="4"></asp:TextBox>
                        </td>
                        <td style="margin-left: 40px;">
                            <asp:RadioButton ID="rdbInaSobreSMLVSI" runat="server" AutoPostBack="true" GroupName="Grup32"  Text="Si"  />
                            <asp:RadioButton ID="rdbInaSobreSMLVNO" runat="server" AutoPostBack="true" GroupName="Grup32"  Text="No" />
                        </td>
                        <td>
                            <asp:TextBox ID="txtCantidadSalRetencion" runat="server" CssClass="textbox" Enabled="true" MaxLength="4"></asp:TextBox>
                        </td>
                        <td style="width: 76px">
                            <asp:TextBox ID="txtPorcRetencion" runat="server" CssClass="textbox" Enabled="true" MaxLength="4"></asp:TextBox>
                        </td>
                        <td style="width: 150px">
                            <asp:TextBox ID="txtBaseMaxima" runat="server" CssClass="textbox" Enabled="true" MaxLength="4"></asp:TextBox>
                            <br />
                        </td>
                        <td style="width: 150px">
                            <asp:TextBox ID="txtuvt" runat="server" CssClass="textbox" Enabled="true" MaxLength="5"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="8" style="color: #FFFFFF; background-color: #0066FF";>OTROS CONCEPTOS DE LEY
                        </td>
                       
                    </tr>
                    <tr>
                        <td colspan="2">Contribuyente<br />
                            <br />
                            <asp:RadioButton ID="rdbContribuyenteSi" runat="server" AutoPostBack="true" GroupName="Grup21" OnCheckedChanged="rdbContribuyenteSi_CheckedChanged" Text="Si" />
                            <asp:RadioButton ID="rdbContribuyenteNo" runat="server" AutoPostBack="true" GroupName="Grup21" OnCheckedChanged="rdbContribuyenteNo_CheckedChanged" Text="No" />
                        </td>
                        <td colspan="2">
                            <asp:Panel ID="Panel5" runat="server" Width="324px">
                                <table style="border-collapse:collapse;">
                                    <tr style="border:1px solid black; border-collapse:collapse;">
                                        <td style="width:300px; text-align:left;border:1px solid black;border-collapse:collapse;">Conceptos Contribuyente
                                            <br />
                                        </td>
                                    </tr>
                                    <tr style="border:1px solid black; border-collapse:collapse;">
                                        <td style="width:300px;border:1px solid black;border-collapse:collapse;">
                                            <asp:CheckBox ID="chkSalud" runat="server" Text="SALUD" />
                                            &nbsp;&nbsp;<asp:CheckBox ID="chkICBF" runat="server" Text="ICBF" />
                                            &nbsp; &nbsp;
                                            <asp:CheckBox ID="chkSena" runat="server" Text="SENA" />
                                            &nbsp; &nbsp;&nbsp;&nbsp;<asp:CheckBox ID="chkCCF" runat="server" Text="CCF" />
                                            &nbsp;
                                            <br />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                        <td>Régimen T. Especial<br />
                            <asp:RadioButton ID="rdbRTESi" runat="server" GroupName="Grup20" Text="Si" />
                            <asp:RadioButton ID="rdbRTENo" runat="server" GroupName="Grup20" Text="No" />
                        </td>
                        <td style="width: 76px">Salarios Base<asp:TextBox ID="TXTbaseRTE" runat="server" CssClass="textbox" onkeypress="return isNumber(event)" />
                        </td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                </table>
            </asp:Panel>
  <asp:Panel ID="PanelOtros" runat="server" >
                <table border="0" style="width: 904px">
                    <tr>                      
                        <td class="tdI" colspan="4" style="color: #FFFFFF; background-color: #0066FF; height: 21px; ">OTROS</td>
                         


                         


                        <td class="tdI" colspan="7" style="color: #FFFFFF; background-color: #0066FF; height: 21px; ">GIROS</td>
                         
                        
                         


                    </tr>
                    <tr>
                        <td>Aprobador Planilla<br />
                            <asp:TextBox ID="txtaprobador" runat="server" CssClass="textbox" Enabled="true"></asp:TextBox>
                        </td>
                        <td>
                            Revisor Planilla<br />
                            <asp:TextBox ID="txtrevisor" runat="server" CssClass="textbox" Enabled="true"></asp:TextBox>
                        </td>
                        <td>Aproximar descuentos<br />
                            <asp:RadioButton ID="rdbAproxSi" runat="server" GroupName="Grup26" Text="Si" OnCheckedChanged="rdbAproxSi_CheckedChanged" AutoPostBack="true" />
                            <asp:RadioButton ID="rdbAproxNo" runat="server" GroupName="Grup26" Text="No" AutoPostBack="true" OnCheckedChanged="rdbAproxNo_CheckedChanged"/>
                        </td>
                        <td>
                            <asp:Panel ID="PanelAproximacion" runat="server" Width="192px">
                                <table style="border-collapse:collapse;">
                                    <tr style="border:1px solid black; border-collapse:collapse;">
                                        <td style="width:300px; text-align:left;border:1px solid black;border-collapse:collapse;">Valor&nbsp; aproximación
                                            <br />
                                        </td>
                                    </tr>
                                    <tr style="border:1px solid black; border-collapse:collapse;">
                                        <td style="width:300px;border:1px solid black;border-collapse:collapse;">
                                            <asp:RadioButton ID="rdb50cercano" runat="server" GroupName="Grup27" Text="50" />
                                            <asp:RadioButton ID="rdbCentesima" runat="server" GroupName="Grup27" Text="100" />
                                            &nbsp; &nbsp;
                                            <asp:RadioButton ID="rdbMilesima" runat="server" GroupName="Grup27" Text="1000" />
                                            &nbsp;&nbsp;&nbsp;&nbsp;
                                            </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                        <td colspan="6" style="text-align: center">Banco Giro
                            <br />
                            <asp:DropDownList ID="ddlEntidadOrigen" runat="server" AutoPostBack="True" CssClass="textbox" OnSelectedIndexChanged="ddlEntidadOrigen_SelectedIndexChanged" Style="margin-left: 0px; text-align: left" Width="80%">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>Tercero(Seg.Social)<br />
                            <br />
                            <asp:RadioButton ID="rdbTerceroEmp" runat="server" AutoPostBack="true" GroupName="Grup28" Text="Empleado" />
                            <asp:RadioButton ID="rdbTerceroEntidad" runat="server" AutoPostBack="true" GroupName="Grup28" Text="Entidad" />
                        </td>
                        <td>Paga Nov. vacaciones<br /> &nbsp;<asp:RadioButton ID="rdbNovVacacionesSi" runat="server" AutoPostBack="true" GroupName="Grup29" Text="Si" />
                            <asp:RadioButton ID="rdbNovVacacionesNo" runat="server" AutoPostBack="true" GroupName="Grup29" Text="No" />
                        </td>
                        <td>Paga vacaciones Anticipadas<br /> &nbsp;<asp:RadioButton ID="rdbVacacionesAntSi" runat="server" AutoPostBack="true" GroupName="Grup30" Text="Si" />
                            <asp:RadioButton ID="rdbVacacionesAntNo" runat="server" AutoPostBack="true" GroupName="Grup30" Text="No" />
                        </td>
                        <td>Reconoce Retroactivo<br /> &nbsp;<asp:RadioButton ID="rdbRetroactivoSi" runat="server" AutoPostBack="true" GroupName="Grup31" Text="Si" />
                            <asp:RadioButton ID="rdbRetroactivoNo" runat="server" AutoPostBack="true" GroupName="Grup31" Text="No" />
                        </td>
                        <td colspan="6" style="text-align: center">Cuenta
                            <br />
                            <asp:DropDownList ID="ddlCuentaOrigen" runat="server" CssClass="textbox" Style="margin-left: 0px; text-align: left" Width="80%">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>Formato Desprendible </td>
                        <td style="margin-left: 40px;">
                            <asp:RadioButton ID="rdbDeprendible1" runat="server" AutoPostBack="true" GroupName="Grup33" Text="1" />
                            <asp:RadioButton ID="rdbDeprendible2" runat="server" AutoPostBack="true" GroupName="Grup33" Text="2" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                </table>
            </asp:Panel>

             <asp:Panel ID="Pn2" runat="server" Visible="false">
                 <hr style="width:100%;"/>
                 <table>
                     <tr>
                         <td style="width:200px; text-align:center;">max.SMLV para&nbsp; ARL<br />
                             <asp:TextBox ID="txtSMLVmaxArl" runat="server" CssClass="textbox" Enabled="true"></asp:TextBox>
                         </td>
                         <td colspan="2">max.SLMV&nbsp; para Parafiscales<br />
                             <asp:TextBox ID="txtSMLVmaxParafiscales" runat="server" CssClass="textbox" Enabled="true"></asp:TextBox>
                         </td>
                         <td colspan="2" style="text-align: center;">max.SLMV&nbsp;&nbsp; para Salud y Pensión<br />
                             <asp:TextBox ID="txtSMLVmaxSaludPension" runat="server" CssClass="textbox" Enabled="true"></asp:TextBox>
                         </td>
                     </tr>
                     <tr>
                         <td style="width:200px; text-align:left;">Permitir pagar inactividades con valor superior a 25 SLMV: </td>
                         <td style="width:100px;">
                             <asp:RadioButton ID="rdbSi1" runat="server" GroupName="Grup1" Text="Si" />
                             &nbsp; &nbsp;
                             <asp:RadioButton ID="rdbNo1" runat="server" GroupName="Grup1" Text="No" />
                         </td>
                         <td style="width:100px;"></td>
                         <td style="width:200px; text-align:left;">Marcar VST en la liquidación automáticamente: </td>
                         <td style="width:100px;">
                             <asp:RadioButton ID="rdbSi2" runat="server" GroupName="Grup2" Text="Si" />
                             &nbsp;&nbsp;
                             <asp:RadioButton ID="rdbNo2" runat="server" GroupName="Grup2" Text="No" />
                         </td>
                     </tr>
                 </table>
            </asp:Panel>
            <asp:Panel ID="Pn3" runat="server" Visible="false">
                <table>
                    <tr>
                        <td style="width:200px; text-align:left;">Descontar aportes mínimo sobre los 10 SMLV para los salarios integrales: </td>
                        <td style="width:100px;">
                            <asp:RadioButton ID="rdbSi3" runat="server" GroupName="Grup3" Text="Si" />
                            &nbsp; &nbsp;
                            <asp:RadioButton ID="rdbNo3" runat="server" GroupName="Grup3" Text="No" />
                        </td>
                        <td style="width:100px;"></td>
                        <td style="width:200px; text-align:left;">Descontar aportes al empleado mínimo sobre el SMLV: </td>
                        <td style="width:100px;">
                            <asp:RadioButton ID="rdbSi4" runat="server" GroupName="Grup4" Text="Si" />
                            &nbsp;&nbsp;
                            <asp:RadioButton ID="rdbNo4" runat="server" GroupName="Grup4" Text="No" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
             <asp:Panel ID="PnDetallado" runat="server" Visible="false">
        <table  border="0">
            <tr>
                 <td class="tdI" colspan="3" style="color: #FFFFFF; background-color: #0066FF; height: 20px; width: 100%;">
                            <strong>DETALLADO</strong>
                        </td>
            </tr>  
        <tr>
            <td class="tdI">
                <asp:Panel ID="Panel3" runat="server">
           
                    <table style="border-collapse:collapse;">
                        <tr style="border:1px solid black; border-collapse:collapse;">
                            <td style="width:300px; text-align:left;border:1px solid black;border-collapse:collapse;">
                            <strong>Calcular inactividad sobre dís calendario:</strong>  <br />
                               
                            </td>
                            <td style="width:300px;text-align:left;border:1px solid black;border-collapse:collapse;">
                            <strong> Descuenta días de castigo:</strong><br />
                           
                            </td>
                            <td style="width:300px;text-align:left;border:1px solid black;border-collapse:collapse;">
                             <strong> Tomar el NIT para el archivo plano:</strong>   <br />
                              
                            </td>
                            <td style="width:300px; border:1px solid black;border-collapse:collapse;" rowspan="2">
                          <strong>Base inactividad días calendario:</strong>   
                            </td>
                           <td style="width:300px; border:1px solid black;border-collapse:collapse;">   
                               <asp:CheckBox ID="chk30dias" runat="server" Text="Calcular sobre 30 días" />
                           </td>
                        </tr>
                        <tr style="border:1px solid black; border-collapse:collapse;">
                            <td style="width:300px;border:1px solid black;border-collapse:collapse;">   
                                <asp:RadioButton ID="rdbSi5" runat="server" GroupName="Grup5" Text="Si" />
                               &nbsp; &nbsp; <asp:RadioButton ID="rdbNo5" runat="server" GroupName="Grup5" Text="No" /></td>

                              <td style="width:300px;border:1px solid black;border-collapse:collapse;">   
                                <asp:RadioButton ID="rdbSi6" runat="server" GroupName="Grup6" Text="Si" />
                               &nbsp; &nbsp; <asp:RadioButton ID="rdbNo6" runat="server" GroupName="Grup6" Text="No" /></td>

                              <td style="width:300px;border:1px solid black;border-collapse:collapse;">   
                                <asp:CheckBox ID="chkEmpresa" runat="server" Text="Empresa" />
                               &nbsp; &nbsp; <asp:CheckBox ID="chkNomina" runat="server" Text="Nomina" /></td>

                              <td style="width:300px;border:1px solid black;border-collapse:collapse;">   
                             <asp:CheckBox ID="chkTomarValor" runat="server" Text="Tomar valor reportado" /></td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
            <tr>
                <td></td>
            </tr>
            <tr>
                <td class="tdI">
                    <asp:Panel ID="Panel4" runat="server">
                        <table  style="border-collapse:collapse;">
                            <tr style="border:1px solid black; border-collapse:collapse;">
                                <td style="width:300px; text-align:left;border:1px solid black;border-collapse:collapse;" rowspan="4">
                                    <strong>Procedimiento Centro de trabajo ARP</strong>

                                </td>
                                <td style="width:300px; text-align:left;border:1px solid black;border-collapse:collapse;">
                                    <asp:RadioButton ID="rdbEmpleado1" runat="server" GroupName="Grup7" Text="Empleado" />
                                   
                                </td>
                             
                                <td style="width:300px; text-align:left;border:1px solid black;border-collapse:collapse;" rowspan="4">
                                    <strong>Tomar el ibc para el calculo de las inactividades</strong>

                                </td>
                                <td style="width:300px; text-align:left;border:1px solid black;border-collapse:collapse;">
                                    <asp:RadioButton ID="rdbMesIbcanterior" runat="server" GroupName="Grup8" Text="Mes anterios de mayor IBC" />
                                  
                                </td>
                                <td style="width:300px; text-align:left;border:1px solid black;border-collapse:collapse;" rowspan="2">
                                    <strong>Para el calculo de los primeros días</strong>

                                </td>
                                 <td style="width:300px; text-align:left;border:1px solid black;border-collapse:collapse;">
                                    <asp:RadioButton ID="rdbCalcIBCempleado" runat="server" GroupName="Grup9" Text="Calcular sobre el IBC del empleado" />
                                  
                                </td>
                            </tr>

                            <tr style="border:1px solid black; border-collapse:collapse;">
                           
                                <td style="width:300px; text-align:left;border:1px solid black;border-collapse:collapse;">
                                    <asp:RadioButton ID="rdbNomina1" runat="server" GroupName="Grup7" Text="Nomina" />

                                </td>
                                <td style="width:300px; text-align:left;border:1px solid black;border-collapse:collapse;">
                                    <asp:RadioButton ID="rdbSinImportar" runat="server" GroupName="Grup8" Text="Mes anterior sin importar si hay novedad" />
                                   
                                </td>
                             
                                <td style="width:300px; text-align:left;border:1px solid black;border-collapse:collapse;">
                                    <asp:RadioButton ID="rdbCalcSMLVemple" runat="server" GroupName="Grup9" Text="Calcular sobre el valor básico del empleado" />

                                </td>
                               
                            </tr>

                             <tr style="border:1px solid black; border-collapse:collapse;">
                                <td style="width:300px; text-align:left;border:1px solid black;border-collapse:collapse;">
                                    <asp:RadioButton ID="rdbCentroCostos1" runat="server" GroupName="Grup7" Text="Centro Costos" />

                                </td>
                                <td style="width:300px; text-align:left;border:1px solid black;border-collapse:collapse;">
                                    <asp:RadioButton ID="rdbsinnovedad" runat="server" GroupName="Grup8" Text="Mes anterior sin que haya novedad" />
                                   
                                </td>
                             
                                <td style="width:300px; text-align:left;border:1px solid black;border-collapse:collapse;" rowspan="2">
                                    <strong>Descontar salud y pensión en vacaciones sobre </strong>

                                </td>
                                   <td style="width:300px; text-align:left;border:1px solid black;border-collapse:collapse;">
                                   <asp:RadioButton ID="rdbVacacionespag" runat="server" GroupName="Grup10" Text="Calcular sober el valor pagado de vacaciones" />

                                </td>
                               
                            </tr>
                             <tr style="border:1px solid black; border-collapse:collapse;">
                                 <td></td>
                                 <td></td>
                                <td style="width:300px; text-align:left;border:1px solid black;border-collapse:collapse;">
                                    <asp:RadioButton ID="rdbVacacionesIBC" runat="server" GroupName="Grup10" Text="Calcular sobre el IBC del mes anterior al inicio de vacaciones" />

                                </td>
                              
                             
                             
                               
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
        </table>
    </asp:Panel>
            </asp:View>
        <asp:View ID="mvFinal" runat="server">
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
                            <asp:Label ID="lblMensajeGrabar" runat="server" Text="Documento Grabado Correctamente"></asp:Label>
                            
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <br />
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;">                            
                        </td>
                    </tr>
                </table>
            
            </asp:Panel>
        </asp:View>
         </asp:MultiView>
</asp:Content>

