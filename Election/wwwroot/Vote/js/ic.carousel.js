function homeSlider(){
	
	// main-silder-swiper
	if(jQuery('.main-silder-swiper').length > 0){
		var swiper = new Swiper('.main-silder-swiper', {
			speed: 1500,
			parallax: true,
			loop:true,
			autoplay: {
			   delay: 3000,
			},
			navigation: {
				nextEl: '.swiper-button-next1',
				prevEl: '.swiper-button-prev1',
			},
		});
	}
}


/* JavaScript Document */
jQuery(document).ready(function() {
    'use strict';
	
	setTimeout(function(){
		homeSlider();	
	}, 300);
	
	
	/* All Testimonial Swiper Start ========== 
	==================================*/
	
	// Testimonial Swiper 1
	if(jQuery('.testimonial-swiper1').length > 0){
		var testswiper1 = new Swiper('.testimonial-swiper1', {
			speed: 1500,
			slidesPerView: 1,
			spaceBetween: 30,
			autoplay: {
			   delay: 3000,
			},
		});
	}
	
	// Testimonial Swiper 2
	if(jQuery('.testimonial-swiper2').length > 0){
		var testswiper2 = new Swiper('.testimonial-swiper2', {
			speed: 1500,
			slidesPerView: 2,
			spaceBetween: 0,
			loop:true,
			autoplay: {
			   delay: 3000,
			},
			breakpoints:{
				1024: {
					slidesPerView: 2,
				},
				320: {
					slidesPerView: 1,
				},
			},
			pagination:{
				el: ".swiper-pagination",
			},
		});
	}
	
	// Testimonial Swiper 3
	if(jQuery('.testimonial-swiper3').length > 0){
		var testimonialswiper1 = new Swiper('.testimonial-swiper3', {
			speed: 1500,
			slidesPerView: 3,
			spaceBetween: 30,
			autoplay: {
			   delay: 3000,
			},
			pagination: {
				el: '.swiper-pagination1',
				clickable: true,
				renderBullet: function (index, className) {
				  return '<span class="' + className + '">' + (index + 1) + '</span>';
				},
			},
			breakpoints: {
				1024: {
					slidesPerView: 3,
				},
				691: {
					slidesPerView: 2,
				},
				320: {
					slidesPerView: 1,
				},
			}
		});
	}
	
	// Testimonial Swiper 4
	if(jQuery('.testimonial-swiper4').length > 0){
		var swiper3 = new Swiper('.testimonial-swiper4', {
			speed: 1500,
			parallax: true,
			slidesPerView: 3,
			spaceBetween: 30,
			loop:true,
			pagination: {
			  el: ".swiper-pagination",
			  clickable: true,
			},
			navigation: {
				nextEl: '.swiper-button-next3',
				prevEl: '.swiper-button-prev3',
			},
			breakpoints: {
				1191: {
					slidesPerView: 2,
				},
				691: {
					slidesPerView: 1,
				},
				320: {
					slidesPerView: 1,
				},
			}
		});
	}
	
	// Testimonial Swiper 5
	if(jQuery('.testimonial-swiper5').length > 0){
		var swiper3 = new Swiper('.testimonial-swiper5', {
			speed: 1500,
			parallax: true,
			slidesPerView: 3,
			spaceBetween: 30,
			loop:true,
			pagination: {
				el: '.swiper-pagination5',
				clickable: true,
				renderBullet: function (index, className) {
				  return '<span class="' + className + '">'+0 + (index + 1) + '</span>';
				},
			},
			navigation: {
				nextEl: '.swiper-button-next3',
				prevEl: '.swiper-button-prev3',
			},
			breakpoints: {
				1191: {
					slidesPerView: 2,
				},
				691: {
					slidesPerView: 1,
				},
				320: {
					slidesPerView: 1,
				},
			}
		});
	}
	
	// Testimonial Swiper 6
	if(jQuery('.testimonial-swiper6').length > 0){
		var testswiper2 = new Swiper('.testimonial-swiper6', {
			speed: 1500,
			slidesPerView: 2,
			spaceBetween: 30,
			loop:true,
			autoplay: {
			   delay: 3000,
			},
			breakpoints:{
				1024: {
					slidesPerView: 2,
				},
				320: {
					slidesPerView: 1,
				},
			},
			//pagination:{
			//	el: ".swiper-pagination",
			//},
		});
	}
	
	// Testimonial Swiper 7
	if(jQuery('.testimonial-swiper7').length > 0){
		var testimonialswiper1 = new Swiper('.testimonial-swiper7', {
			speed: 1500,
			slidesPerView: 3,
			spaceBetween: 30,
			autoplay: {
			   delay: 3000,
			},
			pagination: {
				el: '.swiper-pagination1',
				clickable: true,
				renderBullet: function (index, className) {
				  return '<span class="' + className + '">' + (index + 1) + '</span>';
				},
			},
			breakpoints: {
				1024: {
					slidesPerView: 3,
				},
				691: {
					slidesPerView: 2,
				},
				320: {
					slidesPerView: 1,
				},
			}
		});
	}
	
	// Testimonial Swiper 8
	if(jQuery('.testimonial-swiper8').length > 0){
		var testswiper1 = new Swiper('.testimonial-swiper8', {
			speed: 1500,
			slidesPerView: 1,
			spaceBetween: 30,
			autoplay: {
			   delay: 3000,
			},
			navigation: {
				nextEl: '.testimonial-prev8',
				prevEl: '.testimonial-next8',
			},
		});
	}
		
	/* All Testimonial Swiper End ========== 
	==================================*/
	
	
	
	
	/* All Team Swiper Start ========== 
	==================================*/
	
	// Team Swiper 1
	if(jQuery('.team-swiper1').length > 0){
		var teamswiper1 = new Swiper('.team-swiper1', {
			speed: 1500,
			slidesPerView: 3,
			spaceBetween: 30,
			loop:true,
			autoplay: {
			   delay: 3000,
			},
			navigation: {
				nextEl: '.swiper-button-next2',
				prevEl: '.swiper-button-prev2',
			},
			breakpoints: {
				1191: {
					slidesPerView: 3,
				},
				991: {
					slidesPerView: 3,
				},
				591: {
					slidesPerView: 2,
				},
				320: {
					slidesPerView: 1,
				},
			}
		});
	}
	
	// Team Swiper 2
	if(jQuery('.team-swiper2').length > 0){
		var teamswiper2 = new Swiper('.team-swiper2', {
			speed: 1500,
			slidesPerView: 3,
			spaceBetween: 30,
			loop:true,
			autoplay: {
			   delay: 3000,
			},
			pagination: {
				el: '.swiper-pagination-team-2',
				clickable: true,
				renderBullet: function (index, className) {
				  return '<span class="' + className + '">'+0 + (index + 1) + '</span>';
				},
			},
			navigation: {
				nextEl: '.swiper-button-next2',
				prevEl: '.swiper-button-prev2',
			},
			breakpoints: {
				1191: {
					slidesPerView: 3,
				},
				991: {
					slidesPerView: 3,
				},
				591: {
					slidesPerView: 2,
				},
				320: {
					slidesPerView: 1,
				},
			}
		});
	}
	
	// Team Swiper
	if(jQuery('.team-swiper3').length > 0){
		var teamswiper3 = new Swiper('.team-swiper3', {
			speed: 1500,
			parallax: true,
			slidesPerView: 4,
			spaceBetween: 30,
			loop:true,
			navigation: {
				nextEl: '.swiper-button-next4',
				prevEl: '.swiper-button-prev4',
			},
			breakpoints: {
				1191: {
					slidesPerView: 4,
				},
				991: {
					slidesPerView: 3,
				},
				591: {
					slidesPerView: 2,
				},
				320: {
					slidesPerView: 1,
				},
			}
		});
	}
	
	// Team Swiper 4
	if(jQuery('.team-swiper4').length > 0){
		var teamswiper4 = new Swiper('.team-swiper4', {
			speed: 1500,
			slidesPerView: 4,
			spaceBetween: 30,
			loop:true,
			centeredSlides: true,
			autoplay: {
			   delay: 3000,
			},
			navigation: {
				nextEl: '.team-next',
				prevEl: '.team-prev',
			},
			breakpoints: {
				1191: {
					slidesPerView: 4,
				},
				991: {
					slidesPerView: 3,
				},
				591: {
					slidesPerView: 2,
				},
				320: {
					slidesPerView: 1,
				},
			}
		});
	}
	
	// Team Swiper 5
	if(jQuery('.team-swiper5').length > 0){
		var teamswiper5 = new Swiper('.team-swiper5', {
			speed: 1500,
			spaceBetween: 30,
			slidesPerView: 4,
			loop:true,
			autoplay: {
			   delay: 3000,
			},
			pagination: {
				el: ".swiper-pagination",
			},
			breakpoints: {
				1191: {
					slidesPerView: 4,
				},
				991: {
					slidesPerView: 3,
				},
				591: {
					slidesPerView: 2,
				},
				320: {
					slidesPerView: 1,
				},
			}
		});
	}
	
	/* All Team Swiper End ========== 
	==================================*/
	
	
	
	
	
	// Services Swiper
	if(jQuery('.services-swiper').length > 0){
		var swiper3 = new Swiper('.services-swiper', {
			speed: 1500,
			parallax: true,
			slidesPerView: 4,
			spaceBetween: 30,
			//loop:true,
			pagination: {
				el: ".swiper-pagination",
				clickable: true,
			},
			breakpoints: {
				1191: {
					slidesPerView: 4,
				},
				991: {
					slidesPerView: 3,
				},
				691: {
					slidesPerView: 2,
				},
				320: {
					slidesPerView: 1,
				},
			}
		});
	}
	
	// Services Swiper
	if(jQuery('.portfolio-swiper').length > 0){
		var swiper3 = new Swiper('.portfolio-swiper', {
			speed: 1500,
			parallax: true,
			slidesPerView: 4,
			spaceBetween: 30,
			//loop:true,
			pagination: {
				el: ".swiper-pagination",
				clickable: true,
			},
			breakpoints: {
				1191: {
					slidesPerView: 4,
				},
				691: {
					slidesPerView: 1,
				},
				320: {
					slidesPerView: 1,
				},
			}
		});
	}
	
	// main-silder-swiper
	if(jQuery('.main-silder-swiper-04').length > 0){
		var swiper = new Swiper('.main-silder-swiper-04', {
			speed: 1500,
			parallax: true,
			loop:true,
			autoplay:true,
			pagination: {
				el: '.swiper-pagination',
				clickable: true,
			},
		});
	}
	
	// Swiper Portfolio
	if(jQuery('.swiper-portfolio1').length > 0){
		var portfolioswiper1 = new Swiper('.swiper-portfolio1', {
			slidesPerView: 1,
			spaceBetween: 30,
			speed: 1500,
			loop:true,
			autoplay: {
			   delay: 3000,
			},
			breakpoints: {
				1280: {
					slidesPerView: 4,
				},
				991: {
					slidesPerView: 3,
				},
				691: {
					slidesPerView: 2,
				},
				320: {
					slidesPerView: 1,
				},
			}
		});
	}
	
	// Blog Swiper
	if(jQuery('.blog-swiper').length > 0){
		var blogswiper = new Swiper('.blog-swiper', {
			slidesPerView: 3,
			spaceBetween: 0,
			speed: 1500,
			loop:true,
			autoplay: {
			   delay: 3000,
			},
			breakpoints: {
				1191: {
					slidesPerView: 3,
				},
				991: {
					slidesPerView: 2,
				},
				691: {
					slidesPerView: 2,
				},
				320: {
					slidesPerView: 1,
				},
			}
		});
	}

	// Blog Swiper
	if(jQuery('.post-swiper').length > 0){
		var swiper2 = new Swiper('.post-swiper', {
			slidesPerView: 1,
			spaceBetween: 0,
			speed: 1500,
			loop:true,
			autoplay: {
			   delay: 3000,
			},
			navigation: {
				nextEl: '.prev-post-swiper-btn',
				prevEl: '.next-post-swiper-btn',
			},
		});
	}
	

	// Blog Swiper
	if(jQuery('.blog-swiper-2').length > 0){
		var blogswiper = new Swiper('.blog-swiper-2', {
			slidesPerView: 3,
			spaceBetween: 20,
			speed: 1500,
			loop:true,
			autoplay: {
			delay: 3000,
			},
			breakpoints: {
				1600: {
					slidesPerView: 3,
				},
				991: {
					slidesPerView: 2,
				},
				691: {
					slidesPerView: 2,
				},
				320: {
					slidesPerView: 1,
				},
			}
		});
	}

	// Clients Swiper
	if(jQuery('.clients-swiper').length > 0){
		var swiper5 = new Swiper('.clients-swiper', {
			speed: 1500,
			parallax: true,
			slidesPerView: 4,
			spaceBetween: 30,
			autoplay: {
			   delay: 3000,
			},
			loop:true,
			navigation: {
				nextEl: '.swiper-button-next5',
				prevEl: '.swiper-button-prev5',
			},
			breakpoints: {
				1191: {
					slidesPerView: 6,
				},
				991: {
					slidesPerView: 5,
				},
				691: {
					slidesPerView: 4,
				},
				591: {
					slidesPerView: 3,
				},
				320: {
					slidesPerView: 2,
				},
			}
		});
	}
	

	// client swiper-2
	if(jQuery('.client-swiper-2').length > 0){
		var blogswiper = new Swiper('.client-swiper-2', {
			slidesPerView: 4,
			spaceBetween: 20,
			speed: 1500,
			loop:true,
			autoplay: {
				delay: 3000,
			},
			breakpoints: {
				1280: {
					slidesPerView: 5,
				},
				991: {
					slidesPerView: 4,
				},
				767: {
					slidesPerView: 3,
				},
				320: {
					slidesPerView: 2,
				},
			}
		});
	}


	// Testimonial Swiper 5
	if(jQuery('.project-carousel').length > 0){
		var swiper3 = new Swiper('.project-carousel', {
			speed: 1500,
			parallax: true,
			slidesPerView: 3,
			spaceBetween: 0,
			loop:true,
			navigation: {
				nextEl: '.project-prev',
				prevEl: '.project-next',
			},
			breakpoints: {
				1191: {
					slidesPerView: 3,
				},
				691: {
					slidesPerView: 2,
				},
				320: {
					slidesPerView: 1,
				},
			}
		});
	}
	
	if(jQuery('.testimonial-swiper9').length > 0){
		var swiper = new Swiper(".testimonial-swiper9-thumb", {
			spaceBetween: 5,
			slidesPerView: 7,
			freeMode: true,
			watchSlidesProgress: true,
			breakpoints: {
				1366:{
					slidesPerView: 7,
				},
				1191:{
					slidesPerView: 6,
				},
				991:{
					slidesPerView: 5,
				},
				767:{
					slidesPerView: 4,
				},
				591:{
					slidesPerView: 3,
				},
				320:{
					slidesPerView: 2,
				},
			}
		});
		var swiper2 = new Swiper(".testimonial-swiper9", {
			spaceBetween: 10,
			navigation: {
				nextEl: ".testimonial-next9",
				prevEl: ".testimonial-prev9",
			},
			thumbs: {
				swiper: swiper,
			},
			
		});
	}
	
});
jQuery(window).on('resize',function(){
	homeSlider();
});
/* Document .ready END */