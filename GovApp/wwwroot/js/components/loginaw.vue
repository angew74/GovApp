<template>
    <b-modal id="loginModal" ref="loginModal" hide-footer title="Autenticazione" @hidden="onHidden"
             header-bg-variant="dark"
             header-text-variant="light"
             body-bg-variant="light"
             body-text-variant="dark">
        <b-form @submit.prevent="onSubmit" @reset.prevent="onCancel">
            <b-form-group label="Utenza:" label-for="usernameInput">
                <b-form-input id="usernameInput"
                              type="text"
                              v-model="form.username"
                              required
                              placeholder="inserisci username">
                </b-form-input>
            </b-form-group>
            <b-form-group label="Password:" label-for="passwordInput">
                <b-form-input id="passwordInput"
                              type="password"
                              v-model="form.password"
                              required
                              placeholder="Inserisci Password">
                </b-form-input>
            </b-form-group>
            <button class="btn btn-primary float-right ml-2" type="submit">Accedi</button>
            <button class="btn btn-secondary float-right" type="reset">Annnulla</button>
        </b-form>     
        <b-alert v-model="showDismissibleAlert" variant="danger"><a v-bind:href="url" class="alert-link">{{text}}</a></b-alert>
        <app-alert :message="messaggio" :dismissCountDown="dismiss"></app-alert>
    </b-modal>   
</template>

<script>
    import { mapActions, mapState, mapGetters } from 'vuex';
    import alertaw from '../components/alertaw.vue';
    export default {
        name: 'loginaw',
        namespaced: true,
        components: {
            'app-alert': alertaw          
        },
        data() {
            return {
                form: {
                    username: '',
                    password: ''
                },               
                url: '',
                showDismissibleAlert: false,
                messaggio:'',
                dismiss: '0',
                text:''
            }
        },
        methods:
        {
            showAlertRedirection(message,url, showDismissibleAlert) {
                this.text = message;
                this.showDismissibleAlert = showDismissibleAlert;
                this.url = url;
            },
            showAlert(message, dismissSecs) {
                this.messaggio = message;
                this.dismiss = dismissSecs;
            },
           ...mapActions('context', [
                'login'
            ]),
            onSubmit(evt) {
                this.login({ authMethod: this.authMode, credentials: this.form }).then(() => {
                    if (this.$store.getters["context/isAuthenticated"]) {
                        this.$refs.loginModal.hide();
                        window.location.href = "/home/index";
                    }
                    if (this.$store.getters["context/isMessage"] && !this.$store.getters["context/isUrl"]) {
                        this.showAlert(this.$store.getters["context/Message"], "10");                        
                    }
                    else if (this.$store.getters["context/isUrl"]) {
                        this.showAlertRedirection(this.$store.getters["context/Message"], this.$store.getters["context/Url"], true);
                    }
                })
            },
            onHidden() {
                Object.assign(this.form, {
                    username: '',
                    password: ''
                })
            }
        }
    }
</script>


