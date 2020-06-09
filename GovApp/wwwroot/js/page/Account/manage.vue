<template>
    <div>
        <app-sidebar></app-sidebar>
        <app-users :elementi="users" :configurazione="mapgrid" :topRigheTotali="righe" :topPaginaCorrente="pagina" :topPerPagina="per" :filtriSu="fSu" :opzioni="opz" :topOrdinaPer="oPer" :topOrdinaDesc="oDesc"
                   :ordinaDirezione="direzione" :filtri="fifi" @paging="pagingUsers" @filtering="filteringUsers"></app-users>
        <notifications position="top center" group="errori">
            <template slot="body" slot-scope="props">
                <div>
                    <a class="title">
                        {{props.item.title}}
                    </a>
                    <a class="close" @click="props.close">
                        <v-icon name="times" />
                    </a>
                    <div v-html="props.item.text">
                    </div>
                </div>
            </template>
        </notifications>
        <app-footer></app-footer>
        <error-bound></error-bound>
    </div>
</template>

<script>
    import sidebaraw from '../../components/sidebaraw.vue';
    import footeraw from '../../components/footeraw.vue';
    import griduser from '../../components/gridusers.vue';
    import errorboundaryaw from '../../components/error-boundaryaw.vue';
    import { mapGetters, mapState, mapActions } from 'vuex';
    export default {
        namespaced: true,
        components: {
            'app-sidebar': sidebaraw,
            'app-footer': footeraw,
            'error-bound': errorboundaryaw,
            'app-users': griduser
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
                messaggio: ''
            }
        },
        mounted() {
            this.getUsers(1);
            this.getParams("users",1)
        },
        created() {
            this.restoreContext()
        },
        methods: {
            ...mapActions('context', [
                'restoreContext'
            ]),
            getUsers(page) {
                axios({
                    method: 'get',
                    url: '/api/auth/users',
                    params: {
                        "page": page
                    }
                })
                    .then(response => {
                        this.users = response.data;
                    })
                    .catch(function (error) {
                        console.log(error);
                    });
            },
            getUsersFiltering(page,filter,types) {
                axios({
                    method: 'get',
                    url: '/api/auth/usersfilters',
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
                    });
            },
            getParams(type, page) {
                axios({
                    method: 'get',
                    url: '/api/auth/pagination',
                    params: {
                        "type": type,
                        "page": page
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
                    });
            },
            getParamsFiltering(type, page,filter,opzioni) {
                axios({
                    method: 'get',
                    url: '/api/auth/paginationlike',
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
                    });
            },
            pagingUsers(page) {
                if (page !== null) {
                    this.getUsers(page);
                    this.getParams("users", page);
                }
            },
            filteringUsers(filter,opzioni) {
                if (filter !== null) {
                    this.getUsersFiltering("1",filter,opzioni);
                    this.getParamsFiltering("users", "1",filter, opzioni);
                }
            }
        },
        showAlert(message) {
            this.$notify({
                group: 'errori',
                position: "top center",
                duration: "10000",
                width: "450px",
                type: "error",
                title: 'Attenzione',
                text: message
            })
        }
    }
</script>