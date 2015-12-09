(function(){
	"use strict"
	
	var loggedinBtn = document.querySelector('.loggedIn');
	var popup = document.querySelector('.popup');
	var landing = document.querySelector('.landing');

	loggedinBtn.onclick=function(e){
		e.preventDefault();
		popup.classList.toggle('hidden');

		landing.onclick=function(){
			popup.classList.add('hidden');
		}

		popup.onclick=function(e){
			e.stopPropagation();
		}
	}
})(jQuery);