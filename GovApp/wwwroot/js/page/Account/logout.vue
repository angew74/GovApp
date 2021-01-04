<template>
    <div>
        <app-sidebar></app-sidebar>
        <div class="wrapper">
            <div class="container-fluid" style="width:70%" id="body-container-fluid">
                <b-jumbotron bg-variant="secondary" text-variant="white" border-variant="dark">
                    <template v-slot:header>
                        Logout
                    </template>
                    <template v-slot:lead>
                        <b-iconstack font-scale="2">
                            <b-icon stacked icon="bell-fill" class="rounded-circle bg-danger p-2" variant="light"></b-icon>                          
                        </b-iconstack>
                        Ti sei disconnesso con successo. Arrivederci a presto
                    </template>
                    <hr class="my-4">
                    <p>                      
                    </p>
                </b-jumbotron>
            </div>
        </div>
        <app-footer></app-footer>     
    </div>
</template>

<script>
    import sidebaraw from '../../components/sidebaraw.vue';
    import footeraw from '../../components/footeraw.vue';  
    import { mapGetters, mapState, mapActions } from 'vuex';
    export default {
        namespaced: true,
        components: {
            'app-sidebar': sidebaraw,
            'app-footer': footeraw,           
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