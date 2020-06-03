<template>
    <div>
        <app-sidebar></app-sidebar>
        <app-formuser @finished="registeruser"></app-formuser>
        <app-footer></app-footer>
        <error-bound></error-bound>
        <notifications position="top center" group="errori" />
        <app-alert :showDismissibleAlert="showmessage" :message="messaggio"></app-alert>
    </div>
</template>

<script>
    import sidebaraw from '../../components/sidebaraw.vue';
    import footeraw from '../../components/footeraw.vue';
    import errorboundaryaw from '../../components/error-boundaryaw.vue';
    import formuser from '../../components/formuser';
    import alertaw from '../../components/alertaw.vue';
    import { mapGetters, mapState, mapActions } from 'vuex';   
    export default {
        namespaced: true,
        components: {
            'app-sidebar': sidebaraw,
            'app-footer': footeraw,
            'error-bound': errorboundaryaw,
            'app-alert': alertaw,
            'app-formuser': formuser           
        },
        data: function () {
            return {
                form: {},
                messaggio: '', 
                showmessage:false
            }
        },
        created() {
            this.restoreContext()              
        },
        mounted() {
        },
        methods: {
            ...mapActions('context', [
                'restoreContext'
            ]),    
            ...mapActions('context', [
                'register'
            ]),  
            showAlert(message) {
                this.$notify({
                    group: 'errori',
                    position:"top center",
                    duration:"10000",
                    width:"900",
                    type:"error",
                    title: 'Attenzione',
                    text: message
                })
            },
            registeruser(e) {
                this.form = e;
                this.register({ user: this.form }).then(() => {
                    if (this.$store.getters["context/isMessage"]) {
                        this.showAlert(this.$store.getters["context/Message"]);
                    }
                    else if (this.$store.getters["context/isUrl"]) {
                        window.location.href = this.$store.getters["context/Url"];
                    }
                })
            }
        }
    }
</script>