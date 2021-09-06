$(document).ready( function () {
    let location = window.location.href;
    let cur_url = '/' + location.split('/').pop();

    $('.menu').each(function () {
        let link = $(this).find('a').toArray();
        link.forEach(function (element)
        {
            let hrefElement = '/' + element.href.split('/').pop();
            if (hrefElement == cur_url)
            {
                element.className = "current "+ element.className;
                console.log(hrefElement);
            }
        })       
    });
});