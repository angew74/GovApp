import Vue from 'vue';
import store from "../../wwwroot/js/store/store";
import myprofile from '../../wwwroot/js/page/account/myprofile';
new Vue({
    store,
    render: h => h(myprofile)
}).$mount("#app");