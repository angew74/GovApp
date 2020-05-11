<template>
    <b-container>       
        <div v-if="contents.length">
            <b-row>
                <div v-bind:key="data.index" v-for="data in contents">
                    <b-col l="4">
                        <b-card v-bind:title="data.Contenuto"
                                v-bind:img-src="data.ContenutoThumb"
                                img-alt="Image"
                                img-top
                                tag="article"
                                style="max-width: 20rem;"
                                class="mb-2">
                            <b-card-text>{{ `${data.ContenutoDescrizione.slice(0,100)}...` }}</b-card-text>
                            <b-button href="#" variant="primary">Vai</b-button>
                        </b-card>
                    </b-col>
                </div>
            </b-row>
        </div>
        <div v-else>
            <h5>Nessun contenuto da visuaiizzare</h5>
        </div>
    </b-container>
</template>
<script>
    import axios from "axios";
    export default {
        name: 'cardsaw',  
        props: {
            codice: {
                type: String
            }
        },
        data() {
            return {
                contents: []
            };
        }, created: function () {
            console.log('codice cards')
            console.log(this.codice) //prints out an empty string
        }, mounted() {
            axios({
                method: 'get',
                url: '/values/content',
                params: {
                    "type": this.codice
                }
            })
                .then(function (response) {
                    this.contents = response.data.contenutomodel;
                })
                .catch(function (error) {
                    console.log(error);
                });
        }
    };
</script>