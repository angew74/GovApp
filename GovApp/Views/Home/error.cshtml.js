import Vue from 'vue';
import store from "../../wwwroot/js/store/store";
import error from '../../wwwroot/js/page/home/error';
new Vue({
    store,
    render: h => h(error)
}).$mount("#app");