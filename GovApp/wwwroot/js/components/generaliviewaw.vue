<template>
    <div class="container" style="position: relative;overflow: auto;margin-top:10px">
        <b-card-group v-if="dati" deck>
            <b-card>
                <b-card-header style="background-color:transparent;text-align:center">
                    <v-icon scale="1.5" class="fonticonred" name="person-booth" />
                    <span class="badge" style="font-size:1.1em;color:darkred">Voti {{dati.tipo}}</span>
                </b-card-header>
                <div style="font-weight:bolder">
                    <b-list-group horizontal>
                        <b-list-group-item style="border-right:none">
                            Bianche: {{dati.bianche}}
                        </b-list-group-item>
                        <b-list-group-item class="flex-fill" style="border-right:none;border-left:none">
                            Nulle: {{dati.nulle}}
                        </b-list-group-item>
                        <b-list-group-item class="flex-fill" style="border-right:none;border-left:none">
                            Contestate: {{dati.contestate}}
                        </b-list-group-item>
                        <b-list-group-item class="flex-fill" style="border-right:none;border-left:none">
                            Solo Sindaco: {{dati.soloSindaco}}
                        </b-list-group-item>
                        <b-list-group-item class="flex-fill" style="border-right:none;border-left:none">
                            Totale Valide: {{dati.totaleValide}}
                        </b-list-group-item>
                        <b-list-group-item class="flex-fill" style="border-right:none;border-left:none">
                            Votanti: {{dati.votanti}}
                        </b-list-group-item>
                        <b-list-group-item class="flex-fill" style="border-left:none">
                            Iscritti: {{dati.iscritti}}
                        </b-list-group-item>
                    </b-list-group>
                </div>
            </b-card>
        </b-card-group>
        <div v-else>
            <div style="vertical-align:middle;padding-top:80px">
                <b-jumbotron>
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
        props: ["generali","tipo"],
        components: {
        },
        data: function () {
            return {
                dati: {},
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