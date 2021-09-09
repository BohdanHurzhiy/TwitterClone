$(document).ready(() => GetUserPosts());

function GetUserPosts() {
	let idUser = $("#Id").val();
	let numPosts = 10;
	$.ajax({
		url: '/Post/GetUserPosts',
		method: 'post',
		dataType: 'html',
		data: { id: idUser, numberPosts: numPosts },
		success: function (data) {
			$('#posts-body').html(data);
		}
	});
}