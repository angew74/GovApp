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
            <app-formemail :dati="form" @finished="confirmdata"></app-formemail>
            </b-skeleton-wrapper>
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
                messaggio: '',   
                loading:false
            }
        },
        created() {
            this.restoreContext();
            this.loading = true;
                axios({
                    method: 'get',
                    url: '/GovApp/api/auth/confirm'
                })
                    .then(response => {
                        this.form = response.data;
                        this.loading = false;
                    })
                    .catch(function (error) {
                        console.log(error);
                        this.loading = false;
                    });
        },
        mounted() {
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
        methods: {
            ...mapActions('context', [
                'restoreContext'
            ]),
            ...mapActions('context', [
                'authchange'
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
                this.loading = true;
                this.form = e;
                this.authchange({ authMethod: this.authMode, change: this.form }).then(() => {
                    if ((this.isMessage) && !this.isUrl) {
                        this.showSweetAlert(this.isMessage);
                        this.loading = false;
                    }
                    else if (this.isUrl) {
                        window.location.href = this.Url;
                        this.loading = false;
                    }
                })
            }
        }
    }
</script>