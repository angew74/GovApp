<template>
    <div>
        <div v-if="tipo === 'CO' || tipo=== 'AP'" style="max-width: 80rem;margin-left:720px">
            <b-button variant="light" style="font-size: 1.7rem;background-color:transparent" v-on:click="toWatch(tipo, $event)" class="border-0">
                <b-icon icon="check-circle-fill" class="rounded-circle bg-danger p-2" variant="light"></b-icon> Conferma
            </b-button>
        </div>
        <div class="container" v-if="tipo === '1A' || tipo === '2A' || tipo === '3C'" style="max-width: 80rem;margin-left:360px">
            <validation-observer ref="observer" v-slot="{ handleSubmit }">
                <b-form @submit.stop.prevent="handleSubmit(onSubmit)">

                    <div v-if="tipo === '2A' || tipo === '3C'">
                        <b-badge variant="info">
                            Votanti Maschi precedenti {{affluenzasezione.votantiMaschiAffP}}
                        </b-badge>
                    </div>
                    <validation-provider vid="votantiMaschi"
                                         name="votantiMaschi"
                                         :rules="{required:true,between:[affluenzasezione.votantiMaschiAffP,affluenzasezione.iscrittiMaschi],min:1,max:4}"
                                         v-slot="validationContext">
                        <b-form-group id="votantiMaschigroup"
                                      label="Votanti Maschi:"
                                      label-for="votantiMaschi">
                            <b-form-input id="votantiMaschi"
                                          v-model="affluenzasezione.votantiMaschi"
                                          type="number"
                                          placeholder="Votanti Maschi"
                                          :state="getValidationState(validationContext)"
                                          aria-describedby="input-votantiMaschi-live-feedback">
                            </b-form-input>
                            <b-form-invalid-feedback id="input-votantiMaschi-live-feedback">{{validationContext.errors[0]}}</b-form-invalid-feedback>
                        </b-form-group>
                    </validation-provider>
                    <div v-if="tipo === '2A' || tipo === '3C'">
                        <b-badge variant="info">
                            Votanti Femmine precedenti {{affluenzasezione.votantiFemmineAffP}}
                        </b-badge>
                    </div>
                    <validation-provider vid="votantiFemmine"
                                         name="votantiFemmine"
                                         :rules="{required:true,between:[affluenzasezione.votantiFemmineAffP,affluenzasezione.iscrittiFemmine],min:1,max:4}"
                                         v-slot="validationContext">
                        <b-form-group id="votantiFemminegroup"
                                      label="Votanti Femmine:"
                                      label-for="votantiFemmine">
                            <b-form-input id="votantiFemmine"
                                          v-model="affluenzasezione.votantiFemmine"
                                          type="number"
                                          placeholder="Votanti Femmine"
                                          :state="getValidationState(validationContext)"
                                          aria-describedby="input-votantiFemmine-live-feedback">
                            </b-form-input>
                            <b-form-invalid-feedback id="input-votantiFemmine-live-feedback">{{ validationContext.errors[0] }}</b-form-invalid-feedback>
                        </b-form-group>
                    </validation-provider>
                    <div v-if="tipo === '2A' || tipo === '3C'">
                        <b-badge variant="info">
                            Votanti Totali precedenti {{affluenzasezione.votantiTotaliAffP}}
                        </b-badge>
                    </div>
                    <validation-provider vid="votantiTotali"
                                         name="votantiTotali"
                                         :rules="{required:true,between:[affluenzasezione.votantiTotaliAffP,affluenzasezione.iscrittiTotali],maxDifference:[affluenzasezione.votantiFemmine,affluenzasezione.votantiMaschi], min:1,max:4}"
                                         v-slot="validationContext">
                        <b-form-group id="votantiTotaligroup"
                                      label="Votanti Totali:"
                                      label-for="votantiTotali">
                            <b-form-input id="votantiTotali"
                                          v-model="affluenzasezione.votantiTotali"
                                          type="number"
                                          placeholder="Votanti Totali"
                                          :state="getValidationState(validationContext)"
                                          aria-describedby="input-votantiTotali-live-feedback">
                            </b-form-input>
                            <b-form-invalid-feedback id="input-votantiTotali-live-feedback">{{ validationContext.errors[0] }}</b-form-invalid-feedback>
                        </b-form-group>
                        <ValidationProvider name="iscrittiMaschi" rules="required">
                            <b-form-input id="iscrittiMaschi"
                                          v-model="affluenzasezione.iscrittiMaschi"
                                          type="text"
                                          style="display:none"></b-form-input>
                        </ValidationProvider>
                        <ValidationProvider name="iscritti Femmine" rules="required">
                            <b-form-input id="iscrittiFemmine"
                                          v-model="affluenzasezione.iscrittiFemmine"
                                          type="text"
                                          style="display:none"></b-form-input>
                        </ValidationProvider>
                        <ValidationProvider name="iscrittiTotali" rules="required">
                            <b-form-input id="iscrittiTotali"
                                          v-model="affluenzasezione.iscrittiTotali"
                                          type="text"
                                          style="display:none"></b-form-input>
                        </ValidationProvider>
                        <ValidationProvider name="votanti Maschi precedenti " rules="required">
                            <b-form-input id="votantiMaschiAffP"
                                          v-model="affluenzasezione.votantiMaschiAffP"
                                          type="text"
                                          style="display:none"></b-form-input>
                        </ValidationProvider>
                        <ValidationProvider name="votanti Femmine Precedenti" rules="required">
                            <b-form-input id="votantiFemmineAffP"
                                          v-model="affluenzasezione.votantiFemmineAffP"
                                          type="text"
                                          style="display:none"></b-form-input>
                        </ValidationProvider>
                        <ValidationProvider name="votantiTotaliPrecedenti" rules="required">
                            <b-form-input id="votantiTotaliAffP"
                                          v-model="affluenzasezione.votantiTotaliAffP"
                                          type="text"
                                          style="display:none"></b-form-input>
                        </ValidationProvider>
                        <b-form-input id="numerosezione"
                                      v-model="affluenzasezione.numeroSezione"
                                      type="text"
                                      style="display:none"></b-form-input>
                        <b-form-input id="tipoaffluenza"
                                      v-model="affluenzasezione.tipo"
                                      type="text"
                                      style="display:none"></b-form-input>
                    </validation-provider>
                    <b-button type="submit" style="margin-top: 30px;margin: 0 50%;position: relative;left: -50px;" variant="primary">Conferma</b-button>
                </b-form>
            </validation-observer>
        </div>
    </div>
</template>
<script>
    import { extend } from 'vee-validate';
  
    extend('between', {
        params: ['min', 'max'],
        validate(value, { min, max }) {
            return value >= min && value <= max;
        },
        message: 'Il valore del campo deve essere compreso tra {min} e {max}'
    });

    extend('maxDifference', {
        params: ['otherValue', 'maxDifference'],
        validate: (value, { otherValue, maxDifference }) =>
        {
            var sum = parseInt(otherValue) + parseInt(maxDifference);
            var v = parseInt(value);
            if (sum !== v) { return false; }
            else { return true; }          
        },
        message:
            'La somma dei votanti maschi e femmine deve essere uguale ai votanti totali '
    });

    export default {
        name: 'anda',
        props: ['tipo', 'affluenzasezione'],
        methods: {
            toWatch: function (tipoev, event) {
                this.$emit('watchbutton', tipoev);
            },
            getValidationState({ dirty, validated, valid = null }) {
                return dirty || validated ? valid : null;
            },
            onSubmit() {
                console.log(this.affluenzasezione);
                this.$emit('postanda', this.affluenzasezione);
            }
        }
    };
</script>