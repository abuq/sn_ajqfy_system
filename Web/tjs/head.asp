<!DOCTYPE html>
<!--#include file="../incfiles/SysProduct.asp" -->
<!--#include file="../incfiles/Title.asp" --><head>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1, user-scalable=no">
    <meta name="description" content="">
    <meta name="Copyright" content="宜联科技" />
    <meta name="author" content="">
	
    <%if instr(url,"showpage.asp")>0 then%>
	<meta http-equiv="refresh" content="20">
	<%End if%>
    
    <link rel="shortcut icon" href="../images/favicon/favicon.png" type="image/x-icon">
    <link rel="icon" href="../images/favicon/favicon.png" type="image/x-icon">

    <title><%=Title%></title>
    
    <!--Library Styles-->
    <link href="../css/normalize.css" rel="stylesheet" type="text/css" />  
    <link href="../css/bootstrap.min.css" rel="stylesheet">
    <link href="../css/tips.css" rel="stylesheet">
    <link href="css/tjs.css" rel="stylesheet">
	<!--[if lt IE 9]>
      <script src="../js/html5shiv.js"></script>
      <script src="../js/respond.min.js"></script>
    <![endif]-->
    <!-- Library Scripts -->
	<script type="text/javascript" src="../js/jquery-2.0.0.min.js"></script>
	<script type="text/javascript" src="../js/bootstrap.min.js"></script>
    <script type="text/javascript" src="js/custom.js"></script>
</head>
<body>
<div class="headbox">
	<div class="header">
		<div class="fyname row text-left vertical-middle-sm">
			<div class="col-sm-8 text-left">
				<h1><%=Company%></h1>
				<p class="lead">诉讼服务中心——温馨调解室</p>
			</div>
		</div>
	</div>
</div>