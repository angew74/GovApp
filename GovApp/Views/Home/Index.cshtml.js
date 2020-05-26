import Vue from 'vue';
import store from "../../wwwroot/js/store/store";
import home from '../../wwwroot/js/page/home/home';
new Vue({
    store,
    render: h => h(home)
}).$mount("#app");