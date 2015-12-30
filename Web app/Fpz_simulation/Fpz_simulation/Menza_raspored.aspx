<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Menza_raspored.aspx.cs" Inherits="Fpz_simulation.Menza_raspored" %>

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
        .auto-style1 {
            border: 2px solid #a1a1a1;
            padding: 10px 30px;
            background: url('/Images/Android-Phone-Grey-Color-Background-HD.jpg');
            width: 300px;
            border-radius: 15px;
            box-shadow: 2px 2px 5px #888888;
            font-family: "Eras Medium ITC";
            font-weight: bold;
            font-size: large;
        }
        </style>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="width: 85px">
    
        <asp:ListBox ID="ListBox1" runat="server" style="z-index: 1; left: 210px; top: 120px; position: absolute; width: 180px; height: 236px;" CssClass="Listbox"></asp:ListBox>
    
    </div>
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" style="z-index: 1; left: 620px; top: 384px; position: absolute; height: 40px; width: 106px;" Text="Generiraj" CssClass="TextBox1" />
        <asp:ListBox ID="ListBox2" runat="server" style="z-index: 1; left: 458px; top: 122px; position: absolute; height: 236px; width: 180px" CssClass="Listbox"></asp:ListBox>
        <asp:ListBox ID="ListBox4" runat="server" style="z-index: 1; left: 962px; top: 125px; position: absolute; height: 236px; width: 154px; right: 194px;" CssClass="Listbox"></asp:ListBox>
        <asp:ListBox ID="ListBox3" runat="server" style="z-index: 1; left: 707px; top: 124px; position: absolute; height: 235px; width: 180px" CssClass="Listbox"></asp:ListBox>

        <div style="height: 63px; z-index: 1; left: 284px; top: 445px; position: absolute; width: 719px" class="box">
           <a class="float-shadow" href="Default.aspx">Home</a>&nbsp;&nbsp;
           <a class="float-shadow" href="Dvorane.aspx">Slobodne ucionice</a>&nbsp;&nbsp;
           <a class="float-shadow" href="Raspored_sati.aspx">Raspored sati</a>&nbsp;&nbsp;
           <a class="float-shadow" href="Menza_raspored.aspx">Menza(jelovnik)</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
           <a class="float-shadow" href="Vozni_red.aspx">Zet linije(Kampus)</a>&nbsp;&nbsp;&nbsp; &nbsp;
           <a class="float-shadow" href="#">Kalendar Fpz</a>&nbsp;&nbsp;&nbsp; &nbsp;
           <a class="float-shadow" href="#">O Stranici</a>
           <a class="float-shadow" href="#">Fpz Materjali</a>

      </div>
        <div style="width: 98px; z-index: 1; left: 256px; top: 96px; position: absolute; height: 16px" class="labels">
            Menu-Mesni
        </div>
       <div style="width: 98px; z-index: 1; left: 503px; top: 98px; position: absolute; height: 16px" class="labels">
            Menu-Veget
        </div>
        <div style="width: 98px; z-index: 1; left: 755px; top: 100px; position: absolute; height: 16px" class="labels">
            Glavno jelo
        </div>
        <div style="width: 98px; z-index: 1; left: 995px; top: 101px; position: absolute; height: 16px" class="labels">
            Prilozi
        </div>
        <div class="auto-style1" style="width: 756px; z-index: 1; left: 286px; top: 28px; position: absolute; height: 26px">FPZinfo</div>
         <div style="width: 112px">
       &nbsp;<img src = "Images/Actions-go-previous-icon.png"  style="z-index: 1; left: 62px; top: 41px; position: absolute" /></div>
    </form>
    <script>
        var datum_1 = new Date();
        var n = datum_1.getDay();
        if (n == 6 )
        {
            alert("Menza ne radi subotom ")
            window.location.assign("http://www.fpzmobile.somee.com");
        }
        if (n == 7)
        {
            window.location.assign("http://www.fpzmobile.somee.com");
            alert("Menza ne radi nedjeljom");
        }
    </script>
</body>
</html>

