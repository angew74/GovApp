<template>
    <div class="container" style="position: relative;overflow: auto;">
        <validation-observer ref="observer" v-slot="{ handleSubmit }">
            <b-form v-if="show" @submit.stop.prevent="handleSubmit(onSubmit)">
                <validation-provider vid="sindaco"
                                     name="sindaco"
                                     :rules="{required:true}"
                                     v-slot="validationContext">
                    <b-form-group id="sindacogroup"
                                  label="Sindaco:"
                                  label-for="sindaco">
                        <b-form-select v-model="form.idsindaco"
                                       :options="tipologiche"
                                       class="mb-3"
                                       id="sindaco"
                                       value-field="codice"
                                       text-field="descrizione"
                                       :state="getValidationState(validationContext)"
                                       aria-describedby="input-liste-live-feedback"
                                       disabled-field="notEnabled">
                        </b-form-select>
                        <b-form-invalid-feedback id="input-liste-live-feedback">{{ validationContext.errors[0] }}</b-form-invalid-feedback>
                    </b-form-group>
                </validation-provider>
                <div style="margin-top:10px;text-align:right">
                    <b-button type="submit" variant="primary">Ricerca</b-button>
                </div>
            </b-form>
        </validation-observer>
    </div>
</template>
<script>
    export default {
        name: 'sindacoaw',
        namespaced: true,
        props: ["form"],
        components: {
        },
        data: function () {
            return {
                tipologiche: null,
                show: true,
                loading:false,
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
                url: '/GovApp/values/sindaco',
                params: {
                    "elezione": this.elezione
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