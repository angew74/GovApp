<template>
    <div>
        <app-sidebar></app-sidebar>
        <app-cards :contenuti="componenti"></app-cards>
        <app-footer></app-footer>
    </div>
</template>

<script>
    import sidebaraw from '../components/sidebaraw.vue';
    import footeraw from '../components/footeraw.vue';
    
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
        mounted() {
            axios({
                method: 'get',
                url: '/values/content',
                params: {
                    "type": "Premier"
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
    // senza prop

    /*
    export default {
        components: {
            'app-sidebar': sidebaraw,
            'app-cards': cardsaw,
            'app-footer': footeraw
        }
    }*/
</script>