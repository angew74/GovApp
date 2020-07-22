<template>
    <div>
        <app-sidebar></app-sidebar>
        <app-rights :elementi="rights" :configurazione="mapgrid" :topRigheTotali="righe" :topPaginaCorrente="pagina" :topPerPagina="per" :filtriSu="fSu" :opzioni="opz" :topOrdinaPer="oPer" :topOrdinaDesc="oDesc"
                   :ordinaDirezione="direzione" :filtri="fifi" @sorting="sortingRights"
                   @updating="updateRights"
                   @paging="pagingRights" @filtering="filteringRights"></app-rights>
        <app-footer></app-footer>
        <notifications position="top center" group="errori" />
        <notifications position="top center" group="info" />
        <error-bound></error-bound>
    </div>
</template>

<script>
    import sidebaraw from '../../components/sidebaraw.vue';
    import footeraw from '../../components/footeraw.vue';  
    import errorboundaryaw from '../../components/error-boundaryaw.vue';
    import gridaw from '../../components/gridaw.vue';
    import { mapGetters, mapState, mapActions } from 'vuex';
    export default {
        namespaced: true,
        components: {
            'app-sidebar': sidebaraw,
            'app-footer': footeraw,
            'error-bound': errorboundaryaw,
            'app-rights': gridaw

        },
        data: function () {
            return {
                rights: [],
                mapgrid: [],
                righe: 0,
                pagina: 0,
                per: 0,
                opz: [],
                oPer: '',
                oDesc: false,
                direzione: 'asc',
                fSu: [],
                fifi: '',
                messaggio: '',
                errored: false
            }
        },
        mounted() {
            this.getRights(1);
            this.getParams("rights", 1)
        },
        created() {
            this.restoreContext()
        },
        methods: {
            ...mapActions('context', [
                'restoreContext'
            ]),
            getRights(page, ordinaPer, ordinaDesc, filter, filterarray) {
                axios({
                    method: 'get',
                    url: '/GovApp/api/rights/rights',
                    params: {
                        "page": page,
                        "ordinaPer": ordinaPer,
                        "ordinaDesc": ordinaDesc,
                        "filter": filter,
                        "fitriarray": filterarray
                    }
                })
                    .then(response => {
                        this.rights = response.data;
                    })
                    .catch(function (error) {
                        console.log(error);
                        this.errored = true;
                        this.messaggio = error.response.statusText;
                    });
            },
            getRightsFiltering(page, filter, types) {
                axios({
                    method: 'get',
                    url: '/GovApp/api/rights/rightsfilters',
                    params: {
                        "page": page,
                        "filtro": filter,
                        "fitriarray": types
                    }
                })
                    .then(response => {
                        this.rights = response.data;
                    })
                    .catch(function (error) {
                        console.log(error);
                        this.errored = true;
                        this.messaggio = error.response.statusText;
                    });
            },
            getRightsSorting(sort) {
                axios.post('/GovApp/api/rights/rightssorting', sort, {
                    headers: {
                        'Content-Type': 'application/json'
                    }
                }).then(response => {
                    this.rights = response.data;
                }).catch(error => {
                    error => this.messaggio = error.response.data.errMsg;
                    this.showAlert(error.response.data.errMsg);
                    console.log(error.response.data);
                    console.log(error.response.status);
                    console.log(error.response.headers);
                });
            },
            getParams(type, page, ordinaPer, ordinaDesc, filter, filtriarray) {
                axios({
                    method: 'get',
                    url: '/GovApp/api/rights/pagination',
                    params: {
                        "type": type,
                        "page": page,
                        "ordinaPer": ordinaPer,
                        "ordinaDesc": ordinaDesc,
                        "filter": filter,
                        "filtriarray": filtriarray
                    }
                }).then(response => {
                    this.mapgrid = response.data.fields;
                    this.pagina = response.data.currentPage;
                    this.per = response.data.perPage;
                    this.opz = response.data.pageOptions;
                    this.oPer = response.data.sortBy;
                    this.oDesc = response.data.sortDesc;
                    this.direzione = response.data.sortDirection;
                    this.righe = response.data.totalRows;
                    this.fifi = response.data.filter;
                    this.fSu = response.data.filterOn;
                })
                    .catch(function (error) {
                        console.log(error);
                        this.showAlert(error.response.statusText);
                    });
            },
            getParamsFiltering(type, page, filter, opzioni) {
                axios({
                    method: 'get',
                    url: '/GovApp/api/rights/paginationlike',
                    params: {
                        "type": type,
                        "page": page,
                        "filtro": filter,
                        "filtriarray": opzioni
                    }
                }).then(response => {
                    this.mapgrid = response.data.fields;
                    this.pagina = response.data.currentPage;
                    this.per = response.data.perPage;
                    this.opz = response.data.pageOptions;
                    this.oPer = response.data.sortBy;
                    this.oDesc = response.data.sortDesc;
                    this.direzione = response.data.sortDirection;
                    this.righe = response.data.totalRows;
                    this.fifi = response.data.filter;
                    this.fSu = response.data.filterOn;
                })
                    .catch(function (error) {
                        console.log(error);
                        this.showAlert(error.response.statusText);
                    });
            },
            pagingRights(page, ordinaPer, ordinaDesc, filter, filtriarray) {
                if (page !== null) {
                    this.getRights(page, ordinaPer, ordinaDesc, filter, filtriarray);
                    this.getParams("rights", page, ordinaPer, ordinaDesc, filter, filtriarray);
                }
            },
            filteringRights(filter, opzioni) {
                if (filter !== null) {
                    this.getRightsFiltering("1", filter, opzioni);
                    this.getParamsFiltering("rights", "1", filter, opzioni);
                }
            },
            sortingRights(ctx) {
                this.getURightsSorting(ctx);
            },
            updateRights(right) {
                axios.post('/GovApp/api/rights/update', right, {
                    headers: {
                        'Content-Type': 'application/json'
                    }
                }).then(response => {
                    this.showAlertinfo("Abilitazione aggiornata correttamente");
                }).catch(error => {
                    error => this.messaggio = error.response.data.errMsg;
                    this.showAlert(error.response.data.errMsg);
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
            },
            showAlertinfo(message) {
                this.$notify({
                    group: 'info',
                    position: "top center",
                    duration: "15000",
                    width: "450px",
                    type: "success",
                    title: 'Congratulazioni',
                    text: message
                })
            }
        }
   }
</script>