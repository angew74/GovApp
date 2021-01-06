<template>
    <div>
        <app-sidebar></app-sidebar>
        <b-skeleton-wrapper :loading="loading">
            <template #loading>
                <b-row>
                    <b-col>
                        <b-skeleton-img></b-skeleton-img>
                    </b-col>
                    <b-col>
                        <b-skeleton-img></b-skeleton-img>
                    </b-col>
                    <b-col cols="12" class="mt-3">
                        <b-skeleton-img no-aspect height="150px"></b-skeleton-img>
                    </b-col>
                </b-row>
            </template>
            <app-rights :elementi="rights" :configurazione="mapgrid" :topRigheTotali="righe" :topPaginaCorrente="pagina" :topPerPagina="per" :filtriSu="fSu" :opzioni="opz" :topOrdinaPer="oPer" :topOrdinaDesc="oDesc"
                        :ordinaDirezione="direzione" :filtri="fifi" @sorting="sortingRights"
                        @updating="updateRights"
                        @paging="pagingRights" @filtering="filteringRights"></app-rights>
            </b-skeleton-wrapper>
            <app-footer></app-footer>
    </div>
</template>

<script>
    import sidebaraw from '../../components/sidebaraw.vue';
    import footeraw from '../../components/footeraw.vue';     
    import gridaw from '../../components/gridaw.vue';
    import { mapGetters, mapState, mapActions } from 'vuex';
    export default {
        namespaced: true,
        components: {
            'app-sidebar': sidebaraw,
            'app-footer': footeraw,          
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
                loading: false
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
                this.loading = true;
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
                        this.loading = false;
                    })
                    .catch(function (error) {
                        console.log(error);                       
                        this.messaggio = error.response.statusText;
                        this.showSweetAlert(error.response.statusText);                       
                        this.loading = false;
                    });
            },
            getRightsFiltering(page, filter, types) {
                this.loading = true;
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
                        this.loading = false;
                    })
                    .catch(function (error) {
                        console.log(error);                       
                        this.messaggio = error.response.statusText;
                        this.showSweetAlert(error.response.statusText);
                        this.loading = false;
                    });
            },
            getRightsSorting(sort) {
                this.loading = true;
                axios.post('/GovApp/api/rights/rightssorting', sort, {
                    headers: {
                        'Content-Type': 'application/json'
                    }
                }).then(response => {
                    this.rights = response.data;
                    this.loading = false;
                }).catch(error => {
                    error => this.messaggio = error.response.data.errMsg;
                    this.showSweetAlert(error.response.data.errMsg);
                    this.loading = false;
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
                        this.showSweetAlert(error.response.statusText);
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
                        this.showSweetAlert(error.response.statusText);
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
                this.loading = true;
                axios.post('/GovApp/api/rights/update', right, {
                    headers: {
                        'Content-Type': 'application/json'
                    }
                }).then(response => {
                    this.showSweetAlertinfo("Abilitazione aggiornata correttamente");
                    this.loading = false;
                }).catch(error => {
                    error => this.messaggio = error.response.data.errMsg;
                    this.showSweetAlert(error.response.data.errMsg);
                    console.log(error.response.data);
                    console.log(error.response.status);
                    this.loading = false;
                    console.log(error.response.headers);
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
            showSweetAlertinfo(message) {
                this.$swal({
                    title: 'Congratulazioni',
                    text: message,
                    icon: 'info',
                    showCancelButton: false,
                    showCloseButton: true,
                })
            }, 
        }
   }
</script>