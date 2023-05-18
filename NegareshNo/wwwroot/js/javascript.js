// owl carousel
let option = {
    margin: 10,
    // loop: true,
    nav: true,
    navText: ['قبلی', 'بعدی'],
    items: 3,
    slideBy: '2',
    dots: true,
    autoplay: true,
    autoplayTimeout: 4000,
    autoplayHoverPause: true,
    center: false,
    mouseDrag: true,
    touchDrag: true,
    autoWidth: true,
    startPosition: 0,
    rtl : true
};
$(document).ready(() => {
    $('.owl-carousel').owlCarousel(option);
});