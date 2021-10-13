$(document).ready(function () {
	GetPostForUser();
	Scroll();
});
$("#btn-addPost").click(function () { GetPostForUser() });

function GetPostForUser(numberPage = 1) {
	let idUser = $("#IdUser").val();
	let numPosts = 10;
	$.ajax({
		url: 'Post/GetPostForUser',
		method: 'post',
		dataType: 'html',		
		data: { id: idUser, numberPage: numberPage, numberOfPosts: numPosts },
		success: function (data) {
			let htmlData = $('#posts-body').html();
			$('#posts-body').html(htmlData + data);
		}
	});
}

function Scroll() {	
	let scrollForOnePage = document.documentElement.clientHeight/2;
	let lastScrollTop = 0;
	let pageNumber = 2;
	$(window).on('scroll', function () {
		let scrollTop = $(this).scrollTop();
		if ((scrollTop - lastScrollTop) > scrollForOnePage ) {
			GetPostForUser(pageNumber);
			pageNumber++;
			console.log('ScrollTop: ' + scrollTop);
			console.log('Last: ' + lastScrollTop);
			console.log('Page: ' + pageNumber);
			lastScrollTop += scrollForOnePage;
		}
	});
}