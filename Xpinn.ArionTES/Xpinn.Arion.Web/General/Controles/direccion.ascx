<%@ Control Language="C#" AutoEventWireup="true" CodeFile="direccion.ascx.cs"
    Inherits="Direccion" Debug="true"  %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<div>
<%--    <script src="../../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery-1.8.2.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.js" type="text/javascript"></script>--%>
    <asp:UpdatePanel ID="upZonaDireccion" runat="server">
        <ContentTemplate>
            <table cellspacing="0" cellpadding="0">
                <tr>
                    <td>
                        <asp:RadioButtonList ID="rbtnDetalleZonaGeo" runat="server" CssClass="componente"
                        Width="180px" AutoPostBack="true" OnSelectedIndexChanged="rbtnDetalleZonaGeo_SelectedIndexChanged"
                        RepeatDirection="Horizontal" ClientIDMode=Static>
                        <asp:ListItem Value="R">Rural</asp:ListItem>
                        <asp:ListItem Value="U">Urbana</asp:ListItem>
                        </asp:RadioButtonList>  
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="rfvtxtValorArriendo2" runat="server" 
                            ControlToValidate="txtDireccion" Display="Dynamic" ErrorMessage="Campo Requerido" 
                            ForeColor="Red" SetFocusOnError="True" ValidationGroup="vgGuardar" 
                                style="font-size: small" />
                        &nbsp;
                    </td>
                </tr>
            </table>
            <table border="0">
                <tr>
                    <td>
                        <asp:Panel ID="pTipoDireccion" runat="server" >
                            <asp:DropDownList ID="ddlVia" runat="server" AutoPostBack="true" 
                                OnSelectedIndexChanged="ddlVia_SelectedIndexChanged" 
                                CssClass="comboDireccionVia" Width="80px">
                                <asp:ListItem Value=""></asp:ListItem>
                                <asp:ListItem Value="AU">Autopista</asp:ListItem>
                                <asp:ListItem Value="AV">Avenida</asp:ListItem>
                                <asp:ListItem Value="AC">Avenida Calle</asp:ListItem>
                                <asp:ListItem Value="AK">Avenida Carrera</asp:ListItem>
                                <asp:ListItem Value="BL">Bulevar</asp:ListItem>
                                <asp:ListItem Value="CL">Calle</asp:ListItem>
                                <asp:ListItem Value="KR">Carrera</asp:ListItem>
                                <asp:ListItem Value="CT">Carretera</asp:ListItem>
                                <asp:ListItem Value="CQ">Circular</asp:ListItem>
                                <asp:ListItem Value="CC">Cuentas Corridas</asp:ListItem>
                                <asp:ListItem Value="DG">Diagonal</asp:ListItem>
                                <asp:ListItem Value="PJ">Pasaje</asp:ListItem>
                                <asp:ListItem Value="PS">Paseo</asp:ListItem>
                                <asp:ListItem Value="PT">Peatonal</asp:ListItem>
                                <asp:ListItem Value="TV">Transversal</asp:ListItem>
                                <asp:ListItem Value="TC">Troncal</asp:ListItem>
                                <asp:ListItem Value="VT">Variante</asp:ListItem>
                                <asp:ListItem Value="VI">Vía</asp:ListItem>
                            </asp:DropDownList>
                            <asp:DropDownList ID="ddlManzana" runat="server" AutoPostBack="true" 
                                OnSelectedIndexChanged="ddlManzana_SelectedIndexChanged" 
                                CssClass="comboDireccionManzana" Width="80px">
                                <asp:ListItem Value=""></asp:ListItem>
                                <asp:ListItem Value="MZ">Manzana</asp:ListItem>
                                <asp:ListItem Value="IN">Interior</asp:ListItem>
                                <asp:ListItem Value="SC">Sector</asp:ListItem>
                                <asp:ListItem Value="ET">Etapa</asp:ListItem>
                                <asp:ListItem Value="ED">Edificio</asp:ListItem>
                                <asp:ListItem Value="MD">Modulo</asp:ListItem>
                                <asp:ListItem Value="TO">Torre</asp:ListItem>
                            </asp:DropDownList>
                        </asp:Panel>
                    </td>
                    <td>
                        <asp:Panel ID="pVia" runat="server">
                            <table>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txtNombreVia" runat="server" AutoPostBack="true" 
                                            CssClass="cajaTextoGen" MaxLength="100" OnTextChanged="txtNombreVia_TextChanged" 
                                            Width="50px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:RequiredFieldValidator ID="rfvTxtNombreVia" runat="server" 
                                            ControlToValidate="txtNombreVia" Display="Dynamic" Enabled="False" 
                                            ErrorMessage="*" SetFocusOnError="true" ValidationGroup="vgGuardar"></asp:RequiredFieldValidator>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlLetra" runat="server" AutoPostBack="true" 
                                            CssClass="comboLetras" 
                                            OnSelectedIndexChanged="ddlLetra_SelectedIndexChanged">
                                            <asp:ListItem Value=" "></asp:ListItem>
                                            <asp:ListItem Value="A">A</asp:ListItem>
                                            <asp:ListItem Value="B">B</asp:ListItem>
                                            <asp:ListItem Value="C">C</asp:ListItem>
                                            <asp:ListItem Value="D">D</asp:ListItem>
                                            <asp:ListItem Value="E">E</asp:ListItem>
                                            <asp:ListItem Value="F">F</asp:ListItem>
                                            <asp:ListItem Value="G">G</asp:ListItem>
                                            <asp:ListItem Value="H">H</asp:ListItem>
                                            <asp:ListItem Value="I">I</asp:ListItem>
                                            <asp:ListItem Value="J">J</asp:ListItem>
                                            <asp:ListItem Value="K">K</asp:ListItem>
                                            <asp:ListItem Value="L">L</asp:ListItem>
                                            <asp:ListItem Value="M">M</asp:ListItem>
                                            <asp:ListItem Value="N">N</asp:ListItem>
                                            <asp:ListItem Value="O">O</asp:ListItem>
                                            <asp:ListItem Value="P">P</asp:ListItem>
                                            <asp:ListItem Value="Q">Q</asp:ListItem>
                                            <asp:ListItem Value="R">R</asp:ListItem>
                                            <asp:ListItem Value="S">S</asp:ListItem>
                                            <asp:ListItem Value="T">T</asp:ListItem>
                                            <asp:ListItem Value="U">U</asp:ListItem>
                                            <asp:ListItem Value="V">V</asp:ListItem>
                                            <asp:ListItem Value="W">W</asp:ListItem>
                                            <asp:ListItem Value="X">X</asp:ListItem>
                                            <asp:ListItem Value="Y">Y</asp:ListItem>
                                            <asp:ListItem Value="Z">Z</asp:ListItem>
                                            <asp:ListItem Value="AA">AA</asp:ListItem>
                                            <asp:ListItem Value="AB">AB</asp:ListItem>
                                            <asp:ListItem Value="AC">AC</asp:ListItem>
                                            <asp:ListItem Value="AD">AD</asp:ListItem>
                                            <asp:ListItem Value="AE">AE</asp:ListItem>
                                            <asp:ListItem Value="AF">AF</asp:ListItem>
                                            <asp:ListItem Value="AG">AG</asp:ListItem>
                                            <asp:ListItem Value="AH">AH</asp:ListItem>
                                            <asp:ListItem Value="AI">AI</asp:ListItem>
                                            <asp:ListItem Value="AJ">AJ</asp:ListItem>
                                            <asp:ListItem Value="AK">AK</asp:ListItem>
                                            <asp:ListItem Value="AL">AL</asp:ListItem>
                                            <asp:ListItem Value="AM">AM</asp:ListItem>
                                            <asp:ListItem Value="AN">AN</asp:ListItem>
                                            <asp:ListItem Value="AO">AO</asp:ListItem>
                                            <asp:ListItem Value="AP">AP</asp:ListItem>
                                            <asp:ListItem Value="AQ">AQ</asp:ListItem>
                                            <asp:ListItem Value="AR">AR</asp:ListItem>
                                            <asp:ListItem Value="AS">AS</asp:ListItem>
                                            <asp:ListItem Value="AT">AT</asp:ListItem>
                                            <asp:ListItem Value="AU">AU</asp:ListItem>
                                            <asp:ListItem Value="AV">AV</asp:ListItem>
                                            <asp:ListItem Value="AW">AW</asp:ListItem>
                                            <asp:ListItem Value="AX">AX</asp:ListItem>
                                            <asp:ListItem Value="AY">AY</asp:ListItem>
                                            <asp:ListItem Value="AZ">AZ</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlBis" runat="server" AutoPostBack="true" 
                                            CssClass="comboBis" OnSelectedIndexChanged="ddlBis_SelectedIndexChanged">
                                            <asp:ListItem Value=""> </asp:ListItem>
                                            <asp:ListItem Value="BIS">BIS</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlLetra2" runat="server" AutoPostBack="true" 
                                            CssClass="comboLetras" Enabled="false" 
                                            OnSelectedIndexChanged="ddlLetra2_SelectedIndexChanged">
                                            <asp:ListItem Value=" "></asp:ListItem>
                                            <asp:ListItem Value="A">A</asp:ListItem>
                                            <asp:ListItem Value="B">B</asp:ListItem>
                                            <asp:ListItem Value="C">C</asp:ListItem>
                                            <asp:ListItem Value="D">D</asp:ListItem>
                                            <asp:ListItem Value="E">E</asp:ListItem>
                                            <asp:ListItem Value="F">F</asp:ListItem>
                                            <asp:ListItem Value="G">G</asp:ListItem>
                                            <asp:ListItem Value="H">H</asp:ListItem>
                                            <asp:ListItem Value="I">I</asp:ListItem>
                                            <asp:ListItem Value="J">J</asp:ListItem>
                                            <asp:ListItem Value="K">K</asp:ListItem>
                                            <asp:ListItem Value="L">L</asp:ListItem>
                                            <asp:ListItem Value="M">M</asp:ListItem>
                                            <asp:ListItem Value="N">N</asp:ListItem>
                                            <asp:ListItem Value="O">O</asp:ListItem>
                                            <asp:ListItem Value="P">P</asp:ListItem>
                                            <asp:ListItem Value="Q">Q</asp:ListItem>
                                            <asp:ListItem Value="R">R</asp:ListItem>
                                            <asp:ListItem Value="S">S</asp:ListItem>
                                            <asp:ListItem Value="T">T</asp:ListItem>
                                            <asp:ListItem Value="U">U</asp:ListItem>
                                            <asp:ListItem Value="V">V</asp:ListItem>
                                            <asp:ListItem Value="W">W</asp:ListItem>
                                            <asp:ListItem Value="X">X</asp:ListItem>
                                            <asp:ListItem Value="Y">Y</asp:ListItem>
                                            <asp:ListItem Value="Z">Z</asp:ListItem>
                                            <asp:ListItem Value="AA">AA</asp:ListItem>
                                            <asp:ListItem Value="AB">AB</asp:ListItem>
                                            <asp:ListItem Value="AC">AC</asp:ListItem>
                                            <asp:ListItem Value="AD">AD</asp:ListItem>
                                            <asp:ListItem Value="AE">AE</asp:ListItem>
                                            <asp:ListItem Value="AF">AF</asp:ListItem>
                                            <asp:ListItem Value="AG">AG</asp:ListItem>
                                            <asp:ListItem Value="AH">AH</asp:ListItem>
                                            <asp:ListItem Value="AI">AI</asp:ListItem>
                                            <asp:ListItem Value="AJ">AJ</asp:ListItem>
                                            <asp:ListItem Value="AK">AK</asp:ListItem>
                                            <asp:ListItem Value="AL">AL</asp:ListItem>
                                            <asp:ListItem Value="AM">AM</asp:ListItem>
                                            <asp:ListItem Value="AN">AN</asp:ListItem>
                                            <asp:ListItem Value="AO">AO</asp:ListItem>
                                            <asp:ListItem Value="AP">AP</asp:ListItem>
                                            <asp:ListItem Value="AQ">AQ</asp:ListItem>
                                            <asp:ListItem Value="AR">AR</asp:ListItem>
                                            <asp:ListItem Value="AS">AS</asp:ListItem>
                                            <asp:ListItem Value="AT">AT</asp:ListItem>
                                            <asp:ListItem Value="AU">AU</asp:ListItem>
                                            <asp:ListItem Value="AV">AV</asp:ListItem>
                                            <asp:ListItem Value="AW">AW</asp:ListItem>
                                            <asp:ListItem Value="AX">AX</asp:ListItem>
                                            <asp:ListItem Value="AY">AY</asp:ListItem>
                                            <asp:ListItem Value="AZ">AZ</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlSentido" runat="server" AutoPostBack="true" 
                                            CssClass="comboSentido" OnSelectedIndexChanged="ddlSentido_SelectedIndexChanged" 
                                            Width="80px">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        #</td>
                                    <td>
                                        <asp:TextBox ID="txtNumero" runat="server" AutoPostBack="true" 
                                            CssClass="cajaTextoNumero" MaxLength="3" OnTextChanged="txtNumero_TextChanged" 
                                            Width="50px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:RequiredFieldValidator ID="rfvTxtNumero" runat="server" 
                                            ControlToValidate="txtNumero" Display="Dynamic" Enabled="False" 
                                            ErrorMessage="*" SetFocusOnError="true" ValidationGroup="vgGuardar"></asp:RequiredFieldValidator>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlLetra3" runat="server" AutoPostBack="true" 
                                            CssClass="comboLetras" 
                                            OnSelectedIndexChanged="ddlLetra3_SelectedIndexChanged">
                                            <asp:ListItem Value=""></asp:ListItem>
                                            <asp:ListItem Value="A">A</asp:ListItem>
                                            <asp:ListItem Value="B">B</asp:ListItem>
                                            <asp:ListItem Value="C">C</asp:ListItem>
                                            <asp:ListItem Value="D">D</asp:ListItem>
                                            <asp:ListItem Value="E">E</asp:ListItem>
                                            <asp:ListItem Value="F">F</asp:ListItem>
                                            <asp:ListItem Value="G">G</asp:ListItem>
                                            <asp:ListItem Value="H">H</asp:ListItem>
                                            <asp:ListItem Value="I">I</asp:ListItem>
                                            <asp:ListItem Value="J">J</asp:ListItem>
                                            <asp:ListItem Value="K">K</asp:ListItem>
                                            <asp:ListItem Value="L">L</asp:ListItem>
                                            <asp:ListItem Value="M">M</asp:ListItem>
                                            <asp:ListItem Value="N">N</asp:ListItem>
                                            <asp:ListItem Value="O">O</asp:ListItem>
                                            <asp:ListItem Value="P">P</asp:ListItem>
                                            <asp:ListItem Value="Q">Q</asp:ListItem>
                                            <asp:ListItem Value="R">R</asp:ListItem>
                                            <asp:ListItem Value="S">S</asp:ListItem>
                                            <asp:ListItem Value="T">T</asp:ListItem>
                                            <asp:ListItem Value="U">U</asp:ListItem>
                                            <asp:ListItem Value="V">V</asp:ListItem>
                                            <asp:ListItem Value="W">W</asp:ListItem>
                                            <asp:ListItem Value="X">X</asp:ListItem>
                                            <asp:ListItem Value="Y">Y</asp:ListItem>
                                            <asp:ListItem Value="Z">Z</asp:ListItem>
                                            <asp:ListItem Value="AA">AA</asp:ListItem>
                                            <asp:ListItem Value="AB">AB</asp:ListItem>
                                            <asp:ListItem Value="AC">AC</asp:ListItem>
                                            <asp:ListItem Value="AD">AD</asp:ListItem>
                                            <asp:ListItem Value="AE">AE</asp:ListItem>
                                            <asp:ListItem Value="AF">AF</asp:ListItem>
                                            <asp:ListItem Value="AG">AG</asp:ListItem>
                                            <asp:ListItem Value="AH">AH</asp:ListItem>
                                            <asp:ListItem Value="AI">AI</asp:ListItem>
                                            <asp:ListItem Value="AJ">AJ</asp:ListItem>
                                            <asp:ListItem Value="AK">AK</asp:ListItem>
                                            <asp:ListItem Value="AL">AL</asp:ListItem>
                                            <asp:ListItem Value="AM">AM</asp:ListItem>
                                            <asp:ListItem Value="AN">AN</asp:ListItem>
                                            <asp:ListItem Value="AO">AO</asp:ListItem>
                                            <asp:ListItem Value="AP">AP</asp:ListItem>
                                            <asp:ListItem Value="AQ">AQ</asp:ListItem>
                                            <asp:ListItem Value="AR">AR</asp:ListItem>
                                            <asp:ListItem Value="AS">AS</asp:ListItem>
                                            <asp:ListItem Value="AT">AT</asp:ListItem>
                                            <asp:ListItem Value="AU">AU</asp:ListItem>
                                            <asp:ListItem Value="AV">AV</asp:ListItem>
                                            <asp:ListItem Value="AW">AW</asp:ListItem>
                                            <asp:ListItem Value="AX">AX</asp:ListItem>
                                            <asp:ListItem Value="AY">AY</asp:ListItem>
                                            <asp:ListItem Value="AZ">AZ</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlBis2" runat="server" AutoPostBack="true" 
                                            CssClass="comboBis" OnSelectedIndexChanged="ddlBis2_SelectedIndexChanged">
                                            <asp:ListItem Value=""> </asp:ListItem>
                                            <asp:ListItem Value="BIS">BIS</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlLetra4" runat="server" AutoPostBack="true" 
                                            CssClass="comboLetras" Enabled="false" 
                                            OnSelectedIndexChanged="ddlLetra4_SelectedIndexChanged">
                                            <asp:ListItem Value="">
                                            </asp:ListItem>
                                            <asp:ListItem Value="A">A</asp:ListItem>
                                            <asp:ListItem Value="B">B</asp:ListItem>
                                            <asp:ListItem Value="C">C</asp:ListItem>
                                            <asp:ListItem Value="D">D</asp:ListItem>
                                            <asp:ListItem Value="E">E</asp:ListItem>
                                            <asp:ListItem Value="F">F</asp:ListItem>
                                            <asp:ListItem Value="G">G</asp:ListItem>
                                            <asp:ListItem Value="H">H</asp:ListItem>
                                            <asp:ListItem Value="I">I</asp:ListItem>
                                            <asp:ListItem Value="J">J</asp:ListItem>
                                            <asp:ListItem Value="K">K</asp:ListItem>
                                            <asp:ListItem Value="L">L</asp:ListItem>
                                            <asp:ListItem Value="M">M</asp:ListItem>
                                            <asp:ListItem Value="N">N</asp:ListItem>
                                            <asp:ListItem Value="O">O</asp:ListItem>
                                            <asp:ListItem Value="P">P</asp:ListItem>
                                            <asp:ListItem Value="Q">Q</asp:ListItem>
                                            <asp:ListItem Value="R">R</asp:ListItem>
                                            <asp:ListItem Value="S">S</asp:ListItem>
                                            <asp:ListItem Value="T">T</asp:ListItem>
                                            <asp:ListItem Value="U">U</asp:ListItem>
                                            <asp:ListItem Value="V">V</asp:ListItem>
                                            <asp:ListItem Value="W">W</asp:ListItem>
                                            <asp:ListItem Value="X">X</asp:ListItem>
                                            <asp:ListItem Value="Y">Y</asp:ListItem>
                                            <asp:ListItem Value="Z">Z</asp:ListItem>
                                            <asp:ListItem Value="AA">AA</asp:ListItem>
                                            <asp:ListItem Value="AB">AB</asp:ListItem>
                                            <asp:ListItem Value="AC">AC</asp:ListItem>
                                            <asp:ListItem Value="AD">AD</asp:ListItem>
                                            <asp:ListItem Value="AE">AE</asp:ListItem>
                                            <asp:ListItem Value="AF">AF</asp:ListItem>
                                            <asp:ListItem Value="AG">AG</asp:ListItem>
                                            <asp:ListItem Value="AH">AH</asp:ListItem>
                                            <asp:ListItem Value="AI">AI</asp:ListItem>
                                            <asp:ListItem Value="AJ">AJ</asp:ListItem>
                                            <asp:ListItem Value="AK">AK</asp:ListItem>
                                            <asp:ListItem Value="AL">AL</asp:ListItem>
                                            <asp:ListItem Value="AM">AM</asp:ListItem>
                                            <asp:ListItem Value="AN">AN</asp:ListItem>
                                            <asp:ListItem Value="AO">AO</asp:ListItem>
                                            <asp:ListItem Value="AP">AP</asp:ListItem>
                                            <asp:ListItem Value="AQ">AQ</asp:ListItem>
                                            <asp:ListItem Value="AR">AR</asp:ListItem>
                                            <asp:ListItem Value="AS">AS</asp:ListItem>
                                            <asp:ListItem Value="AT">AT</asp:ListItem>
                                            <asp:ListItem Value="AU">AU</asp:ListItem>
                                            <asp:ListItem Value="AV">AV</asp:ListItem>
                                            <asp:ListItem Value="AW">AW</asp:ListItem>
                                            <asp:ListItem Value="AX">AX</asp:ListItem>
                                            <asp:ListItem Value="AY">AY</asp:ListItem>
                                            <asp:ListItem Value="AZ">AZ</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        -</td>
                                    <td>
                                        <asp:TextBox ID="txtPlaca" runat="server" AutoPostBack="true" 
                                            CssClass="cajaTextoNumero" MaxLength="3" OnTextChanged="txtPlaca_TextChanged" 
                                            Width="50px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:RequiredFieldValidator ID="rfvTxtPlaca" runat="server" 
                                            ControlToValidate="txtPlaca" Display="Dynamic" Enabled="False" ErrorMessage="*" 
                                            SetFocusOnError="true" ValidationGroup="vgGuardar"></asp:RequiredFieldValidator>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlSentido2" runat="server" AutoPostBack="true" 
                                            CssClass="comboSentido" 
                                            OnSelectedIndexChanged="ddlSentido2_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                    <td>
                        <asp:Panel ID="pManzana" runat="server">
                            <table>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txtNombreManzana" runat="server" CssClass="cajaTextoGen" OnTextChanged="txtNombreManzana_TextChanged"
                                            AutoPostBack="true" MaxLength="100" Width="50px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:RequiredFieldValidator ID="rfvTxtNombreManzana" SetFocusOnError="true" runat="server"
                                            ErrorMessage="*" ControlToValidate="txtNombreManzana" Display="Dynamic" ValidationGroup="vgGuardar" Enabled="False"></asp:RequiredFieldValidator>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlUnidad" runat="server" CssClass="comboDireccionVia" OnSelectedIndexChanged="ddlUnidad_SelectedIndexChanged"
                                            AutoPostBack="true" Width="80px" >
                                            <asp:ListItem Value=""></asp:ListItem>
                                            <asp:ListItem Value="AL">Altillo</asp:ListItem>
                                            <asp:ListItem Value="AP">Apartamento</asp:ListItem>
                                            <asp:ListItem Value="BG">Bodega</asp:ListItem>
                                            <asp:ListItem Value="CS">Casa</asp:ListItem>
                                            <asp:ListItem Value="CN">Consultorio</asp:ListItem>
                                            <asp:ListItem Value="DP">Depósito</asp:ListItem>
                                            <asp:ListItem Value="DS">Depósito Sótano</asp:ListItem>
                                            <asp:ListItem Value="GA">Garaje</asp:ListItem>
                                            <asp:ListItem Value="GS">Garaje Sótano</asp:ListItem>
                                            <asp:ListItem Value="LC">Local</asp:ListItem>
                                            <asp:ListItem Value="LM">Local Mezzanine</asp:ListItem>
                                            <asp:ListItem Value="LT">Lote</asp:ListItem>
                                            <asp:ListItem Value="MN">Mezzanine</asp:ListItem>
                                            <asp:ListItem Value="OF">Oficina</asp:ListItem>
                                            <asp:ListItem Value="PA">Parqueadero</asp:ListItem>
                                            <asp:ListItem Value="PN">Pent-House</asp:ListItem>
                                            <asp:ListItem Value="PL">Planta</asp:ListItem>
                                            <asp:ListItem Value="PD">Predio</asp:ListItem>
                                            <asp:ListItem Value="SS">Semisótano</asp:ListItem>
                                            <asp:ListItem Value="SO">Sótano</asp:ListItem>
                                            <asp:ListItem Value="ST">Suite</asp:ListItem>
                                            <asp:ListItem Value="TZ">Terraza</asp:ListItem>
                                            <asp:ListItem Value="UN">Unidad</asp:ListItem>
                                            <asp:ListItem Value="UL">Unidad Residencial</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:RequiredFieldValidator ID="rfvDdlUnidad" SetFocusOnError="true" runat="server"
                                            ErrorMessage="*" ControlToValidate="ddlUnidad" Display="Dynamic" ValidationGroup="vgGuardar"
                                            Enabled="False"></asp:RequiredFieldValidator>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtNombreUnidad" runat="server" CssClass="cajaTextoGen" 
                                            OnTextChanged="txtNombreUnidad_TextChanged" AutoPostBack="true" MaxLength="100" 
                                            Width="50px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:RequiredFieldValidator ID="rfvTxtNombreUnidad" SetFocusOnError="true" runat="server"
                                            ErrorMessage="*" ControlToValidate="txtNombreUnidad" ValidationGroup="vgGuardar"
                                            Display="Dynamic" Enabled="False"></asp:RequiredFieldValidator>
                                    </td>
                                    </tr>
                                    </table>
                        </asp:Panel>
                    </td>
                    <td>
                        <asp:TextBox ID="txtComplemento" runat="server" CssClass="cajaTextoGen" 
                            OnTextChanged="txtComplemento_TextChanged" AutoPostBack="true" Visible="false" 
                            MaxLength="100" Width="50px"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <table>
                <tr>
                    <td>
                        <asp:TextBox ID="txtDireccion" runat="server" CssClass="cajaTextoDireccion" 
                            Enabled="false" Width="400px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvTxtDireccion" SetFocusOnError="true" runat="server"
                            ErrorMessage="*" ValidationGroup="vgGuardar" ControlToValidate="txtDireccion"
                            Display="Dynamic" Enabled="False"></asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <asp:LinkButton ID="btnLimpiarDireccion" runat="server"  CssClass="vinculo"
                            OnClick="btnLimpiarDireccion_Click">Limpiar</asp:LinkButton>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script type="text/javascript">
        $('#rbtnDetalleZonaGeo').on('change', function (e) {
            if (!e) e = window.event;
            if (e.preventDefault) {
                 //Firefox, Chrome
                e.preventDefault();
            } else {
                 //Internet Explorer
                e.returnValue = false;
            }

            $("#")
                });

    </script>
</div>
