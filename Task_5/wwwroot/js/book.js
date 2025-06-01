let currentIndex = 1;
let isLoading = false;

function getParams() {
    return {
        region: $("#region").val(),
        seed: parseInt($("#seed").val()),
        avgLikes: parseFloat($("#likes").val()),
        avgReviews: parseFloat($("#reviews").val()),
    };
}

function loadBatch(count) {
    if (isLoading) return;
    isLoading = true;

    const params = getParams();
    const data = {
        ...params,
        startIndex: currentIndex,
        count: count
    };

    $.ajax({
        url: '/Home/Generate',
        method: 'POST',
        contentType: 'application/json',
        data: JSON.stringify(data),
        success: function (html) {
            $('#book-table tbody').append(html);
            currentIndex += count;
        },
        complete: function () {
            isLoading = false; 
        }
    });
}

function resetTable() {
    $('#book-table tbody').empty();
    currentIndex = 1;
    loadBatch(20);
}

$("#region, #seed, #likes, #reviews").on("change input", function () {
    $("#likes-val").text($("#likes").val());
    resetTable();
});

function randomizeSeed() {
    let seed = Math.floor(Math.random() * 100000);
    $("#seed").val(seed);
    resetTable();
}

$(window).on("scroll", function () {
    if ($(window).scrollTop() + $(window).height() >= $(document).height() - 10) {
        loadBatch(10);
    }
});

$(document).ready(function () {
    resetTable();
});
