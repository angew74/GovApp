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
            <app-cards :contenuti="componenti"></app-cards>
        </b-skeleton-wrapper>
        <app-footer></app-footer>
    </div>
</template>

<script>
    import sidebaraw from '../../components/sidebaraw.vue';
    import footeraw from '../../components/footeraw.vue';
    import cardsaw from '../../components/cardsaw.vue';
    import { mapGetters, mapState, mapActions } from 'vuex';
    // con prop
      export default {
          components: {
              'app-sidebar': sidebaraw,
              'app-cards': cardsaw,
              'app-footer': footeraw
          },
        data: function () {
            return {
                componenti: [],
                loading:false,
            }
        },
        methods: {
            ...mapActions('context', [
                'restoreContext'
            ])
        },
        created() {
            this.restoreContext()
        },
        mounted() {
            this.loading = true;
            axios({
                method: 'get',
                url: '/GovApp/values/content',
                params: {
                    "type": "Interrogazioni"
                }
            })
                .then(response => {
                    this.componenti = response.data;
                    this.loading = false;
                })
                .catch(function (error) {
                    console.log(error);
                    this.loading = false;
                });
        }
      }
</script>