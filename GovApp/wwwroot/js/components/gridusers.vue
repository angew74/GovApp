<template>
    <b-container fluid>
        <b-row>
            <b-col lg="6" class="my-1">
                <validation-provider name="filtro"
                                     :rules="{min:3,max:16}"
                                     v-slot="validationContext">
                    <b-form-group label="Filtro"
                                  label-cols-sm="3"
                                  label-align-sm="right"
                                  label-size="sm"
                                  label-for="filterInput"
                                  class="mb-0">
                        <b-input-group size="sm">
                            <b-form-input v-model="filter"
                                          type="search"
                                          id="filterInput"
                                          :state="getValidationState(validationContext)"
                                          aria-describedby="input-filtro-live-feedback"
                                          placeholder="Tipo da cercare"></b-form-input>
                            <b-input-group-append>
                                <b-button :disabled="!filter" @click="filter = ''">Pulisci</b-button>
                            </b-input-group-append>
                            <b-form-invalid-feedback id="input-filtro-live-feedback">{{ validationContext.errors[0] }}</b-form-invalid-feedback>
                        </b-input-group>
                    </b-form-group>
                </validation-provider>
            </b-col>

            <b-col lg="6" class="my-1" v-if="filtriSu.length">
                <b-form-group label="Filtra per"
                              label-cols-sm="3"
                              label-align-sm="right"
                              label-size="sm"
                              description="Lascia filtri non selezionati per cercare su tutti i campi"
                              class="mb-0">
                    <b-form-checkbox-group :options="filtriSu"
                                           value-field="item"
                                           text-field="name"
                                           v-model="selectedFilters" v class="mt-1">
                    </b-form-checkbox-group>
                </b-form-group>
            </b-col>
        </b-row>

        <!-- Main table element -->
        <b-table show-empty
                 ref="table"
                 stacked="md"
                 :striped="true"
                 :small="true"
                 :items="elementi"
                 :fields="configurazione"
                 :per-page="0"
                 :current-page="paginaCorrente"
                 :hover="true"
                 :borderless="true"
                 head-variant="dark"
                 :filterIncludedFields="filtriSu"
                 :empty-filtered-text="emptyFiltered"
                 :empty-text="empty"
                 :sort-by.sync="ordinaPer"
                 @sort-changed="sortingChanged"
                 @filtered="onFiltered"
                 :no-local-sorting="true"
                 :no-local-filtering="true"
                 :sort-desc.sync="ordinaDesc"
                 :sort-direction="ordinaDirezione">
            <template v-slot:cell(detailsuser)="row">
                <b-button id="detailsUser" size="sm" style="background-color:#343a40;border:none" variant="dark" @click="info(row.item, row.index, $event.target)" class="mr-1">
                    <b-icon icon="info-square-fill" aria-label="Help"></b-icon>
                </b-button>
                <b-tooltip target="detailsUser" triggers="hover">
                    dettagli utente
                </b-tooltip>
            </template>
            <template v-slot:cell(deleteuser)="row">
                <b-button id="deleteUser" size="sm" @click="delete(row.item,row.index, $event.target)" style="background-color:#343a40;border:none" variant="dark">
                    <b-icon icon="person-dash-fill" aria-label="Help"></b-icon>
                </b-button>
                <b-tooltip target="deleteUser" triggers="hover">
                    cancella utente
                </b-tooltip>
            </template>
            <template v-slot:cell(disableuser)="row">
                <b-button id="disableUser" size="sm" @click="disable(row.item,row.index, $event.target)" style="background-color:#343a40;border:none" variant="dark">
                    <b-icon icon="person-bounding-box" aria-label="Help"></b-icon>
                </b-button>
                <b-tooltip target="disableUser" triggers="hover">
                    disabilita utente
                </b-tooltip>
            </template>
            <template v-slot:cell(resetpassword)="row">
                <b-button id="resetPassword" size="sm" @click="resetpassword(row.item,row.index, $event.target)" style="background-color:#343a40;border:none" variant="dark">
                    <b-icon icon="envelope-fill" aria-label="Help"></b-icon>
                </b-button>
                <b-tooltip target="resetPassword" triggers="hover">
                    reset password
                </b-tooltip>
            </template>
            <template v-slot:row-details="row">
                <b-card>
                    <ul>
                        <li v-for="(value, key) in row.item" :key="key">{{ key }}: {{ value }}</li>
                    </ul>
                </b-card>
            </template>
            <template v-slot:custom-foot>
                <b-td colspan="7" variant="secondary" class="text-right">
                    <v-icon name="table"></v-icon> Righe totali:<b>{{topRigheTotali}}</b>
                </b-td>
            </template>
        </b-table>
        <b-row>
            <b-col sm="5" md="6" class="my-1">
                <b-pagination pills
                              v-model="paginaCorrente"
                              :total-rows="topRigheTotali"
                              :per-page="topPerPagina"
                              @input="pagingClick"
                              align="right"
                              size="sm"
                              class="my-0">
                    <template v-slot:first-text>
                        <span class="text-success"><v-icon name="fast-backward"></v-icon></span>
                    </template>
                    <template v-slot:prev-text>
                        <span class="text-danger"><v-icon name="angle-double-left"></v-icon></span>
                    </template>
                    <template v-slot:next-text>
                        <span class="text-warning"><v-icon name="angle-double-right"></v-icon></span>
                    </template>
                    <template v-slot:last-text>
                        <span class="text-info"><v-icon name="fast-forward"></v-icon></span>
                    </template>
                    <template v-slot:ellipsis-text>
                        <b-spinner small type="grow"></b-spinner>
                        <b-spinner small type="grow"></b-spinner>
                        <b-spinner small type="grow"></b-spinner>
                    </template>
                    <template v-slot:page="{ page, active }">
                        <b v-if="active">{{ page }}</b>
                        <i v-else>{{ page }}</i>
                    </template>
                </b-pagination>
            </b-col>
        </b-row>
        <!-- Info modal -->
        <b-modal :id="infoModal.id" :title="infoModal.title" style="font-size:1.5rem" ok-only @hide="resetInfoModal">
            <b-container fluid>
                <span>
                    <b-badge href="#" variant="dark">
                        <v-icon name="id-badge"></v-icon>
                        ID: {{infoModal.iduser}}
                    </b-badge>
                </span><br />
                <span>
                    <b-badge href="#" variant="dark">
                        <v-icon name="user"></v-icon>
                        UserName: {{infoModal.username}}
                    </b-badge>
                </span><br />
                <span>
                    <b-badge href="#" variant="dark">
                        <v-icon name="user-circle"></v-icon>Nome: {{infoModal.nome}}
                    </b-badge>
                </span><br />
                <span>
                    <b-badge href="#" variant="dark">
                        <v-icon name="user-circle"></v-icon>
                        Cognome: {{infoModal.cognome}}
                    </b-badge>
                </span><br />
                <span>
                    <b-badge href="#" variant="dark">
                        <v-icon name="at"></v-icon>
                        Email: {{infoModal.email}}
                    </b-badge>
                </span><br />
                <span><b-badge href="#" variant="dark"><v-icon name="user-slash"></v-icon>Abilitato: {{infoModal.abilitato}}</b-badge></span><br />
                <span><b-badge href="#" variant="dark"><v-icon name="tags"></v-icon>Ruoli: {{infoModal.ruoli}}</b-badge></span><br />
                </b-container>
        </b-modal>
    </b-container>
</template>

<script>
    export default {

        props: ["elementi", "configurazione", "topRigheTotali", "topPaginaCorrente", "topPerPagina", "opzioni", "topOrdinaPer", "filtriSu", "topOrdinaDesc", "ordinaDirezione", "filtri"],
        data() {
            return {
                ordinaPer: this.topOrdinaPer,
                ordinaDesc: this.topOrdinaDesc,
                filter: this.filtri,
                paginaCorrente: this.topPaginaCorrente,
                selectedFilters: [],
                empty: "Dati non presenti",
                emptyFiltered: 'Nessun riscontro',
                infoModal: {
                    id: 'info-modal',
                    title: '',
                    nome: '',
                    cognome: '',
                    username: '',
                    email: '',
                    abilitato: '',
                    ruoli: '',
                    iduser: ''
                }
            }
        },
        computed: {
        },
        mounted() {
        },
        watch: {
            currentPage(newPage, oldPage) {
                this.$emit('paging', newPage, this.ordinaPer, this.ordinaDesc, this.filter, this.selectedFilters);
            },
            filter(newvalue, oldvalue) {
                if (newvalue !== null && newvalue.length > 2) {
                    this.callParentForFilter(newvalue);
                    return true;
                }
                else if (newvalue != null && newvalue.length === 0 && oldvalue.length > 0) {
                    this.$emit('paging', '1', this.ordinaPer, this.ordinaDesc, this.filter, this.selectedFilters);
                }
            }
        },
        methods: {
            info(item, index, button) {
                this.infoModal.nome = item.nome;
                this.infoModal.cognome = item.cognome;
                this.infoModal.username = item.userName;
                this.infoModal.iduser = item.id;
                this.infoModal.email = item.email;
                this.infoModal.abilitato = item.isActive ? 'SI' : 'NO';
                this.infoModal.ruoli = item.ruoli;
                this.$root.$emit('bv::show::modal', this.infoModal.id, button)
            },
            delete(item, index, button) {
                this.$emit('deleteuser', item)
            },
            disable(item, index, button) {
                this.$emit('disableuser', item)
            },
            resetpassword(item, index, button) {
                this.$emit('resetpassword', item)
            },

            resetInfoModal() {
                this.infoModal.title = ''
                this.infoModal.content = ''
            },
            callParentForFilter(fil) {
                this.$emit('filtering', fil, this.selectedFilters);
            },
            onFiltered(filteredItems) {
                // Trigger pagination to update the number of buttons/pages due to filtering
                this.totalRows = this.elementi;
                this.currentPage = this.paginaCorrente;
            },
            pagingClick: function (pagina) {
                this.$emit('paging', pagina, this.ordinaPer, this.ordinaDesc, this.filter, this.selectedFilters);
            },
            sortingChanged(ctx) {
                this.$emit('sorting', ctx)
            },
            activeUser(value) {
                if (value === true) { return 'SI' }
                else { return 'NO' }
            },
            getValidationState({ dirty, validated, valid = null }) {
                return dirty || validated ? valid : null;
            }
        }
    }
</script>