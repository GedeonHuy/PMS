webpackJsonp([22,26],{

/***/ 146:
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__(0);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__angular_router__ = __webpack_require__(20);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__angular_http__ = __webpack_require__(66);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_3__authen_service__ = __webpack_require__(80);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return UtilityService; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};




var UtilityService = (function () {
    function UtilityService(router, http, authenService) {
        this.http = http;
        this.authenService = authenService;
        this.Unflatten = function (arr) {
            var map = {};
            var roots = [];
            for (var i = 0; i < arr.length; i += 1) {
                var node = arr[i];
                node.children = [];
                map[node.ID] = i; // use map to look-up the parents
                if (node.ParentId !== null) {
                    arr[map[node.ParentId]].children.push(node);
                }
                else {
                    roots.push(node);
                }
            }
            return roots;
        };
        this.Unflatten2 = function (arr) {
            var map = {};
            var roots = [];
            for (var i = 0; i < arr.length; i += 1) {
                var node = arr[i];
                node.children = [];
                map[node.ID] = i; // use map to look-up the parents
                if (node.ParentID !== null) {
                    arr[map[node.ParentID]].children.push(node);
                }
                else {
                    roots.push(node);
                }
            }
            return roots;
        };
        this._router = router;
    }
    UtilityService.prototype.convertDateTime = function (date) {
        var _formattedDate = new Date(date.toString());
        return _formattedDate.toDateString();
    };
    UtilityService.prototype.navigate = function (path) {
        this._router.navigate([path]);
    };
    UtilityService.prototype.navigateToLogin = function () {
        this._router.navigate(["/login"]);
    };
    UtilityService.prototype.MakeSeoTitle = function (input) {
        if (input == undefined || input == '')
            return '';
        //Đổi chữ hoa thành chữ thường
        var slug = input.toLowerCase();
        //Đổi ký tự có dấu thành không dấu
        slug = slug.replace(/á|à|ả|ạ|ã|ă|ắ|ằ|ẳ|ẵ|ặ|â|ấ|ầ|ẩ|ẫ|ậ/gi, 'a');
        slug = slug.replace(/é|è|ẻ|ẽ|ẹ|ê|ế|ề|ể|ễ|ệ/gi, 'e');
        slug = slug.replace(/i|í|ì|ỉ|ĩ|ị/gi, 'i');
        slug = slug.replace(/ó|ò|ỏ|õ|ọ|ô|ố|ồ|ổ|ỗ|ộ|ơ|ớ|ờ|ở|ỡ|ợ/gi, 'o');
        slug = slug.replace(/ú|ù|ủ|ũ|ụ|ư|ứ|ừ|ử|ữ|ự/gi, 'u');
        slug = slug.replace(/ý|ỳ|ỷ|ỹ|ỵ/gi, 'y');
        slug = slug.replace(/đ/gi, 'd');
        //Xóa các ký tự đặt biệt
        slug = slug.replace(/\`|\~|\!|\@|\#|\||\$|\%|\^|\&|\*|\(|\)|\+|\=|\,|\.|\/|\?|\>|\<|\'|\"|\:|\;|_/gi, '');
        //Đổi khoảng trắng thành ký tự gạch ngang
        slug = slug.replace(/ /gi, "-");
        //Đổi nhiều ký tự gạch ngang liên tiếp thành 1 ký tự gạch ngang
        //Phòng trường hợp người nhập vào quá nhiều ký tự trắng
        slug = slug.replace(/\-\-\-\-\-/gi, '-');
        slug = slug.replace(/\-\-\-\-/gi, '-');
        slug = slug.replace(/\-\-\-/gi, '-');
        slug = slug.replace(/\-\-/gi, '-');
        //Xóa các ký tự gạch ngang ở đầu và cuối
        slug = '@' + slug + '@';
        slug = slug.replace(/\@\-|\-\@|\@/gi, '');
        return slug;
    };
    return UtilityService;
}());
UtilityService = __decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["Injectable"])(),
    __metadata("design:paramtypes", [typeof (_a = typeof __WEBPACK_IMPORTED_MODULE_1__angular_router__["b" /* Router */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_1__angular_router__["b" /* Router */]) === "function" && _a || Object, typeof (_b = typeof __WEBPACK_IMPORTED_MODULE_2__angular_http__["c" /* Http */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_2__angular_http__["c" /* Http */]) === "function" && _b || Object, typeof (_c = typeof __WEBPACK_IMPORTED_MODULE_3__authen_service__["a" /* AuthenService */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_3__authen_service__["a" /* AuthenService */]) === "function" && _c || Object])
], UtilityService);

var _a, _b, _c;
//# sourceMappingURL=utility.service.js.map

/***/ }),

/***/ 172:
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__(0);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__theme_configProvider__ = __webpack_require__(173);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return BaThemeConfig; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};


var BaThemeConfig = (function () {
    function BaThemeConfig(_baConfig) {
        this._baConfig = _baConfig;
    }
    BaThemeConfig.prototype.config = function () {
        // this._baConfig.changeTheme({ name: 'my-theme' });
        //
        // let colorScheme = {
        //   primary: '#209e91',
        //   info: '#2dacd1',
        //   success: '#90b900',
        //   warning: '#dfb81c',
        //   danger: '#e85656',
        // };
        //
        // this._baConfig.changeColors({
        //   default: '#4e4e55',
        //   defaultText: '#aaaaaa',
        //   border: '#dddddd',
        //   borderDark: '#aaaaaa',
        //
        //   primary: colorScheme.primary,
        //   info: colorScheme.info,
        //   success: colorScheme.success,
        //   warning: colorScheme.warning,
        //   danger: colorScheme.danger,
        //
        //   primaryLight: colorHelper.tint(colorScheme.primary, 30),
        //   infoLight: colorHelper.tint(colorScheme.info, 30),
        //   successLight: colorHelper.tint(colorScheme.success, 30),
        //   warningLight: colorHelper.tint(colorScheme.warning, 30),
        //   dangerLight: colorHelper.tint(colorScheme.danger, 30),
        //
        //   primaryDark: colorHelper.shade(colorScheme.primary, 15),
        //   infoDark: colorHelper.shade(colorScheme.info, 15),
        //   successDark: colorHelper.shade(colorScheme.success, 15),
        //   warningDark: colorHelper.shade(colorScheme.warning, 15),
        //   dangerDark: colorHelper.shade(colorScheme.danger, 15),
        //
        //   dashboard: {
        //     blueStone: '#005562',
        //     surfieGreen: '#0e8174',
        //     silverTree: '#6eba8c',
        //     gossip: '#b9f2a1',
        //     white: '#10c4b5',
        //   },
        //
        //   custom: {
        //     dashboardPieChart: colorHelper.hexToRgbA('#aaaaaa', 0.8),
        //     dashboardLineChart: '#6eba8c',
        //   },
        // });
    };
    return BaThemeConfig;
}());
BaThemeConfig = __decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["Injectable"])(),
    __metadata("design:paramtypes", [typeof (_a = typeof __WEBPACK_IMPORTED_MODULE_1__theme_configProvider__["a" /* BaThemeConfigProvider */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_1__theme_configProvider__["a" /* BaThemeConfigProvider */]) === "function" && _a || Object])
], BaThemeConfig);

var _a;
//# sourceMappingURL=theme.config.js.map

/***/ }),

/***/ 173:
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__(0);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1_lodash__ = __webpack_require__(204);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1_lodash___default = __webpack_require__.n(__WEBPACK_IMPORTED_MODULE_1_lodash__);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__theme_constants__ = __webpack_require__(264);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return BaThemeConfigProvider; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};



var BaThemeConfigProvider = (function () {
    function BaThemeConfigProvider() {
        this.basic = {
            default: '#ffffff',
            defaultText: '#ffffff',
            border: '#dddddd',
            borderDark: '#aaaaaa',
        };
        // main functional color scheme
        this.colorScheme = {
            primary: '#00abff',
            info: '#40daf1',
            success: '#8bd22f',
            warning: '#e7ba08',
            danger: '#f95372',
        };
        // dashboard colors for charts
        this.dashboardColors = {
            blueStone: '#40daf1',
            surfieGreen: '#00abff',
            silverTree: '#1b70ef',
            gossip: '#3c4eb9',
            white: '#ffffff',
        };
        this.conf = {
            theme: {
                name: 'ng2',
            },
            colors: {
                default: this.basic.default,
                defaultText: this.basic.defaultText,
                border: this.basic.border,
                borderDark: this.basic.borderDark,
                primary: this.colorScheme.primary,
                info: this.colorScheme.info,
                success: this.colorScheme.success,
                warning: this.colorScheme.warning,
                danger: this.colorScheme.danger,
                primaryLight: __WEBPACK_IMPORTED_MODULE_2__theme_constants__["b" /* colorHelper */].tint(this.colorScheme.primary, 30),
                infoLight: __WEBPACK_IMPORTED_MODULE_2__theme_constants__["b" /* colorHelper */].tint(this.colorScheme.info, 30),
                successLight: __WEBPACK_IMPORTED_MODULE_2__theme_constants__["b" /* colorHelper */].tint(this.colorScheme.success, 30),
                warningLight: __WEBPACK_IMPORTED_MODULE_2__theme_constants__["b" /* colorHelper */].tint(this.colorScheme.warning, 30),
                dangerLight: __WEBPACK_IMPORTED_MODULE_2__theme_constants__["b" /* colorHelper */].tint(this.colorScheme.danger, 30),
                primaryDark: __WEBPACK_IMPORTED_MODULE_2__theme_constants__["b" /* colorHelper */].shade(this.colorScheme.primary, 15),
                infoDark: __WEBPACK_IMPORTED_MODULE_2__theme_constants__["b" /* colorHelper */].shade(this.colorScheme.info, 15),
                successDark: __WEBPACK_IMPORTED_MODULE_2__theme_constants__["b" /* colorHelper */].shade(this.colorScheme.success, 15),
                warningDark: __WEBPACK_IMPORTED_MODULE_2__theme_constants__["b" /* colorHelper */].shade(this.colorScheme.warning, 15),
                dangerDark: __WEBPACK_IMPORTED_MODULE_2__theme_constants__["b" /* colorHelper */].shade(this.colorScheme.danger, 15),
                dashboard: {
                    blueStone: this.dashboardColors.blueStone,
                    surfieGreen: this.dashboardColors.surfieGreen,
                    silverTree: this.dashboardColors.silverTree,
                    gossip: this.dashboardColors.gossip,
                    white: this.dashboardColors.white,
                },
                custom: {
                    dashboardPieChart: __WEBPACK_IMPORTED_MODULE_2__theme_constants__["b" /* colorHelper */].hexToRgbA(this.basic.defaultText, 0.8),
                    dashboardLineChart: this.basic.defaultText,
                }
            }
        };
    }
    BaThemeConfigProvider.prototype.get = function () {
        return this.conf;
    };
    BaThemeConfigProvider.prototype.changeTheme = function (theme) {
        __WEBPACK_IMPORTED_MODULE_1_lodash__["merge"](this.get().theme, theme);
    };
    BaThemeConfigProvider.prototype.changeColors = function (colors) {
        __WEBPACK_IMPORTED_MODULE_1_lodash__["merge"](this.get().colors, colors);
    };
    return BaThemeConfigProvider;
}());
BaThemeConfigProvider = __decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["Injectable"])(),
    __metadata("design:paramtypes", [])
], BaThemeConfigProvider);

//# sourceMappingURL=theme.configProvider.js.map

/***/ }),

/***/ 202:
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__(0);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__angular_common__ = __webpack_require__(13);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__angular_forms__ = __webpack_require__(22);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_3__angular_router__ = __webpack_require__(20);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_4_ngx_uploader__ = __webpack_require__(199);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_5__app_translation_module__ = __webpack_require__(207);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_6__theme_config__ = __webpack_require__(172);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_7__theme_configProvider__ = __webpack_require__(173);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_8__components__ = __webpack_require__(563);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_9__components_baCard_baCardBlur_directive__ = __webpack_require__(535);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_10__directives__ = __webpack_require__(570);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_11__pipes__ = __webpack_require__(577);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_12__services__ = __webpack_require__(71);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_13__validators__ = __webpack_require__(510);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return NgaModule; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};














var NGA_COMPONENTS = [
    __WEBPACK_IMPORTED_MODULE_8__components__["a" /* BaAmChart */],
    __WEBPACK_IMPORTED_MODULE_8__components__["b" /* BaBackTop */],
    __WEBPACK_IMPORTED_MODULE_8__components__["c" /* BaCard */],
    __WEBPACK_IMPORTED_MODULE_8__components__["d" /* BaChartistChart */],
    __WEBPACK_IMPORTED_MODULE_8__components__["e" /* BaCheckbox */],
    __WEBPACK_IMPORTED_MODULE_8__components__["f" /* BaContentTop */],
    __WEBPACK_IMPORTED_MODULE_8__components__["g" /* BaFullCalendar */],
    __WEBPACK_IMPORTED_MODULE_8__components__["h" /* BaMenuItem */],
    __WEBPACK_IMPORTED_MODULE_8__components__["i" /* BaMenu */],
    __WEBPACK_IMPORTED_MODULE_8__components__["j" /* BaMsgCenter */],
    __WEBPACK_IMPORTED_MODULE_8__components__["k" /* BaMultiCheckbox */],
    __WEBPACK_IMPORTED_MODULE_8__components__["l" /* BaPageTop */],
    __WEBPACK_IMPORTED_MODULE_8__components__["m" /* BaPictureUploader */],
    __WEBPACK_IMPORTED_MODULE_8__components__["n" /* BaSidebar */],
    __WEBPACK_IMPORTED_MODULE_8__components__["o" /* BaFileUploader */]
];
var NGA_DIRECTIVES = [
    __WEBPACK_IMPORTED_MODULE_10__directives__["a" /* BaScrollPosition */],
    __WEBPACK_IMPORTED_MODULE_10__directives__["b" /* BaSlimScroll */],
    __WEBPACK_IMPORTED_MODULE_10__directives__["c" /* BaThemeRun */],
    __WEBPACK_IMPORTED_MODULE_9__components_baCard_baCardBlur_directive__["a" /* BaCardBlur */]
];
var NGA_PIPES = [
    __WEBPACK_IMPORTED_MODULE_11__pipes__["a" /* BaAppPicturePipe */],
    __WEBPACK_IMPORTED_MODULE_11__pipes__["b" /* BaKameleonPicturePipe */],
    __WEBPACK_IMPORTED_MODULE_11__pipes__["c" /* BaProfilePicturePipe */]
];
var NGA_SERVICES = [
    __WEBPACK_IMPORTED_MODULE_12__services__["b" /* BaImageLoaderService */],
    __WEBPACK_IMPORTED_MODULE_12__services__["c" /* BaThemePreloader */],
    __WEBPACK_IMPORTED_MODULE_12__services__["d" /* BaThemeSpinner */],
    __WEBPACK_IMPORTED_MODULE_12__services__["a" /* BaMenuService */]
];
var NGA_VALIDATORS = [
    __WEBPACK_IMPORTED_MODULE_13__validators__["a" /* EmailValidator */],
    __WEBPACK_IMPORTED_MODULE_13__validators__["b" /* EqualPasswordsValidator */]
];
var NgaModule = NgaModule_1 = (function () {
    function NgaModule() {
    }
    NgaModule.forRoot = function () {
        return {
            ngModule: NgaModule_1,
            providers: [
                __WEBPACK_IMPORTED_MODULE_7__theme_configProvider__["a" /* BaThemeConfigProvider */],
                __WEBPACK_IMPORTED_MODULE_6__theme_config__["a" /* BaThemeConfig */]
            ].concat(NGA_VALIDATORS, NGA_SERVICES),
        };
    };
    return NgaModule;
}());
NgaModule = NgaModule_1 = __decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["NgModule"])({
        declarations: NGA_PIPES.concat(NGA_DIRECTIVES, NGA_COMPONENTS),
        imports: [
            __WEBPACK_IMPORTED_MODULE_1__angular_common__["CommonModule"],
            __WEBPACK_IMPORTED_MODULE_3__angular_router__["a" /* RouterModule */],
            __WEBPACK_IMPORTED_MODULE_2__angular_forms__["FormsModule"],
            __WEBPACK_IMPORTED_MODULE_2__angular_forms__["ReactiveFormsModule"],
            __WEBPACK_IMPORTED_MODULE_5__app_translation_module__["a" /* AppTranslationModule */],
            __WEBPACK_IMPORTED_MODULE_4_ngx_uploader__["a" /* NgUploaderModule */]
        ],
        exports: NGA_PIPES.concat(NGA_DIRECTIVES, NGA_COMPONENTS)
    })
], NgaModule);

var NgaModule_1;
//# sourceMappingURL=nga.module.js.map

/***/ }),

/***/ 207:
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__(0);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__angular_http__ = __webpack_require__(66);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__ngx_translate_core__ = __webpack_require__(266);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_3__ngx_translate_http_loader__ = __webpack_require__(592);
/* unused harmony export createTranslateLoader */
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return AppTranslationModule; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};





function createTranslateLoader(http) {
    return new __WEBPACK_IMPORTED_MODULE_3__ngx_translate_http_loader__["a" /* TranslateHttpLoader */](http, './assets/i18n/US/', '.json');
}
var translationOptions = {
    loader: {
        provide: __WEBPACK_IMPORTED_MODULE_2__ngx_translate_core__["a" /* TranslateLoader */],
        useFactory: (createTranslateLoader),
        deps: [__WEBPACK_IMPORTED_MODULE_1__angular_http__["c" /* Http */]]
    }
};
var AppTranslationModule = (function () {
    function AppTranslationModule(translate) {
        this.translate = translate;
        translate.addLangs(["en"]);
        translate.setDefaultLang('en');
        translate.use('en');
    }
    return AppTranslationModule;
}());
AppTranslationModule = __decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["NgModule"])({
        imports: [__WEBPACK_IMPORTED_MODULE_2__ngx_translate_core__["b" /* TranslateModule */].forRoot(translationOptions)],
        exports: [__WEBPACK_IMPORTED_MODULE_2__ngx_translate_core__["b" /* TranslateModule */]],
        providers: [__WEBPACK_IMPORTED_MODULE_2__ngx_translate_core__["c" /* TranslateService */]]
    }),
    __metadata("design:paramtypes", [typeof (_a = typeof __WEBPACK_IMPORTED_MODULE_2__ngx_translate_core__["c" /* TranslateService */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_2__ngx_translate_core__["c" /* TranslateService */]) === "function" && _a || Object])
], AppTranslationModule);

var _a;
//# sourceMappingURL=app.translation.module.js.map

/***/ }),

/***/ 216:
/***/ (function(module, exports, __webpack_require__) {

var map = {
	"./charts/charts.module": [
		824,
		18
	],
	"./components/components.module": [
		825,
		1
	],
	"./confirm-group/confirm-group.module": [
		826,
		5
	],
	"./dashboard/dashboard.module": [
		827,
		16
	],
	"./editors/editors.module": [
		828,
		17
	],
	"./forms/forms.module": [
		829,
		4
	],
	"./group-details/group-details.module": [
		830,
		2
	],
	"./group/group.module": [
		831,
		6
	],
	"./lecturer/lecturer.module": [
		832,
		13
	],
	"./main/main.module": [
		263
	],
	"./major/major.module": [
		834,
		12
	],
	"./maps/maps.module": [
		835,
		15
	],
	"./project/project.module": [
		836,
		7
	],
	"./quarter/quarter.module": [
		837,
		8
	],
	"./role/role.module": [
		839,
		11
	],
	"./student/student.module": [
		840,
		10
	],
	"./tables/tables.module": [
		841,
		0
	],
	"./tag-details/tag-details.module": [
		842,
		14
	],
	"./tag/tag.module": [
		843,
		9
	],
	"./ui/ui.module": [
		844,
		3
	],
	"app/main/login/login.module": [
		833,
		19
	],
	"app/main/register/register.module": [
		838,
		20
	]
};
function webpackAsyncContext(req) {
	var ids = map[req];	if(!ids)
		return Promise.reject(new Error("Cannot find module '" + req + "'."));
	return Promise.all(ids.slice(1).map(__webpack_require__.e)).then(function() {
		return __webpack_require__(ids[0]);
	});
};
webpackAsyncContext.keys = function webpackAsyncContextKeys() {
	return Object.keys(map);
};
module.exports = webpackAsyncContext;
webpackAsyncContext.id = 216;


/***/ }),

/***/ 262:
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__main_menuStudent__ = __webpack_require__(527);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__main_menuLecturer__ = __webpack_require__(526);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__main_menuAdmin__ = __webpack_require__(525);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_3__core_common_system_constants__ = __webpack_require__(33);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_4__core_services_utility_service__ = __webpack_require__(146);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_5__core_services_authen_service__ = __webpack_require__(80);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_6__angular_core__ = __webpack_require__(0);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_7__theme__ = __webpack_require__(34);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return Main; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};








var Main = (function () {
    function Main(_menuService, utilityService, authenService) {
        this._menuService = _menuService;
        this.utilityService = utilityService;
        this.authenService = authenService;
    }
    Main.prototype.ngOnInit = function () {
        this.user = JSON.parse(localStorage.getItem(__WEBPACK_IMPORTED_MODULE_3__core_common_system_constants__["a" /* SystemConstants */].CURRENT_USER));
        if (this.user.role == "Admin") {
            this._menuService.updateMenuByRoutes(__WEBPACK_IMPORTED_MODULE_2__main_menuAdmin__["a" /* PAGES_MENU_ADMIN */]);
        }
        if (this.user.role == "Lecturer") {
            this._menuService.updateMenuByRoutes(__WEBPACK_IMPORTED_MODULE_1__main_menuLecturer__["a" /* PAGES_MENU_LECTURER */]);
        }
        if (this.user.role == "Student") {
            this._menuService.updateMenuByRoutes(__WEBPACK_IMPORTED_MODULE_0__main_menuStudent__["a" /* PAGES_MENU_STUDENT */]);
        }
    };
    return Main;
}());
Main = __decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_6__angular_core__["Component"])({
        selector: 'pages',
        template: "\n    <ba-sidebar></ba-sidebar>\n    <ba-page-top></ba-page-top>\n    <div class=\"al-main\">\n      <div class=\"al-content\">\n        <ba-content-top></ba-content-top>\n        <router-outlet></router-outlet>\n      </div>\n    </div>\n    <footer class=\"al-footer clearfix\">\n      <div class=\"al-footer-main clearfix\">\n        <div class=\"al-copy\">&copy; <a href=\"http://eiu.edu.vn\" translate>{{'general.eiu'}}</a> 2018</div>\n      </div>\n    </footer>\n    <ba-back-top position=\"200\"></ba-back-top>\n    "
    }),
    __metadata("design:paramtypes", [typeof (_a = typeof __WEBPACK_IMPORTED_MODULE_7__theme__["c" /* BaMenuService */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_7__theme__["c" /* BaMenuService */]) === "function" && _a || Object, typeof (_b = typeof __WEBPACK_IMPORTED_MODULE_4__core_services_utility_service__["a" /* UtilityService */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_4__core_services_utility_service__["a" /* UtilityService */]) === "function" && _b || Object, typeof (_c = typeof __WEBPACK_IMPORTED_MODULE_5__core_services_authen_service__["a" /* AuthenService */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_5__core_services_authen_service__["a" /* AuthenService */]) === "function" && _c || Object])
], Main);

var _a, _b, _c;
//# sourceMappingURL=main.component.js.map

/***/ }),

/***/ 263:
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
Object.defineProperty(__webpack_exports__, "__esModule", { value: true });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__core_services_progress_service__ = __webpack_require__(469);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__core_guards_auth_admin_lecturer_guard__ = __webpack_require__(520);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__core_guards_auth_student_guard__ = __webpack_require__(523);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_3__core_guards_auth_lecturer_guard__ = __webpack_require__(522);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_4__core_guards_auth_admin_guard__ = __webpack_require__(521);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_5__core_services_utility_service__ = __webpack_require__(146);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_6__core_services_authen_service__ = __webpack_require__(80);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_7__angular_core__ = __webpack_require__(0);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_8__angular_common__ = __webpack_require__(13);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_9__main_routing__ = __webpack_require__(528);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_10__theme_nga_module__ = __webpack_require__(202);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_11__app_translation_module__ = __webpack_require__(207);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_12__main_component__ = __webpack_require__(262);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "MainModule", function() { return MainModule; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};













var MainModule = (function () {
    function MainModule() {
    }
    return MainModule;
}());
MainModule = __decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_7__angular_core__["NgModule"])({
        imports: [__WEBPACK_IMPORTED_MODULE_8__angular_common__["CommonModule"], __WEBPACK_IMPORTED_MODULE_11__app_translation_module__["a" /* AppTranslationModule */], __WEBPACK_IMPORTED_MODULE_10__theme_nga_module__["a" /* NgaModule */], __WEBPACK_IMPORTED_MODULE_9__main_routing__["a" /* routing */]],
        declarations: [__WEBPACK_IMPORTED_MODULE_12__main_component__["a" /* Main */]],
        providers: [
            __WEBPACK_IMPORTED_MODULE_0__core_services_progress_service__["a" /* ProgressService */],
            __WEBPACK_IMPORTED_MODULE_5__core_services_utility_service__["a" /* UtilityService */],
            __WEBPACK_IMPORTED_MODULE_6__core_services_authen_service__["a" /* AuthenService */],
            __WEBPACK_IMPORTED_MODULE_4__core_guards_auth_admin_guard__["a" /* AdminAuthGuard */],
            __WEBPACK_IMPORTED_MODULE_3__core_guards_auth_lecturer_guard__["a" /* LecturerAuthGuard */],
            __WEBPACK_IMPORTED_MODULE_1__core_guards_auth_admin_lecturer_guard__["a" /* AdminLecturerAuthGuard */],
            __WEBPACK_IMPORTED_MODULE_2__core_guards_auth_student_guard__["a" /* StudentAuthGuard */]
        ]
    })
], MainModule);

//# sourceMappingURL=main.module.js.map

/***/ }),

/***/ 264:
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* unused harmony export IMAGES_ROOT */
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "d", function() { return layoutSizes; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return layoutPaths; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "b", function() { return colorHelper; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "c", function() { return isMobile; });
var IMAGES_ROOT = 'assets/img/';
var layoutSizes = {
    resWidthCollapseSidebar: 1200,
    resWidthHideSidebar: 500
};
var layoutPaths = {
    images: {
        root: IMAGES_ROOT,
        profile: IMAGES_ROOT + 'app/profile/',
        amMap: 'assets/img/theme/vendor/ammap/',
        amChart: 'assets/img/theme/vendor/amcharts/dist/amcharts/images/'
    }
};
var colorHelper = (function () {
    function colorHelper() {
    }
    return colorHelper;
}());

colorHelper.shade = function (color, weight) {
    return colorHelper.mix('#000000', color, weight);
};
colorHelper.tint = function (color, weight) {
    return colorHelper.mix('#ffffff', color, weight);
};
colorHelper.hexToRgbA = function (hex, alpha) {
    var c;
    if (/^#([A-Fa-f0-9]{3}){1,2}$/.test(hex)) {
        c = hex.substring(1).split('');
        if (c.length == 3) {
            c = [c[0], c[0], c[1], c[1], c[2], c[2]];
        }
        c = '0x' + c.join('');
        return 'rgba(' + [(c >> 16) & 255, (c >> 8) & 255, c & 255].join(',') + ',' + alpha + ')';
    }
    throw new Error('Bad Hex');
};
colorHelper.mix = function (color1, color2, weight) {
    var d2h = function (d) { return d.toString(16); };
    var h2d = function (h) { return parseInt(h, 16); };
    var result = "#";
    for (var i = 1; i < 7; i += 2) {
        var color1Part = h2d(color1.substr(i, 2));
        var color2Part = h2d(color2.substr(i, 2));
        var resultPart = d2h(Math.floor(color2Part + (color1Part - color2Part) * (weight / 100.0)));
        result += ('0' + resultPart).slice(-2);
    }
    return result;
};
var isMobile = function () { return (/android|webos|iphone|ipad|ipod|blackberry|windows phone/).test(navigator.userAgent.toLowerCase()); };
//# sourceMappingURL=theme.constants.js.map

/***/ }),

/***/ 33:
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return SystemConstants; });
var SystemConstants = (function () {
    function SystemConstants() {
    }
    return SystemConstants;
}());

SystemConstants.CURRENT_USER = "CURRENT_USER";
//public static BASE_URL = "http://pmseiuvodoi.azurewebsites.net/";
SystemConstants.BASE_URL = "http://localhost:8000";
SystemConstants.GITHUB_API_URL = "https://api.github.com/repos/";
SystemConstants.PAGE_SIZE = 5;
//# sourceMappingURL=system.constants.js.map

/***/ }),

/***/ 34:
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__services__ = __webpack_require__(71);
/* harmony reexport (binding) */ __webpack_require__.d(__webpack_exports__, "c", function() { return __WEBPACK_IMPORTED_MODULE_0__services__["a"]; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__theme_constants__ = __webpack_require__(264);
/* harmony namespace reexport (by used) */ __webpack_require__.d(__webpack_exports__, "a", function() { return __WEBPACK_IMPORTED_MODULE_1__theme_constants__["a"]; });
/* harmony namespace reexport (by used) */ __webpack_require__.d(__webpack_exports__, "d", function() { return __WEBPACK_IMPORTED_MODULE_1__theme_constants__["c"]; });
/* harmony namespace reexport (by used) */ __webpack_require__.d(__webpack_exports__, "e", function() { return __WEBPACK_IMPORTED_MODULE_1__theme_constants__["d"]; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__theme_configProvider__ = __webpack_require__(173);
/* harmony namespace reexport (by used) */ __webpack_require__.d(__webpack_exports__, "b", function() { return __WEBPACK_IMPORTED_MODULE_2__theme_configProvider__["a"]; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_3__theme_config__ = __webpack_require__(172);
/* unused harmony namespace reexport */




//# sourceMappingURL=index.js.map

/***/ }),

/***/ 439:
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
Object.defineProperty(__webpack_exports__, "__esModule", { value: true });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__(0);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__angular_platform_browser_dynamic__ = __webpack_require__(512);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__environments_environment__ = __webpack_require__(588);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_3__app_app_module__ = __webpack_require__(517);




if (__WEBPACK_IMPORTED_MODULE_2__environments_environment__["a" /* environment */].production) {
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["enableProdMode"])();
}
__webpack_require__.i(__WEBPACK_IMPORTED_MODULE_1__angular_platform_browser_dynamic__["a" /* platformBrowserDynamic */])().bootstrapModule(__WEBPACK_IMPORTED_MODULE_3__app_app_module__["a" /* AppModule */]);
//# sourceMappingURL=main.js.map

/***/ }),

/***/ 469:
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__(0);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1_rxjs_Subject__ = __webpack_require__(32);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1_rxjs_Subject___default = __webpack_require__.n(__WEBPACK_IMPORTED_MODULE_1_rxjs_Subject__);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__angular_http__ = __webpack_require__(66);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return ProgressService; });
/* unused harmony export BrowserXhrWithProgress */
var __extends = (this && this.__extends) || (function () {
    var extendStatics = Object.setPrototypeOf ||
        ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
        function (d, b) { for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p]; };
    return function (d, b) {
        extendStatics(d, b);
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
})();
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};



var ProgressService = (function () {
    function ProgressService() {
        this.uploadProgress = new __WEBPACK_IMPORTED_MODULE_1_rxjs_Subject__["Subject"]();
        this.downloadProgress = new __WEBPACK_IMPORTED_MODULE_1_rxjs_Subject__["Subject"]();
    }
    return ProgressService;
}());
ProgressService = __decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["Injectable"])()
], ProgressService);

//XMLHttpRequest
var BrowserXhrWithProgress = (function (_super) {
    __extends(BrowserXhrWithProgress, _super);
    function BrowserXhrWithProgress(service) {
        var _this = _super.call(this) || this;
        _this.service = service;
        return _this;
    }
    BrowserXhrWithProgress.prototype.build = function () {
        var _this = this;
        var xhr = _super.prototype.build.call(this);
        xhr.onprogress = function (event) {
            _this.service.downloadProgress.next(_this.createProgress(event));
        };
        xhr.upload.onprogress = function (event) {
            _this.service.uploadProgress.next(_this.createProgress(event));
        };
        return xhr;
    };
    BrowserXhrWithProgress.prototype.createProgress = function (event) {
        return {
            total: event.total,
            percentage: Math.round(event.loaded / event.total * 100)
        };
    };
    return BrowserXhrWithProgress;
}(__WEBPACK_IMPORTED_MODULE_2__angular_http__["f" /* BrowserXhr */]));
BrowserXhrWithProgress = __decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["Injectable"])(),
    __metadata("design:paramtypes", [ProgressService])
], BrowserXhrWithProgress);

//# sourceMappingURL=progress.service.js.map

/***/ }),

/***/ 510:
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__email_validator__ = __webpack_require__(586);
/* harmony namespace reexport (by used) */ __webpack_require__.d(__webpack_exports__, "a", function() { return __WEBPACK_IMPORTED_MODULE_0__email_validator__["a"]; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__equalPasswords_validator__ = __webpack_require__(587);
/* harmony namespace reexport (by used) */ __webpack_require__.d(__webpack_exports__, "b", function() { return __WEBPACK_IMPORTED_MODULE_1__equalPasswords_validator__["a"]; });


//# sourceMappingURL=index.js.map

/***/ }),

/***/ 516:
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__(0);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__global_state__ = __webpack_require__(60);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__theme_services__ = __webpack_require__(71);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_3__theme_theme_config__ = __webpack_require__(172);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return App; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};




/*
 * App Component
 * Top Level Component
 */
var App = (function () {
    function App(_state, _imageLoader, _spinner, viewContainerRef, themeConfig) {
        var _this = this;
        this._state = _state;
        this._imageLoader = _imageLoader;
        this._spinner = _spinner;
        this.viewContainerRef = viewContainerRef;
        this.themeConfig = themeConfig;
        this.isMenuCollapsed = false;
        themeConfig.config();
        this._loadImages();
        this._state.subscribe('menu.isCollapsed', function (isCollapsed) {
            _this.isMenuCollapsed = isCollapsed;
        });
    }
    App.prototype.ngAfterViewInit = function () {
        var _this = this;
        // hide spinner once all loaders are completed
        __WEBPACK_IMPORTED_MODULE_2__theme_services__["c" /* BaThemePreloader */].load().then(function (values) {
            _this._spinner.hide();
        });
    };
    App.prototype._loadImages = function () {
        // register some loaders
        __WEBPACK_IMPORTED_MODULE_2__theme_services__["c" /* BaThemePreloader */].registerLoader(this._imageLoader.load('assets/img/sky-bg.jpg'));
    };
    return App;
}());
App = __decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["Component"])({
        selector: 'app',
        styles: [__webpack_require__(764)],
        template: "\n    <main [class.menu-collapsed]=\"isMenuCollapsed\" baThemeRun>\n      <div class=\"additional-bg\"></div>\n      <router-outlet></router-outlet>\n    </main>\n  "
    }),
    __metadata("design:paramtypes", [typeof (_a = typeof __WEBPACK_IMPORTED_MODULE_1__global_state__["a" /* GlobalState */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_1__global_state__["a" /* GlobalState */]) === "function" && _a || Object, typeof (_b = typeof __WEBPACK_IMPORTED_MODULE_2__theme_services__["b" /* BaImageLoaderService */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_2__theme_services__["b" /* BaImageLoaderService */]) === "function" && _b || Object, typeof (_c = typeof __WEBPACK_IMPORTED_MODULE_2__theme_services__["d" /* BaThemeSpinner */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_2__theme_services__["d" /* BaThemeSpinner */]) === "function" && _c || Object, typeof (_d = typeof __WEBPACK_IMPORTED_MODULE_0__angular_core__["ViewContainerRef"] !== "undefined" && __WEBPACK_IMPORTED_MODULE_0__angular_core__["ViewContainerRef"]) === "function" && _d || Object, typeof (_e = typeof __WEBPACK_IMPORTED_MODULE_3__theme_theme_config__["a" /* BaThemeConfig */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_3__theme_theme_config__["a" /* BaThemeConfig */]) === "function" && _e || Object])
], App);

var _a, _b, _c, _d, _e;
//# sourceMappingURL=app.component.js.map

/***/ }),

/***/ 517:
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__(0);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__angular_platform_browser__ = __webpack_require__(69);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__angular_forms__ = __webpack_require__(22);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_3__angular_http__ = __webpack_require__(66);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_4__angular_router__ = __webpack_require__(20);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_5__ng_bootstrap_ng_bootstrap__ = __webpack_require__(468);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_6__core_guards_auth_guard__ = __webpack_require__(53);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_7__app_routing__ = __webpack_require__(518);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_8__app_component__ = __webpack_require__(516);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_9__app_service__ = __webpack_require__(519);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_10__global_state__ = __webpack_require__(60);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_11__theme_nga_module__ = __webpack_require__(202);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_12__main_main_module__ = __webpack_require__(263);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return AppModule; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};







/*
 * Platform and Environment providers/directives/pipes
 */

// App is our top level component





// Application wide providers
var APP_PROVIDERS = [
    __WEBPACK_IMPORTED_MODULE_6__core_guards_auth_guard__["a" /* AuthGuard */],
    __WEBPACK_IMPORTED_MODULE_9__app_service__["a" /* AppState */],
    __WEBPACK_IMPORTED_MODULE_10__global_state__["a" /* GlobalState */]
];
/**
 * `AppModule` is the main entry point into Angular2's bootstraping process
 */
var AppModule = (function () {
    function AppModule(appState) {
        this.appState = appState;
    }
    return AppModule;
}());
AppModule = __decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["NgModule"])({
        bootstrap: [__WEBPACK_IMPORTED_MODULE_8__app_component__["a" /* App */]],
        declarations: [
            __WEBPACK_IMPORTED_MODULE_8__app_component__["a" /* App */]
        ],
        imports: [
            __WEBPACK_IMPORTED_MODULE_1__angular_platform_browser__["a" /* BrowserModule */],
            __WEBPACK_IMPORTED_MODULE_3__angular_http__["a" /* HttpModule */],
            __WEBPACK_IMPORTED_MODULE_4__angular_router__["a" /* RouterModule */],
            __WEBPACK_IMPORTED_MODULE_2__angular_forms__["FormsModule"],
            __WEBPACK_IMPORTED_MODULE_2__angular_forms__["ReactiveFormsModule"],
            __WEBPACK_IMPORTED_MODULE_11__theme_nga_module__["a" /* NgaModule */].forRoot(),
            __WEBPACK_IMPORTED_MODULE_5__ng_bootstrap_ng_bootstrap__["a" /* NgbModule */].forRoot(),
            __WEBPACK_IMPORTED_MODULE_12__main_main_module__["MainModule"],
            __WEBPACK_IMPORTED_MODULE_7__app_routing__["a" /* routing */]
        ],
        providers: [
            APP_PROVIDERS
        ]
    }),
    __metadata("design:paramtypes", [typeof (_a = typeof __WEBPACK_IMPORTED_MODULE_9__app_service__["a" /* AppState */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_9__app_service__["a" /* AppState */]) === "function" && _a || Object])
], AppModule);

var _a;
//# sourceMappingURL=app.module.js.map

/***/ }),

/***/ 518:
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__core_guards_auth_guard__ = __webpack_require__(53);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__angular_router__ = __webpack_require__(20);
/* unused harmony export routes */
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return routing; });


var routes = [
    { path: '', redirectTo: 'login', pathMatch: 'full' },
    { path: 'login', loadChildren: 'app/main/login/login.module#LoginModule' },
    { path: 'main', loadChildren: './main/main.module#MainModule', canActivate: [__WEBPACK_IMPORTED_MODULE_0__core_guards_auth_guard__["a" /* AuthGuard */]] }
];
var routing = __WEBPACK_IMPORTED_MODULE_1__angular_router__["a" /* RouterModule */].forRoot(routes, { useHash: true });
//# sourceMappingURL=app.routing.js.map

/***/ }),

/***/ 519:
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__(0);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return AppState; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};

var AppState = (function () {
    function AppState() {
        this._state = {};
    }
    Object.defineProperty(AppState.prototype, "state", {
        // already return a clone of the current state
        get: function () {
            return this._state = this._clone(this._state);
        },
        // never allow mutation
        set: function (value) {
            throw new Error('do not mutate the `.state` directly');
        },
        enumerable: true,
        configurable: true
    });
    AppState.prototype.get = function (prop) {
        // use our state getter for the clone
        var state = this.state;
        return state.hasOwnProperty(prop) ? state[prop] : state;
    };
    AppState.prototype.set = function (prop, value) {
        // internally mutate our state
        return this._state[prop] = value;
    };
    AppState.prototype._clone = function (object) {
        // simple object clone
        return JSON.parse(JSON.stringify(object));
    };
    return AppState;
}());
AppState = __decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["Injectable"])(),
    __metadata("design:paramtypes", [])
], AppState);

//# sourceMappingURL=app.service.js.map

/***/ }),

/***/ 520:
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__auth_guard__ = __webpack_require__(53);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__common_url_constants__ = __webpack_require__(59);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__common_system_constants__ = __webpack_require__(33);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_3__angular_core__ = __webpack_require__(0);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_4__angular_router__ = __webpack_require__(20);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return AdminLecturerAuthGuard; });
var __extends = (this && this.__extends) || (function () {
    var extendStatics = Object.setPrototypeOf ||
        ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
        function (d, b) { for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p]; };
    return function (d, b) {
        extendStatics(d, b);
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
})();
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};





var AdminLecturerAuthGuard = (function (_super) {
    __extends(AdminLecturerAuthGuard, _super);
    function AdminLecturerAuthGuard(router) {
        return _super.call(this, router) || this;
    }
    AdminLecturerAuthGuard.prototype.canActivate = function (activateRoute, routerState) {
        var user = JSON.parse(localStorage.getItem(__WEBPACK_IMPORTED_MODULE_2__common_system_constants__["a" /* SystemConstants */].CURRENT_USER));
        if (user.role === "Admin" || user.role === "Lecturer") {
            return true;
        }
        else {
            this.router.navigate([__WEBPACK_IMPORTED_MODULE_1__common_url_constants__["a" /* UrlConstants */].HOME], {
                queryParams: {
                    returnUrl: routerState.url
                }
            });
            return false;
        }
    };
    return AdminLecturerAuthGuard;
}(__WEBPACK_IMPORTED_MODULE_0__auth_guard__["a" /* AuthGuard */]));
AdminLecturerAuthGuard = __decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_3__angular_core__["Injectable"])(),
    __metadata("design:paramtypes", [typeof (_a = typeof __WEBPACK_IMPORTED_MODULE_4__angular_router__["b" /* Router */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_4__angular_router__["b" /* Router */]) === "function" && _a || Object])
], AdminLecturerAuthGuard);

var _a;
//# sourceMappingURL=auth-admin-lecturer.guard.js.map

/***/ }),

/***/ 521:
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__auth_guard__ = __webpack_require__(53);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__common_url_constants__ = __webpack_require__(59);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__common_system_constants__ = __webpack_require__(33);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_3__angular_core__ = __webpack_require__(0);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_4__angular_router__ = __webpack_require__(20);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return AdminAuthGuard; });
var __extends = (this && this.__extends) || (function () {
    var extendStatics = Object.setPrototypeOf ||
        ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
        function (d, b) { for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p]; };
    return function (d, b) {
        extendStatics(d, b);
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
})();
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};





var AdminAuthGuard = (function (_super) {
    __extends(AdminAuthGuard, _super);
    function AdminAuthGuard(router) {
        return _super.call(this, router) || this;
    }
    AdminAuthGuard.prototype.canActivate = function (activateRoute, routerState) {
        var user = JSON.parse(localStorage.getItem(__WEBPACK_IMPORTED_MODULE_2__common_system_constants__["a" /* SystemConstants */].CURRENT_USER));
        if (user.role === "Admin") {
            return true;
        }
        else {
            this.router.navigate([__WEBPACK_IMPORTED_MODULE_1__common_url_constants__["a" /* UrlConstants */].HOME], {
                queryParams: {
                    returnUrl: routerState.url
                }
            });
            return false;
        }
    };
    return AdminAuthGuard;
}(__WEBPACK_IMPORTED_MODULE_0__auth_guard__["a" /* AuthGuard */]));
AdminAuthGuard = __decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_3__angular_core__["Injectable"])(),
    __metadata("design:paramtypes", [typeof (_a = typeof __WEBPACK_IMPORTED_MODULE_4__angular_router__["b" /* Router */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_4__angular_router__["b" /* Router */]) === "function" && _a || Object])
], AdminAuthGuard);

var _a;
//# sourceMappingURL=auth-admin.guard.js.map

/***/ }),

/***/ 522:
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__auth_guard__ = __webpack_require__(53);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__common_url_constants__ = __webpack_require__(59);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__common_system_constants__ = __webpack_require__(33);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_3__angular_core__ = __webpack_require__(0);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_4__angular_router__ = __webpack_require__(20);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return LecturerAuthGuard; });
var __extends = (this && this.__extends) || (function () {
    var extendStatics = Object.setPrototypeOf ||
        ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
        function (d, b) { for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p]; };
    return function (d, b) {
        extendStatics(d, b);
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
})();
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};





var LecturerAuthGuard = (function (_super) {
    __extends(LecturerAuthGuard, _super);
    function LecturerAuthGuard(router) {
        return _super.call(this, router) || this;
    }
    LecturerAuthGuard.prototype.canActivate = function (activateRoute, routerState) {
        var user = JSON.parse(localStorage.getItem(__WEBPACK_IMPORTED_MODULE_2__common_system_constants__["a" /* SystemConstants */].CURRENT_USER));
        if (user.role === "Lecturer") {
            return true;
        }
        else {
            this.router.navigate([__WEBPACK_IMPORTED_MODULE_1__common_url_constants__["a" /* UrlConstants */].HOME], {
                queryParams: {
                    returnUrl: routerState.url
                }
            });
            return false;
        }
    };
    return LecturerAuthGuard;
}(__WEBPACK_IMPORTED_MODULE_0__auth_guard__["a" /* AuthGuard */]));
LecturerAuthGuard = __decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_3__angular_core__["Injectable"])(),
    __metadata("design:paramtypes", [typeof (_a = typeof __WEBPACK_IMPORTED_MODULE_4__angular_router__["b" /* Router */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_4__angular_router__["b" /* Router */]) === "function" && _a || Object])
], LecturerAuthGuard);

var _a;
//# sourceMappingURL=auth-lecturer.guard.js.map

/***/ }),

/***/ 523:
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__auth_guard__ = __webpack_require__(53);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__common_url_constants__ = __webpack_require__(59);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__common_system_constants__ = __webpack_require__(33);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_3__angular_core__ = __webpack_require__(0);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_4__angular_router__ = __webpack_require__(20);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return StudentAuthGuard; });
var __extends = (this && this.__extends) || (function () {
    var extendStatics = Object.setPrototypeOf ||
        ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
        function (d, b) { for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p]; };
    return function (d, b) {
        extendStatics(d, b);
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
})();
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};





var StudentAuthGuard = (function (_super) {
    __extends(StudentAuthGuard, _super);
    function StudentAuthGuard(router) {
        return _super.call(this, router) || this;
    }
    StudentAuthGuard.prototype.canActivate = function (activateRoute, routerState) {
        var user = JSON.parse(localStorage.getItem(__WEBPACK_IMPORTED_MODULE_2__common_system_constants__["a" /* SystemConstants */].CURRENT_USER));
        if (user.role === "Student") {
            return true;
        }
        else {
            this.router.navigate([__WEBPACK_IMPORTED_MODULE_1__common_url_constants__["a" /* UrlConstants */].HOME], {
                queryParams: {
                    returnUrl: routerState.url
                }
            });
            return false;
        }
    };
    return StudentAuthGuard;
}(__WEBPACK_IMPORTED_MODULE_0__auth_guard__["a" /* AuthGuard */]));
StudentAuthGuard = __decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_3__angular_core__["Injectable"])(),
    __metadata("design:paramtypes", [typeof (_a = typeof __WEBPACK_IMPORTED_MODULE_4__angular_router__["b" /* Router */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_4__angular_router__["b" /* Router */]) === "function" && _a || Object])
], StudentAuthGuard);

var _a;
//# sourceMappingURL=auth-student.guard.js.map

/***/ }),

/***/ 524:
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return LoggedInUser; });
var LoggedInUser = (function () {
    function LoggedInUser(access_token, avatar, email, fullName, role, major, username) {
        this.access_token = access_token;
        this.avatar = avatar;
        this.email = email;
        this.fullName = fullName;
        this.role = role;
        this.major = major,
            this.username = username;
    }
    return LoggedInUser;
}());

//# sourceMappingURL=loggedin.user.js.map

/***/ }),

/***/ 525:
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return PAGES_MENU_ADMIN; });
var PAGES_MENU_ADMIN = [
    {
        path: "main",
        children: [
            {
                path: "dashboard",
                data: {
                    menu: {
                        title: "general.menu.dashboard",
                        icon: "ion-android-home",
                        selected: false,
                        expanded: false,
                        order: 0
                    }
                }
            },
            {
                path: "role",
                data: {
                    menu: {
                        title: "Role",
                        icon: "fa fa-circle-notch",
                        selected: false,
                        expanded: false,
                        order: 0
                    }
                }
            },
            {
                path: "student",
                data: {
                    menu: {
                        title: "Student",
                        icon: "fa fa-graduation-cap",
                        selected: false,
                        expanded: false,
                        order: 0
                    }
                }
            },
            {
                path: "major",
                data: {
                    menu: {
                        title: "Major",
                        icon: "fa fa-bookmark",
                        selected: false,
                        expanded: false,
                        order: 0
                    }
                }
            },
            {
                path: "project",
                data: {
                    menu: {
                        title: "Project",
                        icon: "fa fa-book",
                        selected: false,
                        expanded: false,
                        order: 0
                    }
                }
            },
            {
                path: "lecturer",
                data: {
                    menu: {
                        title: "Lecturer",
                        icon: "fa fa-male",
                        selected: false,
                        expanded: false,
                        order: 0
                    }
                }
            },
            {
                path: "quarter",
                data: {
                    menu: {
                        title: "Quarter",
                        icon: "fa fa-calendar",
                        selected: false,
                        expanded: false,
                        order: 0
                    }
                }
            },
            {
                path: "group",
                data: {
                    menu: {
                        title: "Group",
                        icon: "fa fa-group",
                        selected: false,
                        expanded: false,
                        order: 0
                    }
                }
            },
            {
                path: "confirm-group",
                data: {
                    menu: {
                        title: "Confirm Group",
                        icon: "fa fa-user-plus",
                        selected: false,
                        expanded: false,
                        order: 0
                    }
                }
            },
            {
                path: "tag",
                data: {
                    menu: {
                        title: "Tag",
                        icon: "fa fa-tags",
                        selected: false,
                        expanded: false,
                        order: 0
                    }
                }
            }
        ]
    }
];
//# sourceMappingURL=main.menuAdmin.js.map

/***/ }),

/***/ 526:
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return PAGES_MENU_LECTURER; });
var PAGES_MENU_LECTURER = [
    {
        path: "main",
        children: [
            {
                path: "dashboard",
                data: {
                    menu: {
                        title: "general.menu.dashboard",
                        icon: "ion-android-home",
                        selected: false,
                        expanded: false,
                        order: 0
                    }
                }
            },
            {
                path: "student",
                data: {
                    menu: {
                        title: "Student",
                        icon: "fa fa-graduation-cap",
                        selected: false,
                        expanded: false,
                        order: 0
                    }
                }
            },
            {
                path: "project",
                data: {
                    menu: {
                        title: "Project",
                        icon: "fa fa-book",
                        selected: false,
                        expanded: false,
                        order: 0
                    }
                }
            },
            {
                path: "lecturer",
                data: {
                    menu: {
                        title: "Lecturer",
                        icon: "fa fa-male",
                        selected: false,
                        expanded: false,
                        order: 0
                    }
                }
            },
            {
                path: "group",
                data: {
                    menu: {
                        title: "Group",
                        icon: "fa fa-group",
                        selected: false,
                        expanded: false,
                        order: 0
                    }
                }
            },
            {
                path: "confirm-group",
                data: {
                    menu: {
                        title: "Confirm Group",
                        icon: "fa fa-user-plus",
                        selected: false,
                        expanded: false,
                        order: 0
                    }
                }
            },
            {
                path: "tag",
                data: {
                    menu: {
                        title: "Tag",
                        icon: "fa fa-tags",
                        selected: false,
                        expanded: false,
                        order: 0
                    }
                }
            }
        ]
    }
];
//# sourceMappingURL=main.menuLecturer.js.map

/***/ }),

/***/ 527:
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return PAGES_MENU_STUDENT; });
var PAGES_MENU_STUDENT = [
    {
        path: "main",
        children: [
            {
                path: "dashboard",
                data: {
                    menu: {
                        title: "general.menu.dashboard",
                        icon: "ion-android-home",
                        selected: false,
                        expanded: false,
                        order: 0
                    }
                }
            },
            {
                path: "project",
                data: {
                    menu: {
                        title: "Project",
                        icon: "fa fa-book",
                        selected: false,
                        expanded: false,
                        order: 0
                    }
                }
            },
            {
                path: "lecturer",
                data: {
                    menu: {
                        title: "Lecturer",
                        icon: "fa fa-male",
                        selected: false,
                        expanded: false,
                        order: 0
                    }
                }
            },
            {
                path: "group",
                data: {
                    menu: {
                        title: "Group",
                        icon: "fa fa-group",
                        selected: false,
                        expanded: false,
                        order: 0
                    }
                }
            },
            {
                path: "confirm-group",
                data: {
                    menu: {
                        title: "Confirm Group",
                        icon: "fa fa-user-plus",
                        selected: false,
                        expanded: false,
                        order: 0
                    }
                }
            },
            {
                path: "tag",
                data: {
                    menu: {
                        title: "Tag",
                        icon: "fa fa-tags",
                        selected: false,
                        expanded: false,
                        order: 0
                    }
                }
            }
        ]
    }
];
//# sourceMappingURL=main.menuStudent.js.map

/***/ }),

/***/ 528:
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__core_guards_auth_guard__ = __webpack_require__(53);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__angular_router__ = __webpack_require__(20);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__main_component__ = __webpack_require__(262);
/* unused harmony export routes */
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return routing; });



// noinspection TypeScriptValidateTypes
// export function loadChildren(path) { return System.import(path); };
var routes = [
    {
        path: "login",
        loadChildren: "app/main/login/login.module#LoginModule",
        pathMatch: "full"
    },
    {
        path: "register",
        loadChildren: "app/main/register/register.module#RegisterModule"
    },
    {
        path: "main",
        component: __WEBPACK_IMPORTED_MODULE_2__main_component__["a" /* Main */],
        children: [
            {
                path: "dashboard",
                loadChildren: "./dashboard/dashboard.module#DashboardModule"
            },
            {
                path: "editors",
                loadChildren: "./editors/editors.module#EditorsModule"
            },
            {
                path: "components",
                loadChildren: "./components/components.module#ComponentsModule"
            },
            {
                path: "charts",
                loadChildren: "./charts/charts.module#ChartsModule"
            },
            {
                path: "ui",
                loadChildren: "./ui/ui.module#UiModule"
            },
            {
                path: "forms",
                loadChildren: "./forms/forms.module#FormsModule"
            },
            {
                path: "tables",
                loadChildren: "./tables/tables.module#TablesModule"
            },
            {
                path: "maps",
                loadChildren: "./maps/maps.module#MapsModule"
            },
            {
                path: "student",
                loadChildren: "./student/student.module#StudentModule"
            },
            { path: "role", loadChildren: "./role/role.module#RoleModule" },
            {
                path: "lecturer",
                loadChildren: "./lecturer/lecturer.module#LecturerModule"
            },
            {
                path: "quarter",
                loadChildren: "./quarter/quarter.module#QuarterModule"
            },
            {
                path: "project",
                loadChildren: "./project/project.module#ProjectModule"
            },
            {
                path: "major",
                loadChildren: "./major/major.module#MajorModule"
            },
            {
                path: "group",
                loadChildren: "./group/group.module#GroupModule"
            },
            {
                path: "tag",
                loadChildren: "./tag/tag.module#TagModule"
            },
            {
                path: "tag-details/:tag",
                loadChildren: "./tag-details/tag-details.module#TagDetailsModule"
            },
            {
                path: "confirm-group",
                loadChildren: "./confirm-group/confirm-group.module#GroupConfirmModule"
            },
            {
                path: "group-details/:id",
                loadChildren: "./group-details/group-details.module#GroupDetailsModule"
            }
        ],
        canActivate: [__WEBPACK_IMPORTED_MODULE_0__core_guards_auth_guard__["a" /* AuthGuard */]]
    }
];
var routing = __WEBPACK_IMPORTED_MODULE_1__angular_router__["a" /* RouterModule */].forChild(routes);
//# sourceMappingURL=main.routing.js.map

/***/ }),

/***/ 529:
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__(0);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__theme_services__ = __webpack_require__(71);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2_amcharts3__ = __webpack_require__(594);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2_amcharts3___default = __webpack_require__.n(__WEBPACK_IMPORTED_MODULE_2_amcharts3__);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_3_amcharts3_amcharts_plugins_responsive_responsive_js__ = __webpack_require__(595);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_3_amcharts3_amcharts_plugins_responsive_responsive_js___default = __webpack_require__.n(__WEBPACK_IMPORTED_MODULE_3_amcharts3_amcharts_plugins_responsive_responsive_js__);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_4_amcharts3_amcharts_serial_js__ = __webpack_require__(596);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_4_amcharts3_amcharts_serial_js___default = __webpack_require__.n(__WEBPACK_IMPORTED_MODULE_4_amcharts3_amcharts_serial_js__);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_5_ammap3__ = __webpack_require__(597);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_5_ammap3___default = __webpack_require__.n(__WEBPACK_IMPORTED_MODULE_5_ammap3__);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_6_ammap3_ammap_maps_js_worldLow__ = __webpack_require__(598);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_6_ammap3_ammap_maps_js_worldLow___default = __webpack_require__.n(__WEBPACK_IMPORTED_MODULE_6_ammap3_ammap_maps_js_worldLow__);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_7__baAmChartTheme_service__ = __webpack_require__(530);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return BaAmChart; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};








var BaAmChart = (function () {
    function BaAmChart(_baAmChartThemeService) {
        this._baAmChartThemeService = _baAmChartThemeService;
        this.onChartReady = new __WEBPACK_IMPORTED_MODULE_0__angular_core__["EventEmitter"]();
        this._loadChartsLib();
    }
    BaAmChart.prototype.ngOnInit = function () {
        AmCharts.themes.blur = this._baAmChartThemeService.getTheme();
    };
    BaAmChart.prototype.ngAfterViewInit = function () {
        var chart = AmCharts.makeChart(this._selector.nativeElement, this.baAmChartConfiguration);
        this.onChartReady.emit(chart);
    };
    BaAmChart.prototype._loadChartsLib = function () {
        __WEBPACK_IMPORTED_MODULE_1__theme_services__["c" /* BaThemePreloader */].registerLoader(new Promise(function (resolve, reject) {
            var amChartsReadyMsg = 'AmCharts ready';
            if (AmCharts.isReady) {
                resolve(amChartsReadyMsg);
            }
            else {
                AmCharts.ready(function () {
                    resolve(amChartsReadyMsg);
                });
            }
        }));
    };
    return BaAmChart;
}());
__decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["Input"])(),
    __metadata("design:type", Object)
], BaAmChart.prototype, "baAmChartConfiguration", void 0);
__decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["Input"])(),
    __metadata("design:type", String)
], BaAmChart.prototype, "baAmChartClass", void 0);
__decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["Output"])(),
    __metadata("design:type", Object)
], BaAmChart.prototype, "onChartReady", void 0);
__decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["ViewChild"])('baAmChart'),
    __metadata("design:type", typeof (_a = typeof __WEBPACK_IMPORTED_MODULE_0__angular_core__["ElementRef"] !== "undefined" && __WEBPACK_IMPORTED_MODULE_0__angular_core__["ElementRef"]) === "function" && _a || Object)
], BaAmChart.prototype, "_selector", void 0);
BaAmChart = __decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["Component"])({
        selector: 'ba-am-chart',
        template: __webpack_require__(783),
        styles: [__webpack_require__(765)],
        providers: [__WEBPACK_IMPORTED_MODULE_7__baAmChartTheme_service__["a" /* BaAmChartThemeService */]],
    }),
    __metadata("design:paramtypes", [typeof (_b = typeof __WEBPACK_IMPORTED_MODULE_7__baAmChartTheme_service__["a" /* BaAmChartThemeService */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_7__baAmChartTheme_service__["a" /* BaAmChartThemeService */]) === "function" && _b || Object])
], BaAmChart);

var _a, _b;
//# sourceMappingURL=baAmChart.component.js.map

/***/ }),

/***/ 53:
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__common_url_constants__ = __webpack_require__(59);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__common_system_constants__ = __webpack_require__(33);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__angular_core__ = __webpack_require__(0);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_3__angular_router__ = __webpack_require__(20);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return AuthGuard; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};




var AuthGuard = (function () {
    function AuthGuard(router) {
        this.router = router;
    }
    AuthGuard.prototype.canActivate = function (activateRoute, routerState) {
        if (localStorage.getItem(__WEBPACK_IMPORTED_MODULE_1__common_system_constants__["a" /* SystemConstants */].CURRENT_USER)) {
            return true;
        }
        else {
            this.router.navigate([__WEBPACK_IMPORTED_MODULE_0__common_url_constants__["a" /* UrlConstants */].LOGIN], {
                queryParams: {
                    returnUrl: routerState.url
                }
            });
            return false;
        }
    };
    return AuthGuard;
}());
AuthGuard = __decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_2__angular_core__["Injectable"])(),
    __metadata("design:paramtypes", [typeof (_a = typeof __WEBPACK_IMPORTED_MODULE_3__angular_router__["b" /* Router */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_3__angular_router__["b" /* Router */]) === "function" && _a || Object])
], AuthGuard);

var _a;
//# sourceMappingURL=auth.guard.js.map

/***/ }),

/***/ 530:
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__(0);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__theme__ = __webpack_require__(34);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return BaAmChartThemeService; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};


var BaAmChartThemeService = (function () {
    function BaAmChartThemeService(_baConfig) {
        this._baConfig = _baConfig;
    }
    BaAmChartThemeService.prototype.getTheme = function () {
        var layoutColors = this._baConfig.get().colors;
        return {
            themeName: "blur",
            AmChart: {
                color: layoutColors.defaultText,
                backgroundColor: "#FFFFFF"
            },
            AmCoordinateChart: {
                colors: [layoutColors.primary, layoutColors.danger, layoutColors.warning, layoutColors.success, layoutColors.info, layoutColors.primaryDark, layoutColors.warningLight, layoutColors.successDark, layoutColors.successLight, layoutColors.primaryLight, layoutColors.warningDark]
            },
            AmStockChart: {
                colors: [layoutColors.primary, layoutColors.danger, layoutColors.warning, layoutColors.success, layoutColors.info, layoutColors.primaryDark, layoutColors.warningLight, layoutColors.successDark, layoutColors.successLight, layoutColors.primaryLight, layoutColors.warningDark]
            },
            AmSlicedChart: {
                colors: [layoutColors.primary, layoutColors.danger, layoutColors.warning, layoutColors.success, layoutColors.info, layoutColors.primaryDark, layoutColors.warningLight, layoutColors.successDark, layoutColors.successLight, layoutColors.primaryLight, layoutColors.warningDark],
                labelTickColor: "#FFFFFF",
                labelTickAlpha: 0.3
            },
            AmRectangularChart: {
                zoomOutButtonColor: '#FFFFFF',
                zoomOutButtonRollOverAlpha: 0.15,
                zoomOutButtonImage: "lens.png"
            },
            AxisBase: {
                axisColor: "#FFFFFF",
                axisAlpha: 0.3,
                gridAlpha: 0.1,
                gridColor: "#FFFFFF"
            },
            ChartScrollbar: {
                backgroundColor: "#FFFFFF",
                backgroundAlpha: 0.12,
                graphFillAlpha: 0.5,
                graphLineAlpha: 0,
                selectedBackgroundColor: "#FFFFFF",
                selectedBackgroundAlpha: 0.4,
                gridAlpha: 0.15
            },
            ChartCursor: {
                cursorColor: layoutColors.primary,
                color: "#FFFFFF",
                cursorAlpha: 0.5
            },
            AmLegend: {
                color: "#FFFFFF"
            },
            AmGraph: {
                lineAlpha: 0.9
            },
            GaugeArrow: {
                color: "#FFFFFF",
                alpha: 0.8,
                nailAlpha: 0,
                innerRadius: "40%",
                nailRadius: 15,
                startWidth: 15,
                borderAlpha: 0.8,
                nailBorderAlpha: 0
            },
            GaugeAxis: {
                tickColor: "#FFFFFF",
                tickAlpha: 1,
                tickLength: 15,
                minorTickLength: 8,
                axisThickness: 3,
                axisColor: '#FFFFFF',
                axisAlpha: 1,
                bandAlpha: 0.8
            },
            TrendLine: {
                lineColor: layoutColors.danger,
                lineAlpha: 0.8
            },
            // ammap
            AreasSettings: {
                alpha: 0.8,
                color: layoutColors.info,
                colorSolid: layoutColors.primaryDark,
                unlistedAreasAlpha: 0.4,
                unlistedAreasColor: "#FFFFFF",
                outlineColor: "#FFFFFF",
                outlineAlpha: 0.5,
                outlineThickness: 0.5,
                rollOverColor: layoutColors.primary,
                rollOverOutlineColor: "#FFFFFF",
                selectedOutlineColor: "#FFFFFF",
                selectedColor: "#f15135",
                unlistedAreasOutlineColor: "#FFFFFF",
                unlistedAreasOutlineAlpha: 0.5
            },
            LinesSettings: {
                color: "#FFFFFF",
                alpha: 0.8
            },
            ImagesSettings: {
                alpha: 0.8,
                labelColor: "#FFFFFF",
                color: "#FFFFFF",
                labelRollOverColor: layoutColors.primaryDark
            },
            ZoomControl: {
                buttonFillAlpha: 0.8,
                buttonIconColor: layoutColors.default,
                buttonRollOverColor: layoutColors.danger,
                buttonFillColor: layoutColors.primaryDark,
                buttonBorderColor: layoutColors.primaryDark,
                buttonBorderAlpha: 0,
                buttonCornerRadius: 0,
                gridColor: "#FFFFFF",
                gridBackgroundColor: "#FFFFFF",
                buttonIconAlpha: 0.6,
                gridAlpha: 0.6,
                buttonSize: 20
            },
            SmallMap: {
                mapColor: "#000000",
                rectangleColor: layoutColors.danger,
                backgroundColor: "#FFFFFF",
                backgroundAlpha: 0.7,
                borderThickness: 1,
                borderAlpha: 0.8
            },
            // the defaults below are set using CSS syntax, you can use any existing css property
            // if you don't use Stock chart, you can delete lines below
            PeriodSelector: {
                color: "#FFFFFF"
            },
            PeriodButton: {
                color: "#FFFFFF",
                background: "transparent",
                opacity: 0.7,
                border: "1px solid rgba(0, 0, 0, .3)",
                MozBorderRadius: "5px",
                borderRadius: "5px",
                margin: "1px",
                outline: "none",
                boxSizing: "border-box"
            },
            PeriodButtonSelected: {
                color: "#FFFFFF",
                backgroundColor: "#b9cdf5",
                border: "1px solid rgba(0, 0, 0, .3)",
                MozBorderRadius: "5px",
                borderRadius: "5px",
                margin: "1px",
                outline: "none",
                opacity: 1,
                boxSizing: "border-box"
            },
            PeriodInputField: {
                color: "#FFFFFF",
                background: "transparent",
                border: "1px solid rgba(0, 0, 0, .3)",
                outline: "none"
            },
            DataSetSelector: {
                color: "#FFFFFF",
                selectedBackgroundColor: "#b9cdf5",
                rollOverBackgroundColor: "#a8b0e4"
            },
            DataSetCompareList: {
                color: "#FFFFFF",
                lineHeight: "100%",
                boxSizing: "initial",
                webkitBoxSizing: "initial",
                border: "1px solid rgba(0, 0, 0, .3)"
            },
            DataSetSelect: {
                border: "1px solid rgba(0, 0, 0, .3)",
                outline: "none"
            }
        };
    };
    return BaAmChartThemeService;
}());
BaAmChartThemeService = __decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["Injectable"])(),
    __metadata("design:paramtypes", [typeof (_a = typeof __WEBPACK_IMPORTED_MODULE_1__theme__["b" /* BaThemeConfigProvider */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_1__theme__["b" /* BaThemeConfigProvider */]) === "function" && _a || Object])
], BaAmChartThemeService);

var _a;
//# sourceMappingURL=baAmChartTheme.service.js.map

/***/ }),

/***/ 531:
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__baAmChart_component__ = __webpack_require__(529);
/* harmony namespace reexport (by used) */ __webpack_require__.d(__webpack_exports__, "a", function() { return __WEBPACK_IMPORTED_MODULE_0__baAmChart_component__["a"]; });

//# sourceMappingURL=index.js.map

/***/ }),

/***/ 532:
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__(0);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return BaBackTop; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};

var BaBackTop = (function () {
    function BaBackTop() {
        this.position = 400;
        this.showSpeed = 500;
        this.moveSpeed = 1000;
    }
    BaBackTop.prototype.ngAfterViewInit = function () {
        this._onWindowScroll();
    };
    BaBackTop.prototype._onClick = function () {
        jQuery('html, body').animate({ scrollTop: 0 }, { duration: this.moveSpeed });
        return false;
    };
    BaBackTop.prototype._onWindowScroll = function () {
        var el = this._selector.nativeElement;
        window.scrollY > this.position ? jQuery(el).fadeIn(this.showSpeed) : jQuery(el).fadeOut(this.showSpeed);
    };
    return BaBackTop;
}());
__decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["Input"])(),
    __metadata("design:type", Number)
], BaBackTop.prototype, "position", void 0);
__decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["Input"])(),
    __metadata("design:type", Number)
], BaBackTop.prototype, "showSpeed", void 0);
__decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["Input"])(),
    __metadata("design:type", Number)
], BaBackTop.prototype, "moveSpeed", void 0);
__decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["ViewChild"])('baBackTop'),
    __metadata("design:type", typeof (_a = typeof __WEBPACK_IMPORTED_MODULE_0__angular_core__["ElementRef"] !== "undefined" && __WEBPACK_IMPORTED_MODULE_0__angular_core__["ElementRef"]) === "function" && _a || Object)
], BaBackTop.prototype, "_selector", void 0);
__decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["HostListener"])('click'),
    __metadata("design:type", Function),
    __metadata("design:paramtypes", []),
    __metadata("design:returntype", Boolean)
], BaBackTop.prototype, "_onClick", null);
__decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["HostListener"])('window:scroll'),
    __metadata("design:type", Function),
    __metadata("design:paramtypes", []),
    __metadata("design:returntype", void 0)
], BaBackTop.prototype, "_onWindowScroll", null);
BaBackTop = __decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["Component"])({
        selector: 'ba-back-top',
        styles: [__webpack_require__(766)],
        template: "\n    <i #baBackTop class=\"fa fa-angle-up back-top ba-back-top\" title=\"Back to Top\"></i>\n  "
    })
], BaBackTop);

var _a;
//# sourceMappingURL=baBackTop.component.js.map

/***/ }),

/***/ 533:
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__baBackTop_component__ = __webpack_require__(532);
/* harmony namespace reexport (by used) */ __webpack_require__.d(__webpack_exports__, "a", function() { return __WEBPACK_IMPORTED_MODULE_0__baBackTop_component__["a"]; });

//# sourceMappingURL=index.js.map

/***/ }),

/***/ 534:
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__(0);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return BaCard; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};

var BaCard = (function () {
    function BaCard() {
    }
    return BaCard;
}());
__decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["Input"])(),
    __metadata("design:type", Object)
], BaCard.prototype, "cardTitle", void 0);
__decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["Input"])(),
    __metadata("design:type", Boolean)
], BaCard.prototype, "isAddButton", void 0);
__decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["Input"])(),
    __metadata("design:type", Object)
], BaCard.prototype, "baCardClass", void 0);
__decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["Input"])(),
    __metadata("design:type", Object)
], BaCard.prototype, "cardType", void 0);
BaCard = __decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["Component"])({
        selector: 'ba-card',
        template: __webpack_require__(784),
    })
], BaCard);

//# sourceMappingURL=baCard.component.js.map

/***/ }),

/***/ 535:
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__(0);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__theme__ = __webpack_require__(34);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__baCardBlurHelper_service__ = __webpack_require__(536);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return BaCardBlur; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};



var BaCardBlur = (function () {
    function BaCardBlur(_baConfig, _baCardBlurHelper, _el) {
        this._baConfig = _baConfig;
        this._baCardBlurHelper = _baCardBlurHelper;
        this._el = _el;
        this.isEnabled = false;
        if (this._isEnabled()) {
            this._baCardBlurHelper.init();
            this._getBodyImageSizesOnBgLoad();
            this._recalculateCardStylesOnBgLoad();
            this.isEnabled = true;
        }
    }
    BaCardBlur.prototype._onWindowResize = function () {
        if (this._isEnabled()) {
            this._bodyBgSize = this._baCardBlurHelper.getBodyBgImageSizes();
            this._recalculateCardStyle();
        }
    };
    BaCardBlur.prototype._getBodyImageSizesOnBgLoad = function () {
        var _this = this;
        this._baCardBlurHelper.bodyBgLoad().subscribe(function () {
            _this._bodyBgSize = _this._baCardBlurHelper.getBodyBgImageSizes();
        });
    };
    BaCardBlur.prototype._recalculateCardStylesOnBgLoad = function () {
        var _this = this;
        this._baCardBlurHelper.bodyBgLoad().subscribe(function (event) {
            setTimeout(_this._recalculateCardStyle.bind(_this));
        });
    };
    BaCardBlur.prototype._recalculateCardStyle = function () {
        if (!this._bodyBgSize) {
            return;
        }
        this._el.nativeElement.style.backgroundSize = Math.round(this._bodyBgSize.width) + 'px ' + Math.round(this._bodyBgSize.height) + 'px';
        this._el.nativeElement.style.backgroundPosition = Math.floor(this._bodyBgSize.positionX) + 'px ' + Math.floor(this._bodyBgSize.positionY) + 'px';
    };
    BaCardBlur.prototype._isEnabled = function () {
        return this._baConfig.get().theme.name == 'blur';
    };
    return BaCardBlur;
}());
__decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["HostBinding"])('class.card-blur'),
    __metadata("design:type", Boolean)
], BaCardBlur.prototype, "isEnabled", void 0);
__decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["HostListener"])('window:resize'),
    __metadata("design:type", Function),
    __metadata("design:paramtypes", []),
    __metadata("design:returntype", void 0)
], BaCardBlur.prototype, "_onWindowResize", null);
BaCardBlur = __decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["Directive"])({
        selector: '[baCardBlur]',
        providers: [__WEBPACK_IMPORTED_MODULE_2__baCardBlurHelper_service__["a" /* BaCardBlurHelper */]]
    }),
    __metadata("design:paramtypes", [typeof (_a = typeof __WEBPACK_IMPORTED_MODULE_1__theme__["b" /* BaThemeConfigProvider */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_1__theme__["b" /* BaThemeConfigProvider */]) === "function" && _a || Object, typeof (_b = typeof __WEBPACK_IMPORTED_MODULE_2__baCardBlurHelper_service__["a" /* BaCardBlurHelper */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_2__baCardBlurHelper_service__["a" /* BaCardBlurHelper */]) === "function" && _b || Object, typeof (_c = typeof __WEBPACK_IMPORTED_MODULE_0__angular_core__["ElementRef"] !== "undefined" && __WEBPACK_IMPORTED_MODULE_0__angular_core__["ElementRef"]) === "function" && _c || Object])
], BaCardBlur);

var _a, _b, _c;
//# sourceMappingURL=baCardBlur.directive.js.map

/***/ }),

/***/ 536:
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__(0);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1_rxjs_Subject__ = __webpack_require__(32);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1_rxjs_Subject___default = __webpack_require__.n(__WEBPACK_IMPORTED_MODULE_1_rxjs_Subject__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return BaCardBlurHelper; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};


var BaCardBlurHelper = (function () {
    function BaCardBlurHelper() {
    }
    BaCardBlurHelper.prototype.init = function () {
        this._genBgImage();
        this._genImageLoadSubject();
    };
    BaCardBlurHelper.prototype.bodyBgLoad = function () {
        return this.imageLoadSubject;
    };
    BaCardBlurHelper.prototype.getBodyBgImageSizes = function () {
        var elemW = document.documentElement.clientWidth;
        var elemH = document.documentElement.clientHeight;
        if (elemW <= 640)
            return;
        var imgRatio = (this.image.height / this.image.width); // original img ratio
        var containerRatio = (elemH / elemW); // container ratio
        var finalHeight, finalWidth;
        if (containerRatio > imgRatio) {
            finalHeight = elemH;
            finalWidth = (elemH / imgRatio);
        }
        else {
            finalWidth = elemW;
            finalHeight = (elemW * imgRatio);
        }
        return { width: finalWidth, height: finalHeight, positionX: (elemW - finalWidth) / 2, positionY: (elemH - finalHeight) / 2 };
    };
    BaCardBlurHelper.prototype._genBgImage = function () {
        this.image = new Image();
        var computedStyle = getComputedStyle(document.body.querySelector('main'), ':before');
        this.image.src = computedStyle.backgroundImage.replace(/url\((['"])?(.*?)\1\)/gi, '$2');
    };
    BaCardBlurHelper.prototype._genImageLoadSubject = function () {
        var _this = this;
        this.imageLoadSubject = new __WEBPACK_IMPORTED_MODULE_1_rxjs_Subject__["Subject"]();
        this.image.onerror = function (err) {
            _this.imageLoadSubject.complete();
        };
        this.image.onload = function () {
            _this.imageLoadSubject.next(null);
            _this.imageLoadSubject.complete();
        };
    };
    return BaCardBlurHelper;
}());
BaCardBlurHelper = __decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["Injectable"])()
], BaCardBlurHelper);

//# sourceMappingURL=baCardBlurHelper.service.js.map

/***/ }),

/***/ 537:
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__baCard_component__ = __webpack_require__(534);
/* harmony namespace reexport (by used) */ __webpack_require__.d(__webpack_exports__, "a", function() { return __WEBPACK_IMPORTED_MODULE_0__baCard_component__["a"]; });

//# sourceMappingURL=index.js.map

/***/ }),

/***/ 538:
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__(0);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1_chartist__ = __webpack_require__(599);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1_chartist___default = __webpack_require__.n(__WEBPACK_IMPORTED_MODULE_1_chartist__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return BaChartistChart; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};


var BaChartistChart = (function () {
    function BaChartistChart() {
        this.onChartReady = new __WEBPACK_IMPORTED_MODULE_0__angular_core__["EventEmitter"]();
    }
    BaChartistChart.prototype.ngAfterViewInit = function () {
        this.chart = new __WEBPACK_IMPORTED_MODULE_1_chartist__[this.baChartistChartType](this._selector.nativeElement, this.baChartistChartData, this.baChartistChartOptions, this.baChartistChartResponsive);
        this.onChartReady.emit(this.chart);
    };
    BaChartistChart.prototype.ngOnChanges = function (changes) {
        if (this.chart) {
            this.chart.update(this.baChartistChartData, this.baChartistChartOptions);
        }
    };
    BaChartistChart.prototype.ngOnDestroy = function () {
        if (this.chart) {
            this.chart.detach();
        }
    };
    return BaChartistChart;
}());
__decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["Input"])(),
    __metadata("design:type", String)
], BaChartistChart.prototype, "baChartistChartType", void 0);
__decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["Input"])(),
    __metadata("design:type", Object)
], BaChartistChart.prototype, "baChartistChartData", void 0);
__decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["Input"])(),
    __metadata("design:type", Object)
], BaChartistChart.prototype, "baChartistChartOptions", void 0);
__decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["Input"])(),
    __metadata("design:type", Object)
], BaChartistChart.prototype, "baChartistChartResponsive", void 0);
__decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["Input"])(),
    __metadata("design:type", String)
], BaChartistChart.prototype, "baChartistChartClass", void 0);
__decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["Output"])(),
    __metadata("design:type", Object)
], BaChartistChart.prototype, "onChartReady", void 0);
__decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["ViewChild"])('baChartistChart'),
    __metadata("design:type", typeof (_a = typeof __WEBPACK_IMPORTED_MODULE_0__angular_core__["ElementRef"] !== "undefined" && __WEBPACK_IMPORTED_MODULE_0__angular_core__["ElementRef"]) === "function" && _a || Object)
], BaChartistChart.prototype, "_selector", void 0);
BaChartistChart = __decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["Component"])({
        selector: 'ba-chartist-chart',
        template: __webpack_require__(785),
        providers: [],
    })
], BaChartistChart);

var _a;
//# sourceMappingURL=baChartistChart.component.js.map

/***/ }),

/***/ 539:
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__baChartistChart_component__ = __webpack_require__(538);
/* harmony namespace reexport (by used) */ __webpack_require__.d(__webpack_exports__, "a", function() { return __WEBPACK_IMPORTED_MODULE_0__baChartistChart_component__["a"]; });

//# sourceMappingURL=index.js.map

/***/ }),

/***/ 540:
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__(0);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__angular_forms__ = __webpack_require__(22);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return BaCheckbox; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
var __param = (this && this.__param) || function (paramIndex, decorator) {
    return function (target, key) { decorator(target, key, paramIndex); }
};


var BaCheckbox = (function () {
    function BaCheckbox(state) {
        this.model = state;
        state.valueAccessor = this;
    }
    BaCheckbox.prototype.onChange = function (value) {
    };
    BaCheckbox.prototype.onTouch = function (value) {
    };
    BaCheckbox.prototype.writeValue = function (state) {
        this.state = state;
    };
    BaCheckbox.prototype.registerOnChange = function (fn) {
        this.onChange = function (state) {
            this.writeValue(state);
            this.model.viewToModelUpdate(state);
        };
    };
    BaCheckbox.prototype.registerOnTouched = function (fn) {
        this.onTouch = fn;
    };
    return BaCheckbox;
}());
__decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["Input"])(),
    __metadata("design:type", Boolean)
], BaCheckbox.prototype, "disabled", void 0);
__decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["Input"])(),
    __metadata("design:type", String)
], BaCheckbox.prototype, "label", void 0);
__decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["Input"])(),
    __metadata("design:type", String)
], BaCheckbox.prototype, "value", void 0);
__decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["Input"])(),
    __metadata("design:type", String)
], BaCheckbox.prototype, "baCheckboxClass", void 0);
BaCheckbox = __decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["Component"])({
        selector: 'ba-checkbox[ngModel]',
        styles: [__webpack_require__(767)],
        template: __webpack_require__(786),
        providers: [__WEBPACK_IMPORTED_MODULE_1__angular_forms__["NgModel"]]
    }),
    __param(0, __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["Self"])()),
    __metadata("design:paramtypes", [typeof (_a = typeof __WEBPACK_IMPORTED_MODULE_1__angular_forms__["NgModel"] !== "undefined" && __WEBPACK_IMPORTED_MODULE_1__angular_forms__["NgModel"]) === "function" && _a || Object])
], BaCheckbox);

var _a;
//# sourceMappingURL=baCheckbox.component.js.map

/***/ }),

/***/ 541:
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__baCheckbox_component__ = __webpack_require__(540);
/* harmony namespace reexport (by used) */ __webpack_require__.d(__webpack_exports__, "a", function() { return __WEBPACK_IMPORTED_MODULE_0__baCheckbox_component__["a"]; });

//# sourceMappingURL=index.js.map

/***/ }),

/***/ 542:
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__(0);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__global_state__ = __webpack_require__(60);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return BaContentTop; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};


var BaContentTop = (function () {
    function BaContentTop(_state) {
        var _this = this;
        this._state = _state;
        this.activePageTitle = '';
        this._state.subscribe('menu.activeLink', function (activeLink) {
            if (activeLink) {
                _this.activePageTitle = activeLink.title;
            }
        });
    }
    return BaContentTop;
}());
BaContentTop = __decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["Component"])({
        selector: 'ba-content-top',
        styles: [__webpack_require__(768)],
        template: __webpack_require__(787),
    }),
    __metadata("design:paramtypes", [typeof (_a = typeof __WEBPACK_IMPORTED_MODULE_1__global_state__["a" /* GlobalState */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_1__global_state__["a" /* GlobalState */]) === "function" && _a || Object])
], BaContentTop);

var _a;
//# sourceMappingURL=baContentTop.component.js.map

/***/ }),

/***/ 543:
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__baContentTop_component__ = __webpack_require__(542);
/* harmony namespace reexport (by used) */ __webpack_require__.d(__webpack_exports__, "a", function() { return __WEBPACK_IMPORTED_MODULE_0__baContentTop_component__["a"]; });

//# sourceMappingURL=index.js.map

/***/ }),

/***/ 544:
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__(0);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1_ngx_uploader__ = __webpack_require__(199);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return BaFileUploader; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};


var BaFileUploader = (function () {
    function BaFileUploader(renderer) {
        this.renderer = renderer;
        this.fileUploaderOptions = { url: '' };
        this.onFileUpload = new __WEBPACK_IMPORTED_MODULE_0__angular_core__["EventEmitter"]();
        this.onFileUploadCompleted = new __WEBPACK_IMPORTED_MODULE_0__angular_core__["EventEmitter"]();
        this.defaultValue = '';
    }
    BaFileUploader.prototype.bringFileSelector = function () {
        this.renderer.invokeElementMethod(this._fileUpload.nativeElement, 'click');
        return false;
    };
    BaFileUploader.prototype.beforeFileUpload = function (uploadingFile) {
        var files = this._fileUpload.nativeElement.files;
        if (files.length) {
            var file = files[0];
            this._onChangeFileSelect(files[0]);
            if (!this._canFleUploadOnServer()) {
                uploadingFile.setAbort();
            }
            else {
                this.uploadFileInProgress = true;
            }
        }
    };
    BaFileUploader.prototype._onChangeFileSelect = function (file) {
        this._inputText.nativeElement.value = file.name;
    };
    BaFileUploader.prototype._onFileUpload = function (data) {
        if (data['done'] || data['abort'] || data['error']) {
            this._onFileUploadCompleted(data);
        }
        else {
            this.onFileUpload.emit(data);
        }
    };
    BaFileUploader.prototype._onFileUploadCompleted = function (data) {
        this.uploadFileInProgress = false;
        this.onFileUploadCompleted.emit(data);
    };
    BaFileUploader.prototype._canFleUploadOnServer = function () {
        return !!this.fileUploaderOptions['url'];
    };
    return BaFileUploader;
}());
__decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["Input"])(),
    __metadata("design:type", typeof (_a = typeof __WEBPACK_IMPORTED_MODULE_1_ngx_uploader__["b" /* NgUploaderOptions */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_1_ngx_uploader__["b" /* NgUploaderOptions */]) === "function" && _a || Object)
], BaFileUploader.prototype, "fileUploaderOptions", void 0);
__decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["Output"])(),
    __metadata("design:type", Object)
], BaFileUploader.prototype, "onFileUpload", void 0);
__decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["Output"])(),
    __metadata("design:type", Object)
], BaFileUploader.prototype, "onFileUploadCompleted", void 0);
__decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["Input"])(),
    __metadata("design:type", String)
], BaFileUploader.prototype, "defaultValue", void 0);
__decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["ViewChild"])('fileUpload'),
    __metadata("design:type", typeof (_b = typeof __WEBPACK_IMPORTED_MODULE_0__angular_core__["ElementRef"] !== "undefined" && __WEBPACK_IMPORTED_MODULE_0__angular_core__["ElementRef"]) === "function" && _b || Object)
], BaFileUploader.prototype, "_fileUpload", void 0);
__decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["ViewChild"])('inputText'),
    __metadata("design:type", typeof (_c = typeof __WEBPACK_IMPORTED_MODULE_0__angular_core__["ElementRef"] !== "undefined" && __WEBPACK_IMPORTED_MODULE_0__angular_core__["ElementRef"]) === "function" && _c || Object)
], BaFileUploader.prototype, "_inputText", void 0);
BaFileUploader = __decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["Component"])({
        selector: 'ba-file-uploader',
        styles: [__webpack_require__(769)],
        template: __webpack_require__(788),
    }),
    __metadata("design:paramtypes", [typeof (_d = typeof __WEBPACK_IMPORTED_MODULE_0__angular_core__["Renderer"] !== "undefined" && __WEBPACK_IMPORTED_MODULE_0__angular_core__["Renderer"]) === "function" && _d || Object])
], BaFileUploader);

var _a, _b, _c, _d;
//# sourceMappingURL=baFileUploader.component.js.map

/***/ }),

/***/ 545:
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__baFileUploader_component__ = __webpack_require__(544);
/* harmony namespace reexport (by used) */ __webpack_require__.d(__webpack_exports__, "a", function() { return __WEBPACK_IMPORTED_MODULE_0__baFileUploader_component__["a"]; });

//# sourceMappingURL=index.js.map

/***/ }),

/***/ 546:
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__(0);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1_fullcalendar_dist_fullcalendar_js__ = __webpack_require__(780);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1_fullcalendar_dist_fullcalendar_js___default = __webpack_require__.n(__WEBPACK_IMPORTED_MODULE_1_fullcalendar_dist_fullcalendar_js__);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2_jquery__ = __webpack_require__(208);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2_jquery___default = __webpack_require__.n(__WEBPACK_IMPORTED_MODULE_2_jquery__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return BaFullCalendar; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};



var BaFullCalendar = (function () {
    function BaFullCalendar() {
        this.onCalendarReady = new __WEBPACK_IMPORTED_MODULE_0__angular_core__["EventEmitter"]();
    }
    BaFullCalendar.prototype.ngAfterViewInit = function () {
        var calendar = __WEBPACK_IMPORTED_MODULE_2_jquery__(this._selector.nativeElement).fullCalendar(this.baFullCalendarConfiguration);
        this.onCalendarReady.emit(calendar);
    };
    return BaFullCalendar;
}());
__decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["Input"])(),
    __metadata("design:type", Object)
], BaFullCalendar.prototype, "baFullCalendarConfiguration", void 0);
__decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["Input"])(),
    __metadata("design:type", String)
], BaFullCalendar.prototype, "baFullCalendarClass", void 0);
__decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["Output"])(),
    __metadata("design:type", Object)
], BaFullCalendar.prototype, "onCalendarReady", void 0);
__decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["ViewChild"])('baFullCalendar'),
    __metadata("design:type", typeof (_a = typeof __WEBPACK_IMPORTED_MODULE_0__angular_core__["ElementRef"] !== "undefined" && __WEBPACK_IMPORTED_MODULE_0__angular_core__["ElementRef"]) === "function" && _a || Object)
], BaFullCalendar.prototype, "_selector", void 0);
BaFullCalendar = __decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["Component"])({
        selector: 'ba-full-calendar',
        template: __webpack_require__(789)
    })
], BaFullCalendar);

var _a;
//# sourceMappingURL=baFullCalendar.component.js.map

/***/ }),

/***/ 547:
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__baFullCalendar_component__ = __webpack_require__(546);
/* harmony namespace reexport (by used) */ __webpack_require__.d(__webpack_exports__, "a", function() { return __WEBPACK_IMPORTED_MODULE_0__baFullCalendar_component__["a"]; });

//# sourceMappingURL=index.js.map

/***/ }),

/***/ 548:
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__(0);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__angular_router__ = __webpack_require__(20);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__services__ = __webpack_require__(71);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_3__global_state__ = __webpack_require__(60);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return BaMenu; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};




var BaMenu = (function () {
    function BaMenu(_router, _service, _state) {
        this._router = _router;
        this._service = _service;
        this._state = _state;
        this.sidebarCollapsed = false;
        this.expandMenu = new __WEBPACK_IMPORTED_MODULE_0__angular_core__["EventEmitter"]();
        this.outOfArea = -200;
    }
    BaMenu.prototype.updateMenu = function (newMenuItems) {
        this.menuItems = newMenuItems;
        this.selectMenuAndNotify();
    };
    BaMenu.prototype.selectMenuAndNotify = function () {
        if (this.menuItems) {
            this.menuItems = this._service.selectMenuItem(this.menuItems);
            this._state.notifyDataChanged('menu.activeLink', this._service.getCurrentItem());
        }
    };
    BaMenu.prototype.ngOnInit = function () {
        var _this = this;
        this._onRouteChange = this._router.events.subscribe(function (event) {
            if (event instanceof __WEBPACK_IMPORTED_MODULE_1__angular_router__["d" /* NavigationEnd */]) {
                if (_this.menuItems) {
                    _this.selectMenuAndNotify();
                }
                else {
                    // on page load we have to wait as event is fired before menu elements are prepared
                    setTimeout(function () { return _this.selectMenuAndNotify(); });
                }
            }
        });
        this._menuItemsSub = this._service.menuItems.subscribe(this.updateMenu.bind(this));
    };
    BaMenu.prototype.ngOnDestroy = function () {
        this._onRouteChange.unsubscribe();
        this._menuItemsSub.unsubscribe();
    };
    BaMenu.prototype.hoverItem = function ($event) {
        this.showHoverElem = true;
        this.hoverElemHeight = $event.currentTarget.clientHeight;
        // TODO: get rid of magic 66 constant
        this.hoverElemTop = $event.currentTarget.getBoundingClientRect().top - 66;
    };
    BaMenu.prototype.toggleSubMenu = function ($event) {
        var submenu = jQuery($event.currentTarget).next();
        if (this.sidebarCollapsed) {
            this.expandMenu.emit(null);
            if (!$event.item.expanded) {
                $event.item.expanded = true;
            }
        }
        else {
            $event.item.expanded = !$event.item.expanded;
            submenu.slideToggle();
        }
        return false;
    };
    return BaMenu;
}());
__decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["Input"])(),
    __metadata("design:type", Boolean)
], BaMenu.prototype, "sidebarCollapsed", void 0);
__decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["Input"])(),
    __metadata("design:type", Number)
], BaMenu.prototype, "menuHeight", void 0);
__decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["Output"])(),
    __metadata("design:type", Object)
], BaMenu.prototype, "expandMenu", void 0);
BaMenu = __decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["Component"])({
        selector: 'ba-menu',
        template: __webpack_require__(790),
        styles: [__webpack_require__(770)]
    }),
    __metadata("design:paramtypes", [typeof (_a = typeof __WEBPACK_IMPORTED_MODULE_1__angular_router__["b" /* Router */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_1__angular_router__["b" /* Router */]) === "function" && _a || Object, typeof (_b = typeof __WEBPACK_IMPORTED_MODULE_2__services__["a" /* BaMenuService */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_2__services__["a" /* BaMenuService */]) === "function" && _b || Object, typeof (_c = typeof __WEBPACK_IMPORTED_MODULE_3__global_state__["a" /* GlobalState */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_3__global_state__["a" /* GlobalState */]) === "function" && _c || Object])
], BaMenu);

var _a, _b, _c;
//# sourceMappingURL=baMenu.component.js.map

/***/ }),

/***/ 549:
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__(0);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return BaMenuItem; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};

var BaMenuItem = (function () {
    function BaMenuItem() {
        this.child = false;
        this.itemHover = new __WEBPACK_IMPORTED_MODULE_0__angular_core__["EventEmitter"]();
        this.toggleSubMenu = new __WEBPACK_IMPORTED_MODULE_0__angular_core__["EventEmitter"]();
    }
    BaMenuItem.prototype.onHoverItem = function ($event) {
        this.itemHover.emit($event);
    };
    BaMenuItem.prototype.onToggleSubMenu = function ($event, item) {
        $event.item = item;
        this.toggleSubMenu.emit($event);
        return false;
    };
    return BaMenuItem;
}());
__decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["Input"])(),
    __metadata("design:type", Object)
], BaMenuItem.prototype, "menuItem", void 0);
__decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["Input"])(),
    __metadata("design:type", Boolean)
], BaMenuItem.prototype, "child", void 0);
__decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["Output"])(),
    __metadata("design:type", Object)
], BaMenuItem.prototype, "itemHover", void 0);
__decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["Output"])(),
    __metadata("design:type", Object)
], BaMenuItem.prototype, "toggleSubMenu", void 0);
BaMenuItem = __decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["Component"])({
        selector: 'ba-menu-item',
        template: __webpack_require__(791),
        styles: [__webpack_require__(771)]
    })
], BaMenuItem);

//# sourceMappingURL=baMenuItem.component.js.map

/***/ }),

/***/ 550:
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__baMenuItem_component__ = __webpack_require__(549);
/* harmony namespace reexport (by used) */ __webpack_require__.d(__webpack_exports__, "a", function() { return __WEBPACK_IMPORTED_MODULE_0__baMenuItem_component__["a"]; });

//# sourceMappingURL=index.js.map

/***/ }),

/***/ 551:
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__baMenu_component__ = __webpack_require__(548);
/* harmony namespace reexport (by used) */ __webpack_require__.d(__webpack_exports__, "a", function() { return __WEBPACK_IMPORTED_MODULE_0__baMenu_component__["a"]; });

//# sourceMappingURL=index.js.map

/***/ }),

/***/ 552:
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__(0);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__baMsgCenter_service__ = __webpack_require__(553);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return BaMsgCenter; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};


var BaMsgCenter = (function () {
    function BaMsgCenter(_baMsgCenterService) {
        this._baMsgCenterService = _baMsgCenterService;
        this.notifications = this._baMsgCenterService.getNotifications();
        this.messages = this._baMsgCenterService.getMessages();
    }
    return BaMsgCenter;
}());
BaMsgCenter = __decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["Component"])({
        selector: 'ba-msg-center',
        providers: [__WEBPACK_IMPORTED_MODULE_1__baMsgCenter_service__["a" /* BaMsgCenterService */]],
        styles: [__webpack_require__(772)],
        template: __webpack_require__(792)
    }),
    __metadata("design:paramtypes", [typeof (_a = typeof __WEBPACK_IMPORTED_MODULE_1__baMsgCenter_service__["a" /* BaMsgCenterService */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_1__baMsgCenter_service__["a" /* BaMsgCenterService */]) === "function" && _a || Object])
], BaMsgCenter);

var _a;
//# sourceMappingURL=baMsgCenter.component.js.map

/***/ }),

/***/ 553:
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__(0);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return BaMsgCenterService; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};

var BaMsgCenterService = (function () {
    function BaMsgCenterService() {
        this._notifications = [
            {
                name: 'Vlad',
                text: 'Vlad posted a new article.',
                time: '1 min ago'
            },
            {
                name: 'Kostya',
                text: 'Kostya changed his contact information.',
                time: '2 hrs ago'
            },
            {
                image: 'assets/img/shopping-cart.svg',
                text: 'New orders received.',
                time: '5 hrs ago'
            },
            {
                name: 'Andrey',
                text: 'Andrey replied to your comment.',
                time: '1 day ago'
            },
            {
                name: 'Nasta',
                text: 'Today is Nasta\'s birthday.',
                time: '2 days ago'
            },
            {
                image: 'assets/img/comments.svg',
                text: 'New comments on your post.',
                time: '3 days ago'
            },
            {
                name: 'Kostya',
                text: 'Kostya invited you to join the event.',
                time: '1 week ago'
            }
        ];
        this._messages = [
            {
                name: 'Nasta',
                text: 'After you get up and running, you can place Font Awesome icons just about...',
                time: '1 min ago'
            },
            {
                name: 'Vlad',
                text: 'You asked, Font Awesome delivers with 40 shiny new icons in version 4.2.',
                time: '2 hrs ago'
            },
            {
                name: 'Kostya',
                text: 'Want to request new icons? Here\'s how. Need vectors or want to use on the...',
                time: '10 hrs ago'
            },
            {
                name: 'Andrey',
                text: 'Explore your passions and discover new ones by getting involved. Stretch your...',
                time: '1 day ago'
            },
            {
                name: 'Nasta',
                text: 'Get to know who we are - from the inside out. From our history and culture, to the...',
                time: '1 day ago'
            },
            {
                name: 'Kostya',
                text: 'Need some support to reach your goals? Apply for scholarships across a variety of...',
                time: '2 days ago'
            },
            {
                name: 'Vlad',
                text: 'Wrap the dropdown\'s trigger and the dropdown menu within .dropdown, or...',
                time: '1 week ago'
            }
        ];
    }
    BaMsgCenterService.prototype.getMessages = function () {
        return this._messages;
    };
    BaMsgCenterService.prototype.getNotifications = function () {
        return this._notifications;
    };
    return BaMsgCenterService;
}());
BaMsgCenterService = __decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["Injectable"])()
], BaMsgCenterService);

//# sourceMappingURL=baMsgCenter.service.js.map

/***/ }),

/***/ 554:
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__baMsgCenter_component__ = __webpack_require__(552);
/* harmony namespace reexport (by used) */ __webpack_require__.d(__webpack_exports__, "a", function() { return __WEBPACK_IMPORTED_MODULE_0__baMsgCenter_component__["a"]; });

//# sourceMappingURL=index.js.map

/***/ }),

/***/ 555:
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__(0);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__angular_forms__ = __webpack_require__(22);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return BaMultiCheckbox; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
var __param = (this && this.__param) || function (paramIndex, decorator) {
    return function (target, key) { decorator(target, key, paramIndex); }
};


var BaMultiCheckbox = (function () {
    function BaMultiCheckbox(state) {
        this.model = state;
        state.valueAccessor = this;
    }
    BaMultiCheckbox.prototype.getProp = function (item, propName) {
        var prop = this.propertiesMapping[propName];
        if (!prop) {
            return item[propName];
        }
        else if (typeof prop === 'function') {
            return prop(item);
        }
        return item[prop];
    };
    BaMultiCheckbox.prototype.onChange = function (value) { };
    BaMultiCheckbox.prototype.onTouch = function (value) { };
    BaMultiCheckbox.prototype.writeValue = function (state) {
        this.state = state;
    };
    BaMultiCheckbox.prototype.registerOnChange = function (fn) {
        this.onChange = function (state) {
            this.writeValue(state);
            this.model.viewToModelUpdate(state);
        };
    };
    BaMultiCheckbox.prototype.registerOnTouched = function (fn) { this.onTouch = fn; };
    return BaMultiCheckbox;
}());
__decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["Input"])(),
    __metadata("design:type", String)
], BaMultiCheckbox.prototype, "baMultiCheckboxClass", void 0);
__decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["Input"])(),
    __metadata("design:type", Object)
], BaMultiCheckbox.prototype, "propertiesMapping", void 0);
BaMultiCheckbox = __decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["Component"])({
        selector: 'ba-multi-checkbox[ngModel]',
        template: __webpack_require__(793),
        styles: [__webpack_require__(773)],
        providers: [__WEBPACK_IMPORTED_MODULE_1__angular_forms__["NgModel"]]
    }),
    __param(0, __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["Self"])()),
    __metadata("design:paramtypes", [typeof (_a = typeof __WEBPACK_IMPORTED_MODULE_1__angular_forms__["NgModel"] !== "undefined" && __WEBPACK_IMPORTED_MODULE_1__angular_forms__["NgModel"]) === "function" && _a || Object])
], BaMultiCheckbox);

var _a;
//# sourceMappingURL=baMultiCheckbox.component.js.map

/***/ }),

/***/ 556:
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__baMultiCheckbox_component__ = __webpack_require__(555);
/* harmony namespace reexport (by used) */ __webpack_require__.d(__webpack_exports__, "a", function() { return __WEBPACK_IMPORTED_MODULE_0__baMultiCheckbox_component__["a"]; });

//# sourceMappingURL=index.js.map

/***/ }),

/***/ 557:
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__core_services_utility_service__ = __webpack_require__(146);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__core_common_url_constants__ = __webpack_require__(59);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__angular_core__ = __webpack_require__(0);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_3__core_common_system_constants__ = __webpack_require__(33);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_4__core_services_authen_service__ = __webpack_require__(80);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_5__global_state__ = __webpack_require__(60);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return BaPageTop; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};






var BaPageTop = (function () {
    function BaPageTop(_state, utilityService, authenService) {
        var _this = this;
        this._state = _state;
        this.utilityService = utilityService;
        this.authenService = authenService;
        this.isScrolled = false;
        this.isMenuCollapsed = false;
        this._state.subscribe('menu.isCollapsed', function (isCollapsed) {
            _this.isMenuCollapsed = isCollapsed;
        });
    }
    BaPageTop.prototype.toggleMenu = function () {
        this.isMenuCollapsed = !this.isMenuCollapsed;
        this._state.notifyDataChanged('menu.isCollapsed', this.isMenuCollapsed);
        return false;
    };
    BaPageTop.prototype.scrolledChanged = function (isScrolled) {
        this.isScrolled = isScrolled;
    };
    BaPageTop.prototype.ngOnInit = function () {
        this.user = JSON.parse(localStorage.getItem(__WEBPACK_IMPORTED_MODULE_3__core_common_system_constants__["a" /* SystemConstants */].CURRENT_USER));
    };
    BaPageTop.prototype.logout = function () {
        localStorage.removeItem(__WEBPACK_IMPORTED_MODULE_3__core_common_system_constants__["a" /* SystemConstants */].CURRENT_USER);
        this.utilityService.navigate(__WEBPACK_IMPORTED_MODULE_1__core_common_url_constants__["a" /* UrlConstants */].LOGIN);
    };
    return BaPageTop;
}());
BaPageTop = __decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_2__angular_core__["Component"])({
        selector: 'ba-page-top',
        template: __webpack_require__(794),
        styles: [__webpack_require__(774)]
    }),
    __metadata("design:paramtypes", [typeof (_a = typeof __WEBPACK_IMPORTED_MODULE_5__global_state__["a" /* GlobalState */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_5__global_state__["a" /* GlobalState */]) === "function" && _a || Object, typeof (_b = typeof __WEBPACK_IMPORTED_MODULE_0__core_services_utility_service__["a" /* UtilityService */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_0__core_services_utility_service__["a" /* UtilityService */]) === "function" && _b || Object, typeof (_c = typeof __WEBPACK_IMPORTED_MODULE_4__core_services_authen_service__["a" /* AuthenService */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_4__core_services_authen_service__["a" /* AuthenService */]) === "function" && _c || Object])
], BaPageTop);

var _a, _b, _c;
//# sourceMappingURL=baPageTop.component.js.map

/***/ }),

/***/ 558:
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__baPageTop_component__ = __webpack_require__(557);
/* harmony namespace reexport (by used) */ __webpack_require__.d(__webpack_exports__, "a", function() { return __WEBPACK_IMPORTED_MODULE_0__baPageTop_component__["a"]; });

//# sourceMappingURL=index.js.map

/***/ }),

/***/ 559:
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__(0);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1_ngx_uploader__ = __webpack_require__(199);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return BaPictureUploader; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};


var BaPictureUploader = (function () {
    function BaPictureUploader(renderer) {
        this.renderer = renderer;
        this.defaultPicture = '';
        this.picture = '';
        this.uploaderOptions = { url: '' };
        this.canDelete = true;
        this.onUpload = new __WEBPACK_IMPORTED_MODULE_0__angular_core__["EventEmitter"]();
        this.onUploadCompleted = new __WEBPACK_IMPORTED_MODULE_0__angular_core__["EventEmitter"]();
    }
    BaPictureUploader.prototype.beforeUpload = function (uploadingFile) {
        var files = this._fileUpload.nativeElement.files;
        if (files.length) {
            var file = files[0];
            this._changePicture(file);
            if (!this._canUploadOnServer()) {
                uploadingFile.setAbort();
            }
            else {
                this.uploadInProgress = true;
            }
        }
    };
    BaPictureUploader.prototype.bringFileSelector = function () {
        this.renderer.invokeElementMethod(this._fileUpload.nativeElement, 'click');
        return false;
    };
    BaPictureUploader.prototype.removePicture = function () {
        this.picture = '';
        return false;
    };
    BaPictureUploader.prototype._changePicture = function (file) {
        var _this = this;
        var reader = new FileReader();
        reader.addEventListener('load', function (event) {
            _this.picture = event.target.result;
        }, false);
        reader.readAsDataURL(file);
    };
    BaPictureUploader.prototype._onUpload = function (data) {
        if (data['done'] || data['abort'] || data['error']) {
            this._onUploadCompleted(data);
        }
        else {
            this.onUpload.emit(data);
        }
    };
    BaPictureUploader.prototype._onUploadCompleted = function (data) {
        this.uploadInProgress = false;
        this.onUploadCompleted.emit(data);
    };
    BaPictureUploader.prototype._canUploadOnServer = function () {
        return !!this.uploaderOptions['url'];
    };
    return BaPictureUploader;
}());
__decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["Input"])(),
    __metadata("design:type", String)
], BaPictureUploader.prototype, "defaultPicture", void 0);
__decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["Input"])(),
    __metadata("design:type", String)
], BaPictureUploader.prototype, "picture", void 0);
__decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["Input"])(),
    __metadata("design:type", typeof (_a = typeof __WEBPACK_IMPORTED_MODULE_1_ngx_uploader__["b" /* NgUploaderOptions */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_1_ngx_uploader__["b" /* NgUploaderOptions */]) === "function" && _a || Object)
], BaPictureUploader.prototype, "uploaderOptions", void 0);
__decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["Input"])(),
    __metadata("design:type", Boolean)
], BaPictureUploader.prototype, "canDelete", void 0);
__decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["Output"])(),
    __metadata("design:type", Object)
], BaPictureUploader.prototype, "onUpload", void 0);
__decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["Output"])(),
    __metadata("design:type", Object)
], BaPictureUploader.prototype, "onUploadCompleted", void 0);
__decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["ViewChild"])('fileUpload'),
    __metadata("design:type", typeof (_b = typeof __WEBPACK_IMPORTED_MODULE_0__angular_core__["ElementRef"] !== "undefined" && __WEBPACK_IMPORTED_MODULE_0__angular_core__["ElementRef"]) === "function" && _b || Object)
], BaPictureUploader.prototype, "_fileUpload", void 0);
BaPictureUploader = __decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["Component"])({
        selector: 'ba-picture-uploader',
        styles: [__webpack_require__(775)],
        template: __webpack_require__(795),
    }),
    __metadata("design:paramtypes", [typeof (_c = typeof __WEBPACK_IMPORTED_MODULE_0__angular_core__["Renderer"] !== "undefined" && __WEBPACK_IMPORTED_MODULE_0__angular_core__["Renderer"]) === "function" && _c || Object])
], BaPictureUploader);

var _a, _b, _c;
//# sourceMappingURL=baPictureUploader.component.js.map

/***/ }),

/***/ 560:
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__baPictureUploader_component__ = __webpack_require__(559);
/* harmony namespace reexport (by used) */ __webpack_require__.d(__webpack_exports__, "a", function() { return __WEBPACK_IMPORTED_MODULE_0__baPictureUploader_component__["a"]; });

//# sourceMappingURL=index.js.map

/***/ }),

/***/ 561:
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__(0);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__global_state__ = __webpack_require__(60);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__theme__ = __webpack_require__(34);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return BaSidebar; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};



var BaSidebar = (function () {
    function BaSidebar(_elementRef, _state) {
        var _this = this;
        this._elementRef = _elementRef;
        this._state = _state;
        this.isMenuCollapsed = false;
        this.isMenuShouldCollapsed = false;
        this._state.subscribe('menu.isCollapsed', function (isCollapsed) {
            _this.isMenuCollapsed = isCollapsed;
        });
    }
    BaSidebar.prototype.ngOnInit = function () {
        if (this._shouldMenuCollapse()) {
            this.menuCollapse();
        }
    };
    BaSidebar.prototype.ngAfterViewInit = function () {
        var _this = this;
        setTimeout(function () { return _this.updateSidebarHeight(); });
    };
    BaSidebar.prototype.onWindowResize = function () {
        var isMenuShouldCollapsed = this._shouldMenuCollapse();
        if (this.isMenuShouldCollapsed !== isMenuShouldCollapsed) {
            this.menuCollapseStateChange(isMenuShouldCollapsed);
        }
        this.isMenuShouldCollapsed = isMenuShouldCollapsed;
        this.updateSidebarHeight();
    };
    BaSidebar.prototype.menuExpand = function () {
        this.menuCollapseStateChange(false);
    };
    BaSidebar.prototype.menuCollapse = function () {
        this.menuCollapseStateChange(true);
    };
    BaSidebar.prototype.menuCollapseStateChange = function (isCollapsed) {
        this.isMenuCollapsed = isCollapsed;
        this._state.notifyDataChanged('menu.isCollapsed', this.isMenuCollapsed);
    };
    BaSidebar.prototype.updateSidebarHeight = function () {
        // TODO: get rid of magic 84 constant
        this.menuHeight = this._elementRef.nativeElement.childNodes[0].clientHeight - 84;
    };
    BaSidebar.prototype._shouldMenuCollapse = function () {
        return window.innerWidth <= __WEBPACK_IMPORTED_MODULE_2__theme__["e" /* layoutSizes */].resWidthCollapseSidebar;
    };
    return BaSidebar;
}());
__decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["HostListener"])('window:resize'),
    __metadata("design:type", Function),
    __metadata("design:paramtypes", []),
    __metadata("design:returntype", void 0)
], BaSidebar.prototype, "onWindowResize", null);
BaSidebar = __decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["Component"])({
        selector: 'ba-sidebar',
        template: __webpack_require__(796),
        styles: [__webpack_require__(776)]
    }),
    __metadata("design:paramtypes", [typeof (_a = typeof __WEBPACK_IMPORTED_MODULE_0__angular_core__["ElementRef"] !== "undefined" && __WEBPACK_IMPORTED_MODULE_0__angular_core__["ElementRef"]) === "function" && _a || Object, typeof (_b = typeof __WEBPACK_IMPORTED_MODULE_1__global_state__["a" /* GlobalState */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_1__global_state__["a" /* GlobalState */]) === "function" && _b || Object])
], BaSidebar);

var _a, _b;
//# sourceMappingURL=baSidebar.component.js.map

/***/ }),

/***/ 562:
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__baSidebar_component__ = __webpack_require__(561);
/* harmony namespace reexport (by used) */ __webpack_require__.d(__webpack_exports__, "a", function() { return __WEBPACK_IMPORTED_MODULE_0__baSidebar_component__["a"]; });

//# sourceMappingURL=index.js.map

/***/ }),

/***/ 563:
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__baPageTop__ = __webpack_require__(558);
/* harmony namespace reexport (by used) */ __webpack_require__.d(__webpack_exports__, "l", function() { return __WEBPACK_IMPORTED_MODULE_0__baPageTop__["a"]; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__baMsgCenter__ = __webpack_require__(554);
/* harmony namespace reexport (by used) */ __webpack_require__.d(__webpack_exports__, "j", function() { return __WEBPACK_IMPORTED_MODULE_1__baMsgCenter__["a"]; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__baSidebar__ = __webpack_require__(562);
/* harmony namespace reexport (by used) */ __webpack_require__.d(__webpack_exports__, "n", function() { return __WEBPACK_IMPORTED_MODULE_2__baSidebar__["a"]; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_3__baMenu_components_baMenuItem__ = __webpack_require__(550);
/* harmony namespace reexport (by used) */ __webpack_require__.d(__webpack_exports__, "h", function() { return __WEBPACK_IMPORTED_MODULE_3__baMenu_components_baMenuItem__["a"]; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_4__baMenu__ = __webpack_require__(551);
/* harmony namespace reexport (by used) */ __webpack_require__.d(__webpack_exports__, "i", function() { return __WEBPACK_IMPORTED_MODULE_4__baMenu__["a"]; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_5__baContentTop__ = __webpack_require__(543);
/* harmony namespace reexport (by used) */ __webpack_require__.d(__webpack_exports__, "f", function() { return __WEBPACK_IMPORTED_MODULE_5__baContentTop__["a"]; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_6__baCard__ = __webpack_require__(537);
/* harmony namespace reexport (by used) */ __webpack_require__.d(__webpack_exports__, "c", function() { return __WEBPACK_IMPORTED_MODULE_6__baCard__["a"]; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_7__baAmChart__ = __webpack_require__(531);
/* harmony namespace reexport (by used) */ __webpack_require__.d(__webpack_exports__, "a", function() { return __WEBPACK_IMPORTED_MODULE_7__baAmChart__["a"]; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_8__baChartistChart__ = __webpack_require__(539);
/* harmony namespace reexport (by used) */ __webpack_require__.d(__webpack_exports__, "d", function() { return __WEBPACK_IMPORTED_MODULE_8__baChartistChart__["a"]; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_9__baBackTop__ = __webpack_require__(533);
/* harmony namespace reexport (by used) */ __webpack_require__.d(__webpack_exports__, "b", function() { return __WEBPACK_IMPORTED_MODULE_9__baBackTop__["a"]; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_10__baFullCalendar__ = __webpack_require__(547);
/* harmony namespace reexport (by used) */ __webpack_require__.d(__webpack_exports__, "g", function() { return __WEBPACK_IMPORTED_MODULE_10__baFullCalendar__["a"]; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_11__baPictureUploader__ = __webpack_require__(560);
/* harmony namespace reexport (by used) */ __webpack_require__.d(__webpack_exports__, "m", function() { return __WEBPACK_IMPORTED_MODULE_11__baPictureUploader__["a"]; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_12__baCheckbox__ = __webpack_require__(541);
/* harmony namespace reexport (by used) */ __webpack_require__.d(__webpack_exports__, "e", function() { return __WEBPACK_IMPORTED_MODULE_12__baCheckbox__["a"]; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_13__baMultiCheckbox__ = __webpack_require__(556);
/* harmony namespace reexport (by used) */ __webpack_require__.d(__webpack_exports__, "k", function() { return __WEBPACK_IMPORTED_MODULE_13__baMultiCheckbox__["a"]; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_14__baFileUploader__ = __webpack_require__(545);
/* harmony namespace reexport (by used) */ __webpack_require__.d(__webpack_exports__, "o", function() { return __WEBPACK_IMPORTED_MODULE_14__baFileUploader__["a"]; });















//# sourceMappingURL=index.js.map

/***/ }),

/***/ 564:
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__(0);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return BaScrollPosition; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};

var BaScrollPosition = (function () {
    function BaScrollPosition() {
        this.scrollChange = new __WEBPACK_IMPORTED_MODULE_0__angular_core__["EventEmitter"]();
    }
    BaScrollPosition.prototype.ngOnInit = function () {
        this.onWindowScroll();
    };
    BaScrollPosition.prototype.onWindowScroll = function () {
        var isScrolled = window.scrollY > this.maxHeight;
        if (isScrolled !== this._isScrolled) {
            this._isScrolled = isScrolled;
            this.scrollChange.emit(isScrolled);
        }
    };
    return BaScrollPosition;
}());
__decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["Input"])(),
    __metadata("design:type", Number)
], BaScrollPosition.prototype, "maxHeight", void 0);
__decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["Output"])(),
    __metadata("design:type", typeof (_a = typeof __WEBPACK_IMPORTED_MODULE_0__angular_core__["EventEmitter"] !== "undefined" && __WEBPACK_IMPORTED_MODULE_0__angular_core__["EventEmitter"]) === "function" && _a || Object)
], BaScrollPosition.prototype, "scrollChange", void 0);
__decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["HostListener"])('window:scroll'),
    __metadata("design:type", Function),
    __metadata("design:paramtypes", []),
    __metadata("design:returntype", void 0)
], BaScrollPosition.prototype, "onWindowScroll", null);
BaScrollPosition = __decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["Directive"])({
        selector: '[baScrollPosition]'
    })
], BaScrollPosition);

var _a;
//# sourceMappingURL=baScrollPosition.directive.js.map

/***/ }),

/***/ 565:
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__baScrollPosition_directive__ = __webpack_require__(564);
/* harmony namespace reexport (by used) */ __webpack_require__.d(__webpack_exports__, "a", function() { return __WEBPACK_IMPORTED_MODULE_0__baScrollPosition_directive__["a"]; });

//# sourceMappingURL=index.js.map

/***/ }),

/***/ 566:
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__(0);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1_jquery_slimscroll__ = __webpack_require__(781);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1_jquery_slimscroll___default = __webpack_require__.n(__WEBPACK_IMPORTED_MODULE_1_jquery_slimscroll__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return BaSlimScroll; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};


var BaSlimScroll = (function () {
    function BaSlimScroll(_elementRef) {
        this._elementRef = _elementRef;
    }
    BaSlimScroll.prototype.ngOnChanges = function (changes) {
        this._scroll();
    };
    BaSlimScroll.prototype._scroll = function () {
        this._destroy();
        this._init();
    };
    BaSlimScroll.prototype._init = function () {
        jQuery(this._elementRef.nativeElement).slimScroll(this.baSlimScrollOptions);
    };
    BaSlimScroll.prototype._destroy = function () {
        jQuery(this._elementRef.nativeElement).slimScroll({ destroy: true });
    };
    return BaSlimScroll;
}());
__decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["Input"])(),
    __metadata("design:type", Object)
], BaSlimScroll.prototype, "baSlimScrollOptions", void 0);
BaSlimScroll = __decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["Directive"])({
        selector: '[baSlimScroll]'
    }),
    __metadata("design:paramtypes", [typeof (_a = typeof __WEBPACK_IMPORTED_MODULE_0__angular_core__["ElementRef"] !== "undefined" && __WEBPACK_IMPORTED_MODULE_0__angular_core__["ElementRef"]) === "function" && _a || Object])
], BaSlimScroll);

var _a;
//# sourceMappingURL=baSlimScroll.directive.js.map

/***/ }),

/***/ 567:
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__baSlimScroll_directive__ = __webpack_require__(566);
/* harmony namespace reexport (by used) */ __webpack_require__.d(__webpack_exports__, "a", function() { return __WEBPACK_IMPORTED_MODULE_0__baSlimScroll_directive__["a"]; });

//# sourceMappingURL=index.js.map

/***/ }),

/***/ 568:
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__(0);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__theme__ = __webpack_require__(34);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return BaThemeRun; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};


var BaThemeRun = (function () {
    function BaThemeRun(_baConfig) {
        this._baConfig = _baConfig;
        this._classes = [];
    }
    BaThemeRun.prototype.ngOnInit = function () {
        this._assignTheme();
        this._assignMobile();
    };
    BaThemeRun.prototype._assignTheme = function () {
        this._addClass(this._baConfig.get().theme.name);
    };
    BaThemeRun.prototype._assignMobile = function () {
        if (__webpack_require__.i(__WEBPACK_IMPORTED_MODULE_1__theme__["d" /* isMobile */])()) {
            this._addClass('mobile');
        }
    };
    BaThemeRun.prototype._addClass = function (cls) {
        this._classes.push(cls);
        this.classesString = this._classes.join(' ');
    };
    return BaThemeRun;
}());
__decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["HostBinding"])('class'),
    __metadata("design:type", String)
], BaThemeRun.prototype, "classesString", void 0);
BaThemeRun = __decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["Directive"])({
        selector: '[baThemeRun]'
    }),
    __metadata("design:paramtypes", [typeof (_a = typeof __WEBPACK_IMPORTED_MODULE_1__theme__["b" /* BaThemeConfigProvider */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_1__theme__["b" /* BaThemeConfigProvider */]) === "function" && _a || Object])
], BaThemeRun);

var _a;
//# sourceMappingURL=baThemeRun.directive.js.map

/***/ }),

/***/ 569:
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__baThemeRun_directive__ = __webpack_require__(568);
/* harmony namespace reexport (by used) */ __webpack_require__.d(__webpack_exports__, "a", function() { return __WEBPACK_IMPORTED_MODULE_0__baThemeRun_directive__["a"]; });

//# sourceMappingURL=index.js.map

/***/ }),

/***/ 570:
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__baScrollPosition__ = __webpack_require__(565);
/* harmony namespace reexport (by used) */ __webpack_require__.d(__webpack_exports__, "a", function() { return __WEBPACK_IMPORTED_MODULE_0__baScrollPosition__["a"]; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__baThemeRun__ = __webpack_require__(569);
/* harmony namespace reexport (by used) */ __webpack_require__.d(__webpack_exports__, "c", function() { return __WEBPACK_IMPORTED_MODULE_1__baThemeRun__["a"]; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__baSlimScroll__ = __webpack_require__(567);
/* harmony namespace reexport (by used) */ __webpack_require__.d(__webpack_exports__, "b", function() { return __WEBPACK_IMPORTED_MODULE_2__baSlimScroll__["a"]; });



//# sourceMappingURL=index.js.map

/***/ }),

/***/ 571:
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__(0);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__theme__ = __webpack_require__(34);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return BaAppPicturePipe; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};


var BaAppPicturePipe = (function () {
    function BaAppPicturePipe() {
    }
    BaAppPicturePipe.prototype.transform = function (input) {
        return __WEBPACK_IMPORTED_MODULE_1__theme__["a" /* layoutPaths */].images.root + input;
    };
    return BaAppPicturePipe;
}());
BaAppPicturePipe = __decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["Pipe"])({ name: 'baAppPicture' })
], BaAppPicturePipe);

//# sourceMappingURL=baAppPicture.pipe.js.map

/***/ }),

/***/ 572:
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__baAppPicture_pipe__ = __webpack_require__(571);
/* harmony namespace reexport (by used) */ __webpack_require__.d(__webpack_exports__, "a", function() { return __WEBPACK_IMPORTED_MODULE_0__baAppPicture_pipe__["a"]; });

//# sourceMappingURL=index.js.map

/***/ }),

/***/ 573:
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__(0);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__theme__ = __webpack_require__(34);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return BaKameleonPicturePipe; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};


var BaKameleonPicturePipe = (function () {
    function BaKameleonPicturePipe() {
    }
    BaKameleonPicturePipe.prototype.transform = function (input) {
        return __WEBPACK_IMPORTED_MODULE_1__theme__["a" /* layoutPaths */].images.root + 'theme/icon/kameleon/' + input + '.svg';
    };
    return BaKameleonPicturePipe;
}());
BaKameleonPicturePipe = __decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["Pipe"])({ name: 'baKameleonPicture' })
], BaKameleonPicturePipe);

//# sourceMappingURL=baKameleonPicture.pipe.js.map

/***/ }),

/***/ 574:
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__baKameleonPicture_pipe__ = __webpack_require__(573);
/* harmony namespace reexport (by used) */ __webpack_require__.d(__webpack_exports__, "a", function() { return __WEBPACK_IMPORTED_MODULE_0__baKameleonPicture_pipe__["a"]; });

//# sourceMappingURL=index.js.map

/***/ }),

/***/ 575:
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__(0);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__theme__ = __webpack_require__(34);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return BaProfilePicturePipe; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};


var BaProfilePicturePipe = (function () {
    function BaProfilePicturePipe() {
    }
    BaProfilePicturePipe.prototype.transform = function (input, ext) {
        if (ext === void 0) { ext = 'png'; }
        return __WEBPACK_IMPORTED_MODULE_1__theme__["a" /* layoutPaths */].images.profile + input + '.' + ext;
    };
    return BaProfilePicturePipe;
}());
BaProfilePicturePipe = __decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["Pipe"])({ name: 'baProfilePicture' })
], BaProfilePicturePipe);

//# sourceMappingURL=baProfilePicture.pipe.js.map

/***/ }),

/***/ 576:
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__baProfilePicture_pipe__ = __webpack_require__(575);
/* harmony namespace reexport (by used) */ __webpack_require__.d(__webpack_exports__, "a", function() { return __WEBPACK_IMPORTED_MODULE_0__baProfilePicture_pipe__["a"]; });

//# sourceMappingURL=index.js.map

/***/ }),

/***/ 577:
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__baProfilePicture__ = __webpack_require__(576);
/* harmony namespace reexport (by used) */ __webpack_require__.d(__webpack_exports__, "c", function() { return __WEBPACK_IMPORTED_MODULE_0__baProfilePicture__["a"]; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__baAppPicture__ = __webpack_require__(572);
/* harmony namespace reexport (by used) */ __webpack_require__.d(__webpack_exports__, "a", function() { return __WEBPACK_IMPORTED_MODULE_1__baAppPicture__["a"]; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__baKameleonPicture__ = __webpack_require__(574);
/* harmony namespace reexport (by used) */ __webpack_require__.d(__webpack_exports__, "b", function() { return __WEBPACK_IMPORTED_MODULE_2__baKameleonPicture__["a"]; });



//# sourceMappingURL=index.js.map

/***/ }),

/***/ 578:
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__(0);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return BaImageLoaderService; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};

var BaImageLoaderService = (function () {
    function BaImageLoaderService() {
    }
    BaImageLoaderService.prototype.load = function (src) {
        return new Promise(function (resolve, reject) {
            var img = new Image();
            img.src = src;
            img.onload = function () {
                resolve('Image with src ' + src + ' loaded successfully.');
            };
        });
    };
    return BaImageLoaderService;
}());
BaImageLoaderService = __decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["Injectable"])()
], BaImageLoaderService);

//# sourceMappingURL=baImageLoader.service.js.map

/***/ }),

/***/ 579:
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__baImageLoader_service__ = __webpack_require__(578);
/* harmony namespace reexport (by used) */ __webpack_require__.d(__webpack_exports__, "a", function() { return __WEBPACK_IMPORTED_MODULE_0__baImageLoader_service__["a"]; });

//# sourceMappingURL=index.js.map

/***/ }),

/***/ 580:
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__(0);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__angular_router__ = __webpack_require__(20);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2_lodash__ = __webpack_require__(204);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2_lodash___default = __webpack_require__.n(__WEBPACK_IMPORTED_MODULE_2_lodash__);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_3_rxjs_BehaviorSubject__ = __webpack_require__(209);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_3_rxjs_BehaviorSubject___default = __webpack_require__.n(__WEBPACK_IMPORTED_MODULE_3_rxjs_BehaviorSubject__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return BaMenuService; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};




var BaMenuService = (function () {
    function BaMenuService(_router) {
        this._router = _router;
        this.menuItems = new __WEBPACK_IMPORTED_MODULE_3_rxjs_BehaviorSubject__["BehaviorSubject"]([]);
        this._currentMenuItem = {};
    }
    /**
     * Updates the routes in the menu
     *
     * @param {Routes} routes Type compatible with app.menu.ts
     */
    BaMenuService.prototype.updateMenuByRoutes = function (routes) {
        var convertedRoutes = this.convertRoutesToMenus(__WEBPACK_IMPORTED_MODULE_2_lodash__["cloneDeep"](routes));
        this.menuItems.next(convertedRoutes);
    };
    BaMenuService.prototype.convertRoutesToMenus = function (routes) {
        var items = this._convertArrayToItems(routes);
        return this._skipEmpty(items);
    };
    BaMenuService.prototype.getCurrentItem = function () {
        return this._currentMenuItem;
    };
    BaMenuService.prototype.selectMenuItem = function (menuItems) {
        var _this = this;
        var items = [];
        menuItems.forEach(function (item) {
            _this._selectItem(item);
            if (item.selected) {
                _this._currentMenuItem = item;
            }
            if (item.children && item.children.length > 0) {
                item.children = _this.selectMenuItem(item.children);
            }
            items.push(item);
        });
        return items;
    };
    BaMenuService.prototype._skipEmpty = function (items) {
        var menu = [];
        items.forEach(function (item) {
            var menuItem;
            if (item.skip) {
                if (item.children && item.children.length > 0) {
                    menuItem = item.children;
                }
            }
            else {
                menuItem = item;
            }
            if (menuItem) {
                menu.push(menuItem);
            }
        });
        return [].concat.apply([], menu);
    };
    BaMenuService.prototype._convertArrayToItems = function (routes, parent) {
        var _this = this;
        var items = [];
        routes.forEach(function (route) {
            items.push(_this._convertObjectToItem(route, parent));
        });
        return items;
    };
    BaMenuService.prototype._convertObjectToItem = function (object, parent) {
        var item = {};
        if (object.data && object.data.menu) {
            // this is a menu object
            item = object.data.menu;
            item.route = object;
            delete item.route.data.menu;
        }
        else {
            item.route = object;
            item.skip = true;
        }
        // we have to collect all paths to correctly build the url then
        if (Array.isArray(item.route.path)) {
            item.route.paths = item.route.path;
        }
        else {
            item.route.paths = parent && parent.route && parent.route.paths ? parent.route.paths.slice(0) : ['/'];
            if (!!item.route.path)
                item.route.paths.push(item.route.path);
        }
        if (object.children && object.children.length > 0) {
            item.children = this._convertArrayToItems(object.children, item);
        }
        var prepared = this._prepareItem(item);
        // if current item is selected or expanded - then parent is expanded too
        if ((prepared.selected || prepared.expanded) && parent) {
            parent.expanded = true;
        }
        return prepared;
    };
    BaMenuService.prototype._prepareItem = function (object) {
        if (!object.skip) {
            object.target = object.target || '';
            object.pathMatch = object.pathMatch || 'full';
            return this._selectItem(object);
        }
        return object;
    };
    BaMenuService.prototype._selectItem = function (object) {
        object.selected = this._router.isActive(this._router.createUrlTree(object.route.paths), object.pathMatch === 'full');
        return object;
    };
    return BaMenuService;
}());
BaMenuService = __decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["Injectable"])(),
    __metadata("design:paramtypes", [typeof (_a = typeof __WEBPACK_IMPORTED_MODULE_1__angular_router__["b" /* Router */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_1__angular_router__["b" /* Router */]) === "function" && _a || Object])
], BaMenuService);

var _a;
//# sourceMappingURL=baMenu.service.js.map

/***/ }),

/***/ 581:
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__baMenu_service__ = __webpack_require__(580);
/* harmony namespace reexport (by used) */ __webpack_require__.d(__webpack_exports__, "a", function() { return __WEBPACK_IMPORTED_MODULE_0__baMenu_service__["a"]; });

//# sourceMappingURL=index.js.map

/***/ }),

/***/ 582:
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__(0);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return BaThemePreloader; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};

var BaThemePreloader = BaThemePreloader_1 = (function () {
    function BaThemePreloader() {
    }
    BaThemePreloader.registerLoader = function (method) {
        BaThemePreloader_1._loaders.push(method);
    };
    BaThemePreloader.clear = function () {
        BaThemePreloader_1._loaders = [];
    };
    BaThemePreloader.load = function () {
        return new Promise(function (resolve, reject) {
            BaThemePreloader_1._executeAll(resolve);
        });
    };
    BaThemePreloader._executeAll = function (done) {
        setTimeout(function () {
            Promise.all(BaThemePreloader_1._loaders).then(function (values) {
                done.call(null, values);
            }).catch(function (error) {
                console.error(error);
            });
        });
    };
    return BaThemePreloader;
}());
BaThemePreloader._loaders = [];
BaThemePreloader = BaThemePreloader_1 = __decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["Injectable"])()
], BaThemePreloader);

var BaThemePreloader_1;
//# sourceMappingURL=baThemePreloader.service.js.map

/***/ }),

/***/ 583:
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__baThemePreloader_service__ = __webpack_require__(582);
/* harmony namespace reexport (by used) */ __webpack_require__.d(__webpack_exports__, "a", function() { return __WEBPACK_IMPORTED_MODULE_0__baThemePreloader_service__["a"]; });

//# sourceMappingURL=index.js.map

/***/ }),

/***/ 584:
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__(0);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return BaThemeSpinner; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};

var BaThemeSpinner = (function () {
    function BaThemeSpinner() {
        this._selector = 'preloader';
        this._element = document.getElementById(this._selector);
    }
    BaThemeSpinner.prototype.show = function () {
        this._element.style['display'] = 'block';
    };
    BaThemeSpinner.prototype.hide = function (delay) {
        var _this = this;
        if (delay === void 0) { delay = 0; }
        setTimeout(function () {
            _this._element.style['display'] = 'none';
        }, delay);
    };
    return BaThemeSpinner;
}());
BaThemeSpinner = __decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["Injectable"])(),
    __metadata("design:paramtypes", [])
], BaThemeSpinner);

//# sourceMappingURL=baThemeSpinner.service.js.map

/***/ }),

/***/ 585:
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__baThemeSpinner_service__ = __webpack_require__(584);
/* harmony namespace reexport (by used) */ __webpack_require__.d(__webpack_exports__, "a", function() { return __WEBPACK_IMPORTED_MODULE_0__baThemeSpinner_service__["a"]; });

//# sourceMappingURL=index.js.map

/***/ }),

/***/ 586:
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return EmailValidator; });
var EmailValidator = (function () {
    function EmailValidator() {
    }
    EmailValidator.validate = function (c) {
        var EMAIL_REGEXP = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
        return EMAIL_REGEXP.test(c.value) ? null : {
            validateEmail: {
                valid: false
            }
        };
    };
    return EmailValidator;
}());

//# sourceMappingURL=email.validator.js.map

/***/ }),

/***/ 587:
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return EqualPasswordsValidator; });
var EqualPasswordsValidator = (function () {
    function EqualPasswordsValidator() {
    }
    EqualPasswordsValidator.validate = function (firstField, secondField) {
        return function (c) {
            return (c.controls && c.controls[firstField].value == c.controls[secondField].value) ? null : {
                passwordsEqual: {
                    valid: false
                }
            };
        };
    };
    return EqualPasswordsValidator;
}());

//# sourceMappingURL=equalPasswords.validator.js.map

/***/ }),

/***/ 588:
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return environment; });
// The file contents for the current environment will overwrite these during build.
// The build system defaults to the dev environment which uses `environment.ts`, but if you do
// `ng build --env=prod` then `environment.prod.ts` will be used instead.
// The list of which env maps to which file can be found in `.angular-cli.json`.
// The file contents for the current environment will overwrite these during build.
var environment = {
    production: false,
};
//# sourceMappingURL=environment.js.map

/***/ }),

/***/ 59:
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return UrlConstants; });
var UrlConstants = (function () {
    function UrlConstants() {
    }
    return UrlConstants;
}());

UrlConstants.LOGIN = "/login";
UrlConstants.HOME = "/main/home/index";
//# sourceMappingURL=url.constants.js.map

/***/ }),

/***/ 60:
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__(0);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1_rxjs_Subject__ = __webpack_require__(32);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1_rxjs_Subject___default = __webpack_require__.n(__WEBPACK_IMPORTED_MODULE_1_rxjs_Subject__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return GlobalState; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};


var GlobalState = (function () {
    function GlobalState() {
        var _this = this;
        this._data = new __WEBPACK_IMPORTED_MODULE_1_rxjs_Subject__["Subject"]();
        this._dataStream$ = this._data.asObservable();
        this._subscriptions = new Map();
        this._dataStream$.subscribe(function (data) { return _this._onEvent(data); });
    }
    GlobalState.prototype.notifyDataChanged = function (event, value) {
        var current = this._data[event];
        if (current !== value) {
            this._data[event] = value;
            this._data.next({
                event: event,
                data: this._data[event]
            });
        }
    };
    GlobalState.prototype.subscribe = function (event, callback) {
        var subscribers = this._subscriptions.get(event) || [];
        subscribers.push(callback);
        this._subscriptions.set(event, subscribers);
    };
    GlobalState.prototype._onEvent = function (data) {
        var subscribers = this._subscriptions.get(data['event']) || [];
        subscribers.forEach(function (callback) {
            callback.call(null, data['data']);
        });
    };
    return GlobalState;
}());
GlobalState = __decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["Injectable"])(),
    __metadata("design:paramtypes", [])
], GlobalState);

//# sourceMappingURL=global.state.js.map

/***/ }),

/***/ 71:
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__baImageLoader__ = __webpack_require__(579);
/* harmony namespace reexport (by used) */ __webpack_require__.d(__webpack_exports__, "b", function() { return __WEBPACK_IMPORTED_MODULE_0__baImageLoader__["a"]; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__baMenu__ = __webpack_require__(581);
/* harmony namespace reexport (by used) */ __webpack_require__.d(__webpack_exports__, "a", function() { return __WEBPACK_IMPORTED_MODULE_1__baMenu__["a"]; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__baThemePreloader__ = __webpack_require__(583);
/* harmony namespace reexport (by used) */ __webpack_require__.d(__webpack_exports__, "c", function() { return __WEBPACK_IMPORTED_MODULE_2__baThemePreloader__["a"]; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_3__baThemeSpinner__ = __webpack_require__(585);
/* harmony namespace reexport (by used) */ __webpack_require__.d(__webpack_exports__, "d", function() { return __WEBPACK_IMPORTED_MODULE_3__baThemeSpinner__["a"]; });




//# sourceMappingURL=index.js.map

/***/ }),

/***/ 764:
/***/ (function(module, exports, __webpack_require__) {

exports = module.exports = __webpack_require__(4)(false);
// imports


// module
exports.push([module.i, ":host /deep/ .al-sidebar {\n  width: 180px;\n  top: 66px;\n  left: 0;\n  z-index: 1001;\n  display: block;\n  min-height: 100%;\n  background-color: #282828;\n  height: 100%;\n  position: fixed; }\n\n:host /deep/ .al-sidebar-list {\n  margin: 0;\n  overflow: hidden;\n  padding: 18px 0 0 0;\n  list-style: none; }\n\n:host /deep/ .al-sidebar-sublist .subitem-submenu-list {\n  padding-left: 15px; }\n\n:host /deep/ .subitem-submenu-link .fa {\n  top: 7px; }\n\n:host /deep/ .al-sidebar-list-item {\n  display: block;\n  position: relative;\n  float: none;\n  padding: 0; }\n  :host /deep/ .al-sidebar-list-item.selected:not(.with-sub-menu) {\n    background-color: #00abff; }\n    :host /deep/ .al-sidebar-list-item.selected:not(.with-sub-menu) a.al-sidebar-list-link {\n      color: #ffffff; }\n      :host /deep/ .al-sidebar-list-item.selected:not(.with-sub-menu) a.al-sidebar-list-link b {\n        color: #ffffff; }\n\n:host /deep/ .ba-sidebar-item-expanded > ul.al-sidebar-sublist {\n  display: block !important; }\n\n:host /deep/ .al-sidebar-list-item.ba-sidebar-item-expanded > .al-sidebar-list-link b, :host /deep/ .ba-sidebar-sublist-item.ba-sidebar-item-expanded > .al-sidebar-list-link b {\n  transform: rotate(180deg); }\n\n:host /deep/ .al-sidebar-list-item.ba-sidebar-item-expanded > .al-sidebar-sublist, :host /deep/ .ba-sidebar-sublist-item.ba-sidebar-item-expanded > .al-sidebar-sublist {\n  display: block; }\n\n:host /deep/ a.al-sidebar-list-link {\n  display: block;\n  height: 42px;\n  padding-left: 18px;\n  text-shadow: none;\n  font-size: 13px;\n  text-decoration: none;\n  color: #ffffff;\n  line-height: 42px;\n  white-space: nowrap;\n  overflow: hidden;\n  cursor: pointer; }\n  :host /deep/ a.al-sidebar-list-link:hover {\n    color: #00abff; }\n    :host /deep/ a.al-sidebar-list-link:hover b {\n      color: #00abff; }\n  :host /deep/ a.al-sidebar-list-link i {\n    margin-right: 18px;\n    width: 16px;\n    display: inline-block; }\n  :host /deep/ a.al-sidebar-list-link b {\n    display: block;\n    opacity: 1;\n    width: 14px;\n    height: 14px;\n    line-height: 14px;\n    text-shadow: none;\n    font-size: 18px;\n    position: absolute;\n    right: 10px;\n    top: 12px;\n    padding: 0;\n    text-align: center;\n    color: #ffffff;\n    transition: transform 0.2s linear; }\n\n:host /deep/ .slimScrollBar, :host /deep/ .slimScrollRail {\n  border-radius: 0 !important;\n  width: 4px !important;\n  left: 176px; }\n\n:host /deep/ .al-sidebar-sublist {\n  padding: 0;\n  list-style: none;\n  position: relative;\n  display: none; }\n  :host /deep/ .al-sidebar-sublist.expanded {\n    display: block; }\n  :host /deep/ .al-sidebar-sublist > ba-menu-item > li {\n    display: block;\n    float: none;\n    padding: 0;\n    border-bottom: none;\n    position: relative; }\n    :host /deep/ .al-sidebar-sublist > ba-menu-item > li a {\n      display: block;\n      text-shadow: none;\n      font-size: 13px;\n      text-decoration: none;\n      color: #ffffff;\n      padding-left: 52px;\n      height: auto;\n      line-height: 29px; }\n      :host /deep/ .al-sidebar-sublist > ba-menu-item > li a:hover {\n        color: #00abff; }\n    :host /deep/ .al-sidebar-sublist > ba-menu-item > li.selected:not(.with-sub-menu) > a {\n      border: none;\n      background-color: #00abff; }\n      :host /deep/ .al-sidebar-sublist > ba-menu-item > li.selected:not(.with-sub-menu) > a:hover {\n        color: #ffffff; }\n\n:host /deep/ .sidebar-hover-elem {\n  width: 4px;\n  background: #00abff;\n  position: absolute;\n  top: -150px;\n  left: 176px;\n  transition: all 0.5s ease;\n  transition-property: top, height;\n  height: 42px;\n  display: block; }\n\n:host /deep/ .sidebar-select-elem {\n  display: block;\n  top: 94px; }\n\n:host /deep/ .menu-collapsed .slimScrollBar, :host /deep/ .menu-collapsed .slimScrollRail {\n  display: none !important; }\n\n@media (min-width: 1200px) {\n  :host /deep/ .menu-collapsed .al-main {\n    margin-left: 50px; }\n  :host /deep/ .menu-collapsed .al-footer {\n    padding-left: 83px; } }\n\n@media (max-width: 1200px) and (min-width: 500px) {\n  :host /deep/ .al-main {\n    margin-left: 50px; }\n  :host /deep/ .al-footer {\n    padding-left: 83px; } }\n\n@media (min-width: 501px) {\n  :host /deep/ .menu-collapsed .al-sidebar {\n    width: 52px; }\n    :host /deep/ .menu-collapsed .al-sidebar .fa-angle-down, :host /deep/ .menu-collapsed .al-sidebar .fa-angle-up {\n      opacity: 0; }\n    :host /deep/ .menu-collapsed .al-sidebar .al-sidebar-sublist {\n      position: absolute;\n      top: -1px;\n      left: 52px;\n      background: rgba(0, 0, 0, 0.8);\n      width: 0;\n      display: block;\n      overflow: hidden;\n      transition: width 0.5s ease; }\n      :host /deep/ .menu-collapsed .al-sidebar .al-sidebar-sublist.slide-right {\n        width: 135px; }\n      :host /deep/ .menu-collapsed .al-sidebar .al-sidebar-sublist::before {\n        display: none; }\n      :host /deep/ .menu-collapsed .al-sidebar .al-sidebar-sublist li::before {\n        display: none; }\n      :host /deep/ .menu-collapsed .al-sidebar .al-sidebar-sublist li a {\n        padding-left: 18px;\n        padding-right: 18px;\n        min-width: 130px;\n        white-space: nowrap; }\n    :host /deep/ .menu-collapsed .al-sidebar .sidebar-hover-elem, :host /deep/ .menu-collapsed .al-sidebar .sidebar-select-elem {\n      left: 48px; } }\n\n@media (max-width: 1200px) {\n  :host /deep/ .al-sidebar {\n    width: 180px;\n    background: rgba(0, 0, 0, 0.75);\n    transition: width 0.5s ease; }\n    :host /deep/ .al-sidebar .fa-angle-down, :host /deep/ .al-sidebar .fa-angle-up {\n      opacity: 1; }\n    :host /deep/ .al-sidebar .al-sidebar-sublist {\n      padding: 0;\n      list-style: none;\n      position: relative;\n      display: none;\n      top: auto;\n      left: auto;\n      background: none;\n      width: auto;\n      overflow: visible;\n      transition: none; }\n      :host /deep/ .al-sidebar .al-sidebar-sublist.expanded {\n        display: block; }\n      :host /deep/ .al-sidebar .al-sidebar-sublist > ba-menu-item > li {\n        display: block;\n        float: none;\n        padding: 0;\n        border-bottom: none;\n        position: relative; }\n        :host /deep/ .al-sidebar .al-sidebar-sublist > ba-menu-item > li a {\n          display: block;\n          text-shadow: none;\n          font-size: 13px;\n          text-decoration: none;\n          color: #ffffff;\n          padding-left: 52px;\n          height: auto;\n          line-height: 29px; }\n          :host /deep/ .al-sidebar .al-sidebar-sublist > ba-menu-item > li a:hover {\n            color: #00abff; }\n        :host /deep/ .al-sidebar .al-sidebar-sublist > ba-menu-item > li.selected:not(.with-sub-menu) > a {\n          border: none;\n          background-color: #00abff; }\n          :host /deep/ .al-sidebar .al-sidebar-sublist > ba-menu-item > li.selected:not(.with-sub-menu) > a:hover {\n            color: #ffffff; }\n    :host /deep/ .al-sidebar .sidebar-hover-elem, :host /deep/ .al-sidebar .sidebar-select-elem {\n      left: 176px;\n      transition: left 0.5s ease; } }\n\n@media (max-width: 500px) {\n  :host /deep/ .menu-collapsed .al-sidebar {\n    width: 0; }\n  :host /deep/ .menu-collapsed .sidebar-hover-elem, :host /deep/ .menu-collapsed .sidebar-select-elem {\n    display: none; }\n  :host /deep/ .al-main {\n    margin-left: 0; }\n  :host /deep/ .al-footer {\n    padding-left: 0; } }\n", ""]);

// exports


/*** EXPORTS FROM exports-loader ***/
module.exports = module.exports.toString();

/***/ }),

/***/ 765:
/***/ (function(module, exports, __webpack_require__) {

exports = module.exports = __webpack_require__(4)(false);
// imports


// module
exports.push([module.i, ".ammapAlert {\n  display: table-cell;\n  vertical-align: middle;\n  text-align: center;\n  font-family: verdana,helvetica,arial,sans-serif;\n  font-size: 12px;\n  color: #CC0000; }\n\n.ammapDescriptionWindow {\n  font-size: 11px;\n  font-family: verdana,helvetica,arial,sans-serif;\n  background-color: #FFFFFF;\n  border-style: solid;\n  border-color: #DADADA;\n  border-width: 1px;\n  color: #000000;\n  padding: 8px;\n  box-sizing: border-box; }\n\n.ammapDescriptionTitle {\n  font-size: 12px;\n  font-weight: bold;\n  font-family: verdana,helvetica,arial,sans-serif;\n  padding-bottom: 5px; }\n\n.ammapObjectList ul {\n  padding-left: 20px;\n  list-style: square outside;\n  color: #999999;\n  font-family: verdana,helvetica,arial,sans-serif;\n  font-size: 12px; }\n\n.ammapObjectList ul ul {\n  padding-left: 14px; }\n\n.ammapObjectList a {\n  color: #000000; }\n\n.ammapObjectList a {\n  color: #000000;\n  text-decoration: none;\n  display: block;\n  padding: 2px; }\n\n.ammapObjectList a:hover {\n  color: #CC0000;\n  text-decoration: none;\n  background: #FFFFFF;\n  cursor: pointer;\n  display: block; }\n\n.ammapDescriptionText {\n  overflow: auto; }\n", ""]);

// exports


/*** EXPORTS FROM exports-loader ***/
module.exports = module.exports.toString();

/***/ }),

/***/ 766:
/***/ (function(module, exports, __webpack_require__) {

exports = module.exports = __webpack_require__(4)(false);
// imports


// module
exports.push([module.i, ".ba-back-top {\n  position: fixed;\n  width: 52px;\n  height: 52px;\n  cursor: pointer;\n  z-index: 9999;\n  display: none;\n  text-decoration: none;\n  right: 40px;\n  bottom: 40px !important;\n  font-size: 45px;\n  text-align: center;\n  opacity: 0.4;\n  color: #00abff;\n  background-color: rgba(0, 0, 0, 0.75);\n  border-radius: 50%;\n  line-height: 46px; }\n  .ba-back-top:hover {\n    opacity: 0.8; }\n", ""]);

// exports


/*** EXPORTS FROM exports-loader ***/
module.exports = module.exports.toString();

/***/ }),

/***/ 767:
/***/ (function(module, exports, __webpack_require__) {

exports = module.exports = __webpack_require__(4)(false);
// imports


// module
exports.push([module.i, ".has-success {\n  position: relative; }\n  .has-success .control-label {\n    color: #ffffff; }\n  .has-success .form-control {\n    border: 1px solid #a2db59; }\n    .has-success .form-control:focus {\n      box-shadow: none;\n      border-color: #8bd22f; }\n  .has-success label.custom-checkbox {\n    color: #a2db59; }\n    .has-success label.custom-checkbox > span:before {\n      color: #a2db59; }\n    .has-success label.custom-checkbox > span:hover:before {\n      border-color: #a2db59; }\n  .has-success .form-control-feedback {\n    color: #a2db59; }\n  .has-success .input-group-addon {\n    background-color: #a2db59;\n    color: #ffffff; }\n\n.has-warning {\n  position: relative; }\n  .has-warning .control-label {\n    color: #ffffff; }\n  .has-warning .form-control {\n    border: 1px solid #ecc839; }\n    .has-warning .form-control:focus {\n      box-shadow: none;\n      border-color: #e7ba08; }\n  .has-warning label.custom-checkbox {\n    color: #ecc839; }\n    .has-warning label.custom-checkbox > span:before {\n      color: #ecc839; }\n    .has-warning label.custom-checkbox > span:hover:before {\n      border-color: #ecc839; }\n  .has-warning .form-control-feedback {\n    color: #ecc839; }\n  .has-warning .input-group-addon {\n    background-color: #ecc839;\n    color: #ffffff; }\n\n.has-error {\n  position: relative; }\n  .has-error .control-label {\n    color: #ffffff; }\n  .has-error .form-control {\n    border: 1px solid #fa758e; }\n    .has-error .form-control:focus {\n      box-shadow: none;\n      border-color: #f95372; }\n  .has-error label.custom-checkbox {\n    color: #fa758e; }\n    .has-error label.custom-checkbox > span:before {\n      color: #fa758e; }\n    .has-error label.custom-checkbox > span:hover:before {\n      border-color: #fa758e; }\n  .has-error .form-control-feedback {\n    color: #fa758e; }\n  .has-error .input-group-addon {\n    background-color: #fa758e;\n    color: #ffffff; }\n\nlabel.custom-checkbox {\n  margin-bottom: 12px; }\n  label.custom-checkbox > span {\n    display: block;\n    margin-right: 10px; }\n", ""]);

// exports


/*** EXPORTS FROM exports-loader ***/
module.exports = module.exports.toString();

/***/ }),

/***/ 768:
/***/ (function(module, exports, __webpack_require__) {

exports = module.exports = __webpack_require__(4)(false);
// imports


// module
exports.push([module.i, ".content-top {\n  padding-top: 13px;\n  padding-bottom: 27px; }\n\nh1.al-title {\n  font-weight: 700;\n  color: #ffffff;\n  float: left;\n  width: auto;\n  margin: 0;\n  padding: 0;\n  font-size: 24px;\n  text-transform: uppercase;\n  opacity: 0.9; }\n\n.al-breadcrumb {\n  background: none;\n  color: #ffffff;\n  padding: 0;\n  margin: 0;\n  float: right;\n  padding-top: 11px; }\n  .al-breadcrumb li {\n    font-size: 18px;\n    font-weight: 400; }\n    .al-breadcrumb li a {\n      color: #4dc4ff; }\n    .al-breadcrumb li.breadcrumb-item.active {\n      color: #ffffff; }\n\n.al-look {\n  float: right;\n  margin-right: 10px;\n  padding-top: 10px; }\n  .al-look > a {\n    font-size: 19px; }\n", ""]);

// exports


/*** EXPORTS FROM exports-loader ***/
module.exports = module.exports.toString();

/***/ }),

/***/ 769:
/***/ (function(module, exports, __webpack_require__) {

exports = module.exports = __webpack_require__(4)(false);
// imports


// module
exports.push([module.i, "", ""]);

// exports


/*** EXPORTS FROM exports-loader ***/
module.exports = module.exports.toString();

/***/ }),

/***/ 770:
/***/ (function(module, exports, __webpack_require__) {

exports = module.exports = __webpack_require__(4)(false);
// imports


// module
exports.push([module.i, "", ""]);

// exports


/*** EXPORTS FROM exports-loader ***/
module.exports = module.exports.toString();

/***/ }),

/***/ 771:
/***/ (function(module, exports, __webpack_require__) {

exports = module.exports = __webpack_require__(4)(false);
// imports


// module
exports.push([module.i, "", ""]);

// exports


/*** EXPORTS FROM exports-loader ***/
module.exports = module.exports.toString();

/***/ }),

/***/ 772:
/***/ (function(module, exports, __webpack_require__) {

exports = module.exports = __webpack_require__(4)(false);
// imports


// module
exports.push([module.i, "/* msg center */\n\n.al-msg-center {\n  float: right;\n  padding: 0;\n  list-style: none;\n  margin: 13px 47px 0 0; }\n  .al-msg-center li {\n    list-style: none;\n    float: left;\n    margin-left: 30px; }\n    .al-msg-center li:first-child {\n      margin-left: 0; }\n    .al-msg-center li > a {\n      color: #ffffff;\n      text-decoration: none;\n      font-size: 13px;\n      position: relative; }\n      .al-msg-center li > a span {\n        display: inline-block;\n        min-width: 10px;\n        padding: 2px 4px 2px 4px;\n        color: #ffffff;\n        vertical-align: baseline;\n        white-space: nowrap;\n        text-align: center;\n        border-radius: 13px;\n        text-shadow: none;\n        line-height: 11px;\n        background-color: #f95372;\n        position: absolute;\n        top: -5px;\n        right: -14px;\n        font-size: 11px; }\n      .al-msg-center li > a .notification-ring {\n        border: 1px solid #f95372;\n        border-radius: 100px;\n        height: 40px;\n        width: 40px;\n        position: absolute;\n        top: -18px;\n        right: -27px;\n        animation: pulsate 8s ease-out;\n        animation-iteration-count: infinite;\n        opacity: 0.0; }\n      .al-msg-center li > a:hover {\n        color: #f95372; }\n        .al-msg-center li > a:hover.msg {\n          color: #00abff; }\n      .al-msg-center li > a.msg span {\n        background-color: #00abff; }\n      .al-msg-center li > a.msg .notification-ring {\n        border-color: #00abff; }\n    .al-msg-center li.open > a {\n      color: #f95372; }\n      .al-msg-center li.open > a.msg {\n        color: #00abff; }\n\n@media (max-width: 435px) {\n  .al-msg-center {\n    margin-right: 20px; }\n    .al-msg-center li {\n      margin-left: 20px; }\n      .al-msg-center li:first-child {\n        margin-left: 0; } }\n\n.msg-block-header {\n  display: inline-block;\n  padding: 0;\n  font-size: 13px;\n  margin: 0 0 0 6px; }\n\n.top-dropdown-menu {\n  width: 316px;\n  left: auto;\n  right: -47px;\n  top: 26px; }\n  .top-dropdown-menu ::-webkit-scrollbar {\n    width: 0.4em;\n    height: 0.4em; }\n  .top-dropdown-menu ::-webkit-scrollbar-thumb {\n    background: rgba(0, 0, 0, 0.5);\n    cursor: pointer; }\n  .top-dropdown-menu ::-webkit-scrollbar-track {\n    background: #fff; }\n  .top-dropdown-menu body {\n    scrollbar-face-color: rgba(0, 0, 0, 0.5);\n    scrollbar-track-color: #fff; }\n  .top-dropdown-menu .header {\n    padding: 10px 12px;\n    border-bottom: 1px solid #ffffff;\n    font-size: 12px; }\n    .top-dropdown-menu .header strong {\n      float: left;\n      color: #7d7d7d; }\n    .top-dropdown-menu .header > a {\n      float: right;\n      margin-left: 12px;\n      text-decoration: none; }\n      .top-dropdown-menu .header > a:hover {\n        color: #7d7d7d; }\n  .top-dropdown-menu .msg-list {\n    max-height: 296px;\n    overflow: scroll;\n    overflow-x: hidden; }\n    .top-dropdown-menu .msg-list > a {\n      border-top: 1px solid #ffffff;\n      padding: 10px 12px;\n      display: block;\n      text-decoration: none;\n      color: #7d7d7d;\n      font-size: 12px; }\n      .top-dropdown-menu .msg-list > a:first-child {\n        border-top: none; }\n      .top-dropdown-menu .msg-list > a .img-area {\n        float: left;\n        width: 36px; }\n        .top-dropdown-menu .msg-list > a .img-area img {\n          width: 36px;\n          height: 36px; }\n          .top-dropdown-menu .msg-list > a .img-area img.photo-msg-item {\n            border-radius: 18px; }\n        .top-dropdown-menu .msg-list > a .img-area > div {\n          width: 36px;\n          height: 36px;\n          border-radius: 4px;\n          font-size: 24px;\n          text-align: center; }\n          .top-dropdown-menu .msg-list > a .img-area > div.comments {\n            color: #e7ba08; }\n          .top-dropdown-menu .msg-list > a .img-area > div.orders {\n            color: #e7ba08; }\n          .top-dropdown-menu .msg-list > a .img-area > div i {\n            width: 36px;\n            line-height: 36px; }\n      .top-dropdown-menu .msg-list > a .msg-area {\n        float: right;\n        width: 230px; }\n        .top-dropdown-menu .msg-list > a .msg-area div {\n          max-height: 34px;\n          overflow: hidden;\n          text-overflow: ellipsis; }\n        .top-dropdown-menu .msg-list > a .msg-area span {\n          font-style: italic;\n          text-align: right;\n          display: block;\n          font-size: 11px; }\n      .top-dropdown-menu .msg-list > a:hover {\n        background: #E2F0FF; }\n  .top-dropdown-menu > a {\n    border-top: 1px solid #ffffff;\n    display: block;\n    text-align: center;\n    padding: 10px;\n    font-size: 12px;\n    text-decoration: none; }\n    .top-dropdown-menu > a:hover {\n      color: #7d7d7d; }\n  .top-dropdown-menu.profile-dropdown {\n    width: 145px;\n    top: 55px;\n    right: -25px; }\n    .top-dropdown-menu.profile-dropdown a {\n      text-align: left;\n      border: none;\n      text-decoration: none;\n      color: #7d7d7d;\n      padding: 4px 16px 4px 20px; }\n      .top-dropdown-menu.profile-dropdown a.signout {\n        border-top: 1px solid #ffffff; }\n      .top-dropdown-menu.profile-dropdown a i {\n        margin-right: 10px; }\n      .top-dropdown-menu.profile-dropdown a:hover {\n        background: #f4fcff; }\n    .top-dropdown-menu.profile-dropdown i.dropdown-arr {\n      right: 25px; }\n  .top-dropdown-menu i.dropdown-arr {\n    position: absolute;\n    top: -22px;\n    right: 42px;\n    display: block;\n    width: 0;\n    height: 0;\n    border: 11px solid transparent;\n    border-bottom-color: rgba(0, 0, 0, 0.15); }\n    .top-dropdown-menu i.dropdown-arr:after {\n      top: -9px;\n      left: 0px;\n      margin-left: -10px;\n      content: \" \";\n      position: absolute;\n      display: block;\n      width: 0;\n      height: 0;\n      border: 10px solid transparent;\n      border-bottom-color: #ffffff; }\n\n@media (max-width: 415px) {\n  .top-dropdown-menu {\n    right: -81px; }\n    .top-dropdown-menu i.dropdown-arr {\n      right: 75px; } }\n", ""]);

// exports


/*** EXPORTS FROM exports-loader ***/
module.exports = module.exports.toString();

/***/ }),

/***/ 773:
/***/ (function(module, exports, __webpack_require__) {

exports = module.exports = __webpack_require__(4)(false);
// imports


// module
exports.push([module.i, ".container-content {\n  width: 100%;\n  display: -ms-flexbox;\n  display: flex; }\n  .container-content ba-checkbox {\n    width: 100%; }\n", ""]);

// exports


/*** EXPORTS FROM exports-loader ***/
module.exports = module.exports.toString();

/***/ }),

/***/ 774:
/***/ (function(module, exports, __webpack_require__) {

exports = module.exports = __webpack_require__(4)(false);
// imports


// module
exports.push([module.i, "/* msg center */\n\n.al-msg-center {\n  float: right;\n  padding: 0;\n  list-style: none;\n  margin: 13px 47px 0 0; }\n  .al-msg-center li {\n    list-style: none;\n    float: left;\n    margin-left: 30px; }\n    .al-msg-center li:first-child {\n      margin-left: 0; }\n    .al-msg-center li > a {\n      color: #ffffff;\n      text-decoration: none;\n      font-size: 13px;\n      position: relative; }\n      .al-msg-center li > a span {\n        display: inline-block;\n        min-width: 10px;\n        padding: 2px 4px 2px 4px;\n        color: #ffffff;\n        vertical-align: baseline;\n        white-space: nowrap;\n        text-align: center;\n        border-radius: 13px;\n        text-shadow: none;\n        line-height: 11px;\n        background-color: #f95372;\n        position: absolute;\n        top: -5px;\n        right: -14px;\n        font-size: 11px; }\n      .al-msg-center li > a .notification-ring {\n        border: 1px solid #f95372;\n        border-radius: 100px;\n        height: 40px;\n        width: 40px;\n        position: absolute;\n        top: -18px;\n        right: -27px;\n        animation: pulsate 8s ease-out;\n        animation-iteration-count: infinite;\n        opacity: 0.0; }\n      .al-msg-center li > a:hover {\n        color: #f95372; }\n        .al-msg-center li > a:hover.msg {\n          color: #00abff; }\n      .al-msg-center li > a.msg span {\n        background-color: #00abff; }\n      .al-msg-center li > a.msg .notification-ring {\n        border-color: #00abff; }\n    .al-msg-center li.open > a {\n      color: #f95372; }\n      .al-msg-center li.open > a.msg {\n        color: #00abff; }\n\n@media (max-width: 435px) {\n  .al-msg-center {\n    margin-right: 20px; }\n    .al-msg-center li {\n      margin-left: 20px; }\n      .al-msg-center li:first-child {\n        margin-left: 0; } }\n\n.msg-block-header {\n  display: inline-block;\n  padding: 0;\n  font-size: 13px;\n  margin: 0 0 0 6px; }\n\n.top-dropdown-menu {\n  width: 316px;\n  left: auto;\n  right: -47px;\n  top: 26px; }\n  .top-dropdown-menu ::-webkit-scrollbar {\n    width: 0.4em;\n    height: 0.4em; }\n  .top-dropdown-menu ::-webkit-scrollbar-thumb {\n    background: rgba(0, 0, 0, 0.5);\n    cursor: pointer; }\n  .top-dropdown-menu ::-webkit-scrollbar-track {\n    background: #fff; }\n  .top-dropdown-menu body {\n    scrollbar-face-color: rgba(0, 0, 0, 0.5);\n    scrollbar-track-color: #fff; }\n  .top-dropdown-menu .header {\n    padding: 10px 12px;\n    border-bottom: 1px solid #ffffff;\n    font-size: 12px; }\n    .top-dropdown-menu .header strong {\n      float: left;\n      color: #7d7d7d; }\n    .top-dropdown-menu .header > a {\n      float: right;\n      margin-left: 12px;\n      text-decoration: none; }\n      .top-dropdown-menu .header > a:hover {\n        color: #7d7d7d; }\n  .top-dropdown-menu .msg-list {\n    max-height: 296px;\n    overflow: scroll;\n    overflow-x: hidden; }\n    .top-dropdown-menu .msg-list > a {\n      border-top: 1px solid #ffffff;\n      padding: 10px 12px;\n      display: block;\n      text-decoration: none;\n      color: #7d7d7d;\n      font-size: 12px; }\n      .top-dropdown-menu .msg-list > a:first-child {\n        border-top: none; }\n      .top-dropdown-menu .msg-list > a .img-area {\n        float: left;\n        width: 36px; }\n        .top-dropdown-menu .msg-list > a .img-area img {\n          width: 36px;\n          height: 36px; }\n          .top-dropdown-menu .msg-list > a .img-area img.photo-msg-item {\n            border-radius: 18px; }\n        .top-dropdown-menu .msg-list > a .img-area > div {\n          width: 36px;\n          height: 36px;\n          border-radius: 4px;\n          font-size: 24px;\n          text-align: center; }\n          .top-dropdown-menu .msg-list > a .img-area > div.comments {\n            color: #e7ba08; }\n          .top-dropdown-menu .msg-list > a .img-area > div.orders {\n            color: #e7ba08; }\n          .top-dropdown-menu .msg-list > a .img-area > div i {\n            width: 36px;\n            line-height: 36px; }\n      .top-dropdown-menu .msg-list > a .msg-area {\n        float: right;\n        width: 230px; }\n        .top-dropdown-menu .msg-list > a .msg-area div {\n          max-height: 34px;\n          overflow: hidden;\n          text-overflow: ellipsis; }\n        .top-dropdown-menu .msg-list > a .msg-area span {\n          font-style: italic;\n          text-align: right;\n          display: block;\n          font-size: 11px; }\n      .top-dropdown-menu .msg-list > a:hover {\n        background: #E2F0FF; }\n  .top-dropdown-menu > a {\n    border-top: 1px solid #ffffff;\n    display: block;\n    text-align: center;\n    padding: 10px;\n    font-size: 12px;\n    text-decoration: none; }\n    .top-dropdown-menu > a:hover {\n      color: #7d7d7d; }\n  .top-dropdown-menu.profile-dropdown {\n    width: 145px;\n    top: 55px;\n    right: -25px; }\n    .top-dropdown-menu.profile-dropdown a {\n      text-align: left;\n      border: none;\n      text-decoration: none;\n      color: #7d7d7d;\n      padding: 4px 16px 4px 20px; }\n      .top-dropdown-menu.profile-dropdown a.signout {\n        border-top: 1px solid #ffffff; }\n      .top-dropdown-menu.profile-dropdown a i {\n        margin-right: 10px; }\n      .top-dropdown-menu.profile-dropdown a:hover {\n        background: #f4fcff; }\n    .top-dropdown-menu.profile-dropdown i.dropdown-arr {\n      right: 25px; }\n  .top-dropdown-menu i.dropdown-arr {\n    position: absolute;\n    top: -22px;\n    right: 42px;\n    display: block;\n    width: 0;\n    height: 0;\n    border: 11px solid transparent;\n    border-bottom-color: rgba(0, 0, 0, 0.15); }\n    .top-dropdown-menu i.dropdown-arr:after {\n      top: -9px;\n      left: 0px;\n      margin-left: -10px;\n      content: \" \";\n      position: absolute;\n      display: block;\n      width: 0;\n      height: 0;\n      border: 10px solid transparent;\n      border-bottom-color: #ffffff; }\n\n@media (max-width: 415px) {\n  .top-dropdown-menu {\n    right: -81px; }\n    .top-dropdown-menu i.dropdown-arr {\n      right: 75px; } }\n\n:host /deep/ .page-top {\n  background-color: #282828;\n  position: fixed;\n  z-index: 904;\n  box-shadow: 2px 0 3px rgba(0, 0, 0, 0.5);\n  height: 66px;\n  width: 100%;\n  min-width: 320px;\n  padding: 0 32px 0 40px; }\n  :host /deep/ .page-top .dropdown-toggle::after {\n    display: none; }\n\n:host /deep/ .blur .page-top.scrolled {\n  background-color: rgba(0, 0, 0, 0.85); }\n\n:host /deep/ a.al-logo {\n  color: #ffffff;\n  display: block;\n  font-size: 24px;\n  font-family: \"Roboto\", sans-serif;\n  white-space: nowrap;\n  float: left;\n  outline: none !important;\n  line-height: 60px; }\n  :host /deep/ a.al-logo span {\n    color: #00abff; }\n\n:host /deep/ a.al-logo:hover {\n  color: #00abff; }\n\n:host /deep/ .user-profile {\n  float: right;\n  min-width: 230px;\n  margin-top: 10px; }\n\n:host /deep/ .al-user-profile {\n  float: right;\n  margin-right: 12px;\n  transition: all .15s ease-in-out;\n  padding: 0;\n  width: 36px;\n  height: 36px;\n  border: 0;\n  opacity: 1;\n  position: relative; }\n  :host /deep/ .al-user-profile ul.profile-dropdown:after {\n    bottom: 100%;\n    right: 0;\n    border: solid transparent;\n    content: \" \";\n    height: 0;\n    width: 0;\n    position: absolute;\n    pointer-events: none;\n    border-color: rgba(255, 255, 255, 0);\n    border-bottom-color: #fff;\n    border-width: 10px;\n    margin-right: 28px; }\n  :host /deep/ .al-user-profile a {\n    display: block; }\n  :host /deep/ .al-user-profile img {\n    width: 45px;\n    height: 45px;\n    border-radius: 50%; }\n\n:host /deep/ a.refresh-data {\n  color: #ffffff;\n  font-size: 13px;\n  text-decoration: none;\n  font-weight: 400;\n  float: right;\n  margin-top: 13px;\n  margin-right: 26px; }\n  :host /deep/ a.refresh-data:hover {\n    color: #e7ba08 !important; }\n\n:host /deep/ a.collapse-menu-link {\n  font-size: 31px;\n  cursor: pointer;\n  display: block;\n  text-decoration: none;\n  line-height: 42px;\n  color: #ffffff;\n  padding: 0;\n  float: left;\n  margin: 11px 0 0 25px; }\n  :host /deep/ a.collapse-menu-link:hover {\n    text-decoration: none;\n    color: #e7ba08; }\n\n:host /deep/ .al-skin-dropdown {\n  float: right;\n  margin-top: 14px;\n  margin-right: 26px; }\n  :host /deep/ .al-skin-dropdown .tpl-skin-panel {\n    max-height: 300px;\n    overflow-y: scroll;\n    overflow-x: hidden; }\n\n:host /deep/ .icon-palette {\n  display: inline-block;\n  width: 14px;\n  height: 13px;\n  background: url(\"/img/theme/palette.png\");\n  background-size: cover; }\n\n:host /deep/ .search {\n  text-shadow: none;\n  font-size: 13px;\n  line-height: 25px;\n  transition: all 0.5s ease;\n  white-space: nowrap;\n  overflow: hidden;\n  width: 162px;\n  float: left;\n  margin: 20px 0 0 30px; }\n  :host /deep/ .search label {\n    cursor: pointer; }\n  :host /deep/ .search i {\n    width: 16px;\n    display: inline-block;\n    cursor: pointer;\n    padding-left: 1px;\n    font-size: 16px;\n    margin-right: 13px; }\n  :host /deep/ .search input {\n    color: #ffffff;\n    background: none;\n    border: none;\n    outline: none;\n    width: 120px;\n    padding: 0;\n    margin: 0 0 0 -3px;\n    height: 27px; }\n\n@media screen and (max-width: 660px) {\n  :host /deep/ .search {\n    display: none; } }\n\n@media screen and (max-width: 500px) {\n  :host /deep/ .page-top {\n    padding: 0 20px; } }\n\n@media (max-width: 435px) {\n  :host /deep/ .user-profile {\n    min-width: 136px; }\n  :host /deep/ a.refresh-data {\n    margin-right: 10px; }\n  :host /deep/ a.collapse-menu-link {\n    margin-left: 10px; }\n  :host /deep/ .al-skin-dropdown {\n    display: none; } }\n\n:host /deep/ .profile-toggle-link {\n  cursor: pointer; }\n", ""]);

// exports


/*** EXPORTS FROM exports-loader ***/
module.exports = module.exports.toString();

/***/ }),

/***/ 775:
/***/ (function(module, exports, __webpack_require__) {

exports = module.exports = __webpack_require__(4)(false);
// imports


// module
exports.push([module.i, ".picture-group {\n  border: 1px dashed #b8b8b8;\n  width: 202px;\n  height: 202px;\n  position: relative;\n  cursor: pointer; }\n  .picture-group .picture-wrapper {\n    width: 200px;\n    height: 200px;\n    overflow: hidden;\n    display: -ms-flexbox;\n    display: flex;\n    -ms-flex-pack: center;\n        justify-content: center;\n    -ms-flex-align: center;\n        align-items: center; }\n  .picture-group img {\n    max-width: 100%;\n    max-height: 100%; }\n  .picture-group i {\n    display: none;\n    position: absolute;\n    font-size: 32px;\n    background: #ffffff;\n    cursor: pointer;\n    color: #00abff;\n    top: -11px;\n    right: -11px;\n    height: 26px;\n    border-radius: 50%; }\n    .picture-group i:before {\n      line-height: 26px; }\n    .picture-group i:hover {\n      color: #f95372; }\n  .picture-group a.change-picture {\n    display: none;\n    width: 202px;\n    background: rgba(0, 0, 0, 0.7);\n    transition: all 200ms ease-in-out;\n    color: #ffffff;\n    text-decoration: none;\n    position: absolute;\n    bottom: -1px;\n    left: -1px;\n    line-height: 32px;\n    text-align: center; }\n  .picture-group:hover i {\n    display: block; }\n  .picture-group:hover .change-picture {\n    display: block; }\n  .picture-group .loading {\n    width: 100%;\n    height: 100%;\n    left: 0;\n    display: -ms-flexbox;\n    display: flex;\n    position: absolute;\n    -ms-flex-pack: center;\n        justify-content: center;\n    -ms-flex-align: center;\n        align-items: center; }\n  .picture-group .spinner {\n    width: 60px;\n    height: 60px;\n    position: relative; }\n  .picture-group .double-bounce1, .picture-group .double-bounce2 {\n    width: 100%;\n    height: 100%;\n    border-radius: 50%;\n    background-color: #fff;\n    opacity: 0.6;\n    position: absolute;\n    top: 0;\n    left: 0;\n    animation: sk-bounce 2.0s infinite ease-in-out; }\n  .picture-group .double-bounce2 {\n    animation-delay: -1.0s; }\n\n@keyframes sk-bounce {\n  0%, 100% {\n    transform: scale(0);\n    -webkit-transform: scale(0); }\n  50% {\n    transform: scale(1);\n    -webkit-transform: scale(1); } }\n", ""]);

// exports


/*** EXPORTS FROM exports-loader ***/
module.exports = module.exports.toString();

/***/ }),

/***/ 776:
/***/ (function(module, exports, __webpack_require__) {

exports = module.exports = __webpack_require__(4)(false);
// imports


// module
exports.push([module.i, ":host /deep/ .al-sidebar {\n  width: 180px;\n  top: 66px;\n  left: 0;\n  z-index: 1001;\n  display: block;\n  min-height: 100%;\n  background-color: #282828;\n  height: 100%;\n  position: fixed; }\n\n:host /deep/ .al-sidebar-list {\n  margin: 0;\n  overflow: hidden;\n  padding: 18px 0 0 0;\n  list-style: none; }\n\n:host /deep/ .al-sidebar-sublist .subitem-submenu-list {\n  padding-left: 15px; }\n\n:host /deep/ .subitem-submenu-link .fa {\n  top: 7px; }\n\n:host /deep/ .al-sidebar-list-item {\n  display: block;\n  position: relative;\n  float: none;\n  padding: 0; }\n  :host /deep/ .al-sidebar-list-item.selected:not(.with-sub-menu) {\n    background-color: #00abff; }\n    :host /deep/ .al-sidebar-list-item.selected:not(.with-sub-menu) a.al-sidebar-list-link {\n      color: #ffffff; }\n      :host /deep/ .al-sidebar-list-item.selected:not(.with-sub-menu) a.al-sidebar-list-link b {\n        color: #ffffff; }\n\n:host /deep/ .ba-sidebar-item-expanded > ul.al-sidebar-sublist {\n  display: block !important; }\n\n:host /deep/ .al-sidebar-list-item.ba-sidebar-item-expanded > .al-sidebar-list-link b, :host /deep/ .ba-sidebar-sublist-item.ba-sidebar-item-expanded > .al-sidebar-list-link b {\n  transform: rotate(180deg); }\n\n:host /deep/ .al-sidebar-list-item.ba-sidebar-item-expanded > .al-sidebar-sublist, :host /deep/ .ba-sidebar-sublist-item.ba-sidebar-item-expanded > .al-sidebar-sublist {\n  display: block; }\n\n:host /deep/ a.al-sidebar-list-link {\n  display: block;\n  height: 42px;\n  padding-left: 18px;\n  text-shadow: none;\n  font-size: 13px;\n  text-decoration: none;\n  color: #ffffff;\n  line-height: 42px;\n  white-space: nowrap;\n  overflow: hidden;\n  cursor: pointer; }\n  :host /deep/ a.al-sidebar-list-link:hover {\n    color: #00abff; }\n    :host /deep/ a.al-sidebar-list-link:hover b {\n      color: #00abff; }\n  :host /deep/ a.al-sidebar-list-link i {\n    margin-right: 18px;\n    width: 16px;\n    display: inline-block; }\n  :host /deep/ a.al-sidebar-list-link b {\n    display: block;\n    opacity: 1;\n    width: 14px;\n    height: 14px;\n    line-height: 14px;\n    text-shadow: none;\n    font-size: 18px;\n    position: absolute;\n    right: 10px;\n    top: 12px;\n    padding: 0;\n    text-align: center;\n    color: #ffffff;\n    transition: transform 0.2s linear; }\n\n:host /deep/ .slimScrollBar, :host /deep/ .slimScrollRail {\n  border-radius: 0 !important;\n  width: 4px !important;\n  left: 176px; }\n\n:host /deep/ .al-sidebar-sublist {\n  padding: 0;\n  list-style: none;\n  position: relative;\n  display: none; }\n  :host /deep/ .al-sidebar-sublist.expanded {\n    display: block; }\n  :host /deep/ .al-sidebar-sublist > ba-menu-item > li {\n    display: block;\n    float: none;\n    padding: 0;\n    border-bottom: none;\n    position: relative; }\n    :host /deep/ .al-sidebar-sublist > ba-menu-item > li a {\n      display: block;\n      text-shadow: none;\n      font-size: 13px;\n      text-decoration: none;\n      color: #ffffff;\n      padding-left: 52px;\n      height: auto;\n      line-height: 29px; }\n      :host /deep/ .al-sidebar-sublist > ba-menu-item > li a:hover {\n        color: #00abff; }\n    :host /deep/ .al-sidebar-sublist > ba-menu-item > li.selected:not(.with-sub-menu) > a {\n      border: none;\n      background-color: #00abff; }\n      :host /deep/ .al-sidebar-sublist > ba-menu-item > li.selected:not(.with-sub-menu) > a:hover {\n        color: #ffffff; }\n\n:host /deep/ .sidebar-hover-elem {\n  width: 4px;\n  background: #00abff;\n  position: absolute;\n  top: -150px;\n  left: 176px;\n  transition: all 0.5s ease;\n  transition-property: top, height;\n  height: 42px;\n  display: block; }\n\n:host /deep/ .sidebar-select-elem {\n  display: block;\n  top: 94px; }\n\n:host /deep/ .menu-collapsed .slimScrollBar, :host /deep/ .menu-collapsed .slimScrollRail {\n  display: none !important; }\n", ""]);

// exports


/*** EXPORTS FROM exports-loader ***/
module.exports = module.exports.toString();

/***/ }),

/***/ 782:
/***/ (function(module, exports, __webpack_require__) {

var map = {
	"./af": 308,
	"./af.js": 308,
	"./ar": 315,
	"./ar-dz": 309,
	"./ar-dz.js": 309,
	"./ar-kw": 310,
	"./ar-kw.js": 310,
	"./ar-ly": 311,
	"./ar-ly.js": 311,
	"./ar-ma": 312,
	"./ar-ma.js": 312,
	"./ar-sa": 313,
	"./ar-sa.js": 313,
	"./ar-tn": 314,
	"./ar-tn.js": 314,
	"./ar.js": 315,
	"./az": 316,
	"./az.js": 316,
	"./be": 317,
	"./be.js": 317,
	"./bg": 318,
	"./bg.js": 318,
	"./bm": 319,
	"./bm.js": 319,
	"./bn": 320,
	"./bn.js": 320,
	"./bo": 321,
	"./bo.js": 321,
	"./br": 322,
	"./br.js": 322,
	"./bs": 323,
	"./bs.js": 323,
	"./ca": 324,
	"./ca.js": 324,
	"./cs": 325,
	"./cs.js": 325,
	"./cv": 326,
	"./cv.js": 326,
	"./cy": 327,
	"./cy.js": 327,
	"./da": 328,
	"./da.js": 328,
	"./de": 331,
	"./de-at": 329,
	"./de-at.js": 329,
	"./de-ch": 330,
	"./de-ch.js": 330,
	"./de.js": 331,
	"./dv": 332,
	"./dv.js": 332,
	"./el": 333,
	"./el.js": 333,
	"./en-au": 334,
	"./en-au.js": 334,
	"./en-ca": 335,
	"./en-ca.js": 335,
	"./en-gb": 336,
	"./en-gb.js": 336,
	"./en-ie": 337,
	"./en-ie.js": 337,
	"./en-il": 338,
	"./en-il.js": 338,
	"./en-nz": 339,
	"./en-nz.js": 339,
	"./eo": 340,
	"./eo.js": 340,
	"./es": 343,
	"./es-do": 341,
	"./es-do.js": 341,
	"./es-us": 342,
	"./es-us.js": 342,
	"./es.js": 343,
	"./et": 344,
	"./et.js": 344,
	"./eu": 345,
	"./eu.js": 345,
	"./fa": 346,
	"./fa.js": 346,
	"./fi": 347,
	"./fi.js": 347,
	"./fo": 348,
	"./fo.js": 348,
	"./fr": 351,
	"./fr-ca": 349,
	"./fr-ca.js": 349,
	"./fr-ch": 350,
	"./fr-ch.js": 350,
	"./fr.js": 351,
	"./fy": 352,
	"./fy.js": 352,
	"./gd": 353,
	"./gd.js": 353,
	"./gl": 354,
	"./gl.js": 354,
	"./gom-latn": 355,
	"./gom-latn.js": 355,
	"./gu": 356,
	"./gu.js": 356,
	"./he": 357,
	"./he.js": 357,
	"./hi": 358,
	"./hi.js": 358,
	"./hr": 359,
	"./hr.js": 359,
	"./hu": 360,
	"./hu.js": 360,
	"./hy-am": 361,
	"./hy-am.js": 361,
	"./id": 362,
	"./id.js": 362,
	"./is": 363,
	"./is.js": 363,
	"./it": 364,
	"./it.js": 364,
	"./ja": 365,
	"./ja.js": 365,
	"./jv": 366,
	"./jv.js": 366,
	"./ka": 367,
	"./ka.js": 367,
	"./kk": 368,
	"./kk.js": 368,
	"./km": 369,
	"./km.js": 369,
	"./kn": 370,
	"./kn.js": 370,
	"./ko": 371,
	"./ko.js": 371,
	"./ky": 372,
	"./ky.js": 372,
	"./lb": 373,
	"./lb.js": 373,
	"./lo": 374,
	"./lo.js": 374,
	"./lt": 375,
	"./lt.js": 375,
	"./lv": 376,
	"./lv.js": 376,
	"./me": 377,
	"./me.js": 377,
	"./mi": 378,
	"./mi.js": 378,
	"./mk": 379,
	"./mk.js": 379,
	"./ml": 380,
	"./ml.js": 380,
	"./mr": 381,
	"./mr.js": 381,
	"./ms": 383,
	"./ms-my": 382,
	"./ms-my.js": 382,
	"./ms.js": 383,
	"./mt": 384,
	"./mt.js": 384,
	"./my": 385,
	"./my.js": 385,
	"./nb": 386,
	"./nb.js": 386,
	"./ne": 387,
	"./ne.js": 387,
	"./nl": 389,
	"./nl-be": 388,
	"./nl-be.js": 388,
	"./nl.js": 389,
	"./nn": 390,
	"./nn.js": 390,
	"./pa-in": 391,
	"./pa-in.js": 391,
	"./pl": 392,
	"./pl.js": 392,
	"./pt": 394,
	"./pt-br": 393,
	"./pt-br.js": 393,
	"./pt.js": 394,
	"./ro": 395,
	"./ro.js": 395,
	"./ru": 396,
	"./ru.js": 396,
	"./sd": 397,
	"./sd.js": 397,
	"./se": 398,
	"./se.js": 398,
	"./si": 399,
	"./si.js": 399,
	"./sk": 400,
	"./sk.js": 400,
	"./sl": 401,
	"./sl.js": 401,
	"./sq": 402,
	"./sq.js": 402,
	"./sr": 404,
	"./sr-cyrl": 403,
	"./sr-cyrl.js": 403,
	"./sr.js": 404,
	"./ss": 405,
	"./ss.js": 405,
	"./sv": 406,
	"./sv.js": 406,
	"./sw": 407,
	"./sw.js": 407,
	"./ta": 408,
	"./ta.js": 408,
	"./te": 409,
	"./te.js": 409,
	"./tet": 410,
	"./tet.js": 410,
	"./tg": 411,
	"./tg.js": 411,
	"./th": 412,
	"./th.js": 412,
	"./tl-ph": 413,
	"./tl-ph.js": 413,
	"./tlh": 414,
	"./tlh.js": 414,
	"./tr": 415,
	"./tr.js": 415,
	"./tzl": 416,
	"./tzl.js": 416,
	"./tzm": 418,
	"./tzm-latn": 417,
	"./tzm-latn.js": 417,
	"./tzm.js": 418,
	"./ug-cn": 419,
	"./ug-cn.js": 419,
	"./uk": 420,
	"./uk.js": 420,
	"./ur": 421,
	"./ur.js": 421,
	"./uz": 423,
	"./uz-latn": 422,
	"./uz-latn.js": 422,
	"./uz.js": 423,
	"./vi": 424,
	"./vi.js": 424,
	"./x-pseudo": 425,
	"./x-pseudo.js": 425,
	"./yo": 426,
	"./yo.js": 426,
	"./zh-cn": 427,
	"./zh-cn.js": 427,
	"./zh-hk": 428,
	"./zh-hk.js": 428,
	"./zh-tw": 429,
	"./zh-tw.js": 429
};
function webpackContext(req) {
	return __webpack_require__(webpackContextResolve(req));
};
function webpackContextResolve(req) {
	var id = map[req];
	if(!(id + 1)) // check for number
		throw new Error("Cannot find module '" + req + "'.");
	return id;
};
webpackContext.keys = function webpackContextKeys() {
	return Object.keys(map);
};
webpackContext.resolve = webpackContextResolve;
module.exports = webpackContext;
webpackContext.id = 782;


/***/ }),

/***/ 783:
/***/ (function(module, exports) {

module.exports = "<div #baAmChart class=\"ba-am-chart {{baAmChartClass || ''}}\"></div>\n"

/***/ }),

/***/ 784:
/***/ (function(module, exports) {

module.exports = "<div baCardBlur class=\"animated fadeIn card {{cardType}} {{baCardClass || ''}}\" zoom-in>\n  <div *ngIf=\"cardTitle\" class=\"card-header clearfix\">\n    <div class=\"row\">\n        <div class=\"col-lg-6 col-md-6\">\n            <h3 style=\"float: right;\" class=\"card-title\" translate>{{cardTitle}}</h3>\n          </div>\n    </div>\n  </div>\n  <div class=\"card-body\">\n    <ng-content></ng-content>\n  </div>\n</div>\n"

/***/ }),

/***/ 785:
/***/ (function(module, exports) {

module.exports = "<div #baChartistChart class=\"ba-chartist-chart {{baChartistChartClass || ''}}\"></div>\n"

/***/ }),

/***/ 786:
/***/ (function(module, exports) {

module.exports = "<div class=\"{{baCheckboxClass}}\">\n  <label class=\"checkbox-inline custom-checkbox nowrap\">\n    <input type=\"checkbox\" [checked]=state\n           (change)=\"onChange($event.target.checked)\"\n           [disabled]=\"disabled\" [value]=\"value\">\n    <span>{{label}}</span>\n  </label>\n</div>\n"

/***/ }),

/***/ 787:
/***/ (function(module, exports) {

module.exports = "<div class=\"content-top clearfix\">\n  <h1 class=\"al-title\" translate>{{ activePageTitle }}</h1>\n\n  <ul class=\"breadcrumb al-breadcrumb\">\n    <li class=\"breadcrumb-item\">\n      <a routerLink=\"/main/dashboard\" translate>{{'general.home'}}</a>\n    </li>\n    <li class=\"breadcrumb-item active\" translate>{{ activePageTitle }}</li>\n  </ul>\n</div>\n"

/***/ }),

/***/ 788:
/***/ (function(module, exports) {

module.exports = "<input #fileUpload ngFileSelect type=\"file\" [options]=\"fileUploaderOptions\" (onUpload)=\"_onFileUpload($event)\" (beforeUpload)=\"beforeFileUpload($event)\" hidden=\"true\">\n<div class=\"input-group\" [ngClass]=\"{uploading: uploadFileInProgress}\">\n    <input #inputText type=\"text\" [value]=\"defaultValue\" class=\"form-control\" readonly>\n    <span class=\"input-group-btn\">\n      <button class=\"btn btn-success\" type=\"button\" (click)=\"bringFileSelector();\">Browse</button>\n  </span>\n</div>"

/***/ }),

/***/ 789:
/***/ (function(module, exports) {

module.exports = "<div #baFullCalendar class=\"ba-full-calendar {{baFullCalendarClass || ''}}\"></div>\n"

/***/ }),

/***/ 790:
/***/ (function(module, exports) {

module.exports = "<ul id=\"al-sidebar-list\" class=\"al-sidebar-list\" baSlimScroll [baSlimScrollOptions]=\"{height: menuHeight}\"\n    (mouseleave)=\"hoverElemTop=outOfArea\">\n  <ba-menu-item\n    [menuItem]=\"item\"\n    (itemHover)=\"hoverItem($event)\"\n    (toggleSubMenu)=\"toggleSubMenu($event)\"\n    *ngFor=\"let item of menuItems\"></ba-menu-item>\n</ul>\n<div class=\"sidebar-hover-elem\" [ngStyle]=\"{top: hoverElemTop + 'px', height: hoverElemHeight + 'px'}\"\n     [ngClass]=\"{'show-hover-elem': showHoverElem }\">\n</div>\n"

/***/ }),

/***/ 791:
/***/ (function(module, exports) {

module.exports = "<li *ngIf=\"!menuItem.hidden\" [title]=\"menuItem.title | translate\" [ngClass]=\"{'al-sidebar-list-item': !child, 'ba-sidebar-sublist-item': child, 'selected': menuItem.selected && !menuItem.expanded, 'with-sub-menu': menuItem.children, 'ba-sidebar-item-expanded': menuItem.expanded}\">\n\n  <a *ngIf=\"!menuItem.children && !menuItem.url\" (mouseenter)=\"onHoverItem($event, item)\" [routerLink]=\"menuItem.route.paths\" class=\"al-sidebar-list-link\">\n    <i *ngIf=\"menuItem.icon\" class=\"{{ menuItem.icon }}\"></i><span translate>{{ menuItem.title }}</span>\n  </a>\n\n  <a *ngIf=\"!menuItem.children && menuItem.url\" (mouseenter)=\"onHoverItem($event, item)\" [href]=\"menuItem.url\" [target]=\"menuItem.target\" class=\"al-sidebar-list-link\">\n    <i *ngIf=\"menuItem.icon\" class=\"{{ menuItem.icon }}\"></i><span translate>{{ menuItem.title }}</span>\n  </a>\n\n  <a *ngIf=\"menuItem.children\" (mouseenter)=\"onHoverItem($event, item)\" href (click)=\"onToggleSubMenu($event, menuItem)\" class=\"al-sidebar-list-link\">\n    <i *ngIf=\"menuItem.icon\" class=\"{{ menuItem.icon }}\"></i><span translate>{{ menuItem.title }}</span>\n    <b class=\"fa fa-angle-down\" [ngClass]=\"{'fa-angle-up': menuItem.expanded}\"></b>\n  </a>\n\n  <ul *ngIf=\"menuItem.children\" class=\"al-sidebar-sublist\" [ngClass]=\"{'slide-right': menuItem.slideRight}\">\n    <ba-menu-item [menuItem]=\"subItem\"\n                  [child]=\"true\"\n                  (itemHover)=\"onHoverItem($event)\"\n                  (toggleSubMenu)=\"onToggleSubMenu($event, subItem)\"\n                  *ngFor=\"let subItem of menuItem.children\"></ba-menu-item>\n  </ul>\n\n</li>\n"

/***/ }),

/***/ 792:
/***/ (function(module, exports) {

module.exports = "<ul class=\"al-msg-center clearfix\">\n  <li class=\"dropdown\">\n    <a href class=\"dropdown-toggle\" id=\"msg-dd1\" data-toggle=\"dropdown\" aria-expanded=\"false\">\n      <i class=\"fa fa-bell-o\"></i><span>{{ notifications.length }}</span>\n\n      <div class=\"notification-ring\"></div>\n    </a>\n\n    <div class=\"top-dropdown-menu dropdown-menu\" aria-labelledby=\"msg-dd1\">\n      <i class=\"dropdown-arr\"></i>\n\n      <div class=\"header clearfix\">\n        <strong>Notifications</strong>\n        <a href>Mark All as Read</a>\n        <a href>Settings</a>\n      </div>\n      <div class=\"msg-list\">\n        <a *ngFor=\"let msg of notifications\" href class=\"clearfix\">\n          <div class=\"img-area\"><img [ngClass]=\"{'photo-msg-item': !msg.image}\"\n                                     src=\"{{ ( msg.image ||  (msg.name | baProfilePicture)) }}\"></div>\n          <div class=\"msg-area\">\n            <div>{{ msg.text }}</div>\n            <span>{{ msg.time }}</span>\n          </div>\n        </a>\n      </div>\n      <a href>See all notifications</a>\n    </div>\n  </li>\n</ul>\n"

/***/ }),

/***/ 793:
/***/ (function(module, exports) {

module.exports = "<div class=\"{{baMultiCheckboxClass}} container-content\">\n  <ba-checkbox *ngFor=\"let item of state\"\n               [(ngModel)]=\"item[propertiesMapping.model]\"\n               [baCheckboxClass]=\"getProp(item, 'baCheckboxClass')\"\n               [label]=\"getProp(item, 'label')\"\n               [disabled]=\"getProp(item, 'disabled')\"\n               [value]=\"getProp(item, 'value')\"\n               id=\"{{getProp(item, 'id')}}\">\n  </ba-checkbox>\n</div>\n"

/***/ }),

/***/ 794:
/***/ (function(module, exports) {

module.exports = "<div class=\"page-top clearfix\" baScrollPosition maxHeight=\"50\" (scrollChange)=\"scrolledChanged($event)\"\n     [ngClass]=\"{scrolled: isScrolled}\">\n  <a routerLink=\"/main/dashboard\" class=\"al-logo clearfix\"><img style=\"height: 50px;width:70px\" src=\"assets/images/eiu.png\"></a>\n  <a href (click)=\"toggleMenu()\" class=\"collapse-menu-link ion-navicon\"></a>\n\n  <div class=\"search\">\n    <i class=\"ion-ios-search-strong\" ng-click=\"startSearch()\"></i>\n    <input id=\"searchInput\" type=\"text\" placeholder=\"Search for...\">\n  </div>\n\n  <div class=\"user-profile clearfix\">\n    <div class=\"dropdown al-user-profile\">\n      <a class=\"profile-toggle-link dropdown-toggle\" id=\"user-profile-dd\" data-toggle=\"dropdown\" aria-expanded=\"false\">\n        <img src=\"{{ ( 'no-photo' | baProfilePicture ) }}\">\n      </a>\n      <ul class=\"dropdown-menu top-dropdown-menu profile-dropdown\" aria-labelledby=\"user-profile-dd\">\n        <li class=\"dropdown-item\"><a href (click)=\"logout()\"><i class=\"fa fa-power-off\"></i>Sign out</a></li>\n      </ul>\n    </div>\n    <ba-msg-center></ba-msg-center>\n  </div>\n</div>\n"

/***/ }),

/***/ 795:
/***/ (function(module, exports) {

module.exports = "<div class=\"picture-group\" [ngClass]=\"{uploading: uploadInProgress}\">\n  <div class=\"picture-wrapper\" (click)=\"bringFileSelector();\">\n    <img [src]=\"picture\" *ngIf=\"picture\">\n    <img [src]=\"defaultPicture\" *ngIf=\"!!!picture && !!defaultPicture\">\n\n    <div class=\"loading\" *ngIf=\"uploadInProgress\">\n      <div class=\"spinner\">\n        <div class=\"double-bounce1\"></div>\n        <div class=\"double-bounce2\"></div>\n      </div>\n    </div>\n  </div>\n  <i class=\"ion-ios-close-outline\" (click)=\"removePicture();\" *ngIf=\"picture && canDelete\"></i>\n  <a href class=\"change-picture\" (click)=\"bringFileSelector();\">Change profile Picture</a>\n  <input #fileUpload ngFileSelect [options]=\"uploaderOptions\"\n         (onUpload)=\"_onUpload($event)\"\n         (beforeUpload)=\"beforeUpload($event)\"\n         type=\"file\" hidden=\"true\">\n</div>\n"

/***/ }),

/***/ 796:
/***/ (function(module, exports) {

module.exports = "<aside class=\"al-sidebar\" sidebarResize>\n  <ba-menu [menuHeight]=\"menuHeight\"\n           [sidebarCollapsed]=\"isMenuCollapsed\"\n           (expandMenu)=\"menuExpand()\"></ba-menu>\n</aside>\n"

/***/ }),

/***/ 80:
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__(0);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__angular_http__ = __webpack_require__(66);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__core_common_system_constants__ = __webpack_require__(33);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_3__models_loggedin_user__ = __webpack_require__(524);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_4_rxjs_add_operator_map__ = __webpack_require__(151);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_4_rxjs_add_operator_map___default = __webpack_require__.n(__WEBPACK_IMPORTED_MODULE_4_rxjs_add_operator_map__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return AuthenService; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};





var AuthenService = (function () {
    function AuthenService(_http) {
        this._http = _http;
    }
    AuthenService.prototype.login = function (email, password) {
        var headers = new __WEBPACK_IMPORTED_MODULE_1__angular_http__["b" /* Headers */]();
        headers.append("Content-Type", "application/JSON");
        headers.append("Access-Control-Allow-Methods", "GET, POST, OPTIONS, PUT, PATCH, DELETE");
        headers.append("Access-Control-Allow-Methods", "X-Requested-With,content-type");
        headers.append("Access-Control-Allow-Credentials", "true");
        var options = new __WEBPACK_IMPORTED_MODULE_1__angular_http__["e" /* RequestOptions */]({ headers: headers });
        return this._http.post(__WEBPACK_IMPORTED_MODULE_2__core_common_system_constants__["a" /* SystemConstants */].BASE_URL + '/account/generateToken', { email: email, password: password }, options)
            .map(function (response) {
            var user = response.json();
            if (user && user.access_token) {
                localStorage.removeItem(__WEBPACK_IMPORTED_MODULE_2__core_common_system_constants__["a" /* SystemConstants */].CURRENT_USER);
                localStorage.setItem(__WEBPACK_IMPORTED_MODULE_2__core_common_system_constants__["a" /* SystemConstants */].CURRENT_USER, JSON.stringify(user));
            }
        });
    };
    AuthenService.prototype.logout = function () {
        localStorage.removeItem(__WEBPACK_IMPORTED_MODULE_2__core_common_system_constants__["a" /* SystemConstants */].CURRENT_USER);
    };
    AuthenService.prototype.isUserAuthenticated = function () {
        var user = localStorage.getItem(__WEBPACK_IMPORTED_MODULE_2__core_common_system_constants__["a" /* SystemConstants */].CURRENT_USER);
        if (user != null) {
            return true;
        }
        else
            return false;
    };
    AuthenService.prototype.getLoggedInUser = function () {
        var user;
        if (this.isUserAuthenticated()) {
            var userData = JSON.parse(localStorage.getItem(__WEBPACK_IMPORTED_MODULE_2__core_common_system_constants__["a" /* SystemConstants */].CURRENT_USER));
            user = new __WEBPACK_IMPORTED_MODULE_3__models_loggedin_user__["a" /* LoggedInUser */](userData.access_token, userData.avatar, userData.email, userData.fullName, userData.role, userData.major, userData.userName);
        }
        else
            user = null;
        return user;
    };
    return AuthenService;
}());
AuthenService = __decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["Injectable"])(),
    __metadata("design:paramtypes", [typeof (_a = typeof __WEBPACK_IMPORTED_MODULE_1__angular_http__["c" /* Http */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_1__angular_http__["c" /* Http */]) === "function" && _a || Object])
], AuthenService);

var _a;
//# sourceMappingURL=authen.service.js.map

/***/ }),

/***/ 820:
/***/ (function(module, exports, __webpack_require__) {

module.exports = __webpack_require__(439);


/***/ })

},[820]);
//# sourceMappingURL=main.bundle.js.map