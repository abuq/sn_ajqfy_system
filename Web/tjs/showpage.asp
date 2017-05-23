<!-- #include file="Head.asp" -->
<%
dim RoomOrder
RoomOrder=request.QueryString("RoomOrder")
set rs=Server.CreateObject("Adodb.Recordset")
%>
<body data-spy="scroll" data-target="#myScrollspy">
<div class="pagebg">
	<div class="page">
    <%if RoomOrder<>"" then
		if not IsNumeric (RoomOrder) then
			response.Write "非法参数！"
		else
			sql="select RoomName from Tj_Room where RoomOrder='"&RoomOrder&"'"
			RoomName=conn.execute(sql)(0)
			sql="select Tj_Room.RoomName,[User].RealName,[User].UserPic,[User].UserIntroduce,TjCase.sCaseName,TjCase.sCaseIntroduce,TjCase.dAclEndTime,TjCase.dPreStaTime,TjCase.dPreEndTime from Tj_Room,TjCase,[User] where TjCase.RoomOrder='"&RoomOrder&"' and [User].ID=TjCase.iUserId and Tj_Room.ID=TjCase.iTjRoomId and getdate()>=TjCase.dPreStaTime and TjCase.dAclEndTime is null order by TjCase.dPreStaTime"
			rs.open sql,conn,1,1%>
			<div class="row">
            <%if rs.eof and rs.bof then%>
				<div class="panel panel-primary divbgtransparent_white7 tjsbox">
					<div class="panel-heading">
						<h1 class="panel-title"><%=RoomName%><small>（空闲中……）</small></h1>
					</div>
					<div class="panel-body tjfree<%=RoomOrder%>">
						<h2>握手言和情谊深，抬头不见低头见。</h2>
					</div>
				</div>
            <%else%>
				<div class="panel panel-primary divbgtransparent_white8 tjsbox">
                	<div class="panel-heading">
                    	<h1 class="panel-title"><%=RoomName%><small>（正在调解——预计调解时间：<%=formatDate(rs("dPreStaTime"),4)%>至<%=formatDate(rs("dPreEndTime"),4)%>）</small></h1>
                    </div>
					<div class="panel-body text-left tjing<%=RoomOrder%>">
                    	<div class="page-header">
                        	<h2>调解员：<%=rs("RealName")%></h2>
                        </div>
                    	<div class="thumbnail">
                    		<img src="../<%=rs("UserPic")%>" alt="">
                        	<div class="caption">
                        		<p><%=rs("UserIntroduce")%></p>
                        	</div>
                    	</div>
                        <div class="page-header">
                        	<h2>调解事项</h2>
                        </div>
                        <div class="thumbnail">
                        	<div class="caption">
                            	<p><%=rs("sCaseIntroduce")%></p>
                            </div>
                        </div>
                    </div>
                </div>
			<%end if%>
			</div>
		<%end if%>
	
	<%else
		response.Write("缺少参数！")
	end if%>
	</div>
</div>
<!-- #include file="foot.asp" -->