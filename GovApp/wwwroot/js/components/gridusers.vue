<template>
    <b-container fluid>
        <b-row>
            <b-col lg="6" class="my-1">
                <b-form-group label="Ordinamento"
                              label-cols-sm="3"
                              label-align-sm="right"
                              label-size="sm"
                              label-for="sortBySelect"
                              class="mb-0">
                    <b-input-group size="sm">
                        <b-form-select v-model="ordinaPer" id="sortBySelect" :options="sortOptions" class="w-75">
                            <template v-slot:first>
                                <option value="">-- none --</option>
                            </template>
                        </b-form-select>
                        <b-form-select v-model="ordinaDesc" size="sm" :disabled="!ordinaPer" class="w-25">
                            <option :value="false">Asc</option>
                            <option :value="true">Desc</option>
                        </b-form-select>
                    </b-input-group>
                </b-form-group>
            </b-col>

            <b-col lg="6" class="my-1">
                <b-form-group label="Ordinamento iniziale"
                              label-cols-sm="3"
                              label-align-sm="right"
                              label-size="sm"
                              label-for="initialSortSelect"
                              class="mb-0">
                    <b-form-select v-model="ordinaPer"
                                   id="initialSortSelect"
                                   size="sm"
                                   :options="['asc', 'desc', 'last']"></b-form-select>
                </b-form-group>
            </b-col>

            <b-col lg="6" class="my-1">
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
                                      placeholder="Tipo da cercare"></b-form-input>
                        <b-input-group-append>
                            <b-button :disabled="!filter" @click="filter = ''">Pulisci</b-button>
                        </b-input-group-append>
                    </b-input-group>
                </b-form-group>
            </b-col>

            <b-col lg="6" class="my-1">
                <b-form-group label="Filtra per"
                              label-cols-sm="3"
                              label-align-sm="right"
                              label-size="sm"
                              description="Lascia filtri non selezionati per cercare su tutti i campi"
                              class="mb-0">
                    <b-form-checkbox-group v-model="filtriSu" class="mt-1">
                        <b-form-checkbox value="userName">username</b-form-checkbox>
                        <b-form-checkbox value="email">mail</b-form-checkbox>
                        <b-form-checkbox value="isActive">attivo</b-form-checkbox>
                    </b-form-checkbox-group>
                </b-form-group>
            </b-col>

            <b-col sm="5" md="6" class="my-1">
                <b-form-group label="Per pagina"
                              label-cols-sm="6"
                              label-cols-md="4"
                              label-cols-lg="3"
                              label-align-sm="right"
                              label-size="sm"
                              label-for="perPageSelect"
                              class="mb-0">
                    <b-form-select v-model="perPagina"
                                   id="perPageSelect"
                                   size="sm"
                                   :options="opzioni"></b-form-select>
                </b-form-group>
            </b-col>

            <b-col sm="7" md="6" class="my-1">
                <b-pagination v-model="paginaCorrente"
                              :total-rows="righeTotali"
                              :per-page="perPagina"
                              align="fill"
                              size="sm"
                              class="my-0"></b-pagination>
            </b-col>
        </b-row>

        <!-- Main table element -->
        <b-table show-empty
                 small
                 stacked="md"
                 :items="elementi"
                 :fields="configurazione"
                 :current-page="paginaCorrente"
                 :per-page="perPagina"
                 :filter="filter"
                 :filterIncludedFields="filtriSu"
                 :sort-by.sync="ordinaPer"
                 :sort-desc.sync="ordinaDesc"
                 :sort-direction="ordinaDirezione"
                 @filtered="onFiltered">       
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
        </b-table>

        <!-- Info modal -->
        <b-modal :id="infoModal.id" :title="infoModal.title" ok-only @hide="resetInfoModal">
            <pre>{{ infoModal.content }}</pre>
        </b-modal>
    </b-container>
</template>

<script>
    export default {
        props: ["elementi", "configurazione", "righeTotali", "topPaginaCorrente", "topPerPagina", "opzioni","topOrdinaPer", "filtriSu", "topOrdinaDesc","ordinaDirezione","filtri"],
    data() {
        return { 
          paginaCorrente: this.topPaginaCorrente,
          ordinaPer: this.topOrdinaPer,
          ordinaDesc: this.topOrdinaDesc,
          perPagina: this.topPerPagina,
          filter: this.filtri,
          infoModal: {
              id: 'info-modal',
              title: '',
              content: ''
          }        
      }
    },
    computed: {
        sortOptions() {
            if (typeof (this.fields) !== 'undefined' && typeof (this.fields.filter) !== 'undefined') {
                return this.fields
                    .filter(f => f.sortable)
                    .map(f => {
                        return { text: f.label, value: f.key }
                    })
            }
        }
    },
    mounted() {
        // Set the initial number of items
        if (typeof (this.items) !== 'undefined' && this.items !== null) {
            this.totalRows = this.items.length
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
      onFiltered(filteredItems) {
        // Trigger pagination to update the number of buttons/pages due to filtering
        this.totalRows = filteredItems.length
        this.currentPage = 1
      }
    }
  }
</script>