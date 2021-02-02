﻿import Vue from 'vue';
import store from "../../wwwroot/js/store/store";
import liste from '../../wwwroot/js/page/interrogazioni/liste';
new Vue({
    store,
    render: h => h(liste)
}).$mount("#app");