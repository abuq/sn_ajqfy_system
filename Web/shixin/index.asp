<!-- #include file="head.asp" -->
<div class="mainpage">
	<div class="wrapper">
	<div class="shixintab gallery">
	<div class="title"><h2>失信被执行人查询</h2></div>
    <div class="title"><h6>(查询提示：在输入框输入姓名、身份证号、案号可快速定位，点击标题可重新排序。)</h6></div>
	<%Set rs=Server.CreateObject("Adodb.RecordSet")
	sql="select * from Sxr where UserState=1 order by ID"
	rs.Open sql,conn,1,1%>
	<table width="100%" class="display dTable" id="" cellspacing="0">
	<thead>
	<tr>
	<th>序号</th>
	<th>姓名/名称</th>
	<th>公民身份号码</th>
	<th>执行案号</th>
	</tr>
	</thead>
	<tbody>
	<%if not (rs.bof and rs.eof) then
	i=1   
	do while not rs.eof%>
	<tr class="gradeA">
	<td><%=i%></td>
	<td><%=rs("RealName")%></a></td>
	<td><%=rs("IDCard")%></td>
	<td><%=rs("CaseNum")%></td>
	</tr>
	<%i=i+1
	rs.MoveNext
	loop
	end if%>
	</tbody>
	</table>
	<%rs.close
	set rs=nothing%>
	</div>
	</div>
</div>
<!-- #include file="foot.asp" -->
