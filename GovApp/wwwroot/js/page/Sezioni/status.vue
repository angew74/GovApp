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
                    header="Status Sezione">
                <app-research v-bind:form="form" @research="statussezione"></app-research>
            </b-card>
            <app-sezione :sezionemodel="sez"></app-sezione>
            <app-status :sezionemodel="sez"></app-status>
        </b-skeleton-wrapper>
        <app-footer></app-footer>
    </div>
</template>
<script>
    import sidebaraw from '../../components/sidebaraw.vue';
    import footeraw from '../../components/footeraw.vue';
    import research from '../../components/research.vue';
    import sezioneaw from '../../components/sezioneaw.vue';
    import statussezioneaw from '../../components/statussezioneaw.vue';
    import { mapGetters, mapState, mapActions, mapMutations } from 'vuex';
    export default {
        namespaced: true,
        components: {
            'app-sidebar': sidebaraw,
            'app-footer': footeraw,
            'app-research': research,
            'app-sezione': sezioneaw,
            'app-status': statussezioneaw
        },
        data: function () {
            return {
                form: {
                    tipo: 'int',
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
                'status'
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
            statussezione(e) {
                this.loading = true;
                this.form = e;
                this.status({ authMethod: this.authMode, research: this.form }).then(() => {
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
            }         
        }
    }
</script>