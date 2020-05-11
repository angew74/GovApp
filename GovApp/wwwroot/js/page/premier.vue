<template>
    <div>
        <app-sidebar></app-sidebar>
        <app-cards codice="Premier"></app-cards>
        <app-footer></app-footer>
    </div>
</template>

<script>
    import sidebaraw from '../components/sidebaraw.vue';
    import footeraw from '../components/footeraw.vue';
    import cardsaw from '../components/cardsaw.vue';
    // con prop
    /*  export default {
          components: {
              'app-sidebar': sidebaraw,
              'app-cards': cardsaw,
              'app-footer': footeraw
          }, props: {
              codice: {
                  type: String,
                  required: true
              }
          }, created: function () {
              console.log('codice premier')
              console.log(this.codice) //prints out an empty string
          },
          data() {
              return {
                  codice : "Premier"
              };
          },
      }*/
    // senza prop
    export default {
        components: {
            'app-sidebar': sidebaraw,
            'app-cards': cardsaw,
            'app-footer': footeraw
        }
    }
</script>