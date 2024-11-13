const $valueSpan1 = $('.valueSpan1'); const $valueSpan2 = $('.valueSpan2'); const $valueSpan3 = $('.valueSpan3'); const $valueSpan4 = $('.valueSpan4');
const $value1 = $('#slider1'); const $value2 = $('#slider2'); const $value3 = $('#slider3'); const $value4 = $('#slider4');
$valueSpan1.html($value1.val()); $valueSpan1.html($value1.val()); $valueSpan3.html($value3.val()); $valueSpan4.html($value4.val());
$value1.on('input change', () => {

    $valueSpan1.html($value1.val());
});
$value2.on('input change', () => {

    $valueSpan2.html($value2.val());
});
$value3.on('input change', () => {

    $valueSpan3.html($value3.val());
});
$value4.on('input change', () => {

    $valueSpan4.html($value4.val());
});
function toast() {
    toastr.options.timeOut = 1500; // 1.5s
    toastr.success('Successfully Posted');
    setTimeout(function () { }, 3000);
}
function toast1() {
    toastr.options.timeOut = 1500; // 1.5s
    toastr.warning('No Connection...Posting Failed!');
    setTimeout(function () { }, 1500);
}