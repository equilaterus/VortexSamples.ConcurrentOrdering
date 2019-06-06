// jQuery events

$(function () {
    $('button[data-ordering]').click(function () {
        const concurrentTimes = $(this).data('ordering');
        const product = $(this).data('product-id');
        ordering.Order(product, concurrentTimes);
    });

    $('button[data-reset]').click(function () {
        const quantity = $(this).data('reset');
        const product = $(this).data('product-id');
        ordering.SubmitReset(product, quantity);
    });
});