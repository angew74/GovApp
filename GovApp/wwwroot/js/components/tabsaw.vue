<template>    
        <!-- Tabs with card integration -->
        <div style="font-size: 1.2em;text-align: left;min-height: 300px;margin-left: 360px;max-width: 80rem;">
            <b-tabs v-model="tabIndex"                  
                    active-nav-item-class="font-weight-bold text-uppercase text-danger"    
                    nav-item-class="font-weight-bold text-uppercase text-info"
                    content-class="mt-3">
                <b-tab active>
                    <template v-slot:title>
                        <b-spinner type="border" small></b-spinner> Sezione
                    </template>
                    <div class="container" style="font-size:0.8em">
                        <app-research v-bind:form="form" @research="cercasezione"></app-research>
                    </div>
                </b-tab>
                <b-tab>
                    <template v-slot:title>
                        <b-spinner type="border" small></b-spinner> Municipio
                    </template>
                    <div class="container" style="font-size:0.8em">
                        <app-municipio v-bind:form="form"  @research="cercamunicipio"></app-municipio>                      
                    </div>
                </b-tab>
                <b-tab>
                    <template v-slot:title>
                        <b-spinner type="border" small></b-spinner> Lista
                    </template>
                    <div class="container" style="font-size:0.8em">
                        <app-lista v-bind:form="form"  @research="cercalista"></app-lista>                      
                    </div>
                </b-tab>
                <b-tab>
                    <template v-slot:title>
                        <b-spinner type="border" small></b-spinner> Tipo Elezione
                    </template>
                    <div class="container">
                        <validation-observer ref="observer" v-slot="{ handleSubmit }">
                            <b-form @submit.stop.prevent="handleSubmit(onSubmit)">
                                <div style="margin-top:20px;text-align:right">
                                    <b-button type="submit" variant="primary">Ricerca</b-button>
                                </div>
                            </b-form>
                        </validation-observer>
                    </div>
                </b-tab>
            </b-tabs>      
        <!-- Control buttons-->
        <div class="text-center">
            <b-button-group class="mt-2" style="font-size:1.4rem">
                <b-button  variant="dark" @click="tabIndex--">Precedente</b-button>
                <b-button variant="dark" @click="tabIndex++">Prossino</b-button>
            </b-button-group>
            <div class="text-muted" style="display:none">Tab Corrente: {{ tabIndex }}</div>
        </div>
    </div>
</template>

<script>

    import { extend } from 'vee-validate';
    import research from '../components/research.vue';
    import listaaw from '../components/listaaw.vue';
    import municipioaw from '../components/municipioaw.vue';
    export default {
        name: 'tabsaw',
        namespaced: true,
        components: {
            'app-research': research,
            'app-lista': listaaw,
            'app-municipio': municipioaw
        },
        props: ['cat'],
        data: function () {
            return {
                form: {},
                show: true,
                messaggio: '',  
                tipo:'',
                tabIndex: 1
            }
        },
        mounted() {
            this.form = this.cat;
        },
        methods: {
            getValidationState({ dirty, validated, valid = null }) {
                return dirty || validated ? valid : null;
            },           
            cercalista(e) {               
                this.form= e;
                this.form.tipoInterrogazione = "1";
                this.$emit('sended', this.form, this.tabIndex);
            },
            cercamunicipio(e) {             
                this.form = e;      
                this.form.tipoInterrogazione = "2";
                this.$emit('sended', this.form, this.tabIndex);
            },
            cercasezione(e) {
                this.form = e;
                this.form.tipoInterrogazione = "3";
                this.$emit('sended', this.form, this.tabIndex);
            },
        }
    }

</script>

