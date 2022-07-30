var Notification = function (type,message) {
    const Toast = Swal.mixin({
        toast: true,
        position: 'top-end',
        showConfirmButton: false,
        timer: 2000,
        timerProgressBar: true,
        didOpen: (toast) => {
            toast.addEventListener('mouseenter', Swal.stopTimer)
            toast.addEventListener('mouseleave', Swal.resumeTimer)
        }
    })

    Toast.fire({
        icon: type,
        title: message
    })
}

var delProductCart = function (productID, size, isDelAll = false) {
    $.ajax({
        url: "/giohang/Delete",
        method: "DELETE",
        data: {
            productID: productID,
            size: size,
            isDeleteAll: isDelAll
        },
        success: function (data) {
            if (data.status == 200) {
                Notification("success", data.message);
                if (isDelAll) {
                    $('.cart-list').html('<p class="text-center m-auto">Giỏ hàng trống</p>')
                } else {
                    $('#' + productID).remove();
                    //totalMoney
                }
                $('.txtTotal').html(data.totalMoney ?? ('0 đ'))
                $('.btnCart span').text(data.count??0)
            } else {
                Notification("error", data.message);
            }
        }
    })
}

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

    $('#btnAddCart').on('click', function (e) {
        e.preventDefault();
        var countCart = $('.btnCart span').text()??0;
        var productID = $('#productID').text();
        var soLuong = $('#txtNumberProduct').val();
        var size = $('#selectSize').val();
        $.ajax({
            url: "/giohang/create",
            method: "POST",
            data: {
                numberProduct: soLuong,
                productID: productID,
                size: size,
            },
            success: function (data) {
                if (data.status == 200) {
                    Notification("success", data.message)
                    $('.btnCart span').text(data.count ?? countCart)
                } else {
                    Notification("error", data.message)
                }
            }
        })
    })

    $('#btnThanhToan').on('click', function (e) {
        e.preventDefault();
        var diaChi = $('#diaChiID').val();
        if (diaChi) {
            $.ajax({
                url: "/giohang/pay",
                method: "POST",
                data: {
                    diaChi: diaChi
                },
                success: function (data) {
                    if (data.status == 200) {
                        Swal.fire(
                            'Đặt hành thành công',
                            `Mã đơn hàng: ${data.madh}`,
                            'success'
                        )
                        $('.cart-list').html(`<p class="text-center m-auto">Giỏ hàng trống</p>`)
                        $('.txtTotal').html('0 ₫')
                        $('.btnCart span').text(0)
                    } else {
                        Notification("error", data.message);
                    }
                }
            })
        } else {
            Notification("error", 'Vui lòng cập nhật địa chỉ');
        }
    })

    $('input[name=lstaddress]').on('change', function () {
        var diachi = $(this).closest('label').html().replace(/(<input[\w+ \=\"]+\>)/g,"").trim();
        $('#viewAddress').html(diachi);
    })

    var delProductCart = function (productID, size,isDelAll=false) {
        $.ajax({
            url: "/giohang/delete",
            method: "DELETE",
            data: {
                productID: productID,
                size: size,
                isDeleteAll:isDelAll
            },
            success: function (data) {
                if (data.status == 200) {
                    Notification("success", data.message);
                    if (isDelAll) {
                        $('.cart-list').html('<p class="text-center m-auto">Giỏ hàng trống</p>')
                    } else {
                        $('#' + productID).remove();
                    }
                } else {
                    Notification("error", data.message);
                }
            }
        })
    }

    $('input[type=number]').on('change', function () {
        var _this = $(this);
        var soluong = $(this).val();
        var productID = $(this).attr('data-id').trim();
        var size = $(this).closest(`#${productID}`).find('.txtSize').text().trim();
        $.ajax({
            url: "/giohang/create",
            method: "POST",
            data: {
                numberProduct: soluong,
                productID: productID,
                size: size,
                isUpdateModified: true
            },
            success: function (data) {
                if (data.status == 200) {
                    if (soluong == 0) {
                        delProductCart(productID, size)
                    } else {
                        _this.closest('.col-3').children('.totalPriceProduct').text(data.totalProduct)
                        $('.txtTotal').text(data.totalCart)
                    }
                } else {
                    Notification("error", data.message);
                }
            }
        })
    })


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

    $('#chooseAddress').on('click', function () {
        $('.lst-address').slideToggle();
    })

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
