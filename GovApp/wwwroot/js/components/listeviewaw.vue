
<template>
    <div class="container" style="position: relative;overflow: auto;">
        <b-card-group deck v-if="dati">
            <b-card>
                <b-list-group v-for="lista in dati.liste" :key="lista.idLista">
                    <b-list-group-item class="d-flex align-items-center">
                        <v-icon scale="1.5" class="fonticonred" name="vote-yea" />
                        <span class="mr-auto redauto" style="margin-left:10px">{{lista.denominazione}}</span>
                        <span class="badge badge-red">{{lista.votiLista}}</span>
                    </b-list-group-item>
                </b-list-group>
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
        name: 'listeviewaw',
        namespaced: true,
        props: ["liste"],
        components: {
        },
        data: function () {
            return {
                dati: {},                 
                loading:false,
                messaggio: ''
            }
        },
        methods: {            showSweetAlert(message) {
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
            this.dati = this.liste;
        }
    }


</script>