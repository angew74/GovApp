<template>
    <div>
        <app-sidebar></app-sidebar>
        <app-formuser @finished="registeruser"></app-formuser>
        <app-footer></app-footer>
        <error-bound></error-bound>
        <notifications position="top center" group="errori" />
        <notifications position="top center" group="avvisi">
            <template slot="body" slot-scope="props">
                <div>
                    <a class="title">
                        {{props.item.title}}
                    </a>
                    <a class="close" @click="props.close">
                        <v-icon name="times" />
                    </a>
                    <div>
                        <span> <v-icon name="thumbs-up" /></span>
                        <p v-html="props.item.text">
                        </p>
                    </div>
                </div>
            </template>
        </notifications>
    </div>
</template>

<script>
    import sidebaraw from '../../components/sidebaraw.vue';
    import footeraw from '../../components/footeraw.vue';
    import errorboundaryaw from '../../components/error-boundaryaw.vue';
    import formuser from '../../components/formuser';
    import { mapGetters, mapState, mapActions } from 'vuex';
    export default {
        namespaced: true,
        components: {
            'app-sidebar': sidebaraw,
            'app-footer': footeraw,
            'error-bound': errorboundaryaw,
            'app-formuser': formuser
        },
        data: function () {
            return {
                form: {},
                messaggio: '',
                showmessage: false,
                countDown: 10               
            }
        },
        created() {
            this.restoreContext()
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
            showAlert(message) {
                this.$notify({
                    group: 'errori',
                    position: "top center",
                    duration: "10000",
                    width: "500px",
                    type: "error",
                    title: 'Attenzione',
                    text: message
                })
            },
            showAlertAvviso(message) {
                this.$notify({
                    group: 'avvisi',
                    position: "top center",
                    duration: "10000",
                    width: "400px",
                    type: "success",
                    title: 'Complimenti',
                    text: message
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
                    window.location.href = this.$store.getters["context/Url"];
                }
            },
            registeruser(e) {
                this.form = e;
                this.register({ user: this.form }).then(() => {
                    if (this.$store.getters["context/isMessage"]) {
                        this.showAlert(this.$store.getters["context/Message"]);
                    }
                    else if (this.$store.getters["context/isUrl"]) {
                        this.showAlertAvviso("Utente registrato con successo, all'indirizzo email indicato sono state inviate le credenziali.");
                        this.countDownTimer();                       
                    }
                })
            }
        }
    }
</script>