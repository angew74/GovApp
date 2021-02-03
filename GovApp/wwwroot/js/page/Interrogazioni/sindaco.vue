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
            <app-tabs @sended="ricerca" :tabIndex="tab" :cat="form"></app-tabs>
            <app-generali :tipo="tipo" :generali="generaliview"></app-generali>
            <app-sindaco :tipo="ricalcolo" :municipio="municipio" :elementi="sindacoview"></app-sindaco>
        </b-skeleton-wrapper>
        <app-footer></app-footer>
    </div>
</template>

<script>
    import sidebaraw from '../../components/sidebaraw.vue';
    import footeraw from '../../components/footeraw.vue';
    import tabsaw from '../../components/tabsaw.vue';
    import generaliviewaw from '../../components/generaliviewaw.vue';
    import sindacoviewaw from '../../components/sindacoviewaw.vue';
    import { mapGetters, mapState, mapActions, mapMutations } from 'vuex';
    // con prop
    export default {
        components: {
            'app-sidebar': sidebaraw,
            'app-footer': footeraw,
            'app-tabs': tabsaw,
            'app-sindaco': sindacoviewaw,
            'app-generali': generaliviewaw
        },
        data: function () {
            return {
                loading: false,
                form: {},
                voti: {},
                sindacoview: null,
                generaliview: null,
                ricalcolo: 'I',
                tipo: null,
                tab: 0,
                municipio: 99
            }
        },
        computed: {
            ...mapState('context', [
                'sezione',
                'message',
                'votiVisualizzazione'
            ]),
            ...mapGetters('context', [
                'Sezione',
                'isMessage',
                'Message',
                'VotiVisualizzazione'
            ]),
        },
        methods: {
            ...mapActions('context', [
                'restoreContext'
            ]),
            ...mapActions('context', [
                'researchvoti'
            ]),
            ...mapMutations('context', [
                'setVotiVisualizzazione'
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
            ricerca(e,t) {
                this.form = e;
                this.form.tipo = "S";
                this.loading = true;
                this.tab = t;
                this.researchvoti({ authMethod: this.authMode, research: e }).then(() => {
                    if (this.isMessage) {
                        this.showSweetAlert(this.Message);
                        this.loading = false;
                    }
                    else {
                        if (this.VotiVisualizzazione) {
                            switch (this.form.tipoInterrogazione) {
                                case "1":
                                    this.tipo = "Voti Sindaco";
                                    break;
                                case "2":
                                    this.tipo = "Voti Sindaco Municipio " + this.form.municipio;
                                    break;
                                case "3":
                                    this.tipo = "Voti Sindaco Sezione " + this.form.sezione;
                                    break;
                            }
                            this.voti = this.VotiVisualizzazione;
                            this.sindacoview = this.voti.dati;
                            this.generaliview = this.voti.dati;
                        }
                        this.loading = false;
                    }
                })
            },
        },
        created() {
            this.restoreContext()
        },
        mounted() {
            this.form.tipo = "S";
        }
    }
</script>