<template>
    <div class="container" style="margin-top:140px;">
        <b-form @submit.prevent="handleSubmit" v-if="show">
            <b-form-group id="emailgroup"
                          label="Email address:"
                          label-for="email"
                          description="Email con cui ti sei registrato">
                <b-form-input id="email"
                              v-model="form.email"
                              type="email"
                              readonly></b-form-input>
            </b-form-group>

            <b-form-group id="usernamegroup" label="Username:" label-for="username">
                <b-form-input id="username"
                              v-model="form.userName"
                              readonly></b-form-input>
            </b-form-group>
            <b-form-group id="passwordgroup" label="Password:" label-for="password">
                <b-form-input id="password"
                              v-model="form.password"
                              required
                              placeholder="inserisci password"></b-form-input>
            </b-form-group>
            <b-form-group id="confirmpasswordgroup" label="Conferma Password:" label-for="confirmpassword">
                <b-form-input id="confirmpassword"
                              v-model="form.confirmPassword"
                              required
                              placeholder="inserisci conferma password"></b-form-input>
            </b-form-group>
            <b-button type="submit" variant="primary">Conferma</b-button>
         </b-form>
        <app-alert :message="messaggio" :dismissCountDown="dismiss"></app-alert>
    </div>
</template>


<script>
    import { mapActions, mapState, mapGetters } from 'vuex';
    import alertaw from '../components/alertaw.vue';

    export default {
        name: 'formemailaw',
        namespaced: true,
        components: {
            'app-alert': alertaw
        },
        props: ['dati'],
        data() {
            return {
                form: this.dati,
                show: true,
                messaggio: '',
                dismiss: '0'

            }
        },
        watch: {
            dati(newVal) {
                this.form = newVal;               
            }
        },
        methods: {
            ...mapActions('context', [
                'authconfirm'
            ]),
            showAlert(message, dismissSecs) {
                this.messaggio = message;
                this.dismiss = dismissSecs;
            },
            handleSubmit() {
                this.authconfirm({ authMethod: this.authMode, confirm: this.form }).then(() => {
                    if (this.$store.getters["context/isMessage"]) {
                        this.showAlert(this.$store.getters["context/Message"], "10");
                    }
                    else if (this.$store.getters["context/isUrl"]) {
                        window.location.href = this.$store.getters["context/Url"];
                    }
                })
            }
        }       
    }

</script>
