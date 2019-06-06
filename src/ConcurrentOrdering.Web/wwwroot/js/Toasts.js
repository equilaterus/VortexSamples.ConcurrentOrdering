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
            <small>Just now</small>
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
