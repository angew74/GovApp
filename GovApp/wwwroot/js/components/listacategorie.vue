﻿<template>
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
                        <b-form-invalid-feedback id="input-abilitazioni-live-feedback">{{ validationContext.errors[0] }}</b-form-invalid-feedback>
                    </b-form-group>
                </validation-provider>
            </validation-observer>      
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
                loading:false,
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
                        this.loading = false;
                    })
                    .catch(error => {
                        error => this.messaggio = error.response.statusText;
                        this.showSweetAlert(error.response.statusText);
                        console.log(error.response.data);
                        console.log(error.response.status);
                        console.log(error.response.headers);     
                        this.loading = false;
                    });
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
        },
        mounted() {
            this.loading = true;
            //this.getFasi();      
            axios({
                method: 'get',
                url: '/GovApp/values/categories',
                params: {
                    "categoria": this.categoria
                }
            })
                .then(response => {
                    this.tipologiche = response.data;
                    this.loading = false;
                })
                .catch(error => {
                    error => this.messaggio = error.response.statusText;
                    this.showSweetAlert(error.response.statusText);
                    console.log(error.response.data);
                    console.log(error.response.status);
                    console.log(error.response.headers);
                    this.loading = false;
                });
        }
    }


</script>