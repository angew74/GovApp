import Vue from "vue";
import store from "../../wwwroot/js/store/store";
import coalizioni from '../../wwwroot/js/page/coalizioni/inserimento';

new Vue({
    store,
    render: h => h(coalizioni)
}).$mount("#app");