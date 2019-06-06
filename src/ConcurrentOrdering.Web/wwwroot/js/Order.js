// Order JS

// Constants
const SUCCESS = 'bg-success';
const ERROR = 'bg-danger';
const WARNING = 'bg-warning';

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
        toasts.Notify(`User ${userId}`, 'Order completed successfully', SUCCESS);
        ordering.UpdateAvailableUnits(result);
    })
    .fail(function (err, textStatus) {
        console.log(err);
        if (err.responseJSON.allowRetry) {
            toasts.Notify(
                `ERROR: User ${userId}`,
                'Order failed because another user already took the item. Please retry.',
                WARNING
            );
        } else {
            toasts.Notify(
                `ERROR: User ${userId}`,
                err.responseJSON.error,
                ERROR
            );
        }
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
        toasts.Notify(`Reset units`, 'Operation completed successfully', SUCCESS);
        ordering.UpdateAvailableUnits(result);
    })
    .fail(function (err, textStatus) {
        toasts.Notify('ERROR: Product update', err.responseJSON.error, ERROR);
    });
};

ordering.UpdateAvailableUnits = function (product) {
    $(`#units_${product.id}`).text(product.availableUnits);
};