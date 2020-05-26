import Vue from 'vue';
import store from "../../wwwroot/js/store/store";
import changepassword from '../../wwwroot/js/page/account/changepassword';
new Vue({
    store,
    render: h => h(changepassword)
}).$mount("#app");