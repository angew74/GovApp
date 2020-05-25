import Vue from 'vue';
import store from "../../wwwroot/js/store/store";
import home from '../../wwwroot/js/page/home';
new Vue({
    store,
    render: h => h(home)
}).$mount("#app");