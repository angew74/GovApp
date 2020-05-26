import Vue from 'vue';
import store from "../../wwwroot/js/store/store";
import confirmemailconfirmation from '../../wwwroot/js/page/account/confirmemailconfirmation';
new Vue({
    store,
    render: h => h(confirmemailconfirmation)
}).$mount("#app");