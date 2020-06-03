import Vue from "vue";
import store from "../../wwwroot/js/store/store";
import account from '../../wwwroot/js/page/account/account';

 
new Vue({
    store,
    render: h => h(account)
}).$mount("#app");