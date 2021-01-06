<template>
    <div role="tablist" style="max-width: 80rem;margin-left:360px" v-if="Object.entries(scrutinio).length !== 0">
        <b-card no-body class="mb-1 border-0">
            <b-card-header header-tag="header" class="p-1 border-0" role="tab">
                <b-button block v-b-toggle.sezione variant="dark">Registrazione voti</b-button>
            </b-card-header>
            <b-collapse id="sezione" visible accordion="my-accordion" role="tabpanel">
                <fieldset>
                    <validation-observer ref="observer" v-slot="{ handleSubmit }">
                        <b-form @submit.stop.prevent="handleSubmit(onSubmit)">
                            <b-form-input type="text" v-model="scrutinio.id"
                                          style="display:none" />
                            <div class="row" style="margin-bottom: 15px;margin-top:10px">
                                <div class="col col-xs-3">
                                    <div class="input-group">
                                        <div class="input-group-prepend">
                                            <span class="input-group-text">A</span>
                                        </div>
                                        <validation-provider vid="valideListe"
                                                             name="valideListe"
                                                             :rules="{required:true,between:[scrutinio.totaleValide,scrutinio.Votanti],min:0}"
                                                             v-slot="validationContext">
                                            <b-form-input type="number"
                                                          v-model="scrutinio.valideListe"
                                                          :state="getValidationState(validationContext)"
                                                          placeholder="Voti Validi Liste">
                                            </b-form-input>
                                            <b-form-invalid-feedback id="input-valideListe-live-feedback">{{ validationContext.errors[0] }}</b-form-invalid-feedback>
                                        </validation-provider>
                                    </div>
                                </div>
                                <div class="col col-xs-3">
                                    <div class="input-group">
                                        <div class="input-group-prepend">
                                            <span class="input-group-text">B</span>
                                        </div>
                                        <validation-provider vid="soloSindaco"
                                                             name="soloSindaco"
                                                             :rules="{required:true,min:0,max:scrutinio.Votanti}"
                                                             v-slot="validationContext">
                                            <b-form-input type="number"
                                                          v-model="scrutinio.soloSindaco"
                                                          placeholder="Solo Sindaco">
                                            </b-form-input>
                                            <b-form-invalid-feedback id="input-soloSindaco-live-feedback">{{ validationContext.errors[0] }}</b-form-invalid-feedback>
                                        </validation-provider>
                                    </div>
                                </div>
                                <div class="col col-xs-3">
                                    <div class="input-group">
                                        <div class="input-group-prepend">
                                            <span class="input-group-text">C</span>
                                        </div>
                                        <validation-provider vid="totaleValide"
                                                             name="totaleValide"
                                                             :rules="{required:true,between:[scrutinio.totaleValide,scrutinio.Votanti]}"
                                                             v-slot="validationContext">
                                            <b-form-input type="number"
                                                          v-model="scrutinio.totaleValide"
                                                          placeholder="Valide Sindaco (B+C)">
                                            </b-form-input>
                                            <b-form-invalid-feedback id="input-totaleValide-live-feedback">{{ validationContext.errors[0] }}</b-form-invalid-feedback>
                                        </validation-provider>
                                    </div>
                                </div>
                                <div class="col col-xs-3">
                                    <div class="input-group">
                                        <div class="input-group-prepend">
                                            <span class="input-group-text">D</span>
                                        </div>
                                        <validation-provider vid="bianche"
                                                             name="bianche"
                                                             :rules="{required:true, min:0,max:scrutinio.Votanti}"
                                                             v-slot="validationContext">
                                            <b-form-input type="number"
                                                          v-model="scrutinio.bianche"
                                                          placeholder="Bianche">
                                            </b-form-input>
                                            <b-form-invalid-feedback id="input-bianche-live-feedback">{{ validationContext.errors[0] }}</b-form-invalid-feedback>
                                        </validation-provider>
                                    </div>
                                </div>
                            </div>
                            <div class="row" style="margin-bottom: 15px">
                                <div class="col col-xs-3">
                                    <div class="input-group">
                                        <div class="input-group-prepend">
                                            <span class="input-group-text">E</span>
                                        </div>
                                        <validation-provider vid="nulle"
                                                             name="nulle"
                                                             :rules="{required:true,min:0,max:scrutinio.Votanti}"
                                                             v-slot="validationContext">
                                            <b-form-input type="number"
                                                          v-model="scrutinio.nulle"
                                                          placeholder="Nulle">
                                            </b-form-input>
                                            <b-form-invalid-feedback id="input-nulle-live-feedback">{{ validationContext.errors[0] }}</b-form-invalid-feedback>
                                        </validation-provider>
                                    </div>
                                </div>
                                <div class="col col-xs-3">
                                    <div class="input-group">
                                        <div class="input-group-prepend">
                                            <span class="input-group-text">F</span>
                                        </div>
                                        <validation-provider vid="contestate"
                                                             name="contestate"
                                                             :rules="{required:true,min:0,max:scrutinio.Votanti}"
                                                             v-slot="validationContext">
                                            <b-form-input type="number"
                                                          v-model="scrutinio.contestate"
                                                          placeholder="Contestate">
                                            </b-form-input>
                                            <b-form-invalid-feedback id="input-contestate-live-feedback">{{ validationContext.errors[0] }}</b-form-invalid-feedback>
                                        </validation-provider>
                                    </div>
                                </div>
                                <div class="col col-xs-3">
                                    <div class="input-group">
                                        <div class="input-group-prepend">
                                            <span class="input-group-text">G</span>
                                        </div>
                                        <validation-provider vid="totale"
                                                             name="totale"
                                                             :rules="{required:true,min:0,max:scrutinio.Votanti}"
                                                             v-slot="validationContext">
                                            <b-form-input type="number"
                                                          v-model="scrutinio.totale"
                                                          placeholder="Totale voti (C+D+E+F)">
                                            </b-form-input>
                                            <b-form-invalid-feedback id="input-totale-live-feedback">{{ validationContext.errors[0] }}</b-form-invalid-feedback>
                                        </validation-provider>
                                    </div>
                                </div>
                                <div class="col col-xs-3">
                                    <div class="input-group">
                                        <div class="input-group-prepend">
                                            <span class="input-group-text">Votanti</span>
                                        </div>
                                        <b-form-input type="number"
                                                      v-model="scrutinio.votanti"
                                                      readonly />

                                    </div>
                                </div>
                            </div>
                            <div v-bind:key="data.index" v-for="(data,index) in scrutinio.sindaci" class="form-group row">
                                <div class="col-sm-12">
                                    <b-form-input type="number"
                                                  style="display:none;"
                                                  v-model="data.numeroSezione" />
                                    <b-form-input type="text"
                                                  style="display:none;"
                                                  v-model="data.tipo" />
                                    <b-form-input name="id" type="number"
                                                  v-model="data.id"
                                                  style="display:none;" />
                                    <b-form-input type="text"
                                                  style="display:none;"
                                                  v-model="data.isCoalizione" />
                                    <b-form-input name="id" type="text"
                                                  v-model="data.idCoalizione"
                                                  style="display:none;" />
                                    <b-form-input type="number"
                                                  style="display:none;"
                                                  v-model="data.idSindaco" />
                                    <b-input-group class="mb-2">
                                        <b-input-group-prepend style="background-color: transparent;border: none;font-size: 2.7rem">
                                            <b-icon icon="check-circle-fill"                                                     
                                                    class="rounded-circle bg-danger p-2" variant="light"
                                                    @click="showModal(data)"  
                                                    :id="'votiCoalizione[' + index + ']'"
                                                    aria-hidden="true"></b-icon>
                                            <b-tooltip :target="'votiCoalizione[' + index + ']'" triggers="hover">
                                                voti coalizione
                                            </b-tooltip>
                                        </b-input-group-prepend>
                                        <label class="col-sm-3  col-form-label">
                                            {{ data.nome}} {{data.cognome}}
                                        </label>
                                        <validation-provider vid="datatotale"
                                                             name="datatotale"
                                                             :rules="{required:true,min:1,max:4}"
                                                             v-slot="validationContext">
                                            <b-form-input v-model="data.totale"
                                                          placeholder="Totale voti"
                                                          type="number" />
                                            <b-form-invalid-feedback id="input-datatotale-live-feedback">{{ validationContext.errors[0] }}</b-form-invalid-feedback>
                                        </validation-provider>
                                        <validation-provider vid="dataSoloSindaco"
                                                             name="dataSoloSindaco"
                                                             :rules="{required:true,min:1,max:4}"
                                                             v-slot="validationContext">
                                            <b-form-input placeholder="Solo Sindaco"
                                                          type="number"
                                                          v-model="data.soloSindaco" />
                                            <b-form-invalid-feedback id="input-dataSoloSindaco-live-feedback">{{ validationContext.errors[0] }}</b-form-invalid-feedback>
                                        </validation-provider>
                                    </b-input-group>
                                </div>
                            </div>
                        </b-form>
                    </validation-observer>
                </fieldset>
            </b-collapse>
        </b-card>
        <!-- Info modal -->
        <b-modal :id="coalizioneModal.id" :title="coalizioneModal.title" style="font-size:1.5rem" ok-only @hide="resetCoalizioneModal">
            <b-container fluid>
                <span>
                    <b-badge href="#" variant="dark">
                        <v-icon name="id-badge"></v-icon>
                        Denominazione: {{coalizioneModal.denominazione}}
                    </b-badge>
                </span><br />
                <span>
                    <b-badge href="#" variant="dark">
                        <v-icon name="user"></v-icon>
                        Voti: {{coalizioneModal.voti}}
                    </b-badge>
                </span><br />
                <span>
                    <b-badge href="#" variant="dark">
                        <v-icon name="user-circle"></v-icon>IdLista: {{coalizioneModal.IdLista}}
                    </b-badge>
                </span><br />
                <span>
                    <b-badge href="#" variant="dark">
                        <v-icon name="user-circle"></v-icon>
                        IdSindaco: {{coalizioneModal.IdSindaco}}
                    </b-badge>
                </span><br />
                <span>
                    <b-badge href="#" variant="dark">
                        <v-icon name="at"></v-icon>
                        IdVoti: {{coalizioneModal.IdVoti}}
                    </b-badge>
                </span><br />
            </b-container>
        </b-modal>
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
        validate: (value, { otherValue, maxDifference }) => {
            var sum = parseInt(otherValue) + parseInt(maxDifference);
            var v = parseInt(value);
            if (sum !== v) { return false; }
            else { return true; }
        },
        message:
            'La somma dei votanti maschi e femmine deve essere uguale ai votanti totali '
    });

    export default {
        name: 'scrutinioaw',
        props: ['tipo', 'scrutinio'],
        data() {
            return {
                coalizioneModal: {
                    id: 'coalizione-modal',
                    title: '',
                    IdVoti: '',
                    Denominazione: '',
                    Voti: '',
                    IdLista: '',
                    IdSindaco: ''
                }
            }
        },
        methods: {
            toWatch: function (tipoev, event) {
                this.$emit('watchbutton', tipoev);
            },
            getValidationState({ dirty, validated, valid = null }) {
                return dirty || validated ? valid : null;
            },
            onSubmit() {
                console.log(this.scrutinio);
                this.$emit('postscrutinio', this.scrutinio);
            },
            showModal(item, button) {
                this.coalizioneModal.IdVoti = item.id;
                this.coalizioneModal.Denominazione = item.denominazione;
                this.coalizioneModal.Voti = item.voti;
                this.coalizioneModal.IdLista = item.idLista;
                this.coalizioneModal.IdSindaco = item.idSindaco;
                this.$root.$emit('bv::show::modal', this.coalizioneModal.id, button)
            },
            resetCoalizioneModal() {
                this.infoModal.title = ''
                this.infoModal.content = ''
            },
        }
    };
</script>