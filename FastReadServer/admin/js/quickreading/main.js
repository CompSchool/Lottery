$(function(){
	$(".leftsidebar_box dd").hide();
	$(".leftsidebar_box dt").click(function(){
		$(".leftsidebar_box dt").css({"background-color":"#3992d0"})
		$(this).css({"background-color": "#317eb4"});
		$(this).find('dd').removeClass("menu_chioce");
		$(".leftsidebar_box dt img").attr("src","images/main/select_xl01.png");
		$(this).find('img').attr("src","images/main/select_xl.png");
		$(".menu_chioce").slideUp();
		$(this).parent().find('dd').slideToggle();
		$(this).parent().find('dd').addClass("menu_chioce");
	});
});