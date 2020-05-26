import Vue from 'vue';
import store from "../../wwwroot/js/store/store";
import confirmemail from '../../wwwroot/js/page/account/confirmemail';
new Vue({
    store,
    render: h => h(confirmemail)
}).$mount("#app");