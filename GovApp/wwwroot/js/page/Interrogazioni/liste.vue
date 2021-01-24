<template>
    <div>
        <app-sidebar></app-sidebar>
        <b-skeleton-wrapper :loading="loading">
            <template #loading>
                <b-row>
                    <b-col>
                        <b-skeleton-img></b-skeleton-img>
                    </b-col>
                    <b-col>
                        <b-skeleton-img></b-skeleton-img>
                    </b-col>
                    <b-col cols="12" class="mt-3">
                        <b-skeleton-img no-aspect height="150px"></b-skeleton-img>
                    </b-col>
                </b-row>
            </template>
            <app-tabs @sended="ricerca" :cat="tipo"></app-tabs>
        </b-skeleton-wrapper>
        <app-footer></app-footer>
    </div>
</template>

<script>
    import sidebaraw from '../../components/sidebaraw.vue';
    import footeraw from '../../components/footeraw.vue';
    import tabsaw from '../../components/tabsaw.vue';
    import { mapGetters, mapState, mapActions } from 'vuex';
    // con prop
    export default {
        components: {
            'app-sidebar': sidebaraw,
            'app-footer': footeraw,
            'app-tabs': tabsaw
        },
        data: function () {
            return {
                loading: false,
                tipo: 'LIS'
            }
        },
        methods: {
            ...mapActions('context', [
                'restoreContext'
            ]),
            ricerca(e) {

            }
        },
        created() {
            this.restoreContext()
        },
        mounted() {
        }
    }
</script>