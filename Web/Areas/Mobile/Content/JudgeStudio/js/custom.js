$(function() {
	//禁止右键
	$(document).bind("contextmenu",function(){return false;});
	//禁止文本选择
	$(document).bind("selectstart",function(){return false;});
	
	//模态框垂直居中
	function centerModals(){
    	$('.modal').each(function(i){
        	var $clone = $(this).clone().css('display', 'block').appendTo('body');
			var top = Math.round(($clone.height() - $clone.find('.modal-content').height()) / 2);
			top = top > 0 ? top : 0;
			$clone.remove();
			$(this).find('.modal-content').css("margin-top", top);
		});
	}
	$('.modal').on('show.bs.modal', centerModals);
	$(window).on('resize', centerModals);

	$(".voice_2 ul li").each(function(){
		var fold = $(this).find(".fold");
		var unfold = $(this).find(".unfold");
		if(fold.is(":hidden")){
			$(this).width(780);
		}else{
			$(this).width(100);
		}
	})
	$(".voice_2 ul li").click(function(){
		var li_index = $(this).index();
		$(this).animate({width:780},200);
		$(this).find(".unfold").show();
		$(this).find(".fold").hide();
		$(this).siblings().animate({width:100},200);
		$(this).siblings().find(".unfold").hide();
		$(this).siblings().find(".fold").show();
	})
	//播放背景音乐
	$(document).on("click",".playmusic",function(){
		var playsrc = $(this).attr('data-toggle');
		var player = document.getElementById("player");
		var oldsrc=$("#player").attr("src");
		if (oldsrc==playsrc){
			if (player.paused){
				player.play();
			}
			else{
				player.pause();
			}
		}
		else{
			$("#player").attr("src",playsrc);
			player.play();
		}
	});
});