﻿<template>
    <b-container>
        <div v-if="contenuti.length">
            <div role="tablist" style="padding-top:80px">
                <b-card v-bind:key="data.index" v-for="(data,index) in contenuti"
                        no-body class="mb-1 text-center border-0">
                    <b-card-header header-tag="header" class="p-1 border-0" style="font-size:1.5rem" role="tab">
                        <b-button block v-b-toggle="'accordion-' + index" variant="dark">
                            <v-icon name="expand-arrows-alt" scale="1.5" />                           
                            {{data.contenutoHeader}}
                        </b-button>
                    </b-card-header>
                    <b-collapse v-bind:id=" 'accordion-' + index" accordion="my-accordion" role="tabpanel">
                        <b-card-text class="border-0" style="font-size:1.6rem">                           
                                <b-icon v-bind:icon="data.contenutoIcon" class="rounded-circle bg-danger p-2"  width="1.6em" height="1.6em"  variant="light"></b-icon>
                                {{ data.contenutoTesto }}                             
                        </b-card-text>
                        <b-card-footer footer-tag="footer" class="card-footer bg-transparent border-0">
                            <a v-bind:href="data.contenutoLink">
                                <b-icon style="color:black" icon="forward" animation="spin" font-scale="3" shift-v="3"></b-icon>
                            </a>
                        </b-card-footer>
                    </b-collapse>
                </b-card>
            </div>
        </div>
        <div v-else>
            <div style="vertical-align:middle;padding-top:80px">
                <b-jumbotron>
                    <p style="font-size:x-large;color:darkred">
                        <b-iconstack style="padding-right:70px" font-scale="2" animation="spin">
                            <b-icon style="color:darkred" icon="exclamation-circle" scale="0.90" shift-v="-0.25"></b-icon>
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