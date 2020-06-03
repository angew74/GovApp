<template>
    <b-container>
        <div v-if="contenuti.length" style="margin-top:100px">
            <b-carousel id="carousel-fade"
                        style="text-shadow: 0px 0px 2px #000"
                        fade
                        indicators
                        img-width="1024"
                        img-height="480">
                <b-carousel-slide v-bind:key="data.index" v-for="(data,index) in contenuti"
                                  v-bind:caption="data.contenutoTitolo"
                                  v-bind:img-src="data.contenutoImmagine"></b-carousel-slide>
            </b-carousel>
        </div>
    </b-container>
</template>
<script>
    export default {
        name: 'carouselaw',
        props: ['contenuti']
    };
</script>