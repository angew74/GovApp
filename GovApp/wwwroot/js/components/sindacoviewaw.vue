
<template>
    <div class="container" style="position: relative;overflow: auto;">
        <b-card-group deck v-if="elementi">
            <b-card>
                <b-list-group v-for="sindaco in elementi.sindaci" :key="sindaco.idSindaco">
                    <b-list-group-item class="d-flex align-items-center">
                        <v-icon scale="1.5" class="fonticonred" name="vote-yea" />
                        <span class="mr-auto redauto" style="margin-left:10px">{{sindaco.nome}} {{sindaco.cognome}}</span>
                        <span class="badge badge-red">{{sindaco.totaleSindaco}} ({{sindaco.percentualeTotale}}%) di cui {{sindaco.soloSindaco}} solo sindaco</span>
                    </b-list-group-item>
                </b-list-group>
            </b-card>
        </b-card-group>
        <div v-else>
            <div style="vertical-align:middle;padding-top:80px">
                <b-jumbotron style="background-color:transparent">
                    <p style="font-size:x-large;color:darkred">
                        <b-iconstack style="padding-right:70px" font-scale="2" animation="spin">
                            <b-icon icon="exclamation-circle" scale="0.90" shift-v="-0.25"></b-icon>
                        </b-iconstack>Niente da visualizzare
                    </p>
                </b-jumbotron>
            </div>
        </div>
        <div if="tipo === 'R'" style="margin-top:20px;text-align:right">
            <b-button v-if="elementi" type="submit" variant="primary" @click="save" style="margin-right:10px">Salva</b-button>
            <b-button type="submit" variant="primary" style="margin-right:10px" @click="previousMunicipio">Precedente</b-button>
            <b-button type="submit" variant="primary" @click="nextMunicipio">Prossimo</b-button>

        </div>
    </div>
</template>
<script>
    export default {
        name: 'sindacoviewaw',
        namespaced: true,
        props: ["elementi", "tipo","municipio"],
        components: {
        },
        data: function () {
            return {
                dati: {},
                loading: false,
                messaggio: '',
            }
        },
        computed: {
            mun: function () {
                return parseInt(this.municipio);
            }
        },
        methods: {
            showSweetAlert(message) {
                this.$swal({
                    title: 'Attenzione',
                    text: message,
                    icon: 'error',
                    showCancelButton: false,
                    showCloseButton: true,
                })
            },
            emitResult(count) {
                this.$emit('next', count);
            },
            nextMunicipio() {
                var count = this.mun;
                if (count === 99) {
                    count = 1;
                    this.emitResult(count);
                    return;
                }
                if (count === 15) {
                    count = 99;
                    this.emitResult(count);
                    return;
                }
                else {
                    count += 1;
                    this.emitResult(count);
                    return;
                }

            },
            previousMunicipio() {
                var count = this.mun;
                if (count === 99) {
                    count = 15;
                    this.emitResult(count);
                    return;
                }
                if (count === 1) {
                    count = 99;
                    this.emitResult(count);
                    return;
                }
                else {
                    count -= 1;
                    this.emitResult(count);
                    return;
                }
            },
            save() {
                this.$emit('saver', this.elementi);
            }
        },
        mounted() {
            this.dati = this.elementi;
        }
    }


</script>