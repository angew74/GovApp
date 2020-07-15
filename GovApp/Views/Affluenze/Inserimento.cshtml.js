import Vue from "vue";
import store from "../../wwwroot/js/store/store";
import premier from '../../wwwroot/js/page/affluenze/inserimento';

new Vue({
    store,
    render: h => h(premier)
}).$mount("#app");