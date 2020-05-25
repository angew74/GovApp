// store/index.js

import Vue from "vue";
import Vuex from "vuex";
import context from '../store/context';

Vue.use(Vuex);

const store = new Vuex.Store({
    modules: {
        context
    }
})
export default store;