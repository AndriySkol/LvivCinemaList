$(function()
{
    $("#rating").rating({ min: 1, max: 5, step: 1, size: 'xs', showClear: false, showCaption: false });
    $('#rating').on('rating.change', function (event, value, caption) {
        model.rate = value;
        $.post("/api/likes/like", model);

    });

    $(".like").click(function (event) {
        var cur = $(event.target);
        
        if (cur.attr('active') === undefined)
        {
            var parent = cur.parents('.comment');
            $.post("/api/comment/like", { '': parent.attr('data-cid') });
            var commentRate = parent.find('.comment-rate');
            var i = 1;
            var close = parent.find('.unlike')
            if (close.attr('active') !== undefined)
            {
                i = 2;
                close.removeAttr('active');
            }
            commentRate.text(parseInt(commentRate.text()) + i);
            cur.attr('active', 'true');
        }
        
    });

    $(".unlike").click(function (event) {
        var cur = $(event.target);

        if (cur.attr('active') === undefined) {
            var parent = cur.parents('.comment');
            $.post("/api/comment/unlike", { '': parent.attr('data-cid') });
            var commentRate = parent.find('.comment-rate');
            var i = 1;
            var close = parent.find('.like')
            if (close.attr('active') !== undefined) {
                i = 2;
                close.removeAttr('active');
            }
            commentRate.text(parseInt(commentRate.text()) - i);
            cur.attr('active', 'true');
        }
    });

    $("#orderSelect").change(function (event) {
        location.href = "/showtime?id=" + showId + "&orderString=" + $(event.target).val();
    });
    
    if(ordered == true)
    {
        $(window).scrollTop($('.comment').offset().top - $('.comment').height());
    }
})