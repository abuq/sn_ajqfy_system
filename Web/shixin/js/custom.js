$(function() {

/* Tables
================================================== */


	//===== Check all checbboxes =====//
	
	$(".titleIcon input:checkbox").click(function() {
		var checkedStatus = this.checked;
		$("#checkAll tbody tr td:first-child input:checkbox").each(function() {
			this.checked = checkedStatus;
				if (checkedStatus == this.checked) {
					$(this).closest('.checker > span').removeClass('checked');
				}
				if (this.checked) {
					$(this).closest('.checker > span').addClass('checked');
				}
		});
	});	
	
	$('#checkAll tbody tr td:first-child').next('td').css('border-left-color', '#CBCBCB');
	
	
	
	//===== Resizable columns =====//
	
	$("#res, #res1").colResizable({
		liveDrag:true,
		draggingClass:"dragging" 
	});
	  
	  
	  
	//===== Sortable columns =====//
	
	$("table").tablesorter();
	
	
	
	//===== Dynamic data table =====//
	
	oTable = $('.dTable').dataTable({
		"bJQueryUI": true,
		"sPaginationType": "full_numbers",
		//"sDom": '<""l>t<"F"fp>',
		"sDom": '<"">t<"F"fp>',	//去掉每页显示数选项
		"aLengthMenu" : [25, 50, 100], //更改显示记录数选项  
		"iDisplayLength" : 25, //默认显示的记录数

	});
	
/* UI stuff
================================================== */
	
	//===== Accordion =====//		
	
	$('div.menu_body:eq(0)').show();
	$('.acc .title:eq(0)').show().css({color:"#2B6893"});
	
	$(".acc .title").click(function() {	
		$(this).css({color:"#2B6893"}).next("div.menu_body").slideToggle(300).siblings("div.menu_body").slideUp("slow");
		$(this).siblings().css({color:"#404040"});
	});
	
	
	//===== Tabs =====//
		
	$.fn.contentTabs = function(){ 
	
		$(this).find(".tab_content").hide(); //Hide all content
		$(this).find("ul.tabs li:first").addClass("activeTab").show(); //Activate first tab
		$(this).find(".tab_content:first").show(); //Show first tab content
	
		$("ul.tabs li").click(function() {
			$(this).parent().parent().find("ul.tabs li").removeClass("activeTab"); //Remove any "active" class
			$(this).addClass("activeTab"); //Add "active" class to selected tab
			$(this).parent().parent().find(".tab_content").hide(); //Hide all tab content
			var activeTab = $(this).find("a").attr("href"); //Find the rel attribute value to identify the active tab + content
			$(activeTab).show(); //Fade in the active content
			return false;
		});
	
	};
	$("div[class^='widget']").contentTabs(); //Run function on any div with class name of "Content Tabs"

	
	//===== Lightbox =====//
	
	$("a[rel^='lightbox']").prettyPhoto();
	
	
	
	//===== Image gallery control buttons =====//
	
	$(".gallery ul li").hover(
		function() { $(this).children(".actions").show("fade", 200); },
		function() { $(this).children(".actions").hide("fade", 200); }
	);


});