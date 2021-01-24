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
                    header="Inserimento Andamento">
                <app-cat :categoria="cat" @selection="selezioneTipo"></app-cat>
                <app-research v-bind:form="form" @research="cercasezione"></app-research>
            </b-card>
            <app-sezione :sezionemodel="sez"></app-sezione>
            <app-anda @watchbutton="bandamento" @postanda="iandamento" :affluenzasezione="aff" :tipo="tipoAffluenza"></app-anda>
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
    import anda from '../../components/anda.vue';
    import { mapGetters, mapState, mapActions, mapMutations } from 'vuex';
    // con prop
    export default {
        components: {
            'app-sidebar': sidebaraw,
            'app-research': research,
            'app-footer': footeraw,
            'app-cat': listacategorie,
            'app-anda': anda,
            'app-sezione': sezioneaw
        },
        data: function () {
            return {
                form: {
                    tipo: '',
                    cabina: '',
                    sezione: ''
                },
                anda: {
                    tipoAffluenza: '',
                    operazione: 'M',
                    sezione: ''
                },
                messaggio: '',
                cat: 'RAF',
                loading:false,
                tipoAffluenza: '',
                sez: { iscritti: {} },
                aff:{numeroSezione:'',tipo:''}
            }
        },
        computed: {
            ...mapState('context', [
                'sezione',
                'message',
                'affluenza'
            ]),
            ...mapGetters('context', [
                'Sezione',
                'isMessage',
                'Message',
                'Affluenza'
            ])
        },
        mounted() {
            this.restoreContext();
        },
        methods: {
            ...mapActions('context', [
                'restoreContext'
            ]),
            ...mapActions('context', [
                'research'
            ]),
            ...mapActions('context', [
                'modandamento'
            ]),
            ...mapActions('context', [
                'modaffluenza'
            ]),
            ...mapActions('context', [
                'researchAffluenza'
            ]),
            ...mapMutations('context', [
                'setSezione'
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
            bandamento(e) {
                this.loading = true;
                this.anda.tipoAffluenza = e;
                this.anda.operazione = "M";
                this.anda.sezione = this.form.sezione;
                this.modandamento({ authMethod: this.authMode, anda: this.anda }).then(() => {
                    if (this.isMessage) {
                        this.showSweetAlert(this.Message);
                        this.loading = false;
                    }
                    else {
                        this.showSweetAlertinfo("Andamento modificato con successo");
                        this.tipoAffluenza = '';
                        this.loading = false;
                    }
                })
            },
            iandamento(r) {
                this.loading = true;
                this.modaffluenza({ authMethod: this.authMode, affluenza: r }).then(() => {
                    if (this.isMessage) {
                        this.showSweetAlert(this.Message);
                        this.loading = false;
                    }
                    else {
                        this.showSweetAlertinfo("Affluenza modificata con successo");
                        this.tipoAffluenza = '';
                        this.loading = false;
                    }
                })
            },
            cercaaffluenza(e) {
                this.loading = true;
                this.researchAffluenza({ authMethod: this.authMode, researchsezione: e }).then(() => {
                    if (this.isMessage) {
                        this.showSweetAlertinfo(this.Message);
                        this.loading = false;
                    }
                    else {
                        this.aff = this.Affluenza;
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
                        this.tipoAffluenza = this.Sezione.tipo;
                        this.sez = this.Sezione;
                        if (this.tipoAffluenza !== 'CO' && this.tipoAffluenza !== 'AP') {
                            this.cercaaffluenza(e);
                            this.loading = false;
                        }
                        else {
                            this.aff.numeroSezione = this.Sezione.sezione;
                            this.aff.tipo = this.Sezione.tipo;
                            this.loading = false;
                        }
                    }
                })

            }
        }
    }


</script>