<template>
    <div role="tablist" style="max-width: 80rem;margin-left:360px" v-if="Object.entries(scrutinio).length !== 0">
        <b-card no-body class="mb-1 border-0">
            <b-card-header header-tag="header" class="p-1 border-0" role="tab">
                <b-button block v-b-toggle.scrutinio variant="dark">Registrazione voti</b-button>
            </b-card-header>
            <b-collapse id="scrutinio" visible accordion="my-accordion" role="tabpanel">
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
                                                             name="voti liste"
                                                             :rules="{required:true,min:0,max:scrutinio.votanti}"
                                                             v-slot="validationContext">
                                            <b-form-input type="number"
                                                          v-model="scrutinio.valideListe"
                                                          aria-describedby="input-valideListe-live-feedback"
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
                                                             name="solo sindaco"
                                                             :rules="{required:true,min:0,max:scrutinio.votanti}"
                                                             v-slot="validationContext">
                                            <b-form-input type="number"
                                                          v-model="scrutinio.soloSindaco"
                                                          aria-describedby="input-soloSindaco-live-feedback"
                                                          :state="getValidationState(validationContext)"
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
                                                             name="totale sindaco"
                                                             :rules="{required:true,min:0,max:scrutinio.votanti}"
                                                             v-slot="validationContext">
                                            <b-form-input type="number"
                                                          v-model="scrutinio.totaleValide"
                                                          aria-describedby="input-totaleValide-live-feedback"
                                                          :state="getValidationState(validationContext)"
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
                                                             :rules="{required:true,min:0,max:scrutinio.votanti}"
                                                             v-slot="validationContext">
                                            <b-form-input type="number"
                                                          v-model="scrutinio.bianche"
                                                          aria-describedby="input-bianche-live-feedback"
                                                          :state="getValidationState(validationContext)"
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
                                                             :rules="{required:true,min:0,max:scrutinio.votanti}"
                                                             v-slot="validationContext">
                                            <b-form-input type="number"
                                                          v-model="scrutinio.nulle"
                                                          aria-describedby="input-nulle-live-feedback"
                                                          :state="getValidationState(validationContext)"
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
                                                             :rules="{required:true,min:0,max:scrutinio.votanti}"
                                                             v-slot="validationContext">
                                            <b-form-input type="number"
                                                          v-model="scrutinio.contestate"
                                                          aria-describedby="input-contestate-live-feedback"
                                                          :state="getValidationState(validationContext)"
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
                                                             :rules="{required:true,min:0,max:scrutinio.votanti}"
                                                             v-slot="validationContext">
                                            <b-form-input type="number"
                                                          v-model="scrutinio.totale"
                                                          aria-describedby="input-totale-live-feedback"
                                                          :state="getValidationState(validationContext)"
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
                            <div v-bind:key="data.index" v-for="(data,index) in scrutinio.sindaci" class="form-group">
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
                                              :ref="'isCoalizione'+ data.idSindaco"
                                              v-model="data.isCoalizione" />
                                <b-form-input name="id" type="text"
                                              v-model="data.idCoalizione"
                                              style="display:none;" />
                                <b-form-input type="number"
                                              style="display:none;"
                                              v-model="data.idSindaco" />
                                <div class="row" style="width:100%">
                                    <div class="col">
                                        <label class=" col-form-label">
                                            <v-icon name="check-circle" :class="(data.soloListe !== '' ) ? 'fonticongreen' : 'fonticonred'" @click="showModal(data,index)" style="margin-top:7px" scale="1.5" />
                                            {{ data.nome}} {{data.cognome}}
                                        </label>
                                    </div>
                                    <div class="col">
                                        <validation-provider vid="totale"
                                                             name="totale"
                                                             :rules="{required:true,min:0,max:scrutinio.totale}"
                                                             v-slot="validationContext">
                                            <b-form-input v-model="data.totale"
                                                          placeholder="Totale voti"
                                                          class="form-control"
                                                          :state="getValidationState(validationContext)"
                                                          type="number" />
                                            <b-form-invalid-feedback id="input-datatotale-live-feedback">{{ validationContext.errors[0] }}</b-form-invalid-feedback>
                                        </validation-provider>
                                    </div>
                                    <div class="col">
                                        <validation-provider vid="dataSoloSindaco"
                                                             name="solo sindaco"
                                                             :rules="{required:true,min:0,max:scrutinio.totale}"
                                                             v-slot="validationContext">
                                            <b-form-input placeholder="Solo Sindaco"
                                                          type="number"
                                                          class="form-control"
                                                          :state="getValidationState(validationContext)"
                                                          aria-describedby="input-dataSoloSindaco-live-feedback"
                                                          v-model="data.soloSindaco" />
                                            <b-form-invalid-feedback id="input-dataSoloSindaco-live-feedback">{{ validationContext.errors[0] }}</b-form-invalid-feedback>
                                        </validation-provider>
                                    </div>
                                    <div class="col">
                                        <b-form-input name="soloListe" type="text"
                                                      v-model="data.soloListe"
                                                      placeholder="totale liste"
                                                      class="form-control"
                                                      readonly />
                                    </div>
                                </div>
                            </div>
                            <div style="margin-top:20px;text-align:right">
                                <b-button type="submit" variant="primary">Conferma</b-button>
                            </div>
                        </b-form>
                    </validation-observer>
                </fieldset>               
            </b-collapse>
        </b-card>
        <!-- Info modal -->
        <app-modal :coalizione="datavoti" :show="showvoti" @cancel="cancelliste" @save="saveliste"></app-modal>
    </div>
</template>
<script>
    import { extend } from 'vee-validate';
    import modalvotiaw from '../components/modalvotiaw';

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
            'La somma dei voti solo sindaco e delle liste deve esssere uguale ai voti totali '
    });

    export default {
        name: 'scrutinioaw',
        props: ['tipo', 'scrutinio'],
        components: {
            'app-modal': modalvotiaw,
        },
        data() {
            return {
                showvoti: false,
                datavoti: {},
                currentIndex: 0,
            }
        },
        watch: {
        },
        methods: {
            getValidationState({ dirty, validated, valid = null }) {
                return dirty || validated ? valid : null;
            },
            saveliste(e) {
                var totale = 0;
                var appo = 0;
                this.scrutinio.sindaci[this.currentIndex]
                for (var i = 0; i < this.scrutinio.sindaci[this.currentIndex].liste.length; i++) {
                    this.scrutinio.sindaci[this.currentIndex].liste[i].voti = e.liste[i].voti;
                    appo = totale;
                    totale = parseInt(e.liste[i].voti) + parseInt(appo);
                    this.scrutinio.sindaci[this.currentIndex].liste[i].isCoalizione = 'S';
                    this.isCoalizione = 'S';
                }
                this.scrutinio.sindaci[this.currentIndex].soloListe = totale;
                this.showvoti = false;
            },
            cancelliste() {
                this.showvoti = false;
            },
            onSubmit() {
                var mess = this.checkSum();
                if (mess !== '') {
                    this.showSweetAlert(mess);
                }
                console.log(this.scrutinio);
                this.$emit('postscrutinio', this.scrutinio);
            },
            checkSum() {
                var messaggio = '';
                var totaleListe = 0;
                var totaleSindaco = 0;
                var totale = parseInt(this.scrutinio.nulle) + parseInt(this.scrutinio.bianche) + parseInt(this.scrutinio.contestate) + parseInt(this.scrutinio.totaleValide);
                if (totale !== parseInt(this.scrutinio.totale)) {
                    messaggio = "Totale G (" + this.scrutinio.totale + ") diverso dalla somma di C + D + E + F (" + totale + ")";
                    return messaggio;
                }
                if (parseInt(this.scrutinio.totale) !== parseInt(this.scrutinio.votanti)) {
                    messaggio = "Totale G (" + this.scrutinio.totale + ") diverso dai votanti (" + this.scrutinio.votanti + ")";
                    return messaggio;
                }
                for (var i = 0; i < this.scrutinio.sindaci.length; i++) {
                    var appoSindaco = totaleSindaco;
                    var appoListe = totaleListe;
                    var numSindaco = parseInt(this.scrutinio.sindaci[i].soloSindaco);
                    var numListe = parseInt(this.scrutinio.sindaci[i].soloListe);
                    totaleSindaco = parseInt(numSindaco) + parseInt(appoSindaco);
                    totaleListe = parseInt(numListe) + parseInt(appoListe);
                }
                if (parseInt(this.scrutinio.soloSindaco) !== totaleSindaco) {
                    messaggio = "Totale solo sindaco (" + this.scrutinio.soloSindaco + ") diverso da somme singoli candidati (" + totaleSindaco +")";
                    return messaggio;
                }
                if (parseInt(this.scrutinio.valideListe) !== totaleListe) {
                    messaggio = "Totale liste (" + this.scrutinio.valideListe + ") diverso da somme singoli raggruppamenti (" + totaleListe + ")";
                    return messaggio;
                }
                return messaggio;

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
            showModal(item, index) {
                this.datavoti = {};
                this.currentIndex = index,
                    this.showvoti = true;
                this.datavoti = item;
            },
        }
    };
</script>