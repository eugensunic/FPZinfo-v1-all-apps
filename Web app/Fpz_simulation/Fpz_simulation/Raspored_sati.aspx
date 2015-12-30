<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Raspored_sati.aspx.cs" Inherits="Fpz_simulation.Raspored_sati" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" type="text/css" href="StyleSheet1.css"/>
    <style>
        * {
    margin: 0;
    padding: 0;
  	-moz-box-sizing: border-box;
		-o-box-sizing: border-box;
		-webkit-box-sizing: border-box;
		box-sizing: border-box;
	}

	body {
		width: 100%;
		height: 100%;
		font-family: "helvetica neue", helvetica, arial, sans-serif;
		font-size: 13px;
		text-align: center;
		background: #333 url('http://subtlepatterns.subtlepatterns.netdna-cdn.com/patterns/low_contrast_linen.png');
	}

	ul {
		margin: 30px auto;
		text-align: center;
	}

	li {
		list-style: none;
		position: relative;
		display: inline-block;
		width: 100px;
		height: 100px;
            top: 190px;
            left: -138px;
        }

	@-moz-keyframes rotate {
		0% {transform: rotate(0deg);}
		100% {transform: rotate(-360deg);}
	}

	@-webkit-keyframes rotate {
		0% {transform: rotate(0deg);}
		100% {transform: rotate(-360deg);}
	}


	

	@keyframes rotate {
		0% {transform: rotate(0deg);}
		100% {transform: rotate(-360deg);}
	}

	.round {
		display: block;
		position: absolute;
		left: -12px;
		top: 12px;
		width: 156%;
		height: 100%;
		padding-top: 30px;		
		text-decoration: none;		
		text-align: center;
		font-size: 25px;		
		text-shadow: 0 1px 0 rgba(255,255,255,.7);
		letter-spacing: -.065em;
		font-family: "Hammersmith One", sans-serif;		
		-webkit-transition: all .25s ease-in-out;
		-o-transition: all .25s ease-in-out;
		-moz-transition: all .25s ease-in-out;
		transition: all .25s ease-in-out;
		box-shadow: 2px 2px 7px rgba(0,0,0,.2);
		border-radius: 300px;
		z-index: 1;
		border-width: 4px;
		border-style: solid;
	}

	.round:hover {
		width: 130%;
		height: 130%;
		left: -15%;
		top: -15%;
		font-size: 33px;
		padding-top: 38px;
		-webkit-box-shadow: 5px 5px 10px rgba(0,0,0,.3);
		-o-box-shadow: 5px 5px 10px rgba(0,0,0,.3);
		-moz-box-shadow: 5px 5px 10px rgba(0,0,0,.3);
		box-shadow: 5px 5px 10px rgba(0,0,0,.3);
		z-index: 2;
		border-size: 10px;
		-webkit-transform: rotate(-360deg);
		-moz-transform: rotate(-360deg);
		-o-transform: rotate(-360deg);
		transform: rotate(-360deg);
	}

	a.red {
		background-color: rgba(239,57,50,1);
		color: rgba(133,32,28,1);
		border-color: rgba(133,32,28,.2);
	}

	a.red:hover {
		color: rgba(239,57,50,1);
	}

	a.green {
		background-color: rgba(1,151,171,1);
		color: rgba(0,63,71,1);
		border-color: rgba(0,63,71,.2);
	}

	a.green:hover {
		color: rgba(1,151,171,1);
	}

	a.yellow {
		background-color: rgba(252,227,1,1);
		color: rgba(153,38,0,1);
		border-color: rgba(153,38,0,.2);
	}

	a.yellow:hover {
		color: rgba(252,227,1,1);
	}

	.round span.round {
		display: block;
		opacity: 0;
		-webkit-transition: all .5s ease-in-out;
		-moz-transition: all .5s ease-in-out;
		-o-transition: all .5s ease-in-out;
		transition: all .5s ease-in-out;
		font-size: 1px;
		border: none;
		padding: 40% 20% 0 20%;
		color: #fff;
	}

	.round span:hover {
		opacity: .85;
		font-size: 16px;
		-webkit-text-shadow: 0 1px 1px rgba(0,0,0,.5);
		-moz-text-shadow: 0 1px 1px rgba(0,0,0,.5);
		-o-text-shadow: 0 1px 1px rgba(0,0,0,.5);
		text-shadow: 0 1px 1px rgba(0,0,0,.5);	
	}

	.green span {
		background: rgba(0,63,71,.7);		
	}

	.red span {
		background: rgba(133,32,28,.7);		
	}

	.yellow span {
		background: rgba(161,145,0,.7);	

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
    <div>
    <ul>
  <li><a href="#" class="round yellow">Promet,ITS,Logistika<span class="round">1.Godina</span></a></li>
  <li><a href="#" class="round red">Promet,ITS,Logistika<span class="round"> 2.Godina </span></a></li>
	<li><a href="#" class="round green">Aeronautika<span class="round">1 i 2.Godina</span></a></li>
</ul> 
    </div>
        <div style="height: 65px; z-index: 1; left: 318px; top: 494px; position: absolute; width: 729px" class="box">
           <a class="float-shadow" href="Default.aspx">Home</a>&nbsp;&nbsp;
           <a class="float-shadow" href="Dvorane.aspx">Slobodne ucionice</a>&nbsp;&nbsp;
           <a class="float-shadow" href="Raspored_sati.aspx">Raspored sati</a>&nbsp;&nbsp;
           <a class="float-shadow" href="Menza_raspored.aspx">Menza(jelovnik)</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
           <a class="float-shadow" href="Vozni_red.aspx">Zet linije(Kampus)</a>&nbsp;&nbsp;&nbsp; &nbsp;
           <a class="float-shadow" href="#">Kalendar Fpz</a>&nbsp;&nbsp;&nbsp; &nbsp;
           <a class="float-shadow" href="#">O Stranici</a>
           <a class="float-shadow" href="#">Fpz Materjali</a>
      </div>
        <div class="auto-style1" style="width: 756px; z-index: 1; left: 325px; top: 27px; position: absolute; height: 56px">FPZinfo</div>
         <div style="width: 112px">
       &nbsp;<img src = "Images/Actions-go-previous-icon.png"  style="z-index: 1; left: 62px; top: 41px; position: absolute" /></div>
    </form>
</body>
</html>
