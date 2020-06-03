<template>
    <b-alert :show="showDismissibleAlert"
             dismissible
             fade
             variant="danger">
    <v-icon name="exclamation-circle"></v-icon>{{ message }}
    </b-alert>
</template>
<script>   
    export default {
        name: 'alertaw',
        namespaced: true,
        props: ['message','showDismissibleAlert'],
        data() {
            return {                        
            }
        },       
        computed: 
        {}
    }
</script>