<template>
    <div>
        <app-sidebar></app-sidebar>       
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
        data: function () {
            return { componenti: [] }
        },
        mounted() {
            axios({
                method: 'get',
                url: '/api/auth/users',
                params: {
                    "type": "User"
                }
            })
                .then(response => {
                    this.componenti = response.data;
                })
                .catch(function (error) {
                    console.log(error);
                });
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