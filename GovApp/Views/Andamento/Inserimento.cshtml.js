import Vue from "vue";
import store from "../../wwwroot/js/store/store";
import andamento from '../../wwwroot/js/page/andamento/inserimento';

new Vue({
    store,
    render: h => h(andamento)
}).$mount("#app");