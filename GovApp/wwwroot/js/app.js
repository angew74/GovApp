import $ from 'jquery';
//// Import the Bootstrap Material Design Theme
import 'bootstrap-material-design/dist/css/bootstrap-material-design.min.css';
import 'bootstrap-material-design/dist/js/bootstrap-material-design.min.js';
import 'material-design-colors/dist/material-design.min.css';
import 'bootstrap/dist/js/bootstrap.js';
import '../lib/font-awesome/all.js';
//// Import the App Styles
import '../css/app.css';
import App from "./App.vue";
import store from "./store.js";
import Vue from "vue";
Vue.config.productionTip = false;
//// Initialize the Material Design elements when the page loads
new Vue({
    store,
    render: h => h(App)
}).$mount("#app")
$(document).ready(function () {
    $('body').bootstrapMaterialDesign
    $('[data-toggle="popover"]').popover
});