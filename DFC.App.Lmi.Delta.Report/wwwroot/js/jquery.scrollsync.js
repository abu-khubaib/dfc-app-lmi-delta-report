// https://github.com/zjffun/jquery-ScrollSync

(function (global, factory) {
    typeof exports === 'object' && typeof module !== 'undefined' ? factory(exports, require('jquery')) :
        typeof define === 'function' && define.amd ? define(['exports', 'jquery'], factory) :
            (factory((global.$ = global.$ || {}, global.$.fn = global.$.fn || {}), global.$));
}(this, (function (exports, $) {
    'use strict';

    $ = $ && $.hasOwnProperty('default') ? $['default'] : $;

    function debounce(func, threshold) {
        var timeout;
        return function debounced() {
            var obj = this, args = arguments;
            function delayed() {
                func.apply(obj, args);
                timeout = null;
            }
            timeout && clearTimeout(timeout);
            timeout = setTimeout(delayed, threshold || 100);
        };
    }

    function smartscroll(fn, threshold) {
        return fn ? this.bind('scroll', debounce(fn, threshold)) : this.trigger('smartscroll');
    }

    //jquery-smartscroll
    $.fn.smartscroll = smartscroll;

    function scrollsync(options) {
        var defaluts = {
            x_sync: true,
            y_sync: true,
            use_smartscroll: false,
            smartscroll_delay: 10,
        };

        var options = $.extend({}, defaluts, options);
        console.log(options);

        var scroll_type = options.use_smartscroll ? 'smartscroll' : 'scroll';
        var $containers = this;

        var scrolling = {};
        Object.defineProperty(scrolling, 'top', {
            set: function (val) {
                $containers.each(function () {
                    $(this).scrollTop(val);
                });
            }
        });
        Object.defineProperty(scrolling, 'left', {
            set: function (val) {
                $containers.each(function () {
                    $(this).scrollLeft(val);
                });
            }
        });

        $containers.on({
            mouseover: function () {
                if (scroll_type == 'smartscroll') {
                    $(this).smartscroll(function () {
                        options.x_sync && (scrolling.top = $(this).scrollTop());
                        options.y_sync && (scrolling.left = $(this).scrollLeft());
                    }, options.smartscroll_delay);
                    return;
                }
                $(this).bind('scroll', function () {
                    options.x_sync && (scrolling.top = $(this).scrollTop());
                    options.y_sync && (scrolling.left = $(this).scrollLeft());
                });
            },
            mouseout: function () {
                $(this).unbind('scroll');
            }
        });


        return this;
    }

    exports.scrollsync = scrollsync;

    Object.defineProperty(exports, '__esModule', { value: true });

})));