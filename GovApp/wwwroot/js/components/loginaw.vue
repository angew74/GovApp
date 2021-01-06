<template>
    <div>
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
        </b-modal>
    </b-skeleton-wrapper>
        </div>
</template>

<script>
    import { mapActions, mapState, mapGetters } from 'vuex';
  export default {
        name: 'loginaw',
        namespaced: true,
        components: {                  
        },
        data() {
            return {
                form: {
                    username: '',
                    password: ''
                },     
                showDismissibleAlert: false,
                messaggio:'',
                text: '',
                loading: false
            }
        },
        computed: {
            ...mapGetters('context', [
                'User',                
                'isMessage',
                'Message',               
                'isAuthenticated',
                'Url',
                'isUrl'
            ]),
            ...mapState('context', [               
                'message',               
                'url'
            ])
        },
        methods:
        {
            showSweetAlertRedirection(message, url) {
                this.$swal({
                    title: '<strong>Attenzione <u>password</u> da cambiare</strong>',
                    icon: 'warning',
                    width: '50em',
                    html:
                        'scegliere una <b>password</b> personalizzata utilizzando il seguente ' +
                        '<strong><a href=' + url + '>link</a></strong>',
                    showCloseButton: true,
                    showCancelButton: false,
                    showConfirmButton: false,
                    focusConfirm: false
                })
            },
            showSweetAlert(message) {
                this.$swal({
                    title: 'Attenzione',
                    text: message,
                    icon: 'error',
                    showCancelButton: false,
                    showCloseButton: true,
                })
            },
           ...mapActions('context', [
                'login'
            ]),
            onSubmit(evt) {
                this.loading = true;
                this.login({ authMethod: this.authMode, credentials: this.form }).then(() => {
                    if (this.isAuthenticated) {  
                        window.location.href = "/GovApp/home/index";
                        this.loading = false;
                    }
                    if ((this.isMessage) && !this.isUrl) {
                        this.showSweetAlert(this.Message);        
                        this.loading = false;
                    }
                    else if (this.isUrl) {
                        this.showSweetAlertRedirection(this.Message, this.Url);
                        this.loading = false;
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


