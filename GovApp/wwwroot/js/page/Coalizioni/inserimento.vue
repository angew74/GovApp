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
  
    import { mapGetters, mapState, mapActions, mapMutations } from 'vuex';
    // con prop
    export default {
        components: {
            'app-sidebar': sidebaraw,
            'app-research': research,
            'app-footer': footeraw,
            'app-cat': listacategorie,       
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
                cat: '  LIS',               
                sez: { iscritti: {} }               
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
            ...mapMutations('context', [
                'setSezione'
            ]),
            selezioneTipo(e) {
                this.form.tipo = e;
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
            cercasezione(e) {
                this.loading = true;
                this.form = e;
                this.research({ authMethod: this.authMode, researchsezione: this.form }).then(() => {
                    if (this.isMessage) {
                        this.showAlert(this.Message);
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