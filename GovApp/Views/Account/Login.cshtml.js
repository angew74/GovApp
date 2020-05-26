import Vue from 'vue';
import store from "../../wwwroot/js/store/store";
import login from '../../wwwroot/js/page/account/login';
new Vue({
    store,
    render: h => h(login)
}).$mount("#app");