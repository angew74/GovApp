<template>
    <div>
        <app-sidebar></app-sidebar>
        <app-formemail :dati="form"></app-formemail>
        <app-footer></app-footer>
        <error-bound></error-bound>
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
                form: {}
            }
        },
        created() {
            this.restoreContext(),
                axios({
                    method: 'get',
                    url: '/api/auth/confirm'
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
            ])
        }
    }
</script>