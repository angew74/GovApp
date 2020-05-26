import Vue from "vue";
import store from "../../wwwroot/js/store/store";
import rights from '../../wwwroot/js/page/Rights/rights';

new Vue({
    store,
    render: h => h(rights)
}).$mount("#app");