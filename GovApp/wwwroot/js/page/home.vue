<template>
    <div>
        <app-sidebar></app-sidebar>
        <app-footer></app-footer>
    </div>
</template>

<script>
    import sidebaraw from '../components/sidebaraw.vue';
    import footeraw from '../components/footeraw.vue';
    export default {
        components: {
            'app-sidebar': sidebaraw,
            'app-footer': footeraw
        }
   }
</script>