<template>
    <div>
        <app-sidebar></app-sidebar>
        <app-research  :tipo="tipoAffluenza" @research="cercasezione"></app-research>
        <app-footer></app-footer>
        <notifications position="top center" group="errori" />
    </div>
</template>

<script>
    import sidebaraw from '../../components/sidebaraw.vue';
    import footeraw from '../../components/footeraw.vue';
    import research from '../../components/research.vue'; 
    import { mapGetters, mapState, mapActions } from 'vuex';
    // con prop
      export default {
          components: {
              'app-sidebar': sidebaraw,
              'app-research': research,
              'app-footer': footeraw
          },
        data: function () {
            return {
                form: {},
                messaggio: '',
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
            confirmdata(e) {
                this.form = e;
                this.research({ authMethod: this.authMode, sezione: this.form }).then(() => {
                    if (this.isMessage) {
                        this.showAlert(this.Message);
                    }
                    else if (this.Sezione != null) {
                      
                    }
                })
            }
      }
   
</script>