import Vue from 'vue';
import store from "../../wwwroot/js/store/store";
import register from '../../wwwroot/js/page/account/register';
new Vue({
    store,
    render: h => h(register)
}).$mount("#app");