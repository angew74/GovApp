<template>
    <b-container>
        <div v-if="contenuti.length">
            <b-row>
                <div v-bind:key="data.index" v-for="data in contenuti">
                    <b-col l="4">
                        <b-card v-bind:title="data.Contenuto"
                                v-bind:img-src="data.ContenutoThumb"
                                img-alt="Image"
                                img-top
                                tag="article"
                                style="max-width: 20rem;"
                                class="mb-2">
                            <b-card-text>{{ '${data.ContenutoDescrizione}' }}</b-card-text>
                            <b-button href="#" variant="primary">Vai</b-button>
                        </b-card>
                    </b-col>
                </div>
            </b-row>
        </div>
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
    </b-container>
</template>
<script>  
    export default {
        name: 'cardsaw',
        props: ['contenuti']
    };
</script>