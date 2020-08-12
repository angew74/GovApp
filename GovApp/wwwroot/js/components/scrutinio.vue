<div v-if="scrutinio.length" class="card" style="border:none">
    <div class="card-body">
        <div class="row">
            <div class="col-md-12">
                <div class="well well-sm">
                    <fieldset>
                        <validation-observer ref="observer" v-slot="{ handleSubmit }">
                            <b-form @submit.stop.prevent="handleSubmit(onSubmit)">
                                <b-form-input type="text" v-model="scrutionio.id"
                                              style="display:none" />
                                <div class="row" style="margin-bottom: 15px">
                                    <div class="col col-xs-3">
                                        <div class="input-group">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">A</span>
                                            </div>
                                            <validation-provider vid="valideListe"
                                                                 name="valideListe"
                                                                 :rules="{required:true,between:[affluenzasezione.votantiTotaliAffP,affluenzasezione.iscrittiTotali],maxDifference:[affluenzasezione.votantiFemmine,affluenzasezione.votantiMaschi], min:1,max:4}"
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
                                                                 :rules="{required:true,between:[affluenzasezione.votantiTotaliAffP,affluenzasezione.iscrittiTotali],maxDifference:[affluenzasezione.votantiFemmine,affluenzasezione.votantiMaschi], min:1,max:4}"
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
                                            <validation-provider vid="soloSindaco"
                                                                 name="soloSindaco"
                                                                 :rules="{required:true,between:[affluenzasezione.votantiTotaliAffP,affluenzasezione.iscrittiTotali],maxDifference:[affluenzasezione.votantiFemmine,affluenzasezione.votantiMaschi], min:1,max:4}"
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
                                                                 :rules="{required:true,between:[affluenzasezione.votantiTotaliAffP,affluenzasezione.iscrittiTotali],maxDifference:[affluenzasezione.votantiFemmine,affluenzasezione.votantiMaschi], min:1,max:4}"
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
                                                                 :rules="{required:true,between:[affluenzasezione.votantiTotaliAffP,affluenzasezione.iscrittiTotali],maxDifference:[affluenzasezione.votantiFemmine,affluenzasezione.votantiMaschi], min:1,max:4}"
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
                                                                 :rules="{required:true,between:[affluenzasezione.votantiTotaliAffP,affluenzasezione.iscrittiTotali],maxDifference:[affluenzasezione.votantiFemmine,affluenzasezione.votantiMaschi], min:1,max:4}"
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
                                                                 :rules="{required:true,between:[affluenzasezione.votantiTotaliAffP,affluenzasezione.iscrittiTotali],maxDifference:[affluenzasezione.votantiFemmine,affluenzasezione.votantiMaschi], min:1,max:4}"
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
                                        <div class="input-group">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text br-15">
                                                    <b-button @click="showModal(data)" id="votiCoalizione" style="background-color:#343a40;border:none" variant="dark">
                                                        <b-icon icon="envelope-fill" aria-label="Help"></b-icon>
                                                    </b-button>
                                                    <b-tooltip target="votiCoalizione" triggers="hover">
                                                        voti coalizione
                                                    </b-tooltip>
                                                </span>
                                            </div>
                                            <b-form-input type="number"
                                                          style="display:none;"
                                                          v-model="data.NumeroSezione" />
                                            <b-form-input type="text"
                                                          style="display:none;"
                                                          v-model="data.tipo" />
                                            <b-form-input name="id" type="number"
                                                          v-model="data.id"
                                                          style="display:none;" />
                                            <b-form-input type="hidden"
                                                          v-model="data.isCoalizione" />
                                            <b-form-input name="id" type="text"
                                                          v-model="data.idCoalizione"
                                                          style="display:none;" />
                                            <b-form-input type="number"
                                                          style="display:none;"
                                                          v-model="data.idSindaco" />
                                            <label class="col-sm-3  col-form-label">
                                                {{ data.Nome}} {{data.Cognome}}
                                            </label>
                                            <validation-provider vid="datatotale"
                                                                 name="datatotale"
                                                                 :rules="{required:true,min:1,max:4}"
                                                                 v-slot="validationContext">
                                                <b-form-input v-model="data.totale"
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
                                        </div>
                                    </div>
                                </div>
                            </b-form>
                        </validation-observer>
                    </fieldset>
                </div>
            </div>
        </div>
    </div>
    <!-- Info modal -->
    <b-modal :id="coalizioneModal.id" :title="coalizioneModal.title" style="font-size:1.5rem" ok-only @hide="resetCoalizioneModal">
        <b-container fluid>
            <span>
                <b-badge href="#" variant="dark">
                    <v-icon name="id-badge"></v-icon>
                    Denominazione: {{scrutinioModal.denominazione}}
                </b-badge>
            </span><br />
            <span>
                <b-badge href="#" variant="dark">
                    <v-icon name="user"></v-icon>
                    Voti: {{scrutinioModal.voti}}
                </b-badge>
            </span><br />
            <span>
                <b-badge href="#" variant="dark">
                    <v-icon name="user-circle"></v-icon>IdLista: {{scrutinioModal.IdLista}}
                </b-badge>
            </span><br />
            <span>
                <b-badge href="#" variant="dark">
                    <v-icon name="user-circle"></v-icon>
                    IdSindaco: {{scrutinioModal.IdSindaco}}
                </b-badge>
            </span><br />
            <span>
                <b-badge href="#" variant="dark">
                    <v-icon name="at"></v-icon>
                    IdVoti: {{scrutinioModal.IdVoti}}
                </b-badge>
            </span><br />
            <span><b-badge href="#" variant="dark"><v-icon name="user-slash"></v-icon>Abilitato: {{infoModal.abilitato}}</b-badge></span><br />
            <span><b-badge href="#" variant="dark"><v-icon name="tags"></v-icon>Ruoli: {{infoModal.ruoli}}</b-badge></span><br />
        </b-container>
    </b-modal>
</div>
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
        name: 'scrutinio',
        props: ['tipo', 'scrutinio'],
        data() {
            return {
                scrutinioModal: {
                    id: 'scrutinio-modal',
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
                console.log(this.affluenzasezione);
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