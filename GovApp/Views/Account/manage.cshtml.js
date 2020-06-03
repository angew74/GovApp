import Vue from 'vue';
import store from "../../wwwroot/js/store/store";
import manage from '../../wwwroot/js/page/account/manage';
new Vue({
    store,
    render: h => h(manage)
}).$mount("#app");