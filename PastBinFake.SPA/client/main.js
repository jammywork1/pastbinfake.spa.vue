var Vue = require('vue');
var VueRouter = require('vue-router');
var axios = require('axios');
var lodash = require('lodash');
var moment = require('moment');
require("moment/min/locales.min");
moment.locale('ru');
window._ = lodash;
window.axios = axios;
window.moment = moment;


Vue.use(VueRouter);

var create = require("./create.vue");
var home = require("./home.vue");


var router = new VueRouter({
    mode: 'history',
    routes: [
        { path: '/', component: home },
        { path: '/notes/create', component: create },
        { path: '/:shortUrl', component: require("./notebyurl.vue") }
    ]
});

var baserouter = require("./baserouter.vue");

new Vue({
    router: router,
    render: function (createElement) {
        return createElement(baserouter);
    }
}).$mount("#app");