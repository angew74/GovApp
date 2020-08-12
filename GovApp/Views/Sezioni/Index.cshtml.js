import Vue from "vue";
import store from "../../wwwroot/js/store/store";
import sezioni from '../../wwwroot/js/page/Sezioni/sezioni';

new Vue({
    store,
    render: h => h(sezioni)
}).$mount("#app");