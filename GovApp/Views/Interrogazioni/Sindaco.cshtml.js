import Vue from 'vue';
import store from "../../wwwroot/js/store/store";
import sindaco from '../../wwwroot/js/page/interrogazioni/sindaco';
new Vue({
    store,
    render: h => h(sindaco)
}).$mount("#app");