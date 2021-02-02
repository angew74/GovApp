<template>
    <div class="container" style="position: relative;overflow: auto;margin-top:10px">
        <b-card-group v-if="generali" deck>
            <b-card>
                <b-card-header v-if="generali.tipo" style="background-color:transparent;text-align:center">
                    <v-icon scale="1.5" class="fonticonred" name="person-booth" />
                    <span class="badge" style="font-size:1.1em;color:darkred">
                        Voti {{generali.tipo}}
                    </span>
                    <span class="badge" v-if="generali.sezioniPervenute" style="font-size:1.1em;color:darkred">
                        Sezioni Pervenute: {{generali.sezioniPervenute}} 
                    </span>
                </b-card-header>
                <div style="font-weight:bolder">
                    <b-list-group horizontal v-if="generali.bianche">
                        <b-list-group-item style="border-right:none">
                            Bianche: {{generali.bianche}}
                        </b-list-group-item>
                        <b-list-group-item class="flex-fill" style="border-right:none;border-left:none">
                            Nulle: {{generali.nulle}}
                        </b-list-group-item>
                        <b-list-group-item class="flex-fill" style="border-right:none;border-left:none">
                            Contestate: {{generali.contestate}}
                        </b-list-group-item>
                        <b-list-group-item  class="flex-fill" style="border-right:none;border-left:none">
                            Solo Sindaco: {{generali.soloSindaco}}
                        </b-list-group-item>
                        <b-list-group-item class="flex-fill" style="border-right:none;border-left:none">
                            Totale Valide: {{generali.totaleValide}}
                        </b-list-group-item>
                        <b-list-group-item class="flex-fill" style="border-right:none;border-left:none">
                            Votanti: {{generali.votanti}}
                        </b-list-group-item>
                        <b-list-group-item class="flex-fill" style="border-left:none">
                            Iscritti: {{generali.iscritti}}
                        </b-list-group-item>
                    </b-list-group>
                    <b-list-group v-else horizontal>
                        <b-list-group-item class="flex-fill" style="border-right:none;border-left:none">
                            Votanti: {{generali.votanti}}
                        </b-list-group-item>
                        <b-list-group-item class="flex-fill" style="border-right:none;border-left:none">
                            Iscritti: {{generali.iscritti}} 
                        </b-list-group-item>
                        <b-list-group-item class="flex-fill" style="border-left:none">
                            Percentuale Votanti: {{generali.percentualeVotanti}}%
                        </b-list-group-item>
                    </b-list-group>
                </div>
            </b-card>
        </b-card-group>
        <div v-else>
            <div style="vertical-align:middle;padding-top:80px">
                <v-icon v-if="tipo" scale="1.5" class="fonticonred" name="person-booth" />
                <span class="badge" style="font-size:1.3em;color:darkred">
                    {{tipo}}
                </span>
                <b-jumbotron style="background-color:transparent">
                    <p style="font-size:x-large;color:darkred">
                        <b-iconstack style="padding-right:70px" font-scale="2" animation="spin">
                            <b-icon icon="exclamation-circle" scale="0.90" shift-v="-0.25"></b-icon>
                        </b-iconstack>Niente da visualizzare
                    </p>
                </b-jumbotron>
            </div>
        </div>
    </div>
</template>
<script>
    export default {
        name: 'generaliviewaw',
        namespaced: true,
        props: ["generali", "tipo"],
        components: {
        },
        data: function () {
            return {
                dati: null,
                loading: false,
                messaggio: ''
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
        },
        mounted() {
            this.dati = this.generali;
        }
    }


</script>