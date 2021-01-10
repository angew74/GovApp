import Vue from 'vue';
import axios from 'axios';

const store = {
    namespaced: true,
    state: {
        profile: {},
        message: '',
        url: '',
        sezione: {},
        affluenza: {},
        voti: {},
        auto: []
    },
    getters: {
        isAuthenticated: state => ((state.profile.name !== null) && (typeof (state.profile.name) !== 'undefined')),
        isMessage: state => state.message !== '',
        User: state => ((state.profile.name !== null) && (typeof (state.profile.name) !== 'undefined')) ? state.profile.name : "",
        Message: state => state.message,
        Url: state => state.url,
        isUrl: state => state.url !== '',       
        Sezione: state => state.sezione,
        Auto: state => state.auto,
        Affluenza: state => state.affluenza,
        Voti: state => state.voti
    },
    mutations: {
        setProfile(state, profile) {
            state.profile = profile
        },
        setMessage(state, message) {
            state.message = message
        },
        setAuto(state, auto) {
            state.auto = auto
        },
        setUrl(state, url) {
            state.url = url;
        },
        setSezione(state, sezione) {
            state.sezione = sezione;
        },
        setVoti(state, voti) {
            state.voti = voti;
        },
        setAffluenza(state, affluenza) {
            state.affluenza = affluenza;
        },
        centralizeMessage(state, error) {
            if ((typeof (error.message) !== 'undefined') && (typeof (error.response.data) === 'undefined')) {
                state.message = error.message;
                return;
            }
            if (typeof (error.response.data) !== 'undefined' && (typeof (error.response.data.url) === 'undefined')) {
                state.message = error.response.data;
                return;
            }
            if (typeof (error.response.data) !== 'undefined' && (typeof (error.response.data.url) === 'undefined' || error.response.data.url === null) && typeof (error.response.data.errMsg) !== 'undefined' && error.response.data.errMsg !== null) {
                //  commit('setMessage', error.response.data.errMsg);
                state.message = error.response.data.errMsg;
                return;
            }
            else if (error.response.data !== null && typeof (error.response.data.url) !== 'undefined' && error.response.data.url !== null) {
                state.url = error.response.data.url;
                state.message = error.response.data.errMsg;
                return;
                // commit('setUrl', error.response.data.url);
            }
            else if (error.response.statusText !== '') {
                // commit('setMessage', error.response.statusText);
                state.message = state, error.response.statusText;
                return;
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
            return axios.post('/GovApp/api/auth/login', credentials
                , {
                    headers: {
                        'Content-Type': 'application/json'
                    }
                }).then(res => {
                    if (res.status === 200) { commit('setProfile', res.data); }
                    else { commit('setProfile', res.data), commit('setMessage', res.message); }
                }).catch((error) => {
                    commit('centralizeMessage', error);
                });
        },
        logout({ commit }) {
            return axios.post('/GovApp/account/logout').then(() => {
                commit('setProfile', {})
            })
        },
        authchange({ commit }, change) {
            return axios.post('/GovApp/api/auth/Change', change
                , {
                    headers: {
                        'Content-Type': 'application/json'
                    }
                }).then(res => {
                    if (res.status === 200 && res.data.change.result === true) { commit('setUrl', res.data.change.url); }
                    else { commit('setMessage', res.message); }
                }).catch((error) => {
                    commit('centralizeMessage', error);
                });
        },
        autocomplete({ commit }, user) {
            axios({
                method: 'get',
                url: '/GovApp/api/auth/userssuggestions',
                params: {
                    "username": user
                }
            })
                .then(response => {    
                    if (response.status === 200) {
                        commit('setAuto', response.data);
                    }
                })
                .catch((error) => {
                    commit('centralizeMessage', error);
                });
        },
        register({ commit }, user) {
            return axios.post('/GovApp/api/auth/Register', user
                , {
                    headers: {
                        'Content-Type': 'application/json'
                    }
                }).then(res => {
                    if (res.status === 200 && res.data.user.result === true) {
                        commit('setUrl', res.data.user.url);
                        commit('setMessage', '');
                    }
                    else { commit('setMessage', res.message); }
                }).catch((error) => {
                    commit('centralizeMessage', error);
                });
        },
        authconfirm({ commit }, confirm) {
            return axios.post('/GovApp/api/auth/Confirmation', confirm
                , {
                    headers: {
                        'Content-Type': 'application/json'
                    }
                }).then(res => {
                    if (res.status === 200 && res.data.confirm.result === true) { commit('setUrl', res.data.confirm.url); }
                    else { commit('setMessage', res.message); }
                }).catch((error) => {
                    commit('centralizeMessage', error);
                });
        },
        research({ commit }, researchsezione) {
            return axios.post('/GovApp/values/ResearchSezione', researchsezione
                , {
                    headers: { 'Content-Type': 'application/json' }
                }).then(res => {
                    if (res.status === 200 && res.data !== null) {
                        commit('setSezione', res.data);
                        commit('setMessage', '');
                    }
                    else {
                        commit('setMessage', res.message);
                        commit('setSezione', null);
                    }
                }).catch((error) => {
                    commit('centralizeMessage', error);
                });
        },
        status({ commit }, researchsezione) {
            return axios.post('/GovApp/values/StatusSezione', researchsezione
                , {
                    headers: { 'Content-Type': 'application/json' }
                }).then(res => {
                    if (res.status === 200 && res.data !== null) {
                        commit('setSezione', res.data);
                        commit('setMessage', '');
                    }
                    else {
                        commit('setMessage', res.message);
                        commit('setSezione', null);
                    }
                }).catch((error) => {
                    commit('centralizeMessage', error);
                });
        },
        restoreContext({ commit }) {
            return axios.get('/GovApp/api/auth/context').then(res => {
                commit('setProfile', res.data)
            })
        },
        insandamento({ commit }, anda) {
            return axios.post('/GovApp/values/apra', anda
                , {
                    headers: { 'Content-Type': 'application/json' }
                }).then(res => {
                    if (res.status === 200 && res.data !== null) {
                        commit('setMessage', '');
                        commit('setSezione', null);
                    }
                    else {
                        commit('setMessage', res.message);
                        commit('setSezione', null);
                    }
                }).catch((error) => {
                    commit('centralizeMessage', error);
                });
        },
        inscoalizione({ commit }, voti) {
            return axios.post('/GovApp/voti/lcom', voti
                , {
                    headers: { 'Content-Type': 'application/json' }
                }).then(res => {
                    if (res.status === 200 && res.data !== null) {
                        commit('setMessage', '');
                        commit('setVoti', null);
                    }
                    else {
                        commit('setMessage', res.message);
                        commit('setVoti', null);
                    }
                }).catch((error) => {
                    commit('centralizeMessage', error);
                });
        },
        modandamento({ commit }, anda) {
            return axios.post('/GovApp/values/rapra', anda
                , {
                    headers: { 'Content-Type': 'application/json' }
                }).then(res => {
                    if (res.status === 200 && res.data !== null) {
                        commit('setMessage', '');
                        commit('setSezione', null);
                    }
                    else {
                        commit('setMessage', res.message);
                        commit('setSezione', null);
                    }
                }).catch((error) => {
                    commit('centralizeMessage', error);
                });
        },
        researchAffluenza({ commit }, researchsezione) {
            return axios.post('/GovApp/values/affluenza', researchsezione
                , {
                    headers: { 'Content-Type': 'application/json' }
                }).then(res => {
                    if (res.status === 200 && res.data !== null) {
                        commit('setMessage', '');
                        commit('setAffluenza', res.data);
                    }
                    else {
                        commit('setMessage', res.message);
                        commit('setAffluenza', null);
                    }
                }).catch((error) => {
                    commit('centralizeMessage', error);
                });
        },
        caricavoti({ commit }, researchsezione) {
            return axios.post('/GovApp/voti/carica', researchsezione
                , {
                    headers: { 'Content-Type': 'application/json' }
                }).then(res => {
                    if (res.status === 200 && res.data !== null) {
                        commit('setMessage', '');
                        commit('setVoti', res.data);
                    }
                    else {
                        commit('setMessage', res.message);
                        commit('setVoti', null);
                    }
                }).catch((error) => {
                    commit('centralizeMessage', error);
                });
        },
        insaffluenza({ commit }, affluenza) {
            return axios.post('/GovApp/values/anda', affluenza
                , {
                    headers: { 'Content-Type': 'application/json' }
                }).then(res => {
                    if (res.status === 200 && res.data !== null) {
                        commit('setMessage', '');
                        commit('setSezione', null);
                    }
                    else {
                        commit('setMessage', res.message);
                        commit('setSezione', null);
                    }
                }).catch((error) => {
                    commit('centralizeMessage', error);
                });
        },
        modaffluenza({ commit }, affluenza) {
            return axios.post('/GovApp/values/anda', affluenza
                , {
                    headers: { 'Content-Type': 'application/json' }
                }).then(res => {
                    if (res.status === 200 && res.data !== null) {
                        commit('setMessage', '');
                        commit('setSezione', null);
                    }
                    else {
                        commit('setMessage', res.message);
                        commit('setSezione', null);
                    }
                }).catch((error) => {
                    commit('centralizeMessage', error);
                });
        },
    }
  
}
export default store;