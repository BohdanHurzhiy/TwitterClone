function searchTag() {
	let data = $("#Tag").val();

	$.ajax({
		url: '/Tag/SearchByTag',
		method: 'post',
		dataType: 'html',
		data: { tag: data },
		success: function (data) {
			$("#tag-for-posts").html(data);
		}
	});
}

$("#Tag").focus(function () {
	let data = $("#SearchTag");
	let text = "Enter some text to search";
	$("#tag-for-posts").html(text);
	$("#tag-for-posts").removeClass("hide");
});

$("#Tag").focusout(function () {
	$("#tag-for-posts").html();
	$("#tag-for-posts").addClass("hide");
	$("#Tag").val("");
});