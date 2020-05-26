<template>
    <div>
        <app-sidebar></app-sidebar>
        <app-cards :contenuti="componenti"></app-cards>
        <app-footer></app-footer>
        <error-bound></error-bound>
    </div>
</template>

<script>
    import sidebaraw from '../../components/sidebaraw.vue';
    import footeraw from '../../components/footeraw.vue';
    import errorboundaryaw from '../../components/error-boundaryaw.vue';
    import cardsaw from '../../components/cardsaw.vue';
    import { mapGetters, mapState, mapActions } from 'vuex';
    export default {
        namespaced: true,
        components: {
            'app-sidebar': sidebaraw,
            'app-cards': cardsaw,
            'app-footer': footeraw,
            'error-bound': errorboundaryaw
        },
        data: function () {
            return { componenti: [] }
        },
        mounted() {
            axios({
                method: 'get',
                url: '/values/content',
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