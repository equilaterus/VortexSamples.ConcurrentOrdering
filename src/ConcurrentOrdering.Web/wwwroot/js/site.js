// Constants
const SUCCESS = 'bg-success';
const ERROR = 'bg-danger';
const WARNING = 'bg-warning';

// Utils
function uuidv4() {
    return ([1e7] + -1e3 + -4e3 + -8e3 + -1e11).replace(/[018]/g, c =>
        (c ^ crypto.getRandomValues(new Uint8Array(1))[0] & 15 >> c / 4).toString(16)
    );
}

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
        alert(`Product ${product} failed to update`, ERROR);
    });
};

ordering.UpdateAvailableUnits = function (product) {
    $(`#units_${product.id}`).text(product.availableUnits);
};

// Toast behavior
const TOAST_AREA_SELECTOR = '#toast-area'; 

const toasts = {};
toasts.Id = 0;
toasts.Notify = function (title, message, bgClass) {
    const toastId = ++this.Id;
    $(TOAST_AREA_SELECTOR).append(
        `<div id="toast_${toastId}" class="toast " role="alert" aria-live="assertive" aria-atomic="true" data-delay="10000" data-autohide="true">
          <div class="toast-header ${bgClass} text-white">
            <strong class="mr-auto">${title}</strong>
            <small>11 mins ago</small>
            <button type="button" class="ml-2 mb-1 close" data-dismiss="toast" aria-label="Close">
              <span aria-hidden="true">&times;</span>
            </button>
          </div>
          <div class="toast-body">
            ${message}
          </div>
        </div>`
    );
    $(`#toast_${toastId}`).toast('show');

    $(`#toast_${toastId}`).on('hidden.bs.toast', function () {
        $(this).toast('dispose');
    });
};

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