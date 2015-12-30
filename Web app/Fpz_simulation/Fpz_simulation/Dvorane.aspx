<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dvorane.aspx.cs" Inherits="Fpz_simulation.Dvorane" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
      <link rel="stylesheet" type="text/css" href="StyleSheet1.css"/>
    <style type="text/css">
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
        .ddv {
            border: 2px solid #a1a1a1;
            padding: 10px 40px;
            background: #dddddd;
            width: 838px;
            border-radius: 25px;
            z-index: 2;
        }
       
        .lista
       {

box-shadow: 10px 10px 5px #888888;
border-radius:25px;
       }

        .auto-style1 {
            font-size: large;
            font-family: "Eras Medium ITC";
        }

    </style>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="height: 504px; width: 1261px">
        <asp:ListBox ID="ListBox3" runat="server" style="z-index: 1; left: 699px; top: 105px; position: absolute; height: 232px; width: 140px" Width="40px" BackColor="#999999" CssClass="TextBox1"></asp:ListBox>
        <asp:ListBox ID="ListBox4" runat="server" style="position: absolute; z-index: 1; left: 917px; top: 104px; height: 230px; width: 144px" BackColor="#999999" CssClass="TextBox1"></asp:ListBox>
        <asp:ListBox ID="ListBox1" runat="server" style="position: relative; top: 80px; left: -255px; height: 236px; width: 178px; margin-top: 6px" BackColor="#999999" CssClass="TextBox1"></asp:ListBox>
        <asp:Label ID="Label4" runat="server" style="z-index: 1; left: 900px; top: 343px; position: absolute" Text="Vukelićeva" CssClass="labels"></asp:Label>
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" style="position: absolute; z-index: 1; left: 582px; top: 387px; height: 43px; width: 113px; right: 299px;" Text="Button" BackColor="#669999" BorderColor="#6666FF" BorderStyle="Ridge" CssClass="TextBox1" />
        <asp:ListBox ID="ListBox2" runat="server" style="z-index: 1; left: -221px; top: 80px; position: relative; width: 184px; height: 233px; right: 37px;" BackColor="#999999" CssClass="TextBox1"></asp:ListBox>
    </div>
      
        <div style="height: 65px; z-index: 1; left: 236px; top: -37px; position: relative; width: 719px" class="box">
           <a class="float-shadow" href="Default.aspx">Home</a>&nbsp;&nbsp;
           <a class="float-shadow" href="Dvorane.aspx">Slobodne ucionice</a>&nbsp;&nbsp;
           <a class="float-shadow" href="Raspored_sati.aspx">Raspored sati</a>&nbsp;&nbsp;
           <a class="float-shadow" href="Menza_raspored.aspx">Menza(jelovnik)</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
           <a class="float-shadow" href="Vozni_red.aspx">Zet linije(Kampus)</a>&nbsp;&nbsp;&nbsp; &nbsp;
           <a class="float-shadow" href="#">Kalendar Fpz</a>&nbsp;&nbsp;&nbsp; &nbsp;
           <a class="float-shadow" href="#">O Stranici</a>
           <a class="float-shadow" href="#">Fpz Materjali</a>
      </div>
        <div style="width: 625px">

        <div style="width: 756px; z-index: 1; left: 242px; top: 29px; position: absolute; height: 26px; text-align: center" class="header">
            &nbsp;&nbsp;<strong><span class="auto-style1">&nbsp;
            FPZinfo</span></strong></div>

      </div>
        <asp:Label ID="Label1" runat="server" Text="Objekt 69" style="position: absolute; z-index: 1; left: 242px; top: 344px; width: 86px; height: 17px;" CssClass="labels"></asp:Label>
        <asp:Label ID="Label2" runat="server" Text="Objekt 70" style="position: absolute; z-index: 1; left: 430px; top: 343px; height: 17px;" CssClass="labels"></asp:Label>
        <asp:Label ID="Label3" runat="server" Text="Objekt 71" style="z-index: 1; left: 739px; top: 342px; position: absolute; height: 23px; width: 101px" CssClass="labels"></asp:Label>
   <div style="width: 112px">
       &nbsp;<img src = "Images/Actions-go-previous-icon.png"  style="z-index: 1; left: 62px; top: 41px; position: absolute" /></div>
        &nbsp;</form>
    <script>    
        var datum_1 = new Date();
        var n = datum_1.getDay();
        if (n == 6 || n == 7)
        {
            alert("Fakultet je Zatvoren")
            window.location.assign("http://www.fpzmobile.somee.com");
        }
    </script>
</body>
</html>

