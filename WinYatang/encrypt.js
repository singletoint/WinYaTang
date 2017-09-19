﻿package YatangJS{
    class encrypt{
        function encode64(e) {
            var t = "",
            n, r, i = "",
            s, o, u, a = "",
            f = 0;
            do n = e.charCodeAt(f++),
            r = e.charCodeAt(f++),
            i = e.charCodeAt(f++),
            s = n >> 2,
            o = (n & 3) << 4 | r >> 4,
            u = (r & 15) << 2 | i >> 6,
            a = i & 63,
            isNaN(r) ? u = a = 64 : isNaN(i) && (a = 64),
            t = t + keyStr.charAt(s) + keyStr.charAt(o) + keyStr.charAt(u) + keyStr.charAt(a),
            n = r = i = "",
            s = o = u = a = "";
            while (f < e.length);
            return t
        }
        function decode64(e) {
            var t = "",
            n, r, i = "",
            s, o, u, a = "",
            f = 0;
            if (e.length % 4 != 0) return "";
            var l = /[^A-Za-z0-9\+\/\=]/g;
            if (l.exec(e)) return "";
            do s = keyStr.indexOf(e.charAt(f++)),
            o = keyStr.indexOf(e.charAt(f++)),
            u = keyStr.indexOf(e.charAt(f++)),
            a = keyStr.indexOf(e.charAt(f++)),
            n = s << 2 | o >> 4,
            r = (o & 15) << 4 | u >> 2,
            i = (u & 3) << 6 | a,
            t += String.fromCharCode(n),
            u != 64 && (t += String.fromCharCode(r)),
            a != 64 && (t += String.fromCharCode(i)),
            n = r = i = "",
            s = o = u = a = "";
            while (f < e.length);
            return t
        }
        function utf16to8(e) {
            var t, n, r, i;
            t = "",
            r = e.length;
            for (n = 0; n < r; n++) i = e.charCodeAt(n),
            i >= 1 && i <= 127 ? t += e.charAt(n) : i > 2047 ? (t += String.fromCharCode(224 | i >> 12 & 15), t += String.fromCharCode(128 | i >> 6 & 63), t += String.fromCharCode(128 | i >> 0 & 63)) : (t += String.fromCharCode(192 | i >> 6 & 31), t += String.fromCharCode(128 | i >> 0 & 63));
            return t
        }
        function utf8to16(e) {
            var t, n, r, i, s, o;
            t = "",
            r = e.length,
            n = 0;
            while (n < r) {
                i = e.charCodeAt(n++);
                switch (i >> 4) {
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                    case 6:
                    case 7:
                        t += e.charAt(n - 1);
                        break;
                    case 12:
                    case 13:
                        s = e.charCodeAt(n++),
                        t += String.fromCharCode((i & 31) << 6 | s & 63);
                        break;
                    case 14:
                        s = e.charCodeAt(n++),
                        o = e.charCodeAt(n++),
                        t += String.fromCharCode((i & 15) << 12 | (s & 63) << 6 | (o & 63) << 0)
                }
            }
            return t
        }
        function long2str(e, t) {
            var n = e.length,
            r = n - 1 << 2;
            if (t) {
                var i = e[n - 1];
                if (i < r - 3 || i > r) return null;
                r = i
            }
            for (var s = 0; s < n; s++) e[s] = String.fromCharCode(e[s] & 255, e[s] >>> 8 & 255, e[s] >>> 16 & 255, e[s] >>> 24 & 255);
            return t ? e.join("").substring(0, r) : e.join("")
        }
        function str2long(e, t) {
            var n = e.length,
            r = [];
            for (var i = 0; i < n; i += 4) r[i >> 2] = e.charCodeAt(i) | e.charCodeAt(i + 1) << 8 | e.charCodeAt(i + 2) << 16 | e.charCodeAt(i + 3) << 24;
            return t && (r[r.length] = n),
            r
        }
        function xxtea_encrypt(e, t) {
            if (e == "") return "";
            var n = str2long(e, !0),
            r = str2long(t, !1);
            r.length < 4 && (r.length = 4);
            var i = n.length - 1,
            s = n[i],
            o = n[0],
            u = 2654435769,
            a,
            f,
            l,
            c = Math.floor(6 + 52 / (i + 1)),
            h = 0;
            while (0 < c--) {
                h = h + u & 4294967295,
                f = h >>> 2 & 3;
                for (l = 0; l < i; l++) o = n[l + 1],
                a = (s >>> 5 ^ o << 2) + (o >>> 3 ^ s << 4) ^ (h ^ o) + (r[l & 3 ^ f] ^ s),
                s = n[l] = n[l] + a & 4294967295;
                o = n[0],
                a = (s >>> 5 ^ o << 2) + (o >>> 3 ^ s << 4) ^ (h ^ o) + (r[l & 3 ^ f] ^ s),
                s = n[i] = n[i] + a & 4294967295
            }
            return long2str(n, !1)
        }
        function xxtea_decrypt(e, t) {
            if (e == "") return "";
            var n = str2long(e, !1),
            r = str2long(t, !1);
            r.length < 4 && (r.length = 4);
            var i = n.length - 1,
            s = n[i - 1],
            o = n[0],
            u = 2654435769,
            a,
            f,
            l,
            c = Math.floor(6 + 52 / (i + 1)),
            h = c * u & 4294967295;
            while (h != 0) {
                f = h >>> 2 & 3;
                for (l = i; l > 0; l--) s = n[l - 1],
                a = (s >>> 5 ^ o << 2) + (o >>> 3 ^ s << 4) ^ (h ^ o) + (r[l & 3 ^ f] ^ s),
                o = n[l] = n[l] - a & 4294967295;
                s = n[i],
                a = (s >>> 5 ^ o << 2) + (o >>> 3 ^ s << 4) ^ (h ^ o) + (r[l & 3 ^ f] ^ s),
                o = n[0] = n[0] - a & 4294967295,
                h = h - u & 4294967295
            }
            return long2str(n, !0)
        }
        var keyStr = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=";

        var hexcase = 0;
        function hex_md5(a) {
            return rstr2hex(rstr_md5(str2rstr_utf8(a)))
        }
        function hex_hmac_md5(a, b) {
            return rstr2hex(rstr_hmac_md5(str2rstr_utf8(a), str2rstr_utf8(b)))
        }
        function md5_vm_test() {
            return hex_md5("abc").toLowerCase() == "900150983cd24fb0d6963f7d28e17f72"
        }
        function rstr_md5(a) {
            return binl2rstr(binl_md5(rstr2binl(a), a.length * 8))
        }
        function rstr_hmac_md5(c, f) {
            var e = rstr2binl(c);
            if (e.length > 16) {
                e = binl_md5(e, c.length * 8)
            }
            var a = Array(16),
            d = Array(16);
            for (var b = 0; b < 16; b++) {
                a[b] = e[b] ^ 909522486;
                d[b] = e[b] ^ 1549556828
            }
            var g = binl_md5(a.concat(rstr2binl(f)), 512 + f.length * 8);
            return binl2rstr(binl_md5(d.concat(g), 512 + 128))
        }
        function rstr2hex(c) {
            try {
                hexcase
            } catch (g) {
                hexcase = 0
            }
            var f = hexcase ? "0123456789ABCDEF" : "0123456789abcdef";
            var b = "";
            var a;
            for (var d = 0; d < c.length; d++) {
                a = c.charCodeAt(d);
                b += f.charAt((a >>> 4) & 15) + f.charAt(a & 15)
            }
            return b
        }
        function str2rstr_utf8(c) {
            var b = "";
            var d = -1;
            var a, e;
            while (++d < c.length) {
                a = c.charCodeAt(d);
                e = d + 1 < c.length ? c.charCodeAt(d + 1) : 0;
                if (55296 <= a && a <= 56319 && 56320 <= e && e <= 57343) {
                    a = 65536 + ((a & 1023) << 10) + (e & 1023);
                    d++
                }
                if (a <= 127) {
                    b += String.fromCharCode(a)
                } else {
                    if (a <= 2047) {
                        b += String.fromCharCode(192 | ((a >>> 6) & 31), 128 | (a & 63))
                    } else {
                        if (a <= 65535) {
                            b += String.fromCharCode(224 | ((a >>> 12) & 15), 128 | ((a >>> 6) & 63), 128 | (a & 63))
                        } else {
                            if (a <= 2097151) {
                                b += String.fromCharCode(240 | ((a >>> 18) & 7), 128 | ((a >>> 12) & 63), 128 | ((a >>> 6) & 63), 128 | (a & 63))
                            }
                        }
                    }
                }
            }
            return b
        }
        function rstr2binl(b) {
            var a = Array(b.length >> 2);
            for (var c = 0; c < a.length; c++) {
                a[c] = 0
            }
            for (var c = 0; c < b.length * 8; c += 8) {
                a[c >> 5] |= (b.charCodeAt(c / 8) & 255) << (c % 32)
            }
            return a
        }
        function binl2rstr(b) {
            var a = "";
            for (var c = 0; c < b.length * 32; c += 8) {
                a += String.fromCharCode((b[c >> 5] >>> (c % 32)) & 255)
            }
            return a
        }
        function binl_md5(p, k) {
            p[k >> 5] |= 128 << ((k) % 32);
            p[(((k + 64) >>> 9) << 4) + 14] = k;
            var o = 1732584193;
            var n = -271733879;
            var m = -1732584194;
            var l = 271733878;
            for (var g = 0; g < p.length; g += 16) {
                var j = o;
                var h = n;
                var f = m;
                var e = l;
                o = md5_ff(o, n, m, l, p[g + 0], 7, -680876936);
                l = md5_ff(l, o, n, m, p[g + 1], 12, -389564586);
                m = md5_ff(m, l, o, n, p[g + 2], 17, 606105819);
                n = md5_ff(n, m, l, o, p[g + 3], 22, -1044525330);
                o = md5_ff(o, n, m, l, p[g + 4], 7, -176418897);
                l = md5_ff(l, o, n, m, p[g + 5], 12, 1200080426);
                m = md5_ff(m, l, o, n, p[g + 6], 17, -1473231341);
                n = md5_ff(n, m, l, o, p[g + 7], 22, -45705983);
                o = md5_ff(o, n, m, l, p[g + 8], 7, 1770035416);
                l = md5_ff(l, o, n, m, p[g + 9], 12, -1958414417);
                m = md5_ff(m, l, o, n, p[g + 10], 17, -42063);
                n = md5_ff(n, m, l, o, p[g + 11], 22, -1990404162);
                o = md5_ff(o, n, m, l, p[g + 12], 7, 1804603682);
                l = md5_ff(l, o, n, m, p[g + 13], 12, -40341101);
                m = md5_ff(m, l, o, n, p[g + 14], 17, -1502002290);
                n = md5_ff(n, m, l, o, p[g + 15], 22, 1236535329);
                o = md5_gg(o, n, m, l, p[g + 1], 5, -165796510);
                l = md5_gg(l, o, n, m, p[g + 6], 9, -1069501632);
                m = md5_gg(m, l, o, n, p[g + 11], 14, 643717713);
                n = md5_gg(n, m, l, o, p[g + 0], 20, -373897302);
                o = md5_gg(o, n, m, l, p[g + 5], 5, -701558691);
                l = md5_gg(l, o, n, m, p[g + 10], 9, 38016083);
                m = md5_gg(m, l, o, n, p[g + 15], 14, -660478335);
                n = md5_gg(n, m, l, o, p[g + 4], 20, -405537848);
                o = md5_gg(o, n, m, l, p[g + 9], 5, 568446438);
                l = md5_gg(l, o, n, m, p[g + 14], 9, -1019803690);
                m = md5_gg(m, l, o, n, p[g + 3], 14, -187363961);
                n = md5_gg(n, m, l, o, p[g + 8], 20, 1163531501);
                o = md5_gg(o, n, m, l, p[g + 13], 5, -1444681467);
                l = md5_gg(l, o, n, m, p[g + 2], 9, -51403784);
                m = md5_gg(m, l, o, n, p[g + 7], 14, 1735328473);
                n = md5_gg(n, m, l, o, p[g + 12], 20, -1926607734);
                o = md5_hh(o, n, m, l, p[g + 5], 4, -378558);
                l = md5_hh(l, o, n, m, p[g + 8], 11, -2022574463);
                m = md5_hh(m, l, o, n, p[g + 11], 16, 1839030562);
                n = md5_hh(n, m, l, o, p[g + 14], 23, -35309556);
                o = md5_hh(o, n, m, l, p[g + 1], 4, -1530992060);
                l = md5_hh(l, o, n, m, p[g + 4], 11, 1272893353);
                m = md5_hh(m, l, o, n, p[g + 7], 16, -155497632);
                n = md5_hh(n, m, l, o, p[g + 10], 23, -1094730640);
                o = md5_hh(o, n, m, l, p[g + 13], 4, 681279174);
                l = md5_hh(l, o, n, m, p[g + 0], 11, -358537222);
                m = md5_hh(m, l, o, n, p[g + 3], 16, -722521979);
                n = md5_hh(n, m, l, o, p[g + 6], 23, 76029189);
                o = md5_hh(o, n, m, l, p[g + 9], 4, -640364487);
                l = md5_hh(l, o, n, m, p[g + 12], 11, -421815835);
                m = md5_hh(m, l, o, n, p[g + 15], 16, 530742520);
                n = md5_hh(n, m, l, o, p[g + 2], 23, -995338651);
                o = md5_ii(o, n, m, l, p[g + 0], 6, -198630844);
                l = md5_ii(l, o, n, m, p[g + 7], 10, 1126891415);
                m = md5_ii(m, l, o, n, p[g + 14], 15, -1416354905);
                n = md5_ii(n, m, l, o, p[g + 5], 21, -57434055);
                o = md5_ii(o, n, m, l, p[g + 12], 6, 1700485571);
                l = md5_ii(l, o, n, m, p[g + 3], 10, -1894986606);
                m = md5_ii(m, l, o, n, p[g + 10], 15, -1051523);
                n = md5_ii(n, m, l, o, p[g + 1], 21, -2054922799);
                o = md5_ii(o, n, m, l, p[g + 8], 6, 1873313359);
                l = md5_ii(l, o, n, m, p[g + 15], 10, -30611744);
                m = md5_ii(m, l, o, n, p[g + 6], 15, -1560198380);
                n = md5_ii(n, m, l, o, p[g + 13], 21, 1309151649);
                o = md5_ii(o, n, m, l, p[g + 4], 6, -145523070);
                l = md5_ii(l, o, n, m, p[g + 11], 10, -1120210379);
                m = md5_ii(m, l, o, n, p[g + 2], 15, 718787259);
                n = md5_ii(n, m, l, o, p[g + 9], 21, -343485551);
                o = safe_add(o, j);
                n = safe_add(n, h);
                m = safe_add(m, f);
                l = safe_add(l, e)
            }
            return Array(o, n, m, l)
        }
        function md5_cmn(h, e, d, c, g, f) {
            return safe_add(bit_rol(safe_add(safe_add(e, h), safe_add(c, f)), g), d)
        }
        function md5_ff(g, f, k, j, e, i, h) {
            return md5_cmn((f & k) | ((~f) & j), g, f, e, i, h)
        }
        function md5_gg(g, f, k, j, e, i, h) {
            return md5_cmn((f & j) | (k & (~j)), g, f, e, i, h)
        }
        function md5_hh(g, f, k, j, e, i, h) {
            return md5_cmn(f ^ k ^ j, g, f, e, i, h)
        }
        function md5_ii(g, f, k, j, e, i, h) {
            return md5_cmn(k ^ (f | (~j)), g, f, e, i, h)
        }
        function safe_add(a, d) {
            var c = (a & 65535) + (d & 65535);
            var b = (a >> 16) + (d >> 16) + (c >> 16);
            return (b << 16) | (c & 65535)
        }
        function bit_rol(a, b) {
            return (a << b) | (a >>> (32 - b))
        };
    }
}