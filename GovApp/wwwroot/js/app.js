import '../css/app.css';
import 'es6-promise/auto';
import Vue from "vue";
import Vuex from 'vuex';
import VeeValidate from "vee-validate";
import Notifications from 'vue-notification';
import VueAutosuggest from "vue-autosuggest";
import {
    ValidationObserver,
    ValidationProvider,
    extend,
    localize
} from "vee-validate";
import it from "vee-validate/dist/locale/it.json";
import * as rules from "vee-validate/dist/rules";
import 'bootstrap-vue/dist/bootstrap-vue.css';
import 'bootstrap/dist/css/bootstrap.css';
import 'bootstrap-vue/dist/bootstrap-vue-icons.min.css'
import { BCard, BCardBody, BCardFooter, BCardHeader, BCardImg, BTable, BCardGroup, BNavbar, BNav, BIcon, BCollapse } from 'bootstrap-vue';   
import { BootstrapVue, BootstrapVueIcons } from 'bootstrap-vue';
import 'vue-awesome/icons';
import Icon from 'vue-awesome/components/Icon';
import { library } from '@fortawesome/fontawesome-svg-core';
import { faFacebook, faTwitter, faMedium, faYoutube, faGithub, faWindows, faMicrosoft, faBlogger,faVuejs} from '@fortawesome/free-brands-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome';
library.add(faMedium);
library.add(faYoutube);
library.add(faGithub);
library.add(faWindows);
library.add(faMicrosoft);
library.add(faBlogger);
library.add(faFacebook);
library.add(faTwitter);
library.add(faVuejs);
// Enable the FontAwesomeIcon component globally
Vue.component('font-awesome-icon', FontAwesomeIcon);
Vue.use(BootstrapVue);
Vue.use(BootstrapVueIcons);
Vue.use(Vuex);
Vue.component('b-card', BCard);
Vue.component('b-card-group', BCardGroup);
Vue.component('b-card-body', BCardBody);
Vue.component('b-card-footer', BCardFooter);
Vue.component('b-card-header', BCardHeader);
Vue.component('b-card-img', BCardImg);
Vue.component('b-table', BTable);
Vue.component('b-nav', BNav);
Vue.component('b-navbar', BNavbar);
Vue.component('b-collapse', BCollapse);
Vue.component('v-icon', Icon);
Vue.use(Notifications);
Vue.use(Vuex);
Vue.use(VueAutosuggest);
Object.keys(rules).forEach(rule => {
    extend(rule, rules[rule]);
});
localize("it", it);
Vue.component("ValidationObserver", ValidationObserver);
Vue.component("ValidationProvider", ValidationProvider);
import './validators';


   
