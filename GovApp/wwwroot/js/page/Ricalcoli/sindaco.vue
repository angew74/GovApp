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
            <app-generali :generali="generaliview" :tipo="tipo"></app-generali>
            <app-sindaci :elementi="listeview" :municipio="municipio" @next="caricaRicalcolo" :tipo="ricalcolo" @saver="saveRicalcolo"></app-sindaci>
        </b-skeleton-wrapper>
        <app-footer></app-footer>
    </div>
</template>

<script>
    import sidebaraw from '../../components/sidebaraw.vue';
    import footeraw from '../../components/footeraw.vue';
    import generaliviewaw from '../../components/generaliviewaw.vue';
    import sindacoviewaw from '../../components/sindacoviewaw.vue';
    import { mapGetters, mapState, mapActions, mapMutations } from 'vuex';
    // con prop
    export default {
        components: {
            'app-sidebar': sidebaraw,
            'app-footer': footeraw,
            'app-sindaci': sindacoviewaw,
            'app-generali': generaliviewaw
        },
        data: function () {
            return {
                loading: false,
                form: {},
                voti: {},
                ricalcolo: "R",
                municipio: 99
            }
        },
        computed: {
            ...mapState('context', [
                'message',
                'votiRicalcolo'
            ]),
            ...mapGetters('context', [
                'isMessage',
                'Message',
                'VotiRicalcolo'
            ]),
            listeview: function () {
                this.loading = false;
                return this.votiRicalcolo.dati;

            },
            generaliview: function () {
                this.tipo = "Voti Sindaco " + this.municipio + " municipio";
                return this.votiRicalcolo.dati;
            }
        },
        methods: {
            ...mapActions('context', [
                'restoreContext'
            ]),
            ...mapActions('context', [
                'ricalcolavoti'
            ]),
            ...mapActions('context', [
                'insricalcolo'
            ]),
            ...mapMutations('context', [
                'setVotiRicalcolo'
            ]),
            showSweetAlert(message) {
                this.$swal({
                    title: 'Attenzione',
                    text: message,
                    icon: 'error',
                    showCancelButton: false,
                    showCloseButton: true,
                })
            },
            saveRicalcolo(e) {
                this.loading = true;
                this.insricalcolo({ ricalcolo: e })
                    .then(() => {
                        if (this.isMessage) {
                            this.showSweetAlert(this.Message);
                            this.loading = false;
                        }
                        else {
                            this.showSweetAlertinfo("Salvataggio effettuato");
                            this.loading = false;
                        }
                    })
            },
            caricaRicalcolo(mun) {
                this.loading = true;
                this.municipio = mun;
                var t = 'S';
                this.ricalcolavoti({tipo: t, municipio: mun }).then(() => {
                    if (this.isMessage) {
                        this.showSweetAlert(this.Message);
                        this.loading = false;
                    }
                })
            },
        },
        created() {
            this.restoreContext();
            this.caricaRicalcolo(99);
        },
        mounted() {

        }
    }
</script>