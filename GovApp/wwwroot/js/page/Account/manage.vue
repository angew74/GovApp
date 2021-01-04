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
            <app-users :elementi="users" :configurazione="mapgrid" :topRigheTotali="righe" :topPaginaCorrente="pagina" :topPerPagina="per" :filtriSu="fSu" :opzioni="opz" :topOrdinaPer="oPer" :topOrdinaDesc="oDesc"
                       :ordinaDirezione="direzione" :filtri="fifi" @sorting="sortingUsers"
                       @disableuser="duser" @deleteuser="deuser" @resetpassword="ruser"
                       @paging="pagingUsers" @filtering="filteringUsers"></app-users>
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
            'app-users': gridaw
        },
        data: function () {
            return {
                users: [],
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
                errored: false,
                loading: false
            }
        },
        mounted() {
            this.loading = true;
            this.getUsers(1);
            this.getParams("users", 1)
        },
        created() {
            this.restoreContext()
        },
        methods: {
            ...mapActions('context', [
                'restoreContext'
            ]),
            duser(user) {
                this.loading = true;
                axios.post('/GovApp/api/auth/disableuser', user, {
                    headers: {
                        'Content-Type': 'application/json'
                    }
                }).then(response => {
                    this.showSweetAlertinfo("Utente disabilitato");
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
            deuser(user) {
                this.loading = true;
                axios.post('/GovApp/api/auth/deleteuser', user, {
                    headers: {
                        'Content-Type': 'application/json'
                    }
                }).then(response => {
                    this.loading = false;
                    this.showSweetAlertinfo("Utente cancellato");
                }).catch(error => {
                    error => this.messaggio = error.response.data.errMsg;
                    this.showSweetAlert(error.response.data.errMsg);
                    this.loading = false;
                    console.log(error.response.data);
                    console.log(error.response.status);
                    console.log(error.response.headers);
                });
            },
            ruser(user) {
                this.loading = true;
                axios.post('/GovApp/api/auth/resetpassword', user, {
                    headers: {
                        'Content-Type': 'application/json'
                    }
                }).then(response => {
                    this.loading = false;
                    this.showSweetAlertinfo("Password reset effettuato all'indirizzo email");
                }).catch(error => {
                    error => this.messaggio = error.response.data.errMsg;
                    this.showSweetAlert(error.response.data.errMsg);
                    this.loading = false;
                    console.log(error.response.data);
                    console.log(error.response.status);
                    console.log(error.response.headers);
                });
            },
            getUsers(page, ordinaPer, ordinaDesc, filter, filterarray) {
                this.loading = true;
                axios({
                    method: 'get',
                    url: '/GovApp/api/auth/users',
                    params: {
                        "page": page,
                        "ordinaPer": ordinaPer,
                        "ordinaDesc": ordinaDesc,
                        "filter": filter,
                        "fitriarray" : filterarray
                    }
                })
                    .then(response => {
                        this.users = response.data;
                        this.loading = false;
                    })
                    .catch(function (error) {
                        console.log(error);
                        this.loading = false;
                        this.messaggio = error.response.statusText;
                        showSweetAlert(this.messaggio);
                    });
            },
            getUsersFiltering(page, filter, types) {
                this.loading = true;
                axios({
                    method: 'get',
                    url: '/GovApp/api/auth/usersfilters',
                    params: {
                        "page": page,
                        "filtro": filter,
                        "fitriarray": types
                    }
                })
                    .then(response => {
                        this.users = response.data;
                        this.loading = false;
                    })
                    .catch(function (error) {
                        console.log(error);                      
                        this.messaggio = error.response.statusText;
                        this.showSweetAlert(this.messaggio);
                        this.loading = false;                        
                    });
            },
            getUsersSorting(sort) {
                this.loading = true;
                axios.post('/GovApp/api/auth/userssorting', sort, {
                    headers: {
                        'Content-Type': 'application/json'
                    }
                }).then(response => {
                    this.users = response.data;
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
                    url: '/GovApp/api/auth/pagination',
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
                        this.loading = false;
                    });
            },
            getParamsFiltering(type, page, filter, opzioni) {
                axios({
                    method: 'get',
                    url: '/GovApp/api/auth/paginationlike',
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
                        this.loading = false;
                    });
            },
            pagingUsers(page,ordinaPer,ordinaDesc,filter,filtriarray) {
                if (page !== null) {
                    this.getUsers(page, ordinaPer, ordinaDesc, filter, filtriarray);
                    this.getParams("users", page, ordinaPer, ordinaDesc, filter, filtriarray);
                }
            },
            filteringUsers(filter, opzioni) {
                if (filter !== null) {
                    this.getUsersFiltering("1", filter, opzioni);
                    this.getParamsFiltering("users", "1", filter, opzioni);
                }
            },
            sortingUsers(ctx) {
                this.getUsersSorting(ctx);
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
            }
        }
        
    }
</script>