// Order JS
const ORDER_URL = `${window.location.origin}/api/order/`;
const ORDER_METHOD = 'POST';

const PRODUCT_RESET_URL = `${window.location.origin}/api/product/`;
const PRODUCT_RESET_METHOD = 'POST';

const ordering = {};

ordering.SubmitOrder = function (product, userId) {
    $.ajax({
        method: ORDER_METHOD,
        url: ORDER_URL,
        contentType: 'application/json',
        dataType: 'json',
        data: JSON.stringify({ ProductId: product, Quantity: 1 })
    })
    .done(function (result) {        
        console.log(result);
        ordering.UpdateAvailableUnits(result);
    })
    .fail(function (err, textStatus) {
        alert(`User ${userId} failed to complete order. ${JSON.stringify(err.responseJSON)}`);
        console.error(err);
        console.error(textStatus);
    });
};

ordering.Order = function (product, concurrentTimes) {
    for (let i = 0; i < concurrentTimes; ++i) {
        this.SubmitOrder(product, i + 1);
    }
};

ordering.SubmitReset = function (product, quantity) {
    $.ajax({
        method: PRODUCT_RESET_METHOD,
        url: PRODUCT_RESET_URL,
        contentType: 'application/json',
        dataType: 'json',
        data: JSON.stringify({ Id: product, AvailableUnits: quantity })
    })
    .done(function (result) {
        ordering.UpdateAvailableUnits(result);
    })
    .fail(function (err, textStatus) {
        alert(`Product ${product} failed to update`);
        console.error(err);
        console.error(textStatus);
    });
}

ordering.UpdateAvailableUnits = function (product) {
    $(`#units_${product.id}`).text(product.availableUnits);
}

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