<template>
    <div>
        <app-sidebar></app-sidebar>
        <app-users :elementi="users" :configurazione="mapgrid" :topRigheTotali="righe" :topPaginaCorrente="pagina" :topPerPagina="per" :filtriSu="fSu" :opzioni="opz" :topOrdinaPer="oPer" :topOrdinaDesc="oDesc"
                   :ordinaDirezione="direzione" :filtri="fifi" @sorting="sortingUsers"
                   @disableuser="duser" @deleteuser="deuser" @resetpassword="ruser"
                   @paging="pagingUsers" @filtering="filteringUsers"></app-users>
        <notifications position="top center" group="errori" />
        <notifications position="top center" group="info" />
        <app-footer></app-footer>
        <error-bound></error-bound>
    </div>
</template>

<script>
    import sidebaraw from '../../components/sidebaraw.vue';
    import footeraw from '../../components/footeraw.vue';
    import gridaw from '../../components/gridaw.vue';
    import errorboundaryaw from '../../components/error-boundaryaw.vue';
    import { mapGetters, mapState, mapActions } from 'vuex';
    export default {
        namespaced: true,
        components: {
            'app-sidebar': sidebaraw,
            'app-footer': footeraw,
            'error-bound': errorboundaryaw,
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
                errored:false
            }
        },
        mounted() {
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
                axios.post('/GovApp/api/auth/disableuser', user, {
                    headers: {
                        'Content-Type': 'application/json'
                    }
                }).then(response => {
                    this.showAlertinfo("Utente disabilitato");
                }).catch(error => {
                    error => this.messaggio = error.response.data.errMsg;
                    this.showAlert(error.response.data.errMsg);
                    console.log(error.response.data);
                    console.log(error.response.status);
                    console.log(error.response.headers);
                });
            },
            deuser(user) {
                axios.post('/GovApp/api/auth/deleteuser', user, {
                    headers: {
                        'Content-Type': 'application/json'
                    }
                }).then(response => {
                    this.showAlertinfo("Utente cancellato");
                }).catch(error => {
                    error => this.messaggio = error.response.data.errMsg;
                    this.showAlert(error.response.data.errMsg);
                    console.log(error.response.data);
                    console.log(error.response.status);
                    console.log(error.response.headers);
                });
            },
            ruser(user) {
                axios.post('/GovApp/api/auth/resetpassword', user, {
                    headers: {
                        'Content-Type': 'application/json'
                    }
                }).then(response => {
                    this.showAlertinfo("Password reset effettuato all'indirizzo email");
                }).catch(error => {
                    error => this.messaggio = error.response.data.errMsg;
                    this.showAlert(error.response.data.errMsg);
                    console.log(error.response.data);
                    console.log(error.response.status);
                    console.log(error.response.headers);
                });
            },
            getUsers(page, ordinaPer, ordinaDesc, filter,filterarray) {
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
                    })
                    .catch(function (error) {
                        console.log(error);
                        this.errored = true;
                        this.messaggio = error.response.statusText;                       
                    });
            },
            getUsersFiltering(page, filter, types) {
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
                    })
                    .catch(function (error) {
                        console.log(error);
                        this.errored = true;
                        this.messaggio = error.response.statusText;
                    });
            },
            getUsersSorting(sort) {
                axios.post('/GovApp/api/auth/userssorting', sort, {
                    headers: {
                        'Content-Type': 'application/json'
                    }
                }).then(response => {
                    this.users = response.data;
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
                        this.showAlert(error.response.statusText);
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
                        this.showAlert(error.response.statusText);
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