$(document).ready(function () { GetPostForUser() });

function GetPostForUser() {
	let idUser = $("#IdUser").val();
	let numPosts = 10;
	$.ajax({
		url: 'Post/GetPostForUser',
		method: 'post',
		dataType: 'html',
		data: { id: idUser, numberPosts: numPosts },
		success: function (data) {
			$('#posts-body').html(data);
		}
	});
}