<template>
    <div class="container">
        <validation-observer ref="observer" v-slot="{ handleSubmit }">
            <b-form v-if="show" @submit.stop.prevent="handleSubmit(onSubmit)">
                <validation-provider vid="sezione"
                                     name="sezione"
                                     :rules="{required:true,min:1,max:4}"
                                     v-slot="validationContext">
                    <b-form-group id="sezionegroup"
                                  label="Numero Sezione:"
                                  label-for="sezione">
                        <b-input-group>
                            <b-input-group-prepend>
                                <span class="input-group-text"><v-icon scale="1.5" name="person-booth" /></span>
                            </b-input-group-prepend>
                            <b-form-input id="sezione"
                                          v-model="form.sezione"
                                          type="number"
                                          placeholder="numero della sezione da ricercare"
                                          :state="getValidationState(validationContext)"
                                          aria-describedby="input-sezione-live-feedback">
                            </b-form-input>
                            <b-form-invalid-feedback id="input-sezione-live-feedback">{{ validationContext.errors[0] }}</b-form-invalid-feedback>
                        </b-input-group>                                                   
                    </b-form-group>
                </validation-provider>
                <validation-provider vid="cabina"
                                     name="cabina"
                                     :rules="{required:true,min:1,max:4}"
                                     v-slot="validationContext">
                    <b-form-group id="cabinagroup" label="Cabina:" label-for="cabina">
                        <b-input-group>
                            <b-input-group-prepend>
                                <span class="input-group-text"><v-icon scale="1.5" name="person-booth" /></span>
                            </b-input-group-prepend>
                            <b-form-input id="cabina"
                                          v-model="form.cabina"
                                          type="number"
                                          placeholder="numero della cabina da ricercare"
                                          :state="getValidationState(validationContext)"
                                          aria-describedby="input-cabina-live-feedback">
                            </b-form-input>
                            <b-form-invalid-feedback id="input-cabina-live-feedback">{{ validationContext.errors[0] }}</b-form-invalid-feedback>
                        </b-input-group>                            
                    </b-form-group>
                </validation-provider>
                <b-form-input id="tipo"
                              v-model="form.tipo"
                              type="text"
                              style="display:none"></b-form-input>
                <b-button type="submit" variant="primary">Ricerca</b-button>
            </b-form>
        </validation-observer>
    </div>
</template>

<script>
    import { extend } from 'vee-validate';
    export default {
        name: 'research',
        namespaced: true,
        components: {
        },      
        props: ['form'],
        data: function () {
            return {              
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
                this.$emit('research', this.form);
            }
        }
    }
    </script>