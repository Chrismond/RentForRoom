/*
Author       : Dreamguys
Template Name: Dreams-Estate - Bootstrap Template
Version      : 1.0
*/

(function($) {
    "use strict";
	
	var $slimScrolls = $('.slimscroll');	
	var $wrapper = $('.main-wrapper');

	if($(window).width() <= 991) {
		var Sidemenu = function() {
			this.$menuItem = $('.main-nav a');
		};

		function init() {
			var $this = Sidemenu;
			$('.main-nav a').on('click', function(e) {
				if($(this).parent().hasClass('has-submenu')) {
					e.preventDefault();
				}
				if(!$(this).hasClass('submenu')) {
					$('ul', $(this).parents('ul:first')).slideUp(350);
					$('a', $(this).parents('ul:first')).removeClass('submenu');
					$(this).next('ul').slideDown(350);
					$(this).addClass('submenu');
				} else if($(this).hasClass('submenu')) {
					$(this).removeClass('submenu');
					$(this).next('ul').slideUp(350);
				}
			});
		}

	// Sidebar Initiate
	
	init();
	}

	// Mobile menu sidebar overlay
	
	$('body').append('<div class="sidebar-overlay"></div>');
	$(document).on('click', '#mobile_btn', function() {
		$('main-wrapper').toggleClass('slide-nav');
		$('.sidebar-overlay').toggleClass('opened');
		$('html').addClass('menu-opened');
		return false;
	});	
	
	$(document).on('click', '.sidebar-overlay', function() {
		$('html').removeClass('menu-opened');
		$(this).removeClass('opened');
		$('main-wrapper').removeClass('slide-nav');
	});
	
	$(document).on('click', '#menu_close', function() {
		$('html').removeClass('menu-opened');
		$('.sidebar-overlay').removeClass('opened');
		$('main-wrapper').removeClass('slide-nav');
	});

	// Header Fixed
	
	if($('.header.header-fix').length > 0) {
		  $(window).on('scroll', function(){
			var scroll = $(window).scrollTop();
			  if (scroll > 115) {
				$(".header.header-fix").css("position" , "relative");				
				$('.header.header-fix').addClass('nav-fixed');
			  }

			  else{
				$(".header.header-fix").css("position" , "sticky"); 			
				$('.header.header-fix').removeClass('nav-fixed');
			  }
		  })
	}

	// theiaStickySidebar

	if ($(window).width() > 767) {
		if($('.theiaStickySidebar').length > 0) {
			$('.theiaStickySidebar').theiaStickySidebar({
			  // Settings
			  additionalMarginTop: 100
			});
		}
	}

	// Payment

	$(document).on('click', '.payment-icon  li', function() {
		$(".payment-icon  li").removeClass("active");
		$(this).addClass("active");   
	});


	
	if($('.datetimepicker').length > 0) {
		$('.datetimepicker').datetimepicker({
			format: 'DD/MM/YYYY',
			icons: {
				up: "fas fa-chevron-up",
				down: "fas fa-chevron-down",
				next: 'fas fa-chevron-right',
				previous: 'fas fa-chevron-left'
			}
		});
	}
	
	// Select Favourite
	
	if($('.favourite').length > 0) {
		$('.favourite').on('click', function() {
			$(this).toggleClass('selected');
		});
	}

	//scroll header
	
	$(window).on('scroll',function(){
		if ($(this).scrollTop()>150){
			$('.map-right').addClass('map-top');
		} else {
			$('.map-right').removeClass('map-top');
		}
	});

	// Banner Img Slider

	if($('.banner-img-slider').length > 0) {
		$('.banner-img-slider').owlCarousel({
			loop:true,
			margin:24,
			nav:false,
			dots:true,
			smartSpeed: 2000,
			responsive:{
				0:{
					items:1
				},
				
				550:{
					items:1
				},		
				992:{
					items:1
				},
				1200:{
					items:1
				},
			}
		})
	}

	// Property Type Slider

	if($('.property-type-slider').length > 0) {
		$('.property-type-slider').owlCarousel({
			loop:false,
			margin:24,
			nav:true,
			dots:false,
			smartSpeed: 2000,
			navText : ["<i class='fa-solid fa-arrow-left'></i>","<i class='fa-solid fa-arrow-right'></i>"],
			navContainer: '.mynav1',
			responsive:{
				0:{
					items:1
				},
				
				550:{
					items:2
				},		
				992:{
					items:3
				},
				1200:{
					items:4
				},
			}
		})
	}

	// City Slider

	if($('.city-card-slider').length > 0) {
		$('.city-card-slider').owlCarousel({
			loop:true,
			margin:24,
			nav:false,
			dots:true,
			smartSpeed: 2000,
			responsive:{
				0:{
					items:3
				},
				
				550:{
					items:2
				},		
				992:{
					items:2
				},
				1200:{
					items:6
				},
			}
		})
	}

	
	if($('.city-card-sliders').length > 0) {
		$('.city-card-sliders').owlCarousel({
			loop:true,
			margin:24,
			nav:false,
			dots:true,
			smartSpeed: 2000,
			responsive:{
				0:{
					items:1
				},
				
				550:{
					items:2
				},		
				992:{
					items:2
				},
				1200:{
					items:4
				},
			}
		})
	}

// Testimonial Slider

if($('.testimonial-slider').length > 0) {
	$('.testimonial-slider').owlCarousel({
		loop:true,
		margin:24,
		nav:true,
		dots:false,
		smartSpeed: 2000,
		navText : ["<i class='fa-solid fa-arrow-left'></i>","<i class='fa-solid fa-arrow-right'></i>"],
		responsive:{
			0:{
				items:1
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
	})
}



	// Rental Slider
	if ($('.rental-slider').length > 0) {
		$('.rental-slider').slick({
			slidesToShow: 1,
			slidesToScroll: 1,
			arrows: true,
			fade: true,
			asNavFor: '.slider-nav-thumbnails'
		});		
		$('.rental-slider').css('visibility', 'visible');
	}

	if ($('.slider-nav-thumbnails').length > 0) {
		$('.slider-nav-thumbnails').slick({
			slidesToShow: 4,
			slidesToScroll: 1,
			asNavFor: '.rental-slider',
			dots: false,
			arrows: false,
			centerMode: false,
			focusOnSelect: true

		});
	}
	// Parners Slider

	if($('.partners-slider').length > 0) {
		$('.partners-slider').owlCarousel({
			loop:true,
			autoplay:true,
			margin:24,
			nav:false,
			dots:false,
			smartSpeed: 2000,
			responsive:{
				0:{
					items:2
				},
				
				550:{
					items:3
				},		
				992:{
					items:4
				},
				1200:{
					items:6
				},
			}
		})
	}

	// Sidebar Image Slider

	if($('.sidebar-img-slider').length > 0) {
		$('.sidebar-img-slider').owlCarousel({
			loop:true,
			margin:24,
			nav:true,
			dots:false,
			smartSpeed: 2000,
			navText : ["<i class='fa-solid fa-arrow-left'></i>","<i class='fa-solid fa-arrow-right'></i>"],
			responsive:{
				0:{
					items:1
				},		
				992:{
					items:1
				},
				1200:{
					items:1
				},
			}
		})
	}

	// Slider Range JS

	if ($("#slider-range").length) {
		$(".noUi-handle").on("click", function () {
			$(this).width(50);
		});
		var rangeSlider = document.getElementById("slider-range");
		var moneyFormat = wNumb({
			decimals: 0,
			thousand: ",",
			prefix: "$"
		});
		noUiSlider.create(rangeSlider, {
			start: [0, 1000],
			step: 1,
			range: {
				min: [0],
				max: [2000]
			},
			format: moneyFormat,
			connect: true
		});

		// Set visual min and max values and also update value hidden form inputs

		rangeSlider.noUiSlider.on("update", function (values, handle) {
			document.getElementById("slider-range-value1").innerHTML = values[0];
			document.getElementById("slider-range-value2").innerHTML = values[1];
			document.getElementsByName("min-value").value = moneyFormat.from(values[0]);
			document.getElementsByName("max-value").value = moneyFormat.from(values[1]);
		});
	}

	//Increment Decrement Numberes
    $(".quantity-btn").on("click", function () {

        var $button = $(this);
        var oldValue = $button.closest('.product-quantity').find("input.quntity-input").val();
        if ($button.text() === "+") {
            var newVal = parseFloat(oldValue) + 1;
        } else {
            if (oldValue > 0) {
                var newVal = parseFloat(oldValue) - 1;
            } else {
                newVal = 0;
            }
        }
        $button.closest('.product-quantity').find("input.quntity-input").val(newVal);
    });

	// JQuery CounterUp

	if($('.dash-count .counter-up').length > 0) {
		$('.dash-count .counter-up').counterUp({
			delay: 15,
			time: 1500
		});
	}

	// Search
	
	$('.searchbar a').on('click', function(){
		$('.search-popup').addClass('visible');
		$('body').addClass('search-visible');
	});
	$('#search-close').on('click', function(){
		$('.search-popup').removeClass('visible');
		$('body').removeClass('search-visible');
	});

	// Toggle Password

	if($('.toggle-password').length > 0) {
		$(document).on('click', '.toggle-password', function() {
			$(this).toggleClass("feather-eye-off");
			var input = $(".pass-input");
			if (input.attr("type") === "password") {
				input.attr("type", "text");
			} else {
				input.attr("type", "password");
			}
		});
	}

	//   View all Show hide One

	if($('.viewall-One').length > 0) {
		$(".viewall-One").hide();
			$(".viewall-button-One").on('click', function () {
			$(this).text($(this).text() === "Close All" ? "View All" : "Close All");
			$(".viewall-One").slideToggle(900);
		});
	}

	//   View all Show hide Two

	if($('.viewall-Two').length > 0) {
		$(".viewall-Two").hide();
		$(".viewall-button-Two").on('click', function () {
			$(this).text($(this).text() === "Close All" ? "View All" : "Close All");
			$(".viewall-Two").slideToggle(900);
		});
	} 

	// Tabs

	$(".property-tabs ul li a").on('click', function () {
		$(".property-tabs ul li a").removeClass("active");
		$(this).addClass("active");
	});

	// Smooth Scroll
    
    $("ul.prop-tab li a").on('click', function(e) {
		$("ul.prop-tab li a").removeClass("active");
		$(this).addClass("active");
        e.preventDefault();
        var target = $(this).attr('href');
        $('html, body').animate({
            scrollTop: ($(target).offset().top - 90)
        }, 500);
    });

	// Add More
	
    $(".add-info").on('click','.trash', function () {
		$(this).closest('.add-cont').remove();
		return false;
    });


	// Category More & Less

	$('ul.term-list').each(function(){
		var len = $(this).find('li').length;
		if( len > 5){    
		  $('li', this).eq(4).nextAll().hide().addClass('toggleable');
		  $(this).append('<li class="more"><a href="javascript:void(0);"><i class="bx bx-plus-circle"></i> More Categories</a></li>');    
		}  
	});
	$('ul.term-list').on('click','.more', function(){
		if( $(this).hasClass('less') ){    
			$(this).text('').removeClass('less').append('<a href="javascript:void(0);"><i class="bx bx-plus-circle"></i> More Categories<a>');    
		}
		else{
			$(this).text('').addClass('less').append('<a href="javascript:void(0);"><i class="bx bx-minus-circle"></i> Less Categories<a>'); 
		}
		$(this).siblings('li.toggleable').slideToggle(); 
	}); 

	// Category More & Less

	$('ul.amene-list').each(function(){
		var len = $(this).find('li').length;
		if( len > 5){    
			$('li', this).eq(4).nextAll().hide().addClass('toggleable');
			$(this).append('<li class="more"><a href="javascript:void(0);"><i class="bx bx-plus-circle"></i> More Amenities</a></li>');    
		}  
	});

	$('ul.amene-list').on('click','.more', function(){
		if( $(this).hasClass('less') ){    
			$(this).text('').removeClass('less').append('<a href="javascript:void(0);"><i class="bx bx-plus-circle"></i> More Amenities</a>');    
		}else{
			$(this).text('').addClass('less').append('<a href="javascript:void(0);"><i class="bx bx-minus-circle"></i> Less Amenities</a>'); 
		}
		$(this).siblings('li.toggleable').slideToggle(); 
	}); 

	
	// Rent Slider
	if($('.rentfeature-slider').length > 0) {
		$('.rentfeature-slider').owlCarousel({
			loop:true,
			margin:24,
			nav:false,
			dots:true,
			smartSpeed: 2000,
			navText : ["<i class='fa-solid fa-arrow-left'></i>","<i class='fa-solid fa-arrow-right'></i>"],
			responsive:{
				0:{
					items:1
				},
				
				768:{
					items:1
				},		
				992:{
					items:2 
				},
				1200:{
					items:4
				},
			}
		})
	}
	// Modal

	// Scroll to Top
	
	if($('.progress-wrap path').length > 0) {
		$(function(s){
			var e=document.querySelector(".progress-wrap path"),
			t=e.getTotalLength();e.style.transition=e.style.WebkitTransition="none",
			e.style.strokeDasharray=t+" "+t,e.style.strokeDashoffset=t,e.getBoundingClientRect(),
			e.style.transition=e.style.WebkitTransition="stroke-dashoffset 10ms linear";
			var o=function(){var o=s(window).scrollTop(),
			r=s(document).height()-s(window).height(),
			i=t-o*t/r;e.style.strokeDashoffset=i};o(),
			s(window).scroll(o);
			jQuery(window).on("scroll",function(){
				jQuery(this).scrollTop()>50?jQuery(".progress-wrap").addClass("active-progress"):jQuery(".progress-wrap").removeClass("active-progress")
			}),
			jQuery(".progress-wrap").on("click",function(s){return s.preventDefault(),jQuery("html, body").animate({scrollTop:0},550),!1})
		});
	}

	// Loade
	// Fancy Box	
	
	if($('[data-fancybox]').length > 0) {
		Fancybox.bind('[data-fancybox]', {
		}); 
	}
	
})(jQuery);
