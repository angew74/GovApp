<template>
    <div>
        <app-sidebar></app-sidebar>
        <app-cards :contenuti="componenti"></app-cards>
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
            return { componenti: [] }
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
            axios({
                method: 'get',
                url: '/GovApp/values/content',
                params: {
                    "type": "Affluenze"
                }
            })
                .then(response => {
                    this.componenti = response.data;
                })
                .catch(function (error) {
                    console.log(error);
                });
        }
      }    
</script>