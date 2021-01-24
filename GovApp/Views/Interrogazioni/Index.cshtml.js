import Vue from 'vue';
import store from "../../wwwroot/js/store/store";
import indice from '../../wwwroot/js/page/interrogazioni/indice';
new Vue({
    store,
    render: h => h(indice)
}).$mount("#app");