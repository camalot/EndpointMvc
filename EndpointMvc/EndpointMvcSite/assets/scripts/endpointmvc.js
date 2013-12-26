﻿(function ($, mouseTrap) {
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

		$(".epm-nav-toggle").on("click", function () {
			var self = $(this);
			var target = $(self.data("epm-target"));
			var toggle = self.data("epm-toggle");
			var collapsed = "collapsed";
			if (target.hasClass(toggle)) {
				target.removeClass(toggle).addClass(collapsed);
				self.attr("title", "Open");
			} else {
				target.removeClass(collapsed).addClass(toggle);
				self.attr("title", "Close");
			}
			self.trigger("click.epm-mousetrap");
		});

		$(".epm-nav .nav-search input.search-field").on("keyup reset.epm-search", function (event) {
			var self = $(this);
			var $list = $(self).closest("ul.nav");
			var items = $("li", $list).not(".no-filter");
			var value = $(this).val();
			var $clear = $(".epm-nav .nav-search button.clear");
			$clear.trigger(value && value.length > 0 ? "show-clear.epm-search" : "hide-clear.epm-search");
			items.each(function () {
				var s = $(this);
				if (s.text().toLowerCase().search(value.toLowerCase()) > -1 || (!value || value.length == 0)) {
					s.removeClass("hidden");
				} else {
					s.addClass("hidden");
				}
			});
		});
		$(".epm-nav .nav-search button.clear").on("click", function (event) {
			$(".epm-nav .nav-search input.search-field").val("").trigger("reset.epm-search");
		}).on("show-clear.epm-search",function(event){
			$(this).removeClass("hidden");
		}).on("hide-clear.epm-search", function (event) {
			$(this).addClass("hidden");
		});

		$("body").scrollspy({ target: ".epm-nav .nav", offset: 100 });

		// if you have mousetrap (http://craig.is/killing/mice) loaded, endpointMVC will bind some events
		if (mouseTrap) {
			// change the input to have the mouse bindings:
			var field = $(".epm-nav.expanded .nav-search input.search-field");
			var currentPH = field.attr("placeholder");
			field.attr("placeholder", currentPH + " (Ctrl+Q)");

			$(".epm-nav-toggle").on("click.epm-mousetrap", function () {
				$(".epm-nav.expanded.left .epm-nav-toggle").attr("title", "Close (Ctrl+Left)");
				$(".epm-nav.collapsed.right .epm-nav-toggle").attr("title", "Open (Ctrl+Left)");
				$(".epm-nav.collapsed.left .epm-nav-toggle").attr("title", "Open (Ctrl+Right)");
				$(".epm-nav.expanded.right .epm-nav-toggle").attr("title", "Close (Ctrl+Right)");
			}).trigger("click.epm-mousetrap");


			mouseTrap.bind("ctrl+q", function (e) {
				$(".epm-nav.expanded .nav-search input.search-field").focus();
			}).bind("ctrl+left",function(e) {
				$(".epm-nav.expanded.left .epm-nav-toggle").trigger("click").attr("title","Open (Ctrl+Right)");
				$(".epm-nav.collapsed.right .epm-nav-toggle").trigger("click").attr("title", "Close (Ctrl+Right)");
			}).bind("ctrl+right",function(e){
				$(".epm-nav.expanded.right .epm-nav-toggle").trigger("click").attr("title", "Open (Ctrl+Left)");
				$(".epm-nav.collapsed.left .epm-nav-toggle").trigger("click").attr("title", "Close (Ctrl+Left)");
			});
		}
	});
})(window.jQuery, window.Mousetrap);