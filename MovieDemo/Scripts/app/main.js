$(function () {
    $('#butAhead').click(moveTimeAhead);
    $('#butBack').click(moveTimeBack);

    function moveTimeAhead() {
        var time = moment.utc($("#filterDate").val()).add(1, 'days');
        $("#filterDate").val(time.format('YYYY-MM-DD'));
        $('#filterForm').submit();
    }

    function moveTimeBack() {
        var time = moment.utc($("#filterDate").val()).subtract(1, 'days');
        $("#filterDate").val(time.format('YYYY-MM-DD'));
        $('#filterForm').submit();
    }

});