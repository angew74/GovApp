import Vue from 'vue';
import store from "../../wwwroot/js/store/store";
import logout from '../../wwwroot/js/page/account/logout';
new Vue({
    store,
    render: h => h(logout)
}).$mount("#app");