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
            <app-formuser @finished="registeruser"></app-formuser>
            </b-skeleton-wrapper>
            <app-footer></app-footer>
    </div>
</template>

<script>
    import sidebaraw from '../../components/sidebaraw.vue';
    import footeraw from '../../components/footeraw.vue';
    import formuser from '../../components/formuser';
    import { mapGetters, mapState, mapActions } from 'vuex';
    export default {
        namespaced: true,
        components: {
            'app-sidebar': sidebaraw,
            'app-footer': footeraw,           
            'app-formuser': formuser
        },
        data: function () {
            return {
                form: {},
                messaggio: '',
                showmessage: false,
                countDown: 10,
                loading:false,
            }
        },
        created() {
            this.restoreContext()
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
        mounted() {
        },
        methods: {
            ...mapActions('context', [
                'restoreContext'
            ]),
            ...mapActions('context', [
                'register'
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
            showSweetAlertinfo(message) {
                this.$swal({
                    title: 'Congratulazioni',
                    text: message,
                    icon: 'info',
                    showCancelButton: false,
                    showCloseButton: true,
                })
            }, 
            countDownTimer() {
                if (this.countDown > 0) {
                    setTimeout(() => {
                        this.countDown -= 1
                        this.countDownTimer();
                    }, 1000)
                }
                else{
                    window.location.href = this.Url;
                }
            },
            registeruser(e) {
                this.loading = true;
                this.form = e;
                this.register({ user: this.form }).then(() => {
                    if ((this.isMessage) && !this.isUrl) {
                        this.showSweetAlert(this.isMessage);
                        this.loading = false;
                    }
                    else if (this.isUrl) {
                        this.showSweetAlertinfo("Utente registrato con successo, all'indirizzo email indicato sono state inviate le credenziali.");
                        this.loading = false;
                    }    
                })
            }
        }
    }
</script>