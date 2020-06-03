<template>
    <div class="container" style="margin-top:140px;">
        <validation-observer ref="observer" v-slot="{ handleSubmit }">
            <b-form v-if="show" @submit.stop.prevent="handleSubmit(onSubmit)">

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
                <validation-provider  vid="password"     
                                     name="password"
                                     :rules="{required:true,password:form.confirmPassword,min:8,max:16}"
                                     v-slot="validationContext">
                    <b-form-group id="passwordgroup" label="Password:" label-for="password">
                        <b-form-input id="password"                                     
                                      v-model="form.password"     
                                      name="password"
                                      :state="getValidationState(validationContext)"
                                      aria-describedby="input-password-live-feedback"                                     
                                      placeholder="inserisci password"></b-form-input>
                        <b-form-invalid-feedback id="input-password-live-feedback">{{ validationContext.errors[0] }}</b-form-invalid-feedback>
                    </b-form-group>
                </validation-provider>
                <validation-provider vid="confirmPassword"                                     
                                     :rules="{required:true,min:8,max:16}"
                                     name="conferma password"
                                     v-slot="validationContext">
                    <b-form-group id="confirmpasswordgroup" label="Conferma Password:" label-for="confirm">
                        <b-form-input id="confirm"
                                      v-model="form.confirmPassword"  
                                      name="confirmPassword"
                                      :state="getValidationState(validationContext)"
                                      aria-describedby="input-confirmpassword-live-feedback"                                     
                                      placeholder="inserisci conferma password"></b-form-input>
                        <b-form-invalid-feedback id="input-confirmpassword-live-feedback">{{ validationContext.errors[0] }}</b-form-invalid-feedback>
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
        name: 'formemailaw',
        namespaced: true,
        components: {
        },
        props: ['dati'],
        data: function () {
            return {
                form: this.dati,
                show: true,
                messaggio: ''
            }
        },
        watch: {
            dati(newVal) {
                this.form = newVal;
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
