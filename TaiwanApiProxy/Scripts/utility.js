/* jshint freeze:false */
/*global numeral*/

// support function outerHTML
jQuery.fn.outerHTML = function (s) {
    'use strict';
    return s
        ? this.before(s).remove()
        : jQuery("<p>").append(this.eq(0).clone()).html();
};

// serialize a form into json object
$.fn.serializeObject = function () {
    'use strict';
    var o = {};
    // find disabled inputs
    var inputs = this.find(':disabled');
    // enable them
    inputs.prop('disabled', false);

    var a = this.serializeArray();

    // then, disable them
    inputs.prop('disabled', true);

    $.each(a, function () {
        if (o[this.name] === undefined) {
            o[this.name] = this.value || '';
        }
    });
    return o;
};

// Implement string.format.
// Example: 'Hello {0}'.format('world') -> 'Hello world'
if (!String.prototype.format) {
    String.prototype.format = function () {
        'use strict';
        var args = arguments;
        return this.replace(/{(\d+)}/g, function (match, number) {
            return typeof args[number] !== 'undefined' ? args[number] : match;
        });
    };
}

// check whether a string is numberic or not
String.prototype.isNumber = function () {
    'use strict';
    var regs = new RegExp(/^-?\d+(\.\d+)?\s*$/);
    return (regs.test(this));
};

// Getting the number of decimals in a number
Number.prototype.countDecimals = function () {
    'use strict';
    if (Math.floor(this.valueOf()) === this.valueOf()) {
        return 0;
    }
    return this.toString().split(".")[1].length || 0;
};

Number.prototype.formatNumber = function (formatString) {
    'use strict';
    if (typeof numeral === 'undefined') {
        alert('please include numeral.js');
        return '';
    }

    if (typeof formatString === 'undefined') {
        formatString = "0,0.00";
    }

    return numeral(this).format(formatString);
};

Number.prototype.formatNegative = function (formatString) {
    'use strict';
    var formattedValue = this.formatNumber(formatString);
    return this < 0 ? '<div class="red-text">{0}</div>'.format(formattedValue) : formattedValue;
};

Number.MinInt = -9007199254740992;
Number.MaxInt = 9007199254740992;
