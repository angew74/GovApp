<template>
    <div class="container" style="margin-top: 90px;position: relative;overflow: auto;">
        <validation-observer ref="observer" v-slot="{ handleSubmit }">
            <b-form v-if="show" @submit.stop.prevent="handleSubmit(onSubmit)">

                <validation-provider name="email"
                                     :rules="{required:true,min:5,max:100,email:true}"
                                     v-slot="validationContext">
                    <b-form-group id="emailgroup"
                                  label="Email address:"
                                  label-for="email"
                                  >
                        <b-input-group>
                            <b-input-group-prepend>
                                <span class="input-group-text"><v-icon scale="1.5" name="at" /></span>
                            </b-input-group-prepend>
                            <b-form-input id="email"
                                          v-model="form.email"
                                          :state="getValidationState(validationContext)"
                                          aria-describedby="input-mail-live-feedback"
                                            placeholder="inserisci email"
                                          type="email"></b-form-input>
                            <b-form-invalid-feedback id="input-email-live-feedback">{{ validationContext.errors[0] }}</b-form-invalid-feedback>
                        </b-input-group>                       
                    </b-form-group>
                </validation-provider>


                <validation-provider name="username"
                                     :rules="{required:true,min:8,max:16}"
                                     v-slot="validationContext">
                    <b-form-group id="usernamegroup" label="Username:" label-for="username">
                        <b-input-group>
                            <b-input-group-prepend>
                                <span class="input-group-text"><v-icon scale="1.5" name="user-plus" /></span>
                            </b-input-group-prepend>
                            <b-form-input id="username"
                                          :state="getValidationState(validationContext)"
                                          aria-describedby="input-username-live-feedback"
                                          placeholder="inserisci username"
                                          v-model="form.userName"></b-form-input>
                            <b-form-invalid-feedback id="input-username-live-feedback">{{ validationContext.errors[0] }}</b-form-invalid-feedback>
                        </b-input-group>                 
                    </b-form-group>
                </validation-provider>


                <validation-provider name="sesso"
                                     :rules="{required:true,min:1,max:1}"
                                     v-slot="validationContext">
                    <b-form-group id="sessogroup"
                                  label="Sesso:"
                                  label-for="sesso">
                        <b-input-group>
                            <b-input-group-prepend>
                                <span class="input-group-text"><v-icon scale="1.5" name="venus-mars" /></span>
                            </b-input-group-prepend>
                            <b-form-input id="sesso"
                                          v-model="form.sesso"
                                          placeholder="inserisci sesso"
                                          :state="getValidationState(validationContext)"
                                          aria-describedby="input-sesso-live-feedback"
                                          type="text"></b-form-input>
                            <b-form-invalid-feedback id="input-sesso-live-feedback">{{ validationContext.errors[0] }}</b-form-invalid-feedback>
                        </b-input-group>                      
                    </b-form-group>
                </validation-provider>

                <validation-provider name="cognome"
                                     :rules="{required:true,min:3,max:150}"
                                     v-slot="validationContext">
                    <b-form-group id="cognomegroup"
                                  label="Cognome:"
                                  label-for="cognome">
                        <b-input-group>
                            <b-input-group-prepend>
                                <span class="input-group-text"><v-icon scale="1.5" name="pen-nib" /></span>
                            </b-input-group-prepend>
                            <b-form-input id="cognome"
                                          v-model="form.cognome"
                                          :state="getValidationState(validationContext)"
                                          aria-describedby="input-cognome-live-feedback"
                                          placeholder="inserisci cognome"
                                          type="text"></b-form-input>
                            <b-form-invalid-feedback id="input-cognome-live-feedback">{{ validationContext.errors[0] }}</b-form-invalid-feedback>
                        </b-input-group>                       
                    </b-form-group>
                </validation-provider>

                <validation-provider name="nome"
                                     :rules="{required:true,min:2,max:150}"
                                     v-slot="validationContext">
                    <b-form-group id="nomegroup"
                                  label="Nome:"
                                  label-for="nome">
                        <b-input-group>
                            <b-input-group-prepend>
                                <span class="input-group-text"><v-icon scale="1.5" name="pen-nib" /></span>
                            </b-input-group-prepend>
                            <b-form-input id="cognome"
                                          v-model="form.nome"
                                          :state="getValidationState(validationContext)"
                                          aria-describedby="input-nome-live-feedback"
                                          placeholder="inserisci nome"
                                          type="text"></b-form-input>
                            <b-form-invalid-feedback id="input-nome-live-feedback">{{ validationContext.errors[0] }}</b-form-invalid-feedback>
                        </b-input-group>                          
                    </b-form-group>
                </validation-provider>

                <validation-provider name="codicefiscale"
                                     :rules="{required:true,min:16,max:16}"
                                     v-slot="validationContext">
                    <b-form-group id="codicefiscalegroup"
                                  label="Codice Fiscale:"
                                  label-for="codicefiscale">
                        <b-input-group>
                            <b-input-group-prepend>
                                <span class="input-group-text"><v-icon scale="1.5" name="id-card-alt" /></span>
                            </b-input-group-prepend>
                            <b-form-input id="codicefiscale"
                                          v-model="form.codicefiscale"
                                          :state="getValidationState(validationContext)"
                                          aria-describedby="input-codicefiscale-live-feedback"
                                          placeholder="inserisci codice fiscale"
                                          type="text"></b-form-input>
                            <b-form-invalid-feedback id="input-codicefiscale-live-feedback">{{ validationContext.errors[0] }}</b-form-invalid-feedback>
                        </b-input-group>                           
                    </b-form-group>
                </validation-provider>


                <validation-provider vid="password"
                                     name="password"
                                     :rules="{required:true,password:form.confirmPassword,min:8,max:16, regex: /^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#\$%\^&\*])(?=.{8,})/}"
                                     v-slot="validationContext">
                    <b-form-group id="passwordgroup" label="Password:" label-for="password">
                        <b-input-group>
                            <b-input-group-prepend>
                                <span class="input-group-text"><v-icon scale="1.5" name="lock" /></span>
                            </b-input-group-prepend>
                            <b-form-input id="password"
                                          v-model="form.password"
                                          name="password"
                                          :state="getValidationState(validationContext)"
                                          aria-describedby="input-password-live-feedback"
                                          placeholder="inserisci password"></b-form-input>
                            <b-form-invalid-feedback id="input-password-live-feedback">{{ validationContext.errors[0] }}</b-form-invalid-feedback>
                        </b-input-group>                         
                    </b-form-group>
                </validation-provider>

                <validation-provider vid="confirmPassword"
                                     :rules="{required:true,min:8,max:16, regex: /^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#\$%\^&\*])(?=.{8,})/}"
                                     name="conferma password"
                                     v-slot="validationContext">
                    <b-form-group id="confirmpasswordgroup" label="Conferma Password:" label-for="confirm">
                        <b-input-group>
                            <b-input-group-prepend>
                                <span class="input-group-text"><v-icon scale="1.5" name="lock" /></span>
                            </b-input-group-prepend>
                            <b-form-input id="confirm"
                                          v-model="form.confirmPassword"
                                          name="confirmPassword"
                                          :state="getValidationState(validationContext)"
                                          aria-describedby="input-confirmpassword-live-feedback"
                                          placeholder="inserisci conferma password"></b-form-input>
                            <b-form-invalid-feedback id="input-confirmpassword-live-feedback">{{ validationContext.errors[0] }}</b-form-invalid-feedback>
                        </b-input-group>                       
                    </b-form-group>
                </validation-provider>

                <b-button type="submit" variant="primary">Conferma</b-button>
            </b-form>
        </validation-observer>
    </div>
</template>


<script>

    import { extend } from 'vee-validate';
    extend('password', {
        params: ['target'],
        validate(value, { target }) {
            return value === target;
        },
        message: 'La conferma password non coincide'
    });  

    export default {
        name: 'formuser',
        namespaced: true,
        components: {
        },      
        data: function () {
            return {
                form: {},
                show: true,
                messaggio: ''
            }
        },       
        methods: {
            getValidationState({ dirty, validated, valid = null }) {
                return dirty || validated ? valid : null;
            },
            onSubmit() {
                console.log(this.form);
                console.log(this.form.password);
                this.$emit('finished', this.form);
            }
        }
    }

</script>
