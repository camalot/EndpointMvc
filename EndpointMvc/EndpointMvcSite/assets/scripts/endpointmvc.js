(function ($) {
	"use strict";
	$(function () {
		$("[data-toggle='popover']").each(function () {
			var self = $(this);
			if (self.data("popover-setup")) {
				return;
			}
			self.data("popover-setup", true);
			self.popover();
		});
		console.log($(".epm-nav-toggle"));
		$(".epm-nav-toggle").on("click", function () {
			console.log("click");
			var self = $(this);
			var target = $(self.data("epm-target"));
			var toggle = self.data("epm-toggle");
			console.log(target);
			console.log(toggle);
			target.toggleClass(toggle);
		});

		$("body").scrollspy({ target: ".epm-nav .nav", offset: 100 });
	});
})(window.jQuery);