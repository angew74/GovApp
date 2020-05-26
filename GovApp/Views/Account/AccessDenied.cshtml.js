import Vue from 'vue';
import store from "../../wwwroot/js/store/store";
import accessdenied from '../../wwwroot/js/page/account/accessdenied';
new Vue({
    store,
    render: h => h(accessdenied)
}).$mount("#app");