<!-- #include file="Head.asp" -->
<div class="pagebg">
	<div class="fan"></div>
	<div class="tjinter">
		<div class="row">
        <%
		set rs=Server.CreateObject("Adodb.Recordset")
		sql="select * from Tj_Room where RoomState=0 order by RoomOrder"
		rs.open sql,conn,1,1
		if rs.eof and rs.bof then%>
        	<a href="#" class="btn tjbtn" target="_self">暂无调解室添加</a>
        <%else
			do while not rs.eof%>
			<a href="showpage.asp?RoomOrder=<%=rs("RoomOrder")%>" class="btn tjbtn" target="_self"><%=rs("RoomName")%></a>
        	<%rs.movenext
			loop
		end if
		rs.close
		set rs=nothing%>
		</div>
	</div>
</div>
<!-- #include file="foot.asp" -->
