<template>
    <div>
        <app-sidebar></app-sidebar>
        <div class="wrapper">
            <div class="container-fluid" style="width:70%" id="body-container-fluid">
                <b-jumbotron bg-variant="danger" text-variant="white" border-variant="dark">
                    <template v-slot:header>
                        Accesso Negato
                    </template>

                    <template v-slot:lead>
                        Utente  {{ profile.name }} non sei abilitato all'accesso a questa area dell'applicazione, oppure la funzione richiesta non è al momento disponibile.
                    </template>

                    <hr class="my-4">

                    <p>
                        Contattare l'amministratore di riferimento o quello di sistema se si ritiene di avere diritto all'accesso a questa area dell'applicazine
                    </p>
                </b-jumbotron>
            </div>
            </div>
            <app-footer></app-footer>
            <error-bound></error-bound>
        </div>
</template>

<script>
    import sidebaraw from '../../components/sidebaraw.vue';
    import footeraw from '../../components/footeraw.vue';
    import errorboundaryaw from '../../components/error-boundaryaw.vue';
    import { mapGetters, mapState, mapActions } from 'vuex';
    export default {
        namespaced: true,
        components: {
            'app-sidebar': sidebaraw,
            'app-footer': footeraw,
            'error-bound': errorboundaryaw
        },
        computed: {
            ...mapState('context', [
                'profile'
            ]),
            ...mapGetters('context', [
                'isAuthenticated'
            ])
        },
        created() {
            this.restoreContext()
        },
        methods: {
            ...mapActions('context', [
                'restoreContext'
            ])
        }
   }
</script>