<template>
    <div>
        <app-sidebar></app-sidebar>
        <app-formemail :dati="form" @finished="confirmdata"></app-formemail>
        <app-footer></app-footer>    
    </div>
</template>

<script>
    import sidebaraw from '../../components/sidebaraw.vue';
    import footeraw from '../../components/footeraw.vue';   
    import formemailaw from '../../components/formemailaw'; 
    import { mapGetters, mapState, mapActions } from 'vuex';
    export default {
        namespaced: true,
        components: {
            'app-sidebar': sidebaraw,
            'app-footer': footeraw,           
            'app-formemail': formemailaw          
        },
        data: function () {
            return {
                form: {},
                messaggio: ''              
            }
        },
        computed: {
            ...mapGetters('context', [
                'isMessage',
                'Message',
                'Url',
                'isUrl'
            ]),
            ...mapState('context', [
                'message',
                'url'
            ])
        },
        created() {
            this.restoreContext(),
                axios({
                    method: 'get',
                    url: '/GovApp/api/auth/confirm'
                })
                    .then(response => {
                        this.form = response.data;
                    })
                    .catch(function (error) {
                        console.log(error);
                    });
        },
        mounted() {
        },
        methods: {
            ...mapActions('context', [
                'restoreContext'
            ]),
            ...mapActions('context', [
                'authconfirm'
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
            confirmdata(e) {
                this.form = e;
                this.authconfirm({ authMethod: this.authMode, confirm: this.form }).then(() => {
                    if ((this.isMessage) && !this.isUrl) {
                        this.showSweetAlert(this.isMessage);
                    }
                    else if (this.isUrl) {
                        window.location.href = this.Url;
                    }
                })
            }
        }
    }
</script>