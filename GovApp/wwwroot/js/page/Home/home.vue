<template>
    <div>
        <app-sidebar></app-sidebar>
        <app-carousel :contenuti="componenti"></app-carousel>
        <app-footer></app-footer>
        <error-bound></error-bound>
    </div>
</template>

<script>
    import sidebaraw from '../../components/sidebaraw.vue';
    import footeraw from '../../components/footeraw.vue';   
    import errorboundaryaw from '../../components/error-boundaryaw.vue';
    import carouselaw from '../../components/carouselaw.vue'; 
    import { mapGetters, mapState, mapActions } from 'vuex';
    export default {
        namespaced: true,
        components: {
            'app-sidebar': sidebaraw,
            'app-footer': footeraw,
            'error-bound': errorboundaryaw,
            'app-carousel': carouselaw
        },
        data: function () {
            return { componenti: [] }
        },
        created() {
            this.restoreContext()
        },
        mounted() {
            axios({
                method: 'get',
                url: '/values/carousel',
                params: {
                    "type": "Home"
                }
            })
                .then(response => {
                    this.componenti = response.data;
                })
                .catch(function (error) {
                    console.log(error);
                });
        },
        methods: {
            ...mapActions('context', [
                'restoreContext'
            ])
        }
   }
</script>