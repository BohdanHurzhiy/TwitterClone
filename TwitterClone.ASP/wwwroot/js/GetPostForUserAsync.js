$(document).ready(function () {
	GetPostForUser();
	NewPagination();	
});
numberPage = 1;
function GetPostForUser() {
	let idUser = $("#IdUser").val();
	let numPosts = 10;
	$.ajax({
		url: 'Post/GetPostForUser',
		method: 'post',
		dataType: 'html',		
		data: { id: idUser, numberPage: numberPage, numberOfPosts: numPosts },
		success: function (data) {
			$('#posts-body').append(data);			
		}
	});
}

function NewPagination() {
	let ForOnePage = document.documentElement.clientHeight * (0.6);//half of the visible window
	let NextPage = ForOnePage;
	numberPage++;
	$(window).on('scroll', function () {
		let posts = document.querySelector('#posts-body');
		let postsTop = posts.getBoundingClientRect().top;
		if ((NextPage + postsTop) < 0) {
			console.log('Page number: ' + numberPage);
			GetPostForUser(numberPage);
			numberPage++;
			NextPage += ForOnePage;
		}
		else {
			Resize(numberPage);
		}
	});
	setTimeout(() => Resize(numberPage), 1000);
	$(window).on('resize', function () {
		Resize(numberPage);
	});
}

function Resize()
{
	let heightDocument = document.documentElement.clientHeight;
	let posts = document.querySelector('#posts-body');
	let postsBottom = posts.getBoundingClientRect().bottom;
	if ((heightDocument / postsBottom) > 1) {
		console.log('Page number: ' + numberPage);
		GetPostForUser(numberPage);
		numberPage++;
	}
}