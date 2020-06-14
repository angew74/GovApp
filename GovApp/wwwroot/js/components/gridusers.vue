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
            <template v-slot:cell(actions)="row">
                <b-button size="sm" style="background-color:#343a40;border:none" variant="dark" @click="info(row.item, row.index, $event.target)" class="mr-1">
                    <b-icon icon="person-fill" aria-label="Help"></b-icon>
                </b-button>
                <b-button size="sm" @click="row.toggleDetails" style="background-color:#343a40;border:none" variant="dark">
                    <b-icon icon="toggles" aria-label="Help"></b-icon>
                </b-button>
            </template>
            <template v-slot:row-details="row">
                <b-card>
                    <ul>
                        <li v-for="(value, key) in row.item" :key="key">{{ key }}: {{ value }}</li>
                    </ul>
                </b-card>
            </template>
            <template v-slot:custom-foot>
           <v-icon name="table"></v-icon> Righe totali: <b>{{topRigheTotali}}</b>
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
        <b-modal :id="infoModal.id" :title="infoModal.title" ok-only @hide="resetInfoModal">
            <pre>{{ infoModal.content
    }}</pre>
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
                emptyFiltered:'Nessun riscontro',
                infoModal: {
                    id: 'info-modal',
                    title: '',
                    content: ''
                }
            }
        },
        computed: {  
        },
        mounted() {
        },
        watch: {
            currentPage(newPage, oldPage) {
                this.$emit('paging', newPage);
            },
            filter(newvalue, oldvalue) {
                if (newvalue !== null && newvalue.length > 2) {
                    this.callParentForFilter(newvalue);                    
                    return true;
                }
                else if (newvalue != null && newvalue.length === 0 && oldvalue.length > 0) {
                    this.$emit('paging', '1');
                }
            }
        },
        methods: {
            info(item, index, button) {
                this.infoModal.title = `Row index: ${index}`
                this.infoModal.content = JSON.stringify(item, null, 2)
                this.$root.$emit('bv::show::modal', this.infoModal.id, button)
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
                this.$emit('paging', pagina);
            },
            sortingChanged(ctx) {
                this.$emit('sorting',ctx)
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