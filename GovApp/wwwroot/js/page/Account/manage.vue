<template>
    <div>
        <app-sidebar></app-sidebar>
        <app-users :elementi="users" :configurazione="mapgrid" :righeTotali="righe" :topPaginaCorrente="pagina" :perPagina="per" :filtriSu="fSu" :opzioni="opz" :topOrdinaPer="oPer" :topOrdinaDesc="oDesc"
                   :ordinaDirezione="direzione" :filtri="fifi"></app-users>
        <app-footer></app-footer>
        <error-bound></error-bound>
        <app-alert :message="messaggio"></app-alert>
    </div>
</template>

<script>
    import sidebaraw from '../../components/sidebaraw.vue';
    import footeraw from '../../components/footeraw.vue';
    import griduser from '../../components/gridusers.vue';
    import errorboundaryaw from '../../components/error-boundaryaw.vue';
    import alertaw from '../../components/alertaw.vue';
    import { mapGetters, mapState, mapActions } from 'vuex';
    export default {
        namespaced: true,
        components: {
            'app-sidebar': sidebaraw,
            'app-footer': footeraw,
            'error-bound': errorboundaryaw,
            'app-users': griduser,
            'app-alert': alertaw
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
            axios({
                method: 'get',
                url: '/api/auth/users'
            })
                .then(response => {
                    this.users = response.data;
                })
                .catch(function (error) {
                    console.log(error);
                });
            axios({
                method: 'get',
                url: '/api/auth/pagination',
                params: {
                    "page": "users"
                }
            }).then(response => {
                this.mapgrid = response.data.fields;
                this.righe = response.data.totalRows;
                this.pagina = response.data.currentPage;
                this.per = response.data.perPage;
                this.opz = response.data.pageOptions;
                this.oPer = response.data.sortBy;
                this.oDesc = response.data.sortDesc;
                this.direzione = response.data.sortDirection;
                this.fifi = response.data.filter;
            })
                .catch(function (error) {
                    console.log(error);
                });
        },
        created() {
            this.restoreContext()
        },
        methods: {
            ...mapActions('context', [
                'restoreContext'
            ])
        },
        showAlert(message) {
            this.messaggio = message;           
        }
    }
</script>