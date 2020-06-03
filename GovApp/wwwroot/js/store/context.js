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
        },
        centralizeMessage(state, error) {
            if (error.response.data !== null && typeof (error.response.data.errMsg) !== 'undefined' && error.response.data.errMsg !== null) {
                //  commit('setMessage', error.response.data.errMsg);
                state.message = error.response.data.errMsg;
            }
            else if (error.response.data !== null && typeof (error.response.data.url) !== 'undefined' && error.response.data.url !== null) {              
                state.url = error.response.data.url;
               // commit('setUrl', error.response.data.url);
            }
            else if (error.response.statusText !== '') {
                // commit('setMessage', error.response.statusText);
                state.message = state, error.response.statusText;
            }
            else {
                state.message = "Attenzione errore non gestito contattare l'amministratore";
              //   commit('setMessage', );
            }
            console.log('Something went wrong');
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
                    commit('centralizeMessage', error);
                });
        },
        logout({ commit }) {
            return axios.post('/account/logout').then(() => {
                commit('setProfile', {})
            })
        },          
        authchange({ commit }, change) {
            return axios.post('/api/auth/Change', change
                , {
                    headers: {
                        'Content-Type': 'application/json'
                    }
                }).then(res => {
                    if (res.status === 200 && res.data.confirm.result === true) { commit('setUrl', res.data.confirm.url); }
                    else { commit('message', res.message); }
                }).catch((error) => {
                    commit('centralizeMessage',error);
                });
        },
        register({ commit }, user) {
            return axios.post('/api/auth/Register', user
                , {
                    headers: {
                        'Content-Type': 'application/json'
                    }
                }).then(res => {
                    if (res.status === 200 && res.data.confirm.result === true) { commit('setUrl', res.data.confirm.url); }
                    else { commit('message', res.message); }
                }).catch((error) => {
                    commit('centralizeMessage', error);
                });
        },
        authconfirm({ commit }, confirm) {
            return axios.post('/api/auth/Confirmation', confirm
                , {
                    headers: {
                        'Content-Type': 'application/json'
                    }
                }).then(res => {
                    if (res.status === 200 && res.data.confirm.result === true) { commit('setUrl', res.data.confirm.url); }
                    else { commit('message', res.message); }
                }).catch((error) => {
                    commit('centralizeMessage', error);
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