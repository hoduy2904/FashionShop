var Dropdowns = function() {
    var t = $(".dropup, .dropright, .dropdown, .dropleft")
      , e = $(".dropdown-menu")
      , r = $(".dropdown-menu .dropdown-menu");
    $(".dropdown-menu .dropdown-toggle").on("click", function() {
        var a;
        return (a = $(this)).closest(t).siblings(t).find(e).removeClass("show"),
        a.next(r).toggleClass("show"),
        !1
    }),
    t.on("hide.bs.dropdown", function() {
        var a, t;
        a = $(this),
        (t = a.find(r)).length && t.removeClass("show")
    })
}()