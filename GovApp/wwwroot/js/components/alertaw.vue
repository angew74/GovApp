<template>
    <b-alert :show="dismissCountDown"
             dismissible
             fade
             variant="danger"
             @dismissed="dismissCountDown=0"
             @dismiss-count-down="countDownChanged">
        {{ message }}
    </b-alert>
</template>
<script>   
    export default {
        name: 'alertaw',
        namespaced: true,
        props: ['message', 'dismissCountDown'],
        data() {
            return {
                dismissSecs: 5               
            }
        },
        watch: {
            dismissCountDown(newVal) {
                this.dismissCountDown = newVal;
            }
        },
        methods:
        {
            countDownChanged(dismissCountDown) {
                this.dismissCountDown = dismissCountDown
            }
        }
    }
</script>