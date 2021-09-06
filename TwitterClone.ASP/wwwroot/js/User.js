////$(document).ready(function () {

////	$('#btnSendMessage').click(function (event) {
////		event.preventDefault();

////		let Follower = "userId";
////		let Followed = "targetId";
////		let data = "dfsfsfsdfsf";
////		$.ajax({
////			url: 'User/MessageHandler',         /* Куда пойдет запрос */
////			method: 'post',             /* Метод передачи (post или get) */
////			dataType: 'html',          /* Тип данных в ответе (xml, json, script, html). */
////			data: { data: data },     /* Параметры передаваемые в запросе. */
////			success: function (data) {   /* функция которая будет выполнена после успешного запроса.  */
////				alert(data);            /* В переменной data содержится ответ от index.php. */
////			}
////		});    
////    });
////})

$("#form").on("click", function () {
	$.ajax({
		url: 'User/MessageHandler',
		method: 'post',
		dataType: 'html',
		data: $(this).serialize(),
		success: function (data) {
			let text = JSON.parse(data);
			$('#btn-subscribe').val(text);			
		}
	});
});