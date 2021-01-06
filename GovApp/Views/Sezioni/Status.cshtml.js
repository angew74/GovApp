import Vue from "vue";
import store from "../../wwwroot/js/store/store";
import status from '../../wwwroot/js/page/Sezioni/status';

new Vue({
    store,
    render: h => h(status)
}).$mount("#app");