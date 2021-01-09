<template>
    <b-modal :id="coalizioneModal.id" hide-footer :show="showmodal" centered size="lg" :title="coalizioneModal.title" style="font-size:1.5rem" @hide="resetCoalizioneModal">
        <b-container fluid>
            <validation-observer ref="observer" v-slot="{ handleSubmit }">
                <b-form @submit.stop.prevent="handleSubmit(onSubmit)">
                    <div v-bind:key="data.index" v-for="(data,index) in coalizioneModal.liste">
                        <div class="row">
                            <div class="col-sm-12">
                                <b-input-group class="mb-2">
                                    <label class="col-sm-5  col-form-label">
                                        {{data.denominazione}}
                                    </label>
                                    <validation-provider vid="voti"
                                                         name="voti"
                                                         :rules="{required:true}"
                                                         v-slot="validationContext">
                                        <b-form-input type="number"
                                                      class="mb-2 mr-sm-2 mb-sm-0"
                                                      id="votiid"
                                                      :state="getValidationState(validationContext)"
                                                      aria-describedby="input-voti-live-feedback"
                                                      v-model="data.voti" />
                                        <b-form-invalid-feedback id="input-voti-live-feedback">
                                            {{validationContext.errors[0]}}
                                        </b-form-invalid-feedback>
                                    </validation-provider>
                                    </b-input-group>
                            </div>
                            </div>
                            <b-form-input type="number"
                                          v-model="data.idLista"
                                          style="display:none"
                                          readonly />
                            <b-form-input type="number"
                                          v-model="data.idSindaco"
                                          style="display:none"
                                          readonly />
                            <b-form-input type="number"
                                          v-model="data.idVoti"
                                          style="display:none"
                                          readonly />
                        </div>
                    <div style="margin-top:20px;text-align:right">
                    <b-button type="submit" variant="primary">Conferma</b-button>
                    </div>
                </b-form>
            </validation-observer>
        </b-container>
    </b-modal>
</template>
<script>
    export default {
        name: 'modalvotiaw',
        props: ['coalizione', 'show'],
        data() {
            return {
                daticoalizione: {},
                showmodal: false,
                coalizioneModal: {
                    id: 'coalizione-modal',
                    title: '',
                    liste: []
                }
            }
        },
        mounted() {
            this.daticoalizione = this.coalizione;
            this.showmodal = this.show;
        },
        watch: {
            show(newvalue) {
                this.showmodal = newvalue;
            },
            coalizione(newobject) {
                this.daticoalizione = newobject;
                this.coalizioneModal.IdVoti = newobject.id;
                this.coalizioneModal.title = newobject.nome + ' ' + newobject.cognome;
                this.coalizioneModal.liste = [];
                if (typeof (this.coalizioneModal.liste) !== 'undefined') {
                    for (var i = 0; i < newobject.liste.length; i++) {
                        var lista = {};
                        lista.denominazione = newobject.liste[i].denominazione;
                        lista.voti = newobject.liste[i].voti;
                        lista.idLista = newobject.liste[i].idLista;
                        lista.idSindaco = newobject.liste[i].idSindaco;
                        this.coalizioneModal.liste.push(lista);
                    }
                    this.$root.$emit('bv::show::modal', this.coalizioneModal.id);
                }
            }
        },
        methods: {
            resetCoalizioneModal() {
                this.coalizioneModal.title = '';
                this.coalizioneModal.content = '';
                this.$emit('cancel');              
                this.$root.$emit('bv::hide::modal', this.coalizioneModal.id);
            },
            getValidationState({ dirty, validated, valid = null }) {
                return dirty || validated ? valid : null;
            },
            onSubmit() {
                console.log(this.coalizioneModal);
                for (var i = 0; i < this.coalizioneModal.liste.length; i++) {
                    this.coalizione.liste[i].voti = this.coalizioneModal.liste[i].voti;
                }             
                this.$emit('save', this.coalizione);
                this.$root.$emit('bv::hide::modal', this.coalizioneModal.id);
            }
        }
    }
</script>