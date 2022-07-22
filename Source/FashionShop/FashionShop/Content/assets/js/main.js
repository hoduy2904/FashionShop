$(document).ready(function () {
    $('.banner').slick({
        dots: true,
        infinite: true,
        speed: 500,
        autoplay: true,
        autoplaySpeed: 2000,
        fade: true,
        cssEase: 'linear'
    });


    var Dropdowns = function () {
        var t = $(".dropup, .dropright, .dropdown, .dropleft")
            , e = $(".dropdown-menu")
            , r = $(".dropdown-menu .dropdown-menu");
        $(".dropdown-menu .dropdown-toggle").on("click", function () {
            var a;
            return (a = $(this)).closest(t).siblings(t).find(e).removeClass("show"),
                a.next(r).toggleClass("show"),
                !1
        }),
            t.on("hide.bs.dropdown", function () {
                var a, t;
                a = $(this),
                    (t = a.find(r)).length && t.removeClass("show")
            })
    }()

    $('.image-products').slick({
        slidesToShow: 1,
        slidesToScroll: 1,
        arrows: false,
        fade: true,
        asNavFor: '.navImage-product'
    });
    $('.navImage-product').slick({
        slidesToShow: 3,
        slidesToScroll: 1,
        asNavFor: '.image-products',
        dots: true,
        centerMode: true,
        focusOnSelect: true
    });

    $('.slideProductSame').slick({
        infinite: false,
        slidesToShow: 4,
        slidesToScroll: 1,
        responsive: [
            {
                breakpoint: 1024,
                settings: {
                    slidesToShow: 3,
                    slidesToScroll: 1,
                    infinite: true,
                    dots: true
                }
            },
            {
                breakpoint: 600,
                settings: {
                    slidesToShow: 2,
                    slidesToScroll: 2
                }
            }
        ]
    });
});
