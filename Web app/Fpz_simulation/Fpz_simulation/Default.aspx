<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Fpz_simulation.Index" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
     <link rel="stylesheet" type="text/css" href="StyleSheet1.css"/>
    <style>
        body 
{
    background: #333 url('http://subtlepatterns.subtlepatterns.netdna-cdn.com/patterns/low_contrast_linen.png');
}
        .TextBox1
        {
            border-top-left-radius: 20px;
            border-top-right-radius: 20px;
            border-bottom-left-radius: 20px;
            border-bottom-right-radius: 20px;
            margin-left: 33px;
        }
        .auto-style2 {
            border: 2px solid #a1a1a1;
            padding: 10px 30px;
            background: url('/Images/Android-Phone-Grey-Color-Background-HD.jpg');
            width: 300px;
            border-radius: 15px;
            box-shadow: 2px 2px 5px #888888;
            font-family: "Eras Medium ITC";
        }
    </style>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
    </div>
     <div>
         
         <asp:AutoCompleteExtender
              ID="AutoCompleteExtender1" 
              runat="server"
              TargetControlID="TextBox1" 
              MinimumPrefixLength="1"
              EnableCaching ="true" 
              CompletionSetCount="1" 
              CompletionInterval="1" ServiceMethod="GetCity"></asp:AutoCompleteExtender>
        
             <asp:Label ID="Label1" runat="server" Text="Upišite ime predmeta:" style="position: absolute; z-index: 1; left: 19px; top: 116px" CssClass="labels"></asp:Label>
        <asp:Label ID="Label19" runat="server" style="position: absolute; z-index: 1; left: 434px; top: 152px; width: 84px; right: 825px; height: 29px; text-decoration: underline;" Text="Podaci:" CssClass="labels"></asp:Label>
        
             <asp:TextBox ID="TextBox1" runat="server" style="position: absolute; top: 112px; left: 203px; height: 20px; width: 163px; margin-top: 0px; z-index: 1; right: 928px;" OnTextChanged="TextBox1_TextChanged" CssClass="TextBox1"></asp:TextBox>
        
         <asp:Label ID="Label20" runat="server" style="z-index: 1; left: 20px; top: 223px; position: absolute" Text="Odaberite Nastavnika:" CssClass="labels"></asp:Label>
        
         <asp:Image ID="Image1" runat="server" style="position: relative; top: 104px; left: 267px; height: 113px; width: 132px; margin-top: 0px;" BorderStyle="None" />
        
     </div>
         <div>
             <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" style="position: absolute; top: 110px; left: 440px; height: 29px; width: 84px; z-index: 1;" Text="Potvrdi" CssClass="TextBox1" />
      
                <asp:Image ID="Image2" runat="server"  style="z-index: 1; left: 909px; top: 243px; position: absolute; height: 180px; width: 189px" CssClass="Listbox" BorderStyle="Groove" />
        
        </div>
        <div style="position: relative; top: 67px; left: 426px; height: 360px; width: 155px; margin-right: 24px;" class="labels">

            Prezime:<br />
            <br />
            Ime:<br />
            <br />
            Email:<br />
            <br />
            Titula:<br />
            <br />
            Soba:<br />
            <br />
            Razina:<br />
            <br />
            <br />
            Konzultacije:<br />
            <br />
            <br />
            Trenuntno stanje:<br />
            <br />
            <br />
            <br />
           <br />
           <br />
           <br />
           <br />
           <br />
           <br />
           <br />
           <br />
           <br />
           <br />
           <br />
           <br />
           <br />

       </div>
        <div style="position: relative; top: -291px; left: 623px; width: 206px; height: 352px">

            <asp:Label ID="Label10" runat="server" Text="&quot;&quot;" CssClass="labelrezultat"></asp:Label>
            <br />
            <br />
            <asp:Label ID="Label11" runat="server" Text="&quot;&quot;" CssClass="labelrezultat"></asp:Label>
            <br />
            <br />
            <asp:Label ID="Label12" runat="server" Text="&quot;&quot;" CssClass="labelrezultat"></asp:Label>
            <br />
            <br />
            <br />
            <asp:Label ID="Label13" runat="server" Text="&quot;&quot;" CssClass="labelrezultat"></asp:Label>
            <br />
            <br />
            <asp:Label ID="Label14" runat="server" Text="&quot;&quot;" CssClass="labelrezultat"></asp:Label>
            <br />
            <br />
            <asp:Label ID="Label15" runat="server" Text="&quot;&quot;" Width="150px" CssClass="labelrezultat"></asp:Label>
            <br />
            <br />
            <asp:Label ID="Label17" runat="server" Text="&quot;&quot;" Width="223px" CssClass="labelrezultat" Height="19px"></asp:Label>

            <asp:Label ID="Label16" runat="server" Text="&quot;&quot;" Width="223px" CssClass="labelrezultat" Height="16px"></asp:Label>
            <br />

            <br />
            <br />
            <asp:Label ID="Label18" runat="server" Text="&quot;&quot;" CssClass="labelrezultat"></asp:Label>

            <br />

            <br />

        </div>
        <asp:ListBox ID="ListBox2" runat="server" CssClass="Listbox" style="margin-left: 24px; top: 271px; left: 17px; position: absolute; height: 194px; width: 118px;" Rows="8" AutoPostBack="True" OnSelectedIndexChanged="ListBox2_SelectedIndexChanged" EnableViewState="True" ViewStateMode="Enabled"></asp:ListBox>
      
          <div style="height: 65px; z-index: 1; left: 225px; top: 564px; position: absolute; width: 719px" class="box" >
           <a class="float-shadow" href="Default.aspx">Home</a>&nbsp;&nbsp;
           <a class="float-shadow" href="Dvorane.aspx">Slobodne ucionice</a>&nbsp;&nbsp;
           <a class="float-shadow" href="Raspored_sati.aspx">Raspored sati</a>&nbsp;&nbsp;
           <a class="float-shadow" href="Menza_raspored.aspx">Menza(jelovnik)</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
           <a class="float-shadow" href="Vozni_red.aspx">Zet linije(Kampus)</a>&nbsp;&nbsp;&nbsp; &nbsp;
           <a class="float-shadow" href="Kalendar.html">Kalendar Fpz</a>&nbsp;&nbsp;&nbsp; &nbsp;
           <a class="float-shadow" href="#">O Stranici</a>
           <a class="float-shadow" href="Fpz_materjali.html">Fpz Materjali</a>
              
      </div>
        <div class="auto-style2" style="width: 687px; z-index: 1; left: 234px; top: 23px; position: absolute; height: 26px"><strong style="font-size: large">FPZinfo</strong></div>
        
    </form>
</body>
</html>