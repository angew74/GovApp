import 'es6-promise/auto';
import 'regenerator-runtime/runtime';
import Vue from "vue";
import Vuex from 'vuex';
import VeeValidate from "vee-validate";
import LiquorTree from 'liquor-tree';
import PrettyCheckbox from 'pretty-checkbox-vue';
import VueFormWizard from 'vue-form-wizard';
import VueCardStack from "vue-card-stack";
import 'vue-loaders/dist/vue-loaders.css';
import VueLoaders from 'vue-loaders';
import Tabs from 'vue-tabs-with-active-line';
import VueScriptComponent from 'vue-script-component';
import 'v-autocomplete/dist/v-autocomplete.css';
import VueAvatar from '@lossendae/vue-avatar';
import VueBootstrapTypeahead from 'vue-bootstrap-typeahead';
import {
    ValidationObserver,
    ValidationProvider,
    extend,
    localize
} from "vee-validate";
import it from "vee-validate/dist/locale/it.json";
import * as rules from "vee-validate/dist/rules";
import 'bootstrap-vue/dist/bootstrap-vue.css';
import 'vue-form-wizard/dist/vue-form-wizard.min.css';
import 'bootstrap/dist/css/bootstrap.css';
import 'bootstrap-vue/dist/bootstrap-vue-icons.min.css';
import 'pretty-checkbox/dist/pretty-checkbox.min.css';
import VueSweetalert2 from 'vue-sweetalert2';
import 'pretty-checkbox-vue/dist/pretty-checkbox-vue.min.js';
import { BCard, BCardBody, BCardFooter, BCardHeader, BCardImg, BTable, BCardGroup, BNavbar, BNav, BIcon, BCollapse } from 'bootstrap-vue';
import { BootstrapVue, BootstrapVueIcons } from 'bootstrap-vue';
import 'vue-awesome/icons';
Vue.component('vue-bootstrap-typeahead', VueBootstrapTypeahead);
import Icon from 'vue-awesome/components/Icon';
import { library } from '@fortawesome/fontawesome-svg-core';
import { faUserSecret } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome';
import { faFacebook, faTwitter, faMedium, faYoutube, faGithub, faWindows, faMicrosoft, faBlogger, faVuejs } from '@fortawesome/free-brands-svg-icons';
// Enable the FontAwesomeIcon component globally
library.add(faUserSecret);
library.add(faMedium);
library.add(faYoutube);
library.add(faGithub);
library.add(faWindows);
library.add(faMicrosoft);
library.add(faBlogger);
library.add(faFacebook);
library.add(faTwitter);
library.add(faVuejs);
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
Vue.component('b-icon', BIcon);
Vue.component('v-icon', Icon);
Vue.component('vue-avatar', VueAvatar);
Vue.use(VueLoaders);
Vue.use(VueScriptComponent);
Vue.use(VueSweetalert2);
Vue.use(Tabs);
Vue.use(Vuex);
Vue.use(VueCardStack);
Object.keys(rules).forEach(rule => {
    extend(rule, rules[rule]);
});
localize("it", it);
Vue.component("ValidationObserver", ValidationObserver);
Vue.component("ValidationProvider", ValidationProvider);
Vue.use(LiquorTree);
Vue.use(PrettyCheckbox);
Vue.use(VueFormWizard);
Vue.use(VueBootstrapTypeahead);




   
