jQuery(function($) {
  "use strict";
	
	/* Mobile Menu */
	jQuery(".nav.navbar-nav li a").on("click", function() { 
		jQuery(this).parent("li").find(".utf_dropdown_menu").slideToggle();
		jQuery(this).find("li i").toggleClass("fa-angle-down fa-angle-up");
	});

	$('.nav-tabs[data-toggle="tab-hover"] > li > a').hover( function(){
    	$(this).tab('show');
	});
	
	/* Site search */
	$('.utf_nav_search').on('click', function () {
		$('.utf_search_block').fadeIn(350);
    });

	$('.utf_search_close').on('click', function(){
		$('.utf_search_block').fadeOut(350);
	});

	$('.navbar-nav .menu-dropdown').on('click', function (event) {
		event.preventDefault();
		event.stopPropagation();
		$(this).siblings().slideToggle();
	});
	
	$('.nav-tabs[data-toggle="tab-hover"] > li > a').hover( function(){
    	$(this).tab('show');
	});

	/*Fixed Header **/
	$(window).on('scroll', function () {
		if ($(window).scrollTop() > 250) {
		   $('.utf_sticky').addClass('sticky fade_down_effect');
		} else {
		   $('.utf_sticky').removeClass('sticky fade_down_effect');
		}
	});	
	
  	/* Owl Carousel */
	
  	//Trending Slide
  	$(".trending-slide").owlCarousel({
		loop:true,
		animateIn: 'fadeIn',
		autoplay:true,
		autoplayTimeout:3000,
		autoplayHoverPause:true,
		nav:true,
		margin:30,
		dots:false,
		mouseDrag:false,
		slideSpeed:500,
		navText: ["<i class='fa fa-angle-left'></i>", "<i class='fa fa-angle-right'></i>"],
		items : 1,
		responsive:{
			0: {
				items:1,
			},
			600: {
				items:1,
			}			
		}
	});

  	//Utf Featured Slide
	$(".utf_featured_slider").owlCarousel({
		loop:true,
		animateOut: 'fadeOut',
		autoplay:false,
		autoplayHoverPause:true,
		nav:true,
		margin:0,
		dots:false,
		mouseDrag:true,
		touchDrag:true,
		slideSpeed:500,
		navText: ["<i class='fa fa-angle-left'></i>", "<i class='fa fa-angle-right'></i>"],
		items : 1,
		responsive:{
		  0:{
				items:1
		  },
		  600:{
				items:1
		  }
		}
	});
	
	//Utf Latest News Slide
	$(".utf_latest_news_slide").owlCarousel({
		loop:false,
		animateIn: 'fadeInLeft',
		autoplay:false,
		autoplayHoverPause:true,
		nav:true,
		margin:30,
		dots:false,
		mouseDrag:false,
		slideSpeed:500,
		navText: ["<i class='fa fa-angle-left'></i>", "<i class='fa fa-angle-right'></i>"],
		items : 3,
		responsive:{
		  0:{
				items:1
		  },
		  600:{
			    items:2
		  },
		  768:{
			    items:2
		  },
		  992:{
			    items:3
		  },
		  1200:{
			    items:4
		  },
		}
	});
	
	//Utf Latest News Slide2
	$(".utf_latest_news_slide2").owlCarousel({
		loop:false,
		animateIn: 'fadeInLeft',
		autoplay:false,
		autoplayHoverPause:true,
		nav:true,
		margin:30,
		dots:false,
		mouseDrag:false,
		slideSpeed:500,
		navText: ["<i class='fa fa-angle-left'></i>", "<i class='fa fa-angle-right'></i>"],
		items : 3,
		responsive:{
		  0:{
				items:1
		  },
		  600:{
			    items:2
		  },
		  768:{
			    items:2
		  },
		  992:{
			    items:3
		  },
		  1200:{
			    items:3
		  },
		}
	});

	//Utf Latest More News Slide
	$(".utf_more_news_slide").owlCarousel({
		loop:false,
		autoplay:false,
		autoplayHoverPause:true,
		nav:false,
		margin:30,
		dots:true,
		mouseDrag:false,
		slideSpeed:500,
		navText: ["<i class='fa fa-angle-left'></i>", "<i class='fa fa-angle-right'></i>"],
		items : 1,
		responsive:{
		  0:{
				items:1
		  },
		  600:{
				items:1
		  }
		}
	});

	//Utf Post Slide	
	$(".utf_post_slide").owlCarousel({
		loop:true,
		animateOut: 'fadeOut',
		autoplay:false,
		autoplayHoverPause:true,
		nav:true,
		margin:30,
		dots:false,
		mouseDrag:false,
		slideSpeed:500,
		navText: ["<i class='fa fa-angle-left'></i>", "<i class='fa fa-angle-right'></i>"],
		items : 1,
		responsive:{
		  0:{
				items:1
		  },
		  600:{
				items:1
		  }
		}
	});

	/* Popup */
	$(document).ready(function(){
		$(".gallery-popup").colorbox({rel:'gallery-popup', transition:"fade", innerHeight:"500"});
		$(".popup").colorbox({iframe:true, innerWidth:600, innerHeight:400});
	});
	
	/* Back to top */
	$(window).scroll(function () {
		if ($(this).scrollTop() > 50) {
			 $('#back-to-top').fadeIn();
		} else {
			 $('#back-to-top').fadeOut();
		}
	});
	
	// scroll body to 0px on click
	$('#back-to-top').on('click', function () {
		 $('#back-to-top').tooltip('hide');
		 $('body,html').animate({
			  scrollTop: 0
		 }, 800);
		 return false;
	});
	$('#back-to-top').tooltip('hide');	
});