import Vue from 'vue';
import axios from 'axios';

const store = {
    namespaced: true,
    state: {
        profile: {},
        message: '',
        url: ''      
    },
    getters: {
        isAuthenticated: state => (state.profile.name !== null),
        isMessage: state => state.message !== '',
        Message: state => state.message,
        Url: state => state.url,
        isUrl: state => state.url !== ''
      
    },
    mutations: {
        setProfile(state, profile) {
            state.profile = profile
        },
        setMessage(state, message) {
            state.message = message
        },
        setUrl(state, url) {
            state.url = url;
        }       
    },
    actions: {
        login({ commit }, credentials) {
            return axios.post('/api/auth/login', credentials
                ,{
                    headers: {
                        'Content-Type': 'application/json'
                    }
                }).then(res => {
                    if (res.status === 200) { commit('setProfile', res.data); }
                    else { commit('setProfile', res.data), commit('message', res.message); }
                }).catch((error) => {
                    if (error.response.data !== null) {
                        commit('setMessage', error.response.data.errMsg);
                        if (error.response.data.url !== null) {
                            commit('setUrl', error.response.data.url);
                        }
                    }
                    else {
                        commit('setMessage', error.response.statusText);
                    }
                    console.log('Something went wrong');
                });
        },
        logout({ commit }) {
            return axios.post('/account/logout').then(() => {
                commit('setProfile', {})
            })
        },        
        authconfirm({ commit }, confirm) {
            return axios.post('/api/auth/Confirmation', confirm
                , {
                    headers: {
                        'Content-Type': 'application/json'
                    }
                }).then(res => {
                    if (res.status === 200 && res.data.confirm.result === true) { commit('setUrl', res.data.confirm.url); }
                    else {commit('message', res.message); }
                }).catch((error) => {
                    if (error.response.data !== null) {
                        commit('setMessage', error.response.data.errMsg);
                        if (error.response.data.url !== null) {
                            commit('setUrl', error.response.data.url);
                        }
                    }
                    else {
                        commit('setMessage', error.response.statusText);
                    }
                    console.log('Something went wrong');
                });
        },
        restoreContext({ commit }) {
            return axios.get('/api/auth/context').then(res => {
                commit('setProfile', res.data)
            })
        }
    }
}
export default store;