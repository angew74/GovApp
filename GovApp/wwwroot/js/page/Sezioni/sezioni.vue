<template>
    <div>
        <app-sidebar></app-sidebar>
        <b-skeleton-wrapper :loading="loading">
            <template #loading>
                <b-row>
                    <b-col>
                        <b-skeleton-img></b-skeleton-img>
                    </b-col>
                    <b-col>
                        <b-skeleton-img></b-skeleton-img>
                    </b-col>
                    <b-col cols="12" class="mt-3">
                        <b-skeleton-img no-aspect height="150px"></b-skeleton-img>
                    </b-col>
                </b-row>
            </template>
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
                                            <span class="input-group-text"><v-icon name="user" /></span>
                                        </b-input-group-prepend>
                                        <vue-bootstrap-typeahead ref="autocomplete"                                                                
                                                                 v-model="query"
                                                                 :data="items"
                                                                 :serializer="item => item.userName"
                                                                 @hit="itemSelected = $event"
                                                                 placeholder="Inserisci utente da associare" />
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
            </b-skeleton-wrapper>
            <app-footer></app-footer>
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
                loading:false,
                selected: null,
                sez: { iscritti: {} },
                user: '',
                nsez: '',
                ncab: '',                         
                items: [],
                itemSelected: null,            
                item: { id: "0", userName: "" },
                query: '',
                isDisabled: true,                       
            }
        },        
        computed: {
            ...mapState('context', [
                'sezione',
                'message',
                'auto'
            ]),
            ...mapGetters('context', [
                'Sezione',
                'isMessage',
                'Message',
                'Auto'
            ]),                   
        },
        mounted() {
            this.restoreContext()
        },
        watch: {  
            query(newQuery) {
                if (newQuery.length > 3) {
                    this.autocomplete(newQuery).then((response) => {
                        if (this.isMessage) {
                            this.showSweetAlert(this.Message);
                        } else {
                            this.items = this.Auto;
                        }
                    })
                }
            },    
        },
        methods: {
            ...mapActions('context', [
                'restoreContext'
            ]),
            ...mapActions('context', [
                'autocomplete'
            ]),
            ...mapActions('context', [
                'research'
            ]),
            ...mapMutations('context', [
                'setSezione'
            ]),          
            showSweetAlert(message) {
                this.$swal({
                    title: 'Attenzione',
                    text: message,
                    icon: 'error',
                    showCancelButton: false,
                    showCloseButton: true,
                })
            },
            showSweetAlertinfo(message) {
                this.$swal({
                    title: 'Congratulazioni',
                    text: message,
                    icon: 'info',
                    showCancelButton: false,
                    showCloseButton: true,
                })
            },                  
            cercasezione(e) {
                this.loading = true;
                this.form = e;
                this.research({ authMethod: this.authMode, research: this.form }).then(() => {
                    if (this.isMessage) {
                        this.showSweetAlert(this.Message);
                        this.loading = false;
                    }
                    else {
                        this.sez = this.Sezione;
                        this.nsez = this.Sezione.numeroSezione;
                        this.user = this.Sezione.userName;
                        this.ncab = this.Sezione.cabina;
                        this.loading = false;
                    }
                })
            },
            getValidationState({ dirty, validated, valid = null }) {
                return dirty || validated ? valid : null;
            },
            onSubmit() {
                this.loading = true;
                axios({
                    method: 'get',
                    url: '/GovApp/api/auth/associa',
                    params: {
                        "user": this.itemSelected.userName,
                        "sezione": this.nsez,
                        "cabina" : this.ncab
                    }
                })
                    .then(response => {
                        this.showSweetAlertinfo("Sezione associata con successo");
                        this.loading = false;
                    })
                    .catch(function (error) {
                        console.log(error);
                        this.errored = true;
                        this.messaggio = error.response.statusText;
                        this.showSweetAlert(this.messaggio);
                        this.loading = false;
                    });
            }
        }
    }
</script>