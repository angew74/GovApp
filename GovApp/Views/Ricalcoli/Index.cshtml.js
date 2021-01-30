import Vue from 'vue';
import store from "../../wwwroot/js/store/store";
import indice from '../../wwwroot/js/page/ricalcoli/indice';
new Vue({
    store,
    render: h => h(indice)
}).$mount("#app");