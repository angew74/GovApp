<template>
    <div>
        <app-sidebar></app-sidebar>
        <b-card title="Ricerca Sezione"
                style="max-width: 80rem;margin-left:360px"
                header="Inserimento Coalizioni">
            <app-cat :categoria="cat" @selection="selezioneTipo"></app-cat>
            <app-research v-bind:form="form" @research="cercasezione"></app-research>
        </b-card>
        <app-sezione :sezionemodel="sez"></app-sezione>
      
        <app-footer></app-footer>
        <notifications position="top center" group="errori" />
        <notifications position="top center" group="avvisi" />
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
                this.form = e;
                this.research({ authMethod: this.authMode, researchsezione: this.form }).then(() => {
                    if (this.isMessage) {
                        this.showAlert(this.Message);
                    }
                    else {
                        this.tipoAffluenza = this.Sezione.tipo;
                        this.sez = this.Sezione;
                        if (this.tipoAffluenza !== 'CO' && this.tipoAffluenza !== 'AP') {
                            this.cercaaffluenza(e);
                        }
                        else {
                            this.aff.numeroSezione = this.Sezione.sezione;
                            this.aff.tipo = this.Sezione.tipo;
                        }
                    }
                })

            }
        }
    }


</script>