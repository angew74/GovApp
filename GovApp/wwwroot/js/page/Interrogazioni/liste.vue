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
            <app-tabs @sended="ricerca" :cat="form"></app-tabs>
            <app-generali :generali="generaliview"></app-generali>
            <app-liste :liste="listeview"></app-liste>
        </b-skeleton-wrapper>
        <app-footer></app-footer>
    </div>
</template>

<script>
    import sidebaraw from '../../components/sidebaraw.vue';
    import footeraw from '../../components/footeraw.vue';
    import tabsaw from '../../components/tabsaw.vue';
    import generaliviewaw from '../../components/generaliviewaw.vue';
    import listeviewaw from '../../components/listeviewaw.vue';
    import { mapGetters, mapState, mapActions, mapMutations } from 'vuex';
    // con prop
    export default {
        components: {
            'app-sidebar': sidebaraw,
            'app-footer': footeraw,
            'app-tabs': tabsaw,
            'app-liste': listeviewaw,
            'app-generali': generaliviewaw
        },
        data: function () {
            return {
                loading: false,
                form: {},
                voti: {},
                listeview: null,
                generaliview: null                
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
            ricerca(e) {
                this.form = e;
                this.form.tipo = "L";
                this.loading = true;
                this.researchvoti({ authMethod: this.authMode, research: e }).then(() => {
                    if (this.isMessage) {
                        this.showSweetAlert(this.Message);
                        this.loading = false;
                    }
                    else {
                        if (this.VotiVisualizzazione) {
                            this.voti = this.VotiVisualizzazione;
                            this.listeview = this.voti.interrogazione;
                            this.generaliview = this.voti.interrogazione;
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
        }
    }
</script>