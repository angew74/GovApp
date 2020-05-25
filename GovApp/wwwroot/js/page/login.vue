<template>
    <div>
        <app-sidebar></app-sidebar>
        <login-modal></login-modal>
        <app-footer></app-footer>
    </div>
</template>

<script>
    import sidebaraw from '../components/sidebaraw.vue';
    import loginaw from '../components/loginaw.vue';
    import footeraw from '../components/footeraw.vue';  
    import { mapGetters, mapState, mapActions } from 'vuex';       
    export default {
        components: {          
            'app-sidebar': sidebaraw,
            'app-footer': footeraw,
            'login-modal': loginaw
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