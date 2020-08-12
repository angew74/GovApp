<template>
    <div>
        <app-sidebar></app-sidebar>
        <b-card title="Ricerca Sezione"
                style="max-width: 80rem;margin-left:360px"
                header="Associa Sezione Utente">
            <app-research v-bind:form="form" @research="cercasezione"></app-research>
        </b-card>
        <app-sezione :sezionemodel="sez"></app-sezione>
        <b-card style="max-width: 80rem;margin-left:360px">
            <div class="container">
                <validation-observer ref="observer" v-slot="{ handleSubmit }">
                    <b-form @submit.stop.prevent="handleSubmit(onSubmit)">

                        <validation-provider name="user"
                                             :rules="{required:true,min:3,max:16}"
                                             v-slot="{ errors }">
                            <b-form-group id="usergroup"
                                          label="Utente associato:"
                                          label-for="user">
                                <b-input-group>
                                    <b-input-group-prepend>
                                        <span class="input-group-text"><v-icon scale="1.5" name="user" /></span>
                                    </b-input-group-prepend>
                                    <vue-autosuggest ref="autocomplete"
                                                     v-model="user"
                                                     :suggestions="suggestions"
                                                     :inputProps="inputProps"
                                                     :renderSuggestion="renderSuggestion"
                                                     :getSuggestionValue="getSuggestionValue"
                                                     @selected="onSelected"
                                                     @input="fetchResults" />
                                </b-input-group>
                                <p class="error">{{ errors[0] }}</p>
                            </b-form-group>
                        </validation-provider>
                        <b-form-input id="nSez"
                                      v-model="nsez"
                                      type="text"
                                      style="display:none"></b-form-input>
                        <b-form-input id="nCab"
                                      v-model="ncab"
                                      type="text"
                                      style="display:none"></b-form-input>
                        <b-button type="submit" :disabled="isDisabled" variant="primary">Associa</b-button>
                    </b-form>
                </validation-observer>               
            </div>
        </b-card>
        <app-footer></app-footer>
        <notifications position="top center" group="errori" />
        <notifications position="top center" group="info" />
    </div>
</template>
<script>
    import sidebaraw from '../../components/sidebaraw.vue';
    import footeraw from '../../components/footeraw.vue';
    import research from '../../components/research.vue';
    import sezioneaw from '../../components/sezioneaw.vue';

    import { mapGetters, mapState, mapActions, mapMutations } from 'vuex';
    export default {
        namespaced: true,
        components: {
            'app-sidebar': sidebaraw,
            'app-footer': footeraw,
            'app-research': research,
            'app-sezione': sezioneaw
        },
        data: function () {
            return {
                form: {
                    tipo: '',
                    cabina: '',
                    sezione: ''
                },
                messaggio: '',
                selected: null,
                sez: { iscritti: {} },
                user: '',
                nsez: '',
                ncab: '',
                users: {},
                suggestions: [],
                isDisabled: true,
                inputProps: {
                    id: "autosuggest__input",
                    placeholder: "Digitare un utente",
                    class: "form-control",
                    name: "user"
                }              
            }
        },
        computed: {
            ...mapState('context', [
                'sezione',
                'message'
            ]),
            ...mapGetters('context', [
                'Sezione',
                'isMessage',
                'Message'
            ]),             
            renderSuggestion(suggestion) {
                if (suggestion === "userslist") {   
                    return suggestion.item.userName;
                }
            },
            getSuggestionValue(suggestion) {
                let { userslist} = suggestion;
                return userslist;
            },           
            fetchResults() {                
                if (this.user !== null)
                {
                    axios({
                        method: 'get',
                        url: '/GovApp/api/auth/userssuggestions',
                        params: {
                            "username": this.user
                        }
                    })
                        .then(response => {
                            this.suggestions = [];
                            this.selected = null;
                            this.users = response.data;
                            this.suggestions.push({ name: "userslist", data: this.users });
                        })
                        .catch(function (error) {
                            console.log(error);
                            this.errored = true;
                            this.messaggio = error.response.statusText;
                        });
                }
            }
        },
        mounted() {
        },
        watch: {           
        },
        methods: {
            ...mapActions('context', [
                'restoreContext'
            ]),
            ...mapActions('context', [
                'research'
            ]),
            ...mapMutations('context', [
                'setSezione'
            ]),
            onSelected(item) {
                this.selected = item.item;
                if (item.item.length > 3)
                    this.isDisabled = false;
                else {
                    this.isDisabled = true;
                }
            },
            showAlert(message) {
                this.$notify({
                    group: 'errori',
                    position: "top center",
                    duration: "15000",
                    width: "450px",
                    type: "error",
                    title: 'Attenzione',
                    text: message
                })
            },
            showAlertAvviso(message) {
                this.$notify({
                    group: 'avvisi',
                    position: "top center",
                    duration: "10000",
                    width: "400px",
                    type: "success",
                    title: 'Complimenti',
                    text: message
                })
            },
            selectHandler(item) {
                if (item) {
                    this.selected = item.item;
                }
            },
            clickHandler(item) {
                this.loading = false;
            },
            cercasezione(e) {
                this.form = e;
                this.research({ authMethod: this.authMode, researchsezione: this.form }).then(() => {
                    if (this.isMessage) {
                        this.showAlert(this.Message);
                    }
                    else {
                        this.sez = this.Sezione;
                        this.nsez = this.Sezione.numeroSezione;
                        this.user = this.Sezione.userName;
                        this.ncab = this.Sezione.cabina;
                    }
                })
            },
            getValidationState({ dirty, validated, valid = null }) {
                return dirty || validated ? valid : null;
            },
            onSubmit() {
                axios({
                    method: 'get',
                    url: '/GovApp/api/auth/associa',
                    params: {
                        "user": this.user,
                        "sezione": this.nsez,
                        "cabina" : this.ncab
                    }
                })
                    .then(response => {
                        this.showAlertAvviso("Sezione associata con successo");
                    })
                    .catch(function (error) {
                        console.log(error);
                        this.errored = true;
                        this.messaggio = error.response.statusText;
                        this.showAlert(this.messaggio);
                    });
            }
        }
    }
</script>