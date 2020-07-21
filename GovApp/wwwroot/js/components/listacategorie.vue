<template>
    <div class="container" style="position: relative;overflow: auto;">
        <validation-observer ref="observer" v-slot="{ handleSubmit }">
            <validation-provider vid="abilitazioni"
                                 name="abilitazioni"
                                 :rules="{required:true}"
                                 v-slot="validationContext">
                <b-form-group id="abilitazionigroup"
                              label="Tipo Andamento:"
                              label-for="abilitazioni">
                    <b-form-select v-model="selected"
                                   v-on:change="getSelectedItem"
                                   :options="tipologiche"
                                   class="mb-3"
                                   id="abilitazioni"
                                   value-field="codice"
                                   text-field="descrizione"
                                   :state="getValidationState(validationContext)"
                                   aria-describedby="input-abilitazioni-live-feedback"
                                   disabled-field="notEnabled">
                    </b-form-select>
                    </b-form-group>
                    <b-form-invalid-feedback id="input-abilitazioni-live-feedback">{{ validationContext.errors[0] }}</b-form-invalid-feedback>
            </validation-provider>
            </validation-observer>
            <notifications position="top center" group="errori" />
    </div>   
</template>
<script>
    export default {
        name: 'listacategorie',
        namespaced: true,
        props: ["categoria"],
        components: {
        },
        data: function () {
            return {
                tipo: {},
                selected: null,
                tipologiche: null,
                show: true,
                messaggio: ''
            }
        },
        methods: {
            getSelectedItem: function (myarg) { // Just a regular js function that takes 1 arg
                this.$emit('selection', myarg);
            },
            getValidationState({ dirty, validated, valid = null }) {
                return dirty || validated ? valid : null;
            },
            getFasi() {
                axios({
                    method: 'get',
                    url: '/GovApp/values/categories',
                    params: {
                        "categoria": this.categoria
                    }
                })
                    .then(response => {
                        this.tipologiche = response.data;
                    })
                    .catch(error => {
                        error => this.messaggio = error.response.statusText;
                        this.showAlert(error.response.statusText);
                        console.log(error.response.data);
                        console.log(error.response.status);
                        console.log(error.response.headers);
                    });
            },
            showAlert(message) {
                this.$notify({
                    group: 'errori',
                    position: "top center",
                    duration: "15000",
                    width: "450px",
                    type: "error",
                    title: 'Attenzione',
                    text: message
                })
            }
        },
        mounted() {
            this.getFasi();
        }
    }


</script>