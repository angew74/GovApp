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
            <app-carousel :contenuti="componenti"></app-carousel>
            </b-skeleton-wrapper>
            <app-footer></app-footer>
    </div>
</template>

<script>
    import sidebaraw from '../../components/sidebaraw.vue';
    import footeraw from '../../components/footeraw.vue';     
    import carouselaw from '../../components/carouselaw.vue'; 
    import { mapGetters, mapState, mapActions, mapMutations } from 'vuex';
    export default {
        namespaced: true,
        components: {
            'app-sidebar': sidebaraw,
            'app-footer': footeraw,           
            'app-carousel': carouselaw
        },
        data: function () {
            return {
                componenti: [],
                loading: false
            }
        },
        created() {
            this.restoreContext()
        },
        mounted() {
            axios({
                method: 'get',
                url: '/GovApp/values/carousel',
                params: {
                    "type": "Home"
                }
            })
                .then(response => {
                    this.componenti = response.data;
                    this.loading = false;
                })
                .catch(function (error) {
                    this.showSweetAlert(error.status);
                    this.loading = false;
                });
        },
        methods: {
            ...mapActions('context', [
                'restoreContext'
            ]),
            ...mapMutations('context', [
                'setMessage'
            ]),
            showSweetAlert(message) {
                this.$swal({
                    title: 'Attenzione',
                    text: message,
                    icon: 'error',
                    showCancelButton: false,
                    showCloseButton: true,
                })
            },
        }
   }
</script>