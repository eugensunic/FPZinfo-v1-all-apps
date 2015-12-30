<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Vozni_red.aspx.cs" Inherits="Fpz_simulation.Vozni_red" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">

    <title></title>
    <link rel="stylesheet" type="text/css" href="StyleSheet1.css"/>
    <style type="text/css">
        body {
		width: 100%;
		height: 100%;
		font-family: "helvetica neue", helvetica, arial, sans-serif;
		font-size: 13px;
		text-align: center;
        background: #333 url('http://subtlepatterns.subtlepatterns.netdna-cdn.com/patterns/low_contrast_linen.png');

		
	}
        #form1 {
            height: 786px;
            width: 1251px;
        }
        progress[value] {
  width: 250px;
  height: 20px;

}
        progress { border-radius:5px;
        }
        .auto-style1 {
            border: 2px solid #a1a1a1;
            padding: 10px 30px;
            background: url('/Images/Android-Phone-Grey-Color-Background-HD.jpg');
            width: 300px;
            border-radius: 15px;
            box-shadow: 2px 2px 5px #888888;
            font-family: "Eras Medium ITC";
            font-size: large;
        }
    </style>
</head>
<body>

    <form id="form1" runat="server">
        <asp:Label ID="Label5" runat="server" style="z-index: 1; left: 270px; top: 132px; position: absolute; height: 16px;" Text="Label" CssClass="labelrezultat"></asp:Label>
        <asp:Label ID="Label6" runat="server" style="z-index: 1; left: 271px; top: 188px; position: absolute" Text="Label" CssClass="labelrezultat"></asp:Label>
        <asp:Label ID="Label7" runat="server" style="z-index: 1; left: 258px; top: 253px; position: absolute; margin-top: 0px; height: 19px; width: 75px;" Text="Nema" CssClass="labelrezultat" ></asp:Label>
        <asp:DropDownList ID="DropDownList2" runat="server" style="z-index: 1; left: 132px; top: 296px; position: absolute; right: 1116px; height: 32px; width: 80px; bottom: 381px;" OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged">
        </asp:DropDownList>
        <asp:DropDownList ID="DropDownList3" runat="server" style="z-index: 1; left: 313px; top: 295px; position: absolute; height: 31px; width: 80px; bottom: 366px;" OnSelectedIndexChanged="DropDownList3_SelectedIndexChanged">
        </asp:DropDownList>
        <asp:Label ID="Label8" runat="server" style="z-index: 1; left: -34px; top: 298px; position: absolute; height: 16px;" Text="Od:" CssClass="labels"></asp:Label>
        <asp:Label ID="Label9" runat="server" style="z-index: 1; left: 167px; top: 299px; position: absolute" Text="Do:" CssClass="labels"></asp:Label>
        <asp:Label ID="Label10" runat="server" style="z-index: 1; left: 22px; top: 476px; position: absolute; height: 19px; " Text="Linija:" CssClass="labels"></asp:Label>
        <asp:Label ID="Label11" runat="server" style="z-index: 1; left: 268px; top: 471px; position: absolute" Text="Odaberite liniju" CssClass="labelrezultat"></asp:Label>
        <asp:Button ID="Button1" runat="server" style="z-index: 1; left: 620px; top: 148px; position: absolute; width: 92px; height: 35px" Text="Button" OnClick="Button1_Click1" CssClass="Listbox"  />
        <asp:Label ID="Label13" runat="server" style="z-index: 1; left: 734px; top: 345px; position: absolute; height: 19px; right: 375px;" Text="2.autobus" CssClass="labels"></asp:Label>
        <asp:Label ID="Label14" runat="server" style="z-index: 1; left: 909px; top: 279px; position: absolute; height: 36px; width: 130px" Text="vrijeme polaska" CssClass="labels"></asp:Label>
        <asp:Label ID="Label15" runat="server" style="z-index: 1; left: 858px; position: absolute; top: 346px" Text="vrijeme polaska" CssClass="labels"></asp:Label>
        &nbsp;
        <div style="z-index: 1; left: 525px; top: 284px; position: absolute; height: 19px; width: 216px">
             <progress id="progress1" value="0" max="100" ></progress>  
        </div>
        <div style="z-index: 1; left: 525px; top: 346px; position: absolute; height: 19px; width: 220px">
             <progress id="progress2" value="0" max="200"></progress>  
        </div>
        <asp:Label ID="Label16" runat="server" style="z-index: 1; left: 438px; top: 285px; position: absolute; width: 42px; bottom: 224px;" Text="mm:ss" CssClass="labels"></asp:Label>
        <asp:Label ID="Label17" runat="server" style="z-index: 1; left: 440px; top: 345px; position: absolute; width: 40px;" Text="mm:ss" CssClass="labels"></asp:Label>
        <p id="percentage1"style="z-index: 1; left: 1067px; top: 268px; position: absolute; height: 28px; width: 116px; font-size: large; font-weight: 700;"></p>

        <div id="percentage2"style="z-index: 1; left: 638px; top: 347px; position: absolute; height: 28px; width: 116px; margin-left: 432px; font-size: large; font-weight: 700;"></div>
         
         <div style="width: 720px; position: absolute; top: 496px; left: 295px ; margin-top: 0px; z-index: 2; height: 64px;" class="box">
         
           <a class="float-shadow" href="Default.aspx">Home</a>&nbsp;&nbsp;
           <a class="float-shadow" href="Dvorane.aspx">Slobodne ucionice</a>&nbsp;&nbsp;
           <a class="float-shadow" href="Raspored_sati.aspx">Raspored sati</a>&nbsp;&nbsp;
           <a class="float-shadow" href="Menza_raspored.aspx">Menza(jelovnik)</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
           <a class="float-shadow" href="Vozni_red.aspx">Zet linije(Kampus)</a>&nbsp;&nbsp;&nbsp; &nbsp;
           <a class="float-shadow" href="#">Kalendar Fpz</a>&nbsp;&nbsp;&nbsp; &nbsp;
           <a class="float-shadow" href="#">O Stranici</a>
            <a class="float-shadow" href="#">Fpz Materjali</a>
     
       </div>
        <div class="auto-style1" style="width: 756px; z-index: 1; left: 273px; top: 24px; position: absolute; height: 26px"><strong>Fpz Mobile Simulation</strong></div>
        <asp:Label ID="Label18" runat="server" style="z-index: 1; left: 805px; top: 281px; position: absolute; width: 83px" Text="1.autobus" CssClass="labels"></asp:Label>
        <div class="labels" style="width: 134px; z-index: 1; left: 71px; top: 135px; position: absolute; height: 16px">Datum:</div>
        <div class="labels" style="height: 27px; width: 154px; z-index: 1; left: 99px; top: 189px; position: absolute">Trenutno vrijeme:</div>
        <div class="labels" style="width: 154px; z-index: 1; left: 67px; top: 248px; position: absolute; height: 16px">Obavijest:</div>
         <div style="width: 112px">
       &nbsp;<img src = "Images/Actions-go-previous-icon.png"  style="z-index: 1; left: 62px; top: 41px; position: absolute" /></div>
    </form>
     <script>
         var datum_1 = new Date();
         var n = datum_1.getDay();
         if (n == 6 || n == 7) {
             alert("Vikendom ne prometuju linije prema kampusu!")
             window.location.assign("http://www.fpzmobile.somee.com");
         }
         else {
             startTime();
             var date = new Date();
             var mjesec = date.getMonth() + 1;
             document.getElementById("Label5").innerText = date.getDate() + "/" + mjesec + "/" + date.getFullYear();

             var percentage1 = 0;
             var percentage2 = 0;

             var progress_value1 = 0;
             var progress_value2 = 0;

             var vrijeme1 = document.getElementById("Label16").innerText;
             var minute1 = vrijeme1.substring(0, 2);
             var sekunde1 = vrijeme1.substring(3, vrijeme1.length);
             var int_minute1 = parse_minutes(minute1);
             var int_sekunde1 = parse_seconds(sekunde1);

             var vrijeme2 = document.getElementById("Label17").innerText;
             var minute2 = vrijeme2.substring(0, 2);
             var sekunde2 = vrijeme2.substring(3, vrijeme2.length);
             var int_minute2 = parse_minutes(minute2);
             var int_sekunde2 = parse_seconds(sekunde2);


             document.getElementById("progress1").max = conversionTime(parse_minutes(minute1), parse_seconds(sekunde1));
             document.getElementById("progress2").max = conversionTime(parse_minutes(minute2), parse_seconds(sekunde2));


             if (vrijeme1 != "mm:ss") {
                 setInterval(prvoVrijeme, 1000);
                 setInterval(progressFunction1, 1000);



             }
             if (vrijeme2 != "mm:ss") {

                 setInterval(countDown2, 1000);
                 setInterval(progressFunction2, 1000);

             }
         }
         
             function conversionTime(minutes_int, seconds_int) { return (minutes_int * 60) + seconds_int; }

             function parse_minutes(minutes_string) {
                 return parseInt(minutes_string);
             }

             function parse_seconds(seconds_string) {
                 return parseInt(seconds_string);
             }

             function prvoVrijeme() {

                 int_sekunde1 = int_sekunde1 - 1
                 if (int_sekunde1 < 0) {
                     int_minute1 = int_minute1 - 1;
                     int_sekunde1 = 59;

                 }
                 if (int_minute1 < 0) {
                     document.getElementById("Label16").innerText = "Bus je krenuo!!!";
                 }
                 if (int_sekunde1 < 10) { document.getElementById("Label16").innerText = int_minute1 + ":0" + int_sekunde1; }
                 else { document.getElementById("Label16").innerText = int_minute1 + ":" + int_sekunde1; }

                 if (int_minute1 < 10) { document.getElementById("Label16").innerText = "0" + int_minute1 + ":" + int_sekunde1; }

                 if (int_minute1 < 10 && int_sekunde1 < 10) { document.getElementById("Label16").innerText = "0" + int_minute1 + ":0" + int_sekunde1; }

             }
             function countDown2() {

                 int_sekunde2 = int_sekunde2 - 1

                 if (int_sekunde2 < 0) {
                     int_minute2 = int_minute2 - 1;
                     int_sekunde2 = 59;

                 }
                 if (int_minute2 < 0) {
                     //clear the interval here.
                 }
                 if (int_sekunde2 < 10) { document.getElementById("Label17").innerText = int_minute2 + ":0" + int_sekunde2; }
                 else { document.getElementById("Label17").innerText = int_minute2 + ":" + int_sekunde2; }

                 if (int_minute2 < 10) { document.getElementById("Label17").innerText = "0" + int_minute2 + ":" + int_sekunde2; }
                 if (int_minute2 < 10 && int_sekunde2 < 10) { document.getElementById("Label16").innerText = "0" + int_minute2 + ":0" + int_sekunde2; }
             }

             function progressFunction1() {


                 progress_value1 = progress_value1 + 1;
                 percentage1 = (progress_value1 / (conversionTime(parse_minutes(minute1), parse_seconds(sekunde1)))) * 100;
                 document.getElementById("progress1").value = progress_value1;

                 document.getElementById("percentage1").innerText = parseInt(percentage1) + "%";

             }

             function progressFunction2() {

                 progress_value2 = progress_value2 + 1;
                 percentage2 = (progress_value2 / (conversionTime(parse_minutes(minute2), parse_seconds(sekunde2)))) * 100;
                 document.getElementById("progress2").value = progress_value2;
                 document.getElementById("percentage2").innerText = parseInt(percentage2) + "%";
             }

             function startTime() {
                 var today = new Date();
                 var h = today.getHours();
                 var m = today.getMinutes();
                 var s = today.getSeconds();
                 m = checkTime(m);
                 s = checkTime(s);
                 document.getElementById('Label6').innerHTML = h + ":" + m + ":" + s;
                 var t = setTimeout(function () { startTime() }, 500);
             }

             function checkTime(i) {
                 if (i < 10) { i = "0" + i };
                 return i;
             }
         
         

      </script>
</body>
</html>
