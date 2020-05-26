import Vue from 'vue';
import store from "../../wwwroot/js/store/store";
import passwordconfirm from '../../wwwroot/js/page/account/passwordconfirm';
new Vue({
    store,
    render: h => h(passwordconfirm)
}).$mount("#app");