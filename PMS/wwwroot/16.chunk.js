webpackJsonp([16,26],{

/***/ 1003:
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return ProjectTypesConstants; });
var ProjectTypesConstants = (function () {
    function ProjectTypesConstants() {
    }
    return ProjectTypesConstants;
}());

ProjectTypesConstants.A = "Basic Thesis";
ProjectTypesConstants.B = "Academic Thesis";
ProjectTypesConstants.C = "Internship";
ProjectTypesConstants.D = "Final Thesis";
//# sourceMappingURL=projectType.constants.js.map

/***/ }),

/***/ 1279:
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__(0);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__theme__ = __webpack_require__(34);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return CalendarService; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};


var CalendarService = (function () {
    function CalendarService(_baConfig) {
        this._baConfig = _baConfig;
    }
    CalendarService.prototype.getData = function () {
        var dashboardColors = this._baConfig.get().colors.dashboard;
        return {
            header: {
                left: 'prev,next today',
                center: 'title',
                right: 'month,agendaWeek,agendaDay'
            },
            defaultDate: '2016-03-08',
            selectable: true,
            selectHelper: true,
            editable: true,
            eventLimit: true,
            events: [
                {
                    title: 'All Day Event',
                    start: '2016-03-01',
                    color: dashboardColors.silverTree
                },
                {
                    title: 'Long Event',
                    start: '2016-03-07',
                    end: '2016-03-10',
                    color: dashboardColors.blueStone
                },
                {
                    title: 'Dinner',
                    start: '2016-03-14T20:00:00',
                    color: dashboardColors.surfieGreen
                },
                {
                    title: 'Birthday Party',
                    start: '2016-04-01T07:00:00',
                    color: dashboardColors.gossip
                }
            ]
        };
    };
    return CalendarService;
}());
CalendarService = __decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["Injectable"])(),
    __metadata("design:paramtypes", [typeof (_a = typeof __WEBPACK_IMPORTED_MODULE_1__theme__["b" /* BaThemeConfigProvider */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_1__theme__["b" /* BaThemeConfigProvider */]) === "function" && _a || Object])
], CalendarService);

var _a;
//# sourceMappingURL=calendar.service.js.map

/***/ }),

/***/ 1280:
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__core_services_data_service__ = __webpack_require__(853);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__angular_router__ = __webpack_require__(20);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2_rxjs_Observable__ = __webpack_require__(6);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2_rxjs_Observable___default = __webpack_require__.n(__WEBPACK_IMPORTED_MODULE_2_rxjs_Observable__);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_3__core_services_notification_service__ = __webpack_require__(848);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_4__angular_core__ = __webpack_require__(0);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_5__core_services_authen_service__ = __webpack_require__(80);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_6__core_common_projectType_constants__ = __webpack_require__(1003);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_7_rxjs_add_observable_forkJoin__ = __webpack_require__(875);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_7_rxjs_add_observable_forkJoin___default = __webpack_require__.n(__WEBPACK_IMPORTED_MODULE_7_rxjs_add_observable_forkJoin__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return Dashboard; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};








var Dashboard = (function () {
    function Dashboard(router, _authenService, _dataService, _notificationService) {
        this.router = router;
        this._authenService = _authenService;
        this._dataService = _dataService;
        this._notificationService = _notificationService;
        this.types = [__WEBPACK_IMPORTED_MODULE_6__core_common_projectType_constants__["a" /* ProjectTypesConstants */].A, __WEBPACK_IMPORTED_MODULE_6__core_common_projectType_constants__["a" /* ProjectTypesConstants */].B, __WEBPACK_IMPORTED_MODULE_6__core_common_projectType_constants__["a" /* ProjectTypesConstants */].C, __WEBPACK_IMPORTED_MODULE_6__core_common_projectType_constants__["a" /* ProjectTypesConstants */].D];
        this.groupsAccepted = {};
        this.groupsStudent = {};
        this.groups = {};
        this.isSaved = false;
        this.isAdmin = false;
        this.isStudent = false;
        this.isLecturer = false;
        this.isLoadData = false;
        this.isLoadLecturerCouncil = false;
    }
    Dashboard.prototype.ngOnInit = function () {
        var _this = this;
        __WEBPACK_IMPORTED_MODULE_2_rxjs_Observable__["Observable"].forkJoin([
            this._dataService.get("/api/quarters/getall"),
            this._dataService.get("/api/projects/getall"),
            this._dataService.get("/api/lecturers/getall"),
            this._dataService.get("/api/students/getall")
        ]).subscribe(function (data) {
            _this.quarters = data[0].items,
                _this.totalProjects = data[1].totalItems,
                _this.lecturers = data[2].items,
                _this.totalLecturers = data[2].totalItems,
                _this.totalStudents = data[3].totalItems,
                _this.isLoadData = true;
        });
        this.user = this._authenService.getLoggedInUser();
        this.permissionAccess();
        if (this.user.role === "Student") {
            this.loadStudentGroup();
        }
        if (this.user.role === "Lecturer") {
            this.loadLecturerData();
        }
    };
    Dashboard.prototype.loadLecturerData = function () {
        this.loadLecturerGroup();
        this.loadLecturerGroupAccepted();
    };
    Dashboard.prototype.loadLecturerGroup = function () {
        var _this = this;
        this._dataService.get("/api/lecturers/getgroups/" + this.user.email + "?isConfirm=Pending&page=3").subscribe(function (response) {
            _this.groups = response;
            _this.isLoadData = true;
        });
    };
    Dashboard.prototype.loadLecturerGroupAccepted = function () {
        var _this = this;
        this._dataService.get("/api/lecturers/getgroups/" + this.user.email + "?isConfirm=Accepted&pageSize=3").subscribe(function (response) {
            _this.groupsAccepted = response;
            _this.isLoadData = true;
        });
    };
    Dashboard.prototype.loadStudentGroup = function () {
        var _this = this;
        this._dataService.get("/api/students/getgroups/" + this.user.email + "?pageSize=3").subscribe(function (response) {
            _this.groupsStudent = response;
            _this.isLoadData = true;
        });
    };
    Dashboard.prototype.permissionAccess = function () {
        if (this.user.role === "Admin") {
            this.isAdmin = true;
        }
        if (this.user.role === "Lecturer") {
            this.isLecturer = true;
        }
        if (this.user.role === "Student") {
            this.isStudent = true;
        }
    };
    return Dashboard;
}());
Dashboard = __decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_4__angular_core__["Component"])({
        selector: 'dashboard',
        styles: [__webpack_require__(1489)],
        template: __webpack_require__(1574)
    }),
    __metadata("design:paramtypes", [typeof (_a = typeof __WEBPACK_IMPORTED_MODULE_1__angular_router__["b" /* Router */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_1__angular_router__["b" /* Router */]) === "function" && _a || Object, typeof (_b = typeof __WEBPACK_IMPORTED_MODULE_5__core_services_authen_service__["a" /* AuthenService */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_5__core_services_authen_service__["a" /* AuthenService */]) === "function" && _b || Object, typeof (_c = typeof __WEBPACK_IMPORTED_MODULE_0__core_services_data_service__["a" /* DataService */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_0__core_services_data_service__["a" /* DataService */]) === "function" && _c || Object, typeof (_d = typeof __WEBPACK_IMPORTED_MODULE_3__core_services_notification_service__["a" /* NotificationService */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_3__core_services_notification_service__["a" /* NotificationService */]) === "function" && _d || Object])
], Dashboard);

var _a, _b, _c, _d;
//# sourceMappingURL=dashboard.component.js.map

/***/ }),

/***/ 1353:
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__(0);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1_jquery__ = __webpack_require__(208);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1_jquery___default = __webpack_require__.n(__WEBPACK_IMPORTED_MODULE_1_jquery__);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__calendar_service__ = __webpack_require__(1279);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return Calendar; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};



var Calendar = (function () {
    function Calendar(_calendarService) {
        var _this = this;
        this._calendarService = _calendarService;
        this.calendarConfiguration = this._calendarService.getData();
        this.calendarConfiguration.select = function (start, end) { return _this._onSelect(start, end); };
    }
    Calendar.prototype.onCalendarReady = function (calendar) {
        this._calendar = calendar;
    };
    Calendar.prototype._onSelect = function (start, end) {
        if (this._calendar != null) {
            var title = prompt('Event Title:');
            var eventData = void 0;
            if (title) {
                eventData = {
                    title: title,
                    start: start,
                    end: end
                };
                __WEBPACK_IMPORTED_MODULE_1_jquery__(this._calendar).fullCalendar('renderEvent', eventData, true);
            }
            __WEBPACK_IMPORTED_MODULE_1_jquery__(this._calendar).fullCalendar('unselect');
        }
    };
    return Calendar;
}());
Calendar = __decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["Component"])({
        selector: 'calendar',
        template: __webpack_require__(1573),
        styles: [__webpack_require__(1488)]
    }),
    __metadata("design:paramtypes", [typeof (_a = typeof __WEBPACK_IMPORTED_MODULE_2__calendar_service__["a" /* CalendarService */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_2__calendar_service__["a" /* CalendarService */]) === "function" && _a || Object])
], Calendar);

var _a;
//# sourceMappingURL=calendar.component.js.map

/***/ }),

/***/ 1354:
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__calendar_component__ = __webpack_require__(1353);
/* harmony namespace reexport (by used) */ __webpack_require__.d(__webpack_exports__, "a", function() { return __WEBPACK_IMPORTED_MODULE_0__calendar_component__["a"]; });

//# sourceMappingURL=index.js.map

/***/ }),

/***/ 1355:
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_router__ = __webpack_require__(20);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__dashboard_component__ = __webpack_require__(1280);
/* unused harmony export routes */
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return routing; });


// noinspection TypeScriptValidateTypes
var routes = [
    {
        path: '',
        component: __WEBPACK_IMPORTED_MODULE_1__dashboard_component__["a" /* Dashboard */],
        children: []
    }
];
var routing = __WEBPACK_IMPORTED_MODULE_0__angular_router__["a" /* RouterModule */].forChild(routes);
//# sourceMappingURL=dashboard.routing.js.map

/***/ }),

/***/ 1488:
/***/ (function(module, exports, __webpack_require__) {

exports = module.exports = __webpack_require__(4)(false);
// imports


// module
exports.push([module.i, ":host /deep/ ba-full-calendar div.blurCalendar {\n  font-size: 12px; }\n\n:host /deep/ ba-full-calendar .fc {\n  direction: ltr;\n  text-align: left; }\n  :host /deep/ ba-full-calendar .fc button {\n    box-sizing: border-box;\n    margin: 0;\n    height: 2.1em;\n    padding: 0 .6em;\n    font-size: 1em;\n    white-space: nowrap;\n    cursor: pointer; }\n    :host /deep/ ba-full-calendar .fc button::-moz-focus-inner {\n      margin: 0;\n      padding: 0; }\n    :host /deep/ ba-full-calendar .fc button .fc-icon {\n      position: relative;\n      top: 0; }\n  :host /deep/ ba-full-calendar .fc .fc-button-group > * {\n    float: left;\n    margin: 0 0 0 -1px; }\n  :host /deep/ ba-full-calendar .fc .fc-button-group > :first-child {\n    margin-left: 0; }\n  :host /deep/ ba-full-calendar .fc hr {\n    height: 0;\n    margin: 0;\n    padding: 0 0 2px;\n    border-style: solid;\n    border-width: 1px 0; }\n  :host /deep/ ba-full-calendar .fc table {\n    width: 100%;\n    table-layout: fixed;\n    border-collapse: collapse;\n    border-spacing: 0;\n    font-size: 1em; }\n  :host /deep/ ba-full-calendar .fc th {\n    text-align: center; }\n  :host /deep/ ba-full-calendar .fc th, :host /deep/ ba-full-calendar .fc td {\n    border: 1px solid rgba(255, 255, 255, 0.3);\n    padding: 0;\n    vertical-align: top; }\n  :host /deep/ ba-full-calendar .fc td.fc-today {\n    border-style: double; }\n  :host /deep/ ba-full-calendar .fc .fc-row {\n    border: 0 solid; }\n  :host /deep/ ba-full-calendar .fc .fc-toolbar > * > * {\n    float: left;\n    margin-left: .75em; }\n  :host /deep/ ba-full-calendar .fc .fc-toolbar > * > :first-child {\n    margin-left: 0; }\n  :host /deep/ ba-full-calendar .fc .fc-axis {\n    vertical-align: middle;\n    padding: 0 4px;\n    white-space: nowrap; }\n\n:host /deep/ ba-full-calendar .fc-rtl {\n  text-align: right; }\n\n:host /deep/ ba-full-calendar .fc-unthemed th, :host /deep/ ba-full-calendar .fc-unthemed td, :host /deep/ ba-full-calendar .fc-unthemed hr, :host /deep/ ba-full-calendar .fc-unthemed thead, :host /deep/ ba-full-calendar .fc-unthemed tbody, :host /deep/ ba-full-calendar .fc-unthemed .fc-row, :host /deep/ ba-full-calendar .fc-unthemed .fc-popover {\n  border-color: rgba(255, 255, 255, 0.3); }\n\n:host /deep/ ba-full-calendar .fc-unthemed .fc-popover {\n  background-color: #ffffff;\n  border: 1px solid; }\n  :host /deep/ ba-full-calendar .fc-unthemed .fc-popover .fc-header {\n    background: #eee; }\n    :host /deep/ ba-full-calendar .fc-unthemed .fc-popover .fc-header .fc-close {\n      color: #666666;\n      font-size: 25px;\n      margin-top: 4px; }\n\n:host /deep/ ba-full-calendar .fc-unthemed hr {\n  background: #eee; }\n\n:host /deep/ ba-full-calendar .fc-unthemed .fc-today {\n  background: rgba(255, 255, 255, 0.15); }\n\n:host /deep/ ba-full-calendar .fc-highlight {\n  background: rgba(255, 255, 255, 0.25);\n  opacity: .3; }\n\n:host /deep/ ba-full-calendar .fc-icon {\n  display: inline-block;\n  font-size: 2em;\n  font-family: \"Courier New\", Courier, monospace; }\n\n:host /deep/ ba-full-calendar .fc-icon-left-single-arrow:after {\n  content: \"\\2039\";\n  font-weight: 700;\n  font-size: 100%; }\n\n:host /deep/ ba-full-calendar .fc-icon-right-single-arrow:after {\n  content: \"\\203A\";\n  font-weight: 700;\n  font-size: 100%; }\n\n:host /deep/ ba-full-calendar .fc-icon-left-double-arrow:after {\n  content: \"\\AB\"; }\n\n:host /deep/ ba-full-calendar .fc-icon-right-double-arrow:after {\n  content: \"\\BB\"; }\n\n:host /deep/ ba-full-calendar .fc-icon-x:after {\n  content: \"\\D7\"; }\n\n:host /deep/ ba-full-calendar .fc-state-default {\n  border: 1px solid;\n  outline: none;\n  background: #f5f5f5 repeat-x;\n  border-color: #e6e6e6 #e6e6e6 #bfbfbf;\n  border-color: rgba(0, 0, 0, 0.1) rgba(0, 0, 0, 0.1) rgba(0, 0, 0, 0.1);\n  color: #333333; }\n  :host /deep/ ba-full-calendar .fc-state-default.fc-corner-left {\n    border-top-left-radius: 0px;\n    border-bottom-left-radius: 0px; }\n  :host /deep/ ba-full-calendar .fc-state-default.fc-corner-right {\n    border-top-right-radius: 0px;\n    border-bottom-right-radius: 0px; }\n\n:host /deep/ ba-full-calendar .fc-state-hover,\n:host /deep/ ba-full-calendar .fc-state-down,\n:host /deep/ ba-full-calendar .fc-state-active,\n:host /deep/ ba-full-calendar .fc-state-disabled {\n  color: #333333;\n  background-color: rgba(255, 255, 255, 0.49); }\n\n:host /deep/ ba-full-calendar .fc-state-hover {\n  color: #333333;\n  text-decoration: none;\n  background-position: 0 -15px;\n  transition: background-position 0.1s linear; }\n\n:host /deep/ ba-full-calendar .fc-state-down,\n:host /deep/ ba-full-calendar .fc-state-active {\n  background: #cccccc none; }\n\n:host /deep/ ba-full-calendar .fc-state-disabled {\n  cursor: default;\n  background-image: none;\n  opacity: 0.65;\n  box-shadow: none; }\n\n:host /deep/ ba-full-calendar .fc-button-group {\n  display: inline-block; }\n\n:host /deep/ ba-full-calendar .fc-popover {\n  position: absolute; }\n  :host /deep/ ba-full-calendar .fc-popover .fc-header {\n    padding: 2px 4px; }\n  :host /deep/ ba-full-calendar .fc-popover .fc-header .fc-title {\n    margin: 0 2px; }\n  :host /deep/ ba-full-calendar .fc-popover .fc-header .fc-close {\n    cursor: pointer; }\n\n:host /deep/ ba-full-calendar .fc-ltr .fc-popover .fc-header .fc-title,\n:host /deep/ ba-full-calendar .fc-rtl .fc-popover .fc-header .fc-close {\n  float: left; }\n\n:host /deep/ ba-full-calendar .fc-rtl .fc-popover .fc-header .fc-title,\n:host /deep/ ba-full-calendar .fc-ltr .fc-popover .fc-header .fc-close {\n  float: right; }\n\n:host /deep/ ba-full-calendar .fc-popover > .ui-widget-header + .ui-widget-content {\n  border-top: 0; }\n\n:host /deep/ ba-full-calendar .fc-clear {\n  clear: both; }\n\n:host /deep/ ba-full-calendar .fc-bg,\n:host /deep/ ba-full-calendar .fc-highlight-skeleton,\n:host /deep/ ba-full-calendar .fc-helper-skeleton {\n  position: absolute;\n  top: 0;\n  left: 0;\n  right: 0; }\n\n:host /deep/ ba-full-calendar .fc-bg {\n  bottom: 0; }\n\n:host /deep/ ba-full-calendar .fc-bg table {\n  height: 100%; }\n\n:host /deep/ ba-full-calendar .fc-row {\n  position: relative; }\n  :host /deep/ ba-full-calendar .fc-row table {\n    border-left: 0 hidden transparent;\n    border-right: 0 hidden transparent;\n    border-bottom: 0 hidden transparent; }\n  :host /deep/ ba-full-calendar .fc-row:first-child table {\n    border-top: 0 hidden transparent; }\n  :host /deep/ ba-full-calendar .fc-row .fc-bg {\n    z-index: 1; }\n  :host /deep/ ba-full-calendar .fc-row .fc-highlight-skeleton {\n    z-index: 2;\n    bottom: 0; }\n    :host /deep/ ba-full-calendar .fc-row .fc-highlight-skeleton table {\n      height: 100%; }\n    :host /deep/ ba-full-calendar .fc-row .fc-highlight-skeleton td {\n      border-color: transparent; }\n  :host /deep/ ba-full-calendar .fc-row .fc-content-skeleton {\n    position: relative;\n    z-index: 3;\n    padding-bottom: 2px; }\n  :host /deep/ ba-full-calendar .fc-row .fc-helper-skeleton {\n    z-index: 4; }\n  :host /deep/ ba-full-calendar .fc-row .fc-content-skeleton td,\n  :host /deep/ ba-full-calendar .fc-row .fc-helper-skeleton td {\n    background: none;\n    border-color: transparent;\n    border-bottom: 0; }\n  :host /deep/ ba-full-calendar .fc-row .fc-content-skeleton tbody td,\n  :host /deep/ ba-full-calendar .fc-row .fc-helper-skeleton tbody td {\n    border-top: 0; }\n\n:host /deep/ ba-full-calendar .fc-event {\n  position: relative;\n  display: block;\n  font-size: .85em;\n  line-height: 1.3;\n  border: 1px solid #00abff;\n  background-color: #00abff;\n  font-weight: 400; }\n\n:host /deep/ ba-full-calendar .fc-event,\n:host /deep/ ba-full-calendar .fc-event:hover,\n:host /deep/ ba-full-calendar .ui-widget .fc-event {\n  color: #ffffff;\n  text-decoration: none; }\n\n:host /deep/ ba-full-calendar .fc-event[href],\n:host /deep/ ba-full-calendar .fc-event.fc-draggable {\n  cursor: pointer; }\n\n:host /deep/ ba-full-calendar .fc-day-grid-event {\n  margin: 1px 2px 0;\n  padding: 0 1px; }\n\n:host /deep/ ba-full-calendar .fc-ltr .fc-day-grid-event.fc-not-start,\n:host /deep/ ba-full-calendar .fc-rtl .fc-day-grid-event.fc-not-end {\n  margin-left: 0;\n  border-left-width: 0;\n  padding-left: 1px;\n  border-top-left-radius: 0;\n  border-bottom-left-radius: 0; }\n\n:host /deep/ ba-full-calendar .fc-ltr .fc-day-grid-event.fc-not-end,\n:host /deep/ ba-full-calendar .fc-rtl .fc-day-grid-event.fc-not-start {\n  margin-right: 0;\n  border-right-width: 0;\n  padding-right: 1px;\n  border-top-right-radius: 0;\n  border-bottom-right-radius: 0; }\n\n:host /deep/ ba-full-calendar .fc-day-grid-event > .fc-content {\n  white-space: nowrap;\n  overflow: hidden; }\n\n:host /deep/ ba-full-calendar .fc-day-grid-event .fc-time {\n  font-weight: 700; }\n\n:host /deep/ ba-full-calendar .fc-day-grid-event .fc-resizer {\n  position: absolute;\n  top: 0;\n  bottom: 0;\n  width: 7px; }\n\n:host /deep/ ba-full-calendar .fc-ltr .fc-day-grid-event .fc-resizer {\n  right: -3px;\n  cursor: e-resize; }\n\n:host /deep/ ba-full-calendar .fc-rtl .fc-day-grid-event .fc-resizer {\n  left: -3px;\n  cursor: w-resize; }\n\n:host /deep/ ba-full-calendar a.fc-more {\n  margin: 1px 3px;\n  font-size: .85em;\n  cursor: pointer;\n  text-decoration: none; }\n  :host /deep/ ba-full-calendar a.fc-more:hover {\n    text-decoration: underline; }\n\n:host /deep/ ba-full-calendar .fc-limited {\n  display: none; }\n\n:host /deep/ ba-full-calendar .fc-day-grid .fc-row {\n  z-index: 1; }\n\n:host /deep/ ba-full-calendar .fc-more-popover {\n  z-index: 2;\n  width: 220px; }\n  :host /deep/ ba-full-calendar .fc-more-popover .fc-event-container {\n    padding: 10px; }\n\n:host /deep/ ba-full-calendar .fc-toolbar {\n  text-align: center;\n  margin-bottom: 1em; }\n  :host /deep/ ba-full-calendar .fc-toolbar .fc-left {\n    float: left; }\n  :host /deep/ ba-full-calendar .fc-toolbar .fc-right {\n    float: right; }\n  :host /deep/ ba-full-calendar .fc-toolbar .fc-center {\n    display: inline-block; }\n  :host /deep/ ba-full-calendar .fc-toolbar h2 {\n    margin: 0;\n    font-size: 24px;\n    width: 100%;\n    line-height: 26px; }\n  :host /deep/ ba-full-calendar .fc-toolbar button {\n    position: relative; }\n  :host /deep/ ba-full-calendar .fc-toolbar .fc-state-hover, :host /deep/ ba-full-calendar .fc-toolbar .ui-state-hover {\n    z-index: 2; }\n  :host /deep/ ba-full-calendar .fc-toolbar .fc-state-down {\n    z-index: 3; }\n  :host /deep/ ba-full-calendar .fc-toolbar .fc-state-active,\n  :host /deep/ ba-full-calendar .fc-toolbar .ui-state-active {\n    z-index: 4; }\n  :host /deep/ ba-full-calendar .fc-toolbar button:focus {\n    z-index: 5; }\n\n:host /deep/ ba-full-calendar .fc-view-container *,\n:host /deep/ ba-full-calendar .fc-view-container *:before,\n:host /deep/ ba-full-calendar .fc-view-container *:after {\n  box-sizing: content-box; }\n\n:host /deep/ ba-full-calendar .fc-view,\n:host /deep/ ba-full-calendar .fc-view > table {\n  position: relative;\n  z-index: 1; }\n\n:host /deep/ ba-full-calendar .fc-basicWeek-view .fc-content-skeleton,\n:host /deep/ ba-full-calendar .fc-basicDay-view .fc-content-skeleton {\n  padding-top: 1px;\n  padding-bottom: 1em; }\n\n:host /deep/ ba-full-calendar .fc-basic-view tbody .fc-row {\n  min-height: 4em;\n  max-height: 70px; }\n\n:host /deep/ ba-full-calendar .fc-row.fc-rigid {\n  overflow: hidden; }\n\n:host /deep/ ba-full-calendar .fc-row.fc-rigid .fc-content-skeleton {\n  position: absolute;\n  top: 0;\n  left: 0;\n  right: 0; }\n\n:host /deep/ ba-full-calendar .fc-basic-view .fc-week-number,\n:host /deep/ ba-full-calendar .fc-basic-view .fc-day-number {\n  padding: 0 2px; }\n\n:host /deep/ ba-full-calendar .fc-basic-view td.fc-week-number span,\n:host /deep/ ba-full-calendar .fc-basic-view td.fc-day-number {\n  padding-top: 2px;\n  padding-bottom: 2px; }\n\n:host /deep/ ba-full-calendar .fc-basic-view .fc-week-number {\n  text-align: center; }\n\n:host /deep/ ba-full-calendar .fc-basic-view .fc-week-number span {\n  display: inline-block;\n  min-width: 1.25em; }\n\n:host /deep/ ba-full-calendar .fc-ltr .fc-basic-view .fc-day-number {\n  text-align: right; }\n\n:host /deep/ ba-full-calendar .fc-rtl .fc-basic-view .fc-day-number {\n  text-align: left; }\n\n:host /deep/ ba-full-calendar .fc-day-number.fc-other-month {\n  opacity: 0.3; }\n\n:host /deep/ ba-full-calendar .fc-agenda-view .fc-day-grid {\n  position: relative;\n  z-index: 2; }\n\n:host /deep/ ba-full-calendar .fc-agenda-view .fc-day-grid .fc-row {\n  min-height: 3em; }\n\n:host /deep/ ba-full-calendar .fc-agenda-view .fc-day-grid .fc-row .fc-content-skeleton {\n  padding-top: 1px;\n  padding-bottom: 1em; }\n\n:host /deep/ ba-full-calendar .fc-ltr .fc-axis {\n  text-align: right; }\n\n:host /deep/ ba-full-calendar .fc-rtl .fc-axis {\n  text-align: left; }\n\n:host /deep/ ba-full-calendar .ui-widget td.fc-axis {\n  font-weight: 400; }\n\n:host /deep/ ba-full-calendar .fc-time-grid-container,\n:host /deep/ ba-full-calendar .fc-time-grid {\n  position: relative;\n  z-index: 1; }\n\n:host /deep/ ba-full-calendar .fc-time-grid {\n  min-height: 100%; }\n\n:host /deep/ ba-full-calendar .fc-time-grid table {\n  border: 0 hidden transparent; }\n\n:host /deep/ ba-full-calendar .fc-time-grid > .fc-bg {\n  z-index: 1; }\n\n:host /deep/ ba-full-calendar .fc-time-grid .fc-slats,\n:host /deep/ ba-full-calendar .fc-time-grid > hr {\n  position: relative;\n  z-index: 2; }\n\n:host /deep/ ba-full-calendar .fc-time-grid .fc-highlight-skeleton {\n  z-index: 3; }\n\n:host /deep/ ba-full-calendar .fc-time-grid .fc-content-skeleton {\n  position: absolute;\n  z-index: 4;\n  top: 0;\n  left: 0;\n  right: 0; }\n\n:host /deep/ ba-full-calendar .fc-time-grid > .fc-helper-skeleton {\n  z-index: 5; }\n\n:host /deep/ ba-full-calendar .fc-slats td {\n  height: 1.5em;\n  border-bottom: 0; }\n\n:host /deep/ ba-full-calendar .fc-slats .fc-minor td {\n  border-top-style: dotted; }\n\n:host /deep/ ba-full-calendar .fc-slats .ui-widget-content {\n  background: none; }\n\n:host /deep/ ba-full-calendar .fc-time-grid .fc-highlight-container {\n  position: relative; }\n\n:host /deep/ ba-full-calendar .fc-time-grid .fc-highlight {\n  position: absolute;\n  left: 0;\n  right: 0; }\n\n:host /deep/ ba-full-calendar .fc-time-grid .fc-event-container {\n  position: relative; }\n\n:host /deep/ ba-full-calendar .fc-ltr .fc-time-grid .fc-event-container {\n  margin: 0 2.5% 0 2px; }\n\n:host /deep/ ba-full-calendar .fc-rtl .fc-time-grid .fc-event-container {\n  margin: 0 2px 0 2.5%; }\n\n:host /deep/ ba-full-calendar .fc-time-grid .fc-event {\n  position: absolute;\n  z-index: 1; }\n\n:host /deep/ ba-full-calendar .fc-time-grid-event {\n  overflow: hidden; }\n  :host /deep/ ba-full-calendar .fc-time-grid-event.fc-not-start {\n    border-top-width: 0;\n    padding-top: 1px;\n    border-top-left-radius: 0;\n    border-top-right-radius: 0; }\n  :host /deep/ ba-full-calendar .fc-time-grid-event.fc-not-end {\n    border-bottom-width: 0;\n    padding-bottom: 1px;\n    border-bottom-left-radius: 0;\n    border-bottom-right-radius: 0; }\n  :host /deep/ ba-full-calendar .fc-time-grid-event > .fc-content {\n    position: relative;\n    z-index: 2; }\n  :host /deep/ ba-full-calendar .fc-time-grid-event .fc-title {\n    padding: 0 1px; }\n  :host /deep/ ba-full-calendar .fc-time-grid-event .fc-time {\n    padding: 0 1px;\n    font-size: .85em;\n    white-space: nowrap; }\n  :host /deep/ ba-full-calendar .fc-time-grid-event .fc-bg {\n    z-index: 1;\n    background: #ffffff;\n    opacity: .25;\n    filter: alpha(opacity=25); }\n  :host /deep/ ba-full-calendar .fc-time-grid-event.fc-short .fc-content {\n    white-space: nowrap; }\n  :host /deep/ ba-full-calendar .fc-time-grid-event.fc-short .fc-time {\n    display: inline-block;\n    vertical-align: top; }\n    :host /deep/ ba-full-calendar .fc-time-grid-event.fc-short .fc-time span {\n      display: none; }\n    :host /deep/ ba-full-calendar .fc-time-grid-event.fc-short .fc-time:before {\n      content: attr(data-start); }\n    :host /deep/ ba-full-calendar .fc-time-grid-event.fc-short .fc-time:after {\n      content: \"\\A0-\\A0\"; }\n  :host /deep/ ba-full-calendar .fc-time-grid-event.fc-short .fc-title {\n    display: inline-block;\n    vertical-align: top;\n    font-size: .85em;\n    padding: 0; }\n  :host /deep/ ba-full-calendar .fc-time-grid-event .fc-resizer {\n    position: absolute;\n    z-index: 3;\n    left: 0;\n    right: 0;\n    bottom: 0;\n    height: 8px;\n    overflow: hidden;\n    line-height: 8px;\n    font-size: 11px;\n    font-family: monospace;\n    text-align: center;\n    cursor: s-resize; }\n    :host /deep/ ba-full-calendar .fc-time-grid-event .fc-resizer:after {\n      content: \"=\"; }\n\n:host /deep/ ba-full-calendar .fc-day-grid-container.fc-scroller {\n  height: auto !important; }\n\n:host /deep/ ba-full-calendar .fc-body > tr > .fc-widget-content {\n  border: none; }\n\n:host /deep/ ba-full-calendar .fc-head {\n  color: #ffffff;\n  background-color: #00abff; }\n  :host /deep/ ba-full-calendar .fc-head td, :host /deep/ ba-full-calendar .fc-head th {\n    border: none; }\n  :host /deep/ ba-full-calendar .fc-head div.fc-widget-header {\n    padding: 5px 0; }\n\n:host /deep/ ba-full-calendar .fc-today-button, :host /deep/ ba-full-calendar .fc-month-button, :host /deep/ ba-full-calendar .fc-agendaWeek-button, :host /deep/ ba-full-calendar .fc-agendaDay-button {\n  display: none; }\n\n:host /deep/ ba-full-calendar .blurCalendar {\n  margin-top: 15px; }\n\n:host /deep/ ba-full-calendar .fc-prev-button, :host /deep/ ba-full-calendar .fc-next-button {\n  position: absolute;\n  background: transparent;\n  box-shadow: none;\n  border: none;\n  color: #ffffff; }\n\n:host /deep/ ba-full-calendar .fc-next-button {\n  left: 30px; }\n\n:host /deep/ ba-full-calendar .fc-day-number {\n  color: #ffffff;\n  opacity: 0.9; }\n\n/deep/.calendar-panel.card .card-body {\n  padding: 0; }\n", ""]);

// exports


/*** EXPORTS FROM exports-loader ***/
module.exports = module.exports.toString();

/***/ }),

/***/ 1489:
/***/ (function(module, exports, __webpack_require__) {

exports = module.exports = __webpack_require__(4)(false);
// imports


// module
exports.push([module.i, ".blue {\n  color: blue; }\n\n.green {\n  color: green; }\n\n:host /deep/ .pie-charts {\n  color: #ffffff; }\n  :host /deep/ .pie-charts .pie-chart-item-container {\n    position: relative;\n    padding: 0 15px;\n    float: left;\n    box-sizing: border-box; }\n    :host /deep/ .pie-charts .pie-chart-item-container .card {\n      height: 114px; }\n  @media screen and (min-width: 1325px) {\n    :host /deep/ .pie-charts .pie-chart-item-container {\n      max-width: 25%;\n      -ms-flex: 0 0 25%;\n          flex: 0 0 25%; } }\n  @media screen and (min-width: 700px) and (max-width: 1325px) {\n    :host /deep/ .pie-charts .pie-chart-item-container {\n      max-width: 50%;\n      -ms-flex: 0 0 50%;\n          flex: 0 0 50%; } }\n  @media screen and (max-width: 700px) {\n    :host /deep/ .pie-charts .pie-chart-item-container {\n      max-width: 100%;\n      -ms-flex: 0 0 100%;\n          flex: 0 0 100%; } }\n  :host /deep/ .pie-charts .pie-chart-item {\n    position: relative; }\n    :host /deep/ .pie-charts .pie-chart-item .chart-icon {\n      position: absolute;\n      right: 0;\n      top: 3px; }\n  @media screen and (min-width: 1325px) and (max-width: 1650px), (min-width: 700px) and (max-width: 830px), (max-width: 400px) {\n    :host /deep/ .pie-charts .chart-icon {\n      display: none; } }\n  :host /deep/ .pie-charts .chart {\n    position: relative;\n    display: inline-block;\n    width: 84px;\n    height: 84px;\n    text-align: center;\n    float: left; }\n  :host /deep/ .pie-charts .chart canvas {\n    position: absolute;\n    top: 0;\n    left: 0; }\n  :host /deep/ .pie-charts .percent {\n    display: inline-block;\n    line-height: 84px;\n    z-index: 2;\n    font-size: 16px; }\n  :host /deep/ .pie-charts .percent:after {\n    content: '%';\n    margin-left: 0.1em;\n    font-size: .8em; }\n  :host /deep/ .pie-charts .description {\n    display: inline-block;\n    padding: 20px 0 0 20px;\n    font-size: 18px;\n    opacity: 0.9; }\n    :host /deep/ .pie-charts .description .description-stats {\n      padding-top: 8px;\n      font-size: 24px; }\n  :host /deep/ .pie-charts .angular {\n    margin-top: 100px; }\n  :host /deep/ .pie-charts .angular .chart {\n    margin-top: 0; }\n\n@media screen and (min-width: 1620px) {\n  .row.shift-up > * {\n    margin-top: -573px; } }\n\n@media screen and (max-width: 1620px) {\n  .card.feed-panel.large-card {\n    height: 824px; } }\n\n.user-stats-card .card-title {\n  padding: 0 0 15px; }\n\n.blurCalendar {\n  height: 475px; }\n", ""]);

// exports


/*** EXPORTS FROM exports-loader ***/
module.exports = module.exports.toString();

/***/ }),

/***/ 1573:
/***/ (function(module, exports) {

module.exports = "<ba-full-calendar [baFullCalendarConfiguration]=\"calendarConfiguration\" baFullCalendarClass=\"blurCalendar\" (onCalendarReady)=\"onCalendarReady($event)\"></ba-full-calendar>\n"

/***/ }),

/***/ 1574:
/***/ (function(module, exports) {

module.exports = "<div class=\"row\">\n  <img *ngIf=\"!isLoadData\" style=\" position: absolute;margin: auto;top: 0;left: 0;right: 0;bottom: 0;\" src=\"/assets/images/loading.gif\"\n  />\n  <div class=\"col-xl-12 col-lg-12 col-md-12 col-sm-12 col-12\" *ngIf=\"isAdmin && isLoadData\">\n    <div class=\"row pie-charts\">\n\n      <ba-card class=\"pie-chart-item-container col-xlg-3 col-lg-3 col-md-6 col-sm-12 col-12\">\n    \n        <div class=\"pie-chart-item\">\n          <div class=\"description\">\n            <div translate>Total Lecturers</div>\n            <div class=\"description-stats\">{{totalLecturers}}</div>\n          </div>\n        </div>    \n      </ba-card>\n\n\n      <ba-card class=\"pie-chart-item-container col-xlg-3 col-lg-3 col-md-6 col-sm-12 col-12\">   \n        <div class=\"pie-chart-item\">\n          <div class=\"description\">\n            <div translate>Total Students</div>\n            <div class=\"description-stats\">{{totalStudents}}</div>\n          </div>\n        </div>\n      </ba-card>\n\n      <ba-card class=\"pie-chart-item-container col-xlg-3 col-lg-3 col-md-6 col-sm-12 col-12\">   \n        <div class=\"pie-chart-item\">\n          <div class=\"description\">\n            <div translate>Total Projects</div>\n            <div class=\"description-stats\">{{totalProjects}}</div>\n          </div>\n        </div>\n      </ba-card>\n    </div>\n\n  </div>\n\n\n  <div class=\"widgets\" *ngIf=\"isLecturer && isLoadData\">\n    <div class=\"row\">\n      <div *ngIf=\"isLecturer\" class=\"col-md-6 col-sm-6 col-xs-12\">\n            <h2>\n              <a href=\"main/group\" alt=\"View all\">My Group is pending</a>\n            </h2>\n          <div >\n            <table class=\"table table-striped\">\n              <thead>\n                <tr>\n                  <th>#</th>\n                  <th>Group Name</th>\n                  <th>Project</th>\n                  <th>Supervisor</th>\n                  <th>Status</th>\n                </tr>\n              </thead>\n              <tbody>\n                <tr *ngFor=\"let group of groups.items; let rowIndex = index\">\n                  <th scope=\"row\">{{rowIndex+1}}</th>\n                  <td>{{group.groupName}}</td>\n                  <td>{{group.project.title}}</td>\n                  <td>{{group.lecturer.name}}</td>\n                  <td [ngClass]=\"group.isConfirm == 'Pending' ? 'blue': 'green'\">\n                    <b>{{group.isConfirm}}</b>\n                  </td>\n                </tr>\n              </tbody>\n            </table>\n          </div>\n      </div>\n\n      <div *ngIf=\"isLecturer\" class=\"col-md-6 col-sm-6 col-xs-12\">\n        <h2>\n          <a href=\"main/confirm-group\" alt=\"View all\">My Group is Accepted</a>\n        </h2>\n      <div >\n        <table class=\"table table-striped\">\n          <thead>\n            <tr>\n              <th>#</th>\n              <th>Group Name</th>\n              <th>Project</th>\n              <th>Supervisor</th>\n              <th>Status</th>\n            </tr>\n          </thead>\n          <tbody>\n            <tr *ngFor=\"let group of groupsAccepted.items; let rowIndex = index\">\n              <th scope=\"row\">{{rowIndex+1}}</th>\n              <td>{{group.groupName}}</td>\n              <td>{{group.project.title}}</td>\n              <td>{{group.lecturer.name}}</td>\n              <td [ngClass]=\"group.isConfirm == 'Pending' ? 'blue': 'green'\">\n                <b>{{group.isConfirm}}</b>\n              </td>\n            </tr>\n          </tbody>\n        </table>\n      </div>\n      </div>\n    </div>\n  </div>\n\n\n  <div class=\"widgets\" *ngIf=\"isStudent && isLoadData\">\n    <div class=\"row\">\n      <div *ngIf=\"isStudent\" class=\"col-md-12 col-sm-12 col-xs-12\">\n            <h2>\n              <a href=\"main/confirm-group\"  alt=\"View all\">My Group</a>\n            </h2>\n          <div >\n            <table class=\"table table-striped\">\n              <thead>\n                <tr>\n                  <th>#</th>\n                  <th>Group Name</th>\n                  <th>Project</th>\n                  <th>Supervisor</th>\n                  <th>Status</th>\n                </tr>\n              </thead>\n              <tbody>\n                <tr *ngFor=\"let group of groupsStudent.items; let rowIndex = index\">\n                  <th scope=\"row\">{{rowIndex+1}}</th>\n                  <td>{{group.groupName}}</td>\n                  <td>{{group.project.title}}</td>\n                  <td>{{group.lecturer.name}}</td>\n                  <td [ngClass]=\"group.isConfirm == 'Pending' ? 'blue': 'green'\">\n                    <b>{{group.isConfirm}}</b>\n                  </td>\n                </tr>\n              </tbody>\n            </table>\n          </div>\n      </div>\n    </div>\n  </div>\n  \n</div>\n"

/***/ }),

/***/ 827:
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
Object.defineProperty(__webpack_exports__, "__esModule", { value: true });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__(0);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__angular_common__ = __webpack_require__(13);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__angular_forms__ = __webpack_require__(22);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_3__app_translation_module__ = __webpack_require__(207);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_4__theme_nga_module__ = __webpack_require__(202);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_5__dashboard_component__ = __webpack_require__(1280);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_6__dashboard_routing__ = __webpack_require__(1355);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_7__calendar__ = __webpack_require__(1354);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_8__calendar_calendar_service__ = __webpack_require__(1279);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_9__core_services_notification_service__ = __webpack_require__(848);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_10__core_services_data_service__ = __webpack_require__(853);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "DashboardModule", function() { return DashboardModule; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};











var DashboardModule = (function () {
    function DashboardModule() {
    }
    return DashboardModule;
}());
DashboardModule = __decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["NgModule"])({
        imports: [
            __WEBPACK_IMPORTED_MODULE_1__angular_common__["CommonModule"],
            __WEBPACK_IMPORTED_MODULE_2__angular_forms__["FormsModule"],
            __WEBPACK_IMPORTED_MODULE_3__app_translation_module__["a" /* AppTranslationModule */],
            __WEBPACK_IMPORTED_MODULE_4__theme_nga_module__["a" /* NgaModule */],
            __WEBPACK_IMPORTED_MODULE_6__dashboard_routing__["a" /* routing */]
        ],
        declarations: [
            __WEBPACK_IMPORTED_MODULE_7__calendar__["a" /* Calendar */],
            __WEBPACK_IMPORTED_MODULE_5__dashboard_component__["a" /* Dashboard */]
        ],
        providers: [
            __WEBPACK_IMPORTED_MODULE_8__calendar_calendar_service__["a" /* CalendarService */],
            __WEBPACK_IMPORTED_MODULE_10__core_services_data_service__["a" /* DataService */], __WEBPACK_IMPORTED_MODULE_9__core_services_notification_service__["a" /* NotificationService */]
        ]
    })
], DashboardModule);

//# sourceMappingURL=dashboard.module.js.map

/***/ }),

/***/ 848:
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__(0);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return NotificationService; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};

var NotificationService = (function () {
    function NotificationService() {
        this._notifier = alertify;
        alertify.defaults = {
            // dialogs defaults
            autoReset: true,
            basic: false,
            closable: true,
            closableByDimmer: true,
            frameless: false,
            maintainFocus: true,
            maximizable: true,
            modal: true,
            movable: true,
            moveBounded: false,
            overflow: true,
            padding: true,
            pinnable: true,
            pinned: true,
            preventBodyShift: false,
            resizable: true,
            startMaximized: false,
            transition: 'pulse',
            // notifier defaults
            notifier: {
                // auto-dismiss wait time (in seconds)  
                delay: 5,
                // default position
                position: 'top-right',
                // adds a close button to notifier messages
                closeButton: false
            },
            // language resources 
            glossary: {
                // dialogs default title
                title: 'Confirm',
                // ok button text
                ok: 'Yes',
                // cancel button text
                cancel: 'Cancel'
            },
            // theme settings
            theme: {
                // class name attached to prompt dialog input textbox.
                input: 'ajs-input',
                // class name attached to ok button
                ok: 'ajs-ok',
                // class name attached to cancel button 
                cancel: 'ajs-cancel'
            }
        };
    }
    NotificationService.prototype.printSuccessMessage = function (message) {
        this._notifier.success(message);
    };
    NotificationService.prototype.printErrorMessage = function (message) {
        this._notifier.error(message);
    };
    NotificationService.prototype.printConfirmationDialog = function (message, okCallback) {
        this._notifier.confirm(message, function (e) {
            if (e) {
                okCallback();
            }
            else {
            }
        });
    };
    return NotificationService;
}());
NotificationService = __decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["Injectable"])(),
    __metadata("design:paramtypes", [])
], NotificationService);

//# sourceMappingURL=notification.service.js.map

/***/ }),

/***/ 853:
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0_rxjs_Observable__ = __webpack_require__(6);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0_rxjs_Observable___default = __webpack_require__.n(__WEBPACK_IMPORTED_MODULE_0_rxjs_Observable__);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__utility_service__ = __webpack_require__(146);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__notification_service__ = __webpack_require__(848);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_3__common_system_constants__ = __webpack_require__(33);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_4__authen_service__ = __webpack_require__(80);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_5__angular_http__ = __webpack_require__(66);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_6__angular_router__ = __webpack_require__(20);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_7__angular_core__ = __webpack_require__(0);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return DataService; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};








var DataService = (function () {
    function DataService(_http, _router, _authService, _notificationService, _utilityService) {
        this._http = _http;
        this._router = _router;
        this._authService = _authService;
        this._notificationService = _notificationService;
        this._utilityService = _utilityService;
        this.headers = new __WEBPACK_IMPORTED_MODULE_5__angular_http__["b" /* Headers */]();
        this.headers.append('Content-Type', 'application/json');
    }
    DataService.prototype.get = function (url) {
        this.headers.delete("Authorization");
        this.headers.append("Authorization", "Bearer " + this._authService.getLoggedInUser().access_token);
        return this._http.get(__WEBPACK_IMPORTED_MODULE_3__common_system_constants__["a" /* SystemConstants */].BASE_URL + url, { headers: this.headers })
            .map(this.extractData);
    };
    DataService.prototype.getGithub = function (url) {
        return this._http.get(__WEBPACK_IMPORTED_MODULE_3__common_system_constants__["a" /* SystemConstants */].GITHUB_API_URL + url)
            .map(this.extractData);
    };
    DataService.prototype.post = function (url, data) {
        this.headers.delete("Authorization");
        this.headers.append("Authorization", "Bearer " + this._authService.getLoggedInUser().access_token);
        return this._http.post(__WEBPACK_IMPORTED_MODULE_3__common_system_constants__["a" /* SystemConstants */].BASE_URL + url, data, { headers: this.headers })
            .map(this.extractData);
    };
    DataService.prototype.put = function (url, data) {
        this.headers.delete("Authorization");
        this.headers.append("Authorization", "Bearer " + this._authService.getLoggedInUser().access_token);
        return this._http.put(__WEBPACK_IMPORTED_MODULE_3__common_system_constants__["a" /* SystemConstants */].BASE_URL + url, data, { headers: this.headers })
            .map(this.extractData);
    };
    DataService.prototype.delete = function (url) {
        this.headers.delete("Authorization");
        this.headers.append("Authorization", "Bearer " + this._authService.getLoggedInUser().access_token);
        return this._http.delete(__WEBPACK_IMPORTED_MODULE_3__common_system_constants__["a" /* SystemConstants */].BASE_URL + url, { headers: this.headers });
    };
    DataService.prototype.upload = function (url, file) {
        this.headers.delete("Authorization");
        this.headers.append("Authorization", "Bearer " + this._authService.getLoggedInUser().access_token);
        var formData = new FormData();
        formData.append('file', file);
        console.log(formData.get("file"));
        return this._http.post(__WEBPACK_IMPORTED_MODULE_3__common_system_constants__["a" /* SystemConstants */].BASE_URL + url, formData)
            .map(function (res) { return res.json(); });
    };
    DataService.prototype.extractData = function (res) {
        var body = res.json();
        return body || {};
    };
    DataService.prototype.handleError = function (error) {
        if (error.status == 401) {
            localStorage.removeItem(__WEBPACK_IMPORTED_MODULE_3__common_system_constants__["a" /* SystemConstants */].CURRENT_USER);
            this._notificationService.printErrorMessage("Error");
            this._utilityService.navigateToLogin();
        }
        else {
            var errMsg = (error.message) ? error.message : error.status ? "" + error.statusText : "Error system";
            this._notificationService.printErrorMessage(errMsg);
            return __WEBPACK_IMPORTED_MODULE_0_rxjs_Observable__["Observable"].throw(errMsg);
        }
    };
    return DataService;
}());
DataService = __decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_7__angular_core__["Injectable"])(),
    __metadata("design:paramtypes", [typeof (_a = typeof __WEBPACK_IMPORTED_MODULE_5__angular_http__["c" /* Http */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_5__angular_http__["c" /* Http */]) === "function" && _a || Object, typeof (_b = typeof __WEBPACK_IMPORTED_MODULE_6__angular_router__["b" /* Router */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_6__angular_router__["b" /* Router */]) === "function" && _b || Object, typeof (_c = typeof __WEBPACK_IMPORTED_MODULE_4__authen_service__["a" /* AuthenService */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_4__authen_service__["a" /* AuthenService */]) === "function" && _c || Object, typeof (_d = typeof __WEBPACK_IMPORTED_MODULE_2__notification_service__["a" /* NotificationService */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_2__notification_service__["a" /* NotificationService */]) === "function" && _d || Object, typeof (_e = typeof __WEBPACK_IMPORTED_MODULE_1__utility_service__["a" /* UtilityService */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_1__utility_service__["a" /* UtilityService */]) === "function" && _e || Object])
], DataService);

var _a, _b, _c, _d, _e;
//# sourceMappingURL=data.service.js.map

/***/ }),

/***/ 875:
/***/ (function(module, exports, __webpack_require__) {

"use strict";

var Observable_1 = __webpack_require__(6);
var forkJoin_1 = __webpack_require__(466);
Observable_1.Observable.forkJoin = forkJoin_1.forkJoin;
//# sourceMappingURL=forkJoin.js.map

/***/ })

});
//# sourceMappingURL=16.chunk.js.map