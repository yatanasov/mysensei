(function(){
	"use strict"
	var howItWorksBtn = document.querySelector('.howItWorks');
	var howItWorks = document.querySelector('#howitworks');

	howItWorksBtn.onclick=function(e){
		e.preventDefault();
		var howTop = $(howItWorks).offset().top;
		$('html, body').animate({
			scrollTop: howTop
		}, 2000);
	}


})(jQuery);