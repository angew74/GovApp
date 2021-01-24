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
                    header="Inserimento Coalizioni">
                <app-cat :categoria="cat" @selection="selezioneTipo"></app-cat>
                <app-research v-bind:form="form" @research="cercasezione"></app-research>
            </b-card>
            <app-sezione :sezionemodel="sez"></app-sezione>
            <app-scrutinio :tipo="cat"  @postscrutinio="savecoalizione" :scrutinio="datiscrutinio"></app-scrutinio>
        </b-skeleton-wrapper>
        <app-footer></app-footer>
    </div>
</template>

<script>
    import sidebaraw from '../../components/sidebaraw.vue';
    import footeraw from '../../components/footeraw.vue';
    import research from '../../components/research.vue';
    import sezioneaw from '../../components/sezioneaw.vue';
    import listacategorie from '../../components/listacategorie.vue';
    import scrutinioaw from '../../components/scrutinioaw.vue';
    import { mapGetters, mapState, mapActions, mapMutations } from 'vuex';
    // con prop
    export default {
        components: {
            'app-sidebar': sidebaraw,
            'app-research': research,
            'app-footer': footeraw,
            'app-cat': listacategorie,
            'app-scrutinio': scrutinioaw,
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
                loading: false,
                cat: 'LIS',
                sez: { iscritti: {} },
                datiscrutinio:{}
            }
        },
        computed: {
            ...mapState('context', [
                'sezione',
                'voti',
                'message'
            ]),
            ...mapGetters('context', [
                'Sezione',
                'Voti',
                'isMessage',
                'Message'
            ])
        },
        mounted() {
            this.restoreContext()
        },
        methods: {
            ...mapActions('context', [
                'restoreContext'
            ]),
            ...mapActions('context', [
                'research'
            ]),
            ...mapActions('context', [
                'caricavoti'
            ]),
            ...mapActions('context', [
                'inscoalizione'
            ]),
            ...mapMutations('context', [
                'setSezione'
            ]),
            ...mapMutations('context', [
                'setVoti'
            ]),
            selezioneTipo(e) {
                this.form.tipo = e;
            },
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
            loadscrutinio(e) {
                this.caricavoti({ authMethod: this.authMode, researchsezione: e}).then(() => {
                    if (this.isMessage) {
                        this.showSweetAlert(this.Message);
                        this.loading = false;
                    }
                    else {
                        this.datiscrutinio = this.Voti;
                        this.loading = false;
                    }
                })
            },
            savecoalizione(e) {
                this.loading = true;
                this.inscoalizione({ voti: e })
                    .then(() => {
                        if (this.isMessage) {
                            this.showSweetAlert(this.Message);
                            this.loading = false;
                        }
                        else {
                            this.showSweetAlertinfo("Inserimento effettuato");
                            this.loading = false;
                        }
                    })
            },
            cercasezione(e) {
                this.loading = true;
                this.form = e;
                this.research({ authMethod: this.authMode, researchsezione: this.form }).then(() => {
                    if (this.isMessage) {
                        this.showSweetAlert(this.Message);
                        this.loading = false;
                    }
                    else {
                        this.sez = this.Sezione;
                        this.loadscrutinio(e);                       
                    }
                })
            }
        }
    }


</script>