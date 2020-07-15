<template>
    <div>
        <app-sidebar></app-sidebar>
        <app-formemail :dati="form" @finished="confirmdata"></app-formemail>
        <app-footer></app-footer>
        <error-bound></error-bound>
        <notifications position="top center" group="errori" />
    </div>
</template>

<script>
    import sidebaraw from '../../components/sidebaraw.vue';
    import footeraw from '../../components/footeraw.vue';
    import errorboundaryaw from '../../components/error-boundaryaw.vue';
    import formemailaw from '../../components/formemailaw'; 
    import { mapGetters, mapState, mapActions } from 'vuex';
    export default {
        namespaced: true,
        components: {
            'app-sidebar': sidebaraw,
            'app-footer': footeraw,
            'error-bound': errorboundaryaw,
            'app-formemail': formemailaw          
        },
        data: function () {
            return {
                form: {},             
                messaggio: '',              
            }
        },
        created() {
            this.restoreContext(),
                axios({
                    method: 'get',
                    url: '/GovApp/api/auth/confirm'
                })
                    .then(response => {
                        this.form = response.data;
                    })
                    .catch(function (error) {
                        console.log(error);
                    });
        },
        mounted() {
        },
        methods: {
            ...mapActions('context', [
                'restoreContext'
            ]),
            ...mapActions('context', [
                'authchange'
            ]),    
            showAlert(message) {
                this.$notify({
                    group: 'errori',
                    position: "top center",
                    duration: "10000",
                    width: "900",
                    type: "error",
                    title: 'Attenzione',
                    text: message
                }) 
            },
            confirmdata(e) {
                this.form = e;
                this.authchange({ authMethod: this.authMode, change: this.form }).then(() => {
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