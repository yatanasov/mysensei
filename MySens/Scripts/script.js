(function(){
	"use strict"
	var loginBtn = document.querySelector('.loginBtn');
	var loginOverlay = document.querySelector('.loginOverlay');
	var signup = document.querySelector('.signup');
	var signupBtn = document.querySelector('.signupBtn');
	var landing = document.querySelector('.landing');
	var howItWorksBtn = document.querySelector('.howItWorks');
	var howItWorks = document.querySelector('#howitworks');
	var btnRotateLeft = document.querySelector('.tBtn1'); 
	var btnRotateRight = document.querySelector('.tBtn2'); 
	var rotator = document.querySelector('.buffer');


	loginBtn.onclick=function(e){
		e.preventDefault();
		loginOverlay.classList.toggle('nonExistent');
		loginOverlay.classList.toggle('hidden');

		landing.onclick=function(){
			loginOverlay.classList.add('hidden');
			loginOverlay.classList.add('nonExistent');
		}

		signupBtn.onclick=function(){
			loginOverlay.classList.add('hidden');
			signup.classList.remove('hidden');
			signup.classList.remove('nonExistent');
		}

		loginOverlay.onclick=function(e){
			e.stopPropagation();
		}
	}

	howItWorksBtn.onclick=function(e){
		e.preventDefault();
		var howTop = $(howItWorks).offset().top;
		$('html, body').animate({
			scrollTop: howTop
		}, 2000);
	}

	

	function homeMadeCarousel(){
		var leftPos = 0;

		btnRotateRight.onclick=function(){
			if (leftPos <= -66) {
				btnRotateRight.classList.add('done');
			}else if (leftPos > -66) {
				btnRotateRight.classList.remove('done');
				btnRotateLeft.classList.remove('done');
				leftPos-=33;
			} else if (leftPos >= 0) {
				btnRotateLeft.classList.add('done');
			}
			rotator.style = 'left:'+leftPos+'%;';
		}

		btnRotateLeft.onclick=function(){
			if (leftPos >= 0) {
				btnRotateLeft.classList.add('done');
			} else if (leftPos < 0) {
				btnRotateRight.classList.remove('done');
				btnRotateLeft.classList.remove('done');
				leftPos +=33;
			} else if (leftPos <= -66) {
				btnRotateRight.classList.add('done');
			}
			rotator.style = 'left:'+leftPos+'%;';
		}

	}

	homeMadeCarousel();



	

})(jQuery);