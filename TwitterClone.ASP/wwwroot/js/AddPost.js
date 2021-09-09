$("#btn-addPost").click(function () {
	let data = $("#addPost");
	let serdata = $(data).serialize();
	$.ajax({
		url: '/Post/AddPost',
		method: 'post',
		dataType: 'html',
		data: serdata,
		success: function () {
			$("#textPost").val(' ');
			GetPostForUser();
        }
	});
});