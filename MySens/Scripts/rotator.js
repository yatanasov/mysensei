(function(){
	"use strict"
	var btnRotateLeft = document.querySelector('.tBtn1'); 
	var btnRotateRight = document.querySelector('.tBtn2'); 
	var rotator = document.querySelector('.buffer');

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
			rotator.style.left = leftPos+"%";
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
			rotator.style.left = leftPos+"%";
		}

	}

	homeMadeCarousel();

})(jQuery);